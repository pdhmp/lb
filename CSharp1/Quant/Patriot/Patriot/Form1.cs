using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using System.IO;

namespace Patriot
{
    public partial class frmPatriot : Form
    {

        private DateTime curDate;
        private DateTime StartTime;

        bool running = false;

        private SortedDictionary<DateTime, double> DayPnL = new SortedDictionary<DateTime, double>();

        public frmPatriot()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            using (newNestConn curConn = new newNestConn())
            {
                try
                {
                    DateTime lastIbovDate = curConn.Return_DateTime("SELECT MAX(SRDATE) FROM NESTDB.DBO.TB053_PRECOS_INDICES (NOLOCK) WHERE IDSECURITY = 1073");
                    dtpIniDate.Value = lastIbovDate;
                    dtpLastDate.Value = lastIbovDate;
                    tbNumThreads.Text = "2";
                }
                catch
                {
                    MessageBox.Show("Unable to get last Bovespa trading date.");
                }
            }

            timer1.Start();
        }

        private void StartRunning()
        {
            StartTime = DateTime.Now;
            PatriotData.Instance.InformationString.Add(StartTime.ToString("hh:mm:ss.fffff") + "\tStarted");                       

            PatriotData.Instance.IniDate = dtpIniDate.Value.AddDays(-100);
            PatriotData.Instance.InitializeObjects();

            curDate = dtpLastDate.Value;

            StartRunner(curDate);
        }

        private void StartRunner(DateTime RunDate)
        {
            PatriotData.Instance.InformationString.Add(DateTime.Now.ToString("hh:mm:ss.fffff") + "\tStarting runner for date " + RunDate.ToString("dd-MM-yyyy"));            

            PatriotRunner curRunner = new PatriotRunner();
            curRunner.OnFinished += new EventHandler(curRunner_OnFinished);


            curRunner.Trigger1 = 0.30;
            curRunner.Trigger2 = 0.35;
            curRunner.minGL = 1;
            curRunner.minPL = 100;
            curRunner.numThreads = int.Parse(tbNumThreads.Text);
            curRunner.Window1 = 42;
            curRunner.Window2 = 21;
            curRunner.Window3 = 10;
            curRunner.curDate = RunDate;

            curRunner.LoadPairs();
            curRunner.StartRunner();
        }

        void curRunner_OnFinished(object sender, EventArgs e)
        {
            PatriotRunner curRunner = (PatriotRunner)sender;

            DayPnL.Add(curRunner.curDate, curRunner.DateGain);

            PatriotData.Instance.InformationString.Add(DateTime.Now.ToString("hh:mm:ss.fffff") + "\tRunner for date " + curRunner.curDate.ToString("dd-MM-yyyy") + " finished. Elapsed time: " + TimeSpan.FromTicks(DateTime.Now.Ticks).Subtract(TimeSpan.FromTicks(curRunner.StartTime.Ticks)));                

            curDate = PatriotData.Instance.GetPrevDate(curDate);
            if (curDate >= dtpIniDate.Value)
            {                
                StartRunner(curDate);
            }
            else
            {
                StreamWriter swBT = new StreamWriter(@"C:\Quant\Strategies\Patriot\Patriot_BT_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".xls", false);

                foreach (KeyValuePair<DateTime, double> kvp in DayPnL)
                {
                    swBT.WriteLine(kvp.Key.ToString("yyyyMMdd") + "\t" + kvp.Value);
                }

                swBT.Close();

                PatriotData.Instance.InformationString.Add(DateTime.Now.ToString("hh:mm:ss.fffff") + "\tAll runners finished. Elapsed time: " + TimeSpan.FromTicks(DateTime.Now.Ticks).Subtract(TimeSpan.FromTicks(StartTime.Ticks)));

                running = false;
            }           
        }

        double T1Max = 0.5;
        double T1Min = 0.1;        

        double T2Ini = 0.45;
        

        private void StartNextRunner()
        {
            //ParamCounter = ParamCounter % 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DayPnL.Clear();
            StartRunning();
            button1.Enabled = false;
            running = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {            
            tbInfo.Lines = PatriotData.Instance.InformationString.ToArray();
            if (!running)
            {
                button1.Enabled = true;
            }
        }
    }
}
