using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using LiveBook.Business;
using System.Data.SqlClient;

namespace LiveBook
{
    public partial class frmVol : Form
    {
        
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
             "  inner join ( select Id_Portfolio, max(Data_PL) as Data_PL" +
             " From Tb025_Valor_PL group by Id_Portfolio) mxDrv " +
             " on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Portfolio = Z.Id_Portfolio Where Z.Id_Portfolio =" + Id_Portfolio;

            using (newNestConn curConn = new newNestConn())
            {
                Data_PL = curConn.Execute_Query_String(String_Campos);
                if (Data_PL == "")
                {
                    Data_PL = DateTime.Now.ToString("yyyy-MM-dd");
                    DateTime Data_PL1 = Convert.ToDateTime(Data_PL);
                }
                else
                {
                    DateTime Data_PL1 = Convert.ToDateTime(Data_PL);
                    Data_PL = Data_PL1.ToString("yyyyMMdd");
                }

                string Data2 = Convert.ToString(DateTime.Now.ToString("yyyyMMdd"));

                String_Campos = "Select coalesce(Port_Name_OBJeto,Id_Portfolio) as Port_Name_OBJeto from VW_Portfolios Where Id_Portfolio =  " + Id_Portfolio;
                string tempId_Portfolio = curConn.Execute_Query_String(String_Campos);


                String_Campos = "Select IdSecurity,Ticker from Tb001_Securities Where IdPriceTable = 7 AND ((Expiration IS NULL) OR Expiration = '19000101' OR (Expiration >= (CONVERT(VARCHAR, GETDATE()-5, 112))) ) order by Ticker";
                NestDLL.FormUtils.LoadCombo(cmbTicker, String_Campos, "IdSecurity", "Ticker", 99);
                Carrega_Grid();
            }
        }

        private void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();
            SQLString = "SET LANGUAGE 'US_ENGLISH'; Select * from VW_Volatility order by [Date Vol]";
            using (newNestConn curConn = new newNestConn())
            {
                tablep = curConn.Return_DataTable(SQLString);

                dgVol.DataSource = tablep;

                tablep.Dispose();
                dgVol.Columns["Save"].Visible = true;
                dgVol.Columns["Save"].DisplayIndex = 0;

                if (dgVol.Columns.Contains("Id Ticker") == true)
                {
                    dgVol.Columns["Id Ticker"].Visible = false;
                    dgVol.Columns["Id Ticker"].DisplayIndex = 1;
                }
                if (dgVol.Columns.Contains("Ticker") == true)
                {
                    dgVol.Columns["Ticker"].Visible = true;
                    dgVol.Columns["Ticker"].DisplayIndex = 2;
                    dgVol.Columns["Ticker"].Width = 87;
                    dgVol.Columns["Date Vol"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }

                if (dgVol.Columns.Contains("Volatility") == true)
                {
                    dgVol.Columns["Volatility"].Visible = true;
                    dgVol.Columns["Volatility"].DisplayIndex = 4;
                    dgVol.Columns["Volatility"].DefaultCellStyle.Format = "#,##0.00##;(#,##0.00##)";
                    dgVol.Columns["Volatility"].Width = 80;
                    dgVol.Columns["Date Vol"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dgVol.Columns.Contains("Date Vol") == true)
                {
                    dgVol.Columns["Date Vol"].Visible = true;
                    dgVol.Columns["Date Vol"].DisplayIndex = 3;
                    dgVol.Columns["Date Vol"].Width = 75;
                    dgVol.Columns["Date Vol"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                dgVol.DefaultCellStyle.BackColor = Color.FromArgb(210, 210, 210);
            }
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
            int retorno = Negocios.Insere_Vol(Convert.ToInt32(cmbTicker.SelectedValue), txtVol.Text.Replace(",", "."));
            if (retorno != 0)
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
            int Id_Ticker;
            string SQLString;
            string Data_PL;
            DateTime Data_PL2;
            string Volatilidade;
            int retorno;
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                Id_Ticker = Convert.ToInt32(dgVol.Rows[e.RowIndex].Cells["Id Ticker"].Value.ToString());
                Data_PL = dgVol.Rows[e.RowIndex].Cells["Date Vol"].Value.ToString();
                Volatilidade = dgVol.Rows[e.RowIndex].Cells["Volatility"].Value.ToString().Replace(",",".");
                
                //MessageBox.Show(Volatilidade.ToString());

                Data_PL2 = Convert.ToDateTime(Data_PL);
                Data_PL = Data_PL2.ToString("yyyyMMdd");

                //MessageBox.Show(Volatilidade);
                
                //SQLString = "UPDATE Tb027_Volatilidade Set Volatilidade =" + Volatilidade +
                //" Where Id_Volatilidade = " + Id_Vol;

                SQLString = "EXEC Proc_Insert_Price @ID_Ativo=" + Id_Ticker + ",@Valor=" + Volatilidade + ",@Data='" + Data_PL + "',@Tipo_Preco=40,@Source=4,@Automated=0";
                using (newNestConn curConn = new newNestConn())
                {
                    retorno = curConn.ExecuteNonQuery(SQLString, 1);

                    if (retorno != 0)
                    {
                        Carrega_Grid();

                    }
                    else
                    {
                        MessageBox.Show("Error in Inserte!");
                    }
                }
            }

        }


     }
}