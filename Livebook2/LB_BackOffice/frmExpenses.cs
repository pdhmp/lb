using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using LiveDLL;


namespace LiveBook
{
    public partial class frmExpenses : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmExpenses()
        {
            InitializeComponent();
        }

        private void frmExpenses_Load(object sender, EventArgs e)
        {
            CarregaPort();
            dtpIniDate.Value = DateTime.Now.Subtract(new TimeSpan(10, 0, 0, 0));
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtValue.Text == "" || txtValue.Text == "0")
            {
                MessageBox.Show("Amount must be different from 0!");
                return;
            }

            int Id_Fund = 0;
            int Id_Account = 0;
            int Id_Ticker1 = 0;
            int Id_Ticker2 = 0;
            string SQLString = "";

            Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());
            Id_Ticker1 = (int)cmbIdSecurity.SelectedValue;

            int curCurrency = curConn.Return_Int("SELECT IdCurrency FROM NESTDB.dbo.Tb001_Securities (nolock) WHERE IdSecurity=" + Id_Ticker1);

            switch (Id_Fund)
            {
                case 5:
                case 6:
                    if (curCurrency == 900) { Id_Account = 1046; Id_Ticker2 = 1844; }
                    if (curCurrency == 1042) { Id_Account = 1060; Id_Ticker2 = 5791; }
                    break;
                case 11:
                    if (curCurrency == 900) { Id_Account = 1073; Id_Ticker2 = 1844; }
                    break;
                case 17:
                    if (curCurrency == 900) { Id_Account = 1289; Id_Ticker2 = 1844; }
                    break;

                case 39:
                    if (curCurrency == 900) { Id_Account = 1211; Id_Ticker2 = 1844; }
                    break;

                case 60:
                case 61:
                    if (curCurrency == 900) { Id_Account = 1405; Id_Ticker2 = 1844; }
                    break;

                case 50:
                case 51:
                    if (curCurrency == 900) { Id_Account = 1372; Id_Ticker2 = 1844; }
                    break;

                case 55:
                case 56:
                    if (curCurrency == 900) { Id_Account = 1526; Id_Ticker2 = 1844; }
                    break;



                case 41:
                case 42:
                    if (curCurrency == 900) { Id_Account = 1086; Id_Ticker2 = 1844; }
                    if (curCurrency == 1042) { Id_Account = 1148; Id_Ticker2 = 5791; }

                    break;
            }

            double insValue = double.Parse(txtValue.Text);

            if (Id_Account == 0)
            {
                MessageBox.Show("This porfolio cannot have expenses or revenues in that currency!");
                return;
            }

            string Book1 = "0", Section1 = "0";

            try
            {
                if (cmbIdBook.SelectedValue.ToString() != "-1" && cmbIdBook.SelectedValue.ToString() != "0")
                {
                    Book1 = cmbIdBook.SelectedValue.ToString();

                    if (cmbIdSection.SelectedValue.ToString() != "-1" && cmbIdSection.SelectedValue.ToString() != "0")
                    {
                        Section1 = cmbIdSection.SelectedValue.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Select a valid Section");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Select a valid Book");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Cant convert book n' section ID's");
                return;
            }

            if (radExpenses.Checked && Section1 != "0" && Book1 != "0")
            {
                SQLString = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                    " VALUES ( 60,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "','" + dtpTradeDate.Value.ToString("yyyyMMdd") + "'," + Id_Account + "," + Id_Ticker1 + "," + Book1 + "," + Section1 + ",0," + Id_Account + "," + Id_Ticker2 + ",5,1," + insValue.ToString().Replace(".", "").Replace(",", ".") + ") ;";
            }

            if (radRevenues.Checked && Section1 != "0" && Book1 != "0")
            {
                SQLString = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                    " VALUES ( 70,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "','" + dtpTradeDate.Value.ToString("yyyyMMdd") + "'," + Id_Account + "," + Id_Ticker1 + "," + Book1 + "," + Section1 + ",0," + Id_Account + "," + Id_Ticker2 + ",5,1," + insValue.ToString().Replace(".", "").Replace(",", ".") + ") ;";
            }
            if (SQLString != "")
            {
                int retorno = curConn.ExecuteNonQuery(SQLString, 1);
                if (retorno != 0 && retorno != 99)
                {
                    MessageBox.Show("Inserted!");
                }
                else
                {
                    MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Carrega_Grid();
                txtValue.Text = "";
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

        public void CarregaPort()
        {
            LiveDLL.FormUtils.LoadCombo(this.cmbFund, "Select Id_Portfolio,Port_Name from  Tb002_Portfolios where Id_Port_Type=1 and Discountinued <> 1 UNION ALL SELECT '-1', 'All Portfolios' ", "Id_Portfolio", "Port_Name", -1);
        }

        public void CarregaBook()
        {
            LiveDLL.FormUtils.LoadCombo(this.cmbIdBook, "SELECT Id_Book, Book FROM [NESTDB].[dbo].[Tb400_Books] WHERE Id_Book <> 0 ORDER BY Book", "Id_Book", "Book");
        }

        public void Carrega_Section()
        {
            int valor;
            try
            {
                if (curUtils.IsNumeric(cmbIdBook.SelectedValue))
                {
                    valor = Convert.ToInt32(cmbIdBook.SelectedValue);
                    //SubEstrategia
                    LiveDLL.FormUtils.LoadCombo(this.cmbIdSection, "Select Id_Section,Section from VW_Book_Strategies where Id_Book =" + valor + " AND Id_Section <> 0 ORDER BY Section", "Id_Section", "Section", 99);
                }
                else
                {
                    LiveDLL.FormUtils.LoadCombo(this.cmbIdSection, "Select Id_Section,Section from VW_Book_Strategies where Id_Section <> 0 ORDER BY Section", "Id_Section", "Section", 99);
                }
            }
            catch (Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());
            }
        }

        private void Carrega_Grid()
        {
            if (cmbFund.SelectedValue.GetType() == typeof(DataRowView))
            {
                return;
            }

            string SQLString;

            DataTable tablep = new DataTable();

            dgExpenses.Columns.Clear();

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());
            /*
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
            */
            SQLString = "SELECT * FROM NESTDB.dbo.FCN_Return_Expenses() Where [Trade Date]>='" + dtpIniDate.Value.ToString("yyyyMMdd") + "' AND [Trade Date]<='" + dtpEndDate.Value.ToString("yyyyMMdd") + "'";

            if (Id_Fund != -1)
            {
                SQLString = SQLString + " AND Idportfolio=" + Id_Fund;
            }

            SQLString = SQLString + " ORDER BY [Trade Date]";

            using (newNestConn curConn = new newNestConn())
            {
                tablep = curConn.Return_DataTable(SQLString);

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

                curUtils.SetColumnStyle(dgExpenses, 1);
            }
        }

        private void txtValue_Leave(object sender, EventArgs e)
        {
            if (curUtils.IsNumeric(this.txtValue.Text))
            {
                decimal Cash = Convert.ToDecimal(txtValue.Text);
                this.txtValue.Text = Cash.ToString("##,##0.00");
            }
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
                    string TransactionId = dgExpenses.GetRowCellValue(dgExpenses.FocusedRowHandle, "TransactionId").ToString();
                    string TrType = dgExpenses.GetRowCellValue(dgExpenses.FocusedRowHandle, "TrType").ToString();

                    // ESSA PARTE NAO PERMITE QUE O USER DELETE LINHAS BROKERAGE DA NOTA CASO HAJA ERRO, DESCOMENTAR O IF E O ELSE
                    if (TrType != "Brokerage")
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to delete this Expense entry?", "Delete Expense", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            string SQLString;
                            SQLString = "DELETE FROM Tb700_Transactions WHERE Transaction_Id=" + TransactionId;
                            using (newNestConn curConn = new newNestConn())
                            {
                                curConn.ExecuteNonQuery(SQLString, 1);
                            }
                            Carrega_Grid();
                        }
                    }
                    else
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to delete this Expense entry?", "Delete Expense", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            using (newNestConn curConn = new newNestConn())
                            {
                                curConn.ExecuteNonQuery("DELETE FROM [NESTDB].[dbo].[Tb719_Brokerage] WHERE IdBrokerage=" + TransactionId, 1);
                            }

                            Carrega_Grid();
                        }

                        // MessageBox.Show("This transaction cannot be deleted because it is a brokerage expense. Delete it in the brokerage module.", "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgExpenses_EndGrouping(object sender, EventArgs e)
        {
            dgExpenses.GroupSummary.Add(SummaryItemType.Sum, "Cash", dgExpenses.Columns["Cash"]);
            ((GridSummaryItem)dgExpenses.GroupSummary[dgExpenses.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
        }

        private void dgExpenses_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            foreach (GridSummaryItem si in view.GroupSummary)
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                    items.Add(si);
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            info.GroupText = /* view.GroupedColumns[level].Caption + ": " +*/ view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);
            foreach (GridGroupSummaryItem item in items)
            {
                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                // calculate summary text and boundaries
                string text = item.GetDisplayText(values[item], false);
                SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                int width = Convert.ToInt32(sz.Width) + 1;
                rect.X += rect.Width - width - 2;
                rect.Width = width;
                rect.Y = e.Bounds.Y;
                rect.Height = e.Bounds.Height - 2;

                // draw a summary values
                e.Appearance.DrawString(e.Cache, text, rect);
            }

            // disable default painting of the group row
            e.Handled = true;
        }

        private void dgExpenses_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgExpenses);

        }

        private void dgExpenses_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgExpenses);
        }

        private void ShowColumnSelector() { ShowColumnSelector(true, dgExpenses); }

        bool show = false;
        private void ShowColumnSelector(bool showForm, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            if (show)
            {
                if (showForm) Nome_Grid.ColumnsCustomization();
            }
            else
            {
                if (showForm) Nome_Grid.DestroyCustomization();
            }
        }
        private Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return Rectangle.Empty;
        }

        private void radExpenses_CheckedChanged(object sender, EventArgs e)
        {
            UpdateType();
        }

        private void UpdateType()
        {
            if (radExpenses.Checked) LiveDLL.FormUtils.LoadCombo(cmbIdSecurity, "SELECT IdSecurity, SecName FROM NESTDB.dbo.Tb001_Securities (nolock) WHERE IdInstrument=17", "IdSecurity", "SecName");
            if (radRevenues.Checked) LiveDLL.FormUtils.LoadCombo(cmbIdSecurity, "SELECT IdSecurity, SecName FROM NESTDB.dbo.Tb001_Securities (nolock) WHERE IdInstrument=24", "IdSecurity", "SecName");
        }

        private void radRevenues_CheckedChanged(object sender, EventArgs e)
        {
            UpdateType();
        }

        private void cmbFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
            CarregaBook();
        }

        private void cmbIdBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Section();
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dtpIniDate_ValueChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }
    }
}