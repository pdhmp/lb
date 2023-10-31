using System;
using System.Collections.Generic;
using System.Text;

namespace FIX_BMF
{
    public class MarketDepthItem
    {
        private string _Ticker = ""; public string Ticker { get { return _Ticker; } set { _Ticker = value; } }

        private double _Bid1 = 0; public double Bid1 { get { return _Bid1; } set { _Bid1 = value; } }
        private double _Bid2 = 0; public double Bid2 { get { return _Bid2; } set { _Bid2 = value; } }
        private double _Bid3 = 0; public double Bid3 { get { return _Bid3; } set { _Bid3 = value; } }
        private double _Bid4 = 0; public double Bid4 { get { return _Bid4; } set { _Bid4 = value; } }
        private double _Bid5 = 0; public double Bid5 { get { return _Bid5; } set { _Bid5 = value; } }

        private double _Ask1 = 0; public double Ask1 { get { return _Ask1; } set { _Ask1 = value; } }
        private double _Ask2 = 0; public double Ask2 { get { return _Ask2; } set { _Ask2 = value; } }
        private double _Ask3 = 0; public double Ask3 { get { return _Ask3; } set { _Ask3 = value; } }
        private double _Ask4 = 0; public double Ask4 { get { return _Ask4; } set { _Ask4 = value; } }
        private double _Ask5 = 0; public double Ask5 { get { return _Ask5; } set { _Ask5 = value; } }

        private double _BidSize1 = 0; public double BidSize1 { get { return _BidSize1; } set { _BidSize1 = value; } }
        private double _BidSize2 = 0; public double BidSize2 { get { return _BidSize2; } set { _BidSize2 = value; } }
        private double _BidSize3 = 0; public double BidSize3 { get { return _BidSize3; } set { _BidSize3 = value; } }
        private double _BidSize4 = 0; public double BidSize4 { get { return _BidSize4; } set { _BidSize4 = value; } }
        private double _BidSize5 = 0; public double BidSize5 { get { return _BidSize5; } set { _BidSize5 = value; } }

        private double _AskSize1 = 0; public double AskSize1 { get { return _AskSize1; } set { _AskSize1 = value; } }
        private double _AskSize2 = 0; public double AskSize2 { get { return _AskSize2; } set { _AskSize2 = value; } }
        private double _AskSize3 = 0; public double AskSize3 { get { return _AskSize3; } set { _AskSize3 = value; } }
        private double _AskSize4 = 0; public double AskSize4 { get { return _AskSize4; } set { _AskSize4 = value; } }
        private double _AskSize5 = 0; public double AskSize5 { get { return _AskSize5; } set { _AskSize5 = value; } }

        public void Bid_Update(int BidPos, double BidPrice, int BidSize)
        {
            if (BidPos == 5) { _Bid5 = BidPrice; _BidSize5 = BidSize; }
            if (BidPos == 4) { _Bid4 = BidPrice; _BidSize4 = BidSize; }
            if (BidPos == 3) { _Bid3 = BidPrice; _BidSize3 = BidSize; }
            if (BidPos == 2) { _Bid2 = BidPrice; _BidSize2 = BidSize; }
            if (BidPos == 1) { _Bid1 = BidPrice; _BidSize1 = BidSize; }
        }

        public void Bid_New(int BidPos, double BidPrice, int BidSize)
        {
            MoveDownBid(BidPos);

            if (BidPos == 1) { _Bid1 = BidPrice; _BidSize1 = BidSize; }
            if (BidPos == 2) { _Bid2 = BidPrice; _BidSize2 = BidSize; }
            if (BidPos == 3) { _Bid3 = BidPrice; _BidSize3 = BidSize; }
            if (BidPos == 4) { _Bid4 = BidPrice; _BidSize4 = BidSize; }
            if (BidPos == 5) { _Bid5 = BidPrice; _BidSize5 = BidSize; }
        }

        public void Bid_Delete(int BidPos)
        {
            MoveUpBid(BidPos);
        }

        private void MoveDownBid(int BidPos)
        {
            if (BidPos == 0) return;
            if (BidPos <= 4) { _Bid5 = _Bid4; _BidSize5 = _BidSize4; }
            if (BidPos <= 3) { _Bid4 = _Bid3; _BidSize4 = _BidSize3; }
            if (BidPos <= 2) { _Bid3 = _Bid2; _BidSize3 = _BidSize2; }
            if (BidPos <= 1) { _Bid2 = _Bid1; _BidSize2 = _BidSize1; }
        }

        private void MoveUpBid(int BidPos)
        {
            if (BidPos == 0) return;
            if (BidPos <= 1) { _Bid1 = _Bid2; _BidSize1 = _BidSize2; }
            if (BidPos <= 2) { _Bid2 = _Bid3; _BidSize2 = _BidSize3; }
            if (BidPos <= 3) { _Bid3 = _Bid4; _BidSize3 = _BidSize4; }
            if (BidPos <= 4) { _Bid4 = _Bid5; _BidSize4 = _BidSize5; }
            if (BidPos <= 5) { _Bid5 = 0; _BidSize5 = 0; }
        }

        public void Ask_Update(int AskPos, double AskPrice, int AskSize)
        {
            if (AskPos == 0) return;
            if (AskPos == 5) { _Ask5 = AskPrice; _AskSize5 = AskSize; }
            if (AskPos == 4) { _Ask4 = AskPrice; _AskSize4 = AskSize; }
            if (AskPos == 3) { _Ask3 = AskPrice; _AskSize3 = AskSize; }
            if (AskPos == 2) { _Ask2 = AskPrice; _AskSize2 = AskSize; }
            if (AskPos == 1) { _Ask1 = AskPrice; _AskSize1 = AskSize; }
        }

        public void Ask_New(int AskPos, double AskPrice, int AskSize)
        {
            if (AskPos == 0) return;
            
            MoveDownAsk(AskPos);

            if (AskPos == 1) { _Ask1 = AskPrice; _AskSize1 = AskSize; }
            if (AskPos == 2) { _Ask2 = AskPrice; _AskSize2 = AskSize; }
            if (AskPos == 3) { _Ask3 = AskPrice; _AskSize3 = AskSize; }
            if (AskPos == 4) { _Ask4 = AskPrice; _AskSize4 = AskSize; }
            if (AskPos == 5) { _Ask5 = AskPrice; _AskSize5 = AskSize; }
        }

        public void Ask_Delete(int AskPos)
        {
            if (AskPos == 0) return;
            MoveUpAsk(AskPos);
        }

        private void MoveDownAsk(int AskPos)
        {
            if (AskPos == 0) return;
            if (AskPos <= 4) { _Ask5 = _Ask4; _AskSize5 = _AskSize4; }
            if (AskPos <= 3) { _Ask4 = _Ask3; _AskSize4 = _AskSize3; }
            if (AskPos <= 2) { _Ask3 = _Ask2; _AskSize3 = _AskSize2; }
            if (AskPos <= 1) { _Ask2 = _Ask1; _AskSize2 = _AskSize1; }
        }

        private void MoveUpAsk(int AskPos)
        {
            if (AskPos == 0) return;
            if (AskPos <= 1) { _Ask1 = _Ask2; _AskSize1 = _AskSize2; }
            if (AskPos <= 2) { _Ask2 = _Ask3; _AskSize2 = _AskSize3; }
            if (AskPos <= 3) { _Ask3 = _Ask4; _AskSize3 = _AskSize4; }
            if (AskPos <= 4) { _Ask4 = _Ask5; _AskSize4 = _AskSize5; }
            if (AskPos <= 5) { _Ask5 = 0; _AskSize5 = 0; }
        }
    }
}
