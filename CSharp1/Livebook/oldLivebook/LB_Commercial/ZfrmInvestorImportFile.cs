using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LiveBook
{
    public partial class frmRebates : Form
    {
        public frmRebates()
        {
            InitializeComponent();
        }

        private void lblImportFile_DragDrop(object sender, DragEventArgs e)
        {
            bool flagImport = false;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            e.Effect = DragDropEffects.None;

            foreach (string curFile in files)
            {
                if (curFile.Contains(".txt"))
                {
                    Mellon curMellonImport = new Mellon();
                    curMellonImport.ImportFileMellonPerfAdm(curFile);
                    flagImport = true;
                }

                if (flagImport == false)
                {
                    System.Windows.Forms.MessageBox.Show("File:\r\n\r\n" + curFile + "\r\n\r\nnot a valid file.", "Import failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
        }

        private void lblImportFile_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void lblImportFile_Click(object sender, EventArgs e)
        {

        }
    }
}
