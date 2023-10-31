using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NestDLL;
using NCalculatorDLL;

namespace LBRTCalc
{
    public class RealTimeCalc
    {
        private int _LinesToCalc; public int LinesToCalc { get { return _LinesToCalc; } }
        private int _LinesCalculated; public int LinesCalculated { get { return _LinesCalculated; } }
        private int _IsRt = 1;
        private DateTime _PositionDate; public DateTime PositionDate { get { return _PositionDate; } }
        private int _IdPortfolio; public int IdPortfolio { get { return _IdPortfolio; } }
        private string _PortFilter; public string PortFilter { get { return _PortFilter; } }
        public bool DeleteCalculator = false;

        public DateTime LastBaseDateCalc = new DateTime(1900, 01, 01);

        private DateTime _StartTime; public DateTime StartTime { get { return _StartTime; } }

        public LBCalculator curCalc;

        public RealTimeCalc(int IdPortfolio)
        {
            _PositionDate = DateTime.Now.Date;
            _IdPortfolio = IdPortfolio;
        }

        public void Calculate()
        {
            _StartTime = DateTime.Now;

            _PortFilter = IdPortfolio.ToString();

            curCalc = new LBCalculator(_PositionDate, _IdPortfolio, _PortFilter, _IsRt);
            curCalc.Calculate();
        }

        public void DeleteDBPositions()
        {
            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery("DELETE FROM NESTRT.dbo.Tb000_Posicao_Atual WHERE [Id Portfolio] = " + _IdPortfolio + ";");
            }
        }

        public LBCalculator getRecalc(DataTable newTransactionTable, DataTable newTradesTable)
        {
            LBCalculator newRTCalc = new LBCalculator(curCalc.PositionDate, curCalc.IdPortfolio, curCalc.PortFilter, _IsRt);
            newRTCalc.Calculate(curCalc.curPositionTable, newTransactionTable, newTradesTable);
            return newRTCalc;
        }
    }
}
