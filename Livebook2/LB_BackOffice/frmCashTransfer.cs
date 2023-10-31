using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using LiveDLL;


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

            LiveDLL.FormUtils.LoadCombo(this.cmbCurrencySell, "Select IdSecurity,NestTicker from Tb001_Securities where IdSecurity in (1844,5791)", "IdSecurity", "NestTicker");
            LiveDLL.FormUtils.LoadCombo(this.cmbCurrencyBuy, "Select IdSecurity,NestTicker from Tb001_Securities where IdSecurity in (1844,5791)", "IdSecurity", "NestTicker");
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtCash.Text == "") { txtCash.Text = "0"; };
            int Id_Fund;
            int Id_Account_USD;
            int Id_Account_BRL;
            int Id_Account1;
            int Id_Account2;

            string SQLString = "";

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
            inValue = Convert.ToDouble(txtFinalCash.Text);

            if (curUtils.IsNumeric(txtCash.Text))
            {
                SQLString = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                    " VALUES ( 50,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "','" + dtpSettlement.Value.ToString("yyyyMMdd") + "'," + Id_Account1 + "," + cmbCurrencySell.SelectedValue + ",5,1," + outValue.ToString().Replace(".", "").Replace(",", ".") + "," + Id_Account2 + "," + cmbCurrencyBuy.SelectedValue + ",5,1," + inValue.ToString().Replace(".", "").Replace(",", ".") + ") ;";
            }

            if (SQLString != "")
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
            gviewTransf.Columns.Clear();
            dtgTransf.DataSource = null;
            dtgTransf.Visible = false;

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());

            string SQLString = "SELECT * FROM NESTDB.dbo.[FCN_Return_Wire]() Where Id_Portfolio=" + Id_Fund + " ORDER BY [Trade Date] DESC";

            DataTable tablep = curConn.Return_DataTable(SQLString);

            if (tablep.Rows.Count > 0)
            {
                dtgTransf.Visible = true;
                dtgTransf.DataSource = tablep;

                tablep.Dispose();

                gviewTransf.BestFitColumns();

                gviewTransf.Columns.AddField("Delete");
                gviewTransf.Columns["Delete"].VisibleIndex = 0;
                gviewTransf.Columns["Delete"].Width = 60;

                RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();

                item5.Buttons[0].Tag = 1;
                item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item5.Buttons[0].Caption = "Delete";
                dtgTransf.RepositoryItems.Add(item5);
                gviewTransf.Columns["Delete"].ColumnEdit = item5;
                gviewTransf.Columns["Delete"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                gviewTransf.OptionsBehavior.Editable = false;
                gviewTransf.Columns["Delete"].Visible = true;

                gviewTransf.Columns["Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gviewTransf.Columns["Cash"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

                gviewTransf.Columns["Trade Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                gviewTransf.Columns["Trade Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gviewTransf.Columns["Trade Date"].DisplayFormat.FormatString = "dd/MM/yyyy";

                curUtils.SetColumnStyle(gviewTransf, 1);

            }
        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            if (curUtils.IsNumeric(this.txtCash.Text))
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

        private void bntLoad_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void gviewTransf_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

            if (Column_Name == "Delete")
            {
                if (gviewTransf.FocusedRowHandle != null)
                {
                    int IdTransaction = int.Parse(gviewTransf.GetRowCellValue(gviewTransf.FocusedRowHandle, "Transaction_Id").ToString());

                    string SQLString = "DELETE FROM [NESTDB].[dbo].[Tb700_Transactions] WHERE Transaction_Id = " + IdTransaction;

                    System.Windows.Forms.DialogResult userConfirmation = System.Windows.Forms.MessageBox.Show("Delete this Transfer?", "Delete Transfer", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

                    if (userConfirmation == System.Windows.Forms.DialogResult.OK)
                    {
                        using (newNestConn curConn = new newNestConn())
                        {
                            if (curConn.ExecuteNonQuery(SQLString) > 0)
                            {
                                MessageBox.Show("Deleted!","Sucess");
                            }
                            else
                                MessageBox.Show("Not Deleted!","Failed");
                        }
                    }
                }
            }

            Carrega_Grid();
        }
    }
}