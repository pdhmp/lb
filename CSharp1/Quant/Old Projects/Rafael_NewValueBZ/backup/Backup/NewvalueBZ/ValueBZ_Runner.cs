using System;
using System.Collections.Generic;
using System.Text;

using System.Data;

using NestDLL;
using NestSymConn;
using NestGeneric;
using NestQuant.Common;
using NCommonTypes;

namespace NewValueBZ
{
    class ValueBZ_Runner
    {
        public SortedDictionary<int, ValueCalc> SectorCalcs = new SortedDictionary<int, ValueCalc>();
        NestSocketServer curSocket = new NestSocketServer();
        //FIXConn curFixConn = new FIXConn(@"T:\RatesBZ_config.cfg");
        SortedDictionary<string, int> SimbolToIdTicker = new SortedDictionary<string, int>();
        int StratCount = 0;

        public bool IsFixConnected
        {
            get { return false; }//curFixConn.IsAliveSession; }
        }

        public ValueBZ_Runner()
        {
            SymConn.Instance.ConnectSym();
            SymConn.Instance.OnData += new EventHandler(NewMarketData);
            using (newNestConn conn = new newNestConn())
            {

                string SQLString = "SELECT Id_Ticker_Component FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Component NOT IN (16307,16310,16311,16317,16318) AND Id_Ticker_Composite=21350 GROUP BY Id_Ticker_Component ORDER BY Id_Ticker_Component";
                //string SQLString = "SELECT 16313 AS Id_Ativo";
                DataTable dt = conn.Return_DataTable(SQLString);

                StratCount = dt.Rows.Count;

                foreach (DataRow curRow in dt.Rows)
                {
                    int curId = Convert.ToInt16(curRow[0].ToString());
                    //int curId = Convert.ToInt16(16322);
                    InitStrategy(curId);
                }
            }
            curSocket.StartListen(12310);
            curSocket.NewMessage += new EventHandler(ReceivedMessage);
            SubscribeAllStrategies();
        }

        public void InitStrategy(int IdTickerComposite)
        {
            ValueCalc CalcConsumer = new ValueCalc();
            CalcConsumer.initStrategy(DateTime.Now.Date, IdTickerComposite, false, false);

            SectorCalcs.Add(IdTickerComposite, CalcConsumer);
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
                    string tempTicker = curConn.Execute_Query_String("SELECT Simbolo FROM NESTDB.dbo.Tb001_Ativos (NOLOCK) WHERE Id_Ativo=" + curIdTicker);
                    SimbolToIdTicker.Add(tempTicker, curIdTicker);
                    curValueCalc.PositionPEs[curIdTicker].Ticker = tempTicker;
                    SymConn.Instance.Subscribe(tempTicker);
                }
            }
        }

        private void NewMarketData(object sender, EventArgs origE)
        {
            SymDataEventArgs e = (SymDataEventArgs)origE;

            int curIdTicker = -1;
            SimbolToIdTicker.TryGetValue(e.Ticker, out curIdTicker);

            if (e.Value[1] != 0 && curIdTicker != -1)
            {
                foreach (ValueCalc curValueCalc in SectorCalcs.Values)
                {
                    TickerPE curValueItem;

                    if (curValueCalc.PositionPEs.TryGetValue(curIdTicker, out curValueItem))
                    {
                        curValueItem.curPrice = e.Value[1];
                    }
                }
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
                    //curOrderCalc.Stager.RemoveAllStagedOrders();
                }
            }

            if (curMessagePart[0] == "CMD")
            {
                if (curMessagePart[1] == "CALCSTAGED")
                {
                    //curOrderCalc.CalcFromPosition(curRates.stratPositions);
                }
            }

            if (curMessagePart[0] == "CMD")
            {
                if (curMessagePart[1] == "SENDSTAGED")
                {
                    //curOrderCalc.Stager.SendAllStagedOrders(false);
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

                    foreach (ValueCalc curValueCalc in SectorCalcs.Values)
                    {
                        curValueCalc.StratRecalc();

                        foreach (KeyValuePair<int, TickerPE> curValueItems in curValueCalc.PositionPEs)
                        {
                            TickerPE curValueItem = curValueItems.Value;

                            string curValueItemString = curValueItem.IdTickerComposite.ToString() + (char)16 +
                                                curValueItem.IdTicker.ToString() + (char)16 +
                                                curValueItem.Ticker.ToString() + (char)16 +
                                                curValueItem.closePrice.ToString() + (char)16 +
                                                curValueItem.EPSDate.ToString() + (char)16 +
                                                curValueItem.EPSKnownDate.ToString() + (char)16 +
                                                curValueItem.EPSValue.ToString() + (char)16 +
                                                curValueItem.EPSShareNumber.ToString() + (char)16 +
                                                curValueItem.curShareNumberDate.ToString() + (char)16 +
                                                curValueItem.curShareNumber.ToString() + (char)16 +
                                                curValueItem.adjEPS.ToString() + (char)16 +
                                                curValueItem.curPrice.ToString() + (char)16 +
                                                curValueItem.curPE.ToString() + (char)16 +
                                                curValueItem.curSignal.ToString() + (char)16;

                            curSocket.SendMessage("INF" + (char)16 + "STRAT" + (char)16 + "ADD" + (char)16 + curValueItemString);
                        }
                    }
                    curSocket.SendMessage("INF" + (char)16 + "STRAT" + (char)16 + "ENDADD");
                }

                if (curMessagePart[1] == "STA")
                {
                    // --------------------------  SEND STATUS TO CLIENT --------------------------------------
                    curSocket.SendMessage("INF" + (char)16 + "STA" + (char)16 + "MKTDATASYM" + (char)16 + SymConn.Instance.IsSymConnected.ToString());
                    curSocket.SendMessage("INF" + (char)16 + "STA" + (char)16 + "FIXCONN" + (char)16 + IsFixConnected.ToString());
                }
                if (curMessagePart[1] == "ORDSTG")
                {

                    curSocket.SendMessage("ORDINF" + (char)16 + "ORDSTG" + (char)16 + "CLEARALL");

                    // --------------------------  SEND STAGED ORDERS TO CLIENT --------------------------------------
                    //for (int i = curOrderCalc.Stager.StagedOrders.Count - 1; i >= 0; i--)
                    //{
                    //    //curSocket.SendMessage("ORDINF" + (char)16 + "ORDSTG" + (char)16 + "ADD"
                    //    //                                                + (char)16 + curOrderCalc.Stager.StagedOrders[i].OrdID
                    //    //                                                + (char)16 + curOrderCalc.Stager.StagedOrders[i].Id_Ticker.ToString()
                    //    //                                                + (char)16 + curOrderCalc.Stager.StagedOrders[i].Quantity.ToString()
                    //    //                                                + (char)16 + curOrderCalc.Stager.StagedOrders[i].Price.ToString()
                    //    //                                                );
                    //}
                    curSocket.SendMessage("ORDINF" + (char)16 + "CTRL" + (char)16 + "ENDBATCH");
                }
            }
        }


    }
}
