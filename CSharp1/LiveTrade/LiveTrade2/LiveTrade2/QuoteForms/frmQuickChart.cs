using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using ZedGraph;
using NCommonTypes;

namespace LiveTrade2
{
    public partial class frmQuickChart : ConnectedForm
    {

        private int _IdSecurity=0;
        private string DragTicker = "";
        DateTime MaxDate = DateTime.Now;
        DateTime MinDate = DateTime.Now;
        DateTime CustomMaxDate = DateTime.Now;
        DateTime CustomMinDate = DateTime.Now;
        double CustomOffset = 0;
        DateTime MDownMaxDate = DateTime.Now;
        DateTime MDownMinDate = DateTime.Now;

        MarketDataItem curMarketItem = new MarketDataItem();
        string _curTicker = "";
        int _curIdInstrument = 0; public int curIdInstrument { get { return _curIdInstrument; } set { _curIdInstrument = value; } }
        Sources curSource;

        double LastSeriesPrice = 0;
        public double LastPrice { get { if (curMarketItem.Last != 0) { return curMarketItem.Last; } else { return LastSeriesPrice; } } }

        int TrendMode = 0;

        bool ChartNeedsUpdating = false;
        bool ChartIsRunning = false;
        bool ChartLoaded = false;

        double LastPriceDrawn = 0;
        ZedGraphControl zgc;
        ZedGraph.PointPair LastPoint;
        ZedGraph.TextObj LastPointLabel;

        StockPointList listSeries;
        PointPairList listIBOV;
        PointPairList listVOLM;
        PointPairList listSeriesLabel;
        PointPairList listBenchLabel;
        PointPairList listLastPoint;
        PointPairList listBuyShares;
        PointPairList listSellShares;

        double MaxVolume = 0;

        TextObj LabelRev;
        TextObj LabelExp;
        TextObj LabelLast;

        LineItem lineSeriesLabel;
        LineItem lineBenchLabel;
        LineItem lineBuyShares;
        LineItem lineSellShares;
        BarItem lineVOLM;
        OHLCBarItem lineSecurity;
        LineItem lineBench;
        LineItem lineLastPoint;
        double iniBench = 1;
        double RTLastBench = 0;

        double iniSeries = 1;

        public string curTicker
        {
            get { return _curTicker; }
            set 
            {
                ChangeTicker(value);
            }
        }
        
        public frmQuickChart()
        {
            InitializeComponent();
            curNDistConn.OnData += new EventHandler(NewMarketData);
        }

        ~frmQuickChart()
        {
            curNDistConn.Disconnect();
        }

        GraphPane VolumePane;

        private void frmQuickChart_Load(object sender, EventArgs e)
        {
            txtHorizLine.Visible = false;
            txtVertLine.Visible = false;

            SetSize();
            tmrRefresh.Start();
            ChartNeedsUpdating = true;
            zgc = zgcQuickChart;

            VolumePane = new GraphPane();
            VolumePane.XAxis.Type = AxisType.DateAsOrdinal;
            VolumePane.Legend.IsVisible = false;
            VolumePane.Border.IsVisible = false;
            //VolumePane.BaseDimension = 1;
            
            zgc.MasterPane.Add(VolumePane);

            Graphics g = CreateGraphics();
            int[] tempint = new int[2];
            float[] tempFloat = new float[2];

            tempint[0] = 1; tempint[1] = 1;
            tempFloat[0] = 3; tempFloat[1] = 1;

            zgc.MasterPane.SetLayout(g, true, tempint, tempFloat);
            zgc.AxisChange();
            zgc.IsSynchronizeXAxes = true; 


            curNDistConn.Subscribe("IBOV", Sources.Bovespa);
            ResizeVolume();

            VolumePane.YAxis.ScaleFormatEvent += new Axis.ScaleFormatHandler(YAxis_ScaleFormatEvent);

            zgcQuickChart.PointValueEvent += new ZedGraphControl.PointValueHandler(MyPointValueHandler);
        }

        //private string MyPointValueHandler(object sender, GraphPane pane, CurveItem curve, int iPt)
        //{
        //    GraphPane ChartPane = zgc.GraphPane;

        //    PointPair ptSeries = ChartPane.CurveList["Series"].Points[iPt];
        //    PointPair ptBench = ChartPane.CurveList["Bench"].Points[iPt];

        //    string valueFormat = "0.00";
        //    if (radTotReturn.Checked) valueFormat = "0.00%";
        //    return DateTime.FromOADate(ptSeries.X).ToString("dd-MMM-yy") + "\r\nSeries " + " " + ptSeries.Y.ToString(valueFormat) + "\r\n" +
        //        "Bench  " + ptBench.Y.ToString(valueFormat) + "\r\n";
        //}

        private void NewMarketData(object sender, EventArgs origE)
        {
            MarketUpdateList curUpdateList = (MarketUpdateList)origE;

            foreach (MarketUpdateItem curUpdateItem in curUpdateList.ItemsList)
            {
                if (curUpdateItem.Ticker == _curTicker)
                {
                    if (!(curUpdateItem.FLID == NestFLIDS.Last && curUpdateItem.ValueDouble == 0))
                    {
                        curMarketItem.Update(curUpdateItem);
                    }

                    if (curUpdateItem.ValueDouble != 0)
                    {
                        
                        if (curUpdateItem.FLID == NestFLIDS.Last && curUpdateItem.ValueDouble != LastPriceDrawn) { TodayBar.Close = curUpdateItem.ValueDouble; ChartNeedsUpdating = true; }
                        if (curUpdateItem.FLID == NestFLIDS.High) { TodayBar.High = curUpdateItem.ValueDouble; ChartNeedsUpdating = true; }
                        if (curUpdateItem.FLID == NestFLIDS.Low) { TodayBar.Low = curUpdateItem.ValueDouble; ChartNeedsUpdating = true; }
                        if (curUpdateItem.FLID == NestFLIDS.Open) { TodayBar.Open = curUpdateItem.ValueDouble; ChartNeedsUpdating = true; }
                        if (curUpdateItem.FLID == NestFLIDS.Volume) { if (lineVOLM != null) lineVOLM.Points[lineVOLM.Points.Count - 1].Y = curUpdateItem.ValueDouble; ChartNeedsUpdating = true; if (curUpdateItem.ValueDouble > MaxVolume) MaxVolume = curUpdateItem.ValueDouble; }

                        if (ChartNeedsUpdating && zgc.GraphPane!= null)
                        {
                            if (zgc.GraphPane.CurveList.Count > 0)
                            {
                                zgc.GraphPane.CurveList["Series"].RemovePoint(zgc.GraphPane.CurveList["Series"].Points.Count - 1);
                                zgc.GraphPane.CurveList["Series"].AddPoint(TodayBar);
                            }
                        }
                    }
                    if (curUpdateItem.FLID == NestFLIDS.Last && curUpdateItem.ValueDouble != LastPriceDrawn)
                    {
                        ChartNeedsUpdating = true;
                        RedrawLastPoints();
                    }
                }
                else if (curUpdateItem.Ticker == "IBOV")
                {
                    if (curUpdateItem.ValueDouble != 0)
                    {
                        if (curUpdateItem.FLID == NestFLIDS.Last)
                        {
                            LastBench = curUpdateItem.ValueDouble;
                            RedrawLastPoints();
                            ChartNeedsUpdating = true;
                        }
                    }
                }
            }
        }

