using System;
using System.Collections.Generic;
using QuickFix;
using System.Diagnostics;

namespace FeedXML2FIX
{
    internal class FixInitiator : Application
    {

        private SocketInitiator _oInitiator;
        private string _sLogPath;
        private SessionID _oSessionID;

        public FixInitiator(string logpath)
        {
            _sLogPath = logpath;
        }
        
        internal bool InitiatorStart()
        {
            bool bReturn = false;
            try
            {
                Application application = this;
                //SessionSettings settings = new SessionSettings(@"C:\DropCopy_xml2fix_Initiator.cfg");
                //SessionSettings settings = new SessionSettings(@"\\NESTSRV03\Nest\TI\NESTSoft\FIX\Configs\DropCopy\DropCopy_Initiator_Xml2Fix.cfg"); 
                SessionSettings settings = new SessionSettings(@"\\NESTSRV03\NESTSoft\FIX\Configs\DropCopy\DropCopy_Initiator_Xml2Fix.cfg"); 
                FileStoreFactory storeFactory = new FileStoreFactory(settings);
                FileLogFactory logFactory = new FileLogFactory(settings);
                MessageFactory messageFactory = new DefaultMessageFactory();
                _oInitiator = new SocketInitiator(application, storeFactory, settings, logFactory, messageFactory);

                _oInitiator.start();
                _oSessionID = (SessionID)_oInitiator.getSessions().ToArray()[0];

                Debug.WriteLine(">> Initiator started. <<");
                bReturn = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return bReturn;
        }

        internal bool InitiatorStop()
        {
            bool bReturn = false;
            try
            {
                if (_oInitiator != null)
                {
                    _oInitiator.stop(true);
                    _oInitiator.Dispose();
                    _oInitiator = null;
                    Debug.WriteLine(">> Initiator stopped. <<");
                    bReturn = true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
            return bReturn;
        }

        internal void SendMessage(Message message)
        {
            if (_oSessionID != null)
                Session.sendToTarget(message, _oSessionID);
        }

        #region Application
        public void fromAdmin(Message message, SessionID sessionID) { }
        public void fromApp(Message message, SessionID sessionID) { }
        public void onCreate(SessionID sessionID) { }
        public void onLogon(SessionID sessionID) { }
        public void onLogout(SessionID sessionID) { }
        public void toAdmin(Message message, SessionID sessionID) { }
        public void toApp(Message message, SessionID sessionID) { }
        #endregion 
       
    }
}
