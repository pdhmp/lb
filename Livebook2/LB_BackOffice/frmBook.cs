using System.Windows.Forms;
using System;
using System.Data;
using LiveDLL;

namespace LiveBook
{
    public partial class frmBook : Form
    {
        string SQLString = "";
        string SQLString1 = "";
        DataSet curDataSet = new DataSet();
        newNestConn curCoon = new newNestConn();

        public frmBook() { InitializeComponent(); }

        private void frmBook_Load(object sender, EventArgs e) { LoadBook(); }

        private void LoadBook()
        {
            SQLString = "SELECT [Id_Book],[Book] FROM [NESTDB].[dbo].[Tb400_Books] ORDER BY [Book]  ; ";

            LiveDLL.FormUtils.LoadList(this.ListBook, SQLString, "Id_Book", "Book");
        }
        private void LoadValidBooks()
        {
            SQLString = " SELECT Id_Sub_Strategy,Sub_Strategy  FROM NESTDB.dbo.Tb403_Sub_Strategy " +
                        " WHERE Id_Sub_Strategy IN  " +
                        " (SELECT [Id_Sub_Strategy] FROM [NESTDB].[dbo].[Tb405_ValidBooks] WHERE Id_Book = " + ListBook.SelectedValue + ") ORDER BY  Sub_Strategy";

            SQLString1 =
                        "SELECT Id_Sub_Strategy, Sub_Strategy FROM " +
                        "( " +
                        "SELECT 0 AS Id_Sub_Strategy,'Select SubStrategy' AS Sub_Strategy UNION ALL  " +
                        "SELECT Id_Sub_Strategy,Sub_Strategy  FROM NESTDB.dbo.Tb403_Sub_Strategy " +
                        "WHERE Id_Sub_Strategy NOT IN  " +
                        "(SELECT [Id_Sub_Strategy] FROM [NESTDB].[dbo].[Tb405_ValidBooks] WHERE Id_Book = " + ListBook.SelectedValue + ") " +
                        ")x " +
                        "ORDER BY CASE WHEN Id_Sub_Strategy<>0 THEN Sub_Strategy END";

            LiveDLL.FormUtils.LoadList(ListValidBooks, SQLString, "Id_Sub_Strategy", "Sub_Strategy");
            LiveDLL.FormUtils.LoadCombo(cmbValidSubStrategy, SQLString1, "Id_Sub_Strategy", "Sub_Strategy");
        }

        private void InsertBook()
        {
            string BookName = txtNewBook.Text;

            if (txtNewBook.Text == "")
            {
                MessageBox.Show("Insert Book name", "Error", MessageBoxButtons.OK);
                return;
            }

            if (curCoon.Return_Int("SELECT COUNT(*)FROM [NESTDB].[dbo].[Tb400_Books] WHERE [Book] = '" + BookName + "'") > 0)
            {
                MessageBox.Show("This Book already exist", "Error", MessageBoxButtons.OK);
                return;
            }

            if (curCoon.ExecuteNonQuery("INSERT [NESTDB].[dbo].[Tb400_Books] SELECT MAX(Id_Book)+1 ,'" + BookName + "'FROM NESTDB.dbo.Tb400_Books") > 0)
            {
                MessageBox.Show("Inserted!", "Sucess", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show("Error on Insert", "Error", MessageBoxButtons.OK);
            }

            LoadBook();
        }
        private void InsertValidBook()
        {
            newNestConn curCoon = new newNestConn();
            if (int.Parse(cmbValidSubStrategy.SelectedValue.ToString()) == 0)
            {
                MessageBox.Show("Select a SubStrategy", "Error", MessageBoxButtons.OK);
                return;
            }

            if (curCoon.Return_Int("Select COUNT (*)  FROM [NESTDB].[dbo].[Tb405_ValidBooks] WHERE Id_Book = " + ListBook.SelectedValue + "	AND Id_Sub_Strategy = " + cmbValidSubStrategy.SelectedValue) > 0)
            {
                MessageBox.Show("This Book already valid on " + ListBook.ValueMember, "Error", MessageBoxButtons.OK);
                return;
            }

            DialogResult userConfirmation = MessageBox.Show("Allow Substrategy " + cmbValidSubStrategy.Text + " on Book " + ListBook.Text + "?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (userConfirmation == DialogResult.Yes)
            {
                if (curCoon.ExecuteNonQuery("INSERT [NESTDB].[dbo].[Tb405_ValidBooks] SELECT " + ListBook.SelectedValue + "," + cmbValidSubStrategy.SelectedValue) > 0)
                {
                    MessageBox.Show("Inserted!", "Sucess", MessageBoxButtons.OK);
                }
                else
                {
                    MessageBox.Show("Error on Insert", "Error", MessageBoxButtons.OK);
                }
            }
            else { return; }

            LoadBook();
            LoadValidBooks();
        }

        private void btnInsertBook_Click(object sender, System.EventArgs e)
        {
            InsertBook();

            txtNewBook.Text = "";

            LoadBook();
        }
        private void btnInsertValidBook_Click(object sender, EventArgs e)
        {
            InsertValidBook();
            LoadValidBooks();
        }

        private void ListBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadValidBooks();
        }
    }
}
