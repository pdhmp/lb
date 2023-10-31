using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NestDLL;
using NFastData;


namespace ReutersTickOneMinBarsGenerator
{
    class AVG_5_Days_Calculator
    {
        #region Old Calculator
        /*
        private FastData Last30Vwap;
        private FastData Last30Volume;
        private FastData CloseAucPrice;
        private FastData CloseAucVolume;
        private FastData First330Vwap;
        private FastData First330Volume;

        private SortedDictionary<double, SortedDictionary<DateTime, double>> Last30Average = new SortedDictionary<double, SortedDictionary<DateTime, double>>();
        private SortedDictionary<double, SortedDictionary<DateTime, double>> CloseAverage = new SortedDictionary<double, SortedDictionary<DateTime, double>>();
        private SortedDictionary<double, SortedDictionary<DateTime, double>> First330Average = new SortedDictionary<double, SortedDictionary<DateTime, double>>();


        private DateTime IniDate = DateTime.Today.AddDays(-20);

        public void CalculateAverage()
        {
            DateTime MinDate = IniDate.AddDays(10);

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
                CloseAverage.Add(curTickers[i], new SortedDictionary<DateTime, double>());
                First330Average.Add(curTickers[i], new SortedDictionary<DateTime, double>());
            }

            curTickers[curTickers.Length - 1] = 1073;

            Last30Vwap.LoadTickers(curTickers);
            Last30Volume.LoadTickers(curTickers);
            CloseAucPrice.LoadTickers(curTickers);
            CloseAucVolume.LoadTickers(curTickers);
            First330Vwap.LoadTickers(curTickers);
            First330Volume.LoadTickers(curTickers);


            DateTime curDate = CloseAucPrice.PrevDate(DateTime.Now);

            while (curDate >= MinDate)
            {
                DateTime Dminus1 = CloseAucPrice.PrevDate(curDate);
                DateTime Dminus2 = CloseAucPrice.PrevDate(Dminus1);
                DateTime Dminus3 = CloseAucPrice.PrevDate(Dminus2);
                DateTime Dminus4 = CloseAucPrice.PrevDate(Dminus3);


                for (int i = 0; i < curTickers.Length - 1; i++)
                {
                    //Last30Average

                    double curLast30Avg = (1.0 / 5) * ((Last30Volume.GetValue(curTickers[i], curDate, true)[1] * (Last30Vwap.GetValue(curTickers[i], curDate, true)[1])) +
                                                       (Last30Volume.GetValue(curTickers[i], Dminus1, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus1, true)[1])) +
                                                       (Last30Volume.GetValue(curTickers[i], Dminus2, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus2, true)[1])) +
                                                       (Last30Volume.GetValue(curTickers[i], Dminus3, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus3, true)[1])) +
                                                       (Last30Volume.GetValue(curTickers[i], Dminus4, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus4, true)[1])));
                    Last30Average[curTickers[i]].Add(curDate, curLast30Avg);


                    //CloseAverage
                    double curCloseAvg = (1.0 / 5) * ((CloseAucVolume.GetValue(curTickers[i], curDate, true)[1] * (CloseAucPrice.GetValue(curTickers[i], curDate, true)[1])) +
                                                      (CloseAucVolume.GetValue(curTickers[i], Dminus1, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus1, true)[1])) +
                                                      (CloseAucVolume.GetValue(curTickers[i], Dminus2, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus2, true)[1])) +
                                                      (CloseAucVolume.GetValue(curTickers[i], Dminus3, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus3, true)[1])) +
                                                      (CloseAucVolume.GetValue(curTickers[i], Dminus4, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus4, true)[1])));
                    CloseAverage[curTickers[i]].Add(curDate, curCloseAvg);

                    //First330Avg
                    double curFirst330Avg = (1.0 / 5) * ((First330Volume.GetValue(curTickers[i], curDate, true)[1] * (First330Vwap.GetValue(curTickers[i], curDate, true)[1])) +
                                                         (First330Volume.GetValue(curTickers[i], Dminus1, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus1, true)[1])) +
                                                         (First330Volume.GetValue(curTickers[i], Dminus2, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus2, true)[1])) +
                                                         (First330Volume.GetValue(curTickers[i], Dminus3, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus3, true)[1])) +
                                                         (First330Volume.GetValue(curTickers[i], Dminus4, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus4, true)[1])));
                    First330Average[curTickers[i]].Add(curDate, curFirst330Avg);
                }

                curDate = CloseAucPrice.PrevDate(curDate);
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
                                                   "@SrValue = " + kvp2.Value.ToString().Replace(',', '.') + ", " +
                                                   "@SrType = 352, " +
                                                   "@SOURCE = 22, " +
                                                   "@AUTOMATED = 1";

                            curConn.ExecuteNonQuery(SQLExpression);
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

                            curConn.ExecuteNonQuery(SQLExpression);
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

                            curConn.ExecuteNonQuery(SQLExpression);
                        }
                    }
                }
            }
        }
    }*/
        #endregion
    
