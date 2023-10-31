using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SGN
{
    public class Program1
    {
        /// <summary>
        /// Application Entry Point
        /// </summary>
        [STAThread]
        public void Startar(string User_Id)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            frmMenu frmMenu = new frmMenu();
            Application.Run(frmMenu);
        }
    }
}