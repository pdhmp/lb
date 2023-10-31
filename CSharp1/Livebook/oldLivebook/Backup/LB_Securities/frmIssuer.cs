using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;


namespace SGN
{
    public partial class frmIssuer : Form
    {
        CarregaDados CargaDados = new CarregaDados();

        public frmIssuer()
        {
            InitializeComponent();
        }

        private void frmIssuer_Load(object sender, EventArgs e)
        {

            CargaDados.carregacombo(this.cmbGeography, "Select Id_Geografia,Nome from Tb100_Geografia order by ordem", "Id_Geografia", "Nome", 99);

            CargaDados.carregacombo(this.cmbIndustry, "Select id_Industria,Industria from dbo.Tb101_Industria order by Industria", "id_Industria", "Industria", 99);

            CargaDados.carregacombo(this.cmbSector, "Select Id_Setor, Setor from Tb113_Setores order by Setor", "Id_Setor", "Setor", 99);

            CargaDados.carregacombo(this.cmbGics, "Select Id_Sub_Industry ,Sub_Industry from Tb205_Sub_Industry order by Sub_Industry", "Id_Sub_Industry", "Sub_Industry", 99);
            
            cmbGeography.SelectedValue = 0;
            cmbIndustry.SelectedValue = 0;
            cmbSector.SelectedValue = 27;
            cmbGics.SelectedValue = 0;
       
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString;

            if (txtName.Text != "")
            {
                SQLString = "INSERT INTO Tb000_Issuers([IdRegion], [IssuerName], [Industry], [IdNestSector], [GicsSubIndustry])" +
                            " VALUES(" + cmbGeography.SelectedValue.ToString() + ", '" + txtName.Text + "', " + cmbIndustry.SelectedValue.ToString()  +
                            ", " + cmbSector.SelectedValue.ToString() + ", " + cmbGics.SelectedValue.ToString() + ")";

                int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);;

                if (retorno == 0)
                {
                    MessageBox.Show("Error in Insert. Verify Datas!");

                }
                else
                {
                    MessageBox.Show("Inserted!");
                    Clear_Fields();
                }
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdClear_Click(object sender, EventArgs e)
        {

            Clear_Fields();
        }

        void Clear_Fields()
        {
            txtName.Text = "";
            cmbGeography.SelectedValue = 0;
            cmbIndustry.SelectedValue = 0;
            cmbSector.SelectedValue = 27;
            cmbGics.SelectedValue = 0;
        
        }

   
    }
}