using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NFIXConn;
using NCommonTypes;

namespace FlexTest
{
    public partial class Form1 : Form
    {
        FIXConn conn = null;


        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (conn == null) { Connect(); } else { Disconnect(); }
        }

        private void Connect()
        {
            string strAppPath = @"N:\TI\Projects\CSharp\Quant\FIX\FIXTest\FlexTest\config\QuickFix.cfg";
            conn = new FIXConn(strAppPath);

            lblStatus.Text = "Connected";
            lblStatus.ForeColor = System.Drawing.Color.Green;

            btnConnect.Text = "Disconnect";

            txtOrdID.Text = conn.ordID.ToString();

            conn.ReceivedFill += new EventHandler(conn_ReceivedFill);
        }

        void conn_ReceivedFill(object sender, EventArgs e)
        {
            ExecutionFillArgs execArgs = (ExecutionFillArgs)e;

            foreach (DataGridViewRow dr in dgvSendOrders.Rows)
            {
                if (dr.Cells[0].Value.ToString() == execArgs.ClOrdID)
                {
                    int remaining = Convert.ToInt32(dr.Cells[4].Value);
                    remaining -= execArgs.LastShares;
                    dr.Cells[4].Value = remaining.ToString();

                    if (remaining == 0)
                    {
                        dr.Cells[5].Value = true; 
                    }
                }
            }
        }

        private void Disconnect()
        {
            conn.Dispose();
            conn = null;

            lblStatus.Text = "Disconnected";
            lblStatus.ForeColor = System.Drawing.Color.Red;

            btnConnect.Enabled = false;

        }

        private void btnSetOrdID_Click(object sender, EventArgs e)
        {
            conn.ordID = Convert.ToInt32(txtOrdID.Text);
        }

        private void btnSendOrder_Click(object sender, EventArgs e)
        {
            string clordID = conn.sendOrder(Convert.ToInt32(txtTickerID.Text),
                                            Convert.ToInt32(txtQTY.Text), 
                                            Convert.ToDouble(txtPrice.Text),
                                            43,
                                            3,
                                            54);
            string[] row = { clordID,txtTickerID.Text, txtQTY.Text, txtPrice.Text, txtQTY.Text, "false" };

            dgvSendOrders.Rows.Add(row);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            conn.cancelAllOrders(Convert.ToInt32(txtTickerID.Text));
        }
    }
}