using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using STATCONNECTORCLNTLib;
using StatConnectorCommonLib;
using STATCONNECTORSRVLib;
using System.IO;
using System.Data;
using NestDLL;

namespace OppOp
{
    class Calculator
    {
        int IdSecurity;
        DateTime IniDate, EndDate;

        public Dictionary<DateTime, TickerData> TickerDataList = new Dictionary<DateTime, TickerData>();
        public List<DateTime> RunDatesList;
        
        public Calculator(int _IdSecurity,DateTime _IniDate, DateTime _EndDate)
        {
            RunDatesList = Utils.getBovOpenDates(_IniDate);
            RunDatesList.Reverse(0, RunDatesList.Count);

            IdSecurity = _IdSecurity;
            IniDate = _IniDate;
            EndDate = _EndDate;

            if (IdSecurity == 347)
            { }

            foreach (DateTime RunDate in RunDatesList)
            {
                if ((RunDate.DayOfWeek != DayOfWeek.Saturday) && (RunDate.DayOfWeek != DayOfWeek.Sunday) && RunDate <= _EndDate)
                {
                    string CheckDateSQL = "SELECT SrValue " +
                                            "FROM dbo.Tb050_Preco_Acoes_Onshore " +
                                            "WHERE SrType IN (1,3,4,5,8) AND IdSecurity = " + _IdSecurity + " AND SrDate = '" + RunDate.ToString("yyyyMMdd") + "' AND Source = 1";
                    bool Traded = true;

                    using (newNestConn conn = new newNestConn())
                    {
                        DataTable CheckTable = conn.Return_DataTable(CheckDateSQL);

                        if (CheckTable.Rows.Count == 5)
                        {
                            for (int i = 0; i < CheckTable.Rows.Count; i++)
                            {
                                if (double.Parse(CheckTable.Rows[i][0].ToString()) == 0 || double.IsNaN(double.Parse(CheckTable.Rows[i][0].ToString())))
                                {
                                    Traded = false;
                                }
                            }
                        }
                        else
                        {
                            Traded = false;
                        }
                    }

                    if (Traded)
                    {
                        TickerDataList.Add(RunDate, new TickerData(IdSecurity, RunDate));
                    }
                }
            }
            Calculate();
            
        }

        private void Calculate()
        {
            for (int i = 0; i < TickerDataList.Count - 1; i++)
            {
                if (i == 62)
                { }

                if (TickerDataList.ElementAt(i).Key < new DateTime(2011, 03, 01) && TickerDataList.ElementAt(i).Value.Security.IdSecurity == 82936)
                { }
                                
                TickerDataList.ElementAt(i).Value.CloseVWAP = Math.Log(TickerDataList.ElementAt(i + 1).Value.Security.Last / TickerDataList.ElementAt(i + 1).Value.Security.VWAP) / TickerDataList.ElementAt(i).Value.Security.Vol;
                TickerDataList.ElementAt(i).Value.CloseOpenPD = Math.Log(TickerDataList.ElementAt(i + 1).Value.Security.Last / TickerDataList.ElementAt(i + 1).Value.Security.Open) / TickerDataList.ElementAt(i).Value.Security.Vol;
                TickerDataList.ElementAt(i).Value.OpenClosePD = Math.Log(TickerDataList.ElementAt(i).Value.Security.Open / TickerDataList.ElementAt(i + 1).Value.Security.Last) / TickerDataList.ElementAt(i).Value.Security.Vol;
                TickerDataList.ElementAt(i).Value.CloseOpen = Math.Log(TickerDataList.ElementAt(i).Value.Security.Last / TickerDataList.ElementAt(i).Value.Security.Open);

                if (double.IsInfinity(TickerDataList.ElementAt(i).Value.CloseVWAP))
                { }

                double RefDown = - (TickerDataList.ElementAt(1).Value.Security.Vol / 2);
                double RefUp = TickerDataList.ElementAt(1).Value.Security.Vol / 2;
                
                TickerDataList.ElementAt(i).Value.Down = TickerDataList.ElementAt(i).Value.CloseOpen < RefDown ? 1 : 0;
                TickerDataList.ElementAt(i).Value.Up = TickerDataList.ElementAt(i).Value.CloseOpen > RefUp ? 1 : 0;
                
                /*
                Log.WriteLog(TickerDataList.ElementAt(i).Value.Security.ToString() + ";" + TickerDataList.ElementAt(i).Key.ToString("yyyyMMdd") + ";" + TickerDataList.ElementAt(i).Value.Security.Open + ";" +
                                TickerDataList.ElementAt(i).Value.Security.Last + ";" + TickerDataList.ElementAt(i).Value.Security.VWAP + ";" +
                                TickerDataList.ElementAt(i).Value.CloseVWAP + ";" + TickerDataList.ElementAt(i).Value.CloseOpenPD + ";" +
                                TickerDataList.ElementAt(i).Value.OpenClosePD + ";" + TickerDataList.ElementAt(i).Value.Down + ";" +
                                TickerDataList.ElementAt(i).Value.Up + ";" + TickerDataList.ElementAt(i).Value.CloseOpen);
                 */
            }
            //TickerDataList.ElementAt(62).Value.Down = 0;

            CallAllRegret();
        }

