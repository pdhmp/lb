using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LiveBook.Business;
using NestDLL;
using System.Data.SqlClient;

namespace LiveBook
{
    public partial class frmSplitPercent : Form
    {
        Business_Class Negocios = new Business_Class();
        newNestConn curConn = new newNestConn();
        LB_Utils curUtils = new LB_Utils();

        public frmSplitPercent()
        {
            InitializeComponent();
        }

        private void frmSplitPercent_Load(object sender, EventArgs e)
        {   
            Carrega_Grid();

            // Desabilita Controles
            this.txtNFund.Enabled = false;
            this.txtMH.Enabled = false;
            this.txtBravo.Enabled = false;
            this.txtHedge.Enabled = false;

        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString = "";
            double MH = 0, NFund = 0, Bravo = 0, dHedge = 0;


            if (txtTotal.Text != "100")
            {
                MessageBox.Show("Error on Percents!");
                return;
            }


            if (optMH_Fund.Checked == true)
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

                int retorno = curConn.ExecuteNonQuery(SQLString, 1);
                if (retorno == 99)
                {
                    MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Carrega_Grid();
                }
            }
            

            if (optFiaHedge.Checked == true)
            {
                if (this.txtBravo.Text != "" && this.txtBravo.Text != "0")
                {
                    Bravo = Convert.ToDouble(this.txtBravo.Text) / 100;
                    SQLString = SQLString + " exec dbo.Proc_Insert_Price 21140," + Bravo.ToString().Replace(",", ".") + ",'" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "',14,7,0 ;";
                    // SQLString = SQLString + " exec dbo.Proc_Insert_Price 4946," + Bravo.ToString().Replace(",", ".") + ",'" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "',14,7,0 ;";
                }

                if (this.txtHedge.Text != "" && this.txtHedge.Text != "0")
                {
                    dHedge = Convert.ToDouble(this.txtHedge.Text) / 100;
                    SQLString = SQLString + " exec dbo.Proc_Insert_Price 683986," + dHedge.ToString().Replace(",", ".") + ",'" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "',14,7,0 ;";
                }

                int retorno = curConn.ExecuteNonQuery(SQLString, 1);
                if (retorno == 99)
                {
                    MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Carrega_Grid();
                }
            }

            #region Comentado OK
            /*
            string SQLString="";
            double MH = 0, NFund = 0, Bravo = 0, dHedge = 0;

            
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

                if (this.txtHedge.Text != "" && this.txtHedge.Text != "0")
                {
                    dHedge = Convert.ToDouble(this.txtHedge.Text) / 100;
                    SQLString = SQLString + " exec dbo.Proc_Insert_Price 683986," + dHedge.ToString().Replace(",", ".") + ",'" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "',14,7,0 ;";
                }


                int retorno = curConn.ExecuteNonQuery(SQLString,1);
                if (retorno == 99)
                {
                    MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Carrega_Grid();
                }
            }
            */
            #endregion

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
            SQLString = "Select IdSecurity ,srValue as Perc_Number,SrDate as Perc_Date,Port_Name as Portfolio   " +
                        " from dbo.Tb056_Precos_Fundos A (nolock) inner join Tb002_Portfolios B (nolock) ON A.IdSecurity = B.Id_Ticker" +
                        " Where SrType=14 and Id_port_Type=2 and Discountinued =0 order by SrDate desc";

            tablep = curConn.Return_DataTable(SQLString);
            dgPL.DataSource = tablep;
            
            tablep.Dispose();

            if (dgPL.Columns.Contains("IdSecurity") == true)
            {
                dgPL.Columns["IdSecurity"].Visible = false;
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

        private void txtHedge_TextChanged(object sender, EventArgs e)
        {
            UpdatePercent();
        }

        void UpdatePercent()
        {
            double Mile = 0, Bravo = 0, Fund = 0, dHedge = 0;

            //if (curUtils.IsNumeric(txtMH.Text.Replace(".", "")) == true) { Mile = Convert.ToDouble(txtMH.Text.Replace(".", "")); }
            //if (curUtils.IsNumeric(txtNFund.Text.Replace(".", "")) == true) { Fund = Convert.ToDouble(txtNFund.Text.Replace(".", "")); }
            //if (curUtils.IsNumeric(txtBravo.Text.Replace(".", "")) == true) { Bravo = Convert.ToDouble(txtBravo.Text.Replace(".", "")); }
            //if (curUtils.IsNumeric(txtHedge.Text.Replace(".", "")) == true) { dHedge = Convert.ToDouble(txtHedge.Text.Replace(".", "")); }
            //txtTotal.Text = Convert.ToString(Mile + Fund + Bravo + dHedge);

            if (optFiaHedge.Checked == true)
            {
                if (curUtils.IsNumeric(txtBravo.Text.Replace(".", "")) == true) { Bravo = Convert.ToDouble(txtBravo.Text.Replace(".", "")); }
                if (curUtils.IsNumeric(txtHedge.Text.Replace(".", "")) == true) { dHedge = Convert.ToDouble(txtHedge.Text.Replace(".", "")); }
                txtTotal.Text = Convert.ToString(Bravo + dHedge);
            }

            if (optMH_Fund.Checked == true)
            {
                if (curUtils.IsNumeric(txtMH.Text.Replace(".", "")) == true) { Mile = Convert.ToDouble(txtMH.Text.Replace(".", "")); }
                if (curUtils.IsNumeric(txtNFund.Text.Replace(".", "")) == true) { Fund = Convert.ToDouble(txtNFund.Text.Replace(".", "")); }
                txtTotal.Text = Convert.ToString(Mile + Fund);
            }

        }

        private void optMH_Fund_CheckedChanged(object sender, EventArgs e)
        {
            this.txtNFund.Enabled = true;
            this.txtMH.Enabled = true;
            this.txtBravo.Enabled = false;
            this.txtHedge.Enabled = false;

            this.txtNFund.Text = "";
            this.txtMH.Text = "";
            this.txtBravo.Text = "";
            this.txtHedge.Text = "";
        }

        private void optFiaHedge_CheckedChanged(object sender, EventArgs e)
        {
            this.txtNFund.Enabled = false;
            this.txtMH.Enabled = false;
            this.txtBravo.Enabled = true;
            this.txtHedge.Enabled = true;

            this.txtNFund.Text = "";
            this.txtMH.Text = "";
            this.txtBravo.Text = "";
            this.txtHedge.Text = "";
        }

    }
}