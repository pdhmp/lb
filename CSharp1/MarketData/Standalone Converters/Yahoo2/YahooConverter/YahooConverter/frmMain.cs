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
using System.Threading;

namespace YahooConverter
{
    public partial class frmMain : Form
    {
        public SocketReceptor curSocketReceptor;
        public YahooConn curYahooConn;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            curYahooConn = new YahooConn();
            curSocketReceptor = new SocketReceptor(new ConnManager(curYahooConn), Sources.Yahoo, 15013);

            curSocketReceptor.Initiate();
            curYahooConn.Connect();
            //curYahooConn.Subscribe("VALE5.SA", Sources.Yahoo);
            //curYahooConn.Subscribe("XOM130720P00060000", Sources.Yahoo);
            tmrUpdateScreen.Start();
            tmrUpdateSecuties.Start();

            labPort.Text = curSocketReceptor.ListenPort.ToString();

            curSocketReceptor.onClientConnected += new EventHandler(ClientConnected);
        }

        private void ClientConnected(object sender, EventArgs e)
        {
            //Thread.Sleep(5000);
            tmrUpdateSecuties_Tick(this, new EventArgs());
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            if(curYahooConn != null)
            {
                labStatus.Text = curYahooConn.curStatus;

                labUpdTime.Text = curYahooConn.LastUpdTime.ToString("HH:mm:ss");

                if(curYahooConn.IsConnected)
                {
                    lblConnected.Text = "CONNECTED";
                    lblConnected.BackColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                    lblConnected.Text = "DISCONNECTED";
                    lblConnected.BackColor = Color.Red;
                }

                if (curYahooConn.NeedsUpdating) curYahooConn.UpdateSecurities();
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(!curYahooConn.IsConnected())
                curYahooConn.Connect();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if(curYahooConn.IsConnected())
                curYahooConn.Disconnect();
        }

        private void tmrUpdateSecuties_Tick(object sender, EventArgs e)
        {
            if(curYahooConn != null)
            {
                curYahooConn.NeedsUpdating = true;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            curYahooConn.Disconnect();
            curYahooConn = null;
        }
    }
}
