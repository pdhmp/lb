using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using NestDLL;

namespace SGN.Business
{
    public class Business_Class
    {
        Valida Validacao = new Valida();
        public CarregaDados CargaDados = new CarregaDados();

         public int Cancela_Ordem(double Id_Ordem, int Messagebox_Visible)
        {
            int resposta;
            if (Messagebox_Visible != 0)
            {
                resposta = 6;
            }
            else
            {
                resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this order?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
            }

            if (resposta == 6)
            {
                String SQLString = "exec proc_Cancel_Ordem @Id_Ordem = " + Id_Ordem ;

                return CargaDados.curConn.ExecuteNonQuery(SQLString,1);
            }
            else
            {
                return 0;
            }
        }
        public int Cancela_Ordem_Trade(double Id_Ordem, int Messagebox_Visivle)
        {
            int resposta;
            if (Messagebox_Visivle != 0)
            {
                resposta = 6;
            }
            else
            {
                resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this order and your Trades ?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
            }

            if (resposta == 6)
            {
                String SQLString = "exec Proc_Cancel_All_Trades_Order @Id_ordem = " + Id_Ordem;

                return CargaDados.curConn.ExecuteNonQuery(SQLString,1);
            }
            else
            {
                return 0;
            }
        }
        public int Inserir_Trade(double Id_Ordem)
        {
            string retorno;

            frmInsertTrade Trade = new frmInsertTrade();
            Trade.Id_Ordem = Id_Ordem;
            Trade.Id_Retorno = "0";
            Trade.ShowDialog();
            retorno = Trade.Id_Retorno.ToString();

            if (retorno == "1")
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public int Inserir_Divisao(double Id_Ordem, string Ticker, string Quantity)
        {
            /*
            frmDivisaoBoletas divisao = new frmDivisaoBoletas();
            divisao.Id_Ordem = Id_Ordem;
            divisao.Id_User = Id_user;
            divisao.Id_Retorno = "0";
            divisao.ShowDialog();
            retorno = divisao.Id_Retorno.ToString();
            

            //frmAlocate_Trade Aloca = new frmAlocate_Trade();

            //Aloca.Id_User = Id_user;
            //Aloca.lblQuantity.Text = Convert.ToString(Math.Abs(Convert.ToDouble(Quantity)));
            //Aloca.lblTicker.Text = Ticker.ToString();
            //Aloca.Id_Retorno = 0;
            //Aloca.ShowDialog();
            //int retorno = Aloca.Id_Retorno;

            

            if (retorno == 1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
            */
            return 0;
        }

        public int Insert_Order(double Id_Order, double Id_Account, Boolean Edit_Order, int Ticker, decimal Qtd, decimal Preco, string expiration, int brooker, int Book, int Section, int Order_Type)
        {
           return Insert_Order( Id_Order,  Id_Account,  Edit_Order,  Ticker,  Qtd,  Preco,  expiration,  brooker,  Book,  Section,  Order_Type,0,0);
        }

        public int Insert_Order(double Id_Order, double Id_Account, Boolean Edit_Order, int Ticker, decimal Qtd, decimal Preco, string expiration, int brooker, int Book, int Section, int Order_Type,int IsFoward, decimal Rate_Foward)
        {
            try
            {
                Boolean resumefunc;
                decimal VFinanc;
                resumefunc = false;
                string data_insert;
                string data_validade;
                string SQLString;
                int resultado;
                decimal Lote_Padrao;
                DateTime dtexpiration;

                if (Qtd.ToString() == "")
                {
                    MessageBox.Show("Insert a valid Amount!");
                    resumefunc = true;
                }

                if (Preco.ToString() == "")
                {
                    MessageBox.Show("Insert a valid Price!!!");
                    resumefunc = true;
                }

                if (resumefunc == false)
                {
                    SQLString = "Select RoundLot from dbo.Tb001_Securities Where IdSecurity =" + Ticker.ToString();
                    Lote_Padrao = Convert.ToDecimal(CargaDados.curConn.Execute_Query_String(SQLString));

                    VFinanc = (Preco * (Qtd / Lote_Padrao));

                    data_insert = DateTime.Now.ToString("yyyyMMdd");

                    if (expiration.ToString() != "")
                    {
                        dtexpiration = Convert.ToDateTime(expiration.ToString());
                        data_validade = dtexpiration.ToString("yyyyMMdd");
                    }
                    else
                    {
                        data_validade = "<NULL>";
                    }

                    if (data_validade == "<NULL>")
                    {
                        data_validade = "NULL";
                    }
                    else
                    {
                        data_validade = "'" + data_validade + "'";
                    }

                    if (IsFoward == 0)
                    {
                        if (Edit_Order == false)
                        {
                            SQLString = "exec proc_insert_Tb012_Ordens " +
                                        "@Id_Ativo_1  =" + Ticker.ToString() +
                                        ", @Quantidade_2 =" + Qtd.ToString().Replace(",", ".") +
                                        ", @Preco_3 =" + Preco.ToString().Replace(",", ".") +
                                        ", @Valor_Financeiro_4 =" + VFinanc.ToString().Replace(",", ".") +
                                        ", @Book_5 =" + Book.ToString() +
                                        ", @Section_6 =" + Section.ToString() +
                                        ", @Tipo_Mercado_7 =" + 0 +
                                        ", @Operador_8 =" + NestDLL.NUserControl.Instance.User_Id.ToString() +
                                        ", @Data_Abert_Ordem_9 ='" + data_insert + "'" +
                                        ", @Status_Ordem_11 = 1" +
                                        ",  @Id_Corretora_14 =" + brooker.ToString() +
                                        ", @Data_Valid_Ordem_15 =" + data_validade +
                                        ", @Id_Account_17 =" + Id_Account +
                                        ",@Id_Order_Broker_16=0";
                            resultado = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
                       }
                        else
                        {
                            decimal Qtd_execut;
                            string Qtd_string;

                            SQLString = "Select sum(quantidade) as QTD from Tb013_Trades where id_ordem =" + Id_Order;
                            Qtd_string = CargaDados.curConn.Execute_Query_String(SQLString);

                            SQLString = "Select operador  from Tb012_Ordens where id_ordem =" + Id_Order;
                            string OP_string = CargaDados.curConn.Execute_Query_String(SQLString);

                            Qtd_execut = Convert.ToDecimal(Qtd_string);

                            if (Qtd >= Qtd_execut)
                            {
                                Cancela_Ordem(Id_Order, 1);
                                MessageBox.Show("Error on Insert Order!");
                                resultado = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
                            }
                            else
                            {
                                resultado = 999;
                            }
                        }
                    }
                    else
                    {
                        Rate_Foward = Rate_Foward / 100;
                        SQLString = "exec Proc_Insert_Fowards " +
                            "@Id_Account = " +Id_Account  +
                            ",@Id_Ticker  =" + Ticker.ToString() +
                            ",@Id_Book =" + Book.ToString() +
                            ",@Id_Section =" + Section.ToString() +
                            ",@Quantity =" + Qtd.ToString().Replace(",", ".") +
                            ",@Price =" + Preco.ToString().Replace(",", ".") +
                            ",@Cash =" + VFinanc.ToString().Replace(",", ".") +
                            ",@Rate_Foward=" + Rate_Foward.ToString().Replace(",", ".") +
                            ",@Trade_Date ='" + data_insert + "'" +
                            ",@Id_User =" + NestDLL.NUserControl.Instance.User_Id.ToString() +
                            ",@Status_Foward = 1";
                     resultado = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
 
                    }
                    if (resultado == 0)
                    {
                        MessageBox.Show("Error in Insert");
                    }

                    return resultado;
                }
                else
                {
                    return 999;
                }
            }
            catch
            {
                //Valida.Error_Dump_TXT(e.ToString());

                return 0;
            }
        }

        public int Insere_PL(int Id_Portfolio, DateTime Data_PL, decimal Valor_PL)
        {
            try
            {
             string SQLString;
            int resultado;

            SQLString = "exec proc_insert_Tb025_Valor_PL " + Id_Portfolio + ",'" + Data_PL.ToString("yyyyMMdd") + "'," + Valor_PL.ToString().Replace(",", ".");
            resultado = CargaDados.curConn.ExecuteNonQuery(SQLString,1);

            SQLString = "INSERT INTO NESTLOG.dbo.Tb211_Log_PL(Id_User,Id_Portfolio,Date_Selected,Host,Type_PL,PL)" +
                        "VALUES(" + NestDLL.NUserControl.Instance.User_Id + "," + Id_Portfolio + ",'" + Data_PL.ToString("yyyyMMdd") + "',host_name(),'INS'," + Valor_PL.ToString().Replace(",", ".") + ")";
            resultado = CargaDados.curConn.ExecuteNonQuery(SQLString,1);

                return resultado;
            }
            catch(Exception e)
            {
                //Valida.Error_Dump_TXT(e.ToString());

                MessageBox.Show(e.Source.ToString());
                return 0;
            }
        }

        public int Insert_Cash(int Id_Portfolio, int Id_Ticker, DateTime Cash_Date, decimal Cash_Amount)
        {
            try
            {
                string SQLString;
                int resultado;

                SQLString = "exec proc_insert_Tb026_Cash_Value " + Id_Portfolio + ", " + Id_Ticker + ",'" + Cash_Date.ToString("yyyyMMdd") + "'," + Cash_Amount.ToString().Replace(",", ".");
                resultado = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);

                return resultado;
            }
            catch (Exception e)
            {
                Validacao.Error_Dump_TXT(e.ToString(), "Insert_Cash");

                //MessageBox.Show(e.Source.ToString());
                return 0;
            }
        }

