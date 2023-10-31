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
using Ionic.Zip;

namespace NestZipCompressor
{
    public partial class NestZipCompressor : Form
    {
        string Path = "", File = "", Status = "";     
        object curSync = new object();
       
        public NestZipCompressor()
        {
            InitializeComponent();
            Status = "Ready!";           
            
        }       

        private void AutoCompress()
        {
            try
            {
                FileStream AutoStream = new FileStream(@"C:\Temp\Compress_Files_Paths.txt", FileMode.Open);
                StreamReader Reader = new StreamReader(AutoStream);
                string ReadLine = "";

                while (!((ReadLine = Reader.ReadLine()) == null))
                {
                    char[] curSeparator = {'\t'};                    
                    string[] Paths = ReadLine.Split(curSeparator, StringSplitOptions.RemoveEmptyEntries);
                    Path = Paths[0];                    
                    File = Paths[1];                   
                    Compress();
                }
                Status = "Done!";
            }
            catch(Exception E)
            {
                MessageBox.Show(E.Message);
                Status = "Error!";
            }
        }

        private void Compress()
        {
            try
            {
                using (ZipFile Zip = new ZipFile())
                {
                    string date = "";
                    if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
                    {
                        date = DateTime.Today.Date.AddDays(-3).ToString("yyyyMMdd");
                    }
                    else if (!((DateTime.Today.DayOfWeek == DayOfWeek.Saturday) || (DateTime.Today.DayOfWeek == DayOfWeek.Sunday)))
                    {
                        date = DateTime.Today.Date.AddDays(-1).ToString("yyyyMMdd");
                    }                                        
                    FileStream OutputStream = new FileStream("C:\\Temp\\" + date + "_" + File.Replace(".txt",".zip"), FileMode.Create);
                    Zip.AddFile(Path + @"\" + date + "_" + File);
                    Status = "Compressing...";
                    Zip.Save(OutputStream);
                    Status = "Compressed!";
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message);
                Status = "Error!";
            }
        }

        private void UpdateScreen()
        {    
            lblStatus.Text = Status;
            txtFile.Text = File;
            txtPath.Text = Path;            
        }
        
        private void timer2_Tick(object sender, EventArgs e)
        {
            UpdateScreen();
        }
              
        private void timer1_Tick(object sender, EventArgs e)
        {
            if ((DateTime.Today.DayOfWeek != DayOfWeek.Saturday) || (DateTime.Today.DayOfWeek != DayOfWeek.Sunday))
            {
                btnAuto.PerformClick();
            }
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            AutoCompress();            
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            Compress();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (Status == "Done!")
            {
                Application.ExitThread();
            }
        }
    }
}
