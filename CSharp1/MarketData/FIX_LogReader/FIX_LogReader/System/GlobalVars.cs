using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using NestDLL;
using System.Data;

namespace FixLogViewer
{
    public sealed class GlobalVars
    {
        static GlobalVars instance = null;
        static readonly object padlock = new object();

        public int RunCounter = 0;

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

        public int getCashId(int IdCurrency)
        {
            switch (IdCurrency)
            {
                case 900: return 1844;
                case 1042: return 5791;
                case 929: return 31057;
                case 933: return 76743;
                default: return 1844;
            }
        }





    }
}
