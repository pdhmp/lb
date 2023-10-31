using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using ZedGraph;

namespace LiveBook
{
    public partial class frmOptionsGraph : LBForm
    {
        public frmOptionsGraph()
        {
            InitializeComponent();

            this.cmbPortfolio.SelectedIndexChanged -= new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);
            NestDLL.FormUtils.LoadCombo(this.cmbPortfolio, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2", "Id_Portfolio", "Port_Name", 99);
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbPortfolio_SelectedIndexChanged);

            UpdateSummary();
        }

        private void UpdateSummary()
        {
            this.Cursor = Cursors.WaitCursor;

            int IdPortfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());
            DateTime ReportDate = dtpDate.Value;


            zgcOptionPayoff.GraphPane.CurveList.Clear();
            zgcOptionPayoff.GraphPane.Border.IsVisible = false;

            // get a reference to the GraphPane
            GraphPane myPane = zgcOptionPayoff.GraphPane;

            // Set the Titles
            myPane.Title.IsVisible = false;
            myPane.XAxis.Title.IsVisible = false;
            myPane.YAxis.Title.IsVisible = false;

            string InnerBlockNegCall = "";
            string InnerBlockPosCall = "";
            string InnerBlockNegPut = "";
            string InnerBlockPosPut = "";
            string OuterBlockNeg = "";
            string OuterBlockPos = "";

            for (int i = 1; i < 100; i++)
            {
                int negi = (100 - i);
                string tempCounterPos = i.ToString();
                string tempCounterNeg = negi.ToString();
                if (i < 10) tempCounterPos = "0" + tempCounterPos;
                if (negi < 10) tempCounterNeg = "0" + tempCounterNeg;
                OuterBlockNeg += ", SUM([n" + tempCounterNeg + "]) AS [n" + tempCounterNeg + "]";
                OuterBlockPos += ", SUM([p" + tempCounterPos + "]) AS [p" + tempCounterPos + "]";
                InnerBlockNegCall += ", (CASE WHEN (1+-0." + tempCounterNeg + ")*UndLast-Strike>0 THEN (1+-0." + tempCounterNeg + ")*UndLast-Strike ELSE 0 END*Position-CurValue)/NAV AS [n" + tempCounterNeg + "]";
                InnerBlockPosCall += ", (CASE WHEN (1+0." + tempCounterPos + ")*UndLast-Strike>0 THEN (1+0." + tempCounterPos + ")*UndLast-Strike ELSE 0 END*Position-CurValue)/NAV AS [p" + tempCounterPos + "]";
                InnerBlockNegPut += ", (CASE WHEN Strike-(1+-0." + tempCounterNeg + ")*UndLast>0 THEN Strike-(1+-0." + tempCounterNeg + ")*UndLast ELSE 0 END*Position-CurValue)/NAV AS [n" + tempCounterNeg + "]";
                InnerBlockPosPut += ", (CASE WHEN Strike-(1+0." + tempCounterPos + ")*UndLast>0 THEN Strike-(1+0." + tempCounterPos + ")*UndLast ELSE 0 END*Position-CurValue)/NAV AS [p" + tempCounterPos + "]";
            }

            string Table_Name = "";

            if (dtpDate.Value.ToString("yyyyMMdd") == DateTime.Now.ToString("yyyyMMdd"))
            {
                Table_Name = "NESTRT.dbo.FCN_Posicao_Atual()";
            }
            else
            {
                Table_Name = "NESTDB.dbo.Tb000_Historical_Positions (NOLOCK)";
            }

            //string PortFilter = "([Id Portfolio]=38 AND (([Id Book]=9 AND [New Id Sub Strategy]=16 AND [Id Section]<>99) OR ([Id Book]=9 AND [New Id Sub Strategy]=34)))";
            //if (IdPortfolio != 38) PortFilter = "[Id Portfolio]=" + IdPortfolio;
            //string PortFilter = "([Id Portfolio]=38 AND (([Id Book]=9 AND [New Id Sub Strategy]=16 AND [Id Section]<>99) OR ([Id Book]=9 AND [New Id Sub Strategy]=34)))";
            string PortFilter = "([Id Portfolio]=38 AND (([Id Book]=9 AND [New Id Sub Strategy]=16 AND [Id Section] NOT IN (99,329)) OR ([Id Book]=9 AND [New Id Sub Strategy]=34)))";
            if (IdPortfolio != 38) PortFilter = "[Id Portfolio]=" + IdPortfolio;
            if (IdPortfolio == 18) PortFilter = "[Id Portfolio]=" + IdPortfolio + " AND [Id Asset Class]=1";


