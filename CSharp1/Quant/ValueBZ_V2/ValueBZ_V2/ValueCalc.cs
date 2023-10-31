using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NestDLL;

namespace ValueBZ_V2
{
    public class ValueCalc
    {
        private DateTime _curDate;
        private int _IdTickerComposite;

        public List<DayPE> DayPEList = new List<DayPE>();
        private int _ValidItems;
        public int ValidItems
        {
            get { return _ValidItems; }
            set { _ValidItems = value; }
        }

        private int _LongItems;
        public int LongItems
        {
            get { return _LongItems; }
            set { _LongItems = value; }
        }

        private int _ShortItems;
        public int ShortItems
        {
            get { return _ShortItems; }
            set { _ShortItems = value; }
        }

        private int _VolDays = 21;
        public int VolDays
        {
            get { return _VolDays; }
            set { _VolDays = value; }
        }

        private int _VolHistDays = 100;
        public int VolHistDays
        {
            get { return _VolHistDays; }
            set { _VolHistDays = value; }
        }                     
        
        public ValueCalc(DateTime __curDate, int __IdTickerComposite)
        {
            _curDate = __curDate;
            _IdTickerComposite = __IdTickerComposite;

            LoadCompositeData();
        }

        private void LoadCompositeData()
        {            
            string SQLString = "SELECT ID_TICKER_COMPONENT, WEIGHT " +
                               "FROM NESTSRV06.NESTDB.DBO.TB023_SECURITIES_COMPOSITION A " +
                               "WHERE ID_TICKER_COMPOSITE = " + _IdTickerComposite + " " +
                               "AND WEIGHT <> 0 " +
                               "AND DATE_REF = (SELECT MAX(DATE_REF) FROM NESTSRV06.NESTDB.DBO.TB023_SECURITIES_COMPOSITION " +
                               "                WHERE ID_TICKER_COMPOSITE = " + _IdTickerComposite + " " +
                               "                AND DATE_REF <= '" + _curDate.ToString("yyyyMMdd") + "')";

            DataTable dt = new DataTable();
            
            using (newNestConn curConn = new newNestConn())
            {
                dt = curConn.Return_DataTable(SQLString);
            }

            if (dt.Rows.Count > 0)
            {
                double[] tempTickers = new double[dt.Rows.Count];
                int i = 0;

                foreach (DataRow curRow in dt.Rows)
                {
                    tempTickers[i] = double.Parse(curRow["ID_TICKER_COMPONENT"].ToString());

                    if (tempTickers[i] == 30187)
                    {
                        int a = 0;
                    }
                    
                    i++;                    
                }

                ValueData.Instance.LoadTickers(tempTickers);

                foreach (DataRow curRow in dt.Rows)
                {
                    int IdSecurity = int.Parse(curRow["ID_TICKER_COMPONENT"].ToString());                    
                    double Weight = double.Parse(curRow["WEIGHT"].ToString());

                    if (IdSecurity == 160004)
                    {
                        int a = 0;
                    }

                    DayPE curPEItem = new DayPE();
                    curPEItem.IdTickerComposite = _IdTickerComposite;
                    curPEItem.IdSecurity = IdSecurity;
                    curPEItem.Ticker = ValueData.Instance.getTicker(IdSecurity);

                    EPSValueObject curEPS = ValueData.Instance.EPS.GetValue(IdSecurity, _curDate, _curDate, false);

                    curPEItem.EPSValue = curEPS.EPSValue;
                    curPEItem.EPSDate = curEPS.RefDate;
                    curPEItem.EPSKnownDate = curEPS.KnownDate;

                    curPEItem.EPSShareNumber = ValueData.Instance.OutShares.GetValue(IdSecurity, curPEItem.EPSDate, false)[1];
                    curPEItem.CurShareNumber = ValueData.Instance.OutShares.GetValue(IdSecurity, _curDate, false)[1];
                    curPEItem.AdjEPS = curPEItem.EPSValue * curPEItem.EPSShareNumber / curPEItem.CurShareNumber;

                    if (curPEItem.AdjEPS == 0 || double.IsNaN(curPEItem.AdjEPS) || double.IsInfinity(curPEItem.AdjEPS))
                    {
                        string logMessage = "";

                        logMessage = logMessage + curPEItem.Ticker + "\t";
                        logMessage = logMessage + _curDate.ToString("dd/MM/yyyy") + "\t";
                        logMessage = logMessage + curPEItem.EPSValue.ToString() + "\t";
                        logMessage = logMessage + curPEItem.EPSDate.ToString("dd/MM/yyyy") + "\t";
                        logMessage = logMessage + curPEItem.EPSKnownDate.ToString("dd/MM/yyyy") + "\t";
                        logMessage = logMessage + curPEItem.EPSShareNumber.ToString() + "\t";
                        logMessage = logMessage + curPEItem.CurShareNumber.ToString() + "\t";

                        //ValueData.Instance.PrintLog(logMessage);
                    }

                    DayPEList.Add(curPEItem);
                }
            }            
        }

