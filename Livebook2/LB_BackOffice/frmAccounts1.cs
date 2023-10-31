using System;
using System.Data;
using System.Windows.Forms;
using NestDLL;

namespace LiveBook
{
    public partial class frmAccounts : Form
    {
        public frmAccounts()
        {
            InitializeComponent();
        }

        private void frmAccounts_Load(object sender, EventArgs e)
        {

            cmbBroker.Text = "";
            cmbPortfolio.Text = "";

            chkByBroker.Checked = false;
            chkByPortfolio.Checked = false;
        }

        private int IdPortfolio;
        private int IdBroker;
        private string SQLString = "";

        private void chkByPortfolio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkByPortfolio.Checked)
            {
                cmbPortfolio.Enabled = true;
                LoadByPortfolio();
            }
            else
            {
                cmbPortfolio.Text = "";
                cmbPortfolio.Enabled = false;
                IdPortfolio = 0;
            }
            LoadGrid();
        }

        private void chkByBroker_CheckedChanged(object sender, EventArgs e)
        {
            if (chkByBroker.Checked)
            {
                cmbBroker.Enabled = true;
                LoadByBroker();
            }
            else
            {
                cmbBroker.Text = "";
                cmbBroker.Enabled = false;
                IdBroker = 0;
            }
            LoadGrid();
        }

        private void LoadByPortfolio()
        {
            SQLString = "SELECT [Id_Portfolio], Port_Name FROM [NESTDB].[dbo].[Tb002_Portfolios] WHERE Discountinued = 0  AND Id_Port_Type = 2 ORDER BY Port_Name;";
            NestDLL.FormUtils.LoadCombo(cmbPortfolio, SQLString, "Id_Portfolio", "Port_Name");
        }

        private void LoadByBroker()
        {
            SQLString = "SELECT [Id_Corretora],[Nome] FROM [NESTDB].[dbo].[Tb011_Corretoras] ORDER BY [Nome]";
            NestDLL.FormUtils.LoadCombo(cmbBroker, SQLString, "Id_Corretora", "Nome");
        }

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cmbPortfolio.SelectedValue.ToString(), out IdPortfolio);
            LoadGrid();
        }

        private void cmbBroker_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cmbBroker.SelectedValue.ToString(), out IdBroker);
            LoadGrid();
        }

        private void LoadGrid()
        {
            if (IdPortfolio != 0 && IdBroker != 0)
            {
                SQLString =
                   " SELECT A.Port_Name,D.Nome,C.[Account_Number],C.[Account_Ticker] " +
                   " FROM [NESTDB].[dbo].[Tb002_Portfolios] A " +
                   " LEFT JOIN   [NESTDB].[DBO].[Tb003_PortAccounts] B ON A.Id_Portfolio = B.Id_Portfolio AND A.Id_Port_Type = 2 " +
                   " LEFT JOIN  [NESTDB].[DBO].[Tb007_Accounts] C ON B.Id_Account = C.Id_Account  " +
                   " LEFT JOIN [NESTDB].[DBO].[Tb011_Corretoras] D ON D.Id_Corretora = C.Id_Broker " +
                   " WHERE (Nome IS NOT NULL OR Account_Number IS NOT NULL OR Account_Ticker IS NOT NULL)  " +
                   " AND A.Id_Portfolio = " + IdPortfolio +
                   " AND C.Id_Broker = " + IdBroker;

            }
            else if (IdPortfolio != 0)
            {

            }
            else if (IdBroker != 0)
            {

            }

        }
    }
}
