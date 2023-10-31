using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NestDLL;

namespace CornDataCracker
{
    class ProcessFile
    {
        string FilePath;
        
        public ProcessFile(string pPath)
        {
            this.FilePath = pPath;
        }

        public bool mandaPraBase(DateTime sData, string symbol, double last, double sharesTraded)
        {
            //pega o IdTicker do database
            string IdSecQuery =        "SELECT IdSecurity " +
                                        "FROM NESTSRV06.NESTDB.dbo.Tb001_Securities " +
                                        "WHERE ReutersTicker = '" + symbol + "'";

            int IdSecurity = 0;

            using (newNestConn conn = new newNestConn(true))
            {
                IdSecurity = conn.Return_Int(IdSecQuery);
            }

            //verifico se o preço já existe.

            string auxQuery;
            /*
            auxQuery = "SELECT SRVALUE FROM [NESTDB].[dbo].[Tb059_Precos_Futuros]" +
                " WHERE IDSECURITY = " + IdSecurity + " AND SRTYPE = 1 AND SOURCE = 1 AND SRDATE = '" + sData.ToString("yyyyMMdd") + "'";

            double dblast = 0;

            using (newNestConn conn = new newNestConn(true))
            {
               Console.WriteLine(conn.Return_Double(auxQuery));
            }

            if (Double.IsNaN(dblast))
            {*/
                //insiro IDSECURITY VALUE DATE CURRENT DATE TIPO SOURCE AUTOMATED

                auxQuery = "EXEC NESTSRV06.NESTDB.dbo.Proc_Insert_Price " +
                                            IdSecurity + ", " + last.ToString().Replace(",", ".") + ",'" + sData.ToString("yyyyMMdd") + "',1,1,0 ";

               using (newNestConn conn = new newNestConn(true))
                {
                    conn.ExecuteNonQuery(auxQuery);
                }
            //}

                auxQuery = "EXEC NESTSRV06.NESTDB.dbo.Proc_Insert_Price " +
                                            IdSecurity + ", " + sharesTraded + ",'" + sData.ToString("yyyyMMdd") + "',11,1,0 ";
                using (newNestConn conn = new newNestConn(true))
                {
                    conn.ExecuteNonQuery(auxQuery);
                }

            return false;
        }



        public void LimpaArquivo()
        {
            System.IO.StreamReader fileToProcess = new System.IO.StreamReader(FilePath);
            System.IO.StreamWriter fileToWrite = new System.IO.StreamWriter(@"R:\RTICK\Future Files\CORN\Unprocessed\" + "arquivolimpo.csv");
            string curLine = "";
            DateTime curDate = new DateTime();

            int posTicker, posPrice, posDate, posTime, posTimeShift, posType, posOpen, posHigh, posLow, posLast, posVolume, posState, quoteBid, quoteAsk;

            posTicker = posPrice = posDate = posTime = posTimeShift = posType = posOpen = posHigh = posLow = posLast = posVolume = posState = quoteBid = quoteAsk = 0;

            char[] sep = { ',' };

            string tempLine = "";
            tempLine = fileToProcess.ReadLine();
            string[] headers = tempLine.Split(',');
            fileToWrite.WriteLine(tempLine);
            
            string[] splitLine;
            string curType, curTicker;
            DateTime dataTrade;
            //quais os dados necessários last e shares traded.
            double last = 0;
            double sharestraded = 0;
            double numNeg = 0;
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
                    splitLine = curLine.Split(',');
                    //Tipo de registro
                    curType = splitLine[4];

                    if (curType == "Trade" && double.Parse(splitLine[posLast].Replace('.', ',')) > 0)
                    {
                        //escreve só linhas válidas no arquivo.
                        fileToWrite.WriteLine(curLine);
                    }
                    
                }
            }
            catch (Exception e)
            { }

            fileToProcess.Close();
            fileToWrite.Close();
        }
        
