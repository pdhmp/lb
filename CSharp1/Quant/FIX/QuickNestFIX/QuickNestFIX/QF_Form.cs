using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QuickNestFIX
{
    public partial class QF_Form : Form
    {
        Utils curUtils = new Utils(4);

        public QF_Form()
        {
            InitializeComponent();
        }

        private void QM_Form_Load(object sender, EventArgs e)
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

            curUtils.LoadFormProperties(this, NestDLL.NUserControl.Instance.User_Id, this.Text);
            
        }

        private void QMForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            curUtils.SaveFormProperties(this, NestDLL.NUserControl.Instance.User_Id);
        }

        private void QM_Form_Resize(object sender, EventArgs e)
        {
            if (!GlobalVars.Instance.appStarting)
            {
                if (this.Left < 10) this.Left = 0;
                if (this.Top < 10) this.Top = 0;
            }
        }

        private void QM_Form_Move(object sender, EventArgs e)
        {
            if (!GlobalVars.Instance.appStarting)
            {
                if (this.Left < 10) this.Left = 0;
                if (this.Top < 10) this.Top = 0;
            }
        }
    }
}