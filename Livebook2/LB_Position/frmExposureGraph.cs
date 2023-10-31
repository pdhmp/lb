using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LiveDLL;
using ZedGraph;

namespace LiveBook
{
    public partial class frmExposureGraph : LBForm
    {
        public frmExposureGraph()
        {
            InitializeComponent();
            LiveDLL.FormUtils.LoadCombo(this.cmbPortfolio, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2", "Id_Portfolio", "Port_Name", 99);
            UpdateSector();
            UpdateSummary();
            tmrUpdate.Start();
        }        

        private void UpdateSector()
        {
            int IdPortfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());

            DateTime eomDate = DateTime.Now.Add(new TimeSpan(-DateTime.Now.Day, 0, 0, 0)).Date;
            DateTime mDate = eomDate.AddMonths(-2).Add(new TimeSpan(-eomDate.Day, 0, 0, 0)).Date;

            string sql_Query = " SELECT * FROM [dbo].[FCN_EXPOSURE_GRAPH](" + IdPortfolio + ");";

            DataTable perf = new DataTable();

            try
            {
                using (newNestConn conn = new newNestConn())
                {
                    perf = conn.Return_DataTable(sql_Query);
                }
            }
            catch
            {
                return;
            }

            string[] tempLabels = new string[30];
            double[] tempNetBZ = new double[30];
            double[] tempNetPrev = new double[30];
            double[] tempNet3m = new double[30];


            int c = 0;

            foreach (DataRow row in perf.Rows)
            {
                tempLabels[c] = row["NestSector"].ToString();
                tempNetBZ[c] = LiveDLL.Utils.ParseToDouble(row["ExposureNow"]);
                tempNetPrev[c] = LiveDLL.Utils.ParseToDouble(row["ExposurePrevMoth"]);
                tempNet3m[c] = LiveDLL.Utils.ParseToDouble(row["ExposurePrev3M"]);

                c++;
            }

            string[] Labels = new string[c];
            double[] pNetBZ = new double[c];
            double[] pNetPrev = new double[c];
            double[] pNet3m = new double[c];

            for (int j = 0; j < c; j++)
            {
                Labels[j] = tempLabels[j];
                pNetBZ[j] = tempNetBZ[j];
                pNetPrev[j] = tempNetPrev[j];
                pNet3m[j] = tempNet3m[j];
            }

            zgcSector.GraphPane.CurveList.Clear();
            zgcSector.GraphPane.GraphObjList.Clear();
            zgcSector.GraphPane.Border.IsVisible = false;

            GraphPane panSector = zgcSector.GraphPane;

            panSector.Title.IsVisible = false;
            panSector.XAxis.Title.IsVisible = false;
            panSector.YAxis.Title.IsVisible = false;

            BarItem crvNet3m = panSector.AddBar("3M Before", pNet3m, null, Color.Gray);
            crvNet3m.Bar.Fill = new Fill(Color.Gray, Color.LightGray, Color.Gray, 90);
            BarItem crvNetPrev = panSector.AddBar("Last Month", pNetPrev, null, Color.DarkGray);
            crvNetPrev.Bar.Fill = new Fill(Color.DarkGray, Color.Gray, Color.DarkGray, 90);
            BarItem crvNetNow = panSector.AddBar("Now", pNetBZ, null, Color.Green);
            crvNetNow.Bar.Fill = new Fill(Color.Blue, Color.LightBlue, Color.Blue, 90);

            panSector.BarSettings.Base = BarBase.Y;
            panSector.IsFontsScaled = false;

            // Customize the YAxis
            panSector.YAxis.MajorGrid.Color = Color.Gray;
            panSector.YAxis.MajorGrid.IsVisible = true;
            panSector.YAxis.Scale.FontSpec.Size = 10;
            panSector.YAxis.MajorTic.IsBetweenLabels = true;
            panSector.YAxis.Scale.MajorStep = 1;

            panSector.YAxis.Scale.TextLabels = Labels;
            panSector.YAxis.Type = AxisType.Text;

            // Customize the XAxis
            panSector.XAxis.Scale.Format = "0.00%";
            panSector.XAxis.MajorGrid.IsVisible = true;
            panSector.XAxis.MajorGrid.Color = Color.Gray;

            panSector.XAxis.Scale.FontSpec.Size = 10;
            
            // Customize Legend
            panSector.Legend.FontSpec.Size = 12;
            panSector.Legend.IsVisible = true;
            panSector.Legend.Position = LegendPos.BottomFlushLeft;

            AddBarLabels(ref panSector, ref crvNetNow, -0.3, 10, Color.Black);
            this.Cursor = Cursors.Default;

            panSector.BarSettings.MinClusterGap  = 1F;

            zgcSector.AxisChange();
            zgcSector.Refresh();
        }

