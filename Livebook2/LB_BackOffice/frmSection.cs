using System;
using System.Data;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmSection : Form
    {
        newNestConn curCoon = new newNestConn();
        string SQLString = "";
        string SQLString1 = "";
        DataSet curDataSet = new DataSet();
        int IdStrategy = 0;
        int IdSubStrategy = 0;

        public frmSection() { InitializeComponent(); }
        private void frmSection_Load(object sender, EventArgs e) { LoadStrategy(); }

        private void LoadStrategy()
        {
            SQLString = "SELECT [Id_Strategy] ,[Strategy] FROM [NESTDB].[dbo].[Tb402_Strategy] ORDER BY [Strategy] ";
            LiveDLL.FormUtils.LoadCombo(cmbStrategy, SQLString, "Id_Strategy", "Strategy");
        }
        private void LoadSubStrategy()
        {
            SQLString = "SELECT [Id_Sub_Strategy],[Sub_Strategy] " +
                        "FROM [NESTDB].[dbo].[Tb403_Sub_Strategy] " +
                        "WHERE Id_Strategy = " + IdStrategy + " " +
                        "ORDER BY [Sub_Strategy]";

            LiveDLL.FormUtils.LoadCombo(cmbSubStrategy, SQLString, "Id_Sub_Strategy", "Sub_Strategy");
        }
        private void LoadSection()
        {
            SQLString = "SELECT [Id_Section], " +
                        "[Section] +' - '+ B.Sub_Strategy   AS Section " +
                        "FROM [NESTDB].[dbo].[Tb404_Section] A  " +
                        "INNER JOIN NESTDB.dbo.Tb403_Sub_Strategy B ON A.Id_Sub_Strategy = B.Id_Sub_Strategy  " +
                        "ORDER BY CASE WHEN Id_Section <> 0 THEN Section END ";

            SQLString1 = "SELECT [Id_Section],[Section] FROM [NESTDB].[dbo].[Tb404_Section] WHERE [Id_Sub_Strategy] = " + IdSubStrategy + " ORDER BY [Section] ";

            LiveDLL.FormUtils.LoadList(ListSection, SQLString1, "Id_Section", "Section");
            LiveDLL.FormUtils.LoadCombo(cmbMirrorSection, SQLString, "Id_Section", "Section");
        }

        private void InsertSection()
        {
            string SectionName = txtSection.Text;

            if (SectionName != "")
            {
                SQLString = "SELECT COUNT (*) FROM [NESTDB].[dbo].[Tb404_Section] WHERE Section = '" + SectionName + "' ";
                if (curCoon.Return_Int(SQLString) > 0)
                {
                    MessageBox.Show("Section name already exist", "Error", MessageBoxButtons.OK);
                    return;
                }

                SQLString = "INSERT [NESTDB].[dbo].[Tb404_Section] Select MAX([Id_Section]) + 1, '" + SectionName + "', " + IdSubStrategy + " FROM [NESTDB].[dbo].[Tb404_Section] ; ";

                DialogResult userConfirmation = MessageBox.Show("Insert new Section: " + txtSection.Text + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (userConfirmation == DialogResult.Yes)
                {
                    int return1 = curCoon.ExecuteNonQuery(SQLString);

                    if (return1 > 0)
                    {
                        MessageBox.Show("Inserted!", "Sucess", MessageBoxButtons.OK);
                        return;
                    }
                }
                else { return; }

                MessageBox.Show("Not Inserted!", "Error", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Insert section name", "Error", MessageBoxButtons.OK);
            }
        }

        private void cmbStrategy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            IdStrategy = Convert.ToInt32(cmbStrategy.SelectedValue.ToString());
            LoadSubStrategy();
        }
        private void cmbSubStrategy_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (cmbSubStrategy.SelectedValue.ToString() != "")
            {
                IdSubStrategy = Convert.ToInt32(cmbSubStrategy.SelectedValue.ToString());
            }
            LoadSection();
        }
        private void cmbMirrorSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdMirrorSection = int.Parse(cmbMirrorSection.SelectedValue.ToString());

            if (IdMirrorSection > 0)
            {
                IdSubStrategy = curCoon.Return_Int("SELECT Id_Sub_Strategy FROM [NESTDB].[dbo].[Tb404_Section] Where Id_Section =" + IdMirrorSection);
                return;
            }

            IdSubStrategy = int.Parse(cmbSubStrategy.SelectedValue.ToString());
        }

        private void btnInsertSection_Click_1(object sender, EventArgs e)
        {
            InsertSection();
            txtSection.Text = "";
            LoadSection();
        }

        private void chkMirror_CheckedChanged(object sender, EventArgs e)
        {
            if (((CheckBox)sender).Checked)
            {
                cmbMirrorSection.Enabled = true;
                cmbStrategy.Visible = false;
                cmbSubStrategy.Visible = false;
                LoadSection();
                ListSection.Visible = false;
                groupBox1.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
            }
            else
            {
                cmbMirrorSection.Enabled = false;
                cmbStrategy.Visible = true;
                cmbSubStrategy.Visible = true;
                ListSection.Visible = true;
                groupBox1.Visible = true;
                label4.Visible = true;
                label5.Visible = true;

                LoadStrategy();
            }
        }
    }
}
