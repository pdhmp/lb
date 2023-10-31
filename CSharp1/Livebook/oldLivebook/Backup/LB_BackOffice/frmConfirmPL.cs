using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using SGN.Business;
using System.Data.SqlClient;

namespace SGN
{
    public partial class frmConfirmPL : Form
    {
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();

        public frmConfirmPL()
        {
            InitializeComponent();
            Carrega_Grid();
        }

        private void frmConfirmPL_Load(object sender, EventArgs e)
        {
            
            carrega_Combo();

            txtNAVNest.BackColor = System.Drawing.Color.White;
            txtNavBRLNest.BackColor = System.Drawing.Color.White;
            txtNavUSDNest.BackColor = System.Drawing.Color.White;
            txtCashBRLNest.BackColor = System.Drawing.Color.White;
            txtCashUSDNest.BackColor = System.Drawing.Color.White;

            txtNAVAdj.BackColor = System.Drawing.Color.White;
            txtNavBRLAdj.BackColor = System.Drawing.Color.White;
            txtNavUSDAdj.BackColor = System.Drawing.Color.White;
            txtCashBRLAdj.BackColor = System.Drawing.Color.White;
            txtCashUSDAdj.BackColor = System.Drawing.Color.White;

            
            Load_LB_NAVs();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            string SQLString="";
            int Id_Portfolio = Convert.ToInt32(this.CmbPortfolio.SelectedValue.ToString());
            DateTime DataPL = Convert.ToDateTime(this.dtpDate.Value.ToString("dd/MM/yyyy"));

            if (Valida.IsNumeric(txtNAVNest.Text) == true)
            {
                    if (Id_Portfolio == 4)
                    {
                        Negocios.Insere_PL(5,DataPL, Convert.ToDecimal(txtNavBRLAdm.Text)) ;
                        Negocios.Insere_PL(6, DataPL, Convert.ToDecimal(txtNavUSDAdm.Text));
                        Negocios.Insere_PL(Id_Portfolio, DataPL, Convert.ToDecimal(txtNAVAdm.Text));

                        if (Valida.IsNumeric(txtCashBRLAdm.Text))
                        {
                            Negocios.Insert_Cash(Id_Portfolio, 1184, DataPL, Convert.ToDecimal(txtCashBRLAdm.Text));
                        }
                        if (Valida.IsNumeric(txtCashUSDAdm.Text))
                        {
                            Negocios.Insert_Cash(Id_Portfolio, 5791, DataPL, Convert.ToDecimal(txtCashUSDAdm.Text));
                        }
                        //InsertAdj(5, 5746, Convert.ToDecimal(txtNavBRLAdj.Text));
                        //InsertAdj(6, 5747, Convert.ToDecimal(txtNavUSDAdj.Text));
                       // SQLString = " EXEC [Proc_Load_Position] @Id_Portfolio=5,@Data_Trades= '" + this.dtpDate.Value.ToString("yyyyMMdd") + "',@Flag_Historic=1; ";
                       // SQLString = SQLString + " EXEC [Proc_Load_Position] @Id_Portfolio=6,@Data_Trades= '" + this.dtpDate.Value.ToString("yyyyMMdd") + "',@Flag_Historic=1;";
                       // SQLString = " EXEC [Proc_Load_Position] @Id_Portfolio=" + Id_Portfolio + ",@Data_Trades= '" + this.dtpDate.Value.ToString("yyyyMMdd") + "',@Flag_Historic=1;";

                    }
                    if (Id_Portfolio == 43)
                    {
                        Negocios.Insere_PL(41, DataPL, Convert.ToDecimal(this.txtNavBRLAdm.Text));
                        Negocios.Insere_PL(42, DataPL, Convert.ToDecimal(this.txtNavUSDAdm.Text));
                        Negocios.Insere_PL(Id_Portfolio, DataPL, Convert.ToDecimal(this.txtNAVAdm.Text));

                        if (Valida.IsNumeric(txtCashBRLAdm.Text))
                        {
                            Negocios.Insert_Cash(Id_Portfolio, 1184, DataPL, Convert.ToDecimal(txtCashBRLAdm.Text));
                        }
                        if (Valida.IsNumeric(txtCashUSDAdm.Text))
                        {
                            Negocios.Insert_Cash(Id_Portfolio, 5791, DataPL, Convert.ToDecimal(txtCashUSDAdm.Text));
                        }
                        //InsertAdj(41, 5746, Convert.ToDecimal(txtNavBRLAdj.Text));
                        //InsertAdj(42, 5747, Convert.ToDecimal(txtNavUSDAdj.Text));
                       // SQLString = " EXEC [Proc_Load_Position] @Id_Portfolio=42,@Data_Trades= '" + this.dtpDate.Value.ToString("yyyyMMdd") + "',@Flag_Historic=1; ";
                       // SQLString = SQLString + " EXEC [Proc_Load_Position] @Id_Portfolio=41,@Data_Trades= '" + this.dtpDate.Value.ToString("yyyyMMdd") + "',@Flag_Historic=1;";
                       // SQLString = " EXEC [Proc_Load_Position] @Id_Portfolio=" + Id_Portfolio + ",@Data_Trades= '" + this.dtpDate.Value.ToString("yyyyMMdd") + "',@Flag_Historic=1;";
                    }
                    if (Id_Portfolio == 10)
                    {
                        if (Valida.IsNumeric(txtCashBRLAdm.Text))
                        {
                            Negocios.Insert_Cash(Id_Portfolio, 1184, DataPL, Convert.ToDecimal(txtCashBRLAdm.Text));
                        }

                        Negocios.Insere_PL(11, DataPL, Convert.ToDecimal(this.txtNavBRLAdm.Text));
                        Negocios.Insere_PL(Id_Portfolio, DataPL, Convert.ToDecimal(this.txtNAVAdm.Text));
                      //  SQLString = " EXEC [Proc_Load_Position] @Id_Portfolio=11,@Data_Trades= '" + this.dtpDate.Value.ToString("yyyyMMdd") + "',@Flag_Historic=1; ";
                      //  SQLString = " EXEC [Proc_Load_Position] @Id_Portfolio=" + Id_Portfolio + ",@Data_Trades= '" + this.dtpDate.Value.ToString("yyyyMMdd") + "',@Flag_Historic=1;";
                    }

                    SQLString = "INSERT INTO Tb207_Pending(Id_Ticker,Ini_Date,Source,Status,IsTR)VALUES(" + Id_Portfolio + ",'" + this.dtpDate.Value.ToString("yyyyMMdd") + "',3,0,0)";

                    string retorno = Convert.ToString(CargaDados.curConn.ExecuteNonQuery(SQLString,1));

                    if (retorno == "0")
                    {
                        MessageBox.Show("Error on insert NAV!");
                    }
                    else
                    {
                        MessageBox.Show("Inserted!");
                    }
                    Carrega_Grid();
            }
        }

