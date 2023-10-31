using System;
using System.Windows.Forms;
using LiveDLL;


namespace LiveBook
{
    public partial class frmInsertLoan : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmInsertLoan()
        {
            InitializeComponent();
            cmbFundo.SelectedIndexChanged += new System.EventHandler(this.cmbFundo_SelectedIndexChanged);

            Carrega_Combo();
            this.dtpVencto.Value = Convert.ToDateTime(curConn.Execute_Query_String("SELECT CONVERT(varchar, dbo.FCN_NDATEADD('du', 1, DATEADD(d, 29, getdate()), 2, 900), 102)"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInsertLoan_Load(object sender, EventArgs e)
        {

        }

        public void Carrega_Combo()
        {
            LiveDLL.FormUtils.LoadCombo(cmbFundo, "SELECT Id_Portfolio, Port_Name FROM dbo.Tb002_Portfolios (nolock)  WHERE Id_Port_Type=2 AND Discountinued=0 AND Id_Portfolio<>48", "Id_Portfolio", "Port_Name");
            LiveDLL.FormUtils.LoadCombo(cmbTicker, "SELECT IdSecurity, NestTicker FROM Tb001_Securities (nolock) WHERE IdPriceTable=1 AND ((Expiration IS NULL) OR Expiration = '19000101' OR (Expiration >= (CONVERT(VARCHAR, GETDATE()-5, 112))) ) order by NestTicker", "IdSecurity", "NestTicker");
            Carrega_Account();
            dtpIni.Value = DateTime.Now;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            InsetLoan();
        }

        public void InsetLoan()
        {
            if (txtRelID.Text == "")
            {
                if (CheckBox_data())
                {
                    if (InsertStock_Loan() != 99)
                    {
                        if (this.Text != "Insert Stock Loan")
                        {
                            this.Close();
                        }
                        else
                        {
                            this.txtQtd.Text = "";
                            this.txtTaxa.Text = "";
                            this.txtRelID.Text = "";
                            this.txtCBLCId.Text = "";
                        }

                        MessageBox.Show("Inserted");
                    }
                    else
                    {
                        MessageBox.Show("Error on Insert!\n\nA possible cause is that there is another contract with the same contract ID.", "Loan Insert Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Incorect Data!");
                }
            }
            else
            {
                if (CheckBox_data())
                {
                    string SQLString;
                    int LoanType;
                    int Tipo = Get_Type();
                    int Side = Get_Side();
                    string RelatedID;
                    string CBLCID;

                    if (Tipo == 0) { MessageBox.Show("Select Type of Stock Loan!"); return; }
                    if (Side == 0) { MessageBox.Show("Select Side of Stock Loan!"); return; }
                    if (txtRelID.Text != "") { RelatedID = "'" + txtRelID.Text + "'"; } else { RelatedID = "null"; }
                    if (txtCBLCId.Text != "") { CBLCID = "'" + txtCBLCId.Text + "'"; } else { CBLCID = "null"; }
                    if (this.Text == "Loan Renew") { LoanType = 2; } else { LoanType = 1; }

                    SQLString =
                                   " UPDATE	Tb710_Stock_Loans SET TimeStamp = GETDATE() " +
                                   ", Id_Side = " + Side +
                                   ", Id_Type = " + Tipo +
                                   ", CBLC_Id = " + CBLCID +
                                   ", Transaction_Type = " + LoanType +
                                   ", Id_Account = " + cmbCorretora.SelectedValue.ToString() +
                                   ", id_Ticker = " + cmbTicker.SelectedValue.ToString() +
                                   ", Trade_Date = '" + dtpIni.Value.ToString("yyyyMMdd") + "'" +
                                   ", Loan_Maturity = '" + dtpVencto.Value.ToString("yyyyMMdd") + "'" +
                                   ", Loan_Rate = " + Get_Value(txtTaxa.Text) + "/100.00 " +
                                   ", Quantity = " + Get_Value(txtQtd.Text) +
                                   " WHERE Loan_ID = " + RelatedID;

                    if (curConn.ExecuteNonQuery(SQLString, 1) > 0) MessageBox.Show("Inserted");
                }
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
            if (cmbFundo.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Select Portfolio!");
                retorno = false;
            }
            if (cmbCorretora.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Select Broker!");
                retorno = false;
            }
            if (cmbTicker.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Select Ticker!");
                retorno = false;
            }
            if (rdTom.Checked == false && rdDom.Checked == false)
            {
                MessageBox.Show("Select Operation!");
                retorno = false;
            }
            if (rdRev.Checked == false && rdNormal.Checked == false && RdFixo.Checked == false)
            {
                MessageBox.Show("Select Type!");
                retorno = false;
            }

            return retorno;
        }

        int InsertStock_Loan()
        {
            string SQLString;
            int retorno;
            int LoanType;

            int Tipo = Get_Type();
            if (Tipo == 0)
            {
                MessageBox.Show("Select Type of Stock Loan!");
                return 0;
            }

            int Side = Get_Side();
            if (Side == 0)
            {
                MessageBox.Show("Select Side of Stock Loan!");
                return 0;
            }

            string RelatedID;
            string CBLCID;

            if (txtRelID.Text != "")
            {
                RelatedID = "'" + txtRelID.Text + "'";
            }
            else
            {
                RelatedID = "null";
            }

            if (txtCBLCId.Text != "")
            {
                CBLCID = "'" + txtCBLCId.Text + "'";
            }
            else
            {
                CBLCID = "null";
            }

            if (this.Text == "Loan Renew")
            {
                LoanType = 2;
            }
            else
            {
                LoanType = 1;
            }

            SQLString = "INSERT INTO Tb710_Stock_Loans(CBLC_Id,Transaction_Type,Id_Account,Id_Ticker, Trade_Date,Loan_Maturity,Loan_Rate,Quantity,Id_Side,Id_Type,Status, Related_Loan_Id, TimeStamp) " +
                        " VALUES (" + CBLCID + ", " + LoanType + ", " + cmbCorretora.SelectedValue.ToString() + "," + cmbTicker.SelectedValue.ToString() +
                         ",'" + dtpIni.Value.ToString("yyyyMMdd") + "','" + dtpVencto.Value.ToString("yyyyMMdd") + "'," +
                         Get_Value(txtTaxa.Text) + "/100.00 ," + Get_Value(txtQtd.Text) + "," + Side + "," + Tipo + ",1," + RelatedID + ",getdate())";

            retorno = curConn.ExecuteNonQuery(SQLString, 1);
            return retorno;

        }
        int Get_Type()
        {
            int Retorno;
            Retorno = 0;
            if (rdRev.Checked == true) { Retorno = 1; }
            if (rdNormal.Checked == true) { Retorno = 2; }
            if (RdFixo.Checked == true) { Retorno = 3; }

            return Retorno;

        }

        int Get_Side()
        {
            int Retorno;

            Retorno = 0;
            if (rdDom.Checked == true) { Retorno = 1; }
            if (rdTom.Checked == true) { Retorno = 2; }
            return Retorno;

        }

        string Get_Value(string Valor)
        {
            Valor = Convert.ToDouble(Valor).ToString("#,##0.##");

            return Valor.Replace(".", "").Replace(",", ".");
        }

        private void txtQtd_Leave(object sender, EventArgs e)
        {
            if (txtQtd.Text == "")
                return;

            decimal testa_pos;
            testa_pos = Convert.ToDecimal(this.txtQtd.Text);

            if (this.txtQtd.Text != "")
            {
                decimal qtd = Convert.ToDecimal(this.txtQtd.Text);
                this.txtQtd.Text = qtd.ToString("##,##0.#########");
            }

        }

        private void cmbFundo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFundo.SelectedValue != null)
            {
                Carrega_Account();
            }
        }

        public void Carrega_Account()
        {
            int valor;
            try
            {
                if (cmbFundo.SelectedIndex >= 0)
                {
                    valor = Convert.ToInt32(cmbFundo.SelectedValue);
                    //SubEstrategia
                    LiveDLL.FormUtils.LoadCombo(this.cmbCorretora, "Select Id_Account,Nome from VW_Account_Broker Where Id_Portfolio = " + valor + " order by Show_Prefered desc, Nome asc", "Id_Account", "Nome", 99);
                }
            }
            catch (Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }
        }
    }
}