using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NestSymConn;
using NCommonTypes;

namespace NDataDist
{
    //class ConnManager
    //{
    //    public string ConnType() { return _curConnector.ConnType(); }

    //    INestConnector _curConnector;
    //    public DateTime LastUpdTime { get { return _curConnector.LastUpdTime(); } }
    //    public bool IsConnected { get { return _curConnector.IsConnected(); } }

    //    public event EventHandler onMarketData;
    //    public event EventHandler onDepthData;
    //    public event EventHandler onDepthAGGData;

    //    public event EventHandler OnConnect;
    //    public event EventHandler OnDisconnect;

    //    Dictionary<string, SubscriptionItem> SubscriptionControl = new Dictionary<string, SubscriptionItem>();
    //    Dictionary<string, SubscriptionItemDepth> SubscriptionControlDepth = new Dictionary<string, SubscriptionItemDepth>();
    //    Dictionary<string, SubscriptionItemDepthAGG> SubscriptionControlDepthAGG = new Dictionary<string, SubscriptionItemDepthAGG>();


    //    public ConnManager(INestConnector curConnector)
    //    {
    //        _curConnector = curConnector;

    //        curConnector.OnConnect += new EventHandler(ConnConnected);
    //        curConnector.OnDisconnect += new EventHandler(ConnDisconnected);
    //        curConnector.OnData += new EventHandler(NewMarketData);
    //        curConnector.OnDepth += new EventHandler(NewDepthData);
    //        curConnector.OnDepthAgg += new EventHandler(NewDepthAGGData);
    //        curConnector.SetDebugLevel(5);

    //        Connect();

    //        string _BovespaSource = NEnuns.NSYMSources.FLEXBSE;
    //    }

    //    public void Connect()
    //    {
    //        _curConnector.Connect();
    //        ReSubscribeAll();
    //    }

    //    public void Disconnect()
    //    {
    //        _curConnector.Disconnect();
    //        //SubscriptionControl.Clear();
    //        //SubscriptionControlDepth.Clear();
    //        //SubscriptionControlDepthAGG.Clear();
    //    }

    //    private void ConnConnected(object sender, EventArgs e)
    //    {
    //        if (OnConnect != null) OnConnect(this, new EventArgs());
    //        ReSubscribeAll();
    //    }

    //    private void ReSubscribeAll()
    //    {
    //        foreach (SubscriptionItem curSubscriptionItem in SubscriptionControl.Values)
    //        {
    //            _curConnector.Subscribe(curSubscriptionItem.Symbol, Sources.None);
    //        }
    //        foreach (SubscriptionItemDepth curSubscriptionItem in SubscriptionControlDepth.Values)
    //        {
    //            _curConnector.SubscribeDepth(curSubscriptionItem.Symbol, Sources.None);
    //        }
    //        foreach (SubscriptionItemDepthAGG curSubscriptionItem in SubscriptionControlDepthAGG.Values)
    //        {
    //            _curConnector.SubscribeDepthAgg(curSubscriptionItem.Symbol, Sources.None);
    //        }
    //    }

    //    private void ConnDisconnected(object sender, EventArgs e)
    //    {
    //        if (OnDisconnect != null) OnDisconnect(this, new EventArgs());
    //    }

    //    public void Subscribe(string Symbol, int Subscriber)
    //    {
    //        GlobalVars.Instance.LogEntry("CONN: SUB: " + Symbol + "\t" + Subscriber, 1);

    //        SubscriptionItem curSubscriptionItem;

    //        if (SubscriptionControl.TryGetValue(Symbol, out curSubscriptionItem))
    //        {

    //            // ===============  PREPARE AND SEND SNAPSHOT =============================
    //            MarketUpdateList curUpdateList = curSubscriptionItem.MarketData.GetSnapshot();

    //            List<int> tempSubscriberList = new List<int>();
    //            tempSubscriberList.Add(Subscriber);

    //            foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
    //            {
    //                MarketUpdateItemWithSource curMarketUpdateItemWithSource = new MarketUpdateItemWithSource();
    //                curMarketUpdateItemWithSource.MarketUpdateItem = curUpdateItem;
    //                curMarketUpdateItemWithSource.Subscribers = tempSubscriberList;

    //                onMarketData(this, curMarketUpdateItemWithSource);
    //            }
    //            // =========================================================================

    //            GlobalVars.Instance.LogEntry("Snapshot sent: " + Symbol + "\t" + Subscriber, 1);

    //            if (!curSubscriptionItem.Subscribers.Contains(Subscriber))
    //                curSubscriptionItem.Subscribers.Add(Subscriber);