        private double LastBench = 0;

        private void RedrawLastPoints()
        {
            if (zgc.GraphPane != null)
            {
                if (zgc.GraphPane.CurveList.Count > 0)
                {
                    zgc.GraphPane.CurveList["Series"].RemovePoint(zgc.GraphPane.CurveList["Series"].Points.Count - 1);
                    zgc.GraphPane.CurveList["Series"].AddPoint(TodayBar);

                    zgc.GraphPane.CurveList["Bench"].RemovePoint(zgc.GraphPane.CurveList["Bench"].Points.Count - 1);
                    zgc.GraphPane.CurveList["Bench"].AddPoint(new PointPair(DateTime.Today.ToOADate(), LastBench / iniBench * iniSeries));
                    
                    RTLastBench = LastBench;
                }
            }
        }

        private void frmPerfChart_Resize(object sender, EventArgs e)
        {
            SetSize();
            zgcQuickChart.AxisChange();
            zgcQuickChart.Refresh();
        }

        private void SetSize()
        {
            zgcQuickChart.Location = new Point(0, 26);
            zgcQuickChart.Size = new Size(ClientRectangle.Width - 0, ClientRectangle.Height - 26);
        }

        StockPt TodayBar = new StockPt();

        int prevIdSecurity = 0;

        private void LoadChart()
        {
            if (zgc == null)
            {
                ChartLoaded = false;
                return;
            }

            if (_IdSecurity == 0 || zgc.GraphPane == null)// || curMarketItem.Last == 0)
            {
                ChartLoaded = false;
                zgc.Visible = false;
                return;
            }
            else
            {
                zgc.Visible = true;
            }

            MinDate = DateTime.Now;
            MaxDate = DateTime.Now;

            this.Cursor = Cursors.WaitCursor;

            zgc.GraphPane.Border.IsVisible = false;

            GraphPane ChartPane = zgc.GraphPane;

            // Set the Titles
            ChartPane.Title.IsVisible = false;
            ChartPane.XAxis.Title.IsVisible = false;
            ChartPane.YAxis.Title.IsVisible = false;

            PointPairList listDates = new PointPairList();

            listSeries = new StockPointList();
            listIBOV = new PointPairList();
            listVOLM = new PointPairList();
            listBuyShares = new PointPairList();
            listSellShares = new PointPairList();
            MaxVolume=0;

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                string curTicker = curConn.Execute_Query_String("SELECT NestTicker FROM NESTDB.dbo.TB001_SECURITIES WHERE IDSECURITY=" + _IdSecurity);

                this.Text = "Simple Chart - " + curTicker;

                string curExchange = curConn.Execute_Query_String("SELECT IdPrimaryExchange FROM NESTDB.dbo.TB001_SECURITIES WHERE IDSECURITY=" + _IdSecurity);
                int curType = curConn.Return_Int("SELECT IdInstrument FROM NESTDB.dbo.TB001_SECURITIES WHERE IDSECURITY=" + _IdSecurity);

                int IdSecSource = 24;

                int IdBench = _IdSecurity;
                string TableName = "";
                int TipoPreco = 1;
                int Id_Source = 1;

                if (curExchange == "2")
                {
                    IdBench = 1073;
                    TipoPreco = 101;
                    TableName = "NESTDB.dbo.Tb050_Preco_Acoes_Onshore";
                }
                else
                {
                    IdBench = 5310;
                    TipoPreco = 1;
                    Id_Source = 1;
                    TableName = "NESTDB.dbo.Tb051_Preco_Acoes_Offshore";
                }

                if (curExchange == "1")
                {
                    IdBench = 5049;
                    TipoPreco = 30;
                    Id_Source = 13;
                    TableName = "NESTDB.dbo.Tb059_Precos_Futuros";
                }

                if (curType == 10)
                {
                    IdBench = 1073;
                    TipoPreco = 1;
                    Id_Source = 1;
                    IdSecSource = 1;
                    TableName = "NESTDB.dbo.Tb053_Precos_Indices";
                }

                string StringSQL;
                SqlDataReader drPoints;

                DateTime IniDate = new DateTime(1900, 01, 01);
                if (radMTD.Checked) IniDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1);
                if (radYTD.Checked) IniDate = new DateTime(DateTime.Today.Year - 1, 12, 31);
                if (rad3m.Checked) IniDate = DateTime.Today.AddDays(-90);
                if (rad12m.Checked) IniDate = DateTime.Today.AddMonths(-12);
                if (radCustom.Checked) IniDate = CustomMinDate;

                IniDate = curConn.Return_DateTime("SELECT MAX(SrDate) FROM Tb053_Precos_Indices WHERE IdSecurity=1073 AND SrDate<='" + IniDate.ToString("yyyy-MM-dd") + "'");

                double LastTRIndex = 1;// curConn.Return_Double("SELECT TOP 1 SrValue FROM " + TableName + " WHERE IdSecurity=" + _IdSecurity + " AND SrType=101 AND Source=1 ORDER BY SrDate DESC");
                double LastTRIndexBench = curConn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb053_Precos_Indices WHERE IdSecurity=" + IdBench + " AND SrType=101 AND Source=1 ORDER BY SrDate DESC");

                TipoPreco = 1;

