using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestDLL;
using System.Data.SqlClient;

namespace LiveBook
{
    public partial class frmCompIndex : LBForm
    {
        bool processing = false;
        DateTime DateReport;

        public frmCompIndex()
        {
            InitializeComponent();

            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }


        private void frmCompIndex_Load(object sender, EventArgs e)
        {
            NestDLL.FormUtils.LoadCombo(cmbView, "Select Id_Portfolio, Port_Name from  dbo.Tb002_Portfolios where Id_Port_Type=2 AND Id_Portfolio=10", "Id_Portfolio", "Port_Name", 99);
            NestDLL.FormUtils.LoadCombo(cmbStrategy, "Select Id_Book, Book from dbo.Tb400_Books WHERE Id_Book IN(SELECT [Id Book] FROM NESTRT.dbo.FCN_Posicao_Atual() WHERE [Id Portfolio]=10) UNION ALL SELECT '0', 'All Books'", "Id_Book", "Book", 0);
            cmbRepType.Items.Add("By Sector");
            cmbRepType.Items.Add("By Ticker");

            Carrega_Grid(Convert.ToInt32(cmbView.SelectedValue.ToString()));
            timer1.Start();
        }

        void Carrega_Grid(int Id_Portfolio)
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            int Id_Book = 0;

            Id_Book = Convert.ToInt32(cmbStrategy.SelectedValue.ToString());

            // This part hasn't been changed to accomodate other dates (i.e. not today) nor change portfolio together with positions screen

            if (DateReport.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                SQLString = "SELECT [Sector] AS Item, PercFund, PercIndex, Diff FROM dbo.FCN_GET_Sector_vs_Index(" + Id_Portfolio + ", 0, " + Id_Book + ");";
            }
            else
            {
                SQLString = "SELECT [Sector] AS Item, PercFund, PercIndex, Diff FROM dbo.FCN_GET_Sector_vs_Index(" + Id_Portfolio + ", 0, " + Id_Book + ");";
            }

            if (cmbRepType.SelectedIndex == 1)
            {
                SQLString = SQLString.Replace("FCN_GET_Sector_vs_Index", "FCN_GET_Position_vs_Index");
                SQLString = SQLString.Replace("[Sector]", "[Base Underlying]");
            }

            try
            {
                using (newNestConn curConn = new newNestConn())
                {
                    tablep = curConn.Return_DataTable(SQLString);

                    dtg.DataSource = tablep;
                }
                curUtils.SetColumnStyle(dgCompIndex, 1);
            }
            catch(Exception e) 
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }

            dgCompIndex.Columns["PercFund"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCompIndex.Columns["PercFund"].DisplayFormat.FormatString = "P2";

            dgCompIndex.Columns["PercIndex"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCompIndex.Columns["PercIndex"].DisplayFormat.FormatString = "P2";

            dgCompIndex.Columns["Diff"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgCompIndex.Columns["Diff"].DisplayFormat.FormatString = "P2";
        }

        public void Set_Portfolio_Values(int Id_Portfolio,DateTime Historical)
        {
            cmbView.SelectedValue = Id_Portfolio;
            DateReport = Historical;

            if (DateReport.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                this.Text = "Exposure Book";
            }
            else
            {
                this.Text = "Exposure Book (HISTORICAL as of " + DateReport.ToString("dd/MM/yy") + ")";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!processing)
            {
                processing = true;
                Carrega_Grid(Convert.ToInt32(cmbView.SelectedValue.ToString()));
                processing = false;
            }
        }

        private void dgCompIndex_MouseUp(object sender, MouseEventArgs e)
        {
            processing = false;

        }

        private void dgCompIndex_MouseDown(object sender, MouseEventArgs e)
        {
            processing = true;

        }

        private void dgCompIndex_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgCompIndex);
            timer1.Start();
        }

        private void dgCompIndex_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgCompIndex);
            timer1.Start();
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgCompIndex.SelectAll();
            dgCompIndex.CopyToClipboard();
        }

    }
}