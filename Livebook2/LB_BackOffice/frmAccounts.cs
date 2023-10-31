using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmAccounts : Form
    {
        public frmAccounts()
        {
            InitializeComponent();
            txtIdAccount.BackColor = Color.White;
            LoadPortfolioData();
            LoadBrokerData();
        }

        private newNestConn curConn = new newNestConn();

        private string SQLString = "";
        private string AccountTicker = "NULL";
        private string AccountNumber = "NULL";
        private string PortFilter = "";
        private string IdAccount = "";

        private int IdBroker = 0;
        private int _IdPortfolio = 0;
        public int IdPortfolio
        {
            get { return _IdPortfolio; }
            set
            {
                _IdPortfolio = value;

                switch (value)
                {
                    case 4: { PortFilter = "4,5"; break; }
                    case 10: { PortFilter = "10,11"; break; }
                    case 18: { PortFilter = "18,17"; break; }
                    case 38: { PortFilter = "38,39"; break; }
                    case 43: { PortFilter = "43,41"; break; }
                    case 50: { PortFilter = "50,51"; break; }
                    case 55: { PortFilter = "55,56"; break; }
                    case 60: { PortFilter = "60,61"; break; }
                    case 80: { PortFilter = "80,81"; break; }
                    default: { PortFilter = ""; break; }
                }
                LoadAccountData();
            }
        }

        public void LoadBrokerData()
        {
            SQLString = "SELECT [Id_Corretora],[Nome] FROM [NESTDB].[dbo].[Tb011_Corretoras] ";
            LiveDLL.FormUtils.LoadCombo(cmbBroker, SQLString, "Id_Corretora", "Nome", -1);
        }
        public void LoadPortfolioData()
        {
            SQLString = " SELECT Id_Portfolio,Port_Name FROM [NESTDB].[dbo].[Tb002_Portfolios] WHERE Id_Port_Type = 2 and Discountinued = 0 ";
            LiveDLL.FormUtils.LoadCombo(cmbPortfolio, SQLString, "Id_Portfolio", "Port_Name", 1);
        }
        public void LoadAccountData()
        {
            SQLString =
                        " SELECT   A.[Id_Account],Id_Portfolio,[Id_Broker],[Nome],[Account_Number],[Account_Ticker] " +
                        " FROM      [NESTDB].[dbo].[Tb007_Accounts] A " +
                        " INNER JOIN [NESTDB].[dbo].[Tb011_Corretoras] B ON [Id_Corretora] = [Id_Broker] " +
                        " INNER JOIN  [NESTDB].[dbo].Tb003_PortAccounts C ON  A.Id_Account = C.Id_Account " +
                        " WHERE C.Id_Portfolio IN (" + IdPortfolio + ") " +
                        " ORDER BY  [Nome]";

            dgEditData.Columns.Clear();

            dtgData.DataSource = curConn.Return_DataTable(SQLString);

            dgEditData.RowHeight = 19;
            dgEditData.Columns["Id_Account"].Visible = false;
            dgEditData.Columns["Id_Broker"].Visible = false;
        }

        public void UpdateAccount()
        {
            if ((cmbBroker.SelectedValue == null) || (int)cmbBroker.SelectedValue <= 0)
            {
                MessageBox.Show("Select a valid Broker", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearFields();
                return;
            }
            if (AccountNumber != "NULL")
            {
                foreach (char Letter in AccountNumber)
                {
                    if (!char.IsNumber(Letter))
                    {
                        MessageBox.Show("Account number cannot contain a letter", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearFields();
                        return;
                    }
                }
            }

            long x = 0;
            if (!long.TryParse(AccountNumber, out x) && x < 0)
            {
                MessageBox.Show("Account number cannot be negative", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ClearFields();
                return;
            }

            if (IdAccount.Length > 0)
            {
                // Update em uma conta existente
                SQLString = " UPDATE   [NESTDB].[dbo].[Tb007_Accounts] " +
                             " SET      [Account_Number] = " + AccountNumber +
                             "          ,[Account_Ticker] = '" + txtAccountTicker.Text + "'" +
                             " WHERE    [Id_Account] = " + txtIdAccount.Text;

                if (curConn.ExecuteNonQuery(SQLString) > 0)
                {
                    MessageBox.Show("Account Updated", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Account not updated", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //Checa se a conta existente já está linkada ao portfolio

                SQLString =" SELECT COUNT (*) FROM [NESTDB].[dbo].Tb003_PortAccounts WHERE Id_Account = " + IdAccount;

                if (curConn.Return_Int(SQLString) == 0)
                {
                    SQLString = "INSERT [NESTDB].[dbo].Tb003_PortAccounts " +
                                "SELECT Id_Portfolio, " + IdAccount + " FROM Tb002_Portfolios WHERE Id_Portfolio IN(" + PortFilter + ")";

                    if (curConn.ExecuteNonQuery(SQLString) > 0)
                    {
                        MessageBox.Show("Account linked to " + cmbPortfolio.Text + "", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Account not linked  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                //Checa se o broker já está linkado ao portfolio.
                SQLString =
                        " SELECT COUNT (*) " +
                        " FROM [NESTDB].[dbo].Tb003_PortAccounts A " +
                        " INNER JOIN [NESTDB].[dbo].[Tb007_Accounts] B  " +
                        " ON A.Id_Account = B.Id_Account AND B.Id_Broker = " + IdBroker + " AND Id_Portfolio IN (" + PortFilter + ") ; ";

                if (curConn.Return_Int(SQLString) > 0)
                {
                    MessageBox.Show("This Broker already linked to this Portfolio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Se nao estiver, insere e linka.
                if (MessageBox.Show("Insert a new Account?", "New Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SQLString = " INSERT INTO   [NESTDB].[dbo].[Tb007_Accounts] " +
                                 "               ([Id_Broker],[Account_Number],[Account_Ticker]) ";

                    if (AccountTicker != "NULL")
                        SQLString += " VALUES        (" + IdBroker + "," + AccountNumber + ",'" + AccountTicker + "') ;";
                    else
                        SQLString += " VALUES        (" + IdBroker + "," + AccountNumber + "," + AccountTicker + ") ;";

                    if (curConn.ExecuteNonQuery(SQLString) > 0)
                    {
                        MessageBox.Show("Account Inserted", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        int NewIdAccount = curConn.Return_Int("SELECT @@IDENTITY");

                        SQLString =
                            "INSERT [NESTDB].[dbo].Tb003_PortAccounts " +
                            "SELECT Id_Portfolio, " + NewIdAccount + " FROM Tb002_Portfolios WHERE Id_Portfolio IN(" + PortFilter + ")";

                        if (curConn.ExecuteNonQuery(SQLString) > 0)
                        {
                            MessageBox.Show("Account linked to " + cmbPortfolio.Text + "", "Sucess", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Account not linked  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Account not inserted  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dtgData_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtIdAccount.Text = dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_Account").ToString();
            cmbBroker.SelectedValue = (int)dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Id_Broker");
            txtAccountNumber.Text = dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Account_Number").ToString();
            txtAccountTicker.Text = dgEditData.GetRowCellValue(dgEditData.FocusedRowHandle, "Account_Ticker").ToString();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            UpdateAccount();
            LoadAccountData();
            ClearFields();
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int port = 0;
            if ((int)cmbPortfolio.SelectedValue != -1)
            {
                if (int.TryParse(cmbPortfolio.SelectedValue.ToString(), out port))
                {
                    IdPortfolio = port;
                    return;
                }
            }
            cmbPortfolio.SelectedValue = -1;
        }
        private void cmbBroker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((int)cmbBroker.SelectedValue != -1)
            {
                if (int.TryParse(cmbBroker.SelectedValue.ToString(), out IdBroker))
                    return;
            }
            cmbBroker.SelectedValue = -1;
        }

        private void txtIdAccount_TextChanged(object sender, EventArgs e)
        {
            if (txtIdAccount.Text != "")
            {
                IdAccount = txtIdAccount.Text;
            }
            else
            {
                IdAccount = "";
            }
        }
        private void txtAccountNumber_TextChanged(object sender, EventArgs e)
        {
            if (txtAccountNumber.Text != "")
            {
                AccountNumber = txtAccountNumber.Text;
            }
            else
            {
                AccountNumber = "NULL";
            }
        }
        private void txtAccountTicker_TextChanged(object sender, EventArgs e)
        {
            if (txtAccountTicker.Text != "")
            {
                AccountTicker = txtAccountTicker.Text;
            }
            else
            {
                AccountTicker = "NULL";
            }
        }

        private void ClearFields()
        {
            txtIdAccount.Text = "";
            txtAccountTicker.Text = "";
            txtAccountNumber.Text = "";
        }
    }
}