        public void StratRecalc(bool IsRT)
        {
            //Refresh Prices                   
            if (IsRT)
            {
                foreach (DayPE curPEItem in DayPEList)
                {
                    curPEItem.LastPrice = ValueData.Instance.GetRTLast(curPEItem.IdSecurity);                    
                }
            }
            else
            {
                foreach (DayPE curPEItem in DayPEList)
                {
                    curPEItem.LastPrice = ValueData.Instance.PreAuc.GetValue(curPEItem.IdSecurity, _curDate, false)[1];
                    //curPEItem.LastPrice = ValueData.Instance.Last.GetValue(curPEItem.IdSecurity, _curDate, false)[1];

                    if (curPEItem.LastPrice == 0)
                    {
                        int a = 0;
                    }
                }
            }

            //Calc average EYield
            List<double> EYieldList = new List<double>();           

            foreach (DayPE curPEItem in DayPEList)
            {
                if (!double.IsNaN(curPEItem.CurEYield) && !double.IsInfinity(curPEItem.CurEYield))
                {
                    EYieldList.Add(curPEItem.CurEYield);
                }
                else 
                {
                    int a = 0;
                }
            }

            double[] EYieldArray = EYieldList.ToArray();
            //TODO: double avgEYield = calcAverage(EYieldArray);
            double avgEYield = NestQuant.Common.Utils.calcMedian(EYieldArray);

            //Set Signals
            _ValidItems = 0;
            _LongItems = 0;
            _ShortItems = 0;
            foreach (DayPE curPEItem in DayPEList)
            {
                if (curPEItem.IdSecurity == 160004)
                {
                    int a = 0;
                }

                if (double.IsNaN(curPEItem.AdjEPS) || double.IsInfinity(curPEItem.AdjEPS)) { curPEItem.Signal = 0; }
                else if (curPEItem.CurEYield > avgEYield) { curPEItem.Signal = 1; _ValidItems++; _LongItems++; }
                else if (curPEItem.CurEYield < avgEYield) { curPEItem.Signal = -1; _ValidItems++; _ShortItems++; }
                else { curPEItem.Signal = 0; _ValidItems++; }
            }

            if (_ValidItems <= 2)
            {
                foreach (DayPE curPEItem in DayPEList)
                {
                    curPEItem.Signal = 0;
                    curPEItem.Weight = 0;
                }
            }
            else
            {
                foreach (DayPE curPEItem in DayPEList)
                {
                    double vol = calcTickerVol(curPEItem.IdSecurity, _VolDays);
                    double volHist = calcTickerVol(curPEItem.IdSecurity, _VolHistDays);

                    double volAdj = Math.Min(1.0, volHist / vol);
                    //TODO: double volAdj = 1;

                    if (curPEItem.Signal == 1) { curPEItem.Weight = (0.5 / _LongItems) * volAdj; }
                    else if (curPEItem.Signal == -1) { curPEItem.Weight = (0.5 / _ShortItems) * volAdj; }
                    else { curPEItem.Weight = 0; }

                    if (double.IsNaN(curPEItem.Weight) || double.IsInfinity(curPEItem.Weight))
                    {
                        int a = 0;
                    }
                }
            }
        }                   

        private double calcAverage(double[] values)
        {
            double average = 0;

            for (int i = 0; i < values.Length; i++)
            {
                average += values[i];
            }

            if (values.Length > 0)
            {
                average = average / values.Length;
            }

            return average;
        }        

        private double calcTickerVol(double IdSecurity, int _Days)
        {
            double vol = double.NaN;

            double[] lastArray = new double[_Days];

            DateTime VolDate = _curDate;

            for (int i = 0; i < _Days; i++)
            {
                VolDate = ValueData.Instance.PrevDate(VolDate);

                lastArray[i] = ValueData.Instance.TRDay.GetValue(IdSecurity, VolDate, true)[1];
            }

            vol = NestQuant.Common.Utils.calcStdev(ref lastArray);

            return vol;
        }
    }

    public class DayPE
    {
        private int _IdTickerComposite;
        private int _IdSecurity;
        private string _Ticker;
        private double _EPSValue;
        private DateTime _EPSDate;
        private DateTime _EPSKnownDate;
        private double _EPSShareNumber;
        private double _CurShareNumber;
        private double _AdjEPS;
        private double _LastPrice;
        private int _Signal;
        private double _Weight;

        public int IdTickerComposite
        {
            get { return _IdTickerComposite; }
            set { _IdTickerComposite = value; }
        }
        public int IdSecurity
        {
            get { return _IdSecurity; }
            set { _IdSecurity = value; }
        }
        public string Ticker
        {
            get { return _Ticker; }
            set { _Ticker = value; }
        }
        public double EPSValue
        {
            get { return _EPSValue; }
            set { _EPSValue = value; }
        }
        public DateTime EPSDate
        {
            get { return _EPSDate; }
            set { _EPSDate = value; }
        }
        public DateTime EPSKnownDate
        {
            get { return _EPSKnownDate; }
            set { _EPSKnownDate = value; }
        }
        public double EPSShareNumber
        {
            get { return _EPSShareNumber; }
            set { _EPSShareNumber = value; }
        }
        public double CurShareNumber
        {
            get { return _CurShareNumber; }
            set { _CurShareNumber = value; }
        }
        public double AdjEPS
        {
            get { return _AdjEPS; }
            set { _AdjEPS = value; }
        }
        public double LastPrice
        {
            get { return _LastPrice; }
            set { _LastPrice = value; }
        }
        public double CurEYield
        {
            get { return _AdjEPS / _LastPrice; }            
        }
        public int Signal
        {
            get { return _Signal; }
            set { _Signal = value; }
        }
        public double Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }
    }
}
