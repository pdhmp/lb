using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFIXConnLT;
using System.IO;

namespace LiveTrade2
{
    class FIXConnections
    {
        static FIXConnections instance = null;
        static readonly object padlock = new object();

        public static FIXConnections Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FIXConnections();
                    }
                    return instance;
                }
            }
        }

        public FIXConnLT curFixConn;

        public void InitializeFIX()
        {
            try
            {
                curFixConn = new NFIXConnLT.FIXConnLT(@"C:\LiveTrade\FIXConfig.cfg");

                if (!File.Exists(@"C:\LiveTrade\LTLimits.txt"))
                {
                    StreamWriter sw = new StreamWriter(@"C:\LiveTrade\LTLimits.txt");

                    sw.WriteLine("[MaxOrderAmount]=10000");
                    sw.WriteLine("[MaxOrderShares]=10000");
                    sw.WriteLine("[MaxTotalGross]=10000");
                    sw.WriteLine("[MaxTotalNet]=10000");
                    sw.WriteLine("[MaxTotalShares]=1000");

                    sw.WriteLine("[MaxContractsDI1]=1");
                    sw.WriteLine("[MaxContractsIND]=1");
                    sw.WriteLine("[MaxContractsDOL]=1");

                    sw.Close();
                }

                if (curFixConn != null)
                {
                    StreamReader sr = new StreamReader(@"C:\LiveTrade\LTLimits.txt");

                    string tempLine = "";
                    while ((tempLine = sr.ReadLine()) != null)
                    {
                        if (tempLine.Contains("[MaxOrderAmount]"))  curFixConn.curLimits.MaxOrderAmount = double.Parse(tempLine.Split('=')[1]);
                        if (tempLine.Contains("[MaxOrderShares]"))  curFixConn.curLimits.MaxOrderShares = double.Parse(tempLine.Split('=')[1]);
                        if (tempLine.Contains("[MaxTotalGross]"))   curFixConn.curLimits.MaxTotalGross  = double.Parse(tempLine.Split('=')[1]);
                        if (tempLine.Contains("[MaxTotalNet]"))     curFixConn.curLimits.MaxTotalNet    = double.Parse(tempLine.Split('=')[1]);
                        if (tempLine.Contains("[MaxTotalShares]"))  curFixConn.curLimits.MaxTotalShares = double.Parse(tempLine.Split('=')[1]);

                        if (tempLine.Contains("[MaxContractsDI1]")) curFixConn.curLimits.MaxContractsDI1 = double.Parse(tempLine.Split('=')[1]);
                        if (tempLine.Contains("[MaxContractsDOL]")) curFixConn.curLimits.MaxContractsDOL = double.Parse(tempLine.Split('=')[1]);
                        if (tempLine.Contains("[MaxContractsIND]")) curFixConn.curLimits.MaxContractsIND = double.Parse(tempLine.Split('=')[1]);
                    }
                }

                foreach (OrderLT curOrder in curFixConn.OrderList)
                {
                    curOrder.Fund = GlobalVars.Instance.GetPortName(curOrder.Account.getValue());
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
        }
    }
}
