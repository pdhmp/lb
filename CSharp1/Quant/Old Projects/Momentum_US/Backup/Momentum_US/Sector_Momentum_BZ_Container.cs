using System;
using System.IO;
using System.Data;
using System.Collections.Generic;

using NestQuant.Common;

namespace NestQuant.Strategies
{
    public class Sector_Momentum_US_Container : Strategy
    {
        //private Performances_Table rollPerformance;
        //private Signal_Table subSignals, auxSubSignals;
        //private Weight_Table auxWeight;
        //private PercPositions_Table auxSubWeights;
               
        //private Weight_Table weightByTicker;
        //private Price_Table priceByTicker;
        //private Signal_Table signalByTicker;
        //private Performances_Table performanceByTicker;
               
        private double _TargetVol = 0.0100F;
        public double TargetVol
        {
            get { return _TargetVol; }
            set { _TargetVol = value; }
        }

        //int medianDays = 11;     
        //private int _rollWindow = 20;
        //public int rollWindow
        //{
        //    get { return _rollWindow; }
        //    set { _rollWindow = value; }
        //}

        private int _RollWindow = 20;
        public int RollWindow
        {
            get { return _RollWindow; }
            set { _RollWindow = value; }
        }
        
        PercPositions_Table auxPositions;
        Performances_Table auxPerformaces;
        Contributions_Table auxContributions;
        perfSummary_Table auxPerfSummary;

	
        public Sector_Momentum_US_Container(bool isRealTime)
            : base(isRealTime)
        {
            Id_Ticker_Composite = 27376;
        }

        public override void RunStrategy()
        {
            DataTable dt;
            using (NestConn conn = new NestConn())
            {
                string SQLString = "SELECT ID_TICKER_COMPONENT FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION"+
                                   "\r\nWHERE ID_TICKER_COMPOSITE = " + Id_Ticker_Composite +
                                   "\r\nGROUP BY ID_TICKER_COMPONENT";
                dt = conn.ExecuteDataTable(SQLString);
            }

            foreach (DataRow curRow in dt.Rows)
            {
                int curId = Convert.ToInt16(curRow[0].ToString());

                Sector_Momentum_US NewStrat = new Sector_Momentum_US(IsRealTime, this);
                NewStrat.Name = "Momentum_" + curId;
                NewStrat.ID = curId;
                NewStrat.Id_Ticker_Template = Id_Ticker_Template;
                NewStrat.Id_Ticker_Composite = curId;
                NewStrat.iniDate = iniDate;
                NewStrat.endDate = endDate;
                NewStrat.DateRowInterval = Utils.TimeIntervals.IntervalDay;
                NewStrat.StrategyType = Utils.StrategyTypes.SectorMomentum;
                NewStrat.Weighing = Utils.WeighingSchemes.EqualWeighed;             
                
                if (IsRealTime)
                {
                    NewStrat.SetPriceFeeder(PriceFeeder);
                }
                NewStrat.RunStrategy();
                AddStrategy(NewStrat);
            }

            Signal_Table stratSignals = new Signal_Table("stratSignals", Id_Ticker_Template, iniDate, endDate, IsRealTime);
            stratSignals.FillPositionFromComposite(Id_Ticker_Composite, Utils.PositionSchemes.Long);
            SignalTables.Add(stratSignals);

            Weight_Table stratWeights = new Weight_Table("stratWeights", Id_Ticker_Template, iniDate, endDate, IsRealTime);
            stratWeights.FillStyle = Utils.TableFillTypes.FillPrevious;
            stratWeights.FillFromComposite(Id_Ticker_Composite);
            WeightTables.Add(stratWeights);

            auxPositions = new PercPositions_Table(stratSignals, stratWeights);
            stPositions = new PercPositions_Table(stratSignals, stratWeights);         
                 
            Load_Performances();
            auxPerformaces = stPerformances;                    
            
            Fill_Contributions(false);
            auxContributions = stContributions;

            //Consolidate_Position();

            Benchmark = Utils.BenchmarkTypes.Custom;
            Fill_Strategy_Performance(RollWindow, false);
            auxPerfSummary = stPerfSummary;

            Adjust_Exposure();

            Fill_Contributions(true);

            Consolidate_Position();
            
            Fill_Strategy_Performance();

            SavePerformanceOnDB();
            SavePositionOnDB();

            Log.Log_CheckIn(201);

            if (IsRealTime)
            {
                foreach (Strategy NewStrategy in Strategies.Values)
                {
                    NewStrategy.StrategyChanged += new EventHandler(Refresh);
                }
            }
        }

