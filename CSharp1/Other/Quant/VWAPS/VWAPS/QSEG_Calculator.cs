using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NestDLL;
using NFastData;
using System.Data;
using NestCalc;
using System.Diagnostics;

namespace VWAPS
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
        //getindexes retorna os indices qsegs
        private List<int> GetIndexes()
        {
            DataTable indexesTable;

            List<int> Indexes = new List<int>();
            //esse cara retorna os qsegs
            string SQL_Indexes =
                "SELECT distinct(SC.Id_Ticker_Component) " +
                "FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "     JOIN  " +
                "     NESTSRV06.NESTDB.dbo.Tb001_Securities S  " +
                "     ON Id_Ticker_Component = idsecurity  " +
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

        private List<int> GetNewIndexes()
        {
            DataTable indexesTable;

            List<int> Indexes = new List<int>();

            string SQL_Indexes =
                "SELECT distinct(SC.Id_Ticker_Component) " +
                "FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "     JOIN  " +
                "     NESTSRV06.NESTDB.dbo.Tb001_Securities S  " +
                "     ON Id_Ticker_Component = idsecurity  " +
                "WHERE  Id_Ticker_Composite = 281020		 ";

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
        //esse cara retorna as ações que compoe cada indice
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
                "FROM NESTSRV06.NESTDB.dbo.Tb023_Securities_CompositiON SC (nolock) " +
                "     JOIN  " +
                "     NESTSRV06.NESTDB.dbo.Tb001_Securities S  " +
                "     ON Id_Ticker_Component = idsecurity  " +
                "WHERE  Id_Ticker_Composite =  " + index;

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
        //esse cara retorna o peso de cada papel dentro do indice
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
                    IndexWeights.Add(prevTicker, TickerWeights);
                    TickerWeights = new Dictionary<DateTime, double>();
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
                    IndexWeights.Add(prevTicker, TickerWeights);
                    TickerWeights = new Dictionary<DateTime, double>();
                }

                TickerWeights.Add(refDate, weight);

                prevTicker = ticker;
            }

            IndexWeights.Add(prevTicker, TickerWeights);

            return IndexWeights;
        }
        //evidentemente, insere o preço de acordo com o tipo.
        public void insertPrice(string idIndex, string Value, string Source_TimeStamp, string Source, string SrType, string DataBase)
        {
            string SQL = "";
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
            List<int> IndexComponents = GetIndexComponents(index);//pega os papeis que compoe o indice.
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
            //pega todos os tickers
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
            
            string Select_TR_Index_SQL = "SELECT SrValue FROM NESTSRV06.NESTDB.dbo.Tb053_Precos_Indices " +
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
                        insertPrice(index.ToString(), Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "103", "RTICKDB");
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
                            insertPrice(index.ToString(), Node.Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", SrType.ToString(), "RTICKDB");
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
                            insertPrice(index.ToString(), Node.Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", SrType.ToString(), "RTICKDB");
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
                    //cheguei aqui.
                    if (index == 265861)
                    {
                        Console.WriteLine("");
                    }
                    double Value = CalculateModifiedNewIndex(index, date, 312, 22, IndexWeights, IndexComponents);//QSEG PreAuc

                    if (Value != 0 && Value != 1)
                    {
                        if (index >=16307 && index <=16324) {
                            int x = 5;
                        } else {
                            insertPrice(index.ToString(), Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "102", "NESTDB");
                        }
                    }
                    else
                    { }
                    
                    
                   Value = CalculateModifiedNewIndex(index, date, 5, 1, IndexWeights, IndexComponents);//QSEG VWAP ALL DAY

                    if (Value != 0 && Value != 1)
                    {
                        if (index >= 16307 && index <= 16324)
                        {
                            int x = 5;
                        }
                        else
                        {
                            Debug.WriteLine("Index:  " + index + " Value: " + Value);
                            insertPrice(index.ToString(), Value.ToString().Replace(",", "."), date.ToString("yyyyMMdd"), "7", "103", "RTICKDB");
                        }
                    }
                    /*

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

        public void CalculateHistoricaNewIndexes(DateTime IniDate, DateTime limDate)
        {
            List<DateTime> RunDates = getRunDates(IniDate);

            foreach (DateTime Date in RunDates)
            {
                if (Date < limDate){
                    CalculateAllNewIndexes(Date);
                    Debug.WriteLine("Data: " + Date);
            } else {
                Debug.WriteLine("opa");
            }
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

            List<DateTime> Dates = new List<DateTime>();

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

        public void CalculateAllTRDay(DateTime IniDate, DateTime limDate)
        {
            List<DateTime> RunDates = getRunDates(IniDate);

            foreach (DateTime Date in RunDates)
            {
                if (Date < limDate)
                {
                    List<int> indexes = GetNewIndexes();
                    double tr = 0;
                    foreach (int index in indexes)
                    {
                        Debug.WriteLine("Date: " + Date + " Index: " + index);
                        tr = CalculateTRDay(index, Date);
                        insertPrice(index.ToString(), tr.ToString().Replace(",", "."), Date.ToString("yyyyMMdd"), "7", "100", "NESTDB");
                        updateTRindex(index);
                    }
                }
                else
                {
                    Debug.WriteLine(" opa");
                }
            }
        }
        public void updateTRindex(int index)
        {
            string SQL = "EXEC NESTSRV06.NESTDB.dbo.Proc_Update_TRIndex " + index.ToString();

            using (newNestConn Conn = new newNestConn(true))
            {
                Conn.ExecuteNonQuery(SQL);
                Debug.WriteLine("TR Index: " + index);
            }
        }

        public void CalculateAllVWAPS(DateTime IniDate, DateTime limDate)
        {
            List<DateTime> RunDates = getRunDates(IniDate);

            foreach (DateTime Date in RunDates)
            {
                if (Date < limDate)
                {
                    List<int> indexes = GetNewIndexes();
                    double tr = 0;
                    foreach (int index in indexes)
                    {
                        Debug.WriteLine("Date: " + Date + " Index: " + index);

                        Dictionary<int, double> HourValues = CalculateHourLastIndex(index, Date);

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
                                insertPrice(index.ToString(), Node.Value.ToString().Replace(",", "."), Date.ToString("yyyyMMdd"), "7", SrType.ToString(), "RTICKDB");
                            }
                            else
                            { }
                        }
                        HourValues = CalculateHourVWAPIndex(index, Date);

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
                                insertPrice(index.ToString(), Node.Value.ToString().Replace(",", "."), Date.ToString("yyyyMMdd"), "7", SrType.ToString(), "RTICKDB");
                            }
                            else
                            { }
                        }


                        double Open = CalculateOpenIndex(index, Date);

                        if (Open != 0)
                        {
                            insertPrice(index.ToString(), Open.ToString().Replace(",", "."), Date.ToString("yyyyMMdd"), "7", "1021", "RTICKDB");
                        }
                        else
                        { }
                        //tr = CalculateTRDay(index, Date);
                        //insertPrice(index.ToString(), tr.ToString().Replace(",", "."), Date.ToString("yyyyMMdd"), "7", "100", "NESTDB");
                        //updateTRindex(index);
                    }
                }
                else
                {
                    Debug.WriteLine("nao deu certo");
                }
            }
        }
        public double CalculateTRDay(int index, DateTime date)
        {
            List<int> IndexComponents = GetNewIndexComponents(index);//pega os papeis que compoe o indice.
            Dictionary<int, Dictionary<DateTime, double>> IndexWeights = GetNewIndexComponentsWeights(index);

            DateTime curDate = date;

            //FastData SourceTypeFD = new FastData(SourceType, date.AddDays(-10), date.AddDays(10), DataSource);
            double[] IndexArray = { (double)index };
            double[] Tickers = new double[IndexComponents.Count];

            for (int i = 0; i < IndexComponents.Count; i++)
            {
                Tickers[i] = (double)IndexComponents.ElementAt(i);
            }
            //pega todos os tickers
            Last.LoadTickers(Tickers);
            //SourceTypeFD.LoadTickers(Tickers);

            TR_IndexNest.LoadTickers(IndexArray);
            TR_IndexBBG.LoadTickers(IndexArray);

            /*if (index == 232216 || index == 232217)
            {
                DateTime prevDate = TR_IndexNest.PrevDate(date, 16307);
            }
            else
            {*/
            //DateTime prevDate = TR_IndexNest.PrevDate(date, index);
            //isso é uma coisa muito feia! Eu não deveria estar fazendo isso. Mas é por uma boa causa.

            //DateTime date = DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.AddDays(-3) : DateTime.Today.AddDays(-1);

            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
                DateTime prevDate = DateTime.Today.AddDays(-3);
            }
            else
            {
                DateTime prevDate = DateTime.Today.AddDays(-1);
            }

            //}
            double price = 0.0;
            double trDay = 0.0;
            String sql = "";
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
                            }
                        }
                    }
                }

                if (Weight != 0)
                {
                    sql = "SELECT SrValue FROM NESTSRV06.NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE SrDate = '" + date.Date.ToString("yyyy-MM-dd") + "' and SrType = 100 and IdSecurity = " + ticker;
                    using (newNestConn conn = new newNestConn(true))
                    {
                        price = conn.Return_Double(sql);
                    }
                    if (!Double.IsNaN(price))
                    {
                        trDay = trDay + (price * Weight);
                    }
                }
            }

            return trDay;
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
        public void Generate_Data(DateTime curDate)
        {
            //DateTime curDate = new DateTime(2006, 01, 01);
            


            string SQL1 = "";
            string SQL2 = "";
            string SQL3 = "";
            string SQL4 = "";
            string SQL5 = "";
            string SQL6 = "";
            string SQL7 = "";
            string SQL8 = "";
            string SQL9 = "";
            string SQL10 = "";
            string SQL11 = "";
            string SQL12 = "";
            string SQL13 = "";
            string SQL14 = "";
            string SQL15 = "";
            string SQL16 = "";
            string SQL17 = "";
            string SQL18 = "";

            /*                     
            int[] Tables = { 0 };
            int[] Tables2 = { 2 };   
            int[] Tables3 = { 0, 2 };

            lock (ScreenSync)
            {
                Ticker = "Generating VWAP Prices...";
            }

            ProcessVWap(440, 60, 30, curDate, true, Tables2, true);
            ProcessVWap(450, 90, 60, curDate, true, Tables2, true);
            ProcessVWap(460, 120, 90, curDate, true, Tables2, true);

            ProcessVWap(360, 30, 20, curDate, true, Tables);
            ProcessVWap(370, 45, 30, curDate, true, Tables);
            ProcessVWap(350, 1000, 90, curDate, true, Tables);             

            ProcessVWap(330, 30, 0, curDate, true, Tables3);
        */

            #region Data Generate Queries

            #region Queries for Yesterday

            String date = curDate.ToString();

            SQL1 =

            "declare @date datetime " +
            "   SET @date = '" + date +"' "+
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),361,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute <=30 and RegressiveMinute > 20 and date = @date " +
            "GROUP BY IdTicker,Date ";

            SQL2 =

            "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),360,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=30 and RegressiveMinute > 20 and date = @date " +
            "GROUP BY IdTicker,Date ";

            SQL3 =

             "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),371,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=45 and RegressiveMinute > 30 and date = @date " +
            "GROUP BY IdTicker,Date ";

            SQL4 =

             "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),370,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=45 and RegressiveMinute > 30 and date = @date " +
            "GROUP BY IdTicker,Date ";

            SQL5 =

             "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(VWAP*Volume)/SUM(Volume) AS VWAP,Date, getdate(),331,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=30 and date = @date " +
            "GROUP BY IdTicker,Date " +

            "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(VWAP*Volume)/SUM(Volume) AS VWAP,Date, getdate(),331,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=30 and date = @date " +
            "GROUP BY IdTicker,Date ";

            SQL6 =

           "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(Volume) AS VWAPVolume,Date, getdate(),330,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=30 and date = @date " +
            "GROUP BY IdTicker,Date " +

             "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(Volume) AS VWAPVolume,Date, getdate(),330,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=30 and date = @date " +
            "GROUP BY IdTicker,Date ";


            SQL7 =

            "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, PreAuction_Price, Date, getdate(),312,22,1 " +
            "FROM RTICKDB.dbo.CloseAuction  " +
            "WHERE date = @date ";

            SQL8 =

           "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, Volume, Date, getdate(),310,22,1 " +
            "FROM RTICKDB.dbo.CloseAuction  " +
            "WHERE  date = @date ";



            SQL9 =

          "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT A.IdTicker, SUM(VWAP*Volume)/SUM(Volume) AS VWAP,A.Date, getdate(),351,22,1 from " +
            "( " +
            "	SELECT SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP, SUM(convert(float,Volume)) as Volume, IdTicker, Date " +
            "		FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "	WHERE RegressiveMinute >= 90 and date = @date " +
            "	GROUP BY IdTicker,Date " +

            "	UNION " +

            "	SELECT Price as VWAP,convert(float,Volume),IdTicker,Date " +
            "		FROM RTICKDB.dbo.OpenAuction	 " +
            "	WHERE date = @date " +
            ") A " +
            "GROUP BY A.IdTicker,A.Date ";


            SQL10 =

             "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(convert(float,VWAPVolume)),Date, getdate(),350,22,1 FROM " +
            "( " +
            "	SELECT SUM(convert(float,Volume)) AS VWAPVolume,idticker,date " +
            "		FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "	WHERE RegressiveMinute >= 90 and date = @date " +
            "	GROUP BY IdTicker,Date " +

            "	UNION " +

            "	SELECT volume as VWAPVolume,idticker,date " +
            "		FROM RTICKDB.dbo.OpenAuction " +
            "	WHERE date = @date " +
            ") A " +
            "GROUP BY A.idticker,A.Date ";

            SQL11 =

            "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, MAX(High) ,Date, getdate(),380,22,1 FROM " +
            "( " +
            "	SELECT MAX(High) AS High,idticker,date " +
            "		FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "	WHERE RegressiveMinute >= 30 AND Date = @date " +
            "	GROUP BY IdTicker,Date " +
            " " +
            "	UNION " +
            " " +
            "	SELECT Price as High,idticker,date " +
            "		FROM RTICKDB.dbo.OpenAuction " +
            "	WHERE Date = @date " +
            ") A " +
            "GROUP BY A.idticker,A.Date ";

            SQL12 =

           "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [NESTSRV06].[NESTDB].[dbo].[Tb050_Preco_Acoes_Onshore]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, MIN(Low) ,Date, getdate(),381,22,1 FROM " +
            "( " +
            "	SELECT MIN(Low) AS Low ,idticker,date " +
            "		FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "	WHERE RegressiveMinute >= 30 AND Date = @date " +
            "	GROUP BY IdTicker,Date " +
            " " +
            "	UNION " +
            " " +
            "	SELECT Price as Low,idticker,date " +
            "		FROM RTICKDB.dbo.OpenAuction " +
            "	WHERE Date = @date " +
            ") A " +
            "GROUP BY A.idticker,A.Date ";

            SQL13 =

            "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),421,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=60 and date = @date " +
            "GROUP BY IdTicker,Date ";

            SQL14 =

           "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),420,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=60 and date = @date " +
            "GROUP BY IdTicker,Date ";

            SQL15 =

             "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),441,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=60 and RegressiveMinute>30 and date = @date " +
            "GROUP BY IdTicker,Date ";

            SQL16 =

             "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),440,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=60 and RegressiveMinute>30 and date = @date " +
            "GROUP BY IdTicker,Date ";

            SQL17 =

            "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(VWAP*Volume)/SUM(convert(float,Volume)) AS VWAP,Date, getdate(),451,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=90 and RegressiveMinute>60  date = @date " +
            "GROUP BY IdTicker,Date ";

            SQL18 =
           "declare @date datetime " +
            "   SET @date = '" + date + "' " +
            "INSERT INTO [RTICKDB].[dbo].[Tb001_Precos_RTICK]([IdSecurity],[SrValue],[SrDate],[InsertDate],[SrType],[Source],[Automated]) " +
            "SELECT IdTicker, SUM(convert(float,Volume)) AS VWAPVolume,Date, getdate(),450,22,1 " +
            "FROM RTICKDB.dbo.IntradayOneMinuteBars " +
            "WHERE RegressiveMinute<=90 and RegressiveMinute>60 and date = @date " +
            "GROUP BY IdTicker,Date ";

            
            #endregion


            #region Execute Queries

            using (newNestConn Conn = new newNestConn(true))
            {
                int result;


                

                result = Conn.ExecuteNonQuery(SQL1);
                if (result != 1)
                {
                    int i = 0;
                }

               

                result = Conn.ExecuteNonQuery(SQL2);
                if (result != 1)
                {
                    int i = 0;
                }

               

                result = Conn.ExecuteNonQuery(SQL3);
                if (result != 1)
                {
                    int i = 0;
                }

               

                result = Conn.ExecuteNonQuery(SQL4);
                if (result != 1)
                {
                    int i = 0;
                }

               

                result = Conn.ExecuteNonQuery(SQL5);
                if (result != 1)
                {
                    int i = 0;
                }

          

                result = Conn.ExecuteNonQuery(SQL6);
                if (result != 1)
                {
                    int i = 0;
                }

             

                result = Conn.ExecuteNonQuery(SQL7);
                if (result != 1)
                {
                    int i = 0;
                }

      

                result = Conn.ExecuteNonQuery(SQL8);
                if (result != 1)
                {
                    int i = 0;
                }

             

                result = Conn.ExecuteNonQuery(SQL9);
                if (result != 1)
                {
                    int i = 0;
                }

             

                result = Conn.ExecuteNonQuery(SQL10);
                if (result != 1)
                {
                    int i = 0;
                }

             

                result = Conn.ExecuteNonQuery(SQL11);
                if (result != 1)
                {
                    int i = 0;
                }

            

                result = Conn.ExecuteNonQuery(SQL12);
                if (result != 1)
                {
                    int i = 0;
                }

           

                result = Conn.ExecuteNonQuery(SQL13);
                if (result != 1)
                {
                    int i = 0;
                }

        

                result = Conn.ExecuteNonQuery(SQL14);
                if (result != 1)
                {
                    int i = 0;
                }

              

                result = Conn.ExecuteNonQuery(SQL15);
                if (result != 1)
                {
                    int i = 0;
                }

              

                result = Conn.ExecuteNonQuery(SQL16);
                if (result != 1)
                {
                    int i = 0;
                }

             

                result = Conn.ExecuteNonQuery(SQL17);
                if (result != 1)
                {
                    int i = 0;
                }

               

                result = Conn.ExecuteNonQuery(SQL18);
                if (result != 1)
                {
                    int i = 0;
                }

            #endregion

            }

            #endregion


        }
    }

}
