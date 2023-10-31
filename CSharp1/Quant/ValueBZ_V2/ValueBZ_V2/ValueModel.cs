using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NCommonTypes;
using NestDLL;

namespace ValueBZ_V2
{
    public class ValueModel
    {
        private DateTime _curDate;
        private DateTime _prevDate;
        private int _IdTickerComposite;
        private bool _IsRT;
        private double _sectorWeight;
        
        private ValueCalc curCalc;
        private ValueCalc prevCalc;

        public SortedDictionary<int, newTickerPE> PositionPEs = new SortedDictionary<int, newTickerPE>();
        private SortedDictionary<int, StratPosItem> _stratPositions = new SortedDictionary<int, StratPosItem>();
        public SortedDictionary<int, StratPosItem> stratPositions { get { return _stratPositions; } }
        private SortedDictionary<int, StratPosItem> ZeroItems = new SortedDictionary<int, StratPosItem>();

        public ValueModel(DateTime __curDate, int __IdTickerComposite, bool __IsRT, double __sectorWeight)
        {
            _IdTickerComposite = __IdTickerComposite;
            _IsRT = __IsRT;
            _sectorWeight = __sectorWeight;

            _curDate = __curDate;
            _prevDate = ValueData.Instance.PrevDate(_curDate);


            if (_IdTickerComposite == 16315)
            {
                if (_curDate <= (new DateTime(2011, 03, 01)))
                {
                    int a = 0;
                }
            }

            curCalc = new ValueCalc(_curDate, _IdTickerComposite);
            prevCalc = new ValueCalc(_prevDate, _IdTickerComposite);

            LoadZeroItems();
        }

