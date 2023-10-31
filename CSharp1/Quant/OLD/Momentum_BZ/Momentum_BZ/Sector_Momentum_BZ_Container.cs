using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

using NestQuant.Common;

namespace NestQuant.Strategies
{
    public class Sector_Momentum_BZ_Container : Strategy_Container
    {
        private Performances_Table rollPerformance;
        private Price_Table subSignals, auxSubSignals;
        private Weight_Table weight, adjustedWeight;
        private PercPositions_Table auxSubWeights;
        private Contributions_Table auxContributions;
        private perfSummary_Table auxPerfSummary;


        private int _Id_Ticker_Composite = 21350;
        public int Id_Ticker_Composite
        {
            get { return _Id_Ticker_Composite; }
            set { _Id_Ticker_Composite = value; }
        }

        private double _targetVol = 0.0090F;
        public double TargetVol
        {
            get { return _targetVol; }
            set { _targetVol = value; }
        }

        int medianDays = 11;     
        private int _rollWindow = 20;
        public int rollWindow
        {
            get { return _rollWindow; }
            set { _rollWindow = value; }
        }
	
        public Sector_Momentum_BZ_Container(bool isRealTime)
            : base(isRealTime)
        {
        }

        public override void RunAll()
        {
            DataTable dt;
            using (NestConn conn = new NestConn())
            {
                string SQLString = "SELECT ID_TICKER_COMPONENT FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION"+
                                   "\r\nWHERE ID_TICKER_COMPOSITE = 21350"+
                                   "\r\nGROUP BY ID_TICKER_COMPONENT";
                dt = conn.ExecuteDataTable(SQLString);
            }

            foreach (DataRow curRow in dt.Rows)
            {
                int curId = Convert.ToInt16(curRow[0].ToString());
                
                Sector_Momentum_BZ NewStrat = new Sector_Momentum_BZ(IsRealTime);
                NewStrat.iniDate = iniDate;
                NewStrat.endDate = endDate;
                NewStrat.DateRowInterval = Utils.TimeIntervals.IntervalDay;
                NewStrat.StrategyType = Utils.StrategyTypes.Undefined;
                NewStrat.Name = "Performance_" + curId;
                NewStrat.Id_Ticker_Composite = curId;
                NewStrat.Id_Ticker_Template = Id_Ticker_Template;
                if (IsRealTime)
                {
                    NewStrat.SetPriceFeeder(PriceFeeder);
                }
                NewStrat.RunStrategy();
                AddStrategy(NewStrat);
            }

            weight = new Weight_Table("weight", Id_Ticker_Template, iniDate, endDate,IsRealTime);
            weight.FillStyle = Utils.TableFillTypes.FillPrevious;
            weight.FillFromComposite(Id_Ticker_Composite);

            adjustedWeight = new Weight_Table("weight", Id_Ticker_Template, iniDate, endDate, IsRealTime);
            adjustedWeight.ZeroFillFromComposite(Id_Ticker_Composite);
            
            Weighing = Utils.WeighingSchemes.FromComposite;

            CalculateStrategy();

            if (IsRealTime)
            {
                foreach (Strategy NewStrategy in Strategies)
                {
                    NewStrategy.StrategyChanged += new EventHandler(Refresh);
                }
            }
        }

        private void CalculateStrategy()
        {
            Load_Performances();

            Fill_Roll_Performance();

            Fill_Signals();

            //Pressupondo que seja possível comprar o setor
            auxSubWeights = new PercPositions_Table(subSignals, weight);

            auxContributions = new Contributions_Table("auxContributions", auxSubWeights, subPerformances);

            auxPerfSummary = new perfSummary_Table("auxPerfSummary", auxContributions);

            //Adjust weights based on the strategy previous performance
            Adjust_Weight();

            //Print_Table(weight, "C:/temp/tableanalysis/post_Weight.txt");

            subWeights = new PercPositions_Table(subSignals, adjustedWeight);

            //Print_Table(subWeights, "C:/temp/tableanalysis/post_Positions.txt");

            subContributions = new Contributions_Table("subContributions", subWeights, subPerformances);

            //Print_Table(subContributions, "C:/temp/tableanalysis/post_Contributions.txt");

            stratPerfSummary = new perfSummary_Table("perfSummary", subContributions);

            //Print_Table(stratPerfSummary, "C:/temp/tableanalysis/post_stratPerfSummary.txt");
        }

