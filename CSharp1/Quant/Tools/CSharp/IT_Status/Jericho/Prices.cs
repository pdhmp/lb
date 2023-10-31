using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IT_Status
{
    class Prices
    {        
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public double Value { get; set; }
        public string TipoPreco { get; set; }

        public Prices (string _symbol, DateTime _date, double _value, string _tipoPreco)
        {
            this.Symbol = _symbol;
            this.Date = _date;
            this.Value = _value;
            this.TipoPreco = _tipoPreco;
        }
    }
}
