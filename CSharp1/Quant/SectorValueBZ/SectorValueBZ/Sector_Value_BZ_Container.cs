using System;
using System.Data;

using NestQuant.Common;

namespace NestQuant.Strategies
{
    public class Sector_Value_BZ_Container : Strategy
    {
        public Sector_Value_BZ_Container()
            :
            base()
        {
        }

        public override void RunStrategy()
        {
            DataTable dt;
            using (NestConn conn = new NestConn())
            {
                //string SQLString = "SELECT Id_Ticker_Component FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=21350 GROUP BY Id_Ticker_Component ORDER BY Id_Ticker_Component";
                string SQLString = "SELECT Id_Ticker_Component FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Component NOT IN (16307,16310,16311,16317,16318) AND Id_Ticker_Composite=21350 GROUP BY Id_Ticker_Component ORDER BY Id_Ticker_Component";
                //string SQLString = "SELECT 16313 AS Id_Ativo";
                dt = conn.ExecuteDataTable(SQLString);
            }


            foreach (DataRow curRow in dt.Rows)
            {
                int curId = Convert.ToInt16(curRow[0].ToString());
                
                SectorValueBZ NewStrat = new SectorValueBZ();
                NewStrat.ID = curId;
                NewStrat.iniDate = iniDate;
                NewStrat.endDate = endDate;
                NewStrat.DateRowInterval = Utils.TimeIntervals.IntervalDay;
                NewStrat.StrategyType = Utils.StrategyTypes.SectorValue;
                string stratName = "";
                using (NestConn conn = new NestConn())
                {
                    stratName = conn.Execute_Query_String("SELECT Nome FROM NESTDB.dbo.Tb001_Ativos WHERE Id_Ativo=" + curId);
                }
                NewStrat.Name = stratName + " (" + curId + ")";
                NewStrat.Id_Ticker_Composite = curId;
                NewStrat.RunStrategy();
                AddStrategy(NewStrat);
            }

            this.ID = 99;

            Load_Performances();
            Fill_Weights_EW();
            Fill_Contributions();
            Consolidate_Position();
            Fill_Strategy_Performance();


            //stPerfSummary = new perfSummary_Table("perfSummary", 1073, iniDate, endDate);
            //stPerfSummary.Fill(stContributions, BmContributions);

        }


    }
}
