using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCommonTypes;

namespace ProxyDiffDataProcessor
{
    class SubscriptionList
    {
        private List<MarketDataItem> SubscribedData = new List<MarketDataItem>();
        private SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();

        public int Count { get { return SubscribedData.Count; } }

        public int Add(MarketDataItem curItem)
        {
            int itemID = -1;

            if (!SubListIndex.ContainsKey(curItem.Ticker))
            {
                SubscribedData.Add(curItem);
                UpdateSubListIndex();
                itemID = SubListIndex[curItem.Ticker];
            }

            return itemID;
        }

        public void Remove(MarketDataItem curItem)
        {
            string Ticker = curItem.Ticker;
            Remove(Ticker);
        }

        public void Remove(string Ticker)
        {
            int index = SubListIndex[Ticker];
            RemoveAt(index);
        }

        public void RemoveAt(int Index)
        {
            SubscribedData.RemoveAt(Index);
            UpdateSubListIndex();
        }

        private void UpdateSubListIndex()
        {
            SubListIndex.Clear();

            for (int i = 0; i < SubscribedData.Count; i++)
            {
                SubListIndex.Add(SubscribedData[i].Ticker, i);
            }
        }        
    }
}
