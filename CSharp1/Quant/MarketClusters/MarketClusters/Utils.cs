using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NestQuant.Common;

namespace MarketClusters
{
    public struct PointPairList
    {
        public DateTime curDate;
        public double curValue;

        public PointPairList(DateTime _curDate, double _curValue)
        {
            this.curDate = _curDate;
            this.curValue = _curValue;
        }
    }

    public struct DataKey : IComparable
    {
        public int Relevant_Id;
        public int Id_Type;

        public DataKey(int _Relevant_Id, int _Id_Type)
        {
            this.Relevant_Id = _Relevant_Id;
            this.Id_Type = _Id_Type;
        }

        public int CompareTo(object obj)
        {
            DataKey convObj = (DataKey)obj;

            if (convObj.Relevant_Id == 561)
            {
                int r = 0;
            }

            if (this.Relevant_Id == convObj.Relevant_Id && this.Id_Type == convObj.Id_Type)
            {
                return 0;
            }
            else if (this.Id_Type > convObj.Id_Type)
            {
                return 1;
            }
            else if(this.Id_Type < convObj.Id_Type)
            {
                return -1;
            }
            else
            {
                if (this.Relevant_Id > convObj.Relevant_Id)
                {
                    return 1;
                }

                if (this.Relevant_Id < convObj.Relevant_Id)
                {
                    return -1;
                }
            }
            return 0;

        }
    }

    class Utils
    {
        public static string GetTickerSymbol(int Id_Ticker)
        {
            DataTable dt;
            using (NestConn conn = new NestConn())
            {
                string SQLString = "SELECT NestTicker + '(' + LEFT(UPPER(Setor), 4) + ')' FROM NESTDB.dbo.Tb001_Securities A LEFT JOIN dbo.Tb000_Issuers B ON A.IdIssuer=B.IdIssuer LEFT JOIN dbo.Tb113_Setores C ON B.IdNestSector=C.Id_Setor WHERE IdSecurity=" + Id_Ticker.ToString();
                return conn.Execute_Query_String(SQLString);
            }
        }


        public static PointPairList[] LoadArray(int Id_Ticker, DateTime iniDate, DateTime endDate)
        {
            DataTable dt;
            using (NestConn conn = new NestConn())
            {
                string SQLString = "SELECT SrDate, SrValue FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=" + Id_Ticker + " AND SrType=100 AND Source=1 AND SrDate>='" + iniDate.ToString("yyyy-MM-dd") + "' AND SrDate<='" + endDate.ToString("yyyy-MM-dd") + "' ORDER BY SrDate";
                dt = conn.ExecuteDataTable(SQLString);
            }

            PointPairList[] thisSeries = new PointPairList[dt.Rows.Count];

            int curPos = 0;

            foreach (DataRow curRow in dt.Rows)
            {
                PointPairList thisPoint = new PointPairList(DateTime.Parse(curRow[0].ToString()), double.Parse(curRow[1].ToString()));
                thisSeries[curPos] = thisPoint;
                curPos++;
            }

            return thisSeries;

        }

        public static double[,] AlignSeries(DataSeries SeriesX, DataSeries SeriesY)
        {
            double[,] tempArray = new double[10000, 3];

            int CounterX = 0;
            int CounterY = 0;
            int CounterOut = 0;

            while (CounterX < SeriesX.PointPairs.Length && CounterY < SeriesY.PointPairs.Length)
            {
                if (SeriesX.PointPairs[CounterX].curDate == SeriesY.PointPairs[CounterY].curDate)
                {
                    tempArray[CounterOut, 0] = SeriesX.PointPairs[CounterX].curValue;
                    tempArray[CounterOut, 1] = SeriesY.PointPairs[CounterY].curValue;
                    tempArray[CounterOut, 2] = SeriesX.PointPairs[CounterX].curDate.ToOADate();
                    CounterOut++;
                    CounterX++;
                    CounterY++;
                }
                else
                {
                    if (SeriesX.PointPairs[CounterX].curDate < SeriesY.PointPairs[CounterY].curDate)
                    {
                        CounterX++;
                    }
                    else
                    {
                        CounterY++;
                    }
                }
            }

            double[,] finalArray = new double[CounterOut, 3];

            System.Array.Copy(tempArray, finalArray, CounterOut * 3);

            return finalArray;

        }

        public static double calcCorrel(double[,] AlignedSeries)
        {
            double sumX = 0;
            double sumY = 0;

            double sumSqX = 0;
            double sumSqY = 0;
            double sumSqXY = 0;

            double meanX = 0;
            double meanY = 0;

            int TotalItems = AlignedSeries.Length / 3;

            for (int i = 0; i < TotalItems - 1; i++)
            {
                sumX += AlignedSeries[i, 0];
                sumY += AlignedSeries[i, 0];
            }

            for (int i = 0; i < TotalItems - 1; i++)
            {
                sumSqX += Math.Pow(AlignedSeries[i, 0] - meanX, 2);
                sumSqY += Math.Pow(AlignedSeries[i, 1] - meanY, 2);
                sumSqXY += (AlignedSeries[i, 0] - meanX) * (AlignedSeries[i, 1] - meanY);
            }

            double stDevX = Math.Pow(sumSqX / TotalItems, 0.5);
            double stDevY = Math.Pow(sumSqY / TotalItems, 0.5);

            double curCorrel = (sumSqXY / TotalItems) / (stDevX * stDevY);

            return curCorrel;
        }

    }
}
