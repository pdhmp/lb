using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Threading;
using System.IO;

using NestSymConn;
using NCommonTypes;
using NestDLL;
using NCustomControls;


namespace LiveTrade2
{
    class PhytonRunner
    {
        static System.Windows.Forms.Timer UpdateTimer = new System.Windows.Forms.Timer();

        public List<StgItem> SubscribedData = new List<StgItem>();
        public SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();
        BrokerList curBrokers = new BrokerList();

        //public StreamWriter LogStream;

        public string strMessage = "";

        public double PercOfDay = 0;

        public DateTime RunDate;

        private TimeSpan StartTime;
        private TimeSpan PreAuctionStartTime;
        private TimeSpan AuctionStartTime;

        FLEXConn curSymConn = new FLEXConn();

        public TimeSpan curUpdTime = new TimeSpan(0, 0, 0);

        bool useHistoricData;
        bool loadFinish = false;
        bool ReloadingOrders = false;

        public StringBuilder logMessages = new StringBuilder("");

        public BSEPlayer curPlayer;

        SortedDictionary<string, string> SentOrders = new SortedDictionary<string, string>();

        int simOrderCount = 1;
        //double PrevSeconds = 0;

        //Market data enqueueing objects
        private Queue<MarketUpdateList> EnqueuedMktData = new Queue<MarketUpdateList>();
        private object mktSync = new object();
        public bool StopProcessingQueue = false;
        Thread queueProcessingThread;

        bool InitialVolumesSet = false;
        bool PreAuctionStarted = false;
        bool AuctionStarted = false;

        public override string ToString()
        {
            double TotalPnL = 0;

            foreach (StgItem curItem in SubscribedData)
            {
                TotalPnL += curItem.TradedPnL;
            }

            if (this.RunDate > new DateTime(1900, 1, 1))
            {
                return this.RunDate.ToString("dd-MMM") + "\t " + this.PercOfDay.ToString("0.00%") + "\t" + TotalPnL.ToString("#,##0.");
            }
            else
            {
                return "0";//histFileName;
            }
        }

        public PhytonRunner(bool _useHistoricData, string _histFileName)
        {
            useHistoricData = _useHistoricData;
            //histFileName = _histFileName;

            UpdateTimer.Interval = 1000;
            UpdateTimer.Tick += new System.EventHandler(UpdateTimer_Tick);
            UpdateTimer.Enabled = true;
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            if (loadFinish)
            {
                if (SubscribedData.Count > 0)
                {
                    if (DateTime.Now.TimeOfDay > StartTime && !InitialVolumesSet && SubscribedData[0].Volume != 0)
                    {
                        foreach (StgItem curItem in SubscribedData)
                        {
                            curItem.InitialVolume = curItem.Volume;
                            curItem.InitialPrice = curItem.Last;
                            curItem.InitialVWAP = curItem.VWAP;
                            curItem.UpdateTradingPhase("OPENING POS");
                        }
                        InitialVolumesSet = true;
                    }

                    if (DateTime.Now.TimeOfDay > PreAuctionStartTime && InitialVolumesSet && !PreAuctionStarted)
                    {
                        foreach (StgItem curItem in SubscribedData)
                        {
                            curItem.UpdateTradingPhase("PREAUCTION");
                            curItem.Disable();
                        }
                        PreAuctionStarted = true;
                    }

                    if (DateTime.Now.TimeOfDay > AuctionStartTime && PreAuctionStarted)
                    {
                        foreach (StgItem curItem in SubscribedData)
                        {
                            curItem.UpdateTradingPhase("AUCTION");
                        }
                        AuctionStarted = true;
                    }

                    double TotalDaySeconds = PreAuctionStartTime.TotalSeconds - StartTime.TotalSeconds;
                    double TotalElapsedSeconds = DateTime.Now.TimeOfDay.TotalSeconds - StartTime.TotalSeconds;
                    double curPercOfDay = TotalElapsedSeconds / TotalDaySeconds;

                    if (curPercOfDay < 0) curPercOfDay = 0;

                    foreach (StgItem curItem in SubscribedData)
                    {
                        curItem.PCTTime = curPercOfDay;
                        curItem.TimeCheckOrders();
                    }
                }
            }
        }

