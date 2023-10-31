using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using System.Windows.Forms;
using NestSymConn;
using NCommonTypes;

namespace LiveTrade2
{
    class StgItem : MarketDataItem
    {
        public event EventHandler OnNewOrder;
        public event EventHandler OnReplaceOrder;
        public event EventHandler OnCancelAll;

        private bool _MasterFlag = false;

        public bool MasterFlag
        {
            get { return _MasterFlag; }
            set { _MasterFlag = value; }
        }

        public SortedDictionary<string, StratOrder> curOrders = new SortedDictionary<string, StratOrder>();
        public SortedDictionary<string, StratOrder> tempOrders = new SortedDictionary<string, StratOrder>();

        private double _InitialVolume = 0;
        private double _InitialPrice = 0;

        private double _DoneShares = 0;
        private double _TradedValue = 0;
        private double _PercOfDay = 0;
        private string _TradingPhase = "NOT STARTED";
        private double _InitialVWAP = 0;
        private TradeModes _TradeMode = TradeModes.WAITING;
        private double _WorkShares = 0;
        private double _PCTTime = 0;
        private double _Rebate = 0.98;

        private TimeSpan _LastExecReceived = new TimeSpan(0, 0, 0);

        private bool _Enabled = false;
        public string strEnabled { get { if (_Enabled) return "ENABLED"; else return "DISABLED"; } }

        public double Rebate { get { return _Rebate; } set { _Rebate = value; } }
        public double InitialVolume { get { return _InitialVolume; } set { _InitialVolume = value; } }
        public double InitialPrice { get { return _InitialPrice; } set { _InitialPrice = value; } }
        public double InitialChange { get { if (_InitialPrice != 0) return (1 + Change) / (_Last / _InitialPrice) - 1; else return 0; } }
        public double ElapsedVolume { get { if (_InitialVolume != 0) { return _Volume - _InitialVolume; } else { return 0; } } }
        public double DoneShares { get { return _DoneShares; } }
        public double AbsDoneShares { get { return Math.Abs(_DoneShares); } }
        public string TradingPhase { get { return _TradingPhase; } set { _TradingPhase = value; } }
        public double TradeCost { get { double CostFactor = 2; if (_CloseOrderSent && _DoneShares == 0) CostFactor = 1; return -(Math.Abs(_BuyValue) + Math.Abs(_SellValue)) * (0.5 / 100 * (1 - _Rebate) + 0.025 / 100) * CostFactor; } }
        public double InitialVWAP { get { return _InitialVWAP; } set { _InitialVWAP = value; } }
        public double ElapsedVWAP { get { if (this.Volume - this.InitialVolume != 0 && this.InitialVolume != 0) { return (this.VWAP * this.Volume - this.InitialVWAP * this.InitialVolume) / (this.Volume - this.InitialVolume); } else { return 0; } } }
        public string TradeMode { get { return _TradeMode.ToString(); } }
        public double WorkShares { get { return _WorkShares; } set { _WorkShares = value; } }
        public double PositionValue { get { if (TradeSide == 1) { return _DoneShares * this.Last; } else { if (TradeSide == -1) { return _DoneShares * this.Last; } else { return 0; } } } }
        public double PCTDone { get { if (_TargetShares != 0) { return AbsDoneShares / _ShareLimit; } else { return 0; } } }
        public double PCTTime { get { return _PCTTime; } set { _PCTTime = value; } }
        public double LimitFinAmt { get { return ShareLimit * Last; } }
        public double NewPCTVolume { get { if (ElapsedVolume != 0) return Math.Min(ShareLimit * _PCTTime / ElapsedVolume, 0.10); else return 0; } }
        
        //===========================================================================================================
        // =========================================   LIMITS   =====================================================
        //===========================================================================================================

        private double _ShareLimit = 0;
        private double _MaxFinancial = 600 * 1000;

        public double ShareLimit { get { return _ShareLimit; } set { _ShareLimit = value; } }

