using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using NestDLL;
using System.Data;
using NCommonTypes;
using System.Windows.Forms;
using System.Drawing;

namespace LiveTrade2
{
    public sealed class GlobalVars
    {
        static GlobalVars instance = null;
        static readonly object padlock = new object();
        public System.Diagnostics.Stopwatch MasterClock;

        public int RunCounter = 0;

        //public StreamWriter fs = new StreamWriter(@"C:\LiveTrade\StrongOpen_Dados.txt");

        public DateTime LastQuoteReceived = new DateTime(1900, 1, 1);
        public List<string> Destinations = new List<string>();
        public List<string> Portfolios = new List<string>();

        public Dictionary<int, string> Books = new Dictionary<int, string>();
        public Dictionary<int, string> Sections = new Dictionary<int, string>();

        public string[,] AccountNumbers;

        public bool appStarting = false;
        public bool appClosing = false;
        public int CheckOrAGG = 1;

        public string ConnectIP = "";
        public int ConnectPort = 0;

        public int GridColorCode = 0;
        public Color GridBackColor = new Color();
        public Color GridForeColor = new Color();
        public Color GridTitleColor = new Color();
        public Color GridPositiveColor = new Color();
        public Color GridSpacerColor = new Color();
        public Color GridChangeColor = new Color();

        GlobalVars()
        {

        }

