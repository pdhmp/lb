using System;
using System.Collections.Generic;
using System.Text;
using QuickFix;

namespace QuickNestFIX
{
    public struct SessionMessage
    {
        public SessionID session;
        public SessionType sessionType;
        public Message message;
    }

    public class SymbolStatus
    {
        private string _Symbol;
        private double _NestBid;
        private double _NestAsk;
        private int _NestBidSize;
        private int _NestAskSize;
        private double _MktBid;
        private double _MktAsk;
        private int _MktBidSize;
        private int _MktAskSize;
        private double _MktLast;


        public string Symbol
        {
            get { return _Symbol; }
            set { _Symbol = value; }
        }
        public double NestBid
        {
            get { return _NestBid; }
            set { _NestBid = value; }
        }
        public double NestAsk
        {
            get { return _NestAsk; }
            set { _NestAsk = value; }
        }
        public int NestBidSize
        {
            get { return _NestBidSize; }
            set { _NestBidSize = value; }
        }
        public int NestAskSize
        {
            get { return _NestAskSize; }
            set { _NestAskSize = value; }
        }
        public double MktBid
        {
            get { return _MktBid; }
            set { _MktBid = value; }
        }
        public double MktAsk
        {
            get { return _MktAsk; }
            set { _MktAsk = value; }
        }
        public int MktBidSize
        {
            get { return _MktBidSize; }
            set { _MktBidSize = value; }
        }
        public int MktAskSize
        {
            get { return _MktAskSize; }
            set { _MktAskSize = value; }
        }
        public double MktLast
        {
            get { return _MktLast; }
            set { _MktLast = value; }
        }


    }

    public class OrderBookLine
    {
        private int _UnordBid;
        private int _LeavesBid;
        private double _PriceBid;
        private double _PriceAsk;
        private int _LeavesAsk;
        private int _UnordAsk;
        
        public int UnordBid
        {
            get { return _UnordBid; }
            set { _UnordBid = value; }
        }
        public int LeavesBid
        {
            get { return _LeavesBid; }
            set { _LeavesBid = value; }
        }
        public double PriceBid
        {
            get { return _PriceBid; }
            set { _PriceBid = value; }
        }
        public double PriceAsk
        {
            get { return _PriceAsk; }
            set { _PriceAsk = value; }
        }
        public int LeavesAsk
        {
            get { return _LeavesAsk; }
            set { _LeavesAsk = value; }
        }
        public int UnordAsk
        {
            get { return _UnordAsk; }
            set { _UnordAsk = value; }
        }
    }

    public class Order
    {
        private string _SessionID;
        private string _ClOrdID;
        private string _Symbol;
        private string _Side;
        private int _Quantity;
        private double _Price;

        public string SessionID { get { return _SessionID; } set { _SessionID = value; } }
        public string ClOrdID { get { return _ClOrdID; } set { _ClOrdID = value; } }
        public string Symbol { get { return _Symbol; } set { _Symbol = value; } }
        public string Side { get { return _Side; } set { _Side = value; } }
        public int Quantity { get { return _Quantity; } set { _Quantity = value; } }
        public double Price { get { return _Price; } set { _Price = value; } }
    }
}
