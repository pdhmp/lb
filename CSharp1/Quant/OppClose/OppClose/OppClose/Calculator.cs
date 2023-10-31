using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using NestDLL;
using NFastData;
using System.IO;

using STATCONNECTORCLNTLib;
using StatConnectorCommonLib;
using STATCONNECTORSRVLib;

namespace OppClose
{
    class RCalculator
    {         
        public double ticker;
        public SortedDictionary<DateTime, Values> ValuesList;

        FastData last;
        FastData TR_Index;
        FastData open;
        FastData vwap;        
        
        Dictionary<DateTime,double> varList;
        Dictionary<DateTime, double> EwmaVolList;
        Dictionary<DateTime, double> lastList;
        Dictionary<DateTime, double> openList;
        Dictionary<DateTime, double> vwapList;

        DateTime iniDate;
        DateTime lastDate;

        public RCalculator(int _ticker, DateTime _IniDate, DateTime _LastDate)
        {
            ticker = _ticker;
            iniDate = _IniDate;
            lastDate = _LastDate;

            lastList = new Dictionary<DateTime, double>();
            openList = new Dictionary<DateTime, double>();
            varList = new Dictionary<DateTime,double>();
            vwapList = new Dictionary<DateTime, double>();
            EwmaVolList = new Dictionary<DateTime, double>();
            ValuesList = new SortedDictionary<DateTime, Values>();
            
            //last = new FastData(1, iniDate, DateTime.Today, 1);
            //open = new FastData(8, iniDate, DateTime.Today, 1);
            //vwap = new FastData(5, iniDate, DateTime.Today, 1);
            //TR_Index = new FastData(101, iniDate, DateTime.Today, 1);

            last = new FastData(1, iniDate, lastDate, 1);
            open = new FastData(8, iniDate, lastDate, 1);
            vwap = new FastData(5, iniDate, lastDate, 1);
            TR_Index = new FastData(101, iniDate, lastDate, 1);

            LoadTickers(ticker);

            Calculate();
        }

        public void Calculate()
        {
            GetLastList();
            GetOpenList();            
            GetVarList();
            GetVwapList();
            GetEwmaVolList();

            Values DateValues = new Values();

            DateTime auxDate = last.endDate;
            DateTime auxDate2 = last.NextDate(auxDate);                       

            while (!((auxDate == iniDate) || (auxDate == last.PrevDate(auxDate))))
            {
                if (auxDate == new DateTime(2011, 09, 08))
                { }

                if (auxDate == new DateTime(2011, 03, 17))
                { }

                double Last = 0;                
                double prevLast = 0;
                double Open = 0;
                double prevOpen = 0;
                double nextOpen = 0;
                double Vwap = 0;
                double prevVwap = 0;
                bool gotAllValues = false;
                                

                gotAllValues = lastList.TryGetValue(auxDate, out Last) &&
                                lastList.TryGetValue(last.PrevDate(auxDate), out prevLast) &&
                                openList.TryGetValue(auxDate, out Open) &&
                                openList.TryGetValue(open.PrevDate(auxDate), out prevOpen) &&
                                openList.TryGetValue(auxDate2, out nextOpen) &&
                                vwapList.TryGetValue(auxDate,out Vwap) &&
                                vwapList.TryGetValue(vwap.PrevDate(auxDate),out prevVwap);

                if (gotAllValues)
                {                     
                    List<double> auxLastList = GetLastList(auxDate);
                    List<double> auxOpenList = GetOpenList(auxDate);

                    double EwmaVol = 0;
                    double prevEwmaVol = 0;
                    EwmaVolList.TryGetValue(auxDate, out EwmaVol);
                    EwmaVolList.TryGetValue(last.PrevDate(auxDate), out prevEwmaVol);

                    DateValues.ultAbe = GetUltAbe(Last, Open, EwmaVol);
                    DateValues.UltTm1Abe = GetUltTM1Abe(Last, prevOpen, EwmaVol);
                    DateValues.ultUltM1 = GetUltUltM1(Last, prevLast, EwmaVol);
                    DateValues.ultVwap = GetUltVwap(Last, Vwap, EwmaVol);
                    DateValues.UltVwapM1 = GetUltVwapM1(Last, prevVwap, EwmaVol);
                    DateValues.Estocastico = GetEstocatisco(Last, GetMinLastOpen(auxLastList, auxOpenList), GetMaxLastOpen(auxLastList, auxOpenList));
                    DateValues.RefValue = GetRefValue(nextOpen, Last, EwmaVol);
                    DateValues.Var = GetVar(nextOpen, Last);
                    DateValues.Up = GetUp(DateValues.RefValue);
                    DateValues.Down = GetDown(DateValues.RefValue);

                    ValuesList.Add(auxDate, DateValues);
                    DateValues = new Values();
                }
                
                auxDate2 = auxDate;
                if (auxDate2 == new DateTime(2011, 08, 15))
                { }
                auxDate = last.PrevDate(auxDate);
            }          
            
        }       
  
