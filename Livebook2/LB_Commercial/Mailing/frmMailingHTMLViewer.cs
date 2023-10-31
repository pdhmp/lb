using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LiveBook
{
    public partial class frmMailingHTMLViewer : LBForm
    {
        public bool SendMail = false;

        RepType Report;
        int IdPortfolio;

        public frmMailingHTMLViewer()
        {
            InitializeComponent();
        }

        public frmMailingHTMLViewer(RepType report, int IdPortfolio)
        {
            InitializeComponent();
            this.Report = report;
            this.IdPortfolio = IdPortfolio;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMail = true;
            this.Hide();
        }

        private void frmMailingHTMLViewer_Load(object sender, EventArgs e)
        {
            //assign a contextmenustrip
            listboxContextMenu = new ContextMenuStrip();
            listboxContextMenu.Opening += new CancelEventHandler(listboxContextMenu_Opening);
            lstSubscribers.ContextMenuStrip = listboxContextMenu;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        public string GetFieldName()
        {
            string FieldName = "";

            if (Report == RepType.Daily || Report == RepType.Correction || Report == RepType.Delay) FieldName += "DNAV_";
            if (Report == RepType.Monthly) FieldName += "MReport_";

            switch (IdPortfolio)
            {
                case 4: return "";
                case 10: return FieldName + "Bravo";
                case 38: return FieldName + "ARB";
                case 62: return FieldName + "ARB30";
                case 50: return FieldName + "PREV";
                case 55: return FieldName + "ICATU";
                case 60: return FieldName + "MultiEstrategia";
                case 80: return FieldName + "LongOnly";

                default: return "";
            }
        }

        private ContextMenuStrip listboxContextMenu;

        private void lstSubscribers_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //select the item under the mouse pointer
                lstSubscribers.SelectedIndex = lstSubscribers.IndexFromPoint(e.Location);
                if (lstSubscribers.SelectedIndex != -1)
                {
                    listboxContextMenu.Show();
                }
            }
        }

        private void listboxContextMenu_Opening(object sender, CancelEventArgs e)
        {
            //clear the menu and add custom items
            listboxContextMenu.Items.Clear();
            listboxContextMenu.Items.Add(string.Format("Delete {0}", lstSubscribers.SelectedItem.ToString()));
            listboxContextMenu.Click += new EventHandler(mnuDelete_Click);
        }

        private void mnuDelete_Click(object sender, EventArgs e)
        {
            string Mail = listboxContextMenu.SourceControl.Text;

            DialogResult userConfirmation = MessageBox.Show("Remove " + Mail + " from this list?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (userConfirmation == DialogResult.Yes)
            {
                using (LiveDLL.newNestConn curConn = new LiveDLL.newNestConn())
                {
                    curConn.ExecuteNonQuery("UPDATE Tb000_Contacts SET " + GetFieldName() + " = 0 WHERE Contact_Mail = '" + Mail + "'");
                    lstSubscribers.Items.Remove(Mail);
                    MessageBox.Show("Contact removed from list");
                }
            }
        }

        private void lstSubscribers_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
