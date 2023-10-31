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

        public RTICKFileCracker(string Folder,TimeSpan refTime)
        {
            //string Folder = @"R:\RTICK\DIFiles\Unprocessed";
                        
            string[] Files = Directory.GetFiles(Folder);
            foreach (string file in Files)
            {
                ProcessFile(file,refTime);
                InsertFilesInDataBase(Folder + @"\DataBaseFiles\");
                File.Move(file,file.Replace("Unprocessed","Processed"));
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

                    if (curLine.Contains("Trade"))
                    { }

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
                                auxDate = DateTime.Parse(splitLine[posDate].Substring(0,4) + "-" + splitLine[posDate].Substring(4,2) + "-" + splitLine[posDate].Substring(6,2) + " " + splitLine[posTime]);
                            }
                            auxDate = auxDate.AddHours(curOffset);

                            curDate = auxDate.Date;
                            TimeSpan curTime = auxDate.TimeOfDay;

                            if (curTime <= refTime)
                            {
                                string curTicker = splitLine[posTicker];

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

                            string InsertValues =
                                            "EXEC NESTSRV06.NESTDB.dbo.Proc_Insert_Price " +
                                            IdSecurity + ", " + values[2].Replace(",", ".") + ",'" + DateTime.Parse(values[1]).ToString("yyyyMMdd") + "',312,22,1 ";

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
