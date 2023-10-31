using System;
using System.Collections.Generic;
using System.Text;

namespace NewValueBZ
{
    public class FastDataObject
    {
        static FastDataObject instance = null;

        static readonly object padlock = new object();

        public FastData curPrices;
        public FastData curTR;

        public FastData curShares;
        public FastDataEPS curEPS;
        public FastData curLotData;

        private DateTime _IniDatePrices;
        private DateTime _IniDateShareData;

        public DateTime IniDatePrices { get {return _IniDatePrices; } set{if(!IsInitialized) _IniDatePrices = value;} }
        public DateTime IniDateShareData { get {return _IniDateShareData; } set{if(!IsInitialized) _IniDateShareData = value;} }
        
        public bool IsInitialized = false;

        public static FastDataObject Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new FastDataObject();
                    }
                    return instance;
                }
            }
        }

        public FastDataObject()
        {
        }

        public void InitializeObjects()
        {
            if (!IsInitialized)
            {
                curPrices = new FastData(1, _IniDatePrices, DateTime.Now, 1);
                curTR = new FastData(100, _IniDatePrices, DateTime.Now, 1);

                curShares = new FastData(503, _IniDateShareData, DateTime.Now, 9);
                curEPS = new FastDataEPS(_IniDateShareData, DateTime.Now);
                //curEPSDates = new FastData(999, _IniDateShareData, DateTime.Now, 0);
                IsInitialized = true;
            }
        }
    }
}
