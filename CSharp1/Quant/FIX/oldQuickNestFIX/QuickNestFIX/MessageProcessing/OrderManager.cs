using System;
using System.Collections.Generic;
using System.Text;
using QuickFix;
using System.Threading;
using System.Data;

namespace QuickNestFIX
{
    public sealed class OrderManager: MessageQueueProcessor
    {
        #region Singleton

        private static volatile OrderManager OMInstance;
        private static Object syncRoot = new Object();
              
        private OrderManager()
            :base()
        {        
            getExtOrdID();
            getIntOrdID();
            getExecRepID();
        }

        public static OrderManager Instance
        {
            get
            {
                if (OMInstance == null)
                {
                    lock (syncRoot)
                    {
                        if (OMInstance == null)
                        {
                            OMInstance = new OrderManager();
                        }
                    }
                }

                return OMInstance;
            }
        }                

        #endregion

        #region Sessions

        private SortedDictionary<string, SessionStatus> ToStreetSessions = new SortedDictionary<string, SessionStatus>();
        private SortedDictionary<string, SessionStatus> FromNestSessions = new SortedDictionary<string, SessionStatus>();
        private SortedDictionary<string, SessionID> Sessions = new SortedDictionary<string, SessionID>();
        private SortedDictionary<string, SessionType> SessionsType = new SortedDictionary<string, SessionType>();

        private List<Sessions> _SessionsList = new List<Sessions>();
        public List<Sessions> SessionsList
        {
            get { return _SessionsList; }
        }
                        
        public void CreateSession(SessionID session, SessionType type)
        {
            switch (type)
            {
                case SessionType.ToStreet:
                    try
                    {
                        ToStreetSessions.Add(session.ToString(), SessionStatus.Disconnected);
                        Sessions.Add(session.ToString(), session);
                        SessionsType.Add(session.ToString(), type);
                        UpdateSessionsList(session.ToString());
                    }
                    catch
                    {
                        //TODO: Implementar tratamento de exceção. Se a sessão já existe, tem q fazer algo?
                        throw new System.NotImplementedException();
                    }
                    break;
                case SessionType.FromNest:
                    try
                    {
                        FromNestSessions.Add(session.ToString(), SessionStatus.Disconnected);
                        Sessions.Add(session.ToString(), session);
                        SessionsType.Add(session.ToString(), type);
                        UpdateSessionsList(session.ToString());
                    }
                    catch
                    {
                        //TODO: Implementar tratamento de exceção. Se a sessão já existe, tem q fazer algo?
                        throw new System.NotImplementedException();
                    }
                    break;
                default:
                    //TODO: Implementar tratamento de SessionType incorreto
                    throw new System.NotImplementedException();
                    
            }


        }

        public void UpdateSession(SessionID session, SessionStatus status, SessionType type)
        {
            switch (type)
            {
                case SessionType.ToStreet:
                    if (ToStreetSessions.ContainsKey(session.ToString()))
                    {
                        ToStreetSessions[session.ToString()] = status;
                        UpdateSessionsList(session.ToString());
                    }
                    break;
                case SessionType.FromNest:
                    if (FromNestSessions.ContainsKey(session.ToString()))
                    {
                        FromNestSessions[session.ToString()] = status;
                        UpdateSessionsList(session.ToString());
                    }
                    break;
                default:
                    //TODO: Implementar tratamento de SessionType incorreto
                    throw new System.NotImplementedException();                    
            }
        }

        private void UpdateSessionsList(string SessionName)
        {
            string status = "";
            string type = "";
            SessionStatus eStatus = SessionStatus.Disconnected;

            switch (SessionsType[SessionName])
            {
                case SessionType.ToStreet:
                    type = "To Street";
                    eStatus = ToStreetSessions[SessionName];
                    break;
                case SessionType.FromNest:
                    type = "From Nest";
                    eStatus = FromNestSessions[SessionName];
                    break;
                default:
                    break;
            }

            switch (eStatus)
            {
                case SessionStatus.Disconnected:
                    status = "Disconnected";
                    break;
                case SessionStatus.Connected:
                    status = "Connected";
                    break;
                default:
                    break;
            }

            bool found = false;

            foreach (Sessions session in SessionsList)
            {
                if (session.Session == SessionName)
                {
                    session.SessionStatus = status;
                    session.SessionType = type;
                    found = true;
                }
            }

            if (!found)
            {
                Sessions session = new Sessions();
                session.Session = SessionName;
                session.SessionStatus = status;
                session.SessionType = type;
                SessionsList.Add(session);
            }
            
        }

