/****************************************************************************
** Copyright (c) quickfixengine.org  All rights reserved.
**
** This file is part of the QuickFIX FIX Engine
**
** This file may be distributed under the terms of the quickfixengine.org
** license as defined by quickfixengine.org and appearing in the file
** LICENSE included in the packaging of this file.
**
** This file is provided AS IS with NO WARRANTY OF ANY KIND, INCLUDING THE
** WARRANTY OF DESIGN, MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
**
** See http://www.quickfixengine.org/LICENSE for licensing information.
**
** Contact ask@quickfixengine.org if any conditions of this licensing are
** not clear to you.
**
****************************************************************************/

using System;
using System.Threading;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using QuickFix;

namespace TwgSendPTF
{
	public class Application: MessageCracker, QuickFix.Application
	{
		private Int64 iUltimaOrder = 0;
		private Int64 iAtualOrder  = 0;
		private Int64 iUltimaOrderCancelada = 0;
		private Int64 iAtualOrderCancelada = 0;
	
		public QuickFix.SessionID secaoId = null;
		System.Threading.Thread thReceberMSG   = null;
		System.Threading.ThreadStart tsReceberMSG   = null;
		
		#region Thread
		public Application()
		{
			tsReceberMSG    = new ThreadStart(ReceberMSG);
			thReceberMSG    = new Thread(tsReceberMSG);
			thReceberMSG.Start();
		}

		public void Dispose()
		{
			thReceberMSG.Suspend();
			tsReceberMSG = null;
			thReceberMSG = null;
		}
		
		public void ReceberMSG()
		{
			while(1==1)
			{
				if ( secaoId != null)
				{
					Console.WriteLine("### Sessão Ativa - Send ###PTF " +  DateTime.Now.ToString() );
					
					#region CANCELAMENTO
					// Verifica se existem Cancelamentos a serem enviados
					iUltimaOrderCancelada =  clsProcessamento.fnVerificaUltimoCancelamento("");
					if(iAtualOrderCancelada != iUltimaOrderCancelada && iUltimaOrderCancelada != 0)
					{
						iAtualOrderCancelada = iUltimaOrderCancelada;
						this.fnFormataCancelRequest(clsProcessamento.fnFormataNewOrdemSigle_35F(iUltimaOrderCancelada), secaoId);
					}
					#endregion


					#region ENVIO
					// Verifica se existem Ordens a serem enviadas
					// -------------------------------------------
					iUltimaOrder = clsProcessamento.fnVerificaUltimaNovaOrdem("");
					if(iAtualOrder != iUltimaOrder && iUltimaOrder != 0)
					{
						iAtualOrder = iUltimaOrder;
						this.fnFormataNewOrdemSigle(clsProcessamento.fnFormataNewOrdemSigle_35D(iUltimaOrder), secaoId);
					}
					#endregion
				} 
				else 
				{
					Console.WriteLine("### Sessão Inativa - ###PTF " +  DateTime.Now.ToString() );
				}

				// Pausa de 10 segundos
				Thread.Sleep(10000);
			}
		}
		#endregion

		#region Eventos FIX

		public void onCreate( SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("onCreate: " + sessionID);
			       Console.WriteLine("onCreate: " + sessionID);
		}
		
		public void onLogon( SessionID sessionID ) 
		{
			clsArquivo.fnGravaLogTXT("onLogon: " + sessionID);
			       Console.WriteLine("onLogon: " + sessionID);

			secaoId = sessionID;
		}
		
		public void fromAdmin( Message message, SessionID sessionID ) 
		{
			Console.Write("fromAdmin: " + message.ToXML());
			crack( message, sessionID );
		}

		public void fromApp( Message message, SessionID sessionID )
		{
			Console.Write("fromApp: " + message.ToXML());
			crack( message, sessionID );
		}

		public void toApp( Message message, SessionID sessionID )   { }
		

		public void toAdmin( Message message, SessionID sessionID )	
		{
			// message.getHeader().setField(new OnBehalfOfCompID("ICAP217")); 
			// message.getHeader().setField(new DeliverToCompID ("FIXA")); 

			Console.WriteLine("toAdmin: " +  message.ToXML());
			Console.WriteLine("toAdmin: " +  sessionID.ToString());
		}

		public void onLogout( SessionID sessionID)
		{ 
			clsArquivo.fnGravaLogTXT("onLogout: " + sessionID.ToString());
			Console.WriteLine       ("onLogout: " + sessionID.ToString());
			secaoId = null;
		}
		