                StringSQL = " SELECT B.SrDate, A.SrValue AS Series, B.SrValue AS Bench, COALESCE(C.SrValue, 0) AS OP, COALESCE(D.SrValue, 0) AS HI, COALESCE(E.SrValue, 0) AS LO, COALESCE(F.SrValue, 1) AS CL, COALESCE(G.SrValue, 1) AS VOLM, COALESCE(H.BuyQuant,0) AS BuyQuant, COALESCE(H.BuyPx,0) AS BuyPx, COALESCE(I.SellQuant, 0) AS SellQuant, COALESCE(I.SellPx,0) AS SellPx" +
                        " FROM   " +
                        " ( " +
                        " 	SELECT SrDate, SrValue FROM " + TableName + " WHERE IdSecurity=" + _IdSecurity + " AND SrType=" + TipoPreco + " AND Source=CASE WHEN SrDate<='2013-12-10' THEN " + Id_Source + " ELSE " + IdSecSource + " END " +
                        " ) A   " +
                        " RIGHT JOIN    " +
                        " ( " +
                        " SELECT SrDate, SrValue FROM NESTDB.dbo.Tb053_Precos_Indices WHERE IdSecurity=1073 AND SrType=1 AND SrDate>='" + IniDate.ToString("yyyy-MM-dd") + "' " +
                        " ) B  " +
                        " ON A.SrDate=B.SrDate   " +
                        " LEFT JOIN( SELECT SrDate, SrValue FROM " + TableName + " WHERE IdSecurity=" + _IdSecurity + " AND SrType=8 AND Source=CASE WHEN SrDate<='2013-12-10' THEN " + Id_Source + " ELSE " + IdSecSource + " END) C ON A.SrDate=C.SrDate  " +
                        " LEFT JOIN( SELECT SrDate, SrValue FROM " + TableName + " WHERE IdSecurity=" + _IdSecurity + " AND SrType=4 AND Source=CASE WHEN SrDate<='2013-12-10' THEN " + Id_Source + " ELSE " + IdSecSource + " END) D ON A.SrDate=D.SrDate   " +
                        " LEFT JOIN( SELECT SrDate, SrValue FROM " + TableName + " WHERE IdSecurity=" + _IdSecurity + " AND SrType=3 AND Source=CASE WHEN SrDate<='2013-12-10' THEN " + Id_Source + " ELSE " + IdSecSource + " END) E ON A.SrDate=E.SrDate   " +
                        " LEFT JOIN( SELECT SrDate, SrValue FROM " + TableName + " WHERE IdSecurity=" + _IdSecurity + " AND SrType=1 AND Source=CASE WHEN SrDate<='2013-12-10' THEN " + Id_Source + " ELSE " + IdSecSource + " END) F ON A.SrDate=F.SrDate   " +
                        " LEFT JOIN( SELECT SrDate, SrValue FROM " + TableName + " WHERE IdSecurity=" + _IdSecurity + " AND SrType=11 AND Source=CASE WHEN SrDate<='2013-12-10' THEN " + Id_Source + " ELSE " + IdSecSource + " END) G ON A.SrDate=G.SrDate   " +
                        " LEFT JOIN  " +
                         " ( " +
                        " 	SELECT Data_Trade,sum(Quantidade_Trade) AS BuyQuant,sum(Quantidade_Trade*Preco_Trade)/sum(Quantidade_Trade) AS BuyPx " +
                        " 	FROM Tb063_Trades_All  A LEFT JOIN VW_PortAccounts B ON A.Id_Account=B.Id_Account " +
                        " 	WHERE Id_Ativo=" + _IdSecurity + " AND Quantidade_Trade>0 AND A.Id_Portfolio=10 AND Id_Port_Type=2  " +
                        " 	GROUP BY Data_Trade " +
                        "  ) H " +
                        "  ON A.SrDate=H.Data_Trade " +
                        " LEFT JOIN  " +
                         " ( " +
                        " 	SELECT Data_Trade,sum(Quantidade_Trade) AS SellQuant,sum(Quantidade_Trade*Preco_Trade)/sum(Quantidade_Trade) AS SellPx " +
                        " 	FROM Tb063_Trades_All  A LEFT JOIN VW_PortAccounts B ON A.Id_Account=B.Id_Account " +
                        " 	WHERE Id_Ativo=" + _IdSecurity + " AND Quantidade_Trade<0 AND A.Id_Portfolio=10 AND Id_Port_Type=2 " +
                        " 	GROUP BY Data_Trade " +
                        "  ) I " +
                        "  ON A.SrDate=I.Data_Trade " +
                        " UNION  " +
                        " SELECT CONVERT(varchar,getdate(),102), SrValue" + ///NESTDB.dbo.FCN_GET_PRICE_Value_Only(" + _IdSecurity + ",getdate(),1,0,2,0,0)*NESTDB.dbo.FCN_GET_PRICE_Value_Only(" + _IdSecurity + ",getdate(),101,0,2,0,0)" + 
                        ", 0 ,0,0,0,0,0,0,0,0,0 " +
                        " FROM NESTRT.dbo.Tb065_Ultimo_Preco   " +
                        " WHERE IdSecurity=" + _IdSecurity + " AND SrType=1  ORDER BY SrDate";


                drPoints = curConn.Return_DataReader(StringSQL);

                if (curMarketItem.Close == 0)
                {
                    curMarketItem.Close = curConn.Return_Double("SELECT * FROM [dbo].[FCN_GET_PRICE](" + _IdSecurity + ",CONVERT(varchar,getdate(),102),1,0,2,1,0,0)");
                }

                bool FirstVal = true;

                double prevPoint = 0;
                double prevBench = 0;

                bool isTR = false;
                if (radTotReturn.Checked) isTR = true;

                double LastBench = 0;
                double FinalBench = 0;

