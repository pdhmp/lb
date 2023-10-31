using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;
using SGN.Validacao;
using SGN.Business;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Npgsql;

namespace SGN
{
    public partial class frmSpam : Form
    {
        Valida Validacao = new Valida();
        CarregaDados CargaDados = new CarregaDados();

        public frmSpam()
        {
            InitializeComponent();
        }

        private void frmSpam_Load(object sender, EventArgs e)
        {

        }

        void CarregaGrid()
        {
            string SQLString = "Select * from spam_requests " +
                               " where update_time >= '" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") + "00:00:00" + "' and update_time <= '" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") + "23:29:00" + "'";

            DataTable tablet = CargaDados.curConn.Return_DataTable(SQLString);

            dtgSpam.DataSource = tablet;

            Validacao.SetColumnStyle(dgSpam, 2, "Trade Quantity");
            
        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

    }
}