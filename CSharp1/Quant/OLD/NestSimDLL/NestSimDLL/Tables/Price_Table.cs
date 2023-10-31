using System;
using System.Data;
using System.Collections.Generic;

namespace NestQuant.Common
{
    public class Price_Table : Base_Table
    {                
        public Price_Table(string _Name, int _Id_Ticker_Template, DateTime _iniDate, DateTime _endDate, bool isRealTime)
            : base(_Name, _Id_Ticker_Template, _iniDate, _endDate, isRealTime)
        {
        }

        public Price_Table(string _Name, int _Id_Ticker_Template, DateTime _iniDate, DateTime _endDate)
            : base(_Name, _Id_Ticker_Template, _iniDate, _endDate)
        {
        }
        
        public Price_Table(string _Name, int Id_Ticker_Template, bool isRealTime)
            : base(_Name, Id_Ticker_Template, isRealTime)
        {            
        }

        public Price_Table(string _Name, int Id_Ticker_Template)
            : base(_Name, Id_Ticker_Template)
        {
        }

        public void FillFromComposite(int Id_Ticker_Composite, int Price_Type, int Price_Source)
        {
            FillFromComposite(Id_Ticker_Composite, Price_Type, Price_Source, false);
        }

        public void FillFromComposite(int Id_Ticker_Composite, int Price_Type, int Price_Source, bool SubsNull)
        {
            DataTable dt;
            
            _Id_Ticker_Composite = Id_Ticker_Composite;
            _ValueColumnType = Utils.TableTypes.Id_Ticker;

            string SQLString = "SELECT Id_Ticker_Component FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=" + Id_Ticker_Composite + " GROUP BY Id_Ticker_Component ORDER BY Id_Ticker_Component";

            dt = curConn.ExecuteDataTable(SQLString);

            int[] tempTickers = new int[dt.Rows.Count];
            int i=0;

            foreach (DataRow curRow in dt.Rows)
            {
                tempTickers[i] = Convert.ToInt32(curRow[0]);
                i++;
            }

            fillFromArray(tempTickers, Price_Type, Price_Source, SubsNull);
        }

        public void fillFromSingleTicker(int Id_Ticker, int Price_Type, int Price_Source)
        {
            int[] tempTickers = new int[0];
            tempTickers[0] = Id_Ticker;
            fillFromArray(tempTickers, Price_Type, Price_Source);
        }

        private void fillFromArray(int[] Id_Tickers, int Price_Type, int Price_Source)
        {
            fillFromArray(Id_Tickers, Price_Type, Price_Source, false);
        }

        private void fillFromArray(int[] Id_Tickers, int Price_Type, int Price_Source, bool SubsNull)
        {
            string SQLFields = "";
            string SQLTables = "";
            bool comma = false;
            string Id_Ticker_Component = "";
            string CurTableID = "";
            DataTable dt;
            string TableName = "";
            string idxTableName = "";

            TableName = Utils.GetTableName(Id_Tickers[0]);
            idxTableName = Utils.GetTableName(Id_Ticker_Template);

            for (int i = 0; i < Id_Tickers.Length; i++)
            {
                AddValueColumn(Id_Tickers[i]);
                Id_Ticker_Component = Id_Tickers[i].ToString();
                if (Id_Ticker_Component != "0")
                {
                    SQLFields = SQLFields + ", \r\n sum(case prices.id_ativo when " + Id_Ticker_Component.ToString() + " then ISNULL(prices.valor,0) else 0 end) as L" + Id_Ticker_Component.ToString();
                    if (comma)
                    {
                        SQLTables = SQLTables + ", " + Id_Ticker_Component.ToString();
                    }
                    else
                    {
                        SQLTables = SQLTables + Id_Ticker_Component.ToString();
                        comma = true;
                    }
                    
                    
//                    CurTableID = "tab" + Id_Ticker_Component;
//                    SQLFields = SQLFields + ", " + CurTableID + ".Valor AS L" + Id_Ticker_Component;
//                    SQLTables = SQLTables + " LEFT JOIN (SELECT Data_Hora_Reg, Valor FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE Id_Ativo=" + Id_Ticker_Component + " AND Tipo_Preco=" + Price_Type.ToString() + " AND Data_Hora_Reg>='" + _iniDate.ToString("yyyy-MM-dd") + "' AND Data_Hora_Reg<='" + _endDate.ToString("yyyy-MM-dd") + "' AND Source=" + Price_Source.ToString() + ") AS " + CurTableID + " ON Idx.Data_Hora_Reg=" + CurTableID + ".Data_Hora_Reg ";
                }
            }

            string SQLExpression = "SELECT Idx.Data_Hora_Reg" + SQLFields +
                "\r\n FROM " +
                "\r\n (SELECT Data_Hora_Reg, Valor FROM NESTDB.dbo." + idxTableName + " " +
                "\r\n WHERE Id_Ativo=" + Id_Ticker_Template + " AND Tipo_Preco=1 "+
                "\r\n AND Data_Hora_Reg>='" + _iniDate.ToString("yyyy-MM-dd") + "' AND Data_Hora_Reg<='" + _endDate.ToString("yyyy-MM-dd") + "' "+
                "\r\n AND Source=1) AS Idx" +
                "\r\n LEFT JOIN (SELECT Data_Hora_Reg, Id_Ativo, Valor FROM NESTDB.dbo." + TableName + " " +
                "\r\n WHERE Id_Ativo in (" + SQLTables + ") AND Tipo_Preco=" + Price_Type.ToString() + 
                "\r\n AND Data_Hora_Reg>='" + _iniDate.ToString("yyyy-MM-dd") + "' AND Data_Hora_Reg<='" + _endDate.ToString("yyyy-MM-dd") + "' "+
                "\r\n AND Source=" + Price_Source.ToString() + ") AS prices "+
                "\r\n ON Idx.Data_Hora_Reg= prices.Data_Hora_Reg " +
                "\r\n GROUP BY Idx.Data_Hora_Reg"+
                "\r\n ORDER BY Idx.Data_Hora_Reg";
            

//            string SQLExpression = "SELECT Idx.Data_Hora_Reg" + SQLFields +
//                            " FROM " +
//                            " (SELECT Data_Hora_Reg, Valor FROM NESTDB.dbo.Tb053_Precos_Indices WHERE Id_Ativo=" + Id_Ticker_Template + " AND Tipo_Preco=1 AND Data_Hora_Reg>='" + _iniDate.ToString("yyyy-MM-dd") + "' AND Data_Hora_Reg<='" + _endDate.ToString("yyyy-MM-dd") + "' AND Source=1) AS Idx" +
//                            SQLTables +
//                            " ORDER BY Idx.Data_Hora_Reg";


            dt = curConn.ExecuteDataTable(SQLExpression);

            int j = 0;

            foreach (DataRow curRow in dt.Rows)
            {
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    if (DateRows[j] != DateTime.Parse(curRow[0].ToString()))
                    {
                        break;
                    }

                    if (curRow[i] != null && curRow[i].ToString() != "" && double.Parse(curRow[i].ToString()) != 0)
                    {
                        try
                        {
                            Values[i - 1][j] = (double)Convert.ToDouble(curRow[i]);
                        }
                        catch
                        {
                            if (SubsNull)
                            {
                                Values[i - 1][j] = double.NaN;
                            }
                            else
                            {
                                Values[i - 1][j] = 0;
                            }
                        }
                    }
                    else
                    {
                        switch (FillStyle)
                        {
                            case Utils.TableFillTypes.FillZero: Values[i - 1][j] = 0; break;
                            case Utils.TableFillTypes.FillPrevious:
                                if (j > 0)
                                {
                                    Values[i - 1][j] = Values[i - 1][j - 1];
                                }
                                else
                                {
                                    Values[i - 1][j] = double.NaN;
                                }
                                break;
                            case Utils.TableFillTypes.FillNaN: Values[i - 1][j] = double.NaN; break;
                            default: Values[i - 1][j] = 0; break;
                        }
                    }
                }
                j++;
            }

