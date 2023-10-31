using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NestQuant.Common
{
    public abstract class Strategy_Base
    {
        protected string _Name = "";
        protected DateTime _iniDate;
        protected DateTime _endDate;
        protected Utils.StrategyTypes _StrategyType = Utils.StrategyTypes.Undefined;
        protected Utils.TimeIntervals _StrategyFrequency = Utils.TimeIntervals.Undefined;
        protected Utils.WeighingSchemes _StrategyWeighing = Utils.WeighingSchemes.Undefined;
        protected int _Id_Ticker_Template = 0;
        
        protected bool _IsRealTime;
        protected RTPrice _PriceFeeder;
        protected Mutex StrategyMutex = new Mutex(); 

        public perfSummary_Table stratPerfSummary;
        public Weight_Table stratPositions;

        public PercPositions_Table subWeights;
        public Performances_Table subPerformances;
        public Contributions_Table subContributions;

        public event EventHandler StrategyChanged;        

        public bool IsRealTime
        {
            get { return _IsRealTime; }
        }
        public RTPrice PriceFeeder
        {
            get { return _PriceFeeder; }
        }
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        public DateTime iniDate
        {
            get
            {
                return _iniDate;
            }
            set
            {
                _iniDate = value;
            }
        }
        public DateTime endDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }
        public Utils.StrategyTypes StrategyType
        {
            get
            {
                return _StrategyType;
            }
            set
            {
                _StrategyType = value;
            }
        }
        public Utils.TimeIntervals DateRowInterval
        {
            get
            {
                return _StrategyFrequency;
            }
            set
            {
                _StrategyFrequency = value;
            }
        }
        public Utils.WeighingSchemes Weighing
        {
            get
            {
                return _StrategyWeighing;
            }
            set
            {
                if (_StrategyWeighing == value)
                    return;
                _StrategyWeighing = value;
            }
        }
        public int Id_Ticker_Template
        {
            get
            {
                return _Id_Ticker_Template;
            }
            set
            {
                if (_Id_Ticker_Template == value)
                    return;
                _Id_Ticker_Template = value;
            }
        }


        public Strategy_Base(bool isRealTime)
        {
            _IsRealTime = isRealTime;
        }
               
        private void Changed()
        {
            if (StrategyChanged != null)
            {
                StrategyChanged(this, EventArgs.Empty);
            }
        }

        public void Refresh(object source, EventArgs e)
        {
            if (IsRealTime)
            {
                StrategyMutex.WaitOne();

                UpdateStrategy(source, e);

                StrategyMutex.ReleaseMutex();

                Changed();
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        protected virtual void UpdateStrategy(object source, EventArgs e)
        {
        }

        public void SetPriceFeeder(RTPrice priceFeeder)
        {
            _PriceFeeder = priceFeeder;
        }
        
    }
}
