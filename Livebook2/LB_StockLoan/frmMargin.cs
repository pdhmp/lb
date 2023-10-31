using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraExport;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using LiveDLL;

namespace LiveBook
{
    public partial class frmMargin : LBForm
    {
        newNestConn curConn = new newNestConn();
        string SQLString = "";

        RefreshHelper hlprMargin;

        public frmMargin()
        {
            InitializeComponent();

            dtGridMargin.LookAndFeel.UseDefaultLookAndFeel = false;
            dtGridMargin.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtGridMargin.LookAndFeel.SetSkinStyle("Blue");

            dtGridMarginTransactions.LookAndFeel.UseDefaultLookAndFeel = false;
            dtGridMarginTransactions.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtGridMarginTransactions.LookAndFeel.SetSkinStyle("Blue");

            dtGridReceivedFiles.LookAndFeel.UseDefaultLookAndFeel = false;
            dtGridReceivedFiles.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtGridReceivedFiles.LookAndFeel.SetSkinStyle("Blue");

            dtGridNotRecMargin.LookAndFeel.UseDefaultLookAndFeel = false;
            dtGridNotRecMargin.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtGridNotRecMargin.LookAndFeel.SetSkinStyle("Blue");
        }
        private void frmMargin_Load(object sender, EventArgs e) { LoadPositions(); }

