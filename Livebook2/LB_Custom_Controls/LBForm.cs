using System;
using System.ComponentModel;

using System.Windows.Forms;
using LiveBook.Business;



namespace LiveBook
{
    public partial class LBForm : Form
    {
         
        protected Business_Class Negocios = new Business_Class();
        protected LB_Utils curUtils = new LB_Utils();
        public bool openDetached = false;
        

        public LBForm()
        {
           
            InitializeComponent();
        }

        public LBForm(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void LBForm_Load(object sender, EventArgs e)
        {
            foreach (object curControl in this.Controls)
            {
                if (curControl.GetType() == typeof(MyXtraGrid.MyGridControl))
                {
                    DevExpress.XtraGrid.GridControl curGrid = (DevExpress.XtraGrid.GridControl)curControl;

                    curGrid.LookAndFeel.UseDefaultLookAndFeel = false;
                    curGrid.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                    curGrid.LookAndFeel.SetSkinStyle("Blue");
                }
            }

            if (curUtils.LoadFormProperties((Form)sender, LiveDLL.NUserControl.Instance.User_Id, this.Text))
            {
                openDetached = true;
            }
            else
            {
                openDetached = false;
            }
        }

        private void LBForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            curUtils.SaveFormProperties(this, LiveDLL.NUserControl.Instance.User_Id);
        }

        private void LBForm_Resize(object sender, EventArgs e)
        {
            if (!GlobalVars.Instance.appStarting)
            {
                if (this.Left < 10) this.Left = 0;
                if (this.Top < 10) this.Top = 0;
            }
        }

        private void LBForm_Move(object sender, EventArgs e)
        {
            if (!GlobalVars.Instance.appStarting)
            {
                if (this.Left < 10 && this.Left > 0) this.Left = 0;
                if (this.Top < 10 && this.Top > 0) this.Top = 0;
            }
        }
    }
}
