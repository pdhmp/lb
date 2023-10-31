using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Business;
using NestDLL;
using System.Data.SqlClient;
using SGN.Validacao;
namespace SGN
{
    public partial class frmSplitPercent : Form
    {
        Business_Class Negocios = new Business_Class();
        CarregaDados CargaDados = new CarregaDados();
        Valida Valida = new Valida();

        public frmSplitPercent()
        {
            InitializeComponent();
        }

        private void frmPL_Load(object sender, EventArgs e)
        {
            
            Carrega_Grid();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString="";
            double MH = 0, NFund = 0, Bravo = 0;

            if (txtTotal.Text != "100")
                {
                    MessageBox.Show("Error on Percents!");
                }
                else
                {

                    if (this.txtNFund.Text != "" && this.txtNFund.Text != "0")
                    {
                        NFund = Convert.ToDouble(this.txtNFund.Text) / 100;

                        SQLString = "exec dbo.Proc_Insert_Price 5228," + NFund.ToString().Replace(",", ".") + ",'" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "',14,7,0 ;";
                    }
                    if (this.txtMH.Text != "" && this.txtMH.Text != "0")
                    {
                        MH = Convert.ToDouble(this.txtMH.Text) / 100;
                        SQLString = SQLString + " exec dbo.Proc_Insert_Price 13663," + MH.ToString().Replace(",", ".") + ",'" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "',14,7,0 ;";
                    }
                    if (this.txtBravo.Text != "" && this.txtBravo.Text != "0")
                    {
                        Bravo = Convert.ToDouble(this.txtBravo.Text) / 100;
                        SQLString = SQLString + " exec dbo.Proc_Insert_Price 4946," + Bravo.ToString().Replace(",", ".") + ",'" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "',14,7,0 ;";
                    }

                    int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString);
                    if (retorno == 99)
                    {
                        MessageBox.Show("Error on Insert!");
                    }
                    else
                    {
                        Carrega_Grid();
                    }
                }
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtpress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ProcessTabKey(true);
        }

        private void Carrega_Grid()
        {

            string SQLString;
            
            DataTable tablep = new DataTable();
            SQLString = "Select Id_Ativo as Id_Ticker,Valor as Perc_Number,Data_Hora_Reg as Perc_Date,Port_Name as Portfolio " +
                        " from dbo.Tb056_Precos_Fundos A inner join Tb002_Portfolios B ON A.Id_Ativo = B.Id_Ticker" +
                        " Where Tipo_preco=14 and Id_port_Type=2 and Discountinued =0 order by data_hora_reg desc";

            tablep = CargaDados.curConn.Return_DataTable(SQLString);
            dgPL.DataSource = tablep;
            
            tablep.Dispose();

            if (dgPL.Columns.Contains("Id_Ticker") == true)
            {
                dgPL.Columns["Id_Ticker"].Visible = false;
            }
            dgPL.Columns["Portfolio"].Width = 130;
            dgPL.Columns["Portfolio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgPL.Columns["Perc_Date"].Width = 80;
            dgPL.Columns["Perc_Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPL.Columns["Perc_Number"].DefaultCellStyle.Format = "p";
            dgPL.Columns["Perc_Number"].Width = 90;
            dgPL.Columns["Perc_Number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPL.DefaultCellStyle.BackColor = Color.FromArgb(210, 210, 210);
        }

        private void CmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();

        }

        private void txtMH_TextChanged(object sender, EventArgs e)
        {
            UpdatePercent();
        }

        private void txtNFund_TextChanged(object sender, EventArgs e)
        {
            UpdatePercent();
        }

        private void txtBravo_TextChanged(object sender, EventArgs e)
        {
            UpdatePercent();
        }

        void UpdatePercent()
        {
            double Mile = 0, Bravo = 0, Fund = 0;

            if (Valida.IsNumeric(txtMH.Text.Replace(".", "")) == true) { Mile = Convert.ToDouble(txtMH.Text.Replace(".", "")); }
            if (Valida.IsNumeric(txtNFund.Text.Replace(".", "")) == true) { Fund = Convert.ToDouble(txtNFund.Text.Replace(".", "")); }
            if (Valida.IsNumeric(txtBravo.Text.Replace(".", "")) == true) { Bravo = Convert.ToDouble(txtBravo.Text.Replace(".", "")); }

            txtTotal.Text = Convert.ToString(Mile + Fund + Bravo);

        }

    }
}