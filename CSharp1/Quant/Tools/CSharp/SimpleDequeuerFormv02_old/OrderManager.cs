using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using NestDLL;
using System.Windows.Forms;
using StreamBase.SB;
using StreamBase.SB.Client;
using System.Threading;

namespace SimpleDequeuerForm
{
    public partial class OrderManager : Form
    {
        
        public OrderManager()
        {
            InitializeComponent();
            doConnection();
        }

        StreamBaseClient client;
        StreamBaseURI uri;
        private string szStream = "AppOut";

        private BindingList<OrderManagerContainter> OrderList = new BindingList<OrderManagerContainter>();
        private BindingList<OrderManagerContainter> ParcialOrderList = new BindingList<OrderManagerContainter>();

        private BindingList<OrderManagerContainter> AuxOrderList = new BindingList<OrderManagerContainter>();
        private BindingList<OrderManagerContainter> AuxParcialOrderList = new BindingList<OrderManagerContainter>();

        private Dictionary<int, string> StrategiesDictionary;

        private BindingList<string> listStrategies = new BindingList<string>();

        private Schema.Field AppOrdID;
        private Schema.Field Symbol;
        private Schema.Field Side;
        private Schema.Field OrderStatus;
        private Schema.Field AvgPx;
        private Schema.Field OrderQty;
        private Schema.Field LastShares;
        private Schema.Field LeavesQty;
        private Schema.Field UpdateTime;
        private Schema.Field OrdRejReason;

        DequeueResult result;
        System.Threading.Thread dequeueThread;
        public delegate void ReceiveTupleDelegate(Tuple tuple);
                
        //Get the list of possible strategies from database.

        private void getStrategiesList()
        {
            DataTable sectionsTable;

            string sqlQuery = "SELECT [Id_Section], [Section] FROM [NESTSRV06].[NESTDB].[dbo].[Tb404_Section]";

            using (newNestConn conn = new newNestConn(true))
            {
                sectionsTable = conn.Return_DataTable(sqlQuery);
            }

            for (int i = 0; i < sectionsTable.Rows.Count; i++)
            {

                Console.WriteLine((string)sectionsTable.Rows[i][1]);
                Console.WriteLine((string)sectionsTable.Rows[i][2]);
                

                //Components.Add((int)componentsTable.Rows[i][0]);
                StrategiesDictionary.Add((int)sectionsTable.Rows[i][0], (string)sectionsTable.Rows[i][1]);
            }

        }
        //connection to streambase
       
