using System;
using System.Collections.Generic;
using System.Text;

namespace NestCommDLL
{
    public class Level2Item : EventArgs
    {

        private string _Ticker = "";

        public string Ticker
        {
            get { return _Ticker; }
            set { _Ticker = value; }
        }

        private char _Side = '0';

        public char Side
        {
            get { return _Side; }
            set { _Side = value; }
        }

        private int _Position;

        public int Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        private int _Broker = 0;

        public int Broker
        {
            get { return _Broker; }
            set { _Broker = value; }
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

    }

    public class Level2Line 
    {

        private int _Position;

        public int Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        // ====================== BID Data ==============

        private string _BrokerBid = "";

        public string BrokerBid
        {
            get { return _BrokerBid; }
            set { _BrokerBid = value; }
        }

        private int _QuantityBid;

        public int QuantityBid
        {
            get { return _QuantityBid; }
            set { _QuantityBid = value; }
        }

        private double _PriceBid;

        public double PriceBid
        {
            get { return _PriceBid; }
            set { _PriceBid = value; }
        }

        // ====================== ASK Data ==============

        private double _PriceAsk;

        public double PriceAsk
        {
            get { return _PriceAsk; }
            set { _PriceAsk = value; }
        }

        private int _QuantityAsk;

        public int QuantityAsk
        {
            get { return _QuantityAsk; }
            set { _QuantityAsk = value; }
        }

        private string _BrokerAsk = "";

        public string BrokerAsk
        {
            get { return _BrokerAsk; }
            set { _BrokerAsk = value; }
        }

        // ====================== Level/Color control ==============

        private double _LevelBid;

        public double LevelBid
        {
            get { return _LevelBid; }
            set { _LevelBid = value; }
        }

        private double _LevelAsk;

        public double LevelAsk
        {
            get { return _LevelAsk; }
            set { _LevelAsk = value; }
        }

    }

}
