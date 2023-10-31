using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NCommonTypes;

namespace NewStrongOpen
{
    /// <summary>
    /// Manages the strategy info for a security
    /// </summary>
    class StgItem : MarketDataItem
    {
        #region Attributes Region

        private int _MinLot = 1;
        private int _IdPrimaryExchange;
       
        private double _PrevChangeTrigger;
        private double _PrevChange = 0;
        private double _PrevChangeIndex = 0;
        private double _HdgPrevClose;
        private double _PrevChangeSpread = 0;

        private string _TradingPhase = "";
        private double _PercOfDay;        
        
        #endregion

        #region Properties Region
              
        public int MinLot
        {
            get { return _MinLot; }
            set { _MinLot = value; }
        }
        public int IdPrimaryExchange
        {
            get { return _IdPrimaryExchange; }
            set { _IdPrimaryExchange = value; }
        }

        public double PrevChangeTrigger
        {
            get { return _PrevChangeTrigger; }
            set { _PrevChangeTrigger = value; }
        }
        public double PrevChange
        {
            get { return _PrevChange; }
            set 
            { 
                _PrevChange = value;
                update_PrevChangeSpread();
            }
        }        
        public double  PrevChangeIndex
        {
            get { return _PrevChangeIndex; }
            set 
            { 
                _PrevChangeIndex = value;
                update_PrevChangeSpread();
            }
        }       
        public double HdgPrevClose
        {
            get { return _HdgPrevClose; }
            set { _HdgPrevClose = value; }
        }        

        public string TradingPhase
        {
            get { return _TradingPhase; }
            set { _TradingPhase = value; }
        }
        public double PercOfDay
        {
            get { return _PercOfDay; }
            set { _PercOfDay = value; }
        }

        #endregion

        #region Events Region

        public event EventHandler OnTrade;

        #endregion

        #region Methods Region

        /// <summary>
        /// Calculate the difference between ticker´s and index´s previous change
        /// </summary>
        private void update_PrevChangeSpread()
        {
            if (_PrevChange != 0 && _PrevChangeIndex != 0)
            {
                _PrevChangeSpread = _PrevChange - _PrevChangeIndex;
            }
        }       

        #endregion


    }
}
