using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

using NestDLL;


namespace LiveBook
{
    public partial class frmRiskFactors : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmRiskFactors()
        {
            InitializeComponent();
        }

        public void Set_Portfolio_Values(int Id_Portfolio)
        {
            cmbPortfolio.SelectedValue = Id_Portfolio;
        }

        private void frmRiskOptions_Load(object sender, EventArgs e)
        {
            
            UpdateRisk();
            tmrUpdate.Interval = 60000;
            tmrUpdate.Start();
        }

        private void UpdateRisk()
        {
            webRiskOptions.Navigate("about:blank");

            DataTable tablep = new DataTable();
            string tempHTML = "";

            tempHTML = tempHTML + "<html><body>";

            string PortName = "";
            string SQLString = "";

            PortName = curConn.Execute_Query_String("Select Port_Name from  dbo.Tb002_Portfolios where Id_Portfolio=" + cmbPortfolio.SelectedValue.ToString());

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=14pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=14pt; font-weight:bold'>" + PortName + "</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=3pt;'>&nbsp;</div>";

            // ======================= MARKET CAP =======================

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Market Cap Breakdown</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

            tempHTML = tempHTML + "<table border=0 cellspacing=0>";

            tempHTML = tempHTML + "<tr style='background-color: #7B9AE7; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=120>Cap Bracket</td>" +
                                        "<td align=center width=60>Gross</td>" +
                                        "<td align=center width=60>Long</td>" +
                                        "<td align=center width=60>Short</td>" +
                                        "<td align=center width=60>Net</td>" +
                                    "</tr>";

            SQLString = "SELECT * FROM NESTDB.[dbo].[FCN_Risk_MktCap_Gross](" + cmbPortfolio.SelectedValue.ToString() + ")";

            tablep.Clear();
            tablep = curConn.Return_DataTable(SQLString);

            int grpCount = 0;
            string prevGroup = "0";

            foreach (DataRow curRow in tablep.Rows)
            {
                tempHTML = tempHTML + "<tr>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                if (grpCount == 0)
                {
                    tempHTML = tempHTML + ">" + ifStrNull(curRow["CapGroup"], "0");
                }
                else 
                {
                    if (DBNull.Value.Equals(curRow["CapGroup"]))
                    {
                        tempHTML = tempHTML + "NA";
                    }
                    else
                    {
                        tempHTML = tempHTML + ifStrNull(curRow["CapGroup"], "0") + "-" + prevGroup;
                    }
                }
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Gross"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Long"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Short"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Net"], "0.00%");
                tempHTML = tempHTML + "</td>";
                
                tempHTML = tempHTML + "</tr>";

                prevGroup = ifStrNull(curRow["CapGroup"], "0");

                grpCount++;
            }

            tempHTML = tempHTML + "</table>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";


            // ======================= PE RATIO =======================

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>PE Ratio</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

            tempHTML = tempHTML + "<table border=0 cellspacing=0>";

            tempHTML = tempHTML + "<tr style='background-color: #7B9AE7; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=120>PE Bracket</td>" +
                                        "<td align=center width=60>Gross</td>" +
                                        "<td align=center width=60>Long</td>" +
                                        "<td align=center width=60>Short</td>" +
                                        "<td align=center width=60>Net</td>" +
                                    "</tr>";

            SQLString = "SELECT * FROM NESTDB.[dbo].[FCN_Risk_PE_Gross](" + cmbPortfolio.SelectedValue.ToString() + ")";

            tablep.Clear();
            tablep = curConn.Return_DataTable(SQLString);

            grpCount = 0;
            prevGroup = "0";

            foreach (DataRow curRow in tablep.Rows)
            {
                tempHTML = tempHTML + "<tr>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                if (grpCount == 0)
                {
                    tempHTML = tempHTML + ">" + ifStrNull(curRow["PEGroup"], "0");
                }
                else
                {
                    if (DBNull.Value.Equals(curRow["PEGroup"]))
                    {
                        tempHTML = tempHTML + "NA";
                    }
                    else
                    {
                        tempHTML = tempHTML + ifStrNull(curRow["PEGroup"], "0") + "-" + prevGroup;
                    }
                }
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Gross"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Long"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Short"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Net"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";

                prevGroup = ifStrNull(curRow["PEGroup"], "0");

                grpCount++;
            }

            tempHTML = tempHTML + "</table>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";



            // ======================= P/B RATIO =======================

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=12pt; font-weight:bold'>Price/Book Ratio</div>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=2pt;'>&nbsp;</div>";

            tempHTML = tempHTML + "<table border=0 cellspacing=0>";

            tempHTML = tempHTML + "<tr style='background-color: #7B9AE7; color=white; font-size=8pt; font-family:Tahoma; font-weight:bold'>" +
                                        "<td align=left width=120>Price/Book Bracket</td>" +
                                        "<td align=center width=60>Gross</td>" +
                                        "<td align=center width=60>Long</td>" +
                                        "<td align=center width=60>Short</td>" +
                                        "<td align=center width=60>Net</td>" +
                                    "</tr>";

            SQLString = "SELECT * FROM NESTDB.[dbo].[FCN_Risk_PB_Gross](" + cmbPortfolio.SelectedValue.ToString() + ")";

            tablep.Clear();
            tablep = curConn.Return_DataTable(SQLString);

            grpCount = 0;
            prevGroup = "0";

            foreach (DataRow curRow in tablep.Rows)
            {
                tempHTML = tempHTML + "<tr>";

                tempHTML = tempHTML + "<td align=center style='font-family:Tahoma; font-size=8pt'>";
                if (grpCount == 0)
                {
                    tempHTML = tempHTML + ">" + ifStrNull(curRow["PBGroup"], "0");
                }
                else
                {
                    if (DBNull.Value.Equals(curRow["PBGroup"]))
                    {
                        tempHTML = tempHTML + "NA";
                    }
                    else
                    {
                        tempHTML = tempHTML + ifStrNull(curRow["PBGroup"], "0") + "-" + prevGroup;
                    }
                }
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Gross"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Long"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Short"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "<td align=right style='font-family:Tahoma; font-size=8pt'>";
                tempHTML = tempHTML + ifStrNull(curRow["Net"], "0.00%");
                tempHTML = tempHTML + "</td>";

                tempHTML = tempHTML + "</tr>";

                prevGroup = ifStrNull(curRow["PBGroup"], "0");

                grpCount++;
            }

            tempHTML = tempHTML + "</table>";
            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=6pt;'>&nbsp;</div>";

            tempHTML = tempHTML + "<div style='color=#FF9900;font-family:Tahoma; font-size=4pt;'>&nbsp;</div>";
            tempHTML = tempHTML + "<div style='font-family:Tahoma; font-size:8pt;'>Updated at: " + DateTime.Now.ToString("dd-MMM-yy HH:mm:ss") + "</div>";
            tempHTML = tempHTML + "</body></html>";

            webRiskOptions.Document.Write(tempHTML);
            webRiskOptions.Refresh();
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

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            UpdateRisk();
        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            UpdateRisk();
        }

        private void cmbPortfolio_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRisk();
        }
    }
}