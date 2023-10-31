using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestDLL;

namespace LiveBook
{
    public partial class frmUpdateStrategy : Form
    {
        
        LB_Utils curUtils = new LB_Utils();
        public int Id_Order;
        public int Id_Book;
        public int Id_Section;
        public string Ticker;
        public string Price;
        public string Quantity;
        public int Id_User;

        public frmUpdateStrategy()
        {
            InitializeComponent();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUpdateStrategy_Load(object sender, EventArgs e)
        {
            this.cmbBook.SelectedValueChanged -= new System.EventHandler(this.cmbStrategy_SelectedValueChanged);
            NestDLL.FormUtils.LoadCombo(this.cmbBook, "Select Id_Book, Book from Tb400_Books", "Id_Book", "Book", Id_Book);
            this.cmbBook.SelectedValueChanged += new System.EventHandler(this.cmbStrategy_SelectedValueChanged);

            Carrega_Sub_Estrategy();
            cmbBook.Focus();
        }
        public void Carrega_Sub_Estrategy()
        {
                //SubEstrategia
            NestDLL.FormUtils.LoadCombo(this.cmbSection, "Select Id_Section,Sub_Strategy + ' - ' + Section as Section,Id_Book from dbo.VW_Book_Strategies WHERE Id_Book=" + Id_Book + " ORDER BY Section", "Id_Section", "Section", Id_Section);
        }

        private void cmdInsertAloc_Click(object sender, EventArgs e)
        {
            Update_Strategy();
        }

        void Update_Strategy()
        {
            string SQLString;
            int New_Book = Convert.ToInt32(cmbBook.SelectedValue.ToString());
            int New_Section = Convert.ToInt32(cmbSection.SelectedValue.ToString());

            SQLString = "UPDATE Tb012_Ordens SET [Id Book]=" + New_Book + ",[Id Section]=" + New_Section + " WHERE ID_Ordem =" + Id_Order;

            using (newNestConn curConn = new newNestConn())
            {
                int retorno = curConn.ExecuteNonQuery(SQLString, 1); ;

                if (retorno == 0)
                {
                    MessageBox.Show("Error on Update");
                }
                else
                {
                    this.Close();
                }
            }
        }

        private void cmbStrategy_SelectedValueChanged(object sender, EventArgs e)
        {
            Id_Book = Convert.ToInt32(cmbBook.SelectedValue);
            Id_Section = 99;
            Carrega_Sub_Estrategy();
        }

    }
}