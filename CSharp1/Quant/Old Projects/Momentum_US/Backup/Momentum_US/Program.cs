using System;
using System.Collections.Generic;
using System.Windows.Forms;

using NestQuant.Common;
using NestQuant.Strategies;
 

namespace NestQuant.NestSim
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application. 
        /// </summary>
        [STAThread]
        static void Main()
        {
            DateTime iniDate = new DateTime(2000, 07, 01); 
            DateTime endDate = new DateTime(2010, 12, 31);

            RTPrice RTPriceFeeder = new RTPrice();

            Sector_Momentum_US_Container MomentumBZ_Container = new Sector_Momentum_US_Container(false);
            MomentumBZ_Container.iniDate = iniDate;
            MomentumBZ_Container.endDate = endDate;
            MomentumBZ_Container.Id_Ticker_Template = 5310;
            MomentumBZ_Container.Name = "Momentum_US";
            MomentumBZ_Container.SetPriceFeeder(RTPriceFeeder);

            List<Strategy> Strategies = new List<Strategy>();

            Strategies.Add(MomentumBZ_Container);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ViewerMain(Strategies, false));
        }
    }
}