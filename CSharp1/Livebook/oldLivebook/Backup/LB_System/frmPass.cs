using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;

namespace SGN
{
    public partial class frmPass : Form
    {
        Valida Valida = new Valida();
        public int Id_User;
        CarregaDados CargaDados = new CarregaDados();

        public frmPass()
        {
            InitializeComponent();
        }

         private void frmPass_Load(object sender, EventArgs e)
        {

        }

        private void txtConfirm_Leave(object sender, EventArgs e)
        {
            if (txtConfirm.Text != txtNewPass.Text)
            {
             MessageBox.Show("Different passwords.ReType!");
            }
        }

        private void cmdAlter_Click(object sender, EventArgs e)
        {
            string SQLString = "Select Senha from Tb014_Pessoas Where Id_Pessoa=" + Id_User;
            Boolean pass = false;
            if (this.txtAlterPass.Text == CargaDados.curConn.Execute_Query_String(SQLString))
            {
                pass = true;
            }
            else
            {
                MessageBox.Show("ReType you password!");
            }

            if (pass == true)
            {
                SQLString = "Update Tb014_Pessoas Set Senha= '" + txtConfirm.Text + "' Where Id_Pessoa=" + Id_User;
                int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,1);

                if (retorno == 1)
                {
                    MessageBox.Show("Password Changed!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error in change password!");
                }
              }
        
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtConfirm_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAlterPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == Keys.Enter.ToString())
            {
                txtNewPass.Select();
            }

        }

        private void txtNewPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == Keys.Enter.ToString())
            {
                txtConfirm.Select();
            }

        }

        private void txtConfirm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == Keys.Enter.ToString())
            {
                cmdAlter.Select();
            }

        }
     }
}