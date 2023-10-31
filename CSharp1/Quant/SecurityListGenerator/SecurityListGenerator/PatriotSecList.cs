using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFastData;
using NestDLL;
using System.Data;
using NestQuant.Common;


namespace SecurityListGenerator
{
    class PatriotSecList
    {
        double MinLast = 3.0;
        double MinFinanAvg = 5000000.0;
        double MaxVol = 1;

        int DaysFinanAvg = 10;
        int DaysVol = 21;


        FastData Last;
        FastData Volume;

        //DateTime IniDate = new DateTime(2007, 01, 01);
        DateTime IniDate = DateTime.Today.AddDays(-10);
        //DateTime EndDate = new DateTime(2010, 01, 10);
        DateTime EndDate = DateTime.Today;


        SortedDictionary<DateTime, List<double>> SecList = new SortedDictionary<DateTime, List<double>>();

        public void GenerateSecList()
        {
            DateTime MinDate = IniDate.AddDays(-50);

            Last = new FastData(1, MinDate, EndDate, 1);
            Volume = new FastData(6, MinDate, EndDate, 1);

            DataTable tempTable = new DataTable();

            using (newNestConn curConn = new newNestConn())
            {
                /*string SQLExpression = " SELECT distinct A.IdSecurity, B.NESTTICKER" +
                                       " FROM NESTDb.dbo.Tb050_Preco_Acoes_Onshore A (NOLOCK) " +
                                       " LEFT JOIN NESTDB.DBO.TB001_SECURITIES B (NOLOCK) " +
                                       " ON A.IDSECURITY = B.IDSECURITY " +
                                       " WHERE SrType=331 ORDER BY A.IDSECURITY";*/

                string SQLExpression = " select idsecurity, nestticker from" +
                                       " (SELECT idsecurity, nestticker, substring(nestticker,len(nestticker),1) as endChar" +
                                       " FROM NESTDB.DBO.TB001_SECURITIES (NOLOCK)" +
                                       " where idcurrency = 900 and idinstrument = 2" +
                                       " and idassetclass = 1) a" +
                                       " where endchar not in ('T','F')";

                tempTable = curConn.Return_DataTable(SQLExpression);
            }

            double[] curTickers = new double[tempTable.Rows.Count];

            for (int i = 0; i < curTickers.Length; i++)
            {
                curTickers[i] = NestDLL.Utils.ParseToDouble(tempTable.Rows[i][0]);                
            }
            double[] ibov = new double[1];
            ibov[0] = 1073;

            Last.LoadTickers(ibov);
            Volume.LoadTickers(ibov);

            Last.LoadTickers(curTickers);
            Volume.LoadTickers(curTickers);

            DateTime curDate = Last.PrevDate(EndDate);
            
            while (curDate > IniDate)
            {                
                List<double> curList = new List<double>();

                for (int i = 0; i < curTickers.Length; i++)
                {
                    double curLast = Last.GetValue(curTickers[i], curDate, true)[1];

                    if (curLast >= MinLast)
                    {
                        DateTime auxDate = curDate;
                        double curFinan = 0;

                        for (int j = 0; j < DaysFinanAvg; j++)
                        {                            
                            double auxVolume = Volume.GetValue(curTickers[i], auxDate, true)[1];

                            curFinan = curFinan + auxVolume;

                            auxDate = Last.PrevDate(auxDate);
                        }

                        curFinan = curFinan / (double)DaysFinanAvg;

                        if (curFinan >= MinFinanAvg)
                        {
                            auxDate = curDate;
                            DateTime prevDate = Last.PrevDate(auxDate);

                            double[] stdevArray = new double[DaysVol];

                            for (int j = 0; j < DaysVol; j++)
                            {
                                double auxLast = Last.GetValue(curTickers[i], auxDate, false)[1];
                                double prevLast = Last.GetValue(curTickers[i], prevDate, false)[1];
                                double curReturn = auxLast / prevLast - 1;

                                stdevArray[j] = curReturn;

                                auxDate = prevDate;
                                prevDate = Last.PrevDate(auxDate);
                            }

                            double stDev = NestQuant.Common.Utils.calcStdev(ref stdevArray);

                            if (stDev < MaxVol)
                            {
                                curList.Add(curTickers[i]);
                            }
                        }
                    }
                }

                SecList.Add(curDate, curList);

                curDate = Last.PrevDate(curDate);
            }

            int counter = 0;

            string SQLHeader = "INSERT INTO NESTDB.DBO.TB023_SECURITIES_COMPOSITION \r\n";
            string SQLInsert = "";  
            string Union = "";

            foreach (KeyValuePair<DateTime, List<double>> curKvp in SecList)
            {
                foreach (double curTicker in curKvp.Value)
                {
                    counter++;

                    SQLInsert = SQLInsert + Union;

                    string line = "SELECT '" + curKvp.Key.ToString("yyyyMMdd") + "',169168," + curTicker + ",0,0,'' ";
                    SQLInsert = SQLInsert + line;

                    if (counter % 100 == 0)
                    {                        
                        SQLInsert = SQLHeader + SQLInsert;

                        using (newNestConn curConn = new newNestConn())
                        {
                            curConn.ExecuteNonQuery(SQLInsert);
                        }

                        SQLInsert = "";
                        Union = "";
                    }
                    else
                    {
                        Union = " UNION ALL \r\n";
                    }
                }
            }

            using (newNestConn curConn = new newNestConn())
            {
                SQLInsert = SQLHeader + SQLInsert;
                curConn.ExecuteNonQuery(SQLInsert);
            }
        }
    }
}
