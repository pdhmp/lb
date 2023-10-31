using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VWAPS
{
    public partial class Form1 : Form
    {
        private DateTime InitialDate;
        private DateTime EndDate;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cbCalcTRDay.Checked = true;

            if (DateTime.Today.AddDays(-1).DayOfWeek == DayOfWeek.Sunday)
            {
                cmbIniDate.Value = DateTime.Today.AddDays(-3);
            }
            else
            {
                cmbIniDate.Value = DateTime.Today.AddDays(-1);
            }

            cmbEndDate.Value = DateTime.Today;
        }

        private void ok_Click(object sender, EventArgs e)
        {

            QSEG_Calculator QSEG = new QSEG_Calculator(new DateTime(2000, 01, 01));

            AVG_5_Days_Calculator AVG_5_Days = new AVG_5_Days_Calculator();

            InitialDate = cmbIniDate.Value;
            EndDate = cmbEndDate.Value;
            DateTime aux = DateTime.Today;
            TimeSpan diff = aux - InitialDate;
            int sub = diff.Days;

            if (cbCalcTRDay.Checked)
            {
                QSEG.CalculateAllTRDay(InitialDate, EndDate);
            }

            QSEG.CalculateHistoricaNewIndexes(InitialDate, EndDate);

            QSEG.CalculateAllVWAPS(InitialDate, EndDate);

            this.Close();
        }

        private void CalcDate_ValueChanged(object sender, EventArgs e)
        {
            InitialDate = cmbIniDate.Value;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            EndDate = cmbEndDate.Value;
        }
    }
}

