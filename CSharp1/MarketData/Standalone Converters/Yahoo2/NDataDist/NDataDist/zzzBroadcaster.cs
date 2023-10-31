//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using NestGeneric;

//namespace NDataDist
//{
//    class Broadcaster
//    {
//        NestSocketServer curSocket = new NestSocketServer();
        
//        public event EventHandler OnData;

//        public void MsgFromClient(object sender, EventArgs origE)
//        {
//            string curMessage = ((NestSocketServer.MsgEventArgs)origE).strMessage;
            
//        }








//        public class QuoteDataEventArgs : EventArgs
//        {
//            private int _IdTicker;
//            private int _FLID;
//            private string _Value;

//            public int IdTicker
//            {
//                get { return _IdTicker; }
//                set { _IdTicker = value; }
//            }
//            public int FLID
//            {
//                get { return _FLID; }
//                set { _FLID = value; }
//            }
//            public string Value
//            {
//                get { return _Value; }
//                set { _Value = value; }
//            }

//            private QuoteDataEventArgs() { }

//            public QuoteDataEventArgs(int IdTicker, int FLID, string Value)
//            {
//                _IdTicker = IdTicker;
//                _FLID = FLID;
//                _Value = Value;
//            }
//        }
//    }
//}
