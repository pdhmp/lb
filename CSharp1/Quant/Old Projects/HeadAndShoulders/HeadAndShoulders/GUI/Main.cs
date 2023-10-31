using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestSim.HS;
using NestSim.Common;

namespace NestSim
{
    /// <summary>
    /// Initial Form
    /// </summary>
    public partial class Main : Form
    {

        HSParameterTester HSSimSet;
        
        /// <summary>
        /// Initial Form
        /// </summary>
        public Main()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            DateTime start = DateTime.Now;
            
            double HurdleMin, HurdleMax, HurdleInterval;
            double BandMin, BandMax, BandInterval;
            double HorizontalMin, HorizontalMax, HorizontalInterval;

            int ExitBarMin, ExitBarMax, ExitBarInterval;
            double GainMin, GainMax, GainInterval;
            double StopMin, StopMax, StopInterval;
            int ExitStrategy;

            int sideFactor;
            

            if( double.TryParse(txtHurdleMin.Text, out HurdleMin)&&
                double.TryParse(txtHurdleMax.Text, out HurdleMax) &&
                double.TryParse(txtHurdleInterval.Text, out HurdleInterval) &&
                double.TryParse(txtBandMin.Text, out BandMin)&&
                double.TryParse(txtBandMax.Text, out BandMax) &&
                double.TryParse(txtBandInterval.Text, out BandInterval) &&
                double.TryParse(txtHorizontalMin.Text, out HorizontalMin)&&
                double.TryParse(txtHorizontalMax.Text, out HorizontalMax) &&
                double.TryParse(txtHorizontalInterval.Text, out HorizontalInterval) &&
                int.TryParse(txtExitBarMin.Text, out ExitBarMin) &&
                int.TryParse(txtExitBarMax.Text, out ExitBarMax) &&
                int.TryParse(txtExitBarInterval.Text, out ExitBarInterval) &&
                double.TryParse(txtGainMin.Text, out GainMin) &&
                double.TryParse(txtGainMax.Text, out GainMax) &&
                double.TryParse(txtGainInterval.Text, out GainInterval) &&
                double.TryParse(txtStopMin.Text, out StopMin) &&
                double.TryParse(txtStopMax.Text, out StopMax) &&
                double.TryParse(txtStopInterval.Text, out StopInterval) &&
                int.TryParse(txtExitStrategy.Text, out ExitStrategy)&&
                int.TryParse(txtSideFactor.Text, out sideFactor) )
            {
                int[] TickerList = new int[134] {393,715,16054,145,16055,245,347,16056,603,713,794,797,35,36,125,1582,253,266,272,396,458,483,536,556,632,1236,1420,5528,1409,1631,1578,86,16065,89,101,123,4660,448,444,1407,786,1583,1219,1694,595,16058,669,1655,1076,97,234,323,16063,439,454,485,533,570,580,587,612,831,28,30,185,1662,547,1677,1238,1061,599,1701,132,16059,806,3,256,561,1,4,456,1069,16060,710,810,1084,279,300,333,382,1067,1411,1075,654,10,50,260,16061,384,390,800,801,1585,280,422,768,799,151,152,153,306,1072,542,625,720,722,750,752,766,767,818,209,210,223,228,240,241,243,313,315,317,318,476,664};
                HSSimSet = new HSParameterTester(chkIntraday.Checked, TickerList, 
                                                 HurdleMin, HurdleMax, HurdleInterval,
                                                 BandMin, BandMax, BandInterval,
                                                 HorizontalMin,HorizontalMax, HorizontalInterval,
                                                 ExitBarMin, ExitBarMax, ExitBarInterval,
                                                 GainMin, GainMax, GainInterval,
                                                 StopMin, StopMax, StopInterval,
                                                 ExitStrategy, sideFactor, dtpBgnDate.Value,dtpEndDate.Value);
                HSSimSet.RunParameters();
            }

            DateTime finish = DateTime.Now;

            MessageBox.Show("Start: " + start.ToString() + "\r\nFinish: " + finish.ToString());
            
        }

        //private void printHS()
        //{
        //    dataGridView1.Rows.Clear();
            
        //    List<string[]> result = HS.printHS();

        //    foreach (string[] line in result)
        //    {
        //        bool flagAdd = true;

        //        if (chkPeaks.Checked)
        //        {
        //            if (line[2].Equals(""))
        //            {
        //                flagAdd = false;
        //            }
        //        }

        //        if (chkTrough.Checked)
        //        {
        //            if (line[3].Equals(""))
        //            {
        //                flagAdd = false;
        //            }
        //        }

        //        if (chkT3.Checked)
        //        {
        //            if (line[4].Equals(""))
        //            {
        //                flagAdd = false;
        //            }
        //        }

        //        if (flagAdd)
        //        {
        //            dataGridView1.Rows.Add(line);
        //        }

        //    }            
        //}
    }
}