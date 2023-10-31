using System;
using System.Collections.Generic;
using System.Text;

namespace RatesBZ
{
    class St_OrderEventArgs : EventArgs
    {
        private string _InternalOrderID;
        
        public string InternalOrderID
        {
            get { return _InternalOrderID; }
            set { _InternalOrderID = value; }
        }
        
        private int _Id_Ticker;

        public int Id_Ticker
        {
            get { return _Id_Ticker; }
            set { _Id_Ticker = value; }
        }

        private int _Quantity;

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        private double _Price;

        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        private St_OrderEventArgs() { }

        public St_OrderEventArgs(string __InternalOrderID, int __Id_Ticker, int __Quantity, double __Price) 
        {
            _Id_Ticker = __Id_Ticker;
            _Quantity = __Quantity;
            _Price = __Price;
            _InternalOrderID = __InternalOrderID;
        }


    }
}
