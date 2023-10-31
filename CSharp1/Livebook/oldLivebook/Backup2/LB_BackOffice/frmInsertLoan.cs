using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using SGN.Business;
using SGN.Validacao;

namespace SGN
{
    public partial class frmInsertLoan : LBForm
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();

        public frmInsertLoan()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmInsertLoan_Load(object sender, EventArgs e)
        {
            if (cmbFundo.Items.Count == 0)
            {
                Carrega_Combo();
                this.dtpVencto.Value = Convert.ToDateTime(CargaDados.curConn.Execute_Query_String("SELECT CONVERT(varchar, dbo.FCN_NDATEADD('du', 1, DATEADD(d, 29, getdate()), 2, 900), 102)"));
            }
            

        }

        public void Carrega_Combo()
        {
            CargaDados.carregacombo(cmbFundo, "SELECT Id_Portfolio, Port_Name FROM dbo.Tb002_Portfolios WHERE Discountinued=0 AND Id_Portfolio<>48", "Id_Portfolio", "Port_Name");
            CargaDados.carregacombo(cmbTicker, "SELECT IdSecurity, NestTicker FROM Tb001_Securities WHERE IdPriceTable=1 AND ((Expiration IS NULL) OR Expiration = '19000101' OR (Expiration >= (CONVERT(VARCHAR, GETDATE()-5, 112))) ) order by NestTicker", "IdSecurity", "NestTicker");
            Carrega_Account();
            dtpIni.Value = DateTime.Now;
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            InsetLoan();
        }

        public void InsetLoan()
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
            if (cmbTicker.SelectedValue.ToString() == "-1")
            {
                MessageBox.Show("Select Ticker!");
                retorno = false;
            }
            if(rdTom.Checked == false && rdDom.Checked == false )
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

            SQLString = "INSERT INTO Tb710_Stock_Loans(CBLC_Id,Transaction_Type,Id_Account,Id_Ticker, Trade_Date,Loan_Maturity,Loan_Rate,Quantity,Id_Side,Id_Type,Status, Related_Loan_Id) " +
                        " VALUES (" + CBLCID + ", " + LoanType + ", " + cmbCorretora.SelectedValue.ToString() + "," + cmbTicker.SelectedValue.ToString() + 
                         ",'" + dtpIni.Value.ToString("yyyyMMdd") + "','" + dtpVencto.Value.ToString("yyyyMMdd") +"'," +
                         Get_Value(txtTaxa.Text) + "/100.00 ," + Get_Value(txtQtd.Text) + "," + Side + "," + Tipo + ",1," + RelatedID + ")";

            retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,1);
            return retorno;

        }
        int Get_Type()
        {
            int Retorno;
            Retorno = 0;
            if (rdRev.Checked == true) { Retorno =  1; }
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
                valor = Convert.ToInt32(cmbFundo.SelectedValue);
                //SubEstrategia
                CargaDados.carregacombo(this.cmbCorretora, "Select Id_Account,Nome from VW_Account_Broker Where Id_Portfolio = " + valor + " order by Show_Prefered desc,Nome asc", "Id_Account", "Nome", 99);
            }
            catch(Exception e)
            {
                Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }
        }
    }
}