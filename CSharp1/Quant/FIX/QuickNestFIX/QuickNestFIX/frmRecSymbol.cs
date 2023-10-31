using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraEditors.Repository;
using NCustomControls;

namespace QuickNestFIX
{
    public partial class frmRecSymbol : QF_Form
    {
        public frmRecSymbol()
        {
            InitializeComponent();
            dtgOrders.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgOrders.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgOrders.LookAndFeel.SetSkinStyle("Blue");
        }

        BindingSource bsRecOrders = new BindingSource();
        List<OrdersBySymbol> lsRecOrders = new List<OrdersBySymbol>();
        List<SelectedItem> SelectedSymbols = new List<SelectedItem>();

        private void frmRecSymbol_Load(object sender, EventArgs e)
        {
            bsRecOrders.DataSource = lsRecOrders;
            dtgOrders.DataSource = bsRecOrders;

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            lsRecOrders.Clear();

            foreach (OrdersBySymbol ord in OrderManager.Instance.ReceivedBySymbol)
            {
                SelectedItem item = new SelectedItem();
                item.IDPortfolio = ord.IDPortfolio;
                item.Symbol = ord.Symbol;

                if (SelectedSymbols.IndexOf(item) > -1)
                {
                    ord.Selected = true;
                }

                lsRecOrders.Add(ord);
            }

            dgOrders.LayoutChanged();
            dgOrders.UpdateGroupSummary();
            dgOrders.RefreshData();
            dgOrders.Columns["Selected"].Width = 40;

            if (SelectedSymbols.Count > 0)
            {
                btnSelectAll.Text = "Unselect All Orders";
                btnSelectAll.Tag = false;
            }
            else
            {
                btnSelectAll.Text = "Select All Orders";
                btnSelectAll.Tag = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            
        }

        private void dtgOrders_DoubleClick(object sender, EventArgs e)
        {            
        }

        private void btnSendAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to SEND selected orders?", "Sending selected Orders", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (SelectedItem item in SelectedSymbols)
                {
                    OrderManager.Instance.MatchOrders(item.IDPortfolio, item.Symbol, chkAuction.Checked, false);
                }

                SelectedSymbols.Clear();

                RefreshGrid();
            }
        }

        private void btnMatch_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to MATCH selected orders?", "Matching selected Orders", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (SelectedItem item in SelectedSymbols)
                {
                    OrderManager.Instance.MatchOrders(item.IDPortfolio, item.Symbol, chkAuction.Checked, true);                    
                }

                SelectedSymbols.Clear();

                RefreshGrid();
            }
        }

        private void dgOrders_Click(object sender, EventArgs e)
        {
            GridView view = (GridView)sender;
            Point pt = view.GridControl.PointToClient(Control.MousePosition);
            DoRowClick(view, pt);
        }

        private void DoRowClick(GridView view, Point pt)
        {
            GridHitInfo hitInfo = view.CalcHitInfo(pt);
            if (hitInfo.InRow || hitInfo.InRowCell)
            {
                string colCaption = hitInfo.Column == null ? "N/A" : hitInfo.Column.GetCaption();

                if (colCaption == "Selected")
                {
                    int idPortfolio = int.Parse(dgOrders.GetRowCellValue(hitInfo.RowHandle, dgOrders.Columns["IDPortfolio"]).ToString());
                    string symbol = dgOrders.GetRowCellValue(hitInfo.RowHandle, dgOrders.Columns["Symbol"]).ToString();

                    SelectedItem item = new SelectedItem();
                    item.IDPortfolio = idPortfolio;
                    item.Symbol = symbol;

                    int index = SelectedSymbols.IndexOf(item);

                    if (index > -1)
                    {
                        SelectedSymbols.RemoveAt(index);
                    }
                    else
                    {
                        SelectedSymbols.Add(item);
                    }
                }
            }

            RefreshGrid();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            RefreshGrid();            
        }

        private void dtgOrders_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            if ((bool)btnSelectAll.Tag)
            {                
                foreach (OrdersBySymbol ord in lsRecOrders)
                {
                    SelectedItem item = new SelectedItem();
                    item.IDPortfolio = ord.IDPortfolio;
                    item.Symbol = ord.Symbol;

                    if (SelectedSymbols.IndexOf(item) == -1)
                    {
                        SelectedSymbols.Add(item);
                    }
                }
            }
            else
            {
                SelectedSymbols.Clear();
            }

            RefreshGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to Cancel selected orders?", "Cancelling selected Orders", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                foreach (SelectedItem item in SelectedSymbols)
                {
                    OrderManager.Instance.CancelOrders(item.IDPortfolio, item.Symbol);
                }

                SelectedSymbols.Clear();

                RefreshGrid();
            }
        }
    }
}
