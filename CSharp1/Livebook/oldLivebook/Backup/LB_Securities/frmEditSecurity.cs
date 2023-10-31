using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using SGN.Validacao;
using SGN.Business;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace SGN
{

    public partial class frmEditSecurity : LBForm
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        DataTable tablep = new DataTable();


        public frmEditSecurity()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radMonth.Checked = true;
            Carrega_Lista();
            Carrega_Combos();
            Valida_Menu();

        }

        private void Valida_Menu()
        {
            if (Valida.Verifica_Acesso(11))
            {
                cmdInsertNew.Enabled = true;
                cmdUpdate.Enabled = true;
                cmdInsertCopy.Enabled = true;
                cmdHistData.Enabled = true;
            }
            else
            {
                cmdInsertNew.Enabled = false;
                cmdUpdate.Enabled = false;
                cmdInsertCopy.Enabled = false;
                cmdHistData.Enabled = false;
            }
        }

        private void Carrega_Combos()
        {
            CargaDados.carregacombo(this.cmbd_IdIssuer, "Select IdIssuer,IssuerName from Tb000_Issuers", "IdIssuer", "IssuerName", 0);
            CargaDados.carregacombo(this.cmbd_IdPriceTable, "Select Id_Price_Table,Price_Table_Name from Tb024_Price_Table", "Id_Price_Table", "Price_Table_Name", 0);
            CargaDados.carregacombo(this.cmbd_IdCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 0);
            CargaDados.carregacombo(this.cmbd_IdPremiumCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 0);
            CargaDados.carregacombo(this.cmbd_IdStrikeCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 0);
            CargaDados.carregacombo(this.cmbPrimaryEx, "Select Id_Mercado, Cod_Mercado from Tb107_Mercados", "Id_Mercado", "Cod_Mercado", 0);
            CargaDados.carregacombo(this.cmbd_IdUnderlying, "Select IdSecurity,NestTicker from dbo.Tb001_Securities where IdPriceTable <> 7 ORDER BY NestTicker", "IdSecurity", "NestTicker", 0);
            CargaDados.carregacombo(this.cmbd_IdInstrument, "Select Id_Instrumento,Instrumento from Tb029_Instrumentos", "Id_Instrumento", "Instrumento", 0);
            CargaDados.carregacombo(this.cmbd_IdAssetClass, "Select Id_Classe_Ativo,Classe_Ativo  from Tb028_Classe_Ativo ", "Id_Classe_Ativo", "Classe_Ativo", 0);
            CargaDados.carregacombo(this.cmbd_HistUpdateFrequency, "Select Id_Frequencia,Frequencia from Tb105_Frequencia_Atualizacao  ", "Id_Frequencia", "Frequencia", 0);
            CargaDados.carregacombo(this.cmbd_HistUpdateSource, "Select Id_Sistemas_Informacoes,Descricao  from Tb102_Sistemas_Informacoes WHERE (Hist = 1 or RT is null)", "Id_Sistemas_Informacoes", "Descricao", 0);
            CargaDados.carregacombo(this.cmbd_RTUpdateSource, "Select Id_Sistemas_Informacoes,Descricao  from Tb102_Sistemas_Informacoes WHERE(RT = 1 or RT is null)", "Id_Sistemas_Informacoes", "Descricao", 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Carrega_Lista();
        }

        public void Carrega_Lista()
        {

            string strWhere = "";
            string[] strFields = new string[1];

            string SQLString = "Select convert(varchar(10), A.IdSecurity,102) + '-' + convert(char(5), A.IdPriceTable,102) as IdSecurity,  A.NestTicker " +
                                " FROM dbo.Tb001_Securities A " +
                                " WHERE IdSecurity>0 ";

            if (chkDiscontinued.Checked != true)
            {
                SQLString = SQLString + " AND ((A.Expiration >= convert(varchar,GETDATE(),112)) OR (A.Expiration IS NULL) OR (A.Expiration ='19000101'))";
            }

            if (txtsearch.Text.Trim() != "")
            {
                strFields = txtsearch.Text.ToString().Split(' ');
                for (int f = 0; f < strFields.Length; f++)
                {
                    if (strFields[f] != "") 
                    { 
                        strWhere = strWhere + " AND (A.NestTicker like '%" + strFields[f] + "%'" +
                        " OR A.SecName Like '%" + strFields[f] + "%' OR A.ExchangeTicker Like '%" + strFields[f] + "%'" +
                        " OR ReutersTicker Like '%" + strFields[f] + "%' OR BloombergTicker Like '%" + strFields[f] + "%'" +
                        " OR Imagineticker like '%" + strFields[f] + "%'" +
                        " OR convert(varchar,IdSecurity)='" + strFields[f] + "')";
                    }
                }
            }

            SQLString = SQLString + strWhere + " order by IdInstrument, NestTicker";

           DataTable table = CargaDados.curConn.Return_DataTable(SQLString);

            lstAtivos.DataSource = table;
            lstAtivos.DisplayMember = "NestTicker";
            lstAtivos.ValueMember = "IdSecurity";
            //lstAtivos.SelectedIndexChanged(null, null);
        }

        private void Carrega_Grid_Prices()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            DateTime DateToGet = new DateTime();
            string[] parte;
            int Id_Ticker;

            if (lstAtivos.SelectedValue == null) { return; }
            if (cmbPrice_Type.SelectedValue == null) { return; }
            if (cmbSource.SelectedValue == null) { return; }

            string Id_Price_Type = cmbPrice_Type.SelectedValue.ToString();
            string Id_Source = cmbSource.SelectedValue.ToString();

            if (Id_Price_Type == "System.Data.DataRowView") { return; };
            if (Id_Source == "System.Data.DataRowView") { return; };

            parte = lstAtivos.SelectedValue.ToString().Split('-');

            if (parte.Length > 1)
            {
                Id_Ticker = Convert.ToInt32(parte[0]);
            }
            else
            {
                return;
            }

            if (radAll.Checked == true) { DateToGet = Convert.ToDateTime("01/01/1900"); };
            if (radWeek.Checked == true) { DateToGet = DateTime.Now.AddDays((int)DateTime.Now.DayOfWeek - 9); };
            if (radMonth.Checked == true) { DateToGet = DateTime.Now.AddDays(1 - (int)DateTime.Now.Day);};
            if (radYear.Checked == true) { DateToGet = Convert.ToDateTime("01/01/" + DateTime.Now.Year.ToString());};

            string PriceTableName = Valida.Retorna_Tabela_Preco_Trade(Id_Ticker, 0);

            SQLString = "Select A.Data_Hora_Reg [Date], A.Valor AS Value, B.Preco as [Price Type], C.Descricao AS Source, A.Data_Hora_Ins AS [Time Inserted]" +
                        "FROM " + PriceTableName + " A " +
                        "LEFT JOIN dbo.Tb116_Tipo_Preco B ON A.Tipo_Preco=B.Id_Tipo_Preco " +
                        "LEFT JOIN dbo.Tb102_Sistemas_Informacoes C ON A.Source=C.Id_Sistemas_Informacoes " +
                        "WHERE Id_Ativo=" + Id_Ticker.ToString() + " AND Data_Hora_Reg>='" + DateToGet.ToString("yyyy-MM-dd") + "'" +
                        "ORDER BY Data_Hora_Reg";

            if (Id_Price_Type != "-1") { SQLString = SQLString.Replace("WHERE", "WHERE Tipo_Preco=" + Id_Price_Type + " AND "); };
            if (Id_Source != "-1") { SQLString = SQLString.Replace("WHERE", "WHERE Source=" + Id_Source + " AND "); };

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dgPrices.DataSource = tablep;
            
            tablep.Dispose();

            dgPrices.Columns["Date"].DisplayIndex = 0;
            dgPrices.Columns["Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrices.Columns["Date"].DefaultCellStyle.Format = "dd-MMM-yy";

            dgPrices.Columns["Value"].DisplayIndex = 1;
            dgPrices.Columns["Value"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgPrices.Columns["Price Type"].DisplayIndex = 2;
            dgPrices.Columns["Price Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgPrices.Columns["Source"].DisplayIndex = 3;
            dgPrices.Columns["Source"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgPrices.Columns["Time Inserted"].DisplayIndex = 4;
            dgPrices.Columns["Time Inserted"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPrices.Columns["Time Inserted"].DefaultCellStyle.Format = "dd-MMM-yy HH:mm:ss";
            //dgPrices.Columns["Ticker"].Width = 87;
            lblTicker.Text = Id_Ticker.ToString();

        }

        private void Carrega_Ativos(int Id_Ativo)
        {
            string SQLString = "SELECT * FROM Tb001_Securities_Fixed Where IdSecurity = " + Id_Ativo;

            foreach (object curGroupControl in this.tabCtrSecurities.TabPages[0].Controls)
            {
                if (curGroupControl.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    if(curGroupControl != groupVariableInfo)
                        LoadGroup((GroupBox)curGroupControl, SQLString);
                }
            }

            switch ((int)cmbd_IdInstrument.SelectedValue)
            {
                case 4:
                case 12:
                case 16:
                    groupOPT.Enabled = true;
                    groupOPT.Enabled = false;
                    break;
                case 15:
                    groupOPT.Enabled = true;
                    groupOPT.Enabled = false;
                    break;
                case 5:
                case 13:
                    groupOPT.Enabled = true;
                    groupOPT.Enabled = false;
                    break;
                case 9:
                    groupOPT.Enabled = false;
                    groupOPT.Enabled = false;
                    break;
                case 3:
                    groupOPT.Enabled = true;
                    groupOPT.Enabled = true;
                    break;
                case 6:
                case 11:
                    groupOPT.Enabled = false;
                    groupOPT.Enabled = false;
                    break;
                default:
                    groupOPT.Enabled = false;
                    groupOPT.Enabled = false;
                    break;
            }

            if (Convert.ToDateTime(dtpd_Expiration.Value) < DateTime.Now.Subtract(new TimeSpan(20, 0, 0, 0)) && NestDLL.NUserControl.Instance.User_Id != 13 && dtpd_Expiration.Value != Convert.ToDateTime("1/1/1900"))
            {
                dtpd_Expiration.Enabled = false;
            }
            else
            {
                dtpd_Expiration.Enabled = true;
            }
            ReloadlstAvDates();
        }

        void ReloadlstAvDates()
        {
            this.lstAvDates.SelectedIndexChanged -= new System.EventHandler(this.lstAvDates_SelectedIndexChanged);
            lstAvDates.SelectedIndex = -1;
            CargaDados.carregaList(lstAvDates, "SELECT ValidAsOfDate FROM NESTDB.dbo.Tb001_Securities_Variable WHERE IdSecurity=" + lbld_IdSecurity.Text, "ValidAsOfDate", "ValidAsOfDate");
            this.lstAvDates.SelectedIndexChanged += new System.EventHandler(this.lstAvDates_SelectedIndexChanged);
            lstAvDates.SelectedIndex = lstAvDates.Items.Count - 1;
            lstAvDates_SelectedIndexChanged(this, new EventArgs() );

        }


        private void lstAvDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SQLString = "SELECT * FROM NESTDB.dbo.Tb001_Securities_Variable WHERE IdSecurity=" + lbld_IdSecurity.Text + " AND ValidAsOfDate='" + ((DateTime)lstAvDates.SelectedValue).ToString("yyyy-MM-dd") + "'";

            LoadGroup(groupVariableInfo, SQLString);

        }

        private void LoadGroup(GroupBox curGroup, string SQLString)
        {
            Console.WriteLine(curGroup.Text.ToString() + " -  " + curGroup.Name.ToString());

            using (newNestConn curConn = new newNestConn())
            {
                DataTable curTable = curConn.Return_DataTable(SQLString);
                DataRow curRow = curTable.Rows[0];

                foreach (object curControl in curGroup.Controls)
                {
                    if (curControl.GetType() == typeof(System.Windows.Forms.ComboBox))
                    {
                        ComboBox curCombo = (ComboBox)curControl;
                        string curFieldName = curCombo.Name.Substring(5);

                        if (curCombo.Name.Substring(0, 5) == "cmbd_")
                        {
                            if (curRow[curFieldName].ToString() != "")
                            {
                                curCombo.SelectedValue = Convert.ToInt32(curRow[curFieldName]);
                            }
                            else
                            {
                                curCombo.SelectedValue = 0;
                            }
                        }
                    }

                    if (curControl.GetType() == typeof(System.Windows.Forms.TextBox))
                    {
                        TextBox curTextBox = (TextBox)curControl;
                        string curFieldName = curTextBox.Name.Substring(5);

                        if (curTextBox.Name.Substring(0, 5) == "txtd_")
                        {
                            curTextBox.Text = curRow[curFieldName].ToString();
                        }
                    }

                    if (curControl.GetType() == typeof(System.Windows.Forms.Label))
                    {
                        Label curLabel = (Label)curControl;
                        string curFieldName = curLabel.Name.Substring(5);

                        if (curLabel.Name.Substring(0, 5) == "lbld_")
                        {
                            curLabel.Text = curRow[curFieldName].ToString();
                        }
                    }

                    if (curControl.GetType() == typeof(System.Windows.Forms.DateTimePicker))
                    {
                        DateTimePicker curDateTimePicker = (DateTimePicker)curControl;
                        string curFieldName = curDateTimePicker.Name.Substring(5);

                        if (curDateTimePicker.Name.Substring(0, 5) == "dtpd_")
                        {
                            curDateTimePicker.Value = DateTime.Parse(curRow[curFieldName].ToString());
                        }
                    }

                    if (curControl.GetType() == typeof(System.Windows.Forms.CheckBox))
                    {
                        CheckBox curCheckBox = (CheckBox)curControl;
                        string curFieldName = curCheckBox.Name.Substring(5);

                        if (curCheckBox.Name.Substring(0, 5) == "chkd_")
                        {
                            if (double.Parse(curRow[curFieldName].ToString()) == 1)
                                curCheckBox.Checked = true;
                            else
                                curCheckBox.Checked = false;
                        }
                    }
                }

                if (curGroup.Name == "groupOPT" && curRow["IdInstrument"].ToString() == "3")
                {
                    int OptionType;
                    OptionType = Convert.ToInt32(curRow["OptionType"].ToString());

                    if (OptionType == 1)
                    {
                        rdCall.Checked = true;
                        rdPut.Checked = false;

                    }
                    else
                    {
                        rdPut.Checked = true;
                        rdCall.Checked = false;
                    }
                }

            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            int retorno = Atualiza_Dados();

            if (retorno == 0)
            {
                MessageBox.Show("Error on update. Check Data.");
            }
            else
            {
                MessageBox.Show("Updated.");
                string SQLString = String.Format("DELETE FROM NESTRT.dbo.Tb000_Posicao_Atual WHERE [Id Ticker]={0}", lbld_IdSecurity.Text);
                retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1); 
            }
        }

        private int Atualiza_Dados()
        {
            string SQLString = "";


            // Security Description
            string strObjectTicker;
            if (cmbd_IdUnderlying.SelectedValue != null) 
            { 
                strObjectTicker = cmbd_IdUnderlying.SelectedValue.ToString(); 
            } 
            else 
            {
                strObjectTicker = null;
                MessageBox.Show("Invalid Underlying Selected. Please check data and try again.","NOT UPDATED", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            };

            // Derivatives

            string dataVencimento;
            DateTime data_ex;

            if (groupOPT.Enabled == true || cmbd_IdInstrument.SelectedValue.ToString() == "12" || cmbd_IdInstrument.SelectedValue.ToString() == "4") 
            {
                data_ex = Convert.ToDateTime(dtpd_Expiration.Value.ToString());
                dataVencimento = data_ex.ToString("yyyyMMdd");
            } 
            else 
            { 
                dataVencimento = "19000101";
            };


            

            data_ex = Convert.ToDateTime("1/1/1900");
            data_ex = Convert.ToDateTime(dtpd_TRDate.Value.ToString());
            string DataTR =  data_ex.ToString("yyyyMMdd") ;



            string strADR_Ratio = "null";

            if (txtd_ADRRatio.Text != "") 
            {
                strADR_Ratio = txtd_ADRRatio.Text.Replace(".", "").Replace(",", ".").ToString(); 
            }
            else 
            { 
                strADR_Ratio = "null"; 
            };
            
            // Options
            string Tipo_Cal_Put;
            string strPreco_Exercicio="0";
            string strCurrencyPrize;
            string strStrikeCurrency;
            string strContract_Ratio;

            if (groupOPT.Enabled == true)
            {
                Tipo_Cal_Put = "null";
                if (rdCall.Checked == true) { Tipo_Cal_Put = "1"; }
                if (rdPut.Checked == true) { Tipo_Cal_Put = "0"; }

                if (txtd_Strike.Text != "") 
                { 
                    strPreco_Exercicio = txtd_Strike.Text.Replace(".", "").Replace(",", ".").ToString(); 
                } else 
                { 
                    strPreco_Exercicio = "0"; 
                };
                if (txtd_ContractRatio.Text != "") { strContract_Ratio = txtd_ContractRatio.Text.Replace(".", "").Replace(",", ".").ToString(); } else { strContract_Ratio = "null"; };

                if (cmbd_IdPremiumCurrency.SelectedValue != null) 
                { 
                    strCurrencyPrize = cmbd_IdPremiumCurrency.SelectedValue.ToString(); 
                } 
                else 
                { 
                    strCurrencyPrize = "null"; 
                };

                if (cmbd_IdStrikeCurrency.SelectedValue != null) 
                { 
                    strStrikeCurrency = cmbd_IdStrikeCurrency.SelectedValue.ToString(); 
                } 
                else 
                {
                    strStrikeCurrency = "null"; 
                };
            }
            else
            {
                Tipo_Cal_Put = "null";
                strPreco_Exercicio = "0";
                strCurrencyPrize = "null";
                strStrikeCurrency = "null";
                strContract_Ratio = "1";
            };

            //Identifiers


            //Trade Information
            string strUpFreq;
            string strPrimaryEx;
            string strLote_Padrao;
            string strLote_Negociacao;
            string strCurrency;

            if (cmbd_HistUpdateFrequency.SelectedValue != null) { strUpFreq = cmbd_HistUpdateFrequency.SelectedValue.ToString(); } else { strUpFreq = "0"; };
            if (cmbPrimaryEx.SelectedValue != null) { strPrimaryEx = cmbPrimaryEx.SelectedValue.ToString(); } else { strPrimaryEx = "null"; };
            if (cmbd_IdCurrency.SelectedValue != null) { strCurrency = cmbd_IdCurrency.SelectedValue.ToString(); } else { strCurrency = "null"; };
            if (txtd_RoundLot.Text.ToString() != "") { strLote_Padrao = txtd_RoundLot.Text.Replace(".", "").Replace(",", ".").ToString(); } else { strLote_Padrao = "null"; };
            if (txtd_RoundLotSize.Text.ToString() != "") { strLote_Negociacao = txtd_RoundLotSize.Text.Replace(".", "").Replace(",", ".").ToString(); } else { strLote_Negociacao = "null"; };

            if (lbld_IdSecurity.Text != "" && lstAvDates.SelectedValue != null && lstAvDates.SelectedValue != "")
            {
                SQLString = "UPDATE [Tb001_Securities_Fixed] " +
                            "SET[IdUnderlying] = " + strObjectTicker.ToString() +
                            "  ,[IdIssuer] = " + cmbd_IdIssuer.SelectedValue.ToString() +
                            "  ,[IdInstrument] = " + cmbd_IdInstrument.SelectedValue.ToString() +
                            "  ,[IdAssetClass] = " + cmbd_IdAssetClass.SelectedValue.ToString() +
                            "  ,[IdCurrency] = " + strCurrency.ToString() +
                            "  ,[TRDate] = '" + DataTR + "'" +
                            "  ,[QuotedAsRate] = '" + Convert.ToInt32(chkd_QuotedAsRate.Checked) + "'" +
                            "  ,[OptionType] = " + Tipo_Cal_Put.ToString() +
                            "  ,[ContractRatio] = " + strContract_Ratio.ToString() +
                            "  ,[IdStrikeCurrency] = " + strStrikeCurrency.ToString() +
                            "  ,[IdPremiumCurrency] = " + strCurrencyPrize.ToString() +
                            "  ,[LastTradeDate] ='" + dtpd_LastTradeDate.Value.ToString("yyyyMMdd") + "'" +
                            "  ,[RTUpdate] = " + Convert.ToInt32(chkd_RTUpdate.Checked) +
                            "  ,[RTUpdateSource] = " + cmbd_RTUpdateSource.SelectedValue.ToString() +
                            "  ,[HistUpdate] = " + Convert.ToInt32(chkd_HistUpdate.Checked) +
                            "  ,[HistUpdateSource] = " + cmbd_HistUpdateSource.SelectedValue.ToString() +
                            "  ,[HistUpdateFrequency] = " + strUpFreq +
                            "  ,[BloombergTicker] = '" + txtd_BloombergTicker.Text.ToString() + "'" +
                            "  ,[ReutersTicker] = '" + txtd_ReutersTicker.Text.ToString() + "'" +
                            "  ,[Imagineticker] = '" + txtd_ImagineTicker.Text.ToString() + "'" +
                            "  WHERE IdSecurity = " + Convert.ToInt32(lbld_IdSecurity.Text.ToString());

                DateTime ValidAsOfDate = Convert.ToDateTime(lstAvDates.SelectedValue);

                SQLString = SQLString + " UPDATE [Tb001_Securities_Variable] " +
                            "SET[SecName] = '" + txtd_SecName.Text.ToString() + "'" +
                            "  ,[NestTicker] = '" + txtd_NestTicker.Text.ToString() + "'" +
                            "  ,[ISIN] = '" + txtd_ISIN.Text.ToString() + "'" +
                            "  ,[ExchangeTicker] = '" + txtd_ExchangeTicker.Text.ToString() + "'" +
                            "  ,[IdPrimaryExchange] = " + strPrimaryEx.ToString() +
                            "  ,[RoundLot] =" + strLote_Padrao.ToString() +
                            "  ,[RoundLotSize] =" + strLote_Negociacao.ToString() +
                            "  ,[Expiration] = '" + dataVencimento + "'" +
                            "  ,[Strike] = " + strPreco_Exercicio.ToString() +
                            "  ,[ADRRatio] = " + strADR_Ratio +
                            "  ,[DealAnnounceDate] ='" + dtpDealAnDate.Value.ToString("yyyyMMdd") + "'" +
                            "  ,[IsInDeal] = " + Convert.ToInt32(chkd_IsInDeal.Checked) +
                            "  ,[DealDetails] = '" + txtd_DealDetails.Text + "'" +

                            "  WHERE IdSecurity = " + Convert.ToInt32(lbld_IdSecurity.Text.ToString()) +
                             "  AND ValidAsOfDate ='" + ValidAsOfDate.ToString("yyyyMMdd") + "'";
            }
            int retorno=0;
            if (SQLString != "")
            {
                retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
            }

            if (retorno != 0)
            {
            Carrega_Ativos(Convert.ToInt32(lbld_IdSecurity.Text.ToString()));
            }
            return retorno;
        }

        private void cmdHistData_Click(object sender, EventArgs e)
        {
            string SQLString2 = "INSERT INTO NESTLOG.dbo.[Tb901_Event_Log] ([Program_Id],[Process_Id],[Event_Code],[Event_DateTime],[Description],[Relevant_ID])" +
                        " Values(1,102,8,getdate(),'User changed Bloomberg Id:" + lbld_IdSecurity.Text.ToString() + " ', " + lbld_IdSecurity.Text.ToString() + " )";
            
            if (CargaDados.curConn.ExecuteNonQuery(SQLString2,1) != 0 && this.txtd_BloombergTicker.Text != "")
            {
                int retorno = Negocios.Busca_Dados_Historicos(Convert.ToInt32(lbld_IdSecurity.Text.ToString()), new DateTime(1900, 01, 01));

                if (retorno == 0)
                {
                    MessageBox.Show("Error on requesting historical data!");
                }
                else
                {
                    MessageBox.Show("Data requested. It can take up to 5 minutes to retrive!");
                }
            }
        }

        private void txtsearch_TextChanged(object sender, EventArgs e)
        {
            if (txtsearch.Text.ToString().Length > 2) { Carrega_Lista(); };
        }

        private void lstAtivos_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] parte;
            parte = lstAtivos.SelectedValue.ToString().Split('-');

            if (parte.Length > 1)
            {
                Carrega_Ativos(Convert.ToInt32(parte[0]));
                Carrega_Combo_Fontes_Tipos(Convert.ToInt32(parte[0]));
                Carrega_Grid_Prices();
            }
        }

        private void txtiPreco_Exercicio_Leave(object sender, EventArgs e)
        {
            try
            {
                txtd_Strike.Text = Convert.ToDecimal(txtd_Strike.Text).ToString("#,##0.0000;(#,##0.0000)");
            }
            catch(Exception z) 
            {
                Valida.Error_Dump_TXT(z.ToString(), this.Name.ToString());

                txtd_Strike.Text = "";
            }
        }

        private void cmdInsertNew_Click(object sender, EventArgs e)
        {
            frmInsertSecurity InsertNewSec = new frmInsertSecurity();
            InsertNewSec.ShowDialog();
            string IdSecurity_Inserted = InsertNewSec.IdSecurity_Inserted;
            string Nome_Ativo_Inserido = InsertNewSec.Ticker_Inserted;
            InsertNewSec.Dispose();
            if (IdSecurity_Inserted != null)
            {
                Carrega_Combos();
                Carrega_Ativos(Convert.ToInt32(IdSecurity_Inserted));
                txtsearch.Text = Nome_Ativo_Inserido;
                Carrega_Lista();
            }
        }

        private void cmdInsertCopy_Click(object sender, EventArgs e)
        {
            frmCopySecurity CopySec = new frmCopySecurity();
            CopySec.txtName.Text = "NEW_"+ this.txtd_SecName.Text;
            CopySec.txtNestTicker.Text = "NEW_" + this.txtd_NestTicker.Text;
            CopySec.txtOldId.Text = this.lbld_IdSecurity.Text;
            if (dtpd_Expiration.Text == "1/1/1900")
            {
                CopySec.txtExpiration.Value = dtpd_Expiration.Value; 
            }
            CopySec.ShowDialog();
            string Id_Ativo_Inserido = CopySec.Id_Ativo_Inserido;
            string Nome_Ativo_Inserido = CopySec.Nome_Ativo_Inserido;
            CopySec.Dispose();
            if (Id_Ativo_Inserido != null)
            {
                Carrega_Combos();
                Carrega_Ativos(Convert.ToInt32(Id_Ativo_Inserido));
                txtsearch.Text = Nome_Ativo_Inserido;
                Carrega_Lista();
            }
        }

        private void chkDiscontinued_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Lista();
        }

        private void chkRt_CheckedChanged(object sender, EventArgs e)
        {
            if (chkd_RTUpdate.Checked == true)
            {
                cmbd_RTUpdateSource.Enabled = true;
            }
            else
            {
                cmbd_RTUpdateSource.Enabled = false;
            }
        }

        private void chkHist_CheckedChanged(object sender, EventArgs e)
        {
            if (chkd_HistUpdate.Checked == true)
            {
                cmbd_HistUpdateSource.Enabled = true;
                cmbd_HistUpdateFrequency.Enabled = true;
            }
            else
            {
                cmbd_HistUpdateSource.Enabled = false;
                cmbd_HistUpdateFrequency.Enabled = false;
            }

        }

        private void lblNewIssuer_Click(object sender, EventArgs e)
        {
            frmIssuer InsertIssuer = new frmIssuer();
            InsertIssuer.ShowDialog(this);
            CargaDados.carregacombo(this.cmbd_IdIssuer, "Select IdIssuer, IssuerName from Tb000_Issuers", "IdIssuer", "IssuerName", 99);
        }

        private void tabCtrSecurities_SelectedIndexChanged(object sender, EventArgs e)
        {
                Carrega_Grid_Prices();
        }

        void Carrega_Combo_Fontes_Tipos(int Id_Ticker)
        {
            string PriceTableName = Valida.Retorna_Tabela_Preco_Trade(Id_Ticker,0);

            CargaDados.carregacombo(cmbPrice_Type, "SELECT -1 AS Id_Tipo_Preco, 'All' AS Preco UNION ALL SELECT A.Id_Tipo_Preco, A.Preco FROM dbo.Tb116_Tipo_Preco A INNER JOIN " + PriceTableName + " B ON A.Id_Tipo_Preco=B.Tipo_Preco WHERE Id_Ativo=" + Id_Ticker + " GROUP BY A.Id_Tipo_Preco, A.Preco ORDER BY Preco", "Id_Tipo_Preco", "Preco", 1);
            CargaDados.carregacombo(cmbSource, "SELECT -1 AS Id_Sistemas_Informacoes, 'All' AS Descricao UNION ALL SELECT Id_Sistemas_Informacoes, Descricao from dbo.Tb102_Sistemas_Informacoes A INNER JOIN " + PriceTableName + " B ON A.Id_Sistemas_Informacoes=B.Source WHERE Id_Ativo=" + Id_Ticker + " GROUP BY A.Id_Sistemas_Informacoes, A.Descricao ORDER BY Descricao", "Id_Sistemas_Informacoes", "Descricao", 1);

        }


        private void radWeek_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid_Prices();
        }

        private void radMonth_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid_Prices();
        }

        private void radYear_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid_Prices();
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid_Prices();
        }

        private void cmbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid_Prices();
        }

        private void cmbPrice_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid_Prices();
        }

        private void cmdInsertPrice_Click(object sender, EventArgs e)
        {
            string[] parte;
            parte = lstAtivos.SelectedValue.ToString().Split('-');

            frmInsertPrice insPrice = new frmInsertPrice();
            insPrice.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
            insPrice.Left = this.Left + 10;

            insPrice.cmbTicker.SelectedValue = parte[0];

            insPrice.ShowDialog();
        }

        private void label32_Click(object sender, EventArgs e)
        {
            frmAddSecurityDate AddSecurityDate = new frmAddSecurityDate();
            AddSecurityDate.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
            AddSecurityDate.Left = this.Left + 250;
            AddSecurityDate.ShowDialog();

            if (AddSecurityDate.Tag != null)
            {
                DateTime DateToInsert = ((DateTime)AddSecurityDate.Tag).Date;

                if (DateToInsert != new DateTime(1900, 01, 01))
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string CopyFromDate = curConn.Execute_Query_String("SELECT MAX(ValidAsOfDate) FROM NESTDB.dbo.Tb001_Securities_Variable WHERE IdSecurity=" + lbld_IdSecurity.Text + " AND ValidAsOfDate<='" + DateToInsert.ToString("yyyy-MM-dd") + "'");

                        string SQLString = "INSERT INTO NESTDB.dbo.Tb001_Securities_Variable SELECT [IdSecurity],'" + DateToInsert.ToString("yyyy-MM-dd") + "',[SecName],[NestTicker],[ISIN],[ExchangeTicker],[IdPrimaryExchange],[RoundLot],[RoundLotSize],[Expiration],[Strike],[ADRRatio],[DealAnnounceDate],[IsInDeal],[DealDetails] FROM NESTDB.dbo.Tb001_Securities_Variable WHERE IdSecurity=" + lbld_IdSecurity.Text + " AND ValidAsOfDate='" + DateTime.Parse(CopyFromDate).ToString("yyyy-MM-dd") + "'";

                        curConn.ExecuteNonQuery(SQLString);
                    }

                    ReloadlstAvDates();
                }
            }
        }


     }
}