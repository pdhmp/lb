using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using SGN.CargaDados;
namespace SGN.Business
{
    class Business_Class
    {
        Valida Validacao = new Valida();
        CarregaDados Carga = new CarregaDados();

        public int Cancela_Ordem(double Id_Ordem)
        {
            return Cancela_Ordem(Id_Ordem, 0);
        }


        public int Cancela_Ordem(double Id_Ordem, int Messagebox_Visivle)
        {
            int resposta;
            if (Messagebox_Visivle != 0)
            {
                resposta = 6;
            }
            else
            {
                resposta = Convert.ToInt32(MessageBox.Show("Do you really want to cancel this order?", "Cancel Order", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
            }

            if (resposta == 6)
            {
                String SQLString = "SET DATEFORMAT DMY;exec proc_update_Tb012_Ordens_Status" +
                              " @Id_Ordem_1 = " + Id_Ordem + ", @Status_Ordem_2 = 4,@GTC_3=0";

                return Validacao.DB.Execute_Insert_Delete_Update(SQLString);
            }
            else
            {
                return 0;
            }
        }
 
        public int Inserir_Trade(double Id_Ordem, int Id_user)
        {
            string retorno;

            frmInsertTrade Trade = new frmInsertTrade();
            Trade.Id_Ordem = Id_Ordem;
            Trade.Id_Usuario = Id_user;
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
            Trade.Close();
            Trade.Dispose();


        }
        public int Inserir_Pendencia_Precos(int Id_Ativo)
        {
            int retorno;
            string StringSQL;

            StringSQL = " INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",1,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",3,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",4,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",5,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",6,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",7,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",8,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",9,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",10,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",11,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",13,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",15,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",16,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",17,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",18,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",19,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",20,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",21,'20000101',1)" +
                        "; INSERT INTO Tb207_Pending (Id_Ticker,Price_type,Ini_Date,Source) values (" + Id_Ativo + ",18,'20000101',1)";
           retorno =  Carga.DB.Execute_Insert_Delete_Update(StringSQL);
           return retorno;
        }


        
        public int Editar_Ordem(double Id_Ordem)
        {
            frmInsertOrdem ordem = new frmInsertOrdem();
            ordem.Id_Ordem = Id_Ordem;
            ordem.ShowDialog();
            return 0;
        }

        public int Insert_Order(double Id_Order, Boolean Edit_Order, int Portfolio, int Ticker, decimal Qtd, decimal Preco, string expiration, int brooker, int Strategy, int Sub_Strategy, int Order_Type, int Id_Usuario)
        {
            try
            {
                Boolean resumefunc;
                decimal VFinanc;
                resumefunc = false;
                string data_insert;
                string data_validade;
                string SqlString;
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
                    SqlString = "Select Lote_Padrao from Tb001_Ativos Where Id_Ativo =" + Ticker.ToString();
                    Lote_Padrao = Convert.ToDecimal(Carga.DB.Execute_Query_String(SqlString));

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
                    if (Edit_Order == false)
                    {
                        SqlString = "SET DATEFORMAT DMY;exec sp_insert_Tb012_Ordens " +
                                    "@Id_Ativo_1  =" + Ticker.ToString() +
                                    ", @Quantidade_2 =" + Qtd.ToString().Replace(",", ".") +
                                    ", @Preco_3 =" + Preco.ToString().Replace(",", ".") +
                                    ", @Valor_Financeiro_4 =" + VFinanc.ToString().Replace(",", ".") +
                                    ", @Estrategia_5 =" + Strategy.ToString() +
                                        ", @Sub_Estrategia_6 =" + Sub_Strategy.ToString() +
                                        ", @Tipo_Mercado_7 =" + Order_Type.ToString() +
                                        ", @Operador_8 =" + Id_Usuario.ToString() +
                                        ", @Data_Abert_Ordem_9 ='" + data_insert + "'" +
                                        ", @Status_Ordem_11 = 1" +
                                        ", @Id_Carteira_13 =" + Portfolio.ToString() +
                                        ",  @Id_Corretora_14 =" + brooker.ToString() +
                                        ", @Data_Valid_Ordem_15 =" + data_validade + "";
                        //MessageBox.Show(SqlString);
                        resultado = Carga.DB.Execute_Insert_Delete_Update(SqlString);
                        if (resultado == 0)
                        {
                            MessageBox.Show("Error in Insert");
                        }
                    }
                    else
                    {
                        decimal Qtd_execut;
                        string Qtd_string;

                        SqlString = "Select sum(quantidade) as QTD from Tb013_Trades where id_ordem =" + Id_Order;
                        Qtd_string = Carga.DB.Execute_Query_String(SqlString);

                        SqlString = "Select operador  from Tb012_Ordens where id_ordem =" + Id_Order;
                        string OP_string = Carga.DB.Execute_Query_String(SqlString);

                        Qtd_execut = Convert.ToDecimal(Qtd_string);

                        if (Qtd >= Qtd_execut)
                        {
                            Cancela_Ordem(Id_Order,1);
                            Insert_Order(0, false, Portfolio, Ticker, Qtd - Qtd_execut, Preco, expiration, brooker, Strategy, Sub_Strategy, Order_Type,Convert.ToInt32(OP_string));
                            resultado = Carga.DB.Execute_Insert_Delete_Update(SqlString);
                        }
                        else
                        {
                            resultado = 999;
                        }
                        /*
                        SqlString = "SET DATEFORMAT DMY;exec sp_update_Tb012_Ordens " +
                                    " @Id_Ordem_1 =" + Id_Order +
                                    ", @Quantidade_7 =" + Qtd.ToString().Replace(",", ".") +
                                    ", @Preco_6 =" + Preco.ToString().Replace(",", ".") +
                                    ", @Valor_Financeiro_8 =" + VFinanc.ToString().Replace(",", ".") +
                                    ", @Estrategia_3 =" + Strategy.ToString() +
                                    ", @Sub_Estrategia_4 =" + Sub_Strategy.ToString() +
                                    ", @Tipo_Mercado_2 =" + Order_Type.ToString() +
                                    ",  @Id_Corretora_9 =" + brooker.ToString() +
                                    ", @Data_Valid_Ordem_5 =" + data_validade; 

                        // MessageBox.Show(SqlString);
                        resultado = Carga.DB.Execute_Insert_Delete_Update(SqlString);
                        if (resultado == 0)
                        {
                            MessageBox.Show("Erro in Edition");
                        }
                        else
                        {
                        }

                         */

                    }
                    return resultado;
                }
                else
                {
                    return 999;
                }
                ///////////////////////////////
            }
            catch
            {
                return 0;
            }
        }

        public int Insere_PL(int Id_Portfolio, DateTime Data_PL, decimal Valor_PL)
        {
            try
            {
             string StringSQL;
            int resultado;

            StringSQL = "SET DATEFORMAT DMY;exec proc_insert_Tb025_Valor_PL " + Id_Portfolio + ",'" + Data_PL + "'," + Valor_PL.ToString().Replace(",", ".");
            resultado = Carga.DB.Execute_Insert_Delete_Update(StringSQL);
    
                return resultado;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Source.ToString());
                return 0;
            }
        }

