using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Business;
using SGN.Validacao;
using System.Data.SqlClient;
using NestDLL;


namespace SGN
{
    public partial class frmCaixa : Form
    {
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
        public int Id_User;

        public frmCaixa()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chbLastDate.Checked = true;

            CargaDados.carregacombo(cmbPortfolio, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2", "Id_Portfolio", "Port_Name");

            Carrega_Grid();
        }

        void Carrega_Grid()
        {
            DataTable table = new DataTable();
            String SQLString;
            DateTime Data_PL;
            string Data_PL_String;
            int Id_Portfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());

            if (dtpDateCaixa.Enabled == false)
            {
                Data_PL_String = CargaDados.curConn.Execute_Query_String("Select max(Data_Caixa) from Tb016_Caixa where Id_Portfolio =" + cmbPortfolio.SelectedValue.ToString());
            }
            else
            {
                Data_PL_String = CargaDados.curConn.Execute_Query_String("Select max(Data_Caixa) from Tb016_Caixa where Id_Portfolio =" + cmbPortfolio.SelectedValue.ToString() + " and Data_Caixa = '" + dtpDateCaixa.Value.ToString("yyyyMMdd") + "'");
                if (Data_PL_String == "")
                {
                    Data_PL_String = CargaDados.curConn.Execute_Query_String("Select max(Data_Caixa) from Tb016_Caixa where Id_Portfolio =" + cmbPortfolio.SelectedValue.ToString());
                    dtpDateCaixa.Enabled = false;
                    chbLastDate.Checked = true;
                }
            
            }
            if (Data_PL_String != "")
            {
                Data_PL = Convert.ToDateTime(Data_PL_String);

                DateTime Data_D_0 = Convert.ToDateTime(CargaDados.curConn.Execute_Query_String("Select dbo.FCN_Retornar_Data_Dias_Uteis_DATA('" + Data_PL.ToString("yyyyMMdd") + "',1)"));

                label10.Text = "-" + Data_D_0.ToString("dd/MM/yyyy") + "";
                DateTime Datad_1 = Convert.ToDateTime(CargaDados.curConn.Execute_Query_String("Select dbo.FCN_Retornar_Data_Dias_Uteis_DATA('" + Data_D_0.ToString("yyyyMMdd") + "',1)"));
                //DateTime Datad_1 = Convert.ToDateTime(Data_1);
                label11.Text = "-" + Datad_1.ToString("dd/MM/yyyy") + "";

                DateTime Datad_2 = Convert.ToDateTime(CargaDados.curConn.Execute_Query_String("Select dbo.FCN_Retornar_Data_Dias_Uteis_DATA('" + Data_D_0.ToString("yyyyMMdd") + "',2)"));
                label13.Text = "-" + Datad_2.ToString("dd/MM/yyyy") + "";

                DateTime Datad_3 = Convert.ToDateTime(CargaDados.curConn.Execute_Query_String("Select dbo.FCN_Retornar_Data_Dias_Uteis_DATA('" + Data_D_0.ToString("yyyyMMdd") + "',3)"));
                label14.Text = "-" + Datad_3.ToString("dd/MM/yyyy") + "";


                SQLString = " Select * from dbo.FCN_Calcula_Caixa(" + Id_Portfolio + ",'" + Data_PL.ToString("yyyyMMdd") + "') ";

                DataTable CashTable = CargaDados.curConn.Return_DataTable(SQLString);
                dgCaixa.DataSource = CashTable;

                // D_0

                SQLString = "Select Valor from Tb016_Caixa where Id_Portfolio =" + Id_Portfolio + " and Data_Caixa= '" + Data_PL.ToString("yyyyMMdd") + "'";

                double Caixa_D_0 = Convert.ToDouble(CargaDados.curConn.Execute_Query_String(SQLString));
                txtBalance_D_0.Text = Caixa_D_0.ToString("#,##0.00;(#,##0.00)");
                
                double Soma_D_0;
                if (table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Data_D_0.ToString("dd/MM/yyyy") + "' and [Credit/Debit] < 0").ToString() != "")
                {
                    Soma_D_0 = Convert.ToDouble(table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Data_D_0.ToString("dd/MM/yyyy") + "' and [Credit/Debit] < 0"));
                }
                else
                {
                    Soma_D_0 = 0;
                }
                txtDebit_D_0.Text = Soma_D_0.ToString("#,##0.00;(#,##0.00)");

