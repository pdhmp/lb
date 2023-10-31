using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMStrategy
{
    public class JMOrder
    {
        public int IdSection;
        public int IdSecurity;


        private string _Section;
        public string Section
        {
            get { return _Section; }
            set { _Section = value; }
        }

        private string _Ticker;
        public string Ticker
        {
            get { return _Ticker; }
            set { _Ticker = value; }
        }

        private int _Quantity;
        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public List<JMPairs> refPairs = new List<JMPairs>();

    }
}
