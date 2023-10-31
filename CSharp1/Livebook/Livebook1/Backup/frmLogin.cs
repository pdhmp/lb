using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using System.Data.SqlClient;
using System.IO;

namespace SGN
{
    public partial class frmLogin : Form
    {
        Valida Valida = new Valida();
        public frmLogin()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new
            System.Globalization.CultureInfo("pt-BR");

            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
            int retorno = Busca_Login();
            if (retorno == 1)
            {
                txtpass.Select();
            }

        
        }


        private void CmdSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {

            Enter_sys();       
        }

        private void Enter_sys()
        {
            //String StrinSQl = "SET LANGUAGE 'US_ENGLISH'";
           // int lingua;
           // lingua = Valida.DB.Execute_Insert_Delete_Update(StrinSQl);
           frmMenu frmMenu = new frmMenu();

            if (txtLogin.Text != "" && txtpass.Text != "")
            {
               Salvar_Login();
                
                
                string Id_Usuario = Valida.GetUserId(txtLogin.Text, txtpass.Text);

                if (Id_Usuario != "")
                {
                    frmMenu.Id_usuario = Convert.ToInt32(Id_Usuario);
                    //frmMenu.frmMenu_Load(frmMenu, null);
                    frmMenu.InitializeComponent();
                    //frmMenu.Carrega_form();
                    frmMenu.Show();
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Login ou Password incorret!", "Attention");
                    txtpass.Text = "";
                    txtLogin.Select();
                }

            }  

        
        }

        private void txtLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == Keys.Enter.ToString())
            {
                txtpass.Select();
            }

        }

         private void txtpass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.ToString() == Keys.Enter.ToString())
            {
                Enter_sys();
            }

            if (e.KeyCode.ToString() == Keys.Escape.ToString())
            {
                this.txtLogin.Select();
            }
        }

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {

        }

        int Salvar_Login()
        {
            try
            {
                // create a writer and open the file
                TextWriter tw = new StreamWriter("c:\\windows\\login.txt");

                // write a line of text to the file
                tw.WriteLine(txtLogin.Text.ToString());

                // close the stream
                tw.Close();
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        int Busca_Login()
        {
            try
            {
                StreamReader tr = new StreamReader("c:\\windows\\login.txt");

                // read a line of text
                txtLogin.Text = tr.ReadLine();

                // close the stream
                tr.Close();
                return 1;
            }
            catch
            {
                return 0;
            }
        }


      }
}