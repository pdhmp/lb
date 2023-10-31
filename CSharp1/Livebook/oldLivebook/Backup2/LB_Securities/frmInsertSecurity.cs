using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using SGN.Business;
using NestDLL;

namespace SGN
{
    public partial class frmInsertSecurity : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();

        public string IdSecurity_Inserted;
        public string Ticker_Inserted;

        public frmInsertSecurity()
        {
            InitializeComponent();
        }

        private void frmInsertSecurity_Load(object sender, EventArgs e)
        {
            CargaDados.carregacombo(this.cmbPriceTable, "Select Id_Price_Table, Price_Table_Name from dbo.Tb024_Price_Table", "Id_Price_Table", "Price_Table_Name", 0);
            CargaDados.carregacombo(this.cmbIssuer, "Select IdIssuer, IssuerName from Tb000_Issuers", "IdIssuer", "IssuerName", 99);
            CargaDados.carregacombo(this.cmbCurrency, "Select Id_Moeda, Currency, CASE WHEN (Id_Moeda=900 OR Id_Moeda=1042 OR Id_Moeda=929 OR Id_Moeda=4856 OR Id_Moeda=933) THEN 1 ELSE 0 END AS ForceOrder from dbo.Vw_Moedas ORDER BY ForceOrder DESC, Currency", "Id_Moeda", "Currency", 99);
            CargaDados.carregacombo(this.cmbInstrument, "Select Id_Instrumento, Instrumento from Tb029_Instrumentos", "Id_Instrumento", "Instrumento", 0);
            CargaDados.carregacombo(this.cmbAsset_Class, "Select Id_Classe_Ativo, Classe_Ativo from dbo.Tb028_Classe_Ativo", "Id_Classe_Ativo", "Classe_Ativo", 0);
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString;

            int flag_new_Table = 1;

            if (flag_new_Table == 0)
            {
                if (txtName.Text != "" && txtNestTicker.Text != "")
                {
                    SQLString = "INSERT INTO ZTb001_Ativos (Id_Price_Table,Nome, Simbolo,Id_Instituicao,Moeda,Id_Instrumento,Id_Classe_Ativo, Lote_Negociacao, Lote_Padrao) " +
                        " VALUES (" + cmbPriceTable.SelectedValue.ToString() +
                        ", '" + txtName.Text + "'" +
                        ", '" + txtNestTicker.Text + "'" +
                        ", " + cmbIssuer.SelectedValue.ToString() +
                        "," + cmbCurrency.SelectedValue.ToString() +
                        "," + cmbInstrument.SelectedValue.ToString() +
                        "," + cmbAsset_Class.SelectedValue.ToString() +
                        ",100, 1);" +
                        "SELECT @@IDENTITY";

                    IdSecurity_Inserted = CargaDados.curConn.Execute_Query_String(SQLString);

                    if (IdSecurity_Inserted == "")
                    {
                        MessageBox.Show("Error on Insert. Please check the data you are trying to insert!");
                    }
                    else
                    {
                        SQLString = "UPDATE ZTb001_Ativos SET Ativo_Objeto=" + IdSecurity_Inserted + " WHERE Id_Ativo=" + IdSecurity_Inserted;
                        string resultado = CargaDados.curConn.Execute_Query_String(SQLString);
                        MessageBox.Show("The security was inserted in the database. Please edit the previous screen to update the security specific information.");
                        this.Hide();
                    }
                    Ticker_Inserted = txtName.Text.ToString();
                }
                else
                {
                    MessageBox.Show("Name and Nest ticker must be filled in!");
                }
            }
            else
            {
                if (txtName.Text != "" && txtNestTicker.Text != "")
                {
                    SQLString = "INSERT INTO Tb001_Securities_Fixed (Description,IdUnderlying,[IdPriceTable],[IdIssuer],[IdCurrency],[IdInstrument],[IdAssetClass],IdStrikeCurrency,IdPremiumCurrency) " +
                        " VALUES ('',0," + cmbPriceTable.SelectedValue.ToString() +
                        ", " + cmbIssuer.SelectedValue.ToString() +
                        "," + cmbCurrency.SelectedValue.ToString() +
                        "," + cmbInstrument.SelectedValue.ToString() +
                        "," + cmbAsset_Class.SelectedValue.ToString() + ",0,0); " +
                        "SELECT @@IDENTITY";

                    IdSecurity_Inserted = CargaDados.curConn.Execute_Query_String(SQLString);

                    if (IdSecurity_Inserted == "")
                    {
                        MessageBox.Show("Error on Insert. Please check the data you are trying to insert!");
                    }
                    else
                    {
                        SQLString = "INSERT INTO Tb001_Securities_Variable([IdSecurity],[SecName],[NestTicker],[RoundLot],[RoundLotSize],IdPrimaryExchange)" +
                        " VALUES (" + IdSecurity_Inserted + ", '" + txtName.Text + "','" + txtNestTicker.Text + "',1,100,0)";

                        int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString);
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
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void lblNewIssuer_Click(object sender, EventArgs e)
        {
            frmIssuer InsertIssuer = new frmIssuer();
            InsertIssuer.ShowDialog(this);
            CargaDados.carregacombo(this.cmbIssuer, "Select IdIssuer, IssuerName from Tb000_Issuers", "IdIssuer", "IssuerName", 99);
        }

    }
}