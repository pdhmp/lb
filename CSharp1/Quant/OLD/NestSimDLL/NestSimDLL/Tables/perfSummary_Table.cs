using System;
using System.Collections.Generic;
using System.Text;

namespace NestQuant.Common
{
    public class perfSummary_Table : Base_Table
    {
        private double _AnnualizedPerformance = 0;
        public double AnnualizedPerformance
        {
            get
            {
                return _AnnualizedPerformance;
            }
        }
        private double _AnnualizedDev = 0;
        public double AnnualizedDev
        {
            get
            {
                return _AnnualizedDev;
            }
        }
        private double _AnnualizedSharpe = 0;
        public double AnnualizedSharpe
        {
            get
            {
                return _AnnualizedSharpe;
            }
        }

        private double _BMAnnualizedPerformance = 0;
        public double BMAnnualizedPerformance
        {
            get
            {
                return _BMAnnualizedPerformance;
            }
        }
        private double _BMAnnualizedDev = 0;
        public double BMAnnualizedDev
        {
            get
            {
                return _BMAnnualizedDev;
            }
        }
        private double _BMAnnualizedSharpe = 0;
        public double BMAnnualizedSharpe
        {
            get
            {
                return _BMAnnualizedSharpe;
            }
        }

        private Contributions_Table _ContribTable;
        private Contributions_Table _bmContribution;
        
        private int _RollWindow = 120;
        public int RollWindow
        {
            get
            {
                return _RollWindow;
            }
            set
            {
                _RollWindow = value;
            }
        }

        public perfSummary_Table(string _Name, int _Id_Ticker_Template, DateTime _iniDate, DateTime _endDate)
            : this(_Name, _Id_Ticker_Template, _iniDate, _endDate,false)
        {
            
        }

        public perfSummary_Table(string _Name, int _Id_Ticker_Template, DateTime _iniDate, DateTime _endDate, bool isRealTime)
            : base(_Name, _Id_Ticker_Template, _iniDate, _endDate,isRealTime)
        {
            AddCustomColumn("PERF");            
            AddCustomColumn("GR100");
            AddCustomColumn("DRAWDOWN");
            AddCustomColumn("BENCHMARK");
            AddCustomColumn("BMGR100");
        }

        public perfSummary_Table(string _Name, Contributions_Table contribTable, Contributions_Table bmContribution)
            : base(_Name, contribTable.Id_Ticker_Template, contribTable.iniDate, contribTable.endDate, contribTable.IsRealTime)
        {            
            Fill(contribTable, bmContribution, true);
        }

        public perfSummary_Table(string _Name, Contributions_Table contribTable, Contributions_Table bmContribution, int rollWindow, bool Annual)
            : base(_Name, contribTable.Id_Ticker_Template, contribTable.iniDate, contribTable.endDate, contribTable.IsRealTime)
        {
            RollWindow = rollWindow;
            Fill(contribTable, bmContribution, Annual);
        }



        public void Fill(Contributions_Table contribTable, Contributions_Table bmContribution)
        {
            Fill(contribTable, bmContribution, true);
        }

