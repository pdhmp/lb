using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace CyrnelSync
{
    public partial class frmCyrnelSync : Form
    {
        IRtdServer m_server;
        RTDGetData curRTDGetData;
        CyrnelGenerator curCyrnelGenerator = new CyrnelGenerator();

        private bool ServerStarted = false;
        private bool StartedLogIn = false;
        private bool LoggedIn = false;
        private bool HubStarted = false;

        private bool _AutoUpdateFile = false;
        public bool AutoUpdateFile
        {
            get { return _AutoUpdateFile; }
            set
            {
                _AutoUpdateFile = value;

                if (_AutoUpdateFile)
                {
                    txtOnFile.Text = "ON";
                    txtOnFile.ForeColor = Color.Green;
                }
                else
                {
                    txtOnFile.Text = "OFF";
                    txtOnFile.ForeColor = Color.Red;
                }

            }
        }

        private bool _AutoUpdateDB = false;
        public bool AutoUpdateDB
        {
            get { return _AutoUpdateDB; }
            set
            {
                _AutoUpdateDB = value;

                if (_AutoUpdateDB)
                {
                    txtOnDB.Text = "ON";
                    txtOnDB.ForeColor = Color.Green;
                }
                else
                {
                    txtOnDB.Text = "OFF";
                    txtOnDB.ForeColor = Color.Red;
                }
            }
        }

        private int ThisHandle = 0;
        private int DBUpdateCounter = 0;
        private int TimerAdjust = 1;

        private bool FileUpdating = false;

        //RiskPortfolioItem curRiskPortfolioItem;

        public DateTime LastFileUpdate = new DateTime(1900, 01, 01, 00, 00, 00);
        public DateTime LastDBUpdate = new DateTime(1900, 01, 01, 00, 00, 00);
        public DateTime LastFileUploaded = new DateTime(1900, 01, 01, 00, 00, 00);

        Dictionary<int, RiskPortfolioItem> RiskPortfolioItems = new Dictionary<int, RiskPortfolioItem>();

        public frmCyrnelSync() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateRiskDate();

            curCyrnelGenerator.OnFinish += new EventHandler(UpdateFileFinished);
            curCyrnelGenerator.CurDate = DateTime.Now.Date;

            String RTDProgID = "Cyrnel.hub";
            Type type = Type.GetTypeFromProgID(RTDProgID);
            m_server = (Microsoft.Office.Interop.Excel.IRtdServer)Activator.CreateInstance(type);

            curRTDGetData = new RTDGetData();

            ThisHandle = this.Handle.ToInt32();

            AutoUpdateFile = true;
            AutoUpdateDB = true;

            tmrRefresh.Start();
            tmrMain.Start();

            curRTDGetData.NewDataReceived += new EventHandler(NewData);

            //InitializePortRisk(4);
            InitializePortRisk(10);
            //InitializePortRisk(38);
            //InitializePortRisk(43);
            //InitializePortRisk(50);
            //InitializePortRisk(55);
            //InitializePortRisk(60);
        }

        private void InitializePortRisk(int IdPortfolio)
        {
            RiskPortfolioItem curRiskPortfolioItem = new RiskPortfolioItem(m_server);
            curRiskPortfolioItem.IdPortfolio = IdPortfolio;
            RiskPortfolioItems.Add(IdPortfolio, curRiskPortfolioItem);
        }


        private void UpdateFile()
        {
            btnUpdateFile.Enabled = false;

            FileUpdating = true;

            Thread t1 = new Thread(curCyrnelGenerator.CreateFile);
            t1.Name = "FileUpdateThread";
            t1.Start();
        }

        private void UpdateDB()
        {
            btnUpdateDB.Enabled = false;

            foreach (RiskPortfolioItem curRiskPortfolioItem in RiskPortfolioItems.Values)
            {
                curRiskPortfolioItem.UpdateDB();
            }
            LastDBUpdate = DateTime.Now;
            btnUpdateDB.Enabled = true;
        }

        private void ServerLogin()
        {
            Thread.Sleep(2000);

            AppSendKeys("Cyrnel Hub Platform Login", "felipe.prata@nestinvestimentos.com.br"); Thread.Sleep(300);
            AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(100);
            AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(100);
            AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(100);
            AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(100);
            AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(300);
            AppSendKeys("Cyrnel Hub Platform Login", "Nest@102030"); Thread.Sleep(100);
            AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(100);
            AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(100);
            AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(100);
            AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(100);
            AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(100);
            AppSendKeys("Cyrnel Hub Platform Login", " "); Thread.Sleep(2000);
            //AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(200);
            //AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(200);
            //AppSendKeys("Cyrnel Hub Platform Login", "{TAB}"); Thread.Sleep(200);
            //AppSendKeys("Cyrnel Hub Platform Login", " ");
        }

        private void AppSendKeys(string txtTitle, string KeysToSend)
        {
            int iHandle = NativeWin32.FindWindow(null, txtTitle);

            NativeWin32.SetForegroundWindow(iHandle);

            System.Windows.Forms.SendKeys.SendWait(KeysToSend);
        }

        private void StartHub()
        {
            try
            {
                Thread.Sleep(2000); System.Diagnostics.Process.Start(@"C:\Program Files\Cyrnel\Hub Portfolio Loader\Hub.PortfolioLoader.exe");
                Thread.Sleep(3000); AppSendKeys("Hub Portfolio Loader", "{TAB}");
                Thread.Sleep(3000); AppSendKeys("Hub Portfolio Loader", "{ENTER}");

                HubStarted = true;
            }
            catch
            {
                HubStarted = false;
            }
        }

        private void tmrMain_Tick(object sender, EventArgs e)
        {
            if (!ServerStarted)
            {
                ServerStarted = true;
                m_server.ServerStart(curRTDGetData);
            }

            if (ServerStarted && !LoggedIn && !StartedLogIn)
            {
                StartedLogIn = true;
                ServerLogin();
                LoggedIn = true;
            }

            if (!HubStarted && ServerStarted && LoggedIn)
            {
                HubStarted = true;
                StartHub();
            }

            if (ServerStarted && LoggedIn)
            {
                foreach (RiskPortfolioItem curRiskPortfolioItem in RiskPortfolioItems.Values)
                {
                    curRiskPortfolioItem.SubscribeData();
                }
            }

            if (DateTime.Now.Hour > 4 && DateTime.Now.Hour < 23)
            {
                if (AutoUpdateDB && LoggedIn)
                {
                    DBUpdateCounter++;

                    if (DateTime.Now.Subtract(LastDBUpdate).TotalMinutes >= 2)
                    {
                        UpdateRiskDate();
                        TimerAdjust = 10;
                        UpdateDB();
                        DBUpdateCounter = 0;
                    }
                }

                if (DateTime.Now.Subtract(LastFileUpdate).TotalMinutes >= 15 && LoggedIn && !FileUpdating && AutoUpdateFile)
                {
                    UpdateFile();
                }
            }

            if (!FileUpdating && btnUpdateFile.Enabled == false)
            { btnUpdateFile.Enabled = true; }

        }

        private void NewData(object sender, EventArgs e)
        {
            Console.WriteLine("New Data");

            int TopicCount = 0;
            Object[,] retval = new Object[2, 1];
            retval = (Object[,])m_server.RefreshData(TopicCount);

            for (int count = 0; count < retval.Length / 2 - 1; count++)
            {
                int curToken = int.Parse(retval[0, count].ToString());

                RiskPortfolioItem curRiskPortfolioItem = RiskPortfolioItems[(int)(curToken / 100000)];

                lock (curRiskPortfolioItem.SyncLock)
                {
                    curRiskPortfolioItem.Update(curToken, retval[1, count].ToString());
                    //Console.WriteLine(retval[0, count] + "\t" + retval[1, count]);
                }
                if (curRiskPortfolioItem.NoPositions != 0) { curRiskPortfolioItem.SubscribePositionsData(); }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_server.ServerTerminate();
            Process[] prs = Process.GetProcesses();

            foreach (Process pr in prs)
            {
                if (pr.ProcessName == "Hub.PortfolioLoader")                {                    pr.Kill();                }
            }
        }

        private void UpdateFileFinished(object sender, EventArgs e)
        {
            FileUpdating = false;
            LastFileUpdate = DateTime.Now;
        }

        private void btnUpdateDB_Click(object sender, EventArgs e)        {            UpdateDB();        }

        private void btnUpdateFile_Click(object sender, EventArgs e)        {            UpdateFile();        }

        private void txtOnDB_Click(object sender, EventArgs e)
        {
            if (AutoUpdateDB)            {                AutoUpdateDB = false;            }
            else            {                AutoUpdateDB = true;                UpdateDB();            }
        }

        private void txtOnFile_Click(object sender, EventArgs e)
        {
            if (AutoUpdateFile)           {                AutoUpdateFile = false;            }
            else            {                AutoUpdateFile = true;                UpdateFile();            }
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            labLastDBUpdate.Text = "Last Update: " + LastDBUpdate.ToString("HH:mm:ss");
            labLastFileUpdate.Text = "Last Update: " + LastFileUpdate.ToString("HH:mm:ss"); ;
        }

        private void UpdateRiskDate()
        {
            string[] filePaths = Directory.GetFiles(@"N:\Risk\Cyrnel\Upload\Temp", "*.xls");
            DateTime maxDate = new DateTime(1900, 01, 01);

            for (int i = 0; i < filePaths.Length; i++)
            {
                string tempDate = filePaths[i].Substring(filePaths[0].LastIndexOf('_') + 1);

                DateTime refDate = new DateTime(int.Parse("20" + tempDate.Substring(4, 2)), int.Parse(tempDate.Substring(2, 2)), int.Parse(tempDate.Substring(0, 2)), int.Parse(tempDate.Substring(6, 2)), int.Parse(tempDate.Substring(8, 2)), 00);

                if (refDate > maxDate) maxDate = refDate;
            }

            if (maxDate != LastFileUploaded)
            {
                LastFileUploaded = maxDate;

                foreach (RiskPortfolioItem curRiskPortfolioItem in RiskPortfolioItems.Values)
                {
                    curRiskPortfolioItem.UpdateTime = LastFileUploaded;
                }

                UpdateDB();
            }
        }
    }
}
