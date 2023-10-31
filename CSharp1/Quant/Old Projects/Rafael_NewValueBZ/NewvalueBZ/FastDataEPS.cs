using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NestDLL;

namespace NewValueBZ
{
    public class FastDataEPS
    {
        public struct EPSValueObject
        {
            public DateTime RefDate;
            public DateTime KnownDate;
            public double EPSValue;
        }

        SortedDictionary<double, SortedDictionary<DateTime, EPSValueObject>> curItems = new SortedDictionary<double, SortedDictionary<DateTime, EPSValueObject>>();
        SortedDictionary<double, DateTime> LastDates = new SortedDictionary<double, DateTime>();

        private DateTime _iniDate;
        private DateTime _endDate;

        public DateTime iniDate
        {
            get { return _iniDate; }
        }

        public DateTime endDate
        {
            get { return _endDate; }
        }

        public FastDataEPS(DateTime __iniDate, DateTime __endDate)
        {
            _iniDate = __iniDate;
            _endDate = __endDate;
        }

        public void LoadTickers(double[] IdTickerArray)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string TickerList = "0";

                for (int i = 0; i < IdTickerArray.Length; i++)
                {
                    if(!curItems.ContainsKey(IdTickerArray[i]))
                        TickerList = TickerList + ", " + IdTickerArray[i];
                }

                if (TickerList != "0")
                {
                    string SQLString;

                    SQLString = "SELECT IdSecurity, RefDate, KnownDate, Value " +
                        " FROM NESTDB.dbo.Tb050_DataSeries_PTA  " +
                        " WHERE IdSecurity in (" + TickerList + ")" +
                        " AND RefDate>='" + _iniDate.ToString("yyyy-MM-dd") + "'" +
                        " AND RefDate<='" + _endDate.ToString("yyyy-MM-dd") + "'" +
                        " ORDER BY IdSecurity, RefDate";

                    DataTable dt = curConn.Return_DataTable(SQLString);

                    double prevTicker = 0;
                    SortedDictionary<DateTime, EPSValueObject> tempDictionary = new SortedDictionary<DateTime, EPSValueObject>();

                    foreach (DataRow curRow in dt.Rows)
                    {
                        if (prevTicker != Utils.ParseToDouble(curRow["IdSecurity"]))
                        {
                            if (prevTicker != 0) curItems.Add(prevTicker, tempDictionary);
                            tempDictionary = new SortedDictionary<DateTime, EPSValueObject>();
                        }

                        EPSValueObject curEPSValueObject = new EPSValueObject();

                        // If RefDate already exists, check if there is a KnownDate for that EPSValueObject and if the date that is there is older than 
                        // the date for the new item. In both cases, update with the new one. This is done because there can only be one item for each 
                        // date and we stay with the  one with the higher knowndatetime, for safety
                        if(tempDictionary.TryGetValue(Utils.ParseToDateTime(curRow["RefDate"]), out curEPSValueObject))
                        {
                            if (curEPSValueObject.KnownDate == new DateTime(1900, 01, 01) || curEPSValueObject.KnownDate < Utils.ParseToDateTime(curRow["KnownDate"]))
                            {
                                EPSValueObject newEPSValueObject = new EPSValueObject();
                                newEPSValueObject.KnownDate = Utils.ParseToDateTime(curRow["KnownDate"]);
                                newEPSValueObject.EPSValue = Utils.ParseToDouble(curRow["Value"]);
                                newEPSValueObject.RefDate = Utils.ParseToDateTime(curRow["RefDate"]);
                                tempDictionary.Remove(curEPSValueObject.RefDate);
                                tempDictionary.Add(newEPSValueObject.RefDate, newEPSValueObject);
                            }
                        }
                        else
                        {
                            curEPSValueObject.KnownDate = Utils.ParseToDateTime(curRow["KnownDate"]);
                            curEPSValueObject.EPSValue = Utils.ParseToDouble(curRow["Value"]);
                            curEPSValueObject.RefDate = Utils.ParseToDateTime(curRow["RefDate"]); 

                            tempDictionary.Add(Utils.ParseToDateTime(curRow["RefDate"]), curEPSValueObject);
                            prevTicker = Utils.ParseToDouble(curRow["IdSecurity"]);
                        }
                    }

                    if (prevTicker != 0)
                        curItems.Add(prevTicker, tempDictionary);
                
                    // Load Last Trading Dates

                    string SQLLastDates = "SELECT IdSecurity, LastTradeDate " +
                        " FROM NESTDB.dbo.Tb001_Securities" +
                        " WHERE IdSecurity in (" + TickerList + ")";

                    DataTable dtLastDates = curConn.Return_DataTable(SQLLastDates);

                    foreach (DataRow curRow in dtLastDates.Rows)
                    {
                        if (!LastDates.ContainsKey(Utils.ParseToDouble(curRow["IdSecurity"])))
                            LastDates.Add(Utils.ParseToDouble(curRow["IdSecurity"]), Utils.ParseToDateTime(curRow["LastTradeDate"]));
                    }
                }
            }
        }

        public EPSValueObject GetValue(double IdTicker, DateTime getDate, DateTime limitDate, bool ExactDateOnly)
        {
            DateTime LastDate;
            EPSValueObject noReturn;

            noReturn.EPSValue = 0;
            noReturn.RefDate = new DateTime(1900, 01, 01);
            noReturn.KnownDate = new DateTime(1900, 01, 01);

            if(LastDates.TryGetValue(IdTicker, out LastDate))
            {
                if (getDate > LastDate && LastDate> new DateTime(1900,01,01))
                {
                    return noReturn;
                }
            }

            SortedDictionary<DateTime, EPSValueObject> tempDictionary = new SortedDictionary<DateTime, EPSValueObject>();

            if (curItems.TryGetValue(IdTicker, out tempDictionary))
            {
                EPSValueObject curValue;
                if (tempDictionary.TryGetValue(getDate, out curValue))
                {
                    if (curValue.KnownDate < getDate)
                    {
                        return curValue;
                    }
                    else
                    {
                        return GetValue(IdTicker, getDate.Add(new TimeSpan(-1, 0, 0, 0)), limitDate, ExactDateOnly);
                    }
                }

                if (!ExactDateOnly)
                {
                    List<DateTime> keysList = new List<DateTime>(tempDictionary.Keys);
                    int kIndex = keysList.BinarySearch(getDate);
                    kIndex = kIndex >= 0 ? kIndex : ~kIndex;

                    if (kIndex > 0)
                    {
                        DateTime PrevDate = keysList[kIndex - 1];

                        if (tempDictionary.TryGetValue(PrevDate, out curValue))
                        {
                            DateTime curKnownDate = curValue.KnownDate;

                            if (curKnownDate == new DateTime(1900, 01, 01))
                            {
                                curKnownDate = PrevDate.Add(new TimeSpan(90, 0, 0, 0));
                            }

                            if (curKnownDate < limitDate)
                            {
                                return curValue;
                            }
                            else
                            {
                                return GetValue(IdTicker, PrevDate.Add(new TimeSpan(-1, 0, 0, 0)), limitDate, ExactDateOnly);
                            }
                        }
                    }
                }
                else
                {
                    return noReturn;
                }
            }
            return noReturn;
        }
    }
}
