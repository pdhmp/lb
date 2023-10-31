using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NestSymConn;
using NCommonTypes;

namespace RTDNest
{
    [
    Guid("DE26E45D-CD62-4C3B-A9A3-022C2C7CCEFE"),
    ProgId("NestRtd"),
    ]

    [assembly: ComVisible(true)]

    public class RTDClass : IRtdServer
    {
        private IRTDUpdateEvent m_callback;
        //NDistConn curNDistConn = new NDistConn();

        Dictionary<int, RequestedItem> RequestedItems = new Dictionary<int, RequestedItem>();

        List<MarketDataItem> SubscribedData = new List<MarketDataItem>();
        SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();

        class RequestedItem
        {
            public string Ticker = "";
            public string FLID = "";
        }

        public int ServerStart(IRTDUpdateEvent callback)
        {
            m_callback = callback;
            //curNDistConn.Connect();
            //curNDistConn.OnData += new EventHandler(NewMarketData);
            return 1;
        }

        public void ServerTerminate()
        {
            //curNDistConn.Disconnect();
        }

        public object ConnectData(int topicId, ref Array strings, ref bool newValues)
        {
            string Ticker = strings.GetValue(0).ToString();
            string Field = strings.GetValue(1).ToString();

            if (!SubListIndex.ContainsKey(Ticker))
            {
                MarketDataItem curMarketDataItem = new MarketDataItem();
                curMarketDataItem.Ticker = Ticker;
                SubscribedData.Add(curMarketDataItem);

                //curNDistConn.Subscribe(Ticker, Sources.Bovespa);

                UpdateListIndex();
            }

            RequestedItem curItem = new RequestedItem();
            curItem.Ticker = Ticker;
            curItem.FLID = Field;

            RequestedItems.Add(topicId, curItem);

            UpdateListIndex();

            return 1;// GetTime(topicId);
        }

        private void UpdateListIndex()
        {
            SubListIndex.Clear();

            for (int i = 0; i < SubscribedData.Count; i++)
            {
                SubListIndex.Add(SubscribedData[i].Ticker, i);
            }
        }

        public void DisconnectData(int topicId)
        {
            //m_timer.Stop();
        }

        public Array RefreshData(ref int topicCount)
        {
            object[,] data = new object[2, RequestedItems.Count];

            int index = 0;

            foreach (int topicId in RequestedItems.Keys)
            {
                data[0, index] = topicId;
                data[1, index] = GetTime(topicId);

                ++index;
            }

            topicCount = RequestedItems.Count;

            return data;
        }

        public int Heartbeat()
        {
            return 1;
        }

        private object GetTime(int topicId)
        {
            int i = 0;
            if (SubListIndex.TryGetValue(RequestedItems[topicId].Ticker, out i))
            {
                NestFLIDS curFLID = (NestFLIDS)Enum.Parse(typeof(NestFLIDS), RequestedItems[topicId].FLID);
                MarketDataItem curItem = (MarketDataItem)SubscribedData[i];
                return curItem.GetValue(curFLID);
            }
            else
            {
                return RequestedItems[topicId].Ticker;
            }
        }

        private void NewMarketData(object sender, EventArgs origE)
        {
            int i = 0;
            MarketUpdateList curUpdateList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
            {
                if (SubListIndex.TryGetValue(curUpdateItem.Ticker, out i))
                {
                    MarketDataItem curItem = (MarketDataItem)SubscribedData[i];
                    curItem.Update(curUpdateItem);
                }
            }

            m_callback.UpdateNotify();

        }
    }
}
