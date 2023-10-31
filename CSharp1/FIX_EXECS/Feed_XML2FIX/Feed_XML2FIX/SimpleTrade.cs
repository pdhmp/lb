using System;
namespace FeedXML2FIX
{
    internal class SimpleTrade
    {
        public string User { get; private set; }
        public string BrokerID { get; private set; }
        public string Symbol { get; private set; }
        public char Side { get; private set; }
        public double Quantity { get; private set; }
        public double Price { get; private set; }
        public string OrderID { get; private set; }
        public string CodClient { get; private set; }
        public string BrokerOrderID { get; private set; }
        public DateTime TransactTime { get; private set; }

        public SimpleTrade(string user, string brokerID, string symbol, string side, string qty, string price, string orderID, string codClient, string brokerOrderID, string transactTime)
        {
            double dTempValue;
            DateTime dtTempValue;

            User = user;
            BrokerID = brokerID;
            Symbol = symbol;
            switch (side.ToLower())
            {
                case "0":
                case "b":
                case "buy":
                    Side = (char)Utils.SideType.Buy;
                    break;
                case "1":
                case "s":
                case "sell":
                case "sellshort":
                    Side = (char)Utils.SideType.Sell;
                    break;
                default:
                    Side = (char)Utils.SideType.Unknown;
                    break;
            }
            Quantity = double.TryParse(qty, out dTempValue) ? dTempValue : 0;
            Price = double.TryParse(price, out dTempValue) ? dTempValue : 0; 
            OrderID = orderID;
            CodClient = codClient;
            BrokerOrderID = brokerOrderID;
            TransactTime = DateTime.TryParse(transactTime, out dtTempValue) ? dtTempValue : DateTime.Now;
        }
    }
}