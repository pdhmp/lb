using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestSymConn;
using NCommonTypes;
using NestDLL;
using System.IO;

namespace LiveTrade2
{
    public partial class frmLevel2 : ConnectedForm
    {
        List<Level2Bid> OrdersBid = new List<Level2Bid>();
        List<Level2Ask> OrdersAsk = new List<Level2Ask>();

        List<Level2Bid> DisplayDepthBid = new List<Level2Bid>();
        List<Level2Ask> DisplayDepthAsk = new List<Level2Ask>();
        List<ConnectedForm> ConnectedForms = new List<ConnectedForm>();

        Level2Grid curLevel2Grid = new Level2Grid();

        BindingSource bndDataSourceBid = new BindingSource();
        BindingSource bndDataSourceAsk = new BindingSource();

        Sources curSource;

        public string[,] ArrDefaultInstruments;
        public string[,] ArrDefaultSecurity;
        public string[,] ArrDefaultBookSection;

        string DefaultPriceStyle = "#,##0.00;-#,##0.00;\\ ";
        string DefaultChangeStyle = "0.00%";

        bool PendingUpdateDepth = true;
        bool PendingUpdateMktData = true;

        string _curTicker = "";
        int _curIdTicker = 0;
        int _curExchange = 0;
        string _curIdOrder = "";
        int _curIdInstrument = 0;

        double dTISBuy = 0;
        double dTISSell = 0;

        BrokerList curBrokers = new BrokerList();
        LTMarketDataItem curMarketItem;

        bool DepthStateAGG = true;
        bool BidMouseDown = false;
        bool AskMouseDown = false;

        bool ReplaceMode = false;

        //double dLast = 0;
        //double dVolume = 0;

        bool DisplayModeAuction = false;

        public frmLevel2(string curTicker)
        {
            InitializeComponent();
            this.Text = curTicker;
        }

        ~frmLevel2()
        {
            curNDistConn.Disconnect();
        }

        private void frmLevel2_Load(object sender, EventArgs e)
        {
            //#if DEBUG
            //    //cmdDefault.Visible = true;    
            //#endif

            txtPrice.MouseWheel += new MouseEventHandler(txtPrice_MouseWheel);
            txtQuantity.MouseWheel += new MouseEventHandler(txtQuantity_MouseWheel);

            if (FIXConnections.Instance.curFixConn != null)
            {
                FIXConnections.Instance.curFixConn.OnOrderUpdate += new EventHandler(FIXUpdateReceived);
            }
            for (int i = 0; i < 1; i++)
            {
                Level2Bid curLineBid = new Level2Bid();
                Level2Ask curLineAsk = new Level2Ask();
                curLineBid.Price = 1;
                curLineAsk.Price = 1;
                curLineBid.Broker = "ZZZ";
                curLineAsk.Broker = "ZZZ";
                curLineBid.Level = 1;
                curLineAsk.Level = 1;

                curLevel2Grid.DepthBid.AddFirst(curLineBid);
                curLevel2Grid.DepthAsk.AddFirst(curLineAsk);

                DisplayDepthBid.Add(new Level2Bid(curLineBid, DefaultPriceStyle, i));
                DisplayDepthAsk.Add(new Level2Ask(curLineAsk, DefaultPriceStyle, i));
            }

            curNDistConn.OnDepth += new EventHandler(NewDepthData);
            curNDistConn.OnDepthAgg += new EventHandler(NewDepthDataAgg);
            curNDistConn.OnData += new EventHandler(NewMarketData);


            bndDataSourceBid.DataSource = DisplayDepthBid;
            bndDataSourceAsk.DataSource = DisplayDepthAsk;

            dtgBid.DataSource = bndDataSourceBid;
            dtgAsk.DataSource = bndDataSourceAsk;

            dtgBid.Columns[0].Visible = false; //Level
            dtgBid.Columns[1].Visible = false; //Position
            dtgBid.Columns[2].Width = 50; //Broker
            dtgBid.Columns[3].Width = 50; //Quantity
            dtgBid.Columns[4].Visible = false; //Price
            dtgBid.Columns[5].Width = 50; //DisplayPrice

            dtgAsk.Columns[0].Width = 50; //DisplayPrice
            dtgAsk.Columns[1].Visible = false; //Price
            dtgAsk.Columns[2].Width = 50; //Quantity
            dtgAsk.Columns[3].Width = 50; //Broker
            dtgAsk.Columns[4].Visible = false; //Position
            dtgAsk.Columns[5].Visible = false; //Level

            dtgBid.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgBid.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dtgBid.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dtgAsk.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dtgAsk.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dtgBid.Columns[3].DefaultCellStyle.Format = "#,##0;-#,##0;\\ ";
            dtgAsk.Columns[2].DefaultCellStyle.Format = "#,##0;-#,##0;\\ ";

            cmbPortfolio.DataSource = GlobalVars.Instance.Portfolios;
            cmbBroker.DataSource = GlobalVars.Instance.Destinations;

            tmrUpdate.Start();
            //radNCustom.Checked = true;
            // radAGG.Checked = true;

            if (GlobalVars.Instance.CheckOrAGG == 0)
            {
                radNCustom.Checked = true;
                radAGG.Checked = false;
            }
            else
            {
                radNCustom.Checked = false;
                radAGG.Checked = true;
            }

            panDisplayAuction.Visible = false;
            panDisplayNormal.Visible = true;

            this.AddTooltips();

            lblTISBuy.Text = "";
            lblTISSell.Text = "";
        }

        private void FIXUpdateReceived(object sender, EventArgs e)
        {
            LoadOrders();
        }

        private void NewDepthDataAgg(object sender, EventArgs curLevel2Item)
        {
            if (DepthStateAGG)
            {
                Level2Item curItem;
                curItem = (Level2Item)curLevel2Item;
                int tempBroker = 0;
                if (int.TryParse(curItem.Broker, out tempBroker))
                {
                    curItem.Broker = curBrokers.BrokerTicker(tempBroker);
                }
                if (curItem.Ticker == _curTicker)
                    curLevel2Grid.Update(curItem);
            }
            PendingUpdateDepth = true;
        }

        private void NewDepthData(object sender, EventArgs curLevel2Item)
        {
            if (!DepthStateAGG)
            {
                Level2Item curItem;
                curItem = (Level2Item)curLevel2Item;
                int tempBroker = 0;
                

                if (int.TryParse(curItem.Broker, out tempBroker))
                {
                    curItem.Broker = curBrokers.BrokerTicker(tempBroker);
                }
                if (curItem.Ticker == _curTicker)
                {
                    curLevel2Grid.Update(curItem);
                }
                //if (curItem.Side == 'a') Console.WriteLine(curItem.Action + "\t" + curItem.Position + "\t" + curItem.Side + "\t" + curItem.Quantity + "\t" + curItem.Price + "\t" + curItem.Broker + "\t" + curBrokers.BrokerTicker(tempBroker));
            }
            PendingUpdateDepth = true;
        }

        private void NewMarketData(object sender, EventArgs origE)
        {
            MarketUpdateList curUpdateList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
            {
                if (curUpdateItem.Ticker == _curTicker)
                {
                    Console.WriteLine(curUpdateItem.Ticker + "\t" + curUpdateItem.FLID + "\t" + curUpdateItem.ValueDouble + "\t" + curUpdateItem.ValueString);
                    if (!(curUpdateItem.FLID == NestFLIDS.Last && curUpdateItem.ValueDouble == 0))
                    {
                        curMarketItem.Update(curUpdateItem);
                    }

                    if (curUpdateItem.FLID == NestFLIDS.AucCloseTime)
                    {
                        //Console.WriteLine(curUpdateItem.ValueDouble);
                    }

                    if (curUpdateItem.FLID == NestFLIDS.TradingStatus)
                    {
                        TradingStatusType oMode = (TradingStatusType)(int)curUpdateItem.ValueDouble;
                        DisplayModeAuction = (oMode == TradingStatusType.AUCTION_K || oMode == TradingStatusType.G_PREOPEN_P);
                    }
                }
            }

            PendingUpdateMktData = true;
        }

        class NCustomLine
        {
            public double Price = 0;
            public long BidShares = 0;
            public long AskShares = 0;
            public long cumBidShares = 0;
            public long cumAskShares = 0;
            public long Matched { get { return Math.Min(cumBidShares, cumAskShares); } }
            public long UnMatched { get { return Math.Max(cumBidShares, cumAskShares) - Math.Min(cumBidShares, cumAskShares); } }
            public long prevBidShares = 0;
            public long prevAskShares = 0;

            public long setBidShares = 0;
            public long setAskShares = 0;

            public override string ToString()
            {
                return Price + "_______" + BidShares + "_" + AskShares + "________" + Matched + "_" + UnMatched;
            }
        }


        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            bool FlagLimit10 = false;

            if (curMarketItem != null && PendingUpdateMktData)
            {
                PendingUpdateMktData = false;
                labLast.Text = curMarketItem.Last.ToString(DefaultPriceStyle);
                labLastAction.Text = curMarketItem.Last.ToString(DefaultPriceStyle);
                labChange.Text = curMarketItem.Change.ToString(DefaultChangeStyle);

                //labChangeAction.Text = (curMarketItem.Last / curMarketItem.AucLast).ToString();

                labVolume.Text = curMarketItem.Volume.ToString("#,##0");

                if (_curTicker.Length > 2)
                {
                    if (_curTicker.Substring(0, 3) == "DI1")
                    {
                        labChange.Text = (curMarketItem.Last - curMarketItem.Close).ToString(DefaultChangeStyle);
                    }
                }
                labHigh.Text = curMarketItem.High.ToString(DefaultPriceStyle);
                labLow.Text = curMarketItem.Low.ToString(DefaultPriceStyle);
                labAucLast.Text = curMarketItem.AucLast.ToString(DefaultPriceStyle);
                labAucEnd.Text = curMarketItem.AucCloseTime.ToString("HH:mm:ss");
                labAucVolume.Text = curMarketItem.AucVolume.ToString("#,##0");
                labTrdStatus.Text = curMarketItem.TradingStatus.ToString().Trim();

                labName.Text = curMarketItem.Name.Trim();
                labISIN.Text = curMarketItem.ISIN.Trim();
                labAvVolum.Text = curMarketItem.RecentAvgShares.ToString("#,##0");
                labTotalShares.Text = curMarketItem.TotalShares.ToString("#,##0");
                labSettlement.Text = curMarketItem.SettlementPrice.ToString(DefaultPriceStyle);
                labPrevSettlPrice.Text = curMarketItem.PrevSettlementPrice.ToString(DefaultPriceStyle);
                /*
                labFreezeLast.Text = curMarketItem.FreezeNonCrossLast.ToString("0.00");
                labFreezeAvg.Text = curMarketItem.FreezeNonCrossAvShares.ToString("0.00");
                LabFreezeShares.Text = curMarketItem.FreezeNonCrossTotalShares.ToString("0.00");
                */

                labClose.Text = curMarketItem.Close.ToString(DefaultPriceStyle);
                labStrike.Text = curMarketItem.Strike.ToString(DefaultPriceStyle);
                labExpiration.Text = curMarketItem.Expiration.ToString("dd-MMM-yy");
                labExpirationTime.Text = curMarketItem.ExpirationTime.ToString();
            }

