using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LiveDLL
{
    public class NUserControl
    {
        static NUserControl instance = null;
        static readonly object padlock = new object();

        public int User_Id = 0;

        public static NUserControl Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new NUserControl();
                    }
                    return instance;
                }
            }
        }

        public NUserControl()
        {

        }
    }
}
