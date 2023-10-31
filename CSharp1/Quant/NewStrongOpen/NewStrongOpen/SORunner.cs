using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;

using NestSymConn;
using NestDLL;
using NCommonTypes;


namespace NewStrongOpen
{
    /// <summary>
    /// Strong Open strategy runner.
    /// It runs the strategy for a specific date (today or any historic date)
    /// </summary>
    class SORunner
    {
        #region Runner Parameters Region

        #region Variable Parameters

        private bool useHistoricData;
        private string histFileName;
        private double hdgIdSecurity;
        private string hdgTicker;
        private int hdgRoundLot;

        #endregion

        #region Fixed Parameters

        /// <summary>
        /// Minimum difference between the stock´s and the index´s performance in the previous day that triggers the strategy
        /// </summary>
        double PrevChangeTrigger = -0.02;

        #endregion

        #endregion

        #region Attributes Region

        private DateTime RunDate;
        private BSEPlayer curPlayer;

        private TimeSpan OpenTime;
        private TimeSpan TriggerTime;
        private TimeSpan CloseTime;

        private bool loadFinish = false;

        public List<StgItem> SubscribedData = new List<StgItem>();
        private SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();

        //Market data enqueueing objects
        private Queue<MarketUpdateList> EnqueuedMktData = new Queue<MarketUpdateList>();
        private object mktSync = new object();
        public bool StopProcessingQueue = false;
        Thread queueProcessingThread; 

        private int HdgPosition = -1;

        private TimeSpan curUpdTime;
        private string curTradingPhase = "";
        private double PercOfDay;

        #endregion

        #region Initializing Methods Region

        /// <summary>
        /// Strong Open constructor. Initialize runner´s parameters
        /// </summary>
        /// <param name="_useHistoricData">Indicates if the execution is for a historical date or for today</param>
        /// <param name="_histFileName">If _useHistoricData is true, must be filled with the full path for de historical ProxyDiff file</param>
        /// <param name="_hdgIdSecurity">IdSecurity for the hedge</param>
        /// <param name="_hdgTicker">Ticker for the hedge</param>
        /// <param name="_hdgRoundLot">Round lot for the hedge</param>
        public SORunner(bool _useHistoricData, string _histFileName, double _hdgIdSecurity, string _hdgTicker, int _hdgRoundLot)
        {
            useHistoricData = _useHistoricData;
            
            if (_useHistoricData == true && (_histFileName == "" || _histFileName == null))
            {
                throw new ArgumentException("For historical run, the historical ProxyDiff file full path must be specified", "_histFileName");
            }
            else
            {
                histFileName = _histFileName;
            }

            hdgIdSecurity = _hdgIdSecurity;
            hdgTicker = _hdgTicker;
            hdgRoundLot = _hdgRoundLot;
        }

