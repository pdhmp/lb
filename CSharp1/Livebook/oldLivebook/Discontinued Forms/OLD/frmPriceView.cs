using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using SGN.Validacao;
using SGN.Business;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace SGN
{

    public partial class frmPriceView : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        DataTable tablep = new DataTable();
        
        public int Id_User;

        public frmPriceView()
        {
            InitializeComponent();
        }
        /*
        private void Form1_Load(object sender, EventArgs e)
        {
            radMonth.Checked = true;
            Carrega_Lista();
            //Carrega_Combos();
        }

        private void Carrega_Combos()
        {
            //CargaDados.carregacombo(cmbPrice_Type, "SELECT -1 AS Id_Tipo_Preco, 'All' AS Preco UNION ALL SELECT Id_Tipo_Preco, Preco FROM dbo.Tb116_Tipo_Preco ORDER BY Preco", "Id_Tipo_Preco", "Preco", 1);
            //CargaDados.carregacombo(cmbSource, "SELECT -1 AS Id_Sistemas_Informacoes, 'All' AS Descricao UNION ALL SELECT Id_Sistemas_Informacoes, Descricao from dbo.Tb102_Sistemas_Informacoes ORDER BY Descricao", "Id_Sistemas_Informacoes", "Descricao", 1);
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            Carrega_Lista();
        }

        public void Carrega_Lista()
        {

            string strWhere = "";
            string[] strFields = new string[1];

            string SQLString = "Select convert(varchar(10),Id_Ativo,102) + '-' + convert(char(5),Id_Price_Table,102) " +
                " as Id_Ativo, Simbolo from Tb001_Ativos WHERE Id_Ativo>0 ";

            if (chkDiscontinued.Checked != true)
            {
                SQLString = SQLString + " AND ((Vencimento >= convert(varchar,GETDATE(),112)) OR (Vencimento IS NULL))";
            }

            if (txtsearch.Text.Trim() != "")
            {
                strFields = txtsearch.Text.ToString().Split(' ');
                for (int f = 0; f < strFields.Length; f++)
                {
                    if (strFields[f] != "") { strWhere = strWhere + " AND (Simbolo like '%" + strFields[f] + "%' OR Nome Like '%" + strFields[f] + "%' OR Id_Ativo like '%" + strFields[f].ToString() + "%')"; }
                }
            }

            SQLString = SQLString + strWhere + " order by Simbolo";

            da.Fill(table);
            lstAtivos.DataSource = table;
            lstAtivos.DisplayMember = "Simbolo";
            lstAtivos.ValueMember = "Id_Ativo";
            //lstAtivos.SelectedIndexChanged(null, null);
        }



        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch.Text.ToString().Length > 2) { Carrega_Lista(); };
        }

        private void lstAtivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] parte;
            int Id_Ticker;

            parte = lstAtivos.SelectedValue.ToString().Split('-');

            if (parte.Length > 1)
            {
                Id_Ticker = Convert.ToInt32(parte[0]);
            }
            else
            {
                return;
            }

            string PriceTableName = Valida.Retorna_Tabela_Preco_Trade(Id_Ticker, 0);

            CargaDados.carregacombo(cmbPrice_Type, "SELECT -1 AS Id_Tipo_Preco, 'All' AS Preco UNION ALL SELECT A.Id_Tipo_Preco, A.Preco FROM dbo.Tb116_Tipo_Preco A INNER JOIN " + PriceTableName + " B ON A.Id_Tipo_Preco=B.Tipo_Preco GROUP BY A.Id_Tipo_Preco, A.Preco ORDER BY Preco", "Id_Tipo_Preco", "Preco", 1);
            CargaDados.carregacombo(cmbSource, "SELECT -1 AS Id_Sistemas_Informacoes, 'All' AS Descricao UNION ALL SELECT Id_Sistemas_Informacoes, Descricao from dbo.Tb102_Sistemas_Informacoes A INNER JOIN " + PriceTableName + " B ON A.Id_Sistemas_Informacoes=B.Source GROUP BY A.Id_Sistemas_Informacoes, A.Descricao ORDER BY Descricao", "Id_Sistemas_Informacoes", "Descricao", 1);
            Carrega_Grid();
        }

        private void cmdInsertNew_Click(object sender, EventArgs e)
        {
            string[] parte;
            int Id_Ticker;

            parte = lstAtivos.SelectedValue.ToString().Split('-');

            if (parte.Length > 1)
            {
                Id_Ticker = Convert.ToInt32(parte[0]);
            }
            else
            {
                return;
            }

            frmInsertPrice Preco = new frmInsertPrice();

            //Preco.iniLoad(); 
            Preco.SetTicker(Id_Ticker);
            Preco.ShowDialog();
            Carrega_Grid();
        }

        private void chkDiscontinued_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Lista();
        }

        private void radToday_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void radWeek_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void radMonth_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void radYear_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void cmbPrice_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void cmbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }
         */
    }
}