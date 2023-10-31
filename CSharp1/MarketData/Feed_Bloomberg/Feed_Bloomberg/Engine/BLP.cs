using System;  
using System.Collections.Generic;
using System.Text;

using Feed_Bloomberg;

using CorrelationID = Bloomberglp.Blpapi.CorrelationID;
using Event = Bloomberglp.Blpapi.Event;
using EventHandler = Bloomberglp.Blpapi.EventHandler;
using Session = Bloomberglp.Blpapi.Session;
using SessionOptions = Bloomberglp.Blpapi.SessionOptions;
using Service = Bloomberglp.Blpapi.Service;
using Subscription = Bloomberglp.Blpapi.Subscription;
using Message = Bloomberglp.Blpapi.Message;
using Element = Bloomberglp.Blpapi.Element;


namespace Feed_Bloomberg.Engine
{
    class BLP
    {
        Session session;
        Service refDataService;
        Feeder feeder;

        public BLP(Feeder _feeder)
        {
            SessionOptions sessionOptions = new SessionOptions();

            this.feeder = _feeder;
          
            //Inicializa variáveis de configuração da sessão Bloomberg
            sessionOptions.ServerHost = "localhost";
            //sessionOptions.ServerHost = "192.168.0.104";
            sessionOptions.ServerPort = 8194;
            sessionOptions.AutoRestartOnDisconnection = true;

            //Instancia nova sessão Bloomberg com as opções definidas
            session = new Session(sessionOptions, new EventHandler(this.ProcessEvent));
            
            //Inicia sessão Bloomberg
            Start();
        }

        //Tratamento de eventos da sessão Bloomberg
        public void ProcessEvent(Event eventObj, Session session)
        {

            switch (eventObj.Type)
            {
                case Event.EventType.SUBSCRIPTION_DATA:
                    handleDataEvent(eventObj, session);
                    break;
                case Event.EventType.SESSION_STATUS:
                case Event.EventType.SERVICE_STATUS:
                case Event.EventType.SUBSCRIPTION_STATUS:
                    handleStatusEvent(eventObj, session);
                    break;
                default:
                    {
                        handleOtherEvent(eventObj, session);
                        break;
                    }
            }

            
        }

        public void handleDataEvent(Event eventObj, Session session)
        {
            foreach (Message m in eventObj.GetMessages())
            {
                feeder.ReceiveMessage(m);
            }            
        }

        public void handleStatusEvent(Event eventObj, Session session)
        {         
            switch (eventObj.Type)
            {
                case Event.EventType.SUBSCRIPTION_STATUS:
                    foreach (Message m in eventObj.GetMessages())
                    {
                        if (m.MessageType.Equals("SubscriptionFailure")){
                            string message;
                            message = "Subscription Failure: \r\n";
                            message = message + "Ticker: " + m.TopicName + "\r\n";
                            
                            Log.AddLogEntry(message);
                            Log.Log_Event(this.feeder.ProgramID,103, 9, "Subscription Failure. Ticker: " + m.TopicName);
                        }
                    }
                    break;
                default:
                    break;
            
                        
            }
               
        }

        public void handleOtherEvent(Event eventObj, Session session)
        {
            foreach (Message m in eventObj.GetMessages())
            {
                Log.AddLogEntry("OTHER: " + m.MessageType);
            }   
        }
        

        //Inicialização de sessão Bloomberg e abre serviço
        public void Start()
        {
            //Inicializa sessão
            if (!session.Start())
            {
                //Caso não seja possível iniciar a sessão, grava log de erro e termina
                Log.AddLogEntry("Unable to start session");
                System.Environment.Exit(1);
            }


            //Abre serviço //blp/mktdata
            if (!session.OpenService("//blp/mktdata"))
            {
                //Caso não seja possível abrir o serviço, grava log de erro e termina
                Log.AddLogEntry("Unable to open service //blp/mktdata");
                System.Environment.Exit(1);
            }

            refDataService = session.GetService("//blp/mktdata");

        }

        //Assina serviço de dados Bloomberg
        public void Subscribe(List<Subscription> subscriptionList)
        {
            session.Subscribe(subscriptionList);            
        }

        //Encerra serviço de dados e finaliza sessão Bloomberg
        public void Stop(List<Subscription> subscriptionList)
        {
            session.Unsubscribe(subscriptionList);
            session.Stop();
        }

    }
}
