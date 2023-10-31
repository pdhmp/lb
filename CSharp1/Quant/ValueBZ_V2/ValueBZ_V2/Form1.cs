using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using NCommonTypes;

namespace ValueBZ_V2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ValueSim thisSim;
        ValueRunner thisRunner;

        private void Form1_Load(object sender, EventArgs e)
        {
            ValueData.Instance.IniDate = new DateTime(1999, 01, 01);
            ValueData.Instance.InitializeObjects();

            thisRunner = new ValueRunner();

            //thisSim = new ValueSim(new DateTime(2008, 01, 02));

            //PrintStatistics();
            //PrintStratItems();
        }

        private void PrintStratItems()
        {

            StreamWriter fs = new StreamWriter(@"C:\Temp\ValueBZPosition_"+DateTime.Now.ToString("yyyyMMdd_HHmmss")+".xls", false);

            string tempData = "";

            tempData = tempData + "DateSim";

            tempData = tempData + "\t" + "RefDate";
            tempData = tempData + "\t" + "StratPerformance";
            tempData = tempData + "\t" + "Long";
            tempData = tempData + "\t" + "Short";
            tempData = tempData + "\t" + "Net";
            tempData = tempData + "\t" + "Gross";

            tempData = tempData + "\t" + "StratName";
            tempData = tempData + "\t" + "IdTickerComposite";
            tempData = tempData + "\t" + "IdTicker";
            tempData = tempData + "\t" + "Ticker";
            tempData = tempData + "\t" + "EPSDate";
            tempData = tempData + "\t" + "EPSKnownDate";
            tempData = tempData + "\t" + "EPSValue";
            tempData = tempData + "\t" + "EPSShareNumber";
            tempData = tempData + "\t" + "curShareNumber";
            tempData = tempData + "\t" + "curShareNumberDate";
            tempData = tempData + "\t" + "AdjEPS";
            tempData = tempData + "\t" + "curPrice";
            tempData = tempData + "\t" + "curPE";
            tempData = tempData + "\t" + "curEYield";
            tempData = tempData + "\t" + "curSignal";
            tempData = tempData + "\t" + "Weight";

            tempData = tempData + "\t" + "closeAdjEPS";
            tempData = tempData + "\t" + "closeShareNumber";
            tempData = tempData + "\t" + "closePrice";
            tempData = tempData + "\t" + "closePE";
            tempData = tempData + "\t" + "closeEYield";
            tempData = tempData + "\t" + "closeSignal";
            tempData = tempData + "\t" + "DayChange";
            tempData = tempData + "\t" + "DayTR";
            tempData = tempData + "\t" + "CloseWeight";
            tempData = tempData + "\t" + "StratContrib";

            tempData = tempData + "\r\n";


            foreach (KeyValuePair<DateTime, StatItem> curKvp in thisSim.SimData)
            {
                foreach (newTickerPE curValueItem in curKvp.Value.StratPositions)
                {
                    tempData = tempData + curKvp.Key.ToString("yyyy-MM-dd"); //DateSim

                    tempData = tempData + "\t" + curKvp.Value.RefDate.ToString("yyyy-MM-dd"); //RefDate
                    tempData = tempData + "\t" + curKvp.Value.Performance.ToString("0.00%"); //StratPerformance
                    tempData = tempData + "\t" + curKvp.Value.Long.ToString("0.00%"); //StratLong
                    tempData = tempData + "\t" + curKvp.Value.Short.ToString("0.00%"); //StratShort
                    tempData = tempData + "\t" + curKvp.Value.Net.ToString("0.00%"); //StratNet
                    tempData = tempData + "\t" + curKvp.Value.Gross.ToString("0.00%"); //StratGross

                    tempData = tempData + "\t" + curValueItem.StratName;
                    tempData = tempData + "\t" + curValueItem.IdTickerComposite;
                    tempData = tempData + "\t" + curValueItem.IdTicker;
                    tempData = tempData + "\t" + curValueItem.Ticker;

                    tempData = tempData + "\t" + curValueItem.EPSDate;
                    tempData = tempData + "\t" + curValueItem.EPSKnownDate.ToString("yyyy-MM-dd");
                    tempData = tempData + "\t" + curValueItem.EPSValue;
                    tempData = tempData + "\t" + curValueItem.EPSShareNumber;
                    tempData = tempData + "\t" + curValueItem.curShareNumber;
                    tempData = tempData + "\t" + curValueItem.curShareNumberDate.ToString("yyyy-MM-dd");
                    tempData = tempData + "\t" + curValueItem.AdjEPS;
                    tempData = tempData + "\t" + curValueItem.curPrice;
                    tempData = tempData + "\t" + curValueItem.curPE.ToString("0.00");
                    tempData = tempData + "\t" + curValueItem.curEYield.ToString("0.00%");
                    tempData = tempData + "\t" + curValueItem.curSignal;
                    tempData = tempData + "\t" + curValueItem.Weight.ToString("0.00%");

                    tempData = tempData + "\t" + curValueItem.closeAdjEPS;
                    tempData = tempData + "\t" + curValueItem.closeShareNumber;
                    tempData = tempData + "\t" + curValueItem.closePrice;
                    tempData = tempData + "\t" + curValueItem.closePE.ToString("0.00");
                    tempData = tempData + "\t" + curValueItem.closeEYield.ToString("0.00%");
                    tempData = tempData + "\t" + curValueItem.closeSignal;
                    tempData = tempData + "\t" + curValueItem.DayChange.ToString("0.00%");
                    tempData = tempData + "\t" + curValueItem.DayTR.ToString("0.00%");
                    tempData = tempData + "\t" + curValueItem.CloseWeight.ToString("0.00%");
                    tempData = tempData + "\t" + curValueItem.StratContrib.ToString("0.00%");

                    tempData = tempData + "\r\n";
                }

                fs.Write(tempData);
                tempData = "";
            }

            //Clipboard.SetText(tempData);

            fs.Close();
        }
        private void PrintStatistics()
        {
            StreamWriter fs = new StreamWriter(@"C:\Temp\ValueBZBackTest_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls", false);

            string tempData = "";

            tempData = tempData + "DateSim";

            tempData = tempData + "\t" + "RefDate";
            tempData = tempData + "\t" + "StratPerformance";
            tempData = tempData + "\t" + "Long";
            tempData = tempData + "\t" + "Short";
            tempData = tempData + "\t" + "Net";
            tempData = tempData + "\t" + "Gross";

            tempData = tempData + "\r\n";


            foreach (KeyValuePair<DateTime, StatItem> curKvp in thisSim.SimData)
            {
                tempData = tempData + curKvp.Key.ToString("yyyy-MM-dd"); //DateSim

                tempData = tempData + "\t" + curKvp.Value.RefDate.ToString("yyyy-MM-dd"); //RefDate
                tempData = tempData + "\t" + curKvp.Value.Performance.ToString("0.00%"); //StratPerformance
                tempData = tempData + "\t" + curKvp.Value.Long.ToString("0.00%"); //StratLong
                tempData = tempData + "\t" + curKvp.Value.Short.ToString("0.00%"); //StratShort
                tempData = tempData + "\t" + curKvp.Value.Net.ToString("0.00%"); //StratNet
                tempData = tempData + "\t" + curKvp.Value.Gross.ToString("0.00%"); //StratGross

                tempData = tempData + "\r\n";


                fs.Write(tempData);
                tempData = "";
            }

            //Clipboard.SetText(tempData);

            fs.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            NestSymConn.SymConn.Instance.Dispose();
        }

    }
}
