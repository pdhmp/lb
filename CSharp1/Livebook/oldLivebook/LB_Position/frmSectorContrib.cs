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
    public partial class frmSectorContrib : LBForm
    {
        public frmSectorContrib()
        {
            InitializeComponent();
            NestDLL.FormUtils.LoadCombo(this.cmbPortfolio, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2", "Id_Portfolio", "Port_Name", 99);
            NestDLL.FormUtils.LoadCombo(this.cmbSector, "Select Setor from NESTDB.dbo.Tb113_Setores (nolock) ORDER BY Setor", "Setor", "Setor", 99);
            this.cmbSector.SelectedIndexChanged += new System.EventHandler(this.cmbSector_SelectedIndexChanged);
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbSector_SelectedIndexChanged);
            UpdateSector();
            cmbSector.SelectedIndex = 0;
            tmrUpdate.Start();
        }

        int LineCount = 0;
        double[] tempDates;
        double[] tempContrib;
        double[] tempExposure;
        DateTime MaxDate = DateTime.Now;
        DateTime MinDate = DateTime.Now;

        private void UpdateSector()
        {


            int IdPortfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());
            string NestSector = cmbSector.SelectedValue.ToString();

            string sql_Query = " SELECT [Date Now],SUM([Delta/NAV]) AS Exposure,SUM([Contribution pC]) AS Contribution " +
                            " FROM NESTDB.dbo.Tb000_Historical_Positions (nolock)  " +
                            "     WHERE [Date Now]>='2012-01-01' " +
                            "     AND [Date Now]<='2012-06-30' " +
                            //"     WHERE [Date Now]>='2011-01-01' " +
                            //"     AND [Date Now]<='2012-12-31' " +
                            "     AND [Id Portfolio] = " + IdPortfolio + " AND [Nest Sector]='" + NestSector + "'" +
                            "     GROUP BY [Date Now] " +
                            "     ORDER BY [Date Now] ";

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

            LineCount = perf.Rows.Count;
            int c = 0;

            tempDates = new double[LineCount];
            tempContrib = new double[LineCount];
            tempExposure = new double[LineCount];

            foreach (DataRow row in perf.Rows)
            {
                DateTime TempDate = Convert.ToDateTime(row["Date Now"]);
                if (TempDate > MaxDate) { MaxDate = TempDate; };
                if (TempDate < MinDate) { MinDate = TempDate; };

                tempDates[c] = NestDLL.Utils.ParseToDateTime(row["Date Now"]).ToOADate();
                tempContrib[c] = NestDLL.Utils.ParseToDouble(row["Contribution"]);
                tempExposure[c] = NestDLL.Utils.ParseToDouble(row["Exposure"]);
                c++;
            }
            RedrawChart();
        }

        private void UpdateSecurity()
        {
            int IdPortfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());
            int IdSecurity;

            if (Int32.TryParse(cmbSecurity.SelectedValue.ToString(), out IdSecurity))
            {

                string sql_Query = " SELECT [Date Now],SUM([Delta/NAV]) AS Exposure,SUM([Contribution pC]) AS Contribution " +
                                " FROM NESTDB.dbo.Tb000_Historical_Positions (nolock)  " +
                                "     WHERE [Date Now]>='2012-01-01' " +
                                "     AND [Date Now]<='2012-06-30' " +
                                //"     WHERE [Date Now]>='2011-01-01' " +
                                //"     AND [Date Now]<='2012-12-31' " +
                                "     AND [Id Portfolio] = " + IdPortfolio + " AND [Id Base Underlying]='" + IdSecurity + "'" +
                                "     GROUP BY [Date Now] " +
                                "     ORDER BY [Date Now] ";

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

                LineCount = perf.Rows.Count;
                int c = 0;

                tempDates = new double[LineCount];
                tempContrib = new double[LineCount];
                tempExposure = new double[LineCount];

                foreach (DataRow row in perf.Rows)
                {
                    DateTime TempDate = Convert.ToDateTime(row["Date Now"]);
                    if (TempDate > MaxDate) { MaxDate = TempDate; };
                    if (TempDate < MinDate) { MinDate = TempDate; };

                    tempDates[c] = NestDLL.Utils.ParseToDateTime(row["Date Now"]).ToOADate();
                    tempContrib[c] = NestDLL.Utils.ParseToDouble(row["Contribution"]);
                    tempExposure[c] = NestDLL.Utils.ParseToDouble(row["Exposure"]);
                    c++;
                }
                RedrawSecurity();
            }
        }

        public void RedrawChart()
        {
            double[] pDates = new double[LineCount];
            double[] pContrib = new double[LineCount];
            double[] pExposure = new double[LineCount];
            double cumContrib = 0;

            double minContrib = 1000;
            double maxContrib = -1000;

            double minExposure = 1000;
            double maxExposure = -1000;

            for (int j = 0; j < LineCount; j++)
            {
                cumContrib = (1 + cumContrib) * (1 + tempContrib[j]) - 1;

                pDates[j] = tempDates[j];
                pContrib[j] = cumContrib;
                pExposure[j] = tempExposure[j];
                if (cumContrib < minContrib) { minContrib = cumContrib;}
                if (cumContrib > maxContrib) { maxContrib = cumContrib; }
                if (pExposure[j] < minExposure) { minExposure = pExposure[j]; }
                if (pExposure[j] > maxExposure) { maxExposure = pExposure[j]; }
            }

            zgcSector.GraphPane.CurveList.Clear();
            zgcSector.GraphPane.GraphObjList.Clear();
            zgcSector.GraphPane.Border.IsVisible = false;

            GraphPane panSector = zgcSector.GraphPane;

            panSector.Title.IsVisible = false;
            panSector.XAxis.Title.IsVisible = false;
            panSector.YAxis.Title.IsVisible = false;
            

            LineItem crvContrib = panSector.AddCurve("Contribution", pDates, pContrib, Color.Red, SymbolType.None);
            LineItem crvExposure = panSector.AddCurve("Exposure", pDates, pExposure, Color.DarkGray, SymbolType.None);
            crvExposure.Line.Fill = new Fill(Color.Blue, Color.LightBlue, -90F);
            crvContrib.Line.Width = 2.0F;

            crvContrib.IsY2Axis = true;

            panSector.Y2Axis.IsVisible = true;
            panSector.YAxis.Cross = panSector.Y2Axis.Cross;

            // Legend and other
            panSector.Legend.Position = ZedGraph.LegendPos.Bottom;

            panSector.XAxis.Type = AxisType.Date;
            panSector.YAxis.Scale.Format = "0.00%";
            panSector.Y2Axis.Scale.Format = "0.00%";
            panSector.Y2Axis.IsVisible = true;

            panSector.XAxis.MajorGrid.IsVisible = true;
            panSector.YAxis.MajorGrid.IsVisible = true;
            panSector.XAxis.MajorGrid.Color = Color.Gray;
            panSector.YAxis.MajorGrid.Color = Color.Gray;

            panSector.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate();
            panSector.XAxis.Scale.Max = MaxDate.Add(new TimeSpan(1, 0, 0, 0)).ToOADate();

            //panSector.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate();
            //panSector.XAxis.Scale.Max = MinDate.Add(new TimeSpan(12, 0, 0, 0)).ToOADate();

            panSector.IsFontsScaled = false;
            panSector.XAxis.Scale.FontSpec.Size = 10;
            panSector.Y2Axis.Scale.FontSpec.Size = 10;
            panSector.Y2Axis.Scale.FontSpec.FontColor = Color.Red;
            panSector.YAxis.Scale.FontSpec.Size = 10;
            panSector.Legend.FontSpec.Size = 7;

            double curMinY = panSector.YAxis.Scale.Min;
            double curMaxY = panSector.YAxis.Scale.Max;

            panSector.YAxis.Scale.Min = (int)(minExposure / 0.05) * 0.05 - 0.05;
            panSector.YAxis.Scale.Max = (int)(maxExposure / 0.05) * 0.05 + 0.05;

            panSector.Y2Axis.Scale.Min = (int)(minContrib / 0.01) * 0.01 - 0.01;
            panSector.Y2Axis.Scale.Max = (int)(maxContrib / 0.01) * 0.01 + 0.01;

            panSector.YAxis.Scale.MinorStep = 0.05;
            panSector.YAxis.Scale.MajorStep = 0.05;
            panSector.Y2Axis.Scale.MinorStep = 0.01;
            panSector.Y2Axis.Scale.MajorStep = 0.01;

            panSector.XAxis.Scale.MinorUnit = DateUnit.Month;
            panSector.XAxis.Scale.MajorUnit = DateUnit.Month;

            if(panSector.XAxis.Scale.MajorStep == 1)
                panSector.XAxis.Scale.MajorStep = 1;

            panSector.XAxis.Scale.LabelGap = 1;
            panSector.XAxis.Scale.FontSpec.Angle = 90;

            //panSector.YAxis.Cross = 0;
            //panSector.Y2Axis.Cross = 0;
            zgcSector.IsSynchronizeYAxes = true;

            zgcSector.IsShowPointValues = true;

            this.Cursor = Cursors.Default;

            zgcSector.AxisChange();
            zgcSector.Refresh();
            zgcSector.ResumeLayout();
        }

        public void RedrawSecurity()
        {
            double[] pDates = new double[LineCount];
            double[] pContrib = new double[LineCount];
            double[] pExposure = new double[LineCount];
            double cumContrib = 0;

            double minContrib = 1000;
            double maxContrib = -1000;

            double minExposure = 1000;
            double maxExposure = -1000;

            for (int j = 0; j < LineCount; j++)
            {
                cumContrib = (1 + cumContrib) * (1 + tempContrib[j]) - 1;

                pDates[j] = tempDates[j];
                pContrib[j] = cumContrib;
                pExposure[j] = tempExposure[j];
                if (cumContrib < minContrib) { minContrib = cumContrib; }
                if (cumContrib > maxContrib) { maxContrib = cumContrib; }
                if (pExposure[j] < minExposure) { minExposure = pExposure[j]; }
                if (pExposure[j] > maxExposure) { maxExposure = pExposure[j]; }
            }

            zgcSecurity.GraphPane.CurveList.Clear();
            zgcSecurity.GraphPane.GraphObjList.Clear();
            zgcSecurity.GraphPane.Border.IsVisible = false;

            GraphPane panSector = zgcSecurity.GraphPane;

            panSector.Title.IsVisible = false;
            panSector.XAxis.Title.IsVisible = false;
            panSector.YAxis.Title.IsVisible = false;

            LineItem crvContrib = panSector.AddCurve("Contribution", pDates, pContrib, Color.Red, SymbolType.None);
            LineItem crvExposure = panSector.AddCurve("Exposure", pDates, pExposure, Color.DarkGray, SymbolType.None);
            crvExposure.Line.Fill = new Fill(Color.Blue, Color.LightBlue, -90F);
            crvContrib.Line.Width = 2.0F;

            crvContrib.IsY2Axis = true;

            panSector.Y2Axis.IsVisible = true;

            // Legend and other
            panSector.Legend.Position = ZedGraph.LegendPos.Bottom;

            panSector.XAxis.Type = AxisType.Date;
            panSector.YAxis.Scale.Format = "0.00%";
            panSector.Y2Axis.Scale.Format = "0.00%";
            panSector.Y2Axis.IsVisible = true;

            panSector.XAxis.MajorGrid.IsVisible = true;
            panSector.YAxis.MajorGrid.IsVisible = true;
            panSector.XAxis.MajorGrid.Color = Color.Gray;
            panSector.YAxis.MajorGrid.Color = Color.Gray;

            panSector.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate();
            panSector.XAxis.Scale.Max = MaxDate.Add(new TimeSpan(1, 0, 0, 0)).ToOADate();

            //panSector.XAxis.Scale.Min = MinDate.Add(new TimeSpan(-1, 0, 0, 0)).ToOADate();
            //panSector.XAxis.Scale.Max = MinDate.Add(new TimeSpan(12, 0, 0, 0)).ToOADate();

            panSector.IsFontsScaled = false;
            panSector.XAxis.Scale.FontSpec.Size = 10;
            panSector.Y2Axis.Scale.FontSpec.Size = 10;
            panSector.Y2Axis.Scale.FontSpec.FontColor = Color.Red;
            panSector.YAxis.Scale.FontSpec.Size = 10;
            panSector.Legend.FontSpec.Size = 7;

            double curMinY = panSector.YAxis.Scale.Min;
            double curMaxY = panSector.YAxis.Scale.Max;

            panSector.YAxis.Scale.Min = (int)(minExposure / 0.05) * 0.05 - 0.05;
            panSector.YAxis.Scale.Max = (int)(maxExposure / 0.05) * 0.05 + 0.05;

            panSector.Y2Axis.Scale.Min = (int)(minContrib / 0.01) * 0.01 - 0.01;
            panSector.Y2Axis.Scale.Max = (int)(maxContrib / 0.01) * 0.01 + 0.01;

            panSector.YAxis.Scale.MinorStep = 0.05;
            panSector.YAxis.Scale.MajorStep = 0.05;

            if (maxContrib - minContrib > 0.03)
            {
                panSector.Y2Axis.Scale.MinorStep = 0.01;
                panSector.Y2Axis.Scale.MajorStep = 0.01;
            }
            else
            {
                panSector.Y2Axis.Scale.MinorStep = 0.0025;
                panSector.Y2Axis.Scale.MajorStep = 0.0025;
            }

            panSector.XAxis.Scale.MinorUnit = DateUnit.Month;
            panSector.XAxis.Scale.MajorUnit = DateUnit.Month;

            if (panSector.XAxis.Scale.MajorStep == 1)
                panSector.XAxis.Scale.MajorStep = 1;

            panSector.XAxis.Scale.LabelGap = 1;
            panSector.XAxis.Scale.FontSpec.Angle = 90;

            zgcSecurity.IsShowPointValues = true;

            this.Cursor = Cursors.Default;

            zgcSecurity.AxisChange();
            zgcSecurity.Refresh();
            zgcSecurity.ResumeLayout();
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            UpdateSector();
        }

        private void cmbSector_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSector();
            int IdPortfolio = Convert.ToInt32(cmbPortfolio.SelectedValue.ToString());
            string NestSector = cmbSector.SelectedValue.ToString();
            this.cmbPortfolio.SelectedIndexChanged -= new System.EventHandler(this.cmbSecurity_SelectedIndexChanged);
            NestDLL.FormUtils.LoadCombo(this.cmbSecurity, "SELECT [Id Base Underlying], [Base Underlying] FROM NESTDB.dbo.Tb000_Historical_Positions (nolock) WHERE [Id Portfolio]=" + IdPortfolio + " AND [Nest Sector]='" + NestSector + "' AND [Id Base Underlying] IS NOT NULL AND [Date Now]>'2009-12-31' GROUP BY [Base Underlying],[Id Base Underlying] ORDER BY [Base Underlying]", "Id Base Underlying", "Base Underlying", 99);
            this.cmbPortfolio.SelectedIndexChanged += new System.EventHandler(this.cmbSecurity_SelectedIndexChanged);
        }

        private void frmSectorContrib_Load(object sender, EventArgs e)
        {
            UpdateSector();
        }

        private void cmbSecurity_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSecurity();
        }

    }
}