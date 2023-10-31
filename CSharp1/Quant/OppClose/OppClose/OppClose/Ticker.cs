using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NestDLL;

using STATCONNECTORCLNTLib;
using StatConnectorCommonLib;
using STATCONNECTORSRVLib;

namespace OppClose
{
    class Ticker
    {
        int ticker;
        DateTime IniDate;
        DateTime LastDate;
        RCalculator Calc;

        public Dictionary<DateTime, DayValues> Statistics = new Dictionary<DateTime, DayValues>();
        
        public Ticker(int _ticker,DateTime _IniDate, DateTime _LastDate)
        {
            ticker = _ticker;
            IniDate = _IniDate;
            LastDate = _LastDate;
            Calc = new RCalculator(_ticker,_IniDate,_LastDate);
            Calculate();
        }

        public void Calculate()
        {
            StatConnector RConn;

            bool GotRegret = true;
            int i = Calc.ValuesList.Count;            
            DateTime dateAux = Calc.ValuesList.Last().Key;                        

            i--;

            RConn = new StatConnector();
            RConn.Init("R");

            while (dateAux > IniDate && GotRegret)
            {                 
                Values DayData = new Values(), RegretUpResults = new Values(), RegretDownResults = new Values();
                DayValues Values = new DayValues();

                if (Calc.ValuesList.TryGetValue(dateAux, out DayData))
                {
                    Values.Var = Calc.ValuesList.Values.ElementAt(i).Var;

                    DateTime dateAuxRegret = dateAux;                    
                    dateAuxRegret = dateAuxRegret.AddDays(-1);

                    while (!Calc.ValuesList.ContainsKey(dateAuxRegret))
                    {
                        dateAuxRegret = dateAuxRegret.AddDays(-1);
                    }
                    i--;                                       

                    GotRegret = Calc.GetRegret(RConn, Calc.ValuesList, dateAuxRegret, out RegretUpResults, out RegretDownResults);

                    if (!GotRegret)
                    { }

                    double UpMMult, DownMMult;

                    UpMMult = RegretUpResults.ultVwap * DayData.ultVwap +
                                RegretUpResults.ultAbe * DayData.ultAbe +
                                RegretUpResults.ultUltM1 * DayData.ultUltM1 +
                                RegretUpResults.UltTm1Abe * DayData.UltTm1Abe +
                                RegretUpResults.Estocastico * DayData.Estocastico +
                                RegretUpResults.UltVwapM1 * DayData.UltVwapM1;

                    DownMMult = RegretDownResults.ultVwap * DayData.ultVwap +
                                RegretDownResults.ultAbe * DayData.ultAbe +
                                RegretDownResults.ultUltM1 * DayData.ultUltM1 +
                                RegretDownResults.UltTm1Abe * DayData.UltTm1Abe +
                                RegretDownResults.Estocastico * DayData.Estocastico +
                                RegretDownResults.UltVwapM1 * DayData.UltVwapM1;

                    Values.ProbUp = Math.Exp(UpMMult + RegretUpResults.Cte) / (1 + Math.Exp(UpMMult + RegretUpResults.Cte));
                    Values.ProbDown = Math.Exp(DownMMult + RegretDownResults.Cte) / (1 + Math.Exp(DownMMult + RegretDownResults.Cte));

                    Statistics.Add(dateAux, Values);

                    dateAux = dateAux.AddDays(-1);

                    while (!Calc.ValuesList.ContainsKey(dateAux))
                    {
                        dateAux = dateAux.AddDays(-1);
                    }

                }

            }

            RConn.Close();
        }

        public string ToString()
        {
            string SqlTicker =
                    "SELECT NestTicker "+
                    "FROM NESTDB.dbo.Tb001_Securities "+
                    "WHERE IdSecurity = " + ticker;            
            
            string result = "";

            using (newNestConn conn = new newNestConn())
            {
                DataTable Tb = conn.Return_DataTable(SqlTicker);
                result = Tb.Rows[0][0].ToString();
            }

            return result;

        }
    }

    class DayValues
    {
        public double ProbUp;
        public double ProbDown;
        public double Var;
    }
}
