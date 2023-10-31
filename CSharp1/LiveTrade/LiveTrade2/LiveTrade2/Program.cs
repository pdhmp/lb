using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace LiveTrade2
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


            Process[] tempProcess = Process.GetProcessesByName("LiveTrade2");

            foreach (Process curProcess in tempProcess)
            {
                if (curProcess.MainWindowTitle == "LiveTrade 2.0")
                {
                    MessageBox.Show("Previous instance still runnuning. Check Task manager for process called 'LiveTrade2.exe'", "ERROR: Already running", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            GlobalVars.Instance.LoadAllSymbolData();
            GlobalVars.Instance.LoadConnDefs();

            //Application.Run(new frmLevel2("VALE5"));
            //Application.Run(new frmIbovArb());

            frmLogin curLogin = new frmLogin();

            curLogin.ShowDialog();

            GlobalVars.Instance.ReadLimits();

            if (NestDLL.NUserControl.Instance.User_Id > 0)
            {
                
                //Application.Run(new frmPnL());

                GlobalVars.Instance.SetColorCode(1);
                if (NestDLL.NUserControl.Instance.User_Id == 17 || NestDLL.NUserControl.Instance.User_Id == 49) GlobalVars.Instance.SetColorCode(3);
                Application.DoEvents();
                Application.Run(new frmMain());
                
                //Application.Run(new frmLevel2(""));
            }

        }
    }
}
