using System;
using System.Collections.Generic;
using System.Text;

using NestSimDLL;

namespace NestSim
{
    public class SectorValueBZ : Strategy
    {
        public Price_Table curPrices;
        public Price_Table curPEs;
        public Price_Table curSignals;

        public int Id_Ticker_Composite;

        public override void RunStrategy() 
        {
            // Create Object

            Id_Ticker_Template = 1073;

            Weight_Table curWeights = new Weight_Table("subWeights", Id_Ticker_Template, iniDate, endDate);
            WeightTables.Add(curWeights);
            curWeights.FillStyle = Utils.TableFillTypes.FillPrevious;
            curWeights.FillFromComposite(Id_Ticker_Composite);
            curWeights.AddCountNonZero();
            curWeights.Convert_EW();

            Weighing = Utils.WeighingSchemes.EqualWeighed;

            curPrices = new Price_Table("curPrices", Id_Ticker_Template, iniDate, endDate);
            PriceTables.Add(curPrices);
            curPrices.FillStyle = Utils.TableFillTypes.FillPrevious;
            curPrices.FillFromComposite(Id_Ticker_Composite, 101, 1);
            //curPrices = Array_SetZero(curPrices, curWeights);

            curPEs = new Price_Table("curPEs", Id_Ticker_Template, iniDate, endDate);
            PriceTables.Add(curPEs);
            curPEs.FillStyle = Utils.TableFillTypes.FillZero;
            curPEs.FillFromComposite(Id_Ticker_Composite, 500, 7);
            curPEs = Utils.Tables.SetZero(curPEs, (Base_Table)curWeights);
            curPEs = Utils.Tables.FixNegPEs(curPEs);
            curPEs.AddCustomMedian();

            subPerformances = new Performances_Table("subPerformances", curPrices);
            subPerformances.ValueFormat = "0.00%; -0.00%; -";

            curSignals = new Price_Table("curSignals", Id_Ticker_Template, iniDate, endDate);
            SignalTables.Add(curSignals);
            curSignals.ZeroFillFromComposite(Id_Ticker_Composite);
            Array_CalcSignals(curSignals, curPEs);

            subWeights = new PercPositions_Table(curSignals, curWeights);
            subWeights.AddGross();
            subWeights.AddLong();
            subWeights.AddShort();
            subWeights.AddNet();
            subWeights.AddCountNonZero();

            subContributions = new Contributions_Table("subContributions", subWeights, subPerformances);

            stratPerfSummary = new perfSummary_Table("stratPerfSummary", subContributions);
            
        }
        
        private void Array_CalcSignals(Price_Table perfTable, Price_Table PE_Table)
        {
            for (int i = 1; i < PE_Table.DateRowCount; i++)
            {
                for (int j = 0; j < PE_Table.ValueColumnCount; j++)
                {
                    int curSignal = 0;
                    if (PE_Table.GetValue(i, j) == 0)
                    {
                        curSignal = 0;
                    }
                    else
                    {
                        if (PE_Table.GetValue(i, j) > PE_Table.GetCustomValue(i, 0))
                        {
                            curSignal = -1;
                        }
                        if (PE_Table.GetValue(i, j) < PE_Table.GetCustomValue(i, 0))
                        {
                            curSignal = 1;
                        }
                    }
                    perfTable.SetValue(i, j, curSignal);
                }
            }
        }

    }
}