        private void LoadTickers(double idSecutiry)
        {           
            double[] ticker = {idSecutiry};
            
            last.LoadTickers(ticker);
            open.LoadTickers(ticker);
            vwap.LoadTickers(ticker);
            TR_Index.LoadTickers(ticker);
        }

        #region Lists

        private List<double> GetOpenList(DateTime iniDate)
        {
            List<double> openlist = new List<double>();

            int i = 0;

            while (i < 21)
            {
                if (iniDate != open.PrevDate(iniDate))
                {
                    double curOpen;
                    if (openList.TryGetValue(iniDate, out curOpen))
                    {
                        openlist.Add(curOpen);
                        iniDate = open.PrevDate(iniDate);
                    }
                }
                i++;
            }
            return openlist;
        }

        private void GetOpenList()
        {
            DateTime auxDate = open.PrevDate(open.endDate);            

            while (!(auxDate < open.iniDate) && !openList.ContainsKey(auxDate))
            {
                if (auxDate == new DateTime(2011, 05, 05))
                { }

                double refLast;
                lastList.TryGetValue(auxDate, out refLast);

                double dbOpen = open.GetValue(ticker, auxDate, true)[1];
                double dbLast = last.GetValue(ticker, auxDate, true)[1];
                double curOpen = refLast * dbOpen / dbLast;
                openList.Add(auxDate, curOpen);
                auxDate = open.PrevDate(auxDate);
            }

        }

        private List<double> GetLastList(DateTime iniDate)
        {
            List<double> lastlist = new List<double>();

            int i = 0;

            while (i < 21)
            {
                if (iniDate != last.PrevDate(iniDate))
                {
                    double curlast;
                    if (lastList.TryGetValue(iniDate, out curlast))
                    {
                        lastlist.Add(curlast);
                        iniDate = last.PrevDate(iniDate);
                    }                    
                    
                }
                i++;
            }
            return lastlist;
        }

        private void GetLastList()
        {
            DateTime auxDate = last.endDate;
            double curlastPrice;
            double[] refIndex = new double[2];
            double[] curIndex = { 0, 0 };

            try
            {
                auxDate = last.PrevDate(auxDate);
                refIndex = TR_Index.GetValue(ticker, auxDate, true);
                lastList.Add(auxDate, last.GetValue(ticker, auxDate, true)[1]);

                auxDate = last.PrevDate(auxDate);

                while (!(auxDate < last.iniDate) && !lastList.ContainsKey(auxDate))
                {
                    if (auxDate == new DateTime(2010, 10, 18))
                    { }
                    curIndex = TR_Index.GetValue(ticker, auxDate, true);

                    curlastPrice = lastList.First().Value * curIndex[1] / refIndex[1];

                    lastList.Add(auxDate, curlastPrice);

                    auxDate = last.PrevDate(auxDate);
                }
            }
            catch (Exception E)
            { }
        }       

        private void GetVarList()
        {
            DateTime auxDate = last.PrevDate(last.endDate);           
            
            while (!(auxDate < last.iniDate))
            {
                if (auxDate == new DateTime(2010, 10, 19))
                { }

                double Last = 0;
                double prevLast = 0;
                bool gotValues = lastList.TryGetValue(auxDate, out Last) && lastList.TryGetValue(last.PrevDate(auxDate), out prevLast);
                
                if (gotValues)
                {
                    varList.Add(auxDate, Last/prevLast -1);
                }
                
                auxDate = last.PrevDate(auxDate);
            }
        }

