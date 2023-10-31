using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;

using LiveBook.Business;
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

namespace LiveBook
{
    public partial class frmSpam : Form
    {
        LB_Utils curUtils = new LB_Utils();
        newNestConn curConn = new newNestConn();

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

            DataTable tablet = curConn.Return_DataTable(SQLString);

            dtgSpam.DataSource = tablet;

            curUtils.SetColumnStyle(dgSpam, 2, "Trade Quantity");
            
        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            CarregaGrid();
        }

    }
}