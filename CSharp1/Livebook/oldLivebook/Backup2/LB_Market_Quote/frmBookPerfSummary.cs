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

using System.Threading;

namespace SGN
{
    public partial class frmBookPerfSummary : LBForm
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
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

                Valida.SetColumnStyle(dgBookPerfSummary, 1);

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
                ThreadTable = CargaDados.curConn.Return_DataTable(SQLString);
                tempTable = ThreadTable;
            }
            catch(Exception e) 
            {
                Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());
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
            Valida.Save_Columns(dgBookPerfSummary);
            timer1.Start();

        }

        private void dgFundPerfSummary_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            timer1.Stop();
            Valida.Save_Columns(dgBookPerfSummary);
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

        private void dtg_Click(object sender, EventArgs e)
        {

        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgBookPerfSummary.SelectAll();
            dgBookPerfSummary.CopyToClipboard();
            //  MessageBox.Show("Copied!");

        }
    }
}