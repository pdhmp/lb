using QuickFix;
using QuickFix42;

namespace FeedXML2FIX
{
    internal static class Utils
    {
        internal static long CounterExecID;

        internal static ExecutionReport SimpleTradeToQuickFixMessage(SimpleTrade trade)
        {
            ExecutionReport oMessage = new ExecutionReport();

            oMessage.setField(new OrderID(trade.OrderID));
            oMessage.setField(new Symbol(trade.Symbol));
            oMessage.setField(new Side(trade.Side));
            oMessage.setField(new OrderQty(trade.Quantity));
            oMessage.setField(new CumQty(trade.Quantity));
            oMessage.setField(new Price(trade.Price));
            oMessage.setField(new LastPx(trade.Price));
            oMessage.setField(new TransactTime(trade.TransactTime));

            oMessage.setField(new OrdStatus(OrdStatus.FILLED));
            oMessage.setField(new ExecID("xml" + ++CounterExecID));
            
            //oMessage.setField(new LastShares(trade.Quantity));
            //oMessage.setField(new Account());
            //oMessage.setField(new ExecBroker());
            
            return oMessage;
        }

        internal static enum SideType
        {
            Unknown = Side.UNDISCLOSED,
            Buy = Side.BUY,
            Sell = Side.SELL
        }
        
    }


    
}
