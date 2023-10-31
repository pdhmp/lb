using System;
using System.Collections;
using System.Collections.Generic;
using QuickFix;

namespace NestFixRouter
{
    internal class FixRouter 
    {
        private frmRouter _oRouterWindow;
        private Inbound _oInboundConnector;
        private Outbound _oOutboundConnector;
        
        public string _sLogPath = @"c:\FIXLOG\NestRouter\Log\" + DateTime.Today.ToString("yyyyMMdd");
        private string _sStorePath = @"c:\FIXLOG\NestRouter\Store\";
        private Dictionary<string, string> _dcMapping = new Dictionary<string, string>(); // converts ClOrdId prefix to client's TargetCompID
        private Dictionary<string, bool> _dcSessionStatus = new Dictionary<string, bool>(); // true = enabled; false = disabled
        private List<string> _lInboundConnection = new List<string>();
        private List<string> _lOutboundConnection = new List<string>();
        private Queue<MessageWithSession> _dcMsgBackLog = new Queue<MessageWithSession>();
        
        public FixRouter(frmRouter routerWindow)
        {
            if (!System.IO.File.Exists(_sLogPath)) { System.IO.Directory.CreateDirectory(_sLogPath); }
            if (!System.IO.File.Exists(_sLogPath + "\\Inbound\\")) { System.IO.Directory.CreateDirectory(_sLogPath + "\\Inbound\\"); }
            if (!System.IO.File.Exists(_sLogPath + "\\Outbound\\")) { System.IO.Directory.CreateDirectory(_sLogPath + "\\Outbound\\"); }

            _oRouterWindow = routerWindow;
            _oInboundConnector = new Inbound(this, _sLogPath + "\\Inbound\\", _sStorePath);
            _oOutboundConnector = new Outbound(this, _sLogPath + "\\Outbound\\", _sStorePath);
        }

        internal void Log(string message)
        {
            _oRouterWindow.LogMessage(message);
        }

        internal List<string> AcceptorStart()
        {
            _lInboundConnection = _oInboundConnector.AcceptorStart();
            foreach (string curConnection in _lInboundConnection)
                _dcSessionStatus[curConnection] = true;

            return _lInboundConnection;
        }
        internal void AcceptorStop()
        {
            _oInboundConnector.AcceptorStop();
            foreach (string curConnection in _lInboundConnection)
                _dcSessionStatus[curConnection] = false;
        }
        internal void AcceptorLogOn(SessionID sessionID)
        {
            string sTarget = sessionID.getTargetCompID();
            string sTargetShortname = sTarget.Length > 2 ? sTarget.Substring(sTarget.Length - 3, 3) : "";
            if (!_dcMapping.ContainsKey(sTargetShortname))
                lock (_dcMapping)
                    _dcMapping.Add(sTargetShortname, sTarget);

            _oRouterWindow.UpdateStatus(sTarget, true);
        }
        internal void AcceptorLogOut(SessionID sessionID)
        {
            string sTarget = sessionID.getTargetCompID();
            string sTargetShortname = sTarget.Length > 2 ? sTarget.Substring(sTarget.Length - 3, 3) : "";
            if (_dcMapping.ContainsKey(sTargetShortname))
                lock (_dcMapping)
                    _dcMapping.Remove(sTargetShortname);

            _oRouterWindow.UpdateStatus(sTarget, false);

        }
        internal void EnableInbound(string session)
        {
            _dcSessionStatus[session] = true;
            _oRouterWindow.UpdateSession(session, true);
        }
        internal void DisableInbound(string session)
        {
            _dcSessionStatus[session] = false;
            _oRouterWindow.UpdateSession(session, false);
        }

