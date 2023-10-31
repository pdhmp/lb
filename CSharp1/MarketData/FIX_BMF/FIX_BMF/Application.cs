/****************************************************************************
** Copyright (c) quickfixengine.org  All rights reserved.
**
** This file is part of the QuickFIX FIX Engine
**
** This file may be distributed under the terms of the quickfixengine.org
** license as defined by quickfixengine.org and appearing in the file
** LICENSE included in the packaging of this file.
**
** This file is provided AS IS with NO WARRANTY OF ANY KIND, INCLUDING THE
** WARRANTY OF DESIGN, MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.
**
** See http://www.quickfixengine.org/LICENSE for licensing information.
**
** Contact ask@quickfixengine.org if any conditions of this licensing are
** not clear to you.
**
****************************************************************************/

using System;
using System.Threading;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using QuickFix;
using System.IO;

using System.Collections.Generic;

using NestDLL;
using NestSymConn;
using NestGeneric;
using NCommonTypes;

namespace FIX_BMF
{    
    public class Application: MessageCracker, QuickFix.Application
	{
        public List<NCommonTypes.MarketDataItem> SubscribedData = new List<NCommonTypes.MarketDataItem>();
        public List<MarketDepthItem> SubscribedDepth = new List<MarketDepthItem>();
        public SortedDictionary<string, int> SubListIndex = new SortedDictionary<string, int>();
        public SortedDictionary<string, string> BMFCodeMap = new SortedDictionary<string, string>();

        public SortedDictionary<string, string> CodeMapToFile = new SortedDictionary<string, string>();

        public SortedDictionary<string, int> OffshorePosition = new SortedDictionary<string, int>();

        NestSocketServer curSocket = new NestSocketServer();
        
        System.Timers.Timer tmrUpdatePosition = new System.Timers.Timer(10000); 
                        
        int Next_SeqNum_MD = 200;

        bool SecListLoadedFromFile = false;

		public QuickFix.SessionID secaoId = null;                   
               	
		#region Thread -  ##### ENVIO DE MENSAGENS
        		
        public Application()
		{
            curSocket.StartListen(12333);
            curSocket.NewMessage += new EventHandler(ReceivedMessage);
            tmrUpdatePosition.AutoReset = true;
            tmrUpdatePosition.Elapsed += new System.Timers.ElapsedEventHandler(GetOffshoreTickers);
            
            LoadSecListFromFile();
		}

        		
		#endregion
	
        #region Eventos FIX

		public void onCreate( SessionID sessionID )
		{
			//clsArquivo.fnGravaLogTXT("onCreate: " + sessionID);
			GlobalVars.Instance.AddMessage("onCreate: " + sessionID);

            secaoId = sessionID;
		}
	


		public void onLogon( SessionID sessionID ) 
		{
			//clsArquivo.fnGravaLogTXT("onLogon: " + sessionID);
			GlobalVars.Instance.AddMessage("onLogon: " + sessionID);

			secaoId = sessionID;
            //BMFSubscribe("DOLQ10");

            //if (!SecListLoadedFromFile)
            {
                RequestSecurityList(sessionID);
                Thread t1 = new Thread(InitialRequest);
                t1.IsBackground = true;
                t1.Start();
            }
            //else
            //{
            //    ReSubscribeTickers();
            //    RequestALLMarketData();
            //}
		}

        private void InitialRequest()
        {
            DateTime reqTime = DateTime.Now;
            while (DateTime.Now < reqTime.Add(new TimeSpan(0, 0, 15)))
            {
            }

            ReSubscribeTickers();
            RequestALLMarketData();
        }

		public void fromAdmin( Message message, SessionID sessionID ) 
		{
            if (!message.ToString().Contains("35=0") && !message.ToString().Contains("35=A"))
            {
                Console.Write("fromAdmin: " + message.ToString());
                crack(message, sessionID);
            }
		}

		public void fromApp( Message message, SessionID sessionID )
		{
            //DataRecorder.Instance.EnqueueMessage((QuickFix.Message)message);

			crack( message, sessionID );
		}

		public void toApp( Message message, SessionID sessionID )   
        { 
        }

		public void toAdmin( Message message, SessionID sessionID )	
		{
            if (isMessageOfType(message, MsgType.LOGON))
            {
                addLogonField(message, sessionID);
            }
		}

        private void addLogonField(Message message, SessionID sessionID)
        {
            // ### Password : LINK
            // message.getHeader().setField(new RawDataLength(6));
            // message.getHeader().setField(new RawData("HFSUOK"));

            // ### Password : XP
            message.getHeader().setField(new RawDataLength(6));
            message.getHeader().setField(new RawData("RHYVAK"));
        }

        private bool isMessageOfType(Message message, String type)
        {
            try
            {
                return type.Equals(message.getHeader().getField(new MsgType()).getValue());
            }
            catch (FieldNotFound e)
            {
                return false;
            }
        }

		public void onLogout( SessionID sessionID)
		{ 
			//clsArquivo.fnGravaLogTXT("onLogout: " + sessionID.ToString());
			GlobalVars.Instance.AddMessage       ("onLogout: " + sessionID.ToString());
			secaoId = null;
		}
		
		#endregion

		#region ### QuickFix44 - ##### RECEBIMENTO DE MENSAGENS

		// 35= 0 - Heartbeat <0> 
		#region void onMessage( QuickFix44.Heartbeat message, SessionID sessionID )
		public override void onMessage( QuickFix44.Heartbeat message, SessionID sessionID )
		{
			//clsArquivo.fnGravaLogTXT("QuickFix44.Heartbeat: " + message.ToXML());
            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - HeartBeat   (BMF FIX) - ");
        }
		#endregion

 
		// 35= 1 - Test Request <1> 
		#region void onMessage( QuickFix44.TestRequest message, SessionID sessionID )
		public override void onMessage( QuickFix44.TestRequest message, SessionID sessionID )
		{
			//clsArquivo.fnGravaLogTXT("QuickFix44.TestRequest: " + message.ToXML());
            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - TestRequest (BMF FIX)");
        }
		#endregion


		// 35= 2 - Resend Request <2> 
		#region void onMessage( QuickFix44.ResendRequest message, SessionID sessionID )
		public override void onMessage( QuickFix44.ResendRequest message, SessionID sessionID )
		{
			//clsArquivo.fnGravaLogTXT("QuickFix44.ResendRequest: " + message.ToXML());
            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - ResendRequest (BMF FIX)");
        }
		#endregion


