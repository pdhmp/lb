using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;

namespace ImportMellonSubRed
{
    public partial class frmFindClient : Form
    {
        public string UserAnswer = "";
        public int SelectedClient = -1;

        public frmFindClient()
        {
            InitializeComponent();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchName();
        }

        private void SearchName()
        {
            string SearchText = txtSearchString.Text;

            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT * FROM dbo.Tb751_Contacts WHERE Contact_Name LIKE '%" + txtSearchString.Text + "%'";
                NestDLL.FormUtils.LoadList(lstContacts, SQLString, "Id_Contact", "Contact_Name");
            }

            if (lstContacts.Items.Count > 0)
            {
                cmdUseSelected.Enabled = true;
            }
            else
            {
                cmdUseSelected.Enabled = false;
            }
        }

        private void txtSearchString_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchName();
            }
        }

        private void cmdUseSelected_Click(object sender, EventArgs e)
        {
            UserAnswer = "USE SELECTED";
            SelectedClient = int.Parse(lstContacts.SelectedValue.ToString());
            this.Hide();
        }

        private void cmdCreateNew_Click(object sender, EventArgs e)
        {
            UserAnswer = "CREATE";
            this.Hide();
        }

        private void frmFindClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UserAnswer == "")
            {
                MessageBox.Show(this, "You must select an Action!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
        }
    }
}