        internal List<string> InitiatorStart()
        {
            _lOutboundConnection = _oOutboundConnector.InitiatorStart();
            foreach (string curConnection in _lOutboundConnection)
                _dcSessionStatus[curConnection] = true;

            return _lOutboundConnection;
        }
        internal void InitiatorStop()
        {
            _oOutboundConnector.InitiatorStop();
            foreach (string curConnection in _lOutboundConnection)
                _dcSessionStatus[curConnection] = false;
       
        }
        internal void InitiatorLogOn(SessionID sessionID)
        {
            string sTarget = sessionID.getTargetCompID();
            //string sTargetShortname = sTarget.Length > 2 ? sTarget.Substring(sTarget.Length - 3, 3) : "";
            //if (!_dcMapping.ContainsKey(sTargetShortname))
            //    lock (_dcMapping)
            //        _dcMapping.Add(sTargetShortname, sTarget);

            _oRouterWindow.UpdateStatus(sTarget, true);
        }
        internal void InitiatorLogOut(SessionID sessionID)
        {
            string sTarget = sessionID.getTargetCompID();
            //string sTargetShortname = sTarget.Length > 2 ? sTarget.Substring(sTarget.Length - 3, 3) : "";
            //if (_dcMapping.ContainsKey(sTargetShortname))
            //    lock (_dcMapping)
            //        _dcMapping.Remove(sTargetShortname);

            _oRouterWindow.UpdateStatus(sTarget, false);
        }
        internal void EnableOutbound(string session)
        {
            _dcSessionStatus[session] = true;
            _oRouterWindow.UpdateSession(session, true);
        }
        internal void DisableOutbound(string session)
        {
            _dcSessionStatus[session] = false;
            _oRouterWindow.UpdateSession(session, false);
        }

        internal void MessageFromClient(Message message, SessionID sessionID)
        {
            string sSession = sessionID.getTargetCompID();
            // ------------------------------------------------------------------------------------------
            // _oRouterWindow.LogMessage("");
            // _oRouterWindow.LogMessage("[1] Message arrived from client " + sSession);
            // _oRouterWindow.LogMessage("[2] " + message.ToString());
            // ------------------------------------------------------------------- Comentado Edson ------

            bool bEnabled = false;
            if (_dcSessionStatus.TryGetValue(sSession, out bEnabled) && (bEnabled)) // client enabled
            {
                string sTarget = message.getHeader().isSetField(DeliverToCompID.FIELD) ? message.getHeader().getField(DeliverToCompID.FIELD) : "";

                bool bConnected = false;
                if ((sTarget.Length > 0) && (_oOutboundConnector.Connected.TryGetValue(sTarget, out bConnected)) && (bConnected))
                {
                    if ((_dcSessionStatus.TryGetValue(sTarget, out bEnabled) && (bEnabled)))
                    {
                        message.getHeader().removeField(OnBehalfOfCompID.FIELD);
                        message.getHeader().removeField(OnBehalfOfSubID.FIELD);
                        message.getHeader().removeField(DeliverToCompID.FIELD);

                        //_oRouterWindow.LogMessage("[3] Message forwarded to server " + sTarget); // Comentado Edson
                        _oOutboundConnector.SendMessage(message, sTarget);
                    }
                    else
                    {
                        //_oRouterWindow.LogMessage("[3] Server " + sTarget + " is disabled. Reject will be sent to client " + sSession);
                        //QuickFix42.ExecutionReport oCancel = _CreateRejectMessage(message, "Order rejected. Message blocked by router. Destination " + sTarget + " is disabled.");
                        //_SendRejectMessage(oCancel, sSession);

                        _oRouterWindow.LogMessage("[3] Server " + sTarget + " is disabled. Reject will be sent to client " + sSession);
                        if (sessionID.getBeginString().Contains("4.4"))
                            _SendRejectMessage(_CreateRejectMessage44(message, "Order rejected. Message blocked by router. Destination " + sTarget + " is disabled."), sSession);
                        else
                            _SendRejectMessage(_CreateRejectMessage(message, "Order rejected. Message blocked by router. Destination " + sTarget + " is disabled."), sSession);
                    }
                }
                else
                {
                    //_oRouterWindow.LogMessage("[3] Server " + sTarget + " is disconnected. Reject will be sent to client " + sSession);
                    //QuickFix42.ExecutionReport oCancel = _CreateRejectMessage(message, "Order rejected. Destination " + sTarget + " not ready.");
                    //_SendRejectMessage(oCancel, sSession);

                    _oRouterWindow.LogMessage("[3] Server " + sTarget + " is disconnected. Reject will be sent to client " + sSession);
                    if (sessionID.getBeginString().Contains("4.4"))
                        _SendRejectMessage(_CreateRejectMessage44(message, "Order rejected. Destination " + sTarget + " not ready."), sSession);
                    else
                        _SendRejectMessage(_CreateRejectMessage(message, "Order rejected. Destination " + sTarget + " not ready."), sSession);
                }
            }
            else
            {
                // _oRouterWindow.LogMessage("[3] Message blocked by router. Session disabled or doesn't exist.");
                // QuickFix42.ExecutionReport oCancel = _CreateRejectMessage(message, "Order rejected. Message blocked by router. Session " + sSession + " is disabled or doesn't exist.");
                // _SendRejectMessage(oCancel, sSession);

                _oRouterWindow.LogMessage("[3] Message blocked by router. Session disabled or doesn't exist.");
                if (sessionID.getBeginString().Contains("4.4"))
                    _SendRejectMessage(_CreateRejectMessage(message, "Order rejected. Message blocked by router. Session " + sSession + " is disabled or doesn't exist."), sSession);
                else
                    _SendRejectMessage(_CreateRejectMessage(message, "Order rejected. Message blocked by router. Session " + sSession + " is disabled or doesn't exist."), sSession);
            }
        }
        internal void MessageFromServer(Message message, SessionID sessionID)
        {
            while (_dcMsgBackLog.Count > 0)
            {
                //_oRouterWindow.LogMessage("[3] BACKLOG Message forwarded to client" + sTargetShortname);

                MessageWithSession curMsg = _dcMsgBackLog.Dequeue();
                if (!TrySendMessage(curMsg, true))
                {
                    _dcMsgBackLog.Enqueue(curMsg);
                }
            }

            MessageWithSession curMessageWithSession = new MessageWithSession();
            curMessageWithSession.curSessionID = sessionID;
            curMessageWithSession.curMessage = message;

            if (!TrySendMessage(curMessageWithSession, false))
            {
                _dcMsgBackLog.Enqueue(curMessageWithSession);
            }
        }

