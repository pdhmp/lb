using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;
using SGN.Validacao;
using SGN.Business;
using System.Collections;
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

namespace SGN
{
    public partial class frmDiferences : LBForm
    {
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();

        public frmDiferences()
        {
            InitializeComponent();
        }

        private void frmDividends_Load(object sender, EventArgs e)
        {
            
            dtgDiferences.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgDiferences.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgDiferences.LookAndFeel.SetSkinStyle("Blue");
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
        
        private void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            dgDiferences.Columns.Clear();

            dtpDate.Value.ToString("yyyyMMdd");

            SQLString = "SELECT * FROM NESTDB.dbo.Fcn_Check_Difference_PL(" + cmbFund.SelectedValue.ToString() + ",'" + dtpDate.Value.ToString("yyyyMMdd") + "')";

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgDiferences.DataSource = tablep;
            
            tablep.Dispose();

            dgDiferences.Columns["DifBps"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferences.Columns["DifBps"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgDiferences.Columns["DifPosition"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgDiferences.Columns["DifPosition"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

           
            Valida.SetColumnStyle(dgDiferences, 1);
            dgDiferences.GroupSummary.Add(SummaryItemType.Sum, "DifBps", dgDiferences.Columns["DifBps"]);
            ((GridSummaryItem)dgDiferences.GroupSummary[dgDiferences.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgDiferences.GroupSummary.Add(SummaryItemType.Sum, "DifPosition", dgDiferences.Columns["DifPosition"]);
            ((GridSummaryItem)dgDiferences.GroupSummary[dgDiferences.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

        }

        private void dgExpenses_DoubleClick(object sender, EventArgs e)
        {
            int resposta;
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Caption.ToString();

            if (Column_Name == "Delete")
            {
                if (dgDiferences.FocusedRowHandle != null)
                {
                    string curPos = dgDiferences.GetRowCellValue(dgDiferences.FocusedRowHandle, "Expense_Id").ToString();

                    resposta = Convert.ToInt32(MessageBox.Show("Do you really want to delete this Expense entry?", "Delete Expense", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

                    if (resposta == 6)
                    {
                        string SQLString;
                        SQLString = "DELETE FROM Tb700_Transactions WHERE Transaction_Id=" + curPos;
                        CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
                        Carrega_Grid();
                    }
                }
            }

        }

        bool show = false;

        private void ShowColumnSelector(bool showForm, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            if (show)
            {
                if (showForm) Nome_Grid.ColumnsCustomization();
            }
            else
            {
                if (showForm) Nome_Grid.DestroyCustomization();
            }
        }
        private void cmdDiferences_Click(object sender, EventArgs e)
        {
            cmdDiferences.Enabled = false;
            Carrega_Grid();
            cmdDiferences.Enabled = true;
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgDiferences.SelectAll();
            dgDiferences.CopyToClipboard();
        }

        private void dgDiferences_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            info.GroupText = /* view.GroupedColumns[level].Caption + ": " +*/ view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
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

        }

 
      
}