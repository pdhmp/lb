using System;
using System.Collections.Generic;
using System.Text;

namespace NewMomentumUS 
{
    public class MomentumInfo
    {
        public SortedDictionary<int, MomentumSectorInfo> MomentumSectorsInfo;
        public SortedDictionary<int, double> Weights;

        public SortedDictionary<int, int> CurrentPosition;
        public SortedDictionary<int, int> TickersBySector;

        public double AdjustFactor;
        public double StrategyNAV;

        public MomentumInfo(SortedDictionary<int, MomentumSectorInfo> _MomentumSectorsInfo,
                            SortedDictionary<int, double> _Weights,
                            SortedDictionary<int, int> _CurrentPosition,
                            SortedDictionary<int, int> _TickersBySector,
                            double _AdjustFactor,
                            double _StrategyNAV)
        {
            MomentumSectorsInfo = _MomentumSectorsInfo;
            Weights = _Weights;
            CurrentPosition = _CurrentPosition;
            TickersBySector = _TickersBySector;
            AdjustFactor = _AdjustFactor;
            StrategyNAV = _StrategyNAV;
        }
    }
}
