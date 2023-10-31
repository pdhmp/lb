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

        private DateTime _curDate = new DateTime(1900, 1, 1);
        private int _IdTickerComposite = 0;
        public double StratTotalWeight = 0;

        private SortedDictionary<int, int> _stratPositions = new SortedDictionary<int, int>();

        public SortedDictionary<int, TickerPE> PositionPEs = new SortedDictionary<int, TickerPE>();

        public double curMedianEYield = 0;
        public double closeMedianEYield = 0;
        
        #region Properties

        public DateTime curDate { get { return _curDate; } }
        public int IdTickerComposite { get { return _IdTickerComposite; } }
        public SortedDictionary<int, int> stratPositions { get { return _stratPositions; } }

        #endregion

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

                    FastDataObject.Instance.curPrices.LoadTickers(tempTickers);

                }


                foreach (DataRow curRow in dt.Rows)
                {
                    try
                    {
                        string SQLStringPEData = "SELECT * FROM [dbo].[FCN_SIM_PE_DATA] (" + curRow["Id_Ticker_Component"].ToString() + ", '" + _curDate.ToString("yyyy-MM-dd") + "')";
                        DataTable dtPEData = curConn.Return_DataTable(SQLStringPEData);
                        DataRow curRowPEData = dtPEData.Rows[0];

                        TickerPE curValueItem = new TickerPE();
                        int tempIdTicker = int.Parse(curRow["Id_Ticker_Component"].ToString());

                        curValueItem.IdTickerComposite = _IdTickerComposite;
                        curValueItem.IdTicker = tempIdTicker;

                        curValueItem.EPSDate = NestDLL.Utils.ParseToDateTime(curRowPEData["BBG_12M_EPS_DATE"]);
                        curValueItem.EPSKnownDate = NestDLL.Utils.ParseToDateTime(curRowPEData["BBG_12M_EPS_KNOWN"]);
                        curValueItem.EPSValue = NestDLL.Utils.ParseToDouble(curRowPEData["BBG_12M_EPS"]);
                        curValueItem.EPSShareNumber = NestDLL.Utils.ParseToDouble(curRowPEData["ECO_INI_SHARE_NUMBER"]);
                        curValueItem.curShareNumberDate = NestDLL.Utils.ParseToDateTime(curRowPEData["ECO_CUR_SHARE_NUMBER_DATE"]);
                        curValueItem.curShareNumber = NestDLL.Utils.ParseToDouble(curRowPEData["ECO_CUR_SHARE_NUMBER"]);
                        curValueItem.adjEPS = NestDLL.Utils.ParseToDouble(curRowPEData["ADJ_EPS"]);
                        curValueItem.DayTR = NestDLL.Utils.ParseToDouble(curRowPEData["DayTR"]);

                        curValueItem.closePrice = NestDLL.Utils.ParseToDouble(curRowPEData["closePrice"]);
                        if (setLast) curValueItem.curPrice = NestDLL.Utils.ParseToDouble(curRowPEData["curPrice"]);

                        PositionPEs.Add(tempIdTicker, curValueItem);
                    }
                    catch 
                    { 

                    }
                }
            }

        }



    }
}