            if (!BidMouseDown && !AskMouseDown && PendingUpdateDepth)
            {
                //long startms = FIXConnections.Instance.curFixConn.MasterClock.ElapsedMilliseconds;

                if (curMarketItem != null)
                {
                    try
                    {
                        int tempVisibleBid = dtgBid.FirstDisplayedScrollingRowIndex;
                        int tempVisibleAsk = dtgAsk.FirstDisplayedScrollingRowIndex;

                        PendingUpdateDepth = false;

                        DisplayDepthBid.Clear();
                        DisplayDepthAsk.Clear();

                        int curLevel = 0;
                        double prevPrice = 0;
                        bool finished = false;

                        LinkedListNode<Level2Bid> curBidNode;
                        LinkedListNode<Level2Ask> curAskNode;

                        int curCounter = 0;

                        long a = GlobalVars.Instance.MasterClock.ElapsedMilliseconds;

                        if (radNCustom.Checked)
                        {
                            FlagLimit10 = true;

                            SortedDictionary<double, NCustomLine> dicPrices = new SortedDictionary<double, NCustomLine>();
                            LinkedListNode<Level2Bid> tempBidNode = curLevel2Grid.DepthBid.First;
                            LinkedListNode<Level2Ask> tempAskNode = curLevel2Grid.DepthAsk.First;

                            while (!finished && tempBidNode != null && curCounter < 1000)
                            {
                                if (dicPrices.ContainsKey(tempBidNode.Value.Price))
                                {
                                    dicPrices[tempBidNode.Value.Price].BidShares += tempBidNode.Value.Quantity;
                                }
                                else
                                {
                                    NCustomLine curNCustomLine = new NCustomLine();
                                    curNCustomLine.BidShares += tempBidNode.Value.Quantity;
                                    curNCustomLine.Price = tempBidNode.Value.Price;
                                    dicPrices.Add(tempBidNode.Value.Price, curNCustomLine);
                                }
                                tempBidNode = tempBidNode.Next;
                                curCounter++;
                            }

                            curCounter = 0;

                            while (!finished && tempAskNode != null && curCounter < 1000)
                            {
                                if (dicPrices.ContainsKey(tempAskNode.Value.Price))
                                {
                                    dicPrices[tempAskNode.Value.Price].AskShares += tempAskNode.Value.Quantity;
                                }
                                else
                                {
                                    NCustomLine curNCustomLine = new NCustomLine();
                                    curNCustomLine.AskShares += tempAskNode.Value.Quantity;
                                    curNCustomLine.Price = tempAskNode.Value.Price;
                                    dicPrices.Add(tempAskNode.Value.Price, curNCustomLine);
                                }
                                tempAskNode = tempAskNode.Next;
                                curCounter++;
                            }

                            LinkedList<Level2Bid> tempListBid = new LinkedList<Level2Bid>();
                            LinkedList<Level2Ask> tempListAsk = new LinkedList<Level2Ask>();

                            double curAucPrice = curMarketItem.AucLast;
                            if (curAucPrice == 0) curAucPrice = (curMarketItem.Bid + curMarketItem.Ask) / 2;


                            Level2Bid AboveLevel2Bid = new Level2Bid();
                            Level2Ask BelowLevel2Ask = new Level2Ask();

                            long cumBid = 0;
                            long totalBid = 0;
                            long cumAsk = 0;

                            foreach (NCustomLine curNCustomLine in dicPrices.Values)
                            {
                                curNCustomLine.prevAskShares = cumAsk;
                                cumAsk += curNCustomLine.AskShares;
                                totalBid += curNCustomLine.BidShares;
                                curNCustomLine.cumAskShares = cumAsk;
                            }

                            foreach (NCustomLine curNCustomLine in dicPrices.Values)
                            {
                                //curNCustomLine.prevBidShares = cumBid;
                                curNCustomLine.cumBidShares = totalBid - cumBid;
                                curNCustomLine.setBidShares = cumBid;// -curNCustomLine.prevAskShares + 1;
                                cumBid += curNCustomLine.BidShares;
                            }

                            if (DisplayModeAuction)
                            {
                                AboveLevel2Bid.Price = curAucPrice + 0.00001;
                                AboveLevel2Bid.DisplayPrice = "ABOVE";
                                AboveLevel2Bid.Level = -1;
                                AboveLevel2Bid.Quantity = 0;


                                BelowLevel2Ask.Price = curAucPrice - 0.00001;
                                BelowLevel2Ask.DisplayPrice = "BELOW";
                                BelowLevel2Ask.Level = -1;
                                BelowLevel2Ask.Quantity = 0;
                                tempListAsk.AddFirst(BelowLevel2Ask);
                            }

                            int tempBidLevel = 0;
                            int tempAskLevel = 0;

                            Level2Bid prevLevel2Bid = new Level2Bid();
                            NCustomLine prevNCustomLine = new NCustomLine();

                            foreach (NCustomLine curNCustomLine in dicPrices.Values)
                            {
                                if (curNCustomLine.BidShares > 0)
                                {
                                    if (curNCustomLine.Price > curAucPrice && curNCustomLine.Price < 999999999 && DisplayModeAuction)
                                    {
                                        AboveLevel2Bid.Quantity += curNCustomLine.BidShares;
                                    }
                                    else
                                    {
                                        Level2Bid curLevel2Bid = new Level2Bid();
                                        curLevel2Bid.Price = curNCustomLine.Price;
                                        curLevel2Bid.Level = tempBidLevel++;
                                        curLevel2Bid.Quantity = curNCustomLine.BidShares;
                                        //curLevel2Bid.Broker = curNCustomLine.cumBidShares.ToString("#,###");
                                        if ((curNCustomLine.cumBidShares - prevNCustomLine.cumAskShares + 1 > 0) && DisplayModeAuction)
                                            prevLevel2Bid.Broker = (curNCustomLine.cumBidShares - prevNCustomLine.cumAskShares + 1).ToString("#,###");
                                        tempListBid.AddFirst(curLevel2Bid);
                                        prevLevel2Bid = curLevel2Bid;
                                        prevNCustomLine = curNCustomLine;


                                        //Console.WriteLine(" -->> curLevel2Bid.Quantity : " + curLevel2Bid.Quantity + " - curLevel2Bid.Level : " + curLevel2Bid.Level.ToString() + " - curLevel2Bid.Price: " + curLevel2Bid.Price.ToString() );
                                    }
                                }

                                long tempPrevAsk = 0;

                                if (curNCustomLine.AskShares > 0)
                                {
                                    if (curNCustomLine.Price < curAucPrice && curNCustomLine.Price > -999999999 && DisplayModeAuction)
                                    {
                                        BelowLevel2Ask.Quantity += curNCustomLine.AskShares;
                                    }
                                    else
                                    {
                                        Level2Ask curLevel2Ask = new Level2Ask();
                                        curLevel2Ask.Price = curNCustomLine.Price;
                                        curLevel2Ask.Level = tempAskLevel++;
                                        curLevel2Ask.Quantity = curNCustomLine.AskShares;
                                        if ((curNCustomLine.prevAskShares - curNCustomLine.cumBidShares + 1 > 0) && DisplayModeAuction)
                                            curLevel2Ask.Broker = (curNCustomLine.prevAskShares - curNCustomLine.cumBidShares + 1).ToString("#,###");
                                        tempListAsk.AddLast(curLevel2Ask);

                                        //Console.WriteLine(" -->> curLevel2Ask.Quantity : " + curLevel2Ask.Quantity + " - curLevel2Ask.Level : " + curLevel2Ask.Level.ToString() + " - curLevel2Ask.Price: " + curLevel2Ask.Price.ToString());

                                    }
                                    tempPrevAsk = curNCustomLine.cumAskShares;
                                }
                            }

                            tempListBid.AddFirst(AboveLevel2Bid);

                            if (AboveLevel2Bid.Quantity == 0) tempListBid.Remove(AboveLevel2Bid);
                            if (BelowLevel2Ask.Quantity == 0) tempListAsk.Remove(BelowLevel2Ask);

                            curBidNode = tempListBid.First;
                            curAskNode = tempListAsk.First;

                            //Console.WriteLine(GlobalVars.Instance.MasterClock.ElapsedMilliseconds - a);

                        }
                        else
                        {
                            curBidNode = curLevel2Grid.DepthBid.First;
                            curAskNode = curLevel2Grid.DepthAsk.First;
                        }

                        curCounter = 0;

                        while (!finished && curBidNode != null)
                        {
                            curCounter++;
                            if (curBidNode.Value.Price != prevPrice) curLevel++;

                            if (FlagLimit10 && curCounter >= 6) finished = true;

                            if (curBidNode.Value.Price != 0)
                            {
                                Level2Bid tempItem = new Level2Bid(curBidNode.Value, DefaultPriceStyle, curLevel);
                                if (curBidNode.Value.DisplayPrice == "ABOVE")
                                {
                                    tempItem.DisplayPrice = "ABOVE";
                                }
                                DisplayDepthBid.Add(tempItem);
                            }

                            prevPrice = curBidNode.Value.Price;

                            curBidNode = curBidNode.Next;
                        }

                        for (int i = 0; i < OrdersBid.Count; i++)
                        {
                            DisplayDepthBid.Add(new Level2Bid(OrdersBid[i], DefaultPriceStyle, (int)OrdersBid[i].Level));
                        }

                        if (radNCustom.Checked) DisplayDepthBid.Sort();

                        for (int i = 0; i < DisplayDepthBid.Count; i++)
                        {
                            if (DisplayDepthBid[i].Price > 9999999999)
                            {
                                DisplayDepthBid[i].DisplayPrice = "OPEN";

                                //txtPrice.Text = "0";
                                //chkPriceOpen.Checked = true;
                            }
                        }

                        curLevel = 0;
                        prevPrice = 0;
                        curCounter = 0;
                        finished = false;

                        while (!finished && curAskNode != null)
                        {
                            curCounter++;

                            if (curAskNode.Value.Price != prevPrice) curLevel++;

                            if (FlagLimit10 && curCounter >= 6) finished = true;

                            if (curAskNode.Value.Price != 0)
                            {
                                Level2Ask tempItem = new Level2Ask(curAskNode.Value, DefaultPriceStyle, curLevel);
                                if (curAskNode.Value.DisplayPrice == "BELOW")
                                {
                                    tempItem.DisplayPrice = "BELOW";
                                }
                                DisplayDepthAsk.Add(tempItem);
                            }
                            prevPrice = curAskNode.Value.Price;

                            curAskNode = curAskNode.Next;
                        }

                        for (int i = 0; i < OrdersAsk.Count; i++)
                        {
                            DisplayDepthAsk.Add(new Level2Ask(OrdersAsk[i], DefaultPriceStyle, (int)OrdersAsk[i].Level));
                        }

                        if (radNCustom.Checked) DisplayDepthAsk.Sort();

                        for (int i = 0; i < DisplayDepthAsk.Count; i++)
                        {
                            if (DisplayDepthAsk[i].Price < -9999999999)
                            {
                                DisplayDepthAsk[i].DisplayPrice = "OPEN";

                                //txtPrice.Text = "0";
                                //chkPriceOpen.Checked = true;
                            }
                        }

                        bndDataSourceBid.ResetBindings(false);
                        bndDataSourceAsk.ResetBindings(false);
                        updateGridColor();

                        dtgBid.ClearSelection();
                        dtgAsk.ClearSelection();

                        if (tempVisibleBid > 0) dtgBid.FirstDisplayedScrollingRowIndex = tempVisibleBid;
                        if (tempVisibleAsk > 0) dtgAsk.FirstDisplayedScrollingRowIndex = tempVisibleAsk;
                    }
                    catch
                    { }

                    //long endms = FIXConnections.Instance.curFixConn.MasterClock.ElapsedMilliseconds - startms;
                }

                if (ReplaceMode)
                {
                    labTicker.Text = _curIdOrder;
                }
                else
                {
                    labTicker.Text = _curTicker;
                }
                labIdTicker.Text = _curIdTicker.ToString();

                if (dtgBid.Columns.Count > 0 && dtgBid.Rows.Count > 0)
                {
                    if ((dtgBid.Rows[0].Height * dtgBid.Rows.Count) > dtgBid.Height)
                        dtgBid.Columns[3].Width = 53;
                    else
                        dtgBid.Columns[3].Width = 70;

                    if ((dtgBid.Rows[0].Height * dtgAsk.Rows.Count) > dtgAsk.Height)
                        dtgAsk.Columns[2].Width = 53;
                    else
                        dtgAsk.Columns[2].Width = 70;
                }

                if (DisplayModeAuction == true)
                {
                    if (curMarketItem != null) this.CalculaTIShares(curMarketItem.AucLast);

                    if (!chkDisplayAuction.Checked)
                        chkDisplayAuction.Checked = true;

                    panDisplayAuction.Visible = true;
                    panDisplayNormal.Visible = false;
                }
                else
                {
                    if (curMarketItem != null)
                    {
                        this.CalculaTIShares(curMarketItem.Last);
                        if (chkDisplayAuction.Checked)
                            chkDisplayAuction.Checked = false;

                        panDisplayAuction.Visible = false;
                        panDisplayNormal.Visible = true;
                    }
                }
            }
        }



