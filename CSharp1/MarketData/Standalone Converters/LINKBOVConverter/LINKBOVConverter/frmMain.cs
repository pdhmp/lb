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
        LINKBOVConn curLINKBOVConn;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            curLINKBOVConn = new LINKBOVConn(@"T:\FIX\Configs\LINKBOV.cfg");
            curSocketReceptor = new SocketReceptor(new ConnManager(curLINKBOVConn), Sources.LINKBOV, 15202);

            curSocketReceptor.Initiate();

            labPort.Text = curSocketReceptor.ListenPort.ToString();
            tmrUpdateScreen.Start();
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            if (curLINKBOVConn != null)
            {
                labConfigFile.Text = curLINKBOVConn.ConfigFile();

                if (curLINKBOVConn.LastUpdTime() > new DateTime(1900, 1, 1))
                {
                    labUpdTime.Text = curLINKBOVConn.LastUpdTime().ToString("HH:mm:ss") + "  ";
                }
                if (curLINKBOVConn.LastTradeTime() > new DateTime(1900, 1, 1))
                {
                    labTradeTime.Text = curLINKBOVConn.LastTradeTime().ToString("HH:mm:ss") + "  ";
                }

                if (curLINKBOVConn.IsConnected())
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
            curLINKBOVConn.Connect();
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            curLINKBOVConn.Disconnect();
        }

        private void cmdReloadConfig_Click(object sender, EventArgs e)
        {
            curLINKBOVConn.ReloadConfigFile();
        }

        private void labConfigFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(curLINKBOVConn.ConfigFile());
        }
    }
}
