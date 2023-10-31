using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SGN.CargaDados;


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
            if (Id_usuario == 1)
            {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  Tb002_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira", 1);
            }
            else
            {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  VW_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira", 1);
            }

        }
        CarregaDados CargaDados = new CarregaDados();
        public int Id_usuario;

        public void Carrega_Grid()
        {
            string StringSQl;
            SqlDataAdapter dt = new SqlDataAdapter();
            DataTable tablet = new DataTable();
            StringSQl = "select * from FCN_Calcula_DIF_POS_TRADES(5,'20071112','20071113') Where Difference <> 0";
            dt = CargaDados.DB.Return_DataAdapter(StringSQl);
            dt.Fill(tablet);
            dgConsistency.DataSource = tablet;
            dt.Dispose();
            tablet.Dispose();
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