using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using newNFIXConn;
using NCommonTypes;
using NestSymConn;

namespace Jericho
{
    public class Jericho
    {

        private newFIXConn curFixConn;
        private string OrderPrefix = "JRCH";

        public List<FIXOrder> Orders = new List<FIXOrder>();
        private volatile object OrdersSync = new object();

        public List<JerichoSec> Securities = new List<JerichoSec>();
        private SortedDictionary<string, int> SecuritiesIndex = new SortedDictionary<string, int>();

        private SortedDictionary<string, MarketDataItem> MarketData = new SortedDictionary<string, MarketDataItem>();
        private volatile object MarketDataSync = new object();
        

        public Jericho()
        {
            curFixConn = new newFIXConn(@"T:\Log\FIX\Jericho\config\JerichoConfig.cfg");
            curFixConn.OnUpdate += new EventHandler(OrderUpdate);

            SymConn.Instance.BovespaSource = NCommonTypes.NEnuns.NSYMSources.FLEXBSE;
            SymConn.Instance.OnData += new EventHandler(OnMarketData);
        }

        void OnMarketData(object sender, EventArgs e)
        {
            MarketUpdateList curList = (MarketUpdateList)e;

            foreach (MarketUpdateItem curItem in curList.ItemsList)
            {
                lock (MarketDataSync)
                {
                    if (MarketData.ContainsKey(curItem.Ticker))
                    {
                        MarketData[curItem.Ticker].Update(curItem);
                    }
                    if (SecuritiesIndex.ContainsKey(curItem.Ticker))
                    {
                        Securities[SecuritiesIndex[curItem.Ticker]].Last = MarketData[curItem.Ticker].ValidLast;
                    }
                }
            }
        }

        private void OrderUpdate(object sender, EventArgs e)
        {
            lock (OrdersSync)
            {
                FIXOrder curOrder = (FIXOrder)e;
                bool found = false;

                for (int i = 0; i < Orders.Count; i++)
                {
                    if (curOrder.OrigClOrdID == Orders[i].OrigClOrdID)
                    {
                        Orders[i] = curOrder;
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Orders.Add(curOrder);
                }

                SecurityUpdate(curOrder.IdTicker);
            }
        }
        private void SecurityUpdate(int IdSecurity)
        {
            int BuyQty = 0;
            int SellQty = 0;
            double BuyValue = 0;
            double SellValue = 0;
            string Security = "";
            double Last = 0;

            foreach (FIXOrder curOrder in Orders)
            {
                if (curOrder.IdTicker == IdSecurity)
                {
                    Security = curOrder.Symbol;
                    if (curOrder.strSide == "BUY")
                    {
                        BuyQty += (int)curOrder.Done;
                        BuyValue += curOrder.ExecPrice * curOrder.Done;
                    }
                    else if (curOrder.strSide == "SELL")
                    {
                        SellQty += (int)curOrder.Done;
                        SellValue += curOrder.ExecPrice * curOrder.Done;
                    }
                }
            }

            lock (MarketDataSync)
            {
                Last = MarketData[Security].ValidLast;
            }

            JerichoSec curSecurity = new JerichoSec();
            curSecurity.Security = Security;
            curSecurity.IdSecurity = IdSecurity;
            curSecurity.BuyQty = BuyQty;
            curSecurity.BuyAvgPx = (BuyQty > 0 ? BuyValue / (double)BuyQty : 0);
            curSecurity.SellQty = SellQty;            
            curSecurity.Last = Last;                       

            if (SecuritiesIndex.ContainsKey(Security))
            {
                Securities[SecuritiesIndex[Security]] = curSecurity;
            }
            else
            {
                Securities.Add(curSecurity);
                UpdateSecuritiesIndex();
            }            
        }
        private void UpdateSecuritiesIndex()
        {
            SecuritiesIndex.Clear();

            for (int i = 0; i < Securities.Count; i++)
            {
                SecuritiesIndex.Add(Securities[i].Security, i);
            }
        }
        private void UpdateHedge()
        {
            int opPos = 0;
            double PosCash = 0;
            double HdgCash = 0;
            int act = 0;

            foreach (JerichoSec curSec in Securities)
            {
                if (curSec.IdSecurity != HdgIdSecurity)
                {
                    if (curSec.PositionOpen) { opPos++; }
                    PosCash += curSec.NetValue;
                }
                else
                {
                    HdgCash = curSec.NetValue;
                }
            }



            if (opPos > OpPosThreshold)
            {
                _HedgeEnabled = true;
            }            
        }

        #region HedgeProperties

        private int OpPosThreshold = 65;
        private int HdgIdSecurity;
        private string HdgSecurity;

        private int _OpenPositions;
        private bool _HedgeEnabled = false;
        private double _PositionCash;
        private double _HedgeCash;
        private int _Action;

        public int OpenPositions
        {
            get { return _OpenPositions; }
            set { _OpenPositions = value; }
        }
        public bool HedgeEnabled
        {
            get { return _HedgeEnabled; }
            set { _HedgeEnabled = value; }
        }
        public double PositionCash
        {
            get { return _PositionCash; }
            set { _PositionCash = value; }
        }
        public double HedgeCash
        {
            get { return _HedgeCash; }
            set { _HedgeCash = value; }
        }
        public int Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        private void LoadHedgeSecuritySettings()
        {
            HdgIdSecurity = 209842;
            HdgSecurity = "WINZ11";
        }

        #endregion

        private void SubscribeTicker(string Security,int IdSecurity)
        {
            if (!MarketData.ContainsKey(Security))
            {
                MarketDataItem curItem = new MarketDataItem();
                curItem.Ticker = Security;
                curItem.IdTicker = IdSecurity;

                MarketData.Add(Security, curItem);
                SymConn.Instance.Subscribe(Security, 2);                
            }
        }

        private void LoadFile(string FileName)
        {

        }

        #region TestOrders
        string lastClOrd = "";

        public void SendTestOrder()
        {
            SubscribeTicker("PETR4", 1);
            lastClOrd = curFixConn.sendOrder(OrderPrefix, 1, 100, 0.01, 18, 11, 223, "", NCommonTypes.NEnuns.NOrderType.Buy, true);
        }

        public void SendTestCancel()
        {
            curFixConn.cancelOrder(lastClOrd);
        }
        #endregion
    }

