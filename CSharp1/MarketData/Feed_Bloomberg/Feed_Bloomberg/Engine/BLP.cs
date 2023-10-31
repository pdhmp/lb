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
          
            //Inicializa vari�veis de configura��o da sess�o Bloomberg
            sessionOptions.ServerHost = "localhost";
            //sessionOptions.ServerHost = "192.168.0.104";
            sessionOptions.ServerPort = 8194;
            sessionOptions.AutoRestartOnDisconnection = true;

            //Instancia nova sess�o Bloomberg com as op��es definidas
            session = new Session(sessionOptions, new EventHandler(this.ProcessEvent));
            
            //Inicia sess�o Bloomberg
            Start();
        }

        //Tratamento de eventos da sess�o Bloomberg
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
        

        //Inicializa��o de sess�o Bloomberg e abre servi�o
        public void Start()
        {
            //Inicializa sess�o
            if (!session.Start())
            {
                //Caso n�o seja poss�vel iniciar a sess�o, grava log de erro e termina
                Log.AddLogEntry("Unable to start session");
                System.Environment.Exit(1);
            }


            //Abre servi�o //blp/mktdata
            if (!session.OpenService("//blp/mktdata"))
            {
                //Caso n�o seja poss�vel abrir o servi�o, grava log de erro e termina
                Log.AddLogEntry("Unable to open service //blp/mktdata");
                System.Environment.Exit(1);
            }

            refDataService = session.GetService("//blp/mktdata");

        }

        //Assina servi�o de dados Bloomberg
        public void Subscribe(List<Subscription> subscriptionList)
        {
            session.Subscribe(subscriptionList);            
        }

        //Encerra servi�o de dados e finaliza sess�o Bloomberg
        public void Stop(List<Subscription> subscriptionList)
        {
            session.Unsubscribe(subscriptionList);
            session.Stop();
        }

    }
}
