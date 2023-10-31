using System;
using System.Windows.Forms;
using LiveBook.Business;

using LiveDLL;

namespace LiveBook
{
    public partial class frmEditPriceTrade : Form
    {
        public int Id_Trade;
        newNestConn curConn = new newNestConn();
        Business_Class Negocio = new Business_Class();
        
        public int Id_User;

        public frmEditPriceTrade()
        {
            InitializeComponent();
        }

        private void frmEditPriceTrade_Load(object sender, EventArgs e)
        {
            decimal Old_Price;
            string StringSQL;
            StringSQL = "Select Preco from Tb013_Trades (nolock) Where Id_Trade = " + Id_Trade;

            Old_Price = Convert.ToDecimal(curConn.Execute_Query_String(StringSQL));

            if (Old_Price.ToString() != "")
            {
                lblOldPrice.Text = Old_Price.ToString("##,##0.00");
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