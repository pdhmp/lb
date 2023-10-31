using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Data;

namespace NestQuant.Common
{
    public class RTPrice
    {
        private int LastSubscriberID = 0;
        
        private SortedDictionary<int, Base_Table> Subscribers = new SortedDictionary<int, Base_Table>();

        private SortedDictionary<int, List<int>> SubscribersByTicker = new SortedDictionary<int, List<int>>();

        private SortedDictionary<int, int[]> TickersBySubscriber = new SortedDictionary<int, int[]>();

        private SortedDictionary<int, double> TickersPrice = new SortedDictionary<int, double>();

        private Timer priceFeeder;


        
        public RTPrice()
        {
            priceFeeder = new Timer(3000);

            priceFeeder.Elapsed += new ElapsedEventHandler(RunExternalFeeder);

            priceFeeder.Start();

        }

        private void priceChange(int ticker, double price)
        {
            List<int> tickerSubsbribers;
            if (SubscribersByTicker.TryGetValue(ticker, out tickerSubsbribers))
            {
                TickersPrice[ticker] = price;

                foreach (int subscriberID in tickerSubsbribers)
                {
                    NotifySubscriber(subscriberID);                    
                }
            }
        }

        private void NotifySubscriber(int SubscriberID)
        {
            Base_Table subscriber;
            if (Subscribers.TryGetValue(SubscriberID, out subscriber))
            {
                if (TickersBySubscriber.ContainsKey(SubscriberID))
                {
                    if (TickersBySubscriber[SubscriberID] != null)
                    {
                        double[] prices = new double[TickersBySubscriber[SubscriberID].Length];

                        for (int i = 0; i < TickersBySubscriber[SubscriberID].Length; i++)
                        {
                            prices[i] = TickersPrice[TickersBySubscriber[SubscriberID][i]];
                        }

                        subscriber.Refresh(TickersBySubscriber[SubscriberID], prices, DateTime.Now);
                    }
                }
                else
                {
                    throw new System.NotImplementedException();
                }
            }
            else
            {
                throw new System.NotImplementedException();
            }

        }

        public void Subscribe(Base_Table table, int[] tickers)
        {
            int newSubscriberID = LastSubscriberID;
            if (!TickersBySubscriber.ContainsKey(table.RTPricesSubscriptionID))
            {
                table.RTPricesSubscriptionID = newSubscriberID;
                Subscribers.Add(newSubscriberID, table);
                TickersBySubscriber.Add(newSubscriberID, tickers);                

                for (int i = 0; i < tickers.Length; i++)
                {
                    if (!SubscribersByTicker.ContainsKey(tickers[i]))
                    {
                        AddTicker(tickers[i]);
                    }

                    SubscribersByTicker[tickers[i]].Add(newSubscriberID);
                }

                LastSubscriberID++;
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        public void RemoveSubscription(Base_Table table)
        {
            if (TickersBySubscriber.ContainsKey(table.RTPricesSubscriptionID))
            {
                int[] tickers = (int[])TickersBySubscriber[table.RTPricesSubscriptionID].Clone();

                TickersBySubscriber[table.RTPricesSubscriptionID] = null;

                int subscriberId = -1;

                foreach (KeyValuePair<int, Base_Table> subscriber in Subscribers)
                {
                    if (subscriber.Value == table)
                    {
                        subscriberId = subscriber.Key;
                    }
                }

                for (int i = 0; i < tickers.Length; i++)
                {
                    SubscribersByTicker[tickers[i]].Remove(subscriberId);
                }

                Subscribers.Remove(subscriberId);

                TickersBySubscriber.Remove(table.RTPricesSubscriptionID);

            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        private void AddTicker(int ticker)
        {
            SubscribersByTicker.Add(ticker, new List<int>());

            TickersPrice.Add(ticker, double.NaN);

            SubscribeExternalFeeder(ticker);
        }

 
        #region External Feeder

        private void SubscribeExternalFeeder(int ticker)
        {
            TickersToObtain.Add(ticker);
        }

        private List<int> TickersToObtain = new List<int>();

        private void RunExternalFeeder(Object source, EventArgs e)
        {
            priceFeeder.Stop();

            NestConn curConn = new NestConn();

            string tickers = "1073";

            foreach (int ticker in TickersToObtain)
            {
                tickers = tickers + ", " + ticker;
            }

            string SQLExpression = "SELECT ID_ATIVO, VALOR FROM NESTRT.DBO.TB065_ULTIMO_PRECO WHERE ID_ATIVO IN (" + tickers + ") AND TIPO_PRECO = 1";

            DataTable dt = curConn.ExecuteDataTable(SQLExpression);

            foreach (DataRow curRow in dt.Rows)
            {
                priceChange(Convert.ToInt32(curRow[0]), Convert.ToDouble(curRow[1]));
            }

            priceFeeder.Start();
        }

        #endregion
    }
}