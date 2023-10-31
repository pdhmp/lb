using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Threading;

namespace Get_File_FTP_Reuters
{
    public partial class frmMain : Form
    {
        private string status = "";
        private string FileLabel = "";
        private int filesToDownload = 0;        
        private FtpWebRequest reqFTPDirFiles;
        private FtpWebRequest reqFTPFile;
        private TimerCallback TmrCBDownloadFiles;
        private System.Threading.Timer tmrDownloadFiles;
        private bool Initialized = false;
        private bool isRunning = false;
        private bool fileListDownloaded = false;
        private bool GridSet = false;
        private bool ProgramWasClosed = false;
        System.Threading.Thread t1;
        System.Threading.Thread t2;

        List<nFileDescription> FTPFileList = new List<nFileDescription>();

        nFileDescription curDownloadFile;

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            status = "Ready!";
            FileLabel = "None";
            filesToDownload = 0;

            timer1.Start();
            timer2.Start();
            tmrUpdateScreen.Start();
            //dtgFiles.DataSource = FTPFileList;

            //TmrCBDownloadFiles = new TimerCallback(DownloadFiles);
            //tmrDownloadFiles = new System.Threading.Timer(TmrCBDownloadFiles, null, 5000, Timeout.Infinite);

            //GetFilesForDownload();
        }

        public void DownloadFile()
        {
            string ftpServer = "tickhistory-ftp.thomsonreuters.com";
            string ftpServerPort = "15500";
            string ftpUserID = "luis.fonseca@nestinvestimentos.com.br";
            string ftpPassword = "Miletrend01";
            string Path = "results";

            int i = 0;

            if (System.IO.File.Exists(curDownloadFile.OutPutDirectory + curDownloadFile.Name.Replace("\r\n", "")))
            {
                System.IO.FileInfo curFileInfo = new FileInfo(curDownloadFile.OutPutDirectory + curDownloadFile.Name.Replace("\r\n", ""));

                if (curFileInfo.Length == curDownloadFile.Size)
                {
                    FileLabel = "File already downloaded: " + curDownloadFile.Name;
                    curDownloadFile.Downloaded = curDownloadFile.Size;
                    filesToDownload--;
                    isRunning = false;
                    return;
                }
            }

            //try
            {
                status = "Downloading";

                reqFTPFile = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServer + ":" + ftpServerPort + "/" + Path + "/" + curDownloadFile.Name));
                reqFTPFile.UseBinary = true;
                reqFTPFile.UsePassive = true;
                reqFTPFile.EnableSsl = true;
                reqFTPFile.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTPFile.Method = WebRequestMethods.Ftp.DownloadFile;

                FtpWebResponse response = (FtpWebResponse)reqFTPFile.GetResponse();
                Stream ftpDirStream = response.GetResponseStream();

                Stream OutPutFile = new FileStream(curDownloadFile.OutPutDirectory + curDownloadFile.Name.Replace("\r\n", ""), FileMode.Create, FileAccess.Write);

                curDownloadFile.StartTime = DateTime.Now;

                int bufferSize = 4096 * 2;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpDirStream.Read(buffer, 0, bufferSize);

                while (readCount > 0)
                {
                    OutPutFile.Write(buffer, 0, readCount);
                    readCount = ftpDirStream.Read(buffer, 0, bufferSize);
                    curDownloadFile.Downloaded = OutPutFile.Length;
                    if (ProgramWasClosed) break;
                }

                curDownloadFile.Downloaded = OutPutFile.Length;
                curDownloadFile.EndTime = DateTime.Now;

                ftpDirStream.Close();
                OutPutFile.Close();
                response.Close();

                FileInfo fi = new FileInfo(curDownloadFile.OutPutDirectory + curDownloadFile.Name.Replace("\r\n", ""));
                Decompress(fi);                

            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //    status = "Error!";
            //    FileLabel = curDownloadFile.Name;
                
