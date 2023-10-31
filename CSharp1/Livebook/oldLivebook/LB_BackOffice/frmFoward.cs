using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using SGN.CargaDados;
using SGN.Business;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace SGN
{
    public partial class frmFowards : Form
    {
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
        public int Id_usuario;

        public frmFowards()
        {
            InitializeComponent();
        }

        private void frmFowards_Load(object sender, EventArgs e)
        {
            CargaDados.DB.User_Id = Id_usuario;
            dtgTransf.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgTransf.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgTransf.LookAndFeel.SetSkinStyle("Blue");

            CargaDados.carregacombo(this.cmbCurrencySell, "Select Id_Ativo,Simbolo from Tb001_Ativos where Id_Ativo in (1844,5791)", "Id_Ativo", "Simbolo");
            CargaDados.carregacombo(this.cmbCurrencyBuy, "Select Id_Ativo,Simbolo from Tb001_Ativos where Id_Ativo in (1844,5791)", "Id_Ativo", "Simbolo");


        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtCash.Text == "") { txtCash.Text = "0"; };
            int Id_Fund;
            int Id_Account;
            int Id_Ticker_USD;
            int Id_Ticker_BRL;
            int Id_Ticker1=0;
            int Id_Ticker2 = 0;
            string String_SQL="";

            Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());

            switch (Id_Fund)
            { 
                case 4:
                    Id_Account = 1060;
                    Id_Ticker_USD = 5791;
                    Id_Ticker_BRL = 1844;
                    break;

                case 10:
                    Id_Account = 1073;
                    Id_Ticker_USD = 5791;
                    Id_Ticker_BRL = 1844;
                    break;
                case 43:
                    Id_Account = 1148;
                    Id_Ticker_USD = 5791;
                    Id_Ticker_BRL = 1844;
                    break;
                default:
                    Id_Account = 0;
                    Id_Ticker_USD = 0;
                    Id_Ticker_BRL = 0;
                    break;
            }


            if (Id_Ticker_USD == 0 || Id_Ticker_BRL == 0)
            {
                return;
            }



            

            double inValue = 0;
            double outValue = 0;
            //txtCash.Text.Replace(".", "").Replace(",", ".")

            outValue = Convert.ToDouble(txtCash.Text) * -1;
            inValue = Convert.ToDouble(txtFinalCash.Text) ;

            if (Valida.IsNumeric(txtCash.Text) )
            {
                String_SQL = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                    " VALUES ( 50,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "','" + dtpTradeDate.Value.ToString("yyyyMMdd") + "'," + Id_Account + "," + cmbCurrencySell.SelectedValue + ",5,9," + outValue.ToString().Replace(".", "").Replace(",", ".") + "," + Id_Account + "," + cmbCurrencyBuy.SelectedValue + ",5,9," + inValue.ToString().Replace(".", "").Replace(",", ".") + ") ;";
            }

            if (String_SQL !="")
            {
                int retorno = CargaDados.DB.Execute_Insert_Delete_Update(String_SQL, Id_usuario, 1);
                if (retorno != 0 && retorno != 99)
                {
                    MessageBox.Show("Expense Inserted!");
                }
                else
                {
                    MessageBox.Show("Error on Insert!");
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
            Valida.Save_Columns(tempGrid, Id_usuario);
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid, Id_usuario);
        }
        
        private void Carrega_Grid()
        {
            string StringSQl;
            SqlDataAdapter dp = new SqlDataAdapter();
            DataTable tablep = new DataTable();

            dgTransf.Columns.Clear();

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());
            int Id_Account1=0;

            switch (Id_Fund)
            { 
                case 4:
                    Id_Account1 = 1060;
                    break;

                case 10:
                    Id_Account1 = 1073;
                    break;

                case 43:
                    Id_Account1 = 1148;
                    break;
            }

            StringSQl = "SELECT * FROM NESTDB.dbo.[FCN_Return_Wire]() Where Account_Ticker=" + Id_Account1;

            dp = CargaDados.DB.Return_DataAdapter(StringSQl);
            dp.Fill(tablep);
            dtgTransf.DataSource = tablep;
            dp.Dispose();
            tablep.Dispose();

            // Delete Button
            dgTransf.Columns["Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTransf.Columns["Cash"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgTransf.Columns["Trade Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgTransf.Columns["Trade Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgTransf.Columns["Trade Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            Valida.SetColumnStyle(dgTransf, Id_usuario, 1);

        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(this.txtCash.Text) )
            {
                decimal Cash = Convert.ToDecimal(txtCash.Text);
                this.txtCash.Text = Cash.ToString("##,##0.00######").Replace("-","") ;
            }

        }
        private void txtPrice_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(this.txtPrice.Text))
            {
                decimal Cash = Convert.ToDecimal(txtPrice.Text);
                this.txtPrice.Text = Cash.ToString("##,##0.00###").Replace("-", "");
            }
        }


        private void frmExpenses_FormClosing(object sender, FormClosingEventArgs e)
        {
            Valida.Save_Properties_Form(this, Id_usuario, 0);

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
            String SqlString;
            decimal Lote_Padrao;
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

  
     }
}