using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using SGN.Business;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace SGN
{
    public partial class frmExpenses : LBForm
    {
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();

        public frmExpenses()
        {
            InitializeComponent();
        }

        private void frmDividends_Load(object sender, EventArgs e)
        {
            
            dtgExpenses.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgExpenses.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgExpenses.LookAndFeel.SetSkinStyle("Blue");
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtCash.Text == "") { txtCash.Text = "0"; };
            int Id_Fund;
            int Id_Account;
            int Id_Ticker1;
            int Id_Ticker2;
            string SQLString="";

            Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());

            switch (Id_Fund)
            { 
                case 4:
                    Id_Account = 1060;
                    Id_Ticker1 = 19017;
                    Id_Ticker2 = 5791;
                    break;

                case 10:
                    Id_Account = 1073;
                    Id_Ticker1 = 18990;
                    Id_Ticker2 = 1844;
                    break;
                case 43:
                    Id_Account = 1086;
                    Id_Ticker1 = 18990;
                    Id_Ticker2 = 1844;
                    break;
                default:
                    Id_Account = 0;
                    Id_Ticker1 = 0;
                    Id_Ticker2 = 0;
                    break;
            }

            if (Valida.IsNumeric(txtCash.Text) && txtCash.Text != "0")
            {
                SQLString = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                    " VALUES ( 60,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "','" + dtpTradeDate.Value.ToString("yyyyMMdd") + "'," + Id_Account + "," + Id_Ticker1 + ",5,1,0," + Id_Account + "," + Id_Ticker2  + ",5,1," + "-" + txtCash.Text.Replace(".","").Replace(",",".") +") ;";
            }
            if (Valida.IsNumeric(txtRevenues.Text))
            {

                SQLString = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                    " VALUES ( 70,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "','" + dtpTradeDate.Value.ToString("yyyyMMdd") + "'," + Id_Account + "," + Id_Ticker1 + ",5,1,0," + Id_Account + "," + Id_Ticker2 + ",5,1," + txtRevenues.Text.Replace(".", "").Replace(",", ".") + ") ;";
            }
            if (SQLString !="")
            {
                int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
                if (retorno != 0 && retorno != 99)
                {
                    MessageBox.Show("Inserted!");
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
            Valida.Save_Columns(tempGrid);
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid);
        }
        
        private void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            dgExpenses.Columns.Clear();

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
                    Id_Account1 = 1086;
                    break;
            }

            SQLString = "SELECT * FROM NESTDB.dbo.FCN_Return_Expenses() Where Account_Ticker=" + Id_Account1;

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgExpenses.DataSource = tablep;
            
            tablep.Dispose();

            // Delete Button
            dgExpenses.Columns.AddField("Delete");
            dgExpenses.Columns["Delete"].VisibleIndex = 0;
            dgExpenses.Columns["Delete"].Width = 60;
            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Delete";
            dtgExpenses.RepositoryItems.Add(item5);
            dgExpenses.Columns["Delete"].ColumnEdit = item5;
            dgExpenses.Columns["Delete"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgExpenses.OptionsBehavior.Editable = false;
            dgExpenses.Columns["Delete"].Visible = true;

            dgExpenses.Columns["Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgExpenses.Columns["Cash"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgExpenses.Columns["Trade Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgExpenses.Columns["Trade Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgExpenses.Columns["Trade Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            Valida.SetColumnStyle(dgExpenses, 1);

        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(this.txtCash.Text) )
            {
                decimal Cash = Convert.ToDecimal(txtCash.Text);
                this.txtCash.Text = Cash.ToString("##,##0.00").Replace("-","");
            }

        }

        private void txtRevenues_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(this.txtRevenues.Text))
            {
                decimal Revenues = Convert.ToDecimal(txtRevenues.Text);
                this.txtRevenues.Text = Revenues.ToString("##,##0.00").Replace("-", "");
            }

        }

        private void cmbFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgExpenses_DoubleClick(object sender, EventArgs e)
        {
            int resposta;
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

            if (Column_Name == "Delete")
            {
                if (dgExpenses.FocusedRowHandle != null)
                {
                    string curPos = dgExpenses.GetRowCellValue(dgExpenses.FocusedRowHandle, "Expense_Id").ToString();

                    resposta = Convert.ToInt32(MessageBox.Show("Do you really want to delete this Expense entry?", "Delete Expense", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                    if (resposta == 6)
                    {
                        string SQLString;
                        SQLString = "DELETE FROM Tb700_Transactions WHERE Transaction_Id=" + curPos;
                        CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
                        Carrega_Grid();
                    }
                }
            }

        }

       }
}