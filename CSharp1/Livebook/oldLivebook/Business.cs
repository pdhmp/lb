using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

using NestDLL;

namespace LiveBook.Business
{
    public class Business_Class
    {
        LB_Utils curUtils = new LB_Utils();
        newNestConn curConn = new newNestConn();

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
                String SQLString = "exec proc_Cancel_Ordem @Id_Ordem = " + Id_Ordem;
                using (newNestConn curConn = new newNestConn())
                {
                    return curConn.ExecuteNonQuery(SQLString, 1);
                }
            }
            else
            {
                return 0;
            }
        }

        public int Cancela_Ordem_Trade(double Id_Ordem, int Messagebox_Visivle)
        {
            using (newNestConn curConn = new newNestConn())
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

                    return curConn.ExecuteNonQuery(SQLString, 1);
                }
                else
                {
                    return 0;
                }
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
           return Insert_Order( Id_Order,  Id_Account,  Edit_Order,  Ticker,  Qtd,  Preco,  expiration,  brooker,  Book,  Section,  Order_Type,0,0,0,0);
        }

        public int Insert_Order(double Id_Order, double Id_Account, Boolean Edit_Order, int Ticker, decimal Qtd, decimal Preco, string expiration, int brooker, int Book, int Section, int Order_Type,int IsFoward, decimal SpotPrice, decimal PriceWBrokerage, decimal SpotPriceWBrokerage)
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
                    int IdSide = 0;

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
                        SQLString = "Select RoundLot from dbo.Tb001_Securities (nolock) Where IdSecurity =" + Ticker.ToString();
                        Lote_Padrao = Convert.ToDecimal(curConn.Execute_Query_String(SQLString));

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

                        if (Qtd >= 0)
                        {
                            IdSide = 1;
                        }
                        else
                        {
                            IdSide = 2;
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
                                            ",@Id_Order_Broker_16=0,@Id_Ticker_Debt_18=0" +
                                            ",@IdSide = " + IdSide;
                                            using (newNestConn curConn1 = new newNestConn())
                                            {
                                                resultado = curConn1.ExecuteNonQuery(SQLString, 1);
                                            }
                            }
                            else
                            {
                                decimal Qtd_execut;
                                string Qtd_string;

                                SQLString = "Select sum(quantidade) as QTD from Tb013_Trades where id_ordem =" + Id_Order;
                                using (newNestConn curConn1 = new newNestConn())
                                {
                                    Qtd_string = curConn1.Execute_Query_String(SQLString);
                                }
                                SQLString = "Select operador  from Tb012_Ordens where id_ordem =" + Id_Order;
                                using (newNestConn curConn1 = new newNestConn())
                                {

                                    string OP_string = curConn1.Execute_Query_String(SQLString);
                                }
                                Qtd_execut = Convert.ToDecimal(Qtd_string);

                                if (Qtd >= Qtd_execut)
                                {
                                    Cancela_Ordem(Id_Order, 1);
                                    MessageBox.Show("Error on Insert Order!");
                                    using (newNestConn curConn1 = new newNestConn())
                                    {
                                        resultado = curConn1.ExecuteNonQuery(SQLString, 1);
                                    }
                                }
                                else
                                {
                                    resultado =999;
                                }
                            }
                        }
                        else
                        {
                            SQLString = "exec Proc_Insert_Fowards " +
                                "@Id_Account = " + Id_Account +
                                ",@Id_Ticker  =" + Ticker.ToString() +
                                ",@Id_Book =" + Book.ToString() +
                                ",@Id_Section =" + Section.ToString() +
                                ",@Quantity =" + Qtd.ToString().Replace(",", ".") +
                                ",@Price =" + Preco.ToString().Replace(",", ".") +
                                ",@Cash =" + VFinanc.ToString().Replace(",", ".") +
                                ",@Spot_Price=" + SpotPrice.ToString().Replace(",", ".") +
                                ",@Trade_Date ='" + data_insert + "'" +
                                ",@Id_User =" + NestDLL.NUserControl.Instance.User_Id.ToString() +
                                ",@Status_Foward = 1" +
                                ",@PriceWBrokerage = " + PriceWBrokerage.ToString().Replace(",", ".") +
                                ",@SpotPriceWBrokerage = " + SpotPriceWBrokerage.ToString().Replace(",", ".") + " ;";
                            using (newNestConn curConn1 = new newNestConn())
                            {
                                resultado = curConn1.ExecuteNonQuery(SQLString, 1);
                            }
                        }
                        if (resultado == 0 || resultado == 99 )
                        {
                            MessageBox.Show("Error in Insert");
                        }

                        if (resultado == 93)
                        {
                            MessageBox.Show("This Forward already exists in the database. Please check the Security Ticker selected.", "Error on insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        return 1;
                    }
                    else
                    {
                        return 999;
                    }
                }
                catch(System.Data.SqlClient.SqlException ex)
                {
                    MessageBox.Show("Error on insert! Duplicated key" + ex.ToString());
                    return 0;
                }
        }

        public int Insert_Strategy_Transfer(double Id_Order, double Id_Account, Boolean Edit_Order, int Ticker, decimal Qtd, decimal Preco, string expiration, int brooker, int Book_From, int Section_From, int Book_To, int Section_To, int Order_Type)
        {
            return Insert_Strategy_Transfer(Id_Order, Id_Account, Edit_Order, Ticker, Qtd, Preco, expiration, brooker,   Book_From,  Section_From,  Book_To,  Section_To, Order_Type, 0, 0);
        }
        
        public int Insert_Strategy_Transfer(double Id_Order, double Id_Account, Boolean Edit_Order, int Ticker, decimal Qtd, decimal Preco, string expiration, int brooker, int Book_From, int Section_From, int Book_To, int Section_To, int Order_Type, int IsFoward, decimal SpotPrice)
        {
            using (newNestConn curConn = new newNestConn())
            {
                try
                {
                    Boolean resumefunc;
                    decimal VFinanc;
                    resumefunc = false;
                    string data_insert;
                    string data_validade;
                    string SQLString;
                    int resultado = 0;
                    decimal Lote_Padrao;
                    DateTime dtexpiration;
                    int IdSide = 0;

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
                        SQLString = "Select RoundLot from dbo.Tb001_Securities (nolock) Where IdSecurity =" + Ticker.ToString();
                        Lote_Padrao = Convert.ToDecimal(curConn.Execute_Query_String(SQLString));

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

                        if (Qtd >= 0)
                        {
                            IdSide = 1;
                        }
                        else
                        {
                            IdSide = 2;
                        }

                        if (IsFoward == 0)
                        {
                            if (Edit_Order == false)
                            {
                                SQLString = "exec proc_Strategy_Transfer " +
                                            "@Id_Ativo_1  =" + Ticker.ToString() +
                                            ", @Quantidade_2 =" + Qtd.ToString().Replace(",", ".") +
                                            ", @Preco_3 =" + Preco.ToString().Replace(",", ".") +
                                            ", @Valor_Financeiro_4 =" + VFinanc.ToString().Replace(",", ".") +
                                            ", @Book_From_5 =" + Book_From.ToString() +
                                            ", @Section_From_6 =" + Section_From.ToString() +
                                            ", @Book_To_5 =" + Book_To.ToString() +
                                            ", @Section_To_6 =" + Section_To.ToString() +
                                            ", @Tipo_Mercado_7 =" + 0 +
                                            ", @Operador_8 =" + NestDLL.NUserControl.Instance.User_Id.ToString() +
                                            ", @Data_Abert_Ordem_9 ='" + data_insert + "'" +
                                            ", @Status_Ordem_11 = 1" +
                                            ",  @Id_Corretora_14 =" + brooker.ToString() +
                                            ", @Data_Valid_Ordem_15 =" + data_validade +
                                            ", @Id_Account_17 =" + Id_Account +
                                            ",@Id_Order_Broker_16=0,@Id_Ticker_Debt_18=0" +
                                            ",@IdSide = " + IdSide;
                                resultado = curConn.ExecuteNonQuery(SQLString, 1);
                            }
                            else
                            {
                                decimal Qtd_execut;
                                string Qtd_string;

                                SQLString = "Select sum(quantidade) as QTD from Tb013_Trades where id_ordem =" + Id_Order;
                                Qtd_string = curConn.Execute_Query_String(SQLString);

                                SQLString = "Select operador  from Tb012_Ordens where id_ordem =" + Id_Order;
                                string OP_string = curConn.Execute_Query_String(SQLString);

                                Qtd_execut = Convert.ToDecimal(Qtd_string);

                                if (Qtd >= Qtd_execut)
                                {
                                    Cancela_Ordem(Id_Order, 1);
                                    MessageBox.Show("Error on Insert Order!");
                                    resultado = curConn.ExecuteNonQuery(SQLString, 1);
                                }
                                else
                                {
                                    resultado = 999;
                                }
                            }
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
                    //curUtils.Log_Error_Dump_TXT(e.ToString());

                    return 0;
                }
            }
        }

        public int Insere_PL(int Id_Portfolio, DateTime Data_PL, decimal Valor_PL)
        {
            try
            {
                string SQLString;
                int resultado;

                SQLString = "exec proc_insert_Tb025_Valor_PL " + Id_Portfolio + ",'" + Data_PL.ToString("yyyyMMdd") + "'," + Valor_PL.ToString().Replace(",", ".");
                resultado = curConn.ExecuteNonQuery(SQLString,1);

                SQLString = "INSERT INTO NESTLOG.dbo.Tb211_Log_PL(Id_User,Id_Portfolio,Date_Selected,Host,Type_PL,PL)" +
                            "VALUES(" + NestDLL.NUserControl.Instance.User_Id + "," + Id_Portfolio + ",'" + Data_PL.ToString("yyyyMMdd") + "',host_name(),'INS'," + Valor_PL.ToString().Replace(",", ".") + ")";
                resultado = curConn.ExecuteNonQuery(SQLString,1);

                return resultado;
            }
            catch(Exception e)
            {
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
                resultado = curConn.ExecuteNonQuery(SQLString, 1);

                return resultado;
            }
            catch (Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), "Insert_Cash");

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
                resultado = curConn.ExecuteNonQuery(SQLString, 1);;
                    return resultado;
            }
            catch (Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), "Insere_Vol");

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

                    SQLString_RT = " exec [NESTRT].[dbo].Proc_Insert_Price_RT " + IdSecurity + ", " + preco.Replace(",", ".") + ", '" + DateTime.Now.ToString("yyyyMMdd") + "', " + tipo_preco + ",7,'" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss") + "',0 ;";
                    tabela = curUtils.Retorna_Tabela_Preco_Trade(IdSecurity, 0);
                   
                    SQLString_HIST = "exec Proc_Insert_Price " + IdSecurity + ", " + preco.Replace(",", ".") + ", '" + data + "', " + tipo_preco + ",7,0" +
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

                    resultado = curConn.ExecuteNonQuery(SQLString,1);

                }
                else
                {
                    resultado = 0;
                }

                return resultado;
            }
            catch(Exception e)
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), "Insere_Preco");

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
                int resultado = curConn.ExecuteNonQuery(SQLString,1);
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

                if (curConn.ExecuteNonQuery(SQLString, 1) != 0)
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
                curUtils.Log_Error_Dump_TXT(e.ToString(), "Busca_Dados_Historicos");

                return 0;
            }

        }

        public void Exercise_Options(int IdPortfolio, int IdAccountUnderlying,int IdUnderlying, double Quantity, int IdSide, Int32 IdSecurity, Boolean OTM, double Strike, int IdBook, int IdSection, Boolean NetInsert)
        {
            newNestConn curConn = new newNestConn();
            int IdAccountOption = 0;
            string StringSQL;

            StringSQL = "Select Id_Account FROM NESTDB.dbo.VW_Account_Broker WHERE Id_Broker = 31 AND Id_Portfolio = " + IdPortfolio;
            IdAccountOption = curConn.Return_Int(StringSQL);
            
            StringSQL = "Select Id_Broker FROM NESTDB.dbo.Tb007_Accounts WHERE Id_Account = " + IdAccountUnderlying;
            int IdBroker = curConn.Return_Int(StringSQL);

            StringSQL = "Select OptionType FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE IdSecurity = " + IdSecurity;
            int TIPOCP = curConn.Return_Int(StringSQL);

            int IdSideSecurity;
            if (IdSide == 1)
            {
                IdSideSecurity = 2;
            }
            else
            {
                IdSideSecurity = 1;
            }

            if (OTM)
            {
                ExpireOptionsOrder(IdAccountOption, IdSecurity, Convert.ToDecimal(Quantity), Convert.ToDecimal(0.000001), IdBook, IdSection, DateTime.Now, IdSideSecurity);
            }
            else
            {
                int IdSideUnderlying=0;
                if (TIPOCP == 1 && IdSide == 1)
                {
                    IdSideUnderlying = 1;
                }
                if (TIPOCP == 1 && IdSide == 2)
                {
                    IdSideUnderlying = 2;
                }
                if (TIPOCP == 0 && IdSide == 1)
                {
                    IdSideUnderlying = 2;
                }
                if (TIPOCP == 0 && IdSide == 2)
                {
                    IdSideUnderlying = 1;
                }

                if (NetInsert == false)
                {
                    ExpireOptionsOrder(IdAccountOption, IdSecurity, Convert.ToDecimal(Quantity), Convert.ToDecimal(0.000001), IdBook, IdSection, DateTime.Now, IdSideSecurity);
                    ExpireOptionsOrder(IdAccountUnderlying, IdUnderlying, Convert.ToDecimal(Quantity), Convert.ToDecimal(Strike), IdBook, IdSection, DateTime.Now, IdSideUnderlying);
                }
                else
                {
                    ExpireOptionsOrder(IdAccountUnderlying, IdUnderlying, Convert.ToDecimal(Quantity), Convert.ToDecimal(Strike), IdBook, IdSection, DateTime.Now, IdSideUnderlying);
                }
           }
       }

        private void ExpireOptionsOrder(double IdAccount, int IdSecurity, decimal Qtt, decimal Price, int IdBook, int IdSection, DateTime Date,int IdSide)
        {
            double IdOrder;
            string SQLString;

            using (newNestConn curConn = new newNestConn())
            {
                SQLString = "Select RoundLot from dbo.Tb001_Securities (nolock) Where IdSecurity =" + IdSecurity.ToString();
                decimal Lote_Padrao = Convert.ToDecimal(curConn.Execute_Query_String(SQLString));


                if (IdSide == 2)
                {
                    Qtt = Qtt * -1;
                }

                decimal Cash = (Price * (Qtt / Lote_Padrao));

                SQLString = "Select Id_Broker FROM NESTDB.dbo.Tb007_Accounts WHERE Id_Account = " + IdAccount;

                int IdBroker = curConn.Return_Int(SQLString);

                SQLString = "exec proc_insert_Tb012_Ordens " +
                                                    "@Id_Ativo_1  =" + IdSecurity.ToString() +
                                                    ", @Quantidade_2 =" + Qtt.ToString().Replace(",", ".") +
                                                    ", @Preco_3 =" + Price.ToString().Replace(",", ".") +
                                                    ", @Valor_Financeiro_4 =" + Cash.ToString().Replace(",", ".") +
                                                    ", @Book_5 =" + IdBook.ToString() +
                                                    ", @Section_6 =" + IdSection.ToString() +
                                                    ", @Tipo_Mercado_7 =18" +
                                                    ", @Operador_8 =" + NestDLL.NUserControl.Instance.User_Id.ToString() +
                                                    ", @Data_Abert_Ordem_9 ='" + Date.ToString("yyyy-MM-dd") + "'" +
                                                    ", @Status_Ordem_11 = 1" +
                                                    ",  @Id_Corretora_14 =" + IdBroker.ToString() +
                                                    ", @Data_Valid_Ordem_15 ='1900-01-01'" +
                                                    ", @Id_Account_17 =" + IdAccount +
                                                    ",@Id_Order_Broker_16=0,@Id_Ticker_Debt_18=0" +
                                                    ",@IdSide = " + IdSide + " ; SELECT @@Identity";

                IdOrder = curConn.Return_Double(SQLString);

                ExpireOptionsTrade(IdOrder, Qtt, Price, Cash, IdBook, NestDLL.NUserControl.Instance.User_Id);

            }
        }

        void ExpireOptionsTrade(double IdOrder, decimal Qtt, decimal Price, decimal Cash, int IdBroker, int IdUser)
        {
            try
            {
                int resultado;
                string SQLString;
                decimal TaxaBovespa;
                decimal TaxaCBLC;
                decimal TaxaCorretagem;
                decimal Percentual_Rebate;
                TaxaCorretagem = 1;
                string data_Insert;

                Percentual_Rebate = 99;

                //TaxaCorretagem = Math.Abs(Decimal.Round(Valor_Financeiro * Convert.ToDecimal("0,005") + Convert.ToDecimal("25,21")) * (1 - Percentual_Rebate), 2);

                SQLString = "Select a.IdSecurity, a.IdPriceTable,a.RoundLotSize, Data_Abert_ordem from Tb001_Securities a(NOLOCK) " +
                            " inner join Tb012_Ordens b on b.Id_Ativo = a.IdSecurity" +
                            " Where Id_Ordem =" + IdOrder;

                int IdSecurity;
                int IdPriceTable = 0;
                Decimal RoundLotSize = 1;

                using (newNestConn curConn = new newNestConn())
                {

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
                            TaxaBovespa = Math.Abs(Decimal.Round(Cash * Convert.ToDecimal(0.00019), 2));
                            TaxaCBLC = Math.Abs(Decimal.Round(Cash * Convert.ToDecimal(0.00006), 2));
                            TaxaCorretagem = Decimal.Round(Convert.ToDecimal(Math.Abs((Convert.ToDouble(Cash) * 0.005 + 25.21) * (1 - Convert.ToDouble(Percentual_Rebate)))), 2);
                            break;

                        case 7:
                            TaxaBovespa = Math.Abs(Decimal.Round(Cash * Convert.ToDecimal(0.00019), 2));
                            TaxaCBLC = Math.Abs(Decimal.Round(Cash * Convert.ToDecimal(0.00006), 2));
                            TaxaCorretagem = Decimal.Round(Convert.ToDecimal(Math.Abs((Convert.ToDouble(Cash) * 0.005 + 25.21) * (1 - Convert.ToDouble(Percentual_Rebate)))), 2);
                            break;

                        default:
                            TaxaBovespa = Math.Abs(Decimal.Round(Cash * Convert.ToDecimal(0.00019), 2));
                            TaxaCBLC = Math.Abs(Decimal.Round(Cash * Convert.ToDecimal(0.00006), 2));
                            TaxaCorretagem = Decimal.Round(Convert.ToDecimal(Math.Abs((Convert.ToDouble(Cash) * 0.005 + 25.21) * (1 - Convert.ToDouble(Percentual_Rebate)))), 2);
                            break;
                        case 2:
                            TaxaBovespa = Math.Abs(Decimal.Round(Cash * Convert.ToDecimal(0.00019), 2));
                            TaxaCBLC = Math.Abs(Decimal.Round(Cash * Convert.ToDecimal(0.00006), 2));
                            TaxaCorretagem = Convert.ToDecimal(0.10) * Percentual_Rebate;
                            break;
                    }

                    data_Insert = DateTime.Now.ToString("yyyyMMdd");
                    SQLString = "exec sp_insert_Tb013_Trades " +

                        "@Id_Ordem_1 =" + IdOrder +
                        ",@Quantidade_2 =" + Qtt.ToString().Replace(",", ".") +
                        ",@Preco_3 =" + Price.ToString().Replace(",", ".") +
                        ",@Valor_Financeiro_4 =" + Cash.ToString().Replace(",", ".") + "" +
                        ",@Corretora_5 =" + IdBroker.ToString() +
                        ",@Operador_7 =" + IdUser +
                        ",@Data_Trade_8 ='" + data_Insert.ToString() + "'" +
                        ",@Status_Trade_12 =1";

                    resultado = curConn.ExecuteNonQuery(SQLString, 1);
                    if (resultado == 0)
                    {
                        MessageBox.Show("Error in Insert!");
                    }
                }

            }
            catch (Exception excep)
            {

            }
        }

    }
}
