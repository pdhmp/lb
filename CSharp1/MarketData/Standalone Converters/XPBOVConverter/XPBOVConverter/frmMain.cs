﻿using System;
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

namespace XPBOVConverter
{
    public partial class frmMain : Form
    {
        public SocketReceptor curSocketReceptor;
        XPBOVConn curXPBOVConn;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            curXPBOVConn = new XPBOVConn(@"T:\FIX\Configs\XPBOV.cfg");
            curSocketReceptor = new SocketReceptor(new ConnManager(curXPBOVConn), Sources.XPBOV, 15203);

            curSocketReceptor.Initiate();

            labPort.Text = curSocketReceptor.ListenPort.ToString();
            tmrUpdateScreen.Start();
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            if (curXPBOVConn != null)
            {
                labConfigFile.Text = curXPBOVConn.ConfigFile();

                if (curXPBOVConn.LastUpdTime() > new DateTime(1900, 1, 1))
                {
                    labUpdTime.Text = curXPBOVConn.LastUpdTime().ToString("HH:mm:ss") + "  ";
                }
                if (curXPBOVConn.LastTradeTime() > new DateTime(1900, 1, 1))
                {
                    labTradeTime.Text = curXPBOVConn.LastTradeTime().ToString("HH:mm:ss") + "  ";
                }

                if (curXPBOVConn.IsConnected())
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
            curXPBOVConn.Connect();
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            curXPBOVConn.Disconnect();
        }

        private void cmdReloadConfig_Click(object sender, EventArgs e)
        {
            curXPBOVConn.ReloadConfigFile();
        }

        private void labConfigFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(curXPBOVConn.ConfigFile());
        }
    }
}