            string StringSQL = "SELECT Underlying " + OuterBlockNeg + ", SUM([Chg0]) AS [Chg0]       " + OuterBlockPos +
                "FROM( " +
                "SELECT * " + InnerBlockNegCall + ", (CASE WHEN (1+-0)*UndLast-Strike>0 THEN (1+0)*UndLast-Strike ELSE 0 END*Position-CurValue)/NAV AS [Chg0]   " + InnerBlockPosCall +
                "FROM " +
                "   (             " +
                "          SELECT Ticker, Position, [Option Type], Underlying, Strike, Last   " +
                "                 , CASE WHEN [Id Instrument]=3 THEN [Underlying Last] ELSE Last END AS UndLast " +
                "                 , Position*Last as CurValue " +
                "                 , NAV " +
                "          FROM  " + Table_Name +
                "          WHERE " + PortFilter + " AND [Date Now]='" + ReportDate.ToString("yyyyMMdd") + "' AND ([Option Type]=1 OR [Id Instrument]<>3)" +
                "   ) A    " +
                " UNION ALL SELECT * " + InnerBlockNegPut + " , (CASE WHEN Strike-(1+-0)*UndLast>0 THEN Strike-(1+0)*UndLast ELSE 0 END*Position-CurValue)/NAV AS [Chg0] " + InnerBlockPosPut +
                " FROM            " +
                "   (            " +
                "          SELECT Ticker, Position, [Option Type], Underlying, Strike, Last   " +
                "                 , CASE WHEN [Id Instrument]=3 THEN [Underlying Last] ELSE Last END AS UndLast " +
                "                 , Position*Last as CurValue " +
                "                 , NAV " +
                "          FROM " + Table_Name + " WHERE " + PortFilter;


            StringSQL += " AND [Date Now]='" + ReportDate.ToString("yyyyMMdd") + "' AND [Option Type]=0 " +
                         " ) A " +
                         " ) B       " +
                         "GROUP BY Underlying      ";

            if (IdPortfolio != 38)
            {
                StringSQL = StringSQL.Replace("WHERE", "WHERE ([Id Instrument]=3 OR [Id Base Underlying] IN (SELECT [Id Base Underlying] FROM " + Table_Name + " WHERE [Id Instrument]=3 AND [Id Portfolio] =" + IdPortfolio + " AND [Date Now]='" + ReportDate.ToString("yyyyMMdd") + "')) AND ");
            }
            DataTable perf = new DataTable();

            try
            {
                using (newNestConn conn = new newNestConn())
                {
                    perf = conn.Return_DataTable(StringSQL);
                }
            }
            catch
            {
                return;
            }

            // Create Data Arrays

            List<PointPairList> DataPoints = new List<PointPairList>();

            int LineCounter = 0;

            PointPairList curTotalPoints = new PointPairList();

            double[] TotalArray = new double[perf.Columns.Count];
            double MinValue = 0;

            SortedDictionary<string,double> SortDic = new SortedDictionary<string,double>();

            DataTable Carry = new DataTable();


            StringSQL = "SELECT * FROM dbo.FCN_Risk_Arb_Option_Carry('" + ReportDate.ToString("yyyyMMdd") + "')  ";

                using (newNestConn conn = new newNestConn())
                {
                    Carry = conn.Return_DataTable(StringSQL);
                }

                int Counter=0;

                foreach (DataRow curRow in Carry.Rows)
                {
                    string[] Dt = new string[2];
                    SortDic.Add(curRow[0].ToString(), Convert.ToDouble(curRow[1]));
                    Counter++;
                }

