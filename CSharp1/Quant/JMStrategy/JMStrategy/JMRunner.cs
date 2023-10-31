using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

using NestDLL;
using NFIXConn;
using NCommonTypes;

namespace JMStrategy
{
    class JMRunner
    {
        public List<JMModel> Models = new List<JMModel>();

        public List<JMOrder> GridOrders = new List<JMOrder>();
        public List<JMOrder> Orders = new List<JMOrder>();
        public volatile object SyncOrders = new object();
        public SortedDictionary<int, SortedDictionary<int, int>> OrdersIndex = new SortedDictionary<int, SortedDictionary<int, int>>();

        public List<JMClosedSec> ClosedSec = new List<JMClosedSec>();
        private volatile object SyncClSec = new object();
        public SortedDictionary<int, SortedDictionary<int, int>> ClSecIndex = new SortedDictionary<int, SortedDictionary<int, int>>();

        private DateTime DateRef;

        private FIXConn curFixConn;
        private SortedDictionary<string, JMPairs> orderPair = new SortedDictionary<string, JMPairs>();
        private volatile object SentOrderSync = new object();

        public JMRunner(DateTime _DateRef)
        {
            DateRef = _DateRef;

            if (DateRef == DateTime.Today)
            {
                curFixConn = new FIXConn(@"T:\Log\FIX\JM\config\JMConfig.cfg");
                curFixConn.ReceivedFill += new EventHandler(curFixConn_ReceivedFill);
                 
            }
        }

        void curFixConn_ReceivedFill(object sender, EventArgs e)
        {
            lock (SentOrderSync)
            {
                ExecutionFillArgs exec = (ExecutionFillArgs)e;

                JMPairs curPair;

                if (orderPair.TryGetValue(exec.ClOrdID, out curPair))
                {
                    curPair.UpdateExecQty(exec.ID_Ticker, exec.LastShares);
                }
            }
        }

        public void LoadModels()
        {
            string SQLExpression = "";

            if (DateRef == DateTime.Today)
            {
                SQLExpression = "SELECT * FROM NESTSIM.DBO.TB700_JM_MODELS (NOLOCK) WHERE INIDATE <= '" + DateRef.ToString("yyyyMMdd") + "' " +
                                "AND (ENDDATE >= '" + DateRef.ToString("yyyyMMdd") + "' OR ENDDATE IS NULL)";
            }
            else
            {
                SQLExpression = "SELECT * FROM NESTSIM.DBO.TB705_JM_BT_MODELS (NOLOCK) WHERE INIDATE <= '" + DateRef.ToString("yyyyMMdd") + "' " +
                                "AND (ENDDATE >= '" + DateRef.ToString("yyyyMMdd") + "' OR ENDDATE IS NULL)";
            }

            DataTable activeModels = new DataTable();

            using (newNestConn curConn = new newNestConn(true))
            {
                activeModels = curConn.Return_DataTable(SQLExpression);
            }

            foreach (DataRow dr in activeModels.Rows)
            {
                JMModel curModel = new JMModel();
                curModel.IdModel = int.Parse(dr["IDMODEL"].ToString());
                curModel.IdSection = int.Parse(dr["IDSECTION"].ToString());
                curModel.TriggerIN = double.Parse(dr["TRIGGERIN"].ToString());
                curModel.TriggerOUT = double.Parse(dr["TRIGGEROUT"].ToString());
                curModel.Days = int.Parse(dr["DAYS"].ToString());
                curModel.Multiplier = double.Parse(dr["MULTIPLIER"].ToString());
                curModel.Limiter = double.Parse(dr["LIMITER"].ToString());
                curModel.CDI = JMData.Instance.LastPrice.GetValue(JMUtils.IdSecurityCDI, DateRef, false)[1] / 100;
                //TODO: Alterar PL
                curModel.StrategyPL = 15000000;
                curModel.DateRef = DateRef;

                if (DateRef == DateTime.Today)
                {
                    curModel.LoadPairList();
                }
                else
                {
                    curModel.LoadBTPairList();
                }

                curModel.OnAction += new EventHandler(curModel_OnAction);
                curModel.OnClosedSec += new EventHandler(curModel_OnClosedSec);

                Models.Add(curModel);
            }

            if (DateRef == DateTime.Today)
            {
                JMData.Instance.SubscribeTickers();
            }
        }

