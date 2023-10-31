using System;
using System.Collections.Generic;
using System.Xml;
using QuickFix42;

namespace FeedXML2FIX
{
    internal class SourceSafra
    {
        private frmMain _oMessenger;
        private string _sFilename = "Nest.xml";
        private string _sPath = @"T:\Import\XML Safra\";
        private string _sNode = "/identificador/order";

        public SourceSafra(frmMain messenger)
        {
            _oMessenger = messenger;
        }

        private void GetDatasource()
        {
            try
            {
                string fileName = "Nest.xml";
                string sourcePath = @"C:\FileSafra";
                string targetPath = @"T:\Import\XML Safra";

                string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
                string destFile = System.IO.Path.Combine(targetPath, fileName);

                System.IO.File.Copy(sourceFile, destFile, true);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("[Safra] Error on <GetDatasource>: " + e.Message.ToString());
            }
        }

        internal void DeliverDropcopy()
        {
            GetDatasource();


            XmlDocument oXmlDocument = new XmlDocument();
            oXmlDocument.Load(System.IO.Path.Combine(_sPath, _sFilename));
            XmlNodeList oNodes = oXmlDocument.SelectNodes(_sNode);


            DateTime dtDataPregao;
            string sSymbol, sSide, sQty, sPrice, sTime, sCodCliente, sOperator, sOrderControl;
            Dictionary<string, ExecutionReport> _dcTradeList = new Dictionary<string, ExecutionReport>();
            foreach (XmlNode curNode in oNodes)
            {
                dtDataPregao = Convert.ToDateTime(curNode.ChildNodes[0].InnerText);
                sTime = curNode.ChildNodes[1].InnerText;
                sSymbol = curNode.ChildNodes[2].InnerText;
                sQty = curNode.ChildNodes[3].InnerText; ;
                sPrice = curNode.ChildNodes[4].InnerText.Replace('.', ',');
                sCodCliente = curNode.ChildNodes[5].InnerText;
                sOperator = curNode.ChildNodes[6].InnerText;
                sOrderControl = curNode.ChildNodes[7].InnerText;
                sSide = curNode.ChildNodes[8].InnerText;

                if (dtDataPregao.Day == DateTime.Today.Day)
                {
                    double dQuantity, dPrice;
                    TimeSpan dtTime;
                    if (TimeSpan.TryParse(sTime, out dtTime) && double.TryParse(sQty, out dQuantity) && double.TryParse(sPrice, out dPrice))
                    {
                        dtDataPregao = dtDataPregao.Add(dtTime);
                        string sTradeKey = "xml" + StaticHelper.GenerateHash(new string[] { dtDataPregao.ToString("yyMMddHHmmss"), sSymbol, sSide, dQuantity.ToString("#0"), dPrice.ToString("@#0.0000").Replace(',','_'), "#", sCodCliente, sOrderControl, sOperator });

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

                        oTrade.setField(new QuickFix.Account("-1014"));
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
                            System.Diagnostics.Debug.WriteLine("[Safra] The token was already used - " + sTradeKey);
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
                System.Diagnostics.Debug.WriteLine("[Safra] " +_dcTradeList.Count.ToString() + " trades were processed.");
            }
        }

    }
}
