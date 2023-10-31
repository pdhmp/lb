using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDataDist
{
    public sealed class GlobalVars
    {
        static GlobalVars instance = null;
        static readonly object padlock = new object();

        public bool appStarting = false;
        public bool appClosing = false;
        public int curUser = 0;

        public int DebugMode = 1;
        public int DebugLevel = 5;

        GlobalVars()
        {

        }

        public static GlobalVars Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GlobalVars();
                    }
                    return instance;
                }
            }
        }

        public void LogEntry(string strMessage, int Priority)
        {
            if (DebugMode == 1) 
            {
                if (Priority >= DebugLevel) Console.WriteLine(strMessage);
            }
            //System.Diagnostics.Debugger.Log(0, "1", "\nOnDataReceived: Socket has been closed\n");
        }
        
    }
}