    //        }
    //        else
    //        {
    //            curSubscriptionItem = new SubscriptionItem();
    //            curSubscriptionItem.Symbol = Symbol;
    //            curSubscriptionItem.MarketData.Ticker = Symbol;
    //            curSubscriptionItem.Subscribers.Add(Subscriber);
    //            lock (SubscriptionControl)
    //            {
    //                SubscriptionControl.Add(Symbol, curSubscriptionItem);
    //            }
    //            _curConnector.Subscribe(Symbol, Sources.None);

    //            GlobalVars.Instance.LogEntry("New Subscription: " + Symbol + "\t" + Subscriber, 1);
    //        }
    //    }

    //    public void UnSubscribe(string Symbol, int Subscriber)
    //    {
    //        GlobalVars.Instance.LogEntry("CONN: UNSUB: " + Symbol + "\t" + Subscriber, 1);

    //        SubscriptionItem curSubscriptionItem;

    //        if (SubscriptionControl.TryGetValue(Symbol, out curSubscriptionItem))
    //        {
    //            curSubscriptionItem.Subscribers.Remove(Subscriber);
    //            if (curSubscriptionItem.Subscribers.Count == 0)
    //            {
    //                lock (SubscriptionControl)
    //                {
    //                    SubscriptionControl.Remove(Symbol);
    //                }
    //                _curConnector.UnSubscribe(Symbol, Sources.None);
    //                _curConnector.UnSubscribeDepth(Symbol, Sources.None);
    //                _curConnector.UnSubscribeDepthAgg(Symbol, Sources.None);
    //            }
    //        }
    //    }

    //    public void SubscribeDepth(string Symbol, int Subscriber)
    //    {
    //        GlobalVars.Instance.LogEntry("CONN: SUBDEPTH: " + Symbol + "\t" + Subscriber, 1);

    //        SubscriptionItemDepth curSubscriptionItem;

    //        if (SubscriptionControlDepth.TryGetValue(Symbol, out curSubscriptionItem))
    //        {
    //            // ===============  PREPARE AND SEND SNAPSHOT =============================
    //            Level2UpdateList curUpdateList = curSubscriptionItem.Level2Grid.GetSnapshot(true);

    //            List<int> tempSubscriberList = new List<int>();
    //            tempSubscriberList.Add(Subscriber);

    //            foreach (Level2Item curUpdateItem in curUpdateList.ItemsList)
    //            {
    //                DepthUpdateItemWithSource curMarketUpdateItemWithSource = new DepthUpdateItemWithSource();
    //                curMarketUpdateItemWithSource.DepthUpdateItem = curUpdateItem;
    //                curMarketUpdateItemWithSource.Subscribers = tempSubscriberList;

    //                onDepthData(this, curMarketUpdateItemWithSource);
    //            }
    //            // =========================================================================

    //            GlobalVars.Instance.LogEntry("DEPTH Snapshot sent: " + Symbol + "\t" + Subscriber, 1);

    //            if (!curSubscriptionItem.Subscribers.Contains(Subscriber))
    //                curSubscriptionItem.Subscribers.Add(Subscriber);
    //        }
    //        else
    //        {
    //            curSubscriptionItem = new SubscriptionItemDepth();
    //            curSubscriptionItem.Symbol = Symbol;
    //            curSubscriptionItem.Level2Grid.Ticker = Symbol;
    //            curSubscriptionItem.Subscribers.Add(Subscriber);
    //            lock (SubscriptionControlDepth)
    //            {
    //                SubscriptionControlDepth.Add(Symbol, curSubscriptionItem);
    //            }
    //            _curConnector.SubscribeDepth(Symbol, Sources.None);

    //            GlobalVars.Instance.LogEntry("New DEPTH Subscription: " + Symbol + "\t" + Subscriber, 1);
    //        }
    //    }

    //    public void UnSubscribeDepth(string Symbol, int Subscriber)
    //    {
    //        GlobalVars.Instance.LogEntry("CONN: UNSUBDEPTH: " + Symbol + "\t" + Subscriber, 1);

    //        SubscriptionItemDepth curSubscriptionItem;

    //        if (SubscriptionControlDepth.TryGetValue(Symbol, out curSubscriptionItem))
    //        {
    //            curSubscriptionItem.Subscribers.Remove(Subscriber);
    //            if (curSubscriptionItem.Subscribers.Count == 0)
    //            {
    //                lock (SubscriptionControlDepth)
    //                {
    //                    SubscriptionControlDepth.Remove(Symbol);
    //                }
    //                _curConnector.UnSubscribeDepth(Symbol, Sources.None);
    //            }
    //        }
    //    }

    //    public void SubscribeDepthAGG(string Symbol, int Subscriber)
    //    {
    //        GlobalVars.Instance.LogEntry("CONN: SUBDEPTHAGG: " + Symbol + "\t" + Subscriber, 1);

