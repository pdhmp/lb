using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using newNFIXConn;
using NCommonTypes;
using NestSymConn;
using NestDLL;

namespace Tomahawk
{
    public class TomahawkExec
    {
        public List<THSymbolLine> Symbols = new List<THSymbolLine>();
        private SortedDictionary<string, int> SymbolsIndex = new SortedDictionary<string, int>();

        public List<FIXOrder> Orders = new List<FIXOrder>();
        private volatile object OrdersSync = new object();

        private bool OrdersSent = false;

        newFIXConn curFixConn;

        public TomahawkExec()
        {
            curFixConn = new newFIXConn(@"T:\Log\FIX\Tomahawk\config\TomahawkConfig.cfg");
            curFixConn.OnUpdate += new EventHandler(OrderUpdate);

            SymConn.Instance.BovespaSource = NEnuns.NSYMSources.FLEXBSE;
            SymConn.Instance.OnData += new EventHandler(OnMarketData);
            
        }

        void OnMarketData(object sender, EventArgs e)
        {
            MarketUpdateList curList = (MarketUpdateList)e;

            foreach (MarketUpdateItem curItem in curList.ItemsList)
            {
                if (SymbolsIndex.ContainsKey(curItem.Ticker))
                {
                    Symbols[SymbolsIndex[curItem.Ticker]].MarketData.Update(curItem);
                }
            }
        }

        public void LoadData()
        {
            LoadHedgeInfo();
            LoadOrdersFile(@"N:\Quant\Strategies\Tomahawk\Orders\Tomahawk" + DateTime.Today.ToString("yyyyMMdd") + ".csv");
            LoadPrevPos();
        }


        public void LoadOrdersFile(string filename)
        {
            StreamReader sr = new StreamReader(filename);

            string header = sr.ReadLine();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] splitLine = line.Split(',');

                THSymbolLine curSymbol = new THSymbolLine();
                curSymbol.IdSecurity = int.Parse(splitLine[0]);
                curSymbol.Ticker = splitLine[1];
                curSymbol.QtyBuy = int.Parse(splitLine[2]);
                curSymbol.PxBuy = double.Parse(splitLine[3].Replace('.',','));
                curSymbol.QtySell = int.Parse(splitLine[4]);
                curSymbol.PxSell = double.Parse(splitLine[5].Replace('.', ','));
                if (curSymbol.IdSecurity == FHIdSecurity || curSymbol.IdSecurity == MHIdSecurity)
                {
                    curSymbol.SecurityType = "HEDGE";
                }
                else
                {
                    curSymbol.SecurityType = "SECURITIES";
                }


                if (!SymbolsIndex.ContainsKey(curSymbol.Ticker))
                {
                    Symbols.Add(curSymbol);
                    UpdateSymbolsIndex();
                    SymConn.Instance.Subscribe(curSymbol.Ticker, 2);
                }
                else
                {
                    Console.WriteLine("Symbol already loaded: {0}. Discarding line",curSymbol.Ticker);
                }                
            }