        private void Adjust_Exposure()
        {
            int IdxRollDev = auxPerfSummary.GetCustomColumnIndex("BMROLLDEV");
            for (int i = 0; i < auxPerfSummary.DateRowCount; i++)
            {
                if (i > RollWindow)
                {
                    double adjustFactor = 1;
                    double stdev = auxPerfSummary.GetCustomValue(i - 1, IdxRollDev);
                    if (stdev != 0)
                    {
                        adjustFactor = Math.Min(1.0, (TargetVol / (stdev * Utils.NORMSINV95)));
                        for (int j = 0; j < stPositions.ValueColumnCount; j++)
                        {
                            stPositions.SetValue(i, j, auxPositions.GetValue(i, j) * adjustFactor);
                        }
                    }
                }
            }
            stPositions.AddRowGross();
            stPositions.AddRowNet();
        }

        public override void Custom_Benchmark()
        {
            BmPositions =  new PercPositions_Table(SignalTables[0],WeightTables[0]);
            Fill_Benchmark_Performances();
            BmContributions = new Contributions_Table("BmContributions", BmPositions, BmPerformances);
        }

        private void SavePerformanceOnDB()
        {
            DateTime LastPerformance = GetLastPerformanceDate();

            int index = stPerfSummary.GetDateIndex(LastPerformance) + 1;
            index = Math.Max(index - 15, 0);

            for (int i = index; i < stPerfSummary.DateRowCount; i++)
            {
                DateTime perfDate = stPerfSummary.DateRows[i];
                double perf = stPerfSummary.GetCustomValue(i, 0);
                InsertPerformanceOnDB(perfDate, perf);
            }
        }

        private void InsertPerformanceOnDB(DateTime date, double performance)
        {
            string sqlString = "EXEC NESTDB.dbo.Proc_Insert_Price " +
                               "@ID_ATIVO = 32917, " +
                               "@VALOR = " + performance.ToString().Replace(',', '.') + " , " +
                               "@DATA = '" + date.ToString("yyyyMMdd") + "', " +
                               "@TIPO_PRECO = 100, " +
                               "@SOURCE = 7, " +
                               "@AUTOMATED = 1";

            using (NestConn conn = new NestConn())
            {
                conn.openConn();
                conn.ExecuteNonQuery(sqlString);
            }

        }

        private DateTime GetLastPerformanceDate()
        {
            string sqlString = "SELECT NESTDB.dbo.FCN_LAST_DATE(32917,'" + DateTime.Today.ToString("yyyyMMdd") + "', 100, 7)";
            string result = "";

            using (NestConn conn = new NestConn())
            {
                result = conn.Execute_Query_String(sqlString);
            }

            DateTime dtresult = new DateTime(Convert.ToInt32(result.Substring(0, 4)), Convert.ToInt32(result.Substring(4, 2)), Convert.ToInt32(result.Substring(6, 2)));

            return dtresult;
        }

        private void SavePositionOnDB()
        {
            DateTime LastDate = GetLastPositionDate();

            int index = tkPositions.GetDateIndex(LastDate) + 1;

            for (int i = index; i < tkPositions.DateRowCount; i++)
            {
                DateTime PosDate = tkPositions.DateRows[i];
                InsertPositionOnDB(PosDate);
            }
        }

        private void InsertPositionOnDB(DateTime date)
        {           
            int dateBar = tkPositions.GetDateIndex(date);

            string SQLString = "";
            string Union = "SELECT '";

            SQLString = SQLString + "INSERT INTO NESTDB.DBO.TB023_SECURITIES_COMPOSITION \r\n";
            SQLString = SQLString + "SELECT * FROM \r\n";
            SQLString = SQLString + "(";

            for (int i = 0; i < tkPositions.ValueColumnCount; i++)
            {
                SQLString = SQLString + Union;
                SQLString = SQLString + date.ToString("yyyyMMdd") + "' AS DATE_REF, ";
                SQLString = SQLString + "32917 AS ID_TICKER_COMPOSITE, ";
                SQLString = SQLString + tkPositions.GetValueColumnID(i) + " AS ID_TICKER_COMPONENT, ";
                SQLString = SQLString + tkPositions.GetValue(dateBar, i).ToString().Replace(',','.') + " AS WEIGHT, ";
                SQLString = SQLString + "0 AS QUANTITY";

                Union = " UNION ALL \r\n SELECT '";
            }

            SQLString = SQLString + ") AS A";

            using (NestConn conn = new NestConn())
            {
                conn.openConn();
                conn.ExecuteNonQuery(SQLString);
            }  

        }

