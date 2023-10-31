//using System;
//using System.Collections.Generic;
//using System.Text;
//using NestSymConn;
//using System.Threading;
//using NCommonTypes;

//namespace FIX_BMF
//{
//    public class MarketDataAnalyzer
//    {
//        private static MarketDataAnalyzer _Instance;
//        private static volatile object syncroot = new object();

//        public static MarketDataAnalyzer Instance
//        {
//            get
//            {
//                if (_Instance == null)
//                {
//                    lock (syncroot)
//                    {
//                        if (_Instance == null)
//                        {
//                            _Instance = new MarketDataAnalyzer();
//                        }
//                    }
//                }

//                return _Instance;
//            }
//        }


//        public List<NCommonTypes.MarketDataItem> LocalMarketData = new List<NCommonTypes.MarketDataItem>();
//        public List<NCommonTypes.MarketDataItem> SymMarketData = new List<NCommonTypes.MarketDataItem>();

//        private List<DateTime> LastReceivedLocalData = new List<DateTime>();
//        private List<DateTime> LastReceivedSymData = new List<DateTime>();

//        private List<MarketDepthItem> LocalMarketDepth = new List<MarketDepthItem>();
//        private List<MarketDepthItem> SymMarketDepth = new List<MarketDepthItem>();

//        private List<DateTime> LastReceivedLocalDepth = new List<DateTime>();
//        private List<DateTime> LastReceivedSymDepth = new List<DateTime>();

//        private SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();

//        private Mutex LocalDataMutex = new Mutex();
//        private Mutex SymDataMutex = new Mutex();
//        private Mutex LocalDepthMutex = new Mutex();
//        private Mutex SymDepthMutex = new Mutex();

//        private Timer tmrCheckSym;
//        private List<string> TickersToCheck = new List<string>();


//        private MarketDataAnalyzer()
//        {
//            SymConn.Instance.OnData += new EventHandler(NewSymData);
//            SymConn.Instance.OnDepth += new EventHandler(NewSymDepth);

//            //TimerCallback run = RunCheck;
//            //tmrCheckSym = new Timer(run, null, 10000, 500);

//        }        

//        public void SubscribeTicker(string Ticker)
//        {
//            LocalDataMutex.WaitOne();
//            SymDataMutex.WaitOne();
//            LocalDepthMutex.WaitOne();
//            SymDepthMutex.WaitOne();

//            int curID = SubListIndex.Count;
//            NCommonTypes.MarketDataItem localData = new NCommonTypes.MarketDataItem();
//            NCommonTypes.MarketDataItem symData = new NCommonTypes.MarketDataItem();
//            MarketDepthItem localDepth = new MarketDepthItem();
//            MarketDepthItem symDepth = new MarketDepthItem();

//            SubListIndex.Add(Ticker, curID);

//            LocalMarketData.Add(localData);
//            SymMarketData.Add(symData);
//            LocalMarketDepth.Add(localDepth);
//            SymMarketDepth.Add(symDepth);
//            LastReceivedLocalData.Add(DateTime.MinValue);
//            LastReceivedLocalDepth.Add(DateTime.MinValue);
//            LastReceivedSymData.Add(DateTime.MinValue);
//            LastReceivedSymDepth.Add(DateTime.MinValue);

//            SymDepthMutex.ReleaseMutex();
//            LocalDepthMutex.ReleaseMutex();
//            SymDataMutex.ReleaseMutex();
//            LocalDataMutex.ReleaseMutex();

//            //SymConn.Instance.Subscribe(Ticker);
//            //SymConn.Instance.SubscribeDepth(Ticker, 2);

//            TickersToCheck.Add(Ticker);
//        }

//        public void ReceiveLocalMarketData(NCommonTypes.MarketDataItem curItem)
//        {
//            LocalDataMutex.WaitOne();

//            int curID = 0;