        #region Comentado tmrUpdate_Tick
        /* Comentado - 2013-10-01 - Edson - Ordenação de novas ordens na fila
        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            bool FlagLimit10 = false;

            if (curMarketItem != null && PendingUpdateMktData)
            {
                PendingUpdateMktData = false;
                labLast.Text = curMarketItem.Last.ToString(DefaultPriceStyle);
                labLastAction.Text = curMarketItem.Last.ToString(DefaultPriceStyle);
                labChange.Text = curMarketItem.Change.ToString(DefaultChangeStyle);

                //labChangeAction.Text = (curMarketItem.Last / curMarketItem.AucLast).ToString();

                labVolume.Text = curMarketItem.Volume.ToString("#,##0");

                if (_curTicker.Length > 2)
                {
                    if (_curTicker.Substring(0, 3) == "DI1")
                    {
                        labChange.Text = (curMarketItem.Last - curMarketItem.Close).ToString(DefaultChangeStyle);
                    }
                }
                labHigh.Text = curMarketItem.High.ToString(DefaultPriceStyle);
                labLow.Text = curMarketItem.Low.ToString(DefaultPriceStyle);
                labAucLast.Text = curMarketItem.AucLast.ToString(DefaultPriceStyle);
                labAucEnd.Text = curMarketItem.AucCloseTime.ToString("HH:mm:ss");
                labAucVolume.Text = curMarketItem.AucVolume.ToString("#,##0");
                labTrdStatus.Text = curMarketItem.TradingStatus.ToString().Trim();

                labName.Text = curMarketItem.Name.Trim();
                labISIN.Text = curMarketItem.ISIN.Trim();
                labAvVolum.Text = curMarketItem.RecentAvgShares.ToString("#,##0");
                labTotalShares.Text = curMarketItem.TotalShares.ToString("#,##0");
                labLotePadrao.Text = curMarketItem.LotePadrao.ToString("#,##0.##");
                labSettlement.Text = curMarketItem.SettlementPrice.ToString(DefaultPriceStyle);
                labPrevSettlPrice.Text = curMarketItem.PrevSettlementPrice.ToString(DefaultPriceStyle);

                labClose.Text = curMarketItem.Close.ToString(DefaultPriceStyle);
                labStrike.Text = curMarketItem.Strike.ToString(DefaultPriceStyle);
                labExpiration.Text = curMarketItem.Expiration.ToString("dd-MMM-yy");
                labExpirationTime.Text = curMarketItem.ExpirationTime.ToString();
            }

            if (!BidMouseDown && !AskMouseDown && PendingUpdateDepth)
            {
                //long startms = FIXConnections.Instance.curFixConn.MasterClock.ElapsedMilliseconds;

                if (curMarketItem != null)
                {
                    try
                    {
                        int tempVisibleBid = dtgBid.FirstDisplayedScrollingRowIndex;
                        int tempVisibleAsk = dtgAsk.FirstDisplayedScrollingRowIndex;

                        PendingUpdateDepth = false;

                        DisplayDepthBid.Clear();
                        DisplayDepthAsk.Clear();

                        int curLevel = 0;
                        double prevPrice = 0;
                        bool finished = false;

                        LinkedListNode<Level2Bid> curBidNode;
                        LinkedListNode<Level2Ask> curAskNode;

                        int curCounter = 0;

                        long a = GlobalVars.Instance.MasterClock.ElapsedMilliseconds;

                        if (radNCustom.Checked)
                        {
                            FlagLimit10 = true;

                            SortedDictionary<double, NCustomLine> dicPrices = new SortedDictionary<double, NCustomLine>();
                            LinkedListNode<Level2Bid> tempBidNode = curLevel2Grid.DepthBid.First;
                            LinkedListNode<Level2Ask> tempAskNode = curLevel2Grid.DepthAsk.First;

                            while (!finished && tempBidNode != null && curCounter < 2000)
                            {
                                if (dicPrices.ContainsKey(tempBidNode.Value.Price))
                                {
                                    dicPrices[tempBidNode.Value.Price].BidShares += tempBidNode.Value.Quantity;
                                }
                                else
                                {
                                    NCustomLine curNCustomLine = new NCustomLine();
                                    curNCustomLine.BidShares += tempBidNode.Value.Quantity;
                                    curNCustomLine.Price = tempBidNode.Value.Price;
                                    dicPrices.Add(tempBidNode.Value.Price, curNCustomLine);
                                }
                                tempBidNode = tempBidNode.Next;
                                curCounter++;
                            }

                            curCounter = 0;

                            while (!finished && tempAskNode != null && curCounter < 1000)
                            {
                                if (dicPrices.ContainsKey(tempAskNode.Value.Price))
                                {
                                    dicPrices[tempAskNode.Value.Price].AskShares += tempAskNode.Value.Quantity;
                                }
                                else
                                {
                                    NCustomLine curNCustomLine = new NCustomLine();
                                    curNCustomLine.AskShares += tempAskNode.Value.Quantity;
                                    curNCustomLine.Price = tempAskNode.Value.Price;
                                    dicPrices.Add(tempAskNode.Value.Price, curNCustomLine);
                                }
                                tempAskNode = tempAskNode.Next;
                                curCounter++;
                            }

                            LinkedList<Level2Bid> tempListBid = new LinkedList<Level2Bid>();
                            LinkedList<Level2Ask> tempListAsk = new LinkedList<Level2Ask>();

                            double curAucPrice = curMarketItem.AucLast;
                            if (curAucPrice == 0) curAucPrice = (curMarketItem.Bid + curMarketItem.Ask) / 2;


                            Level2Bid AboveLevel2Bid = new Level2Bid();
                            Level2Ask BelowLevel2Ask = new Level2Ask();

                            long cumBid = 0;
                            long totalBid = 0;
                            long cumAsk = 0;

                            foreach (NCustomLine curNCustomLine in dicPrices.Values)
                            {
                                curNCustomLine.prevAskShares = cumAsk;
                                cumAsk += curNCustomLine.AskShares;
                                totalBid += curNCustomLine.BidShares;
                                curNCustomLine.cumAskShares = cumAsk;
                            }

                            foreach (NCustomLine curNCustomLine in dicPrices.Values)
                            {
                                //curNCustomLine.prevBidShares = cumBid;
                                curNCustomLine.cumBidShares = totalBid - cumBid;
                                curNCustomLine.setBidShares = cumBid;// -curNCustomLine.prevAskShares + 1;
                                cumBid += curNCustomLine.BidShares;
                            }

                            if (DisplayModeAuction)
                            {
                                AboveLevel2Bid.Price = curAucPrice + 0.00001;
                                AboveLevel2Bid.DisplayPrice = "ABOVE";
                                AboveLevel2Bid.Level = -1;
                                AboveLevel2Bid.Quantity = 0;
                                

                                BelowLevel2Ask.Price = curAucPrice - 0.00001;
                                BelowLevel2Ask.DisplayPrice = "BELOW";
                                BelowLevel2Ask.Level = -1;
                                BelowLevel2Ask.Quantity = 0;
                                tempListAsk.AddFirst(BelowLevel2Ask);
                            }

                            int tempBidLevel = 0;
                            int tempAskLevel = 0;

                            Level2Bid prevLevel2Bid = new Level2Bid();
                            NCustomLine prevNCustomLine = new NCustomLine();

                            foreach (NCustomLine curNCustomLine in dicPrices.Values)
                            {
                                if (curNCustomLine.BidShares > 0)
                                {
                                    if (curNCustomLine.Price > curAucPrice && curNCustomLine.Price < 999999999 && DisplayModeAuction)
                                    {
                                        AboveLevel2Bid.Quantity += curNCustomLine.BidShares;
                                    }
                                    else
                                    {
                                        Level2Bid curLevel2Bid = new Level2Bid();
                                        curLevel2Bid.Price = curNCustomLine.Price;
                                        curLevel2Bid.Level = tempBidLevel++;
                                        curLevel2Bid.Quantity = curNCustomLine.BidShares;
                                        //curLevel2Bid.Broker = curNCustomLine.cumBidShares.ToString("#,###");
                                        if ((curNCustomLine.cumBidShares - prevNCustomLine.cumAskShares + 1 > 0) && DisplayModeAuction)
                                            prevLevel2Bid.Broker = (curNCustomLine.cumBidShares - prevNCustomLine.cumAskShares + 1).ToString("#,###");
                                        tempListBid.AddFirst(curLevel2Bid);
                                        prevLevel2Bid = curLevel2Bid;
                                        prevNCustomLine = curNCustomLine;


                                        //Console.WriteLine(" -->> curLevel2Bid.Quantity : " + curLevel2Bid.Quantity + " - curLevel2Bid.Level : " + curLevel2Bid.Level.ToString() + " - curLevel2Bid.Price: " + curLevel2Bid.Price.ToString() );
                                    }
                                }

                                long tempPrevAsk = 0;

                                if (curNCustomLine.AskShares > 0)
                                {
                                    if (curNCustomLine.Price < curAucPrice && curNCustomLine.Price > -999999999 && DisplayModeAuction)
                                    {
                                        BelowLevel2Ask.Quantity += curNCustomLine.AskShares;
                                    }
                                    else
                                    {
                                        Level2Ask curLevel2Ask = new Level2Ask();
                                        curLevel2Ask.Price = curNCustomLine.Price;
                                        curLevel2Ask.Level = tempAskLevel++;
                                        curLevel2Ask.Quantity = curNCustomLine.AskShares;
                                        if ((curNCustomLine.prevAskShares - curNCustomLine.cumBidShares + 1 > 0) && DisplayModeAuction)
                                            curLevel2Ask.Broker = (curNCustomLine.prevAskShares - curNCustomLine.cumBidShares + 1).ToString("#,###");
                                        tempListAsk.AddLast(curLevel2Ask);

                                        //Console.WriteLine(" -->> curLevel2Ask.Quantity : " + curLevel2Ask.Quantity + " - curLevel2Ask.Level : " + curLevel2Ask.Level.ToString() + " - curLevel2Ask.Price: " + curLevel2Ask.Price.ToString());

                                    }
                                    tempPrevAsk = curNCustomLine.cumAskShares;
                                }
                            }

                            tempListBid.AddFirst(AboveLevel2Bid);

                            if (AboveLevel2Bid.Quantity == 0) tempListBid.Remove(AboveLevel2Bid);
                            if (BelowLevel2Ask.Quantity == 0) tempListAsk.Remove(BelowLevel2Ask);

                            curBidNode = tempListBid.First;
                            curAskNode = tempListAsk.First;

                            //Console.WriteLine(GlobalVars.Instance.MasterClock.ElapsedMilliseconds - a);

                        }
                        else
                        {
                            curBidNode = curLevel2Grid.DepthBid.First;
                            curAskNode = curLevel2Grid.DepthAsk.First;
                        }

                        curCounter = 0;

                        while (!finished && curBidNode != null)
                        {
                            curCounter++;
                            if (curBidNode.Value.Price != prevPrice) curLevel++;

                            if (FlagLimit10 && curCounter >= 5) finished = true;

                            if (curBidNode.Value.Price != 0)
                            {
                                Level2Bid tempItem = new Level2Bid(curBidNode.Value, DefaultPriceStyle, curLevel);
                                if (curBidNode.Value.DisplayPrice == "ABOVE") 
                                { 
                                    tempItem.DisplayPrice = "ABOVE"; 
                                }
                                DisplayDepthBid.Add(tempItem);
                            }

                            prevPrice = curBidNode.Value.Price;

                            curBidNode = curBidNode.Next;
                        }

                        for (int i = 0; i < OrdersBid.Count; i++)
                        {
                            DisplayDepthBid.Add(new Level2Bid(OrdersBid[i], DefaultPriceStyle, (int)OrdersBid[i].Level));
                        }

                        //DisplayDepthBid.Sort();

                        for (int i = 0; i < DisplayDepthBid.Count; i++)
                        {
                            if (DisplayDepthBid[i].Price > 9999999999)
                            {
                                DisplayDepthBid[i].DisplayPrice = "OPEN";

                                //txtPrice.Text = "0";
                                //chkPriceOpen.Checked = true;
                            }
                        }

                        curLevel = 0;
                        prevPrice = 0;
                        curCounter = 0;
                        finished = false;

                        while (!finished && curAskNode != null)
                        {
                            curCounter++;

                            if (curAskNode.Value.Price != prevPrice) curLevel++;

                            if (FlagLimit10 && curCounter >= 5) finished = true;

                            if (curAskNode.Value.Price != 0)
                            {
                                Level2Ask tempItem = new Level2Ask(curAskNode.Value, DefaultPriceStyle, curLevel);
                                if (curAskNode.Value.DisplayPrice == "BELOW") 
                                { 
                                    tempItem.DisplayPrice = "BELOW"; 
                                }
                                DisplayDepthAsk.Add(tempItem);
                            }
                            prevPrice = curAskNode.Value.Price;

                            curAskNode = curAskNode.Next;
                        }

                        for (int i = 0; i < OrdersAsk.Count; i++)
                        {
                            DisplayDepthAsk.Add(new Level2Ask(OrdersAsk[i], DefaultPriceStyle, (int)OrdersAsk[i].Level));
                        }

                        //DisplayDepthAsk.Sort();

                        for (int i = 0; i < DisplayDepthAsk.Count; i++)
                        {
                            if (DisplayDepthAsk[i].Price < -9999999999)
                            {
                                DisplayDepthAsk[i].DisplayPrice = "OPEN";

                                //txtPrice.Text = "0";
                                //chkPriceOpen.Checked = true;
                            }
                        }

                        bndDataSourceBid.ResetBindings(false);
                        bndDataSourceAsk.ResetBindings(false);
                        updateGridColor();

                        dtgBid.ClearSelection();
                        dtgAsk.ClearSelection();

                        if (tempVisibleBid > 0) dtgBid.FirstDisplayedScrollingRowIndex = tempVisibleBid;
                        if (tempVisibleAsk > 0) dtgAsk.FirstDisplayedScrollingRowIndex = tempVisibleAsk;
                    }
                    catch
                    { }

                    //long endms = FIXConnections.Instance.curFixConn.MasterClock.ElapsedMilliseconds - startms;
                }

                if (ReplaceMode)
                {
                    labTicker.Text = _curIdOrder;
                }
                else
                {
                    labTicker.Text = _curTicker;
                }
                labIdTicker.Text = _curIdTicker.ToString();

                if (dtgBid.Columns.Count > 0 && dtgBid.Rows.Count > 0)
                {
                    if ((dtgBid.Rows[0].Height * dtgBid.Rows.Count) > dtgBid.Height)
                        dtgBid.Columns[3].Width = 53;
                    else
                        dtgBid.Columns[3].Width = 70;

                    if ((dtgBid.Rows[0].Height * dtgAsk.Rows.Count) > dtgAsk.Height)
                        dtgAsk.Columns[2].Width = 53;
                    else
                        dtgAsk.Columns[2].Width = 70;
                }

                if (DisplayModeAuction == true)
                {
                    if (curMarketItem != null) this.CalculaTIShares(curMarketItem.AucLast);

                    if (!chkDisplayAuction.Checked)
                        chkDisplayAuction.Checked = true;

                    panDisplayAuction.Visible = true;
                    panDisplayNormal.Visible = false;
                }
                else
                {
                    if (curMarketItem != null)
                    {
                        this.CalculaTIShares(curMarketItem.Last);
                        if (chkDisplayAuction.Checked)
                            chkDisplayAuction.Checked = false;

                        panDisplayAuction.Visible = false;
                        panDisplayNormal.Visible = true;
                    }
                }
            }
        }

        */
        #endregion


