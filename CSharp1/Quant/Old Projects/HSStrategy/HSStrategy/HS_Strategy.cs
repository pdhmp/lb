using System;
using System.Collections.Generic;
using System.Text;

using NestQuant.Common;

namespace NestQuant.Strategies
{
    class HS_Strategy : Strategy
    {
        #region Strategy Properties

        private double _HurdleRate = 0.03;
        private double _BandPercent = 0.20;
        private double _Horizontal = 1.5;
        private int _ExitBar = 1;
        private double _GainLimit = 0.02;
        private double _StopLimit = 0.01;
        private int _ExitStrategy = 1;
        private bool Intraday = false;
        private Utils.TimeIntervals _TimeIntervals = Utils.TimeIntervals.IntervalDay;
        private int _Id_Ticker_Composite = 21141;
        private int _SideFactor = 1;

        public int SideFactor
        {
            get { return _SideFactor; }
            set { _SideFactor = value; }
        }
	    public int Id_Ticker_Composite
        {
            get { return _Id_Ticker_Composite; }
            set { _Id_Ticker_Composite = value; }
        }
        public NestQuant.Common.Utils.TimeIntervals TimeIntervals
        {
            get { return _TimeIntervals; }
            set
            {
                switch (value)
                {
                    case Utils.TimeIntervals.Interval15mins:
                        _TimeIntervals = value;
                        Intraday = true;
                        break;
                    case Utils.TimeIntervals.IntervalDay:
                        _TimeIntervals = value;
                        Intraday = false;
                        break;
                    case Utils.TimeIntervals.IntervalMonth:
                    case Utils.TimeIntervals.IntervalQuarter:
                    case Utils.TimeIntervals.IntervalYear:
                    case Utils.TimeIntervals.Undefined:
                    default:
                        _TimeIntervals = Utils.TimeIntervals.IntervalDay;
                        Intraday = false;
                        break;
                }                
               
            }
        }
	    public int ExitStrategy
        {
            get { return _ExitStrategy; }
            set { _ExitStrategy = value; }
        }
	    public double StopLimit
        {
            get { return _StopLimit; }
            set { _StopLimit = value; }
        }
	    public double GainLimit
        {
            get { return _GainLimit; }
            set { _GainLimit = value; }
        }
	    public int ExitBar
        {
            get { return _ExitBar; }
            set { _ExitBar = value; }
        }
	    public double Horizontal
        {
            get { return _Horizontal; }
            set { _Horizontal = value; }
        }
	    public double BandPercent
        {
            get { return _BandPercent; }
            set { _BandPercent = value; }
        }
        public double HurdleRate
        {
            get { return _HurdleRate; }
            set { _HurdleRate = value; }
        }
        
        #endregion

        private HSContainer HS_Container;
        
        public override void RunStrategy()
        {
            HS_Container = new HSContainer(1, Intraday, Id_Ticker_Composite, HurdleRate, BandPercent, Horizontal, ExitBar, GainLimit, StopLimit, ExitStrategy, 
                                           SideFactor, iniDate, endDate);

            HS_Container.calculateHS();

            Price_Table HSSignal = new Price_Table("curSignals", Id_Ticker_Template, iniDate, endDate);
            HSSignal.Merge(HS_Container.SignalTables);
            SignalTables.Add(HSSignal);

            Price_Table HSPrices = new Price_Table("HSPrices", Id_Ticker_Template, iniDate, endDate);
            HSPrices.FillStyle = Utils.TableFillTypes.FillPrevious;
            HSPrices.FillFromComposite(Id_Ticker_Composite, 1, 1);
            PriceTables.Add(HSPrices);

            Weight_Table HSWeights = new Weight_Table("HSWeights", Id_Ticker_Template, iniDate, endDate);
            HSWeights.FillEWFromPrices(HSSignal);
            WeightTables.Add(HSWeights);

            subPerformances = new Performances_Table("subPerformances", HSPrices);

            subWeights = new PercPositions_Table(HSSignal, HSWeights);

            subContributions = new Contributions_Table("subContributions", subWeights, subPerformances);

            stratPerfSummary = new perfSummary_Table("stratPerfSummary", subContributions);


        }        
    }
}
