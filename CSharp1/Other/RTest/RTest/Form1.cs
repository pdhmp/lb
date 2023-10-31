using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;

using NestQuant;
using NFastData;
using STATCONNECTORCLNTLib;
using StatConnectorCommonLib;
using STATCONNECTORSRVLib;
using NestDLL;


namespace RTest
{

    //
    // COM References
    //
    

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            
            RInitialize();
        }

        SortedDictionary<double, string> IdSecurityToTicker = new SortedDictionary<double, string>();

        FastData vwapData;
        FastData lastData;
        StatConnector rConn;
        double curSize = 100000;
        double curRebate = 0.95;

        int count1 = 42;
        int count2 = 21;
        int count3 = 10;

        double trigger1 = 0.20;
        double trigger2 = 0.30;
        double minPL =100;
        double minGL = 2;

        object[] testX = new object[42];
        object[] testY = new object[42];

        private void RInitialize()
        {
            rConn = new StatConnector();
            rConn.Init("R"); 

            //for (int i = 0; i < 10; i++)
            {
                Thread t1 = new Thread(Runtest);
                t1.Start();
            }
        }

        private void Runtest()
        {
            //LoadTest();

            File.Delete(@"c:\temp\Patriot_Trade.xls");
            File.Delete(@"c:\temp\Patriot_Excluded.xls");
            File.Delete(@"c:\temp\Patriot_All.xls");

            TimeSpan IniTime = TimeSpan.FromTicks(DateTime.Now.Ticks);
            Console.WriteLine("Thread Started.");
            using (newNestConn curConn = new newNestConn())
            {

                DataTable tempTable = curConn.Return_DataTable("SELECT distinct A.IdSecurity, B.NESTTICKER"+
                                                                " FROM NESTDb.dbo.Tb050_Preco_Acoes_Onshore A (NOLOCK) " +
                                                                " LEFT JOIN NESTDB.DBO.TB001_SECURITIES B (NOLOCK) "+
                                                                " ON A.IDSECURITY = B.IDSECURITY "+
                                                                " WHERE SrType=331 ORDER BY A.IDSECURITY");

                double[] curTickers = new double[tempTable.Rows.Count];

                for (int j = 0; j < tempTable.Rows.Count - 1; j++)
                {
                    curTickers[j] = NestDLL.Utils.ParseToDouble(tempTable.Rows[j][0]);
                    IdSecurityToTicker.Add(double.Parse(tempTable.Rows[j][0].ToString()), tempTable.Rows[j][1].ToString());
                }

                vwapData = new FastData(331, new DateTime(2011, 06, 01), new DateTime(2011, 09, 19), 1);
                vwapData.LoadTickers(curTickers);
                lastData = new FastData(1, new DateTime(2011, 06, 01), new DateTime(2011, 09, 19), 1);
                lastData.LoadTickers(curTickers);

                Int64 runCounter = 0;

                try
                {

                    for (int i = 0; i < curTickers.Length - 1; i++)
                    {
                        Console.WriteLine(TimeSpan.FromTicks(DateTime.Now.Ticks).Subtract(IniTime) + " T_" + runCounter + " i_" + i + " j_" + 0);
                        for (int j = i + 1; j < curTickers.Length - 1; j++)
                        {
                            RunTest(curTickers[i], curTickers[j]);
                            
                            runCounter++;
                        }
                    }
                    //TimeSpan EndTime = TimeSpan.FromTicks(DateTime.Now.Ticks);

                    Console.WriteLine(TimeSpan.FromTicks(DateTime.Now.Ticks).Subtract(IniTime) + " FINISH");

                    rConn.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void RunTest(double Ticker1, double Ticker2)
        {
            if (Ticker1 == 1 && Ticker2 == 35) { int a = 0; }
            if (Ticker1 == 35 && Ticker2 == 1) { int a = 0; }
            double curX = 0;
            double curY = 0;

            object[] x1 = new object[count1];
            object[] y1 = new object[count1];
            object[] x2 = new object[count2];
            object[] y2 = new object[count2];
            object[] x3 = new object[count3];
            object[] y3 = new object[count3];

            double[] dpl1 = new double[count1];
            double[] dpl2 = new double[count2];
            double[] dpl3 = new double[count3];

            double[] dwl1 = new double[count1];
            double[] dwl2 = new double[count2];
            double[] dwl3 = new double[count3];

            DateTime getDate = vwapData.endDate;
            int invalidCounter = 0;
            //bool invalidPair = false;

            for (int i = 0; i < count1; i++)
            {
                double T1Last = lastData.GetValue(Ticker1, getDate, true)[1];
                double T2Last = lastData.GetValue(Ticker2, getDate, true)[1];
                double T1Vwap = vwapData.GetValue(Ticker1, getDate, true)[1];
                double T2Vwap = vwapData.GetValue(Ticker2, getDate, true)[1];

                if (T1Last == 0) { invalidCounter++; }
                if (T2Last == 0) { invalidCounter++; }
                if (T1Vwap == 0) { invalidCounter++; }
                if (T2Vwap == 0) { invalidCounter++; }

                if (T1Last == 0 && T1Vwap != 0) { T1Last = T1Vwap; }
                else if (T1Last != 0 && T1Vwap == 0) { T1Vwap = T1Last; }
                else if (T1Last == 0 && T1Vwap == 0) { T1Last = 1; T1Vwap = 1; }

                if (T2Last == 0 && T2Vwap != 0) { T2Last = T2Vwap; }
                else if (T2Last != 0 && T2Vwap == 0) { T2Vwap = T2Last; }
                else if (T2Last == 0 && T2Vwap == 0) { T2Last = 1; T2Vwap = 1; }

                curX = T2Last / T1Last;
                curY = T2Vwap / T1Vwap;

                //curX = (double)testX[i];
                //curY = (double)testY[i];

                if (double.IsInfinity(curX) || double.IsInfinity(curY)||double.IsNaN(curX)||double.IsNaN(curY))
                {
                    curX = 0; curY = 0;                    
                    //invalidPair = true;
                }

                if (i < count1)
                {
                    x1[i] = curX;
                    y1[i] = curY;

                    dpl1[i] = (curX / curY - 1) * curSize - curSize * 4 * (0.00025 + 0.005 * (1 - curRebate));
                    if (dpl1[i] > 0) dwl1[i] = 1;
                }
                if (i < count2)
                {
                    x2[i] = curX;
                    y2[i] = curY;

                    dpl2[i] = (curX / curY - 1) * curSize - curSize * 4 * (0.00025 + 0.005 * (1 - curRebate));
                    if (dpl2[i] > 0) dwl2[i] = 1;
                }
                if (i < count3)
                {
                    x3[i] = curX;
                    y3[i] = curY;

                    dpl3[i] = (curX / curY - 1) * curSize - curSize * 4 * (0.00025 + 0.005 * (1 - curRebate));
                    if (dpl3[i] > 0) dwl3[i] = 1;
                }

                getDate = vwapData.PrevDate(getDate);
            }



            //if (!invalidPair)
            {
                try
                {
                    rConn.SetSymbol("x", x1);
                    rConn.SetSymbol("y", y1);
                    //rConn.Evaluate("x3<-wilcox.test(x,y,paired = TRUE, alternative = \"two.sided\")");
                    rConn.Evaluate("x1<-wilcox.test(x,y,paired = FALSE, alternative = \"greater\")");
                }
                catch
                {
                    int a = 0;
                }
                var o1 = rConn.GetSymbol("x1");

                try
                {
                    rConn.SetSymbol("x", x2);
                    rConn.SetSymbol("y", y2);
                    //rConn.Evaluate("x3<-wilcox.test(x,y,paired = TRUE, alternative = \"two.sided\")");
                    rConn.Evaluate("x2<-wilcox.test(x,y,paired = FALSE, alternative = \"greater\")");
                }
                catch
                {
                    int a = 0;
                }
                var o2 = rConn.GetSymbol("x2");



                try
                {
                    rConn.SetSymbol("x", x3);
                    rConn.SetSymbol("y", y3);
                    //rConn.Evaluate("x3<-wilcox.test(x,y,paired = TRUE, alternative = \"two.sided\")");
                    rConn.Evaluate("x3<-wilcox.test(x,y,paired = FALSE, alternative = \"greater\")");
                }
                catch
                {
                    int a = 0;
                }
                var o3 = rConn.GetSymbol("x3");


                double p1 = 0; double.TryParse(o1[2].ToString(), out p1);
                double p2 = 0; double.TryParse(o2[2].ToString(), out p2);
                double p3 = 0; double.TryParse(o3[2].ToString(), out p3);

                int side1 = 1;
                int side2 = 1;
                int side3 = 1;

                if (Math.Abs(p1) > 0.5) side1 = -1;
                if (Math.Abs(p2) > 0.5) side2 = -1;
                if (Math.Abs(p3) > 0.5) side3 = -1;

                if (p1 > 0.5) p1 = 1 - p1;
                if (p2 > 0.5) p2 = 1 - p2;
                if (p3 > 0.5) p3 = 1 - p3;                                

                double pl1 = 0;
                double pl2 = 0;
                double pl3 = 0;

                double gl1 = 0;
                double gl2 = 0;
                double gl3 = 0;

                double gains1 = 0;
                double gains2 = 0;
                double gains3 = 0;

                double losses1 = 0;
                double losses2 = 0;
                double losses3 = 0;

                for (int i = 0; i < count1; i++)
                {
                    if (i < count1)
                    {
                        pl1 += dpl1[i] * side1;
                        if (dpl1[i] * side1 > 0) gains1 += dpl1[i] * side1;
                        if (dpl1[i] * side1 < 0) losses1 += dpl1[i] * side1;
                    }
                    if (i < count2)
                    {
                        pl2 += dpl2[i] * side2;
                        if (dpl1[i] * side2 > 0) gains2 += dpl2[i] * side2;
                        if (dpl1[i] * side2 < 0) losses2 += dpl2[i] * side2;
                    }
                    if (i < count3)
                    {
                        pl3 += dpl3[i] * side3;
                        if (dpl1[i] * side3 > 0) gains3 += dpl3[i] * side3;
                        if (dpl1[i] * side3 < 0) losses3 += dpl3[i] * side3;
                    }
                }

                pl1 = pl1 / count1;
                pl2 = pl2 / count2;
                pl3 = pl3 / count3;

                gl1 = gains1 / -losses1;
                gl2 = gains2 / -losses2;
                gl3 = gains3 / -losses3;


                //Separate Trades from Excluded
                int iTerm1 = 0;
                int iTerm2 = 0;
                int iTerm3 = 0;
                int iTerm4 = 0;
                int iTerm5 = 0;
                int iTerm6 = 0;
                int iTerm7 = 0;
                int iTerm8 = 0;
                int iTerm9 = 0;
                int iTerm10 = 0;
                int iTerm11 = 0;
                int iTerm12 = 0;
                int iTerm13 = 0;
                int iTerm14 = 0;
                int iTerm15 = 0;

                //3 ins
                if ((p1 <= trigger1) && (p2 <= trigger1) && (p3 <= trigger1)) { iTerm1 = 1; }
                
                //1 an 3 in
                if ((p1 <= trigger1) && (p3 <= trigger1) && (p2 > trigger1)) { iTerm5 = 1; }
                if ((pl1 > minPL) && (pl2 > minPL) && (pl3 > minPL)) { iTerm6 = iTerm5; }
                if ((gl1 > minGL) && (gl2 > minGL) && (gl3 > minGL)) { iTerm7 = iTerm5; }
                iTerm2 = iTerm5 * iTerm6 * iTerm7;
                
                //2 and 3 in
                if ((p3 <= trigger1) && (p2 <= trigger1) && (p1 > trigger1)) { iTerm8 = 1; }
                if ((pl2 > minPL) && (pl3 > minPL)) { iTerm9 = iTerm8; }
                if ((gl2 > minGL) && (gl3 > minGL)) { iTerm10 = iTerm8; }
                if (p1 < trigger2) { iTerm11 = iTerm8; }
                iTerm3 = iTerm8 * iTerm9 * iTerm10 * iTerm11;

                //1 and 2 in
                if ((p1 < trigger1) && (p2 < trigger1) && (p3 > trigger1)) { iTerm12 = 1; }
                if ((pl1 > minPL) && (pl2 > minPL) && (pl3 > minPL)) { iTerm13 = iTerm12; }
                if ((gl1 > minGL) && (gl2 > minGL) && (gl3 > minGL)) { iTerm14 = iTerm12; }
                if (pl1 < trigger2) { iTerm15 = iTerm12; }
                iTerm4 = iTerm12 * iTerm13 * iTerm14 * iTerm15;

                int iCriteria1 = int.MinValue;
                int iCriteria2 = int.MinValue;

                iCriteria1 = iTerm1 + iTerm2 + iTerm3 + iTerm4;
                iCriteria2 = iTerm5 + iTerm6 + iTerm7 + iTerm8 + iTerm9 + iTerm10 + iTerm11 + iTerm12 + iTerm13 + iTerm14 + iTerm15;                             

                bool bTrade = false;

                if (iCriteria1 == 1) { bTrade = true; }
                else if ((iCriteria1 == 0) && (iCriteria2 == 3 || iCriteria2 == 4)) { bTrade = true; }
                if (invalidCounter > 3) { bTrade = false; }                

                if (bTrade)
                {
                    StreamWriter swTrade = new StreamWriter(@"c:\temp\Patriot_Trade.xls", true);
                    swTrade.WriteLine(IdSecurityToTicker[Ticker1] + "\t" +
                                      IdSecurityToTicker[Ticker2] + "\t" +
                                      pl1 + "\t" + gl1 + "\t" + p1 + "\t" + (side1 == 1?"Compra":"Vende") + "\t" + invalidCounter + "\t \t" +
                                      pl2 + "\t" + gl2 + "\t" + p2 + "\t" + (side2 == 1?"Compra":"Vende") + "\t" +
                                      pl3 + "\t" + gl3 + "\t" + p3 + "\t" + (side3 == 1?"Compra":"Vende"));

                    swTrade.Close();
                }
                else
                {
                    StreamWriter swExcluded = new StreamWriter(@"c:\temp\Patriot_Excluded.xls", true);
                    swExcluded.WriteLine(IdSecurityToTicker[Ticker1] + "\t" +
                                         IdSecurityToTicker[Ticker2] + "\t" +
                                         pl1 + "\t" + gl1 + "\t" + p1 + "\t" + (side1 == 1 ? "Compra" : "Vende") + "\t" + invalidCounter + "\t \t" +
                                         pl2 + "\t" + gl2 + "\t" + p2 + "\t" + (side2 == 1 ? "Compra" : "Vende") + "\t" +
                                         pl3 + "\t" + gl3 + "\t" + p3 + "\t" + (side3 == 1 ? "Compra" : "Vende"));

                    swExcluded.Close();
                }

                StreamWriter swAll = new StreamWriter(@"c:\temp\Patriot_All.xls", true);
                swAll.WriteLine(IdSecurityToTicker[Ticker1] + "\t" + 
                                IdSecurityToTicker[Ticker2] + "\t" +
                                pl1 + "\t" + gl1 + "\t" + p1 + "\t" + (side1 == 1 ? "Compra" : "Vende") + "\t" + invalidCounter + "\t \t" +
                                pl2 + "\t" + gl2 + "\t" + p2 + "\t" + (side2 == 1 ? "Compra" : "Vende") + "\t" +
                                pl3 + "\t" + gl3 + "\t" + p3 + "\t" + (side3 == 1 ? "Compra" : "Vende"));

                swAll.Close();


                //Console.Write(Ticker1 + ";" + Ticker2 + ";XXXXXXXX;");
                //Console.Write(pl1 + ";" + gl1 + ";" + p1 + ";" + side1 + ";" + invalidCounter1 + ";XXXXXXXX;");
                //Console.Write(pl2 + ";" + gl2 + ";" + p2 + ";" + side2 + ";" + invalidCounter2 + ";XXXXXXXX;");
                //Console.WriteLine(pl3 + ";" + gl3 + ";" + p3 + ";" + side3 + ";" + invalidCounter3 + ";XXXXXXXX;");
            }
        }

        private void LoadTest()
        {
            testX[0] = 2.05409356725146;
            testX[1] = 2.04527559055118;
            testX[2] = 2.02949852507375;
            testX[3] = 2.03126550868486;
            testX[4] = 2.00435624394966;
            testX[5] = 1.98632144601856;
            testX[6] = 1.98102845731403;
            testX[7] = 1.98085419734904;
            testX[8] = 1.94819391634981;
            testX[9] = 1.95432692307692;
            testX[10] = 1.94841849148418;
            testX[11] = 1.94094680331869;
            testX[12] = 1.9608040201005;
            testX[13] = 1.92849949647533;
            testX[14] = 1.91250617894216;
            testX[15] = 1.91468253968254;
            testX[16] = 1.9089058524173;
            testX[17] = 1.89390862944162;
            testX[18] = 1.88669950738916;
            testX[19] = 1.93100143747005;
            testX[20] = 1.91473988439306;
            testX[21] = 1.89852310624107;
            testX[22] = 1.92906403940887;
            testX[23] = 1.94045522018803;
            testX[24] = 1.90598073998986;
            testX[25] = 1.95860574412533;
            testX[26] = 1.91369973190349;
            testX[27] = 1.94721010901883;
            testX[28] = 1.95161259079903;
            testX[29] = 1.91100044863167;
            testX[30] = 1.90254347826087;
            testX[31] = 1.90848608515235;
            testX[32] = 1.90974210806084;
            testX[33] = 1.93737461204753;
            testX[34] = 1.91246919959516;
            testX[35] = 1.91970246610805;
            testX[36] = 1.93653869236441;
            testX[37] = 1.97736552419797;
            testX[38] = 1.96837660744946;
            testX[39] = 2.01850484790104;
            testX[40] = 2.00487017319136;
            testX[41] = 1.99085557473554;


            testY[0] = 2.04835893939174;
            testY[1] = 2.05391342908527;
            testY[2] = 2.03884205944042;
            testY[3] = 2.02619777207193;
            testY[4] = 2.00114811116417;
            testY[5] = 1.9887003434978;
            testY[6] = 1.9898214884135;
            testY[7] = 1.97151071103231;
            testY[8] = 1.95026777066677;
            testY[9] = 1.9561889667902;
            testY[10] = 1.94611218924373;
            testY[11] = 1.94388120839734;
            testY[12] = 1.9543226592986;
            testY[13] = 1.92443366837717;
            testY[14] = 1.91679748045221;
            testY[15] = 1.90925913982229;
            testY[16] = 1.90345405172195;
            testY[17] = 1.89053886542977;
            testY[18] = 1.90103530448365;
            testY[19] = 1.92554961520942;
            testY[20] = 1.91406000869691;
            testY[21] = 1.90884243291798;
            testY[22] = 1.92594250741657;
            testY[23] = 1.93263308970622;
            testY[24] = 1.92214756826727;
            testY[25] = 1.94116814938229;
            testY[26] = 1.91440982863844;
            testY[27] = 1.95196614861077;
            testY[28] = 1.94088977755561;
            testY[29] = 1.90632521378017;
            testY[30] = 1.90957765561085;
            testY[31] = 1.9062584052359;
            testY[32] = 1.91451503348896;
            testY[33] = 1.93686768052038;
            testY[34] = 1.91418061713486;
            testY[35] = 1.91583815422021;
            testY[36] = 1.93758977151408;
            testY[37] = 1.97551403838387;
            testY[38] = 1.97189069185084;
            testY[39] = 2.01506346055385;
            testY[40] = 2.00102368660123;
            testY[41] = 1.98830755895971;


        }
    }
}