    //        SubscriptionItemDepthAGG curSubscriptionItem;

    //        if (SubscriptionControlDepthAGG.TryGetValue(Symbol, out curSubscriptionItem))
    //        {
    //            // ===============  PREPARE AND SEND SNAPSHOT =============================
    //            Level2UpdateList curUpdateList = curSubscriptionItem.Level2Grid.GetSnapshot(true);

    //            List<int> tempSubscriberList = new List<int>();
    //            tempSubscriberList.Add(Subscriber);

    //            foreach (Level2Item curUpdateItem in curUpdateList.ItemsList)
    //            {
    //                DepthUpdateItemWithSource curMarketUpdateItemWithSource = new DepthUpdateItemWithSource();
    //                curMarketUpdateItemWithSource.DepthUpdateItem = curUpdateItem;
    //                curMarketUpdateItemWithSource.Subscribers = tempSubscriberList;

    //                onDepthAGGData(this, curMarketUpdateItemWithSource);
    //            }
    //            // =========================================================================

    //            GlobalVars.Instance.LogEntry("CONN: DepthAGG Snapshot sent: " + Symbol + "\t" + Subscriber, 1);

    //            if (!curSubscriptionItem.Subscribers.Contains(Subscriber))
    //                curSubscriptionItem.Subscribers.Add(Subscriber);

    //        }
    //        else
    //        {
    //            curSubscriptionItem = new SubscriptionItemDepthAGG();
    //            curSubscriptionItem.Symbol = Symbol;
    //            curSubscriptionItem.Level2Grid.Ticker = Symbol;
    //            curSubscriptionItem.Subscribers.Add(Subscriber);
    //            lock (SubscriptionControlDepthAGG)
    //            {
    //                SubscriptionControlDepthAGG.Add(Symbol, curSubscriptionItem);
    //            }
    //            _curConnector.SubscribeDepthAgg(Symbol, Sources.None);

    //            GlobalVars.Instance.LogEntry("CONN: New DepthAGG Subscritpion: " + Symbol + "\t" + Subscriber, 1);
    //        }
    //    }

    //    public void UnSubscribeDepthAGG(string Symbol, int Subscriber)
    //    {
    //        GlobalVars.Instance.LogEntry("CONN: UNSUBDEPTHAGG: " + Symbol + "\t" + Subscriber, 1);

    //        SubscriptionItemDepthAGG curSubscriptionItem;

    //        if (SubscriptionControlDepthAGG.TryGetValue(Symbol, out curSubscriptionItem))
    //        {
    //            curSubscriptionItem.Subscribers.Remove(Subscriber);
    //            if (curSubscriptionItem.Subscribers.Count == 0)
    //            {
    //                lock (SubscriptionControlDepthAGG)
    //                {
    //                    SubscriptionControlDepthAGG.Remove(Symbol);
    //                }
    //                _curConnector.UnSubscribeDepth(Symbol, Sources.None);
    //            }
    //        }
    //    }

    //    public void UnSubscribeAll()
    //    {
    //        List<int> Subscribers = new List<int>();

    //        lock (SubscriptionControl)
    //        {
    //            foreach (SubscriptionItem curSubscriptionItem in SubscriptionControl.Values)
    //            {
    //                foreach (int curSubscriber in curSubscriptionItem.Subscribers)
    //                {
    //                    if (!Subscribers.Contains(curSubscriber))
    //                    {
    //                        Subscribers.Add(curSubscriber);
    //                    }
    //                }
    //            }
    //        }

    //        foreach (int curSubscriber in Subscribers)
    //        {
    //            UnSubscribeAll(curSubscriber);
    //        }
    //    }

    //    public void UnSubscribeAll(int Subscriber)
    //    {
    //        GlobalVars.Instance.LogEntry("CONN: UNSUBALL:\t" + Subscriber, 1);

    //        int counter = 0;
    //        string[] UnsubTickers = new string[SubscriptionControl.Values.Count];

    //        lock (SubscriptionControl)
    //        {
    //            foreach (SubscriptionItem curSubscriptionItem in SubscriptionControl.Values)
    //            {
    //                if (curSubscriptionItem.Subscribers.Contains(Subscriber))
    //                {
    //                    UnsubTickers[counter++] = curSubscriptionItem.Symbol;
    //                }

    //            }
    //        }

    //        for (int i = 0; i < counter; i++)
    //        {
    //            UnSubscribe(UnsubTickers[i], Subscriber);
    //        }
    //    }

    //    private void NewMarketData(object sender, EventArgs e)
    //    {
    //        lock (SubscriptionControl)
    //        {
    //            MarketUpdateList curUpdateList = (MarketUpdateList)e;

