using System;
using System.Collections.Generic;
using System.Text;
using QuickFix;

namespace QuickNestFIX
{
    /// <summary>
    /// Handles the connection between NestFix and FlexTrade
    /// </summary>
    public class FixInitiator: MessageCracker, QuickFix.Application
    {
        private SessionType _sessionType = SessionType.ToStreet;
        public SessionType sessionType
        {
            get { return _sessionType; }
        }
        
        //QuickFix session object
        private SessionSettings _settings;
        private FileStoreFactory _store;
        private FileLogFactory _logs;
        private MessageFactory _messages;
        private SocketInitiator _initiator;

        #region Constructors

        /// <summary>
        /// Create instance of FIX class
        /// </summary>
        public FixInitiator()
        {
            string strConfigPath = QuickNestFIX.Properties.Settings.Default.ToStreetConfigPath;

            try
            {
                _settings = new SessionSettings(strConfigPath);
                _store = new FileStoreFactory(_settings);
                _logs = new FileLogFactory(_settings);
                _messages = new DefaultMessageFactory();
                _initiator = new SocketInitiator(this, _store, _settings, _logs, _messages);

                _initiator.start();
            }
            catch(Exception e)
            {
                throw new Exception("Unable to create instance of class FIX", e);
            }//catch
        }//FIX

        #endregion

        public void Stop()
        {
            _initiator.stop();
        }

        #region QuickFix Events

        /// <summary>
        /// Called on FIX session creation
        /// </summary>
        /// <param name="sessionID">Uniquely identifies the session</param>
        public void onCreate(SessionID sessionID)
        {
            OrderManager OM = OrderManager.Instance;

            OM.CreateSession(sessionID, sessionType);
        }

        /// <summary>
        /// Called on successful logon
        /// </summary>
        /// <param name="sessionID">Uniquely identifies the session</param>
        public void onLogon(SessionID sessionID)
        {
            OrderManager OM = OrderManager.Instance;

            OM.UpdateSession(sessionID, SessionStatus.Connected, sessionType);
        }

        /// <summary>
        /// Called when connection ends
        /// </summary>
        /// <param name="sessionID">Uniquely identifies the session</param>
        public void onLogout(SessionID sessionID)
        {
            OrderManager OM = OrderManager.Instance;

            OM.UpdateSession(sessionID, SessionStatus.Disconnected, sessionType);
        }

        /// <summary>
        /// Called before the application sends a session message
        /// </summary>
        /// <param name="message">FIX message</param>
        /// <param name="sessionID">Uniquely identifies the session</param>
        public void toAdmin(Message message, SessionID sessionID)
        {
        }

        /// <summary>
        /// Called when a session message is received
        /// </summary>
        /// <param name="message">FIX message</param>
        /// <param name="sessionID">Uniquely identifies the session</param>
        public void fromAdmin(Message message, SessionID sessionID)
        {
            crack(message, sessionID);
        }

        /// <summary>
        /// Called before the application sends an application message
        /// </summary>
        /// <param name="message">FIX message</param>
        /// <param name="sessionID">Uniquely identifies the session</param>
        public void toApp(Message message, SessionID sessionID)
        { 
        }

        /// <summary>
        /// Called when an application message is received
        /// </summary>
        /// <param name="message">FIX message</param>
        /// <param name="sessionID">Uniquely identifies the session</param>
        public void fromApp(Message message, SessionID sessionID)
        {
            OrderManager OM = OrderManager.Instance;

            OM.EnqueueMessage(sessionID, sessionType, message);
        }
        #endregion


    }
}
