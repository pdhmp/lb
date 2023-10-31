using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using NCommonTypes;

namespace NDataDist
{
    //class SocketServer
    //{
    //    // ==========================   Properties  =======================================

    //    public event EventHandler NewMessage;

    //    private Socket mainSocket;
    //    public Dictionary<int, DataClient> DataClients = new Dictionary<int, DataClient>();

    //    private bool _IsListening = false;
    //    //public bool IsListening { get { return _IsListening; } }
    //    public bool IsListening { get { if (mainSocket != null) return _IsListening; else return false; } }

    //    object SendLock = new object();

    //    private int _ListenPort; public int ListenPort { get { return _ListenPort; } set { _ListenPort = value; } }
    //    public int clientCount { get { return DataClients.Count; } }

    //    public event EventHandler onClientDisconnected;

    //    // ==========================   Start/Stop  =======================================

    //    public void StartListen(int __ListenPort)
    //    {
    //        try
    //        {
    //            if (!_IsListening)
    //            {
    //                _ListenPort = __ListenPort;

    //                mainSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    //                IPEndPoint ipLocal = new IPEndPoint(IPAddress.Any, ListenPort);

    //                mainSocket.Bind(ipLocal);

    //                mainSocket.Listen(4);

    //                mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);

    //                GlobalVars.Instance.LogEntry("Listening on Port:" + __ListenPort, 98);
    //                _IsListening = true;
    //            }
    //        }
    //        catch (SocketException se)
    //        {
    //            GlobalVars.Instance.LogEntry("ERROR on StartListen():" + se.ToString(), 99);
    //        }

    //    }

    //    public void StopListen()
    //    {
    //        CloseSockets();
    //        _IsListening = false;
    //    }

    //    void CloseSockets()
    //    {
    //        if (mainSocket != null)
    //        {
    //            mainSocket.Close();
    //        }

    //        foreach (DataClient curDataClient in DataClients.Values)
    //        {
    //            curDataClient.Socket.Close();
    //        }

    //        DataClients.Clear();
    //    }

    //    // ==========================   On Client Connect/Disconnect  =======================================


    //    public void OnClientConnect(IAsyncResult asyn)
    //    {
    //        try
    //        {
    //            Socket curSocket = mainSocket.EndAccept(asyn);

    //            curSocket.SendTimeout = 10000;
    //            curSocket.NoDelay = true;

    //            DataClient curDataClient = new DataClient();
    //            curDataClient.Name = curSocket.LocalEndPoint.ToString();
    //            curDataClient.Port = ((IPEndPoint)curSocket.RemoteEndPoint).Port;
    //            curDataClient.Socket = curSocket;

    //            curDataClient.ClientDisconnected += new EventHandler(OnClientDisconnect);
    //            curDataClient.NewClientMessage += new EventHandler(OnClientMessage);

    //            curDataClient.WaitForData();

    //            string curIPPort = curDataClient._Socket.RemoteEndPoint.ToString();
    //            int tempClientID = int.Parse(curIPPort.Substring(curIPPort.LastIndexOf(".") + 1).Replace(":", ""));

    //            DataClients.Add(curDataClient.Port, curDataClient);

    //            mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);

    //            GlobalVars.Instance.LogEntry("New Client connected: " + curDataClient.Port, 98);

    //        }
    //        catch (ObjectDisposedException)
    //        {
    //            GlobalVars.Instance.LogEntry("OnClientConnection: Socket has been closed", 99);
    //        }
    //        catch (SocketException se)
    //        {
    //            int a = 0;
    //        }

    //    }

    //    private void OnClientMessage(object sender, EventArgs e)
    //    {
    //        DataClientMsg curClientMsgEventArgs = (DataClientMsg)e;
    //        NewMessage(this, curClientMsgEventArgs);
    //    }

    //    private void OnClientDisconnect(object sender, EventArgs e)
    //    {
    //        DataClientMsg curClientMsgEventArgs = (DataClientMsg)e;

    //        DataClients.Remove(curClientMsgEventArgs.ClientID);
    //        GlobalVars.Instance.LogEntry("Client Disconnected: " + ((DataClient)sender).Port, 98);

    //        if (onClientDisconnected != null)
    //        {
    //            onClientDisconnected(this, new MsgEventArgs(((DataClient)sender).Port.ToString()));
    //        }
    //    }

    //    // ==========================   Other/Utils  =======================================

    //    String GetIP()
    //    {
    //        String strHostName = Dns.GetHostName();

    //        // Find host by name
    //        IPHostEntry iphostentry = Dns.GetHostByName(strHostName);

    //        // Grab the first IP addresses
    //        String IPStr = "";
    //        foreach (IPAddress ipaddress in iphostentry.AddressList)
    //        {
    //            IPStr = ipaddress.ToString();
    //            return IPStr;
    //        }
    //        return IPStr;
    //    }

