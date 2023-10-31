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


namespace Jericho
{
    public partial class frmJericho : Form
    {
        public frmJericho()
        {
            InitializeComponent();
        }

        Jericho curStrategy;

        private BindingSource bndOrders = new BindingSource();
        private BindingSource bndSecurities = new BindingSource();

        private bool flagMouseDown = false;

        private void Jericho_Load(object sender, EventArgs e)
        {
            curStrategy = new Jericho();            

            bndOrders.DataSource = curStrategy.Orders;
            dgcOrders.DataSource = bndOrders;

            bndSecurities.DataSource = curStrategy.Securities;
            dgcSecurities.DataSource = bndSecurities;

            timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            curStrategy.SendTestOrder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            curStrategy.SendTestCancel();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshGrids();
        }

        private void RefreshGrids()
        {
            if (!flagMouseDown)
            {
                dgvOrders.LayoutChanged();
                dgvOrders.UpdateGroupSummary();
                dgvOrders.RefreshData();

                dgvSecurities.LayoutChanged();
                dgvSecurities.UpdateGroupSummary();
                dgvSecurities.RefreshData();
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

        private void frmJericho_FormClosing(object sender, FormClosingEventArgs e)
        {
            SymConn.Instance.Dispose();
        }
    }
}
