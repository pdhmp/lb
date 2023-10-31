using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.IO;
using NestDLL;

namespace RTICKReaderDI
{
    public class RTICKFileCracker
    {
        private StreamReader fileToProcess;
        RTICKFileCrack rtick = new RTICKFileCrack();
        private TimeSpan getTimeSpan(string srType)
        {
            string paramFile = @"N:\Quant\Gustav\temp\RTICKReader.txt";

            StreamReader srParam = new StreamReader(paramFile);

            string ln = "";
            TimeSpan t = new TimeSpan();
            while ((ln = srParam.ReadLine()) != null)
            {
                if (ln.Contains(srType))
                {
                    string[] vtParam = ln.Split(',');
                    t = TimeSpan.Parse(vtParam[1]);
                }
            }
            srParam.Close();
            
            return t;
        }

        public RTICKFileCracker(string Folder)
        {
            TimeSpan refTime = new TimeSpan();
            
            if (Folder.Contains("DI"))
            {                
                refTime = getTimeSpan("DI");
                rtick.setLbInstrument("DI");
                rtick.setLbStatus("Retrieving parameters");
                rtick.setProgBarValue(6);
                rtick.RefreshForm();
            }
            else if (Folder.Contains("ICF"))
            {
                refTime = getTimeSpan("ICF");
                rtick.setLbInstrument("ICF");
                rtick.setLbStatus("Retrieving parameters");
                rtick.setProgBarValue(6);
                rtick.RefreshForm();           
            }
            else if (Folder.Contains("CORN"))
            {
                refTime = getTimeSpan("CORN");
                rtick.setLbInstrument("CORN");
                rtick.setLbStatus("Retrieving parameters");
                rtick.setProgBarValue(6);
                rtick.RefreshForm();
            }
            else if (Folder.Contains("IBOV"))
            {
                refTime = getTimeSpan("PAIBOV");
                rtick.setLbInstrument("PAIBOV");
                rtick.setLbStatus("Retrieving parameters");
                rtick.setProgBarValue(6);
                rtick.RefreshForm();
            }
            else if (Folder.Contains("ETHANOL"))
            {
                refTime = getTimeSpan("ETHANOL");
                rtick.setLbInstrument("ETHANOL");
                rtick.setLbStatus("Retrieving parameters");
                rtick.setProgBarValue(6);
                rtick.RefreshForm();
            }
            else if (Folder.Contains("BOI"))
            {
                refTime = getTimeSpan("BOI");
                rtick.setLbInstrument("BOI");
                rtick.setLbStatus("Retrieving parameters");
                rtick.setProgBarValue(6);
                rtick.RefreshForm();
            }
            else if (Folder.Contains("SOJA"))
            {
                refTime = getTimeSpan("SOJA");
                rtick.setLbInstrument("SOJA");
                rtick.setLbStatus("Retrieving parameters");
                rtick.setProgBarValue(6);
                rtick.RefreshForm();
            }
            string[] Files = Directory.GetFiles(Folder);
            foreach (string file in Files)
            {
                ProcessFile(file,refTime);
                rtick.setLbStatus("Processing File");
                rtick.setProgBarValue(6);
                rtick.RefreshForm();
                InsertFilesInDataBase(Folder + @"\DataBaseFiles\");
                rtick.setLbStatus("Inserting data into Database");
                rtick.setProgBarValue(6);
                rtick.RefreshForm();
                File.Move(file,file.Replace("Unprocessed","Processed"));
                rtick.setLbStatus("Moving CSV File");
            }              
        }

        public void ReadNLines(int numberOfLines, string newFilePath)
        {
            string curLine = "";
            int count = 0;
            StreamWriter tempFile = new StreamWriter(newFilePath);

            while (((curLine = fileToProcess.ReadLine()) != null) && count < numberOfLines)
            {
                tempFile.WriteLine(curLine);
                count++;
            }

            tempFile.Close();
        }

        public void SeparateByTicker(string FilePath)
        {
            fileToProcess = new StreamReader(FilePath);

            string headerLine = fileToProcess.ReadLine();

            SortedDictionary<string, SortedDictionary<int, StreamWriter>> FileContainer = new SortedDictionary<string, SortedDictionary<int, StreamWriter>>();
            string curline = "";

            while ((curline = fileToProcess.ReadLine()) != null)
            {
                string[] splitline = curline.Split(',');

                string curPath = @"C:\temp\difiles\";

                string curTicker = splitline[0];
                DateTime curDate = DateTime.Parse(splitline[1]);
                int curYear = curDate.Year;

                StreamWriter curWriter;
                SortedDictionary<int, StreamWriter> curContainer;

                if (FileContainer.TryGetValue(curTicker, out curContainer))
                {
                    if (curContainer.TryGetValue(curYear, out curWriter))
                    {
                        curWriter.WriteLine(curline);
                    }
                    else
                    {
                        curWriter = new StreamWriter(curPath + curTicker + "\\" + curTicker + "_" + curYear + ".csv");

                        curWriter.WriteLine(headerLine);
                        curWriter.WriteLine(curline);

                        curContainer.Add(curYear, curWriter);
                    }
                }
                else
                {
                    Directory.CreateDirectory(curPath + curTicker);

                    curWriter = new StreamWriter(curPath + curTicker + "\\" + curTicker + "_" + curYear + ".csv");
                    curWriter.WriteLine(headerLine);
                    curWriter.WriteLine(curline);

                    curContainer = new SortedDictionary<int, StreamWriter>();
                    curContainer.Add(curYear, curWriter);
                    FileContainer.Add(curTicker, curContainer);
                }
            }

            foreach (SortedDictionary<int, StreamWriter> curContainer in FileContainer.Values)
            {
                foreach (StreamWriter curWriter in curContainer.Values)
                {
                    curWriter.Close();
                }
            }            
        }

        public void ProcessFile(string FilePath, TimeSpan refTime)
        {
            fileToProcess = new StreamReader(FilePath);

            SortedDictionary<string, SortedDictionary<DateTime, double>> PreAucPrice = new SortedDictionary<string, SortedDictionary<DateTime, double>>();

            string curLine = "";
            DateTime curDate = new DateTime();
            Boolean isBid=true;

            int posTicker, posPrice, posDate, posTime, posTimeShift, posType, posOpen, posHigh, posLow, posLast, posVolume, posState, quoteBid, quoteAsk;

            posTicker = posPrice = posDate = posTime = posTimeShift = posType = posOpen = posHigh = posLow = posLast = posVolume = posState = quoteBid = quoteAsk = 0;

            char[] sep = { ',' };

            string tempLine = "";

            string[] headers = fileToProcess.ReadLine().Split(',');

            for (int i = 0; i < headers.Length; i++)
            {
                switch (headers[i])
                {
                    case "#RIC": posTicker = i;
                        break;
                    case "Date[G]": posDate = i;
                        break;
                    case "Time[G]": posTime = i;
                        break;
                    case "GMT Offset": posTimeShift = i;
                        break;
                    case "Type": posType = i;
                        break;
                    case "Price": posLast = i;
                        break;
                    case "Volume": posVolume = i;
                        break;
                    case "Bid Price": quoteBid = i;
                        break;
                    case "Ask Price": quoteAsk = i;
                        break;
                    case "Qualifiers": posState = i;
                        break;
                }
            }

            try
            {
                while (!fileToProcess.EndOfStream)
                {
                    curLine = fileToProcess.ReadLine();
                    string[] splitLine = curLine.Split(',');

                    string curType = splitLine[4];
                    string curTicker = splitLine[posTicker];

                    if (curTicker.Contains("ETH"))
                    {

                        if (curType == "Quote")
                        {
                            int curOffset = int.Parse(splitLine[posTimeShift]);
                            DateTime auxDate;

                            if (!DateTime.TryParse(splitLine[posDate] + " " + splitLine[posTime], out auxDate))
                            {
                                auxDate = DateTime.Parse(splitLine[posDate].Substring(0, 4) + "-" + splitLine[posDate].Substring(4, 2) + "-" + splitLine[posDate].Substring(6, 2) + " " + splitLine[posTime]);
                            }
                            auxDate = auxDate.AddHours(curOffset);

                            curDate = auxDate.Date;
                            TimeSpan curTime = auxDate.TimeOfDay;

                            if (curTime <= refTime)
                            {
                                try
                                {
                                    SortedDictionary<DateTime, double> curTickerPrices;

                                    double curAsk=0;
                                    double curBid=0;

                                    if (splitLine[quoteAsk].Replace('.', ',') != "")
                                    {
                                        curAsk=double.Parse(splitLine[quoteAsk].Replace('.', ','));
                                    }
                                    else if (splitLine[quoteBid].Replace('.', ',') != "")
                                    {
                                        curBid = double.Parse(splitLine[quoteBid].Replace('.', ','));
                                    }


                                    if (PreAucPrice.TryGetValue(curTicker, out curTickerPrices))
                                    {

                                        if (curAsk>0)
                                        {

                                            if (!curTickerPrices.ContainsKey(curDate))
                                            {
                                                curTickerPrices.Add(curDate, curAsk);
                                            }
                                            else
                                            {
                                                if (isBid == false)
                                                {
                                                    curTickerPrices[curDate] = curAsk;
                                                }
                                                else
                                                {
                                                    double curPrice = (curAsk + curTickerPrices[curDate]) / 2;
                                                    curTickerPrices[curDate] = curPrice;
                                                }
                                            }

                                            isBid = false;
                                        }
                                        else if (curBid>0)
                                        {

                                            if (!curTickerPrices.ContainsKey(curDate))
                                            {
                                                curTickerPrices.Add(curDate, curBid);
                                            }
                                            else
                                            {
                                                if (isBid == true)
                                                {
                                                    curTickerPrices[curDate] = curBid;
                                                }
                                                else
                                                {
                                                    double curPrice = (curBid+ curTickerPrices[curDate]) / 2;
                                                    curTickerPrices[curDate] = curPrice;
                                                }
                                            }
                                            isBid = true;
                                        }
                                    }

                                    else
                                    {
                                        curTickerPrices = new SortedDictionary<DateTime, double>();

                                        if (curAsk>0)
                                        {
                                            curTickerPrices.Add(curDate, curAsk);
                                            PreAucPrice.Add(curTicker, curTickerPrices);
                                        }
                                        else if (curBid>0)
                                        {
                                            curTickerPrices.Add(curDate, curBid);
                                            PreAucPrice.Add(curTicker, curTickerPrices);
                                        }


                                    }

                                }
                                catch (Exception E)
                                { }
                            }
                            
                        }

                    }
                    else
                    {

                        if (curType == "Trade")
                        {
                            //string curQualifier = splitLine[14];
                            string curQualifier = splitLine[posState];

                            if (curQualifier.Contains("31[IRGCOND]"))
                            {
                                int curOffset = int.Parse(splitLine[posTimeShift]);
                                //DateTime auxDate = DateTime.Parse(splitLine[posDate] + " " + splitLine[posTime]);
                                DateTime auxDate;
                                if (!DateTime.TryParse(splitLine[posDate] + " " + splitLine[posTime], out auxDate))
                                {
                                    auxDate = DateTime.Parse(splitLine[posDate].Substring(0, 4) + "-" + splitLine[posDate].Substring(4, 2) + "-" + splitLine[posDate].Substring(6, 2) + " " + splitLine[posTime]);
                                }
                                auxDate = auxDate.AddHours(curOffset);

                                curDate = auxDate.Date;
                                TimeSpan curTime = auxDate.TimeOfDay;

                                if (curTime <= refTime)
                                {

                                    if ((curTicker.Contains("DIJV2")))
                                    { }

                                    try
                                    {
                                        double curPrice = double.Parse(splitLine[posLast].Replace('.', ','));
                                        //double curPrice = double.Parse(splitLine[6].Replace('.', ','));

                                        SortedDictionary<DateTime, double> curTickerPrices;

                                        if (PreAucPrice.TryGetValue(curTicker, out curTickerPrices))
                                        {
                                            if (!curTickerPrices.ContainsKey(curDate))
                                            {
                                                curTickerPrices.Add(curDate, curPrice);
                                            }
                                            else
                                            {
                                                curTickerPrices[curDate] = curPrice;
                                            }
                                        }
                                        else
                                        {
                                            curTickerPrices = new SortedDictionary<DateTime, double>();
                                            curTickerPrices.Add(curDate, curPrice);
                                            PreAucPrice.Add(curTicker, curTickerPrices);
                                        }
                                    }
                                    catch (Exception E)
                                    { }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            { }
            string output = FilePath.Insert(FilePath.IndexOf("Unprocessed") + 12, @"DataBaseFiles\").Replace(".csv", "_" + curDate.ToString("yyyyMMdd") + "_Pre_Auc_Price.txt");

            StreamWriter PAPFile = new StreamWriter(output);

            PAPFile.WriteLine("Ticker\tDate\tPreAucPrice");

            foreach (KeyValuePair<string, SortedDictionary<DateTime, double>> curTickerPrices in PreAucPrice)
            {
                foreach (KeyValuePair<DateTime, double> curDatePrice in curTickerPrices.Value)
                {
                    PAPFile.WriteLine(curTickerPrices.Key + "\t" + curDatePrice.Key.ToString("dd/MM/yyyy") + "\t" + curDatePrice.Value.ToString());
                }
            }

            PAPFile.Close();
            fileToProcess.Close();
        }             

        public void InsertFilesInDataBase(string Folder)
        {
            //string Folder = @"R:\RTICK\DIFiles\Unprocessed\DataBaseFiles\";
            
            string[] Files = Directory.GetFiles(Folder);
            foreach (string file in Files)
            {
                if (file.Contains(".txt"))
                {
                    StreamReader sR = new StreamReader(file);
                    sR.ReadLine();
                    while (!sR.EndOfStream)
                    {
                        string curLine = sR.ReadLine();
                        string[] sep = { "\t" };

                        string[] values = curLine.Split(sep,StringSplitOptions.RemoveEmptyEntries);

                        if (values[0].IndexOf("2") == 0)
                        {
                            values[0] = values[0].Remove(values[0].IndexOf("2"), 1);
                        }

                        string IdSecQuery =
                                        "SELECT IdSecurity " +
                                        "FROM NESTSRV06.NESTDB.dbo.Tb001_Securities " +
                                        "WHERE ReutersTicker = '" + values[0] + "'";
                            
                        int IdSecurity = 0;
                            
                        using(newNestConn conn = new newNestConn(true))
                        {
                            IdSecurity = conn.Return_Int(IdSecQuery);
                        }

                        if (!(IdSecurity > 0))
                        {
                            IdSecQuery =
                                        "SELECT IdSecurity " +
                                        "FROM NESTSRV06.NESTDB.dbo.Tb001_Securities " +
                                        "WHERE ReutersTicker = '" + values[0].Insert(0,"2") + "'";

                            using (newNestConn conn = new newNestConn(true))
                            {
                                IdSecurity = conn.Return_Int(IdSecQuery);
                            }
                        }

                        if (IdSecurity > 0)
                        {
                            if(values[0].Contains("DI"))
                            {
                                values[2] = (double.Parse(values[2]) / 100).ToString().Replace(",", ".");
                            }

                            string InsertValues;

                            if (values[0].Contains("IND"))
                            {
                                InsertValues = "EXEC NESTSRV06.NESTDB.dbo.Proc_Insert_Price " +
                                            IdSecurity + ", " + values[2].Replace(",", ".") + ",'" + DateTime.Parse(values[1]).ToString("yyyyMMdd") + "',1200,22,1 ";
                            }
                            else
                            {
                                InsertValues = "EXEC NESTSRV06.NESTDB.dbo.Proc_Insert_Price " +
                                            IdSecurity + ", " + values[2].Replace(",", ".") + ",'" + DateTime.Parse(values[1]).ToString("yyyyMMdd") + "',312,22,1 ";
                            }

                            using (newNestConn conn = new newNestConn(true))
                            {
                                conn.ExecuteNonQuery(InsertValues);
                            }
                        }
                    }
                    sR.Close();
                    if (File.Exists(file.Replace("Unprocessed", "Processed")))
                    {
                        File.Delete(file.Replace("Unprocessed", "Processed"));
                    }
                    File.Move(file,file.Replace("Unprocessed", "Processed"));                    
                }
            }
        }
    }
}
