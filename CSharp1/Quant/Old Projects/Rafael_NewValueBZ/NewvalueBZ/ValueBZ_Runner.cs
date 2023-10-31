using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using NestDLL;
using NestSymConn;
using NestGeneric;
using NestQuant.Common;
using NCommonTypes;
using NOrderStager;
using NFIXConn;

namespace NewValueBZ
{
    class ValueBZ_Runner
    {
        NestSocketServer curSocket = new NestSocketServer();
        OrderCalc_Percent curOrderCalcMH;
        OrderCalc_Percent curOrderCalcNF;

        //NFIXConn.FIXConn curFixConn = new NFIXConn.FIXConn(@"T:\Log\FIX\ValBZ\config\ValueBZConfig.cfg");
        NFIXConn.FIXConn curFixConn;

        int StratCount = 0;
        public SortedDictionary<int, ValueCalc> SectorCalcs = new SortedDictionary<int, ValueCalc>();
        SortedDictionary<string, int> SimbolToIdTicker = new SortedDictionary<string, int>();

        private double _StrategyMHNAV = 0;
        public double StrategyMHNAV
        {
            get { return _StrategyMHNAV; }
            set { _StrategyMHNAV = value; }
        }

        private double _StrategyNFNAV = 0;
        public double StrategyTopNAV
        {
            get { return _StrategyNFNAV; }
            set { _StrategyNFNAV = value; }
        }

        public bool IsFixConnected
        {
            get { return curFixConn.IsAliveSession; }
        }

        public ValueBZ_Runner()
        {
            Start();
        }

        public void Start()
        {
            InitStrategyValBZ();

            SymConn.Instance.OnData += new EventHandler(NewMarketData);

            curOrderCalcMH.Stager.NewOrderToSend += new EventHandler(SendStagedOrder);
            curOrderCalcNF.Stager.NewOrderToSend += new EventHandler(SendStagedOrder);

            curSocket.NewMessage += new EventHandler(ReceivedMessage);
            //curFixConn.ReceivedFill += new EventHandler(curOrderCalcMH.ReceiveFill);
            //curFixConn.ReceivedFill += new EventHandler(curOrderCalcNF.ReceiveFill);
            
            //curRates.PositionChange += new EventHandler(StratUpdated);

            //SymConn.Instance.ConnectSym();

            curSocket.StartListen(NEnuns.QuantPorts.ValBZ); 

            SubscribeAllStrategies();
        }

        private void InitStrategyValBZ()
        {
            curOrderCalcMH = new OrderCalc_Percent(NEnuns.QuantPortfolios.MileHigh,
                                                   NEnuns.QuantBooks.Quantitative,
                                                   NEnuns.QuantSections.ValueBZ, "MH_VALBZ");

            curOrderCalcNF = new OrderCalc_Percent(NEnuns.QuantPortfolios.NestFund,
                                                   NEnuns.QuantBooks.Quantitative,
                                                   NEnuns.QuantSections.ValueBZ, "NF_VALBZ");

            using (newNestConn conn = new newNestConn())
            {

                string SQLString = "SELECT Id_Ticker_Component FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Component NOT IN (16307,16310,16311,16317,16318) AND Id_Ticker_Composite=21350 GROUP BY Id_Ticker_Component ORDER BY Id_Ticker_Component";
                //string SQLString = "SELECT 16323 AS Id_Ativo";
                DataTable dt = conn.Return_DataTable(SQLString);

                StratCount = dt.Rows.Count;

                foreach (DataRow curRow in dt.Rows)
                {
                    int curId = Convert.ToInt16(curRow[0].ToString());
                    //int curId = Convert.ToInt16(16322);
                    InitComposite(curId);
                }
            }
        }


        public void InitComposite(int IdTickerComposite)
        {
            ValueCalc curValueCalc = new ValueCalc();
            curValueCalc.initStrategy(DateTime.Now.Date, IdTickerComposite, true, true);

            curValueCalc.StratTotalWeight = 1F / StratCount;
            curValueCalc.SimNAV = 0;

            curValueCalc.StratCalcPrevPos();

            curValueCalc.StratRecalc();

            SectorCalcs.Add(IdTickerComposite, curValueCalc);

        }

