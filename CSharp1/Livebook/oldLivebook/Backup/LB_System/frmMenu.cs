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

using System.IO;


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
using System.Threading;

using NestDLL;

namespace SGN
{
    public partial class frmMenu : Form
    {
        public class myReverserClass : IComparer
        {

            // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
            int IComparer.Compare(Object x, Object y)
            {
                return ((new CaseInsensitiveComparer()).Compare(y, x));
            }

        }
        public string LBVersion;

        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();

        DataTable tablep = new DataTable();
        
        bool IsLoading=false;
        bool Checa_rotina = false;
        int updPositions=300;
        int updResume=300;
        int updBookSummary=300;
        int updTop10A=300;
        int updTop10D=300;
        int updOptionStrikes=1000;

        double PTAXClose = 0;
        double PTAXLast = 0;

        public frmMenu()
        {
            InitializeComponent();
        }


        void Inicializar()
        {
            try
            {
                if (File.Exists("c:\\Livebook2.cfg"))
                {
                    FileStream s = new FileStream("c:\\Livebook2.cfg", FileMode.Open,FileAccess.Read,FileShare.None);
                    StreamReader r = new StreamReader(s);
                    string t;
                    while ((t = r.ReadLine()) != null)
                    {
                        if (!(t.ToString().Substring(0, 1) == "["))
                        {
                            string[] tempArray = t.ToString().Split(':');
                            if (tempArray[0] == "frmPositions") { updPositions = Convert.ToInt32(Convert.ToSingle(tempArray[1].Replace('.', ',')) * 1000); };
                            if (tempArray[0] == "frmresume") { updResume = Convert.ToInt32(Convert.ToSingle(tempArray[1].Replace('.', ',')) * 1000); };
                            if (tempArray[0] == "frmSubPortfolio") { updBookSummary = Convert.ToInt32(Convert.ToSingle(tempArray[1].Replace('.', ',')) * 1000); };
                            if (tempArray[0] == "frmTop10A") { updTop10A = Convert.ToInt32(Convert.ToSingle(tempArray[1].Replace('.', ',')) * 1000); };
                            if (tempArray[0] == "frmTop10D") { updTop10D = Convert.ToInt32(Convert.ToSingle(tempArray[1].Replace('.', ',')) * 1000); };
                            if (tempArray[0] == "frmOptionStrikes") { updOptionStrikes = Convert.ToInt32(Convert.ToSingle(tempArray[1].Replace('.', ',')) * 1000); };
                        };
                    }
                    r.Close();
                }
                else
                {
                    FileStream s = new FileStream("c:\\Livebook2.cfg", FileMode.Create);
                    StreamWriter w = new StreamWriter(s, Encoding.UTF8);

                    w.WriteLine("[Update Frequencies in seconds]");
                    w.WriteLine("frmPositions: 0.3");
                    w.WriteLine("frmresume: 0.3");
                    w.WriteLine("frmSubPortfolio: 0.3");
                    w.WriteLine("frmTop10A: 0.3");
                    w.WriteLine("frmTop10D: 0.3");
                    w.WriteLine("default: 1");
                    w.Flush();
                    w.Close();

                }
            }
            catch (Exception excep)
            {

                MessageBox.Show("dd" + excep.ToString());
            }
        }

