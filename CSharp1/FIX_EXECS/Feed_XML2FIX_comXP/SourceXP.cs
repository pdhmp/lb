using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using QuickFix42;

namespace FeedXML2FIX
{
    class SourceXP
    {
        public SourceXP(frmMain messenger) { Messenger = messenger; }

        private frmMain Messenger;

        internal void DeliverDropcopy()
        {
            string Login, Senha;
            Dictionary<string, ExecutionReport> TradeList = new Dictionary<string, ExecutionReport>();
            GetXPExecutions.ctServicoExterno curXP = new GetXPExecutions.ctServicoExterno();
            GetXPExecutions.XPSyncBrokerSOAPHeader Header = curXP.AutenticarBroker("NEST", "fMpE64");
            DateTime Date = DateTime.Today;

            for (int i = 0; i < Header.avaliableClients.Length; i++)
            {
                Header = curXP.AutenticarBroker("NEST", "fMpE64");

                curXP.XPSyncBrokerSOAPHeaderValue = Header;

                DataTable dtTrades = curXP.XPSyncNegociosConsultar("ANALITICO", Date.ToString("dd/MM/yyyy"), Date.ToString("dd/MM/yyyy"), Header.avaliableBrokers[0], Header.avaliableClients[i], "");

                Console.WriteLine("[XP] Getting trades for acct: " + Header.avaliableClients[i]);
                
                foreach (DataRow curRow in dtTrades.Rows)
                {
                    int Quantity = int.Parse(curRow["QUANTIDADE"].ToString());

                    double Price = double.Parse(curRow["P. UNITÁRIO"].ToString().Replace(".", ","));

                    string TradeAccount = curRow["CLIENTE"].ToString() + curRow["DÍGITO CLIENTE"].ToString();
                    string ExecId = curRow["NR. NEGÓCIO"].ToString() + curRow["NR. ORDEM"].ToString();
                    string OrderId = curRow["NR. ORDEM"].ToString();
                    string Ticker = curRow["PAPEL"].ToString();
                    string BloombergTicker = curRow["TICKER BLOOMBER"].ToString();
                    string TradeDate = curRow["DT. PREGÃO"].ToString();
                    string Side = curRow["NATUREZA"].ToString() == "C" ? "1" : "2";

                    ExecutionReport curTrade = new ExecutionReport(
                                        new QuickFix.OrderID(OrderId),
                                        new QuickFix.ExecID(ExecId),
                                        new QuickFix.ExecTransType(QuickFix.ExecTransType.CORRECT),
                                        new QuickFix.ExecType(QuickFix.ExecType.FILL),
                                        new QuickFix.OrdStatus(QuickFix.OrdStatus.FILLED),
                                        new QuickFix.Symbol(Ticker),
                                        new QuickFix.Side(StaticHelper.GetSide(Side)),
                                        new QuickFix.LeavesQty(0),
                                        new QuickFix.CumQty(Quantity),
                                        new QuickFix.AvgPx(Price));
                    curTrade.setField(new QuickFix.Account(TradeAccount));
                    curTrade.setField(new QuickFix.OrderQty(Quantity));
                    curTrade.setField(new QuickFix.LastShares(Quantity));
                    curTrade.setField(new QuickFix.Price(Price));
                    curTrade.setField(new QuickFix.LastPx(Price));
                    curTrade.setField(new QuickFix.TransactTime(Convert.ToDateTime(TradeDate)));


                    if (!TradeList.ContainsKey(ExecId))
                    {
                        lock (TradeList)
                        {
                            TradeList.Add(ExecId, curTrade);
                            Console.WriteLine(curTrade.ToString().Replace(((char)1).ToString(), "$"));
                        }
                    }
                    else
                    {
                        Console.WriteLine("[XP] The ExecId was already used - " + ExecId);
                    }
                }
            }
            lock (TradeList)
            {
                foreach (ExecutionReport curTrade in TradeList.Values) { Messenger.SendFixMessage(curTrade);}
                System.Diagnostics.Debug.WriteLine("[XP] " + TradeList.Count.ToString() + " trades were processed.");
            }
        }
    }
}
