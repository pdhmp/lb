using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using LiveBook.Business;

using NestDLL;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace LiveBook
{
    public partial class frmRiskMarginalVARs : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmRiskMarginalVARs()
        {
            InitializeComponent();
        }

        private void frmRiskMarginalVARs_Load(object sender, EventArgs e)
        {
            

            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            Carrega_Grid();

            dgMarginalVars.GroupSummary.Add(SummaryItemType.Sum, "MVAR %", dgMarginalVars.Columns["MVAR %"]);
            ((GridSummaryItem)dgMarginalVars.GroupSummary[dgMarginalVars.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";
            
            dgMarginalVars.GroupSummary.Add(SummaryItemType.Sum, "VAR %", dgMarginalVars.Columns["VAR %"]);
            ((GridSummaryItem)dgMarginalVars.GroupSummary[dgMarginalVars.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgMarginalVars.GroupSummary.Add(SummaryItemType.Sum, "VAR_Total", dgMarginalVars.Columns["VAR_Total"]);
            ((GridSummaryItem)dgMarginalVars.GroupSummary[dgMarginalVars.GroupSummary.Count - 1]).DisplayFormat = "{0:#,###.}";

            dgMarginalVars.GroupSummary.Add(SummaryItemType.Sum, "VAR_Marginal", dgMarginalVars.Columns["VAR_Marginal"]);
            ((GridSummaryItem)dgMarginalVars.GroupSummary[dgMarginalVars.GroupSummary.Count - 1]).DisplayFormat = "{0:#,###.}";

            cmbPortfolio.SelectedIndexChanged -= new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            carrega_Combo();
            cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            cmbPortfolio_SelectedIndexChanged(sender, e);

            if (dgMarginalVars.GroupCount > 0)
            {
                GridColumn firstGroupingColumn = dgMarginalVars.SortInfo[0].Column;
                dgMarginalVars.GroupSummarySortInfo.Add(((GridSummaryItem)dgMarginalVars.GroupSummary[dgMarginalVars.GroupSummary.Count - 1]), ColumnSortOrder.Descending, firstGroupingColumn);
            }
        }

        void Carrega_Grid()
        {
            if (cmbPortfolio.SelectedValue != null)
            {
                int Id_Portfolio;

                string SQLString;

                Id_Portfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());

                DataTable tablep = new DataTable();

                SQLString = "SELECT * FROM dbo.FCN_GET_Position_Var (" + Id_Portfolio + ")";

                tablep = curConn.Return_DataTable(SQLString);

                dtg.Refresh(); 
                dtg.DataSource = tablep;

                dgMarginalVars.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgMarginalVars.Columns[0].DisplayFormat.FormatString = "dd/MMM HH:mm"; 

                dgMarginalVars.Columns[3].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgMarginalVars.Columns[3].DisplayFormat.FormatString = "#,#0;(#,#0);-"; ;

                dgMarginalVars.Columns[4].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgMarginalVars.Columns[4].DisplayFormat.FormatString = "#,#0;(#,#0);-"; ;

                dgMarginalVars.Columns[5].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgMarginalVars.Columns[5].DisplayFormat.FormatString = "#,#0;(#,#0);-"; ;

                dgMarginalVars.Columns[6].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgMarginalVars.Columns[6].DisplayFormat.FormatString = "#,#0;(#,#0);-"; ;

                dgMarginalVars.Columns[7].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgMarginalVars.Columns[7].DisplayFormat.FormatString = "#,#0;(#,#0);-"; ;
                
                dgMarginalVars.Columns["VAR %"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgMarginalVars.Columns["VAR %"].DisplayFormat.FormatString = "{0:p2}"; ;

                dgMarginalVars.Columns["MVAR %"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgMarginalVars.Columns["MVAR %"].DisplayFormat.FormatString = "{0:p2}"; ;

                for (int curRow=0; curRow < dgMarginalVars.RowCount; curRow++)
                {
                    if (dgMarginalVars.GetRowCellValue(curRow, "VAR_DateTime").ToString() != "") { lblUpdateTime.Text = dgMarginalVars.GetRowCellValue(curRow, "VAR_DateTime").ToString(); };
                }
                curUtils.SetColumnStyle(dgMarginalVars, 1, "PositionLB");

            }
        }

        public void Set_Portfolio_Values(int Id_Portfolio)
        {
            cmbPortfolio.SelectedValue = Id_Portfolio;
        }

        void carrega_Combo()
        {
            NestDLL.FormUtils.LoadCombo(this.cmbPortfolio, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2 UNION ALL SELECT '-1', 'All Portfolios'", "Id_Portfolio", "Port_Name", 99);
        }

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgMarginalVars_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dgMarginalVars);
        }

        private void dgMarginalVars_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dgMarginalVars);
        }

        private void dgresume_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
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

                // draw a summary values
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

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            //cmbPortfolio_SelectedIndexChanged(sender, e);
            Carrega_Grid();
        }
    }
}