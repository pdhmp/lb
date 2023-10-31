using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using NestDLL;
using NCommonTypes;

namespace NewValueBZ
{
    class ValueBZ_Sim
    {
        public SortedDictionary<int, ValueCalc> SectorCalcs = new SortedDictionary<int, ValueCalc>();
        SortedDictionary<string, int> SimbolToIdTicker = new SortedDictionary<string, int>();
        public SortedDictionary<DateTime, StatItem> SimData = new SortedDictionary<DateTime, StatItem>();
        int StratCount = 0;

        public DateTime IniDate;

        public ValueBZ_Sim(DateTime _IniDate)
        {
            IniDate = _IniDate;
            using (newNestConn curConn = new newNestConn())
            {
                //string SQLString = "SELECT SrDate FROM NESTDB.dbo.Tb053_Precos_Indices WHERE SrDate='2010-11-04' AND IdSecurity=1073 AND SrType=1 AND Source=1 ORDER BY SrDate DESC";
                //string SQLString = "SELECT TOP 350 Data_Hora_Reg FROM NESTDB.dbo.Tb053_Precos_Indices WHERE Id_Ativo=1073 AND Tipo_Preco=1 AND Source=1 ORDER BY Data_Hora_Reg DESC";
                string SQLString = "SELECT SrDate FROM NESTDB.dbo.Tb053_Precos_Indices WHERE IdSecurity=1073 AND SrType=1 AND Source=1 AND SrDate>='" + IniDate.ToString("yyyy-MM-dd") + "' ORDER BY SrDate DESC";
                DataTable dt = curConn.Return_DataTable(SQLString);

                int curCount = 0;

                foreach (DataRow curRow in dt.Rows)
                {
                    DateTime curDate = DateTime.Parse(curRow["SrDate"].ToString());
                    LoadForDate(curDate);

                    //Console.WriteLine(curDate.ToString() + '\t' + SimData[curDate].Performance.ToString());
                    Console.WriteLine(curCount++.ToString());

                    SectorCalcs.Clear();
                }
            }
        }

        private void LoadForDate(DateTime LoadDate)
        {
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "SELECT Id_Ticker_Component FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Component NOT IN (16307,16310,16311,16317,16318) AND Id_Ticker_Composite=21350 GROUP BY Id_Ticker_Component ORDER BY Id_Ticker_Component";
                //string SQLString = "SELECT 16319";
                DataTable dt = curConn.Return_DataTable(SQLString);
                
                StratCount = dt.Rows.Count;

                foreach (DataRow curRow in dt.Rows)
                {
                    int curId = Convert.ToInt16(curRow[0].ToString());
                    InitStrategy(curId, LoadDate);
                }
                StatItem curStatItem = CalcStats();
                curStatItem.RefDate = LoadDate;
                SimData.Add(LoadDate, curStatItem);
            }
        }

        public void InitStrategy(int IdTickerComposite, DateTime LoadDate)
        {
            ValueCalc curValueCalc = new ValueCalc();
            curValueCalc.initStrategy(LoadDate, IdTickerComposite, true, true);

            SectorCalcs.Add(IdTickerComposite, curValueCalc);
            curValueCalc.StratTotalWeight = 1F / StratCount;
            curValueCalc.StratRecalc();
            curValueCalc.StratCalcPrevPos();
        }

        private StatItem CalcStats()
        {
            StatItem curStatItem = new StatItem();

            foreach (ValueCalc curValueCalc in SectorCalcs.Values)
            {
                foreach (KeyValuePair<int, TickerPE> curValueItems in curValueCalc.PositionPEs)
                {
                    TickerPE curValueItem = curValueItems.Value;
                    curStatItem.StratPositions.Add(curValueItem);
                }
            }
            curStatItem.CalcStats();
            
            return curStatItem;

        }

        public class StatItem
        {
            public DateTime RefDate = new DateTime(1900, 01, 01);
            
            public double Performance = 0;

            public double Long = 0;
            public double Short = 0;
            public double Gross = 0;
            public double Net = 0;

            public List<TickerPE> StratPositions = new List<TickerPE>();

            public void CalcStats()
            {
                foreach (TickerPE curValueItem in StratPositions)
                {
                    double curValue = curValueItem.Weight * curValueItem.closeSignal;
                    if (curValue > 0) Long = Long + curValue;
                    if (curValue < 0) Short = Short + curValue;
                    Net = Net + curValue;
                    Gross = Gross + Math.Abs(curValue);
                    Performance = Performance + curValue * curValueItem.DayTR;
                    curValueItem.StratContrib = curValue * curValueItem.DayTR;
                }
            }
        }

    }
}
