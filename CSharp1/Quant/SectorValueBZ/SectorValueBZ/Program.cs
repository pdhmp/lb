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
            DateTime endDate = DateTime.Now.Date;

            Sector_Value_BZ_Container ValueBZ_Container = new Sector_Value_BZ_Container();
            ValueBZ_Container.iniDate = iniDate;
            ValueBZ_Container.endDate = endDate;
            ValueBZ_Container.Name = "ValueBZ";
            ValueBZ_Container.Id_Ticker_Template = 1073;

            List<Strategy> Strategies = new List<Strategy>();

            Strategies.Add(ValueBZ_Container);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ViewerMain(Strategies));
        }
    }
}