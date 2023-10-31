using System;
using System.Collections.Generic;
using System.Text;
using NestDLL;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using System.Windows.Forms;
using System.Drawing;
using NestDLL;
using System.IO;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using System.Threading;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;


namespace SGN.Validacao
{
    public class Valida
    {
        public VBCodeProvider VB = new VBCodeProvider();
        CarregaDados CargaDados = new CarregaDados();
                
        public void SetColumnStyle(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid, int Type_Grid)
        {
            SetColumnStyle(Nome_Grid, Type_Grid,"");
        }

        public void SetColumnStyle(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid, int Type_Grid, string Coluna)
        {
            int Id_Grid;
            string nome;
            int largura;
            Boolean visivel;
            int Indice;
            int indice_grupo;
            //Id_Grid = 4;
            string Caption;
            int c = 0;
            try
            {

                foreach (DevExpress.XtraGrid.Columns.GridColumn col in Nome_Grid.Columns)
                {
                    Nome_Grid.Columns[c].Caption = col.ToString() ;//.Replace(' ','\n');
                    c++;
                }

                
                string SQLString = "Select Id_Grid from Tb110_Nome_Grids where Nome_Grid= '" + Nome_Grid.Name.ToString() + "'";
                Id_Grid = Convert.ToInt32(CargaDados.curConn.Execute_Query_String(SQLString));
                SQLString = "Select * from Tb109_Caractisticas_Colunas where Id_Grid =" + Id_Grid + " and Id_User =" + NestDLL.NUserControl.Instance.User_Id + " and Versao = 2  order by indice_grupo asc,Indice ";

                Nome_Grid.BeginSort();

                DataTable curTable = CargaDados.curConn.Return_DataTable(SQLString);
                foreach (DataRow curRow in curTable.Rows)
                {

                    nome = Convert.ToString(curRow["Nome_Coluna"]);
                    Caption = Convert.ToString(curRow["Caption_Coluna"]);

                    if (Caption != "Edit" && nome != "Cancel" && nome != "")
                    {
                        largura = Convert.ToInt32(curRow["Largura"]);
                        visivel = Convert.ToBoolean(curRow["Visible"]);
                        Indice = Convert.ToInt32(curRow["Indice"]);
                        indice_grupo = Convert.ToInt32(curRow["Indice_Grupo"]);
                        Caption = curRow["Caption_Coluna"].ToString();

                        if (Type_Grid == 1)
                        {
                            SetColumnStyle_RT(Nome_Grid, ColumnByCaption(nome.ToString(), Nome_Grid), visivel, largura, Indice, indice_grupo, Caption);
                        }
                        if (Type_Grid == 2)
                        {
                            SetColumnStyle_Report(Nome_Grid, ColumnByCaption(nome.ToString(), Nome_Grid), visivel, largura, Indice, indice_grupo, Coluna);
                        }

                    }
                }
                Nome_Grid.EndSort();
            }

            catch(Exception e)
            {
                Error_Dump_TXT(e.ToString(), this.ToString());
            }
        }

        public bool IsNumeric(object value)
        {
            try
            {
             if (String.IsNullOrEmpty(Convert.ToString(value)))
                {
                    return false;
                }
                else
                {
                    decimal d = Convert.ToDecimal(value.ToString());
                    return true;
                }
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public DevExpress.XtraGrid.Columns.GridColumn ColumnByCaption(string s, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn c in Nome_Grid.Columns)
                if (c.Name == s)
                    return c;
            return null;
        }

        public void SetColumnStyle_RT(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid, DevExpress.XtraGrid.Columns.GridColumn c, Boolean visivel, int largura, int Indice,int indice_grupo, string Caption)
        {
            if (c == null) return;
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns[c.FieldName]) == true)
            {
                Nome_Grid.Columns[c.FieldName].Visible = visivel;
                Nome_Grid.Columns[c.FieldName].VisibleIndex = Indice;
                Nome_Grid.Columns[c.FieldName].Width = largura;
                Nome_Grid.Columns[c.FieldName].GroupIndex = indice_grupo;
            }
            else
            {
                MessageBox.Show("ee");
            }
        }