        public void SaveInitialVolumes()
        {
            StreamWriter IniVolumesWriter = new StreamWriter(@"C:\LiveTrade\IniVolumes.txt");
            IniVolumesWriter.AutoFlush = true;

            foreach (StgItem curItem in SubscribedData)
            {
                IniVolumesWriter.WriteLine(curItem.Ticker + ";" + curItem.InitialVolume + ";" + curItem.InitialVWAP);
            }

            IniVolumesWriter.Close();
        }

        public void LoadInitialVolumes()
        {
            StreamReader IniVolumesReader = new StreamReader(@"C:\LiveTrade\IniVolumes.txt");
            string tempLine = "";

            while ((tempLine = IniVolumesReader.ReadLine()) != null)
            {
                string[] curRow = tempLine.Split(';');

                if (curRow[0] != "")
                {
                    int curPos = 0;
                    SubListIndex.TryGetValue(curRow[0], out curPos);

                    StgItem curItem = (StgItem)SubscribedData[curPos];

                    curItem.InitialVolume = double.Parse(curRow[1]);
                    curItem.InitialVWAP = double.Parse(curRow[2]);
                }
            }

            IniVolumesReader.Close();
        }

        public void Start()
        {
            queueProcessingThread = new Thread(new ThreadStart(ProcessMarketDataQueue));
            queueProcessingThread.Start();

            UpdateTimer.Interval = 1000;
            UpdateTimer.Tick += new System.EventHandler(UpdateTimer_Tick);
            UpdateTimer.Enabled = true;
            UpdateTimer.Start();

            FIXConnections.Instance.InitializeFIX();

            

            if (FIXConnections.Instance.curFixConn != null)
            {
                FIXConnections.Instance.curFixConn.ReceivedUpdate += new EventHandler(FIXUpdateReceived);
            }

            //LogStream = new StreamWriter(@"C:\LiveTrade\PhytonLog.txt", true);
            //LogStream.AutoFlush = true;

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                //StartTime = new TimeSpan(13, 14, 00);
                //PreAuctionStartTime = new TimeSpan(13, 59, 00);

                StartTime = new TimeSpan(17, 00, 00);
                PreAuctionStartTime = new TimeSpan(17, 54, 40);
                AuctionStartTime = new TimeSpan(18, 00, 00);
                RunDate = DateTime.Now.Date;

                string curFileName = @"C:\LiveTrade\Phyton2.csv";

                StreamReader sr = new StreamReader(curFileName);

                string tempLine = "";
                string SQLTickers = "";

                while ((tempLine = sr.ReadLine()) != null)
                {
                    string[] curRow = tempLine.Split(';');

                    if (curRow[0] != "")
                    {
                        SQLTickers += " UNION SELECT " + curRow[3] + "," + curRow[1] + "," + curRow[10].Replace("%", "").Replace(",", ".") + "/100," + curRow[9].Replace(".", "") + " ";
                    }
                }

                sr.Close();

                DateTime maxDate = DateTime.Parse(curConn.Execute_Query_String("SELECT MAX(Date_Ref) FROM NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=1073"));

                string SQLString = " SELECT CASE WHEN ExchangeTicker<>'' THEN ExchangeTicker ELSE NestTicker END as ExchangeTicker, Setor, C.IdCurrency, A.IdSecurity, C.IdInstrument, " +
                            " COALESCE(C.Strike, 0) AS Strike, COALESCE(C.Expiration, '1900-01-01') AS Expiration, TradeSide, IdPrimaryExchange, TgtPart, LimShares " +
                            " FROM (  " +
                            " 	SELECT 0 AS IdSecurity, 0 AS TradeSide, 0 AS TgtPart, 0 AS LimShares" +
                            SQLTickers +
                            " ) A " +
                            " LEFT JOIN NESTDB.dbo.FCN001_Securities_All('" + RunDate.ToString("yyyyMMdd") + "') C " +
                            " ON A.IdSecurity=C.IdSecurity   " +
                            " LEFT JOIN NESTDB.dbo.Tb000_Issuers D " +
                            " ON C.IdIssuer=D.IdIssuer   " +
                            " LEFT JOIN NESTDB.dbo.Tb113_Setores E " +
                            " ON D.IdNestSector=E.Id_Setor " +
                            " WHERE ReutersTicker ='XXXX'  " +
                            " ORDER BY A.IdSecurity DESC";


                DataTable dt = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {

                    StgItem curItem = CreateQuote(curRow["ExchangeTicker"].ToString(), curRow["Setor"].ToString(), curRow["IdCurrency"].ToString(), int.Parse(curRow["IdSecurity"].ToString()), int.Parse(curRow["IdInstrument"].ToString()), double.Parse(curRow["Strike"].ToString()), DateTime.Parse(curRow["Expiration"].ToString()), 0, int.Parse(curRow["IdPrimaryExchange"].ToString()));

                    curItem.VolumeParticipation = NestDLL.Utils.ParseToDouble(curRow["TgtPart"]);
                    curItem.TradeSide = (int)NestDLL.Utils.ParseToDouble(curRow["TradeSide"]);
                    curItem.ShareLimit = (int)NestDLL.Utils.ParseToDouble(curRow["LimShares"]);

                    curItem.OnNewOrder += new EventHandler(OnNewOrder);
                    curItem.OnReplaceOrder += new EventHandler(OnReplace);
                    curItem.OnCancelAll += new EventHandler(OnCancelAll);
                }
            }

