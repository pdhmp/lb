using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using System.Data.SqlClient;

namespace SGN
{
    public partial class frmSubPortfolio : LBForm
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
        bool processing = false;
        DateTime DateReport;

        public frmSubPortfolio()
        {
            InitializeComponent();
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }


        private void frmBookSummary_Load(object sender, EventArgs e)
        {
            

            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            DateReport = DateTime.Now;

            CargaDados.carregacombo(this.cmbView, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2 UNION ALL SELECT '-1', 'All Portfolios'", "Id_Portfolio", "Port_Name", 99);
            
            Carrega_Grid(Convert.ToInt32(cmbView.SelectedValue.ToString()));
            timer1.Start();
        }

        void Carrega_Grid(int Id_Portfolio)
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            if (DateReport.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                SQLString = "SELECT * FROM dbo.FCN_GET_Book_Summary(" + Id_Portfolio + ");";
            }
            else
            {
                SQLString = "SELECT * FROM dbo.FCN_GET_Book_Summary_Historical(" + Id_Portfolio + ",'" + DateReport.ToString("yyyyMMdd") + "');";
            }

            try
            {
                tablep = CargaDados.curConn.Return_DataTable(SQLString);

                dtg.DataSource = tablep;
                

                Valida.SetColumnStyle(dgBookSummary, 1);

            }
            catch(Exception e) 
            {
                Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }

            dgBookSummary.Columns["Long"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgBookSummary.Columns["Long"].DisplayFormat.FormatString = "P2";

            dgBookSummary.Columns["Short"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgBookSummary.Columns["Short"].DisplayFormat.FormatString = "P2";

            dgBookSummary.Columns["Gross"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgBookSummary.Columns["Gross"].DisplayFormat.FormatString = "P2";

            dgBookSummary.Columns["Net"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgBookSummary.Columns["Net"].DisplayFormat.FormatString = "P2";

            dgBookSummary.Columns["Perf"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgBookSummary.Columns["Perf"].DisplayFormat.FormatString = "P2";

            dgBookSummary.Columns["VAR %"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgBookSummary.Columns["VAR %"].DisplayFormat.FormatString = "P2";

            dgBookSummary.Columns["VAR(M) %"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgBookSummary.Columns["VAR(M) %"].DisplayFormat.FormatString = "P2";
            
            dgBookSummary.Columns["VAR_DateTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgBookSummary.Columns["VAR_DateTime"].DisplayFormat.FormatString = "HH:mm";
        }

        public void Set_Portfolio_Values(int Id_Portfolio,DateTime Historical)
        {
            cmbView.SelectedValue = Id_Portfolio;
            DateReport = Historical;

            if (DateReport.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                this.Text = "Exposure Strategy";
            }
            else
            {
                this.Text = "Exposure Strategy (HISTORICAL as of " + DateReport.ToString("dd/MM/yy") + ")";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!processing)
            {
                processing = true;
                Carrega_Grid(Convert.ToInt32(cmbView.SelectedValue.ToString()));
                processing = false;
            }
        }

        private void dgBookSummary_MouseUp(object sender, MouseEventArgs e)
        {
            processing = false;

        }

        private void dgBookSummary_MouseDown(object sender, MouseEventArgs e)
        {
            processing = true;

        }

        private void dgBookSummary_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            timer1.Stop();
            Valida.Save_Columns(dgBookSummary);
            timer1.Start();
        }

        private void dgBookSummary_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            timer1.Stop();
            Valida.Save_Columns(dgBookSummary);
            timer1.Start();
        }
    }
}