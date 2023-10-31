using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NestDLL;

namespace VWAPS
{
    class PriceCalculator
    {
        int Security;
        DateTime Date;

        public Dictionary<int, double> VWAP_List = new Dictionary<int, double>();
        public Dictionary<int, double> Last_List = new Dictionary<int, double>();
        public double Open;
        public double Close;

        public PriceCalculator(int _Security, DateTime _Date)
        {
            if (Date == new DateTime(2012, 01, 30))
            { }
            if (_Security == 556)
            { }

            Security = _Security;
            Date = _Date;

            DataTable Data;
            DataTable OpenData;
            DataTable CloseData;

            using (newNestConn curConn = new newNestConn(true))
            {
                string SQL = "  SELECT IdTicker, Minute, VWAP, Volume, [Close], [Open] " +
                             "  FROM RTICKDB.dbo.IntradayOneMinuteBars (NOLOCK) " +
                             "  WHERE Date = '" + Date.ToString("yyyyMMdd") + "' " +
                             "  AND IdTicker = " + Security +
                             "  ORDER BY Minute";

                Data = curConn.Return_DataTable(SQL);

                string SQL_Open = "SELECT Price " +
                                    "FROM RTICKDB.dbo.OpenAuction " +
                                    "WHERE IDTICKER = " + _Security + " AND DATE = '" + _Date.ToString("yyyyMMdd") + "'";

                OpenData = curConn.Return_DataTable(SQL_Open);

                string SQL_Close = "SELECT Price " +
                                    "FROM RTICKDB.dbo.CloseAuction " +
                                    "WHERE IDTICKER = " + _Security + " AND DATE = '" + _Date.ToString("yyyyMMdd") + "'";

                CloseData = curConn.Return_DataTable(SQL_Close);
            }

            double curVwap = 0, curLast = 0, sumVol = 0;

            for (int i = 0, j = 0; i < Data.Rows.Count; i++)
            {
                if ((int)Data.Rows[i][1] == 360)
                { }

                if (i == Data.Rows.Count - 1)
                { }
                if (!((int)Data.Rows[i][1] / 60 > j))
                {
                    curVwap += double.Parse(Data.Rows[i][2].ToString()) * double.Parse(Data.Rows[i][3].ToString());
                    curLast = double.Parse(Data.Rows[i][4].ToString());
                    sumVol += double.Parse(Data.Rows[i][3].ToString());

                    if (i == Data.Rows.Count - 1)
                    {
                        VWAP_List.Add(j, curVwap / sumVol);
                        Last_List.Add(j, curLast);
                    }
                }
                else
                {
                    if (curVwap != 0 && sumVol != 0 && curLast != 0)
                    {
                        VWAP_List.Add(j, curVwap / sumVol);
                        Last_List.Add(j, curLast);
                    }

                    j = (int)Data.Rows[i][1] / 60;

                    curVwap = sumVol = curLast = 0;

                    curVwap += double.Parse(Data.Rows[i][2].ToString()) * double.Parse(Data.Rows[i][3].ToString());
                    sumVol += double.Parse(Data.Rows[i][3].ToString());
                    curLast = double.Parse(Data.Rows[i][2].ToString());

                    if (i == Data.Rows.Count - 1)
                    {
                        VWAP_List.Add(j, curVwap / sumVol);
                        Last_List.Add(j, curLast);
                    }
                }
            }


            if (OpenData.Rows.Count > 0)
            {
                Open = double.Parse(OpenData.Rows[0][0].ToString());
            }
            else if (Data.Rows.Count > 0)
            {
                Open = double.Parse(Data.Rows[0][5].ToString());
            }

            if (CloseData.Rows.Count > 0)
            {
                Close = double.Parse(CloseData.Rows[0][0].ToString());
            }
            else if (Data.Rows.Count > 0)
            {
                Close = double.Parse(Data.Rows[Data.Rows.Count - 1][4].ToString());
            }
        }

    }
}