        //===========================================================================================================
        // ========================================= STRATEGY SETTINGS ==============================================
        //===========================================================================================================

        private int _TradeSide = 0;
        private double _TargetShares = 0;
        private double _VolumeParticipation = 0;

        public int TradeSide { get { return _TradeSide; } set { _TradeSide = value; } }
        public double TargetShares { get { return _TargetShares; } }
        public double VolumeParticipation { get { return _VolumeParticipation; } set { _VolumeParticipation = value; } }

        //===========================================================================================================
        //========================================== MARKET DATA VARIABLES ==========================================
        //===========================================================================================================
       
        private int _AskCounter = 0;
        private int _BidCounter = 0;

        public int AskCounter { get { return _AskCounter; } set { _AskCounter = value; } }
        public int BidCounter { get { return _BidCounter; } set { _BidCounter = value; } }
        public double BidTrades { get { if ((_BidCounter + _AskCounter) > 0) { return _BidCounter / ((double)_BidCounter + _AskCounter); } else return 0; } }
        public double AucChange { get { if (Last != 0) { return _AucLast / Last - 1; } else { return 0; } } }

        public double PCTAuction
        {
            get
            {
                if (TradingStatus == TradingStatusType.G_PREOPEN_P || TradingStatus == TradingStatusType.AUCTION_K)
                {
                    if (Last != 0)
                    {
                        if (AucLast != 0) return AucLast / Last - 1;
                    }
                    else if (Close != 0)
                    {
                        if (AucLast != 0) return AucLast / Close - 1;
                    }
                    else
                    {
                        return 0;
                    }
                    return 0;
                }
                else
                    return 0;
            }
        }

        public double validLast { get { if (TradingStatus == TradingStatusType.G_PREOPEN_P || TradingStatus == TradingStatusType.AUCTION_K) { return this.AucLast; } else return this.Last; } }
        public double validChange { get { if (TradingStatus == TradingStatusType.G_PREOPEN_P || TradingStatus == TradingStatusType.AUCTION_K) { return this.PCTAuction; } else return this.Change; } }
        public double validOpen { get { if (TradingStatus == TradingStatusType.G_PREOPEN_P || TradingStatus == TradingStatusType.AUCTION_K) { return this.AucLast; } else return this.Open; } }
        public double validAucVolume { get { if (TradingStatus == TradingStatusType.G_PREOPEN_P || TradingStatus == TradingStatusType.AUCTION_K) { return this.AucVolume; } else return this.Volume; } }
        public double Spread { get { return this.Ask - this.Bid; } }

        public List<Level2Line> MktDepth = new List<Level2Line>();

        public List<Level2Line> MktDepthOrd
        {
            get
            {
                Level2Line curOrd = new Level2Line();
                List<Level2Line> newMktDepth = new List<Level2Line>();

                if (TopBuyId != "" && TopBuyId != "TEMP")
                {
                    curOrd.PriceBid = curOrders[TopBuyId].Price;
                    curOrd.QuantityBid = curOrders[TopBuyId].OrderQty;

                    bool OrdInserted = false;

                    foreach (Level2Line curItem in MktDepth)
                    {
                        if (curItem.PriceBid < curOrd.PriceBid && !OrdInserted)
                        {
                            newMktDepth.Add(curOrd);
                            OrdInserted = true;
                        }
                        newMktDepth.Add(curItem);
                    }
                }
                else if (TopSellId != "" && TopSellId != "TEMP")
                {
                    curOrd.PriceAsk = curOrders[TopSellId].Price;
                    curOrd.QuantityAsk = curOrders[TopSellId].OrderQty;

                    bool OrdInserted = false;


                    foreach (Level2Line curItem in MktDepth)
                    {
                        if (curItem.PriceAsk >= curOrd.PriceAsk && !OrdInserted)
                        {
                            newMktDepth.Add(curOrd);
                            OrdInserted = true;
                        }
                        newMktDepth.Add(curItem);
                    }
                }
                else
                {
                    foreach (Level2Line curItem in MktDepth)
                    {
                        newMktDepth.Add(curItem);
                    }
                }

                
                return newMktDepth;
            }
        }

