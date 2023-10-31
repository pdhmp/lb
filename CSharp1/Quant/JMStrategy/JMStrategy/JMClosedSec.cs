using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMStrategy
{
    class JMClosedSec: EventArgs
    {
        private bool _Blocked;

        public bool Blocked
        {
            get { return _Blocked; }
            set { _Blocked = value; }
        }

        private string _ClosedSec;

        public string ClosedSec
        {
            get { return _ClosedSec; }
            set { _ClosedSec = value; }
        }

        private string _PairSec;

        public string PairSec
        {
            get { return _PairSec; }
            set { _PairSec = value; }
        }

        private int _ClosedQty;

        public int ClosedQty
        {
            get { return _ClosedQty; }
            set { _ClosedQty = value; }
        }

        private int _PairQty;

        public int PairQty
        {
            get { return _PairQty; }
            set { _PairQty = value; }
        }

        private int _IdModel;

        public int IdModel
        {
            get { return _IdModel; }
            set { _IdModel = value; }
        }

        private int _IdPair;

        public int IdPair
        {
            get { return _IdPair; }
            set { _IdPair = value; }
        }


        public JMPairs refPair;
        
    }
}