        private void frmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!GlobalVars.Instance.appCloseTimerFired)
            {
                int resposta;
                resposta = Convert.ToInt32(MessageBox.Show("Do you really want to Logoff?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                GlobalVars.Instance.appClosing = true;

                if (resposta == 6)
                {
                    Valida.Save_Properties_Form(this, 0);
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                Valida.Save_Properties_Form(this, 0);
            }
        }

        private void Valida_Menu()
        {
            if (Valida.Verifica_Acesso(1) || Valida.Verifica_Acesso(2) || Valida.Verifica_Acesso(3))
            {
                pLToolStripMenuItem.Enabled = true;
            }
            if (Valida.Verifica_Acesso(2))
            {
                inserirPLToolStripMenuItem.Enabled = true;
            }
            if (Valida.Verifica_Acesso(14))
            {
                commercialToolStripMenuItem.Enabled = true;
            }
        }

        #region OpenForms

        private void FormOpener(Form FormToOpen)
        {
            if (FormToOpen.Visible == false)
            {
                if (Valida.Load_Properties_Form(FormToOpen))
                {
                    FormToOpen.MdiParent = null;
                }
                else
                {
                    FormToOpen.MdiParent = this;
                }
            }
            FormToOpen.Icon = this.Icon;

            FormToOpen.Show();
            FormToOpen.Activate();

            if (FormToOpen.Name == "frmPerfChart")
            {
                int tempHeight = FormToOpen.Height;
                FormToOpen.ControlBox = false;
                FormToOpen.Text = string.Empty;
                FormToOpen.Height = tempHeight;
            }

            Valida.Set_Controls_Form(FormToOpen);

            if (FormToOpen.Left > this.Width - 20 && !IsLoading)
            {
                MessageBox.Show("The form you are trying to open is already opened, but is is located \n outside the visible area (too much to the right). Use the scrollbars to find it.", "Nest Livebook 2.0", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (FormToOpen.Top > this.Height - 20 && !IsLoading)
            {
                MessageBox.Show("The form you are trying to open is already opened, but is is located \n outside the visible area (too much to the bottom). Use the scrollbars to find it.", "Nest Livebook 2.0", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

            public frmPositions Tela_Pos = new frmPositions();
            public frmOrders Tela_Ordens = new frmOrders();
            public frmTradesPosition Tela_Trades_Pos = new frmTradesPosition();
            public frmTradeSummary Tela_TradeSummary = new frmTradeSummary();
            public frmInsertOrder Tela_Insert_Ordem = new frmInsertOrder();
            public frmTop10Contributions Top10Desc = new frmTop10Contributions();
            public frmBottom10Contributions Top10Asc = new frmBottom10Contributions();
            public frmTop10Changes TopChanges = new frmTop10Changes();
            public frmBottom10Changes BottonChanges = new frmBottom10Changes();
            public frmTop10Long Top10Long = new frmTop10Long();
            public frmTop10Short Top10Short = new frmTop10Short();
            public frmPortSummary PortSummary = new frmPortSummary();
            public frmSubPortfolio BookSummary = new frmSubPortfolio();
            public frmExposure_PL Tela_Exposure = new frmExposure_PL();
            public frmInsertLoan Tela_Insere_Aluguel = new frmInsertLoan();
            public frmConfirmPL PL = new frmConfirmPL();
            public frmVol Fmrvol = new frmVol();
            public frmIssuer Issuer = new frmIssuer();
            public frmTrades Trades = new frmTrades();
            private frmTrades_group Trades_Ticker = new frmTrades_group();
        
            //public frmEdit Editar = new frmEdit();
            public frmInsertPrice Preco = new frmInsertPrice();
            public frmQuote NestQuote = new frmQuote();
            public frmFundPerfSummary FundPerf = new frmFundPerfSummary();
            public frmOptionStrikes OptionStrikes = new frmOptionStrikes();
            public frmTradeSplit TradeSplit = new frmTradeSplit();
            public frmEditBookSection Edit_BookSection = new frmEditBookSection();
            public frmRiskMarginalVARs MarginalVARs = new frmRiskMarginalVARs();
            public frmMonSectorIndex SectorIdx = new frmMonSectorIndex();
            public frmVARChart VARChart = new frmVARChart();
            public frmStock_Loan Stock_Loan = new frmStock_Loan();
            public frmCompIndex CompIndex = new frmCompIndex();
            public frmDividends Dividends = new frmDividends();
            public frmRiskLimits RiskLimits = new frmRiskLimits();
            public frmPerfChart PerfChart = new frmPerfChart();
            public frmResearchNews ResearchNews = new frmResearchNews();
            public frmRiskSectors RiskSectors = new frmRiskSectors();
            public frmRiskOptions RiskOptions = new frmRiskOptions();
            public frmSubsRedemp SubsRedemp = new frmSubsRedemp();
            public frmLiquidity Liquidity = new frmLiquidity();
            public frmAttribution Attribution = new frmAttribution();
            public frmExpenses Expenses = new frmExpenses();
            public frmRiskFactors FactorExposure = new frmRiskFactors();
            public frmCashTransfer Wire = new frmCashTransfer();
            public frmRiskWeeklyReport WeeklyReport = new frmRiskWeeklyReport();
            public frmContacts Contacts = new frmContacts();
            public frmContacts_Report ContSummary = new frmContacts_Report();
            public frmDDChart DDChart = new frmDDChart();
            public frmRisk_Scenarios Scenarios = new frmRisk_Scenarios();
            public frmFowards Foward = new frmFowards();
            public frmContPayouts ContPayouts = new frmContPayouts();
            public frmMonteCarlo MonteCarlo = new frmMonteCarlo();
            public frmBookPerfSummary BookPerfSummary = new frmBookPerfSummary();
            public frmQuickChart QuickChart = new frmQuickChart();
            public frmDiferences DifReport = new frmDiferences();
            public frmRiskCharts RiskCharts = new frmRiskCharts();
            public frmEditSecurity EditSecurity = new frmEditSecurity();
            public frmImportFiles ImportFiles = new frmImportFiles();



            private void securitiesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (EditSecurity.Visible == false)
                {
                    EditSecurity = new frmEditSecurity();
                    FormOpener(EditSecurity);
                }
            }

            private void riskChartsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (RiskCharts.Visible == false)
                {
                    RiskCharts = new frmRiskCharts();
                    FormOpener(RiskCharts);
                }
            }

            private void simpleChartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (QuickChart.Visible == false)
                {
                    QuickChart = new frmQuickChart();
                    FormOpener(QuickChart);
                }
            }

            private void bookPerformanceSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (BookPerfSummary.Visible == false)
                {
                    BookPerfSummary = new frmBookPerfSummary();
                    FormOpener(BookPerfSummary);
                }
            }

            private void monteCarloToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (MonteCarlo.Visible == false)
                {
                    MonteCarlo = new frmMonteCarlo();
                    FormOpener(MonteCarlo);
                }
            }

            private void payoutSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (ContPayouts.Visible == false)
                {
                    ContPayouts = new frmContPayouts();
                    FormOpener(ContPayouts);
                }
            }

            private void scenariosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Scenarios.Visible == false)
                {
                    Scenarios = new frmRisk_Scenarios();
                    FormOpener(Scenarios);
                }
            }

            private void drawDownChartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (DDChart.Visible == false)
                {
                    DDChart = new frmDDChart();
                    FormOpener(DDChart);
                }
            }

            private void monthSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (ContSummary.Visible == false)
                {
                    ContSummary = new frmContacts_Report();
                    FormOpener(ContSummary);
                }
            }
            private void fundInvestorsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Contacts.Visible == false)
                {
                    Contacts = new frmContacts();
                    FormOpener(Contacts);
                }
            }

            private void weeklyRiskReportToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (WeeklyReport.Visible == false)
                {
                    WeeklyReport = new frmRiskWeeklyReport();
                    FormOpener(WeeklyReport);
                }
            }
            private void diferencesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (DifReport.Visible == false)
                {
                    DifReport = new frmDiferences();
                    FormOpener(DifReport);
                }

            }

            private void factorExposureToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (FactorExposure.Visible == false)
                {
                    FactorExposure = new frmRiskFactors();
                    FormOpener(FactorExposure);
                }
            }

            private void sectorExposureToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (RiskSectors.Visible == false)
                {
                    RiskSectors = new frmRiskSectors();
                    FormOpener(RiskSectors);
                }
            }

            private void optionsExposureToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (RiskOptions.Visible == false)
                {
                    RiskOptions = new frmRiskOptions();
                    FormOpener(RiskOptions);
                }
            }
            
        private void researchNewsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (ResearchNews.Visible == false)
                {
                    ResearchNews = new frmResearchNews();
                    FormOpener(ResearchNews);
                }
            }

            private void intradayPerformanceChartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (PerfChart.Visible == false)
                {
                    PerfChart = new frmPerfChart();
                    FormOpener(PerfChart);
                }
            }

            private void riskLimitsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (RiskLimits.Visible == false)
                {
                    RiskLimits = new frmRiskLimits();
                    FormOpener(RiskLimits);
                }
            }

            private void dividendsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Dividends.Visible == false)
                {
                    Dividends = new frmDividends();
                    FormOpener(Dividends);
                }
            }
            
            private void compareToIndexToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (CompIndex.Visible == false)
                {
                    CompIndex = new frmCompIndex();
                    FormOpener(CompIndex);
                }
            }
        
            private void vARChartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (VARChart.Visible == false)
                {
                    VARChart = new frmVARChart();
                    FormOpener(VARChart);
                }
            }

        private void sectorIndicesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (SectorIdx.Visible == false)
                {
                    SectorIdx = new frmMonSectorIndex();
                    FormOpener(SectorIdx);
                }
            }
            private void positionSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Top10Long.Visible == false)
                {
                    Top10Long = new frmTop10Long();
                    FormOpener(Top10Long);
                }
            }
            
            private void positionsToolStripMenuItem2_Click(object sender, EventArgs e)
            {
                if (Tela_Pos.Visible == false)
                {
                    Tela_Pos = new frmPositions();
                    Tela_Pos.Carrega_Dados += new frmPositions.ChangeData(ChangeData_Local);
                    Tela_Pos.Carrega_Portfolio += new frmPositions.ChangePortfolio(ChangeData_Portfolio);
                    Tela_Pos.Carrega_Trades_Pos += new frmPositions.ChangeData_Trades_Pos(Change_Trades_Position);
                    Tela_Pos.SetUpdateFreq(updPositions);
                }

                FormOpener(Tela_Pos);
            }

            private void marginalVARsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (MarginalVARs.Visible == false)
                {
                    MarginalVARs = new frmRiskMarginalVARs();
                }
                FormOpener(MarginalVARs);
            }

            
            private void tradeSplitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (TradeSplit.Visible == false)
                {
                    TradeSplit = new frmTradeSplit();
                    //TradeSplit.Carrega_Portfolio += new frmTradeSplit.ChangePortfolio(ChangeData_Portfolio);
                    TradeSplit.SetUpdateFreq(updPositions);
                }

                FormOpener(TradeSplit);
            }


            private void fundPerformanceSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (FundPerf.Visible == false)
                {
                    FundPerf = new frmFundPerfSummary();
                }

                FormOpener(FundPerf);
            }

            private void openOrdesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_Ordens.Visible == false)
                {
                    Tela_Ordens = new frmOrders();
                }

                FormOpener(Tela_Ordens);
            }

            private void tradesPosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_Trades_Pos.Visible == false)
                {
                    Tela_Trades_Pos = new frmTradesPosition();
                }

                FormOpener(Tela_Trades_Pos);
            }

            private void tradeSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_TradeSummary.Visible == false)
                {
                    Tela_TradeSummary = new frmTradeSummary();
                }

                FormOpener(Tela_TradeSummary);
            }


            private void quoteToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (NestQuote.Visible == false)
                {
                    NestQuote = new frmQuote();
                }

                FormOpener(NestQuote);
            }

            private void marginToolStripMenuItem_Click(object sender, EventArgs e)
            {
                frmMargin Garantia = new frmMargin();
                Valida.Load_Properties_Form(Garantia);
                FormOpener(Garantia);
            }

            private void stockLoanToolStripMenuItem_Click(object sender, EventArgs e)
            {
                frmStock_Loan Stock_Loan = new frmStock_Loan();
                Valida.Load_Properties_Form(Stock_Loan);
                FormOpener(Stock_Loan);
            }

            private void insertOrderToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_Insert_Ordem.Visible == false)
                {
                    Tela_Insert_Ordem = new frmInsertOrder();
                    FormOpener(Tela_Insert_Ordem);
                }
            }

            private void top10ContributionToolStripMenuItem_Click(object sender, EventArgs e)
            {

                if (Top10Desc.Visible == false)
                {
                    Top10Desc = new frmTop10Contributions();
                    FormOpener(Top10Desc);
                    Top10Desc.SetUpdateFreq(updTop10D);
                }

            }

            private void bottom10ContributionToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Top10Asc.Visible == false)
                {
                    Top10Asc = new frmBottom10Contributions();
                    FormOpener(Top10Asc);
                    Top10Asc.SetUpdateFreq(updTop10A);
                }
            }

            private void top10ChangesToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                if (TopChanges.Visible == false)
                {
                    TopChanges = new frmTop10Changes();
                    FormOpener(TopChanges);
                }
            }

            private void Bottom20Changes_Click(object sender, EventArgs e)
            {
                if (BottonChanges.Visible == false)
                {
                    BottonChanges = new frmBottom10Changes();
                    FormOpener(BottonChanges);
                }
            }

            private void top10ShortToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Top10Short.Visible == false)
                {
                    Top10Short = new frmTop10Short();
                    FormOpener(Top10Short);
                }
            }

            private void exposureSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (PortSummary.Visible == false)
                {
                    PortSummary = new frmPortSummary();
                    FormOpener(PortSummary);
                    PortSummary.SetUpdateFreq(updResume);
                }
            }

            private void optionStrikesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (OptionStrikes.Visible == false)
                {
                    OptionStrikes = new frmOptionStrikes();
                    FormOpener(OptionStrikes);
                    OptionStrikes.SetUpdateFreq(updOptionStrikes);
                }
            }

            private void exposurePerStrategyToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (BookSummary.Visible == false)
                {
                    BookSummary = new frmSubPortfolio();
                    FormOpener(BookSummary);
                    BookSummary.SetUpdateFreq(updBookSummary);
                }
            }

            private void exposurePLToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_Exposure.Visible == false)
                {
                    Tela_Exposure = new frmExposure_PL();
                    FormOpener(Tela_Exposure);
                }
            }

            private void insertStockLoanToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_Insere_Aluguel.Visible == false)
                {
                    Tela_Insere_Aluguel = new frmInsertLoan();
                    FormOpener(Tela_Insere_Aluguel);
                }
            }

            private void inserirPLToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    if (PL.Visible == false)
                    {
                        PL = new frmConfirmPL();

                        //PL = new frmpl();
                        //PL.Id_User = Id_User;
                        FormOpener(PL);

                    }
                }

            private void insertVolatilityToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Fmrvol.Visible == false)
                {
                    Fmrvol = new frmVol();
                    FormOpener(Fmrvol);
                }
            }

            private void issuerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Issuer.Visible == false)
                {
                    Issuer = new frmIssuer();
                    FormOpener(Issuer);
                }
            }
        /*

            private void tradesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Trades.Visible == false)
                {
                    Trades = new frmTrades();
                    Trades.Id_User = Id_User;
                    FormOpener(Trades);
                }
            }
        */

            private void priceToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Preco.Visible == false)
                {
                    Preco = new frmInsertPrice();
                    FormOpener(Preco);
                }
            }

        private void strategySubStrategyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStrategy strategy = new frmStrategy();
            strategy.MdiParent = this;
            strategy.Show();
        }


        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPass Pass = new frmPass();
            Pass.MdiParent = this;
            Pass.Show();
        }

        #endregion OpenForms

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
          this.Close();
        }

        private void dgTrades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Function not qualified!");
        }
   
        private void frmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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

        void Carrega_Grid_Open_Orders()
        {
        Tela_Ordens.Carrega_Grid();
        }

        void ChangeData_Local(int Id_Position, int Id_Ticker,string Table)
        {
            int Retorno = Check_Open_Form("frmInsertOrder");
            if (Retorno != 0)
            {
                Tela_Insert_Ordem.Preenche_Dados_Posicao(Id_Position, Table);
            }
            if (NestQuote.Visible == true)
            {
                NestQuote.Id_Ticker = Id_Ticker;
            }
            if (QuickChart.Visible)
            {
                QuickChart.Id_Ticker = Id_Ticker;
            }

        }

        void Change_Trades_Position(int Id_Position,string Table)
        {
            int Retorno = Check_Open_Form("frmTradesPosition");
            if (Retorno != 0)
            {
                Tela_Trades_Pos.lblIdPosition.Text = Id_Position.ToString();
                Tela_Trades_Pos.lblTable.Text = Table.ToString();
                Tela_Trades_Pos.Carrega_Grid();
            }
        }

        void ChangeData_Portfolio(int Id_Porfolio, DateTime Historical)
        {
            if (Id_Porfolio != 0)
            {
                switch(Id_Porfolio)
                  {
                        case 41:
                        case 42:
                            Id_Porfolio = 43;
                            break;

                        case 5:
                        case 6:
                            Id_Porfolio = 4;
                            break;

                        case 11:
                            Id_Porfolio = 10;
                            break;

                        case 46:
                            Id_Porfolio = 45;
                            break;

                        case 48:
                            Id_Porfolio = 4;
                            break;
                }
                if (Check_Open_Form("frmTop10Contributions") != 0)
                {
                    Top10Desc.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmBottom10Contributions") != 0)
                {
                    Top10Asc.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmTop10Long") != 0)
                {
                    Top10Long.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmTop10Short") != 0)
                {
                    Top10Short.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmTop10Changes") != 0)
                {
                    TopChanges.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmBottom10Changes") != 0)
                {
                    BottonChanges.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmSubPortfolio") != 0)
                {
                    BookSummary.Set_Portfolio_Values(Id_Porfolio, Historical);
                }
                if (Check_Open_Form("frmExposure_PL") != 0)
                {
                    Tela_Exposure.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmTradeSummary") != 0)
                {
                    Tela_TradeSummary.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmOptionStrikes") != 0)
                {
                    OptionStrikes.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmRiskMarginalVARs") != 0)
                {
                    MarginalVARs.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmPortSummary") != 0)
                {
                    PortSummary.Set_Historical_Falg(Historical);
                }
                if (Check_Open_Form("frmRiskOptions") != 0)
                {
                    RiskOptions.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmRiskSectors") != 0)
                {
                    RiskSectors.Set_Portfolio_Values(Id_Porfolio);
                }
                if (Check_Open_Form("frmBookPerfSummary") != 0)
                {
                    BookPerfSummary.Set_Portfolio_Values(Id_Porfolio);
                }
            }
        }

        int Check_Open_Form(string Frm_Name)
        {
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == Frm_Name)
                {
                    return 1;
                }
            }
            return 0;
        }

        void Save_Manual_Form()
        {
            foreach (Form frm in Application.OpenForms)
            {
                //portfolio = frm.Name;
                //frm.Close();
                try
                {
                    Valida.Save_Properties_Form(frm);
                    Valida.Save_Visible_Form(frm);
                }
                catch
                {
                    MessageBox.Show("Error when saving form properties. Not all form locations and visible status were saved.");
                }
            }
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {
            Inicializar();
            Carrega_Ptax();
            tmrCarrega_Ptax.Start();
        }

       public void Carrega_Form()
        {
            Valida_Menu();

            frmStatusFlags SysStatus = new frmStatusFlags();
            SysStatus.MdiParent = this;
            Valida.Load_Properties_Form(SysStatus);
            SysStatus.Show();

            string SQLString;

            SQLString = "Select A.*, B.Show_Order " +
                        "from Tb124_Caracteristicas_Formularios A " +
                        "INNER JOIN dbo.Tb123_Nome_Formularios B " +
                        "ON B.Id_Formulario=A.Id_Form " +
                        "where Id_User=" + NestDLL.NUserControl.Instance.User_Id + " and visivel = 1 " +
                        "ORDER BY IsDetached, B.Show_Order";

            IsLoading = true;

            DataTable curTable = CargaDados.curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                Cursor.Current = Cursors.WaitCursor;

                switch ((int)curRow["Id_Form"])
                {
                    case 1:
                        positionsToolStripMenuItem2_Click(this, new EventArgs());
                        break;

                    case 2:
                        openOrdesToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 3:
                        tradesToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 4:
                        insertOrderToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 5:
                        exposurePLToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 6:
                        top10ContributionToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 7:
                        bottom10ContributionToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 8:
                        exposureSummaryToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 9:
                        insertStockLoanToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 10:
                        marginToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 11:
                        positionSummaryToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 12:
                        top10ShortToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 13:
                        top10ChangesToolStripMenuItem1_Click(this, new EventArgs());
                        break;

                    case 14:
                        Bottom20Changes_Click(this, new EventArgs());
                        break;

                    case 15:
                        exposurePerStrategyToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 17:
                        stockLoanToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 21:
                        tradesPosToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 24:
                        tradeSummaryToolStripMenuItem_Click(this, new EventArgs());
                        break;

                    case 30:
                        quoteToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 32:
                        fundPerformanceSummaryToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 33:
                        optionStrikesToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 38:
                        tradeSplitToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 47:
                        sectorIndicesToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 52:
                        compareToIndexToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 56:
                        dividendsToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 57:
                        riskLimitsToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 58:
                        intradayPerformanceChartToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 59:
                        researchNewsToolStripMenuItem_Click (this, new EventArgs());
                        break;
                    case 60:
                        sectorExposureToolStripMenuItem_Click (this, new EventArgs());
                        break;
                    case 61:
                        optionsExposureToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 63:
                        subscriptionRedemptionToolStripMenuItem1_Click(this, new EventArgs());
                        break;
                    case 64:
                        liquidityToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 66:
                        expensesToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 67:
                        factorExposureToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 69:
                        wireToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 70:
                        weeklyRiskReportToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 71:
                        stocksToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 75:
                        fundInvestorsToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 76:
                        monthSummaryToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 77:
                        editStrategyToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 84:
                        bookPerformanceSummaryToolStripMenuItem_Click(this, new EventArgs());
                        break;
                    case 85:
                        simpleChartToolStripMenuItem_Click(this, new EventArgs());
                        break;
                }
                Application.DoEvents();
            }
            Cursor.Current = Cursors.Default;
            IsLoading = false;
        }

        private void deletePLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmApagaPL ApagaPL = new frmApagaPL();
            ApagaPL.MdiParent = this;
            ApagaPL.Show();
        }

        private void cancelTradeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCancel_Trades CancelaTrade = new frmCancel_Trades();
            CancelaTrade.MdiParent = this;
            Valida.Load_Properties_Form(CancelaTrade);
            CancelaTrade.Show();
        }

        private void updateDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdate_Trades Update_Date = new frmUpdate_Trades();
            Update_Date.MdiParent = this;
            Valida.Load_Properties_Form(Update_Date);
            Update_Date.Show();
        }

        private void stocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmEdit Editar = new frmEdit();
            //Editar.MdiParent = this;
            //Editar.Show();
        }

        private void editStrategyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (Edit_BookSection.Visible == false)
            {
                Edit_BookSection = new frmEditBookSection();
                Edit_BookSection.SetUpdateFreq(updPositions);
            }

            FormOpener(Edit_BookSection);

        }

        private void Trades_TickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Trades_Ticker.Visible == false)
            {
                Trades_Ticker = new frmTrades_group();
                FormOpener(Trades_Ticker);
            }

        }

        private void tradesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Trades.Visible == false)
            {
                Trades = new frmTrades();
                FormOpener(Trades);
            }

        }

        private void tmrCarrega_Ordens_Tick(object sender, EventArgs e)
        {

            if (Checa_rotina == false)
            {
                System.Threading.Thread t1;
                t1 = new System.Threading.Thread(new ThreadStart(Carrega_Ptax));
                t1.Start();
            }
            tlStValClose.Text = PTAXClose.ToString("#,##0.0000;(#,##0.0000)");
            tlStValToday.Text = PTAXLast.ToString("#,##0.0000;(#,##0.0000)");

            DateTime CloseAtTime = DateTime.Now.Date.Add(new TimeSpan(23, 50, 00));
            string tempString = Properties.Settings.Default.LastAutoClose;
            DateTime LastAutoClose = DateTime.Parse(tempString);

            if (DateTime.Now > CloseAtTime && LastAutoClose.Date != DateTime.Now.Date)
            {
                GlobalVars.Instance.appClosing = true;
                GlobalVars.Instance.appCloseTimerFired = true;
                Properties.Settings.Default.LastAutoClose = DateTime.Now.Date.ToString();
                Properties.Settings.Default.Save();
                this.Close();
            }
        }

        void Carrega_Ptax()
        {
            Checa_rotina = true;

            string SQLStringClose;
            string SQLStringLast;

            string SQLString = "SELECT convert(varchar,dbo.FCN_NDATEADD('du',-1,'" + DateTime.Now.ToString("yyyyMMdd") + "',2,1),112)";
            string Date_PtaxClose = CargaDados.curConn.Execute_Query_String(SQLString);

            SQLStringClose = "SELECT dbo.[FCN_Convert_Moedas](1042,900,'" + Date_PtaxClose + "')";
            SQLStringLast = "SELECT dbo.[FCN_Convert_Moedas](1042,900,'" + DateTime.Now.ToString("yyyyMMdd") + "')";

            try 
        	{	        
                PTAXClose = Double.Parse(CargaDados.curConn.Execute_Query_String(SQLStringClose));
                PTAXLast = Double.Parse(CargaDados.curConn.Execute_Query_String(SQLStringLast));
	        }
	        catch (Exception)
	        {
        		
	        }
            Checa_rotina = false;

        }

        private void contentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSpam Spam = new frmSpam();
            Spam.Show();
        }

        private void splitPercentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSplitPercent Split = new frmSplitPercent();
            Split.Show();
        }

        private void restartReutersFeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String SQLString = "INSERT INTO Tb207_Pending(Id_Ticker,Ini_Date,Source,Status,IsTR)VALUES(1,'19000101',4,1,0)";

          int Retorno = CargaDados.curConn.ExecuteNonQuery(SQLString);

          if (Retorno == 0)
          {
              MessageBox.Show("Error on Restart!");
          }
          else
          {
              MessageBox.Show("Wait a few minutes for Restart Reuters Feed!");
          }

        }

        private void savePropertiesFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Save_Manual_Form();
            MessageBox.Show("Saved!");
        }

        private void liquidityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Liquidity.Visible == false)
            {
                Liquidity = new frmLiquidity();
                FormOpener(Liquidity);
            }

        }

        private void attributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Attribution.Visible == false)
            {
                Attribution = new frmAttribution();
                FormOpener(Attribution);
            }
            
        }

        private void subscriptionRedemptionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SubsRedemp.Visible == false)
            {
                SubsRedemp = new frmSubsRedemp();
                FormOpener(SubsRedemp);
            }

        }

        private void expensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Expenses.Visible == false)
            {
                Expenses = new frmExpenses();
                FormOpener(Expenses);
            }

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nest Livebook \nVersion " + LBVersion + "\n");
        }

        private void wireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Wire.Visible == false)
            {
                Wire = new frmCashTransfer();
                FormOpener(Wire);
            }

        }

        private void earlyCloseFowardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Foward.Visible == false)
            {
                Foward = new frmFowards();
                FormOpener(Foward);
            }
        }

        private void detachWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild != null)
            {
                this.ActiveMdiChild.Top = this.ActiveMdiChild.Top + this.Top + 55;
                this.ActiveMdiChild.Left = this.ActiveMdiChild.Left + this.Left + 5;
                this.ActiveMdiChild.MdiParent = null;
            }
        }

        private void attachAllWindowsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FormCollection OpenForms = Application.OpenForms;

            for (int i = OpenForms.Count - 1; i >= 0; i--)
            {
                if (OpenForms[i].MdiParent == null && OpenForms[i] != this)
                {
                    OpenForms[i].MdiParent = this;
                    OpenForms[i].Top = this.ActiveMdiChild.Top - this.Top - 55;
                    OpenForms[i].Left = this.ActiveMdiChild.Left - this.Left - 5;
                }
            }
        }

        private void importFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ImportFiles.Visible == false)
            {
                ImportFiles = new frmImportFiles();
                FormOpener(ImportFiles);
            }
        }


    }
}