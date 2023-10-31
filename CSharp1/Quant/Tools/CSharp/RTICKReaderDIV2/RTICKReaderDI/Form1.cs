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
    public partial class RTICKFileCrack : Form
    {
        public RTICKFileCrack()
        {
            InitializeComponent();
        }

        public void setProgBarValue(int value)
        {
            progBar.Value = progBar.Value + value;
        }

        public void RefreshForm()
        {
            this.Refresh();
        }

        public void setLbInstrument(string text)
        {
            lbInstrument.Text = text;
        }

        public void setLbStatus(string text)
        {
            lbStatus.Text = text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progBar.Value = 0;

            RTICKFileCracker curDI_Cracker =        new RTICKFileCracker(@"R:\RTICK\Future Files\DI\Unprocessed\");
            progBar.Value = 25;
            this.Refresh();

            RTICKFileCracker curETHANOL_Cracker = new RTICKFileCracker(@"R:\RTICK\Future Files\ETHANOL\Unprocessed");
            progBar.Value = 32;
            this.Refresh();

            RTICKFileCracker curICF_Cracker =       new RTICKFileCracker(@"R:\RTICK\Future Files\ICF\Unprocessed");
            progBar.Value = 50;
            this.Refresh();

            RTICKFileCracker curSOJA_Cracker = new RTICKFileCracker(@"R:\RTICK\Future Files\SOJA\Unprocessed");
            progBar.Value = 60;
            this.Refresh();


            RTICKFileCracker curCORN_Cracker =      new RTICKFileCracker(@"R:\RTICK\Future Files\CORN\Unprocessed");
            progBar.Value = 75;
            this.Refresh();

            RTICKFileCracker curBOI_Cracker = new RTICKFileCracker(@"R:\RTICK\Future Files\BOI\Unprocessed");
            progBar.Value = 88;
            this.Refresh();


            RTICKFileCracker curPAIBOV_Cracker =    new RTICKFileCracker(@"R:\RTICK\Future Files\PAIBOV\Unprocessed");

            progBar.Value = 0;
            lbInstrument.Text = "";
            lbStatus.Text = "Done!";
            this.Refresh();
        }         
    }
}