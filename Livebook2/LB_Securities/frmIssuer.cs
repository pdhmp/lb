using System;
using System.Windows.Forms;
using LiveDLL;


namespace LiveBook
{
    public partial class frmIssuer : Form
    {
        newNestConn curConn = new newNestConn();

        public frmIssuer()
        {
            InitializeComponent();
        }

        private void frmIssuer_Load(object sender, EventArgs e)
        {

            LiveDLL.FormUtils.LoadCombo(this.cmbGeography, "Select Id_Geografia,Nome from Tb100_Geografia order by ordem", "Id_Geografia", "Nome", 99);

            LiveDLL.FormUtils.LoadCombo(this.cmbSector, "Select Id_Setor, Setor from Tb113_Setores order by Setor", "Id_Setor", "Setor", 99);

            LiveDLL.FormUtils.LoadCombo(this.cmbGics, "Select Id_Sub_Industry ,Sub_Industry from Tb205_Sub_Industry order by Sub_Industry", "Id_Sub_Industry", "Sub_Industry", 99);
            
            cmbGeography.SelectedValue = 0;
            cmbSector.SelectedValue = 27;
            cmbGics.SelectedValue = 0;
       
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            string SQLString;

            if (txtName.Text != "")
            {
                SQLString = "INSERT INTO Tb000_Issuers([IdRegion], [IssuerName], [IdNestSector], [GicsSubIndustry])" +
                            " VALUES(" + cmbGeography.SelectedValue.ToString() + ", '" + txtName.Text + "', " +
                            " " + cmbSector.SelectedValue.ToString() + ", " + cmbGics.SelectedValue.ToString() + ")";

                int retorno = curConn.ExecuteNonQuery(SQLString, 1);;

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
            cmbSector.SelectedValue = 27;
            cmbGics.SelectedValue = 0;
        
        }

   
    }
}