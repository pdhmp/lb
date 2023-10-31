using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;
using LiveBook.Business;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Helpers;


namespace LiveBook
{
    public partial class frmOrderReview : LBForm
    {
        newNestConn curConn = new newNestConn();
        List<Order> OrderList = new List<Order>();
        
        string data_now = DateTime.Now.ToString("yyyyMMdd");

        public frmOrderReview()
        {
            InitializeComponent();
        }

        private void frmOpen_Load(object sender, EventArgs e)
        {
            dtgOrderReview.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgOrderReview.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgOrderReview.LookAndFeel.SetSkinStyle("Blue");

            RefreshGrids();
            radAll.Checked = true;
        }

        private void RefreshGrids()
        {
            cmdRefresh.Enabled = false;
            LoadOrders();
            cmdRefresh.Enabled = true;
        }

        private void LoadOrders()
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT * " +
                            " FROM VW_LB_ORDERS A " +
                            " LEFT JOIN (SELECT * FROM VW_LB_Trades WHERE [Trade Date]='" + DateTime.Now.ToString("yyyy-MM-dd") + "' AND Id_Port_Type = 2) B " +
                            " ON A.[Id Order] = b.[Id Order] " +
                            " WHERE A.[Order Date]='" + DateTime.Now.ToString("yyyy-MM-dd") + "' " +
                            " ORDER BY A.[Id Order], A.[Id Portfolio], B.TimeStamp ";

                DataTable curTable = curConn.Return_DataTable(SQLString);

                double prevOrder = 0;
                int prevPortfolio = 0;
                Order curOrder = new Order();

                OrderList.Clear();

                foreach (DataRow curRow in curTable.Rows)
                {
                    if (Utils.ParseToDouble(curRow["Id Order"]) != prevOrder || Utils.ParseToDouble(curRow["Id Portfolio"]) != prevPortfolio)
                    {
                        if (curOrder.IdOrder != 0) OrderList.Add(curOrder);
                        curOrder = new Order();
                    }

                    prevOrder = Utils.ParseToDouble(curRow["Id Order"]);
                    prevPortfolio = (int)Utils.ParseToDouble(curRow["Id Portfolio"]);

                    if (curOrder.IdOrder == 0)
                    {
                        curOrder.IdOrder = Utils.ParseToDouble(curRow["Id Order"]);
                        curOrder.Portfolio = curRow["Portfolio"].ToString();
                        curOrder.Book = curRow["Book"].ToString();
                        curOrder.Section = curRow["Section"].ToString();
                        curOrder.Ticker = curRow["Ticker"].ToString();
                        curOrder.Total = Utils.ParseToDouble(curRow["Total"]);
                        curOrder.Done = Utils.ParseToDouble(curRow["Done"]);
                        curOrder.Leaves = Utils.ParseToDouble(curRow["Leaves"]);
                        curOrder.CashDone = Utils.ParseToDouble(curRow["Cash Done"]);
                        curOrder.AvgPriceDone = Utils.ParseToDouble(curRow["Avg Price Done"]);
                        curOrder.Login = curRow["Login"].ToString();
                        curOrder.Status = curRow["Status"].ToString();
                        curOrder.Split = (int)Utils.ParseToDouble(curRow["Split"]);
                        curOrder.OrderDate = Utils.ParseToDateTime(curRow["Order Date"]);
                        curOrder.Currency = curRow["Currency"].ToString();
                        curOrder.Broker = curRow["Broker"].ToString();
                        curOrder.IdBroker = (int)Utils.ParseToDouble(curRow["Id Broker"]);
                        curOrder.IdBook = (int)Utils.ParseToDouble(curRow["Id Book"]);
                        curOrder.IdSection = (int)Utils.ParseToDouble(curRow["Id Section"]);
                        curOrder.OrderPrice = Utils.ParseToDouble(curRow["Order Price"]);
                    }

                    if (Utils.ParseToDouble(curRow["Trade Quantity"]) != 0)
                    {
                        Trade curTrade = new Trade();
                        curTrade.TradePrice = Utils.ParseToDouble(curRow["Trade Price"]);
                        curTrade.TradeQuantity = Utils.ParseToDouble(curRow["Trade Quantity"]);
                        curTrade.TradeCash = Utils.ParseToDouble(curRow["Trade Cash"]);
                        curTrade.TradeStatus = curRow["Trade Status"].ToString();
                        curTrade.IdTrade = Utils.ParseToDouble(curRow["Id Trade"]);
                        curTrade.IdOrder = Utils.ParseToDouble(curRow["Id Order"]);
                        curTrade.IdTradeStatus = (int)Utils.ParseToDouble(curRow["Id Trade Status"]);
                        curTrade.Portfolio = curRow["Portfolio"].ToString();
                        curTrade.IdPortfolio = (int)Utils.ParseToDouble(curRow["Id Portfolio"]);
                        curTrade.IdTicker = (int)Utils.ParseToDouble(curRow["Id Ticker"]);
                        curTrade.TimeStamp = Utils.ParseToDateTime(curRow["TimeStamp"]);

                        curOrder.AddTrade(curTrade);
                    }
                }

