using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using System.Data.SqlClient;
using System.Xml;

namespace SGN
{
    public partial class frmTrade_Aloc_Override : Form
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();

        public int Id_Ticker;
        public int Id_Broker;
        public int Id_User;
        public int Id_Book;
        public int Id_Section;
        
        public frmTrade_Aloc_Override()
        {
            InitializeComponent();
        }

        private void frmTrade_Aloc_Override_Load(object sender, EventArgs e)
        {
            LoadAloc();
            LoadCurrentOverride();
        }

        private void LoadCurrentOverride()
        {
            
            DataTable tablep = new DataTable();
            
            string SQLString = "SELECT Override_Type FROM dbo.Tb352_Trade_Alocation_Override " +
                                "WHERE [Override_Date]=CONVERT(varchar, getdate(), 112) AND [Id_Ticker]=" + Id_Ticker.ToString() +
                                " AND [Id_Broker]=" + Id_Broker.ToString() + " AND [Id_Book]=" + Id_Book.ToString() + " AND [Id_Section]=" + Id_Section.ToString();

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            if (tablep.Rows.Count == 0) { radNoOverride.Checked = true; };
            foreach (DataRow row in tablep.Rows) 
            {
                if (row[0].ToString() == "0") { radNoOverride.Checked = true; };
                if (row[0].ToString() == "1") { radModel.Checked = true; rdMH.Checked = true; };
                if (row[0].ToString() == "2") { radModel.Checked = true; rdFund.Checked = true; };
                if (row[0].ToString() == "3") { radModel.Checked = true; rdBravo.Checked = true; };
                if (row[0].ToString() == "7") { radModel.Checked = true; rdSplit.Checked = true; };
                if (row[0].ToString() == "8") { radRemoveAloc.Checked = true; };
                if (row[0].ToString() == "9") { radFixed.Checked = true; };
            }
        }

        private void LoadQuantities()
        {
            
            DataTable tablep = new DataTable();

            string SQLString = "SELECT *  " +
                "FROM [dbo].[FCN_GET_Trade_Split_Status] () " +
                "WHERE [Id Broker]=" + Id_Broker + " AND [Id Ticker]=" + Id_Ticker +
                " AND [Id Book]=" + Id_Book.ToString() + " AND [Id Section]=" + Id_Section.ToString();

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            foreach (DataRow row in tablep.Rows)
            {
                txtBuyMH.Text = Convert.ToInt32(row["MH_Buy"]).ToString("#,###");
                txtBuyNFund.Text = Convert.ToInt32(row["NFund_Buy"]).ToString("#,###");
                txtBuyBravo.Text = Convert.ToInt32(row["Bravo_Buy"]).ToString("#,###");
                txtSellMH.Text = Convert.ToInt32(row["MH_Sell"]).ToString("#,###");
                txtSellNFund.Text = Convert.ToInt32(row["NFund_Sell"]).ToString("#,###");
                txtSellBravo.Text = Convert.ToInt32(row["Bravo_Sell"]).ToString("#,###");
                //txtBuyAloc.Text = (Convert.ToInt32(row["MH_Buy"]) + Convert.ToInt32(row["Top_Buy"]) + Convert.ToInt32(row["Bravo_Buy"])).ToString("#,###");
                //txtSellAloc.Text = (Convert.ToInt32(row["MH_Sell"]) + Convert.ToInt32(row["Top_Sell"]) + Convert.ToInt32(row["Bravo_Sell"])).ToString("#,###");
            }
            UpdateTotals();

        }


