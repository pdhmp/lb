using System;
using System.Data;
using System.Data.SqlClient;
using NestDLL;
using QuickFix;

namespace FIX_BMF
{
	public class clsProcessamento
	{
        String StringConexao = " Data Source=NestSrv02;Initial Catalog=NESTDB;User Id=NestUpdate;Password=ParkHill43;";
        public SqlConnection ConexaoDB = new SqlConnection();

        void openConn()
        {
            try
            {
                
                ConexaoDB.ConnectionString = StringConexao;
                ConexaoDB.Open();
            }
            
            catch
            {
                ConexaoDB.Dispose();
                ConexaoDB = new SqlConnection();
            }

        }
       public void Log_Checkin()
       {
           //openConn();
            string SQLExpression = "UPDATE [NESTLOG].dbo.Tb900_CheckIn_Log SET CheckInTime=getdate() WHERE Program_Id=82";
            //int retorno = Execute_Insert_Delete_Update(SQLExpression);
        }

        public int Execute_Insert_Delete_Update(string String_SQL)
        {
            //Função para inserir, Apagar e Update em registros, recebe sql e retorna qtos registros foram afetados
            int retorno;
            SqlCommand Cmd = new SqlCommand();
            if (ConexaoDB.State == ConnectionState.Closed)
            {
                ConexaoDB.Open();
            }
            Cmd.Connection = ConexaoDB;
            Cmd.CommandText = String_SQL;
            Cmd.CommandType = CommandType.Text;

            try
            {
                retorno = Convert.ToInt32(Cmd.ExecuteNonQuery());
            }
            catch (Exception e)
            {
                clsArquivo.fnGravaLogTXT(e.ToString());
                retorno = 666;
            }

            ConexaoDB.Close();
            ConexaoDB.Dispose();
            Cmd.Dispose();

            return retorno;
        }

		#region void fnGravaPrints(QuickFix44.ExecutionReport msg, SessionID sessionID)
		public void fnGravaPrints(QuickFix44.ExecutionReport msg, SessionID sessionID)
		{
            openConn();

            try
			{
                string StringSQL;

                string Broker_Id ="";
                string Account ;
                string AvgPx ;
                string ClOrdID ;
                string CumQty ;
                string ExecID ;
                string ExecTransType ;
                string LastPx ;
                string LastShares ;
                string OrderID ;
                string OrderQty ;
                string OrdStatus ;
                string Side ;
                string Symbol ;
                string Texto ;
                string OrigClOrdID ;
                string TransactTime ;
                string SendingTime ;
                string ExecType ;
                string LeavesQty ;
                string TSubID;

                if (msg.isSetField(1)) {Account = msg.getField(1);} else {Account = "0";}
                if (msg.isSetField(6)) {AvgPx = msg.getField(6);} else {AvgPx = "0";}
                if (msg.isSetField(11)) {ClOrdID = msg.getField(11);} else {ClOrdID = "0";}
                if (msg.isSetField(14)) {CumQty = msg.getField(14);} else {CumQty = "0";}
                if (msg.isSetField(17)) {ExecID = msg.getField(17);} else {ExecID = "0";}
                if (msg.isSetField(20)) {ExecTransType = msg.getField(20);} else {ExecTransType = "0";}
                if (msg.isSetField(31)) {LastPx = msg.getField(31);} else {LastPx = "0";}
                if (msg.isSetField(32)) {LastShares = msg.getField(32);} else {LastShares = "0";}
                if (msg.isSetField(37)) {OrderID = msg.getField(37);} else {OrderID = "0";}
                if (msg.isSetField(38)) {OrderQty = msg.getField(38);} else {OrderQty = "0";}
                if (msg.isSetField(39)) {OrdStatus = msg.getField(39);} else {OrdStatus = "0";}
                if (msg.isSetField(54)) {Side = msg.getField(54);} else {Side = "0";}
                if (msg.isSetField(55)) {Symbol = msg.getField(55);} else {Symbol = "0";}
                if (msg.isSetField(57)) { TSubID = msg.getField(57); } else { TSubID = ""; }
                if (msg.isSetField(58)) {Texto = msg.getField(58);} else {Texto = "";}
                if (msg.isSetField(41)) {OrigClOrdID = msg.getField(41);} else {OrigClOrdID = "0";}
                if (msg.isSetField(60)) {TransactTime = msg.getField(60);} else {TransactTime = "";}
                if (msg.isSetField(52)) {SendingTime = msg.getField(52); } else { SendingTime = "01/01/1900 00:00:01"; }
                if (msg.isSetField(150)) {ExecType = msg.getField(150); } else { ExecType = ""; }
                if (msg.isSetField(151)) {LeavesQty = msg.getField(151);} else {LeavesQty = "";}

                if (msg.ToString().IndexOf("57=") > 0)
                {
                    int initialPos = msg.ToString().IndexOf("57=") + 3;
                    int endPos = msg.ToString().IndexOf("", initialPos);
                    TSubID = msg.ToString().Substring(initialPos, endPos - initialPos);
                }

                switch(ClOrdID.Substring(0, 1))
                {
                    case "A": Broker_Id = "1"; break;
                    case "B": Broker_Id = "1"; break;
                    case "S": Broker_Id = "8"; break;
                    case "T": Broker_Id = "16"; break;
                    case "L": Broker_Id = "33"; break;
                    case "F": Broker_Id = "33"; break;
                    case "P": Broker_Id = "32"; break;
                    case "R": Broker_Id = "16"; break;
                   case "K": Broker_Id = "36"; break; 
                    default: Broker_Id = "0"; break;
                }

                StringSQL = " EXEC [NESTDB].[dbo].[proc_FIX_InsExecReport] " +
                 "   @Broker_Id = " + Broker_Id +
                 "  ,@Account = '" + Account  + "'" +
                 "  ,@AvgPx = " +AvgPx +
                 "  ,@ClOrdID = '" + ClOrdID + "'" +
                 "  ,@CumQty = " + CumQty +
                 "  ,@ExecID = '" + ExecID + "'" +
                 "  ,@ExecTransType = '" + ExecTransType + "'" +
                 "  ,@LastPx = " + LastPx +
                 "  ,@LastShares = " + LastShares +
                 "  ,@OrderID = '" + OrderID + "'" +
                 "  ,@OrderQty = " + OrderQty +
                 "  ,@OrdStatus = '" + OrdStatus + "'" +
                 "  ,@Side = '" + Side + "'" +
                 "  ,@Symbol = '" + Symbol + "'" +
                 "  ,@Texto = '" + Texto + "'" +
                 "  ,@OrigClOrdID = '" + OrigClOrdID + "'" +
                 "  ,@TransactTime = '" + TransactTime.Replace('-', ' ') + "'" +
                 "  ,@SendingTime = '" + SendingTime + "'" +
                 "  ,@ExecType = '" + ExecType + "'" +
                 "  ,@LeavesQty =" + LeavesQty +
                 "  ,@TargetSubID ='" + TSubID + "'" +
                 "  ,@Id_Login_Program=31" +
                 "  ,@Manual_Feed=0 ;";

                int retorno = Execute_Insert_Delete_Update(StringSQL);
                if (retorno == 666)
                {
                   clsArquivo.fnGravaLogTXT(msg.ToString());
                }

			}
			catch(Exception e)
			{
				GlobalVars.Instance.AddMessage(e.ToString() + " - " + e.Message.ToString() );
                clsArquivo.fnGravaLogTXT(e.ToString());
                clsArquivo.fnGravaLogTXT(msg.ToString());
			}
		}
		#endregion 


	}
}