        public static GlobalVars Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new GlobalVars();
                        instance.MasterClock = new System.Diagnostics.Stopwatch();
                        instance.MasterClock.Start();
                    }
                    return instance;
                }
            }
        }

        public void LoadConnDefs()
        {
            if (File.Exists(@"C:\LiveTrade\ConnDefs.cfg"))
            {
                StreamReader sr = new StreamReader(@"C:\LiveTrade\ConnDefs.cfg");
                string tempLine = "";
                while ((tempLine = sr.ReadLine()) != null)
                {
                    if (tempLine.Contains("="))
                    {
                        string[] curLine = tempLine.Split('=');
                        if (curLine[0] == "ConnectIP") ConnectIP = curLine[1];
                        if (curLine[0] == "ConnectPort") ConnectPort = int.Parse(curLine[1]);
                    }
                }
            }
            else
            {
                ConnectIP = "192.168.0.215";
                ConnectPort = 15432;
            }
        }

        public void LoadAccountNumbers()
        {
            string tempLine = "";
            string sFileAccounts = "";
            int curCounter = 0;

            // Adicionei para evitar usar as contas de produção ao longo de testes
            if (File.Exists(@"C:\LiveTrade\TS_Accounts.csv"))
                sFileAccounts = @"C:\LiveTrade\TS_Accounts.csv";
            else
                sFileAccounts = @"T:\Resources\Accounts.csv";

            StreamReader sr = new StreamReader(sFileAccounts);
            while ((tempLine = sr.ReadLine()) != null) { if (tempLine.Length > 0) { if (tempLine.Substring(0, 1) != "#") curCounter++; } }
            sr.Close();

            AccountNumbers = new string[curCounter, 5];
            curCounter = 0;

            sr = new StreamReader(sFileAccounts);
            while ((tempLine = sr.ReadLine()) != null)
            {
                if (tempLine.Length > 0)
                {
                    if (tempLine.Substring(0, 1) != "#")
                    {
                        string[] tempParts = tempLine.Split(';');
                        AccountNumbers[curCounter, 0] = tempParts[0];
                        AccountNumbers[curCounter, 1] = tempParts[1];
                        AccountNumbers[curCounter, 2] = tempParts[2];
                        AccountNumbers[curCounter, 3] = tempParts[3];
                        AccountNumbers[curCounter, 4] = tempParts[4];
                        if (!Destinations.Contains(tempParts[0])) Destinations.Add(tempParts[0]);
                        if (!Portfolios.Contains(tempParts[1])) Portfolios.Add(tempParts[1]);
                        curCounter++;
                    }
                }
            }
            sr.Close();

        }

        public string GetAccountNumber(string Portfolio, string Broker)
        {
            for (int i = 0; i < AccountNumbers.Length / 5; i++)
            {
                if (AccountNumbers[i, 0] == Broker)
                {
                    if (AccountNumbers[i, 1] == Portfolio)
                    {
                        return AccountNumbers[i, 2];
                        break;
                    }
                }
            }
            return "0";
        }

        public string GetPortName(string Account)
        {
            for (int i = 0; i < AccountNumbers.Length / 5; i++)
            {
                if (AccountNumbers[i, 2] == Account)
                {
                    return AccountNumbers[i, 1];
                    break;
                }
            }
            return "";
        }

        public string GetSessionType(string Account)
        {
            for (int i = 0; i < AccountNumbers.Length / 5; i++)
            {
                if (AccountNumbers[i, 2] == Account)
                {
                    return AccountNumbers[i, 3];
                    break;
                }
            }
            return "";
        }

        private Dictionary<string, SymbolData> SymbolToIdSecurity = new Dictionary<string, SymbolData>();

        public int getIdSecurity(string Symbol)
        {
            SymbolData tempVal;

            tempVal = getSymbolData(Symbol);

            if (tempVal != null)
            {
                return tempVal.IdSecurity;
            }

            return 0;
        }

        public int getIdInstrument(string Symbol)
        {
            SymbolData tempVal;

            tempVal = getSymbolData(Symbol);

            if (tempVal != null)
            {
                return tempVal.IdInstrument;
            }

            return 0;
        }

        public int getIdPrimaryExchange(string Symbol)
        {
            SymbolData tempVal;

            tempVal = getSymbolData(Symbol);

            if (tempVal != null)
            {
                return tempVal.IdPrimaryExchange;
            }

            return 0;
        }

        public Sources getDataSource(string Symbol)
        {
            SymbolData tempVal;

            tempVal = getSymbolData(Symbol);

            if (tempVal != null)
            {
                return tempVal.DataSource;
            }

            return 0;
        }

        private SymbolData getSymbolData(string Symbol)
        {
            SymbolData tempVal;
            DataTable result;

            if (Symbol.Length > 0)
            {
                if (Symbol.Substring(0, 1) == "'") return null;
            }
            if (SymbolToIdSecurity.TryGetValue(Symbol, out tempVal))
            {
                return tempVal;
            }
            else
            {
                using (newNestConn curConn = new newNestConn())
                {
                    string SQLExpresion = "SELECT IDSECURITY, IDINSTRUMENT, IdPrimaryExchange, RTUpdateSource FROM NESTDB.DBO.TB001_SECURITIES (NOLOCK) WHERE EXCHANGETICKER = '" + Symbol + "'";
                    result = curConn.Return_DataTable(SQLExpresion);
                    if (result.Rows.Count != 0)
                    {
                        SymbolData curSymbolData = new SymbolData();
                        curSymbolData.IdSecurity = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][0]);
                        curSymbolData.IdInstrument = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][1]);
                        curSymbolData.IdPrimaryExchange = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][2]);
                        curSymbolData.DataSource = (Sources)((int)NestDLL.Utils.ParseToDouble(result.Rows[0][3]));
                        SymbolToIdSecurity.Add(Symbol, curSymbolData);
                        return curSymbolData;
                    }

                    SQLExpresion = "SELECT IDSECURITY, IDINSTRUMENT, IdPrimaryExchange, RTUpdateSource FROM NESTDB.DBO.TB001_SECURITIES (NOLOCK) WHERE NESTTICKER = '" + Symbol + "'";
                    result = curConn.Return_DataTable(SQLExpresion);
                    if (result.Rows.Count != 0)
                    {
                        SymbolData curSymbolData = new SymbolData();
                        curSymbolData.IdSecurity = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][0]);
                        curSymbolData.IdInstrument = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][1]);
                        curSymbolData.IdPrimaryExchange = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][2]);
                        curSymbolData.DataSource = (Sources)((int)NestDLL.Utils.ParseToDouble(result.Rows[0][3]));
                        SymbolToIdSecurity.Add(Symbol, curSymbolData);
                        return curSymbolData;
                    }

                    SQLExpresion = "SELECT IDSECURITY, IDINSTRUMENT, IdPrimaryExchange, RTUpdateSource FROM NESTDB.DBO.TB001_SECURITIES (NOLOCK) WHERE REUTERSTICKER = '" + Symbol + "'";
                    result = curConn.Return_DataTable(SQLExpresion);
                    if (result.Rows.Count != 0)
                    {
                        SymbolData curSymbolData = new SymbolData();
                        curSymbolData.IdSecurity = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][0]);
                        curSymbolData.IdInstrument = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][1]);
                        curSymbolData.IdPrimaryExchange = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][2]);
                        curSymbolData.DataSource = (Sources)((int)NestDLL.Utils.ParseToDouble(result.Rows[0][3]));
                        SymbolToIdSecurity.Add(Symbol, curSymbolData);
                        return curSymbolData;
                    }
                }
            }
            return null;
        }

        public void LoadAllSymbolData()
        {
            DataTable result;

            using (newNestConn curConn = new newNestConn())
            {
                string SQLExpresion = " SELECT Symbol, MAX(IdSecurity) IdSecurity, MAX(IdInstrument) IdInstrument, MAX(IdPrimaryExchange) IdPrimaryExchange, MAX(RTUpdateSource) RTUpdateSource" +
                                    " FROM  " +
                                    " ( " +
                                    " SELECT COALESCE(ExchangeTicker, NestTicker) Symbol, MAX(IdSecurity) IdSecurity, MAX(IdInstrument) IdInstrument, MAX(IdPrimaryExchange) IdPrimaryExchange, MAX(RTUpdateSource) RTUpdateSource" +
                                    " FROM NESTDB.dbo.Tb001_Securities " +
                                    " WHERE COALESCE(ExchangeTicker, NestTicker) <> '' AND (Expiration>=GETDATE() OR Expiration='1900-01-01') " +
                                    " GROUP BY COALESCE(ExchangeTicker, NestTicker) " +
                                    " UNION ALL " +
                                    " SELECT ImagineTicker Symbol, MAX(IdSecurity) IdSecurity, MAX(IdInstrument) IdInstrument, MAX(IdPrimaryExchange) IdPrimaryExchange, MAX(RTUpdateSource) RTUpdateSource " +
                                    " FROM NESTDB.dbo.Tb001_Securities " +
                                    " WHERE ImagineTicker <> '' AND (Expiration>=GETDATE() OR Expiration='1900-01-01') AND IdInstrument<>5 AND IdCurrency=1042 AND IdInstrument<>3 " +
                                    " GROUP BY ImagineTicker " +
                                    " ) X " +
                                    " GROUP BY Symbol " +
                                    " ORDER BY Symbol";

                result = curConn.Return_DataTable(SQLExpresion);
                if (result.Rows.Count != 0)
                {
                    foreach (DataRow curRow in result.Rows)
                    {
                        SymbolData curSymbolData = new SymbolData();
                        curSymbolData.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["IdSecurity"]);
                        curSymbolData.IdInstrument = (int)NestDLL.Utils.ParseToDouble(curRow["IdInstrument"]);
                        curSymbolData.IdPrimaryExchange = (int)NestDLL.Utils.ParseToDouble(curRow["IdPrimaryExchange"]);
                        curSymbolData.DataSource = (Sources)((int)NestDLL.Utils.ParseToDouble(curRow["RTUpdateSource"]));
                        SymbolToIdSecurity.Add(curRow["Symbol"].ToString(), curSymbolData);
                    }
                }
            }
        }

        public DataTable dtBooks;
        public DataTable dtSections;
        public DataTable dtPortfolios;

        public void LoadBookSection()
        {
            using (newNestConn curConn = new newNestConn())
            {
                dtBooks = curConn.Return_DataTable("SELECT Id_Book, Book FROM NESTDB.dbo.Tb400_Books  ORDER BY Book ");
                dtSections = curConn.Return_DataTable("SELECT Id_Section, Section FROM NESTDB.dbo.Tb404_Section ORDER BY Section");
            }

            foreach (DataRow curRow in dtBooks.Rows)
            {
                Books.Add((int)NestDLL.Utils.ParseToDouble(curRow["Id_Book"].ToString()), curRow["Book"].ToString());
            }

            foreach (DataRow curRow in dtSections.Rows)
            {
                Sections.Add((int)NestDLL.Utils.ParseToDouble(curRow["Id_Section"].ToString()), curRow["Section"].ToString());
            }
        }

        public void LoadSection(int iBook)
        {
            using (newNestConn curConn = new newNestConn())
            {
                dtSections = curConn.Return_DataTable(string.Format("Select Id_Section,Section from VW_Book_Strategies where Id_Book = {0} ORDER BY Section, Id_Section", iBook.ToString()));
            }
        }

        class SymbolData
        {
            public int IdSecurity = 0;
            public int IdInstrument = 0;
            public int IdPrimaryExchange = 0;
            public Sources DataSource = Sources.Bovespa;
        }

        public void ReadLimits()
        {
            if (!File.Exists(@"C:\LiveTrade\LTLimits.txt"))
            {
                StreamWriter sw = new StreamWriter(@"C:\LiveTrade\LTLimits.txt");

                sw.WriteLine("[MaxOrderAmount]=10000");
                sw.WriteLine("[MaxOrderShares]=10000");
                sw.WriteLine("[MaxTotalGross]=10000");
                sw.WriteLine("[MaxTotalNet]=10000");
                sw.WriteLine("[MaxTotalShares]=1000");

                sw.WriteLine("[MaxContractsDI1]=1");
                sw.WriteLine("[MaxContractsIND]=1");
                sw.WriteLine("[MaxContractsDOL]=1");

                sw.Close();
            }

            #region ## Comentado
            /*
            if (FIXConnections.Instance.curFixConn != null)
            {
                StreamReader sr = new StreamReader(@"C:\LiveTrade\LTLimits.txt");

                string tempLine = "";

                while ((tempLine = sr.ReadLine()) != null)
                {
                    if (tempLine.Contains("[MaxOrderAmount]")) FIXConnections.Instance.curFixConn.curLimits.MaxOrderAmount = double.Parse(tempLine.Split('=')[1]);
                    if (tempLine.Contains("[MaxOrderShares]")) FIXConnections.Instance.curFixConn.curLimits.MaxOrderShares = double.Parse(tempLine.Split('=')[1]);
                    if (tempLine.Contains("[MaxTotalGross]")) FIXConnections.Instance.curFixConn.curLimits.MaxTotalGross = double.Parse(tempLine.Split('=')[1]);
                    if (tempLine.Contains("[MaxTotalNet]")) FIXConnections.Instance.curFixConn.curLimits.MaxTotalNet = double.Parse(tempLine.Split('=')[1]);
                    if (tempLine.Contains("[MaxTotalShares]")) FIXConnections.Instance.curFixConn.curLimits.MaxTotalShares = double.Parse(tempLine.Split('=')[1]);
                }
            }
            */
            #endregion
        }

        public void SetColorCode(int ColorCode)
        {
            GridColorCode = ColorCode;
            switch (ColorCode)
            {
                case 1:
                    GridBackColor = Color.Black;
                    GridForeColor = Color.Lime;
                    GridTitleColor = Color.White;
                    GridPositiveColor = Color.Lime;
                    GridSpacerColor = Color.FromArgb(20, 20, 20);
                    GridChangeColor = Color.Navy;
                    break;
                case 2:
                    GridBackColor = Color.White;
                    GridForeColor = Color.Black;
                    GridTitleColor = Color.Black;
                    GridPositiveColor = Color.Green;
                    GridSpacerColor = Color.FromArgb(250, 250, 250);
                    GridChangeColor = Color.LightBlue;
                    break;
                case 3:
                    GridBackColor = Color.Black;
                    GridForeColor = Color.White;
                    GridTitleColor = Color.White;
                    GridPositiveColor = Color.Lime;
                    GridSpacerColor = Color.FromArgb(20, 20, 20);
                    GridChangeColor = Color.Navy;
                    break;
                default:
                    break;

            }
        }
    }
}
