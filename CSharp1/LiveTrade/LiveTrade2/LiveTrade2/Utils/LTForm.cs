using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace LiveTrade2
{
    public partial class LTForm : Form
    {
        Utils curUtils = new Utils(5);

        public LTForm()
        {
            InitializeComponent();
        }

        public LTForm(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void LTForm_Load(object sender, EventArgs e)
        {
            foreach (object curControl in this.Controls)
            {
                if (curControl.GetType() == typeof(NCustomControls.MyGridControl))
                {
                    DevExpress.XtraGrid.GridControl curGrid = (DevExpress.XtraGrid.GridControl)curControl;

                    curGrid.LookAndFeel.UseDefaultLookAndFeel = false;
                    curGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                    curGrid.LookAndFeel.SetSkinStyle("Blue");
                }
            }
            
            curUtils.LoadFormProperties((Form)sender, NestDLL.NUserControl.Instance.User_Id, this.Text);
            
        }

        private void LBForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            curUtils.SaveFormProperties(this, NestDLL.NUserControl.Instance.User_Id);
        }

        private void LTForm_Resize(object sender, EventArgs e)
        {
            if (!GlobalVars.Instance.appStarting)
            {
                if (this.Left < 10) this.Left = 0;
                if (this.Top < 10) this.Top = 0;
            }
        }

        private void LTForm_Move(object sender, EventArgs e)
        {
            if (!GlobalVars.Instance.appStarting)
            {
                if (this.Left < 10) this.Left = 0;
                if (this.Top < 10) this.Top = 0;
            }
        }
    }
}
