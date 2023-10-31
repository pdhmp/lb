using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using NestSymConn;
using NCommonTypes;

namespace NestRTD
{

    [
    Guid("5A12E67C-7B19-4AA0-81C5-34D29EFFD710"),
    ProgId("Nest.Rtd"),
    ComVisible(true)]
        
    public class RTDClass : IRtdServer
    {
        private IRTDUpdateEvent m_callback;
        NDistConn curNDistConn = new NDistConn();
        private Timer m_timer;

        Dictionary<int, RequestedItem> RequestedItems = new Dictionary<int,RequestedItem> ();

        List<MarketDataItem> SubscribedData = new List<MarketDataItem>();
        SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();

        DateTime LastUpdate = new DateTime(1900, 01, 01);

        class RequestedItem
        {
            public string Ticker = "";
            public string FLID = "";
        }

        public int ServerStart(IRTDUpdateEvent callback)
        {
            Console.WriteLine("1");

            curNDistConn.OnConnect += new EventHandler(ResubscribeTickers);

            m_callback = callback;
            m_timer = new Timer();
            m_timer.Tick += new EventHandler(TimerEventHandler); 
            m_timer.Interval = 500;
            m_timer.Start();

            curNDistConn.Connect();
            curNDistConn.OnData += new EventHandler(NewMarketData);
            return 1;
        }

        private void ResubscribeTickers(object sender, EventArgs args)
        {
            curNDistConn.ReSubscribe();
        }

        private void TimerEventHandler(object sender, EventArgs args)
        {
            m_callback.UpdateNotify();
        }

        public void ServerTerminate()
        {
            if (null != m_timer)
            {
                m_timer.Stop();
                m_timer.Dispose();
                m_timer = null;
            }

            curNDistConn.Disconnect();
        }
        // C:\Temp\NestRTD\
        // C:\Program Files (x86)\Microsoft Office\Office12
        public object ConnectData(int topicId, ref Array strings, ref bool newValues)
        {
            int Source = 24;
            string Field = "";

            string Ticker = strings.GetValue(0).ToString();
            if (strings.Length > 1) { Field = strings.GetValue(1).ToString(); }
            if (strings.Length > 2) { Source = int.Parse(strings.GetValue(2).ToString()); }

            if (Ticker.Contains("DI1") || Ticker.Contains("DOL") || Ticker.Contains("IND") || Ticker.Contains("WIN") )
                Source = 13;

            if (!SubListIndex.ContainsKey(Ticker))
            {
                MarketDataItem curMarketDataItem = new MarketDataItem();
                curMarketDataItem.Ticker = Ticker;
                SubscribedData.Add(curMarketDataItem);

                curNDistConn.Subscribe(Ticker, (Sources)Source);

                UpdateListIndex();
            }

            RequestedItem curItem = new RequestedItem();
            curItem.Ticker = Ticker;
            curItem.FLID = Field;

            RequestedItems.Add(topicId, curItem);

            UpdateListIndex();

            return GetTime(topicId);
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
            RequestedItems.Remove(topicId);
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

        int retryCounter = 0;

        DateTime LastResubTime = new DateTime(1900, 01, 01);
        int resubCounter = 0;

        private object GetTime(int topicId)
        {
            if (!curNDistConn.IsConnected() && topicId == 1)
            {
                if (retryCounter > 2)
                {
                    curNDistConn.Connect();
                    retryCounter = 0;
                }
                else
                {
                    retryCounter++;
                }
            }

            int i = 0;

            if (RequestedItems[topicId].Ticker == "STATUS")
            {
                return curNDistConn.IsConnected();
            }

            if (RequestedItems[topicId].Ticker == "LASTUPD")
            {
                return LastUpdate.ToString("HH:mm:ss");
            }

            if (DateTime.Now.Subtract(LastUpdate).TotalSeconds > 10)
            {
                if (DateTime.Now.Subtract(LastResubTime).TotalMinutes > 2) resubCounter = 0;

                if (resubCounter < 3)
                {
                    if (DateTime.Now.Hour < 19 && DateTime.Now.Hour > 8 && curNDistConn.IsConnected() && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                    {
                        curNDistConn.ReSubscribe();
                        LastResubTime = DateTime.Now;
                        resubCounter++;
                    }
                }
            }

            if (SubListIndex.TryGetValue(RequestedItems[topicId].Ticker, out i))
            {
                if (RequestedItems[topicId].FLID != "")
                {
                    NestFLIDS curFLID;
                    if (NestFLIDS.TryParse(RequestedItems[topicId].FLID, out curFLID))
                    {
                        MarketDataItem curItem = (MarketDataItem)SubscribedData[i];
                        return curItem.GetValue(curFLID);
                    }
                    else
                    {
                        return "NA Field";
                    }
                }
                else
                {
                    return "NA Field";
                }
            }
            else
            {
                return "NA";
            }
        }

        private void NewMarketData(object sender, EventArgs origE)
        {
            int i = 0;
            MarketUpdateList curUpdateList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
            {
                LastUpdate = DateTime.Now;

                if (SubListIndex.TryGetValue(curUpdateItem.Ticker, out i))
                {
                    MarketDataItem curItem = (MarketDataItem)SubscribedData[i];
                    curItem.Update(curUpdateItem);
                }
            }
        }
    }
}