        public SessionID GetStreetSession()
        {
            SessionID StreetSession = new SessionID();

            foreach (KeyValuePair<string,SessionStatus> session in ToStreetSessions)
            {
                StreetSession = Sessions[session.Key];
                break;                
            }

            return StreetSession;
        }

        #endregion

        #region Orders        

        public override void ProcessMessage(SessionMessage message)
        {
            crack(message.message, message.session);            
        }

        private SortedDictionary<string, SortedDictionary<string, string>> SessionClOrdIDSymbol = new SortedDictionary<string, SortedDictionary<string, string>>();
        private List<Order> Orders = new List<Order>();
        private volatile object OrdersSync = new object();
                
        public override void onMessage(QuickFix42.NewOrderSingle message, SessionID session)
        {
            string strSession = session.ToString();
            string strClOrdID = message.getClOrdID().getValue();
            string strSymbol = message.getSymbol().getValue();

            if (SessionClOrdIDSymbol.ContainsKey(strSession))
            {
                if (SessionClOrdIDSymbol[strSession].ContainsKey(strClOrdID))
                {
                    RejectDuplicateOrder(message, session);
                }
                else
                {
                    SessionClOrdIDSymbol[strSession].Add(strClOrdID, strSymbol);

                    NewOrderReceived(message, session);
                }
            }
            else
            {
                SortedDictionary<string, string> temp = new SortedDictionary<string, string>();
                temp.Add(strClOrdID, strSymbol);
                SessionClOrdIDSymbol.Add(strSession, temp);

                NewOrderReceived(message, session);
            }                       
        }
        public override void onMessage(QuickFix42.OrderCancelReplaceRequest message, SessionID session)
        {
            string strSession = session.ToString();
            string strOrigClOrdID = message.getOrigClOrdID().getValue();
            string strSymbol = message.getSymbol().getValue();

            bool OrigMsgFound = false;

            if (SessionClOrdIDSymbol.ContainsKey(strSession))
            {
                if (SessionClOrdIDSymbol[strSession].ContainsKey(strOrigClOrdID))
                {
                    if (SessionClOrdIDSymbol[strSession][strOrigClOrdID] == strSymbol)
                    {
                        OrigMsgFound = true;
                    }
                }
            }

            if (OrigMsgFound)
            {
                SessionClOrdIDSymbol[strSession].Add(strOrigClOrdID, strSymbol);                
            }
            else
            {
                RejectUnkownOrder(message, session);
            }

        }
        public override void onMessage(QuickFix42.OrderCancelRequest message, SessionID session)
        {
            string strSession = session.ToString();
            string strOrigClOrdID = message.getOrigClOrdID().getValue();
            string strSymbol = message.getSymbol().getValue();

            bool OrigMsgFound = false;

            if (SessionClOrdIDSymbol.ContainsKey(strSession))
            {
                if (SessionClOrdIDSymbol[strSession].ContainsKey(strOrigClOrdID))
                {
                    if (SessionClOrdIDSymbol[strSession][strOrigClOrdID] == strSymbol)
                    {
                        OrigMsgFound = true;
                    }
                }
            }

            if (OrigMsgFound)
            {
                SessionClOrdIDSymbol[strSession].Add(strOrigClOrdID, strSymbol);                
            }
            else
            {
                RejectUnkownOrder(message, session);
            }
        }
        public override void onMessage(QuickFix42.ExecutionReport message, SessionID session)
        {
            string strSession = session.ToString();
            string strClOrdID = message.getClOrdID().getValue();
            string strSymbol = message.getSymbol().getValue();

            bool OrigMsgFound = false;
            char chrDKReason = '?';

            if (SessionClOrdIDSymbol.ContainsKey(strSession))
            {
                if (SessionClOrdIDSymbol[strSession].ContainsKey(strClOrdID))
                {
                    if (SessionClOrdIDSymbol[strSession][strClOrdID] == strSymbol)
                    {
                        OrigMsgFound = true;
                    }
                    else
                    {
                        chrDKReason = 'A'; //Unknown symbol
                    }
                }
                else
                {
                    chrDKReason = 'D'; //No matching order
                }
            }

            if (OrigMsgFound)
            {
                SessionClOrdIDSymbol[strSession].Add(strClOrdID, strSymbol);                
            }
            else
            {
                RejectDontKnowTrade(message, session, chrDKReason);
            }
        }
        public override void onMessage(QuickFix42.OrderCancelReject message, SessionID session)
        {
            string strSession = session.ToString();
            string strClOrdID = message.getClOrdID().getValue();
            string strSymbol = "";

            bool OrigMsgFound = false;

            if (SessionClOrdIDSymbol.ContainsKey(strSession))
            {
                if (SessionClOrdIDSymbol[strSession].ContainsKey(strClOrdID))
                {
                    strSymbol = SessionClOrdIDSymbol[strSession][strClOrdID];
                    OrigMsgFound = true;
                }
            }

            if (OrigMsgFound)
            {
                SessionClOrdIDSymbol[strSession].Add(strClOrdID, strSymbol);                
            }
            else
            {
                RejectUnknownCxlRplRqt(message, session);
            }
        }
                    
   
        //TODO: Implementar rejeições de mensagens recebidas
        private void RejectDuplicateOrder(QuickFix42.NewOrderSingle message, SessionID session)
        {
            throw new System.NotImplementedException("Duplicate Order");
        }
        private void RejectUnkownOrder(QuickFix42.OrderCancelReplaceRequest message, SessionID session)
        {
            throw new System.NotImplementedException("Unknown order requested to be replaced");
        }
        private void RejectUnkownOrder(QuickFix42.OrderCancelRequest message, SessionID session)
        {
            throw new System.NotImplementedException("Unknown order requested to be cancelled");
        }
        private void RejectDontKnowTrade(QuickFix42.ExecutionReport message, SessionID session, char chrDKReason)
        {
            throw new System.NotImplementedException("Unknown order received execution report");
        }
        private void RejectUnknownCxlRplRqt(QuickFix42.OrderCancelReject message, SessionID session)
        {
            throw new System.NotImplementedException("Unknown Cancel/Replace received rejection");
        }

