using System;
using System.Windows.Forms;

using LiveBook.Business;
using LiveDLL;

namespace LiveBook
{
    public partial class frmCopySecurity : Form
    {
        newNestConn curConn = new newNestConn();
        Business_Class Negocios = new Business_Class();
        LB_Utils curUtils = new LB_Utils();

        public string Id_Ativo_Inserido;
        public string Nome_Ativo_Inserido;

        public frmCopySecurity()
        {
            InitializeComponent();
        }

        private void frmCopySecurity_Load(object sender, EventArgs e)
        {
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString;
            SQLString = String.Format("SELECT * FROM Tb001_Securities_Variable WHERE NestTicker = '{0}'", txtName.Text);

            Id_Ativo_Inserido = curConn.Execute_Query_String(SQLString);

            if (Id_Ativo_Inserido != "")
            {
                MessageBox.Show("Error on Insert. This item already exists.");
                Id_Ativo_Inserido = "inserted";
                return;
            }


            if (txtName.Text != "" && txtNestTicker.Text != "")
            {
                string tempDate = "null";

                if (txtExpiration.Value.ToString("yyyyMMdd") != "19000101")
                {
                    tempDate = "'" + txtExpiration.Value.ToString("yyyyMMdd") + "'";
                }
                else
                {
                    tempDate = "null";
                }

                SQLString = "INSERT INTO Tb001_Securities_Fixed (Description,IdUnderlying,[IdPriceTable],[IdIssuer],[IdCurrency],[IdInstrument],[IdAssetClass]," +
                            " IdStrikeCurrency,IdPremiumCurrency,OptionType, RTUpdate, RTUpdateSource, HistUpdate, HistUpdateSource, HistUpdateFrequency, " +
                            " BloombergTicker,ReutersTicker,Imagineticker, BtgTicker, CyrnelTicker, ItauTicker, MellonTicker, Adminticker )" +
                            " Select Description,IdUnderlying,[IdPriceTable],[IdIssuer],[IdCurrency],[IdInstrument],[IdAssetClass], IdStrikeCurrency, IdPremiumCurrency, OptionType, " +
                            " RTUpdate, RTUpdateSource, HistUpdate, HistUpdateSource, HistUpdateFrequency, " +
                            " CASE WHEN BloombergTicker<>'' THEN 'NEW_' ELSE '' END + BloombergTicker," +
                            " CASE WHEN ReutersTicker<>'' THEN 'NEW_' ELSE '' END + ReutersTicker," +
                            " CASE WHEN Imagineticker<>'' THEN 'NEW_' ELSE '' END + Imagineticker, " +

                            " CASE WHEN BtgTicker       <>'' THEN 'NEW_' ELSE '' END + BtgTicker, " +
                            " CASE WHEN CyrnelTicker       <>'' THEN 'NEW_' ELSE '' END + CyrnelTicker, " +
                            " CASE WHEN ItauTicker      <>'' THEN 'NEW_' ELSE '' END + ItauTicker," +
                            " CASE WHEN MellonTicker    <>'' THEN 'NEW_' ELSE '' END + MellonTicker, " +
                            " CASE WHEN Adminticker     <>'' THEN 'NEW_' ELSE '' END + Adminticker " +

                            " FROM Tb001_Securities_Fixed " +
                            " Where IdSecurity = " + txtOldId.Text + ";" +
                            " SELECT @@IDENTITY";

                Id_Ativo_Inserido = curConn.Execute_Query_String(SQLString);

                if (Id_Ativo_Inserido == "")
                {
                    MessageBox.Show("Error on Insert. Please check the data you are trying to insert!");
                }
                else
                {
                    SQLString = "INSERT INTO Tb001_Securities_Variable([IdSecurity],[SecName],[NestTicker],[RoundLot],[RoundLotSize],IdPrimaryExchange,Expiration,PriceFromUnderlying, ExchangeTicker)" +
                                " SELECT Top 1 " + Id_Ativo_Inserido + ",'" + txtName.Text + "','" + txtNestTicker.Text + "',[RoundLot],[RoundLotSize],IdPrimaryExchange,'" + txtExpiration.Value.ToString("yyyyMMdd") + "',0, " +
                                "  CASE WHEN ExchangeTicker     <>'' THEN 'NEW_' ELSE '' END + ExchangeTicker " +
                                " FROM  Tb001_Securities_Variable " +
                                " Where IdSecurity = " + txtOldId.Text + " order by ValidAsOfDate desc ;";

                    int resultado = curConn.ExecuteNonQuery(SQLString);

                    MessageBox.Show("The security was inserted in the database. Please edit the previous screen to update the security specific information.");
                    this.Hide();
                }
                Nome_Ativo_Inserido = txtName.Text.ToString();
            }
            else
            {
                MessageBox.Show("Name and Nest ticker must be filled in!");
            }
            
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblNewIssuer_Click(object sender, EventArgs e)
        {
            frmIssuer InsertIssuer = new frmIssuer();
            InsertIssuer.ShowDialog(this);
        }

    }
}