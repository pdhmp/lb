using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraExport;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using LiveDLL;



namespace LiveBook
{
    public partial class frmRebateClients : LBForm
    {
        int ExpandCounter = 0;
        
        public frmRebateClients()
        {
            InitializeComponent();
        }

        private void frmRebateClients_Load(object sender, EventArgs e)
        {
            //lblCopy.Parent = (Control)dtgAttribution;

            dtgRebates.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgRebates.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgRebates.LookAndFeel.SetSkinStyle("Blue");

            LiveDLL.FormUtils.LoadCombo(this.CmbDate, "Select distinct(RefDate) as RefDate FROM NESTIMPORT.dbo.Tb_Mellon_Investors_Perf order by RefDate ", "RefDate", "RefDate", 99);
        }

        public void Carrega_Grid()
        {
            using (newNestConn curConn = new newNestConn())
            {
                dgRebates.GroupSummary.Clear();
                dgRebates.Columns.Clear();
                dtgRebates.Refresh();

                //  dtpHistDate.Value = endDate;

                string SQLString = "Select * from dbo.FCN_COM_Contacts_Rebates('" + Convert.ToDateTime(CmbDate.SelectedValue).ToString("yyyyMMdd") + "')";

                dtgRebates.DataSource = curConn.Return_DataTable(SQLString);

            }

            dgRebates.Columns["AdminTax"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRebates.Columns["AdminTax"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRebates.Columns["RebateAdm"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRebates.Columns["RebateAdm"].DisplayFormat.FormatString = "P2";

            dgRebates.Columns["RebatePerf"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRebates.Columns["RebatePerf"].DisplayFormat.FormatString = "P2";

            dgRebates.Columns["PercentageMellonFee"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRebates.Columns["PercentageMellonFee"].DisplayFormat.FormatString = "P2";

            dgRebates.Columns["MellonTax"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRebates.Columns["MellonTax"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRebates.Columns["RebateValueAdm"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRebates.Columns["RebateValueAdm"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRebates.Columns["RebateValuePerf"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRebates.Columns["RebateValuePerf"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRebates.Columns["AppropPerformanceFee"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRebates.Columns["AppropPerformanceFee"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRebates.Columns["AppropPerformanceFee"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRebates.Columns["AppropPerformanceFee"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            curUtils.SetColumnStyle(dgRebates, 1);

            dgRebates.GroupSummary.Add(SummaryItemType.Sum, "RebateValueAdm", dgRebates.Columns["RebateValueAdm"]);
            ((GridSummaryItem)dgRebates.GroupSummary[dgRebates.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgRebates.GroupSummary.Add(SummaryItemType.Sum, "AdminTax", dgRebates.Columns["AdminTax"]);
            ((GridSummaryItem)dgRebates.GroupSummary[dgRebates.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgRebates.GroupSummary.Add(SummaryItemType.Sum, "MellonTax", dgRebates.Columns["MellonTax"]);
            ((GridSummaryItem)dgRebates.GroupSummary[dgRebates.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgRebates.GroupSummary.Add(SummaryItemType.Sum, "AppropPerformanceFee", dgRebates.Columns["AppropPerformanceFee"]);
            ((GridSummaryItem)dgRebates.GroupSummary[dgRebates.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgRebates.GroupSummary.Add(SummaryItemType.Sum, "PerformanceFeeRedemption", dgRebates.Columns["PerformanceFeeRedemption"]);
            ((GridSummaryItem)dgRebates.GroupSummary[dgRebates.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgRebates.GroupSummary.Add(SummaryItemType.Sum, "RebateValuePerf", dgRebates.Columns["RebateValuePerf"]);
            ((GridSummaryItem)dgRebates.GroupSummary[dgRebates.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";


        }

        private void dgAttribution_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            info.GroupText = view.GroupedColumns[level].Caption + "" + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
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

                if ((rect.X + rect.Width / 2) > view.Columns[4].Width)
                {
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
            }

            // disable default painting of the group row
            e.Handled = true;
            //dgPositions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;

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

        private GridSummaryItem GetGroupByName(GridView view, string Groupname)
        {
            foreach (GridSummaryItem curItem in view.GroupSummary)
            {
                if (curItem.FieldName == Groupname)
                {
                    return curItem;
                }
            }
            return null;
        }

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            ExpandGroups();
        }

        private void ExpandGroups()
        {
            dgRebates.CollapseAllGroups();

            ExpandCounter++;

            if (ExpandCounter > dgRebates.GroupCount) { ExpandCounter = 0; };

            for (int i = -1; ; i--)
            {
                if (!dgRebates.IsValidRowHandle(i)) return;
                if (dgRebates.GetRowLevel(i) < ExpandCounter)
                {
                    dgRebates.SetRowExpanded(i, true);
                }
            }
        }

        private void cmdExpand_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgRebates.ExpandAllGroups();
                ExpandCounter = dgRebates.GroupCount;
            }
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            dgRebates.CollapseAllGroups();
            ExpandCounter = 0;
        }

        private void cmdCollapse_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgRebates.CollapseAllGroups();
                ExpandCounter = 0;
            }
        }

        private void dgRebates_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(tempGrid);
        }

        private void dgRebates_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(tempGrid);
        }

        private void lblImportFile_DragDrop(object sender, DragEventArgs e)
        {
            bool flagImport = false;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            e.Effect = DragDropEffects.None;

            foreach (string curFile in files)
            {
                if (curFile.Contains(".txt"))
                {
                    Mellon curMellonImport = new Mellon();
                    curMellonImport.ImportFileMellonPerfAdm(curFile);
                    flagImport = true;
                }

                if (flagImport == false)
                {
                    System.Windows.Forms.MessageBox.Show("File:\r\n\r\n" + curFile + "\r\n\r\nnot a valid file.", "Import failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("File Imported!");
                }
            }
        }

        private void lblImportFile_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void lblImportFile_Click(object sender, EventArgs e)
        {

        }

        private void cmdTxMellon_Click(object sender, EventArgs e)
        {
            frmInsertTxMellon TxMellon = new frmInsertTxMellon();
            TxMellon.ShowDialog();

        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                string user = Environment.UserName.ToString();
                string Loca_Machine = Environment.MachineName.ToString();
                string fileName_Log = "T:\\Log\\Reports\\Position_Id_LB_" + LiveDLL.NUserControl.Instance.User_Id + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Rebate_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";
                ExportTo(new ExportXlsProvider(fileName_Log));

                ExportTo(new ExportXlsProvider(fileName));

                OpenFile(fileName);
            }
        }
        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export To " + title;
            dlg.FileName = "Rebate";
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }
        private void ExportTo(IExportProvider provider)
        {
            IExportProvider Log_Provider;
            Log_Provider = provider;

            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.FindForm().Refresh();
            BaseExportLink link = dgRebates.CreateExportLink(provider);
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

        private void Copy_Click(object sender, EventArgs e)
        {

            int groupRowHandle = -1;
            string CopyString = "";
            string[] printTitles = new string[1];

            while (dgRebates.IsValidRowHandle(groupRowHandle))
            {
                //if (dgAttribution.IsRowVisible(groupRowHandle) == RowVisibleState.Visible)
                {
                    int row = dgRebates.GetDataRowHandleByGroupRowHandle(groupRowHandle);
                    int tempLevel = dgRebates.GetRowLevel(groupRowHandle);

                    string tempCopy = new string(' ', tempLevel * 4);

                    tempCopy = tempCopy + dgRebates.GetRowCellDisplayText(row, dgRebates.GroupedColumns[tempLevel]);

                    GridView view = dgRebates as GridView;
                    Hashtable values = view.GetGroupSummaryValues(groupRowHandle);

                    string[] printValues = new string[dgRebates.Columns.Count];
                    printTitles = new string[dgRebates.Columns.Count];

                    int minColumn = 9999;

                    foreach (GridGroupSummaryItem curKey in values.Keys)
                    {
                        if (curKey.ShowInGroupColumnFooter.VisibleIndex < minColumn) minColumn = curKey.ShowInGroupColumnFooter.VisibleIndex;
                    }

                    foreach (GridGroupSummaryItem curKey in values.Keys)
                    {
                        int poscolumn = curKey.ShowInGroupColumnFooter.VisibleIndex;
                        //KeyValuePair<GridGroupSummaryItem, double> curItem = values;
                        double curValue = Convert.ToDouble(values[curKey]);
                        printValues[poscolumn - minColumn] = curValue.ToString();
                        printTitles[poscolumn - minColumn] = curKey.ShowInGroupColumnFooter.ToString();
                    }

                    for (int i = 0; i < printValues.Length; i++)
                    {
                        tempCopy = tempCopy + '\t' + printValues[i];
                    }


                    Console.WriteLine(tempCopy);
                    CopyString = CopyString + tempCopy + "\r\n";
                }

                groupRowHandle--;
            }

            string tempTitles = "";
            for (int i = 0; i < printTitles.Length; i++)
            {
                tempTitles = tempTitles + '\t' + printTitles[i];
            }

            CopyString = tempTitles + "\r\n" + CopyString;

            Clipboard.SetDataObject(CopyString, true);
        }

        private void CmdLoad_Click(object sender, EventArgs e)
        {
            Carrega_Grid();

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            int x = 0;

            using (newNestConn curConn = new newNestConn())
            {
                x = curConn.Return_Int("SELECT COUNT (*) FROM NESTIMPORT.dbo.Tb_Mellon_Investors_Perf WHERE RefDate = '" + Convert.ToDateTime(CmbDate.SelectedValue).ToString("yyyy-MM-dd") + "'");
                if (x > 0)
                {
                    DialogResult userConfirmation = MessageBox.Show("Delete the file \r\nDate: " + Convert.ToDateTime(CmbDate.SelectedValue).ToString("dd-MM-yyyy") + "\r\n\r\nDo you confirm?", "Delete File", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (userConfirmation == DialogResult.OK)
                    {
                        curConn.ExecuteNonQuery("DELETE FROM NESTIMPORT.dbo.Tb_Mellon_Investors_Perf WHERE RefDate = '" + Convert.ToDateTime(CmbDate.SelectedValue).ToString("yyyy-MM-dd") + "'");
                        MessageBox.Show("File Deleted");
                    }
                }
            }

            Carrega_Grid();
        }


    }
}