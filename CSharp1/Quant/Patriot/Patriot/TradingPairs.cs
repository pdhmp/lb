using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Patriot
{
    public class TradingPairs
    {
        private PatriotRunner ParentRunner;

        private double _Security1;
        private double _Security2;

        public double Security1
        {
            get { return _Security1; }
            set { _Security1 = value; }
        }
        public double Security2
        {
            get { return _Security2; }
            set { _Security2 = value; }
        }

        private double _PL1;
        private double _GL1;
        private double _PValue1;
        private int _Side1;
        private int _InvalidCounter;
        private int _AuxValue;
        private double _PL2;
        private double _GL2;
        private double _PValue2;
        private int _Side2;
        private double _PL3;
        private double _GL3;
        private double _PValue3;
        private int _Side3;
        private bool _Trade;

        public double PL1
        {
            get { return _PL1; }
            set { _PL1 = value; }
        }
        public double GL1
        {
            get { return _GL1; }
            set { _GL1 = value; }
        }
        public double PValue1
        {
            get { return _PValue1; }
            set { _PValue1 = value; }
        }
        public int Side1
        {
            get { return _Side1; }
            set { _Side1 = value; }
        }
        public int InvalidCounter
        {
            get { return _InvalidCounter; }
            set { _InvalidCounter = value; }
        }
        public int AuxValue
        {
            get { return _AuxValue; }
            set { _AuxValue = value; }
        }
        public double PL2
        {
            get { return _PL2; }
            set { _PL2 = value; }
        }
        public double GL2
        {
            get { return _GL2; }
            set { _GL2 = value; }
        }
        public double PValue2
        {
            get { return _PValue2; }
            set { _PValue2 = value; }
        }
        public int Side2
        {
            get { return _Side2; }
            set { _Side2 = value; }
        }
        public double PL3
        {
            get { return _PL3; }
            set { _PL3 = value; }
        }
        public double GL3
        {
            get { return _GL3; }
            set { _GL3 = value; }
        }
        public double PValue3
        {
            get { return _PValue3; }
            set { _PValue3 = value; }
        }
        public int Side3
        {
            get { return _Side3; }
            set { _Side3 = value; }
        }
        public bool Trade
        {
            get { return _Trade; }
            set { _Trade = value; }
        }



        private double _pairPreSize;
        public double PairPreSize
        {
            get { return _pairPreSize; }
            set { _pairPreSize = value; }
        }

        private double _pairFinalSize;
        public double PairFinalSize
        {
            get { return _pairFinalSize; }
            set { _pairFinalSize = value; }
        }


        private double _Sec1MaxSize;
        public double Sec1MaxSize
        {
            get { return _Sec1MaxSize; }
            set { _Sec1MaxSize = value; }
        }

        private double _Sec2MaxSize;
        public double Sec2MaxSize
        {
            get { return _Sec2MaxSize; }
            set { _Sec2MaxSize = value; }
        }



        public TradingPairs(PatriotRunner curRunner)
        {
            ParentRunner = curRunner;
        }

        public void CalculatePairSize()
        {
            if (_Side1 == 1)
            {
                double auxsec = _Security1;
                _Security1 = _Security2;
                _Security2 = auxsec;
            }

            if (_Security1 == 1075 || _Security2 == 1075)
            {
                int a = 0;
            }


            DateTime refDate = ParentRunner.refDate;
            DateTime curDate = ParentRunner.curDate;

            double sec1Last30avg = PatriotData.Instance.GetValue(_Security1, 332, refDate, refDate, true)[1];
            double sec2Last30avg = PatriotData.Instance.GetValue(_Security2, 332, refDate, refDate, true)[1];

            double sec1CloseAvg = PatriotData.Instance.GetValue(_Security1, 313, refDate, refDate, true)[1];
            double sec2CloseAvg = PatriotData.Instance.GetValue(_Security2, 313, refDate, refDate, true)[1];

            double sec1First330avg = PatriotData.Instance.GetValue(_Security1, 352, refDate, refDate, true)[1];
            double sec2First330avg = PatriotData.Instance.GetValue(_Security1, 352, refDate, refDate, true)[1];

            double sec1First330Vwap = PatriotData.Instance.GetValue(_Security1, 351, curDate, curDate, true)[1];
            double sec2First330Vwap = PatriotData.Instance.GetValue(_Security2, 351, curDate, curDate, true)[1];

            double sec1First330Volume = PatriotData.Instance.GetValue(_Security1, 350, curDate, curDate, true)[1];
            double sec2First330Volume = PatriotData.Instance.GetValue(_Security1, 350, curDate, curDate, true)[1];

            double sec1CurFirst330 = sec1First330Vwap * sec1First330Volume;
            double sec2CurFirst330 = sec2First330Vwap * sec2First330Volume;

            //double sec1CurAdj = Math.Min(1.0, sec1CurFirst330 / sec1First330avg);
            //double sec2CurAdj = Math.Min(1.0, sec2CurFirst330 / sec2First330avg);

            double sec1CurAdj = 1.0;
            double sec2CurAdj = 1.0;

            _Sec1MaxSize = Math.Min(sec1Last30avg * ParentRunner.Last30Perc, sec1CloseAvg * ParentRunner.CloseAucPerf);
            _Sec2MaxSize = Math.Min(sec2Last30avg * ParentRunner.Last30Perc, sec2CloseAvg * ParentRunner.CloseAucPerf);

            

            double sec1Last30Size = Math.Min(ParentRunner.SizeLimit, sec1Last30avg * sec1CurAdj * ParentRunner.Last30Perc);
            double sec2Last30Size = Math.Min(ParentRunner.SizeLimit, sec2Last30avg * sec2CurAdj * ParentRunner.Last30Perc);

            double sec1CloseSize = Math.Min(ParentRunner.SizeLimit, sec1CloseAvg * sec1CurAdj * ParentRunner.CloseAucPerf);
            double sec2CloseSize = Math.Min(ParentRunner.SizeLimit, sec2CloseAvg * sec2CurAdj * ParentRunner.CloseAucPerf);

            double sec1Size = Math.Min(sec1Last30Size, sec1CloseSize);
            double sec2Size = Math.Min(sec2Last30Size, sec2CloseSize);

            _pairPreSize = Math.Min(sec1Size, sec2Size) * ParentRunner.SizeMultiplier;

            if (_pairPreSize == 0) { int a = 0; }
            
        }

    }
}
