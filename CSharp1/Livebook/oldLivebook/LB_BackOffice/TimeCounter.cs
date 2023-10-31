using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LiveBook.LB_BackOffice
{
    public class TimeCounter
    {
       
            DateTime tempStart;
            public TimeSpan ElapsedTime = new TimeSpan(0, 0, 0);

            public void Start()
            {
                tempStart = DateTime.Now;
            }

            public void End()
            {
                ElapsedTime += DateTime.Now.Subtract(tempStart);
            }

            public void Reset()
            {
                ElapsedTime = new TimeSpan(0, 0, 0);
            }
        }
    
}