        private FastData Last30Vwap;
        private FastData Last30Volume;
        private FastData CloseAucPrice;
        private FastData CloseAucVolume;
        private FastData First330Vwap;
        private FastData First330Volume;

        private SortedDictionary<double, SortedDictionary<DateTime, double>> Last30Average = new SortedDictionary<double, SortedDictionary<DateTime, double>>();
        private SortedDictionary<double, SortedDictionary<DateTime, double>> CloseAverage = new SortedDictionary<double, SortedDictionary<DateTime, double>>();
        private SortedDictionary<double, SortedDictionary<DateTime, double>> First330Average = new SortedDictionary<double, SortedDictionary<DateTime, double>>();


        //private DateTime IniDate = (new DateTime(2004, 01, 08)).AddDays(-15);
        private DateTime IniDate = DateTime.Today.AddDays(-30);

        public void CalculateAverage()
        {
            DateTime MinDate = IniDate.AddDays(30);

            Last30Vwap = new FastData(331, IniDate, DateTime.Now, 22);
            Last30Volume = new FastData(330, IniDate, DateTime.Now, 22);
            CloseAucPrice = new FastData(1, IniDate, DateTime.Now, 1);
            CloseAucVolume = new FastData(310, IniDate, DateTime.Now, 22);
            First330Vwap = new FastData(351, IniDate, DateTime.Now, 22);
            First330Volume = new FastData(350, IniDate, DateTime.Now, 22);

            DataTable tempTable = new DataTable();

            using (newNestConn curConn = new newNestConn(true))
            {
                string SQLExpression = "SELECT distinct(P.IdSecurity)" +
                                       " FROM NESTSRV06.NESTDb.dbo.Tb050_Preco_Acoes_Onshore P (NOLOCK) " +                                       
                                       " WHERE SrType=331 ORDER BY IDSECURITY";

                tempTable = curConn.Return_DataTable(SQLExpression);
            }

            double[] curTickers = new double[tempTable.Rows.Count + 1];

            for (int i = 0; i < curTickers.Length - 1; i++)
            {
                curTickers[i] = NestDLL.Utils.ParseToDouble(tempTable.Rows[i][0]);
                Last30Average.Add(curTickers[i], new SortedDictionary<DateTime, double>());
                CloseAverage.Add(curTickers[i], new SortedDictionary<DateTime, double>());
                First330Average.Add(curTickers[i], new SortedDictionary<DateTime, double>());
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
                    //Last30Average

                    double curLast30Avg = (1.0 / 5) * ((Last30Volume.GetValue(curTickers[i], curDate, true)[1] * (Last30Vwap.GetValue(curTickers[i], curDate, true)[1])) +
                                                       (Last30Volume.GetValue(curTickers[i], Dminus1, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus1, true)[1])) +
                                                       (Last30Volume.GetValue(curTickers[i], Dminus2, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus2, true)[1])) +
                                                       (Last30Volume.GetValue(curTickers[i], Dminus3, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus3, true)[1])) +
                                                       (Last30Volume.GetValue(curTickers[i], Dminus4, true)[1] * (Last30Vwap.GetValue(curTickers[i], Dminus4, true)[1])));
                    Last30Average[curTickers[i]].Add(calcDate, curLast30Avg);