        private bool TrySendMessage(MessageWithSession curMessageWithSession, bool FromBackLog)
        {
            string sSession = curMessageWithSession.curSessionID.getTargetCompID();

            // ----------------------------------------------------------------------------------
            // _oRouterWindow.LogMessage("");
            // _oRouterWindow.LogMessage("[1] Message arrived from server " + sSession);
            // _oRouterWindow.LogMessage("[2] " + message.ToString());
            // -------------------------------------------------------------------------- Edson 

            bool bEnabled = false;
            if (_dcSessionStatus.TryGetValue(sSession, out bEnabled) && (bEnabled))
            {
                string sTargetShortname = curMessageWithSession.curMessage.getField(11).Length > 2 ? curMessageWithSession.curMessage.getField(11).Substring(0, 3) : "";
                string sTarget = _dcMapping.ContainsKey(sTargetShortname) ? _dcMapping[sTargetShortname] : "";

                bool bConnected = false;
                if (sTarget.Length > 0)
                {
                    if ((_oInboundConnector.Connected.TryGetValue(sTarget, out bConnected)) && (bConnected))
                    {
                        _oInboundConnector.SendMessage(curMessageWithSession.curMessage, sTarget);
                        if (FromBackLog) _oRouterWindow.LogMessage("[3] BACKLOG Message forwarded to client" + sTargetShortname);
                    }
                    else
                    {
                        if (!FromBackLog)
                        {
                            _oRouterWindow.LogMessage("[3] Client " + sTarget + " is unavailable");
                            _oRouterWindow.LogMessage(curMessageWithSession.curMessage.ToString());
                        }

                        return false;
                    }
                }
                else
                {
                    if(!FromBackLog) _oRouterWindow.LogMessage("[3] Client couldn't be identified by the ClOrdID prefix : " + sTargetShortname + " - Message : " + curMessageWithSession.curMessage.ToString());
                    return false;
                }
            }
            else
            {
                if (!FromBackLog) _oRouterWindow.LogMessage("[3] Message blocked by router. Session disabled or doesn't exist.");
                return false;
            }

            return true;
        }

