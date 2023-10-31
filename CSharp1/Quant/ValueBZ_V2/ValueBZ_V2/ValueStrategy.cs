using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NestDLL;
using NCommonTypes;

namespace ValueBZ_V2
{
    public class ValueStrategy
    {
        public SortedDictionary<int, ValueModel> SectorCalcs = new SortedDictionary<int, ValueModel>();

        public SortedDictionary<int, newTickerPE> PositionPEs = new SortedDictionary<int, newTickerPE>();

        private SortedDictionary<int, StratPosItem> _stratPositions = new SortedDictionary<int, StratPosItem>();
        public SortedDictionary<int, StratPosItem> stratPositions { get { return _stratPositions; } }
        private int StratCount = 0;
        private double SectorWeight = 0;

        private DateTime _curDate;
        public DateTime curDate
        {
            get { return _curDate; }
            set { _curDate = value; }
        }

        private bool _IsRT = false;
        public bool IsRT
        {
            get { return _IsRT; }
            set { _IsRT = value; }
        }

        public ValueStrategy(DateTime __curDate)
        {
            _curDate = __curDate;

            if (_curDate == DateTime.Today) { _IsRT = true; }

            LoadComposites();

            StratRecalc();
            StratRecalc();
        }

        private void LoadComposites()
        {
            //TODO: Remover setores que Luis removeu caso o backtest nao fique bom.
            /*
            string SQLString = "SELECT Id_Ticker_Component " +
                               "FROM dbo.Tb023_Securities_Composition " +
                               "WHERE Id_Ticker_Component NOT IN (16307,16310,16311,16317,16318) " +
                               "AND Id_Ticker_Composite=21350 " +
                               "GROUP BY Id_Ticker_Component " +
                               "ORDER BY Id_Ticker_Component";
             */

            string SQLString = "SELECT Id_Ticker_Component " +
                               "FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_Composition " +
                               "WHERE Id_Ticker_Composite=21350 " +
                               "AND Id_Ticker_Component NOT IN (16307,16310,16311,16317,16318) " +
                               "GROUP BY Id_Ticker_Component " +
                               "ORDER BY Id_Ticker_Component";

            DataTable dt = new DataTable();

            using (newNestConn curConn = new newNestConn())
            {
                dt = curConn.Return_DataTable(SQLString);
            }

            StratCount = dt.Rows.Count;
            SectorWeight = 1F / StratCount;

            foreach (DataRow curRow in dt.Rows)
            {
                int IdComposite = int.Parse(curRow[0].ToString());

                if (IdComposite == 16314)
                {
                    int a = 0;
                }
                

                InitComposite(IdComposite);
            }
        }

        private void InitComposite(int IdTickerComposite)
        {
            ValueModel curValueModel = new ValueModel(_curDate, IdTickerComposite, IsRT, SectorWeight);
            
            SectorCalcs.Add(IdTickerComposite, curValueModel);
        }

        public void StratRecalc()
        {
            _stratPositions.Clear();
            PositionPEs.Clear();
            foreach (ValueModel curModel in SectorCalcs.Values)
            {
                curModel.StratRecalc();

                foreach (KeyValuePair<int, StratPosItem> curKVP in curModel.stratPositions)
                {
                    try
                    {
                        _stratPositions.Add(curKVP.Key, curKVP.Value);                        
                    }
                    catch (System.ArgumentException e)
                    {
                        throw new System.NotImplementedException("Duplicate key in StraPositions", e);
                    }
                }

                foreach (KeyValuePair<int, newTickerPE> curKVP in curModel.PositionPEs)
                {
                    try
                    {
                        PositionPEs.Add(curKVP.Key, curKVP.Value);
                    }
                    catch (System.ArgumentException e)
                    {
                        throw new System.NotImplementedException("Duplicate key in PositionPEs", e);
                    }
                }

            }
        }
    }
}
