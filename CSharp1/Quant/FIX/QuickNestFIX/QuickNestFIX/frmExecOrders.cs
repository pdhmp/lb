using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;

namespace QuickNestFIX
{
    public partial class frmExecOrders : Form
    {
        public frmExecOrders()
        {
            InitializeComponent();
            dtgOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgOrders.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgOrders.LookAndFeel.SetSkinStyle("Blue");
        }

        BindingSource bsRecOrders = new BindingSource();
        List<Order> lsRecOrders = new List<Order>();

        private void frmExecOrders_Load(object sender, EventArgs e)
        {
            bsRecOrders.DataSource = lsRecOrders;
            dtgOrders.DataSource = bsRecOrders;
                       
            RefreshGrid();

            dgOrders.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgOrders.Columns["Quantity"]);
            dgOrders.GroupSummary.Add(SummaryItemType.Sum, "ExecQty", dgOrders.Columns["ExecQty"]);
            dgOrders.GroupSummary.Add(SummaryItemType.Sum, "ExecValue", dgOrders.Columns["ExecValue"]);

            dgOrders.Columns["IDPortfolio"].GroupIndex = 1;
            dgOrders.Columns["IDBook"].GroupIndex = 2;
            dgOrders.Columns["IDSection"].GroupIndex = 3;
            dgOrders.ExpandAllGroups();

            dgOrders.BestFitColumns();

        }

        private void RefreshGrid()
        {
            lsRecOrders.Clear();

            foreach (Order ord in OrderManager.Instance.ExecOrders)
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
