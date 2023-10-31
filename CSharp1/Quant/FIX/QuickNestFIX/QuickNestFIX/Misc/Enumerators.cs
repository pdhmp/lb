using System;
using System.Collections.Generic;
using System.Text;

namespace QuickNestFIX
{
    public enum SessionStatus
    {
        Disconnected,
        Connected
    }

    public enum SessionType
    {
        ToStreet,
        FromNest,
        DropCopy
    }

    public enum OrderSide
    {
        BUY,
        SELL
    }

    public enum OrderType
    {
        Limited,
        Market,
        IOC
    }
   
    public enum OrderCategory
    {
        ByPass,
        Hold,
        Imediate,
        ImediateOrHold
    }    
}