		// 35= 3 - Reject <3> 
		#region void onMessage( QuickFix44.Reject message, SessionID sessionID )
		public override void onMessage( QuickFix44.Reject message, SessionID sessionID )
		{
			//clsArquivo.fnGravaLogTXT("QuickFix44.Reject: " + message.ToXML());
			GlobalVars.Instance.AddMessage("QuickFix44.Reject: " + message.ToXML());
		}
		#endregion


		// 35= 4 - Sequence Reset <4> 
		#region void onMessage( QuickFix44.SequenceReset message, SessionID sessionID )
		public override void onMessage( QuickFix44.SequenceReset message, SessionID sessionID )
		{
            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - SequenceReset (BMF FIX)");
        }
		#endregion


		// 35= 5 = Logout <A> 
		#region void onMessage( QuickFix44.Logout message, SessionID sessionID )
		public override void onMessage( QuickFix44.Logout message, SessionID sessionID )
		{
            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - Logout (BMF FIX)");

			// Envia mensagem de queda de conexão
			Session.sendToTarget(message, sessionID);
			
			// anula Sessoes ativas
			secaoId = null;
		}
		#endregion


		// 35= 8 = Execution Report <8> 
		#region void onMessage( QuickFix44.ExecutionReport message, SessionID sessionID )
		public override void onMessage( QuickFix44.ExecutionReport message, SessionID sessionID )
		{
			       GlobalVars.Instance.AddMessage("QuickFix44.ExecutionReport: " + message.ToXML());
			//clsArquivo.fnGravaLogTXT("QuickFix44.ExecutionReport: " + message.ToXML());
			
			// Aciona metodo para inserir Prints
			clsProcessamento obj = new clsProcessamento();
			obj.fnGravaPrints(message,  sessionID );
		}
		#endregion

	
		// 35= A = Logon <A> 
		#region void onMessage( QuickFix44.Logon message, SessionID sessionID )
		public override void onMessage( QuickFix44.Logon message, SessionID sessionID )
		{
            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - Logon (BMF FIX)");            
        }
		#endregion

        // 35= ? = MarketData 

        public void LoadSecListFromFile()
        {
            string curFileName = "C:\\Temp\\BMFSecList.txt";

            if (File.Exists(curFileName))
            {
                FileStream fs = new FileStream(curFileName, FileMode.Open);
                StreamReader sReader = new StreamReader(fs);

                string curLine = "";

                while (sReader.Peek() >= 0)
                {
                    
                        curLine = sReader.ReadLine();
                        if (curLine.Length > 3)
                        {
                        string[] LineArray = curLine.Split('\t');
                        if (LineArray[0] == "FUT")
                        {
                            if (!BMFCodeMap.ContainsKey(LineArray[1]))
                            {
                                BMFCodeMap.Add(LineArray[1], LineArray[2]);
                            }
                        }

                        string CMTFKey = LineArray[0] + (char)16 + LineArray[1];
                        if (!CodeMapToFile.ContainsKey(CMTFKey))
                        {
                            CodeMapToFile.Add(CMTFKey, LineArray[2]);
                        }
                    }
                }

                sReader.Close();
                fs.Close();

                GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - Loaded security List from file: " + curFileName);

                SecListLoadedFromFile = true;
            }
        }

