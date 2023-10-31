using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NestDLL;
using NFastData;

namespace OppOp
{
    class Ticker
    {
        public int IdSecurity;
        DateTime RunDate;

        public double Low, High, Last, Open, VWAP, Vol, Var;

        private double CurIndex, RefIndex, RefLast;

        public Ticker(int _IdSecurity, DateTime _RunDate)
        {
            if (_IdSecurity == 82936 && _RunDate > new DateTime(2011, 05, 01))
            { }

            IdSecurity = _IdSecurity;
            RunDate = _RunDate;

            if (RunDate == new DateTime(2008,2,21) && IdSecurity == 1816)
            { }

            RefIndex = OppOpData.Instance.FD_TR_Index.GetValue(IdSecurity, OppOpData.Instance.FD_TR_Index.PrevDate(DateTime.Today), true)[1];
            RefLast = OppOpData.Instance.FD_Last.GetValue(IdSecurity, OppOpData.Instance.FD_TR_Index.PrevDate(DateTime.Today), true)[1];
            CurIndex = OppOpData.Instance.FD_TR_Index.GetValue(IdSecurity, RunDate, true)[1];

            Last = RefLast * CurIndex / RefIndex;

            Low = OppOpData.Instance.FD_Low.GetValue(IdSecurity, RunDate, true)[1] * Last / OppOpData.Instance.FD_Last.GetValue(IdSecurity, RunDate, true)[1];
            High = OppOpData.Instance.FD_High.GetValue(IdSecurity, RunDate, true)[1] * Last / OppOpData.Instance.FD_Last.GetValue(IdSecurity, RunDate, true)[1];
            Open = OppOpData.Instance.FD_Open.GetValue(IdSecurity, RunDate, true)[1] * Last / OppOpData.Instance.FD_Last.GetValue(IdSecurity, RunDate, true)[1];
            VWAP = OppOpData.Instance.FD_VWAP.GetValue(IdSecurity, RunDate, true)[1] * Last / OppOpData.Instance.FD_Last.GetValue(IdSecurity, RunDate, true)[1];
            

            Calculate();
        }

        public void Calculate()
        {
            /*
            List<double> OpenList = new List<double>();
            List<double> CloseList = new List<double>();
            List<double> HighList = new List<double>();
            List<double> LowList = new List<double>();
             */
            try
            {
                List<double> VarList = new List<double>();

                DateTime AuxDate = RunDate;
                int i = 0;

                CurIndex = OppOpData.Instance.FD_TR_Index.GetValue(IdSecurity, OppOpData.Instance.FD_TR_Index.PrevDate(AuxDate), true)[1];

                Var = Math.Log(Last / (RefLast * CurIndex / RefIndex));

                AuxDate = OppOpData.Instance.FD_Last.PrevDate(AuxDate);

                while (i < 21)
                {
                    if (i == 20)
                    { }
                    double PrevIndex = CurIndex;
                    CurIndex = OppOpData.Instance.FD_TR_Index.GetValue(IdSecurity, OppOpData.Instance.FD_TR_Index.PrevDate(AuxDate), true)[1];
                    VarList.Add(Math.Log((RefLast * PrevIndex / RefIndex) / (RefLast * CurIndex / RefIndex)));
                    AuxDate = OppOpData.Instance.FD_Last.PrevDate(AuxDate);
                    i++;
                }

                Vol = getSTDEV(VarList);
            }
            catch (Exception E)
            { }

            /*
            while (i < 22)
            {
                curIndex = FD_TR_Index.GetValue(IdSecurity, FD_Open.PrevDate(AuxDate), true)[1];

                CloseList.Add(Last * curIndex / refIndex);

                OpenList.Add(CloseList[i] * FD_Open.GetValue(IdSecurity, FD_Open.PrevDate(AuxDate), true)[1] / FD_Last.GetValue(IdSecurity, FD_Last.PrevDate(AuxDate), true)[1]);
                HighList.Add(CloseList[i] * FD_High.GetValue(IdSecurity, FD_High.PrevDate(AuxDate), true)[1] / FD_Last.GetValue(IdSecurity, FD_Last.PrevDate(AuxDate), true)[1]);
                LowList.Add(CloseList[i] * FD_Low.GetValue(IdSecurity, FD_Low.PrevDate(AuxDate), true)[1] / FD_Last.GetValue(IdSecurity, FD_Last.PrevDate(AuxDate), true)[1]);
                AuxDate = FD_Last.PrevDate(AuxDate);
                i++;
            }            

            Vol = VolGKYZ(OpenList.GetRange(0,22),CloseList.GetRange(0,22),HighList.GetRange(0,22),LowList.GetRange(0,22));
             */


        }

        private double VolGKYZ(List<double> _OpenList, List<double> _CloseList, List<double> _HighList, List<double> _LowList)
        {
            double sig2 = 0;
            
            try
            {               
                int count = _OpenList.Count;

                for (int i = 0; i < count - 1; i++)
                {
                    sig2 += Math.Pow(Math.Log(_OpenList[i] / _CloseList[i]), 2) + 0.5 * (Math.Pow(Math.Log(_HighList[i] / _LowList[i]), 2) - (2 * Math.Log(2) - 1) * (Math.Pow(Math.Log(_CloseList[i] / _OpenList[i]), 2)));
                }

                sig2 = Math.Pow(sig2 / (count - 1), 0.5);
            }
            catch (Exception E)
            { }

            return sig2;
        }

        private double getSTDEV(List<double> VarList)
        {
            double result = 0;
            try
            {
                double _x = 0;

                foreach (double x in VarList)
                {
                    _x += x;
                }

                _x /= VarList.Count;

                double aux = 0;

                foreach (double x in VarList)
                {
                    aux += Math.Pow((x - _x), 2);
                }

                result = Math.Sqrt(aux / (VarList.Count - 1));               
            }
            catch (Exception E)
            { }

            return result;
           
        }
    }
}
