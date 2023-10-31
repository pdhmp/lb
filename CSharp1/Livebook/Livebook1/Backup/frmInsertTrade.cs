using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.CargaDados;
using SGN.Validacao;
using SGN.Business;
using System.Data.SqlClient;

namespace SGN
{
    public partial class frmInsertTrade : Form
    {
        public double Id_Ordem;
        public int Id_Usuario;
        public string Id_Retorno;
        public Boolean Perm_Usuario;
        public double Id_Corretora;
        public decimal Price_Order;
        CarregaDados Carga = new CarregaDados();
        Valida Valida = new Valida();
         Business_Class Negocio = new Business_Class();
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
           // Perm_Usuario =  Valida.Verifica_Acesso(Id_Usuario, 2);

        }

        private void Carrega_Trade(double Id_Ordem)
        {
            string SqlString;
            Boolean ordemTrade = false;
            SqlString = "SET DATEFORMAT DMY;exec sp_TotalOrdem_Trades_Restante @Id_Ordem=" + Id_Ordem;
            SqlDataReader dados;
                 dados = Carga.DB.Execute_Query_DataRead(SqlString);
                 double Lote_Padrao;
            while (dados.Read())
             {
                 SqlString = "Select Lote_Padrao from Tb001_Ativos a inner join Tb012_Ordens b" +
                                   " on a.Id_Ativo = b.Id_Ativo Where Id_Ordem =" + Id_Ordem;
                 Lote_Padrao = Convert.ToDouble(Carga.DB.Execute_Query_String(SqlString));
                 
                 // se tiver trandes para ordens roda esta rotina
                 this.lblCart.Text = dados["Carteira"].ToString();
                 this.lblAtivo.Text = dados["Simbolo"].ToString();   
                 //executados

                 decimal PrecoExec = Decimal.Round(Convert.ToDecimal(dados["Preco_Medio"].ToString()), 2);
                 this.txtPrecoExec.Text = PrecoExec.ToString("#,##0.00");

                  decimal QtdExec = Decimal.Round(Convert.ToDecimal(dados["QTD"].ToString()), 2);
                  this.txtQtdExec.Text = QtdExec.ToString("#,##0");

                  decimal VfExec = Decimal.Round(Convert.ToDecimal(dados["VF"].ToString()), 2);
                  this.txtVfExec.Text = VfExec.ToString("#,##0.00");
                  
                  
                //ordem
                 
                decimal PrecoOrdem = Decimal.Round(Convert.ToDecimal(dados["preco"].ToString()),2);
                Price_Order = PrecoOrdem;
                this.txtPrecoOrdem.Text = PrecoOrdem.ToString("#,##0.00");

                 decimal QTDOrdem = Decimal.Round(Convert.ToDecimal(dados["Quantidade"].ToString()), 2);
                 this.txtQtdordem.Text = QTDOrdem.ToString("#,##0");
                
                //trade

                 decimal PrecoRest = Decimal.Round(Convert.ToDecimal(dados["Preco_Medio"].ToString()), 2);
                 //txtPreco.Text = PrecoRest.ToString("#,##0.00");
                 
                 decimal QTDRest = Decimal.Round(Convert.ToDecimal(dados["Restante"].ToString()), 2);
                 this.txtQtd.Text = QTDRest.ToString("#,##0");

                 Decimal Valor_Fianc = decimal.Round(PrecoRest * QTDRest, 2);

                 txtValorFinanc.Text = Valor_Fianc.ToString("#,##0.00");

                 double preco = Convert.ToDouble(dados["preco"]);
                 double QTD = Convert.ToDouble(dados["Quantidade"]);
                 double VF = preco * (QTD / Lote_Padrao);
                 this.txtVFOrdem.Text = VF.ToString("#,##0.00");

                 //pendente
                 decimal Qtdpend = Decimal.Round(Convert.ToDecimal(dados["Restante"].ToString()), 2);
                 this.txtQtdpend.Text = Qtdpend.ToString("#,##0");

                 double Vfpend = VF - Convert.ToDouble(dados["VF"]);
                 this.txtVFpend.Text = Vfpend.ToString("#,##0.00");

                 decimal precopend = (Convert.ToDecimal(VF) - VfExec) / Qtdpend;

                 this.txtPrecoPend.Text = precopend.ToString("#,##0.00");
                 txtPreco.Text = precopend.ToString("#,##0.00");
                 ordemTrade = true;
                 Id_Corretora = Convert.ToDouble(dados["Id_Corretora"].ToString());
             }
             dados.Close();
             dados.Dispose();
             if (ordemTrade == false)
             {
                 //senão roda esta
                 SqlString = "Select Id_Ordem,Tb012_Ordens.Id_Carteira,Carteira, Quantidade,Preco,Valor_Financeiro, Simbolo, Id_Corretora" +
                             " from Tb012_Ordens inner join Tb001_Ativos on" +
                             " Tb001_Ativos.Id_Ativo = Tb012_Ordens.Id_Ativo" +
                             " inner join Tb002_Carteiras on " +
                             " Tb002_Carteiras.Id_Carteira = Tb012_Ordens.Id_Carteira" + 
                             " Where Id_Ordem=" + Id_Ordem ;
                 dados = Carga.DB.Execute_Query_DataRead(SqlString);

                 while (dados.Read())
                 {
                     SqlString = "Select Lote_Padrao from Tb001_Ativos a inner join Tb012_Ordens b" +
                                        " on a.Id_Ativo = b.Id_Ativo Where Id_Ordem =" + Id_Ordem;
                     Lote_Padrao = Convert.ToDouble(Carga.DB.Execute_Query_String(SqlString));

                     this.lblCart.Text = dados["Carteira"].ToString();
                     this.lblAtivo.Text = dados["Simbolo"].ToString();
                     //pendente
                     decimal Qtdpenddbl = Convert.ToDecimal(dados["Quantidade"].ToString());

                     this.txtQtdpend.Text = Qtdpenddbl.ToString("##,###");
                     //this.txtQtdpend.Text = dados["Quantidade"].ToString();
                     //ordem

                     
                     decimal precodbl = Convert.ToDecimal(dados["preco"].ToString());
                     this.txtPrecoOrdem.Text = precodbl.ToString("##,###.00########");
                     Price_Order = precodbl;
                     decimal Qtdordemdbl = Convert.ToDecimal(dados["Quantidade"].ToString());
                     this.txtQtdordem.Text = Qtdordemdbl.ToString("##,###");
                     double preco = Convert.ToDouble(dados["preco"]);
                     double QTD = Convert.ToDouble(dados["Quantidade"]);
                     double VF = preco * (QTD / Lote_Padrao);
                     this.txtVFOrdem.Text = VF.ToString("##,###.00########");
                     //trade

                     this.txtVFpend.Text = VF.ToString("##,###.00########");
                     
                     double Precopenddbl = Convert.ToDouble(txtVFpend.Text) / Convert.ToDouble(txtQtdpend.Text);
                     this.txtPrecoPend.Text = Precopenddbl.ToString("##,###.00########");

                     double Precodbl = Convert.ToDouble(dados["preco"].ToString());
                     txtPreco.Text = Precodbl.ToString("##,###.00########");

                     double txtqtddbl = Convert.ToDouble(dados["Quantidade"].ToString());
                     this.txtQtd.Text = txtqtddbl.ToString("##,###");
                     Id_Corretora = Convert.ToDouble(dados["Id_Corretora"].ToString());
                 };
                 dados.Dispose();
             }
             SqlString = "Select Rebate_padrao from Tb011_Corretoras Where Id_Corretora = " + this.Id_Corretora;
             this.txtRebate.Text = Carga.DB.Execute_Query_String(SqlString);
        
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            
            try{
                double temp_total =Convert.ToDouble(this.txtQtd.Text.Replace("-",""))  ;
                double tem_pend = Convert.ToDouble(this.txtQtdpend.Text.Replace("-", ""));
                int resposta;
                if (tem_pend < temp_total)
                {//se for mair q saldo, entra aqui e checa se quer altera ordem
                    MessageBox.Show("Amount Greater than Amount Outstanding!");

                    resposta = Convert.ToInt32(MessageBox.Show("Do you wish to modify the order?", "Orders", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                    if (resposta == 6)
                    {
                        Negocio.Editar_Ordem(Id_Ordem);
                    }
                }
                else
                {//se for menor  q saldo entra aqui e insere
                    int resultado;
                    string SqlString;
                    decimal Valor_Financeiro = Convert.ToDecimal(txtValorFinanc.Text);
                    decimal Qtd = Convert.ToDecimal(txtQtd.Text);
                    decimal Preco = Convert.ToDecimal(txtPreco.Text);
                    decimal TaxaBovespa;
                    decimal TaxaCBLC;
                    decimal TaxaCorretagem;
                    decimal Percentual_Rebate;
                    TaxaCorretagem = 1;

                    Percentual_Rebate = Convert.ToDecimal(this.txtRebate.Text);
                    
                    
                    //TaxaCorretagem = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal("0,005") + Convert.ToDecimal("25,21")) * (1 - Percentual_Rebate), 2);

                    SqlString = "Select a.Id_Ativo, a.Id_Tipo_Ativo,a.Lote_Negociacao, Data_Abert_ordem from Tb001_Ativos a " +
                                " inner join Tb012_Ordens b on b.Id_Ativo = a.Id_Ativo" +
                                " Where Id_Ordem =" + Id_Ordem;

                    SqlDataReader dr_ativo = Carga.DB.Execute_Query_DataRead(SqlString);
                    dr_ativo.Read();
                    int Id_Ativo = Convert.ToInt32(dr_ativo[0]);
                    int Id_Tipo_Ativo = Convert.ToInt32(dr_ativo[1]);
                    Decimal Lote_Negociacao = Convert.ToDecimal(dr_ativo[2]);
                    string data_Insert = Convert.ToString(dr_ativo[3].ToString());

                    dr_ativo.Close();
                    dr_ativo.Dispose();

                    switch(Id_Tipo_Ativo)
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

                    string divisao = Convert.ToString(Convert.ToDecimal(this.txtQtd.Text) / Lote_Negociacao);

                   divisao = divisao.Replace(",00", "");
                    //int teste;
                    resposta = 6;
                    if (Price_Order > 0)
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

                    if (resposta == 6)
                    {

                        //if (int.TryParse(divisao, out teste))
                        //{
                           // string data_Insert = DateTime.Now.ToString("yyyyMMdd");
                            SqlString = "SET DATEFORMAT DMY; exec sp_insert_Tb013_Trades " +

                                "@Id_Ordem_1 =" + Id_Ordem +
                                ",@Quantidade_2 =" + Qtd.ToString().Replace(",", ".") +
                                ",@Preco_3 =" + Preco.ToString().Replace(",", ".") +
                                ",@Valor_Financeiro_4 =" + Valor_Financeiro.ToString().Replace(",", ".") + "" +
                                ",@Corretora_5 =" + Id_Corretora.ToString() +
                                ",@Rebate_Corretagem_6 =" + Percentual_Rebate.ToString().Replace(",", ".") +
                                ",@Operador_7 =" + Id_Usuario +
                                ",@Data_Trade_8 ='" + data_Insert.ToString() + "'" +
                                ",@Corretagem_9 =" + TaxaCorretagem.ToString().Replace(",", ".") +
                                ",@Taxa_Operac_CBLC_10 =" + TaxaCBLC.ToString().Replace(",", ".") +
                                ",@TaxaOOperac_BOV_11 =" + TaxaBovespa.ToString().Replace(",", ".") +
                                ",@Status_Trade_12 =1";

                            resultado = Carga.DB.Execute_Insert_Delete_Update(SqlString);
                            if (resultado == 0)
                            {
                                MessageBox.Show("Error in Insert!");
                            }
                        //}
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }
            finally
                {
                    Id_Retorno = "1";
                    DialogResult.OK.ToString(); 
                this.Close();
                //this.Close();
            }
        }

        private void txtQtd_TextChanged(object sender, EventArgs e)
        {
            Calcula_VF();
        }

        private void Calcula_VF()
        {
            String SqlString;
            decimal Lote_Padrao;

            SqlString = "Select Lote_Padrao from Tb001_Ativos a inner join Tb012_Ordens b" +
                            " on a.Id_Ativo = b.Id_Ativo Where Id_Ordem =" + Id_Ordem;
            Lote_Padrao = Convert.ToDecimal(Carga.DB.Execute_Query_String(SqlString));

            double txpreco;
            double txQtd;

            if (double.TryParse(txtPreco.Text, out txpreco) && double.TryParse(txtQtd.Text, out txQtd))
            {

               txtPreco.Text = txtPreco.Text.Replace(",,", ",");
                double ValorFinancdbl = Convert.ToDouble(Convert.ToDecimal(this.txtPreco.Text) * (Convert.ToDecimal(txtQtd.Text) / Lote_Padrao));
                this.txtValorFinanc.Text = ValorFinancdbl.ToString("##,###.0000########");
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
                this.txtPreco.Text = txpreco.ToString("##,###.00########");
            }
        }

        private void TxtQtd_Leave(object sender, EventArgs e)
        {
            if (this.txtQtd.Text != "")
            {
                decimal qtd = Convert.ToDecimal(this.txtQtd.Text);
                this.txtQtd.Text = qtd.ToString("##,###.00");
            }

        }
     }
}