        public void SetColumnStyle_Report(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid, DevExpress.XtraGrid.Columns.GridColumn c, Boolean visivel, int largura, int Indice, int indice_grupo, string Coluna)
        {
            if (c == null) return;
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns[c.Caption]) == true)
            {
                Nome_Grid.Columns[c.Caption].Visible = visivel;
                Nome_Grid.Columns[c.Caption].VisibleIndex = Indice;
                Nome_Grid.Columns[c.Caption].Width = largura;
                Nome_Grid.Columns[c.Caption].GroupIndex = indice_grupo;
                //Nome_Grid.Columns[c.Caption].Fixed = 
            }
            StyleFormatCondition cn;

            cn = new StyleFormatCondition(FormatConditionEnum.Less, Nome_Grid.Columns[Coluna], null, -1);
            cn.Appearance.BackColor = Color.FromArgb(250, 220, 216);
            cn.ApplyToRow = true;
            Nome_Grid.FormatConditions.Add(cn);

            cn = new StyleFormatCondition(FormatConditionEnum.Greater, Nome_Grid.Columns[Coluna], null, 1);
            cn.Appearance.BackColor = Color.FromArgb(222, 254, 235);
            cn.ApplyToRow = true;
            Nome_Grid.FormatConditions.Add(cn);

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Delta Cash"]) == true)
            {
                Nome_Grid.Columns["Delta Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Delta Cash"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // gridView1.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Expiration"]) == true)
            {
                Nome_Grid.Columns["Expiration"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.None;
                Nome_Grid.Columns["Expiration"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // gridView1.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Order Price"]) == true)
            {
                Nome_Grid.Columns["Order Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Order Price"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Position"]) == true)
            {
                Nome_Grid.Columns["Position"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Position"].DisplayFormat.FormatString = "#,##0;(#,##0)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Cash Done"]) == true)
            {
                Nome_Grid.Columns["Cash Done"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Cash Done"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Avg Price Done"]) == true)
            {
                Nome_Grid.Columns["Avg Price Done"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Avg Price Done"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Trade Cash"]) == true)
            {
                Nome_Grid.Columns["Trade Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Trade Cash"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
            }

            
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Portfolio"]) == true)
            {
                Nome_Grid.Columns["Portfolio"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Id Ticker"]) == true)
            {
                Nome_Grid.Columns["Id Ticker"].Visible = false;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Id Portfolio"]) == true)
            {
                Nome_Grid.Columns["Id Portfolio"].Visible = false;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Id Order"]) == true)
            {
                Nome_Grid.Columns["Id Order"].Visible = false;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Id Ticker Type"]) == true)
            {
                Nome_Grid.Columns["Id Ticker Type"].Visible = false;
            }
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Id Strategy"]) == true)
            {
                Nome_Grid.Columns["Id Strategy"].Visible = false;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Id Sub Strategy"]) == true)
            {
                Nome_Grid.Columns["Id Sub Strategy"].Visible = false;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["NAV"]) == true)
            {
                Nome_Grid.Columns["NAV"].Visible = false;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Strategy"]) == true)
            {
                Nome_Grid.Columns["Strategy"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Sub Strategy"]) == true)
            {
                Nome_Grid.Columns["Sub Strategy"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Ticker Type"]) == true)
            {
                Nome_Grid.Columns["Ticker Type"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Ticker"]) == true)
            {
                Nome_Grid.Columns["Ticker"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Description"]) == true)
            {
                Nome_Grid.Columns["Description"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Last Position"]) == true)
            {
                Nome_Grid.Columns["Last Position"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                Nome_Grid.Columns["Last Position"].DisplayFormat.FormatString = "dd/MMM/yy";
                // Nome_Grid.FormatConditions.Add(cn);
            }
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Date Now"]) == true)
            {
                Nome_Grid.Columns["Date Now"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                Nome_Grid.Columns["Date Now"].DisplayFormat.FormatString = "dd/MMM/yy";
                //Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["TimeStamp"]) == true)
            {
                Nome_Grid.Columns["TimeStamp"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                Nome_Grid.Columns["TimeStamp"].DisplayFormat.FormatString = "dd/MMM/yy hh:mm:ss";
                //Nome_Grid.FormatConditions.Add(cn);
            }
            
            //
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Lot Size"]) == true)
            {
                Nome_Grid.Columns["Lot Size"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Lot Size"].DisplayFormat.FormatString = "#,##0.00##;(#,##0.00##)";
                //Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Round Lot"]) == true)
            {
                Nome_Grid.Columns["Round Lot"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Round Lot"].DisplayFormat.FormatString = "#,##0.00##;(#,##0.00##)";
                //Nome_Grid.FormatConditions.Add(cn);
            }

            
            //
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Cost Vwap"]) == true)
            {
                Nome_Grid.Columns["Cost Vwap"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Cost Vwap"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }
            //
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Cost Close"]) == true)
            {
                Nome_Grid.Columns["Cost Close"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Cost Close"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Currency Chg"]) == true)
            {
                Nome_Grid.Columns["Currency Chg"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Currency Chg"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Currency Spot"]) == true)
            {
                Nome_Grid.Columns["Currency Spot"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Currency Spot"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }


            //
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["VWAP"]) == true)
            {
                Nome_Grid.Columns["VWAP"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["VWAP"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }
            //
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Last"]) == true)
            {
                Nome_Grid.Columns["Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Cash Order"]) == true)
            {
                Nome_Grid.Columns["Cash Order"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Cash Order"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["BRL"]) == true)
            {
                Nome_Grid.Columns["BRL"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["BRL"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["BRL/NAV"]) == true)
            {
                Nome_Grid.Columns["BRL/NAV"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["BRL/NAV"].DisplayFormat.FormatString = "P2";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Cash/NAV"]) == true)
            {
                Nome_Grid.Columns["Cash/NAV"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Cash/NAV"].DisplayFormat.FormatString = "P2";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Delta/NAV"]) == true)
            {
                Nome_Grid.Columns["Delta/NAV"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Delta/NAV"].DisplayFormat.FormatString = "P2";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Delta"]) == true)
            {
                Nome_Grid.Columns["Delta"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Delta"].DisplayFormat.FormatString = "#,##0.0000 ;(#,##0.0000)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Spot"]) == true)
            {
                Nome_Grid.Columns["Spot"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Spot"].DisplayFormat.FormatString = "#,##0.0000 ;(#,##0.0000)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Spot USD"]) == true)
            {
                Nome_Grid.Columns["Spot USD"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Spot USD"].DisplayFormat.FormatString = "#,##0.0000 ;(#,##0.0000)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Brokerage"]) == true)
            {
                Nome_Grid.Columns["Brokerage"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Brokerage"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Rebate"]) == true)
            {
                Nome_Grid.Columns["Rebate"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Rebate"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }



            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Price Currency Inv"]) == true)
            {
                Nome_Grid.Columns["Price Currency Inv"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Price Currency Inv"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }


            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Price Currency"]) == true)
            {
                Nome_Grid.Columns["Price Currency"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Price Currency"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }


            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Currency P/L"]) == true)
            {
                Nome_Grid.Columns["Currency P/L"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Currency P/L"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Asset P/L pC"]) == true)
            {
                Nome_Grid.Columns["Asset P/L pC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Asset P/L pC"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Asset pC"]) == true)
            {
                Nome_Grid.Columns["Asset pC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Asset pC"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Asset uC"]) == true)
            {
                Nome_Grid.Columns["Asset uC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Asset uC"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Last pC"]) == true)
            {
                Nome_Grid.Columns["Last pC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Last pC"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Cost Close PU"]) == true)
            {
                Nome_Grid.Columns["Cost Close PU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Cost Close PU"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Settlement Close"]) == true)
            {
                Nome_Grid.Columns["Settlement Close"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Settlement Close"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Settlement Last"]) == true)
            {
                Nome_Grid.Columns["Settlement Last"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Settlement Last"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Last PU"]) == true)
            {
                Nome_Grid.Columns["Last PU"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Last PU"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Cost Close pC"]) == true)
            {
                Nome_Grid.Columns["Cost Close pC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Cost Close pC"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Total P/L"]) == true)
            {
                Nome_Grid.Columns["Total P/L"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Total P/L"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Contribution pC"]) == true)
            {
                Nome_Grid.Columns["Contribution pC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Contribution pC"].DisplayFormat.FormatString = "P2";
                // Nome_Grid.FormatConditions.Add(cn);
            }
            ////////////////////////////
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Long"]) == true)
            {
                Nome_Grid.Columns["Long"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Long"].DisplayFormat.FormatString = "P2";
                // Nome_Grid.FormatConditions.Add(cn);
            }
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Short"]) == true)
            {
                Nome_Grid.Columns["Short"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Short"].DisplayFormat.FormatString = "P2";
                // Nome_Grid.FormatConditions.Add(cn);
            }
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Gross"]) == true)
            {
                Nome_Grid.Columns["Gross"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Gross"].DisplayFormat.FormatString = "P2";
                // Nome_Grid.FormatConditions.Add(cn);
            }
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["P/L %"]) == true)
            {
                Nome_Grid.Columns["P/L %"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["P/L %"].DisplayFormat.FormatString = "P2";
                // Nome_Grid.FormatConditions.Add(cn);
            }



            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Total P/L MTM"]) == true)
            {
                Nome_Grid.Columns["Total P/L MTM"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Total P/L MTM"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Total"]) == true)
            {
                Nome_Grid.Columns["Total"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Total"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }


            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Cash Flow"]) == true)
            {
                Nome_Grid.Columns["Cash Flow"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Cash Flow"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Cash"]) == true)
            {
                Nome_Grid.Columns["Cash"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Cash"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }


            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Avg Price Trade"]) == true)
            {
                Nome_Grid.Columns["Avg Price Trade"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Avg Price Trade"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Price"]) == true)
            {
                Nome_Grid.Columns["Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Price"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }


            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Trade Price"]) == true)
            {
                Nome_Grid.Columns["Trade Price"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Trade Price"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }
  
            
            
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Leaves"]) == true)
            {
                Nome_Grid.Columns["Leaves"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Leaves"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Done"]) == true)
            {
                Nome_Grid.Columns["Done"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Done"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Trade Quantity"]) == true)
            {
                Nome_Grid.Columns["Trade Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Trade Quantity"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            
            
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["AVG Price"]) == true)
            {
                Nome_Grid.Columns["AVG Price Trade"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["AVG Price Trade"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Fee BOV"]) == true)
            {
                Nome_Grid.Columns["Fee BOV"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Fee BOV"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }
            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Fee CBLC"]) == true)
            {
                Nome_Grid.Columns["Fee CBLC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Fee CBLC"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }


            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Credit/Debit"]) == true)
            {
                Nome_Grid.Columns["Credit/Debit"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                Nome_Grid.Columns["Credit/Debit"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";
                //  Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Settlement Date"]) == true)
            {
                Nome_Grid.Columns["Settlement Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                Nome_Grid.Columns["Settlement Date"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                Nome_Grid.Columns["Settlement Date"].DisplayFormat.FormatString = "dd/MMM/yy";
                // Nome_Grid.FormatConditions.Add(cn);
            }

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns["Trade Date"]) == true)
            {
                Nome_Grid.Columns["Trade Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                Nome_Grid.Columns["Trade Date"].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                Nome_Grid.Columns["Trade Date"].DisplayFormat.FormatString = "dd/MMM/yy";
                // Nome_Grid.FormatConditions.Add(cn);
            }
            //c.Visible = visivel;
            //c.Width = largura;
            //c.VisibleIndex = Indice;
        }

        public int Save_Columns(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            string SQLString;
            string Nome;
            decimal largura;
            int visivel;
            int f = 0;
            string indice;
            int Id_Grid;
            int retorno;
            int var = 1;
            string indice_grupo;
            string Caption;

            Id_Grid = Convert.ToInt32(CargaDados.curConn.Execute_Query_String("Select Id_Grid from Tb110_Nome_Grids where Nome_Grid= '" + Nome_Grid.Name + "'"));

            //Id_Grid = 4;
            foreach (DevExpress.XtraGrid.Columns.GridColumn coluna in Nome_Grid.Columns)
            {
                Nome = Nome_Grid.Columns[f].Name;

                if (Nome != "Edit" && Nome != "Cancel" && Nome != "")
                {

                    largura = Nome_Grid.Columns[f].Width;
                    visivel = Convert.ToInt32(Nome_Grid.Columns[f].Visible);
                    indice = Nome_Grid.Columns[f].VisibleIndex.ToString();
                    indice_grupo = Nome_Grid.Columns[f].GroupIndex.ToString();
                    Caption = Nome_Grid.Columns[f].Caption.ToString();

                    SQLString = "INSERT INTO Tb109_Caractisticas_Colunas([Id_Grid], [Id_User], [Nome_Coluna], [Largura], [Visible], [Indice],Indice_Grupo,Caption_Coluna,Versao)" +
                    " VALUES(" + Id_Grid + "," + NestDLL.NUserControl.Instance.User_Id + ",'" + Nome + "'," + largura + "," + visivel + "," + indice + "," + indice_grupo + ",'" + Caption + "',2)";
                    retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,0);
                    if (retorno == 0)
                    {
                        var = 0;
                    }
                }
                f++;
            }
            return var;

        }

        public String GetUserId(string usuario, string senha)
        {
            string SQLString;
            SQLString = "exec Get_User '" + usuario + "','" + senha + "'";

            return CargaDados.curConn.Execute_Query_String(SQLString);
        }

        public Boolean Verifica_Acesso(int Id_Grupo)
        {
            try
            {
                string SQLString = "SELECT COUNT(*) FROM NESTDB.dbo.FCN_CheckUserAccess(" + NestDLL.NUserControl.Instance.User_Id + ") WHERE IdGroup IN (" + Id_Grupo.ToString() + ")";

                int AccessCounter = int.Parse(CargaDados.curConn.Execute_Query_String(SQLString));

                if (AccessCounter > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                Error_Dump_TXT(e.ToString(), this.ToString());
                return false;

            }
        }

        public string Retorna_ordem(int Grid_Cod)
        {
            string SQLString;

            SQLString = "Select Campos_Mostrar from Tb109_Ordens_Colunas where Id_Usuario =" + NestDLL.NUserControl.Instance.User_Id + " and Grid_Cod = " + Grid_Cod;
            SQLString = CargaDados.curConn.Execute_Query_String(SQLString);

            return SQLString;
        }
        //#Fora de USO
        public void Formatar_Grid(DataGridView dg, string Campo)
        {
            // seuDataGridView.ColumnDisplayIndexChanged -= new DataGridViewColumnEventHandler(this.MetodoQueTrataOEvento);

            for (int row = 0; row < dg.Rows.Count; row++)
            {
                if (dg.Rows[row].Cells[Campo].Value.ToString() == "")
                {

                }
                else
                {
                    if (Convert.ToDecimal(dg.Rows[row].Cells[Campo].Value.ToString()) > 0)
                    {
                        dg.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(222, 254, 235);
                    }
                    else if (Convert.ToDecimal(dg.Rows[row].Cells[Campo].Value.ToString()) < 0)
                    {
                        dg.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(250, 220, 216);
                    }
                    else if (Convert.ToDecimal(dg.Rows[row].Cells[Campo].Value.ToString()) == 0)
                    {
                        dg.Rows[row].DefaultCellStyle.BackColor = Color.White;

                    }

                    // dg.Rows[row].RowHeight = 32;

                }
                if (dg.Columns.Contains("Quantity") == true)
                {
                    if (dg.Columns.Contains("Expiration") == true)
                    {
                        if (dg.Rows[row].Cells["Expiration"].Value.ToString() == "")
                        {
                            dg.Rows[row].Cells["Expiration"].Value = "GTC";
                        }
                    }
                }

                if (dg.Columns.Contains("Quantity") == true)
                {
                    if (dg.Rows[row].Cells["Price"].Value.ToString() == "0.0000")
                    {
                        dg.Rows[row].Cells["Price"].Value = "Mkt";
                    }
                    else
                    {
                        dg.Columns["Price"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
                    }
                }
            }

            if (dg.Columns.Contains("Portfolio") == true)
            {
                //                dg.Columns["Portfolio"].Width = 90;
                //                dg.Columns["Portfolio"].Visible = true;
                dg.Columns["Portfolio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dg.Columns.Contains("Id Ticker") == true)
            {
                dg.Columns["Id Ticker"].Visible = false;
            }

            if (dg.Columns.Contains("Ticker") == true)
            {
                //                dg.Columns["Ticker"].Width = 70;
                dg.Columns["Ticker"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dg.Columns.Contains("Description") == true)
            {
                //    dg.Columns["Description"].Width = 98;
                dg.Columns["Description"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dg.Columns.Contains("Id Order") == true)
            {
                dg.Columns["Id Order"].Visible = false;
            }

            if (dg.Columns.Contains("Id Ticker Type") == true)
            {
                dg.Columns["Id Ticker Type"].Visible = false;
            }
            if (dg.Columns.Contains("Ticker Type") == true)
            {
                //    dg.Columns["Ticker Type"].Width = 89;
                dg.Columns["Ticker Type"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dg.Columns.Contains("Id Strategy") == true)
            {
                dg.Columns["Id Strategy"].Visible = false;
            }

            if (dg.Columns.Contains("Strategy") == true)
            {
                //   dg.Columns["Strategy"].Width = 98;
                dg.Columns["Strategy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dg.Columns.Contains("Id Sub Strategy") == true)
            {
                dg.Columns["Id Sub Strategy"].Visible = false;
            }

            if (dg.Columns.Contains("Sub Strategy") == true)
            {
                //     dg.Columns["Sub Strategy"].Width = 95;
                dg.Columns["Sub Strategy"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            if (dg.Columns.Contains("Last Position") == true)
            {
                //    dg.Columns["Last Position"].Width = 95;
                dg.Columns["Last Position"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dg.Columns["Last Position"].DefaultCellStyle.Format = "dd/MMM/yy";
            }

            if (dg.Columns.Contains("Date Now") == true)
            {
                //     dg.Columns["Date Now"].Width = 85;
                dg.Columns["Date Now"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dg.Columns["Date Now"].DefaultCellStyle.Format = "dd/MMM/yy";
            }

            if (dg.Columns.Contains("Position") == true)
            {
                //     dg.Columns["Position"].Width = 75;
                dg.Columns["Position"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Position"].DefaultCellStyle.Format = "#,##0;(#,##)";
            }

            if (dg.Columns.Contains("Lot size") == true)
            {
                //     dg.Columns["Lot Size"].Width = 70;
                dg.Columns["Lot Size"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Lot Size"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Cost Vwap") == true)
            {
                //     dg.Columns["Cost Vwap"].Width = 85;
                dg.Columns["Cost Vwap"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Cost Vwap"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }
            if (dg.Columns.Contains("Cost Close") == true)
            {
                //     dg.Columns["Cost Close"].Width = 85;
                dg.Columns["Cost Close"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Cost Close"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("VWAP") == true)
            {
                //  dg.Columns["VWAP"].Width = 50;
                dg.Columns["VWAP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["VWAP"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("LAST") == true)
            {
                //   dg.Columns["LAST"].Width = 50;
                dg.Columns["LAST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["LAST"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Cash") == true)
            {
                //   dg.Columns["Cash"].Width = 70;
                dg.Columns["Cash"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Cash"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Delta Cash") == true)
            {
                //   dg.Columns["Delta Cash"].Width = 85;
                dg.Columns["Delta Cash"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Delta Cash"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("NAV") == true)
            {
                dg.Columns["NAV"].Visible = false;
            }

            if (dg.Columns.Contains("CASH/NAV") == true)
            {
                //    dg.Columns["% NAV"].Width = 70;
                dg.Columns["CASH/NAV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["CASH/NAV"].DefaultCellStyle.Format = "P2";
            }

            if (dg.Columns.Contains("Delta/NAV") == true)
            {
                //    dg.Columns["% NAV"].Width = 70;
                dg.Columns["Delta/NAV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Delta/NAV"].DefaultCellStyle.Format = "P2";
            }

            if (dg.Columns.Contains("Delta") == true)
            {
                //    dg.Columns["Delta"].Width = 70;
                dg.Columns["Delta"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Delta"].DefaultCellStyle.Format = "#,##0.00##;(#,##0.00##)";

            }
            if (dg.Columns.Contains("Spot") == true)
            {
                //    dg.Columns["Delta"].Width = 70;
                dg.Columns["Spot"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Spot"].DefaultCellStyle.Format = "#,##0.00##;(#,##0.00##)";

            }
            if (dg.Columns.Contains("Spot USD") == true)
            {
                //    dg.Columns["Delta"].Width = 70;
                dg.Columns["Spot USD"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Spot USD"].DefaultCellStyle.Format = "#,##0.00##;(#,##0.00##)";

            }

            if (dg.Columns.Contains("brokerage") == true)
            {
                //  dg.Columns["brokerage"].Width = 60;
                dg.Columns["brokerage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["brokerage"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Rebate brokerage") == true)
            {
                //    dg.Columns["Rebate"].Width = 80;
                dg.Columns["Rebate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Rebate"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Open P/L VWAP") == true)
            {
                //    dg.Columns["Open P/L VWAP"].Width = 115;
                dg.Columns["Open P/L VWAP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Open P/L VWAP"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Open P/L LAST") == true)
            {
                //   dg.Columns["Open P/L LAST"].Width = 110;
                dg.Columns["Open P/L LAST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Open P/L LAST"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Open P/L MTM") == true)
            {
                //    dg.Columns["Open P/L MTM"].Width = 110;
                dg.Columns["Open P/L MTM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Open P/L MTM"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Closed P/L VWAP") == true)
            {
                //    dg.Columns["Closed P/L VWAP"].Width = 120;
                dg.Columns["Closed P/L VWAP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Closed P/L VWAP"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Closed P/L LAST") == true)
            {
                //    dg.Columns["Closed P/L LAST"].Width = 115;
                dg.Columns["Closed P/L LAST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Closed P/L LAST"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }
            if (dg.Columns.Contains("Closed P/L MTM") == true)
            {
                //   dg.Columns["Closed P/L MTM"].Width = 115;
                dg.Columns["Closed P/L MTM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Closed P/L MTM"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Total P/L VWAP") == true)
            {
                //    dg.Columns["Total P/L VWAP"].Width = 115;
                dg.Columns["Total P/L VWAP"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Total P/L VWAP"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Total P/L LAST") == true)
            {
                //    dg.Columns["Total P/L LAST"].Width = 110;
                dg.Columns["Total P/L LAST"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Total P/L LAST"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Total P/L MTM") == true)
            {
                //    dg.Columns["Total P/L MTM"].Width = 110;
                dg.Columns["Total P/L MTM"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Total P/L MTM"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Quantity") == true)
            {
                //        dg.Columns["Quantity"].Width = 73;
                dg.Columns["Quantity"].DefaultCellStyle.Format = "#,##0;(#,##0)";
            }

            if (dg.Columns.Contains("Cash Flow") == true)
            {
                //       dg.Columns["Cash Flow"].Width = 83;
                dg.Columns["Cash Flow"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            //largura das linhas
            if (dg.Columns.Contains("Price") == true)
            {
                //       dg.Columns["Price"].Width = 56;
                dg.Columns["Price"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Leaves Quantity") == true)
            {
                dg.Columns["Leaves Quantity"].DefaultCellStyle.Format = "#,##0;(#,##0)";
                //             dg.Columns["Leaves Quantity"].Width = 114;
            }

            if (dg.Columns.Contains("Done Quantity") == true)
            {
                dg.Columns["Done Quantity"].DefaultCellStyle.Format = "#,##0;(#,##0)";
                //           dg.Columns["Done Quantity"].Width = 104;
            }

            if (dg.Columns.Contains("AVG Price") == true)
            {
                //           dg.Columns["AVG Price"].Width = 82;
                dg.Columns["AVG Price"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Fee BOV") == true)
            {
                //        dg.Columns["Fee BOV"].Width = 120;
                dg.Columns["Fee BOV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Fee BOV"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }

            if (dg.Columns.Contains("Fee CBLC") == true)
            {
                //        dg.Columns["Fee CBLC"].Width = 120;
                dg.Columns["Fee CBLC"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dg.Columns["Fee CBLC"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
            }


            if (dg.Name == "dgCaixa")
            {
                if (dg.Columns.Contains("Credit/Debit") == true)
                {
                    //   dg.Columns["Delta Cash"].Width = 85;
                    dg.Columns["Credit/Debit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dg.Columns["Credit/Debit"].DefaultCellStyle.Format = "#,##0.00;(#,##0.00)";
                }

                if (dg.Columns.Contains("Settlement Date") == true)
                {
                    //     dg.Columns["Date Now"].Width = 85;
                    dg.Columns["Settlement Date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dg.Columns["Settlement Date"].DefaultCellStyle.Format = "dd/MMM/yy";
                }


            }
        }

        public int Checa_Arquivo(string Caminho_Nome_Arquivo)
        {
            int Valida_arq;
            try
            {
                if (File.Exists(Caminho_Nome_Arquivo))
                {
                    Valida_arq = 1;
                }
                else
                {
                    // Valida_arq =  Cria_Arquivo(Convert.ToString(Caminho_Nome_Arquivo));
                    Valida_arq = 0;
                }
                return Valida_arq;
            }
            catch(Exception e)
            {
                Error_Dump_TXT(e.ToString(), "Checa_Arquivo");

                return 0;
            }
        }

        public string Abre_Arquivo()
        {
            if (Checa_Arquivo(@"C:\WINDOWS\login.txt") == 1)
            {

            }
            else
            {
            }
            return "certo";
        }
        //Tipo 0 = Preco - Tipo 1 = Trades
        public string Retorna_Tabela_Preco_Trade(int Id_Ativo, int tipo_tabela)
        {
            string Nome_Tabela = "";
            int IdPriceTable = Convert.ToInt32(CargaDados.curConn.Execute_Query_String("Select IdPriceTable from dbo.Tb001_Securities where IdSecurity = " + Id_Ativo));

            switch (IdPriceTable)
            {
                case 1:
                    if (tipo_tabela == 0)
                    {
                        Nome_Tabela = "Tb050_Preco_Acoes_Onshore";
                    }
                    if (tipo_tabela == 1)
                    {
                        Nome_Tabela = "Tb061_Trades_Onshore";
                    }
                    break;
                case 2:
                    if (tipo_tabela == 0)
                    {
                        Nome_Tabela = "Tb051_Preco_Acoes_Offshore";
                    }
                    if (tipo_tabela == 1)
                    {
                        Nome_Tabela = "Tb062_Trades_Offshore";
                    }
                    break;
                case 3:
                    if (tipo_tabela == 0)
                    {
                        Nome_Tabela = "Tb058_Precos_Moedas";
                    }
                    if (tipo_tabela == 1)
                    {
                        Nome_Tabela = "";
                    }
                    break;
                case 4:
                    if (tipo_tabela == 0)
                    {
                        Nome_Tabela = "Tb053_Precos_Indices";
                    }
                    if (tipo_tabela == 1)
                    {
                        Nome_Tabela = "";
                    }
                    break;
                case 5:
                    if (tipo_tabela == 0)
                    {
                        Nome_Tabela = "Tb059_Precos_Futuros";
                    }
                    if (tipo_tabela == 1)
                    {
                        Nome_Tabela = "Tb063_Trades_Futuros";
                    }
                    break;
                case 6:
                    if (tipo_tabela == 0)
                    {
                        Nome_Tabela = "Tb057_Precos_Commodities";
                    }
                    if (tipo_tabela == 1)
                    {
                        Nome_Tabela = "";
                    }
                    break;
                case 7:
                    if (tipo_tabela == 0)
                    {
                        Nome_Tabela = "Tb054_Precos_Opcoes";
                    }
                    if (tipo_tabela == 1)
                    {
                        Nome_Tabela = "Tb064_Trades_Opcoes";
                    }
                    break;

                case 8:
                    if (tipo_tabela == 0)
                    {
                        Nome_Tabela = "Tb052_Precos_Titulos";
                    }
                    if (tipo_tabela == 1)
                    {
                        Nome_Tabela = "";
                    }
                    break;
                case 9:
                    if (tipo_tabela == 0)
                    {
                        Nome_Tabela = "Tb060_Preco_Caixa";
                    }
                    if (tipo_tabela == 1)
                    {
                        Nome_Tabela = "";
                    }
                    break;
                case 10:
                    if (tipo_tabela == 0)
                    {
                        Nome_Tabela = "Tb056_Precos_Fundos";
                    }
                    if (tipo_tabela == 1)
                    {
                        Nome_Tabela = "";
                    }
                    break;
                default:
                    Nome_Tabela = "";
                    break;
            }
            return Nome_Tabela;
        }
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }

                
        public void  Save_Properties_Form(Form Formulario)
        {
            Save_Properties_Form(Formulario, 0);
        }

        public void Save_Properties_Form(Form FormToSave, int ID)
        {
            string TOP;
            string LEFT;
            int Visible;
            string Width;
            string height;
            string Nome_Formulario;
            string SQLString;
            int Id_Form;
            int retorno;
            int IsDetached = 0;

            Nome_Formulario = FormToSave.Name;

            Id_Form = Convert.ToInt32(CargaDados.curConn.Execute_Query_String("Select Id_Formulario from Tb123_Nome_Formularios where Nome_Formulario= '" + Nome_Formulario + "'"));

            if (FormToSave.WindowState.ToString() != "Maximized")
            {
                /*
                if(FormToSave.Top <-5 || FormToSave.Top < 5)
                {
                    FormToSave.Top=0;
                }

                if (FormToSave.Left < -5 || FormToSave.Left < 5)
                {
                    FormToSave.Left = 0;
                }
                */
                if (FormToSave.MdiParent == null)
                {
                    IsDetached = 1;
                }

                TOP = FormToSave.Top.ToString().Replace(",", ".");
                LEFT = FormToSave.Left.ToString().Replace(",", ".");
                Width = FormToSave.Width.ToString().Replace(",", ".");
                height = FormToSave.Height.ToString().Replace(",", ".");

                if (GlobalVars.Instance.appClosing)
                {
                    Visible = 1;
                }
                else
                {
                    Visible = 0;
                }

                string ControlValues = " ";

                foreach (object curControl in FormToSave.Controls)
                {
                    if (curControl.GetType() == typeof(System.Windows.Forms.ComboBox) || curControl.GetType() == typeof(NestPortCombo) || curControl.GetType() == typeof(NestBookCombo))
                    {
                        string curName = ((System.Windows.Forms.ComboBox)curControl).Name;
                        string curValue = ((System.Windows.Forms.ComboBox)curControl).SelectedIndex.ToString();
                        ControlValues = ControlValues + '\n' + curName + '\t' + curValue;
                    }
                }

                if (Convert.ToDouble(TOP) > -10000 || Convert.ToDouble(LEFT) > -10000)
                {
                    SQLString = "Insert into Tb124_Caracteristicas_Formularios (Id_Form,Id_User,form_top,form_left,visivel,Largura,altura,ID, ControlValues, IsDetached) values " +
                                 "(" + Id_Form + "," + NestDLL.NUserControl.Instance.User_Id + "," + TOP + "," + LEFT + "," + Visible + " ," + Width + "," + height + "," + ID + ", '" + ControlValues + "', " + IsDetached + ")";

                    retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,0);
                }
                else
                {
                    retorno = 1;
                }
                if (retorno == 0)
                {
                    
                }
            }
            Save_Open_Close_Form(Nome_Formulario, 2);
        }

        public void Save_ID_Form(Form Formulario, int ID)
        {
            int Id_Form;
            string Nome_Formulario;
            string SQLString;

            Nome_Formulario = Formulario.Name;
         

            Id_Form = Convert.ToInt32(CargaDados.curConn.Execute_Query_String("Select Id_Formulario from Tb123_Nome_Formularios where Nome_Formulario= '" + Nome_Formulario + "'"));

            SQLString = "UPDATE Tb124_Caracteristicas_Formularios SET ID = 1 Where Id_User=" + NestDLL.NUserControl.Instance.User_Id + " and Id_Form=" + Id_Form;
            
                int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,0);
                if (retorno == 0)
                {
                    MessageBox.Show("Error on save Properties!");
                }
        }

        public void Save_Visible_Form(Form Formulario)
        {
            int Id_Form;
            string Nome_Formulario;
            string SQLString;

            Nome_Formulario = Formulario.Name;

            string teste = CargaDados.curConn.Execute_Query_String("Select Id_Formulario from Tb123_Nome_Formularios where Nome_Formulario= '" + Nome_Formulario + "'");
            if (teste != "")
            {

                Id_Form = Convert.ToInt32(CargaDados.curConn.Execute_Query_String("Select Id_Formulario from Tb123_Nome_Formularios where Nome_Formulario= '" + Nome_Formulario + "'"));

                SQLString = "UPDATE Tb124_Caracteristicas_Formularios SET visivel = 1 Where Id_User=" + NestDLL.NUserControl.Instance.User_Id + " and Id_Form=" + Id_Form;

                int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,0);
                if (retorno == 0)
                {
                    MessageBox.Show("Error on save Properties!");
                }
            }
        }

        public bool Load_Properties_Form(Form FormToSet)
        {
            string Nome_Formulario;
            string SQLString;
            int Id_Form;
            Nome_Formulario = FormToSet.Name;
            bool tempReturn = false;

            Id_Form = Convert.ToInt32(CargaDados.curConn.Execute_Query_String("Select Id_Formulario from Tb123_Nome_Formularios where Nome_Formulario= '" + Nome_Formulario + "'"));

            SQLString = "Select * from Tb124_Caracteristicas_Formularios Where Id_Form=" + Id_Form + " and Id_User=" + NestDLL.NUserControl.Instance.User_Id;

            DataTable curTable = CargaDados.curConn.Return_DataTable(SQLString);
            foreach (DataRow curRow in curTable.Rows)
            {
                FormToSet.Top = Convert.ToInt32(curRow["form_top"]);
                FormToSet.Left = Convert.ToInt32(curRow["form_left"]);
                FormToSet.Width = Convert.ToInt32(curRow["Largura"]);
                FormToSet.Height = Convert.ToInt32(curRow["altura"]);
                if (Convert.ToInt32(curRow["IsDetached"]) == 1)
                {
                    tempReturn = true;
                }
                else
                {
                    tempReturn = false;
                }
            }

            Save_Open_Close_Form(Nome_Formulario, 1);
            return tempReturn;

        }

        public void Set_Controls_Form(Form Formulario)
        {
            string Nome_Formulario;
            string SQLString;
            int Id_Form;
            Nome_Formulario = Formulario.Name;

            Id_Form = Convert.ToInt32(CargaDados.curConn.Execute_Query_String("Select Id_Formulario from Tb123_Nome_Formularios where Nome_Formulario= '" + Nome_Formulario + "'"));

            SQLString = "Select * from Tb124_Caracteristicas_Formularios Where Id_Form=" + Id_Form + " and Id_User=" + NestDLL.NUserControl.Instance.User_Id;

            DataTable curTable = CargaDados.curConn.Return_DataTable(SQLString);

            try
            {
                foreach (DataRow curRow in curTable.Rows)
                {
                    string ControlValues = curRow["ControlValues"].ToString();

                    if (ControlValues.Trim() != "")
                    {
                        string[] curControlData = ControlValues.Split('\n');

                        for (int i = 0; i < curControlData.Length; i++)
                        {
                            if (curControlData[i].Trim() != "")
                            {
                                string curName = curControlData[i].Split('\t')[0];
                                string curVal = curControlData[i].Split('\t')[1];
                                Control curControl = FindControlByName(Formulario, curName);
                                if (curControl.GetType() == typeof(System.Windows.Forms.ComboBox) || curControl.GetType() == typeof(NestPortCombo) || curControl.GetType() == typeof(NestBookCombo))
                                {
                                    if (((ComboBox)curControl).Items.Count > Convert.ToInt16(curVal))
                                    {
                                        ((ComboBox)curControl).SelectedIndex = Convert.ToInt16(curVal);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
            
            }
        }

        private Control FindControlByName(Form Formulario, string name)
        {
            foreach (Control c in Formulario.Controls) 
            {
                if (c.Name == name)
                    return c; 
            }
            return null; 
        }

       public int Error_Dump_TXT(string MsgError,string Source)
        {
            string user = Environment.UserName.ToString();
            string Loca_Machine = Environment.MachineName.ToString();

            string strPath = @"T:\Log\LiveBook\Error_" + user + "_" + Loca_Machine + ".txt";

            FileInfo OpenArq = new FileInfo(strPath);
            StreamWriter Escreve;

            if (!OpenArq.Exists)
            {
                OpenArq.Create().Close();
            }
            Escreve = OpenArq.AppendText();
            Escreve.WriteLine("Source - " + Source.ToString());
            Escreve.WriteLine("Date - " + DateTime.Now.ToString());
            Escreve.WriteLine(MsgError.ToString());
            Escreve.WriteLine("---------------------------------------------");
            Escreve.WriteLine("");
            Escreve.WriteLine("");
            Escreve.Flush();
            Escreve.Close();

            return 1;
        }

       public int Save_Open_Close_Form (string FormName,int ActionType)
       {
           string user = Environment.UserName.ToString();
           string Loca_Machine = Environment.MachineName.ToString();
           string StringType="";
           if (ActionType == 1)
           {
            StringType ="OPEN";
           }
           if (ActionType == 2)
           {
           StringType ="CLOSE";
           }

           string strPath = @"T:\Log\LiveBook\StatusForm_" + user + "_" + Loca_Machine + ".txt";

           FileInfo OpenArq = new FileInfo(strPath);
           StreamWriter Escreve;

           if (!OpenArq.Exists)
           {
               OpenArq.Create().Close();
           }
           Escreve = OpenArq.AppendText();
           Escreve.WriteLine(DateTime.Now.ToString() + " - " + StringType + " - " + FormName.ToString());
           Escreve.WriteLine("---------------------------------------------");
           Escreve.WriteLine("");
           Escreve.Flush();
           Escreve.Close();

           return 1;
       }

    }
}