        private void NewOrderReceived(QuickFix42.NewOrderSingle message, SessionID session)
        {                                
            Order ReceivedOrder = new Order();

            ReceivedOrder.SessionID = session.toString();
            ReceivedOrder.ClOrdID = message.getClOrdID().getValue();
            ReceivedOrder.Symbol = message.getSymbol().getValue();

            switch (message.getSide().getValue())
            {
                case QuickFix.Side.BUY:
                    ReceivedOrder.Side = "BUY";
                    break;
                case QuickFix.Side.SELL:
                    ReceivedOrder.Side = "SELL";
                        break;
                default:
                        break;
            }

            OrderQty qfQuantity = new OrderQty();
            message.getField(qfQuantity);
            ReceivedOrder.Quantity = Convert.ToInt32(qfQuantity.getValue());

            QuickFix.Price qfPrice = new Price();
            message.getField(qfPrice);
            ReceivedOrder.Price = qfPrice.getValue();

            lock (OrdersSync)
            {
                Orders.Add(ReceivedOrder);
            }
        }
        
        public void SendMessage(SessionMessage MessageToSend)
        {
            Session.sendToTarget(MessageToSend.message, MessageToSend.session);                    
        }       
                              
        #endregion

        #region OrderID

        private int OrdID;
        private Mutex OrdIDMutex = new Mutex();
        private void getExtOrdID()
        {
            OrdIDMutex.WaitOne();
            if (DateTime.Today.Equals(QuickNestFIX.Properties.Settings.Default.ExtOrdIDDate))
            {
                OrdID = QuickNestFIX.Properties.Settings.Default.ExtOrdID;
            }
            else
            {
                QuickNestFIX.Properties.Settings.Default.ExtOrdIDDate = DateTime.Today;
                OrdID = 0;
                QuickNestFIX.Properties.Settings.Default.ExtOrdID = OrdID;
                QuickNestFIX.Properties.Settings.Default.Save();
            }
            OrdIDMutex.ReleaseMutex();
        }

