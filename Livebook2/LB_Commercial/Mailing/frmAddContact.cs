using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmAddContact : LBForm
    {

        public int NewIdContact;

        public frmAddContact() { InitializeComponent(); }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string Mail, Name, SQLString;

            if (txtMail.Text == "" || txtName.Text == "")
            { MessageBox.Show("Nenhum campo pode ser nulo."); return; }
            else
            { Mail = txtMail.Text; Name = txtName.Text; }

            SQLString = "INSERT Tb000_Contacts (Contact_Name,Contact_Mail) SELECT '" + Name + "','" + Mail + "' ; SELECT @@IDENTITY";

            using (newNestConn curConn = new newNestConn()) { NewIdContact = curConn.Return_Int(SQLString); }

            if (NewIdContact > 0)
            {
                MessageBox.Show("Novo contato inserido.");
                this.Hide();
            }
            else
            {
                MessageBox.Show("Houve um erro ao inserir o contato.");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddContact_Load(object sender, EventArgs e)
        {
            NewIdContact = 0;
        }
    }
}
