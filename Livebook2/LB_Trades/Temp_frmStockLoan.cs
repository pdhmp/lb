using System;
using System.Collections;
using System.Data;
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
using LiveBook.Business;
using LiveDLL;

namespace LiveBook
{
    public partial class TempfrmStockLoan : Form
    {
        newNestConn curConn = new newNestConn();

        public TempfrmStockLoan()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtgTrade.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgTrade.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgTrade.LookAndFeel.SetSkinStyle("Blue");

            LiveDLL.FormUtils.LoadCombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type in (2) UNION ALL SELECT '-1', 'All Portfolios'", "Id_Portfolio", "Port_Name", 1);
        }
        
        Business_Class Negocios = new Business_Class();
        LB_Utils curUtils = new LB_Utils();
        public int Id_User;

        public void Carrega_Grid()
        {
            string SQLString;

            SQLString = "Select B.Port_Name,Broker,A.* FROM " +
                            " dbo.Tb710_Stock_Loans_Import A " +
                            " inner join dbo.VW_PortAccounts B " +
                            " ON A.Id_Account = B.Id_Account " +
                            " Where File_Date>='" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") + 
                            "' and File_Date<='" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") +"' " +
                            " and Id_Port_Type=2 ";

            if (cmbChoosePortfolio.SelectedValue.ToString() != "-1")
            {
                SQLString = SQLString + " and Id_portfolio = " + cmbChoosePortfolio.SelectedValue.ToString();
            
            }

            DataTable tablet = curConn.Return_DataTable(SQLString);

            dtgTrade.DataSource = tablet;

            curUtils.SetColumnStyle(dgTempSotckLoan, 2,"Trade Quantity");
            
            dgTempSotckLoan.Columns["Quantidade"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTempSotckLoan.Columns["Quantidade"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgTempSotckLoan.Columns["Cot_Fechamento"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTempSotckLoan.Columns["Cot_Fechamento"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgTempSotckLoan.Columns["Cot_Liquid"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTempSotckLoan.Columns["Cot_Liquid"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgTempSotckLoan.Columns["Taxa"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTempSotckLoan.Columns["Taxa"].DisplayFormat.FormatString = "P2";
        }

        private void dgTradesRel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
          private void cmdrefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgTradesRel_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgTempSotckLoan);

        }

        private void dgTradesRel_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgTempSotckLoan);

        }

        private void dgTradesRel_EndGrouping(object sender, EventArgs e)
        {
            dgTempSotckLoan.ExpandAllGroups();

            dgTempSotckLoan.GroupSummary.Add(SummaryItemType.Sum, "Trade Quantity", dgTempSotckLoan.Columns["Trade Quantity"]);
            ((GridSummaryItem)dgTempSotckLoan.GroupSummary[dgTempSotckLoan.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgTempSotckLoan.GroupSummary.Add(SummaryItemType.Sum, "Avg Price", dgTempSotckLoan.Columns["Avg Price"]);
            ((GridSummaryItem)dgTempSotckLoan.GroupSummary[dgTempSotckLoan.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgTempSotckLoan.GroupSummary.Add(SummaryItemType.Sum, "Trade Cash", dgTempSotckLoan.Columns["Trade Cash"]);
            ((GridSummaryItem)dgTempSotckLoan.GroupSummary[dgTempSotckLoan.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
        }

        private void dgTradesRel_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgTempSotckLoan);

        }

        private void dgTradesRel_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgTempSotckLoan);

        }
        private void ShowColumnSelector() { ShowColumnSelector(true, dgTempSotckLoan); }
        
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

        private void button4_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                string user = Environment.UserName.ToString();
                string Loca_Machine = Environment.MachineName.ToString();
                string fileName_Log = "T:\\Log\\Reports\\Trades_Id_LB_" + Id_User + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Portfolio_" + cmbChoosePortfolio.Text + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";
                ExportTo(new ExportXlsProvider(fileName_Log), dgTempSotckLoan);

                ExportTo(new ExportXlsProvider(fileName),dgTempSotckLoan);
                OpenFile(fileName);
            }
        }

        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export To " + title;
            dlg.FileName = "Trades Report";
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }
        private void ExportTo(IExportProvider provider,DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.FindForm().Refresh();
            BaseExportLink link = Nome_Grid.CreateExportLink(provider);
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
                catch(Exception e)
                {
                    curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //progressBarControl1.Position = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Text Document", "Text Files|*.txt");
            if (fileName != "")
            {
                ExportTo(new ExportTxtProvider(fileName),dgTempSotckLoan);
                OpenFile(fileName);
            }
        }

          private void GTradesRel_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            //curUtils.Save_Columns(GTradesRel, 1);

        }

          private void dgTradesRel_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
               private Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return Rectangle.Empty;
        }
 

    }
}