using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using LiveDLL;

namespace LiveBook
{
    public partial class frmCheckPortfolioAdministrator : LBForm
    {
        newNestConn curConn = new newNestConn();
        bool GridInitialized = false;
        int IdPortfolio;
        DateTime TradeDate;
        public frmCheckPortfolioAdministrator()
        {
            InitializeComponent();

            dtgDiferencesAdmin1.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgDiferencesAdmin1.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgDiferencesAdmin1.LookAndFeel.SetSkinStyle("Blue");
        }

        private void frmCheckPortfolioAdministrator_Load(object sender, EventArgs e)
        {
            dgDiferencesAdmin1.ColumnPanelRowHeight = 32;
            dgDiferencesAdmin1.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            LoadHTML();
            radAll.Checked = true;

            DateTime histDate = DateTime.Now.AddDays(-1);
            if (histDate.DayOfWeek == DayOfWeek.Sunday) histDate = DateTime.Now.AddDays(-3);

            dtpDate.Value = histDate;

        }

        private double Load_Comparison()
        {
            string SQLString;

            DataTable tablep = new DataTable();

            //dgDiferencesAdmin1.Columns.Clear();

            //------------------------------------------------------------------------
            IdPortfolio = Int32.Parse(cmbFund.SelectedValue.ToString());
            TradeDate = dtpDate.Value;

            if (IdPortfolio == 4)
            {
                cmdInsertPrice.Enabled = true;
            }
            else
            {
                cmdInsertPrice.Enabled = false;
            }
            if (IdPortfolio == 43)
            {
                lblFunds.Enabled = false;
            }
            else
            {
                lblFunds.Enabled = true;
            }

            SQLString = "SET ARITHABORT ON; SELECT 'PORTFOLIO' AS Portfolio, *, CASE WHEN MellonType='EXPENSES' AND DiferenceQuantity<>0 THEN 'Insert' ELSE '' END AS [Insert], CASE WHEN MellonType='EXPENSES' AND DiferenceQuantity<>0 THEN 'Charge Cash' ELSE '' END AS [Charge Cash] FROM NESTDB.dbo.FCN_Check_Portfolio_Administrator(" + IdPortfolio + ",'" + TradeDate.ToString("yyyyMMdd") + "')";

            tablep = curConn.Return_DataTable(SQLString);
            dtgDiferencesAdmin1.DataSource = tablep;

            tablep.Dispose();

            if (!GridInitialized && dgDiferencesAdmin1.RowCount > 0)
                InitializeGrid();

            double NAVLB = 0;
            double NAVAdmin = 0;

            double EQ_LB = 0;
            double OP_LB = 0;
            double FW_LB = 0;
            double CX_LB = 0;
            double BD_LB = 0;


            double EQ_Admin = 0;
            double OP_Admin = 0;
            double FW_Admin = 0;
            double CX_Admin = 0;
            double BD_Admin = 0;

            double EQ_Diff = 0;
            double OP_Diff = 0;
            double FW_Diff = 0;
            double CX_Diff = 0;
            double BD_Diff = 0;

            foreach (DataRow curRow in tablep.Rows)
            {
                NAVLB = NAVLB + LiveDLL.Utils.ParseToDouble(curRow["CashLiveBook"]);
                NAVAdmin = NAVAdmin + LiveDLL.Utils.ParseToDouble(curRow["CashAdmin"]);

                switch (curRow["MellonType"].ToString())
                {
                    case "EQUITY": EQ_LB += LiveDLL.Utils.ParseToDouble(curRow["CashLiveBook"]); EQ_Admin += LiveDLL.Utils.ParseToDouble(curRow["CashAdmin"]); break;
                    case "OPTION BRL":
                    case "OPTION OFF":
                    case "OPTION FUT":
                        OP_LB += LiveDLL.Utils.ParseToDouble(curRow["CashLiveBook"]); OP_Admin += LiveDLL.Utils.ParseToDouble(curRow["CashAdmin"]); break;
                    case "FORWARD": FW_LB += LiveDLL.Utils.ParseToDouble(curRow["CashLiveBook"]); FW_Admin += LiveDLL.Utils.ParseToDouble(curRow["CashAdmin"]); break;
                    case "BONDS": BD_LB += LiveDLL.Utils.ParseToDouble(curRow["CashLiveBook"]); BD_Admin += LiveDLL.Utils.ParseToDouble(curRow["CashAdmin"]); break;
                    case "CASH":
                    case "FUNDS":
                    case "EXPENSES": CX_LB += LiveDLL.Utils.ParseToDouble(curRow["CashLiveBook"]); CX_Admin += LiveDLL.Utils.ParseToDouble(curRow["CashAdmin"]); break;
                    default: break;
                }
            }

            EQ_Diff = (EQ_LB - EQ_Admin);
            OP_Diff = (OP_LB - OP_Admin);
            FW_Diff = (FW_LB - FW_Admin);
            CX_Diff = (CX_LB - CX_Admin);
            CX_Diff = (CX_LB - CX_Admin);
            BD_Diff = (BD_LB - BD_Admin);


            txtEQ_Admin.Text = EQ_Admin.ToString("#,##0.00");
            txtEQ_LB.Text = EQ_LB.ToString("#,##0.00");
            txtEQ_Diff.Text = EQ_Diff.ToString("#,##0.00");

            txtOP_Admin.Text = OP_Admin.ToString("#,##0.00");
            txtOP_LB.Text = OP_LB.ToString("#,##0.00");
            txtOP_Diff.Text = OP_Diff.ToString("#,##0.00");

            txtFW_Admin.Text = FW_Admin.ToString("#,##0.00");
            txtFW_LB.Text = FW_LB.ToString("#,##0.00");
            txtFW_Diff.Text = FW_Diff.ToString("#,##0.00");

            txtCX_Admin.Text = CX_Admin.ToString("#,##0.00");
            txtCX_LB.Text = CX_LB.ToString("#,##0.00");
            txtCX_Diff.Text = CX_Diff.ToString("#,##0.00");

            txtBD_Admin.Text = BD_Admin.ToString("#,##0.00");
            txtBD_LB.Text = BD_LB.ToString("#,##0.00");
            txtBD_Diff.Text = BD_Diff.ToString("#,##0.00");

            txtNAVLivebook.Text = NAVLB.ToString("#,##0.00");
            txtNAVAdmin.Text = NAVAdmin.ToString("#,##0.00");
            txtNAVDiff.Text = (NAVLB - NAVAdmin).ToString("#,##0.00");


            SQLString = "SELECT Valor_PL FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio=" + IdPortfolio + " and Data_PL='" + TradeDate.ToString("yyyyMMdd") + "'";

            double NAVTableLivebook = curConn.Return_Double(SQLString);

            txtNAVTableLivebook.Text = NAVTableLivebook.ToString("#,##0.00");

            if (txtNAVTableLivebook.Text != txtNAVLivebook.Text)
            {
                txtNAVTableLivebook.ForeColor = Color.Red;
            }
            else
            {
                txtNAVTableLivebook.ForeColor = Color.Black;
            }

            if (IdPortfolio == 4)
            {
                SQLString = "SELECT TOP 1 NAVAssetValue FROM NESTIMPORT.dbo.Tb_BTG_NAVShares WHERE IdPortfolio=" + IdPortfolio + " and RefDate='" + TradeDate.ToString("yyyyMMdd") + "' ORDER BY NAVAssetValue DESC";

            }
            else if (IdPortfolio == 50)
            {
                SQLString = "SELECT TOP 1 Patrimonio FROM [NESTIMPORT].[dbo].[Tb_Itau_Patrimonio_Cotas]  WHERE IdPortfolio=" + IdPortfolio + " and RefDate='" + TradeDate.ToString("yyyyMMdd") + "' ORDER BY Patrimonio DESC";
            }

            else
            {
                SQLString = "SELECT TOP 1 Patrimonio FROM NESTIMPORT.dbo.Tb_Mellon_Patrimonio WHERE IdPortfolio=" + IdPortfolio + " and RefDate='" + TradeDate.ToString("yyyyMMdd") + "' ORDER BY Patrimonio DESC";
            }

            double NAVFile = curConn.Return_Double(SQLString);

            if (NAVFile > 0)
            {
                txtNAVFile.Text = NAVFile.ToString("#,##0.00");
            }
            else
            {
                txtNAVFile.Text = "0";
            }
            if (txtNAVAdmin.Text != txtNAVFile.Text)
            {
                txtNAVFile.ForeColor = Color.Red;
            }
            else
            {
                txtNAVFile.ForeColor = Color.Black;
            }


            SetSection();

            if (dgDiferencesAdmin1.RowCount > 0) dgDiferencesAdmin1.FocusedRowHandle = 0;
            if (dgDiferencesAdmin1.RowCount > 0) dgDiferencesAdmin1.FocusedRowHandle = -1;

            dgDiferencesAdmin1.SortInfo.Clear();
            dgDiferencesAdmin1.Columns["DiferenceCashABS"].SortOrder = ColumnSortOrder.Descending;

            SetSection();
            return NAVFile;
        }

