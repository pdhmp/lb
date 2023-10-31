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
    public partial class frmSwap : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();

        public frmSwap()
        {
            InitializeComponent();
        }

        private void frmSwap_Load(object sender, EventArgs e)
        {
            CargaDados.carregacombo(this.cmbIssuer, "Select Id_Instituicao,Nome from Tb000_Instituicoes", "Id_Instituicao", "Nome", 99);

            CargaDados.carregacombo(this.cmbCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 99);

            CargaDados.carregacombo(this.cmbPrimaryEx, "Select Id_Mercado, Cod_Mercado from Tb107_Mercados", "Id_Mercado", "Cod_Mercado", 99);

            CargaDados.carregacombo(this.cmbObjectTicker, "Select Id_Ativo,Simbolo from Tb001_Ativos where Id_Tipo_Ativo in(1)", "Id_Ativo", "Simbolo", 99);

            CargaDados.carregacombo(this.cmbObjectPassive, "Select Id_Ativo,Simbolo from Tb001_Ativos where Id_Tipo_Ativo in(1)", "Id_Ativo", "Simbolo", 99);
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string StringSQL;

            if (txtName.Text != "" && txtTicker.Text != "" && txtLotSize.Text != "" && txtRoundLotSize.Text != "")
            {
                StringSQL = "INSERT INTO Tb001_Ativos (Id_Instituicao,Nome,Simbolo,Id_Tipo_Ativo,Lote_Padrao,Lote_Negociacao," +
                " Fator,Moeda,Bolsa_Primaria,Descricao,Ativo_Objeto,Vencimento,Passivo_Objeto,Taxa_Ativo,Taxa_Passivo) " +
                " VALUES (" + cmbIssuer.SelectedValue.ToString() + ", '" + txtName.Text + "', '" + txtTicker.Text + 
                "',1," + txtLotSize.Text + "," + txtRoundLotSize.Text + "," + txtFactor.Text + "," + cmbCurrency.SelectedValue.ToString() + 
                "," + cmbPrimaryEx.SelectedValue.ToString() + ", '" + txtDescript.Text + "'," +  cmbObjectTicker.SelectedValue.ToString() +
                "," + txtExpiration.Text + "," + cmbObjectPassive.SelectedValue.ToString() + "," + txtTickerTax.Text + "," + txtPassiveTax.Text  + ")";
                int retorno = CargaDados.DB.Execute_Insert_Delete_Update(StringSQL);

                if (retorno == 0)
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

        void Clear_Fields()
        {
            txtName.Text = "";
            txtTicker.Text = "";
            txtLotSize.Text = "";
            txtRoundLotSize.Text = "";
            txtFactor.Text = "";
            txtDescript.Text = "";
            cmbIssuer.SelectedValue = 673;
            cmbCurrency.SelectedValue = 900;
            cmbPrimaryEx.SelectedValue = 0;
            cmbObjectTicker.SelectedValue = 0;
            txtExpiration.Text = "";
            cmbObjectPassive.SelectedValue = 0;
            txtPassiveTax.Text = "";
            txtTickerTax.Text = "";

        }

        private void cmdClear_Click(object sender, EventArgs e)
        {
            Clear_Fields();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}