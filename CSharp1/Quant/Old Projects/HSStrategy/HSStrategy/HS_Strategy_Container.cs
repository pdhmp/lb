using System;
using System.Collections.Generic;
using System.Text;

using NestQuant.Common;

namespace NestQuant.Strategies
{
    public class HS_Strategy_Container: Strategy_Container
    {
        public HS_Strategy_Container()
            : base()
        {
        }
        
        public override void RunAll()
        {
            HS_Strategy HS = new HS_Strategy();



            HS.iniDate = iniDate;
            HS.endDate = endDate;

            HS.Name = "Head And Shoulders";
            HS.StrategyType = Utils.StrategyTypes.HeadAndShoulder;
            HS.Id_Ticker_Composite = 21141;
            HS.Id_Ticker_Template = 1073;
            HS.RunStrategy();
            AddStrategy(HS);

            Load_Performances();
            Fill_Weights_EW();
            Fill_Contributions();

            stratPerfSummary = new perfSummary_Table("perfSummary", 1073, iniDate, endDate);
            stratPerfSummary.Fill(subContributions);


        }
    }
}
