using System;
using System.Collections.Generic;
using System.Text;
using NCommonTypes;

namespace CPort
{
    class CPortItem : MarketDataItem
    {
        private TimeSpan OpenTime = new TimeSpan(09, 45, 00);
        private TimeSpan TriggerTime = new TimeSpan(10, 30, 00);
        private TimeSpan Closetime = new TimeSpan(18, 00, 00);

        private double _TradedQuantity = 0;
        private double _futPrevClose = 0;
        private double _futLast = 0;
        private double _SentQuantity = 0;
        private int _TradeSignal = 0;
        private double _TargetQuantity = 0;
        private double _PositionQuantity = 0;
        private double _TradedValue = 0;


        public double TradedQuantity { get { return _TradedQuantity; } set { _TradedQuantity = value; } }
        public double futLast { get { return _futLast; } set { _futLast = value; } }
        public double futChange { get { if (_futPrevClose != 0) { return _futLast / _futPrevClose - 1; } else { return 0; } } set { _futLast = value; } }
        public double HedgeSize { get { if (_futPrevClose != 0 && this.TargetQuantity != 0 && this.validLast < 65000) { return -this.TargetQuantity * this.validLast / _futLast; } else { return 0; } } set { _futLast = value; } }
        public double SentQuantity { get { return _SentQuantity; } set { _SentQuantity = value; } }
        public int TradeSignal { get { return _TradeSignal; } }
        public double TargetQuantity { get { return _TargetQuantity; } }
        public double PositionQuantity { get { return _PositionQuantity; } }
        public string TradingPhase { get { if (DateTime.Now.TimeOfDay < OpenTime) { return "PRE-OPEN"; } else if (DateTime.Now.TimeOfDay < TriggerTime) { return "RUNNING"; } else if (DateTime.Now.TimeOfDay < Closetime) { return "RUNNING"; } else { return "FINISHED"; } } }
        public double PositionValue { get { return _SentQuantity * this.validLast; } }
        public double validLast { get { if (this.AucVolume < this.Volume ) { return this.Last; } else { return this.AucLast; } } }
        public double validChange { get { if (this.AucVolume < this.Volume) { return this.Change; } else { return this.PCTAuction; } } }
        public double TradedValue { get { return _TradedValue; } set { _TradedValue = value; } }
        public double TradedPnL { get { return this.PositionValue + _TradedValue; } }

        public double PCTAuction
        {
            get
            {
                if (_AucCond == "G_PREOPEN" || _AucCond == "AUCTION")
                {
                    if (Last != 0)
                    {
                        if (AucLast != 0) return AucLast / Last - 1;
                    }
                    else if (Close != 0)
                    {
                        if (AucLast != 0) return AucLast / Close - 1;
                    }
                    else
                    {
                        return 0;
                    }
                    return 0;
                }
                else
                    return 0;
            }
        }

        private double _prevQuantity = 0;
        private double _curQuantity = 0;

        public double prevQuantity { get { return _prevQuantity; } }
        public double curQuantity { get { return _curQuantity; } }
        public double diffQuantity { get { if (_prevQuantity != 0) { return _curQuantity - _prevQuantity; } else { return 0; } } }

        public double _publishBid = 0;
        public double _publishAsk = 0;
        public double _prevLast = 0;
        public double _recVolume = 0;

        public double publishBid { get { return _publishBid; } set { _publishBid = value; } }
        public double publishAsk { get { return _publishAsk; } set { _publishAsk = value; } }
        public double recVolume { get { return _recVolume; } set { _recVolume = value; } }

        public double prevLast { get { return _prevLast; } }
        
        public void UpdateSignal()
        {
            if (curQuantity > prevQuantity)
            {
                _TradeSignal = 1;
                UpdateQuantity();
            }
            else if (curQuantity < prevQuantity)
            {
                _TradeSignal = -1;
                UpdateQuantity();
            }
            else
            {
                _TradeSignal = 0;
            }
        }

        public void SetQuants()
        {
            _prevQuantity = _curQuantity;
            _prevLast = validLast;
            _curQuantity = 100000000 / validLast;

            UpdateQuantity();
        }

        public void UpdateQuantity()
        {
            if (this.TradingPhase == "RUNNING")
            {
                double maxShares = 10000000 / validLast;
                double refVolume = Math.Min(_curQuantity - _prevQuantity, maxShares);
                _TargetQuantity = (int)(diffQuantity / 100) * 100;
            }

            if (this.TradingPhase == "FINISHED")
            {
                _TargetQuantity = 0;
            }
        }

        public void UpdateContracts(double NoContracts)
        {
            double RoundedQuantity = Math.Round(NoContracts / 1, 0) * 1;

            if(NoContracts!=0)
                _TargetQuantity = RoundedQuantity;

            _PositionQuantity = _TargetQuantity;
        }
    
    }
}
