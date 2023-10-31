using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestDLL;
using LiveBook.Business;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace LiveBook
{
    public partial class frmCashTransfer : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmCashTransfer()
        {
            InitializeComponent();
        }

        private void frmCashTransfer_Load(object sender, EventArgs e)
        {
            
            dtgTransf.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgTransf.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgTransf.LookAndFeel.SetSkinStyle("Blue");

            NestDLL.FormUtils.LoadCombo(this.cmbCurrencySell, "Select IdSecurity,NestTicker from Tb001_Securities where IdSecurity in (1844,5791)", "IdSecurity", "NestTicker");
            NestDLL.FormUtils.LoadCombo(this.cmbCurrencyBuy, "Select IdSecurity,NestTicker from Tb001_Securities where IdSecurity in (1844,5791)", "IdSecurity", "NestTicker");
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtCash.Text == "") { txtCash.Text = "0"; };
            int Id_Fund;
            int Id_Account_USD;
            int Id_Account_BRL;
            int Id_Account1;
            int Id_Account2;

            string SQLString="";

            Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());


            switch (Id_Fund)
            { 
                case 4:
                    Id_Account_USD = 1060;
                    Id_Account_BRL = 1046;
                    break;
                case 43:
                    Id_Account_USD = 1148;
                    Id_Account_BRL = 1086;
                    break;
                default:
                    Id_Account_USD = 0;
                    Id_Account_BRL = 0;
                    break;
            }


            if (Id_Account_USD == 0 || Id_Account_BRL == 0)
            {
                return;
            }
            else
            {
                if (Convert.ToInt32(cmbCurrencySell.SelectedValue) == 1844)
                {
                    Id_Account1 = Id_Account_BRL;
                    Id_Account2 = Id_Account_USD;

                }
                else
                {
                    Id_Account2 = Id_Account_BRL;
                    Id_Account1 = Id_Account_USD;
                }
            }

            double inValue = 0;
            double outValue = 0;
            //txtCash.Text.Replace(".", "").Replace(",", ".")

            outValue = Convert.ToDouble(txtCash.Text) * -1;
            inValue = Convert.ToDouble(txtFinalCash.Text) ;

            if (curUtils.IsNumeric(txtCash.Text) )
            {
                SQLString = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                    " VALUES ( 50,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "','" + dtpSettlement.Value.ToString("yyyyMMdd") + "'," + Id_Account1 + "," + cmbCurrencySell.SelectedValue + ",5,1," + outValue.ToString().Replace(".", "").Replace(",", ".") + "," + Id_Account2 + "," + cmbCurrencyBuy.SelectedValue + ",5,1," + inValue.ToString().Replace(".", "").Replace(",", ".") + ") ;";
            }

            if (SQLString !="")
            {
                int retorno = curConn.ExecuteNonQuery(SQLString, 1);
                if (retorno != 0 && retorno != 99)
                {
                    MessageBox.Show("Wire Inserted!");
                }
                else
                {
                    MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Carrega_Grid();
                txtCash.Text = "";
            }
            
        }

       private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {

            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(tempGrid);
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(tempGrid);
        }
        
        private void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            dgTransf.Columns.Clear();

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());

            SQLString = "SELECT * FROM NESTDB.dbo.[FCN_Return_Wire]() Where Id_Portfolio=" + Id_Fund;

            tablep = curConn.Return_DataTable(SQLString);

            dtgTransf.DataSource = tablep;
            
            tablep.Dispose();

            // Delete Button
            dgTransf.Columns["Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTransf.Columns["Cash"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgTransf.Columns["Trade Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgTransf.Columns["Trade Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgTransf.Columns["Trade Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            curUtils.SetColumnStyle(dgTransf, 1);

        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            if (curUtils.IsNumeric(this.txtCash.Text) )
            {
                decimal Cash = Convert.ToDecimal(txtCash.Text);
                this.txtCash.Text = Cash.ToString("##,##0.0000").Replace("-", "");
            }

        }
        private void txtPrice_Leave(object sender, EventArgs e)
        {
            if (curUtils.IsNumeric(this.txtPrice.Text))
            {
                decimal Cash = Convert.ToDecimal(txtPrice.Text);
                this.txtPrice.Text = Cash.ToString("##,##0.#################").Replace("-", "");
            }
        }

        private void cmbFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            calcula_VF();
        }
        private void calcula_VF()
        {
            decimal VF;
            decimal QTD;
            decimal Price;

            if (decimal.TryParse(txtCash.Text, out QTD) && decimal.TryParse(txtPrice.Text, out Price))
            {
                VF = (Convert.ToDecimal(txtPrice.Text) * QTD);
                txtFinalCash.Text = Convert.ToString(VF);
            }
            else
            {
                txtCash.Text = "";
            }
        }

        private void txtFinalCash_Leave(object sender, EventArgs e)
        {
            if (curUtils.IsNumeric(this.txtFinalCash.Text))
            {
                decimal Cash = Convert.ToDecimal(txtFinalCash.Text);
                this.txtFinalCash.Text = Cash.ToString("##,##0.00").Replace("-", "");
            }

        }

  
     }
}