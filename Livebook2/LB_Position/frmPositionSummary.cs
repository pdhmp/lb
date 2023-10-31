using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraExport;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using LiveDLL;

namespace LiveBook
{
    public partial class frmPositionSummary : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmPositionSummary()
        {
            InitializeComponent();
        }

        private void frmTop10_Load(object sender, EventArgs e)
        {
            cmbView.SelectedValueChanged -= new System.EventHandler(this.cmbView_SelectedValueChanged);
            carrega_Combo();
            cmbView.SelectedValueChanged += new System.EventHandler(this.cmbView_SelectedValueChanged);
            Carrega_Grid();
            timer1.Start();
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }

        void carrega_Combo()
        {
            int Id_Portfolio = Convert.ToInt32(cmbView.SelectedValue.ToString());

            LiveDLL.FormUtils.LoadCombo(this.cmbStrategy, "Select Id_Book, Book from dbo.Tb400_Books WHERE Id_Book IN(SELECT [Id Book] FROM NESTRT.dbo.FCN_Posicao_Atual() WHERE [Id Portfolio]=" + Id_Portfolio + ") UNION ALL SELECT '0', 'All Books'", "Id_Book", "Book", 0);

            cmbGroup.Items.Insert(0, "Base Underlying");
            cmbGroup.Items.Insert(1, "Ticker");
            cmbGroup.Items.Insert(2, "Nest Sector");
            cmbGroup.Items.Insert(3, "Security Currency");
            cmbGroup.Items.Insert(4, "Asset Class");
            cmbGroup.Items.Insert(5, "Side");
            cmbGroup.Items.Insert(6, "Commodities");
            cmbGroup.SelectedIndex = 0;
        }

        public void Set_Portfolio_Values(int Id_Portfolio)
        {
            cmbView.SelectedValue = Id_Portfolio;
        }

