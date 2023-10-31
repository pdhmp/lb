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

namespace BELLConverterXP
{
    public partial class frmMain : Form
    {
        public SocketReceptor curSocketReceptor;
        BELLConnXP curBELLConnXP;
            
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            curBELLConnXP = new BELLConnXP(@"T:\FIX\Configs\BMF_BELL_XP.cfg");
            curSocketReceptor = new SocketReceptor(new ConnManager(curBELLConnXP), Sources.BELL, 15213);

            curSocketReceptor.Initiate();

            labPort.Text = curSocketReceptor.ListenPort.ToString();
            tmrUpdateScreen.Start();
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            labConfigFile.Text = curBELLConnXP.ConfigFile();

            if (curBELLConnXP != null)
            {
                if (curBELLConnXP.LastUpdTime() > new DateTime(1900, 1, 1))
                {
                    labUpdTime.Text = curBELLConnXP.LastUpdTime().ToString("HH:mm:ss") + "  ";
                }
                if (curBELLConnXP.LastTradeTime() > new DateTime(1900, 1, 1))
                {
                    labTradeTime.Text = curBELLConnXP.LastTradeTime().ToString("HH:mm:ss") + "  ";
                }

                if (curBELLConnXP.IsConnected())
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
            curBELLConnXP.Connect();
        }

        private void cmdDisconnect_Click(object sender, EventArgs e)
        {
            curBELLConnXP.Disconnect();
        }

        private void cmdReloadConfig_Click(object sender, EventArgs e)
        {
            curBELLConnXP.ReloadConfigFile();
        }

        private void labConfigFile_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(curBELLConnXP.ConfigFile());
        }
    }
}