//            if (SubListIndex.TryGetValue(curItem.Ticker, out curID))
//            {
//                LocalMarketData[curID].Last = curItem.Last;
//                LocalMarketData[curID].Open = curItem.Open;
//                LocalMarketData[curID].High = curItem.High;
//                LocalMarketData[curID].Low = curItem.Low;
//                LocalMarketData[curID].Close = curItem.Close;
//                LocalMarketData[curID].Bid = curItem.Bid;
//                LocalMarketData[curID].Ask = curItem.Ask;
//                LocalMarketData[curID].BidSize = curItem.BidSize;
//                LocalMarketData[curID].AskSize = curItem.AskSize;
//                LocalMarketData[curID].AucCond = curItem.AucCond;
//                LocalMarketData[curID].AucLast = curItem.AucLast;
//                LocalMarketData[curID].Volume = curItem.Volume;
//                LocalMarketData[curID].VWAP = curItem.VWAP;
//                LocalMarketData[curID].ExchangeSymbol = curItem.ExchangeSymbol;

//                LastReceivedLocalData[curID] = DateTime.Now;                
//            }
//            else
//            {
//                int a = 0;
//            }

//            LocalDataMutex.ReleaseMutex();
//        }
//        public void ReceiveLocalMarketDepth(MarketDepthItem curItem)
//        {
//            LocalDepthMutex.WaitOne();

//            int curID = 0;

//            if (SubListIndex.TryGetValue(curItem.Ticker, out curID))
//            {
//                LocalMarketDepth[curID].Ask1 = curItem.Ask1;
//                LocalMarketDepth[curID].Ask2 = curItem.Ask2;
//                LocalMarketDepth[curID].Ask3 = curItem.Ask3;
//                LocalMarketDepth[curID].Ask4 = curItem.Ask4;
//                LocalMarketDepth[curID].Ask5 = curItem.Ask5;

//                LocalMarketDepth[curID].AskSize1 = curItem.AskSize1;
//                LocalMarketDepth[curID].AskSize2 = curItem.AskSize2;
//                LocalMarketDepth[curID].AskSize3 = curItem.AskSize3;
//                LocalMarketDepth[curID].AskSize4 = curItem.AskSize4;
//                LocalMarketDepth[curID].AskSize5 = curItem.AskSize5;

//                LocalMarketDepth[curID].Bid1 = curItem.Bid1;
//                LocalMarketDepth[curID].Bid2 = curItem.Bid2;
//                LocalMarketDepth[curID].Bid3 = curItem.Bid3;
//                LocalMarketDepth[curID].Bid4 = curItem.Bid4;
//                LocalMarketDepth[curID].Bid5 = curItem.Bid5;

//                LocalMarketDepth[curID].BidSize1 = curItem.BidSize1;
//                LocalMarketDepth[curID].BidSize2 = curItem.BidSize2;
//                LocalMarketDepth[curID].BidSize3 = curItem.BidSize3;
//                LocalMarketDepth[curID].BidSize4 = curItem.BidSize4;
//                LocalMarketDepth[curID].BidSize5 = curItem.BidSize5;

//                LastReceivedLocalDepth[curID] = DateTime.Now;
                
//            }
//            else
//            {
//                int a = 0;
//            }

//            LocalDepthMutex.ReleaseMutex();
//        }

//        private void NewSymDepth(object sender, EventArgs curLevel2Item)
//        {
//            Level2Item curItem = (Level2Item)curLevel2Item;

//            int curID = 0;

//            SymDepthMutex.WaitOne();

