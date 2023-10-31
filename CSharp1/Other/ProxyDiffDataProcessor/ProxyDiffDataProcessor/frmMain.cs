using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace ProxyDiffDataProcessor
{
    public partial class frmMain : Form
    {
        int ThreadCounter = 0;       

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //PDDProcessor curProcessor = new PDDProcessor(@"R:\Data\Temp folder ProxyDiff Uncompressed\20110117_Proxydiff.txt",null);
            //curProcessor.Start(0);

            StartAllPDDProcessors();
            
        }

        private void StartAllPDDProcessors()
        {
            //string FolderPath = @"D:\Strategies\StrongOpen\TestFiles\";
            string FolderPath = @"R:\Data\Temp folder ProxyDiff Uncompressed";
            DirectoryInfo dir = new DirectoryInfo(FolderPath);

            FileInfo[] files = dir.GetFiles();

            List<ManualResetEvent> doneEvents = new List<ManualResetEvent>();           

            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Extension == ".txt")
                {
                    //CreateHistRunner(FolderPath + "\\" + files[i].Name);
                    ManualResetEvent curEvent = new ManualResetEvent(false);
                    doneEvents.Add(curEvent);
                    PDDProcessor curPDD = new PDDProcessor(FolderPath + "\\" + files[i].Name, curEvent);
                    ThreadPool.QueueUserWorkItem(curPDD.Start, i);                    

                    ThreadCounter++;
                    

                    if (ThreadCounter >= 5)
                    {
                        int freeHandle = WaitHandle.WaitAny(doneEvents.ToArray());

                        doneEvents.RemoveAt(freeHandle);

                        ThreadCounter--;
                    }                    
                }
            }
        }
    }
}
