using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using NestDLL;
using NFIXConn;
using NCommonTypes;

namespace OrderSender
{
    public partial class OrderSender : Form
    {
        private volatile object ordListSync = new object();
        private List<Order> OrderList = new List<Order>();
        private SortedDictionary<string, int> OrderListIndex = new SortedDictionary<string, int>();
        private SortedDictionary<int, Order> NewOrdList = new SortedDictionary<int, Order>();

        private FIXConn curFixConn = new FIXConn(@"T:\LOG\FIX\ORDERSENDER\CONFIG\ORDERSENDERCONFIG.CFG");

        private Thread OrdersLoop;
        private bool KeepRunning = true;

        public OrderSender()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            curFixConn.ReceivedFill += new EventHandler(curFixConn_ReceivedFill);
            OrdersLoop = new Thread(new ThreadStart(RunThread));
            OrdersLoop.Start();
        }

        void curFixConn_ReceivedFill(object sender, EventArgs e)
        {
            ExecutionFillArgs curFill = (ExecutionFillArgs)e;

            int curOrderID;

            if (OrderListIndex.TryGetValue(curFill.ClOrdID, out curOrderID))
            {
                Order curOrder = OrderList[curOrderID];

                curOrder.Leaves -= curFill.LastShares;
                curOrder.ExecQty += curFill.LastShares;

                using (newNestConn curConn = new newNestConn(true))
                {
                    string SQLExpression = "UPDATE NESTSIM.DBO.TB600_QUANTORDERS " +
                                           "SET LEAVES = " + curOrder.Leaves.ToString() +
                                           ", EXECQTY = " + curOrder.ExecQty +
                                           " WHERE IDORDER = " + curOrder.IdOrder.ToString();

                    curConn.ExecuteNonQuery(SQLExpression);
                }
            }
        }

        private void RunThread()
        {
            while (KeepRunning)
            {
                GetNewOrders();
                Thread.Sleep(5000);
            }
        }
        
        private void GetNewOrders()
        {            
            DataTable newOrders = new DataTable();

            using (newNestConn curConn = new newNestConn(true))
            {
                string SQLExpression = "SELECT * FROM NESTSIM.DBO.TB600_QUANT_ORDERS " +
                                       "WHERE ORDERDATE = '" + DateTime.Today.ToString("yyyyMMdd") + "' " +
                                       "AND UNORDQTY <> 0";
                newOrders = curConn.Return_DataTable(SQLExpression);
            }

            foreach (DataRow curRow in newOrders.Rows)
            {
                Order curOrder = new Order();
                curOrder.IdOrder = Convert.ToInt32(curRow["IdOrder"]);
                curOrder.IdSecurity = Convert.ToInt32(curRow["IdSecurity"]);
                curOrder.Price = Convert.ToDouble(curRow["Price"]);
                curOrder.OrderQty = Convert.ToInt32(curRow["OrderQty"]);
                curOrder.UnordQty = Convert.ToInt32(curRow["UnordQty"]);
                curOrder.Leaves = Convert.ToInt32(curRow["Leaves"]);
                curOrder.ExecQty = Convert.ToInt32(curRow["ExecQty"]);
                curOrder.ExecValue = Convert.ToDouble(curRow["ExecValue"]);
                curOrder.IdPortfolio = Convert.ToInt32(curRow["IdPortfolio"]);
                curOrder.IdBook = Convert.ToInt32(curRow["IdBook"]);
                curOrder.IdSection = Convert.ToInt32(curRow["IdSection"]);
                curOrder.OrderDate = Convert.ToDateTime(curRow["OrderDate"]);
                curOrder.DontMatch = Convert.ToInt32(curRow["DontMatch"]);

                bool Send = false;

                lock (ordListSync)
                {
                    OrderList.Add(curOrder);
                    if (!NewOrdList.ContainsKey(curOrder.IdOrder))
                    {
                        NewOrdList.Add(curOrder.IdOrder, curOrder);
                        Send = true;
                    }                    
                }

                //Send Order and update orders table
                if (Send)
                {
                    try
                    {
                        bool updated = false;

                        using (newNestConn curConn = new newNestConn(true))
                        {
                            string SQLExpression = "UPDATE NESTSIM.DBO.TB600_QUANT_ORDERS " +
                                                   "SET UNORDQTY = 0" +
                                                   ", LEAVES = " + curOrder.UnordQty.ToString() +                                                
                                                   " WHERE IDORDER = " + curOrder.IdOrder.ToString();

                            curConn.ExecuteNonQuery(SQLExpression);

                            string checkUpdate = "SELECT UNORDQTY FROM NESTSIM.DBO.TB600_QUANT_ORDERS WHERE IDORDER = " + curOrder.IdOrder.ToString();

                            int unord = curConn.Return_Int(checkUpdate);

                            if (unord == 0)
                            {
                                updated = true;
                            }
                            
                        }

                        if (updated)
                        {
                            bool DontMatch = false;

                            if (curOrder.DontMatch == 1) { DontMatch = true; }

                            string ClOrdID = curFixConn.sendOrder(curOrder.IdSecurity, curOrder.UnordQty, curOrder.Price, curOrder.IdPortfolio, curOrder.IdBook, curOrder.IdSection, DontMatch);
                            curOrder.ClOrdID = ClOrdID;

                            UpdateOrderListIndex();

                            curOrder.Leaves = curOrder.UnordQty;
                            curOrder.UnordQty = 0;

                            using (newNestConn curConn = new newNestConn(true))
                            {
                                string SQLExpression = "UPDATE NESTSIM.DBO.TB600_QUANT_ORDERS " +
                                                       "SET CLORDID = '" + curOrder.ClOrdID + "'" +
                                                       " WHERE IDORDER = " + curOrder.IdOrder.ToString();

                                curConn.ExecuteNonQuery(SQLExpression);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        StreamWriter sr = new StreamWriter(@"C:\temp\OrderSenderLog.txt",true);
                        sr.WriteLine("Novo Erro: " + DateTime.Now.ToString() + "\r\n" + e.ToString());
                        sr.Close();

                        MessageBox.Show("Erro no envio de ordens. Verifique o arquivo de de log C:\\temp\\OrderSenderLog.txt");
                    }                
                }                                      
            }
        }

        private void UpdateOrderListIndex()
        {
            OrderListIndex.Clear();

            for (int i = 0; i < OrderList.Count; i++)
            {
                OrderListIndex.Add(OrderList[i].ClOrdID, i);
            }
        }

        private void OrderSender_FormClosing(object sender, FormClosingEventArgs e)
        {
            KeepRunning = false;
        }
    }
}
