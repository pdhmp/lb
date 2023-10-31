using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using NestQuant.Common;
using System.IO;

namespace MarketClusters
{
    class ClusterCalc
    {
        DataContainer curContainer = new DataContainer();
        public DateTime iniDate = new DateTime();
        public DateTime endDate = new DateTime();
        public int GroupCounter = 1;
        public int serCount = 0;

        public double[,] SortedGroups;
        public int SortedCounter = 0;

        public int[] DrawOrder = new int[100];
        public int DrawOrderCounter = 0;

        public void LoadAllSeries()
        {

            DataTable dt;

            string SQLString = " SELECT Id_Ticker_Component " +
                                " FROM NESTDB.dbo.Tb023_Securities_Composition " +
                                " WHERE Id_Ticker_Composite=1073 AND Date_Ref=(SELECT MAX(Date_Ref) FROM NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=1073)  " +
                                " ORDER BY Id_Ticker_Component";


            /*
            SQLString = " SELECT Id_Ticker_Component " +
                                " FROM NESTDB.dbo.Tb023_Securities_Composition " +
                                " WHERE Id_Ticker_Composite IN " +
                                " ( " +
                                " SELECT Id_Ticker_Component " +
                                " FROM NESTDB.dbo.Tb023_Securities_Composition " +
                                " WHERE Id_Ticker_Composite=21350 AND Date_Ref= " +
                                " (SELECT MAX(Date_Ref) FROM NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=21350) " +
                                " )  " +
                                " AND Weight>0 AND Date_Ref= " +
                                " (SELECT MAX(Date_Ref) FROM NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=21350) " +
                                " GROUP BY Id_Ticker_Component " +
                                " ORDER BY Id_Ticker_Component ";
            */


            //SQLString = "SELECT 561 AS Id_Ticker_Component UNION SELECT 1";


            using (NestConn conn = new NestConn())
            {
                dt = conn.ExecuteDataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {
                    int curId_Ticker = int.Parse(curRow["Id_Ticker_Component"].ToString());
                    DataSeries thisSeries = new DataSeries(curId_Ticker, 0, true);
                    thisSeries.Id_Type = 0;
                    thisSeries.PointPairs = Utils.LoadArray(curId_Ticker, iniDate, endDate);
                    curContainer.Add(curId_Ticker, 0, thisSeries);
                }

                serCount = dt.Rows.Count;

                SortedGroups = new double[dt.Rows.Count - 1, 7];

            // ===================================    Dump all correlations to a file
                StreamWriter sw = new StreamWriter("C:\\temp\\correlOut.txt", true);

                for (int i = 0; i < curContainer.correlCounter; i++)
                {
                    sw.Write(curContainer.correlKeys[i, 0] + "\t" + curContainer.correlKeys[i, 1] + "\t" + curContainer.correlKeys[i, 2] + "\t" + curContainer.correlKeys[i, 3] + "\t" + curContainer.correlValues[i] + "\r\n");
                }
                sw.Close();
            // ======================================================================
            }
        }
        
        public void Clusterize()
        {
            int MaxPosition = GetMaxCorrelPosition();

            DataKey Key1 = new DataKey(curContainer.correlKeys[MaxPosition, 0], curContainer.correlKeys[MaxPosition, 1]);
            DataKey Key2 = new DataKey(curContainer.correlKeys[MaxPosition, 2], curContainer.correlKeys[MaxPosition, 3]);

            DataSeries Series1;
            DataSeries Series2;
            
            curContainer.allSeries.TryGetValue(Key1, out Series1);
            curContainer.allSeries.TryGetValue(Key2, out Series2);

            SortedGroups[SortedCounter, 0] = 0;
            SortedGroups[SortedCounter, 1] = curContainer.correlValues[MaxPosition];
            SortedGroups[SortedCounter, 2] = Series1.Relevant_Id;
            SortedGroups[SortedCounter, 3] = Series1.Id_Type;
            SortedGroups[SortedCounter, 4] = Series2.Relevant_Id;
            SortedGroups[SortedCounter, 5] = Series2.Id_Type;
            SortedGroups[SortedCounter, 6] = GroupCounter;
            SortedCounter++;

            MergeSeries(Series1, Series2);

            curContainer.Remove(Key1);
            curContainer.Remove(Key2);

        }

        private void MergeSeries(DataSeries Series1, DataSeries Series2)
        {
            int Relevant_Id = GroupCounter++;
            DataSeries thisSeries = new DataSeries(Relevant_Id, 0, true);
            thisSeries.Id_Type = 1;

            double Weight1 = 0.5F;
            double Weight2 = 0.5F;

            double[,] tempArray = Utils.AlignSeries(Series1, Series2);

            PointPairList[] thisPointPairs = new PointPairList[Series1.PointPairs.Length];

            for (int i = 0; i < tempArray.Length / 3 - 1; i++)
			{
                PointPairList thisPoint = new PointPairList(DateTime.FromOADate(tempArray[i, 2]), tempArray[i, 0] * Weight1 + tempArray[i, 1] * Weight2);
                thisPointPairs[i] = thisPoint;
			}

            thisSeries.PointPairs = thisPointPairs;
            curContainer.Add(Relevant_Id, 1, thisSeries);

        }

        private int GetMaxCorrelPosition()
        {
            double MaxCorrel = double.NegativeInfinity;
            int MaxCorrelPosition = -1;

            for (int i = 0; i < curContainer.correlCounter; i++)
            {
                double curCorrel = curContainer.correlValues[i];
                if (curCorrel > MaxCorrel)
                {
                    MaxCorrel = curCorrel;
                    MaxCorrelPosition = i;
                }
            }

            return MaxCorrelPosition;
        }

        public void ReorderGroups(int curPos)
        {
            double ser1 = SortedGroups[curPos, 2];
            double ser2 = SortedGroups[curPos, 4];
            double type1 = SortedGroups[curPos, 3];
            double type2 = SortedGroups[curPos, 5];

            if (type1 == 1)
            {
                ReorderGroups(FindPos(ser1));
            }
            else
            {
                DrawOrder[DrawOrderCounter++] = (int)ser1;
            }

            if (type2 == 1)
            {
                ReorderGroups(FindPos(ser2));
            }
            else
            {
                DrawOrder[DrawOrderCounter++] = (int)ser2;
            }
        }

        private int FindPos(double GroupNumber)
        {
            for (int i = 0; i < SortedCounter; i++)
            {
                if (SortedGroups[i, 6] == GroupNumber)
                {
                    return i;
                }
            }
            return -1;
        }

        public void ForceCorrelIncrease()
        {
            for (int i = 1; i < SortedCounter; i++)
            {
                if (SortedGroups[i, 1] > SortedGroups[i - 1, 1])
                {
                    SortedGroups[i, 1] = SortedGroups[i - 1, 1] - 0.001F;
                }
            }

        }

    }
}
