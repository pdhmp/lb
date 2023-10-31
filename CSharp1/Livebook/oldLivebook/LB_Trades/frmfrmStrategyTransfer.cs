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
    public partial class frmStrategyTransfer : LBForm
    {
        newNestConn curConn = new newNestConn();
        DataTable TableCache;

        public frmStrategyTransfer()
        {
            InitializeComponent();
        }

       private void frmInsertOrder_Load(object sender, EventArgs e)
        {
           this.cmbportfolio.SelectedIndexChanged -= new System.EventHandler(this.cmbportfolio_SelectedIndexChanged);

           LoadInsertTransaction();

           this.cmbportfolio.SelectedIndexChanged += new System.EventHandler(this.cmbportfolio_SelectedIndexChanged);
           // string StringSQL;

           NestDLL.FormUtils.LoadCombo(this.cmbBookFrom, "Select Id_Book, Book from dbo.Tb400_Books (nolock) ", "Id_Book", "Book", 99);
           Carrega_Section1();
           NestDLL.FormUtils.LoadCombo(this.cmbBookTo, "Select Id_Book, Book from dbo.Tb400_Books (nolock) ", "Id_Book", "Book", 99);
           Carrega_Section2();

           cmbportfolio_SelectedIndexChanged(sender, e);
        }

        public void LoadInsertTransaction()
        {
            Carrega_Portfolio();
        }

        public void Carrega_Portfolio()
        {
            try
            {
                NestDLL.FormUtils.LoadCombo(this.cmbportfolio, "Select Id_Portfolio,Port_Name FROM dbo.Tb002_Portfolios (nolock) Where Discountinued <> 1 and Id_Port_Type =2", "Id_Portfolio", "Port_Name", 99);
            }
            catch(Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

                MessageBox.Show("Error when loading the Combo Portfólios ");
            }
        }

        public event FrmClosing Fechando;
        public delegate void FrmClosing();

        private void cmbportfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbportfolio.SelectedValue != null)
            {
                string StringSQL = "SELECT [Id Ticker] as IdSecurity,Ticker as NestTicker,[Id Book] as IdBook,Book,[Id Section] as IdSection,Section FROM NESTRT.dbo.Tb000_Posicao_Atual (nolock) WHERE [Id Portfolio]=" + cmbportfolio.SelectedValue + " order by Ticker";

                TableCache = curConn.Return_DataTable(StringSQL);

                //NestDLL.FormUtils.LoadCombo(this.cmbTicker, StringSQL, "IdSecurity", "NestTicker", 99);
                LB_Utils.carregacombo_Table(this.cmbTicker, TableCache, "IdSecurity", "NestTicker", 99);
            }
        }

        private void cmbBookTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Section2();
        }

        public void Carrega_Section1()
        {
            int valor;
            try
            {
                if (curUtils.IsNumeric(cmbBookFrom.SelectedValue))
                {

                    valor = Convert.ToInt32(cmbBookFrom.SelectedValue);
                    //SubEstrategia
                    NestDLL.FormUtils.LoadCombo(this.cmbSectionFrom, "Select Id_Section,Section from VW_Book_Strategies where Id_Book =" + valor, "Id_Section", "Section", 99);
                }
            }
            catch (Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }
        }

        public void Carrega_Section2()
        {
            int valor;
            try
            {
                if (curUtils.IsNumeric(cmbBookTo.SelectedValue))
                {

                    valor = Convert.ToInt32(cmbBookTo.SelectedValue);
                    //SubEstrategia
                    NestDLL.FormUtils.LoadCombo(this.cmbSectionTo, "Select Id_Section,Section from VW_Book_Strategies where Id_Book =" + valor, "Id_Section", "Section", 99);
                }
            }
            catch (Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());
            }
        }

        private void cmbBookFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Section1();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdInsert_Order_Click(object sender, EventArgs e)
        {
            TransferPosition();
        }

        int TransferPosition()
        {
            string StringSql;
            string StringSql2;
            float IdSecurity =Convert.ToInt32(cmbTicker.SelectedValue);
            int Id_Fund = Convert.ToInt32(cmbportfolio.SelectedValue.ToString());
            int Id_Account=0;
            int IdBookFrom;
            int IdSectionFrom;
            int IdBookTo;
            int IdSectionTo;
            decimal Quantity;
            decimal Price;
            decimal Cash;
            int IdCash =0;


            IdBookFrom = Convert.ToInt32(cmbBookFrom.SelectedValue.ToString());
            IdBookTo = Convert.ToInt32(cmbBookTo.SelectedValue.ToString());

            IdSectionFrom = Convert.ToInt32(cmbSectionFrom.SelectedValue.ToString());
            IdSectionTo = Convert.ToInt32(cmbSectionTo.SelectedValue.ToString());

            int curCurrency = curConn.Return_Int("SELECT IdCurrency FROM NESTDB.dbo.Tb001_Securities (nolock) WHERE IdSecurity=" + IdSecurity);
            decimal RoundLot = Convert.ToDecimal(curConn.Return_Double("SELECT RoundLot FROM NESTDB.dbo.Tb001_Securities (nolock) WHERE IdSecurity=" + IdSecurity));
          
            switch (Id_Fund)
            {
                case 5:
                case 6:
                    if (curCurrency == 900) { Id_Account = 1046; IdCash = 1844; }
                    if (curCurrency == 1042) { Id_Account = 1060; IdCash = 5791; }
                    break;
                case 10:
                case 11:
                    if (curCurrency == 900) { Id_Account = 1073; IdCash = 1844; }
                    break;
                case 38:
                case 39:
                    if (curCurrency == 900) { Id_Account = 1211; IdCash = 1844; }
                    break;
                case 41:
                case 42:
                    if (curCurrency == 900) { Id_Account = 1086; IdCash = 1844;}
                    if (curCurrency == 1042) { Id_Account = 1148; IdCash = 5791; }
                    break;

            }

            Quantity = Convert.ToDecimal(txtQuantity.Text.ToString());

            Price = Convert.ToDecimal(txtPrice.Text.ToString());
            Cash = Price * (Quantity / RoundLot);

            Cash = Cash * -1;

            int retorno; 

            StringSql = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2) " +
                                " VALUES (11,'" + dtpDate.Value.ToString("yyyyMMdd") + "','" + dtpDate.Value.ToString("yyyyMMdd") + "'," + Id_Account.ToString() + "," + IdSecurity.ToString() + "," + IdBookFrom.ToString() + "," + IdSectionFrom.ToString() + "," + Quantity.ToString().Replace(".", "").Replace(",", ".") + "," + Id_Account.ToString() + "," + IdCash + ",5,1," + Cash.ToString().Replace(".", "").Replace(",", ".") + ") ; Select @@IDENTITY";

            retorno = curConn.Return_Int(StringSql);

            Quantity = Quantity * -1;
            Cash = Cash * -1;

            StringSql2 = " INSERT INTO [NESTDB].[dbo].[Tb700_Transactions](Transaction_Type,[Trade_Date],Settlement_Date,Id_Account1,Id_Ticker1,[Id Book1],[Id Section1],Quantity1,Id_Account2,Id_Ticker2,[Id Book2],[Id Section2],Quantity2,IdRelation) " +
                                " VALUES (11,'" + dtpDate.Value.ToString("yyyyMMdd") + "','" + dtpDate.Value.ToString("yyyyMMdd") + "'," + Id_Account.ToString() + "," + IdSecurity.ToString() + "," + IdBookTo.ToString() + "," + IdSectionTo.ToString() + "," + Quantity.ToString().Replace(".", "").Replace(",", ".") + "," + Id_Account.ToString() + "," + IdCash + ",5,1," + Cash.ToString().Replace(".", "").Replace(",", ".") + "," + retorno + ") ;";

            retorno = curConn.ExecuteNonQuery(StringSql2);

           return retorno;
        }

        private void txtQuantity_Leave(object sender, EventArgs e)
        {
            if (txtQuantity.Text != "")
            {
                decimal Qtd = Convert.ToDecimal(this.txtQuantity.Text);
                this.txtQuantity.Text = Qtd.ToString("##,##0.00#######");
            }
        }

        public void Preenche_Dados_Posicao(int Id_Position, string table)
        {
            string Temp_Table_Name;

            int IdPortfolio;
            int IdSecurity;
            int IdBook;
            int IdSection;
            int IdCurrency;
            double Position;
            double ClosePrice;
            DateTime CloseDate;

            Temp_Table_Name = "";

            if (table.ToString() == "NESTRT.dbo.FCN_Posicao_Atual()")
            { Temp_Table_Name = table; }
            else
            { Temp_Table_Name = table + " (nolock)"; }


            string SQLString = "Select * from " + table + " Where [Id Position] = " + Id_Position;

            try
            {
                DataTable curTable = curConn.Return_DataTable(SQLString);
                foreach (DataRow curRow in curTable.Rows)
                {
                    IdPortfolio = Convert.ToInt32(curRow[1].ToString());
                    IdSecurity = Convert.ToInt32(curRow[3].ToString());
                    IdBook = Convert.ToInt32(curRow[164].ToString());
                    IdSection = Convert.ToInt32(curRow[172].ToString());
                    Position = Convert.ToInt32(curRow[15]);
                    IdCurrency = Convert.ToInt32(curRow[31]);
                    CloseDate = Convert.ToDateTime(curRow["Close_Date"]);
                    ClosePrice = Convert.ToInt32(curRow["Last Admin"]);

                     if (IdPortfolio != 0)
                    cmbportfolio.SelectedValue = IdPortfolio;
 
                    if (IdSecurity != 0)
                        cmbTicker.SelectedValue = IdSecurity;

                    if (IdBook != 0)
                        cmbBookFrom.SelectedValue = IdBook;

                    if (IdSection != 0)
                        cmbSectionFrom.SelectedValue = IdSection;

                    if (Position != 0)
                        txtQuantity.Text = Convert.ToDouble(Position * -1).ToString("##,##0.00#######");

                    if (ClosePrice != 0)
                        txtPrice.Text = Convert.ToDouble(ClosePrice * -1).ToString("##,##0.00#######");

                    dtpDate.Value = CloseDate;


                }
            }
            catch(Exception e)
            {
            
            }
        }
    }
}