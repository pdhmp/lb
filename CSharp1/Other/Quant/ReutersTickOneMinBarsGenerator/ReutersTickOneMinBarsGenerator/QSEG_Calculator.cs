using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NestDLL;
using NFastData;
using System.Data;
using NestCalc;

namespace ReutersTickOneMinBarsGenerator
{
    class QSEG_Calculator
    {
        private FastData Last; 
        private FastData TR_DayBBG;
        private FastData TR_IndexBBG;
        private FastData TR_DayNest;
        private FastData TR_IndexNest;

        private List<DateTime> RunDates = new List<DateTime>();

        public QSEG_Calculator(DateTime IniDate)
        {            
            Last = new FastData(1, IniDate.AddDays(-5), DateTime.Today, 1);
            TR_IndexBBG = new FastData(101, IniDate.AddDays(-5), DateTime.Today, 1);
            TR_IndexNest = new FastData(101, IniDate.AddDays(-5), DateTime.Today, 7);
        }

        private List<int> GetIndexes()
        {
            DataTable indexesTable;

            List<int> Indexes = new List<int>();

            string SQL_Indexes =
                "SELECT distinct(SC.Id_Ticker_Component) " +
                "FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "     JOIN  " +
                "     NESTSRV06.NESTDB.dbo.Tb001_Securities S  " +
                "     ON Id_Ticker_Component = idsecurity  " +
                "WHERE  Id_Ticker_Composite = 281020	 ";

            using (newNestConn conn = new newNestConn(true))
            {
                indexesTable = conn.Return_DataTable(SQL_Indexes);
            }

            for (int i = 0; i < indexesTable.Rows.Count; i++)
            {
                Indexes.Add((int)indexesTable.Rows[i][0]);
            }

            return Indexes;
        }

        private List<int> GetNewIndexes()
        {
            DataTable indexesTable;

            List<int> Indexes = new List<int>();

            string SQL_Indexes =
                "SELECT distinct(SC.Id_Ticker_Component) " +
                "FROM NESTSRV02.RTICKDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "WHERE  Id_Ticker_Composite = 21350	 ";

            using (newNestConn conn = new newNestConn(true))
            {
                indexesTable = conn.Return_DataTable(SQL_Indexes);
            }

            for (int i = 0; i < indexesTable.Rows.Count; i++)
            {
                Indexes.Add((int)indexesTable.Rows[i][0]);
            }

            return Indexes;
        }

        private List<int> GetIndexComponents(int index)
        {
            DataTable componentsTable;

            List<int> Components = new List<int>();

            string SQL_Indexes =
                "SELECT distinct(SC.Id_Ticker_Component) " +
                "FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "     JOIN  " +
                "     NESTSRV06.NESTDB.dbo.Tb001_Securities S  " +
                "     ON Id_Ticker_Component = idsecurity  " +
                "WHERE  Id_Ticker_Composite = " + index;

            using (newNestConn conn = new newNestConn(true))
            {
                componentsTable = conn.Return_DataTable(SQL_Indexes);
            }

            for (int i = 0; i < componentsTable.Rows.Count; i++)
            {
                Components.Add((int)componentsTable.Rows[i][0]);
            }

            return Components;
        }

        private List<int> GetNewIndexComponents(int index)
        {
            DataTable componentsTable;

            List<int> Components = new List<int>();

            string SQL_Indexes =
                "SELECT distinct(SC.Id_Ticker_Component) " +
                "FROM NESTSRV02.RTICKDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "WHERE  Id_Ticker_Composite = " + index;

            using (newNestConn conn = new newNestConn(true))
            {
                componentsTable = conn.Return_DataTable(SQL_Indexes);
            }

            for (int i = 0; i < componentsTable.Rows.Count; i++)
            {
                Components.Add((int)componentsTable.Rows[i][0]);
            }

            return Components;
        }

        private Dictionary<int, Dictionary<DateTime, double>> GetIndexComponentsWeights(int index)
        {
            DataTable indexComponentsWeightsTable;

            Dictionary<int, Dictionary<DateTime, double>> IndexWeights = new Dictionary<int, Dictionary<DateTime, double>>();
            Dictionary<DateTime, double> TickerWeights = new Dictionary<DateTime, double>();
            
            int ticker = 0;
            double weight = 0;
            int prevTicker = 0;            
            DateTime refDate = new DateTime();

            string SQL_Indexes_Components =
                "SELECT * " +
                "FROM " +
                "( " +
                "    SELECT distinct(SC.Id_Ticker_Component) as IdTicker,S.NestTicker as TickerName,Id_Ticker_Composite as IdIndex, S2.NestTicker as IndexName,Weight,Date_Ref " +
                "    FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "         JOIN  " +
                "         NESTSRV06.NESTDB.dbo.Tb001_Securities S " +
                "         ON Id_Ticker_Component = S.idsecurity " +
                "         JOIN " +
                "         NESTSRV06.NESTDB.dbo.Tb001_Securities S2 " +
                "         ON Id_Ticker_Composite = S2.idsecurity " +
                "    WHERE SC.Id_Ticker_Composite =" + index.ToString() +
                ")A " +
                "ORDER BY IdIndex,IdTicker,Date_Ref ";

            using (newNestConn conn = new newNestConn(true))
            {
                indexComponentsWeightsTable = conn.Return_DataTable(SQL_Indexes_Components);
            }

            for (int i = 0; i < indexComponentsWeightsTable.Rows.Count; i++)
            {                    
                ticker = (int)indexComponentsWeightsTable.Rows[i][0];
                weight = (double)indexComponentsWeightsTable.Rows[i][4];
                refDate = (DateTime)indexComponentsWeightsTable.Rows[i][5];                

                if (ticker != prevTicker && prevTicker != 0)
                {
                    IndexWeights.Add(prevTicker,TickerWeights);
                    TickerWeights = new Dictionary<DateTime,double>();
                }

                TickerWeights.Add(refDate, weight);

                prevTicker = ticker;
            }

            IndexWeights.Add(prevTicker, TickerWeights);

            return IndexWeights;
        }

