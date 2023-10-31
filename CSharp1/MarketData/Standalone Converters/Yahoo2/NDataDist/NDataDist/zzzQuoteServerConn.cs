//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NestGeneric;

//namespace NDataDist
//{
//    class QuoteServerConn
//    {
//        List<NestSocketClient> ServerConnections = new List<NestSocketClient>();
//        Broadcaster curBroadCaster = new Broadcaster();

//        public event EventHandler OnData;

//        public QuoteServerConn(string ServerId, string ServerIP, int ServerPort)
//        {
//            ConnectQuoteServer(ServerId, ServerIP, ServerPort);
//        }

//        public void MsgFromServer(object sender, EventArgs origE)
//        {
//            //string curMessage = ((NestSocketClient.MsgEventArgs)origE).strMessage;
//            if (OnData != null)
//            {
//                OnData(this, origE);
//            }
//        }

//        public void ConnectQuoteServer(string ServerId, string ServerIP, int ServerPort)
//        {
//            NestSocketClient curClient = new NestSocketClient(ServerIP, ServerPort);
//            curClient.NewMessage += new EventHandler(MsgFromServer);
//        }
//    }
//}