//            if (SubListIndex.TryGetValue(curItem.Ticker, out curID))
//            {
//                switch (curItem.Position)
//                {
//                    case 1:
//                        if (curItem.Side == 'b')
//                        {
//                            SymMarketDepth[curID].Bid1 = (curItem.Price != double.MinValue ? curItem.Price : SymMarketDepth[curID].Bid1);
//                            SymMarketDepth[curID].BidSize1 = (curItem.Quantity != int.MinValue ? curItem.Quantity : SymMarketDepth[curID].BidSize1);                            
//                        }
//                        else if (curItem.Side == 'a')
//                        {
//                            SymMarketDepth[curID].Ask1 = (curItem.Price != double.MinValue ? curItem.Price : SymMarketDepth[curID].Ask1);
//                            SymMarketDepth[curID].AskSize1 = (curItem.Quantity != int.MinValue ? curItem.Quantity : SymMarketDepth[curID].AskSize1);
//                        }
//                        break;
//                    case 2:
//                        if (curItem.Side == 'b')
//                        {
//                            SymMarketDepth[curID].Bid2 = (curItem.Price != double.MinValue ? curItem.Price : SymMarketDepth[curID].Bid2);
//                            SymMarketDepth[curID].BidSize2 = (curItem.Quantity != int.MinValue ? curItem.Quantity : SymMarketDepth[curID].BidSize2);
//                        }
//                        else if (curItem.Side == 'a')
//                        {
//                            SymMarketDepth[curID].Ask2 = (curItem.Price != double.MinValue ? curItem.Price : SymMarketDepth[curID].Ask2);
//                            SymMarketDepth[curID].AskSize2 = (curItem.Quantity != int.MinValue ? curItem.Quantity : SymMarketDepth[curID].AskSize2);
//                        }
//                        break;
//                    case 3:
//                        if (curItem.Side == 'b')
//                        {
//                            SymMarketDepth[curID].Bid3 = (curItem.Price != double.MinValue ? curItem.Price : SymMarketDepth[curID].Bid3);
//                            SymMarketDepth[curID].BidSize3 = (curItem.Quantity != int.MinValue ? curItem.Quantity : SymMarketDepth[curID].BidSize3);
//                        }
//                        else if (curItem.Side == 'a')
//                        {
//                            SymMarketDepth[curID].Ask3 = (curItem.Price != double.MinValue ? curItem.Price : SymMarketDepth[curID].Ask3);
//                            SymMarketDepth[curID].AskSize3 = (curItem.Quantity != int.MinValue ? curItem.Quantity : SymMarketDepth[curID].AskSize3);
//                        }
//                        break;
//                    case 4:
//                        if (curItem.Side == 'b')
//                        {
//                            SymMarketDepth[curID].Bid4 = (curItem.Price != double.MinValue ? curItem.Price : SymMarketDepth[curID].Bid4);
//                            SymMarketDepth[curID].BidSize4 = (curItem.Quantity != int.MinValue ? curItem.Quantity : SymMarketDepth[curID].BidSize4);
//                        }
//                        else if (curItem.Side == 'a')
//                        {
//                            SymMarketDepth[curID].Ask4 = (curItem.Price != double.MinValue ? curItem.Price : SymMarketDepth[curID].Ask4);
//                            SymMarketDepth[curID].AskSize4 = (curItem.Quantity != int.MinValue ? curItem.Quantity : SymMarketDepth[curID].AskSize4);
//                        }
//                        break;
//                    case 5:
//                        if (curItem.Side == 'b')
//                        {
//                            SymMarketDepth[curID].Bid5 = (curItem.Price != double.MinValue ? curItem.Price : SymMarketDepth[curID].Bid5);
//                            SymMarketDepth[curID].BidSize5 = (curItem.Quantity != int.MinValue ? curItem.Quantity : SymMarketDepth[curID].BidSize5);
//                        }
//                        else if (curItem.Side == 'a')
//                        {
//                            SymMarketDepth[curID].Ask5 = (curItem.Price != double.MinValue ? curItem.Price : SymMarketDepth[curID].Ask5);
//                            SymMarketDepth[curID].AskSize5 = (curItem.Quantity != int.MinValue ? curItem.Quantity : SymMarketDepth[curID].AskSize5);
//                        }
//                        break;
//                    default:
//                        break;
//                }

//                LastReceivedSymDepth[curID] = DateTime.Now;
                
//            }
//            else
//            {
//                int a = 0;
//            }

//            SymDepthMutex.ReleaseMutex();
//        }
//        private void NewSymData(object sender, EventArgs e)
//        {
//            SymDataEventArgs curSymData = (SymDataEventArgs)e;

//            NCommonTypes.MarketDataItem curMarketItem = new NCommonTypes.MarketDataItem();
//            string Ticker = curSymData.Ticker;
                       
//            int curID = 0;

//            SymDataMutex.WaitOne();