        public int Insere_Vol(int IdSecurity, string Valor_Vol)
        {
            try
            {
                string SQLString;
                int resultado;
                SQLString = "EXEC Proc_Insert_Price @ID_Ativo=" + IdSecurity + ",@Valor=" + Valor_Vol + ",@Data='" + DateTime.Now.ToString("yyyyMMdd") + "',@Tipo_Preco=40,@Source=7,@Automated=0";
                resultado = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);;
                    return resultado;
            }
            catch (Exception e)
            {
                Validacao.Error_Dump_TXT(e.ToString(), "Insere_Vol");

                //MessageBox.Show(e.Source.ToString());
                return 0;
            }

        }
        public int Insere_Preco(int IdSecurity, string preco, string tipo_preco, string data)
        {
            string tabela;
            decimal teste;
            int teste2;
            int resultado;
            string SQLString = "";
            string SQLString_RT = "";
            string SQLString_HIST = "";

            try
            {
                if (decimal.TryParse(preco, out teste) && int.TryParse(tipo_preco, out teste2))
                {

                    SQLString_RT = " exec [NESTRT].[dbo].Proc_Insert_Price_RT @Id_Ativo=" + IdSecurity + ", @Valor=" + preco.Replace(",", ".") + ", @Data='" + DateTime.Now.ToString("yyyyMMdd") + "', @Tipo_preco=" + tipo_preco + ",@Source=7,@SourcetimeStamp='" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "',@Automated=0 ;";
                    tabela = Validacao.Retorna_Tabela_Preco_Trade(IdSecurity, 0);
                   
                    SQLString_HIST = "exec Proc_Insert_Price @Id_Ativo=" + IdSecurity + ", @Valor=" + preco.Replace(",", ".") + ", @Data='" + data + "', @Tipo_preco=" + tipo_preco + ",@Source=7,@Automated=0" +
                                "; INSERT INTO NESTLOG.dbo.Tb210_Log_Precos(Tabela,Id_Usuario,Id_Ativo,Valor,Tipo_preco,Data) values" +
                                "('" + tabela + "'," + NestDLL.NUserControl.Instance.User_Id + "," + IdSecurity + "," + preco.Replace(",", ".") + ","
                                + tipo_preco + ",'" + data + "')";

                    if (data == DateTime.Now.ToString("yyyyMMdd"))
                    {
                        SQLString = SQLString_RT + SQLString_HIST;
                    }
                    else
                    {
                        SQLString = SQLString_HIST;
                    }

                    resultado = CargaDados.curConn.ExecuteNonQuery(SQLString,1);

                }
                else
                {
                    resultado = 0;
                }

                return resultado;
            }
            catch(Exception e)
            {
                Validacao.Error_Dump_TXT(e.ToString(), "Insere_Preco");

                return 0; 
            }

        }

