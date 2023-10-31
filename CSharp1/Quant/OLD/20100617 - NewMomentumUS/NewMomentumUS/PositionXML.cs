using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace NewMomentumUS
{
    public static class PositionXML
    {
        public static SortedDictionary<int, int> LoadPositions(string StrategyName) 
        {
            SortedDictionary<int, int> returnPositions = new SortedDictionary<int, int>();

            XmlDocument positions = new XmlDocument();
            bool canRead = false;

            try
            {
                positions.Load("Positions.xml");
                canRead = true;
            }
            catch
            {
                canRead = false;
            }

            if (canRead)
            {
                foreach (XmlNode strategy in positions.SelectNodes("/Positions/Strategy"))
                {
                    string stratName = strategy.SelectSingleNode("./Name").InnerText;
                    if (stratName == StrategyName)
                    {
                        foreach (XmlElement ticker in strategy.SelectNodes("./Tickers/Ticker"))
                        {
                            int ID_Ticker = Convert.ToInt32(ticker.Attributes["IDTicker"].Value);
                            int Quantity = Convert.ToInt32(ticker.Attributes["Quantity"].Value);

                            returnPositions.Add(ID_Ticker, Quantity);
                        }
                    }
                }
            }

            return returnPositions;
        }

        public static bool SavePositions(string StrategyName, SortedDictionary<int, int> StrategyPositions)
        {
            XmlDocument positions = new XmlDocument();
            bool canSave = false;

            try
            {
                positions.Load("Positions.xml");
                positions.Save(DateTime.Now.ToString("yyyyMMdd_HHmmssfff") + "_Positions.xml");
                canSave = true;
            }
            catch
            {
                canSave = false;
            }

            if (canSave)
            {
                XmlNode strategy = positions.CreateElement("Strategy");

                XmlNode name = positions.CreateElement("Name");
                name.InnerText = StrategyName;
                strategy.AppendChild(name);

                XmlNode tickers = positions.CreateElement("Tickers");

                foreach (KeyValuePair<int, int> position in StrategyPositions)
                {
                    XmlElement xmlPosition = positions.CreateElement("Ticker");
                    
                    XmlAttribute xmlIDTicker = positions.CreateAttribute("IDTicker");
                    xmlIDTicker.Value = position.Key.ToString();

                    XmlAttribute xmlQuantity = positions.CreateAttribute("Quantity");
                    xmlQuantity.Value = position.Value.ToString();

                    xmlPosition.Attributes.Append(xmlIDTicker);
                    xmlPosition.Attributes.Append(xmlQuantity);

                    tickers.AppendChild(xmlPosition);
                }

                strategy.AppendChild(tickers);

                foreach (XmlNode remove in positions.SelectNodes("/Positions/Strategy"))
                {
                    string removeName = remove.SelectSingleNode("./Name").InnerText;

                    if (removeName == StrategyName)
                    {
                        positions.SelectSingleNode("/Positions").RemoveChild(remove);
                    }
                }
                
                positions.SelectSingleNode("Positions").AppendChild(strategy);

                try
                {
                    positions.Save("Positions.xml");
                    canSave = true;
                }
                catch
                {
                    canSave = false;
                }
            }

            return canSave;
        }
    }
}