        private void SendStagedOrder(object sender, EventArgs e)
        {
            StagedOrderItem OrderToSend = (StagedOrderItem)e;
            curFixConn.sendOrder(OrderToSend.OrdID,
                              OrderToSend.Id_Ticker,
                              OrderToSend.Quantity,
                              OrderToSend.Price,
                              OrderToSend.IdPortfolio,
                              OrderToSend.IdBook,
                              OrderToSend.IdSection);
                       
            if (OrderToSend.IdPortfolio == NEnuns.QuantPortfolios.MileHigh) { curOrderCalcMH.Positions.UpdatePosition(OrderToSend.Id_Ticker, OrderToSend.Quantity); }
            else if (OrderToSend.IdPortfolio == NEnuns.QuantPortfolios.NestFund) { curOrderCalcNF.Positions.UpdatePosition(OrderToSend.Id_Ticker, OrderToSend.Quantity); }            
        }

        public void SubscribeAllStrategies()
        {
            foreach (ValueCalc curValueCalc in SectorCalcs.Values)
            {
                SubscribeStratTickers(curValueCalc);
            }
        }

        private void SubscribeStratTickers(ValueCalc curValueCalc)
        {
            foreach (int curIdTicker in curValueCalc.PositionPEs.Keys)
            {
                using (newNestConn curConn = new newNestConn())
                {
                    string tempTicker = curConn.Execute_Query_String("SELECT NestTicker FROM NESTDB.dbo.Tb001_Securities (NOLOCK) WHERE IdSecurity=" + curIdTicker);
                    if (tempTicker == "MYPK3")
                    {
                        int a = 0;
                    }
                    SimbolToIdTicker.Add(tempTicker, curIdTicker);
                    curValueCalc.PositionPEs[curIdTicker].Ticker = tempTicker;
                    SymConn.Instance.Subscribe(tempTicker);
                }
            }
        }

        private void NewMarketData(object sender, EventArgs origE)
        {
            //SymDataEventArgs e = (SymDataEventArgs)origE;

            MarketUpdateList curUpdateList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
            {
                int curIdTicker = -1;

                if (SimbolToIdTicker.TryGetValue(curUpdateItem.Ticker, out curIdTicker))
                {
                    foreach (ValueCalc curValueCalc in SectorCalcs.Values)
                    {
                        TickerPE curValueItem;

                        if (curValueCalc.PositionPEs.TryGetValue(curIdTicker, out curValueItem))
                        {
                            curValueItem.Update(curUpdateItem);

                            if (curUpdateItem.FLID == NestFLIDS.Last ||
                                curUpdateItem.FLID == NestFLIDS.Close ||
                                curUpdateItem.FLID == NestFLIDS.AucLast)
                            {
                                if (curValueCalc.RefPrice.ContainsKey(curValueItem.IdTicker))
                                {
                                    curValueCalc.RefPrice[curValueItem.IdTicker] = curValueItem.MarketData.ValidLast;
                                }
                                else
                                {
                                    curValueCalc.RefPrice.Add(curValueItem.IdTicker, curValueItem.MarketData.ValidLast);
                                }

                                curValueItem.StratContrib = curValueItem.MarketData.Change * curValueItem.Weight * curValueItem.closeSignal;

                                //curValueItem.curPrice = curValueItem.MarketData.ValidLast;
                                //curValueItem.StratContrib = curValueItem.DayChange * curValueItem.Weight * curValueItem.closeSignal;
                            }
                        }
                    } 
                }



                /*if (curUpdateItem.FLID == NestFLIDS.Last && curIdTicker != -1)
                {
                    foreach (ValueCalc curValueCalc in SectorCalcs.Values)
                    {
                        TickerPE curValueItem;

                        if (curValueCalc.PositionPEs.TryGetValue(curIdTicker, out curValueItem))
                        {
                            curValueItem.curPrice = curUpdateItem.ValueDouble;
                            curValueItem.StratContrib = curValueItem.DayChange * curValueItem.Weight * curValueItem.closeSignal;
                        }
                    }
                }

                if (curUpdateItem.FLID == NestFLIDS.Close)
                {
                    foreach (ValueCalc curValueCalc in SectorCalcs.Values)
                    {
                        TickerPE curValueItem;

                        if (curValueCalc.PositionPEs.TryGetValue(curIdTicker, out curValueItem))
                        {
                            if (curValueItem.curPrice == 0)
                            {
                                curValueItem.curPrice = curUpdateItem.ValueDouble;
                                curValueItem.StratContrib = curValueItem.DayChange * curValueItem.Weight * curValueItem.closeSignal;
                            }
                        }
                    }
                }*/
            }
        }

