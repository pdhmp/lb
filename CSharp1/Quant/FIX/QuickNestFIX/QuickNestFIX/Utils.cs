using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace QuickNestFIX
{
    public class Utils
    {
        public int IdApplication = 0;

        public Utils(int _IdApplication)
        {
            IdApplication = _IdApplication;
        }

        public void SaveFormProperties(Form FormToSave, int Id_User)
        {
            string FormName = "";
            string FormTop = "";
            string FormLeft = "";
            string FormWidth = "";
            string FormHeight = "";
            string FormCaption = "";

            int Visible = 0;
            int IsDetached = 0;

            string SQLString;
            int Id_Form = 0;
            int ListID = 0;

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {

                FormName = FormToSave.Name;

                SQLString = "SELECT Id_Form FROM UI_DEFS.dbo.Tb201_Forms WHERE Form_Name= '" + FormName + "' AND IdApplication=" + IdApplication;
                try
                {
                    Id_Form = int.Parse(curConn.Execute_Query_String(SQLString));
                }
                catch (Exception e)
                {
                    Id_Form = 0;
                }
            }

            if (FormToSave.WindowState.ToString() != "Maximized")
            {
                if (FormToSave.MdiParent == null)
                {
                    IsDetached = 1;
                }

                FormTop = FormToSave.Top.ToString().Replace(",", ".");
                FormLeft = FormToSave.Left.ToString().Replace(",", ".");
                FormWidth = FormToSave.Width.ToString().Replace(",", ".");
                FormHeight = FormToSave.Height.ToString().Replace(",", ".");
                FormCaption = FormToSave.Text;

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
                    if (curControl.GetType() == typeof(System.Windows.Forms.ComboBox))
                    {
                        string curName = ((System.Windows.Forms.ComboBox)curControl).Name;
                        string curValue = ((System.Windows.Forms.ComboBox)curControl).SelectedIndex.ToString();
                        ControlValues = ControlValues + '\n' + curName + '\t' + curValue;
                    }
                }

                if (Convert.ToDouble(FormTop) > -10000 || Convert.ToDouble(FormLeft) > -10000)
                {
                    SQLString = "INSERT INTO UI_DEFS.dbo.Tb201_Form_Properties (Id_Form,Id_User,Form_Top,Form_Left,Visible,Form_Width,Form_Height,ID, ControlValues, IsDetached, Form_Caption) values " +
                                 "(" + Id_Form + "," + Id_User + "," + FormTop + "," + FormLeft + "," + Visible + " ," + FormWidth + "," + FormHeight + ", " + ListID + ", '" + ControlValues + "', " + IsDetached + ", '" + FormCaption + "')";

                    using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
                    {
                        curConn.ExecuteNonQuery(SQLString);
                    }
                }
            }
        }

        public bool LoadFormProperties(Form FormToLoad, int Id_User, string FormCaption)
        {
            string Nome_Formulario;
            string SQLString;
            int Id_Form = 0;
            Nome_Formulario = FormToLoad.Name;
            bool tempReturn = false;
            string FormName = "";

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                FormName = FormToLoad.Name;

                SQLString = "SELECT Id_Form FROM UI_DEFS.dbo.Tb201_Forms WHERE Form_Name= '" + FormName + "' AND IdApplication=" + IdApplication.ToString(); ;

                try
                {
                    Id_Form = int.Parse(curConn.Execute_Query_String(SQLString));
                }
                catch (Exception e)
                {
                    Id_Form = 0;
                }

                SQLString = "SELECT * FROM UI_DEFS.dbo.Tb201_Form_Properties WHERE Id_Form=" + Id_Form + " AND Id_User=" + Id_User + " AND Form_Caption='" + FormCaption + "'";

                SqlDataReader DtReader = curConn.Return_DataReader(SQLString);

                while (DtReader.Read())
                {
                    FormToLoad.Top = Convert.ToInt32(DtReader["Form_Top"]);
                    FormToLoad.Left = Convert.ToInt32(DtReader["Form_Left"]);
                    FormToLoad.Width = Convert.ToInt32(DtReader["Form_Width"]);
                    FormToLoad.Height = Convert.ToInt32(DtReader["Form_Height"]);
                    if (Convert.ToInt32(DtReader["IsDetached"]) == 1)
                    {
                        tempReturn = true;
                    }
                    else
                    {
                        tempReturn = false;
                    }
                }

                DtReader.Close();
                DtReader.Dispose();
            }

            return tempReturn;

        }

        public void LoadOpenForms(Form senderForm)
        {
            string SQLString = "SELECT * " +
                        " FROM UI_DEFS.dbo.Tb201_Forms A " +
                        " INNER JOIN UI_DEFS.dbo.Tb201_Form_Properties B " +
                        " ON A.Id_Form=B.Id_Form " +
                        " WHERE Form_Name<>'frmMain' AND Visible=1 AND IdApplication=" + IdApplication + " AND Id_User= " + NestDLL.NUserControl.Instance.User_Id;

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                DataTable dt = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {
                    string curClassName = curRow["Form_Name"].ToString();
                    Type t = Type.GetType("QuantMonitor." + curClassName);
                    Form newForm = (Form)Activator.CreateInstance(t);

                    if (curRow["IsDetached"].ToString() != "1") newForm.MdiParent = senderForm;
                    newForm.Icon = senderForm.Icon;
                    newForm.Show();
                }
            }
        }
        
        public void LoadGridColumns(DevExpress.XtraGrid.Views.Grid.GridView curGrid)
        {
            int curGridId;
            string curColumnName;
            int curWidth;
            Boolean curVisible;
            int curIndex;
            int curGroupIndex;

            try
            {
                using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
                {
                    string SQLString = "SELECT IdGrid FROM UI_DEFS.dbo.Tb210_Grids WHERE GridName= '" + curGrid.Name.ToString() + "' AND IdApplication=" + IdApplication.ToString();
                    curGridId = Convert.ToInt32(curConn.Execute_Query_String(SQLString));

                    SQLString = "SELECT * FROM UI_DEFS.dbo.Tb211_Grid_Columns WHERE IdGrid =" + curGridId + " and IdUser =" + NestDLL.NUserControl.Instance.User_Id + " ORDER BY VisibleIndex asc";
                    SqlDataReader DtReader = curConn.Return_DataReader(SQLString);
                    curGrid.BeginSort();

                    while (DtReader.Read())
                    {
                        try
                        {
                            curColumnName = Convert.ToString(DtReader["ColumnName"]);

                            if (curColumnName != "Edit" && curColumnName != "Cancel" && curColumnName != "")
                            {
                                curWidth = Convert.ToInt32(DtReader["ColumnWidth"]);
                                curVisible = Convert.ToBoolean(DtReader["isVisible"]);
                                curIndex = Convert.ToInt32(DtReader["VisibleIndex"]);
                                curGroupIndex = Convert.ToInt32(DtReader["GroupIndex"]);

                                DevExpress.XtraGrid.Columns.GridColumn curColumn = curGrid.Columns.ColumnByName(curColumnName);

                                curColumn.Visible = curVisible;
                                curColumn.VisibleIndex = curIndex;
                                curColumn.Width = curWidth;
                                curColumn.GroupIndex = curGroupIndex;

                            }
                        }
                        catch
                        { 
                        }
                    }
                    curGrid.EndSort();
                }
            }
            catch (Exception e) 
            { 
            }
        }

        public void SaveGridColumns(DevExpress.XtraGrid.Views.Grid.GridView curGrid)
        {
            string SQLString;
            int curGridId;
            string curColumnName;
            int curWidth;
            int curVisible;
            int curIndex;
            int curGroupIndex;

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                SQLString = "SELECT IdGrid FROM UI_DEFS.dbo.Tb210_Grids WHERE GridName= '" + curGrid.Name.ToString() + "' AND IdApplication=" + IdApplication.ToString(); ;
                curGridId = int.Parse(curConn.Execute_Query_String(SQLString));

                foreach (DevExpress.XtraGrid.Columns.GridColumn curColumn in curGrid.Columns)
                {
                    curColumnName = curColumn.Name;

                    if (curColumnName != "Edit" && curColumnName != "Cancel" && curColumnName != "")
                    {
                        curWidth = curColumn.Width;
                        if (curColumn.Visible) { curVisible = 1; } else { curVisible = 0; };
                        curIndex = curColumn.VisibleIndex;
                        curGroupIndex = curColumn.GroupIndex;

                        SQLString = "INSERT INTO UI_DEFS.[dbo].[Tb211_Grid_Columns]([IdGrid],[IdUser],[ColumnName],[ColumnWidth],[isVisible],[VisibleIndex],[GroupIndex])" +
                        " VALUES(" + curGridId + "," + NestDLL.NUserControl.Instance.User_Id + ",'" + curColumnName + "'," + curWidth + "," + curVisible + "," + curIndex.ToString() + "," + curGroupIndex.ToString() + ")";
                        curConn.ExecuteNonQuery(SQLString, 0);
                    }
                }
            }
        }
        
    }
}