        private void updateGridColor()
        {
            for (int i = 0; i < dtgBid.Rows.Count; i++)
            {
                int multBid = int.Parse(dtgBid.Rows[i].Cells[0].Value.ToString());

                if (multBid > 0 && multBid < 13 && (double)dtgBid.Rows[i].Cells[4].Value != 0)
                {
                    //dtgBid.Rows[i].Cells[1].Style.SelectionBackColor = Color.FromArgb(255 - multBid * 20, 255 - multBid * 12, 252);
                    //dtgBid.Rows[i].Cells[1].Style.SelectionForeColor = Color.Black;
                    dtgBid.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255 - multBid * 20, 255 - multBid * 12, 252);
                    dtgBid.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255 - multBid * 20, 255 - multBid * 12, 252);
                    dtgBid.Rows[i].Cells[5].Style.BackColor = Color.FromArgb(255 - multBid * 20, 255 - multBid * 12, 252);
                }
                else if (multBid < 0)
                {
                    if (dtgBid.Rows[i].Cells[2].Value.ToString().Contains("NEW_"))
                    {
                        dtgBid.Rows[i].Cells[2].Style.BackColor = Color.OrangeRed;
                        dtgBid.Rows[i].Cells[3].Style.BackColor = Color.OrangeRed;
                        dtgBid.Rows[i].Cells[5].Style.BackColor = Color.OrangeRed;

                        dtgBid.Rows[i].Cells[2].Style.ForeColor = Color.White;
                        dtgBid.Rows[i].Cells[3].Style.ForeColor = Color.White;
                        dtgBid.Rows[i].Cells[5].Style.ForeColor = Color.White;
                    }
                    else if (dtgBid.Rows[i].Cells[2].Value == _curIdOrder)
                    {
                        dtgBid.Rows[i].Cells[2].Style.BackColor = Color.Black;
                        dtgBid.Rows[i].Cells[3].Style.BackColor = Color.Black;
                        dtgBid.Rows[i].Cells[5].Style.BackColor = Color.Black;

                        dtgBid.Rows[i].Cells[2].Style.ForeColor = Color.White;
                        dtgBid.Rows[i].Cells[3].Style.ForeColor = Color.White;
                        dtgBid.Rows[i].Cells[5].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        dtgBid.Rows[i].Cells[2].Style.BackColor = Color.Orange;
                        dtgBid.Rows[i].Cells[3].Style.BackColor = Color.Orange;
                        dtgBid.Rows[i].Cells[5].Style.BackColor = Color.Orange;

                        dtgBid.Rows[i].Cells[2].Style.ForeColor = Color.Black;
                        dtgBid.Rows[i].Cells[3].Style.ForeColor = Color.Black;
                        dtgBid.Rows[i].Cells[5].Style.ForeColor = Color.Black;
                    }
                }

