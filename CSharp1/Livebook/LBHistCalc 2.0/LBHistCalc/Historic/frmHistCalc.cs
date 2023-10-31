using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using LiveDLL;
using NCalculatorDLL;


namespace LBHistCalc
{
    public partial class frmHistCalc : Form
    {
        List<HistoricCalculator> AllHistoricCalcs = new List<HistoricCalculator>();

        string strMessages = "";
        Thread HistoricalThread;

        int counterRefresh = 0;
        int counterCalc = 0;
        int PortCounter = 0;
        bool flagRefresh = false;
        bool flagCalc = false;
        bool flagStop = false;

        public frmHistCalc()
        {
            InitializeComponent();

            tmrClose.Start();

            dtgLinesToCalc.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgLinesToCalc.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgLinesToCalc.LookAndFeel.SetSkinStyle("Blue");

            dgLinesToCalc.ColumnPanelRowHeight = 32;
            dgLinesToCalc.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }

        private void frmHistCalc_Load(object sender, EventArgs e)
        {

            HistoricCalculator curItem;
            curItem = new HistoricCalculator(new DateTime(2014, 02, 28), 43, -9); curItem.Calculate();
            //curItem = new HistoricCalculator(new DateTime(2013, 04, 03), 60, -9); curItem.Calculate();
            //curItem = new HistoricCalculator(new DateTime(2013, 04, 03), 50, -9); curItem.Calculate();
            //curItem = new HistoricCalculator(new DateTime(2013, 04, 03), 43, -9); curItem.Calculate();

            LoadData();
            LoadGrid();
            tmrRefresh.Start();
            tmrSavePos.Start();
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            CancelThreads();
        }

        void CancelThreads()
        {
            using (old_Conn curConn = new old_Conn())
            {
                curConn.ExecuteNonQuery("UPDATE Tb207_Pending SET [Status]=5 WHERE Source = 6 and Status = 0");
            }
            if (flagStop)
            {
                flagStop = false;
            }
            else
            {
                flagStop = true;
            }
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            dgLinesToCalc.Invalidate();
            dgLinesToCalc.RefreshData();
            txtMessages.Text = strMessages;

            if (counterRefresh == 6)
            {
                LoadData();
                counterRefresh = 0;
            }

            if (counterCalc == 6)
            {
                StartCalcAllItems();
                counterCalc = 0;
            }

            counterRefresh++;
            counterCalc++;

            CheckCancel();

            lblTimePrice.Text = new DateTime(GlobalVars.Instance.GetPriceTimer.ElapsedTime.Ticks).ToString("HH\"h\"mm\"m\"ss\"s\"");
            lblTimeRate.Text = new DateTime(GlobalVars.Instance.GetRateTimer.ElapsedTime.Ticks).ToString("HH\"h\"mm\"m\"ss\"s\"");
            lblTimeVol.Text = new DateTime(GlobalVars.Instance.GetVolTimer.ElapsedTime.Ticks).ToString("HH\"h\"mm\"m\"ss\"s\"");
            lblTimeLoadData.Text = new DateTime(GlobalVars.Instance.CreateUpdatePosTimer.ElapsedTime.Ticks).ToString("HH\"h\"mm\"m\"ss\"s\"");



            lblHitsPrice.Text = GlobalVars.Instance.GetPrice_Hit.ToString("#,##0") + " / " + (GlobalVars.Instance.GetPrice_Miss.ToString("#,##0"));
            lblHitsRate.Text = GlobalVars.Instance.GetRate_Hit.ToString("#,##0") + " / " + (GlobalVars.Instance.GetRate_Miss.ToString("#,##0"));
            lblHitsVol.Text = GlobalVars.Instance.GetVol_Hit.ToString("#,##0") + " / " + (GlobalVars.Instance.GetVol_Miss.ToString("#,##0"));

            lblPorts.Text = PortCounter.ToString();
        }

        void LoadTableClone()
        {
            List<HistoricCalculator> CloneHistoricCalcs = new List<HistoricCalculator>(AllHistoricCalcs);
            string StringSQL = "";

            foreach (HistoricCalculator ItemCalc in CloneHistoricCalcs) // Loop through List with foreach
            {
                StringSQL = StringSQL +
                " EXEC NESTRT.dbo.Proc_InsertTableStatusCalcPortfolios " + ItemCalc.IdPending + "," + ItemCalc.IdPortfolio + ",'" + ItemCalc.PositionDate.ToString("yyyyMMdd") + "',0,0,0,'" + ItemCalc.Status + "','" + ItemCalc.StartTime + "','" + ItemCalc.CreateLines + "','" + ItemCalc.CalcLines + "',0,0,0,0,0,0,'" + ItemCalc.TotalTimeTaken + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',0;";
            }
            if (StringSQL != "")
            {
                using (old_Conn curConn = new old_Conn())
                {
                    curConn.ExecuteNonQuery(StringSQL);
                }
            }
            StringSQL = "";
        }

        private void cmdLoadData_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void cmdCalcAll_Click(object sender, EventArgs e)
        {
            StartCalcAllItems();
        }

        private void StartCalcAllItems()
        {
            int ThreadId;

            HistoricalThread = new Thread(new ThreadStart(CalcAllItems));
            ThreadId = HistoricalThread.ManagedThreadId;
            HistoricalThread.Start();
        }

