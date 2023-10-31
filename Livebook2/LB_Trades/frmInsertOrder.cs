using System;
using System.Data;
using System.Windows.Forms;
using LiveDLL;



namespace LiveBook
{
    public partial class frmInsertOrder : LBForm
    {
        old_Conn curConn = new old_Conn();
        DataTable TableCache;

        public frmInsertOrder()
        {
            InitializeComponent();
        }

        private void txtpress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ProcessTabKey(true);

        }

        private void txtPrice_Leave(object sender, EventArgs e)
        {
            decimal testa_pos;

            if (decimal.TryParse(txtQtd.Text, out testa_pos))
            {
                testa_pos = Convert.ToDecimal(this.txtQtd.Text);
                if (testa_pos < 0)
                {
                    cmdInsert_Order_Neg.Enabled = true;
                    cmdInsert_Order.Enabled = false;
                }
                else
                {
                    cmdInsert_Order_Neg.Enabled = false;
                    cmdInsert_Order.Enabled = true;
                }

                if (this.txtPrice.Text != "")
                {
                    decimal Preco = Convert.ToDecimal(txtPrice.Text);
                    this.txtPrice.Text = Preco.ToString("##,##0.00#######");
                }
                if (this.txtQtd.Text != "")
                {
                    decimal qtd = Convert.ToDecimal(this.txtQtd.Text);
                    this.txtQtd.Text = qtd.ToString("##,##0.00#######");
                }

                if (this.txtCash.Text != "")
                {
                    decimal vfin = Convert.ToDecimal(this.txtCash.Text);
                    this.txtCash.Text = vfin.ToString("##,##0.00#######");
                }
            }
            else
            {
                txtQtd.Text = "0,00";
            }
        }

        private void cmdInsert_Order_Click(object sender, EventArgs e)
        {
            Insert_order();
        }

        private void cmdInsert_Order_Neg_Click(object sender, EventArgs e)
        {
            Insert_order();
        }

