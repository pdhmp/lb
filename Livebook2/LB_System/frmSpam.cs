using System;
using System.Data;
using System.Windows.Forms;
using LiveDLL;

// using Npgsql;

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