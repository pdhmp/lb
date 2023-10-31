using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestDLL;

namespace LiveBook
{
    public partial class frmAccounts : Form
    {
        public frmAccounts()
        {
            InitializeComponent();
            LoadBrokerData();
            LoadAccountData();
        }

        public void LoadBrokerData()
        {
            DataTable dtData = new DataTable();
            string sSQLString = " SELECT [Id_Corretora],[Nome] FROM [NESTDB].[dbo].[Tb011_Corretoras] ";

            using (newNestConn curConn = new newNestConn())
            {
                dtData = curConn.Return_DataTable(sSQLString);

                cboBroker.DisplayMember = "Nome";
                cboBroker.ValueMember = "Id_Corretora";
                cboBroker.DataSource = dtData;
                cboBroker.SelectedValue = -1;
            }
        }

        public void LoadAccountData()
        {
            DataTable dtData = new DataTable();
            string sSQLString = " SELECT    [Id_Account],[Id_Broker],[Nome],[Account_Number],[Account_Ticker] " +
                                " FROM      [NESTDB].[dbo].[Tb007_Accounts] " +
                                "           INNER JOIN [NESTDB].[dbo].[Tb011_Corretoras] ON [Id_Corretora] = [Id_Broker] " +
                                " ORDER BY  [Nome] ";

            dgEditData.Columns.Clear();
            using (newNestConn curConn = new newNestConn())
            {
                dtData = curConn.Return_DataTable(sSQLString);

                dtgData.DataSource = dtData;

                dtData.Dispose();
                dgEditData.RowHeight = 19;
                dgEditData.Columns["Id_Account"].Visible = false;
                dgEditData.Columns["Id_Broker"].Visible = false;
            }
        }

        private void dtgData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtAccountID.Text = dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_Account").ToString();
            cboBroker.SelectedValue = (int)dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_Broker");
            txtAccountNumber.Text = dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Account_Number").ToString();
            txtAccountTicker.Text = dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Account_Ticker").ToString();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            bool bValid = true;
            long lLong = 0;
            string sAccountNumber = txtAccountNumber.Text;

            if ((cboBroker.SelectedValue == null) || (int)cboBroker.SelectedValue <= 0) 
                bValid = false;

            if (sAccountNumber == "") 
                sAccountNumber = "NULL";
            else
                if (!long.TryParse(sAccountNumber, out lLong) || (lLong <= 0))
                    bValid = false;
                
            if (!bValid)
            {
                MessageBox.Show("Values not valid. Try again.", "Accounts", MessageBoxButtons.OK);
                return;
            }


            string sSQLString = "";
            if (txtAccountID.Text.Length > 0)
            {
                sSQLString = " UPDATE   [NESTDB].[dbo].[Tb007_Accounts] " +
                             " SET      [Id_Broker] = " + (int)cboBroker.SelectedValue +
                             "          ,[Account_Number] = " + sAccountNumber +
                             "          ,[Account_Ticker] = '" + txtAccountTicker.Text + "'" +
                             " WHERE    [Id_Account] = " + txtAccountID.Text;
            }
            else
            {
                if (MessageBox.Show("Insert a new Account?", "Accounts", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    sSQLString = " INSERT INTO   [NESTDB].[dbo].[Tb007_Accounts] " +
                                 "               ([Id_Broker],[Account_Number],[Account_Ticker])  " +
                                 " VALUES        (" + (int)cboBroker.SelectedValue + "," + sAccountNumber + ",'" + txtAccountTicker.Text + "')";
                }
            }

            if (sSQLString.Length > 0)
            {
                using (newNestConn curConn = new newNestConn())
                {
                    MessageBox.Show("NADA FEITO.", "Accounts", MessageBoxButtons.OK);
                    return;
                    curConn.ExecuteNonQuery(sSQLString);
                    LoadAccountData();
                }

            }

        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            txtAccountID.Text = "";
            txtAccountTicker.Text = "";
            txtAccountNumber.Text = "";
            cboBroker.SelectedValue = -1;
        }

    }
}
