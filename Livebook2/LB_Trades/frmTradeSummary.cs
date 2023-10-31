using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using LiveDLL;


namespace LiveBook
{
    public partial class frmTradeSummary : LBForm
    {
        newNestConn curConn = new newNestConn();
        old_Conn oldConn = new old_Conn();
        string SQLString;
        DataTable tempTable = new DataTable();

        public frmTradeSummary()
        {
            InitializeComponent();
        }

        private void frmTradeSummary_Load(object sender, EventArgs e)
        {
            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            cmbportfolio.SelectedValueChanged -= new System.EventHandler(this.cmbView_SelectedValueChanged);
            carrega_Combo();
            radToday.Checked = true;
            cmbportfolio.SelectedValueChanged += new System.EventHandler(this.cmbView_SelectedValueChanged);

            ExecSQL();
            Carrega_Grid();
            
            dgTradeSummary.Columns["Bought"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTradeSummary.Columns["Bought"].DisplayFormat.FormatString = "#,##0;(#,##0);-";

            dgTradeSummary.Columns["Sold"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTradeSummary.Columns["Sold"].DisplayFormat.FormatString = "#,##0;(#,##0);-";

            dgTradeSummary.Columns["Net"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTradeSummary.Columns["Net"].DisplayFormat.FormatString = "#,##0;(#,##0);-";

            dgTradeSummary.Columns["AvgPriceSold"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTradeSummary.Columns["AvgPriceSold"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000);-";

            dgTradeSummary.Columns["AvgPriceBought"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTradeSummary.Columns["AvgPriceBought"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000);-";

            dgTradeSummary.Columns["Cash_Flow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTradeSummary.Columns["Cash_Flow"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgTradeSummary.GroupSummary.Add(SummaryItemType.Sum, "Cash_Flow", dgTradeSummary.Columns["Column25"]);
            ((GridSummaryItem)dgTradeSummary.GroupSummary[dgTradeSummary.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgTradeSummary.Columns["Id_Ticker"].Visible = false;
            
            //timer1.Start();
        }

        void carrega_Combo()
        {
           // LiveDLL.FormUtils.LoadCombo(this.cmbView, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2", "Id_Portfolio", "Port_Name", 99);
            LiveDLL.FormUtils.LoadCombo(this.cmbportfolio, "Select Id_Portfolio,Port_Name FROM dbo.Tb002_Portfolios Where Discountinued <> 1 and Id_Port_Type =2 order by Port_Name", "Id_Portfolio", "Port_Name", 99);

      }
        public void Set_Portfolio_Values(int Id_Portfolio)
        {
            cmbportfolio.SelectedValue = Id_Portfolio;
        }

      public void Carrega_Grid()
        {
            DataTable tablep = new DataTable();
            
            DateTime DateToGet = new DateTime();

            string Id_Portfolio;
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            cmdRefresh.Enabled = false;

            tempTable.Clear();

            if (cmbportfolio.SelectedValue.ToString() == "System.Data.DataRowView") { return; };

            if (radToday.Checked == true) { DateToGet = DateTime.Now;};
            if (radWeek.Checked == true) { DateToGet = DateTime.Now.AddDays((int)DateTime.Now.DayOfWeek - 9);};
            if (radMonth.Checked == true) { DateToGet = DateTime.Now.AddDays(1 - (int)DateTime.Now.Day);};
            if (radYear.Checked == true) { DateToGet = Convert.ToDateTime("01/01/" + DateTime.Now.Year.ToString()); };

            Id_Portfolio = cmbportfolio.SelectedValue.ToString();

            SQLString = "SELECT * FROM [dbo].[FCN_GET_Trade_Summary]('" + Id_Portfolio + "', '" + DateToGet.ToString("yyyy-MM-dd") + "')";

            tempTable = oldConn.Return_DataTable(SQLString);

            dtg.DataSource = tempTable;

            curUtils.SetColumnStyle(dgTradeSummary, 1);

            dgTradeSummary.ExpandAllGroups();
          
            Cursor.Current = currentCursor;
            cmdRefresh.Enabled = true;

            dgTradeSummary.CollapseAllGroups();
        }

        private void ExecSQL()
        {
            
            DataTable ThreadTable = new DataTable();

            try
            {
                using (newNestConn curConn = new newNestConn())
                {
                    ThreadTable = curConn.Return_DataTable(SQLString);

                    tempTable.Clear();
                    tempTable = ThreadTable;
                    dtg.DataSource = tempTable;
                }
            }
            catch(Exception e) 
            {
                curUtils.Log_Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }
            

        }

        private void cmbView_SelectedValueChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void radToday_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void radWeek_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void radMonth_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void radYear_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void cmbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgTradeSummary.SelectAll();
            dgTradeSummary.CopyToClipboard();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgTradeSummary_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            foreach (GridSummaryItem si in view.GroupSummary)
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                    items.Add(si);
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            info.GroupText = view.GroupedColumns[level].Caption + ": " + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);

            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);
            foreach (GridGroupSummaryItem item in items)
            {
                //e.Appearance.BackColor = Color.White;
                //e.Appearance.DrawBackground(e.Cache, GetColumnBounds(view.Columns[15]));

                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                if (column == null)
                {
                    MessageBox.Show("Column name is null");
                }
                Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                // calculate summary text and boundaries
                string text = item.GetDisplayText(values[item], false);
                SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                int width = Convert.ToInt32(sz.Width) + 1;
                rect.X += rect.Width - width - 2;
                rect.Width = width;
                rect.Y = e.Bounds.Y;
                rect.Height = e.Bounds.Height - 2;
                //e.Appearance.BackColor = Color.White;
                // draw a summary values
                //e.Appearance.DrawBackground(e.Cache, e.Bounds);
                e.Appearance.DrawString(e.Cache, text, rect);
            }

            // disable default painting of the group row
            e.Handled = true;

        }

        private Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return Rectangle.Empty;
        }

        private void dgTradeSummary_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgTradeSummary);
        }

        private void dgTradeSummary_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgTradeSummary);
        }
    }
}