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
    public partial class frmClientTransactions : LBForm
    {
        Business_Class Negocios = new Business_Class();
        LB_Utils curUtils = new LB_Utils();
        newNestConn curConn = new newNestConn();

        string Transaction_Type_Message = "";

        public frmClientTransactions()
        {
            InitializeComponent();
        }

        private void frmDividends_Load(object sender, EventArgs e)
        {
            
            dtgClientTransac.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgClientTransac.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgClientTransac.LookAndFeel.SetSkinStyle("Blue");

            Load_Combos();
            
            Carrega_Grid();
        }

        void Load_Combos()
        {
            NestDLL.FormUtils.LoadCombo(this.cmbFund, "Select Id_Portfolio,Port_Name from Tb002_Portfolios Where Id_Port_Type=3 and Discountinued=0 order by Port_Name", "Id_Portfolio", "Port_Name", 99);
            NestDLL.FormUtils.LoadCombo(this.cmbTransType, "Select Id_Trans_Type, Description from dbo.Tb701_Transaction_Types WHERE Id_Trans_Type IN (30,31,32) order by Description", "Id_Trans_Type", "Description", 21);
            NestDLL.FormUtils.LoadCombo(this.cmbContact, "SELECT * FROM dbo.Tb751_Contacts order by Contact_Name", "Id_Contact", "Contact_Name", 99);
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtShare.Text == "") { txtShare.Text = "0000"; };
            if (txtCash.Text == "") { txtCash.Text = "0"; };
            if (txtSharePrice.Text == "") { txtSharePrice.Text = "0000"; };
            if (txtIncomeTax.Text == "") { txtIncomeTax.Text = "0000"; };

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());
            int Id_Account = 0;
            int Id_Ticker = 0;
            int Id_Contact = Convert.ToInt32(cmbContact.SelectedValue.ToString());

            switch (Id_Fund)
            {
                case 4:
                    Id_Account=1060;
                    Id_Ticker = 5228;
                    break;
                case 43:
                    Id_Account=1086;
                    Id_Ticker = 4803;
                    break;
                case 10:
                    Id_Account = 1073;
                    Id_Ticker = 4946;
                    break;
                default:
                    Id_Account = 0;
                    Id_Ticker = 0;
                    break;
            }

            string AdminID = "";
            string AdminID_Sub = "";

            if(cmbTransType.SelectedValue.ToString() == "30") {AdminID_Sub = txtAdminID.Text;};
            if (cmbTransType.SelectedValue.ToString() != "30") { AdminID = txtAdminID.Text; };

            if (curUtils.IsNumeric(txtCash.Text) || curUtils.IsNumeric(txtShare))
            {
                string SQLString = "INSERT INTO [NESTDB].[dbo].[Tb760_Subscriptions_Mellon]([Id_Contact],[Request_Date],[Trade_Date],[Settlement_Date],[Transaction_Type],[Transaction_NAV],[Id_Portfolio],[AdminRef],[AdminRef_Sub],[Quantity],[Fin_Amount],[IncomeTax],[Create_Timestamp]) " +
                                    "VALUES ( " + Id_Contact + ",'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "','" + dtpConversion.Value.ToString("yyyyMMdd") + "','" + dtpPayment.Value.ToString("yyyyMMdd") + "'," + cmbTransType.SelectedValue.ToString() + ", " + txtSharePrice.Text.Replace(".", "").Replace(",", ".") + ", " + Id_Fund + ", '" + AdminID + "', '" + AdminID_Sub + "', " + txtShare.Text.Replace(",", ".") + ", " + txtCash.Text.Replace(".", "").Replace(",", ".") + ", " + txtIncomeTax.Text.Replace(".", "").Replace(",", ".") + ", GetDate())";

                int retorno = curConn.ExecuteNonQuery(SQLString, 1);

                if (retorno != 0 && retorno != 99)
                {
                    MessageBox.Show(Transaction_Type_Message);
                }
                else
                {
                    MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Carrega_Grid();
                txtCash.Text = "";
                txtShare.Text = "";
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

            dgClientTransac.Columns.Clear();

            SQLString = "SELECT B.Contact_Name, C.Port_Name, A.*, DATEADD(DD, 1 - DAY(Trade_Date), Trade_Date) AS RefMonth, D.Description AS TransactionType " +
                        " FROM NESTDB.dbo.Tb760_Subscriptions_Mellon A " +
                        " INNER JOIN NESTDB.dbo.Tb751_Contacts B " +
                        " ON A.Id_Contact=B.Id_Contact " +
                        " INNER JOIN NESTDB.dbo.Tb002_Portfolios C " +
                        " ON A.Id_Portfolio=C.Id_Portfolio " +
                        " INNER JOIN NESTDB.dbo.Tb701_Transaction_Types D " +
                        " ON A.Transaction_Type=D.Id_Trans_Type";
            
            tablep = curConn.Return_DataTable(SQLString);

            dtgClientTransac.DataSource = tablep;
            
            tablep.Dispose();

            // Delete Button
            dgClientTransac.Columns.AddField("Delete");
            dgClientTransac.Columns["Delete"].VisibleIndex = 0;
            dgClientTransac.Columns["Delete"].Width = 60;
            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Delete";
            dtgClientTransac.RepositoryItems.Add(item5);
            dgClientTransac.Columns["Delete"].ColumnEdit = item5;
            dgClientTransac.Columns["Delete"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgClientTransac.OptionsBehavior.Editable = false;
            dgClientTransac.Columns["Delete"].Visible = true;

            dgClientTransac.Columns["Fin_Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgClientTransac.Columns["Fin_Amount"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            //dgSubRedemp.Columns["Cash Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            //dgSubRedemp.Columns["Cash Amount"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            //dgSubRedemp.Columns["Payment Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //dgSubRedemp.Columns["Payment Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //dgSubRedemp.Columns["Payment Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            //dgSubRedemp.Columns["Conversion Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //dgSubRedemp.Columns["Conversion Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //dgSubRedemp.Columns["Conversion Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            //dgSubRedemp.Columns["Trade Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //dgSubRedemp.Columns["Trade Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //dgSubRedemp.Columns["Trade Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            curUtils.SetColumnStyle(dgClientTransac, 1);

        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            if (curUtils.IsNumeric(this.txtCash.Text) )
            {
                decimal Cash = Convert.ToDecimal(txtCash.Text);
                this.txtCash.Text = Cash.ToString("##,##0");
            }

        }

        private void cmbTransType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate_Date();
        }

        void Validate_Date()
        {
 
            if ( cmbTransType.SelectedValue != null && cmbFund.SelectedValue != null)
            {
                if (curUtils.IsNumeric(cmbTransType.SelectedValue.ToString()) && curUtils.IsNumeric(cmbFund.SelectedValue.ToString()))
                {
                    //grpDate.Enabled = true;
                    int Transaction_Type = Convert.ToInt32(cmbTransType.SelectedValue.ToString());

                  int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());
                  int Region;
                    if (Id_Fund == 4)
                    {
                        Region = 4;
                    }
                    else
                    {
                        Region = 1;
                    }

                    if (Transaction_Type == 30)
                    {
                        Transaction_Type_Message = "Subscription Inserted!";
                        //next day
                        dtpConversion.Value = dtpTradeDate.Value; //Convert.ToDateTime(CargaDados.DB.Execute_Query_String("Select NESTDB.dbo.FCN_NDATEADD('du',1,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "',2," + Region + ")"));
                        dtpPayment.Value = dtpTradeDate.Value;
                    }

                    if (Transaction_Type != 30)
                    {
                        Transaction_Type_Message = "Redemption Inserted!";
                        dtpConversion.Value = dtpTradeDate.Value;
                        dtpPayment.Value = Convert.ToDateTime(curConn.Execute_Query_String("Select NESTDB.dbo.FCN_NDATEADD('du',3,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "',2," + Region + ")"));
                        //3 days
                    }
                }
            }
        
        }

        private void cmbFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate_Date();
        }

        private void dgClientTransac_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Name;

            if (Column_Name == "colDelete" && (NestDLL.NUserControl.Instance.User_Id==38 ||  NestDLL.NUserControl.Instance.User_Id==17))
            {
                if (dgClientTransac.FocusedRowHandle != null)
                {
                    int curIndex = int.Parse(dgClientTransac.GetRowCellValue(dgClientTransac.FocusedRowHandle, "Transaction_Id").ToString());

                    DialogResult UserAnswer = MessageBox.Show("Do you really want to delete this entry?", "Delete Entry", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (UserAnswer == DialogResult.OK)
                    {
                        using (newNestConn curConn = new newNestConn())
                        {
                            curConn.ExecuteNonQuery("DELETE FROM NESTDB.dbo.Tb760_Subscriptions_Mellon WHERE Transaction_Id=" + curIndex.ToString() + ";");
                            Carrega_Grid();
                        }
                    }
                }
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgClientTransac.SelectAll();
            dgClientTransac.CopyToClipboard();
            //  MessageBox.Show("Copied!");
        }
     }
}