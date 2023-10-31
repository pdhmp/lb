using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickNestFIX
{
    public partial class frmRecOrders : QF_Form
    {
        public frmRecOrders()
        {
            InitializeComponent();
            dtgOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgOrders.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgOrders.LookAndFeel.SetSkinStyle("Blue");
        }

        BindingSource bsRecOrders = new BindingSource();
        List<Order> lsRecOrders = new List<Order>();

        private void frmRecOrders_Load(object sender, EventArgs e)
        {
            bsRecOrders.DataSource = lsRecOrders;
            dtgOrders.DataSource = bsRecOrders;

            RefreshGrid();
        }

        private void RefreshGrid()
        {           
            lsRecOrders.Clear();

            foreach (Order ord in OrderManager.Instance.ReceivedOrders)
            {
                lsRecOrders.Add(ord);
            }

            dgOrders.LayoutChanged();
            dgOrders.UpdateGroupSummary();
            dgOrders.RefreshData();
            dgOrders.BestFitColumns();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }
    }
}