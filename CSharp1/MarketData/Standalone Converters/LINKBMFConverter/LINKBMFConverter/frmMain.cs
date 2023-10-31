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

namespace LINKBMFConverter
{
    public partial class frmMain : Form
    {
        public SocketReceptor curSocketReceptor;
        LINKBMFConn curLinkBMFConn;
            
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            curLinkBMFConn = new LINKBMFConn(@"T:\FIX\Configs\LINKBMF.cfg");
            curSocketReceptor = new SocketReceptor(new ConnManager(curLinkBMFConn), Sources.LINKBMF, 15214);

            curSocketReceptor.Initiate();

            labPort.Text = curSocketReceptor.ListenPort.ToString();
            tmrUpdateScreen.Start();
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            labConfigFile.Text = curLinkBMFConn.ConfigFile();

            if (curLinkBMFConn != null)
            {
                if (curLinkBMFConn.LastUpdTime() > new DateTime(1900, 1, 1))
                {
                    labUpdTime.Text = curLinkBMFConn.LastUpdTime().ToString("HH:mm:ss") + "  ";
                }
                if (curLinkBMFConn.LastTradeTime() > new DateTime(1900, 1, 1))
                {
                    labTradeTime.Text = curLinkBMFConn.LastTradeTime().ToString("HH:mm:ss") + "  ";
                }

                if (curLinkBMFConn.IsConnected())
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
        }

        private void cmdConnect_Click(object sender, EventArgs e)
        {
            curLinkBMFConn.Connect();
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            curLinkBMFConn.Disconnect();
        }

        private void cmdReloadConfig_Click(object sender, EventArgs e)
        {
            curLinkBMFConn.ReloadConfigFile();
        }

        private void labConfigFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(curLinkBMFConn.ConfigFile());
        }
    }
}
