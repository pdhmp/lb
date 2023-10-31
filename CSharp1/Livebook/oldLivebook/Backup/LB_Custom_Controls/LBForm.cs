using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using System.Windows.Forms;
using NestDLL;
using SGN.Business;
using SGN.Validacao;


namespace SGN
{
    public partial class LBForm : Form
    {
        protected CarregaDados CargaDados = new CarregaDados();
        protected Business_Class Negocios = new Business_Class();
        protected Valida Valida = new Valida();

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
        }

        private void LBForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Valida.Save_Properties_Form(this, 0);
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
