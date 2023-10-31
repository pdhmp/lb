using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;

using LiveBook.Business;
using System.Collections;
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
using System.IO;
using System.Globalization;

namespace LiveBook
{
    public partial class frmTrades_group : Form
    {
        private Business_Class Negocios = new Business_Class();
        private LB_Utils curUtils = new LB_Utils();
        private newNestConn curConn = new newNestConn();

        private bool show = false;
        public int Id_User;

        public frmTrades_group()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtgTrade.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgTrade.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgTrade.LookAndFeel.SetSkinStyle("Blue");
            
                NestDLL.FormUtils.LoadCombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type in (1,2)", "Id_Portfolio", "Port_Name", 1);

        }

        public void Carrega_Grid()
        {
            string SQLString;

            SQLString = "Select [Id Ticker],NestTicker,[Id Broker],Broker,[Trade Type],Sum([Trade Quantity])as [Trade Quantity], " +
            "sum([Trade Price]*[Trade Quantity]) as [Trade Cash],sum([Trade Price]*[Trade Quantity])/case when Sum([Trade Quantity])=0 then 1 else Sum([Trade Quantity])end [Avg Price],[Trade Date],[RoundLot],[Book],Sub_Portfolio,Strategy,Sub_Strategy,[Section],[Old Estrategy],[Old Sub Estrategy] from " +
                " ( SELECT A.*,case when [Trade Quantity]>0 then 'Buy' else 'Sell' end [Trade Type] FROM dbo.VW_LB_Trades  A" +
              //  " inner join dbo.Tb001_Securities B ON A.[Id Ticker] = B.IdSecurity " +
            " Where [Trade Date] >='" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") + "' and [Trade Date]<= '" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") +"'" +
            " and [Id Trade Status]<>4 " +
            " and [Id Portfolio]=" + cmbChoosePortfolio.SelectedValue.ToString() +
            " ) A group by [Id Ticker],NestTicker,[Id Broker],Broker,[Trade Type],[Trade Date],[RoundLot],[Book],Sub_Portfolio,Strategy,Sub_Strategy,[Section],[Old Estrategy],[Old Sub Estrategy]";
            
            DataTable tablet = curConn.Return_DataTable(SQLString);

            dtgTrade.DataSource = tablet;

            curUtils.SetColumnStyle(dgTradesGroup, 2,"Trade Quantity");
            
