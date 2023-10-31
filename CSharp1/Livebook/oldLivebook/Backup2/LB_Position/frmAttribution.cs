using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;

using NestDLL;
using SGN.Business;
using SGN.Validacao;

using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;


using ZedGraph;

namespace SGN
{
    public partial class frmAttribution : LBForm
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        int ExpandCounter = 0;
        int flagCumulative = 1;
        DateTime iniDate = DateTime.Now.Subtract(TimeSpan.FromDays(DateTime.Now.Day - 1));
        DateTime endDate = DateTime.Now;
        
        public frmAttribution()
        {
            InitializeComponent();
        }

        private void frmAttribution_Load(object sender, EventArgs e)
        {
            
            radCumulative.CheckedChanged -= new System.EventHandler(radCumulative_CheckedChanged);

            radCumulative.Checked = true;
            radCumulative.CheckedChanged += new System.EventHandler(radCumulative_CheckedChanged);
            
            dtpHistDate.Value = iniDate;

            dtgAttribution.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgAttribution.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgAttribution.LookAndFeel.SetSkinStyle("Blue");

            dtgErrors.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgErrors.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgErrors.LookAndFeel.SetSkinStyle("Blue");

        }
 
        public void Carrega_Grid()
        {
            string SQLString="";
            string SQLFields = "";

            this.Cursor = Cursors.WaitCursor;
            
            int Id_Portfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());
            int viewBooks = -1;
            if (chkBooks.Checked) { viewBooks = 1; };
            
            string Portfolio_Name = CargaDados.curConn.Execute_Query_String("Select Port_Name from  dbo.Tb002_Portfolios where Id_Portfolio=" + cmbPortfolio.SelectedValue.ToString());

            string SQLDates = "SELECT [Date Now] FROM [dbo].[FCN_Attribution](" + Id_Portfolio + ", " + viewBooks + ", '" + iniDate.ToString("yyyyMMdd") + "', '" + endDate.ToString("yyyyMMdd") + "', " + flagCumulative + ") GROUP BY [Date Now] ORDER BY [Date Now]";

            dgAttribution.GroupSummary.Clear();
            dgAttribution.Columns.Clear();
            dtgAttribution.DataSource = null;

            DataTable FieldTable = CargaDados.curConn.Return_DataTable(SQLDates);
            foreach (DataRow row in FieldTable.Rows)
            {
                SQLFields = SQLFields + ",COALESCE(SUM(CASE WHEN [Date Now]='" + Convert.ToDateTime(row[0]).ToString("yyyyMMdd") + "' THEN Perf ELSE 0 END), 0) AS '" + Convert.ToDateTime(row[0]).ToString("dd/MMM") + "'";
            }

            SQLString = "SELECT CASE WHEN MAX(Book)<>'Bench' THEN '" + Portfolio_Name + "' ELSE 'Bench' END AS Portfolio, MAX(PortType) AS PortType, MAX(SubStrategy) AS SubStrategy, MAX(Book) AS Book, MAX(Sector) AS Sector, COALESCE(MAX([Base Underlying]),'NA') AS [Base Underlying]" + SQLFields + " FROM [dbo].[FCN_Attribution](" + Id_Portfolio + ", " + viewBooks + ", '" + iniDate.ToString("yyyyMMdd") + "', '" + endDate.ToString("yyyyMMdd") + "', " + flagCumulative + ") GROUP BY PortType, Book, Sector, [Base Underlying]";

            DataTable AttribTable = CargaDados.curConn.Return_DataTable(SQLString);

            dtgAttribution.DataSource = AttribTable;

            int curCounter=0;

            foreach (GridColumn curColumn in dgAttribution.Columns)
            {
                if (curCounter > 4)
                {
                    curColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    curColumn.DisplayFormat.FormatString = "+0.00%;-0.00%;-";
                    curColumn.Width = 60;
                }
                curCounter++;
            }

            dgAttribution.Columns[0].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda
            dgAttribution.Columns[1].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda
            dgAttribution.Columns[2].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda
            dgAttribution.Columns[3].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda
            dgAttribution.Columns[4].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda

