using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using NestSymConn;
using NOrderStager;
using NestDLL;
using NestQuant.Common;
using NCommonTypes;


namespace NewValueBZ
{
    class ValueCalc
    {
        public event EventHandler PositionChange;

        public double StratTotalWeight = 0;
        public double SimNAV = 0;

        #region Properties

        private DateTime _curDate = new DateTime(1900, 1, 1);
        private int _IdTickerComposite = 0;
        private SortedDictionary<int, StratPosItem> _stratPositions = new SortedDictionary<int, StratPosItem>();

        public DateTime curDate { get { return _curDate; } }
        public int IdTickerComposite { get { return _IdTickerComposite; } }
        public SortedDictionary<int, StratPosItem> stratPositions { get { return _stratPositions; } }

        #endregion


        public SortedDictionary<int, TickerPE> PositionPEs = new SortedDictionary<int, TickerPE>();

        private SortedDictionary<int, double> LotSize = new SortedDictionary<int, double>();
        private SortedDictionary<int, double> RoundLotSize = new SortedDictionary<int, double>();
        public SortedDictionary<int, double> RefPrice = new SortedDictionary<int, double>();

        public double curMedianEYield = 0;
        public double closeMedianEYield = 0;
        
        
        public void initStrategy(DateTime __curDate, int __IdTickerComposite, bool setLast, bool LoadFastData)
        {
            _curDate = __curDate;
            _IdTickerComposite = __IdTickerComposite;

            LoadCompositeData(setLast, LoadFastData);
        }

        public void StratRecalc()
        {
            double[] EYieldArray = new double[PositionPEs.Count];
            bool flagInvalidEYield = false;
            int validEYieldCounter = 0;

            stratPositions.Clear();

            int i = 0;

            foreach(TickerPE curValueItem in PositionPEs.Values)
            {
                if (!double.IsInfinity(curValueItem.curEYield) && curValueItem.curEYield != 0)
                {
                    EYieldArray[i] = curValueItem.curEYield;
                    validEYieldCounter++;
                }
                else
                    flagInvalidEYield = true;

                if (curValueItem.curPrice == 0)
                {
                    stratPositions.Clear();
                    return;
                }


                i++;
            }

            if (validEYieldCounter % 2 != 0) validEYieldCounter--;

            curMedianEYield = NestQuant.Common.Utils.calcMedian(EYieldArray);

            foreach (TickerPE curValueItem in PositionPEs.Values)
            {
                if (flagInvalidEYield)
                {
                    curValueItem.curSignal = 0;
                    curValueItem.Weight = 0;
                }
                else if (curValueItem.curEYield == curMedianEYield)
                {
                    curValueItem.curSignal = 0;
                    curValueItem.Weight = 0;
                }
                else if (curValueItem.curEYield > curMedianEYield)
                {
                    curValueItem.curSignal = 1;
                    curValueItem.Weight = 1F / validEYieldCounter * StratTotalWeight;
                }
                else if (curValueItem.curEYield < curMedianEYield)
                {
                    curValueItem.curSignal = -1;
                    curValueItem.Weight = 1F / validEYieldCounter * StratTotalWeight;
                }

                double PosPercent = curValueItem.curSignal * (curValueItem.Weight);

                StratPosItem curStratPosItem = new StratPosItem();

                curStratPosItem.Percent = PosPercent;
                curStratPosItem.PositionType = StratPosItem.PositionTypes.OnClose;
                curStratPosItem.Ticker = curValueItem.Ticker;
                double refprice = 0;
                if (RefPrice.TryGetValue(curValueItem.IdTicker, out refprice))
                {
                    curStratPosItem.RefPrice = refprice;
                }
                else
                {
                    curStratPosItem.RefPrice = curValueItem.curPrice;
                }

                _stratPositions.Add(curValueItem.IdTicker, curStratPosItem);
            }
            

        } 

        public void StratCalcPrevPos()
        {
            double[] EYieldArray = new double[PositionPEs.Count];
            bool flagInvalidPE = false;
            int validEYieldCounter = 0;

            int i = 0;

            foreach (TickerPE curValueItem in PositionPEs.Values)
            {
                if (!double.IsInfinity(curValueItem.closeEYield) && curValueItem.closeEYield != 0)
                {
                    EYieldArray[i] = curValueItem.closeEYield;
                    //validEYieldCounter++;
                }
                else
                    flagInvalidPE = true;
                validEYieldCounter++;
                i++;
            }

            if (validEYieldCounter % 2 != 0) validEYieldCounter--;

            closeMedianEYield = NestQuant.Common.Utils.calcMedian(EYieldArray, true);

            foreach (TickerPE curValueItem in PositionPEs.Values)
            {
                if (double.IsInfinity(curValueItem.closeEYield) || curValueItem.closeEYield == 0)
                //if (flagInvalidPE)
                {
                    curValueItem.closeSignal = 0;
                    curValueItem.Weight = 0;
                }
                else if (curValueItem.closeEYield == closeMedianEYield)
                {
                    curValueItem.closeSignal = 0;
                    curValueItem.Weight = 0;
                }
                else if (curValueItem.closeEYield > closeMedianEYield)
                {
                    curValueItem.closeSignal = 1;
                    curValueItem.Weight = 1F / validEYieldCounter * StratTotalWeight;
                }
                else if (curValueItem.closeEYield < closeMedianEYield)
                {
                    curValueItem.closeSignal = -1;
                    curValueItem.Weight = 1F / validEYieldCounter * StratTotalWeight;
                }
            }
        }

