using System;
using System.Collections;
using System.Collections.Generic;
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
using ZedGraph;

namespace LiveBook
{
    public partial class frmAttribution : LBForm
    {
        int ExpandCounter = 0;
        int flagCumulative = 1;
        int flagMonth = 1;
        DateTime iniDate = DateTime.Now.Subtract(TimeSpan.FromDays(DateTime.Now.Day - 1));
        DateTime endDate = DateTime.Now;

        public frmAttribution()
        {
            InitializeComponent();
        }

        private void frmAttribution_Load(object sender, EventArgs e)
        {
            //lblCopy.Parent = (Control)dtgAttribution;

            radCumulative.CheckedChanged -= new System.EventHandler(radCumulative_CheckedChanged);
            radCumulative.Checked = true;
            radCumulative.CheckedChanged += new System.EventHandler(radCumulative_CheckedChanged);

            radNormal.CheckedChanged -= new System.EventHandler(radNormal_CheckedChanged);
            radNormal.Checked = true;
            flagCumulative = 0;
            radNormal.CheckedChanged += new System.EventHandler(radNormal_CheckedChanged);

            radEoMonth.CheckedChanged -= new System.EventHandler(radMonth_CheckedChanged);
            radEoMonth.Checked = true;
            radEoMonth.CheckedChanged += new System.EventHandler(radMonth_CheckedChanged);


            radDaily.CheckedChanged -= new System.EventHandler(radDaily_CheckedChanged);
            radDaily.Checked = true;
            radDaily.CheckedChanged += new System.EventHandler(radDaily_CheckedChanged);

            dtpHistDate.Value = iniDate;

            dtgAttribution.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgAttribution.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgAttribution.LookAndFeel.SetSkinStyle("Blue");

            dtgErrors.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgErrors.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgErrors.LookAndFeel.SetSkinStyle("Blue");

            Carrega_Grid();
        }