        private TimeSpan _curUpdTime;

        public TimeSpan curUpdTime
        {
            get { return _curUpdTime; }
            set { _curUpdTime = value; }
        }

        //===========================================================================================================
        // ========================================= STRATEGY ORDER VARIABLES =======================================
        //===========================================================================================================


        // --- Top Buy
        public string TopBuyId = "";
        private bool _TopBuyEnabled = false;
        private TimeSpan _TopBuy_LastChangeTime = new TimeSpan(0, 0, 0);

        public bool TopBuyEnabled { get { return _TopBuyEnabled; } set { _TopBuyEnabled = value; Check_BuyTop(); } }
        public string strTopBuyId { get { return TopBuyId; } }
        public double dblMissingQuantity { get { return (int)(_TargetShares - AbsDoneShares); } }

        // --- Top Sell
        public string TopSellId = "";
        private bool _TopSellEnabled = false;
        private TimeSpan _TopSell_LastChangeTime = new TimeSpan(0, 0, 0);

        public bool TopSellEnabled { get { return _TopSellEnabled; } set { _TopSellEnabled = value; Check_SellTop(); } }
        public string strTopSellId { get { return TopSellId; } }

        // --- Buy Pickup
        private bool _PickupBuyEnabled = false;
        private TimeSpan _PickupBuy_LastChangeTime = new TimeSpan(0, 0, 0);

        public bool PickupBuyEnabled { get { return _PickupBuyEnabled; } set { _PickupBuyEnabled = value; Check_BuyPickup(); } }

        // --- Sell Pickup
        private bool _PickupSellEnabled = false;
        private TimeSpan _PickupSell_LastChangeTime = new TimeSpan(0, 0, 0);

        public bool PickupSellEnabled { get { return _PickupSellEnabled; } set { _PickupSellEnabled = value; Check_SellPickup(); } }
        
        // --- Close Order
        public string CloseOrderId = "";
        public string strCloseOrderId { get { return CloseOrderId; } }
        private bool _CloseOrderSent = false;

        public bool CloseOrderSent { get { return _CloseOrderSent; } }

        public int _TopCounter = 0;
        public int _PickupCounter = 0;
        public int TopCounter { get { return _TopCounter; } }
        public int PickupCounter { get { return _PickupCounter; } }

        //============================================================================================================

        public string curSideString 
        {
            get 
            {
                if (this.TradeSide == 1) return "BUY";
                if (this.TradeSide == -1) return "SELL";
                else return "UNDEFINED";
            }
        }

        public double ToVWAP { get { if (ElapsedVWAP == 0) { return 0; } else if (TradeSide == 1) { if (AVGPBuy == 0) { return 0; } else { return AVGPBuy / ElapsedVWAP - 1; } } else { if (TradeSide == -1) { if (AVGPSell == 0) { return 0; } else { return -AVGPSell / ElapsedVWAP - 1; } } else { return 0; } } } }
        
        public double TradedValue 
        { 
            get { return _TradedValue; } 
            set { _TradedValue = value; } 
        }
        
        public double TradedPnL { get { return this.PositionValue + _TradedValue + TradeCost; } }

        private int _MinLot = 1; public int MinLot { get { return _MinLot; } set { _MinLot = value; } }

        private double _BuyValue = 0; private double _BuyShares = 0; 
        public double AVGPBuy { get { if (_BuyShares != 0)  return -_BuyValue / _BuyShares; else return 0; } }
        private double _SellValue = 0; private double _SellShares = 0; 

