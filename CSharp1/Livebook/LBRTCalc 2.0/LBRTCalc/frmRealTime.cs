using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using NCalculatorDLL;

namespace LBRTCalc
{
    public partial class frmRealTime : Form
    {
        RTCalcRunner curRTCalcRunner;

        private System.Windows.Forms.Timer tmrRefreshFast = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer tmrRefreshQuantity = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer tmrRefreshSlow = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer tmrCheckConnection = new System.Windows.Forms.Timer();
        bool RefreshStarted = false;
        System.Threading.Thread CalcThread;
        string curStatus = "";
        string CurrentFeed = "";
        static string ConfigurationFolder = @"C:\RTCalculator\";
        string ConfigurationFile = ConfigurationFolder + "FeedConn" + DateTime.Now.ToString("yyyyMMdd") + ".cfg";

        string Hour = "00";
        string Minutes = "00";
        string MktDataFile = ConfigurationFolder + "MktDataHour.cfg";
        private DateTime MktDataTime = new DateTime();
        bool CheckMktDataTime = false;

        public frmRealTime()
        {
            InitializeComponent();
            LoadMktDataTime();
            tmrClose.Start();
        }

        private void LoadMktDataTime()
        {
            if (!Directory.Exists(ConfigurationFolder))
                Directory.CreateDirectory(ConfigurationFolder);

            if (File.Exists(MktDataFile))
            {
                StreamReader sr = new StreamReader(MktDataFile);
                string tempLine = "";
                while ((tempLine = sr.ReadLine()) != null)
                {
                    if (tempLine.Contains(":"))
                    {
                        string[] Time = tempLine.Split(':');
                        if (Time[0] != "") Hour = Time[0];
                        if (Time[1] != "") Minutes = Time[1];
                    }
                }
                sr.Close();
            }
            else
            {
                File.Create(MktDataFile).Close();
                StreamWriter sw = new StreamWriter(MktDataFile, false);
                sw.WriteLine("00:00");
                sw.Close();
            }

            MktDataTime = new DateTime(1900, 01, 01, int.Parse(Hour), int.Parse(Minutes), 00);
            txtHour.Text = Hour;
            txtMinutes.Text = Minutes;
        }

