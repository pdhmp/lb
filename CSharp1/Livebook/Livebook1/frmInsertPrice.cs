using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.CargaDados;
using SGN.Business;
using SGN.Validacao;

namespace SGN
{
    public partial class frmInsertPrice : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        public int Id_usuario;


        public frmInsertPrice()
        {
            InitializeComponent();
        }

        private void frmInsertPrice_Load(object sender, EventArgs e)
        {
            CargaDados.carregacombo(this.cmbTicker, "Select Id_Ativo, Simbolo from Tb001_Ativos order by Simbolo", "Id_Ativo", "Simbolo", 99);
            CargaDados.carregacombo(this.cmbPriceType, "Select Id_Tipo_Preco,Preco from Tb116_Tipo_Preco order by Preco", "Id_Tipo_Preco", "Preco", 1);
        
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
           int Id_Ativo;
            string Preco;
            string tipo_preco;
            string Data;

            Id_Ativo = Convert.ToInt32(cmbTicker.SelectedValue.ToString());
            tipo_preco = cmbPriceType.SelectedValue.ToString();

            Preco = Convert.ToString(Convert.ToDecimal(txtPrice.Text.ToString()));
            Data = dtpDate.Value.ToString("yyyyMMdd");

            int retorno = Negocios.Insere_Preco(Id_Ativo, Preco, tipo_preco, Data, Id_usuario);
            if (retorno != 0)
            {
                MessageBox.Show("Inserted Price!");
                Clear_Fields();
            }
            else
            {
                MessageBox.Show("Error on Insert. Verify Datas!");
            }
           // string StringSQL; 


        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            decimal Preco;
            if (txtPrice.Text != "")
            {
                Preco = Convert.ToDecimal(txtPrice.Text.ToString());
                if (this.txtPrice.Text != "")
                {
                    this.txtPrice.Text = Preco.ToString("##,###.00###");
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