		#endregion
	

		#region ### QuickFix42

		// 35= 0 - Heartbeat <0> 
		#region void onMessage( QuickFix42.Heartbeat message, SessionID sessionID )
		public override void onMessage( QuickFix42.Heartbeat message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.Heartbeat: " + message.ToXML());
			Console.WriteLine("QuickFix42.Heartbeat: " + message.ToXML());
		}
		#endregion

 
		// 35= 1 - Test Request <1> 
		#region void onMessage( QuickFix42.TestRequest message, SessionID sessionID )
		public override void onMessage( QuickFix42.TestRequest message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.TestRequest: " + message.ToXML());
			Console.WriteLine("QuickFix42.TestRequest: "  + message.ToXML());
		}
		#endregion


		// 35= 2 - Resend Request <2> 
		#region void onMessage( QuickFix42.ResendRequest message, SessionID sessionID )
		public override void onMessage( QuickFix42.ResendRequest message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.ResendRequest: " + message.ToXML());
			Console.WriteLine("QuickFix42.ResendRequest: " + message.ToXML());
		}
		#endregion


		// 35= 3 - Reject <3> 
		#region void onMessage( QuickFix42.Reject message, SessionID sessionID )
		public override void onMessage( QuickFix42.Reject message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.Reject: " + message.ToXML());
			Console.WriteLine("QuickFix42.Reject: " + message.ToXML());
		}
		#endregion


		// 35= 4 - Sequence Reset <4> 
		#region void onMessage( QuickFix42.SequenceReset message, SessionID sessionID )
		public override void onMessage( QuickFix42.SequenceReset message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.SequenceReset: " + message.ToXML());
			Console.WriteLine("QuickFix42.SequenceReset:        " + message.ToXML());

			QuickFix42.SequenceReset msg = new QuickFix42.SequenceReset();
			msg.setField( new BooleanField(123, true));		// GapFillFlag
			msg.setField( new IntField(36, 1) );			// NewSeqNo
			Session.sendToTarget(msg, sessionID);
		}
		#endregion


		// 35= 5 = Logout <A> 
		#region void onMessage( QuickFix42.Logout message, SessionID sessionID )
		public override void onMessage( QuickFix42.Logout message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.Logout: " + message.ToXML());
			Console.WriteLine("QuickFix42.Logout:        " + message.ToXML());

			// Envia mensagem de queda de conexão
			Session.sendToTarget(message, sessionID);
			
			// anula Sessoes ativas
			secaoId = null;
		}
		#endregion


		// 35= 8 = Execution Report <8> 
		#region void onMessage( QuickFix42.ExecutionReport message, SessionID sessionID )
		public override void onMessage( QuickFix42.ExecutionReport message, SessionID sessionID )
		{
			       Console.WriteLine("QuickFix42.ExecutionReport: " + message.ToXML());
			clsArquivo.fnGravaLogTXT("QuickFix42.ExecutionReport: " + message.ToXML());
			
			// Aciona metodo para inserir Prints
			clsProcessamento obj = new clsProcessamento();
			obj.fnGravaPrints(message,  sessionID );
		}
		#endregion

	
		// 35= 9 = Order Cancel Reject <9>
		#region void onMessage( QuickFix42.OrderCancelReject message, SessionID sessionID )
		public override void onMessage( QuickFix42.OrderCancelReject message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.OrderCancelReject" + message.ToXML());
			Console.WriteLine       ("QuickFix42.OrderCancelReject" + message.ToXML());
			
            ExecutionReport_35_9(message, sessionID);			
		}
		#endregion


		// 35= A = Logon <A> 
		#region void onMessage( QuickFix42.Logon message, SessionID sessionID )
		public override void onMessage( QuickFix42.Logon message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.Logon: " + message.ToXML());
			Console.WriteLine("QuickFix42.Logon:        " + message.ToXML());

			secaoId = sessionID;
		}
		#endregion


		// 35= D = Order - Single <D>
		#region void onMessage( QuickFix42.NewOrderSingle message, SessionID sessionID )
		public override void onMessage( QuickFix42.NewOrderSingle message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.NewOrderSingle " + message.ToXML());
			Console.WriteLine       ("QuickFix42.NewOrderSingle " + message.ToXML().ToString());
		}
		#endregion

		
		// 35= F = Order Cancel Request <F>
		#region void onMessage( QuickFix42.OrderCancelRequest message, SessionID sessionID )
		public override void onMessage( QuickFix42.OrderCancelRequest message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.OrderCancelRequest" + message.ToXML());
			Console.WriteLine       ("QuickFix42.OrderCancelRequest" + message.ToXML());

			ExecutionReport_35F(message, sessionID);
		}
		#endregion


