using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IT_Status
{
    class QSEGS
    {
        public string QSEGName { get; set; }
        public DateTime Date { get; set; }
        public string PriceType { get; set; }
        public double Price { get; set; }
        public double PriceDm1 { get; set; }
        public string Var { get; set; }

        public QSEGS (string _qsegName, DateTime _date, string _priceType, double _price, double _pricedm1)
        {
            this.QSEGName = _qsegName;
            this.Date = _date;
            this.PriceType = _priceType;
            this.Price = _price;
            this.PriceDm1 = _pricedm1;
            if (_price == 0 || PriceDm1 == 0)
            {
                this.Var = "";
            }
            else
            {
                this.Var = Math.Round(_price / _pricedm1 - 1,8).ToString()+"%";
            }
        }
        
    }
}