        void InsertAdj(int Id_Portfolio, int Id_Ticker, decimal AdjValue)
        {
            int Id_Account=0;
            if (AdjValue != 0)
            {
                switch (Id_Portfolio)
                {
                    case 5: Id_Account = 1046; break;
                    case 6: Id_Account = 1060; break;
                    case 11: Id_Account = 1073; break;
                    case 41: Id_Account = 1148; break;
                    case 42: Id_Account = 1086; break;
                    case 45: Id_Account = 1093; break;
                }

                string SQLString = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions]([Transaction_Type],[Trade_Date],[Settlement_Date],[Id_Account1],[Id_Ticker1],[Id_Strategy1],[Id_Sub_Strategy1],[Quantity1],[Id_Account2],[Id_Ticker2],[Id_Strategy2],[Id_Sub_Strategy2],[Quantity2]) " +
                                " VALUES(99, '" + this.dtpDate.Value.ToString("yyyyMMdd") + "', '" + this.dtpDate.Value.ToString("yyyyMMdd") + "', " + Id_Account + ", " + Id_Ticker + ", 9, 9, 0, " + Id_Account + ", " + Id_Ticker + ", 9, 9, " + AdjValue.ToString().Replace(",", ".") + ") ;";
                CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
            }
        }

        void LoadAdminNAVs(int Id_Portfolio, int Id_PortfolioBRL, int Id_PortfolioUSD, string DateToGet)
        {

            string SQLString = "SELECT Valor_PL FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio= " + Id_Portfolio + " AND Data_PL='" + DateToGet + "'";
            string tempNAV = CargaDados.curConn.Execute_Query_String(SQLString);
            if (tempNAV != "")
            {
                txtNAVAdm.Text = Convert.ToDouble(tempNAV).ToString("#,##0.00");
            }
            else
            {
                txtNAVAdm.Text = "";
            }

            SQLString = "SELECT Valor_PL FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio= " + Id_PortfolioBRL + " AND Data_PL='" + DateToGet + "'";
            tempNAV = CargaDados.curConn.Execute_Query_String(SQLString);
            if (tempNAV != "")
            {
                txtNavBRLAdm.Text = Convert.ToDouble(tempNAV).ToString("#,##0.00");
            }
            else
            {
                txtNavBRLAdm.Text = "";
            }

            SQLString = "SELECT Valor_PL FROM dbo.Tb025_Valor_PL WHERE Id_Portfolio= " + Id_PortfolioUSD + " AND Data_PL='" + DateToGet + "'";
            tempNAV = CargaDados.curConn.Execute_Query_String(SQLString);
            if (tempNAV != "")
            {
                txtNavUSDAdm.Text = Convert.ToDouble(tempNAV).ToString("#,##0.00");
            }
            else
            {
                txtNavUSDAdm.Text = "";
            } 
        }

