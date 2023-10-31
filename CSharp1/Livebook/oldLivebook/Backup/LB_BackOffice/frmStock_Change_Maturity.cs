using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using SGN.Business;
using SGN.Validacao;

namespace SGN
{
    public partial class frmStock_Change_Maturity : Form
    {
        CarregaDados CargaDados = new CarregaDados();
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
            string StringSQL = "EXE Proc_Update_Maturity_Stock_Loan(" + Loan_ID + ",'" + dtpNewDate.Value.ToString("yyyyMMdd") + "'," + NestDLL.NUserControl.Instance.User_Id + ")";

            int retorno = CargaDados.curConn.ExecuteNonQuery(StringSQL, 1);

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