            ReloadingOrders = true;
            if (FIXConnections.Instance.curFixConn != null) FIXConnections.Instance.curFixConn.resendAllStatus();
            ReloadingOrders = false;

            loadFinish = true;

            if (useHistoricData)
            {
                curPlayer.PauseOnDate = false;
            }
        }

        void OnNewOrder(object sender, EventArgs e)
        {
            NewOrderDef curOrder = (NewOrderDef)e;
            string thisOrderID = "";

            AddlogEntry("NEW ORDER:      " + thisOrderID + " " + curOrder.Ticker + " " + curOrder.Price);

            if (FIXConnections.Instance.curFixConn != null)
            {
                if (FIXConnections.Instance.curFixConn.IsAliveSession)
                {
                    //thisOrderID = FIXConnections.Instance.curFixConn.sendOrder(curOrder.Id_Ticker, curOrder.Shares, curOrder.Price, 18, 0, 0);
                }
            }
            else
            {
                thisOrderID = "SIM0000" + simOrderCount++;
                StgItem thisStgItem = (StgItem)sender;
                thisStgItem.RegisterBuy(curOrder.Shares, curOrder.Price);
            }

            SentOrders.Add(thisOrderID, curOrder.Ticker);

            int curPos = 0;
            SubListIndex.TryGetValue(curOrder.Ticker, out curPos);

            StgItem curStgItem = (StgItem)SubscribedData[curPos];

            curStgItem.tempOrders[curOrder.OrderType].OrderID = thisOrderID;

            curStgItem.curOrders.Add(thisOrderID, curStgItem.tempOrders[curOrder.OrderType]);
            curStgItem.tempOrders.Remove(curOrder.OrderType);

            if (curOrder.OrderType == "TOPBID") curStgItem.TopBuyId = thisOrderID;
            if (curOrder.OrderType == "TOPASK") curStgItem.TopSellId = thisOrderID;
            if (curOrder.OrderType == "CLOSEPOS") curStgItem.CloseOrderId = thisOrderID;
        }

        void OnReplace(object sender, EventArgs e)
        {
            ReplaceOrderDef curOrder = (ReplaceOrderDef)e;

            AddlogEntry("REPLACE REQUESTED: " + curOrder.ExternalID + " " + curOrder.newPrice);

            if (FIXConnections.Instance.curFixConn != null)
            {
                if (FIXConnections.Instance.curFixConn.IsAliveSession)
                {
                    FIXConnections.Instance.curFixConn.replaceOrder(curOrder.ExternalID, curOrder.newPrice, 0, 0);
                }
            }
            else
            {
                StgItem curStgItem = (StgItem)sender;
                curStgItem.curOrders[curOrder.ExternalID].Price = curOrder.newPrice;
                curStgItem.curOrders[curOrder.ExternalID].ReplaceRequested = false;
            }
        }

        void OnCancelAll(object sender, EventArgs e)
        {
            NewOrderDef curOrder = (NewOrderDef)e;

            AddlogEntry("CANCEL ALL REQUESTED: " + curOrder.Id_Ticker + " " + curOrder.Ticker);

            if (FIXConnections.Instance.curFixConn != null)
            {
                if (FIXConnections.Instance.curFixConn.IsAliveSession)
                {
                    FIXConnections.Instance.curFixConn.cancelAllOrders(curOrder.Id_Ticker);
                }
            }
        }

