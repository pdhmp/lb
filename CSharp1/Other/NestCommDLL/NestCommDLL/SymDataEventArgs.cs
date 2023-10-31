using System;
using System.Collections.Generic;
using System.Text;

namespace NestCommDLL
{
    class SymDataEventArgs : EventArgs
    {
        private string _Ticker;
        private string[] _FLID;
        private double[] _Value;
        private string[] _Text;
        private string[] _Type;
        private bool[] _IsSet;

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
        public string[] Text
        {
            get { return _Text; }
            set { _Text = value; }
        }
        public string[] Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public bool[] IsSet
        {
            get { return _IsSet; }
            set { _IsSet = value; }
        }



        private SymDataEventArgs() { }

        public SymDataEventArgs(string Ticker, 
                                string[] FLID, 
                                double[] Value, 
                                string[] Text, 
                                string[] Type, 
                                bool[] IsSet) 
        {
            _Ticker = Ticker;
            _FLID = FLID;
            _Value = Value;
            _Text = Text;
            _Type = Type;
            _IsSet = IsSet;
        }


    }
}
