using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCommonTypes;

namespace Tomahawk
{
    public class THSymbolLine
    {
        private int _IdSecurity = 0;
        private string _Ticker = "";
        private int _QtyBuy = 0;
        private double _PxBuy = 0;
        private int _QtySell = 0;
        private double _PxSell = 0;
        private int _ExecBuyQTY = 0;
        private double _ExecBuyPx = 0;
        private int _ExecSellQty = 0;
        private double _ExecSellPx = 0;
        private int _PrevPos = 0;
        private string _SecurityType = "";                 

        public int IdSecurity
        {
            get { return _IdSecurity; }
            set { _IdSecurity = value; }
        }
        public string Ticker
        {
            get { return _Ticker; }
            set { _Ticker = value; }
        }
        public int QtyBuy
        {
            get { return _QtyBuy; }
            set { _QtyBuy = value; }
        }
        public double PxBuy
        {
            get { return _PxBuy; }
            set { _PxBuy = value; }
        }
        public int QtySell
        {
            get { return _QtySell; }
            set { _QtySell = value; }
        }
        public double PxSell
        {
            get { return _PxSell; }
            set { _PxSell = value; }
        }
        public double Last
        {
            get { return MarketData.ValidLast; }
        }
        public int ExecBuyQTY
        {
            get { return _ExecBuyQTY; }
            set { _ExecBuyQTY = value; }
        }
        public double ExecBuyPX
        {
            get { return _ExecBuyPx; }
            set { _ExecBuyPx = value; }
        }
        public int ExecSellQty
        {
            get { return _ExecSellQty; }
            set { _ExecSellQty = value; }
        }
        public double ExecSellPx
        {
            get { return _ExecSellPx; }
            set { _ExecSellPx = value; }
        }
        public int PrevPos
        {
            get { return _PrevPos; }
            set { _PrevPos = value; }
        }
        public int NetPos
        {
            get
            {
                return _PrevPos + (_ExecBuyQTY - _ExecSellQty);
            }
        }
        public double NetValue
        {
            get { return NetPos * Last; }
        }
        public string SecurityType
        {
            get { return _SecurityType; }
            set { _SecurityType = value; }
        }

        public string TradingStatus
        {
            get
            {
                return MarketData.AucCond;
            }
        }

        public string WillExec
        {
            get
            {
                string result = "No";
                if (Last <= _PxBuy)
                {
                    result = "Buy";
                }
                else if (Last >= _PxSell)
                {
                    result = "Sell";
                }

                return result;

            }
        }

        public MarketDataItem MarketData = new MarketDataItem();
        
    }
}
