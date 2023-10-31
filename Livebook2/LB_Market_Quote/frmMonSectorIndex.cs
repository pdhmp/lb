using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using LiveDLL;



namespace LiveBook
{
    public partial class frmMonSectorIndex : LBForm
    {
        newNestConn curConn = new newNestConn();

        bool processing = false;

        public frmMonSectorIndex()
        {
            InitializeComponent();
        }

        private void frmMonSectorIndex_Load(object sender, EventArgs e)
        {
            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            Carrega_Grid();
            timer1.Start();
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }

        void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            SQLString = "Select * FROM [dbo].[FCN_GET_Sector_Idx_Perf] ()";

            tablep = curConn.Return_DataTable(SQLString);

            dtg.DataSource = tablep;
            

            curUtils.SetColumnStyle(dgSecIndexPerf, 1);

            //dgresume.Columns["Portfolio"].Width = 200;

            dgSecIndexPerf.Columns["Val_Prev"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgSecIndexPerf.Columns["Val_Prev"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)"; 

            dgSecIndexPerf.Columns["Val_Now"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgSecIndexPerf.Columns["Val_Now"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)"; 

            dgSecIndexPerf.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgSecIndexPerf.Columns["Change"].DisplayFormat.FormatString = "+0.00%;-0.00%;-";

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!processing)
            {
                processing = true;
                Carrega_Grid();
                processing = false;
            }
        }

        private void dgresume_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgSecIndexPerf);
            timer1.Start();

        }

        private void dgresume_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            timer1.Stop();
            curUtils.Save_Columns(dgSecIndexPerf);
            timer1.Start();

        }

        private void dgresume_MouseUp(object sender, MouseEventArgs e)
        {
            processing = false;

        }

        private void dgresume_MouseDown(object sender, MouseEventArgs e)
        {
            processing = true;

        }

        private void dgSecIndexPerf_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.CellValue != null && e.CellValue.ToString()!="")
            {
                if (e.Column.Name == "colChange")
                {
                    if (Convert.ToSingle(e.CellValue) > 0 && e.Appearance.ForeColor != Color.Green)
                    {
                        e.Appearance.ForeColor = Color.Green;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                    else if (Convert.ToSingle(e.CellValue) < 0 && e.Appearance.ForeColor != Color.Red)
                    {
                        e.Appearance.ForeColor = Color.Red;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                    }
                    else if (Convert.ToSingle(e.CellValue) == 0 && e.Appearance.ForeColor != Color.Black)
                    {
                        e.Appearance.ForeColor = Color.Red;
                        e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                    }
                }
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgSecIndexPerf.SelectAll();
            dgSecIndexPerf.CopyToClipboard();
            //  MessageBox.Show("Copied!");

        }
    }
}