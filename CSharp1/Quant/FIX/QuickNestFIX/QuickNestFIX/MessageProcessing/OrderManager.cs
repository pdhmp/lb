using System;
using System.Collections.Generic;
using System.Text;
using QuickFix;
using System.Threading;
using System.Data;
using NestSymConn;
using NCommonTypes;


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
            SymConn.Instance.OnData += new EventHandler(Instance_OnData);
        }

        void Instance_OnData(object sender, EventArgs e)
        {
            lock (OrdersSync)
            {
                MarketUpdateList curUpdateList = (MarketUpdateList)e;

                foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
                {
                    if (curUpdateItem.FLID == NestFLIDS.Last) { LastPrice[curUpdateItem.Ticker] = curUpdateItem.ValueDouble; }
                }                
            }
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

        private SessionID DropCopySession;
        private SessionStatus DropCopyStatus;

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
                case SessionType.DropCopy:
                    DropCopySession = session;
                    DropCopyStatus = SessionStatus.Disconnected;
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
                case SessionType.DropCopy:
                    DropCopyStatus = status;
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


        private volatile object OrdersSync = new object();
        private List<Order> Orders = new List<Order>();        
        public List<Order> ReceivedOrders
        {
            get
            {
                List<Order> auxOrders = new List<Order>();

                lock (OrdersSync)
                {                  
                    foreach (Order ord in Orders)
                    {
                        if (ord.Quantity > 0)
                        {
                            Order auxOrd = new Order();

                            auxOrd.SessionID = ord.SessionID;
                            auxOrd.ClOrdID = ord.ClOrdID;
                            auxOrd.Symbol = ord.Symbol;
                            auxOrd.Side = ord.Side;
                            auxOrd.Quantity = ord.Quantity;
                            auxOrd.Price = ord.Price;
                            auxOrd.OrderID = ord.OrderID;
                            auxOrd.IDPortfolio = ord.IDPortfolio;
                            auxOrd.IDBook = ord.IDBook;
                            auxOrd.IDSection = ord.IDSection;
                            auxOrd.ExecQty = ord.ExecQty;
                            auxOrd.AvgPx = ord.AvgPx;
                            auxOrd.DontMatch = ord.DontMatch;
                            auxOrd.IntCLOrd = ord.IntCLOrd;
                            auxOrd.IntOrigClOrd = ord.IntOrigClOrd;
                            auxOrd.OrigClOrdID = ord.OrigClOrdID; 

                            auxOrders.Add(auxOrd);
                        }
                    }
                }

                return auxOrders;
            }
        }
        public List<Order> ExecOrders
        {
            get
            {
                List<Order> auxOrders = new List<Order>();

                lock (OrdersSync)
                {
                    foreach (Order ord in Orders)
                    {
                        Order auxOrd = new Order();

                        auxOrd.SessionID = ord.SessionID;
                        auxOrd.ClOrdID = ord.ClOrdID;
                        auxOrd.Symbol = ord.Symbol;
                        auxOrd.Side = ord.Side;
                        auxOrd.Quantity = ord.Quantity;
                        auxOrd.Price = ord.Price;
                        auxOrd.OrderID = ord.OrderID;
                        auxOrd.IDPortfolio = ord.IDPortfolio;
                        auxOrd.IDBook = ord.IDBook;
                        auxOrd.IDSection = ord.IDSection;
                        auxOrd.ExecQty = ord.ExecQty;
                        auxOrd.AvgPx = ord.AvgPx;
                        auxOrd.DontMatch = ord.DontMatch;
                        auxOrd.IntCLOrd = ord.IntCLOrd;
                        auxOrd.IntOrigClOrd = ord.IntOrigClOrd;
                        auxOrd.OrigClOrdID = ord.OrigClOrdID;

                        auxOrders.Add(auxOrd);
                    }
                }

                return auxOrders;
            }
        }
        public List<OrdersBySymbol> ReceivedBySymbol
        {
            get
            {
                List<OrdersBySymbol> recBySymbol = new List<OrdersBySymbol>();
                SortedDictionary<SelectedItem, OrdersBySymbol> auxDic = new SortedDictionary<SelectedItem, OrdersBySymbol>();                

                lock (OrdersSync)
                {
                    foreach (Order ord in Orders)
                    {
                        SelectedItem item = new SelectedItem();
                        item.IDPortfolio = ord.IDPortfolio;
                        item.Symbol = ord.Symbol;
                        
                        if (ord.Quantity > 0)
                        {                           
                            if (auxDic.ContainsKey(item))
                            {
                                if (ord.Side == "BUY")
                                {
                                    auxDic[item].BuyQty += ord.Quantity;
                                }
                                else
                                {
                                    auxDic[item].SellQty += ord.Quantity;
                                }
                                auxDic[item].TotalQty = auxDic[item].BuyQty - auxDic[item].SellQty;
                            }
                            else
                            {
                                OrdersBySymbol aux = new OrdersBySymbol();
                                aux.Symbol = ord.Symbol;
                                aux.IDPortfolio = ord.IDPortfolio;
                                if (ord.Side == "BUY")
                                {
                                    aux.BuyQty = ord.Quantity;
                                    aux.SellQty = 0;
                                }
                                else
                                {
                                    aux.BuyQty = 0;
                                    aux.SellQty = ord.Quantity;
                                }
                                aux.TotalQty = aux.BuyQty - aux.SellQty;

                                auxDic.Add(item, aux);

                            }
                        }
                    }
                }

                foreach (OrdersBySymbol ord in auxDic.Values)
                {
                    recBySymbol.Add(ord);
                }

                return recBySymbol;
            }
        }
        
        private SortedDictionary<string, Order> InternalOrders = new SortedDictionary<string, Order>();
        private SortedDictionary<string, SortedDictionary<string, Order>> ExternalOrders = new SortedDictionary<string, SortedDictionary<string, Order>>();

        private SortedDictionary<string, double> LastPrice = new SortedDictionary<string, double>();
                
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
            string strClOrdID = message.getClOrdID().getValue();
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
                try
                {
                    SessionClOrdIDSymbol[strSession].Add(strClOrdID, strSymbol);
                }
                catch (Exception e)
                {
                    RejectDuplicateOrder(message, session);
                }
                OrderCancelReceived(message, session);
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
                ExecutionReportReceived(message, session);               
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
            //throw new System.NotImplementedException("Duplicate Order");
        }
        private void RejectDuplicateOrder(QuickFix42.OrderCancelRequest message, SessionID session)
        {
            //throw new System.NotImplementedException("Duplicate Order");
        }
        private void RejectUnkownOrder(QuickFix42.OrderCancelReplaceRequest message, SessionID session)
        {
            //throw new System.NotImplementedException("Unknown order requested to be replaced");
        }
        private void RejectUnkownOrder(QuickFix42.OrderCancelRequest message, SessionID session)
        {
            //throw new System.NotImplementedException("Unknown order requested to be cancelled");
        }
        private void RejectDontKnowTrade(QuickFix42.ExecutionReport message, SessionID session, char chrDKReason)
        {
            //throw new System.NotImplementedException("Unknown order received execution report");
        }
        private void RejectUnknownCxlRplRqt(QuickFix42.OrderCancelReject message, SessionID session)
        {
            //throw new System.NotImplementedException("Unknown Cancel/Replace received rejection");
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
                case QuickFix.Side.SELL_SHORT:
                    ReceivedOrder.Side = "SELL";
                        break;
                default:
                        break;
            }

            OrderQty qfQuantity = new OrderQty();
            message.getField(qfQuantity);
            ReceivedOrder.Quantity = Convert.ToInt32(qfQuantity.getValue());

            try
            {
                QuickFix.Price qfPrice = new Price();
                message.getField(qfPrice);
                ReceivedOrder.Price = qfPrice.getValue();
            }
            catch
            {
                ReceivedOrder.Price = 0;
            }

            ReceivedOrder.OrderID = "ORD" + getNextExtOrdID().ToString("0000");

            ReceivedOrder.IDPortfolio = message.getInt(9307);
            ReceivedOrder.IDBook = message.getInt(9305);
            ReceivedOrder.IDSection = message.getInt(9306);

            string dontMatch = "";
            try
            {
                dontMatch = message.getString(9308);
            }
            catch
            {
                dontMatch = "N";
            }
            if (dontMatch == "Y") { ReceivedOrder.DontMatch = true; } else { ReceivedOrder.DontMatch = false; }

            ReceivedOrder.OriginalOrder = message;

            lock (OrdersSync)
            {
                Orders.Add(ReceivedOrder);
                if (ExternalOrders.ContainsKey(session.toString()))
                {
                    ExternalOrders[session.toString()].Add(ReceivedOrder.ClOrdID, ReceivedOrder);
                }
                else
                {
                    SortedDictionary<string, Order> aux = new SortedDictionary<string, Order>();
                    aux.Add(ReceivedOrder.ClOrdID, ReceivedOrder);
                    ExternalOrders.Add(session.toString(), aux);
                }
                

                 
                
                
                if (!LastPrice.ContainsKey(ReceivedOrder.Symbol))
                {
                    SymConn.Instance.Subscribe(ReceivedOrder.Symbol,0);
                    LastPrice.Add(ReceivedOrder.Symbol, 0);
                }

                if (ReceivedOrder.DontMatch)
                {
                    string strAuction = "";
                    try
                    {
                        strAuction = message.getString(9304);
                    }
                    catch
                    {
                        strAuction = "N";
                    }

                    bool bAuction = false;
                    if (strAuction == "Y") { bAuction = true; }

                    SendOrderToStreet(ReceivedOrder, bAuction);
                }
            }
        }
        private void ExecutionReportReceived(QuickFix42.ExecutionReport message, SessionID session)
        {
            ExecType rptType = message.getExecType();
            ExecTransType rptTransType = message.getExecTransType();

            if (rptTransType.getValue() == ExecTransType.NEW)
            {
                if (rptType.getValue() == ExecType.FILL || rptType.getValue() == ExecType.PARTIAL_FILL)
                {
                    Order origOrder = InternalOrders[message.getClOrdID().getValue()];
                    int Quantity = Convert.ToInt32(message.getLastShares().getValue());
                    double AvgPrice = Convert.ToDouble(message.getAvgPx().getValue());
                    int TotalExec = Convert.ToInt32(message.getCumQty().getValue());

                    lock (OrdersSync)
                    {
                        for (int i = 0; i < Orders.Count; i++)
                        {
                            if (Orders[i].SessionID == origOrder.SessionID && Orders[i].ClOrdID == origOrder.ClOrdID)
                            {
                                Orders[i].Quantity = Orders[i].Quantity - Quantity;
                                Orders[i].ExecQty = TotalExec;
                                Orders[i].AvgPx = AvgPrice;
                                SendExecutionReport(Orders[i], message);

                                break;
                            }
                        }
                    }
                }
                else if (rptType.getValue() == ExecType.NEW)
                {
                    Order origOrder = InternalOrders[message.getClOrdID().getValue()];

                    lock (OrdersSync)
                    {
                        for (int i = 0; i < Orders.Count; i++)
                        {
                            if (Orders[i].OrderID == origOrder.OrderID)
                            {
                                SendExecutionReport(Orders[i], message);
                            }
                        }
                    }
                }
                else if (rptType.getValue() == ExecType.CANCELED)
                {
                    Order origOrder = InternalOrders[message.getClOrdID().getValue()];

                    lock (OrdersSync)
                    {
                        for (int i = 0; i < Orders.Count; i++)
                        {
                            if (Orders[i].OrderID == origOrder.OrderID)
                            {
                                SendCancelReport(origOrder, message);
                                Orders.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
            }
        }
        private void OrderCancelReceived(QuickFix42.OrderCancelRequest message, SessionID session)
        {
            string clOrdID = message.getClOrdID().getValue();
            string origClOrdID = message.getOrigClOrdID().getValue();

            Order curOrder = ExternalOrders[session.toString()][origClOrdID];
            curOrder.OrigClOrdID = curOrder.ClOrdID;
            curOrder.ClOrdID = clOrdID;

            CancelOrder(curOrder);

            //CancelOrderToStreet(curOrder);

        }

        public void MatchOrders(int IdPortfolio, string Symbol, bool auction, bool onlyMatch)
        {                      
            lock (OrdersSync)
            {
                List<Order> BuyOrders = new List<Order>();
                List<Order> SellOrders = new List<Order>();

                double price = LastPrice[Symbol];

                foreach (Order ord in Orders)
                {
                    if (ord.IDPortfolio == IdPortfolio)
                    {
                        if (ord.Symbol == Symbol)
                        {
                            switch (ord.Side)
                            {
                                case "BUY":

                                    while (SellOrders.Count > 0 && ord.Quantity > 0)
                                    {
                                        int matchQuantity = Math.Min(SellOrders[0].Quantity, ord.Quantity);

                                        ord.Quantity = ord.Quantity - matchQuantity;
                                        SellOrders[0].Quantity = SellOrders[0].Quantity - matchQuantity;

                                        //TODO: Definir o preco de execução;                                   
                                        GenerateTrade(ord, SellOrders[0], matchQuantity, price);

                                        if (SellOrders[0].Quantity == 0) { SellOrders.RemoveAt(0); }
                                    }

                                    if (ord.Quantity > 0)
                                    {
                                        BuyOrders.Add(ord);
                                    }

                                    break;
                                case "SELL":

                                    while (BuyOrders.Count > 0 && ord.Quantity > 0)
                                    {
                                        int matchQuantity = Math.Min(BuyOrders[0].Quantity, ord.Quantity);

                                        ord.Quantity = ord.Quantity - matchQuantity;
                                        BuyOrders[0].Quantity = BuyOrders[0].Quantity - matchQuantity;

                                        //TODO: Definir o preco de execução
                                        GenerateTrade(BuyOrders[0], ord, matchQuantity, price);

                                        if (BuyOrders[0].Quantity == 0) { BuyOrders.RemoveAt(0); }
                                    }

                                    if (ord.Quantity > 0)
                                    {
                                        SellOrders.Add(ord);
                                    }

                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }

                if (!onlyMatch)
                {

                    if (BuyOrders.Count > 0 && SellOrders.Count == 0)
                    {
                        foreach (Order ord in BuyOrders)
                        {
                            SendOrderToStreet(ord, auction);
                        }
                    }
                    else if (BuyOrders.Count == 0 && SellOrders.Count > 0)
                    {
                        foreach (Order ord in SellOrders)
                        {
                            SendOrderToStreet(ord, auction);
                        }
                    }
                    else if (BuyOrders.Count == 0 && SellOrders.Count == 0)
                    {
                    }
                    else
                    {
                        throw new System.NotImplementedException("Inconsistent match!");
                    }
                }
            }
        }
        private void GenerateTrade(Order BuyOrder, Order SellOrder, int Quantity, double Price)
        {
            SendExecutionReport(BuyOrder, Quantity, Price,GetAccountNumber(BuyOrder.IDPortfolio, BuyOrder.IDSection,true));
            SendExecutionReport(SellOrder, Quantity, Price, GetAccountNumber(SellOrder.IDPortfolio, SellOrder.IDSection,true));
        }
        private void SendOrderToStreet(Order orderToSend, bool auction)
        {
            QuickFix42.NewOrderSingle toStreetOrder = new QuickFix42.NewOrderSingle();

            ClOrdID qfClOrdID = new ClOrdID("QNF" + getNextIntOrdID().ToString("0000"));
            HandlInst qfHandInst = new HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE);
            Symbol qfSymbol = new Symbol(orderToSend.Symbol);
            Side qfSide = new Side(orderToSend.OriginalOrder.getSide().getValue());
            TransactTime qfTransactTime = new TransactTime(DateTime.Now);
                                             
            OrdType qfOrdType = new OrdType(OrdType.MARKET);
            Price qfPrice = new Price(0.0);

            if (orderToSend.DontMatch && orderToSend.Price > 0)
            {
                qfOrdType = new OrdType(OrdType.LIMIT);
                qfPrice = new Price(orderToSend.Price);
            }    
            

            OrderQty qfQuantity = new OrderQty(Convert.ToDouble(orderToSend.Quantity));

            toStreetOrder.set(qfClOrdID);
            toStreetOrder.set(qfHandInst);
            toStreetOrder.set(qfSymbol);
            toStreetOrder.set(qfSide);
            toStreetOrder.set(qfTransactTime);
            toStreetOrder.set(qfOrdType);
            toStreetOrder.set(qfQuantity);

            if (orderToSend.DontMatch && orderToSend.Price > 0)
            {
                toStreetOrder.set(qfPrice);
            }    
    
            if (auction) { toStreetOrder.setString(9304, "Y"); } else { toStreetOrder.setString(9304, "N"); }
            toStreetOrder.setInt(9307, orderToSend.IDPortfolio);

            orderToSend.IntCLOrd = qfClOrdID.getValue();
            InternalOrders.Add(qfClOrdID.getValue(), orderToSend);
            if (SessionClOrdIDSymbol.ContainsKey(GetStreetSession().toString()))
            {
                SessionClOrdIDSymbol[GetStreetSession().toString()].Add(qfClOrdID.getValue(), qfSymbol.getValue());
            }
            else
            {
                SortedDictionary<string, string> aux = new SortedDictionary<string, string>();
                aux.Add(qfClOrdID.getValue(), qfSymbol.getValue());
                SessionClOrdIDSymbol.Add(GetStreetSession().toString(), aux);
            }

            Session.sendToTarget(toStreetOrder, GetStreetSession());
        }
        private void SendExecutionReport(Order filledOrder, int fills, double Price, int AccNum)
        {
            int OrigQuantity = Convert.ToInt32(filledOrder.OriginalOrder.getOrderQty().getValue());

            QuickFix42.ExecutionReport execRep = new QuickFix42.ExecutionReport();

            OrderID qfOrderID = new OrderID(filledOrder.OrderID);
            ClOrdID qfClOrdID1 = new ClOrdID(filledOrder.ClOrdID);
            ClOrdID qfClOrdID2 = new ClOrdID("Q" + filledOrder.ClOrdID);
            ExecID qfExecID = new ExecID("QNR" + getNextExecID().ToString("0000"));
            ExecTransType qfExecTransType = new ExecTransType(ExecTransType.NEW);
            ExecType qfExecType = new ExecType(ExecType.PARTIAL_FILL);
            OrdStatus qfOrdStatus = new OrdStatus(OrdStatus.PARTIALLY_FILLED);
            Symbol qfSymbol = new Symbol(filledOrder.Symbol);
            Side qfSide = filledOrder.OriginalOrder.getSide();
            OrderQty qfOrdQty = new OrderQty(OrigQuantity);
            LeavesQty qfLeavesQty = new LeavesQty(Convert.ToDouble(filledOrder.Quantity));
            CumQty qfCumQty = new CumQty(Convert.ToDouble(OrigQuantity - filledOrder.Quantity));
            AvgPx qfAvgPx = new AvgPx(0);
            LastShares qfLastShares = new LastShares(Convert.ToDouble(fills));
            LastPx qfLastPx = new LastPx(Price);
            Account qfAccount = new Account(AccNum.ToString());
            TargetSubID qfTargetSubID = new TargetSubID("QNF");
            TransactTime qfTransactTime = new TransactTime(DateTime.Now);

            execRep.getHeader().setField(qfTargetSubID);
            execRep.set(qfOrderID);
            execRep.set(qfClOrdID1);
            execRep.set(qfExecID);
            execRep.set(qfExecTransType);
            execRep.set(qfExecType);
            execRep.set(qfOrdStatus);
            execRep.set(qfSymbol);
            execRep.set(qfSide);
            execRep.set(qfOrdQty);
            execRep.set(qfLeavesQty);
            execRep.set(qfCumQty);
            execRep.set(qfAvgPx);
            execRep.set(qfLastShares);
            execRep.set(qfLastPx);
            execRep.set(qfAccount);
            execRep.set(qfTransactTime);
            execRep.setInt(9305, filledOrder.IDBook);
            execRep.setInt(9306, filledOrder.IDSection);

                

            Session.sendToTarget(execRep, Sessions[filledOrder.SessionID]);

            execRep.set(qfClOrdID2);
            Session.sendToTarget(execRep, DropCopySession);

        }
        private void SendExecutionReport(Order filledOrder, QuickFix42.ExecutionReport report)
        {
            Account qfAccount = new Account(GetAccountNumber(filledOrder.IDPortfolio, filledOrder.IDSection,false).ToString());
            TargetSubID qfTargetSubID = new TargetSubID("QNF");
            ClOrdID qfClOrdID1 = new ClOrdID(filledOrder.ClOrdID);
            ClOrdID qfClOrdID2 = new ClOrdID(GetBrokerPrefix(filledOrder.IDSection) + filledOrder.ClOrdID);
            

            if (!report.isSetField(1)) { report.set(qfAccount); }
            if (!report.isSetField(60)) { report.set(new TransactTime(DateTime.Now)); }

            report.getHeader().setField(qfTargetSubID);
            report.setInt(9305, filledOrder.IDBook);
            report.setInt(9306, filledOrder.IDSection);

            report.set(qfClOrdID1);
            Session.sendToTarget(report, Sessions[filledOrder.SessionID]);

            if (report.getExecType().getValue() == ExecType.FILL || report.getExecType().getValue() == ExecType.PARTIAL_FILL)
            {
                report.set(qfClOrdID2);
                Session.sendToTarget(report, DropCopySession);
            }
        }
        private void SendCancelReport(Order cancelledOrder)
        {

        }
        private void SendCancelReport(Order cancelledOrder, QuickFix42.ExecutionReport report)
        {
            Account qfAccount = new Account(GetAccountNumber(cancelledOrder.IDPortfolio, cancelledOrder.IDSection, false).ToString());
            TargetSubID qfTargetSubID = new TargetSubID("QNF");
            ClOrdID qfClOrdID1 = new ClOrdID(cancelledOrder.ClOrdID);
            OrigClOrdID qfOrigClOrdId = new OrigClOrdID(cancelledOrder.OrigClOrdID);

            if (!report.isSetField(1)) { report.set(qfAccount); }
            if (!report.isSetField(60)) { report.set(new TransactTime(DateTime.Now)); }

            report.getHeader().setField(qfTargetSubID);
            report.setInt(9305, cancelledOrder.IDBook);
            report.setInt(9306, cancelledOrder.IDSection);

            report.set(qfClOrdID1);
            if (qfOrigClOrdId.getValue() != null) report.set(qfOrigClOrdId);
            Session.sendToTarget(report, Sessions[cancelledOrder.SessionID]);
        }
        
        public void CancelOrders(int IdPortfolio, string Symbol)
        {
            lock (OrdersSync)
            {
                int i = 0;

                while (i < Orders.Count)
                {
                    if (Orders[i].IDPortfolio == IdPortfolio)
                    {
                        if (Orders[i].Symbol == Symbol)
                        {

                            bool SentToStreet = CancelOrder(Orders[i]);                             

                            if (!SentToStreet)
                            {                                
                                i--;
                            }                            
                        }
                    }

                    i++;
                }
            }
        }
        public bool CancelOrder(Order orderToCancel)
        {
            bool SentToStreet = false;

            foreach (Order ord in InternalOrders.Values)
            {
                if (ord.OrderID == orderToCancel.OrderID)
                {
                    SentToStreet = true;
                    break;
                }
            }

            if (!SentToStreet)
            {
                int i = 0;
                while (i < Orders.Count)
                {
                    if (Orders[i].OrderID == orderToCancel.OrderID)
                    {
                        Orders.RemoveAt(i);                        
                    }
                }
            }
            else
            {
                CancelOrderToStreet(orderToCancel);
            }

            return SentToStreet;
        }
    
        public void CancelOrderToStreet(Order orderToCancel)
        {
            OrigClOrdID qfOrigClOrdID = new OrigClOrdID(orderToCancel.IntCLOrd);
            ClOrdID qfClOrdID = new ClOrdID("QNF" + getNextIntOrdID().ToString("0000"));
            Symbol qfSymbol = new Symbol(orderToCancel.Symbol);
            Side qfSide = new Side(orderToCancel.OriginalOrder.getSide().getValue());
            TransactTime qfTransactTime = new TransactTime(DateTime.Now);
            OrderQty qfOrderQty = new OrderQty(Math.Abs(orderToCancel.Quantity));

            QuickFix42.OrderCancelRequest cancelMessage = new QuickFix42.OrderCancelRequest(qfOrigClOrdID,
                                                                                            qfClOrdID,
                                                                                            qfSymbol,
                                                                                            qfSide,
                                                                                            qfTransactTime);

            cancelMessage.set(qfOrderQty);

            orderToCancel.IntOrigClOrd = orderToCancel.IntCLOrd;
            orderToCancel.IntCLOrd = qfClOrdID.getValue();
            InternalOrders.Add(qfClOrdID.getValue(), orderToCancel);
            SessionClOrdIDSymbol[GetStreetSession().toString()].Add(qfClOrdID.getValue(), qfSymbol.getValue());

            Session.sendToTarget(cancelMessage, GetStreetSession());            
        }

        private int GetAccountNumber(int IdPortfolio, int IdSection, bool bInternal)
        {
            int account = 0;

            if (bInternal)
            {
                switch (IdSection)
                {
                    case 54:
                    case 13:
                    case 68:
                    case 162: //Strong Open
                    case 186: //JM Banks
                    case 188: //JM Shopping
                    case 189: //JM Home Builders
                    case 190: //JM Pulp and Paper
                    case 191: //JM Steel
                    case 193: //JM Transportation
                    case 221: //Martin Pescador
                    case 223: //Jericho
                    case 224: //Katucha
                    case 225: //Momentum LS
                    case 226: //Momentum 40
                    case 227: //Momentum 4
                    case 228: //Momentum 1
                    case 231: //Tomahawk
                        if (IdPortfolio == NEnuns.QuantPortfolios.MileHigh)
                        {
                            account = 1198; //QUANT MH MASTER
                        }
                        else if (IdPortfolio == NEnuns.QuantPortfolios.NestFund)
                        {
                            account = 1200; //QUANT NEST FUND
                        }
                        else if (IdPortfolio == NEnuns.QuantPortfolios.Quant)
                        {
                            account = 1289; //NEST QUANT
                        } 
                        break;
                    case 55:
                        if (IdPortfolio == NEnuns.QuantPortfolios.MileHigh)
                        {
                            account = 1199; //QUANT MH OVERSEAS
                        }
                        else if (IdPortfolio == NEnuns.QuantPortfolios.NestFund)
                        {
                            account = 1201; //QUANT NF Prime
                        } 
                        
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (IdSection)
                {
                    case 54: //Momentum BZ
                    case 68: //Value BZ
                    case 162: //Strong Open
                    case 186: //JM Banks
                    case 188: //JM Shopping
                    case 189: //JM Home Builders
                    case 190: //JM Pulp and Paper
                    case 191: //JM Steel
                    case 193: //JM Transportation
                    case 221: //Martin Pescador
                    case 223: //Jericho
                    case 224: //Katucha
                    case 225: //Momentum LS
                    case 226: //Momentum 40
                    case 227: //Momentum 4
                    case 228: //Momentum 1
                    case 231: //Tomahawk
                        if (IdPortfolio == NEnuns.QuantPortfolios.MileHigh)
                        {
                            //account = 162298; //LINK MILE HIGH
                            account = 82229; //XP MILE HIGH
                        }
                        else if (IdPortfolio == NEnuns.QuantPortfolios.NestFund)
                        {
                            //account = 98905; //LINK NEST FUND
                            account = 182002; //XP NEST FUND
                        }
                        else if (IdPortfolio == NEnuns.QuantPortfolios.Broker)
                        {
                            //account = 96422; //Link Broker
                            account = 9000068; //XP Broker
                        }
                        else if (IdPortfolio == NEnuns.QuantPortfolios.Quant)
                        {
                            //account = 367434; //Link BVSP Quant
                            account = 8007686; //XP BVSP Quant
                        }
                        break;
                    case 13:
                    case 173:
                    case 184:
                    case 185:
                        if (IdPortfolio == NEnuns.QuantPortfolios.Broker)
                        {
                            //account = 9642; //Link BMF BROKER
                            account = 999144; //XP BMF BROKER
                        }
                        else if (IdPortfolio == 18)
                        {
                            //account = 36743; //Link BMF Quant
                            account = 800768; //XP BMF Quant
                        }
                        break;
                    case 55:
                        account = 0;
                        break;
                    default:
                        break;
                }
            }

            return account;
        }
        private string GetBrokerPrefix(int IdSection)
        {
            string prefix = "X";

            switch (IdSection)
            {
                case 54:
                case 68:
                case 162:
                case 186:
                case 188:
                case 189:
                case 190:
                case 191:
                case 193:
                case 221: //Martin Pescador
                case 223: //Jericho
                case 224: //Katucha
                case 225: //Momentum LS
                case 226: //Momentum 40
                case 227: //Momentum 4
                case 228: //Momentum 1
                case 231: //Tomahawk
                    //prefix = "L"; //LNKBOV
                    prefix = "V"; //XPBOV
                    break;
                case 13:
                case 173:
                case 184:
                case 185:
                    //prefix = "F"; //LNKBMF
                    prefix = "W"; //XPBMF
                    break;
                case 55:
                    //prefix = "R"; //TWSE
                    prefix = "K"; //TRBK
                    break;
                default:
                    break;
            }            

            return prefix;
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
            SymConn.Instance.Dispose();
        }
    }
}