        public int getNextExtOrdID()
        {
            OrdIDMutex.WaitOne();
            if (DateTime.Today.Equals(QuickNestFIX.Properties.Settings.Default.ExtOrdIDDate))
            {
                OrdID++;
                QuickNestFIX.Properties.Settings.Default.ExtOrdID = OrdID;
                QuickNestFIX.Properties.Settings.Default.Save();
            }
            else
            {
                QuickNestFIX.Properties.Settings.Default.ExtOrdIDDate = DateTime.Today;
                OrdID = 0;
                QuickNestFIX.Properties.Settings.Default.ExtOrdID = OrdID;
                QuickNestFIX.Properties.Settings.Default.Save();
            }
            OrdIDMutex.ReleaseMutex();

            return OrdID;
        }

        private int IntOrdID;

        private void getIntOrdID()
        {
            OrdIDMutex.WaitOne();
            if (DateTime.Today.Equals(QuickNestFIX.Properties.Settings.Default.IntOrdIDDate))
            {
                IntOrdID = QuickNestFIX.Properties.Settings.Default.IntOrdID;
            }
            else
            {
                QuickNestFIX.Properties.Settings.Default.IntOrdIDDate = DateTime.Today;
                IntOrdID = 0;
                QuickNestFIX.Properties.Settings.Default.IntOrdID = IntOrdID;
                QuickNestFIX.Properties.Settings.Default.Save();
            }
            OrdIDMutex.ReleaseMutex();
        }

        public int getNextIntOrdID()
        {
            OrdIDMutex.WaitOne();
            if (DateTime.Today.Equals(QuickNestFIX.Properties.Settings.Default.IntOrdIDDate))
            {
                IntOrdID++;
                QuickNestFIX.Properties.Settings.Default.IntOrdID = IntOrdID;
                QuickNestFIX.Properties.Settings.Default.Save();
            }
            else
            {
                QuickNestFIX.Properties.Settings.Default.IntOrdIDDate = DateTime.Today;
                IntOrdID = 0;
                QuickNestFIX.Properties.Settings.Default.IntOrdID = IntOrdID;
                QuickNestFIX.Properties.Settings.Default.Save();
            }
            OrdIDMutex.ReleaseMutex();

            return IntOrdID;
        }


        private int ExecRepID;

        private void getExecRepID()
        {
            OrdIDMutex.WaitOne();
            if (DateTime.Today.Equals(QuickNestFIX.Properties.Settings.Default.ExecRepDate))
            {
                ExecRepID = QuickNestFIX.Properties.Settings.Default.ExecRepID;
            }
            else
            {
                QuickNestFIX.Properties.Settings.Default.ExecRepDate = DateTime.Today;
                ExecRepID = 0;
                QuickNestFIX.Properties.Settings.Default.ExecRepID = ExecRepID;
                QuickNestFIX.Properties.Settings.Default.Save();
            }
            OrdIDMutex.ReleaseMutex();
        }

        public int getNextExecID()
        {
            OrdIDMutex.WaitOne();
            if (DateTime.Today.Equals(QuickNestFIX.Properties.Settings.Default.ExecRepDate))
            {
                ExecRepID++;
                QuickNestFIX.Properties.Settings.Default.ExecRepID = ExecRepID;
                QuickNestFIX.Properties.Settings.Default.Save();
            }
            else
            {
                QuickNestFIX.Properties.Settings.Default.ExecRepDate = DateTime.Today;
                ExecRepID = 0;
                QuickNestFIX.Properties.Settings.Default.ExecRepID = ExecRepID;
                QuickNestFIX.Properties.Settings.Default.Save();
            }
            OrdIDMutex.ReleaseMutex();

            return ExecRepID;
        }

        #endregion
                                     

        public override void Stop()
        {
            base.Stop();            
        }
    }
}
