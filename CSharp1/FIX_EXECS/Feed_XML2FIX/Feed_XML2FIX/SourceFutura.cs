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
            string sTradesXml = GetDatasource();

            if (sTradesXml != "")
            {
                sTradesXml = sTradesXml.Replace("\n", "");
                System.IO.StringReader oReader = new System.IO.StringReader(sTradesXml);

                DataSet oDataSet = new DataSet();
                oDataSet.ReadXml(oReader, XmlReadMode.Auto);
                DataTable oTable;

                if (oDataSet.Tables.Count > 0)
                {
                    oTable = oDataSet.Tables[0];

                    DateTime dtDataPregao;
                    string sTime, sSymbol, sQty, sPrice, sAccount, sSide, sOrderID, sTradeID;
                    Dictionary<string, ExecutionReport> _dcTradeList = new Dictionary<string, ExecutionReport>();
                    try
                    {
                        foreach (DataRow curRow in oTable.Rows)
                        {
                            dtDataPregao = Convert.ToDateTime(curRow["dt-pregao"].ToString());
                            sTime = curRow["hh-negocio"].ToString();
                            sSymbol = curRow["cd-negocio"].ToString();
                            sQty = curRow["qt-negocio"].ToString();
                            sPrice = curRow["vl-negocio"].ToString().Replace(".", ",");
                            sAccount = curRow["cd-cliente"].ToString();
                            sOrderID = curRow["nr-negocio"].ToString();
                            sSide = curRow["tp-operacao"].ToString();
                            sTradeID = curRow["trade_ID"].ToString();

                            if (dtDataPregao.Day == DateTime.Today.Day)
                            {
                                double dQuantity, dPrice;
                                TimeSpan dtTime;
                                if (TimeSpan.TryParse(sTime, out dtTime) && double.TryParse(sQty, out dQuantity) && double.TryParse(sPrice, out dPrice))
                                {
                                    dtDataPregao = dtDataPregao.Add(dtTime);
                                    string sTradeKey = "xml" + StaticHelper.GenerateHash(new string[] { dtDataPregao.ToString("yyMMddHHmmss"), sSymbol, sSide, dQuantity.ToString("#0"), dPrice.ToString("@#0.0000").Replace(',', '_'), "#", sAccount, sOrderID });

                                    ExecutionReport oTrade = new ExecutionReport(
                                        new QuickFix.OrderID(sTradeKey),
                                        new QuickFix.ExecID(sTradeKey),
                                        new QuickFix.ExecTransType(QuickFix.ExecTransType.CORRECT),
                                        new QuickFix.ExecType(QuickFix.ExecType.FILL),
                                        new QuickFix.OrdStatus(QuickFix.OrdStatus.FILLED),
                                        new QuickFix.Symbol(sSymbol),
                                        new QuickFix.Side(StaticHelper.GetSide(sSide)),
                                        new QuickFix.LeavesQty(0),
                                        new QuickFix.CumQty(dQuantity),
                                        new QuickFix.AvgPx(dPrice));

                                    oTrade.setField(new QuickFix.Account("-1131"));
                                    oTrade.setField(new QuickFix.OrderQty(dQuantity));
                                    oTrade.setField(new QuickFix.LastShares(dQuantity));
                                    oTrade.setField(new QuickFix.Price(dPrice));
                                    oTrade.setField(new QuickFix.LastPx(dPrice));
                                    oTrade.setField(new QuickFix.TransactTime(dtDataPregao));

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
