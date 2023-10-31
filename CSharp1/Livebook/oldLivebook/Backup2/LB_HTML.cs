using System;
using System.Collections.Generic;
using System.Text;

using NestDLL;
using System.Data;
using System.Data.SqlClient;

namespace SGN
{
    class LB_HTML
    {
        CarregaDados CargaDados = new CarregaDados();

        public string Risk_Summary(bool InternalOnly)
        {
            return Risk_Summary(-1, InternalOnly);
        }

        public string Risk_Summary(int Id_Portfolio, bool InternalOnly)
        {
            string tempHTML = "";
            int prevFund = 0;
            int prevGroup = 0;

            string SQLString = "SELECT * FROM dbo.[FCN_Risk_Limits]() WHERE Id_Portfolio <> -10 ORDER BY Id_Portfolio, Valid_As_Of, Id_Risk_Limit_Group, Risk_Limit_Type";

            if (!InternalOnly)
            {
                SQLString = SQLString.Replace("WHERE ", "WHERE IsInternal<>1 AND ");
            }
            if (Id_Portfolio != -1)
            {
                SQLString = SQLString.Replace("WHERE", " WHERE Id_Portfolio=" + Id_Portfolio + " AND ");
            }


            DataTable tablep = CargaDados.curConn.Return_DataTable(SQLString);

            foreach (DataRow curRow in tablep.Rows)
            {
                if (InternalOnly || (int)curRow["Risk_Limit_Type"] != 20)
                {
                    if (Convert.ToInt32(curRow["Id_Portfolio"]) != prevFund)
                    {
                        if (prevFund != 0)
                        {
                            tempHTML = tempHTML + "</table>";
                        }
                        tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
                        tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>" + curRow["Port_Name"] + "</div>";
                        tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";
                        tempHTML = tempHTML + "<table border=0 cellspacing=0>";
                        tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                                    "<td align=left width=200>Limit Type</td>";
                        if (InternalOnly) { tempHTML = tempHTML + "<td align=center width=60>Relevant Value</td>"; }
                        tempHTML = tempHTML + "<td align=center width=60>Limit Value</td>" +
                                                    "<td align=center width=60>Current Value</td>" +
                                                    "<td align=center width=60>Percent Limit</td>";

                        if (InternalOnly) { tempHTML = tempHTML + "<td align=center width=100>Description</td>"; }
                        tempHTML = tempHTML + "</tr>";

                        prevGroup = 0;
                    }
                    if (Convert.ToInt32(curRow["Id_Risk_Limit_Group"]) != prevGroup)
                    {
                        tempHTML = tempHTML + "<tr>";
                        tempHTML = tempHTML + "<td style='color=#FF9900;font-family:Tahoma; font-size=8pt; font-weight:bold'>" + curRow["Risk_Group_Description"] + "</td>";
                        tempHTML = tempHTML + "</tr>";
                    }
                    tempHTML = tempHTML + "<tr>";

                    tempHTML = tempHTML + "<td style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + curRow["Risk_Limit_Description"];
                    tempHTML = tempHTML + "</td>";
                    if (InternalOnly)
                    {

                        tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                        tempHTML = tempHTML + ifStrNull(curRow["Relevant_Value"]);
                        tempHTML = tempHTML + "</td>";
                    }
                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Risk_Limit_Value"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    float Limit_Value = Convert.ToSingle((curRow["Risk_Limit_Value"]));

                    if (Convert.ToSingle((curRow["Risk_Limit_Type"])) == 1 || Convert.ToSingle((curRow["Risk_Limit_Type"])) == 2)
                    {
                        Limit_Value = Convert.ToSingle((curRow["Relevant_Value"]));
                    }


                    string SQLString_Actual = "SELECT Id_Porfolio, Limit_Value, Limit_Description FROM [dbo].[FCN_Risk_Limit_Value] (" + curRow["Id_Portfolio"].ToString() + ", " + curRow["Risk_Limit_Type"].ToString() + ")";

                    DataTable tablep_Actual = CargaDados.curConn.Return_DataTable(SQLString_Actual);

                    foreach (DataRow curRow_Actual in tablep_Actual.Rows)
                    {
                        bool PutPercent = false;
                        string formatString = "";
                        float Current_Value = 0;

                        if (!DBNull.Value.Equals(curRow_Actual["Limit_Value"]))
                        {
                            Current_Value = Convert.ToSingle((curRow_Actual["Limit_Value"]));
                            if (Math.Abs(Current_Value) / Math.Abs(Limit_Value) > 0.8)
                            {
                                formatString = "color: #E66C2C; font-weight:bold;";
                            }
                            if (Math.Abs(Current_Value) > Math.Abs(Limit_Value))
                            {
                                formatString = "color: red; font-weight:bold;";
                            }
                            PutPercent = true;
                        }

                        tempHTML = tempHTML + "<td align=right style='" + formatString + "font-family:Tahoma; font-size=8pt'>";
                        tempHTML = tempHTML + Current_Value.ToString("0.00%");
                        tempHTML = tempHTML + "</td>";

                        tempHTML = tempHTML + "<td align=center style='" + formatString + "font-family:Tahoma; font-size=8pt'>";
                        if (PutPercent)
                        {
                            tempHTML = tempHTML + (Math.Abs(Current_Value) / Math.Abs(Limit_Value)).ToString("0%");
                        }
                        else
                        {
                            tempHTML = tempHTML + "&nbsp";
                        }
                        tempHTML = tempHTML + "</td>";
                        if (InternalOnly)
                        {
                            tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                            tempHTML = tempHTML + "&nbsp;&nbsp;" + curRow_Actual["Limit_Description"].ToString();
                            tempHTML = tempHTML + "</td>";
                        }
                    }
                }
                tempHTML = tempHTML + "</tr>";
                prevFund = Convert.ToInt32(curRow["Id_Portfolio"]);
                prevGroup = Convert.ToInt32(curRow["Id_Risk_Limit_Group"]);
            }

            tempHTML = tempHTML + "</table>";
            //tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";

            return tempHTML;

        }

        public string Risk_Options(int Id_Portfolio)
        {
            return Risk_Options(Id_Portfolio, false);
        }