        /// <summary>
        /// Starts the Strong Open strategy
        /// </summary>
        public void Start()
        {
            //Initialize connection with data provider
            if (useHistoricData)
            {
                curPlayer = new BSEPlayer(histFileName);
                curPlayer.OnData += new EventHandler(NewMarketData);
            }
            else
            {
                SymConn.Instance.BovespaSource = NEnuns.NSYMSources.NESTBSE;
                SymConn.Instance.OnData += new EventHandler(NewMarketData);
                SymConn.Instance.ConnectSym();
            }

            //Get the run date and initalize phase time
            RunDate = GetRunDate();
            InitializePhaseTime();

            using (newNestConn curConn = new newNestConn())
            {
                //Get Index change in the day before the run date
                double IndexPrevChange = curConn.Return_Double("SELECT SrValue FROM NESTDB.dbo.Tb053_Precos_Indices WHERE IdSecurity=1073 AND SrType=100 AND Source=1 AND srDate=(SELECT NESTDB.dbo.FCN_NDATEADD('du',-1,CONVERT(varchar,'" + RunDate.ToString("yyyy-MM-dd") + "',112),99,1073))");
                
                //Get closing price for the hedge in the day before the run date
                double hdgPrevClose = curConn.Return_Double("SELECT Return_Value FROM NESTDB.dbo.FCN_GET_PRICE(" + hdgIdSecurity + ", NESTDB.dbo.FCN_NDATEADD('du',-1,CONVERT(varchar,'" + RunDate.ToString("yyyy-MM-dd") + "',112),99,1073), 1, 0, 0, 0, 0, 0)");

                //Get the securities to process
                string SQLString = " SELECT CASE WHEN ExchangeTicker<>'' THEN ExchangeTicker ELSE NestTicker END as ExchangeTicker, Setor, C.IdCurrency, A.IdSecurity, C.IdInstrument, " +
                            " COALESCE(C.Strike, 0), COALESCE(C.Expiration, '1900-01-01'), IdxWeight, F.SrValue, IdPrimaryExchange " +
                            " FROM (  " +
                            " 	SELECT Id_Ticker_Component AS IdSecurity, Quantity AS IdxWeight  " +
                            " 	FROM NESTDB.dbo.Tb023_Securities_Composition " +
                            " 	WHERE Id_Ticker_Composite=1073 AND Date_Ref='" + RunDate.ToString("yyyy-MM-dd") + "' " +
                            " 	UNION  " +
                            " 	SELECT 1073, 0 " +
                            " 	UNION  " +
                            " 	SELECT " + hdgIdSecurity + ", 0 " +
                            " ) A " +
                            " LEFT JOIN NESTDB.dbo.Tb001_Securities C " +
                            " ON A.IdSecurity=C.IdSecurity   " +
                            " LEFT JOIN NESTDB.dbo.Tb000_Issuers D " +
                            " ON C.IdIssuer=D.IdIssuer   " +
                            " LEFT JOIN NESTDB.dbo.Tb113_Setores E " +
                            " ON D.IdNestSector=E.Id_Setor " +
                            " LEFT JOIN (SELECT * FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE SrType=100 AND Source=1 AND SrDate=(SELECT MAX(SrDate) FROM NESTDB.dbo.Tb053_Precos_Indices WHERE IdSecurity=1073 AND SrDate<'" + RunDate.ToString("yyyy-MM-dd") + "')) AS F " +
                            " ON A.IdSecurity=F.IdSecurity " +
                            " WHERE ReutersTicker IS NOT NULL AND ReutersTicker<>''  " +
                            " ORDER BY A.IdSecurity DESC";

                DataTable dt = curConn.Return_DataTable(SQLString);

                //Run through the securities to calculate strategy fitness
                foreach (DataRow curRow in dt.Rows)
                {
                    if ((Utils.ParseToDouble(curRow["SrValue"]) - IndexPrevChange) < PrevChangeTrigger ||
                        int.Parse(curRow[4].ToString()) == hdgIdSecurity)
                    {
                        //Fill the StgItem
                        StgItem curItem = CreateQuote(curRow[0].ToString(), curRow[1].ToString(), curRow[2].ToString(), int.Parse(curRow[3].ToString()), int.Parse(curRow[4].ToString()), double.Parse(curRow[5].ToString()), DateTime.Parse(curRow[6].ToString()), double.Parse(curRow[7].ToString()), int.Parse(curRow[9].ToString()));
                        curItem.PrevChangeTrigger = PrevChangeTrigger;
                        curItem.PrevChange = NestDLL.Utils.ParseToDouble(curRow["SrValue"]);
                        curItem.PrevChangeIndex = IndexPrevChange;
                        curItem.HdgPrevClose = hdgPrevClose;

                        curItem.OnTrade += new EventHandler(OnTrade);
                    }                       
                } 
            }

            UpdateListIndex();

            SubscribeAll();
            if (useHistoricData)
            {                
                curPlayer.Play();
            }

            loadFinish = true;

            queueProcessingThread = new Thread(new ThreadStart(ProcessMarketDataQueue));
            queueProcessingThread.Start();

        }
                      

