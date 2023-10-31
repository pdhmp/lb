using System;
using System.Data;
using System.Threading;
using LiveDLL;

namespace LiveBook
{
    public partial class frmstatus : LBForm
    {
        newNestConn curConn = new newNestConn();

        string SQLString;
        DataTable tempTable = new DataTable();
        bool ThreadRunning;

        public frmstatus()
        {
            InitializeComponent();
        }

        private void frmStatus_Load(object sender, EventArgs e)
        {
            // TS - 2013-08-26
            //SQLString = "SELECT Id_Program, Login + ' - ' + Err_Message AS Err_Message FROM NESTLOG.dbo.Tb902_Error_Report A LEFT JOIN NESTDB.dbo.Tb014_Pessoas B ON A.Responsability=B.Id_Pessoa Order by Severity";
            //lstStatus.DisplayMember = "Err_Message";
            //lstStatus.ValueMember = "Id_Program";
            //timer1.Start();
            //lstStatus.Width = this.Width - 10;
            //lstStatus.Height = this.Height - 30;
        }

        private void frmStatus_Resize(object sender, EventArgs e)
        {
            lstStatus.Top = 0;
            lstStatus.Left = 0;
            lstStatus.Width = this.Width-10;
            lstStatus.Height = this.Height-30;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!ThreadRunning)
            {
                System.Threading.Thread t1;
                t1 = new System.Threading.Thread(new ThreadStart(ExecSQL));
                t1.Start();
            }

            lstStatus.DataSource = tempTable;
            lstStatus.DisplayMember = "Err_Message";
            lstStatus.ValueMember = "Id_Program";

        }

        private void ExecSQL()
        {
            DataTable ThreadTable = new DataTable();
            

            ThreadRunning = true;
            
            try
            {
                ThreadTable = curConn.Return_DataTable(SQLString);
                tempTable = ThreadTable;
                lstStatus.Refresh() ;

                

                ThreadRunning = false;
            }
            catch(Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(),this.Name.ToString());
                ThreadRunning = false;
            }
        }
    }
}