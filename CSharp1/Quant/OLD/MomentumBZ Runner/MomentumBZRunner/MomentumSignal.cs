using System;
using System.Collections.Generic;
using System.Text;
using NestQuant.Common;

namespace MomentumBZRunner
{
    public class MomentumSignal
    { 
        public  SortedDictionary<int, double> DayPrices = new SortedDictionary<int,double>();
        private SortedDictionary<int, double> Weights = new SortedDictionary<int,double>();
        private SortedDictionary<int, double> WindowIniPrice = new SortedDictionary<int,double>();
        private double[] SignalReference;

        private int _Signal = 0;
        public int Signal
        {
            get { return _Signal; }
        }

        

        public MomentumSignal(MomentumSectorInfo Info)
        {
            foreach (KeyValuePair<int, double> price in Info.DayPrices)
            {
                DayPrices.Add(price.Key, price.Value);
            }

            foreach (KeyValuePair<int, double> weight in Info.Weights)
            {
                Weights.Add(weight.Key, weight.Value);
            }

            foreach (KeyValuePair<int, double> iniPrice in Info.WindowIniPrice)
            {
                WindowIniPrice.Add(iniPrice.Key, iniPrice.Value);
            }

            SignalReference = new double[Info.SignalReference.Length];

            for (int i = 0; i < Info.SignalReference.Length; i++)
            {
                SignalReference[i] = Info.SignalReference[i];
            }
        }

        public void UpdatePrices(Dictionary<int, double> prices)
        {
            foreach (KeyValuePair<int, double> tickerPrice in prices)
            {
                DayPrices[tickerPrice.Key] = tickerPrice.Value;
            }
        }

        public void GenerateSignal()
        {
            int signal = 0;

            SignalReference[SignalReference.Length -1] = SectorDayRollingPerformance();

            double[] MedianReference = new double[SignalReference.Length];

            SignalReference.CopyTo(MedianReference, 0);

            double median = Utils.calcMedian(MedianReference, false);

            if (median < 0)
            {
                signal = -1;
            }
            else if (median > 0)
            {
                signal = 1;
            }
            else
            {
                signal = 0;
            }

            _Signal = signal;
        }

        private double SectorDayRollingPerformance()
        {
            double performance = 0;

            foreach (KeyValuePair<int, double> tickerPrice in DayPrices)
            {
                double dayPrice = tickerPrice.Value;
                double windowPrice = WindowIniPrice[tickerPrice.Key];
                double weight = Weights[tickerPrice.Key];

                if (double.IsNaN(dayPrice) || double.IsNaN(windowPrice))
                {
                    if ((weight != 0)&&(!double.IsNaN(weight)))
                    {
                        throw new System.Exception("Weighted ticker without price: " + tickerPrice.Key);
                    }
                    else
                    {
                        dayPrice = 1;
                        windowPrice = 1;
                        weight = 0;
                    }
                }

                performance = performance + weight * (dayPrice / windowPrice - 1);
            }

            return performance;
        }
    }
}