        private void LoadData()
        {
            if (!flagRefresh && !flagCalc)
            {
                flagRefresh = true;
                string SQLString = "SELECT Id_Pending, Id_Ticker, Ini_Date,Status FROM dbo.Tb207_Pending(NOLOCK) WHERE Source=3 AND Status in(-1,0) ORDER BY Ini_Date, Id_Ticker DESC";
                int RunMode = 0;
                int IdPortfolio = 0;
                string[] RunModeS;

                using (LiveDLL.old_Conn curConn = new LiveDLL.old_Conn())
                {
                    DataTable tablep = curConn.Return_DataTable(SQLString);
                    HistoricCalculator curItem;

                    foreach (DataRow curRow in tablep.Rows)
                    {
                        IdPortfolio = Convert.ToInt32(curRow["Id_Ticker"]);

                        RunModeS = curRow["Id_Ticker"].ToString().Split(',');

                        if (RunModeS.Length == 2)
                        {
                            RunMode = Convert.ToInt32(RunModeS[1]);
                        }
                        else
                        {
                            RunMode = 2;
                        }

                        if (curRow["Status"].ToString() == "0")
                        {
                            curItem = new HistoricCalculator(
                                LiveDLL.Utils.ParseToDateTime(curRow["Ini_Date"])
                                , (int)LiveDLL.Utils.ParseToDouble(IdPortfolio)
                                , (int)LiveDLL.Utils.ParseToDouble(curRow["Id_Pending"])
                                );

                            if (!AllHistoricCalcs.Contains(curItem))
                            {
                                AllHistoricCalcs.Add(curItem);
                            }
                        }
                    }
                }
                flagRefresh = false;
            }
        }

        private void LoadGrid()
        {
            dtgLinesToCalc.DataSource = AllHistoricCalcs;

            foreach (DevExpress.XtraGrid.Columns.GridColumn curColumn in dgLinesToCalc.Columns)
            {
                curColumn.Visible = false;
                curColumn.Width = 70;
            }

            int CurPosition = 1;
            string curColName = "";

            curColName = "IdPending"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "IdPortfolio"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "LinesToCalc"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "PositionDate"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "Status"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "StartTime"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "CalcLines"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "CreateLines"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "TotalTimeTaken"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;

            dgLinesToCalc.Columns["PositionDate"].Width = 70;
            dgLinesToCalc.Columns["Status"].Width = 140;
        }

        private void CalcAllItems()
        {
            bool ResetedAlready = false;

            if (!flagCalc)
            {

                flagCalc = true;

                GlobalVars.Instance.LoadFixedBuffers();

                foreach (HistoricCalculator curItem in AllHistoricCalcs)
                {
                    if (curItem.Status == "Not calc" && !flagStop)
                    {
                        if (!ResetedAlready)
                        {
                            PortCounter = 0;

                            GlobalVars.Instance.GetPrice_Hit = 0;
                            GlobalVars.Instance.GetRate_Hit = 0;
                            GlobalVars.Instance.GetVol_Hit = 0;

                            GlobalVars.Instance.GetPrice_Miss = 0;
                            GlobalVars.Instance.GetRate_Miss = 0;
                            GlobalVars.Instance.GetVol_Miss = 0;

                            GlobalVars.Instance.GetPriceTimer.Reset();
                            GlobalVars.Instance.GetRateTimer.Reset();
                            GlobalVars.Instance.GetVolTimer.Reset();
                            GlobalVars.Instance.CreateUpdatePosTimer.Reset();

                            ResetedAlready = true;
                        }

                        if (curItem.PositionDate.ToString("yyyy-MM-dd") == new DateTime(1900, 01, 01).ToString("yyyy-MM-dd"))
                        {
                        }

                        using (newNestConn curConn = new newNestConn())
                        {
                            DateTime CloseDate = curConn.Return_DateTime("SELECT MAX(Data_PL) FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio=" + curItem.IdPortfolio + " AND Data_PL<'" + curItem.PositionDate.ToString("yyyy-MM-dd") + "'");
                            GlobalVars.Instance.LoadDayBuffers(curItem.PositionDate, CloseDate);
                        }

                        PortCounter++;

                        curItem.Calculate();
                    }
                }

                GlobalVars.Instance.ClearBuffers();

                flagCalc = false;
            }
        }

        private void tmrSavePos_Tick(object sender, EventArgs e)
        {
            Thread ExecThread1 = new Thread(new ThreadStart(LoadTableClone));
            ExecThread1.Start();
        }

        void CheckCancel()
        {
            string SQLString = "SELECT count(*) FROM dbo.Tb207_Pending(NOLOCK) WHERE Source=6 AND Status in(0) ";
            string Retorno;

            using (old_Conn curConn = new old_Conn())
            {
                Retorno = curConn.Execute_Query_String(SQLString);
            }

            if (Retorno != "0")
            {
                CancelThreads();
            }
        }

        private void cmdFromDate_Click(object sender, EventArgs e)
        {
            frmFromDate curFromDate = new frmFromDate();
            curFromDate.Show();
        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            tmrClose.Interval = 1000 * 60 * 10;

            if (DateTime.Now.Hour >= 23 || DateTime.Now.Hour < 1) { this.Close(); }
        }


        private void frmHistCalc_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Thread.Sleep(5000);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