                if (curOrder.IdOrder != 0) OrderList.Add(curOrder);

                dtgOrderReview.DataSource = OrderList;

                curUtils.SetColumnStyle(dgOrderReview, 2, "Total");

                dgOrderReview.Columns["Total"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgOrderReview.Columns["Total"].DisplayFormat.FormatString = "#,##0;(#,##0)";
                dgOrderReview.Columns["Done"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgOrderReview.Columns["Done"].DisplayFormat.FormatString = "#,##0;(#,##0)";
                dgOrderReview.Columns["Leaves"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgOrderReview.Columns["Leaves"].DisplayFormat.FormatString = "#,##0;(#,##00)";
                dgOrderReview.Columns["CashDone"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgOrderReview.Columns["CashDone"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                dgOrderReview.Columns["AvgPriceDone"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgOrderReview.Columns["AvgPriceDone"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";
                dgOrderReview.Columns["OrderPrice"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; dgOrderReview.Columns["OrderPrice"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

                if (dgOrderReview.GroupSummary.Count == 0)
                {
                    dgOrderReview.GroupSummary.Add(SummaryItemType.Sum, "Total", dgOrderReview.Columns["Total"]);
                    ((GridSummaryItem)dgOrderReview.GroupSummary[dgOrderReview.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0}";

                    dgOrderReview.GroupSummary.Add(SummaryItemType.Sum, "Done", dgOrderReview.Columns["Done"]);
                    ((GridSummaryItem)dgOrderReview.GroupSummary[dgOrderReview.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0}";

                    dgOrderReview.GroupSummary.Add(SummaryItemType.Sum, "Leaves", dgOrderReview.Columns["Leaves"]);
                    ((GridSummaryItem)dgOrderReview.GroupSummary[dgOrderReview.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0}";

                    dgOrderReview.GroupSummary.Add(SummaryItemType.Sum, "CashDone", dgOrderReview.Columns["CashDone"]);
                    ((GridSummaryItem)dgOrderReview.GroupSummary[dgOrderReview.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
                }
            }
        }

        private void dgOrderReview_MasterRowExpanded(object sender, CustomMasterRowEventArgs e)
        {
            GridView detail = dgOrderReview.GetDetailView(e.RowHandle, e.RelationIndex) as GridView;
            detail.Columns["TradeQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; detail.Columns["TradeQuantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";
            detail.Columns["TradePrice"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; detail.Columns["TradePrice"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";
            detail.Columns["TradeCash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric; detail.Columns["TradeCash"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
            detail.Columns["TimeStamp"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime; detail.Columns["TimeStamp"].DisplayFormat.FormatString = "dd/MM/yy hh:mm:ss";
            detail.BestFitColumns();
            detail.OptionsBehavior.Editable = false;
            detail.DoubleClick += new System.EventHandler(this.dgTrades_DoubleClick);
            detail.Columns["IdOrder"].Visible = false;
            detail.Columns["IdTradeStatus"].Visible = false;
            detail.Columns["IdPortfolio"].Visible = false;
            detail.Columns["Portfolio"].Visible = false;
        }

        private void dgOrderReview_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgOrderReview);
        }

        private void dgOrderReview_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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

        private void dgOrderReview_DoubleClick(object sender, EventArgs e)
        {
            int resultado=666;

            string Order_Status = (string)dgOrderReview.GetRowCellValue(dgOrderReview.FocusedRowHandle, "Status");

            if (Order_Status != "Cancelada")
            {
                //string Quantity = (string)dgAbertas.GetRowCellValue(dgAbertas.FocusedRowHandle, "Tot'");
                string Ticker = (string)dgOrderReview.GetRowCellValue(dgOrderReview.FocusedRowHandle, "Ticker");
                int Id_Account = Convert.ToInt32(dgOrderReview.GetRowCellValue(dgOrderReview.FocusedRowHandle, "IdPortfolio"));
                int Id_Order = Convert.ToInt32(dgOrderReview.GetRowCellValue(dgOrderReview.FocusedRowHandle, "IdOrder"));
                double Total = Convert.ToDouble(dgOrderReview.GetRowCellValue(dgOrderReview.FocusedRowHandle, "Total"));
                double Done = Convert.ToDouble(dgOrderReview.GetRowCellValue(dgOrderReview.FocusedRowHandle, "Done"));


                if (Id_Order != 0)
                {
                    GridView Get_Column = sender as GridView;
                    string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

                    switch (Column_Name)
                    {

                        case "Cancel Leaves":
                            resultado = Negocios.Cancela_Ordem(Id_Order,0);
                            break;

                        case "Cancel All":
                            resultado = Negocios.Cancela_Ordem_Trade(Id_Order, 0);
                            break;
                            
                        default:

                            if (Math.Abs(Done) < Math.Abs(Total))
                            {
                                if (Id_Account == 55)
                                {
                                    //resultado = Negocios.Inserir_Divisao(Id_Order, Id_User, Ticker, Convert.ToStringTotal);
                                }
                                else
                                {
                                    resultado = Negocios.Inserir_Trade(Id_Order);
                                }
                            }
                            break;
                    }
                    if (NestDLL.NUserControl.Instance.User_Id != 9)
                    {
                        if (resultado != 0 && resultado !=666)
                        {
                            RefreshGrids();
                        }
                    }
                    // ButtonEdit edit = sender as ButtonEdit;
                    // GridView view = (edit.Parent as GridControl).FocusedView as GridView;
                    // string name = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["Id Portfolio"]).ToString();
                    // MessageBox.Show(name);
                }
            }
        }

        private void dgOrderReview_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgOrderReview);

        }

        private bool Valida_Colunas(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            if (curUtils.Verifica_Acesso(1) || curUtils.Verifica_Acesso(2))
            {
                return true;
            }
            else
            {
                return false;
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
        
        private void ShowColumnSelector() { ShowColumnSelector(true, dgOrderReview); }
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

        private void dgTrades_DoubleClick(object sender, EventArgs e)
        {
            int resultado;
            string SQLString;

            GridView dgTrades = (GridView)sender;

            int Id_Trade = Convert.ToInt32(dgTrades.GetRowCellValue(dgTrades.FocusedRowHandle, "IdTrade"));
            int Id_Order = Convert.ToInt32(dgTrades.GetRowCellValue(dgTrades.FocusedRowHandle, "IdOrder"));
            int Id_Status = Convert.ToInt32(dgTrades.GetRowCellValue(dgTrades.FocusedRowHandle, "IdTradeStatus"));

            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.FieldName.ToString();

            if (Column_Name == "Cancel" && Id_Status != 4)
            {
                int resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this Trade?", "Trades", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                if (resposta == 6)
                {
                    SQLString = "EXEC Proc_Cancela_Trade_Ordem @Id_Trade = " + Id_Trade;
                    using (newNestConn curConn = new newNestConn())
                    {
                        resultado = curConn.ExecuteNonQuery(SQLString, 1);
                        if (resultado != 0)
                        {
                            //RefreshGrids();
                        }
                        else
                        {
                            MessageBox.Show("Error on Cancel!");
                        }
                    }
                }
            }
            if (Column_Name == "Edit")
            {

                frmEditPriceTrade Edita_Trade = new frmEditPriceTrade();
                Edita_Trade.Id_Trade = Id_Trade;
                Edita_Trade.ShowDialog();

            }
        }

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            dgOrderReview.ExpandAllGroups();
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            dgOrderReview.CollapseAllGroups();
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radAll.Checked == true)
            {
                ClearStatusFilter();
            }
        }

        private void radOpen_CheckedChanged(object sender, EventArgs e)
        {
            CheckFilter();
        }

        private void radDone_CheckedChanged(object sender, EventArgs e)
        {
            CheckFilter();
        }

        private void radCancelled_CheckedChanged(object sender, EventArgs e)
        {
            CheckFilter();
        }

        void CheckFilter()
        {
            if (radOpen.Checked == true) { ClearStatusFilter(); dgOrderReview.Columns["Status"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Status] = 'Aberta'"); };
            if (radDone.Checked == true) { ClearStatusFilter(); dgOrderReview.Columns["Status"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Status] = 'Executadas'"); };
            if (radCancelled.Checked == true) { ClearStatusFilter(); dgOrderReview.Columns["Status"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Status] = 'Cancelada'"); };
        }

        private void ClearStatusFilter()
        {
                if (dgOrderReview.ActiveFilter.Criteria != null)
                {
                    string curFilter = dgOrderReview.ActiveFilter.Criteria.ToString();
                    string[] arrCriteria = curFilter.Split('[');
                    string newFilter = "";

                    foreach (string curCriteria in arrCriteria)
                    {
                        if (curCriteria.Contains("Status") == false && curCriteria != "") { newFilter += "[" + curCriteria; };
                    }
                    if (newFilter.Length > 4)
                    {
                        if (newFilter.Substring(newFilter.Length - 4, 4) == "And ") { newFilter = newFilter.Substring(0, newFilter.Length - 4); };
                    }
                   
                    dgOrderReview.ActiveFilterString = newFilter;
                }
        }

        private void tbMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGrids();
        }

        private void dgOrderReview_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

                GridCellInfo temp = (GridCellInfo)e.Cell;
                Order curRow = (Order)dgOrderReview.GetRow(temp.RowHandle);

                if (curRow.Status == "Cancelada")
                {
                    e.Appearance.ForeColor = Color.Red;
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                if (curRow.Split == 1)
                {
                    e.Appearance.ForeColor = Color.DarkGoldenrod; 
                    //e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrids();
        }

        private class Order
        {
            List<Trade> _OrderTrades = new List<Trade>(); public List<Trade> OrderTrades { get { return _OrderTrades; } }

            string _CancelAll = "Cancel"; public string CancelAll { get { return "Cancel"; } }
            string _CancelLeaves = "Cancel"; public string CancelLeaves { get { return "Cancel"; } }
            double _IdOrder = 0; public double IdOrder { get { return _IdOrder; } set { _IdOrder = value; } }
            public int IdPortfolio = 0;
            string _Portfolio = ""; public string Portfolio { get { return _Portfolio; } set { _Portfolio = value; } }
            public Int32 IdTicker = 0;
            string _Ticker = ""; public string Ticker { get { return _Ticker; } set { _Ticker = value; } }
            int _IdBook = 0; public int IdBook { get { return _IdBook; } set { _IdBook = value; } }
            string _Book = ""; public string Book { get { return _Book; } set { _Book = value; } }
            int _IdSection = 0; public int IdSection { get { return _IdSection; } set { _IdSection = value; } }
            string _Section = ""; public string Section { get { return _Section; } set { _Section = value; } }
            public double _OrderPrice = 0; public double OrderPrice { get { return _OrderPrice; } set { _OrderPrice = value; } }
            double _Total = 0; public double Total { get { return _Total; } set { _Total = value; } }
            double _Done = 0; public double Done { get { return _Done; } set { _Done = value; } }
            double _Leaves = 0; public double Leaves { get { return _Leaves; } set { _Leaves = value; } }
            double _CashDone = 0; public double CashDone { get { return _CashDone; } set { _CashDone = value; } }
            double _AvgPriceDone = 0; public double AvgPriceDone { get { return _AvgPriceDone; } set { _AvgPriceDone = value; } }
            public Int32 IdCurrency = 0;
            string _Currency = ""; public string Currency { get { return _Currency; } set { _Currency = value; } }
            DateTime _OrderDate = new DateTime(1900, 01, 01); public DateTime OrderDate { get { return _OrderDate; } set { _OrderDate = value; } }
            public int IdLogin = 0;
            string _Login = ""; public string Login { get { return _Login; } set { _Login = value; } }
            public int IdStatus = 0;
            string _Status = ""; public string Status { get { return _Status; } set { _Status = value; } }
            int _IdBroker = 0; public int IdBroker { get { return _IdBroker; } set { _IdBroker = value; } }
            string _Broker = ""; public string Broker { get { return _Broker; } set { _Broker = value; } }
            int _Split = 0; public int Split { get { return _Split; } set { _Split = value; } }
            public string IdOrderBroker = "";

            public int IdSubPortfolio = 0;
            public string SubPortfolio = "";

            public int IdStrategy = 0;
            public string Strategy = "";

            public int IdSubStrategy = 0;
            public string SubStrategy = "";

            public string Side { get { if(_Total>0) return "BUY"; else return "SELL";} }

            public void AddTrade(Trade TradeToAdd)
            {
                _OrderTrades.Add(TradeToAdd);
            }

        }

        private class Trade
        {
            double _IdTrade = 0; public double IdTrade { get { return _IdTrade; } set { _IdTrade = value; } }
            double _IdOrder = 0; public double IdOrder { get { return _IdOrder; } set { _IdOrder = value; } }
            int _IdPortfolio = 0; public int IdPortfolio { get { return _IdPortfolio; } set { _IdPortfolio = value; } }
            string _Portfolio = ""; public string Portfolio { get { return _Portfolio; } set { _Portfolio = value; } }
            Int32 _IdTicker = 0; public Int32 IdTicker { get { return _IdTicker; } set { _IdTicker = value; } }

            string _Cancel = "Cancel"; public string Cancel { get { return "Cancel"; } }
            string _Edit = "Edit"; public string Edit { get { return "Edit"; } }

            double _TradeQuantity = 0; public double TradeQuantity { get { return _TradeQuantity; } set { _TradeQuantity = value; } }
            double _TradePrice = 0; public double TradePrice { get { return _TradePrice; } set { _TradePrice = value; } }
            double _TradeCash = 0; public double TradeCash { get { return _TradeCash; } set { _TradeCash = value; } }
            string _TradeStatus = ""; public string TradeStatus { get { return _TradeStatus; } set { _TradeStatus = value; } }
            int _IdTradeStatus = 0; public int IdTradeStatus { get { return _IdTradeStatus; } set { _IdTradeStatus = value; } }
            DateTime _TimeStamp = new DateTime(1900, 01, 01); public DateTime TimeStamp { get { return _TimeStamp; } set { _TimeStamp = value; } }
        }


    
    
    }
}