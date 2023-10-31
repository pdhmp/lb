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

namespace LiveBook
{
    public partial class frmStock_Loan : LBForm
    {
        newNestConn curConn = new newNestConn();

        DateTime MaxDate;

        public frmStock_Loan()
        {
            InitializeComponent();
        }

        private void frmStock_Loan_Load(object sender, EventArgs e)
        {
            //radYear.Checked = true;
            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");

            dtg2.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg2.LookAndFeel.SetSkinStyle("Blue");

            dtgLoanTransactions2.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgLoanTransactions2.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgLoanTransactions2.LookAndFeel.SetSkinStyle("Blue");

            dtgNotDB.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgNotDB.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgNotDB.LookAndFeel.SetSkinStyle("Blue");

            dtgNotCBLC.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgNotCBLC.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgNotCBLC.LookAndFeel.SetSkinStyle("Blue");

            dtgSimilar.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgSimilar.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgSimilar.LookAndFeel.SetSkinStyle("Blue");

            dtgDifQuantity.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgDifQuantity.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgDifQuantity.LookAndFeel.SetSkinStyle("Blue");

            dtgReceivedFiles.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgReceivedFiles.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgReceivedFiles.LookAndFeel.SetSkinStyle("Blue");

            dtgNRLoans.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgNRLoans.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgNRLoans.LookAndFeel.SetSkinStyle("Blue");

            dtgCBLCData.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgCBLCData.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgCBLCData.LookAndFeel.SetSkinStyle("Blue");

            radToday.Checked = true;
        }

