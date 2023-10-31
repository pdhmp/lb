using System;
using System.Collections.Generic;
using System.Text;
using NestQuant.Common;

namespace MomentumBZRunner
{
    public class SectorInfo 
    {
        private int _ID_Ticker_Template;
        public int ID_Ticker_Template
        {
            get { return _ID_Ticker_Template; }
            set { _ID_Ticker_Template = value; }
        }

        private int _ID_Ticker_Composite;
        public int ID_Ticker_Composite
        {
            get { return _ID_Ticker_Composite; }
            set { _ID_Ticker_Composite = value; }
        }

        private DateTime _IniDate;
        public DateTime IniDate
        {
            get { return _IniDate; }
            set { _IniDate = value; }
        }

        private DateTime _EndDate;
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        private int _Window;
        public int Window
        {
            get { return _Window; }
            set { _Window = value; }
        }
        
        private Price_Table _Price;
        public Price_Table Price
        {
            get { return _Price; }
        }

        private Weight_Table _Weight;
        public Weight_Table Weight
        {
            get { return _Weight; }
        }

        private perfSummary_Table _PerfSummary;
        public perfSummary_Table PerfSummary
        {
            get { return _PerfSummary; }
        }

        public SectorInfo()
        {
        }

        public void FillSectorInfo()
        {
            Sector_Performance sectorPerformance = new Sector_Performance(false,null);
            sectorPerformance.ID = ID_Ticker_Composite;
            sectorPerformance.Name = "Performance_" + ID_Ticker_Composite.ToString();
            sectorPerformance.Id_Ticker_Template = ID_Ticker_Template;
            sectorPerformance.Id_Ticker_Composite = ID_Ticker_Composite;
            sectorPerformance.Benchmark = Utils.BenchmarkTypes.FromPosition;
            sectorPerformance.DateRowInterval = Utils.TimeIntervals.IntervalDay;
            sectorPerformance.iniDate = IniDate;
            sectorPerformance.endDate = EndDate;
            sectorPerformance.Weighing = Utils.WeighingSchemes.FromComposite;
            sectorPerformance.StrategyType = Utils.StrategyTypes.Undefined;
            sectorPerformance.RollWindow = Window;

            sectorPerformance.RunStrategy();

            _Price = sectorPerformance.PriceTables[0];
            _Weight = sectorPerformance.WeightTables[0];
            _PerfSummary = sectorPerformance.stPerfSummary;            
        }
    }
}
