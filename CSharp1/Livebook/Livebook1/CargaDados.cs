using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using NestDLL;

namespace SGN.CargaDados
{
    class CarregaDados
    {
        public DB DB = new DB();

       public void carregacombo(ComboBox ComboName, string StringSQl, string Value, string Display, int selected)
        {
            SqlDataAdapter da = DB.Return_DataAdapter(StringSQl);
            DataTable table = new DataTable();
            da.Fill(table); 
            ComboName.DataSource = table;
            ComboName.DisplayMember = Display;
            ComboName.ValueMember = Value;
            if (selected != 99)
            {
                ComboName.SelectedValue = selected;
            }
            da.Dispose();
        }
        public void carregacombo(ComboBox ComboName, string StringSQl, string Value, string Display)
        {
            carregacombo(ComboName, StringSQl, Value, Display, 99);
        }
        public void carregaList(ListBox ListName, string StringSQl, string Value, string Display)
        {
            SqlDataAdapter da = DB.Return_DataAdapter(StringSQl);
            DataTable table = new DataTable();
            da.Fill(table);
            ListName.DataSource = table;
            ListName.DisplayMember = Display;
            ListName.ValueMember = Value;
            da.Dispose();
        }
    }
}
