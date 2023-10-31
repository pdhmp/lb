using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NestGeneric;
using NCommonTypes;
using NestSymConn;

namespace NDataDist
{
    class DataDist
    {
        //List<QuoteServerConn> ServerList = new List<QuoteServerConn>();
        SocketServer curSocketServer = new SocketServer();
        public ConnManager FlexConnection;
        public ConnManager BELLConnection;
        public ConnManager BELLXPConnection;
        public ConnManager XPUMDFBmfConnection;
        public ConnManager UBSUMDFBmfConnection;
        public ConnManager UBSUMDFBovConnection;
        public ConnManager XPUMDFBovConnection;
        public ConnManager BSEConnection;
        public ConnManager LINKBOVConnection;
        public ConnManager LINKBMFConnection;
        public ConnManager YahooConnection;
        public ConnManager BloombergConnection;

        public NDistConn curBSEConn;

        private Sources BMFSource = Sources.None;
        private Sources BovespaSource = Sources.None;

        bool BellSourceisXP = false;

        List<ConnManager> DataConnections = new List<ConnManager>();

        NestSymConn.DataLog curDataLog = new NestSymConn.DataLog(@"C:\FIXLOG\DataDist\" + DateTime.Now.Date.ToString("yyyyMMdd") + "_1.txt");
        public void SetDebugLevel(int DebugLevel) { curDataLog.DebugLevel = DebugLevel; }
        public void SetDebugMode(NestSymConn.DebugModes DebugMode) { curDataLog.DebugMode = DebugMode; }

        public void ChangeBovespaSource(Sources newSource)
        {
            if (GetNestConnector(BovespaSource) != null) GetNestConnector(BovespaSource).UnSubscribeAll();
            BovespaSource = newSource;
            RequestResubscriptions();
        }

        public void ChangeBMFSource(Sources newSource, string isXP)
        {
            if (isXP == "XP") BellSourceisXP = true; else BellSourceisXP = false;
            if (GetNestConnector(BMFSource) != null) GetNestConnector(BMFSource).UnSubscribeAll();
            BMFSource = newSource;
            RequestResubscriptions();
        }

        public List<string> getDataClients()
        {
            List<string> tempList = new List<string>();
            foreach (DataClient curClient in curSocketServer.DataClients.Values)
            {
                tempList.Add(curClient.ToString());
                curDataLog.LogEntry("getDataClients : " + curClient.ToString(), 1);
            }
            return tempList;
        }

        public DataDist() { }

        NDistConn LINKBOVDistConn;
        NDistConn XPUMDFBovDistConn;
        NDistConn XPUMDFBmfDistConn;
        NDistConn BELLDistConn;
        NDistConn BELLXPDistConn;
        NDistConn LINKBMFDistConn;
        NDistConn YahooDistConn;
        NDistConn BloombergDistConn;
        NDistConn UBSUMDFBovDistConn;
        NDistConn UBSUMDFBmfDistConn;

        public void InitializeBSE()
        {
            curDataLog.LogEntry("InitializeBSE - 127.0.0.1:15201", 1);

            //curBSEConn = new NDistConn("127.0.0.1", 15201);
            curBSEConn = new NDistConn("127.0.0.1", 14001);
            curBSEConn.Connect();
            BSEConnection = new ConnManager(curBSEConn);
            DataConnections.Add(BSEConnection);
            CreateEvents(BSEConnection);
        }

        public void InitializeLINKBOV()
        {
            LINKBOVDistConn = new NDistConn("127.0.0.1", 15202);
            curDataLog.LogEntry("InitializeLINKBOV - " + LINKBOVDistConn.ConnectIP + ":" + LINKBOVDistConn.ConnectPort, 1);
            LINKBOVConnection = new ConnManager(LINKBOVDistConn);
            DataConnections.Add(LINKBOVConnection);
            CreateEvents(LINKBOVConnection);
        }

        public void InitializeXPUMDFBov()
        {

            //XPUMDFBovDistConn = new NDistConn("10.12.29.160", 15301);
            XPUMDFBovDistConn = new NDistConn("192.168.0.133", 19301);
            curDataLog.LogEntry("InitializeXPUMDFBov - " + XPUMDFBovDistConn.ConnectIP + ":" + XPUMDFBovDistConn.ConnectPort, 1);
            XPUMDFBovConnection = new ConnManager(XPUMDFBovDistConn);
            DataConnections.Add(XPUMDFBovConnection);
            CreateEvents(XPUMDFBovConnection);
        }

        public void InitializeXPUMDFBmf()
        {
            //XPUMDFBmfDistConn = new NDistConn("10.12.29.160", 15302);
            XPUMDFBmfDistConn = new NDistConn("192.168.0.133", 19302);
            curDataLog.LogEntry("InitializeXPUMDFBmf - " + XPUMDFBmfDistConn.ConnectIP + ":" + XPUMDFBmfDistConn.ConnectPort, 1);
            XPUMDFBmfConnection = new ConnManager(XPUMDFBmfDistConn);
            DataConnections.Add(XPUMDFBmfConnection);
            CreateEvents(XPUMDFBmfConnection);
        }



        public void InitializeUBSUMDFBov()
        {
            UBSUMDFBovDistConn = new NDistConn("189.36.24.56", 15301);
            curDataLog.LogEntry("InitializeUBSUMDFBov - " + UBSUMDFBovDistConn.ConnectIP + ":" + UBSUMDFBovDistConn.ConnectPort, 1);
            UBSUMDFBovConnection = new ConnManager(UBSUMDFBovDistConn);
            DataConnections.Add(UBSUMDFBovConnection);
            CreateEvents(UBSUMDFBovConnection);
        }

        public void InitializeUBSUMDFBmf()
        {
            UBSUMDFBmfDistConn = new NDistConn("189.36.24.56", 15302);
            curDataLog.LogEntry("InitializeUBSUMDFBmf - " + UBSUMDFBmfDistConn.ConnectIP + ":" + UBSUMDFBmfDistConn.ConnectPort, 1);
            UBSUMDFBmfConnection = new ConnManager(UBSUMDFBmfDistConn);
            DataConnections.Add(UBSUMDFBmfConnection);
            CreateEvents(UBSUMDFBmfConnection);
        }

        public void InitializeBELL()
        {
            curDataLog.LogEntry("InitializeBELL - 127.0.0.1:15212", 1);
            BELLDistConn = new NDistConn("127.0.0.1", 15212);
            BELLConnection = new ConnManager(BELLDistConn);
            DataConnections.Add(BELLConnection);
            CreateEvents(BELLConnection);
        }


        public void InitializeBELLXP()
        {
            curDataLog.LogEntry("InitializeBELLXP - 127.0.0.1:15213", 1);
            BELLXPDistConn = new NDistConn("127.0.0.1", 15213);
            BELLXPConnection = new ConnManager(BELLXPDistConn);
            DataConnections.Add(BELLXPConnection);
            CreateEvents(BELLXPConnection);
        }

        public void InitializeLINKBMF()
        {
            curDataLog.LogEntry("InitializeLINKBMF - 127.0.0.1:15214", 1);
            LINKBMFDistConn = new NDistConn("127.0.0.1", 15214);
            LINKBMFConnection = new ConnManager(LINKBMFDistConn);
            DataConnections.Add(LINKBMFConnection);
            CreateEvents(LINKBMFConnection);
        }

        public void InitializeFLEX()
        {
            curDataLog.LogEntry("InitializeFLEX", 1);
            FlexConnection = new ConnManager(new NestSymConn.FLEXConn());
            DataConnections.Add(FlexConnection);
            CreateEvents(FlexConnection);
        }

        public void InitializeYAHOO()
        {
            curDataLog.LogEntry("InitializeYAHOO - 127.0.0.1:15213", 1);
            YahooDistConn = new NDistConn("127.0.0.1", 15013);
            YahooConnection = new ConnManager(YahooDistConn);
            DataConnections.Add(YahooConnection);
            CreateEvents(YahooConnection);
        }

        public void InitializeBLOOMBERG()
        {
            curDataLog.LogEntry("InitializeBLOOMBERG - BBG02:15014", 1);
            BloombergDistConn = new NDistConn("192.168.0.104", 15014);
            BloombergConnection = new ConnManager(BloombergDistConn);
            DataConnections.Add(BloombergConnection);
            CreateEvents(BloombergConnection);
        }

        private void CreateEvents(ConnManager curConnManager)
        {
            curConnManager.OnConnect += new EventHandler(ConnConnect);
            curConnManager.OnDisconnect += new EventHandler(ConnDisconnect);
            curConnManager.onMarketData += new EventHandler(NewMarketData);
            curConnManager.onDepthData += new EventHandler(NewDepthData);
            curConnManager.onDepthAGGData += new EventHandler(NewDepthAGGData);
            curConnManager.onReloadDepth += new EventHandler(ReloadDepth);
            curConnManager.onSecList += new EventHandler(NewSecList);
        }

        private void ConnConnect(object sender, EventArgs e)
        {
            curDataLog.LogEntry(((ConnManager)sender).ConnType() + " Connected", 1);
            Console.WriteLine(((ConnManager)sender).ConnType() + " Connected");
        }

        private void ReloadDepth(object sender, EventArgs e)
        {
            curDataLog.LogEntry(((ConnManager)sender).ConnType() + " Reload Depth requested ", 1);
            Console.WriteLine(((ConnManager)sender).ConnType() + " Reload Depth requested ");
        }

        private void ConnDisconnect(object sender, EventArgs e)
        {
            curDataLog.LogEntry(((ConnManager)sender).ConnType() + " Disconnected", 1);
            Console.WriteLine(((ConnManager)sender).ConnType() + " Disconnected");
        }

        public void StartListen()
        {
            if (!curSocketServer.IsListening)
            {
                curSocketServer.NewMessage += new EventHandler(MsgFromClient);
                curSocketServer.onClientDisconnected += new EventHandler(ClientDisconnected);
                curSocketServer.StartListen(15432);
                //curSocketServer.StartListen(14000);
            }
        }

        public void ClientDisconnected(object sender, EventArgs e)
        {
            SocketServer.MsgEventArgs curClientMsgEventArgs = (SocketServer.MsgEventArgs)e;

            int curClient = int.Parse(curClientMsgEventArgs.strMessage);

            foreach (ConnManager curConnManager in DataConnections)
            {
                curConnManager.UnSubscribeAll(curClient);
            }
        }

        public void MsgFromClient(object sender, EventArgs e)
        {
            NCommonTypes.DataClientMsg curDataClientMsg = (NCommonTypes.DataClientMsg)e;
            string curMessage = curDataClientMsg.strMessage;
            curDataLog.LogEntry("MsgFromClient : " + curMessage, 1);


            if (curMessage != null)
            {
                string[] tempString = curMessage.Split((char)16);

                Sources reqSource = (Sources)int.Parse(tempString[3]);

                if (reqSource == Sources.BMF) reqSource = BMFSource;
                if (reqSource == Sources.Bovespa) reqSource = BovespaSource;
                curDataLog.LogEntry("ReqSource : " + reqSource.ToString(), 1);

                if (tempString.Length > 1)
                {
                    switch (tempString[1])
                    {
                        case "DISC": FlexConnection.UnSubscribeAll(int.Parse(tempString[2])); break;
                        case "SUB": Subscribe(curDataClientMsg.ClientID, tempString[2], reqSource); break;
                        case "SUBD": SubscribeDepth(curDataClientMsg.ClientID, tempString[2], reqSource); break;
                        case "SUBDA": SubscribeDepthAGG(curDataClientMsg.ClientID, tempString[2], reqSource); break;
                        case "SUBT": SubscribeTrades(curDataClientMsg.ClientID, tempString[2], reqSource); break;
                        case "GETSEC":  GetSecList(curDataClientMsg.ClientID, reqSource); break;
                        case "GETICP": GetIndexComp(curDataClientMsg.ClientID, tempString[2], reqSource); break;
                        case "UNSUB": UnSubscribe(curDataClientMsg.ClientID, tempString[2], reqSource); break;
                        case "UNSUBD": UnSubscribeDepth(curDataClientMsg.ClientID, tempString[2], reqSource); break;
                        case "UNSUBDA": UnSubscribeDepthAGG(curDataClientMsg.ClientID, tempString[2], reqSource); break;
                        case "RELOADDEPTH": ReloadDepth(curDataClientMsg.ClientID, tempString[2], reqSource); break;
                        default: break;
                    }
                }
            }
        }

        #region ForwardSubscriptions
        // ============================================  FORWARD SUBSCRIPTIONS  ============================================

        public void ReloadDepth(int Subscriber, string Symbol, Sources Source)
        {
            try
            {
                ConnManager curConnector = GetNestConnector(Source);
                if (curConnector != null)
                {
                    curConnector.ReloadDepth(Symbol, Source);
                    curDataLog.LogEntry("Symbol : " + Symbol + " - Reload Depth : " + Subscriber.ToString() + " Source : " + Source.ToString(), 1);
                }
                else
                {
                    Console.WriteLine("Reload Depth: source does not exist for: " + Symbol);
                    curDataLog.LogEntry("Reload Depth: source does not exist for: " + Symbol, 1);
                }
            }
            catch
            {
                Console.WriteLine("Reload Depth: source does not exist for: " + Symbol);
                curDataLog.LogEntry("Reload Depth: source does not exist for: " + Symbol, 1);
            }
        }

        public void Subscribe(int Subscriber, string Symbol, Sources Source)
        {
            try
            {
                ConnManager curConnector = GetNestConnector(Source);
                if (curConnector != null)
                {
                    curConnector.Subscribe(Symbol, Subscriber);
                    curDataLog.LogEntry("Symbol : " + Symbol + " - Subscribe : " + Subscriber.ToString() + " Source : " + Source.ToString(), 1);
                }
                else
                {
                    Console.WriteLine("Subscribe: source does not exist for: " + Symbol);
                    curDataLog.LogEntry("Subscribe: source does not exist for: " + Symbol, 1);
                }
            }
            catch
            {
                Console.WriteLine("Subscribe: source does not exist for: " + Symbol);
                curDataLog.LogEntry("Subscribe: source does not exist for: " + Symbol, 1);
            }
        }

        public void UnSubscribe(int Subscriber, string Symbol, Sources Source)
        {
            try
            {
                ConnManager curConnector = GetNestConnector(Source);
                curConnector.UnSubscribe(Symbol, Subscriber);
                curDataLog.LogEntry("Symbol : " + Symbol + " - Unsubscribe : " + Subscriber.ToString() + " Source : " + Source.ToString(), 1);
            }
            catch
            {
                Console.WriteLine("Unsubscribe: source does not exist for: " + Symbol);
                curDataLog.LogEntry("Unsubscribe: source does not exist for: " + Symbol, 1);
            }
        }

        public void SubscribeDepth(int Subscriber, string Symbol, Sources Source)
        {
            try
            {
                ConnManager curConnector = GetNestConnector(Source);
                if (curConnector != null)
                { curConnector.SubscribeDepth(Symbol, Subscriber); }
                curDataLog.LogEntry("Symbol : " + Symbol + " - SubscribeDepth : " + Subscriber.ToString() + " Source : " + Source.ToString(), 1);
            }
            catch
            {
                Console.WriteLine("SubscribeDepth: source does not exist for: " + Symbol);
                curDataLog.LogEntry("SubscribeDepth: source does not exist for: " + Symbol, 1);
            }
        }

        public void UnSubscribeDepth(int Subscriber, string Symbol, Sources Source)
        {
            try
            {
                ConnManager curConnector = GetNestConnector(Source);
                curConnector.UnSubscribeDepth(Symbol, Subscriber);
                curDataLog.LogEntry("Symbol : " + Symbol + " - UnSubscribeDepth : " + Subscriber.ToString() + " Source : " + Source.ToString(), 1);
            }
            catch
            {
                Console.WriteLine("UnSubscribeDepth: source does not exist for: " + Symbol);
                curDataLog.LogEntry("UnSubscribeDepth: source does not exist for: " + Symbol, 1);
            }
        }

        public void SubscribeDepthAGG(int Subscriber, string Symbol, Sources Source)
        {
            try
            {
                ConnManager curConnector = GetNestConnector(Source);
                curConnector.SubscribeDepthAGG(Symbol, Subscriber);
                curDataLog.LogEntry("Symbol : " + Symbol + " - SubscribeDepthAGG : " + Subscriber.ToString() + " Source : " + Source.ToString(), 1);
            }
            catch
            {
                Console.WriteLine("SubscribeDepthAGG: source does not exist for: " + Symbol);
                curDataLog.LogEntry("SubscribeDepthAGG: source does not exist for: " + Symbol, 1);
            }
        }

        public void UnSubscribeDepthAGG(int Subscriber, string Symbol, Sources Source)
        {
            try
            {
                ConnManager curConnector = GetNestConnector(Source);
                curConnector.UnSubscribeDepthAGG(Symbol, Subscriber);
                curDataLog.LogEntry("Symbol : " + Symbol + " - UnSubscribeDepthAGG : " + Subscriber.ToString() + " Source : " + Source.ToString(), 1);
            }
            catch
            {
                Console.WriteLine("UnSubscribeDepthAGG: source does not exist for: " + Symbol);
                curDataLog.LogEntry("UnSubscribeDepthAGG: source does not exist for: " + Symbol, 1);
            }
        }

        public void SubscribeTrades(int Subscriber, string Symbol, Sources Source)
        {

        }

        public void GetSecList(int Subscriber, Sources Source) // 1
        {
            try
            {
                ConnManager curConnector = GetNestConnector(Source);
                if (curConnector != null)
                {
                    curConnector.GetSecList(Source, Subscriber);
                    curDataLog.LogEntry("GetSecList: " + Subscriber.ToString() + " Source : " + Source.ToString(), 1);
                }
                else
                {
                    Console.WriteLine("GetSecList: source does not exist for: " + Source);
                    curDataLog.LogEntry("GetSecList: source does not exist for: " + Source, 1);
                }
            }
            catch
            {
                Console.WriteLine("GetSecList: source does not exist for: " + Source);
                curDataLog.LogEntry("GetSecList: source does not exist for: " + Source, 1);
            }
         }

        public void GetIndexComp(int Subscriber, string Symbol, Sources Source)
        {
        }

        ConnManager GetNestConnector(Sources Source)
        {
            switch (Source)
            {
                case Sources.ProxyDiff: return BSEConnection;
                case Sources.Yahoo: return YahooConnection;
                case Sources.Bloomberg: return BloombergConnection;
                case Sources.FlexTrade: return FlexConnection;
                case Sources.BELL: if (BellSourceisXP) return BELLXPConnection; else return BELLConnection;
                case Sources.XPUMDFBmf: return XPUMDFBmfConnection;
                case Sources.XPUMDFBov: return XPUMDFBovConnection;
                case Sources.LINKBOV: return LINKBOVConnection;
                case Sources.LINKBMF: return LINKBMFConnection;
                case Sources.Reuters:
                default: return null;
            }
        }
        // =================================================================================================================

        #endregion

        #region ForwardMarketData
        // ============================================  FORWARD DATA  ============================================

        private void NewMarketData(object sender, EventArgs origE)
        {
            MarketUpdateItemWithSource curItem = (MarketUpdateItemWithSource)origE;
            foreach (int curSubscriber in curItem.Subscribers)
            {
                if (curSocketServer.DataClients.ContainsKey(curSubscriber))
                {
                    string tempMessage = curItem.MarketUpdateItem.EncodeToString("NDD");//, curItem.MarketUpdateItem);
                    // curDataLog.LogEntry("NewMarketData : " + tempMessage, 1);

                    try
                    {
                        curSocketServer.DataClients[curSubscriber].SendMessage(curItem.MarketUpdateItem.EncodeToString("NDD"));//, curItem.MarketUpdateItem));
                    }
                    catch
                    {
                        curDataLog.LogEntry("catch : NewMarketData", 1);
                    }
                }
            }
        }

        private void NewDepthData(object sender, EventArgs origE)
        {
            DepthUpdateItemWithSource curItem = (DepthUpdateItemWithSource)origE;
            foreach (int curSubscriber in curItem.Subscribers)
            {
                if (curSocketServer.DataClients.ContainsKey(curSubscriber))
                {
                    try
                    {
                        curSocketServer.DataClients[curSubscriber].SendMessage(curItem.DepthUpdateItem.EncodeToString("NDD", false));//, curItem.DepthUpdateItem, false));
                    }
                    catch
                    {
                        curDataLog.LogEntry("catch : NewDepthData", 1);
                    }
                }
            }
        }

        private void NewDepthAGGData(object sender, EventArgs origE)
        {
            DepthUpdateItemWithSource curItem = (DepthUpdateItemWithSource)origE;
            foreach (int curSubscriber in curItem.Subscribers)
            {
                if (curSocketServer.DataClients.ContainsKey(curSubscriber))
                {
                    try
                    {
                        curSocketServer.DataClients[curSubscriber].SendMessage(curItem.DepthUpdateItem.EncodeToString("NDD", true));//, curItem.DepthUpdateItem, true));
                    }
                    catch
                    {
                        curDataLog.LogEntry("catch : NewDepthAGGData", 1);
                    }
                }
            }
        }

        private void NewSecList(object sender, EventArgs origE)
        {
            SecListItemWithSource curItem = (SecListItemWithSource)origE;

            foreach (int curSubscriber in curItem.Subscribers)
            {
                if (curSocketServer.DataClients.ContainsKey(curSubscriber))
                {
                    try
                    {
                        curSocketServer.DataClients[curSubscriber].SendMessage(curItem.SecListUpdateItem.EncodeToString("NDD"));//, curItem.DepthUpdateItem, true));
                    }
                    catch { curDataLog.LogEntry("catch : NewSecList", 1); }
                }
            }
        }

        private void RequestResubscriptions()
        {
            lock (curSocketServer.DataClients)
            {
                foreach (DataClient curDataClient in curSocketServer.DataClients.Values)
                {
                    curDataClient.SendMessage("NDD" + (char)16 + "RESUB");
                }
            }
        }

        // =================================================================================================================

        #endregion
    }
}
