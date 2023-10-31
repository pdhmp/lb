using System;
using System.Collections.Generic;
using System.Text;

namespace MarketClusters
{
    class DataSeries
    {
        public int Relevant_Id;
        public int Id_Type;
        public double Weight;
        public bool isActive;
        public PointPairList[] PointPairs;

        public DataSeries(int _Id_Ticker, double _Weight, bool _isActive)
        {
            Relevant_Id = _Id_Ticker;
            Weight = _Weight;
            isActive = _isActive;
        }
    }
}
