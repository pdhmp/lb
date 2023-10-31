using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using NestDLL;

namespace CyrnelSync
{
    class RiskPortfolioItem
    {
        IRtdServer m_server;
        public object SyncLock = new object();

        public List<RiskPositionItem> RiskPositionItems = new List<RiskPositionItem>();

        private int _IdPortfolio = 0; public int IdPortfolio { get { return _IdPortfolio; } set { _IdPortfolio = value; } }
        private DateTime _RefDate = new DateTime(1900, 01, 01); public DateTime RefDate { get { return _RefDate; } set { _RefDate = value; } }
        private int _NoPositions = 0; public int NoPositions { get { return _NoPositions; } set { _NoPositions = value; } }
        private double _PortValue = 0; public double PortValue { get { return _PortValue; } set { _PortValue = value; } }

        private double _VarPerc = 0; public double VarPerc { get { return _VarPerc; } set { _VarPerc = value; } }
        private double _VarMC = 0; public double VarMC { get { return _VarMC; } set { _VarMC = value; } }
        private double _VarFin = 0; public double VarFin { get { return _VarFin; } set { _VarFin = value; } }
        private double _VarMCFin = 0; public double VarMCFin { get { return _VarMCFin; } set { _VarMCFin = value; } }

        private double _BMFA1 = 0; public double BMFA1 { get { return _BMFA1; } set { _BMFA1 = value; } }
        private double _BMFA2 = 0; public double BMFA2 { get { return _BMFA2; } set { _BMFA2 = value; } }
        private double _BMFA3 = 0; public double BMFA3 { get { return _BMFA3; } set { _BMFA3 = value; } }
        private double _BMFB1 = 0; public double BMFB1 { get { return _BMFB1; } set { _BMFB1 = value; } }
        private double _BMFB2 = 0; public double BMFB2 { get { return _BMFB2; } set { _BMFB2 = value; } }
        private double _BMFB3 = 0; public double BMFB3 { get { return _BMFB3; } set { _BMFB3 = value; } }

        private bool _PositionsRequested = false; public bool PositionsRequested { get { return _PositionsRequested; } }

        public DateTime UpdateTime = new DateTime(1900, 01, 01);

        public RiskPortfolioItem(IRtdServer RTDServer)
        {
            m_server = RTDServer;
        }

        public void Update(int Token, string Value)
        {
            if (Value == "data not found" || Value == "Retrieving Data..." || Value == "." || Value == "" || Value == "Invalid field")
            {
                return;
            }

            if ((int)(Token / 100000) != _IdPortfolio)
            {
                return;
            }

            int DataID = Token - (_IdPortfolio * 100000);

            if (DataID < 100)
            {
                switch (DataID)
                {
                    case 1: _VarPerc = double.Parse(Value); break;
                    case 2: _VarMC = double.Parse(Value); break;
                    case 3: _NoPositions = int.Parse(Value); break;
                    case 4: _PortValue = double.Parse(Value); break;
                    case 5: _RefDate = DateTime.Parse(Value); break;
                    case 6: VarFin = double.Parse(Value); break;
                    case 7: VarMCFin = double.Parse(Value); break;
                    case 11: _BMFA1 = double.Parse(Value); break;
                    case 12: _BMFA2 = double.Parse(Value); break;
                    case 13: _BMFA3 = double.Parse(Value); break;
                    case 14: _BMFB1 = double.Parse(Value); break;
                    case 15: _BMFB2 = double.Parse(Value); break;
                    case 16: _BMFB3 = double.Parse(Value); break;
                    default:
                        break;
                }
            }
            else
            {
                int IdPosition = (int)(DataID / 100);
                RiskPositionItem curRiskPositionItem = RiskPositionItems[IdPosition - 1];
                int PosID = DataID - IdPosition * 100;
                switch (PosID)
                {
                    case 1: curRiskPositionItem.VarP = double.Parse(Value); break;
                    case 2: curRiskPositionItem.VarMC = double.Parse(Value); break;
                    case 3: curRiskPositionItem.Value = double.Parse(Value); break;
                    case 4: curRiskPositionItem.Weight = double.Parse(Value); break;
                    case 5: curRiskPositionItem.Ticker = Value; break;
                    case 6: curRiskPositionItem.VarFin = -double.Parse(Value); break;
                    case 7: curRiskPositionItem.VarMCFin = -double.Parse(Value); break;
                    case 11: curRiskPositionItem.BMFA1 = double.Parse(Value); break;
                    case 12: curRiskPositionItem.BMFA2 = double.Parse(Value); break;
                    case 13: curRiskPositionItem.BMFA3 = double.Parse(Value); break;
                    case 14: curRiskPositionItem.BMFB1 = double.Parse(Value); break;
                    case 15: curRiskPositionItem.BMFB2 = double.Parse(Value); break;
                    case 16: curRiskPositionItem.BMFB3 = double.Parse(Value); break;
                    default:
                        break;
                }
            }
        }

