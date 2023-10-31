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

        private SortedDictionary<DateTime, SortedDictionary<double, SortedDictionary<double, double>>> DayPnL = new SortedDictionary<DateTime, SortedDictionary<double, SortedDictionary<double, double>>>();
        private SortedDictionary<DateTime, SortedDictionary<double, SortedDictionary<double, double>>> DayGross = new SortedDictionary<DateTime, SortedDictionary<double, SortedDictionary<double, double>>>();
        private SortedDictionary<DateTime, SortedDictionary<double, SortedDictionary<double, double>>> QtySymbols = new SortedDictionary<DateTime, SortedDictionary<double, SortedDictionary<double, double>>>();

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

            //curT1 = 0.2;
            //curT2 = 0.3;

            curT1 = T1Max;
            curT2 = T2Max;

            StartRunner(curDate, curT1, curT2);
        }

        private void StartRunner(DateTime RunDate, double T1, double T2)
        {
            PatriotData.Instance.InformationString.Add(DateTime.Now.ToString("hh:mm:ss.fffff") + "\tStarting runner for date " + RunDate.ToString("dd-MM-yyyy"));            

            PatriotRunner curRunner = new PatriotRunner();
            curRunner.OnFinished += new EventHandler(curRunner_OnFinished);


            curRunner.Trigger1 = T1;
            curRunner.Trigger2 = T2;
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

            if (!DayPnL.ContainsKey(curRunner.curDate)) { DayPnL.Add(curRunner.curDate, new SortedDictionary<double,SortedDictionary<double,double>>()); }
            if(!DayPnL[curRunner.curDate].ContainsKey(curRunner.Trigger2)){DayPnL[curRunner.curDate].Add(curRunner.Trigger2,new SortedDictionary<double,double>());}
            
            //DayPnL.Add(curRunner.curDate, curRunner.DateGain);
            DayPnL[curRunner.curDate][curRunner.Trigger2].Add(curRunner.Trigger1,curRunner.DateGain);

            if (!DayGross.ContainsKey(curRunner.curDate)) { DayGross.Add(curRunner.curDate, new SortedDictionary<double, SortedDictionary<double, double>>()); }
            if (!DayGross[curRunner.curDate].ContainsKey(curRunner.Trigger2)) { DayGross[curRunner.curDate].Add(curRunner.Trigger2, new SortedDictionary<double, double>()); }

            //DayGross.Add(curRunner.curDate, curRunner.DateGain);
            DayGross[curRunner.curDate][curRunner.Trigger2].Add(curRunner.Trigger1, curRunner.GrossSize);

            if (!QtySymbols.ContainsKey(curRunner.curDate)) { QtySymbols.Add(curRunner.curDate, new SortedDictionary<double, SortedDictionary<double, double>>()); }
            if (!QtySymbols[curRunner.curDate].ContainsKey(curRunner.Trigger2)) { QtySymbols[curRunner.curDate].Add(curRunner.Trigger2, new SortedDictionary<double, double>()); }

            //QtySymbols.Add(curRunner.curDate, curRunner.DateGain);
            QtySymbols[curRunner.curDate][curRunner.Trigger2].Add(curRunner.Trigger1, curRunner.QtdSymbols);

            PatriotData.Instance.InformationString.Add(DateTime.Now.ToString("hh:mm:ss.fffff") + "\tRunner for date " + curRunner.curDate.ToString("dd-MM-yyyy") + " finished. Elapsed time: " + TimeSpan.FromTicks(DateTime.Now.Ticks).Subtract(TimeSpan.FromTicks(curRunner.StartTime.Ticks)));

            StartNextRunner();                       
        }        

        double T1Max = 0.30;
        double T1Min = 0.30;
        double curT1;

        double T2Max = 0.35;
        double T2Min = 0.35;

        double step = 0.05;
        double curT2;
        

        private void StartNextRunner()
        {
            bool runNext = true;

            curT1 -= step;
            curT1 = Math.Round(curT1, 4);

            if (curT1 < T1Min)
            {
                curT2 -= step;
                curT2 = Math.Round(curT2, 4);
                
                if (curT2 < T2Min)
                {
                    curDate = PatriotData.Instance.GetPrevDate(curDate);
                    curT2 = T2Max;                   

                    if (curDate < dtpIniDate.Value)
                    {
                        runNext = false;
                    }                                        
                }
                curT1 = curT2 - step;
            }

            
            
            
            if (runNext)
            {
                StartRunner(curDate,curT1, curT2);
            }
            else
            {
                StreamWriter swBT = new StreamWriter(@"C:\Quant\Strategies\Patriot\Patriot_BT_" + DateTime.Now.ToString("yyyyMMdd_hhmmss") + ".xls", false);
                swBT.WriteLine("Date\tTrigger2\tTrigger1\tDayPnL\tGross\tQtySymbols");

                foreach (KeyValuePair<DateTime, SortedDictionary<double, SortedDictionary<double, double>>> Datekvp in DayPnL)
                {
                    foreach (KeyValuePair<double, SortedDictionary<double, double>> T2kvp in Datekvp.Value)
                    {
                        foreach (KeyValuePair<double, double> T1kvp in T2kvp.Value)
                        {
                            double gross = DayGross[Datekvp.Key][T2kvp.Key][T1kvp.Key];
                            double numSymb = QtySymbols[Datekvp.Key][T2kvp.Key][T1kvp.Key];

                            swBT.WriteLine(Datekvp.Key.ToString("dd/MM/yyyy") + "\t" + T2kvp.Key.ToString() + "\t" + T1kvp.Key.ToString() + "\t" + T1kvp.Value.ToString() + "\t" + gross.ToString() + "\t" + numSymb.ToString());
                        }                       
                    }
                }

                swBT.Close();

                PatriotData.Instance.InformationString.Add(DateTime.Now.ToString("hh:mm:ss.fffff") + "\tAll runners finished. Elapsed time: " + TimeSpan.FromTicks(DateTime.Now.Ticks).Subtract(TimeSpan.FromTicks(StartTime.Ticks)));

                running = false;
            }
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