        public void Carrega_Grid()
        {
            int runType = 1;
            using (newNestConn curConn = new newNestConn())
            {
                string SQLString = "";
                string SQLFields = "";

                dgAttribution.GroupSummary.Clear();
                dgAttribution.Columns.Clear();
                dtgAttribution.DataSource = null;
                dtgAttribution.Refresh();

                this.Cursor = Cursors.WaitCursor;

                int Id_Portfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());
                int viewBooks = -1;
                if (chkBooks.Checked) { viewBooks = 1; };

                List<DateTime> ErrorDates = new List<DateTime>();
                string aux = "SELECT [Date Now] FROM Tb000_Historical_Positions WHERE [Date Now] >= '" + iniDate.ToString("yyyy-MM-dd") + "' AND [Date Now] <= '" + endDate.ToString("yyyy-MM-dd") + "' AND (NAVPrevious = 0 OR NAVPrevious = NULL) AND [Id Portfolio] = " + cmbPortfolio.SelectedValue.ToString() + " GROUP BY [Date Now]";
                DataTable dt = curConn.Return_DataTable(aux);

                if (dt.Rows.Count > 0)
                {
                    aux = "";
                    foreach (DataRow curRow in dt.Rows)
                    {
                        aux += Convert.ToDateTime(curRow[0]).ToString("dd-MM-yyyy") + "\r\n";
                    }

                    MessageBox.Show("Datas abaixo com possivel erro no NAV. Favor recalcular as posições e tentar novamente. \r\n" + aux, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                string Portfolio_Name = curConn.Execute_Query_String("Select Port_Name from  dbo.Tb002_Portfolios where Id_Portfolio=" + cmbPortfolio.SelectedValue.ToString());

                curConn.ExecuteNonQuery("CREATE TABLE #tempResultAtt ([Date Now] datetime, PortType varchar(40), Book varchar(40), Section varchar(40), SubStrategy varchar(40), Instrument varchar(40), Sector varchar(40), ReportSector varchar(40), [Base Underlying] varchar(40), Perf float, AdjPerf float)");
                if (runType == 1)
                {
                    curConn.ExecuteNonQuery("SET ARITHABORT ON; INSERT INTO #tempResultAtt EXEC [dbo].[Proc_Attribution] " + Id_Portfolio + ", " + viewBooks + ", '" + iniDate.ToString("yyyy-MM-dd") + "', '" + endDate.ToString("yyyy-MM-dd") + "', " + flagCumulative + "");
                    curConn.ExecuteNonQuery("DELETE FROM #tempResultAtt WHERE Perf=0 AND AdjPerf=0");
                }

                if (radMonthly.Checked)
                {
                    if (radCumulative.Checked)
                    {
                        curConn.ExecuteNonQuery("DELETE FROM #tempResultAtt WHERE [Date Now] NOT IN (SELECT MAX([Date Now]) FROM #tempResultAtt GROUP BY YEAR([Date Now]),MONTH([Date Now]))");
                    }
                    else
                    {
                        curConn.ExecuteNonQuery("CREATE TABLE #MonthlyTable ([Date Now] datetime, PortType varchar(40), Book varchar(40), Section varchar(40), SubStrategy varchar(40), Instrument varchar(40), Sector varchar(40), ReportSector varchar(40), [Base Underlying] varchar(40), Perf float, AdjPerf float)");
                        curConn.ExecuteNonQuery("INSERT INTO #MonthlyTable SELECT * FROM #tempResultAtt;");
                        curConn.ExecuteNonQuery("TRUNCATE TABLE #tempResultAtt");
                        curConn.ExecuteNonQuery("INSERT INTO #tempResultAtt " +
                                                " SELECT DATEADD(dd,-(DAY([Date Now])-1),[Date Now]), PortType, Book, Section, SubStrategy, Instrument, Sector, ReportSector, [Base Underlying], SUM(Perf), SUM(AdjPerf) " +
                                                " FROM #MonthlyTable " +
                                                " GROUP BY DATEADD(dd,-(DAY([Date Now])-1),[Date Now]), PortType, Book, Section, SubStrategy, Instrument, Sector, ReportSector, [Base Underlying]");

                        curConn.ExecuteNonQuery("DROP TABLE #MonthlyTable");
                    }
                }

                string SQLDates = "SELECT [Date Now] FROM #tempResultAtt GROUP BY [Date Now] ORDER BY [Date Now]";

                string getField = "Perf";
                if (chkAdjust.Checked) getField = "AdjPerf";

                DataTable FieldTable = curConn.Return_DataTable(SQLDates);
                foreach (DataRow row in FieldTable.Rows)
                {
                    SQLFields = SQLFields + ",COALESCE(SUM(CASE WHEN [Date Now]='" + Convert.ToDateTime(row[0]).ToString("yyyy-MM-dd") + "' THEN " + getField + " ELSE 0 END), 0) AS '" + Convert.ToDateTime(row[0]).ToString("dd/MMM/yy") + "'";
                }

                SQLString = "SELECT CASE WHEN MAX(Book)<>'Bench' THEN '" + Portfolio_Name + "' ELSE 'Bench' END AS Portfolio, MAX(PortType) AS PortType, MAX(SubStrategy) AS SubStrategy, MAX(Book) AS Book, MAX(Section) AS Section, MAX(Instrument) AS Instrument, MAX(Sector) AS Sector, MAX(ReportSector) AS ReportSector, COALESCE(MAX([Base Underlying]),'NA') AS [Base Underlying]" + SQLFields + " FROM #tempResultAtt GROUP BY PortType, Book, Section, Sector, [Base Underlying]";

                DataTable AttribTable = curConn.Return_DataTable(SQLString);

                dtgAttribution.DataSource = AttribTable;

                int curCounter = 0;

                foreach (GridColumn curColumn in dgAttribution.Columns)
                {
                    if (curCounter > 8)
                    {
                        curColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                        curColumn.DisplayFormat.FormatString = "+0.000%;-0.000%;-";
                        curColumn.Width = 60;
                    }
                    curCounter++;
                }

                dgAttribution.Columns["Portfolio"].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda
                dgAttribution.Columns["PortType"].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda
                dgAttribution.Columns["Book"].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda
                dgAttribution.Columns["Sector"].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda
                dgAttribution.Columns["SubStrategy"].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda
                dgAttribution.Columns["Base Underlying"].Fixed = FixedStyle.Left; //Fixa Colunas na Esquerda

                dgAttribution.Columns[4].Width = 150;

                curCounter = 0;

                foreach (GridColumn curColumn in dgAttribution.Columns)
                {
                    if (curCounter > 8)
                    {
                        if (radMonthly.Checked)
                        {
                            dgAttribution.Columns[curCounter].Caption = Convert.ToDateTime(dgAttribution.Columns[curCounter].Name.Replace("col", "")).ToString("MMM/yy");
                        }

                        dgAttribution.GroupSummary.Add(SummaryItemType.Custom, curColumn.Name.Replace("col", ""), curColumn);
                        ((GridSummaryItem)dgAttribution.GroupSummary[dgAttribution.GroupSummary.Count - 1]).DisplayFormat = "{0:0.00%}";

                    }
                    curCounter++;
                }

                if (viewBooks == -1)
                {
                    dgAttribution.Columns["Portfolio"].GroupIndex = 0;
                    dgAttribution.Columns["Book"].GroupIndex = 1;
                    dgAttribution.Columns["Section"].GroupIndex = 2;
                    //dgAttribution.Columns["Sector"].GroupIndex = 3;
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

                SQLString = " SELECT TOP 10 [Date Now] AS Date, SUM(Perf) AS Error FROM #tempResultAtt " +
                            " WHERE PortType='0-Error' GROUP BY [Date Now] ORDER BY ABS(SUM(Perf)) DESC";

                DataTable AtribErrorTable = curConn.Return_DataTable(SQLString);
                dtgErrors.DataSource = AtribErrorTable;

                curConn.ExecuteNonQuery("DROP TABLE #tempResultAtt");

                dgErrors.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                dgErrors.Columns[0].DisplayFormat.FormatString = "dd/MMM/yy";
                dgErrors.Columns[0].Width = 120;
                dgErrors.Columns[0].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;


                dgErrors.Columns[1].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgErrors.Columns[1].DisplayFormat.FormatString = "+0.00%;-0.00%;-";
                dgErrors.Columns[1].Width = 80;
                dgErrors.Columns[1].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                this.Cursor = Cursors.Default;

                dgAttribution.BestFitColumns();
            }
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

                if ((rect.X + rect.Width / 2) > view.Columns[4].Width)
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
                    if (curGrouping.Name != "colPortfolio")

                        if (GetGroupByName(dgAttribution, info.Column.Name.Replace("col", "")) != null)
                        {
                            dgAttribution.GroupSummarySortInfo.Add(GetGroupByName(dgAttribution, info.Column.Name.Replace("col", "")), curOrder, curGrouping);
                        }
                }
                dgAttribution.ClearSorting();
                info.Column.SortOrder = curOrder;
                CreateGraph(zgcPerformance);
            }

            if (info.HitTest == GridHitTest.GroupPanelColumn)
            {
                CreateGraph(zgcPerformance);
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
            if (e.Column.Name != "colSubStrategy" && e.Column.Name != "colBaseUnderlying" && e.Column.Name != "colSector" && e.Column.Name != "colReportSector" && e.Column.Name != "colPortType" && e.Column.Name != "colBook" && e.Column.Name != "colInstrument")
            {
                if (curUtils.IsNumeric(e.CellValue))
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

        }

        private void radCumulative_CheckedChanged(object sender, EventArgs e)
        {
            if (radCumulative.Checked == true)
            {
                flagCumulative = 1;
                Carrega_Grid();
            }

        }

        private void radNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (radNormal.Checked == true)
            {
                flagCumulative = 0;
                Carrega_Grid();
            }
        }

        private void radDaily_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void radMonthly_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
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

            List<PointPairList> DataPoints = new List<PointPairList>();
            DateTime MaxDate = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime MinDate = DateTime.Now;

            int LineCounter = 0;

            for (int i = -1; i > -500; i--)
            {
                if (dgAttribution.GetRowLevel(i) == 1)
                {
                    //Console.WriteLine(dgAttribution.GetRowLevel(i) + "  -  " + dgAttribution.GetRowCellDisplayText(i, dgAttribution.GroupedColumns[1])); 

                    int dataRowHandle = dgAttribution.GetDataRowHandleByGroupRowHandle(i);
                    Hashtable values = dgAttribution.GetGroupSummaryValues(i);

                    if (values != null)
                    {
                        PointPairList curPoints = new PointPairList();
                        foreach (DictionaryEntry curSumItem in values)
                        {
                            GridGroupSummaryItem tempItem = (GridGroupSummaryItem)curSumItem.Key;
                            DateTime TempDate = Convert.ToDateTime(tempItem.FieldName);
                            if (TempDate > MaxDate) { MaxDate = TempDate; };
                            if (TempDate < MinDate) { MinDate = TempDate; };
                            double tempValue = Convert.ToDouble(curSumItem.Value);
                            curPoints.Add(TempDate.ToOADate(), tempValue);
                        }

                        if (radDaily.Checked)
                        {
                            curPoints.Add(MinDate.AddDays(-1).ToOADate(), 0);
                        }
                        else
                        {
                            curPoints.Add(MinDate.AddMonths(-1).ToOADate(), 0);
                        }

                        curPoints.Sort();
                        DataPoints.Add(curPoints);

                        string ItemName = dgAttribution.GetRowCellDisplayText(dataRowHandle, dgAttribution.GroupedColumns[1]).ToString();
                        if (ItemName.Contains("Bench"))
                        {
                            LineItem curLine = myPane.AddCurve(ItemName, curPoints, Color.Gray, SymbolType.None);
                            curLine.Line.Fill = new Fill(Color.FromArgb(10, 0, 0, 0), Color.FromArgb(100, 200, 200, 200), 90F);
                            curLine.Line.Width = 0.5F;
                        }
                        else if (ItemName.Contains("Error"))
                        {
                            LineItem curLine = myPane.AddCurve(ItemName, curPoints, Color.Red, SymbolType.None);
                            curLine.Line.Width = 2.0F;
                        }
                        else
                        {
                            LineItem curLine = myPane.AddCurve(ItemName, curPoints, GetLineColor(LineCounter++), SymbolType.None);
                            curLine.Line.Width = 2.0F;
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


            if (radDaily.Checked)
            {
                myPane.XAxis.Scale.Min = MinDate.AddDays(-1).ToOADate();//.Add(new TimeSpan(-1, 0, 0)).ToOADate();
                myPane.XAxis.Scale.Max = MaxDate.AddDays(1).ToOADate();
            }
            else
            {
                myPane.XAxis.Scale.Min = MinDate.AddMonths(-1).ToOADate();//.Add(new TimeSpan(-1, 0, 0)).ToOADate();
                myPane.XAxis.Scale.Max = MaxDate.AddMonths(1).ToOADate();
            }


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
                case 1: return Color.Orange;
                case 2: return Color.Green;
                case 3: return Color.Blue;
                case 4: return Color.Lime;
                case 5: return Color.Magenta;
                case 6: return Color.Brown;
                case 7: return Color.LightBlue;
                case 8: return Color.LightSalmon;
                case 9: return Color.Bisque;
                case 10: return Color.MediumTurquoise;
                case 11: return Color.DarkGray;
                case 12: return Color.MediumPurple;
                default: return Color.Gray;
            }
        }

        private void chkBooks_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dtpHistDate_CloseUp(object sender, EventArgs e)
        {
            ChangeDate();
            /*
            if (endDate > DateTime.Now) { endDate = DateTime.Now; };
            if (endDate <= iniDate) { endDate = iniDate.Add(new TimeSpan(1, 0, 0, 0)); };
            endDate = DateTime.Now.Date;
            */

            Carrega_Grid();
        }

        void ChangeDate()
        {
            iniDate = dtpHistDate.Value;
            DateTime futDate = dtpHistDate.Value.AddMonths(1);

            if (flagMonth == 2)
            {
                endDate = new DateTime(futDate.Year, 12, 31);

            }
            else if (flagMonth == 1)
            {
                endDate = futDate.Subtract(TimeSpan.FromDays(dtpHistDate.Value.Day));

            }
            else
            {
                endDate = DateTime.Now.Date;
            }

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

        private void dgAttribution_ColumnFilterChanged(object sender, EventArgs e)
        {
            CreateGraph(zgcPerformance);
        }

        private void radMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (radEoMonth.Checked == true)
            {
                flagMonth = 1;
                ChangeDate();
                Carrega_Grid();
            }
        }

        private void radAll_CheckedChanged(object sender, EventArgs e)
        {
            if (radAll.Checked == true)
            {
                flagMonth = 0;
                ChangeDate();
                Carrega_Grid();
            }
        }

        private void radYear_CheckedChanged(object sender, EventArgs e)
        {
            if (radYear.Checked == true)
            {
                flagMonth = 2;
                ChangeDate();
                Carrega_Grid();
            }
        }

        private void chkAdjust_CheckedChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {

            int groupRowHandle = -1;
            string CopyString = "";
            string[] printTitles = new string[1];

            while (dgAttribution.IsValidRowHandle(groupRowHandle))
            {
                //if (dgAttribution.IsRowVisible(groupRowHandle) == RowVisibleState.Visible)
                {
                    int row = dgAttribution.GetDataRowHandleByGroupRowHandle(groupRowHandle);
                    int tempLevel = dgAttribution.GetRowLevel(groupRowHandle);

                    string tempCopy = new string(' ', tempLevel * 4);

                    tempCopy = tempCopy + dgAttribution.GetRowCellDisplayText(row, dgAttribution.GroupedColumns[tempLevel]);

                    GridView view = dgAttribution as GridView;
                    Hashtable values = view.GetGroupSummaryValues(groupRowHandle);

                    string[] printValues = new string[values.Keys.Count];
                    printTitles = new string[values.Keys.Count];

                    int minColumn = 9999;

                    foreach (GridGroupSummaryItem curKey in values.Keys)
                    {
                        if (curKey.ShowInGroupColumnFooter.VisibleIndex < minColumn) minColumn = curKey.ShowInGroupColumnFooter.VisibleIndex;
                    }

                    foreach (GridGroupSummaryItem curKey in values.Keys)
                    {
                        int poscolumn = curKey.ShowInGroupColumnFooter.VisibleIndex;
                        //KeyValuePair<GridGroupSummaryItem, double> curItem = values;
                        double curValue = (double)values[curKey];
                        printValues[poscolumn - minColumn] = curValue.ToString();
                        printTitles[poscolumn - minColumn] = curKey.ShowInGroupColumnFooter.ToString();
                    }

                    for (int i = 0; i < printValues.Length; i++)
                    {
                        tempCopy = tempCopy + '\t' + printValues[i];
                    }


                    Console.WriteLine(tempCopy);
                    CopyString = CopyString + tempCopy + "\r\n";
                }

                groupRowHandle--;
            }

            string tempTitles = "";
            for (int i = 0; i < printTitles.Length; i++)
            {
                tempTitles = tempTitles + '\t' + printTitles[i];
            }

            CopyString = tempTitles + "\r\n" + CopyString;

            Clipboard.SetDataObject(CopyString, true);

        }

        private GridColumn GetColumnByName(GridView view, string Columnname)
        {
            foreach (GridColumn curItem in view.Columns)
            {
                if (curItem.FieldName == Columnname)
                {
                    return curItem;
                }
            }
            return null;
        }

        private void cmdCashAdjust_Click(object sender, EventArgs e)
        {
            using (newNestConn curConn = new newNestConn())
            {
                this.Cursor = Cursors.WaitCursor;
                int Id_Portfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());
                int IdBenchmark = 5049;  // 5049 = CDI | 1073 = BOVESPA
                int iRetorno;

                if (Id_Portfolio == 10) IdBenchmark = 1073;

                try
                {
                    string sSQL = String.Format("EXEC [dbo].[Proc_UpdateAdjust] {0}, '{1}', {2}", Id_Portfolio, (iniDate.AddDays(-70)).ToString("yyyy-MM-dd"), IdBenchmark);
                    iRetorno = Convert.ToInt32(curConn.ExecuteNonQuery(sSQL));
                    MessageBox.Show("Cash Adjustment - Done");
                }
                catch (Exception err)
                {
                    MessageBox.Show("Cash Adjustment - Error", err.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Cursor = Cursors.Default;
            }

        }

        private void Update_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

    }
}