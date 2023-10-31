using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IT_Status
{
    class Summary
    {
        public string Type {get;set;}
        public int Quantity{get;set;}

        public Summary(string _type, int _qty)
        {
            this.Type = _type;
            this.Quantity = _qty;
        }
    }
}
