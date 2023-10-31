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
        BELLConn curBELLConn;
        private DateTime ValidaLastTradeTime;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            curBELLConn = new BELLConn(@"T:\FIX\Configs\BMF_BELL_LINK.cfg");
            curSocketReceptor = new SocketReceptor(new ConnManager(curBELLConn), Sources.BELL, 15212);

            curSocketReceptor.Initiate();

            labPort.Text = curSocketReceptor.ListenPort.ToString();
            tmrUpdateScreen.Start();
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            labConfigFile.Text = curBELLConn.ConfigFile();
            ValidaLastTradeTime = curBELLConn.LastUpdTime();

            if (curBELLConn != null)
            {
                if (curBELLConn.LastUpdTime() > new DateTime(1900, 1, 1))
                {
                    labUpdTime.Text = curBELLConn.LastUpdTime().ToString("HH:mm:ss") + "  ";
                }
                if (curBELLConn.LastTradeTime() > new DateTime(1900, 1, 1))
                {
                    labTradeTime.Text = curBELLConn.LastTradeTime().ToString("HH:mm:ss") + "  ";
                }

                if (curBELLConn.IsConnected())
                {
                    labConnectedBSE.Text = "CONNECTED";
                    labConnectedBSE.BackColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                    labConnectedBSE.Text = "DISCONNECTED";
                    labConnectedBSE.BackColor = Color.Red;
                }
            }
            //else
            //    Console.WriteLine("#### TESTE : curBELLConn == null");
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            curBELLConn.Connect();
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            curBELLConn.Disconnect();
        }

        private void cmdReloadConfig_Click(object sender, EventArgs e)
        {
            curBELLConn.ReloadConfigFile();
        }

        private void labConfigFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(curBELLConn.ConfigFile());
        }
    }
}
