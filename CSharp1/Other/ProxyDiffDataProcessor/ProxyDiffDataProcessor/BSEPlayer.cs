using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using NestSymConn;
using NCommonTypes;

namespace ProxyDiffDataProcessor
{
    class BSEPlayer
    {
        public BSECracker curCracker = new BSECracker(true,false);
        private System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog();

        string curFileName="";
        public int MessagesRead = 0;

        private bool _StopPlaying = false;
        public bool PauseOnDate = true;

        public event EventHandler OnData;

        //Queue<string> FileLines = new Queue<string>();
        //bool bFileFinished = false;

        public BSEPlayer(string _FileName)
        {
            curFileName = _FileName;
            curCracker.OnNewData += new EventHandler(SendData);
        }

        private void SendData(object sender, EventArgs e)
        {
            MarketUpdateList curList = new MarketUpdateList();

            MarketUpdateItem DataArgs = (MarketUpdateItem)e;

            switch (DataArgs.FLID)
            {
                case NestFLIDS.AskBroker1:
                case NestFLIDS.AskBroker2:
                case NestFLIDS.AskBroker3:
                case NestFLIDS.AskBroker4:
                case NestFLIDS.AskBroker5:
                case NestFLIDS.AucCond:
                case NestFLIDS.BidBroker1:
                case NestFLIDS.BidBroker2:
                case NestFLIDS.BidBroker3:
                case NestFLIDS.BidBroker4:
                case NestFLIDS.BidBroker5:
                case NestFLIDS.Description:
                case NestFLIDS.Exchange:
                case NestFLIDS.None:
                    DataArgs.ValueString = DataArgs.ValueString; break;
                default:
                    DataArgs.ValueDouble = double.Parse(DataArgs.ValueString.Replace(".",",")); break;
            }

            curList.ItemsList.Add(DataArgs);

            if (OnData != null)
            {
                OnData(this, curList);
            }
        }

        public void Stop()
        {
            _StopPlaying = true;
        }

        public void Play()
        {
            if (curFileName == "")
            {
                openFileDialog1.InitialDirectory = @"C:\ProxyFiles\";
                openFileDialog1.FileName = "";

                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    curFileName = openFileDialog1.FileName; 
                }
            }

            if (curFileName != "")
            {

                Thread t5 = new Thread(new ThreadStart(ReadAndCrack));
                t5.Start();

                //Thread t1 = new Thread(new ThreadStart(ReadFile));
                //t1.Start();
                //Thread t2 = new Thread(new ThreadStart(CrackFileLines));
                //t2.Start();
            }
            
        }
        /*
        private void ReadFile()
        {
            FileStream fs = new FileStream(curFileName, FileMode.Open,FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            //MemoryStream mem = new MemoryStream((int)fs.Length);
            //fs.Read(mem.GetBuffer(), 0, (int)fs.Length);

            string tempLine = "";
            string crackLine = "";

            while ((tempLine = sr.ReadLine()) != null && !StopPlaying)
            {
                crackLine = crackLine + tempLine;
                if (crackLine.IndexOf((char)17) > 0)
                {
                    CountMessage++;

                    if (CountMessage > 30005)
                    {

                    }


                    while (FileLines.Count > 50000)
                    {
                        System.Threading.Thread.Sleep(200);
                    }
                    
                    lock (FileLines)
                    {
                        FileLines.Enqueue(crackLine);
                    }

                    crackLine = "";
                }
            }

            sr.Close();
            fs.Close();
            bFileFinished = true;
        }

        private void CrackFileLines()
        {
            while (!bFileFinished)
            {
                string tempLine = "";
                lock (FileLines)
                {
                    if (FileLines.Count > 0)
                    {
                        tempLine = FileLines.Dequeue();
                    }
                }
                if (tempLine != null)
                {
                    if (tempLine != "")
                    {
                        curCracker.Crack(tempLine);
                    }
                }
            }
        }
        */
        private void ReadAndCrack()
        {
            FileStream fs = new FileStream(curFileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            string tempLine = "";
            string crackLine = "";

            while ((tempLine = sr.ReadLine()) != null)
            {
                if (_StopPlaying) break;
                crackLine = crackLine + tempLine;
                if (crackLine.IndexOf((char)17) > 0)
                {
                    MessagesRead++;

                    curCracker.Crack(crackLine);

                    crackLine = "";

                    //Block after getting date
                    if (curCracker.LastMessageTime.Date > new DateTime(1900, 01, 10) && PauseOnDate)
                    {
                        while (PauseOnDate)
                        {
                            Thread.Sleep(100);
                        }
                    }
                }
            }            

            sr.Close();
            fs.Close();
        }

    }
}
