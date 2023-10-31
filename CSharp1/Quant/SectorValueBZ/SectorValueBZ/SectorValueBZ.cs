using System;
using System.Collections.Generic;
using System.Text;

using NestQuant.Common;

namespace NestQuant.Strategies
{
    public class SectorValueBZ : Strategy
    {
        public Price_Table curPrices;
        public Price_Table curPEs;
        public Signal_Table curSignals;

        public SectorValueBZ():base()
        { 
        }

        public override void RunStrategy() 
        {
            // Create Object

            Id_Ticker_Template = 1073;

            Weight_Table curWeights = new Weight_Table("tkPositions", Id_Ticker_Template, iniDate, endDate);
            WeightTables.Add(curWeights);
            curWeights.FillStyle = Utils.TableFillTypes.FillPrevious;
            curWeights.FillFromComposite(Id_Ticker_Composite, true);
            curWeights.AddRowCountNonZero();
            curWeights.Convert_EW(true);

            Weighing = Utils.WeighingSchemes.EqualWeighed;

            curPrices = new Price_Table("curPrices", Id_Ticker_Template, iniDate, endDate);
            PriceTables.Add(curPrices);
            curPrices.FillStyle = Utils.TableFillTypes.FillPrevious;
            curPrices.FillFromComposite(Id_Ticker_Composite, 101, 1, true);

            curPEs = new Price_Table("curPEs", Id_Ticker_Template, iniDate, endDate);
            PriceTables.Add(curPEs);
            curPEs.FillStyle = Utils.TableFillTypes.FillZero;
            curPEs.FillFromComposite(Id_Ticker_Composite, 500, 7, false);
            curPEs = Utils.Tables.SetNull(curPEs, curWeights, true);
            curPEs = Utils.Tables.FixNegPEs(curPEs);
            curPEs.AddRowMedian();

            tkPerformances = new Performances_Table("tkPerformances", curPrices);
            tkPerformances.ValueFormat = "0.00%; -0.00%; -";

            curSignals = new Signal_Table("curSignals", Id_Ticker_Template, iniDate, endDate);
            SignalTables.Add(curSignals);
            curSignals.ZeroFillFromComposite(Id_Ticker_Composite);
            Array_CalcSignals(curSignals, curPEs);

            tkPositions = new PercPositions_Table(curSignals, curWeights);
            tkPositions.AddRowGross();
            tkPositions.AddRowLong();
            tkPositions.AddRowShort();
            tkPositions.AddRowNet();
            tkPositions.AddRowCountNonZero();

            tkContributions = new Contributions_Table("tkContributions", tkPositions, tkPerformances);

            stPerfSummary = new perfSummary_Table("stratPerfSummary", tkContributions, tkContributions);
            
        }
        
        private void Array_CalcSignals(Price_Table signalTable, Price_Table PE_Table)
        {
            signalTable.ValueColumnType = Utils.TableTypes.Id_Ticker;
            for (int i = 1; i < PE_Table.DateRowCount; i++)
            {
                for (int j = 0; j < PE_Table.ValueColumnCount; j++)
                {
                    int curSignal = 0;
                    // This part below should not take into consideration zero values
                    if (double.IsNaN(PE_Table.GetValue(i, j)) || PE_Table.GetValue(i, j) == 0)
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
                    signalTable.SetValue(i, j, curSignal);
                }
            }
        }

    }
}
