using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using LiveBook.Business;
using NestDLL;

namespace LiveBook
{
    public partial class frmInsertSecurity : Form
    {
        newNestConn curConn = new newNestConn();
        Business_Class Negocios = new Business_Class();
        LB_Utils curUtils = new LB_Utils();

        public string IdSecurity_Inserted;
        public string Ticker_Inserted;

        public frmInsertSecurity()
        {
            InitializeComponent();
        }

        private void frmInsertSecurity_Load(object sender, EventArgs e)
        {
            NestDLL.FormUtils.LoadCombo(this.cmbPriceTable, "Select Id_Price_Table, Price_Table_Name from dbo.Tb024_Price_Table", "Id_Price_Table", "Price_Table_Name", 0);
            NestDLL.FormUtils.LoadCombo(this.cmbIssuer, "Select IdIssuer, IssuerName from Tb000_Issuers", "IdIssuer", "IssuerName", 99);
            NestDLL.FormUtils.LoadCombo(this.cmbCurrency, "Select Id_Moeda, Currency, CASE WHEN (Id_Moeda=900 OR Id_Moeda=1042 OR Id_Moeda=929 OR Id_Moeda=4856 OR Id_Moeda=933) THEN 1 ELSE 0 END AS ForceOrder from dbo.Vw_Moedas ORDER BY ForceOrder DESC, Currency", "Id_Moeda", "Currency", 99);
            NestDLL.FormUtils.LoadCombo(this.cmbInstrument, "Select Id_Instrumento, Instrumento from Tb029_Instrumentos", "Id_Instrumento", "Instrumento", 0);
            NestDLL.FormUtils.LoadCombo(this.cmbAsset_Class, "Select Id_Classe_Ativo, Classe_Ativo from dbo.Tb028_Classe_Ativo", "Id_Classe_Ativo", "Classe_Ativo", 0);
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString;
            
                if (txtName.Text != "" && txtNestTicker.Text != "")
                {
                    SQLString = "INSERT INTO Tb001_Securities_Fixed (Description,IdUnderlying,[IdPriceTable],[IdIssuer],[IdCurrency],[IdInstrument],[IdAssetClass],IdStrikeCurrency,IdPremiumCurrency) " +
                        " VALUES ('',0," + cmbPriceTable.SelectedValue.ToString() +
                        ", " + cmbIssuer.SelectedValue.ToString() +
                        "," + cmbCurrency.SelectedValue.ToString() +
                        "," + cmbInstrument.SelectedValue.ToString() +
                        "," + cmbAsset_Class.SelectedValue.ToString() + ",0,0); " +
                        "SELECT @@IDENTITY";

                    IdSecurity_Inserted = curConn.Execute_Query_String(SQLString);

                    if (IdSecurity_Inserted == "")
                    {
                        MessageBox.Show("Error on Insert. Please check the data you are trying to insert!");
                    }
                    else
                    {
                        SQLString = "INSERT INTO Tb001_Securities_Variable([IdSecurity],[SecName],[NestTicker],[RoundLot],[RoundLotSize],IdPrimaryExchange,PriceFromUnderlying)" +
                        " VALUES (" + IdSecurity_Inserted + ", '" + txtName.Text + "','" + txtNestTicker.Text + "',1,100,0,0)";

                        int retorno = curConn.ExecuteNonQuery(SQLString);
                        if (retorno != 0)
                        {
                            MessageBox.Show("The security was inserted in the database. Please edit the previous screen to update the security specific information.");
                            this.Hide();
                            Ticker_Inserted = txtName.Text.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Error on Insert. Please check the data you are trying to insert!");
                        }
                    }
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
            NestDLL.FormUtils.LoadCombo(this.cmbIssuer, "Select IdIssuer, IssuerName from Tb000_Issuers", "IdIssuer", "IssuerName", 99);
        }

    }
}