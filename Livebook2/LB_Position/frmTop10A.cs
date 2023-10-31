using System;
using System.Data;
using System.Drawing;
using LiveDLL;



namespace LiveBook
{
    public partial class frmBottom10Contributions : LBForm
    {

        public frmBottom10Contributions()
        {
            InitializeComponent();
        }

        private void frmTop10_Load(object sender, EventArgs e)
        {
            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");
            
            cmbView.SelectedValueChanged -= new System.EventHandler(this.cmbView_SelectedValueChanged);
            carrega_Combo();
            cmbView.SelectedValueChanged += new System.EventHandler(this.cmbView_SelectedValueChanged);
            Carrega_Grid();
            timer1.Start();
        }

        public void SetUpdateFreq(int UpdTime)
        {
            timer1.Interval = UpdTime;
        }


        void carrega_Combo()
        {

          LiveDLL.FormUtils.LoadCombo(this.cmbView, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2 UNION ALL SELECT '-1', 'All Portfolios'", "Id_Portfolio", "Port_Name", 99);
  
          cmbGroup.Items.Insert(0, "Ticker");
          cmbGroup.Items.Insert(1, "Underlying");
          cmbGroup.Items.Insert(2, "Nest Sector");
          cmbGroup.Items.Insert(3, "Security Currency");
          cmbGroup.Items.Insert(4, "Asset Class");
          cmbGroup.SelectedIndex = 0;
      }
        public void Set_Portfolio_Values(int Id_Portfolio)
        {
            cmbView.SelectedValue = Id_Portfolio;
        }

      public void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            int Id_Portfolio;
            string Item;

            Id_Portfolio = Convert.ToInt32(cmbView.SelectedValue.ToString());
            
            Item = cmbGroup.SelectedItem.ToString();


            if (Item == "Ticker")
            {
                SQLString = "Select top 10 [" + Item + "] as Item,sum([Contribution pC])[Contribution pC],avg([P/L %])[Change],sum([Delta/NAV])[Delta/NAV] " +
                            " from NESTRT.dbo.FCN_Posicao_Atual() Where [Id Portfolio]= " + Id_Portfolio + " AND [Id Book] NOT IN (5,7) group by [" + Item + "] order by [Contribution pC] asc";
            }
            else
            {
                SQLString = "Select top 10 [" + Item + "] as Item,sum([Contribution pC])[Contribution pC],CASE WHEN sum([Delta/NAV])=0 THEN 0 ELSE (sum([Contribution pC])/sum([Delta/NAV])) END [Change],sum([Delta/NAV])[Delta/NAV] " +
                            " from NESTRT.dbo.FCN_Posicao_Atual() Where [Id Portfolio]= " + Id_Portfolio + " AND [Id Book] NOT IN (5,7) group by [" + Item + "] order by [Contribution pC] asc";
            }
            using (newNestConn curConn = new newNestConn())
            {
                tablep = curConn.Return_DataTable(SQLString);


                dtg.DataSource = tablep;


                dgTop10Desc.Columns["Contribution pC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgTop10Desc.Columns["Contribution pC"].DisplayFormat.FormatString = "P2";

                dgTop10Desc.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgTop10Desc.Columns["Change"].DisplayFormat.FormatString = "+0.00%;-0.00%;-";

                dgTop10Desc.Columns["Delta/NAV"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgTop10Desc.Columns["Delta/NAV"].DisplayFormat.FormatString = "P2";
            }
        }

        private void cmbView_SelectedValueChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Carrega_Grid();

        }

        private void dgTop10Desc_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "colChange")
            {
                if (Convert.ToSingle(e.CellValue) > 0)
                {
                    e.Appearance.ForeColor = Color.Green;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if (Convert.ToSingle(e.CellValue) < 0)
                {
                    e.Appearance.ForeColor = Color.Red;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
                else if (Convert.ToSingle(e.CellValue) == 0)
                {
                    e.Appearance.ForeColor = Color.Black;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                }
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgTop10Desc.SelectAll();
            dgTop10Desc.CopyToClipboard();
            //  MessageBox.Show("Copied!");

        }
    }
}