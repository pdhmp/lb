using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.IO;
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
    public partial class frmCheckImportedFile : LBForm
    {
        //Business_Class Negocios = new Business_Class();
        public int Id_User;
        DataTable tableBrokerage;
        DataTable dtTradeLines;
        DateTime FileDate;
        bool TradesLoaded = false;
        bool BrokerageLoaded = false;

        public frmCheckImportedFile()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtgDiferencesBroker.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgDiferencesBroker.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgDiferencesBroker.LookAndFeel.SetSkinStyle("Blue");

            dtgBrokerage.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgBrokerage.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgBrokerage.LookAndFeel.SetSkinStyle("Blue");


            dgBrokerage.ColumnPanelRowHeight = 32;

            dgBrokerage.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            Load_Grid_Trades();
            Load_Grid_Brokerage();
        }

        public void Load_Grid_Trades()
        {
            using (newNestConn curConn = new newNestConn())
            {
                DataTable dtTrades = new DataTable();

                string SQLString;

                SQLString = "Select * FROM NESTIMPORT.dbo.FCN_Return_CheckQuantitiesBroker('" + dtpIniDate.Value.ToString("yyyyMMdd") + "') order by TradeDate,IdBroker,IdPortfolio,IdSecurity,TradeType";
                dtTradeLines = curConn.Return_DataTable(SQLString);

                dtgDiferencesBroker.DataSource = dtTradeLines;

                dgCheckDiferencesBroker.Columns["TradeQuantityFile"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBroker.Columns["TradeQuantityFile"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBroker.Columns["TradeQuantityNest"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBroker.Columns["TradeQuantityNest"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBroker.Columns["DiferenceQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBroker.Columns["DiferenceQuantity"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBroker.Columns["TradeCashFile"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBroker.Columns["TradeCashFile"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBroker.Columns["TradeCashNest"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBroker.Columns["TradeCashNest"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBroker.Columns["DiferenceCash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBroker.Columns["DiferenceCash"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgCheckDiferencesBroker.Columns["AvgPriceFile"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBroker.Columns["AvgPriceFile"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

                dgCheckDiferencesBroker.Columns["AvgPriceNest"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCheckDiferencesBroker.Columns["AvgPriceNest"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            }
            if (!TradesLoaded) { SetTradeTotals(); TradesLoaded = true; }
        }

        private void SetTradeTotals()
        {
            curUtils.SetColumnStyle(dgCheckDiferencesBroker, 1);

            dgCheckDiferencesBroker.CollapseAllGroups();

            dgCheckDiferencesBroker.GroupSummary.Add(SummaryItemType.Sum, "TradeQuantityFile", dgCheckDiferencesBroker.Columns["TradeQuantityFile"]);
            ((GridSummaryItem)dgCheckDiferencesBroker.GroupSummary[dgCheckDiferencesBroker.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBroker.GroupSummary.Add(SummaryItemType.Sum, "TradeQuantityNest", dgCheckDiferencesBroker.Columns["TradeQuantityNest"]);
            ((GridSummaryItem)dgCheckDiferencesBroker.GroupSummary[dgCheckDiferencesBroker.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBroker.GroupSummary.Add(SummaryItemType.Sum, "DiferenceQuantity", dgCheckDiferencesBroker.Columns["DiferenceQuantity"]);
            ((GridSummaryItem)dgCheckDiferencesBroker.GroupSummary[dgCheckDiferencesBroker.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBroker.GroupSummary.Add(SummaryItemType.Sum, "TradeCashFile", dgCheckDiferencesBroker.Columns["TradeCashFile"]);
            ((GridSummaryItem)dgCheckDiferencesBroker.GroupSummary[dgCheckDiferencesBroker.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBroker.GroupSummary.Add(SummaryItemType.Sum, "TradeCashNest", dgCheckDiferencesBroker.Columns["TradeCashNest"]);
            ((GridSummaryItem)dgCheckDiferencesBroker.GroupSummary[dgCheckDiferencesBroker.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBroker.GroupSummary.Add(SummaryItemType.Sum, "DiferenceCash", dgCheckDiferencesBroker.Columns["DiferenceCash"]);
            ((GridSummaryItem)dgCheckDiferencesBroker.GroupSummary[dgCheckDiferencesBroker.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgCheckDiferencesBroker.GroupSummary.Add(SummaryItemType.Max, "MatchCash", dgCheckDiferencesBroker.Columns["MatchCash"]);
            dgCheckDiferencesBroker.GroupSummary.Add(SummaryItemType.Max, "MatchQuantities", dgCheckDiferencesBroker.Columns["MatchQuantities"]);
        }

        public void Load_Grid_Brokerage()
        {
            string SQLString;

            using (newNestConn curConn = new newNestConn())
            {
                int ShowInserted = 0;
                if (chkShowInserted.Checked) ShowInserted = 1;

                SQLString = "Select * FROM NESTIMPORT.dbo.Fcn_Return_Cost('" + dtpIniDate.Value.ToString("yyyyMMdd") + "', " + ShowInserted + ") order by DataPregao,IdBroker,IdPortfolio";
                tableBrokerage = curConn.Return_DataTable(SQLString);

                dtgBrokerage.DataSource = tableBrokerage;

                curUtils.SetColumnStyle(dgBrokerage, 1, "DiferenceQuantity");

                dgBrokerage.Columns["TotalCompras"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["TotalCompras"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["TotalVendas"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["TotalVendas"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["TotalTermo"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["TotalTermo"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["TotalAjusteDiario"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["TotalAjusteDiario"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["TotalNegocios"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["TotalNegocios"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["Corretagem"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["Corretagem"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["TaxasCBLC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["TaxasCBLC"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["TaxasBovespa"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["TaxasBovespa"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["TaxasOperaciONais"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["TaxasOperaciONais"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["OutrasDespesas"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["OutrasDespesas"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["IR Day Trade"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["IR Day Trade"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["IR Operacoes"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["IR Operacoes"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["TotalLiquido"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["TotalLiquido"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

                dgBrokerage.Columns["RebatePadrao"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["RebatePadrao"].DisplayFormat.FormatString = "p2";

                dgBrokerage.Columns["RebateEfetivo"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgBrokerage.Columns["RebateEfetivo"].DisplayFormat.FormatString = "p2";

            }

            if (!BrokerageLoaded) { BrokerageLoaded = true; }
        }

        void Load_Grid()
        {
            Load_Grid_Trades();
            Load_Grid_Brokerage();
            curUtils.SetColumnStyle(dgBrokerage, 1);
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

        private void dgTradesRel_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgCheckDiferencesBroker);

        }

        private void dgTradesRel_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgCheckDiferencesBroker);

        }

        private void dgTradesRel_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgCheckDiferencesBroker);

        }

        private void dgTradesRel_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgCheckDiferencesBroker);

        }
        private void ShowColumnSelector() { ShowColumnSelector(true, dgCheckDiferencesBroker); }

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
                string fileName_Log = "T:\\Log\\Reports\\Trades_Id_LB_" + Id_User + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";
                ExportTo(new ExportXlsProvider(fileName_Log), dgCheckDiferencesBroker);

                ExportTo(new ExportXlsProvider(fileName), dgCheckDiferencesBroker);
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
        private void ExportTo(IExportProvider provider, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
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
                catch (Exception e)
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
                ExportTo(new ExportTxtProvider(fileName), dgCheckDiferencesBroker);
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

        private void CmdInsertBrokerage_Click(object sender, EventArgs e)
        {
            InsertBrokerage();
            Load_Grid();
        }

        int InsertBrokerage()
        {
            string StringSQLBrokerage = "";
            string StringSQLExpenses = "";
            string StringSProcessed = "";

            foreach (DataRow row in tableBrokerage.Rows)
            {
                if (row[10].ToString() != "0" && row[10].ToString() != "INSERTED")
                {
                    StringSQLBrokerage = StringSQLBrokerage + " EXECUTE[NESTDB].[dbo].[proc_Insert_Brokerage] '" + Convert.ToDateTime(row["DataPregao"]).ToString("yyyyMMdd") + "'," + row["Id_Account"] + "  ,76837 ,1844," + row["Corretagem"].ToString().Replace(".", "").Replace(",", ".") + " ; ";

                    StringSQLExpenses = StringSQLExpenses + " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                  " VALUES ( 60,'" + Convert.ToDateTime(row["DataPregao"]).ToString("yyyyMMdd") + "','" + Convert.ToDateTime(row["DataPregao"]).ToString("yyyyMMdd") + "'," + row["Id_Account"] + ",76839,5,1,0," + row["Id_Account"] + ",1844,5,1," + row["TaxasOperacionais"].ToString().Replace(".", "").Replace(",", ".") + ") ; ";

                    StringSProcessed = StringSProcessed + " UPDATE NESTIMPORT.dbo.Tb_ResumoConfirmacaoDiario SET INSERIDO =1 WHERE IDPORTFOLIO=" + row["IdPortfolio"] + " AND IDBROKER =" + row["IdBroker"] + "  AND DATAPREGAO='" + Convert.ToDateTime(row["DataPregao"]).ToString("yyyyMMdd") + "' AND Instrumento='" + row["Instrumento"] + "'; ";
                }
            }

            using (newNestConn curConn = new newNestConn())
            {
                int return1 = curConn.ExecuteNonQuery(StringSQLBrokerage);
                int return2 = curConn.ExecuteNonQuery(StringSQLExpenses);
                int return3 = curConn.ExecuteNonQuery(StringSProcessed);
            }

            MessageBox.Show("Inserted!");

            return 1;

        }

        private void cmdExportMellon_Click(object sender, EventArgs e)
        {
            ExportEquities();
        }

        private void cmdFutures_Click(object sender, EventArgs e)
        {
            ExportFutures();
        }

        void ExportFutures()
        {
            string outFileName = @"N:\Trading\Boletagem\Mellon\" + dtpIniDate.Value.ToString("yyyy-MM-dd") + "Futuro2.txt";
            StreamWriter sw = new StreamWriter(outFileName);

            string SQLString = " SELECT DATAPREGAO " +
                                " , C.MellonExpName AS Fund " +
                                "  , 'FUT ' + left(ExchangeTicker,3) as Fut " +
                                "  ,right(ExchangeTicker,3) as Vencto " +
                                "  , TIPO " +
                                "  , QUANTIDADE " +
                                "  , Preco  " +
                                "  , B.MellonExpName AS Broker " +
                                "  , A.IdBroker " +
                                "  ,DefaultRebate " +
                                " FROM NESTIMPORT.dbo.Tb_NegociosBRL A (nolock) " +
                                " LEFT JOIN NESTDB.dbo.Brokers B (nolock) " +
                                " ON A.IdBroker=B.IdBroker " +
                                " LEFT JOIN NESTDB.dbo.Tb002_Portfolios C (nolock) " +
                                " ON A.IdPortfolio=C.Id_Portfolio " +
                                " LEFT JOIN NESTDB.dbo.Tb001_Securities D (nolock) " +
                                " ON A.IdSecurity=D.IdSecurity " +
                                " WHERE DATAPREGAO='" + dtpIniDate.Value.ToString("yyyyMMdd") + "' AND IdPortfolio<>4 and Instrumento in ('Fut')";

            using (newNestConn curconn = new newNestConn())
            {
                DataTable dtExpMellon = curconn.Return_DataTable(SQLString);

                sw.WriteLine("0#FU");

                foreach (DataRow curRow in dtExpMellon.Rows)
                {
                    string tempLine = "";

                    if (curRow["Broker"].ToString() == "NULL")
                    {
                        MessageBox.Show("No Mellon name found for Broker Id: " + curRow["IdBroker"].ToString() + "\r\n Export Failed!!", "Error on Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sw.Close();
                        sw = new StreamWriter(outFileName);
                        sw.Close();
                        return;
                    }

                    tempLine = tempLine + "#" + LiveDLL.Utils.ParseToDateTime(curRow["DATAPREGAO"]).ToString("yyyyMMdd");
                    tempLine = tempLine + "#" + curRow["Fund"].ToString();
                    tempLine = tempLine + "#" + curRow["Fut"].ToString();
                    tempLine = tempLine + "#" + curRow["Vencto"].ToString();
                    tempLine = tempLine + "#" + curRow["TIPO"].ToString();
                    tempLine = tempLine + "#" + ((int)LiveDLL.Utils.ParseToDouble(curRow["QUANTIDADE"])).ToString();
                    tempLine = tempLine + "#" + LiveDLL.Utils.ParseToDouble(curRow["Preco"]).ToString("0.00").Replace(",", ".");
                    tempLine = tempLine + "#" + curRow["Broker"].ToString();
                    tempLine = tempLine + "#" + "S";
                    tempLine = tempLine + "#" + "N";
                    tempLine = tempLine + "#99.95";
                    tempLine = tempLine + "###RV3";
                    tempLine = tempLine + "##" + curRow["Broker"].ToString();
                    tempLine = tempLine + "#" + "0";
                    tempLine = tempLine + "##";

                    sw.WriteLine(tempLine);
                }
                sw.WriteLine("99#FU");
                sw.Close();
            }
        }

        void ExportEquities()
        {
            string outFileName = @"N:\Trading\Boletagem\Mellon\" + dtpIniDate.Value.ToString("yyyy-MM-dd") + "RendaVariavel.txt";
            StreamWriter sw = new StreamWriter(outFileName);

            string SQLString = " SELECT INSTRUMENTO " +
                                " , DATAPREGAO " +
                                " , C.MellonExpName AS Fund " +
                                "  , TIPO " +
                                "  , case when INSTRUMENTO not in ('OPC','OPV') THEN ExchangeTicker else ExchangeTicker end NestTicker " +
                                "  , B.MellonExpName AS Broker " +
                                "  , QUANTIDADE " +
                                "  , Preco  " +
                                "  , A.IdBroker " +
                                "  ,DefaultRebate " +
                                " FROM NESTIMPORT.dbo.Tb_NegociosBRL A (nolock) " +
                                " LEFT JOIN NESTDB.dbo.Brokers B (nolock) " +
                                " ON A.IdBroker=B.IdBroker " +
                                " LEFT JOIN NESTDB.dbo.Tb002_Portfolios C (nolock) " +
                                " ON A.IdPortfolio=C.Id_Portfolio " +
                                " LEFT JOIN NESTDB.dbo.FCN001_Securities_All('" + dtpIniDate.Value.ToString("yyyyMMdd") + "') D " +
                                " ON A.IdSecurity=D.IdSecurity " +
                //" WHERE DATAPREGAO='" + dtpIniDate.Value.ToString("yyyyMMdd") + "' AND IdPortfolio<>4 and Instrumento in ('VIS','OPC','OPV','LEI')" +
                                " WHERE DATAPREGAO='" + dtpIniDate.Value.ToString("yyyyMMdd") + "' AND ( IdPortfolio <> 4 AND IdPortfolio <> 50 ) and Instrumento in ('VIS','OPC','OPV','LEI')" +
                                " AND FileName not in " +
                                " (Select distinct(FileName) FROM" +
                                " NESTIMPORT.dbo.Tb_NegociosBRL A (nolock) " +
                // " WHERE DATAPREGAO='" + dtpIniDate.Value.ToString("yyyyMMdd") + "' AND IdPortfolio<>4 and Instrumento='Ter' " +
                                " WHERE DATAPREGAO='" + dtpIniDate.Value.ToString("yyyyMMdd") + "' AND ( IdPortfolio <> 4 AND IdPortfolio <> 50 ) and Instrumento='Ter' " +
                                " AND Tipo ='V')";

            using (newNestConn curconn = new newNestConn())
            {
                DataTable dtExpMellon = curconn.Return_DataTable(SQLString);

                sw.WriteLine("0#RV");

                foreach (DataRow curRow in dtExpMellon.Rows)
                {
                    string tempLine = "";

                    if (curRow["Broker"].ToString() == "NULL")
                    {
                        MessageBox.Show("No Mellon name found for Broker Id: " + curRow["IdBroker"].ToString() + "\r\n Export Failed!!", "Error on Export", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        sw.Close();
                        sw = new StreamWriter(outFileName);
                        sw.Close();
                        return;
                    }

                    tempLine = tempLine + "#" + LiveDLL.Utils.ParseToDateTime(curRow["DATAPREGAO"]).ToString("yyyyMMdd");
                    tempLine = tempLine + "#" + curRow["Fund"].ToString();
                    tempLine = tempLine + "#" + curRow["TIPO"].ToString();
                    tempLine = tempLine + "#" + "N";
                    tempLine = tempLine + "#" + "BOVESPA";
                    tempLine = tempLine + "#" + curRow["NestTicker"].ToString();
                    tempLine = tempLine + "#" + "V";
                    tempLine = tempLine + "#" + curRow["Broker"].ToString();

                    //if (curRow["Fund"].ToString() == "NEST QUANT MAST")
                    //{
                    //    tempLine = tempLine + "#0497";
                    //}

                    //else
                    //{
                    //    tempLine = tempLine + "#" + curRow["Broker"].ToString();
                    //}

                    tempLine = tempLine + "#" + curRow["Broker"].ToString();

                    tempLine = tempLine + "#" + ((int)LiveDLL.Utils.ParseToDouble(curRow["QUANTIDADE"])).ToString();
                    tempLine = tempLine + "#" + LiveDLL.Utils.ParseToDouble(curRow["Preco"]).ToString("0.00").Replace(",", ".");
                    tempLine = tempLine + "#" + Math.Round((LiveDLL.Utils.ParseToDouble(curRow["DefaultRebate"]) * 100), 2).ToString("#.##").Replace(",", ".");
                    // tempLine = tempLine + "#" + Math.Round((LiveDLL.Utils.ParseToDouble(curRow["DefaultRebate"]) * 100), 0).ToString("#.##").Replace(",", ".");
                    tempLine = tempLine + "#" + "S";
                    tempLine = tempLine + "#######";

                    sw.WriteLine(tempLine);
                }
                sw.WriteLine("99#RV");
                sw.Close();
            }
        }

        private void cmdDeleteFile_Click(object sender, EventArgs e)
        {
            frmDeleteFileBrokerage DeleteFile = new frmDeleteFileBrokerage();
            DeleteFile.ShowDialog();

        }

        private void lblOpenFolder_Click(object sender, EventArgs e)
        {
            string myPath = @"N:\Trading\Boletagem\Mellon\";
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = myPath;
            prc.Start();


        }

        private void dgCheckDiferencesBroker_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
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

        private void dgBrokerage_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int TempVal = e.RowHandle;
            GridView curView = (GridView)sender;
            int x = 0;

            if (curView.GetRowCellValue(TempVal, "RebatePadrao").ToString() != curView.GetRowCellValue(TempVal, "RebateEfetivo").ToString() && curView.GetRowCellValue(TempVal, "Instrumento").ToString() != "TER")
            {
                x++;
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.Black;
            }
            if (double.Parse(curView.GetRowCellValue(TempVal, "CAPValue").ToString()) > 0)
            {
                if (double.Parse(curView.GetRowCellValue(TempVal, "CAPValue").ToString()) <= double.Parse(curView.GetRowCellValue(TempVal, "CumulativeBrokerage").ToString()))
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.ForeColor = Color.Black;
                    x++;
                }
            }

            if (x == 0)
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.Green;
            }

            if (x == 2)
            {
                e.Appearance.BackColor = Color.MediumOrchid;
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void dgBrokerage_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgBrokerage);
        }

        private void dgBrokerage_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgBrokerage);

        }

        private void chkShowInserted_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowInserted.Checked)
                CmdInsertBrokerage.Enabled = false;
            else
                CmdInsertBrokerage.Enabled = true;
        }
    }
}