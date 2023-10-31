using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;
using System.Data;
using StreamBase.SB;
using StreamBase.SB.Client;
using System.Collections.Generic;
using System.Diagnostics;

namespace SimpleDequeuerForm
{

    /// <summary>
    /// Summary description for SimpleDequeuerForm.
    /// </summary>
    public class SimpleDequeuerForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label hostLabel;
        private System.Windows.Forms.TextBox hostField;
        private System.Windows.Forms.Label portLabel;
        private System.Windows.Forms.NumericUpDown portField;
        private System.Windows.Forms.Button connectButton;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private bool osmXPConnected = false;
        private bool mdBovespaStarted = false;
        private bool nestProject = false;
        /// <summary>
        /// Application variables
        /// </summary>
        /// 
        StreamBaseClient client;
        StreamBaseURI uri;
        private System.Windows.Forms.Label streamLabelOut;
        private System.Windows.Forms.TextBox streamFieldOut;
        DequeueResult result;
        private System.Windows.Forms.Button disconnectButton;
        System.Threading.Thread dequeueThread;
        public delegate void ReceiveTupleDelegate(Tuple tuple);

        //
        // These are used to cache the field objects in the schema;
        // it's more efficient to use Field objects than field names
        // to access tuple values.
        //
        private Schema.Field dExtraInfo;
        private Schema.Field dMsgType;
        private Schema.Field dMsgSeqNum;
        private Schema.Field dSenderCompID;
        private Schema.Field dTargetCompID;


        private const String FIELD1 = "Command";
        //private const String FIELD2 = "mystring";
        //private const String FIELD3 = "mydouble";


        private RichTextBox richTextBox1;
        private ComboBox comboBox1;
        private Button button2;
        private Panel panelConnection;
        private Label streamLabelIn;
        private TextBox streamFieldIn;
        private Label labelPanelConnection;
        private Panel panel1;
        private Label labelPanelFIX;
        private Panel panelStatusFIX;
        private Label labelPanelStatus;
        private Button CancelAllButton;
        private Panel panel2;
        private Button startNestProject;
        private Button startMDBMF;
        private Button startOSMXP;
        private Button OrderManager;
        private Panel panel3;
        private RichTextBox outputCMD;

        private Button button1;
        //private Schema.Field myDouble;


