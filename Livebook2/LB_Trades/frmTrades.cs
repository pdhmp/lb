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
using LiveDLL;

namespace LiveBook
{
    public partial class frmTrades : LBForm
    {
        public frmTrades()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtgTrade.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgTrade.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgTrade.LookAndFeel.SetSkinStyle("Blue");

            if (LiveDLL.NUserControl.Instance.User_Id == 1)
            {
                LiveDLL.FormUtils.LoadCombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type=1", "Id_Portfolio", "Port_Name", 1);
            }
            else
            {
                LiveDLL.FormUtils.LoadCombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name from  Tb002_Portfolios where Id_Port_Type=1", "Id_Portfolio", "Port_Name", 1);
            }

        }

        public void Carrega_Grid()
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString;

                SQLString = "SELECT * FROM dbo.VW_LB_Trades " +
                            " Where [Trade Date] >='" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") + "' and [Trade Date]<= '" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") +
                            "' and [Id Trade Status]<>4 and [Id Portfolio]=" + cmbChoosePortfolio.SelectedValue.ToString();


                DataTable tablet = curConn.Return_DataTable(SQLString);
                dtgTrade.DataSource = tablet;

                curUtils.SetColumnStyle(dgTradesRel, 2, "Trade Quantity");

                dgTradesRel.Columns["Trade Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgTradesRel.Columns["Trade Quantity"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";
            }
        }

        private void dgTradesRel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        public void Carrega_Grid_Rel()
        {
            
            string SQLString;
            
            SQLString = "Select [Id Trade],[Id Order],Ticker,[Trade Quantity],[Trade Price],[Trade Cash],Rebate,Login,Strategy," +
                        " [Sub Strategy],[Round Lot],Portfolio,[Ticker Currency], [Status Trade],[Trade Date]," +
                        " [Id Ticker],Broker from VW_LB_Trades" +
                        " Where [Trade Date] >='" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") +
                        "' and [Trade Date]<= '" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") + "' and [Status Trade]<>4" +
                        " and [Id Portfolio]=" + cmbChoosePortfolio.SelectedValue.ToString() ;
            
            using (newNestConn curConn = new newNestConn())
            {
                DataTable tablet = curConn.Return_DataTable(SQLString);

                gdTRadesREL.DataSource = tablet;

                curUtils.SetColumnStyle(GTradesRel, 2);
            }
        }

         private void cmdrefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgTradesRel_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgTradesRel);

        }

        private void dgTradesRel_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgTradesRel);

        }

        private void dgTradesRel_EndGrouping(object sender, EventArgs e)
        {
            dgTradesRel.ExpandAllGroups();

            dgTradesRel.GroupSummary.Add(SummaryItemType.Sum, "Trade Quantity", dgTradesRel.Columns["Trade Quantity"]);
            ((GridSummaryItem)dgTradesRel.GroupSummary[dgTradesRel.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgTradesRel.GroupSummary.Add(SummaryItemType.Sum, "Trade Price", dgTradesRel.Columns["Trade Price"]);
            ((GridSummaryItem)dgTradesRel.GroupSummary[dgTradesRel.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgTradesRel.GroupSummary.Add(SummaryItemType.Sum, "Trade Cash", dgTradesRel.Columns["Trade Cash"]);
            ((GridSummaryItem)dgTradesRel.GroupSummary[dgTradesRel.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
        }

        private void dgTradesRel_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgTradesRel);

        }

        private void dgTradesRel_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgTradesRel);

        }
        private void ShowColumnSelector() { ShowColumnSelector(true, dgTradesRel); }
        
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
                string fileName_Log = "T:\\Log\\Reports\\Trades_Id_LB_" + LiveDLL.NUserControl.Instance.User_Id + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Portfolio_" + cmbChoosePortfolio.Text + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";
                ExportTo(new ExportXlsProvider(fileName_Log), dgTradesRel);

                ExportTo(new ExportXlsProvider(fileName),dgTradesRel);
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
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Text Document", "Text Files|*.txt");
            if (fileName != "")
            {
                ExportTo(new ExportTxtProvider(fileName),dgTradesRel);
                OpenFile(fileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Carrega_Grid_Rel();

            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                string user = Environment.UserName.ToString();
                string Loca_Machine = Environment.MachineName.ToString();
                string fileName_Log = "T:\\Log\\Reports\\Trades_Id_LB_" + LiveDLL.NUserControl.Instance.User_Id + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Portfolio_" + cmbChoosePortfolio.Text + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";
                ExportTo(new ExportXlsProvider(fileName_Log), GTradesRel);

                ExportTo(new ExportTxtProvider(fileName), GTradesRel);
                OpenFile(fileName);
            }
        }

        private void Valida_Colunas(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            if (curUtils.Verifica_Acesso(1) || curUtils.Verifica_Acesso(2))
            {
                dgTradesRel.Columns["Edit"].Visible = true;
                dgTradesRel.Columns["Edit"].VisibleIndex = 0;
                dgTradesRel.Columns["Cancel"].Visible = true;
                dgTradesRel.Columns["Cancel"].VisibleIndex = 1;
            }
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

               private void label1_Click(object sender, EventArgs e)
               {

               }
 

    }
}