        public double AVGPSell { get { if (_SellShares != 0)  return _SellValue / _SellShares; else return 0; } }
        public double Slippage { get { if (_TradeSide == 1) return ((_BuyShares * (Last - AVGPBuy)) - _TargetShares * (Last - ElapsedVWAP)); else return ((_SellShares * (Last + AVGPSell)) - _TargetShares * -(Last - ElapsedVWAP)); } }
        public double AucGain { get { return _DoneShares * (AucLast - _Last); } }


//====================================================================================================================================================================================================
//====================================================================================================================================================================================================
//====================================================================================================================================================================================================

        public void Enable()
        {
            _TradeMode = TradeModes.NORMAL;

            _MasterFlag = true;

            if (TradeSide == 1)
            {
                _TopBuyEnabled = true;
                _PickupBuyEnabled = true;
                this.Check_BuyTop();
            }

            if (TradeSide == -1)
            {
                _TopSellEnabled = true;
                _PickupSellEnabled = true;
                this.Check_SellTop();
            }

            _Enabled = true;
        }

        public void Disable()
        {
            this._TradeMode = TradeModes.STOPPED;
            this.Stop();
            _Enabled = false;
        }

        public void Stop()
        {
            SendCancelAll();
            _TopBuyEnabled = false;
            _PickupBuyEnabled = false;

            _MasterFlag = false;
        }

        public void TimeCheckOrders()
        {
            if (TradeSide == 1)
            {
                Check_BuyTop();
                Check_BuyPickup();
            }
            if (TradeSide == -1)
            {
                Check_SellTop();
                Check_SellPickup();
            }
        }

        private void Check_BuyPickup()
        {
            if (_ShareLimit - AbsDoneShares < 100 && _TradeMode != TradeModes.HITLIMIT)
            {
                _TradeMode = TradeModes.HITLIMIT;
                this.Stop();
                return;
            }

            if (_PickupBuyEnabled && _TradeMode == TradeModes.NORMAL && TradingPhase == "OPENING POS")
            {
                int MissingQuantity = (int)(_TargetShares - _DoneShares);

                if (MissingQuantity > 100)
                {
                    if (DateTime.Now.TimeOfDay.Subtract(_PickupBuy_LastChangeTime) > new TimeSpan(0, 0, 0, 0, 700))
                    {
                        //if (_TradeSide == 1 && (_DoneShares * AVGPBuy + 100 * _Ask) / (_DoneShares + 100) < ElapsedVWAP && CheckExecSpeed())
                        if (_TradeSide == 1 && ((_DoneShares * AVGPBuy + 100 * _Ask) / (_DoneShares + 100) < ElapsedVWAP || _Ask < ElapsedVWAP) && CheckExecSpeed())
                        {
                            StratOrder curPickupOrder = new StratOrder(this.IdTicker, this.Ticker, curSideString, 100, _Ask + 0.05, "PICKUP");

                            SendBuy(curPickupOrder);
                            _PickupBuy_LastChangeTime = DateTime.Now.TimeOfDay;

                            return;
                        }
                        else if (_TradeSide == 1 && MissingQuantity > 300 && Spread <= 0.011 && CheckExecSpeed()) // && _Last > 5)
                        {
                            StratOrder curPickupOrder = new StratOrder(this.IdTicker, this.Ticker, curSideString, 100, _Ask + 0.05, "PICKUP");

                            SendBuy(curPickupOrder);
                            _PickupBuy_LastChangeTime = DateTime.Now.TimeOfDay;
                            
                            return;
                        }
                    }
                }
            }
        }

