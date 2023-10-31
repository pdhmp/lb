using System;
using System.Collections.Generic;
using System.Text;
 
namespace MomentumBZRunner
{
    public class MomentumSectorInfo
    {
        public SortedDictionary<int, double> DayPrices;
        public SortedDictionary<int, double> Weights;
        public SortedDictionary<int, double> WindowIniPrice;
        public double[] SignalReference;       

        private int _SectorID;
        public int SectorID
        {
            get { return _SectorID; }
        }

        public MomentumSectorInfo(int sectorID,
                                  SortedDictionary<int, double> _DayPrices,
                                  SortedDictionary<int, double> _Weights,
                                  SortedDictionary<int, double> _WindowIniPrice,
                                  double[] _SignalReference)
        {
            _SectorID = sectorID;
            DayPrices = _DayPrices;
            Weights = _Weights;
            WindowIniPrice = _WindowIniPrice;
            SignalReference = _SignalReference;
        }
	
    }
}
