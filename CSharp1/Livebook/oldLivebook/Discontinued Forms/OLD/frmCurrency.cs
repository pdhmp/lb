using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using SGN.Business;

namespace SGN
{
    public partial class frmCurrency : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();

        
        public frmCurrency()
        {
            InitializeComponent();
        }

        private void frmCurrency_Load(object sender, EventArgs e)
        {
            CargaDados.carregacombo(this.cmbIssuer, "Select Id_Instituicao,Nome from Tb000_Instituicoes", "Id_Instituicao", "Nome", 99);

        }

        private void cmdIssuer_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        void Clear_Fields()
        {
            txtName.Text = "";
            txtTicker.Text = "";
            txtDescript.Text = "";
            txtBlbTicker.Text = "";
            txtReutersTicker.Text = "";
            cmbIssuer.SelectedValue = 673;
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString;

            if (txtName.Text != "" && txtTicker.Text != "")
            {
                SQLString = "INSERT INTO Tb001_Ativos (Id_Instituicao,Nome,Simbolo,Id_Price_Table,Descricao,Id_Instrumento,Id_Classe_Ativo) " +
                " VALUES (" + cmbIssuer.SelectedValue.ToString() + ", '" + txtName.Text + "', '" + txtTicker.Text + "',3," +
                ", '" + txtDescript.Text + "',9,3)"
                + "sELECT @@IDENTITY";
                string retorno = CargaDados.curConn.Execute_Query_String(SQLString);
                if (retorno == "")
                {
                    MessageBox.Show("Error in Insert. Verify Datas!");

                }
                else
                {
                    SQLString = "Insert into Tb119_Convert_Simbolos (Id_Ativo,Cod_Reuters,Cod_BBL,Cod_Imagine) values " +
                    "(" + retorno + ",'" + txtReutersTicker.Text + "'," + "'" + txtBlbTicker.Text + "','" + txtReutersTicker.Text + "')";
                    int retorno2 = CargaDados.curConn.ExecuteNonQuery(SQLString);

                    if (retorno2 == 0)
                    {
                        MessageBox.Show("Error in Insert. Verify Datas!");

                    }
                    else
                    {
                        MessageBox.Show("Inserted!");
                        Clear_Fields();
                        //Negocios.Busca_Dados_Historicos(Convert.ToInt32(retorno), "19000101");

                    }
                }
            }

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            Clear_Fields();
        }

    }
}