using System;
using NFastData;
using System.Collections.Generic;

namespace Patriot
{
    public class PatriotData
    {
        #region Singleton Region

        private static volatile object SyncRoot = new object();

        private static PatriotData _Instance;
        public static PatriotData Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new PatriotData();
                        }
                    }
                }

                return _Instance;
            }
        }

        private PatriotData() { }

        #endregion

        #region FastData Region

        private bool isInitialized = false;
        private bool iniDateInitialized = false;

        private DateTime _IniDate;
        public DateTime IniDate
        {
            get { return _IniDate; }
            set { if (!isInitialized) { _IniDate = value; iniDateInitialized = true; } }
        }

        public FastData Last30Vwap;
        public FastData Last;
        public FastData TRIndex;
        public FastData CloseAucAvg5d;
        public FastData Last30Avg5d;
        public FastData First330Vwap;
        public FastData First330Volume;
        public FastData First330Avg5d;

        public void InitializeObjects()
        {
            if (!isInitialized && iniDateInitialized)
            {
                Last30Vwap = new FastData(331, _IniDate, DateTime.Now, 22);
                Last = new FastData(1, _IniDate, DateTime.Now, 1);
                TRIndex = new FastData(101, _IniDate, DateTime.Now, 1);
                CloseAucAvg5d = new FastData(313, _IniDate, DateTime.Now, 22);
                Last30Avg5d = new FastData(332, _IniDate, DateTime.Now, 22);
                First330Vwap = new FastData(351, _IniDate, DateTime.Now, 22);
                First330Volume = new FastData(350, _IniDate, DateTime.Now, 22);
                First330Avg5d = new FastData(352, _IniDate, DateTime.Now, 22);


                double[] IBOV = new double[1];
                IBOV[0] = 1073;
                Last.LoadTickers(IBOV);               

                isInitialized = true;
            }
        }

        public void LoadTickers(double[] SecurityList)
        {
            Last30Vwap.LoadTickers(SecurityList);
            Last.LoadTickers(SecurityList);
            TRIndex.LoadTickers(SecurityList);
            CloseAucAvg5d.LoadTickers(SecurityList);
            Last30Avg5d.LoadTickers(SecurityList);
            First330Vwap.LoadTickers(SecurityList);
            First330Volume.LoadTickers(SecurityList);
            First330Avg5d.LoadTickers(SecurityList);
        }

        public DateTime GetPrevDate(DateTime curDate)
        {
            return Last.PrevDate(curDate);
        }

        public double[] GetValue(double Ticker, int PriceType, DateTime CurDate, DateTime refDate, bool ExactOnly)
        {
            double[] result = new double[2];
           
            

            bool adjust = false;

            switch (PriceType)
            {
                case 1:
                    result = Last.GetValue(Ticker, CurDate, ExactOnly);
                    adjust = true;
                    break;
                case 101:
                    result = TRIndex.GetValue(Ticker, CurDate, ExactOnly);
                    break;
                case 331:
                    result = Last30Vwap.GetValue(Ticker, CurDate, ExactOnly);
                    adjust = true;
                    break;
                case 313:
                    result = CloseAucAvg5d.GetValue(Ticker, CurDate, ExactOnly);
                    break;
                case 332:
                    result = Last30Avg5d.GetValue(Ticker, CurDate, ExactOnly);
                    break;
                case 351:
                    result = First330Vwap.GetValue(Ticker, CurDate, ExactOnly);
                    break;
                case 350:
                    result = First330Volume.GetValue(Ticker, CurDate, ExactOnly);
                    break;
                case 352:
                    result = First330Avg5d.GetValue(Ticker, CurDate, ExactOnly);
                    break; 
            }

            if (adjust)
            {

                double refTR = TRIndex.GetValue(Ticker, refDate, true)[1];
                double refLast = Last.GetValue(Ticker, refDate, true)[1];
                double curTR = TRIndex.GetValue(Ticker, CurDate, ExactOnly)[1];
                double curLast = Last.GetValue(Ticker, CurDate, ExactOnly)[1];
                double adjLast = refLast * (curTR / refTR);

                result[1] = result[1] * (adjLast / curLast);
            }                                                                                    

            if (double.IsNaN(result[1])) { result[1] = 0; }

            return result;
        }

        #endregion

        #region InformationRegion

        public List<string> InformationString = new List<string>();

        #endregion
    }
}