        private void CallAllRegret()
        {
            StatConnector RConn = new StatConnector();

            RConn.Init("R");

            List<double> CloseVWAPList = new List<double>();
            List<double> CloseOpenPDList = new List<double>();
            List<double> OpenClosePDList = new List<double>();
            List<int> UpList = new List<int>();
            List<int> DownList = new List<int>();

            int i = 1, pos = 0;

            while (pos < TickerDataList.Count - 101)
            {
                if (i <= 100)
                {
                    CloseVWAPList.Add(TickerDataList.ElementAt(i).Value.CloseVWAP);
                    CloseOpenPDList.Add(TickerDataList.ElementAt(i).Value.CloseOpenPD);
                    OpenClosePDList.Add(TickerDataList.ElementAt(i).Value.OpenClosePD);
                    DownList.Add(TickerDataList.ElementAt(i).Value.Down);
                    UpList.Add(TickerDataList.ElementAt(i).Value.Up);
                    if (i == 100)
                    { }
                    i++;
                    
                }
                else
                {
                    try
                    {
                        #region RInterface

                        double[,] RResults = CallRegret(RConn, CloseVWAPList, CloseOpenPDList, OpenClosePDList, UpList, DownList);

                        for (int j = 0; j < 2; j++)
                        {
                            for (int k = 0; k < 4; k++)
                            {
                                switch (j)
                                {
                                    case 0:
                                        switch (k)
                                        {
                                            case 0: TickerDataList.ElementAt(pos).Value.CteUp = RResults[0, 0];
                                                break;
                                            case 1: TickerDataList.ElementAt(pos).Value.CloseVWAPRegUp = RResults[0, 1];
                                                break;
                                            case 2: TickerDataList.ElementAt(pos).Value.CloseOpenPDRegUp = RResults[0, 2];
                                                break;
                                            case 3: TickerDataList.ElementAt(pos).Value.OpenClosePDRegUp = RResults[0, 3];
                                                break;
                                        }
                                        break;
                                    case 1:
                                        switch (k)
                                        {
                                            case 0: TickerDataList.ElementAt(pos).Value.CteDown = RResults[1, 0];
                                                break;
                                            case 1: TickerDataList.ElementAt(pos).Value.CloseVWAPRegDown = RResults[1, 1];
                                                break;
                                            case 2: TickerDataList.ElementAt(pos).Value.CloseOpenPDRegDown = RResults[1, 2];
                                                break;
                                            case 3: TickerDataList.ElementAt(pos).Value.OpenClosePDRegDown = RResults[1, 3];
                                                break;
                                        }
                                        break;
                                }
                            }
                        }

                        #endregion

                        TickerDataList.ElementAt(pos).Value.ProbUp =
                                                                    Math.Exp(TickerDataList.ElementAt(pos).Value.CteUp +
                                                                    TickerDataList.ElementAt(pos).Value.CloseVWAPRegUp *
                                                                    TickerDataList.ElementAt(pos).Value.CloseVWAP +
                                                                    TickerDataList.ElementAt(pos).Value.CloseOpenPDRegUp *
                                                                    TickerDataList.ElementAt(pos).Value.CloseOpenPD +
                                                                    TickerDataList.ElementAt(pos).Value.OpenClosePDRegUp *
                                                                    TickerDataList.ElementAt(pos).Value.OpenClosePD
                                                                    )
                                                                    /
                                                                    (1 + Math.Exp(TickerDataList.ElementAt(pos).Value.CteUp +
                                                                    TickerDataList.ElementAt(pos).Value.CloseVWAPRegUp *
                                                                    TickerDataList.ElementAt(pos).Value.CloseVWAP +
                                                                    TickerDataList.ElementAt(pos).Value.CloseOpenPDRegUp *
                                                                    TickerDataList.ElementAt(pos).Value.CloseOpenPD +
                                                                    TickerDataList.ElementAt(pos).Value.OpenClosePDRegUp *
                                                                    TickerDataList.ElementAt(pos).Value.OpenClosePD));

                        TickerDataList.ElementAt(pos).Value.ProbDown =
                                                                    Math.Exp(TickerDataList.ElementAt(pos).Value.CteDown +
                                                                    TickerDataList.ElementAt(pos).Value.CloseVWAPRegDown *
                                                                    TickerDataList.ElementAt(pos).Value.CloseVWAP +
                                                                    TickerDataList.ElementAt(pos).Value.CloseOpenPDRegDown *
                                                                    TickerDataList.ElementAt(pos).Value.CloseOpenPD +
                                                                    TickerDataList.ElementAt(pos).Value.OpenClosePDRegDown *
                                                                    TickerDataList.ElementAt(pos).Value.OpenClosePD
                                                                    )
                                                                    /
                                                                    (1 + Math.Exp(TickerDataList.ElementAt(pos).Value.CteDown +
                                                                    TickerDataList.ElementAt(pos).Value.CloseVWAPRegDown *
                                                                    TickerDataList.ElementAt(pos).Value.CloseVWAP +
                                                                    TickerDataList.ElementAt(pos).Value.CloseOpenPDRegDown *
                                                                    TickerDataList.ElementAt(pos).Value.CloseOpenPD +
                                                                    TickerDataList.ElementAt(pos).Value.OpenClosePDRegDown *
                                                                    TickerDataList.ElementAt(pos).Value.OpenClosePD));



                        CloseVWAPList.RemoveAt(0);
                        CloseOpenPDList.RemoveAt(0);
                        OpenClosePDList.RemoveAt(0);
                        DownList.RemoveAt(0);
                        UpList.RemoveAt(0);

                        CloseVWAPList.Add(TickerDataList.ElementAt(i).Value.CloseVWAP);
                        CloseOpenPDList.Add(TickerDataList.ElementAt(i).Value.CloseOpenPD);
                        OpenClosePDList.Add(TickerDataList.ElementAt(i).Value.OpenClosePD);
                        DownList.Add(TickerDataList.ElementAt(i).Value.Down);
                        UpList.Add(TickerDataList.ElementAt(i).Value.Up);

                        i++;
                        pos++;
                        if (pos == 130)
                        { }
                    }
                    catch(Exception E)
                    { }

                }
            }
        }

