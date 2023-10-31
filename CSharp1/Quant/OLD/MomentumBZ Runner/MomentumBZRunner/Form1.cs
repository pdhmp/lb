using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MomentumBZRunner
{
    public partial class Form1 : Form
    {
        MomentumRunner Runner;
        Timer check = new Timer();

        bool initializing;
        
        public Form1()
        {
            initializing = true;
            InitializeComponent();
            Runner = new MomentumRunner();
            Runner.AutoSendOrders = MomentumBZRunner.Properties.Settings.Default.AutoSendOrders;
            chkAutoSendOrders.Checked = Runner.AutoSendOrders;
            check.Interval = 1000;
            check.Tick += new EventHandler(check_Tick);
            initializing = false;
        }

        void check_Tick(object sender, EventArgs e)
        {
            int col = dgvStagedOrders.FirstDisplayedScrollingColumnIndex;
            int row = dgvStagedOrders.FirstDisplayedScrollingRowIndex;            

            dgvStagedOrders.DataSource = Runner.ToDataTableStagedOrders();

            dgvStagedOrders.FirstDisplayedScrollingColumnIndex = (col >= 0 ? col : 0);
            dgvStagedOrders.FirstDisplayedScrollingRowIndex = (row >= 0 ? row : 0);            
        }
               

        private void btnRun_Click(object sender, EventArgs e)
        {            
            Runner.ID_Ticker_Template = Convert.ToInt32(txtIDTickerTemplate.Text);
            Runner.ID_Ticker_Composite = Convert.ToInt32(txtIDTickerComposite.Text);
            Runner.IniDate = Convert.ToDateTime(dtpIniDate.Value);
            Runner.EndDate = Convert.ToDateTime(dtpEndDate.Value);
            Runner.Window = Convert.ToInt32(txtWindow.Text);
            Runner.MedianWindow = Convert.ToInt32(txtMedianWindow.Text);
            Runner.StrategyNAV = Convert.ToDouble(txtStratNAV.Text);
            Runner.TargetVol = Convert.ToDouble(txtTargetVol.Text);            
            Runner.RunMomentum();

            check.Start();

        }        

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Runner.Stop();
        }

        private void btnSendOrders_Click(object sender, EventArgs e)
        {
            check.Stop();

            Runner.SendStagedOrders(chkAuction.Checked);

            check.Start();            
        }

        private void chkAutoSendOrders_CheckedChanged(object sender, EventArgs e)
        {
            if (!initializing)
            {
                if (chkAutoSendOrders.Checked == true)
                {
                    DialogResult yesNoCancel = MessageBox.Show("Do you want to auto send orders in the next sessions?", "Auto send orders", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (yesNoCancel)
                    {
                        case DialogResult.No:
                            Runner.AutoSendOrders = true;
                            break;
                        case DialogResult.Yes:
                            Runner.AutoSendOrders = true;
                            MomentumBZRunner.Properties.Settings.Default.AutoSendOrders = true;
                            MomentumBZRunner.Properties.Settings.Default.Save();
                            break;
                        case DialogResult.Cancel:
                        default:
                            chkAutoSendOrders.Checked = false;
                            break;
                    }
                }
                else
                {
                    Runner.AutoSendOrders = false;
                    MomentumBZRunner.Properties.Settings.Default.AutoSendOrders = false;
                    MomentumBZRunner.Properties.Settings.Default.Save();
                }
            }
        }               
    }
}