using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using LiveBook.Business;
using LiveDLL;

namespace LiveBook
{
    public partial class frmDividends : Form
    {
        Business_Class Negocios = new Business_Class();
        LB_Utils curUtils = new LB_Utils();
        newNestConn curConn = new newNestConn();

        public frmDividends()
        {
            InitializeComponent();
        }

        private void frmDividends_Load(object sender, EventArgs e)
        {
            
            dtgDividends.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgDividends.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgDividends.LookAndFeel.SetSkinStyle("Blue");

            Load_Combos();
            
            Carrega_Grid();
        }

        void Load_Combos()
        {
            LiveDLL.FormUtils.LoadCombo(this.cmbTicker, "Select IdSecurity, NestTicker from Tb001_Securities(nolock)  Where (Expiration IS NULL) OR Expiration = '19000101' OR (Expiration >= (CONVERT(VARCHAR, GETDATE()-5, 112))) order by NestTicker", "IdSecurity", "NestTicker", 99);
            LiveDLL.FormUtils.LoadCombo(this.cmbTransType, "Select Id_Trans_Type, Description from dbo.Tb701_Transaction_Types (nolock)  WHERE Id_Trans_Type IN (21,22,23) order by Description", "Id_Trans_Type", "Description", 21);
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {

            if (txtNet.Text == "") { txtNet.Text = "0"; };
            if (txtGross.Text == "") { txtGross.Text = "0"; };
            if (txtBonus.Text == "") { txtBonus.Text = "0"; };

            string SQLString = "INSERT INTO [NESTDB].[dbo].[Tb720_Dividends]([Transaction_Type],[Id_Ticker],[Declared_Date],[Ex_Date],[Record_Date],[Payment_Date],[Per_Share_Net],[Per_Share_Gross],[Percent_Bonus],[Description])" +
                                "VALUES (" + cmbTransType.SelectedValue.ToString() + ", " + cmbTicker.SelectedValue.ToString() + ", '" + dtpDeclared.Value.ToString("yyyyMMdd") + "', '" + dtpEx.Value.ToString("yyyyMMdd") + "', '" + dtpRecord.Value.ToString("yyyyMMdd") + "', '" + dtpPayment.Value.ToString("yyyyMMdd") + "', " + txtNet.Text.Replace(",", ".") + ", " + txtGross.Text.Replace(",", ".") + ", " + txtBonus.Text.Replace(",", ".") + ", '')";

            int retorno = curConn.ExecuteNonQuery(SQLString, 1);
            if (retorno != 0)
            {
                if (this.Text != "Insert Dividend")
                {
                    MessageBox.Show("Dividend Inserted!");
                }
            }
            else
            {
                MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Carrega_Grid();

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

            dgDividends.Columns.Clear();

            SQLString = "SELECT B.NestTicker, C.Description AS [Transaction Type], A.* " +
                        "FROM dbo.Tb720_Dividends A (nolock) " +
                        "INNER JOIN dbo.Tb001_Securities B (nolock)  " +
                        "ON A.Id_Ticker=B.IdSecurity " +
                        "INNER JOIN dbo.Tb701_Transaction_Types C (nolock) " +
                        "ON A.Transaction_Type=C.Id_Trans_Type " +
                        "ORDER BY Ex_Date desc";

            tablep = curConn.Return_DataTable(SQLString);
            dtgDividends.DataSource = tablep;
            
            tablep.Dispose();

            // Delete Button
            dgDividends.Columns.AddField("Delete");
            dgDividends.Columns["Delete"].VisibleIndex = 0;
            dgDividends.Columns["Delete"].Width = 60;
            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Delete";
            dtgDividends.RepositoryItems.Add(item5);
            dgDividends.Columns["Delete"].ColumnEdit = item5;
            dgDividends.Columns["Delete"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgDividends.OptionsBehavior.Editable = false;
            dgDividends.Columns["Delete"].Visible = true;

            // ----------------------------------------------------------------------------------------------------------

            // Insert Button
            dgDividends.Columns.AddField("Insert");
            dgDividends.Columns["Insert"].VisibleIndex = 0;
            dgDividends.Columns["Insert"].Width = 60;

            RepositoryItemButtonEdit item6 = new RepositoryItemButtonEdit();
            item6.Buttons[0].Tag = 2;
            item6.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item6.Buttons[0].Caption = "Insert";
            dtgDividends.RepositoryItems.Add(item6);
            dgDividends.Columns["Insert"].ColumnEdit = item6;
            dgDividends.Columns["Insert"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgDividends.OptionsBehavior.Editable = false;
            dgDividends.Columns["Insert"].Visible = true;

            // ----------------------------------------------------------------------------------------------------------

            dgDividends.Columns["Per_Share_Net"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDividends.Columns["Per_Share_Net"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgDividends.Columns["Per_Share_Gross"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDividends.Columns["Per_Share_Gross"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgDividends.Columns["Payment_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgDividends.Columns["Payment_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgDividends.Columns["Payment_Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            dgDividends.Columns["Record_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgDividends.Columns["Record_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgDividends.Columns["Record_Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            dgDividends.Columns["Ex_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgDividends.Columns["Ex_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgDividends.Columns["Ex_Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            dgDividends.Columns["Declared_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgDividends.Columns["Declared_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgDividends.Columns["Declared_Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            dgDividends.Columns["Percent_Bonus"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgDividends.Columns["Percent_Bonus"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDividends.Columns["Percent_Bonus"].DisplayFormat.FormatString = "P2";

            curUtils.SetColumnStyle(dgDividends, 1);

        }

        private void dgDividends_DoubleClick(object sender, EventArgs e)
        {
            int resposta;
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

            if (Column_Name == "Delete")
            {
                if (dgDividends.FocusedRowHandle != null)
                {
                    string curPos = dgDividends.GetRowCellValue(dgDividends.FocusedRowHandle, "Id_Dividend").ToString();

                    resposta = Convert.ToInt32(MessageBox.Show("Do you really want to delete this dividend entry?", "Delete Dividend", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                    if (resposta == 6)
                    {
                        string SQLString;
                        SQLString = "DELETE FROM dbo.Tb720_Dividends WHERE Id_Dividend=" + curPos;
                      int retorno = curConn.ExecuteNonQuery(SQLString,1);
                        Carrega_Grid();
                    }
                }
            }
            if (Column_Name == "Insert")
            {
                //frmSubscription frm = new frmSubscription();

                //frm.IdSecurity = Convert.ToInt32(dgDividends.GetRowCellValue(dgDividends.FocusedRowHandle, "Id_Ticker"));
                //frm.NestTicker = dgDividends.GetRowCellValue(dgDividends.FocusedRowHandle, "NestTicker").ToString();

                //frm.ShowDialog();

               // frmTrade_Aloc_Override Aloca = new frmSubscription();

                //Aloca.Top = this.Top + 100;
                //Aloca.Left = this.Left + 100;

                //Aloca.Id_Ticker = Id_Ticker;
                //Aloca.Id_Broker = Id_Broker;
                //Aloca.Id_Book = Id_Book;
                //Aloca.Id_Section = Id_Section;
                //Aloca.lblTicker.Text = Ticker_Name;
                //Aloca.lblBroker.Text = Broker_Name;
                //Aloca.ShowDialog();
            }
        }

        private void cmbTransType_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbTransType.SelectedIndex != 0)
            {
                if (Convert.ToInt32(cmbTransType.SelectedValue) == 23)
                {
                    txtGross.Text = "";
                    txtGross.Enabled = false;
                    txtNet.Text = "";
                    txtNet.Enabled = false;

                    txtBonus.Enabled = true;
                }
                else
                {
                    txtBonus.Text = "";
                    txtBonus.Enabled = false;

                    txtGross.Enabled = true;
                    txtNet.Enabled = true;
                }
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgDividends.SelectAll();
            dgDividends.CopyToClipboard();

        }

     }
}