        private void Insert_order()
        {
            int IdSecurity;

            using (newNestConn curConn = new newNestConn())
            {
                if (this.txtPrice.Text == "")
                {
                    txtPrice.Text = "0";
                    this.txtCash.Text = "0";
                }
                cmbTicker.Focus();

                if (cmbTicker.SelectedValue == null)
                {
                    MessageBox.Show("Invalid Ticker!");
                }
                else
                {
                    IdSecurity = Convert.ToInt32(this.cmbTicker.SelectedValue.ToString());


                    if (this.txtQtd.Text != "")
                    {

                        String SQLString;

                        Decimal Lote_Negociacao = Convert.ToDecimal(TbAtivos_GetValue(Convert.ToInt32(cmbTicker.SelectedValue.ToString()), "Lote_Negociacao"));

                        string divisao = Convert.ToString(Convert.ToDouble(this.txtQtd.Text) / Convert.ToDouble(Lote_Negociacao));
                        decimal Quantidade;
                        decimal Price;
                        decimal CheckPrice;
                        string data = DateTime.Now.ToString("yyyyMMdd");

                        SQLString = "select Return_Value from FCN_GET_PRICE(" + IdSecurity.ToString() + ",'" + data + "',1,0,2,0,1,0)";
                        Quantidade = Convert.ToDecimal(this.txtQtd.Text);
                        Price = Convert.ToDecimal(this.txtPrice.Text);
                        int resposta = 0;

                        string Check_Price_String = curConn.Execute_Query_String(SQLString);
                        if (Check_Price_String != "")
                        {
                            CheckPrice = (decimal)Convert.ToDouble(Check_Price_String);
                        }
                        else
                        {
                            CheckPrice = 0;
                        }
                        int resposta2;
                        if (Price < 0)
                        {
                            resposta2 = Convert.ToInt32(MessageBox.Show("The price is negative. Would you like to insert anyway?", "Insert Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                        }
                        else
                        {
                            resposta2 = 6;
                        }

                        if (resposta2 == 6)
                        {
                            if (CheckPrice > 0)
                            {

                                if (Math.Abs(Price / CheckPrice - 1) > Convert.ToDecimal(0.1))
                                {
                                    resposta = Convert.ToInt32(MessageBox.Show("The price differs more than 10% from last price. Would you like to insert anyway?", "Insert Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                                }
                                else
                                {
                                    resposta = 6;
                                }

                            }
                            else
                            {
                                resposta = 6;
                            }
                        }

                        if (resposta == 6)
                        {

                            if (this.cmBroker.SelectedValue != null)
                            {

                                double Id_Account = Convert.ToDouble(this.cmBroker.SelectedValue.ToString());
                                string id_oldPortfolio = this.cmbportfolio.SelectedValue.ToString();

                                string Id_Broker = curConn.Execute_Query_String("Select Id_Broker from dbo.Tb007_Accounts Where Id_Account = " + Id_Account.ToString());

                                int retorno;

                                int Id_Instrumento = Convert.ToInt32(TbAtivos_GetValue(Convert.ToInt32(cmbTicker.SelectedValue.ToString()), "IdInstrument"));
                                if (Id_Instrumento != 12)
                                {
                                    //retorno = Negocios.Insert_Order(0, Id_Account, false, Convert.ToInt32(this.cmbTicker.SelectedValue.ToString()), Quantidade, Price, this.dtpExpiration.Value.ToString("yyyy-MM-dd"), Convert.ToInt32(Id_Broker.ToString()), Convert.ToInt32(this.cmbBook.SelectedValue.ToString()), Convert.ToInt32(this.cmbSection.SelectedValue.ToString()), Convert.ToInt32(this.cmbOrder_Type.SelectedValue.ToString()), Id_User);
                                    retorno = Negocios.Insert_Order(0, Id_Account, false, Convert.ToInt32(this.cmbTicker.SelectedValue.ToString()), Quantidade, Price, this.dtpExpiration.Value.ToString("yyyy-MM-dd"), Convert.ToInt32(Id_Broker.ToString()), Convert.ToInt32(this.cmbBook.SelectedValue.ToString()), Convert.ToInt32(this.cmbSection.SelectedValue.ToString()), 0);
                                }
                                else
                                {
                                    retorno = Negocios.Insert_Order(0, Id_Account, false, Convert.ToInt32(this.cmbTicker.SelectedValue.ToString()), Quantidade, Price, this.dtpExpiration.Value.ToString("yyyy-MM-dd"), Convert.ToInt32(Id_Broker.ToString()), Convert.ToInt32(this.cmbBook.SelectedValue.ToString()), Convert.ToInt32(this.cmbSection.SelectedValue.ToString()), 0, 1, Convert.ToDecimal(txtSpotPrice.Text), Convert.ToDecimal(txtPriceWBrokerage.Text), Convert.ToDecimal(txtSpotPriceWBrokerage.Text));
                                    
                                }

                                switch (retorno)
                                {
                                    case 1:
                                        this.dtpExpiration.Value = Convert.ToDateTime(DateTime.Now);
                                        this.txtQtd.Text = "";
                                        this.txtCash.Text = "";
                                        //Carrega_Portfolio();
                                        break;
                                    case 0:
                                        MessageBox.Show("Verifies the data of Insertion!");
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Select the Broker");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Insert a Valid Amount!");
                    }
                }
            }
        }



        private void frmInsertOrder_Load(object sender, EventArgs e)
        {
            this.cmbTicker.SelectedIndexChanged -= new System.EventHandler(this.cmbTicker_SelectedIndexChanged);

            Valida_Menu();
            LoadSecurityTable();
            Carrega_Insert_order();

            this.cmbTicker.SelectedIndexChanged += new System.EventHandler(this.cmbTicker_SelectedIndexChanged);

        }
        public void Carrega_Insert_order()
        {
            Carrega_Portfolio();
            //Tipo de Mercado - Normal, after...
            // LiveDLL.FormUtils.LoadCombo(this.cmbOrder_Type, "Select Id_Tipo_Mercado,Tipo_Mercado from  Tb114_Tipo_Mercados", "Id_Tipo_Mercado", "Tipo_Mercado", 99);

            //Ativos
            //Ativos

            LB_Utils.carregacombo_Table(this.cmbTicker, TableCache, "IdSecurity", "NestTicker", 99);
            //Book
            LiveDLL.FormUtils.LoadCombo(this.cmbBook, "Select Id_Book, Book from dbo.Tb400_Books WHERE id_book in (26,27,28,29) ORDER BY Book", "Id_Book", "Book", 99);

            Carrega_Section();
            //Brooker
        }
        public void Carrega_Section()
        {
            int valor;
            try
            {
                if (curUtils.IsNumeric(cmbBook.SelectedValue))
                {

                    valor = Convert.ToInt32(cmbBook.SelectedValue);
                    //SubEstrategia
                    LiveDLL.FormUtils.LoadCombo(this.cmbSection, "Select Id_Section,Section from VW_Book_Strategies where Id_Book =" + valor + "ORDER BY Section", "Id_Section", "Section", 99);
                }
            }
            catch (Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }
        }

        public void Carrega_Account()
        {
            int valor;
            try
            {
                if (cmBroker.Items.Count > 0)
                    cmBroker.DataSource = null;

                if (curUtils.IsNumeric(cmbportfolio.SelectedValue))
                {
                    valor = Convert.ToInt32(cmbportfolio.SelectedValue);
                    //SubEstrategia
                    //LiveDLL.FormUtils.LoadCombo(this.cmBroker, "Select Id_Account,Nome from VW_Account_Broker Where Id_Portfolio = " + valor + " order by Show_Prefered desc,Nome asc", "Id_Account", string.Concat("Nome", "Id_Account"), 99);
                    LiveDLL.FormUtils.LoadCombo(this.cmBroker, "Select Id_Account,Nome,nome + ' - ' + convert(varchar,Id_Account) as DisplayName from VW_Account_Broker Where Id_Portfolio = " + valor + " order by Nome asc", "Id_Account", "DisplayName", 99);
                }
            }
            catch (Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }
        }

        //public event LoadGrid Carrega_Grid_Open;
        public delegate void LoadGrid();


        public void Carrega_Portfolio()
        {
            try
            {
                LiveDLL.FormUtils.LoadCombo(this.cmbportfolio, "Select Id_Portfolio,Port_Name FROM dbo.Tb002_Portfolios Where Discountinued <> 1 and Id_Port_Type =1 order by Port_Name", "Id_Portfolio", "Port_Name", 99);
            }
            catch (Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

                MessageBox.Show("Error when loading the Combo Portfólios ");
            }
        }

        private void Valida_Menu()
        {
            if (curUtils.Verifica_Acesso(1) || curUtils.Verifica_Acesso(2))
            {
                this.Enabled = true;
            }
        }

        private void txtQtd_TextChanged(object sender, EventArgs e)
        {
            calcula_VF("qtd");

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            calcula_VF("qtd");

        }

        private void calcula_VF(string sender)
        {
            decimal Lote_Padrao;
            if (sender.ToString() == "qtd")
            {
                decimal VF;
                decimal QTD;
                decimal Price;

                if (decimal.TryParse(txtQtd.Text, out QTD) && decimal.TryParse(txtPrice.Text, out Price) && cmbTicker.SelectedValue != null)
                {
                    Lote_Padrao = Convert.ToDecimal(TbAtivos_GetValue(Convert.ToInt32(cmbTicker.SelectedValue.ToString()), "RoundLot"));


                    VF = (Convert.ToDecimal(txtPrice.Text) * (QTD / Lote_Padrao));
                    txtCash.Text = Convert.ToString(VF);
                }
                else
                {
                    txtCash.Text = "";
                }
            }
        }

        public void Preenche_Dados_Posicao(int Id_Position, string table)
        {

            int Id_Ticker;
            int Id_Book;
            int Id_Section;
            int Id_Portfolio;
            int Id_Broker;
            int Id_Ticker_Type;
            double Posicao;
            double Ultimo;
            double Moeda;
            string Temp_Table_Name;

            Temp_Table_Name = "";

            if (table.ToString() == "NESTRT.dbo.FCN_Posicao_Atual()")
            { Temp_Table_Name = table; }
            else
            { Temp_Table_Name = table + " (nolock)"; }


            string SQLString = "Select * from " + table + " Where [Id Position] = " + Id_Position;

            DataTable curTable = curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                try
                {
                    Id_Portfolio = Convert.ToInt32(curRow[1].ToString());
                    Id_Ticker = Convert.ToInt32(curRow[3].ToString());
                    Id_Book = Convert.ToInt32(curRow[164].ToString());
                    Id_Section = Convert.ToInt32(curRow[172].ToString());
                    Posicao = Convert.ToInt32(curRow[15]);
                    Id_Ticker_Type = Convert.ToInt32(curRow[6]);

                    if (Id_Ticker_Type == 5)
                    {
                        Id_Broker = 15;
                    }
                    else
                    {
                        Id_Broker = 1;
                    }

                    if (curRow[17].ToString() != "")
                        Ultimo = Convert.ToDouble(curRow[19]);
                    else
                        Ultimo = 0;

                    Moeda = Convert.ToInt32(curRow[31]);

                    if (Id_Portfolio != 0)
                    {
                        if (Id_Portfolio == 4)
                        {
                            if (Moeda == 900)
                            {
                                cmbportfolio.SelectedValue = 5;
                            }
                            else
                            {
                                cmbportfolio.SelectedValue = 6;
                            }
                        }
                        if (Id_Portfolio == 43)
                        {
                            if (Moeda == 900)
                            {
                                cmbportfolio.SelectedValue = 41;
                            }
                            else
                            {
                                cmbportfolio.SelectedValue = 42;
                            }

                        }
                        if (Id_Portfolio == 10)
                        {
                            cmbportfolio.SelectedValue = 11;
                        }

                        if (Id_Portfolio == 38)
                        {
                            cmbportfolio.SelectedValue = 39;
                        }

                        if (Id_Portfolio == 18)
                        {
                            cmbportfolio.SelectedValue = 17;
                        }

                        if (Id_Portfolio == 50)
                        {
                            cmbportfolio.SelectedValue = 51;
                        }

                        if (Id_Portfolio == 60)
                        {
                            cmbportfolio.SelectedValue = 61;
                        }
                    }

                    cmBroker.SelectedValue = Id_Broker;

                    cmbTicker.SelectedValue = Id_Ticker;

                    if (cmbTicker.SelectedValue == null)
                    {
                        LB_Utils.carregacombo_Table(this.cmbTicker, TableCache, "IdSecurity", "NestTicker", 99);
                    }

                    if (Id_Book != 0)
                        cmbBook.SelectedValue = Id_Book;

                    if (Id_Section != 0)
                        cmbSection.SelectedValue = Id_Section;
                    //MessageBox.Show("PASSOU");
                    if (Posicao != 0)
                        txtQtd.Text = Convert.ToDouble(Posicao * -1).ToString("##,##0.00#######");

                    //  if (Ultimo != 0)
                    txtPrice.Text = Convert.ToDouble(Ultimo).ToString("##,##0.00#######");
                    if (this.txtCash.Text != "")
                    {
                        decimal vfin = Convert.ToDecimal(this.txtCash.Text);
                        this.txtCash.Text = vfin.ToString("##,##0.00#######");
                    }
                    if (Convert.ToDouble(Posicao * -1) < 0)
                    {
                        cmdInsert_Order_Neg.Enabled = true;
                        cmdInsert_Order.Enabled = false;
                    }
                    else
                    {
                        cmdInsert_Order_Neg.Enabled = false;
                        cmdInsert_Order.Enabled = true;
                    }
                }
                catch (Exception e)
                {
                    curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

                    //MessageBox.Show("ERRO");
                }
            }
        }

        private void cmbBook_SelectedValueChanged(object sender, EventArgs e)
        {
            Carrega_Section();

        }

        public delegate void FrmClosing();

        private void cmbportfolio_SelectedValueChanged(object sender, EventArgs e)
        {
            Carrega_Account();
        }

        private void cmbTicker_Enter(object sender, EventArgs e)
        {

            string tempText = cmbTicker.Text;
            LB_Utils.carregacombo_Table(this.cmbTicker, TableCache, "IdSecurity", "NestTicker", 99);
            cmbTicker.Text = tempText;

        }

        private void cmbTicker_Leave(object sender, EventArgs e)
        {
            if (cmbTicker.FindStringExact(cmbTicker.Text) == -1)
            {
                MessageBox.Show("Invalid Ticker!");
                cmbTicker.Text = "";
            }
        }

        string TbAtivos_GetValue(int Id_Ticker, string Field)
        {
            DataRow[] foundRows;
            foundRows = TableCache.Select("IdSecurity= " + Id_Ticker);
            int intField;

            switch (Field)
            {
                case "IdSecurity":
                    intField = 0;
                    break;

                case "NestTicker":
                    intField = 1;
                    break;

                case "IdInstrument":
                    intField = 2;
                    break;

                case "RoundLot":
                    intField = 3;
                    break;

                case "RoundLotSize":
                    intField = 4;
                    break;
                default:
                    intField = 0;
                    break;
            }
            string foundRowsGetValueToString = foundRows[0].ItemArray[intField].ToString();

            return foundRowsGetValueToString;
        }

        private void cmbTicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cmbTicker.SelectedValue != null)
            {
                int Id_Instrumento = Convert.ToInt32(TbAtivos_GetValue(Convert.ToInt32(cmbTicker.SelectedValue.ToString()), "IdInstrument"));
                if (Id_Instrumento != 12)
                {
                    label5.Visible = false;
                    txtSpotPrice.Visible = false;
                    label9.Visible = false;
                    label10.Visible = false;
                    txtPriceWBrokerage.Visible = false;
                    txtSpotPriceWBrokerage.Visible = false;
                }
                else
                {
                    label5.Visible = true;
                    txtSpotPrice.Visible = true;
                    label9.Visible = true;
                    label10.Visible = true;
                    txtPriceWBrokerage.Visible = true;
                    txtSpotPriceWBrokerage.Visible = true;
                }
            }

        }

        private void chkIncludeExp_CheckedChanged(object sender, EventArgs e)
        {
            LoadSecurityTable();
        }

        private void LoadSecurityTable()
        {
            if (chkIncludeExp.Checked)
            {
                TableCache = curConn.Return_DataTable("SELECT IdSecurity,NestTicker,IdInstrument,RoundLot,RoundLotSize FROM Tb001_Securities WHERE NOT NestTicker IS NULL order by NestTicker");
            }
            else
            {
                TableCache = curConn.Return_DataTable("SELECT IdSecurity,NestTicker,IdInstrument,RoundLot,RoundLotSize FROM Tb001_Securities WHERE ((Expiration IS NULL) OR Expiration = '19000101' OR (Expiration >= (CONVERT(VARCHAR, GETDATE()-5, 112))) )AND not NestTicker is null  order by NestTicker");
            }
        }



    }
}