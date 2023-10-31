using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using System.Threading;
using DevExpress.XtraGrid;
using System.Diagnostics;


namespace LBHistCalc
{
    public partial class frmHistCalc : Form
    {
        List<HistoricCalc> AllHistoricCalcs = new List<HistoricCalc>();

        bool RecalcAllLines = true;
        bool CalcSubPorts = true;
        bool CreateAdjustments = true;
        string strMessages = "";
        Thread ExecThread;
        
        int counterRefresh = 0;
        bool flagRefresh = false;
        int counterCalc = 0;
        bool flagCalc = false;
        bool flagStop = false;

        public frmHistCalc()
        {
            InitializeComponent();

            dtgLinesToCalc.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgLinesToCalc.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgLinesToCalc.LookAndFeel.SetSkinStyle("Blue");

            dgLinesToCalc.ColumnPanelRowHeight = 32;
            dgLinesToCalc.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

        }

        private void frmHistCalc_Load(object sender, EventArgs e)
        {
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
                cmdStop.Text = "Stop on next";
            }
            else
            {
                flagStop = true;
                cmdStop.Text = "Start";
            }
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            dgLinesToCalc.Invalidate();
            dgLinesToCalc.RefreshData();
            txtMessages.Text = strMessages;

            if (!flagCalc)
            {
                cmdCalcAll.Enabled = true;
                cmdLoadData.Enabled = true; 
            }
            else
            {
                cmdCalcAll.Enabled = false;
                cmdLoadData.Enabled = false; 
            }

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

        }

        void LoadTableClone()
        {
            List<HistoricCalc> CloneHistoricCalcs = new List<HistoricCalc>(AllHistoricCalcs);
            string StringSQL="";

            foreach (HistoricCalc ItemCalc in CloneHistoricCalcs) // Loop through List with foreach
            {
                StringSQL = StringSQL +
                " EXEC NESTRT.dbo.Proc_InsertTableStatusCalcPortfolios " + ItemCalc.IdPending + "," + ItemCalc.IdPortfolio + ",'" + ItemCalc.PositionDate.ToString("yyyyMMdd") + "',0,0,0,'" + ItemCalc.Status + "','" + ItemCalc.StartTime + "',0,'" + ItemCalc.CalcLines + "',0,0,0,0,0,0,'" + ItemCalc.TotalTimeTaken + "','" + DateTime.Now.ToString("yyyyMMdd") + "',0;";
            }
            if(StringSQL != "")
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

            cmdCalcAll.Enabled = false;
            cmdLoadData.Enabled = false;

            ExecThread = new Thread(new ThreadStart(CalcAllItems));
            ThreadId = ExecThread.ManagedThreadId;
            ExecThread.Start();


        }

        private void LoadData()
        {
            if (!flagRefresh && !flagCalc)
            {
                flagRefresh = true;
                string SQLString = "SELECT Id_Pending, Id_Ticker, Ini_Date,Status FROM dbo.Tb207_Pending(NOLOCK) WHERE Source=3 AND Status in(-1,0) ORDER BY Ini_Date, Id_Ticker DESC";
                int RunMode=0;
                int IdPortfolio=0;
                string[] RunModeS;

                using (NestDLL.old_Conn curConn = new NestDLL.old_Conn())
                {
                    DataTable tablep = curConn.Return_DataTable(SQLString);
                    HistoricCalc curItem;

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
                            curItem = new HistoricCalc(
                                NestDLL.Utils.ParseToDateTime(curRow["Ini_Date"])
                                , (int)NestDLL.Utils.ParseToDouble(IdPortfolio)
                                , (int)NestDLL.Utils.ParseToDouble(curRow["Id_Pending"])
                                );
                            
                            if (!AllHistoricCalcs.Contains(curItem))
                            {
                                AllHistoricCalcs.Add(curItem);
                            }
                        }


                        //if (curItem.IdPortfolio == 4)  { curItem.IdPortfolioBRL = 5; curItem.IdPortfolioUSD = 6; curItem.IdAccountBRL = 1046; curItem.IdAccountUSD = 1060; curItem.FlagHasUSD = true; }
                        //if (curItem.IdPortfolio == 43) { curItem.IdPortfolioBRL = 41; curItem.IdPortfolioUSD = 42; curItem.IdAccountBRL = 1086; curItem.IdAccountUSD = 1148; curItem.FlagHasUSD = true; }
                        //if (curItem.IdPortfolio == 10) { curItem.IdPortfolioBRL = 11; curItem.IdPortfolioUSD = 0; curItem.IdAccountBRL = 1073; curItem.IdAccountUSD = 0; curItem.FlagHasUSD = false; }
                        //if (curItem.IdPortfolio == 38) { curItem.IdPortfolioBRL = 39; curItem.IdPortfolioUSD = 0; curItem.IdAccountBRL = 1211; curItem.IdAccountUSD = 0; curItem.FlagHasUSD = false; }
                        //if (curItem.IdPortfolio == 18) { curItem.IdPortfolioBRL = 17; curItem.IdPortfolioUSD = 0; curItem.IdAccountBRL = 1289; curItem.IdAccountUSD = 0; curItem.FlagHasUSD = false; }

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
            curColName = "CreateLines"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "CalcLines"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "TotalTimeTaken"; dgLinesToCalc.Columns[curColName].Visible = true; dgLinesToCalc.Columns[curColName].VisibleIndex = CurPosition++;
            
            //dgLinesToCalc.BestFitColumns();

            dgLinesToCalc.Columns["PositionDate"].Width = 70;
            dgLinesToCalc.Columns["Status"].Width = 140;
        }

        private void CalcAllItems()
        {
            if (!flagCalc)
            {
                flagCalc = true;

                foreach (HistoricCalc curItem in AllHistoricCalcs)
                {
                    if (curItem.Status == "Not calc" && !flagStop)
                    {
                        curItem.Calculate();
                    }
                }

                flagCalc = false;
            }
        }

        private void tmrSavePos_Tick(object sender, EventArgs e)
        {
            Thread ExecThread1 = new Thread(new ThreadStart(LoadTableClone));
            ExecThread1.Start();
        }

        int CheckInSkip = 0;

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
    }
}
