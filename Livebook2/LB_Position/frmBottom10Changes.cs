using System;
using System.Data;
using LiveDLL;

namespace LiveBook
{
    public partial class frmBottom10Changes : LBForm
    {
        newNestConn curConn = new newNestConn();

        public frmBottom10Changes()
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

            Id_Portfolio = Convert.ToInt32(cmbView.SelectedValue.ToString());


            SQLString = "Select top 10 Ticker,sum([Contribution pC])[Contribution pC],avg([P/L %])[Change],sum([Delta/NAV])[Size] " +
                        " from NESTRT.dbo.FCN_Posicao_Atual() Where [Id Portfolio]= " + Id_Portfolio + " and [Id Ticker Type] <> 7 " +
                        " group by Ticker order by Change asc";

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgTop10Desc.SelectAll();
            dgTop10Desc.CopyToClipboard();
            //  MessageBox.Show("Copied!");

        }

    }
}