        private void doConnection()
        {
            string server;

            server = "sb://localhost:10000";

            try
            {
                uri = new StreamBaseURI(server);

            }
            catch (URIException e)
            {
                MessageBox.Show("Invalid URI (" + e.ToString() + ")");
                return; // EXIT
            }

            // Connect to the server
            try
            {
                client = new StreamBaseClient(uri);
            }
            catch (StreamBaseException e)
            {
                MessageBox.Show("Cound not connect to the server at: " + uri.ToString() + " (" + e.ToString() + ")");
                return; // EXIT
            }

            //subscribe to the output stream
            try
            {
                client.Subscribe(szStream);
            }
            catch (StreamBaseException e)
            {
                MessageBox.Show("Could not connect to stream: Order "+ e.ToString());
                return; // EXIT
            }

            //pega as propriedades do stream que estou dando o dequeue
            StreamProperties props = client.GetStreamProperties(szStream);
            //pega o schema do output.
            Schema schema = props.GetSchema();

            AppOrdID = schema.GetField("AppOrdID");
            Symbol = schema.GetField("Symbol");
            Side = schema.GetField("Side");
            OrderStatus = schema.GetField("OrderStatus");
            AvgPx = schema.GetField("AvgPx");
            OrderQty = schema.GetField("OrderQty");
            LastShares = schema.GetField("LastShares");
            LeavesQty = schema.GetField("LeavesQty");
            UpdateTime = schema.GetField("UpdateTime");
            OrdRejReason = schema.GetField("OrdRejReason");

            cbStrategies.DataSource = listStrategies;

            getStrategiesList();

            // spin out dequeue thread so that waiting for results doesn't block windows main loop
            dequeueThread = new System.Threading.Thread(new ThreadStart(DequeueProcedure));
            try
            {
                dequeueThread.Start();
            }
            catch (System.Security.SecurityException e)
            {
                MessageBox.Show("Error starting dequeue thread: " + e.ToString());
                return; // EXIT
            }
        }
        private void DequeueProcedure()
        {
            try
            {
                // loop getting results from the server as they come in
                while (true)
                {
                    result = client.Dequeue();

                    // If we dequeued tuples, output their content
                    if (result.Status == DequeueResult.GOOD)
                    {
                        // check the right stream (not strictly necesary)
                        if (result.GetStreamName() == "AppOut")
                        {
                            // Iterate over each tuple in the results using the Iterator
                            // where "myint" and "mystring" are field names in the output stream
                            // of your StreamBase application
                            foreach (Tuple tuple in result)
                            {
                                // Call back into the main thread.  NB this is done synchronously using Invoke.
                                // To use the tuple in an asynchronous call (BeginInvoke) you should use a cloned copy
                                // of the tuple instead as the original is not guaranteed to still be valid after
                                // the next call to StreamBaseClient.Dequeue().
                                Invoke(new ReceiveTupleDelegate(ReceiveTuple), new object[] { tuple });
                            }
                        }
                    }
                    else if (result.Status == DequeueResult.CLOSED)
                    {
                        return; // Exit thread
                    }
                    else
                    {
                        // NB cant use MessageBox because not in main thread
                        Console.WriteLine("Unexected status in DequeueResult: " + result.Status);
                        return; // Exit thread
                    }
                }
            }
            catch (StreamBaseException e)
            {
                // NB cant use MessageBox because not in main thread
                Console.WriteLine("Could not dequeue from stream: Order (" + e.ToString());
                return; // Exit thread
            }
        }
        private void doDisconnection()
        {
            client.Close();
            dequeueThread.Join();
        }
        private void ReceiveTuple(Tuple tuple)
        {
            try
            {               
                if (tuple.GetField(AppOrdID) != null && tuple.GetField(Symbol) != null)
                {
                    string _orderId = tuple.GetField(AppOrdID).ToString();
                    string _symbol =  tuple.GetField(Symbol).ToString();

                    string _side = "";
                    string _orderStatus = "";
                    double _avgPrice = 0;
                    double _orderQty = 0;
                    double _lastShares = 0;
                    double _leavesQty = 0;
                    string _updateTime = "";
                    string _OrdRejReason = "";

                    if (tuple.GetField(Side) != null)
                    {
                        _side = tuple.GetField(Side).ToString();
                    }
                    if (tuple.GetField(OrderStatus) != null)
                    {
                        _orderStatus = tuple.GetField(OrderStatus).ToString();
                    }
                    if (tuple.GetField(AvgPx) != null)
                    {
                        _avgPrice = Double.Parse(tuple.GetField(AvgPx).ToString());
                    }
                    if (tuple.GetField(OrderQty) != null)
                    {
                        _orderQty = Double.Parse(tuple.GetField(OrderQty).ToString());
                    }
                    if (tuple.GetField(LastShares) != null)
                    {
                        _lastShares = Double.Parse(tuple.GetField(LastShares).ToString());
                    }
                    if (tuple.GetField(LeavesQty) != null)
                    {
                        _leavesQty = Double.Parse(tuple.GetField(LeavesQty).ToString());
                    }
                    if (tuple.GetField(UpdateTime) != null)
                    {
                        _updateTime = tuple.GetField(UpdateTime).ToString();
                    }
                    if (tuple.GetField(OrdRejReason) != null)
                    {
                        _OrdRejReason = tuple.GetField(OrdRejReason).ToString();
                    }
                                      
                    

                }
            }
            catch (TupleException e)
            {
                MessageBox.Show("Problem with field (" + e.ToString() + ")");
                return; // EXIT
            }
        }
        private void addOrUpdateOrders(string _OrderId, string _Symbol, string _Side, string _OrderStatus, double _AveragePrice, double _OrderQuantity, double _Executed, double _Leaves, string _UpdateTime, string _ExtraInfo)
        {
            //msm esq. procuro . se nao existe, insiro.
            Boolean _found = false;
            
            foreach (OrderManagerContainter omc in OrderList)
            {
                if (String.Compare(omc.OrderId,_OrderId) == 0 && String.Compare(omc.Symbol,_Symbol) == 0)
                {
                    omc.setOrderStatus(_OrderStatus);
                    omc.setAveragePrice(_AveragePrice);
                    omc.setExecuted(omc.getExecuted() + _Executed);
                    omc.setLeaves(_Leaves);
                    omc.setUpdateTime(_UpdateTime);
                    omc.setExtraInfo(_ExtraInfo);
                    _found = true;
                }
            }
            if (!_found)
            {
                //pegar o section do orderid, pro
                int szAux = Int16.Parse(_OrderId.Substring(_OrderId.Length-4,_OrderId.Length-1));
                string _Strategy = "";

                if (StrategiesDictionary.ContainsKey(szAux))
                {
                    _Strategy = StrategiesDictionary[szAux];
                }

                OrderList.Add(new OrderManagerContainter(_OrderId, _Strategy, _Symbol, _Side, _OrderStatus, _AveragePrice, _OrderQuantity, _Executed, _Leaves, _UpdateTime, _ExtraInfo));
                if (!listStrategies.Contains(_Strategy))
                {
                    listStrategies.Add(_Strategy);
                }
            }
        }
        private void dgOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void showAll_CheckedChanged(object sender, EventArgs e)
        {
            if (showAll.Checked)
            {
                cbStrategies.Enabled = false;
                //change data binding
                dgOrders.DataSource = OrderList;
                ParcialOrderList.Clear();
                string modeView = cbModeView.Text;
                foreach (OrderManagerContainter omc in OrderList)
                {
                    if (String.Compare(omc.OrderStatus, modeView) == 0)
                    {
                        ParcialOrderList.Add(omc);
                    }
                }
                dgParcialOrder.DataSource = ParcialOrderList;
            }            
        }

        private void showStrategy_CheckedChanged(object sender, EventArgs e)
        {
            if (showStrategy.Enabled)
            {
                cbStrategies.Enabled = true;
                //
                string strategy = cbStrategies.Text;
                AuxOrderList.Clear();
                foreach (OrderManagerContainter omc in OrderList)
                {
                    if (String.Compare(omc.Strategy, strategy) == 0)
                    {
                        AuxOrderList.Add(omc);
                    }
                }

                dgOrders.DataSource = AuxOrderList;
                string modeView = cbModeView.Text;
                AuxParcialOrderList.Clear();
                foreach (OrderManagerContainter omc in AuxOrderList)
                {
                    if (String.Compare(modeView, omc.OrderStatus) == 0)
                    {
                        AuxParcialOrderList.Add(omc);
                    }
                }
                dgParcialOrder.DataSource = AuxParcialOrderList;
            }
        }
   
    }
}
