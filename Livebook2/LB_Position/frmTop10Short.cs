using System;
using System.Data;
using LiveDLL;



namespace LiveBook
{
    public partial class frmTop10Short : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmTop10Short()
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

        void carrega_Combo()
        {
            LiveDLL.FormUtils.LoadCombo(this.cmbView, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2 UNION ALL SELECT '-1', 'All Portfolios'", "Id_Portfolio", "Port_Name", 99);

            cmbGroup.Items.Insert(0, "Underlying");
            cmbGroup.Items.Insert(1, "Issuer");
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

            if (Item == "Underlying")
            {
                SQLString = "Select top 10 [Underlying] as Item,sum([Contribution pC])[Contribution pC],avg([P/L %])[Change],sum([Delta/NAV])[Size] " +
                            " from NESTRT.dbo.FCN_Posicao_Atual() Where [Id Portfolio]= " + Id_Portfolio + " group by [Underlying] order by [Size] asc";
            }
            else 
            {
                SQLString = "Select top 10 C.IssuerName as Item ,sum([Contribution pC])[Contribution pC],avg([P/L %])Change," +
                            " sum([Delta/NAV])[Size] from NESTRT.dbo.FCN_Posicao_Atual() A inner join Tb001_Securities B on A.[Id Ticker] = B.IdSecurity" +
                            " inner join Tb000_Issuers C on B.Id_Instituicao = C.IdIssuer" +
                            " Where [Id Portfolio]= " + Id_Portfolio + "  group by C.IssuerName order by [Size] asc";
            }
            tablep = curConn.Return_DataTable(SQLString);

            dtg.DataSource = tablep;
            

            dgTop10Desc.Columns["Contribution pC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTop10Desc.Columns["Contribution pC"].DisplayFormat.FormatString = "P2";

            dgTop10Desc.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTop10Desc.Columns["Change"].DisplayFormat.FormatString = "P2";

            dgTop10Desc.Columns["Size"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgTop10Desc.Columns["Size"].DisplayFormat.FormatString = "P2";
        }

        private void cmbView_SelectedValueChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgTop10Desc.SelectAll();
            dgTop10Desc.CopyToClipboard();
            //  MessageBox.Show("Copied!");

        }

    }
}