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
        private string _OrigClOrdID;
        private string _Symbol;
        private string _Side;
        private int _Quantity;
        private double _Price;
        private string _OrderID;
        private int _IDPortfolio;
        private int _IDBook;
        private int _IDSection;
        private bool _DontMatch;
        private string _IntCLOrd;
        private string _IntOrigClOrd;
        private int _ExecQty;
        private double _AvgPx;

        public string SessionID { get { return _SessionID; } set { _SessionID = value; } }
        public string ClOrdID { get { return _ClOrdID; } set { _ClOrdID = value; } }
        public string OrigClOrdID { get { return _OrigClOrdID; } set { _OrigClOrdID = value; } }
        public string Symbol { get { return _Symbol; } set { _Symbol = value; } }
        public string Side { get { return _Side; } set { _Side = value; } }
        public int Quantity { get { return _Quantity; } set { _Quantity = value; } }
        public double Price { get { return _Price; } set { _Price = value; } }
        public string OrderID { get { return _OrderID; } set { _OrderID = value; } }
        public int IDPortfolio { get { return _IDPortfolio; } set { _IDPortfolio = value; } }
        public int IDBook { get { return _IDBook; } set { _IDBook = value; } }
        public int IDSection { get { return _IDSection; } set { _IDSection = value; } }
        public bool DontMatch { get { return _DontMatch; } set { _DontMatch = value; } }
        public string IntCLOrd { get { return _IntCLOrd; } set { _IntCLOrd = value; } }
        public string IntOrigClOrd { get { return _IntOrigClOrd; } set { _IntOrigClOrd = value; } }
        public int ExecQty { get { return (_Side == "SELL" ? (-1) * _ExecQty : _ExecQty); } set { _ExecQty = value; } }
        public double AvgPx { get { return _AvgPx; } set { _AvgPx = value; } }
        public double ExecValue { get { return _AvgPx * ExecQty; } }

        public QuickFix42.NewOrderSingle OriginalOrder;
        
    }

    public class OrdersBySymbol
    {
        private bool _Selected = false;
        private int _IDPortfolio;
        private string _Symbol;
        private int _BuyQty;
        private int _SellQty;
        private int _TotalQty;

        public bool Selected { get { return _Selected; } set { _Selected = value; } }
        public int IDPortfolio { get { return _IDPortfolio; } set { _IDPortfolio = value; } }
        public string Symbol { get { return _Symbol; } set { _Symbol = value; } }
        public int BuyQty { get { return _BuyQty; } set { _BuyQty = value; } }
        public int SellQty { get { return _SellQty; } set { _SellQty = value; } }
        public int TotalQty { get { return _TotalQty; } set { _TotalQty = value; } }
    }

    public class SelectedItem : IComparable, IEquatable<SelectedItem>
    {
        private int _IDPortfolio;

        public int IDPortfolio
        {
            get { return _IDPortfolio; }
            set { _IDPortfolio = value; }
        }
        private string _Symbol;

        public string Symbol
        {
            get { return _Symbol; }
            set { _Symbol = value; }
        }

        public int CompareTo(object obj)
        {
            SelectedItem other = obj as SelectedItem;
            if (other != null)
            {
                if (this.IDPortfolio < other.IDPortfolio)
                {
                    return -1;
                }
                else if (this.IDPortfolio > other.IDPortfolio)
                {
                    return 1;
                }
                else
                {
                    return this.Symbol.CompareTo(other.Symbol);
                }
            }
            else
            {
                throw new ArgumentException("Object is not a SelectedItem");
            }
        }

        public bool Equals(SelectedItem other)
        {
            bool result = false;
            if (other.IDPortfolio == this.IDPortfolio)
            {
                if (other.Symbol == this.Symbol)
                {
                    result = true;
                }
            }

            return result;
        }
    }
}
