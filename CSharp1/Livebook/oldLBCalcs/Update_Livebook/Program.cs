using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace Update_Livebook
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

            //Form1 tempForm;
            //tempForm = new Form1();
            //tempForm.Show();
            
            //CostClose tempForm2;
            //tempForm2 = new CostClose();
            //tempForm2.Show();

            Application.Run(new Form1());
            //Application.Run(new CostClose());
        }



    
    }
}