using System;
using System.Threading;
using System.Windows.Forms;

namespace LiveBook
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

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    GlobalVars.Instance.appStarting = true;

                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    LB_Utils curUtils = new LB_Utils();

                    frmLogin curLogin = new frmLogin();
                    frmMenu curApplication = new frmMenu();

                    curLogin.ShowDialog();
                    int User_Id = curLogin.Id_User;
                    //int User_Id = 2;
                    curApplication.LBVersion = curLogin.LBVersion;

                    if (User_Id > 0)
                    {
                        LiveDLL.NUserControl.Instance.User_Id = User_Id;

                        curApplication.Show();
                        Application.DoEvents();
                        GlobalVars.Instance.appStarting = false;
                        curApplication.Carrega_Form();

                        Application.Run(curApplication);
                    }
                    Application.ExitThread();
                }
                else
                {
                    using (Mutex mutex = new Mutex(true, "LiveBook", out NotRunning))
                    {
                        if (NotRunning)
                        {
                            GlobalVars.Instance.appStarting = true;

                            Application.EnableVisualStyles();
                            Application.SetCompatibleTextRenderingDefault(false);
                            LB_Utils curUtils = new LB_Utils();

                            frmLogin curLogin = new frmLogin();
                            frmMenu curApplication = new frmMenu();

                            curLogin.ShowDialog();
                            int User_Id = curLogin.Id_User;
                            //int User_Id = 2;
                            curApplication.LBVersion = curLogin.LBVersion;

                            if (User_Id > 0)
                            {
                                LiveDLL.NUserControl.Instance.User_Id = User_Id;

                                curApplication.Show();
                                Application.DoEvents();
                                GlobalVars.Instance.appStarting = false;
                                curApplication.Carrega_Form();

                                Application.Run(curApplication);
                            }
                            Application.ExitThread();
                        }
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