using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NestDLL;

namespace LBHistCalc
{
    class RTCalc
    {
        private int _LinesToCalc; public int LinesToCalc { get { return _LinesToCalc; } }
        private int _LinesCalculated; public int LinesCalculated { get { return _LinesCalculated; } }

        private DateTime _PositionDate; public DateTime PositionDate { get { return _PositionDate; } }
        private int _IdPortfolio; public int IdPortfolio { get { return _IdPortfolio; } }
        private string _PortFilter; public string PortFilter { get { return _PortFilter; } }

        private DateTime _StartTime; public DateTime StartTime { get { return _StartTime; } }

        public LBCalculator curCalc;

        public RTCalc(int IdPortfolio)
        {
            _PositionDate = DateTime.Now.Date;
            _IdPortfolio = IdPortfolio;
        }

        public void Calculate()
        {
            _StartTime = DateTime.Now;

            switch (IdPortfolio)
            {
                case 4:
                    _PortFilter = "4,5,6";
                    break;
                case 10:
                    _PortFilter = "10";
                    break;
                case 18:
                    _PortFilter = "18,17";
                    break;
                case 38:
                    _PortFilter = "38,39";
                    break;
                case 43:
                    _PortFilter = "43,42,41";
                    break;
                case 50:
                    _PortFilter = "50,51";
                    break;
                case 60:
                    _PortFilter = "60,61";
                    break;
                default:
                    _PortFilter = "";
                    break;
            }

            curCalc = new LBCalculator(_PositionDate, _IdPortfolio, _PortFilter);
            curCalc.Calculate();

            Console.WriteLine(_IdPortfolio + "\t" + _PositionDate + "\t" + DateTime.Now.Subtract(_StartTime).TotalSeconds + "\tCalculated");
        }

        public LBCalculator getRecalc(DataTable newTransactionTable, DataTable newTradesTable)
        {
            LBCalculator newRTCalc = new LBCalculator(curCalc.PositionDate, curCalc.IdPortfolio, curCalc.PortFilter);
            newRTCalc.Calculate(curCalc.curPositionTable, newTransactionTable, newTradesTable);

            return newRTCalc;
        }
    }
}
