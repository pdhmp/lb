using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RTICKReaderDI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         // /  //RTICKFileCracker curDI_Cracker = new RTICKFileCracker(@"R:\RTICK\Future Files\DI\Unprocessed\", new TimeSpan(16, 05, 00));
           // RTICKFileCracker curICF_Cracker = new RTICKFileCracker(@"R:\RTICK\Future Files\ICF\Unprocessed", new TimeSpan(14, 25, 00));
            RTICKFileCracker curCORN_Cracker = new RTICKFileCracker(@"R:\RTICK\Future Files\CORN\Unprocessed", new TimeSpan(15, 20, 00));
           // RTICKFileCracker curPAIBOV_Cracker = new RTICKFileCracker(@"R:\RTICK\Future Files\PAIBOV\Unprocessed", new TimeSpan(16, 55, 00));
            //curCracker.ReadNLines(1000, @"C:\TEMP\DIFile.csv");
            //curCracker.SeparateByTicker();
            //curCracker.ProcessFile();
        }
    }
}