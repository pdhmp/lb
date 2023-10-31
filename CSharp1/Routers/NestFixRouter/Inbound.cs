using System;
using System.Collections.Generic;
using QuickFix;

namespace NestFixRouter
{
    internal class Inbound : Application
    {

        private FixRouter _oRouter;
        private SocketAcceptor _oAcceptor;
        private string _sLogPath;
        private string _sStorePath;
        private List<string> _lConnections;
        private Dictionary<string, SessionID> _dcSesssionID;

        internal Dictionary<string, bool> Connected { get; private set; }

        public Inbound(FixRouter router, string logpath, string storepath)
        {
            _oRouter = router;
            _sLogPath = logpath;
            _sStorePath = storepath;
            Connected = new Dictionary<string, bool>();
        }

        internal List<string> AcceptorStart()
        {
            try
            {
                Application application = this;
                SessionSettings settings = new SessionSettings(@"\\NESTSRV03\NESTSoft\Fix\Configs\Router_Acceptor.cfg");
                FileStoreFactory storeFactory = new FileStoreFactory(_sStorePath);
                FileLogFactory logFactory = new FileLogFactory(_sLogPath);
                MessageFactory messageFactory = new DefaultMessageFactory();
                _oAcceptor = new SocketAcceptor(application, storeFactory, settings, logFactory, messageFactory);

                _oAcceptor.start();
                _lConnections = new List<string>(); 
                _dcSesssionID = new Dictionary<string, SessionID>();
                foreach (SessionID curSessionID in _oAcceptor.getSessions())
                {
                    _dcSesssionID.Add(curSessionID.getTargetCompID(), curSessionID);
                    _lConnections.Add(curSessionID.getTargetCompID());
                }

                _oRouter.Log(">> Acceptor started. <<");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return _lConnections;
        }

        internal bool AcceptorStop()
        {
            bool bReturn = false;
            try
            {
                if (_oAcceptor != null)
                {
                    _oAcceptor.stop(true);
                    _oAcceptor.Dispose();
                    _oAcceptor = null;
                    _oRouter.Log(">> Acceptor stopped. <<");
                    bReturn = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return bReturn;
        }

        internal void SendMessage(Message message, string target)
        {
            if (_dcSesssionID.ContainsKey(target))
            {
                
                Session.sendToTarget(message, _dcSesssionID[target]);
            }
        }

        #region Application
        public void fromAdmin(Message message, SessionID sessionID) { }
        public void fromApp(Message message, SessionID sessionID) { _oRouter.MessageFromClient(message, sessionID); }
        public void onCreate(SessionID sessionID) { }
        public void onLogon(SessionID sessionID) 
        { 
            Connected[sessionID.getTargetCompID()] = true;
            _oRouter.AcceptorLogOn(sessionID); 
        }
        public void onLogout(SessionID sessionID) 
        {
            Connected[sessionID.getTargetCompID()] = false;
            _oRouter.AcceptorLogOut(sessionID); 
        }
        public void toAdmin(Message message, SessionID sessionID) { }
        public void toApp(Message message, SessionID sessionID) { }
        #endregion 

    }
}
