using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestCalc;

namespace TRDayCompositeCalc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NestCalc.CalcCompositeTRIndex.CalcTRIndex(88925, 1073);
            NestCalc.CalcCompositeTRIndex.CalcTRIndex(88926, 1073);
        }
    }
}
