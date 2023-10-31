using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace TickerDLL
{
    public class FormUtils
    {
        public static void LoadCombo(ComboBox ComboName, string StringSQl, string Value, string Display, int selected)
        {
            using (newNestConn curconn = new newNestConn())
            {
                DataTable table = curconn.Return_DataTable(StringSQl);
                ComboName.ValueMember = Value;
                ComboName.DisplayMember = Display;
                ComboName.DataSource = table;
                
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
        }

        public static void LoadCombo(ComboBox ComboName, string StringSQl, string Value, string Display)
        {
            LoadCombo(ComboName, StringSQl, Value, Display, 99);
        }

        public static void LoadList(ListBox ListName, string StringSQl, string Value, string Display)
        {
            using (newNestConn curconn = new newNestConn())
            {
                DataTable table = curconn.Return_DataTable(StringSQl);
                ListName.ValueMember = Value;
                ListName.DisplayMember = Display;
                ListName.DataSource = table;
            }
        }

    }
}