        public void SendAuctionOrders()
        {
            FIXConnections.Instance.curFixConn.curLimits.MaxOrderShares = 100000;
            FIXConnections.Instance.curFixConn.curLimits.MaxOrderAmount = 550000;

            foreach (StgItem curItem in SubscribedData)
            {
                curItem.ClosePosition();
            }
        }

        private void FIXUpdateReceived(object sender, EventArgs e)
        {
            NCommonTypes.FIXOrder curUpdateOrder = (NCommonTypes.FIXOrder)e;

            AddlogEntry("RECEIVE FIX REFRESH: " + curUpdateOrder.ClOrdID + "\t" + curUpdateOrder.Symbol + "\t" + curUpdateOrder.Price + "\t" + curUpdateOrder.strStatus);

            string curTicker;

            if (SentOrders.TryGetValue(curUpdateOrder.OrderID, out curTicker))
            {
                int curPos = 0;
                SubListIndex.TryGetValue(curTicker, out curPos);

                StgItem curStgItem = (StgItem)SubscribedData[curPos];

                double prevPrice = curStgItem.curOrders[curUpdateOrder.OrderID].Price;

                if (curUpdateOrder.Price == -1 & curUpdateOrder.OrderQty == -1)
                {
                    curStgItem.curOrders[curUpdateOrder.OrderID].ReplaceRequested = false;
                    return;
                }

                double LastShares = curUpdateOrder.Done - curStgItem.curOrders[curUpdateOrder.OrderID].Done;

                curStgItem.curOrders[curUpdateOrder.OrderID].Update(curUpdateOrder);

                if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "TOPBID" && curUpdateOrder.strStatus == "NEW") AddlogEntry(curUpdateOrder.ClOrdID + "\t" + "NEWTOPBID");
                if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "PICKUP" && curUpdateOrder.strStatus == "NEW") AddlogEntry(curUpdateOrder.ClOrdID + "\t" + "NEWPICKUP");
                if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "TOPASK" && curUpdateOrder.strStatus == "NEW") AddlogEntry(curUpdateOrder.ClOrdID + "\t" + "NEWTOPASK");

                //AddlogEntry("PRG FOUND ORDER:   " + curStgItem.curOrders[curUpdateOrder.OrigClOrdID].OrigClOrdID + "\t" + curStgItem.curOrders[curUpdateOrder.OrigClOrdID].Price + "\t" + curStgItem.curOrders[curUpdateOrder.OrigClOrdID].strStatus);

                if (curStgItem.curOrders[curUpdateOrder.OrderID].strStatus == "REPLACED" && curStgItem.curOrders[curUpdateOrder.OrderID].Price != prevPrice)
                {
                    curStgItem.curOrders[curUpdateOrder.OrderID].ReplaceRequested = false;
                }

                if (curStgItem.curOrders[curUpdateOrder.OrderID].strStatus == "CANCELED")
                {
                    curStgItem.curOrders[curUpdateOrder.OrderID].Cancelled = curStgItem.curOrders[curUpdateOrder.OrderID].OrderQty - curStgItem.curOrders[curUpdateOrder.OrderID].Done;
                    if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "TOPBID") curStgItem.TopBuyId = "";
                    if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "TOPASK") curStgItem.TopSellId = "";
                }

                if (curStgItem.curOrders[curUpdateOrder.OrderID].strStatus == "FILLED")
                {
                    if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "TOPBID") { curStgItem.TopBuyId = ""; curStgItem._TopCounter++; }
                    if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "TOPASK") { curStgItem.TopSellId = ""; curStgItem._TopCounter++; }
                    if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "PICKUP") { curStgItem._PickupCounter++; }
                    if (curUpdateOrder.strSide == "BUY") curStgItem.RegisterBuy(LastShares, curUpdateOrder.ExecPrice);
                    if (curUpdateOrder.strSide == "SELL") curStgItem.RegisterSell(LastShares, curUpdateOrder.ExecPrice);
                }

                if (curStgItem.curOrders[curUpdateOrder.OrderID].strStatus == "PARTIALLY_FILLED")
                {
                    if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "TOPBID") { curStgItem._TopCounter++; }
                    if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "TOPASK") { curStgItem._TopCounter++; }
                    if (curStgItem.curOrders[curUpdateOrder.OrderID].OrderType == "PICKUP") { curStgItem._PickupCounter++; }
                    if (curUpdateOrder.strSide == "BUY") curStgItem.RegisterBuy(LastShares, curUpdateOrder.ExecPrice);
                    if (curUpdateOrder.strSide == "SELL") curStgItem.RegisterSell(LastShares, curUpdateOrder.ExecPrice);
                }
            }
            else
            {
                if(!ReloadingOrders) AddlogEntry("ORDER NOT FOUND!   " + curUpdateOrder.ClOrdID);
            }

            if (ReloadingOrders)
            {
                int curPos = -1;

                if (SubListIndex.ContainsKey(curUpdateOrder.Symbol))
                {
                    SubListIndex.TryGetValue(curUpdateOrder.Symbol, out curPos);
                    if (curPos != -1)
                    {
                        StgItem curStgItem = (StgItem)SubscribedData[curPos];

                        if (curUpdateOrder.strSide == "BUY")
                            curStgItem.RegisterBuy(curUpdateOrder.Done, curUpdateOrder.ExecPrice);

                        if (curUpdateOrder.strSide == "SELL")
                            curStgItem.RegisterSell(curUpdateOrder.Done, curUpdateOrder.ExecPrice);
                    }
                }
            }
        }

        private StgItem CreateQuote(string Ticker, string Sector, string Currency, int IdTicker, int IdInstrument, double Strike, DateTime Expiration, double idxQuantity, int IdPrimaryExchange)
        {
            StgItem curItem = new StgItem();
            curItem.IdTicker = IdTicker;
            curItem.Ticker = Ticker;
            if (!(IdPrimaryExchange == 2))
                curItem.ExchangeTradingCode = Ticker ;
            else
                curItem.ExchangeTradingCode = Ticker ;
            curItem.IdInstrument = IdInstrument;
            curItem.Sector = Sector;
            curItem.Currency = Currency;
            curItem.Strike = Strike;
            curItem.Expiration = Expiration;

            curItem.MinLot = 100;

            SubscribedData.Add(curItem);

            Console.WriteLine(this.RunDate + "\tSubscribing:\t" + Ticker + "\t" + curItem.ExchangeTradingCode);

            UpdateListIndex();

            if (useHistoricData)
            {
                curPlayer.curCracker.SubscribedTickers.Add(curItem.ExchangeTradingCode, curItem.ExchangeTradingCode);
            }
            else
            {
                curSymConn.Subscribe(curItem.ExchangeTradingCode, Sources.Bovespa);
                curSymConn.SubscribeDepth(curItem.ExchangeTradingCode, Sources.Bovespa);
            }

            return curItem;
        }

        private void UpdateListIndex()
        {
            SubListIndex.Clear();

            for (int i = 0; i < SubscribedData.Count; i++)
            {
                SubListIndex.Add(SubscribedData[i].ExchangeTradingCode, i);
            }
        }

        private void NewMarketData(object sender, EventArgs origE)
        {
            while (!loadFinish) { Thread.Sleep(10); }

            MarketUpdateList curList = (MarketUpdateList)origE;

            MarketUpdateList curList2 = new MarketUpdateList ();

            foreach (MarketUpdateItem xlist in curList.ItemsList)

            {
                curList2.ItemsList.Add(xlist);
            }

            lock (mktSync)
            {
                EnqueuedMktData.Enqueue(curList2);
            }

            
        }

        private void NewDepthData(object sender, EventArgs curLevel2Item)
        {
            if (SubscribedData.Count != 0)
            {
                //if (sender.ToString().Contains("SymConn") && _curExchange == 2)
                //    return;

                Level2Item curItem = (Level2Item)curLevel2Item;

                int curPos = 0;
                SubListIndex.TryGetValue(curItem.Ticker, out curPos);

                StgItem curStgItem = (StgItem)SubscribedData[curPos];

                int curPosition = -1;

                if (curPos < 0)
                    return;

                for (int i = 0; i < curStgItem.MktDepth.Count; i++)
                {
                    if (curStgItem.MktDepth[i].Position == curItem.Position)
                    {
                        curPosition = i;
                    }
                }
                if (curPosition == -1)
                {
                    Level2Line curLine = new Level2Line();
                    curStgItem.MktDepth.Add(curLine);
                    curPosition = curStgItem.MktDepth.Count - 1;
                }

                curStgItem.MktDepth[curPosition].Position = curItem.Position;
                if (curItem.Side == 'b')
                {
                    curStgItem.MktDepth[curPosition].PriceBid = curItem.Price;
                    curStgItem.MktDepth[curPosition].QuantityBid = (int)curItem.Quantity;
                    curStgItem.MktDepth[curPosition].BrokerBid = curBrokers.BrokerTicker(int.Parse(curItem.Broker));
                }
                else if (curItem.Side == 'a')
                {
                    curStgItem.MktDepth[curPosition].PriceAsk = curItem.Price;
                    curStgItem.MktDepth[curPosition].QuantityAsk = (int)curItem.Quantity;
                    curStgItem.MktDepth[curPosition].BrokerAsk = curBrokers.BrokerTicker(int.Parse(curItem.Broker));
                }

                try
                {
                    double prevBid = 0;
                    int counterBid = 0;
                    double prevAsk = 0;
                    int counterAsk = 0;


                    for (int i = 0; i < curStgItem.MktDepth.Count; i++)
                    {
                        if (curStgItem.MktDepth[i].PriceBid != prevBid)
                        {
                            counterBid++;
                            prevBid = curStgItem.MktDepth[i].PriceBid;
                        }
                        curStgItem.MktDepth[i].LevelBid = counterBid;

                        if (curStgItem.MktDepth[i].PriceAsk != prevAsk)
                        {
                            counterAsk++;
                            prevAsk = curStgItem.MktDepth[i].PriceAsk;
                        }
                        curStgItem.MktDepth[i].LevelAsk = counterAsk;
                    }
                }
                catch (Exception e)
                {
                    int a = 0;
                }
            }
        }

        private void ProcessMarketDataQueue()
        {
            while (!StopProcessingQueue)
            {
                MarketUpdateList curList = new MarketUpdateList();
                bool nothingToProcess = false;
                lock (mktSync)
                {
                    if (EnqueuedMktData.Count > 0)
                    {
                        curList = EnqueuedMktData.Dequeue();
                    }
                    else
                    {
                        nothingToProcess = true;
                    }
                }

                if (nothingToProcess)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    ProcessMarketUpdateList(curList);
                }
            }            
        }

        private void ProcessMarketUpdateList(MarketUpdateList curList)
        {            
            foreach (MarketUpdateItem curUpdate in curList.ItemsList)
            {
                int i = 0;

                //if (curUpdate.FLID == NestFLIDS.AucVolume && curUpdate.Ticker == "LREN3" && curUpdate.ValueDouble > 13000)
                //{ }

                if (curUpdate.FLID == NestFLIDS.UpdateTime)
                {
                    DateTime tempdate = NestDLL.Utils.UnixTimestampToDateTime((int)curUpdate.ValueDouble);
                    TimeSpan tempUpdTime = NestDLL.Utils.UnixTimestampToDateTime((int)curUpdate.ValueDouble).TimeOfDay;
                    if (tempUpdTime > curUpdTime) curUpdTime = tempUpdTime;
                    //UpdateTradingPhase();
                    //UpdatePODay();
                }

                if (SubListIndex.TryGetValue(curUpdate.Ticker, out i))
                {
                    if (curUpdate.FLID == NestFLIDS.UpdateTime && curUpdate.ValueDouble == 0)
                    { }
                    else
                    {
                        StgItem curItem = (StgItem)SubscribedData[i];
                        curItem.Update(curUpdate);

                        //curItem.UpdateQuantity();
                        

                        //if (curItem.TradingPhase == "CLOSING POS" && (curUpdate.FLID == NestFLIDS.Last || curUpdate.FLID == NestFLIDS.LastSize))
                        //{
                        //    curItem.UpdateVWAP(curUpdate);
                        //}
                        //else if (curItem.TradingPhase == "FINISHED")
                        //{
                        //    curItem.Last = curItem.VWAPPrice;
                        //    curItem.Bid = curItem.VWAPPrice;
                        //    curItem.Ask = curItem.VWAPPrice;                            
                        //}
                    }
                }
            }
        }

        public void AddlogEntry(string strMessage)
        {
            logMessages.AppendLine(strMessage);
            //Console.WriteLine(DateTime.Now.ToString("hh:mm:ss") + ": " + strMessage);
            //LogStream.WriteLine(DateTime.Now.ToString("hh:mm:ss") + ": " + strMessage);
        }
    }
}
