using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;

using FlexTrade.FlexTrader.Api.Connection;
using FlexTrade.FlexTrader.Api.Defs.Sym;
using FlexTrade.FlexTrader.Api.Enumerate;
using FlexTrade.FlexTrader.Api.Value;
using FlexTrade.FlexTrader.Api.Defs.Depth;


using FlexTrade.FlexTrader.Api.Core;

namespace NestCommDLL
{
    public sealed class SymConn : IDisposable 
    {
        static public SymConnection sym;
        static public DepthConnection symdepth;
        static public DepthConnection symdepthBSE;

        static Thread SymListenThread;
        static Thread SymDepthListenThread;
        static Thread SymDepthBSEListenThread;        

        public event EventHandler OnData;
        public event EventHandler OnDepth;        

        static SymConn instance = null;
        static readonly object padlock = new object();

        public string _SymName= "";        

        public static SymConn Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new SymConn("Inst");
                    }
                    return instance;
                }
            }
        }

        public SymConn(string SymName)
        {
            _SymName = SymName;
            ReadFlidConfig();
        }

        

        public bool IsSymConnected
        {
            get { return sym.IsConnected(); }
        }
	

        public void ConnectSym()
        {
            sym = new SymConnection();

            try
            {
                sym.Connect("192.168.0.133", 14132);
            }
            catch (FtConnectionException e)
            {
                MessageBox.Show("Connection to sym failed");
            }

            sym.OnData += new FlexTrade.FlexTrader.Api.Defs.Sym.OnDataHandler(sym_OnData);

            SymListenThread = new Thread(new ThreadStart(symThread));
            SymListenThread.Start();

        }

        public void ConnectDepth()
        {
            symdepth = new DepthConnection();

            try
            {
                symdepth.Connect("192.168.0.133", 14133);   // DIST - REUT
            }
            catch (FtConnectionException e)
            {
                MessageBox.Show("Connection to symDepth failed");
            }

            symdepth.onDepth += sym_OnDepth;

            SymDepthListenThread = new Thread(new ThreadStart(symDepthThread));
            SymDepthListenThread.Start();

        }

        public void ConnectDepthBSE()
        {
            symdepthBSE = new DepthConnection();

            try
            {
                symdepthBSE.Connect("192.168.0.133", 14000);  // SYMDEPTH - BSE
            }
            catch (FtConnectionException e)
            {
                MessageBox.Show("Connection to symDepth failed");
            }

            symdepthBSE.onDepth += sym_OnDepth;

            if (SymDepthListenThread == null)
            {
                SymDepthBSEListenThread = new Thread(new ThreadStart(symDepthBSEThread));
                SymDepthBSEListenThread.Start();
            }

        }

        static void symThread()
        {
            sym.Loop(1000);
        }

        static void symDepthThread()
        {
            symdepth.Loop(1000);
        }

        static void symDepthBSEThread()
        {
            symdepthBSE.Loop(1000);
        }

        public void Subscribe(string Ticker)
        {
            if (sym == null)
            {
                ConnectSym();
            }

            if (sym.IsConnected())
            {
                AddSymbol(Ticker);
                sym.Subscribe(Ticker);
            }
        }

        public void UnSubscribe(string Ticker)
        {
            if (sym == null)
            {
                ConnectSym();
            }
            if (sym.IsConnected())
            {
                List<string> curTickers = new List<string>();
                curTickers.Add(Ticker);
                sym.UnSubscribe(curTickers);
            }
        }

        public void SubscribeDepth(string Ticker, int Exchange)
        {
            if (Exchange == 2)
            {
                if (symdepthBSE == null)
                {
                    ConnectDepthBSE();
                }

                if (symdepthBSE.IsConnected())
                {
                    symdepthBSE.Subscribe(Ticker, DepthSourceType.BSE);
                }
            }
            else
            {
                if (symdepth == null)
                {
                    ConnectDepth();
                }

                if (symdepth.IsConnected())
                {
                    symdepth.Subscribe(Ticker, DepthSourceType.REUT);
                }
            }

        }
        
        public void UnSubscribeDepth(string Ticker, int Exchange)
        {
            if (Exchange == 2)
            {
                if (symdepthBSE != null)
                {
                    if (symdepthBSE.IsConnected())
                    {
                        symdepthBSE.UnSubscribe(Ticker, DepthSourceType.BSE);
                    }
                }
            }
            else
            {
                if (symdepth != null)
                {
                    if (symdepth.IsConnected())
                    {
                        symdepth.UnSubscribe(Ticker, DepthSourceType.REUT);
                    }
                }
            }

        }

        void sym_OnData(string topic, FlexTrade.FlexTrader.Api.Value.SymData symData)
        {
            string Ticker = "";
            string[] FLID = new string[FLIDsSize];
            double[] Value = new double[FLIDsSize];
            string[] Text = new string[FLIDsSize];
            string[] Type = new string[FLIDsSize];
            bool[] IsSet = new bool[FLIDsSize];

            List<int> receivedFlids = new List<int>();

            Type enumType = typeof(SymFlids);
            foreach (KeyValuePair<int, Object> kvp in symData.GetDataMap())
            {
                SymFlids flid = (SymFlids)Enum.ToObject(enumType, kvp.Key);

                string type = "";

                if (FlidsConfig.TryGetValue(kvp.Key, out type))
                {
                    receivedFlids.Add(kvp.Key);

                    IsSet[kvp.Key] = true;
                    FLID[kvp.Key] = flid.ToString();
                    Type[kvp.Key] = type;

                    if (kvp.Key == 1024)
                    {
                        Ticker = kvp.Value.ToString();
                    }

                    switch (type)
                    {
                        case "NUMERIC": Value[kvp.Key] = Double.Parse(kvp.Value.ToString()); break;
                        case "TEXT": Text[kvp.Key] = kvp.Value.ToString(); break;
                        case "DATETIME": Text[kvp.Key] = (new DateTime(1970, 1, 1, 0, 0, 0)).Add(TimeSpan.FromSeconds(Double.Parse(kvp.Value.ToString()))).ToString(); break;
                        default: break;
                    }
                }
            }

            SymDataEventArgs DataArgs = new SymDataEventArgs(Ticker, FLID, Value, Text, Type, IsSet);
            UpdateFLID(Ticker, FLID, Value, Text, Type, IsSet, receivedFlids);


            if (OnData != null)
            {
                OnData(this, DataArgs);
            }

        }

        public void sym_OnDepth(RTPDepthData depth)
        {
            //Console.WriteLine("Received depth\n" + depth.ToString());

            for (int i = 0; i < depth.DepthLevelDataList.Count; i++)
            {
                Level2Item curItem = new Level2Item();
                curItem.Ticker = depth.Symbol;
                curItem.Position = depth.DepthLevelDataList[i].Level;
                curItem.Price = depth.DepthLevelDataList[i].Price;
                curItem.Quantity = depth.DepthLevelDataList[i].Size;

                if (depth.DepthLevelDataList[i].ToString().Contains("RTPDepthExtendedLevelData"))
                {
                    string curBroker = ((RTPDepthExtendedLevelData)depth.DepthLevelDataList[i]).ID;
                    if (curBroker != "")
                    {
                        try
                        {
                            curItem.Broker = int.Parse(curBroker.ToString().Replace("+", ""));
                        }
                        catch 
                        {
                            curItem.Broker = 0;
                        }
                        if(curItem.Broker == 0)
                        {
                            int a = 0;
                        }
                    }
                    else
                    { 
                        int a = 0;
                    }
                    curItem.Side = depth.DepthLevelDataList[i].Type;
                }
                else
                {
                    curItem.Broker = 0;
                    curItem.Side = depth.DepthLevelDataList[i].Type;
                }

                if (curItem.Price != -999999999)
                {
                    if (OnDepth != null)
                    {
                        OnDepth(this, curItem);
                    }
                }
            }
        }
        
        public void CloseAllConn()
        {
            if (SymListenThread != null) 
            { 
                SymListenThread.Abort();
                sym.Disconnect();
                sym.Dispose();
            };
            if (SymDepthListenThread != null) 
            {
                symdepth.Disconnect();
                SymDepthListenThread.Abort();
                symdepth.Dispose();
            };
        }

        public void Dispose()
        {
            if (SymListenThread != null)
            {
                SymListenThread.Abort();
                sym.Disconnect();
                sym.Dispose();
            };
            if (SymDepthListenThread != null)
            {
                symdepth.Disconnect();
                SymDepthListenThread.Abort();
                symdepth.Dispose();
            };
            if (SymDepthBSEListenThread != null)
            {
                symdepthBSE.Disconnect();
                SymDepthBSEListenThread.Abort();
                symdepthBSE.Dispose();
            };
        }

        #region Last Received Data
        
        private SortedDictionary<string, string[]> FLIDsByTicker = new SortedDictionary<string, string[]>();
        private SortedDictionary<string, double[]> ValuesByTicker = new SortedDictionary<string, double[]>();
        private SortedDictionary<string, string[]> TextByTicker = new SortedDictionary<string, string[]>();
        private SortedDictionary<string, string[]> TypesByTicker = new SortedDictionary<string, string[]>();
        private SortedDictionary<string, bool[]> IsSetByTicker = new SortedDictionary<string, bool[]>();
        private Mutex MainMutex = new Mutex();

        private void AddSymbol(string Symbol)
        {
            if (!FLIDsByTicker.ContainsKey(Symbol))
            {
                MainMutex.WaitOne();
                if (!FLIDsByTicker.ContainsKey(Symbol))
                {
                    string[] FLID = new string[FLIDsSize];
                    double[] Value = new double[FLIDsSize];
                    string[] Text = new string[FLIDsSize];
                    string[] Type = new string[FLIDsSize];
                    bool[] IsSet = new bool[FLIDsSize];

                    FLIDsByTicker.Add(Symbol, FLID);
                    ValuesByTicker.Add(Symbol, Value);
                    TextByTicker.Add(Symbol, Text);
                    TypesByTicker.Add(Symbol, Type);
                    IsSetByTicker.Add(Symbol, IsSet);
                }
                MainMutex.ReleaseMutex();
            }
        }
        private void UpdateFLID(string Ticker, string[] FLID, double[] Value, string[] Text, 
                                string[] Type, bool[] IsSet, List<int> receivedFlids)
        {
            foreach (int flidID in receivedFlids)
            {
                IsSetByTicker[Ticker][flidID] = IsSet[flidID];
                FLIDsByTicker[Ticker][flidID] = FLID[flidID];
                TypesByTicker[Ticker][flidID] = Type[flidID];

                switch (Type[flidID])
                {
                    case "NUMERIC":
                        ValuesByTicker[Ticker][flidID] = Value[flidID];
                        break;
                    case "TEXT":
                        TextByTicker[Ticker][flidID] = Text[flidID];
                        break;
                    case "DATETIME":
                        TextByTicker[Ticker][flidID] = Text[flidID];
                        break;
                    default:
                        break;
                }

            }
        }

        public bool TryGetValue(string Symbol, int FLID, out double Value)
        {
            object aux = null;
            if (TryGetValue(Symbol, FLID, out aux))
            {
                Value = (double)aux;
                return true;
            }
            else
            {
                Value = double.NaN;
                return false;                
            }
        }
        public bool TryGetValue(string Symbol, int FLID, out object Value)
        {
            if (IsSetByTicker[Symbol][FLID])
            {
                switch (TypesByTicker[Symbol][FLID])
                {
                    case "NUMERIC":
                        Value = ValuesByTicker[Symbol][FLID];
                        return true;
                    case "TEXT":
                        Value = TextByTicker[Symbol][FLID];
                        return true;
                    case "DATETIME":
                        Value = TextByTicker[Symbol][FLID];
                        return true;
                    default:
                        Value = null;
                        return false;
                }
            }
            else
            {
                Value = null;
                return false;
            }
        }

        public bool TryGetName(string Symbol, int FLID, out string Name)
        {
            if (IsSetByTicker[Symbol][FLID])
            {
                Name = FLIDsByTicker[Symbol][FLID];
                return true;
            }
            else
            {
                Name = null;
                return false;
            }
        }

        #endregion

        #region FLID Config

        private SortedDictionary<int, string> FlidsConfig = new SortedDictionary<int, string>();

        private static int FLIDsSize = 0;

        private void ReadFlidConfig()
        {
            try
            {
                StreamReader configFile = new StreamReader(@"T:\AppConfigFiles\SymData\FlidConfig.txt");

                while (!configFile.EndOfStream)
                {
                    string[] line = configFile.ReadLine().Split(',');

                    int flid = Convert.ToInt32(line[0]);
                    string type = line[1];
                    FlidsConfig.Add(flid, type);
                    FLIDsSize = (FLIDsSize > flid + 1 ? FLIDsSize : flid + 1);
                }
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("Unable to open FlidConfig at T:\\AppConfigFiles\\SymData\\FlidConfig.txt");
            }
            catch (Exception e)
            {
                MessageBox.Show("General error while reading or processing T:\\AppConfigFiles\\SymData\\FlidConfig.txt.");
            }
            
        }

        #endregion
    }

}
