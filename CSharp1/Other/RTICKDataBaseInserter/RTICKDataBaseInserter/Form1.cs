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

namespace RTICKDataBaseInserter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void InsertFileInDatabase(string Folder)
        {
            string curFolder = Folder;

            string[] files = Directory.GetFiles(Folder);

            foreach (string curFile in files)
            {
                if (curFile.Contains("OpenAuction"))
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[0];

                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO RTICKDB.dbo.OpenAuction (IdTicker,Ticker,Date,EndTimeGMT,EndTimeLocal,Price,Volume,TotalTrades,InsertDate) VALUES "
                                            + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "','" + curValues[2] + "','" + curValues[3] + "'," + curValues[4] + "," + curValues[5] + "," + curValues[6] + ",'" + DateTime.Today.ToString("yyyyMMdd") + "')";
                                Result = curConn.ExecuteNonQuery(StringSQL);

                                if (Result != 1)
                                {
                                }
                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                }

                if (curFile.Contains("CloseAuction"))
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[0];

                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO RTICKDB.dbo.CloseAuction (IdTicker,Ticker,Date,StartTimeGMT,StartTimeLocal,EndTimeGMT,EndTimeLocal,PreAuction_Price,Price,Volume,TotalTrades,InsertDate) VALUES "
                                            + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "','" + curValues[2] + "','" + curValues[3] + "','" + curValues[4] + "','" + curValues[5] + "'," + curValues[6] + "," + curValues[7] + "," + curValues[8] + "," + curValues[9] + ",'" + DateTime.Today.ToString("yyyyMMdd")+"')";
                                Result = curConn.ExecuteNonQuery(StringSQL);

                                if (Result != 1)
                                { 
                                }

                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                }

                if (curFile.Contains("OneMinBar"))
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string tempLine = "";
                        FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                        StreamReader sr = new StreamReader(fs);

                        double IdSecurity = double.NaN;
                        string TempTicker = "";
                        string PrevTicker = "";
                        string StringSQL = "";
                        int Result = 0;

                        while ((tempLine = sr.ReadLine()) != null)
                        {
                            string[] curValues = tempLine.Split(';');
                            TempTicker = curValues[0];

                            if (TempTicker != PrevTicker)
                            {
                                IdSecurity = curConn.Return_Double("SELECT IdSecurity FROM NESTDB.dbo.Tb001_Securities_Fixed WHERE ReutersTicker='" + TempTicker + "'");
                            }

                            if (!double.IsNaN(IdSecurity))
                            {
                                StringSQL = "INSERT INTO RTICKDB.dbo.IntradayOneMinuteBars (IdTicker,Ticker,Date,Minute,StartTimeGMT,StartTimeLocal,EndTimeGMT,EndTimeLocal,VWAP,Volume,Low,High,[Open],[Close],AskTrades,BidTrades,TotalTrades,InsertDate) VALUES "
                                            + "(" + IdSecurity.ToString() + ",'" + TempTicker + "','" + curValues[1] + "'," + curValues[2] + ",'" + curValues[3] + "','" + curValues[4] + "','" + curValues[5] + "','" + curValues[6] + "'," + curValues[7] + "," + curValues[8] + "," + curValues[10]
                                            + "," + curValues[11] + "," + curValues[12] + "," + curValues[13] + "," + curValues[14] + "," + curValues[15] + "," + curValues[16] + ",'" + DateTime.Today.ToString("yyyyMMdd") +"')";
                                Result = curConn.ExecuteNonQuery(StringSQL);

                                if (Result != 1)
                                {
                                }
                            }
                            PrevTicker = TempTicker;
                        }
                        fs.Close();
                        sr.Close();
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InsertFileInDatabase(@"C:\temp\RTICK\DataBaseFiles");
        }
    }
}