        public void Fill(Contributions_Table contribTable, Contributions_Table bmContribution, bool Annual)
        {
            _ContribTable = contribTable;
            _bmContribution = bmContribution;
            
            double curPerf = 0;
            double cumAmount = 100;
            double cumDDNAmount = 1;
            
            int curPerfColumn = contribTable.GetCustomColumnIndex("SUM");


            int PERF_IDX = AddCustomColumn("PERF");
            int GR100_IDX = AddCustomColumn("GR100");
            int DRAWDOWN_IDX = AddCustomColumn("DRAWDOWN");
            
            if (curPerfColumn != -1)
            {
                for (int i = 0; i < contribTable.DateRowCount; i++)
                {
                    curPerf = contribTable.GetCustomValue(i, curPerfColumn);
                    cumAmount = cumAmount * (1 + curPerf);
                    cumDDNAmount = cumDDNAmount * (1 + curPerf);
                    if (cumDDNAmount > 1) { cumDDNAmount = 1; };
                    SetCustomValue(i, PERF_IDX, curPerf);
                    SetCustomValue(i, GR100_IDX, cumAmount);
                    SetCustomValue(i, DRAWDOWN_IDX, cumDDNAmount - 1);                  
                }
                int ROLLPERF = AddRollingPerf("ROLLPERF", 1, PERF_IDX, RollWindow, Annual);
                int ROLLDEV = AddRollingDev("ROLLDEV", 1, PERF_IDX, RollWindow, Annual);
                AddRollingSharpe("ROLLSHARPE", ROLLPERF, ROLLDEV, RollWindow);

                _AnnualizedPerformance = Math.Pow((cumAmount / 100.00), (252F / DateRowCount)) - 1;
                double[] perfValues = CustomValues[PERF_IDX];
                _AnnualizedDev = Utils.calcStdev(ref perfValues) * Math.Sqrt(252);
                _AnnualizedSharpe = _AnnualizedPerformance / _AnnualizedDev;

                double bmCurPerf = 0;
                double bmCumAmount = 100;
                double bmCumDDNAmount = 1;

                int BENCHMARK_IDX = AddCustomColumn("BENCHMARK");
                int BMGR100_IDX = AddCustomColumn("BMGR100");
                int BMDRAWDOWM_IDX = AddCustomColumn("BMDRAWDOWN");

                int bmCurPerfColumn = contribTable.GetCustomColumnIndex("SUM");

                for (int i = 0; i < bmContribution.DateRowCount; i++)
                {
                    bmCurPerf = bmContribution.GetCustomValue(i, bmCurPerfColumn);
                    bmCumAmount = bmCumAmount * (1 + bmCurPerf);
                    bmCumDDNAmount = bmCumDDNAmount * (1 + bmCurPerf);
                    if (bmCumDDNAmount > 1) { bmCumDDNAmount = 1; };
                    SetCustomValue(i, BENCHMARK_IDX, bmCurPerf);
                    SetCustomValue(i, BMGR100_IDX, bmCumAmount);
                    SetCustomValue(i, BMDRAWDOWM_IDX, bmCumDDNAmount - 1);
                }

                int BMROLLPERF = AddRollingPerf("BMROLLPERF", 1, BENCHMARK_IDX, RollWindow, Annual);
                int BMROLLDEV = AddRollingDev("BMROLLDEV", 1, BENCHMARK_IDX, RollWindow, Annual);
                AddRollingSharpe("BMROLLSHARPE", BMROLLPERF, BMROLLDEV, RollWindow);

                _BMAnnualizedPerformance = Math.Pow((bmCumAmount / 100.00), (252F / DateRowCount)) - 1;
                double[] bmPerfValues = CustomValues[BENCHMARK_IDX];
                _BMAnnualizedDev = Utils.calcStdev(ref bmPerfValues) * Math.Sqrt(252);
                _BMAnnualizedSharpe = _BMAnnualizedPerformance / _BMAnnualizedDev;
                
            }
        }

        protected override void UpdateTable()
        {
            int curPerf = GetCustomColumnIndex("PERF");
            int curGR100 = GetCustomColumnIndex("GR100");
            int curDrawDown = GetCustomColumnIndex("DRAWDOWN");
            int curPerfColumn = _ContribTable.GetCustomColumnIndex("SUM");
            int curDate = DateRowCount - 1;

            double perf = _ContribTable.GetCustomValue(curDate, curPerfColumn);
            double cumAmount = GetCustomValue(curDate - 1, curGR100) * (1 + perf);
            double cumDDNAmount = (1 + GetCustomValue(curDate - 1, curDrawDown)) * (1 + perf);
            if (cumDDNAmount > 1) { cumDDNAmount = 1; };
            SetCustomValue(curDate, curPerf, perf);
            SetCustomValue(curDate, curGR100, cumAmount);
            SetCustomValue(curDate, curDrawDown, cumDDNAmount - 1);

            _AnnualizedPerformance = Math.Pow((cumAmount / 100.00), (252F / DateRowCount)) - 1;
            double[] perfValues = CustomValues[curPerfColumn];
            _AnnualizedDev = Utils.calcStdev(ref perfValues) * Math.Sqrt(252);
            _AnnualizedSharpe = _AnnualizedPerformance / _AnnualizedDev;
            
        }
    }
}