        public override void onMessage(QuickFix44.SecurityList message, SessionID sessionID)
        {
            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - Requesting SecurityList");

            NoRelatedSym secList = new NoRelatedSym();

            message.get(secList);

            FileStream fs = new FileStream("C:\\Temp\\" + DateTime.Now.Date.ToString("yyyyMMdd") + "_BMFSecList.txt", FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            for (uint i = 1; i <= secList.getValue(); i++)
            {
                QuickFix44.SecurityList.NoRelatedSym secGroup = new QuickFix44.SecurityList.NoRelatedSym();
                message.getGroup(i, secGroup);

                SecurityID curSecurityID = new SecurityID();
                SecurityDesc curSecurityDesc = new SecurityDesc();
                ContractSettlMonth curContractSettlMonth = new ContractSettlMonth ();
                Symbol curSymbol = new Symbol();
                CFICode curCFICode = new CFICode();
                SecurityType curSecurityType = new SecurityType();

                secGroup.get(curSecurityID);
                secGroup.get(curSecurityDesc);
                secGroup.get(curContractSettlMonth);
                secGroup.get(curSymbol);
                secGroup.get(curCFICode);
                secGroup.get(curSecurityType);

                string CMTFKey = curSecurityType.ToString() + (char)16 + curSymbol.ToString();
                if (CodeMapToFile.ContainsKey(CMTFKey))
                {
                    CodeMapToFile.Remove(CMTFKey);
                }
                CodeMapToFile.Add(CMTFKey, curSecurityID.ToString());

                //sw.Write(curSecurityType.ToString() + "\t" + curSymbol.ToString() + "\t" + curSecurityID.ToString() + "\r\n");

                if (curSecurityType.ToString().Contains("FUT"))
                {
                    GlobalVars.Instance.AddMessage(curSymbol.ToString() + " - " + curSecurityDesc.ToString() + " - " + curContractSettlMonth + " - " + curSecurityID.ToString() + " - " + curCFICode.ToString());

                    if (!BMFCodeMap.ContainsKey(curSymbol.ToString()))
                    {
                        BMFCodeMap.Add(curSymbol.ToString(), curSecurityID.ToString());
                    }
                    else
                    {
                        BMFCodeMap[curSymbol.ToString()] = curSecurityID.ToString();
                    }                    
                }
            }

            foreach (KeyValuePair<string, string> kvp in CodeMapToFile)
            {
                string curSecurityType = kvp.Key.Split((char)16)[0];
                string curSymbol = kvp.Key.Split((char)16)[1];
                string curSecurityID = kvp.Value;

                sw.Write(curSecurityType.ToString() + "\t" + curSymbol.ToString() + "\t" + curSecurityID.ToString() + "\r\n");
            }

            sw.Close();
            fs.Close();

            secaoId = sessionID;
        }

        public override void onMessage(QuickFix44.MarketDataSnapshotFullRefresh message, SessionID sessionID)
        {
            NoMDEntries noUpdates = new NoMDEntries();
            message.get(noUpdates);

            List<int> ChangedItems_MKT = new List<int>();
            List<int> ChangedItems_DEPTH = new List<int>();

            string prevTicker;

            for (uint i = 1; i <= noUpdates.getValue(); i++)
            {
                QuickFix44.MarketDataIncrementalRefresh.NoMDEntries curNoMDEntry = new QuickFix44.MarketDataIncrementalRefresh.NoMDEntries();
                message.getGroup(i, curNoMDEntry);

                double curQuantity = 0;
                int curPosition = 0;
                double curValue = 0;
                int curPos = -1;
                string curQuoteCondition = "*";

                string curTicker = message.getSymbol().ToString();

                if (curTicker == "DI1F13")
                {
                    int a = 0;
                }

                string curBMFCode = "";

                if (BMFCodeMap.TryGetValue(curTicker, out curBMFCode))
                {
                    if (!SubListIndex.TryGetValue(curTicker, out curPos))
                    {
                        NCommonTypes.MarketDataItem curItem = new NCommonTypes.MarketDataItem();
                        curItem.Ticker = curTicker;
                        curItem.ExchangeSymbol = BMFCodeMap[curTicker];

                        MarketDepthItem curDepthItem = new MarketDepthItem();
                        curDepthItem.Ticker = curTicker.ToString();

                        SubscribedData.Add(curItem);
                        SubscribedDepth.Add(curDepthItem);

                        UpdateListIndex();
                    }
                }

                if (SubListIndex.TryGetValue(curTicker, out curPos))
                {
                    string curType = curNoMDEntry.getMDEntryType().getValue().ToString();
                    if (curNoMDEntry.isSetMDEntryPx()) curValue = curNoMDEntry.getMDEntryPx().getValue();
                    if (curNoMDEntry.isSetMDEntrySize()) curQuantity = curNoMDEntry.getMDEntrySize().getValue();
                    if (curNoMDEntry.isSetMDEntryPositionNo()) curPosition = curNoMDEntry.getMDEntryPositionNo().getValue();
                    if (curNoMDEntry.isSetQuoteCondition()) curQuoteCondition = curNoMDEntry.getQuoteCondition().getValue();

                    int curAction = CrackMarketMessage(curTicker, curType, curValue, curQuantity, curPosition, curNoMDEntry, 0, curQuoteCondition);

                    if (curAction == 1 || curAction == 11) ChangedItems_MKT.Add(curPos);
                    if (curAction == 10 || curAction == 11) ChangedItems_DEPTH.Add(curPos);
                }

                prevTicker = curTicker;

            }

            foreach (int chgPos in ChangedItems_MKT)
            {
                SendMarketItem(SubscribedData[chgPos]);
            }

            foreach (int chgPos in ChangedItems_MKT)
            {
                SendDepthItem(SubscribedDepth[chgPos]);
            }

            secaoId = sessionID;
        }

        public override void onMessage(QuickFix44.MarketDataIncrementalRefresh message, SessionID sessionID)
        {
            NoMDEntries noUpdates = new NoMDEntries();
            message.get(noUpdates);

            List<int> ChangedItems_MKT = new List<int>();
            List<int> ChangedItems_DEPTH = new List<int>();

            string prevTicker;

            for (uint i = 1; i <= noUpdates.getValue(); i++)
			{
                QuickFix44.MarketDataIncrementalRefresh.NoMDEntries curNoMDEntry = new QuickFix44.MarketDataIncrementalRefresh.NoMDEntries();
			    message.getGroup(i, curNoMDEntry);

                double curQuantity = 0;
                int curPosition = 0;
                double curValue = 0;
                int curPos = -1;
                string curOpenCloseSettlFlag = "*";

                string curTicker = curNoMDEntry.getSymbol().ToString();

                if (curTicker == "DOLH11")
                {
                    int a = 0;
                }

                string curBMFCode = "";

                if (BMFCodeMap.TryGetValue(curTicker, out curBMFCode))
                {
                    if (!SubListIndex.TryGetValue(curTicker, out curPos))
                    {
                        NCommonTypes.MarketDataItem curItem = new NCommonTypes.MarketDataItem();
                        curItem.Ticker = curTicker;
                        curItem.ExchangeSymbol = BMFCodeMap[curTicker];

                        MarketDepthItem curDepthItem = new MarketDepthItem();
                        curDepthItem.Ticker = curTicker.ToString();

                        SubscribedData.Add(curItem);
                        SubscribedDepth.Add(curDepthItem);

                        UpdateListIndex();
                    }
                }

                if (SubListIndex.TryGetValue(curTicker, out curPos))
                {
                    string curType = curNoMDEntry.getMDEntryType().getValue().ToString();
                    if (curNoMDEntry.isSetMDEntryPx()) curValue = curNoMDEntry.getMDEntryPx().getValue();
                    if (curNoMDEntry.isSetMDEntrySize()) curQuantity = curNoMDEntry.getMDEntrySize().getValue();
                    if (curNoMDEntry.isSetMDEntryPositionNo()) curPosition = curNoMDEntry.getMDEntryPositionNo().getValue();
                    if (curNoMDEntry.isSetOpenCloseSettlFlag()) curOpenCloseSettlFlag = curNoMDEntry.getOpenCloseSettlFlag().getValue();

                    int curAction = CrackMarketMessage(curTicker, curType, curValue, curQuantity, curPosition, curNoMDEntry, 1,curOpenCloseSettlFlag);

                    if (curAction == 1 || curAction == 11) ChangedItems_MKT.Add(curPos);
                    if (curAction == 10 || curAction == 11) ChangedItems_DEPTH.Add(curPos);
                }

                prevTicker = curTicker;

            }
            foreach (int chgPos in ChangedItems_MKT)
            {
                SendMarketItem(SubscribedData[chgPos]);
            }

            foreach (int chgPos in ChangedItems_DEPTH)
            {
                SendDepthItem(SubscribedDepth[chgPos]);
            }

            foreach (int chgPos in ChangedItems_MKT)
            {
                //MarketDataAnalyzer.Instance.CheckLocalData(SubscribedData[chgPos].Ticker);
            }

            foreach (int chgPos in ChangedItems_DEPTH)
            {
               // MarketDataAnalyzer.Instance.CheckLocalData(SubscribedDepth[chgPos].Ticker);
            }


            secaoId = sessionID;
        }

        public int CrackMarketMessage(string curTicker, string curType, double curValue, double curQuantity, int curPosition, QuickFix44.MarketDataIncrementalRefresh.NoMDEntries curNoMDEntry, int Source, string OpenCloseSettlFlag)
        {
            bool tempReturnDepth = false;
            bool tempReturnMarket = false;
            int curPos = -1;

            if (curTicker == "DI1F17")
            {
                int a = 0;
            }

            if (SubListIndex.TryGetValue(curTicker, out curPos))
            {

                NCommonTypes.MarketDataItem curItem = SubscribedData[curPos];
                MarketDepthItem curDepthItem = SubscribedDepth[curPos];
                int curAction = -1;

                switch (curType)
                {
                    case "2": curItem.Last = curValue; curItem.LastSize = curQuantity; tempReturnMarket = true; break;
                    case "4":
                        if (OpenCloseSettlFlag == "5")
                        {
                            if (curTicker == "DI1F13" || curTicker == "DI1F12")
                            {
                                int a = 0;
                            }
                            curItem.AucLast = curValue;
                            if (curNoMDEntry.isSetMDEntrySize()) { curItem.AucVolume = curNoMDEntry.getMDEntrySize().getValue(); }
                        }
                        else
                        {
                            curItem.Open = curValue;
                        } 
                        tempReturnMarket = true; 
                        break;
                    case "5": curItem.Close = curValue; tempReturnMarket = true; break;
                    case "7": 
                        curItem.High = curValue; 
                        tempReturnMarket = true;

                        if (curTicker == "DOLH11")
                        {
                            //System.Windows.Forms.MessageBox.Show("INDV10 - High: " + curValue.ToString());
                        }

                        break;
                    case "8": curItem.Low = curValue; tempReturnMarket = true; break;
                    case "9": curItem.VWAP = curValue; tempReturnMarket = true; break;
                    case "B": curItem.Volume = curValue; tempReturnMarket = true; break;
                    case "X": curItem.Ask = curValue; curItem.AskSize = curQuantity; tempReturnMarket = true; break;
                    case "Z": curItem.Bid = curValue; curItem.BidSize = curQuantity; tempReturnMarket = true; break;
                    case "b": curItem.TradingStatus = ConvertTradingStatus(curNoMDEntry.getTradingSessionSubID().ToString()); tempReturnMarket = true; break;
                    case "e":
                        if (curNoMDEntry.isSetMDUpdateAction()) curAction = int.Parse(curNoMDEntry.getMDUpdateAction().ToString());
                        if (curAction == -1 && Source == 0) curAction = 0;
                        if (curAction > -1)
                        {
                            int curPositionNo = 0;
                            if (curNoMDEntry.isSetMDEntryPositionNo()) curPositionNo = int.Parse(curNoMDEntry.getMDEntryPositionNo().ToString());
                            if (curAction == 2)
                            {
                                curDepthItem.Bid_Delete(curPositionNo);
                                if (GlobalVars.Instance.SaveLog && curTicker == GlobalVars.Instance.SaveLogTicker) SaveFileLog(curAction + "\t" + curPositionNo, 1);
                            }
                            else
                            {
                                double curPrice = 0;
                                int curSize = 0;
                                int curOrderNo = 0;

                                if (curNoMDEntry.isSetMDEntryPx()) curPrice = double.Parse(curNoMDEntry.getMDEntryPx().ToString());
                                if (curNoMDEntry.isSetMDEntrySize()) curSize = int.Parse(curNoMDEntry.getMDEntrySize().ToString());


                                if (curAction == 0)
                                {
                                    curDepthItem.Bid_New(curPositionNo, curPrice, curSize);
                                }
                                if (curAction == 1)
                                {
                                    curDepthItem.Bid_Update(curPositionNo, curPrice, curSize);
                                }

                                if (GlobalVars.Instance.SaveLog && curTicker == GlobalVars.Instance.SaveLogTicker) SaveFileLog(curAction + "\t" + curPositionNo + "\t" + curPrice + "\t" + curSize, 1);
                            }
                            tempReturnDepth = true;
                        }
                        break;
                    case "f":
                        if (curNoMDEntry.isSetMDUpdateAction()) curAction = int.Parse(curNoMDEntry.getMDUpdateAction().ToString());
                        if (curAction == -1) curAction = 0;
                        if (curAction > -1)
                        {
                            
                            int curPositionNo = 0;
                            if (curNoMDEntry.isSetMDEntryPositionNo()) curPositionNo = int.Parse(curNoMDEntry.getMDEntryPositionNo().ToString());
                            if (curAction == 2)
                            {
                                curDepthItem.Ask_Delete(curPositionNo);
                                if (GlobalVars.Instance.SaveLog && curTicker == GlobalVars.Instance.SaveLogTicker) SaveFileLog(curAction + "\t" + curPositionNo, 2);
                            }
                            else
                            {
                                double curPrice = 0;
                                int curSize = 0;

                                if (curNoMDEntry.isSetMDEntryPx()) curPrice = double.Parse(curNoMDEntry.getMDEntryPx().ToString());
                                if (curNoMDEntry.isSetMDEntrySize()) curSize = int.Parse(curNoMDEntry.getMDEntrySize().ToString());

                                if (curAction == 0)
                                {
                                    curDepthItem.Ask_New(curPositionNo, curPrice, curSize);
                                }
                                if (curAction == 1)
                                {
                                    curDepthItem.Ask_Update(curPositionNo, curPrice, curSize);
                                }

                                if (GlobalVars.Instance.SaveLog && curTicker == GlobalVars.Instance.SaveLogTicker) SaveFileLog(curAction + "\t" + curPositionNo + "\t" + curPrice + "\t" + curSize, 2);
                            }


                            tempReturnDepth = true;
                        }
                        break;
                    case "a": break;
                    case "c":
                        string tempString = "";
                        if (curNoMDEntry.isSetField(326))
                        {                            
                            tempString = FormatTradingStatus(curNoMDEntry.getField(326).ToString());
                            curItem.TradingStatus = ConvertTradingStatus(tempString);
                            tempReturnMarket = true;
                        }
                        break;
                    default:
                        SaveFileLog(curType + "\t", 0);
                        break;
                }

                if (tempReturnDepth && tempReturnMarket)
                    return 11;
                else if (tempReturnDepth && !tempReturnMarket)
                    return 10;
                else if (!tempReturnDepth && tempReturnMarket)
                    return 1;
            }
            return 0;
        }

        public TradingStatusType ConvertTradingStatus(string status)
        {
            TradingStatusType oReturn = TradingStatusType.UNKNOWN;

            switch (status)
            {
                case "100": break; //não-iniciado
                case "101": oReturn = TradingStatusType.G_TRADING_S; break; //negociação
                case "102": oReturn = TradingStatusType.AUCTION_K; break; //leilão
                case "103": break; //proibido
                case "104": break; //leilão encerrado
                case "105": oReturn = TradingStatusType.SUSP_U; break; //suspenso
                case "106": oReturn = TradingStatusType.G_PREOPEN_P; break; //pré-leilão
                case "107": break; //pós-leilão
                case "108": break; //revogado
                case "109": break; //cancelado
                case "110": break; //fechamento eletrônico de ofertas
                case "111": break; //sem ofertante
                case "112": break; //oferta de compra mínima não alcançada
                case "113": break; //adjudicado
                case "114": break; //aceito
                case "115": break; //impugnad
                case "116": break; //proibido (reservado)
                case "117": oReturn = TradingStatusType.AUCTION_K; break; //sujeito à interferência
                case "118": oReturn = TradingStatusType.FROZEN_G; break; //congelado
                case "119": oReturn = TradingStatusType.AUCTION_K; break; //leilão estendido
                case "120": break; //sem validade
                case "121": oReturn = TradingStatusType.AUCTION_K; break; //com final aleatório
                case "122": oReturn = TradingStatusType.G_MKTCONTROL_N; break; //interrompido
                case "123": break; //suspenso e proibido
                case "124": break; //negociação proibida
                case "125": break; //congelado e proibido

                case "B": oReturn = TradingStatusType.G_NIGHT_B; break;
                case "C": oReturn = TradingStatusType.G_PREP_C; break;
                case "E": oReturn = TradingStatusType.G_INTERV_E; break;
                case "F": oReturn = TradingStatusType.G_EODAY_F; break;
                case "O": oReturn = TradingStatusType.G_OPENING_O; break;
                case "P": oReturn = TradingStatusType.G_PREOPEN_P; break;
                case "R": oReturn = TradingStatusType.G_AFTERMKT_R; break;
                case "S": oReturn = TradingStatusType.G_TRADING_S; break;
                case "I": oReturn = TradingStatusType.G_IMPEDED_I; break;
                case "N": oReturn = TradingStatusType.G_MKTCONTROL_N; break;
                case "Z": oReturn = TradingStatusType.G_INTERRUPTED_Z; break;
                case "H": oReturn = TradingStatusType.INHIBITED_H; break;
                case "U": oReturn = TradingStatusType.SUSP_U; break;
                case "G": oReturn = TradingStatusType.FROZEN_G; break;
                case "K": oReturn = TradingStatusType.AUCTION_K; break;
                case "": oReturn = TradingStatusType.UNKNOWN; break;

                default: break;
            }


            //switch (BMFTradingStatus)
            //{
            //    case "100": status = BMFTradingStatus; break; //não-iniciado
            //    case "101": status = "S"; break; //negociação
            //    case "102": status = "K"; break; //leilão
            //    case "103": status = BMFTradingStatus; break; //proibido
            //    case "104": status = BMFTradingStatus; break; //leilão encerrado
            //    case "105": status = "U"; break; //suspenso
            //    case "106": status = "P"; break; //pré-leilão
            //    case "107": status = BMFTradingStatus; break; //pós-leilão
            //    case "108": status = BMFTradingStatus; break; //revogado
            //    case "109": status = BMFTradingStatus; break; //cancelado
            //    case "110": status = BMFTradingStatus; break; //fechamento eletrônico de ofertas
            //    case "111": status = BMFTradingStatus; break; //sem ofertante
            //    case "112": status = BMFTradingStatus; break; //oferta de compra mínima não alcançada
            //    case "113": status = BMFTradingStatus; break; //adjudicado
            //    case "114": status = BMFTradingStatus; break; //aceito
            //    case "115": status = BMFTradingStatus; break; //impugnado
            //    case "116": status = BMFTradingStatus; break; //proibido (reservado)
            //    case "117": status = "K"; break; //sujeito à interferência
            //    case "118": status = "G"; break; //congelado
            //    case "119": status = "K"; break; //leilão estendido
            //    case "120": status = BMFTradingStatus; break; //sem validade
            //    case "121": status = "K"; break; //com final aleatório
            //    case "122": status = "N"; break; //interrompido
            //    case "123": status = BMFTradingStatus; break; //suspenso e proibido
            //    case "124": status = BMFTradingStatus; break; //negociação proibida
            //    case "125": status = BMFTradingStatus; break; //congelado e proibido
            //    default: status = BMFTradingStatus; break;
            //}

            return oReturn;
        }

        private string FormatTradingStatus(string BMFTradingStatus)
        {
            string status = "";
            switch (BMFTradingStatus)
            {
                case "100": status = BMFTradingStatus; break; //não-iniciado
                case "101": status = "S"; break; //negociação
                case "102": status = "K"; break; //leilão
                case "103": status = BMFTradingStatus; break; //proibido
                case "104": status = BMFTradingStatus; break; //leilão encerrado
                case "105": status = "U"; break; //suspenso
                case "106": status = "P"; break; //pré-leilão
                case "107": status = BMFTradingStatus; break; //pós-leilão
                case "108": status = BMFTradingStatus; break; //revogado
                case "109": status = BMFTradingStatus; break; //cancelado
                case "110": status = BMFTradingStatus; break; //fechamento eletrônico de ofertas
                case "111": status = BMFTradingStatus; break; //sem ofertante
                case "112": status = BMFTradingStatus; break; //oferta de compra mínima não alcançada
                case "113": status = BMFTradingStatus; break; //adjudicado
                case "114": status = BMFTradingStatus; break; //aceito
                case "115": status = BMFTradingStatus; break; //impugnado
                case "116": status = BMFTradingStatus; break; //proibido (reservado)
                case "117": status = "K"; break; //sujeito à interferência
                case "118": status = "G"; break; //congelado
                case "119": status = "K"; break; //leilão estendido
                case "120": status = BMFTradingStatus; break; //sem validade
                case "121": status = "K"; break; //com final aleatório
                case "122": status = "N"; break; //interrompido
                case "123": status = BMFTradingStatus; break; //suspenso e proibido
                case "124": status = BMFTradingStatus; break; //negociação proibida
                case "125": status = BMFTradingStatus; break; //congelado e proibido
                default: status = BMFTradingStatus; break;
            }

            return status;
        }

        public void SaveFileLog(string LineToSave, int FileType)
        {
            string curFileName = "";
            if (FileType == 0) curFileName = "T:\\Log\\not_implemented.txt";
            if (FileType == 1) curFileName = "T:\\Log\\depthlog_BID.txt";
            if (FileType == 2) curFileName = "T:\\Log\\depthlog_ASK.txt";

            bool HasWritten = false;

            while (!HasWritten)
            {
                try
                {
                    FileStream fs = new FileStream(curFileName, FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(LineToSave);
                    sw.Close();
                    fs.Close();
                    HasWritten = true;
                }
                catch { }
            }
        }

        public override void onMessage(QuickFix44.MarketDataRequestReject message, SessionID sessionID)
        {
            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - Market Data REJECT " + message.ToString());


            secaoId = sessionID;
        }

        public override void onMessage(QuickFix44.News message, SessionID sessionID)
        {
            if (message.isSetField(48))
                GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - NEWS: " + message.getField(48));
            if (message.isSetField(58))
                GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - NEWS: " + message.getField(58));

            secaoId = sessionID;
        }

		#endregion
			
		#region ### FORMATACAO DE MENSAGENS

		// ***** NewOrdemSigle
		#region void fnFormataNewOrdemSigle(QuickFix.Message message_ordem, SessionID sessionID )
		private void fnFormataNewOrdemSigle(QuickFix.Message message_ordem, SessionID sessionID )
		{
			//clsArquivo.fnGravaLogTXT("fnFormataNewOrdemSigle_35D: " + message_ordem.ToXML());
            GlobalVars.Instance.AddMessage("fnFormataNewOrdemSigle_35D: " + message_ordem.ToXML());

			QuickFix44.NewOrderSingle message = new QuickFix44.NewOrderSingle();
			
			message.setField(1, message_ordem.getField(1));			// Account
			message.setField(11, message_ordem.getField(11));		// CLORDID
			message.setField(15, message_ordem.getField(15));		// Currency
			message.setField(21, message_ordem.getField(21));		// HandlIns 
			message.setField(22, message_ordem.getField(22));		// IDSource
			message.setField(38, message_ordem.getField(38));		// OrderQty
			message.setField(40, message_ordem.getField(40));		// OrdType 
			message.setField(44, message_ordem.getField(44));		// Price
			message.setField(54, message_ordem.getField(54));		// SIDE 
			message.setField(55, message_ordem.getField(55));		// SYMBOL
			message.setField(59, message_ordem.getField(59));		// TimeInForce
			message.setField(207, message_ordem.getField(207));		// SecurityExchange
			message.setField(60, message_ordem.getField(60));		// TransactTime
			
			// Envia menssagem
			Session.sendToTarget(message, sessionID);                                                      
		}

		#endregion

		
		// ***** CancelRequest
		#region void fnFormataCancelRequest(QuickFix.Message message_ordem, SessionID sessionID )
		private void fnFormataCancelRequest(QuickFix.Message message_ordem, SessionID sessionID )
		{
            GlobalVars.Instance.AddMessage("fnFormataCancelRequest: " + message_ordem.ToXML());
			//clsArquivo.fnGravaLogTXT("fnFormataCancelRequest: " + message_ordem.ToXML());

			QuickFix44.OrderCancelRequest message = new QuickFix44.OrderCancelRequest();
			
			message.setField(11, message_ordem.getField(11));		// CLORDID
			message.setField(41, message_ordem.getField(41));		// OrigClOrdID 
			message.setField(54, message_ordem.getField(54));		// Side 
			message.setField(55, message_ordem.getField(55));		// Symbol
			message.setField(60, message_ordem.getField(60));		// TransactTime
			
			// Envia menssagem
			Session.sendToTarget(message, sessionID);                                                      
		}
		#endregion


		// ***** CancelReject - 35 = 9
		#region void ExecutionReport_35_9(QuickFix.Message message, SessionID sessionID )
		private void ExecutionReport_35_9(QuickFix.Message message, SessionID sessionID )
		{
            GlobalVars.Instance.AddMessage("ExecutionReport_35_9: " + message.ToXML());
			//clsArquivo.fnGravaLogTXT("ExecutionReport_35_9: " + message.ToXML());
			
			QuickFix44.ExecutionReport msgFix = new QuickFix44.ExecutionReport();
			
			msgFix.setField(11, message.getField(11));	// CLORDID
			msgFix.setField(37, message.getField(37));	// OrderID
			msgFix.setField(39, message.getField(39));	// OrdStatus
			msgFix.setField(41, message.getField(41));	// OrigCLORDID
			
			// Envia menssagem
			Session.sendToTarget(msgFix, secaoId);                                                      
		}

		#endregion

		#endregion


        private void RequestSecurityList(SessionID sessionID)
        {
            SubscribedData.Clear();

            QuickFix44.SecurityListRequest message = new QuickFix44.SecurityListRequest(new SecurityReqID("SLT0008"), new SecurityListRequestType(1));

            message.setField(167, "FUT");
            message.setField(263, "0");

            // Envia menssagem
            Session.sendToTarget(message, sessionID);

            FIX_BMF.Properties.Settings.Default.SecListDate = DateTime.Today;
            FIX_BMF.Properties.Settings.Default.Save();

        }

        private void RequestMarketData(SessionID sessionID, string strSymbol, string strSecurityID, int iSecurityIDSource)
        {
            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - Requesting Market Data For:" + strSymbol);

            string strSeqNum = "MD" + Next_SeqNum_MD.ToString("000000");
            Next_SeqNum_MD++;

            QuickFix44.MarketDataRequest message = new QuickFix44.MarketDataRequest(new MDReqID(strSeqNum), new SubscriptionRequestType('1'), new MarketDepth(0));
            
            MDEntryType curMDEntryType = new MDEntryType();
            QuickFix44.MarketDataRequest.NoMDEntryTypes grpNoMDEntryTypes = new QuickFix44.MarketDataRequest.NoMDEntryTypes();

            curMDEntryType.setValue('e'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('f'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);

            curMDEntryType.setValue('2'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('4'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('5'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('7'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('8'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('9'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('B'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('X'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('Z'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);

            curMDEntryType.setValue('a'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('b'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('c'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            
            QuickFix44.MarketDataRequest.NoRelatedSym grpNoRelatedSym = new QuickFix44.MarketDataRequest.NoRelatedSym();

            grpNoRelatedSym.set(new Symbol(strSymbol));
            grpNoRelatedSym.set(new SecurityID(strSecurityID));
            grpNoRelatedSym.set(new SecurityIDSource(iSecurityIDSource.ToString()));

            message.addGroup(grpNoRelatedSym);

            // Envia menssagem
            Session.sendToTarget(message, secaoId);
        
        }

        private void RequestALLMarketData()
        {
            string strSeqNum = "MD" + Next_SeqNum_MD.ToString("000000");
            Next_SeqNum_MD++;

            QuickFix44.MarketDataRequest message = new QuickFix44.MarketDataRequest(new MDReqID(strSeqNum), new SubscriptionRequestType('1'), new MarketDepth(0));

            MDEntryType curMDEntryType = new MDEntryType();
            QuickFix44.MarketDataRequest.NoMDEntryTypes grpNoMDEntryTypes = new QuickFix44.MarketDataRequest.NoMDEntryTypes();

            curMDEntryType.setValue('e'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('f'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);

            curMDEntryType.setValue('2'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('4'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('5'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('7'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('8'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('9'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('B'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('X'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('Z'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);

            curMDEntryType.setValue('a'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('b'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);
            curMDEntryType.setValue('c'); grpNoMDEntryTypes.set(curMDEntryType); message.addGroup(grpNoMDEntryTypes);

            QuickFix44.MarketDataRequest.NoRelatedSym grpNoRelatedSym = new QuickFix44.MarketDataRequest.NoRelatedSym();

            grpNoRelatedSym.set(new Symbol("[N/A]"));
            grpNoRelatedSym.set(new SecurityType("FUT"));

            message.addGroup(grpNoRelatedSym);

            // Envia menssagem
            Session.sendToTarget(message, secaoId);

        }

        private void BMFSubscribe(string curSymbol)
        {
            int curPos = 0;
            string curSecurityID="";

            if (BMFCodeMap.TryGetValue(curSymbol, out curSecurityID))
            {           
                //if (!SubListIndex.TryGetValue(curSymbol, out curPos))
                //{
                    if (!SubscribedTickers.Contains(curSymbol))
                    {
                        SubscribedTickers.Add(curSymbol);
                    }

                    int curId = -1;
                    if (SubListIndex.TryGetValue(curSymbol,out curId))
                    {
                        SendMarketItem(SubscribedData[curId]);
                        SendDepthItem(SubscribedDepth[curId]);
                    }

                    //NCommonTypes.MarketDataItem curItem = new NCommonTypes.MarketDataItem();
                    //curItem.Ticker = curSymbol.ToString();
                    //curItem.ExchangeSymbol = curSecurityID;

                    //MarketDepthItem curDepthItem = new MarketDepthItem();
                    //curDepthItem.Ticker = curSymbol.ToString();

                    //RequestMarketData(secaoId, curSymbol, curSecurityID, 8);

                    //SubscribedData.Add(curItem);
                    //SubscribedDepth.Add(curDepthItem);

                    //MarketDataAnalyzer.Instance.SubscribeTicker(curSymbol);

                    //UpdateListIndex();
                //}
            }
        }

        private void UpdateListIndex()
        {
            SubListIndex.Clear();

            for (int i = 0; i < SubscribedData.Count; i++)
            {
                SubListIndex.Add(SubscribedData[i].Ticker, i);
            }
        }

        private List<string> SubscribedTickers = new List<string>();

        private void ReSubscribeTickers()
        {
            //SubListIndex.Clear();
            //SubscribedData.Clear();
            //SubscribedDepth.Clear();

            foreach (string ticker in SubscribedTickers)
            {
                BMFSubscribe(ticker);
            }
        }

// ==================================================================================================================================================
// ===============================  MESSAGING  ======================================================================================================
// ==================================================================================================================================================

        public void ReceivedMessage(object sender, EventArgs origE)
        {
            string curMessage = ((NestSocketServer.MsgEventArgs)origE).strMessage;

            string[] curMessagePart = curMessage.Split((char)16);

            if (curMessagePart[0] == "CMD")
            {
                if (curMessagePart[1] == "SUBSC")
                {
                    //curSocket.SendMessage("New Subscription Received:" + curMessagePart[2]);

                    string curSecurityID = "";

                    if (BMFCodeMap.TryGetValue(curMessagePart[2], out curSecurityID))
                    {
                        //If it's an Offshore ticker, subscribe as a Offshore and start updating the available 
                        //quantity of this ticker from LiveBook, else subscribe as a BMF ticker.
                        //if (SubscribeOffshore(curMessagePart[2]))
                        //{
                        //    tmrUpdatePosition.Start();                               
                        //}
                        //else
                        //{
                            BMFSubscribe(curMessagePart[2]);
                        //}
                        SendQuote(curMessagePart[2], "Exchange", "XBMF");
                        SendQuote(curMessagePart[2], "Description", curMessagePart[2]);
                    }
                }
            }
        }

        /// <summary>
        /// Subscribe offshore tickers to receive position information from Livebook
        /// </summary>
        /// <param name="szTopic">Ticker for which the position will be sent</param>

        public void GetOffshoreTickers(object sender, EventArgs origE)
        {
            foreach (KeyValuePair<string, int> TickerPair in OffshorePosition)
            {
                UpdatePosition(TickerPair.Key);
            }
        }
        
        private bool SubscribeOffshore(string szTopic)
        {
            //Check if the ticker has extension
            if (szTopic.IndexOf('.') > 0)
            {                
                string[] szTopicPart = szTopic.Split('.');

                //If the extension is an offshore exchange, subscribe to receive position
                if (szTopicPart[1] == "O" || szTopicPart[1] == "P" || szTopicPart[1] == "N")
                {
                    if (!OffshorePosition.ContainsKey(szTopicPart[0]))
                    {
                        OffshorePosition.Add(szTopicPart[0], 0);
                    }
                    return true;
                }
            }
            return false;
        }        

        private void UpdatePosition(string szTopic)
        {
            using (NestDLL.newNestConn Conn = new newNestConn())
            {        
                
                string SQLQuery = "SELECT SUM(POSITION) FROM"
                                    + "("
                                    + "	    (SELECT SUM([Position]) POSITION FROM NESTRT.dbo.Tb000_Posicao_Atual (NOLOCK)"
                                    + "			     WHERE (([Id Portfolio] = 43) OR ([Id Portfolio] = 4))"
                                    + "	     	    		 AND ([Ticker] = '" + szTopic + "'))"
                                    + "		    UNION ALL"

                                    + "	    (SELECT SUM(PORT_TRANS.POSITION) POSITION"
                                    + "		    FROM 	"
                                    + "			    (SELECT ID_TICKER1, SUM(QUANTITY1) POSITION"
                                    + "				    FROM				"
                                    + "					    (SELECT	ID_ACCOUNT1,"
                                    + "						    	ID_TICKER1,"
                                    + "							    QUANTITY1 "
                                    + "							    FROM NESTDB.DBO.VW_TRANSACTIONS_ALL TA (NOLOCK), "
                                    + "								     NESTDB.DBO.TB001_SECURITIES SEC (NOLOCK)"
                                    + "					    WHERE (TA.TRADE_DATE = CONVERT(VARCHAR,GETDATE(),102))"
                                    + "						      AND "
                                    + "						      (SUBSTRING(SEC.[ReutersTicker],0,CHARINDEX('.',SEC.[ReutersTicker])) = '" + szTopic + "')"
                                    + "					    ) AS TR"

                                    + "				    INNER JOIN"

                                    + "					    (SELECT * FROM NESTDB.DBO.TB003_PORTACCOUNTS (NOLOCK)"
                                    + "					    WHERE ID_PORTFOLIO = 48"
                                    + "					    ) AS PORT"

                                    + "				    ON TR.ID_ACCOUNT1 = PORT.ID_ACCOUNT"

                                    + "		        GROUP BY ID_TICKER1"
                                    + "	        ) AS PORT_TRANS"
                                    + "	    )"
                                    + ")AS POL";

                int Position = Conn.Return_Int(SQLQuery);                 
                SendQuote(szTopic, "Position", Position.ToString("0.00").Replace(",", "."));
                curSocket.SendMessage("BMFCONN" + (char)16 + "CMD" + (char)16 + "FLUSH" + (char)16 + "1" + (char)16 + "1");
            }
        }
        
        public void SendQuote(string szTopic, string QuoteItem, string QuoteValue)
        {
            curSocket.SendMessage("BMFCONN" + (char)16 + "MKT" + (char)16 + szTopic + (char)16 + QuoteItem + (char)16 + QuoteValue);
        }

        public void SendDepthQuote(string szTopic, string QuoteItem, string QuoteValue)
        {
            curSocket.SendMessage("BMFCONN" + (char)16 + "DEPTH" + (char)16 + szTopic + (char)16 + QuoteItem + (char)16 + QuoteValue);
        }

        private static volatile object lockroot = new object();

        public static string blockTicker = "NONE";

        public void SendMarketItem(NCommonTypes.MarketDataItem curItem)
        {
            lock (lockroot)
            {
                if (SubscribedTickers.Contains(curItem.Ticker))
                {

                    if (curItem.Ticker == Application.blockTicker)
                    {
                        int a = 0;
                    }

                    SendQuote(curItem.Ticker, "Last", curItem.Last.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Open", curItem.Open.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "High", curItem.High.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Low", curItem.Low.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Close", curItem.Close.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Bid", curItem.Bid.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Ask", curItem.Ask.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BidSize", curItem.BidSize.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "AskSize", curItem.AskSize.ToString("0.00").Replace(",", "."));
                    if (curItem.TradingStatus != null) SendQuote(curItem.Ticker, "AucCond", curItem.TradingStatus.ToString());
                    SendQuote(curItem.Ticker, "AucLast", curItem.AucLast.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "AucVolume", curItem.AucVolume.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Volume", curItem.Volume.ToString("0").Replace(",", "."));
                    SendQuote(curItem.Ticker, "VWAP", curItem.Volume.ToString("0").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BMFSymbol", curItem.ExchangeSymbol);

                    curSocket.SendMessage("BMFCONN" + (char)16 + "CMD" + (char)16 + "FLUSH" + (char)16 + "1" + (char)16 + "1");


                    //MarketDataAnalyzer.Instance.ReceiveLocalMarketData(curItem);

                }
            }

        }

        public void SendDepthItem(MarketDepthItem curItem)
        {
            if (curItem.Bid1 == curItem.Bid2)
            {
                int a = 1;
            }

            lock (lockroot)
            {
                if (SubscribedTickers.Contains(curItem.Ticker))
                {

                    SendQuote(curItem.Ticker, "Bid1", curItem.Bid1.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Bid2", curItem.Bid2.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Bid3", curItem.Bid3.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Bid4", curItem.Bid4.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Bid5", curItem.Bid5.ToString("0.00").Replace(",", "."));

                    SendQuote(curItem.Ticker, "BidSize1", curItem.BidSize1.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BidSize2", curItem.BidSize2.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BidSize3", curItem.BidSize3.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BidSize4", curItem.BidSize4.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BidSize5", curItem.BidSize5.ToString("0.00").Replace(",", "."));

                    SendQuote(curItem.Ticker, "Ask1", curItem.Ask1.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Ask2", curItem.Ask2.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Ask3", curItem.Ask3.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Ask4", curItem.Ask4.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "Ask5", curItem.Ask5.ToString("0.00").Replace(",", "."));

                    SendQuote(curItem.Ticker, "AskSize1", curItem.AskSize1.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "AskSize2", curItem.AskSize2.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "AskSize3", curItem.AskSize3.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "AskSize4", curItem.AskSize4.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "AskSize5", curItem.AskSize5.ToString("0.00").Replace(",", "."));

                    curSocket.SendMessage("BMFCONN" + (char)16 + "CMD" + (char)16 + "FLUSH" + (char)16 + "1" + (char)16 + "1");


                    //MarketDataAnalyzer.Instance.ReceiveLocalMarketDepth(curItem);
                }
            }
        }

        public void SendBidItem(MarketDepthItem curItem, int curPos)
        {
            switch(curPos)
            {
                case 1: 
                    SendQuote(curItem.Ticker, "Bid1", curItem.Bid1.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BidSize1", curItem.BidSize1.ToString("0.00").Replace(",", "."));
                    break;
                case 2:
                    SendQuote(curItem.Ticker, "Bid2", curItem.Bid2.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BidSize2", curItem.BidSize2.ToString("0.00").Replace(",", "."));
                    break;
                case 3:
                    SendQuote(curItem.Ticker, "Bid3", curItem.Bid3.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BidSize3", curItem.BidSize3.ToString("0.00").Replace(",", "."));
                    break;
                case 4:
                    SendQuote(curItem.Ticker, "Bid4", curItem.Bid4.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BidSize4", curItem.BidSize4.ToString("0.00").Replace(",", "."));
                    break;
                case 5:
                    SendQuote(curItem.Ticker, "Bid5", curItem.Bid5.ToString("0.00").Replace(",", "."));
                    SendQuote(curItem.Ticker, "BidSize5", curItem.BidSize5.ToString("0.00").Replace(",", "."));
                    break;
                default:
                    break;
            }
            curSocket.SendMessage("BMFCONN" + (char)16 + "CMD" + (char)16 + "FLUSH" + (char)16 + "1" + (char)16 + "1");
        }
               
	}  
       

}