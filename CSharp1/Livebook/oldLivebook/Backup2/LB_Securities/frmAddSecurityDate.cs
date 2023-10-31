using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SGN
{
    public partial class frmAddSecurityDate : Form
    {
        public frmAddSecurityDate()
        {
            InitializeComponent();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Tag = new DateTime(1900, 01, 01);
            this.Hide();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            this.Tag = dtpd_LastTradeDate.Value;
            this.Hide();
        }
    }
}