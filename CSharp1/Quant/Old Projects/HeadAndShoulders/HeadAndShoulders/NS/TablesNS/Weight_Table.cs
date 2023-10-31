using System;
using System.Data;

namespace NestSim
{
    public class Weight_Table : Base_Table
    {

        public Weight_Table(string _Name, int _Id_Ticker_Template, DateTime _iniDate, DateTime _endDate)
            : base(_Name, _Id_Ticker_Template, _iniDate, _endDate)
        {

        }

        public Weight_Table(string _Name, int Id_Ticker_Template)
            : base(_Name, Id_Ticker_Template)
        {
            
        }

        public void FillFromComposite(int Id_Ticker_Composite)
        {
            DataTable dt;
            string Id_Ticker_Component = "";
            string CurTableID = "";
            string SQLFields = "";

            string SQLString = "SELECT Id_Ticker_Component FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=" + Id_Ticker_Composite + " GROUP BY Id_Ticker_Component ORDER BY Id_Ticker_Component";

            dt = curConn.ExecuteDataTable(SQLString);

            foreach (DataRow curRow in dt.Rows)
            {
                AddValueColumn(Convert.ToInt16(curRow[0].ToString()));
                Id_Ticker_Component = curRow[0].ToString();
                if (Id_Ticker_Component != "0")
                {
                    CurTableID = "tab" + Id_Ticker_Component;
                    SQLFields = SQLFields + ", (SELECT TOP 1 Weight FROM NESTDB.dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite = " + Id_Ticker_Composite + " AND id_Ticker_Component=" + Id_Ticker_Component + " AND Date_Ref<=Idx.Data_Hora_Reg ORDER BY Date_Ref DESC) ";
                }
            }

            string SQLExpression = "SELECT Idx.Data_Hora_Reg" + SQLFields +
                            " FROM " +
                            " (SELECT Data_Hora_Reg, Valor FROM NESTDB.dbo.Tb053_Precos_Indices WHERE Id_Ativo=" + Id_Ticker_Template + " AND Tipo_Preco=1 AND Data_Hora_Reg>='" + _iniDate.ToString("yyyy-MM-dd") + "' AND Data_Hora_Reg<='" + _endDate.ToString("yyyy-MM-dd") + "' AND Source=1) AS Idx" +
                            " ORDER BY Idx.Data_Hora_Reg";

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

                    if (curRow[i] != null && curRow[i].ToString() != "")
                    {
                        try
                        {
                            Values[i - 1][j] = (float)Convert.ToDouble(curRow[i]);
                        }
                        catch
                        {
                            Values[i - 1][j] = 0;
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
                                    Values[i - 1][j] = 0;
                                }
                                break;
                            case Utils.TableFillTypes.FillNegVal: Values[i - 1][j] = -10000; break;
                            default: Values[i - 1][j] = 0; break;
                        }
                    }
                }
                j++;
            }
        }

        public void Convert_EW()
        {
            int noValcolumn = this.GetCustomColumnIndex ("COUNTNZ");

            if (noValcolumn = -1) { this.AddCountNonZero(); };

            if(noValcolumn >= 0)
            {
                for (int i = 1; i < _DateRowCount; i++)
                {
                    for (int j = 0; j < _ValueColumnCount; j++)
                    {
                        if (GetCustomValue(i, noValcolumn) > 0 && GetValue(i, j) != 0)
                        {
                            float tempValue = (1F / GetCustomValue(i, noValcolumn));
                            SetValue(i, j, tempValue);
                        }
                        else
                        {
                            SetValue(i, j, 0);
                        }
                    }
                }
            }
        }
    }
}
