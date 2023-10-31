using System;
using System.Collections.Generic;
using System.Text;

using NestSimDLL;

namespace NestSim
{
    class Base_Strategy_Container:Strategy_Container
    {
        public override void RunAll()
        {
            foreach (Strategy_Container CustomStrategyContainer in this.Containers)
            {
                CustomStrategyContainer.RunAll();
            }
        }
    }
}
