using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Feed_Bloomberg
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);            
            Log.AddLogEntry("Program.cs Ponto 1");
            Application.Run(new Form1());
        }
    }
}