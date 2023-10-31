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
    public partial class frmContacts : LBForm
    {
        GridSummaryItem tempItem;
        ColumnSortOrder curOrder;
        GridColumn curGrouping;

        LB_HTML HTMLEngine = new LB_HTML();
        int ExpandCounter = 0;

        public frmContacts()
        {
            InitializeComponent();
        }

        private void frmContacts_Load(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            SQLString = "SELECT * FROM NESTDB.dbo.FCN_COM_Contact_Summary()";

            dgContacts.Columns.Clear();

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgContacts.DataSource = tablep;
            
            tablep.Dispose();

            dgContacts.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContacts.Columns["Quantity"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgContacts.Columns["FinAmount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgContacts.Columns["FinAmount"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            Valida.SetColumnStyle(dgContacts, 1);

            dgContacts.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgContacts.Columns["Quantity"], "{0:#,#0}");
            dgContacts.GroupSummary.Add(SummaryItemType.Sum, "FinAmount", dgContacts.Columns["FinAmount"], "{0:#,#0}");

        }

        private void Update_Contact(int Id_Contact)
        {

            webReport.Navigate("about:blank");

            string tempHTML = "";
            tempHTML = tempHTML + "<html><body>";

            string ContactName = CargaDados.curConn.Execute_Query_String("Select Contact_Name FROM NESTDB.dbo.Tb751_Contacts WHERE Id_Contact=" + Id_Contact.ToString());
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=14pt; font-weight:bold'>" + ContactName + "</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";

            tempHTML = tempHTML + HTMLEngine.Contact_Sub_Summary(Id_Contact);

            tempHTML = tempHTML + HTMLEngine.Contact_Red_Summary(Id_Contact, false);

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>Updated at: " + DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "</div>";
            tempHTML = tempHTML + "</body></html>";

            webReport.Document.Write(tempHTML);
            webReport.Refresh();

        }


        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgContacts_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid);
        }

        private void dgContacts_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid);
        }

        private void dgContacts_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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

        private void dgContacts_MouseUp(object sender, MouseEventArgs e)
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
                foreach (GridColumn loopGrouping in dgContacts.GroupedColumns)
                {
                    curGrouping = loopGrouping;
                    tempItem = GetGroupByName(dgContacts, info.Column.Name.Replace("col", ""));
                    if (tempItem != null)
                    {
                        dgContacts.GroupSummarySortInfo.Add(tempItem, curOrder, curGrouping);
                    }
                }
                dgContacts.ClearSorting();

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
            dgContacts.CollapseAllGroups();

            ExpandCounter++;

            if (ExpandCounter > dgContacts.GroupCount) { ExpandCounter = 0; };

            for (int i = -1; ; i--)
            {
                if (!dgContacts.IsValidRowHandle(i)) return;
                if (dgContacts.GetRowLevel(i) < ExpandCounter)
                {
                    dgContacts.SetRowExpanded(i, true);
                }
            }
        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            dgContacts.CollapseAllGroups();
            ExpandCounter = 0;
        }

        private void dgContacts_Click(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

            if (dgContacts.FocusedRowHandle != null)
            {
                Update_Contact(Convert.ToInt32(dgContacts.GetRowCellValue(dgContacts.FocusedRowHandle, "Id_Contact")));
            }
        }

        private void dgContacts_DoubleClick(object sender, EventArgs e)
        {
            if (dgContacts.RowCount > 0 && dgContacts.FocusedRowHandle >= 0)
            {
                GridView Get_Column = sender as GridView;
                string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

                int Id_Contact = 0;
                if (!DBNull.Value.Equals(dgContacts.GetRowCellValue(dgContacts.FocusedRowHandle, "Id_Contact")))
                {
                    Id_Contact = Convert.ToInt32(dgContacts.GetRowCellValue(dgContacts.FocusedRowHandle, "Id_Contact"));
                }

                int Id_Distributor = 0;
                if (!DBNull.Value.Equals(dgContacts.GetRowCellValue(dgContacts.FocusedRowHandle, "Id_Distributor")))
                {
                    Id_Distributor = Convert.ToInt32(dgContacts.GetRowCellValue(dgContacts.FocusedRowHandle, "Id_Distributor"));
                }

                if (Column_Name == "Distributor Name" || Column_Name == "Contact Name")
                {
                    frmEditContDistrib Edit_Contact = new frmEditContDistrib();
                    Edit_Contact.Top = this.Top + Convert.ToInt32(this.Height / 2 - 100);
                    Edit_Contact.Left = this.Left + 10;

                    Edit_Contact.txtRelID.Text = Convert.ToString(Id_Contact);

                    Edit_Contact.LoadContInfo();

                    Edit_Contact.ShowDialog();

                    int tempSelected = dgContacts.FocusedRowHandle;

                    Carrega_Grid();

                    dgContacts.FocusedRowHandle = tempSelected;
                }
            }
        }
    }
}