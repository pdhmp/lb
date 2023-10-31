using System;
using System.Collections.Generic;
using System.Text;

using NestQuant.Common;

namespace NestQuant.Strategies
{
    public class Sector_Momentum_BZ : Strategy
    {
        //public Price_Table curPrices;
        //public Price_Table curPEs;
        //public Price_Table curSignals;

        public Sector_Momentum_BZ(bool isRealTime)
            : base(isRealTime)
        {
        }

        public int Id_Ticker_Composite;

        public override void RunStrategy()
        {
            //Prices
            Price_Table sectorPrices = new Price_Table("sectorPrices", Id_Ticker_Template, iniDate, endDate,IsRealTime);
            sectorPrices.FillStyle = Utils.TableFillTypes.FillPrevious;
            sectorPrices.FillFromComposite(Id_Ticker_Composite, 101, 1);
            PriceTables.Add(sectorPrices);

            //Weights
            Weighing = Utils.WeighingSchemes.FromComposite;
            Weight_Table sectorWeights = new Weight_Table("sectorWeights", Id_Ticker_Template, iniDate, endDate,IsRealTime);
            WeightTables.Add(sectorWeights);
            sectorWeights.FillStyle = Utils.TableFillTypes.FillPrevious;
            sectorWeights.FillFromComposite(Id_Ticker_Composite);

            //Signals
            Price_Table curSignals = new Price_Table("curSignals", Id_Ticker_Template, iniDate, endDate, IsRealTime);
            SignalTables.Add(curSignals);
            curSignals.FillStyle = Utils.TableFillTypes.FillPrevious;
            curSignals.FillFromComposite(Id_Ticker_Composite, 1, 1);

            for (int i = 0; i < curSignals.DateRowCount; i++)
            {
                for (int j = 0; j < curSignals.ValueColumnCount; j++)
                {
                    if (sectorWeights.GetValue(i, j) != 0)
                    {
                        curSignals.SetValue(i, j, 1);
                    }
                    else
                    {
                        curSignals.SetValue(i, j, 0);
                    }
                    
                }

            }

            //Calculate performance
            subPerformances = new Performances_Table("subPerformances", sectorPrices);
            subPerformances.ValueFormat = "0.00%; -0.00%; -";

            //Calculate subWeights
            subWeights = new PercPositions_Table(curSignals, sectorWeights);

            //Calculate Contributions
            subContributions = new Contributions_Table("subContributions", subWeights, subPerformances, true);

            //Calculate perfSummary
            stratPerfSummary = new perfSummary_Table("stratPerfSummary", subContributions);

            if (IsRealTime)
            {
                sectorPrices.TableChanged += new EventHandler(Refresh);
                sectorPrices.SubscribeRealTime(PriceFeeder);
            }
        }

        protected override void UpdateStrategy(object source, EventArgs e)
        {
            //Recalculate performance
            subPerformances.Refresh();

            //Recalculate Contributions
            subContributions.Refresh();

            //Recalculate perfSummary
            stratPerfSummary.Refresh();
        }
    }
}
