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

    public partial class frmEdit : LBForm
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        DataTable tablep = new DataTable();
        

        public frmEdit()
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
            CargaDados.carregacombo(this.cmbIssuer, "Select IdIssuer,IssuerName from Tb000_Issuers", "IdIssuer", "IssuerName", 0);
            CargaDados.carregacombo(this.cmbTickerType, "Select Id_Price_Table,Price_Table_Name from Tb024_Price_Table", "Id_Price_Table", "Price_Table_Name", 0);
            CargaDados.carregacombo(this.cmbCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 0);
            CargaDados.carregacombo(this.cmbCurrenPrize, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 0);
            CargaDados.carregacombo(this.cmbStrikeCurrency, "Select Id_Moeda,Currency from dbo.Vw_Moedas", "Id_Moeda", "Currency", 0);
            CargaDados.carregacombo(this.cmbPrimaryEx, "Select Id_Mercado, Cod_Mercado from Tb107_Mercados", "Id_Mercado", "Cod_Mercado", 0);
            CargaDados.carregacombo(this.cmbObjectTicker, "Select IdSecurity,NestTicker from dbo.Tb001_Securities where IdPriceTable <> 7 ORDER BY NestTicker", "IdSecurity", "NestTicker", 0);
            CargaDados.carregacombo(this.cmbInstrument, "Select Id_Instrumento,Instrumento from Tb029_Instrumentos", "Id_Instrumento", "Instrumento", 0);
            CargaDados.carregacombo(this.cmbAssetClass, "Select Id_Classe_Ativo,Classe_Ativo  from Tb028_Classe_Ativo ", "Id_Classe_Ativo", "Classe_Ativo", 0);
            CargaDados.carregacombo(this.cmbUpFreq, "Select Id_Frequencia,Frequencia from Tb105_Frequencia_Atualizacao  ", "Id_Frequencia", "Frequencia", 0);
            CargaDados.carregacombo(this.cmbFonteUp, "Select Id_Sistemas_Informacoes,Descricao  from Tb102_Sistemas_Informacoes WHERE (Hist = 1 or RT is null)", "Id_Sistemas_Informacoes", "Descricao", 0);
            CargaDados.carregacombo(this.cmbSourceRT, "Select Id_Sistemas_Informacoes,Descricao  from Tb102_Sistemas_Informacoes WHERE(RT = 1 or RT is null)", "Id_Sistemas_Informacoes", "Descricao", 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Carrega_Lista();
        }

        public void Carrega_Lista()
        {

            string strWhere = "";
            string[] strFields = new string[1];

            string SQLString = "Select convert(varchar(10), A.IdSecurity,102) + '-' + convert(char(5)," +
                                " A.IdPriceTable,102) as IdSecurity, " +
                                " A.NestTicker " +
                                " FROM dbo.Tb001_Securities A LEFT JOIN dbo.Tb119_Convert_Simbolos B ON A.IdSecurity=B.Id_Ativo" +
                                " WHERE A.IdSecurity>0 ";

            if (chkDiscontinued.Checked != true)
            {
                SQLString = SQLString + " AND ((A.Expiration >= convert(varchar,GETDATE(),112)) OR (A.Expiration IS NULL))";
            }

            if (txtsearch.Text.Trim() != "")
            {
                strFields = txtsearch.Text.ToString().Split(' ');
                for (int f = 0; f < strFields.Length; f++)
                {
                    if (strFields[f] != "") { strWhere = strWhere + " AND (A.NestTicker like '%" + strFields[f] + "%' OR A.Name Like '%" + strFields[f] + "%' OR A.ExchangeTicker Like '%" + strFields[f] + "%' OR B.Cod_Reuters Like '%" + strFields[f] + "%' OR B.Cod_BBL Like '%" + strFields[f] + "%' OR A.IdSecurity like '%" + strFields[f].ToString() + "%')"; }
                }
            }

            SQLString = SQLString + strWhere + " order by NestTicker";

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
            string SQLString;
            SQLString = "Select A.*, B.Cod_Reuters,Cod_BBL,Cod_Imagine from Tb001_Securities A " +
            " left join Tb119_Convert_Simbolos B on A.IdSecurity = B.Id_Ativo" +
            " Where A.IdSecurity = " + Id_Ativo;

            DataTable curTable = CargaDados.curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                lblId_Ticker.Text = Id_Ativo.ToString();

                if (curRow["IdIssuer"].ToString() != "")
                { cmbIssuer.SelectedValue = Convert.ToInt32(curRow["IdIssuer"]); }
                else 
                    { cmbIssuer.SelectedIndex = -1; }

                if (curRow["IdPriceTable"].ToString() != "")
                { cmbTickerType.SelectedValue = Convert.ToInt32(curRow["IdPriceTable"]); }
                else
                    { cmbTickerType.SelectedIndex = -1; }

                if (curRow["IdCurrency"].ToString() != "")
                { cmbCurrency.SelectedValue = Convert.ToInt32(curRow["IdCurrency"]); }
                else
                    { cmbCurrency.SelectedIndex = -1; }

                if (curRow["IdPremiumCurrency"].ToString() != "")
                { cmbCurrenPrize.SelectedValue = Convert.ToInt32(curRow["IdPremiumCurrency"]); }
                else
                    { cmbCurrenPrize.SelectedIndex = -1; }

                if (curRow["IdStrikeCurrency"].ToString() != "")
                { cmbStrikeCurrency.SelectedValue = Convert.ToInt32(curRow["IdStrikeCurrency"]); }
                else
                    { cmbStrikeCurrency.SelectedIndex = -1; }

                if (curRow["IdPrimaryExchange"].ToString() != "")
                { cmbPrimaryEx.SelectedValue = Convert.ToInt32(curRow["IdPrimaryExchange"]); }
                else
                    { cmbPrimaryEx.SelectedIndex = -1; }

                if (curRow["IdUnderlying"].ToString() != "")
                { cmbObjectTicker.SelectedValue = Convert.ToInt32(curRow["IdUnderlying"]); }
                else
                    { cmbObjectTicker.SelectedIndex = -1; }

                if (curRow["IdInstrument"].ToString() != "")
                {
                    cmbInstrument.SelectedValue = Convert.ToInt32(curRow["IdInstrument"]); 
                }
                else
                    { cmbInstrument.SelectedIndex = -1; }

                if (curRow["IdAssetClass"].ToString() != "")
                { cmbAssetClass.SelectedValue = Convert.ToInt32(curRow["IdAssetClass"]); }
                else
                    { cmbAssetClass.SelectedIndex = -1; }

                if (curRow["HistUpdateFrequency"].ToString() != "")
                {
                    cmbUpFreq.SelectedValue = Convert.ToInt32(curRow["HistUpdateFrequency"]); 
                }
                else
                { 
                    cmbUpFreq.SelectedIndex = -1; 
                }

                if (curRow["HistUpdateSource"].ToString() != "")
                {
                    cmbFonteUp.SelectedValue = Convert.ToInt32(curRow["HistUpdateSource"]); 
                }
                else
                { 
                    cmbFonteUp.SelectedIndex = -1; 
                }

                if (curRow["HistUpdate"].ToString() == "1")
                {
                    chkHist.Checked = true;
                }
                else
                {
                    chkHist.Checked = false;
                    cmbUpFreq.Enabled = false;
                    cmbFonteUp.Enabled = false;
                }

                if (curRow["RTUpdateSource"].ToString() != "")
                { cmbSourceRT.SelectedValue = Convert.ToInt32(curRow["RTUpdateSource"]); }
                else
                { cmbSourceRT.SelectedIndex = -1; }

                if (curRow["RTUpdate"].ToString() == "1")
                {
                    chkRt.Checked = true;
                }
                else
                {
                    chkRt.Checked = false;
                    cmbSourceRT.Enabled = false;
                }
                
                if (curRow["SecName"].ToString() != "")
                {
                    txtiNome.Text = curRow["SecName"].ToString();
                }
                else
                {
                    txtiNome.Text = "";
                }

                if (curRow["NestTicker"].ToString() != "")
                {
                    txtiSimbolo.Text = curRow["NestTicker"].ToString();
                }
                else
                {
                    txtiSimbolo.Text = "";
                }

                if (curRow["ExchangeTicker"].ToString() != "")
                {
                    txtExchangeTicker.Text = curRow["ExchangeTicker"].ToString();
                }
                else
                {
                    txtExchangeTicker.Text = "";
                }

                if (curRow["Strike"].ToString() != "")
                {
                    txtiPreco_Exercicio.Text = Convert.ToDecimal(curRow["Strike"].ToString()).ToString("#,##0.0000;(#,##0.0000)");
                }
                else
                {
                    txtiPreco_Exercicio.Text = "";
                }

                if (curRow["ContractRatio"].ToString() != "")
                {
                    txtiContract_Ratio.Text = Convert.ToDecimal(curRow["ContractRatio"].ToString()).ToString("#,##0.0000;(#,##0.0000)");
                }
                else
                {
                    txtiContract_Ratio.Text = "";
                }

                if (curRow["COD_BBL"].ToString() != "")
                {
                   txtBlbTicker.Text = curRow["COD_BBL"].ToString();
                }
                else
                {
                    txtBlbTicker.Text = "";
                }

                if (curRow["Cod_Reuters"].ToString() != "")
                {
                   txtReutersTicker.Text = curRow["Cod_Reuters"].ToString();
                }
                else
                {
                    txtReutersTicker.Text = "";
                }
                if (curRow["Cod_Imagine"].ToString() != "")
                {
                    txtImagineTicker.Text = curRow["Cod_Imagine"].ToString();
                }
                else
                {
                    txtImagineTicker.Text = "";
                }

                if (curRow["ISIN"].ToString() != "")
                {
                    txtiISIN.Text = curRow["ISIN"].ToString();
                }
                else
                {
                    txtiISIN.Text = "";
                }

                if (curRow["OptionType"].ToString() != "")
                {
                    if (curRow["OptionType"].ToString() == "1")
                    {
                        rdCall.Checked = true;
                        rdPut.Checked = false;
                    }
                    else
                    {
                        rdCall.Checked = false;
                        rdPut.Checked = true;
                    }
                }
                else
                {
                    rdCall.Checked = false;
                    rdPut.Checked = false;
                }

                if (curRow["RoundLot"].ToString() != "")
                {
                    txtiLote_Padrao.Text = Convert.ToDecimal(curRow["RoundLot"].ToString()).ToString("#,##0.0000########;(#,##0.0000########)");
                }
                else
                {
                    txtiLote_Padrao.Text = "";
                }

                if (curRow["QuotedAsRate"].ToString() == "1")
                {
                    chkQuote.Checked = true;
                }
                else
                {
                    chkQuote.Checked = false;
                }

                if (curRow["RoundLotSize"].ToString() != "")
                {
                    txtiLote_Negociacao.Text = Convert.ToDecimal(curRow["RoundLotSize"].ToString()).ToString("#,##0.0000;(#,##0.0000)");
                }
                else
                {
                    txtiLote_Negociacao.Text = "";
                }
                if (curRow["TRDate"].ToString() != "")
                {
                    dtpTrDate.Value = Convert.ToDateTime(curRow["TRDate"].ToString());
                }
                else
                {
                    dtpTrDate.Value = Convert.ToDateTime("1/1/1900");
                }

                if (curRow["Expiration"].ToString() != "")
                {
                    txtExpiration.Value = Convert.ToDateTime(curRow["Expiration"].ToString());
                }
                else
                {
                    txtExpiration.Value = Convert.ToDateTime("1/1/1900");
                }

                if (curRow["LastTradeDate"].ToString() != "")
                {
                    dtpLastTradeDate.Value = Convert.ToDateTime(curRow["LastTradeDate"].ToString());
                }
                else
                {
                    dtpLastTradeDate.Value = Convert.ToDateTime("1/1/1900");
                }

                if (curRow["DealAnnounceDate"].ToString() != "")
                {
                  dtpDealAnDate.Value = Convert.ToDateTime(curRow["DealAnnounceDate"].ToString());
                }
                else
                {
                    dtpDealAnDate.Value = Convert.ToDateTime("1/1/1900");
                }
                
                if (curRow["ADRRatio"].ToString() != "")
                {
                    txti_ADR_Ratio.Text = Convert.ToDecimal(curRow["ADRRatio"].ToString()).ToString("#,##0.0000;(#,##0.0000)");
                }
                else
                {
                    txti_ADR_Ratio.Text = "0,0000";
                }

                switch (Convert.ToInt32(curRow["IdInstrument"]))
                {
                    case 4:
                    case 12:
                    case 16:
                        groupLot.Enabled = true;
                        groupOPFUT.Enabled = true;
                        groupOPT.Enabled = false;
                        break;
                    case 15:
                        groupLot.Enabled = true;
                        groupOPFUT.Enabled = true;
                        groupOPT.Enabled = false;
                        break;
                    case 5:
                    case 13:
                        groupLot.Enabled = true;
                        groupOPFUT.Enabled = true;
                        groupOPT.Enabled = false;
                        break;
                    case 9:
                        groupLot.Enabled = false;
                        groupOPFUT.Enabled = false;
                        groupOPT.Enabled = false;
                        break;
                    case 3:
                        groupLot.Enabled = true;
                        groupOPFUT.Enabled = true;
                        groupOPT.Enabled = true;
                        break;
                    case 6:
                    case 11:
                        groupLot.Enabled = false;
                        groupOPFUT.Enabled = false;
                        groupOPT.Enabled = false;
                        break;
                    default:
                        groupLot.Enabled = true;
                        groupOPFUT.Enabled = false;
                        groupOPT.Enabled = false;
                        break;
                }
                if (Convert.ToDateTime(txtExpiration.Value) < DateTime.Now.Subtract(new TimeSpan(20, 0, 0, 0)) && NestDLL.NUserControl.Instance.User_Id != 13 && txtExpiration.Value != Convert.ToDateTime("1/1/1900"))
                {
                    txtExpiration.Enabled = false;
                }
                else 
                {
                    txtExpiration.Enabled = true;
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
                string SQLString = String.Format("DELETE FROM NESTRT.dbo.Tb000_Posicao_Atual WHERE [Id Ticker]={0}", lblId_Ticker.Text);
                retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1); 
            }
        }

        private int Atualiza_Dados()
        {
            string SQLString = "";


            // Security Description
            string strObjectTicker;
            if (cmbObjectTicker.SelectedValue != null) 
            { 
                strObjectTicker = cmbObjectTicker.SelectedValue.ToString(); 
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

            if (groupOPFUT.Enabled == true) 
            {
                data_ex = Convert.ToDateTime(txtExpiration.Value.ToString());
                dataVencimento = "'" + data_ex.ToString("yyyyMMdd") + "'";
            } 
            else 
            { 
                dataVencimento = "null";
            };


            

            data_ex = Convert.ToDateTime("1/1/1900");
            data_ex = Convert.ToDateTime(dtpTrDate.Value.ToString());
            string DataTR = "'" + data_ex.ToString("yyyyMMdd") + "'";



            string strADR_Ratio = "null";
            if (txti_ADR_Ratio.Text != "") { strADR_Ratio = txti_ADR_Ratio.Text.Replace(".", "").Replace(",", ".").ToString(); } else { strADR_Ratio = "null"; };
            
            // Options
            string Tipo_Cal_Put;
            string strPreco_Exercicio;
            string strCurrencyPrize;
            string strStrikeCurrency;
            string strContract_Ratio;

            if (groupOPT.Enabled == true)
            {
                Tipo_Cal_Put = "null";
                if (rdCall.Checked == true) { Tipo_Cal_Put = "1"; }
                if (rdPut.Checked == true) { Tipo_Cal_Put = "0"; }

                if (txtiPreco_Exercicio.Text != "") { strPreco_Exercicio = txtiPreco_Exercicio.Text.Replace(".", "").Replace(",", ".").ToString(); } else { strPreco_Exercicio = "null"; };
                if (txtiContract_Ratio.Text != "") { strContract_Ratio = txtiContract_Ratio.Text.Replace(".", "").Replace(",", ".").ToString(); } else { strContract_Ratio = "null"; };

                if (cmbCurrenPrize.SelectedValue != null) { strCurrencyPrize = cmbCurrenPrize.SelectedValue.ToString(); } else { strCurrencyPrize = "null"; };

                if (cmbStrikeCurrency.SelectedValue != null) { strStrikeCurrency = cmbStrikeCurrency.SelectedValue.ToString(); } else { strStrikeCurrency = "null"; };
            }
            else
            {
                Tipo_Cal_Put = "null";
                strPreco_Exercicio = "null";
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

            if (cmbUpFreq.SelectedValue != null) { strUpFreq = cmbUpFreq.SelectedValue.ToString(); } else { strUpFreq = "0"; };
            if (cmbPrimaryEx.SelectedValue != null) { strPrimaryEx = cmbPrimaryEx.SelectedValue.ToString(); } else { strPrimaryEx = "null"; };
            if (cmbCurrency.SelectedValue != null) { strCurrency = cmbCurrency.SelectedValue.ToString(); } else { strCurrency = "null"; };
            if (txtiLote_Padrao.Text.ToString() != "") { strLote_Padrao = txtiLote_Padrao.Text.Replace(".", "").Replace(",", ".").ToString(); } else { strLote_Padrao = "null"; };
            if (txtiLote_Negociacao.Text.ToString() != "") { strLote_Negociacao = txtiLote_Negociacao.Text.Replace(".", "").Replace(",", ".").ToString(); } else { strLote_Negociacao = "null"; };

            SQLString = "UPDATE [ZTb001_Ativos] " +
                           "SET [Nome] = '" + txtiNome.Text.ToString() + "'" +
                            "  ,[Id_Instituicao] = " + cmbIssuer.SelectedValue.ToString() +
                            "  ,[Simbolo] = '" + txtiSimbolo.Text.ToString() + "'" +
                            "  ,[Exchange_Ticker] = '" + txtExchangeTicker.Text.ToString() + "'" +
                            "  ,[Lote_Padrao] =" + strLote_Padrao.ToString() +
                            "  ,[Lote_Negociacao] =" + strLote_Negociacao.ToString() +
                            "  ,[Moeda] = " + strCurrency.ToString() +
                            "  ,[Id_Instrumento] = " + cmbInstrument.SelectedValue.ToString() +
                            "  ,[Id_Classe_Ativo] = " + cmbAssetClass.SelectedValue.ToString() +
                            "  ,[Hist_Update] = " + Convert.ToInt32(chkHist.Checked) +
                            "  ,[Frequencia_Atualizacao] = " + strUpFreq +
                            "  ,[Fonte_Atualizacao] = " + cmbFonteUp.SelectedValue.ToString() +
                            "  ,[RT_Update] = " + Convert.ToInt32(chkRt.Checked)  +
                            "  ,[RT_Update_Source] = " + cmbSourceRT.SelectedValue.ToString() +
                            "  ,[Ativo_Objeto] = " + strObjectTicker.ToString() +
                            "  ,[Vencimento] = " + dataVencimento +
                            "  ,[TR_Date] = " + DataTR +
                            "  ,[Moeda_Premio] = " + strCurrencyPrize.ToString() +
                            "  ,[Preco_Exercicio] = " + strPreco_Exercicio.ToString() +
                            "  ,[Contract_Ratio] = " + strContract_Ratio.ToString() +
                            "  ,[Moeda_Strike] = " + strStrikeCurrency.ToString() +
                            "  ,[Tipo_C_V] = " + Tipo_Cal_Put.ToString() +
                            "  ,[Bolsa_Primaria] = " + strPrimaryEx.ToString() +
                            "  ,[ISIN] = '" + txtiISIN.Text.ToString() + "'" +
                            "  ,[Quoted_As_Rate] = '" + Convert.ToInt32(chkQuote.Checked) + "'" +
                            "  ,[ADR_Ratio] = " + strADR_Ratio +
                            "  ,[DealAnnounceDate] ='" + dtpDealAnDate.Value.ToString("yyyyMMdd") + "'" +
                            "  ,[LastTradeDate] ='" + dtpLastTradeDate.Value.ToString("yyyyMMdd") + "'" +
                            "  WHERE Id_Ativo = " + Convert.ToInt32(lblId_Ticker.Text.ToString());
        
            int retorno;
            if (SQLString != "")
            {
                retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
                if (retorno != 0 & retorno != 99)
                {
                    string Cod_Imagine;

                    Cod_Imagine = txtReutersTicker.Text.ToString() + "-" + Valida.Right(txtExpiration.Value.Year.ToString(), 2);

                    SQLString = "INSERT INTO Tb119_Convert_Simbolos(Id_Ativo, Cod_Reuters, Cod_BBL, Cod_Imagine) " +
                                 "VALUES (" + lblId_Ticker.Text.ToString() + ", '" + txtReutersTicker.Text.ToString() + "'," +
                                 " '" + txtBlbTicker.Text.ToString() + "'," +
                                 " '" + txtImagineTicker.Text.ToString() + "')";


                    retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
                }
                else
                {
                    MessageBox.Show("Error on Update");
                }
            }
            else
                retorno = 0;

            Carrega_Ativos(Convert.ToInt32(lblId_Ticker.Text.ToString()));
            
            return retorno;
        }

        private void cmdHistData_Click(object sender, EventArgs e)
        {
            string SQLString2 = "INSERT INTO NESTLOG.dbo.[Tb901_Event_Log] ([Program_Id],[Process_Id],[Event_Code],[Event_DateTime],[Description],[Relevant_ID])" +
                        " Values(1,102,8,getdate(),'User changed Bloomberg Id:" + lblId_Ticker.Text.ToString() + " ', " + lblId_Ticker.Text.ToString() + " )";
            
            if (CargaDados.curConn.ExecuteNonQuery(SQLString2,1) != 0 && this.txtBlbTicker.Text != "")
            {
                int retorno = Negocios.Busca_Dados_Historicos(Convert.ToInt32(lblId_Ticker.Text.ToString()), new DateTime(1900, 01, 01));

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
                txtiPreco_Exercicio.Text = Convert.ToDecimal(txtiPreco_Exercicio.Text).ToString("#,##0.0000;(#,##0.0000)");
            }
            catch(Exception z) 
            {
                Valida.Error_Dump_TXT(z.ToString(), this.Name.ToString());

                txtiPreco_Exercicio.Text = "";
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
            CopySec.txtName.Text = "NEW_"+ this.txtiNome.Text;
            CopySec.txtNestTicker.Text = "NEW_" + this.txtiSimbolo.Text;
            CopySec.txtOldId.Text = this.lblId_Ticker.Text;
            if (txtExpiration.Text == "1/1/1900")
            {
                CopySec.txtExpiration.Value = txtExpiration.Value; 
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
            if (chkRt.Checked == true)
            {
                cmbSourceRT.Enabled = true;
            }
            else
            {
                cmbSourceRT.Enabled = false;
            }
        }

        private void chkHist_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHist.Checked == true)
            {
                cmbFonteUp.Enabled = true;
                cmbUpFreq.Enabled = true;
            }
            else
            {
                cmbFonteUp.Enabled = false;
                cmbUpFreq.Enabled = false;
            }

        }

        private void lblNewIssuer_Click(object sender, EventArgs e)
        {
            frmIssuer InsertIssuer = new frmIssuer();
            InsertIssuer.ShowDialog(this);
            CargaDados.carregacombo(this.cmbIssuer, "Select IdIssuer, IssuerNamefrom Tb000_Issuers", "IdIssuer", "IssuerName", 99);
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
     }
}