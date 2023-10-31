using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using NFastData;
using NestDLL;
using System.Data;

namespace PriceCalculator
{
    class PriceCalculator
    {
        private FastData Last30Vwap;
        private FastData Last30Volume;
        private FastData CloseAucPrice;
        private FastData CloseAucVolume;
        private FastData First330Vwap;
        private FastData First330Volume;

        private SortedDictionary<double, SortedDictionary<DateTime, double>> Last30Average = new SortedDictionary<double, SortedDictionary<DateTime, double>>();
        private SortedDictionary<double, SortedDictionary<DateTime, double>> CloseAverage = new SortedDictionary<double, SortedDictionary<DateTime, double>>();
        private SortedDictionary<double, SortedDictionary<DateTime, double>> First330Average = new SortedDictionary<double, SortedDictionary<DateTime, double>>();

        private SortedDictionary<double, SortedDictionary<DateTime, double>> Last30Median = new SortedDictionary<double, SortedDictionary<DateTime, double>>();
        private SortedDictionary<double, SortedDictionary<DateTime, double>> CloseMedian = new SortedDictionary<double, SortedDictionary<DateTime, double>>();
        private SortedDictionary<double, SortedDictionary<DateTime, double>> First330Median = new SortedDictionary<double, SortedDictionary<DateTime, double>>();


        //private DateTime IniDate = (new DateTime(2011, 12, 20)).AddDays(-15);
        private DateTime IniDate = DateTime.Today.AddDays(-30);

        public void CalculateAverage()
        {
            DateTime MinDate = IniDate.AddDays(15);

            Last30Vwap = new FastData(331, IniDate, DateTime.Now, 22);
            Last30Volume = new FastData(330, IniDate, DateTime.Now, 22);
            CloseAucPrice = new FastData(1, IniDate, DateTime.Now, 1);
            CloseAucVolume = new FastData(310, IniDate, DateTime.Now, 22);
            First330Vwap = new FastData(351, IniDate, DateTime.Now, 22);
            First330Volume = new FastData(350, IniDate, DateTime.Now, 22);

            DataTable tempTable = new DataTable();

            using (newNestConn curConn = new newNestConn())
            {
                string SQLExpression = "SELECT distinct IdSecurity" +
                                       " FROM NESTDb.dbo.Tb050_Preco_Acoes_Onshore (NOLOCK) " +                                       
                                       " WHERE SrType=331 ORDER BY IDSECURITY";

                tempTable = curConn.Return_DataTable(SQLExpression);
            }

            double[] curTickers = new double[tempTable.Rows.Count + 1];

            for (int i = 0; i < curTickers.Length - 1; i++)
            {
                curTickers[i] = NestDLL.Utils.ParseToDouble(tempTable.Rows[i][0]);
                Last30Average.Add(curTickers[i], new SortedDictionary<DateTime, double>());
                Last30Median.Add(curTickers[i], new SortedDictionary<DateTime, double>());
                CloseAverage.Add(curTickers[i], new SortedDictionary<DateTime, double>());
                CloseMedian.Add(curTickers[i], new SortedDictionary<DateTime, double>());
                First330Average.Add(curTickers[i], new SortedDictionary<DateTime, double>());
                First330Median.Add(curTickers[i], new SortedDictionary<DateTime, double>());
            }

            curTickers[curTickers.Length - 1] = 1073;

            Last30Vwap.LoadTickers(curTickers);
            Last30Volume.LoadTickers(curTickers);
            CloseAucPrice.LoadTickers(curTickers);
            CloseAucVolume.LoadTickers(curTickers);
            First330Vwap.LoadTickers(curTickers);
            First330Volume.LoadTickers(curTickers);


            //DateTime curDate = CloseAucPrice.PrevDate(DateTime.Now);
            //if (DateTime.Now.Month != curDate.Month) { curDate = CloseAucPrice.PrevDate(curDate); }

            DateTime prevDate = DateTime.Today;

            while (prevDate > MinDate)
            {
                DateTime calcDate = CloseAucPrice.PrevDate(prevDate);
                DateTime curDate = CloseAucPrice.PrevDate(prevDate);                
                if (prevDate.Month != curDate.Month) { curDate = CloseAucPrice.PrevDate(curDate); }

                DateTime Dminus1 = CloseAucPrice.PrevDate(curDate);
                if (curDate.Month != Dminus1.Month) { Dminus1 = CloseAucPrice.PrevDate(Dminus1); }

                DateTime Dminus2 = CloseAucPrice.PrevDate(Dminus1);
                if (Dminus1.Month != Dminus2.Month) { Dminus2 = CloseAucPrice.PrevDate(Dminus2); }

                DateTime Dminus3 = CloseAucPrice.PrevDate(Dminus2);
                if (Dminus2.Month != Dminus3.Month) { Dminus3 = CloseAucPrice.PrevDate(Dminus3); }

                DateTime Dminus4 = CloseAucPrice.PrevDate(Dminus3);
                if (Dminus3.Month != Dminus4.Month) { Dminus4 = CloseAucPrice.PrevDate(Dminus4); }


                for (int i = 0; i < curTickers.Length - 1; i++)
                {

                    //Last 30 minutes

                    double[] last30Arr = new double[5];

                    last30Arr[0] = Last30Volume.GetValue(curTickers[i], curDate, true)[1] * (Last30Vwap.GetValue(curTickers[i], curDate, true)[1]);
                    last30Arr[1] = Last30Volume.GetValue(curTickers[i], Dminus1, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus1, true)[1]);
                    last30Arr[2] = Last30Volume.GetValue(curTickers[i], Dminus2, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus2, true)[1]);
                    last30Arr[3] = Last30Volume.GetValue(curTickers[i], Dminus3, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus3, true)[1]);
                    last30Arr[4] = Last30Volume.GetValue(curTickers[i], Dminus4, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus4, true)[1]);

