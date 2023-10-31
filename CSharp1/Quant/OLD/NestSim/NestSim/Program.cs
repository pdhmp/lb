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
            DateTime endDate = new DateTime(2008, 12, 31);

            Sector_Value_BZ_Container ValueBZ_Container = new Sector_Value_BZ_Container();
            ValueBZ_Container.iniDate = iniDate;
            ValueBZ_Container.endDate = endDate;
            ValueBZ_Container.Name = "ValueBZ";

            List<Strategy_Container> Strategies = new List<Strategy_Container>();

            Strategies.Add(ValueBZ_Container);
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ViewerMain(Strategies));
        }
    }
}