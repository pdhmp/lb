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
    public partial class frmAtivos : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        
        public frmAtivos()
        {
            InitializeComponent();
        }

        private void frmAtivos_Load(object sender, EventArgs e)
        {
            CargaDados.carregaList(this.lstTickers, "Select Id_Ativo, Simbolo from VW001_Ativos order by Simbolo", "Id_Ativo", "Simbolo");  
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            string pesq;

            if(txtpesq.ToString().Length() > 0) 
            {
                pesq = txtpesq.Text;
               pesq = pesq.Replace("  ", " ");
               pesq = pesq.Replace(" ", "+");

            }
            */
        }

        private void lstTickers_Click(object sender, EventArgs e)
        {
            MessageBox.Show(lstTickers.SelectedValue.ToString());
        }
     }
}