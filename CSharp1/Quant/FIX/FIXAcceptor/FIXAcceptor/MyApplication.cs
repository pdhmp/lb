using System;
//using System.Collections.Generic;
//using System.Text;
using QuickFix;

namespace FIXAcceptor
{
    class MyApplication: MessageCracker, QuickFix.Application
    {
        SessionID secaoID = null; //ID da seção FIX

        #region Tratamento de Mensagens

        public override void onMessage(QuickFix42.NewOrderSingle message, SessionID session)
        {
            Console.WriteLine("");
            Console.WriteLine("|----------- NewOrderSingle -----------|");
            Console.WriteLine(message.ToXML());
            Console.WriteLine("|----------- NewOrderSingle -----------|");
            Console.WriteLine("");

            int iQty = Convert.ToInt32(message.getOrderQty().getValue());
            int fills = iQty; //  (iQty == 1 ? 1 : iQty / 2);
            
            QuickFix42.ExecutionReport excRep = new QuickFix42.ExecutionReport(new OrderID("001"),
                                                                               new ExecID("001"),
                                                                               new ExecTransType(ExecTransType.NEW),
                                                                               new ExecType(ExecType.PARTIAL_FILL),
                                                                               new OrdStatus(OrdStatus.FILLED),
                                                                               message.getSymbol(),
                                                                               message.getSide(),
                                                                               new LeavesQty(iQty - fills),
                                                                               new CumQty(fills),
                                                                               new AvgPx(0));

            excRep.set(new OrderQty(message.getOrderQty().getValue()));
            excRep.set(new LastShares(fills));
            excRep.set(new ClOrdID(message.getClOrdID().getValue()));
            if (message.getOrdType().getValue() == OrdType.MARKET)
            {
                excRep.set(new LastPx(66.6));
            }
            else
            {
                excRep.set(new LastPx(message.getPrice().getValue()));
            }
            TimeInForce tif = new TimeInForce();
            bool IOC = false;

            if (message.isSet(tif))
            {
                tif = message.getTimeInForce();
                excRep.set(tif);
                if (tif.getValue() == TimeInForce.IMMEDIATE_OR_CANCEL)
                {
                    IOC = true;
                }
            }
            

            Session.sendToTarget(excRep, secaoID);


            if (IOC)
            {
                QuickFix42.ExecutionReport excCxlRep = new QuickFix42.ExecutionReport(new OrderID("001"),
                                                                               new ExecID("001"),
                                                                               new ExecTransType(ExecTransType.NEW),
                                                                               new ExecType(ExecType.CANCELED),
                                                                               new OrdStatus(OrdStatus.CANCELED),
                                                                               message.getSymbol(),
                                                                               message.getSide(),
                                                                               new LeavesQty(0),
                                                                               new CumQty(fills),
                                                                               new AvgPx(0));

                excCxlRep.set(new OrderQty(message.getOrderQty().getValue()));
                excCxlRep.set(new LastShares(0));
                excCxlRep.set(new ClOrdID(message.getClOrdID().getValue()));
                excCxlRep.set(new LastPx(0));
                excCxlRep.set(new TimeInForce(message.getTimeInForce().getValue()));


                Session.sendToTarget(excCxlRep, secaoID);
            }           
        }

        public override void onMessage(QuickFix42.OrderCancelRequest message, SessionID session)
        {
            SendCancel(message, session);
            //SendCxlRplReject(message, session);
        }

        private void SendCancel(QuickFix42.OrderCancelRequest message, SessionID session)
        {
            Console.WriteLine("\r\n\r\nEnter the cumulative quantity realized for the order");
            string sqty = Console.ReadLine();
            int iqty = Convert.ToInt32(sqty);
            QuickFix42.ExecutionReport excCxlRep = new QuickFix42.ExecutionReport(new OrderID("001"),
                                                                               new ExecID("001"),
                                                                               new ExecTransType(ExecTransType.NEW),
                                                                               new ExecType(ExecType.CANCELED),
                                                                               new OrdStatus(OrdStatus.CANCELED),
                                                                               message.getSymbol(),
                                                                               message.getSide(),
                                                                               new LeavesQty(0),
                                                                               new CumQty(iqty),
                                                                               new AvgPx(0));

            excCxlRep.set(new OrderQty(message.getOrderQty().getValue()));
            excCxlRep.set(new LastShares(0));
            excCxlRep.set(new ClOrdID(message.getClOrdID().getValue()));
            excCxlRep.set(new LastPx(0));
            

            Session.sendToTarget(excCxlRep, secaoID);
        }

