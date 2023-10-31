using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using StreamBase.SB;
using StreamBase.SB.Client;

namespace StreamBase.SB.Sample {

    /// <summary>
    /// Summary description for SimpleDequeuerForm.
    /// </summary>
    public class SimpleDequeuerForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.TextBox hostField;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.NumericUpDown portField;
        private System.Windows.Forms.TextBox myIntField;
        private System.Windows.Forms.Label myIntLabel;
        private System.Windows.Forms.TextBox myStringField;
        private System.Windows.Forms.Label myStringLabel;
        private System.Windows.Forms.Button connectButton;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;


        /// <summary>
        /// Application variables
        /// </summary>
        /// 
        StreamBaseClient client;
        StreamBaseURI uri;
        private System.Windows.Forms.Label streamLabel;
        private System.Windows.Forms.TextBox streamField;
        DequeueResult result;
        private System.Windows.Forms.Button disconnectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox myDoubleField;
        System.Threading.Thread dequeueThread;
        public delegate void ReceiveTupleDelegate(Tuple tuple);

        //
        // These are used to cache the field objects in the schema;
        // it's more efficient to use Field objects than field names
        // to access tuple values.
        //
        private Schema.Field myInt;
        private Schema.Field myString;
        private Schema.Field myDouble;


        public SimpleDequeuerForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
                if (components != null) 
                {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.hostLabel = new System.Windows.Forms.Label();
            this.hostField = new System.Windows.Forms.TextBox();
            this.portLabel = new System.Windows.Forms.Label();
            this.portField = new System.Windows.Forms.NumericUpDown();
            this.myIntField = new System.Windows.Forms.TextBox();
            this.myIntLabel = new System.Windows.Forms.Label();
            this.myStringField = new System.Windows.Forms.TextBox();
            this.myStringLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.streamLabel = new System.Windows.Forms.Label();
            this.streamField = new System.Windows.Forms.TextBox();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.myDoubleField = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.portField)).BeginInit();
            this.SuspendLayout();
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(86, 32);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(29, 13);
            this.hostLabel.TabIndex = 0;
            this.hostLabel.Text = "Host";
            this.hostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hostField
            // 
            this.hostField.Location = new System.Drawing.Point(120, 32);
            this.hostField.Name = "hostField";
            this.hostField.Size = new System.Drawing.Size(96, 20);
            this.hostField.TabIndex = 1;
            this.hostField.Text = "localhost";
            this.hostField.TextChanged += new System.EventHandler(this.hostField_TextChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(88, 65);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Port";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // portField
            // 
            this.portField.Location = new System.Drawing.Point(120, 65);
            this.portField.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.portField.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.portField.Name = "portField";
            this.portField.Size = new System.Drawing.Size(96, 20);
            this.portField.TabIndex = 3;
            this.portField.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // myIntField
            // 
            this.myIntField.Location = new System.Drawing.Point(120, 131);
            this.myIntField.Name = "myIntField";
            this.myIntField.Size = new System.Drawing.Size(96, 20);
            this.myIntField.TabIndex = 5;
            // 
            // myIntLabel
            // 
            this.myIntLabel.AutoSize = true;
            this.myIntLabel.Location = new System.Drawing.Point(82, 131);
            this.myIntLabel.Name = "myIntLabel";
            this.myIntLabel.Size = new System.Drawing.Size(31, 13);
            this.myIntLabel.TabIndex = 4;
            this.myIntLabel.Text = "myint";
            this.myIntLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // myStringField
            // 
            this.myStringField.Location = new System.Drawing.Point(120, 164);
            this.myStringField.Name = "myStringField";
            this.myStringField.Size = new System.Drawing.Size(96, 20);
            this.myStringField.TabIndex = 7;
            // 
            // myStringLabel
            // 
            this.myStringLabel.AutoSize = true;
            this.myStringLabel.Location = new System.Drawing.Point(66, 164);
            this.myStringLabel.Name = "myStringLabel";
            this.myStringLabel.Size = new System.Drawing.Size(45, 13);
            this.myStringLabel.TabIndex = 6;
            this.myStringLabel.Text = "mystring";
            this.myStringLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(68, 256);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(72, 24);
            this.connectButton.TabIndex = 8;
            this.connectButton.Text = "Connect";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // streamLabel
            // 
            this.streamLabel.AutoSize = true;
            this.streamLabel.Location = new System.Drawing.Point(72, 98);
            this.streamLabel.Name = "streamLabel";
            this.streamLabel.Size = new System.Drawing.Size(40, 13);
            this.streamLabel.TabIndex = 9;
            this.streamLabel.Text = "Stream";
            this.streamLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // streamField
            // 
            this.streamField.Location = new System.Drawing.Point(120, 98);
            this.streamField.Name = "streamField";
            this.streamField.Size = new System.Drawing.Size(96, 20);
            this.streamField.TabIndex = 10;
            this.streamField.Text = "OutputStream1";
            this.streamField.TextChanged += new System.EventHandler(this.streamField_TextChanged);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(156, 256);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(72, 24);
            this.disconnectButton.TabIndex = 11;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // myDoubleField
            // 
            this.myDoubleField.Location = new System.Drawing.Point(120, 197);
            this.myDoubleField.Name = "myDoubleField";
            this.myDoubleField.Size = new System.Drawing.Size(96, 20);
            this.myDoubleField.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 197);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "mydouble";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // SimpleDequeuerForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(296, 301);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.myDoubleField);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.streamField);
            this.Controls.Add(this.streamLabel);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.myStringField);
            this.Controls.Add(this.myStringLabel);
            this.Controls.Add(this.myIntField);
            this.Controls.Add(this.myIntLabel);
            this.Controls.Add(this.portField);
            this.Controls.Add(this.portLabel);
            this.Controls.Add(this.hostField);
            this.Controls.Add(this.hostLabel);
            this.Name = "SimpleDequeuerForm";
            this.Text = "SimpleDequeuerForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimpleDequeuerForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.portField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            Application.Run(new SimpleDequeuerForm());
        }

        /// <summary>
        /// Connects to the server if the host and port field are set up.
        /// If a connection is established a separate thread is spawned to process results.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectButton_Click(object sender, System.EventArgs eventArgs)
        {
            string server;

            server = "sb://"+ hostField.Text+":"+portField.Value.ToString();

            try
            {
                uri = new StreamBaseURI(server);

            }
            catch (URIException e)
            {
                MessageBox.Show("Invalid URI ("+e.ToString()+")");
                return; // EXIT
            }

            // Connect to the server
            try
            {
                client = new StreamBaseClient(uri);
            }
            catch (StreamBaseException e)
            {
                MessageBox.Show("Cound not connect to the server at: "+uri.ToString()+" ("+e.ToString()+")");
                return; // EXIT
            }

            //subscribe to the output stream
            try
            {
                client.Subscribe(streamField.Text);
            }
            catch (StreamBaseException e)
            {
                MessageBox.Show("Could not connect to stream: "+streamField.Text+" ("+e.ToString());
                return; // EXIT
            }

            // Grab the actual field objects from the schema -- it's more efficient
            // to access tupole fields using the actual Schema.Field object than it
            // is using its name...
            StreamProperties props = client.GetStreamProperties(streamField.Text);
            Schema schema = props.GetSchema();
            myInt = schema.GetField("myint");
            myString = schema.GetField("mystring");
            myDouble = schema.GetField("mydouble");

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

            connectButton.Enabled = false;
            hostField.Enabled = false;
            portField.Enabled = false;
            streamField.Enabled = false;
            disconnectButton.Enabled = true;
        }

        private void disconnectButton_Click(object sender, System.EventArgs e)
        {
            client.Close(); // This will make the dequeuer thread exit
            dequeueThread.Join();
            connectButton.Enabled = true;
            hostField.Enabled = true;
            portField.Enabled = true;
            streamField.Enabled = true;
            disconnectButton.Enabled = false;

        }

        private void hostField_TextChanged(object sender, System.EventArgs e)
        {
            if ((hostField.Text != "") && (streamField.Text != ""))
            {
                connectButton.Enabled = true;
            }
            else
            {
                connectButton.Enabled = false;
            }
        }

        private void streamField_TextChanged(object sender, System.EventArgs e)
        {
            if ((hostField.Text != "") && (streamField.Text != ""))
            {
                connectButton.Enabled = true;
            }
            else
            {
                connectButton.Enabled = false;
            }
        }

        // Call back method for marshalling into main thread
        // This method is run within the main windows loop thread

        private void ReceiveTuple(Tuple tuple)
        {
            // get the integer
            try
            {
                if (tuple.IsNull(myInt))
                {
                    myIntField.Text = "null";
                }
                else
                {
                    myIntField.Text = tuple.GetInt(myInt).ToString();
                }
            }
            catch (TupleException e)
            {
                MessageBox.Show("Problem with field myint ("+e.ToString()+")");
                return; // EXIT
            }

            // get the string
            try
            {
                if (tuple.IsNull(myString))
                {
                    myStringField.Text = "null";
                }
                else
                {
                    myStringField.Text = tuple.GetString(myString);
                }
            }
            catch (TupleException e)
            {
                MessageBox.Show("Problem with field mystring ("+e.ToString()+")");
                return; // EXIT
            }
        
            // get the double
            try
            {
                if (tuple.IsNull(myDouble))
                {
                    myDoubleField.Text = "null";
                }
                else
                {
                    myDoubleField.Text = tuple.GetDouble(myDouble).ToString();
                }
            }
            catch (TupleException e)
            {
                MessageBox.Show("Problem with field mydouble ("+e.ToString()+")");
                return; // EXIT
            }
        }

        // Separate Thread for processing results

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
                        if (result.GetStreamName() == streamField.Text)
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
                Console.WriteLine("Could not dequeue from stream: " + streamField.Text + " (" + e.ToString());
                return; // Exit thread
            }
        }

        private void SimpleDequeuerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Closing the client's connection will make the dequeuer thread return as well
            if (client != null && !client.IsClosed())
                client.Close();
            if (dequeueThread != null)
                dequeueThread.Join(1000);
            Application.Exit();
        }
    }
}
