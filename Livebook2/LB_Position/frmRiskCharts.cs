using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

using LiveDLL;
using ZedGraph;

namespace LiveBook
{
    public partial class frmRiskCharts : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmRiskCharts()
        {
            InitializeComponent();
        }

        private void frmRiskCharts_Load(object sender, EventArgs e)
        {
            SetSize();

            string SQLString_Gross = "SELECT [Date Now], SUM(ABS([Delta/nav])) AS GrossExposure " +
                               " , SUM(ABS(CASE WHEN [Id Instrument]<>3 THEN [Delta/nav] ELSE 0 END)) AS GrossExOptions" +
                               " FROM NESTDB.dbo.Tb000_Historical_Positions (nolock) " +  
                               " WHERE [Id Portfolio]=10 AND [Asset Class] = 'Equity' and [Date Now]>='2012-12-31' " +
                               " GROUP BY [Date Now] " +
                               " ORDER BY [Date Now]";

            LoadGraphFromSQL_TimeLine(zgcGrossExposure, SQLString_Gross, false);

            string SQLString_Vol = "SELECT * FROM [dbo].[FCN_Risk_Rolling_Vol](4946) WHERE curDate>'2012-12-31'";

            LoadGraphFromSQL_TimeLine(zgcVolatility, SQLString_Vol, false);

            //tmrRefresh.Start();
        }

        private void frmRiskCharts_Resize(object sender, EventArgs e)
        {
            SetSize();
        }

        private void SetSize()
        {
            int noCharts = 2;

            zgcGrossExposure.Location = new Point(0, 0);
            zgcGrossExposure.Size = new Size(ClientRectangle.Width, ClientRectangle.Height / noCharts);
            zgcGrossExposure.AxisChange();
            zgcGrossExposure.Refresh();

            zgcVolatility.Location = new Point(0, ClientRectangle.Height / noCharts);
            zgcVolatility.Size = new Size(ClientRectangle.Width, ClientRectangle.Height / noCharts);
            zgcVolatility.AxisChange();
            zgcVolatility.Refresh();
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            //CreateGraph(zedGraphControl1);
        }

        private void LoadGraphFromSQL_TimeLine(ZedGraphControl curChartControl, string StringSQL, bool GrayScale)
        {

            curChartControl.GraphPane.CurveList.Clear();
            curChartControl.GraphPane.Border.IsVisible = false;

            GraphPane curChartPane = curChartControl.GraphPane;

            // Create Data Arrays

            List<PointPairList> DataPoints = new List<PointPairList>();
            DateTime MaxDate = new DateTime(1900, 1, 1, 0, 0, 0);
            DateTime MinDate = DateTime.Now;
            int LineCounter = 0;

            DataTable curTable = curConn.Return_DataTable(StringSQL);

            for (int i = 1; i < curTable.Columns.Count; i++)
            {
                PointPairList curPoints = new PointPairList();

                foreach (DataRow curRow in curTable.Rows)
                {
                    DateTime TempDate = Convert.ToDateTime(curRow[0].ToString());
                    if (TempDate > MaxDate) { MaxDate = TempDate; };
                    if (TempDate < MinDate) { MinDate = TempDate; };

                    double valDate = (double)TempDate.ToOADate();
                    double valValue = double.Parse(curRow[i].ToString());

                    curPoints.Add(valDate, valValue);

                }

                curPoints.Sort();

                DataPoints.Add(curPoints);

                LineItem curLine = curChartPane.AddCurve(curTable.Columns[i].Caption, curPoints, GetLineColor(LineCounter++, GrayScale), SymbolType.None);
                curLine.Line.Width = 2.0F;

            }

            // Set the Titles
            curChartPane.Title.IsVisible = false;
            curChartPane.XAxis.Title.IsVisible = false;
            curChartPane.YAxis.Title.IsVisible = false;

            curChartPane.Legend.Position = ZedGraph.LegendPos.Bottom;

            curChartPane.XAxis.Type = AxisType.Date;
            curChartPane.YAxis.Scale.Format = "0.00%";

            curChartPane.XAxis.MajorGrid.IsVisible = true;
            curChartPane.YAxis.MajorGrid.IsVisible = true;
            curChartPane.XAxis.MajorGrid.Color = Color.Gray;
            curChartPane.YAxis.MajorGrid.Color = Color.Gray;

            curChartPane.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate();//.Add(new TimeSpan(-1, 0, 0)).ToOADate();
            curChartPane.XAxis.Scale.Max = MaxDate.Add(new TimeSpan(1, 0, 0, 0)).ToOADate();

            curChartPane.IsFontsScaled = false;
            curChartPane.XAxis.Scale.FontSpec.Size = 10;
            curChartPane.YAxis.Scale.FontSpec.Size = 10;
            curChartPane.Legend.FontSpec.Size = 12;

            curChartPane.XAxis.Scale.MinorUnit = DateUnit.Day;
            curChartPane.XAxis.Scale.MajorUnit = DateUnit.Day;

            curChartControl.IsShowPointValues = true;

            curChartControl.AxisChange();
            curChartControl.Refresh();

        }

        private Color GetLineColor(int LineIndex, bool GrayScale)
        {
            if (GrayScale)
            {
                switch (LineIndex)
                {
                    case 0: return Color.Black;
                    //case 1: return Color.DimGray;
                    //case 2: return Color.DarkGray;
                    case 1: return Color.Gray;
                    //case 5: return Color.SlateGray;
                    //case 2: return Color.LightSlateGray;
                    //case 3: return Color.Silver;
                    //case 2: return Color.Gainsboro;
                    //case 2: return Color.WhiteSmoke;
                    case 2: return Color.LightGray;
                    default: return Color.Orange;
                }
            }
            else
            {
                switch (LineIndex)
                {
                    case 0: return Color.Black;
                    case 1: return Color.Red;
                    case 2: return Color.Green;
                    case 3: return Color.Blue;
                    case 4: return Color.Lime;
                    case 5: return Color.Magenta;
                    case 6: return Color.Brown;
                    default: return Color.Orange;
                }
            }
        }
    }
}