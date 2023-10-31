using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace ImagineSync
{
    public partial class frmImagineSync : Form
    {
        newNestConn curConn = new newNestConn();
        string[] UploadTicker = new string[10000];
        int[] UploadTickerID = new int[10000];
        int ProgramID = 20;
        bool bolIsRunning = false;
        string strRunTime = "";
        string strCurProcess = "";

        // ====================================  FORM ROUTINES  ===========================================

        public frmImagineSync()
        {
            InitializeComponent();
        }

        private void frmImagineSync_Load(object sender, EventArgs e)
        {
            tmrCheck.Start();
        }

        private void tmrCheck_Tick(object sender, EventArgs e)
        {
            double TriggerDate;
            double LastRunDate;

            Log.Log_CheckIn(ProgramID);

            if (!bolIsRunning)
            {
                double tempHour = (Convert.ToDouble(DateTime.Now.Hour) / 24);
                TriggerDate = DateTime.Now.Date.ToOADate() + tempHour;

                if (DateTime.Now.Minute > 30) TriggerDate = TriggerDate + (1F / 24 / 2);

                LastRunDate = Log.GetLastRunDate(120).ToOADate();

                if (DateTime.Now.Hour == 16 && DateTime.Now.Minute > 45)
                {
                    TriggerDate = Convert.ToDateTime(DateTime.Now.Date.Add(new TimeSpan(16, 30, 00))).ToOADate();
                }

                if (LastRunDate < TriggerDate && DateTime.Now.Hour < 22 && DateTime.Now.Hour > 7)
                {
                    StartFullSync();
                }
            }
            if (labRunTime.Text != strRunTime) labRunTime.Text = strRunTime;
            if (labCurrent.Text != strCurProcess) labCurrent.Text = strCurProcess;
            this.Refresh();
        }

        private void cmdAll_Click(object sender, EventArgs e)
        {
            StartFullSync();
        }

        private void StartFullSync()
        {
            System.Threading.Thread tFullSync;
            tFullSync = new System.Threading.Thread(new ThreadStart(FullSync));
            tFullSync.Start();
        }

        private void FullSync()
        {
            try
            {

            bolIsRunning = true;

            DateTime iniTime = DateTime.Now;

            Log.Log_Event(ProgramID, 120, 1, "Starting Imagine Full Sync");

            //cmdAll.Enabled = false;

            if (SyncQuantities(true))
            {
                DownloadPortfolio();
            }

            if (DateTime.Now.Minute > 25) RunMonteCarloSym();

            Log.Log_Event(ProgramID, 120, 2, "Finished Imagine Full Sync");

            TimeSpan elapsed = DateTime.Now - iniTime;

            string nicePrettyString = elapsed.ToString(); // ##:##:##

            DateTime d = new DateTime(elapsed.Ticks);

            strRunTime = d.ToString("HH:mm:ss");

            bolIsRunning = false;
            //cmdAll.Enabled = true;
        }
        catch (Exception)
        {

            throw;
        }

        }

        private void cmdUploadALL_Click(object sender, EventArgs e)
        {
            SyncQuantities(true);
        }

        private void cmdGetPort_Click(object sender, EventArgs e)
        {
            Log.Log_Event(ProgramID, 120, 1, "Starting Imagine Potfolio Download Only");
            DownloadPortfolio();
            Log.Log_Event(ProgramID, 120, 2, "Finished Imagine Potfolio Download Only");
        }

        private void RunMonteCarloSym()
        {
            strCurProcess = "Running Monte Carlo Sym";

            CreateImportFileMCSym();

            string tempFileName = @"T:\Import\Imagine\OutFiles\monteCarlo_out.csv";
            DateTime tempTime = DateTime.Now;

            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }

            Process p = new Process();
            p.StartInfo.FileName = @"T:\Import\Imagine\monteCarlo_run.bat";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            string curMode = "";
            string ImagineTicker = "";
            int ImagineQuant = 0;
            int Id_Portfolio = 0;

            using (TextReader tr = new StreamReader(tempFileName))
            {
                string tempString = "";

                while ((tempString = tr.ReadLine()) != null)
                {
                    if (tempString.Length > 2)
                    {
                        if (tempString.Contains("Portfolio:"))
                        {
                            if (tempString.Contains("NestFund")) Id_Portfolio = 4;
                            if (tempString.Contains("Bravo")) Id_Portfolio = 10;
                            if (tempString.Contains("Orion")) Id_Portfolio = 45;
                            if (tempString.Contains("Mile")) Id_Portfolio = 43;
                            curMode = "PORT";
                        }

                        if (tempString.Contains("Holding:"))
                        {
                            curMode = "HOLD";
                            string[] tempArray = tempString.Split(' ');
                            ImagineTicker = tempArray[2];
                            ImagineQuant = (int)Double.Parse(tempArray[1].Replace('.', ','));
                        }

                        if (Id_Portfolio != 0 && tempString.Substring(0, 3) == "\"20")
                        {
                            string finalString = tempString.Replace("\"", "");
                            string[] tempArray = finalString.Split(',');

                            if (curMode == "PORT" && tempArray[1].Trim() != "0" && tempArray[2].Trim() != "0")
                            {
                                string SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb813_Imagine_MCSym]([run_DateTime],[Id_Portfolio],[Imagine_Name],[Min_Value],[Max_Value],[Avg_Value],[Dev_Value],[VAR95_Lo_Value],[VAR95_Lo_EQT],[VAR95_Lo_FX],[VAR95_Lo_IR],[VAR95_Hi_Value],[VAR95_Hi_EQT],[VAR95_Hi_FX],[VAR95_Hi_IR])" +
                                                                            "VALUES('" + tempTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Id_Portfolio.ToString() + ", '" + tempArray[1] + "', " + tempArray[1] + ", " + tempArray[2] + ", " + tempArray[3] + ", " + tempArray[4] + ", " + tempArray[5] + ", " + tempArray[6] + ", " + tempArray[7] + ", " + tempArray[8] + ", " + tempArray[9] + ", " + tempArray[10] + ", " + tempArray[11] + ", " + tempArray[12] + ")";
                                curConn.ExecuteNonQuery(SQLExpression);
                            }

                            if (curMode == "HOLD" && tempArray[1].Trim() != "0" && tempArray[2].Trim() != "0")
                            {
                                string SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb814_Imagine_MCSym_Position]([run_DateTime],[Id_Portfolio],[Imagine_Quantity],[Imagine_Ticker],[Min_Value],[Max_Value],[Avg_Value],[Dev_Value],[VAR95_Lo_Value],[VAR95_Lo_EQT],[VAR95_Lo_FX],[VAR95_Lo_IR],[VAR95_Hi_Value],[VAR95_Hi_EQT],[VAR95_Hi_FX],[VAR95_Hi_IR])" +
                                                                            "VALUES('" + tempTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Id_Portfolio.ToString() + ", " + ImagineQuant + ", '" + ImagineTicker + "', " + tempArray[1] + ", " + tempArray[2] + ", " + tempArray[3] + ", " + tempArray[4] + ", " + tempArray[5] + ", " + tempArray[6] + ", " + tempArray[7] + ", " + tempArray[8] + ", " + tempArray[9] + ", " + tempArray[10] + ", " + tempArray[11] + ", " + tempArray[12] + ")";
                                curConn.ExecuteNonQuery(SQLExpression);
                                ImagineTicker = "";
                                ImagineQuant = 0;
                            }
                        }
                    }
                }
            }
            strCurProcess = "Finished Monte Carlo Sym";
        }

        private void DownloadPortfolio()
        {
            strCurProcess = "Downloading Portfolio";

            CreateImportFile(true);
            string tempFileName = @"T:\Import\Imagine\OutFiles\Print_Portfolio_Output.csv";
            DateTime tempTime = DateTime.Now;

            if (File.Exists(tempFileName))
            {
                File.Delete(tempFileName);
            }

            Process p = new Process();
            p.StartInfo.FileName = @"T:\Import\Imagine\Print_Portfolio.bat";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();

            string curMode = "";

            int Id_Portfolio = 0;
            try
            {
            using (TextReader tr = new StreamReader(tempFileName))
            {
                string tempString = "";
                while ((tempString = tr.ReadLine()) != null)
                {
                    if (tempString.Length > 2)
                    {
                        if (tempString.Substring(0, 3) == "\"\"\"")
                        {
                            if (tempString.Contains("VAR") && tempString.Contains("Gamma") && !tempString.Contains("Security"))
                            {
                                curMode = "PT_VAR";
                            }
                            else if (tempString.Contains("VAR") && !tempString.Contains("Gamma") && !tempString.Contains("Security"))
                            {
                                curMode = "BK_VAR";
                            }
                            else if (tempString.Contains("VAR") && tempString.Contains("Security"))
                            {
                                curMode = "PO_VAR";
                            }
                            else if (tempString.Contains("Cenario") && tempString.Contains("Security"))
                            {
                                curMode = "PO_CEN";
                            }
                            else if (tempString.Contains("Cenario") && !tempString.Contains("Security"))
                            {
                                curMode = "PT_CEN";
                            }
                            else
                            {
                                curMode = "";
                            }
                        }

                        string finalString = tempString.Replace("\"", "");


                        if (finalString.Contains("NestFund")) Id_Portfolio = 4;
                        if (finalString.Contains("Bravo")) Id_Portfolio = 10;
                        if (finalString.Contains("Orion")) Id_Portfolio = 45;
                        if (finalString.Contains("Mile")) Id_Portfolio = 43;

                        if (curMode != "" && tempString.Substring(0, 3) != "\"\"\"")
                        {
                            string[] tempArray = finalString.Split(',');
                            if (curMode == "PT_VAR" || curMode == "BK_VAR")
                            {
                                string SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb811_Imagine_Per_Book]([VAR_DateTime],[Id_Portfolio],[Id_Strategy],[Imagine_Book_Name],[Holdings],[VAR_Total],[VAR_Marginal],[VAR_Credit],[VAR_Equity],[VAR_FX],[VAR_IR],[VAR_VOL])" +
                                                                            "VALUES('" + tempTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Id_Portfolio.ToString() + "," + GetStratID(tempArray[1].ToString()) + ", '" + tempArray[1] + "', " + tempArray[0] + ", " + tempArray[2] + ", " + tempArray[3] + ", " + tempArray[4] + ", " + tempArray[5] + ", " + tempArray[6] + ", " + tempArray[7] + ", " + tempArray[8] + ")";
                                curConn.ExecuteNonQuery(SQLExpression);
                            }


                            if (curMode == "PO_VAR")
                            {
                                string SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb810_Imagine_Per_Position]([VAR_DateTime],[Id_Portfolio],[Imagine_Ticker],[Quantity],[VAR_Total],[VAR_Marginal],[VAR_Credit],[VAR_Equity],[VAR_FX],[VAR_IR],[VAR_VOL])" +
                                                                            "VALUES('" + tempTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Id_Portfolio.ToString() + ", '" + tempArray[2].ToString() + "', " + tempArray[0].ToString() + ", " + tempArray[6].ToString() + ", " + tempArray[7].ToString() + ", " + tempArray[8].ToString() + ", " + tempArray[9].ToString() + ", " + tempArray[10].ToString() + ", " + tempArray[11].ToString() + ", " + tempArray[12].ToString() + ")";
                                curConn.ExecuteNonQuery(SQLExpression);
                            }

                            if (curMode == "PT_CEN")
                            {
                                string SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb812_Imagine_Scenarios]([run_DateTime],[Id_Portfolio],[Imagine_Name],[Holdings],[Id_Scenario],[Loss_Value], Loss_Equities_BRL, Loss_Equities_nonBR, Loss_Currency, Loss_FI_BRL, Loss_FI_nonBRL)" +
                                                                            "VALUES('" + tempTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Id_Portfolio.ToString() + ", '" + tempArray[1] + "', " + tempArray[0] + ", 1, " + tempArray[2] + ", " + tempArray[3] + ", " + tempArray[4] + ", " + tempArray[5] + ", " + tempArray[6] + ", " + tempArray[7] + ")";
                                curConn.ExecuteNonQuery(SQLExpression);
                                SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb812_Imagine_Scenarios]([run_DateTime],[Id_Portfolio],[Imagine_Name],[Holdings],[Id_Scenario],[Loss_Value], Loss_Equities_BRL, Loss_Equities_nonBR, Loss_Currency, Loss_FI_BRL, Loss_FI_nonBRL)" +
                                                                            "VALUES('" + tempTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Id_Portfolio.ToString() + ", '" + tempArray[1] + "', " + tempArray[0] + ", 2, " + tempArray[8] + ", " + tempArray[9] + ", " + tempArray[10] + ", " + tempArray[11] + ", " + tempArray[12] + ", " + tempArray[13] + ")";
                                curConn.ExecuteNonQuery(SQLExpression);
                                SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb812_Imagine_Scenarios]([run_DateTime],[Id_Portfolio],[Imagine_Name],[Holdings],[Id_Scenario],[Loss_Value], Loss_Equities_BRL, Loss_Equities_nonBR, Loss_Currency, Loss_FI_BRL, Loss_FI_nonBRL)" +
                                                                            "VALUES('" + tempTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Id_Portfolio.ToString() + ", '" + tempArray[1] + "', " + tempArray[0] + ", 3, " + tempArray[14] + ", " + tempArray[15] + ", " + tempArray[16] + ", " + tempArray[17] + ", " + tempArray[18] + ", " + tempArray[19] + ")";
                                curConn.ExecuteNonQuery(SQLExpression);
                            }

                            if (curMode == "PO_CEN")
                            {
                                string SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb812_Imagine_Scenarios_Position]([run_DateTime],[Id_Portfolio],[Imagine_Ticker],[Id_Scenario],[Loss_Value], Loss_Equities_BRL, Loss_Equities_nonBR, Loss_Currency, Loss_FI_BRL, Loss_FI_nonBRL)" +
                                                                            "VALUES('" + tempTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Id_Portfolio.ToString() + ", '" + tempArray[1] + "', 1, " + tempArray[2] + ", " + tempArray[3] + ", " + tempArray[4] + ", " + tempArray[5] + ", " + tempArray[6] + ", " + tempArray[7] + ")";
                                curConn.ExecuteNonQuery(SQLExpression);
                                SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb812_Imagine_Scenarios_Position]([run_DateTime],[Id_Portfolio],[Imagine_Ticker],[Id_Scenario],[Loss_Value], Loss_Equities_BRL, Loss_Equities_nonBR, Loss_Currency, Loss_FI_BRL, Loss_FI_nonBRL)" +
                                                                            "VALUES('" + tempTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Id_Portfolio.ToString() + ", '" + tempArray[1] + "', 2, " + tempArray[8] + ", " + tempArray[9] + ", " + tempArray[10] + ", " + tempArray[11] + ", " + tempArray[12] + ", " + tempArray[13] + ")";
                                curConn.ExecuteNonQuery(SQLExpression);
                                SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb812_Imagine_Scenarios_Position]([run_DateTime],[Id_Portfolio],[Imagine_Ticker],[Id_Scenario],[Loss_Value], Loss_Equities_BRL, Loss_Equities_nonBR, Loss_Currency, Loss_FI_BRL, Loss_FI_nonBRL)" +
                                                                            "VALUES('" + tempTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + Id_Portfolio.ToString() + ", '" + tempArray[1] + "', 3, " + tempArray[14] + ", " + tempArray[15] + ", " + tempArray[16] + ", " + tempArray[17] + ", " + tempArray[18] + ", " + tempArray[19] + ")";
                                curConn.ExecuteNonQuery(SQLExpression);
                            }


                        }
                    }
                }
            }
            strCurProcess = "Finished Downloading Portfolio";
        }
        catch (Exception)
        {
            throw;
        }
    }

        private int GetStratID(string StratName)
        {
            if (StratName.Contains("ALL") || StratName.Contains("Books") || StratName.Contains("Equities BR") || StratName.Contains("Equities nBR") || StratName.Contains("ByCountry"))
            {
                return 0;
            }

            return Convert.ToInt32(curConn.Execute_Query_String("SELECT Id_Sub_Portfolio FROM dbo.Tb401_Sub_Portfolios WHERE Sub_Portfolio LIKE '%" + StratName.Split(' ')[1] + "%'"));

        }

        private void CreateImportFile(bool chkWithMMarginal)
        {
            string tempFileName = @"T:\Import\Imagine\ImpFiles\Print_Portfolio_Input.csv";

            if (File.Exists(tempFileName)) { File.Delete(tempFileName); }

            int noDays = GetNoDays();

            TextWriter tw = new StreamWriter(tempFileName);

            tw.WriteLine("portName,portType,calcVar,varConfLevel,varTimeHorizon,volTerm,volScale,corrScale,varSubtractMean,varMRatio,varIncIntRate,varIncVega,varIncRlzd,varTvolCol,expandCompositeHoldings,format,viewBy,totalOnly");
            tw.WriteLine("bkBravo Books,book,1,95," + noDays + ",60,1,1,0,1,1,1,0,2,0,titledCsv,VAR_and_Options,1");
            tw.WriteLine("bkMile Books,book,1,95," + noDays + ",60,1,1,0,1,1,1,0,2,0,titledCsv,VAR_and_Options,1");
            tw.WriteLine("bkNestFund Books,book,1,95," + noDays + ",60,1,1,0,1,1,1,0,2,0,titledCsv,VAR_and_Options,1");
            tw.WriteLine("bkNestFund ByCountry,book,1,95," + noDays + ",60,1,1,0,1,1,1,0,2,0,titledCsv,VAR_and_Options,1");

            if (chkWithMMarginal == true)
            {
                tw.WriteLine("bkMile ALL,book,1,95," + noDays + ",60,1,1,0,1,1,1,0,2,0,titledCsv,VAR_and_Options,0");
                tw.WriteLine("bkBravo ALL,book,1,95," + noDays + ",60,1,1,0,1,1,1,0,2,0,titledCsv,VAR_and_Options,0");
                tw.WriteLine("bkNestFund ALL,book,1,95," + noDays + ",60,1,1,0,1,1,1,0,2,0,titledCsv,VAR_and_Options,0");
            }

            tw.WriteLine("ptNestFund ALL Cenarios,portfolio,,,,,,,,,,,,,,titledCsv,,0");
            tw.WriteLine("ptMile ALL Cenarios,portfolio,,,,,,,,,,,,,,titledCsv,,0");
            tw.WriteLine("ptBravo ALL Cenarios,portfolio,,,,,,,,,,,,,,titledCsv,,0");

            tw.Close();
        }

        private void CreateImportFileMCSym()
        {
            string tempFileName = @"T:\Import\Imagine\ImpFiles\montecarlo.txt";

            if (File.Exists(tempFileName)) { File.Delete(tempFileName); }

            int noDays = GetNoDays();

            TextWriter tw = new StreamWriter(tempFileName);

            tw.WriteLine("batchCommand|args");
            tw.WriteLine("monteCarlo|\"-portName ptMileALL  -portfolio -loadShocks all -update -confLevels 95 -nrDays " + noDays + " -random -setupName Mellon -csvFormat\"");
            tw.WriteLine("monteCarlo|\"-portName ptNestFundALL  -portfolio -loadShocks all -update -confLevels 95 -nrDays " + noDays + " -random -setupName Mellon -csvFormat\"");
            tw.WriteLine("monteCarlo|\"-portName ptBravoALL  -portfolio -loadShocks all -update -confLevels 95 -nrDays " + noDays + " -random -setupName Mellon -csvFormat\"");

            tw.Close();
        }

        private int GetNoDays()
        {
            string tempDays = curConn.Execute_Query_String("SELECT CONVERT(varchar, dbo.FCN_NDATEADD('du', 1, getdate(), 2, 900), 23)");

            int GetNoDays = Convert.ToInt32(Convert.ToDateTime(tempDays).ToOADate() - DateTime.Now.Date.ToOADate());

            return GetNoDays;

        }

        private bool SyncQuantities(bool ReloadAll)
        {
            DataTable dt = new DataTable();

            if (ReloadAll)
            {
                dt = curConn.Return_DataTable("SELECT * FROM vwImagine_Upload ORDER BY [Id Portfolio], [Strategy]");
            }
            else
            {
                dt = curConn.Return_DataTable("SELECT * FROM vwImagineData ORDER BY [ImagineTicker]");
            }

            int UploadCounter = 1;
            string PosUpload = "";
            //string PosDelete = "";

            foreach (DataRow curRow in dt.Rows)
            {
                if (curRow["PositionLB"] != curRow["PositionImag"] || DBNull.Value.Equals(curRow["PositionImag"]))
                {
                    if (curRow["PositionLB"].ToString() != "0")
                    {
                        string accountName = "";
                        string groupPrefix = "";

                        if (curRow["Id Portfolio"].ToString() == "4") { accountName = "Nest Holding"; groupPrefix = "grpNestFund"; };
                        if (curRow["Id Portfolio"].ToString() == "10") { accountName = "Nest Holding"; groupPrefix = "grpBravo"; };
                        if (curRow["Id Portfolio"].ToString() == "43") { accountName = "Nest Holding"; groupPrefix = "grpMile"; };
                        if (curRow["Id Portfolio"].ToString() == "45") { accountName = "Nest Holding"; groupPrefix = "grpOrion"; };

                        double curPosition = Convert.ToDouble(curRow["PositionLB"]);
                        string curTicker = (curRow["ImagineTicker"].ToString()).Replace(",", ".");
                        PosUpload = PosUpload + "" + curTicker + "," + accountName + "," + groupPrefix + " " + curRow["Strategy"] + "," + curRow["AvgCostClose"].ToString().Replace(",", ".") + "," + curPosition.ToString().Replace(",", ".") + "\r\n";
                        UploadTicker[UploadCounter] = curTicker;
                        //UploadTickerID[UploadCounter] = curTicker;
                        UploadCounter++;
                    }
                }
            }

            string tempFileName = @"T:\Import\Imagine\ImpFiles\Delete_Holdings_Input.csv";
            if (File.Exists(tempFileName)) { File.Delete(tempFileName); }

            TextWriter tw = new StreamWriter(tempFileName);
            tw.WriteLine("holdAccount,deleteHoldings,noAuditing");
            tw.WriteLine("Nest Holding,1,1");
            //tw.WriteLine(PosDelete);
            //tw.WriteLine("Nest Orion FIM,1,1");
            //tw.WriteLine("Nest Mile,1,1");
            //tw.WriteLine("Nest Fund,1,1");
            tw.Close();

            tempFileName = @"T:\Import\Imagine\ImpFiles\Upload_Trades_Input.csv";
            if (File.Exists(tempFileName)) { File.Delete(tempFileName); }

            tw = new StreamWriter(tempFileName);
            tw.WriteLine("hldSecName,hldAcct,hldGroup, execPrice, execQty");
            tw.Write(PosUpload);
            tw.Close();

            tempFileName = @"T:\Import\Imagine\Log\Upload_Trades_log.txt";
            if (File.Exists(tempFileName)) { File.Delete(tempFileName); }

            strCurProcess = "Deleting Holdings";
            Process p = new Process();
            p.StartInfo.FileName = @"T:\Import\Imagine\Delete_Holdings.bat";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            strCurProcess = "Finished Deleting Holdings";

            strCurProcess = "Uploading Trades";
            p = new Process();
            p.StartInfo.FileName = @"T:\Import\Imagine\Upload_Trades.bat";
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            strCurProcess = "Finished Uploading Trades";

            ReadUploadLog();

            return true;

        }

        private void ReadUploadLog()
        {
            int pos = 1;
            int pos2 = 0;
            int pos3 = 0;
            int initialPos = 0;
            string tempFileName = @"T:\Import\Imagine\Log\Upload_Trades_log.txt";
            string finalString = "";
            string tempString = "";

            using (TextReader tr = new StreamReader(tempFileName))
            {
                while ((tempString = tr.ReadLine()) != null)
                {
                    finalString = finalString + tempString + "\r\n";
                }
            }

            while (pos != -1)
            {
                pos = finalString.IndexOf("requesting Data service", pos + 1);
                if (pos != -1) initialPos = pos;
            }
            pos = initialPos;

            while (pos != -1)
            {
                pos = finalString.IndexOf("-E- Error", pos + 1);
                if (pos != -1)
                {
                    pos2 = finalString.IndexOf("\r\n", pos + 1);
                    string tempMessage = finalString.Substring(pos, pos2 - pos);
                    pos3 = tempMessage.IndexOf(")", 1);
                    int tempLineNumber = Convert.ToInt32(tempMessage.Substring(15, pos3 - 15));
                    //MessageBox.Show("Ticker not uploaded:" + UploadTicker[tempLineNumber - 1]);
                    Log.Log_Event(ProgramID, 120, 9, "Ticker not uploaded:" + UploadTicker[tempLineNumber - 1], "'" + UploadTicker[tempLineNumber - 1] + "'");
                    //Log_Event 120, 9, "Ticker not uploaded:" & UploadTicker(tempLineNumber - 1), UploadTicker(tempLineNumber - 1)
                }
            }
        }

     }
}