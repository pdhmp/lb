using System;
using System.Drawing;
using System.Collections;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using StreamBase.SB;
using StreamBase.SB.Client;
using System.Collections.Generic;

namespace Dashboards
{
    public partial class Dashboards : Form
    {
       // private IContainer components;
        public Dashboards()
        {
            InitializeComponent();
        }
        static void Main()
        {
            Application.Run(new Dashboards());
        }       
        
        private void monitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //QSEGMonitor qsegmon = new QSEGMonitor();
            QSEGMonitor qsegmon = new QSEGMonitor();
            qsegmon.Show();
            qsegmon.MdiParent = this;
        }

        private void momentumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Momentum mommon = new Momentum();
            mommon.Show();
            mommon.MdiParent = this;
        }

        private void cBOToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            CBO cbo = new CBO();
            cbo.Show();
            cbo.MdiParent = this;
        }

        private void orderReviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrderReview orv = new OrderReview();
            orv.Show();
            orv.MdiParent = this;
        }

        private void controllerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controller contr = new Controller();
            contr.Show();
            contr.MdiParent = this;
        }  
    }
}