            dgAttribution.Columns[4].Width = 150;

            curCounter=0;

            foreach (GridColumn curColumn in dgAttribution.Columns)
            {
                if (curCounter > 5)
                {
                    dgAttribution.GroupSummary.Add(SummaryItemType.Custom, curColumn.Name.Replace("col", ""), curColumn);
                    ((GridSummaryItem)dgAttribution.GroupSummary[dgAttribution.GroupSummary.Count - 1]).DisplayFormat = "{0:0.00%}";
                }
                curCounter++;
            }
            
            if (viewBooks == -1)
            {
                dgAttribution.Columns["Portfolio"].GroupIndex = 0;
                dgAttribution.Columns["PortType"].GroupIndex = 1;
                dgAttribution.Columns["Book"].GroupIndex = 2;
                dgAttribution.Columns["Sector"].GroupIndex = 3;
            }
            else 
            {
                dgAttribution.Columns["Portfolio"].GroupIndex = 0;
                dgAttribution.Columns["Book"].GroupIndex = 1;
                dgAttribution.Columns["PortType"].GroupIndex = 2;
                dgAttribution.Columns["Sector"].GroupIndex = 3;
            }
            dgAttribution.LeftCoord = 500;

            ExpandCounter = 0;
            ExpandGroups();

            CreateGraph(zgcPerformance);

            SQLString = " SELECT TOP 5 [Date Now] AS Date, SUM(Perf) AS Error FROM [dbo].[FCN_Attribution](" + Id_Portfolio + ", " + viewBooks + ", '" + iniDate.Add(new TimeSpan(-1, 0, 0, 0)).ToString("yyyyMMdd") + "', '" + endDate.ToString("yyyyMMdd") + "', 0) " +
                        " WHERE PortType='0-Error' GROUP BY [Date Now] ORDER BY ABS(SUM(Perf)) DESC";
            
            DataTable AtribErrorTable = CargaDados.curConn.Return_DataTable(SQLString);
            dtgErrors.DataSource = AtribErrorTable;

            dgErrors.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgErrors.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";
            dgErrors.Columns[0].Width = 120;
            dgErrors.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;