        /// <summary>
        /// Gets the run date.        
        /// </summary>
        /// <returns>
        /// If running a historical date, returns the date from BSEPlayer.
        /// Otherwise, returns today´s date.
        /// </returns>
        private DateTime GetRunDate()
        {
            DateTime auxRunDate = DateTime.MinValue;

            if (!useHistoricData)
            {
                auxRunDate = DateTime.Today.Date;
            }
            else
            {
                auxRunDate = curPlayer.GetMarketDate();
            }

            return auxRunDate;
        }

        /// <summary>
        /// Initalizes the trading phase boundaries
        /// </summary>
        private void InitializePhaseTime()
        {
            int tradingTimeType = 0;

            //Check the trading time type of RunDate
            if (RunDate > new DateTime(2010, 10, 17) && RunDate < new DateTime(2011,03,14))
            {
                tradingTimeType = 1;
            }

            switch (tradingTimeType)
            {
                case 0:
                    OpenTime = new TimeSpan(09, 45, 00);
                    TriggerTime = new TimeSpan(10, 30, 00);
                    CloseTime = new TimeSpan(14, 00, 00);
                    break;
                case 1:
                    OpenTime = new TimeSpan(10, 45, 00);
                    TriggerTime = new TimeSpan(11, 30, 00);
                    CloseTime = new TimeSpan(15, 00, 00);
                    break;
                default:
                    OpenTime = new TimeSpan(09, 45, 00);
                    TriggerTime = new TimeSpan(10, 30, 00);
                    CloseTime = new TimeSpan(14, 00, 00);
                    break;
            }

        }

        /// <summary>
        /// Creates a StgItem
        /// </summary>
        /// <param name="Ticker">Ticker</param>
        /// <param name="Sector">Sector</param>
        /// <param name="Currency">Currency</param>
        /// <param name="IdTicker">Nest ID Ticker</param>
        /// <param name="IdInstrument">Nest Id Instrument</param>
        /// <param name="Strike">Strike price</param>
        /// <param name="Expiration">Expiration date</param>
        /// <param name="idxQuantity">Index Quantity</param>
        /// <param name="IdPrimaryExchange">Id Primary Exchange</param>
        /// <returns></returns>
        private StgItem CreateQuote(string Ticker, string Sector, string Currency, int IdTicker, int IdInstrument, double Strike, DateTime Expiration, double idxQuantity, int IdPrimaryExchange)
        {
            StgItem curItem = new StgItem();

            curItem.IdTicker = IdTicker;
            curItem.Ticker = Ticker;
            curItem.ExchangeTradingCode = Ticker;
            curItem.IdInstrument = IdInstrument;
            curItem.Sector = Sector;
            curItem.Strike = Strike;
            curItem.Expiration = Expiration;
            curItem.MinLot = GetMinLot(curItem.IdTicker);
            curItem.IdPrimaryExchange = IdPrimaryExchange;

            SubscribedData.Add(curItem);

            return curItem;
        }

        /// <summary>
        /// Returns the minimum trading lot for the security
        /// </summary>
        /// <param name="IdTicker">NestID of the security</param>
        /// <returns>Minimum trading lot</returns>
        private int GetMinLot(int IdTicker)
        {
            int MinLot;

            if (IdTicker == hdgIdSecurity)
            {
                MinLot = 5;
            }
            else
            {
                MinLot = 100;
            }

            return MinLot;
        }

        /// <summary>
        /// Subscribe all tickers to the data provider
        /// </summary>
        private void SubscribeAll()
        {
            foreach (StgItem curItem in SubscribedData)
            {
                if (useHistoricData)
                {
                    curPlayer.Subscribe(curItem.Ticker);
                }
                else
                {
                    SymConn.Instance.Subscribe(curItem.ExchangeTradingCode, curItem.IdPrimaryExchange);
                }
            }
        }

        /// <summary>
        /// Update subscribed tickers index
        /// </summary>
        private void UpdateListIndex()
        {
            SubListIndex.Clear();

            for (int i = 0; i < SubscribedData.Count; i++)
            {
                SubListIndex.Add(SubscribedData[i].ExchangeTradingCode, i);
                if (SubscribedData[i].IdTicker == hdgIdSecurity) HdgPosition = i;
            }
        }

