using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

using NFastData;
using NestSymConn;
using NCommonTypes;
using NestDLL;

namespace ValueBZ_V2
{
    public class ValueData
    {
        #region Singleton Region

        private static ValueData _Instance = null;
        private static volatile object padlock = new object();

        public static ValueData Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (padlock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new ValueData();
                        }
                    }
                }

                return _Instance;
            }
        }

        private ValueData()
        {
            SymConn.Instance.BovespaSource = NEnuns.NSYMSources.FLEXBSE;
            SymConn.Instance.OnData += new EventHandler(OnMarketData);
        }


        #endregion

        #region FastData Region

        public FastData Last;
        public FastData TRDay;
        public FastData PreAuc;

        public FastData OutShares;
        public FastDataEPS EPS;

        private DateTime _IniDate;
        public DateTime IniDate
        {
            get
            {
                return _IniDate;
            }
            set
            {
                if (!IsInitialized)
                {
                    _IniDate = value;
                }
            }
        }

        private bool IsInitialized = false;

        public void InitializeObjects()
        {
            if (!IsInitialized)
            {
                IsInitialized = true;

                Last = new FastData(1, _IniDate, DateTime.Today, 1);
                TRDay = new FastData(100, _IniDate, DateTime.Today, 1);
                PreAuc = new FastData(312, _IniDate, DateTime.Today, 22);   

                OutShares = new FastData(503, _IniDate, DateTime.Today, 9);
                EPS = new FastDataEPS(_IniDate, DateTime.Today);
                
                //Load ticker template
                double[] ibov = new double[1];
                ibov[0] = 1073;
                Last.LoadTickers(ibov);                              
            }
        }

        public void LoadTickers(double[] IdSecurityList)
        {
            Last.LoadTickers(IdSecurityList);
            TRDay.LoadTickers(IdSecurityList);
            PreAuc.LoadTickers(IdSecurityList);
            OutShares.LoadTickers(IdSecurityList);
            EPS.LoadTickers(IdSecurityList);

            SubscribeTickers(IdSecurityList);
        }

        public DateTime PrevDate(DateTime CurDate)
        {
            DateTime result;

            result = DateTime.FromOADate(Last.GetValue(1073, CurDate.AddDays(-1), false)[0]);

            return result;
        }

        public string getTicker(double IdSecurity)
        {
            string Ticker = "NotFound";

            if (!IdSecurityToTicker.TryGetValue(IdSecurity, out Ticker))
            {
                Ticker = "NotFound";
            }

            return Ticker;
        }

        #endregion

        #region RealTime Prices Region

        private SortedDictionary<string, MarketDataItem> MarketData = new SortedDictionary<string, MarketDataItem>();
        private SortedDictionary<double, string> IdSecurityToTicker = new SortedDictionary<double, string>();

        private void SubscribeTickers(double[] IdSecurityList)
        {            
            string sIdSec = "0";
            for (int i = 0; i < IdSecurityList.Length; i++)
            {
                if (!IdSecurityToTicker.ContainsKey(IdSecurityList[i]))
                {
                    sIdSec = sIdSec + ", " + IdSecurityList[i];
                }                
            }

            string SQLString = "SELECT IDSECURITY, EXCHANGETICKER FROM NESTSRV06.NESTDB.DBO.TB001_SECURITIES WHERE IDSECURITY IN (" + sIdSec + ")";

            DataTable dt = new DataTable();

            using (newNestConn curConn = new newNestConn())
            {
                dt = curConn.Return_DataTable(SQLString);
            }

            foreach (DataRow curRow in dt.Rows)
            {
                double IdSec = double.Parse(curRow["IdSecurity"].ToString());
                string Ticker = curRow["ExchangeTicker"].ToString();

                if (!IdSecurityToTicker.ContainsKey(IdSec))
                {
                    MarketDataItem curItem = new MarketDataItem();
                    curItem.Ticker = Ticker;
                    curItem.IdTicker = (int)IdSec;

                    MarketData.Add(Ticker, curItem);
                    IdSecurityToTicker.Add(IdSec, Ticker);

                    SymConn.Instance.Subscribe(Ticker, 2);
                }
            }
        }

        private void OnMarketData(object sender, EventArgs e)
        {
            MarketUpdateList curList = (MarketUpdateList)e;

            foreach (MarketUpdateItem curUpdate in curList.ItemsList)
            {
                if (MarketData.ContainsKey(curUpdate.Ticker))
                {
                    MarketData[curUpdate.Ticker].Update(curUpdate);
                }
            }        
        }

        public double GetRTLast(double IdSecurity)
        {
            double lastPx = double.NaN;

            if (IdSecurityToTicker.ContainsKey(IdSecurity))
            {
                lastPx = MarketData[IdSecurityToTicker[IdSecurity]].Last;
                if (lastPx == 0)
                {
                    SymConn.Instance.Subscribe(IdSecurityToTicker[IdSecurity], 2);
                    System.Threading.Thread.Sleep(500);
                    lastPx = GetRTLast(IdSecurity);
                }
            }

            return lastPx;
        }

        public double GetDayChange(double IdSecurity, DateTime curDate)
        {
            double change = 0;

            if (curDate != DateTime.Today)
            {
                double[] GetTR = TRDay.GetValue(IdSecurity, curDate, true);
                if (GetTR[0] != 1)
                {
                    change = GetTR[1];
                }
            }
            else
            {
                string Ticker = "";
                if (IdSecurityToTicker.TryGetValue(IdSecurity, out Ticker))
                {
                    double close = MarketData[Ticker].Close;
                    double last = MarketData[Ticker].ValidLast;

                    change = last / close - 1;
                }
            }

            return change;
        }

        #endregion

        #region Debug Region

        public void PrintLog(string message)
        {
            StreamWriter logFile = new StreamWriter(@"C:\Temp\ValueBZ_Debug.xls",true);
            logFile.WriteLine(message);
            logFile.Close();
        }

        #endregion

    }
}