        public string Risk_Options(int Id_Portfolio, bool PublicData)
        {
            string tempHTML = "";
            string PortName = "";
            string SQLString = "";
            DataTable tablep = new DataTable();

            PortName = CargaDados.curConn.Execute_Query_String("Select Port_Name from  dbo.Tb002_Portfolios where Id_Portfolio=" + Id_Portfolio.ToString());
            if (!PublicData)
            {
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=14pt; font-weight:bold'>" + PortName + "</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";
            }
            // ======================= PORTFOLIO TOTAL =======================

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Option Premium</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

            tempHTML = tempHTML + "<table border=0 cellspacing=0>";

            tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=120>Side</td>" +
                                        "<td align=center width=60>Calls</td>" +
                                        "<td align=center width=60>Puts</td>" +
                                        "<td align=center width=60>Total</td>" +
                                    "</tr>";

            SQLString = "SELECT Side, SUM(Call) AS Call, SUM(Put) AS Put, SUM(Total) AS Total FROM " +
                                " ( " +
                                " SELECT CASE WHEN [Cash Premium]/[NAV pC]>0 THEN 'Long' ELSE 'Short' END AS Side " +
                                " , CASE WHEN [Option Type]=1 THEN coalesce([Cash Premium]/[NAV pC],0) ELSE 0 END AS Call " +
                                " , CASE WHEN [Option Type]=0 THEN coalesce([Cash Premium]/[NAV pC],0) ELSE 0 END AS Put " +
                                " , coalesce([Cash Premium]/[NAV pC],0) AS Total " +
                                " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 " +
                                " ) A " +
                                " GROUP BY Side";

            tablep.Clear();
            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            foreach (DataRow curRow in tablep.Rows)
            {
                tempHTML = tempHTML + "<tr>";

                tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + curRow["Side"];
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + Convert.ToSingle((curRow["Call"])).ToString("0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + Convert.ToSingle((curRow["Put"])).ToString("0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; font-weight:bold'>";
                tempHTML = tempHTML + Convert.ToSingle((curRow["Total"])).ToString("0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";
            }

            tempHTML = tempHTML + "</table>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";

            // ======================= TV vs Intrinsic  =======================

            if (!PublicData)
            {

                tempHTML = tempHTML + "<table border=0 cellspacing=0>";

                tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                            "<td align=left width=120>Side</td>" +
                                            "<td align=center width=60>Time Val</td>" +
                                            "<td align=center width=60>Intrinsic</td>" +
                                            "<td align=center width=60>Total</td>" +
                                        "</tr>";

                SQLString = "SELECT Side, SUM(TV) AS TV, SUM(Intrinsic) AS Intrinsic, SUM(Total) AS Total FROM " +
                                    " ( " +
                                    " SELECT CASE WHEN [Position]>0 THEN 'Long' ELSE 'Short' END AS Side " +
                                    " , coalesce([Option TV Cash pC]/[NAV pC],0) AS TV " +
                                    " , coalesce([Option Intrinsic Cash pC]/[NAV pC],0) AS Intrinsic" +
                                    " , coalesce(([Option Intrinsic Cash pC]+[Option TV Cash pC])/[NAV pC],0) AS Total " +
                                    " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                    " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 " +
                                    " ) A " +
                                    " GROUP BY Side";

                tablep.Clear();
                tablep = CargaDados.curConn.Return_DataTable(SQLString);

                float TotalTV = 0;
                float TotalIntrinsic = 0;
                float TotalPremuim = 0;

                foreach (DataRow curRow in tablep.Rows)
                {
                    tempHTML = tempHTML + "<tr>";

                    tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + curRow["Side"];
                    tempHTML = tempHTML + "</td>";

                    if (!DBNull.Value.Equals(curRow["TV"]))
                    {
                        TotalTV = TotalTV + Convert.ToSingle((curRow["TV"]));
                        tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                        tempHTML = tempHTML + ifStrNull((curRow["TV"]));
                        tempHTML = tempHTML + "</td>";

                        TotalIntrinsic = TotalIntrinsic + Convert.ToSingle((curRow["Intrinsic"]));
                        tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                        tempHTML = tempHTML + Convert.ToSingle((curRow["Intrinsic"])).ToString("0.00%");
                        tempHTML = tempHTML + "</td>";

                        TotalPremuim = TotalPremuim + Convert.ToSingle((curRow["Total"]));
                        tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; font-weight:bold'>";
                        tempHTML = tempHTML + Convert.ToSingle((curRow["Total"])).ToString("0.00%");
                        tempHTML = tempHTML + "</td>";

                    }
                    tempHTML = tempHTML + "</tr>";
                }

                tempHTML = tempHTML + "<tr>";
                tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>Total</td>";
                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>" + TotalTV.ToString("0.00%") + "</td>";
                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>" + TotalIntrinsic.ToString("0.00%") + "</td>";
                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; font-weight:bold''>" + TotalPremuim.ToString("0.00%") + "</td>";
                tempHTML = tempHTML + "</tr>";
            }

            // ---               THETA TOTALS            ---

            if (!PublicData)
            {
                tempHTML = tempHTML + "<tr><td colspan=4 style='font-family:Tahoma; font-size=4pt'>&nbsp;</tr>";

                SQLString = "SELECT coalesce(SUM(Call),0) AS Call, coalesce(SUM(Put),0) AS Put, coalesce(SUM(Total),0) AS Total FROM " +
                                   " ( " +
                                   " SELECT CASE WHEN [Option Type]=1 THEN coalesce([Theta/NAV],0) ELSE 0 END AS Call " +
                                   " , CASE WHEN [Option Type]=0 THEN coalesce([Theta/NAV],0) ELSE 0 END AS Put " +
                                   " , coalesce([Theta/NAV],0) AS Total " +
                                   " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                   " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 " +
                                   " ) A ";

                tablep.Clear();
                tablep = CargaDados.curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in tablep.Rows)
                {
                    tempHTML = tempHTML + "<tr>";

                    tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + "1-day Decay";
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Call"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Put"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; font-weight:bold'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Total"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "</tr>";
                }

                tempHTML = tempHTML + "</table>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";
            }

            // ======================= PREMIUM TYPE BREAKDOWN =======================

            if (!PublicData)
            {
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Option Premium by Type</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

                tempHTML = tempHTML + "<table border=0 cellspacing=0>";

                tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                            "<td align=left width=120>Type</td>" +
                                            "<td align=center width=60>Calls</td>" +
                                            "<td align=center width=60>Puts</td>" +
                                            "<td align=center width=60>Total</td>" +
                                        "</tr>";

                SQLString = "SELECT 'Equities' AS curType, coalesce(SUM(Call),0) AS Call, coalesce(SUM(Put),0) AS Put, coalesce(SUM(Total),0) AS Total FROM " +
                                    " ( " +
                                    " SELECT CASE WHEN [Option Type]=1 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Call " +
                                    " , CASE WHEN [Option Type]=0 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Put " +
                                    " , [Cash Premium]/[NAV pC] AS Total " +
                                    " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                    " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 AND [New Strategy]='Equities'" +
                                    " ) A " +
                                    " UNION " +
                                    " SELECT'Interest Rates' AS curType, coalesce(SUM(Call),0) AS Call, coalesce(SUM(Put),0) AS Put, coalesce(SUM(Total),0) AS Total FROM " +
                                    " ( " +
                                    " SELECT CASE WHEN [Option Type]=1 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Call " +
                                    " , CASE WHEN [Option Type]=0 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Put " +
                                    " , [Cash Premium]/[NAV pC] AS Total " +
                                    " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                    " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 AND [New Strategy]='FI'" +
                                    " ) B " +
                                    " UNION " +
                                    " SELECT 'Currency' AS curType, coalesce(SUM(Call),0) AS Call, coalesce(SUM(Put),0) AS Put, coalesce(SUM(Total),0) AS Total FROM " +
                                    " ( " +
                                    " SELECT CASE WHEN [Option Type]=1 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Call " +
                                    " , CASE WHEN [Option Type]=0 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Put " +
                                    " , [Cash Premium]/[NAV pC] AS Total " +
                                    " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                    " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 AND [New Strategy]='Currency'" +
                                    " ) C" +
                                    " UNION " +
                                    " SELECT 'Commodities' AS curType, coalesce(SUM(Call),0) AS Call, coalesce(SUM(Put),0) AS Put, coalesce(SUM(Total),0) AS Total FROM " +
                                    " ( " +
                                    " SELECT CASE WHEN [Option Type]=1 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Call " +
                                    " , CASE WHEN [Option Type]=0 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Put " +
                                    " , [Cash Premium]/[NAV pC] AS Total " +
                                    " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                    " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 AND [New Strategy]='Commodities' " +
                                    " ) D" +
                                    " ORDER BY curType DESC";

                tablep.Clear();
                tablep = CargaDados.curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in tablep.Rows)
                {
                    tempHTML = tempHTML + "<tr>";

                    tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + curRow["curtype"];
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + ifStrNull(curRow["Call"]);
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + ifStrNull(curRow["Put"]);
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; font-weight:bold'>";
                    tempHTML = tempHTML + ifStrNull(curRow["Total"]);
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "</tr>";
                }

                tempHTML = tempHTML + "</table>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";
            }
            // ======================= DELTA TYPE BREAKDOWN =======================

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Option Delta by Type</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

            tempHTML = tempHTML + "<table border=0 cellspacing=0>";

            tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=120>Type</td>" +
                                        "<td align=center width=60>Calls</td>" +
                                        "<td align=center width=60>Puts</td>" +
                                        "<td align=center width=60>Total</td>" +
                                    "</tr>";

            SQLString = "SELECT * FROM (SELECT 'Equities' AS curType, coalesce(SUM(Call),0) AS Call,coalesce( SUM(Put),0) AS Put, coalesce(SUM(Total),0) AS Total FROM " +
                                " ( " +
                                " SELECT CASE WHEN [Option Type]=1 THEN [Delta/NAV] ELSE 0 END AS Call " +
                                " , CASE WHEN [Option Type]=0 THEN [Delta/NAV] ELSE 0 END AS Put " +
                                " , [Delta/NAV] AS Total " +
                                " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 AND [New Strategy]='Equities'" +
                                " ) A " +
                                " UNION " +
                                " SELECT'Interest Rates' AS curType, coalesce(SUM(Call),0) AS Call, coalesce(SUM(Put),0) AS Put, coalesce(SUM(Total),0) AS Total FROM " +
                                " ( " +
                                " SELECT CASE WHEN [Option Type]=1 THEN [Delta/NAV] ELSE 0 END AS Call " +
                                " , CASE WHEN [Option Type]=0 THEN [Delta/NAV] ELSE 0 END AS Put " +
                                " , [Delta/NAV] AS Total " +
                                " FROM NESTRT.dbo.FCN_Posicao_Atual()" +
                                " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 AND [New Strategy]='FI'" +
                                " ) B " +
                                " UNION " +
                                " SELECT 'Currency' AS curType, coalesce(SUM(Call),0) AS Call,  coalesce(SUM(Put),0) AS Put,  coalesce(SUM(Total),0) AS Total FROM " +
                                " ( " +
                                " SELECT CASE WHEN [Option Type]=1 THEN [Delta/NAV] ELSE 0 END AS Call " +
                                " , CASE WHEN [Option Type]=0 THEN [Delta/NAV] ELSE 0 END AS Put " +
                                " , [Delta/NAV] AS Total " +
                                " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3  AND [New Strategy]='Currency'" +
                                " ) C " +
                                " UNION " +
                                " SELECT 'Commodities' AS curType, coalesce(SUM(Call),0) AS Call, coalesce(SUM(Put),0) AS Put, coalesce(SUM(Total),0) AS Total FROM " +
                                " ( " +
                                " SELECT CASE WHEN [Option Type]=1 THEN [Delta/NAV] ELSE 0 END AS Call " +
                                " , CASE WHEN [Option Type]=0 THEN [Delta/NAV] ELSE 0 END AS Put " +
                                " , [Delta/NAV] AS Total " +
                                " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 AND [New Strategy]='Commodities' " +
                                " ) D ) X" +
                                " ORDER BY CASE WHEN curType='Equities' THEN 0 ELSE 1 END";

            tablep.Clear();
            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            foreach (DataRow curRow in tablep.Rows)
            {
                tempHTML = tempHTML + "<tr>";

                tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + curRow["curtype"];
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Call"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Put"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; font-weight:bold'>";
                tempHTML = tempHTML + ifStrNull(curRow["Total"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";
            }

            tempHTML = tempHTML + "</table>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";

            // ======================= CURRENCY BREAKDOWN =======================

            if (!PublicData)
            {
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Currency Breakdown</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

                tempHTML = tempHTML + "<table border=0 cellspacing=0>";

                tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                            "<td align=left width=120>Currency</td>" +
                                            "<td align=center width=60>Calls</td>" +
                                            "<td align=center width=60>Puts</td>" +
                                            "<td align=center width=60>Total</td>" +
                                        "</tr>";

                SQLString = "SELECT [Security Currency], coalesce(SUM(Call),0) AS Call, coalesce(SUM(Put),0) AS Put, coalesce(SUM(Total),0) AS Total FROM " +
                                    " ( " +
                                    " SELECT [Security Currency] " +
                                    " , CASE WHEN [Option Type]=1 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Call " +
                                    " , CASE WHEN [Option Type]=0 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Put " +
                                    " , [Cash Premium]/[NAV pC] AS Total " +
                                    " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                    " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 " +
                                    " ) A " +
                                    " GROUP BY [Security Currency]";

                tablep.Clear();
                tablep = CargaDados.curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in tablep.Rows)
                {
                    tempHTML = tempHTML + "<tr>";

                    tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + curRow["Security Currency"];
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Call"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Put"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; font-weight:bold'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Total"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "</tr>";
                }

                tempHTML = tempHTML + "</table>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";
            }
            // ======================= EXPIRATION BREAKDOWN =======================

            if (!PublicData)
            {
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Expiration Breakdown</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

                tempHTML = tempHTML + "<table border=0 cellspacing=0>";

                tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                            "<td align=left width=120>Expiration</td>" +
                                            "<td align=center width=60>Calls</td>" +
                                            "<td align=center width=60>Puts</td>" +
                                            "<td align=center width=60>Total</td>" +
                                        "</tr>";

                SQLString = "SELECT MIN([Expiration]) AS Expiration, coalesce(SUM(Call),0) AS Call, coalesce(SUM(Put),0) AS Put, coalesce(SUM(Total),0) AS Total FROM " +
                                    " ( " +
                                    " SELECT [Expiration]  " +
                                    " , CASE WHEN [Option Type]=1 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Call " +
                                    " , CASE WHEN [Option Type]=0 THEN [Cash Premium]/[NAV pC] ELSE 0 END AS Put " +
                                    " , [Cash Premium]/[NAV pC] AS Total " +
                                    " FROM NESTRT.dbo.FCN_Posicao_Atual() " +
                                    " WHERE [Id Portfolio]=" + Id_Portfolio.ToString() + " AND [Id instrument]=3 " +
                                    " ) A " +
                                    " GROUP BY CONVERT(varchar(6),[Expiration],112) " +
                                    " ORDER BY CONVERT(varchar(6),[Expiration],112) ";

                tablep.Clear();
                tablep = CargaDados.curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in tablep.Rows)
                {
                    tempHTML = tempHTML + "<tr>";

                    tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + Convert.ToDateTime(curRow["Expiration"]).ToString("MMM-yy");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Call"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Put"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; font-weight:bold'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Total"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "</tr>";
                }

                tempHTML = tempHTML + "</table>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";
            }
            return tempHTML;

        }

