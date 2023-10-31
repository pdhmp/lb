using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using NestDLL;

namespace RTICKOneMinBarReaderOffshore
{
    public partial class Form1 : Form
    {
        Thread Start;
        RTICKFile curRTICKFile = new RTICKFile();
        RTICKBars curRTICKBars = new RTICKBars();

        public Form1()
        {
            InitializeComponent();
        }

        public void Run()
        {
            //curRTICKFile.ProcessFolder(@"R:\RTICK\OneMinuteBars\OffShore\Unprocessed");

            string curFileName = @"C:\EUROPE\luis.fonseca@nestinvestimentos.com.br-EUROPELUIS-N35545701.csv";

            //curRTICKBars.ProcessFile(curFileName);
            //curRTICKBars.PrintDataToFile(curFileName);
            curRTICKBars.InsertFileInDataBase(@"C:\EUROPE\DATA");
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            Start = new Thread(new ThreadStart(Run));
            Start.Start();
        }
    }

    public class OneMinBar
    {
        public DateTime Date;
        public double Minute;
        public double RegressiveMinute;
        public string Ticker;
        public double Open = 0;
        public double Last = 0;
        public double High = 0;
        public double Low = 0;
        public Int64 Volume = 0;
        public int NoTrades = 0;
        public double VWAP = 0;
        public double OpenBid = 0;
        public double CloseBid = 0;
        public double HighBid = 0;
        public double LowBid = 0;
        public double OpenAsk = 0;
        public double CloseAsk = 0;
        public double HighAsk = 0;
        public double LowAsk = 0;
    }
}