            dgTradesGroup.Columns["Trade Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTradesGroup.Columns["Trade Quantity"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgTradesGroup.Columns["Trade Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTradesGroup.Columns["Trade Cash"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgTradesGroup.Columns["Avg Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTradesGroup.Columns["Avg Price"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

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

        private Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return Rectangle.Empty;
        }

        private void ShowColumnSelector() 
        { 
            ShowColumnSelector(true, dgTradesGroup); 
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


        private void button4_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                string user = Environment.UserName.ToString();
                string Loca_Machine = Environment.MachineName.ToString();
                string fileName_Log = "T:\\Log\\Reports\\Trades_Id_LB_" + Id_User + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Portfolio_" + cmbChoosePortfolio.Text + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";
                ExportTo(new ExportXlsProvider(fileName_Log), dgTradesGroup);

                ExportTo(new ExportXlsProvider(fileName), dgTradesGroup);
                OpenFile(fileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Text Document", "Text Files|*.txt");
            if (fileName != "")
            {
                ExportTo(new ExportTxtProvider(fileName), dgTradesGroup);
                OpenFile(fileName);
            }
        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgTradesRel_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgTradesRel_EndGrouping(object sender, EventArgs e)
        {
            dgTradesGroup.ExpandAllGroups();

            dgTradesGroup.GroupSummary.Add(SummaryItemType.Sum, "Trade Quantity", dgTradesGroup.Columns["Trade Quantity"]);
            ((GridSummaryItem)dgTradesGroup.GroupSummary[dgTradesGroup.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgTradesGroup.GroupSummary.Add(SummaryItemType.Sum, "Avg Price", dgTradesGroup.Columns["Avg Price"]);
            ((GridSummaryItem)dgTradesGroup.GroupSummary[dgTradesGroup.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgTradesGroup.GroupSummary.Add(SummaryItemType.Sum, "Trade Cash", dgTradesGroup.Columns["Trade Cash"]);
            ((GridSummaryItem)dgTradesGroup.GroupSummary[dgTradesGroup.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
        }

        private void dgTradesRel_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgTradesGroup);

        }

        private void dgTradesRel_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgTradesGroup);

        }

        private void dgTradesRel_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgTradesGroup);
        }

        private void dgTradesRel_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgTradesGroup);
        }

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

        private void dtpEndDate_CloseUp(object sender, EventArgs e)
        {
            if (dtpIniDate.Value > dtpEndDate.Value)
                dtpIniDate.Value = dtpEndDate.Value;
        }

        private void dtpIniDate_CloseUp(object sender, EventArgs e)
        {
            if (dtpEndDate.Value < dtpIniDate.Value)
                dtpEndDate.Value = dtpIniDate.Value;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string SQLString = @"  Select	 [Trade Type] AS SIDE
		                                    ,[NestTicker] AS SYMBOL
		                                    ,ROUND(ABS(SUM([Trade Quantity])),0) AS QTY
		                                    ,ROUND(SUM([Trade Price]*[Trade Quantity])/CASE WHEN SUM([Trade Quantity])=0 THEN 1 ELSE SUM([Trade Quantity]) END, 6) AS PRICE
		                                    ,'Nest' AS OPERADOR
		                                    ,[Trade Date] AS DATA_OPERACAO
                                            ,39289 AS CGE_CORRRETORA
                                            ,4607 AS CGE_CONTRAPARTE
                                            ,0 AS COMISSAO
                                            ,'' AS ESTRATEGIA
                                            ,'' AS CAMBIO
                                            ,'' AS OFFICER
                                            ,'' AS [ ]
                                            ,'' AS [  ]
                                            ,'' AS [   ]
                                            ,'' AS [    ]
                                            ,'' AS CANCELAMENTO_ADR
                                            ,'' AS [     ]
                                            ,'' AS TIPO_BOLETA
                                            ,'' AS EXERCICIO_OPCAO
                                            ,'' AS [NDF/DF]
                                            ,'' AS FIX_DATE
                                    FROM	( 
			                                    SELECT	A.*, CASE WHEN [Trade Quantity]>0 THEN 'B' ELSE 'S' END [Trade Type]
			                                    FROM	dbo.VW_LB_Trades  A
			                                    WHERE	[Trade Date] >= '" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") + @"'
                                                        and [Trade Date] <= '" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") + @"'	
				                                        and [Id Trade Status] <> 4 
					                                    and [Id Portfolio] = 6
		                                    ) AS X 
                                    GROUP	BY [Trade Date],NestTicker,[Trade Type]
                                    ORDER	BY [Trade Date],NestTicker,[Trade Type]
                                ";
            DataTable oTable = curConn.Return_DataTable(SQLString);
            string fileName = @"N:\Operations\Boletagem\Boletagem OffShore\Boletagem Final do Dia\UBS Prime\2011\ Modelo UBS_Renda Variavel Inter "+ DateTime.Today.ToString("ddMMyyyy") + ".xls";
            //string fileName = @"C:\" + DateTime.Today.ToString("yyyyMMdd") + "_xlsexport.xlsx";
            if (fileName != "")
            {

                System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                ExportExcel(oTable, fileName);
                System.Threading.Thread.CurrentThread.CurrentCulture = CurrentCI; 
            }

            (new frmDataTable(oTable)).ShowDialog(this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string SQLString = @"   Select	 [Trade Date] AS OperationDate
		                                    ,'NEST MH FUND' AS PortfolioName
		                                    ,[Trade Type] AS Side
		                                    ,'BOVESPA' AS Exchange
		                                    ,COALESCE(Symbol,'') AS Symbol
		                                    ,'C SUISSE' AS Broker
		                                    ,ROUND(ABS(SUM([Trade Quantity])),0) AS Qty
		                                    ,ROUND((SUM([Trade Price]*[Trade Quantity])/(CASE WHEN SUM([Trade Quantity])=0 THEN 1 ELSE SUM([Trade Quantity]) END)) *
                                                   (CASE [IdInstrument] WHEN 3 THEN 100 ELSE 1 END), 6) AS [Price]
                                            ,SymbolType
                                    FROM	( 
			                                    SELECT	A.*, CASE WHEN [Trade Quantity]>0 THEN 'C' ELSE 'V' END [Trade Type], COALESCE(NULLIF(B.ExchangeTicker,''), '!@#$%_' + A.NestTicker) AS Symbol, A.Instrument AS SymbolType
			                                    FROM	[NESTDB].[dbo].[VW_LB_Trades] A
					                                    LEFT JOIN [NESTDB].[dbo].[Tb001_Securities] B ON A.[Id Ticker] = B.IdSecurity 
			                                    WHERE	[Trade Date] >= '" + Convert.ToDateTime(dtpIniDate.Value.ToString()).ToString("yyyyMMdd") + @"'
                                                        and [Trade Date] <= '" + Convert.ToDateTime(dtpEndDate.Value.ToString()).ToString("yyyyMMdd") + @"'	
				                                        and [Id Trade Status] <> 4 
					                                    and [Id Portfolio] = 42
		                                    ) AS X
                                    GROUP	BY Portfolio,[Trade Date],SymbolType,Symbol,[Trade Type],[IdInstrument]
                                    ORDER	BY Portfolio,[Trade Date],SymbolType,Symbol,[Trade Type],[IdInstrument]
                                ";
            DataTable oTable = curConn.Return_DataTable(SQLString);
            string sFilePath = @"N:\Trading\Boletagem\Overseas\"; // @"C:\";

            System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            DataRow[] oRows = oTable.Select("[SymbolType] = 'Futures'");
            if (oRows.Length > 0)
            {
                using (StreamWriter oWriter = new StreamWriter(sFilePath + DateTime.Today.ToString("yyyy-MM-dd") + "Futuro_.txt"))
                {
                    oWriter.AutoFlush = true;
                    oWriter.WriteLine("0#FU");
                    foreach (DataRow curRow in oRows)
                    {
                        oWriter.WriteLine(
                            "#" + ((DateTime)curRow["OperationDate"]).ToString("yyyyMMdd") +
                            "#" + curRow["PortfolioName"] +
                            "#" + curRow["Symbol"] +
                            "#FUT IBE#" + curRow["Side"] +
                            "#" + ((double)curRow["Qty"]).ToString("#0") +
                            "#" + ((double)curRow["Price"]).ToString("#0.0000") +
                            "#" + curRow["Broker"] +
                            "#N#N#99###RV1##" + curRow["Broker"] +
                            "#0##");
                    }
                    oWriter.WriteLine("99#FU");
                    oWriter.Close();
                }
            }
            oRows = oTable.Select("[SymbolType] <> 'Futures'");
            if (oRows.Length > 0)
            {
                using (StreamWriter oWriter = new StreamWriter(sFilePath + DateTime.Today.ToString("yyyy-MM-dd") + "RendaVariavel.txt"))
                {
                    oWriter.AutoFlush = true;
                    oWriter.WriteLine("0#RV");
                    foreach (DataRow curRow in oRows)
                    {
                        oWriter.WriteLine(
                            "#" + ((DateTime)curRow["OperationDate"]).ToString("yyyyMMdd") +
                            "#" + curRow["PortfolioName"] +
                            "#" + curRow["Side"] +
                            "#N#" + curRow["Exchange"] +
                            "#" + curRow["Symbol"] +
                            "#V#" + curRow["Broker"] +
                            "#" + curRow["Broker"] +
                            "#" + ((double)curRow["Qty"]).ToString("#0") +
                            "#" + ((double)curRow["Price"]).ToString("#0.0000") +
                            "#99#N#######");
                    }
                    oWriter.WriteLine("99#RV");
                    oWriter.Close();
                }
            }

            System.Threading.Thread.CurrentThread.CurrentCulture = CurrentCI; 
            (new frmDataTable(oTable)).Show(this);
        }

        protected void ExportExcel(DataTable table, string filename)
        {
            if (table == null || table.Rows.Count == 0) 
                return;

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            if (xlApp == null)
                return;
            
            try
            {
                Microsoft.Office.Interop.Excel.Workbooks oWorkbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook oWorkbook = oWorkbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet oWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)oWorkbook.Worksheets[1];
                long lRowCount = table.Rows.Count;
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    oWorksheet.Cells[1, i + 1] = table.Columns[i].ColumnName;
                }
                for (int r = 0; r < table.Rows.Count; r++)
                {
                    for (int c = 0; c < table.Columns.Count; c++)
                    {
                        oWorksheet.Cells[r + 2, c + 1] = table.Rows[r][c].ToString();
                    }
                }

                ((Microsoft.Office.Interop.Excel.Range)oWorksheet.Cells[1, 6]).EntireColumn.NumberFormat = "dd/MM/yy";
                //xlApp.Visible = true;
                
                oWorkbook.Saved = true;
                oWorkbook.SaveCopyAs(filename);
                oWorkbook.Close(true, Type.Missing, Type.Missing);
                oWorkbook = null;
                xlApp.Quit();
                xlApp = null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to create Excel output filed","LiveBook", MessageBoxButtons.OK,  MessageBoxIcon.Exclamation);
            }
        }

    }
}