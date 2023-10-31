using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestSymConn;
using NCommonTypes;
using System.IO;

namespace ProxyDiffConverter
{
    public partial class frmMain : Form
    {
        public SocketReceptor curSocketReceptor;
        PROXYDIFFConn curBSEConn;
        bool Started = false;
        int retryCounter = 0;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            tmrUpdateScreen.Start();
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            if (!Started)
            {
                curBSEConn = new PROXYDIFFConn(@"c:\temp\proxydiffDefs.txt");
                // curSocketReceptor = new SocketReceptor(new ConnManager(curBSEConn), Sources.ProxyDiff, 15201);
                curSocketReceptor = new SocketReceptor(new ConnManager(curBSEConn), Sources.ProxyDiff, 14001);
                
                curSocketReceptor.Initiate();

                labPort.Text = curSocketReceptor.ListenPort.ToString();
                Started = true;
            }

            if (curBSEConn != null)
            {
                labConfigFile.Text = curBSEConn.ConfigFile();

                if (curBSEConn.LastUpdTime() > new DateTime(1900, 1, 1))
                {
                    labUpdTime.Text = curBSEConn.LastUpdTime().TimeOfDay.ToString() + "  ";
                }
                if (curBSEConn.LastTradeTime() > new DateTime(1900, 1, 1))
                {
                    labTradeTime.Text = curBSEConn.LastTradeTime().TimeOfDay.ToString() + "  ";
                }

                if (curBSEConn.IsConnected())
                {
                    labConnectedBSE.Text = "CONNECTED";
                    labConnectedBSE.BackColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                    labConnectedBSE.Text = "DISCONNECTED";
                    labConnectedBSE.BackColor = Color.Red;

                    if (retryCounter > 10 && !cmdConnect.Enabled)
                    {
                        cmdConnect_Click(this, new EventArgs());
                        retryCounter = 0;
                    }
                    else
                    {
                        retryCounter++;
                    }
                }
            }
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            cmdConnect.Enabled = false;
            curBSEConn.Connect();
            cmdDisconnect.Enabled = true;
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            cmdDisconnect.Enabled = false;
            curBSEConn.Disconnect();
            cmdConnect.Enabled = true;
        }

        private void cmdReloadConfig_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"c:\temp\proxydiffDefs.txt");
            string tempLine = "";
            char[] curSeparator = new char[1]; curSeparator[0] = '\t';
            string LogFilePath = "";
            string curIP = "";
            string curPort = "";

            while ((tempLine = sr.ReadLine()) != null)
            {
                string[] curDef = tempLine.Split(curSeparator, StringSplitOptions.RemoveEmptyEntries);
                if (curDef.Length == 2)
                {
                    if (curDef[0] == "LogFilePath") { LogFilePath = curDef[1]; }
                    if (curDef[0] == "IP") { curIP = curDef[1]; }
                    if (curDef[0] == "Port") { curPort = curDef[1]; }
                }
            }

            curBSEConn.UpdateIPPort(curIP,curPort);
        }

        private void labConfigFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(curBSEConn.ConfigFile());
        }
    }
}
