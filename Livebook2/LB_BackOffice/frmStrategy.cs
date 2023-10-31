using System;
using System.Windows.Forms;
using LiveDLL;


namespace LiveBook
{
    public partial class frmStrategy : Form
    {
        newNestConn curConn = new newNestConn();

        int IdSubPortfolio = 0;
        int IdStrategy = 0;

        const string SubPortText = "New SubPortfolio";
        const string StrategyText = "New Strategy";
        const string SubStrategyText = "New SubStrategy";

        string SQLString = "";
        string SQLString1 = "";

        public frmStrategy() { InitializeComponent(); }
        private void frmStrategy_Load(object sender, EventArgs e) { LoadSubPortfolio(true); }

        public void LoadSubPortfolio(bool AllCollections)
        {
            SQLString = "SELECT [Id_Sub_Portfolio],[Sub_Portfolio] FROM [NESTDB].[dbo].[Tb401_Sub_Portfolios]";
            LiveDLL.FormUtils.LoadList(ListSubPorts, SQLString, "Id_Sub_Portfolio", "Sub_Portfolio");

            if (AllCollections)
            {
                SQLString1 = "SELECT Id_Sub_Portfolio,Sub_Portfolio FROM ( " +
                             "SELECT 0 as Id_Sub_Portfolio,'Select SubPortfolio' as Sub_Portfolio union all " +
                             "SELECT [Id_Sub_Portfolio],[Sub_Portfolio] " +
                             "FROM [NESTDB].[dbo].[Tb401_Sub_Portfolios] )x " +
                             "ORDER BY CASE WHEN Id_Sub_Portfolio <> 0 THEN Sub_Portfolio END ";

                LiveDLL.FormUtils.LoadCombo(cmbSubPorts, SQLString1, "Id_Sub_Portfolio", "Sub_Portfolio");
            }
        }
        public void LoadStrategy(bool AllCollections)
        {
            SQLString = "SELECT [Id_Strategy],[Strategy] FROM [NESTDB].[dbo].[Tb402_Strategy] WHERE [Id_Sub_Portfolio] = " + ListSubPorts.SelectedValue;
            LiveDLL.FormUtils.LoadList(ListStrategy, SQLString, "Id_Strategy", "Strategy");

            if (AllCollections)
            {
                SQLString1 = "SELECT Id_Strategy, Strategy FROM " +
                            "(SELECT 0 AS [Id_Strategy] , 'Select Strategy' AS Strategy Union ALL " +
                            "SELECT [Id_Strategy],[Strategy] FROM [NESTDB].[dbo].[Tb402_Strategy])X " +
                            "ORDER BY CASE WHEN Id_Strategy <> 0 THEN Strategy END; ";
                LiveDLL.FormUtils.LoadCombo(cmbStrategy, SQLString1, "Id_Strategy", "Strategy");
            }
        }
        public void LoadSubEstrategy()
        {
            LoadSubEstrategy((int)ListStrategy.SelectedValue);
        }
        public void LoadSubEstrategy(int IdStrategy)
        {
            SQLString = "SELECT [Id_Sub_Strategy],[Sub_Strategy] FROM [NESTDB].[dbo].[Tb403_Sub_Strategy] WHERE Id_Strategy = " + IdStrategy;
            LiveDLL.FormUtils.LoadList(this.ListSubStrategy, SQLString, "Id_Sub_Strategy", "Sub_Strategy");
        }