        private void GetVwapList()
        {
            DateTime auxDate = vwap.PrevDate(vwap.endDate);

            while (!(auxDate < vwap.iniDate) && !vwapList.ContainsKey(auxDate))
            {
                if (auxDate == new DateTime(2011, 05, 05))
                { }

                double refLast;
                lastList.TryGetValue(auxDate, out refLast);

                double dbVwap = vwap.GetValue(ticker, auxDate, true)[1];
                double dbLast = last.GetValue(ticker, auxDate, true)[1];
                double curVwap = refLast * dbVwap / dbLast;
                vwapList.Add(auxDate, curVwap);

                if (vwapList.ContainsKey(auxDate) || vwap.PrevDate(auxDate) < vwap.iniDate)
                { }
                
                auxDate = vwap.PrevDate(auxDate);

                
            }
        }

        private void GetEwmaVolList()
        {
            int i = 0;          

            DateTime auxFirstLoopDate = last.PrevDate(last.endDate);
            DateTime endFirstLoopDate = varList.Last().Key;

            DateTime auxSecondLoopDate = last.PrevDate(auxFirstLoopDate);
            
            List<double> varLast100 = new List<double>();
            double var;

            while(auxFirstLoopDate != endFirstLoopDate)
            {
                if (auxFirstLoopDate == new DateTime(2011, 03, 17))
                { }
                while (i < 100 && !(auxSecondLoopDate == last.PrevDate(auxSecondLoopDate)))
                {
                    if (i == 99)
                    { }
                    if (varList.TryGetValue(auxSecondLoopDate, out var))
                    {
                        if (auxSecondLoopDate == new DateTime(2011, 09, 15))
                        { }
                        varLast100.Add(var);
                        i++;                        
                    }
                    auxSecondLoopDate = last.PrevDate(auxSecondLoopDate);
                }
                if (auxFirstLoopDate == new DateTime(2011, 04, 18))
                { }
                EwmaVolList.Add(auxFirstLoopDate,GetEwmaVol(varLast100,0.94,true)/2);
                auxFirstLoopDate = last.PrevDate(auxFirstLoopDate);
                auxSecondLoopDate = last.PrevDate(auxFirstLoopDate);
                varLast100.Clear();
                i = 0;
            }
            int j = 0;
        }

        #endregion

        #region Basic Values

        private double GetEwmaVol(List<double> ListVars, double lambda, bool reverseOrder = false)
        {
            int inicial, final, count, paco;
            double ex = 0, ex2 = 0, total = 1;
            double[] Px = new double[300];


            if (reverseOrder)
            {
                inicial = ListVars.Count;
                final = 0;
                paco = -1;
            }
            else
            {
                inicial = 0;
                final = ListVars.Count;
                paco = 1;
            }

            for (count = inicial * paco - paco; count <= final; count++)
            {
                Px[count * paco] = total * (1 - lambda);
                total -= Px[count * paco];
            }

            for (count = 0; count < ListVars.Count; count++)
            {
                Px[count] /= (1 - total);
                ex2 += (Px[count] * Math.Pow(ListVars.ElementAt(count), 2));
                ex += Px[count] * ListVars.ElementAt(count);
            }

            double result = Math.Pow((ex2 - Math.Pow(ex, 2)), 0.5);
            return result;
        }

        private double GetUltVwap(double last, double vwap, double ewmaVol)
        {
            return Math.Log(last/vwap)/ewmaVol;
        }

        private double GetUltAbe(double last, double open, double ewmaVol)
        {
            return Math.Log(last/open)/ewmaVol;
        }
        
        private double GetUltUltM1(double last, double prevLast, double ewmaVol)
        {
            return Math.Log(last/prevLast)/ewmaVol;
        }

        private double GetUltTM1Abe(double last, double prevOpen, double ewmaVol)
        {
            return Math.Log(last/prevOpen)/ewmaVol;
        }

