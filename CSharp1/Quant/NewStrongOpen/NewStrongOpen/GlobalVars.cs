using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewStrongOpen
{
    class GlobalVars
    {
        #region Singleton region

        private static GlobalVars _Instance = null;
        private static readonly object padlock = new object();
        public static GlobalVars Instance
        {
            get
            {
                if (_Instance == null)
                {
                    lock (padlock)
                    {
                        if (_Instance == null)
                        {
                            _Instance = new GlobalVars();
                        }
                    }
                }

                return _Instance;
            }
        }
        private GlobalVars() { }

        #endregion

        #region Attributes Region
        
        private int _RunCounter = 0;

        #endregion

        #region Properties Region

        public int RunCounter
        {
            get { return _RunCounter; }
            set { _RunCounter = value; }
        }

        #endregion

    }
}
