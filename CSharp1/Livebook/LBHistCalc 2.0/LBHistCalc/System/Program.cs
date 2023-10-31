using System;
using System.Windows.Forms;
using System.Threading;

namespace LBHistCalc
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
                using (Mutex mutex = new Mutex(true, "LBHistCalc", out NotRunning))
                {
                    if (NotRunning)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        frmHistCalc Form = new frmHistCalc();
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
