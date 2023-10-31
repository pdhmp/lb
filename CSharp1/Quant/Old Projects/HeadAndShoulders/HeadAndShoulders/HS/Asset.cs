using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NestSim.Common;

using NestSimDLL;



namespace NestSim.HS
{
    public class Asset
    {

        public struct Price
        {
            public DateTime date;
            public double value;
        }

        private int idTicker;

        public int Id_Ticker
        {
            get { return idTicker; }
            set { idTicker = value; }
        }
      
        private int maxBar;
        
        public int MaxBar
        {
            get { return maxBar; }
            set { maxBar = value; }
        }

        public SortedDictionary<int, Price> barPrice;

        public double LastPrice;
        public double LastIndex;
        public int sideFactor;

        public Asset(int _Id_Ticker, int _sideFactor)
        {
            Id_Ticker = _Id_Ticker;
            MaxBar = 0;
            barPrice = new SortedDictionary<int,Price>();
            sideFactor = _sideFactor;


            getLastPrice();
            getLastIndex();
        }
                
        /// <summary>
        /// Inserts the asset´s price on the last bar
        /// </summary>
        /// <param name="date">Price´s date</param>
        /// <param name="value">Price´s value</param>
        /// <returns></returns>
        public bool insertBarPrice(DateTime date, double value)
        {
            
            Price _price;

            _price.date = date;
            _price.value = value;
            
            try
            {
                barPrice.Add(maxBar, _price);                
            }
            catch 
            {
                Log.AddLogEntry("Unable to insert price", "C:\\Logs\\HeadAndShouldersLog.txt");                
                return false;
            }

            maxBar++;

            return true;
        }

        public bool getLastPrice()
        {
            string SQLExpression;

            using (NestConn conn = new NestConn())
            {
                conn.openConn();
                SQLExpression = "SELECT NESTDB.dbo.FCN_GET_PRICE_Value_Only(" + Id_Ticker + 
                                ", '"+DateTime.Today.AddDays(-1).ToString("yyyyMMdd")+ "', 1, 0, 2, 0, 0)";
                SqlDataReader result = conn.ExecuteReader(SQLExpression);
                
                if (result.Read())
                {
                    LastPrice = result.GetDouble(0);
                    return true;
                }
                else
                {
                    Log.AddLogEntry("Unable to get LastPrice for Id_Ticker " + Id_Ticker, "C:\\Logs\\HeadAndShouldersLog.txt");
                    return false;
                }
            }
        }

        public bool getLastIndex()
        {
            string SQLExpression;

            using (NestConn conn = new NestConn())
            {
                conn.openConn();
                SQLExpression = "SELECT NESTDB.dbo.FCN_GET_PRICE_Value_Only(" + Id_Ticker +
                                ", '" + DateTime.Today.AddDays(-1).ToString("yyyyMMdd") + "', 101, 0, 2, 0, 0)";
                SqlDataReader result = conn.ExecuteReader(SQLExpression);

                if (result.Read())
                {
                    LastIndex = result.GetDouble(0);
                    return true;
                }
                else
                {
                    Log.AddLogEntry("Unable to get LastIndex for Id_Ticker " + Id_Ticker, "C:\\Logs\\HeadAndShouldersLog.txt");
                    return false;
                }
            }
        }

        public void loadHistoric(DateTime beginDate, DateTime endDate, bool intraday)
        {
            if (intraday)
            {
                loadHistoricIntraday(beginDate, endDate);
            }
            else
            {
                loadHistoricDaily(beginDate, endDate);
            }
        }
        
        private void loadHistoricDaily(DateTime beginDate, DateTime endDate)
        {
            DateTime minDate = new DateTime();
           
            using (NestConn conn = new NestConn())
            {
                conn.openConn();
                string SQLExpression;

                SQLExpression = "SELECT MAX(Data_Hora_Reg) FROM " +
                                "(SELECT TOP 60 Data_Hora_Reg FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore " +
                                "WHERE Id_Ativo=" + Id_Ticker + " AND Data_Hora_Reg >='" +
                                beginDate.ToString("yyyyMMdd") + "' AND Data_Hora_Reg <='" +
                                endDate.ToString("yyyyMMdd") + "' AND Tb050_Preco_Acoes_Onshore.Tipo_Preco=100 " +
                                "ORDER BY Data_Hora_Reg) AS A";

                SqlDataReader result = conn.ExecuteReader(SQLExpression);

                if (result.Read())
                {
                    minDate = result.GetDateTime(0);
                }
                else
                {
                    Log.AddLogEntry("Unable to obtain minDate", "C:\\Logs\\HeadAndShouldersLog.txt");
                    return;
                }
            }

            using (NestConn conn = new NestConn())
            {
                conn.openConn();
                string SQLExpression;
            
                SQLExpression = "SELECT * FROM NESTSIM.dbo.FCN_RescaleVol(" + Id_Ticker +
                                ",'2004-12-31','" + beginDate.ToString("yyyyMMdd") + "'" +
                                ",'" + endDate.ToString("yyyyMMdd") + "' " +
                                ",'" + minDate.ToString("yyyyMMdd") + "', 0.021, 0) "+
                                "ORDER BY Data_Hora_Reg";

                SqlDataReader result = conn.ExecuteReader(SQLExpression);

                while (result.Read())
                {
                    insertBarPrice(result.GetDateTime(result.GetOrdinal("Data_Hora_Reg")),
                                   sideFactor* result.GetDouble(result.GetOrdinal("Valor")));
                    
                }                
            }
        }

        private void loadHistoricIntraday(DateTime beginDate, DateTime endDate)
        {
            using (NestConn conn = new NestConn())
            {
                string SQLExpression;

                SQLExpression = "select trade_datetime, price " +
                                " from nestsim.dbo.tb300_Quote_Recap_sim" +
                                " where id_ticker = " + idTicker + " and price is not null" +
                                " and trade_datetime >= '" + beginDate.ToString("yyyyMMdd") + "' "+
                                " and trade_datetime <= '" + endDate.ToString("yyyyMMdd") + "' " +
                                " order by trade_datetime";

                SqlDataReader result = conn.ExecuteReader(SQLExpression);

                while (result.Read())
                {
                    insertBarPrice(result.GetDateTime(result.GetOrdinal("trade_datetime")),
                                   sideFactor * result.GetDouble(result.GetOrdinal("price")));

                } 
            }
        }    
    }
}
