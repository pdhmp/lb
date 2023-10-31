using System;
using System.Drawing;
using System.Windows.Forms;
using NCommonTypes;
using NestSymConn;

namespace BloombergConverter
{
    public partial class frmMain : Form
    {
        public SocketReceptor curSocketReceptor;
        public BloombergConn curBloombergConn;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            curBloombergConn = new BloombergConn();
            curSocketReceptor = new SocketReceptor(new ConnManager(curBloombergConn), Sources.Bloomberg, 15014);

            curSocketReceptor.Initiate();

            tmrUpdateScreen.Start();
            curBloombergConn.Connect();
           
            labPort.Text = curSocketReceptor.ListenPort.ToString();
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            if (curBloombergConn != null)
            {
                labSubCounter.Text = "Total Subscriptions: " + curBloombergConn.SubscriptionCounter;
                labValid.Text = "Valid: " + curBloombergConn.ValidTickers.Count;

                if (curBloombergConn.LastUpdTime() > new DateTime(1900, 1, 1))
                {
                    labUpdTime.Text = curBloombergConn.LastUpdTime().ToString("HH:mm:ss") + "  ";
                }

                if (curBloombergConn.IsConnected())
                {
                    lblConnected.Text = "CONNECTED";
                    lblConnected.BackColor = Color.FromArgb(0, 192, 0);
                }
                else
                {
                    lblConnected.Text = "DISCONNECTED";
                    lblConnected.BackColor = Color.Red;
                }
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (!curBloombergConn.IsConnected())
                curBloombergConn.Connect();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            if (curBloombergConn.IsConnected())
                curBloombergConn.Disconnect();
        }
    }
}