        #endregion

        #region Data Processing Region

        /// <summary>
        /// Receives data from data provider
        /// </summary>
        /// <param name="sender">Data provider object</param>
        /// <param name="origE">Market data update element.</param>
        private void NewMarketData(object sender, EventArgs origE)
        {
            MarketUpdateList curUpdate = (MarketUpdateList)origE;

            lock (mktSync)
            {
                EnqueuedMktData.Enqueue(curUpdate);
            }
        }

        /// <summary>
        /// This function process the market data queue.
        /// It runs indefinitely in a separate thread until requested to stop
        /// </summary>
        private void ProcessMarketDataQueue()
        {
            while (!loadFinish) { Thread.Sleep(10); }

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
                    Thread.Sleep(10);
                }
                else
                {
                    ProcessMarketUpdateList(curList);
                }
            }
        }

        /// <summary>
        /// This function process the market data update list.
        /// It is called by ProcesMarketDataQueue function.
        /// </summary>
        /// <param name="curList">Market data update item to process</param>
        private void ProcessMarketUpdateList(MarketUpdateList curList)
        {
            foreach (MarketUpdateItem curUpdate in curList.ItemsList)
            {
                //Update Trading Phase and Percentage of Day
                if (curUpdate.FLID == NestFLIDS.UpdateTime)
                {
                    DateTime tempdate = NestDLL.Utils.UnixTimestampToDateTime((int)curUpdate.ValueDouble);
                    TimeSpan tempUpdTime = NestDLL.Utils.UnixTimestampToDateTime((int)curUpdate.ValueDouble).TimeOfDay;
                    if (tempUpdTime > curUpdTime) curUpdTime = tempUpdTime;
                    UpdateTradingPhase();
                    UpdatePODay();
                }

                //Update Strategy Item
                int curId = 0;
                if (SubListIndex.TryGetValue(curUpdate.Ticker, out curId))
                {
                    StgItem curItem = SubscribedData[curId];
                    curItem.Update(curUpdate);

                    if (curItem.IdTicker != hdgIdSecurity)
                    {
                        if (curUpdate.FLID == NestFLIDS.Last    || curUpdate.FLID == NestFLIDS.Volume ||
                            curUpdate.FLID == NestFLIDS.AucLast || curUpdate.FLID == NestFLIDS.AucVolume)
                        {

                        }
                    }
                    else
                    {
                    }
                }                
            }            
        }

        /// <summary>
        /// Handles the trade event thrown by the StgItem
        /// </summary>
        /// <param name="sender">StgItem that threw the event</param>
        /// <param name="e">Event arguments</param>
        void OnTrade(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UpdateTradingPhase()
        {
            string tempTradingPhase = "";

            if (curUpdTime < OpenTime)
            {
                tempTradingPhase = "PRE-OPEN";
            }
            else if (curUpdTime < TriggerTime)
            {
                tempTradingPhase = "OPENING POS";
            }
            else if
                (curUpdTime < CloseTime)
            {
                tempTradingPhase = "CLOSING POS";
            }
            else
            {
                tempTradingPhase = "FINISHED";
            }

            if (tempTradingPhase != curTradingPhase)
            {
                curTradingPhase = tempTradingPhase;

                foreach (StgItem futUpdItem in SubscribedData)
                {
                    futUpdItem.TradingPhase = tempTradingPhase;
                }
            }
        }

        private void UpdatePODay()
        {
            double TotalDaySeconds = CloseTime.TotalSeconds - TriggerTime.TotalSeconds;
            double TotalElapsedSeconds = curUpdTime.TotalSeconds - TriggerTime.TotalSeconds;
            double curPercOfDay = TotalElapsedSeconds / TotalDaySeconds;

            foreach (StgItem futUpdItem in SubscribedData)
            {
                futUpdItem.PercOfDay = curPercOfDay;
            }

            PercOfDay = curPercOfDay;

            if (PercOfDay > 1.05 && curPlayer != null)
            {
                curPlayer.Stop();
            }
        }

        #endregion
    }
}
