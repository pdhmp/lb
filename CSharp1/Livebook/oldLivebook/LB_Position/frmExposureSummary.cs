using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestDLL;
using System.Data.SqlClient;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;


namespace LiveBook
{
    public partial class frmPortSummary : LBForm
    {
        old_Conn curConn = new old_Conn();
        
        bool processing = false;
        DateTime DateReport;

        public frmPortSummary()
        {
            InitializeComponent();
        }

        private void frmPortSummary_Load(object sender, EventArgs e)
        {
            
            DateReport = DateTime.Now;

            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            Carrega_Grid();
            timer1.Start();
            lblCopy.BringToFront();
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }
        public void Set_Historical_Falg(DateTime GetDate)
        {
            DateReport = GetDate;

            if (DateReport.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                this.Text = "Exposure Summaries";
            }
            else
            {
                this.Text = "Exposure Summaries (HISTORICAL as of " + DateReport.ToString("dd/MM/yy") + ")";
            }
        }

        void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();
            string String_Function;

            if (DateReport.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                String_Function = "[dbo].[FCN_GET_Port_Summary] ()";
            }
            else
            {
                String_Function = "[dbo].[FCN_GET_Port_Summary_Historical]('" + DateReport.ToString("yyyyMMdd") + "')";
            }

                SQLString = "Select * FROM " + String_Function;

            try
            {
                tablep = curConn.Return_DataTable(SQLString);
                dtg.DataSource = tablep;
                curUtils.SetColumnStyle(dgPortSummary, 1);
            }
            catch(Exception e) 
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());
            }

            dgPortSummary.Columns["Long"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["Long"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["Short"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["Short"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["Gross"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["Gross"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["Net"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["Net"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["Perf"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["Perf"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["NAV (MM)"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["NAV (MM)"].DisplayFormat.FormatString = "{0:#,#0.00}";

            dgPortSummary.Columns["VAR %"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["VAR %"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["VAR MC %"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["VAR MC %"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["X uC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["X uC"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["VAR_DateTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgPortSummary.Columns["VAR_DateTime"].DisplayFormat.FormatString = "HH:mm";

            dgPortSummary.Columns["Perf ADM"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["Perf ADM"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["Diff"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["Diff"].DisplayFormat.FormatString = "P2";


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!processing)
            {
                processing = true;
                Carrega_Grid();
                processing = false;
            }
        }

        private void dgresume_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgPortSummary);
            timer1.Start();

        }

        private void dgresume_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgPortSummary);
            timer1.Start();

        }

        private void dgresume_MouseUp(object sender, MouseEventArgs e)
        {
            processing = false;

        }

        private void dgresume_MouseDown(object sender, MouseEventArgs e)
        {
            processing = true;

        }

        private void dgresume_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            if (e.Column.Name == "colPerf" || e.Column.Name == "colPerfADM")
            {
                if (Convert.ToSingle(e.CellValue) > 0.0010)
                {
                    if (e.Appearance.BackColor != Color.White)
                    {
                        e.Appearance.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Green;
                    }
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if (Convert.ToSingle(e.CellValue) < -0.0010)
                {
                    if (e.Appearance.BackColor != Color.White)
                    {
                        e.Appearance.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Red;
                    }
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                }
            }

            if (e.Column.Name == "colPortfolio")
            {
                int tempVal = (int)dgPortSummary.GetRowCellValue(e.RowHandle, "Lim_Any");
                if (tempVal >0 )
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                }
            }

            if (e.Column.Name == "colVARMC%" || e.Column.Name == "colLong" || e.Column.Name == "colShort" || e.Column.Name == "colNet" || e.Column.Name == "colGross")
            {
                int tempVal = 0;
                if (e.Column.Name == "colVARMC%") { tempVal = (int)dgPortSummary.GetRowCellValue(e.RowHandle, "Lim_VAR"); };
                if (e.Column.Name == "colLong") { tempVal = (int)dgPortSummary.GetRowCellValue(e.RowHandle, "Lim_Long"); };
                if (e.Column.Name == "colShort") { tempVal = (int)dgPortSummary.GetRowCellValue(e.RowHandle, "Lim_Short"); };
                if (e.Column.Name == "colNet") { tempVal = (int)dgPortSummary.GetRowCellValue(e.RowHandle, "Lim_Net"); };
                if (e.Column.Name == "colGross") { tempVal = (int)dgPortSummary.GetRowCellValue(e.RowHandle, "Lim_Gross"); };
                if (tempVal > 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else
                {
                    if (e.Appearance.BackColor != Color.White)
                    {
                        e.Appearance.ForeColor = Color.White;
                    }
                    else
                    {
                        e.Appearance.ForeColor = Color.Black;
                    }
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                }
            }

            if (e.Column.Name == "colVAR_DateTime" || e.Column.Name == "colMinCloseDate")
            {
                if (e.CellValue != null && e.CellValue.ToString() != "")
                {
                    if (Convert.ToDateTime(e.CellValue).Date != DateTime.Now.Date)
                    { 
                        e.DisplayText = Convert.ToDateTime(e.CellValue).ToString("dd-MMM");
                    }
                }
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgPortSummary.SelectAll();
            dgPortSummary.CopyToClipboard();
            //  MessageBox.Show("Copied!");

        }
    }
}