        private double[,] CallRegret(StatConnector RConn, List<double> _CloseVWAPList, List<double> _CloseOpenPDList, List<double> _OpenClosePDList, List<int> _UpList, List<int> _DownList)
        {
            object[] Up = new object[100];
            object[] Down = new object[100];
            object[] CloseVwap = new object[100];
            object[] CloseOpenPD = new object[100];
            object[] OpenClosePD = new object[100];

            for (int i = 0; i < 100; i++)
            {
                Up[i] = _UpList[i];
                Down[i] = _DownList[i];
                CloseVwap[i] = _CloseVWAPList[i];
                CloseOpenPD[i] = _CloseOpenPDList[i];
                OpenClosePD[i] = _OpenClosePDList[i];
            }

            RConn.SetSymbol("Up", Up);
            RConn.SetSymbol("Down", Down);
            RConn.SetSymbol("CloseVwap", CloseVwap);
            RConn.SetSymbol("CloseOpenPD", CloseOpenPD);
            RConn.SetSymbol("OpenClosePD", OpenClosePD);

            try
            {
                RConn.EvaluateNoReturn("UpRegret <- glm(Up~CloseVwap+CloseOpenPD+OpenClosePD, family=binomial(link = logit))$coefficients");
            }
            catch
            { }

            try
            {
                RConn.EvaluateNoReturn("DownRegret <- glm(Down~CloseVwap+CloseOpenPD+OpenClosePD, family=binomial(link = logit))$coefficients");
            }
            catch 
            { }
            var UpRegret = RConn.GetSymbol("UpRegret");
            var DownRegret = RConn.GetSymbol("DownRegret");

            double[,] Results = new double[2,4];

            for(int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch(i)
                    {
                        case 0: Results[i,j] = UpRegret[j];
                                break;
                        case 1: Results[i,j] = DownRegret[j];
                                break;
                    }
                }
            }

            return Results;

        }

        public string ToString()
        {
            using (newNestConn conn = new newNestConn())
            {
                return conn.Return_DataTable("SELECT NestTicker FROM dbo.Tb001_Securities WHERE IdSecurity =" + IdSecurity).Rows[0][0].ToString();
            }
        }
                
    }

    class TickerData
    {
        public Ticker Security;
        public DateTime RunDate;

        public double CloseVWAP;
        public double CloseOpenPD;
        public double OpenClosePD;

        public int Up;
        public int Down;
        public double CloseOpen;

        public double CteUp;
        public double CloseVWAPRegUp;
        public double CloseOpenPDRegUp;
        public double OpenClosePDRegUp;
        public double ProbUp;

        public double CteDown;
        public double CloseVWAPRegDown;
        public double CloseOpenPDRegDown;
        public double OpenClosePDRegDown;
        public double ProbDown;

        public TickerData(int _Security, DateTime _RunDate)
        {
            Security = new Ticker(_Security, _RunDate);
            RunDate = _RunDate;
        }        
    }

    class Log
    {
        public static void WriteLog(string LogString)
        {
            StreamWriter srWriter = new StreamWriter(@"C:\Temp\Logs\OppOp_Log.txt",true);
            srWriter.WriteLine(LogString);
            srWriter.Close();
        }
    }
}