        void Load_Perf(double NAVFile, Boolean IsMerge)
        {

            string SQLString;
            double TotalPerfAdmin = 0;
            double TotalPerfLB = 0;

            DataTable tablep = new DataTable();
            if (IdPortfolio == 4)
            {
                cmdInsertPrice.Enabled = true;
            }
            else
            {
                cmdInsertPrice.Enabled = false;
            }

            ClearFields();


            if (IsMerge)
            {
                DateTime ReturnDate = Convert.ToDateTime(curConn.Execute_Query_String("Select MAX(Data_PL)a FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio = 4 and Data_PL<'" + TradeDate.ToString("yyyyMMdd") + "'"));

                SQLString = " SET ARITHABORT ON; SELECT Type,SUM(PlAdmin) PlAdmin ,SUM(PLLB) PLLB " +
                " FROM ( Select * FROM dbo.FCN_Check_Portfolio_Performance (" + IdPortfolio + ",'" + ReturnDate.ToString("yyyyMMdd") + "')" +
                 "UNION ALL " +
                " Select * FROM dbo.FCN_Check_Portfolio_Performance(" + IdPortfolio + ",'" + TradeDate.ToString("yyyyMMdd") + "')) A Group by Type ";
            }
            else
            {
                SQLString = "SET ARITHABORT ON; SELECT * FROM NESTDB.dbo.FCN_Check_Portfolio_Performance(" + IdPortfolio + ",'" + TradeDate.ToString("yyyyMMdd") + "')";
            }


            tablep = curConn.Return_DataTable(SQLString);

            double Diference = 0;

            foreach (DataRow curRow in tablep.Rows)
            {
                Diference = 0;
                Diference = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"].ToString()) - LiveDLL.Utils.ParseToDouble(curRow["PlLb"].ToString());

                if (curRow["Type"].ToString() == "Equities")
                {
                    txtEquities_Adm.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtEquities_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtEquities_Dif.Text = Diference.ToString("#,##0.00");

                    txtPercDif_Equities.Text = (Diference / NAVFile).ToString("p2");
                }

                if (curRow["Type"].ToString() == "Bonds")
                {
                    txtBonds_Adm.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtBonds_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtBonds_Dif.Text = Diference.ToString("#,##0.00");
                    txtPercDif_Bonds.Text = (Diference / NAVFile).ToString("p2");
                }

                if (curRow["Type"].ToString() == "Options")
                {
                    txtOptions_Adm.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtOptions_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtOptions_Dif.Text = Diference.ToString("#,##0.00");
                    txtPercDif_Options.Text = (Diference / NAVFile).ToString("p2");
                }
                if (curRow["Type"].ToString() == "Fowards")
                {
                    txtFoward_Adm.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtFoward_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtFoward_Dif.Text = Diference.ToString("#,##0.00");
                    txtPercDif_FW.Text = (Diference / NAVFile).ToString("p2");
                }
                if (curRow["Type"].ToString() == "Cash")
                {
                    txtCashOther_Adm.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtCashOther_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtCashOther_Dif.Text = Diference.ToString("#,##0.00");
                    txtPercDif_Cash.Text = (Diference / NAVFile).ToString("p2");
                }

                if (curRow["Type"].ToString() == "Futures")
                {
                    txtFutures_Admin.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtFutures_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtFutures_Dif.Text = Diference.ToString("#,##0.00");
                    txtPercDif_Futures.Text = (Diference / NAVFile).ToString("p2");
                }

                if (curRow["Type"].ToString() == "Funds")
                {
                    txtFunds_Admin.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtFunds_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtFunds_Dif.Text = Diference.ToString("#,##0.00");
                    txtPercDif_Funds.Text = (Diference / NAVFile).ToString("p2");
                }

                if (curRow["Type"].ToString() == "Dividends")
                {
                    txtDividends_Admin.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtDividends_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtDividends_Dif.Text = Diference.ToString("#,##0.00");

                    txtPercDif_Divid.Text = (Diference / NAVFile).ToString("p2");
                }

                if (curRow["Type"].ToString() == "Brokerage")
                {
                    txtBrokerage_Admin.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtBrokerage_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtBrokerage_Dif.Text = Diference.ToString("#,##0.00");

                    txtPercDif_Brokerage.Text = (Diference / NAVFile).ToString("p2");
                }

                if (curRow["Type"].ToString() == "Brokerage Futures")
                {
                    txtBrok_Fut_Admin.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtBrok_Fut_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtBrok_Fut_Diff.Text = Diference.ToString("#,##0.00");

                    txtBrok_Fut_DifPerc.Text = (Diference / NAVFile).ToString("p2");
                }
                if (curRow["Type"].ToString() == "Expense")
                {
                    txtExpense_Admin.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtExpense_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtExpense_Dif.Text = Diference.ToString("#,##0.00");

                    txtPercDif_Expense.Text = (Diference / NAVFile).ToString("p2");
                }

                if (curRow["Type"].ToString() == "StockLoan")
                {
                    txtStockLoand_Admin.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");
                    TotalPerfAdmin += LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]);

                    txtStockLoand_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");
                    TotalPerfLB += LiveDLL.Utils.ParseToDouble(curRow["PlLb"]);

                    txtStockLoand_Dif.Text = Diference.ToString("#,##0.00");

                    txtPercDif_Loan.Text = (Diference / NAVFile).ToString("p2");
                }