//            if (SubListIndex.TryGetValue(Ticker, out curID))
//            {                   
//                if (curSymData.Value[1] != 0) SymMarketData[curID].Last = curSymData.Value[1];
//                if (curSymData.Value[8] != 0) SymMarketData[curID].Open = curSymData.Value[8];
//                if (curSymData.Value[4] != 0) SymMarketData[curID].High = curSymData.Value[4];
//                if (curSymData.Value[3] != 0) SymMarketData[curID].Low = curSymData.Value[3];
//                if (curSymData.Value[9] != 0) SymMarketData[curID].Bid = curSymData.Value[9];
//                if (curSymData.Value[10] != 0) SymMarketData[curID].Ask = curSymData.Value[10];
//                if (curSymData.Value[22] != 0) SymMarketData[curID].BidSize = curSymData.Value[22];
//                if (curSymData.Value[23] != 0) SymMarketData[curID].AskSize = curSymData.Value[23];
//                if (curSymData.Value[6] != 0) SymMarketData[curID].Volume = curSymData.Value[6];
//                //if (curSymData.Value[2] != 0) SymMarketData[curID].VWAP = curSymData.Value[5];
//                if (curSymData.Value[500] != 0) SymMarketData[curID].AucLast = curSymData.Value[500];
//                if (curSymData.Value[998] != 0) SymMarketData[curID].Close = curSymData.Value[998];
//                if (curSymData.FLID[997] != "" && curSymData.FLID[997] != null) SymMarketData[curID].AucCond = curSymData.FLID[997];

//                if (curSymData.Value[48] != 0 || curSymData.Value[528] != 0)
//                {
//                    Level2Item curB1 = new Level2Item();
//                    curB1.Broker = int.Parse(curSymData.Value[1080].ToString());
//                    curB1.Position = 1;
//                    curB1.Price = (curSymData.Value[48] != 0 ? double.Parse(curSymData.Value[48].ToString()) : double.MinValue);
//                    curB1.Quantity = (curSymData.Value[528] != 0 ? int.Parse(curSymData.Value[528].ToString()) : int.MinValue);
//                    curB1.Side = 'b';
//                    curB1.Ticker = curSymData.Ticker;

//                    NewSymDepth(this, curB1);
//                }

//                if (curSymData.Value[49] != 0 || curSymData.Value[529] != 0)
//                {
//                    Level2Item curB1 = new Level2Item();
//                    curB1.Broker = int.Parse(curSymData.Value[1081].ToString());
//                    curB1.Position = 2;
//                    curB1.Price = (curSymData.Value[49] != 0 ? double.Parse(curSymData.Value[49].ToString()) : double.MinValue);
//                    curB1.Quantity = (curSymData.Value[529] != 0 ? int.Parse(curSymData.Value[529].ToString()) : int.MinValue);
//                    curB1.Side = 'b';
//                    curB1.Ticker = curSymData.Ticker;

//                    NewSymDepth(this, curB1);
//                }

//                if (curSymData.Value[50] != 0 || curSymData.Value[530] != 0)
//                {
//                    Level2Item curB1 = new Level2Item();
//                    curB1.Broker = int.Parse(curSymData.Value[1082].ToString());
//                    curB1.Position = 3;
//                    curB1.Price = (curSymData.Value[50] != 0 ? double.Parse(curSymData.Value[50].ToString()) : double.MinValue);
//                    curB1.Quantity = (curSymData.Value[530] != 0 ? int.Parse(curSymData.Value[530].ToString()) : int.MinValue);
//                    curB1.Side = 'b';
//                    curB1.Ticker = curSymData.Ticker;

//                    NewSymDepth(this, curB1);
//                }

//                if (curSymData.Value[51] != 0 || curSymData.Value[531] != 0)
//                {
//                    Level2Item curB1 = new Level2Item();
//                    curB1.Broker = int.Parse(curSymData.Value[1083].ToString());
//                    curB1.Position = 4;
//                    curB1.Price = (curSymData.Value[51] != 0 ? double.Parse(curSymData.Value[51].ToString()) : double.MinValue);
//                    curB1.Quantity = (curSymData.Value[531] != 0 ? int.Parse(curSymData.Value[531].ToString()) : int.MinValue);
//                    curB1.Side = 'b';
//                    curB1.Ticker = curSymData.Ticker;

