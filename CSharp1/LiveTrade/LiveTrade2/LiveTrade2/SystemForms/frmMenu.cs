using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LiveTrade2.SystemForms
{
    public partial class frmMenu : Form
    {
        public frmOptionChain OptionChain;

        public frmMenu()
        {
            InitializeComponent();
        }


        #region OpenForms
        private void FormOpener(Form FormToOpen)
        {
            FormToOpen.Icon = this.Icon;
            FormToOpen.MdiParent = this;

            FormToOpen.Show();
            FormToOpen.Activate();
        }


        private void positionsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (OptionChain == null || OptionChain.IsDisposed)
            {
                OptionChain = new frmOptionChain("PETR4");
                FormOpener(OptionChain);
            }
        }


        private void EditLimitToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void QuickChartToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        #endregion OpenForms

    }
}