                double Sub_D_0;
                if (table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Data_D_0.ToString("dd/MM/yyyy") + "' and [Credit/Debit] > 0").ToString() != "")
                {
                    Sub_D_0 =  Convert.ToDouble(table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Data_D_0.ToString("dd/MM/yyyy") + "' and [Credit/Debit] > 0"));
                }
                else
                {
                    Sub_D_0 = 0;
                }
                txtCredit_D_0.Text = Sub_D_0.ToString("#,##0.00;(#,##0.00)");

                Double Soma_balanco_D_0 = Caixa_D_0 + Soma_D_0 + Sub_D_0;
                txtCBalance_D_0.Text = Soma_balanco_D_0.ToString("#,##0.00;(#,##0.00)");

                 // D_1
                Double Caixa_D_1 = Soma_balanco_D_0;
                txtBalance_D_1.Text = Caixa_D_1.ToString("#,##0.00;(#,##0.00)");

                double Soma_D_1;
                if (table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_1.ToString("dd/MM/yyyy") + "' and [Credit/Debit] < 0").ToString() != "")
                {
                    Soma_D_1 = Convert.ToDouble(table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_1.ToString("dd/MM/yyyy") + "' and [Credit/Debit] < 0"));
                }
                else
                {
                    Soma_D_1 = 0;
                }
                txtDebit_D_1.Text = Soma_D_1.ToString("#,##0.00;(#,##0.00)");

                double Sub_D_1;
                if (table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_1.ToString("dd/MM/yyyy") + "' and [Credit/Debit] > 0").ToString() != "")
                {
                    Sub_D_1 = Convert.ToDouble(table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_1.ToString("dd/MM/yyyy") + "' and [Credit/Debit] > 0"));
                }
                else
                {
                   Sub_D_1 = 0;
                }
                txtCredit_D_1.Text = Sub_D_1.ToString("#,##0.00;(#,##0.00)");
                
                Double Soma_balanco_D_1 = Caixa_D_1 + Soma_D_1 + Sub_D_1;
                txtCBalance_D_1.Text = Soma_balanco_D_1.ToString("#,##0.00;(#,##0.00)");

                // D_2
                Double Caixa_D_2 = Soma_balanco_D_1;
                txtBalance_D_2.Text = Caixa_D_2.ToString("#,##0.00;(#,##0.00)");
                
                double Soma_D_2;
                if (table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_2.ToString("dd/MM/yyyy") + "' and [Credit/Debit] < 0").ToString() != "")
                {
                    Soma_D_2 = Convert.ToDouble(table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_2.ToString("dd/MM/yyyy") + "' and [Credit/Debit] < 0"));
                }
                else
                {
                    Soma_D_2 = 0;
                }
                txtDebit_D_2.Text = Soma_D_2.ToString("#,##0.00;(#,##0.00)");

                double Sub_D_2;
                if (table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_2.ToString("dd/MM/yyyy") + "' and [Credit/Debit] > 0").ToString() != "")
                {
                    Sub_D_2 =  Convert.ToDouble(table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_2.ToString("dd/MM/yyyy") + "' and [Credit/Debit] > 0"));
                }
                else
                {
                    Sub_D_2 = 0;
                }
                txtCredit_D_2.Text = Sub_D_2.ToString("#,##0.00;(#,##0.00)");
                
                Double Soma_balanco_D_2 = Caixa_D_2 + Soma_D_2 + Sub_D_2;
                txtCBalance_D_2.Text = Soma_balanco_D_2.ToString("#,##0.00;(#,##0.00)");

                // D_3

                Double Caixa_D_3 = Soma_balanco_D_2;
                txtBalance_D_3.Text = Caixa_D_3.ToString("#,##0.00;(#,##0.00)");

                double Soma_D_3;
                if (table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_3.ToString("dd/MM/yyyy") + "' and [Credit/Debit] < 0").ToString() != "")
                {
                    Soma_D_3 =  Convert.ToDouble(table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_3.ToString("dd/MM/yyyy") + "' and [Credit/Debit] < 0"));
                }
                else
                {
                    Soma_D_3 = 0;
                }
                txtDebit_D_3.Text = Soma_D_3.ToString("#,##0.00;(#,##0.00)");

                double Sub_D_3;
                if (table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_3.ToString("dd/MM/yyyy") + "' and [Credit/Debit] > 0").ToString() != "")
                {
                    Sub_D_3 =  Convert.ToDouble(table.Compute("Sum ([Credit/Debit])", "[Settlement Date] = '" + Datad_3.ToString("dd/MM/yyyy") + "' and [Credit/Debit] > 0"));
                }
                else
                {
                    Sub_D_3 = 0;
                }
                txtCredit_D_3.Text = Sub_D_3.ToString("#,##0.00;(#,##0.00)");

                Double Soma_balanco_D_3 = Caixa_D_3 + Soma_D_3 + Sub_D_3;
                txtCBalance_D_3.Text = Soma_balanco_D_3.ToString("#,##0.00;(#,##0.00)");


                /////////////////////////////////////////////////////////////////////////
                table.Dispose();

                Valida.Formatar_Grid(dgCaixa, "Credit/Debit");
            }
            else
            {
                dgCaixa.DataSource = "";
                //dgCaixa
                label10.Text = "" ;
                label11.Text = "" ;
                label13.Text = "" ;
                label14.Text = "" ;
                txtBalance_D_0.Text = "";
                txtBalance_D_1.Text = "";
                txtBalance_D_2.Text = "";
                txtBalance_D_3.Text = "";
                txtDebit_D_0.Text = "";
                txtDebit_D_1.Text = "";
                txtDebit_D_2.Text = "";
                txtDebit_D_3.Text = "";
                txtCredit_D_0.Text = "";
                txtCredit_D_1.Text = "";
                txtCredit_D_2.Text = "";
                txtCredit_D_3.Text = "";
                txtCBalance_D_0.Text = "";
                txtCBalance_D_1.Text = "";
                txtCBalance_D_2.Text = "";
                txtCBalance_D_3.Text = "";
            }
            this.dgCaixa.ColumnWidthChanged -= new DataGridViewColumnEventHandler(this.dgCaixa_ColumnWidthChanged);

           int retorno = Valida.Carregar_Estilo_Colunas(dgCaixa);

            this.dgCaixa.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dgCaixa_ColumnWidthChanged);

        
        }

        void Carrega_campos()
        {
        


        
        }

        private void cmd_Refresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbLastDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chbLastDate.Checked == true)
            {
                dtpDateCaixa.Enabled = false;
            }
            else
            {
                dtpDateCaixa.Enabled = true;
            
            }

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgCaixa_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (Convert.ToInt32(e.RowIndex.ToString()) == -1)
            {
                int retorno = 0;//Valida.Salvar_Estilo_Colunas(dgCaixa, Id_User);
                if (retorno == 0)
                {
                    MessageBox.Show("Error in Save!");
                }
            }

        }

        private void dgCaixa_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            dgCaixa.CellMouseUp -= new DataGridViewCellMouseEventHandler(this.dgCaixa_CellMouseUp);

            int retorno = 0;// Valida.Salvar_Estilo_Colunas(dgCaixa, Id_User);
            if (retorno == 0)
            {
                MessageBox.Show("Error in saving!");
            }

            dgCaixa.CellMouseUp += new DataGridViewCellMouseEventHandler(this.dgCaixa_CellMouseUp);
        }

    }
}