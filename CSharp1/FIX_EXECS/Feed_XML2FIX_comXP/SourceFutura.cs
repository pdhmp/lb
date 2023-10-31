using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using QuickFix42;

namespace FeedXML2FIX
{
    internal class SourceFutura
    {
        private frmMain _oMessenger;

        public SourceFutura(frmMain messenger)
        {
            _oMessenger = messenger;
        }

        private string GetDatasource()
        {
            GetFuturaExecutions.WSIntegrador operacoes = new GetFuturaExecutions.WSIntegrador();
            string sXml = "";

            try
            {
                sXml = operacoes.QueryTradeStr();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("[Futura] Error on <GetDatasource>: " + e.Message.ToString());
            }
            return sXml;
        }

        internal void DeliverDropcopy()
        {
            string TradesXml = GetDatasource();

            if (TradesXml != "")
            {
                TradesXml = TradesXml.Replace("\n", "");
                System.IO.StringReader Reader = new System.IO.StringReader(TradesXml);

                DataSet DataSet = new DataSet();
                DataSet.ReadXml(Reader, XmlReadMode.Auto);
                DataTable Table;

                if (DataSet.Tables.Count > 0)
                {
                    Table = DataSet.Tables[0];

                    DateTime DataPregao;
                    string Time, Symbol, Qty, Price, Account, Side, OrderID, TradeID;
                    Dictionary<string, ExecutionReport> _dcTradeList = new Dictionary<string, ExecutionReport>();
                    try
                    {
                        foreach (DataRow curRow in Table.Rows)
                        {
                            DataPregao = Convert.ToDateTime(curRow["dt-pregao"].ToString());
                            Time = curRow["hh-negocio"].ToString();
                            Symbol = curRow["cd-negocio"].ToString();
                            Qty = curRow["qt-negocio"].ToString();
                            Price = curRow["vl-negocio"].ToString().Replace(".", ",");
                            Account = curRow["cd-cliente"].ToString();
                            OrderID = curRow["nr-negocio"].ToString();
                            Side = curRow["tp-operacao"].ToString();
                            TradeID = curRow["trade_ID"].ToString();

                            if (DataPregao.Day == DateTime.Today.Day)
                            {
                                double Quantity, dPrice;
                                TimeSpan dtTime;
                                if (TimeSpan.TryParse(Time, out dtTime) && double.TryParse(Qty, out Quantity) && double.TryParse(Price, out dPrice))
                                {
                                    DataPregao = DataPregao.Add(dtTime);
                                    string sTradeKey = "xml" + StaticHelper.GenerateHash(new string[] { DataPregao.ToString("yyMMddHHmmss"), Symbol, Side, Quantity.ToString("#0"), dPrice.ToString("@#0.0000").Replace(',', '_'), "#", Account, OrderID });

                                    ExecutionReport oTrade = new ExecutionReport(
                                        new QuickFix.OrderID(sTradeKey),
                                        new QuickFix.ExecID(sTradeKey),
                                        new QuickFix.ExecTransType(QuickFix.ExecTransType.CORRECT),
                                        new QuickFix.ExecType(QuickFix.ExecType.FILL),
                                        new QuickFix.OrdStatus(QuickFix.OrdStatus.FILLED),
                                        new QuickFix.Symbol(Symbol),
                                        new QuickFix.Side(StaticHelper.GetSide(Side)),
                                        new QuickFix.LeavesQty(0),
                                        new QuickFix.CumQty(Quantity),
                                        new QuickFix.AvgPx(dPrice));

                                    oTrade.setField(new QuickFix.Account("-1179"));
                                    oTrade.setField(new QuickFix.OrderQty(Quantity));
                                    oTrade.setField(new QuickFix.LastShares(Quantity));
                                    oTrade.setField(new QuickFix.Price(dPrice));
                                    oTrade.setField(new QuickFix.LastPx(dPrice));
                                    oTrade.setField(new QuickFix.TransactTime(DataPregao));

                                    if (!_dcTradeList.ContainsKey(sTradeKey))
                                    {
                                        lock (_dcTradeList)
                                        {
                                            _dcTradeList.Add(sTradeKey, oTrade);
                                        }
                                    }
                                    else
                                    {
                                        System.Diagnostics.Debug.WriteLine("[Futura] The token was already used - " + sTradeKey);
                                    }
                                }
                            }
                        }
                        lock (_dcTradeList)
                        {
                            foreach (ExecutionReport curTrade in _dcTradeList.Values)
                            {
                                _oMessenger.SendFixMessage(curTrade);
                            }
                            System.Diagnostics.Debug.WriteLine("[Futura] " + _dcTradeList.Count.ToString() + " trades were processed.");
                        }
                    }
                    catch (Exception e)
                    {
                        System.Diagnostics.Debug.WriteLine("[Futura] Error on <DeliverDropcopy>: " + e.Message.ToString());
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("[Futura] The trade table is empty at the moment.");
                }
            }
        }

    }
}
