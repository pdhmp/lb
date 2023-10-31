using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using LiveDLL;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Helpers;
using System.Collections.Generic;

namespace LiveBook
{
    public partial class frmCompIndex : LBForm
    {

        GridControlState curGridControlState = new GridControlState(new GridControlState.ViewDescriptor("", "Sector"));

        old_Conn curConn = new old_Conn();

        RecordCollection srcList;

        DateTime LastRefreshTime = new DateTime(1900, 01, 01);

        GridSummaryItem tempItem;
        ColumnSortOrder curOrder;
        GridColumn curGrouping;
        BindingSource bndGridSource = new BindingSource();
        int SkippedRefresh = 0;


        bool processing = false;
        DateTime DateReport;


        public frmCompIndex()
        {
            InitializeComponent();

            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }


        private void frmCompIndex_Load(object sender, EventArgs e)
        {
            LiveDLL.FormUtils.LoadCombo(cmbView, "Select Id_Portfolio, Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type=2 AND Id_Portfolio=10", "Id_Portfolio", "Port_Name", 99);
            LiveDLL.FormUtils.LoadCombo(cmbStrategy, "Select Id_Book, Book from dbo.Tb400_Books WHERE Id_Book IN(SELECT [Id Book] FROM NESTRT.dbo.FCN_Posicao_Atual() WHERE [Id Portfolio]=10) UNION ALL SELECT '0', 'All Books'", "Id_Book", "Book", 0);

            dgCompIndex.ColumnPanelRowHeight = 32;
            dgCompIndex.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            UpdateGrid();
            timer1.Start();
        }

        void UpdateGrid()
        {
            //dgCompIndex.GroupSummary.Clear();
            //dgCompIndex.Columns.Clear();
            //dtg.DataSource = null;
            //dtg.Refresh();

            List<object> curList = new List<object>();

            curGridControlState.SaveViewInfo(dgCompIndex);

            int Id_Portfolio = Convert.ToInt32(cmbView.SelectedValue.ToString());

            string SQLString;

            DataTable tablep = new DataTable();
            //srcList = new RecordCollection();

            // This part hasn't been changed to accomodate other dates (i.e. not today) nor change portfolio together with positions screen

            SQLString = "SELECT [Sector] ,[Base Underlying], PercFund, PercIndex, Diff, SecChange,SecContrib FROM dbo.FCN_GET_Position_vs_Index(" + Id_Portfolio + ", 0, 0);";

            try
            {
                using (newNestConn curConn = new newNestConn())
                {
                    tablep = curConn.Return_DataTable(SQLString);
                    dtg.DataSource = tablep;
                }
            }
            catch (Exception e) { curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString()); }

