using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace LBRTCalc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                bool NotRunning = true;
                using (Mutex mutex = new Mutex(true, "LBRTCalc", out NotRunning))
                {
                    if (NotRunning)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        frmRealTime Form = new frmRealTime();
                        Application.DoEvents();
                        Application.Run(Form);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