        private void InsertSubPortfolio()
        {
            string _SubPortName = txtSubPort.Text;

            if (_SubPortName == "" || _SubPortName == SubPortText)
            {
                MessageBox.Show("Insert New SubPortfolio name", "Error", MessageBoxButtons.OK);
                return;
            }

            if (curConn.Return_Int("SELECT COUNT (*) FROM [NESTDB].[dbo].[Tb401_Sub_Portfolios] where Sub_Portfolio = '" + _SubPortName + "'") > 0)
            {
                MessageBox.Show("SubPortfolio name already in use", "Error", MessageBoxButtons.OK);
                return;
            }

            if (curConn.ExecuteNonQuery("INSERT [NESTDB].[dbo].[Tb401_Sub_Portfolios] SELECT '" + _SubPortName + "'") > 0)
            {
                MessageBox.Show("Inserted!", "Sucess", MessageBoxButtons.OK);
                return;
            }
            MessageBox.Show("Not Inserted!", "Error", MessageBoxButtons.OK);
        }
        private void InsertStrategy()
        {
            string _StrategyName = txtStrategy.Text;

            if (IdSubPortfolio == 0)
            {
                MessageBox.Show("Select SubPortfolio", "Error", MessageBoxButtons.OK);
                return;
            }

            if (_StrategyName == StrategyText || _StrategyName == "")
            {
                MessageBox.Show("Insert New Strategy name", "Error", MessageBoxButtons.OK);
                return;
            }

            if (curConn.Return_Int("SELECT  COUNT (*) FROM [NESTDB].[dbo].[Tb402_Strategy] WHERE Strategy = '" + _StrategyName + "'") > 0)
            {
                MessageBox.Show("Strategy name already in use", "Error", MessageBoxButtons.OK);
                return;
            }

            if (curConn.ExecuteNonQuery("Insert [NESTDB].[dbo].[Tb402_Strategy] Select '" + _StrategyName + "'," + IdSubPortfolio + " ;") > 0)
            {
                MessageBox.Show("Inserted!", "Sucess", MessageBoxButtons.OK);
                return;
            }

            MessageBox.Show("Not Inserted!", "Error", MessageBoxButtons.OK);

        }
        private void InsertSubStrategy()
        {
            string _SubStrategyName = txtSubStrategy.Text;

            if (IdStrategy == 0)
            {
                MessageBox.Show("Select Strategy", "Error", MessageBoxButtons.OK);
                return;
            }

            if (_SubStrategyName == SubStrategyText || _SubStrategyName == "")
            {
                MessageBox.Show("Insert New SubStrategy name", "Error", MessageBoxButtons.OK);
                return;
            }

            if (curConn.Return_Int("SELECT COUNT(*) FROM [NESTDB].[dbo].[Tb403_Sub_Strategy] WHERE Sub_Strategy = '" + _SubStrategyName + "'") > 0)
            {
                MessageBox.Show("SubStrategy name already in use", "Error", MessageBoxButtons.OK);
                return;
            }

            if (curConn.ExecuteNonQuery("INSERT [NESTDB].[dbo].[Tb403_Sub_Strategy] SELECT '" + _SubStrategyName + "', " + IdStrategy) > 0)
            {
                MessageBox.Show("Inserted!", "Sucess", MessageBoxButtons.OK);
                return;
            }

            MessageBox.Show("Not Inserted!", "Error", MessageBoxButtons.OK);

        }

        private void btnInsertSubPort_Click(object sender, EventArgs e)
        {
            InsertSubPortfolio();
            txtSubPort.Text = SubPortText;
            LoadSubPortfolio(true);
        }
        private void btnInsertStrategy_Click(object sender, EventArgs e)
        {
            InsertStrategy();
            txtStrategy.Text = StrategyText;
            LoadStrategy(true);
        }
        private void btnInsertSubStrategy_Click(object sender, EventArgs e)
        {
            InsertSubStrategy();
            txtSubStrategy.Text = SubStrategyText;
            LoadSubEstrategy((int)cmbStrategy.SelectedValue);
        }

        private void cmbSubPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cmbSubPorts.SelectedValue.ToString(), out IdSubPortfolio);
            LoadStrategy(true);
        }
        private void cmbStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
            int.TryParse(cmbStrategy.SelectedValue.ToString(), out IdStrategy);
        }

        private void ListSubPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadStrategy(false);
        }
        private void ListStrategy_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSubEstrategy();
        }

        private void btnEditBooks_Click(object sender, EventArgs e)
        {
            frmBook book = new frmBook();
            book.Show();
        }
        private void btnEditSections_Click(object sender, EventArgs e)
        {
            frmSection section = new frmSection();
            section.Show();
        }

        private void txtSubPort_Leave(object sender, EventArgs e)
        {
            if (txtSubPort.Text == "" || txtSubPort.Text == SubPortText)
                txtSubPort.Text = SubPortText;
        }
        private void txtStrategy_Leave(object sender, EventArgs e)
        {
            if (txtStrategy.Text == "" || txtStrategy.Text == StrategyText)
                txtStrategy.Text = StrategyText;
        }
        private void txtSubStrategy_Leave(object sender, EventArgs e)
        {
            if (txtSubStrategy.Text == "" || txtSubStrategy.Text == SubStrategyText)
                txtSubStrategy.Text = SubStrategyText;
        }

        private void txtSubPort_Click(object sender, EventArgs e)
        {
            if (txtSubPort.Text == SubPortText)
                txtSubPort.Text = "";
        }
        private void txtStrategy_Click(object sender, EventArgs e)
        {
            if (txtStrategy.Text == StrategyText)
                txtStrategy.Text = "";
        }
        private void txtSubStrategy_Click(object sender, EventArgs e)
        {
            if (txtSubStrategy.Text == SubStrategyText)
                txtSubStrategy.Text = "";
        }
    }
}