        public int Insere_Vol(int Id_Ativo, string Valor_Vol)
        {
            try
            {
                string StringSQL;
                int resultado;
                StringSQL = "Insert into Tb027_Volatilidade (Id_Ativo,Volatilidade,[Data_Hora_Reg]) values (" + Id_Ativo + "," + Valor_Vol + ",'" + DateTime.Now.ToString("yyyyMMdd") + "')";
                resultado = Carga.DB.Execute_Insert_Delete_Update(StringSQL);
                    return resultado;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Source.ToString());
                return 0;
            }

        }
        public int Insere_Preco(int Id_Ativo, string preco, string tipo_preco, string data, int Id_User)
        {
            string tabela;
            decimal teste;
            int teste2;
            int resultado;
            
            try
            {
                if (decimal.TryParse(preco, out teste) && int.TryParse(tipo_preco, out teste2))
                {

                    string StringSQL;
                    
                    tabela = Validacao.Retorna_Tabela_Preco_Trade(Id_Ativo, 0);

                    StringSQL = "Select Valor from " + tabela + " Where Id_Ativo=" + Id_Ativo + " and  Tipo_Preco=" + tipo_preco +
                        " and Data_Hora_Reg='" + data + "'";

                    string old_value = Carga.DB.Execute_Query_String(StringSQL);

                    if (old_value == "")
                    {
                        old_value = "0";
                    }
                    else
                    {
                        old_value = old_value.Replace(",", ".");
                    }

                    StringSQL = "INSERT INTO " + tabela + " ([Id_Ativo], [Valor], [Data_Hora_Reg], [Tipo_Preco],Source)" +
                    "VALUES(" + Id_Ativo + "," + preco.Replace(",", ".") + ",'" + data + "'," + tipo_preco + ",4); " +
                    "INSERT INTO Tb210_Log_Precos(Tabela,Id_Usuario,Id_Ativo,Valor,Tipo_preco,Data) values" +
                    "('" + tabela + "'," + Id_User + "," + Id_Ativo + "," + preco.Replace(",", ".") + ","
                    + tipo_preco + ",'" + data + "')";


                    resultado = Carga.DB.Execute_Insert_Delete_Update(StringSQL);

                }
                else
                {
                    resultado = 0;
                }

                return resultado;
            }
            catch
            {

                return 0; 
            }

        }

        public int Edit_Price_Trade(int Id_Trade, string New_Price)
        {
            string StringSQL;
            decimal teste;

            if (decimal.TryParse(New_Price, out teste) && Id_Trade != 0)
            {
                StringSQL = "update Tb013_Trades set Preco = " + New_Price.Replace(",", ".") + "Where Id_Trade = " + Id_Trade;
                int resultado = Carga.DB.Execute_Insert_Delete_Update(StringSQL);
                return resultado;
            }
            else
            {
                return 0;
            }
        }

        public int Busca_Dados_Historicos(int Id_Ativo, string IniDate)
        {
            try
            {
                string StringSQL;

                StringSQL = "insert into Tb207_Pending  SELECT A.Id_Ativo AS Id_Ticker, Id_Tipo_Preco AS Price_Type," +
                "'" + IniDate + "' AS Ini_Date, 1 AS Source, 1 AS Status from VW001_Ativos A " +
                " Inner join Tb119_Convert_Simbolos B on A.Id_Ativo = B.Id_Ativo cross join Tb116_Tipo_Preco C " +
                " inner join Tb000_Instituicoes D  on D.Id_Instituicao = A.Id_Instituicao " +
                " Where Not BBG_Field_Name Is Null and A.Id_Ativo = " + Id_Ativo;

                if (Carga.DB.Execute_Insert_Delete_Update(StringSQL) != 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }

        }
       
    }
}
