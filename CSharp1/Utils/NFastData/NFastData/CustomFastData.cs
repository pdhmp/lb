using System;
using System.Collections.Generic;
using System.Text;

namespace NFastData
{
    public class CustomFastData : FastData
    {

        public CustomFastData(int __PriceType, DateTime __iniDate, DateTime __endDate, int __Source)
            :base(__PriceType,__iniDate,__endDate,__Source)
        {            
        }

        public void InsertValue(double IdSecurity, DateTime curDate, double curValue)
        {
            SortedDictionary<DateTime, double> tempDictionary = new SortedDictionary<DateTime, double>();

            if (curItems.TryGetValue(IdSecurity, out tempDictionary))
            {
                if (tempDictionary.ContainsKey(curDate))
                {
                    tempDictionary[curDate] = curValue;
                }
                else
                {
                    tempDictionary.Add(curDate, curValue);
                }
            }
            else
            {
                tempDictionary = new SortedDictionary<DateTime, double>();
                tempDictionary.Add(curDate, curValue);
                curItems.Add(IdSecurity, tempDictionary);
            }
        }
    }
}
