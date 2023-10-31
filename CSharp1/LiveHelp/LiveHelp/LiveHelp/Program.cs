using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace NestDesk
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool instantiated = true;

            using (Mutex mutex = new Mutex(true,"LiveHelp", out instantiated))
            {
                if (instantiated)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new frmHelpDesk());
                }
            }
        }
    }
}