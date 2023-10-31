using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using NestDLL;
using System.Data;

namespace LBHistCalc
{
    public sealed class GlobalVars
    {
        static GlobalVars instance = null;
        static readonly object padlock = new object();

        public int RunCounter = 0;

        //public StreamWriter fs = new StreamWriter(@"C:\temp\StrongOpen_Dados.txt");

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
                    }
                    return instance;
                }
            }
        }

        #region PortData

        public List<AccountData> AccountPortList = new List<AccountData>();

        public void LoadAccounts()
        {
            AccountPortList.Clear();

            using (newNestConn curConn = new newNestConn())
            {
                DateTime iniTime = DateTime.Now;
                string StringSQL = "SELECT * FROM NESTDB.dbo.VW_PortAccounts";

                DataTable curTable = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in curTable.Rows)
                {
                    AccountData curAccountData = new AccountData();
                    curAccountData.IdAccount = (int)NestDLL.Utils.ParseToDouble(curRow["Id_Account"]);
                    curAccountData.IdPortfolio = (int)NestDLL.Utils.ParseToDouble(curRow["Id_Portfolio"]);
                    curAccountData.IdPortType = (int)NestDLL.Utils.ParseToDouble(curRow["Id_Port_Type"]);
                    AccountPortList.Add(curAccountData);
                }
            }
        }

        public int GetPort(int IdAccount, int IdPortType)
        {
            foreach (AccountData curAccountData in AccountPortList)
            {
                if (curAccountData.IdAccount == IdAccount && curAccountData.IdPortType == IdPortType)
                {
                    return curAccountData.IdPortfolio;
                }
            }

            return 0;
        }

        public List<AccountData> GetPorts(int IdAccount)
        {
            List<AccountData> ReturnValues = new List<AccountData>();

            foreach (AccountData curAccountData in AccountPortList)
            {
                if (curAccountData.IdAccount == IdAccount)
                {
                    ReturnValues.Add(curAccountData);
                }
            }

            return ReturnValues;
        }

        public Dictionary<int, int> PortCurrency = new Dictionary<int, int>();

        public void LoadPortCurrency()
        {
            using (newNestConn curConn = new newNestConn())
            {
                DateTime iniTime = DateTime.Now;
                string StringSQL = "SELECT * FROM NESTDB.dbo.Tb002_Portfolios WHERE Id_Portfolio>0";

                DataTable curTable = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in curTable.Rows)
                {
                    PortCurrency.Add((int)NestDLL.Utils.ParseToDouble(curRow["Id_Portfolio"]), (int)NestDLL.Utils.ParseToDouble(curRow["Port_Currency"]));
                }
            }
        }

        public int getPortCurrency(int IdPortfolio)
        {
            int IdCurrency = 0;
            if (PortCurrency.TryGetValue(IdPortfolio, out IdCurrency))
            {
                return IdCurrency;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        #region SecurityData

        private SortedDictionary<string, SymbolData> SymbolToIdSecurity = new SortedDictionary<string, SymbolData>();

        public int getIdInstrument(int IdSecurity, DateTime RefDate)
        {
            SymbolData tempVal;

            tempVal = getSymbolData(IdSecurity, RefDate);

            if (tempVal != null)
            {
                return tempVal.IdInstrument;
            }

            return 0;
        }

        public double getLotSize(int IdSecurity, DateTime RefDate)
        {
            SymbolData tempVal;

            tempVal = getSymbolData(IdSecurity, RefDate);

            if (tempVal != null)
            {
                return tempVal.LotSize;
            }

            return 0;
        }

        public int getIdPrimaryExchange(int IdSecurity, DateTime RefDate)
        {
            SymbolData tempVal;

            tempVal = getSymbolData(IdSecurity, RefDate);

            if (tempVal != null)
            {
                return tempVal.IdPrimaryExchange;
            }

            return 0;
        }

        private SymbolData getSymbolData(int IdSecurity, DateTime RefDate)
        {
            SymbolData tempVal;
            DataTable result;

            if (SymbolToIdSecurity.TryGetValue(RefDate.ToString("yyyyMMdd") + IdSecurity, out tempVal))
            {
                return tempVal;
            }
            else
            {
                using (newNestConn curConn = new newNestConn())
                {
                    string SQLExpresion = "SELECT IDSECURITY, IDINSTRUMENT, IdPrimaryExchange, RoundLot FROM NESTDB.DBO.FCN001_Securities_All('" + RefDate.ToString("yyyy-MM-dd") +"') WHERE IdSecurity = '" + IdSecurity + "'";
                    //string SQLExpresion = "SELECT IDSECURITY, IDINSTRUMENT, IdPrimaryExchange, RoundLot FROM NESTDB.DBO.TB001_SECURITIES (NOLOCK) WHERE IdSecurity = '" + IdSecurity + "'";
                    result = curConn.Return_DataTable(SQLExpresion);
                    if (result.Rows.Count != 0)
                    {
                        SymbolData curSymbolData = new SymbolData();
                        curSymbolData.IdSecurity = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][0]);
                        curSymbolData.IdInstrument = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][1]);
                        curSymbolData.IdPrimaryExchange = (int)NestDLL.Utils.ParseToDouble(result.Rows[0][2]);
                        curSymbolData.LotSize = NestDLL.Utils.ParseToDouble(result.Rows[0][3]);
                        SymbolToIdSecurity.Add(RefDate.ToString("yyyyMMdd") + IdSecurity, curSymbolData);
                        return curSymbolData;
                    }
                }
            }
            return null;
        }

        public void LoadAllSymbolData(DateTime RefDate)
        {
            DataTable result;

            using (newNestConn curConn = new newNestConn())
            {
                string SQLExpresion = " SELECT A.IdSecurity, IdInstrument, IdPrimaryExchange, RoundLot " +
                                        " FROM NESTDB.dbo.Tb001_Securities A  " +
                                        " INNER JOIN (SELECT [Id Ticker] FROM NESTDB.dbo.Tb000_Historical_Positions WHERE [Date Now]>='" + DateTime.Now.Subtract(new TimeSpan(30, 0, 0, 0)).Date.ToString("yyyy-MM-dd") + "' GROUP BY [Id Ticker]) B " +
                                        " ON A.IdSecurity=B.[Id Ticker]";

                result = curConn.Return_DataTable(SQLExpresion);
                if (result.Rows.Count != 0)
                {
                    foreach (DataRow curRow in result.Rows)
                    {
                        SymbolData curSymbolData = new SymbolData();
                        curSymbolData.IdSecurity = (int)NestDLL.Utils.ParseToDouble(curRow["IdSecurity"]);
                        curSymbolData.IdInstrument = (int)NestDLL.Utils.ParseToDouble(curRow["IdInstrument"]);
                        curSymbolData.IdPrimaryExchange = (int)NestDLL.Utils.ParseToDouble(curRow["IdPrimaryExchange"]);
                        curSymbolData.LotSize = NestDLL.Utils.ParseToDouble(curRow["RoundLot"]);
                        SymbolToIdSecurity.Add(RefDate.ToString("yyyyMMdd") + (int)NestDLL.Utils.ParseToDouble(curRow["IdSecurity"]), curSymbolData);
                    }
                }
            }
        }

        #endregion

        public int getCashId(int IdCurrency)
        {
            switch (IdCurrency)
            {
                case 900: return 1844;
                case 1042: return 5791;
                case 929: return 31057;
                case 933: return 76743;
                default: return 1844; 
            }
        }

        public DataTable dtBooks;
        public DataTable dtSections;

        public void LoadBookSection()
        {
            using (newNestConn curConn = new newNestConn())
            {
                dtBooks = curConn.Return_DataTable("SELECT Id_Book, Book FROM NESTDB.dbo.Tb400_Books");
                dtSections = curConn.Return_DataTable("SELECT Id_Section, Section FROM NESTDB.dbo.Tb404_Section");
            }
        }

        class SymbolData
        {
            public int IdSecurity = 0;
            public int IdInstrument = 0;
            public int IdPrimaryExchange = 0;
            public double LotSize = 0;
        }

        public class AccountData
        {
            public int IdAccount = 0;
            public int IdPortType = 0;
            public int IdPortfolio = 0;
        }
    }
}
