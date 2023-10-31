namespace FeedXML2FIX
{
    static class StaticHelper
    {
        static public char GetSide(string side)
        {
            char cSide;
            switch (side.ToLower())
            {
                case "1":
                case "c":
                case "compra":
                case "b":
                case "buy":
                    cSide = QuickFix.Side.BUY;
                    break;
                case "2":
                case "v":
                case "venda":
                case "s":
                case "sell":
                case "sellshort":
                    cSide = QuickFix.Side.SELL;
                    break;
                default:
                    cSide = QuickFix.Side.UNDISCLOSED;
                    break;
            }
            return cSide;
        }

        static public string GenerateHash(string[] data)
        {
            string sReturn = "";
            for (int i = 0; i < data.Length; i++)
                sReturn += data[i];
            return sReturn;
        }
    }
}
