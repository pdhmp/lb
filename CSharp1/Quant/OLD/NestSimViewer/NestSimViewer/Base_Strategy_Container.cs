using System;
using System.Collections.Generic;
using System.Text;

namespace NestQuant.Common
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
