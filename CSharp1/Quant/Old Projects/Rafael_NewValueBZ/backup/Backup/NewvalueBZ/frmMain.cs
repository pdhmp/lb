using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NCommonTypes;

namespace NewValueBZ
{
    public partial class frmMain : Form
    {
        ValueBZ_Runner thisRunner;
        ValueBZ_Sim thisSim;

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime TimeCounter = DateTime.Now;
            //thisRunner = new ValueBZ_Runner();
            thisSim = new ValueBZ_Sim();
            double TimeTaken = DateTime.Now.Subtract(TimeCounter).TotalMilliseconds;
            SendPerfToClipBoard();
            PrintStratItems();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //thisRunner.Stop();
        }

        private void SendPerfToClipBoard()
        {
            string tempData = "";

            foreach (KeyValuePair<DateTime, ValueBZ_Sim.StatItem> curKvp in thisSim.SimData)
            {
                tempData = tempData + curKvp.Key.ToString("yyyy-MM-dd") + "\t" + curKvp.Value.Performance + "\r\n";
            }

            Clipboard.SetText(tempData);

        }

        private void PrintStratItems()
        {
            string tempData = "";

            foreach (KeyValuePair<DateTime, ValueBZ_Sim.StatItem> curKvp in thisSim.SimData)
            {
                foreach(TickerPE curValueItem in curKvp.Value.StratPositions)
                {
                    tempData = tempData + curKvp.Key.ToString("yyyy-MM-dd") + "\t" + curKvp.Value.Performance.ToString("0.00%") + "\t";
                    tempData = tempData + curValueItem.IdTickerComposite + "\t" + curValueItem.IdTicker + "\t" + curValueItem.closeSignal + "\t" + curValueItem.closeEYield.ToString("0.00%") + "\t" + curValueItem.DayTR.ToString("0.00%") + "\t" + curValueItem.StratContrib.ToString("0.0000%") + "\r\n";
                }
            }

            Clipboard.SetText(tempData);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            foreach (ValueCalc curValueCalc in thisRunner.SectorCalcs.Values)
            {
                curValueCalc.StratRecalc();

                Console.WriteLine("\r\n\r\n");
                Console.WriteLine(curValueCalc.IdTickerComposite + "\t-\t" + curValueCalc.curMedianPE);
                Console.WriteLine("\r\n");
                foreach (KeyValuePair<int, TickerPE> curValueItem in curValueCalc.PositionPEs)
                {
                    Console.WriteLine(curValueItem.Key + "\t" + curValueItem.Value.EPSDate + "\t" + curValueItem.Value.EPSKnownDate + "\t" + curValueItem.Value.adjEPS + "\t" + curValueItem.Value.curPE + "\t" + curValueItem.Value.curSignal);
                }
            }
            */
        }
    }
}