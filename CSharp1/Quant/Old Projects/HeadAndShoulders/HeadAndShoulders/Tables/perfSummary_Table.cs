using System;
using System.Collections.Generic;
using System.Text;

namespace NestSim
{
    public class perfSummary_Table : Base_Table
    {
        private float _AnnualizedPerformance = 0;
        public float AnnualizedPerformance
        {
            get
            {
                return _AnnualizedPerformance;
            }
        }

        public perfSummary_Table(string _Name, int _Id_Ticker_Template, DateTime _iniDate, DateTime _endDate)
            : base(_Name, _Id_Ticker_Template, _iniDate, _endDate)
        {
            AddCustomColumn("PERF");
            AddCustomColumn("GR100");
            AddCustomColumn("DRAWDOWN");
        }

        public perfSummary_Table(string _Name, Contributions_Table contribTable)
            : base(_Name, contribTable.Id_Ticker_Template, contribTable.iniDate, contribTable.endDate)
        {
            AddCustomColumn("PERF");
            AddCustomColumn("GR100");
            AddCustomColumn("DRAWDOWN");
            Fill(contribTable);
        }


        public void Fill(Contributions_Table contribTable)
        {
            float curPerf = 0;
            float cumAmount = 100;
            float cumDDNAmount = 1;
            int curPerfColumn = contribTable.GetCustomColumnIndex("SUM");

            if (curPerfColumn != -1)
            {
                for (int i = 0; i < contribTable.DateRowCount; i++)
                {
                    curPerf = contribTable.GetCustomValue(i, 0);
                    cumAmount = cumAmount * (1 + curPerf);
                    cumDDNAmount = cumDDNAmount * (1 + curPerf);
                    if (cumDDNAmount > 1) { cumDDNAmount = 1; };
                    SetCustomValue(i, 0, curPerf);
                    SetCustomValue(i, 1, cumAmount);
                    SetCustomValue(i, 2, cumDDNAmount - 1);
                }

                //_AnnualizedPerformance = (cumAmount / 100) ^ (252 / curPerf_Table.DateRowCount);
                
            }
        }
    }
}
