using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Feed_Bloomberg.Engine;


namespace Feed_Bloomberg
{
    public partial class Form1 : Form
    {

        Feed_BBG feed_Bloomberg;
        Backup_Reuters backup_Reuters;
        Feed_Fundamentals feed_Fundamentals;

        public Form1()
        {
            InitializeComponent();
            //Log.AddLogEntry("Form1.cs Ponto 1");
            //StartBBG();
            RunFeed_Fundamentals();
        }

        private void btnFutStart_Click(object sender, EventArgs e)
        {
            StartBBG();
        }

        private void RunFeed_Fundamentals()
        {
            this.feed_Fundamentals = new Feed_Fundamentals();
            feed_Fundamentals.Start();
        }


        private void StartBBG()
        {
            Log.AddLogEntry("Form1.cs Ponto 2");
            Log.Log_Event(3, 103, 3, "User requested BBG Futures feeder to start");

            Log.AddLogEntry("Form1.cs Ponto 3");
            this.feed_Bloomberg = new Feed_BBG();

            Log.AddLogEntry("Form1.cs Ponto 4");
            this.feed_Bloomberg.Start();

            Log.AddLogEntry("Form1.cs Ponto 5");
            this.feed_Bloomberg.StartCheckInLog(3);

            Log.AddLogEntry("Form1.cs Ponto 6");
            this.btnBBGStart.Enabled = false;
            this.btnBBGStop.Enabled = true;

            Log.AddLogEntry("Form1.cs Ponto 7");
        }

        private void btnBBGStop_Click(object sender, EventArgs e)
        {

            Log.Log_Event(3, 103, 3, "User requested BBG Futures feeder to stop");

            this.feed_Bloomberg.Stop();
            this.feed_Bloomberg.StopCheckInLog();
            this.feed_Bloomberg = null;
            GC.Collect();
            this.btnBBGStart.Enabled = true;
            this.btnBBGStop.Enabled = false;

        }

        private void btnBKPStart_Click(object sender, EventArgs e)
        {

            Log.Log_Event(3, 103, 3, "User requested BBG Reuters Backup feeder to start");

            this.backup_Reuters = new Backup_Reuters();
            this.backup_Reuters.Start();
            this.btnBKPStart.Enabled = false;
            this.btnBKPStop.Enabled = true;
        }

        private void btnBKPStop_Click(object sender, EventArgs e)
        {

            Log.Log_Event(3, 103, 3, "User requested BBG Reuters Backup feeder to stop");
            
            this.backup_Reuters.Stop();
            this.backup_Reuters = null;
            GC.Collect();
            this.btnBKPStart.Enabled = true;
            this.btnBKPStop.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string status = "";

            if (this.feed_Bloomberg != null)
            {
                status = status +
                         "Bloomberg feeder last update: " +
                         this.feed_Bloomberg.lastUpdate.ToString("G") + "\r\n";

            }
            if (this.backup_Reuters != null)
            {
                status = status +
                         "BBG Reuters Backup feeder last update: " +
                         this.backup_Reuters.lastUpdate.ToString("G") + "\r\n";
            }

            this.txtStatus.Text = status ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            btnFutStart_Click(sender, e);
        }
    }
}