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
using SGN.Business;
using SGN.Validacao;

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SGN
{
    public partial class frmMargin : LBForm
    {

        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        RefreshHelper hlprMargin;

        public frmMargin()
        {
            InitializeComponent();
        }

        private void frmMargin_Load(object sender, EventArgs e)
        {
            

            //radYear.Checked = true;
            dtgMargin.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgMargin.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgMargin.LookAndFeel.SetSkinStyle("Blue");

            dtgMarginTransactions.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgMarginTransactions.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgMarginTransactions.LookAndFeel.SetSkinStyle("Blue");

            dtgMgReceivedFiles.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgMgReceivedFiles.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgMgReceivedFiles.LookAndFeel.SetSkinStyle("Blue");

            dtgNRMargin.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgNRMargin.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgNRMargin.LookAndFeel.SetSkinStyle("Blue");

            Load_Positions(); 

        }

        void Load_Positions()
        {

            string SQLString;
            DataTable tablep = new DataTable();

            hlprMargin = new RefreshHelper(dgMargin, "Id_Portfolio");
            hlprMargin.SaveViewInfo();
            int curRow = dgMargin.FocusedRowHandle; 

            dgMargin.Columns.Clear();

           //dgMargin.GroupSummary[0].SummaryType;
            SQLString = "SELECT Z.*, D.Nome AS Broker, NestTicker AS Ticker, B.Id_Portfolio, Port_Name AS Portfolio, D.Id_Corretora AS Id_Broker, " +
                        " CASE WHEN (DIFF=0 AND E.LastReceived=MaxFileDate) THEN 'Match' " +
                        " 	WHEN (DIFF>0 AND E.LastReceived=MaxFileDate) THEN 'Deposit' " +
                        " 	WHEN (DIFF<0 AND E.LastReceived=MaxFileDate) THEN 'Withdraw' " +
                        " 	ELSE 'Not Received' " +
                        " END AS 'Action',  " +
                        " Quantity*Last_Px AS FinAmount,  " +
                        " Quantity*Last_Px*(1-Discount) AS MarginValue " +
                        " FROM   " +
                        " (  " +
                        " 	SELECT  " +
                        "   dbo.FCN_GET_Margin_Discount(Y.NestTicker, getdate(), Quantity) AS Discount, " +
                        "   dbo.FCN_GETD_RT_Value_Only(Y.Id_Ticker, 1, 0, 0) AS Last_Px, " +
                        " 	COALESCE(Y.Id_Account, X.Id_Account) AS Id_Account, " +
                        " 	COALESCE(Y.Id_Ticker, X.Id_Ticker) AS Id_Ticker, " +
                        " 	Y.Quantity, X.Quantidade AS Quantity_CBLC, " +
                        " 	COALESCE(X.Quantidade,0)-COALESCE(Y.Quantity,0) AS DIFF, (SELECT MAX(File_Date) AS MaxLastReceived FROM dbo.Tb712_MarginAssets_Import) AS MaxFileDate  " +
                        " 	FROM   " +
                        " 	(  " +
                        " 		SELECT Id_Account, Id_Ticker, SUM(Quantity) AS Quantity " +
                        " 		FROM dbo.Tb712_MarginAssets " +
                        " 		WHERE Status<> 4 " +
                        " 		GROUP BY Id_Account, Id_Ticker " +
                        " 	) AS Y  " +
                        " 	FULL OUTER JOIN   " +
                        " 	(  " +
                        " 	SELECT Id_Account, Id_Ticker, SUM(Quantidade) AS Quantidade FROM " +
                        " 	(  " +
                        " 		SELECT * FROM dbo.Tb712_MarginAssets_Import " +
                        " 		WHERE File_Date=(SELECT MAX(File_Date) FROM Tb712_MarginAssets_Import) " +
                        " 	) AS A  " +
                        " 	GROUP BY Id_Account, Id_Ticker " +
                        " 	) AS X " +
                        " 	ON X.Id_Account=Y.Id_Account " +
                        " 	AND X.Id_Ticker=Y.Id_Ticker " +
                        " ) AS Z " +
                        " LEFT JOIN dbo.Tb003_PortAccounts L  " +
                        " ON Z.Id_Account=L.Id_Account  " +
                        " LEFT JOIN dbo.Tb007_Accounts M  " +
                        " ON Z.Id_Account=M.Id_Account  " +
                        " LEFT JOIN dbo.Tb002_Portfolios B " +
                        " ON L.Id_Portfolio=B.Id_Portfolio " +
                        " inner join dbo.Tb001_Securities C " +
                        " ON Z.Id_Ticker = C.IdSecurity " +
                        " inner join Tb011_Corretoras D " +
                        " ON M.Id_Broker = D.Id_Corretora " +
                        " LEFT JOIN " +
                        " ( " +
                        " SELECT Id_Account, MAX(File_Date) AS LastReceived " +
                        " FROM dbo.Tb712_MarginAssets_Import " +
                        " GROUP BY Id_Account " +
                        " ) AS E " +
                        " ON Z.Id_Account=E.Id_Account " +
                        " WHERE Id_Port_Type=1 AND Quantity<>0 OR Quantity_CBLC<>0";


            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgMargin.DataSource = tablep;
            

            // Renew Button
            dgMargin.Columns.AddField("Update");
            dgMargin.Columns["Update"].VisibleIndex = 2;
            dgMargin.Columns["Update"].Width = 60;
            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Update";
            dtgMargin.RepositoryItems.Add(item5);
            dgMargin.Columns["Update"].ColumnEdit = item5;
            dgMargin.Columns["Update"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgMargin.OptionsBehavior.Editable = false;
            dgMargin.Columns["Update"].Visible = true;
 
            dgMargin.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgMargin.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgMargin.Columns["Quantity_CBLC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgMargin.Columns["Quantity_CBLC"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgMargin.Columns["DIFF"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgMargin.Columns["DIFF"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgMargin.Columns["MarginValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgMargin.Columns["MarginValue"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgMargin.Columns["FinAmount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgMargin.Columns["FinAmount"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgMargin.Columns["Last_Px"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgMargin.Columns["Last_Px"].DisplayFormat.FormatString = "0.00;(0.00)";

            dgMargin.Columns["Discount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgMargin.Columns["Discount"].DisplayFormat.FormatString = "0.00%;(0.00%)";

            Valida.SetColumnStyle(dgMargin, 1);
            hlprMargin.LoadViewInfo();
            dgMargin.FocusedRowHandle = curRow; 
        }

        void Load_Transactions()
        {

            string SQLString;
            
            DataTable tablep = new DataTable();

            dgMarginTransactions.Columns.Clear();

            SQLString = "SELECT A.Id_Margin, A.Trade_Date, A.Quantity, D.Nome AS Broker, NestTicker, Port_Name AS Portfolio " + 
                 " FROM  " + 
                 " ( " +
                 " 		SELECT Id_Margin, Trade_Date, Id_Account, Id_Ticker, Quantity " + 
                 " 		FROM dbo.Tb712_MarginAssets   " + 
                 " 		WHERE Status<> 4  " +
                 " ) A " +
                 " LEFT JOIN dbo.Tb003_PortAccounts L " +
                 " ON A.Id_Account=L.Id_Account " +
                 " LEFT JOIN dbo.Tb007_Accounts M " +
                 " ON A.Id_Account=M.Id_Account " +
                 " LEFT JOIN dbo.Tb002_Portfolios B " +
                 " ON L.Id_Portfolio=B.Id_Portfolio " +
                 " inner join dbo.Tb001_Securities C " +
                 " ON A.Id_Ticker = C.IdSecurity " +
                 " inner join Tb011_Corretoras D " +
                 " ON M.Id_Broker = D.Id_Corretora " +
                 " WHERE Id_Port_Type=1" +
                 " ORDER BY Trade_Date";

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgMarginTransactions.DataSource = tablep;
            

            // Cancel Button
            dgMarginTransactions.Columns.AddField("Cancel");
            dgMarginTransactions.Columns["Cancel"].VisibleIndex = 0;
            dgMarginTransactions.Columns["Cancel"].Width = 60;
            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Cancel";
            dtgMarginTransactions.RepositoryItems.Add(item3);
            dgMarginTransactions.Columns["Cancel"].ColumnEdit = item3;
            dgMarginTransactions.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgMarginTransactions.OptionsBehavior.Editable = false;
            dgMarginTransactions.Columns["Cancel"].Visible = true;

            dgMarginTransactions.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgMarginTransactions.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";
            
            dgMarginTransactions.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgMarginTransactions.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgMarginTransactions.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            Valida.SetColumnStyle(dgMarginTransactions, 1);
           
            
        }

        void Load_RecFiles()
        {

            string SQLString;
            
            DataTable tablep = new DataTable();

            dgMgReceivedFiles.Columns.Clear();

            SQLString = "SELECT Nome, Port_Name, MAX(File_Date) AS LastReceived, COALESCE(NoDeposits, 0) AS NoDeposits  " +
                        "FROM dbo.Tb712_MarginAssets_Import A  " +
                        " LEFT JOIN dbo.Tb003_PortAccounts L  " +
                        " ON A.Id_Account=L.Id_Account  " +
                        " LEFT JOIN dbo.Tb007_Accounts M  " +
                        " ON A.Id_Account=M.Id_Account  " +
                        "LEFT JOIN dbo.Tb011_Corretoras B  " +
                        "ON M.Id_Broker=B.Id_Corretora  " +
                        "LEFT JOIN dbo.Tb002_Portfolios C  " +
                        "ON L.Id_Portfolio=C.Id_Portfolio  " +
                        "LEFT JOIN   " +
                        "(  " +
                        "	SELECT Id_Account,COUNT(Quantity) AS NoDeposits  " +
                        "	FROM (  " +
                        "		SELECT Id_Account, Id_Ticker, SUM(Quantity) AS Quantity " +
                        "		FROM dbo.Tb712_MarginAssets   " +
                        "		WHERE Status<> 4  " +
                        "		GROUP BY Id_Account, Id_Ticker " +
                        "	) AS X   " +
                        "	WHERE Quantity<>0  " +
                        "	GROUP BY Id_Account " +
                        ") AS Y  " +
                        "ON A.Id_Account=Y.Id_Account " +
                        " WHERE Id_Port_Type=1" +
                        "GROUP BY Nome, Port_Name, NoDeposits  " +
                        "ORDER BY MAX(File_Date),Nome, Port_Name, NoDeposits";

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgMgReceivedFiles.DataSource = tablep;
            

            dgMgReceivedFiles.Columns["LastReceived"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgMgReceivedFiles.Columns["LastReceived"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgMgReceivedFiles.Columns["LastReceived"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            dgMgReceivedFiles.Columns["Nome"].BestFit();
            dgMgReceivedFiles.Columns["Port_Name"].BestFit();
            dgMgReceivedFiles.Columns["LastReceived"].BestFit();
            dgMgReceivedFiles.Columns["NoDeposits"].BestFit();
        }

        void Load_NRLoans()
        {

            string SQLString;
            
            DataTable tablep = new DataTable();

            dgNRMargin.Columns.Clear();

            SQLString = "SELECT L.*, D.Nome, B.Port_Name AS Portfolio FROM " +
                        " (  " +
                        "     SELECT * FROM (  " +
                        "     SELECT Id_Account, MAX(File_Date) AS LastReceived, (SELECT MAX(File_Date) FROM dbo.Tb712_MarginAssets_Import) AS MAXDATE  " +
                        "     FROM dbo.Tb712_MarginAssets_Import  " +
                        "     GROUP BY Id_Account  " +
                        " ) AS A  " +
                        " WHERE LastReceived <> MAXDATE  " +
                        " ) AS K  " +
                        " RIGHT JOIN " +
                        " (  " +
                        "     SELECT * FROM " +
                        "     (  " +
                        "         SELECT A.Id_Account, A.Trade_Date, Quantity, NestTicker " +
                        "         FROM dbo.Tb712_MarginAssets A " +
                        "         LEFT JOIN dbo.Tb001_Securities C " +
                        "         ON A.Id_Ticker = C.IdSecurity " +
                        "         AND Status<> 4 " +
                        "     ) AS Q WHERE Quantity<>0 " +
                        " ) AS L  " +
                        " ON K.Id_Account=L.Id_Account  " +
                        " LEFT JOIN dbo.Tb003_PortAccounts X  " +
                        " ON L.Id_Account=X.Id_Account  " +
                        " LEFT JOIN dbo.Tb007_Accounts Y  " +
                        " ON X.Id_Account=Y.Id_Account  " +
                        " LEFT JOIN dbo.Tb002_Portfolios B " +
                        " ON X.Id_Portfolio=B.Id_Portfolio " +
                        " inner join Tb011_Corretoras D " +
                        " ON Y.Id_Broker = D.Id_Corretora " +
                        " WHERE Id_Port_Type=1 AND K.Id_Account IS NOT NULL ";

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgNRMargin.DataSource = tablep;
            

            // Close Button
            dgNRMargin.Columns.AddField("Close");
            dgNRMargin.Columns["Close"].VisibleIndex = 0;
            dgNRMargin.Columns["Close"].Width = 60;
            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Close";
            dtgNRMargin.RepositoryItems.Add(item3);
            dgNRMargin.Columns["Close"].ColumnEdit = item3;
            dgNRMargin.Columns["Close"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgNRMargin.OptionsBehavior.Editable = false;
            dgNRMargin.Columns["Close"].Visible = true;

            // Cancel Button
            dgNRMargin.Columns.AddField("Cancel");
            dgNRMargin.Columns["Cancel"].VisibleIndex = 1;
            dgNRMargin.Columns["Cancel"].Width = 60;
            RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
            item4.Buttons[0].Tag = 1;
            item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item4.Buttons[0].Caption = "Cancel";
            dtgMargin.RepositoryItems.Add(item4);
            dgNRMargin.Columns["Cancel"].ColumnEdit = item4;
            dgNRMargin.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgNRMargin.OptionsBehavior.Editable = false;
            dgNRMargin.Columns["Cancel"].Visible = true;

            dgNRMargin.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgNRMargin.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgNRMargin.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgNRMargin.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgNRMargin.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            Valida.SetColumnStyle(dgNRMargin, 1);


        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frmInsertMargin InsereMargem = new frmInsertMargin();
            Valida.Load_Properties_Form(InsereMargem);
            InsereMargem.ShowDialog();
            Load_Positions();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            tabLoans_SelectedIndexChanged(this, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void radNormal_CheckedChanged(object sender, EventArgs e)
        {
            Load_Positions();
        }

        private void radToday_CheckedChanged_1(object sender, EventArgs e)
        {
            Load_Positions();
        }

        private void rad5Days_CheckedChanged(object sender, EventArgs e)
        {
            Load_Positions();
        }

        private void tabLoans_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabLoans.SelectedIndex == 0)
            { 
                Load_Positions(); 
            }

            if (tabLoans.SelectedIndex == 1)
            {
                Load_Transactions();
            }
            if (tabLoans.SelectedIndex == 2)
            {
                Load_RecFiles();
                Load_NRLoans();
            }
        }

        private void Default_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {

            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid); 
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid);
        }

        private void dgMargin_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

            if (Column_Name == "Update")
            {
                if (dgMargin.FocusedRowHandle != null)
                {
                    double tempDB = 0;
                    double tempCBLC = 0;
                    double tempQuant = 0;

                    if (dgMargin.GetRowCellValue(dgMargin.FocusedRowHandle, "Quantity").ToString() != "") { tempDB = Convert.ToInt32(dgMargin.GetRowCellValue(dgMargin.FocusedRowHandle, "Quantity")); };
                    if (dgMargin.GetRowCellValue(dgMargin.FocusedRowHandle, "Quantity_CBLC").ToString() != "") { tempCBLC = Convert.ToInt32(dgMargin.GetRowCellValue(dgMargin.FocusedRowHandle, "Quantity_CBLC")); };

                    tempQuant = tempCBLC - tempDB;
                    if (tempQuant != 0)
                    {

                        frmInsertMargin InsereMargem = new frmInsertMargin();
                        Valida.Load_Properties_Form(InsereMargem);

                        InsereMargem.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                        InsereMargem.Left = this.Left + 10;
                        InsereMargem.Text = "Update Margin";
                        InsereMargem.Carrega_Combo();
                        InsereMargem.cmbFundo.SelectedValue = dgMargin.GetRowCellValue(dgMargin.FocusedRowHandle, "Id_Portfolio");
                        InsereMargem.Carrega_Account();
                        InsereMargem.cmbCorretora.SelectedValue = dgMargin.GetRowCellValue(dgMargin.FocusedRowHandle, "Id_Account");
                        InsereMargem.cmbTicker.SelectedValue = dgMargin.GetRowCellValue(dgMargin.FocusedRowHandle, "Id_Ticker");
                        InsereMargem.txtQtd.Text = dgMargin.GetRowCellValue(dgMargin.FocusedRowHandle, "Quantity").ToString();


                        if (tempQuant > 0)
                        {
                            InsereMargem.rdDep.Checked = true;
                            InsereMargem.txtQtd.Text = tempQuant.ToString();
                        }
                        else
                        {
                            InsereMargem.rdRet.Checked = true;
                            InsereMargem.txtQtd.Text = (-tempQuant).ToString();
                        }
                        InsereMargem.ShowDialog();
                        Load_Positions();
                    }
                }
            }
        }

        private void dgMarginTransactions_DoubleClick(object sender, EventArgs e)
        {
            int resposta;

            if (dgMarginTransactions.RowCount > 0)
            {
                int Margin_Id = (int)dgMarginTransactions.GetRowCellValue(dgMarginTransactions.FocusedRowHandle, "Id_Margin");
                string Column_Name = dgMarginTransactions.FocusedColumn.Caption.ToString();

                if (Column_Name == "Cancel")
                {
                    if (Margin_Id != 0)
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this Margin transaction?", "Cancel Stock Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            string SQLString;
                            SQLString = "UPDATE dbo.Tb712_MarginAssets SET Status=4 WHERE Id_Margin=" + Margin_Id;
                            CargaDados.curConn.ExecuteNonQuery(SQLString,1);
                            Load_Transactions();
                        }
                    }
                }
            }
        }

        private void dgStockLoan_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            //dgPositions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
        }

        private void dgStockLoan_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            GridColumn weightColumn = null;

            if (e.Item == view.GroupSummary[1])
            {
                weightColumn = dgMargin.Columns["MarginValue"];
            }
            else
            {
                weightColumn = dgMargin.Columns["MarginValue"];
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
                        double size = 0;
                        double weight = 0;
                        
                        if (e.FieldValue.ToString() != "") { size = Convert.ToDouble(e.FieldValue); };

                        if (((GridView)sender).GetRowCellValue(e.RowHandle, weightColumn).ToString() != "") { weight = Convert.ToDouble(((GridView)sender).GetRowCellValue(e.RowHandle, weightColumn)); };
                        
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

        private void dgStockLoan_EndGrouping(object sender, EventArgs e)
        {

            dgMargin.GroupSummary.Clear();

            dgMargin.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgMargin.Columns["Quantity"], "{0:#,#0}");

            dgMargin.GroupSummary.Add(SummaryItemType.Custom, "Discount", dgMargin.Columns["Discount"], "{0:0.00%}");
            dgMargin.GroupSummary.Add(SummaryItemType.Sum, "FinAmount", dgMargin.Columns["Fin_Amount_Open"], "{0:#,#0}");
            dgMargin.GroupSummary.Add(SummaryItemType.Sum, "MarginValue", dgMargin.Columns["Fin_Amount_Now"], "{0:#,#0}");
            //dgMargin.GroupSummary.Add(SummaryItemType.Custom, "Loan_Rate", dgMargin.Columns["Loan_Rate"], "{0:0.00%}");

            //dgMargin.GroupSummary.Add(SummaryItemType.Sum, "Cost_Contrib", dgMargin.Columns["Cost_Contrib"], "{0:0.00%}");

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

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            dgMargin.ExpandAllGroups();
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            dgMargin.CollapseAllGroups();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                string user = Environment.UserName.ToString();
                string Loca_Machine = Environment.MachineName.ToString();
                string fileName_Log = "T:\\Log\\Reports\\Margin_Id_LB_" + NestDLL.NUserControl.Instance.User_Id + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";

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
            BaseExportLink link = dgMargin.CreateExportLink(provider);
            if (tabLoans.SelectedIndex == 0) 
            {
                link=dgMargin.CreateExportLink(provider);
            }
            if (tabLoans.SelectedIndex == 1)
            {
                link = dgMarginTransactions.CreateExportLink(provider);
            }
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
                    Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());

                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgMargin_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (dgMargin.Columns.Count > 0 && e.RowHandle >= 0)
            {
                if (dgMargin.GetRowCellValue(e.RowHandle, "Action").ToString() == "Deposit" || dgMargin.GetRowCellValue(e.RowHandle, "Action").ToString() == "Withdraw")
                {
                    e.Appearance.BackColor = Color.FromArgb(250, 220, 216);
                }
                if (dgMargin.GetRowCellValue(e.RowHandle, "Action").ToString() == "Not Received")
                {
                    e.Appearance.BackColor = Color.LightGray;
                }
            }
        }
    }
}