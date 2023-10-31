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
    public partial class frmBrokerage : LBForm
    {
        newNestConn curConn = new newNestConn();
        int ExpandCounter = 0;

        public frmBrokerage()
        {
            InitializeComponent();
        }

        private void frmFowards_Load(object sender, EventArgs e)
        {
            dtgBrokerage.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgBrokerage.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgBrokerage.LookAndFeel.SetSkinStyle("Blue");

            dtpIniDate.Value = new DateTime(DateTime.Now.Year, 1, 1);

            radMonthly.CheckedChanged -= new System.EventHandler(radDaily_CheckedChanged);
            radMonthly.Checked = true;
            radMonthly.CheckedChanged += new System.EventHandler(radMonthly_CheckedChanged);

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
            string SQLSInsert;
            
            DataTable tablep = new DataTable();

            dgBrokerage.Columns.Clear();

            string SQLCreateTable = "IF EXISTS(SELECT * FROM tempdb.dbo.sysobjects WHERE ID = OBJECT_ID(N'tempdb..#tempResult')) DROP TABLE #tempResult ; " +
                                    " CREATE TABLE #tempResult (TradeDate datetime, IdAccount int, Broker varchar(40),Portfolio varchar(40), BrokerageType varchar(40), Brokerage float) ";

          curConn.ExecuteNonQuery(SQLCreateTable);

          if (radMonthly.Checked == true)
          {
              SQLSInsert = " INSERT INTO #tempResult " +
              " SELECT DATEADD(dd,-(DAY(TradeDate)-1),TradeDate)TradeDate,IdAccount,Broker,Port_Name,Tipo,Sum(Brokerage) " +
              " FROM NESTDB.dbo.[FCN_REPORT_Brokerage]('" + dtpIniDate.Value.ToString("yyyyMMdd") + "','" + dtpEndDate.Value.ToString("yyyyMMdd") + "') " +
              " GROUP BY DATEADD(dd,-(DAY(TradeDate)-1),TradeDate),IdAccount,Broker,Port_Name,Tipo ";
          }

          else if (radDaily.Checked == true)
          {
              SQLSInsert = " INSERT INTO #tempResult " +
             " SELECT TradeDate,IdAccount,Broker,Port_Name,Tipo,Brokerage " +
             " FROM NESTDB.dbo.[FCN_REPORT_Brokerage]('" + dtpIniDate.Value.ToString("yyyyMMdd") + "','" + dtpEndDate.Value.ToString("yyyyMMdd") + "')";

          }
          else
          {
              SQLSInsert = " INSERT INTO #tempResult " +
             " SELECT DATEADD(mm,-(month(TradeDate)-1),DATEADD(dd,-(day(TradeDate)-1),TradeDate)) TradeDate,IdAccount,Broker,Port_Name,Tipo,Sum(Brokerage) " +
             " FROM NESTDB.dbo.[FCN_REPORT_Brokerage]('" + dtpIniDate.Value.ToString("yyyyMMdd") + "','" + dtpEndDate.Value.ToString("yyyyMMdd") + "') " +
             " GROUP BY DATEADD(mm,-(month(TradeDate)-1),DATEADD(dd,-(day(TradeDate)-1),TradeDate)) ,IdAccount,Broker,Port_Name,Tipo ";
          }

          curConn.ExecuteNonQuery(SQLSInsert);

          string SQLMount;

          SQLMount = "SELECT  TradeDate FROM #tempResult  GROUP BY TradeDate ORDER BY TradeDate";

          string SQLFields="";

          DataTable FieldTable = curConn.Return_DataTable(SQLMount);
          foreach (DataRow row in FieldTable.Rows)
          {
              SQLFields = SQLFields + ",COALESCE(SUM(CASE WHEN TradeDate='" + Convert.ToDateTime(row[0]).ToString("yyyyMMdd") + "' THEN Brokerage ELSE 0 END), 0) AS '" + Convert.ToDateTime(row[0]).ToString("dd/MMM/yy") + "'";
          }


          string SqlFinal = "Select Broker,Portfolio,BrokerageType" + SQLFields + " FROM #tempResult group by Broker,Portfolio,BrokerageType Order by Broker,Portfolio,BrokerageType";

          tablep = curConn.Return_DataTable(SqlFinal);

          dtgBrokerage.DataSource = tablep;
            
          tablep.Dispose();

          int curCounter = 0;

          foreach (GridColumn curColumn in dgBrokerage.Columns)
          {
             // if (curCounter > 7)
              {
                  curColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                  curColumn.DisplayFormat.FormatString = "#,##0.#0;(#,##0.#0)";
                  curColumn.Width = 60;
              }
              curCounter++;
          }
          curCounter = 0;
          foreach (GridColumn curColumn in dgBrokerage.Columns)
          {

              if (dgBrokerage.Columns[curCounter].Name.IndexOf("/") != -1)
              {
                  if (radMonthly.Checked)
                  {
                      dgBrokerage.Columns[curCounter].Caption = Convert.ToDateTime(dgBrokerage.Columns[curCounter].Name.Replace("col", "")).ToString("MMM/yy");
                  }

                dgBrokerage.GroupSummary.Add(SummaryItemType.Sum, curColumn.Name.Replace("col", ""), curColumn);
                ((GridSummaryItem)dgBrokerage.GroupSummary[dgBrokerage.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
              }

              curCounter++;
          }
          ExpandCounter = 0;
          ExpandGroups();

          dgBrokerage.BestFitColumns();
          curUtils.SetColumnStyle(dgBrokerage, 1);

        }

        private void ExpandGroups()
        {
            dgBrokerage.CollapseAllGroups();

            ExpandCounter++;

            if (ExpandCounter > dgBrokerage.GroupCount) { ExpandCounter = 0; };

            for (int i = -1; ; i--)
            {
                if (!dgBrokerage.IsValidRowHandle(i)) return;
                if (dgBrokerage.GetRowLevel(i) < ExpandCounter)
                {
                    dgBrokerage.SetRowExpanded(i, true);
                }
            }
        }

        private void cmdLoad_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                string user = Environment.UserName.ToString();
                string Loca_Machine = Environment.MachineName.ToString();
                string fileName_Log = "T:\\Log\\Reports\\StockLoan_Id_LB_" + LiveDLL.NUserControl.Instance.User_Id + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";
                ExportTo(new ExportXlsProvider(fileName_Log));

                ExportTo(new ExportXlsProvider(fileName));
                OpenFile(fileName);
            }
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
            BaseExportLink link = dgBrokerage.CreateExportLink(provider);
                link = dgBrokerage.CreateExportLink(provider);

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

        private void dtgBrokerage_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            //info.GroupText = view.GroupedColumns[level].Caption + ": " + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
            info.GroupText = view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
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

                if ((rect.X + rect.Width / 2) > 1)
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
                    Console.WriteLine(text);
                }
            }

            e.Handled = true;
             
        }
        private Rectangle GetColumnBounds(GridColumn column)
        {
            if (column != null)
            {
                GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
                GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
                if (colInfo != null)
                    return colInfo.Bounds;
                else
                    return Rectangle.Empty;
            }
            else
                return Rectangle.Empty;
        }

        private void radMonthly_CheckedChanged(object sender, EventArgs e)
        {
            cmdLoad_Click(sender, e);
        }

        private void radDaily_CheckedChanged(object sender, EventArgs e)
        {
            cmdLoad_Click(sender, e);
        }
        private void radAnnual_CheckedChanged(object sender, EventArgs e)
        {
            cmdLoad_Click(sender, e);
        }

        private void dgBrokerage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Clicks > 1) return;
            GridView view = sender as GridView;
            if (view.State != GridState.ColumnDown) return;

            //info.Column.SortOrder

            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);
            if (info.HitTest == GridHitTest.Column)
            {
                ColumnSortOrder curOrder = ColumnSortOrder.Descending;
                if (info.Column.SortOrder == ColumnSortOrder.Descending) { curOrder = ColumnSortOrder.Ascending; };
                foreach (GridColumn curGrouping in dgBrokerage.GroupedColumns)
                {
                    if (curGrouping.Name != "colPortfolio")

                        if (GetGroupByName(dgBrokerage, info.Column.Name.Replace("col", "")) != null)
                        {
                            dgBrokerage.GroupSummarySortInfo.Add(GetGroupByName(dgBrokerage, info.Column.Name.Replace("col", "")), curOrder, curGrouping);
                        }
                }
                dgBrokerage.ClearSorting();
                info.Column.SortOrder = curOrder;
            }
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



     }
}