//                    NewSymDepth(this, curB1);
//                }

//                if (curSymData.Value[56] != 0 || curSymData.Value[536] != 0)
//                {
//                    Level2Item curB1 = new Level2Item();
//                    curB1.Broker = int.Parse(curSymData.Value[1088].ToString());
//                    curB1.Position = 5;
//                    curB1.Price = (curSymData.Value[56] != 0 ? double.Parse(curSymData.Value[56].ToString()) : double.MinValue);
//                    curB1.Quantity = (curSymData.Value[536] != 0 ? int.Parse(curSymData.Value[536].ToString()) : int.MinValue);
//                    curB1.Side = 'b';
//                    curB1.Ticker = curSymData.Ticker;

//                    NewSymDepth(this, curB1);
//                }

//                // =========================   ASKS  ==================
//                if (curSymData.Value[52] != 0 || curSymData.Value[532] != 0)
//                {
//                    Level2Item curB1 = new Level2Item();
//                    curB1.Broker = int.Parse(curSymData.Value[1084].ToString());
//                    curB1.Position = 1;
//                    curB1.Price = (curSymData.Value[52] != 0 ? double.Parse(curSymData.Value[52].ToString()) : double.MinValue);
//                    curB1.Quantity = (curSymData.Value[532] != 0 ? int.Parse(curSymData.Value[532].ToString()) : int.MinValue);
//                    curB1.Side = 'a';
//                    curB1.Ticker = curSymData.Ticker;

//                    NewSymDepth(this, curB1);
//                }

//                if (curSymData.Value[53] != 0 || curSymData.Value[533] != 0)
//                {
//                    Level2Item curB1 = new Level2Item();
//                    curB1.Broker = int.Parse(curSymData.Value[1085].ToString());
//                    curB1.Position = 2;
//                    curB1.Price = (curSymData.Value[53] != 0 ? double.Parse(curSymData.Value[53].ToString()) : double.MinValue);
//                    curB1.Quantity = (curSymData.Value[533] != 0 ? int.Parse(curSymData.Value[533].ToString()) : int.MinValue);
//                    curB1.Side = 'a';
//                    curB1.Ticker = curSymData.Ticker;

//                    NewSymDepth(this, curB1);
//                }

//                if (curSymData.Value[54] != 0 || curSymData.Value[534] != 0)
//                {
//                    Level2Item curB1 = new Level2Item();
//                    curB1.Broker = int.Parse(curSymData.Value[1086].ToString());
//                    curB1.Position = 3;
//                    curB1.Price = (curSymData.Value[54] != 0 ? double.Parse(curSymData.Value[54].ToString()) : double.MinValue);
//                    curB1.Quantity = (curSymData.Value[534] != 0 ? int.Parse(curSymData.Value[534].ToString()) : int.MinValue);
//                    curB1.Side = 'a';
//                    curB1.Ticker = curSymData.Ticker;

//                    NewSymDepth(this, curB1);
//                }

//                if (curSymData.Value[55] != 0 || curSymData.Value[535] != 0)
//                {
//                    Level2Item curB1 = new Level2Item();
//                    curB1.Broker = int.Parse(curSymData.Value[1087].ToString());
//                    curB1.Position = 4;
//                    curB1.Price = (curSymData.Value[55] != 0 ? double.Parse(curSymData.Value[55].ToString()) : double.MinValue);
//                    curB1.Quantity = (curSymData.Value[535] != 0 ? int.Parse(curSymData.Value[535].ToString()) : int.MinValue);
//                    curB1.Side = 'a';
//                    curB1.Ticker = curSymData.Ticker;

//                    NewSymDepth(this, curB1);
//                }

