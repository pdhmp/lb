using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;

namespace NestQuant.Common
{
    public class Trade
    {
        private FIXConn fixConn;

        private Mutex tradeListMutex = new Mutex();
        
        public struct TradeInfo
        {
            public double PercPosition;
            public double CashPosition;
            public double LastPrice;            
            public int Quantity;
            public int Current;
            public int Difference;
        }

        public PercPositions_Table tkPositions;
        public SortedDictionary<int, TradeInfo> TradeList;
        private int _ID_BOOK = 2;

        public int ID_BOOK
        {
            get { return _ID_BOOK; }
            set { _ID_BOOK = value; }
        }
	

        double _Cash;

        public Trade()
        {
        }

        public Trade(PercPositions_Table _tkPositions, double cash)
        {
            tkPositions = _tkPositions;
            _Cash = cash;

            fixConn = new FIXConn(@"C:\Projetos_Testes\FIX\FIXInitiator\FIXInitiator\config\QuickFix.cfg");
            Generate_Trade_List();
        }

        public void Generate_Trade_List()
        {
            TradeList = new SortedDictionary<int, TradeInfo>();

            int[] tickerList = new int[tkPositions.ValueColumnCount];

            int LastDateIdx = tkPositions.DateRowCount - 1;

            for (int i = 0; i < tkPositions.ValueColumnCount; i++)
            {
                int TickerID = tkPositions.GetValueColumnID(i);
                TradeInfo tInfo = new TradeInfo();

                tInfo.PercPosition = tkPositions.GetValue(LastDateIdx, i);
                tInfo.CashPosition = tInfo.PercPosition * _Cash;

                TradeList.Add(TickerID, tInfo);

                tickerList[i] = TickerID;
            }
            Dictionary<int, double> LastPriceList = Get_LastPrice(tickerList);
            Dictionary<int, int> CurrentPositionList = Get_Current_Position(tickerList);

            foreach (KeyValuePair<int, int> currentPosition in CurrentPositionList)
            {
                TradeInfo tInfo = new TradeInfo();
                tInfo.PercPosition = TradeList[currentPosition.Key].PercPosition;
                tInfo.CashPosition = TradeList[currentPosition.Key].CashPosition;
                tInfo.LastPrice = LastPriceList[currentPosition.Key];
                tInfo.Quantity = (int)Math.Truncate(tInfo.CashPosition / tInfo.LastPrice);
                tInfo.Current = currentPosition.Value;
                tInfo.Difference = tInfo.Quantity - tInfo.Current;

                TradeList[currentPosition.Key] = tInfo;
            }           
        }

        public Dictionary<int,double> Get_LastPrice(int[] tickerList)
        {
            Dictionary<int, double> priceList = new Dictionary<int, double>();

            string tickers = "";
            int i = 0;            
            for (; i < tickerList.Length - 1; i++)
            {
                tickers = tickers + tickerList[i].ToString() + ",";
            }
            tickers = tickers + tickerList[i].ToString();

            string SQLExpression = "SELECT A.ID_ATIVO, " +
                                   "NESTDB.DBO.FCN_GET_PRICE_Value_Only(A.Id_Ativo,'"+DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff")+"',1,0,2,0,0) AS VALOR " +
                                   "FROM NESTDB.DBO.TB001_ATIVOS A " +
                                   "WHERE ID_ATIVO IN ("+tickers+") ";

            DataTable dt;
            using (NestConn conn = new NestConn())
            {
               dt = conn.ExecuteDataTable(SQLExpression);
            }

            foreach (DataRow dr in dt.Rows)
            {
                priceList.Add(Convert.ToInt16(dr["ID_ATIVO"].ToString()),Convert.ToDouble(dr["VALOR"].ToString()));
            }
            
            return priceList;
        }

        public Dictionary<int, int> Get_Current_Position(int[] tickerList)
        {
            Dictionary<int, int> positionList = new Dictionary<int, int>();
            
            string tickers = "";
            int i = 0;
            for (; i < tickerList.Length - 1; i++)
            {
                tickers = tickers + tickerList[i].ToString() + ",";
            }
            tickers = tickers + tickerList[i].ToString();

            string SQLExpression = "SELECT [ID TICKER], SUM([POSITION]) AS VALOR " +
                                   "FROM NESTRT.DBO.TB000_POSICAO_ATUAL " +
                                   "WHERE [ID BOOK] = " + ID_BOOK + " " +
                                   "AND [ID TICKER] IN (" + tickers + ") " +
                                   "GROUP BY [ID TICKER], [TICKER] " +
                                   "ORDER BY [ID TICKER]";

            DataTable dt;
            using (NestConn conn = new NestConn())
            {
                dt = conn.ExecuteDataTable(SQLExpression);
            }

            foreach (DataRow dr in dt.Rows)
            {
                positionList.Add(Convert.ToInt32(dr["ID TICKER"].ToString()), Convert.ToInt32(dr["VALOR"].ToString()));
            }

            return positionList;
        }

        public DataTable ToDataTable()
        {
            DataTable dt = new DataTable("TradeList");

            dt.Columns.Add("ID_TICKER", Type.GetType("System.Int32"));
            dt.Columns.Add("PERCPOSITION", Type.GetType("System.Double"));
            dt.Columns.Add("CASHPOSITION", Type.GetType("System.Double"));
            dt.Columns.Add("LASTPRICE", Type.GetType("System.Double"));
            dt.Columns.Add("QUANTITY", Type.GetType("System.Int32"));
            dt.Columns.Add("CURRENT", Type.GetType("System.Int32"));
            dt.Columns.Add("DIFFERENCE", Type.GetType("System.Int32"));

            foreach (KeyValuePair<int, TradeInfo> trade in TradeList)
            {
                DataRow dr = dt.NewRow();

                dr["ID_TICKER"] = trade.Key;
                dr["PERCPOSITION"] = trade.Value.PercPosition;
                dr["CASHPOSITION"] = trade.Value.CashPosition;
                dr["LASTPRICE"] = trade.Value.LastPrice;
                dr["QUANTITY"] = trade.Value.Quantity;
                dr["CURRENT"] = trade.Value.Current;
                dr["DIFFERENCE"] = trade.Value.Difference;

                dt.Rows.Add(dr);
            }

            return dt;
        }

        public void sendOrders()
        {
            try
            {
                fixConn.ReceivedFill += new EventHandler(fixConn_ReceivedFill);

                tradeListMutex.WaitOne();

                foreach (KeyValuePair<int, TradeInfo> order in TradeList)
                {
                    if (order.Value.Difference != 0)
                    {
                        fixConn.sendOrder(order.Key, order.Value.Difference, -1.0);
                    }
                }

                tradeListMutex.ReleaseMutex();
            }
            catch (Exception e)
            {
                throw new System.NotImplementedException();
            }
        }

        void fixConn_ReceivedFill(object sender, EventArgs e)
        {           

            TradeInfo tInfo = new TradeInfo();
            ExecutionFillArgs fillArgs = (ExecutionFillArgs)e;

            tInfo = TradeList[fillArgs.ID_Ticker];

            tInfo.Current += fillArgs.LastShares;
            tInfo.Difference = tInfo.Quantity - tInfo.Current;

            tradeListMutex.WaitOne();
            TradeList[fillArgs.ID_Ticker] = tInfo;
            tradeListMutex.ReleaseMutex();
        }        
    }
}