                while (drPoints.Read())
                {
                    if (FirstVal && drPoints["Series"].ToString() != "")
                    {
                        iniSeries = Convert.ToDouble(drPoints["Series"]);
                        iniBench = Convert.ToDouble(drPoints["Bench"]);
                        curMinDate = Convert.ToDateTime(drPoints["SrDate"]);

                        FirstVal = false;
                    }
                    DateTime TempDate = Convert.ToDateTime(drPoints["SrDate"]);

                    if (drPoints["Series"].ToString() != "")
                    {
                        double tempSeries = Convert.ToDouble(drPoints["Series"]);
                        double tempIBOV = Convert.ToDouble(drPoints["Bench"]);

                        double tempOP = Convert.ToDouble(drPoints["OP"]);
                        double tempHI = Convert.ToDouble(drPoints["HI"]);
                        double tempLO = Convert.ToDouble(drPoints["LO"]);
                        double tempCL = Convert.ToDouble(drPoints["CL"]);
                        double tempVOLM = Convert.ToDouble(drPoints["VOLM"]);
                        double tempBuyPx = Convert.ToDouble(drPoints["BuyPx"]);
                        double tempSellPx = Convert.ToDouble(drPoints["SellPx"]);
                        double tempBuyQuant = Convert.ToDouble(drPoints["BuyQuant"]);
                        double tempSellQuant = Convert.ToDouble(drPoints["SellQuant"]);

                        if (tempVOLM > MaxVolume) MaxVolume = tempVOLM;

                        if (TempDate > MaxDate) { MaxDate = TempDate; };
                        if (TempDate < MinDate) { MinDate = TempDate; };

                        double tempPoint = prevPoint;
                        if (Convert.ToDouble(drPoints["Series"]) != 0)
                        {
                            if (isTR)
                                tempPoint = tempSeries / iniSeries - 1;
                            else
                                tempPoint = tempSeries;// / LastTRIndex * curMarketItem.Close;
                            
                            if(tempPoint != 0) prevPoint = tempPoint;
                        }

                        double tempBench = prevBench;
                        if (Convert.ToDouble(drPoints["Bench"]) != 0)
                        {
                            if (isTR)
                                tempBench = tempIBOV / iniBench - 1;
                            else
                                tempBench = tempIBOV / iniBench * iniSeries;// (iniSeries / LastTRIndex * curMarketItem.Close);

                            
                            if (tempBench != 0) prevBench = tempBench;
                        }

                        StockPt pt = new StockPt(TempDate.ToOADate(), tempHI / tempCL * tempPoint, tempLO / tempCL * tempPoint, tempOP / tempCL * tempPoint, tempPoint, 1);

                        if (TempDate != DateTime.Today.Date)
                        {
                            listSeries.Add(pt);
                            listIBOV.Add(TempDate.ToOADate(), tempBench);
                            listVOLM.Add(TempDate.ToOADate(), tempVOLM);
                            //if (tempBuyPx != 0) 
                            listBuyShares.Add(TempDate.ToOADate(), tempBuyPx, tempBuyQuant.ToString("#,###"));
                            listSellShares.Add(TempDate.ToOADate(), tempSellPx, tempSellQuant.ToString("#,###"));
                        }

                        if (tempBench != 0) FinalBench = tempIBOV;

                        LastBench = tempBench;
                    }
                }

                curMaxDate = MaxDate;
                
                if (isTR)
                { }
                else
                {
                    if(listSeries.Count>0) LastSeriesPrice = listSeries[listSeries.Count - 1].Y;

                    //listSeries.Add(DateTime.Now.Date.ToOADate(), LastPrice);
                    //listIBOV.Add(DateTime.Now.Date.ToOADate(), LastBench);
                }

                if (_IdSecurity != prevIdSecurity) TodayBar = new StockPt();
                prevIdSecurity = _IdSecurity;

                TodayBar.Date = DateTime.Now.Date.ToOADate();
                TodayBar.Close = prevPoint;
                listSeries.Add(TodayBar);

                listVOLM.Add(DateTime.Now.Date.ToOADate(), 0);

                if (RTLastBench == 0) RTLastBench = FinalBench;
                if(RTLastBench!=0) listIBOV.Add(DateTime.Now.Date.ToOADate(), RTLastBench / iniBench * iniSeries);

                // CREATE CURVES

                ChartPane.CurveList.Clear();
                VolumePane.CurveList.Clear();
                //ChartPane.GraphObjList.Clear();
                
                lineSecurity = null;
                lineBench = null;
                lineVOLM = null;
                lineBuyShares = null;
                lineSellShares = null;

                listLastPoint = null;
                listSeriesLabel = null;
                listBenchLabel = null;
                

                //if (listLastPoint == null)
                //{
                //    listLastPoint = new PointPairList();
                //    listLastPoint.Add(DateTime.Now.Date.ToOADate(), LastPrice);
                //    lineLastPoint = ChartPane.AddCurve("LastPoint", listLastPoint, Color.FromArgb(102, 153, 255), SymbolType.Circle);
                //    lineLastPoint.Line.IsVisible = false;
                //    lineLastPoint.Symbol.Fill = new Fill(Color.FromArgb(102, 153, 255));
                //    lineLastPoint.Symbol.Size = 7;

                //    PointPair pt = listLastPoint[0];
                //    LabelLast = new TextObj(pt.Y.ToString("f2"), pt.X - 5, pt.Y - 5, CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                //    LabelLast.ZOrder = ZOrder.A_InFront;
                //    LabelLast.FontSpec.Angle = 0;
                //    LabelLast.FontSpec.IsBold = true;
                //    LabelLast.FontSpec.FontColor = Color.FromArgb(102, 153, 255);
                //    LabelLast.FontSpec.Border.IsVisible = false;
                //    LabelLast.FontSpec.Fill.IsVisible = false; 

                //    ChartPane.GraphObjList.Add(LabelLast);
                //}

                //if (listSeriesLabel == null)
                //{
                //    listSeriesLabel = new PointPairList();
                //    listSeriesLabel.Add(new PointPair(listSeries[listSeries.Count - 1].X, LastPrice));
                //    lineSeriesLabel = ChartPane.AddCurve("SeriesLabel", listSeriesLabel, Color.FromArgb(102, 153, 255), SymbolType.Circle);
                //    lineSeriesLabel.Line.IsVisible = false;
                //    lineSeriesLabel.Symbol.Fill = new Fill(Color.FromArgb(102, 153, 255));
                //    lineSeriesLabel.Symbol.Size = 7;


                //    LabelRev = new TextObj("", 0, 0, CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                //    LabelRev.ZOrder = ZOrder.A_InFront;
                //    LabelRev.FontSpec.Angle = 0;
                //    LabelRev.FontSpec.IsBold = true;
                //    LabelRev.FontSpec.FontColor = Color.FromArgb(102, 153, 255);
                //    LabelRev.FontSpec.Border.IsVisible = false;
                //    LabelRev.FontSpec.Fill.IsVisible = false;
                //    ChartPane.GraphObjList.Add(LabelRev);
                //}

                //if (listBenchLabel == null)
                //{
                //    listBenchLabel = new PointPairList();
                //    listBenchLabel.Add(new PointPair(0, 0));
                //    lineBenchLabel = ChartPane.AddCurve("BenchLabel", listBenchLabel, Color.FromArgb(102, 153, 255), SymbolType.Circle);
                //    lineBenchLabel.Line.IsVisible = false;
                //    lineBenchLabel.Symbol.Fill = new Fill(Color.Gray);
                //    lineBenchLabel.Symbol.Size = 7;

                //    LabelExp = new TextObj("", 0, 0, CoordType.AxisXYScale, AlignH.Left, AlignV.Center);
                //    LabelExp.ZOrder = ZOrder.A_InFront;
                //    LabelExp.FontSpec.Angle = 0;
                //    LabelExp.FontSpec.IsBold = true;
                //    LabelExp.FontSpec.FontColor = Color.Gray;
                //    LabelExp.FontSpec.Border.IsVisible = false;
                //    LabelExp.FontSpec.Fill.IsVisible = false;
                //    ChartPane.GraphObjList.Add(LabelExp);
                //}