        private DateTime GetLastPositionDate()
        {
            string SQLString = "SELECT MAX(DATE_REF) FROM NESTDB.DBO.TB023_SECURITIES_COMPOSITION WHERE ID_TICKER_COMPOSITE = 32917";
            string result = "";

            using (NestConn conn = new NestConn())
            {
                result = conn.Execute_Query_String(SQLString);
            }

            DateTime dtresult = new DateTime();

            if (result != "")
            {
                dtresult = Convert.ToDateTime(result);
            }

            return dtresult;
        }

        //private void Refresh_Roll_Performance()
        //{
        //    int curDate = stPerformances.DateRowCount - 1;

        //    for (int i = 0; i < stPerformances.ValueColumnCount; i++)
        //    {
        //        int k = curDate - rollWindow + 1;
        //        double tempValue = 1F;

        //        if (k >= 0)
        //        {
        //            for (; k <= curDate; k++)
        //            {
        //                tempValue = tempValue * (stPerformances.GetValue(k, i) + 1);
        //            }
        //        }

        //        tempValue = tempValue - 1;
        //        rollPerformance.SetValue(curDate, i, tempValue);                
        //    }
        //}

        //private void Refresh_Signals()
        //{
        //    int curDate =  rollPerformance.DateRowCount -1;

        //    for (int j = 0; j < rollPerformance.ValueColumnCount; j++)
        //    {
        //        if (curDate < rollWindow - 1)
        //        {
        //            auxSubSignals.SetValue(curDate, j, 0);
        //        }
        //        else
        //        {
        //            double perf = rollPerformance.GetValue(curDate, j);
        //            if (perf > 0.000000001)
        //            {
        //                auxSubSignals.SetValue(curDate, j, 1);
        //            }
        //            else if (perf < -0.000000001)
        //            {
        //                auxSubSignals.SetValue(curDate, j, -1);
        //            }
        //            else
        //            {
        //                auxSubSignals.SetValue(curDate, j, 0);
        //            }
        //        }
        //    }


        //    for (int j = 0; j < auxSubSignals.ValueColumnCount; j++)
        //    {
        //        double[] medianWindow = new double[medianDays];

        //        for (int k = curDate; k > curDate - medianDays; k--)
        //        {
        //            medianWindow[curDate - k] = auxSubSignals.GetValue(k, j);
        //        }

        //        double median = Utils.calcMedian(medianWindow);

        //        subSignals.SetValue(curDate, j, median);                
        //    }
        //}

        //private void Refresh_Adjusted_Weights()
        //{
        //    double[] sample = new double[rollWindow];
        //    double stdev;
        //    double adjustFactor;

        //    int curDate = auxPerfSummary.DateRowCount - 1;

        //    if (curDate < rollWindow)
        //    {
        //        for (int j = 0; j < auxWeight.ValueColumnCount; j++)
        //        {
        //            stratWeight.SetValue(curDate, j, auxWeight.GetValue(curDate, j));
        //        }
        //    }
        //    else
        //    {

        //        for (int k = curDate - rollWindow; k < curDate; k++)
        //        {
        //            sample[k - curDate + rollWindow] = auxPerfSummary.GetCustomValue(k, 0);
        //        }

        //        stdev = Utils.calcStdev(ref sample);

        //        if (stdev != 0)
        //        {
        //            adjustFactor = (TargetVol / (stdev * Utils.NORMSINV95));
        //        }
        //        else
        //        {
        //            adjustFactor = 1F;
        //        }

        //        if (adjustFactor > 1) { adjustFactor = 1F; };

        //        for (int j = 0; j < stratWeight.ValueColumnCount; j++)
        //        {
        //            double aux = auxWeight.GetValue(curDate, j);
        //            aux = aux * adjustFactor;
        //            stratWeight.SetValue(curDate, j, aux);
        //        }
        //    }


