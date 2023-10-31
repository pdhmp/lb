using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.CargaDados;

namespace SGN
{
    public partial class frmCalculateDelta : Form
    {
        CarregaDados carga = new CarregaDados();

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
            string SQL_String = "EXEC [Proc_ReCalcula_Delta_Copia_Tb066_Calculo_Vol_Data] @Date_Calc = '" + dtpDate.Value.ToString("yyyyMMdd") + "'";

            int retorno = carga.DB.Execute_Insert_Delete_Update(SQL_String);

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
    }
}