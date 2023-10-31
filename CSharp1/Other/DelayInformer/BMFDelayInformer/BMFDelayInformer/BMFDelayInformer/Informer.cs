using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NestDLL;
using NestSymConn;
using NCommonTypes;

namespace BMFDelayInformer
{
    class Informer
    {
        List<string> SubscribedTickers = new List<string>();
        public TimeSpan Hora = new TimeSpan();
        public event EventHandler OnDelayed;
        bool FirstTime = true;

        public Informer()
        {
            SubscribedTickers.Add("DOLV11");
            SubscribedTickers.Add("INDV11");            
            InitializeSym();
        }

        private void InitializeSym()
        {
            SymConn.Instance.BovespaSource = NEnuns.NSYMSources.FLEXBSE;
            SymConn.Instance.OnData += new EventHandler(ReceiveMarketData);
        }

        public void SubscribeTickers(object obj)
        {
            foreach (string ticker in SubscribedTickers)
            {
                SymConn.Instance.Subscribe(ticker, 1);
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
                            (DateTime.Now.TimeOfDay > new TimeSpan(08, 59, 59)) &&
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
    }
}