        private QuickFix42.ExecutionReport _CreateRejectMessage(Message message, string reason)
        {
            QuickFix42.ExecutionReport oCancel = null;

            ClOrdID oClOrdID = new ClOrdID("Error_ClOrdID");
            Symbol oSymbol = new Symbol("Error_Symbol");
            Side oSide = new Side(Side.AS_DEFINED);
            OrderQty oOrderQty = new OrderQty(0);
            Price oPrice = new Price(0);

            Type oMessageType = message.GetType();
            if (oMessageType == typeof(QuickFix42.NewOrderSingle))
            {
                QuickFix42.NewOrderSingle oNewOrderSingle = (QuickFix42.NewOrderSingle)message;
                oClOrdID = oNewOrderSingle.getClOrdID();
                oSymbol = oNewOrderSingle.isSetSymbol() ? oNewOrderSingle.getSymbol() : oSymbol;
                oSide = oNewOrderSingle.isSetSide() ? oNewOrderSingle.getSide() : oSide;
                oOrderQty = oNewOrderSingle.isSetOrderQty() ? oNewOrderSingle.getOrderQty() : oOrderQty;
                oPrice = oNewOrderSingle.isSetPrice() ? oNewOrderSingle.getPrice() : oPrice;
            }
            else if (oMessageType == typeof(QuickFix42.OrderCancelReplaceRequest))
            {
                QuickFix42.OrderCancelReplaceRequest oOrderCancelReplaceRequest = (QuickFix42.OrderCancelReplaceRequest)message;
                oClOrdID = oOrderCancelReplaceRequest.getClOrdID();
                oSymbol = oOrderCancelReplaceRequest.isSetSymbol() ? oOrderCancelReplaceRequest.getSymbol() : oSymbol;
                oSide = oOrderCancelReplaceRequest.isSetSide() ? oOrderCancelReplaceRequest.getSide() : oSide;
                oOrderQty = oOrderCancelReplaceRequest.isSetOrderQty() ? oOrderCancelReplaceRequest.getOrderQty() : oOrderQty;
                oPrice = oOrderCancelReplaceRequest.isSetPrice() ? oOrderCancelReplaceRequest.getPrice() : oPrice;
            }
            else if (oMessageType == typeof(QuickFix42.OrderCancelRequest))
            {
                QuickFix42.OrderCancelRequest oOrderCancelRequest = (QuickFix42.OrderCancelRequest)message;
                oClOrdID = oOrderCancelRequest.getClOrdID();
                oSymbol = oOrderCancelRequest.isSetSymbol() ? oOrderCancelRequest.getSymbol() : oSymbol;
                oSide = oOrderCancelRequest.isSetSide() ? oOrderCancelRequest.getSide() : oSide;
                oOrderQty = oOrderCancelRequest.getOrderQty();
                oPrice = new Price(0);
            }
            else
            {
                oMessageType = null;
            }

            if (oMessageType != null)
            {
                oCancel = new QuickFix42.ExecutionReport(new OrderID(oClOrdID.ToString()),
                                                         new ExecID("rej" + oClOrdID.ToString()),
                                                         new ExecTransType(ExecTransType.CANCEL),
                                                         new ExecType(ExecType.REJECTED),
                                                         new OrdStatus(OrdStatus.REJECTED),
                                                         oSymbol,
                                                         oSide,
                                                         new LeavesQty(0),
                                                         new CumQty(0),
                                                         new AvgPx(0));
                oCancel.set(oClOrdID);
                oCancel.set(oOrderQty);
                oCancel.set(oPrice);
                oCancel.set(new Text(reason));
            }

            return oCancel;
        }

