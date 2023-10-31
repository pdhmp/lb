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

using NestDLL;

namespace SGN
{
    public partial class frmContacts_Report : LBForm
    {
        GridSummaryItem tempItem;
        ColumnSortOrder curOrder;
        GridColumn curGrouping;

        LB_HTML HTMLEngine = new LB_HTML();
        int ExpandCounter = 0;
        int ExpandCounter2 = 0;

        public frmContacts_Report()
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
            Carrega_Grid();
            Carrega_Grid_Red();
        }

        private void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            SQLString = " SELECT A.*, Pedro*NetMngIncome AS Pedro, Felipe*NetMngIncome AS Felipe, Pedro*NetMngIncome + Felipe*NetMngIncome AS TotalOfficers FROM " +
                        " ( " +
                        " SELECT * FROM NESTDB.dbo.FCN_COM_Contact_Incomes(2, '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " UNION ALL SELECT * FROM NESTDB.dbo.FCN_COM_Contact_Incomes(3, '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " UNION ALL SELECT * FROM NESTDB.dbo.FCN_COM_Contact_Incomes(12, '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " UNION ALL SELECT * FROM NESTDB.dbo.FCN_COM_Contact_Incomes(15, '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " ) A " +
                        " LEFT JOIN  " +
                        " ( " +
                        " 	SELECT Id_Distributor " +
                        " 		, SUM(CASE WHEN Id_Officer=4 THEN OfficerPercent ELSE 0 END) AS Pedro " +
                        " 		, SUM(CASE WHEN Id_Officer=5 THEN OfficerPercent ELSE 0 END) AS Felipe " +
                        " 	FROM dbo.Tb754_DistOfficers " +
                        " 	GROUP BY Id_Distributor " +
                        " ) B " +
                        " ON A.Id_Distributor=B.Id_Distributor";

            dgContReport.Columns.Clear();

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgContReport.DataSource = tablep;
            
            tablep.Dispose();

