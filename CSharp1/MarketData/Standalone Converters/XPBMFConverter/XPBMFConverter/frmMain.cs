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

namespace XPBMFConverter
{
    public partial class frmMain : Form
    {
        public SocketReceptor curSocketReceptor;
        XPBMFConn curXPBMFConn;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            curXPBMFConn = new XPBMFConn(@"T:\FIX\Configs\XPBMF.cfg");
            curSocketReceptor = new SocketReceptor(new ConnManager(curXPBMFConn), Sources.XPBMF, 15211);

            curSocketReceptor.Initiate();

            labPort.Text = curSocketReceptor.ListenPort.ToString();
            tmrUpdateScreen.Start();
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            labConfigFile.Text = curXPBMFConn.ConfigFile();

            if (curXPBMFConn != null)
            {
                if (curXPBMFConn.LastUpdTime() > new DateTime(1900, 1, 1))
                {
                    labUpdTime.Text = curXPBMFConn.LastUpdTime().ToString("HH:mm:ss") + "  ";
                }
                if (curXPBMFConn.LastTradeTime() > new DateTime(1900, 1, 1))
                {
                    labTradeTime.Text = curXPBMFConn.LastTradeTime().ToString("HH:mm:ss") + "  ";
                }

                if (curXPBMFConn.IsConnected())
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
            curXPBMFConn.Connect();
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            curXPBMFConn.Disconnect();
        }

        private void cmdReloadConfig_Click(object sender, EventArgs e)
        {
            curXPBMFConn.ReloadConfigFile();
        }

        private void labConfigFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(curXPBMFConn.ConfigFile());
        }
    }
}
