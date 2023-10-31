using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using NestSimDLL;

using NestSim.Common;

namespace NestSim.HS
{
    public class HSContainer
    {

        #region Private Fields

        private SortedDictionary<int, HeadAndShoulders> HSTickers;

        private int _Id_SimSet;
        private int _Id_Sim;
        private double _HurdleRate;
        private double _BandPercent;
        private double _Horizontal;
        private int _ExitBar;
        private double _GainLimit;
        private double _StopLimit;
        private int _ExitStrategy;
        private int _SideFactor;
        private DateTime _BeginDate;
        private DateTime _EndDate;
        private int[] _Id_Ticker_List;
        private bool _Intraday;

        private int _Id_Ticker_Template = 1073;
        private int _Id_Ticker_Composite = 21141;

        private Price_Table SignalTables;
        private Weight_Table HSWeights;
        private PercPositions_Table HSPercPositions;
        private Performances_Table HSPerformances;
        private Price_Table HSPrices;
        private Contributions_Table HSContributions;
        private perfSummary_Table HSPerfSummary;

        #endregion
        
        #region Properties

        public int Id_SimSet
        {
            get { return _Id_SimSet; }
            set { _Id_SimSet = value; }
        }
        public int Id_Sim
        {
            get { return _Id_Sim; }
            set { _Id_Sim = value; }
        }
        public double HurdleRate
        {
            get { return _HurdleRate; }
            set { _HurdleRate = value; }
        }
        public double BandPercent
        {
            get { return _BandPercent; }
            set { _BandPercent = value; }
        }
        public double Horizontal
        {
            get { return _Horizontal; }
            set { _Horizontal = value; }
        }
        public int ExitBar
        {
            get { return _ExitBar; }
            set { _ExitBar = value; }
        }
        public double GainLimit
        {
            get { return _GainLimit; }
            set { _GainLimit = value; }
        }
        public double StopLimit
        {
            get { return _StopLimit; }
            set { _StopLimit = value; }
        }
        public int ExitStrategy
        {
            get { return _ExitStrategy; }
            set { _ExitStrategy = value; }
        }
        public int SideFactor
        {
            get { return _SideFactor; }
            set { _SideFactor = value; }
        }
        public DateTime BeginDate
        {
            get { return _BeginDate; }
            set { _BeginDate = value; }
        }
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        public int[] Id_Ticker_List
        {
            get { return _Id_Ticker_List; }
            set { _Id_Ticker_List = value; }
        }
        public bool Intraday
        {
            get { return _Intraday; }
            set { _Intraday = value; }
        }

        #endregion

                
        public HSContainer(int _Id_SimSet, bool _Intraday, int[] _Id_Ticker_List,
                           double _HurdleRate, double _BandPercent, double _Horizontal,
                           int _ExitBar, double _GainLimit, double _StopLimit, 
                           int _ExitStrategy, int _SideFactor,
                           DateTime _BeginDate, DateTime _EndDate)
        {
            HurdleRate = _HurdleRate;
            BandPercent = _BandPercent;
            Horizontal = _Horizontal;
            ExitBar = _ExitBar;
            GainLimit = _GainLimit;
            StopLimit = _StopLimit;
            ExitStrategy = _ExitStrategy;
            SideFactor = _SideFactor;
            BeginDate = _BeginDate;
            EndDate = _EndDate;
            Id_Ticker_List = _Id_Ticker_List;
            Intraday = _Intraday;
            Id_SimSet = _Id_SimSet;
            Id_Sim = startSim();

            HSTickers = new SortedDictionary<int, HeadAndShoulders>();
                                    
            SignalTables = new Price_Table("SIM " + Id_Sim + " Signal Table", _Id_Ticker_Template, BeginDate, EndDate);
            HSPrices = new Price_Table("SIM " + Id_Sim + " Price Table", _Id_Ticker_Template, BeginDate, EndDate);
            HSWeights = new Weight_Table("SIM " + Id_Sim + " Weight Table", _Id_Ticker_Template, BeginDate, EndDate);
        }
               
