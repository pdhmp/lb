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
    public partial class frmAcoes : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        
        public frmAcoes()
        {
            InitializeComponent();
        }

        private void frmAcoes_Load(object sender, EventArgs e)
        {
            CargaDados.carregacombo(this.cmbIssuer, "Select Id_Instituicao,Nome from Tb000_Instituicoes", "Id_Instituicao", "Nome", 99);

            CargaDados.carregacombo(this.cmbCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 99);

            CargaDados.carregacombo(this.cmbPrimaryEx, "Select Id_Mercado, Cod_Mercado from Tb107_Mercados", "Id_Mercado", "Cod_Mercado", 99);
            CargaDados.carregacombo(this.cmbInstrument, "Select Id_Instrumento,Instrumento from Tb029_Instrumentos", "Id_Instrumento", "Instrumento", 0);

            CargaDados.carregacombo(this.cmbAssetClass, "Select Id_Classe_Ativo,Classe_Ativo  from Tb028_Classe_Ativo ", "Id_Classe_Ativo", "Classe_Ativo", 0);

            CargaDados.carregacombo(this.cmbFonteUp, "Select Id_Sistemas_Informacoes,Descricao from Tb102_Sistemas_Informacoes order by ordem ", "Id_Sistemas_Informacoes", "Descricao", 0);

            CargaDados.carregacombo(this.cmbUpFreq, "Select Id_Frequencia,Frequencia  from Tb105_Frequencia_Atualizacao ", "Id_Frequencia", "Frequencia", 0);
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString;

            if (txtName.Text != "" && txtTicker.Text != "" && txtLotSize.Text != "" && txtRoundLotSize.Text != "")
            {
                SQLString = "INSERT INTO Tb001_Ativos (Id_Instituicao,Nome,Simbolo,Id_Price_Table,Lote_Padrao,Lote_Negociacao,Moeda,Bolsa_Primaria,Descricao,Id_Instrumento,Id_Classe_Ativo,Frequencia_Atualizacao,Fonte_Atualizacao) " +
                " VALUES (" + cmbIssuer.SelectedValue.ToString() + ", '" + txtName.Text + "', '" + txtTicker.Text + "',1," + txtLotSize.Text + "," + txtRoundLotSize.Text +
                "," + cmbCurrency.SelectedValue.ToString() + "," + cmbPrimaryEx.SelectedValue.ToString() + ", '" + txtDescript.Text + "'," + cmbInstrument.SelectedValue.ToString() + "," + cmbAssetClass.SelectedValue.ToString() +
                "," + cmbUpFreq.SelectedValue.ToString() + "," + cmbFonteUp.SelectedValue.ToString() + ")"
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
        void Clear_Fields()
        {
            txtName.Text = "";
            txtTicker.Text = "";
            txtLotSize.Text = "";
            txtRoundLotSize.Text = "";
            txtDescript.Text = "";
            txtBlbTicker.Text = "";
            txtReutersTicker.Text = "";
            cmbIssuer.SelectedValue = 673;
            cmbCurrency.SelectedValue = 900;
            cmbPrimaryEx.SelectedValue = 0;
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            Clear_Fields();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }


    }
}