    public class JerichoSec
    {
        private string _Security;
        private int _IdSecurity;
        private int _BuyQty;
        private double _BuyAvgPx;
        private int _SellQty;
        private double _SellAvgPx;               
        private double _Last;

        public string Security
        {
            get { return _Security; }
            set { _Security = value; }
        }
        public int IdSecurity
        {
            get { return _IdSecurity; }
            set { _IdSecurity = value; }
        }
        public int BuyQty
        {
            get { return _BuyQty; }
            set { _BuyQty = value; }
        }
        public double BuyAvgPx
        {
            get { return _BuyAvgPx; }
            set { _BuyAvgPx = value; }
        }
        public int SellQty
        {
            get { return _SellQty; }
            set { _SellQty = value; }
        }
        public double SellAvgPx
        {
            get { return _SellAvgPx; }
            set { _SellAvgPx = value; }
        }        
        public double Last
        {
            get { return _Last; }
            set { _Last = value; }
        }

        public int NetQty
        {
            get { return _BuyQty - _SellQty; }
         }
        public double NetValue
        {
            get { return NetQty * _Last; }         
        }        
        public double Realized
        {
            get
            {
                int reaQty = Math.Min(_BuyQty, _SellQty);
                return (_SellAvgPx - _BuyAvgPx) * (double)reaQty;
            }
        }
        public double UnrealPnL
        {
            get
            {
                double px = (NetQty > 0 ? _BuyAvgPx : _SellAvgPx);
                return NetQty * (_Last - px);
            }
        }
        public double TotalPnl
        {
            get
            {
                return Realized + UnrealPnL;
            }
        }
        public bool PositionOpen
        {
            get
            {
                bool result = false;

                if (Math.Abs(_BuyQty) + Math.Abs(_SellQty) > 0)
                {
                    result = true;
                }

                return result;
            }
        }
    }
}
