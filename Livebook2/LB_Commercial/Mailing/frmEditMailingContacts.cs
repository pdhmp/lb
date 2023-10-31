using LiveDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace LiveBook
{
    public partial class frmEditMailingContacts : LBForm
    {
        public Dictionary<string, MailContact> ContactList = new Dictionary<string, MailContact>();

        private void frmEditMailingContacts_Load(object sender, EventArgs e)
        {
            txtIdContact.BackColor = Color.White;
            Search(SearchTypes.Text);
        }

        public frmEditMailingContacts()
        {
            InitializeComponent();
        }

        private void rdText_CheckedChanged(object sender, EventArgs e)
        {
            ChangeRadios();
            Search(SearchTypes.Text);
        }

        private void rdPort_CheckedChanged(object sender, EventArgs e)
        {
            ChangeRadios();
            Search(SearchTypes.Port);
        }

        public void Search(SearchTypes searchType)
        {
            string SQLString = string.Empty;

            switch (searchType)
            {
                case SearchTypes.Text:


                    string WhereString = "";
                    string[] strFields = new string[1];

                    if (txtSearch.Text.Trim() != "")
                    {
                        strFields = txtSearch.Text.ToString().Split(' ');
                        for (int f = 0; f < strFields.Length; f++)
                        {
                            if (strFields[f] != "")
                            {
                                WhereString = WhereString +
                                    "AND (Contact_Mail like '%" + strFields[f] +
                                    "%' OR Contact_Name Like '%" + strFields[f] +
                                    "%')";
                            }
                        }
                    }

                    SQLString = "SELECT COALESCE(Contact_Name,Contact_Mail) as Display_Name , * FROM Tb000_Contacts WHERE ID_CONTACT > 0 " + WhereString + "  order by Display_Name";

                    break;
                case SearchTypes.Port:
                    SQLString = "SELECT COALESCE(Contact_Name,Contact_Mail) as Display_Name , * FROM Tb000_Contacts WHERE 0=0 " + GetPortWhere() + "  order by Display_Name";
                    break;
            }

            DataTable ReturnTable;

            using (newNestConn curConn = new newNestConn()) { ReturnTable = curConn.Return_DataTable(SQLString); }

            if (ReturnTable != null && ReturnTable.Rows.Count > 0)
            {
                foreach (DataRow curRow in ReturnTable.Rows)
                {
                    MailContact curContact = new MailContact(curRow);

                    if (!ContactList.ContainsKey(curContact.IdContact)) { ContactList.Add(curContact.IdContact, curContact); }
                }
            }

            lsClients.DataSource = ReturnTable;
            lsClients.DisplayMember = "Display_Name";
            lsClients.ValueMember = "Id_Contact";
        }

        string GetPortWhere()
        {
            if ((int)cmbPort.SelectedValue == 10) { return " AND DNAV_Bravo = 1 AND MReport_Bravo = 1 "; }
            else if ((int)cmbPort.SelectedValue == 80) { return " AND DNAV_LO = 1 AND MReport_LO = 1 "; }
            else if ((int)cmbPort.SelectedValue == 50) { return " AND DNAV_PREV = 1 AND MReport_PREV = 1 "; }
            else if ((int)cmbPort.SelectedValue == 55) { return " AND DNAV_ICATU = 1 AND MReport_ICATU = 1 "; }
            else if ((int)cmbPort.SelectedValue == 60) { return " AND DNAV_MultiEstrategia = 1 AND MReport_MultiEstrategia = 1 "; }
            return "";
        }

        public void ChangeRadios()
        {
            if (rdText.Checked)
            {
                this.cmbPort.Visible = false;
                this.txtSearch.Visible = true;
                this.txtSearch.Select();
                this.btnSearch.Visible = true;
            }
            else if (rdPort.Checked)
            {
                this.cmbPort.Visible = true;
                this.txtSearch.Visible = false;
                this.btnSearch.Visible = false;
            }
        }

        private void cmbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search(SearchTypes.Port);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.ToString().Length > 2) { Search(SearchTypes.Text); }
        }

        public void ChangeContact(MailContact curMailContact)
        {
            ClearAll();

            txtIdContact.Text = curMailContact.IdContact;
            txtName.Text = curMailContact.Name;
            txtMail.Text = curMailContact.Mail;

            chkDAcoes.Checked = curMailContact.DailyAcoes;
            chkDArb.Checked = curMailContact.DailyArb;
            chkDHedge.Checked = curMailContact.DailyHedge;
            chkDIcatu.Checked = curMailContact.DailyIcatu;
            chkDPrev.Checked = curMailContact.DailyPrev;

            chkMAcoes.Checked = curMailContact.MonthlyAcoes;
            chkMArb.Checked = curMailContact.MonthlyArb;
            chkMHedge.Checked = curMailContact.MonthlyHedge;
            chkMIcatu.Checked = curMailContact.MonthlyIcatu;
            chkMPrev.Checked = curMailContact.MonthlyPrev;

        }


        private void lsClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_Contact;

            if (int.TryParse(lsClients.SelectedValue.ToString(), out Id_Contact))
            {

                if (Id_Contact.ToString().Length > 1)
                {
                    try
                    {
                        ChangeContact(ContactList[Id_Contact.ToString()]);
                    }

                    catch { MessageBox.Show("ERRO 1"); }
                }
            }
        }

        #region Tools

        public enum SearchTypes
        {
            Text = 1, Port = 2
        }

        public void ClearAll()
        {
            txtIdContact.Clear();
            txtName.Clear();
            txtMail.Clear();

            chkDHedge.Checked = false;
            chkDPrev.Checked = false;
            chkDIcatu.Checked = false;
            chkDArb.Checked = false;
            chkDAcoes.Checked = false;
            chkMHedge.Checked = false;
            chkMPrev.Checked = false;
            chkMIcatu.Checked = false;
            chkMArb.Checked = false;
            chkMAcoes.Checked = false;
        }
        #endregion

        private void btnNewContact_Click(object sender, EventArgs e)
        {
            frmAddContact frmAdd = new frmAddContact();
            frmAdd.ShowDialog();

            if (frmAdd.NewIdContact > 0)
            {
                txtSearch.Text = string.Empty;
                rdText.Checked = true;
                rdText_CheckedChanged(sender, e);
                lsClients.SelectedValue = frmAdd.NewIdContact;
                txtName.Select();
            }

            frmAdd.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string Mail, Name, SQLString, IdContact;

            IdContact = txtIdContact.Text;

            if (txtMail.Text == "")
            { MessageBox.Show("E-mail não pode ser nulo."); return; }
            if (txtName.Text == "")
            { MessageBox.Show("Nome não pode ser nulo."); return; }

            string SETString = "SET ";

            SETString += " Contact_Name='" + txtName.Text + "' , ";
            SETString += " Contact_Mail='" + txtMail.Text + "' , ";

            SETString += " DNAV_Bravo=" + ((chkDAcoes.Checked) ? "1" : "0") + " , ";
            SETString += " DNAV_LO=" + ((chkDArb.Checked) ? "1" : "0") + " , ";
            SETString += " DNAV_MultiEstrategia=" + ((chkDHedge.Checked) ? "1" : "0") + " , ";
            SETString += " DNAV_PREV=" + ((chkDPrev.Checked) ? "1" : "0") + " , ";
            SETString += " DNAV_ICATU=" + ((chkDIcatu.Checked) ? "1" : "0") + " , ";

            SETString += " MReport_Bravo=" + ((chkMAcoes.Checked) ? "1" : "0") + " , ";
            SETString += " MReport_LO=" + ((chkMArb.Checked) ? "1" : "0") + " , ";
            SETString += " MReport_MultiEstrategia=" + ((chkMHedge.Checked) ? "1" : "0") + " , ";
            SETString += " MReport_PREV=" + ((chkMPrev.Checked) ? "1" : "0") + " , ";
            SETString += " MReport_ICATU=" + ((chkMIcatu.Checked) ? "1" : "0");

            SQLString = "UPDATE Tb000_Contacts " + SETString + " WHERE Id_Contact = " + IdContact + ";";

            using (newNestConn curConn = new newNestConn())
            {
                if (curConn.ExecuteNonQuery(SQLString) > 0)
                {
                    MessageBox.Show("Contato atualizado.");
                    ContactList.Remove(IdContact);
                    Search(SearchTypes.Text);
                    lsClients.SelectedValue = IdContact;
                }
                else { MessageBox.Show("Houve um erro ao atualizar o contato."); }
            }
        }
    }

    public class MailContact
    {
        public MailContact()
        {

        }
        public MailContact(DataRow curRow)
        {
            this.IdContact = curRow["Id_Contact"].ToString();
            this.Name = curRow["Contact_Name"].ToString();
            this.Mail = curRow["Contact_Mail"].ToString();

            this.DailyAcoes = (curRow["DNAV_Bravo"] != null && curRow["DNAV_Bravo"].ToString() != "") ? (bool)curRow["DNAV_Bravo"] : false;
            this.DailyArb = (curRow["DNAV_LO"] != null && curRow["DNAV_LO"].ToString() != "") ? (bool)curRow["DNAV_LO"] : false;
            this.DailyIcatu = (curRow["DNAV_ICATU"] != null && curRow["DNAV_ICATU"].ToString() != "") ? (bool)curRow["DNAV_ICATU"] : false;
            this.DailyHedge = (curRow["DNAV_MultiEstrategia"] != null && curRow["DNAV_MultiEstrategia"].ToString() != "") ? (bool)curRow["DNAV_MultiEstrategia"] : false;
            this.DailyPrev = (curRow["DNAV_PREV"] != null && curRow["DNAV_PREV"].ToString() != "") ? (bool)curRow["DNAV_PREV"] : false;

            this.MonthlyAcoes = (curRow["MReport_Bravo"] != null && curRow["MReport_Bravo"].ToString() != "") ? (bool)curRow["MReport_Bravo"] : false;
            this.MonthlyArb = (curRow["MReport_LO"] != null && curRow["MReport_LO"].ToString() != "") ? (bool)curRow["MReport_LO"] : false;
            this.MonthlyIcatu = (curRow["MReport_ICATU"] != null && curRow["MReport_ICATU"].ToString() != "") ? (bool)curRow["MReport_ICATU"] : false;
            this.MonthlyHedge = (curRow["MReport_MultiEstrategia"] != null && curRow["MReport_MultiEstrategia"].ToString() != "") ? (bool)curRow["MReport_MultiEstrategia"] : false;
            this.MonthlyPrev = (curRow["MReport_PREV"] != null && curRow["MReport_PREV"].ToString() != "") ? (bool)curRow["MReport_PREV"] : false;
        }

        public string IdContact;
        public string Name;
        public string Mail;

        public bool
         DailyArb,
         DailyAcoes,
         DailyIcatu,
         DailyPrev,
         DailyHedge;

        public bool
         MonthlyArb,
         MonthlyAcoes,
         MonthlyIcatu,
         MonthlyPrev,
         MonthlyHedge;
    }
}