using System;  
using System.Data;
using System.Data.SqlClient;

namespace NestSim
{
    public class NestConn : IDisposable
    {
        SqlConnection Conn;

        public NestConn()
        {
        }

        void openConn()
        {
            this.Conn = new SqlConnection("Server=NESTSRV02;Uid=NestUpdate;Pwd=ParkHill43;");
            this.Conn.Open();
        }

        public void ExecuteNonQuery(string CommandText)
        {
                using (SqlCommand cmd = new SqlCommand(CommandText, Conn))
                {
                    cmd.ExecuteNonQuery();
                }            


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