        private void ChangeMktDataTime()
        {
            try
            {
                if (txtHour.Text != "") Hour = txtHour.Text;
                if (txtMinutes.Text != "") Minutes = txtMinutes.Text;

                StreamWriter sw = new StreamWriter(MktDataFile, false);
                sw.WriteLine(Hour + ":" + Minutes);
                sw.Close();

                MktDataTime = new DateTime(1900, 01, 01, int.Parse(Hour), int.Parse(Minutes), 00);
                CheckMktDataTime = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void frmRealTime_Load(object sender, EventArgs e)
        {
            GetCopyFunction();
            GlobalVars.Instance.MasterClock.Start();
            curRTCalcRunner = new RTCalcRunner();
            btnChangeFeed.Enabled = false;

            tmrRefreshSlow.Tick += new EventHandler(curRTCalcRunner.tmrRefreshSlow_Tick);
            tmrRefreshSlow.Interval = 30 * 1000;

            tmrRefreshFast.Tick += new EventHandler(curRTCalcRunner.tmrRefreshFast_Tick);
            tmrRefreshFast.Interval = 250;

            tmrRefreshQuantity.Tick += new EventHandler(curRTCalcRunner.tmrRefreshQuantity_Tick);
            tmrRefreshQuantity.Interval = 500;

            tmrCheckConnection.Tick += new EventHandler(curRTCalcRunner.tmrCheckConnection_Tick);
            tmrCheckConnection.Interval = 2000;

            CheckCurrentFeed();

            tmrStart.Start();
            tmrTurnMktData.Start();

            curRTCalcRunner.OnStarted += new EventHandler(CheckMarketData);
        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            tmrClose.Interval = 1000 * 60 * 10;

            if (DateTime.Now.Hour >= 23 || DateTime.Now.Hour < 1)
            {
                this.Close();
            }
        }

        private void tmrStart_Tick(object sender, EventArgs e)
        {
            if (!curRTCalcRunner.Started)
            {
                CalcThread = new System.Threading.Thread(curRTCalcRunner.Start);
                CalcThread.Name = "CalculatorThread";
                CalcThread.Start();
                tmrUpdateScreen.Start();
                btnChangeFeed.Enabled = true;
            }
            else
            {
                if (curRTCalcRunner.Initialized && !RefreshStarted)
                {
                    RefreshStarted = true;
                    tmrRefreshFast.Start();
                    tmrRefreshQuantity.Start();
                    tmrRefreshSlow.Start();
                    tmrCheckConnection.Start();
                }
            }
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            labLastMktData.Text = GlobalVars.Instance.LastMarketDataUpdate.ToString("HH\":\"mm\":\"ss\"\"");
            labQuantityCheck.Text = GlobalVars.Instance.LastQuantityUpdate.ToString("HH\":\"mm\":\"ss\"\"");
            labRecalcAll.Text = GlobalVars.Instance.LastRecalcAll.ToString("HH\":\"mm\":\"ss\"\"");
            lblRecalcAllTimeTaken.Text = GlobalVars.Instance.LastRecalcAllTimeTaken.TotalSeconds.ToString("0.00") + " s";
            lblRTQuantityTimeTaken.Text = GlobalVars.Instance.LastRTQuantityTimeTaken.TotalSeconds.ToString("0.00") + " s";
            labAllFields.Text = GlobalVars.Instance.AllFieldsUpdateMS.ToString("0.0") + " ms";
            labRTFields.Text = GlobalVars.Instance.RTFieldsUpdateMS.ToString("0.0") + " ms";
            labQTFields.Text = GlobalVars.Instance.QTFieldsUpdateMS.ToString("0.0") + " ms";
            labMktDataQueue.Text = "MarketData Pending: " + GlobalVars.Instance.MarketDataQueueCounter.ToString();
            labSQLInsertQueue.Text = "SQLUpdate Pending: " + GlobalVars.Instance.SQLInsertQueueCounter.ToString();
            lblCurrentFeed.Text = CurrentFeed;

            if (GlobalVars.Instance.StatusQueue.Count > 0)
            {
                if (curStatus != GlobalVars.Instance.StatusQueue.Peek())
                {
                    txtLogView.Text += DateTime.Now.ToString("hh:mm:ss") + ": " + GlobalVars.Instance.StatusQueue.Dequeue() + "\r\n";
                    txtLogView.SelectionStart = txtLogView.Text.Length;
                    txtLogView.ScrollToCaret();
                    txtLogView.Refresh();
                }
                else
                    GlobalVars.Instance.StatusQueue.Dequeue();
            }

            bool IsConnected = GlobalVars.Instance.MarketDataConnected;

            if (curRTCalcRunner.Initialized)
            {
                labStarted.Text = "STARTED";
                labStarted.BackColor = Color.FromArgb(0, 192, 0);
            }
            else
            {
                labStarted.Text = "NOT STARTED";
                labStarted.BackColor = Color.Gray;
            }

            if (IsConnected)
            {
                btnMKTData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            }
            else
            {
                btnMKTData.BackColor = Color.Red;
            }
        }

        private void frmRealTime_FormClosing(object sender, FormClosingEventArgs e)
        {
            curRTCalcRunner.Dispose();
        }

        public static string GetCompareFunction()
        {
            PositionItem curPositionItem = new PositionItem();
            string FieldList = "";
            foreach (PropertyInfo property in curPositionItem.GetType().GetProperties())
            {
                string Formatter = "";
                string PreFormatter = "";
                if (property.PropertyType == typeof(DateTime)) { Formatter = ".ToString(\"yyyy-MM-dd\") + \"'\""; PreFormatter = "'"; }
                if (property.PropertyType == typeof(double)) { Formatter = ".ToString().Replace(\",\", \".\")"; }
                if (property.PropertyType == typeof(int)) { Formatter = ".ToString().Replace(\",\", \".\")"; }
                if (property.PropertyType == typeof(string)) { Formatter = "+ \"'\""; PreFormatter = "'"; }

                string DBField = curPositionItem.GetFieldName(property.Name);

                if (DBField != "")
                {
                    string curField = "if (newPositionItem." + property.Name + " != oldPositionItem." + property.Name + ") UpdateString += \", " + curPositionItem.GetFieldName(property.Name) + "=" + PreFormatter + "\" + newPositionItem." + property.Name + Formatter + ";";
                    FieldList += curField + "\r\n";
                }
            }

            var members = typeof(PositionItem).GetFields().Select(m => new
            {
                Name = m.Name,
                MemType = m.MemberType,
                RtField = m.GetType(),
                Type = m.FieldType
            });

            foreach (var property in members)
            {
                string Formatter = "";
                string PreFormatter = "";
                if (property.Type == typeof(DateTime)) { Formatter = ".ToString(\"yyyy-MM-dd\") + \"'\""; PreFormatter = "'"; }
                if (property.Type == typeof(double)) { Formatter = ".ToString().Replace(\",\", \".\")"; }
                if (property.Type == typeof(int)) { Formatter = ".ToString().Replace(\",\", \".\")"; }
                if (property.Type == typeof(string)) { Formatter = "+ \"'\""; PreFormatter = "'"; }

                string DBField = curPositionItem.GetFieldName(property.Name);

                if (DBField != "")
                {
                    string curField = "if (newPositionItem." + property.Name + " != oldPositionItem." + property.Name + ") UpdateString += \", " + curPositionItem.GetFieldName(property.Name) + "=" + PreFormatter + "\" + newPositionItem." + property.Name + Formatter + ";";
                    FieldList += curField + "\r\n";
                }
            }

            return FieldList;
        }

        public static string GetCopyFunction()
        {
            PositionItem curPositionItem = new PositionItem();
            string FieldList = "";
            foreach (PropertyInfo property in curPositionItem.GetType().GetProperties())
            {
                if (property.CanWrite)
                {
                    string curField = "newPositionItem." + property.Name + " = oldPositionItem." + property.Name + ";";
                    FieldList += curField + "\r\n";
                }
            }

            var members = typeof(PositionItem).GetFields().Select(m => new
            {
                Name = m.Name,
                MemType = m.MemberType,
                RtField = m.GetType(),
                Type = m.FieldType
            });

            foreach (var property in members)
            {
                string curField = "newPositionItem." + property.Name + " = oldPositionItem." + property.Name + ";";
                FieldList += curField + "\r\n";
            }

            return FieldList;
        }

        private void btnChangeFeed_Click(object sender, EventArgs e)
        {
            ChangeCurrentFeed();
            CheckCurrentFeed();

            if (curRTCalcRunner.Started && curRTCalcRunner.Initialized)
            {
                while (curRTCalcRunner.RecalcAllRunning) { Thread.Sleep(1000); }

                if (curRTCalcRunner.BloombergBackup == false)
                {
                    GlobalVars.Instance.StatusQueue.Enqueue("Changing to Normal Feed");
                    curRTCalcRunner.DeleteAllCalculators();
                    curRTCalcRunner.Started = false;
                }
                else if (curRTCalcRunner.BloombergBackup == true)
                {
                    GlobalVars.Instance.StatusQueue.Enqueue("Changing to Bloomberg Feed");
                    curRTCalcRunner.DeleteAllCalculators();
                    curRTCalcRunner.Started = false;
                }
            }
        }

        private void CheckCurrentFeed()
        {
            if (!Directory.Exists(ConfigurationFolder))
                Directory.CreateDirectory(ConfigurationFolder);

            if (File.Exists(ConfigurationFile))
            {
                StreamReader sr = new StreamReader(ConfigurationFile);
                string tempLine = "";
                while ((tempLine = sr.ReadLine()) != null)
                {
                    if (tempLine.Contains("="))
                    {
                        string[] curLine = tempLine.Split('=');
                        if (curLine[0] == "CurrentFeed")
                            CurrentFeed = curLine[1];
                    }
                }
                sr.Close();
            }
            else
            {
                File.Create(ConfigurationFile).Close();
                StreamWriter sw = new StreamWriter(ConfigurationFile, true);
                sw.WriteLine("CurrentFeed=Normal");
                sw.Close();
                CurrentFeed = "Normal";
            }

            if (CurrentFeed == "Normal") { curRTCalcRunner.BloombergBackup = false; }
            else if (CurrentFeed == "Bloomberg") { curRTCalcRunner.BloombergBackup = true; }
        }

        private void ChangeCurrentFeed()
        {
            if (CurrentFeed == "Normal")
            {
                StreamWriter sw = new StreamWriter(ConfigurationFile);
                sw.WriteLine("CurrentFeed=Bloomberg");
                sw.Close();
            }
            else if (CurrentFeed == "Bloomberg")
            {
                StreamWriter sw = new StreamWriter(ConfigurationFile);
                sw.WriteLine("CurrentFeed=Normal");
                sw.Close();
            }
        }

        private void chkProcessMktData_CheckedChanged(object sender, EventArgs e)
        {
            if (chkProcessMktData.Checked)
            {
                curRTCalcRunner.EnableMarketData();
                if (!tmrRefreshFast.Enabled) { tmrRefreshFast.Start(); }
                chkProcessMktData.ForeColor = Color.Black;
                tmrRefreshSlow.Interval = 30 * 1000;
            }
            else
            {
                CheckMktDataTime = false;
                curRTCalcRunner.DisableMarketData();
                if (tmrRefreshFast.Enabled) { tmrRefreshFast.Stop(); }
                chkProcessMktData.ForeColor = Color.Red;
                tmrRefreshSlow.Interval = 2000;
            }
        }

        public void CheckMarketData(object sender, EventArgs e)
        {
            if (chkProcessMktData.Checked)
            {
                curRTCalcRunner.EnableMarketData();
                chkProcessMktData.ForeColor = Color.Black;
            }
            else
            {
                curRTCalcRunner.DisableMarketData();
                chkProcessMktData.ForeColor = Color.Red;
            }
        }

        private void bntRecalc_Click(object sender, EventArgs e)
        {
            Thread RecalcAllThread = new Thread(curRTCalcRunner.ReCalcAll);
            RecalcAllThread.Name = "RecalcAllThread";
            RecalcAllThread.Start();
        }

        private void btnMKTData_Click(object sender, EventArgs e)
        {
            curRTCalcRunner.Connect();
        }

        private void tmrTurnMktData_Tick(object sender, EventArgs e)
        {
            if (CheckMktDataTime)
            {
                if (DateTime.Now.TimeOfDay >= MktDataTime.TimeOfDay && curRTCalcRunner.Started)
                {
                    chkProcessMktData.Checked = false;
                }
                else if (!chkProcessMktData.Checked && curRTCalcRunner.Started)
                {
                    chkProcessMktData.Checked = true;
                }
            }
            else
            {
                tmrTurnMktData.Interval = 8000;
            }
        }

        private void txtHour_TextChanged(object sender, EventArgs e)
        {
            if (txtHour.Text.Length == 2)
            {
                if (int.Parse(txtHour.Text) >= 0 && int.Parse(txtHour.Text) < 24)
                {
                    ChangeMktDataTime();
                }
            }
        }

        private void txtMinutes_TextChanged(object sender, EventArgs e)
        {
            if (txtMinutes.Text.Length == 2)
            {
                if (int.Parse(txtMinutes.Text) >= 0 && int.Parse(txtMinutes.Text) < 60)
                {
                    ChangeMktDataTime();
                }
            }
        }

        private void frmRealTime_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Thread.Sleep(5000);
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
    }
}
