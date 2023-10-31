using System;
using System.Collections.Generic;
//using System.Text;

using NestQuant.Common;

namespace NestQuant.Strategies
{
    public class ZigZag
    {

        public struct Vertex
        {
            public int bar;
            public DateTime date;
            public double value;
            public int lastVertex;
        }        
        
        public Asset Asset;
        public double HurdleRate;
        public SortedDictionary<int, Vertex> Peaks;
        public SortedDictionary<int, Vertex> Troughs;

        private int maxBar;
        private double maxValue;
        private DateTime maxDate;

        private int lastPeak;
        private int lastTrough;

        private int minBar;
        private double minValue;
        private DateTime minDate;

        private bool inMaxFlag;
        
        private double sideFactor;

        public ZigZag(Asset _Asset, double _HurdleRate)
        {
            Asset = _Asset;
            sideFactor = Asset.sideFactor;
            HurdleRate = _HurdleRate;
            Peaks = new SortedDictionary<int, Vertex>();
            Troughs = new SortedDictionary<int, Vertex>();
        }


        public void historicZigZag(bool intraday)
        {
            if (intraday)
            {
                historicZigZagIntraday();
            }
            else
            {
                historicZigZagDaily();
            }
        }

        private void historicZigZagDaily()
        {
            Asset.Price startPrice;

            if (!Asset.barPrice.TryGetValue(0, out startPrice))
            {
                Log.AddLogEntry("Unable to get initial asset price for ID_Ticker " + Asset.Id_Ticker, "C:\\Logs\\HeadAndShouldersLog.txt");
                return;
            }

            maxValue = startPrice.value;
            maxDate = startPrice.date;
            maxBar = 0;

            minValue = startPrice.value;
            minDate = startPrice.date;
            minBar = 0;

            inMaxFlag = false;    

            foreach (KeyValuePair<int, Asset.Price> assetBarPrice in Asset.barPrice)
            {
                pontualZigZag(assetBarPrice.Key, false);
            }
        }

        private void historicZigZagIntraday()
        {
            DateTime analysisDay = DateTime.MinValue;
            
            foreach (KeyValuePair<int, Asset.Price> assetBarPrice in Asset.barPrice)
            {

                if (assetBarPrice.Value.date.Date > analysisDay.Date)
                {
                    analysisDay = assetBarPrice.Value.date.Date;

                    maxValue = assetBarPrice.Value.value;
                    maxDate = assetBarPrice.Value.date;
                    maxBar = 0;
                    lastPeak = -1;

                    minValue = assetBarPrice.Value.value;
                    minDate = assetBarPrice.Value.date;
                    minBar = 0;
                    lastTrough = -1;

                    inMaxFlag = false;
                }

                pontualZigZag(assetBarPrice.Key, true);
            }
        }

        public bool pontualZigZag(int bar, bool intraday)
        {

            Asset.Price assetPrice = new Asset.Price();

            if (!Asset.barPrice.TryGetValue(bar, out assetPrice))
            {
                Log.AddLogEntry("Unable to obtain asset price from historic", "C:\\Logs\\HeadAndShouldersLog.txt");
                return false;
            }
            
            if (assetPrice.value >= maxValue)
            {
                maxValue = assetPrice.value;
                maxDate = assetPrice.date;
                maxBar = bar;
            }

            if ((sideFactor * (assetPrice.value / maxValue - 1) < -HurdleRate) && inMaxFlag)
            {
                Vertex peak;
                int peakID;

                peak.bar = maxBar;
                peak.date = maxDate;
                if (intraday)
                {
                    peak.value = maxValue;
                }
                else
                {
                    peak.value = maxValue / Asset.LastIndex * Asset.LastPrice;
                }
                peak.lastVertex = lastTrough;
                
                peakID = Peaks.Count;
                lastPeak = peakID;

                try
                {
                    Peaks.Add(peakID, peak);
                }
                catch
                {
                    Log.AddLogEntry("Unable to add peak to the dictionary", "C:\\Logs\\HeadAndShouldersLog.txt");
                    return false;
                }

                maxDate = assetPrice.date;
                maxValue = assetPrice.value;
                maxBar = bar;

                minDate = assetPrice.date;
                minValue = assetPrice.value;
                minBar = bar;

                inMaxFlag = false;

            }

            if (assetPrice.value <= minValue)
            {
                minDate = assetPrice.date;
                minValue = assetPrice.value;
                minBar = bar;
            }

            if ((sideFactor * (assetPrice.value / minValue - 1) > HurdleRate) && !inMaxFlag)
            {
                Vertex trough;
                int troughID;

                trough.bar = minBar;
                trough.date = minDate;

                if (intraday)
                {
                    trough.value = minValue;
                }
                else
                {
                    trough.value = minValue / Asset.LastIndex * Asset.LastPrice;
                }

                trough.lastVertex = lastPeak;

                troughID = Troughs.Count;
                lastTrough = troughID;
                
                try
                {
                    Troughs.Add(troughID, trough);
                }
                catch
                {
                    Log.AddLogEntry("Unable to add trough to the dictionary", "C:\\Logs\\HeadAndShouldersLog.txt");
                    return false;
                }

                maxDate = assetPrice.date;
                maxValue = assetPrice.value;
                maxBar = bar;

                minDate = assetPrice.date;
                minValue = assetPrice.value;
                minBar = bar;

                inMaxFlag = true;
            }
            
            return true;
        }

    }
}
