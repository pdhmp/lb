using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LiveTrade2
{
    public partial class frmLimits : LTForm
    {
        public frmLimits()
        {
            InitializeComponent();
        }

        private void frmLimits_Load(object sender, EventArgs e)
        {
            LoadLimits();
        }

        private void LoadLimits()
        {
            GlobalVars.Instance.ReadLimits();

            if (FIXConnections.Instance.curFixConn != null)
            {
                txtMaxOrderAmount.Text = FIXConnections.Instance.curFixConn.curLimits.MaxOrderAmount.ToString("#,##0.00");
                txtMaxOrderShares.Text = FIXConnections.Instance.curFixConn.curLimits.MaxOrderShares.ToString("#,##0");
                txtMaxTotalGross.Text  = FIXConnections.Instance.curFixConn.curLimits.MaxTotalGross.ToString("#,##0.00");
                txtMaxTotalNet.Text    = FIXConnections.Instance.curFixConn.curLimits.MaxTotalNet.ToString("#,##0.00");
                txtMaxTotalShares.Text = FIXConnections.Instance.curFixConn.curLimits.MaxTotalShares.ToString("#,##0");

                txtMaxDI1.Text = FIXConnections.Instance.curFixConn.curLimits.MaxContractsDI1.ToString();
                txtMaxDOL.Text = FIXConnections.Instance.curFixConn.curLimits.MaxContractsDOL.ToString();
                txtMaxIND.Text = FIXConnections.Instance.curFixConn.curLimits.MaxContractsIND.ToString();
            }
            else
            {
                MessageBox.Show("FIX not connected. Cannot edit limits", "Limits", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void UpdateLimits()
        {
            if (FIXConnections.Instance.curFixConn != null)
            {
                FIXConnections.Instance.curFixConn.curLimits.MaxOrderAmount = fromTextBox(txtMaxOrderAmount);
                FIXConnections.Instance.curFixConn.curLimits.MaxOrderShares = fromTextBox(txtMaxOrderShares);
                FIXConnections.Instance.curFixConn.curLimits.MaxTotalGross = fromTextBox(txtMaxTotalGross);
                FIXConnections.Instance.curFixConn.curLimits.MaxTotalNet = fromTextBox(txtMaxTotalNet);
                FIXConnections.Instance.curFixConn.curLimits.MaxTotalShares = fromTextBox(txtMaxTotalShares);

                FIXConnections.Instance.curFixConn.curLimits.MaxContractsDI1 = fromTextBox(txtMaxDI1);
                FIXConnections.Instance.curFixConn.curLimits.MaxContractsIND = fromTextBox(txtMaxIND);
                FIXConnections.Instance.curFixConn.curLimits.MaxContractsDOL = fromTextBox(txtMaxDOL);
            }

            StreamWriter sw = new StreamWriter(@"C:\LiveTrade\LTLimits.txt");

            sw.WriteLine("[MaxOrderAmount]=" + FIXConnections.Instance.curFixConn.curLimits.MaxOrderAmount);
            sw.WriteLine("[MaxOrderShares]=" + FIXConnections.Instance.curFixConn.curLimits.MaxOrderShares);
            sw.WriteLine("[MaxTotalGross]=" + FIXConnections.Instance.curFixConn.curLimits.MaxTotalGross);
            sw.WriteLine("[MaxTotalNet]=" + FIXConnections.Instance.curFixConn.curLimits.MaxTotalNet);
            sw.WriteLine("[MaxTotalShares]=" + FIXConnections.Instance.curFixConn.curLimits.MaxTotalShares);

            sw.WriteLine("[MaxContractsDI1]=" + FIXConnections.Instance.curFixConn.curLimits.MaxContractsDI1);
            sw.WriteLine("[MaxContractsIND]=" + FIXConnections.Instance.curFixConn.curLimits.MaxContractsIND);
            sw.WriteLine("[MaxContractsDOL]=" + FIXConnections.Instance.curFixConn.curLimits.MaxContractsDOL);
            sw.Close();

            MessageBox.Show("Limits Updated!", "Limits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private double fromTextBox(TextBox curBox)
        {
            string curText = curBox.Text;

            return double.Parse(curText.Replace(".", ""));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateLimits();
        }
    }
}
