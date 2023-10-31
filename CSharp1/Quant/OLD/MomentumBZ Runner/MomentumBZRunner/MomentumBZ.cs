using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MomentumBZRunner
{
    public class MomentumBZ 
    {
        private MomentumInfo Info = null;
        private SortedDictionary<int, MomentumSignal> SectorSignalContainer;
        private SortedDictionary<int, double> TargetPercPosition;
        private SortedDictionary<int, int> CurrentPosition;

        private Mutex PositionsMutex;

        public event EventHandler<OrderEventArgs> SendOrdersEvent;

        public MomentumBZ(MomentumInfo _Info)
        {
            Info = _Info;

            SectorSignalContainer = new SortedDictionary<int, MomentumSignal>();
            TargetPercPosition = new SortedDictionary<int, double>();
            PositionsMutex = new Mutex();
            CurrentPosition = new SortedDictionary<int, int>();
        }

        public void StartStrategy()
        {
            if (Info == null) { throw new System.ArgumentNullException("MomentumInfo Info"); };

            foreach (KeyValuePair<int,MomentumSectorInfo> sector in Info.MomentumSectorsInfo)
            {
                int ID_Sector = sector.Key;
                MomentumSignal sectorSignal = new MomentumSignal(sector.Value);                
                sectorSignal.GenerateSignal();
                SectorSignalContainer.Add(ID_Sector, sectorSignal);

                GeneratePositions(ID_Sector);
            }

            if (SendOrdersEvent != null)
            {
                SendOrders();
            }
        }

        private void GeneratePositions(int ID_Sector)
        {
            double sectorWeight = Info.Weights[ID_Sector];
            double sectorPosition = SectorSignalContainer[ID_Sector].Signal * sectorWeight * Info.AdjustFactor;

            foreach (KeyValuePair<int, double> tickerWeight in Info.MomentumSectorsInfo[ID_Sector].Weights)
            {
                double tickerPosition = tickerWeight.Value * sectorPosition;

                if (TargetPercPosition.ContainsKey(tickerWeight.Key))
                {
                    TargetPercPosition[tickerWeight.Key] = tickerPosition;
                }                    
                else
                {
                    TargetPercPosition.Add(tickerWeight.Key, tickerPosition);
                }
            }
        }    

        private void SendOrders()
        {
            OrderEventArgs orders = new OrderEventArgs();
            bool newOrderFlag = false;

            foreach (KeyValuePair<int, double> tickerPosition in TargetPercPosition)
            {
                if (tickerPosition.Value != 0||(tickerPosition.Value == 0 &&CurrentPosition.ContainsKey(tickerPosition.Key)))
                {

                    double targetCashQty = tickerPosition.Value * Info.StrategyNAV;

                    int tickerSector = Info.TickersBySector[tickerPosition.Key];
                    double price = SectorSignalContainer[tickerSector].DayPrices[tickerPosition.Key];

                    int targetQty = Convert.ToInt32(targetCashQty / price);

                    int currentQty = 0;

                    if (CurrentPosition.ContainsKey(tickerPosition.Key))
                    {
                        currentQty = CurrentPosition[tickerPosition.Key];
                    }

                    int difference = targetQty - currentQty;

                    if (difference != 0)
                    {
                        orders.AddOrder(tickerPosition.Key, difference, -1);
                        newOrderFlag = true;
                    }
                }
            }

            if (newOrderFlag)
            {
                SendOrdersEvent(this, orders);
            }
        }

        public void UpdatePrices(Dictionary<int, double> Prices)
        {
            PositionsMutex.WaitOne();

            SortedDictionary<int, Dictionary<int, double>> SectorPrices = new SortedDictionary<int, Dictionary<int, double>>();

            foreach (KeyValuePair<int, double> tickerPrice in Prices)
            {
                int sector = Info.TickersBySector[tickerPrice.Key];

                if (!SectorPrices.ContainsKey(sector))
                {
                    Dictionary<int, double> Ticker = new Dictionary<int, double>();
                    SectorPrices.Add(sector, Ticker);
                }

                SectorPrices[sector].Add(tickerPrice.Key, tickerPrice.Value);
            }

            foreach (KeyValuePair<int, Dictionary<int, double>> sector in SectorPrices)
            {
                SectorSignalContainer[sector.Key].UpdatePrices(sector.Value);
                SectorSignalContainer[sector.Key].GenerateSignal();
                               
                GeneratePositions(sector.Key);                              
            }           
            
            PositionsMutex.ReleaseMutex();

            if (SendOrdersEvent != null)
            {
                SendOrders();
            }
        }

        public void UpdatePositions(SortedDictionary<int, int> positions)
        {
            PositionsMutex.WaitOne();

            foreach (KeyValuePair<int, int> tickerPosition in positions)
            {
                if (CurrentPosition.ContainsKey(tickerPosition.Key))
                {
                    CurrentPosition[tickerPosition.Key] = tickerPosition.Value;
                }
                else
                {
                    CurrentPosition.Add(tickerPosition.Key, tickerPosition.Value);
                }
            }

            PositionsMutex.ReleaseMutex();
        }

        public void CheckOrders()
        {
            if (SendOrdersEvent != null)
            {
                SendOrders();
            }
        }
    }
}
