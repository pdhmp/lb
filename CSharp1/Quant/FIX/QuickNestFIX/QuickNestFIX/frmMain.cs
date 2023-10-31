using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickNestFIX
{
    public partial class frmMain : QF_Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            OrderManager OM = OrderManager.Instance;
            FixAcceptor FromNest = new FixAcceptor();
            FixInitiator ToStreet = new FixInitiator();
            DropCopy Drop = new DropCopy();
        }

        private void receivedOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecOrders recOrders = new frmRecOrders();
            recOrders.MdiParent = this;
            recOrders.Show();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void Stop()
        {
            OrderManager.Instance.Stop();
        }

        private void receivedBySymbolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRecSymbol frmSymbol = new frmRecSymbol();
            frmSymbol.MdiParent = this;
            frmSymbol.Show();
        }

        private void execOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExecOrders frmExec = new frmExecOrders();
            frmExec.MdiParent = this;
            frmExec.Show();
        }
    }
}
