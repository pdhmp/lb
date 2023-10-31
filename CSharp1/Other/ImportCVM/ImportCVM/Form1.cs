using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using System.Data.SqlClient;
using System.IO;


namespace ImportCVM
{
    public partial class Form1 : Form
    {
        /*
        List<grpFileItems> AllItems = new List<grpFileItems>();
        
        class grpFileItems
        {
            private string _Name = "";

            public string Name
            {
                get { return _Name; }
                set { _Name = value; }
            }
        }
        */
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //ProcessFolder(@"C:\DFP");
            ProcessFolder(@"C:\ITR");

            //ProcessFolder(@"S:\ARQUIVOS CVM\CVM\ITR");


            //MoveProcessedFiles();
            //StageFilesForReprocess(@"C:\ITR\", 4820);

            /*
            dtgFileNames.RowTemplate.Height = 18;
            
            LoadFileList();
            
            dtgFileNames.Columns[0].Width = 300;
            */
        }

        private void StageFilesForReprocess(string FolderPath, int IdCVM)
        {
            NestDLL.Log.AddLogEntry("================ Moving Files: " + FolderPath + "  =======================================", @"T:\Log\Import_CVM_Log.txt");

            DirectoryInfo dir = new DirectoryInfo(FolderPath + "\\Imported\\");

            FileInfo[] files = dir.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == ".zip" && files[i].Name.Contains("_" + IdCVM.ToString() + "_") && files[i].Name.Contains("20090"))
                {
                    try
                    {
                        File.Copy(FolderPath + "\\Imported\\" + files[i].Name, FolderPath + "\\" + files[i].Name);
                        if (File.Exists(FolderPath + "\\" + files[i].Name)) File.Delete(FolderPath + "\\Imported\\" + files[i].Name);
                    }
                    catch
                    {
                        int a = 1;
                    }
                }
            }
        }

        private void MoveProcessedFiles()
        {
            string FolderPath = @"C:\DFP";

            DirectoryInfo dir = new DirectoryInfo(FolderPath);

            using (NestConn curConn = new NestConn())
            {



                string finalSQL = "SELECT ZipName FROM nestother.dbo.Tb022_CVM_ITR_CONFIG WHERE ZipName in (SELECT ZipName FROM nestother.dbo.Tb025_CVM_ITR_DATA WHERE FileName LIKE '%DER%' GROUP BY ZipName) GROUP BY ZipName";
                SqlDataReader dr = curConn.ExecuteReader(finalSQL);
                
                DataRow curRow;
                
                while (dr.Read())
                {
                    string curFileName = dr[0].ToString();

                    try
                    {
                        File.Copy(FolderPath + "\\Imported\\" + curFileName, FolderPath + "\\" + curFileName);
                        if (File.Exists(FolderPath + "\\" + curFileName)) File.Delete(FolderPath + "\\Imported\\" + curFileName);
                    }
                    catch
                    {
                        int a = 1;
                    }
                }

            }
        }

        private void ProcessFolder(string FolderPath)
        {

            NestDLL.Log.AddLogEntry("================ Initiating SESSION for folder: " + FolderPath + "  =======================================", @"T:\Log\Import_CVM_Log.txt");
            
            DirectoryInfo dir = new DirectoryInfo(FolderPath);

            FileInfo[] files = dir.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == ".zip")
                {
                    try
                    {
                        //Console.WriteLine(FolderPath + "\\" + files[i].Name);
                        if (ImportZipFile(FolderPath + "\\" + files[i].Name))
                        {
                            File.Copy(FolderPath + "\\" + files[i].Name, FolderPath + "\\Imported\\" + files[i].Name);
                            if (File.Exists(FolderPath + "\\Imported\\" + files[i].Name)) File.Delete(FolderPath + "\\" + files[i].Name);
                        }
                        else
                        {
                            File.Copy(FolderPath + "\\" + files[i].Name, FolderPath + "\\Error\\" + files[i].Name);
                            if (File.Exists(FolderPath + "\\Error\\" + files[i].Name)) File.Delete(FolderPath + "\\" + files[i].Name);
                        }
                    }
                    catch
                    {
                        int a = 1;
                    }
                }
            }
        }

        private bool ImportZipFile(string FileAndPath)
        {
            TimeSpan StartTime = DateTime.Now.TimeOfDay;
            bool tempReturn = false;

            foreach(string curDirectory in Directory.GetDirectories(@"C:\temp\CVM_IMPORT\"))
            {
                try
                {
                    System.IO.Directory.Delete(curDirectory, true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            WinZip.UnZipFiles(FileAndPath, @"C:\temp\CVM_IMPORT", "", false);
            
            string newDir;
            string prevDir = @"C:\temp\CVM_IMPORT";

            while ((newDir = FindNextDir(prevDir)) != "")
            {
                prevDir = newDir;
            }

            string ZipFileName = FileAndPath.Substring(FileAndPath.LastIndexOf('\\') + 1);

            NestDLL.Log.AddLogEntry("Initiating import for file: " + ZipFileName, @"T:\Log\Import_CVM_Log.txt");

            if (FileAndPath.Contains("\\DFP")) 
            { 
                ReadDFP curCVM = new ReadDFP(prevDir, ZipFileName);
                if (!curCVM.bolErrors) 
                    tempReturn = true;
            }
            else if (FileAndPath.Contains("\\ITR"))
            {
                ReadITR curCVM = new ReadITR(prevDir, ZipFileName);
                if (!curCVM.bolErrors)
                    tempReturn = true;
            }

            // ============      Write Time taken to Log ================
            TimeSpan TotalTime = DateTime.Now.TimeOfDay.Subtract(StartTime);
            string curMsg;
            if (tempReturn)
            {
                curMsg = "Import Success - " + (int)TotalTime.TotalSeconds + ":" + TotalTime.Milliseconds;
            }
            else
            {
                curMsg = "Import FAIL - " + (int)TotalTime.TotalSeconds + ":" + TotalTime.Milliseconds;
            }
            NestDLL.Log.AddLogEntry(curMsg, @"T:\Log\Import_CVM_Log.txt");

            return tempReturn;
        }

        private string FindNextDir(string curPath)
        { 
            string[] curDirectory = Directory.GetDirectories(curPath);
            if (curDirectory.Length > 0)
            {
                return curDirectory[0];
            }
            else
            {
                return "";
            }
        }


        private void LoadFileList()
        {
            /*
            //DirectoryInfo dir = new DirectoryInfo(@"S:\ARQUIVOS CVM\CVM\DFP");
            DirectoryInfo dir = new DirectoryInfo(@"C:\temp\WINDOWS\TEMP_CVM");

            FileInfo[] files = dir.GetFiles();

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(files[i].Name);
                grpFileItems curItem = new grpFileItems();
                curItem.Name = files[i].Name; 
                AllItems.Add(curItem);
            }

            this.dtgFileNames.DataSource = AllItems;
            */

        }


        /*
        private void ImportFile(string impFileName)
        {
            using (StreamReader thisReader = File.OpenText (impFileName))
            { 
                string curLine = thisReader.ReadLine();
            }
                
        }

        




        private void button2_Click(object sender, EventArgs e)
        {

            string outFileName = @"C:\temp\ziptest.zip";
            string[] fileNames = new string[] { @"C:\temp\correlOut.txt" };

            //ZipFiles(fileNames, outFileName);
            WinZip.UnZipFiles(@"c:\temp\98758_4820_DFP_20061231_000000000.zip", @"c:\temp\", "", false);

            this.Close();
        }
        */



    }
}