            dgErrors.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgErrors.Columns[1].DisplayFormat.FormatString = "+0.00%;-0.00%;-";
            dgErrors.Columns[1].Width = 80;
            dgErrors.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            this.Cursor = Cursors.Default;

        }


        private void dgAttribution_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
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
            info.GroupText = view.GroupedColumns[level].Caption + "" + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
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

        private void dgAttribution_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left || e.Clicks > 1) return;
            GridView view = sender as GridView;
            if (view.State != GridState.ColumnDown) return;

            //info.Column.SortOrder

            Point p = view.GridControl.PointToClient(MousePosition);
            GridHitInfo info = view.CalcHitInfo(p);
            if (info.HitTest == GridHitTest.Column)
            {
                ColumnSortOrder curOrder = ColumnSortOrder.Descending;
                if (info.Column.SortOrder == ColumnSortOrder.Descending) { curOrder = ColumnSortOrder.Ascending; };
                foreach (GridColumn curGrouping in dgAttribution.GroupedColumns)
                {
                    if(curGrouping.Name != "colPortfolio")
                    dgAttribution.GroupSummarySortInfo.Add(GetGroupByName(dgAttribution, info.Column.Name.Replace("col", "")), curOrder, curGrouping);
                }
                dgAttribution.ClearSorting();
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

        private void dgAttribution_GroupLevelStyle(object sender, GroupLevelStyleEventArgs e)
        {
            switch (e.Level)
            {
                case 0: 
                    e.LevelAppearance.BackColor = Color.LightGray;
                    e.LevelAppearance.Font = new Font(dgAttribution.Appearance.Row.Font, FontStyle.Bold); ;
                    break;
                case 1: e.LevelAppearance.BackColor = Color.FromArgb(219, 220, 250); break;
                case 2: e.LevelAppearance.BackColor = Color.FromArgb(230, 230, 250); break;
            }
        }

        private void cmdExpand_Click(object sender, EventArgs e)
        {
            ExpandGroups();
        }

        private void ExpandGroups()
        {
            dgAttribution.CollapseAllGroups();

            ExpandCounter++;

            if (ExpandCounter > dgAttribution.GroupCount) { ExpandCounter = 0; };

            for (int i = -1; ; i--)
            {
                if (!dgAttribution.IsValidRowHandle(i)) return;
                if (dgAttribution.GetRowLevel(i) < ExpandCounter)
                {
                    dgAttribution.SetRowExpanded(i, true);
                }
            }
        }

        private void cmdExpand_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgAttribution.ExpandAllGroups();
                ExpandCounter = dgAttribution.GroupCount;
            }

        }

        private void cmdCollapse_Click(object sender, EventArgs e)
        {
            dgAttribution.CollapseAllGroups();
            ExpandCounter = 0;
        }

        private void cmdCollapse_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgAttribution.CollapseAllGroups();
                ExpandCounter = 0;
            }
        }

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            int curId_Portfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());
            Carrega_Grid();
        }


        private void dgAttribution_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {

            if (e.Column.Name != "colSubStrategy" && e.Column.Name != "colBaseUnderlying" && e.Column.Name != "colSector" && e.Column.Name != "colPortType" && e.Column.Name != "colBook")
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

        private void radCumulative_CheckedChanged(object sender, EventArgs e)
        {
            if (radCumulative.Checked == true)
            {
                flagCumulative = 1;
                Carrega_Grid();
            }

        }

        private void radDaily_CheckedChanged(object sender, EventArgs e)
        {
            if (radDaily.Checked == true)
            {
                flagCumulative = 0;
                Carrega_Grid();
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgAttribution.SelectAll();
            dgAttribution.CopyToClipboard();
        }

        private void CreateGraph(ZedGraphControl zgc)
        {
            this.Cursor = Cursors.WaitCursor;

            int Id_Portfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());
            int viewBooks = -1;
            if (chkBooks.Checked) { viewBooks = 1; };

            zgc.GraphPane.CurveList.Clear();
            zgc.GraphPane.Border.IsVisible = false;

            // get a reference to the GraphPane
            GraphPane myPane = zgc.GraphPane;

            // Set the Titles
            myPane.Title.IsVisible = false;
            myPane.XAxis.Title.IsVisible = false;
            myPane.YAxis.Title.IsVisible = false;

            // Create Data Arrays

            List <PointPairList> DataPoints = new List <PointPairList>();
            DateTime MaxDate = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime MinDate = DateTime.Now;

            int LineCounter = 0;

            for (int i = -1; i > -100; i--)
            {
                if (dgAttribution.GetRowLevel(i) == 1)
                {
                    int dataRowHandle = dgAttribution.GetDataRowHandleByGroupRowHandle(i);
                    Hashtable values = dgAttribution.GetGroupSummaryValues(i);
                    
                    if (values != null)
                    {
                        PointPairList curPoints = new PointPairList();
                        foreach (DictionaryEntry curSumItem in values)
                        {
                            GridGroupSummaryItem tempItem = (GridGroupSummaryItem)curSumItem.Key;
                            DateTime TempDate = Convert.ToDateTime(tempItem.FieldName + "/2010");
                            if (TempDate > MaxDate) { MaxDate = TempDate; };
                            if (TempDate < MinDate) { MinDate = TempDate; };
                            double tempValue = Convert.ToDouble(curSumItem.Value);
                            curPoints.Add(TempDate.ToOADate(), tempValue);
                        }
                        curPoints.Add(MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate(), 0);
                        curPoints.Sort();
                        DataPoints.Add(curPoints);

                        string ItemName = dgAttribution.GetRowCellDisplayText(dataRowHandle, dgAttribution.GroupedColumns[1]).ToString();
                        if(!ItemName.Contains("Bench"))
                        {
                            LineItem curLine = myPane.AddCurve(ItemName, curPoints, GetLineColor(LineCounter++), SymbolType.None);
                            curLine.Line.Width = 2.0F;
                        }
                        else
                        {
                            LineItem curLine = myPane.AddCurve(ItemName, curPoints, Color.Gray, SymbolType.None);
                            curLine.Line.Fill = new Fill(Color.FromArgb(10, 0, 0, 0), Color.FromArgb(100, 200, 200, 200), 90F);
                            curLine.Line.Width = 0.5F;
                        }
                    }

                }
            }

            myPane.Legend.Position = ZedGraph.LegendPos.Bottom;

            myPane.XAxis.Type = AxisType.Date;
            myPane.YAxis.Scale.Format = "0.00%";

            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.Gray;
            myPane.YAxis.MajorGrid.Color = Color.Gray;

            myPane.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate();//.Add(new TimeSpan(-1, 0, 0)).ToOADate();
            myPane.XAxis.Scale.Max = MaxDate.Add(new TimeSpan(1, 0, 0, 0)).ToOADate();

            myPane.IsFontsScaled = false;
            myPane.XAxis.Scale.FontSpec.Size = 10;
            myPane.YAxis.Scale.FontSpec.Size = 10;
            myPane.Legend.FontSpec.Size = 12;

            myPane.XAxis.Scale.MinorUnit = DateUnit.Day;
            myPane.XAxis.Scale.MajorUnit = DateUnit.Day;

            zgc.IsShowPointValues = true;

            this.Cursor = Cursors.Default;

            // Tell ZedGraph to refigure the
            // axes since the data have changed
            zgc.AxisChange();
            zgc.Refresh();

        }

        private Color GetLineColor(int LineIndex)
        {
            switch (LineIndex)
            {
                case 0: return Color.Black;
                case 1: return Color.Red;
                case 2: return Color.Green;
                case 3: return Color.Blue;
                case 4: return Color.Lime ;
                case 5: return Color.Magenta;
                case 6: return Color.Brown;
                default: return Color.Orange;
            }
        }

        private void chkBooks_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dtpHistDate_CloseUp(object sender, EventArgs e)
        {
            iniDate = dtpHistDate.Value;
            DateTime futDate = dtpHistDate.Value.AddMonths(1);
            endDate = futDate.Subtract(TimeSpan.FromDays(dtpHistDate.Value.Day));
            if (endDate > DateTime.Now) { endDate = DateTime.Now; };
            if (endDate <= iniDate) { endDate = iniDate.Add(new TimeSpan(1, 0, 0, 0)); };
            Carrega_Grid();
        }

        private void dgAttribution_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            GridView view = sender as GridView;
            switch (e.SummaryProcess)
            {
                case CustomSummaryProcess.Start:
                    {
                        e.TotalValue = new TotalCalculator();
                        break;
                    }
                case CustomSummaryProcess.Calculate:
                    {
                        double curValue = Convert.ToDouble(e.FieldValue);
                        ((TotalCalculator)e.TotalValue).Add(curValue);
                        break;
                    }
                case CustomSummaryProcess.Finalize:
                    {
                        string curRowString = view.GetRowCellValue(e.RowHandle, "Sector").ToString();

                        if (chkBooks.Checked && e.GroupLevel == 0 && !(curRowString == "Bench"))
                        {
                            e.TotalValue = 0; 
                        }
                        else 
                        {
                            e.TotalValue = ((TotalCalculator)e.TotalValue).Value;
                        }
                        break;
                    }
            }
        }

        private sealed class TotalCalculator
        {
            private double _totalValue;

            public void Add(double curValue)
            {
                _totalValue += curValue;
            }

            public double Value
            {
                get { return _totalValue; }
            }
        }

        private void dgAttribution_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Point pt = dtgAttribution.PointToClient(MousePosition);
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = dgAttribution.CalcHitInfo(pt);
            if (hi.InGroupPanel)
            {
                CreateGraph(zgcPerformance);
            }
        }

    }
}