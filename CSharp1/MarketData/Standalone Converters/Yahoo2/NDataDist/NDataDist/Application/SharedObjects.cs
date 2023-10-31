using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCommonTypes;

namespace NDataDist
{
    class SubscriptionItem
    {
        public string Symbol = "";
        public List<int> Subscribers = new List<int>();
        public MarketDataItem MarketData = new MarketDataItem();

        public List<int> getSubscribersClone()
        {
            List<int> retList = new List<int>();
            lock (Subscribers)
            {
                foreach (int curItem in Subscribers)
                {
                    retList.Add(curItem);
                }
            }
            return retList;
        }
    }

    class SubscriptionItemDepth
    {
        public string Symbol = "";
        public List<int> Subscribers = new List<int>();
        public Level2Grid Level2Grid = new Level2Grid();

        public List<int> getSubscribersClone()
        {
            List<int> retList = new List<int>();
            lock (Subscribers)
            {
                foreach (int curItem in Subscribers)
                {
                    retList.Add(curItem);
                }
            }
            return retList;
        }
    }

    class SubscriptionItemDepthAGG
    {
        public string Symbol = "";
        public List<int> Subscribers = new List<int>();
        public Level2Grid Level2Grid = new Level2Grid();

        public List<int> getSubscribersClone()
        {
            List<int> retList = new List<int>();
            lock (Subscribers)
            {
                foreach (int curItem in Subscribers)
                {
                    retList.Add(curItem);
                }
            }
            return retList;
        }
    }

    //class MarketUpdateItemWithSource : EventArgs
    //{
    //    public MarketUpdateItem MarketUpdateItem;
    //    public List<int> Subscribers = new List<int>();
    //}

    //class DepthUpdateItemWithSource : EventArgs
    //{
    //    public Level2Item DepthUpdateItem;
    //    public List<int> Subscribers = new List<int>();
    //}
}
