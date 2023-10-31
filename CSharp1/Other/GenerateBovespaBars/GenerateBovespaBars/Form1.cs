using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NestDLL;

namespace GenerateBovespaBars
{
    public partial class Form1 : Form
    {
        string curPath = @"C:\BOV DATA\Negocios\";
        string outPutFileName = @"C:\TEMP\TradeSummary.txt";
        DateTime TimeCounter;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            File.Delete(outPutFileName);
            ProcessFolder(curPath);
            TimeSpan TimeTaken = DateTime.Now.Subtract(TimeCounter);
            Console.WriteLine(TimeTaken.TotalMilliseconds.ToString("#,##0") + "ms");
            Console.WriteLine(TimeTaken);
        }

        private void ProcessFolder(string FolderPath)
        {
            TimeCounter = DateTime.Now;

            DirectoryInfo dir = new DirectoryInfo(FolderPath);

            FileInfo[] files = dir.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == ".txt")
                {
                    try
                    {
                        Console.WriteLine(FolderPath + "\\" + files[i].Name);
                        ProccessFile(FolderPath + "\\" + files[i].Name);
                    }
                    catch
                    {
                        int a = 1;
                    }
                }

                TimeSpan TimeTaken = DateTime.Now.Subtract(TimeCounter);
                Console.WriteLine(TimeTaken.TotalMilliseconds.ToString("#,##0") + "ms");
                Console.WriteLine(TimeTaken);
            }
        }

        private void ProccessFile(string ProcFileName)
        {
            StreamReader sr = new StreamReader(ProcFileName);
            StreamWriter sw = new StreamWriter(outPutFileName, true);
            int LineCounter = 0;
            string tempLine = "";
            string[] LineArray;

            DateTime curDate = new DateTime(1900,01,01);
            string curTicker = "";

            int curTradeID = 0;
            double curTradePrice = 0;
            Int64 curTradeVolume = 0;
            TimeSpan curTradeTime = new TimeSpan(0, 0, 0);
            string curCancelFlag = "";
            DateTime curBuyOfferDate = new DateTime(1900, 01, 01);
            int curBuyOfferID = 0;
            DateTime curSellOfferDate = new DateTime(1900, 01, 01);
            int curSellOfferID = 0;

            DateTime totalDate = new DateTime(1900, 01, 01);
            Int64 barShares = 0;
            double barFinAmount = 0;
            double barLast = 0;

            int prevBarNo = 0;
            string prevTicker = "";
            DateTime prevDate = new DateTime(1900, 01, 01);

            string SQLValues = "";
            int SQLCounter = 0;

            while ((tempLine = sr.ReadLine()) != null)
            {
                LineCounter++;
                if (tempLine.Contains("RH NEG"))
                { }
                else if (tempLine.Contains("RT"))
                { }
                else if (tempLine !="")
                {
                    LineArray = tempLine.Split(';');

                    curTicker = LineArray[1].Trim();
                    curTradeTime = TimeSpan.Parse(LineArray[5]);
                    int barNo = (int)(curTradeTime.TotalMinutes / 5);

                    if (barNo != prevBarNo || curTicker != prevTicker)
                    {
                        TimeSpan barTime = TimeSpan.FromMinutes((prevBarNo + 1) * 5);
                        // INSERT DB
                        if (prevTicker != "")
                        {
                            //sw.WriteLine(prevDate.ToShortDateString() + "\t" + barTime.ToString() + "\t" + prevBarNo + "\t" + prevTicker + "\t" + barShares + "\t" + barFinAmount.ToString().Replace(",", ".") + "\t" + barLast.ToString().Replace(",", ".") + "\t" + (barFinAmount / barShares).ToString().Replace(",", "."));
                            SQLValues = SQLValues + " UNION SELECT '" + prevDate.ToString("yyyy-MM-dd") + "', '" + barTime.ToString() + "', " + prevBarNo + ", '" + prevTicker + "', " + barShares + ", " + barFinAmount.ToString().Replace(",", ".") + ", " + barLast.ToString().Replace(",", ".") + ", " + (barFinAmount / barShares).ToString().Replace(",", ".");
                        }
                        // ZERO TOTALS
                        barShares = 0;
                        barFinAmount = 0;
                        barLast = 0;
                        SQLCounter++;
                    }

                    curDate = DateTime.Parse(LineArray[0]);
                    curTradeID = int.Parse(LineArray[2]);
                    curTradePrice = double.Parse(LineArray[3].Replace('.', ','));
                    curTradeVolume = Int64.Parse(LineArray[4]);
                    curCancelFlag = LineArray[6];
                    curBuyOfferDate = DateTime.Parse(LineArray[7]);
                    curBuyOfferID = int.Parse(LineArray[8]);
                    curSellOfferDate = DateTime.Parse(LineArray[9]);
                    curSellOfferID = int.Parse(LineArray[10]);

                    barShares = barShares + curTradeVolume;
                    barFinAmount = barFinAmount + curTradeVolume * curTradePrice;
                    barLast = curTradePrice;

                    prevBarNo = barNo;
                    prevTicker = curTicker;
                    prevDate = curDate;

                    if (SQLCounter > 50)
                    {
                        using (newNestConn curConn = new newNestConn())
                        {
                            curConn.ExecuteNonQuery("INSERT INTO NESTTICK.dbo.Tb000_IntradayBov " + SQLValues.Substring(6));
                            SQLValues = "";
                            SQLCounter = 0;
                        }
                    }
                }
            }

            if (SQLValues != "")
            {
                using (newNestConn curConn = new newNestConn())
                {
                    curConn.ExecuteNonQuery("INSERT INTO NESTTICK.dbo.Tb000_IntradayBov " + SQLValues.Substring(6));
                }
            }

            TimeSpan barTime2 = TimeSpan.FromMinutes((prevBarNo + 1) * 5);
            sw.WriteLine(prevDate.ToShortDateString() + "\t" + barTime2.ToString() + "\t" + prevBarNo + "\t" + prevTicker + "\t" + barShares + "\t" + barFinAmount + "\t" + barLast + "\t" + barFinAmount / barShares);

            sr.Close();
            sw.Close();
        }

    }
}
