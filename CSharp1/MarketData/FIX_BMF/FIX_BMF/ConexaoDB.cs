using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace FIX_BMF
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
				cnn = new SqlConnection("User Id=;PWD=;Data Source=;Initial Catalog=");
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