        private void LoadPositions()
        {
            hlprMargin = new RefreshHelper(gridViewMargin, "Id_Portfolio");
            hlprMargin.SaveViewInfo();
            int curRow = gridViewMargin.FocusedRowHandle;

            gridViewMargin.Columns.Clear();

            SQLString = "EXEC dbo.Proc_Margin_Load_Positions";

            dtGridMargin.DataSource = curConn.Return_DataTable(SQLString);

            // Renew Button
            gridViewMargin.Columns.AddField("Update");
            gridViewMargin.Columns["Update"].VisibleIndex = 0;
            gridViewMargin.Columns["Update"].Width = 60;
            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Update";
            dtGridMargin.RepositoryItems.Add(item5);
            gridViewMargin.Columns["Update"].ColumnEdit = item5;
            gridViewMargin.Columns["Update"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            gridViewMargin.OptionsBehavior.Editable = false;
            gridViewMargin.Columns["Update"].Visible = true;

            gridViewMargin.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMargin.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            gridViewMargin.Columns["Quantity_CBLC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMargin.Columns["Quantity_CBLC"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            gridViewMargin.Columns["DIFF"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMargin.Columns["DIFF"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            gridViewMargin.Columns["MarginValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMargin.Columns["MarginValue"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            gridViewMargin.Columns["FinAmount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMargin.Columns["FinAmount"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            gridViewMargin.Columns["LastPx"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMargin.Columns["LastPx"].DisplayFormat.FormatString = "0.00;(0.00)";

            gridViewMargin.Columns["Discount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMargin.Columns["Discount"].DisplayFormat.FormatString = "0.00%;(0.00%)";

            curUtils.SetColumnStyle(gridViewMargin, 1);
            hlprMargin.LoadViewInfo();
            gridViewMargin.FocusedRowHandle = curRow;
        }
        private void LoadTransactions()
        {
            gridViewMarginTransactions.Columns.Clear();

            SQLString = "EXEC dbo.Proc_Margin_Load_Transactions";

            dtGridMarginTransactions.DataSource = curConn.Return_DataTable(SQLString);

            // Cancel Button
            gridViewMarginTransactions.Columns.AddField("Cancel");
            gridViewMarginTransactions.Columns["Cancel"].VisibleIndex = 0;
            gridViewMarginTransactions.Columns["Cancel"].Width = 60;
            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Cancel";
            dtGridMarginTransactions.RepositoryItems.Add(item3);
            gridViewMarginTransactions.Columns["Cancel"].ColumnEdit = item3;
            gridViewMarginTransactions.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            gridViewMarginTransactions.OptionsBehavior.Editable = false;
            gridViewMarginTransactions.Columns["Cancel"].Visible = true;

            gridViewMarginTransactions.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewMarginTransactions.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            gridViewMarginTransactions.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridViewMarginTransactions.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
            gridViewMarginTransactions.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            curUtils.SetColumnStyle(gridViewMarginTransactions, 1);
        }
        private void LoadReceivedFiles()
        {
            gridViewReceivedFiles.Columns.Clear();

            SQLString = " EXEC dbo.Proc_Margin_Load_ReceivedFiles ";

            dtGridReceivedFiles.DataSource = curConn.Return_DataTable(SQLString);

            gridViewReceivedFiles.Columns["LastReceived"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridViewReceivedFiles.Columns["LastReceived"].DisplayFormat.FormatString = "dd-MMM-yy";
            gridViewReceivedFiles.Columns["LastReceived"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            gridViewReceivedFiles.Columns["Nome"].BestFit();
            gridViewReceivedFiles.Columns["Port_Name"].BestFit();
            gridViewReceivedFiles.Columns["LastReceived"].BestFit();
            gridViewReceivedFiles.Columns["NoDeposits"].BestFit();
        }
        private void LoadNotReceivedLoans()
        {
            gridViewNotRecMargin.Columns.Clear();

            SQLString = "  exec dbo.Proc_Margin_Load_NotReceivedLoans ";

            dtGridNotRecMargin.DataSource = curConn.Return_DataTable(SQLString);

            // Close Button
            gridViewNotRecMargin.Columns.AddField("Close");
            gridViewNotRecMargin.Columns["Close"].VisibleIndex = 0;
            gridViewNotRecMargin.Columns["Close"].Width = 60;
            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Close";
            dtGridNotRecMargin.RepositoryItems.Add(item3);
            gridViewNotRecMargin.Columns["Close"].ColumnEdit = item3;
            gridViewNotRecMargin.Columns["Close"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            gridViewNotRecMargin.OptionsBehavior.Editable = false;
            gridViewNotRecMargin.Columns["Close"].Visible = true;

            // Cancel Button
            gridViewNotRecMargin.Columns.AddField("Cancel");
            gridViewNotRecMargin.Columns["Cancel"].VisibleIndex = 1;
            gridViewNotRecMargin.Columns["Cancel"].Width = 60;
            RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
            item4.Buttons[0].Tag = 1;
            item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item4.Buttons[0].Caption = "Cancel";
            dtGridMargin.RepositoryItems.Add(item4);
            gridViewNotRecMargin.Columns["Cancel"].ColumnEdit = item4;
            gridViewNotRecMargin.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            gridViewNotRecMargin.OptionsBehavior.Editable = false;
            gridViewNotRecMargin.Columns["Cancel"].Visible = true;

            gridViewNotRecMargin.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            gridViewNotRecMargin.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            gridViewNotRecMargin.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gridViewNotRecMargin.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
            gridViewNotRecMargin.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            curUtils.SetColumnStyle(gridViewNotRecMargin, 1);


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

        private void tabLoans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabLoans.SelectedIndex == 0)
            {
                LoadPositions();
            }

            if (tabLoans.SelectedIndex == 1)
            {
                LoadTransactions();
            }
            if (tabLoans.SelectedIndex == 2)
            {
                LoadReceivedFiles();
                LoadNotReceivedLoans();
            }
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

        private void btnExpand_Click(object sender, EventArgs e)
        {
            gridViewMargin.ExpandAllGroups();
        }
        private void btnCollapse_Click(object sender, EventArgs e)
        {
            gridViewMargin.CollapseAllGroups();
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                string user = Environment.UserName.ToString();
                string Loca_Machine = Environment.MachineName.ToString();
                string fileName_Log = "T:\\Log\\Reports\\Margin_Id_LB_" + LiveDLL.NUserControl.Instance.User_Id + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";

                ExportTo(new ExportXlsProvider(fileName_Log));
                ExportTo(new ExportXlsProvider(fileName));
                OpenFile(fileName);
            }
        }
        private void btnInsert_Click(object sender, EventArgs e)
        {
            frmInsertMargin InsereMargem = new frmInsertMargin();
            InsereMargem.ShowDialog();
            LoadPositions();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tabLoans_SelectedIndexChanged(this, e);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export To " + title;
            dlg.FileName = "Position";
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }
        private void ExportTo(IExportProvider provider)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.FindForm().Refresh();
            BaseExportLink link = gridViewMargin.CreateExportLink(provider);
            if (tabLoans.SelectedIndex == 0)
            {
                link = gridViewMargin.CreateExportLink(provider);
            }
            if (tabLoans.SelectedIndex == 1)
            {
                link = gridViewMarginTransactions.CreateExportLink(provider);
            }
            (link as GridViewExportLink).ExpandAll = false;
            link.ExportTo(true);
            provider.Dispose();

            Cursor.Current = currentCursor;
        }
        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch (Exception e)
                {
                    curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgMargin_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (gridViewMargin.Columns.Count > 0 && e.RowHandle >= 0)
            {
                if (gridViewMargin.GetRowCellValue(e.RowHandle, "Action").ToString() == "Deposit" || gridViewMargin.GetRowCellValue(e.RowHandle, "Action").ToString() == "Withdraw")
                {
                    e.Appearance.BackColor = Color.FromArgb(250, 220, 216);
                }
                if (gridViewMargin.GetRowCellValue(e.RowHandle, "Action").ToString() == "Not Received")
                {
                    e.Appearance.BackColor = Color.LightGray;
                }
            }
        }
        private void dgMargin_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

            if (Column_Name == "Update")
            {
                if (gridViewMargin.FocusedRowHandle != null)
                {
                    double tempDB = 0;
                    double tempCBLC = 0;
                    double tempQuant = 0;

                    if (gridViewMargin.GetRowCellValue(gridViewMargin.FocusedRowHandle, "Quantity").ToString() != "") { tempDB = Convert.ToInt32(gridViewMargin.GetRowCellValue(gridViewMargin.FocusedRowHandle, "Quantity")); };
                    if (gridViewMargin.GetRowCellValue(gridViewMargin.FocusedRowHandle, "Quantity_CBLC").ToString() != "") { tempCBLC = Convert.ToInt32(gridViewMargin.GetRowCellValue(gridViewMargin.FocusedRowHandle, "Quantity_CBLC")); };

                    tempQuant = tempCBLC - tempDB;
                    if (tempQuant != 0)
                    {

                        frmInsertMargin InsereMargem = new frmInsertMargin();

                        InsereMargem.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                        InsereMargem.Left = this.Left + 10;
                        InsereMargem.Text = "Update Margin";
                        InsereMargem.Carrega_Combo();
                        InsereMargem.cmbFundo.SelectedValue = gridViewMargin.GetRowCellValue(gridViewMargin.FocusedRowHandle, "Id_Portfolio");
                        InsereMargem.Carrega_Account();
                        InsereMargem.cmbCorretora.SelectedValue = gridViewMargin.GetRowCellValue(gridViewMargin.FocusedRowHandle, "Id_Account");
                        InsereMargem.cmbTicker.SelectedValue = gridViewMargin.GetRowCellValue(gridViewMargin.FocusedRowHandle, "Id_Ticker");
                        InsereMargem.txtQtd.Text = gridViewMargin.GetRowCellValue(gridViewMargin.FocusedRowHandle, "Quantity").ToString();


                        if (tempQuant > 0)
                        {
                            InsereMargem.rdDep.Checked = true;
                            InsereMargem.txtQtd.Text = tempQuant.ToString();
                        }
                        else
                        {
                            InsereMargem.rdRet.Checked = true;
                            InsereMargem.txtQtd.Text = (-tempQuant).ToString();
                        }
                        InsereMargem.ShowDialog();
                        LoadPositions();
                    }
                }
            }
        }
        private void dgMarginTransactions_DoubleClick(object sender, EventArgs e)
        {
            int resposta;

            if (gridViewMarginTransactions.RowCount > 0)
            {
                int Margin_Id = (int)gridViewMarginTransactions.GetRowCellValue(gridViewMarginTransactions.FocusedRowHandle, "Id_Margin");
                string Column_Name = gridViewMarginTransactions.FocusedColumn.Caption.ToString();

                if (Column_Name == "Cancel")
                {
                    if (Margin_Id != 0)
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this Margin transaction?", "Cancel Stock Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            string SQLString;
                            SQLString = "UPDATE dbo.Tb712_MarginAssets SET Status=4 WHERE Id_Margin=" + Margin_Id;
                            curConn.ExecuteNonQuery(SQLString, 1);
                            LoadTransactions();
                        }
                    }
                }
            }
        }
        private void dgStockLoan_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            info.GroupText = view.GroupedColumns[level].Caption + ": " + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
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
            //dgPositions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
        }
        private void dgStockLoan_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn weightColumn = null;

            if (e.Item == view.GroupSummary[1])
            {
                weightColumn = gridViewMargin.Columns["MarginValue"];
            }
            else
            {
                weightColumn = gridViewMargin.Columns["MarginValue"];
            }

            if (weightColumn == null)
                return;

            switch (e.SummaryProcess)
            {
                case CustomSummaryProcess.Start:
                    {
                        e.TotalValue = new WeightedAverageCalculator();
                        break;
                    }
                case CustomSummaryProcess.Calculate:
                    {
                        double size = 0;
                        double weight = 0;

                        if (e.FieldValue.ToString() != "") { size = Convert.ToDouble(e.FieldValue); };

                        if (((GridView)sender).GetRowCellValue(e.RowHandle, weightColumn).ToString() != "") { weight = Convert.ToDouble(((GridView)sender).GetRowCellValue(e.RowHandle, weightColumn)); };

                        ((WeightedAverageCalculator)e.TotalValue).Add(weight, size);
                        break;
                    }
                case CustomSummaryProcess.Finalize:
                    {
                        e.TotalValue = ((WeightedAverageCalculator)e.TotalValue).Value;
                        break;
                    }
            }
        }
        private void dgStockLoan_EndGrouping(object sender, EventArgs e)
        {

            gridViewMargin.GroupSummary.Clear();

            gridViewMargin.GroupSummary.Add(SummaryItemType.Sum, "Quantity", gridViewMargin.Columns["Quantity"], "{0:#,#0}");

            gridViewMargin.GroupSummary.Add(SummaryItemType.Custom, "Discount", gridViewMargin.Columns["Discount"], "{0:0.00%}");
            gridViewMargin.GroupSummary.Add(SummaryItemType.Sum, "FinAmount", gridViewMargin.Columns["Fin_Amount_Open"], "{0:#,#0}");
            gridViewMargin.GroupSummary.Add(SummaryItemType.Sum, "MarginValue", gridViewMargin.Columns["Fin_Amount_Now"], "{0:#,#0}");
            //dgMargin.GroupSummary.Add(SummaryItemType.Custom, "Loan_Rate", dgMargin.Columns["Loan_Rate"], "{0:0.00%}");

            //dgMargin.GroupSummary.Add(SummaryItemType.Sum, "Cost_Contrib", dgMargin.Columns["Cost_Contrib"], "{0:0.00%}");

        }

        private sealed class WeightedAverageCalculator
        {
            private double _sumOfProducts;
            private double _totalWeight;

            public void Add(double weight, double size)
            {
                _sumOfProducts += weight * size;
                _totalWeight += weight;
            }

            public double Value
            {
                get { return _totalWeight == 0 ? 0 : _sumOfProducts / _totalWeight; }
            }
        }
    }
}