        public void Carrega_Grid()
        {
            string SQLString;

            DataTable tablep = new DataTable();

            int Id_Portfolio = 0;
            int Id_Book = 0;
            string Item;
            string strACList = "";

            int selRow = 0;

            selRow = dgTop10Desc.FocusedRowHandle;

            if (selRow < 0) selRow = 0;
            int selRowFromLast = dgTop10Desc.RowCount - dgTop10Desc.FocusedRowHandle;

            try
            {
                Id_Portfolio = Convert.ToInt32(cmbView.SelectedValue.ToString());

                Id_Book = Convert.ToInt32(cmbStrategy.SelectedValue.ToString());

                Item = cmbGroup.SelectedItem.ToString();

                string PerfFieldName = "[Contribution pC Admin]";

                if (chkEquities.Checked) { strACList = strACList + ", 1"; };
                if (chkMacro.Checked) { strACList = strACList + ", 2,4,3"; };
                if (chkExCurrency.Checked) { PerfFieldName = "[Contribution pC Admin]"; };

                if (Item == "Side")
                {
                    Item = "CASE WHEN [Delta/NAV]>0 THEN 'Long' ELSE 'Short' END ";
                }
                else
                {
                    Item = "[" + Item + "]";
                }

                if (Item == "[Commodities]")
                {
                    Item = "NESTDB.[dbo].[FCN_IsCommSector]([Nest Sector]) ";
                }

                if (Item == "Ticker")
                {
                    SQLString = "Select " + Item + " as Item,sum(" + PerfFieldName + ") AS Contrib, (B.Last / B.[Close]) -1 [Change], " +
                                " SUM([Delta/NAV]) AS Size " +
                                " FROM NESTRT.dbo.FCN_Posicao_Atual() A " +
                                " INNER JOIN  " +
                                     " ( " +
                                          " SELECT X.IdSecurity , X.SrValue as 'Close' , Y.SrValue as 'Last' " +
                                          "    FROM NESTRT.dbo.Tb065_Ultimo_Preco X  " +
                                          "    inner join NESTRT.dbo.Tb065_Ultimo_Preco Y  " +
                                          "        ON X.IdSecurity = Y.IdSecurity  " +
                                          "    WHERE X.SrType = 2 AND Y.SrType = 1 " +
                                       " ) B " +
                                    "  ON A.[Id Ticker]  = B.IdSecurity " +
                                " WHERE [Asset Class] NOT IN (0" + strACList + ") AND [Id Book]<>5 AND [Id Portfolio]= " + Id_Portfolio + " group by " + Item + " , ,B.Last / B.[Close] -1  order by Contrib asc";
                }
                else
                {
                    SQLString = " Select " + Item + " as Item,sum(" + PerfFieldName + ") AS Contrib, " +
                                " ROW_NUMBER() OVER(ORDER BY sum(" + PerfFieldName + ") asc) as RowNumberCountrib," +
                                " (B.Last / B.[Close]) -1 [Change], " +
                                " ROW_NUMBER() OVER(ORDER BY CASE WHEN sum([Delta/NAV])=0 THEN 0 ELSE (sum(" + PerfFieldName + ")/sum([Delta/NAV])) END asc) as RowNumberChange," +
                                " SUM([Delta/NAV]) AS Size, " +
                                " ROW_NUMBER() OVER(ORDER BY SUM([Delta/NAV]) asc) as RowNumberSize " +
                                " FROM NESTRT.dbo.FCN_Posicao_Atual() A " +
                                   " INNER JOIN  " +
                                     "   ( " +
                                     "     SELECT X.IdSecurity , X.SrValue as 'Close' , Y.SrValue as 'Last' " +
                                     "      FROM NESTRT.dbo.Tb065_Ultimo_Preco X  " +
                                     "   inner join NESTRT.dbo.Tb065_Ultimo_Preco Y  " +
                                     "     ON X.IdSecurity = Y.IdSecurity  " +
                                     "     WHERE X.SrType = 2 AND Y.SrType = 1 " +
                                     "   ) B " +
                                     " ON A.[Id Underlying]  = B.IdSecurity " +
                                " WHERE [Id Asset Class] IN (0" + strACList + ") AND [Id Book]<>5 AND [Id Portfolio]= " + Id_Portfolio + " group by " + Item + " ,B.Last / B.[Close] -1  order by Contrib asc";
                    // " WHERE [Id Asset Class] IN (0" + strACList + ") AND ([Id Book]<>5 or [Id Section]<>1) AND [Id Portfolio]= " + Id_Portfolio + " group by " + Item + " order by Contrib asc";
                }

                if (Id_Book > 0)
                {

                    SQLString = SQLString.Replace("[Id Book]<>5", "[Id Book]=" + Id_Book);
                    SQLString = SQLString.Replace("[Contribution pC Admin]", "[Contribution pC Book]");
                    SQLString = SQLString.Replace("[Delta/NAV]", "[Delta/Book]");


                    //SQLString = SQLString.Replace("[Id Book]<>5", "[Id Book]=" + Id_Book);
                    //SQLString = SQLString.Replace(PerfFieldName, PerfFieldName.Substring(0, PerfFieldName.Length-1) + " Book]");
                    //SQLString = SQLString.Replace("[Delta/NAV]", "[Delta/Book]");
                }

                tablep = curConn.Return_DataTable(SQLString);

                dtg.DataSource = tablep;

                dgTop10Desc.Columns["RowNumberCountrib"].Visible = false;
                dgTop10Desc.Columns["RowNumberChange"].Visible = false;
                dgTop10Desc.Columns["RowNumberSize"].Visible = false;

                dgTop10Desc.Columns["Contrib"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgTop10Desc.Columns["Contrib"].DisplayFormat.FormatString = "P2";

                dgTop10Desc.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgTop10Desc.Columns["Change"].DisplayFormat.FormatString = "+0.00%;-0.00%;-";

                dgTop10Desc.Columns["Size"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgTop10Desc.Columns["Size"].DisplayFormat.FormatString = "P2";

                if (selRowFromLast > 0)
                {
                    dgTop10Desc.FocusedRowHandle = tablep.Rows.Count - selRowFromLast;
                }
                else
                {
                    dgTop10Desc.FocusedRowHandle = tablep.Rows.Count - 1;
                }

            }
            catch
            {


            }
        }

        private void cmbView_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbView.SelectedIndex > 0)
            {
                int Id_Portfolio = Convert.ToInt32(cmbView.SelectedValue.ToString());
                LiveDLL.FormUtils.LoadCombo(this.cmbStrategy, "Select Id_Book, Book from dbo.Tb400_Books WHERE Id_Book IN(SELECT [Id Book] FROM NESTRT.dbo.FCN_Posicao_Atual() WHERE [Id Portfolio]=" + Id_Portfolio + ") UNION ALL SELECT '0', 'All Books'", "Id_Book", "Book", 0);
            }
            Carrega_Grid();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Carrega_Grid();

        }

        private void dgTop10Desc_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "colContributionpC")
            {
                if (Convert.ToSingle(e.CellValue) > 0.0010)
                {
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if (Convert.ToSingle(e.CellValue) < -0.0010)
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else
                {
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Regular);
                }
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgTop10Desc.SelectAll();
            dgTop10Desc.CopyToClipboard();

        }

        private void chkTop_CheckedChanged(object sender, EventArgs e)
        {

            if (chkTop.Checked)
            {
                int rowcount = dgTop10Desc.RowCount;
                string FilterOrder = "[RowNumberSize] <= 20.0 Or [RowNumberSize] >=" + Convert.ToString(rowcount - 20);
                dgTop10Desc.ActiveFilterString = FilterOrder;

            }

            else
            {
                dgTop10Desc.ActiveFilter.Clear();
            }

        }

        private void dgTop10Desc_Click(object sender, EventArgs e)
        {

            //string Order_Status = Convert.ToString(dgTop10Desc.GetFocusedRowCellValue(sender.

            //  MessageBox.Show(sender.ToString());

        }

        private void dgTop10Desc_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    if (downHitInfo.Column != null)
                    {
                        //DataRow row = view.GetDataRow(downHitInfo.RowHandle);
                        string curCol = downHitInfo.Column.Name;
                        string IdTicker = "DRAGITEM\t" + dgTop10Desc.GetRowCellValue(downHitInfo.RowHandle, curCol.Replace("col", "")).ToString();
                        view.GridControl.DoDragDrop(IdTicker, DragDropEffects.Move);

                        downHitInfo = null;
                        DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                    }
                }
            }
        }

        GridHitInfo downHitInfo = null;

        private void dgTop10Desc_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfo = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                downHitInfo = hitInfo;
        }


    }
}