//                if (curSymData.Value[60] != 0 || curSymData.Value[540] != 0)
//                {
//                    Level2Item curB1 = new Level2Item();
//                    curB1.Broker = int.Parse(curSymData.Value[1092].ToString());
//                    curB1.Position = 5;
//                    curB1.Price = (curSymData.Value[60] != 0 ? double.Parse(curSymData.Value[60].ToString()) : double.MinValue);
//                    curB1.Quantity = (curSymData.Value[540] != 0 ? int.Parse(curSymData.Value[540].ToString()) : int.MinValue);
//                    curB1.Side = 'a';
//                    curB1.Ticker = curSymData.Ticker;

//                    NewSymDepth(this, curB1);
//                }                

//                LastReceivedSymData[curID] = DateTime.Now;
//            }
//            else
//            {
//                int a = 0;
//            }

//            SymDataMutex.ReleaseMutex();
//        }

//        public bool CheckLocalData(string Ticker)
//        {
//            bool noErrors = true;

//            LocalDataMutex.WaitOne();
//            LocalDepthMutex.WaitOne();

//            int curID = 0;

//            if (SubListIndex.TryGetValue(Ticker, out curID))
//            {
//                if (LocalMarketData[curID].Bid >= LocalMarketData[curID].Ask &&
//                    LocalMarketData[curID].Ask > 0)
//                {
//                    noErrors = false;
//                }
                
//                if (LocalMarketData[curID].Bid != LocalMarketDepth[curID].Bid1) 
//                { 
//                    noErrors = false; 
//                }
//                if (LocalMarketData[curID].BidSize != LocalMarketDepth[curID].BidSize1)
//                {
//                    noErrors = false;
//                }

//                if (LocalMarketData[curID].Ask != LocalMarketDepth[curID].Ask1)
//                {
//                    noErrors = false;
//                }
//                if (LocalMarketData[curID].AskSize != LocalMarketDepth[curID].AskSize1)
//                {
//                    noErrors = false;
//                }
                           
//                if (LocalMarketDepth[curID].Bid1 <= LocalMarketDepth[curID].Bid2 &&
//                    LocalMarketDepth[curID].Bid2 > 0) 
//                { 
//                    noErrors = false; 
//                }
//                if (LocalMarketDepth[curID].Bid2 <= LocalMarketDepth[curID].Bid3 &&
//                    LocalMarketDepth[curID].Bid3 > 0)
//                {
//                    noErrors = false;
//                }
//                if (LocalMarketDepth[curID].Bid3 <= LocalMarketDepth[curID].Bid4 &&
//                    LocalMarketDepth[curID].Bid4 > 0)
//                {
//                    noErrors = false;
//                }
//                if (LocalMarketDepth[curID].Bid4 <= LocalMarketDepth[curID].Bid5 &&
//                    LocalMarketDepth[curID].Bid5 > 0)
//                {
//                    noErrors = false;
//                }

//                if (LocalMarketDepth[curID].Ask1 >= LocalMarketDepth[curID].Ask2 &&
//                    LocalMarketDepth[curID].Ask2 > 0)
//                {
//                    noErrors = false;
//                }
//                if (LocalMarketDepth[curID].Ask2 >= LocalMarketDepth[curID].Ask3 &&
//                    LocalMarketDepth[curID].Ask3 > 0)
//                {
//                    noErrors = false;
//                }
//                if (LocalMarketDepth[curID].Ask3 >= LocalMarketDepth[curID].Ask4 &&
//                    LocalMarketDepth[curID].Ask4 > 0)
//                {
//                    noErrors = false;
//                }
//                if (LocalMarketDepth[curID].Ask4 >= LocalMarketDepth[curID].Ask5 &&
//                    LocalMarketDepth[curID].Ask5 > 0)
//                {
//                    noErrors = false;
//                }

//                if (!noErrors)
//                {
//                    int a = 0;
//                }

//            }

//            LocalDepthMutex.ReleaseMutex();
//            LocalDataMutex.ReleaseMutex();

//            return noErrors;
//        }

//        public void RunCheck(object sender)
//        {
//            foreach (string ticker in TickersToCheck)
//            {
//                CheckSymData(ticker);
//            }
//        }

//        public int cursor = -1;

