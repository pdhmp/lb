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
namespace SGN
{
    public partial class frmPL : Form
    {
        Business_Class Negocios = new Business_Class();
        CarregaDados CargaDados = new CarregaDados();

        public frmPL()
        {
            InitializeComponent();
            Carrega_Grid();
        }

        private void frmPL_Load(object sender, EventArgs e)
        {
            if (NestDLL.NUserControl.Instance.User_Id == 1)
            {
            CargaDados.carregacombo(this.CmbPortfolio, "select Id_Portfolio,Port_Name from dbo.Tb002_Portfolios Where Id_Port_Type in (1,2) and Discountinued<>1", "Id_Portfolio", "Port_Name");
            }
            else
            {
                CargaDados.carregacombo(this.CmbPortfolio, "select Id_Portfolio,Port_Name from VW_Portfolios Where Id_Port_Type in (1,2) and Discountinued<>1", "Id_Portfolio", "Port_Name");
            }
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString;

            //SQLString = " select Id_Portfolio from VW_PL_Portfolios where Id_Portfolio = " + this.CmbPortfolio.SelectedValue.ToString() + " and Data_PL = '" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "'";

                if (txtPL.Text == "")
                {
                    MessageBox.Show("Insert Valid value!");
                }
                else
                {
                    SQLString = "select Z.Data_PL as Data_PL From Tb025_Valor_PL Z" +
                    "  inner join ( select Id_Portfolio, max(Data_PL) as Data_PL" +
                    " From Tb025_Valor_PL Where Id_Portfolio =" + this.CmbPortfolio.SelectedValue.ToString() + " group by Id_Portfolio) mxDrv " +
                    " on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Portfolio = Z.Id_Portfolio Where Z.Id_Portfolio =" + this.CmbPortfolio.SelectedValue.ToString();

                     string Data_INI = CargaDados.curConn.Execute_Query_String(SQLString);

                    if (Negocios.Insere_PL(Convert.ToInt32(this.CmbPortfolio.SelectedValue.ToString()), Convert.ToDateTime(this.dtpDataPL.Value.ToString("dd/MM/yyyy")), Convert.ToDecimal(this.txtPL.Text)) != 0)
                    {
                        SQLString = " EXEC [Proc_Load_Position] @Id_Portfolio=" + this.CmbPortfolio.SelectedValue.ToString() + ",@Data_Trades= '" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "',@Flag_Historic=1";

                        string retorno = Convert.ToString(CargaDados.curConn.ExecuteNonQuery(SQLString,1));

                        if ( retorno == "0")
                        {
                            MessageBox.Show("Error on insert Historical Position!");
                        }

                        string Id_Portfolio = this.CmbPortfolio.SelectedValue.ToString();
                        if (retorno == "0")
                        {
                            SQLString = "delete from Tb025_Valor_PL Where Data_Pl='" + this.dtpDataPL.Value.ToString("yyyyMMdd") + "' and Id_Portfolio=" + Id_Portfolio;
                            CargaDados.curConn.ExecuteNonQuery(SQLString, 1);

                            SQLString = "INSERT INTO NESTLOG.dbo.Tb211_Log_PL(Id_User,Id_Portfolio,Date_Selected,Host,Type_PL,PL)" +
                              "VALUES(" + NestDLL.NUserControl.Instance.User_Id + "," + Id_Portfolio + ",'" + Convert.ToDateTime(this.dtpDataPL.Value.ToString("dd/MM/yyyy")) + "',host_name(),'DEL'," + Convert.ToDecimal(this.txtPL.Text);
                            int resultado = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);

                            MessageBox.Show("Error on Copy Positions!");
                        }
                        Carrega_Grid();
                }
                    else
                    {
                        MessageBox.Show("Error on insert, verify data and date!");
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
                this.txtPL.Text = Preco.ToString("##,##0.00");
            }
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
            SQLString = "SET LANGUAGE 'US_ENGLISH'; select *  from VW_PL_Portfolios order by PL_Date desc";
            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dgPL.DataSource = tablep;
            
            tablep.Dispose();

            if (dgPL.Columns.Contains("Id_Portfolio") == true)
            {
                dgPL.Columns["Id_Portfolio"].Visible = false;
            }
            dgPL.Columns["Portfolio"].Width = 130;
            dgPL.Columns["Portfolio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgPL.Columns["PL_Date"].Width = 80;
            dgPL.Columns["PL_Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPL.Columns["Pl_Number"].DefaultCellStyle.Format = "##,##0.00;(#,##0.00)";
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
            string SQLString;
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

                SQLString = "UPDATE Tb025_Valor_PL Set Valor_PL =" + Valor_Pl +
                " Where Id_Portfolio = " + Id_Portfolio + " and Data_PL='" + Data_PL + "'";

                retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,1);

                if (retorno != 0)
                {
                    Carrega_Grid();

                }
                else
                {
                    MessageBox.Show("Error on Insert!");
                }
            }


        }
    }
}