        public override void onMessage(QuickFix42.OrderCancelReplaceRequest message, SessionID session)
        {
            SendReplace(message, session);
        }

        private void SendReplace(QuickFix42.OrderCancelReplaceRequest message, SessionID session)
        {
            QuickFix42.ExecutionReport excCxlRep = new QuickFix42.ExecutionReport(new OrderID("001"),
                                                                               new ExecID("001"),
                                                                               new ExecTransType(ExecTransType.NEW),
                                                                               new ExecType(ExecType.REPLACE),
                                                                               new OrdStatus(OrdStatus.REPLACED),
                                                                               message.getSymbol(),
                                                                               message.getSide(),
                                                                               new LeavesQty(0),
                                                                               new CumQty((int)(message.getOrderQty().getValue() / 2)),
                                                                               new AvgPx(0));

            excCxlRep.set(new OrderQty(message.getOrderQty().getValue()));
            excCxlRep.set(new LastShares(0));
            excCxlRep.set(new ClOrdID(message.getClOrdID().getValue()));
            excCxlRep.set(new LastPx(0));


            Session.sendToTarget(excCxlRep, secaoID);
        }

        private void SendCxlRplReject(QuickFix42.OrderCancelRequest message, SessionID session)
        {
            SendCxlRplReject(message.getClOrdID().getValue(), message.getOrigClOrdID().getValue(), CxlRejResponseTo.ORDER_CANCEL_REQUEST, session);
        }
        private void SendCxlRplReject(QuickFix42.OrderCancelReplaceRequest message, SessionID session)
        {
            SendCxlRplReject(message.getClOrdID().getValue(), message.getOrigClOrdID().getValue(), CxlRejResponseTo.ORDER_CANCEL_REPLACE_REQUEST, session);
        }
        private void SendCxlRplReject(string _OrderID, string _OrigOrderID, char _responseTo, SessionID session)
        {
            OrderID qf_OrderId = new OrderID(_OrderID);
            ClOrdID qf_ClOrdID = new ClOrdID(_OrderID);
            OrigClOrdID qf_OrigClOrdID = new OrigClOrdID(_OrigOrderID);
            OrdStatus qf_OrdStatus = new OrdStatus(OrdStatus.PARTIALLY_FILLED);
            CxlRejResponseTo qf_CxlRejResponseTo = new CxlRejResponseTo(_responseTo);
            TransactTime qf_TransactTime = new TransactTime(DateTime.Now, true);
            CxlRejReason qf_CxlRejReason = new CxlRejReason(CxlRejReason.BROKER_EXCHANGE_OPTION);

            QuickFix42.OrderCancelReject reject = new QuickFix42.OrderCancelReject(qf_OrderId, qf_ClOrdID, qf_OrigClOrdID, qf_OrdStatus, qf_CxlRejResponseTo);

            reject.set(qf_TransactTime);
            reject.set(qf_CxlRejReason);

            Session.sendToTarget(reject, session);
        }

        #endregion

        #region Eventos FIX - Métodos obrigatórios QuickFix

        public void onCreate(SessionID sessionID)
        {
            Console.WriteLine("onCreate: " + sessionID);
        }
        public void onLogon(SessionID sessionID)
        {
            Console.WriteLine("onLogon: " + sessionID);

            //Armazena o id da seção FIX
            secaoID = sessionID;
        }
        public void onLogout(SessionID sessionID)
        {
            Console.WriteLine("onLogout: "+sessionID.toString());

            //Apaga o valor armazenado da seção FIX
            secaoID = null;
        }
        public void toAdmin(Message message, SessionID sessionID)
        {
            //Console.WriteLine("toAdmin: " + message.ToXML());
            Console.WriteLine("toAdmin: " + sessionID.ToString());
        }
        public void toApp(Message message, SessionID sessionID)
        {
            //Método em branco.
        }
        public void fromAdmin(Message message, SessionID sessionID)
        {
            //Console.WriteLine("fromAdmin: " + message.ToXML());
                        
            crack(message, sessionID);
        }
        public void fromApp(Message message, SessionID sessionID)
        {
            Console.WriteLine("fromApp: " + message.ToXML());

            crack(message, sessionID);
        }

        #endregion

        #region Disposable Region
        public void Dispose()
        {
        }
        #endregion
    }
}
