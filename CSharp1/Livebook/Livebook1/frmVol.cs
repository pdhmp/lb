using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.CargaDados;
using SGN.Business;
using System.Data.SqlClient;

namespace SGN
{
    public partial class frmVol : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        public int Id_Portfolio;

        public frmVol()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string String_Campos;
            string Data_PL;
            String_Campos = "select Z.Data_PL as Data_PL From Tb025_Valor_PL Z" +
             "  inner join ( select Id_Carteira, max(Data_PL) as Data_PL" +
             " From Tb025_Valor_PL group by Id_Carteira) mxDrv " +
             " on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Carteira = Z.Id_Carteira Where Z.Id_Carteira =" + Id_Portfolio;
            Data_PL = CargaDados.DB.Execute_Query_String(String_Campos);
            if (Data_PL == "")
            {
                Data_PL = DateTime.Now.ToString("yyyyMMdd");
                DateTime Data_PL1 = Convert.ToDateTime(Data_PL);
            }
            else
            {
                DateTime Data_PL1 = Convert.ToDateTime(Data_PL);
                Data_PL = Data_PL1.ToString("yyyyMMdd");
            }

            string Data2 = Convert.ToString(DateTime.Now.ToString("yyyyMMdd"));

            String_Campos = "Select coalesce(Carteira_OBJeto,Id_Carteira) as Carteira_OBJeto from VW_Carteiras Where Id_Carteira =  " + Id_Portfolio;
            string Id_Carteira = CargaDados.DB.Execute_Query_String(String_Campos);


            String_Campos = "Select Id_Ativo as [Id Ticker],Simbolo as Ticker from VW001_Ativos Where id_tipo_ativo = 7 order by Simbolo";
            CargaDados.carregacombo(cmbTicker, String_Campos, "Id Ticker", "Ticker", 99);
            Carrega_Grid();
        }

        private void Carrega_Grid()
        {
        string StringSQl;
        SqlDataAdapter dp = new SqlDataAdapter();
        DataTable tablep = new DataTable();
        StringSQl = "SET LANGUAGE 'US_ENGLISH'; Select * from VW_Volatility order by [Date Vol]";
            dp = CargaDados.DB.Return_DataAdapter(StringSQl);
            dp.Fill(tablep);
            dgVol.DataSource = tablep;
            dp.Dispose();
            tablep.Dispose();
            dgVol.Columns["Save"].Visible = true;
            dgVol.Columns["Save"].DisplayIndex = 0;

            if (dgVol.Columns.Contains("Id Ticker") == true)
            {
                dgVol.Columns["Id Ticker"].Visible = false;
                dgVol.Columns["Id Ticker"].DisplayIndex = 1;
            }
            if (dgVol.Columns.Contains("id Volatility") == true)
            {
                dgVol.Columns["id Volatility"].Visible = false;
                dgVol.Columns["id Volatility"].DisplayIndex = 2;
            }
            if (dgVol.Columns.Contains("Ticker") == true)
            {
                dgVol.Columns["Ticker"].Visible = true;
                dgVol.Columns["Ticker"].DisplayIndex = 3;
                dgVol.Columns["Ticker"].Width = 87;
                dgVol.Columns["Date Vol"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dgVol.Columns.Contains("Volatility") == true)
            {
                dgVol.Columns["Volatility"].Visible = true;
                dgVol.Columns["Volatility"].DisplayIndex = 5;
                dgVol.Columns["Volatility"].DefaultCellStyle.Format = "#,##0.00##;(#,##0.00##)";
                dgVol.Columns["Volatility"].Width = 80;
                dgVol.Columns["Date Vol"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (dgVol.Columns.Contains("Date Vol") == true)
            {
                dgVol.Columns["Date Vol"].Visible = true;
                dgVol.Columns["Date Vol"].DisplayIndex = 4;
                dgVol.Columns["Date Vol"].Width = 75;
                dgVol.Columns["Date Vol"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dgVol.DefaultCellStyle.BackColor = Color.FromArgb(210, 210, 210);
        }


         private void txtVol_Leave(object sender, EventArgs e)
        {
            if (this.txtVol.Text != "")
            {
                decimal vol = Convert.ToDecimal(txtVol.Text);
                this.txtVol.Text = vol.ToString("##,##0.00############");
            }
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
         int retorno =  Negocios.Insere_Vol(Convert.ToInt32(cmbTicker.SelectedValue),txtVol.Text.Replace(",","."));
         if (retorno == 1)
         {
             MessageBox.Show("Inserted!");
             Carrega_Grid();
         }
         else
         {
             MessageBox.Show("Error in Inserte!");
 
         }
        
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgVol_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Id_Vol;
            string StringSQl;
            string Data_PL;
            DateTime Data_PL2;
            string Volatilidade;
            int retorno;
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                Id_Vol = Convert.ToInt32(dgVol.Rows[e.RowIndex].Cells["id Volatility"].Value.ToString());
                Data_PL = dgVol.Rows[e.RowIndex].Cells["Date Vol"].Value.ToString();
                Volatilidade = dgVol.Rows[e.RowIndex].Cells["Volatility"].Value.ToString().Replace(",",".");
                
                //MessageBox.Show(Volatilidade.ToString());

                Data_PL2 = Convert.ToDateTime(Data_PL);
                Data_PL = Data_PL2.ToString("dd/MM/yyyy");

                //MessageBox.Show(Volatilidade);
                
                StringSQl = "UPDATE Tb027_Volatilidade Set Volatilidade =" + Volatilidade +
                " Where Id_Volatilidade = " + Id_Vol;

                retorno = CargaDados.DB.Execute_Insert_Delete_Update(StringSQl);

                if (retorno != 0)
                {
                   // Carrega_Grid();
                }
                else
                {
                    MessageBox.Show("Error in Inserte!");
                }
            }

        }


     }
}