        public void StratRecalc()
        {                        
            curCalc.StratRecalc(_IsRT);
            prevCalc.StratRecalc(false);

            if (prevCalc.ValidItems == 0)
            {
                int a = 0;
            }            
            
            foreach (DayPE curPEitem in prevCalc.DayPEList)
            {
                if (!PositionPEs.ContainsKey(curPEitem.IdSecurity))
                {
                    newTickerPE newTickerPE = new newTickerPE();
                    newTickerPE.StratName = "ValueBZ";
                    newTickerPE.IdTickerComposite = curPEitem.IdTickerComposite;
                    newTickerPE.IdTicker = curPEitem.IdSecurity;
                    newTickerPE.Ticker = curPEitem.Ticker;

                    newTickerPE.closeAdjEPS = curPEitem.AdjEPS;
                    newTickerPE.closeShareNumber = curPEitem.CurShareNumber;
                    newTickerPE.closePrice = curPEitem.LastPrice;                    
                    newTickerPE.closeSignal = curPEitem.Signal;
                    newTickerPE.CloseWeight = curPEitem.Weight * _sectorWeight;
                    

                    if (double.IsNaN(newTickerPE.CloseWeight))
                    {
                        int a = 0;
                    }

                    PositionPEs.Add(curPEitem.IdSecurity, newTickerPE);                    
                }
                else
                {
                    PositionPEs[curPEitem.IdSecurity].closeAdjEPS = curPEitem.AdjEPS;
                    PositionPEs[curPEitem.IdSecurity].closeShareNumber = curPEitem.CurShareNumber;
                    PositionPEs[curPEitem.IdSecurity].closePrice = curPEitem.LastPrice;
                    PositionPEs[curPEitem.IdSecurity].closeSignal = curPEitem.Signal;
                    PositionPEs[curPEitem.IdSecurity].CloseWeight = curPEitem.Weight * _sectorWeight;
                    
                    if (double.IsNaN(PositionPEs[curPEitem.IdSecurity].CloseWeight))
                    {
                        int a = 0;
                    }
                }
            }
                        
            foreach (DayPE curPEitem in curCalc.DayPEList)
            {
                if (!PositionPEs.ContainsKey(curPEitem.IdSecurity))
                {
                    newTickerPE newTickerPE = new newTickerPE();
                    newTickerPE.StratName = "ValueBZ";
                    newTickerPE.IdTickerComposite = curPEitem.IdTickerComposite;
                    newTickerPE.IdTicker = curPEitem.IdSecurity;
                    newTickerPE.Ticker = curPEitem.Ticker;

                    newTickerPE.closeSignal = 0;

                    newTickerPE.AdjEPS = curPEitem.AdjEPS;
                    newTickerPE.curPrice = curPEitem.LastPrice;
                    newTickerPE.curShareNumber = curPEitem.CurShareNumber;
                    newTickerPE.curSignal = curPEitem.Signal;
                    newTickerPE.EPSDate = curPEitem.EPSDate;
                    newTickerPE.EPSKnownDate = curPEitem.EPSKnownDate;
                    newTickerPE.EPSShareNumber = curPEitem.EPSShareNumber;
                    newTickerPE.EPSValue = curPEitem.EPSValue;
                    newTickerPE.Weight = curPEitem.Weight * _sectorWeight;
                 
                    PositionPEs.Add(curPEitem.IdSecurity, newTickerPE);
                }
                else
                {
                    PositionPEs[curPEitem.IdSecurity].AdjEPS = curPEitem.AdjEPS;                    
                    PositionPEs[curPEitem.IdSecurity].curShareNumber = curPEitem.CurShareNumber;
                    PositionPEs[curPEitem.IdSecurity].curSignal = curPEitem.Signal;
                    PositionPEs[curPEitem.IdSecurity].EPSDate = curPEitem.EPSDate;
                    PositionPEs[curPEitem.IdSecurity].EPSKnownDate = curPEitem.EPSKnownDate;
                    PositionPEs[curPEitem.IdSecurity].EPSShareNumber = curPEitem.EPSShareNumber;
                    PositionPEs[curPEitem.IdSecurity].EPSValue = curPEitem.EPSValue;
                    PositionPEs[curPEitem.IdSecurity].Weight = curPEitem.Weight * _sectorWeight;

                    PositionPEs[curPEitem.IdSecurity].curPrice = curPEitem.LastPrice;
                    PositionPEs[curPEitem.IdSecurity].closePrice = PositionPEs[curPEitem.IdSecurity].closePrice;
                                     
                }
            }

            stratPositions.Clear();

            foreach (newTickerPE curValueItem in PositionPEs.Values)
            {
                curValueItem.DayTR = ValueData.Instance.GetDayChange(curValueItem.IdTicker, _curDate);
                curValueItem.StratContrib = curValueItem.CloseWeight * curValueItem.DayTR * curValueItem.closeSignal;

                StratPosItem curStratPosItem = new StratPosItem();
                curStratPosItem.Percent = curValueItem.curSignal * curValueItem.Weight;
                curStratPosItem.PositionType = StratPosItem.PositionTypes.OnClose;
                curStratPosItem.Ticker = curValueItem.Ticker;                
                curStratPosItem.RefPrice = curValueItem.curPrice;

                _stratPositions.Add(curValueItem.IdTicker, curStratPosItem);
            }

            //AddZeroItems
            foreach (KeyValuePair<int, StratPosItem> curZero in ZeroItems)
            {
                if (!_stratPositions.ContainsKey(curZero.Key))
                {
                    _stratPositions.Add(curZero.Key, curZero.Value);
                }
            }           
        }

        private void LoadZeroItems()
        {
            ZeroItems.Clear();

            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT Id_Ticker_Component, Weight " +
                                    " FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_Composition A " +
                                    " WHERE Id_Ticker_Composite=" + _IdTickerComposite + " AND Weight=0 AND Date_Ref=(SELECT MAX(Date_Ref) FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=" + _IdTickerComposite + " AND Date_Ref<='" + _curDate.ToString("yyyy-MM-dd") + "')" +
                                    " AND ID_TICKER_COMPONENT not in (81076,1701)";

                DataTable dt = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {
                    int idSecurity = int.Parse(curRow["Id_Ticker_Component"].ToString());

                    StratPosItem curItem = new StratPosItem();
                    curItem.Ticker = curConn.Execute_Query_String("SELECT NestTicker FROM NESTSRV06.NESTDB.dbo.Tb001_Securities WITH(NOLOCK) WHERE IdSecurity=" + idSecurity);
                    curItem.Quantity = 0;
                    curItem.Percent = 0;
                    curItem.PositionType = StratPosItem.PositionTypes.OnClose;
                    curItem.RefPrice = double.MaxValue;

                    ZeroItems.Add(idSecurity, curItem);
                }
            }
        }

    }
}