        private void UpdateSummary()
        {
            return;
            string sql_Query = "	SELECT  " +
                                "	SUM([Delta/Book]) AS Net " +
                                "	, SUM(CASE WHEN [Delta/Book]>0 THEN [Delta/Book] ELSE 0 END) AS Long " +
                                "	, SUM(CASE WHEN [Delta/Book]<0 THEN [Delta/Book] ELSE 0 END) AS Short " +
                                "	, SUM(ABS([Delta/Book])) AS Gross " +
                                "	, SUM(CASE WHEN [Id Section]=54 THEN [Delta/Book]/[Strategy %] ELSE 0 END) AS NetBZ  " +
                                "	, SUM(CASE WHEN [Id Section]=54 AND [Delta/Book]>0 THEN [Delta/Book]/[Strategy %] ELSE 0 END) AS LongBZ " +
                                "	, SUM(CASE WHEN [Id Section]=54 AND [Delta/Book]<0 THEN [Delta/Book]/[Strategy %] ELSE 0 END) AS ShortBZ " +
                                "	, SUM(ABS(CASE WHEN [Id Section]=54 THEN [Delta/Book]/[Strategy %] ELSE 0 END)) AS GrossBZ " +
                                "	, SUM(CASE WHEN [Id Section]=55 THEN [Delta/Book]/[Strategy %] ELSE 0 END) AS NetUS  " +
                                "	, SUM(CASE WHEN [Id Section]=55 AND [Delta/Book]>0 THEN [Delta/Book]/[Strategy %] ELSE 0 END) AS LongUS " +
                                "	, SUM(CASE WHEN [Id Section]=55 AND [Delta/Book]<0 THEN [Delta/Book]/[Strategy %] ELSE 0 END) AS ShortUS " +
                                "	, SUM(ABS(CASE WHEN [Id Section]=55 THEN [Delta/Book]/[Strategy %] ELSE 0 END)) AS GrossUS " +
                                "	, SUM(CASE WHEN [Id Section]=68 THEN [Delta/Book]/[Strategy %] ELSE 0 END) AS NetValBZ  " +
                                "	, SUM(CASE WHEN [Id Section]=68 AND [Delta/Book]>0 THEN [Delta/Book]/[Strategy %] ELSE 0 END) AS LongValBZ " +
                                "	, SUM(CASE WHEN [Id Section]=68 AND [Delta/Book]<0 THEN [Delta/Book]/[Strategy %] ELSE 0 END) AS ShortValBZ " +
                                "	, SUM(ABS(CASE WHEN [Id Section]=68 THEN [Delta/Book]/[Strategy %] ELSE 0 END)) AS GrossValBZ " +
                                "	FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) X " +
                                "	where [id portfolio]=43 AND [New Strategy]='Equities' AND [Id Section] IN (54,55,68)";

            DataTable perf = new DataTable();

            double MomBZSize;
            double MomUSSize;
            double ValBZSize; 

            try
            {
                using (newNestConn conn = new newNestConn())
                {
                    perf = conn.Return_DataTable(sql_Query);
                    MomBZSize = conn.Return_Double("SELECT TOP 1 [Strategy %] FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Section]=54");
                    MomUSSize = conn.Return_Double("SELECT TOP 1 [Strategy %] FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Section]=55");
                    ValBZSize = conn.Return_Double("SELECT TOP 1 [Strategy %] FROM NESTRT.dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Section]=68");
                }
            }
            catch
            {
                return;
            }

            string[] Labels = new string[4];
            double[] pValues = new double[4];
            double[] bzValues = new double[4];
            double[] usValues = new double[4];
            double[] valbzValues = new double[4];

            Labels[3] = "Net";
            Labels[2] = "Long";
            Labels[1] = "Short";
            Labels[0] = "Gross";

            foreach (DataRow row in perf.Rows)
            {
                pValues[3] = LiveDLL.Utils.ParseToDouble(row["Net"]) / (MomBZSize + MomUSSize + ValBZSize);
                pValues[2] = LiveDLL.Utils.ParseToDouble(row["Long"]) / (MomBZSize + MomUSSize + ValBZSize);
                pValues[1] = LiveDLL.Utils.ParseToDouble(row["Short"]) / (MomBZSize + MomUSSize + ValBZSize);
                pValues[0] = LiveDLL.Utils.ParseToDouble(row["Gross"]) / (MomBZSize + MomUSSize + ValBZSize);

                bzValues[3] = LiveDLL.Utils.ParseToDouble(row["NetBZ"]);
                bzValues[2] = LiveDLL.Utils.ParseToDouble(row["LongBZ"]);
                bzValues[1] = LiveDLL.Utils.ParseToDouble(row["ShortBZ"]);
                bzValues[0] = LiveDLL.Utils.ParseToDouble(row["GrossBZ"]);

                usValues[3] = LiveDLL.Utils.ParseToDouble(row["NetUS"]);
                usValues[2] = LiveDLL.Utils.ParseToDouble(row["LongUS"]);
                usValues[1] = LiveDLL.Utils.ParseToDouble(row["ShortUS"]);
                usValues[0] = LiveDLL.Utils.ParseToDouble(row["GrossUS"]);

                valbzValues[3] = LiveDLL.Utils.ParseToDouble(row["NetValBZ"]);
                valbzValues[2] = LiveDLL.Utils.ParseToDouble(row["LongValBZ"]);
                valbzValues[1] = LiveDLL.Utils.ParseToDouble(row["ShortValBZ"]);
                valbzValues[0] = LiveDLL.Utils.ParseToDouble(row["GrossValBZ"]);

            }

            zgcExpTotal.GraphPane.CurveList.Clear();
            zgcExpTotal.GraphPane.GraphObjList.Clear();
            zgcExpTotal.GraphPane.Border.IsVisible = false;

            GraphPane panExpTotal = zgcExpTotal.GraphPane;

            panExpTotal.Title.IsVisible = false;
            panExpTotal.XAxis.Title.IsVisible = false;
            panExpTotal.YAxis.Title.IsVisible = false;

            BarItem crvExpTotal = panExpTotal.AddBar("Total", pValues, null, Color.LightGray);
            crvExpTotal.Bar.Fill = new Fill(Color.LightGray, Color.LightGray, Color.LightGray, 90);
            crvExpTotal.Bar.Border = new Border(Color.Gray, 0.1F);

            BarItem crvExpBZ = panExpTotal.AddBar("MomBZ", bzValues, null, Color.Green);
            crvExpBZ.Bar.Fill = new Fill(Color.Green, Color.White, Color.Green, 90);
            crvExpBZ.Bar.Border = new Border(Color.Gray, 0.1F);

            BarItem crvExpUS = panExpTotal.AddBar("MomUS", usValues, null, Color.Blue);
            crvExpUS.Bar.Fill = new Fill(Color.Blue, Color.White, Color.Blue, 90);
            crvExpUS.Bar.Border = new Border(Color.Gray, 0.1F);

            BarItem crvExpValBZ = panExpTotal.AddBar("ValBZ", valbzValues, null, Color.DarkOrange);
            crvExpValBZ.Bar.Fill = new Fill(Color.DarkOrange, Color.White, Color.DarkOrange, 90);
            crvExpValBZ.Bar.Border = new Border(Color.Gray, 0.1F);

            panExpTotal.BarSettings.Base = BarBase.Y;
            panExpTotal.IsFontsScaled = false;

            // Customize the YAxis
            panExpTotal.YAxis.MajorGrid.Color = Color.Gray;
            panExpTotal.YAxis.MajorGrid.IsVisible = true;
            panExpTotal.YAxis.Scale.FontSpec.Size = 10;
            panExpTotal.YAxis.MajorTic.IsBetweenLabels = true;
            panExpTotal.YAxis.Scale.MajorStep = 1;

            panExpTotal.YAxis.Scale.TextLabels = Labels;
            panExpTotal.YAxis.Type = AxisType.Text;

            // Customize the XAxis
            panExpTotal.XAxis.Scale.Format = "0.00%";
            panExpTotal.XAxis.MajorGrid.IsVisible = true;
            panExpTotal.XAxis.MajorGrid.Color = Color.Gray;

            panExpTotal.XAxis.Scale.FontSpec.Size = 10;

            // Customize Legend
            panExpTotal.Legend.IsVisible = false;

            double TopOffSet = -0.01;

            AddBarLabels(ref panExpTotal, ref crvExpTotal, 0.30 + TopOffSet, 10, Color.Gray);
            AddBarLabels(ref panExpTotal, ref crvExpBZ, 0.1 + TopOffSet, 10, Color.Black);
            AddBarLabels(ref panExpTotal, ref crvExpUS, -0.1 + TopOffSet, 10, Color.Black);
            AddBarLabels(ref panExpTotal, ref crvExpValBZ, -0.30 + TopOffSet, 10, Color.Black);

            this.Cursor = Cursors.Default;

            panExpTotal.BarSettings.MinClusterGap = 1.5F;

            zgcExpTotal.AxisChange();
            zgcExpTotal.Refresh();
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
                //text.FontSpec.Fill = new Fill(
                text.FontSpec.Size = curFontSize;
                text.FontSpec.Angle = 0;
                curPane.GraphObjList.Add(text);
            }

        }

        private void frmExposureGraph_Load(object sender, EventArgs e)
        {

        }

        private void zgcSector_Load(object sender, EventArgs e)
        {

        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            UpdateSector();
            UpdateSummary();
        }

    }
}