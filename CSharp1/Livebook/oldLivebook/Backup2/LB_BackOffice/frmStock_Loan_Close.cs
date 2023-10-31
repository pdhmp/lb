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
    public partial class frmStock_Loan_Close : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();

        public frmStock_Loan_Close()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmStock_Loan_Close_Load(object sender, EventArgs e)
        {
            
            if (Convert.ToSingle(txtQtd.Text.Replace(",",".")) == 0)
            {
                rdFullClose.Checked = true;
                txtQtd.Text = txtFullQtd.Text;
            }
            string SQLString = "Select dbo.FCN_NDATEADD('du',-1,'" + DateTime.Now.ToString("yyyyMMdd") + "',1,1)";

            DateTime Data = Convert.ToDateTime(CargaDados.curConn.Execute_Query_String(SQLString));

            dtpIni.Value = Data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckBox_data())
            {
                if (Close_Stock_Loan() != 0)
                {
                    //MessageBox.Show("Inserted!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error on inserting loan close!");
                }
            }
        }

        bool CheckBox_data()
        {
           
            bool retorno = true;
            if (rdPartialClose.Checked == true && txtQtd.Text == "")
            {
                MessageBox.Show("Quantity field is empty!");
                retorno = false;
            }
            if (rdPartialClose.Checked == true && Get_Value(txtQtd.Text) == Get_Value(txtFullQtd.Text))
            {
                MessageBox.Show("Cannot Partial Close full quantity!");
                retorno = false;
            }
            if (rdPartialClose.Checked == true && Convert.ToInt32(Get_Value(txtQtd.Text)) > Convert.ToInt32(Get_Value(txtFullQtd.Text)))
            {
                MessageBox.Show("Cannot Partial Close more shares than there are in the contract!");
                retorno = false;
            }
            return retorno;
        }

        int Close_Stock_Loan()
        {
            string SQLString;
            int retorno;

            string RelatedID;
            int TransType=0;

            if (txtRelID.Text != "")
            {
                RelatedID = "'" + txtRelID.Text + "'";
                if (rdFullClose.Checked == true) { TransType = 4; };
                if (rdPartialClose.Checked == true) { TransType = 3; };

                SQLString = "INSERT INTO dbo.Tb710_Stock_Loans_Early_Close(Loan_ID, Transaction_Type, Close_Date, Close_Quantity) " +
                             "VALUES (" + RelatedID + ",'" + TransType + "','" + dtpIni.Value.ToString("yyyyMMdd") + "', " + Get_Value(txtQtd.Text) + ");";
                retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,1);
            }
            else
            {
                retorno = 0;
            }
            
            return retorno;

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

        private void rdFullClose_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFullClose.Checked == true)
            {
                txtQtd.Enabled = false;
                txtQtd.Text = txtFullQtd.Text;
            }
        }

        private void rdPartialClose_CheckedChanged(object sender, EventArgs e)
        {
            txtQtd.Enabled = true;
        }
    }
}