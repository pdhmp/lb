using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;
using System.Data;

namespace NestDesk
{
    public class Utils
    {
        public int IdApplication = 0;

        public void SetColumnStyle(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid, int Type_Grid)
        {
            SetColumnStyle(Nome_Grid, Type_Grid, "");
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
                    Nome_Grid.Columns[c].Caption = col.ToString();//.Replace(' ','\n');
                    c++;
                }


                string SQLString = "Select Id_Grid from Tb110_Nome_Grids where Nome_Grid= '" + Nome_Grid.Name.ToString() + "'";
                using (newNestConn curConn = new newNestConn())
                {
                    Id_Grid = Convert.ToInt32(curConn.Execute_Query_String(SQLString));
                    SQLString = "Select * from Tb109_Caractisticas_Colunas where Id_Grid =" + Id_Grid + " and Id_User =" + NestDLL.NUserControl.Instance.User_Id + " and Versao = 2  order by Indice ";

                    Nome_Grid.BeginSort();

                    DataTable curTable = curConn.Return_DataTable(SQLString);
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

                            SetColumnStyle_RT(Nome_Grid, ColumnByCaption(nome.ToString(), Nome_Grid), visivel, largura, Indice, indice_grupo, Caption);


                        }
                    }
                    Nome_Grid.EndSort();
                }
            }

            catch (Exception e)
            {
            }
        }
            public void SetColumnStyle_RT(DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid, DevExpress.XtraGrid.Columns.GridColumn c, Boolean visivel, int largura, int Indice,int indice_grupo, string Caption)
        {
            if (c == null) return;
            if (c.Caption == "Delta/Book")
                c.Caption = c.Caption;

            if (Nome_Grid.Columns.Contains(Nome_Grid.Columns[c.FieldName]) == true)
            {
                Nome_Grid.Columns[c.FieldName].VisibleIndex = Indice;
                Nome_Grid.Columns[c.FieldName].Visible = visivel;
                Nome_Grid.Columns[c.FieldName].Width = largura;
                Nome_Grid.Columns[c.FieldName].GroupIndex = indice_grupo;
            }
            else
            {
                MessageBox.Show("ee");
            }
        }

            public DevExpress.XtraGrid.Columns.GridColumn ColumnByCaption(string s, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
            {
                foreach (DevExpress.XtraGrid.Columns.GridColumn c in Nome_Grid.Columns)
                    if (c.Name == s)
                        return c;
                return null;
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
            using (newNestConn curConn = new newNestConn())
            {
                Id_Grid = Convert.ToInt32(curConn.Execute_Query_String("Select Id_Grid from Tb110_Nome_Grids where Nome_Grid= '" + Nome_Grid.Name + "'"));

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

                        retorno = curConn.ExecuteNonQuery(SQLString, 0);

                        if (retorno == 0)
                        {
                            var = 0;
                        }
                    }
                    f++;
                }
            }
            return var;
        }

        public Boolean Verifica_Acesso(int Id_Grupo)
        {
            try
            {
                string SQLString = "SELECT COUNT(*) FROM NESTDB.dbo.FCN_CheckUserAccess(" + NestDLL.NUserControl.Instance.User_Id + ") WHERE IdGroup IN (" + Id_Grupo.ToString() + ")";
                using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
                {

                    int AccessCounter = int.Parse(curConn.Execute_Query_String(SQLString));

                if (AccessCounter > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                }
            }
            catch (Exception e)
            {
                return false;

            }
        }
    }
}
