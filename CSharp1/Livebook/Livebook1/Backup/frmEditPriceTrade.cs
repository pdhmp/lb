using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Business;
using SGN.CargaDados;


namespace SGN
{
    public partial class frmEditPriceTrade : Form
    {
       public int Id_Trade;

        Business_Class Negocio = new Business_Class();
        CarregaDados Carga = new CarregaDados();

        public frmEditPriceTrade()
        {
            InitializeComponent();
        }

        private void frmEditPriceTrade_Load(object sender, EventArgs e)
        {
            decimal Old_Price;
            string StrinSQL;
            StrinSQL = "Select Preco from Tb013_Trades Where Id_Trade = " + Id_Trade;
            Old_Price = Convert.ToDecimal(Carga.DB.Execute_Query_String(StrinSQL));

            if (Old_Price.ToString() != "")
            {

                lblOldPrice.Text = Old_Price.ToString("##,###.00");
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (Id_Trade != 0)
            {
                Negocio.Edit_Price_Trade(Id_Trade, txtPrice.Text.ToString());
            }
            else
            {
                MessageBox.Show("Error on Update");
            }
            this.Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}