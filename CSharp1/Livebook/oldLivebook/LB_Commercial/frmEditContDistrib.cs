using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using LiveBook.Business;

using DevExpress.XtraEditors.Repository;
namespace LiveBook
{
    public partial class frmEditContDistrib : Form
    {
        
        Business_Class Negocios = new Business_Class();
        LB_Utils curUtils = new LB_Utils();
        RepositoryItemLookUpEdit comboDistributor;

        public frmEditContDistrib()
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

        public void LoadContInfo()
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString;

                DataTable tablep = new DataTable();

                SQLString = " SELECT Id_DistContact, FromDate, A.Id_Distributor " +
                            " FROM NESTDB.dbo.Tb752_DistContacts A (nolock) " +
                            " LEFT JOIN NESTDB.dbo.Tb751_Contacts C (nolock) " +
                            "     ON A.Id_Contact=C.Id_Contact " +
                            " LEFT JOIN NESTDB.dbo.Tb750_Distributors D (nolock) " +
                            "     ON A.Id_Distributor=D.Id_Distributor " +
                            " WHERE A.Id_Contact= " + txtRelID.Text;

                dgEditData.Columns.Clear();

                tablep = curConn.Return_DataTable(SQLString);

                dtgEditData.DataSource = tablep;

                tablep.Dispose();
                dgEditData.RowHeight = 19;
                dgEditData.Columns["Id_DistContact"].Visible = false;

                string SQLString2 = " SELECT Id_Distributor, DistributorName FROM NESTDB.dbo.Tb750_Distributors (nolock) ";

                DataTable tablep2 = curConn.Return_DataTable(SQLString2);

                comboDistributor = new RepositoryItemLookUpEdit();
                DataView dvt = new DataView(tablep2);

                comboDistributor.DataSource = dvt;


                comboDistributor.DisplayMember = "DistributorName";
                comboDistributor.ValueMember = "Id_Distributor";



                dgEditData.Columns["Id_Distributor"].ColumnEdit = comboDistributor;
            }
        }

        private void dgEditData_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name == "colId_Distributor")
            {
                int Id_DistContact = (int)dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_DistContact");

                string SQLString = " UPDATE NESTDB.dbo.Tb752_DistContacts  " +
                            " SET Id_Distributor= " + e.Value.ToString() + " " +
                            " WHERE Id_DistContact = " + Id_DistContact.ToString();

                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(SQLString);
                }
                LoadContInfo();
            }

            if (e.Column.Name == "colFromDate")
            {
                int Id_DistContact = (int)dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_DistContact");

                string SQLString = " UPDATE NESTDB.dbo.Tb752_DistContacts  " +
                            " SET From_Date= " + Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd") + " " +
                            " WHERE Id_DistContact = " + Id_DistContact.ToString();
                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(SQLString);
                }
                LoadContInfo();
            }

        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            int Id_Contact = Convert.ToInt32(txtRelID.Text);

            string SQLString = " INSERT INTO NESTDB.dbo.Tb752_DistContacts SELECT '1900-01-01', 1, " + Id_Contact.ToString();
            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery(SQLString);
            }
            LoadContInfo();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int resposta = Convert.ToInt32(MessageBox.Show("Do you really want delete this distributor alocation?", "Delete Distributor Alocation", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

            if (resposta == 6)
            {

                int Id_DistContact = (int)dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_DistContact");

                string SQLString = " DELETE FROM NESTDB.dbo.Tb752_DistContacts  " +
                            " WHERE Id_DistContact = " + Id_DistContact.ToString();

                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery(SQLString);
                }
                LoadContInfo();
            }
        }
    }
}