using System;
using System.Data;

using NestSimDLL;

namespace NestSim
{
    class Sector_Value_BZ_Container : Strategy_Container
    {
        public Sector_Value_BZ_Container()
            :
            base()
        {
        }

        public override void RunAll()
        {
            DataTable dt;
            using (NestConn conn = new NestConn())
            {
                string SQLString = "SELECT Id_Ativo FROM NESTDB.dbo.Tb001_Ativos WHERE Simbolo like 'QSEG%' ORDER BY Id_Ativo";
                //string SQLString = "SELECT 16324 AS Id_Ativo";
                dt = conn.ExecuteDataTable(SQLString);
            }

            foreach (DataRow curRow in dt.Rows)
            {
                int curId = Convert.ToInt16(curRow[0].ToString());
                
                SectorValueBZ NewStrat = new SectorValueBZ();
                NewStrat.iniDate = iniDate;
                NewStrat.endDate = endDate;
                NewStrat.DateRowInterval = Utils.TimeIntervals.IntervalDay;
                NewStrat.StrategyType = Utils.StrategyTypes.SectorValue;
                NewStrat.Name = "ValueBZ_" + curId;
                NewStrat.Id_Ticker_Composite = curId;
                NewStrat.RunStrategy();
                AddStrategy(NewStrat);
            }

            Load_Performances();
            Fill_Weights_EW();
            Fill_Contributions();

            stratPerfSummary = new perfSummary_Table("perfSummary", 1073, iniDate, endDate);
            stratPerfSummary.Fill(subContributions);

        }


    }
}
