using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SGN.Validacao;

namespace SGN
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            GlobalVars.Instance.appStarting = true;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Valida Valida = new Valida();

            frmLogin curLogin = new frmLogin();
            frmMenu curApplication = new frmMenu();

            curLogin.ShowDialog();
            int User_Id = curLogin.Id_User;
            curApplication.LBVersion = curLogin.LBVersion;

            if (User_Id > 0)
            {
                NestDLL.NUserControl.Instance.User_Id = User_Id;
                Valida.Load_Properties_Form(curApplication);

                curApplication.Show();
                Application.DoEvents();
                GlobalVars.Instance.appStarting = false;
                curApplication.Carrega_Form();

                Application.Run(curApplication);
            }
        }
    }
}