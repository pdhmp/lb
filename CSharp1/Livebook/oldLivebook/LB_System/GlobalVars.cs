using System;
using System.Collections.Generic;
using System.Text;

namespace LiveBook
{
    public sealed class GlobalVars
    {
        static GlobalVars instance = null;
        static readonly object padlock = new object();

        public bool appStarting = false;
        public bool appClosing = false;
        public bool appCloseTimerFired = false;

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
    }
}