        private QuickFix44.ExecutionReport _CreateRejectMessage44(Message message, string reason)
        {
            QuickFix44.ExecutionReport oCancel = null;

            ClOrdID oClOrdID = new ClOrdID("Error_ClOrdID");
            Symbol oSymbol = new Symbol("Error_Symbol");
            Side oSide = new Side(Side.AS_DEFINED);
            OrderQty oOrderQty = new OrderQty(0);
            Price oPrice = new Price(0);

            Type oMessageType = message.GetType();
            if (oMessageType == typeof(QuickFix44.NewOrderSingle))
            {
                QuickFix44.NewOrderSingle oNewOrderSingle = (QuickFix44.NewOrderSingle)message;
                oClOrdID  = oNewOrderSingle.getClOrdID();
                oSymbol   = oNewOrderSingle.isSetSymbol()   ? oNewOrderSingle.getSymbol()   : oSymbol;
                oSide     = oNewOrderSingle.isSetSide()     ? oNewOrderSingle.getSide()     : oSide;
                oOrderQty = oNewOrderSingle.isSetOrderQty() ? oNewOrderSingle.getOrderQty() : oOrderQty;
                oPrice    = oNewOrderSingle.isSetPrice()    ? oNewOrderSingle.getPrice()    : oPrice;
            }
            else if (oMessageType == typeof(QuickFix44.OrderCancelReplaceRequest))
            {
                QuickFix44.OrderCancelReplaceRequest oOrderCancelReplaceRequest = (QuickFix44.OrderCancelReplaceRequest)message;
                oClOrdID  = oOrderCancelReplaceRequest.getClOrdID();
                oSymbol   = oOrderCancelReplaceRequest.isSetSymbol()   ? oOrderCancelReplaceRequest.getSymbol()   : oSymbol;
                oSide     = oOrderCancelReplaceRequest.isSetSide()     ? oOrderCancelReplaceRequest.getSide()     : oSide;
                oOrderQty = oOrderCancelReplaceRequest.isSetOrderQty() ? oOrderCancelReplaceRequest.getOrderQty() : oOrderQty;
                oPrice    = oOrderCancelReplaceRequest.isSetPrice()    ? oOrderCancelReplaceRequest.getPrice()    : oPrice;
            }
            else if (oMessageType == typeof(QuickFix44.OrderCancelRequest))
            {
                QuickFix44.OrderCancelRequest oOrderCancelRequest = (QuickFix44.OrderCancelRequest)message;
                oClOrdID  = oOrderCancelRequest.getClOrdID();
                oSymbol   = oOrderCancelRequest.isSetSymbol() ? oOrderCancelRequest.getSymbol() : oSymbol;
                oSide     = oOrderCancelRequest.isSetSide()   ? oOrderCancelRequest.getSide()   : oSide;
                oOrderQty = oOrderCancelRequest.getOrderQty();
                oPrice    = new Price(0);
            }
            else
            {
                oMessageType = null;
            }

            if (oMessageType != null)
            {
                oCancel = new QuickFix44.ExecutionReport(new OrderID(oClOrdID.ToString()),
                                                         new ExecID("rej" + oClOrdID.ToString()),
                                                         new ExecType(ExecType.REJECTED),
                                                         new OrdStatus(OrdStatus.REJECTED),
                                                         oSide,
                                                         new LeavesQty(0),
                                                         new CumQty(0),
                                                         new AvgPx(0));
                oCancel.set(oSymbol);
                oCancel.set(oClOrdID);
                oCancel.set(oOrderQty);
                oCancel.set(oPrice);
                oCancel.set(new Text(reason));
            }

            return oCancel;
        }

        private void _SendRejectMessage(Message message, string session)
        {
            if (message != null)
            {
                _oRouterWindow.LogMessage("[4] " + message.ToString());
                _oInboundConnector.SendMessage(message, session);
            }
            else
            {
                _oRouterWindow.LogMessage("[4] failed to identify type of message received. Reject was not replied to sender.");
            }
        }

        private class MessageWithSession
        {
            public Message curMessage;
            public SessionID curSessionID;
        }
    }
}
