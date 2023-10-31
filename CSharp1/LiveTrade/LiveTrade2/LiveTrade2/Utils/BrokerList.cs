using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Sql;
using System.Data;

namespace LiveTrade2
{
    class BrokerList
    {
        SortedDictionary<int, string> Brokers = new SortedDictionary<int, string>();

        public BrokerList()
        {
            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                string SQLString = "SELECT Id_BOVESPA, Broker_Ticker FROM NESTDB.dbo.Tb011_Corretoras WHERE Id_BOVESPA IS NOT NULL AND Broker_Ticker IS NOT NULL";

                DataTable dt = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {
                    Brokers.Add(int.Parse(curRow[0].ToString()), curRow[1].ToString());
                }
            }
        }

        public string BrokerTicker(int IdBroker)
        {
            string strBrokerTicker = "";

            if (IdBroker > 0)
            {
                if (!Brokers.TryGetValue(IdBroker, out strBrokerTicker))
                {
                    strBrokerTicker = IdBroker.ToString();
                }
            }
            else if (IdBroker == -1)
            {
                strBrokerTicker = "AGG";
            }
            else
            {
                strBrokerTicker = "";
            }
            return strBrokerTicker;
        }



    }
}