        private void Fill_Roll_Performance()
        {
            rollPerformance = new Performances_Table("rollPerformance", Strategies[0], Strategies.Count);

            for (int i = 0; i < subPerformances.ValueColumnCount; i++)
            {
                for (int j = 0; j < subPerformances.DateRowCount; j++)
                {
                    int k = j - rollWindow + 1;
                    double tempValue = 1F;

                    if (k >= 0)
                    {
                        for (; k <= j; k++)
                        {
                            tempValue = tempValue * (subPerformances.GetValue(k, i) + 1);
                        }
                    }

                    tempValue = tempValue - 1;
                    rollPerformance.SetValue(j, i, tempValue);                   
                }
            }


        }

        private void Fill_Signals()
        {
            auxSubSignals = new Price_Table("subSignals", Id_Ticker_Template, iniDate, endDate, IsRealTime);
            auxSubSignals.ZeroFillFromComposite(Id_Ticker_Composite);

            for (int i = 0; i < rollPerformance.DateRowCount; i++) 
            {
                for (int j = 0; j < rollPerformance.ValueColumnCount; j++)
                {
                    if (i < rollWindow-1)
                    {
                        auxSubSignals.SetValue(i, j, 0);
                    }
                    else
                    {                       
                        double perf = rollPerformance.GetValue(i, j);
                        if (perf > 0.000000001)
                        {
                            auxSubSignals.SetValue(i, j, 1);
                        }
                        else if (perf < -0.000000001)
                        {
                            auxSubSignals.SetValue(i, j, -1);
                        }
                        else
                        {
                            auxSubSignals.SetValue(i, j, 0);
                        }
                    }
                }
            }

            //Calculate signal 10-day median

            subSignals = new Price_Table("subSignals", Id_Ticker_Template, iniDate, endDate, IsRealTime);
            subSignals.ZeroFillFromComposite(Id_Ticker_Composite);

            for (int j = 0; j < auxSubSignals.ValueColumnCount; j++)
            {
                for (int i = medianDays - 1; i < auxSubSignals.DateRowCount; i++)
                {
                    double[] medianWindow = new double[medianDays];

                    for (int k = i; k > i - medianDays; k--)
                    {
                        medianWindow[i - k] = auxSubSignals.GetValue(k, j);
                    }
                    
                    double median = Utils.calcMedian(medianWindow);
                    
                    subSignals.SetValue(i, j, median);
                }
            }
        }

        private void Adjust_Weight()
        {
            double[] sample = new double[rollWindow];
            double stdev;
            double adjustFactor;

            for (int i = 0; i < auxPerfSummary.DateRowCount; i++)
            {
                if (i < rollWindow)
                {
                    for (int j = 0; j < weight.ValueColumnCount; j++)
                    {
                        adjustedWeight.SetValue(i, j, weight.GetValue(i, j));
                    }
                }
                else
                {

                    for (int k = i - rollWindow; k < i; k++)
                    {
                        sample[k - i + rollWindow] = auxPerfSummary.GetCustomValue(k, 0);
                    }

                    stdev = Utils.calcStdev(ref sample);

                    if (stdev != 0)
                    {
                        adjustFactor = (TargetVol / (stdev * Utils.NORMSINV95));
                    }
                    else
                    {
                        adjustFactor = 1F;
                    }

                    if (adjustFactor > 1) { adjustFactor = 1F; };

                    for (int j = 0; j < adjustedWeight.ValueColumnCount; j++)
                    {
                        double aux = weight.GetValue(i, j);
                        aux = aux * adjustFactor;
                        adjustedWeight.SetValue(i, j, aux);
                    }
                }        
            }
        }

