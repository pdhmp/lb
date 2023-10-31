using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NFastData;
using NestDLL;
using NestQuant.Common;

namespace CBMomentumBZ
{
    class DataLoader
    {
        #region Parameters

        private int _IdTickerTemplate;
        private int _IdTickerComposite;
        private DateTime _IniDate;
        private DateTime _EndDate;              

        public int IdTickerTemplate { get { return _IdTickerTemplate; } set { _IdTickerTemplate = value; } }
        public int IdTickerComposite { get { return _IdTickerComposite; } set { _IdTickerComposite = value; } }
        public DateTime IniDate { get { return _IniDate; } set { _IniDate = value; } }
        public DateTime EndDate { get { return _EndDate; } set { _EndDate = value; } }

        #endregion

        #region Data

        DateTime[] TemplateDates;

        FastData StrategyWeights;
        SortedDictionary<double, FastData> SectorWeights;
        SortedDictionary<double, FastData> SectorPrices;

        double[] Sectors;
        SortedDictionary<double, double[]> TickerBySector;
        SortedDictionary<double, double> SectorByTicker;


        #endregion

        #region Methods

        public void DataLoader(int __IdTickerTemplate, int __IdTickerComposite, DateTime __IniDate, DateTime __EndDate)
        {
            _IdTickerTemplate = __IdTickerTemplate;
            _IdTickerComposite = __IdTickerComposite;
            _IniDate = __IniDate;
            _EndDate = __EndDate;
        }

        public void LoadData()
        {
            LoadTemplateDates();


        }

        public void LoadTemplateDates()
        {
            string SQLString = "SELECT DISTINCT SRDATE" +
                               " FROM NESTDB.DBO.TB053_PRECOS_INDICES (NOLOCK)" +
                               " WHERE IDSECURITY = " + IdTickerTemplate +
                               " AND SRDATE >= '" + _IniDate.ToString("yyyyMMdd") + "'" +
                               " AND SRDATE <= '" + _EndDate.ToString("yyyyMMdd") + "'" +
                               " ORDER BY SRDATE";

            DataTable dt;

            using (newNestConn conn = new newNestConn())
            {
                dt = conn.Return_DataTable(SQLString);
            }

            TemplateDates = new DateTime[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TemplateDates[i] = DateTime.Parse(dt.Rows[i]["SRDATE"].ToString());
            }
        }
        public void LoadStrategyWeights()
        {
            using (newNestConn conn = new newNestConn())
            {
                string SQLString = "SELECT DISTINCT ID_TICKER_COMPONENT" +
                                   " FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION (NOLOCK)" +
                                   " WHERE ID_TICKER_COMPOSITE = " + _IdTickerComposite +
                                   " AND DATE_REF >= '" + _IniDate.ToString("yyyyMMdd") + "'" +
                                   " AND DATE_REF <= '" + _EndDate.ToString("yyyyMMdd") + "'";

                DataTable dt = conn.Return_DataTable(SQLString);

                Sectors = new double[dt.Rows.Count];

                int i = 0;

                foreach (DataRow curRow in dt.Rows)
                {
                    Sectors[i] = NestDLL.Utils.ParseToDouble(curRow["ID_TICKER_COMPONENT"]);
                    i++;
                }


            }
        }

        #endregion
    }
}