        private double GetUltVwapM1(double last, double prevVwap, double ewmaVol)
        {
            return Math.Log(last/prevVwap)/ewmaVol;
        }

        private double GetEstocatisco(double last, double minLast_Open, double MaxLast_Open)
        {
            return (last - minLast_Open)/(MaxLast_Open-minLast_Open) - 0.5;
        }

        private double GetMinLastOpen(List<double> LastList, List<double>OpenList)
        {
            double Last = LastList.First();
            double Open = OpenList.First();

            foreach (double last in LastList)
            {
                Last = (Last < last) ? Last : last;
            }

            foreach (double open in OpenList)
            {
                Open = (Open < open) ? Open : open;
            }

            double result = Math.Min(Open, Last);

            return result;

        }

        private double GetMaxLastOpen(List<double> LastList, List<double>OpenList)
        {
            double Last = 0;
            double Open = 0;

            foreach (double last in LastList)
            {
                if (last == 10)
                { }
                Last = (Last > last) ? Last : last;
            }

            foreach (double open in OpenList)
            {
                Open = (Open > open) ? Open : open;
            }

            double result = Math.Max(Open, Last);

            return result;
        }

        private double GetRefValue(double nextOpen, double Last, double EwmaVol)
        {
            double result = Math.Log(nextOpen / Last) / EwmaVol;
            return result;
        }

        private double GetVar(double Open, double Last)
        {
            return Open / Last - 1;
        }

        private int GetUp(double refValue)
        {
            return (refValue > 1) ? 1 : 0;
        }

        private int GetDown(double refValue)
        {
            return (refValue < -1) ? 1 : 0;
        }

        #endregion