        private void Refresh_Roll_Performance()
        {
            int curDate = subPerformances.DateRowCount - 1;

            for (int i = 0; i < subPerformances.ValueColumnCount; i++)
            {
                int k = curDate - rollWindow + 1;
                double tempValue = 1F;

                if (k >= 0)
                {
                    for (; k <= curDate; k++)
                    {
                        tempValue = tempValue * (subPerformances.GetValue(k, i) + 1);
                    }
                }

                tempValue = tempValue - 1;
                rollPerformance.SetValue(curDate, i, tempValue);                
            }
        }

        private void Refresh_Signals()
        {
            int curDate =  rollPerformance.DateRowCount -1;
            
            for (int j = 0; j < rollPerformance.ValueColumnCount; j++)
            {
                if (curDate < rollWindow - 1)
                {
                    auxSubSignals.SetValue(curDate, j, 0);
                }
                else
                {
                    double perf = rollPerformance.GetValue(curDate, j);
                    if (perf > 0.000000001)
                    {
                        auxSubSignals.SetValue(curDate, j, 1);
                    }
                    else if (perf < -0.000000001)
                    {
                        auxSubSignals.SetValue(curDate, j, -1);
                    }
                    else
                    {
                        auxSubSignals.SetValue(curDate, j, 0);
                    }
                }
            }

            for (int j = 0; j < auxSubSignals.ValueColumnCount; j++)
            {
                double[] medianWindow = new double[medianDays];

                for (int k = curDate; k > curDate - medianDays; k--)
                {
                    medianWindow[curDate - k] = auxSubSignals.GetValue(k, j);
                }

                double median = Utils.calcMedian(medianWindow);

                subSignals.SetValue(curDate, j, median);                
            }
        }

        private void Refresh_Adjusted_Weights()
        {
            double[] sample = new double[rollWindow];
            double stdev;
            double adjustFactor;

            int curDate = auxPerfSummary.DateRowCount - 1;
            
            if (curDate < rollWindow)
            {
                for (int j = 0; j < weight.ValueColumnCount; j++)
                {
                    adjustedWeight.SetValue(curDate, j, weight.GetValue(curDate, j));
                }
            }
            else
            {

                for (int k = curDate - rollWindow; k < curDate; k++)
                {
                    sample[k - curDate + rollWindow] = auxPerfSummary.GetCustomValue(k, 0);
                }

                stdev = Utils.calcStdev(ref sample);

                if (stdev != 0)
                {
                    adjustFactor = (TargetVol / (stdev * Utils.NORMSINV95));
                }
                else
                {
                    adjustFactor = 1F;
                }

                if (adjustFactor > 1) { adjustFactor = 1F; };

                for (int j = 0; j < adjustedWeight.ValueColumnCount; j++)
                {
                    double aux = weight.GetValue(curDate, j);
                    aux = aux * adjustFactor;
                    adjustedWeight.SetValue(curDate, j, aux);
                }
            }           
        }

        protected override void UpdateStrategy(object source, EventArgs e)
        {
            Refresh_Performances();

            Refresh_Roll_Performance();

            Refresh_Signals();

            auxSubWeights.Refresh();

            auxContributions.Refresh();

            auxPerfSummary.Refresh();

            Refresh_Adjusted_Weights();

            subWeights.Refresh();

            subContributions.Refresh();

            stratPerfSummary.Refresh();           

        }
        
        private void Print_Table(Base_Table table, string filePath)
        {
            StreamWriter file = new StreamWriter(filePath,false);

            string line = "";

            line = line + "Date;";

            for (int i = 0; i < table.ValueColumnCount; i++)
            {
                line = line + "Value "+ i.ToString() + ";";
            }

            for (int i = 0; i < table.CustomColumnCount; i++)
            {
                line = line + "Custom " + i.ToString() + ";";
            }

            file.WriteLine(line);

            for (int i = 0; i < table.DateRowCount; i++)
            {
                line = table.DateRows[i].ToString() + ";";

                for (int j = 0; j < table.ValueColumnCount; j++)
                {
                    line = line + table.GetValue(i, j).ToString() + ";";
                }

                for (int j = 0; j < table.CustomColumnCount; j++)
                {
                    line = line + table.GetCustomValue(i, j).ToString() + ";";
                }

                file.WriteLine(line);
            }

            file.Dispose();
            file = null;
            GC.Collect();
        }         
    }
}