        private void curModel_OnClosedSec(object sender, EventArgs e)
        {
            UpdateClosedSec((JMClosedSec)e);
        }

        private void curModel_OnAction(object sender, EventArgs e)
        {
            //UpdateOrders();
        }

        public void InitializeModels()
        {
            foreach (JMModel curModel in Models)
            {
                curModel.InitializeModel();
            }
        }

        private void UpdateOrders()
        {
            lock (JMData.Instance.MainSync)
            {
                Orders.Clear();
                OrdersIndex.Clear();

                foreach (JMModel curModel in Models)
                {
                    lock (curModel.SyncOrders)
                    {
                        foreach (JMOrder curOrder in curModel.Orders)
                        {
                            bool orderFound = false;

                            if (OrdersIndex.ContainsKey(curOrder.IdSection))
                            {
                                if (OrdersIndex[curOrder.IdSection].ContainsKey(curOrder.IdSecurity))
                                {
                                    Orders[OrdersIndex[curOrder.IdSection][curOrder.IdSecurity]].Quantity += curOrder.Quantity;

                                    foreach (JMPairs curpair in curOrder.refPairs)
                                    {
                                        Orders[OrdersIndex[curOrder.IdSection][curOrder.IdSecurity]].refPairs.Add(curpair);
                                    }

                                    orderFound = true;
                                }
                            }

                            if (!orderFound)
                            {
                                Orders.Add(curOrder);
                                UpdateOrdersIndex();
                            }
                        }
                    }
                }

                GridOrders = Orders;
                Orders = new List<JMOrder>();
            }
        }

        public void UpdateOrders2()
        {
            foreach (JMModel curModel in Models)
            {
                curModel.UpdateOrders();
            }

            UpdateOrders();
        }

        private void UpdateOrdersIndex()
        {
            OrdersIndex.Clear();

            for (int i = 0; i < Orders.Count; i++)
            {
                if (!OrdersIndex.ContainsKey(Orders[i].IdSection))
                {
                    OrdersIndex.Add(Orders[i].IdSection, new SortedDictionary<int, int>());
                }

                OrdersIndex[Orders[i].IdSection].Add(Orders[i].IdSecurity, i);
            }
        }

        private void UpdateClosedSec(JMClosedSec closedPair)
        {
            bool found = false;
            int idModel = closedPair.refPair.ParentModel.IdModel;
            int idPair = closedPair.refPair.IdPair;

            lock (SyncClSec)
            {               
                if (ClSecIndex.ContainsKey(idModel))
                {
                    if (ClSecIndex[idModel].ContainsKey(idPair))
                    {
                        ClosedSec[ClSecIndex[idModel][idPair]] = closedPair;
                        found = true;
                    }
                }

                if (!found)
                {
                    ClosedSec.Add(closedPair);
                    UpdateClSecIndex();
                }            
            }        
        }

        private void UpdateClSecIndex()
        {
            ClSecIndex.Clear();

            for (int i = 0; i < ClosedSec.Count; i++)
            {
                int idModel = ClosedSec[i].refPair.ParentModel.IdModel;
                int idPair = ClosedSec[i].refPair.IdPair;

                if (!ClSecIndex.ContainsKey(idModel))
                {
                    ClSecIndex.Add(idModel, new SortedDictionary<int, int>());
                }

                ClSecIndex[idModel].Add(idPair, i);
            }
        }

