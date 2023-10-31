using System;
using System.Net;
using System.Net.Sockets;

namespace NestGeneric
{
    public class NestSocketClient
    {
        public event EventHandler NewMessage;

        byte[] dataBuffer = new byte[10];
        IAsyncResult result;
        public AsyncCallback pfnCallBack;
        public Socket clientSocket;

        string MessageBuffer = "";
        public bool IsReceivingData = false;

        private static object SendLock = new object();

        private object recLock;

        private string _ConnectIP;

        public string ConnectIP
        {
            get { return _ConnectIP; }
            set { _ConnectIP = value; }
        }

        private int _ConnectPort;

        public int ConnectPort
        {
            get { return _ConnectPort; }
            set { _ConnectPort = value; }
        }

        public bool IsConnected
        {
            get 
            {
                if (clientSocket != null)
                {
                    return clientSocket.Connected;
                }
                else
                {
                    return false;
                }
            }
        }

        public NestSocketClient(string __ConnectIP, int __ConnectPort)
        {
            _ConnectIP = __ConnectIP;
            _ConnectPort = __ConnectPort;
        }

        // ==========================   Start/Stop  =======================================

        public void Connect()
        {
            ReConnect();
        }

        public void ReConnect()
        {
            if (_ConnectIP != "" && _ConnectPort > 0)
            {
                try
                {
                    clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    IPAddress ip = IPAddress.Parse(_ConnectIP);
                    int iPortNo = System.Convert.ToInt16(_ConnectPort);

                    IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);

                    clientSocket.Connect(ipEnd);

                    if (clientSocket.Connected)
                    {
                        WaitForData();
                    }
                }
                catch (SocketException se)
                {
                    string str;
                    str = "\nConnection failed, is the server running?\n" + se.Message;
                }
            }
        }


        public void Disconnect()
        {
            lock (SendLock)
            {
                if (clientSocket != null)
                {
                    clientSocket.Close();
                    clientSocket = null;
                }
            }
        }

        public void WaitForData()
        {
            try
            {
                if (pfnCallBack == null)
                {
                    pfnCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.thisSocket = clientSocket;

                result = clientSocket.BeginReceive(theSocPkt.dataBuffer, 0, theSocPkt.dataBuffer.Length, SocketFlags.None, pfnCallBack, theSocPkt);
            }
            catch (SocketException se)
            {
                int a = 0;
            }
        }

        // ==========================   Send/Receive Messages  =======================================

        public void SendMessage(string strMessage)
        {
            lock (SendLock)
            {
                try
                {
                    Object objData = strMessage + (char)17;
                    byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());
                    if (clientSocket != null)
                    {
                        clientSocket.Send(byData);
                    }
                }
                catch (SocketException se)
                {
                    string test = strMessage;
                }
            }
        }

        public void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket theSockId = (SocketPacket)asyn.AsyncState;
                int iRx = theSockId.thisSocket.EndReceive(asyn);
                char[] chars = new char[iRx + 1];
                System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
                int charLen = d.GetChars(theSockId.dataBuffer, 0, iRx, chars, 0);
                string szData = chars[0].ToString();

                ProcessMessage(szData);

                WaitForData();
            }
            catch (ObjectDisposedException)
            {
                System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
            }
            catch (SocketException se)
            {
                int a = 0;
            }
        }

        private void ProcessMessage(string strMessage)
        {
            char testChar = strMessage[0];
            int i = Convert.ToInt16(testChar);
            if (i != 17)
            {
                IsReceivingData = true;
                MessageBuffer = MessageBuffer + strMessage;
            }
            else
            {
                string lastMessage = MessageBuffer;
                //Console.WriteLine(lastMessage);
                MsgEventArgs thisMsg = new MsgEventArgs();
                thisMsg.strMessage = lastMessage;
                if(NewMessage != null) 
                    NewMessage(this, thisMsg);
                MessageBuffer = "";
                IsReceivingData = false;
            }
        }

        // ==========================   Other/Utils  =======================================

        public class SocketPacket
        {
            public System.Net.Sockets.Socket thisSocket;
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
