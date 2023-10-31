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
    public partial class OrderReview : Form
    {
        public OrderReview()
        {
            InitializeComponent();
            doConnection();
        }

        private BindingList<DividedOrders> ListOrders = new BindingList<DividedOrders>();
        private BindingList<DividedOrders> Aux  = new BindingList<DividedOrders>();
        private BindingList<string> listStrategies = new BindingList<string>();



        private string _owner;
        private string _strategy;
        private string _symbol;
        private double _quantity;
        private double _qtyBroker;
        private double _qtyVirtual;
                
        private string szStream = "TFDividedOrders";

        StreamBaseClient client;
        StreamBaseURI uri;
        DequeueResult result;
        System.Threading.Thread dequeueThread;
        public delegate void ReceiveTupleDelegate(Tuple tuple);

        //schema

        private Schema.Field Owner;
        private Schema.Field Strategy;
        private Schema.Field Symbol;
        private Schema.Field Qtty;
        private Schema.Field QttyBroker;
        private Schema.Field QttyVirtual;

        private const string EnqueingString = "TFDividedOrders";

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

            Owner = schema.GetField("Owner");
            Strategy = schema.GetField("Strategy");
            Symbol = schema.GetField("Symbol");
            Qtty = schema.GetField("Qtty");
            QttyBroker = schema.GetField("QttyBroker");
            QttyVirtual = schema.GetField("QttyVirtual");

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
            
            OrdemConsolidado.DataSource = ListOrders;
            strategies.DataSource = listStrategies;
        }

        private void doDisconnection()
        {
            client.Close();
            dequeueThread.Join();

        }

        private void ReceiveTuple(Tuple tuple)
        {//Owner, Strategy, Symbol, Qtty, QttyBroker, QttyVirtual
            try
            {
                if (tuple.GetField(Owner) != null && tuple.GetField(Strategy) != null && tuple.GetField(Symbol) != null)
                {
                    _owner = tuple.GetField(Owner).ToString();
                    _strategy = tuple.GetField(Strategy).ToString();
                    _symbol = tuple.GetField(Symbol).ToString();
                    if (tuple.GetField(Qtty) != null)
                    {
                        _quantity = Double.Parse(tuple.GetField(Qtty).ToString());
                    }
                    else
                    {
                        _quantity = 0;
                    }
                    if (tuple.GetField(QttyBroker) != null)
                    {
                        _qtyBroker = Double.Parse(tuple.GetField(QttyBroker).ToString());
                    }
                    else
                    {
                        _qtyBroker = 0;
                    }
                    if (tuple.GetField(QttyVirtual) != null)
                    {
                        _qtyVirtual = Double.Parse(tuple.GetField(QttyVirtual).ToString());
                    }
                    else
                    {
                        _qtyVirtual = 0;
                    }
                    
                    //i will have all orders of all strategies of all securities.

                    addOrUpdateOrders(_owner, _strategy, _symbol,_quantity, _qtyBroker, _qtyVirtual);

                }
            }
            catch (TupleException e)
            {
                MessageBox.Show("Problem with field (" + e.ToString() + ")");
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
                        if (result.GetStreamName() == "TFDividedOrders")
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
        private void addOrUpdateOrders(string __owner, string __strategy, string __symbol, double __quantity, double __qtyBroker,double __qtyVirtual)
        {
            Boolean _found = false;
            
            foreach (DividedOrders dv in ListOrders)
            {
                if (dv.OrderId == __owner && dv.Symbol == __symbol)
                {
                    dv.setTotal(__quantity);
                    dv.setVirtual(__qtyVirtual);
                    dv.setBroker(__qtyBroker);                    
                    _found = true;
                }
            }
            if (!_found)
            {
                ListOrders.Add(new DividedOrders(__owner, __strategy, __symbol, __quantity, __qtyBroker, __qtyVirtual));
            }
            if (!listStrategies.Contains(__strategy))
            {
                listStrategies.Add(__strategy);
            }
        }
        private void getStrategy()
        {

        }

        private void showAll_CheckedChanged(object sender, EventArgs e)
        {
            //show all orders from all strategies
            strategies.Enabled = false;
            changeDataBinding("all");
        }

        private void showStrategy_CheckedChanged(object sender, EventArgs e)
        {
            //show only selected strategy
            strategies.Enabled = true;
            strategies.DataSource = listStrategies;
            string auxBuff = strategies.Text;
            if (auxBuff != "" && auxBuff != null)
            {
                changeDataBinding(auxBuff);
            }

        }

        private void strategies_SelectedIndexChanged(object sender, EventArgs e)
        {

            string auxBuff = strategies.Text;
            if (auxBuff != "" && auxBuff != null)
            {
                changeDataBinding(auxBuff);
            }


        }
        private void changeDataBinding(string szStrategy)
        {
            if (szStrategy == "all")
            {
                OrdemConsolidado.DataSource = ListOrders;
            }
            else
            {
                Aux.Clear();
                for (int ii = 0; ii < ListOrders.Count; ii++)
                {
                    if (ListOrders[ii].getStrategy() == szStrategy)
                    {
                        Aux.Add(ListOrders[ii]);
                    }

                }
                OrdemConsolidado.DataSource = Aux;
            }

            
        }

    }
}
