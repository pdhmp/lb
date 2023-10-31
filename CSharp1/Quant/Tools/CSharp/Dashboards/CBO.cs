using System;
using System.Drawing;
using System.Collections;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using StreamBase.SB;
using StreamBase.SB.Client;
using System.Collections.Generic;

namespace Dashboards
{
    public partial class CBO : Form
    {
        public CBO()
        {
            InitializeComponent();
            this.doConnection();
        }

        private BindingList<OrderViewer> ListOrdersCBO5 = new BindingList<OrderViewer>();
        private BindingList<OrderViewer> ListOrdersCBO11 = new BindingList<OrderViewer>();

        private string _prefixorderId;
        private string _symbol;
        private double _last;
        private double _positiondm1;
        private double _positiond0;
        private double _action;


        private string szStream = "Order";
        StreamBaseClient client;
        StreamBaseURI uri;
        DequeueResult result;
        System.Threading.Thread dequeueThread;
        public delegate void ReceiveTupleDelegate(Tuple tuple);

        //
        // These are used to cache the field objects in the schema;
        // it's more efficient to use Field objects than field names
        // to access tuple values.

        private Schema.Field PrefixOrderId;
        private Schema.Field Symbol;
        private Schema.Field Last;
        private Schema.Field PositionDm1;
        private Schema.Field PositionD0;
        private Schema.Field Action;

        private const string SendOrderIdCBO5 = "18_19_327";
        private const string SendOrderIdCBO11 = "18_19_328";
        private const string EnqueuingStream = "GetPreview";

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
                MessageBox.Show("Could not connect to stream: " + szStream + e.ToString());
                return; // EXIT
            }

            //pega as propriedades do stream que estou dando o dequeue
            StreamProperties props = client.GetStreamProperties(szStream);
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
                return; // EXIT
            }


            dvOrdersCBO5.DataSource = ListOrdersCBO5;
            dvOrdersCBO11.DataSource = ListOrdersCBO11;
            
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
                if (tuple.GetField(Symbol) != null)
                {
                    _prefixorderId = tuple.GetField(PrefixOrderId).ToString();
                    _symbol = tuple.GetField(Symbol).ToString();
                    if (tuple.GetField(Last) != null)
                    {
                        _last = Double.Parse(tuple.GetField(Last).ToString());
                    }
                    else
                    {
                        _last = 0;
                    }
                    if (tuple.GetField(PositionDm1) != null)
                    {
                        _positiondm1 = Double.Parse(tuple.GetField(PositionDm1).ToString());
                    }
                    else
                    {
                        _positiondm1 = 0;
                    }
                    if (tuple.GetField(PositionD0) != null)
                    {
                        _positiond0 = Double.Parse(tuple.GetField(PositionD0).ToString());
                    }
                    else
                    {
                        _positiond0 = 0;
                    }
                    if (tuple.GetField(Action) != null)
                    {
                        _action = Double.Parse(tuple.GetField(Action).ToString());
                    }
                    else
                    {
                        _action = 0;
                    }
                    AddOrUpdateOrder(_prefixorderId, _symbol, _last, _positiondm1, _positiond0, _action);
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
            //na hora de inserir, percorro a lista inteira. se encontrar, altero os fields, se não encontrar, insiro.

            Boolean found = false;
            if (_prefixOrderId == SendOrderIdCBO5)
            {
                foreach (OrderViewer ov in ListOrdersCBO5)
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
                    ListOrdersCBO5.Add(new OrderViewer(_symbol, _last, _positiondm1, _positiond0, _action, ""));
                }
            }
            else if (_prefixOrderId == SendOrderIdCBO11)
            {
                foreach (OrderViewer ov in ListOrdersCBO11)
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
                    ListOrdersCBO11.Add(new OrderViewer(_symbol, _last, _positiondm1, _positiond0, _action, ""));
                }
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
                        if (result.GetStreamName() == "Order")
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

    }
}
