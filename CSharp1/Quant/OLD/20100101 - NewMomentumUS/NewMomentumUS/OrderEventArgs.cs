using System;
using System.Collections.Generic;
using System.Text;

namespace NewMomentumUS
{
    public class OrderEventArgs : EventArgs
    {
        public List<OrderInfo> OrderList;

        public OrderEventArgs() 
        {
            OrderList = new List<OrderInfo>();
        }

        public void AddOrder(int ID_Ticker, int Quantity, double Price)
        {
            OrderInfo order = new OrderInfo();
            order.ID_Ticker = ID_Ticker;
            order.Quantity = Quantity;
            order.Price = Price;

            OrderList.Add(order);
        }

        public struct OrderInfo
        {
            public int ID_Ticker;
            public int Quantity;
            public double Price;
        }
    }
}