            dgContReport.Columns["FinAmount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["FinAmount"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgContReport.Columns["NetFinAmount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["NetFinAmount"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgContReport.Columns["RebateManag"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["RebateManag"].DisplayFormat.FormatString = "0.00%;(0.00%)";

            dgContReport.Columns["GrossMngIncome"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["GrossMngIncome"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgContReport.Columns["NetMngIncome"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["NetMngIncome"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgContReport.Columns["MngIncomeLessRebate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["MngIncomeLessRebate"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgContReport.Columns["Rebate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["Rebate"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgContReport.Columns["IR"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["IR"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgContReport.Columns["Pedro"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["Pedro"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgContReport.Columns["Felipe"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["Felipe"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgContReport.Columns["TotalOfficers"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["TotalOfficers"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            Valida.SetColumnStyle(dgContReport, 1);

            dgContReport.GroupSummary.Add(SummaryItemType.Sum, "FinAmount", dgContReport.Columns["OriginalValue"], "{0:#,#0}");
            dgContReport.GroupSummary.Add(SummaryItemType.Sum, "NetFinAmount", dgContReport.Columns["NetFinAmount"], "{0:#,#0}");
            dgContReport.GroupSummary.Add(SummaryItemType.Sum, "NetMngIncome", dgContReport.Columns["NetMngIncome"], "{0:#,#0.00}");
            dgContReport.GroupSummary.Add(SummaryItemType.Sum, "Pedro", dgContReport.Columns["Pedro"], "{0:#,#0.00}");
            dgContReport.GroupSummary.Add(SummaryItemType.Sum, "Felipe", dgContReport.Columns["Felipe"], "{0:#,#0.00}");
            dgContReport.GroupSummary.Add(SummaryItemType.Sum, "TotalOfficers", dgContReport.Columns["TotalOfficers"], "{0:#,#0.00}");

        }

        private void Carrega_Grid_Red()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            SQLString = " SELECT DATEADD(DD, 1 - DAY(redDate), redDate) AS RefMonth, A.*, Pedro*PenaltyValue AS Pedro, Felipe*PenaltyValue AS Felipe, Pedro*PenaltyValue + Felipe*PenaltyValue AS TotalOfficers FROM " +
                        " ( " +
                        " SELECT * FROM NESTDB.dbo.FCN_COM_Redemptions(2, '2007-12-31', '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " UNION ALL SELECT * FROM NESTDB.dbo.FCN_COM_Redemptions(3, '2007-12-31', '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " UNION ALL SELECT * FROM NESTDB.dbo.FCN_COM_Redemptions(12, '2007-12-31', '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
                        " UNION ALL SELECT * FROM NESTDB.dbo.FCN_COM_Redemptions(15, '2007-12-31', '" + DateTime.Today.ToString("yyyy-MM-dd") + "') " +
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

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgRedemptions.DataSource = tablep;
            
            tablep.Dispose();

            dgRedemptions.Columns["OriginalValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["OriginalValue"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgRedemptions.Columns["RedemptionValue"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgRedemptions.Columns["RedemptionValue"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

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

            dgContReport.Columns["TotalOfficers"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContReport.Columns["TotalOfficers"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            Valida.SetColumnStyle(dgRedemptions, 1);

            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "OriginalValue", dgRedemptions.Columns["OriginalValue"], "{0:#,#0}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "RedemptionValue", dgRedemptions.Columns["OriginalValue"], "{0:#,#0}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "NetFinAmount", dgRedemptions.Columns["NetFinAmount"], "{0:#,#0}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "NetMngIncome", dgRedemptions.Columns["NetMngIncome"], "{0:#,#0.00}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "PenaltyValue", dgRedemptions.Columns["PenaltyValue"], "{0:#,#0.00}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "Pedro", dgRedemptions.Columns["Pedro"], "{0:#,#0.00}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "Felipe", dgRedemptions.Columns["Felipe"], "{0:#,#0.00}");
            dgRedemptions.GroupSummary.Add(SummaryItemType.Sum, "TotalOfficers", dgRedemptions.Columns["TotalOfficers"], "{0:#,#0.00}");

        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid);
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid);
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

        private void dgContReport_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Clicks > 1) return;
            GridView view = sender as GridView;
            if (view.State != GridState.ColumnDown) return;

            //info.Column.SortOrder

            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);
            if (info.HitTest == GridHitTest.Column)
            {
                curOrder = ColumnSortOrder.Descending;
                if (info.Column.SortOrder == ColumnSortOrder.Descending) { curOrder = ColumnSortOrder.Ascending; };
                foreach (GridColumn loopGrouping in dgContReport.GroupedColumns)
                {
                    curGrouping = loopGrouping;
                    tempItem = GetGroupByName(dgContReport, info.Column.Name.Replace("col", ""));
                    if (tempItem != null)
                    {
                        dgContReport.GroupSummarySortInfo.Add(tempItem, curOrder, curGrouping);
                    }
                }
                dgContReport.ClearSorting();

                info.Column.SortOrder = curOrder;
            }
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

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            ExpandGroups();
        }

        private void ExpandGroups()
        {
            dgContReport.CollapseAllGroups();

            ExpandCounter++;

            if (ExpandCounter > dgContReport.GroupCount) { ExpandCounter = 0; };

            for (int i = -1; ; i--)
            {
                if (!dgContReport.IsValidRowHandle(i)) return;
                if (dgContReport.GetRowLevel(i) < ExpandCounter)
                {
                    dgContReport.SetRowExpanded(i, true);
                }
            }
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

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            dgContReport.CollapseAllGroups();
            ExpandCounter = 0;
        }

        private void dgContReport_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Clicks > 1) return;
            GridView view = sender as GridView;
            if (view.State != GridState.ColumnDown) return;

            //info.Column.SortOrder

            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);
            if (info.HitTest == GridHitTest.Column)
            {
                curOrder = ColumnSortOrder.Descending;
                if (info.Column.SortOrder == ColumnSortOrder.Descending) { curOrder = ColumnSortOrder.Ascending; };
                foreach (GridColumn loopGrouping in dgContReport.GroupedColumns)
                {
                    curGrouping = loopGrouping;
                    tempItem = GetGroupByName(dgContReport, info.Column.Name.Replace("col", ""));
                    if (tempItem != null)
                    {
                        dgContReport.GroupSummarySortInfo.Add(tempItem, curOrder, curGrouping);
                    }
                }
                dgContReport.ClearSorting();

                info.Column.SortOrder = curOrder;
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgContReport.SelectAll();
            dgContReport.CopyToClipboard();
        }

        private void lblCopy2_Click(object sender, EventArgs e)
        {
            dgRedemptions.SelectAll();
            dgRedemptions.CopyToClipboard();
        }

        private void cmdExpand2_Click(object sender, EventArgs e)
        {
            ExpandGroups2();
        }

        private void cmdCollapse2_Click(object sender, EventArgs e)
        {
            dgRedemptions.CollapseAllGroups();
            ExpandCounter2 = 0;
        }

        private void dgContReport_DoubleClick(object sender, EventArgs e)
        {
            if (dgContReport.RowCount > 0 && dgContReport.FocusedRowHandle >= 0)
            {
                GridView Get_Column = sender as GridView;
                string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

                //string strTicker = (string)dgCBLCData.GetRowCellValue(dgCBLCData.FocusedRowHandle, "Cod_Neg");

                int Id_Contact = 0;
                if (!DBNull.Value.Equals(dgContReport.GetRowCellValue(dgContReport.FocusedRowHandle, "Id_Contact")))
                {
                    Id_Contact = Convert.ToInt32(dgContReport.GetRowCellValue(dgContReport.FocusedRowHandle, "Id_Contact"));
                }
                
                int Id_Distributor = 0;
                if (!DBNull.Value.Equals(dgContReport.GetRowCellValue(dgContReport.FocusedRowHandle, "Id_Distributor")))
                {
                    Id_Distributor = Convert.ToInt32(dgContReport.GetRowCellValue(dgContReport.FocusedRowHandle, "Id_Distributor"));
                }

                int Id_Portfolio = 0;
                if (!DBNull.Value.Equals(dgContReport.GetRowCellValue(dgContReport.FocusedRowHandle, "Id_Portfolio")))
                {
                    Id_Portfolio = Convert.ToInt32(dgContReport.GetRowCellValue(dgContReport.FocusedRowHandle, "Id_Portfolio"));
                }
                
                if (Column_Name == "Distributor Name")
                {
                    frmEditContDistrib Edit_Contact = new frmEditContDistrib();
                    //Edit_Contact.Text = "Insert New Loan";
                    Edit_Contact.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                    Edit_Contact.Left = this.Left + 10;

                    Edit_Contact.txtRelID.Text = Convert.ToString(Id_Contact);

                    Edit_Contact.LoadContInfo();

                    Edit_Contact.ShowDialog();
                }

                if (Column_Name == "Rebate Manag")
                {
                    frmEditDistribRebate Edit_DistRebates = new frmEditDistribRebate();

                    Edit_DistRebates.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                    Edit_DistRebates.Left = this.Left + 10;

                    Edit_DistRebates.txtId_Distributor.Text = Convert.ToString(Id_Distributor);
                    Edit_DistRebates.txtId_Portfolio.Text = Convert.ToString(Id_Portfolio);

                    Edit_DistRebates.LoadDistRebates();

                    Edit_DistRebates.ShowDialog();
                }

            }
        }

    }
}