        private void Check_SellPickup()
        {
            if (_ShareLimit - AbsDoneShares < 100 && _TradeMode != TradeModes.HITLIMIT)
            {
                _TradeMode = TradeModes.HITLIMIT;
                this.Stop();
                return;
            }

            if (_PickupSellEnabled && _TradeMode == TradeModes.NORMAL && TradingPhase == "OPENING POS")
            {
                int MissingQuantity = (int)(_TargetShares - AbsDoneShares);

                if (MissingQuantity > 100)
                {
                    if (DateTime.Now.TimeOfDay.Subtract(_PickupSell_LastChangeTime) > new TimeSpan(0, 0, 0, 0, 700))
                    {
                        //if (_TradeSide == -1 && ((_DoneShares * AVGPSell - 100 * _Bid) / -(_DoneShares - 100) > ElapsedVWAP) && CheckExecSpeed())
                        if (_TradeSide == -1 && ((_DoneShares * AVGPSell - 100 * _Bid) / -(_DoneShares - 100) > ElapsedVWAP || _Bid > ElapsedVWAP) && CheckExecSpeed())
                        {
                            StratOrder curPickupOrder = new StratOrder(this.IdTicker, this.Ticker, curSideString, 100, _Bid - 0.05, "PICKUP");

                            SendSell(curPickupOrder);
                            _PickupSell_LastChangeTime = DateTime.Now.TimeOfDay;

                            return;
                        }
                        else if (_TradeSide == -1 && MissingQuantity > 300 && Spread <= 0.011 && CheckExecSpeed())// && _Last > 5)
                        {
                            StratOrder curPickupOrder = new StratOrder(this.IdTicker, this.Ticker, curSideString, 100, _Bid - 0.05, "PICKUP");

                            SendSell(curPickupOrder);
                            _PickupSell_LastChangeTime = DateTime.Now.TimeOfDay;

                            return;
                        }
                    }
                }
            }
        }

