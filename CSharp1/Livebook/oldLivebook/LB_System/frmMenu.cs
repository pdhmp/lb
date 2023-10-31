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
using LiveBook;

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

namespace LiveBook
{
    public partial class frmMenu : LBForm
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

        DataTable tablep = new DataTable();
        
        bool IsLoading = false;
        bool Checa_rotina = false;
        int updPositions=300;
        int updResume=300;
        int updSubPortSummary = 300;
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
                            if (tempArray[0] == "frmSubPortfolio") { updSubPortSummary = Convert.ToInt32(Convert.ToSingle(tempArray[1].Replace('.', ',')) * 1000); };
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

                if (resposta != 6)
                {
                    e.Cancel = true;
                }

                GlobalVars.Instance.appClosing = true;
            }
        }

        private void Valida_Menu()
        {
            if (curUtils.Verifica_Acesso(1) || curUtils.Verifica_Acesso(2) || curUtils.Verifica_Acesso(3))
            {
                pLToolStripMenuItem.Enabled = true;
            }
            if (curUtils.Verifica_Acesso(2))
            {
                inserirPLToolStripMenuItem.Enabled = true;
            }
            if (curUtils.Verifica_Acesso(14))
            {
                commercialToolStripMenuItem.Enabled = true;
            }
        }

        #region OpenForms

        private void FormOpener(Form FormToOpen)
        {
            FormToOpen.Icon = this.Icon;
            FormToOpen.MdiParent = this;

            FormToOpen.Show();

            if (FormToOpen.GetType().BaseType == typeof(LBForm))
            {
                if (((LBForm)FormToOpen).openDetached)
                {
                    FormToOpen.MdiParent = null;
                }
            }

            FormToOpen.Activate();

            curUtils.Set_Controls_Form(FormToOpen);

            if (FormToOpen.Name == "frmPerfChart")
            {
                int tempHeight = FormToOpen.Height;
                FormToOpen.ControlBox = false;
                FormToOpen.Text = string.Empty;
                FormToOpen.Height = tempHeight;
            }
            /*
            if (FormToOpen.Left > this.Width - 20 && !IsLoading)
            {
                MessageBox.Show("The form you are trying to open is already opened, but is is located \n outside the visible area (too much to the right). Use the scrollbars to find it.", "Nest Livebook 2.0", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (FormToOpen.Top > this.Height - 20 && !IsLoading)
            {
                MessageBox.Show("The form you are trying to open is already opened, but is is located \n outside the visible area (too much to the bottom). Use the scrollbars to find it.", "Nest Livebook 2.0", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            */
        }

            public frmPositions Tela_Pos;
            public frmOrders Tela_Ordens;
            public frmTradesPosition Tela_Trades_Pos;
            public frmTradeSummary Tela_TradeSummary;
            public frmInsertOrder Tela_Insert_Ordem;
            public frmTop10Contributions Top10Desc;
            public frmBottom10Contributions Top10Asc;
            public frmTop10Changes TopChanges;
            public frmBottom10Changes BottonChanges;
            public frmPositionSummary Top10Long;
            public frmTop10Short Top10Short;
            public frmPortSummary PortSummary;
            public frmSubPortfolio SubPortfolioSummary;
            public frmExposure_PL Tela_Exposure;
            public frmInsertLoan Tela_Insere_Aluguel;
            public frmConfirmPL PL;
            public frmVol Fmrvol;
            public frmIssuer Issuer;
            public frmTrades Trades;
            private frmTrades_group Trades_Ticker;
        
            //public frmEdit Editar = new frmEdit();
            public frmInsertPrice Preco;
            public frmQuote NestQuote;
            public frmFundPerfSummary FundPerf;
            public frmOptionStrikes OptionStrikes;
            public frmTradeSplit TradeSplit;
            //public _TradeSplit _TradeSplit;
            public frmEditBookSection Edit_BookSection;
            public frmRiskMarginalVARs MarginalVARs;
            public frmMonSectorIndex SectorIdx;
            public frmVARChart VARChart;
            public frmStock_Loan Stock_Loan;
            public frmCompIndex CompIndex;
            public frmDividends Dividends;
            public frmRiskLimits RiskLimits;
            public frmPerfChart PerfChart;
            public frmResearchNews ResearchNews;
            public frmRiskSectors RiskSectors;
            public frmRiskOptions RiskOptions;
            public frmSubsRedemp SubsRedemp;
            public frmLiquidity Liquidity;
            public frmAttribution Attribution;
            public frmExpenses Expenses;
            public frmRiskFactors FactorExposure;
            public frmCashTransfer Wire;
            public frmRiskWeeklyReport WeeklyReport;
            public frmContacts Contacts;
            public frmContacts_Report ContSummary;
            public frmDDChart DDChart;
            public frmRisk_Scenarios Scenarios;
            public frmFowards Foward;
            public frmContPayouts ContPayouts;
            public frmMonteCarlo MonteCarlo;
            public frmBookPerfSummary BookPerfSummary;
            public frmQuickChart QuickChart;
            public frmDiferences DifReport;
            public frmRiskCharts RiskCharts;
            public frmEditSecurity EditSecurity;
            public frmImportFiles ImportFiles;
            public frmCheckImportedFile DiferencesBroker;
            public frmCheckPortfolioAdministrator DiferencesPortfolio;
            public frmStrategyTransfer StrategyTransfer;
            public frmBrokers EditBrokers;
            public TempfrmStockLoan TempStockLoan;
            public frmExposureBook BookSummary;
            public frmViewCalcHist ViewCalcHist;
            public frmRiskArb RiskArb;
            public frmEditLimitsArb RiskEditArb;
            public frmStatisticsPortfolio EstastitcPortfolio;
            public frmExposureGraph ExposureGraph;
            public frmSectorContrib SectorContrib;
            public frmRebates Rebates;
            public frmRebateClients RebateClients;
            public frmImportDividends ImpDividends;

            public frmOptionsGraph OptionGraph;
            public frmPrepareXML PrepareXML;
            public frmBrokerage Brokerage;

            public frmClientTransactions ClientTransactions;
            public frmOptionExercise OptionExercise;
            public frmSubscrReceipt ReceiptWarrant;
            public frmCheckBrokersTradesFut  FutureReconciliation;
            public frmStatusTi StatusTi;
            public frmStatusOps StatusOps;
            public frmOrderReview OrderReview;

            public frmAccounts Accounts;


            private void orderReview2ToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (OrderReview == null || OrderReview.IsDisposed)
                {
                    OrderReview = new frmOrderReview();
                    FormOpener(OrderReview);
                }
            }


            private void exerciseOptionsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (OptionExercise == null || OptionExercise.IsDisposed)
                {
                    OptionExercise = new frmOptionExercise();
                    FormOpener(OptionExercise);
                }
            }

            private void ClientTransactionstoolStripMenuItem1_Click(object sender, EventArgs e)
            {
                if (ClientTransactions == null || ClientTransactions.IsDisposed)
                {
                    ClientTransactions = new frmClientTransactions();
                    FormOpener(ClientTransactions);
                }
            }

            private void optionPayoffChartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (OptionGraph == null || OptionGraph.IsDisposed)
                {
                    OptionGraph = new frmOptionsGraph();
                    FormOpener(OptionGraph);
                }
            }

            private void SectorContribToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (SectorContrib == null || SectorContrib.IsDisposed)
                {
                    SectorContrib = new frmSectorContrib();
                    FormOpener(SectorContrib);
                }
            }

            private void ExposureGraphMenuItem_Click(object sender, EventArgs e)
            {
                if (ExposureGraph == null || ExposureGraph.IsDisposed)
                {
                    ExposureGraph = new frmExposureGraph();
                    FormOpener(ExposureGraph);
                }
            }

            private void securitiesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (EditSecurity == null || EditSecurity.IsDisposed)
                {
                    EditSecurity = new frmEditSecurity();
                    FormOpener(EditSecurity);
                }
            }

            private void riskChartsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (RiskCharts == null || RiskCharts.IsDisposed)
                {
                    RiskCharts = new frmRiskCharts();
                    FormOpener(RiskCharts);
                }
            }

            private void simpleChartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (QuickChart == null || QuickChart.IsDisposed)
                {
                    QuickChart = new frmQuickChart();
                    FormOpener(QuickChart);
                }
            }

            private void bookPerformanceSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (BookPerfSummary == null || BookPerfSummary.IsDisposed)
                {
                    BookPerfSummary = new frmBookPerfSummary();
                    FormOpener(BookPerfSummary);
                }
            }

            private void monteCarloToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (MonteCarlo == null || MonteCarlo.IsDisposed)
                {
                    MonteCarlo = new frmMonteCarlo();
                    FormOpener(MonteCarlo);
                }
            }

            private void payoutSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (ContPayouts == null || ContPayouts.IsDisposed)
                {
                    ContPayouts = new frmContPayouts();
                    FormOpener(ContPayouts);
                }
            }

            private void scenariosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Scenarios == null || Scenarios.IsDisposed)
                {
                    Scenarios = new frmRisk_Scenarios();
                    FormOpener(Scenarios);
                }
            }

            private void drawDownChartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (DDChart == null || DDChart.IsDisposed)
                {
                    DDChart = new frmDDChart();
                    FormOpener(DDChart);
                }
            }

            private void monthSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (ContSummary == null || ContSummary.IsDisposed)
                {
                    ContSummary = new frmContacts_Report();
                    FormOpener(ContSummary);
                }
            }
            private void fundInvestorsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Contacts == null || Contacts.IsDisposed)
                {
                    Contacts = new frmContacts();
                    FormOpener(Contacts);
                }
            }

            private void weeklyRiskReportToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (WeeklyReport == null || WeeklyReport.IsDisposed)
                {
                    WeeklyReport = new frmRiskWeeklyReport();
                    FormOpener(WeeklyReport);
                }
            }
            private void diferencesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (DifReport == null || DifReport.IsDisposed)
                {
                    DifReport = new frmDiferences();
                    FormOpener(DifReport);
                }

            }

            private void factorExposureToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (FactorExposure == null || FactorExposure.IsDisposed)
                {
                    FactorExposure = new frmRiskFactors();
                    FormOpener(FactorExposure);
                }
            }

            private void sectorExposureToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (RiskSectors == null || RiskSectors.IsDisposed)
                {
                    RiskSectors = new frmRiskSectors();
                    FormOpener(RiskSectors);
                }
            }

            private void optionsExposureToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (RiskOptions == null || RiskOptions.IsDisposed)
                {
                    RiskOptions = new frmRiskOptions();
                    FormOpener(RiskOptions);
                }
            }
            
        private void researchNewsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (ResearchNews == null || ResearchNews.IsDisposed)
                {
                    ResearchNews = new frmResearchNews();
                    FormOpener(ResearchNews);
                }
            }

            private void intradayPerformanceChartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (PerfChart == null || PerfChart.IsDisposed)
                {
                    PerfChart = new frmPerfChart();
                    FormOpener(PerfChart);
                }
            }

            private void riskLimitsToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (RiskLimits == null || RiskLimits.IsDisposed)
                {
                    RiskLimits = new frmRiskLimits();
                    FormOpener(RiskLimits);
                }
            }

           private void compareToIndexToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (CompIndex == null || CompIndex.IsDisposed)
                {
                    CompIndex = new frmCompIndex();
                    FormOpener(CompIndex);
                }
            }
        
            private void vARChartToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (VARChart == null || VARChart.IsDisposed)
                {
                    VARChart = new frmVARChart();
                    FormOpener(VARChart);
                }
            }

        private void sectorIndicesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (SectorIdx == null || SectorIdx.IsDisposed)
                {
                    SectorIdx = new frmMonSectorIndex();
                    FormOpener(SectorIdx);
                }
            }
            private void positionSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Top10Long == null || Top10Long.IsDisposed)
                {
                    Top10Long = new frmPositionSummary();
                    FormOpener(Top10Long);
                }
            }
            
            private void positionsToolStripMenuItem2_Click(object sender, EventArgs e)
            {
                if (Tela_Pos == null || Tela_Pos.IsDisposed)
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
                if (MarginalVARs == null || MarginalVARs.IsDisposed)
                {
                    MarginalVARs = new frmRiskMarginalVARs();
                }
                FormOpener(MarginalVARs);
            }

            
            private void tradeSplitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (TradeSplit == null || TradeSplit.IsDisposed)
                {
                    TradeSplit = new frmTradeSplit();
                    //TradeSplit.Carrega_Portfolio += new frmTradeSplit.ChangePortfolio(ChangeData_Portfolio);
                    //TradeSplit.SetUpdateFreq(updPositions);
                }

                FormOpener(TradeSplit);
            }

            // Menu inserido em 24/09/2012
            private void _tradeSplitToolStripMenuItem_Click(object sender, EventArgs e)
            {
                //if (_TradeSplit == null || _TradeSplit.IsDisposed)
                //{
                //    TradeSplit = new frmTradeSplit();
                //    //TradeSplit.Carrega_Portfolio += new frmTradeSplit.ChangePortfolio(ChangeData_Portfolio);
                //    //TradeSplit.SetUpdateFreq(updPositions);
                //}

                //FormOpener(_TradeSplit);
            }


            private void fundPerformanceSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (FundPerf == null || FundPerf.IsDisposed)
                {
                    FundPerf = new frmFundPerfSummary();
                }

                FormOpener(FundPerf);
            }

            private void openOrdesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_Ordens == null || Tela_Ordens.IsDisposed)
                {
                    Tela_Ordens = new frmOrders();
                }

                FormOpener(Tela_Ordens);
            }

            private void tradesPosToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_Trades_Pos == null || Tela_Trades_Pos.IsDisposed)
                {
                    Tela_Trades_Pos = new frmTradesPosition();
                }

                FormOpener(Tela_Trades_Pos);
            }

            private void tradeSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_TradeSummary == null || Tela_TradeSummary.IsDisposed)
                {
                    Tela_TradeSummary = new frmTradeSummary();
                }

                FormOpener(Tela_TradeSummary);
            }


            private void quoteToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (NestQuote == null || NestQuote.IsDisposed)
                {
                    NestQuote = new frmQuote();
                }

                FormOpener(NestQuote);
            }

            private void marginToolStripMenuItem_Click(object sender, EventArgs e)
            {
                frmMargin Garantia = new frmMargin();
                FormOpener(Garantia);
            }

            private void stockLoanToolStripMenuItem_Click(object sender, EventArgs e)
            {
                frmStock_Loan Stock_Loan = new frmStock_Loan();
                FormOpener(Stock_Loan);
            }

            private void insertOrderToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_Insert_Ordem == null || Tela_Insert_Ordem.IsDisposed)
                {
                    Tela_Insert_Ordem = new frmInsertOrder();
                    FormOpener(Tela_Insert_Ordem);
                }
            }

            private void top10ContributionToolStripMenuItem_Click(object sender, EventArgs e)
            {

                if (Top10Desc == null || Top10Desc.IsDisposed)
                {
                    Top10Desc = new frmTop10Contributions();
                    FormOpener(Top10Desc);
                    Top10Desc.SetUpdateFreq(updTop10D);
                }

            }

            private void bottom10ContributionToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Top10Asc == null || Top10Asc.IsDisposed)
                {
                    Top10Asc = new frmBottom10Contributions();
                    FormOpener(Top10Asc);
                    Top10Asc.SetUpdateFreq(updTop10A);
                }
            }

            private void top10ChangesToolStripMenuItem1_Click(object sender, EventArgs e)
            {
                if (TopChanges == null || TopChanges.IsDisposed)
                {
                    TopChanges = new frmTop10Changes();
                    FormOpener(TopChanges);
                }
            }

            private void Bottom20Changes_Click(object sender, EventArgs e)
            {
                if (BottonChanges == null || BottonChanges.IsDisposed)
                {
                    BottonChanges = new frmBottom10Changes();
                    FormOpener(BottonChanges);
                }
            }

            private void top10ShortToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Top10Short == null || Top10Short.IsDisposed)
                {
                    Top10Short = new frmTop10Short();
                    FormOpener(Top10Short);
                }
            }

            private void exposureSummaryToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (PortSummary == null || PortSummary.IsDisposed)
                {
                    PortSummary = new frmPortSummary();
                    FormOpener(PortSummary);
                    PortSummary.SetUpdateFreq(updResume);
                }
            }

            private void optionStrikesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (OptionStrikes == null || OptionStrikes.IsDisposed)
                {
                    OptionStrikes = new frmOptionStrikes();
                    FormOpener(OptionStrikes);
                    OptionStrikes.SetUpdateFreq(updOptionStrikes);
                }
            }

            private void exposurePerStrategyToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (SubPortfolioSummary == null || SubPortfolioSummary.IsDisposed)
                {
                    SubPortfolioSummary = new frmSubPortfolio();
                    FormOpener(SubPortfolioSummary);
                    SubPortfolioSummary.SetUpdateFreq(updSubPortSummary);
                }
            }

            private void exposurePLToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_Exposure == null || Tela_Exposure.IsDisposed)
                {
                    Tela_Exposure = new frmExposure_PL();
                    FormOpener(Tela_Exposure);
                }
            }

            private void insertStockLoanToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Tela_Insere_Aluguel == null || Tela_Insere_Aluguel.IsDisposed)
                {
                    Tela_Insere_Aluguel = new frmInsertLoan();
                    FormOpener(Tela_Insere_Aluguel);
                }
            }

            private void inserirPLToolStripMenuItem_Click(object sender, EventArgs e)
                {
                    if (PL == null || PL.IsDisposed)
                    {
                        PL = new frmConfirmPL();

                        //PL = new frmpl();
                        //PL.Id_User = Id_User;
                        FormOpener(PL);

                    }
                }

            private void insertVolatilityToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Fmrvol == null || Fmrvol.IsDisposed)
                {
                    Fmrvol = new frmVol();
                    FormOpener(Fmrvol);
                }
            }

            private void issuerToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Issuer == null || Issuer.IsDisposed)
                {
                    Issuer = new frmIssuer();
                    FormOpener(Issuer);
                }
            }
        /*

            private void tradesToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Trades == null || .IsDisposed)
                {
                    Trades = new frmTrades();
                    Trades.Id_User = Id_User;
                    FormOpener(Trades);
                }
            }
        */

            private void priceToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (Preco == null || Preco.IsDisposed)
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
            Thread.Sleep(5000);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
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
        //Tela_Ordens.Carrega_Grid();
        }

        void ChangeData_Local(int Id_Position, int Id_Ticker,string Table)
        {
            int Retorno = Check_Open_Form("frmInsertOrder");
            if (Retorno != 0)
            {
                Tela_Insert_Ordem.Preenche_Dados_Posicao(Id_Position, Table);
            }

            int Retorno1 = Check_Open_Form("frmStrategyTransfer");
            if (Retorno1 != 0)
            {
                StrategyTransfer.Preenche_Dados_Posicao(Id_Position, Table);
            }

            if (NestQuote != null)
            {
                NestQuote.Id_Ticker = Id_Ticker;
            }
            if (QuickChart != null)
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

                        case 39:
                            Id_Porfolio = 38;
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
                    SubPortfolioSummary.Set_Portfolio_Values(Id_Porfolio, Historical);
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
        /*
        void Save_Manual_Form()
        {
            foreach (Form frm in Application.OpenForms)
            {
                try
                {
                    curUtils.Save_Properties_Form(frm);
                    curUtils.Save_Visible_Form(frm);
                }
                catch
                {
                    MessageBox.Show("Error when saving form properties. Not all form locations and visible status were saved.");
                }
            }
        }
        */
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
            SysStatus.Show();

            string SQLString;

            SQLString = "SELECT A.*, B.* " +
                        "FROM UI_DEFS.dbo.Tb201_Form_Properties A (nolock) " +
                        "INNER JOIN UI_DEFS.dbo.Tb201_Forms B (nolock) " +
                        "ON B.Id_Form=A.Id_Form " +
                        "WHERE Id_User=" + NestDLL.NUserControl.Instance.User_Id + " and Visible = 1 AND IdApplication=1" +
                        "ORDER BY IsDetached, B.Show_Order";

            IsLoading = true;
            using (newNestConn curConn = new newNestConn())
            {
                DataTable curTable = curConn.Return_DataTable(SQLString);
                foreach (DataRow curRow in curTable.Rows)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    switch (curRow["Form_Name"].ToString())
                    {
                        case "frmPositions": positionsToolStripMenuItem2_Click(this, new EventArgs()); break;
                        case "frmOrders": openOrdesToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmTrades": tradesToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmInsertOrder": insertOrderToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmExposure_PL": exposurePLToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmBottom10Contributions": top10ContributionToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmTop10Contributions": bottom10ContributionToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmPortSummary": exposureSummaryToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmInsereGarantia": insertStockLoanToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmGarantia": marginToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmTop10Long": positionSummaryToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmTop10Short": top10ShortToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmTop10Changes": top10ChangesToolStripMenuItem1_Click(this, new EventArgs()); break;
                        case "frmBottom10Changes": Bottom20Changes_Click(this, new EventArgs()); break;
                        case "frmSubPortfolio": exposurePerStrategyToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmStock_Loan": stockLoanToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmTradesPosition": tradesPosToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmTradeSummary": tradeSummaryToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmQuote": quoteToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmFundPerfSummary": fundPerformanceSummaryToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmOptionStrikes": optionStrikesToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmTradeSplit": tradeSplitToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmMonSectorIndex": sectorIndicesToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmCompIndex": compareToIndexToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmRiskLimits": riskLimitsToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmPerfChart": intradayPerformanceChartToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmResearchNews": researchNewsToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmRiskSectors": sectorExposureToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmRiskOptions": optionsExposureToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmSubsRedemp": subscriptionRedemptionToolStripMenuItem1_Click(this, new EventArgs()); break;
                        case "frmLiquidity": liquidityToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmExpenses": expensesToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmRiskFactors": factorExposureToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmCashTransfer": wireToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmRiskWeeklyReport": weeklyRiskReportToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmEdit": stocksToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmContacts": fundInvestorsToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmContacts_Report": monthSummaryToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmEditBookSection": editStrategyToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmBookPerfSummary": bookPerformanceSummaryToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmQuickChart": simpleChartToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmExposureBook": exposureBookToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmRiskArb": riskArbToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmExposureGraph": ExposureGraphMenuItem_Click(this, new EventArgs()); break;
                        case "frmOrderReview": orderReview2ToolStripMenuItem_Click(this, new EventArgs()); break;
                        case "frmOptionsGraph": optionPayoffChartToolStripMenuItem_Click(this, new EventArgs()); break;
                    }
                    Application.DoEvents();
                }
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
            CancelaTrade.Show();
        }

        private void updateDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdate_Trades Update_Date = new frmUpdate_Trades();
            Update_Date.MdiParent = this;
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
            
            if (Edit_BookSection == null || Edit_BookSection.IsDisposed)
            {
                Edit_BookSection = new frmEditBookSection();
                Edit_BookSection.SetUpdateFreq(updPositions);
            }

            FormOpener(Edit_BookSection);

        }

        private void Trades_TickerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Trades_Ticker == null || Trades_Ticker.IsDisposed)
            {
                Trades_Ticker = new frmTrades_group();
                FormOpener(Trades_Ticker);
            }

        }

        private void tradesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Trades == null || Trades.IsDisposed)
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
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT convert(varchar,dbo.FCN_NDATEADD('du',-1,'" + DateTime.Now.ToString("yyyyMMdd") + "',2,1),112)";
                string Date_PtaxClose = curConn.Execute_Query_String(SQLString);

                SQLStringClose = "SELECT dbo.[FCN_Convert_Moedas](1042,900,'" + Date_PtaxClose + "')";
                SQLStringLast = "SELECT dbo.[FCN_Convert_Moedas](1042,900,'" + DateTime.Now.ToString("yyyyMMdd") + "')";

                try
                {
                    PTAXClose = Double.Parse(curConn.Execute_Query_String(SQLStringClose));
                    PTAXLast = Double.Parse(curConn.Execute_Query_String(SQLStringLast));
                }
                catch (Exception)
                {

                }
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
            using (newNestConn curConn = new newNestConn())
            {
                String SQLString = "INSERT INTO Tb207_Pending(Id_Ticker,Ini_Date,Source,Status,IsTR)VALUES(1,'19000101',4,1,0)";

                int Retorno = curConn.ExecuteNonQuery(SQLString);

                if (Retorno == 0)
                {
                    MessageBox.Show("Error on Restart!");
                }
                else
                {
                    MessageBox.Show("Wait a few minutes for Restart Reuters Feed!");
                }
            }
        }

        private void liquidityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Liquidity == null || Liquidity.IsDisposed)
            {
                Liquidity = new frmLiquidity();
                FormOpener(Liquidity);
            }
        }

        private void attributionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Attribution == null || Attribution.IsDisposed)
            {
                Attribution = new frmAttribution();
                FormOpener(Attribution);
            }
        }

        private void subscriptionRedemptionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (SubsRedemp == null || SubsRedemp.IsDisposed)
            {
                SubsRedemp = new frmSubsRedemp();
                FormOpener(SubsRedemp);
            }
        }

        private void expensesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Expenses == null || Expenses.IsDisposed)
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
            if (Wire == null || Wire.IsDisposed)
            {
                Wire = new frmCashTransfer();
                FormOpener(Wire);
            }

        }

        private void earlyCloseFowardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Foward == null || Foward.IsDisposed)
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
            if (ImportFiles == null || ImportFiles.IsDisposed)
            {
                ImportFiles = new frmImportFiles();
                FormOpener(ImportFiles);
            }
        }

        private void TradeReconciliationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DiferencesBroker == null || DiferencesBroker.IsDisposed)
            {
                DiferencesBroker = new frmCheckImportedFile();
                FormOpener(DiferencesBroker);
            }
        }

        private void checkPortfolioAdministratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DiferencesPortfolio == null || DiferencesPortfolio.IsDisposed)
            {
                DiferencesPortfolio = new frmCheckPortfolioAdministrator ();
                FormOpener(DiferencesPortfolio);
            }
        }

        private void transferStrategyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (StrategyTransfer == null || StrategyTransfer.IsDisposed)
            {
                StrategyTransfer = new frmStrategyTransfer();
                FormOpener(StrategyTransfer);
            }
        }

        private void brokersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EditBrokers == null || EditBrokers.IsDisposed)
            {
                EditBrokers = new frmBrokers();
                FormOpener(EditBrokers);
            }
        }

        private void tempStockLoandToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (TempStockLoan == null || TempStockLoan.IsDisposed)
            {
                TempStockLoan = new TempfrmStockLoan();
                FormOpener(TempStockLoan);
            }
 
        }

        private void exposureBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (BookSummary == null || BookSummary.IsDisposed)
            {
                BookSummary = new frmExposureBook();
            }
            FormOpener(BookSummary);
        }

  
        private void viewHistoricalCalculateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ViewCalcHist == null || ViewCalcHist.IsDisposed)
            {
                ViewCalcHist = new frmViewCalcHist();
                FormOpener(ViewCalcHist);
            }

            
        }

        private void riskArbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RiskArb == null || RiskArb.IsDisposed)
            {
                RiskArb = new frmRiskArb();
            }
            FormOpener(RiskArb);

            
        }

        private void editLimitsArbToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RiskEditArb == null || RiskEditArb.IsDisposed)
            {
                RiskEditArb = new frmEditLimitsArb();
                FormOpener(RiskEditArb);
            }

        }

        private void portfolioStatisticsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (EstastitcPortfolio == null || EstastitcPortfolio.IsDisposed)
            {
                EstastitcPortfolio = new frmStatisticsPortfolio();
                FormOpener(EstastitcPortfolio);
            }
        }

       private void rebatesPerClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RebateClients == null || RebateClients.IsDisposed)
            {
                RebateClients = new frmRebateClients();
                FormOpener(RebateClients);
            }
        }

       private void dividendsToolStripMenuItem1_Click(object sender, EventArgs e)
       {
           if (Dividends == null || Dividends.IsDisposed)
           {
               Dividends = new frmDividends();
               FormOpener(Dividends);
           }
       }

       private void importDividendsToolStripMenuItem_Click(object sender, EventArgs e)
       {
           if (ImpDividends == null || ImpDividends.IsDisposed)
           {
               ImpDividends = new frmImportDividends();
               FormOpener(ImpDividends);
           }
       }

       private void reportTestToolStripMenuItem_Click(object sender, EventArgs e)
       {

           frmRiskReportArb RiskPerto = new frmRiskReportArb();
           RiskPerto.Show();

       }


       private void prepareXMLArbToolStripMenuItem_Click(object sender, EventArgs e)
       {
           if (PrepareXML == null || PrepareXML.IsDisposed)
           {
               PrepareXML = new frmPrepareXML();
               FormOpener(PrepareXML);
           }
       }

       private void brokerageToolStripMenuItem_Click(object sender, EventArgs e)
       {
           if (Brokerage == null || Brokerage.IsDisposed)
           {
               Brokerage = new frmBrokerage();
               FormOpener(Brokerage);
           }
       }

       private void receiptWarrantToolStripMenuItem_Click(object sender, EventArgs e)
       {
           if (ReceiptWarrant == null || ReceiptWarrant.IsDisposed)
           {
               ReceiptWarrant = new frmSubscrReceipt();
               FormOpener(ReceiptWarrant);
           }
       }

       private void futuresReconculiationToolStripMenuItem_Click(object sender, EventArgs e)
       {
           if (FutureReconciliation == null || FutureReconciliation.IsDisposed)
           {
               FutureReconciliation = new frmCheckBrokersTradesFut ();
               FormOpener(FutureReconciliation);
           }
       }

       private void iTStatusToolStripMenuItem_Click(object sender, EventArgs e)
       {
           if (StatusTi == null || StatusTi.IsDisposed)
           {
               StatusTi = new frmStatusTi();
               FormOpener(StatusTi);
           }
       }

       private void checklistStatusToolStripMenuItem_Click(object sender, EventArgs e)
       {
           if (StatusOps == null || StatusOps.IsDisposed)
           {
               StatusOps = new  frmStatusOps();
               FormOpener(StatusOps);
           }
       }

       private void updateFowardsTableToolStripMenuItem_Click(object sender, EventArgs e)
       {
           using (newNestConn curConn = new newNestConn())
           {
               curConn.ExecuteNonQuery("INSERT INTO Tb207_Pending SELECT 1, getdate(), 7, 1, 0");
           }
       }

       private void mnuManageAccounts_Click(object sender, EventArgs e)
       {
           Accounts = new frmAccounts();
           FormOpener(Accounts);
       }

       
    }
}
