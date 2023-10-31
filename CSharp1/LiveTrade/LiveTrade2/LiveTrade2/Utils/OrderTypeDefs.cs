using System;
using System.Collections.Generic;
using System.Text;

namespace LiveTrade2
{
    class NewOrderDef : EventArgs
    {
        public int Id_Ticker;
        public string Ticker;
        public int Shares;
        public double Price;
        public string OrderType = "";

        public NewOrderDef(int _Id_Ticker, string _Ticker, int _Shares, double _Price, string _OrderType)
        {
            this.Id_Ticker = _Id_Ticker;
            this.Ticker = _Ticker;
            this.Shares = _Shares;
            this.Price = _Price;
            this.OrderType = _OrderType;
        }    
    }

    class ReplaceOrderDef : EventArgs
    {
        public string ExternalID;
        public double newPrice;

        public ReplaceOrderDef(string _ExternalID, double _newPrice)
        {
            this.ExternalID = _ExternalID;
            this.newPrice = _newPrice;
        }
    }
}