                if (curRow["Type"].ToString() == "FilePerf")
                {
                    if (LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]) != 0)
                        txtFileNAV_Adm.Text = LiveDLL.Utils.ParseToDouble(curRow["PlAdmin"]).ToString("#,##0.00");

                    txtFileNAV_LB.Text = LiveDLL.Utils.ParseToDouble(curRow["PlLb"]).ToString("#,##0.00");

                }

            }

            txtTotal_Adm.Text = TotalPerfAdmin.ToString("#,##0.00");
            txtTotal_LB.Text = TotalPerfLB.ToString("#,##0.00");
            txtTotal_Dif.Text = (TotalPerfAdmin - TotalPerfLB).ToString("#,##0.00");
            txtPercDif_Total.Text = ((TotalPerfAdmin - TotalPerfLB) / NAVFile).ToString("p2");

            if (txtFileNAV_Adm.Text != txtTotal_Adm.Text)
            {
                txtFileNAV_Adm.ForeColor = Color.Red;
            }
            else
            {
                txtFileNAV_Adm.ForeColor = Color.Black;
            }

            if (txtFileNAV_LB.Text != txtTotal_LB.Text)
            {
                txtFileNAV_LB.ForeColor = Color.Red;
            }
            else
            {
                txtFileNAV_LB.ForeColor = Color.Black;
            }

            tablep.Dispose();

        }

        void Load_ComparePerf()
        {
            string StringSQL = "SET ARITHABORT ON; Select * FROM  NESTDB.dbo.FCN_Check_ReturnPrevNAV(" + IdPortfolio + ",'" + TradeDate.ToString("yyyyMMdd") + "') ";

            DataTable tablep = new DataTable();

            tablep = curConn.Return_DataTable(StringSQL);

            double NavFile = 1;
            double PerfFile;


            foreach (DataRow curRow in tablep.Rows)
            {

                if (curRow["NAVType"].ToString() == "File")
                {
                    NavFile = LiveDLL.Utils.ParseToDouble(curRow["NAV"]);
                }
            }
            PerfFile = Convert.ToDouble(txtFileNAV_Adm.Text);

            txtCheckPerfCalc.Text = (PerfFile / NavFile).ToString("p4");
            Load_Nav();
        }

        void Load_Nav()
        {
            string StringSQL = "SET ARITHABORT ON; Select * FROM  NESTDB.dbo.FCN_Check_Portfolio_PL(" + IdPortfolio + ",'" + TradeDate.ToString("yyyyMMdd") + "') ";

            DataTable tablep = new DataTable();

            tablep = curConn.Return_DataTable(StringSQL);

            txtCheckPerfFile.Text = "";
            txtNavTotal.Text = "";
            txtnavUsd.Text = "";
            txtNavBrl.Text = "";
            txtNavShare.Text = "";

            foreach (DataRow curRow in tablep.Rows)
            {
                txtCheckPerfFile.Text = LiveDLL.Utils.ParseToDouble(curRow["PL"]).ToString("p4");

                txtNavTotal.Text = LiveDLL.Utils.ParseToDouble(curRow["NavTotal"]).ToString("#,##0.00;(#,##0.00)");
                txtnavUsd.Text = LiveDLL.Utils.ParseToDouble(curRow["NavUSD"]).ToString("#,##0.00;(#,##0.00)");
                txtNavBrl.Text = LiveDLL.Utils.ParseToDouble(curRow["NavBRL"]).ToString("#,##0.00;(#,##0.00)");
                txtNavShare.Text = LiveDLL.Utils.ParseToDouble(curRow["NAVShares"]).ToString("#,##0.000########;(#,##0000########)");
            }

            if (IdPortfolio == 50) txtCheckPerfCalc.Text = txtCheckPerfFile.Text;

            if (txtCheckPerfFile.Text != txtCheckPerfCalc.Text)
            {
                txtCheckPerfCalc.ForeColor = Color.Red;
            }
            else
            {
                txtCheckPerfCalc.ForeColor = Color.Black;
            }
        }

        void ClearFields()
        {
            txtEquities_Adm.Text = "0,00";
            txtEquities_LB.Text = "0,00";
            txtEquities_Dif.Text = "0,00";
            txtPercDif_Equities.Text = "0,00%";


            txtBonds_Adm.Text = "0,00"; ;
            txtBonds_LB.Text = "0,00";
            txtBonds_Dif.Text = "0,00";
            txtPercDif_Bonds.Text = "0,00%";

            txtOptions_Adm.Text = "0,00"; ;
            txtOptions_LB.Text = "0,00";
            txtOptions_Dif.Text = "0,00";
            txtPercDif_Options.Text = "0,00%";

            txtFoward_Adm.Text = "0,00"; ;
            txtFoward_LB.Text = "0,00";
            txtFoward_Dif.Text = "0,00";
            txtPercDif_FW.Text = "0,00%";

            txtFutures_Admin.Text = "0,00"; ;
            txtFutures_LB.Text = "0,00";
            txtFutures_Dif.Text = "0,00";
            txtPercDif_Futures.Text = "0,00%";

            txtDividends_Admin.Text = "0,00"; ;
            txtDividends_LB.Text = "0,00";
            txtDividends_Dif.Text = "0,00";
            txtPercDif_Divid.Text = "0,00%";


            txtBrokerage_Admin.Text = "0,00"; ;
            txtBrokerage_LB.Text = "0,00";
            txtBrokerage_Dif.Text = "0,00";
            txtPercDif_Brokerage.Text = "0,00%";

            txtExpense_Admin.Text = "0,00"; ;
            txtExpense_LB.Text = "0,00";
            txtExpense_Dif.Text = "0,00";
            txtPercDif_Expense.Text = "0,00%";


            txtStockLoand_Admin.Text = "0,00"; ;
            txtStockLoand_LB.Text = "0,00";
            txtStockLoand_Dif.Text = "0,00";
            txtPercDif_Loan.Text = "0,00%";

            txtFunds_Admin.Text = "0,00"; ;
            txtFunds_LB.Text = "0,00";
            txtFunds_Dif.Text = "0,00";
            txtPercDif_Funds.Text = "0,00%";

            txtCashOther_Adm.Text = "0,00"; ;
            txtCashOther_LB.Text = "0,00";
            txtCashOther_Dif.Text = "0,00";
            txtPercDif_Cash.Text = "0,00%";

            txtFileNAV_Adm.Text = "0,00"; ;
            txtFileNAV_LB.Text = "0,00";


        }

        private void InitializeGrid()
        {
            curUtils.SetColumnStyle(dgDiferencesAdmin1, 1);

            dgDiferencesAdmin1.Columns["TradeDateAdmin"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgDiferencesAdmin1.Columns["TradeDateAdmin"].DisplayFormat.FormatString = "dd/MM/yy";

            dgDiferencesAdmin1.Columns["TradeDateLiveBook"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgDiferencesAdmin1.Columns["TradeDateLiveBook"].DisplayFormat.FormatString = "dd/MM/yy";

            dgDiferencesAdmin1.Columns["QuantityAdmin"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["QuantityAdmin"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["PriceAdmin"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["PriceAdmin"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgDiferencesAdmin1.Columns["CashAdmin"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["CashAdmin"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["ResultAdmin"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["ResultAdmin"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["QuantityLiveBook"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["QuantityLiveBook"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["PriceLiveBook"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["PriceLiveBook"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgDiferencesAdmin1.Columns["CashLiveBook"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["CashLiveBook"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["ResultLiveBook"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["ResultLiveBook"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["DiferenceQuantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["DiferenceQuantity"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["DiferenceQuantityABS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["DiferenceQuantityABS"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["DiferencePrice"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["DiferencePrice"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["DiferenceCashABS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["DiferenceCashABS"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["DiferenceCash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["DiferenceCash"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["DiferenceResult"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["DiferenceResult"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferencesAdmin1.Columns["DiferenceResultABS"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferencesAdmin1.Columns["DiferenceResultABS"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            //-----------------------------------------------------------------

            dgDiferencesAdmin1.GroupSummary.Add(SummaryItemType.Sum, "CashAdmin", dgDiferencesAdmin1.Columns["CashAdmin"], "{0:#,#0.00}");
            dgDiferencesAdmin1.GroupSummary.Add(SummaryItemType.Sum, "ResultAdmin", dgDiferencesAdmin1.Columns["ResultAdmin"], "{0:#,#0.00}");
            dgDiferencesAdmin1.GroupSummary.Add(SummaryItemType.Sum, "CashLiveBook", dgDiferencesAdmin1.Columns["CashLiveBook"], "{0:#,#0.00}");
            dgDiferencesAdmin1.GroupSummary.Add(SummaryItemType.Sum, "ResultLiveBook", dgDiferencesAdmin1.Columns["ResultLiveBook"], "{0:#,#0.00}");
            dgDiferencesAdmin1.GroupSummary.Add(SummaryItemType.Sum, "DiferenceCash", dgDiferencesAdmin1.Columns["DiferenceCash"], "{0:#,#0.00}");
            dgDiferencesAdmin1.GroupSummary.Add(SummaryItemType.Sum, "DiferenceCashABS", dgDiferencesAdmin1.Columns["DiferenceCashABS"], "{0:#,#0.00}");
            dgDiferencesAdmin1.GroupSummary.Add(SummaryItemType.Sum, "DiferenceResult", dgDiferencesAdmin1.Columns["DiferenceResult"], "{0:#,#0.00}");
            dgDiferencesAdmin1.GroupSummary.Add(SummaryItemType.Sum, "DiferenceResultABS", dgDiferencesAdmin1.Columns["DiferenceResultABS"], "{0:#,#0.00}");

            dgDiferencesAdmin1.GroupSummary.Add(SummaryItemType.Sum, "DiferenceQuantity", dgDiferencesAdmin1.Columns["DiferenceQuantity"], "{0:#,#0.00}");
            dgDiferencesAdmin1.GroupSummary.Add(SummaryItemType.Sum, "DiferenceQuantityABS", dgDiferencesAdmin1.Columns["DiferenceQuantityABS"], "{0:#,#0.00}");

            foreach (GridColumn curColumn in dgDiferencesAdmin1.Columns)
            {
                if (curColumn.Name.Contains("Admin")) curColumn.AppearanceCell.BackColor = Color.LightSteelBlue;
            }

            dgDiferencesAdmin1.Columns["DiferenceCash"].AppearanceCell.BackColor = Color.Yellow;

            GridInitialized = true;
        }

        private void cmdDiferences_Click(object sender, EventArgs e)
        {
            cmdDiferences.Enabled = false;
            Load_Perf(Load_Comparison(), chkMerge.Checked);

            Load_ComparePerf();
            cmdDiferences.Enabled = true;

            dgDiferencesAdmin1.CollapseAllGroups();

            dgDiferencesAdmin1.SetRowExpanded(-1, true);

            /*
            Int32 ExpandCounter = dgDiferencesAdmin1.GroupCount;
            
            if (ExpandCounter > dgDiferencesAdmin1.GroupCount) { ExpandCounter = 0; };

            for (int i = -1; ; i--)
            {
                if (!dgDiferencesAdmin1.IsValidRowHandle(i)) return;
                if (dgDiferencesAdmin1.GetRowLevel(i) < ExpandCounter)
                {
                    dgDiferencesAdmin1.SetRowExpanded(i, true);
                }
            }
           */
        }

        private void LoadHTML()
        {
            if (chkViewMellon.Checked)
            {
                LB_HTML HTMLEngine = new LB_HTML();

                webMellon.Navigate("about:blank");

                string tempHTML = "";

                tempHTML = tempHTML + "<html><body>";

                if (radEquities.Checked)
                {
                    tempHTML = tempHTML + HTMLEngine.MellonCheck((int)IdPortfolio, "Tb_Mellon_Acoes", TradeDate);
                    tempHTML = tempHTML + HTMLEngine.MellonCheck((int)IdPortfolio, "Tb_Mellon_Opcoes", TradeDate);
                    tempHTML = tempHTML + HTMLEngine.MellonCheck((int)IdPortfolio, "Tb_Mellon_EmprestimoAcoes", TradeDate);
                }
                if (radForwards.Checked)
                {
                    tempHTML = tempHTML + HTMLEngine.MellonCheck((int)IdPortfolio, "Tb_Mellon_RendaFixa", TradeDate);
                }
                if (radCash.Checked)
                {
                    tempHTML = tempHTML + HTMLEngine.MellonCheck((int)IdPortfolio, "Tb_Mellon_Fundos", TradeDate);
                    tempHTML = tempHTML + HTMLEngine.MellonCheck((int)IdPortfolio, "Tb_Mellon_ContasPagar", TradeDate);
                    tempHTML = tempHTML + HTMLEngine.MellonCheck((int)IdPortfolio, "Tb_Mellon_Tesouraria", TradeDate);
                }

                tempHTML = tempHTML + "</body></html>";

                webMellon.Document.Write(tempHTML);
                webMellon.Refresh();
                splitMellon.Panel2Collapsed = false;
            }
            else
            {
                splitMellon.Panel2Collapsed = true;
                webMellon.Navigate("about:blank");
            }
        }

        private void radEquities_CheckedChanged(object sender, EventArgs e)
        {
            SetSection();
        }

        private void radForwards_CheckedChanged(object sender, EventArgs e)
        {
            SetSection();
        }

        private void radCash_CheckedChanged(object sender, EventArgs e)
        {
            SetSection();
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            SetSection();
        }

        private void SetSection()
        {
            if (GridInitialized)
            {
                if (radEquities.Checked) dgDiferencesAdmin1.Columns["MellonType"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[MellonType] = 'OPTION BRL' OR [MellonType] = 'OPTION OFF' OR [MellonType] = 'EQUITY' OR [MellonType] = 'BONDS' OR [MellonType] = 'OPTION FUT' ");
                if (radForwards.Checked) dgDiferencesAdmin1.Columns["MellonType"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[MellonType] = 'FORWARD'");
                if (radCash.Checked) dgDiferencesAdmin1.Columns["MellonType"].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Custom, null, "[MellonType] = 'EXPENSES' OR [MellonType] = 'CASH' OR [MellonType] = 'FUNDS'");
                if (radAll.Checked) dgDiferencesAdmin1.Columns["MellonType"].ClearFilter();

                dgDiferencesAdmin1.SortInfo.Clear();
                dgDiferencesAdmin1.Columns["DiferenceCashABS"].SortOrder = ColumnSortOrder.Descending;
                curUtils.SetColumnStyle(dgDiferencesAdmin1, 1);
                dgDiferencesAdmin1.CollapseAllGroups();
                LoadHTML();

                if (radCash.Checked) dgDiferencesAdmin1.Columns["Insert"].Visible = true; else dgDiferencesAdmin1.Columns["Insert"].Visible = false;
                if (radCash.Checked) dgDiferencesAdmin1.Columns["Charge Cash"].Visible = true; else dgDiferencesAdmin1.Columns["Charge Cash"].Visible = false;
            }
            dgDiferencesAdmin1.CollapseAllGroups();
            dgDiferencesAdmin1.SetRowExpanded(-1, true);

        }

        private void Default_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns((DevExpress.XtraGrid.Views.Grid.GridView)sender);
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns((DevExpress.XtraGrid.Views.Grid.GridView)sender);
        }

        private void dgDefault_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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

        private void dgDiferencesAdmin1_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (!dgDiferencesAdmin1.IsGroupRow(e.RowHandle))
            {
                string SecurityType = dgDiferencesAdmin1.GetRowCellValue(e.RowHandle, "MellonType").ToString();
                double tempDifference = (double)dgDiferencesAdmin1.GetRowCellValue(e.RowHandle, "DiferenceCash");

                if (SecurityType == "FORWARD")
                {
                    if (Math.Abs(tempDifference) < 11)
                    {
                        e.Appearance.ForeColor = Color.Gray;
                    }
                }
                else
                {
                    if (Math.Abs(tempDifference) < 0.01)
                    {
                        e.Appearance.ForeColor = Color.Gray;
                    }
                }
            }
        }

        private void lblImportFile_DragDrop(object sender, DragEventArgs e)
        {
            bool flagImport = false;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            e.Effect = DragDropEffects.None;

            foreach (string curFile in files)
            {
                if (curFile.Contains(".txt"))
                {
                    Mellon curMellonImport = new Mellon();
                    curMellonImport.ImportFileMellonPort(curFile);
                    flagImport = true;
                }

                if (curFile.Contains("PosicaoDiaria"))
                {
                    Itau curItauImport = new Itau(curFile);
                    flagImport = true;
                }

                else if (curFile.Contains(".xls"))
                {
                    BTG curBtgImport = new BTG();
                    curBtgImport.ImportFileBtg(curFile);
                    flagImport = true;
                }

                if (flagImport == false)
                {
                    System.Windows.Forms.MessageBox.Show("File:\r\n\r\n" + curFile + "\r\n\r\nnot a valid file.", "Import failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }

        private void lblImportFile_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void chkViewMellon_CheckedChanged(object sender, EventArgs e)
        {
            LoadHTML();
        }

        private void cmdEditMellon_Click(object sender, EventArgs e)
        {
            string TempMellonCode = "";
            DateTime TempTradeDate;
            if (dgDiferencesAdmin1.FocusedRowHandle > 0)
            {
                TempMellonCode = dgDiferencesAdmin1.GetRowCellValue(dgDiferencesAdmin1.FocusedRowHandle, "DetailsAdmin").ToString();
                TempTradeDate = Convert.ToDateTime(dgDiferencesAdmin1.GetRowCellValue(dgDiferencesAdmin1.FocusedRowHandle, "TradeDateAdmin").ToString());

                OpenMellonEditor(TempMellonCode, TempTradeDate);
            }
        }

        private void dgDiferencesAdmin1_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

            if (Column_Name == "Security Admin" || Column_Name == "Mellon Type")
            {
                if (dgDiferencesAdmin1.FocusedRowHandle >= 0)
                {
                    string TempMellonCode = "";
                    DateTime TempTradeDate;
                    TempMellonCode = dgDiferencesAdmin1.GetRowCellValue(dgDiferencesAdmin1.FocusedRowHandle, "DetailsAdmin").ToString();
                    TempTradeDate = Convert.ToDateTime(dgDiferencesAdmin1.GetRowCellValue(dgDiferencesAdmin1.FocusedRowHandle, "TradeDateAdmin").ToString());

                    OpenMellonEditor(TempMellonCode, TempTradeDate);
                }
            }

            if (Column_Name == "Insert")
            {
                if (dgDiferencesAdmin1.FocusedRowHandle >= 0)
                {
                    System.Windows.Forms.DialogResult userConfirmation = System.Windows.Forms.MessageBox.Show("Inserting Expense:\r\nDate:\t " + TradeDate.ToString("dd-MMM-yy") + "\r\n\r\nDo you confirm?", "Insert Expense", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

                    if (userConfirmation == System.Windows.Forms.DialogResult.OK)
                    {
                        Int64 insIdSecurity = 0;
                        double InsValue = 0;

                        InsValue = double.Parse(dgDiferencesAdmin1.GetRowCellValue(dgDiferencesAdmin1.FocusedRowHandle, "DiferenceQuantity").ToString());

                        if (Int64.TryParse(dgDiferencesAdmin1.GetRowCellValue(dgDiferencesAdmin1.FocusedRowHandle, "IdSecurity").ToString(), out insIdSecurity))
                        {
                            InsertExpense(insIdSecurity, InsValue, true, false, false);
                        }
                        else
                        {
                            MessageBox.Show("Database Id not found!");
                        }
                    }
                }
            }

            if (Column_Name == "Charge Cash")
            {
                if (dgDiferencesAdmin1.FocusedRowHandle >= 0)
                {
                    System.Windows.Forms.DialogResult userConfirmation = System.Windows.Forms.MessageBox.Show("Inserting Expense:\r\nDate:\t " + TradeDate.ToString("dd-MMM-yy") + "\r\n\r\nDo you confirm?", "Insert Expense", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

                    if (userConfirmation == System.Windows.Forms.DialogResult.OK)
                    {
                        Int64 insIdSecurity = 0;
                        double InsValue = 0;

                        InsValue = double.Parse(dgDiferencesAdmin1.GetRowCellValue(dgDiferencesAdmin1.FocusedRowHandle, "DiferenceQuantity").ToString());

                        if (Int64.TryParse(dgDiferencesAdmin1.GetRowCellValue(dgDiferencesAdmin1.FocusedRowHandle, "IdSecurity").ToString(), out insIdSecurity))
                        {
                            InsertExpense(insIdSecurity, InsValue, true, true, false);
                        }
                        else
                        {
                            MessageBox.Show("Database Id not found!");
                        }
                    }
                }
            }
        }

        private void OpenMellonEditor(string MellonCode, DateTime TradeDate)
        {
            frmMellonFwdNames curMellonFwdNames = new frmMellonFwdNames();
            curMellonFwdNames.txtMellonCode.Text = MellonCode;
            curMellonFwdNames.dtpTradeDate.Value = TradeDate;
            curMellonFwdNames.Top = this.Top + 30;
            curMellonFwdNames.Left = this.Left + 30;
            curMellonFwdNames.ShowDialog();
        }

        private void InsertExpense(Int64 IdExpense, double InsValue, bool CreditExpense, bool DebitCash, bool AddToExisting)
        {
            if (InsValue == 0)
            {
                MessageBox.Show("Amount must be different from 0!");
                return;
            }

            int IdFund = 0;
            int IdAccount = 0;
            int IdTickerCash = 0;
            string SQLString = "";
            double CashAmount = 0;
            double ExpenseAmount = 0;

            if (CreditExpense) ExpenseAmount = InsValue;
            if (DebitCash) CashAmount = -InsValue;


            IdFund = Convert.ToInt32(cmbFund.SelectedValue.ToString());

            int curCurrency = curConn.Return_Int("SELECT IdCurrency FROM NESTDB.dbo.Tb001_Securities (nolock) WHERE IdSecurity=" + IdExpense);

            switch (IdFund)
            {
                case 4:
                    if (curCurrency == 1042)
                    {
                        IdAccount = 1060;
                        IdTickerCash = 5791;
                    }
                    break;
                case 10:
                    if (curCurrency == 900)
                    {
                        IdAccount = 1073;
                        IdTickerCash = 1844;
                    }
                    break;
                case 18:
                    if (curCurrency == 900)
                    {
                        IdAccount = 1289;
                        IdTickerCash = 1844;
                    }
                    break;

                case 43:
                    if (curCurrency == 900)
                    {
                        IdAccount = 1086;
                        IdTickerCash = 1844;
                    }
                    break;
                case 38:
                    if (curCurrency == 900)
                    {
                        IdAccount = 1211;
                        IdTickerCash = 1844;
                    }
                    break;

                case 55:
                    if (curCurrency == 900)
                    {
                        IdAccount = 1520;
                        IdTickerCash = 1844;
                    }
                    break;

                case 80:
                    if (curCurrency == 900)
                    {
                        IdAccount = 1532;
                        IdTickerCash = 1844;
                    }
                    break;
            
            }

            if (IdAccount == 0)
            {
                MessageBox.Show("This porfolio cannot have expenses or revenues in that currency!");
                return;
            }

            SQLString = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                        " VALUES ( 60,'" + TradeDate.ToString("yyyyMMdd") + "','" + TradeDate.ToString("yyyyMMdd") + "'," + IdAccount + "," + IdExpense + ",5,1," + ExpenseAmount.ToString().Replace(".", "").Replace(",", ".") + "," + IdAccount + "," + IdTickerCash + ",5,1," + CashAmount.ToString().Replace(".", "").Replace(",", ".") + ") ;";

            if (SQLString != "")
            {
                int retorno = curConn.ExecuteNonQuery(SQLString, 1);
                if (retorno != 0 && retorno != 99)
                {
                    MessageBox.Show("Inserted!");
                }
                else
                {
                    MessageBox.Show("There was an error. Adjustment not inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgDiferencesAdmin1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "colInsert")
            {
                e.Appearance.ForeColor = Color.Blue;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Underline);
            }
            if (e.Column.Name == "colChargeCash")
            {
                e.Appearance.ForeColor = Color.Blue;
                e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Underline);
            }
        }

        private void cmdPartialRecalc_Click(object sender, EventArgs e)
        {
            int Id_Portfolio = IdPortfolio;
            string SQLString = "INSERT INTO Tb207_Pending(Id_Ticker,Ini_Date,Source,Status,IsTR)VALUES(" + Id_Portfolio + ",'" + TradeDate.ToString("yyyyMMdd") + "',3,-1,0)";
            curConn.ExecuteNonQuery(SQLString, 1);
        }

        private void cmdFinished_Click(object sender, EventArgs e)
        {
            // int Id_Portfolio = IdPortfolio;
            // string SQLString = "INSERT INTO Tb207_Pending(Id_Ticker,Ini_Date,Source,Status,IsTR)VALUES(" + Id_Portfolio + ",'" + TradeDate.ToString("yyyyMMdd") + "',3,0,0)";
            //curConn.ExecuteNonQuery(SQLString, 1);

            InsertNAV();
        }


        private void InsertNAV()
        {
            string SQLString = "";

            if (curUtils.IsNumeric(txtNavTotal.Text) == true)
            {
                if (IdPortfolio == 4)
                {
                    Negocios.Insere_PL(5, TradeDate, Convert.ToDecimal(txtNavBrl.Text));
                    Negocios.Insere_PL(6, TradeDate, Convert.ToDecimal(txtnavUsd.Text));
                    Negocios.Insere_PL((Int32)IdPortfolio, TradeDate, Convert.ToDecimal(txtNavTotal.Text));

                }
                if (IdPortfolio == 43)
                {
                    Negocios.Insere_PL(41, TradeDate, Convert.ToDecimal(this.txtNavBrl.Text));
                    Negocios.Insere_PL(42, TradeDate, Convert.ToDecimal(this.txtnavUsd.Text));
                    Negocios.Insere_PL((Int32)IdPortfolio, TradeDate, Convert.ToDecimal(this.txtNavTotal.Text));

                }
                if (IdPortfolio == 10)
                {


                    Negocios.Insere_PL(11, TradeDate, Convert.ToDecimal(this.txtNavBrl.Text));
                    Negocios.Insere_PL((Int32)IdPortfolio, TradeDate, Convert.ToDecimal(this.txtNavTotal.Text));
                }

                if (IdPortfolio == 38)
                {

                    Negocios.Insere_PL(39, TradeDate, Convert.ToDecimal(this.txtNavBrl.Text));
                    Negocios.Insere_PL((Int32)IdPortfolio, TradeDate, Convert.ToDecimal(this.txtNavTotal.Text));
                }

                if (IdPortfolio == 18)
                {

                    Negocios.Insere_PL(17, TradeDate, Convert.ToDecimal(this.txtNavBrl.Text));
                    Negocios.Insere_PL((Int32)IdPortfolio, TradeDate, Convert.ToDecimal(this.txtNavTotal.Text));
                }
                if (IdPortfolio == 60)
                {
                    Negocios.Insere_PL(61, TradeDate, Convert.ToDecimal(this.txtNavBrl.Text));
                    Negocios.Insere_PL((Int32)IdPortfolio, TradeDate, Convert.ToDecimal(this.txtNavTotal.Text));
                }
                if (IdPortfolio == 50)
                {
                    Negocios.Insere_PL(51, TradeDate, Convert.ToDecimal(this.txtNavBrl.Text));
                    Negocios.Insere_PL((Int32)IdPortfolio, TradeDate, Convert.ToDecimal(this.txtNavTotal.Text));
                }

                if (IdPortfolio == 55)
                {
                    Negocios.Insere_PL(56, TradeDate, Convert.ToDecimal(this.txtNavBrl.Text));
                    Negocios.Insere_PL((Int32)IdPortfolio, TradeDate, Convert.ToDecimal(this.txtNavTotal.Text));
                }

                if (IdPortfolio == 80)
                {
                    Negocios.Insere_PL(81, TradeDate, Convert.ToDecimal(this.txtNavBrl.Text));
                    Negocios.Insere_PL((Int32)IdPortfolio, TradeDate, Convert.ToDecimal(this.txtNavTotal.Text));
                }

                double IdPortfolioPend = IdPortfolio + 0.1;

                SQLString = "INSERT INTO Tb207_Pending(Id_Ticker,Ini_Date,Source,Status,IsTR)VALUES(" + IdPortfolioPend.ToString().Replace(",", ".") + ",'" + this.dtpDate.Value.ToString("yyyyMMdd") + "',3,0,0)";

                string retorno = Convert.ToString(curConn.ExecuteNonQuery(SQLString, 1));

                if (retorno == "0")
                {
                    MessageBox.Show("Error on insert NAV!");
                }
                else
                {
                    MessageBox.Show("NAV Inserted!");
                }

                string IdSecurity = curConn.Execute_Query_String(" Select Id_Ticker FROM dbo.Tb002_Portfolios WHERE Id_Portfolio =" + IdPortfolio);

                if (IdPortfolio == 4 && dtpDate.Value < new DateTime(2012, 11, 01))
                { IdSecurity = "5672"; }
                if (IdPortfolio == 4 && dtpDate.Value >= new DateTime(2012, 11, 01))
                { IdSecurity = "690870"; }

                if (Convert.ToDouble(txtNavShare.Text) != 0)
                {
                    SQLString = " EXEC NESTDB.dbo.Proc_Insert_Price " + IdSecurity + ", " + txtNavShare.Text.Replace(",", ".") + ",'" + TradeDate.ToString("yyyyMMdd") + "',1,7,0";
                    retorno = Convert.ToString(curConn.ExecuteNonQuery(SQLString, 1));
                }
                else
                {
                    retorno = "99";
                }

                if (retorno == "0")
                {
                    MessageBox.Show("Error on insert NAV per Share!");
                }
                else if (retorno == "99")
                {
                    MessageBox.Show("Error on insert NAV per Share! NAV per Share is 0.00 ");
                }
                else
                {
                    MessageBox.Show("NAV per Share Inserted!");
                }
            }
        }

        void InserPrice()
        {
            double curPrice = 0;
            string SecurityAdmin = "";
            string SecurityLB = "";
            string StringSQL = "";
            //string PrecoAdmin = "";
            DateTime PriceDate;
            string IdSecurity;
            int c = 0;

            try
            {
                while (dgDiferencesAdmin1.GetRowCellValue(c, "SecurityAdmin") != null)
                {
                    if (dgDiferencesAdmin1.GetRowCellValue(c, "SecurityAdmin").ToString() != "" && dgDiferencesAdmin1.GetRowCellValue(c, "SecurityLiveBook").ToString() != "")
                    {
                        SecurityAdmin = dgDiferencesAdmin1.GetRowCellValue(c, "SecurityAdmin").ToString();
                        SecurityLB = dgDiferencesAdmin1.GetRowCellValue(c, "SecurityLiveBook").ToString();
                        PriceDate = Convert.ToDateTime(dgDiferencesAdmin1.GetRowCellValue(c, "TradeDateAdmin").ToString());

                        IdSecurity = dgDiferencesAdmin1.GetRowCellValue(c, "IdSecurity").ToString();
                        curPrice = (double)dgDiferencesAdmin1.GetRowCellValue(c, "PriceAdmin");

                        string curSection = dgDiferencesAdmin1.GetRowCellValue(c, "MellonType").ToString();
                        if (curSection == "OPTION OFF")
                        {
                            curPrice = curPrice / 100;
                        }

                        if (curSection == "OPTION FUT")
                        {
                            curPrice = curPrice / 1000;
                        }


                        if (curPrice != 0)
                        {
                            StringSQL += " EXEC nestdb.DBO.Proc_Insert_Price " + IdSecurity + ", " + curPrice.ToString().Replace(",", ".") + ", '" + PriceDate.ToString("yyyyMMdd") + "',1,19,0 ;";
                        }
                    }

                    SecurityAdmin = "";
                    SecurityLB = "";
                    //PrecoAdmin = "";
                    IdSecurity = "";
                    c++;
                }

                try
                {
                    curConn.ExecuteNonQuery(StringSQL);
                    MessageBox.Show("Price imported");
                }
                catch (Exception)
                {
                    MessageBox.Show("Price import Failed. No prices were inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Price import Failed. No prices were inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmdInsertPrice_Click(object sender, EventArgs e)
        {
            InserPrice();
        }

        private void lblFunds_Click(object sender, EventArgs e)
        {
            if (IdPortfolio == 4 || IdPortfolio == 43)
            {
                InsertExpenses(84081, Convert.ToDouble(txtFunds_Admin.Text));
            }
            else
                InsertExpenses(83050, Convert.ToDouble(txtFunds_Admin.Text));
        }

        private void lblExpenses_Click(object sender, EventArgs e)
        {
            int IdSecurity = 0;

            if (IdPortfolio == 4)
            {
                IdSecurity = 19017;
            }

            else
            {
                IdSecurity = 18990;
            }
            InsertExpenses(IdSecurity, Convert.ToDouble(txtExpense_Admin.Text));
        }

        private void lblStockLoan_Click(object sender, EventArgs e)
        {
            int IdSecurity = 0;

            if (IdPortfolio == 4)
            {
                IdSecurity = 128277;
            }

            else
            {
                IdSecurity = 141994;
            }
            InsertExpenses(IdSecurity, Convert.ToDouble(txtStockLoand_Admin.Text));
        }

        int InsertExpenses(decimal IdSecurityExpense, double SrValue)
        {
            if (SrValue == 0)
            {
                MessageBox.Show("Amount must be different from 0!");
                return 0;
            }

            int Id_Account = 0;
            int Id_Ticker2 = 0;
            string SQLString = "";
            int TransType;

            int curCurrency = curConn.Return_Int("SELECT IdCurrency FROM NESTDB.dbo.Tb001_Securities (nolock) WHERE IdSecurity=" + IdSecurityExpense);

            switch (IdPortfolio)
            {
                case 4:
                    if (curCurrency == 900) { Id_Account = 1046; Id_Ticker2 = 1844; }
                    if (curCurrency == 1042) { Id_Account = 1060; Id_Ticker2 = 5791; }
                    break;
                case 10:
                    if (curCurrency == 900) { Id_Account = 1073; Id_Ticker2 = 1844; }
                    break;
                case 38:
                    if (curCurrency == 900) { Id_Account = 1211; Id_Ticker2 = 1844; }
                    break;
                case 18:
                    if (curCurrency == 900) { Id_Account = 1289; Id_Ticker2 = 1844; }
                    break;
                case 43:
                    if (curCurrency == 900) { Id_Account = 1086; Id_Ticker2 = 1844; }
                    if (curCurrency == 1042) { Id_Account = 1148; Id_Ticker2 = 5791; }
                    break;
                case 60:
                    if (curCurrency == 900) { Id_Account = 1375; Id_Ticker2 = 1844; }
                    break;
                case 55:
                    if (curCurrency == 900) { Id_Account = 1521; Id_Ticker2 = 1844; }
                    break;

                case 80:
                    if (curCurrency == 900) { Id_Account = 1532; Id_Ticker2 = 1844; }
                    break;

            }

            if (Id_Account == 0)
            {
                MessageBox.Show("This porfolio cannot have expenses or revenues in that currency!");
                return 0;
            }

            if (SrValue > 0)
            {
                TransType = 70;
            }
            else
            {
                TransType = 60;
            }
            SQLString = "DELETE FROM [NESTDB].[dbo].[Tb700_Transactions] WHERE Trade_Date='" + TradeDate.ToString("yyyyMMdd") + "' AND Id_Account1= " + Id_Account + " AND Id_Ticker1=" + IdSecurityExpense + " ; ";


            SQLString += " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                " VALUES ( " + TransType + ",'" + TradeDate.ToString("yyyyMMdd") + "','" + TradeDate.ToString("yyyyMMdd") + "'," + Id_Account + "," + IdSecurityExpense + ",5,1,0," + Id_Account + "," + Id_Ticker2 + ",5,1," + SrValue.ToString().Replace(".", "").Replace(",", ".") + ") ;";

            if (SQLString != "")
            {
                int retorno = curConn.ExecuteNonQuery(SQLString, 1);
                if (retorno != 0 && retorno != 99)
                {
                    MessageBox.Show("Inserted!");
                }
                else
                {
                    MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }


            return 1;
        }

        private void label12_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DialogResult userConfirmation = System.Windows.Forms.MessageBox.Show("Inserting Adjustment:\r\nDate:\t " + TradeDate.ToString("dd-MMM-yy") + "\r\n\r\nDo you confirm?", "Insert adjustment", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

            if (userConfirmation == System.Windows.Forms.DialogResult.OK)
            {
                double adjValue = 0;

                if (double.TryParse(txtFW_Diff.Text, out adjValue))
                {
                    InsertExpense(82955, -adjValue, true, false, true);
                }
            }
        }

        private void chkMerge_CheckedChanged(object sender, EventArgs e)
        {
            cmdDiferences_Click(sender, e);
        }


    }


}
