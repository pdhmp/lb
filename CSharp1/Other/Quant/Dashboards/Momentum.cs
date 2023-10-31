using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StreamBase.SB;
using StreamBase.SB.Client;
using System.Threading;

namespace Dashboards
{
    public partial class Momentum : Form
    {
        private BindingList<OrderViewer> ListOrdersMom1 = new BindingList<OrderViewer>();
        private BindingList<OrderViewer> ListOrdersMom2 = new BindingList<OrderViewer>();
        private BindingList<OrderViewer> ListOrdersMom7 = new BindingList<OrderViewer>();
        private BindingList<OrderViewer> ListOrdersMom8 = new BindingList<OrderViewer>();
        private BindingList<OrderViewer> ListOrdersMomLS = new BindingList<OrderViewer>();


        private string stream = "Order";
        private string OrderidMom1 = "18_19_325";
        private string OrderidMom2 = "18_19_326";
        private string OrderidMom7 = "18_19_268";
        private string OrderidMom8 = "18_19_269";
        private string OrderidMomLS = "18_19_225";
        
        StreamBaseClient client;
        StreamBaseURI uri;
        DequeueResult result;
        System.Threading.Thread dequeueThread;
        public delegate void ReceiveTupleDelegate(Tuple tuple);

        private Schema.Field PrefixOrderId;
        private Schema.Field Symbol;
        private Schema.Field Last;
        private Schema.Field PositionDm1;
        private Schema.Field PositionD0;
        private Schema.Field Action;

        private Boolean doConnection()
        {
            string server;

            server = "sb://NestSrv05:20000";

            try
            {
                uri = new StreamBaseURI(server);

            }
            catch (URIException e)
            {
                MessageBox.Show("Invalid URI (" + e.ToString() + ")");
                return false ; 
            }

            // Connect to the server
            try
            {
                client = new StreamBaseClient(uri);
            }
            catch (StreamBaseException e)
            {
                MessageBox.Show("Cound not connect to the server at: " + uri.ToString() + " (" + e.ToString() + ")");
                return false; // EXIT
            }

            //subscribe to the output stream
            try
            {
                client.Subscribe(stream);
            }
            catch (StreamBaseException e)
            {
                MessageBox.Show("Could not connect to stream: " + stream + " (" + e.ToString());
                return false; // EXIT
            }

            //pega as propriedades do stream que estou dando o dequeue
            StreamProperties props = client.GetStreamProperties(stream);
            //pega o schema do output.
            Schema schema = props.GetSchema();

            PrefixOrderId = schema.GetField("PrefixOrderId");
            Symbol = schema.GetField("Symbol");
            Last = schema.GetField("Last");
            PositionDm1 = schema.GetField("PositionDm1");
            PositionD0 = schema.GetField("PositionD0");
            Action = schema.GetField("Action");

            // spin out dequeue thread so that waiting for results doesn't block windows main loop
            dequeueThread = new System.Threading.Thread(new ThreadStart(DequeueProcedure));
            try
            {
                dequeueThread.Start();
            }
            catch (System.Security.SecurityException e)
            {
                MessageBox.Show("Error starting dequeue thread: " + e.ToString());
                return false; // EXIT
            }

            dgvMomentum19.DataSource = ListOrdersMom1;
            dgvMomentum24.DataSource = ListOrdersMom2;
            dgvMomentum7.DataSource = ListOrdersMom7;
            dgvMomentum8.DataSource = ListOrdersMom8;
            dgvMomentumLS.DataSource = ListOrdersMomLS;

            return true;
        }

        private Boolean doDisconnection()
        {
            client.Close(); // This will make the dequeuer thread exit
            dequeueThread.Join();
            return true;
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
                        if (result.GetStreamName() == stream)
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
                Console.WriteLine("Could not dequeue from stream: " + stream + " (" + e.ToString());
                return; // Exit thread
            }
        }

