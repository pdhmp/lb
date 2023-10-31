using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.IO;
using System.Reflection;

using NestDLL;

namespace LiveTrade2
{
    public partial class frmLogin : Form
    {
        Utils curUtils = new Utils(5);
        public int Id_User;
        public string LBVersion = "";
        public frmLogin()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new
            System.Globalization.CultureInfo("pt-BR");

            InitializeComponent();


            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                System.Deployment.Application.ApplicationDeployment ad = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                LBVersion = ad.CurrentVersion.ToString();
            }
            else
            {
                Version version = this.GetType().Assembly.GetName().Version;
                LBVersion = version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision;
            }
            this.Text = "LiveTrade " + LBVersion;
            lblVersion.Text = "Version: " + LBVersion;
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

            Enter_sys(sender,e);       
        }

        private void Enter_sys(object sender, EventArgs e)
        {
            txtLogin.Enabled = false;
            txtpass.Enabled = false;
            cmdOK.Enabled = false;
            CmdSair.Enabled = false;

            if (txtLogin.Text != "")
            {
               Salvar_Login();
                
                
                string curUser = GetUserId(txtLogin.Text, txtpass.Text);

                if (curUser != "")
                {
                    NUserControl.Instance.User_Id = Convert.ToInt32(curUser);
                    this.Visible = false;
                }
                else
                {
                    MessageBox.Show("Login ou Password incorrect!", "Attention");

                    txtLogin.Enabled = true;
                    txtpass.Enabled = true;
                    cmdOK.Enabled = true;
                    CmdSair.Enabled = true;

                    txtpass.Text = "";
                    txtpass.Select();
                }

            }  
        }

        public String GetUserId(string usuario, string senha)
        {
            string SQLString;
            SQLString = "exec Get_User '" + usuario + "','" + senha + "'";

            using (newNestConn curConn = new newNestConn())
            {
                return curConn.Execute_Query_String(SQLString);
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
                Enter_sys(sender, e);
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
                TextWriter tw = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\login.txt");

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
                StreamReader tr = new StreamReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\login.txt");

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

        private void label2_Click(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {

        }


      }
}