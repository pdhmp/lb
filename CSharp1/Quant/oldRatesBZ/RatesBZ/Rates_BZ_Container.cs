using System;
using System.Data;
using NestQuant.Common;

namespace NestQuant.Strategies
{
    public class Rates_BZ_Container : Strategy
    {
        public Rates_BZ_Container() : base()
        {
        }

        public override void RunStrategy()
        {
            /*
            DataTable dt;
            using (NestConn conn = new NestConn())
            {
                string SQLString = "SELECT Id_Ativo FROM NESTDB.dbo.Tb001_Ativos WHERE Simbolo like 'QSEG%' ORDER BY Id_Ativo";
                //string SQLString = "SELECT 16324 AS Id_Ativo";
                dt = conn.ExecuteDataTable(SQLString);
            }
            
            foreach (DataRow curRow in dt.Rows)
            {
            */    
                //int curId = Convert.ToInt16(curRow[0].ToString());

                Rates_BZ NewStrat = new Rates_BZ();
                NewStrat.iniDate = iniDate;
                NewStrat.endDate = endDate;
                NewStrat.DateRowInterval = Utils.TimeIntervals.IntervalDay;
                NewStrat.StrategyType = Utils.StrategyTypes.Undefined;
                NewStrat.Name = "RatesBZ";
                NewStrat.Id_Ticker_Composite = 27374;
                NewStrat.Id_Ticker_Template = 27374;
                NewStrat.RunStrategy();
                AddStrategy(NewStrat);
            //}

            //Load_Performances();
            //Fill_Weights_EW();
            //Fill_Contributions();

            //stratPerfSummary = new perfSummary_Table("perfSummary", 1073, iniDate, endDate);
            //stratPerfSummary.Fill(subContributions);

        }


    }
}
