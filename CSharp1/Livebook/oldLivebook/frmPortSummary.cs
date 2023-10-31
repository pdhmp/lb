using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using SGN.CargaDados;
using System.Data.SqlClient;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;


namespace SGN
{
    public partial class frmPortSummary : Form
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
        public int Id_usuario;
        bool processing = false;
        DateTime DateReport;

        public frmPortSummary()
        {
            InitializeComponent();
        }

        private void frmPortSummary_Load(object sender, EventArgs e)
        {
            CargaDados.DB.User_Id = Id_usuario;
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
            string StringSQL;
            SqlDataAdapter dp = new SqlDataAdapter();
            DataTable tablep = new DataTable();
            StyleFormatCondition cn;
            string String_Function;

            if (DateReport.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                String_Function = "[dbo].[FCN_GET_Port_Summary] ()";
            }
            else
            {
                String_Function = "[dbo].[FCN_GET_Port_Summary_Historical]('" + DateReport.ToString("yyyyMMdd") + "')";
            }
            
            if (Id_usuario == 2)
            {
                StringSQL = "Select * FROM " + String_Function + " Where Id_portfolio in(4,10,43,45)";
            }
            else
            {
                StringSQL = "Select * FROM " + String_Function;
            }
            try
            {
                dp = CargaDados.DB.Return_DataAdapter(StringSQL);
                dp.Fill(tablep);

                dtg.DataSource = tablep;
                dp.Dispose();

                Valida.SetColumnStyle(dgPortSummary, Id_usuario, 1);
            }
            catch(Exception e) 
            {
                Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());

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

            dgPortSummary.Columns["X uC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["X uC"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["VAR_DateTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgPortSummary.Columns["VAR_DateTime"].DisplayFormat.FormatString = "HH:mm";

            dgPortSummary.Columns["Perf ADM"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["Perf ADM"].DisplayFormat.FormatString = "P2";

            dgPortSummary.Columns["Diff"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPortSummary.Columns["Diff"].DisplayFormat.FormatString = "P2";


        }

        private void frmPortSummary_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Stop();
            Valida.Save_Properties_Form(this, Id_usuario, 0);

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
            Valida.Save_Columns(dgPortSummary, Id_usuario);
            timer1.Start();

        }

        private void dgresume_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            timer1.Stop();
            Valida.Save_Columns(dgPortSummary, Id_usuario);
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
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if (Convert.ToSingle(e.CellValue) < -0.0010)
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

            if (e.Column.Name == "colVAR%")
            {
                int tempVal = (int)dgPortSummary.GetRowCellValue(e.RowHandle, "Lim_VAR");
                if (tempVal > 0)
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

            if (e.Column.Name == "colVAR_DateTime")
            {
                if (e.CellValue != null && e.CellValue.ToString() != "")
                {
                    if (Convert.ToDateTime(e.CellValue) < DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)))
                    { e.Appearance.ForeColor = Color.White; }
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