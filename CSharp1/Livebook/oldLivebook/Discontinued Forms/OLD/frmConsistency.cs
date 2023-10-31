using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;


namespace SGN
{
    public partial class frmConsistency : Form
    {
        public frmConsistency()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2", "Id_Portfolio", "Port_Name", 1);

        }
        CarregaDados CargaDados = new CarregaDados();
        public int Id_User;

        public void Carrega_Grid()
        {
            string SQLString;

            SQLString = "select * from FCN_Calcula_DIF_POS_TRADES(5,'20071112','20071113') Where Difference <> 0";
            
            DataTable tablet = CargaDados.curConn.Return_DataTable(SQLString);
            dgConsistency.DataSource = tablet;

            if (dgConsistency.Rows.Count == 0)
            {

                MessageBox.Show("Nenhuma Diferença foi encontrada!");
            }
            else
            {
                this.Height = 410;
                dgConsistency.Visible = true;
            }
        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }
    }
}