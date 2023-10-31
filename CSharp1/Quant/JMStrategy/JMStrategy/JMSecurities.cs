using System;
using NCommonTypes;
using NestSymConn;
using NestQuant.Common;




namespace JMStrategy
{
    public class JMSecurities
    {        
        private int _IdSecurity;
        private string _Ticker;
        private double _Volatility;                
        private double _Stochastic;                
                       
        public int IdSecurity
        {
            get { return _IdSecurity; }            
        }    
        public string Ticker
        {
            get { return _Ticker; }            
        }        
        public double Volatility
        {
            get { return _Volatility; }
            set { _Volatility = value; }
        }       
        public double Close
        {
            get
            {
                double _Close = 0;

                lock (CurDataSync)
                {
                    _Close = CurData.Close;
                }

                return _Close;
            }
        
        }        
        public double Last
        {
            get
            {
                double _Last = 0;

                lock (CurDataSync)
                {
                    if (ParentModel.DateRef == DateTime.Today)
                    {
                        _Last = CurData.Last;
                    }
                    else
                    {
                        _Last = LastPrice[0];
                    }
                }

                return _Last;
            }
        }
        public double Stochastic
        {
            get { return _Stochastic; }
            set { _Stochastic = value; }
        }             
        
                      
        private double vol21days = double.NaN;
        private bool _SecurityInitialized = false;
        public bool SecurityInitialized { get { return _SecurityInitialized; } }
        
        public MarketDataItem CurData;
        private volatile object CurDataSync = new object();
        
        private double[] _LastPrice;
        private volatile object LastPriceSync = new object();
        public double[] LastPrice
        {
            get
            {
                lock (LastPriceSync)
                {
                    return _LastPrice;
                }
            }
        }

        private double[] Open;
        private double[] High;
        private double[] Low;

        private JMModel ParentModel;

        public event EventHandler SecurityUpdated;
        
        public JMSecurities(int _IdSec, string _newTicker, JMModel _ParentModel)
        {
            _IdSecurity = _IdSec;
            _Ticker = _newTicker;

            ParentModel = _ParentModel;

            CurData = new MarketDataItem();
            CurData.IdTicker = _IdSec;
            CurData.Ticker = _newTicker;

            _LastPrice = new double[JMUtils.MaxDays];
            Open = new double[JMUtils.MaxDays];
            High = new double[JMUtils.MaxDays];
            Low = new double[JMUtils.MaxDays];

            JMData.Instance.LoadTickers(_IdSecurity);

            if (ParentModel.DateRef == DateTime.Today)
            {
                SymConn.Instance.OnData += new EventHandler(ReceiveMarketData);
            }
        }

        void ReceiveMarketData(object sender, EventArgs e)
        {
            MarketUpdateList curList = (MarketUpdateList)e;
            
            foreach (MarketUpdateItem curItem in curList.ItemsList)
            {
                if (curItem.Ticker == Ticker)
                {
                    lock (CurDataSync)
                    {
                        CurData.Update(curItem);
                    }


                    if (curItem.FLID == NestFLIDS.Last || curItem.FLID == NestFLIDS.AucLast)
                    {
                        if (_SecurityInitialized)
                        {
                            UpdateTicker();
                        }
                    }
                }
            }
        }

        public void InitializeSecurity()
        {
            if (!_SecurityInitialized)
            {
                if (_IdSecurity == 1237)
                {
                    int a = 0;
                }

                LoadHistData();
                CalculateHistData();

                _SecurityInitialized = true;

                UpdateTicker();
            }
        }

        private void LoadHistData()
        {
            _LastPrice = JMData.Instance.GetPriceRange(_IdSecurity, NEnuns.NPriceType.LastPrice, ParentModel.DateRef, JMUtils.MaxDays);
            Open = JMData.Instance.GetPriceRange(_IdSecurity, NEnuns.NPriceType.Open, ParentModel.DateRef, JMUtils.MaxDays);
            High = JMData.Instance.GetPriceRange(_IdSecurity, NEnuns.NPriceType.High, ParentModel.DateRef, JMUtils.MaxDays);
            Low = JMData.Instance.GetPriceRange(_IdSecurity, NEnuns.NPriceType.Low, ParentModel.DateRef, JMUtils.MaxDays);            
        }
        private void CalculateHistData()
        {
            //21 days volatility
            vol21days = Utils.calcStdev(ref _LastPrice, 1, 21);
        }

        public void UpdateTicker()
        {
            //Get last price. If the security is on auction, check if there are spikes and remove them.
            if (ParentModel.DateRef == DateTime.Today)
            {
                double curLast = 0;

                lock (CurDataSync)
                {
                    if (CurData.ValidLast == 0 || CurData.Last == 0)
                    {
                        throw new Exception("Invalid realtime Last Price for security " + Ticker);
                    }

                    curLast = CurData.Last;

                    if (CurData.ValidLast > CurData.Last)
                    {
                        curLast = Math.Min(CurData.ValidLast, CurData.Last + vol21days);
                    }
                    else if (CurData.ValidLast < CurData.Last)
                    {
                        curLast = Math.Max(CurData.ValidLast, CurData.Last - vol21days);
                    }
                }

                lock (LastPriceSync)
                {
                    _LastPrice[0] = curLast;
                }
            }

            lock (LastPriceSync)
            {
                if (IdSecurity == 279 && ParentModel.Days == 21)
                {
                    int a = 0;
                }                

                _Volatility = JMUtils.VolGarmanKlassYZ(_LastPrice, _LastPrice, High, Low, ParentModel.Days);
                _Stochastic = JMUtils.Stochastic(_LastPrice, ParentModel.Days);
            }

            if (SecurityUpdated != null)
            {
                SecurityUpdated(this, null);
            }
        }
    }
}
