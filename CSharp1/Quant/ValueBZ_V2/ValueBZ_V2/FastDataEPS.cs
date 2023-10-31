using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NestDLL;

namespace ValueBZ_V2
{
    public class FastDataEPS
    {
        public static DateTime ZeroDate = new DateTime(1900, 01, 01);

        private DateTime _IniDate;
        private DateTime _EndDate;

        public DateTime IniDate
        {
            get { return _IniDate; }            
        }        
        public DateTime EndDate
        {
            get { return _EndDate; }            
        }

        SortedDictionary<double, SortedDictionary<DateTime, SortedDictionary<DateTime, EPSValueObject>>> curItems = new SortedDictionary<double, SortedDictionary<DateTime, SortedDictionary<DateTime, EPSValueObject>>>();
        SortedDictionary<double, DateTime> LastDates = new SortedDictionary<double, DateTime>();

        public FastDataEPS(DateTime __IniDate, DateTime __EndDate)
        {
            _IniDate = __IniDate;
            _EndDate = __EndDate;
        }

        public void LoadTickers(double[] IdTickerArray)
        {            
            string TickerList = "0";

            for (int i = 0; i < IdTickerArray.Length; i++)
            {
                if (!curItems.ContainsKey(IdTickerArray[i]))
                {
                    TickerList = TickerList + ", " + IdTickerArray[i];
                }
            }

            if (TickerList != "0")
            {
                string SQLString;

                SQLString = "SELECT IDSECURITY, REFDATE, KNOWNDATE, VALUE " +
                            "FROM NESTSRV06.NESTDB.DBO.TB050_DATASERIES_PTA " +
                            "WHERE IDSECURITY IN (" + TickerList + ") " +
                            "AND REFDATE >= '" + _IniDate.ToString("yyyy-MM-dd") + "' " +
                            "AND REFDATE <= '" + _EndDate.ToString("yyyy-MM-dd") + "' " +
                            "ORDER BY IDSECURITY, REFDATE, KNOWNDATE";

                DataTable dt = new DataTable();
                
                using (newNestConn curConn = new newNestConn())
                {
                    dt = curConn.Return_DataTable(SQLString);
                }

                foreach (DataRow curRow in dt.Rows)
                {
                    double IdSecurity = double.Parse(curRow["IdSecurity"].ToString());
                    DateTime RefDate = DateTime.Parse(curRow["RefDate"].ToString());
                    DateTime KnownDate = DateTime.Parse(curRow["KnownDate"].ToString());
                    double EPSValue = double.Parse(curRow["Value"].ToString());

                    EPSValueObject curEPS = new EPSValueObject();
                    curEPS.IdSecurity = IdSecurity;
                    curEPS.RefDate = RefDate;
                    curEPS.KnownDate = KnownDate;
                    curEPS.EPSValue = EPSValue;

                    if (IdSecurity == 30187)
                    {
                        int a = 0;
                    }

                    if (!curItems.ContainsKey(IdSecurity))
                    {
                        //Create Zero EPSData 
                        EPSValueObject ZeroEPS = new EPSValueObject();
                        ZeroEPS.IdSecurity = IdSecurity;
                        ZeroEPS.RefDate = ZeroDate;
                        ZeroEPS.KnownDate = ZeroDate;
                        ZeroEPS.EPSValue = double.NaN;

                        SortedDictionary<DateTime, EPSValueObject> ZknownDic = new SortedDictionary<DateTime, EPSValueObject>();
                        ZknownDic.Add(ZeroDate, ZeroEPS);

                        SortedDictionary<DateTime, SortedDictionary<DateTime, EPSValueObject>> ZrefDic = new SortedDictionary<DateTime, SortedDictionary<DateTime, EPSValueObject>>();
                        ZrefDic.Add(ZeroDate, ZknownDic);

                        //curItems.Add(IdSecurity, ZrefDic);

                        //Insert actual data
                        SortedDictionary<DateTime, EPSValueObject> knownDic = new SortedDictionary<DateTime, EPSValueObject>();
                        knownDic.Add(KnownDate, curEPS);

                        SortedDictionary<DateTime, SortedDictionary<DateTime, EPSValueObject>> refDic = new SortedDictionary<DateTime, SortedDictionary<DateTime, EPSValueObject>>();
                        refDic.Add(RefDate, knownDic);

                        curItems.Add(IdSecurity, refDic);
                        //curItems[IdSecurity].Add(RefDate, knownDic);
                    }
                    else
                    {
                        if (!curItems[IdSecurity].ContainsKey(RefDate))
                        {
                            SortedDictionary<DateTime, EPSValueObject> knownDic = new SortedDictionary<DateTime, EPSValueObject>();
                            knownDic.Add(KnownDate, curEPS);

                            curItems[IdSecurity].Add(RefDate, knownDic);
                        }
                        else
                        {
                            if (!curItems[IdSecurity][RefDate].ContainsKey(KnownDate))
                            {
                                curItems[IdSecurity][RefDate].Add(KnownDate, curEPS);
                            }
                            else
                            {
                                curItems[IdSecurity][RefDate][KnownDate] = curEPS;
                            }
                        }
                    }
                }

                // Load Last Trading Dates

                string SQLLastDates = "SELECT IdSecurity, LastTradeDate " +
                    " FROM NESTSRV06.NESTDB.dbo.Tb001_Securities" +
                    " WHERE IdSecurity in (" + TickerList + ")";

                DataTable dtLastDates = new DataTable();

                using (newNestConn curConn = new newNestConn())
                {
                    dtLastDates = curConn.Return_DataTable(SQLLastDates);
                }

                foreach (DataRow curRow in dtLastDates.Rows)
                {
                    if (!LastDates.ContainsKey(Utils.ParseToDouble(curRow["IdSecurity"])))
                        LastDates.Add(Utils.ParseToDouble(curRow["IdSecurity"]), Utils.ParseToDateTime(curRow["LastTradeDate"]));
                }
            }
        }