        public bool GetRegret(StatConnector RConn,SortedDictionary<DateTime,Values> Data,DateTime Date, out Values _UpResults, out Values _DownResults)
        {
            Values UpResults = new Values(), DownResults = new Values();
            bool GotValues = true;            

            //object[] Parameters = new object[8];
            object[] Up = new object[100]; 
            object[] Down = new object[100];
            object[] ultVwap = new object[100];
            object[] ultAbe = new object[100];
            object[] ultUltM1 = new object[100];
            object[] UltTm1Abe = new object[100];
            object[] UltVwapM1 = new object[100];
            object[] Estocastico = new object[100];
     
            /*
            Up[0] = "Up";
            Down[0] = "Down";
            ultVwap[0] = "ultVwap";
            ultAbe[0] = "ultAbe";
            ultUltM1[0] = "ultUltM1";
            UltTm1Abe[0] = "ultTm1Abe";
            UltVwapM1[0] = "ultVwapM1";
            Estocastico[0] = "Est";
            */

            int i = 0;

            try
            {
                while (i < 100)
                {
                    Values DataNode;
                    if (Data.TryGetValue(Date, out DataNode))
                    {
                        if (i == 99)
                        { }
                        Up[i] = DataNode.Up;
                        Down[i] = DataNode.Down;
                        ultVwap[i] = DataNode.ultVwap;
                        ultAbe[i] = DataNode.ultAbe;
                        ultUltM1[i] = DataNode.ultUltM1;
                        UltTm1Abe[i] = DataNode.UltTm1Abe;
                        UltVwapM1[i] = DataNode.UltVwapM1;                        
                        Estocastico[i] = DataNode.Estocastico;                        
                        /*StreamWriter SW = new StreamWriter(@"c:\Temp\LogOppClose.csv",true);
                        SW.WriteLine(DataNode.ultVwap + ";" + DataNode.ultAbe + ";" + DataNode.ultUltM1 + ";" + DataNode.UltTm1Abe + ";" + DataNode.Estocastico + ";" + DataNode.UltVwapM1 + ";" + DataNode.Up + ";" + DataNode.Down);
                        SW.Close();*/
                    }
                    else
                    {
                        _UpResults = UpResults;
                        _DownResults = DownResults;
                        return false;
                    }
                    Date = last.PrevDate(Date);
                    i++;
                }
            }
            catch (Exception E)
            {
                GotValues = false;
            }
            
            /*
            Parameters[0] = Up;
            Parameters[1] = Down;
            Parameters[2] = ultVwap;
            Parameters[3] = ultAbe;
            Parameters[4] = ultUltM1;
            Parameters[5] = UltTm1Abe;
            Parameters[6] = UltVwapM1;
            Parameters[7] = Estocastico;
             * */   

            try
            {                
                RConn.SetSymbol("Up", Up);
                RConn.SetSymbol("Down", Down);
                RConn.SetSymbol("ultVwap", ultVwap);
                RConn.SetSymbol("ultAbe", ultAbe);
                RConn.SetSymbol("ultUltM1", ultUltM1);
                RConn.SetSymbol("ultTm1Abe", UltTm1Abe);
                RConn.SetSymbol("ultVwapM1", UltVwapM1);                
                //RConn.SetSymbol("Est", Estocastico);
            }

            catch(Exception E)
            {
                GotValues = false;
            }

            //try
            //{
            //    RConn.EvaluateNoReturn("Parameters<-data.frame(Up,Down,ultVwap,ultAbe,ultUltM1,ultTm1Abe,ultVwapM1,Est");
            //}
            //catch (Exception E)
            //{ }


            try
            {
                //RConn.EvaluateNoReturn("DownAns<-glm(Down~ultVwap+ultAbe+ultUltM1+ultTm1Abe+Est+ultVwapM1, family=binomial(link = logit))$coefficients");
                RConn.EvaluateNoReturn("DownAns<-glm(Down~ultVwap+ultAbe+ultUltM1+ultTm1Abe+ultVwapM1, family=binomial(link = logit))$coefficients");
            }
            catch (Exception E)
            {
                GotValues = false;
            }

            try
            {
                //RConn.EvaluateNoReturn("UpAns<-glm(Up~ultVwap+ultAbe+ultUltM1+ultTm1Abe+Est+ultVwapM1, family=binomial(link = logit))$coefficients");                             
                RConn.EvaluateNoReturn("UpAns<-glm(Up~ultVwap+ultAbe+ultUltM1+ultTm1Abe+ultVwapM1, family=binomial(link = logit))$coefficients");                             
            }
            catch (Exception E)
            {
                GotValues = false;
            }

            if (GotValues)
            {
                try
                {
                    //var UpAns = RConn.Evaluate("summary(UpAns)");
                    //var DownAns = RConn.Evaluate("summary(DownAns)");

                    var UpAns = RConn.GetSymbol("UpAns");
                    var DownAns = RConn.GetSymbol("DownAns");


                    //for (int k = 0; k < 7; k++)
                    for (int k = 0; k < 6; k++)
                    {
                        switch (k)
                        {
                            case 0: UpResults.Cte = UpAns[k];
                                DownResults.Cte = DownAns[k];
                                break;
                            case 1: UpResults.ultVwap = UpAns[k];
                                DownResults.ultVwap = DownAns[k];
                                break;
                            case 2: UpResults.ultAbe = UpAns[k];
                                DownResults.ultAbe = DownAns[k];
                                break;
                            case 3: UpResults.ultUltM1 = UpAns[k];
                                DownResults.ultUltM1 = DownAns[k];
                                break;
                            case 4: UpResults.UltTm1Abe = UpAns[k];
                                DownResults.UltTm1Abe = DownAns[k];
                                break;
                            case 5: UpResults.UltVwapM1 = UpAns[k];
                                DownResults.UltVwapM1 = DownAns[k];
                                break;
                            /*case 5: UpResults.Estocastico = UpAns[k];
                                DownResults.Estocastico = DownAns[k];
                                break;
                            case 6: UpResults.UltVwapM1 = UpAns[k];
                                DownResults.UltVwapM1 = DownAns[k];
                                break;*/
                        }
                    }

                }
                catch (Exception E)
                {
                    GotValues = false;
                }
            }
            

            _UpResults = UpResults;
            _DownResults = DownResults;

            return GotValues;
            
        }

    }   

    class Values
    {
        public double ultVwap;
        public double ultAbe;        
        public double ultUltM1;
        public double UltTm1Abe;
        public double UltVwapM1;
        public double Estocastico;
        public double RefValue;
        public double Var;
        public double Cte;
        public int Up;
        public int Down;        
    }
}
