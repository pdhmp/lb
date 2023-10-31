using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NestDLL;

using DevExpress.Data;
using DevExpress.Utils;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using System.Collections;
using System.Collections.Generic;

namespace LiveTrade2
{
    public partial class frmOrderReview : ConnectedForm
    {
        Utils curUtils = new Utils(5);
        
        List<NFIXConnLT.OrderLT> curOrderList = new List<NFIXConnLT.OrderLT>();

        bool OrderGridInitialized = false;
        BindingSource bndDataSourceOrders = new BindingSource();
        int ColorCode = 0;
        double curFairLast = 0;
        bool PendingUpdate = false;

        public frmOrderReview()
        {
            InitializeComponent();
        }

        private void fmrOrderReview_Load(object sender, EventArgs e)
        {
            dtgOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgOrders.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgOrders.LookAndFeel.SetSkinStyle("Blue");

            //bndDataSourceOrders.DataSource = FIXConnections.Instance.curFixConn.OrderList;

            FIXConnections.Instance.curFixConn.OnOrderUpdate += new EventHandler(FIXUpdateReceived);

            tmrUpdate.Start();
            PendingUpdate = true;
        }

        private void FIXUpdateReceived(object sender, EventArgs e)
        {
            PendingUpdate = true;
        }

        private void InitializeOrderGrid()
        {
            if (!OrderGridInitialized)
            {
                bndDataSourceOrders.DataSource = curOrderList;
                dtgOrders.DataSource = bndDataSourceOrders;

                dgOrders.Columns["Done"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgOrders.Columns["Done"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

                dgOrders.Columns["NetDone"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgOrders.Columns["NetDone"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

                dgOrders.Columns["Leaves"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgOrders.Columns["Leaves"].DisplayFormat.FormatString = "#,##0;(#,##0);\\ ";

                dgOrders.Columns["Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgOrders.Columns["Price"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

                dgOrders.Columns["ExecPrice"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgOrders.Columns["ExecPrice"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

                dgOrders.Columns["dblPrice"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgOrders.Columns["dblPrice"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);\\ ";

                dgOrders.Columns["SentTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgOrders.Columns["SentTime"].DisplayFormat.FormatString = "hh:mm:ss fff";

                dgOrders.Columns["SentTime"].VisibleIndex = 0;

                dgOrders.GroupSummary.Add(SummaryItemType.Sum, "NetDone", dgOrders.Columns["NetDone"], "{0:#,##0}");
                dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Done", dgOrders.Columns["Done"], "{0:#,##0}");
                dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Leaves", dgOrders.Columns["Leaves"], "{0:#,##0}");
                dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Buy", dgOrders.Columns["Buy"], "{0:#,##0}");
                dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Sell", dgOrders.Columns["Sell"], "{0:#,##0}");

                dgOrders.GroupSummary.Add(SummaryItemType.Custom, "ExecPrice", dgOrders.Columns["ExecPrice"], "{0:#,##0.0000}");
                dgOrders.GroupSummary.Add(SummaryItemType.Custom, "BuyPrice", dgOrders.Columns["BuyPrice"], "{0:#,##0.0000}");
                dgOrders.GroupSummary.Add(SummaryItemType.Custom, "SellPrice", dgOrders.Columns["SellPrice"], "{0:#,##0.0000}");

                dgOrders.Columns["Side"].Visible = false;
                dgOrders.Columns["Status"].Visible = false;
                dgOrders.Columns["SentTick"].Visible = false;

                foreach (GridColumn curColumn in dgOrders.Columns)
                {
                    if (curColumn.Name.Contains("str")) { curColumn.Caption = curColumn.Name.Substring(3).Replace("str", ""); }
                    if (curColumn.Name.Contains("dbl")) { curColumn.Caption = curColumn.Name.Substring(3).Replace("dbl", "").Replace("col", ""); }
                }

                curUtils.LoadGridColumns(dgOrders, this.Text);
            }

            OrderGridInitialized = true;
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (PendingUpdate) { RefreshGrid(); }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            if (FIXConnections.Instance.curFixConn != null)
            {
                if (FIXConnections.Instance.curFixConn.OrderList.Count > 0)
                {
                    InitializeOrderGrid();

                    curOrderList.Clear();

                    dgOrders.BeginUpdate();
                    foreach (NFIXConnLT.OrderLT curOrderLT in FIXConnections.Instance.curFixConn.OrderList)
                    {
                        NFIXConnLT.OrderLT curDisplayOrderLT = curOrderLT;
                        string curBook = "";
                        string curSection = "";
                        if (GlobalVars.Instance.Books.TryGetValue(curDisplayOrderLT.IdBook, out curBook)) { curDisplayOrderLT.Book = curBook; }
                        if (GlobalVars.Instance.Sections.TryGetValue(curDisplayOrderLT.IdSection, out curSection)) { curDisplayOrderLT.Section = curSection; }
                        curOrderList.Add(curDisplayOrderLT);
                    }
                    dgOrders.EndUpdate();

                    bndDataSourceOrders.ResetBindings(false);
                    PendingUpdate = false;
                }
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgOrders.SelectAll();
            dgOrders.CopyToClipboard();
        }

        private void dgOrders_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn weightColumn = null;

            GridGroupSummaryItem curItem = (GridGroupSummaryItem)e.Item;


            switch (curItem.FieldName)
            {
                case "ExecPrice": weightColumn = dgOrders.Columns["Done"]; break;
                case "BuyPrice": weightColumn = dgOrders.Columns["Buy"]; break;
                case "SellPrice": weightColumn = dgOrders.Columns["Sell"]; break;
                default:
                    break;
            
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
                        double size = Convert.ToDouble(e.FieldValue);
                        double weight = Convert.ToDouble(((GridView)sender).GetRowCellValue(e.RowHandle, weightColumn));

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

        private void dgOrders_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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

        private Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return Rectangle.Empty;
        }

        private void dgOrders_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.SaveGridColumns(dgOrders, this.Text);
        }

        private void dgOrders_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.SaveGridColumns(dgOrders, this.Text);
        }

        private void dgOrders_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);

            string a = ((GridView)sender).FocusedColumn.Name;

            if (info.HitTest == GridHitTest.RowCell)// && ((GridView)sender).FocusedValue.ToString() != "" && ((GridView)sender).FocusedColumn.Name.Contains("Ticker"))
            {
                string curValue = dgOrders.GetRowCellValue(dgOrders.FocusedRowHandle, "Symbol").ToString();
                frmLevel2 curFrmLevel2 = new frmLevel2(curValue);
                curFrmLevel2.Show();
                curFrmLevel2.txtTicker.Text = curValue;
                curFrmLevel2.cmdRequest_Click(this, new EventArgs());
                curFrmLevel2.LoadOrders();
            }
        }

        ContextMenu GridContextMenu;
        MenuItem mnuAddOuts;

        private void dgOrders_ShowGridMenu(object sender, GridMenuEventArgs e)
        {
            GridView view = sender as GridView;
            GridHitInfo hitInfo = view.CalcHitInfo(e.Point);

            if (hitInfo.InRow)
            {
                if (hitInfo.RowHandle >= 0)
                {
                    GridContextMenu = new ContextMenu();

                    string IdOrder = dgOrders.GetRowCellValue(dgOrders.FocusedRowHandle, "OrderID").ToString();

                    mnuAddOuts = new MenuItem();
                    mnuAddOuts.Text = "Add outs to order: " + IdOrder;
                    mnuAddOuts.Click += new EventHandler(mnuAddOuts_Click);
                    mnuAddOuts.Tag = IdOrder + (char)16 + hitInfo.RowHandle;

                    GridContextMenu.MenuItems.Add(mnuAddOuts);

                    view.FocusedRowHandle = hitInfo.RowHandle;
                    GridContextMenu.Show(view.GridControl, e.Point);
                }
            }
        }

        private void mnuAddOuts_Click(object sender, EventArgs e)
        {
            string[] tempValues = mnuAddOuts.Tag.ToString().Split((char)16);

            string IdOrder = tempValues[0];

            FIXConnections.Instance.curFixConn.AddOutsToOrder(IdOrder);
            RefreshGrid();

            dgOrders.RefreshRow(int.Parse(tempValues[1]));
        }
    }
}