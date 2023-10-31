using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCommonTypes;

namespace LiveTrade2
{
    class TSLineItem : System.IEquatable<TSLineItem>
    {
        private int _IdSecurity = 0; public int IdSecurity { get { return _IdSecurity; } set { _IdSecurity = value; } }
        private string _Ticker = ""; public string Ticker { get { return _Ticker; } set { _Ticker = value; } }
        private string _Fund = ""; public string Fund { get { return _Fund; } set { _Fund = value; } }
        private string _Broker = ""; public string Broker { get { return _Broker; } set { _Broker = value; } }
        private double _LotSize = 0; public double LotSize { get { return _LotSize; } set { _LotSize = value; } }

        private int _Bought = 0; public int Bought { get { return _Bought; } set { _Bought = value; } }
        private int _Sold = 0; public int Sold { get { return _Sold; } set { _Sold = value; } }
        private int _Net = 0; public int Net { get { return _Net; } set { _Net = value; } }
        private double _AvgPriceBought = 0; public double AvgPriceBought { get { return _AvgPriceBought; } set { _AvgPriceBought = value; } }
        private double _AvgPriceSold = 0; public double AvgPriceSold { get { return _AvgPriceSold; } set { _AvgPriceSold = value; } }
        private double _CashFlow = 0; public double CashFlow { get { return _CashFlow; } set { _CashFlow = value; } }
        private double _Last = 0; public double Last { get { return _Last; } set { _Last = value; } }
        private string _Trader = "NA"; public string Trader { get { return _Trader; } set { _Trader = value; } }

        public double PnL { get { if (_Last != 0) return -(_Bought * _AvgPriceBought + _Sold * _AvgPriceSold - _Net * _Last) / _LotSize; else return 0; } }


        public bool Equals(TSLineItem obj)
        {
            if (this.Fund == obj.Fund 
                && this.IdSecurity == obj.IdSecurity 
                && this.Trader == obj.Trader
                && this.Broker == obj.Broker)
                return true;
            else
                return false;
        }
    }
}
