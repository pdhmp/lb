using System;
using System.Windows.Forms;
using System.Threading;

namespace FIX_BMF
{
	class Executor
	{
		static void Main( string[] args )
		{
            //TestQuotes.ExecTestQuotes();

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new frmControl());

		}

        private void OpenMainForm()
        {
            
        }

	}
}