using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using NestQuant.Common;

namespace NestQuant.Strategies
{
    static class Program
    {
        static void Main()
        {
            DateTime iniDate = new DateTime(2002, 01, 18);
            DateTime endDate = new DateTime(2008, 12, 31);

            Rates_BZ NewStrat = new Rates_BZ();
            NewStrat.iniDate = iniDate;
            NewStrat.endDate = endDate;
            NewStrat.DateRowInterval = Utils.TimeIntervals.IntervalDay;
            NewStrat.StrategyType = Utils.StrategyTypes.Undefined;
            NewStrat.Name = "RatesBZ";
            NewStrat.Id_Ticker_Composite = 27374;
            NewStrat.Id_Ticker_Template = 27374;

            List<Strategy> Strategies = new List<Strategy>();

            Strategies.Add(NewStrat);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ViewerMain(Strategies));
        }
    }
}

