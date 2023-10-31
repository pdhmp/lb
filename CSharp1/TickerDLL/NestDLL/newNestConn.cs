using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace TickerDLL
{
    public class newNestConn : IDisposable
    {
        private bool UseOldConn = false;

        public newNestConn(bool UseOldConn)
        {
            InitConn(UseOldConn);
        }
        public newNestConn()
        {
            InitConn(false);
        }

        private void InitConn(bool UseOldConn)
        {
                if (ConnManager.Instance.ConexaoDB == null)
                    ConnManager.Instance.OpenConnection(UseOldConn);
        }

        public int ExecuteNonQuery(string String_SQL)
        {
            return ExecuteNonQuery(String_SQL, 0);
        }

        public int ExecuteNonQuery(string String_SQL, int Save_Log)
        {
            int retorno;

            SqlCommand Cmd = new SqlCommand();

        curDBAccess:
            //Cmd.Connection = ConnManager.Instance.ConexaoDB;
            Cmd.Connection = (!UseOldConn ? ConnManager.Instance.ConexaoDB : NestSrv02Conn.Instance.ConexaoDB);
            Cmd.CommandText = String_SQL;
            Cmd.CommandType = CommandType.Text;
            Cmd.CommandTimeout = 0;

            try
            {

                retorno = Convert.ToInt32(Cmd.ExecuteNonQuery());

                if (Save_Log == 1)
                {
                    Audit_Log(String_SQL);
                }
            }
            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "Non-Query DLL");

                if (e.ToString().Contains("Cannot insert duplicate key row in object 'dbo.Tb725_Fowards'"))
                {
                    retorno = 93;
                }
                else
                {
                    retorno = 99;
                }

                if (e.Message.Contains("onnection") && e.Message.Contains("transport-level"))
                {
                    //ConnManager.Instance.ReOpenConnection();
                    if (!UseOldConn) { ConnManager.Instance.ReOpenConnection(); }
                    else { NestSrv02Conn.Instance.ReOpenConnection(); }
                    goto curDBAccess;
                }
            }

            Cmd.Dispose();
            return retorno;
        }

        public int ExecuteNonQuery(string String_SQL, SqlParameter Parameters, CommandType CommandType)
        {
            int retorno;

            SqlCommand command = new SqlCommand();

        curDBAccess:
            command.Connection = (!UseOldConn ? ConnManager.Instance.ConexaoDB : NestSrv02Conn.Instance.ConexaoDB);
            command.CommandText = String_SQL;
            command.CommandType = CommandType;
            command.CommandTimeout = 0;
            command.Parameters.Add(Parameters);

            try
            {
                retorno = Convert.ToInt32(command.ExecuteNonQuery());
            }
            catch (Exception e)
            {

                Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "Non-Query DLL");

                if (e.ToString().Contains("Cannot insert duplicate key row in object 'dbo.Tb725_Fowards'"))
                {
                    retorno = -1;
                }
                else
                {
                    retorno = -2;
                }

                if (e.Message.Contains("onnection") && e.Message.Contains("transport-level"))
                {
                    //ConnManager.Instance.ReOpenConnection();
                    if (!UseOldConn) { ConnManager.Instance.ReOpenConnection(); }
                    else { NestSrv02Conn.Instance.ReOpenConnection(); }
                    goto curDBAccess;
                }
            }

            command.Dispose();
            return retorno;
        }

        #region Returns

        public int Return_Int(string String_SQL)
        {
            DataTable curTable = new DataTable();
            int ReturnValue;

            curTable = Return_DataTable(String_SQL);

            if (curTable.Rows.Count > 0)
                if (int.TryParse(curTable.Rows[0][0].ToString(), out ReturnValue))
                {
                    return ReturnValue;
                }
                else
                {
                    return 0;
                }
            else
                return 0;
        }

        public double Return_Double(string String_SQL)
        {
            DataTable curTable = new DataTable();
            double ReturnValue;

            curTable = Return_DataTable(String_SQL);

            if (curTable.Rows.Count > 0)
                if (double.TryParse(curTable.Rows[0][0].ToString(), out ReturnValue))
                {
                    return ReturnValue;
                }
                else
                {
                    return double.NaN;
                }
            else
                return double.NaN;
        }

        public float Return_Float(string String_SQL)
        {
            DataTable curTable = new DataTable();
            float ReturnValue;

            curTable = Return_DataTable(String_SQL);

            if (curTable.Rows.Count > 0)
                if (float.TryParse(curTable.Rows[0][0].ToString(), out ReturnValue))
                {
                    return ReturnValue;
                }
                else
                {
                    return float.NaN;
                }
            else
                return float.NaN;
        }

        public DateTime Return_DateTime(string String_SQL)
        {
            DataTable curTable = new DataTable();
            DateTime ReturnValue;

            curTable = Return_DataTable(String_SQL);

            if (curTable.Rows.Count > 0)
                if (DateTime.TryParse(curTable.Rows[0][0].ToString(), out ReturnValue))
                {
                    return ReturnValue;
                }
                else
                {
                    return new DateTime(1850, 01, 01);
                }
            else
                return new DateTime(1850, 01, 01);
        }

        public string Return_String(string String_SQL)
        {
            return Execute_Query_String(String_SQL);
        }

        public string Execute_Query_String(string String_SQL)
        {
            DataTable curTable = new DataTable();

            curTable = Return_DataTable(String_SQL);

            if (curTable.Rows.Count > 0)
                return curTable.Rows[0][0].ToString();
            else
                return "";
        }

        public DataSet Return_DataSet(string String_SQL)
        {
            //Função para Selecionar registros, recebe sql e retorna um DataSet com multiplas linhas e Colunas.
            SqlCommand Cmd = new SqlCommand();
            SqlDataAdapter DA = new SqlDataAdapter();
            DataSet objDS = new DataSet();

            DA.SelectCommand = Cmd;

        curDBAccess:

            Cmd.CommandText = String_SQL;
            Cmd.Connection = (!UseOldConn ? ConnManager.Instance.ConexaoDB : NestSrv02Conn.Instance.ConexaoDB);
            Cmd.CommandType = CommandType.Text;

            try
            {
                DA.Fill(objDS);
            }
            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "DataSet DLL");
                if (e.Message.Contains("onnection") && e.Message.Contains("transport-level"))
                {
                    if (!UseOldConn) { ConnManager.Instance.ReOpenConnection(); }
                    else { NestSrv02Conn.Instance.ReOpenConnection(); }

                    goto curDBAccess;
                }
            }
            return objDS;
        }

        public SqlDataReader Return_DataReader(string String_SQL)
        {

            //Função para Selecionar registros, recebe sql e retorna um DataRed com multiplas linhas e Colunas.
            SqlDataReader DtReader = null;
            SqlCommand Cmd = new SqlCommand();

        curDBAccess:

            Cmd.CommandText = String_SQL;
            //Cmd.Connection = ConnManager.Instance.ConexaoDB;
            Cmd.Connection = (!UseOldConn ? ConnManager.Instance.ConexaoDB : NestSrv02Conn.Instance.ConexaoDB);
            Cmd.CommandType = CommandType.Text;

            try
            {
                DtReader = Cmd.ExecuteReader(CommandBehavior.Default);
            }

            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "SqlDataReader DLL");
                if (e.Message.Contains("onnection") && e.Message.Contains("transport-level"))
                {
                    //ConnManager.Instance.ReOpenConnection();
                    if (!UseOldConn) { ConnManager.Instance.ReOpenConnection(); }
                    else { NestSrv02Conn.Instance.ReOpenConnection(); }
                    goto curDBAccess;
                }
            }
            return DtReader;

            return null;
        }

        public SqlDataAdapter Return_DataAdapter(string String_SQL)
        {
            //Função para Selecionar registros, recebe sql e retorna um DataSet com multiplas linhas e Colunas.
            SqlCommand Cmd = new SqlCommand();
            SqlDataAdapter DA = new SqlDataAdapter();

        curDBAccess:

            Cmd.CommandText = String_SQL;
            //Cmd.Connection = ConnManager.Instance.ConexaoDB;
            Cmd.Connection = (!UseOldConn ? ConnManager.Instance.ConexaoDB : NestSrv02Conn.Instance.ConexaoDB);
            Cmd.CommandType = CommandType.Text;

            try
            {
                DA.SelectCommand = Cmd;
            }
            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "SqlDataAdapter DLL -- " + String_SQL);
                if (e.Message.Contains("onnection") && e.Message.Contains("transport-level"))
                {
                    //ConnManager.Instance.ReOpenConnection();
                    if (!UseOldConn) { ConnManager.Instance.ReOpenConnection(); }
                    else { NestSrv02Conn.Instance.ReOpenConnection(); }
                    goto curDBAccess;
                }
            }
            return DA;
        }

        public DataTable Return_DataTable(string String_SQL)
        {
            using (SqlCommand curCommand = new SqlCommand())
            {
                using (SqlDataAdapter curDataAdapter = new SqlDataAdapter())
                {
                    using (DataTable curDataTable = new DataTable())
                    {
                    curDBAccess:

                        curCommand.CommandText = String_SQL;
                        //curCommand.Connection = ConnManager.Instance.ConexaoDB;
                        curCommand.Connection = (!UseOldConn ? ConnManager.Instance.ConexaoDB : NestSrv02Conn.Instance.ConexaoDB);
                        curCommand.CommandType = CommandType.Text;
                        curCommand.CommandTimeout = 360;
                        try
                        {

                            if (curCommand.Connection.State != ConnectionState.Open)
                            {
                                curCommand.Connection.Open();
                            }

                            curDataAdapter.SelectCommand = curCommand;
                            curDataAdapter.Fill(curDataTable);
                            curDataAdapter.Dispose();
                        }
                        catch (Exception e)
                        {
                            if (e.ToString() == "Could not continue scan with NOLOCK due to data movement.")
                            {
                                goto curDBAccess;
                            }
                            Log.Error_Dump_DLL(e.ToString() + "\r\n" + String_SQL, "DataTable DLL -- " + String_SQL);
                            if (e.Message.Contains("onnection") && e.Message.Contains("transport-level"))
                            {
                                //ConnManager.Instance.ReOpenConnection();
                                if (!UseOldConn) { ConnManager.Instance.ReOpenConnection(); }
                                else { NestSrv02Conn.Instance.ReOpenConnection(); }
                                goto curDBAccess;
                            }
                        }
                        finally
                        {
                            curCommand.Dispose();
                        }
                        return curDataTable;
                    }
                }
            }
        }

        #endregion

        private int Audit_Log(string String_SQL_LOG)
        {
            int retorno;
            SqlCommand Cmd = new SqlCommand();
            string SQL_Command = "";

            SQL_Command = "INSERT INTO NESTLOG.dbo.Tb212_Log_StringSQL([Id_User],[String_SQL]) VALUES (" + NUserControl.Instance.User_Id + ",'" + String_SQL_LOG.Replace("'", "''") + "')";

            try
            {
                //Cmd.Connection = ConnManager.Instance.ConexaoDB;
                Cmd.Connection = (!UseOldConn ? ConnManager.Instance.ConexaoDB : NestSrv02Conn.Instance.ConexaoDB);
                Cmd.CommandText = SQL_Command;
                Cmd.CommandType = CommandType.Text;

                retorno = Convert.ToInt32(Cmd.ExecuteNonQuery());
            }
            catch (Exception e)
            {
                Log.Error_Dump_DLL(e.ToString(), "INSERT_LOG DLL");
                retorno = 0;
            }

            Cmd.Dispose();
            return retorno;
        }

        public void Dispose()
        {
        }
    }
}

