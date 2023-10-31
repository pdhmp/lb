using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using NestQuant.Common;

namespace NewMomentumUS
{
    public class StrategyInfo 
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

        private SortedDictionary<int,SectorInfo> _Sectors;
        public SortedDictionary<int,SectorInfo> Sectors
        {
            get { return _Sectors; }
        }

        private Weight_Table _StratWeights;
        public Weight_Table StratWeights
        {
            get { return _StratWeights; }
        }

        private perfSummary_Table _BenchmarkPerformance;
        public perfSummary_Table BenchmarkPerformance
        {
            get { return _BenchmarkPerformance; }
        }

        private SortedDictionary<int,int> _TickersBySector;
        public SortedDictionary<int,int> TickersBySector
        {
            get { return _TickersBySector; }
        }

        public StrategyInfo()
        {
            _Sectors = new SortedDictionary<int, SectorInfo>();
            _TickersBySector = new SortedDictionary<int, int>();
        }

        public void FillStrategyInfo()
        {
            foreach (int sectorID in GetComposition(ID_Ticker_Composite))
            {
                SectorInfo sectorInfo = new SectorInfo();
                sectorInfo.ID_Ticker_Template = ID_Ticker_Template;
                sectorInfo.ID_Ticker_Composite = sectorID;
                sectorInfo.IniDate = IniDate;
                sectorInfo.EndDate = EndDate;
                sectorInfo.Window = Window;

                sectorInfo.FillSectorInfo();

                _Sectors.Add(sectorID, sectorInfo);

                foreach (int tickerID in GetComposition(sectorID))
                {
                    _TickersBySector.Add(tickerID, sectorID);
                }
            }

            _StratWeights = new Weight_Table("StratWeights", ID_Ticker_Template,IniDate,EndDate);
            _StratWeights.FillFromComposite(ID_Ticker_Composite);

            FillBenchmarkPerformance();


        }

        private int[] GetComposition(int Composite)
        {
            DataTable dt;
            using (NestConn conn = new NestConn())
            {
                string SQLString = "SELECT ID_TICKER_COMPONENT FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION" +
                                   "\r\nWHERE ID_TICKER_COMPOSITE = " + Composite +
                                   "\r\nGROUP BY ID_TICKER_COMPONENT";
                dt = conn.ExecuteDataTable(SQLString);
            }

            int[] composition = new int[dt.Rows.Count];
            int i = 0;

            foreach (DataRow curRow in dt.Rows)
            {
                composition[i] = Convert.ToInt16(curRow[0].ToString());
                i++;
            }

            return composition;
        }

        private void FillBenchmarkPerformance()
        {
            Signal_Table allLong = new Signal_Table("allLong", ID_Ticker_Template, IniDate,EndDate );
            allLong.FillPositionFromComposite(ID_Ticker_Composite, Utils.PositionSchemes.Long);

            Sector_Performance strat = new Sector_Performance(false, null);
            strat.Id_Ticker_Template = ID_Ticker_Template;
            strat.iniDate = IniDate;
            strat.endDate = EndDate;

            Performances_Table sectorPerformances = new Performances_Table("sectorPerformances", strat, 0);
            sectorPerformances.ZeroFillFromComposite(ID_Ticker_Composite);

            for (int j = 0; j < sectorPerformances.ValueColumnCount; j++)
            {
                int sectorID = sectorPerformances.GetValueColumnID(j);

                SectorInfo sectorInfo;

                if (_Sectors.TryGetValue(sectorID, out sectorInfo))
                {                    
                    int perfColID = sectorInfo.PerfSummary.GetCustomColumnIndex("PERF");

                    for (int i = 0; i < sectorPerformances.DateRowCount; i++)
                    {
                        sectorPerformances.SetValue(i, j, sectorInfo.PerfSummary.GetCustomValue(i, perfColID));
                    }                    
                }
                else
                {
                    throw new System.NotImplementedException();
                }
            }

            PercPositions_Table Positions = new PercPositions_Table(allLong, _StratWeights);
            Contributions_Table Contribution = new Contributions_Table("Contributions", Positions, sectorPerformances);
            Contributions_Table BMContribution = Contribution;

            _BenchmarkPerformance = new perfSummary_Table("BenchmarkPerformances", Contribution, BMContribution,Window,false);

        }
    }
}
