using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace PriceCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TimerCallback tmrCB;
        System.Threading.Timer timer;

        private void Form1_Load(object sender, EventArgs e)
        {
            tmrCB = new TimerCallback(Calculate);
            //timer = new System.Threading.Timer(tmrCB, null, new TimeSpan(20, 00, 00),new TimeSpan(0,0,0,0,-1));
            timer = new System.Threading.Timer(tmrCB, null, 1000, Timeout.Infinite);
        }

        private void Calculate(object obj)
        {           

            //IndexCalculator IdxPreAuctionPrice = new IndexCalculator(312,new DateTime(2004,01,01),22);
            //IdxPreAuctionPrice.CalculateAllIndexes(new DateTime(2011,11,29));

            PriceCalculator curCalc = new PriceCalculator();
            curCalc.CalculateAverage();

            MessageBox.Show("Calculation finished");
        }
    }
}
