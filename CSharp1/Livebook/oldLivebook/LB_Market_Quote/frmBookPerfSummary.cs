
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestDLL;
using System.Data.SqlClient;

using System.Threading;

namespace LiveBook
{
    public partial class frmBookPerfSummary : LBForm
    {
        old_Conn curConn = new old_Conn();

        bool processing = false;
        bool ThreadRunning;
        string SQLString;
        DataTable tempTable = new DataTable();

        public frmBookPerfSummary()
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
            timer2.Start();
            timer2_Tick(this, null);
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }

        void Carrega_Grid()
        {
            if (cmbPortfolio.SelectedValue != null)
            {
                int Id_Portfolio = int.Parse(cmbPortfolio.SelectedValue.ToString());

                SQLString = "SELECT * FROM NESTDB.[dbo].[FCN_Book_Performance] (" + Id_Portfolio.ToString() + ") ORDER BY Id_Book";

                curUtils.SetColumnStyle(dgBookPerfSummary, 1);

                //dgFundPerfSummary.Columns["Portfolio"].Width = 200;

                dtg.DataSource = tempTable;

                if (dgBookPerfSummary.Columns.Count > 0)
                {
                    dgBookPerfSummary.Columns["Today"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    dgBookPerfSummary.Columns["Today"].DisplayFormat.FormatString = "P2";

                    dgBookPerfSummary.Columns["MTD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    dgBookPerfSummary.Columns["MTD"].DisplayFormat.FormatString = "P2";

                    dgBookPerfSummary.Columns["HTD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    dgBookPerfSummary.Columns["HTD"].DisplayFormat.FormatString = "P2";

                    dgBookPerfSummary.Columns["YTD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    dgBookPerfSummary.Columns["YTD"].DisplayFormat.FormatString = "P2";

                }
            }
        }

        public void Set_Portfolio_Values(int Id_Porfolio)
        {
            cmbPortfolio.SelectedValue = Id_Porfolio;
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

        private void timer2_Tick(object sender, EventArgs e)
        {
            int Id_Portfolio = int.Parse(cmbPortfolio.SelectedValue.ToString());
            string sQuery = @"  DECLARE @portfolio INT
                                SET @portfolio = " + Id_Portfolio.ToString() + @"

                                DECLARE @first DATETIME
                                DECLARE @last DATETIME

                                SET @first = [NESTDB].[dbo].[FCN_NDATEADD]('du',1,GETDATE()-DAY(GETDATE()- 1),1,1)
                                SET @last = [NESTDB].[dbo].[FCN_NDATEADD]('du',-1,GETDATE(),1,1)

                                DECLARE @ticker INTEGER
                                SELECT @ticker = [Id_Ticker] FROM [NESTDB].[dbo].[Tb002_Portfolios] WHERE [Id_Portfolio] = @portfolio
                                IF @portfolio = 4 SET @ticker = 5672 -- gambs

                                SELECT (SELECT SUM([Contribution pC Admin]) FROM NESTDB.dbo.Tb000_Historical_Positions WHERE [Date Now]>@first AND [Id Portfolio] = @portfolio) -
                                ((SELECT SrValue FROM NESTDB.dbo.Tb056_Precos_Fundos WHERE SrType = 1 AND Source = 7 AND IdSecurity = @ticker AND SrDate = @last)
                                /(SELECT SrValue FROM NESTDB.dbo.Tb056_Precos_Fundos WHERE SrType = 1 AND Source = 7 AND IdSecurity = @ticker AND SrDate = @first) - 1) 
                            ";
            DataTable oTable = curConn.Return_DataTable(sQuery);
            
            string sMTDError;
            if ((oTable.Rows.Count > 0) && (oTable.Rows[0][0] != DBNull.Value))
            {
                sMTDError = ((double)oTable.Rows[0][0]).ToString("0.00%");
            }
            else
            {
                sMTDError = "N/A";
            }
            lblErrorText.Text = sMTDError;
        }

        private void dgFundPerfSummary_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgBookPerfSummary);
            timer1.Start();

        }

        private void dgFundPerfSummary_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgBookPerfSummary);
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
            dgBookPerfSummary.SelectAll();
            dgBookPerfSummary.CopyToClipboard();
        }

        private void cmbPortfolio_SelectionChangeCommitted(object sender, EventArgs e)
        {
            timer2_Tick(this, null);
        }

    }
}