using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;



namespace ReadPortMellon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdImport_Click(object sender, EventArgs e)
        {
            ImpMellonPortfolio curImport = new ImpMellonPortfolio();
            string curFileName = "";
            string curPath = @"T:\Import\MELLON\";

            openFileDialog1.InitialDirectory = curPath;
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                curFileName = openFileDialog1.FileName;
            }


            curImport.ImportFile(curFileName);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
