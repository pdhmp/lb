using System;
using System.Data;
using System.Windows.Forms;
using LiveBook.Business;
using LiveDLL;

namespace LiveBook
{
    public partial class frmEditDistribRebate : Form
    {
        
        Business_Class Negocios = new Business_Class();
        LB_Utils curUtils = new LB_Utils();

        public frmEditDistribRebate()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmStock_Loan_Close_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadDistRebates()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            SQLString = " SELECT Id_DistRebate, FromDate, Rebate_Manag, Rebate_Perf " +
                        " FROM NESTDB.dbo.Tb753_DistRebates A (nolock) " +
                        " WHERE Id_Distributor= " + txtId_Distributor.Text + " AND Id_Portfolio=" + txtId_Portfolio.Text;

            dgEditData.Columns.Clear();
            using (newNestConn curConn = new newNestConn())
            {
                tablep = curConn.Return_DataTable(SQLString);

                dtgEditData.DataSource = tablep;

                tablep.Dispose();
                dgEditData.RowHeight = 19;
                dgEditData.Columns["Id_DistRebate"].Visible = false;

                dgEditData.Columns["Rebate_Manag"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgEditData.Columns["Rebate_Manag"].DisplayFormat.FormatString = "0.00%;(0.00%)";

                dgEditData.Columns["Rebate_Perf"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgEditData.Columns["Rebate_Perf"].DisplayFormat.FormatString = "0.00%;(0.00%)";
            }
        }

        private void dgEditData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colRebate_Manag")
            {
                int Id_DistRebate = (int)dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_DistRebate");

                string SQLString = " UPDATE NESTDB.dbo.Tb753_DistRebates  " +
                            " SET Rebate_Manag= " + e.Value.ToString().Replace(",",".") + " " +
                            " WHERE Id_DistRebate = " + Id_DistRebate.ToString();
                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(SQLString,1);
                }
                LoadDistRebates();
            }

            if (e.Column.Name == "colRebate_Perf")
            {
                int Id_DistRebate = (int)dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_DistRebate");

                string SQLString = " UPDATE NESTDB.dbo.Tb753_DistRebates  " +
                            " SET Rebate_Perf= " + e.Value.ToString().Replace(",", ".") + " " +
                            " WHERE Id_DistRebate = " + Id_DistRebate.ToString();

                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(SQLString,1);
                }
                LoadDistRebates();
            }

            if (e.Column.Name == "colFromDate")
            {
                int Id_DistRebate = (int)dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_DistRebate");

                string SQLString = " UPDATE NESTDB.dbo.Tb753_DistRebates  " +
                            " SET FromDate='" + Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd") + "' " +
                            " WHERE Id_DistRebate = " + Id_DistRebate.ToString();

                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(SQLString,1);
                }
                LoadDistRebates();
            }

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            int Id_Contact = Convert.ToInt32(txtId_Distributor.Text);

            string SQLString = " INSERT INTO NESTDB.dbo.Tb753_DistRebates SELECT '1900-01-02', " + txtId_Portfolio.Text + ", " + txtId_Distributor.Text + ", 0, 0";

            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery(SQLString,1);
            }
            LoadDistRebates();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int resposta = Convert.ToInt32(MessageBox.Show("Do you really want delete this distributor rebate entry?", "Delete Distributor Rebate", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

            if (resposta == 6)
            {

                int Id_DistRebate = (int)dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_DistRebate");

                string SQLString = " DELETE FROM NESTDB.dbo.Tb753_DistRebates  " +
                            " WHERE Id_DistRebate = " + Id_DistRebate.ToString();

                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(SQLString,1);
                }
                LoadDistRebates();
            }
        }
    }
}