using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using NOrderStager;
using newNFIXConn;
using NestGeneric;
using NCommonTypes;
using NestSymConn;
using NestDLL;

namespace ValueBZ_V2
{
    public class ValueRunner
    {
        #region Strategy Region

        private ValueStrategy curStrategy;
        
        private double _StrategyQTNAV = 0;
        public double StrategyQTNAV
        {
            get { return _StrategyQTNAV; }            
        }

        private double _FUNDQTNAV = 0;

        public ValueRunner()
        {
            Start();
        }

        private void Start()
        {
            _StrategyQTNAV = GetStratNAV();

            StartOrderCalc();                                  

            curStrategy = new ValueStrategy(DateTime.Today);

            StartSocket();            
        }

        private double GetStratNAV()
        {
            string SQLExpression = "SELECT TOP 1 VALOR_PL FROM NESTSRV06.NESTDB.DBO.TB025_VALOR_PL WITH(NOLOCK) WHERE ID_PORTFOLIO = 18 ORDER BY DATA_PL DESC";
            double fundNAV = 0;
            
            using (newNestConn curConn = new newNestConn())
            {
                fundNAV = curConn.Return_Double(SQLExpression);
            }

            _FUNDQTNAV = fundNAV;

            //TODO: Mudar percentual qdo perda exceder 2 dias CDI
            double stratPerc = 0.09;

            double stratNAV = fundNAV * stratPerc;

            return stratNAV;
        }

        #endregion        

        #region FIX Region

        newFIXConn curFixConn;

        public bool IsFixConnected
        {
            get
            {
                return false;// curFixConn.IsAliveSession;
            }
        }

        private void SendStagedOrder(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region OrderCalc Region

        private OrderCalc_Percent curOrderCalcQuant;

        private void StartOrderCalc()
        {
            curOrderCalcQuant = new OrderCalc_Percent(NEnuns.QuantPortfolios.Quant,
                                                      NEnuns.QuantBooks.Quantitative,
                                                      NEnuns.QuantSections.ValueBZ, "QT_VALBZ");

            curOrderCalcQuant.Stager.NewOrderToSend += new EventHandler(SendStagedOrder);
        }        

        private void CalcStagedOrders()
        {           
            curStrategy.StratRecalc();

            curOrderCalcQuant.Clear();
            curOrderCalcQuant.Stager.minOrderAmount = 0;
            curOrderCalcQuant.CalcFromPosition(curStrategy.stratPositions, _StrategyQTNAV, false);
        }

        #endregion

        #region Socket Region

        private NestSocketServer curSocket = new NestSocketServer();

        private void StartSocket()
        {
            curSocket.StartListen(NEnuns.QuantPorts.ValBZ);
            curSocket.NewMessage += new EventHandler(ReceiveMessage);
        }

        void ReceiveMessage(object sender, EventArgs e)
        {
            string curMessage = ((NestSocketServer.MsgEventArgs)e).strMessage;

            string[] curMessagePart = curMessage.Split((char)16);

            #region =========================  CLIENT COMMANDS  =========================

            if (curMessagePart[0] == "CMD")
            {
                if (curMessagePart[1] == "CLEARSTAGED")
                {
                    curOrderCalcQuant.Stager.RemoveAllStagedOrders();
                }

                if (curMessagePart[1] == "CALCSTAGED")
                {
                    CalcStagedOrders(); 
                }

                if (curMessagePart[1] == "UPDATENAV")
                {
                    if (curMessagePart[2] == "VALBZ")
                    {
                        if (curMessagePart[3] == "QT")
                        {
                            double newNAV;
                            if (double.TryParse(curMessagePart[4], out newNAV))
                            {
                                _StrategyQTNAV = newNAV;
                            }
                        }
                    }
                }

                if (curMessagePart[1] == "SENDSTAGED")
                {
                    curOrderCalcQuant.Stager.SendAllStagedOrders(false);
                }

                if (curMessagePart[1] == "SENDORD")
                {
                    int Portfolio = int.Parse(curMessagePart[2]);
                    string OrdID = curMessagePart[3];
                    bool auction = Convert.ToBoolean(curMessagePart[4]);

                    if (Portfolio == NEnuns.QuantPortfolios.Quant) { curOrderCalcQuant.Stager.SendSingleStagedOrder(OrdID, auction); }

                    curSocket.SendMessage("ORDINF" + (char)16 + "SENTORD" + (char)16 + OrdID);
                }

                if (curMessagePart[1] == "CANCELORD")
                {
                    int Portfolio = int.Parse(curMessagePart[2]);
                    string OrdID = curMessagePart[3];

                    bool ordRemoved = false;

                    if (Portfolio == NEnuns.QuantPortfolios.Quant) { ordRemoved = curOrderCalcQuant.Stager.RemoveSingleStagedOrder(OrdID); }

                    if (ordRemoved)
                    {
                        curSocket.SendMessage("ORDINF" + (char)16 + "ORDCANCEL" + (char)16 + OrdID);
                    }
                }

            }
            #endregion

            #region =========================  CLIENT REQUESTS  =========================

            if (curMessagePart[0] == "REQ")
            {
                if (curMessagePart[1] == "STRAT")
                {
                    curStrategy.StratRecalc();
                    
                    foreach (KeyValuePair<int, newTickerPE> curValueItems in curStrategy.PositionPEs)
                    {
                        newTickerPE curValueItem = curValueItems.Value;

                        string curValueItemString = curValueItem.EncodeTXT();

                        curSocket.SendMessage("INF" + (char)16 + "STRAT" + (char)16 + "ADD" + (char)16 + curValueItemString);
                    }

                    curSocket.SendMessage("INF" + (char)16 + "STRAT" + (char)16 + "ENDADD");
                    curSocket.SendMessage("INF" + (char)16 + "STRAT" + (char)16 + "CURNAVSTRAT" + (char)16 + _StrategyQTNAV.ToString("#,##0.00"));
                    curSocket.SendMessage("INF" + (char)16 + "STRAT" + (char)16 + "CURNAVFUND" + (char)16 + _FUNDQTNAV.ToString("#,##0.00"));
                }

                if (curMessagePart[1] == "STA")
                {
                    curSocket.SendMessage("INF" + (char)16 + "STA" + (char)16 + "MKTDATASYM" + (char)16 + SymConn.Instance.IsSymConnected.ToString());
                    curSocket.SendMessage("INF" + (char)16 + "STA" + (char)16 + "FIXCONN" + (char)16 + IsFixConnected.ToString());
                }

                if (curMessagePart[1] == "ORDSTG")
                {
                    curSocket.SendMessage(OrderStager.MultipleStagersEncodeString(curOrderCalcQuant.Stager));
                }
            }

            #endregion
        }

        #endregion
    }
}