        void Load_LB_NAVs()
        {
            if (CmbPortfolio.SelectedIndex >=0)
            {
                int Id_Portfolio = Convert.ToInt32(CmbPortfolio.SelectedValue.ToString());
                DateTime DataPL = Convert.ToDateTime(dtpDate.Value.ToString("dd/MM/yyyy"));
                string Tablename = "";
                string DateFilter = "";

                int Id_PortfolioBRL = 0;
                int Id_PortfolioUSD = 0;
                
                txtNAVNest.Text = "";
                txtNAVAdm.Text = "";
                txtNAVAdj.Text = "";

                txtNavBRLNest.Text = "";
                txtNavBRLAdm.Text = "";
                txtNavBRLAdj.Text = "";

                txtNavUSDNest.Text = "";
                txtNavUSDAdm.Text = "";
                txtNavUSDAdj.Text = "";

                txtCashUSDNest.Text = "";
                txtCashUSDAdm.Text = "";
                txtCashUSDAdj.Text = "";

                txtCashBRLNest.Text = "";
                txtCashBRLAdm.Text = "";
                txtCashBRLAdj.Text = "";

                switch (Id_Portfolio)
                {
                    case 4:
                        Id_PortfolioBRL = 5;
                        Id_PortfolioUSD = 6;
                        break;
                    case 10:
                        Id_PortfolioBRL = 11;
                        Id_PortfolioUSD = 0;
                        break;
                    case 43:
                        Id_PortfolioBRL = 41;
                        Id_PortfolioUSD = 42;
                        break;
                    case 45:
                        Id_PortfolioBRL = 46;
                        Id_PortfolioUSD = 0;
                        break;
                }
                
                if (dtpDate.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy"))
                {
                    Tablename = "NESTRT.dbo.FCN_Posicao_Atual()";
                }
                else
                {
                    Tablename = "dbo.Tb000_Historical_Positions";
                    DateFilter = " AND [Date Now]='" + dtpDate.Value.ToString("yyyyMMdd") + "'";
                }

                string SQLString = "SELECT SUM([Cash]) FROM " + Tablename + " WHERE [Id Portfolio]= " + Id_Portfolio + DateFilter;
                string tempNAV = CargaDados.curConn.Execute_Query_String(SQLString);
                if (tempNAV != "")
                {
                    txtNAVNest.Text = Convert.ToDouble(tempNAV).ToString("#,##0.00");
                }
                else
                {
                    txtNAVNest.Text = "0.00";
                }

                SQLString = "SELECT SUM([Cash]) FROM " + Tablename + " WHERE [Id Portfolio]= " + Id_PortfolioBRL + DateFilter;

                tempNAV = CargaDados.curConn.Execute_Query_String(SQLString);
                if (tempNAV != "")
                {
                    txtNavBRLNest.Text = Convert.ToDouble(tempNAV).ToString("#,##0.00");
                }
                else
                {
                    txtNavBRLNest.Text = "0.00";
                }

                SQLString = "SELECT SUM([Cash]) FROM " + Tablename + " WHERE [Id Portfolio]= " + Id_PortfolioUSD + DateFilter;

                tempNAV = CargaDados.curConn.Execute_Query_String(SQLString);
                if (tempNAV != "")
                {
                    txtNavUSDNest.Text = Convert.ToDouble(tempNAV).ToString("#,##0.00");
                }
                else
                {
                    txtNavUSDNest.Text = "0.00";
                }

                SQLString = "SELECT SUM(CASE WHEN [Id Ticker]=1844 THEN [Cash] ELSE 0 END) FROM " + Tablename + " WHERE [Id Portfolio]= " + Id_Portfolio + DateFilter;

                tempNAV = CargaDados.curConn.Execute_Query_String(SQLString);
                if (tempNAV != "")
                {
                    txtCashBRLNest.Text = Convert.ToDouble(tempNAV).ToString("#,##0.00");
                }
                else
                {
                    txtCashBRLNest.Text = "0.00";
                }

                SQLString = "SELECT SUM(CASE WHEN [Id Ticker]=5791 THEN [Cash] ELSE 0 END) FROM " + Tablename + " WHERE [Id Portfolio]= " + Id_Portfolio + DateFilter;

                tempNAV = CargaDados.curConn.Execute_Query_String(SQLString);
                if (tempNAV != "")
                {
                    txtCashUSDNest.Text = Convert.ToDouble(tempNAV).ToString("#,##0.00");
                }
                else
                {
                    txtCashUSDNest.Text = "0.00";
                }

                switch (Id_Portfolio)
                {
                    case 4: LoadAdminNAVs(4, 5, 6, dtpDate.Value.ToString("yyyyMMdd")); break;
                    case 10: LoadAdminNAVs(10, 11, 0, dtpDate.Value.ToString("yyyyMMdd")); break;
                    case 43: LoadAdminNAVs(43, 41, 42, dtpDate.Value.ToString("yyyyMMdd")); break;
                    case 45: LoadAdminNAVs(45, 46, 0, dtpDate.Value.ToString("yyyyMMdd")); break;
                }

                if (Valida.IsNumeric(CmbPortfolio.SelectedValue.ToString()) == true)
                {
                    if (Convert.ToInt32(CmbPortfolio.SelectedValue.ToString()) == 10 || Convert.ToInt32(CmbPortfolio.SelectedValue.ToString()) == 45)
                    {
                        txtNavUSDAdm.Visible = false;
                        txtCashUSDAdm.Visible = false;
                        txtNavUSDAdj.Visible = false;
                        txtCashUSDAdj.Visible = false;

                        txtNavUSDAdm.Text = "";
                        txtCashUSDAdm.Text = "";
                        txtNavUSDNest.Text = "";
                        txtCashUSDNest.Text = "";
                    }
                    else
                    {
                        txtNavUSDAdm.Visible = true;
                        txtCashUSDAdm.Visible = true;
                        txtNavUSDAdj.Visible = true;
                        txtCashUSDAdj.Visible = true;
                    }
                }
            }
        }

        void carrega_Combo()
        {
            CmbPortfolio.SelectedValueChanged -= CmbPortfolio_SelectedValueChanged;
            CargaDados.carregacombo(this.CmbPortfolio, "Select Id_Portfolio,Port_Name from  Tb002_Portfolios where Id_Port_Type = 2 And (RT_Position=1 or HIST_Position=1)", "Id_Portfolio", "Port_Name", 99);
            CmbPortfolio.SelectedValueChanged += CmbPortfolio_SelectedValueChanged;
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
            //dgPL.Columns["Save"].Visible = true;
            dgPL.DefaultCellStyle.BackColor = Color.FromArgb(210, 210, 210);
        }

        private void CmbPortfolio_SelectedValueChanged(object sender, EventArgs e)
        {
            Load_LB_NAVs();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            Load_LB_NAVs();
        }

        private void UpdateAdjustments()
        {
            if (Valida.IsNumeric(txtNAVAdm.Text) && Valida.IsNumeric(txtNAVNest.Text))
            {
                double tempNAVAdm = Convert.ToDouble(txtNAVAdm.Text);
                double tempNAVNest = Convert.ToDouble(txtNAVNest.Text);
                txtNAVAdj.Text = (tempNAVAdm - tempNAVNest).ToString("#,##0.00");
            }

            if (Valida.IsNumeric(txtNavUSDAdm.Text) && Valida.IsNumeric(txtNavUSDNest.Text))
            {
                double tempNAVUSDAdm = Convert.ToDouble(txtNavUSDAdm.Text);
                double tempNAVUSDNest = Convert.ToDouble(txtNavUSDNest.Text);
                txtNavUSDAdj.Text = (tempNAVUSDAdm - tempNAVUSDNest).ToString("#,##0.00");
            }

            if (Valida.IsNumeric(txtNavBRLAdm.Text) && Valida.IsNumeric(txtNavBRLNest.Text))
            {
                double tempNAVBRLAdm = Convert.ToDouble(txtNavBRLAdm.Text);
                double tempNAVBRLNest = Convert.ToDouble(txtNavBRLNest.Text);
                txtNavBRLAdj.Text = (tempNAVBRLAdm - tempNAVBRLNest).ToString("#,##0.00");
            }

            if (Valida.IsNumeric(txtCashBRLAdm.Text) && Valida.IsNumeric(txtCashBRLNest.Text))
            {
                double tempCashBRLAdm = Convert.ToDouble(txtCashBRLAdm.Text);
                double tempCashBRLNest = Convert.ToDouble(txtCashBRLNest.Text);
                txtCashBRLAdj.Text = (tempCashBRLAdm - tempCashBRLNest).ToString("#,##0.00");
            }

            if (Valida.IsNumeric(txtCashUSDAdm.Text) && Valida.IsNumeric(txtCashUSDNest.Text))
            {
                double tempCashUSDAdm = Convert.ToDouble(txtCashUSDAdm.Text);
                double tempCashUSDNest = Convert.ToDouble(txtCashUSDNest.Text);
                txtCashUSDAdj.Text = (tempCashUSDAdm - tempCashUSDNest).ToString("#,##0.00");
            }
        }

        #region TextBoxEvents

        private void txtNAVAdm_TextChanged(object sender, EventArgs e)
        {
            UpdateAdjustments();
        }

        private void txtNavUSDAdm_TextChanged(object sender, EventArgs e)
        {
            UpdateAdjustments();
        }

        private void txtNavBRLAdm_TextChanged(object sender, EventArgs e)
        {
            UpdateAdjustments();
        }

        private void txtCashBRLAdm_TextChanged(object sender, EventArgs e)
        {
            UpdateAdjustments();
        }

        private void txtCashUSDAdm_TextChanged(object sender, EventArgs e)
        {
            UpdateAdjustments();
        }

        private void txtNAVAdm_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtNAVAdm.Text))
            {
                txtNAVAdm.Text = Convert.ToDouble(txtNAVAdm.Text).ToString("#,##0.00");
            }
            else
            {
                txtNAVAdm.Text = "";
            }
        }

        private void txtNavBRLAdm_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtNavBRLAdm.Text))
            {
                txtNavBRLAdm.Text = Convert.ToDouble(txtNavBRLAdm.Text).ToString("#,##0.00");
            }
            else
            {
                txtNavBRLAdm.Text = "";
            }
        }

        private void txtNavUSDAdm_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtNavUSDAdm.Text))
            {
                txtNavUSDAdm.Text = Convert.ToDouble(txtNavUSDAdm.Text).ToString("#,##0.00");
            }
            else
            {
                txtNavUSDAdm.Text = "";
            }
        }

        private void txtCashBRLAdm_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtCashBRLAdm.Text))
            {
                txtCashBRLAdm.Text = Convert.ToDouble(txtCashBRLAdm.Text).ToString("#,##0.00");
            }
            else 
            {
                txtCashBRLAdm.Text = "";
            }
        }

        private void txtCashUSDAdm_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtCashUSDAdm.Text))
            {
                txtCashUSDAdm.Text = Convert.ToDouble(txtCashUSDAdm.Text).ToString("#,##0.00");
            }
            else
            {
                txtCashUSDAdm.Text = "";
            }
        }
#endregion

        private void CmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

     }
}