                if (lineSecurity == null)
                {
                    //lineSecurity = ChartPane.AddOHLCBar("listSeries", listSeries, Color.FromArgb(102, 153, 255));
                    lineSecurity = ChartPane.AddOHLCBar("listSeries", listSeries, Color.Black);
                    if (lineSecurity.Points.Count < 100)
                    {
                        lineSecurity.Bar.Size = 10.0F;
                        lineSecurity.Bar.Width = 2.0F;
                    }
                    else
                    {
                        //lineSecurity.Bar.Size = 5.0F;
                        lineSecurity.Bar.Width = 1.0F;
                    }
                    lineSecurity.Label.Text = "Series";
                    
                }

                if (lineSellShares == null)
                {
                    lineSellShares = ChartPane.AddCurve("listSellShares", listSellShares, Color.Red, SymbolType.Circle);
                    lineSellShares.Label.Text = "BuyShares";
                    lineSellShares.Symbol.Fill = new Fill(Color.Red);
                    lineSellShares.Line.IsVisible = false;
                    lineSellShares.Symbol.Size = 6;
                }

                if (lineBuyShares == null)
                {
                    lineBuyShares = ChartPane.AddCurve("listBuyShares", listBuyShares, Color.Green, SymbolType.Circle);
                    lineBuyShares.Label.Text = "BuyShares";
                    lineBuyShares.Symbol.Fill = new Fill(Color.Green);
                    lineBuyShares.Line.IsVisible = false;
                    lineBuyShares.Symbol.Size = 6;
                }

                if (lineBench == null)
                {
                    lineBench = ChartPane.AddCurve("listIBOV", listIBOV, Color.Gray, SymbolType.None);
                    lineBench.Label.Text = "Bench";
                    lineBench.Line.Fill = new Fill(Color.FromArgb(25, 150, 150, 150));
                    lineBench.Line.Width = 2.0F;
                }

                if (lineVOLM == null)
                {
                    lineVOLM = VolumePane.AddBar("listVOLM", listVOLM, Color.LightGray);
                    lineVOLM.Label.Text = "Volume";
                    lineVOLM.Bar.Fill = new Fill(Color.LightGray, Color.LightGray, -45F);
                    //lineVOLM.Bar.Width = 2.0F;
                }


                // Legend and other
                ChartPane.Legend.Position = ZedGraph.LegendPos.Bottom;

                ChartPane.XAxis.Type = AxisType.DateAsOrdinal;

                if (isTR)
                    ChartPane.YAxis.Scale.Format = "0.00%";
                else
                    ChartPane.YAxis.Scale.Format = "0.00";
                
                ChartPane.XAxis.MajorGrid.IsVisible = true;
                ChartPane.YAxis.MajorGrid.IsVisible = true;
                ChartPane.XAxis.MajorGrid.Color = Color.Gray;
                ChartPane.YAxis.MajorGrid.Color = Color.Gray;

                ChartLoaded = true;

                zgc.GraphPane.GraphObjList.Clear();
            }
            

            ChartPane.IsFontsScaled = false;
            ChartPane.XAxis.Scale.FontSpec.Size = 10;
            ChartPane.YAxis.Scale.FontSpec.Size = 10;
            ChartPane.Legend.FontSpec.Size = 7;

            VolumePane.IsFontsScaled = false;
            VolumePane.YAxis.Scale.MagAuto = false;
            VolumePane.XAxis.Scale.FontSpec.Size = 10;
            VolumePane.YAxis.Scale.FontSpec.Size = 10;
            VolumePane.YAxis.Scale.Format = "#,##0.0";
            
            ChartPane.XAxis.Scale.MinorUnit = DateUnit.Day;
            ChartPane.XAxis.Scale.MinorUnit = DateUnit.Day;

            

            zgc.IsShowPointValues = true;
            zgc.PointValueFormat = "0.00";
            zgc.PointDateFormat = "dd-MMM-yy";

            UpdateChart();

            LastPriceDrawn = LastPrice;

            ResizeVolume();

