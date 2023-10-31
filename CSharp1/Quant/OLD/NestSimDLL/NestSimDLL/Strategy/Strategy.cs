using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NestQuant.Common
{
    public abstract class Strategy
    {
        public SortedDictionary<int, Strategy> Strategies = new SortedDictionary<int, Strategy>();
       
        #region Fields

        protected string _Name = "";
        protected int _ID = int.MinValue;
        protected DateTime _iniDate;
        protected DateTime _endDate;
        protected Utils.StrategyTypes _StrategyType = Utils.StrategyTypes.Undefined;
        protected Utils.TimeIntervals _StrategyFrequency = Utils.TimeIntervals.Undefined;
        protected Utils.WeighingSchemes _StrategyWeighing = Utils.WeighingSchemes.Undefined;
        protected Utils.BenchmarkTypes _StrategyBenchmark = Utils.BenchmarkTypes.StrategyEqualWeighed;
        protected int _Id_Ticker_Template = 0;
        protected Strategy _Parent;
        private int _Id_Ticker_Composite;        

        #region Realtime Fields

        protected bool _IsRealTime;
        protected RTPrice _PriceFeeder;
        protected Mutex StrategyMutex = new Mutex();
        public event EventHandler StrategyChanged;  

        #endregion

        #endregion

        #region Tables

        public Trade TradeList;
        
        public PercPositions_Table stPositions;
        public Performances_Table stPerformances;
        public Contributions_Table stContributions;
        public perfSummary_Table stPerfSummary;

        public PercPositions_Table BmPositions;
        public Performances_Table BmPerformances;
        public Contributions_Table BmContributions;
        
        public PercPositions_Table tkPositions;
        public Performances_Table tkPerformances;
        public Contributions_Table tkContributions;        

        public List<Price_Table> PriceTables = new List<Price_Table>();
        public List<Weight_Table> WeightTables = new List<Weight_Table>();
        public List<Signal_Table> SignalTables = new List<Signal_Table>();
       
        #endregion

        #region Properties

        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
        public int ID
        {
            get { return _ID; }
            set
            {
                if (_ID == int.MinValue)
                {
                    _ID = value;
                }
            }
        }
        public int Id_Ticker_Template
        {
            get
            {
                return _Id_Ticker_Template;
            }
            set
            {
                if (_Id_Ticker_Template == value)
                    return;
                _Id_Ticker_Template = value;
            }
        }
        public int Id_Ticker_Composite
        {
            get { return _Id_Ticker_Composite; }
            set { _Id_Ticker_Composite = value; }
        }
        public DateTime iniDate
        {
            get
            {
                return _iniDate;
            }
            set
            {
                _iniDate = value;
            }
        }
        public DateTime endDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }
        public Utils.TimeIntervals DateRowInterval
        {
            get
            {
                return _StrategyFrequency;
            }
            set
            {
                _StrategyFrequency = value;
            }
        }
        public Utils.StrategyTypes StrategyType
        {
            get
            {
                return _StrategyType;
            }
            set
            {
                _StrategyType = value;
            }
        }
        public Utils.WeighingSchemes Weighing
        {
            get
            {
                return _StrategyWeighing;
            }
            set
            {
                if (_StrategyWeighing == value)
                    return;
                _StrategyWeighing = value;
            }
        }
        public Utils.BenchmarkTypes Benchmark
        {
            get { return _StrategyBenchmark; }
            set { _StrategyBenchmark = value; }
        }
        public bool IsRealTime
        {
            get { return _IsRealTime; }
        }
        public RTPrice PriceFeeder
        {
            get { return _PriceFeeder; }
        }
        public Strategy Parent
        {
            get { return _Parent; }
        }                      

        #endregion

        #region Constructors

        public Strategy()
            : this(false, null)
        {
        }

        public Strategy(bool isRealTime)
            : this(isRealTime, null)
        {
        }

        public Strategy(Strategy parent)
            : this(false, parent)
        {
        }

        public Strategy(bool isRealTime, Strategy parent)
        {
            _IsRealTime = isRealTime;
            _Parent = parent;
        }

        #endregion
        
        #region Methods

        /// <summary>
        /// Loads the performance of each strategy in the Strategies container to the subPerformances table
        /// </summary>
        public void Load_Performances()
        {
            Load_Performances(false);
        }

        /// <summary>
        /// Loads the performance of each strategy in the Strategies container to the subPerformances table
        /// </summary>
        /// <param name="RollPerformance">False to load the performance. True to load the rolling performance.</param>
        public void Load_Performances(bool RollPerformance)
        {
            stPerformances = new Performances_Table("subPerformances", Strategies[0], 0);
                      
            foreach (Strategy curStrategy in Strategies.Values)
            {
                if (stPerformances.GetValueColumnIndex(curStrategy.ID) == -1)
                {
                    int stratIndex = stPerformances.AddValueColumn(curStrategy.ID);
                    int perfIndex = -1;

                    if (!RollPerformance)
                    {
                        perfIndex = curStrategy.stPerfSummary.GetCustomColumnIndex("PERF");
                    }
                    else
                    {
                        perfIndex = curStrategy.stPerfSummary.GetCustomColumnIndex("ROLLPERF");
                    }
                    for (int i = 0; i < stPerformances.DateRowCount; i++)
                    {
                        stPerformances.SetValue(i, stratIndex, curStrategy.stPerfSummary.GetCustomValue(i, perfIndex));
                    }
                }
                else
                {
                    throw new Exception("There are multiple strategies with the same ID (" + curStrategy.ID.ToString() + ") in Strategies container");
                }
            }            
        }        

        /// <summary>
        /// Fills the subPositions table in a equal weighted scheme based on the subPerformances table
        /// </summary>
        public void Fill_Weights_EW()
        {
            stPositions = new PercPositions_Table("subPositions", Strategies[0], 0);
            
            Weighing = Utils.WeighingSchemes.EqualWeighed;

            double tempValue = (1F / stPerformances.ValueColumnCount);

            foreach (Strategy curStrategy in Strategies.Values)
            {
                if (stPositions.GetValueColumnIndex(curStrategy.ID) == -1)
                {
                    int stratIndex = stPositions.AddValueColumn(curStrategy.ID);

                    for (int i = 0; i < curStrategy.stPerfSummary.DateRowCount; i++)
                    {
                        stPositions.SetValue(i, stratIndex, tempValue);
                    }
                }
                else
                {
                    throw new Exception("There are multiple strategies with the same ID (" + curStrategy.ID.ToString() + ") in Strategies container");
                }
            }
        }

        //TODO Ajustar para considerar o ID das estratégias
        public void Fill_Contributions()
        {
            stContributions = new Contributions_Table("subContributions", Strategies[0], Strategies.Count);

            int i=0;

            foreach (Strategy curStrategy in Strategies.Values)
            {
                for (int j = 0; j < curStrategy.stPerfSummary.DateRowCount; j++)
                {
                    double tempValue = stPerformances.GetValue(j, i) * stPositions.GetValue(j, i);
                    stContributions.SetValue(j, i, tempValue);
                }
                i++;
            }
            stContributions.AddRowSum(false);
        }

        /// <summary>
        /// Consolidates position of all strategies inside the Strategies container.
        /// </summary>
        public void Consolidate_Position()
        {
            tkPositions = new PercPositions_Table("stratPositions", Strategies[0], 0);
            tkPerformances = new Performances_Table("stratPerformance", Strategies[0], 0);
            
            foreach (Strategy curStrategy in Strategies.Values)
            {
                int Idx_Strategy = stPositions.GetValueColumnIndex(curStrategy.ID);
                if (Idx_Strategy != -1)
                {
                    for (int j = 0; j < curStrategy.tkPositions.ValueColumnCount; j++)
                    {
                        int idValue = curStrategy.tkPositions.GetValueColumnID(j);
                        if (idValue != -1)
                        {
                            int idxValue = tkPositions.GetValueColumnIndex(idValue);

                            if (idxValue == -1)
                            {
                                idxValue = tkPositions.AddValueColumn(idValue);
                            }

                            for (int i = 0; i < tkPositions.DateRowCount; i++)
                            {
                                double initialPosition = tkPositions.GetValue(i, idxValue);
                                double curPosition = curStrategy.tkPositions.GetValue(i, j);
                                double newPosition = initialPosition + curPosition * stPositions.GetValue(i, Idx_Strategy);

                                tkPositions.SetValue(i, idxValue, newPosition);
                            }
                        }
                        else
                        {
                            throw new Exception("Unable to find Id_Value for index " + j + " in stratPositions table");
                        }
                    }
                }
                else
                {
                    throw new Exception("Unable to find index for Stragety ID " + curStrategy.ID +" in subPositions table");
                }

                tkPerformances.Merge(curStrategy.tkPerformances);

            }            

            tkPositions.AddRowGross();
            tkPositions.AddRowNet();

            tkPositions.ValueColumnType = Utils.TableTypes.Id_Ticker;
            tkPerformances.ValueColumnType = Utils.TableTypes.Id_Ticker;
        }



        /// <summary>
        /// Fills the strategy performance summary
        /// </summary>
        public void Fill_Strategy_Performance()
        {
            Fill_Strategy_Performance(-1, false);
        }


        public void Fill_Strategy_Performance(int rollWindow, bool annual)
        {
            Fill_Benchmark();       
            if (rollWindow != -1)
            {
                stPerfSummary = new perfSummary_Table("stratPerfSummary", stContributions, BmContributions, rollWindow, annual);
            }
            else
            {
                stPerfSummary = new perfSummary_Table("stratPerfSummary", stContributions, BmContributions);
            }
        }

        public void Fill_Benchmark()
        {
            switch (Benchmark)
            {
                case Utils.BenchmarkTypes.FromPosition:
                    if (tkPositions != null)
                    {
                        BmPositions = new PercPositions_Table("BmPositions", this, 0);
                        for (int j = 0; j < tkPositions.ValueColumnCount; j++)
                        {
                            int Id_Value = tkPositions.GetValueColumnID(j);
                            if (Id_Value != -1)
                            {
                                int Idx_Value = BmPositions.AddValueColumn(Id_Value);
                                for (int i = 0; i < tkPositions.DateRowCount; i++)
                                {
                                    BmPositions.SetValue(i, Idx_Value, Math.Abs(tkPositions.GetValue(i, j)));
                                }
                            }
                            else
                            {
                                throw new Exception("Unable to find index " + j + " in stratPositions table");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("stratPositions table not initialized. Unable to create BmPositions.");
                    }
                    BmContributions = new Contributions_Table("BmContributions", BmPositions, tkPerformances,true);
                    break;
                case Utils.BenchmarkTypes.Custom:
                    Custom_Benchmark();
                    break;
                case Utils.BenchmarkTypes.StrategyEqualWeighed:
                    Fill_Benchmark_Performances();
                    Fill_Bm_Positions_EW();
                    BmContributions = new Contributions_Table("BmContributions", BmPositions, BmPerformances);
                    break;
                default:
                    throw new System.NotImplementedException();
                    break;
            }
            
        }

        public void Fill_Benchmark_Performances()
        {
            BmPerformances = new Performances_Table("BmPerformances", this, 0);

            foreach (Strategy curStrategy in Strategies.Values)
            {
                if (BmPerformances.GetValueColumnIndex(curStrategy.ID) == -1)
                {
                    int Idx_Strategy = BmPerformances.AddValueColumn(curStrategy.ID);
                    int Idx_BM = curStrategy.stPerfSummary.GetCustomColumnIndex("BENCHMARK");

                    for (int i = 0; i < BmPerformances.DateRowCount; i++)
                    {
                        BmPerformances.SetValue(i, Idx_Strategy, curStrategy.stPerfSummary.GetCustomValue(i, Idx_BM));
                    }
                }
                else
                {
                    //TODO ajustar exceção
                    throw new System.NotImplementedException();
                }
            }            
        }

        public void Fill_Bm_Positions_EW()
        {
            BmPositions = new PercPositions_Table("BmPositions", this, 0);

            for (int j = 0; j < BmPerformances.ValueColumnCount; j++)
            {
                int Id_Value = BmPerformances.GetValueColumnID(j);
                if (Id_Value != -1)
                {
                    if (BmPositions.GetValueColumnIndex(Id_Value) == -1)
                    {
                        int Idx_Value = BmPositions.AddValueColumn(Id_Value);
                        double pos = 1F / BmPerformances.ValueColumnCount;

                        for (int i = 0; i < BmPerformances.DateRowCount; i++)
                        {
                            BmPositions.SetValue(i, Idx_Value, pos);
                        }
                    }
                    else
                    {
                        throw new System.NotImplementedException();
                    }
                }
                else
                {
                    throw new System.NotImplementedException();

                }
            }
        }

        public virtual void Custom_Benchmark()
        {
            throw new System.NotImplementedException();
        }

        public void SetPriceFeeder(RTPrice priceFeeder)
        {
            _PriceFeeder = priceFeeder;
        }

        
        /// <summary>
        /// Add a strategy to the Strategies container.
        /// If the strategy´s ID hasn´t been set, set it to its index in the container.
        /// </summary>
        /// <param name="StrategyToAdd">Strategy to be added</param>
        public void AddStrategy(Strategy StrategyToAdd)
        {
            int index = Strategies.Count;

            if (StrategyToAdd.ID == int.MinValue)
            {
                StrategyToAdd.ID = index;
            }
            
            Strategies.Add(index, StrategyToAdd);
        }

        #region RealTime Methods

        public void Refresh(object source, EventArgs e)
        {
            if (IsRealTime)
            {
                StrategyMutex.WaitOne();

                UpdateStrategy(source, e);

                StrategyMutex.ReleaseMutex();

                Changed();
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }       
        
        protected virtual void UpdateStrategy(object source, EventArgs e)
        {
        }       

        private void Changed()
        {
            if (StrategyChanged != null)
            {
                StrategyChanged(this, EventArgs.Empty);
            }
        }

        public void Refresh_Performances()
        {
            int i = 0;

            int curDate = stPerformances.DateRowCount - 1;

            foreach (Strategy curStrategy in Strategies.Values)
            {
                stPerformances.SetValue(curDate, i, curStrategy.stPerfSummary.GetCustomValue(curDate, 0));
                i++;
            }
            
        }

        public void Refresh_Contributions()
        {
            int i = 0;

            int curDate = stPerformances.DateRowCount - 1;

            foreach (Strategy curStrategy in Strategies.Values)
            {
                double tempValue = stPerformances.GetValue(curDate, i) * stPositions.GetValue(curDate, i);
                stContributions.SetValue(curDate, i, tempValue);
                i++;
            }

        }

        #endregion

        public abstract void RunStrategy();
             
        #endregion

    }
}