		// 35= G = Order Cancel/Replace Request
		#region void onMessage( QuickFix42.OrderCancelReplaceRequest message, SessionID sessionID )
		public override void onMessage( QuickFix42.OrderCancelReplaceRequest message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.OrderCancelReplaceRequest" + message.ToXML());
			Console.WriteLine       ("QuickFix42.OrderCancelReplaceRequest" + message.ToXML());
		}
		#endregion


		// 35= J = Allocation
		#region void onMessage( QuickFix42.Allocation message, SessionID sessionID )
		public override void onMessage( QuickFix42.Allocation message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.Allocation" + message.ToXML());
			Console.WriteLine       ("QuickFix42.Allocation" + message.ToXML());
		}
		#endregion


		// 35= j = Business Message Reject
		#region void onMessage( QuickFix42.BusinessMessageReject message, SessionID sessionID )
		/*
		public override void onMessage( QuickFix42.BusinessMessageReject message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.BusinessMessageReject" + message.ToXML());
			Console.WriteLine       ("QuickFix42.BusinessMessageReject" + message.ToXML());
		}
		*/
		#endregion


		// 35= P = Allocation ACK
		#region void onMessage( QuickFix42.AllocationACK message, SessionID sessionID )
		public override void onMessage( QuickFix42.AllocationACK message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.AllocationACK " + message.ToXML());
			Console.WriteLine("QuickFix42.AllocationACK " + message.ToXML());
		}
		#endregion


		// 35= Q = Don't Know Trade
		#region void onMessage( QuickFix42.DontKnowTrade message, SessionID sessionID )
		public override void onMessage( QuickFix42.DontKnowTrade message, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("QuickFix42.DontKnowTrade" + message.ToXML());
			Console.WriteLine("QuickFix42.DontKnowTrade" + message.ToXML());
		}
		#endregion


		#endregion

			
		#region ### ExecutionReport's

		// ***** Sequence Reset - 35 = 4
		#region void fnSequenceReset(SessionID sessionID )
		private void fnSequenceReset(SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("fnSequenceReset: ");
			Console.WriteLine("fnSequenceReset: ");
			
			QuickFix42.SequenceReset msg = new QuickFix42.SequenceReset();
			msg.setField( new BooleanField(123, true));		// GapFillFlag
			msg.setField( new IntField(36, 1) );			// NewSeqNo
			Session.sendToTarget(msg, sessionID);
		}

		#endregion

		
		// ***** NewOrdemSigle
		#region void fnFormataNewOrdemSigle(QuickFix.Message message_ordem, SessionID sessionID )
		private void fnFormataNewOrdemSigle(QuickFix.Message message_ordem, SessionID sessionID )
		{
			clsArquivo.fnGravaLogTXT("fnFormataNewOrdemSigle_35D: " + message_ordem.ToXML());
			       Console.WriteLine("fnFormataNewOrdemSigle_35D: " + message_ordem.ToXML());

			QuickFix42.NewOrderSingle message = new QuickFix42.NewOrderSingle();
			
			message.setField(1, message_ordem.getField(1));			// Account
			message.setField(11, message_ordem.getField(11));		// CLORDID
			message.setField(15, message_ordem.getField(15));		// Currency
			message.setField(21, message_ordem.getField(21));		// HandlIns 
			message.setField(22, message_ordem.getField(22));		// IDSource
			message.setField(38, message_ordem.getField(38));		// OrderQty
			message.setField(40, message_ordem.getField(40));		// OrdType 
			message.setField(44, message_ordem.getField(44));		// Price
			message.setField(54, message_ordem.getField(54));		// SIDE 
			message.setField(55, message_ordem.getField(55));		// SYMBOL
			message.setField(59, message_ordem.getField(59));		// TimeInForce
			message.setField(207, message_ordem.getField(207));		// SecurityExchange
			message.setField(60, message_ordem.getField(60));		// TransactTime
			
			// Envia menssagem
			Session.sendToTarget(message, sessionID);                                                      
		}