        private void ReceiveTuple(Tuple tuple)
        {
            string _symbol = "";
            double _action = 0;
            double _posDm1 = 0;
            double _posD0 = 0;
            double _last = 0;
            string _orderId;
            try
            {
                if (tuple.GetField(Symbol)!= null && tuple.GetField(Last) != null)
                {
                    _symbol = tuple.GetField(Symbol).ToString();
                    _last = Double.Parse(tuple.GetField(Last).ToString());
                    _orderId = tuple.GetField(PrefixOrderId).ToString();
                    if (tuple.GetField(PositionDm1) != null)
                    {
                        _posDm1 = Double.Parse(tuple.GetField(PositionDm1).ToString());
                    }
                    else
                    {
                        _posDm1 = 0;
                    }
                    if (tuple.GetField(PositionD0) != null)
                    {
                        _posD0 = Double.Parse(tuple.GetField(PositionD0).ToString());
                    }
                    else
                    {
                        _posD0 = 0;
                    }
                    if (tuple.GetField(Action) != null)
                    {
                        _action = Double.Parse(tuple.GetField(Action).ToString());
                    }
                    else
                    {
                        _action = 0;
                    }
                    AddOrUpdateOrder(_orderId, _symbol, _last, _posDm1, _posD0, _action);
                }
            }
            catch (TupleException e)
            {
                MessageBox.Show("Problem with field (" + e.ToString() + ")");
                return; // EXIT
            }

        }

        private void AddOrUpdateOrder(string _prefixOrderId, string _symbol, double _last, double _positiondm1, double _positiond0, double _action)
        {
            Boolean found = false;
            //momentum1
            if (_prefixOrderId == OrderidMom1)
            {
                foreach (OrderViewer ov in ListOrdersMom1)
                {
                    if (ov.Symbol == _symbol)
                    {
                        ov.setPositionDm1(_positiondm1);
                        ov.setPositionD0(_positiond0);
                        ov.setAction(_action);
                        found = true;
                    }
                }
                if (!found)
                {
                    ListOrdersMom1.Add(new OrderViewer(_symbol, _last, _positiondm1, _positiond0, _action, ""));
                }
            }

            //Momentum2
            else if (_prefixOrderId == OrderidMom2)
            {
                foreach (OrderViewer ov in ListOrdersMom2)
                {
                    if (ov.Symbol == _symbol)
                    {
                        ov.setPositionDm1(_positiondm1);
                        ov.setPositionD0(_positiond0);
                        ov.setAction(_action);
                        found = true;
                    }
                }
                if (!found)
                {
                    ListOrdersMom2.Add(new OrderViewer(_symbol, _last, _positiondm1, _positiond0, _action, ""));
                }
            }
            //Momentum7
            else if (_prefixOrderId == OrderidMom7)
            {
                foreach (OrderViewer ov in ListOrdersMom7)
                {
                    if (ov.Symbol == _symbol)
                    {
                        ov.setPositionDm1(_positiondm1);
                        ov.setPositionD0(_positiond0);
                        ov.setAction(_action);
                        found = true;
                    }
                }
                if (!found)
                {
                    ListOrdersMom7.Add(new OrderViewer(_symbol, _last, _positiondm1, _positiond0, _action, ""));
                }
            }
            //Momentum8
            else if (_prefixOrderId == OrderidMom8)
            {
                foreach (OrderViewer ov in ListOrdersMom8)
                {
                    if (ov.Symbol == _symbol)
                    {
                        ov.setPositionDm1(_positiondm1);
                        ov.setPositionD0(_positiond0);
                        ov.setAction(_action);
                        found = true;
                    }
                }
                if (!found)
                {
                    ListOrdersMom8.Add(new OrderViewer(_symbol, _last, _positiondm1, _positiond0, _action, ""));
                }
            }
            //MomentumLS
            else if (_prefixOrderId == OrderidMomLS)
            {
                foreach(OrderViewer ov in ListOrdersMomLS)
                {
                    if (ov.Symbol == _symbol)
                    {
                        ov.setPositionDm1(_positiondm1);
                        ov.setPositionD0(_positiond0);
                        ov.setAction(_action);
                        found = true;
                    }
                }
                if (!found)
                {
                    ListOrdersMomLS.Add(new OrderViewer(_symbol, _last, _positiondm1, _positiond0, _action, ""));
                }
            }
        }

        public Momentum()
        {
            
            InitializeComponent();
            this.doConnection();

        }

    }
}