        public void FileCracker()
        {
            System.IO.StreamReader fileToProcess = new System.IO.StreamReader(FilePath);
            System.IO.StreamWriter filetoWrite = new System.IO.StreamWriter(@"R:\RTICK\Future Files\CORN\Unprocessed\" + "pronto.csv");
            filetoWrite.WriteLine("Data,Ticker,Last,NumNeg,SharesTraded");
            string curLine = "";
            DateTime curDate = new DateTime();

            int posTicker, posPrice, posDate, posTime, posTimeShift, posType, posOpen, posHigh, posLow, posLast, posVolume, posState, quoteBid, quoteAsk;

            posTicker = posPrice = posDate = posTime = posTimeShift = posType = posOpen = posHigh = posLow = posLast = posVolume = posState = quoteBid = quoteAsk = 0;

            char[] sep = { ',' };

            string tempLine = "";

            string[] headers = fileToProcess.ReadLine().Split(',');

            string[] splitLine;
            string curType, curTicker, strData;
            DateTime dataTrade;
            //quais os dados necessários last e shares traded.
            double last = 0;
            double sharestraded = 0;
            double numNeg = 0;
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
                //tenho a linha atual e a última que gravei. se a data mudou, gravo os dados atuais, se nao, acumula
                //leio a primeira linha.
                curLine = fileToProcess.ReadLine();
                 DateTime DateReference = new DateTime(2012, 1, 1);
                splitLine = curLine.Split(',');
                dataTrade = DateTime.Parse(splitLine[posDate].Substring(0, 4) + "-" + splitLine[posDate].Substring(4, 2) + "-" + splitLine[posDate].Substring(6, 2));
                strData = splitLine[posDate];
                last = double.Parse(splitLine[posLast].Replace('.', ','));
                curTicker = splitLine[posTicker];
                if (splitLine[posVolume] == "0" || splitLine[posVolume] == "")
                {
                    sharestraded = sharestraded + 1;
                }
                else
                {
                    sharestraded = sharestraded+double.Parse(splitLine[posVolume]);
                }
                
                numNeg = numNeg + 1;
                //começo  a ler o arquivo
                while (!fileToProcess.EndOfStream)
                {
                    //leio a linha

                    curLine = fileToProcess.ReadLine();
                    splitLine = curLine.Split(',');
                    
                    //se a data mudou, eu escrevo, senao, acumulo.
                    if (splitLine[posDate] != strData)
                    {                        
                        //filetoWrite.WriteLine("Data,Ticker,Last,NumNeg,SharesTraded");
                        filetoWrite.WriteLine(DateTime.Parse(strData.Substring(0, 4) + "-" + strData.Substring(4, 2) + "-" + strData.Substring(6, 2)) + "," + curTicker + "," + last + "," + numNeg + "," + sharestraded);
                        if (DateTime.Parse(strData.Substring(0, 4) + "-" + strData.Substring(4, 2) + "-" + strData.Substring(6, 2)) < DateReference )
                        {
                            mandaPraBase(DateTime.Parse(strData.Substring(0, 4) + "-" + strData.Substring(4, 2) + "-" + strData.Substring(6, 2)), curTicker, last, sharestraded);
                            Console.WriteLine("Ticker: " + curTicker + " Data: " + strData);
                        }

                        Console.WriteLine("Ticker: " + splitLine[posTicker] + " Data: " + DateTime.Parse(strData.Substring(0, 4) + "-" + strData.Substring(4, 2) + "-" + strData.Substring(6, 2)) + " Last: " + last + " NumNeg: " + numNeg + " VolumeTraded: " + sharestraded);
                        last = sharestraded = numNeg = 0;
                        curTicker = splitLine[posTicker];
                        strData = splitLine[posDate];
                        dataTrade = DateTime.Parse(splitLine[posDate].Substring(0, 4) + "-" + splitLine[posDate].Substring(4, 2) + "-" + splitLine[posDate].Substring(6, 2));
                        last = double.Parse(splitLine[posLast].Replace('.', ','));
                        if (splitLine[posVolume] == "0" || splitLine[posVolume] == "")
                        {
                            sharestraded = sharestraded + 1;
                        }
                        else
                        {
                            sharestraded = sharestraded + double.Parse(splitLine[posVolume]);
                        }
                        numNeg = numNeg + 1;
                    } else {
                        //se não eu acumulo
                        dataTrade = DateTime.Parse(splitLine[posDate].Substring(0, 4) + "-" + splitLine[posDate].Substring(4, 2) + "-" + splitLine[posDate].Substring(6, 2));
                        strData = splitLine[posDate];
                        last = double.Parse(splitLine[posLast].Replace('.', ','));
                        curTicker = splitLine[posTicker];
                        if (splitLine[posVolume] == "0" || splitLine[posVolume] == "")
                        {
                            sharestraded = sharestraded + 1;
                        }
                        else
                        {
                            sharestraded = sharestraded + double.Parse(splitLine[posVolume]);
                        }
                        numNeg = numNeg + 1;
                    }
                  
                        
                        
                    
                }
            }
            catch (Exception e)
            { }

            fileToProcess.Close();
            filetoWrite.Close();
        }

    }
}