            //}
            isRunning = false;
            filesToDownload--;

        }
        
        public bool CheckFileForDownload(string curFile)
        {
            //if ((curFile.Contains("FRA_SPA_ITA") || curFile.Contains("OneMinBar") || curFile.Contains("DITicks") || curFile.Contains("ICFTicks")) && (curFile.Contains("csv.gz")) && (!curFile.Contains("report")) && (!curFile.Contains("SP600")))
            if ((curFile.Contains("OneMinBar") || curFile.Contains("IB") || curFile.Contains("PAIBOV") || curFile.Contains("ETHANOL") || curFile.Contains("SOJA") || curFile.Contains("BOI") || curFile.Contains("Corn") || curFile.Contains("DITicks") || curFile.Contains("ICFTicks")) && (curFile.Contains("csv.gz")) && (!curFile.Contains("report")) && (!curFile.Contains("SP600")))
            {
                return true;
            }

            return false;
        }

        //KILL Process
        void Close_Import()
        {
            Process[] pArray = Process.GetProcesses();

            foreach (Process p in pArray)
            {
                string s = p.ProcessName;
                if (s.CompareTo("fileName") == 0)
                {
                    p.Kill();
                }
            }                                     
        }

        public void LoadFileList()
        {            
            string FilesInDir = "";
            string ftpServer = "tickhistory-ftp.thomsonreuters.com";
            string ftpServerPort = "15500";
            string ftpUserID = "luis.fonseca@nestinvestimentos.com.br";
            string ftpPassword = "Miletrend01";
            string Path = "results";

            status = "Loading file list from FTP Server";

            try
            {
                reqFTPDirFiles =  (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServer + ":" + ftpServerPort + "//" + Path));
                reqFTPDirFiles.UseBinary = true;
                reqFTPDirFiles.UsePassive = true;
                reqFTPDirFiles.EnableSsl = true;
                reqFTPDirFiles.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTPDirFiles.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                FtpWebResponse response = (FtpWebResponse)reqFTPDirFiles.GetResponse();                
                Stream ftpDirStream = response.GetResponseStream();
                                
                int bufferSize = 8192;
                int readCount;                              
                byte[] buffer = new byte[bufferSize];
                
                readCount = ftpDirStream.Read(buffer, 0, bufferSize);                

                while (readCount > 0)
                {                                                                       
                    foreach(byte charRead in buffer)
                    {
                        FilesInDir += (char)charRead;
                        if(charRead == '\n')
                        {
                            nFileDescription curFile = new nFileDescription();
                            if (curFile.FromString(FilesInDir))
                            {
                                if(CheckFileForDownload(curFile.Name))
                                    FTPFileList.Add(curFile);
                            }
                            FilesInDir = "";
                        }
                    }
                    readCount = ftpDirStream.Read(buffer, 0, bufferSize);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                status = "Error!";
            }

            status = "File list loaded";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Initialized)
            {
                Initialized = true;
                t1 = new Thread(InitializeData);
                t1.Start();
            }

            if (fileListDownloaded && !GridSet)
            {
                GridSet = true;
                dtgFiles.DataSource = FTPFileList;
                dtgFiles.Columns[7].DefaultCellStyle.Format = "p";
                dtgFiles.Columns[9].DefaultCellStyle.Format = "f";
                dtgFiles.Columns[0].Width = 500;
            }

            if (filesToDownload > 0 && !isRunning)
            {
                isRunning = true;
                foreach (nFileDescription curFile in FTPFileList)
                {
                    if (curFile.Downloaded == 0)
                    {
                        curDownloadFile = curFile;
                        FileLabel = "Requesting File: " + curFile.Name;
                        t2 = new Thread(DownloadFile);
                        t2.Start();
                        break;
                    }                    
                }
            }
            else if (filesToDownload == 0 && fileListDownloaded)
            {
                status = "Downloaded";
            }
            

        }
        //r
        private void InitializeData()
        {
            LoadFileList();
            UpdateFileCounter();
            fileListDownloaded = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if ((status == "No Files for Download") || (status == "Downloaded"))
            {
                status = "Exiting...";
                Application.ExitThread();
            }
        }

        private void UpdateFileCounter()
        { 
            //int tempCounter = 0;
            //foreach (nFileDescription curFile in FTPFileList)
            //{
            //    if (curFile.GetFile) tempCounter++;
            //}
            //filesToDownload = tempCounter;
            filesToDownload = FTPFileList.Count;
            if (filesToDownload == 0) status = "No Files for Download";
                
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            txtStatus.Text = status;
            txtFileName.Text = FileLabel;
            txtNumber.Text = filesToDownload.ToString();

            dtgFiles.Refresh();
            dtgFiles.Invalidate();
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProgramWasClosed = true;
        }

        public static void Decompress(FileInfo fi)
        {
            // Get the stream of the source file.
            using (FileStream inFile = fi.OpenRead())
            {
                // Get original file extension, for example
                // "doc" from report.doc.gz.
                string curFile = fi.FullName;
                string origName = curFile.Remove(curFile.Length -
                        fi.Extension.Length);
                origName = origName.Insert(origName.IndexOf("luis"), @"Unprocessed\");

                //Create the decompressed file.
                using (FileStream outFile = File.Create(origName))
                {
                    using (GZipStream Decompress = new GZipStream(inFile,
                            CompressionMode.Decompress))
                    {
                        // Copy the decompression stream 
                        // into the output file.
                        Decompress.CopyTo(outFile);

                        //Console.WriteLine("Decompressed: {0}", fi.Name);

                    }
                }
            }
        }
    }

    public class nFileDescription
    {
        string _fAccess;
        int _Number1;
        int _Number901;
        string _castle;
        double _Size;
        string _fDateTime;
        string _Name;

        double _Downloaded;
        DateTime _StartTime;
        string _OutPutDirectory;
        DateTime _EndTime;

        public string Name { get { return _Name; } set { _Name = value; } }
        public double Size { get { return _Size; } set { _Size = value; } }
        public string fDateTime { get { return _fDateTime; } set { _fDateTime = value; } }
        public DateTime StartTime { get { return _StartTime; } set { _StartTime = value; } }
        public string OutPutDirectory { get { return _OutPutDirectory; } set { _OutPutDirectory = value; } }
        public DateTime EndTime { get { return _EndTime; } set { _EndTime = value; } }

        public double Downloaded { get { return _Downloaded; } set { _Downloaded = value; } }
        public double PercentDone { get { return _Downloaded/_Size; } }
        public TimeSpan ElapsedTime
        {
            get
            {
                if (StartTime.TimeOfDay != new TimeSpan(0, 0, 0))
                {
                    if (EndTime.TimeOfDay != new TimeSpan(0, 0, 0))
                        return EndTime.TimeOfDay.Subtract(StartTime.TimeOfDay);
                    else
                        return DateTime.Now.TimeOfDay.Subtract(StartTime.TimeOfDay);
                }
                else 
                    return new TimeSpan(0, 0, 0);
            }
        }

        
        
        public double AverageKbps { get { if (StartTime.TimeOfDay != new TimeSpan(0, 0, 0)) return _Downloaded / ElapsedTime.TotalSeconds / 1000; else return 0;} }

        public bool FromString(string strDescription)
        {
            string[] tempString;

            char[] curChars = new char[1];
            curChars[0] = ' ';

            tempString = strDescription.Split(curChars, StringSplitOptions.RemoveEmptyEntries);

            if (tempString.Length > 8)
            {
                _fAccess = tempString[0];
                _Number1 = int.Parse(tempString[1]);
                _Number901 = int.Parse(tempString[2]);
                _castle = tempString[3];
                _Size = double.Parse(tempString[4]);
                _fDateTime = tempString[5] + " " + tempString[6] + " " + tempString[7];
                _Name = tempString[8];
                SetDirectory();
                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetDirectory()
        {
            if (_Name.Contains("OneMinBar"))
            {
                _OutPutDirectory = @"R:\RTICK\OneMinuteBars\OnShore\";
            }
            else if (_Name.Contains("DITicks"))
            {
                _OutPutDirectory = @"R:\RTICK\Future Files\DI\";
            }
            else if (_Name.Contains("ICFTicks"))
            {
                _OutPutDirectory = @"R:\RTICK\Future Files\ICF\";
            }
            else if (_Name.Contains("ETHANOL"))
            {
                _OutPutDirectory = @"R:\RTICK\Future Files\ETHANOL\";
            }
            else if (_Name.Contains("SOJA"))
            {
                _OutPutDirectory = @"R:\RTICK\Future Files\SOJA\";
            }
            else if (_Name.Contains("BOI"))
            {
                _OutPutDirectory = @"R:\RTICK\Future Files\BOI\";
            }
            else if (_Name.Contains("PAIBOV"))
            {
                _OutPutDirectory = @"R:\RTICK\Future Files\PAIBOV\";
            }
            else if (_Name.Contains("Corn"))
            {
                _OutPutDirectory = @"R:\RTICK\Future Files\CORN\";
            }
            else
            {
                _OutPutDirectory = @"R:\RTICK\Other Requests\";
            }
            if (_Name.Contains("FRA_SPA_ITA"))
            {
                _OutPutDirectory = @"C:\Temp\";
            }
        }

        //public string ToString()
        //{
        //    string tempReturn = "";

        //    if (_GetFile)
        //    {
        //        tempReturn = Name;
        //    }
        //    else
        //    {
        //        tempReturn = "";
        //    }

        //    return tempReturn;
        //}
    }



}
