using System;
using System.Collections.Generic;
using System.Text;

using NestQuant.Common;

namespace CBMomentumBZ
{
    public class Sector_Performance : Strategy 
    {
        private int _RollWindow = 20;
        public int RollWindow
        {
            get { return _RollWindow; }
            set { _RollWindow = value; }
        }

        public Sector_Performance(bool isRealTime, Strategy parent) 
            : base(isRealTime,parent)
        {
        }           

        public override void RunStrategy()
        {
            //Prices
            Price_Table sectorPrices = new Price_Table("sectorPrices", Id_Ticker_Template, iniDate, endDate,IsRealTime);
            sectorPrices.FillStyle = Utils.TableFillTypes.FillPrevious;
            sectorPrices.FillFromComposite(Id_Ticker_Composite, 101, 1);
            PriceTables.Add(sectorPrices);

            //Weights
            Weight_Table sectorWeights = new Weight_Table("sectorWeights", Id_Ticker_Template, iniDate, endDate,IsRealTime);
            WeightTables.Add(sectorWeights);
            sectorWeights.FillStyle = Utils.TableFillTypes.FillPrevious;
            sectorWeights.FillFromComposite(Id_Ticker_Composite);

            //Signals
            Signal_Table curSignals = new Signal_Table("curSignals", Id_Ticker_Template, iniDate, endDate, IsRealTime);
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
            stPerformances = new Performances_Table("stPerformances", sectorPrices);
            stPerformances.ValueFormat = "0.00%; -0.00%; -";

            //Calculate stPositions
            stPositions = new PercPositions_Table(curSignals, sectorWeights);
            stPositions.AddRowGross();
            stPositions.AddRowNet();

            tkPositions = stPositions;
            tkPerformances = stPerformances;

            //Calculate Contributions
            stContributions = new Contributions_Table("stContributions", stPositions, stPerformances, true);

            //Calculate perfSummary
            Benchmark = Utils.BenchmarkTypes.FromPosition;
            Fill_Strategy_Performance(RollWindow, false);

            
            if (IsRealTime)
            {
                sectorPrices.TableChanged += new EventHandler(Refresh);
                sectorPrices.SubscribeRealTime(PriceFeeder);
            }
        }

        protected override void UpdateStrategy(object source, EventArgs e)
        {
            //Recalculate performance
            stPerformances.Refresh();

            //Recalculate Contributions
            stContributions.Refresh();

            //Recalculate perfSummary
            stPerfSummary.Refresh();
        }
    }
}
