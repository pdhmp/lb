using System;
using System.Collections.Generic;
using System.Text;

namespace MomentumBZRunner
{
    class SymDataEventArgs : EventArgs 
    {
        private string _Ticker;
        private string[] _FLID;
        private double[] _Value;

        public string Ticker
        {
            get { return _Ticker; }
            set { _Ticker = value; }
        }
        public string[] FLID
        {
            get { return _FLID; }
            set { _FLID = value; }
        }
        public double[] Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private SymDataEventArgs() { }

        public SymDataEventArgs(string Ticker, string[] FLID, double[] Value) 
        {
            _Ticker = Ticker;
            _FLID = FLID;
            _Value = Value;
        }


    }
}
