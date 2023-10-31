using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;


using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace LiveBook
{
    public partial class frmTradesPosition : LBForm
    {
        public frmTradesPosition()
        {
            InitializeComponent();
        }

        private void frmTradesPosition_Load(object sender, EventArgs e)
        {
            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            radToday.Checked = true;

        }

        public void Carrega_Grid()
        {
            string SQLString;

            DateTime DateToGet = new DateTime();
            int Checkrad=0;
            string Temp_Table_Name;

            if (radToday.Checked == true) { DateToGet = DateTime.Now; Checkrad=0;};
            if (radWeek.Checked == true) { DateToGet = DateTime.Now.AddDays((int)DateTime.Now.DayOfWeek - 9); Checkrad=1; };
            if (radMonth.Checked == true) { DateToGet = DateTime.Now.AddDays(1 - (int)DateTime.Now.Day); Checkrad=1; };
            if (rad6m.Checked == true) { DateToGet = DateTime.Now.AddMonths(-6).AddDays(1 - (int)DateTime.Now.Day); Checkrad = 1; };


            Temp_Table_Name = "";

            if (lblTable.Text == "NESTRT.dbo.FCN_Posicao_Atual()")
            { Temp_Table_Name = lblTable.Text; }
            else
            { Temp_Table_Name = lblTable.Text + " (nolock)"; }



            if (lblIdPosition.Text != "")
            {
                using (newNestConn curConn = new newNestConn())
                {
                    SQLString = "Select [Id Portfolio],[Portfolio],[Id Ticker],[Ticker],[Id Book],Book,[Id Section]," +
                                " [Section],[Date now] from " + Temp_Table_Name +
                                " Where [Id Position] = " + Convert.ToInt32(lblIdPosition.Text);

                    DataTable curTable = curConn.Return_DataTable(SQLString);
                    foreach (DataRow curRow in curTable.Rows)
                    {
                        if (chkAllBooks.Checked == true)
                        {
                            lblPortfolio.Text = curRow["Portfolio"].ToString() + " - " + curRow["Ticker"].ToString() + " (All Books)";

                            string Id_Portfolio = curRow["Id Portfolio"].ToString();
                            if (Id_Portfolio == "4") { Id_Portfolio = "5,6"; }

                            SQLString = " Select [Data_Trade] as [Trade Date],convert(varchar,Data_Insercao_Trade,108) as [Trade Time],  [Quantidade_Trade] as Quantity, " +
                                        " [Preco_Trade] as Price,[Valor_Financeiro_Trade] as [Money Amount] " +
                                        " from   dbo.VW_TRADES_ALL " +
                                        " Where [Id_Ativo]=" + curRow["Id Ticker"] + " and [Id_Portfolio] IN (" + Id_Portfolio + ")" +
                                        " and [Data_Trade]>='" + DateToGet.ToString("yyyy-MM-dd") + "' and [Data_Trade]<=convert(varchar,getdate(),112)" +
                                        " and [Status_Trade]<>4 ";


                        }
                        else
                        {
                            lblPortfolio.Text = curRow["Portfolio"].ToString() + " - " + curRow["Ticker"].ToString() + " (Book: " + curRow["Book"].ToString() + " - " + curRow["Section"].ToString() + ")";

                            string Id_Portfolio = curRow["Id Portfolio"].ToString();
                            if (Id_Portfolio == "4") { Id_Portfolio = "5,6"; }

                            SQLString = " Select [Data_Trade] as [Trade Date],convert(varchar,Data_Insercao_Trade,108) as [Trade Time],  [Quantidade_Trade] as Quantity, " +
                                       " [Preco_Trade] as Price,[Valor_Financeiro_Trade] as [Money Amount] " +
                                       " from   dbo.VW_TRADES_ALL " +
                                        " Where [Id_Ativo]=" + curRow["Id Ticker"] + " and [Id_Portfolio] IN (" + Id_Portfolio + ")" +
                                        " and [Data_Trade]>='" + DateToGet.ToString("yyyy-MM-dd") + "' and [Data_Trade]<=convert(varchar,getdate(),112)" +
                                        " and [Id Book] = " + curRow["Id Book"] + " and [Id Section]=" + curRow["Id Section"] +
                                        " and [Status_Trade]<>4 ";

                        }

                        DataTable tablep = curConn.Return_DataTable(SQLString);

                        dtg.DataSource = tablep;


                        //ALTERAR
                        dgTradesPos.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        dgTradesPos.Columns["Quantity"].DisplayFormat.FormatString = "{0:#,#0.00}";

                        dgTradesPos.Columns["Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        dgTradesPos.Columns["Price"].DisplayFormat.FormatString = "{0:#,#0.00}";

                        dgTradesPos.Columns["Money Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        dgTradesPos.Columns["Money Amount"].DisplayFormat.FormatString = "{0:#,#0.00}";

                        dgTradesPos.Columns["Trade Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                        dgTradesPos.Columns["Trade Date"].DisplayFormat.FormatString = "{0:dd/MMM}";

                        dgTradesPos.Columns["Trade Time"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                        dgTradesPos.Columns["Trade Date"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

                        dgTradesPos.Columns["Trade Time"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;

                        if (Checkrad == 1)
                        {
                            dgTradesPos.Columns["Trade Date"].Group();
                            //dgTradesPos.Columns["Trade_Date"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                            dgTradesPos.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgTradesPos.Columns["Quantity"]);
                            ((GridSummaryItem)dgTradesPos.GroupSummary[dgTradesPos.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
                            dgTradesPos.GroupSummary.Add(SummaryItemType.Sum, "Money Amount", dgTradesPos.Columns["Money Amount"]);
                            ((GridSummaryItem)dgTradesPos.GroupSummary[dgTradesPos.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
                            if (radMonth.Checked == true || rad6m.Checked == true)
                            { dgTradesPos.CollapseAllGroups(); }
                            else
                            { dgTradesPos.ExpandAllGroups(); };
                        }
                        else
                        {
                            dgTradesPos.ClearGrouping();
                        }

                    }
                }
            }
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

#region DrawTotals

        private void dgTradesPos_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            //dgPositions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;

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

#endregion DrawTotals

        private void chkAllBooks_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }


    }
}