            foreach (DataRow curRow in perf.Rows)
            {

                string ItemName = curRow[0].ToString();
                double CarryValue=0;

                SortDic.TryGetValue(ItemName.ToString(), out CarryValue);

                PointPairList curPoints = new PointPairList();
                for (int i = 1; i < curRow.ItemArray.Length; i++)
                {
                    double tempval = double.Parse((perf.Columns[i].ColumnName.Replace("n", "-").Replace("p", "").Replace("Chg", "")));
                    double tempX = tempval / 100;
                    double tempY = double.Parse(curRow[i].ToString());
                    tempY += CarryValue;
                    TotalArray[i] = TotalArray[i] + tempY;
                    if (TotalArray[i] < MinValue) { MinValue = TotalArray[i]; }

                    curPoints.Add(tempX, tempY);
                }

                lblMinLoss.Text = MinValue.ToString("0.00%");
                curPoints.Sort();
                DataPoints.Add(curPoints);

                LineItem curLine = myPane.AddCurve(ItemName, curPoints, GetLineColor(LineCounter++), SymbolType.None);
                curLine.Line.Width = 2.0F;
            }

            for (int i = 1; i < perf.Columns.Count; i++)
            {
                double tempval = double.Parse((perf.Columns[i].ColumnName.Replace("n", "-").Replace("p", "").Replace("Chg", "")));
                curTotalPoints.Add(tempval / 100, TotalArray[i]);
            }

            LineItem curTotal = myPane.AddCurve("Total", curTotalPoints, Color.Black, SymbolType.None);
            curTotal.Line.Width = 3.0F;

            myPane.Legend.Position = ZedGraph.LegendPos.Bottom;

            myPane.XAxis.Type = AxisType.Linear;
            myPane.YAxis.Scale.Format = "0.00%";
            myPane.XAxis.Scale.Format = "0.00%";

            myPane.YAxis.Cross = 0;

            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;
            myPane.XAxis.MajorGrid.Color = Color.Gray;
            myPane.YAxis.MajorGrid.Color = Color.Gray;

            myPane.XAxis.Scale.Min = -1;
            myPane.XAxis.Scale.Max = 1;

            myPane.IsFontsScaled = false;
            myPane.XAxis.Scale.FontSpec.Size = 10;
            myPane.YAxis.Scale.FontSpec.Size = 10;
            myPane.Legend.FontSpec.Size = 12;

            myPane.XAxis.Scale.MajorStep = 0.05;

            zgcOptionPayoff.IsShowPointValues = true;

            this.Cursor = Cursors.Default;

            zgcOptionPayoff.AxisChange();
            zgcOptionPayoff.Refresh();
        }


        private Color GetLineColor(int LineIndex)
        {
            switch (LineIndex)
            {
                case 0: return Color.Red;
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

        private void AddBarLabels(ref GraphPane curPane, ref BarItem curBarItem, double LabelOffset, int curFontSize, Color curFontColor)
        {
            for (int i = 0; i < curBarItem.Points.Count; i++)
            {
                PointPair pt = curBarItem.Points[i];
                // Create a text label from the Y data value 
                AlignH curAlign = AlignH.Left;
                double XOffset = 0.0006;

                if (pt.X < 0)
                {
                    curAlign = AlignH.Right;
                    XOffset = -XOffset;
                }
                TextObj text = new TextObj(pt.X.ToString("0.0%"), pt.X + XOffset, pt.Y + LabelOffset, CoordType.AxisXYScale, curAlign, AlignV.Center);
                text.ZOrder = ZOrder.A_InFront;
                text.FontSpec.Border.IsVisible = false;
                text.FontSpec.Fill.IsVisible = false;
                text.FontSpec.FontColor = curFontColor;
                text.FontSpec.Size = curFontSize;
                text.FontSpec.Angle = 0;
                curPane.GraphObjList.Add(text);
            }

        }

        private void frmExposureGraph_Load(object sender, EventArgs e)
        {
            UpdateSummary();
        }

        private void zgcSector_Load(object sender, EventArgs e)
        {

        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {

        }

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSummary();
        }

        private void dtpDate_ValueChanged(object sender, EventArgs e)
        {
            UpdateSummary();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            UpdateSummary();
        }

    }
}