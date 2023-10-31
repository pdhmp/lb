using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestSymConn;

namespace LiveTrade2
{
    public partial class ConnectedForm : LTForm
    {
        public NDistConn curNDistConn = new NDistConn(GlobalVars.Instance.ConnectIP, GlobalVars.Instance.ConnectPort);
        //public NDistConn curNDistConn = new NDistConn();
        public event EventHandler ClientClosedForm;


        public ConnectedForm()
        {
            InitializeComponent();
        }

        private void ConnectedForm_Load(object sender, EventArgs e)
        {

        }

        public void ReSubscribe()
        {
            curNDistConn.ReSubscribe();
        }

        private void ConnectedForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ClientClosedForm != null) ClientClosedForm(this, new EventArgs());
        }
    }
}
