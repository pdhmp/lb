using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SecurityListGenerator
{
    public class BovDataListGenerator
    {
        SortedDictionary<string, SortedDictionary<DateTime, TickerLine>> curData = new SortedDictionary<string, SortedDictionary<DateTime, TickerLine>>();

    }

    public class TickerLine
    {
        public string Ticker;
        public double Last;
        public double NumShares;
        public double FinanValue;
        public double quotationLot;
    }
}