        public void SubscribeData()
        {
            Object[] curTopic = new Object[5];

            curTopic[2] = GetPortCode(_IdPortfolio);
            curTopic[0] = "risk";   
            curTopic[1] = "COMPOSITIONS";
            curTopic[3] = "0";

            curTopic[4] = "Prar2"; m_server.ConnectData(IdPortfolio * 100000 + 1, curTopic, true);
            curTopic[4] = "NUMBEROFSECURITIES"; m_server.ConnectData(IdPortfolio * 100000 + 3, curTopic, true);
            curTopic[4] = "VALUE"; m_server.ConnectData(IdPortfolio * 100000 + 4, curTopic, true);
            curTopic[4] = "DATE"; m_server.ConnectData(IdPortfolio * 100000 + 5, curTopic, true);
            curTopic[4] = "Pvar2"; m_server.ConnectData(IdPortfolio * 100000 + 6, curTopic, true);
            curTopic[4] = "NPvar2"; m_server.ConnectData(IdPortfolio * 100000 + 7, curTopic, true);

            curTopic[4] = "Shock.RelativePL.BMFA1"; m_server.ConnectData(IdPortfolio * 100000 + 11, curTopic, true);
            curTopic[4] = "Shock.RelativePL.BMFA2"; m_server.ConnectData(IdPortfolio * 100000 + 12, curTopic, true);
            curTopic[4] = "Shock.RelativePL.BMFA3"; m_server.ConnectData(IdPortfolio * 100000 + 13, curTopic, true);
            curTopic[4] = "Shock.RelativePL.BMFB1"; m_server.ConnectData(IdPortfolio * 100000 + 14, curTopic, true);
            curTopic[4] = "Shock.RelativePL.BMFB2"; m_server.ConnectData(IdPortfolio * 100000 + 15, curTopic, true);
            curTopic[4] = "Shock.RelativePL.BMFB3"; m_server.ConnectData(IdPortfolio * 100000 + 16, curTopic, true);
        }

        public void SubscribePositionsData()
        {
            if (!_PositionsRequested)
            {
                _PositionsRequested = true;

                Object[] curTopic = new Object[6];

                curTopic[0] = "risk";
                curTopic[1] = "SECURITY";
                curTopic[2] = "NestAcoes";
                curTopic[3] = "LATEST";

                for (int i = 0; i < NoPositions - 1; i++)
                {
                    this.RiskPositionItems.Add(new RiskPositionItem());
                    curTopic[4] = i.ToString();

                    curTopic[5] = "Prar2"; m_server.ConnectData(IdPortfolio * 100000 + (i * 100) + 1, curTopic, true);
                    curTopic[5] = "Ticker"; m_server.ConnectData(IdPortfolio * 100000 + (i * 100) + 5, curTopic, true);
                    curTopic[5] = "Pvar2"; m_server.ConnectData(IdPortfolio * 100000 + (i * 100) + 6, curTopic, true);
                    curTopic[5] = "NPvar2"; m_server.ConnectData(IdPortfolio * 100000 + (i * 100) + 7, curTopic, true);

                    curTopic[5] = "Shock.RelativePL.BMFA1"; m_server.ConnectData(IdPortfolio * 100000 + (i * 100) + 11, curTopic, true);
                    curTopic[5] = "Shock.RelativePL.BMFA2"; m_server.ConnectData(IdPortfolio * 100000 + (i * 100) + 12, curTopic, true);
                    curTopic[5] = "Shock.RelativePL.BMFA3"; m_server.ConnectData(IdPortfolio * 100000 + (i * 100) + 13, curTopic, true);
                    curTopic[5] = "Shock.RelativePL.BMFB1"; m_server.ConnectData(IdPortfolio * 100000 + (i * 100) + 14, curTopic, true);
                    curTopic[5] = "Shock.RelativePL.BMFB2"; m_server.ConnectData(IdPortfolio * 100000 + (i * 100) + 15, curTopic, true);
                    curTopic[5] = "Shock.RelativePL.BMFB3"; m_server.ConnectData(IdPortfolio * 100000 + (i * 100) + 16, curTopic, true);
                }
            }
        }