    //            if (onMarketData != null)
    //            {
    //                foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
    //                {
    //                    if (SubscriptionControl.ContainsKey(curUpdateItem.Ticker))
    //                    {
    //                        SubscriptionItem curSubscriptionItem = SubscriptionControl[curUpdateItem.Ticker];
    //                        lock (curSubscriptionItem)
    //                        {
    //                            curSubscriptionItem.MarketData.Update(curUpdateItem);
    //                            if (curSubscriptionItem.MarketData.TradingStatus == TradingStatusType.INVALID)
    //                            {
    //                            }
    //                        }
    //                        if (curUpdateItem.FLID != NestFLIDS.UpdateTime)
    //                        {
    //                            MarketUpdateItemWithSource curMarketUpdateItemWithSource = new MarketUpdateItemWithSource();
    //                            curMarketUpdateItemWithSource.MarketUpdateItem = curUpdateItem;
    //                            curMarketUpdateItemWithSource.Subscribers = curSubscriptionItem.getSubscribersClone();
    //                            onMarketData(this, curMarketUpdateItemWithSource);
    //                        }


    //                    }
    //                }
    //            }
    //        }
    //    }

    //    private void NewDepthData(object sender, EventArgs e)
    //    {
    //        Level2Item curUpdateItem;
    //        curUpdateItem = (Level2Item)e;

    //        lock (SubscriptionControlDepth)
    //        {
    //            if (SubscriptionControlDepth.ContainsKey(curUpdateItem.Ticker))
    //            {
    //                SubscriptionItemDepth curSubscriptionItem = SubscriptionControlDepth[curUpdateItem.Ticker];

    //                lock (curSubscriptionItem)
    //                {
    //                    curSubscriptionItem.Level2Grid.Update(curUpdateItem);
    //                }
    //                DepthUpdateItemWithSource curMarketUpdateItemWithSource = new DepthUpdateItemWithSource();
    //                curMarketUpdateItemWithSource.DepthUpdateItem = curUpdateItem;
    //                curMarketUpdateItemWithSource.Subscribers = curSubscriptionItem.getSubscribersClone();

    //                if (onDepthData != null)
    //                {
    //                    onDepthData(this, curMarketUpdateItemWithSource);
    //                }
    //            }
    //        }
    //    }

    //    private void NewDepthAGGData(object sender, EventArgs e)
    //    {
    //        Level2Item curUpdateItem;
    //        curUpdateItem = (Level2Item)e;

    //        lock (SubscriptionControlDepthAGG)
    //        {
    //            if (SubscriptionControlDepthAGG.ContainsKey(curUpdateItem.Ticker))
    //            {
    //                SubscriptionItemDepthAGG curSubscriptionItem = SubscriptionControlDepthAGG[curUpdateItem.Ticker];
    //                lock (curSubscriptionItem)
    //                {
    //                    if (curUpdateItem.Action == 2)
    //                    {

    //                    }

    //                    curSubscriptionItem.Level2Grid.Update(curUpdateItem);

    //                }
    //                DepthUpdateItemWithSource curMarketUpdateItemWithSource = new DepthUpdateItemWithSource();
    //                curMarketUpdateItemWithSource.DepthUpdateItem = curUpdateItem;
    //                curMarketUpdateItemWithSource.Subscribers = curSubscriptionItem.getSubscribersClone();

    //                if (onDepthAGGData != null)
    //                {
    //                    onDepthAGGData(this, curMarketUpdateItemWithSource);
    //                }

    //                //LinkedListNode<Level2Bid> curDISTNode = curSubscriptionItem.Level2Grid.DepthBid.First;
    //                //double prevPrice = 0;
    //                //while (curDISTNode != null)
    //                //{
    //                //    if (prevPrice == curDISTNode.Value.Price)
    //                //    {
    //                //        int a = 0;
    //                //    }
    //                //    prevPrice = curDISTNode.Value.Price;
    //                //    curDISTNode = curDISTNode.Next;
    //                //}

    //                //LinkedListNode<Level2Ask> curDISTNodeA = curSubscriptionItem.Level2Grid.DepthAsk.First;
    //                //prevPrice = 0;
    //                //while (curDISTNodeA != null)
    //                //{
    //                //    if (prevPrice == curDISTNodeA.Value.Price)
    //                //    {
    //                //        int a = 0;
    //                //    }
    //                //    prevPrice = curDISTNodeA.Value.Price;
    //                //    curDISTNodeA = curDISTNodeA.Next;
    //                //}
    //            }
    //        }
    //    }

    //    ~ConnManager()
    //    {
    //        UnSubscribeAll();
    //    }
    //}
}