            this.Cursor = Cursors.Default;
        }

        private void UpdateChart()
        {
            GraphPane ChartPane = zgc.GraphPane;

            if (ChartPane.CurveList["Series"].Points.Count < 5) return;

            //if (radMTD.Checked)
            //{
            //    ChartPane.XAxis.Scale.Min = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1).ToOADate();
            //    ChartPane.XAxis.Scale.Max = MaxDate.ToOADate() + 5;
            //}
            //else if (rad3m.Checked)
            //{
            //    ChartPane.XAxis.Scale.Min = DateTime.Today.AddDays(-90).ToOADate();
            //    ChartPane.XAxis.Scale.Max = MaxDate.ToOADate() + Math.Round(((MaxDate.ToOADate() - ChartPane.XAxis.Scale.Min) * 0.1));
            //}
            //else if (rad12m.Checked)
            //{
            //    ChartPane.XAxis.Scale.Min = DateTime.Today.AddMonths(-12).ToOADate();
            //    ChartPane.XAxis.Scale.Max = MaxDate.ToOADate() + Math.Round(((MaxDate.ToOADate() - ChartPane.XAxis.Scale.Min) * 0.1));
            //}
            //else if (radYTD.Checked)
            //{
            //    ChartPane.XAxis.Scale.Min = 0;// new DateTime(DateTime.Today.Year - 1, 12, 31).ToOADate();
            //    ChartPane.XAxis.Scale.Max = 100;// MaxDate.ToOADate() + Math.Round(((MaxDate.ToOADate() - ChartPane.XAxis.Scale.Min) * 0.1));
            //}
            //else if (radALL.Checked)
            //{
            //    ChartPane.XAxis.Scale.Min = MinDate.ToOADate();
            //    ChartPane.XAxis.Scale.Max = MaxDate.ToOADate() + Math.Round(((MaxDate.ToOADate() - ChartPane.XAxis.Scale.Min) * 0.1));
            //}
            if (radCustom.Checked)
            {
                ChartPane.XAxis.Scale.Min = CustomMinDate.Subtract(MinDate).TotalDays;
                //ChartPane.XAxis.Scale.Max = MaxDate.ToOADate() + Math.Round(((MaxDate.ToOADate() - ChartPane.XAxis.Scale.Min) * 0.1));
            }

            //ChartPane.XAxis.Scale.Min = 0;
            ChartPane.XAxis.Scale.Max = ChartPane.CurveList["Series"].Points.Count + 2;
 

            double MaxY = 0;
            double MinY = double.PositiveInfinity;

            for (int i = 0; i < ChartPane.CurveList["Series"].Points.Count; i++)
            {
                //if (ChartPane.CurveList["Series"].Points[i].X >= ChartPane.XAxis.Scale.Min && ChartPane.CurveList["Series"].Points[i].X <= ChartPane.XAxis.Scale.Max)
                {
                    if (ChartPane.CurveList["Series"].Points[i].Y > MaxY) MaxY = ChartPane.CurveList["Series"].Points[i].Y;
                    if (ChartPane.CurveList["Series"].Points[i].Y < MinY && ChartPane.CurveList["Series"].Points[i].Y != 0) MinY = ChartPane.CurveList["Series"].Points[i].Y;
                }
            }

            for (int i = 0; i < ChartPane.CurveList["Bench"].Points.Count; i++)
            {
                //if (ChartPane.CurveList["Bench"].Points[i].X >= ChartPane.XAxis.Scale.Min && ChartPane.CurveList["Bench"].Points[i].X <= ChartPane.XAxis.Scale.Max)
                {
                    if (ChartPane.CurveList["Bench"].Points[i].Y > MaxY) MaxY = ChartPane.CurveList["Bench"].Points[i].Y;
                    if (ChartPane.CurveList["Bench"].Points[i].Y < MinY) MinY = ChartPane.CurveList["Bench"].Points[i].Y;
                }
            }

            double ChartLastValue = 0;

            if (!radTotReturn.Checked)
            {
                ChartPane.YAxis.Scale.Max = MaxY * 1.1;
                ChartPane.YAxis.Scale.Min = MinY * 0.9;
                if (ChartPane.CurveList["Series"].Points.Count > 0)
                {
                    //ChartLastValue = ChartPane.CurveList["Series"].Points[ChartPane.CurveList["Series"].Points.Count - 1].Y = LastPrice;
                    //ChartPane.CurveList["Series"].Points[ChartPane.CurveList["Series"].Points.Count - 1].Y = ChartLastValue;
                }
            }
            else
            {
                ChartPane.YAxis.Scale.Max = Math.Round(MaxY / 0.05) * 0.05 + 0.05;
                ChartPane.YAxis.Scale.Min = Math.Round(MinY / 0.05) * 0.05 - 0.05;
                if (ChartPane.CurveList["Series"].Points.Count > 2)
                {
                    //ChartLastValue = (1 + ChartPane.CurveList["Series"].Points[ChartPane.CurveList["Series"].Points.Count - 2].Y) * (LastPrice / curMarketItem.Close) - 1;
                    //ChartPane.CurveList["Series"].Points[ChartPane.CurveList["Series"].Points.Count - 1].Y = ChartLastValue;
                }
            }
            
            //if (ChartPane.GraphObjList.Count > 0)
            //{
            //    double YOffSet = 0;
            //    double XOffSet = 0;

            //    if (!radTotReturn.Checked)
            //    {
            //        ((TextObj)ChartPane.GraphObjList[0]).Text = ChartLastValue.ToString("0.00");
            //        YOffSet = 0;
            //        XOffSet = 0;// (ChartPane.XAxis.Scale.Max - MaxDate.ToOADate());
            //    }
            //    else
            //    {
            //        ((TextObj)ChartPane.GraphObjList[0]).Text = ChartLastValue.ToString("0.00%");
            //        YOffSet = 0;
            //        XOffSet = 0; //(ChartPane.XAxis.Scale.Max - MaxDate.ToOADate());
            //    }

            //    //ChartPane.GraphObjList[0].Location.X = ChartPane.CurveList["Series"].Points[ChartPane.CurveList["Series"].Points.Count - 1].X - XOffSet;
            //    //ChartPane.GraphObjList[0].Location.Y = ChartPane.CurveList["Series"].Points[ChartPane.CurveList["Series"].Points.Count - 1].Y + YOffSet;
            //}

            RedrawLastPoints();

            //ChartPane.YAxis.Scale.MinAuto = true;
            //ChartPane.YAxis.Scale.MaxAuto = true;
            //ChartPane.XAxis.Scale.MinAuto = true;
            //ChartPane.XAxis.Scale.MaxAuto = true;
            
            zgc.AxisChange();
            zgc.Refresh();
            zgc.Invalidate();

            LastPriceDrawn = LastPrice;
        }

        private void cmbView_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateChart();
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            if (ChartNeedsUpdating && !ChartIsRunning)
            {
                ChartIsRunning = true;
                ChartNeedsUpdating = false;
                if (ChartLoaded)
                    UpdateChart();
                else
                    LoadChart();
                
                ChartIsRunning = false;
            }
        }
        
        private int getIdSecurity(string Ticker)
        {
            string SQLExpresion = "SELECT IDSECURITY FROM NESTDB.DBO.TB001_SECURITIES (NOLOCK) WHERE EXCHANGETICKER = '" + Ticker + "'";

            int result = 0;

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                result = curConn.Return_Int(SQLExpresion);
            }

            return result;
        }

        private void radRedraw_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
            {
                LoadChart();
            }
        }

        private void ChangeTicker(string newTicker)
        {
            if (newTicker != _curTicker)
            {
                if (_curTicker != "")
                {
                    curNDistConn.UnSubscribe(_curTicker, curSource);
                }

                _curTicker = newTicker;
                int newIdSecurity = getIdSecurity(newTicker);
                _IdSecurity = newIdSecurity;
                _curIdInstrument = GlobalVars.Instance.getIdInstrument(_curTicker);

                curMarketItem = new MarketDataItem();

                TodayBar = new StockPt();

                if (newIdSecurity > 0)
                {
                    zgcQuickChart.Visible = false;
                    LoadChart();
                }

                curSource = GlobalVars.Instance.getDataSource(_curTicker);
            }

            curNDistConn.Subscribe(_curTicker, curSource);
            
        }

        #region MouseDragDrop

        private void zgcQuickChart_DragDrop(object sender, DragEventArgs e)
        {
            if (DragTicker != "")
            {
                ChangeTicker(DragTicker);
                DragTicker = "";
            }
        }

        private void zgcQuickChart_DragOver(object sender, DragEventArgs e)
        {
            //Object co = System.Runtime.InteropServices.Marshal.CreateWrapperOfType(e.Data, e.GetType());
            string tempVal = e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();

            //if (e.Data.GetType() == typeof(System.Windows.Forms.DataObject))
            {
                string DroppedIdTicker = e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();
                if (DroppedIdTicker != null)
                {
                    if (DroppedIdTicker.Split('\t')[0] == "DRAGITEM")
                        e.Effect = DragDropEffects.Move;
                    else
                        e.Effect = DragDropEffects.None;
                }
                else
                    e.Effect = DragDropEffects.None;
            }
        }

        private void zgcQuickChart_DragEnter(object sender, DragEventArgs e)
        {
            string tempVal = e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString();
            if (tempVal.Contains("DRAGITEM"))
            {
                DragTicker = e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString().Split('\t')[1];
            }
        }

        #endregion

        #region MouseHovering

        bool MouseButtonDown = false;
        Point MouseDownPoint = new Point(0, 0);


        private void zgcQuickChart_MouseHover(object sender, EventArgs e)
        {
            
        }

        private bool zgcQuickChart_MouseUpEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            if (TrendMode == 1)
            {
                TrendMode = 2;
            }
            else if (TrendMode == 2)
            {
                TrendMode = 3;
                cmdDrawTrend.Enabled = true;
            }


            MouseButtonDown = false;

            if (radCustom.Checked && CustomMinDate != MDownMinDate)
            {
                if (zgc.GraphPane.XAxis.Scale.Min > 0)
                {
                    StockPt curPoint = (StockPt)lineSecurity[(int)zgc.GraphPane.XAxis.Scale.Min];
                    CustomMinDate = DateTime.FromOADate(curPoint.Date);
                }
                LoadChart();
            }

            return default(bool);
        }

        DateTime curMinDate = new DateTime(1900, 01, 01);
        DateTime curMaxDate = new DateTime(1900, 01, 01);

        private bool zgcQuickChart_MouseDownEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            radCustom.Enabled = true;
            MouseButtonDown = true;
            MouseDownPoint = new Point(e.X, e.Y);
            MDownMaxDate = curMaxDate;// DateTime.FromOADate(zgc.GraphPane.XAxis.Scale.Max).AddDays(-3);
            MDownMinDate = curMinDate;//DateTime.FromOADate(zgc.GraphPane.XAxis.Scale.Min).AddDays(1);
            
            //MDownMaxDate
            //MDownMinDate

            if (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Shift)
            {
                if (this.ControlBox == true)
                {
                    this.ControlBox = false;
                    this.Text = string.Empty;
                }
                else
                {
                    this.ControlBox = true;
                    this.Text = "Simple Chart";
                }
            }
            return default(bool);
        }

        StockPt curMousePt = new StockPt();
        int curMouseIndex = 0;

        private void zgcQuickChart_MouseMove(object sender, MouseEventArgs e)
        {
            double curX=0, curY=0;
            double drawX, drawY;

            PointF mousePt = new PointF(e.X, e.Y);

            GraphPane pane = ((ZedGraphControl)sender).MasterPane.FindChartRect(mousePt);
            if (pane != null)
            {
                pane.ReverseTransform(mousePt, out curX, out curY);
                if (VolumePane.Chart.Rect.Contains(new PointF(e.X, e.Y)))
                    if (curY < 2000000)
                        curY = (int)(curY / 1000) * 1000;
                    else
                        curY = (int)(curY / 1000000) * 1000000;
                else
                    curY = Math.Round(curY, 2);
                mousePt = pane.GeneralTransform(Math.Round(curX, 0), curY, CoordType.AxisXYScale);
                pane.ReverseTransform(mousePt, out curX, out curY);
            }

            drawX = mousePt.X;
            drawY = mousePt.Y;

            if (zgcQuickChart.GraphPane.Chart.Rect.Contains(new PointF(e.X, e.Y)) || VolumePane.Chart.Rect.Contains(new PointF(e.X, e.Y)))
            {
                txtHorizLine.SetBounds((int)drawX, (int)zgc.GraphPane.Chart.Rect.Location.Y + 28, 1, (int)zgc.GraphPane.Rect.Y + (int)zgc.GraphPane.Rect.Height + (int)VolumePane.Rect.Height - 26);
                txtVertLine.SetBounds((int)zgc.GraphPane.Chart.Rect.Location.X, (int)drawY + zgcQuickChart.Location.Y, (int)zgc.GraphPane.Chart.Rect.Width, 1);

                labPrice.Left = 10;
                labPrice.Top = (int)mousePt.Y + 15;

                labDate.Left = (int)mousePt.X - 20;
                labDate.Top = (int)VolumePane.Rect.Location.Y - 25;

                string NumFormat = "#,##0.00";
                if (VolumePane.Chart.Rect.Contains(new PointF(e.X, e.Y))) { NumFormat = "#,##0"; if (curY > 2000000) curY = Math.Round(curY / 1000000, 0) * 1000000; }

                labPrice.Text = Math.Round(curY,2).ToString(NumFormat);
                if (curX <= listSeries.Count && curX > 0)
                {
                    labDate.Text = DateTime.FromOADate(listSeries[(int)Math.Round(curX, 0) - 1].X).ToString("dd-MMM");
                    curMousePt = (StockPt)listSeries[(int)Math.Round(curX, 0) - 1];
                    curMouseIndex = (int)Math.Round(curX, 0);
                    bool isAbove = true;
                    if (curY < curMousePt.Low) isAbove = false;
                    DrawLine(isAbove);
                }
                else
                    labDate.Text = "";

                txtHorizLine.Visible = true;
                txtVertLine.Visible = true;
                labPrice.Visible = true;
                //labVolume.Visible = true;
                labDate.Visible = true;
            }
            else
            {
                txtHorizLine.Visible = false;
                txtVertLine.Visible = false;
                labPrice.Visible = false;
                //labVolume.Visible = false;
                labDate.Visible = false;
            }

            zgc.Invalidate();
            zgc.Refresh();

            if (MouseButtonDown)
            {
                Point curPoint = new Point(e.X, e.Y);

                int Xoffset = curPoint.X - MouseDownPoint.X;
                int Yoffset = curPoint.Y - MouseDownPoint.Y;

                if (Math.Abs(Xoffset) > Math.Abs(Yoffset))
                {
                    if (Xoffset != 0)
                    {
                        if (!radCustom.Checked) radCustom.Checked = true;

                        int curscale = (int)MDownMaxDate.Subtract(MDownMinDate).TotalDays;
                        if (curscale < 100) curscale = 100;
                        int Xtotal = zgc.Width;

                        int DaysOffset = (int)(-Xoffset / (double)Xtotal * curscale);
                        CustomMinDate = MDownMinDate.AddDays(DaysOffset);
                        if (CustomMinDate.AddDays(5) >= MaxDate) CustomMinDate = MaxDate.AddDays(-5);
                        UpdateChart();
                        Console.WriteLine(CustomMinDate);
                    }
                }
            }

            
        }

        #endregion

        private bool zgcQuickChart_MouseMoveEvent(ZedGraphControl sender, MouseEventArgs e)
        {
            PointF mousePt = new PointF(e.X, e.Y);
            GraphPane pane = sender.MasterPane.FindChartRect(mousePt);
            if (pane != null)
            {
                double x, y;
                pane.ReverseTransform(mousePt, out x, out y);
                PointPair labelPoint;
                for (int i = 1; i < listSeries.Count; i++)
                {
                    if (listSeries[i].X > x)
                    {
                        //if (Math.Abs(listSeries[i].X - x) < Math.Abs(listSeries[i - 1].X - x))
                        //{
                        //    labelPoint = listSeries[i];
                        //}
                        //else
                        //{
                        //    labelPoint = listSeries[i - 1];
                        //}
                        //listSeriesLabel[0].X = labelPoint.X;
                        //listSeriesLabel[0].Y = labelPoint.Y;
                        //LabelRev.Location.X = labelPoint.X;
                        //LabelRev.Location.Y = labelPoint.Y + (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) / 40;
                        //if (!radTotReturn.Checked)
                        //{
                        //    LabelRev.Text = labelPoint.Y.ToString("#,##0.00");
                        //}
                        //else
                        //{
                        //    LabelRev.Text = labelPoint.Y.ToString("0.00%");
                        //}
                        break;
                    }
                }
                for (int i = 1; i < listIBOV.Count; i++)
                {
                    if (listIBOV[i].X > x)
                    {
                        //if (Math.Abs(listIBOV[i].X - x) < Math.Abs(listIBOV[i - 1].X - x))
                        //{
                        //    labelPoint = listIBOV[i];
                        //}
                        //else
                        //{
                        //    labelPoint = listIBOV[i - 1];
                        //}
                        //listBenchLabel[0].X = labelPoint.X;
                        //listBenchLabel[0].Y = labelPoint.Y;
                        //LabelExp.Location.X = labelPoint.X;
                        //LabelExp.Location.Y = labelPoint.Y - (pane.YAxis.Scale.Max - pane.YAxis.Scale.Min) / 40; ;
                        //if (!radTotReturn.Checked)
                        //{
                        //    LabelExp.Text = labelPoint.Y.ToString("#,##0.00");
                        //}
                        //else
                        //{
                        //    LabelExp.Text = labelPoint.Y.ToString("0.00%");
                        //}
                        break;
                    }
                }
                zgc.Invalidate();
            }
            return false;

        }

        private void zgcQuickChart_Resize(object sender, EventArgs e)
        {
            ResizeVolume();
        }

        private void ResizeVolume()
        {
            if (VolumePane != null)
            {
                GraphPane ChartPane = zgc.GraphPane;
                float tempHeight = ChartPane.Rect.Y + ChartPane.Rect.Height;
                VolumePane.Chart.Rect = new RectangleF(ChartPane.Chart.Rect.X, tempHeight + 20, ChartPane.Chart.Rect.Width, this.Height - tempHeight - 110);
            }
        
        }

        private void zgcQuickChart_Paint(object sender, PaintEventArgs e)
        {
            //ResizeVolume();

        }

        private void frmQuickChart_Activated(object sender, EventArgs e)
        {
            //ResizeVolume();
        }

        private void frmQuickChart_SizeChanged(object sender, EventArgs e)
        {
            ResizeVolume();
        }

        string YAxis_ScaleFormatEvent(GraphPane pane, Axis axis, double val, int index)
        {
            if(MaxVolume>1000000)
                return (val / 1000000).ToString("#,##0.0") + "M";
            else if (MaxVolume>1000)
                return (val / 1000).ToString("#,##0.0") + "k";
            else
                return (val).ToString("#,##0.0") + "";
        }

        private string MyPointValueHandler(object sender, GraphPane pane, CurveItem curve, int iPt)
        {
            if (curve.GetType().ToString().Contains("OHLCBarItem"))
            {
                OHLCBarItem tempCurve = (OHLCBarItem)curve;

                StockPt pt = (StockPt)zgc.GraphPane.CurveList["Series"].Points[iPt];
                String tempString = "CL " + pt.Close.ToString("f2") + "\r\n";
                tempString += "OP " + pt.Open.ToString("f2") + "\r\n";
                tempString += "HI " + pt.High.ToString("f2") + "\r\n";
                tempString += "LO " + pt.Low.ToString("f2") + "\r\n";
                return tempString;
            }
            else
            {
                PointPair pt = curve[iPt];
                string tempLabel = "";
                if (curve.Label.Text.Contains("Shares"))
                {
                    if (pt.Tag != "") tempLabel = tempLabel + pt.Tag + " @ ";
                }
                if (pt.Y > 1000)
                {
                    tempLabel = tempLabel + pt.Y.ToString("#,##0");
                }
                else
                {
                    tempLabel = tempLabel + pt.Y.ToString("f2");
                }
                return tempLabel;
            }
        }

        LineObj threshHoldLine;
        double TrendIniX = 0;
        double TrendIniY = 0;

        private void DrawLine(bool isAbove)
        {
            if (TrendMode == 1)
            {
                
                TrendIniX = curMouseIndex;
                if(isAbove)
                    TrendIniY = curMousePt.High;
                else
                    TrendIniY = curMousePt.Low;
            }

            if (TrendMode == 2)
            {
                double TrendFinalY =0;

                if(isAbove)
                    TrendFinalY = curMousePt.High;
                else
                    TrendFinalY = curMousePt.Low;

                if (threshHoldLine == null)
                {
                    threshHoldLine = new LineObj();
                    zgc.GraphPane.GraphObjList.Add(threshHoldLine);
                }

                double curRatio = (lineSecurity.Points.Count - TrendIniX) / (curMouseIndex - TrendIniX);
                double endX = TrendIniY + ((TrendFinalY - TrendIniY) * curRatio);

                threshHoldLine = new LineObj(Color.Red, TrendIniX, TrendIniY, lineSecurity.Points.Count, endX);

                threshHoldLine.Location.CoordinateFrame = CoordType.AxisXYScale; // This do the trick !
                threshHoldLine.IsClippedToChartRect = true;

                threshHoldLine.Line.Style = System.Drawing.Drawing2D.DashStyle.Solid;
                threshHoldLine.Line.Width = 1F;

                zgc.GraphPane.GraphObjList.Clear();
                zgc.GraphPane.GraphObjList.Add(threshHoldLine);

                zgc.Invalidate();
                zgc.Refresh();
            }
        }

        private void cmdDrawTrend_Click(object sender, EventArgs e)
        {
            TrendMode = 1;
            cmdDrawTrend.Enabled = false;
        }
    }
}