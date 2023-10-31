using System;
using System.Data;
using System.Data.SqlClient;
using QuickFix;

namespace TwgSendPTF
{
	/// <summary>
	/// Summary description for clsProcessamento.
	/// </summary>
	public class clsProcessamento
	{
		#region static Int64 fnVerificaUltimaNovaOrdem(string sTargetSubId)
		public static Int64 fnVerificaUltimaNovaOrdem(string sTargetSubId)
		{
			// Busca um nova conexão
			SqlConnection cnn = null;
			Int64 iAuxi = 0;

			// Busca uma nova conexão do banco
			ConexaoDB clsDB = new ConexaoDB();
			cnn = clsDB.fnOpenConnection();
			
			try
			{
				SqlCommand cmd = new SqlCommand("sp_FIX_Sel_UltimaOrdemPTF", cnn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@TargetSubId", SqlDbType.VarChar).Value = sTargetSubId;
				
				cnn.Open();
				iAuxi = Convert.ToInt64(cmd.ExecuteScalar() );
			}
			catch(Exception e)
			{	
				Console.WriteLine(       e.ToString() + " - " + e.Message.ToString() );
				clsArquivo.fnGravaLogTXT(e.ToString() + " - " + e.Message.ToString() );
			}
			finally
			{
				cnn.Close();
			}
			return iAuxi;
		}
		#endregion


		#region static Int64 fnVerificaUltimoCancelamento(string sTargetSubId)
		public static Int64 fnVerificaUltimoCancelamento(string sTargetSubId)
		{
			// Busca um nova conexão
			SqlConnection cnn = null;
			Int64 iAuxi = 0;

			// Busca uma nova conexão do banco
			ConexaoDB clsDB = new ConexaoDB();
			cnn = clsDB.fnOpenConnection();
			
			try
			{
				SqlCommand cmd = new SqlCommand("sp_FIX_Sel_UltimaOrdemCanceladaPTF", cnn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@TargetSubId", SqlDbType.VarChar).Value = sTargetSubId;
				
				cnn.Open();
				iAuxi = Convert.ToInt64(cmd.ExecuteScalar() );
			}
			catch(Exception e)
			{	
				       Console.WriteLine(e.ToString() + " - " + e.Message.ToString() );
				clsArquivo.fnGravaLogTXT(e.ToString() + " - " + e.Message.ToString() );
			}
			finally
			{
				cnn.Close();
			}
			return iAuxi;
		}
		#endregion


		#region static QuickFix.Message fnFormataNewOrdemSigle_35D(Int64 iOrdem)
		public static QuickFix.Message fnFormataNewOrdemSigle_35D(Int64 iOrdem)
		{
			QuickFix42.NewOrderSingle message = new QuickFix42.NewOrderSingle();
			SqlDataReader dr  = null;
			SqlConnection cnn = null;

			// Busca uma nova conexão do banco
			ConexaoDB clsDB = new ConexaoDB();
			cnn = clsDB.fnOpenConnection();

			SqlCommand cmd = new SqlCommand("sp_FIX_Sel_NewOrderSingle_35D_PTF", cnn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@twgOrder", SqlDbType.BigInt).Value = iOrdem;
			cmd.Connection = cnn;
			
			try
			{
				cnn.Open();
				dr = cmd.ExecuteReader();
				dr.Read();

				message.setField(1,  dr.GetString(2));									// Account
				message.setField(11, dr.GetString(1));									// CLORDID
				message.setField(15, dr.GetString(12).Trim());							// Currency
				message.setField(21, dr.GetInt32(10).ToString());						// HandlInst
				message.setField(22, dr.GetInt32(14).ToString());						// IDSource
				message.setField(38, dr.GetInt32(6).ToString());						// OrderQty
				message.setField(40, dr.GetInt32(11).ToString());						// OrdType
				message.setField(44, dr.GetDecimal(7).ToString().Replace(",", ".") ) ;	// Price
				message.setField(54, dr.GetInt32(4).ToString());						// Side 
				message.setField(55, dr.GetString(3).Trim());							// Symbol
				message.setField(59, dr.GetInt32(9).ToString());						// TimeInForce
				message.setField(207, dr.GetString(13).Trim());							// SecurityExchange
				message.setField(60, dr.GetDateTime(5).ToString("yyyyMMdd-HH:mm:ss"));  // TransactTime
			}
			catch(Exception e)
			{
				Console.WriteLine(       e.ToString() + " - " + e.Message.ToString() );
				clsArquivo.fnGravaLogTXT(e.ToString() + " - " + e.Message.ToString() );
			}
			finally
			{
				dr.Close();
				cnn.Close();
				cnn.Dispose();
			}

			return message;
		}
		#endregion


		#region static QuickFix.Message fnFormataNewOrdemSigle_35F(Int64 iOrdem)
		public static QuickFix.Message fnFormataNewOrdemSigle_35F(Int64 iOrdem)
		{
			QuickFix42.OrderCancelRequest message = new QuickFix42.OrderCancelRequest();
			SqlDataReader dr  = null;
			SqlConnection cnn = null;

			// Busca uma nova conexão do banco
			ConexaoDB clsDB = new ConexaoDB();
			cnn = clsDB.fnOpenConnection();

			SqlCommand cmd = new SqlCommand("sp_FIX_OrderCancelRequest_35F_PTF", cnn);
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.Parameters.Add("@twgOrder", SqlDbType.BigInt).Value = iOrdem;
			cmd.Connection = cnn;

			try
			{
				cnn.Open();
				dr = cmd.ExecuteReader();
				dr.Read();

				message.setField(11, dr.GetString(1));									// CLORDID
				message.setField(41, dr.GetString(2));									// OrigClOrdID
				message.setField(54, dr.GetInt32(4).ToString());						// Side 
				message.setField(55, dr.GetString(3).Trim());							// Symbol
				message.setField(60, dr.GetDateTime(5).ToString("yyyyMMdd-HH:mm:ss"));  // TransactTime
			}
			catch(Exception e)
			{
				       Console.WriteLine(e.ToString() + " - " + e.Message.ToString() );
				clsArquivo.fnGravaLogTXT(e.ToString());
			}
			finally
			{
				dr.Close();
				cnn.Close();
				cnn.Dispose();
			}

			return message;
		}
		#endregion


		#region void fnGravaPrints(QuickFix42.ExecutionReport msg, SessionID sessionID)
		public void fnGravaPrints(QuickFix42.ExecutionReport msg, SessionID sessionID)
		{
			//SqlDataReader dr  = null;
			SqlConnection cnn = null;

			// Busca uma nova conexão do banco
			ConexaoDB clsDB = new ConexaoDB();
			cnn = clsDB.fnOpenConnection();

			try
			{
				SqlCommand cmd = new SqlCommand("sp_FIX_InsPrintPTF", cnn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Connection = cnn;
				
				cmd.Parameters.Add("@AvgPx",        msg.getField(6));		// AvgPx	- <6>
				cmd.Parameters.Add("@ClOrdID",		msg.getField(11));		// ClOrdID  - <11>
				cmd.Parameters.Add("@CumQty",		msg.getField(14));		// CumQty	- <14>
				cmd.Parameters.Add("@ExecID",		msg.getField(17));		// EXECID	- <17>
				cmd.Parameters.Add("@ExecTransType",  msg.getField(20));	// ExecTransType - <20>
				cmd.Parameters.Add("@LastPx",		msg.getField(31));		// LastPX	- <31>
				cmd.Parameters.Add("@LastShares",	msg.getField(32));		// LastSH	- <32>
				cmd.Parameters.Add("@OrderID",		msg.getField(37));		// OrderID  - <37>
				cmd.Parameters.Add("@OrderQty",		msg.getField(38));		// OrderQty - <37>
				cmd.Parameters.Add("@OrdStatus",	msg.getField(39));		// OrdStatus- <39>
				cmd.Parameters.Add("@Side",			msg.getField(54));		// Side		- <54>
				cmd.Parameters.Add("@Symbol",		msg.getField(55));		// Symbol	- <55>
				if (msg.isSetText()) cmd.Parameters.Add("@Texto", msg.getField(58));					// Texto	- <58>
				cmd.Parameters.Add("@TransactTime", DateTime.UtcNow.ToString("M/d/yyyy hh:mm:ss tt") ); // TransactTime - <60>
				cmd.Parameters.Add("@ExecType",		msg.getField(150));		// ExecType	- <150>
				cmd.Parameters.Add("@LeavesQty",	msg.getField(151));		// lvs Qty	- <151>
				
				cnn.Open();
				cmd.ExecuteNonQuery();
			}
			catch(Exception e)
			{
				Console.WriteLine(e.ToString() + " - " + e.Message.ToString() );
				clsArquivo.fnGravaLogTXT(e.ToString());
			}
			finally
			{
				cnn.Close();
			}
		}
		#endregion 


		#region void fnAtualizaFlagProcessado(string sOrdID, int iFlag )
		public static void fnAtualizaFlagProcessado(string sOrdID, int iFlag )
		{
			// Busca um nova conexão
			SqlConnection cnn = null;
			
			// Busca uma nova conexão do banco
			ConexaoDB clsDB = new ConexaoDB();
			cnn = clsDB.fnOpenConnection();
			
			try
			{
				SqlCommand cmd = new SqlCommand("sp_upd_MESOrdemProcessamento", cnn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@ORD_Id", SqlDbType.Float).Value = sOrdID;
				cmd.Parameters.Add("@Flag"  , SqlDbType.Int).Value   = iFlag;
				
				cnn.Open();
				cmd.ExecuteNonQuery();
			}
			catch(Exception e)
			{	
				Console.WriteLine(e.ToString() + " - " + e.Message.ToString() );
				clsArquivo.fnGravaLogTXT(e.ToString());
			}
			finally
			{
				cnn.Close();
			}
		}
		#endregion

		
		#region string ObterNovoId
		public static string ObterNovoId(string sTabelaBusca)
		{
			string sAuxi = "";
			SqlConnection cnn = null;

			// Busca uma nova conexão do banco
			ConexaoDB clsDB = new ConexaoDB();
			cnn = clsDB.fnOpenConnection();

			try
			{
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = cnn;
				cmd.CommandText = "execute sp_Sequencia " + sTabelaBusca;
				cnn.Open();
				sAuxi = cmd.ExecuteScalar().ToString().Trim();
			}
			catch (Exception e)   
			{
				Console.WriteLine(e.ToString() + " - " + e.Message.ToString() );
				clsArquivo.fnGravaLogTXT(e.ToString());
			}
			finally
			{
				cnn.Close();
			}
			return sAuxi;
		}
		#endregion
	}
}