                if (DisplayModeAuction)
                {
                    if ((((double)dtgBid.Rows[i].Cells[4].Value == curMarketItem.AucLast && double.Parse(dtgBid.Rows[i].Cells[3].Value.ToString()) != 0) || dtgBid.Rows[i].Cells[5].Value.ToString() == "ABOVE" || dtgBid.Rows[i].Cells[5].Value.ToString() == "OPEN") && DisplayModeAuction)
                    {
                        dtgBid.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                        dtgBid.Rows[i].Cells[3].Style.BackColor = Color.Yellow;
                        dtgBid.Rows[i].Cells[5].Style.BackColor = Color.Yellow;
                    }
                }
            }

            for (int i = 0; i < dtgAsk.Rows.Count; i++)
            {
                int multAsk = int.Parse(dtgAsk.Rows[i].Cells[5].Value.ToString());

                if (multAsk > 0 && multAsk < 13 && (double)dtgAsk.Rows[i].Cells[1].Value != 0)
                {
                    dtgAsk.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(255 - multAsk * 20, 255 - multAsk * 12, 252);
                    dtgAsk.Rows[i].Cells[2].Style.BackColor = Color.FromArgb(255 - multAsk * 20, 255 - multAsk * 12, 252);
                    dtgAsk.Rows[i].Cells[3].Style.BackColor = Color.FromArgb(255 - multAsk * 20, 255 - multAsk * 12, 252);
                }
                else if (multAsk < 0)
                {
                    if (dtgAsk.Rows[i].Cells[2].Value.ToString().Contains("NEW_"))
                    {
                        dtgAsk.Rows[i].Cells[0].Style.BackColor = Color.OrangeRed;
                        dtgAsk.Rows[i].Cells[2].Style.BackColor = Color.OrangeRed;
                        dtgAsk.Rows[i].Cells[3].Style.BackColor = Color.OrangeRed;

                        dtgAsk.Rows[i].Cells[0].Style.ForeColor = Color.White;
                        dtgAsk.Rows[i].Cells[2].Style.ForeColor = Color.White;
                        dtgAsk.Rows[i].Cells[3].Style.ForeColor = Color.White;
                    }
                    else if (dtgAsk.Rows[i].Cells[3].Value == _curIdOrder)
                    {
                        dtgAsk.Rows[i].Cells[0].Style.BackColor = Color.Black;
                        dtgAsk.Rows[i].Cells[2].Style.BackColor = Color.Black;
                        dtgAsk.Rows[i].Cells[3].Style.BackColor = Color.Black;

                        dtgAsk.Rows[i].Cells[0].Style.ForeColor = Color.White;
                        dtgAsk.Rows[i].Cells[2].Style.ForeColor = Color.White;
                        dtgAsk.Rows[i].Cells[3].Style.ForeColor = Color.White;
                    }
                    else
                    {
                        dtgAsk.Rows[i].Cells[0].Style.BackColor = Color.Orange;
                        dtgAsk.Rows[i].Cells[2].Style.BackColor = Color.Orange;
                        dtgAsk.Rows[i].Cells[3].Style.BackColor = Color.Orange;

                        dtgAsk.Rows[i].Cells[0].Style.ForeColor = Color.Black;
                        dtgAsk.Rows[i].Cells[2].Style.ForeColor = Color.Black;
                        dtgAsk.Rows[i].Cells[3].Style.ForeColor = Color.Black;
                    }
                }
                #region
                if (DisplayModeAuction)
                {
                    if ((((double)dtgAsk.Rows[i].Cells[1].Value == curMarketItem.AucLast && double.Parse(dtgBid.Rows[i].Cells[3].Value.ToString()) != 0) || dtgAsk.Rows[i].Cells[0].Value.ToString() == "BELOW" || dtgAsk.Rows[i].Cells[0].Value.ToString() == "OPEN") && DisplayModeAuction)
                    {
                        dtgAsk.Rows[i].Cells[0].Style.BackColor = Color.Yellow;
                        dtgAsk.Rows[i].Cells[2].Style.BackColor = Color.Yellow;
                        dtgAsk.Rows[i].Cells[3].Style.BackColor = Color.Yellow;
                    }
                }
                #endregion
            }
        }

        public void cmdRequest_Click(object sender, EventArgs e)
        {
            _curExchange = GlobalVars.Instance.getIdPrimaryExchange(_curTicker);
            UpdateTicker(txtTicker.Text.Trim(), _curExchange);
        }

        public void UpdateTicker(string Ticker, int Exchange)
        {
            tmrUpdate.Stop();

            Ticker = Ticker.ToUpper();

            if (Ticker != _curTicker)
            {
                string prevTicker = _curTicker;
                _curTicker = Ticker;

                _curIdInstrument = GlobalVars.Instance.getIdInstrument(_curTicker);
                curSource = GlobalVars.Instance.getDataSource(_curTicker);

                // if (curSource == Sources.BMF) curSource = Sources.LINKBMF;     // ================================================== COMENTAR
                // if (curSource == Sources.Bovespa) curSource = Sources.LINKBOV; // ================================================== COMENTAR
                // if (curSource == Sources.Bovespa) curSource = Sources.XPBOV;   // ================================================== COMENTAR
                // if (curSource == Sources.BMF) curSource = Sources.XPBMF;   // ================================================== COMENTAR

                // Se nao encontrar, força envio via Bovespa 
                if (_curIdInstrument == 0) _curIdInstrument = 2;
                if (curSource == Sources.None) curSource = Sources.Bovespa;

                if (_curTicker != "")
                {
                    curNDistConn.UnSubscribe(prevTicker, curSource);
                    curNDistConn.UnSubscribeDepth(prevTicker, curSource);
                    curNDistConn.UnSubscribeDepthAgg(prevTicker, curSource);
                }

                curLevel2Grid.DepthBid.Clear();
                curLevel2Grid.DepthAsk.Clear();

                DisplayDepthBid.Clear();
                DisplayDepthAsk.Clear();

                bndDataSourceBid.ResetBindings(false);
                bndDataSourceAsk.ResetBindings(false);

                //dLast = 0;
                //dVolume = 0;
                curMarketItem = new LTMarketDataItem();

                _curIdTicker = GlobalVars.Instance.getIdSecurity(_curTicker);
                if (_curIdTicker == 0)
                {
                    MessageBox.Show("Ticker NOT FOUND!!! \r\n\r\nWILL SHOW MARKET DATA BUT NOT ENTER TRADES.");
                }

                // if (curSource == Sources.Bovespa) curSource = Sources.LINKBOV; // <<<<<<<<================================================== COMENTAR
                // if (curSource == Sources.Bovespa) curSource = Sources.XPBOV;   // <<<<<<<<================================================== COMENTAR
                curNDistConn.Subscribe(_curTicker, curSource);

                SubscribeDepth(_curTicker, DepthStateAGG);

                this.Text = _curTicker;
                txtTicker.Text = _curTicker;

                _curExchange = Exchange;

                if (Ticker.ToUpper().Length > 2)
                {
                    if (Ticker.Substring(0, 3) == "IND" || Ticker.Substring(0, 3) == "WIN")
                    {
                        DefaultPriceStyle = "#,##0.;-#,##0.;\\ ";
                    }
                    else
                    {
                        DefaultPriceStyle = "#,##0.00;-#,##0.00;\\ ";
                    }
                    if (Ticker.Substring(0, 3) == "DI1")
                    {
                        DefaultChangeStyle = "0.00";
                    }
                    else
                    {
                        DefaultChangeStyle = "0.00%";
                    }
                }
            }

            if (dtgBid.Columns.Count > 2)
            {
                dtgBid.Columns[4].DefaultCellStyle.Format = DefaultPriceStyle;
                dtgAsk.Columns[0].DefaultCellStyle.Format = DefaultPriceStyle;
            }

            cmbBook.DataSource = GlobalVars.Instance.dtBooks;
            cmbBook.ValueMember = "Id_Book";
            cmbBook.DisplayMember = "Book";

            cmbSection.DataSource = GlobalVars.Instance.dtSections;
            cmbSection.ValueMember = "Id_Section";
            cmbSection.DisplayMember = "Section";

            ToOrderMode();
            UpdateDefaults();
            LoadOrders();

            LoadDefaultUpdatePrice();

            //this.SetaBroker();
            if (_curIdInstrument == 2) { cmbBroker.SelectedItem = "XP"; }
            if (_curIdInstrument == 2) { cmbBroker.SelectedItem = "XPBOVE"; }
            //if (_curIdInstrument == 2) { cmbBroker.SelectedItem = "XPBOV"; }

            if (_curIdInstrument == 4 || _curIdInstrument == 16) { cmbBroker.SelectedItem = "XPBMF"; }
            if (_curTicker.Contains("DOL")) { cmbPortfolio.SelectedItem = "BROKER"; }
            //if (_curIdInstrument == 4 || _curIdInstrument == 16) { cmbBroker.SelectedItem = "XPBMFH"; }

            tmrUpdate.Start();

            this.LimitesEntryPointLine(txtTicker.Text.ToUpper());
            txtTicker.Text = "";
        }

        private void frmLevel2_FormClosing(object sender, FormClosingEventArgs e)
        {
            curNDistConn.Dispose();
        }

        private void txtTicker_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.LimitesEntryPointLine(txtTicker.Text.ToUpper());
                cmdRequest_Click(sender, e);
                // txtTicker.Text = txtTicker.Text.ToUpper();
                dtgBid.Focus();
            }
        }

        private void CalculaTIShares(double LastPrice)
        {
            lblTISBuy.Text = (dTISBuy / LastPrice).ToString("#,###");
            lblTISSell.Text = (dTISSell / LastPrice).ToString("#,###");
        }

        private void LimitesEntryPointLine(string Ticker)
        {
            string SQLString = string.Format("SELECT TOP 1 Ticker, TCI, TVI, LCI, LVI, TCD, TVD, LCD, LVD, LCC, LVC FROM NESTDB.dbo.Tb800_LimitesBolsaEntryPoint WHERE Ticker = '{0}'", Ticker);

            newNestConn curConn = new newNestConn();
            System.Data.SqlClient.SqlDataReader dr = curConn.Return_DataReader(SQLString);

            if (dr.Read())
            {
                dTISBuy = Convert.ToDouble(dr["TCI"]);
                dTISSell = Convert.ToDouble(dr["TVI"]);

                if ((double)dr["TCI"] < 1000000) { lblTIBuy.Text = (Convert.ToDouble(dr["TCI"]) / 1000).ToString() + " K"; } else { lblTIBuy.Text = (Convert.ToDouble(dr["TCI"]) / 1000000).ToString() + " M"; }
                if ((double)dr["TVI"] < 1000000) { lblTISell.Text = (Convert.ToDouble(dr["TVI"]) / 1000).ToString() + " K"; } else { lblTISell.Text = (Convert.ToDouble(dr["TVI"]) / 1000000).ToString() + " M"; }
                if ((double)dr["TCD"] < 1000000) { lblTDBuy.Text = (Convert.ToDouble(dr["TCD"]) / 1000).ToString() + " K"; } else { lblTDBuy.Text = (Convert.ToDouble(dr["TCD"]) / 1000000).ToString() + " M"; }
                if ((double)dr["TVD"] < 1000000) { lblTDSell.Text = (Convert.ToDouble(dr["TVD"]) / 1000).ToString() + " K"; } else { lblTDSell.Text = (Convert.ToDouble(dr["TVD"]) / 1000000).ToString() + " M"; }
                if ((double)dr["LCD"] < 1000000) { lblLDBuy.Text = (Convert.ToDouble(dr["LCD"]) / 1000).ToString() + " K"; } else { lblLDBuy.Text = (Convert.ToDouble(dr["LCD"]) / 1000000).ToString() + " M"; }
                if ((double)dr["LVD"] < 1000000) { lblLDSell.Text = (Convert.ToDouble(dr["LVD"]) / 1000).ToString() + " K"; } else { lblLDSell.Text = (Convert.ToDouble(dr["LVD"]) / 1000000).ToString() + " M"; }
                if ((double)dr["LCI"] < 1000000) { lblLCBuy.Text = (Convert.ToDouble(dr["LCI"]) / 1000).ToString() + " K"; } else { lblLCBuy.Text = (Convert.ToDouble(dr["LCI"]) / 1000000).ToString() + " M"; }
                if ((double)dr["LVI"] < 1000000) { lblLCSell.Text = (Convert.ToDouble(dr["LVI"]) / 1000).ToString() + " K"; } else { lblLCSell.Text = (Convert.ToDouble(dr["LVI"]) / 1000000).ToString() + " M"; }
                if ((double)dr["LCC"] < 1000000) { lblLIBuy.Text = (Convert.ToDouble(dr["LCC"]) / 1000).ToString() + " K"; } else { lblLIBuy.Text = (Convert.ToDouble(dr["LCC"]) / 1000000).ToString() + " M"; }
                if ((double)dr["LVC"] < 1000000) { lblLISell.Text = (Convert.ToDouble(dr["LVC"]) / 1000).ToString() + " K"; } else { lblLISell.Text = (Convert.ToDouble(dr["LVC"]) / 1000000).ToString() + " M"; }

                // lblTIBuy.Text = dr["TCI"].ToString();
                // lblTISell.Text = dr["TVI"].ToString();

                //lblLCBuy.Text = dr["LCI"].ToString();
                //lblLCSell.Text = dr["LVI"].ToString();

                //lblLDBuy.Text = dr["TCD"].ToString();
                //lblLDSell.Text = dr["TVD"].ToString();

                //lblTDBuy.Text = dr["TCD"].ToString();
                //lblTDSell.Text = dr["TVD"].ToString();

                //lblLIBuy.Text = dr["LCC"].ToString();
                //lblLISell.Text = dr["LVC"].ToString();
            }
            else
            {
                lblTIBuy.Text = "N/A";
                lblTISell.Text = "N/A";

                lblLCBuy.Text = "N/A";
                lblLCSell.Text = "N/A";

                lblLDBuy.Text = "N/A";
                lblLDSell.Text = "N/A";

                lblTDBuy.Text = "N/A";
                lblTDSell.Text = "N/A";

                lblLIBuy.Text = "N/A";
                lblLISell.Text = "N/A";
            }
        }

        private void AddTooltips()
        {
            System.Windows.Forms.ToolTip ToolTipTIBuy = new System.Windows.Forms.ToolTip();
            System.Windows.Forms.ToolTip ToolTipTISell = new System.Windows.Forms.ToolTip();
            System.Windows.Forms.ToolTip ToolTipLIBuy = new System.Windows.Forms.ToolTip();
            System.Windows.Forms.ToolTip ToolTipLISell = new System.Windows.Forms.ToolTip();
            System.Windows.Forms.ToolTip ToolTipTDBuy = new System.Windows.Forms.ToolTip();
            System.Windows.Forms.ToolTip ToolTipTDSell = new System.Windows.Forms.ToolTip();
            System.Windows.Forms.ToolTip ToolTipLDBuy = new System.Windows.Forms.ToolTip();
            System.Windows.Forms.ToolTip ToolTipLDSell = new System.Windows.Forms.ToolTip();
            System.Windows.Forms.ToolTip ToolTipLCBuy = new System.Windows.Forms.ToolTip();
            System.Windows.Forms.ToolTip ToolTipLCSell = new System.Windows.Forms.ToolTip();

            ToolTipTIBuy.SetToolTip(lblTIBuy, "Financeiro máximo de ordem de compra por instrumento.");
            ToolTipTIBuy.IsBalloon = true;
            ToolTipTIBuy.ShowAlways = true;

            ToolTipTISell.SetToolTip(lblTISell, "Financeiro máximo de ordem de venda por instrumento.");
            ToolTipTISell.IsBalloon = true;
            ToolTipTISell.ShowAlways = true;

            ToolTipLIBuy.SetToolTip(lblLIBuy, "Limite financeiro da posição comprada por instrumento.");
            ToolTipLIBuy.IsBalloon = true;
            ToolTipLIBuy.ShowAlways = true;

            ToolTipLISell.SetToolTip(lblLISell, "Limite financeiro da posição vendida por instrumento.");
            ToolTipLISell.IsBalloon = true;
            ToolTipLISell.ShowAlways = true;

            ToolTipTDBuy.SetToolTip(lblTDBuy, "Tamanho máximo de ordem de compra por contrato padrão.");
            ToolTipTDBuy.IsBalloon = true;
            ToolTipTDBuy.ShowAlways = true;

            ToolTipTDSell.SetToolTip(lblTDSell, "Tamanho máximo de ordem de venda por contrato padrão.");
            ToolTipTDSell.IsBalloon = true;
            ToolTipTDSell.ShowAlways = true;

            ToolTipLDBuy.SetToolTip(lblLDBuy, "Limite de posição comprada por contrato padrão.");
            ToolTipLDBuy.IsBalloon = true;
            ToolTipLDBuy.ShowAlways = true;

            ToolTipLDSell.SetToolTip(lblLDSell, "Limite de posição vendida por contrato padrão.");
            ToolTipLDSell.IsBalloon = true;
            ToolTipLDSell.ShowAlways = true;

            ToolTipLCBuy.SetToolTip(lblLCBuy, "Limite financeiro da posição comprada por Instrumento Equivalente.");
            ToolTipLCBuy.IsBalloon = true;
            ToolTipLCBuy.ShowAlways = true;

            ToolTipLCSell.SetToolTip(lblLCSell, "Limite financeiro da posição vendida por Instrumento Equivalente.");
            ToolTipLCSell.IsBalloon = true;
            ToolTipLCSell.ShowAlways = true;
        }

        private void frmLevel2_KeyDown(object sender, KeyEventArgs e)
        {
            txtTicker.Focus();
        }

        //private void dtgLevel2_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if ((int)e.KeyCode > 64 && (int)e.KeyCode < 100)
        //    {
        //        txtTicker.Text = e.KeyCode.ToString();
        //        txtTicker.SelectionStart = 2;
        //        txtTicker.Focus();
        //    }
        //}

        private bool CheckFIX()
        {
            if (FIXConnections.Instance.curFixConn != null)
            {
                if (FIXConnections.Instance.curFixConn.IsAliveSession)
                {
                    if (!FIXConnections.Instance.curFixConn.curLimits.OrderSendBlocked)
                        return true;
                    else
                        MessageBox.Show("Trading Disabled! Click on button in main screen to enable trading.", "ERROR: Trading Disabled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Not connected to Nest FIX Router!", "ERROR: FIX not Connected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("FIX not enabled!", "ERROR: FIX not enabled", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        private bool bValidaCamposTela()
        {
            bool bAuxi = true;

            if (txtQuantity.Text.Trim() == "")
            {
                MessageBox.Show("Invalid Quantity");
                bAuxi = false;
            }

            if (txtPrice.Text.Trim() == "")
            {
                MessageBox.Show("Invalid Quantity");
                bAuxi = false;
            }

            if (txtDisplayShares.Text.Trim() == "")
            {
                MessageBox.Show("Invalid display shares");
                bAuxi = false;
            }

            //if (Convert.ToDecimal(txtPrice.Text) != 0)
            //{
            //    if (Math.Abs(Convert.ToDecimal(labLast.Text) / Convert.ToDecimal(txtPrice.Text) - 1) > Convert.ToDecimal(0.15))
            //    {
            //        int iResposta = Convert.ToInt32(MessageBox.Show("The price differs more than 15% from last price. Would you like to insert anyway?", "Live Trade", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
            //        if (iResposta != 6) bAuxi = false;
            //    }
            //}

            return bAuxi;
        }

        private void cmdBuy_Click(object sender, EventArgs e)
        {
            if (!bValidaCamposTela())
                return;

            if (CheckFIX())
            {
                int curQuantity = int.Parse(txtQuantity.Text);
                double curPrice = double.Parse(txtPrice.Text);
                int curDisplayShares = int.Parse(txtDisplayShares.Text);

                int IdAccount;
                int.TryParse(txtAccount.Text, out IdAccount);

                if (chkMKT.Checked) curPrice = -1;
                if (chkPriceOpen.Checked) curPrice = -2;

                int IdPortfolio = 0;
                int IdBook = 0;
                int IdSection = 0;

                // cmbPortfolio.SelectedItem = IdPortfolio;

                if (cmbPortfolio.SelectedValue.ToString() == "BROKER") IdPortfolio = 48;
                else if (cmbPortfolio.SelectedValue.ToString() == "FIA") IdPortfolio = 10;
                else if (cmbPortfolio.SelectedValue.ToString() == "ARB") IdPortfolio = 38;
                else if (cmbPortfolio.SelectedValue.ToString() == "TOP") IdPortfolio = 4;
                else if (cmbPortfolio.SelectedValue.ToString() == "MH") IdPortfolio = 43;
                else if (cmbPortfolio.SelectedValue.ToString() == "QUANT") IdPortfolio = 18;
                else if (cmbPortfolio.SelectedValue.ToString() == "PREV") IdPortfolio = 50;
                else if (cmbPortfolio.SelectedValue.ToString() == "ICATU") IdPortfolio = 55;

                if (!int.TryParse(cmbBook.SelectedValue.ToString(), out IdBook))
                {
                    MessageBox.Show("Invalid Book!");
                    return;
                }

                if (!int.TryParse(cmbSection.SelectedValue.ToString(), out IdSection))
                {
                    MessageBox.Show("Invalid Section!");
                    return;
                }

                string testOrderID = FIXConnections.Instance.curFixConn.sendOrder(IdAccount, _curIdTicker, curQuantity, curPrice, curDisplayShares, IdPortfolio, IdBook, IdSection, new QuickFix.DeliverToCompID(cmbBroker.SelectedValue.ToString()), GlobalVars.Instance.GetSessionType(IdAccount.ToString()));
            }
        }

        private void cmdSell_Click(object sender, EventArgs e)
        {
            if (!bValidaCamposTela()) return;

            if (CheckFIX())
            {
                //string testOrderID = FIXConnections.Instance.curFixConn.sendOrder(1, 100, 10, 38, 1, 1);

                int curQuantity = int.Parse(txtQuantity.Text);
                double curPrice = double.Parse(txtPrice.Text);
                int curDisplayShares = int.Parse(txtDisplayShares.Text);

                int IdAccount;
                int.TryParse(txtAccount.Text, out IdAccount);

                if (chkMKT.Checked) curPrice = -1;
                if (chkPriceOpen.Checked) curPrice = -2;

                int IdPortfolio = 0;
                int IdBook = 0;
                int IdSection = 0;

                //cmbPortfolio.SelectedItem = IdPortfolio;
                if (cmbPortfolio.SelectedValue.ToString() == "BROKER") IdPortfolio = 48;
                else if (cmbPortfolio.SelectedValue.ToString() == "FIA") IdPortfolio = 10;
                else if (cmbPortfolio.SelectedValue.ToString() == "ARB") IdPortfolio = 38;
                else if (cmbPortfolio.SelectedValue.ToString() == "TOP") IdPortfolio = 4;
                else if (cmbPortfolio.SelectedValue.ToString() == "MH") IdPortfolio = 43;
                else if (cmbPortfolio.SelectedValue.ToString() == "QUANT") IdPortfolio = 18;
                else if (cmbPortfolio.SelectedValue.ToString() == "PREV") IdPortfolio = 50;

                if (!int.TryParse(cmbBook.SelectedValue.ToString(), out IdBook))
                {
                    MessageBox.Show("Invalid Book!");
                    return;
                }

                if (!int.TryParse(cmbSection.SelectedValue.ToString(), out IdSection))
                {
                    MessageBox.Show("Invalid Section!");
                    return;
                }

                string testOrderID = FIXConnections.Instance.curFixConn.sendOrder(IdAccount, _curIdTicker, -curQuantity, curPrice, curDisplayShares, IdPortfolio, IdBook, IdSection, new QuickFix.DeliverToCompID(cmbBroker.SelectedValue.ToString()), GlobalVars.Instance.GetSessionType(IdAccount.ToString()));
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (CheckFIX())
            {
                FIXConnections.Instance.curFixConn.cancelOrder(_curIdOrder);
                ToOrderMode();
            }
        }

        private void cmdReplace_Click(object sender, EventArgs e)
        {
            if (CheckFIX())
            {
                int curQuantity = int.Parse(txtQuantity.Text.Replace(".", ""));
                double curPrice = double.Parse(txtPrice.Text);
                int curDisplayShares = int.Parse(txtDisplayShares.Text);

                if (chkMKT.Checked) curPrice = -1;
                // if (curDisplayShares < 1000) curDisplayShares = 1000;

                ReplaceOrder(curPrice, curQuantity, curDisplayShares);
                ToOrderMode();
            }
        }

        private void cmdSwitchSide_Click(object sender, EventArgs e)
        {
            cmdBuy.Visible = !cmdBuy.Visible;
            cmdSell.Visible = !cmdSell.Visible;
        }

        private void ReplaceOrder(double newPrice, double newQuantity, int newDisplayShares)
        {
            NFIXConnLT.OrderLT ReplaceOrder = null;

            foreach (NFIXConnLT.OrderLT curOrder in FIXConnections.Instance.curFixConn.OrderList)
            {
                if (curOrder.OrderID.getValue() == _curIdOrder) { ReplaceOrder = curOrder; break; }
            }

            if (ReplaceOrder != null)
            {
                FIXConnections.Instance.curFixConn.replaceOrder(_curIdOrder, newPrice, newQuantity, newDisplayShares);
            }
        }

        private void dtgAsk_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (DisplayDepthAsk[e.RowIndex].Price <= -999999999.0)
            {
                // txtPrice.Text = DisplayDepthAsk[1].Price.ToString(DefaultPriceStyle);
                txtPrice.Text = "0";
                chkPriceOpen.Checked = true;
            }
            else
            {
                txtPrice.Text = DisplayDepthAsk[e.RowIndex].Price.ToString(DefaultPriceStyle);
            }


            // Totaliza qtde de Asks na pedra
            // ------------------------------
            long lTotal = 0;
            for (int i = 0; i <= e.RowIndex; i++)
            {
                lTotal += DisplayDepthAsk[i].Quantity;
            }

            // txtQuantity.Text = lTotal.ToString("###0");
            txtQuantity.Text = DisplayDepthAsk[e.RowIndex].Quantity.ToString("###0");
            //---------------------------------------------------------------

            if (DisplayDepthAsk[e.RowIndex].Level == -1)
            {
                NFIXConnLT.OrderLT curOrder = null;
                _curIdOrder = DisplayDepthAsk[e.RowIndex].Broker;

                foreach (NFIXConnLT.OrderLT testOrder in FIXConnections.Instance.curFixConn.OrderList)
                {
                    if (testOrder.OrderID.getValue() == _curIdOrder) { curOrder = testOrder; break; }
                }

                txtQuantity.Text = curOrder.dblOrderQty.ToString("###0");
                txtDisplayShares.Text = curOrder.DisplayShares.ToString("###0");
                ToReplaceMode();
            }
            else
            {
                txtQuantity.Text = lTotal.ToString("###0");
                txtDisplayShares.Text = "0"; // update
                ToOrderMode();
                cmdSell.Visible = false;
                cmdBuy.Visible = true;
            }

            txtQuantity.Focus();
        }
        private void dtgBid_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (DisplayDepthBid[e.RowIndex].Price >= 999999999.0)
            {
                //txtPrice.Text = DisplayDepthBid[1].Price.ToString(DefaultPriceStyle);
                txtPrice.Text = "0";
                chkPriceOpen.Checked = true;
            }
            else
            {
                txtPrice.Text = DisplayDepthBid[e.RowIndex].Price.ToString(DefaultPriceStyle);
            }

            // Totaliza qtde de Asks na pedra
            // ------------------------------
            long lTotal = 0;
            for (int i = 0; i <= e.RowIndex; i++)
            {
                lTotal += DisplayDepthBid[i].Quantity;
            }

            // txtQuantity.Text = lTotal.ToString("###0");
            txtQuantity.Text = DisplayDepthBid[e.RowIndex].Quantity.ToString("###0");
            //---------------------------------------------------------------

            if (DisplayDepthBid[e.RowIndex].Level == -1)
            {
                NFIXConnLT.OrderLT curOrder = null;
                _curIdOrder = DisplayDepthBid[e.RowIndex].Broker;

                foreach (NFIXConnLT.OrderLT testOrder in FIXConnections.Instance.curFixConn.OrderList)
                {
                    if (testOrder.OrderID.getValue() == _curIdOrder) { curOrder = testOrder; break; }
                }

                txtDisplayShares.Text = curOrder.DisplayShares.ToString("###0");
                txtQuantity.Text = curOrder.dblOrderQty.ToString("###0");
                ToReplaceMode();
            }
            else
            {
                txtQuantity.Text = lTotal.ToString("###0");
                txtDisplayShares.Text = "0"; // update
                ToOrderMode();
                cmdSell.Visible = true;
                cmdBuy.Visible = false;
            }

            txtQuantity.Focus();
        }

        private void ToReplaceMode()
        {
            if (!ReplaceMode)
            {
                cmdBuy.Visible = false;
                cmdSell.Visible = false;
                cmdSwitchSide.Visible = false;

                cmdCp1.Visible = true;
                cmdCp5.Visible = true;
                cmdCm1.Visible = true;
                cmdCm5.Visible = true;

                cmdCb.Visible = true;
                cmdCa.Visible = true;

                cmdReplace.Visible = true;
                cmdCancel.Visible = true;

                //radFIA.Visible = false;
                //radBroker.Visible = false;
                //radMH.Visible = false;
                //radTOP.Visible = false;
                //radArb.Visible = false;
                //radQuant.Visible = false;
                //radFiaHedge.Visible = false;
                //radTopLine.Visible = false;
                //radPrev.Visible = false;

                cmdDefault1.Visible = false;
                cmdDefault2.Visible = false;
                cmdDefault3.Visible = false;

                cmbPortfolio.Visible = false;
                cmbBroker.Visible = false;
                cmbBook.Visible = false;
                cmbSection.Visible = false;

                txtAccount.Visible = false;

                label11.Visible = false;
                label12.Visible = false;

                ReplaceMode = true;
            }
        }
        private void ToOrderMode()
        {
            if (ReplaceMode)
            {
                cmdBuy.Visible = true;
                cmdSell.Visible = false;
                cmdSwitchSide.Visible = true;

                cmdCp1.Visible = false;
                cmdCp5.Visible = false;
                cmdCm1.Visible = false;
                cmdCm5.Visible = false;

                cmdCb.Visible = false;
                cmdCa.Visible = false;

                cmdReplace.Visible = false;
                cmdCancel.Visible = false;

                ReplaceMode = false;
                _curIdOrder = "";

                //radFIA.Visible = true;
                //radBroker.Visible = true;
                //radMH.Visible = true;
                //radTOP.Visible = true;
                //radArb.Visible = true;
                //radQuant.Visible = true;
                //cmbBroker.Visible = true;
                //radFiaHedge.Visible = true;
                //radTopLine.Visible = true;
                //radPrev.Visible = true;

                cmdDefault1.Visible = true;
                cmdDefault2.Visible = true;
                cmdDefault3.Visible = true;

                cmbPortfolio.Visible = true;
                cmbBroker.Visible = true;
                cmbBook.Visible = true;
                cmbSection.Visible = true;

                label11.Visible = true;
                label12.Visible = true;

                txtAccount.Visible = true;
            }
        }

        private void dtgAsk_MouseDown(object sender, MouseEventArgs e)
        {
            AskMouseDown = true;
        }
        private void dtgAsk_MouseUp(object sender, MouseEventArgs e)
        {
            AskMouseDown = false;
            PendingUpdateDepth = true;
        }

        private void dtgBid_MouseDown(object sender, MouseEventArgs e)
        {
            BidMouseDown = true;
        }
        private void dtgBid_MouseUp(object sender, MouseEventArgs e)
        {
            BidMouseDown = false;
            PendingUpdateDepth = true;
        }

        public void LoadOrders()
        {
            OrdersBid.Clear();
            OrdersAsk.Clear();

            int BidOrderCounter = 0;
            int AskOrderCounter = 0;

            long startms = 0;

            if (FIXConnections.Instance.curFixConn != null)
            {
                startms = FIXConnections.Instance.curFixConn.MasterClock.ElapsedMilliseconds;

                foreach (NFIXConnLT.OrderLT curOrder in FIXConnections.Instance.curFixConn.OrderList)
                {
                    if (curOrder.IdSecurity == _curIdTicker)
                    {
                        if (curOrder.strSide == "BUY")
                        {
                            //Console.WriteLine("ClOrdID : " + curOrder.ClOrdID + " BrokerOrderID : " + curOrder.BrokerOrderID );
                            if (((int)curOrder.Leaves != 0 && curOrder.Status.getValue() != '4') || ((int)curOrder.Leaves != 0 && curOrder.Status.getValue() == 'A') || ((int)curOrder.Leaves != 0 && curOrder.Status.getValue() == '8'))
                            {
                                Level2Bid curBuyOrder = new Level2Bid();
                                curBuyOrder.Broker = curOrder.OrderID.ToString();
                                curBuyOrder.Price = curOrder.Price.getValue();
                                curBuyOrder.Level = -1;
                                curBuyOrder.Quantity = (int)curOrder.Leaves;
                                OrdersBid.Add(curBuyOrder);
                                BidOrderCounter++;
                            }
                        }

                        if (curOrder.strSide == "SELL")
                        {
                            if (((int)curOrder.Leaves != 0 && curOrder.Status.getValue() != '4') || ((int)curOrder.Leaves != 0 && curOrder.Status.getValue() == 'A') || ((int)curOrder.Leaves != 0 && curOrder.Status.getValue() == '8'))
                            // if (((int)curOrder.Leaves != 0 && curOrder.Status.getValue() != '4') || curOrder.Status.getValue() == 'A' || curOrder.Status.getValue() == '8')
                            {
                                Level2Ask curSellOrder = new Level2Ask();
                                curSellOrder.Broker = curOrder.OrderID.ToString();
                                curSellOrder.Price = curOrder.Price.getValue();
                                curSellOrder.Level = -1;
                                curSellOrder.Quantity = (int)curOrder.Leaves;
                                OrdersAsk.Add(curSellOrder);
                                AskOrderCounter++;
                            }
                        }
                    }
                }
                long endms = FIXConnections.Instance.curFixConn.MasterClock.ElapsedMilliseconds - startms;
            }

            PendingUpdateDepth = true;
        }

        private void cmdCp5_Click(object sender, EventArgs e)
        {
            if (CheckFIX())
            {
                NFIXConnLT.OrderLT curOrder = null;

                foreach (NFIXConnLT.OrderLT testOrder in FIXConnections.Instance.curFixConn.OrderList)
                {
                    if (testOrder.OrderID.getValue() == _curIdOrder) { curOrder = testOrder; break; }
                }

                double newValue = 0;
                if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL")
                {
                    newValue = +1;
                }
                else if (_curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
                {
                    newValue = +20;
                }
                else
                {
                    newValue = +0.05;
                }

                ReplaceOrder(curOrder.Price.getValue() + newValue, curOrder.OrderQty.getValue(), -1);
                // ReplaceOrder(curOrder.Price.getValue() + 0.05, curOrder.OrderQty.getValue(), 0);
            }
        }
        private void cmdCp1_Click(object sender, EventArgs e)
        {
            if (CheckFIX())
            {
                NFIXConnLT.OrderLT curOrder = null;

                foreach (NFIXConnLT.OrderLT testOrder in FIXConnections.Instance.curFixConn.OrderList)
                {
                    if (testOrder.OrderID.getValue() == _curIdOrder) { curOrder = testOrder; break; }
                }

                double newValue = 0;
                if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL")
                {
                    newValue = +.5;
                }
                else if (_curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
                {
                    newValue = +5;
                }
                else
                {
                    newValue = +0.01;
                }

                ReplaceOrder(curOrder.Price.getValue() + newValue, curOrder.OrderQty.getValue(), -1);
                //ReplaceOrder(curOrder.Price.getValue() + 0.01, curOrder.OrderQty.getValue(), 0);
            }
        }
        private void cmdCm1_Click(object sender, EventArgs e)
        {
            if (CheckFIX())
            {
                NFIXConnLT.OrderLT curOrder = null;

                foreach (NFIXConnLT.OrderLT testOrder in FIXConnections.Instance.curFixConn.OrderList)
                {
                    if (testOrder.OrderID.getValue() == _curIdOrder) { curOrder = testOrder; break; }
                }

                double newValue = 0;
                if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL")
                {
                    newValue = +.5;
                }
                else if (_curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
                {
                    newValue = +5;
                }
                else
                {
                    newValue = +0.01;
                }

                if (curOrder.Price.getValue() > 0.01)
                {
                    ReplaceOrder(curOrder.Price.getValue() - newValue, curOrder.OrderQty.getValue(), -1);
                }

                //if (curOrder.Price.getValue() > 0.01)
                //{
                //    ReplaceOrder(curOrder.Price.getValue() - 0.01, curOrder.OrderQty.getValue(), 0);
                //}
            }
        }
        private void cmdCm5_Click(object sender, EventArgs e)
        {
            if (CheckFIX())
            {
                NFIXConnLT.OrderLT curOrder = null;

                foreach (NFIXConnLT.OrderLT testOrder in FIXConnections.Instance.curFixConn.OrderList)
                {
                    if (testOrder.OrderID.getValue() == _curIdOrder) { curOrder = testOrder; break; }
                }

                double newValue = 0;
                if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL")
                {
                    newValue = +1;
                }
                else if (_curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
                {
                    newValue = +20;
                }
                else
                {
                    newValue = +0.05;
                }

                if (curOrder.Price.getValue() > 0.05)
                {
                    ReplaceOrder(curOrder.Price.getValue() - newValue, curOrder.OrderQty.getValue(), -1);
                }

                //if (curOrder.Price.getValue() > 0.05)
                //{
                //    ReplaceOrder(curOrder.Price.getValue() - 0.05, curOrder.OrderQty.getValue(), 0);
                //}
            }
        }
        private void cmdCb_Click(object sender, EventArgs e)
        {
            if (CheckFIX())
            {
                NFIXConnLT.OrderLT curOrder = null;

                foreach (NFIXConnLT.OrderLT testOrder in FIXConnections.Instance.curFixConn.OrderList)
                {
                    if (testOrder.OrderID.getValue() == _curIdOrder) { curOrder = testOrder; break; }
                }

                if (DisplayDepthBid.Count > 0) ReplaceOrder(double.Parse(DisplayDepthBid[0].Price.ToString("0.00")), curOrder.OrderQty.getValue(), -1);
            }
        }
        private void cmdCa_Click(object sender, EventArgs e)
        {
            if (CheckFIX())
            {
                NFIXConnLT.OrderLT curOrder = null;

                foreach (NFIXConnLT.OrderLT testOrder in FIXConnections.Instance.curFixConn.OrderList)
                {
                    if (testOrder.OrderID.getValue() == _curIdOrder) { curOrder = testOrder; break; }
                }

                if (DisplayDepthAsk.Count > 0) ReplaceOrder(double.Parse(DisplayDepthAsk[0].Price.ToString("0.00")), curOrder.OrderQty.getValue(), -1);
            }
        }

        private void chkConsol_CheckedChanged(object sender, EventArgs e)
        {
            SubscribeDepth(_curTicker, DepthStateAGG);
        }

        private void DepthDisplay_CheckedChanged(object sender, EventArgs e)
        {
            if ((((RadioButton)sender).Checked))
            {
                if (radAGG.Checked)
                {
                    DepthStateAGG = true;
                    dtgBid.Visible = true;
                    dtgAsk.Visible = true;
                    panOtherInfo.Visible = false;
                }
                else if (radFULL.Checked)
                {
                    DepthStateAGG = false;
                    dtgBid.Visible = true;
                    dtgAsk.Visible = true;
                    panOtherInfo.Visible = false;
                }
                else if (radOTHER.Checked)
                {
                    DepthStateAGG = true;
                    dtgBid.Visible = false;
                    dtgAsk.Visible = false;
                    panOtherInfo.Visible = true;
                }
                else if (radNCustom.Checked)
                {
                    DepthStateAGG = false;
                    dtgBid.Visible = true;
                    dtgAsk.Visible = true;
                    panOtherInfo.Visible = false;
                }

                PendingUpdateDepth = true;

                SubscribeDepth(_curTicker, DepthStateAGG);
            }
        }

        private void SubscribeDepth(string _curTicker, bool _Consol)
        {
            curLevel2Grid.DepthBid.Clear();
            curLevel2Grid.DepthAsk.Clear();

            if (_Consol)
            {
                curNDistConn.SubscribeDepthAgg(_curTicker, curSource);
            }
            else
            {
                curNDistConn.SubscribeDepth(_curTicker, curSource);
            }
        }

        private void UpdateAccountNumber()
        {
            if (cmbBroker.SelectedIndex == -1 || cmbPortfolio.SelectedIndex == -1)
                return;

            SetAccountNumber(cmbPortfolio.SelectedValue.ToString(), cmbBroker.SelectedValue.ToString());

            //if (radFIA.Checked) SetAccountNumber("FIA", cmbBroker.SelectedValue.ToString());
            //if (radMH.Checked) SetAccountNumber("MH", cmbBroker.SelectedValue.ToString());
            //if (radBroker.Checked) SetAccountNumber("BROKER", cmbBroker.SelectedValue.ToString());
            //if (radArb.Checked) SetAccountNumber("ARB", cmbBroker.SelectedValue.ToString());
            //if (radQuant.Checked) SetAccountNumber("QUANT", cmbBroker.SelectedValue.ToString());
            //if (radTOP.Checked) SetAccountNumber("TOP", cmbBroker.SelectedValue.ToString());
            //if (radTopLine.Checked) SetAccountNumber("TOPLINE", cmbBroker.SelectedValue.ToString());
            //if (radPrev.Checked) SetAccountNumber("PREV", cmbBroker.SelectedValue.ToString());
        }

        private void UpdateDefaults()
        {
            this.LoadDefaults();
            this.LoadDefaultBookSection(txtTicker.Text.ToUpper());

            // if (radBroker.Checked) { cmbBook.SelectedValue = 2; cmbSection.SelectedValue = 2; }
            //if (radBroker.Checked) { cmbBook.SelectedValue = 2; cmbSection.SelectedValue = 2; }
            //if (radFIA.Checked) { cmbBook.SelectedValue = 2; cmbSection.SelectedValue = 2; }
            //if (radMH.Checked) { cmbBook.SelectedValue = 2; cmbSection.SelectedValue = 2; } 
            //if (radTOP.Checked) { cmbBook.SelectedValue = 2; cmbSection.SelectedValue = 2; }
            //if (radTopLine.Checked) { cmbBook.SelectedValue = 2; cmbSection.SelectedValue = 2; }
            //if (radArb.Checked) { cmbBook.SelectedValue = 6; cmbSection.SelectedValue = 1; }
            //if (radQuant.Checked) { cmbBook.SelectedValue = 18; cmbSection.SelectedValue = 164; }
            //if (radPrev.Checked) { cmbBook.SelectedValue = 2; cmbSection.SelectedValue = 2; }

            //if (radFIA.Checked && NUserControl.Instance.User_Id == 12)
            //{
            //    cmbBook.SelectedValue = 4; cmbSection.SelectedValue = 2;
            //}
        }

        private void SetAccountNumber(string Portfolio, string Broker)
        {
            txtAccount.Text = GlobalVars.Instance.GetAccountNumber(Portfolio, Broker);
        }

        private void frmLevel2_MouseEnter(object sender, EventArgs e)
        {
            //if (this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
            //{
            //    this.Height = 700;
            //}
        }

        private void frmLevel2_MouseLeave(object sender, EventArgs e)
        {
            //if (!this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
            //{
            //    this.Height = 400;
            //}
        }

        private void chkMKT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMKT.Checked)
            {
                txtPrice.Enabled = false;
            }
            else
            {
                txtPrice.Enabled = true;
            }
        }

        private void txtPrice_MouseWheel(object sender, EventArgs e)
        {
            MouseEventArgs CurArgs = (MouseEventArgs)e;

            if (CurArgs.Location.X > 0 && CurArgs.Location.X < txtPrice.Width && CurArgs.Location.Y > 0 && CurArgs.Location.Y < txtPrice.Height)
            {
                double newValue = 0;
                if (_curTicker.Substring(0, 3) == "DOL" || _curTicker.Substring(0, 3) == "WIN")
                {
                    newValue = double.Parse(txtPrice.Text) + 0.5 * CurArgs.Delta / 120;
                }
                else if (_curTicker.Substring(0, 3) == "IND")
                {
                    newValue = double.Parse(txtPrice.Text) + 5 * CurArgs.Delta / 120;
                }
                else if (_curTicker.Substring(0, 3) == "ICF")
                {
                    newValue = double.Parse(txtPrice.Text) + .05 * CurArgs.Delta / 120;
                }
                else
                {
                    newValue = double.Parse(txtPrice.Text) + 0.01 * CurArgs.Delta / 120;
                }

                if (newValue < 0) newValue = 0;
                txtPrice.Text = newValue.ToString();
            }
        }

        private void txtQuantity_MouseWheel(object sender, EventArgs e)
        {
            MouseEventArgs CurArgs = (MouseEventArgs)e;

            if (CurArgs.Location.X > 0 && CurArgs.Location.X < txtQuantity.Width && CurArgs.Location.Y > 0 && CurArgs.Location.Y < txtQuantity.Height)
            {
                int newValue = 0;

                if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL" || _curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
                {
                    newValue = int.Parse(txtQuantity.Text.Replace(".", "")) + 5 * CurArgs.Delta / 120;
                }
                else
                {
                    newValue = int.Parse(txtQuantity.Text.Replace(".", "")) + 100 * CurArgs.Delta / 120;
                }

                if (newValue < 0) newValue = 0;

                txtQuantity.Text = newValue.ToString();
            }
        }

        private void txtQuantity_MouseEnter(object sender, EventArgs e)
        {
            //txtQuantity.Focus();
        }

        private void txtPrice_MouseEnter(object sender, EventArgs e)
        {
            //txtPrice.Focus();
        }

        private void chkDisplayAuction_CheckedChanged(object sender, EventArgs e)
        {
            ToggleDisplayAuction();
        }

        private void ToggleDisplayAuction()
        {
            if (chkDisplayAuction.Checked == true)
            {
                DisplayModeAuction = true;
            }
            else
            {
                DisplayModeAuction = false;
            }
            PendingUpdateDepth = true;
        }

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAccountNumber();
        }

        private void cmbBroker_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateAccountNumber();
        }

        private void Portfolios_CheckedChanged(object sender, EventArgs e)
        {
            UpdateAccountNumber();
            UpdateDefaults();
        }

        public void LoadDefaultInstruments(int curIdInstrument)
        {
            string sFile = @"C:\LiveTrade\LTConfigDefaultInstruments.csv";

            if (!File.Exists(sFile))
                return;

            StreamReader sr = new StreamReader(sFile);
            string tempLine = "";

            while ((tempLine = sr.ReadLine()) != null)
            {
                string[] curLine = tempLine.Split(';');
                if (curLine[1] == curIdInstrument.ToString())
                {
                    cmbBroker.SelectedItem = curLine[0];
                    break;
                }
            }
        }

        public void LoadDefaultSecurity(string curIdSecurity)
        {
            string sReturn = "";

            StreamReader sr = new StreamReader(@"C:\LiveTrade\LTConfigDefaultSecurity.csv");
            string tempLine = "";

            while ((tempLine = sr.ReadLine()) != null)
            {
                string[] curLine = tempLine.Split(';');
                if (curLine[1] == curIdSecurity)
                {
                    sReturn = curLine[0];
                    break;
                }
            }

            cmbBroker.SelectedItem = sReturn;
        }

        public void LoadDefaultBookSection(string curIdSecurity)
        {
            string sFile = @"C:\LiveTrade\LTConfigDefaultBookSection.csv";

            if (!File.Exists(sFile))
                return;

            StreamReader sr = new StreamReader(sFile);
            string tempLine = "";

            while ((tempLine = sr.ReadLine()) != null)
            {
                string[] curLine = tempLine.Split(';');
                if (curLine[0] == curIdSecurity.ToString())
                {
                    cmbBook.SelectedValue = curLine[1];
                    cmbSection.SelectedValue = curLine[2];
                    break;
                }
            }
        }

        public void LoadDefaults()
        {
            string sFile = @"C:\LiveTrade\LTDefault.txt";

            if (!File.Exists(sFile))
                return;

            StreamReader sr = new StreamReader(sFile);

            string tempLine = "";
            while ((tempLine = sr.ReadLine()) != null)
            {
                if (tempLine.Contains("[Botao1]")) cmdDefault1.Text = tempLine.Split('=')[1];
                if (tempLine.Contains("[Botao2]")) cmdDefault2.Text = tempLine.Split('=')[1];
                if (tempLine.Contains("[Botao3]")) cmdDefault3.Text = tempLine.Split('=')[1];

                if (tempLine.Contains("[Fundo1]")) cmbPortfolio.SelectedItem = tempLine.Split('=')[1];
                if (tempLine.Contains("[Broker1]")) cmbBroker.SelectedItem = tempLine.Split('=')[1];
                if (tempLine.Contains("[Book1]")) cmbBook.SelectedValue = tempLine.Split('=')[1];
                if (tempLine.Contains("[Section1]")) cmbSection.SelectedValue = tempLine.Split('=')[1];
            }

            sr.Close();

        }

        public void LoadDefaultUpdatePrice()
        {
            string sCmdCp5 = "";
            if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL")
            {
                sCmdCp5 = "+1";
            }
            else if (_curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
            {
                sCmdCp5 = "+20";
            }
            else
            {
                sCmdCp5 = "+5c";
            }
            cmdCp5.Text = sCmdCp5;

            //----------------------------------------------------------------------------------------------------------------------

            string sCmdCp1 = "";
            if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL")
            {
                sCmdCp1 = "+.5";
            }
            else if (_curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
            {
                sCmdCp1 = "+5";
            }
            else
            {
                sCmdCp1 = "+1c";
            }
            cmdCp1.Text = sCmdCp1;

            //----------------------------------------------------------------------------------------------------------------------

            string sCmdCm5 = "";
            if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL")
            {
                sCmdCm5 = "-1";
            }
            else if (_curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
            {
                sCmdCm5 = "-20";
            }
            else
            {
                sCmdCm5 = "-5c";
            }
            cmdCm5.Text = sCmdCm5;

            //----------------------------------------------------------------------------------------------------------------------

            string sCmdCm1 = "";
            if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL")
            {
                sCmdCm1 = "-.5";
            }
            else if (_curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
            {
                sCmdCm1 = "-5";
            }
            else
            {
                sCmdCm1 = "-1c";
            }
            cmdCm1.Text = sCmdCm1;
        }

        private void cmdDefault1_Click(object sender, EventArgs e)
        {
            string sFile = @"C:\LiveTrade\LTDefault.txt";

            if (!File.Exists(sFile))
                return;

            StreamReader sr = new StreamReader(sFile);

            string tempLine = "";
            while ((tempLine = sr.ReadLine()) != null)
            {
                if (tempLine.Contains("[Botao1]")) cmdDefault1.Text = tempLine.Split('=')[1];
                if (tempLine.Contains("[Fundo1]")) cmbPortfolio.SelectedItem = tempLine.Split('=')[1];
                if (tempLine.Contains("[Broker1]")) cmbBroker.SelectedItem = tempLine.Split('=')[1];
                if (tempLine.Contains("[Book1]")) cmbBook.SelectedValue = tempLine.Split('=')[1];
                if (tempLine.Contains("[Section1]")) cmbSection.SelectedValue = tempLine.Split('=')[1];
            }

            sr.Close();
        }

        private void cmdDefault2_Click(object sender, EventArgs e)
        {
            string sFile = @"C:\LiveTrade\LTDefault.txt";

            if (!File.Exists(sFile))
                return;

            StreamReader sr = new StreamReader(sFile);

            string tempLine = "";
            while ((tempLine = sr.ReadLine()) != null)
            {
                if (tempLine.Contains("[Botao2]")) cmdDefault2.Text = tempLine.Split('=')[1];
                if (tempLine.Contains("[Fundo2]")) cmbPortfolio.SelectedItem = tempLine.Split('=')[1];
                if (tempLine.Contains("[Broker2]")) cmbBroker.SelectedItem = tempLine.Split('=')[1];
                if (tempLine.Contains("[Book2]")) cmbBook.SelectedValue = tempLine.Split('=')[1];
                if (tempLine.Contains("[Section2]")) cmbSection.SelectedValue = tempLine.Split('=')[1];
            }

            sr.Close();
        }

        private void cmdDefault3_Click(object sender, EventArgs e)
        {
            string sFile = @"C:\LiveTrade\LTDefault.txt";

            if (!File.Exists(sFile))
                return;

            StreamReader sr = new StreamReader(sFile);

            string tempLine = "";
            while ((tempLine = sr.ReadLine()) != null)
            {
                if (tempLine.Contains("[Botao3]")) cmdDefault3.Text = tempLine.Split('=')[1];
                if (tempLine.Contains("[Fundo3]")) cmbPortfolio.SelectedItem = tempLine.Split('=')[1];
                if (tempLine.Contains("[Broker3]")) cmbBroker.SelectedItem = tempLine.Split('=')[1];
                if (tempLine.Contains("[Book3]")) cmbBook.SelectedValue = tempLine.Split('=')[1];
                if (tempLine.Contains("[Section3]")) cmbSection.SelectedValue = tempLine.Split('=')[1];
            }

            sr.Close();
        }

        private void SetaBroker()
        {
            if (_curIdInstrument == 2) { cmbBroker.SelectedItem = "XP"; }
            if (_curIdInstrument == 2) { cmbBroker.SelectedItem = "XPBOVE"; }
            //if (_curIdInstrument == 2) { cmbBroker.SelectedItem = "XPBOV"; }
            if (_curIdInstrument == 2) { cmbBroker.SelectedItem = "LNKBOVH"; }
            if (_curIdInstrument == 2) { cmbBroker.SelectedItem = "LNKBOV3"; }

            if (_curIdInstrument == 4 || _curIdInstrument == 16) { cmbBroker.SelectedItem = "XPBMF"; }
            if (_curIdInstrument == 4 || _curIdInstrument == 16) { cmbBroker.SelectedItem = "XPBMFH"; }
            if (_curIdInstrument == 4 || _curIdInstrument == 16) { cmbBroker.SelectedItem = "LNKBMFH"; }
            if (_curIdInstrument == 4 || _curIdInstrument == 16) { cmbBroker.SelectedItem = "LNKBMFH3"; }
        }

        private void cmbBook_SelectedIndexChanged(object sender, EventArgs e)
        {
            // GlobalVars.Instance.LoadSection( Convert.ToInt32( cmbBook.SelectedValue));
        }

        #region Updates de Preco e Qtde
        private void cmdPriceUP_Click(object sender, EventArgs e)
        {
            double newValue = 0;
            if (_curTicker.Substring(0, 3) == "DOL" || _curTicker.Substring(0, 3) == "WIN")
            {
                newValue = double.Parse(txtPrice.Text) + 0.5;
            }
            else if (_curTicker.Substring(0, 3) == "IND")
            {
                newValue = double.Parse(txtPrice.Text) + 5;
            }
            else if (_curTicker.Substring(0, 3) == "ICF")
            {
                newValue = double.Parse(txtPrice.Text) + .05;
            }
            else
            {
                newValue = double.Parse(txtPrice.Text) + 0.01;
            }

            if (newValue < 0) newValue = 0;
            txtPrice.Text = newValue.ToString();

        }

        private void cmdPriceDown_Click(object sender, EventArgs e)
        {
            double newValue = 0;
            if (_curTicker.Substring(0, 3) == "DOL" || _curTicker.Substring(0, 3) == "WIN")
            {
                newValue = double.Parse(txtPrice.Text) - 0.5;
            }
            else if (_curTicker.Substring(0, 3) == "IND")
            {
                newValue = double.Parse(txtPrice.Text) - 5;
            }
            else if (_curTicker.Substring(0, 3) == "ICF")
            {
                newValue = double.Parse(txtPrice.Text) - .05;
            }
            else
            {
                newValue = double.Parse(txtPrice.Text) - 0.01;
            }

            if (newValue < 0) newValue = 0;
            txtPrice.Text = newValue.ToString();

        }

        private void cmdQtdeUp_Click(object sender, EventArgs e)
        {
            int newValue = 0;

            if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL" || _curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
            {
                newValue = int.Parse(txtQuantity.Text.Replace(".", "")) + 5;
            }
            else
            {
                newValue = int.Parse(txtQuantity.Text.Replace(".", "")) + 100;
            }

            if (newValue < 0) newValue = 0;

            txtQuantity.Text = newValue.ToString();

        }

        private void cmdQtdeDown_Click(object sender, EventArgs e)
        {
            int newValue = 0;

            if (_curTicker.Substring(0, 3) == "DI1" || _curTicker.Substring(0, 3) == "DOL" || _curTicker.Substring(0, 3) == "IND" || _curTicker.Substring(0, 3) == "WIN")
            {
                newValue = int.Parse(txtQuantity.Text.Replace(".", "")) - 5;
            }
            else
            {
                newValue = int.Parse(txtQuantity.Text.Replace(".", "")) - 100;
            }

            if (newValue < 0) newValue = 0;

            txtQuantity.Text = newValue.ToString();
        }
        #endregion

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            if (chkPriceOpen.Checked)
                chkPriceOpen.Checked = false;
        }

        private void lnkOption_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (labTicker.Text != "")
            {
                frmOptionChain newOptionChain = new frmOptionChain(labTicker.Text);
                ConnectedForms.Add(newOptionChain);
                newOptionChain.ClientClosedForm += new EventHandler(ClientFormClosed);
                newOptionChain.Show();
            }
        }

        private void ClientFormClosed(object sender, EventArgs e)
        {
            ConnectedForms.Remove((ConnectedForm)sender);
        }

        private void lnkReloadDepth_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (labTicker.Text != "")
            {
                MessageBox.Show("Depth reset requested!");
                curNDistConn.ReloadDepth(labTicker.Text, curSource);
            }
        }
    }
}