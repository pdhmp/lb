using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using NestDLL;
using STATCONNECTORCLNTLib;
using StatConnectorCommonLib;
using STATCONNECTORSRVLib;
using System.Threading;
using System.IO;


namespace Patriot
{
    public class PatriotModel
    {
        public List<TradingPairs> StrategyPairs = new List<TradingPairs>();
        private PatriotRunner ParentRunner;
        public int IdModel;

        StatConnector rConn;

        private double curSize = 100000;
        private double curRebate = 0.95;


        private bool Started = false;
        public long PairsProcessed = 0;

        public event EventHandler OnFinished;

        public PatriotModel(PatriotRunner _ParentRunner, int ModelId)
        {
            ParentRunner = _ParentRunner;
            IdModel = ModelId;
        }

        public bool AddPair(double Security1, double Security2)
        {
            if (!Started && (Security1 != Security2))
            {
                TradingPairs curPair = new TradingPairs(ParentRunner);
                curPair.Security1 = Security1;
                curPair.Security2 = Security2;
                StrategyPairs.Add(curPair);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StartModel()
        {
            if (!Started)
            {
                Thread T1 = new Thread(RunModel);
                T1.Start();
                Started = true;
            }
        }

        private void RunModel()
        {
            RInitialize();            

            foreach (TradingPairs curPair in StrategyPairs)
            {
                CalculatePair(curPair);
                PairsProcessed++;
            }

            foreach (TradingPairs curPair in StrategyPairs)
            {
                //ParentRunner.PrintTradingPairs(curPair);
            }

            rConn.Close();

            Console.WriteLine("Thread (" + IdModel + ") finished. Pairs processed: " + PairsProcessed + ". Elapsed time: " 
                               + TimeSpan.FromTicks(DateTime.Now.Ticks).Subtract(TimeSpan.FromTicks(ParentRunner.StartTime.Ticks)));

            if (OnFinished != null)
            {
                OnFinished(this, null);
            }
        }

        private void RInitialize()
        {
            rConn = new StatConnector();
            rConn.Init("R");
        }

        StreamWriter debugFile;
        private void CalculatePair(TradingPairs curPair)
        {             
            bool bDebug = false;

            double Ticker1 = curPair.Security1;
            double Ticker2 = curPair.Security2;

            if (Ticker1 == -1 && Ticker2 == -1) { bDebug = true; }
            if (Ticker1 == -1 && Ticker2 == -1) { bDebug = true; }

            if (bDebug)
            {
                debugFile = new StreamWriter(@"C:\Quant\Strategies\Patriot\debugFile.xls", false);
            }

            int count1 = ParentRunner.Window1;
            int count2 = ParentRunner.Window2;
            int count3 = ParentRunner.Window3;


            DateTime auxDate = ParentRunner.refDate;

            if (auxDate.Month != ParentRunner.curDate.Month)
            {
                //TODO: auxDate = PatriotData.Instance.GetPrevDate(auxDate);
                auxDate = PatriotData.Instance.GetPrevDate(auxDate);
            }

            DateTime getDate = auxDate;
            DateTime refDate = auxDate;

            double trigger1 = ParentRunner.Trigger1;
            double trigger2 = ParentRunner.Trigger2;

            double minPL = ParentRunner.minPL;
            double minGL = ParentRunner.minGL;

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

            double[] dpl1NC = new double[count1];
            double[] dpl2NC = new double[count2];
            double[] dpl3NC = new double[count3];

            double[] dCost1 = new double[count1];
            double[] dCost2 = new double[count2];
            double[] dCost3 = new double[count3];


            double[] dwl1 = new double[count1];
            double[] dwl2 = new double[count2];
            double[] dwl3 = new double[count3];

            int invalidCounter = 0;

            for (int i = 0; i < count1; i++)
            {
                double T1Last = PatriotData.Instance.GetValue(Ticker1, 1, getDate, refDate, true)[1];
                double T2Last = PatriotData.Instance.GetValue(Ticker2, 1, getDate, refDate, true)[1];
                double T1Vwap = PatriotData.Instance.GetValue(Ticker1, 331, getDate, refDate, true)[1];
                double T2Vwap = PatriotData.Instance.GetValue(Ticker2, 331, getDate, refDate, true)[1];

                if (T1Last == 0) { invalidCounter++; }
                if (T2Last == 0) { invalidCounter++; }
                if (T1Vwap == 0) { invalidCounter++; }
                if (T2Vwap == 0) { invalidCounter++; }

                if (T1Last == 0 && T1Vwap != 0) { T1Last = T1Vwap; }
                else if (T1Last != 0 && T1Vwap == 0) { T1Vwap = T1Last; }
                else if (T1Last == 0 && T1Vwap == 0) { if (Ticker1 != 83068) { int a = 0; } T1Last = 1; T1Vwap = 1; }

                if (T2Last == 0 && T2Vwap != 0) { T2Last = T2Vwap; }
                else if (T2Last != 0 && T2Vwap == 0) { T2Vwap = T2Last; }
                else if (T2Last == 0 && T2Vwap == 0) { if (Ticker2 != 83068) { int a = 0; } T2Last = 1; T2Vwap = 1; }

                curX = T2Last / T1Last;
                curY = T2Vwap / T1Vwap;

                if (double.IsInfinity(curX) || double.IsInfinity(curY) || double.IsNaN(curX) || double.IsNaN(curY))
                {
                    curX = 0; curY = 0;
                }

                if (i < count1)
                {
                    x1[i] = curX;
                    y1[i] = curY;

                    dpl1NC[i] = (curX / curY - 1) * curSize;
                    dCost1[i] = curSize * 4 * (0.00025 + 0.005 * (1 - curRebate));
                    dpl1[i] = dpl1NC[i] - dCost1[i];
                    //dpl1[i] = (curX / curY - 1) * curSize - curSize * 4 * (0.00025 + 0.005 * (1 - curRebate));
                    if (dpl1[i] > 0) dwl1[i] = 1;
                }
                if (i < count2)
                {
                    x2[i] = curX;
                    y2[i] = curY;

                    dpl2NC[i] = (curX / curY - 1) * curSize;
                    dCost2[i] = curSize * 4 * (0.00025 + 0.005 * (1 - curRebate));
                    dpl2[i] = dpl2NC[i] - dCost2[i];
                    //dpl2[i] = (curX / curY - 1) * curSize - curSize * 4 * (0.00025 + 0.005 * (1 - curRebate));
                    if (dpl2[i] > 0) dwl2[i] = 1;
                }
                if (i < count3)
                {
                    x3[i] = curX;
                    y3[i] = curY;

                    dpl3NC[i] = (curX / curY - 1) * curSize;
                    dCost3[i] = curSize * 4 * (0.00025 + 0.005 * (1 - curRebate));
                    dpl3[i] = dpl3NC[i] - dCost3[i];
                    //dpl3[i] = (curX / curY - 1) * curSize - curSize * 4 * (0.00025 + 0.005 * (1 - curRebate));
                    if (dpl3[i] > 0) dwl3[i] = 1;
                }

                if (bDebug)
                {
                    debugFile.WriteLine(T1Last + "\t" + T1Vwap + "\t" + T2Last + "\t" + T2Vwap + "\t" + curX + "\t" + curY);
                }

                DateTime auxGetDate = PatriotData.Instance.GetPrevDate(getDate);
                if (auxGetDate.Month != getDate.Month)
                {
                    //TODO: auxGetDate = PatriotData.Instance.GetPrevDate(auxGetDate);
                    auxGetDate = PatriotData.Instance.GetPrevDate(auxGetDate);
                }
                getDate = auxGetDate;

            }

            if (bDebug)
            {
                debugFile.Close();
            }

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

            curPair.PValue1 = p1;
            curPair.PValue2 = p2;
            curPair.PValue3 = p3;

            curPair.Side1 = 1;
            curPair.Side2 = 1;
            curPair.Side3 = 1;

            if (Math.Abs(curPair.PValue1) > 0.5) curPair.Side1 = -1;
            if (Math.Abs(curPair.PValue2) > 0.5) curPair.Side2 = -1;
            if (Math.Abs(curPair.PValue3) > 0.5) curPair.Side3 = -1;

            if (curPair.PValue1 > 0.5) curPair.PValue1 = 1 - curPair.PValue1;
            if (curPair.PValue2 > 0.5) curPair.PValue2 = 1 - curPair.PValue2;
            if (curPair.PValue3 > 0.5) curPair.PValue3 = 1 - curPair.PValue3;

            curPair.PL1 = 0;
            curPair.PL2 = 0;
            curPair.PL3 = 0;

            curPair.GL1 = 0;
            curPair.GL2 = 0;
            curPair.GL3 = 0;

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
                    curPair.PL1 += dpl1NC[i] * curPair.Side1 - dCost1[i];
                    //curPair.PL1 += dpl1[i] * curPair.Side1;
                    if (dpl1[i] * curPair.Side1 > 0) gains1 += dpl1NC[i] * curPair.Side1 - dCost1[i]; //dpl1[i] * curPair.Side1;
                    if (dpl1[i] * curPair.Side1 < 0) losses1 += dpl1NC[i] * curPair.Side1 - dCost1[i]; //dpl1[i] * curPair.Side1;
                }
                if (i < count2)
                {
                    curPair.PL2 += dpl2NC[i] * curPair.Side2 - dCost2[i];
                    //curPair.PL2 += dpl2[i] * curPair.Side2;
                    if (dpl1[i] * curPair.Side2 > 0) gains2 += dpl2NC[i] * curPair.Side2 - dCost2[i]; //dpl2[i] * curPair.Side2;
                    if (dpl1[i] * curPair.Side2 < 0) losses2 += dpl2NC[i] * curPair.Side2 - dCost2[i]; //dpl2[i] * curPair.Side2;
                }
                if (i < count3)
                {
                    curPair.PL3 += dpl3NC[i] * curPair.Side3 - dCost3[i]; 
                    //curPair.PL3 += dpl3[i] * curPair.Side3;
                    if (dpl1[i] * curPair.Side3 > 0) gains3 += dpl3NC[i] * curPair.Side3 - dCost3[i]; //dpl3[i] * curPair.Side3;
                    if (dpl1[i] * curPair.Side3 < 0) losses3 += dpl3NC[i] * curPair.Side3 - dCost3[i]; //dpl3[i] * curPair.Side3;
                }
            }

            if (bDebug)
            {
                int a = 0;
            }

            curPair.PL1 = curPair.PL1 / count1;
            curPair.PL2 = curPair.PL2 / count2;
            curPair.PL3 = curPair.PL3 / count3;

            curPair.GL1 = gains1 / -losses1;
            curPair.GL2 = gains2 / -losses2;
            curPair.GL3 = gains3 / -losses3;


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
            if ((curPair.PValue1 <= trigger1) && (curPair.PValue2 <= trigger1) && (curPair.PValue3 <= trigger1)) { iTerm1 = 1; }

            //1 an 3 in
            if ((curPair.PValue1 <= trigger1) && (curPair.PValue3 <= trigger1) && (curPair.PValue2 > trigger1)) { iTerm5 = 1; }
            if ((curPair.PL1 > minPL) && (curPair.PL2 > minPL) && (curPair.PL3 > minPL)) { iTerm6 = iTerm5; }
            if ((curPair.GL1 > minGL) && (curPair.GL2 > minGL) && (curPair.GL3 > minGL)) { iTerm7 = iTerm5; }
            iTerm2 = iTerm5 * iTerm6 * iTerm7;

            //2 and 3 in
            if ((curPair.PValue3 <= trigger1) && (curPair.PValue2 <= trigger1) && (curPair.PValue1 > trigger1)) { iTerm8 = 1; }
            if ((curPair.PL2 > minPL) && (curPair.PL3 > minPL)) { iTerm9 = iTerm8; }
            if ((curPair.GL2 > minGL) && (curPair.GL3 > minGL)) { iTerm10 = iTerm8; }
            if (curPair.PValue1 < trigger2) { iTerm11 = iTerm8; }
            iTerm3 = iTerm8 * iTerm9 * iTerm10 * iTerm11;

            //1 and 2 in
            if ((curPair.PValue1 < trigger1) && (curPair.PValue2 < trigger1) && (curPair.PValue3 > trigger1)) { iTerm12 = 1; }
            if ((curPair.PL1 > minPL) && (curPair.PL2 > minPL) && (curPair.PL3 > minPL)) { iTerm13 = iTerm12; }
            if ((curPair.GL1 > minGL) && (curPair.GL2 > minGL) && (curPair.GL3 > minGL)) { iTerm14 = iTerm12; }
            if (curPair.PL1 < trigger2) { iTerm15 = iTerm12; }
            iTerm4 = iTerm12 * iTerm13 * iTerm14 * iTerm15;

            int iCriteria1 = int.MinValue;
            int iCriteria2 = int.MinValue;

            iCriteria1 = iTerm1 + iTerm2 + iTerm3 + iTerm4;
            iCriteria2 = iTerm5 + iTerm6 + iTerm7 + iTerm8 + iTerm9 + iTerm10 + iTerm11 + iTerm12 + iTerm13 + iTerm14 + iTerm15;

            curPair.Trade = false;

            if (iCriteria1 == 1) { curPair.Trade = true; }
            else if ((iCriteria1 == 0) && (iCriteria2 == 3 || iCriteria2 == 4)) { curPair.Trade = true; }
            if (invalidCounter > 3) { curPair.Trade = false; }

            if (!curPair.Trade)
            {
                int a = 0;
            }

            if (bDebug)
            {
                int a = 0;
            }

            if (curPair.PL1 <= 100)
            {
                curPair.Trade = false;
            }

            if (curPair.Trade)
            {
                curPair.CalculatePairSize();
            }

        }
    }
}
