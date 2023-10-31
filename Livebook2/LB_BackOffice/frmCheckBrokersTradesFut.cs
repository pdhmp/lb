using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using LiveDLL;


namespace LiveBook
{
    public partial class frmCheckBrokersTradesFut : LBForm
    {
        DateTime FileDate;
        //LB_Utils curUtils = new LB_Utils();
        bool TradesLoaded = false;
        bool BrokerageLoaded = false;
        DataTable tableBrokerage;

        public frmCheckBrokersTradesFut()
        {
            InitializeComponent();
        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            FileDate = dtpIniDate.Value;
            if (FileDate.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                cmdDeleteFile.Enabled = true;
            }
            else
            {
                cmdDeleteFile.Enabled = false;
            }


            Load_Grid();
        }

        void Load_Grid()
        {
            Load_Grid_Trades();
            Load_Grid_Brokerage();
            curUtils.SetColumnStyle(dgBrokerageFut, 1);
        }
        public void Load_Grid_Trades()
        {
            DataTable dtTradeLines;

            using (newNestConn curConn = new newNestConn())
            {
                string SQLString;

                SQLString = "Select * FROM NESTIMPORT.dbo.FCN_Return_CheckQuantitiesBrokerFut('" + dtpIniDate.Value.ToString("yyyyMMdd") + "') order by TradeDate,IdBroker,IdPortfolio,IdSecurity,TradeType";
                dtTradeLines = curConn.Return_DataTable(SQLString);

                dtgDiferencesBrokerFut.DataSource = dtTradeLines;

                curUtils.SetColumnStyle(dgCheckDiferencesBrokerFut, 1, "DiferenceQuantity");

                dgCheckDiferencesBrokerFut.Columns["TradeQuantityFile"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBrokerFut.Columns["TradeQuantityFile"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBrokerFut.Columns["TradeQuantityNest"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBrokerFut.Columns["TradeQuantityNest"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBrokerFut.Columns["DiferenceQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBrokerFut.Columns["DiferenceQuantity"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBrokerFut.Columns["TradeCashFile"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBrokerFut.Columns["TradeCashFile"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBrokerFut.Columns["TradeCashNest"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBrokerFut.Columns["TradeCashNest"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBrokerFut.Columns["DiferenceCash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBrokerFut.Columns["DiferenceCash"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBrokerFut.Columns["AvgPriceFile"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBrokerFut.Columns["AvgPriceFile"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

                dgCheckDiferencesBrokerFut.Columns["AvgPriceNest"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBrokerFut.Columns["AvgPriceNest"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            }
            if (!TradesLoaded) { SetTradeTotals(); TradesLoaded = true; }
        }
        private void SetTradeTotals()
        {

            dgCheckDiferencesBrokerFut.CollapseAllGroups();

            dgCheckDiferencesBrokerFut.GroupSummary.Add(SummaryItemType.Sum, "TradeQuantityFile", dgCheckDiferencesBrokerFut.Columns["TradeQuantityFile"]);
            ((GridSummaryItem)dgCheckDiferencesBrokerFut.GroupSummary[dgCheckDiferencesBrokerFut.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBrokerFut.GroupSummary.Add(SummaryItemType.Sum, "TradeQuantityNest", dgCheckDiferencesBrokerFut.Columns["TradeQuantityNest"]);
            ((GridSummaryItem)dgCheckDiferencesBrokerFut.GroupSummary[dgCheckDiferencesBrokerFut.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBrokerFut.GroupSummary.Add(SummaryItemType.Sum, "DiferenceQuantity", dgCheckDiferencesBrokerFut.Columns["DiferenceQuantity"]);
            ((GridSummaryItem)dgCheckDiferencesBrokerFut.GroupSummary[dgCheckDiferencesBrokerFut.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBrokerFut.GroupSummary.Add(SummaryItemType.Sum, "TradeCashFile", dgCheckDiferencesBrokerFut.Columns["TradeCashFile"]);
            ((GridSummaryItem)dgCheckDiferencesBrokerFut.GroupSummary[dgCheckDiferencesBrokerFut.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBrokerFut.GroupSummary.Add(SummaryItemType.Sum, "TradeCashNest", dgCheckDiferencesBrokerFut.Columns["TradeCashNest"]);
            ((GridSummaryItem)dgCheckDiferencesBrokerFut.GroupSummary[dgCheckDiferencesBrokerFut.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBrokerFut.GroupSummary.Add(SummaryItemType.Sum, "DiferenceCash", dgCheckDiferencesBrokerFut.Columns["DiferenceCash"]);
            ((GridSummaryItem)dgCheckDiferencesBrokerFut.GroupSummary[dgCheckDiferencesBrokerFut.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBrokerFut.GroupSummary.Add(SummaryItemType.Max, "MatchCash", dgCheckDiferencesBrokerFut.Columns["MatchCash"]);
            dgCheckDiferencesBrokerFut.GroupSummary.Add(SummaryItemType.Max, "MatchQuantities", dgCheckDiferencesBrokerFut.Columns["MatchQuantities"]);
        }
        public void Load_Grid_Brokerage()
        {
            string SQLString;
            using (newNestConn curConn = new newNestConn())
            {
                SQLString = "Select * FROM NESTIMPORT.dbo.Fcn_Return_Cost_Futures('" + dtpIniDate.Value.ToString("yyyyMMdd") + "') order by DataPregao,IdBroker,IdPortfolio";
                tableBrokerage = curConn.Return_DataTable(SQLString);

                dtgBrokerageFut.DataSource = tableBrokerage;

                dgBrokerageFut.Columns["VendaDisponivel"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["VendaDisponivel"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["CompraDisponivel"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["CompraDisponivel"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["VendaOpcoes"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["VendaOpcoes"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["CompraOpcoes"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["CompraOpcoes"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["ValorNegocios"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["ValorNegocios"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["IRPF"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["IRPF"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["IRPFSDayTrade"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["IRPFSDayTrade"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["TaxasOperacionais"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["TaxasOperacionais"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["TaxaRegistroBMF"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["TaxaRegistroBMF"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["IRPFSCorretagem"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["IRPFSCorretagem"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["TaxasOPSIntermediacao"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["TaxasOPSIntermediacao"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["AjustePosicao"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["AjustePosicao"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["AjusteDayTrade"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["AjusteDayTrade"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["TotalDespesas"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["TotalDespesas"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["OutrosCustos"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["OutrosCustos"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["TotalContaInvestimento"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["TotalContaInvestimento"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["TotalContraNormal"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["TotalContraNormal"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["TotalLiquido"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["TotalLiquido"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["TotalLiquidoNota"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["TotalLiquidoNota"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["ISS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["ISS"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerageFut.Columns["Outros"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerageFut.Columns["Outros"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
            }

            if (!BrokerageLoaded) { BrokerageLoaded = true; }
        }

        private void frmCheckBrokersTradesFut_Load(object sender, EventArgs e)
        {
            dtgDiferencesBrokerFut.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgDiferencesBrokerFut.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgDiferencesBrokerFut.LookAndFeel.SetSkinStyle("Blue");

            dtgBrokerageFut.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgBrokerageFut.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgBrokerageFut.LookAndFeel.SetSkinStyle("Blue");

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

        private void CmdInsertBrokerage_Click(object sender, EventArgs e)
        {
            InsertBrokerage();
            MessageBox.Show("Inserted!");
            Load_Grid();
        }

        int InsertBrokerage()
        {
            string StringSQLBrokerage = "";
            string StringSQLExpenses = "";
            string StringSProcessed = "";

            int Id_Account = 0;

            foreach (DataRow row in tableBrokerage.Rows)
            {
                if (row[29].ToString() != "0" && row[10].ToString() != "INSERTED")
                {
                    StringSQLBrokerage = StringSQLBrokerage + " EXECUTE[NESTDB].[dbo].[proc_Insert_Brokerage] '" + Convert.ToDateTime(row["DataPregao"]).ToString("yyyyMMdd") + "'," + row["Id_Account"] + "  ,167675 ,1844," + row["TaxasOperacionais"].ToString().Replace(".", "").Replace(",", ".") + " ; ";

                    double Emolumentos = 0;

                    Emolumentos = Convert.ToDouble(row["TAXASBMF"]) + Convert.ToDouble(row["TaxaRegistroBMF"]) + Convert.ToDouble(row["OutrosCustos"]);

                    StringSQLExpenses = StringSQLExpenses + " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                  " VALUES ( 60,'" + Convert.ToDateTime(row["DataPregao"]).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(row["DataPregao"]).ToString("yyyyMMdd") + "'," + row["Id_Account"] + ",216346,5,1,0," + row["Id_Account"] + ",1844,5,1," + Emolumentos.ToString().Replace(".", "").Replace(",", ".") + ") ; ";

                    StringSProcessed = StringSProcessed + " UPDATE NESTIMPORT.dbo.Tb_ResumoConfirmacaoDiarioBrlFut SET INSERIDO =1 WHERE IDPORTFOLIO=" + row["IdPortfolio"] + " AND IDBROKER =" + row["IdBroker"] + "  AND DATAPREGAO='" + Convert.ToDateTime(row["DataPregao"]).ToString("yyyyMMdd") + "'; ";
                }
            }

            using (newNestConn curConn = new newNestConn())
            {
                int return1 = curConn.ExecuteNonQuery(StringSQLBrokerage);
                int return2 = curConn.ExecuteNonQuery(StringSQLExpenses);
                int return3 = curConn.ExecuteNonQuery(StringSProcessed);
            }


            return 1;

        }

        private void dgBrokerageFut_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgBrokerageFut);
        }

        private void dgBrokerageFut_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgBrokerageFut);
        }

        private void dgCheckDiferencesBrokerFut_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgCheckDiferencesBrokerFut);
        }

        private void dgCheckDiferencesBrokerFut_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int TempVal = e.RowHandle;
            GridView curView = (GridView)sender;


            if (curView.GetRowCellValue(TempVal, "MatchCash").ToString() == "MISMATCH" || curView.GetRowCellValue(TempVal, "MatchQuantities").ToString() == "MISMATCH")
            {
                if (e.Column.Name == "colMatchCash" || e.Column.Name == "colMatchQuantities")
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.ForeColor = Color.Black;
                }
                else
                {
                    e.Appearance.ForeColor = Color.Red;
                }
            }
            else
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Green;
            }
        }

        private void dgCheckDiferencesBrokerFut_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgCheckDiferencesBrokerFut);
        }
    }
}
