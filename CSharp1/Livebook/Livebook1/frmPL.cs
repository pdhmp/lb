using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Business;
using SGN.CargaDados;
using System.Data.SqlClient;
namespace SGN
{
    public partial class frmpl : Form
    {
        Business_Class Negocios = new Business_Class();
        CarregaDados CargaDados = new CarregaDados();
        public int Id_usuario;

        public frmpl()
        {
            InitializeComponent();
            Carrega_Grid();
        }

        private void frmPL_Load(object sender, EventArgs e)
        {
            if (Id_usuario == 1)
            {
            CargaDados.carregacombo(this.CmbPortfolio, "select Id_Carteira,Carteira from Tb002_Carteiras", "Id_Carteira", "Carteira");
            }
            else
            {
            CargaDados.carregacombo(this.CmbPortfolio, "select Id_Carteira,Carteira from VW_Carteiras", "Id_Carteira", "Carteira");
            }
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string String_Campos;

            String_Campos = " select Id_Carteira from VW_PL_Carteira where Id_Carteira = " + this.CmbPortfolio.SelectedValue.ToString() + " and Data_PL = '" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "'";

                if (txtPL.Text == "")
                {
                    MessageBox.Show("Insert Valid value!");
                }
                else
                {
                    String_Campos = "select Z.Data_PL as Data_PL From Tb025_Valor_PL Z" +
                    "  inner join ( select Id_Carteira, max(Data_PL) as Data_PL" +
                    " From Tb025_Valor_PL group by Id_Carteira) mxDrv " +
                    " on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Carteira = Z.Id_Carteira Where Z.Id_Carteira =" + this.CmbPortfolio.SelectedValue.ToString();

                     string Data_INI = CargaDados.DB.Execute_Query_String(String_Campos);

                    if (Negocios.Insere_PL(Convert.ToInt32(this.CmbPortfolio.SelectedValue.ToString()), Convert.ToDateTime(this.dtpDataPL.Value.ToString("dd/MM/yyyy")), Convert.ToDecimal(this.txtPL.Text)) != 0)
                    {
                        if (Convert.ToInt32(this.CmbPortfolio.SelectedValue.ToString()) == 10)
                        {
                        Negocios.Insere_PL(11, Convert.ToDateTime(this.dtpDataPL.Value.ToString("dd/MM/yyyy")), Convert.ToDecimal(this.txtPL.Text));
                        }
                       //  this.Close();

                        if (Convert.ToInt32(this.CmbPortfolio.SelectedValue.ToString()) == 35)
                        {
                            Negocios.Insere_PL(42, Convert.ToDateTime(this.dtpDataPL.Value.ToString("dd/MM/yyyy")), Convert.ToDecimal(this.txtPL.Text));
                        }
                        Carrega_Grid();

                    DateTime Data_INI2;

                    int Consolidado = Convert.ToInt32(CargaDados.DB.Execute_Query_String("Select Consolidada from Tb002_Carteiras where Id_Carteira=" + CmbPortfolio.SelectedValue.ToString()));
                    int z;
                    string Id_Portfolio = this.CmbPortfolio.SelectedValue.ToString();

                    Data_INI2 = Convert.ToDateTime(Data_INI);

                    Data_INI = Data_INI2.ToString("yyyyMMdd");
                    
                        if (Data_INI != "" && Consolidado == 0)
                    {

                        String_Campos = "exec Proc_Copy_Positions_Closed @Id_Portfolio=" + Id_Portfolio + ",@Initial_Date='" + Data_INI + "',@End_Date='" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "'";
                        z = CargaDados.DB.Execute_Insert_Delete_Update(String_Campos);
                        if (z == 0)
                        {
                            String_Campos = "delete from Tb025_Valor_PL Where Data_Pl=Data_INI and Id_Carteira=" + Id_Portfolio ;
                            CargaDados.DB.Execute_Insert_Delete_Update(String_Campos);
                            
                            MessageBox.Show("Error on Copy Positions!");
                        }
                    }
                    if (Data_INI != "")
                    {
                        String_Campos = " exec Insert_Tb215_History_Position @Id_Portfolio=" + Id_Portfolio + ",@Initial_Date='" + Data_INI + "',@End_Date='" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "'";
                        z = CargaDados.DB.Execute_Insert_Delete_Update(String_Campos);
                        if (z == 0)
                        {
                            MessageBox.Show("Error on Copy History!");
                        }
                    }

                }
                    else
                    {
                        MessageBox.Show("Error in the insertion, verifies the data and dates!");
                    }
                }
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPl_Leave(object sender, EventArgs e)
        {
            if (txtPL.Text != "")
            {
                decimal Preco = Convert.ToDecimal(txtPL.Text);
                this.txtPL.Text = Preco.ToString("##,###.00");
            }
        }
        private void txtpress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ProcessTabKey(true);
        }

        private void Carrega_Grid()
        {
            string StringSQl;
            SqlDataAdapter dp = new SqlDataAdapter();
            DataTable tablep = new DataTable();
            StringSQl = "SET LANGUAGE 'US_ENGLISH'; select *  from VW_PL_Carteira order by PL_Date desc";
            dp = CargaDados.DB.Return_DataAdapter(StringSQl);
            dp.Fill(tablep);
            dgPL.DataSource = tablep;
            dp.Dispose();
            tablep.Dispose();

            if (dgPL.Columns.Contains("Id_Portfolio") == true)
            {
                dgPL.Columns["Id_Portfolio"].Visible = false;
            }
            dgPL.Columns["Portfolio"].Width = 130;
            dgPL.Columns["Portfolio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgPL.Columns["PL_Date"].Width = 80;
            dgPL.Columns["PL_Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPL.Columns["Pl_Number"].DefaultCellStyle.Format = "##,###.00;(#,###.00)";
            dgPL.Columns["Pl_Number"].Width = 90;
            dgPL.Columns["Pl_Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPL.Columns["Save"].Visible = true;
            dgPL.DefaultCellStyle.BackColor = Color.FromArgb(210, 210, 210);
        }

        private void dgPL_CellFormat(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //     dgPL.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(210, 210, 210);
        }

        private void dgPL_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int Id_Portfolio;
            string StringSQl;
            string Data_PL;
            DateTime Data_PL2;
            string Valor_Pl;
            int retorno;
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                Id_Portfolio = Convert.ToInt32(dgPL.Rows[e.RowIndex].Cells[1].Value.ToString());
                Data_PL = dgPL.Rows[e.RowIndex].Cells[3].Value.ToString();
                Valor_Pl = dgPL.Rows[e.RowIndex].Cells[4].Value.ToString().Replace(".", "").Replace(",", ".");

                Data_PL2 = Convert.ToDateTime(Data_PL);
                Data_PL = Data_PL2.ToString("yyyyMMdd");

                StringSQl = "UPDATE Tb025_Valor_PL Set Valor_PL =" + Valor_Pl +
                " Where Id_Carteira = " + Id_Portfolio + " and Data_PL='" + Data_PL + "'";

                retorno = CargaDados.DB.Execute_Insert_Delete_Update(StringSQl);

                if (retorno != 0)
                {
                    Carrega_Grid();

                }
                else
                {
                    MessageBox.Show("Error on Inserte!");
                }
            }


        }
    }
}