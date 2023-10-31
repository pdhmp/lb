using System;
using System.Windows.Forms;
using LiveDLL;


namespace LiveBook
{
    public partial class frmInsertMargin : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmInsertMargin()        {            InitializeComponent();        }

        private void button2_Click(object sender, EventArgs e)        {            this.Close();        }

        private void frmInsertMargin_Load(object sender, EventArgs e)
        {
            if (cmbFundo.Items.Count == 0)
            {
                Carrega_Combo();
                dtpData.Value = DateTime.Now;
                rdDep.Checked = true;
            }
        }

        public void Carrega_Combo()
        {
            LiveDLL.FormUtils.LoadCombo(cmbFundo, "SELECT Id_Portfolio, Port_Name FROM dbo.Tb002_Portfolios (nolock) WHERE Discountinued=0 AND Id_Portfolio<>48 and Where Id_Port_Type =2 ", "Id_Portfolio", "Port_Name");
            LiveDLL.FormUtils.LoadCombo(cmbTicker, "SELECT IdSecurity, NestTicker FROM Tb001_Securities (nolock) WHERE IdPriceTable=1 AND ((Expiration IS NULL) OR Expiration = '19000101' OR (Expiration >= (CONVERT(VARCHAR, GETDATE()-5, 112))) ) order by NestTicker", "IdSecurity", "NestTicker");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckBox_data())
            {
                InsertMargin();
                if (this.Text == "Update Margin")
                {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Incorect Data!");
            }
        }

        bool CheckBox_data()
        {
                       bool retorno = true;
            if (txtQtd.Text == "")
            {
                MessageBox.Show("Quantity field is empty!");
                retorno = false;
            }
            if(cmbFundo.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Select Portfolio!");
                retorno = false;
            }
            if (cmbCorretora.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Select Broker!");
                retorno = false;
            }
            if (cmbTicker.SelectedValue == null || cmbTicker.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Select Ticker!");
                retorno = false;
            }
            if(rdDep.Checked == false && rdRet.Checked == false )
            {
                MessageBox.Show("Select Operation!");
                retorno = false;
            }
            return retorno;
        }

        int InsertMargin()
        {
            string SQLString;
            int retorno;
            SQLString = "INSERT INTO Tb712_MarginAssets(Id_Account, Trade_Date, Quantity, Id_Ticker, Status) VALUES (" +
                         cmbCorretora.SelectedValue.ToString() + ",'" + dtpData.Value.ToString("yyyyMMdd") + "'," + Get_Amount() + "," + Get_Ticker() + ",1)";

            retorno = curConn.ExecuteNonQuery(SQLString,1);
            if (retorno != 0)
            {
                if (this.Text != "Update Margin")
                {
                    MessageBox.Show("Data Inserted!");
                }
            }
            else 
            {
                MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            cmbTicker.SelectedValue = -1;
            txtQtd.Text = "";
            return retorno;

        }

        string Get_Ticker()
        {
                return cmbTicker.SelectedValue.ToString();
        }

        int Get_Trade_Type()
        {
            if (rdDep.Checked == true) { return 0; }
            else { return 1; }
        }

        string Get_Amount()
        {
            string tempQtdString;
            tempQtdString = Convert.ToDouble(txtQtd.Text).ToString("#,##0.00").Replace(".", "").Replace(",", ".");
            if(rdRet.Checked == true) 
            {
                tempQtdString = "-" + tempQtdString;
            }
            return tempQtdString; 
        }
        string Get_Dinheiro()
        {
            txtQtd.Text = Convert.ToDouble(txtQtd.Text).ToString("#,##0.00");

            return txtQtd.Text.Replace(".", "").Replace(",", "."); 
        }

        private void txtQtd_Leave(object sender, EventArgs e)
        {
            if (this.txtQtd.Text != "")
            {
                decimal qtd = Convert.ToDecimal(this.txtQtd.Text);
                if (qtd < 0)
                {
                    rdRet.Checked = true;
                    qtd = -qtd;
                }
                this.txtQtd.Text = qtd.ToString("##,##0.00");
            }

        }

        private void rdDinheiro_CheckedChanged(object sender, EventArgs e)
        {
            cmbTicker.Visible = false;
            label4.Visible = false;
        }

        private void rdAtivo_CheckedChanged(object sender, EventArgs e)
        {
            cmbTicker.Visible = true;
            label4.Visible = true;
        }

        private void txtQtd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.button1_Click(sender, e);
                cmbTicker.Focus();
            }
            
        }
        public void Carrega_Account()
        {
            int valor;
            try
            {
                valor = Convert.ToInt32(cmbFundo.SelectedValue);
                //SubEstrategia
                LiveDLL.FormUtils.LoadCombo(this.cmbCorretora, "Select Id_Account,Nome from VW_Account_Broker Where Id_Portfolio = " + valor + " order by Show_Prefered desc,Nome asc", "Id_Account", "Nome", 99);
            }
            catch(Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }
        }

        private void cmbFundo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Account();
        }   
    }
}