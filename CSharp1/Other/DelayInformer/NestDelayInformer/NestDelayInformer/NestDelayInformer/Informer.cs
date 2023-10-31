using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NCommonTypes;
using NestSymConn;
using NestDLL;

namespace NestDelayInformer
{
    class Informer
    {
        List<string> SubscribedTickers = new List<string>();
        public TimeSpan Hora = new TimeSpan();
        public event EventHandler OnDelayed;
        bool FirstTime = true;

        public Informer()
        {
            SubscribedTickers.Add("PETR4");
            SubscribedTickers.Add("VALE5");
            SubscribedTickers.Add("IBOV");           
            InitializeSym();
        }

        private void InitializeSym()
        {
            SymConn.Instance.BovespaSource = NEnuns.NSYMSources.NESTBSE;
            SymConn.Instance.OnData += new EventHandler(ReceiveMarketData);
        }

        public void SubscribeTickers(object obj)
        {
            foreach (string ticker in SubscribedTickers)
            {
                SymConn.Instance.Subscribe(ticker, 2);
            }
        }

        private void ReceiveMarketData(object sender, EventArgs e)
        {
            MarketUpdateList curList = (MarketUpdateList)e;

            foreach (MarketUpdateItem curItem in curList.ItemsList)
            {
                if (SubscribedTickers.Contains(curItem.Ticker))
                {
                    if (curItem.FLID == NestFLIDS.UpdateTime)
                    {
                        DateTime HoraAux = NestDLL.Utils.UnixTimestampToDateTime((int)curItem.ValueDouble);
                        Hora = HoraAux.TimeOfDay;
                        if ((DateTime.Now.TimeOfDay - Hora) > new TimeSpan(0, 0, 10) && 
                            (DateTime.Now.TimeOfDay > new TimeSpan(09,59,59)) &&
                            (DateTime.Now.TimeOfDay < new TimeSpan(18, 00, 01)))
                        {
                            if (!FirstTime)
                            {
                                if (OnDelayed != null)
                                {
                                    OnDelayed(this, null);
                                }
                            }
                            else { FirstTime = false; }
                        }
                    }
                }
            }
        }

        public void Close()
        {
            SymConn.Instance.Dispose();
        }
    }
}
