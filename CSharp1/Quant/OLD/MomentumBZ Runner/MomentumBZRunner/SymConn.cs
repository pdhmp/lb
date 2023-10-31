using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using FlexTrade.FlexTrader.Api.Connection;
using FlexTrade.FlexTrader.Api.Defs.Sym;
using FlexTrade.FlexTrader.Api.Enumerate;
using FlexTrade.FlexTrader.Api.Value;

namespace MomentumBZRunner
{
    class SymConn : IDisposable  
    {
        static public SymConnection sym;
        static Thread SymListenThread;

        public event EventHandler OnData;
        
        public void Connect()
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

        static void symThread()
        {
            sym.Loop(1000);
        }

        public void Subscribe(string Ticker)
        {
            sym.Subscribe(Ticker);
        }

        void sym_OnData(string topic, FlexTrade.FlexTrader.Api.Value.SymData symData)
        {
            string Ticker = "";
            string[] FLID = new string[1000];
            double[] Value = new double[1000];

            Type enumType = typeof(SymFlids);
            foreach (KeyValuePair<int, Object> kvp in symData.GetDataMap())
            {
                SymFlids flid = (SymFlids)Enum.ToObject(enumType, kvp.Key);

                switch (flid.ToString())
                {
                    case "FLID_SYMBOL": Ticker = kvp.Value.ToString(); break;
                    case "FLID_BID": FLID[9] = flid.ToString(); Value[9] = Double.Parse(kvp.Value.ToString()); break;
                    case "FLID_ASK": FLID[10] = flid.ToString(); Value[10] = Double.Parse(kvp.Value.ToString()); break;
                    case "FLID_LAST": FLID[1] = flid.ToString(); Value[1] = Double.Parse(kvp.Value.ToString()); break;
                    case "FLID_BID_SIZE": FLID[26] = flid.ToString(); Value[26] = Double.Parse(kvp.Value.ToString()); break;
                    case "FLID_ASK_SIZE": FLID[27] = flid.ToString(); Value[27] = Double.Parse(kvp.Value.ToString()); break;
                    case "FLID_VOLUME": FLID[11] = flid.ToString(); Value[11] = Double.Parse(kvp.Value.ToString()); break;
                    case "FLID_TRADE_TIME": FLID[999] = flid.ToString(); Value[999] = Double.Parse(kvp.Value.ToString()); break;
                    default: break;
                }
            }

            SymDataEventArgs DataArgs = new SymDataEventArgs(Ticker, FLID, Value);

            OnData(this, DataArgs);

        }

        public void Dispose()
        {
            if (SymListenThread!= null)
            {
                SymListenThread.Abort();
            }
        }
    }
}