        private void LoadCompositeData(bool setLast, bool LoadFastData)
        {
            using(newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT Id_Ticker_Component, Weight " +
                                    " FROM NESTDB.dbo.Tb023_Securities_Composition A " +
                                    " WHERE Id_Ticker_Composite=" + _IdTickerComposite + " AND Weight<>0 AND Date_Ref=(SELECT MAX(Date_Ref) FROM NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=" + _IdTickerComposite + " AND Date_Ref<='" + _curDate.ToString("yyyy-MM-dd") + "')";

                DataTable dt = curConn.Return_DataTable(SQLString);

                if (LoadFastData)
                {
                    double[] tempTickers = new double[dt.Rows.Count];

                    int i = 0;

                    foreach (DataRow curRow in dt.Rows)
                    {
                        tempTickers[i] = NestDLL.Utils.ParseToDouble(curRow["Id_Ticker_Component"]);
                        i++;
                    }

                    if (tempTickers.Length > 0)
                    {
                        FastDataObject.Instance.curPrices.LoadTickers(tempTickers);
                        FastDataObject.Instance.curTR.LoadTickers(tempTickers);
                        FastDataObject.Instance.curShares.LoadTickers(tempTickers);
                        FastDataObject.Instance.curEPS.LoadTickers(tempTickers);
                        //FastDataObject.Instance.curEPSDates.LoadTickers(tempTickers);
                    }
                }

                foreach (DataRow curRow in dt.Rows)
                {
                    try
                    {
                        //Initialize TickerPE
                        TickerPE curValueItem = new TickerPE();
                        int tempIdTicker = int.Parse(curRow["Id_Ticker_Component"].ToString());
                                               
                        curValueItem.IdTickerComposite = _IdTickerComposite;
                        curValueItem.IdTicker = tempIdTicker;
                        
                        //Get reference dates
                        DateTime curRefDate = DateTime.FromOADate(FastDataObject.Instance.curPrices.GetValue(1073, _curDate.AddDays(-1), false)[0]);
                        DateTime closeRefDate = DateTime.FromOADate(FastDataObject.Instance.curPrices.GetValue(1073, curRefDate.AddDays(-1), false)[0]);

                        //Set current values                       
                        FastDataEPS.EPSValueObject curEPSValueObject;
                        curEPSValueObject = FastDataObject.Instance.curEPS.GetValue(tempIdTicker, curRefDate, curRefDate, false);

                        curValueItem.EPSValue = curEPSValueObject.EPSValue;
                        curValueItem.EPSDate = curEPSValueObject.RefDate;
                        curValueItem.EPSKnownDate = curEPSValueObject.KnownDate;

                        curValueItem.EPSShareNumber = FastDataObject.Instance.curShares.GetValue(tempIdTicker, curValueItem.EPSDate, false)[1];

                        curValueItem.curShareNumberDate = DateTime.FromOADate(FastDataObject.Instance.curShares.GetValue(tempIdTicker, curRefDate, false)[0]);
                        curValueItem.curShareNumber = FastDataObject.Instance.curShares.GetValue(tempIdTicker, curRefDate, false)[1];
                        curValueItem.AdjEPS = curValueItem.EPSValue * curValueItem.EPSShareNumber / curValueItem.curShareNumber;


                        //Set close values
                        DateTime closeDate = DateTime.FromOADate(FastDataObject.Instance.curPrices.GetValue(tempIdTicker, _curDate.Subtract(new TimeSpan(1, 0, 0, 0)), false)[0]);
                                                
                        FastDataEPS.EPSValueObject closeEPSValueObject;
                        closeEPSValueObject = FastDataObject.Instance.curEPS.GetValue(tempIdTicker, closeRefDate, closeRefDate, false);

                        double closeEPS = closeEPSValueObject.EPSValue;
                        double closeEPSShareNumber = FastDataObject.Instance.curShares.GetValue(tempIdTicker, closeEPSValueObject.RefDate, false)[1];
                        double closeShareNumber = FastDataObject.Instance.curShares.GetValue(tempIdTicker, closeRefDate, false)[1];

                        curValueItem.closeAdjEPS = closeEPS * closeEPSShareNumber / closeShareNumber;

                        curValueItem.closeShareNumber = FastDataObject.Instance.curShares.GetValue(tempIdTicker, closeRefDate, false)[1];
                        curValueItem.closePrice = FastDataObject.Instance.curPrices.GetValue(tempIdTicker, closeRefDate, false)[1];

                        if (setLast) curValueItem.curPrice = FastDataObject.Instance.curPrices.GetValue(tempIdTicker, curRefDate, false)[1];
                        curValueItem.DayTR = FastDataObject.Instance.curTR.GetValue(tempIdTicker, _curDate, true)[1];

                        PositionPEs.Add(tempIdTicker, curValueItem);

                        LoadLotSize(tempIdTicker);
                    }
                    catch 
                    { 

                    }
                }
            }
        }

        private void LoadLotSize(int idTicker)
        {
            string sqlString = "SELECT ROUNDLOT, ROUNDLOTSIZE " +
                               "FROM NESTDB.DBO.TB001_SECURITIES (NOLOCK) " +
                               "WHERE IDSECURITY = " + idTicker.ToString();

            using (newNestConn conn = new newNestConn())
            {
                DataTable dt = conn.Return_DataTable(sqlString);

                double dlotsize = Convert.ToDouble(dt.Rows[0][0].ToString());
                double droundlotsize = Convert.ToDouble(dt.Rows[0][1].ToString());

                LotSize.Add(idTicker, dlotsize);
                RoundLotSize.Add(idTicker, droundlotsize);
            }
        }
    }
}