        private Dictionary<int, Dictionary<DateTime, double>> GetNewIndexComponentsWeights(int index)
        {
            DataTable indexComponentsWeightsTable;

            Dictionary<int, Dictionary<DateTime, double>> IndexWeights = new Dictionary<int, Dictionary<DateTime, double>>();
            Dictionary<DateTime, double> TickerWeights = new Dictionary<DateTime, double>();

            int ticker = 0;
            double weight = 0;
            int prevTicker = 0;
            DateTime refDate = new DateTime();

            string SQL_Indexes_Components =
                "SELECT * " +
                "FROM " +
                "( " +
                "    SELECT distinct(SC.Id_Ticker_Component) as IdTicker,S.NestTicker as TickerName,Id_Ticker_Composite as IdIndex, S2.NestTicker as IndexName,Weight,Date_Ref " +
                "    FROM NESTSRV02.RTICKDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "         JOIN  " +
                "         NESTSRV06.NESTDB.dbo.Tb001_Securities S " +
                "         ON Id_Ticker_Component = S.idsecurity " +
                "         JOIN " +
                "         NESTSRV06.NESTDB.dbo.Tb001_Securities S2 " +
                "         ON Id_Ticker_Composite = S2.idsecurity " +
                "    WHERE SC.Id_Ticker_Composite =" + index.ToString() +
                ")A " +
                "ORDER BY IdIndex,IdTicker,Date_Ref ";

            using (newNestConn conn = new newNestConn(true))
            {
                indexComponentsWeightsTable = conn.Return_DataTable(SQL_Indexes_Components);
            }

            for (int i = 0; i < indexComponentsWeightsTable.Rows.Count; i++)
            {
                ticker = (int)indexComponentsWeightsTable.Rows[i][0];
                weight = (double)indexComponentsWeightsTable.Rows[i][4];
                refDate = (DateTime)indexComponentsWeightsTable.Rows[i][5];

                if (ticker != prevTicker && prevTicker != 0)
                {
                    IndexWeights.Add(prevTicker, TickerWeights);
                    TickerWeights = new Dictionary<DateTime, double>();
                }

                TickerWeights.Add(refDate, weight);

                prevTicker = ticker;
            }

            IndexWeights.Add(prevTicker, TickerWeights);

            return IndexWeights;
        }       

        public void insertPrice(string idIndex, string Value, string Source_TimeStamp, string Source, string SrType, string DataBase)
        {
            string SQL ="";
            if (DataBase == "NESTDB")
            {
                SQL = "EXEC NESTSRV06.NESTDB.dbo.Proc_Insert_Price " + idIndex + "," + Value + ",'" + Source_TimeStamp + "'," + SrType + "," + Source + ",1";
            }                
            else
            {
                SQL = "INSERT INTO RTICKDB.dbo.Tb001_Precos_RTICK VALUES (" + idIndex + "," + Value + ",'" + Source_TimeStamp + "', getdate(), " + SrType + "," + Source + ",1)";
            }
            using (newNestConn Conn = new newNestConn(true))
            {
                int result = Conn.ExecuteNonQuery(SQL);
            }
        }

        public void InsertPriceNewModel(string idIndex, string Value, string Source_TimeStamp, int IniMinute, int EndMinute, int BaseSrType, int IsRegressive, int Source)
        {
            string SQL = "INSERT INTO RTICKDB.dbo.Tb002_Precos_QSEGS VALUES (" + idIndex + "," + Value + ",'" + Source_TimeStamp + "', getdate(), " + IniMinute + "," + EndMinute + "," + BaseSrType + "," + IsRegressive + "," + Source + ",1)";
            
            using (newNestConn Conn = new newNestConn(true))
            {
                int result = Conn.ExecuteNonQuery(SQL);
            }
        }

