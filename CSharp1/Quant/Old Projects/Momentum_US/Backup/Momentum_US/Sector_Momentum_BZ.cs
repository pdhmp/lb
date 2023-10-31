using System;
using System.Collections.Generic;
using System.Text;

using NestQuant.Common;

namespace NestQuant.Strategies
{
    class Sector_Momentum_US : Strategy
    {
        private int _MedianWindow = 5;
        public int MedianWindow
        {
            get { return _MedianWindow; }
            set { _MedianWindow = value; }
        }

        private int _RollWindow = 20;
        public int RollWindow
        {
            get { return _RollWindow; }
            set { _RollWindow = value; }
        }

        private Performances_Table subRollPerformances;

        public Sector_Momentum_US(bool isRealTime, Strategy parent)
            : base(isRealTime, parent)
        {
        }

        public override void RunStrategy()
        {
            //Initiate sector performance
            Sector_Performance newSectorPerformance = new Sector_Performance(IsRealTime,this);
            newSectorPerformance.Name = "Performance_" + Id_Ticker_Composite;
            newSectorPerformance.ID = Id_Ticker_Composite;
            newSectorPerformance.Id_Ticker_Template = Id_Ticker_Template;
            newSectorPerformance.Id_Ticker_Composite = Id_Ticker_Composite;
            newSectorPerformance.iniDate = iniDate;
            newSectorPerformance.endDate = endDate;
            newSectorPerformance.DateRowInterval = DateRowInterval;
            newSectorPerformance.StrategyType = StrategyType;
            newSectorPerformance.Weighing = Utils.WeighingSchemes.FromComposite;            

            if (IsRealTime)
            {
                newSectorPerformance.SetPriceFeeder(PriceFeeder);
            }

            //Run sector performance
            newSectorPerformance.RunStrategy();
            AddStrategy(newSectorPerformance);

            //Loads roll performances
            Load_Performances(true);
            subRollPerformances = stPerformances;

            //Generate strategy positions                        
            Fill_Signals();
            Fill_Weights_EW();
            Fill_Positions();

            //Generate strategy performance
            Load_Performances(false);

            stContributions = new Contributions_Table("stContributions", stPositions, stPerformances,false);

            Consolidate_Position();

            Benchmark = Utils.BenchmarkTypes.StrategyEqualWeighed;
            Fill_Strategy_Performance(RollWindow, false);

            
        }       

        private void Fill_Signals()
        {
            Signal_Table SubSignals = new Signal_Table("SubSignals", Id_Ticker_Template, iniDate, endDate, IsRealTime);

            for (int j = 0; j < subRollPerformances.ValueColumnCount; j++)
            {
                int LastSignal = 0;
                int NewSignal = 0;

                int Id_Value = subRollPerformances.GetValueColumnID(j);

                if (Id_Value != -1)
                {
                    if (SubSignals.GetValueColumnIndex(Id_Value) == -1)
                    {
                        int Idx_Signal = SubSignals.AddValueColumn(Id_Value);
                        for (int i = 0; i < subRollPerformances.DateRowCount; i++)
                        {
                            if (i < MedianWindow - 1)
                            {
                                NewSignal = 0;
                            }
                            else
                            {
                                double[] medianSample = new double[MedianWindow];

                                for (int k = i; k > i - MedianWindow; k--)
                                {
                                    medianSample[i - k] = subRollPerformances.GetValue(k, j);
                                }

                                //double median = Utils.calcMedian(medianSample, false);

                                int median = checkSignal(LastSignal, medianSample);

                                if (median < 0)
                                {
                                    NewSignal = -1;
                                }
                                else if (median > 0)
                                {
                                    NewSignal = 1;
                                }
                                else
                                {
                                    NewSignal = 0;
                                }
                            }
                        
                            SubSignals.SetValue(i, Idx_Signal, NewSignal);
                            LastSignal = NewSignal;
                        }
                    }
                    else
                    {
                        throw new Exception("Multiple Id_Value("+Id_Value+") entries in auxSubSignals table");
                    }
                }
                else
                {
                    throw new Exception("Unable to find Id_Value for index " + j + " in subRollPerformances table");
                }
            }

            SignalTables.Add(SubSignals);

        }

        private int checkSignal(int LastSignal, double[] SignalReference)
        {
            bool change = true;
            int refSignal = (SignalReference[0] >= 0 ? 1 : -1);

            for (int i = 1; i < SignalReference.Length; i++)
            {
                int compare = (SignalReference[i] >= 0 ? 1 : -1);

                if (compare != refSignal)
                {
                    change = false;
                }
            }

            if (change)
            {
                return refSignal;
            }
            else
            {
                return LastSignal;
            }

        }
        private void Fill_Positions()
        {
            for (int j = 0; j < stPositions.ValueColumnCount; j++)
            {
                int Id_Value = stPositions.GetValueColumnID(j);

                if (Id_Value != -1)
                {
                    int Idx_Signal = SignalTables[0].GetValueColumnIndex(Id_Value);

                    if (Idx_Signal != -1)
                    {
                        for (int i = 0; i < stPositions.DateRowCount; i++)
                        {
                            double position = stPositions.GetValue(i, j) * SignalTables[0].GetValue(i, Idx_Signal);
                            stPositions.SetValue(i, j, position);                            
                        }
                    }
                    else
                    {
                        throw new Exception("Unable to find index for Id_Value " + Id_Value + " in SubSignals table");
                    }
                }
                else
                {
                    throw new Exception("Unable to find Id_Value for index " + j + " in stPositions table");
                }
            }
            stPositions.AddRowGross();
            stPositions.AddRowNet();
        }
    }       
}