        //}

        //protected override void UpdateStrategy(object source, EventArgs e)
        //{
        //    Refresh_Performances();

        //    Refresh_Roll_Performance();

        //    Refresh_Signals();

        //    auxSubWeights.Refresh();

        //    auxContributions.Refresh(); 

        //    auxPerfSummary.Refresh();

        //    Refresh_Adjusted_Weights();

        //    stPositions.Refresh();

        //    stContributions.Refresh();

        //    stPerfSummary.Refresh();

        //}

        //private void UpdateStrategyByTicker()
        //{
        //    int curDate = auxWeight.DateRowCount - 1;
        //    foreach (Sector_Performance curStrategy in Strategies.Values)
        //    {                
        //        //Update Prices
        //        for (int curCol = 0; curCol < curStrategy.PriceTables[0].ValueColumnCount; curCol++)
        //        {
        //            int Ticker_ID = curStrategy.PriceTables[0].GetValueColumnID(curCol);
        //            int auxCurCol = priceByTicker.GetValueColumnIndex(Ticker_ID);

        //            if (Ticker_ID != -1 && auxCurCol != -1)
        //            {
        //                double auxPrice = curStrategy.PriceTables[0].GetValue(curDate, curCol);
        //                priceByTicker.SetValue(curDate, auxCurCol, auxPrice);
        //            }
        //            else
        //            {
        //                throw new System.NotImplementedException();
        //            }
        //        }



        //        //Update Weights

        //        double sectorWeight = stratWeight.GetValue(curDate, stratWeight.GetValueColumnIndex(curStrategy.Id_Ticker_Composite));

        //        for (int curCol = 0; curCol < curStrategy.WeightTables[0].ValueColumnCount; curCol++)
        //        {
        //            int Ticker_ID = curStrategy.WeightTables[0].GetValueColumnID(curCol);
        //            int auxCurCol = weightByTicker.GetValueColumnIndex(Ticker_ID);

        //            if (Ticker_ID != -1 && auxCurCol != -1)
        //            {
        //                double weightAdjust = curStrategy.WeightTables[0].GetValue(curDate, curCol);
        //                weightByTicker.SetValue(curDate, auxCurCol, weightAdjust * sectorWeight);
        //            }
        //            else
        //            {
        //                throw new System.NotImplementedException();
        //            }
        //        }

        //        //Update Signals
        //        double sectorSignal = subSignals.GetValue(curDate, stratWeight.GetValueColumnIndex(curStrategy.Id_Ticker_Composite));

        //        for (int curCol = 0; curCol < curStrategy.WeightTables[0].ValueColumnCount; curCol++)
        //        {
        //            int Ticker_ID = curStrategy.WeightTables[0].GetValueColumnID(curCol);
        //            int auxCurCol = signalByTicker.GetValueColumnIndex(Ticker_ID);

        //            if (Ticker_ID != -1 && auxCurCol != -1)
        //            {
        //                signalByTicker.SetValue(curDate, auxCurCol, sectorSignal);
        //            }
        //            else
        //            {
        //                throw new System.NotImplementedException();
        //            }
        //        }
        //    }

        //    performanceByTicker.Refresh();
        //}

        //private void Print_Table(Base_Table table, string filePath)
        //{
        //    StreamWriter file = new StreamWriter(filePath,false);

        //    string line = "";

        //    line = line + "Date;";

        //    for (int i = 0; i < table.ValueColumnCount; i++)
        //    {
        //        line = line + "Value "+ i.ToString() + ";";
        //    }

        //    for (int i = 0; i < table.CustomColumnCount; i++)
        //    {
        //        line = line + "Custom " + i.ToString() + ";";
        //    }

        //    file.WriteLine(line);

        //    for (int i = 0; i < table.DateRowCount; i++)
        //    {
        //        line = table.DateRows[i].ToString() + ";";

        //        for (int j = 0; j < table.ValueColumnCount; j++)
        //        {
        //            line = line + table.GetValue(i, j).ToString() + ";";
        //        }

        //        for (int j = 0; j < table.CustomColumnCount; j++)
        //        {
        //            line = line + table.GetCustomValue(i, j).ToString() + ";";
        //        }

        //        file.WriteLine(line);
        //    }

        //    file.Dispose();
        //    file = null;
        //    GC.Collect();
        //}         
    }
}