        void Load_Positions()
        {

            string SQLString;
            DataTable tablep = new DataTable();
            dgStockLoan.Columns.Clear();


            SQLString = "SELECT *, dbo.FCN_Is_Renewed_Loan(Loan_Id) AS IsRenewed, " + 
                        " Quantity*PriceAtOpen AS Fin_Amount_Open,  " + 
                        " Quantity*PriceNow AS Fin_Amount_Now,  " + 
                        " PriceAtOpen*Loan_Rate/PriceNow AS New_Rate,  " + 
                        " (PriceAtOpen*Loan_Rate/PriceNow)-Loan_Rate AS Rate_Diff,  " + 
                        " -(Quantity*PriceNow*Loan_Rate)/Valor_PL AS Cost_Contrib " + 
                        " FROM ( " +
                        "     SELECT A.Loan_Id, A.CBLC_Id, A.Trade_Date, A.Loan_Rate, A.Loan_Maturity, F.Type_Stock_Loan, L.Id_Portfolio, A.Id_Ticker, A.Id_Account, M.Id_Broker, D.Nome AS Broker, NestTicker, Port_Name AS Portfolio, " + 
                        "     CASE WHEN Id_Side=1 THEN -(A.Quantity-dbo.FCN_Closed_Loan_Amount(A.Loan_Id, getdate())) ELSE (A.Quantity-dbo.FCN_Closed_Loan_Amount(A.Loan_Id, getdate())) END AS Quantity ,  " +
                        "     CASE WHEN Id_Side=1 THEN 'Loan' ELSE 'Borrow' END AS Side,  " + 
                        "     dbo.FCN_GET_PRICE_Value_Only(A.Id_Ticker, DATEADD(d,-1,Trade_Date),1,0,2,0,0) AS PriceAtOpen,  " +
                        "     dbo.FCN_GETD_RT_Value_Only(A.Id_Ticker, 1, 0, 0) AS PriceNow, E.Valor_PL  " +
                        "     FROM dbo.Tb710_Stock_Loans(nolock) A  " +
                        "     LEFT JOIN dbo.Tb003_PortAccounts(nolock) L  " + 
                        "     ON A.Id_Account=L.Id_Account  " +
                        "     LEFT JOIN dbo.Tb007_Accounts(nolock) M  " + 
                        "     ON A.Id_Account=M.Id_Account  " +
                        "     LEFT JOIN dbo.Tb002_Portfolios(nolock) B  " + 
                        "     ON L.Id_Portfolio=B.Id_Portfolio  " +
                        "     INNER JOIN dbo.Tb001_Securities(nolock) C  " +
                        "     ON A.Id_Ticker = C.IdSecurity  " +
                        "     INNER JOIN Tb011_Corretoras(nolock) D  " + 
                        "     ON M.Id_Broker = D.Id_Corretora  " +
                        "     LEFT JOIN dbo.Tb127_Type_Stock_Loan(nolock) F " +  
                        "     ON A.Id_Type = F.Id_Type_Stock_Loan  " +
                        "     LEFT JOIN (SELECT K.Id_Portfolio,Valor_PL FROM dbo.Tb025_Valor_PL(nolock) K INNER JOIN (SELECT Id_Portfolio,MAX(Data_PL) AS Data_PL FROM dbo.Tb025_Valor_PL(nolock) GROUP BY Id_Portfolio) L ON K.Id_Portfolio=L.Id_Portfolio AND K.Data_PL=L.Data_PL) E  " + 
                        "     ON L.Id_Portfolio=E.Id_Portfolio  " +
                        "     WHERE Status<> 4 AND Loan_Maturity>=CONVERT(varchar,GetDate(),112) AND Id_Port_Type=1" + 
                        " ) AS X WHERE Quantity<>0 ORDER BY Trade_Date, Id_Broker DESC, Portfolio";

            if (radToday.Checked == true) 
            {
                SQLString = SQLString.Replace("WHERE", "WHERE dbo.FCN_Is_Renewed_Loan(Loan_Id)=0 AND Loan_Maturity='" + DateTime.Now.ToString("yyyyMMdd") + "' AND ");
            };
            if (rad5Days.Checked == true) 
            { 
                SQLString = SQLString.Replace("WHERE", "WHERE Loan_Maturity<='" + DateTime.Now.AddDays(5).ToString("yyyyMMdd") + "' AND ");
            };
            using (newNestConn curConn = new newNestConn())
            {
                tablep = curConn.Return_DataTable(SQLString);

                dtg.DataSource = tablep;


                // Cancel Button
                dgStockLoan.Columns.AddField("Cancel");
                dgStockLoan.Columns["Cancel"].VisibleIndex = 0;
                dgStockLoan.Columns["Cancel"].Width = 60;
                RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
                item3.Buttons[0].Tag = 1;
                item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item3.Buttons[0].Caption = "Cancel";
                dtg.RepositoryItems.Add(item3);
                dgStockLoan.Columns["Cancel"].ColumnEdit = item3;
                dgStockLoan.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dgStockLoan.OptionsBehavior.Editable = false;
                dgStockLoan.Columns["Cancel"].Visible = true;

                // Close Button
                dgStockLoan.Columns.AddField("Close");
                dgStockLoan.Columns["Close"].VisibleIndex = 1;
                dgStockLoan.Columns["Close"].Width = 60;
                RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
                item4.Buttons[0].Tag = 1;
                item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item4.Buttons[0].Caption = "Close";
                dtg.RepositoryItems.Add(item4);
                dgStockLoan.Columns["Close"].ColumnEdit = item4;
                dgStockLoan.Columns["Close"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dgStockLoan.OptionsBehavior.Editable = false;
                dgStockLoan.Columns["Close"].Visible = true;

                // Renew Button
                dgStockLoan.Columns.AddField("Renew");
                dgStockLoan.Columns["Renew"].VisibleIndex = 2;
                dgStockLoan.Columns["Renew"].Width = 60;
                RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
                item5.Buttons[0].Tag = 1;
                item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item5.Buttons[0].Caption = "Renew";
                dtg.RepositoryItems.Add(item5);
                dgStockLoan.Columns["Renew"].ColumnEdit = item5;
                dgStockLoan.Columns["Renew"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dgStockLoan.OptionsBehavior.Editable = false;
                dgStockLoan.Columns["Renew"].Visible = true;

                dgStockLoan.Columns["Loan_Rate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgStockLoan.Columns["Loan_Rate"].DisplayFormat.FormatString = "0.00%;(0.00%)";
                dgStockLoan.Columns["New_Rate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgStockLoan.Columns["New_Rate"].DisplayFormat.FormatString = "0.00%;(0.00%)";
                dgStockLoan.Columns["Rate_Diff"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgStockLoan.Columns["Rate_Diff"].DisplayFormat.FormatString = "0.00%;(0.00%)";
                dgStockLoan.Columns["Cost_Contrib"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgStockLoan.Columns["Cost_Contrib"].DisplayFormat.FormatString = "0.00%;(0.00%)";
                dgStockLoan.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgStockLoan.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";
                dgStockLoan.Columns["Fin_Amount_Open"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgStockLoan.Columns["Fin_Amount_Open"].DisplayFormat.FormatString = "#,##0;(#,##0)";
                dgStockLoan.Columns["Fin_Amount_Now"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgStockLoan.Columns["Fin_Amount_Now"].DisplayFormat.FormatString = "#,##0;(#,##0)";

                dgStockLoan.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgStockLoan.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
                dgStockLoan.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                //Cost_Contrib
                dgStockLoan.Columns["Loan_Maturity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgStockLoan.Columns["Loan_Maturity"].DisplayFormat.FormatString = "dd-MMM-yy";
                dgStockLoan.Columns["Loan_Maturity"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                curUtils.SetColumnStyle(dgStockLoan, 1);

                if (radToday.Checked == true || rad5Days.Checked == true) { dgStockLoan.ExpandAllGroups(); }
            }
        }

        void Load_CBLCData()
        {

            string SQLString;
            DataTable tablep = new DataTable();
            dgCBLCData.Columns.Clear();


            SQLString = "SELECT A.*, B.Port_Name AS Portfolio, B.Id_Portfolio AS Id_Portfolio, D.Nome AS Broker, E.Loan_Id " +
                        " FROM dbo.Tb710_Stock_Loans_Import A (nolock) " +
                        " LEFT JOIN dbo.Tb003_PortAccounts L (nolock) " +
                        " ON A.Id_Account=L.Id_Account " +
                        " LEFT JOIN dbo.Tb007_Accounts M (nolock) " +
                        " ON A.Id_Account=M.Id_Account " +
                        " LEFT JOIN dbo.Tb002_Portfolios B (nolock) " +
                        " ON L.Id_Portfolio=B.Id_Portfolio " +
                        " LEFT JOIN Tb011_Corretoras D (nolock) " +
                        " ON M.Id_Broker = D.Id_Corretora " +
                        " LEFT JOIN (SELECT * FROM Tb710_Stock_Loans (nolock)  WHERE Status<>4) E " +
                        " ON A.Contrato = E.CBLC_Id " +
                        " WHERE Id_Port_Type=2 ";

            using (newNestConn curConn = new newNestConn())
            {
                tablep = curConn.Return_DataTable(SQLString);

                dtgCBLCData.DataSource = tablep;


                // Insert Button
                dgCBLCData.Columns.AddField("Insert");
                dgCBLCData.Columns["Insert"].VisibleIndex = 0;
                dgCBLCData.Columns["Insert"].Width = 60;
                RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
                item3.Buttons[0].Tag = 1;
                item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item3.Buttons[0].Caption = "Insert";
                dtgCBLCData.RepositoryItems.Add(item3);
                dgCBLCData.Columns["Insert"].ColumnEdit = item3;
                dgCBLCData.Columns["Insert"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dgCBLCData.OptionsBehavior.Editable = false;
                dgCBLCData.Columns["Insert"].Visible = true;


                dgCBLCData.Columns["Taxa"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCBLCData.Columns["Taxa"].DisplayFormat.FormatString = "0.00%;(0.00%)";

                dgCBLCData.Columns["File_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgCBLCData.Columns["File_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
                dgCBLCData.Columns["File_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                dgCBLCData.Columns["Data_Registro"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgCBLCData.Columns["Data_Registro"].DisplayFormat.FormatString = "dd-MMM-yy";
                dgCBLCData.Columns["Data_Registro"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                dgCBLCData.Columns["Data_Vecto"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgCBLCData.Columns["Data_Vecto"].DisplayFormat.FormatString = "dd-MMM-yy";
                dgCBLCData.Columns["Data_Vecto"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                dgCBLCData.Columns["Quantidade"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgCBLCData.Columns["Quantidade"].DisplayFormat.FormatString = "#,##0;(#,##0)";

                dgCBLCData.Columns["Data_Carencia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgCBLCData.Columns["Data_Carencia"].DisplayFormat.FormatString = "dd-MMM-yy";
                dgCBLCData.Columns["Data_Carencia"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                curUtils.SetColumnStyle(dgCBLCData, 1);

                if (radToday.Checked == true || rad5Days.Checked == true) { dgStockLoan.ExpandAllGroups(); }
            }
        }

        void Load_Transactions()
        {

            string SQLString;
            DataTable tablep = new DataTable();

            using (newNestConn curConn = new newNestConn())
            {
                dgLoanTransactions.Columns.Clear();

                SQLString = "SELECT A.Loan_Id, E.Code AS Trans_Type, A.CBLC_Id, A.Trade_Date, A.Quantity, A.Loan_Rate, A.Loan_Maturity, D.Nome AS Broker, NestTicker, Port_Name AS Portfolio " +
                     " FROM  " +
                     " ( " +
                     " 	SELECT Loan_Id, Transaction_Type, CBLC_Id, Trade_Date, CASE WHEN Id_Side=1 THEN -Quantity ELSE Quantity END AS Quantity, Loan_Rate, Loan_Maturity, Id_Ticker,Id_Account, Status, Id_Side " +
                     " 	FROM dbo.Tb710_Stock_Loans J(NOLOCK) WHERE Status <> 4 " +
                     " 	UNION ALL " +
                     " 	SELECT K.Loan_Id, K.Transaction_Type, L.CBLC_Id, Close_Date AS Trade_Date, CASE WHEN Id_Side=1 THEN Close_Quantity ELSE -Close_Quantity END AS Quantity, L.Loan_Rate, L.Loan_Maturity, L.Id_Ticker,L.Id_Account, L.Status, L.Id_Side " +
                     " 	FROM dbo.Tb710_Stock_Loans_Early_Close K (NOLOCK)" +
                     " 	INNER JOIN  " +
                     " 	dbo.Tb710_Stock_Loans L (nolock) " +
                     " 	ON K.Loan_Id=L.Loan_Id " +
                     "  WHERE K.Status <> 4 " +
                     " ) A " +
                     " LEFT JOIN dbo.Tb003_PortAccounts L (NOLOCK) " +
                     " ON A.Id_Account=L.Id_Account  " +
                     " LEFT JOIN dbo.Tb007_Accounts M (NOLOCK) " +
                     " ON A.Id_Account=M.Id_Account  " +
                     " LEFT JOIN dbo.Tb002_Portfolios B (NOLOCK) " +
                     " ON L.Id_Portfolio=B.Id_Portfolio  " +
                     " inner join dbo.Tb001_Securities C (NOLOCK) " +
                     " ON A.Id_Ticker = C.IdSecurity  " +
                     " inner join Tb011_Corretoras D (NOLOCK) " +
                     " ON M.Id_Broker = D.Id_Corretora  " +
                     " LEFT JOIN dbo.Tb701_Transaction_Types E (NOLOCK)  " +
                     " ON A.Transaction_Type=E.Id_Trans_Type " +
                     " WHERE Id_Port_Type=2 " +
                    // " AND A.Trade_Date = convert(varchar,getdate(),112)" +  
                     " ORDER BY Trade_Date";

                tablep = curConn.Return_DataTable(SQLString);

                dtg2.DataSource = tablep;


                // Cancel Button
                dgLoanTransactions.Columns.AddField("Cancel");
                dgLoanTransactions.Columns["Cancel"].VisibleIndex = 0;
                dgLoanTransactions.Columns["Cancel"].Width = 60;
                RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
                item3.Buttons[0].Tag = 1;
                item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item3.Buttons[0].Caption = "Cancel";
                dtg2.RepositoryItems.Add(item3);
                dgLoanTransactions.Columns["Cancel"].ColumnEdit = item3;
                dgLoanTransactions.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dgLoanTransactions.OptionsBehavior.Editable = false;
                dgLoanTransactions.Columns["Cancel"].Visible = true;

                dgLoanTransactions.Columns["Loan_Rate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgLoanTransactions.Columns["Loan_Rate"].DisplayFormat.FormatString = "0.00%;(0.00%)";
                dgLoanTransactions.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgLoanTransactions.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";

                dgLoanTransactions.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgLoanTransactions.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
                dgLoanTransactions.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                dgLoanTransactions.Columns["Trans_Type"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                dgLoanTransactions.Columns["Loan_Maturity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgLoanTransactions.Columns["Loan_Maturity"].DisplayFormat.FormatString = "dd-MMM-yy";
                dgLoanTransactions.Columns["Loan_Maturity"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                curUtils.SetColumnStyle(dgLoanTransactions, 1);
            }
        }

        void Load_Transactions2(bool All_Transactions)
        {

            string SQLString;
            DataTable tablep = new DataTable();

            dgLoanTransactions2.Columns.Clear();

            if (All_Transactions)
            {
                SQLString = "SELECT A.Loan_Id, E.Code AS Trans_Type, A.CBLC_Id, A.Trade_Date, A.Quantity, A.Loan_Rate, A.Loan_Maturity, D.Nome AS Broker, NestTicker, Port_Name AS Portfolio " +
                     " FROM  " +
                     " ( " +
                     " 	SELECT Loan_Id, Transaction_Type, CBLC_Id, Trade_Date, CASE WHEN Id_Side=1 THEN -Quantity ELSE Quantity END AS Quantity, Loan_Rate, Loan_Maturity, Id_Ticker,Id_Account, Status, Id_Side " +
                     " 	FROM dbo.Tb710_Stock_Loans J (nolock) WHERE Status <> 4 " +
                     " 	UNION ALL " +
                     " 	SELECT K.Loan_Id, K.Transaction_Type, L.CBLC_Id, Close_Date AS Trade_Date, CASE WHEN Id_Side=1 THEN Close_Quantity ELSE -Close_Quantity END AS Quantity, L.Loan_Rate, L.Loan_Maturity, L.Id_Ticker,L.Id_Account, L.Status, L.Id_Side " +
                     " 	FROM dbo.Tb710_Stock_Loans_Early_Close K (nolock) " +
                     " 	INNER JOIN  " +
                     " 	dbo.Tb710_Stock_Loans L (nolock) " +
                     " 	ON K.Loan_Id=L.Loan_Id " +
                     "  WHERE K.Status <> 4 " +
                     " ) A " +
                     " LEFT JOIN dbo.Tb003_PortAccounts L (nolock)  " +
                     " ON A.Id_Account=L.Id_Account  " +
                     " LEFT JOIN dbo.Tb007_Accounts M  (nolock) " +
                     " ON A.Id_Account=M.Id_Account  " +
                     " LEFT JOIN dbo.Tb002_Portfolios B  (nolock) " +
                     " ON L.Id_Portfolio=B.Id_Portfolio  " +
                     " inner join dbo.Tb001_Securities C  (nolock) " +
                     " ON A.Id_Ticker = C.IdSecurity  " +
                     " inner join Tb011_Corretoras D  (nolock) " +
                     " ON M.Id_Broker = D.Id_Corretora  " +
                     " LEFT JOIN dbo.Tb701_Transaction_Types E  (nolock) " +
                     " ON A.Transaction_Type=E.Id_Trans_Type " +
                     " WHERE Id_Port_Type=2 " +
                     " ORDER BY Trade_Date";
            }
            else
            {
                SQLString = " SELECT Trade_Date, Id_Portfolio, Port_Name, Broker, Ticker, Loan_Id, Trade_Date, Loan_Maturity, Loan_Rate, Id_Type, Quantity, Type " +
                            " FROM  VW_LB_Stock_Loans  WHERE  Id_Port_Type=2 " +
                            " GROUP BY Trade_Date, Id_Portfolio, Port_Name, Broker, Ticker, Loan_Id, Trade_Date, Loan_Maturity, Loan_Rate, Id_Type, Quantity, Type ";
            }

            tablep = curConn.Return_DataTable(SQLString);

            dtgLoanTransactions2.DataSource = tablep;

            // Cancel Button
            dgLoanTransactions2.Columns.AddField("Cancel");
            dgLoanTransactions2.Columns["Cancel"].VisibleIndex = 0;
            dgLoanTransactions2.Columns["Cancel"].Width = 60;
            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Cancel";
            dtgLoanTransactions2.RepositoryItems.Add(item3);
            dgLoanTransactions2.Columns["Cancel"].ColumnEdit = item3;
            dgLoanTransactions2.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgLoanTransactions2.OptionsBehavior.Editable = false;
            dgLoanTransactions2.Columns["Cancel"].Visible = true;

            dgLoanTransactions2.Columns["Loan_Rate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgLoanTransactions2.Columns["Loan_Rate"].DisplayFormat.FormatString = "0.00%;(0.00%)";
            dgLoanTransactions2.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgLoanTransactions2.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgLoanTransactions2.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgLoanTransactions2.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgLoanTransactions2.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            //dgLoanTransactions2.Columns["Trans_Type"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            dgLoanTransactions2.Columns["Loan_Maturity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgLoanTransactions2.Columns["Loan_Maturity"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgLoanTransactions2.Columns["Loan_Maturity"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            curUtils.SetColumnStyle(dgLoanTransactions2, 1);


        }

        void Load_NotDB()
        {

            string SQLString;
            DataTable tablep = new DataTable();

            dgNotDB.Columns.Clear();

            SQLString = " SELECT A.*, B.Id_Portfolio, B.Port_Name AS Portfolio, D.Id_Corretora AS Id_Broker, D.Nome AS Broker " +
                        " FROM dbo.Tb710_Stock_Loans_Import A (nolock)  " +
                        " LEFT JOIN dbo.Tb003_PortAccounts L (nolock) " +
                        " ON A.Id_Account=L.Id_Account " +
                        " LEFT JOIN dbo.Tb007_Accounts M (nolock) " +
                        " ON A.Id_Account=M.Id_Account " +
                        " LEFT JOIN dbo.Tb002_Portfolios B (nolock) " +
                        " ON L.Id_Portfolio=B.Id_Portfolio " +
                        " LEFT JOIN Tb011_Corretoras D(nolock) " +
                        " ON M.Id_Broker = D.Id_Corretora " +
                        " WHERE Id_Port_Type=1 AND Contrato not IN ( " +
                        " SELECT CBLC_Id FROM dbo.Tb710_Stock_Loans (nolock) WHERE CBLC_Id IS NOT NULL AND Status<> 4 AND Loan_Maturity>=CONVERT(varchar,GetDate(),112)) " +
                        " AND File_Date=(SELECT MAX(File_Date) FROM Tb710_Stock_Loans_Import (nolock) ) " +
                        " AND Data_Vecto>=CONVERT(varchar,GetDate(),112) " +
                        " AND Contrato NOT IN " +
                        " ( " +
                        " 	SELECT A.Contrato FROM " +
                        " 	( " +
                        " 		SELECT *, " +
                        " 		CASE " +
                        "      		WHEN Liq_Ant='SIM' AND Liq_Ant_Doador='SIM' THEN 1 " +
                        "      		WHEN Liq_Ant='SIM' AND Liq_Ant_Doador='NAO' THEN 2 " +
                        "      		WHEN Liq_Ant='NAO' AND Liq_Ant_Doador='NAO' THEN 3 " +
                        "   	    END AS Tipo_Aluguel, " +
                        " 		CASE WHEN DT='D' THEN 1 ELSE 2 END AS Id_Side " +
                        " 		FROM dbo.Tb710_Stock_Loans_Import (nolock) " +
                        " 	) A " +
                        " 	INNER JOIN (SELECT X.*, Y.NestTicker as Simbolo, CASE WHEN Id_Side=1 THEN -(X.Quantity-dbo.FCN_Closed_Loan_Amount(X.Loan_Id, getdate())) ELSE (X.Quantity-dbo.FCN_Closed_Loan_Amount(X.Loan_Id, getdate())) END AS Final_Quantity FROM dbo.Tb710_Stock_Loans X (nolock) INNER JOIN dbo.Tb001_Securities Y (nolock) ON X.Id_Ticker=Y.IdSecurity) B " +
                        " 	ON A.Id_Side=B.Id_Side " +
                        " 	AND A.Data_Registro=B.Trade_Date " +
                        " 	AND A.Data_Vecto=B.Loan_Maturity " +
                        " 	AND A.Taxa=B.Loan_Rate " +
                        " 	AND A.Id_Account=B.Id_Account " +
                        " 	AND A.Tipo_Aluguel=B.Id_Type " +
                        " 	AND A.Cod_Neg=B.Simbolo " +
                        " 	WHERE CBLC_Id IS NULL AND B.Status <> 4 " +
                        " ) " +
                        " ORDER BY File_Date";

            tablep = curConn.Return_DataTable(SQLString);

            dtgNotDB.DataSource = tablep;
            

            // Insert Button
            dgNotDB.Columns.AddField("Insert");
            dgNotDB.Columns["Insert"].VisibleIndex = 0;
            dgNotDB.Columns["Insert"].Width = 60;
            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Insert";
            dtgNotDB.RepositoryItems.Add(item3);
            dgNotDB.Columns["Insert"].ColumnEdit = item3;
            dgNotDB.Columns["Insert"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgNotDB.OptionsBehavior.Editable = false;
            dgNotDB.Columns["Insert"].Visible = true;


            dgNotDB.Columns["Taxa"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgNotDB.Columns["Taxa"].DisplayFormat.FormatString = "0.00%;(0.00%)";

            dgNotDB.Columns["File_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgNotDB.Columns["File_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgNotDB.Columns["File_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            dgNotDB.Columns["Data_Registro"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgNotDB.Columns["Data_Registro"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgNotDB.Columns["Data_Registro"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            
            dgNotDB.Columns["Data_Vecto"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgNotDB.Columns["Data_Vecto"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgNotDB.Columns["Data_Vecto"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            dgNotDB.Columns["Quantidade"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgNotDB.Columns["Quantidade"].DisplayFormat.FormatString = "#,##0;(#,##0)";
            
            dgNotDB.Columns["Data_Carencia"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgNotDB.Columns["Data_Carencia"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgNotDB.Columns["Data_Carencia"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            curUtils.SetColumnStyle(dgNotDB, 1);

            dgNotDB_DoubleClick(dgNotDB, new EventArgs());

        }

        void Load_NotCBLC()
        {

            string SQLString;
            DataTable tablep = new DataTable();

            dgNotCBLC.Columns.Clear();

            SQLString = " SELECT L.* FROM " +
                        " ( " +
                        " 	SELECT * FROM " +
                        " 	( " +
                        " 		SELECT Id_Account, MAX(File_Date) AS LastReceived, (SELECT MAX(File_Date) FROM dbo.Tb710_Stock_Loans_Import (nolock) ) AS MAXDATE " +
                        " 		FROM dbo.Tb710_Stock_Loans_Import (nolock) " +
                        " 		GROUP BY Id_Account " +
                        " 	) AS A " +
                        " 	WHERE LastReceived = MAXDATE " +
                        " ) AS K " +
                        " RIGHT JOIN " +
                        " ( " +
                        " 	SELECT * FROM " +
                        " 	( " +
                        " 		SELECT A.Status, A.Id_Account, A.Loan_Id, A.Transaction_Type, A.CBLC_Id, A.Trade_Date, CASE WHEN Id_Side=1 THEN -(A.Quantity-dbo.FCN_Closed_Loan_Amount(A.Loan_Id, getdate())) ELSE (A.Quantity-dbo.FCN_Closed_Loan_Amount(A.Loan_Id, getdate())) END AS Quantity, A.Loan_Rate, A.Loan_Maturity, D.Nome AS Broker,NestTicker, B.Port_Name AS Portfolio, E.Type_Stock_Loan, " +
                        " 		CASE WHEN Id_Side=1 THEN 'Loan' ELSE 'Borrow' END AS Side " +
                        " 		FROM dbo.Tb710_Stock_Loans A  (nolock) " +
                        " 		LEFT JOIN dbo.Tb003_PortAccounts L (nolock) " +
                        " 		ON A.Id_Account=L.Id_Account " +
                        " 		LEFT JOIN dbo.Tb007_Accounts M (nolock) " +
                        " 		ON A.Id_Account=M.Id_Account " +
                        " 		LEFT JOIN dbo.Tb002_Portfolios B (nolock) " +
                        " 		ON L.Id_Portfolio=B.Id_Portfolio " +
                        " 		LEFT JOIN Tb011_Corretoras D (nolock) " +
                        " 		ON M.Id_Broker = D.Id_Corretora " +
                        " 		LEFT JOIN dbo.Tb001_Securities C (nolock) " +
                        " 		ON A.Id_Ticker = C.IdSecurity " +
                        " 		LEFT JOIN dbo.Tb127_Type_Stock_Loan E (nolock) " +
                        " 		ON A.Id_Type = E.Id_Type_Stock_Loan " +
                        " 		WHERE Id_Port_Type=1 AND CBLC_Id not IN ( " +
                        " 		SELECT Contrato FROM dbo.Tb710_Stock_Loans_Import (nolock) WHERE File_Date=(SELECT MAX(File_Date) FROM Tb710_Stock_Loans_Import (nolock) )) " +
                        " 		AND Status<> 4 AND Loan_Maturity>=CONVERT(varchar,GetDate(),112) OR CBLC_Id IS NULL " +
                        " 	) AS Q WHERE Quantity<>0 AND Status<> 4 " +
                        " ) AS L " +
                        " ON K.Id_Account=L.Id_Account " +
                        " WHERE K.Id_Account IS NOT NULL AND Loan_Id NOT IN " +
                        " 	( " +
                        " 	SELECT B.Loan_Id FROM " +
                        " 	( " +
                        " 		SELECT *, " +
                        " 		CASE " +
                        " 			WHEN Liq_Ant='SIM' AND Liq_Ant_Doador='SIM' THEN 1 " +
                        " 			WHEN Liq_Ant='SIM' AND Liq_Ant_Doador='NAO' THEN 2 " +
                        " 			WHEN Liq_Ant='NAO' AND Liq_Ant_Doador='NAO' THEN 3 " +
                        "  		END AS Tipo_Aluguel, " +
                        " 		CASE WHEN DT='D' THEN 1 ELSE 2 END AS Id_Side  " +
                        " 		FROM dbo.Tb710_Stock_Loans_Import  (nolock) " +
                        " 	) A  " +
                        " 	INNER JOIN (SELECT X.*, Y.NestTicker, CASE WHEN Id_Side=1 THEN -(X.Quantity-dbo.FCN_Closed_Loan_Amount(X.Loan_Id, getdate())) ELSE (X.Quantity-dbo.FCN_Closed_Loan_Amount(X.Loan_Id, getdate())) END AS Final_Quantity FROM dbo.Tb710_Stock_Loans X (nolock) INNER JOIN dbo.Tb001_Securities Y (nolock)  ON X.Id_Ticker=Y.IdSecurity) B  " +
                        " 	ON A.Id_Side=B.Id_Side " +
                        " 	AND A.Data_Registro=B.Trade_Date " +
                        " 	AND A.Data_Vecto=B.Loan_Maturity " +
                        " 	AND A.Taxa=B.Loan_Rate " +
                        " 	AND A.Tipo_Aluguel=B.Id_Type " +
                        " 	AND A.Id_Account=B.Id_Account " +
                        " 	AND A.Cod_Neg=B.NestTicker " +
                        " 	WHERE CBLC_Id IS NULL AND B.Status <> 4" +
                        " 	) " +
                        " ORDER BY Id_Account, NestTicker";

            tablep = curConn.Return_DataTable(SQLString);

            dtgNotCBLC.DataSource = tablep;
            

            // Close/Cancel Button
            dgNotCBLC.Columns.AddField("Close/Cancel");
            dgNotCBLC.Columns["Close/Cancel"].VisibleIndex = 0;
            dgNotCBLC.Columns["Close/Cancel"].Width = 90;

            dgNotCBLC.Columns.AddField("UpdateMaturity");
            dgNotCBLC.Columns["UpdateMaturity"].VisibleIndex = 0;
            dgNotCBLC.Columns["UpdateMaturity"].Width = 90;


            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Close/Cancel";
            dtgNotCBLC.RepositoryItems.Add(item3);
            dgNotCBLC.Columns["Close/Cancel"].ColumnEdit = item3;
            dgNotCBLC.Columns["Close/Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgNotCBLC.OptionsBehavior.Editable = false;
            dgNotCBLC.Columns["Close/Cancel"].Visible = true;


            RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
            item4.Buttons[0].Tag = 1;
            item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item4.Buttons[0].Caption = "UpdateMaturity";
            dtgNotCBLC.RepositoryItems.Add(item4);
            dgNotCBLC.Columns["UpdateMaturity"].ColumnEdit = item4;
            dgNotCBLC.Columns["UpdateMaturity"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgNotCBLC.OptionsBehavior.Editable = false;
            dgNotCBLC.Columns["UpdateMaturity"].Visible = true;


            dgNotCBLC.Columns["Loan_Rate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgNotCBLC.Columns["Loan_Rate"].DisplayFormat.FormatString = "0.00%;(0.00%)";
            dgNotCBLC.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgNotCBLC.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgNotCBLC.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgNotCBLC.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgNotCBLC.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            dgNotCBLC.Columns["Loan_Maturity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgNotCBLC.Columns["Loan_Maturity"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgNotCBLC.Columns["Loan_Maturity"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            curUtils.SetColumnStyle(dgNotCBLC, 1);

            dgNotCBLC_DoubleClick(dgNotCBLC, new EventArgs());

        }

        void Load_DifQuantity()
        {

            string SQLString;
            DataTable tablep = new DataTable();

            dgDifQuantity.Columns.Clear();

            SQLString = " SELECT * " +
                        " FROM VW_LB_Stock_Loans " +
                        " WHERE Id_Port_Type=1 AND Quantity<>Quant_CBLC AND CBLC_Id=Imported_CBLC_Id AND Loan_Rate=Taxa AND Id_Type=Tipo_Aluguel AND CBLC_Id IS NOT NULL " +
                        " ORDER BY Id_Portfolio, Broker, Ticker";

            tablep = curConn.Return_DataTable(SQLString);

            dtgDifQuantity.DataSource = tablep;

            // Cancel Button
            dgDifQuantity.Columns.AddField("Cancel");
            dgDifQuantity.Columns["Cancel"].VisibleIndex = 0;
            dgDifQuantity.Columns["Cancel"].Width = 60;
            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Cancel";
            dtgDifQuantity.RepositoryItems.Add(item3);
            dgDifQuantity.Columns["Cancel"].ColumnEdit = item3;
            dgDifQuantity.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgDifQuantity.OptionsBehavior.Editable = false;
            dgDifQuantity.Columns["Cancel"].Visible = true;

            // Close Button
            dgDifQuantity.Columns.AddField("Close");
            dgDifQuantity.Columns["Close"].VisibleIndex = 0;
            dgDifQuantity.Columns["Close"].Width = 60;
            RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
            item4.Buttons[0].Tag = 1;
            item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item4.Buttons[0].Caption = "Close";
            dtgNRLoans.RepositoryItems.Add(item4);
            dgDifQuantity.Columns["Close"].ColumnEdit = item4;
            dgDifQuantity.Columns["Close"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgDifQuantity.OptionsBehavior.Editable = false;
            dgDifQuantity.Columns["Close"].Visible = true;

            dgDifQuantity.Columns["Loan_Rate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDifQuantity.Columns["Loan_Rate"].DisplayFormat.FormatString = "0.00%;(0.00%)";
            dgDifQuantity.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDifQuantity.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";
            dgDifQuantity.Columns["Quant_CBLC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDifQuantity.Columns["Quant_CBLC"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgDifQuantity.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgDifQuantity.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgDifQuantity.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            dgDifQuantity.Columns["Loan_Maturity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgDifQuantity.Columns["Loan_Maturity"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgDifQuantity.Columns["Loan_Maturity"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            curUtils.SetColumnStyle(dgDifQuantity, 1);

            //dgDifQuantity.FocusedColumn = dgDifQuantity.Columns["Quantity"];

            dgDifQuantity_DoubleClick(dgDifQuantity, new EventArgs());
            
        }

        void Load_Similar()
        {

            string SQLString;
            DataTable tablep = new DataTable();

            dgSimilar.Columns.Clear();

            string strFilter="";

            if (rdDifferent.Checked == true)
            {
                if (chkQuantity.Checked == true)
                {
                    strFilter += " AND Quantity<>Quant_CBLC";
                }
                else
                {
                    //strFilter += " OR Quantity=Quant_CBLC";
                }
                if (chkType.Checked == true)
                {
                    strFilter += " AND Id_Type<>Tipo_Aluguel";
                }
                else
                {
                    //strFilter += " OR Id_Type=Tipo_Aluguel";
                }
                if (chkRate.Checked == true)
                {
                    strFilter += " AND Loan_Rate<>Taxa";
                }
                else
                {
                    //strFilter += " OR Loan_Rate=Taxa";
                }
                if (chkMaturity.Checked == true)
                {
                    strFilter += " AND Data_Vecto<>Loan_Maturity";
                }
                else
                {
                    //strFilter += " OR Data_Vecto=Loan_Maturity";
                }
            }
            if (rdMatch.Checked == true)
            {
                strFilter = " AND Quantity=Quant_CBLC AND Id_Type=Tipo_Aluguel AND Data_Vecto=Loan_Maturity AND Loan_Rate=Taxa";
            }

            SQLString = " SELECT Trade_Date, Id_Portfolio, Port_Name, Broker, Ticker, Id_Side  " +
                        " FROM VW_LB_Stock_Loans " +
                        " WHERE Id_Port_Type=2 AND CBLC_Id IS NULL" + strFilter + " " +
                        " GROUP BY Trade_Date, Id_Portfolio, Port_Name, Broker, Ticker, Id_Side " +
                        " ORDER BY Trade_Date, Id_Portfolio, Port_Name, Broker, Ticker, Id_Side ";

            tablep = curConn.Return_DataTable(SQLString);

            dtgSimilar.DataSource = tablep;
            


            dgSimilar.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgSimilar.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgSimilar.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            curUtils.SetColumnStyle(dgSimilar, 1);

            if (dgSimilar.RowCount == 0)
            {
                dgLoanTransactions2.Columns.Clear();
                dgCBLCData.Columns.Clear();
            }
            else
            {
                dgSimilar_DoubleClick(dgSimilar, new EventArgs());
            }
        }

        void Load_RecFiles()
        {

            string SQLString;
            DataTable tablep = new DataTable();

            dgReceivedFiles.Columns.Clear();

            SQLString = "SELECT Nome, Port_Name, MAX(File_Date) AS LastReceived, COALESCE(NoLoans, 0) AS NoLoans  " +
                        " FROM dbo.Tb710_Stock_Loans_Import A  (nolock) " +
                        " LEFT JOIN dbo.Tb003_PortAccounts L (nolock)  " +
                        " ON A.Id_Account=L.Id_Account  " +
                        " LEFT JOIN dbo.Tb007_Accounts M  (nolock) " +
                        " ON A.Id_Account=M.Id_Account  " +
                        " LEFT JOIN dbo.Tb011_Corretoras B (nolock)  " +
                        " ON M.Id_Broker=B.Id_Corretora  " +
                        " LEFT JOIN dbo.Tb002_Portfolios C  (nolock) " +
                        " ON L.Id_Portfolio=C.Id_Portfolio  " +
                        " LEFT JOIN   " +
                        " (  " +
                        " 	SELECT Id_Account,COUNT(Quantity) AS NoLoans  " +
                        " 	FROM (  " +
                        " 		SELECT A.Loan_Id, A.Id_Account,  " +
                        " 		CASE WHEN Id_Side=1 THEN -(A.Quantity-dbo.FCN_Closed_Loan_Amount(A.Loan_Id, getdate())) ELSE (A.Quantity-dbo.FCN_Closed_Loan_Amount(A.Loan_Id, getdate())) END AS Quantity  " +
                        " 		FROM dbo.Tb710_Stock_Loans A  (nolock) " +
                        " 		WHERE Status<> 4 AND Loan_Maturity>=CONVERT(varchar,GetDate(),112)  " +
                        " 	) AS X   " +
                        " 	WHERE Quantity<>0  " +
                        " 	GROUP BY Id_Account  " +
                        " ) AS Y  " +
                        " ON A.Id_Account=Y.Id_Account  " +
                        " WHERE Id_Port_Type=1 " +
                        " GROUP BY Nome, Port_Name, NoLoans " +
                        " ORDER BY MAX(File_Date),Nome, Port_Name, NoLoans";

            tablep = curConn.Return_DataTable(SQLString);

            dtgReceivedFiles.DataSource = tablep;

            MaxDate = Convert.ToDateTime(curConn.Execute_Query_String("SELECT MAX(File_Date) FROM Tb710_Stock_Loans_Import (nolock) "));

            dgReceivedFiles.Columns["LastReceived"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgReceivedFiles.Columns["LastReceived"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgReceivedFiles.Columns["LastReceived"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            dgReceivedFiles.Columns["Nome"].BestFit();
            dgReceivedFiles.Columns["Port_Name"].BestFit();
            dgReceivedFiles.Columns["LastReceived"].BestFit();
            dgReceivedFiles.Columns["NoLoans"].BestFit();

        }

        void Load_NRLoans()
        {

            string SQLString;
            DataTable tablep = new DataTable();

            dgNRLoans.Columns.Clear();

            SQLString = "SELECT L.* FROM " +
                        "( " +
                        "    SELECT * FROM ( " +
                        "    SELECT Id_Account, MAX(File_Date) AS LastReceived, (SELECT MAX(File_Date) FROM dbo.Tb710_Stock_Loans_Import (nolock) ) AS MAXDATE " +
                        "    FROM dbo.Tb710_Stock_Loans_Import (nolock) " +
                        "    GROUP BY Id_Account " +
                        ") AS A " +
                        "WHERE LastReceived = MAXDATE " +
                        ") AS K " +
                        "RIGHT JOIN  " +
                        "( " +
                        "    SELECT * FROM  " +
                        "    ( " +
                        "        SELECT A.Status, A.Id_Account AS Id_Account, A.Loan_Id, A.Transaction_Type, A.CBLC_Id, A.Trade_Date, CASE WHEN Id_Side=1 THEN -(A.Quantity-dbo.FCN_Closed_Loan_Amount(A.Loan_Id, getdate())) ELSE (A.Quantity-dbo.FCN_Closed_Loan_Amount(A.Loan_Id, getdate())) END AS Quantity, A.Loan_Rate, A.Loan_Maturity, D.Nome AS Broker, NestTicker, B.Port_Name AS Portfolio, E.Type_Stock_Loan,  " +
                        "        CASE WHEN Id_Side=1 THEN 'Loan' ELSE 'Borrow' END AS Side  " +
                        "        FROM dbo.Tb710_Stock_Loans A  (nolock) " +
                        "        LEFT JOIN dbo.Tb003_PortAccounts X (nolock)  " +
                        "        ON A.Id_Account=X.Id_Account  " +
                        "        LEFT JOIN dbo.Tb007_Accounts M  (nolock) " +
                        "        ON A.Id_Account=M.Id_Account  " +
                        "        LEFT JOIN dbo.Tb002_Portfolios B  (nolock) " +
                        "        ON X.Id_Portfolio=B.Id_Portfolio  " +
                        "        LEFT JOIN dbo.Tb001_Securities C  (nolock) " +
                        "        ON A.Id_Ticker = C.IdSecurity  " +
                        "        LEFT JOIN Tb011_Corretoras D (nolock)  " +
                        "        ON M.Id_Broker = D.Id_Corretora  " +
                        "        LEFT JOIN dbo.Tb127_Type_Stock_Loan E (nolock)  " +
                        "        ON A.Id_Type = E.Id_Type_Stock_Loan  " +
                        "        WHERE Id_Port_Type=1 AND CBLC_Id not IN (  " +
                        "        SELECT Contrato FROM dbo.Tb710_Stock_Loans_Import WHERE File_Date=(SELECT MAX(File_Date) FROM Tb710_Stock_Loans_Import (nolock) )) " +
                        "        AND Status<> 4 AND Loan_Maturity>=CONVERT(varchar,GetDate(),112) OR CBLC_Id IS NULL " +
                        "    ) AS Q WHERE Quantity<>0 AND Status<> 4 " +
                        " ) AS L " +
                        " ON K.Id_Account=L.Id_Account " +
                        " WHERE K.Id_Account IS NULL" +
                        " ORDER BY Id_Account, NestTicker ";

            tablep = curConn.Return_DataTable(SQLString);

            dtgNRLoans.DataSource = tablep;
            

            // Close Button
            dgNRLoans.Columns.AddField("Close");
            dgNRLoans.Columns["Close"].VisibleIndex = 0;
            dgNRLoans.Columns["Close"].Width = 60;
            RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
            item3.Buttons[0].Tag = 1;
            item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item3.Buttons[0].Caption = "Close";
            dtgNRLoans.RepositoryItems.Add(item3);
            dgNRLoans.Columns["Close"].ColumnEdit = item3;
            dgNRLoans.Columns["Close"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgNRLoans.OptionsBehavior.Editable = false;
            dgNRLoans.Columns["Close"].Visible = true;

            // Cancel Button
            dgNRLoans.Columns.AddField("Cancel");
            dgNRLoans.Columns["Cancel"].VisibleIndex = 1;
            dgNRLoans.Columns["Cancel"].Width = 60;
            RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
            item4.Buttons[0].Tag = 1;
            item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item4.Buttons[0].Caption = "Cancel";
            dtg.RepositoryItems.Add(item4);
            dgNRLoans.Columns["Cancel"].ColumnEdit = item4;
            dgNRLoans.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgNRLoans.OptionsBehavior.Editable = false;
            dgNRLoans.Columns["Cancel"].Visible = true;

            dgNRLoans.Columns["Loan_Rate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgNRLoans.Columns["Loan_Rate"].DisplayFormat.FormatString = "0.00%;(0.00%)";
            dgNRLoans.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgNRLoans.Columns["Quantity"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgNRLoans.Columns["Trade_Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgNRLoans.Columns["Trade_Date"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgNRLoans.Columns["Trade_Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            dgNRLoans.Columns["Loan_Maturity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgNRLoans.Columns["Loan_Maturity"].DisplayFormat.FormatString = "dd-MMM-yy";
            dgNRLoans.Columns["Loan_Maturity"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            curUtils.SetColumnStyle(dgNRLoans, 1);


        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frmInsertLoan InsereAluguel = new frmInsertLoan();
            InsereAluguel.ShowDialog();
            tabReconcile_SelectedIndexChanged(sender, e);
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
            if (tabLoans.SelectedIndex == 3)
            {
                tabReconcile_SelectedIndexChanged(sender, e);
            }
        }

        private void Default_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(dgStockLoan); 
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(dgStockLoan);
        }

        private void dgStockLoan_DoubleClick(object sender, EventArgs e)
        {
            int resposta;

            if (dgStockLoan.RowCount > 0 && dgStockLoan.FocusedRowHandle >= 0)
            {

                int Loan_Id = (int)dgStockLoan.GetRowCellValue(dgStockLoan.FocusedRowHandle, "Loan_Id");
                GridView Get_Column = sender as GridView;
                string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

                if (Column_Name == "Cancel")
                {
                    if (Loan_Id != 0)
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to mark this Stock Loan entry as cancelled?", "Cancel Stock Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            string SQLString;
                            SQLString = "EXEC dbo.[proc_Cancel_Stock_Loan] @Loan_Id=" + Loan_Id;
                            curConn.ExecuteNonQuery(SQLString,1);
                            Load_Positions();
                        }
                    }
                }
                if (Column_Name == "Close")
                {
                    if (Loan_Id != 0)
                    {
                        frmStock_Loan_Close LoanClose = new frmStock_Loan_Close();
                        //LoanRenew.ini();
                        LoanClose.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                        LoanClose.Left = this.Left + Convert.ToInt32(this.Width / 2 - 150);
                        LoanClose.txtQtd.Text = dgStockLoan.GetRowCellValue(dgStockLoan.FocusedRowHandle, "Quantity").ToString();
                        LoanClose.txtFullQtd.Text = Math.Abs(Convert.ToDouble(dgStockLoan.GetRowCellValue(dgStockLoan.FocusedRowHandle, "Quantity"))).ToString();
                        LoanClose.rdFullClose.Checked = true;
                        LoanClose.txtRelID.Text = dgStockLoan.GetRowCellValue(dgStockLoan.FocusedRowHandle, "Loan_Id").ToString();
                        LoanClose.ShowDialog();
                    }
                }
                if (Column_Name == "Renew")
                {
                    if (Loan_Id != 0)
                    {
                        frmInsertLoan LoanRenew = new frmInsertLoan();
                        //LoanRenew.ini();
                        LoanRenew.Text = "Loan Renew";
                        LoanRenew.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                        LoanRenew.Left = this.Left + 10;
                        LoanRenew.Carrega_Combo();
                        LoanRenew.cmbFundo.SelectedValue = dgStockLoan.GetRowCellValue(dgStockLoan.FocusedRowHandle, "Id_Portfolio").ToString();
                        LoanRenew.Carrega_Account();
                        LoanRenew.cmbCorretora.SelectedValue = dgStockLoan.GetRowCellValue(dgStockLoan.FocusedRowHandle, "Id_Account").ToString();
                        LoanRenew.cmbTicker.SelectedValue = dgStockLoan.GetRowCellValue(dgStockLoan.FocusedRowHandle, "Id_Ticker");
                        LoanRenew.txtQtd.Text = dgStockLoan.GetRowCellValue(dgStockLoan.FocusedRowHandle, "Quantity").ToString();
                        LoanRenew.txtRelID.Text = dgStockLoan.GetRowCellValue(dgStockLoan.FocusedRowHandle, "Loan_Id").ToString();
                        LoanRenew.dtpVencto.Value = Convert.ToDateTime(curConn.Execute_Query_String("SELECT CONVERT(varchar, dbo.FCN_NDATEADD('du', 1, DATEADD(d, 29, getdate()), 2, 900), 102)"));
                        if (Convert.ToInt32(dgStockLoan.GetRowCellValue(dgStockLoan.FocusedRowHandle, "Id_Side")) == 1)
                        { LoanRenew.rdDom.Checked = true; }
                        else
                        { LoanRenew.rdTom.Checked = true; }
                        LoanRenew.ShowDialog();
                        //dgStockLoan.GetRelationNameGetDisplayTextByColumnValue(5,);
                    }
                }
            }
        }

        private void dgLoanTransactions_DoubleClick(object sender, EventArgs e)
        {
            int resposta;

            if (dgLoanTransactions.RowCount > 0)
            {
                int Loan_Id = (int)dgLoanTransactions.GetRowCellValue(dgLoanTransactions.FocusedRowHandle, "Loan_Id");
                string Column_Name = dgLoanTransactions.FocusedColumn.Caption.ToString();
                DateTime Trade_Date = (DateTime)dgLoanTransactions.GetRowCellValue(dgLoanTransactions.FocusedRowHandle, "Trade_Date");

                if (Column_Name == "Cancel")
                {
                    string strType = (string)dgLoanTransactions.GetRowCellValue(dgLoanTransactions.FocusedRowHandle, "Trans_Type");

                    if (Loan_Id != 0 && (strType == "NW" || strType == "RN"))
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this Stock Loan? (All transactions related to this loan will be cancelled!)", "Cancel Stock Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            string SQLString;
                            SQLString = "EXEC dbo.[proc_Cancel_Stock_Loan] @Loan_Id=" + Loan_Id;
                            curConn.ExecuteNonQuery(SQLString,1);
                            Load_Transactions();
                        }
                    }
                    if (Loan_Id != 0 && (strType == "PC" || strType == "FC"))
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this Early Close transaction?", "Cancel Stock Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            string SQLString;
                            int intType = 0;
                            if (strType == "PC") { intType = 3; };
                            if (strType == "FC") { intType = 4; };
                            SQLString = "UPDATE dbo.Tb710_Stock_Loans_Early_Close SET Status=4 WHERE Loan_Id=" + Loan_Id + " AND Close_Date='" + Trade_Date.ToString("yyyyMMdd") + "' AND Transaction_Type=" + intType + "";
                            curConn.ExecuteNonQuery(SQLString,1);
                            Load_Transactions();
                        }
                    }
                }
            }
        }

        private void dgLoanTransactions2_DoubleClick(object sender, EventArgs e)
        {
            int resposta;

            if (dgLoanTransactions2.RowCount > 0)
            {
                int Loan_Id = (int)dgLoanTransactions2.GetRowCellValue(dgLoanTransactions2.FocusedRowHandle, "Loan_Id");
                string Column_Name = dgLoanTransactions2.FocusedColumn.Caption.ToString();
                DateTime Trade_Date = (DateTime)dgLoanTransactions2.GetRowCellValue(dgLoanTransactions2.FocusedRowHandle, "Trade_Date");

                if (Column_Name == "Cancel")
                {
                    string strType = (string)dgLoanTransactions2.GetRowCellValue(dgLoanTransactions2.FocusedRowHandle, "Trans_Type");

                    if (Loan_Id != 0 && (strType == "NW" || strType == "RN" || strType==null))
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this Stock Loan? (All transactions related to this loan will be cancelled!)", "Cancel Stock Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            string SQLString;
                            SQLString = "EXEC dbo.[proc_Cancel_Stock_Loan] @Loan_Id=" + Loan_Id;
                            curConn.ExecuteNonQuery(SQLString,1);
                            tabReconcile_SelectedIndexChanged(this, e);
                        }
                    }
                    if (Loan_Id != 0 && (strType == "PC" || strType == "FC"))
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this Early Close transaction?", "Cancel Stock Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            string SQLString;
                            int intType = 0;
                            if (strType == "PC") { intType = 3; };
                            if (strType == "FC") { intType = 4; };
                            SQLString = "UPDATE dbo.Tb710_Stock_Loans_Early_Close SET Status=4 WHERE Loan_Id=" + Loan_Id + " AND Close_Date='" + Trade_Date.ToString("yyyyMMdd") + "' AND Transaction_Type=" + intType + "";
                            curConn.ExecuteNonQuery(SQLString,1);
                            tabReconcile_SelectedIndexChanged(this, e);
                        }
                    }
                }
            }
        }

        private void dgNotDB_DoubleClick(object sender, EventArgs e)
        {
            if (dgNotDB.RowCount > 0)
            {
                GridView Get_Column = sender as GridView;
                string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

                string strTicker = (string)dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Cod_Neg");

                string Id_Ticker = curConn.Execute_Query_String("SELECT IdSecurity FROM dbo.Tb001_Securities (nolock) WHERE NestTicker='" + strTicker + "'");

                switch (Column_Name)
                {
                    case "Insert":
                        if (Id_Ticker == "")
                        {
                            MessageBox.Show("Id_Ticker not found for that Security!!");
                        }
                        else
                        {
                            frmInsertLoan LoanRenew = new frmInsertLoan();
                            //LoanRenew.ini();
                            LoanRenew.Text = "Insert New Loan";
                            LoanRenew.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                            LoanRenew.Left = this.Left + 10;
                            LoanRenew.Carrega_Combo();
                            LoanRenew.cmbFundo.SelectedValue = dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Id_Portfolio").ToString();
                            LoanRenew.Carrega_Account();
                            LoanRenew.cmbCorretora.SelectedValue = dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Id_Account").ToString();
                            LoanRenew.txtQtd.Text = dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Quantidade").ToString();
                            LoanRenew.cmbTicker.SelectedValue = Id_Ticker;
                            LoanRenew.dtpIni.Value = Convert.ToDateTime(dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Data_Registro"));
                            LoanRenew.dtpVencto.Value = Convert.ToDateTime(dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Data_Vecto"));
                            LoanRenew.txtCBLCId.Text = dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Contrato").ToString();
                            LoanRenew.txtTaxa.Text = (Convert.ToDouble(dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Taxa")) * 100).ToString();
                            if (Convert.ToString(dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "DT")) == "D")
                            { LoanRenew.rdDom.Checked = true; }
                            else
                            { LoanRenew.rdTom.Checked = true; }

                            if (Convert.ToString(dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Liq_Ant")) == "NAO")
                            {
                                LoanRenew.RdFixo.Checked = true;
                            }
                            else
                            {
                                if (Convert.ToString(dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Liq_Ant_Doador")) == "NAO")
                                {
                                    LoanRenew.rdNormal.Checked = true;
                                }
                                else
                                {
                                    LoanRenew.rdRev.Checked = true;
                                }
                            }

                            //LoanRenew.ShowDialog();
                            LoanRenew.InsetLoan();
                            Load_NotDB();
                        }
                        break;
                    case "Update":
                        int Loan_Id = Convert.ToInt32(dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Loand_ID").ToString());

                        frmStock_Change_Maturity Change_Maturity = new frmStock_Change_Maturity();
                        Change_Maturity.Loan_ID = Loan_Id;
                        Change_Maturity.ShowDialog();

                        break;

                    default:
                        string Contrato = dgNotDB.GetRowCellValue(dgNotDB.FocusedRowHandle, "Contrato").ToString();
                        dgCBLCData.Columns["Contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Contrato] = '" + Contrato + "'");

                        break;
                }
            }
        }

        private void dgNotCBLC_DoubleClick(object sender, EventArgs e)
        {
            int resposta;

            if (dgNotCBLC.RowCount > 0)
            {
                int Loan_Id = (int)dgNotCBLC.GetRowCellValue(dgNotCBLC.FocusedRowHandle, "Loan_Id");
                GridView Get_Column = sender as GridView;
                string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

                if (Column_Name == "Close/Cancel")
                {
                    if (dgNotCBLC.GetRowCellValue(dgNotCBLC.FocusedRowHandle, "CBLC_Id").ToString() != "")
                    {
                        if (Loan_Id != 0)
                        {
                            frmStock_Loan_Close LoanClose = new frmStock_Loan_Close();
                            //LoanRenew.ini();
                            LoanClose.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                            LoanClose.Left = this.Left + Convert.ToInt32(this.Width / 2 - 150);
                            LoanClose.txtQtd.Text = dgNotCBLC.GetRowCellValue(dgNotCBLC.FocusedRowHandle, "Quantity").ToString();
                            LoanClose.txtFullQtd.Text = Math.Abs(Convert.ToDouble(dgNotCBLC.GetRowCellValue(dgNotCBLC.FocusedRowHandle, "Quantity"))).ToString();
                            LoanClose.rdFullClose.Checked = true;
                            LoanClose.dtpIni.Value = Convert.ToDateTime(curConn.Execute_Query_String("SELECT CONVERT(varchar, dbo.FCN_NDATEADD('du', -1, getdate(), 2, 900), 102)"));
                            LoanClose.txtRelID.Text = dgNotCBLC.GetRowCellValue(dgNotCBLC.FocusedRowHandle, "Loan_Id").ToString();
                            LoanClose.ShowDialog();
                            tabReconcile_SelectedIndexChanged(sender, e);
                        }
                    }
                    else 
                    {
                        if (Loan_Id != 0)
                        {
                            resposta = Convert.ToInt32(MessageBox.Show("Do you really want to mark this Stock Loan entry as cancelled?", "Cancel Stock Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                            if (resposta == 6)
                            {
                                string SQLString;
                                SQLString = "EXEC dbo.[proc_Cancel_Stock_Loan] @Loan_Id=" + Loan_Id;
                                curConn.ExecuteNonQuery(SQLString,1);
                                tabReconcile_SelectedIndexChanged(sender, e);
                            }
                        }
                    }
                }
                else
                {
                    string Contrato = dgNotCBLC.GetRowCellValue(dgNotCBLC.FocusedRowHandle, "CBLC_Id").ToString();
                    if (dgLoanTransactions2.Columns.Count == 0) { Load_Transactions2(true); };
                    dgCBLCData.Columns["Contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Contrato] = '" + Contrato + "'");
                    dgLoanTransactions2.Columns["Loan_Id"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Loan_Id] = '" + Loan_Id + "'");
                }
            }
        }

        private void dgDifQuantity_DoubleClick(object sender, EventArgs e)
        {
            int resposta;

            if (dgDifQuantity.RowCount > 0)
            {

                int Loan_Id = (int)dgDifQuantity.GetRowCellValue(dgDifQuantity.FocusedRowHandle, "Loan_Id");
                GridView Get_Column = sender as GridView;
                string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

                if (Column_Name == "Close")
                {
                    if (Loan_Id != 0)
                    {
                        frmStock_Loan_Close LoanClose = new frmStock_Loan_Close();
                        //LoanRenew.ini();
                        LoanClose.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                        LoanClose.Left = this.Left + Convert.ToInt32(this.Width / 2 - 150);
                        LoanClose.rdPartialClose.Checked = true;
                        LoanClose.txtFullQtd.Text = Math.Abs(Convert.ToDouble(dgDifQuantity.GetRowCellValue(dgDifQuantity.FocusedRowHandle, "Quantity"))).ToString();
                        LoanClose.txtQtd.Text = (Math.Abs(Convert.ToInt32(dgDifQuantity.GetRowCellValue(dgDifQuantity.FocusedRowHandle, "Quantity"))) - Math.Abs(Convert.ToInt32(dgDifQuantity.GetRowCellValue(dgDifQuantity.FocusedRowHandle, "Quant_CBLC")))).ToString(); ;
                        LoanClose.txtRelID.Text = dgDifQuantity.GetRowCellValue(dgDifQuantity.FocusedRowHandle, "Loan_Id").ToString();
                        LoanClose.ShowDialog();
                        Load_DifQuantity();
                    }
                }
                else if (Column_Name == "Cancel")
                {
                    if (Loan_Id != 0)
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to mark this Stock Loan entry as cancelled?", "Cancel Stock Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            string SQLString;
                            SQLString = "EXEC dbo.[proc_Cancel_Stock_Loan] @Loan_Id=" + Loan_Id;
                            curConn.ExecuteNonQuery(SQLString,1);
                            Load_DifQuantity();
                            Load_NotCBLC();
                            Load_NotDB();
                        }
                    }
                }
                else
                {
                    string Contrato = dgDifQuantity.GetRowCellValue(dgDifQuantity.FocusedRowHandle, "Imported_CBLC_Id").ToString();
                    dgCBLCData.Columns["Contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Contrato] = '" + Contrato + "'");
                    dgLoanTransactions2.Columns["Loan_Id"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Loan_Id] = '" + Loan_Id + "'");
                }

            }
        }

        private void dgSimilar_DoubleClick(object sender, EventArgs e)
        {
            if (dgSimilar.RowCount > 0)
            {
                if (dgLoanTransactions2.Columns.Count == 0) { Load_Transactions2(false); };
                if (dgCBLCData.Columns.Count == 0) { Load_CBLCData(); };

                string Trade_Date = dgSimilar.GetRowCellValue(dgSimilar.FocusedRowHandle, "Trade_Date").ToString();
                string Id_Portfolio = dgSimilar.GetRowCellValue(dgSimilar.FocusedRowHandle, "Id_Portfolio").ToString();
                string Broker = dgSimilar.GetRowCellValue(dgSimilar.FocusedRowHandle, "Broker").ToString();
                string Ticker = dgSimilar.GetRowCellValue(dgSimilar.FocusedRowHandle, "Ticker").ToString();

                dgCBLCData.Columns["Contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Data_Registro] = '" + Trade_Date + "' AND [Id_Portfolio]=" + Id_Portfolio + " AND [Broker]='" + Broker + "' AND [Cod_Neg]='" + Ticker + "'");
                dgLoanTransactions2.Columns["Loan_Id"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[Trade_Date] = '" + Trade_Date + "' AND [Id_Portfolio]=" + Id_Portfolio + " AND [Broker]='" + Broker + "' AND [NestTicker]='" + Ticker + "'");

                float tempTotal=0;

                for (int c = 0; c < dgCBLCData.RowCount; c++)
                {
                    tempTotal += Convert.ToInt32(dgCBLCData.GetRowCellValue(c, "Quantidade"));
                }
                lblQuantCBLC.Text = "(Total quantity: " + tempTotal.ToString("#,###") + ")";

                tempTotal = 0;

                for (int c = 0; c < dgLoanTransactions2.RowCount; c++)
                {
                    tempTotal += Convert.ToInt32(dgLoanTransactions2.GetRowCellValue(c, "Quantity"));
                }
                lblQuantDB.Text = "(Total quantity: " + tempTotal.ToString("#,###") + ")";

                if (dgCBLCData.RowCount == 1 && dgLoanTransactions2.RowCount == 1 && rdMatch.Checked == true)
                {
                    cmdMatch.Enabled = true;
                }
                else 
                {
                    cmdMatch.Enabled = false;
                }
            }
        }

        private void dgNRLoans_DoubleClick(object sender, EventArgs e)
        {
            int resposta;

            if (dgNRLoans.RowCount > 0)
            {

                int Loan_Id = (int)dgNRLoans.GetRowCellValue(dgNRLoans.FocusedRowHandle, "Loan_Id");
                GridView Get_Column = sender as GridView;
                string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

                if (Column_Name == "Cancel")
                {
                    if (Loan_Id != 0)
                    {
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want to mark this Stock Loan entry as cancelled?", "Cancel Stock Loan", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                        if (resposta == 6)
                        {
                            string SQLString;
                            SQLString = "EXEC dbo.[proc_Cancel_Stock_Loan] @Loan_Id=" + Loan_Id;
                            curConn.ExecuteNonQuery(SQLString,1);
                            Load_NRLoans();
                        }
                    }
                }
                if (Column_Name == "Close")
                {
                    if (Loan_Id != 0)
                    {
                        frmStock_Loan_Close LoanClose = new frmStock_Loan_Close();
                        //LoanRenew.ini();
                        LoanClose.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                        LoanClose.Left = this.Left + Convert.ToInt32(this.Width / 2 - 150);
                        LoanClose.txtQtd.Text = dgNRLoans.GetRowCellValue(dgNRLoans.FocusedRowHandle, "Quantity").ToString();
                        LoanClose.txtFullQtd.Text = Math.Abs(Convert.ToDouble(dgNRLoans.GetRowCellValue(dgNRLoans.FocusedRowHandle, "Quantity"))).ToString();
                        LoanClose.rdFullClose.Checked = true;
                        LoanClose.txtRelID.Text = dgNRLoans.GetRowCellValue(dgNRLoans.FocusedRowHandle, "Loan_Id").ToString();
                        LoanClose.ShowDialog();
                    }
                }

            }
        }

        private void dgCBLCData_DoubleClick(object sender, EventArgs e)
        {
            if (dgCBLCData.RowCount > 0)
            {
                GridView Get_Column = sender as GridView;
                string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

                string strTicker = (string)dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Cod_Neg");

                string Id_Ticker = curConn.Execute_Query_String("SELECT IdSecurity FROM dbo.Tb001_Securities (nolock) WHERE NestTicker='" + strTicker + "'");

                if (Column_Name == "Insert" && dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Loan_Id").ToString() == "")
                {
                    if (Id_Ticker == "")
                    {
                        MessageBox.Show("Id_Ticker not found for that Security!!");
                    }
                    else
                    {
                        frmInsertLoan LoanRenew = new frmInsertLoan();
                        LoanRenew.Text = "Insert New Loan";
                        LoanRenew.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                        LoanRenew.Left = this.Left + 10;
                        LoanRenew.Carrega_Combo();
                        LoanRenew.cmbFundo.SelectedValue = dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Id_Portfolio").ToString();
                        LoanRenew.Carrega_Account();
                        LoanRenew.cmbCorretora.SelectedValue = dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Id_Account").ToString();
                        LoanRenew.txtQtd.Text = dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Quantidade").ToString();
                        LoanRenew.cmbTicker.SelectedValue = Id_Ticker;
                        LoanRenew.dtpIni.Value = Convert.ToDateTime(dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Data_Registro"));
                        LoanRenew.dtpVencto.Value = Convert.ToDateTime(dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Data_Vecto"));
                        LoanRenew.txtCBLCId.Text = dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Contrato").ToString();
                        LoanRenew.txtTaxa.Text = (Convert.ToDouble(dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Taxa")) * 100).ToString();
                        if (Convert.ToString(dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "DT")) == "D")
                        { LoanRenew.rdDom.Checked = true; }
                        else
                        { LoanRenew.rdTom.Checked = true; }

                        if (Convert.ToString(dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Liq_Ant")) == "NAO")
                        {
                            LoanRenew.RdFixo.Checked = true;
                        }
                        else
                        {
                            if (Convert.ToString(dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Liq_Ant_Doador")) == "NAO")
                            {
                                LoanRenew.rdNormal.Checked = true;
                            }
                            else
                            {
                                LoanRenew.rdRev.Checked = true;
                            }
                        }

                        LoanRenew.InsetLoan();
                        string tempfilter = dgCBLCData.FilterPanelText.ToString();
                        Load_CBLCData();
                        dgCBLCData.Columns["Contrato"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, tempfilter);
                    }
                }
            }
        }

        private void dgCBLCData_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (dgCBLCData.Columns.Count > 0 && e.RowHandle >= 0)
            {
                if (dgCBLCData.GetRowCellValue(e.RowHandle, "Loan_Id").ToString() == "")
                {
                    e.Appearance.BackColor = Color.FromArgb(250, 220, 216);
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
                weightColumn = dgStockLoan.Columns["Fin_Amount_Now"];
            }
            else
            {
                weightColumn = dgStockLoan.Columns["Fin_Amount_Open"];
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

        private void dgStockLoan_EndGrouping(object sender, EventArgs e)
        {

            dgStockLoan.GroupSummary.Clear();

            dgStockLoan.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgStockLoan.Columns["Quantity"], "{0:#,#0}");

            dgStockLoan.GroupSummary.Add(SummaryItemType.Custom, "New_Rate", dgStockLoan.Columns["New_Rate"], "{0:0.00%}");
            dgStockLoan.GroupSummary.Add(SummaryItemType.Custom, "Loan_Rate", dgStockLoan.Columns["Loan_Rate"], "{0:0.00%}");

            dgStockLoan.GroupSummary.Add(SummaryItemType.Sum, "Fin_Amount_Open", dgStockLoan.Columns["Fin_Amount_Open"], "{0:#,#0}");
            dgStockLoan.GroupSummary.Add(SummaryItemType.Sum, "Fin_Amount_Now", dgStockLoan.Columns["Fin_Amount_Now"], "{0:#,#0}");
            dgStockLoan.GroupSummary.Add(SummaryItemType.Sum, "Cost_Contrib", dgStockLoan.Columns["Cost_Contrib"], "{0:0.00%}");

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
            dgStockLoan.ExpandAllGroups();
            //dgStockLoan.ExpandAllGroups();
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            dgStockLoan.CollapseAllGroups();
        }

        private void cmdExcel_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                string user = Environment.UserName.ToString();
                string Loca_Machine = Environment.MachineName.ToString();
                string fileName_Log = "T:\\Log\\Reports\\StockLoan_Id_LB_" + NestDLL.NUserControl.Instance.User_Id + "_Id_AD_" + user + "_Computer_" + Loca_Machine + "_Date_" + DateTime.Now.ToString("yyyyMMdd_HH-mm-ss") + ".xls";
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
            BaseExportLink link = dgStockLoan.CreateExportLink(provider);
            if (tabLoans.SelectedIndex == 0) 
            {
                link=dgStockLoan.CreateExportLink(provider);
            }
            if (tabLoans.SelectedIndex == 1)
            {
                link = dgLoanTransactions.CreateExportLink(provider);
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
                    curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void chkQuantity_CheckStateChanged(object sender, EventArgs e)
        {
            Load_Similar();
        }

        private void chkType_CheckStateChanged(object sender, EventArgs e)
        {
            Load_Similar();
        }

        private void chkMaturity_CheckStateChanged(object sender, EventArgs e)
        {
            Load_Similar();
        }

        private void chkRate_CheckStateChanged(object sender, EventArgs e)
        {
            Load_Similar();
        }

        private void cmdMatch_Click(object sender, EventArgs e)
        {
            if (dgLoanTransactions2.RowCount == 1 && dgCBLCData.RowCount == 1 && dgLoanTransactions2.FocusedRowHandle == 0 && dgCBLCData.FocusedRowHandle == 0)
            {
                int Loan_Id = Convert.ToInt32(dgLoanTransactions2.GetRowCellValue(dgLoanTransactions2.FocusedRowHandle, "Loan_Id"));
                string CBLC_Id = dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Contrato").ToString();
                if (Loan_Id != 0 && CBLC_Id != "")
                {
                    int resposta = Convert.ToInt32(MessageBox.Show("Update the CBLC Id for this Loan?", "Margin", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                    if (resposta == 6)
                    {
                        string SQLString;
                        SQLString = "UPDATE dbo.Tb710_Stock_Loans SET CBLC_Id='" + dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Contrato") + "' WHERE Loan_Id=" + Loan_Id;
                        curConn.ExecuteNonQuery(SQLString,1);
                        tabLoans_SelectedIndexChanged(this, e);
                    }
                }
            }
        }

        private void rdMatch_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMatch.Checked == true)
            {
                chkQuantity.Enabled = false;
                chkType.Enabled = false;
                chkMaturity.Enabled = false;
                chkRate.Enabled = false;
                cmdMatch.Enabled = true;
                Load_Similar();
            }
        }

        private void rdDifferent_CheckedChanged(object sender, EventArgs e)
        {
            if (rdDifferent.Checked == true)
            {
                chkQuantity.Enabled = true;
                chkType.Enabled = true;
                chkMaturity.Enabled = true;
                chkRate.Enabled = true;
                cmdMatch.Enabled = false;
                Load_Similar();
            }
        }

        private void tabReconcile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabReconcile.SelectedIndex == 0)
            {
                Load_CBLCData();
                Load_Transactions2(false);
                rdMatch.Checked = true;
                rdMatch_CheckedChanged(sender, e);
            }
            if (tabReconcile.SelectedIndex == 1)
            {
                Load_CBLCData();
                Load_Transactions2(true);
                cmdMatch.Enabled = false;
                Load_DifQuantity();
            }
            if (tabReconcile.SelectedIndex == 2)
            {
                Load_CBLCData();
                Load_Transactions2(true);
                cmdMatch.Enabled = false;
                Load_NotCBLC();
            }
            if (tabReconcile.SelectedIndex == 3)
            {
                Load_CBLCData();
                Load_Transactions2(true);
                cmdMatch.Enabled = false;
                Load_NotDB();
            }
        }

        private void dgReceivedFiles_RowStyle(object sender, RowStyleEventArgs e)
        {
            /*
            if (Convert.ToDateTime(dgReceivedFiles.GetRowCellValue(e.RowHandle, "LastReceived")) != MaxDate )
            {
                e.Appearance.BackColor = Color.FromArgb(222, 254, 235);
            }
            */
        }

        private void dgReceivedFiles_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "colLastReceived")
            {
                if (Convert.ToDateTime(e.CellValue) != MaxDate && e.Appearance.ForeColor != Color.Green)
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
 
            }
        }
    }
}