        public void SendOrders()
        {
            lock (JMData.Instance.MainSync)
            {
                foreach (JMOrder curOrder in GridOrders)
                {
                    if (true)
                    {
                        List<JMPairs> BuyPairs = new List<JMPairs>();
                        List<JMPairs> SellPairs = new List<JMPairs>();

                        foreach (JMPairs curPair in curOrder.refPairs)
                        {
                            int refSec = (curPair.Security1.IdSecurity == curOrder.IdSecurity ? 1 : 2);
                            int curQty = (refSec == 1 ? curPair.Sec1Action : curPair.Sec2Action);

                            if (curQty > 0)
                            {
                                while (SellPairs.Count > 0 && curQty > 0)
                                {
                                    JMPairs matchPair = SellPairs[0];

                                    int mpRefSec = (matchPair.Security1.IdSecurity == curOrder.IdSecurity ? 1 : 2);
                                    int mpQty = (-1) * (mpRefSec == 1 ? matchPair.Sec1Action : matchPair.Sec2Action);
                                    int matchQty = Math.Min(curQty, mpQty);

                                    MatchPairs(curPair, matchPair, curOrder.IdSecurity, matchQty);

                                    curQty -= matchQty;

                                    int newMpQty = (-1) * (mpRefSec == 1 ? matchPair.Sec1Action : matchPair.Sec2Action);

                                    if (newMpQty == 0)
                                    {
                                        SellPairs.RemoveAt(0);
                                    }
                                }

                                if (curQty > 0)
                                {
                                    BuyPairs.Add(curPair);
                                }
                            }
                            else if (curQty < 0)
                            {
                                while (BuyPairs.Count > 0 && curQty < 0)
                                {
                                    JMPairs matchPair = BuyPairs[0];

                                    int mpRefSec = (matchPair.Security1.IdSecurity == curOrder.IdSecurity ? 1 : 2);
                                    int mpQty = (-1) * (mpRefSec == 1 ? matchPair.Sec1Action : matchPair.Sec2Action);
                                    int matchQty = Math.Max(curQty, mpQty);

                                    MatchPairs(curPair, matchPair, curOrder.IdSecurity, matchQty);

                                    curQty -= matchQty;

                                    int newMpQty = (-1) * (mpRefSec == 1 ? matchPair.Sec1Action : matchPair.Sec2Action);

                                    if (newMpQty == 0)
                                    {
                                        BuyPairs.RemoveAt(0);
                                    }
                                }

                                if (curQty < 0)
                                {
                                    SellPairs.Add(curPair);
                                }
                            }
                        }

                        if (BuyPairs.Count > 0 && SellPairs.Count == 0)
                        {
                            foreach (JMPairs curpair in BuyPairs)
                            {
                                SendSingleOrder(curpair, curOrder.IdSecurity);
                            }
                        }
                        else if (BuyPairs.Count == 0 && SellPairs.Count > 0)
                        {
                            foreach (JMPairs curpair in SellPairs)
                            {
                                SendSingleOrder(curpair, curOrder.IdSecurity);
                            }
                        }
                        else if (BuyPairs.Count != 0 && SellPairs.Count != 0)
                        {
                            throw new Exception("Inconsistent match");
                        }
                    }
                }
            }

            /*foreach (JMModel curModel in Models)
            {
                curModel.UpdateOrders();
            }

            UpdateOrders();*/
        }

        public void SendOrdersBT()
        {
            StreamWriter sw = new StreamWriter(@"C:\QUANT\STRATEGIES\JM\JM_BT_POSITION_" + DateRef.ToString("yyyyMMdd") + ".txt");

            foreach (JMModel curModel in Models)
            {
                foreach (JMPairs curPair in curModel.PairList)
                {
                    string posLine = curModel.IdModel.ToString() + (char)16 +
                                     curPair.IdPair.ToString() + (char)16 +
                                     curPair.Sec1CurPos.ToString() + (char)16 +
                                     curPair.Sec2CurPos.ToString();
                                        

                    sw.WriteLine(posLine);
                }
            }

            sw.Close();
        }

        private void MatchPairs(JMPairs curPair, JMPairs matchPair, int IdSecurity, int matchQty)
        {
            curPair.UpdateSentQty(IdSecurity, matchQty);
            curPair.UpdateExecQty(IdSecurity, matchQty);

            matchPair.UpdateSentQty(IdSecurity, (-1) * matchQty);
            matchPair.UpdateExecQty(IdSecurity, (-1) * matchQty);
        }

        private void SendSingleOrder(JMPairs curPair, int IdSecurity)
        {
            lock (SentOrderSync)
            {
                int qty = 0;

                if (curPair.Security1.IdSecurity == IdSecurity)
                {
                    qty = curPair.Sec1Action;
                }
                else if (curPair.Security2.IdSecurity == IdSecurity)
                {
                    qty = curPair.Sec2Action;
                }
                else
                {
                    int a = 0;
                }

                if (DateRef == DateTime.Today)
                {
                    string ClOrdID = curFixConn.sendOrder(IdSecurity, qty, -2, 18, 13, curPair.ParentModel.IdSection, true);
                    orderPair.Add(ClOrdID, curPair);
                    curPair.UpdateSentQty(IdSecurity, qty);
                }
                else
                {
                    curPair.UpdateSentQty(IdSecurity, qty);
                    curPair.UpdateExecQty(IdSecurity, qty);
                }                
            }
        }

