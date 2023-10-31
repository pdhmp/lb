using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Dashboards
{
    class OrderViewer
    {
        public string Symbol { get; set; }
        public double Action { get; set; }
        public double PositionDm1 { get; set; }
        public double PositionD0 { get; set; }
        public double Last { get; set; }
        public string OrderStatus { get; set; }
        //class constuctor
        public OrderViewer(string symbol, double last, double posdm1, double pos0, double action, string orderStatus)
        {
            this.Symbol = symbol;
            this.Last = last;
            this.PositionDm1 = posdm1;
            this.PositionD0 = pos0;
            this.Action = action;
            this.OrderStatus = orderStatus;
        }
        public void setPositionDm1(double _positiondm1)
        {
            this.PositionDm1 = _positiondm1;
        }
        public void setPositionD0(double _positiond0)
        {
            this.PositionD0 = _positiond0;
        }
        public void setAction(double _action)
        {
            this.Action = _action;
        }
        public void setOrderStatus(string _orderStatus)
        {
            this.OrderStatus = _orderStatus;
        }
        public string getSymbol()
        {
            return this.Symbol;
        }
    }    
}
