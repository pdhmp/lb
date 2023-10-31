using System;
using System.Collections.Generic;
//using System.Text;
using System.Data.SqlClient;

using NestQuant.Common;

namespace NestQuant.Strategies
{
    public class HSParameterTester
    {

        #region Private Fields

        private bool _Intraday;
        private int[] _Id_TickerList;
        private double _HurdleMin;
        private double _HurdleMax;
        private double _HurdleInterval;
        private double _BandMin;
        private double _BandMax;
        private double _BandInterval;
        private double _HorizontalMin;
        private double _HorizontalMax;
        private double _HorizontalInterval;
        private int _ExitBarMin;
        private int _ExitBarMax;
        private int _ExitBarInterval;
        private double _GainMin;
        private double _GainMax;
        private double _GainInterval;
        private double _StopMin;
        private double _StopMax;
        private double _StopInterval;
        private int _ExitStrategy;
        private int _SideFactor;
        private DateTime _BeginDate;
        private DateTime _EndDate;

        #endregion

        #region Properties

        public bool Intraday
        {
            get { return _Intraday; }
            set { _Intraday = value; }
        }
        public int[] Id_TickerList
        {
            get { return _Id_TickerList; }
            set { _Id_TickerList = value; }
        }
        public double HurdleMin
        {
            get { return _HurdleMin; }
            set { _HurdleMin = value; }
        }
        public double HurdleMax
        {
            get { return _HurdleMax; }
            set { _HurdleMax = value; }
        }
        public double HurdleInterval
        {
            get { return _HurdleInterval; }
            set { _HurdleInterval = value; }
        }
        public double BandMin
        {
            get { return _BandMin; }
            set { _BandMin = value; }
        }
        public double BandMax
        {
            get { return _BandMax; }
            set { _BandMax = value; }
        }
        public double BandInterval
        {
            get { return _BandInterval; }
            set { _BandInterval = value; }
        }
        public double HorizontalMin
        {
            get { return _HorizontalMin; }
            set { _HorizontalMin = value; }
        }
        public double HorizontalMax
        {
            get { return _HorizontalMax; }
            set { _HorizontalMax = value; }
        }
        public double HorizontalInterval
        {
            get { return _HorizontalInterval; }
            set { _HorizontalInterval = value; }
        }
        public int ExitBarMin
        {
            get { return _ExitBarMin; }
            set { _ExitBarMin = value; }
        }
        public int ExitBarMax
        {
            get { return _ExitBarMax; }
            set { _ExitBarMax = value; }
        }
        public int ExitBarInterval
        {
            get { return _ExitBarInterval; }
            set { _ExitBarInterval = value; }
        }
        public double GainMin
        {
            get { return _GainMin; }
            set { _GainMin = value; }
        }
        public double GainMax
        {
            get { return _GainMax; }
            set { _GainMax = value; }
        }
        public double GainInterval
        {
            get { return _GainInterval; }
            set { _GainInterval = value; }
        }
        public double StopMin
        {
            get { return _StopMin; }
            set { _StopMin = value; }
        }
        public double StopMax
        {
            get { return _StopMax; }
            set { _StopMax = value; }
        }
        public double StopInterval
        {
            get { return _StopInterval; }
            set { _StopInterval = value; }
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

#endregion

        
        public HSParameterTester(bool _Intraday, int[] _Id_TickerList,
                                 double _HurdleMin, double _HurdleMax, double _HurdleInterval, 
                                 double _BandMin, double _BandMax, double _BandInterval, 
                                 double _HorizontalMin, double _HorizontalMax, double _HorizontalInterval, 
                                 int _ExitBarMin, int _ExitBarMax, int _ExitBarInterval, 
                                 double _GainMin, double _GainMax, double _GainInterval, 
                                 double _StopMin, double _StopMax, double _StopInterval, 
                                 int _ExitStrategy, int _SideFactor, DateTime _BeginDate, DateTime _EndDate)
        {
            HurdleMin = _HurdleMin;
            HurdleMax = _HurdleMax;
            HurdleInterval = _HurdleInterval;

            BandMin = _BandMin;
            BandMax = _BandMax;
            BandInterval = _BandInterval;

            HorizontalMin = _HorizontalMin;
            HorizontalMax = _HorizontalMax;
            HorizontalInterval = _HorizontalInterval;

            ExitBarMin = _ExitBarMin;
            ExitBarMax = _ExitBarMax;
            ExitBarInterval = _ExitBarInterval;

            GainMin = _GainMin;
            GainMax = _GainMax;
            GainInterval = _GainInterval;

            StopMin = _StopMin;
            StopMax = _StopMax;
            StopInterval = _StopInterval;

            ExitStrategy = _ExitStrategy;

            SideFactor = _SideFactor;

            BeginDate = _BeginDate;
            EndDate = _EndDate;

            Id_TickerList = _Id_TickerList;

            Intraday = _Intraday;

         
        }
        
        public void RunParameters()
        {

            int Id_SimSet = startSimSet();

            for (double HurdleRate = HurdleMin; HurdleRate <= HurdleMax; HurdleRate = HurdleRate + HurdleInterval)
            {
                for (double BandPercent = BandMin; BandPercent <= BandMax; BandPercent = BandPercent + BandInterval)
                {
                    for (double Horizontal = HorizontalMin; Horizontal <= HorizontalMax; Horizontal = Horizontal + HorizontalInterval)
                    {
                        for (int ExitBar = ExitBarMin; ExitBar <= ExitBarMax; ExitBar = ExitBar + ExitBarInterval)
                        {
                            for (double GainLimit = GainMin; GainLimit <= GainMax; GainLimit = GainLimit + GainInterval)
                            {
                                for (double StopLimit = StopMin; StopLimit <= StopMax; StopLimit = StopLimit + StopInterval)
                                {
                                    HSContainer HSSim = new HSContainer(Id_SimSet, Intraday, 21141, HurdleRate, BandPercent, 
                                                                        Horizontal, ExitBar, GainLimit, StopLimit, ExitStrategy, SideFactor, 
                                                                        BeginDate, EndDate);
                                    HSSim.calculateHS();
                                }
                            }
                        }
                    }
                }
            }
        }
        
        private int startSimSet()
        {
            int id_SimSet = 0;

            //Get the next Id_Set available
            using (NestConn conn = new NestConn())
            {
                conn.openConn();
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
                conn.openConn();
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

        
    }
}
