using NCommonTypes;

namespace FeedLBSym
{
    internal class LBMarketDataItem: MarketDataItem
    {
        public Sources Source { get; set; }
        public string SubscribeTicker { get; set; }
        public bool Subscribed { get; set; }
        public bool LastUpdated { get; set; }
        public bool BidUpdated { get; set; }
        public bool AskUpdated { get; set; }
        public bool SettUpdated { get; set; }
        public bool CloseUpdated { get; set; }

    }
}
