using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmFundPerfSummary : LBForm
    {
        old_Conn curConn = new old_Conn();

        bool processing = false;
        bool ThreadRunning;
        string SQLString;
        DataTable tempTable = new DataTable();

        public frmFundPerfSummary()
        {
            InitializeComponent();
        }

        private void frmresume_Load(object sender, EventArgs e)
        {
                        dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            Carrega_Grid();
            timer1.Start();
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }

        void Carrega_Grid()
        {

            SQLString = "SELECT Port_Name, Today, MTD, HTD, YTD,[12M],[24M],[36M] FROM NESTRT.dbo.TB003_Port_Performance order by   type desc, Port_Name";

            curUtils.SetColumnStyle(dgFundPerfSummary,1);

            //dgFundPerfSummary.Columns["Portfolio"].Width = 200;

            dtg.DataSource = tempTable;

            if (dgFundPerfSummary.Columns.Count > 0)
            {
                dgFundPerfSummary.Columns["Today"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgFundPerfSummary.Columns["Today"].DisplayFormat.FormatString = "P2";

                dgFundPerfSummary.Columns["MTD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgFundPerfSummary.Columns["MTD"].DisplayFormat.FormatString = "P2";

                dgFundPerfSummary.Columns["HTD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgFundPerfSummary.Columns["HTD"].DisplayFormat.FormatString = "P2";

                dgFundPerfSummary.Columns["YTD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgFundPerfSummary.Columns["YTD"].DisplayFormat.FormatString = "P2";

                dgFundPerfSummary.Columns["12M"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgFundPerfSummary.Columns["12M"].DisplayFormat.FormatString = "P2";

                dgFundPerfSummary.Columns["24M"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgFundPerfSummary.Columns["24M"].DisplayFormat.FormatString = "P2";

                dgFundPerfSummary.Columns["36M"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgFundPerfSummary.Columns["36M"].DisplayFormat.FormatString = "P2";


            }
        }
        private void ExecSQL()
        {
            
            DataTable ThreadTable = new DataTable();

            ThreadRunning = true;

            try
            {
                ThreadTable = curConn.Return_DataTable(SQLString);
                tempTable = ThreadTable;
            }
            catch(Exception e) 
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());
            }
            

            ThreadRunning = false;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!ThreadRunning)
            {
                System.Threading.Thread t1;
                t1 = new System.Threading.Thread(new ThreadStart(ExecSQL));
                t1.Start();
            }
            if (!processing)
            {
                Carrega_Grid();
            };
        }


        private void dgFundPerfSummary_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgFundPerfSummary);
            timer1.Start();

        }

        private void dgFundPerfSummary_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgFundPerfSummary);
            timer1.Start();

        }

        private void dgFundPerfSummary_MouseUp(object sender, MouseEventArgs e)
        {
            processing = false;

        }

        private void dgFundPerfSummary_MouseDown(object sender, MouseEventArgs e)
        {
            processing = true;

        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgFundPerfSummary.SelectAll();
            dgFundPerfSummary.CopyToClipboard();
            //  MessageBox.Show("Copied!");

        }
    }
}