    //    public class MsgEventArgs : EventArgs
    //    {
    //        public MsgEventArgs(string __strMessage)
    //        {
    //            _strMessage = __strMessage;
    //        }

    //        private string _strMessage; public string strMessage { get { return _strMessage; } set { _strMessage = value; } }
    //    }

    //}

    //class DataClient
    //{
    //    public string Name = "";
    //    public int Port = 0;
    //    public Socket _Socket;
    //    private object SendLock = new object();

    //    string MessageBuffer = "";
    //    public AsyncCallback pfnWorkerCallBack;
    //    public Socket Socket { get { return _Socket; } set { _Socket = value; } }

    //    public event EventHandler NewClientMessage;
    //    public event EventHandler ClientDisconnected;

    //    public DataClient()
    //    {

    //    }

    //    public void SendMessage(string strMessage)
    //    {
    //        try
    //        {
    //            lock (SendLock)
    //            {

    //                Object objData = strMessage + (char)17;
    //                byte[] byData = System.Text.Encoding.ASCII.GetBytes(objData.ToString());

    //                if (_Socket != null)
    //                {
    //                    if (_Socket.Connected)
    //                    {
    //                        _Socket.Send(byData);
    //                    }
    //                }
    //            }
    //        }
    //        catch (SocketException se)
    //        {
    //            GlobalVars.Instance.LogEntry(se.ToString(), 99);
    //        }

    //    }

    //    void OnDataReceived(IAsyncResult asyn)
    //    {
    //        SocketPacket socketData = null;
    //        try
    //        {
    //            socketData = (SocketPacket)asyn.AsyncState;

    //            int iRx = 0;

    //            iRx = socketData.currentSocket.EndReceive(asyn);

    //            char[] chars = new char[iRx + 1];
    //            System.Text.Decoder d = System.Text.Encoding.UTF8.GetDecoder();
    //            int charLen = d.GetChars(socketData.dataBuffer, 0, iRx, chars, 0);
    //            System.String szData = new System.String(chars);

    //            ProcessMessage(szData);

    //            // Continue the waiting for data on the Socket
    //            WaitForData();
    //        }
    //        catch (ObjectDisposedException)
    //        {
    //            GlobalVars.Instance.LogEntry("OnDataReceived: Socket has been closed", 99);
    //            DataClientMsg thisMsg = new DataClientMsg();
    //            thisMsg.ClientID = Port;
    //            ClientDisconnected(this, thisMsg);
    //        }
    //        catch (SocketException se)
    //        {
    //            //if (se.ErrorCode == 10054)
    //            //{
    //            DataClientMsg thisMsg = new DataClientMsg();
    //            thisMsg.ClientID = Port;
    //            ClientDisconnected(this, thisMsg);
    //            //}
    //        }
    //    }

    //    private void ProcessMessage(string strMessage)
    //    {
    //        char testChar = strMessage[0];
    //        int i = Convert.ToInt16(testChar);
    //        if (i != 17)
    //        {
    //            MessageBuffer = MessageBuffer + testChar;
    //        }
    //        else
    //        {
    //            string lastMessage = MessageBuffer;

    //            DataClientMsg thisMsg = new DataClientMsg();
    //            thisMsg.ClientID = Port;
    //            thisMsg.strMessage = lastMessage;
    //            NewClientMessage(this, thisMsg);
    //            MessageBuffer = "";
    //        }
    //    }

    //    public void WaitForData()
    //    {
    //        try
    //        {
    //            if (pfnWorkerCallBack == null)
    //            {
    //                pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
    //            }
    //            SocketPacket theSocPkt = new SocketPacket();
    //            theSocPkt.currentSocket = _Socket;
    //            // Start receiving any data written by the connected client
    //            // asynchronously
    //            _Socket.BeginReceive(theSocPkt.dataBuffer, 0,
    //                               theSocPkt.dataBuffer.Length,
    //                               SocketFlags.None,
    //                               pfnWorkerCallBack,
    //                               theSocPkt);
    //        }
    //        catch (SocketException se)
    //        {
    //            if (se.ErrorCode == 10053 || se.ErrorCode == 10054)
    //            {
    //                int SocketPort = ((IPEndPoint)_Socket.LocalEndPoint).Port;
    //            }
    //            DataClientMsg thisMsg = new DataClientMsg();
    //            thisMsg.strMessage = "SOCK" + (char)16 + "DISC" + (char)16 + Port;
    //            thisMsg.ClientID = Port;
    //            ClientDisconnected(this, thisMsg);
    //        }

    //    }

    //    public void Dispose()
    //    {

    //    }

    //    public override string ToString()
    //    {
    //        return this.Socket.RemoteEndPoint.ToString();// +":" + this.Socket.LocalEndPoint.ToString();
    //    }

    //    public class SocketPacket
    //    {
    //        public System.Net.Sockets.Socket currentSocket;
    //        public byte[] dataBuffer = new byte[1];
    //    }
    //}


}