        public double CalculateModifiedIndex(int index, DateTime date, int SourceType, int DataSource)
        {            
            List<int> IndexComponents = GetIndexComponents(index);
            Dictionary<int, Dictionary<DateTime, double>> IndexWeights = GetIndexComponentsWeights(index);           

            DateTime curDate = date;

            FastData SourceTypeFD = new FastData(SourceType, date.AddDays(-10), date.AddDays(10), DataSource);

            double TR_Pre_Auction_Price = 0;


            double[] IndexArray = { (double)index };
            double[] Tickers = new double[IndexComponents.Count];

            for (int i = 0; i < IndexComponents.Count; i++)
            {
                Tickers[i] = (double)IndexComponents.ElementAt(i);
            }                       

            Last.LoadTickers(Tickers);
            SourceTypeFD.LoadTickers(Tickers);

            TR_IndexNest.LoadTickers(IndexArray);
            TR_IndexBBG.LoadTickers(IndexArray);

            
            DateTime prevDate = TR_IndexNest.PrevDate(date, index);

            double[] IndexValueArray = TR_IndexNest.GetValue(index, date, true);            

            if (!(IndexValueArray[1] != 0))
            {
                IndexValueArray = TR_IndexBBG.GetValue(index, date, true);
            }
            
            
            double IndexValue = IndexValueArray[1];

            foreach (int ticker in IndexComponents)
            {
                Dictionary<DateTime, double> tickerWeights = new Dictionary<DateTime, double>();
                double Weight = 0.0;                

                if (IndexWeights.TryGetValue(ticker, out tickerWeights))
                {
                    if (curDate >= tickerWeights.Last().Key)
                    {
                        Weight = tickerWeights.Last().Value;
                    }
                    else
                    {
                        for (int i = 0; i < tickerWeights.Count; i++)
                        {
                            if (curDate >= tickerWeights.Keys.ElementAt(i) && curDate < tickerWeights.Keys.ElementAt(i+1))
                            {
                                tickerWeights.TryGetValue(tickerWeights.Keys.ElementAt(i), out Weight);
                            }
                        }
                    }
                }

                if (Weight != 0)
                {

                    double[] curLast = new double[2];
                    double[] SourceTypeValue = new double[2];

                    curLast = Last.GetValue(ticker, curDate, true);
                    //SourceType = DBInterface.GetValue(ticker, curDate, true);
                    SourceTypeValue = SourceTypeFD.GetValue(ticker, curDate, true);                    
                   
                    double _Last = curLast[1];
                    double _SourceTypeValue = SourceTypeValue[1];

                    if (_SourceTypeValue == 0)
                    {
                        _SourceTypeValue = _Last;
                    }

                    double Close_PreAuc_Var = _Last / _SourceTypeValue - 1;

                    if (_Last != 0 && _SourceTypeValue != 0)
                    {
                        TR_Pre_Auction_Price += Weight * Close_PreAuc_Var;                    
                    }
                }
            }

            if (TR_Pre_Auction_Price != 0)
            {
                IndexValue /= 1 + TR_Pre_Auction_Price;                
                return IndexValue;
            }
            else
            {                
                return 0;
            }
        }

        public double CalculateModifiedNewIndex(int index, DateTime date, int SourceType, int DataSource, Dictionary<int, Dictionary<DateTime, double>> IndexWeights, List<int> IndexComponents)
        {           
            DateTime curDate = date;

            FastData SourceTypeFD = new FastData(SourceType, date.AddDays(-10), date.AddDays(10), DataSource);

            double TR_Pre_Auction_Price = 0;

            double[] IndexArray = { (double)index };
            double[] Tickers = new double[IndexComponents.Count];

            for (int i = 0; i < IndexComponents.Count; i++)
            {
                Tickers[i] = (double)IndexComponents.ElementAt(i);
            }

            Last.LoadTickers(Tickers);
            SourceTypeFD.LoadTickers(Tickers);

            double IndexValue = 0;

            string Select_TR_Index_SQL =    "SELECT SrValue FROM NESTSRV02.RTICKDB.dbo.Tb001_Precos_RTICK " +
                                            "WHERE IdSecurity = " + index + "AND SrType = 101 AND SrDate = '" + date.ToString("yyyyMMdd") + "'";

            using (newNestConn conn = new newNestConn(true))
            {
                IndexValue = conn.Return_Double(Select_TR_Index_SQL);
            }

            if (IndexValue != 0 && !double.IsNaN(IndexValue))
            {

                foreach (int ticker in IndexComponents)
                {
                    Dictionary<DateTime, double> tickerWeights = new Dictionary<DateTime, double>();
                    double Weight = 0.0;

                    if (IndexWeights.TryGetValue(ticker, out tickerWeights))
                    {
                        if (curDate >= tickerWeights.Last().Key)
                        {
                            Weight = tickerWeights.Last().Value;
                        }
                        else
                        {
                            for (int i = 0; i < tickerWeights.Count; i++)
                            {
                                if (curDate >= tickerWeights.Keys.ElementAt(i) && curDate < tickerWeights.Keys.ElementAt(i + 1))
                                {
                                    tickerWeights.TryGetValue(tickerWeights.Keys.ElementAt(i), out Weight);
                                    break;
                                }
                            }
                        }
                    }

                    if (Weight != 0)
                    {

                        double[] curLast = new double[2];
                        double[] SourceTypeValue = new double[2];

                        curLast = Last.GetValue(ticker, curDate, true);
                        SourceTypeValue = SourceTypeFD.GetValue(ticker, curDate, true);

                        double _Last = curLast[1];
                        double _SourceTypeValue = SourceTypeValue[1];

                        if (_SourceTypeValue == 0)
                        {
                            _SourceTypeValue = _Last;
                        }

                        double Close_PreAuc_Var = _Last / _SourceTypeValue - 1;

                        if (_Last != 0 && _SourceTypeValue != 0)
                        {
                            TR_Pre_Auction_Price += Weight * Close_PreAuc_Var;
                        }
                    }
                }
            }

            if (TR_Pre_Auction_Price != 0)
            {
                IndexValue /= 1 + TR_Pre_Auction_Price;
                return IndexValue;
            }
            else
            {
                if (IndexValue != 0 && !double.IsNaN(IndexValue)) return IndexValue;
                else return 0;
            }
        }

