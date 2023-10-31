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
    public partial class Controller : Form
    {
        public Controller()
        {
            InitializeComponent();
            doConnection();
        }
        
        private StreamBaseURI uri;
        private StreamBaseClient client;
        private System.Windows.Forms.Timer tmBoleta = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer tmSend = new System.Windows.Forms.Timer();
        BindingList<string> commands = new BindingList<string>();
        
        private void startNestProject_Click(object sender, EventArgs e)
        {

        }

        private void startMDBMF_Click(object sender, EventArgs e)
        {

        }

        private void startOSM_Click(object sender, EventArgs e)
        {

        }

        private void btSendCommand_Click(object sender, EventArgs e)
        {
            if (client == null || client.IsClosed())
                    MessageBox.Show("Please connect before sending command.");
                else
                {
                    if (cbCommand.Text == "GET PREVIEW")
                    {
                        try
                        {
                        Schema schema = client.GetSchemaForStream("Command");
                        Tuple tuple = schema.CreateTuple();

                        Schema.Field field1 = schema.GetField("Command");

                        // initialize the tuple's fields
                        tuple.SetString(field1, "SEND ORDERS");

                        // enqueue the tuple
                        client.Enqueue("Command", tuple);
                        }
                        catch (StreamBaseException es)
                        {
                            MessageBox.Show("Could not enqueue to stream: Command " + es.ToString());
                            return; // EXIT
                        }

                        tmBoleta.Interval = 5000;
                        tmSend.Start();                        
                    } 
                else 
                    {
                         try
                    {
                        Schema schema = client.GetSchemaForStream("Command");
                        Tuple tuple = schema.CreateTuple();

                        Schema.Field field1 = schema.GetField("Command");

                        // initialize the tuple's fields
                        tuple.SetString(field1, cbCommand.Text);

                        // enqueue the tuple
                        client.Enqueue("Command", tuple);
                    }
                    catch (StreamBaseException es)
                    {
                        MessageBox.Show("Could not enqueue to stream: Command " + es.ToString());
                        return; // EXIT
                    }
                }
            }
        }

        private void tmBoleta_Tick()
        {
            tmBoleta.Stop();

            if (client == null || client.IsClosed())
                MessageBox.Show("Please connect before sending command.");
            else
            {
                try
                {
                    Schema schema = client.GetSchemaForStream("Command");
                    Tuple tuple = schema.CreateTuple();

                    Schema.Field field1 = schema.GetField("Command");
                    
                    tuple.SetString(field1, "DIVIDE");

                        // enqueue the tuple
                    client.Enqueue("Command", tuple);
                }
                catch (StreamBaseException es)
                {
                    MessageBox.Show("Could not enqueue to stream: Command " + es.ToString());
                    return; // EXIT
                }

                tmBoleta.Interval = 5000;
                tmSend.Start();
                }
            tmSend.Interval = 20000;
            tmSend.Start();
        }

        private void tmSend_Tick()
        {
            tmSend.Stop();
            if (client == null || client.IsClosed())
                MessageBox.Show("Please connect before sending command.");
            else
            {
                try
                {
                    Schema schema = client.GetSchemaForStream("Command");
                    Tuple tuple = schema.CreateTuple();
                    Schema.Field field1 = schema.GetField("Command");

                        // initialize the tuple's fields
                    tuple.SetString(field1, "SEND PREVIEW");

                        // enqueue the tuple
                    client.Enqueue("Command", tuple);
                }
                catch (StreamBaseException es)
                {
                    MessageBox.Show("Could not enqueue to stream: Command " + es.ToString());
                    return; // EXIT
                }
                tmBoleta.Interval = 5000;
                tmSend.Start();
            }
        }
         

        public void threadStart()
        {
        }

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
            commands.Add("START");
            commands.Add("MDREQUEST");
            commands.Add("SEND ORDERS");
            commands.Add("GET PREVIEW");
            cbCommand.DataSource = commands;
        }

        private void doDisconnection()
        {
            client.Close();           
        }

        private void BMFReqBt_Click(object sender, EventArgs e)
        {
            //prepare EntryType field.
            List<string> entryTypes = new List<string>();
            string szEntry = EntryTypeBMF.Text;
            string[] entryList = szEntry.Split(',');

            foreach (string str in entryList)
            {
                entryTypes.Add(str);
            }

            
        }



   
    }
}
