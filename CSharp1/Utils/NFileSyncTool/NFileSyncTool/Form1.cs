using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace NFileSyncTool
{
    public partial class frmSyncFiles : Form
    {

        FileSync curFileSync = new FileSync();


        public frmSyncFiles()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tmrUpdate.Start();
        }

        private void cmdSync_Click(object sender, EventArgs e)
        {
            Thread t1 = new Thread(new ThreadStart(Syncfiles));
            t1.Start();
        }

        private void Syncfiles()
        {
            curFileSync.sourcePath = @"N:\Administrativo";
            curFileSync.destinationPath = @"\\192.168.1.5\Dados\nest\Administrativo";
            curFileSync.Sync();
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            txtStatus.Text = curFileSync.StatusMsg;
            txtStatus.ScrollToCaret();
        }
    }
}