                    Array.Sort(last30Arr);

                    double curLast30Avg = (1.0 / 5) * ((last30Arr[0]) + (last30Arr[1]) + (last30Arr[2]) + (last30Arr[3]) + (last30Arr[4]));
                    Last30Average[curTickers[i]].Add(calcDate, curLast30Avg);                   

                    Last30Median[curTickers[i]].Add(calcDate, last30Arr[2]);
                    
                          

                    //CloseAverage

                    double[] CloseArr = new double[5];

                    CloseArr[0] = CloseAucVolume.GetValue(curTickers[i], curDate, true)[1] * (CloseAucPrice.GetValue(curTickers[i], curDate, true)[1]);
                    CloseArr[1] = CloseAucVolume.GetValue(curTickers[i], Dminus1, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus1, true)[1]);
                    CloseArr[2] = CloseAucVolume.GetValue(curTickers[i], Dminus2, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus2, true)[1]);
                    CloseArr[3] = CloseAucVolume.GetValue(curTickers[i], Dminus3, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus3, true)[1]);
                    CloseArr[4] = CloseAucVolume.GetValue(curTickers[i], Dminus4, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus4, true)[1]);

                    Array.Sort(CloseArr);

                    double curCloseAvg = (1.0 / 5) * ((CloseArr[0]) + (CloseArr[1]) + (CloseArr[2]) + (CloseArr[3]) + (CloseArr[4]));
                    CloseAverage[curTickers[i]].Add(calcDate, curCloseAvg);

                    CloseMedian[curTickers[i]].Add(calcDate, CloseArr[2]);


                    //First330Avg

                    double[] F330Arr = new double[5];

                    F330Arr[0] = First330Volume.GetValue(curTickers[i], curDate, true)[1] * (First330Vwap.GetValue(curTickers[i], curDate, true)[1]);
                    F330Arr[1] = First330Volume.GetValue(curTickers[i], Dminus1, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus1, true)[1]);
                    F330Arr[2] = First330Volume.GetValue(curTickers[i], Dminus2, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus2, true)[1]);
                    F330Arr[3] = First330Volume.GetValue(curTickers[i], Dminus3, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus3, true)[1]);
                    F330Arr[4] = First330Volume.GetValue(curTickers[i], Dminus4, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus4, true)[1]);

                    Array.Sort(F330Arr);

                    double curFirst330Avg = (1.0 / 5) * ((F330Arr[0]) + (F330Arr[1]) + (F330Arr[2]) + (F330Arr[3]) + (F330Arr[4]));
                    First330Average[curTickers[i]].Add(calcDate, curFirst330Avg);

                    First330Median[curTickers[i]].Add(calcDate, F330Arr[2]);
                }

                prevDate = calcDate;                
            }


            using (newNestConn curConn = new newNestConn())
            {
                foreach (KeyValuePair<double, SortedDictionary<DateTime, double>> kvp1 in First330Average)
                {
                    foreach (KeyValuePair<DateTime, double> kvp2 in kvp1.Value)
                    {
                        if (kvp2.Value != 0)
                        {
                            string SQLExpression = "EXEC NESTDB.dbo.Proc_Insert_Price " +
                                                   "@IdSecurity = " + kvp1.Key.ToString() + ", " +
                                                   "@DATA = '" + kvp2.Key.ToString("yyyyMMdd") + "' , " +
                                                   "@SrValue = " + kvp2.Value.ToString().Replace(',','.') + ", " +
                                                   "@SrType = 352, " +
                                                   "@SOURCE = 22, " +
                                                   "@AUTOMATED = 1";

                            //curConn.ExecuteNonQuery(SQLExpression);
                        }
                    }
                }

                foreach (KeyValuePair<double, SortedDictionary<DateTime, double>> kvp1 in CloseAverage)
                {
                    foreach (KeyValuePair<DateTime, double> kvp2 in kvp1.Value)
                    {
                        if (kvp2.Value != 0)
                        {
                            string SQLExpression = "EXEC NESTDB.dbo.Proc_Insert_Price " +
                                                   "@IdSecurity = " + kvp1.Key.ToString() + ", " +
                                                   "@DATA = '" + kvp2.Key.ToString("yyyyMMdd") + "' , " +
                                                   "@SrValue = " + kvp2.Value.ToString().Replace(',', '.') + ", " +
                                                   "@SrType = 313, " +
                                                   "@SOURCE = 22, " +
                                                   "@AUTOMATED = 1";

                            //curConn.ExecuteNonQuery(SQLExpression);
                        }
                    }
                }

                foreach (KeyValuePair<double, SortedDictionary<DateTime, double>> kvp1 in Last30Average)
                {
                    foreach (KeyValuePair<DateTime, double> kvp2 in kvp1.Value)
                    {
                        if (kvp2.Value != 0)
                        {
                            string SQLExpression = "EXEC NESTDB.dbo.Proc_Insert_Price " +
                                                   "@IdSecurity = " + kvp1.Key.ToString() + ", " +
                                                   "@DATA = '" + kvp2.Key.ToString("yyyyMMdd") + "' , " +
                                                   "@SrValue = " + kvp2.Value.ToString().Replace(',', '.') + ", " +
                                                   "@SrType = 332, " +
                                                   "@SOURCE = 22, " +
                                                   "@AUTOMATED = 1";

                            //curConn.ExecuteNonQuery(SQLExpression);
                        }
                    }
                }
                foreach (KeyValuePair<double, SortedDictionary<DateTime, double>> kvp1 in Last30Median)
                {
                    foreach (KeyValuePair<DateTime, double> kvp2 in kvp1.Value)
                    {
                        if (kvp2.Value != 0)
                        {
                            string SQLExpression = "EXEC NESTDB.dbo.Proc_Insert_Price " +
                                                   "@IdSecurity = " + kvp1.Key.ToString() + ", " +
                                                   "@DATA = '" + kvp2.Key.ToString("yyyyMMdd") + "' , " +
                                                   "@SrValue = " + kvp2.Value.ToString().Replace(',', '.') + ", " +
                                                   "@SrType = 333, " +
                                                   "@SOURCE = 22, " +
                                                   "@AUTOMATED = 1";

                            curConn.ExecuteNonQuery(SQLExpression);
                        }
                    }
                }
                foreach (KeyValuePair<double, SortedDictionary<DateTime, double>> kvp1 in CloseMedian)
                {
                    foreach (KeyValuePair<DateTime, double> kvp2 in kvp1.Value)
                    {
                        if (kvp2.Value != 0)
                        {
                            string SQLExpression = "EXEC NESTDB.dbo.Proc_Insert_Price " +
                                                   "@IdSecurity = " + kvp1.Key.ToString() + ", " +
                                                   "@DATA = '" + kvp2.Key.ToString("yyyyMMdd") + "' , " +
                                                   "@SrValue = " + kvp2.Value.ToString().Replace(',', '.') + ", " +
                                                   "@SrType = 315, " +
                                                   "@SOURCE = 22, " +
                                                   "@AUTOMATED = 1";

                            curConn.ExecuteNonQuery(SQLExpression);
                        }
                    }
                }
                foreach (KeyValuePair<double, SortedDictionary<DateTime, double>> kvp1 in First330Median)
                {
                    foreach (KeyValuePair<DateTime, double> kvp2 in kvp1.Value)
                    {
                        if (kvp2.Value != 0)
                        {
                            string SQLExpression = "EXEC NESTDB.dbo.Proc_Insert_Price " +
                                                   "@IdSecurity = " + kvp1.Key.ToString() + ", " +
                                                   "@DATA = '" + kvp2.Key.ToString("yyyyMMdd") + "' , " +
                                                   "@SrValue = " + kvp2.Value.ToString().Replace(',', '.') + ", " +
                                                   "@SrType = 353, " +
                                                   "@SOURCE = 22, " +
                                                   "@AUTOMATED = 1";

                            curConn.ExecuteNonQuery(SQLExpression);
                        }
                    }
                }
            }
        }        
    }

    class IndexCalculator
    {
        private FastData DBInterface;
        private FastData Last; 
        private FastData TR_DayBBG;
        private FastData TR_IndexBBG;
        private FastData TR_DayNest;
        private FastData TR_IndexNest;

        public IndexCalculator(int srType, DateTime IniDate, int Source)
        {            
            DBInterface = new FastData(srType, IniDate, DateTime.Today, Source);
            Last = new FastData(1, IniDate, DateTime.Today, 1);
            TR_DayBBG = new FastData(100, IniDate, DateTime.Today, 1);
            TR_IndexBBG = new FastData(101, IniDate, DateTime.Today, 1);
            TR_DayNest = new FastData(100, IniDate, DateTime.Today, 7);
            TR_IndexNest = new FastData(101, IniDate, DateTime.Today, 7);
        }

        private List<int> GetIndexes()
        {
            DataTable indexesTable;

            List<int> Indexes = new List<int>();

            string SQL_Indexes =
                "SELECT distinct(Id_Ticker_Component) " +
                "FROM NESTDB.dbo.Tb023_Securities_CompositiON (nolock) " +
                "     JOIN  " +
                "     NESTDB.dbo.Tb001_Securities S  " +
                "     ON Id_Ticker_Component = idsecurity  " +
                "WHERE  Id_Ticker_Composite = 21350	 ";

            using (newNestConn conn = new newNestConn())
            {
                indexesTable = conn.Return_DataTable(SQL_Indexes);
            }

            for (int i = 0; i < indexesTable.Rows.Count; i++)
            {
                Indexes.Add((int)indexesTable.Rows[i][0]);
            }

            return Indexes;
        }

        private List<int> GetIndexComponents(int index)
        {
            DataTable componentsTable;

            List<int> Components = new List<int>();

            string SQL_Indexes =
                "SELECT distinct(Id_Ticker_Component) " +
                "FROM NESTDB.dbo.Tb023_Securities_CompositiON (nolock) " +
                "     JOIN  " +
                "     NESTDB.dbo.Tb001_Securities S  " +
                "     ON Id_Ticker_Component = idsecurity  " +
                "WHERE  Id_Ticker_Composite = " + index;

            using (newNestConn conn = new newNestConn())
            {
                componentsTable = conn.Return_DataTable(SQL_Indexes);
            }

            for (int i = 0; i < componentsTable.Rows.Count; i++)
            {
                Components.Add((int)componentsTable.Rows[i][0]);
            }

            return Components;
        }

        private Dictionary<int, Dictionary<DateTime, double>> GetIndexComponentsWeights(int index)
        {
            DataTable indexComponentsWeightsTable;

            Dictionary<int, Dictionary<DateTime, double>> IndexWeights = new Dictionary<int, Dictionary<DateTime, double>>();
            Dictionary<DateTime, double> TickerWeights = new Dictionary<DateTime, double>();
            
            int ticker = 0;
            double weight = 0;
            int prevTicker = 0;            
            DateTime refDate = new DateTime();

            string SQL_Indexes_Components =
                "SELECT * " +
                "FROM " +
                "( " +
                "    SELECT distinct(SC.Id_Ticker_Component) as IdTicker,S.NestTicker as TickerName,Id_Ticker_Composite as IdIndex, S2.NestTicker as IndexName,Weight,Date_Ref " +
                "    FROM NESTDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "         JOIN  " +
                "         NESTDB.dbo.Tb001_Securities S " +
                "         ON Id_Ticker_Component = S.idsecurity " +
                "         JOIN " +
                "         dbo.Tb001_Securities S2 " +
                "         ON Id_Ticker_Composite = S2.idsecurity " +
                "    WHERE SC.Id_Ticker_Composite =" + index.ToString() +
                "    AND Weight <> 0 " +
                ")A " +
                "ORDER BY IdIndex,IdTicker,Date_Ref ";

            using (newNestConn conn = new newNestConn())
            {
                indexComponentsWeightsTable = conn.Return_DataTable(SQL_Indexes_Components);
            }

            for (int i = 0; i < indexComponentsWeightsTable.Rows.Count; i++)
            {                    
                ticker = (int)indexComponentsWeightsTable.Rows[i][0];
                weight = (double)indexComponentsWeightsTable.Rows[i][4];
                refDate = (DateTime)indexComponentsWeightsTable.Rows[i][5];                

                if (ticker != prevTicker && prevTicker != 0)
                {
                    IndexWeights.Add(prevTicker,TickerWeights);
                    TickerWeights = new Dictionary<DateTime,double>();
                }

                TickerWeights.Add(refDate, weight);

                prevTicker = ticker;
            }

            IndexWeights.Add(prevTicker, TickerWeights);

            return IndexWeights;
        }

        private double GetTR_Index(int index, DateTime date)
        {
            string SQL =
                "SELECT SrValue "+
                "FROM dbo.Tb053_Precos_Indices "+
                "WHERE IdSecurity = "+ index +" AND "+         
                "        SrDate = '" + date.ToString("yyyyMMdd") + "' AND "+
                "        SrType = 101";

            using (newNestConn conn = new newNestConn())
            {
                double result = conn.Return_Double(SQL);

                if(!double.IsNaN(result))
                    return result;
                else 
                    return 1;                
            }
        }

        public void insertPrice(string idIndex, string Value, string Source_TimeStamp, string Source)
        {
            string SQL;

            SQL = "EXEC NESTDB.dbo.Proc_Insert_Price " + idIndex + "," + Value + ",'" + Source_TimeStamp + "',102 ," + Source + ",1";

            using (newNestConn Conn = new newNestConn())
            {
                Conn.ExecuteNonQuery(SQL);
            }
        }

        public void CalculateHistoricalIndexes()
        {
            List<int> Indexes = GetIndexes();            

            DateTime iniDate = DBInterface.iniDate;

            List<DateTime> Dates = GetDates(iniDate);

            int i = 0;

            foreach (DateTime curDate in Dates)
            {
                i++;
                CalculateAllIndexes(curDate);                
            }
            
        }        

        public double CalculateIndex(int index,DateTime date)
        {            
            List<int> IndexComponents = GetIndexComponents(index);
            Dictionary<int, Dictionary<DateTime, double>> IndexWeights = GetIndexComponentsWeights(index);           

            DateTime curDate = date;

            double TR_Pre_Auction_Price = 0;


            double[] IndexArray = { (double)index };
            double[] Tickers = new double[IndexComponents.Count];

            for (int i = 0; i < IndexComponents.Count; i++)
            {
                Tickers[i] = (double)IndexComponents.ElementAt(i);
            }                       

            Last.LoadTickers(Tickers);
            TR_DayBBG.LoadTickers(Tickers);
            TR_DayNest.LoadTickers(Tickers);
            TR_IndexNest.LoadTickers(IndexArray);
            TR_IndexBBG.LoadTickers(Tickers);
            DBInterface.LoadTickers(Tickers);

            double[] IndexValueArray = TR_IndexNest.GetValue(index, TR_IndexNest.PrevDate(date), true);
            if (!(IndexValueArray[1] != 0))
            {                
                IndexValueArray = TR_IndexBBG.GetValue(index, TR_IndexNest.PrevDate(date), true);
            }

            double IndexValue = IndexValueArray[1];

            if (date == new DateTime(2004, 07, 01))
            { }

            if (IndexValue > 0)
            { }

            foreach (int ticker in IndexComponents)
            {
                Dictionary<DateTime, double> tickerWeights = new Dictionary<DateTime, double>();
                double Weight = 0.0;                

                if (IndexWeights.TryGetValue(ticker, out tickerWeights))
                {
                    if (curDate >= tickerWeights.Last().Key)
                    {
                        Weight = tickerWeights.Last().Value;
                    }
                    else
                    {
                        for (int i = 0; i < tickerWeights.Count; i++)
                        {
                            if (curDate >= tickerWeights.Keys.ElementAt(i) && curDate < tickerWeights.Keys.ElementAt(i+1))
                            {
                                tickerWeights.TryGetValue(tickerWeights.Keys.ElementAt(i), out Weight);
                            }
                        }
                    }
                }

                if (Weight != 0)
                {

                    double[] curLast = new double[2];
                    double[] SourceType = new double[2];
                    double[] trDay = new double[2];

                    curLast = Last.GetValue(ticker, curDate, true);
                    SourceType = DBInterface.GetValue(ticker, curDate, true);
                    trDay = TR_DayBBG.GetValue(ticker, curDate, true);

                    if (!(trDay[1] != 0))
                    {
                        trDay = TR_DayNest.GetValue(ticker, curDate, true);
                    }

                    if (!(curLast[1] != 0))
                    { }                    

                    //TODO BACKTEST
                    //StreamWriter Sw = new StreamWriter(@"C:\Temp\QSEGTest.csv", true);

                    double _Last = curLast[1];
                    double _PreAuction = SourceType[1];
                    double _trDay = trDay[1];

                    /*string SLast = _Last != 0 ? _Last.ToString() : @"N\A";
                    string SPreAuction = _PreAuction != 0 ? _PreAuction.ToString() : @"N\A";
                    string STrDay = _trDay != 0 ? _trDay.ToString() : @"N\A";
                    string SWeight = Weight != 0 ? Weight.ToString() : @"N\A";

                    string Line = curDate.ToString("yyyyMMdd") +";" + index +";" +ticker+";" + SLast + ";" + SPreAuction + ";" + STrDay + ";" + SWeight;
                    Sw.WriteLine(Line);
                    Sw.Close();*/

                    if (_PreAuction == 0)
                    {
                        _PreAuction = _Last;
                    }

                    /*StreamWriter Sw2 = new StreamWriter(@"C:\Temp\QSEGTest2.csv", true);

                    SPreAuction = _PreAuction != 0 ? _PreAuction.ToString() : @"N\A";

                    string Line2 = curDate.ToString("yyyyMMdd") + ";" + index + ";" + ticker + ";" + SLast + ";" + SPreAuction + ";" + STrDay + ";" + SWeight;
                    Sw2.WriteLine(Line2);
                    Sw2.Close();*/

                    if (_Last != 0 && _PreAuction != 0 && _trDay != 0)
                    {
                        TR_Pre_Auction_Price += Weight * (_trDay * _PreAuction / _Last);
                    }
                }
            }

            if (TR_Pre_Auction_Price != 0)
            {
                IndexValue *= 1 + TR_Pre_Auction_Price;
                return IndexValue;
            }
            else
            {
                return 0;
            }
        }

        public void CalculateAllIndexes(DateTime date)
        {
             List<int> Indexes = GetIndexes();

             foreach (int index in Indexes)
             {
                 double value = CalculateIndex(index, date);
                 if (value != 0)
                 {
                     insertPrice(index.ToString(), value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7");
                 }
             }
        }

        public List<DateTime> GetDates(DateTime IniDate)
        {
            List<DateTime> Dates = new List<DateTime>();
            DataTable Tb;
            string SqlQuery =
                "SELECT DISTINCT(SrDate) " +
                "FROM NESTDB.dbo.Tb053_Precos_Indices (NOLOCK) " +
                "WHERE IdSecurity = 1073 AND " +
                "    SrType = 1 AND " +
                "    SOURCE = 1";

            using (newNestConn conn = new newNestConn())
            {
                Tb = conn.Return_DataTable(SqlQuery);
            }

            for (int i = 0; i < Tb.Rows.Count; i++)
            {
                if(DateTime.Parse(Tb.Rows[i][0].ToString()) >= IniDate)
                Dates.Add(DateTime.Parse(Tb.Rows[i][0].ToString()));
            }

            return Dates;
        }


        
        
    }
}
