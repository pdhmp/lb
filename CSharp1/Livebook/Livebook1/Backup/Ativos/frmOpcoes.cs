using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.CargaDados;
using SGN.Validacao;
using SGN.Business;
namespace SGN
{
    public partial class frmOpcoes : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();

        Valida Valida = new Valida();

        public frmOpcoes()
        {
            InitializeComponent();
        }

        private void frmOpcoes_Load(object sender, EventArgs e)
        {
            CargaDados.carregacombo(this.cmbIssuer, "Select Id_Instituicao,Nome from Tb000_Instituicoes", "Id_Instituicao", "Nome", 99);

            CargaDados.carregacombo(this.cmbCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 99);

            CargaDados.carregacombo(this.cmbCurrenPrize, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 99);

            CargaDados.carregacombo(this.cmbStrikeCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 99);

            CargaDados.carregacombo(this.cmbPrimaryEx, "Select Id_Mercado, Cod_Mercado from Tb107_Mercados", "Id_Mercado", "Cod_Mercado", 99);

            CargaDados.carregacombo(this.cmbObjectTicker, "Select Id_Ativo,Simbolo from Tb001_Ativos where Id_Tipo_Ativo in(1,2,3,4,5,6)", "Id_Ativo", "Simbolo", 99);

            CargaDados.carregacombo(this.cmbInstrument, "Select Id_Instrumento,Instrumento from Tb029_Instrumentos", "Id_Instrumento", "Instrumento", 0);

            CargaDados.carregacombo(this.cmbAssetClass, "Select Id_Classe_Ativo,Classe_Ativo  from Tb028_Classe_Ativo ", "Id_Classe_Ativo", "Classe_Ativo", 0);
            CargaDados.carregacombo(this.cmbFonteUp, "Select Id_Sistemas_Informacoes,Descricao from Tb102_Sistemas_Informacoes order by ordem ", "Id_Sistemas_Informacoes", "Descricao", 0);

            CargaDados.carregacombo(this.cmbUpFreq, "Select Id_Frequencia,Frequencia  from Tb105_Frequencia_Atualizacao ", "Id_Frequencia", "Frequencia", 0);
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            Clear_Fields();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string StringSQL;
            DateTime data_ex = Convert.ToDateTime(dtpExpiration.Value.ToString(""));
            //string Data_ex2 = data_ex.ToString("")
            int Tipo_C_P;
            
            if (txtName.Text != "" && txtTicker.Text != "" && txtLotSize.Text != "" && txtRoundLotSize.Text != "" && (rdCall.Checked || rdPut.Checked ))
            {
                if (rdCall.Checked == true)
                {
                    Tipo_C_P = 1;
                }
                else
                {
                    Tipo_C_P = 0;
                }

                StringSQL = "INSERT INTO Tb001_Ativos (Id_Instituicao,Nome,Simbolo,Id_Tipo_Ativo,Lote_Padrao,Lote_Negociacao," +
                " Moeda,Bolsa_Primaria,Descricao,Ativo_Objeto,Vencimento,Moeda_Premio,Preco_Exercicio,Moeda_Strike,Tipo_C_V,Id_Instrumento,Id_Classe_Ativo,Frequencia_Atualizacao,Fonte_Atualizacao) " +
                " VALUES (" + cmbIssuer.SelectedValue.ToString() + ", '" + txtName.Text + "', '" + txtTicker.Text + "',7," + 
                txtLotSize.Text.Replace(",",".") + "," + txtRoundLotSize.Text + "," + cmbCurrency.SelectedValue.ToString() + 
                "," + cmbPrimaryEx.SelectedValue.ToString() + ",'" + txtDescript.Text + "'," + cmbObjectTicker.SelectedValue.ToString() +
                ",'" + data_ex.ToString("yyyyMMdd") + "', " + cmbCurrenPrize.SelectedValue.ToString() + "," + txtExercicePrices.Text + "," + cmbStrikeCurrency.SelectedValue.ToString() + "," + Tipo_C_P +
                "," + cmbInstrument.SelectedValue.ToString() + "," + cmbAssetClass.SelectedValue.ToString() +
                "," + cmbUpFreq.SelectedValue.ToString() + "," + cmbFonteUp.SelectedValue.ToString() + ")"
                + "sELECT @@IDENTITY";
                
                string retorno = CargaDados.DB.Execute_Query_String(StringSQL);

                if (retorno == "")
                {
                    MessageBox.Show("Error in Insert. Verify Datas!");

                }
                else
                {
                    string Cod_Imagine = txtReutersTicker.Text.ToString() + "-" + Valida.Right(dtpExpiration.Value.Year.ToString(),2);

                    StringSQL = "Insert into Tb119_Convert_Simbolos (Id_Ativo,Cod_Reuters,Cod_BBL,Cod_Imagine) values " +
                    "(" + retorno + ",'" + txtReutersTicker.Text + "'," + "'" + txtBlbTicker.Text + "','" +  Cod_Imagine + "')";
                    int retorno2 = CargaDados.DB.Execute_Insert_Delete_Update(StringSQL);

                    if (retorno2 == 0)
                    {
                        MessageBox.Show("Error in Insert. Verify Datas!");

                    }
                    else
                    {
                        MessageBox.Show("Inserted!");
                        Clear_Fields();
                        Negocios.Inserir_Pendencia_Precos(Convert.ToInt32(retorno));

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
            cmbIssuer.SelectedValue = 673;
            cmbCurrency.SelectedValue = 900;
            cmbPrimaryEx.SelectedValue = 0;
            cmbObjectTicker.SelectedValue = 0;
            dtpExpiration.Text = "";
            cmbCurrenPrize.SelectedValue = 900;
            txtExercicePrices.Text = "";
            cmbStrikeCurrency.SelectedValue = 900;
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

    }
}