        private void cmdRemove_Click(object sender, EventArgs e)
        {
            string SQLString = "DELETE FROM dbo.Tb352_Trade_Alocation_Override WHERE [Override_Date]=CONVERT(varchar, getdate(), 112) AND [Id_Ticker]=" + Id_Ticker.ToString() + " AND [Id_Broker]=" + Id_Broker.ToString() + " [Id_Book]=" + Id_Book.ToString() + "[Id_Section]=" + Id_Section.ToString() + ";";
            //string SQLString = "INSERT INTO dbo.Tb352_Trade_Alocation_Override([Override_Date],[Id_Ticker],[Id_Broker],[Override_Type])" +
            //                                                    "VALUES(CONVERT(varchar, getdate(), 112)," + Id_Ticker.ToString() + ", " + Id_Broker.ToString() + ",0);";
            string SplitDate;

            SplitDate = DateTime.Now.ToString("yyyyMMdd");

            SQLString = SQLString + "exec [dbo].[proc_Split_Trades] " + Id_Ticker.ToString() + ",'" + SplitDate + "'," + Id_Book.ToString() + "," + Id_Section.ToString();

            int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,1);
            if (retorno == 0)
            {
                MessageBox.Show("Error on Insert!");
            }
            else
            {
                this.Close();
            }
        }

        private void UpdateOverride()
        {
            int OverType;
            string SQLString;
            string data_now = DateTime.Now.ToString("yyyyMMdd");

            OverType = 0;
            SQLString = "";

            if (radNoOverride.Checked == true) { OverType = 0; }
            if (radModel.Checked == true)
            {
                if (rdMH.Checked == true) { OverType = 1; }
                if (rdFund.Checked == true) { OverType = 2; }
                if (rdBravo.Checked == true) { OverType = 3; }
                if (rdSplit.Checked == true) { OverType = 7; }
            }
            if (radRemoveAloc.Checked == true) { OverType = 8; }
            if (radFixed.Checked == true) { OverType = 9; }

            if (OverType != 0)
            {
                SQLString = "INSERT INTO dbo.Tb352_Trade_Alocation_Override([Override_Date],[Id_Ticker],[Id_Broker],[Override_Type],Id_Book,Id_Section)" +
                                                   "VALUES(CONVERT(varchar, getdate(), 112)," + Id_Ticker.ToString() + ", " + Id_Broker.ToString() + "," + OverType + "," + Id_Book + "," + Id_Section + ");";
            }
            else
            {
                SQLString = "DELETE FROM dbo.Tb352_Trade_Alocation_Override WHERE [Override_Date]=CONVERT(varchar, getdate(), 112) AND [Id_Ticker]=" + Id_Ticker.ToString() + " AND [Id_Broker]=" + Id_Broker.ToString() + " AND [Id_Book]=" + Id_Book.ToString() + " AND [Id_Section]=" + Id_Section.ToString() + ";";
            }

            if (OverType == 9) 
            {
                decimal percBuyMH = 0;
                decimal percBuynFund = 0;
                decimal percBuyBravo = 0;
                decimal percSellMH = 0;
                decimal percSellNFund = 0;
                decimal percSellBravo = 0;

                decimal TotalPurchases = 0;
                decimal TotalSales = 0;

                if (txtBuyTotal.Text != "") 
                {
                    TotalPurchases = Convert.ToInt32(txtBuyTotal.Text.Replace(".", "")); 
                };
                if (txtSellTotal.Text != "")
                {
                    TotalSales = Convert.ToInt32(txtSellTotal.Text.Replace(".", "")); 
                };

                if (TotalPurchases != 0)
                {
                    if (Valida.IsNumeric(txtBuyMH.Text) == true) { percBuyMH = Convert.ToDecimal(txtBuyMH.Text.Replace(".", "")) / TotalPurchases; };
                    if (Valida.IsNumeric(txtBuyNFund.Text) == true) { percBuynFund = Convert.ToDecimal(txtBuyNFund.Text.Replace(".", "")) / TotalPurchases; };
                    if (Valida.IsNumeric(txtBuyBravo.Text) == true) { percBuyBravo = Convert.ToDecimal(txtBuyBravo.Text.Replace(".", "")) / TotalPurchases; };
                }
                if (TotalSales != 0)
                {
                    if (Valida.IsNumeric(txtSellMH.Text) == true) { percSellMH = Convert.ToDecimal(txtSellMH.Text.Replace(".", "")) / TotalSales; };
                    if (Valida.IsNumeric(txtSellNFund.Text) == true) { percSellNFund = Convert.ToDecimal(txtSellNFund.Text.Replace(".", "")) / TotalSales; };
                    if (Valida.IsNumeric(txtSellBravo.Text) == true) { percSellBravo = Convert.ToDecimal(txtSellBravo.Text.Replace(".", "")) / TotalSales; };
                }
                SQLString = SQLString + "EXEC dbo.proc_Split_Trades_Manual " + Id_Broker + ", " + Id_Ticker + "," + Id_Book + "," + Id_Section + ", '" + data_now + "'," + percBuyMH.ToString().Replace(',', '.') + ", " + percBuynFund.ToString().Replace(',', '.') + ", " + percBuyBravo.ToString().Replace(',', '.') + ", " + percSellMH.ToString().Replace(',', '.') + ", " + percSellNFund.ToString().Replace(',', '.') + ", " + percSellBravo.ToString().Replace(',', '.') + ";";
            }

            SQLString = SQLString + "exec [dbo].[proc_Split_Trades] " + Id_Ticker.ToString() + ",'" + data_now + "'," + Id_Book.ToString() + "," + Id_Section.ToString() ;

            int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,1);
            if (retorno == 0)
            {
                MessageBox.Show("Error on Insert!");
            }
            else
            {
                this.Close();
            }
        }

        private void LoadAloc()
        {
            
            DataTable tablep = new DataTable();

            string SQLString = "SELECT COALESCE(SUM(a.Quantidade),0) AS Net," +
                "COALESCE(SUM(CASE WHEN a.Quantidade>0 THEN a.Quantidade ELSE 0 END),0), " +
                "COALESCE(SUM(CASE WHEN a.Quantidade<0 THEN a.Quantidade ELSE 0 END),0) " +
                "FROM dbo.Tb013_Trades AS a INNER JOIN " +
                "    dbo.Tb012_Ordens AS b ON b.Id_Ordem = a.Id_Ordem INNER JOIN " +
                "    dbo.Tb001_Securities AS c ON c.IdSecurity = b.Id_Ativo  " +
                " INNER JOIN VW_PortAccounts D ON B.Id_Account = D.Id_Account " +
                "WHERE (a.StatusTrade <> 4) AND b.Status_Ordem<>4 AND (a.Data_Trade >= CONVERT(varchar, GetDate(), 112))  " +
                "AND D.Id_Portfolio = 48 AND b.Id_Ativo=" + Id_Ticker + " AND D.Id_Broker=" + Id_Broker + "  AND B.[Id Book]=" + Id_Book.ToString() + " AND B.[Id Section]=" + Id_Section.ToString();

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            foreach (DataRow row in tablep.Rows)
            {
                txtBuyTotal.Text = Convert.ToInt32(row[1]).ToString("#,###");
                txtSellTotal.Text = Convert.ToInt32(row[2]).ToString("#,###");
                txtNetTotal.Text = Convert.ToInt32(row[0]).ToString("#,###");
            }
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (Convert.ToDouble(txtBuyAloc.Text.Replace(".", "") + "0") != Convert.ToDouble(txtBuyTotal.Text.Replace(".", "") + "0") && radFixed.Checked==true)
            {
                MessageBox.Show("Can't do partial alocations. Check Remaining shares!");
            }
            else
                if (Convert.ToDouble(txtSellAloc.Text.Replace(".", "") + "0") != Convert.ToDouble(txtSellTotal.Text.Replace(".", "") + "0") && radFixed.Checked == true)
                {
                    MessageBox.Show("Can't do partial alocations. Check Remaining shares!");
                }
                else 
                {
                    UpdateOverride();
                }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radModel_CheckedChanged(object sender, EventArgs e)
        {
            if (radModel.Checked == true) { grpModel.Enabled = true; rdMH.Checked = true; } else { grpModel.Enabled = false; };
        }

        private void radFixed_CheckedChanged(object sender, EventArgs e)
        {
            if (radFixed.Checked == true) { grpFixed.Enabled = true; } else { grpFixed.Enabled = false; };
            LoadQuantities();
            txtSellRemain.Text = "";
            txtBuyRemain.Text = "";
            UpdateTotals();
        }

        private void radRemoveOverride_CheckedChanged(object sender, EventArgs e)
        {
            grpModel.Enabled = false;
            grpFixed.Enabled = false;
        }

        private void radRemoveAloc_CheckedChanged(object sender, EventArgs e)
        {
            grpModel.Enabled = false;
            grpFixed.Enabled = false;
        }


        private void UpdateTotals()
        {
            int TotalPurchases = 0;
            int TargetPurchases = 0;
            if (Valida.IsNumeric(txtBuyMH.Text.Replace(".", "")) == true) { TotalPurchases = TotalPurchases + Convert.ToInt32(txtBuyMH.Text.Replace(".", "")); };
            if (Valida.IsNumeric(txtBuyNFund.Text.Replace(".", "")) == true) { TotalPurchases = TotalPurchases + Convert.ToInt32(txtBuyNFund.Text.Replace(".", "")); };
            if (Valida.IsNumeric(txtBuyBravo.Text.Replace(".", "")) == true) { TotalPurchases = TotalPurchases + Convert.ToInt32(txtBuyBravo.Text.Replace(".", "")); };

            if (Valida.IsNumeric(txtBuyTotal.Text.Replace(".", "")) == true) { TargetPurchases = Convert.ToInt32(txtBuyTotal.Text.Replace(".", "")); };

            txtBuyAloc.Text = TotalPurchases.ToString("#,###");
            txtBuyRemain.Text = Convert.ToInt32(TargetPurchases - TotalPurchases).ToString("#,###");

            int TotalSales = 0;
            int TargetSales = 0;
            if (Valida.IsNumeric(txtSellMH.Text.Replace(".", "")) == true) { TotalSales = TotalSales + Convert.ToInt32(txtSellMH.Text.Replace(".", "")); };
            if (Valida.IsNumeric(txtSellNFund.Text.Replace(".", "")) == true) { TotalSales = TotalSales + Convert.ToInt32(txtSellNFund.Text.Replace(".", "")); };
            if (Valida.IsNumeric(txtSellBravo.Text.Replace(".", "")) == true) { TotalSales = TotalSales + Convert.ToInt32(txtSellBravo.Text.Replace(".", "")); };

            if (Valida.IsNumeric(txtSellTotal.Text.Replace(".", "")) == true) { TargetSales = Convert.ToInt32(txtSellTotal.Text.Replace(".", "")); };

            txtSellAloc.Text = TotalSales.ToString("#,###");
            txtSellRemain.Text = Convert.ToInt32(TargetSales - TotalSales).ToString("#,###");

        }
  
#region TextBoxInputControl

        private void txtBuyMH_TextChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void txtBuyTop_TextChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void txtBuyBravo_TextChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void txtSellMH_TextChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void txtSellTop_TextChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void txtSellBravo_TextChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }
        
        private void txtBuyMH_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtBuyMH.Text) == false) { txtBuyMH.Text = ""; };
            if (txtBuyMH.Text != "") { if (Valida.IsNumeric(txtBuyMH.Text) == false) { txtBuyMH.Text = ""; };};
        }

        private void txtBuyTop_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtBuyNFund.Text) == false) { txtBuyNFund.Text = ""; };
            if (txtBuyNFund.Text != "") { if (Valida.IsNumeric(txtBuyNFund.Text) == false) { txtBuyNFund.Text = ""; };};
        }

        private void txtBuyBravo_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtBuyBravo.Text) == false) { txtBuyBravo.Text = ""; };
            if (txtBuyBravo.Text != "") { if (Valida.IsNumeric(txtBuyBravo.Text) == false) { txtBuyBravo.Text = ""; };};
        }

        private void txtSellMH_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtSellMH.Text) == false) { txtSellMH.Text = ""; };
            if (txtSellMH.Text != "") { if (Valida.IsNumeric(txtSellMH.Text) == false) { txtSellMH.Text = ""; };};
        }

        private void txtSellTop_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtSellNFund.Text) == false) { txtSellNFund.Text = ""; };
            if (txtSellNFund.Text != "") { if (Valida.IsNumeric(txtSellNFund.Text) == false) { txtSellNFund.Text = ""; };};
        }

        private void txtSellBravo_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(txtSellBravo.Text) == false) { txtSellBravo.Text = ""; };
            if (txtSellBravo.Text != "") { if (Valida.IsNumeric(txtSellBravo.Text) == false) { txtSellBravo.Text = ""; };};
        }

        private void txtBuyRemain_TextChanged(object sender, EventArgs e)
        {
            if (txtBuyRemain.Text == "") { txtBuyRemain.ForeColor = Color.Black; } else { txtBuyRemain.ForeColor = Color.Red; };
        }

        private void txtSellRemain_TextChanged(object sender, EventArgs e)
        {
            if (txtSellRemain.Text == "") { txtSellRemain.ForeColor = Color.White; } else { txtSellRemain.ForeColor = Color.Red; };
        }

#endregion


    }
}