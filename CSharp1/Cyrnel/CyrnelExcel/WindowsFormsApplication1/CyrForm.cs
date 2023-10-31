using System.Data.OleDb;
using System.Threading;
using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CyrnelSync
{
    public partial class CyrForm : Form
    {
        CyrnelGenerator curCyrnelGenerator = new CyrnelGenerator();

        private bool _AutoUpdate;
        public bool AutoUpdate
        {
            get
            {
                return _AutoUpdate;
            }
            set
            {
                if (value == true)
                {
                    _AutoUpdate = value;
                    txtAutoUpdateON.Text = "ON";
                    txtAutoUpdateON.ForeColor = Color.Green;
                    UpdatePortfolio();
                }
                else
                {
                    _AutoUpdate = value;
                    txtAutoUpdateON.Text = "OFF";
                    txtAutoUpdateON.ForeColor = Color.Red;
                }
            }
        }


        public CyrForm() { InitializeComponent(); }

        private void Form1_Load(object sender, EventArgs e) { dateTimePicker1.Value = DateTime.Now; AutoUpdate = true; }


        private void bntUpdate_Click(object sender, EventArgs e)
        {
            AutoUpdate = true;
        }

        private void btnStopUpdate_Click(object sender, EventArgs e)
        {
            AutoUpdate = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //CurDate = Convert.ToDateTime(dateTimePicker1.Value.ToString());
            //GetLastDate();
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            UpdatePortfolio();
        }

        private void UpdatePortfolio()
        {
            txtLastUpdated.Text = DateTime.Now.ToString("HH:mm:ss");
            curCyrnelGenerator.CreateFile();
        }
    }
}