		#endregion

		
		// ***** CancelRequest
		#region void fnFormataCancelRequest(QuickFix.Message message_ordem, SessionID sessionID )
		private void fnFormataCancelRequest(QuickFix.Message message_ordem, SessionID sessionID )
		{
			       Console.WriteLine("fnFormataCancelRequest: " + message_ordem.ToXML());
			clsArquivo.fnGravaLogTXT("fnFormataCancelRequest: " + message_ordem.ToXML());

			QuickFix42.OrderCancelRequest message = new QuickFix42.OrderCancelRequest();
			
			message.setField(11, message_ordem.getField(11));		// CLORDID
			message.setField(41, message_ordem.getField(41));		// OrigClOrdID 
			message.setField(54, message_ordem.getField(54));		// Side 
			message.setField(55, message_ordem.getField(55));		// Symbol
			message.setField(60, message_ordem.getField(60));		// TransactTime
			
			// Envia menssagem
			Session.sendToTarget(message, sessionID);                                                      
		}
		#endregion


		// ***** ExecutionReport - 35 = D
		#region void ExecutionReport_35D(QuickFix.Message message_ordem, SessionID sessionID )
		private void ExecutionReport_35D(QuickFix.Message message_ordem, SessionID sessionID )
		{
			       Console.WriteLine("ExecutionReport_35D: " + message_ordem.ToXML());
			clsArquivo.fnGravaLogTXT("ExecutionReport_35D: " + message_ordem.ToXML());

			QuickFix42.NewOrderSingle message = new QuickFix42.NewOrderSingle();
			
			message.setField(11, message_ordem.getField(11));		// CLORDID
			message.setField(1,  message_ordem.getField(1));		// Account
			message.setField(15, message_ordem.getField(15));		// Currency
			message.setField(21, message_ordem.getField(21));		// HandlIns 
			message.setField(38, message_ordem.getField(38));		// OrderQty
			message.setField(40, message_ordem.getField(40));		// OrdType 
			message.setField(44, message_ordem.getField(44));		// Price
			message.setField(54, message_ordem.getField(54));		// SIDE 
			message.setField(55, message_ordem.getField(55));		// SYMBOL
			message.setField(59, message_ordem.getField(59));		// TimeInForce
			message.setField(207, message_ordem.getField(207));		// SecurityExchange
			message.setField(60, message_ordem.getField(60));		// TransactTime
		
			// Envia menssagem
			Session.sendToTarget(message, sessionID); 
		}

		#endregion

		
		// ***** CancelRequest - 35 = F
		#region void ExecutionReport_35F(QuickFix.Message message_ordem, SessionID sessionID )
		private void ExecutionReport_35F(QuickFix.Message message_ordem, SessionID sessionID )
		{
			       Console.WriteLine("ExecutionReport_35F: " + message_ordem.ToXML());
			clsArquivo.fnGravaLogTXT("ExecutionReport_35F: " + message_ordem.ToXML());
			
			QuickFix42.ExecutionReport message = new QuickFix42.ExecutionReport();
			
			message.setField(11, message_ordem.getField(11));	// CLORDID
			message.setField(41, message_ordem.getField(41));	// OrigCLORDID
			message.setField(54, message_ordem.getField(54));	// Side 
			message.setField(55, message_ordem.getField(55));	// Symbol
			message.setField(60, message_ordem.getField(60));	// TransactTime 
			
			// Envia mensagem
			Session.sendToTarget(message, sessionID);
		}

		#endregion


		// ***** CancelReject - 35 = 9
		#region void ExecutionReport_35_9(QuickFix.Message message, SessionID sessionID )
		private void ExecutionReport_35_9(QuickFix.Message message, SessionID sessionID )
		{
			       Console.WriteLine("ExecutionReport_35_9: " + message.ToXML());
			clsArquivo.fnGravaLogTXT("ExecutionReport_35_9: " + message.ToXML());
			
			QuickFix42.ExecutionReport msgFix = new QuickFix42.ExecutionReport();
			
			msgFix.setField(11, message.getField(11));	// CLORDID
			msgFix.setField(37, message.getField(37));	// OrderID
			msgFix.setField(39, message.getField(39));	// OrdStatus
			msgFix.setField(41, message.getField(41));	// OrigCLORDID
			
			// Envia menssagem
			Session.sendToTarget(msgFix, sessionID);                                                      
		}

		#endregion

		#endregion
		
	}
}