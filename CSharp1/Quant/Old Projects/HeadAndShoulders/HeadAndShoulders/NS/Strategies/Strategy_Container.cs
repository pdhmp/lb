using System;
using System.Collections.Generic;
using System.Text;

namespace NestSim
{
    public class Strategy_Container: Strategy_Base
    {
        public List<Strategy> Strategies = new List<Strategy>();
        public List<Strategy_Container> Containers = new List<Strategy_Container>();

        public void AddStrategy(Strategy StrategyToAdd)
        {
            Strategies.Add(StrategyToAdd);
        }

        public void AddContainer(Strategy_Container ContainerToAdd)
        {
            Containers.Add(ContainerToAdd);
        }

        public void Load_Performances()
        {
            subPerformances = new Performances_Table("stratPerformances", Strategies[0], Strategies.Count);

            int i = 0;

            foreach (Strategy curStrategy in Strategies)
            {
                for (int j = 0; j < curStrategy.stratPerfSummary.DateRowCount; j++)
                {
                    subPerformances.SetValue(j, i, curStrategy.stratPerfSummary.GetCustomValue(j, 0));
                }
                i++;
            }
            
        }
        
        public void Fill_Weights_EW()
        {
            subWeights = new PercPositions_Table("StratWeights", Strategies[0], Strategies.Count);
            //subWeights.ZeroFill(Strategies.Count);

            Weighing = Utils.WeighingSchemes.EqualWeighed;

            int i=0;

            foreach (Strategy curStrategy in Strategies)
            {
                for (int j = 0; j < curStrategy.stratPerfSummary.DateRowCount; j++)
                {
                    float tempValue = (1F / subPerformances.ValueColumnCount);
                    subWeights.SetValue(j, i, tempValue);
                }
                i++;
            }
        }

        public void Fill_Contributions()
        {
            subContributions = new Contributions_Table("subContributions", Strategies[0], Strategies.Count);

            int i=0;

            foreach (Strategy curStrategy in Strategies)
            {
                for (int j = 0; j < curStrategy.stratPerfSummary.DateRowCount; j++)
                {
                    float tempValue = subPerformances.GetValue(j, i) * subWeights.GetValue(j, i);
                    subContributions.SetValue(j, i, tempValue);
                }
                i++;
            }
            subContributions.AddCustomSum();
        }

    }
}