        public void UpdateDB()
        {
            if (UpdateTime != new DateTime(1900, 01, 01))
            {
                lock (SyncLock)
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        string SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb811_Imagine_Per_Book]([VAR_DateTime],[Id_Portfolio],[Id_Strategy],[Imagine_Book_Name],[Holdings],[VAR_Total],[VAR_Marginal],[VAR_Credit],[VAR_Equity],[VAR_FX],[VAR_IR],[VAR_VOL])" +
                                                        "VALUES('" + UpdateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + this.IdPortfolio.ToString() + ",0, '" + GetPortCode(IdPortfolio) + "', " + this.NoPositions.ToString().Replace(",", ".") + ", " + this.VarFin.ToString().Replace(",", ".") + ", 0, 0, 0, 0, 0, 0)";
                        curConn.ExecuteNonQuery(SQLExpression);

                        SQLExpression = "INSERT INTO [NESTDB].[dbo].[Tb813_Imagine_MCSym]([run_DateTime],[Id_Portfolio],[Imagine_Name],[Min_Value],[Max_Value],[Avg_Value],[Dev_Value],[VAR95_Lo_Value],[VAR95_Lo_EQT],[VAR95_Lo_FX],[VAR95_Lo_IR],[VAR95_Hi_Value],[VAR95_Hi_EQT],[VAR95_Hi_FX],[VAR95_Hi_IR])" +
                                            "VALUES('" + UpdateTime.ToString("yyyy-MM-dd HH:mm:ss") + "', " + this.IdPortfolio.ToString() + ", '" + GetPortCode(IdPortfolio) + "', 0, 0, 0, 0, " + this.VarMCFin.ToString().Replace(",", ".") + ", 0, 0, 0, 0, 0, 0, 0)";
                        curConn.ExecuteNonQuery(SQLExpression);
                    }
                }
            }
        }

        public string GetPortCode(int IdPortfolio)
        {
            switch (IdPortfolio)
            {
                case 4: return "NestEquityHedge"; 
                case 10: return "NestAcoes";
                case 38: return "NestArb"; 
                case 43: return "NestMH";
                case 50: return "NestSulAmericaPrev";
                case 55: return "NestIcatuPrev";
                case 60: return "NestHedge"; 
                default: return ""; 
                                }
        }
            }

    class RiskPositionItem
    {
        private int _IdPosition = 0; public int IdPosition { get { return _IdPosition; } set { _IdPosition = value; } }
        private string _Ticker = ""; public string Ticker { get { return _Ticker; } set { _Ticker = value; } }
        private double _Weight = 0; public double Weight { get { return _Weight; } set { _Weight = value; } }
        private double _Value = 0; public double Value { get { return _Value; } set { _Value = value; } }

        private double _VarP = 0; public double VarP { get { return _VarP; } set { _VarP = value; } }
        private double _VarMC = 0; public double VarMC { get { return _VarMC; } set { _VarMC = value; } }
        private double _VarFin = 0; public double VarFin { get { return _VarFin; } set { _VarFin = value; } }
        private double _VarMCFin = 0; public double VarMCFin { get { return _VarMCFin; } set { _VarMCFin = value; } }

        private double _BMFA1 = 0; public double BMFA1 { get { return _BMFA1; } set { _BMFA1 = value; } }
        private double _BMFA2 = 0; public double BMFA2 { get { return _BMFA2; } set { _BMFA2 = value; } }
        private double _BMFA3 = 0; public double BMFA3 { get { return _BMFA3; } set { _BMFA3 = value; } }
        private double _BMFB1 = 0; public double BMFB1 { get { return _BMFB1; } set { _BMFB1 = value; } }
        private double _BMFB2 = 0; public double BMFB2 { get { return _BMFB2; } set { _BMFB2 = value; } }
        private double _BMFB3 = 0; public double BMFB3 { get { return _BMFB3; } set { _BMFB3 = value; } }
    }

}
