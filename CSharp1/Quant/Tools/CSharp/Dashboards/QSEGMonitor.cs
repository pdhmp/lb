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
    public partial class QSEGMonitor : Form
    {
        public QSEGMonitor()
        {
            InitializeComponent();
            this.doConnection();
        }

        
        //lista de qsegs
        private BindingList<QSEGS> ListQSEGS = new BindingList<QSEGS>();
                
        private string deqStream = "QSEGS";

        private string strQsegName;
        private double dbValue;
        private double dbValueDm1;

        StreamBaseClient client;
        StreamBaseURI uri;
        DequeueResult result;
        System.Threading.Thread dequeueThread;
        public delegate void ReceiveTupleDelegate(Tuple tuple);

        private Schema.Field QSEGName;
        private Schema.Field Value;
        private Schema.Field ValueDm1;
        /*
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
        //    base.Dispose(disposing);
        }*/        
        public bool doConnection()
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
                return false;
            }

            // Connect to the server
            try
            {
                client = new StreamBaseClient(uri);
            }
            catch (StreamBaseException e)
            {
                MessageBox.Show("Cound not connect to the server at: " + uri.ToString() + " (" + e.ToString() + ")");
                return false ; // EXIT
            }

            //subscribe to the output stream
            try
            {
                client.Subscribe(deqStream);
            }
            catch (StreamBaseException e)
            {
                MessageBox.Show("Could not connect to stream: "+deqStream+ e.ToString());
                return false; // EXIT
            }

            //pega as propriedades do stream que estou dando o dequeue
            StreamProperties props = client.GetStreamProperties(deqStream);
            //pega o schema do output.
            Schema schema = props.GetSchema();
            
            QSEGName = schema.GetField("QSEGName");
            Value = schema.GetField("Value");
            ValueDm1 = schema.GetField("ValueDm1");
                        
            dequeueThread = new System.Threading.Thread(new ThreadStart(DequeueProcedure));
            try
            {
                dequeueThread.Start();
            }
            catch (System.Security.SecurityException e)
            {
                MessageBox.Show("Error starting dequeue thread: " + e.ToString());
                return false ; // EXIT
            }
            QSEGSGrid.DataSource = ListQSEGS;
            return true;
        }

        public Boolean doDisconnection()
        {
            client.Close();
            dequeueThread.Join();            
            return true;
        }

        private void DequeueProcedure ()
        {            
            try
            {                
                while (true)
                {
                    result = client.Dequeue();
                    if (result.Status == DequeueResult.GOOD)
                    {
                        // check the right stream (not strictly necesary)
                        if (result.GetStreamName() == deqStream)
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
                Console.WriteLine("Could not dequeue from stream: " + deqStream + " (" + e.ToString());
                return; // Exit thread
            }
        }

        private void ReceiveTuple(Tuple tuple)
        {
            try
            {
                if (tuple.GetField(QSEGName) != null && tuple.GetField(Value) != null && tuple.GetField(ValueDm1) != null)
                {
                    strQsegName = tuple.GetField(QSEGName).ToString();
                    dbValue = Double.Parse(tuple.GetField(Value).ToString());
                    dbValueDm1 = Double.Parse(tuple.GetField(ValueDm1).ToString());
                    //se o qsegname está lá, atualiza, senão, insere
                    Boolean found = false;
                    foreach (QSEGS _qseg in ListQSEGS)
                    {
                        if (_qseg.Name == strQsegName)
                        {
                            _qseg.setName(strQsegName);
                            _qseg.setValue(dbValue);
                            _qseg.setValueDm1(dbValueDm1);
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        //ListOrdersCBO1.Add(new OrderViewer(_symbol, _last, _positiondm1, _positiond0, _action, ""));
                        ListQSEGS.Add(new QSEGS(strQsegName, dbValue, dbValueDm1));
                    }
                }
            }catch (TupleException e)
            {
                MessageBox.Show("Problem with field ("+e.ToString()+")");
                return; // EXIT
            }
        }         
    }
}