        public EPSValueObject GetValue(double IdTicker, DateTime getDate, DateTime limitDate, bool ExactDateOnly)
        {
            DateTime LastDate;
            EPSValueObject noReturn = new EPSValueObject();
            noReturn.IdSecurity = double.NaN;
            noReturn.EPSValue = double.NaN;
            noReturn.RefDate = ZeroDate;
            noReturn.KnownDate = ZeroDate;

            if (LastDates.TryGetValue(IdTicker, out LastDate))
            {
                if (getDate > LastDate && LastDate > ZeroDate)
                {
                    return noReturn;
                }
            }

            SortedDictionary<DateTime, SortedDictionary<DateTime, EPSValueObject>> tempRefDic = new SortedDictionary<DateTime, SortedDictionary<DateTime, EPSValueObject>>();

            if (curItems.TryGetValue(IdTicker, out tempRefDic))
            {
                List<DateTime> refKeyList = new List<DateTime>(tempRefDic.Keys);
                int refKIndex = refKeyList.BinarySearch(getDate);
                refKIndex = (refKIndex >= 0 ? refKIndex : ~refKIndex);

                if (refKIndex > 0)
                {
                    DateTime RefDate = refKeyList[refKIndex - 1];

                    SortedDictionary<DateTime, EPSValueObject> tempKnownDic = new SortedDictionary<DateTime, EPSValueObject>();

                    if (tempRefDic.TryGetValue(RefDate, out tempKnownDic))
                    {
                        List<DateTime> knownKeyList = new List<DateTime>(tempKnownDic.Keys);
                        int knownKIndex = knownKeyList.BinarySearch(limitDate);
                        int teste = ~knownKIndex;
                        knownKIndex = (knownKIndex >= 0 ? knownKIndex : ~knownKIndex);

                        if (knownKIndex > 0)
                        {
                            DateTime KnownDate = knownKeyList[knownKIndex - 1];
                            DateTime auxKnownDate = KnownDate;

                            if (KnownDate == ZeroDate)
                            {
                                auxKnownDate = RefDate.AddDays(60);
                            }

                            if (auxKnownDate < limitDate)
                            {
                                return tempKnownDic[KnownDate];
                            }
                            else
                            {
                                return GetValue(IdTicker, getDate.AddDays(-1), limitDate, ExactDateOnly);
                            }
                        }
                        else
                        {
                            return GetValue(IdTicker, getDate.AddDays(-1), limitDate, ExactDateOnly);
                        }
                    }
                }
            }

            return noReturn;            
        }
    }

    public class EPSValueObject
    {
        private double _IdSecurity;
        private string _Ticker;
        private DateTime _RefDate;
        private DateTime _KnownDate;
        private double _EPSValue;
        
        public double IdSecurity
        {
            get { return _IdSecurity; }
            set { _IdSecurity = value; }
        }
        public string Ticker
        {
            get { return _Ticker; }
            set { _Ticker = value; }
        }        
        public DateTime RefDate
        {
            get { return _RefDate; }
            set { _RefDate = value; }
        }
        public DateTime KnownDate
        {
            get { return _KnownDate; }
            set { _KnownDate = value; }
        }
        public double EPSValue
        {
            get { return _EPSValue; }
            set { _EPSValue = value; }
        }
    }
}