        public SimpleDequeuerForm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            //comboBox1.SelectedText = "Connect";

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
            this.connectButton = new System.Windows.Forms.Button();
            this.streamLabelOut = new System.Windows.Forms.Label();
            this.streamFieldOut = new System.Windows.Forms.TextBox();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.panelConnection = new System.Windows.Forms.Panel();
            this.labelPanelConnection = new System.Windows.Forms.Label();
            this.streamFieldIn = new System.Windows.Forms.TextBox();
            this.streamLabelIn = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CancelAllButton = new System.Windows.Forms.Button();
            this.labelPanelFIX = new System.Windows.Forms.Label();
            this.panelStatusFIX = new System.Windows.Forms.Panel();
            this.labelPanelStatus = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.OrderManager = new System.Windows.Forms.Button();
            this.startNestProject = new System.Windows.Forms.Button();
            this.startMDBMF = new System.Windows.Forms.Button();
            this.startOSMXP = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.outputCMD = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.portField)).BeginInit();
            this.panelConnection.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelStatusFIX.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // hostLabel
            // 
            this.hostLabel.AutoSize = true;
            this.hostLabel.Location = new System.Drawing.Point(39, 52);
            this.hostLabel.Name = "hostLabel";
            this.hostLabel.Size = new System.Drawing.Size(29, 13);
            this.hostLabel.TabIndex = 0;
            this.hostLabel.Text = "Host";
            this.hostLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // hostField
            // 
            this.hostField.Location = new System.Drawing.Point(74, 52);
            this.hostField.Name = "hostField";
            this.hostField.Size = new System.Drawing.Size(96, 20);
            this.hostField.TabIndex = 1;
            this.hostField.Text = "Nestsrv05";
            this.hostField.TextChanged += new System.EventHandler(this.hostField_TextChanged);
            // 
            // portLabel
            // 
            this.portLabel.AutoSize = true;
            this.portLabel.Location = new System.Drawing.Point(42, 85);
            this.portLabel.Name = "portLabel";
            this.portLabel.Size = new System.Drawing.Size(26, 13);
            this.portLabel.TabIndex = 2;
            this.portLabel.Text = "Port";
            this.portLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // portField
            // 
            this.portField.Location = new System.Drawing.Point(74, 85);
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
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(19, 199);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(72, 24);
            this.connectButton.TabIndex = 8;
            this.connectButton.Text = "Connect";
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // streamLabelOut
            // 
            this.streamLabelOut.AutoSize = true;
            this.streamLabelOut.Location = new System.Drawing.Point(11, 118);
            this.streamLabelOut.Name = "streamLabelOut";
            this.streamLabelOut.Size = new System.Drawing.Size(57, 13);
            this.streamLabelOut.TabIndex = 9;
            this.streamLabelOut.Text = "StreamOut";
            this.streamLabelOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // streamFieldOut
            // 
            this.streamFieldOut.Location = new System.Drawing.Point(74, 118);
            this.streamFieldOut.Name = "streamFieldOut";
            this.streamFieldOut.Size = new System.Drawing.Size(96, 20);
            this.streamFieldOut.TabIndex = 10;
            this.streamFieldOut.Text = "FIXAdminOut";
            this.streamFieldOut.TextChanged += new System.EventHandler(this.streamField_TextChanged);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Enabled = false;
            this.disconnectButton.Location = new System.Drawing.Point(107, 199);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(72, 24);
            this.disconnectButton.TabIndex = 11;
            this.disconnectButton.Text = "Disconnect";
            this.disconnectButton.Click += new System.EventHandler(this.disconnectButton_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(174, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 36);
            this.button1.TabIndex = 14;
            this.button1.Text = "OFF";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(14, 30);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(371, 100);
            this.richTextBox1.TabIndex = 15;
            this.richTextBox1.Text = "";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Connect",
            "Logon",
            "LogonAll",
            "Logout",
            "LogoutAll",
            "ConnectionStatus",
            "ConnectionStatusAll",
            "ResetSession",
            "ResetAllSessions",
            "GetMsgSeqNums",
            "SetSenderMsgSeqNum",
            "SetTargetMsgSeqNum"});
            this.comboBox1.Location = new System.Drawing.Point(19, 153);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(188, 21);
            this.comboBox1.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(19, 182);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(188, 24);
            this.button2.TabIndex = 17;
            this.button2.Text = "SendFIXCommand";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panelConnection
            // 
            this.panelConnection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelConnection.Controls.Add(this.labelPanelConnection);
            this.panelConnection.Controls.Add(this.streamFieldIn);
            this.panelConnection.Controls.Add(this.streamLabelIn);
            this.panelConnection.Controls.Add(this.disconnectButton);
            this.panelConnection.Controls.Add(this.connectButton);
            this.panelConnection.Controls.Add(this.button1);
            this.panelConnection.Controls.Add(this.hostField);
            this.panelConnection.Controls.Add(this.hostLabel);
            this.panelConnection.Controls.Add(this.portLabel);
            this.panelConnection.Controls.Add(this.streamFieldOut);
            this.panelConnection.Controls.Add(this.portField);
            this.panelConnection.Controls.Add(this.streamLabelOut);
            this.panelConnection.Location = new System.Drawing.Point(200, 22);
            this.panelConnection.Name = "panelConnection";
            this.panelConnection.Size = new System.Drawing.Size(214, 233);
            this.panelConnection.TabIndex = 18;
            // 
            // labelPanelConnection
            // 
            this.labelPanelConnection.AutoSize = true;
            this.labelPanelConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPanelConnection.Location = new System.Drawing.Point(15, 12);
            this.labelPanelConnection.Name = "labelPanelConnection";
            this.labelPanelConnection.Size = new System.Drawing.Size(160, 17);
            this.labelPanelConnection.TabIndex = 15;
            this.labelPanelConnection.Text = "StreamBase Connection";
            // 
            // streamFieldIn
            // 
            this.streamFieldIn.Location = new System.Drawing.Point(74, 150);
            this.streamFieldIn.Name = "streamFieldIn";
            this.streamFieldIn.Size = new System.Drawing.Size(96, 20);
            this.streamFieldIn.TabIndex = 13;
            this.streamFieldIn.Text = "FIXCommandIn";
            // 
            // streamLabelIn
            // 
            this.streamLabelIn.AutoSize = true;
            this.streamLabelIn.Location = new System.Drawing.Point(19, 150);
            this.streamLabelIn.Name = "streamLabelIn";
            this.streamLabelIn.Size = new System.Drawing.Size(49, 13);
            this.streamLabelIn.TabIndex = 12;
            this.streamLabelIn.Text = "StreamIn";
            this.streamLabelIn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.CancelAllButton);
            this.panel1.Controls.Add(this.labelPanelFIX);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(420, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 233);
            this.panel1.TabIndex = 19;
            // 
            // CancelAllButton
            // 
            this.CancelAllButton.Location = new System.Drawing.Point(19, 74);
            this.CancelAllButton.Name = "CancelAllButton";
            this.CancelAllButton.Size = new System.Drawing.Size(188, 23);
            this.CancelAllButton.TabIndex = 18;
            this.CancelAllButton.Text = "Cancel All";
            this.CancelAllButton.UseVisualStyleBackColor = true;
            this.CancelAllButton.Click += new System.EventHandler(this.CancelAllButton_Click);
            // 
            // labelPanelFIX
            // 
            this.labelPanelFIX.AutoSize = true;
            this.labelPanelFIX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPanelFIX.Location = new System.Drawing.Point(41, 14);
            this.labelPanelFIX.Name = "labelPanelFIX";
            this.labelPanelFIX.Size = new System.Drawing.Size(141, 17);
            this.labelPanelFIX.TabIndex = 16;
            this.labelPanelFIX.Text = "FIX Sessions\' Control";
            // 
            // panelStatusFIX
            // 
            this.panelStatusFIX.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStatusFIX.Controls.Add(this.labelPanelStatus);
            this.panelStatusFIX.Controls.Add(this.richTextBox1);
            this.panelStatusFIX.Location = new System.Drawing.Point(12, 261);
            this.panelStatusFIX.Name = "panelStatusFIX";
            this.panelStatusFIX.Size = new System.Drawing.Size(402, 151);
            this.panelStatusFIX.TabIndex = 20;
            // 
            // labelPanelStatus
            // 
            this.labelPanelStatus.AutoSize = true;
            this.labelPanelStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPanelStatus.Location = new System.Drawing.Point(20, 10);
            this.labelPanelStatus.Name = "labelPanelStatus";
            this.labelPanelStatus.Size = new System.Drawing.Size(140, 17);
            this.labelPanelStatus.TabIndex = 18;
            this.labelPanelStatus.Text = "FIX Status Messages";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.OrderManager);
            this.panel2.Controls.Add(this.startNestProject);
            this.panel2.Controls.Add(this.startMDBMF);
            this.panel2.Controls.Add(this.startOSMXP);
            this.panel2.Location = new System.Drawing.Point(12, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(182, 233);
            this.panel2.TabIndex = 21;
            // 
            // OrderManager
            // 
            this.OrderManager.Location = new System.Drawing.Point(23, 174);
            this.OrderManager.Name = "OrderManager";
            this.OrderManager.Size = new System.Drawing.Size(127, 40);
            this.OrderManager.TabIndex = 3;
            this.OrderManager.Text = "OrderManager";
            this.OrderManager.UseVisualStyleBackColor = true;
            this.OrderManager.Click += new System.EventHandler(this.OrderManager_Click);
            // 
            // startNestProject
            // 
            this.startNestProject.Location = new System.Drawing.Point(23, 118);
            this.startNestProject.Name = "startNestProject";
            this.startNestProject.Size = new System.Drawing.Size(127, 40);
            this.startNestProject.TabIndex = 2;
            this.startNestProject.Text = "NestProject";
            this.startNestProject.UseVisualStyleBackColor = true;
            // 
            // startMDBMF
            // 
            this.startMDBMF.Location = new System.Drawing.Point(23, 65);
            this.startMDBMF.Name = "startMDBMF";
            this.startMDBMF.Size = new System.Drawing.Size(127, 40);
            this.startMDBMF.TabIndex = 1;
            this.startMDBMF.Text = "MD BMF";
            this.startMDBMF.UseVisualStyleBackColor = true;
            // 
            // startOSMXP
            // 
            this.startOSMXP.Location = new System.Drawing.Point(23, 14);
            this.startOSMXP.Name = "startOSMXP";
            this.startOSMXP.Size = new System.Drawing.Size(127, 40);
            this.startOSMXP.TabIndex = 0;
            this.startOSMXP.Text = "OSM XP";
            this.startOSMXP.UseVisualStyleBackColor = true;
            this.startOSMXP.Click += new System.EventHandler(this.startOSMXP_Click);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.outputCMD);
            this.panel3.Location = new System.Drawing.Point(420, 261);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(225, 151);
            this.panel3.TabIndex = 22;
            // 
            // outputCMD
            // 
            this.outputCMD.Location = new System.Drawing.Point(3, 30);
            this.outputCMD.Name = "outputCMD";
            this.outputCMD.Size = new System.Drawing.Size(204, 100);
            this.outputCMD.TabIndex = 0;
            this.outputCMD.Text = "";
            // 
            // SimpleDequeuerForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(657, 426);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelStatusFIX);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelConnection);
            this.Name = "SimpleDequeuerForm";
            this.Text = "Nest Investimentos - StreamBase Mini Panel";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SimpleDequeuerForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.portField)).EndInit();
            this.panelConnection.ResumeLayout(false);
            this.panelConnection.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelStatusFIX.ResumeLayout(false);
            this.panelStatusFIX.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

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
                button1.BackColor = Color.Green;
            }
            catch (StreamBaseException e)
            {
                MessageBox.Show("Cound not connect to the server at: "+uri.ToString()+" ("+e.ToString()+")");
                return; // EXIT
            }

            //subscribe to the output stream
            try
            {
                client.Subscribe(streamFieldOut.Text);
            }
            catch (StreamBaseException e)
            {
                MessageBox.Show("Could not connect to stream: "+streamFieldOut.Text+" ("+e.ToString());
                return; // EXIT
            }

            // Grab the actual field objects from the schema -- it's more efficient
            // to access tuple fields using the actual Schema.Field object than it
            // is using its name...
            StreamProperties props = client.GetStreamProperties(streamFieldOut.Text);
            Schema schema = props.GetSchema();
            dExtraInfo = schema.GetField("__ExtraInfo");
            dMsgType = schema.GetField("MsgType");
            dMsgSeqNum = schema.GetField("MsgSeqNum");
            dSenderCompID = schema.GetField("SenderCompID");
            dTargetCompID = schema.GetField("TargetCompID");


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
            streamFieldOut.Enabled = false;
            streamFieldIn.Enabled = false;
            disconnectButton.Enabled = true;
            button1.BackColor = Color.Green;
            button1.Text = "ON";
            CancelAllButton.Enabled = true;
        }

        private void disconnectButton_Click(object sender, System.EventArgs e)
        {
            client.Close(); // This will make the dequeuer thread exit
            dequeueThread.Join();
            connectButton.Enabled = true;
            hostField.Enabled = true;
            portField.Enabled = true;
            streamFieldOut.Enabled = true;
            streamFieldIn.Enabled = true;
            disconnectButton.Enabled = false;
            button1.BackColor = Color.Red;
            button1.Text = "OFF";
            CancelAllButton.Enabled = false;
        }

        private void hostField_TextChanged(object sender, System.EventArgs e)
        {
            if ((hostField.Text != "") && (streamFieldOut.Text != ""))
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
            if ((hostField.Text != "") && (streamFieldOut.Text != ""))
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
            // set current time
            richTextBox1.AppendText(DateTime.Now.TimeOfDay.ToString()+": ");
            
            // get ExtraInfo
            try
            {
                if (tuple.IsNull(dExtraInfo))
                {
                    richTextBox1.AppendText("<No ExtraInfo>");
                }
                else
                {
                    richTextBox1.AppendText(tuple.GetString(dExtraInfo).ToString()+": ");
                }
            }
            catch (TupleException e)
            {
                MessageBox.Show("Problem with field ExtraInfo ("+e.ToString()+")");
                return; // EXIT
            }
            
            // get SenderCompID
            try
            {
                if (tuple.IsNull(dSenderCompID))
                {
                    richTextBox1.AppendText("<No SenderCompID available for this session>");
                }
                else
                {
                    richTextBox1.AppendText(tuple.GetString(dSenderCompID).ToString());
                }
            }
            catch (TupleException e)
            {
                MessageBox.Show("Problem with field SenderCompID ("+e.ToString()+")");
                return; // EXIT
            }
        
            // get TargetCompID
            try
            {
                if (tuple.IsNull(dTargetCompID))
                {
                    richTextBox1.AppendText("<No TargetCompID available for this session>\n");
                }
                else
                {
                    richTextBox1.AppendText(tuple.GetString(dTargetCompID).ToString()+"\n");
                }
            }
            catch (TupleException e)
            {
                MessageBox.Show("Problem with field TargetCompID ("+e.ToString()+")");
                return; // EXIT
            }
            richTextBox1.ScrollToCaret();
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
                        if (result.GetStreamName() == streamFieldOut.Text)
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
                Console.WriteLine("Could not dequeue from stream: " + streamFieldOut.Text + " (" + e.ToString());
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

        private void button2_Click(object sender, System.EventArgs eventArgs)
        {
            if (client == null || client.IsClosed())
                MessageBox.Show("Please connect before sending FIX command.");
            else
            {
                try
                {
                    // create a Tuple object that we will reuse each time we enqueue.
                    Schema schema = client.GetSchemaForStream(streamFieldIn.Text);
                    Tuple tuple = schema.CreateTuple();

                    // turn the field names into Field objects outside the enqueue loop,
                    // for efficiency of the loop
                    Schema.Field field1 = schema.GetField(FIELD1);

                    // initialize the tuple's fields
                    tuple.SetString(field1, comboBox1.SelectedItem.ToString());

                    // enqueue the tuple
                    client.Enqueue(streamFieldIn.Text, tuple);
                }
                catch (StreamBaseException e)
                {
                    MessageBox.Show("Could not enqueue to stream: " + streamFieldIn.Text + " (" + e.ToString());
                    return; // EXIT
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.BackColor == Color.Red)
                connectButton_Click(sender, e);
            else
                disconnectButton_Click(sender, e);
        }

        private void CancelAllButton_Click(object sender, System.EventArgs eventArgs)
        {
            if (client == null || client.IsClosed())
                MessageBox.Show("Please connect before trying to Cancel All Oustanding Orders.");
            else
            {
                try
                {
                    // create a Tuple object that we will reuse each time we enqueue.
                    Schema schema = client.GetSchemaForStream("CancelAll");
                    Tuple tuple = schema.CreateTuple();

                    // turn the field names into Field objects outside the enqueue loop,
                    // for efficiency of the loop
                    // Schema.Field field1 = schema.GetField(FIELD1);

                    // initialize the tuple's fields
                    // tuple.SetString(field1, comboBox1.SelectedItem.ToString());

                    // enqueue the tuple
                    client.Enqueue("CancelAll", tuple);
                }
                catch (StreamBaseException e)
                {
                    MessageBox.Show("Could not enqueue to stream: " + "CancelAll" + " (" + e.ToString());
                    return; // EXIT
                }
            }
        }

        private void startOSMXP_Click(object sender, EventArgs e)
        {
            if (!osmXPConnected)
            {
                Process.Start(@"C:\SBControls\startOSMXP.bat");
                
                //proc.WaitForExit();
                startOSMXP.BackColor = Color.Green;
                osmXPConnected = true;
            }
            else
            {
                System.Diagnostics.Process.Start(@"C:\SBControls\stopOSMXP.bat");
                startOSMXP.BackColor = Color.Red;
                osmXPConnected = false;
            }
        }

        private void OrderManager_Click(object sender, EventArgs e)
        {
            OrderManager om = new OrderManager();
            om.Show();
        }
        /*
        private void startMDBMF_Click(object sender, EventArgs e)
        {
            if (!osmXPConnected)
            {
                System.Diagnostics.Process.Start(@"C:\SBControls\startMDBMF.bat");
                startMDBMF.BackColor = Color.Green;
            }
            else
            {
                System.Diagnostics.Process.Start(@"C:\SBControls\stopMDBMF.bat");
                startMDBMF.BackColor = Color.Red;
            }
        }

        private void startNestProject_Click(object sender, EventArgs e)
        {
            if (!osmXPConnected)
            {
                System.Diagnostics.Process.Start(@"C:\SBControls\startNP.bat");
                startNestProject.BackColor = Color.Green;
            }
            else
            {
                System.Diagnostics.Process.Start(@"C:\SBControls\stopNP.bat");
                startNestProject.BackColor = Color.Red;
            }
        }

        private void OrderManager_Click(object sender, EventArgs e)
        {
            OrderManager om = new OrderManager();
            om.Show();
        }*/
    }
}
