using System;
using System.Windows.Forms;
using LiveDLL;


namespace LiveBook
{
    public partial class frmStock_Change_Maturity : Form
    {
        newNestConn curConn = new newNestConn();
        public int Loan_ID;
        public frmStock_Change_Maturity()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string StringSQL = "EXE Proc_Update_Maturity_Stock_Loan(" + Loan_ID + ",'" + dtpNewDate.Value.ToString("yyyyMMdd") + "'," + LiveDLL.NUserControl.Instance.User_Id + ")";

            int retorno = curConn.ExecuteNonQuery(StringSQL, 1);

            if (retorno != 1)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error on inserting loan close!");
                }
        }

    }
}