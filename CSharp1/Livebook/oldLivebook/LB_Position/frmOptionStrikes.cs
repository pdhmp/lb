using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestDLL;
using System.Data.SqlClient;

namespace LiveBook
{
    public partial class frmOptionStrikes : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmOptionStrikes()
        {
            InitializeComponent();
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }


        private void frmOptionStrikes_Load(object sender, EventArgs e)
        {
            

            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            Carrega_Grid();
            timer1.Start();

            cmbView.SelectedIndexChanged -= new System.EventHandler(this.cmbView_SelectedIndexChanged);
            cmbGroup.SelectedIndexChanged -= new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            cmbSide.SelectedIndexChanged -= new System.EventHandler(this.cmbSide_SelectedIndexChanged);
            carrega_Combo();
            cmbView.SelectedIndexChanged += new System.EventHandler(this.cmbView_SelectedIndexChanged);
            cmbGroup.SelectedIndexChanged += new System.EventHandler(this.cmbGroup_SelectedIndexChanged);
            cmbSide.SelectedIndexChanged += new System.EventHandler(this.cmbSide_SelectedIndexChanged);
        }

        void Carrega_Grid()
        {
            if (cmbView.SelectedValue != null && cmbSide.SelectedValue != null && cmbGroup.SelectedValue != null)
            {
                int Id_Portfolio;
                int OptSide;
                int OptType;

                string SQLString;
                
                DataTable tablep = new DataTable();

                Id_Portfolio = Convert.ToInt32(cmbView.SelectedValue.ToString());
                OptSide = Convert.ToInt32(cmbSide.SelectedValue.ToString());
                OptType = Convert.ToInt32(cmbGroup.SelectedValue.ToString());

                SQLString = "SELECT * FROM [FCN_Option_Strikes] (" + Id_Portfolio + ", " + OptType + ", " + OptSide + ")" +
                    "UNION " +
                    "SELECT 1, SUM(AllDates), SUM([Apr-28]), SUM([May-16]), SUM([May-18]), SUM([Jun-15]), SUM([Jun-20]), SUM([Sep-19]) FROM [FCN_Option_Strikes] (" + Id_Portfolio + ", " + OptType + ", " + OptSide + ")";

                tablep = curConn.Return_DataTable(SQLString);

                dtg.DataSource = tablep;
                

                int c = 0;

                foreach (DataColumn col in tablep.Columns)
                {
                    dgresume.Columns[c].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    dgresume.Columns[c].DisplayFormat.FormatString = "#,#0.00%;(#,#0.00%);-";;
                    c++;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        public void Set_Portfolio_Values(int Id_Portfolio)
        {
            cmbView.SelectedValue = Id_Portfolio;
        }
        void carrega_Combo()
        {
            NestDLL.FormUtils.LoadCombo(this.cmbView, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2 UNION ALL SELECT '-1', 'All Portfolios'", "Id_Portfolio", "Port_Name", 99);

            
            NestDLL.FormUtils.LoadCombo(this.cmbGroup, "SELECT -1 AS Id_Type, 'All' AS Description UNION SELECT 1, 'Calls' UNION SELECT 0, 'Puts'", "Id_Type", "Description", -1);
            NestDLL.FormUtils.LoadCombo(this.cmbSide, "SELECT -1 AS Id_Side, 'Net' AS Description UNION SELECT 1, 'Long' UNION SELECT 2, 'Short'", "Id_Side", "Description", -1);
            
            cmbGroup.SelectedIndex = 0;
            cmbSide.SelectedIndex = 0;
        }

        private void cmbSide_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }
        private void cmbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }
    }
}