//        public bool CheckSymData(string Ticker)
//        {
//            SymDataMutex.WaitOne();
//            SymDepthMutex.WaitOne();
//            LocalDataMutex.WaitOne();
//            LocalDepthMutex.WaitOne();

//            int curID = int.MinValue;
//            bool hasErrors = false;
//            if (SubListIndex.TryGetValue(Ticker, out curID))
//            {
//                if (LastReceivedLocalData[curID].AddMilliseconds(500) < DateTime.Now &&
//                    LastReceivedLocalDepth[curID].AddMilliseconds(500) < DateTime.Now &&
//                    LastReceivedSymData[curID].AddMilliseconds(500) < DateTime.Now &&
//                    LastReceivedSymDepth[curID].AddMilliseconds(500) < DateTime.Now)
//                {
//                    if (Ticker == Application.blockTicker)
//                    {
//                        int a = 0;
//                    }
//                    if (LocalMarketData[curID].Last != SymMarketData[curID].Last) { Application.blockTicker = Ticker; cursor = curID; }
//                    if (LocalMarketData[curID].Bid != SymMarketData[curID].Bid) { Application.blockTicker = Ticker; cursor = curID; }
//                    if (LocalMarketData[curID].BidSize != SymMarketData[curID].BidSize) { Application.blockTicker = Ticker; cursor = curID; }
//                    if (LocalMarketData[curID].Ask != SymMarketData[curID].Ask) { Application.blockTicker = Ticker; cursor = curID; }
//                    if (LocalMarketData[curID].AskSize != SymMarketData[curID].AskSize) { Application.blockTicker = Ticker; cursor = curID; }
//                    if (LocalMarketData[curID].Open != SymMarketData[curID].Open) { Application.blockTicker = Ticker; cursor = curID; }
//                    if (LocalMarketData[curID].Close != SymMarketData[curID].Close) { Application.blockTicker = Ticker; cursor = curID; }
//                    if (LocalMarketData[curID].Low != SymMarketData[curID].Low) { Application.blockTicker = Ticker; cursor = curID; }
//                    if (LocalMarketData[curID].High != SymMarketData[curID].High) { Application.blockTicker = Ticker; cursor = curID; }
//                    if (LocalMarketData[curID].Volume != SymMarketData[curID].Volume) {  }

//                    if (LocalMarketDepth[curID].Bid1 != SymMarketDepth[curID].Bid1) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].Bid2 != SymMarketDepth[curID].Bid2) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].Bid3 != SymMarketDepth[curID].Bid3) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].Bid4 != SymMarketDepth[curID].Bid4) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].Bid5 != SymMarketDepth[curID].Bid5) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].BidSize1 != SymMarketDepth[curID].BidSize1) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].BidSize2 != SymMarketDepth[curID].BidSize2) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].BidSize3 != SymMarketDepth[curID].BidSize3) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].BidSize4 != SymMarketDepth[curID].BidSize4) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].BidSize5 != SymMarketDepth[curID].BidSize5) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].Ask1 != SymMarketDepth[curID].Ask1) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].Ask2 != SymMarketDepth[curID].Ask2) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].Ask3 != SymMarketDepth[curID].Ask3) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].Ask4 != SymMarketDepth[curID].Ask4) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].Ask5 != SymMarketDepth[curID].Ask5) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].AskSize1 != SymMarketDepth[curID].AskSize1) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].AskSize2 != SymMarketDepth[curID].AskSize2) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].AskSize3 != SymMarketDepth[curID].AskSize3) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].AskSize4 != SymMarketDepth[curID].AskSize4) { hasErrors = true; }
//                    if (LocalMarketDepth[curID].AskSize5 != SymMarketDepth[curID].AskSize5) { hasErrors = true; }
                    
//                }
//            }
            
//            LocalDepthMutex.ReleaseMutex();
//            LocalDataMutex.ReleaseMutex();
//            SymDepthMutex.ReleaseMutex();
//            SymDataMutex.ReleaseMutex();

//            return hasErrors;
//        }


//        public void Stop()
//        {
//            SymConn.Instance.Dispose();
//        }
//    }
//}
