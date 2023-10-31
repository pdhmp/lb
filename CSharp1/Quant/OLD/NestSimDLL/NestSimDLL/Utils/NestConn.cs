using System;  
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NestQuant.Common
{
    public class NestConn : IDisposable
    {
        SqlConnection Conn;

        public NestConn()
        {            
        }

        public void openConn()
        {
            this.Conn = new SqlConnection("Server=NESTSRV02;Uid=NestUpdate;Pwd=ParkHill43;");
            this.Conn.Open();
        }
        
        public void ExecuteNonQuery(string CommandText)
        {

            try
            {
                using (SqlCommand cmd = new SqlCommand(CommandText, Conn))
                {
                    cmd.ExecuteNonQuery();
                }            
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public SqlDataReader ExecuteReader(string CommandText)
        {
            SqlDataReader result = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand(CommandText, Conn))
                {
                    result = cmd.ExecuteReader();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return result;
        }

        public DataTable ExecuteDataTable(string CommandText)
        {
            openConn();

            DataTable dTable = new DataTable();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                using (SqlCommand cmd = new SqlCommand(CommandText, Conn))
                {
                    cmd.CommandText = CommandText;
                    cmd.CommandType = CommandType.Text;
                    try
                    {
                        da.SelectCommand = cmd;
                    }
                    catch (Exception e)
                    {
                        //throw err;
                    }
                }
                da.Fill(dTable);
                this.Conn.Dispose();
                return dTable;
            }
        }

        public string Execute_Query_String(string String_SQL)
        {
            string retorno;

            openConn();

            using (SqlDataAdapter da = new SqlDataAdapter())
            {
                using (SqlCommand cmd = new SqlCommand(String_SQL, Conn))
                {

                    cmd.CommandText = String_SQL;
                    cmd.CommandType = CommandType.Text;

                    try
                    {
                        retorno = Convert.ToString(cmd.ExecuteScalar());
                        return retorno;
                    }
                    catch (Exception e)
                    {
                        //throw err;
                    }
                }
            }
            this.Conn.Dispose();
            return "";
        }

        #region IDisposable Members

        public void Dispose()
        {
            this.Conn.Close();
        }

        #endregion 
    }
}
