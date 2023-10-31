using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NestDLL;
using NCommonTypes;

namespace JMStrategy
{
    public class JMModel
    {
        #region Model Parameters

        private int _IdModel;
        private int _IdSection;
        private string _Section;
        private double _TriggerIN;
        private double _TriggerOUT;        
        private int _Days;
        private double _Multiplier;
        private double _Limiter;
        private double _CDI;
        private double _StrategyPL;
        private DateTime _DateRef;

        public int IdModel
        {
            get { return _IdModel; }
            set { _IdModel = value; }
        }
        public int IdSection
        {
            get { return _IdSection; }
            set { _IdSection = value; _Section = GetSectionName(); }
        }
        public string Section
        {
            get { return _Section; }
        }
        public double TriggerIN
        {
            get { return _TriggerIN; }
            set { _TriggerIN = value; }
        }
        public double TriggerOUT
        {
            get { return _TriggerOUT; }
            set { _TriggerOUT = value; }
        }
        public double Size
        {
            get { return (MaxLoss / 5000) * 50000 / _Multiplier; }            
        }
        public int Days
        {
            get { return _Days; }
            set { _Days = value; }
        }
        public double Multiplier
        {
            get { return _Multiplier; }
            set { _Multiplier = value; }
        }
        public double Limiter
        {
            get { return _Limiter; }
            set { _Limiter = value; }
        }
        public double CDI
        {
            get { return _CDI; }
            set { _CDI = value; }
        }
        public double StrategyPL
        {
            get { return _StrategyPL; }
            set { _StrategyPL = value; }
        }
        public double MaxLoss
        {
            get
            {
                double dayCDI = Math.Pow((1.0 + _CDI), (1.0 / 252.0)) - 1;
                double loss = _StrategyPL * dayCDI;

                return loss;
            }
        }
        public DateTime DateRef
        {
            get { return _DateRef; }
            set { _DateRef = value; }
        }

        #endregion

        #region Attributes

        public List<JMPairs> PairList = new List<JMPairs>();
        private SortedDictionary<int, List<int>> PairListIndex = new SortedDictionary<int, List<int>>();

        private volatile object PairSync = new object();

        private List<JMSecurities> SecurityList = new List<JMSecurities>();
        private SortedDictionary<string, int> SecListIndex = new SortedDictionary<string, int>();

        private volatile object SecListSync = new object();        

        #endregion  

        public event EventHandler OnAction;
        public event EventHandler OnClosedSec;

        public List<JMOrder> Orders = new List<JMOrder>();
        private SortedDictionary<int, int> OrdersIndex = new SortedDictionary<int, int>();
        public volatile object SyncOrders = new object();

        public JMModel()
        {                     
        }        

        public void LoadPairList()
        {
            string SQLExpression = " SELECT	A.*, " +
                                   "        B.EXCHANGETICKER AS TICKER1," +
                                   " 		C.EXCHANGETICKER AS TICKER2" +
                                   " FROM NESTSIM.dbo.TB701_JM_PAIRS (NOLOCK) A" +
                                   " JOIN NESTSRV06.NESTDB.DBO.TB001_SECURITIES B WITH(NOLOCK)" +
                                   " ON A.IDSECURITY1 = B.IDSECURITY" +
                                   " JOIN NESTSRV06.NESTDB.DBO.TB001_SECURITIES C WITH(NOLOCK)" +
                                   " ON A.IDSECURITY2 = C.IDSECURITY" +
                                   " WHERE IDSECTION = " + _IdSection.ToString() +
                                   " AND INIDATE <= '" + DateRef.ToString("yyyyMMdd") + "'" +
                                   " AND (ENDDATE >= '" + DateRef.ToString("yyyyMMdd") + "' OR ENDDATE IS NULL)";

            DataTable activePairs = new DataTable();

            using (newNestConn curConn = new newNestConn(true))
            {
                activePairs = curConn.Return_DataTable(SQLExpression);
            }

            foreach (DataRow dr in activePairs.Rows)
            {
                string Ticker1 = dr["TICKER1"].ToString();
                string Ticker2 = dr["TICKER2"].ToString();

                if (!SecListIndex.ContainsKey(Ticker1))
                {
                    int IdSecurity1 = int.Parse(dr["IDSECURITY1"].ToString());

                    JMSecurities Security1 = new JMSecurities(IdSecurity1, Ticker1, this);
                    SecurityList.Add(Security1);

                    UpdateSecListIndex();
                }

                if (!SecListIndex.ContainsKey(Ticker2))
                {
                    int IdSecurity2 = int.Parse(dr["IDSECURITY2"].ToString());

                    JMSecurities Security2 = new JMSecurities(IdSecurity2, Ticker2, this);
                    SecurityList.Add(Security2);

                    UpdateSecListIndex();
                }

                JMPairs curPair = new JMPairs(SecurityList[SecListIndex[Ticker1]],
                                              SecurityList[SecListIndex[Ticker2]],
                                              int.Parse(dr["IDPAIR"].ToString()),
                                              this);

                curPair.OnAction += new EventHandler(curPair_OnAction);
                curPair.OnClosedSec += new EventHandler(curPair_OnClosedSec);

                PairList.Add(curPair);

                UpdatePairListIndex();
            }        
        }

        public void LoadBTPairList()
        {
            List<PairConfig> curList = JMData.Instance.GetPairs(IdSection, DateRef);

            foreach (PairConfig curPair in curList)
            {
                CreatePair(curPair.IdPair, curPair.IdSecurity1, curPair.IdSecurity2, curPair.Ticker1, curPair.Ticker2);
            }
        }

