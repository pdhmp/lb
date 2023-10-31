using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NCommonTypes;

namespace ValueBZ_V2
{
    public class ValueSim
    {
        private DateTime _IniDate;
        public DateTime IniDate
        {
            get { return _IniDate; }
            set { _IniDate = value; }
        }

        public SortedDictionary<DateTime, ValueStrategy> HistCalcs = new SortedDictionary<DateTime, ValueStrategy>();
        public SortedDictionary<DateTime, StatItem> SimData = new SortedDictionary<DateTime, StatItem>();

        public ValueSim(DateTime __IniDate)
        {
            _IniDate = __IniDate;

            DateTime curDate = ValueData.Instance.PrevDate(DateTime.Today);
            //curDate = new DateTime(2011, 04, 01);

            while (IniDate <= curDate)
            {
                ValueStrategy curStrategy = new ValueStrategy(curDate);
                curStrategy.StratRecalc();

                HistCalcs.Add(curDate, curStrategy);

                StatItem curStatItem = CalcStats(curStrategy, curDate);                
                SimData.Add(curDate, curStatItem);

                curDate = ValueData.Instance.PrevDate(curDate);
            }
        }

        private StatItem CalcStats(ValueStrategy curStrategy, DateTime refDate)
        {
            StatItem curStatItem = new StatItem();
            curStatItem.RefDate = refDate;

            foreach (ValueModel curModel in curStrategy.SectorCalcs.Values)
            {
                foreach (newTickerPE curTickerPE in curModel.PositionPEs.Values)
                {
                    curStatItem.StratPositions.Add(curTickerPE);
                }
            }

            curStatItem.CalcStats();

            return curStatItem;
        }    
    }

    public class StatItem
    {
        public DateTime RefDate = new DateTime(1900, 01, 01);

        public double Performance = 0;

        public double Long = 0;
        public double Short = 0;
        public double Gross = 0;
        public double Net = 0;

        public List<newTickerPE> StratPositions = new List<newTickerPE>();

        public void CalcStats()
        {
            foreach (newTickerPE curValueItem in StratPositions)
            {
                double curValue = curValueItem.CloseWeight * curValueItem.closeSignal;
                double dayTR = curValueItem.DayTR;

                if (curValueItem.Ticker == "MNDL4")
                {
                    int a = 0;
                }

                if (double.IsNaN(curValue))
                {
                    int a = 0;
                }

                if (curValue == 0)
                {
                    dayTR = 0;
                }
                else
                {
                    if ((double.IsInfinity(dayTR) || double.IsNaN(dayTR)))
                    {
                        string logmsg = curValueItem.Ticker + "\t" + curValueItem.IdTicker + "\t" + RefDate.ToString("dd/MM/yyyy");
                        ValueData.Instance.PrintLog(logmsg);
                    }
                }

                if (curValue > 0) Long = Long + curValue;
                if (curValue < 0) Short = Short + curValue;
                Net = Net + curValue;
                Gross = Gross + Math.Abs(curValue);
                Performance = Performance + curValue * dayTR;
                curValueItem.StratContrib = curValue * dayTR;
            }
        }
    }
}
