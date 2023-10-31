using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestDLL;

namespace LiveBook
{
    public partial class frmEditFoward : LBForm
    {
       newNestConn curConn = new newNestConn();

       public float IdFoward;
       decimal RoundLot=1;

        public frmEditFoward()
        {
            InitializeComponent();
        }

        private void frmEditFoward_Load(object sender, EventArgs e)
        {
            NestDLL.FormUtils.LoadCombo(this.cmbBook, "Select Id_Book, Book from dbo.Tb400_Books (nolock) ", "Id_Book", "Book", 99);

            LoadInfo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void LoadInfo()
        {
            string StringSQL = "Select A.Id_Foward,A.Id_Ticker,A.Id_Book,A.Id_Section,A.Quantity,A.Price,A.Cash,A.PriceBrokerage,A.SpotPriceBrokerage, " +
           " A.SpotPrice,NestTicker,Port_Name,A.Id_Account,RoundLot,Id_Portfolio,Trade_Date FROM dbo.Tb725_Fowards (nolock) A " +
           " INNER JOIN dbo.Tb001_Securities (nolock) B " +
           " ON A. Id_Ticker = B.IdSecurity " +
           " INNER JOIN dbo.VW_PortAccounts (nolock) C " +
           " ON A.Id_Account = C.Id_Account " +
           " Where Id_Foward = " + IdFoward + 
           " and Id_Port_Type =2" ;

          DataTable curTable = curConn.Return_DataTable(StringSQL);

          foreach (DataRow curRow in curTable.Rows)
          {

              this.txtFund.Text = curRow["Port_Name"].ToString();
              this.txtSecurity.Text = curRow["NestTicker"].ToString();
              this.txtQuantity.Text = curRow["Quantity"].ToString();
              this.txtPrice.Text = curRow["Price"].ToString();
              this.txtSpotPrice.Text = curRow["SpotPrice"].ToString();
              this.txtCash.Text = curRow["Cash"].ToString();

              this.txtPriceWBrokerage.Text = curRow["PriceBrokerage"].ToString();
              this.txtSpotPriceWBrokerage.Text = curRow["SpotPriceBrokerage"].ToString();
              
              this.dtpTradeDate.Value = Convert.ToDateTime(curRow["Trade_Date"].ToString());

              NestDLL.FormUtils.LoadCombo(this.cmBroker, "Select Id_Account,Nome from VW_Account_Broker Where Id_Portfolio = " + curRow["Id_Portfolio"].ToString() + " order by Show_Prefered desc,Nome asc", "Id_Account", "Nome", 99);

              this.cmbBook.SelectedValue = curRow["Id_Book"].ToString();
              this.cmbSection.SelectedValue = curRow["Id_Section"].ToString();
              this.cmBroker.SelectedValue = curRow["Id_Account"].ToString();
              RoundLot = Convert.ToDecimal(curRow["RoundLot"].ToString());

              object sender = new object();
              EventArgs e = new EventArgs();

              txtQuantity_Leave(sender, e);
              txtPrice_Leave(sender, e);
              txtSpotPrice_Leave(sender, e);
              txtCash_Leave(sender, e);
              txtPriceWBrokerage_Leave(sender, e);
              txtSpotPriceWBrokerage_Leave(sender, e);


          }
 
        }

        private void cmbBook_SelectedValueChanged(object sender, EventArgs e)
        {
            Carrega_Section();
        }

        public void Carrega_Section()
        {
            int valor;
            try
            {
                if (curUtils.IsNumeric(cmbBook.SelectedValue))
                {

                    valor = Convert.ToInt32(cmbBook.SelectedValue);
                    //SubEstrategia
                    NestDLL.FormUtils.LoadCombo(this.cmbSection, "Select Id_Section,Section from VW_Book_Strategies where Id_Book =" + valor, "Id_Section", "Section", 99);
                }
            }
            catch (Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            if (this.txtQuantity.Text != "")
            {
                decimal QTD = Convert.ToDecimal(txtQuantity.Text);
                this.txtQuantity.Text = QTD.ToString("##,##0.00#######");
            }
            txtCash_Leave(sender, e);

        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            if (this.txtPrice.Text != "")
            {
                decimal Price = Convert.ToDecimal(txtPrice.Text);
                this.txtPrice.Text = Price.ToString("##,##0.00#######");
            }
            txtCash_Leave(sender, e);

        }

        private void txtSpotPrice_Leave(object sender, EventArgs e)
        {
            if (this.txtSpotPrice.Text != "")
            {
                decimal Price = Convert.ToDecimal(txtSpotPrice.Text);
                this.txtSpotPrice.Text = Price.ToString("##,##0.00#######");
            }
        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            if (this.txtCash.Text != "")
            {
                decimal Cash = Convert.ToDecimal(txtCash.Text);
                this.txtCash.Text = Cash.ToString("##,##0.00#######");
            }

        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            calcula_VF();

        }

        private void calcula_VF()
        {
            String SQLString;
                decimal VF;
                decimal QTD;
                decimal Price;

                if (decimal.TryParse(txtQuantity.Text, out QTD) && decimal.TryParse(txtPrice.Text, out Price) && txtSecurity.Text != null)
                {



                    VF = (Convert.ToDecimal(txtPrice.Text) * (QTD / RoundLot));
                    txtCash.Text = Convert.ToString(VF);
                }
                else
                {
                    txtCash.Text = "";
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateData();
        }

        void UpdateData()
        {
            decimal IdAccount = Convert.ToDecimal(cmBroker.SelectedValue);
            int IdBook = Convert.ToInt32(cmbBook.SelectedValue);
            int IdSection =  Convert.ToInt32(cmbSection.SelectedValue);
            decimal Quantity = Convert.ToDecimal(txtQuantity.Text.ToString());
            decimal Price = Convert.ToDecimal(txtPrice.Text.ToString());
            decimal SpotPrice = Convert.ToDecimal(txtSpotPrice.Text.ToString());
            decimal Cash = Convert.ToDecimal(txtCash.Text.ToString());
            decimal PriceBrokerage = Convert.ToDecimal(txtPriceWBrokerage.Text.ToString());
            decimal SpotPriceWBrokerage = Convert.ToDecimal(txtSpotPriceWBrokerage.Text.ToString());

            string StringSQL;

            StringSQL = "UPDATE [NESTDB].[dbo].[Tb725_Fowards] " +
                         "  SET [Id_Account] = " + IdAccount +
                         "     ,Trade_Date = '" + dtpTradeDate.Value.ToString("yyyyMMdd") + "'" +
                         "     ,[Id_Book] = " + IdBook + 
                         "     ,[Id_Section] = " + IdSection +
                         "     ,[Quantity] = " + Quantity.ToString().Replace(".", "").Replace(",", ".") +
                         "     ,[Price] = " + Price.ToString().Replace(".", "").Replace(",", ".") +
                         "     ,[SpotPrice] = " + SpotPrice.ToString().Replace(".", "").Replace(",", ".") +
                         "     ,[Cash] = " + Cash.ToString().Replace(".", "").Replace(",", ".") +
                         "     ,PriceBrokerage = " + PriceBrokerage.ToString().Replace(".", "").Replace(",", ".") +
                         "     ,SpotPriceBrokerage = " + SpotPriceWBrokerage.ToString().Replace(".", "").Replace(",", ".") +
                         " WHERE Id_Foward =" + IdFoward ;

            int retorno = curConn.ExecuteNonQuery(StringSQL,1);

            if (retorno != 1)
            {
                MessageBox.Show("Error on Update!");
            }
            else
            {
                MessageBox.Show("Updated!");
            }



        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmdUpdate.Enabled = false;
            DeleteFoward();
        }

        void DeleteFoward()
        {
             string StringSQL="";

             int returns = Convert.ToInt32(MessageBox.Show("Do you really want to Delete this Foward?", "Delete Foward", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

             if (returns == 6)
             {
                 StringSQL = "DELETE FROM [NESTDB].[dbo].[Tb725_Fowards] " +
                     "WHERE ID_Foward=" + IdFoward;
             }

             if (StringSQL != "")
             {
                 int retorno = curConn.ExecuteNonQuery(StringSQL, 1);

                 if (retorno != 1)
                 {
                     MessageBox.Show("Error on Delete!");
                 }
                 else
                 {
                     MessageBox.Show("Deleted!");
                 }
             }
        }

        private void txtPriceWBrokerage_Leave(object sender, EventArgs e)
        {
            if (this.txtPriceWBrokerage.Text != "")
            {
                decimal Cash = Convert.ToDecimal(txtPriceWBrokerage.Text);
                this.txtPriceWBrokerage.Text = Cash.ToString("##,##0.00#######");
            }
        }

        private void txtSpotPriceWBrokerage_Leave(object sender, EventArgs e)
        {
            if (this.txtSpotPriceWBrokerage.Text != "")
            {
                decimal Cash = Convert.ToDecimal(txtSpotPriceWBrokerage.Text);
                this.txtSpotPriceWBrokerage.Text = Cash.ToString("##,##0.00#######");
            }
        }
    }
}
