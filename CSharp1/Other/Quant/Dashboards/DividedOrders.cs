using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboards
{
    class DividedOrders
    {
        public string OrderId { get; set; }
        public string Strategy { get; set; }
        public string Symbol { get; set; }
        public double Total { get; set; }
        public double Broker { get; set; }
        public double Virtual { get; set; }

        public DividedOrders(string szOrderId, string szStrategy, string szSymbol, double dbTotal, double dbBroker, double dbVirtual)
        {
            this.OrderId = szOrderId;
            this.Strategy = szStrategy;
            this.Symbol = szSymbol;
            this.Total = dbTotal;
            this.Broker = dbBroker;
            this.Virtual = dbVirtual; 
        }

        public void setOrderId(string szOrderId)
        {
            this.OrderId = szOrderId;
        }
        public void setStrategy(string szStrategy)
        {
            this.Strategy = szStrategy;
        }
        public void setSymbol(string szSymbol)
        {
            this.Symbol = szSymbol;
        }
        public void setTotal(double dbTotal)
        {
            this.Total = dbTotal;
        }
        public void setBroker(double dbBroker)
        {
            this.Broker = dbBroker;
        }
        public void setVirtual(double dbVirtual)
        {
            this.Virtual = dbVirtual;
        }
        public string getSymbol()
        {
            return this.Symbol;
        }
        public string getStrategy()
        {
            return this.Strategy;
        }
        public string getOrderId()
        {
            return this.OrderId;
        }
    }
}