        void CreatePair(int idPair, int idSec1, int idSec2, string Sec1Ticker, string Sec2Ticker)
        {
            string Ticker1 = Sec1Ticker;
            string Ticker2 = Sec2Ticker;

            if (!SecListIndex.ContainsKey(Ticker1))
            {
                int IdSecurity1 = idSec1;

                JMSecurities Security1 = new JMSecurities(IdSecurity1, Ticker1, this);
                SecurityList.Add(Security1);

                UpdateSecListIndex();
            }

            if (!SecListIndex.ContainsKey(Ticker2))
            {
                int IdSecurity2 = idSec2;

                JMSecurities Security2 = new JMSecurities(IdSecurity2, Ticker2, this);
                SecurityList.Add(Security2);

                UpdateSecListIndex();
            }

            JMPairs curPair = new JMPairs(SecurityList[SecListIndex[Ticker1]],
                                          SecurityList[SecListIndex[Ticker2]],
                                          idPair,
                                          this);

            curPair.OnAction += new EventHandler(curPair_OnAction);
            curPair.OnClosedSec += new EventHandler(curPair_OnClosedSec);

            PairList.Add(curPair);

            UpdatePairListIndex();
        }

        void curPair_OnClosedSec(object sender, EventArgs e)
        {
            if (OnClosedSec != null)
            {
                OnClosedSec(sender, e);
            }
        }

        void curPair_OnAction(object sender, EventArgs e)
        {
            //UpdateOrders();

            if (OnAction != null)
            {
                OnAction(this, null);
            }
        }        

        private void UpdateSecListIndex()
        {
            lock (SecListSync)
            {
                SecListIndex.Clear();

                for (int i = 0; i < SecurityList.Count; i++)
                {
                    SecListIndex.Add(SecurityList[i].Ticker, i);
                }
            }
        }

        private void UpdatePairListIndex()
        {
            lock (PairSync)
            {
                PairListIndex.Clear();

                for (int i = 0; i < PairList.Count; i++)
                {
                    int IdSecurity1 = PairList[i].Security1.IdSecurity;
                    int IdSecurity2 = PairList[i].Security2.IdSecurity;

                    if (!PairListIndex.ContainsKey(IdSecurity1))
                    {
                        PairListIndex.Add(IdSecurity1, new List<int>());
                    }

                    if (!PairListIndex.ContainsKey(IdSecurity2))
                    {
                        PairListIndex.Add(IdSecurity2, new List<int>());
                    }

                    PairListIndex[IdSecurity1].Add(i);
                    PairListIndex[IdSecurity2].Add(i);
                }
            }
        }

        public void InitializeModel()
        {            
            foreach (JMSecurities curSec in SecurityList)
            {
                curSec.InitializeSecurity();
            }
        }

        public string GetSectionName()
        {
            string secName = "";

            string SQLExpression = "SELECT SECTION FROM NESTSRV06.NESTDB.DBO.TB404_SECTION WITH(NOLOCK) WHERE ID_SECTION = " + _IdSection.ToString();

            using (newNestConn curConn = new newNestConn(true))
            {
                secName = curConn.Execute_Query_String(SQLExpression);
            }

            return secName;
        }

        public override string ToString()
        {
            return this.Section + "\t" + this.IdModel;
        }

        public void UpdateOrders()
        {
            lock (JMData.Instance.MainSync)
            {
                Orders.Clear();
                OrdersIndex.Clear();

                foreach (JMPairs curPair in PairList)
                {
                    lock (curPair.SyncOrders)
                    {
                        if (!curPair.PairBlocked)
                        {
                            if (curPair.Sec1Action != 0)
                            {
                                if (_Section == "JM Transportation")
                                {
                                    int a = 0;
                                }
                                int IdOrder1 = 0;
                                if (!OrdersIndex.TryGetValue(curPair.Security1.IdSecurity, out IdOrder1))
                                {
                                    JMOrder newOrder = new JMOrder();
                                    newOrder.IdSection = _IdSection;
                                    newOrder.Section = Section;
                                    newOrder.IdSecurity = curPair.Security1.IdSecurity;
                                    newOrder.Ticker = curPair.Security1.Ticker;
                                    newOrder.Quantity = 0;

                                    Orders.Add(newOrder);

                                    UpdateOrdersIndex();

                                    IdOrder1 = OrdersIndex[newOrder.IdSecurity];
                                }

                                Orders[IdOrder1].Quantity += curPair.Sec1Action;
                                Orders[IdOrder1].refPairs.Add(curPair);
                            }

                            if (curPair.Sec2Action != 0)
                            {
                                int IdOrder2 = 0;
                                if (!OrdersIndex.TryGetValue(curPair.Security2.IdSecurity, out IdOrder2))
                                {
                                    JMOrder newOrder = new JMOrder();
                                    newOrder.IdSection = _IdSection;
                                    newOrder.Section = Section;
                                    newOrder.IdSecurity = curPair.Security2.IdSecurity;
                                    newOrder.Ticker = curPair.Security2.Ticker;
                                    newOrder.Quantity = 0;

                                    Orders.Add(newOrder);

                                    UpdateOrdersIndex();

                                    IdOrder2 = OrdersIndex[newOrder.IdSecurity];
                                }

                                Orders[IdOrder2].Quantity += curPair.Sec2Action;
                                Orders[IdOrder2].refPairs.Add(curPair);
                            }
                        }
                    }
                }
            }
        }

        private void UpdateOrdersIndex()
        {
            OrdersIndex.Clear();

            for (int i = 0; i < Orders.Count; i++)
            {
                OrdersIndex.Add(Orders[i].IdSecurity, i);
            }
        }
    }
}
