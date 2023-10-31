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
    public partial class frmInsertPrice : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();


        public frmInsertPrice()
        {
            InitializeComponent();
            CargaDados.carregacombo(this.cmbTicker, "Select IdSecurity, NestTicker from Tb001_Securities order by NestTicker", "IdSecurity", "NestTicker", 99);
            CargaDados.carregacombo(this.cmbPriceType, "Select Id_Tipo_Preco,Preco from Tb116_Tipo_Preco order by Preco", "Id_Tipo_Preco", "Preco", 1);

        }

        public void SetTicker(int Id_Ticker)
        {
            CargaDados.carregacombo(this.cmbTicker, "Select IdSecurity, NestTicker from Tb001_Securities order by NestTicker", "IdSecurity", "NestTicker", Id_Ticker);
        }

        private void frmInsertPrice_Load(object sender, EventArgs e)
        {
            
        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear_Fields();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int IdSecurity;
            string Preco;
            string tipo_preco;
            string Data;

            IdSecurity = Convert.ToInt32(cmbTicker.SelectedValue.ToString());
            tipo_preco = cmbPriceType.SelectedValue.ToString();

            Preco = Convert.ToString(Convert.ToDecimal(txtPrice.Text.ToString()));
            Data = dtpDate.Value.ToString("yyyyMMdd");

            int retorno = Negocios.Insere_Preco(IdSecurity, Preco, tipo_preco, Data);
            if (retorno != 0)
            {
                MessageBox.Show("Inserted Price!");
                Clear_Fields();
            }
            else
            {
                MessageBox.Show("Error on Insert. Verify Datas!");
            }
           // string SQLString; 


        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            decimal Preco;
            if (txtPrice.Text != "")
            {
                Preco = Convert.ToDecimal(txtPrice.Text.ToString());
                if (this.txtPrice.Text != "")
                {
                    this.txtPrice.Text = Preco.ToString("##,##0.00#############");
                }
            }

        }
        private void Clear_Fields() 
        {
            this.txtPrice.Text = "";
            //cmbPriceType.SelectedValue = null;
            //cmbTicker.SelectedValue = null;
           // DateTime.Now.ToString()
            dtpDate.Value = Convert.ToDateTime(DateTime.Now);
            cmbTicker.Focus();
        }
    }
}