using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace TwgSendPTF
{
	/// <summary>
	/// Summary description for ConexaoDB.
	/// </summary>
	public class ConexaoDB
	{
		#region SqlConnection fnOpenConnection()
		public SqlConnection fnOpenConnection()
		{
			SqlConnection cnn = null;

			try
			{   
				cnn = new SqlConnection("User Id=user_intercapital;PWD=icap;Data Source=SRV-APP01;Initial Catalog=Intercapital_desenvolvimento");
			}
			catch (SqlException eSql)   
			{
				throw eSql;
			}
			catch (Exception e)   
			{
				throw e;
			}

			return cnn;
		}
		#endregion
	
	}
}
