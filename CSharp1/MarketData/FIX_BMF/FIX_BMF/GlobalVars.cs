using System;
using System.Collections.Generic;
using System.Text;

namespace FIX_BMF
{
    public sealed class GlobalVars
    {
        static GlobalVars instance = null;
        static readonly object padlock = new object();

        public string strMessages = "";
        public bool isClosing = false;
        public bool SaveLog = false;
        public string SaveLogTicker = "";

        GlobalVars()
        {
        }

        public static GlobalVars Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance==null)
                    {
                        instance = new GlobalVars();
                    }
                    return instance;
                }
            }
        }

        public void AddMessage(string curMessage)
        {
            strMessages = strMessages + curMessage + "\r\n";
        }
    }

}