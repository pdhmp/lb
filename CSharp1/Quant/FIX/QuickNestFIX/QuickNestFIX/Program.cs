using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace QuickNestFIX
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //string SystemUser = System.Environment.UserName;
            string SystemUser = "NestQuant";
            string DBUser;

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                DBUser = curConn.Execute_Query_String("SELECT Id_Pessoa FROM NESTDB.dbo.Tb014_Pessoas WHERE login='" + SystemUser + "'");
                NestDLL.NUserControl.Instance.User_Id = int.Parse(DBUser);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain());
        }
    }
}