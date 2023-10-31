using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

using NestSymConn;
using NCommonTypes;


namespace NewStrongOpen
{
    /// <summary>
    /// Manage the processing of a ProxyDiff file, providing data to the caller in a manner similar to SYM
    /// </summary>
    class BSEPlayer
    {
        #region Attributes Region

        private BSECracker curCracker = new BSECracker(true, false);
        private string curFileName;
        private bool isRunning = false;
        private bool StopPlaying = false;
        
        #endregion

        #region Events Region

        public event EventHandler OnData;

        #endregion

        #region Initializing Methods Region

        /// <summary>
        /// BSEPlayer constructor
        /// </summary>
        /// <param name="_FileName">Full path for the ProxyDiff file</param>
        public BSEPlayer(string _FileName)
        {
            curFileName = _FileName;
            curCracker.OnNewData += new EventHandler(SendData);
        }        

        #endregion

        #region Data Processing Region

        /// <summary>
        /// Start the processing of ProxyDiff file
        /// </summary>
        public void Play()
        {
            if (curFileName != "")
            {
                Thread readThread = new Thread(new ThreadStart(ReadAndCrack));
                readThread.Start();
            }        
        }

        /// <summary>
        /// Stop the processing of ProxyDiff file
        /// </summary>
        public void Stop()
        {
            StopPlaying = true;
        }

        /// <summary>
        /// Read and process the ProxyDiff file
        /// </summary>
        public void ReadAndCrack()
        {
            FileStream fs = new FileStream(curFileName, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            isRunning = true;

            while (!StopPlaying && !sr.EndOfStream)
            {
                string crackLine = ReadLine(sr);
                curCracker.Crack(crackLine);                
            }

            isRunning = false;
        }
        
        /// <summary>
        /// Gets the market date of the file being processed
        /// </summary>
        /// <returns></returns>
        public DateTime GetMarketDate()
        {
            BSECracker dateCracker = new BSECracker(true,false);
            FileStream fs = new FileStream(curFileName,FileMode.Open,FileAccess.Read);
            StreamReader sr = new StreamReader(fs);

            bool dateFound = false;
            DateTime marketDate = DateTime.MinValue;

            //Run through the ProxyDiff messages to find market date
            while (!dateFound && !sr.EndOfStream)
            {
                string crackLine = ReadLine(sr);
                dateCracker.Crack(crackLine);

                if (dateCracker.LastMessageTime.Date > new DateTime(1900, 01, 10))
                {
                    marketDate = dateCracker.LastMessageTime.Date;
                    dateFound = true;
                }
            }

            if (!dateFound)
            {
                throw new Exception("Unable to get market date for file" + curFileName);
            }

            return marketDate;

        }

        /// <summary>
        /// Reads a message from the ProxyDiff file, handling multi-line messages
        /// </summary>
        /// <param name="sr">StreamReader from which the line will be read</param>
        /// <returns></returns>
        private string ReadLine(StreamReader sr)
        {
            string tempLine = "";
            string crackLine = "";
            bool crackLineFound = false;
            
            //Run through the file until a full message is read
            while (!sr.EndOfStream && !crackLineFound)
            {
                tempLine = sr.ReadLine();
                crackLine = crackLine + tempLine;

                if (crackLine.IndexOf((char)17) > 0)
                {
                    crackLineFound = true;
                }
            }

            if (!crackLineFound)
            {
                throw new EndOfStreamException("ProxyDiff file is corrupted. Last message is incomplete");
            }

            return crackLine;
        }
        
        /// <summary>
        /// Subscribe ticker for market data
        /// </summary>
        /// <param name="Ticker">Ticker to receive market data</param>
        public void Subscribe(string Ticker)
        {
            try
            {
                curCracker.SubscribedTickers.Add(Ticker, Ticker);
            }
            catch
            { }
        }

        /// <summary>
        /// Send the market data to the event subscriber
        /// </summary>
        /// <param name="sender">Bse Player</param>
        /// <param name="e">Market data update args</param>
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
                    DataArgs.ValueDouble = double.Parse(DataArgs.ValueString.Replace(".", ",")); break;
            }

            curList.ItemsList.Add(DataArgs);

            if (OnData != null)
            {
                OnData(this, curList);
            }
        }

        #endregion
    }
}
