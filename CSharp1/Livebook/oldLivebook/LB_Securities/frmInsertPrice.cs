using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using LiveBook.Business;


namespace LiveBook
{
    public partial class frmInsertPrice : Form
    {
        
        Business_Class Negocios = new Business_Class();

        public frmInsertPrice()
        {
            InitializeComponent();
            NestDLL.FormUtils.LoadCombo(this.cmbTicker, "Select IdSecurity, NestTicker from Tb001_Securities order by NestTicker", "IdSecurity", "NestTicker", 99);
            NestDLL.FormUtils.LoadCombo(this.cmbPriceType, "Select Id_Tipo_Preco,Preco from Tb116_Tipo_Preco order by Preco", "Id_Tipo_Preco", "Preco", 1);
        }

        public void SetTicker(int Id_Ticker)
        {
            NestDLL.FormUtils.LoadCombo(this.cmbTicker, "Select IdSecurity, NestTicker from Tb001_Securities order by NestTicker", "IdSecurity", "NestTicker", Id_Ticker);
        }

        private void frmInsertPrice_Load(object sender, EventArgs e)
        {
            
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            int IdSecurity;
            string Preco;
            string tipo_preco;
            string Data;
            decimal insPrice = 0;

            if (decimal.TryParse(txtPrice.Text, out insPrice))
            {
                IdSecurity = Convert.ToInt32(cmbTicker.SelectedValue.ToString());
                tipo_preco = cmbPriceType.SelectedValue.ToString();

                Preco = Convert.ToString(insPrice);
                Data = dtpDate.Value.ToString("yyyyMMdd");

                int retorno = Negocios.Insere_Preco(IdSecurity, Preco, tipo_preco, Data);
                if (retorno != 0)
                {
                    MessageBox.Show("Inserted Price!");
                    Clear_Fields();
                }
                else
                {
                    MessageBox.Show("Error on Insert.");
                }
            }
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

            dtpDate.Value = Convert.ToDateTime(DateTime.Now);
            cmbTicker.Focus();
        }
    }
}