        public void calculateHS()
        {
            for (int i = 0; i < Id_Ticker_List.Length; i++)
            {
                HSTickers.Add(Id_Ticker_List[i], new HeadAndShoulders(Id_Ticker_List[i], HurdleRate, BandPercent, Horizontal, SideFactor, Id_Sim, ExitStrategy, ExitBar, GainLimit, StopLimit));
            }

            foreach (KeyValuePair<int, HeadAndShoulders> kvp in HSTickers)
            {
                kvp.Value.calculateHS(BeginDate, EndDate, Intraday);
                SignalTables.Merge(kvp.Value.SignalTable);
            }

            SignalTables.FillCumulative();
            
            HSWeights.FillEWFromPrices(SignalTables);
            
            HSPercPositions = new PercPositions_Table(SignalTables, HSWeights);
            
            HSPrices.FillFromComposite(_Id_Ticker_Composite, 1, 1); 
            
            HSPerformances = new Performances_Table("SIM " + Id_Sim + " Performances Table", HSPrices);
            
            HSContributions = new Contributions_Table("SIM " + Id_Sim + " Contribution Table",HSPercPositions,HSPerformances);
            
            HSPerfSummary = new perfSummary_Table("SIM " + Id_Sim + " PerfSummary Table", HSContributions);
        }

        private int getIdSIM()
        {
            string SQLExpression = "SELECT ISNULL(MAX(Id_SIM),0) FROM [NESTSIM].[DBO].[TB401_SIM_PARAM]";

            int idSIM = 0;

            using (NestConn conn = new NestConn())
            {
                conn.openConn();
                SqlDataReader result = conn.ExecuteReader(SQLExpression);
                
                while (result.Read())
                {
                    idSIM = result.GetInt32(0) + 1;
                }
            }

            return idSIM;

        }

        private int startSimSet()
        {
            int id_SimSet = 0;

            //Get the next Id_Set available
            using (NestConn conn = new NestConn())
            {
                string SQLExpression = "SELECT ISNULL(MAX(ID_SIMSET),0) FROM [NESTSIM].[DBO].[TB400_SIM_SET]";

                SqlDataReader result = conn.ExecuteReader(SQLExpression);

                while (result.Read())
                {
                    id_SimSet = result.GetInt32(0) + 1;
                }
            }

            //Insert new simulation set
            string SetName = "Head and Shoulders";
            DateTime Set_BeginDate = BeginDate;
            DateTime Set_EndDate = EndDate;
            string Param0 = "Hurdle Rate";
            string Param1 = "Band Percent";
            string Param2 = "Horizontal";
            string Param3 = "Side Factor";
            string Param4 = "Exit Strategy";
            string Param5 = "Exit Bar";
            string Param6 = "Gain Limit";
            string Param7 = "Stop Limit";

            using (NestConn conn = new NestConn())
            {
                string SQLExpression = "INSERT INTO [NESTSIM].[DBO].[TB400_SIM_SET] \r\n" +
                                       "(ID_SIMSET,SETNAME,SET_BEGINDATE,SET_ENDDATE," +
                                       "PARAM0,PARAM1,PARAM2,PARAM3,PARAM4,PARAM5,PARAM6,PARAM7) VALUES \r\n" +
                                       "(" + id_SimSet.ToString() + ",'" + SetName + "','" + Set_BeginDate.ToString("yyyyMMdd HH:mm:ss.fff") + "','" +
                                       Set_EndDate.ToString("yyyyMMdd HH:mm:ss.fff") + "','" + Param0 + "','" + Param1 +
                                       "','" + Param2 + "','" + Param3 + "','" + Param4 + "','" + Param5 + "','" + Param6 + "','" + Param7 + "')";

                conn.ExecuteNonQuery(SQLExpression);
            }

            return id_SimSet;

        }
                
        private int startSim()
        {

            //Get maximum simulation ID

            int idSim = getIdSIM();

            //Insert new simulation
            using (NestConn conn = new NestConn())
            {
                conn.openConn();
                string SQLExpression = "INSERT INTO [NESTSIM].[DBO].[TB401_SIM_PARAM] \r\n" +
                                       "(ID_SIMSET, ID_SIM, PARAM0, PARAM1, PARAM2, PARAM3, PARAM4, PARAM5, PARAM6, PARAM7) VALUES " +
                                       "(" + Id_SimSet.ToString() + "," + idSim.ToString() + ",'" + HurdleRate.ToString() + "','" +
                                       BandPercent.ToString() + "','" + Horizontal.ToString() + "','" + SideFactor.ToString() + "','" +
                                       ExitStrategy.ToString() + "','" + ExitBar.ToString() + "','" + GainLimit.ToString() + "','" + 
                                       StopLimit.ToString() + "')";


                conn.ExecuteNonQuery(SQLExpression);
            }

            return idSim;
            
        }
               
        
    }
}