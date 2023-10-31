using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NestDLL;

namespace NewValueBZ
{
    public class FastData
    {
        SortedDictionary<double, SortedDictionary<DateTime, double>> curItems = new SortedDictionary<double, SortedDictionary<DateTime, double>>();

        private int _PriceType;
        private DateTime _iniDate;
        private DateTime _endDate;

        public int PriceType
        {
            get { return _PriceType; }
            set { _PriceType = value; }
        }
        
        public DateTime iniDate
        {
            get { return _iniDate; }
            set { _iniDate = value; }
        }

        public DateTime endDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }

        public FastData(int __PriceType, DateTime __iniDate, DateTime __endDate)
        {
            _PriceType = __PriceType;
            _iniDate = __iniDate;
            _endDate = __endDate;
        }


        public void LoadTickers(double[] IdTickerArray)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string TickerList = "0";

                for (int i = 0; i < IdTickerArray.Length; i++)
                {
                    if(!curItems.ContainsKey(IdTickerArray[i]))
                        TickerList = TickerList + ", " + IdTickerArray[i];
                }

                string SQLString = "SELECT Id_Ativo, Data_Hora_Reg, Valor " +
                    " FROM " + NestDLL.Utils.GetTableName(IdTickerArray[0]) +
                    " WHERE Id_Ativo in (" + TickerList + ")" +
                    " AND Tipo_Preco=" + _PriceType.ToString() +
                    " AND Source=1" +
                    " ORDER BY Id_Ativo, Data_Hora_Reg";

                DataTable dt = curConn.Return_DataTable(SQLString);

                double prevTicker = 0;
                SortedDictionary<DateTime, double> tempDictionary = new SortedDictionary<DateTime, double>();

                foreach (DataRow curRow in dt.Rows)
                {
                    if (prevTicker != Utils.ParseToDouble(curRow["Id_Ativo"]))
                    {
                        if (prevTicker != 0) curItems.Add(prevTicker, tempDictionary);
                        tempDictionary = new SortedDictionary<DateTime, double>();
                    }

                    tempDictionary.Add (Utils.ParseToDateTime(curRow["Data_Hora_Reg"]), Utils.ParseToDouble(curRow["Valor"]));
                    prevTicker = Utils.ParseToDouble(curRow["Id_Ativo"]);
                }

                if(prevTicker!=0)
                    curItems.Add(prevTicker, tempDictionary);
            }
        }

        public double GetValue(double IdTicker, DateTime getDate)
        {
            SortedDictionary<DateTime, double> tempDictionary = new SortedDictionary<DateTime, double>();

            if (curItems.TryGetValue(IdTicker, out tempDictionary))
            {
                double curValue = 0;
                if (tempDictionary.TryGetValue(getDate, out curValue))
                {
                    return curValue;
                }
            }
            return 0;
        }
    }
}