        public void Stop()
        {
            SymConn.Instance.Dispose();
            //curFixConn.Dispose();
            curSocket.StopListen();
        }

        // ================================================   REPORT TO CLIENTS ====================================================================

        public void ReceivedMessage(object sender, EventArgs origE)
        {
            string curMessage = ((NestSocketServer.MsgEventArgs)origE).strMessage;

            string[] curMessagePart = curMessage.Split((char)16);

            // ===============================  CLIENT COMMANDS ========================================

            if (curMessagePart[0] == "CMD")
            {
                if (curMessagePart[1] == "CLEARSTAGED")
                {
                    curOrderCalcMH.Stager.RemoveAllStagedOrders();
                    curOrderCalcNF.Stager.RemoveAllStagedOrders();
                }

                if (curMessagePart[1] == "CALCSTAGED")
                {
                    curOrderCalcMH.Clear();
                    curOrderCalcNF.Clear();

                    double USDBRL = NestDLL.Utils.GetUSDBRL();

                    foreach (ValueCalc curValueCalc in SectorCalcs.Values)
                    {
                        curValueCalc.StratRecalc();
                        curOrderCalcMH.CalcFromPosition(curValueCalc.stratPositions, _StrategyMHNAV, false);
                        

                        if (!double.IsNaN(USDBRL))
                        {
                            curOrderCalcNF.CalcFromPosition(curValueCalc.stratPositions, _StrategyNFNAV * USDBRL, false);
                        }
                        else
                        {
                            curSocket.SendMessage("ERRMSG" + (char)16 + "ERROR" + (char)16 + "Unable to get USDBRL from database. NestFund orders not staged.");
                        }
                    }
                }
                
                if (curMessagePart[1] == "UPDATENAV")
                {
                    if (curMessagePart[2] == "VALBZ")
                    {
                        if (curMessagePart[3] == "MH")
                        {
                            double newNAV;

                            if (Double.TryParse(curMessagePart[4], out newNAV))
                            {
                                _StrategyMHNAV = newNAV;
                            }
                        }

                        if (curMessagePart[3] == "NF")
                        {
                            double newNAV;

                            if (Double.TryParse(curMessagePart[4], out newNAV))
                            {
                                _StrategyNFNAV = newNAV;
                            }
                        }

                        /*double newNAV;

                        if (Double.TryParse(curMessagePart[3], out newNAV))
                        {
                            foreach (ValueCalc curValuecalc in SectorCalcs.Values)
                            {
                                curValuecalc.SimNAV = newNAV;
                                curValuecalc.StratRecalc();
                            }
                        }*/                       
                    }
                }

                if (curMessagePart[1] == "SENDSTAGED")
                {
                    curOrderCalcMH.Stager.SendAllStagedOrders(false);
                    curOrderCalcNF.Stager.SendAllStagedOrders(false);
                }

                if (curMessagePart[1] == "SENDORD")
                {
                    int Portfolio = int.Parse(curMessagePart[2]);
                    string OrdID = curMessagePart[3];
                    bool auction = Convert.ToBoolean(curMessagePart[4]);

                    if (Portfolio == NEnuns.QuantPortfolios.MileHigh) { curOrderCalcMH.Stager.SendSingleStagedOrder(OrdID, auction); }
                    else if (Portfolio == NEnuns.QuantPortfolios.NestFund) { curOrderCalcNF.Stager.SendSingleStagedOrder(OrdID, auction); }
                                        
                    curSocket.SendMessage("ORDINF" + (char)16 + "SENTORD" + (char)16 + OrdID);
                }

                if (curMessagePart[1] == "CANCELORD")
                {
                    int Portfolio = int.Parse(curMessagePart[2]);
                    string OrdID = curMessagePart[3];

                    bool ordRemoved = false;

                    if (Portfolio == NEnuns.QuantPortfolios.MileHigh) { ordRemoved = curOrderCalcMH.Stager.RemoveSingleStagedOrder(OrdID); }
                    else if (Portfolio == NEnuns.QuantPortfolios.NestFund) { ordRemoved = curOrderCalcNF.Stager.RemoveSingleStagedOrder(OrdID); }
                                        
                    if(ordRemoved)
                        curSocket.SendMessage("ORDINF" + (char)16 + "ORDCANCEL" + (char)16 + OrdID);
                }
            }

            // ===============================  CLIENT REQUESTS ========================================
            if (curMessagePart[0] == "REQ")
            {
                if (curMessagePart[1] == "MKT")
                {
                    // --------------------------  SEND MARKET DATA TO CLIENT --------------------------------------
                    //curSocket.SendMessage("INF" + (char)16 + "MKT" + (char)16 + "LAST" + (char)16 + curMarketDataItem.Last.ToString("0.00"));
                    //curSocket.SendMessage("INF" + (char)16 + "MKT" + (char)16 + "BID" + (char)16 + curMarketDataItem.Bid.ToString("0.00"));
                    //curSocket.SendMessage("INF" + (char)16 + "MKT" + (char)16 + "ASK" + (char)16 + curMarketDataItem.Ask.ToString("0.00"));
                    //curSocket.SendMessage("INF" + (char)16 + "MKT" + (char)16 + "BIDSZ" + (char)16 + curMarketDataItem.BidSize.ToString("0.00"));
                    //curSocket.SendMessage("INF" + (char)16 + "MKT" + (char)16 + "ASKSZ" + (char)16 + curMarketDataItem.AskSize.ToString("0.00"));
                    //curSocket.SendMessage("INF" + (char)16 + "MKT" + (char)16 + "VOLUME" + (char)16 + curMarketDataItem.Volume.ToString("0.00"));
                }
                if (curMessagePart[1] == "STRAT")
                {
                    // --------------------------  SEND STRATEGY DATA TO CLIENT --------------------------------------

                    double curSimNAV = 0;

                    foreach (ValueCalc curValueCalc in SectorCalcs.Values)
                    {
                        curValueCalc.StratRecalc();

                        curSimNAV = curValueCalc.SimNAV;

                        foreach (KeyValuePair<int, TickerPE> curValueItems in curValueCalc.PositionPEs)
                        {
                            TickerPE curValueItem = curValueItems.Value;

                            string curValueItemString = curValueItem.EncodeTXT();

                            curSocket.SendMessage("INF" + (char)16 + "STRAT" + (char)16 + "ADD" + (char)16 + curValueItemString);
                        }
                    }
                    curSocket.SendMessage("INF" + (char)16 + "STRAT" + (char)16 + "ENDADD");
                    curSocket.SendMessage("INF" + (char)16 + "STRAT" + (char)16 + "CURNAVMH" + (char)16 + _StrategyMHNAV.ToString("#,##0.00"));
                    curSocket.SendMessage("INF" + (char)16 + "STRAT" + (char)16 + "CURNAVNF" + (char)16 + _StrategyNFNAV.ToString("#,##0.00"));
                }

                if (curMessagePart[1] == "STA")
                {
                    // --------------------------  SEND STATUS TO CLIENT --------------------------------------
                    curSocket.SendMessage("INF" + (char)16 + "STA" + (char)16 + "MKTDATASYM" + (char)16 + SymConn.Instance.IsSymConnected.ToString());
                    curSocket.SendMessage("INF" + (char)16 + "STA" + (char)16 + "FIXCONN" + (char)16 + IsFixConnected.ToString());
                }
                if (curMessagePart[1] == "ORDSTG")
                {
                    //--------------------------  SEND STAGED ORDERS TO CLIENT --------------------------------------
                    curSocket.SendMessage(OrderStager.MultipleStagersEncodeString(curOrderCalcMH.Stager, curOrderCalcNF.Stager));
                 }
            }
        }


    }
}
