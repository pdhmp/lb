using System;
using System.Collections.Generic;
using System.Text;

using NestSimDLL;

namespace NestSim.Common
{
    class TradeSignal
    {
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        private int id_Ticker;

        public int Id_Ticker
        {
            get { return id_Ticker; }
            set { id_Ticker = value; }
        }
        private int signal;

        public int Signal
        {
            get { return signal; }
            set { signal = value; }
        }
        
        public TradeSignal(DateTime _date, int _id_Ticker, int _signal)
        {
            date = _date;
            id_Ticker = _id_Ticker;
            signal = _signal;
        }

        public void insertDB(int Id_SIM)
        {            
            string SQLExpression;

            SQLExpression = "INSERT INTO [NESTSIM].[dbo].[Trade_Signals_SIM] " +
                             "([Id_SIM],[Trade_Signal_DateTime],[Id_Ticker],[Trade_Side],[Trade_Price]) " +
                             " VALUES (" + Id_SIM + ", '" + date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', " + id_Ticker + ", " + signal + ", 0)";

            using (NestConn conn = new NestConn())
            {
                conn.openConn();
                conn.ExecuteNonQuery(SQLExpression);
            }

                             
        }
    }
}