                    //CloseAverage
                    double curCloseAvg = (1.0 / 5) * ((CloseAucVolume.GetValue(curTickers[i], curDate, true)[1] * (CloseAucPrice.GetValue(curTickers[i], curDate, true)[1])) +
                                                      (CloseAucVolume.GetValue(curTickers[i], Dminus1, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus1, true)[1])) +
                                                      (CloseAucVolume.GetValue(curTickers[i], Dminus2, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus2, true)[1])) +
                                                      (CloseAucVolume.GetValue(curTickers[i], Dminus3, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus3, true)[1])) +
                                                      (CloseAucVolume.GetValue(curTickers[i], Dminus4, true)[1] * (CloseAucPrice.GetValue(curTickers[i], Dminus4, true)[1])));
                    CloseAverage[curTickers[i]].Add(calcDate, curCloseAvg);

                    //First330Avg
                    double curFirst330Avg = (1.0 / 5) * ((First330Volume.GetValue(curTickers[i], curDate, true)[1] * (First330Vwap.GetValue(curTickers[i], curDate, true)[1])) +
                                                         (First330Volume.GetValue(curTickers[i], Dminus1, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus1, true)[1])) +
                                                         (First330Volume.GetValue(curTickers[i], Dminus2, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus2, true)[1])) +
                                                         (First330Volume.GetValue(curTickers[i], Dminus3, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus3, true)[1])) +
                                                         (First330Volume.GetValue(curTickers[i], Dminus4, true)[1] * (First330Vwap.GetValue(curTickers[i], Dminus4, true)[1])));
                    First330Average[curTickers[i]].Add(calcDate, curFirst330Avg);
                }

                prevDate = calcDate;                
            }


            using (newNestConn curConn = new newNestConn(true))
            {
                foreach (KeyValuePair<double, SortedDictionary<DateTime, double>> kvp1 in First330Average)
                {
                    foreach (KeyValuePair<DateTime, double> kvp2 in kvp1.Value)
                    {
                        if (kvp2.Value != 0)
                        {
                            string SQLExpression = "EXEC NESTSRV06.NESTDB.dbo.Proc_Insert_Price " +
                                                   "@IdSecurity = " + kvp1.Key.ToString() + ", " +
                                                   "@DATA = '" + kvp2.Key.ToString("yyyyMMdd") + "' , " +
                                                   "@SrValue = " + kvp2.Value.ToString().Replace(',','.') + ", " +
                                                   "@SrType = 352, " +
                                                   "@SOURCE = 22, " +
                                                   "@AUTOMATED = 1";

                            int result = curConn.ExecuteNonQuery(SQLExpression);
                        }
                    }
                }

                foreach (KeyValuePair<double, SortedDictionary<DateTime, double>> kvp1 in CloseAverage)
                {
                    foreach (KeyValuePair<DateTime, double> kvp2 in kvp1.Value)
                    {
                        if (kvp2.Value != 0)
                        {
                            string SQLExpression = "EXEC NESTSRV06.NESTDB.dbo.Proc_Insert_Price " +
                                                   "@IdSecurity = " + kvp1.Key.ToString() + ", " +
                                                   "@DATA = '" + kvp2.Key.ToString("yyyyMMdd") + "' , " +
                                                   "@SrValue = " + kvp2.Value.ToString().Replace(',', '.') + ", " +
                                                   "@SrType = 313, " +
                                                   "@SOURCE = 22, " +
                                                   "@AUTOMATED = 1";

                            curConn.ExecuteNonQuery(SQLExpression);
                        }
                    }
                }

                foreach (KeyValuePair<double, SortedDictionary<DateTime, double>> kvp1 in Last30Average)
                {
                    foreach (KeyValuePair<DateTime, double> kvp2 in kvp1.Value)
                    {
                        if (kvp2.Value != 0)
                        {
                            string SQLExpression = "EXEC NESTSRV06.NESTDB.dbo.Proc_Insert_Price " +
                                                   "@IdSecurity = " + kvp1.Key.ToString() + ", " +
                                                   "@DATA = '" + kvp2.Key.ToString("yyyyMMdd") + "' , " +
                                                   "@SrValue = " + kvp2.Value.ToString().Replace(',', '.') + ", " +
                                                   "@SrType = 332, " +
                                                   "@SOURCE = 22, " +
                                                   "@AUTOMATED = 1";

                            curConn.ExecuteNonQuery(SQLExpression);
                        }
                    }
                }
            }
        }
    }
}
