using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NestDLL;

namespace NFastData
{
    public class FastData
    {
        protected SortedDictionary<double, SortedDictionary<DateTime, double>> curItems = new SortedDictionary<double, SortedDictionary<DateTime, double>>();
        protected SortedDictionary<double, DateTime> LastDates = new SortedDictionary<double, DateTime>();

        private int _PriceType;
        private int _Source;
        private DateTime _iniDate;
        private DateTime _endDate;
        private double iniTicker = 0;

        public int PriceType
        {
            get { return _PriceType; }
        }

        public int Source
        {
            get { return _Source; }
        }

        public DateTime iniDate
        {
            get { return _iniDate; }
        }

        public DateTime endDate
        {
            get { return _endDate; }
        }

        public FastData(int __PriceType, DateTime __iniDate, DateTime __endDate, int __Source)
        {
            _PriceType = __PriceType;
            _iniDate = __iniDate;
            _endDate = __endDate;
            _Source = __Source;
        }

        public void LoadTickers(double[] IdTickerArray)
        {
            LoadTickers(IdTickerArray, int.MinValue, _Source);
        }

        public void LoadTickers(double[] IdTickerArray, int _IdComposite)
        {
            LoadTickers(IdTickerArray, _IdComposite, _Source);
        }

        public void LoadTickers(double[] IdTickerArray, int _IdComposite, int source)
        {
            bool useOldDB = (PriceType == 999 ? true : false);

            using (newNestConn curConn = new newNestConn(useOldDB))
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

                    if (PriceType == 999)
                    {
                        SQLString = "SELECT IdSecurity AS Id_Ativo, DATAREF AS Data_Hora_Reg, CAST(MIN(KnowDateTime)+2 as float) AS Valor " +
                            " FROM (SELECT CODCVM, DATAREF, KnowDateTime FROM NESTOTHER.dbo.Tb021_CVM_ITR_CTL UNION ALL SELECT CODCVM, DATAREF, KnowDateTime FROM NESTOTHER.dbo.Tb021_CVM_DFP_CTL) A " +
                            " LEFT JOIN NESTSRV06.NESTDB.dbo.Tb000_Issuer WITH(NOLOCK)B " +
                            " ON A.CODCVM=B.CODCVM " +
                            " LEFT JOIN NESTSRV06.NESTDB.dbo.Tb001_Securities C " +
                            " ON C.IdIssuer=B.IdIssuer " +
                            " WHERE IdSecurity in (" + TickerList + ")" +
                            " AND DATAREF>='" + _iniDate.ToString("yyyy-MM-dd") + "'" +
                            " AND DATAREF<='" + _endDate.ToString("yyyy-MM-dd") + "'" +
                            " GROUP BY IdSecurity, DATAREF " +
                            " ORDER BY IdSecurity, DATAREF";
                    }
                    else if (PriceType == 998)
                    {
                        if (_IdComposite != int.MinValue)
                        {
                            SQLString = "SELECT ID_TICKER_COMPONENT AS IdSecurity, DATE_REF as SrDate, WEIGHT as SrValue" +
                                        " FROM NESTSRV06.NESTDB.DBO.TB023_SECURITIES_COMPOSITION WITH(NOLOCK)" +
                                        " WHERE ID_TICKER_COMPOSITE = " + _IdComposite.ToString() +
                                        " AND ID_TICKER_COMPONENT IN (" + TickerList + ")" +
                                        " AND DATE_REF>='" + _iniDate.ToString("yyyy-MM-dd") + "'" +
                                        " AND DATE_REF<='" + _endDate.ToString("yyyy-MM-dd") + "'" +
                                        " ORDER BY ID_TICKER_COMPONENT, DATE_REF";
                        }
                        else
                        {
                            throw new Exception("IdComposite is not set. Unable to load weight data");
                        }
                    }
                    else
                    {
                        SQLString = "SELECT IdSecurity, SrDate, SrValue " +
                            " FROM NESTSRV06.NESTDB.DBO." + NestDLL.Utils.GetTableName(IdTickerArray[0]) +
                            " WHERE IdSecurity in (" + TickerList + ")" +
                            " AND SrType=" + _PriceType.ToString() +
                            " AND Source IN(" + source.ToString() + ")" +
                            " AND SrDate>='" + _iniDate.ToString("yyyy-MM-dd") + "'" +
                            " AND SrDate<='" + _endDate.ToString("yyyy-MM-dd") + "'" +
                            " ORDER BY IdSecurity, SrDate";
                    }
                    DataTable dt = curConn.Return_DataTable(SQLString);

                    double prevTicker = 0;
                    SortedDictionary<DateTime, double> tempDictionary = new SortedDictionary<DateTime, double>();

                    foreach (DataRow curRow in dt.Rows)
                    {
                        if (prevTicker != Utils.ParseToDouble(curRow["IdSecurity"]))
                        {
                            if (prevTicker != 0) curItems.Add(prevTicker, tempDictionary);
                            if (iniTicker == 0) iniTicker = prevTicker;
                            tempDictionary = new SortedDictionary<DateTime, double>();
                        }

                        tempDictionary.Add(Utils.ParseToDateTime(curRow["SrDate"]), Utils.ParseToDouble(curRow["SrValue"]));
                        prevTicker = Utils.ParseToDouble(curRow["IdSecurity"]);
                    }

                    if (prevTicker != 0)
                    {
                        curItems.Add(prevTicker, tempDictionary);
                        if (iniTicker == 0) iniTicker = prevTicker;
                    }
                
                    // Load Last Trading Dates

                    string SQLLastDates = "SELECT IdSecurity, LastTradeDate " +
                        " FROM NESTSRV06.NESTDB.dbo.Tb001_Securities" +
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
        
        public double[] GetValue(double IdTicker, DateTime getDate, bool ExactDateOnly)
        {
            DateTime LastDate;
            double[] noReturn = { 1, 0 };

            if(LastDates.TryGetValue(IdTicker, out LastDate))
            {
                if (getDate > LastDate && LastDate> new DateTime(1900,01,01))
                {
                    return noReturn;
                }
            }
            
            SortedDictionary<DateTime, double> tempDictionary = new SortedDictionary<DateTime, double>();

            if (curItems.TryGetValue(IdTicker, out tempDictionary))
            {
                double curValue = 0;
                if (tempDictionary.TryGetValue(getDate, out curValue))
                {
                    double[] tempReturn = { getDate.ToOADate(), curValue };
                    return tempReturn;
                }
                else if (!ExactDateOnly)
                {
                    List<DateTime> keysList = new List<DateTime>(tempDictionary.Keys);
                    int kIndex = keysList.BinarySearch(getDate);
                    kIndex = kIndex >= 0 ? kIndex : ~kIndex;

                    if (kIndex > 0)
                    {
                        DateTime PrevDate = keysList[kIndex - 1];

                        if (tempDictionary.TryGetValue(PrevDate, out curValue))
                        {
                            double[] tempReturn = { PrevDate.ToOADate(), curValue };
                            return tempReturn;
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

        public DateTime PrevDate(DateTime getDate)
        {
            SortedDictionary<DateTime, double> tempDictionary = new SortedDictionary<DateTime, double>();

            curItems.TryGetValue(iniTicker, out tempDictionary);
            
            List<DateTime> keysList = new List<DateTime>(tempDictionary.Keys);
            int kIndex = keysList.BinarySearch(getDate);
            kIndex = kIndex >= 0 ? kIndex : ~kIndex;

            if (kIndex > 0)
            {
                DateTime PrevDate = keysList[kIndex - 1];

                return PrevDate;
            }
            else
            {
                return new DateTime(1900, 01, 01);
            }
        }

        public DateTime PrevDate(DateTime getDate, double IdTicker)
        {
            SortedDictionary<DateTime, double> tempDictionary = new SortedDictionary<DateTime, double>();

            curItems.TryGetValue(IdTicker, out tempDictionary);

            List<DateTime> keysList = new List<DateTime>(tempDictionary.Keys);
            int kIndex = keysList.BinarySearch(getDate);
            kIndex = kIndex >= 0 ? kIndex : ~kIndex;

            if (kIndex > 0)
            {
                DateTime PrevDate = keysList[kIndex - 1];

                return PrevDate;
            }
            else
            {
                return new DateTime(1900, 01, 01);
            }
        }

        public DateTime NextDate(DateTime getDate)
        {
            SortedDictionary<DateTime, double> tempDictionary = new SortedDictionary<DateTime, double>();

            curItems.TryGetValue(iniTicker, out tempDictionary);

            List<DateTime> keysList = new List<DateTime>(tempDictionary.Keys);
            int kIndex = keysList.BinarySearch(getDate);
            //kIndex = kIndex >= 0 ? kIndex : ~kIndex;

            if (kIndex >= 0)
            {
                DateTime NextDate = new DateTime(1900,01,01);

                if (!(kIndex >= (keysList.Count - 1)))
                {
                    NextDate = keysList[kIndex + 1];
                }

                return NextDate;
            }
            else
            {
                DateTime NextDate = new DateTime(1900, 01, 01);
                
                if (!(~kIndex > (keysList.Count - 1)))
                {
                    NextDate = keysList[~kIndex];                
                }

                return NextDate;
            }
        }

        public DateTime NextDate(DateTime getDate, double IdTicker)
        {
            SortedDictionary<DateTime, double> tempDictionary = new SortedDictionary<DateTime, double>();

            curItems.TryGetValue(IdTicker, out tempDictionary);

            List<DateTime> keysList = new List<DateTime>(tempDictionary.Keys);
            int kIndex = keysList.BinarySearch(getDate);
            //kIndex = kIndex >= 0 ? kIndex : ~kIndex;

            if (kIndex >= 0)
            {
                DateTime NextDate = new DateTime(1900, 01, 01);

                if (!(kIndex >= (keysList.Count - 1)))
                {
                    NextDate = keysList[kIndex + 1];
                }

                return NextDate;
            }
            else
            {
                DateTime NextDate = new DateTime(1900, 01, 01);

                if (!(~kIndex > (keysList.Count - 1)))
                {
                    NextDate = keysList[~kIndex];
                }

                return NextDate;
            }
        }
    }
}
