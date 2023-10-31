using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace NDataDist
{
    public partial class frmMain : Form
    {
        DataDist curDataDist = new DataDist();

        //NestSymConn.DataLog curDataLog = new NestSymConn.DataLog(@"C:\FIXLOG\Feeds\DataDistForm\" + DateTime.Now.Date.ToString("yyyyMMdd") + ".txt");
        //public void SetDebugLevel(int DebugLevel) { curDataLog.DebugLevel = DebugLevel; }
        //public void SetDebugMode(NestSymConn.DebugModes DebugMode) { curDataLog.DebugMode = DebugMode; }

        bool Started = false;
        bool dontRunHandler = false;

        bool AutoStart = false;
        static string ConfigurationFolder = @"C:\NDataDist\";
        string ConfigurationFile = ConfigurationFolder + "StartConfig.cfg";

        List<string> DataClients = new List<string>();

        public frmMain()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ChangeStartOption(checkBox1.Checked);
        }

        private void ChangeStartOption(bool aux)
        {
            if (aux)
            {
                StreamWriter sw = new StreamWriter(ConfigurationFile);
                sw.WriteLine("true");
                sw.Close();
            }
            else
            {
                StreamWriter sw = new StreamWriter(ConfigurationFile);
                sw.WriteLine("false");
                sw.Close();
            }
        }

        private void CheckStartOption()
        {
            if (!Directory.Exists(ConfigurationFolder))
                Directory.CreateDirectory(ConfigurationFolder);

            if (File.Exists(ConfigurationFile))
            {
                StreamReader sr = new StreamReader(ConfigurationFile);
                string tempLine = "";
                while ((tempLine = sr.ReadLine()) != null)
                {
                    if (tempLine == "true") AutoStart = true; else AutoStart = false;
                }
                sr.Close();
            }
            else
            {
                File.Create(ConfigurationFile).Close();
                StreamWriter sw = new StreamWriter(ConfigurationFile, true);
                sw.WriteLine("false");
                sw.Close();
                AutoStart = false;
            }
            if (AutoStart)
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
        }

        private void AutoStartRoutine()
        {
            System.Threading.Thread.Sleep(2000);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            lstClients.DataSource = DataClients;
            tmrUpdate.Start();
            CheckStartOption();
            cmdIniBLOOMBERG_Click_1(sender, e);
            cmdConBLOOMBERG_Click(sender, e);

        }

        private bool tmrRunning = false;

        #region Update Tick
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            if (!tmrRunning)
            {
                tmrRunning = true;
                if (!Started)
                {
                    Started = true;
                    curDataDist.StartListen();

                    if (checkBox1.Checked)
                    {
                        cmdIniXPUMDFBov_Click(this, new EventArgs());
                        cmdIniXPUMDFBmf_Click(this, new EventArgs());
                        cmdIniYAHOO_Click(this, new EventArgs());

                        System.Threading.Thread.Sleep(3000);
                        radXPUMDFBov.Checked = true;
                        radXPUMDFBmf.Checked = true;
                    }
                }

                string temptext = DateTime.Now.TimeOfDay.ToString();
                if (temptext.Contains('.')) temptext = temptext.Substring(0, temptext.LastIndexOf('.'));
                labCurTime.Text = temptext;

                #region PROXYDIFFConnection
                if (curDataDist.BSEConnection != null)
                {
                    if (curDataDist.BSEConnection.IsConnected)
                    {
                        labConnectedBSE.Text = "CONNECTED";
                        labConnectedBSE.BackColor = Color.FromArgb(0, 192, 0);


                        radProxydiff.Enabled = true;
                        //radProxydiff.Checked = true;
                    }
                    else
                    {
                        labConnectedBSE.Text = "DISCONNECTED";
                        labConnectedBSE.BackColor = Color.Red;
                        radProxydiff.Enabled = false;
                        //radProxydiff.Checked = false;
                    }

                    //curDataDist.StartListen();

                    TimeSpan BSETime = curDataDist.BSEConnection.LastUpdTime.TimeOfDay;
                    if (BSETime != new TimeSpan(12, 0, 0) && BSETime != new TimeSpan(0, 0, 0) && BSETime.ToString() != labBSETime.Text)
                    {
                        labBSETime.Text = BSETime.ToString();
                    }
                }
                #endregion

                #region LINKBOVConnection
                if (curDataDist.LINKBOVConnection != null)
                {
                    if (curDataDist.LINKBOVConnection.IsConnected)
                    {
                        labConnectedLINKBOV.Text = "CONNECTED";
                        labConnectedLINKBOV.BackColor = Color.FromArgb(0, 192, 0);

                        //radLBVDirect.Enabled = true;
                        radLINKBOV.Enabled = true;

                        string sHostConfigFile = getHostConfigFile(curDataDist.LINKBOVConnection.ConfigFile());
                        if (sHostConfigFile == "VPN")
                        {
                            dontRunHandler = false;
                        }
                        else if (sHostConfigFile == "DIRECT")
                        {
                            dontRunHandler = false;
                            //radLBVDirect.Checked = true;
                        }
                    }
                    else
                    {
                        labConnectedLINKBOV.Text = "DISCONNECTED";
                        labConnectedLINKBOV.BackColor = Color.Red;

                        radLINKBOV.Enabled = false;
                    }

                    TimeSpan LINKBOVTime = curDataDist.LINKBOVConnection.LastUpdTime.TimeOfDay;
                    if (LINKBOVTime != new TimeSpan(12, 0, 0) && LINKBOVTime != new TimeSpan(0, 0, 0) && LINKBOVTime.ToString() != labLINKBOVTime.Text)
                    {
                        labLINKBOVTime.Text = LINKBOVTime.ToString();
                    }
                }
                #endregion

                #region XPUMDFBovConnection
                if (curDataDist.XPUMDFBovConnection != null)
                {
                    if (curDataDist.XPUMDFBovConnection.IsConnected)
                    {
                        labConnectedXPUMDFBov.Text = "CONNECTED";
                        labConnectedXPUMDFBov.BackColor = Color.FromArgb(0, 192, 0);

                        radXPUMDFBov.Enabled = true;
                    }
                    else
                    {
                        labConnectedXPUMDFBov.Text = "DISCONNECTED";
                        labConnectedXPUMDFBov.BackColor = Color.Red;
                        radXPUMDFBov.Enabled = false;
                    }

                    TimeSpan XPUMDFBovTime = curDataDist.XPUMDFBovConnection.LastUpdTime.TimeOfDay;
                    if (XPUMDFBovTime != new TimeSpan(12, 0, 0) && XPUMDFBovTime != new TimeSpan(0, 0, 0) && XPUMDFBovTime.ToString() != labXPUMDFBovTime.Text)
                    {
                        labXPUMDFBovTime.Text = XPUMDFBovTime.ToString();
                    }
                }
                #endregion

                #region XPUMDFBmfConnection
                if (curDataDist.XPUMDFBmfConnection != null)
                {
                    if (curDataDist.XPUMDFBmfConnection.IsConnected)
                    {
                        labConnectedXPUMDFBmf.Text = "CONNECTED";
                        labConnectedXPUMDFBmf.BackColor = Color.FromArgb(0, 192, 0);

                        radXPUMDFBmf.Enabled = true;

                        //string sHostConfigFile = getHostConfigFile(curDataDist.XPUMDFBmfConnection.ConfigFile());
                        //if(sHostConfigFile == "VPN")
                        //{
                        //    dontRunHandler = false;
                        //    radXBMFVPN.Checked = true;
                        //}
                        //else if(sHostConfigFile == "DIRECT")
                        //{
                        //    dontRunHandler = false;
                        //    radXBMFDirect.Checked = true;
                        //}
                    }
                    else
                    {
                        labConnectedXPUMDFBmf.Text = "DISCONNECTED";
                        labConnectedXPUMDFBmf.BackColor = Color.Red;

                        radXPUMDFBmf.Enabled = false;
                    }

                    TimeSpan XPUMDFBmfTime = curDataDist.XPUMDFBmfConnection.LastUpdTime.TimeOfDay;
                    if (XPUMDFBmfTime != new TimeSpan(12, 0, 0) && XPUMDFBmfTime != new TimeSpan(0, 0, 0) && XPUMDFBmfTime.ToString() != labXPUMDFBmfTime.Text)
                    {
                        labXPUMDFBmfTime.Text = XPUMDFBmfTime.ToString();
                    }
                }
                #endregion

                #region BELLConnection
                if (curDataDist.BELLConnection != null)
                {
                    if (curDataDist.BELLConnection.IsConnected)
                    {
                        labConnectedBELL.Text = "CONNECTED";
                        labConnectedBELL.BackColor = Color.FromArgb(0, 192, 0);


                        radBELL.Enabled = true;

                        string sHostConfigFile = getHostConfigFile(curDataDist.BELLConnection.ConfigFile());
                        if (sHostConfigFile == "VPN")
                        {
                            dontRunHandler = false;

                        }
                        else if (sHostConfigFile == "DIRECT")
                        {
                            dontRunHandler = false;

                        }
                    }
                    else
                    {
                        labConnectedBELL.Text = "DISCONNECTED";
                        labConnectedBELL.BackColor = Color.Red;

                        radBELL.Enabled = false;
                    }

                    TimeSpan BELLTime = curDataDist.BELLConnection.LastUpdTime.TimeOfDay;
                    if (BELLTime != new TimeSpan(12, 0, 0) && BELLTime != new TimeSpan(0, 0, 0) && DateTime.Now.Date.Add(BELLTime).ToString("HH:mm:ss") != labBELLTime.Text)
                    {
                        labBELLTime.Text = DateTime.Now.Date.Add(BELLTime).ToString("HH:mm:ss");
                    }
                }
                #endregion

                #region BELLXPConnection
                if (curDataDist.BELLXPConnection != null)
                {
                    if (curDataDist.BELLXPConnection.IsConnected)
                    {
                        labConnectedBELLXP.Text = "CONNECTED";
                        labConnectedBELLXP.BackColor = Color.FromArgb(0, 192, 0);

                        radBELLXP.Enabled = true;

                        string sHostConfigFile = getHostConfigFile(curDataDist.BELLXPConnection.ConfigFile());
                        if (sHostConfigFile == "@INTERNET")
                        {
                            dontRunHandler = false;
                        }
                        else if (sHostConfigFile == "DIRECT")
                        {
                            dontRunHandler = false;

                        }
                    }
                    else
                    {
                        labConnectedBELLXP.Text = "DISCONNECTED";
                        labConnectedBELLXP.BackColor = Color.Red;
                    }

                    TimeSpan BELLXPTime = curDataDist.BELLXPConnection.LastUpdTime.TimeOfDay;
                    if (BELLXPTime != new TimeSpan(12, 0, 0) && BELLXPTime != new TimeSpan(0, 0, 0) && DateTime.Now.Date.Add(BELLXPTime).ToString("HH:mm:ss") != labBELLXPTime.Text)
                    {
                        labBELLXPTime.Text = DateTime.Now.Date.Add(BELLXPTime).ToString("HH:mm:ss");
                    }
                }
                #endregion

                #region LINKBMFConnection
                if (curDataDist.LINKBMFConnection != null)
                {
                    if (curDataDist.LINKBMFConnection.IsConnected)
                    {
                        labConnectedLINKBMF.Text = "CONNECTED";
                        labConnectedLINKBMF.BackColor = Color.FromArgb(0, 192, 0);

                        radLINKBMF.Enabled = true;

                        string sHostConfigFile = getHostConfigFile(curDataDist.LINKBMFConnection.ConfigFile());
                        if (sHostConfigFile == "VPN")
                        {
                            dontRunHandler = false;
                        }
                        else if (sHostConfigFile == "DIRECT")
                        {
                            dontRunHandler = false;
                        }
                    }
                    else
                    {
                        labConnectedLINKBMF.Text = "DISCONNECTED";
                        labConnectedLINKBMF.BackColor = Color.Red;

                        radLINKBMF.Enabled = false;
                    }

                    TimeSpan LINKBMFTime = curDataDist.LINKBMFConnection.LastUpdTime.TimeOfDay;
                    if (LINKBMFTime != new TimeSpan(12, 0, 0) && LINKBMFTime != new TimeSpan(0, 0, 0) && DateTime.Now.Date.Add(LINKBMFTime).ToString("HH:mm:ss") != labLINKBMFTime.Text)
                    {
                        labLINKBMFTime.Text = DateTime.Now.Date.Add(LINKBMFTime).ToString("HH:mm:ss");
                    }
                }
                #endregion

                #region FlexConnection
                if (curDataDist.FlexConnection != null)
                {
                    if (curDataDist.FlexConnection.IsConnected)
                    {
                        labConnectedFLEX.Text = "CONNECTED";
                        labConnectedFLEX.BackColor = Color.FromArgb(0, 192, 0);
                    }
                    else
                    {
                        labConnectedFLEX.Text = "DISCONNECTED";
                        labConnectedFLEX.BackColor = Color.Red;
                    }

                    TimeSpan FLEXTime = curDataDist.FlexConnection.LastUpdTime.TimeOfDay;
                    if (FLEXTime != new TimeSpan(12, 0, 0) && FLEXTime != new TimeSpan(0, 0, 0) && FLEXTime.ToString() != labFLEXTime.Text)
                    {
                        labFLEXTime.Text = FLEXTime.ToString();
                    }
                }
                #endregion

                #region YahooConnection
                if (curDataDist.YahooConnection != null)
                {
                    if (curDataDist.YahooConnection.IsConnected)
                    {
                        labConnectedYAHOO.Text = "CONNECTED";
                        labConnectedYAHOO.BackColor = Color.FromArgb(0, 192, 0);
                    }
                    else
                    {
                        labConnectedYAHOO.Text = "DISCONNECTED";
                        labConnectedYAHOO.BackColor = Color.Red;
                    }

                    TimeSpan YAHOOTime = curDataDist.YahooConnection.LastUpdTime.TimeOfDay;
                    if (YAHOOTime != new TimeSpan(12, 0, 0) && YAHOOTime != new TimeSpan(0, 0, 0) && YAHOOTime.ToString() != labYAHOOTime.Text)
                    {
                        labYAHOOTime.Text = YAHOOTime.ToString();
                    }
                }
                #endregion

                #region BloombergConnection
                if (curDataDist.BloombergConnection != null)
                {
                    if (curDataDist.BloombergConnection.IsConnected)
                    {
                        labConnectedBLOOMBERG.Text = "CONNECTED";
                        labConnectedBLOOMBERG.BackColor = Color.FromArgb(0, 192, 0);
                    }
                    else
                    {
                        labConnectedBLOOMBERG.Text = "DISCONNECTED";
                        labConnectedBLOOMBERG.BackColor = Color.Red;
                    }

                    TimeSpan BLOOMBERGTime = curDataDist.BloombergConnection.LastUpdTime.TimeOfDay;
                    if (BLOOMBERGTime != new TimeSpan(12, 0, 0) && BLOOMBERGTime != new TimeSpan(0, 0, 0) && BLOOMBERGTime.ToString() != labBLOOMBERGTime.Text)
                    {
                        labBLOOMBERGTime.Text = BLOOMBERGTime.ToString();
                    }
                }
                #endregion

                #region UBSUMDFBovConnection
                if (curDataDist.UBSUMDFBovConnection != null)
                {
                    if (curDataDist.UBSUMDFBovConnection.IsConnected)
                    {
                        labConnectedUBSUMDFBov.Text = "CONNECTED";
                        labConnectedUBSUMDFBov.BackColor = Color.FromArgb(0, 192, 0);

                        radUBSUMDFBov.Enabled = true;
                    }
                    else
                    {
                        labConnectedUBSUMDFBov.Text = "DISCONNECTED";
                        labConnectedUBSUMDFBov.BackColor = Color.Red;
                        radUBSUMDFBov.Enabled = false;
                    }

                    TimeSpan UBSUMDFBovTime = curDataDist.UBSUMDFBovConnection.LastUpdTime.TimeOfDay;
                    if (UBSUMDFBovTime != new TimeSpan(12, 0, 0) && UBSUMDFBovTime != new TimeSpan(0, 0, 0) && UBSUMDFBovTime.ToString() != labUBSUMDFBovTime.Text)
                    {
                        labUBSUMDFBovTime.Text = UBSUMDFBovTime.ToString();
                    }
                }
                #endregion

                #region UBSUMDFBmfConnection
                if (curDataDist.UBSUMDFBmfConnection != null)
                {
                    if (curDataDist.UBSUMDFBmfConnection.IsConnected)
                    {
                        labConnectedUBSUMDFBmf.Text = "CONNECTED";
                        labConnectedUBSUMDFBmf.BackColor = Color.FromArgb(0, 192, 0);

                        radUBSUMDFBmf.Enabled = true;
                    }
                    else
                    {
                        labConnectedUBSUMDFBmf.Text = "DISCONNECTED";
                        labConnectedUBSUMDFBmf.BackColor = Color.Red;

                        radUBSUMDFBmf.Enabled = false;
                    }

                    TimeSpan UBSUMDFBmfTime = curDataDist.UBSUMDFBmfConnection.LastUpdTime.TimeOfDay;
                    if (UBSUMDFBmfTime != new TimeSpan(12, 0, 0) && UBSUMDFBmfTime != new TimeSpan(0, 0, 0) && UBSUMDFBmfTime.ToString() != labUBSUMDFBmfTime.Text)
                    {
                        labUBSUMDFBmfTime.Text = UBSUMDFBmfTime.ToString();
                    }
                }
                #endregion


                lstClients.DataSource = DataClients;
                //lstClients.Invalidate();
                tmrRunning = false;
            }
        }
        #endregion

        #region CommandButtons

        private void cmdIniBSE_Click(object sender, EventArgs e)
        {
            curDataDist.InitializeBSE();
            cmdIniBSE.Enabled = false;
            cmdConBSE.Enabled = true;
            cmdDisBSE.Enabled = true;


        }

        private void cmdIniLINKBOV_Click(object sender, EventArgs e)
        {
            curDataDist.InitializeLINKBOV();
            cmdIniLINKBOV.Enabled = false;
            cmdConLINKBOV.Enabled = true;
            cmdDisLINKBOV.Enabled = true;

            //radLBVDirect.Enabled = true;
            //radLBVVPN.Enabled = true;
        }

        private void cmdIniXPUMDFBov_Click(object sender, EventArgs e)
        {
            curDataDist.InitializeXPUMDFBov();
            cmdIniXPUMDFBov.Enabled = false;
            cmdConXPUMDFBov.Enabled = true;
            cmdDisXPUMDFBov.Enabled = true;
        }

        private void cmdIniXPUMDFBmf_Click(object sender, EventArgs e)
        {
            curDataDist.InitializeXPUMDFBmf();
            cmdIniXPUMDFBmf.Enabled = false;
            cmdConXPUMDFBmf.Enabled = true;
            cmdDisXPUMDFBmf.Enabled = true;
        }

        private void cmdIniBELL_Click(object sender, EventArgs e)
        {
            curDataDist.InitializeBELL();
            cmdIniBELL.Enabled = false;
            cmdConBELL.Enabled = true;
            cmdDisBELL.Enabled = true;
        }

        private void cmdIniBELLXP_Click(object sender, EventArgs e)
        {
            curDataDist.InitializeBELLXP();
            cmdIniBELLXP.Enabled = false;
            cmdConBELLXP.Enabled = true;
            cmdDisBELLXP.Enabled = true;

            //radXBEDirect.Enabled = true;
            //radXBEVPN.Enabled = true;
        }

        private void cmdIniLINKBMF_Click(object sender, EventArgs e)
        {
            curDataDist.InitializeLINKBMF();
            cmdIniLINKBMF.Enabled = false;
            cmdConLINKBMF.Enabled = true;
            cmdDisLINKBMF.Enabled = true;

            //radLBMFDirect.Enabled = true;
            //radLBMFVPN.Enabled = true;
        }

        private void cmdIniFLEX_Click(object sender, EventArgs e)
        {

            curDataDist.InitializeFLEX();
            cmdIniFLEX.Enabled = false;
            cmdConFLEX.Enabled = true;
            cmdDisFLEX.Enabled = true;
        }

        private void cmdIniYAHOO_Click(object sender, EventArgs e)
        {
            curDataDist.InitializeYAHOO();
            cmdIniYAHOO.Enabled = false;
            cmdConYAHOO.Enabled = true;
            cmdDisYAHOO.Enabled = true;
        }

        private void cmdIniBLOOMBERG_Click_1(object sender, EventArgs e)
        {
            curDataDist.InitializeBLOOMBERG();
            cmdIniBLOOMBERG.Enabled = false;
            cmdConBLOOMBERG.Enabled = true;
            cmdDisBLOOMBERG.Enabled = true;
        }

        private void cmdConBSE_Click(object sender, EventArgs e)
        {
            curDataDist.BSEConnection.Connect();
        }

        private void cmdDisBSE_Click(object sender, EventArgs e)
        {
            curDataDist.BSEConnection.Disconnect();
        }

        private void cmdConXPUMDFBmf_Click(object sender, EventArgs e)
        {
            curDataDist.XPUMDFBmfConnection.Connect();
        }

        private void cmdDisXPUMDFBmf_Click(object sender, EventArgs e)
        {
            curDataDist.XPUMDFBmfConnection.Disconnect();
        }

        private void cmdConXPUMDFBov_Click(object sender, EventArgs e)
        {
            curDataDist.XPUMDFBovConnection.Connect();
        }

        private void cmdDisXPUMDFBov_Click(object sender, EventArgs e)
        {
            curDataDist.XPUMDFBovConnection.Disconnect();
        }

        private void cmdConBELL_Click(object sender, EventArgs e)
        {
            curDataDist.BELLConnection.Connect();
        }

        private void cmdDisBELL_Click(object sender, EventArgs e)
        {
            curDataDist.BELLConnection.Disconnect();
        }

        private void cmdConBELLXP_Click(object sender, EventArgs e)
        {
            curDataDist.BELLXPConnection.Connect();
        }

        private void cmdDisBELLXP_Click(object sender, EventArgs e)
        {
            curDataDist.BELLXPConnection.Disconnect();
        }

        private void cmdConFLEX_Click(object sender, EventArgs e)
        {
            curDataDist.FlexConnection.Connect();
        }

        private void cmdConYAHOO_Click(object sender, EventArgs e)
        {
            curDataDist.YahooConnection.Connect();
        }

        private void cmdDisFLEX_Click(object sender, EventArgs e)
        {
            curDataDist.FlexConnection.Disconnect();
        }

        private void cmdDisYAHOO_Click(object sender, EventArgs e)
        {
            curDataDist.YahooConnection.Disconnect();
        }

        private void cmdConLINKBOV_Click(object sender, EventArgs e)
        {
            curDataDist.LINKBOVConnection.Connect();
        }

        private void cmdDisLINKBOV_Click(object sender, EventArgs e)
        {
            curDataDist.LINKBOVConnection.Disconnect();
        }

        private void cmdConLINKBMF_Click(object sender, EventArgs e)
        {
            curDataDist.LINKBMFConnection.Connect();
        }

        private void cmdDisLINKBMF_Click(object sender, EventArgs e)
        {
            curDataDist.LINKBMFConnection.Disconnect();
        }

        private void cmdConBLOOMBERG_Click(object sender, EventArgs e)
        {
            curDataDist.BloombergConnection.Connect();
        }

        private void cmdDisBLOOMBERG_Click(object sender, EventArgs e)
        {

            curDataDist.BloombergConnection.Disconnect();
        }

        private void cmdRefreshClients_Click(object sender, EventArgs e)
        {
            DataClients = curDataDist.getDataClients();
        }

        #endregion

        #region RadioButtons

        private void radBELL_CheckedChanged(object sender, EventArgs e)
        {
            if (radBELL.Checked) curDataDist.ChangeBMFSource(NCommonTypes.Sources.BELL, "");
        }

        private void radBELLXP_CheckedChanged(object sender, EventArgs e)
        {
            if (radBELLXP.Checked) curDataDist.ChangeBMFSource(NCommonTypes.Sources.BELL, "XP");
        }

        private void radLINKBMF_CheckedChanged(object sender, EventArgs e)
        {
            if (radLINKBMF.Checked) curDataDist.ChangeBMFSource(NCommonTypes.Sources.LINKBMF, "");
        }

        private void radXPUMDFBmf_CheckedChanged(object sender, EventArgs e)
        {
            if (radXPUMDFBmf.Checked) curDataDist.ChangeBMFSource(NCommonTypes.Sources.XPUMDFBmf, "");
        }

        private void radUBSUMDFBmf_CheckedChanged(object sender, EventArgs e)
        {
            if (radUBSUMDFBmf.Checked) curDataDist.ChangeBMFSource(NCommonTypes.Sources.UBSUMDFBmf, "");
        }

        private void radUBSUMDFBov_CheckedChanged(object sender, EventArgs e)
        {
            if (radUBSUMDFBov.Checked) curDataDist.ChangeBovespaSource(NCommonTypes.Sources.UBSUMDFBov);
        }

        private void radLINKBOV_CheckedChanged(object sender, EventArgs e)
        {
            if (radLINKBOV.Checked) curDataDist.ChangeBovespaSource(NCommonTypes.Sources.LINKBOV);
        }

        private void radProxydiff_CheckedChanged(object sender, EventArgs e)
        {
            if (radProxydiff.Checked) curDataDist.ChangeBovespaSource(NCommonTypes.Sources.ProxyDiff);
        }

        private void radXPUMDFBov_CheckedChanged(object sender, EventArgs e)
        {
            if (radXPUMDFBov.Checked) curDataDist.ChangeBovespaSource(NCommonTypes.Sources.XPUMDFBov);
        }

        #endregion

        private void UpdateConfigFile(string FilePath, string newType)
        {
            StreamReader sr = new StreamReader(FilePath);
            string newFilePath = FilePath + "_new";
            StreamWriter sw = new StreamWriter(newFilePath);

            string tempLine = "";

            while ((tempLine = sr.ReadLine()) != null)
            {
                if (newType == "VPN")
                {
                    if (tempLine.Contains("@VPN"))
                    {
                        if (tempLine[0] == '#') tempLine = tempLine.Substring(1, tempLine.Length - 1);
                    }
                    if (tempLine.Contains("@DIRECT"))
                    {
                        if (tempLine[0] != '#') tempLine = "#" + tempLine;
                    }
                }

                if (newType == "DIRECT")
                {
                    if (tempLine.Contains("@DIRECT"))
                    {
                        if (tempLine[0] == '#') tempLine = tempLine.Substring(1, tempLine.Length - 1);
                    }
                    if (tempLine.Contains("@VPN"))
                    {
                        if (tempLine[0] != '#') tempLine = "#" + tempLine;
                    }
                }
                sw.WriteLine(tempLine);
            }
            sr.Close();
            sw.Close();

            File.Copy(newFilePath, FilePath, true);
            File.Delete(newFilePath);
        }

        private string getHostConfigFile(string FilePath)
        {
            StreamReader sr = new StreamReader(FilePath);
            string tempLine = "";
            string sAuxi = "";

            // SocketConnectHost =200.143.33.126 # @VPN
            // #SocketConnectHost=189.39.62.101  # @DIRECT
            while ((tempLine = sr.ReadLine()) != null)
            {
                if (tempLine.Contains("@VPN"))
                {
                    if (tempLine[0] != '#') sAuxi = "VPN";
                }

                if (tempLine.Contains("@DIRECT"))
                {
                    if (tempLine[0] != '#') sAuxi = "DIRECT";
                }
            }

            sr.Close();
            return sAuxi;
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (curDataDist.BSEConnection != null) curDataDist.BSEConnection.Disconnect();
            if (curDataDist.XPUMDFBmfConnection != null) curDataDist.XPUMDFBmfConnection.Disconnect();
            if (curDataDist.BELLConnection != null) curDataDist.BELLConnection.Disconnect();
            if (curDataDist.FlexConnection != null) curDataDist.FlexConnection.Disconnect();
            if (curDataDist.LINKBOVConnection != null) curDataDist.LINKBOVConnection.Disconnect();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void cmdIniUBSUMDFBov_Click(object sender, EventArgs e)
        {
            curDataDist.InitializeUBSUMDFBov();
            cmdIniUBSUMDFBov.Enabled = false;
            cmdConUBSUMDFBov.Enabled = true;
            cmdDisUBSUMDFBov.Enabled = true;
        }

        private void cmdConUBSUMDFBov_Click(object sender, EventArgs e)
        {
            curDataDist.UBSUMDFBovConnection.Connect();
        }

        private void cmdDisUBSUMDFBov_Click(object sender, EventArgs e)
        {
            curDataDist.UBSUMDFBovConnection.Disconnect();
        }

        private void cmdIniUBSUMDFBmf_Click(object sender, EventArgs e)
        {
            curDataDist.InitializeUBSUMDFBmf();
            cmdIniUBSUMDFBmf.Enabled = false;
            cmdConUBSUMDFBmf.Enabled = true;
            cmdDisUBSUMDFBmf.Enabled = true;
        }

        private void cmdConUBSUMDFBmf_Click(object sender, EventArgs e)
        {
            curDataDist.UBSUMDFBmfConnection.Connect();
        }



    }
}
