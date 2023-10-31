using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.CargaDados;
using SGN.Business;

namespace SGN
{
    public partial class frmFuturos : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();

        public frmFuturos()
        {
            InitializeComponent();
        }

         private void frmFuturos_Load(object sender, EventArgs e)
        {
            CargaDados.carregacombo(this.cmbIssuer, "Select Id_Instituicao,Nome from Tb000_Instituicoes", "Id_Instituicao", "Nome", 99);

            CargaDados.carregacombo(this.cmbCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 99);

            CargaDados.carregacombo(this.cmbPrimaryEx, "Select Id_Mercado, Cod_Mercado from Tb107_Mercados", "Id_Mercado", "Cod_Mercado", 99);

            CargaDados.carregacombo(this.cmbObjectTicker, "Select Id_Ativo,Simbolo from Tb001_Ativos where Id_Tipo_Ativo in(1,2,7,4,6)", "Id_Ativo", "Simbolo", 99);
            
             CargaDados.carregacombo(this.cmbInstrument, "Select Id_Instrumento,Instrumento from Tb029_Instrumentos", "Id_Instrumento", "Instrumento", 0);

            CargaDados.carregacombo(this.cmbAssetClass, "Select Id_Classe_Ativo,Classe_Ativo  from Tb028_Classe_Ativo ", "Id_Classe_Ativo", "Classe_Ativo", 0);
            CargaDados.carregacombo(this.cmbFonteUp, "Select Id_Sistemas_Informacoes,Descricao from Tb102_Sistemas_Informacoes order by ordem ", "Id_Sistemas_Informacoes", "Descricao", 0);

            CargaDados.carregacombo(this.cmbUpFreq, "Select Id_Frequencia,Frequencia  from Tb105_Frequencia_Atualizacao ", "Id_Frequencia", "Frequencia", 0);
        }

        private void cmdObjectTicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string StringSQL;
            DateTime data_ex = Convert.ToDateTime(txtExpiration.Value.ToString(""));

            if (txtName.Text != "" && txtTicker.Text != "" && txtLotSize.Text != "" && txtRoundLotSize.Text != "")
            {
                StringSQL = "INSERT INTO Tb001_Ativos (Id_Instituicao,Nome,Simbolo,Id_Tipo_Ativo,Lote_Padrao,Lote_Negociacao,Moeda,Bolsa_Primaria,Descricao,Ativo_Objeto,Vencimento,Id_Instrumento,Id_Classe_Ativo,Frequencia_Atualizacao,Fonte_Atualizacao) " +
                " VALUES (" + cmbIssuer.SelectedValue.ToString() + ", '" + txtName.Text + "', '" + txtTicker.Text + "',5," + txtLotSize.Text + "," + txtRoundLotSize.Text +
                "," + cmbCurrency.SelectedValue.ToString() + "," + cmbPrimaryEx.SelectedValue.ToString() + ", '" + txtDescript.Text + "'," + cmbObjectTicker.SelectedValue.ToString() + ",'" + data_ex.ToString("yyyyMMdd") + "'," + cmbInstrument.SelectedValue.ToString() + "," + cmbAssetClass.SelectedValue.ToString() +
                "," + cmbUpFreq.SelectedValue.ToString() + "," + cmbFonteUp.SelectedValue.ToString() + ")"
                + "sELECT @@IDENTITY";
                string retorno = CargaDados.DB.Execute_Query_String(StringSQL);
                if (retorno == "")
                {
                    MessageBox.Show("Error in Insert. Verify Datas!");
                }
                else
                {
                    StringSQL = "Insert into Tb119_Convert_Simbolos (Id_Ativo,Cod_Reuters,Cod_BBL,Cod_Imagine) values " +
                    "(" + retorno + ",'" + txtReutersTicker.Text + "'," + "'" + txtBlbTicker.Text + "','" + txtImagineTicker.Text + "')";
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
            txtDescript.Text = "";
            cmbIssuer.SelectedValue = 673;
            cmbObjectTicker.SelectedValue = 1;
            txtExpiration.Text = "";
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            Clear_Fields();

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void cmbFonteUp_SelectedIndexChanged(object sender, EventArgs e)
        {

        }   

    }
}