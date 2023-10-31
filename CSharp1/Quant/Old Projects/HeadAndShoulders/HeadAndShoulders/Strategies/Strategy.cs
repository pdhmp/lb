using System;
using System.Collections.Generic;

namespace NestSim
{
    public abstract class Strategy : Strategy_Base
    {

        public List<Price_Table> PriceTables = new List<Price_Table>();
        public List<Weight_Table> WeightTables = new List<Weight_Table>();
        public List<Price_Table> SignalTables = new List<Price_Table>();

        public Strategy()
        {

        }

        public abstract void RunStrategy();



    }
}
