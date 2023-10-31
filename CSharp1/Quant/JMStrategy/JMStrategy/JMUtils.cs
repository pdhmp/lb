using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMStrategy
{
    public class JMUtils
    {
        public static int MaxDays = 120;
        public static int IdSecurityCDI = 5050; // 5050 = CDITAXACETIP        

        public static double VolGarmanKlassYZ(double[] Open, double[] Close, double[] High, double[] Low, int Days)
        {
            double auxSum = 0;

            for (int i = 0; i < Days; i++)
            {
                double fator1 = Math.Pow(Math.Log(Open[i] / Close[i + 1]), 2);
                double fator2 = (1.0 / 2.0) * Math.Pow(Math.Log(High[i] / Low[i]), 2);
                double fator3 = (2.0 * Math.Log(2.0) - 1.0) * Math.Pow(Math.Log(Close[i] / Open[i]), 2);

                auxSum = auxSum + fator1 + fator2 - fator3;
            }

            double volatility = Math.Sqrt(auxSum / Days);

            return volatility;
        }

        public static double Stochastic(double[] Last, int Days)
        {
            double retValue = 0;
            double dist = 0;
            double range = 0;
            double lastPrice = Last[0];

            dist = lastPrice - Min(Last, Days);
            range = Max(Last, Days) - Min(Last, Days);
            if (range != 0)
            {
                retValue = dist / range;
            }

            return retValue;            
        }

        public static double Max(double[] data, int size)
        {
            double max = double.MinValue;

            for (int i = 0; i < size; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                }
            }

            return max;
        }

        public static double Min(double[] data, int size)
        {
            double min = double.MaxValue;

            for (int i = 0; i < size; i++)
            {
                if (data[i] < min)
                {
                    min = data[i];
                }
            }

            return min;
        }
        
    }

    public class JM2ColItem
    {
        private string _Item;

        public string Item
        {
            get { return _Item; }
            set { _Item = value; }
        }
        private string _Value;

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public JM2ColItem(string sItem, string sValue)
        {
            _Item = sItem;
            _Value = sValue;
        }
    }
}
