using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Helpers;
namespace NestDesk
{
    public partial class Form1 : Form
    {
        public DB DB = new DB();
        SqlDataAdapter daREfresh;
        DataTable Tb;
        RefreshHelper hlprOrders;

        public Form1()
        {
            InitializeComponent();
            
            dgTicket.LookAndFeel.UseDefaultLookAndFeel = false;
            dgTicket.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dgTicket.LookAndFeel.SetSkinStyle("Blue");
            

        }
        private void Form1_Load(object sender, EventArgs e)
        {

            Carrega_Grid();
        }

       void Carrega_Grid()
        {
            string SqlString;
            SqlString = "SELECT * FROM [NESTHDESK].[dbo].[Tb000_Ticket]";
            daREfresh = new SqlDataAdapter();
            Tb = new DataTable();

            daREfresh = DB.Return_DataAdapter(SqlString);
            daREfresh.Fill(Tb);
            dgTicket.DataSource = Tb;
       

        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {

        }

        void RefreshGrid()
        {
            hlprOrders = new RefreshHelper(dtgTicket, "Id Order");
            hlprOrders.SaveViewInfo();

            Tb.Clear();
            dgTicket.BeginUpdate();

            daREfresh.Fill(Tb);

            hlprOrders.LoadViewInfo();
            dgTicket.Refresh();
            dgTicket.EndUpdate();


        }
    }

}