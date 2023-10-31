using System;
using System.Data;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmBrokers : LBForm
    {
        public frmBrokers()
        {
            InitializeComponent();
        }

        private void frmBrokers_Load(object sender, EventArgs e)
        {
            LoadCombos();
        }

        void LoadCombos()
        {
            LiveDLL.FormUtils.LoadCombo(this.cmbd_Id_praca, "Select Id_Geografia,Nome from Tb100_Geografia order by ordem", "Id_Geografia", "Nome", 99);
        }

        public void Carrega_Lista()
        {

            string strWhere = "";
            string[] strFields = new string[1];

            string SQLString = "Select IdBroker,BrokerName FROM dbo.Brokers A WHERE IdBroker>0";

            if (txtsearch.Text.Trim() != "")
            {
                strFields = txtsearch.Text.ToString().Split(' ');
                for (int f = 0; f < strFields.Length; f++)
                {
                    if (strFields[f] != "")
                    {
                        strWhere = strWhere + " AND (A.BrokerName like '%" + strFields[f] + "%'" +
                        " OR A.Broker_Ticker Like '%" + strFields[f] + "%'" +
                        " OR MellonExpName Like '%" + strFields[f] + "%' OR ImportName Like '%" + strFields[f] + "%'" +
                        " OR convert(varchar,IdBroker)='" + strFields[f] + "')";
                    }
                }
            }

            SQLString = SQLString + strWhere + " order by IdBroker,BrokerName";
            using (newNestConn curConn = new newNestConn())
            {
                DataTable table = curConn.Return_DataTable(SQLString);

                lstBrokers.DataSource = table;
                lstBrokers.ValueMember = "IdBroker";
                lstBrokers.DisplayMember = "BrokerName";
                //lstAtivos.SelectedIndexChanged(null, null);
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch.Text.ToString().Length > 2) { Carrega_Lista(); };
        }

        private void lstBrokers_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdBroker=0;
            if (Int32.TryParse(lstBrokers.SelectedValue.ToString(), out IdBroker) != false)
            {
                IdBroker = Convert.ToInt32(lstBrokers.SelectedValue.ToString());
            }

            if (IdBroker != 0)
            {
                Carrega_Broker(IdBroker);
            }
        }

        void Carrega_Broker(int IdBroker)
        {
            string SQLString = "SELECT * FROM Tb011_Corretoras (nolock)  A LEFT JOIN NESTDB.dbo.Tb100_Geografia B (nolock) ON A.Id_praca=B.id_geografia WHERE Id_Corretora = " + IdBroker;

            LoadGroup(groupDescription, SQLString);
            ReloadlstAvDates();
        }

        private void LoadGroup(GroupBox curGroup, string SQLString)
        {
            Console.WriteLine(curGroup.Text.ToString() + " -  " + curGroup.Name.ToString());

            using (newNestConn curConn = new newNestConn())
            {
                DataTable curTable = curConn.Return_DataTable(SQLString);
                DataRow curRow = curTable.Rows[0];

                foreach (object curControl in curGroup.Controls)
                {
                    if (curControl.GetType() == typeof(System.Windows.Forms.ComboBox))
                    {
                        ComboBox curCombo = (ComboBox)curControl;
                        string curFieldName = curCombo.Name.Substring(5);

                        if (curCombo.Name.Substring(0, 5) == "cmbd_")
                        {
                            if (curRow[curFieldName].ToString() != "")
                            {
                                curCombo.SelectedValue = Convert.ToInt32(curRow[curFieldName]);
                            }
                            else
                            {
                                curCombo.SelectedValue = 0;
                            }
                        }
                    }

                    if (curControl.GetType() == typeof(System.Windows.Forms.TextBox))
                    {
                        TextBox curTextBox = (TextBox)curControl;
                        string curFieldName = curTextBox.Name.Substring(5);

                        if (curTextBox.Name.Substring(0, 5) == "txtd_")
                        {
                            curTextBox.Text = curRow[curFieldName].ToString();
                        }
                    }

                    if (curControl.GetType() == typeof(System.Windows.Forms.Label))
                    {
                        Label curLabel = (Label)curControl;
                        string curFieldName = curLabel.Name.Substring(5);

                        if (curLabel.Name.Substring(0, 5) == "lbld_")
                        {
                            curLabel.Text = curRow[curFieldName].ToString();
                        }
                    }

                    if (curControl.GetType() == typeof(System.Windows.Forms.DateTimePicker))
                    {
                        DateTimePicker curDateTimePicker = (DateTimePicker)curControl;
                        string curFieldName = curDateTimePicker.Name.Substring(5);

                        if (curDateTimePicker.Name.Substring(0, 5) == "dtpd_")
                        {
                            curDateTimePicker.Value = DateTime.Parse(curRow[curFieldName].ToString());
                        }
                    }

                    if (curControl.GetType() == typeof(System.Windows.Forms.CheckBox))
                    {
                        CheckBox curCheckBox = (CheckBox)curControl;
                        string curFieldName = curCheckBox.Name.Substring(5);

                        if (curCheckBox.Name.Substring(0, 5) == "chkd_")
                        {
                            if (double.Parse(curRow[curFieldName].ToString()) == 1)
                                curCheckBox.Checked = true;
                            else
                                curCheckBox.Checked = false;
                        }
                    }
                }

            }
        }

        void ReloadlstAvDates()
        {
            using (newNestConn curConn = new newNestConn())
            {
                this.lstAvDates.SelectedIndexChanged -= new System.EventHandler(this.lstAvDates_SelectedIndexChanged);
                lstAvDates.SelectedIndex = -1;
                LiveDLL.FormUtils.LoadList(lstAvDates, "SELECT ValidAsOfDate FROM NESTDB.dbo.Tb012_Brokers_Variable (nolock) WHERE IdBroker=" + lbld_Id_Corretora.Text, "ValidAsOfDate", "ValidAsOfDate");
                this.lstAvDates.SelectedIndexChanged += new System.EventHandler(this.lstAvDates_SelectedIndexChanged);
                lstAvDates.SelectedIndex = lstAvDates.Items.Count - 1;
                lstAvDates_SelectedIndexChanged(this, new EventArgs());
            }
        }

        private void lstAvDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SQLString = "SELECT * FROM NESTDB.dbo.Tb012_Brokers_Variable (nolock) WHERE IdBroker=" + lbld_Id_Corretora.Text + " AND ValidAsOfDate='" + ((DateTime)lstAvDates.SelectedValue).ToString("yyyy-MM-dd") + "'";
            LoadGroup(groupVariableInfo, SQLString);
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            int retorno = UpdateData();

            if (retorno == 0)
            {
                MessageBox.Show("Error on update. Check Data.");
            }
            else
            {
                MessageBox.Show("Updated.");
            }

        }

        int UpdateData()
        {
            string StringSQL;
            int retorno;

            string IdBovespa = txtd_Id_BOVESPA.Text.ToString().Replace(".", "").Replace(",", ".");
            string IdBMF = txtd_Id_BMF.Text.ToString().Replace(".", "").Replace(",", ".");
            string IdCBLC = txtd_Id_CBLC.Text.ToString().Replace(".", "").Replace(",", ".");

            if (IdBovespa == "") { IdBovespa = "0";}
            if (IdBMF == "") { IdBMF = "0";}
            if (IdCBLC == "") { IdCBLC = "0"; }

            StringSQL = "UPDATE [NESTDB].[dbo].[Tb011_Corretoras] " +
                        "   SET [Id_praca] = " + cmbd_Id_praca.SelectedValue.ToString() +
                        "      ,[Id_BOVESPA] = " + IdBovespa +
                        "      ,[Id_BMF] = " + IdBMF +
                        "      ,[Id_CBLC] = " + IdCBLC +
                        "      ,[Broker_Ticker] = '" + txtd_Broker_Ticker.Text.ToString() + "'" +
                        "      ,[MellonExpName] ='" + txtd_MellonExpName.Text.ToString() + "'" +
                        "      ,[ImportName] = '" + txtd_ImportName.Text.ToString() + "'" + 
                        " WHERE Id_Corretora=" + lbld_Id_Corretora.Text;


            using (newNestConn curConn = new newNestConn())
            {
               retorno = curConn.ExecuteNonQuery(StringSQL,1);
            }

            string DefaultRebate = txtd_DefaultRebate.Text.ToString().Replace(".", "").Replace(",", ".");
            string RebateAfterCAP = txtd_RebateAfterCAP.Text.ToString().Replace(".", "").Replace(",", ".");
            string CapValue = txtd_CAPValue.Text.ToString().Replace(".", "").Replace(",", ".");

            if (DefaultRebate == "") { DefaultRebate = "0"; }
            if (RebateAfterCAP == "") { RebateAfterCAP = "0"; }
            if (CapValue == "") { CapValue = "0"; }

            StringSQL = "UPDATE [NESTDB].[dbo].[Tb012_Brokers_Variable] " +
                        " SET [DefaultRebate] = " + DefaultRebate +
                        " ,[RebateAfterCAP] = " + RebateAfterCAP +
                        " ,[CAPValue] = " + CapValue +
                        " WHERE [IdBroker] = " + lbld_Id_Corretora.Text + " AND [ValidAsOfDate] ='" + Convert.ToDateTime(lstAvDates.SelectedValue.ToString()).ToString("yyyy-MM-dd") + "'"; 

            using (newNestConn curConn = new newNestConn())
            {
                retorno = curConn.ExecuteNonQuery(StringSQL,1);
            }

            return retorno;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtd_CAPValue_Leave(object sender, EventArgs e)
        {
            decimal CAPValue = Convert.ToDecimal(txtd_CAPValue.Text);
            this.txtd_CAPValue.Text = CAPValue.ToString("##,##0.00#######");
        }

        private void label2_Click(object sender, EventArgs e)
        {
            frmAddSecurityDate Data = new frmAddSecurityDate();
            Data.ShowDialog();

            DateTime TradeDate = Convert.ToDateTime(Data.Tag);
            int retorno;
            string DefaultRebate = txtd_DefaultRebate.Text.ToString().Replace(".", "").Replace(",", ".");
            string RebateAfterCAP = txtd_RebateAfterCAP.Text.ToString().Replace(".", "").Replace(",", ".");
            string CapValue = txtd_CAPValue.Text.ToString().Replace(".", "").Replace(",", ".");

            if (DefaultRebate == "") { DefaultRebate = "0"; }
            if (RebateAfterCAP == "") { RebateAfterCAP = "0"; }
            if (CapValue == "") { CapValue = "0"; }

            string StringSQL = "INSERT INTO [NESTDB].[dbo].[Tb012_Brokers_Variable]" +
            " ([IdBroker],[ValidAsOfDate],[DefaultRebate],[RebateAfterCAP],[CAPValue])" +
            " VALUES (" + lbld_Id_Corretora.Text + ",'" + TradeDate.ToString("yyyyMMdd") + "'," + DefaultRebate + "," + RebateAfterCAP + "," + CapValue + ")";

            using (newNestConn curConn = new newNestConn())
            {
                retorno = curConn.ExecuteNonQuery(StringSQL,1);
            }
            ReloadlstAvDates();

        }
    }
}