        public void CalculateAllIndexes(DateTime date)
        {            
            List<int> Indexes = GetIndexes();           

            foreach (int index in Indexes)
            {
                
                if (CalcCompositeTRIndex.CalcTRIndex(index, 1073))
                {
                    //double Value = CalculatePreAucIndex(index, date);
                    
                    double Value = CalculateModifiedIndex(index, date, 312, 22);//QSEG PreAuc

                    if (Value != 0)
                    {
                        insertPrice(index.ToString(), Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "102", "NESTDB");
                        //insertPrice(index.ToString(), Value[1].ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "10000","RTICKDB");
                    }
                    else
                    { }
                     

                    Value = CalculateModifiedIndex(index, date, 5, 1);//QSEG VWAP ALL DAY

                    if (Value != 0)
                    {
                        insertPrice(index.ToString(), Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "103","RTICKDB");
                        //insertPrice(index.ToString(), Value[1].ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "10000");
                    }
                
                
                    Dictionary<int, double> HourValues = CalculateHourLastIndex(index, date);

                    foreach (KeyValuePair<int, double> Node in HourValues)
                    {
                        int SrType = 0;
                        switch (Node.Key)
                        {
                            case 0: SrType = 1001;
                                break;
                            case 1: SrType = 1002;
                                break;
                            case 2: SrType = 1003;
                                break;
                            case 3: SrType = 1004;
                                break;
                            case 4: SrType = 1005;
                                break;
                            case 5: SrType = 1006;
                                break;
                            case 6: SrType = 1007;
                                break;
                            case 7: SrType = 1008;
                                break;
                            case 8: SrType = 1009;
                                break;
                        }
                        if (Node.Value != 0)
                        {
                            insertPrice(index.ToString(), Node.Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", SrType.ToString(),"RTICKDB");
                        }
                        else
                        { }
                    }
                    HourValues = CalculateHourVWAPIndex(index, date);

                    foreach (KeyValuePair<int, double> Node in HourValues)
                    {
                        int SrType = 0;
                        switch (Node.Key)
                        {
                            case 0: SrType = 1011;
                                break;
                            case 1: SrType = 1012;
                                break;
                            case 2: SrType = 1013;
                                break;
                            case 3: SrType = 1014;
                                break;
                            case 4: SrType = 1015;
                                break;
                            case 5: SrType = 1016;
                                break;
                            case 6: SrType = 1017;
                                break;
                            case 7: SrType = 1018;
                                break;
                            case 8: SrType = 1019;
                                break;
                        }
                        if (Node.Value != 0)
                        {
                            insertPrice(index.ToString(), Node.Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", SrType.ToString(),"RTICKDB");
                        }
                        else
                        { }
                    }


                    double Open = CalculateOpenIndex(index, date);

                    if (Open != 0)
                    {
                        insertPrice(index.ToString(), Open.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "1021", "RTICKDB");
                    }
                    else
                    { }
                
                    /*
                        double Close = CalculateCloseIndex(index, date);
                        if (Close != 0)
                        {
                           insertPrice(index.ToString(), Close.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "1022");
                        }
                    */
                }
            }
        }

        public void CalculateAllNewIndexes(DateTime date)
        {
            List<int> Indexes = GetNewIndexes();

            foreach (int index in Indexes)
            {
                List<int> IndexComponents = GetNewIndexComponents(index);
                Dictionary<int, Dictionary<DateTime, double>> IndexWeights = GetNewIndexComponentsWeights(index);

                //if (CalcCompositeTRIndex.CalcNewTRIndex(index, 1073))
                {
                    if (index == 16324)
                    { }
                    double Value = CalculateModifiedNewIndex(index, date, 312, 22, IndexWeights, IndexComponents);//QSEG PreAuc

                    if (Value != 0 && Value != 1)
                    {
                        insertPrice(index.ToString(), Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "102", "RTICKDB");
                    }
                    else
                    { }

                    /*
                    Value = CalculateModifiedNewIndex(index, date, 5, 1, IndexWeights, IndexComponents);//QSEG VWAP ALL DAY

                    if (Value != 0 && Value != 1)
                    {
                        //insertPrice(index.ToString(), Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "1000003", "RTICKDB");
                    }


                    Dictionary<int, double> HourValues = CalculateHourLastNewIndex(index, date, IndexWeights, IndexComponents);

                    foreach (KeyValuePair<int, double> Node in HourValues)
                    {
                        int IniMinute = 0, EndMinute = 0, IsRegressive = 0;
                        
                        switch (Node.Key)
                        {
                            case 0: IniMinute = 0; EndMinute = 60; IsRegressive = 0;
                                break;
                            case 1: IniMinute = 60; EndMinute = 120; IsRegressive = 0;
                                break;
                            case 2: IniMinute = 120; EndMinute = 180; IsRegressive = 0;
                                break;
                            case 3: IniMinute = 180; EndMinute = 240; IsRegressive = 0;
                                break;
                            case 4: IniMinute = 240; EndMinute = 300; IsRegressive = 0;
                                break;
                            case 5: IniMinute = 300; EndMinute = 360; IsRegressive = 0;
                                break;
                            case 6: IniMinute = 360; EndMinute = 420; IsRegressive = 0;
                                break;
                            case 7: IniMinute = 420; EndMinute = 480; IsRegressive = 0;
                                break;
                            case 8: IniMinute = 480; EndMinute = 540; IsRegressive = 0;
                                break;
                        }
                        if (Node.Value != 0 && Node.Value != 1 && !double.IsNaN(Node.Value))
                        {
                            //insertPrice(index.ToString(), Node.Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", SrType.ToString(), "RTICKDB");
                            //InsertPriceNewModel(index.ToString(), Node.Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), IniMinute, EndMinute, 1, IsRegressive, 7);
                        }
                        else
                        { }
                    }
                    HourValues = CalculateHourVWAPNewIndex(index, date, IndexWeights, IndexComponents);

                    foreach (KeyValuePair<int, double> Node in HourValues)
                    {
                        int IniMinute = 0, EndMinute = 0, IsRegressive=0;                        
                        switch (Node.Key)
                        {
                            case 0: IniMinute = 0; EndMinute = 60; IsRegressive = 0;
                                break;
                            case 1: IniMinute = 60; EndMinute = 120; IsRegressive = 0;
                                break;
                            case 2: IniMinute = 120; EndMinute = 180; IsRegressive = 0;
                                break;
                            case 3: IniMinute = 180; EndMinute = 240; IsRegressive = 0;
                                break;
                            case 4: IniMinute = 240; EndMinute = 300; IsRegressive = 0;
                                break;
                            case 5: IniMinute = 300; EndMinute = 360; IsRegressive = 0;
                                break;
                            case 6: IniMinute = 360; EndMinute = 420; IsRegressive = 0;
                                break;
                            case 7: IniMinute = 420; EndMinute = 480; IsRegressive = 0;
                                break;
                            case 8: IniMinute = 480; EndMinute = 540; IsRegressive = 0;
                                break;
                        }
                        if (Node.Value != 0 && Node.Value != 1 && !double.IsNaN(Node.Value))
                        {
                            //insertPrice(index.ToString(), Node.Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", SrType.ToString(), "RTICKDB");
                            //InsertPriceNewModel(index.ToString(), Node.Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), IniMinute, EndMinute, 5, IsRegressive, 7);
                        }
                        else
                        { }
                    }


                    double Open = CalculateOpenNewIndex(index, date, IndexWeights, IndexComponents);

                    if (Open != 0 && Open != 1)
                    {
                        //insertPrice(index.ToString(), Open.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "1000021", "RTICKDB");
                    }
                    else
                    { }
                     * */
                }                
            }
        }

        public void CalculateHistoricalIndexes(DateTime IniDate)
        {
            List<DateTime> RunDates = getRunDates(IniDate);

            foreach (DateTime Date in RunDates)
            {
                CalculateAllIndexes(Date);
            }
        }

        public void CalculateHistoricaNewIndexes(DateTime IniDate)
        {
            List<DateTime> RunDates = getRunDates(IniDate);

            foreach (DateTime Date in RunDates)
            {
                CalculateAllNewIndexes(Date);
            }
        }

        public List<DateTime> getRunDates(DateTime IniDate)
        {
            string OpenDatesQuery = "SELECT DISTINCT(P.SrDate) " +
                                    "FROM NESTSRV06.NESTDB.dbo.Tb053_Precos_Indices P (NOLOCK) " +
                                    "WHERE IdSecurity = 1073 AND " +
                                    "    SrType = 1 AND " +
                                    "    SOURCE = 1 AND" +
                                    "    SrDate >= '" + IniDate.ToString("yyyyMMdd") + "'";

            List<DateTime> Dates= new List<DateTime>();

            using (newNestConn curConn = new newNestConn(true))
            {
                DataTable DataTb = curConn.Return_DataTable(OpenDatesQuery);

                for (int i = 0; i < DataTb.Rows.Count; i++)
                {
                    Dates.Add((DateTime)DataTb.Rows[i][0]);
                }
            }

            return Dates;
        }

        public Dictionary<int, double> CalculateHourVWAPIndex(int index, DateTime date)
        {
            Dictionary<int, double> VWAP_Hour_Indexes = new Dictionary<int, double>();

            List<int> IndexComponents = GetIndexComponents(index);
            Dictionary<int, Dictionary<DateTime, double>> IndexWeights = GetIndexComponentsWeights(index);

            List<PriceCalculator> HourVWAPList = new List<PriceCalculator>();
            double[] TR_Hour_VWAP = new double[7];

            DateTime curDate = date;

            double[] IndexArray = { (double)index };
            double[] Tickers = new double[IndexComponents.Count];

            for (int i = 0; i < IndexComponents.Count; i++)
            {
                Tickers[i] = (double)IndexComponents.ElementAt(i);
            }

            Last.LoadTickers(Tickers);
            TR_IndexNest.LoadTickers(IndexArray);
            TR_IndexBBG.LoadTickers(IndexArray);

            double[] IndexValueArray = TR_IndexNest.GetValue(index, curDate, true);

            if (!(IndexValueArray[1] != 0))
            {
                IndexValueArray = TR_IndexBBG.GetValue(index, curDate, true);
            }

            double IndexValue = IndexValueArray[1];

            if (IndexValue != 0 && !double.IsNaN(IndexValue))
            {
                foreach (int ticker in IndexComponents)
                {
                    PriceCalculator TickerPrices = new PriceCalculator(ticker, curDate);

                    Dictionary<DateTime, double> tickerWeights = new Dictionary<DateTime, double>();
                    double Weight = 0.0;

                    if (IndexWeights.TryGetValue(ticker, out tickerWeights))
                    {
                        if (curDate >= tickerWeights.Last().Key)
                        {
                            Weight = tickerWeights.Last().Value;
                        }
                        else
                        {
                            for (int i = 0; i < tickerWeights.Count; i++)
                            {
                                if (curDate >= tickerWeights.Keys.ElementAt(i) && curDate < tickerWeights.Keys.ElementAt(i + 1))
                                {
                                    tickerWeights.TryGetValue(tickerWeights.Keys.ElementAt(i), out Weight);
                                }
                            }
                        }
                    }

                    if (Weight != 0)
                    {
                        double[] curLast = new double[2];

                        curLast = Last.GetValue(ticker, curDate, true);              
                        
                        double _Last = curLast[1];

                        if (_Last != 0)
                        {

                            foreach (int Hour in TickerPrices.VWAP_List.Keys)
                            {
                                if (TickerPrices.VWAP_List[Hour] != 0)
                                {
                                    TR_Hour_VWAP[Hour] += Weight * (_Last / TickerPrices.VWAP_List[Hour] - 1);
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < TR_Hour_VWAP.Length; i++)
                {
                    if (TR_Hour_VWAP[i] != 0)
                    {
                        VWAP_Hour_Indexes.Add(i, IndexValue / (1 + TR_Hour_VWAP[i]));
                    }
                    else
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (TR_Hour_VWAP[j] != 0)
                            {
                                TR_Hour_VWAP[i] = TR_Hour_VWAP[j];
                                break;
                            }
                        }

                        if (TR_Hour_VWAP[i] != 0)
                        {
                            VWAP_Hour_Indexes.Add(i, IndexValue / (1 + TR_Hour_VWAP[i]));
                        }
                        else
                        {
                            VWAP_Hour_Indexes.Add(i, IndexValue);
                        }

                    }
                }
            }

            return VWAP_Hour_Indexes;

        }

        public Dictionary<int, double> CalculateHourVWAPNewIndex(int index, DateTime date, Dictionary<int, Dictionary<DateTime, double>> IndexWeights, List<int> IndexComponents)
        {
            Dictionary<int, double> VWAP_Hour_Indexes = new Dictionary<int, double>();           

            List<PriceCalculator> HourVWAPList = new List<PriceCalculator>();
            double[] TR_Hour_VWAP = new double[7];

            DateTime curDate = date;

            double[] Tickers = new double[IndexComponents.Count];

            for (int i = 0; i < IndexComponents.Count; i++)
            {
                Tickers[i] = (double)IndexComponents.ElementAt(i);
            }

            Last.LoadTickers(Tickers);

            double IndexValue = 0;

            string Select_TR_Index_SQL = "SELECT SrValue FROM NESTSRV02.RTICKDB.dbo.Tb001_Precos_RTICK " +
                                            "WHERE IdSecurity = " + index + "AND SrType = 101 AND SrDate = '" + date.ToString("yyyyMMdd") + "'";

            using (newNestConn conn = new newNestConn(true))
            {
                IndexValue = conn.Return_Double(Select_TR_Index_SQL);
            }

            if (IndexValue != 0 && !double.IsNaN(IndexValue))
            {
                foreach (int ticker in IndexComponents)
                {
                    PriceCalculator TickerPrices = new PriceCalculator(ticker, curDate);

                    Dictionary<DateTime, double> tickerWeights = new Dictionary<DateTime, double>();
                    double Weight = 0.0;

                    if (IndexWeights.TryGetValue(ticker, out tickerWeights))
                    {
                        if (curDate >= tickerWeights.Last().Key)
                        {
                            Weight = tickerWeights.Last().Value;
                        }
                        else
                        {
                            for (int i = 0; i < tickerWeights.Count; i++)
                            {
                                if (curDate >= tickerWeights.Keys.ElementAt(i) && curDate < tickerWeights.Keys.ElementAt(i + 1))
                                {
                                    tickerWeights.TryGetValue(tickerWeights.Keys.ElementAt(i), out Weight);
                                }
                            }
                        }
                    }

                    if (Weight != 0)
                    {
                        double[] curLast = new double[2];

                        curLast = Last.GetValue(ticker, curDate, true);

                        double _Last = curLast[1];

                        if (_Last != 0)
                        {

                            foreach (int Hour in TickerPrices.VWAP_List.Keys)
                            {
                                if (TickerPrices.VWAP_List[Hour] != 0)
                                {
                                    TR_Hour_VWAP[Hour] += Weight * (_Last / TickerPrices.VWAP_List[Hour] - 1);
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < TR_Hour_VWAP.Length; i++)
                {
                    if (TR_Hour_VWAP[i] != 0)
                    {
                        VWAP_Hour_Indexes.Add(i, IndexValue / (1 + TR_Hour_VWAP[i]));
                    }
                    else
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (TR_Hour_VWAP[j] != 0)
                            {
                                TR_Hour_VWAP[i] = TR_Hour_VWAP[j];
                                break;
                            }
                        }

                        if (TR_Hour_VWAP[i] != 0)
                        {
                            VWAP_Hour_Indexes.Add(i, IndexValue / (1 + TR_Hour_VWAP[i]));
                        }
                        else
                        {
                            VWAP_Hour_Indexes.Add(i, IndexValue);
                        }

                    }
                }
            }

            return VWAP_Hour_Indexes;

        }

        public Dictionary<int, double> CalculateHourLastIndex(int index, DateTime date)
        {
            Dictionary<int, double> Last_Hour_Indexes = new Dictionary<int, double>();

            List<int> IndexComponents = GetIndexComponents(index);
            Dictionary<int, Dictionary<DateTime, double>> IndexWeights = GetIndexComponentsWeights(index);

            List<PriceCalculator> HourLastList = new List<PriceCalculator>();
            double[] TR_Hour_Last = new double[7];

            DateTime curDate = date;

            double[] IndexArray = { (double)index };
            double[] Tickers = new double[IndexComponents.Count];

            for (int i = 0; i < IndexComponents.Count; i++)
            {
                Tickers[i] = (double)IndexComponents.ElementAt(i);
            }

            Last.LoadTickers(Tickers);

            TR_IndexNest.LoadTickers(IndexArray);
            TR_IndexBBG.LoadTickers(IndexArray);


            double[] IndexValueArray = TR_IndexNest.GetValue(index, curDate, true);

            if (!(IndexValueArray[1] != 0))
            {

                IndexValueArray = TR_IndexBBG.GetValue(index, curDate, true);
            }

            double IndexValue = IndexValueArray[1];

            if (IndexValue != 0 && !double.IsNaN(IndexValue))
            {

                foreach (int ticker in IndexComponents)
                {
                    PriceCalculator TickerPrices = new PriceCalculator(ticker, curDate);

                    Dictionary<DateTime, double> tickerWeights = new Dictionary<DateTime, double>();
                    double Weight = 0.0;

                    if (IndexWeights.TryGetValue(ticker, out tickerWeights))
                    {
                        if (curDate >= tickerWeights.Last().Key)
                        {
                            Weight = tickerWeights.Last().Value;
                        }
                        else
                        {
                            for (int i = 0; i < tickerWeights.Count; i++)
                            {
                                if (curDate >= tickerWeights.Keys.ElementAt(i) && curDate < tickerWeights.Keys.ElementAt(i + 1))
                                {
                                    tickerWeights.TryGetValue(tickerWeights.Keys.ElementAt(i), out Weight);
                                }
                            }
                        }
                    }

                    if (Weight != 0)
                    {

                        double[] curLast = new double[2];

                        curLast = Last.GetValue(ticker, curDate, true);

                        double _Last = curLast[1];

                        if (_Last != 0)
                        {

                            foreach (int Hour in TickerPrices.Last_List.Keys)
                            {
                                if (TickerPrices.Last_List[Hour] != 0)
                                {
                                    TR_Hour_Last[Hour] += Weight * (_Last / TickerPrices.Last_List[Hour] - 1);
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < TR_Hour_Last.Length; i++)
                {
                    if (TR_Hour_Last[i] != 0)
                    {
                        Last_Hour_Indexes.Add(i, IndexValue / (1 + TR_Hour_Last[i]));
                    }
                    else
                    {
                        for (int j = i-1; j >= 0; j--)
                        {
                            if (TR_Hour_Last[j] != 0)
                            {
                                TR_Hour_Last[i] = TR_Hour_Last[j];
                                break;
                            }
                        }

                        if (TR_Hour_Last[i] != 0)
                        {
                            Last_Hour_Indexes.Add(i, IndexValue / (1 + TR_Hour_Last[i]));
                        }
                        else
                        {
                            Last_Hour_Indexes.Add(i, IndexValue);
                        }

                    }
                }
            }

            return Last_Hour_Indexes;

        }

        public Dictionary<int, double> CalculateHourLastNewIndex(int index, DateTime date, Dictionary<int, Dictionary<DateTime, double>> IndexWeights, List<int> IndexComponents)
        {
            Dictionary<int, double> Last_Hour_Indexes = new Dictionary<int, double>();           

            List<PriceCalculator> HourLastList = new List<PriceCalculator>();
            double[] TR_Hour_Last = new double[7];

            DateTime curDate = date;

            double[] Tickers = new double[IndexComponents.Count];

            for (int i = 0; i < IndexComponents.Count; i++)
            {
                Tickers[i] = (double)IndexComponents.ElementAt(i);
            }

            Last.LoadTickers(Tickers);

            double IndexValue = 0;

            string Select_TR_Index_SQL = "SELECT SrValue FROM NESTSRV02.RTICKDB.dbo.Tb001_Precos_RTICK " +
                                            "WHERE IdSecurity = " + index + "AND SrType = 101 AND SrDate = '" + date.ToString("yyyyMMdd") + "'";

            using (newNestConn conn = new newNestConn(true))
            {
                IndexValue = conn.Return_Double(Select_TR_Index_SQL);
            }

            if (IndexValue != 0 && !double.IsNaN(IndexValue))
            {

                foreach (int ticker in IndexComponents)
                {
                    PriceCalculator TickerPrices = new PriceCalculator(ticker, curDate);

                    Dictionary<DateTime, double> tickerWeights = new Dictionary<DateTime, double>();
                    double Weight = 0.0;

                    if (IndexWeights.TryGetValue(ticker, out tickerWeights))
                    {
                        if (curDate >= tickerWeights.Last().Key)
                        {
                            Weight = tickerWeights.Last().Value;
                        }
                        else
                        {
                            for (int i = 0; i < tickerWeights.Count; i++)
                            {
                                if (curDate >= tickerWeights.Keys.ElementAt(i) && curDate < tickerWeights.Keys.ElementAt(i + 1))
                                {
                                    tickerWeights.TryGetValue(tickerWeights.Keys.ElementAt(i), out Weight);
                                }
                            }
                        }
                    }

                    if (Weight != 0)
                    {

                        double[] curLast = new double[2];

                        curLast = Last.GetValue(ticker, curDate, true);

                        double _Last = curLast[1];

                        if (_Last != 0)
                        {

                            foreach (int Hour in TickerPrices.Last_List.Keys)
                            {
                                if (TickerPrices.Last_List[Hour] != 0)
                                {
                                    TR_Hour_Last[Hour] += Weight * (_Last / TickerPrices.Last_List[Hour] - 1);
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < TR_Hour_Last.Length; i++)
                {
                    if (TR_Hour_Last[i] != 0)
                    {
                        Last_Hour_Indexes.Add(i, IndexValue / (1 + TR_Hour_Last[i]));
                    }
                    else
                    {
                        for (int j = i - 1; j >= 0; j--)
                        {
                            if (TR_Hour_Last[j] != 0)
                            {
                                TR_Hour_Last[i] = TR_Hour_Last[j];
                                break;
                            }
                        }

                        if (TR_Hour_Last[i] != 0)
                        {
                            Last_Hour_Indexes.Add(i, IndexValue / (1 + TR_Hour_Last[i]));
                        }
                        else
                        {
                            Last_Hour_Indexes.Add(i, IndexValue);
                        }

                    }
                }
            }

            return Last_Hour_Indexes;

        }

        public double CalculateOpenIndex(int index, DateTime date)
        {
            double OpenIndex = 0;
            
            List<int> IndexComponents = GetIndexComponents(index);
            Dictionary<int, Dictionary<DateTime, double>> IndexWeights = GetIndexComponentsWeights(index);

            DateTime curDate = date;

            double[] IndexArray = { (double)index };
            double[] Tickers = new double[IndexComponents.Count];

            List<PriceCalculator> OpenList = new List<PriceCalculator>();

            for (int i = 0; i < IndexComponents.Count; i++)
            {
                Tickers[i] = (double)IndexComponents.ElementAt(i);
            }

            Last.LoadTickers(Tickers);

            TR_IndexNest.LoadTickers(IndexArray);
            TR_IndexBBG.LoadTickers(IndexArray);


            double[] IndexValueArray = TR_IndexNest.GetValue(index, curDate, true);
            if (!(IndexValueArray[1] != 0))
            {
                IndexValueArray = TR_IndexBBG.GetValue(index, curDate, true);
            }

            double IndexValue = IndexValueArray[1];

            if (IndexValue != 0 && !double.IsNaN(IndexValue))
            {

                foreach (int ticker in IndexComponents)
                {
                    PriceCalculator TickerPrices = new PriceCalculator(ticker, curDate);

                    Dictionary<DateTime, double> tickerWeights = new Dictionary<DateTime, double>();
                    double Weight = 0.0;

                    if (IndexWeights.TryGetValue(ticker, out tickerWeights))
                    {
                        if (curDate >= tickerWeights.Last().Key)
                        {
                            Weight = tickerWeights.Last().Value;
                        }
                        else
                        {
                            for (int i = 0; i < tickerWeights.Count; i++)
                            {
                                if (curDate >= tickerWeights.Keys.ElementAt(i) && curDate < tickerWeights.Keys.ElementAt(i + 1))
                                {
                                    tickerWeights.TryGetValue(tickerWeights.Keys.ElementAt(i), out Weight);
                                }
                            }
                        }
                    }

                    if (Weight != 0)
                    {

                        double[] curLast = new double[2];
                        double[] trDay = new double[2];

                        curLast = Last.GetValue(ticker, curDate, true);
                         
                        double _Last = curLast[1];

                        if (_Last != 0)
                        {
                            if (TickerPrices.Open == 0)
                            {
                                FastData BBGOpen = new FastData(8, date, date, 1);
                                double[] AuxTickerArray = { ticker };
                                BBGOpen.LoadTickers(AuxTickerArray);
                                OpenIndex += Weight * (_Last / BBGOpen.GetValue(ticker, date, true)[1] - 1);

                            }
                            else
                            {
                                OpenIndex += Weight * (_Last / TickerPrices.Open - 1);
                            }
                            if(double.IsInfinity(OpenIndex))
                            {}
                        }
                    }
                }

                if (OpenIndex != 0)
                {
                    OpenIndex = IndexValue / (1 + OpenIndex);
                }
                else
                { }
            }

            return OpenIndex;
        }

        public double CalculateOpenNewIndex(int index, DateTime date, Dictionary<int, Dictionary<DateTime, double>> IndexWeights, List<int> IndexComponents)
        {
            double OpenIndex = 0;           

            DateTime curDate = date;

            double[] IndexArray = { (double)index };
            double[] Tickers = new double[IndexComponents.Count];

            List<PriceCalculator> OpenList = new List<PriceCalculator>();

            for (int i = 0; i < IndexComponents.Count; i++)
            {
                Tickers[i] = (double)IndexComponents.ElementAt(i);
            }

            Last.LoadTickers(Tickers);

            double IndexValue = 0;

            string Select_TR_Index_SQL = "SELECT SrValue FROM NESTSRV02.RTICKDB.dbo.Tb001_Precos_RTICK " +
                                            "WHERE IdSecurity = " + index + "AND SrType = 101 AND SrDate = '" + date.ToString("yyyyMMdd") + "'";

            using (newNestConn conn = new newNestConn(true))
            {
                IndexValue = conn.Return_Double(Select_TR_Index_SQL);
            }

            if (IndexValue != 0 && !double.IsNaN(IndexValue))
            {

                foreach (int ticker in IndexComponents)
                {
                    PriceCalculator TickerPrices = new PriceCalculator(ticker, curDate);

                    Dictionary<DateTime, double> tickerWeights = new Dictionary<DateTime, double>();
                    double Weight = 0.0;

                    if (IndexWeights.TryGetValue(ticker, out tickerWeights))
                    {
                        if (curDate >= tickerWeights.Last().Key)
                        {
                            Weight = tickerWeights.Last().Value;
                        }
                        else
                        {
                            for (int i = 0; i < tickerWeights.Count; i++)
                            {
                                if (curDate >= tickerWeights.Keys.ElementAt(i) && curDate < tickerWeights.Keys.ElementAt(i + 1))
                                {
                                    tickerWeights.TryGetValue(tickerWeights.Keys.ElementAt(i), out Weight);
                                }
                            }
                        }
                    }

                    if (Weight != 0)
                    {

                        double[] curLast = new double[2];
                        double[] trDay = new double[2];

                        curLast = Last.GetValue(ticker, curDate, true);

                        double _Last = curLast[1];

                        if (_Last != 0)
                        {
                            if (TickerPrices.Open == 0)
                            {
                                FastData BBGOpen = new FastData(8, date, date, 1);
                                double[] AuxTickerArray = { ticker };
                                BBGOpen.LoadTickers(AuxTickerArray);
                                OpenIndex += Weight * (_Last / BBGOpen.GetValue(ticker, date, true)[1] - 1);

                            }
                            else
                            {
                                OpenIndex += Weight * (_Last / TickerPrices.Open - 1);
                            }
                            if (double.IsInfinity(OpenIndex))
                            { }
                        }
                    }
                }

                if (OpenIndex != 0)
                {
                    OpenIndex = IndexValue / (1 + OpenIndex);
                }
                else
                { }
            }

            return OpenIndex;
        }
    }
}
