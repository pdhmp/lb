using System;  
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace NestSim.Common
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
                Log.AddLogEntry("Unable to execute SQL command. \r\n" + CommandText + "\r\n" + ex.ToString());
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
                Log.AddLogEntry("Unable to execute SQL command. \r\n" + ex.ToString());
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



        #region IDisposable Members

        public void Dispose()
        {
            this.Conn.Close();
        }

        #endregion 
    }
}
