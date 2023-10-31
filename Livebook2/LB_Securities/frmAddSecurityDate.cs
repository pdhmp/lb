using System;
using System.Windows.Forms;

namespace LiveBook
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