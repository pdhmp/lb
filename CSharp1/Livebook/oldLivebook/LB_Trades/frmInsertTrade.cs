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
    public partial class frmInsertTrade : Form
    {
        public double Id_Ordem;
        public int Id_User;
        public string Id_Retorno;
        public Boolean Perm_Usuario;
        public double Id_Corretora;
        public decimal Price_Order;
        LB_Utils curUtils = new LB_Utils();
        old_Conn curConn = new old_Conn();
         
        public frmInsertTrade()
        {
            InitializeComponent();

        }

       private void cmdCancel_Click(object sender, EventArgs e)
        {
            Id_Retorno = "0";
            DialogResult.Ignore.ToString(); 
           this.Close();
            //this.Close();
        }

        private void frmInsertTrade_Load(object sender, EventArgs e)
        {
            Carrega_Trade(Id_Ordem);
           // Perm_Usuario =  curUtils.Verifica_Acesso(Id_User, 2);

        }

        public void Carrega_Trade(double Id_Ordem)
        {
            string SQLString;
            Boolean ordemTrade = false;
            double Lote_Padrao;

            SQLString = "SELECT A.Quantidade AS DoneQuant, A.Valor_Financeiro AS DoneFin, B.*, C.NestTicker, D.Port_Name " +
                "FROM dbo.VW_Done_Trades (nolock) A  " +
                "INNER JOIN dbo.Tb012_Ordens (nolock) B  " +
                "ON A.Id_Ordem=B.Id_Ordem  " +
                "INNER JOIN dbo.Tb001_Securities (nolock) C  " +
                "ON B.Id_Ativo=C.IdSecurity  " +
                "INNER JOIN dbo.VW_PortAccounts (nolock) D  " +
                "ON B.Id_Account=D.Id_Account  " +
                "WHERE A.Id_Ordem=" + Id_Ordem;
            
            DataTable curTable = curConn.Return_DataTable(SQLString);

            foreach (DataRow curRow in curTable.Rows)
            {
                SQLString = "Select RoundLot from Tb001_Securities (nolock) a inner join Tb012_Ordens (nolock) b" +
                                   " on a.IdSecurity = b.Id_Ativo Where Id_Ordem =" + Id_Ordem;
                 Lote_Padrao = Convert.ToDouble(curConn.Execute_Query_String(SQLString));
                 
                 // se tiver trandes para ordens roda esta rotina
                 this.lblCart.Text = curRow["Port_Name"].ToString();
                 this.lblAtivo.Text = curRow["NestTicker"].ToString();   
                 //executados

                 decimal AvgPrice = (Convert.ToDecimal(curRow["DoneFin"]) / Convert.ToDecimal(curRow["DoneQuant"]));
                 decimal LeavesQTD = (Convert.ToDecimal(curRow["Quantidade"]) - Convert.ToDecimal(curRow["DoneQuant"]));

                decimal PrecoExec = Decimal.Round(Convert.ToDecimal(AvgPrice.ToString()), 2);
                this.txtPrecoExec.Text = PrecoExec.ToString("#,##0.00");

                decimal QtdExec = Decimal.Round(Convert.ToDecimal(curRow["DoneQuant"].ToString()), 2);
                this.txtQtdExec.Text = QtdExec.ToString("#,##0");

                decimal VfExec = Decimal.Round(Convert.ToDecimal(curRow["DoneFin"].ToString()), 2);
                this.txtVfExec.Text = VfExec.ToString("#,##0.00");
                  
                  
                //ordem

                decimal PrecoOrdem = Decimal.Round(Convert.ToDecimal(curRow["preco"].ToString()), 2);
                Price_Order = PrecoOrdem;
                this.txtPrecoOrdem.Text = PrecoOrdem.ToString("#,##0.00");

                decimal QTDOrdem = Decimal.Round(Convert.ToDecimal(curRow["Quantidade"].ToString()), 2);
                this.txtQtdordem.Text = QTDOrdem.ToString("#,##0");

                //trade

                 decimal PrecoRest = Decimal.Round(Convert.ToDecimal(AvgPrice.ToString()), 2);
                 //txtPreco.Text = PrecoRest.ToString("#,##0.00");

                 decimal QTDRest = Decimal.Round(Convert.ToDecimal(LeavesQTD.ToString()), 2);
                 this.txtQtd.Text = QTDRest.ToString("#,##0");

                 Decimal Valor_Fianc = decimal.Round(PrecoRest * QTDRest, 2);

                 txtValorFinanc.Text = Valor_Fianc.ToString("#,##0.00");

                 double preco = Convert.ToDouble(curRow["preco"]);
                 double QTD = Convert.ToDouble(curRow["Quantidade"]);
                 double VF = preco * (QTD / Lote_Padrao);
                 this.txtVFOrdem.Text = VF.ToString("#,##0.00");

                 //pendente
                 decimal Qtdpend = Decimal.Round(Convert.ToDecimal(LeavesQTD.ToString()), 2);
                 this.txtQtdpend.Text = Qtdpend.ToString("#,##0");

                 double Vfpend = VF - Convert.ToDouble(curRow["DoneFin"]);
                 this.txtVFpend.Text = Vfpend.ToString("#,##0.00");

                 decimal precopend = (Convert.ToDecimal(VF) - VfExec) / Qtdpend;

                 this.txtPrecoPend.Text = precopend.ToString("#,##0.00");
                 txtPreco.Text = precopend.ToString("#,##0.00");
                 ordemTrade = true;
                 Id_Corretora = Convert.ToDouble(curRow["Id_Corretora"].ToString());
             };

             if (ordemTrade == false)
             {
                 //senão roda esta
                 SQLString = "Select Id_Ordem,Tb012_Ordens.Id_Account,Account_Ticker, Quantidade,Preco,Valor_Financeiro, NestTicker, Id_Corretora" +
                             " from Tb012_Ordens inner join Tb001_Securities on" +
                             " Tb001_Securities.IdSecurity = Tb012_Ordens.Id_Ativo" +
                             " inner join dbo.Tb007_Accounts on " +
                             " dbo.Tb007_Accounts.Id_Account = Tb012_Ordens.Id_Account" + 
                             " Where Id_Ordem=" + Id_Ordem ;
                 
                 curTable = curConn.Return_DataTable(SQLString);

                 foreach (DataRow curRow in curTable.Rows)
                 {
                     SQLString = "Select RoundLot from Tb001_Securities a inner join Tb012_Ordens b" +
                                        " on a.IdSecurity = b.Id_Ativo Where Id_Ordem =" + Id_Ordem;
                     Lote_Padrao = Convert.ToDouble(curConn.Execute_Query_String(SQLString));

                     this.lblCart.Text = curRow["Account_Ticker"].ToString();
                     this.lblAtivo.Text = curRow["NestTicker"].ToString();
                     //pendente
                     decimal Qtdpenddbl = Convert.ToDecimal(curRow["Quantidade"].ToString());

                     this.txtQtdpend.Text = Qtdpenddbl.ToString("##,###");
                     //this.txtQtdpend.Text = dados["Quantidade"].ToString();
                     //ordem


                     decimal precodbl = Convert.ToDecimal(curRow["preco"].ToString());
                     this.txtPrecoOrdem.Text = precodbl.ToString("##,##0.00########");
                     Price_Order = precodbl;
                     decimal Qtdordemdbl = Convert.ToDecimal(curRow["Quantidade"].ToString());
                     this.txtQtdordem.Text = Qtdordemdbl.ToString("##,###");
                     double preco = Convert.ToDouble(curRow["preco"]);
                     double QTD = Convert.ToDouble(curRow["Quantidade"]);
                     double VF = preco * (QTD / Lote_Padrao);
                     this.txtVFOrdem.Text = VF.ToString("##,##0.00########");
                     //trade

                     this.txtVFpend.Text = VF.ToString("##,##0.00########");
                     
                     double Precopenddbl = Convert.ToDouble(txtVFpend.Text) / Convert.ToDouble(txtQtdpend.Text);
                     this.txtPrecoPend.Text = Precopenddbl.ToString("##,##0.00########");

                     double Precodbl = Convert.ToDouble(curRow["preco"].ToString());
                     txtPreco.Text = Precodbl.ToString("##,##0.00########");

                     double txtqtddbl = Convert.ToDouble(curRow["Quantidade"].ToString());
                     this.txtQtd.Text = txtqtddbl.ToString("##,###");
                     Id_Corretora = Convert.ToDouble(curRow["Id_Corretora"].ToString());
                 };
             }
             SQLString = "Select DefaultRebate from Brokers Where IdBroker = " + this.Id_Corretora;
             this.txtRebate.Text = curConn.Execute_Query_String(SQLString);
        
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            try
            {
                double temp_total =Convert.ToDouble(this.txtQtd.Text.Replace("-",""))  ;
                double tem_pend = Convert.ToDouble(this.txtQtdpend.Text.Replace("-", ""));
                int resposta;
                if (tem_pend < temp_total)
                {//se for mair q saldo, entra aqui e checa se quer altera ordem
                    MessageBox.Show("Amount Greater than Amount Outstanding!");
                }
                else
                {//se for menor  q saldo entra aqui e insere
                    int resultado;
                    string SQLString;
                    decimal Valor_Financeiro = Convert.ToDecimal(txtValorFinanc.Text);
                    decimal Qtd = Convert.ToDecimal(txtQtd.Text);
                    decimal Preco = Convert.ToDecimal(txtPreco.Text);
                    decimal TaxaBovespa;
                    decimal TaxaCBLC;
                    decimal TaxaCorretagem;
                    decimal Percentual_Rebate;
                    TaxaCorretagem = 1;
                    string data_Insert;

                    if (this.txtRebate.Text != "")
                    {
                        Percentual_Rebate = Convert.ToDecimal(this.txtRebate.Text);
                    }
                    else
                    {
                        Percentual_Rebate = 99;
                    }
                    
                    //TaxaCorretagem = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal("0,005") + Convert.ToDecimal("25,21")) * (1 - Percentual_Rebate), 2);

                    SQLString = "Select a.IdSecurity, a.IdPriceTable,a.RoundLotSize, Data_Abert_ordem from Tb001_Securities a(NOLOCK) " +
                                " inner join Tb012_Ordens b on b.Id_Ativo = a.IdSecurity" +
                                " Where Id_Ordem =" + Id_Ordem;

                    int IdSecurity;
                    int IdPriceTable = 0;
                    Decimal RoundLotSize = 1;


                    DataTable curTable = curConn.Return_DataTable(SQLString);
                    foreach (DataRow curRow in curTable.Rows)
                    {
                        IdSecurity = Convert.ToInt32(curRow[0]);
                        IdPriceTable = Convert.ToInt32(curRow[1]);
                        RoundLotSize = Convert.ToDecimal(curRow[2]);
                    }

                    switch (IdPriceTable)
                    {// Case que verifica o tipo do ativo e calcula as taxas 
                        case 1:
                            TaxaBovespa = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal(0.00019), 2));
                            TaxaCBLC = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal(0.00006), 2));
                            TaxaCorretagem = Decimal.Round(Convert.ToDecimal(Math.Abs((Convert.ToDouble(Valor_Financeiro) * 0.005 + 25.21) * (1 - Convert.ToDouble(Percentual_Rebate)))), 2);
                            break;
                    
                        case 7:
                            TaxaBovespa = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal(0.00019), 2));
                            TaxaCBLC = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal(0.00006), 2));
                            TaxaCorretagem = Decimal.Round(Convert.ToDecimal(Math.Abs((Convert.ToDouble(Valor_Financeiro) * 0.005 + 25.21) * (1 - Convert.ToDouble(Percentual_Rebate)))), 2);
                            break;
                    
                        default:
                            TaxaBovespa = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal(0.00019), 2));
                            TaxaCBLC = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal(0.00006), 2));
                            TaxaCorretagem = Decimal.Round(Convert.ToDecimal(Math.Abs((Convert.ToDouble(Valor_Financeiro) * 0.005 + 25.21) * (1 - Convert.ToDouble(Percentual_Rebate)))), 2);
                            break;
                        case 2:
                            TaxaBovespa = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal(0.00019), 2));
                            TaxaCBLC = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal(0.00006), 2));
                            TaxaCorretagem = Convert.ToDecimal(0.10) * Percentual_Rebate;
                            break;
                    }

                    string divisao = Convert.ToString(Convert.ToDecimal(this.txtQtd.Text) / RoundLotSize);

                   divisao = divisao.Replace(",00", "");
                    //int teste;
                    resposta = 6;
                    if (Qtd > 0)
                    {
                        if (Preco > Price_Order)
                        {
                            resposta = Convert.ToInt32(MessageBox.Show("Execution price is" + " greater " + " than limit Price!Do you wish continue?", "Trades", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                        }
                    }
                    else
                    {
                        if (Preco < Price_Order)
                        {
                            resposta = Convert.ToInt32(MessageBox.Show("Execution price is" + " greater " + " than limit Price!Do you wish continue?", "Trades", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                        }
                    }
                    if (Preco != 0)
                    {
                        if (Math.Abs(Preco / Price_Order - 1) > Convert.ToDecimal(0.1))
                        {
                            resposta = Convert.ToInt32(MessageBox.Show("The price differs more than 10% from the order price. Would you like to insert anyway?", "Insert Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                        }
                    }
                    if (resposta == 6)
                    {

                        //if (int.TryParse(divisao, out teste))
                        //{
                            data_Insert = DateTime.Now.ToString("yyyyMMdd");
                            SQLString = "exec sp_insert_Tb013_Trades " +

                                "@Id_Ordem_1 =" + Id_Ordem +
                                ",@Quantidade_2 =" + Qtd.ToString().Replace(",", ".") +
                                ",@Preco_3 =" + Preco.ToString().Replace(",", ".") +
                                ",@Valor_Financeiro_4 =" + Valor_Financeiro.ToString().Replace(",", ".") + "" +
                                ",@Corretora_5 =" + Id_Corretora.ToString() +
                                ",@Operador_7 =" + Id_User +
                                ",@Data_Trade_8 ='" + data_Insert.ToString() + "'" +
                                ",@Status_Trade_12 =1";

                            resultado = curConn.ExecuteNonQuery(SQLString,1);
                            if (resultado == 0)
                            {
                                MessageBox.Show("Error in Insert!");
                            }
                        //}
                    }
                }
            }
            catch(Exception excep)
            {
                curUtils.Log_Error_Dump_TXT(excep.ToString(), this.Name.ToString());

                //MessageBox.Show(e.ToString());
            }
                this.Close();
                Id_Retorno = "1";
                DialogResult.OK.ToString(); 

        }

        private void txtQtd_TextChanged(object sender, EventArgs e)
        {
            Calcula_VF();
        }

        private void Calcula_VF()
        {
            String SQLString;
            decimal Lote_Padrao;

            SQLString = "Select RoundLot from Tb001_Securities a inner join Tb012_Ordens b" +
                            " on a.IdSecurity = b.Id_Ativo Where Id_Ordem =" + Id_Ordem;
            Lote_Padrao = Convert.ToDecimal(curConn.Execute_Query_String(SQLString));

            double txpreco;
            double txQtd;

            if (double.TryParse(txtPreco.Text, out txpreco) && double.TryParse(txtQtd.Text, out txQtd))
            {

               txtPreco.Text = txtPreco.Text.Replace(",,", ",");
                double ValorFinancdbl = Convert.ToDouble(Convert.ToDecimal(this.txtPreco.Text) * (Convert.ToDecimal(txtQtd.Text) / Lote_Padrao));
                this.txtValorFinanc.Text = ValorFinancdbl.ToString("##,##0.0000########");
            }

        }

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            Calcula_VF();
        }

         private void TxtPreco_Leave(object sender, EventArgs e)
        {
            if (this.txtPreco.Text != "")
            {
                decimal txpreco = Convert.ToDecimal(this.txtPreco.Text);
                this.txtPreco.Text = txpreco.ToString("##,##0.00########");
            }
        }

        private void TxtQtd_Leave(object sender, EventArgs e)
        {
            if (this.txtQtd.Text != "")
            {
                decimal qtd = Convert.ToDecimal(this.txtQtd.Text);
                this.txtQtd.Text = qtd.ToString("##,##0.00");
            }

        }
     }
}