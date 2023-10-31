using System;
using System.IO;
using System.Windows.Forms;

namespace FIX_BMF
{
    public partial class frmReadLog : Form
    {
        double[,] LoadedLines = new double[10000, 4];
        int nextLine = 0;
        string strMessages = "";
        string PrintMode = "";

        public frmReadLog()
        {
            InitializeComponent();
        }

        private void frmReadLog_Load(object sender, EventArgs e)
        {

        }

        private void cmdLoadAsk_Click(object sender, EventArgs e)
        {
            nextLine = 0;
            strMessages = "";
            try
            {

                StreamReader sr = File.OpenText(@"T:\\Log\\depthlog_ASK.txt");

                string curLine = "";

                curLine = sr.ReadLine();

                while (curLine != null)
                {
                    string[] curArray = curLine.Split('\t');
                    for (int i = 0; i < curArray.Length; i++)
                    {
                        LoadedLines[nextLine, i] = double.Parse(curArray[i]);
                    }
                    nextLine++;
                    curLine = sr.ReadLine();
                }

                sr.Close();

                txtEndLine.Text = nextLine.ToString();
                PrintDepthAsk(nextLine);

                PrintMode = "ASK";
            }
            catch { }
        }

        private void cmdLoadBid_Click(object sender, EventArgs e)
        {
            nextLine = 0;
            strMessages = "";
            try
            {
                StreamReader sr = File.OpenText(@"T:\\Log\\depthlog_BID.txt");

                string curLine = "";

                curLine = sr.ReadLine();

                while (curLine != null)
                {
                    string[] curArray = curLine.Split('\t');
                    for (int i = 0; i < curArray.Length; i++)
                    {
                        LoadedLines[nextLine, i] = double.Parse(curArray[i]);
                    }
                    nextLine++;
                    curLine = sr.ReadLine();
                }

                sr.Close();

                txtEndLine.Text = nextLine.ToString();
                PrintDepthBid(nextLine);

                PrintMode = "BID";
            }
            catch { }
        }

        private void PrintDepthBid(int UntilPos)
        {
            strMessages = "";
            MarketDepthItem curItem = new MarketDepthItem();

            if (UntilPos > nextLine)
            {
                UntilPos = nextLine;
            }

            for (int i = 0; i <= UntilPos; i++)
            {
                if (LoadedLines[i, 0] == 0) curItem.Bid_New((int)LoadedLines[i, 1], LoadedLines[i, 2], (int)LoadedLines[i, 3]);
                if (LoadedLines[i, 0] == 1) curItem.Bid_Update((int)LoadedLines[i, 1], LoadedLines[i, 2], (int)LoadedLines[i, 3]);
                if (LoadedLines[i, 0] == 2) curItem.Bid_Delete((int)LoadedLines[i, 1]);
            }

            string tempLine = "";

            AddMessage(curItem.Ticker + "CurPos: " + "\t\t" + UntilPos.ToString() + "\r\n");
            if (LoadedLines[UntilPos, 0] != 2) tempLine = "\t" + LoadedLines[UntilPos, 2].ToString() + "\t" + LoadedLines[UntilPos, 3].ToString();
            AddMessage(curItem.Ticker + "Last Change: " + "\t" + LoadedLines[UntilPos, 0].ToString() + "\t" + LoadedLines[UntilPos, 1].ToString() + tempLine);
            AddMessage("\r\n");
            AddMessage(curItem.Ticker + "Bid1" + "\t\t" + curItem.BidSize1.ToString("0") + '\t' + curItem.Bid1.ToString("0.00"));
            AddMessage(curItem.Ticker + "Bid2" + "\t\t" + curItem.BidSize2.ToString("0") + '\t' + curItem.Bid2.ToString("0.00"));
            AddMessage(curItem.Ticker + "Bid3" + "\t\t" + curItem.BidSize3.ToString("0") + '\t' + curItem.Bid3.ToString("0.00"));
            AddMessage(curItem.Ticker + "Bid4" + "\t\t" + curItem.BidSize4.ToString("0") + '\t' + curItem.Bid4.ToString("0.00"));
            AddMessage(curItem.Ticker + "Bid5" + "\t\t" + curItem.BidSize5.ToString("0") + '\t' + curItem.Bid5.ToString("0.00"));
            AddMessage("============");

            txtMessages.Text = strMessages;

        }