        private void Check_BuyTop()
        {
            double OrderPrice = 0;
            int OrderShares = 100;

            int MissingQuantity = (int)(_TargetShares - AbsDoneShares);

            if ((int)(_ShareLimit - AbsDoneShares) > 1000) OrderShares = 200;
            if ((int)(_ShareLimit - AbsDoneShares) > 10000) OrderShares = 400;
            if ((int)(_ShareLimit - AbsDoneShares) > 15000) OrderShares = 1000;

            if (_TopBuyEnabled && _TradeMode == TradeModes.NORMAL && TradingPhase == "OPENING POS")
            {
                if (_TradeSide <= 1)
                {
                    if ((_BidSize > 500 || Spread <= 0.011) && !(TopBuyId != "" && _BidSize == 100))
                    {
                        OrderPrice = _Bid;
                    }
                    else
                    {
                        double Bid2 = 0;
                        double Bid2Shares = 0;

                        for (int i = 0; i < MktDepth.Count; i++)
                        {
                            if (MktDepth[i].LevelBid == 2)
                            {
                                Bid2 = MktDepth[i].PriceBid;
                                Bid2Shares += MktDepth[i].QuantityBid;
                            }
                        }

                        if (Bid2 == 0)
                        {
                            Bid2 = _Bid - 0.01;
                            Bid2Shares = 100;
                        }

                        OrderPrice = Bid2;
                        if (Bid2Shares > 500) OrderPrice = Bid2 + 0.01;

                        if (OrderPrice > _Bid) OrderPrice = _Bid;
                    }

                    if (DateTime.Now.TimeOfDay.Subtract(_TopBuy_LastChangeTime) > new TimeSpan(0, 0, 0, 0, 700))
                    {
                        if (TopBuyId == "")
                        {
                            if (CheckExecSpeed() && (MissingQuantity > -100 || (MissingQuantity > -200 && _ShareLimit > 2000)))
                            {
                                TopBuyId = "TEMP";
                                StratOrder curTopOrder = new StratOrder(this.IdTicker, this.Ticker, curSideString, OrderShares, OrderPrice, "TOPBID");
                                SendBuy(curTopOrder);
                            }
                        }
                        else
                        {
                            if (TopBuyId != "TEMP")
                            {
                                if (Math.Round(curOrders[TopBuyId].Price, 5) != Math.Round(OrderPrice, 5) && curOrders[TopBuyId].Price != -1)
                                {
                                    SendReplacePrice(curOrders[TopBuyId], OrderPrice);
                                    _TopBuy_LastChangeTime = DateTime.Now.TimeOfDay;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Check_SellTop()
        {
            double OrderPrice = 0;
            int OrderShares = 100;

            int MissingQuantity = (int)(_TargetShares - AbsDoneShares);

            if ((int)(_ShareLimit - AbsDoneShares) > 1000) OrderShares = 200;
            if ((int)(_ShareLimit - AbsDoneShares) > 10000) OrderShares = 400;
            if ((int)(_ShareLimit - AbsDoneShares) > 15000) OrderShares = 1000;

            if (_TopSellEnabled && _TradeMode == TradeModes.NORMAL && TradingPhase == "OPENING POS")
            {
                if (_TradeSide == -1)
                {
                    if ((_AskSize > 500 || Spread <= 0.011) && !(TopSellId != "" && _AskSize == 100))
                    {
                        OrderPrice = _Ask;
                    }
                    else
                    {
                        double Ask2 = 0;
                        double Ask2Shares = 0;

                        for (int i = 0; i < MktDepth.Count; i++)
                        {
                            if (MktDepth[i].LevelAsk == 2)
                            {
                                Ask2 = MktDepth[i].PriceAsk;
                                Ask2Shares += MktDepth[i].QuantityAsk;
                            }
                        }

                        if (Ask2 == 0)
                        {
                            Ask2 = _Ask - 0.01;
                            Ask2Shares = 100;
                        }

                        OrderPrice = Ask2;
                        if (Ask2Shares > 500) OrderPrice = Ask2 - 0.01;

                        if (OrderPrice < _Ask) OrderPrice = _Ask;
                    }

                    if (DateTime.Now.TimeOfDay.Subtract(_TopSell_LastChangeTime) > new TimeSpan(0, 0, 0, 0, 700))
                    {
                        if (TopSellId == "")
                        {
                            if (CheckExecSpeed() && (MissingQuantity > -100 || (MissingQuantity > -200 && _ShareLimit > 2000)))
                            {
                                TopSellId = "TEMP";
                                StratOrder curTopOrder = new StratOrder(this.IdTicker, this.Ticker, curSideString, OrderShares, OrderPrice, "TOPASK");
                                SendSell(curTopOrder);
                            }
                        }
                        else
                        {
                            if (TopSellId != "TEMP")
                            {
                                if (Math.Round(curOrders[TopSellId].Price, 5) != Math.Round(OrderPrice, 5) && curOrders[TopSellId].Price != -1)
                                {
                                    SendReplacePrice(curOrders[TopSellId], OrderPrice);
                                    _TopSell_LastChangeTime = DateTime.Now.TimeOfDay;
                                }
                            }
                        }
                    }
                }
            }
        }

        private bool CheckExecSpeed()
        {
            if (PCTDone - PCTTime > 0.20 && TradedPnL < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void ClosePosition()
        {
            string OrderSide = "";
            int OrderQuantity = 0;

            if (this.DoneShares >= 0)
            {
                OrderSide = "SELL";
                OrderQuantity = -(int)this.DoneShares;
            }
            else
            {
                OrderSide = "BUY";
                OrderQuantity = -(int)this.DoneShares;
            }

            if (OrderQuantity != 0 && !_CloseOrderSent)
            {
                StratOrder curOrder = new StratOrder(this.IdTicker, this.Ticker, OrderSide, OrderQuantity, -2, "CLOSEPOS");

                DialogResult UserAnswer;

                if (_Last > 40 || curOrder.OrderQty > 10000)
                {
                    UserAnswer = MessageBox.Show("SEND CLOSE ORDER?\r\n\r\n" + OrderSide + " " + curOrder.OrderQty + " " + ExchangeTradingCode, "Close Order", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                }
                else
                {
                    UserAnswer = DialogResult.Yes;
                }
                
                if (UserAnswer == DialogResult.Yes)
                {
                    tempOrders.Add(curOrder.OrderType, curOrder);
                    OnNewOrder(this, new NewOrderDef(curOrder.IdTicker, ExchangeTradingCode, curOrder.OrderQty, curOrder.Price, curOrder.OrderType));
                    _CloseOrderSent = true;
                }
            }
        }

        private void SendBuy(StratOrder curOrder)
        {
            CheckHitLimit();

            if (_TradedValue + curOrder.OrderQty * curOrder.Price > _MaxFinancial)
            {
                curOrder.OrderQty = (int)Math.Round(((_MaxFinancial - _TradedValue) / curOrder.Price) / 100, 0) * 100;
            }

            if (_DoneShares + curOrder.OrderQty > _ShareLimit)
            {
                curOrder.OrderQty = (int)Math.Round((_ShareLimit - _DoneShares) / 100, 0) * 100;
            }

            if (curOrder.OrderQty <= 0)
            {
                _TradeMode = TradeModes.HITLIMIT;
                return;
            }

            if (_MasterFlag)
            {
                if (!tempOrders.ContainsKey(curOrder.OrderType))
                {
                    tempOrders.Add(curOrder.OrderType, curOrder);

                    if (OnNewOrder != null)
                    {
                        OnNewOrder(this, new NewOrderDef(curOrder.IdTicker, ExchangeTradingCode, curOrder.OrderQty, curOrder.Price, curOrder.OrderType));
                    }
                }
            }
        }
        
        private void SendSell(StratOrder curOrder)
        {
            CheckHitLimit();

            if (curOrder.OrderQty < 0)
                return;

            if (Math.Abs(_TradedValue + curOrder.OrderQty * curOrder.Price) > _MaxFinancial)
            {
                curOrder.OrderQty = -(int)Math.Round(((_MaxFinancial - Math.Abs(_TradedValue) / curOrder.Price)) / 100, 0) * 100;
            }

            if (Math.Abs(AbsDoneShares + curOrder.OrderQty) > _ShareLimit)
            {
                curOrder.OrderQty = -(int)Math.Round((_ShareLimit - AbsDoneShares) / 100, 0) * 100;
            }

            if (curOrder.OrderQty <= 0)
            {
                return;
            }

            curOrder.OrderQty = -curOrder.OrderQty;

            if (_MasterFlag)
            {
                if (!tempOrders.ContainsKey(curOrder.OrderType))
                {
                    tempOrders.Add(curOrder.OrderType, curOrder);

                    if (OnNewOrder != null)
                    {
                        OnNewOrder(this, new NewOrderDef(curOrder.IdTicker, ExchangeTradingCode, curOrder.OrderQty, curOrder.Price, curOrder.OrderType));
                    }
                }
            }
        }

        private bool CheckHitLimit()
        {
            if (Math.Abs(_ShareLimit) - AbsDoneShares < 100 && _MasterFlag != false)
            {
                _TradeMode = TradeModes.HITLIMIT;
                this.Stop();
                return false;
            }

            return true;
        }
        private void SendReplacePrice(StratOrder curOrder, double newPrice)
        {
            if (!curOrder.ReplaceRequested)
            {
                curOrder.ReplaceRequested = true;
                OnReplaceOrder(this, new ReplaceOrderDef(curOrder.OrderID, newPrice));
            }
        }

        private void SendCancelAll()
        {
            if (OnCancelAll != null)
            {
                OnCancelAll(this, new NewOrderDef(this._IdTicker, this.ExchangeTradingCode, 0, 0, ""));
            }
        }

        public void RegisterBuy(double tradeShares, double tradePrice)
        {
            _DoneShares = _DoneShares + tradeShares;
            TradedValue = TradedValue + -tradeShares * tradePrice;

            _BuyShares = _BuyShares + tradeShares;
            _BuyValue = _BuyValue + -tradeShares * tradePrice;
            if (_Bid > tradePrice - 0.05) _Bid = tradePrice - 0.05;

            _LastExecReceived = DateTime.Now.TimeOfDay;
        }

        public void RegisterSell(double tradeShares, double tradePrice)
        {
            _DoneShares = _DoneShares + -tradeShares;
            _TradedValue = _TradedValue + tradeShares * tradePrice;

            _SellShares = _SellShares + -tradeShares;
            _SellValue = _SellValue + tradeShares * tradePrice;

            _LastExecReceived = DateTime.Now.TimeOfDay;
        }

        public new string ToString()
        {
            string curItem = this.Ticker;

            curItem = curItem + "\t" + LastUpdTime.ToString("dd/MM/yyyy hh:mm:ss.fff");
            curItem = curItem + "\t" + _DoneShares.ToString();
            curItem = curItem + "\t" + _TradeSide.ToString();
            curItem = curItem + "\t" + _TradedValue.ToString();
            curItem = curItem + "\t" + _PercOfDay.ToString();
            curItem = curItem + "\t" + _TradingPhase;
            curItem = curItem + "\t" + validLast.ToString();
            curItem = curItem + "\t" + Close.ToString();
            curItem = curItem + "\t" + validOpen.ToString();
            curItem = curItem + "\t" + TradingStatus.ToString();
            curItem = curItem + "\t" + AucLast.ToString();
            curItem = curItem + "\t" + Last.ToString();
            curItem = curItem + "\t" + Open.ToString();
            
            return curItem;
        }

        public void UpdateTradingPhase(string newTradingPhase)
        {

            _TradingPhase = newTradingPhase;

        }

        //VWAP section
        long VWAPQuantity = 0;
        public double VWAPPrice = 0;
        double VWAPFinancial = 0;
        bool LastReceived = false;
        bool LSReceived = false;
        double vwapLast = 0;
        double vwapLastSize = 0;

        public void UpdateVWAP(MarketUpdateItem curItem)
        {
            if (curItem.FLID == NestFLIDS.Last)
            {
                vwapLast = curItem.ValueDouble;
                LastReceived = true;
            }
            else if (curItem.FLID == NestFLIDS.LastSize)
            {
                vwapLastSize = curItem.ValueDouble;
                LSReceived = true;
            }

            if (LastReceived && LSReceived)
            {
                VWAPFinancial = VWAPFinancial + vwapLastSize * vwapLast;
                VWAPQuantity = VWAPQuantity + (long)vwapLastSize;
                VWAPPrice = VWAPFinancial / (double)VWAPQuantity;
                LastReceived = false;
                LSReceived = false;
            }
        }
   
        public new void Update(MarketUpdateItem curItem)
        {
            base.Update(curItem);
            if (curItem.FLID == NestFLIDS.Volume)
            {
                double PrevTargetShares = _TargetShares;
                _TargetShares = Math.Round((ElapsedVolume * VolumeParticipation) / 100, 0) * 100;
                if (_TargetShares > _ShareLimit) _TargetShares = _ShareLimit;
                double RoundedLimitShares = Math.Round((ElapsedVolume * VolumeParticipation) / 100, 0) * 100;
                if (_TargetShares > RoundedLimitShares) _TargetShares = RoundedLimitShares;
                if (_TargetShares != PrevTargetShares) Check_BuyPickup();
                if (this._Last == this._Ask) AskCounter++;
                if (this._Last == this._Bid) BidCounter++;
            }
            if (curItem.FLID == NestFLIDS.Bid)
            {
                Check_BuyTop();
            }
            if (curItem.FLID == NestFLIDS.Ask)
            {
                Check_SellTop();
            }
        }

        public enum TradeModes
        {
            HITLIMIT = -3,
            FINISHED = -2,
            WAITING = -1,
            NORMAL = 0,
            SLOW = 1,
            STOPPED = -4
        }

        public class StratOrder : NCommonTypes.FIXOrder
        {
            public bool ReplaceRequested = false;
            public string OrderType = "";

            public StratOrder(int __IdTicker, string __Symbol, string __strSide, int __OrderQty, double __Price, string __OrderType)
                : base(__IdTicker, __Symbol, __strSide, __OrderQty, __Price)
            {
                OrderType = __OrderType;
            }
        }
    }
}
