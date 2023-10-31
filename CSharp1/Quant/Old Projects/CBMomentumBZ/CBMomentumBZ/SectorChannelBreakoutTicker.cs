using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NCommonTypes
{
    class SectorChannelBreakoutTicker
    {
        #region Attributes

        private int _IdSector;
        private string _Sector;
        private double _SectorWeight;
        private double _SectorLow;
        private double _SectorHigh;
        private double _SectorPrice;        

        private int _IdTicker;
        private string _Ticker;
        private double _TickerWeight;
        private double _TickerPrice;

        private int _CloseSignal;
        private int _CurSignal;

        #endregion

        #region Properties

        public int IdSector { get { return _IdSector; } set { _IdSector = value; } }
        public string Sector { get { return _Sector; } set { _Sector = value; } }
        public double SectorWeight { get { return _SectorWeight; } set { _SectorWeight = value; } }
        public double SectorLow { get { return _SectorLow; } set { _SectorLow = value; } }
        public double SectorHigh { get { return _SectorHigh; } set { _SectorHigh = value; } }
        public double SectorPrice { get { return _SectorPrice; } set { _SectorPrice = value; } }

        public int IdTicker { get { return _IdTicker; } set { _IdTicker = value; } }
        public string Ticker { get { return _Ticker; } set { _Ticker = value; } }
        public double TickerWeight { get { return _TickerWeight; } set { _TickerWeight = value; } }
        public double TickerPrice { get { return _TickerPrice; } set { _TickerPrice = value; } }

        #endregion
    }
}
