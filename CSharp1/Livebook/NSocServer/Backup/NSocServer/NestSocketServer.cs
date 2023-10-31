using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace NestGeneric
{
    public class NestSocketServer
    {
        // ==========================   Properties  =======================================

        public event EventHandler NewMessage;

        private Socket mainSocket;
        private Socket[] workerSocket = new Socket[10];
        public AsyncCallback pfnWorkerCallBack;

        string MessageBuffer = "";

        private int _ListenPort;

        public int ListenPort
        {
            get { return _ListenPort; }
            set { _ListenPort = value; }
        }

        private int _clientCount;

        public int clientCount
        {
            get { return _clientCount; }
            set { _clientCount = value; }
        }

        // ==========================   Start/Stop  =======================================

        public void StartListen(int __ListenPort)
        {
            try
            {
                _ListenPort = __ListenPort;

                mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, ListenPort);

                mainSocket.Bind(ipLocal);

                mainSocket.Listen(4);

                mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);

            }
            catch (SocketException se)
            {
                int a = 0;
            }

        }

        public void StopListen()
        {
            CloseSockets();
        }

        void CloseSockets()
        {
            if (mainSocket != null)
            {
                mainSocket.Close();
            }
            for (int i = 0; i < _clientCount; i++)
            {
                if (workerSocket[i] != null)
                {
                    workerSocket[i].Close();
                    workerSocket[i] = null;
                    _clientCount--;
                }
            }
        }

        // ==========================   On Client Connect/Disconnect  =======================================


        public void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                workerSocket[_clientCount] = mainSocket.EndAccept(asyn);
                WaitForData(workerSocket[_clientCount]);

                _clientCount++;

                mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);

                //SendMessage("New Client connected: " + clientCount);

            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\n OnClientConnection: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                int a = 0;
            }

        }

        public void WaitForData(System.Net.Sockets.Socket soc)
        {
            try
            {
                if (pfnWorkerCallBack == null)
                {
                    pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.currentSocket = soc;
                // Start receiving any data written by the connected client
                // asynchronously
                soc.BeginReceive(theSocPkt.dataBuffer, 0,
                                   theSocPkt.dataBuffer.Length,
                                   SocketFlags.None,
                                   pfnWorkerCallBack,
                                   theSocPkt);
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10053)
                {
                    if (soc != null)
                    {
                        soc.Close();
                        soc = null;
                        //_clientCount--;
                    }
                    _clientCount--;
                }
            }

        }

        // ==========================   Send/Receive Messages  =======================================

        public void SendMessage(string strMessage)
        {
            try
            {
                Object objData = strMessage + (char)17;
                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
                for (int i = 0; i < 10; i++)
                {
                    if (workerSocket[i] != null)
                    {
                        if (workerSocket[i].Connected)
                        {
                            workerSocket[i].Send(byData);
                        }
                    }
                }

            }
            catch (SocketException se)
            {
                int a = 0;
            }
        }

        void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                int iRx = 0;

                iRx = socketData.currentSocket.EndReceive(asyn);

                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(socketData.dataBuffer, 0, iRx, chars, 0);
                System.String szData = new System.String(chars);

                ProcessMessage(szData);

                // Continue the waiting for data on the Socket
                WaitForData(socketData.currentSocket);
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                if (se.ErrorCode == 10054)
                {
                    _clientCount--;
                }
            }
        }

        private void ProcessMessage(string strMessage)
        {
            char testChar = strMessage[0];
            int i = Convert.ToInt16(testChar);
            if (i != 17)
            {
                MessageBuffer = MessageBuffer + testChar;
            }
            else
            {
                string lastMessage = MessageBuffer;
                //Console.WriteLine(lastMessage);
                MsgEventArgs thisMsg = new MsgEventArgs();
                thisMsg.strMessage = lastMessage;
                NewMessage(this, thisMsg);
                MessageBuffer = "";
            }
        }


        // ==========================   Other/Utils  =======================================

        public class SocketPacket
        {
            public System.Net.Sockets.Socket currentSocket;
            public byte[] dataBuffer = new byte[1];
        }

        String GetIP()
        {
            String strHostName = Dns.GetHostName();

            // Find host by name
            IPHostEntry iphostentry = Dns.GetHostByName(strHostName);

            // Grab the first IP addresses
            String IPStr = "";
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                IPStr = ipaddress.ToString();
                return IPStr;
            }
            return IPStr;
        }

        public class MsgEventArgs : EventArgs
        {
            private string _strMessage;

            public string strMessage
            {
                get { return _strMessage; }
                set { _strMessage = value; }
            }
        }


    }
}