            dgCompIndex.Columns["PercFund"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgCompIndex.Columns["PercFund"].DisplayFormat.FormatString = "P2";
            dgCompIndex.Columns["PercIndex"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgCompIndex.Columns["PercIndex"].DisplayFormat.FormatString = "P2";
            dgCompIndex.Columns["Diff"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgCompIndex.Columns["Diff"].DisplayFormat.FormatString = "P2";
            dgCompIndex.Columns["SecChange"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgCompIndex.Columns["SecChange"].DisplayFormat.FormatString = "P2";
            dgCompIndex.Columns["SecContrib"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgCompIndex.Columns["SecContrib"].DisplayFormat.FormatString = "P2";

            if (dgCompIndex.GroupSummary.Count == 0)
            {
                dgCompIndex.GroupSummary.Add(SummaryItemType.Sum, "PercFund", dgCompIndex.Columns["PercFund"]); ((GridSummaryItem)dgCompIndex.GroupSummary[dgCompIndex.GroupSummary.Count - 1]).DisplayFormat = "{0:0.00%}";
                dgCompIndex.GroupSummary.Add(SummaryItemType.Sum, "PercIndex", dgCompIndex.Columns["PercIndex"]); ((GridSummaryItem)dgCompIndex.GroupSummary[dgCompIndex.GroupSummary.Count - 1]).DisplayFormat = "{0:0.00%}";
                dgCompIndex.GroupSummary.Add(SummaryItemType.Sum, "Diff", dgCompIndex.Columns["Diff"]); ((GridSummaryItem)dgCompIndex.GroupSummary[dgCompIndex.GroupSummary.Count - 1]).DisplayFormat = "{0:0.00%}";
                dgCompIndex.GroupSummary.Add(SummaryItemType.Sum, "SecChange", dgCompIndex.Columns["SecChange"]); ((GridSummaryItem)dgCompIndex.GroupSummary[dgCompIndex.GroupSummary.Count - 1]).DisplayFormat = "{0:0.00%}";
                dgCompIndex.GroupSummary.Add(SummaryItemType.Sum, "SecContrib", dgCompIndex.Columns["SecContrib"]); ((GridSummaryItem)dgCompIndex.GroupSummary[dgCompIndex.GroupSummary.Count - 1]).DisplayFormat = "{0:0.00%}";
            }

            //dgCompIndex.Columns["Sector"].GroupIndex = 0;

            //dgCompIndex.BestFitColumns();

            curUtils.SetColumnStyle(dgCompIndex, 1);
            curGridControlState.LoadViewInfo(dgCompIndex);
        }




        private void dgCompIndex_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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


        public void Set_Portfolio_Values(int Id_Portfolio, DateTime Historical)
        {
            cmbView.SelectedValue = Id_Portfolio;
            DateReport = Historical;

            if (DateReport.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                this.Text = "Exposure Book";
            }
            else
            {
                this.Text = "Exposure Book (HISTORICAL as of " + DateReport.ToString("dd/MM/yy") + ")";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (!processing)
            {
                processing = true;

                UpdateGrid();

                processing = false;


            }
        }

        private void dgCompIndex_MouseDown(object sender, MouseEventArgs e)
        {
            processing = true;

            bolHoldCalc = true;

            GridView view = sender as GridView;
            downHitInfo = null;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                downHitInfo = hitInfo;

        }

        private void dgCompIndex_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgCompIndex);
            timer1.Start();
        }

        private void dgCompIndex_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgCompIndex);
            timer1.Start();
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgCompIndex.SelectAll();
            dgCompIndex.CopyToClipboard();
        }

        DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo downHitInfo = null;
        bool bolHoldCalc;

        private void dgCompIndex_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    if (downHitInfo.Column != null)
                    {
                        //DataRow row = view.GetDataRow(downHitInfo.RowHandle);
                        string curCol = downHitInfo.Column.Name;
                        string IdTicker = "DRAGITEM\t" + dgCompIndex.GetRowCellValue(downHitInfo.RowHandle, curCol.Replace("col", "")).ToString();
                        view.GridControl.DoDragDrop(IdTicker, DragDropEffects.Move);

                        downHitInfo = null;
                        DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                    }
                }
            }
        }

        private void dgCompIndex_MouseUp(object sender, MouseEventArgs e)
        {
            bolHoldCalc = false;

            if (e.Button != MouseButtons.Left || e.Clicks > 1) return;
            GridView view = sender as GridView;
            if (view.State != GridState.ColumnDown) return;

            //info.Column.SortOrder

            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);
            if (info.HitTest == GridHitTest.Column)
            {
                curOrder = ColumnSortOrder.Descending;
                if (info.Column.SortOrder == ColumnSortOrder.Descending) { curOrder = ColumnSortOrder.Ascending; };
                foreach (GridColumn loopGrouping in dgCompIndex.GroupedColumns)
                {
                    curGrouping = loopGrouping;
                    tempItem = GetGroupByName(dgCompIndex, info.Column.Name.Replace("col", ""));
                    if (tempItem != null)
                    {
                        dgCompIndex.GroupSummarySortInfo.Add(tempItem, curOrder, curGrouping);
                    }
                }
                dgCompIndex.ClearSorting();

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

        private void dgCompIndex_DragObjectDrop_1(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgCompIndex);
            timer1.Start();
        }

    }
}