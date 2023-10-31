using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using DevExpress.Data;
using DevExpress.Utils;

using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;

using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using NestDLL;
using NestSymConn;
using NCustomControls;
using NCommonTypes;

namespace LiveTrade2
{
    public partial class frmPnL : ConnectedForm
    {
        List<string> SubscribedTickers = new List<string>();
        List<TSLineItem> LineList = new List<TSLineItem>();
        BindingSource bndDataSource = new BindingSource();
        bool flgMouseDown = false;
        public string SQLString;

        TradingStatus curStatusList = new TradingStatus();
        Utils curUtils = new Utils(5);
        bool SelectFirstonNextSort = false;

        public frmPnL()
        {
            InitializeComponent();
        }

        ~frmPnL()
        {
            curNDistConn.Disconnect();
        }

        private void frmPnL_Load(object sender, EventArgs e)
        {
            //chkFiltro1.BackColor = Color.Transparent;
            //chkFiltro1.Parent = dtgPnL;
            //chkFiltro1.ForeColor = Color.White;
            
            curNDistConn.OnData += new EventHandler(NewMarketData);

            InitializeGrid();

            SubscribeTickers();

            tmrUpdate.Start();
            tmrReload.Start();
        }

        private void InitializeGrid()
        { 
            dtgPnL.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgPnL.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgPnL.LookAndFeel.SetSkinStyle("Blue");

            dgPnL.ColumnPanelRowHeight = 32;
            dgPnL.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            dgPnL.Appearance.Row.ForeColor = GlobalVars.Instance.GridForeColor;
            dgPnL.Appearance.Row.BackColor = GlobalVars.Instance.GridBackColor;
            dgPnL.OptionsView.ShowHorzLines = false;
            dgPnL.OptionsView.ShowVertLines = false;
            dgPnL.Appearance.Empty.BackColor = GlobalVars.Instance.GridBackColor;
            dgPnL.Appearance.FocusedRow.BackColor = Color.DarkGray;
            dgPnL.Appearance.SelectedRow.BackColor = Color.DarkGray;

            RefreshData();

            dgPnL.Columns["Bought"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPnL.Columns["Bought"].DisplayFormat.FormatString = "#,##0;(#,##0);-";

            dgPnL.Columns["Sold"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPnL.Columns["Sold"].DisplayFormat.FormatString = "#,##0;(#,##0);-";

            dgPnL.Columns["Net"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPnL.Columns["Net"].DisplayFormat.FormatString = "#,##0;(#,##0);-";

            dgPnL.Columns["AvgPriceSold"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPnL.Columns["AvgPriceSold"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000);-";

            dgPnL.Columns["AvgPriceBought"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPnL.Columns["AvgPriceBought"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000);-";

            dgPnL.Columns["CashFlow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPnL.Columns["CashFlow"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgPnL.Columns["Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPnL.Columns["Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);-";

            dgPnL.Columns["PnL"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgPnL.Columns["PnL"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00);-";

            
            dgPnL.GroupSummary.Add(SummaryItemType.Sum, "CashFlow", dgPnL.Columns["CashFlow"], "{0:#,##0.00; -#,##0.00; -}");
            dgPnL.GroupSummary.Add(SummaryItemType.Sum, "PnL", dgPnL.Columns["PnL"], "{0:#,##0.00; -#,##0.00; -}");

            dgPnL.Columns["Ticker"].Width = 110;
            dgPnL.Columns["Ticker"].Fixed = FixedStyle.Left;

            curUtils.LoadGridColumns(dgPnL, this.Text);

            dgPnL.ExpandAllGroups();
        }

        private void RefreshData()
        {
            SQLString = "SELECT * FROM [dbo].[FCN_LT_Trade_Summary]()";

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                DataTable dt = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {
                    TSLineItem curItem = CreateQuote((string)curRow["Ticker"], (int)NestDLL.Utils.ParseToDouble(curRow["IdSecurity"].ToString()), (int)NestDLL.Utils.ParseToDouble(curRow["RTUpdateSource"].ToString()));
                    curItem.Fund = curRow["Fund"].ToString();
                    curItem.Broker = curRow["Broker"].ToString();
                    curItem.Trader = curRow["Trader"].ToString();
                    curItem.Bought = (int)NestDLL.Utils.ParseToDouble(curRow["Bought"]);
                    curItem.AvgPriceBought = NestDLL.Utils.ParseToDouble(curRow["AvgPriceBought"]);
                    curItem.Sold = (int)NestDLL.Utils.ParseToDouble(curRow["Sold"]);
                    curItem.AvgPriceSold = NestDLL.Utils.ParseToDouble(curRow["AvgPriceSold"]);
                    curItem.Net = (int)NestDLL.Utils.ParseToDouble(curRow["Net"]);
                    curItem.CashFlow = NestDLL.Utils.ParseToDouble(curRow["CashFlow"]);
                    curItem.LotSize = NestDLL.Utils.ParseToDouble(curRow["LotSize"]);

                    if (LineList.Contains(curItem))
                    {
                        lock (LineList)
                        {
                            TSLineItem tempItem = LineList[LineList.IndexOf(curItem)];
                            tempItem.Bought = curItem.Bought;
                            tempItem.AvgPriceBought = curItem.AvgPriceBought;
                            tempItem.Sold = curItem.Sold;
                            tempItem.AvgPriceSold = curItem.AvgPriceSold;
                            tempItem.Net = curItem.Net;
                            tempItem.CashFlow = curItem.CashFlow;
                        }
                    }
                    else
                    {
                        LineList.Add(curItem);
                    }
                }
            }

            SubscribeTickers();

            bndDataSource.DataSource = LineList;
            dtgPnL.DataSource = bndDataSource;
            curUtils.LoadGridColumns(dgPnL, this.Text);
        }

        private void SubscribeTickers()
        {
            foreach (TSLineItem curItem in LineList)
            {
                if (!SubscribedTickers.Contains(curItem.Ticker))
                {
                    curNDistConn.Subscribe(curItem.Ticker, GlobalVars.Instance.getDataSource(curItem.Ticker));
                    Console.WriteLine(curItem.Ticker + "\t" + GlobalVars.Instance.getDataSource(curItem.Ticker));
                    SubscribedTickers.Add(curItem.Ticker);
                }
            }
        }

        private TSLineItem CreateQuote(string Ticker, int IdSecurity, int UpdateSource)
        {
            TSLineItem curItem = new TSLineItem();
            curItem.IdSecurity = IdSecurity;
            curItem.Ticker = Ticker;
            
            return curItem;
        }

        private void RefreshGrid()
        {
            dgPnL.LayoutChanged();
            dgPnL.UpdateGroupSummary();
            dgPnL.RefreshData();
        }
        
        private void NewMarketData(object sender, EventArgs origE)
        {
            int i = 0;
            MarketUpdateList curUpdateList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
            {
                if (curUpdateItem.FLID == NestFLIDS.Last && curUpdateItem.ValueDouble != 0)
                {
                    foreach (TSLineItem curTSLineItem in LineList)
                    {
                        if(curTSLineItem.Ticker == curUpdateItem.Ticker)
                            curTSLineItem.Last = curUpdateItem.ValueDouble;
                    }
                }
            }
        }

        double ParseToDouble(object strValue)
        {
            if (DBNull.Value.Equals(strValue))
            {
                return 0;
            }
            else if (String.IsNullOrEmpty(Convert.ToString(strValue)))
            {
                return 0;
            }
            else
            {
                return double.Parse(strValue.ToString());
            }
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {

        }

        private void tmrReload_Tick(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dtgQuotes_MouseDown(object sender, MouseEventArgs e)
        {
            flgMouseDown = true;
        }

        private void dtgQuotes_MouseUp(object sender, MouseEventArgs e)
        {
            flgMouseDown = false;
        }

        private void dgQuotes_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //Color upColor;
            //upColor = Color.Lime;


            //if (e.Appearance.BackColor != Color.Black)
            //{
            //    upColor = Color.Green;
            //}

            //if (e.Column.Name == "colABSAuction" || e.Column.Name == "colPCTAuction")
            //{
            //    object curSelRow = dgPnL.GetRowCellValue(e.RowHandle, dgPnL.Columns["PCTAuction"]);
                    
            //    if (curSelRow != null)
            //    {
            //        double refValue = double.Parse(curSelRow.ToString());

            //        if (refValue < 0 && e.DisplayText.Substring(0,1) != "-") e.DisplayText = "-" + e.DisplayText;

            //        if (refValue > 0 && e.Appearance.ForeColor != upColor)
            //        {
            //            e.Appearance.ForeColor = upColor;
            //            if (refValue > 0.01)
            //            {
            //                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            //            }
            //        }
            //        else if (refValue < 0 && e.Appearance.ForeColor != Color.Red)
            //        {
            //            e.Appearance.ForeColor = Color.Red;
            //            if (refValue < -0.01)
            //            {
            //                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            //            }
            //        }
            //        else if (refValue == 0 && e.Appearance.ForeColor != dgPnL.Appearance.Row.ForeColor)
            //        {
            //            e.Appearance.ForeColor = dgPnL.Appearance.Row.ForeColor;
            //            e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
            //        }


            //        int IdInstrument = 0;
            //        NCustomControls.MyGridView curView = (NCustomControls.MyGridView)sender;

            //        IdInstrument = int.Parse(curView.GetRowCellValue(e.RowHandle, "IdInstrument").ToString());

            //        if (IdInstrument == 16)
            //        {
            //            double chgValue = double.Parse(e.DisplayText.Replace("%", ""));
            //            e.DisplayText = chgValue.ToString("0.00");
            //        }
            //    }
            //}
        }

        private void dgQuotes_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            info.GroupText = view.GetRowCellDisplayText(row, view.GroupedColumns[level]).ToUpper();
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

        private void dgQuotes_EndSorting(object sender, EventArgs e)
        {
            if (SelectFirstonNextSort)
            {
                dgPnL.FocusedRowHandle = 0;
                SelectFirstonNextSort = false;
            }
        }

        public void SetSortedColumn(string colName, bool RecurseColumns)
        {
            ColumnSortOrder curOrder = ColumnSortOrder.Descending;
            if (dgPnL.Columns[colName.Replace("col", "")].SortOrder == ColumnSortOrder.Descending) { curOrder = ColumnSortOrder.Ascending; };
            foreach (GridColumn curGrouping in dgPnL.GroupedColumns)
            {
                GridSummaryItem curItem = GetGroupByName(dgPnL, colName.Replace("col", ""));
                if (curItem != null)
                    dgPnL.GroupSummarySortInfo.Add(curItem, curOrder, curGrouping);
            }
            dgPnL.ClearSorting();
            dgPnL.Columns[colName.Replace("col", "")].SortOrder = curOrder;
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

        private void dgQuotes_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            e.LevelAppearance.BackColor = Color.White;
            e.LevelAppearance.ForeColor = Color.Black;
            e.LevelAppearance.Font = new Font(dgPnL.Appearance.Row.Font, FontStyle.Bold); ;
        }

        private void dgQuotes_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);

            string a = ((GridView)sender).FocusedColumn.Name;

            if (info.HitTest == GridHitTest.RowCell && ((GridView)sender).FocusedValue.ToString() != "" && ((GridView)sender).FocusedColumn.Name.Contains("Ticker"))
            {
                if ((Control.ModifierKeys & Keys.Shift) != Keys.None)
                {
                    string curTicker = dgPnL.FocusedValue.ToString();

                    if (curTicker != "")
                    {
                        frmOptionChain newOptionChain = new frmOptionChain(curTicker);
                        newOptionChain.Show();
                    }
                }
                else
                {
                    string curValue = ((GridView)sender).FocusedValue.ToString();
                    frmLevel2 curFrmLevel2 = new frmLevel2(curValue);
                    curFrmLevel2.Show();
                    //if (curValue.IndexOf('.') == -1) curValue = curValue + ".SA";
                    curFrmLevel2.txtTicker.Text = curValue;
                    curFrmLevel2.cmdRequest_Click(this, new EventArgs());
                    curFrmLevel2.LoadOrders();
                    //e.Cancel = true; 
                }
            }
        }

        private void dgAucQuotes_MouseUp(object sender, MouseEventArgs e)
        {
            dgPnL.UnselectRow(dgPnL.FocusedRowHandle);

        }

        private void dgPnL_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.SaveGridColumns(dgPnL, this.Text);
        }

        private void dgPnL_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.SaveGridColumns(dgPnL, this.Text);
        }

        private void chkFiltro1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateFilter();
        }

        private void UpdateFilter()
        {
            //string FilterText = "([Trader] = 'Michel' OR [Trader] = 'MichelLT' OR [Trader] = 'FRANCISCO' )";

            //if (chkFiltro1.Checked)
            //{
            //    dgPnL.Columns["Trader"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, FilterText);
            //}
            //else
            //{
            //    dgPnL.Columns["Trader"].ClearFilter();
            //}

        }

       

    }
}