            if (IsRealTime)
            {
                for (int k = 0; k < ValueColumnCount; k++)
                {
                    Values[k][DateRowCount - 1] = Values[k][DateRowCount - 2];
                }
            }

            if (Price_Type == 101)
            {
                AdjustToLastPrice();
            }

        }

        public void FillCumulative()
        {
            for (int i = 1; i < this.DateRowCount; i++)
            {
                for (int j = 0; j < this.ValueColumnCount; j++)
                {
                    this.SetValue(i,j,this.GetValue(i,j) + this.GetValue(i-1,j));
                }
            }
        }

        private void AdjustToLastPrice()
        {
            foreach(KeyValuePair<int,int> tickerIndex in ValuePos)
            {
                double LastPrice = 0;
                double LastIndex = 0;
                string SQLExpression = ""; 

                using (NestConn conn = new NestConn())
                {
                    SQLExpression = "SELECT NESTDB.dbo.FCN_GET_PRICE_Value_Only(" + tickerIndex.Key + ", '" + endDate.ToString("yyyyMMdd") + "', 1, 0, 2, 0, 0)";
                    DataTable dt = conn.ExecuteDataTable(SQLExpression);
                    LastPrice = Convert.ToDouble(dt.Rows[0][0]);

                    SQLExpression = "SELECT NESTDB.dbo.FCN_GET_PRICE_Value_Only(" + tickerIndex.Key + ", '" + endDate.ToString("yyyyMMdd") + "', 101, 0, 2, 0, 0)";
                    dt = conn.ExecuteDataTable(SQLExpression);
                    LastIndex = Convert.ToDouble(dt.Rows[0][0]);
                }

                for (int i = 0; i < DateRowCount; i++)
                {
                    double adjustedPrice = GetValue(i, tickerIndex.Value) / LastIndex * LastPrice;
                    SetValue(i, tickerIndex.Value, adjustedPrice);
                }
                
                
            }
        }

        public void SubscribeRealTime(RTPrice PriceFeeder)
        {
            int[] tickerList = (int[])ValueColumns.Clone();

            PriceFeeder.Subscribe(this, tickerList);
        }

        protected override void UpdateTable(int[] RTTickers, double[] RTPrices)
        {
            for (int i = 0; i < RTTickers.Length; i++)
            {
                int TickerIndex = GetValueColumnIndex(RTTickers[i]);
                int LastDateIndex = DateRowCount - 1;

                if (TickerIndex != -1 && LastDateIndex != -1)
                {
                    if (!double.IsNaN(RTPrices[i]))
                    {
                        SetValue(LastDateIndex, TickerIndex, RTPrices[i]);
                    }
                }
                else
                {
                    throw new System.NotImplementedException();
                }    
            }            
        }       
    }
}
