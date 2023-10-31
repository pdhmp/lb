using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FeedXML2FIX
{
    public partial class frmMain : Form
    {
        private FixInitiator _oInitiator;
        private List<string> _lSessions;
        private Timer _oTimer;
        private Boolean _bProcessing;
        private SourceSafra oSafra;
        private SourceFutura oFutura;
        private SourceXP oXP;

        public frmMain()
        {
            InitializeComponent();
           
        }

        private void InsertDropCopy()
        {
            if (!_bProcessing)
            {
                InsertXP();
                //InsertSafra();
                //InsertFutura();
            }
        }
        private void InsertSafra()
        {
            if (!_bProcessing && oSafra != null)
            {
                _bProcessing = true;
                btnSafra.Enabled = false;
                oSafra.DeliverDropcopy();
                txtSafra.Text = DateTime.Now.ToString("dd-MMM-yy HH:mm:ss");
                btnSafra.Enabled = true;
                _bProcessing = false;
            }
        }
        private void InsertFutura()
        {
            if (!_bProcessing && oFutura != null)
            {
                _bProcessing = true;
                btnFutura.Enabled = false;
                oFutura.DeliverDropcopy();
                txtFutura.Text = DateTime.Now.ToString("dd-MMM-yy HH:mm:ss");
                btnFutura.Enabled = true;
                _bProcessing = false;
            }
        }
        private void InsertXP()
        {
            if (!_bProcessing && oXP != null)
            {
                _bProcessing = true;
                btnXP.Enabled = false;
                oXP.DeliverDropcopy();
                txtXP.Text = DateTime.Now.ToString("dd-MMM-yy HH:mm:ss");
                btnXP.Enabled = true;
                _bProcessing = false;
            }
        }

        private void _oTimer_Tick(object sender, EventArgs e) { InsertDropCopy(); }

        private void btnSafra_Click(object sender, EventArgs e) { InsertSafra(); }
        private void btnFutura_Click(object sender, EventArgs e) { InsertFutura(); }
        private void btnXP_Click(object sender, EventArgs e) { InsertXP(); }

        internal void SendFixMessage(QuickFix42.ExecutionReport trade)
        {
            if (_oInitiator != null)
                _oInitiator.SendMessage(trade);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _oInitiator = new FixInitiator(@"c:\fixlog\xml2fix\");
            _oInitiator.InitiatorStart();

            oSafra = new SourceSafra(this);
            oFutura = new SourceFutura(this);
            oXP = new SourceXP(this);
            InsertDropCopy();

            _oTimer = new Timer();
            _oTimer.Interval = 1000 * 60 * 5;
            _oTimer.Tick += new EventHandler(_oTimer_Tick);
            _oTimer.Start();
        }
    }
}
