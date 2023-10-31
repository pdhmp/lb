using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NCustomControls;
using NestSymConn;

namespace Tomahawk
{
    public partial class btnCancelAll : Form
    {
        TomahawkExec curExec;

        private bool flagMouseDown = false;
        BindingSource bndSymbols = new BindingSource();
        BindingSource bndOrders = new BindingSource();

        public btnCancelAll()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            curExec = new TomahawkExec();                        
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            RefreshGrids();
        }

        private void RefreshGrids()
        {
            if (!flagMouseDown)
            {
                dgvSymbols.LayoutChanged();
                dgvSymbols.UpdateGroupSummary();
                dgvSymbols.RefreshData();

                dgvOrders.LayoutChanged();
                dgvOrders.UpdateGroupSummary();
                dgvOrders.RefreshData();
            }
        }

        private void GridMouseDown(object sender, MouseEventArgs e)
        {
            flagMouseDown = true;
        }

        private void GridMouseUp(object sender, MouseEventArgs e)
        {
            flagMouseDown = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SymConn.Instance.Dispose();
        }

        private void tmrHedge_Tick(object sender, EventArgs e)
        {
            curExec.CalculateHedge();

            lblFHTicker.Text = curExec.FHTicker.ToString();
            lblFHPrevPos.Text = curExec.FHPrevPos.ToString();
            lblFHNewPos.Text = curExec.FHNewPos.ToString();
            lblFHExec.Text = curExec.FHExec.ToString();
            lblFHValue2.Text = curExec.FHValue.ToString();
            lblFHAction.Text = curExec.FHAction.ToString();

            lblMHTicker.Text = curExec.MHTicker.ToString();
            lblMHPrevPos.Text = curExec.MHPrevPos.ToString();
            lblMHNewPos.Text = curExec.MHNewPos.ToString();
            lblMHExec.Text = curExec.MHExec.ToString();
            lblMHValue2.Text = curExec.MHValue.ToString();
            lblMHAction.Text = curExec.MHAction.ToString();

            lblNetValue.Text = curExec.NetValue.ToString();
            lblTotalHedge.Text = (curExec.MHValue + curExec.FHValue).ToString();
            lblValueDiff.Text = (curExec.NetValue + curExec.FHValue + curExec.MHValue).ToString();
        }

        private void btnSendOrders_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Send ALL Orders?", "Send Orders", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                curExec.SendOrders();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Cancel ALL Orders?", "Cancel Orders", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                curExec.CancelAllOrders();
            }
        }

        private void btnStartHedge_Click(object sender, EventArgs e)
        {
            tmrHedge.Start();
        }

        private void btnStopHedge_Click(object sender, EventArgs e)
        {
            tmrHedge.Stop();
        }

        private void btnSendTest_Click(object sender, EventArgs e)
        {
            curExec.SendTestOrder();
        }

        private void btnCancelTest_Click(object sender, EventArgs e)
        {
            curExec.CancelTestOrder();
        }

        private void btnSendHedge_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Send Hedge orders?", "Hedge Order Send", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                curExec.SendHedge();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            curExec.LoadData();

            bndSymbols.DataSource = curExec.Symbols;
            dgcSymbols.DataSource = bndSymbols;

            bndOrders.DataSource = curExec.Orders;
            dgcOrders.DataSource = bndOrders;

            tmrRefresh.Start();
            tmrHedge.Start();

            btnLoad.Enabled = false;
        }
    }
}