        public int Edit_Price_Trade(int Id_Trade, string New_Price)
        {
            string SQLString;
            decimal teste;

            if (decimal.TryParse(New_Price, out teste) && Id_Trade != 0)
            {
                SQLString = "exec proc_update_Price_Tb013_Trades @Preco_2 = " + New_Price.Replace(",", ".") + ",@Id_Trade_1=" + Id_Trade;
                int resultado = CargaDados.curConn.ExecuteNonQuery(SQLString,1);
                return resultado;
            }
            else
            {
                return 0;
            }
        }

        public int Busca_Dados_Historicos(int IdSecurity, DateTime IniDate)
        {
            try
            {
                string SQLString;

                SQLString = " INSERT INTO Tb207_Pending " +
                            " SELECT IdSecurity, '" + IniDate.ToString("yyyy-MM-dd") + "' AS Ini_Date, 1 AS Source, 1 AS Status, 0 AS IsRT  " +
                            " FROM Tb001_Securities (nolock)  " +
                            " WHERE IdSecurity=" + IdSecurity +
                            " UNION ALL " +
                            " SELECT IdSecurity, '" + IniDate.ToString("yyyy-MM-dd") + "' AS Ini_Date, 1 AS Source, 1 AS Status, 1 AS IsRT  " +
                            " FROM Tb001_Securities (nolock)  " +
                            " WHERE IdSecurity=" + IdSecurity;


                if (CargaDados.curConn.ExecuteNonQuery(SQLString, 1) != 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch(Exception e)
            {
                Validacao.Error_Dump_TXT(e.ToString(), "Busca_Dados_Historicos");

                return 0;
            }

        }
 
    }
}
