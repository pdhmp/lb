using System;
using System.Data;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmEditLimitsArb : LBForm
    {
        public frmEditLimitsArb()
        {
            InitializeComponent();
        }

        private void frmBrokers_Load(object sender, EventArgs e)
        {
            Carrega_Lista();
        }

        public void Carrega_Lista()
        {
            LiveDLL.FormUtils.LoadCombo(cmbd_StressType, "SELECT IdStressType,StressType FROM NESTDB.dbo.Tb821_StressType (nolock)", "IdStressType", "StressType");

            LiveDLL.FormUtils.LoadCombo(cmbPar2, "SELECT IdSecurity,NestTicker FROM NESTDB.dbo.Tb001_Securities (nolock) WHERE IdInstrument NOT IN (3, 11, 12) ORDER BY NestTicker", "IdSecurity", "NestTicker");
            LiveDLL.FormUtils.LoadCombo(cmbPar3, "SELECT IdSecurity,NestTicker FROM NESTDB.dbo.Tb001_Securities (nolock) WHERE IdInstrument NOT IN (3, 11, 12) ORDER BY NestTicker", "IdSecurity", "NestTicker");
            
            string SQLString = "Select distinct([Id Section]) as IdSection,Section FROM " +
                                " NESTRT.dbo.Tb000_Posicao_Atual (nolock) " + 
                                " WHERE [Id POrtfolio]=38 and [Id Book] in (6,10)";

             using (newNestConn curConn = new newNestConn())
            {
                DataTable table = curConn.Return_DataTable(SQLString);

                lstSection.DataSource = table;
                lstSection.ValueMember = "IdSection";
                lstSection.DisplayMember = "Section";
            }
        }
        
        void LoadSection ()
        {
            ReloadlstAvDates();
        }

         void ReloadlstAvDates()
        {
            int IdSection=0;

            IdSection = Convert.ToInt32(lstSection.SelectedValue.ToString());
            using (newNestConn curConn = new newNestConn())
            {
                this.lstAvDates.SelectedIndexChanged -= new System.EventHandler(this.lstAvDates_SelectedIndexChanged);
                lstAvDates.SelectedIndex = -1;
                LiveDLL.FormUtils.LoadList(lstAvDates, "SELECT ValidDate,ValidDate AS Valid2 FROM NESTDB.dbo.Tb820_ArbStress (nolock) WHERE IdSection=" + IdSection + " ORDER BY ValidDate", "ValidDate", "ValidDate");
                this.lstAvDates.SelectedIndexChanged += new System.EventHandler(this.lstAvDates_SelectedIndexChanged);
                lstAvDates.SelectedIndex = lstAvDates.Items.Count - 1;
                lstAvDates_SelectedIndexChanged(this, new EventArgs());
            }
        }

        private void lstAvDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdSection = 0;

            IdSection = Convert.ToInt32(lstSection.SelectedValue.ToString());

            if (lstAvDates.SelectedIndex != -1)
            {
                LoadStress(IdSection, Convert.ToDateTime(lstAvDates.Text));
            }
            else
            {
                txtd_StressValue.Text = "";
            }
        }

       void LoadStress(int IdSection, DateTime ValidDate)
        {

            string SQLString = "SELECT * FROM NESTDB.dbo.[FCN_Risk_StressType_Param] (" + IdSection.ToString() + ",'" + ValidDate.ToString("yyyyMMdd") + "')";

            using (newNestConn curConn = new newNestConn())
            {
                DataTable tablep = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in tablep.Rows)
                {
                   txtd_StressValue.Text = curRow["StressValue"].ToString();
                   cmbd_StressType.SelectedValue = curRow["IdStressType"].ToString();
                   dtpEndDate.Value = LiveDLL.Utils.ParseToDateTime(curRow["EndDate"]);
                   dtpIniDate.Value = LiveDLL.Utils.ParseToDateTime(curRow["IniDate"]);
                   grpOPA1.Visible = false;

                   lblPar1.Text = "";
                   lblPar2.Text = "";
                   lblPar3.Text = "";

                   txtPar1.Text = "";
                   cmbPar2.Text = "";
                   cmbPar3.Text = "";

                   if ((int)curRow["IdStressType"] == 2 || (int)curRow["IdStressType"] == 3)
                   {
                       grpOPA1.Visible = true;
                       lblPar1.Text = curRow["ParName1"].ToString();
                       lblPar2.Text = curRow["ParName2"].ToString();
                       lblPar3.Text = curRow["ParName3"].ToString();

                       txtPar1.Text = curRow["ParValue1"].ToString();
                       cmbPar2.SelectedValue = curRow["ParValue2"].ToString();
                       cmbPar3.SelectedValue = curRow["ParValue3"].ToString();

                   }
                }
            }


        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            int retorno = UpdateData();

            

        }

        int UpdateData()
        {
            string StringSQL;
            int retorno = 0;

            if (lstAvDates.SelectedIndex != -1)
            {
                if (cmbd_StressType.SelectedValue.ToString() == "2" || cmbd_StressType.SelectedValue.ToString() == "3")
                {
                    if (txtPar1.Text == "") { return 99; }

                    StringSQL = " UPDATE [NESTDB].[dbo].Tb820_ArbStress " +
                            "   SET  StressValue = " + txtd_StressValue.Text.Replace(".", "").Replace(",", ".") +
                            "      , StressType  = " + cmbd_StressType.SelectedValue.ToString() +
                            "      , IniDate = '" + dtpIniDate.Value.ToString("yyyy-MM-dd") + "'" +
                            "      , EndDate = '" + dtpEndDate.Value.ToString("yyyy-MM-dd") + "'" +
                            "      , Par1 = " + txtPar1.Text.Replace(".", "").Replace(",", ".") +
                            "      , Par2 = " + cmbPar2.SelectedValue +
                            "      , Par3 = " + cmbPar3.SelectedValue + 
                            " WHERE IdSection=" + lstSection.SelectedValue.ToString() + " AND ValidDate= '" + Convert.ToDateTime(lstAvDates.Text).ToString("yyyyMMdd") + "' ";
                }
                else
                {
                    StringSQL = " UPDATE [NESTDB].[dbo].Tb820_ArbStress " +
                            "   SET  StressValue = " + txtd_StressValue.Text.Replace(".", "").Replace(",", ".") +
                            "      , StressType  = " + cmbd_StressType.SelectedValue.ToString() +
                            "      , IniDate = '" + dtpIniDate.Value.ToString("yyyy-MM-dd") + "'" +
                            "      , EndDate = '" + dtpEndDate.Value.ToString("yyyy-MM-dd") + "'" +
                            " WHERE IdSection=" + lstSection.SelectedValue.ToString() + " AND ValidDate= '" + Convert.ToDateTime(lstAvDates.Text).ToString("yyyyMMdd") + "' ";
                }
                using (newNestConn curConn = new newNestConn())
                {
                    retorno = curConn.ExecuteNonQuery(StringSQL);

                    if (retorno == 99)
                    {
                        MessageBox.Show("Error on update. Check Data.");
                    }
                    else
                    {
                        MessageBox.Show("Updated.");
                    }
                }
            }
            return retorno;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void label2_Click(object sender, EventArgs e)
        {
            frmAddSecurityDate Data = new frmAddSecurityDate();
            Data.ShowDialog();

            DateTime TradeDate = Convert.ToDateTime(Data.Tag);
            int retorno;
            int IdSection;
            int IdStressType;

            if (TradeDate.ToString("yyyy-MM-dd") != "1900-01-01")
            {

                if (Int32.TryParse(lstSection.SelectedValue.ToString(), out IdSection) != false)
                {
                    IdSection = Convert.ToInt32(lstSection.SelectedValue.ToString());
                }

                if (Int32.TryParse(cmbd_StressType.SelectedValue.ToString(), out IdStressType) != false)
                {
                    IdStressType = Convert.ToInt32(cmbd_StressType.SelectedValue.ToString());
                }

                string StressValue = txtd_StressValue.Text.ToString().Replace(".", "").Replace(",", ".");

                if (StressValue == "") { StressValue = "0"; }

                double txt1Value = 0;
                double.TryParse(txtPar1.Text.ToString(), out txt1Value);



                string StringSQL = "INSERT INTO NESTDB.dbo.Tb820_ArbStress " +
                " ([IdSection],[ValidDate],[StressType],[StressValue],Par1,Par2,Par3,IniDate,EndDate)" +
                " VALUES (" + IdSection.ToString() + ",'" + TradeDate.ToString("yyyyMMdd") + "'," + IdStressType + "," + StressValue + "," + txt1Value.ToString().Replace(".", "").Replace(",", ".") + "," + cmbPar2.SelectedValue.ToString() + "," + cmbPar3.SelectedValue.ToString() + ",'" + dtpIniDate.Value.ToString("yyyyMMdd") + "','" + dtpEndDate.Value.ToString("yyyyMMdd") + "')";

                using (newNestConn curConn = new newNestConn())
                {
                    retorno = curConn.ExecuteNonQuery(StringSQL);
                }
                ReloadlstAvDates();
            }
        }

        private void lstSection_SelectedIndexChanged(object sender, EventArgs e)
        {
            int IdSection = 0;
            if (Int32.TryParse(lstSection.SelectedValue.ToString(), out IdSection) != false)
            {
                IdSection = Convert.ToInt32(lstSection.SelectedValue.ToString());
            }

            if (IdSection != 0)
            {
                LoadSection();
            }
        }
        private void cmbd_StressType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbd_StressType.SelectedValue.ToString() == "2")
            {
                LoadOPA1();
            }
        }


       void LoadOPA1()
        {
           using (newNestConn curConn = new newNestConn())
           {
               grpOPA1.Visible = true;

               string StringSQL = "SELECT * FROM dbo.Tb821_StressType WHERE IdStressType=2";

               DataTable tablep = curConn.Return_DataTable(StringSQL);

               foreach (DataRow curRow in tablep.Rows)
               {
                   lblPar1.Text = curRow["Par1"].ToString();
                   lblPar2.Text = curRow["Par2"].ToString();
                   lblPar3.Text = curRow["Par3"].ToString();
               }
           }

        }
    }
}
