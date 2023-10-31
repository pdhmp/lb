using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

using NFastData;
using NCommonTypes;
using NestSymConn;
using NestDLL;


namespace JMStrategy
{
    public class JMData
    {
        #region Singleton Region

        private static volatile object SyncRoot = new object();

        private static JMData _Instance;
        public static JMData Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new JMData();
                        }
                    }
                }

                return _Instance;
            }
        }

        private JMData() { }

        #endregion

        #region Parameters Region

        private DateTime _IniDate;
        public DateTime IniDate
        {
            get { return _IniDate; }
            set { _IniDate = value; IniDateInitialized = true; }
        }

        private bool _IsRealTime = true;
        public bool IsRealTime
        {
            get { return _IsRealTime; }
            set { _IsRealTime = value; }
        }


        private bool IniDateInitialized = false;
        private bool isInitialized = false;


        #endregion

        #region SymData Region

        List<MarketDataItem> MarketDataList = new List<MarketDataItem>();
        SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();
        List<string> SubscribedTickers = new List<string>();

        private void InitializeSym()
        {
            if (IsRealTime)
            {
                SymConn.Instance.BovespaSource = NEnuns.NSYMSources.FLEXBSE;
                SymConn.Instance.OnData += new EventHandler(ReceiveMarketData);
            }
        }

        public void SubscribeTickers()
        {
            if (IsRealTime)
            {
                foreach(string ticker in SubscribedTickers)
                {                    
                    SymConn.Instance.Subscribe(ticker, 2);
                    AddTickerToList(ticker, (int)TickerToIdSecurity[ticker]);
                }
            }
        }
        public void ReSubscribeTickers()
        {
            if (IsRealTime)
            {
                foreach (string ticker in SubscribedTickers)
                {
                    SymConn.Instance.Subscribe(ticker, 2);
                    //AddTickerToList(ticker, (int)TickerToIdSecurity[ticker]);
                }
            }
        }

        private void AddTickerToList(string Ticker, int IdSecurity)
        {
            if (!SubListIndex.ContainsKey(Ticker))
            {
                MarketDataItem curItem = new MarketDataItem();
                curItem.IdTicker = IdSecurity;
                curItem.Ticker = Ticker;

                MarketDataList.Add(curItem);

                SubListIndex.Clear();

                for (int i = 0; i < MarketDataList.Count; i++)
                {
                    SubListIndex.Add(MarketDataList[i].Ticker, i);
                }
            }
        }

        private void ReceiveMarketData(object sender, EventArgs e)
        {
            MarketUpdateList curList = (MarketUpdateList)e;

            foreach (MarketUpdateItem curItem in curList.ItemsList)
            {
                int curIndex = 0;

                if (SubListIndex.TryGetValue(curItem.Ticker, out curIndex))
                {
                    MarketDataList[curIndex].Update(curItem);
                }
            }
        }

        #endregion

        #region FastData Region

        public FastData High;
        public FastData Low;
        public FastData Open;
        public FastData LastPrice;
        public FastData TRIndex;
        public FastData TRDay;

        public void InitializeObjects()
        {
            if (!isInitialized)
            {
                if (IniDateInitialized)
                {
                    High = new FastData(4, _IniDate, DateTime.Now, 1);
                    Low = new FastData(3, _IniDate, DateTime.Now, 1);
                    Open = new FastData(8, _IniDate, DateTime.Now, 1);
                    LastPrice = new FastData(1, _IniDate, DateTime.Now, 1);                    
                    TRIndex = new FastData(101, _IniDate, DateTime.Now, 1);
                    TRDay = new FastData(100, _IniDate, DateTime.Now, 1);

                    InitializeSym();

                    isInitialized = true;

                    LoadTickers(1073);
                }
            }
        }

        public void LoadTickers(int IdSecurity)
        {
            double[] SecurityList = new double[1];

            SecurityList[0] = IdSecurity;

            LoadTickers(SecurityList);
        }

        public void LoadTickers(double[] SecurityList)
        {
            High.LoadTickers(SecurityList);
            Low.LoadTickers(SecurityList);
            Open.LoadTickers(SecurityList);
            LastPrice.LoadTickers(SecurityList);
            TRIndex.LoadTickers(SecurityList);
            TRDay.LoadTickers(SecurityList);

            for (int i = 0; i < SecurityList.Length; i++)
            {
                string ticker = GetSymbol(SecurityList[i]);
                if (!SubscribedTickers.Contains(ticker))
                {
                    SubscribedTickers.Add(ticker);
                }
            }            
        }

        public double[] GetPriceRange(int IdSecurity, int PriceType, DateTime EndDate, int Days)
        {
            double dIdSecurity = (double)IdSecurity;

            double[] retArr = new double[Days];
            double[] auxArr = new double[Days];
            double[] lastArr = new double[Days];
            double[] adjLastArr = new double[Days];
            double[] trArr = new double[Days];

            DateTime[] IbovDates = new DateTime[Days];            
            DateTime auxDate = EndDate.AddDays(1);
            int curCounter = 0;

            if (IsRealTime)
            {
                if (EndDate == DateTime.Today)
                {
                    IbovDates[0] = EndDate;
                    auxArr[0] = GetRTData(dIdSecurity, PriceType);
                    lastArr[0] = GetRTData(dIdSecurity, NEnuns.NPriceType.LastPrice);
                    adjLastArr[0] = GetRTData(dIdSecurity, NEnuns.NPriceType.LastPrice);
                    trArr[0] = GetRTData(dIdSecurity, NEnuns.NPriceType.TRIndex);
                    curCounter = 1;                    
                }
            }

            for (int i = curCounter; i < Days; i++)
            {
                IbovDates[i] = DateTime.FromOADate(TRIndex.GetValue(1073, auxDate.AddDays(-1), false)[0]);
                auxDate = IbovDates[i];
            }

            FastData curData = GetPriceObject(PriceType);            

            for (int i = curCounter; i < Days; i++)
            {
                auxArr[i] = curData.GetValue(dIdSecurity, IbovDates[i], false)[1];
                lastArr[i] = LastPrice.GetValue(dIdSecurity, IbovDates[i], false)[1];
                adjLastArr[i] = lastArr[i];
                trArr[i] = TRIndex.GetValue(dIdSecurity, IbovDates[i], false)[1];
            }

            for (int i = 0; i < Days; i++)
            {
                adjLastArr[i] = (lastArr[0] / trArr[0]) * trArr[i];
                retArr[i] = (adjLastArr[i] / lastArr[i]) * auxArr[i];
            }

            return retArr;
        }

        public bool CheckAllStarted()
        {
            bool allStarted = true;

            foreach (MarketDataItem curItem in MarketDataList)
            {
                if (curItem.Last == 0 && curItem.IdTicker != 1073 && curItem.IdTicker != 5050) { allStarted = false; ReSubscribeTickers(); break; }
                if (curItem.Open == 0 && curItem.IdTicker != 1073 && curItem.IdTicker != 5050) { allStarted = false; ReSubscribeTickers(); break; }
                if (curItem.Low == 0 && curItem.IdTicker != 1073 && curItem.IdTicker != 5050) { allStarted = false; ReSubscribeTickers(); break; }
                if (curItem.High == 0 && curItem.IdTicker != 1073 && curItem.IdTicker != 5050) { allStarted = false; ReSubscribeTickers(); break; }
                if (curItem.Close == 0 && curItem.IdTicker != 1073 && curItem.IdTicker != 5050) { allStarted = false; ReSubscribeTickers(); break; }                
            }

            return allStarted;
        }

        #endregion

        #region Utils Region

        SortedDictionary<string, double> TickerToIdSecurity = new SortedDictionary<string, double>();
        SortedDictionary<double, string> IdSecurityToTicker = new SortedDictionary<double, string>();

        private string GetSymbol(double IdSecurity)
        {
            string symbol = "";

            if (!IdSecurityToTicker.TryGetValue(IdSecurity, out symbol))
            {
                using (newNestConn curConn = new newNestConn(true))
                {
                    symbol = curConn.Execute_Query_String("SELECT EXCHANGETICKER FROM NESTSRV06.NESTDB.DBO.TB001_SECURITIES WHERE IDSECURITY = " + ((int)IdSecurity).ToString());
                    IdSecurityToTicker.Add(IdSecurity, symbol);
                    TickerToIdSecurity.Add(symbol, IdSecurity);
                }
            }

            return symbol;
        }

        private double GetIdSecurity(string Ticker)
        {
            double IdSecurity = 0;

            if (!TickerToIdSecurity.TryGetValue(Ticker, out IdSecurity))
            {
                throw new NotImplementedException();
            }

            return IdSecurity;
        }

        private FastData GetPriceObject(int PriceType)
        {
            if (PriceType == NEnuns.NPriceType.LastPrice)
            {
                return LastPrice;
            }
            else if (PriceType == NEnuns.NPriceType.Low)
            {
                return Low;
            }
            else if (PriceType == NEnuns.NPriceType.High)
            {
                return High;
            }
            else if (PriceType == NEnuns.NPriceType.Open)
            {
                return Open;
            }
            else if (PriceType == NEnuns.NPriceType.TRIndex)
            {
                return TRIndex;
            }
            else
            {
                throw new Exception("Invalid Price Type");
            }
        }

        private double GetRTData(double IdSecurity, int PriceType)
        {
            int curIndex = -1;

            if (SubListIndex.TryGetValue(GetSymbol(IdSecurity), out curIndex))
            {
                if (PriceType == NEnuns.NPriceType.LastPrice)
                {
                    while (MarketDataList[curIndex].Last == 0)
                    {
                        System.Threading.Thread.Sleep(200);
                    }

                    return MarketDataList[curIndex].Last;
                }
                else if (PriceType == NEnuns.NPriceType.Low)
                {
                    while (MarketDataList[curIndex].Low == 0)
                    {
                        System.Threading.Thread.Sleep(200);
                    }

                    return MarketDataList[curIndex].Low;
                }
                else if (PriceType == NEnuns.NPriceType.High)
                {
                    while (MarketDataList[curIndex].High == 0)
                    {
                        System.Threading.Thread.Sleep(200);
                    }

                    return MarketDataList[curIndex].High;
                }
                else if (PriceType == NEnuns.NPriceType.Open)
                {
                    while (MarketDataList[curIndex].Open == 0)
                    {
                        System.Threading.Thread.Sleep(200);
                    }

                    return MarketDataList[curIndex].Open;
                }
                else if (PriceType == NEnuns.NPriceType.TRIndex)
                {
                    while (MarketDataList[curIndex].Close == 0 || MarketDataList[curIndex].Last == 0)
                    {
                        System.Threading.Thread.Sleep(200);
                    }
                    return TRIndex.GetValue(IdSecurity, DateTime.Today, false)[1] * (1 + MarketDataList[curIndex].Change);
                }
                else
                {
                    throw new Exception("Invalid Price Type");
                }
            }
            else
            {
                throw new Exception("Symbol not found");
            }
        }

        public volatile object MainSync = new object();

        #endregion

        #region BT Data Region

        public SortedDictionary<int, SortedDictionary<int, int>> Sec1BTPos = new SortedDictionary<int, SortedDictionary<int, int>>();
        public SortedDictionary<int, SortedDictionary<int, int>> Sec2BTPos = new SortedDictionary<int, SortedDictionary<int, int>>();

        public void LoadBTPosition(DateTime refDate)
        {
            Sec1BTPos.Clear();
            Sec2BTPos.Clear();

            try
            {
                StreamReader sr = new StreamReader(@"C:\QUANT\STRATEGIES\JM\JM_BT_POSITION_" + refDate.ToString("yyyyMMdd") + ".txt");

                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split((char)16);

                    int idModel = int.Parse(line[0]);
                    int idPair = int.Parse(line[1]);
                    int sec1Pos = int.Parse(line[2]);
                    int sec2Pos = int.Parse(line[3]);

                    if (!Sec1BTPos.ContainsKey(idModel))
                    {
                        SortedDictionary<int, int> newSD1 = new SortedDictionary<int, int>();
                        Sec1BTPos.Add(idModel, newSD1);

                        SortedDictionary<int, int> newSD2 = new SortedDictionary<int, int>();
                        Sec2BTPos.Add(idModel, newSD2);
                    }

                    if (!Sec1BTPos[idModel].ContainsKey(idPair))
                    {
                        Sec1BTPos[idModel].Add(idPair, sec1Pos);
                        Sec2BTPos[idModel].Add(idPair, sec2Pos);
                    }
                    else
                    {
                        Sec1BTPos[idModel][idPair] = sec1Pos;
                        Sec2BTPos[idModel][idPair] = sec2Pos;
                    }
                }
            }
            catch { }

            
            /*
            string SQLExpression = " SELECT * FROM NESTSIM.dbo.TB703_JM_BT_POSITIONS " +
                                   " WHERE DATE = (SELECT MAX(DATE) FROM NESTSIM.dbo.TB703_JM_BT_POSITIONS " +
                                                   " WHERE DATE < '" + refDate.ToString("yyyyMMdd") + "') "+
                                   " ORDER BY IDMODEL, IDPAIR ";

            DataTable dt = new DataTable();

            using (newNestConn curConn = new newNestConn(true))
            {
                dt = curConn.Return_DataTable(SQLExpression);
            }

            foreach (DataRow curRow in dt.Rows)
            {
                int idModel = int.Parse(curRow["IDMODEL"].ToString());
                int idPair = int.Parse(curRow["IDPAIR"].ToString());
                int sec1Pos = int.Parse(curRow["SEC1POSITION"].ToString());
                int sec2Pos = int.Parse(curRow["SEC2POSITION"].ToString());

                if (!Sec1BTPos.ContainsKey(idModel))
                {
                    SortedDictionary<int, int> newSD1 = new SortedDictionary<int, int>();
                    Sec1BTPos.Add(idModel, newSD1);

                    SortedDictionary<int, int> newSD2 = new SortedDictionary<int, int>();
                    Sec1BTPos.Add(idModel, newSD2);
                }

                if (!Sec1BTPos[idModel].ContainsKey(idPair))
                {
                    Sec1BTPos[idModel].Add(idPair, sec1Pos);
                    Sec2BTPos[idModel].Add(idPair, sec2Pos);
                }
                else
                {
                    Sec1BTPos[idModel][idPair] = sec1Pos;
                    Sec2BTPos[idModel][idPair] = sec2Pos;
                }
             
            }
           */
        }

        SortedDictionary<int, List<PairConfig>> curPairs = new SortedDictionary<int, List<PairConfig>>();

        public void LoadPairList()
        {
            string SQLExpression = " SELECT A.IDPAIR, " +
                                   "        A.IDSECTION, " +
                                   " 		A.IDSECURITY1, " +
                                   " 		B.EXCHANGETICKER, " +
                                   " 		A.IDSECURITY2, " +
                                   " 		C.EXCHANGETICKER, " +
                                   " 		A.INIDATE, " +
                                   " 		A.ENDDATE " +
                                   "    FROM NESTSIM.DBO.TB701_JM_PAIRS A WITH(NOLOCK) " +
                                   "    JOIN NESTSRV06.NESTDB.DBO.TB001_SECURITIES B WITH(NOLOCK) " +
                                   "      ON A.IDSECURITY1 = B.IDSECURITY " +
                                   "    JOIN NESTSRV06.NESTDB.DBO.TB001_SECURITIES C WITH(NOLOCK) " +
                                   "      ON A.IDSECURITY2 = C.IDSECURITY";

            DataTable dt = new DataTable();

            using (newNestConn curConn = new newNestConn(true))
            {
                dt = curConn.Return_DataTable(SQLExpression);
            }

            curPairs.Clear();

            foreach (DataRow curRow in dt.Rows)
            {
                PairConfig curPair = new PairConfig();
                curPair.IdPair = int.Parse(curRow[0].ToString());
                curPair.IdSection = int.Parse(curRow[1].ToString());
                curPair.IdSecurity1 = int.Parse(curRow[2].ToString());
                curPair.Ticker1 = curRow[3].ToString();
                curPair.IdSecurity2 = int.Parse(curRow[4].ToString());
                curPair.Ticker2 = curRow[5].ToString();
                curPair.IniDate = DateTime.Parse(curRow[6].ToString());

                DateTime endDate = new DateTime();
                if (DateTime.TryParse(curRow[7].ToString(), out endDate))
                {
                    curPair.EndDate = endDate;
                }
                else
                {
                    curPair.EndDate = DateTime.MaxValue;
                }

                

                if (!curPairs.ContainsKey(curPair.IdSection))
                {
                    curPairs.Add(curPair.IdSection, new List<PairConfig>());
                }

                curPairs[curPair.IdSection].Add(curPair);
            }
        }

        public List<PairConfig> GetPairs(int IdSection, DateTime curDate)
        {
            List<PairConfig> curList = new List<PairConfig>();

            if (curPairs.ContainsKey(IdSection))
            {
                foreach (PairConfig pair in curPairs[IdSection])
                {
                    if (pair.IniDate <= curDate)
                    {
                        if (pair.EndDate >= curDate)
                        {
                            curList.Add(pair);
                        }                    
                    }
                }
            }

            return curList;
        }
         
        #endregion

    }

    public class PairConfig
    {
        private int _IdSection;
        private int _IdPair;
        private int _IdSecurity1;
        private string _Ticker1;
        private int _IdSecurity2;
        private string _Ticker2;        
        private DateTime _IniDate;
        private DateTime _EndDate;
        
        public int IdSection
        {
            get { return _IdSection; }
            set { _IdSection = value; }
        }
        public int IdPair
        {
            get { return _IdPair; }
            set { _IdPair = value; }
        }
        public int IdSecurity1
        {
            get { return _IdSecurity1; }
            set { _IdSecurity1 = value; }
        }
        public string Ticker1
        {
            get { return _Ticker1; }
            set { _Ticker1 = value; }
        }
        public int IdSecurity2
        {
            get { return _IdSecurity2; }
            set { _IdSecurity2 = value; }
        }
        public string Ticker2
        {
            get { return _Ticker2; }
            set { _Ticker2 = value; }
        }
        public DateTime IniDate
        {
            get { return _IniDate; }
            set { _IniDate = value; }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        
        

        


    }
}


