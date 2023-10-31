using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NestDLL;
using NFastData;

namespace OppOp
{
    class OppOpData
    {
        #region Singleton Region

        private static volatile object SyncRoot = new object();

        private static OppOpData _Instance;
        public static OppOpData Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new OppOpData();
                        }
                    }
                }

                return _Instance;
            }
        }

        private OppOpData() { }

        #endregion

        #region FastData Region

        private bool isInitialized = false;
        private bool iniDateInitialized = false;

        private DateTime _RunDate;
        public DateTime RunDate
        {
            get { return _RunDate; }
            set { if (!isInitialized) { _RunDate = value; iniDateInitialized = true; } }
        }

        public FastData FD_Low;
        public FastData FD_High;
        public FastData FD_Last;
        public FastData FD_Open;
        public FastData FD_VWAP;
        public FastData FD_TR_Index;        

        public void InitializeObjects()
        {
            if (!isInitialized && iniDateInitialized)
            {
                FD_Low = new FastData(3, RunDate, DateTime.Today, 1);
                FD_High = new FastData(4, RunDate, DateTime.Today, 1);
                FD_Last = new FastData(1, RunDate, DateTime.Today, 1);
                FD_Open = new FastData(8, RunDate, DateTime.Today, 1);
                FD_VWAP = new FastData(5, RunDate, DateTime.Today, 1);
                FD_TR_Index = new FastData(101, RunDate, DateTime.Today, 1);

                isInitialized = true;
            }
        }

        public void LoadTickers(double[] SecurityList)
        {
            FD_Low.LoadTickers(SecurityList);
            FD_High.LoadTickers(SecurityList);
            FD_Last.LoadTickers(SecurityList);
            FD_Open.LoadTickers(SecurityList);
            FD_VWAP.LoadTickers(SecurityList);
            FD_TR_Index.LoadTickers(SecurityList);
        }     

        #endregion

        #region InformationRegion

        public List<string> InformationString = new List<string>();

        #endregion
    }
}
