using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace LB_Hist_Update
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
                using (Mutex mutex = new Mutex(true, "LB_Calc", out NotRunning))
                {
                    if (NotRunning)
                    {
                        GlobalVars.Instance.appStarting = true;

                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        frmUpdate Update = new frmUpdate();
                        Update.Show();
                        Application.DoEvents();
                        GlobalVars.Instance.appStarting = false;

                        Application.Run(Update);
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