        private void PrintDepthAsk(int UntilPos)
        {
            strMessages = "";
            MarketDepthItem curItem = new MarketDepthItem();

            if (UntilPos > nextLine)
            {
                UntilPos = nextLine;
            }

            for (int i = 0; i <= UntilPos; i++)
            {
                if (LoadedLines[i, 0] == 0) curItem.Ask_New((int)LoadedLines[i, 1], LoadedLines[i, 2], (int)LoadedLines[i, 3]);
                if (LoadedLines[i, 0] == 1) curItem.Ask_Update((int)LoadedLines[i, 1], LoadedLines[i, 2], (int)LoadedLines[i, 3]);
                if (LoadedLines[i, 0] == 2) curItem.Ask_Delete((int)LoadedLines[i, 1]);
            }

            string tempLine = "";

            AddMessage(curItem.Ticker + "CurPos: " + "\t\t" + UntilPos.ToString() + "\r\n");
            if (LoadedLines[UntilPos, 0] != 2) tempLine = "\t" + LoadedLines[UntilPos, 2].ToString() + "\t" + LoadedLines[UntilPos, 3].ToString();
            AddMessage(curItem.Ticker + "Last Change: " + "\t" + LoadedLines[UntilPos, 0].ToString() + "\t" + LoadedLines[UntilPos, 1].ToString() + tempLine);
            AddMessage("\r\n");
            AddMessage(curItem.Ticker + "Ask1" + "\t\t" + curItem.AskSize1.ToString("0") + '\t' + curItem.Ask1.ToString("0.00"));
            AddMessage(curItem.Ticker + "Ask2" + "\t\t" + curItem.AskSize2.ToString("0") + '\t' + curItem.Ask2.ToString("0.00"));
            AddMessage(curItem.Ticker + "Ask3" + "\t\t" + curItem.AskSize3.ToString("0") + '\t' + curItem.Ask3.ToString("0.00"));
            AddMessage(curItem.Ticker + "Ask4" + "\t\t" + curItem.AskSize4.ToString("0") + '\t' + curItem.Ask4.ToString("0.00"));
            AddMessage(curItem.Ticker + "Ask5" + "\t\t" + curItem.AskSize5.ToString("0") + '\t' + curItem.Ask5.ToString("0.00"));
            AddMessage("============");

            txtMessages.Text = strMessages;

        }

        public void AddMessage(string curMessage)
        {
            strMessages = strMessages + curMessage + "\r\n";
        }

        private void txtEndLine_TextChanged(object sender, EventArgs e)
        {
            if (txtEndLine.Text != "")
            {
                if (PrintMode == "BID")
                {
                    PrintDepthBid(int.Parse(txtEndLine.Text));
                }
                if (PrintMode == "ASK")
                {
                    PrintDepthAsk(int.Parse(txtEndLine.Text));
                }
            }
        }

        private void cmdUpOne_Click(object sender, EventArgs e)
        {
            txtEndLine.Text = (int.Parse(txtEndLine.Text) + 1).ToString();
        }

        private void cmdDownOne_Click(object sender, EventArgs e)
        {
            txtEndLine.Text = (int.Parse(txtEndLine.Text) - 1).ToString();
        }



        private void cmdDownTen_Click(object sender, EventArgs e)
        {
            txtEndLine.Text = (int.Parse(txtEndLine.Text) - 10).ToString();
        }

        private void cmdUpTen_Click(object sender, EventArgs e)
        {
            txtEndLine.Text = (int.Parse(txtEndLine.Text) + 10).ToString();
        }
    }
}