        public void Send100Sell()
        {
            curFixConn.sendOrder(86, -100, -2, 18, 13, 186, true);
            curFixConn.sendOrder(89, -100, -2, 18, 13, 186, true);
            curFixConn.sendOrder(444, -100, -2, 18, 13, 186, true);
            curFixConn.sendOrder(14960, -100, -2, 18, 13, 186, true);
            curFixConn.sendOrder(1074, -100, -2, 18, 13, 188, true);
            curFixConn.sendOrder(1237, -100, -2, 18, 13, 188, true);
            curFixConn.sendOrder(1695, -100, -2, 18, 13, 188, true);
            curFixConn.sendOrder(23332, -100, -2, 18, 13, 188, true);
            curFixConn.sendOrder(27903, -100, -2, 18, 13, 188, true);
            curFixConn.sendOrder(279, -100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(382, -100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(654, -100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1075, -100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1077, -100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1084, -100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1231, -100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1411, -100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1687, -100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(456, -100, -2, 18, 13, 190, true);
            curFixConn.sendOrder(710, -100, -2, 18, 13, 190, true);
            curFixConn.sendOrder(809, -100, -2, 18, 13, 190, true);
            curFixConn.sendOrder(260, -100, -2, 18, 13, 191, true);
            curFixConn.sendOrder(384, -100, -2, 18, 13, 191, true);
            curFixConn.sendOrder(801, -100, -2, 18, 13, 191, true);
            curFixConn.sendOrder(29, -100, -2, 18, 13, 193, true);
            curFixConn.sendOrder(185, -100, -2, 18, 13, 193, true);
            curFixConn.sendOrder(393, -100, -2, 18, 13, 193, true);
            curFixConn.sendOrder(715, -100, -2, 18, 13, 193, true);
            curFixConn.sendOrder(30187, -100, -2, 18, 13, 193, true);

        }

        public void Send100Buy()
        {
            curFixConn.sendOrder(86, 100, -2, 18, 13, 186, true);
            curFixConn.sendOrder(89, 100, -2, 18, 13, 186, true);
            curFixConn.sendOrder(444, 100, -2, 18, 13, 186, true);
            curFixConn.sendOrder(14960, 100, -2, 18, 13, 186, true);
            curFixConn.sendOrder(1074, 100, -2, 18, 13, 188, true);
            curFixConn.sendOrder(1237, 100, -2, 18, 13, 188, true);
            curFixConn.sendOrder(1695, 100, -2, 18, 13, 188, true);
            curFixConn.sendOrder(23332, 100, -2, 18, 13, 188, true);
            curFixConn.sendOrder(27903, 100, -2, 18, 13, 188, true);
            curFixConn.sendOrder(279, 100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(382, 100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(654, 100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1075, 100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1077, 100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1084, 100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1231, 100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1411, 100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(1687, 100, -2, 18, 13, 189, true);
            curFixConn.sendOrder(456, 100, -2, 18, 13, 190, true);
            curFixConn.sendOrder(710, 100, -2, 18, 13, 190, true);
            curFixConn.sendOrder(809, 100, -2, 18, 13, 190, true);
            curFixConn.sendOrder(260, 100, -2, 18, 13, 191, true);
            curFixConn.sendOrder(384, 100, -2, 18, 13, 191, true);
            curFixConn.sendOrder(801, 100, -2, 18, 13, 191, true);
            curFixConn.sendOrder(29, 100, -2, 18, 13, 193, true);
            curFixConn.sendOrder(185, 100, -2, 18, 13, 193, true);
            curFixConn.sendOrder(393, 100, -2, 18, 13, 193, true);
            curFixConn.sendOrder(715, 100, -2, 18, 13, 193, true);
            curFixConn.sendOrder(30187, 100, -2, 18, 13, 193, true);
        }
    }
}
