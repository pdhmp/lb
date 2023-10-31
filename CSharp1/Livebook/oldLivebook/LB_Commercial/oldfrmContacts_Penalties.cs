using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace SGN
{
    public partial class frmContacts_Penalties : LBForm
    {
        GridSummaryItem tempItem;
        ColumnSortOrder curOrder;
        GridColumn curGrouping;

        LB_HTML HTMLEngine = new LB_HTML();
        int ExpandCounter = 0;
        int ExpandCounter2 = 0;

        public frmContacts_Penalties()
        {
            InitializeComponent();
        }

        private void frmContacts_Report_Load(object sender, EventArgs e)
        {
            /*
            dtgContReport.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgContReport.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgContReport.LookAndFeel.SetSkinStyle("Blue");
            */
            Carrega_Grid_Red();
        }
                
        private void Carrega_Grid_Red()
        {
            string StringSQl;
            SqlDataAdapter dp = new SqlDataAdapter();
            DataTable tablep = new DataTable();

            StringSQl = " SELECT DATEADD(DD, 1 - DAY(redDate), redDate) AS RefMonth, A.*, Pedro*PenaltyValue AS Pedro, Felipe*PenaltyValue AS Felipe FROM " +
                        " ( " +
                        " SELECT * FROM NESTDB.dbo.FCN_GET_Redemptions(2, '2007-12-31', '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " UNION SELECT * FROM NESTDB.dbo.FCN_GET_Redemptions(3, '2007-12-31', '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " UNION SELECT * FROM NESTDB.dbo.FCN_GET_Redemptions(10, '2007-12-31', '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " UNION SELECT * FROM NESTDB.dbo.FCN_GET_Redemptions(15, '2007-12-31', '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " ) A " +
                        " LEFT JOIN  " +
                        " ( " +
                        "   SELECT Id_Distributor " +
                        "       , SUM(CASE WHEN Id_Officer=4 THEN OfficerPercent ELSE 0 END) AS Pedro " +
                        "       , SUM(CASE WHEN Id_Officer=5 THEN OfficerPercent ELSE 0 END) AS Felipe " +
                        "   FROM dbo.Tb754_DistOfficers " +
                        "   GROUP BY Id_Distributor " +
                        " ) B " +
                        " ON A.Id_Distributor=B.Id_Distributor";

            dgRedemptions.Columns.Clear();

            dp = CargaDados.DB.Return_DataAdapter(StringSQl);
            dp.Fill(tablep);
            dtgRedemptions.DataSource = tablep;
            dp.Dispose();
            tablep.Dispose();

            dgRedemptions.Columns["FinAmount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["FinAmount"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRedemptions.Columns["NetFinAmount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["NetFinAmount"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRedemptions.Columns["RebateManagement"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["RebateManagement"].DisplayFormat.FormatString = "0.00%;(0.00%)";

            dgRedemptions.Columns["GrossManagIncome"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["GrossManagIncome"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRedemptions.Columns["NetMngIncome"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["NetMngIncome"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRedemptions.Columns["MngIncomeLessRebate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["MngIncomeLessRebate"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRedemptions.Columns["PenaltyRate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["PenaltyRate"].DisplayFormat.FormatString = "0.00%;(0.00%)";

            dgRedemptions.Columns["Rebate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["Rebate"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRedemptions.Columns["IR"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["IR"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRedemptions.Columns["PenaltyValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["PenaltyValue"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRedemptions.Columns["Pedro"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["Pedro"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRedemptions.Columns["Felipe"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["Felipe"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            Valida.SetColumnStyle(dgRedemptions, Id_usuario, 1);

            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "FinAmount", dgRedemptions.Columns["FinAmount"], "{0:#,#0}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "NetFinAmount", dgRedemptions.Columns["NetFinAmount"], "{0:#,#0}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "NetMngIncome", dgRedemptions.Columns["NetMngIncome"], "{0:#,#0.00}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "Pedro", dgRedemptions.Columns["Pedro"], "{0:#,#0.00}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "Felipe", dgRedemptions.Columns["Felipe"], "{0:#,#0.00}");

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid, Id_usuario);
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid, Id_usuario);
        }

        private void Default_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            info.GroupText = view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
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

                if ((rect.X+rect.Width/2) > view.Columns[4].Width)
                {
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

        private GridSummaryItem GetGroupByName(GridView view, string Groupname)
        {
            foreach (GridSummaryItem curItem in view.GroupSummary)
            {
                if (curItem.FieldName == Groupname)
                {
                    return curItem;
                }
            }
            return null;
        }


        private void ExpandGroups2()
        {
            dgRedemptions.CollapseAllGroups();

            ExpandCounter2++;

            if (ExpandCounter2 > dgRedemptions.GroupCount) { ExpandCounter2 = 0; };

            for (int i = -1; ; i--)
            {
                if (!dgRedemptions.IsValidRowHandle(i)) return;
                if (dgRedemptions.GetRowLevel(i) < ExpandCounter2)
                {
                    dgRedemptions.SetRowExpanded(i, true);
                }
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgRedemptions.SelectAll();
            dgRedemptions.CopyToClipboard();
        }

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            ExpandGroups2();
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            dgRedemptions.CollapseAllGroups();
            ExpandCounter2 = 0;
        }
    }
}