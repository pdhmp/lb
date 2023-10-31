using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCommonTypes;
using NestSymConn;

namespace LiveTrade2
{
    class LTMarketDataItem : MarketDataItem
    {
        //  ===================================  OVERRIDEN PROPERTIES

        private BlackSholes curBlackSholes = new BlackSholes();

        new public string Ticker
        {
            get { return _Ticker; }
            set
            {
                base.Ticker = value;
                if (_Ticker.Length > 5)
                {
                    char OptCode = Ticker[4];
                    if (OptCode > 'L')
                    {
                        _OptType = "Put";
                    }
                    else
                    {
                        _OptType = "Call";
                    }

                }
            }
        }

        //  ===================================  
        public List<Level2Line> BuyDepth = new List<Level2Line>();

        private string _SecType = "None"; public string SecType { get { return _SecType; } set { _SecType = value; } }
        private string _OptType = "None"; public string OptType { get { return _OptType; } }
        private string _Category; public string Category { get { return _Category; } set { _Category = value; } }
        private int _SortOrder; public int SortOrder { get { return _SortOrder; } set { _SortOrder = value; } }
        private double _AvVolume6m = 0; public double AvVolume6m { get { if (!double.IsNaN(_AvVolume6m)) return _AvVolume6m * Last * PriceMult / 1000000; else return 0; } set { _AvVolume6m = value; } }

        private double _IndexQuantity = 0; public double IndexQuantity { get { return _IndexQuantity; } set { _IndexQuantity = value; } }

        // =============================== BID/ASK Fields ============================
        private double _Spread = 0; public double Spread { get { if (_Bid != 0 && _Ask != 0) return _Ask - _Bid; else return 0; } }
        private double _AvgSpread = 0; public double AvgSpread { get { return _AvgSpread; } }

        // =============================== High/Low Fields ============================
        private double _DateHigh = 0; public double DateHigh { get { return _DateHigh; } set { _DateHigh = value; } }
        private double _DateLow = 0; public double DateLow { get { return _DateLow; } set { _DateLow = value; } }

        public double FromHigh { get { if (_High != 0 && _Last != 0) return _Last / _High - 1; else return 0; } }
        public double FromLow { get { if (_Low != 0 && _Last != 0) return _Last / _Low - 1; else return 0; } }

        public double FromDateHigh { get { if (_DateHigh != 0 && _Last != 0) return _Last / _DateHigh - 1; else return 0; } }
        public double FromDateLow { get { if (_DateLow != 0 && _Last != 0) return _Last / _DateLow - 1; else return 0; } }

        public int CloseToDateHigh { get { if (this.FromDateHigh > -0.05 && this.FromDateHigh != -0) return 1; else return 0; } }
        public int CloseToDateLow { get { if (this.FromDateLow < 0.05 && this.FromDateLow != -0) return 1; else return 0; } }

        // =============================== AUCTION Fields ============================
        private double _CHGPrevAuction = 0; public double CHGPrevAuc { get { return _CHGPrevAuction; } }

        public double ABSAuction { get { return Math.Abs(PCTAuction); } }
        public double AucGain { get { if (TradingStatus == TradingStatusType.G_PREOPEN_P || TradingStatus == TradingStatusType.AUCTION_K) return AucVolume * Math.Abs(AucLast - Last) * PriceMult; else return 0; } }
        //public double AucGain { get { if (_AucCond == "G_PREOPEN" || _AucCond == "AUCTION") return AucVolume * Math.Abs(AucLast - Last) * PriceMult; else return 0; } }
        public double IndexContrib { get { if (_Last != 0) { return _IndexQuantity * _Last; } else { return _IndexQuantity * _Close; } } }
        public double FromVWAP { get { if (_VWAP != 0 && _Last != 0) return _Last / _VWAP - 1; else return 0; } }


        private bool _CalcIVol = false; public bool CalcIVol { get { return _CalcIVol; } set { _CalcIVol = value; } }
        private double _UnderlyingLast = 0; public double UnderlyingLast { get { return _UnderlyingLast; } set { _UnderlyingLast = value; } }
        public double IVolBid { get { if (CalcIVol && _Bid > 0) return curBlackSholes.ImpliedVol(_OptType, _UnderlyingLast, _Strike, _Expiration.Subtract(DateTime.Now.Date).TotalDays / 252, 0.08, _Bid); else return 0; } }
        public double IVolAsk { get { if (CalcIVol && _Bid > 0) return curBlackSholes.ImpliedVol(_OptType, _UnderlyingLast, _Strike, _Expiration.Subtract(DateTime.Now.Date).TotalDays / 252, 0.08, _Ask); else return 0; } }
        public double IVolLast { get { if (CalcIVol && _Bid > 0) return curBlackSholes.ImpliedVol(_OptType, _UnderlyingLast, _Strike, _Expiration.Subtract(DateTime.Now.Date).TotalDays / 252, 0.08, _Last); else return 0; } }
        public double Intrinsic { get { if (_OptType == "Call") { if (_UnderlyingLast - _Strike > 0) return _UnderlyingLast - _Strike; else return 0; } else { if (_Strike - _UnderlyingLast > 0) return _Strike - _UnderlyingLast; else return 0; }; } }
        public double Delta { get { if (CalcIVol && (_Bid + _Ask) / 2 > 0) return curBlackSholes.Delta(_OptType, _UnderlyingLast, _Strike, _Expiration.Subtract(DateTime.Now.Date).TotalDays / 252, 0.08, (_Bid + _Ask) / 2); else return 0; } }
        public double PercMny { get { if (_OptType == "Call") { if (_UnderlyingLast != 0 && _Strike != 0) { return -(_Strike / _UnderlyingLast - 1); } else return 0; } else { if (_UnderlyingLast != 0 && _Strike != 0) { return (_Strike / _UnderlyingLast - 1); } else return 0; }; } }

        private int _PosMH_Trad; public int PosMH_Trad { get { return _PosMH_Trad; } set { _PosMH_Trad = value; } }
        private int _PosMH_LS; public int PosMH_LS { get { return _PosMH_LS; } set { _PosMH_LS = value; } }
        private int _PosFIA_Trad; public int PosFIA_Trad { get { return _PosFIA_Trad; } set { _PosFIA_Trad = value; } }
        private int _PosFIA_SCaps; public int PosFIA_SCaps { get { return _PosFIA_SCaps; } set { _PosFIA_SCaps = value; } }
        
        //public DateTime AucTimeLeft { get { return new DateTime(1900,01,01).Add(new TimeSpan(0,0,0,0,100)); } }
        public DateTime AucTimeLeft 
        { 
            get 
            {
                return (DateTime.Now < AucCloseTime) ? _AucCloseTime.Subtract(DateTime.Now.TimeOfDay) : new DateTime(1900, 1, 1);
            } 
        }

        public double PCTAuction
        {
            get
            {
                if (TradingStatus == TradingStatusType.G_PREOPEN_P || TradingStatus == TradingStatusType.AUCTION_K)
                //if (_AucCond == "G_PREOPEN" || _AucCond == "AUCTION")
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

        private void update_AvgSpread()
        {
            if (_Bid != 0 && _Ask != 0)
            {
                if (_AvgSpread == 0)
                {
                    _AvgSpread = _Spread;
                }
                else
                {
                    _AvgSpread = ((50 * _AvgSpread) + _Spread) / 51;
                }
            }
        }
    }
}
