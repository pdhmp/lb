using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace LiveDLL
{
    public class old_Conn : IDisposable
    {
        String StringConexao = LiveDLL.Properties.Settings.Default.connString;

        public int ExecuteNonQuery(string String_SQL)
        {
            return ExecuteNonQuery(String_SQL, 0);
        }

        public int ExecuteNonQuery(string String_SQL, int Save_Log)
        {
            int retorno;

            SqlConnection ConexaoDB = new SqlConnection(StringConexao);
            SqlCommand Cmd = new SqlCommand();

            Cmd.Connection = ConexaoDB;
            Cmd.CommandText = String_SQL;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandTimeout = 4800;

            try
            {
                ConexaoDB.Open();
                retorno = Convert.ToInt32(Cmd.ExecuteNonQuery());
                
                if (Save_Log == 1)
                {
                    Audit_Log(String_SQL);
                }
            }
            catch (Exception e)
            {
                retorno = 99;
            }
            ConexaoDB.Close();
            ConexaoDB.Dispose();
            Cmd.Dispose();
            return retorno;
        }

        public string Execute_Query_String(string String_SQL)
        {
            //Função para Selecionar registros, recebe sql e retorna somente 1 coluna, convertendo p/ string.
            string retorno;
            SqlConnection ConexaoDB = new SqlConnection(StringConexao);
            SqlCommand Cmd1 = new SqlCommand();

            Cmd1.Connection = ConexaoDB;
            Cmd1.CommandTimeout = 4800;
            Cmd1.CommandText = String_SQL;
            Cmd1.CommandType = CommandType.Text;

            try
            {
                ConexaoDB.Open();
                retorno = Convert.ToString(Cmd1.ExecuteScalar());

            }
            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "Query_String DLL");
                retorno = e.ToString();
            }
            ConexaoDB.Close();
            ConexaoDB.Dispose();
            Cmd1.Dispose();
            return retorno;
        }

        public DataSet Return_DataSet(string String_SQL)
        {
            //Função para Selecionar registros, recebe sql e retorna um DataSet com multiplas linhas e Colunas.
            SqlConnection ConexaoDB = new SqlConnection(StringConexao);
            SqlCommand Cmd = new SqlCommand();
            SqlDataAdapter DA = new SqlDataAdapter();
            DataSet objDS = new DataSet();
            Cmd.CommandText = String_SQL;
            Cmd.Connection = ConexaoDB;
            Cmd.CommandType = CommandType.Text;

            DA.SelectCommand = Cmd;
            try
            {
                DA.Fill(objDS);
            }
            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "DataSet DLL");
            }
            return objDS;
        }

        public SqlDataReader Return_DataReader(string String_SQL)
        {
            //Função para Selecionar registros, recebe sql e retorna um DataRed com multiplas linhas e Colunas.
            SqlConnection ConexaoDB = new SqlConnection(StringConexao);
            SqlDataReader DtReader = null;
            SqlCommand Cmd = new SqlCommand();

            Cmd.CommandText = String_SQL;
            Cmd.Connection = ConexaoDB;
            Cmd.CommandType = CommandType.Text;

            try
            {
                ConexaoDB.Open();
                DtReader = Cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }

            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "SqlDataReader DLL");
            }
            return DtReader;

        }

        public SqlDataAdapter Return_DataAdapter(string String_SQL)
        {
            //Função para Selecionar registros, recebe sql e retorna um DataSet com multiplas linhas e Colunas.
            SqlConnection ConexaoDB = new SqlConnection(StringConexao);
            SqlCommand Cmd = new SqlCommand();
            SqlDataAdapter DA = new SqlDataAdapter();

            Cmd.CommandText = String_SQL;
            Cmd.Connection = ConexaoDB;
            Cmd.CommandType = CommandType.Text;
            try
            {
                DA.SelectCommand = Cmd;
            }
            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "SqlDataAdapter DLL");

                //throw err;
            }
            return DA;
        }

        public DataTable Return_DataTable(string String_SQL)
        {

            DataTable tablep = new DataTable();

            SqlConnection ConexaoDB = new SqlConnection(StringConexao);
            SqlCommand curCommand = new SqlCommand();

            curCommand.CommandText = String_SQL;
            curCommand.Connection = ConexaoDB;
            curCommand.CommandType = CommandType.Text;
            curCommand.CommandTimeout = 4800;

            try
            {
                ConexaoDB.Open();
                SqlDataReader curDataReader = curCommand.ExecuteReader();
                tablep.Load(curDataReader);
                curDataReader.Dispose();
            }
            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "DataTable DLL");
            }
            finally
            {
                curCommand.Dispose();
            }


            return tablep;


        }

        private int Audit_Log(string String_SQL_LOG)
        {
            int retorno;
            SqlConnection ConexaoDB = new SqlConnection(StringConexao);
            SqlCommand Cmd = new SqlCommand();
            string SQL_Command = "";

            SQL_Command = "INSERT INTO NESTSRV06.NESTLOG.dbo.Tb212_Log_StringSQL([Id_User],[String_SQL]) VALUES (" + NUserControl.Instance.User_Id + ",'" + String_SQL_LOG.Replace("'", "''") + "')";

            Cmd.Connection = ConexaoDB;
            Cmd.CommandText = SQL_Command;
            Cmd.CommandType = CommandType.Text;

            try
            {
                ConexaoDB.Open();
                retorno = Convert.ToInt32(Cmd.ExecuteNonQuery());
            }
            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString(), "INSERT_LOG DLL");
                retorno = 0;
            }

            ConexaoDB.Close();
            ConexaoDB.Dispose();
            Cmd.Dispose();
            return retorno;
        }

        public void Dispose()
        { }
    }
}

