using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleDequeuerForm
{
    class OrderManagerContainter
    {
        public string OrderId { get; set; }
        public string Strategy { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public string OrderStatus { get; set; }
        public double AveragePrice { get; set; }
        public double OrderQuantity { get; set; }
        public double Executed { get; set; }
        public double Leaves { get; set; }
        public string UpdateTime { get; set; }
        public string ExtraInfo { get; set; }
        //class constructor
        public OrderManagerContainter(string _OrderId, string _Strategy, string _Symbol, string _Side, string _OrderStatus, double _AveragePrice, double _OrderQuantity, double _Executed, double _Leaves, string _UpdateTime, string _ExtraInfo)
        {
            this.OrderId = _OrderId;
            this.Strategy = _Strategy;
            this.Symbol = _Symbol;
            this.Side = _Side;
            this.OrderStatus = _OrderStatus;
            this.AveragePrice = _AveragePrice;
            this.OrderQuantity = _OrderQuantity;
            this.Executed = _Executed;
            this.Leaves = _Leaves;
            this.UpdateTime = _UpdateTime;
            this.ExtraInfo = _ExtraInfo;
        
        }
        //getters and setters
        public string getOrderId()
        {
            return this.OrderId;
        }
        public string getStrategy()
        {
            return this.Strategy;
        }
        public string getSymbol()
        {
            return this.Symbol;
        }
        public string getSide()
        {
            return this.Side;
        }
        public string getOrderStatus()
        {
            return this.OrderStatus;
        }
        public double getAveragePrice()
        {
            return this.AveragePrice;
        }
        public double getOrderQuantity()
        {
            return this.OrderQuantity;
        }
        public double getExecuted()
        {
            return this.Executed;
        }
        public double getLeaves()
        {
            return this.Leaves;
        }
        public string getUpdateTime()
        {
            return this.UpdateTime;
        }
        public string getExtraInfo()
        {
            return this.ExtraInfo;
        }
        //setters
        public void setOrderStatus(string _orderStatus)
        {
            this.OrderStatus = _orderStatus;
        }
        public void setAveragePrice(double _averagePrice)
        {
            this.AveragePrice = _averagePrice;
        }
        public void setOrderQuantity(double _orderQuantity)
        {
            this.OrderQuantity = _orderQuantity;
        }
        public void setExecuted(double _executed)
        {
            this.Executed = _executed;
        }
        public void setLeaves(double _leaves)
        {
            this.Leaves = _leaves;
        }
        public void setUpdateTime(string _updateTime)
        {
            this.UpdateTime = _updateTime;
        }
        public void setExtraInfo(string _extraInfo)
        {
            this.ExtraInfo = _extraInfo;            
        }
    }
}
