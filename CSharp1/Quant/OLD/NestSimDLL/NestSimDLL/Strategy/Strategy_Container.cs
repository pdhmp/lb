using System;
using System.Collections.Generic;
using System.Text;

namespace NestQuant.Common
{
    public abstract class Strategy_Container: Strategy_Base
    {
        public List<Strategy> Strategies = new List<Strategy>();
        public List<Strategy_Container> Containers = new List<Strategy_Container>();

        private Strategy_Container _Parent;
        public Strategy_Container Parent
        {
            get { return _Parent; }
        }

        public Strategy_Container()
            : base(false)
        {
            _Parent = this;
        }

        public Strategy_Container(bool isRealTime)            
            :base(isRealTime)
        {
            _Parent = this;
        }

        public Strategy_Container(Strategy_Container StrategyContainerParent, bool isRealTime)
            :base(isRealTime)
        {
            this._Parent = StrategyContainerParent;
        }

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
                    double tempValue = (1F / subPerformances.ValueColumnCount);
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
                    double tempValue = subPerformances.GetValue(j, i) * subWeights.GetValue(j, i);
                    subContributions.SetValue(j, i, tempValue);
                }
                i++;
            }
            subContributions.AddRowSum(false);
        }

        public abstract void RunAll();

        public void Refresh_Performances()
        {
            int i = 0;

            int curDate = subPerformances.DateRowCount - 1;

            foreach (Strategy curStrategy in Strategies)
            {
                subPerformances.SetValue(curDate, i, curStrategy.stratPerfSummary.GetCustomValue(curDate, 0));
                i++;
            }
            
        }

        public void Refresh_Contributions()
        {
            int i = 0;

            int curDate = subPerformances.DateRowCount - 1;

            foreach (Strategy curStrategy in Strategies)
            {
                double tempValue = subPerformances.GetValue(curDate, i) * subWeights.GetValue(curDate, i);
                subContributions.SetValue(curDate, i, tempValue);
                i++;
            }

        }

    }
}
