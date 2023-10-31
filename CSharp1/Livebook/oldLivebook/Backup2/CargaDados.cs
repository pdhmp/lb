using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using NestDLL;

namespace SGN
{
    public class CarregaDados
    {
        public newNestConn curConn = new newNestConn();

        public void carregacombo(ComboBox ComboName, string SQLString, string Value, string Display, int selected)
        {
            DataTable table = curConn.Return_DataTable(SQLString);

            ComboName.DataSource = table;
            ComboName.DisplayMember = Display;
            ComboName.ValueMember = Value;

            if (selected != 99)
            {
                foreach (System.Data.DataRowView tempItem in ComboName.Items)
                {
                    if (tempItem.Row[0].ToString() == selected.ToString())
                    {
                        ComboName.SelectedValue = selected;
                    }
                }
            }
        }


        public void carregacombo(ComboBox ComboName, string SQLString, string Value, string Display)
        {
            carregacombo(ComboName, SQLString, Value, Display, 99);
        }

        public void carregacombo_Table(ComboBox ComboName, DataTable table, string Value, string Display, int selected)
        {
            ComboName.DataSource = table;
            ComboName.DisplayMember = Display;
            ComboName.ValueMember = Value;
            if (selected != 99)
            {
                foreach (System.Data.DataRowView tempItem in ComboName.Items)
                {
                    if (tempItem.Row[0].ToString() == selected.ToString())
                    {
                        ComboName.SelectedValue = selected;
                    }
                }
            }
        }


        public void carregaList(ListBox ListName, string SQLString, string Value, string Display)
        {
            DataTable table = curConn.Return_DataTable(SQLString);

            ListName.DataSource = table;
            ListName.DisplayMember = Display;
            ListName.ValueMember = Value;   
        }

    }
}