        public string Risk_VAR(int Id_Portfolio, bool InternalOnly)
        {
            string tempHTML = "";

            string SQLString = "SELECT * FROM NESTDB.dbo.FCN_GET_Book_Summary(" + Id_Portfolio + " ) WHERE Strategy<>'Cash';";

            DataTable tablep = CargaDados.curConn.Return_DataTable(SQLString);

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>VAR per Book</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            
            tempHTML = tempHTML + "<table border=0 cellspacing=0>";
            tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=200>Book Name</td>" +
                                        "<td align=center width=60>Gross Exposure</td>" +
                                        "<td align=center width=60>VaR</td>" +
                                    "</tr>";

            foreach (DataRow curRow in tablep.Rows)
            {

                tempHTML = tempHTML + "<tr>";

                tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + curRow["Strategy"];
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Gross"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'; font-weight:bold'>";
                tempHTML = tempHTML + ifStrNull(curRow["VAR %"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";
            }

            tempHTML = tempHTML + "</table>";

            return tempHTML;

        }
        
        public string oldRisk_Sectors(int Id_Portfolio)
        {
            string tempHTML = "";
            string SQLString = "SELECT * " +
                                "FROM NESTDB.dbo.[FCN_Risk_Sector_Exposure](" + Id_Portfolio.ToString() + ") A " +
                                "LEFT JOIN  " +
                                "( " +
                                "    SELECT * FROM NESTDB.dbo.[FCN_Risk_Sector_Liquidity](" + Id_Portfolio.ToString() + ") " +
                                "    UNION " +
                                "    SELECT 'Total' AS Sector, SUM(grp_10) AS grp_10, SUM(grp_3_10) AS grp_3_10, SUM(grp_0_3) AS grp_0_3, SUM(grp_NULL) AS grp_NULL FROM NESTDB.dbo.[FCN_Risk_Sector_Liquidity](" + Id_Portfolio.ToString() + ") " +
                                ") B " +
                                "ON A.Sector=B.Sector";


            DataTable tablep = CargaDados.curConn.Return_DataTable(SQLString);

            string PortName = "";

            PortName = CargaDados.curConn.Execute_Query_String("Select Port_Name from dbo.Tb002_Portfolios where Id_Portfolio=" + Id_Portfolio.ToString());

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=17pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<table cellspacing=0>";
            tempHTML = tempHTML + "<tr>";
            tempHTML = tempHTML + "<td colspan=6 align=center style='border:2px solid black;background-color:FFCF00;color=black;font-family:Tahoma; font-size=12pt; font-weight:bold'>" + PortName + "</td>";
            tempHTML = tempHTML + "<td colspan=4 align=center style='border-right:2px solid black;border-top:2px solid black;border-bottom:2px solid black;background-color:FFCF00;color=black;font-family:Tahoma; font-size=12pt; font-weight:bold'>Liquidity Breakdown (Net)</td>";
            tempHTML = tempHTML + "</tr>";
            tempHTML = tempHTML + "<tr style='background-color: black; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=100>Strategy</td>" +
                                        "<td align=center width=100>Sector</td>" +
                                        "<td align=center width=60>Gross</td>" +
                                        "<td align=center width=60>Long</td>" +
                                        "<td align=center width=60>Short</td>" +
                                        "<td align=center width=60>Net</td>" +
                                        "<td align=center width=60>>10 MM</td>" +
                                        "<td align=center width=60>3-10 MM</td>" +
                                        "<td align=center width=60>0-3 MM</td>" +
                                        "<td align=center width=60>N/A</td>" +
                //"<td align=center width=60>Contrib</td>" +
                                    "</tr>";
            string prevStrategy = "";
            int rowCounter = 0;

            string topRow = "";
            string strbackcolor = "";

            foreach (DataRow curRow in tablep.Rows)
            {
                if (strbackcolor == "white" && curRow["Strategy"].ToString() == "Long-Short")
                {
                    strbackcolor = "FFFF9C";
                }
                else
                {
                    strbackcolor = "white";
                }
                tempHTML = tempHTML + "<tr>";
                //if (prevStrategy == "") { prevStrategy = curRow["Strategy"].ToString(); };

                if (prevStrategy != curRow["Strategy"].ToString())
                {
                    topRow = ";border-top:2px solid black";
                    tempHTML = tempHTML.Replace("@NoRows", rowCounter.ToString());
                    tempHTML = tempHTML + "<td @tempSpan style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=10pt" + topRow + ";vertical-align:top; border-left:2px solid black;' rowspan=@NoRows >";
                    tempHTML = tempHTML + curRow["Strategy"];
                    tempHTML = tempHTML + "</td>";
                    rowCounter = 0;
                }
                else
                {
                    topRow = "";
                }

                if (curRow["Sector"].ToString() == "")
                {
                    tempHTML = tempHTML.Replace("@tempSpan", " colspan=2 ");
                }
                else
                {
                    tempHTML = tempHTML.Replace("@tempSpan", "");
                    if (curRow["Sector"].ToString() == "Total" || curRow["Sector"].ToString() == "Total FI")
                    {
                        strbackcolor = "FFCF00";
                        tempHTML = tempHTML + "<td align=left style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt; border-left:2px solid black" + topRow + "'>";
                        tempHTML = tempHTML + curRow["Sector"];
                        tempHTML = tempHTML + "</td>";
                    }
                    else
                    {
                        tempHTML = tempHTML + "<td align=left style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt; border-left:2px solid black" + topRow + "'>";
                        tempHTML = tempHTML + curRow["Sector"];
                        tempHTML = tempHTML + "</td>";
                    }
                }

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "; border-left:2px solid black'>";
                tempHTML = tempHTML + ifStrNull(curRow["Gross"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["Long"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["Short"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "; border-left:2px solid black; border-right:2px solid black'>";
                tempHTML = tempHTML + ifStrNull(curRow["Net"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["grp_10"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["grp_3_10"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["grp_0_3"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "; border-right:2px solid black'>";
                tempHTML = tempHTML + ifStrNull(curRow["grp_NULL"]);
                tempHTML = tempHTML + "</td>";

                prevStrategy = curRow["Strategy"].ToString();

                tempHTML = tempHTML + "</tr>";
                rowCounter++;
            }

            tempHTML = tempHTML.Replace(">0,00%</td>", ">&nbsp;</td>");

            tempHTML = tempHTML.Replace("@NoRows", rowCounter.ToString());

            tempHTML = tempHTML + "<tr>";
            tempHTML = tempHTML + "<td style='font-family:Tahoma; font-size=8pt;border-top:2px solid black' colspan=10 ><div style='color=#FF9900;font-family:Tahoma; font-size=0pt;'>&nbsp;</div></td>";
            tempHTML = tempHTML + "</tr>";

            tempHTML = tempHTML + "</table>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";

            return tempHTML;
        
        }

        public string Risk_Sectors(int Id_Portfolio)
        {
            string tempHTML = "";
            string SQLString = "SELECT * " +
                                "FROM NESTDB.dbo.[FCN_Risk_Sector_Exposure](" + Id_Portfolio.ToString() + ") A " +
                                "LEFT JOIN  " +
                                "( " +
                                "    SELECT * FROM NESTDB.dbo.[FCN_Risk_Sector_Liquidity](" + Id_Portfolio.ToString() + ") " +
                                "    UNION " +
                                "    SELECT 'Total' AS Sector, SUM(grp_10) AS grp_10, SUM(grp_3_10) AS grp_3_10, SUM(grp_0_3) AS grp_0_3, SUM(grp_NULL) AS grp_NULL FROM NESTDB.dbo.[FCN_Risk_Sector_Liquidity](" + Id_Portfolio.ToString() + ") " +
                                ") B " +
                                "ON A.Sector=B.Sector";

            DataTable tablep = CargaDados.curConn.Return_DataTable(SQLString);

            tempHTML = tempHTML + "<table cellspacing=0>";
            tempHTML = tempHTML + "<tr>";
            tempHTML = tempHTML + "<td colspan=6 align=center style='background-color:#999999;color=white;font-family:Tahoma; font-size=12pt; font-weight:bold'>Exposure Breakdown</td>"; 
            tempHTML = tempHTML + "<td colspan=4 align=center style='background-color:#999999;color=white;font-family:Tahoma; font-size=12pt; font-weight:bold'>Liquidity Breakdown (Net)</td>";
            tempHTML = tempHTML + "</tr>";
            tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=100>Strategy</td>" +
                                        "<td align=center width=100>Sector</td>" +
                                        "<td align=center width=60>Gross</td>" +
                                        "<td align=center width=60>Long</td>" +
                                        "<td align=center width=60>Short</td>" +
                                        "<td align=center width=60>Net</td>" +
                                        "<td align=center width=60>>10 MM</td>" +
                                        "<td align=center width=60>3-10 MM</td>" +
                                        "<td align=center width=60>0-3 MM</td>" +
                                        "<td align=center width=60>N/A</td>" +
                //"<td align=center width=60>Contrib</td>" +
                                    "</tr>";
            string prevStrategy = "";
            int rowCounter = 0;

            string topRow = "";
            string strbackcolor = "";

            foreach (DataRow curRow in tablep.Rows)
            {
                if (strbackcolor == "white" && curRow["Strategy"].ToString() == "Long-Short")
                {
                    strbackcolor = "DDDDDD";
                }
                else
                {
                    strbackcolor = "white";
                }
                tempHTML = tempHTML + "<tr>";
                //if (prevStrategy == "") { prevStrategy = curRow["Strategy"].ToString(); };

                if (prevStrategy != curRow["Strategy"].ToString())
                {
                    topRow = ";border-top:2px solid #999999";
                    tempHTML = tempHTML.Replace("@NoRows", rowCounter.ToString());
                    tempHTML = tempHTML + "<td @tempSpan style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + ";vertical-align:top;' rowspan=@NoRows >";
                    tempHTML = tempHTML + curRow["Strategy"];
                    tempHTML = tempHTML + "</td>";
                    rowCounter = 0;
                }
                else
                {
                    topRow = "";
                }

                if (curRow["Sector"].ToString() == "")
                {
                    tempHTML = tempHTML.Replace("@tempSpan", " colspan=2 ");
                }
                else
                {
                    tempHTML = tempHTML.Replace("@tempSpan", "");
                    if (curRow["Sector"].ToString() == "Total" || curRow["Sector"].ToString() == "Total FI")
                    {
                        strbackcolor = "BBBBBB";
                        tempHTML = tempHTML + "<td align=left style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                        tempHTML = tempHTML + curRow["Sector"];
                        tempHTML = tempHTML + "</td>";
                    }
                    else
                    {
                        tempHTML = tempHTML + "<td align=left style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                        tempHTML = tempHTML + curRow["Sector"];
                        tempHTML = tempHTML + "</td>";
                    }
                }

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["Gross"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["Long"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["Short"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["Net"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='border-left:2px solid #DDDDDD; background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["grp_10"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["grp_3_10"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["grp_0_3"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='background-color:" + strbackcolor + ";font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["grp_NULL"]);
                tempHTML = tempHTML + "</td>";

                prevStrategy = curRow["Strategy"].ToString();

                tempHTML = tempHTML + "</tr>";
                rowCounter++;
            }

            tempHTML = tempHTML.Replace(">0,00%</td>", ">&nbsp;</td>");

            tempHTML = tempHTML.Replace("@NoRows", rowCounter.ToString());

            tempHTML = tempHTML + "<tr>";
            tempHTML = tempHTML + "<td style='font-family:Tahoma; font-size=8pt;border-top:2px solid black' colspan=10 ><div style='color=#FF9900;font-family:Tahoma; font-size=0pt;'>&nbsp;</div></td>";
            tempHTML = tempHTML + "</tr>";

            tempHTML = tempHTML + "</table>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";

            return tempHTML;

        }

        public string Risk_Liquidity(int Id_Portfolio, bool PublicData)
        {
            // ======================= Time to reduce Gross =======================

            string tempHTML = "";
            string SQLString = "";

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Days to Reduce Gross Exposure¹</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

            tempHTML = tempHTML + "<table border=0 cellspacing=0>";

            tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=120>Reduce Gross to¹</td>" +
                                        "<td align=center width=60>Days</td>" +
                                    "</tr>";

            float MaxGross = 1.5F;
            if (Id_Portfolio == 4) { MaxGross = (float)2.2; };

            SQLString = "SELECT * FROM NESTDB.[dbo].[FCN_Risk_Liquidity_Gross](" + Id_Portfolio.ToString() + ", " + MaxGross.ToString().Replace(',', '.') + ") ORDER BY COALESCE(Days,1000)";

            DataTable tablep = CargaDados.curConn.Return_DataTable(SQLString);

            foreach (DataRow curRow in tablep.Rows)
            {
                tempHTML = tempHTML + "<tr>";

                tempHTML = tempHTML + "<td align=center valign=top style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Gross"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                if (curRow["Days"].ToString() == "-1")
                {
                    tempHTML = tempHTML + "Current<BR>&nbsp;";
                }
                else
                {
                    tempHTML = tempHTML + ifStrNull(curRow["Days"], "0.0");
                }
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";
            }

            tempHTML = tempHTML + "</table>";

            if (!PublicData)
            {
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";

                // ======================= Liquidity Profile =======================

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Liquidity Profile²</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

                tempHTML = tempHTML + "<table border=0 cellspacing=0>";

                tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                            "<td align=left width=120>Time in days</td>" +
                                            "<td align=center width=60>Percent</td>" +
                                        "</tr>";

                SQLString = "SELECT SUM(PercPort) FROM NESTDB.[dbo].[FCN_Risk_Liquidity_Profile](" + Id_Portfolio.ToString() + ")";
                string TempTotal = CargaDados.curConn.Execute_Query_String(SQLString);

                double TotalPortfolio = 1;

                if (TempTotal != "")
                {
                    TotalPortfolio = Convert.ToDouble(TempTotal);
                }

                SQLString = "SELECT * FROM NESTDB.[dbo].[FCN_Risk_Liquidity_Profile](" + Id_Portfolio.ToString() + ") WHERE DayBracket<>'zFUT'";

                tablep.Clear();
                tablep = CargaDados.curConn.Return_DataTable(SQLString);

                double CurPercent = 0;
                double CumPercent = 0;

                foreach (DataRow curRow in tablep.Rows)
                {
                    tempHTML = tempHTML + "<tr>";

                    tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + curRow["DayBracket"];
                    tempHTML = tempHTML + "</td>";

                    CurPercent = Convert.ToSingle((curRow["PercPort"]));
                    CumPercent = CumPercent + CurPercent;

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    if (curRow["DayBracket"].ToString() == "NA")
                    {
                        tempHTML = tempHTML + (CurPercent / TotalPortfolio).ToString("0.00%");
                    }
                    else
                    {
                        tempHTML = tempHTML + (CumPercent / TotalPortfolio).ToString("0.00%");
                    }
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "</tr>";
                }

                tempHTML = tempHTML + "</table>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";

                // ======================= TOP 10 by Liquidity  =======================

                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Less Liquid Positions</div>";
                tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

                tempHTML = tempHTML + "<table border=0 cellspacing=0>";

                tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                            "<td align=center width=60>Ticker</td>" +
                                            "<td align=center width=60>Days To Liquidate</td>" +
                                            "<td align=center width=60>Gross</td>" +
                                            "<td align=center width=60>Net</td>" +
                                        "</tr>";

                SQLString = "SELECT TOP 10 * FROM NESTDB.[dbo].[FCN_Risk_Liquidity_Names](" + Id_Portfolio.ToString() + ") WHERE PosType='EQ'";

                tablep.Clear();
                tablep = CargaDados.curConn.Return_DataTable(SQLString);

                float PosDays = 0;

                foreach (DataRow curRow in tablep.Rows)
                {
                    tempHTML = tempHTML + "<tr>";

                    if (!DBNull.Value.Equals(curRow["Days"]))
                    {
                        PosDays = Convert.ToSingle((curRow["Days"]));
                    }

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + curRow["Ticker"];
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + PosDays.ToString("0.00");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Gross"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";

                    tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; font-weight:bold'>";
                    tempHTML = tempHTML + Convert.ToSingle((curRow["Net"])).ToString("0.00%");
                    tempHTML = tempHTML + "</td>";


                    tempHTML = tempHTML + "</tr>";
                }
                tempHTML = tempHTML + "</table>";
            }

            return tempHTML;

        }

        public string Contact_Sub_Summary(int Id_Contact)
        {
            DataTable tablep = new DataTable();
            string tempHTML = "";

            string SQLString = "SELECT * FROM (SELECT 'MH' AS Fund, * FROM dbo.FCN_COM_ContactSubSummary(" + Id_Contact.ToString() + ", 2, 0) " +
                            "UNION SELECT 'MH 30' AS Fund, * FROM dbo.FCN_COM_ContactSubSummary(" + Id_Contact.ToString() + ", 3, 0) " +
                            "UNION SELECT 'Nest FIA' AS Fund, * FROM dbo.FCN_COM_ContactSubSummary(" + Id_Contact.ToString() + ", 12, 0)" +
                            "UNION SELECT 'MH 1' AS Fund, * FROM dbo.FCN_COM_ContactSubSummary(" + Id_Contact.ToString() + ", 15, 0)" +
                            ") A WHERE Quantity <> 0 ORDER BY Fund, curDate Desc";

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            tempHTML = tempHTML + "<table cellspacing=0>";
            tempHTML = tempHTML + "<tr>";
            tempHTML = tempHTML + "<td colspan=8 style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Current Positions</td>";
            tempHTML = tempHTML + "</tr>";
            tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=100>Date</td>" +
                                        "<td align=center width=100>Quantity</td>" +
                                        "<td align=center width=60>Purchase NAV</td>" +
                                        "<td align=center width=100>Purchase Value</td>" +
                                        "<td align=center width=60>Current NAV</td>" +
                                        "<td align=center width=100>Current Value</td>" +
                                        "<td align=center width=60>Months</td>" +
                                        "<td align=center width=60>Performance</td>" +
                                        "<td align=center width=60>Benchmark</td>" +
                                        "<td align=center width=60>Excess</td>" +
                                    "</tr>";
            string prevFund = "";
            int rowCounter = 0;
            string topRow = "";

            double FundSubCost = 0;
            double FundSubValue = 0;

            double TotalSubCost = 0;
            double TotalSubValue = 0;
            double FundSubQuantity = 0;

            foreach (DataRow curRow in tablep.Rows)
            {
                if (prevFund != curRow["Fund"].ToString())
                {
                    if (FundSubCost > 0 || FundSubValue > 0)
                    {
                        tempHTML = tempHTML + "</tr>";
                        tempHTML = tempHTML + "<tr style='background-color=#C0C0C0'>";
                        tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>Total</td>";
                        tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                        tempHTML = tempHTML + FundSubQuantity.ToString("#,##0.00");
                        tempHTML = tempHTML + "</td><td>&nbsp;</td>";
                        tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                        tempHTML = tempHTML + FundSubCost.ToString("#,##0.00");
                        tempHTML = tempHTML + "</td>";
                        tempHTML = tempHTML + "<td>&nbsp;</td>";
                        tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                        tempHTML = tempHTML + FundSubValue.ToString("#,##0.00");
                        tempHTML = tempHTML + "</td>";
                        tempHTML = tempHTML + "<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";
                    }
                    tempHTML = tempHTML + "</tr>";
                    tempHTML = tempHTML + "<tr>";
                    tempHTML = tempHTML + "<td colspan=8 align=left style='color=#FF9900;font-family:Tahoma; font-size=8pt; font-weight:bold'>";
                    tempHTML = tempHTML + curRow["Fund"].ToString();
                    tempHTML = tempHTML + "</td>";
                    tempHTML = tempHTML + "<tr>";
                    FundSubCost = 0;
                    FundSubValue = 0;
                    FundSubQuantity = 0;
                }
                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + Convert.ToDateTime(curRow["curDate"]).ToString("dd-MMM-yy");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["Quantity"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["curNAV"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["origValue"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["Last_NAV"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["curValue"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(Convert.ToDouble(curRow["noDays"])/30.42, "#,##0.0");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["PerfFund"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["PerfBench"]);
                tempHTML = tempHTML + "</td>";

                double perFund = 0;
                perFund = Convert.ToDouble(curRow["PerfFund"]);

                double perBench = 0;
                perBench = Convert.ToDouble(curRow["PerfBench"]);

                string fontColor = "black";
                if (perFund - perBench < 0) { fontColor = "red"; }

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; color=" + fontColor + "" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(perFund - perBench);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";

                FundSubCost += Convert.ToDouble(curRow["origValue"]);
                FundSubValue += Convert.ToDouble(curRow["curValue"]);
                FundSubQuantity += Convert.ToDouble(curRow["Quantity"]);

                TotalSubCost += Convert.ToDouble(curRow["origValue"]);
                TotalSubValue += Convert.ToDouble(curRow["curValue"]);

                prevFund = curRow["Fund"].ToString();
                rowCounter++;
            }

            tempHTML = tempHTML.Replace(">0,00%</td>", ">&nbsp;</td>");

            if (FundSubCost > 0 || FundSubValue > 0)
            {
                tempHTML = tempHTML + "</tr>";
                tempHTML = tempHTML + "<tr style='background-color=#C0C0C0'>";
                tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>Total</td>";
                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + FundSubQuantity.ToString("#,##0.00");
                tempHTML = tempHTML + "</td><td>&nbsp;</td>";
                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + FundSubCost.ToString("#,##0.00");
                tempHTML = tempHTML + "</td>";
                tempHTML = tempHTML + "<td>&nbsp;</td>";
                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + FundSubValue.ToString("#,##0.00");
                tempHTML = tempHTML + "</td>";
                tempHTML = tempHTML + "<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";
            }

            tempHTML = tempHTML + "<tr>";
            tempHTML = tempHTML + "<td style='font-family:Tahoma; font-size=8pt;border-top:2px solid black' colspan=10 ><div style='color=#FF9900;font-family:Tahoma; font-size=0pt;'>&nbsp;</div></td>";
            tempHTML = tempHTML + "</tr>";

            if (TotalSubCost > 0 || TotalSubValue > 0)
            {
                tempHTML = tempHTML + "<tr>";
                tempHTML = tempHTML + "<tr>";
                tempHTML = tempHTML + "<td align=left style='font-weight:bold; font-family:Tahoma; font-size=8pt'>Total Value</td><td>&nbsp;</td><td>&nbsp;</td>";
                tempHTML = tempHTML + "<td align=right style='font-weight:bold; font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + TotalSubCost.ToString("#,##0.00");
                tempHTML = tempHTML + "</td>";
                tempHTML = tempHTML + "<td>&nbsp;</td>";
                tempHTML = tempHTML + "<td align=right style='font-weight:bold; font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + TotalSubValue.ToString("#,##0.00");
                tempHTML = tempHTML + "</td>";
                tempHTML = tempHTML + "<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";
                tempHTML = tempHTML + "</tr>";
            }

            tempHTML = tempHTML + "</table>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";

            return tempHTML;

        }

        public string Contact_Red_Summary(int Id_Contact, bool IncludeIR)
        {
            string tempHTML = "";

            string SQLString = "SELECT *, alocQuantity*redNAV AS redAmount FROM (SELECT 'MH' AS Fund, * FROM dbo.FCN_COM_ContactRedSummary(" + Id_Contact.ToString() + ", 2, 0) " +
                            "UNION SELECT 'MH 30' AS Fund, * FROM dbo.FCN_COM_ContactRedSummary(" + Id_Contact.ToString() + ", 3, 0) " +
                            "UNION SELECT 'Nest FIA' AS Fund, * FROM dbo.FCN_COM_ContactRedSummary(" + Id_Contact.ToString() + ", 12, 0)" +
                            "UNION SELECT 'MH 1' AS Fund, * FROM dbo.FCN_COM_ContactRedSummary(" + Id_Contact.ToString() + ", 15, 0)" +
                            ") A ORDER BY Fund, redDate Desc";

            //if (!IncludeIR) { SQLString = SQLString.Replace("ORDER BY", "WHERE Transaction_Type<> 33 ORDER BY"); };

            DataTable tablep = CargaDados.curConn.Return_DataTable(SQLString);

            tempHTML = tempHTML + "<table cellspacing=0>";

            tempHTML = tempHTML + "<tr>";
            tempHTML = tempHTML + "<td colspan=8 style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Redemptions</td>";
            tempHTML = tempHTML + "</tr>";
            tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=60>Purchase Date</td>" +
                                        "<td align=left width=60>Redemption Date</td>" +
                                        "<td align=left width=20>Type</td>" +
                                        "<td align=center width=100>Quantity</td>" +
                                        "<td align=center width=60>Purchase NAV</td>" +
                                        "<td align=center width=100>Purchase Value</td>" +
                                        "<td align=center width=60>Redemption NAV</td>" +
                                        "<td align=center width=100>Redemption Value</td>" +
                                        "<td align=center width=60>Months</td>" +
                                        "<td align=center width=60>Performance</td>" +
                                        "<td align=center width=60>Benchmark</td>" +
                                        "<td align=center width=60>Excess</td>" +
                                    "</tr>";
            string prevFund = "";
            int rowCounter = 0;
            string topRow = "";

            double FundSubCost = 0;
            double FundSubValue = 0;

            double TotalSubCost = 0;
            double TotalSubValue = 0;

            foreach (DataRow curRow in tablep.Rows)
            {
                if (prevFund != curRow["Fund"].ToString())
                {
                    if (FundSubCost > 0 || FundSubValue > 0)
                    {
                        tempHTML = tempHTML + "</tr>";
                        tempHTML = tempHTML + "<tr style='background-color=#C0C0C0'>";
                        tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>Total</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";
                        tempHTML = tempHTML + "<td>&nbsp;</td>";
                        tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                        tempHTML = tempHTML + FundSubCost.ToString("#,##0.00");
                        tempHTML = tempHTML + "</td>";
                        tempHTML = tempHTML + "<td>&nbsp;</td>";
                        tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                        tempHTML = tempHTML + FundSubValue.ToString("#,##0.00");
                        tempHTML = tempHTML + "</td>";
                        tempHTML = tempHTML + "<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";
                    }
                    tempHTML = tempHTML + "</tr>";
                    tempHTML = tempHTML + "<tr>";
                    tempHTML = tempHTML + "<td colspan=8 align=left style='color=#FF9900;font-family:Tahoma; font-size=8pt; font-weight:bold'>";
                    tempHTML = tempHTML + curRow["Fund"].ToString();
                    tempHTML = tempHTML + "</td>";
                    tempHTML = tempHTML + "<tr>";
                    FundSubCost = 0;
                    FundSubValue = 0;
                }

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + Convert.ToDateTime(curRow["subDate"]).ToString("dd-MMM-yy");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + Convert.ToDateTime(curRow["redDate"]).ToString("dd-MMM-yy");
                tempHTML = tempHTML + "</td>";

                string IdTransType = CargaDados.curConn.Execute_Query_String("Select Code FROM NESTDB.dbo.Tb701_Transaction_Types WHERE Id_Trans_Type=" + curRow["Transaction_Type"].ToString());

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + IdTransType;
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["origQuantity"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["subNAV"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(Convert.ToDouble(curRow["origQuantity"]) * Convert.ToDouble(curRow["subNAV"]), "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["redNAV"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["redAmount"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(Convert.ToDouble(curRow["noDays"]) / 30.42, "#,##0.0");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["PerfFund"]);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(curRow["PerfBench"]);
                tempHTML = tempHTML + "</td>";

                double perFund = 0;
                perFund = Convert.ToDouble(curRow["PerfFund"]);

                double perBench = 0;
                perBench = Convert.ToDouble(curRow["PerfBench"]);

                string fontColor = "black";
                if (perFund - perBench < 0) { fontColor = "red"; }

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt; color=" + fontColor + "" + topRow + "'>";
                tempHTML = tempHTML + ifStrNull(perFund - perBench);
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";

                FundSubCost += Convert.ToDouble(curRow["origQuantity"]) * Convert.ToDouble(curRow["subNAV"]);
                FundSubValue += Convert.ToDouble(curRow["redAmount"]);

                TotalSubCost += Convert.ToDouble(curRow["origQuantity"]) * Convert.ToDouble(curRow["subNAV"]); ;
                TotalSubValue += Convert.ToDouble(curRow["redAmount"]);

                prevFund = curRow["Fund"].ToString();
                rowCounter++;
            }

            tempHTML = tempHTML.Replace(">0,00%</td>", ">&nbsp;</td>");

            if (FundSubCost > 0 || FundSubValue > 0)
            {
                tempHTML = tempHTML + "<tr>";
                tempHTML = tempHTML + "<tr style='background-color=#C0C0C0'>";
                tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>Total</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";
                tempHTML = tempHTML + "<td>&nbsp;</td>";
                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + FundSubCost.ToString("#,##0.00");
                tempHTML = tempHTML + "</td>";
                tempHTML = tempHTML + "<td>&nbsp;</td>";
                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + FundSubValue.ToString("#,##0.00");
                tempHTML = tempHTML + "</td>";
                tempHTML = tempHTML + "<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";
                tempHTML = tempHTML + "</tr>";
            }

            tempHTML = tempHTML + "<tr>";
            tempHTML = tempHTML + "<td style='font-family:Tahoma; font-size=8pt;border-top:2px solid black' colspan=12 ><div style='color=#FF9900;font-family:Tahoma; font-size=0pt;'>&nbsp;</div></td>";
            tempHTML = tempHTML + "</tr>";

            if (TotalSubCost > 0 || TotalSubValue > 0)
            {
                tempHTML = tempHTML + "<tr>";
                tempHTML = tempHTML + "<tr>";
                tempHTML = tempHTML + "<td align=left style='font-weight:bold; font-family:Tahoma; font-size=8pt'>Total Redeemed</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";
                tempHTML = tempHTML + "<td>&nbsp;</td>";
                tempHTML = tempHTML + "<td align=right style='font-weight:bold; font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + TotalSubCost.ToString("#,##0.00");
                tempHTML = tempHTML + "</td>";
                tempHTML = tempHTML + "<td>&nbsp;</td>";
                tempHTML = tempHTML + "<td align=right style='font-weight:bold; font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + TotalSubValue.ToString("#,##0.00");
                tempHTML = tempHTML + "</td>";
                tempHTML = tempHTML + "<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>";
                tempHTML = tempHTML + "</tr>";
            }

            tempHTML = tempHTML + "</table>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";

            return tempHTML;

        }
        
        public string Risk_Scenarios()
        { 

            DataTable tablep = new DataTable();
            string tempHTML = "";

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Stress Scenarios (BM&F)</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";

            string SQLString = "SELECT Id_Portfolio " +
                                " , SUM(CASE WHEN Id_Scenario = 1 THEN Scenario_Loss ELSE 0 END) AS Scenario_Loss1 " +
                                " , SUM(CASE WHEN Id_Scenario = 2 THEN Scenario_Loss ELSE 0 END) AS Scenario_Loss2 " +
                                " , SUM(CASE WHEN Id_Scenario = 3 THEN Scenario_Loss ELSE 0 END) AS Scenario_Loss3 " +
                                " , SUM(CASE WHEN Id_Scenario = 1 THEN [Loss %] ELSE 0 END) AS Scenario_Percent1 " +
                                " , SUM(CASE WHEN Id_Scenario = 2 THEN [Loss %] ELSE 0 END) AS Scenario_Percent2 " +
                                " , SUM(CASE WHEN Id_Scenario = 3 THEN [Loss %] ELSE 0 END) AS Scenario_Percent3 " +
                                " FROM [dbo].[FCN_Risk_Port_Scenarios]() " +
                                " GROUP BY Id_Portfolio";

            tablep.Clear();
            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=10pt; font-weight:bold'>Portfolio Totals</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";

            tempHTML = tempHTML + "<table cellspacing=0>";
            tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=center width=100>&nbsp;</td>" +
                                        "<td align=center colspan=3>Gain/Loss</td>" +
                                        "<td align=center colspan=3>Gain/Loss %</td>" +
                                    "</tr>" + 
                                    "<tr style='background-color: #999999; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=100>Portfolio</td>" +
                                        "<td align=center width=90>Pess 1</td>" +
                                        "<td align=center width=90>Pess 2</td>" +
                                        "<td align=center width=90>Optim</td>" +
                                        "<td align=center width=50>Pess 1</td>" +
                                        "<td align=center width=50>Pess 2</td>" +
                                        "<td align=center width=50>Optim</td>" +
                                    "</tr>";

            foreach (DataRow curRow in tablep.Rows)
            {
                tempHTML = tempHTML + "<tr>";

                tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                string PortName = CargaDados.curConn.Execute_Query_String("Select Port_Name from  dbo.Tb002_Portfolios where Id_Portfolio=" + curRow["Id_Portfolio"].ToString());
                tempHTML = tempHTML + PortName;
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + Convert.ToSingle((curRow["Scenario_Loss1"])).ToString("#,##0");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + Convert.ToSingle((curRow["Scenario_Loss2"])).ToString("#,##0");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + Convert.ToSingle((curRow["Scenario_Loss3"])).ToString("#,##0");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + Convert.ToSingle((curRow["Scenario_Percent1"])).ToString("0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + Convert.ToSingle((curRow["Scenario_Percent2"])).ToString("0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + Convert.ToSingle((curRow["Scenario_Percent3"])).ToString("0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";
            }

            tempHTML = tempHTML + "</table>";

            // Portfolio Breakdowns

            SQLString = "SELECT * FROM NESTDB.[dbo].[FCN_Risk_Port_Scenarios_Breakdown]() ";

            DataTable tablep2 = CargaDados.curConn.Return_DataTable(SQLString);

            tempHTML = tempHTML + "<BR><div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=10pt; font-weight:bold'>Portfolio Breakdown</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";

            string prevPort = "-1";

            foreach (DataRow curRow in tablep2.Rows)
            {

                if (prevPort != curRow["Id_Portfolio"].ToString())
                {
                    string PortName = CargaDados.curConn.Execute_Query_String("Select Port_Name from  dbo.Tb002_Portfolios where Id_Portfolio=" + curRow["Id_Portfolio"].ToString());
                    tempHTML = tempHTML + "</table><BR><table cellspacing=0>";

                    tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                            "<td align=Left colspan=2>" + PortName + "</td>" +
                            "<td align=center colspan=5>Gain/Loss %</td>" +
                        "</tr>" +
                        "<tr style='background-color: #999999; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                            "<td align=left width=100>Scenario</td>" +
                            "<td align=center width=90>Total</td>" +
                            "<td align=center width=90>Equities BRL</td>" +
                            "<td align=center width=90>Equities nBRL</td>" +
                            "<td align=center width=90>Currency</td>" +
                            "<td align=center width=90>FI BRL</td>" +
                            "<td align=center width=90>FI nBRL</td>" +
                        "</tr>";
                }

                tempHTML = tempHTML + "</tr>";

                string Scenario = "";

                switch (curRow["Id_Scenario"].ToString())
                {
                    case "1": Scenario = "Pess 1"; break;
                    case "2": Scenario = "Pess 2"; break;
                    case "3": Scenario = "Optim"; break;
                    default: break;
                }

                tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + Scenario;
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Total Loss %"], "0.00%;-0.00%;-");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Loss_Equities_BRL %"], "0.00%;-0.00%;-");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Loss_Equities_nonBR %"], "0.00%;-0.00%;-");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Loss_Currency %"], "0.00%;-0.00%;-");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Loss_FI_BRL %"], "0.00%;-0.00%;-");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Loss_FI_nonBRL %"], "0.00%;-0.00%;-");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";

                prevPort = curRow["Id_Portfolio"].ToString();

            }

            tempHTML = tempHTML + "</table>";

            return tempHTML;

        }

        public string Risk_MonteCarlo()
        {
            // Portfolio Breakdowns

            string SQLString = "SELECT * FROM NESTDB.[dbo].[FCN_Risk_Port_Monte_Carlo_Breakdown]() ";

            DataTable tablep2 = CargaDados.curConn.Return_DataTable(SQLString);

            string tempHTML = "";
            
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Monte Carlo</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";


            tempHTML = tempHTML + "<table cellspacing=0>";

            tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                    "<td align=Left colspan=2>&nbsp;</td>" +
                    "<td align=center colspan=3>Loss %</td>" +
                "</tr>" +
                "<tr style='background-color: #999999; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                    "<td align=left width=100>Scenario</td>" +
                    "<td align=center width=90>Total</td>" +
                    "<td align=center width=90>Equities</td>" +
                    "<td align=center width=90>Currency</td>" +
                    "<td align=center width=90>FI</td>" +
                "</tr>";

            tempHTML = tempHTML + "</tr>";

            foreach (DataRow curRow in tablep2.Rows)
            {

                string PortName = CargaDados.curConn.Execute_Query_String("Select Port_Name from  dbo.Tb002_Portfolios where Id_Portfolio=" + curRow["Id_Portfolio"].ToString());

                tempHTML = tempHTML + "<td align=left style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + PortName;
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["VAR %"], "0.00%;-0.00%;-");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["VAR_Equities_BRL %"], "0.00%;-0.00%;-");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["VAR_Currency %"], "0.00%;-0.00%;-");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["VAR_FI %"], "0.00%;-0.00%;-");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";

            }

            tempHTML = tempHTML + "</table>";

            return tempHTML;

        }

        public string Cont_Payouts(int Id_Portfolio, int Id_Officer)
        {

            string tempHTML = "";

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Payout Summary</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";

            string SQLString = "SELECT * FROM NESTDB.dbo.FCN_COM_PayoutSummary(" + Id_Portfolio + "," + Id_Officer + ") WHERE RefMonth>'2006-01-01'";

            DataTable tablep = CargaDados.curConn.Return_DataTable(SQLString);

            string PortName = CargaDados.curConn.Execute_Query_String("Select Port_Name from  dbo.Tb002_Portfolios where Id_Portfolio=" + Id_Portfolio.ToString());
            
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=10pt; font-weight:bold'>" + PortName + "</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";

            tempHTML = tempHTML + "<table cellspacing=0>";
            tempHTML = tempHTML + "<tr style='background-color: #999999; color=white; font-size=10pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=100>Date</td>" +
                                        "<td align=center width=90>Payout</td>" +
                                        "<td align=center width=90>Months 1-6</td>" +
                                        "<td align=center width=90>Months 7-12</td>" +
                                        "<td align=center width=90>Months 13-18</td>" +
                                        "<td align=center width=90>Total Payout</td>" +
                                        "<td align=center width=90>Penalty</td>" +
                                        "<td align=center width=90>Total Penalty</td>" +
                                        "<td align=center width=90>Net Payout</td>" +
                                    "</tr>";

            foreach (DataRow curRow in tablep.Rows)
            {
                tempHTML = tempHTML + "<tr>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + DateTime.Parse(curRow["RefMonth"].ToString()).ToString("MMM-yy");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Payout"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["M1_6"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["M7_12"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["M13_18"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["TotalPayout"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Penalty"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["TotalPenalty"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["NetPayout"], "#,##0.00");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";
            }

            tempHTML = tempHTML + "</table>";

            
            tempHTML = tempHTML + "</table>";

            return tempHTML;

        }
        
        private string ifStrNull(object strValue)
        {
            if (DBNull.Value.Equals(strValue))
            {
                return "&nbsp;";
            }
            else if (String.IsNullOrEmpty(Convert.ToString(strValue)))
            {
                return "&nbsp;";
            }
            else
            {
                return Convert.ToSingle(strValue.ToString()).ToString("0.00%");
            }
        }

        private string ifStrNull(object strValue, string strFormat)
        {
            if (DBNull.Value.Equals(strValue))
            {
                return "&nbsp;";
            }
            else if (String.IsNullOrEmpty(Convert.ToString(strValue)))
            {
                return "&nbsp;";
            }
            else
            {
                return Convert.ToSingle(strValue.ToString()).ToString(strFormat);
            }
        }

    }
}
