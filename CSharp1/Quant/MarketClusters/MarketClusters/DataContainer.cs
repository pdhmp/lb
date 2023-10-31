using System;
using System.Collections.Generic;
using System.Text;

namespace MarketClusters
{
    class DataContainer
    {
        public SortedDictionary<DataKey, DataSeries> allSeries = new SortedDictionary<DataKey, DataSeries>();
        public int[,] correlKeys = new int[1000000, 4];
        public double[] correlValues = new double[1000000];
        public int correlCounter = 0;

        public DataContainer()
        { 
        }

        public void Add(int Relevant_Id, int Id_Type, DataSeries curSeries)
        {
            foreach (DataSeries existingSeries in allSeries.Values)
            {
                double[,] tempArray = Utils.AlignSeries(curSeries, existingSeries);
                double curCorrel = Utils.calcCorrel(tempArray);
                if (curCorrel == 0) curCorrel = 0.001F;
                correlKeys[correlCounter, 0] = curSeries.Relevant_Id;
                correlKeys[correlCounter, 1] = curSeries.Id_Type;
                correlKeys[correlCounter, 2] = existingSeries.Relevant_Id;
                correlKeys[correlCounter, 3] = existingSeries.Id_Type;
                correlValues[correlCounter] = curCorrel;
                correlCounter++;
            }
            DataKey curKey = new DataKey(Relevant_Id, Id_Type);
            allSeries.Add(curKey, curSeries);
        }

        public void Remove(DataKey curKey)
        {
            for (int i = 0; i < correlCounter; i++)
            {
                if ((correlKeys[i, 0] == curKey.Relevant_Id && correlKeys[i, 1] == curKey.Id_Type) ||
                    (correlKeys[i, 2] == curKey.Relevant_Id && correlKeys[i, 3] == curKey.Id_Type))
                {
                    correlKeys[i, 0] = 0;
                    correlKeys[i, 1] = 0;
                    correlKeys[i, 2] = 0;
                    correlKeys[i, 3] = 0;
                    correlValues[i] = 0;
                }
            }
            allSeries.Remove(curKey);
        }

    }
}
