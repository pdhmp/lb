using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;

namespace SGN
{
    public partial class frmCalculateDelta : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        public int Id_User;


        public frmCalculateDelta()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            string SQLString = "EXEC [Proc_ReCalcula_Delta_Copia_Tb066_Calculo_Vol_Data] @Date_Calc = '" + dtpDate.Value.ToString("yyyyMMdd") + "'";

            int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);

            this.Cursor = Cursors.Default;

            if (retorno == 0)
            {
                MessageBox.Show("Error on Calculate!");
            }
            else
            {
                MessageBox.Show("Recalculate Delta OK!");
            }

        }

        private void frmCalculateDelta_Load(object sender, EventArgs e)
        {

        }
     }
}