using System;
using System.Collections.Generic;
using System.Text;

namespace NewValueBZ
{
    public class FastDataObject
    {
        static FastDataObject instance = null;

        static readonly object padlock = new object();

        public FastData curPrices = new FastData(1, new DateTime(2000, 01, 01), DateTime.Now);

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
    }
}