            sr.Close();
        }
        private void LoadPrevPos()
        {
            string SQLExpression = "SELECT [EXCHANGE TICKER], [ID TICKER], POSITION FROM NESTRT.DBO.TB000_POSICAO_ATUAL (NOLOCK) WHERE [ID PORTFOLIO] = 18 AND [ID BOOK] = 11 AND [ID SECTION] = 231";
            DataTable result = new DataTable();

            using (newNestConn curConn = new newNestConn())
            {
                result = curConn.Return_DataTable(SQLExpression);
            }

            foreach (DataRow curRow in result.Rows)
            {
                string Ticker = curRow[0].ToString();
                int IdSecurity = int.Parse(curRow[1].ToString());
                int Position = int.Parse(curRow[2].ToString());

                if (SymbolsIndex.ContainsKey(Ticker))
                {
                    Symbols[SymbolsIndex[Ticker]].PrevPos = Position;
                }
                else
                {
                    int exchange = 2;

                    THSymbolLine curLine = new THSymbolLine();
                    curLine.IdSecurity = IdSecurity;
                    curLine.Ticker = Ticker;
                    curLine.PrevPos = Position;
                    curLine.QtyBuy = 0;
                    curLine.PxBuy = 0;
                    curLine.QtySell = 0;
                    curLine.PxSell = 0;
                    if (IdSecurity == FHIdSecurity || IdSecurity == MHIdSecurity)
                    {
                        curLine.SecurityType = "HEDGE";
                        exchange = 1;
                    }
                    else
                    {
                        curLine.SecurityType = "SECURITIES";
                    }

                    Symbols.Add(curLine);
                    UpdateSymbolsIndex();

                    SymConn.Instance.Subscribe(curLine.Ticker, exchange);
                }

            }

        }
        private void UpdateSymbolsIndex()
        {
            SymbolsIndex.Clear();

            for (int i = 0; i < Symbols.Count; i++)
            {
                SymbolsIndex.Add(Symbols[i].Ticker, i);
            }
        }

        public void SendOrders()
        {
            if (!OrdersSent)
            {
                foreach (THSymbolLine curSymbol in Symbols)
                {
                    if (curSymbol.IdSecurity != FHIdSecurity && curSymbol.IdSecurity != MHIdSecurity)
                    {
                        if (curSymbol.QtyBuy > 0 && curSymbol.PxBuy > 0)
                        {
                            curFixConn.sendOrder("THWK", curSymbol.IdSecurity, curSymbol.QtyBuy, curSymbol.PxBuy, 18, 11, 231, "", NEnuns.NOrderType.Buy, true);
                        }
                        if (curSymbol.QtySell > 0 && curSymbol.PxSell > 0)
                        {
                            curFixConn.sendOrder("THWK", curSymbol.IdSecurity, (-1)*curSymbol.QtySell, curSymbol.PxSell, 18, 11, 231, "", NEnuns.NOrderType.Sell, true);
                        }
                        if (curSymbol.PrevPos != 0)
                        {
                            int ordQty = (-1) * curSymbol.PrevPos;
                            string ordType = (ordQty > 0 ? NEnuns.NOrderType.Buy : NEnuns.NOrderType.Sell);

                            curFixConn.sendOrder("THWK", curSymbol.IdSecurity, ordQty, -2, 18, 11, 231, "", ordType, true);
                        }
                    }
                }

                OrdersSent = true;
            }
        }
        public void CancelAllOrders()
        {
            lock (OrdersSync)
            {
                foreach (FIXOrder curOrder in Orders)
                {
                    if (curOrder.strStatus != "FILLED")
                    {
                        curFixConn.cancelOrder(curOrder.OrigClOrdID);
                    }
                }
            }
        }

        private void OrderUpdate(object sender, EventArgs e)
        {
            lock (OrdersSync)
            {
                FIXOrder curOrder = (FIXOrder)e;
                bool found = false;

                for (int i = 0; i < Orders.Count; i++)
                {
                    if (curOrder.OrigClOrdID == Orders[i].OrigClOrdID)
                    {
                        Orders[i] = curOrder;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Orders.Add(curOrder);
                }

                SymbolsUpdate(curOrder.IdTicker);
            }
        }
        private void SymbolsUpdate(int IdSecurity)
        {
            int BuyQty = 0;
            int SellQty = 0;
            double BuyValue = 0;
            double SellValue = 0;
            string Security = "";
            double Last = 0;

            foreach (FIXOrder curOrder in Orders)
            {
                if (curOrder.IdTicker == IdSecurity)
                {
                    Security = curOrder.Symbol;
                    if (curOrder.strSide == "BUY")
                    {
                        BuyQty += (int)curOrder.Done;
                        BuyValue += curOrder.ExecPrice * curOrder.Done;
                    }
                    else if (curOrder.strSide == "SELL")
                    {
                        SellQty += (int)curOrder.Done;
                        SellValue += curOrder.ExecPrice * curOrder.Done;
                    }
                }
            }

            if (SymbolsIndex.ContainsKey(Security))
            {
                Symbols[SymbolsIndex[Security]].ExecBuyQTY = BuyQty;
                Symbols[SymbolsIndex[Security]].ExecBuyPX = (int)(BuyQty > 0 ? (BuyValue / BuyQty) : 0);                
                Symbols[SymbolsIndex[Security]].ExecSellQty = SellQty;
                Symbols[SymbolsIndex[Security]].ExecSellPx = (int)(SellQty > 0 ? (SellValue / SellQty) : 0);
            }
        }


        #region Hedge        

        public int FHIdSecurity = 76792;
        public int MHIdSecurity = 209842;

        public string FHTicker = "INDZ11";
        public string MHTicker = "WINZ11";

        public int FHPrevPos = 0;
        public int MHPrevPos = 0;

        public int FHNewPos = 0;
        public int MHNewPos = 0;

        public int FHAction = 0;
        public int MHAction = 0;

        public double FHValue = 0;
        public double MHValue = 0;

        public int FHExec = 0;
        public int MHExec = 0;

        public double NetValue;

        DateTime startHedge;
        DateTime endHedge;

        bool StartedHedge = false;

        private void LoadHedgeInfo()
        {
            startHedge = DateTime.Today.AddHours(18).AddMinutes(15);
            endHedge = startHedge.AddMinutes(15);

            FHIdSecurity = 82933;
            MHIdSecurity = 222612;

            FHTicker = "INDJ12";
            MHTicker = "WINJ12";

            THSymbolLine FHLine = new THSymbolLine();
            FHLine.IdSecurity = FHIdSecurity;
            FHLine.Ticker = FHTicker;
            FHLine.SecurityType = "HEDGE";

            THSymbolLine MHLine = new THSymbolLine();
            MHLine.IdSecurity = MHIdSecurity;
            MHLine.Ticker = MHTicker;
            MHLine.SecurityType = "HEDGE";

            Symbols.Add(FHLine);
            Symbols.Add(MHLine);

            UpdateSymbolsIndex();

            SymConn.Instance.Subscribe(FHLine.Ticker, 1);
            SymConn.Instance.Subscribe(MHLine.Ticker, 1);
        }

        public void CalculateHedge()
        {           
            double HdgdValue = 0;
            NetValue = 0;

            lock (OrdersSync)
            {
                foreach (THSymbolLine curLine in Symbols)
                {
                    if (curLine.IdSecurity != FHIdSecurity && curLine.IdSecurity != MHIdSecurity)
                    {
                        NetValue += curLine.NetValue;
                    }
                    else
                    {
                        if (curLine.IdSecurity == FHIdSecurity) { HdgdValue += curLine.NetValue; }
                        else { HdgdValue += (curLine.NetValue / 5.0); }
                    }
                }
            }

            double FHPrice = Symbols[SymbolsIndex[FHTicker]].Last;
            FHNewPos = (-1) * (int)Math.Round(NetValue / FHPrice / 5.0, 0) * 5;
            FHValue = FHNewPos * FHPrice;
            FHPrevPos = Symbols[SymbolsIndex[FHTicker]].PrevPos;                        
            FHExec = Symbols[SymbolsIndex[FHTicker]].ExecBuyQTY - Symbols[SymbolsIndex[FHTicker]].ExecSellQty;
            FHAction = FHNewPos - (FHPrevPos + FHExec);

            double MHPrice = Symbols[SymbolsIndex[MHTicker]].Last;
            MHNewPos = (-1) * (int)Math.Round((NetValue + FHNewPos * FHPrice) / (MHPrice / 5.0), 0);
            MHValue = MHNewPos * MHPrice/5;
            MHPrevPos = Symbols[SymbolsIndex[MHTicker]].PrevPos;
            MHExec = Symbols[SymbolsIndex[MHTicker]].ExecBuyQTY - Symbols[SymbolsIndex[MHTicker]].ExecSellQty;
            MHAction = MHNewPos - (MHPrevPos + MHExec);                                  
        }

        public void SendHedge()
        {
            lock (OrdersSync)
            {
                if (FHAction != 0)
                {
                    string ordType = (FHAction > 0 ? NEnuns.NOrderType.Buy : NEnuns.NOrderType.Sell);
                    curFixConn.sendOrder("THWK", FHIdSecurity, FHAction, -1, 18, 11, 231, "", ordType, true);
                }
                if (MHAction != 0)
                {
                    string ordType = (MHAction > 0 ? NEnuns.NOrderType.Buy : NEnuns.NOrderType.Sell);
                    curFixConn.sendOrder("THWK", MHIdSecurity, MHAction, -1, 18, 11, 231, "", ordType, true);
                }
            }
        }
        
        /*public void SendHedgeOrder()
        {
            bool allClosed = true;
            double NetValue = 0;
            double HdgdValue = 0;

            lock (OrdersSync)
            {
                foreach (THSymbolLine curLine in Symbols)
                {
                    if (curLine.IdSecurity != FHIdSecurity && curLine.IdSecurity != MHIdSecurity)
                    {
                        NetValue += curLine.NetValue;
                        if (curLine.TradingStatus != "G_MKTCONTROL") { allClosed = false; }
                    }
                    else
                    {
                        if (curLine.IdSecurity == FHIdSecurity) { HdgdValue += curLine.NetValue; }
                        else { HdgdValue += (curLine.NetValue / 5.0); }
                    }
                }
            }

            if (allClosed && !StartedHedge)
            {
                startHedge = DateTime.Now;
                endHedge = startHedge.AddMinutes(15);
                StartedHedge = true;
            }

            if (DateTime.Now >= startHedge)
            {
                StartedHedge = true;
            }

            if (StartedHedge && DateTime.Now <= endHedge)
            {
                double FHPrice = Symbols[SymbolsIndex[FHTicker]].Last;
                int FHNewPos = (-1) * (int)Math.Round(NetValue / FHPrice / 5.0, 0) * 5;
                int FHTotOrder = FHNewPos - Symbols[SymbolsIndex[FHTicker]].PrevPos;
                int FHExec = Symbols[SymbolsIndex[FHTicker]].ExecBuyQTY - Symbols[SymbolsIndex[FHTicker]].ExecSellQty;

                double MHPrice = Symbols[SymbolsIndex[MHTicker]].Last;
                int MHNewPos = (-1) * (int)Math.Round((NetValue + FHNewPos * FHPrice) / (MHPrice / 5.0), 0);
                int MHTotOrder = MHNewPos - Symbols[SymbolsIndex[MHTicker]].PrevPos;
                int MHExec = Symbols[SymbolsIndex[MHTicker]].ExecBuyQTY - Symbols[SymbolsIndex[MHTicker]].ExecSellQty;

                TimeSpan TotalTime = endHedge - startHedge;
                TimeSpan Elapsed = DateTime.Now - startHedge;

                double expectedPerc = Elapsed.TotalSeconds / TotalTime.TotalSeconds;

                int FHExpected = (int)Math.Round(FHTotOrder * expectedPerc / 5,0) * 5;
                int FHAction = FHExpected - FHExec;

                int MHExpected = (int)Math.Round(MHTotOrder * expectedPerc, 0);
                int MHAction = MHExpected - MHExec;

                if (FHAction != 0)
                {
                    string ordType = (FHAction > 0 ? NEnuns.NOrderType.Buy : NEnuns.NOrderType.Sell);
                    //curFixConn.sendOrder("THWK", FHIdSecurity, FHAction, -1, 18, 11, 231, "", ordType, true);
                }
                if (MHAction != 0)
                {
                    string ordType = (MHAction > 0 ? NEnuns.NOrderType.Buy : NEnuns.NOrderType.Sell);
                    //curFixConn.sendOrder("THWK", MHIdSecurity, MHAction, -1, 18, 11, 231, "", ordType, true);
                }                
            }
        }*/

        
        #endregion

        string TestOrderCLORDID = "";

        public void SendTestOrder()
        {
            //TestOrderCLORDID = curFixConn.sendOrder("THWK", 209842, -1, -1, 18, 11, 231, "", NEnuns.NOrderType.Sell, true);
        }
        public void CancelTestOrder()
        {
            //curFixConn.cancelOrder(TestOrderCLORDID);
        }
    }
}
