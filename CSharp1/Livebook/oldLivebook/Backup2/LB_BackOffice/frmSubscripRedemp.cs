using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using SGN.Business;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace SGN
{
    public partial class frmSubsRedemp : Form
    {
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
        string Transaction_Type_Message = "";

        public frmSubsRedemp()
        {
            InitializeComponent();
        }

        private void frmDividends_Load(object sender, EventArgs e)
        {
            
            dtgSubRedemp.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgSubRedemp.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgSubRedemp.LookAndFeel.SetSkinStyle("Blue");

            Load_Combos();
            
            Carrega_Grid();
        }

        void Load_Combos()
        {
            CargaDados.carregacombo(this.cmbFund, "Select Id_Portfolio,Port_Name from Tb002_Portfolios Where Id_Port_Type=2 and Discountinued=0 order by Port_Name", "Id_Portfolio", "Port_Name", 99);
            CargaDados.carregacombo(this.cmbTransType, "Select Id_Trans_Type, Description from dbo.Tb701_Transaction_Types WHERE Id_Trans_Type IN (30,31,32) order by Description", "Id_Trans_Type", "Description", 21);
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtShare.Text == "") { txtShare.Text = "0000"; };
            if (txtCash.Text == "") { txtCash.Text = "0"; };

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());
            int Id_Account = 0;
            int Id_Ticker = 0;
            switch (Id_Fund)
            {
                case 4:
                    Id_Account=1060;
                    Id_Ticker = 5228;
                    break;
                case 43:
                    Id_Account=1086;
                    Id_Ticker = 4803;
                    break;
                case 10:
                    Id_Account = 1073;
                    Id_Ticker = 4946;
                    break;
                default:
                    Id_Account = 0;
                    Id_Ticker = 0;
                    break;
            }

            if (Valida.IsNumeric(txtCash.Text) || Valida.IsNumeric(txtShare))
            {
                string SQLString = "INSERT INTO [NESTDB].[dbo].[Tb711_SubscripRedemp]([Id_Portfolio],Id_Account,Id_Ticker,[Trade_Date],[Conversion_Date],[Payment_Date],[Transaction_Type],[Nominal_Amount],[Share_Amount])" +
                                    "VALUES ( " + Id_Fund + "," + Id_Account + "," + Id_Ticker +",'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "','" + dtpConversion.Value.ToString("yyyyMMdd") + "','" + dtpPayment.Value.ToString("yyyyMMdd") + "'," + cmbTransType.SelectedValue.ToString() + ", " + txtCash.Text.Replace(".", "").Replace(",", ".") + ", " + txtShare.Text.Replace(",", ".") + ")";

                int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
                if (retorno != 0 && retorno != 99)
                {
                    MessageBox.Show(Transaction_Type_Message);
                }
                else
                {
                    MessageBox.Show("Error on Insert!");
                }

                Carrega_Grid();
                txtCash.Text = "";
                txtShare.Text = "";
            }
        }

       private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {

            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid);
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            Valida.Save_Columns(tempGrid);
        }
        
        private void Carrega_Grid()
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            dgSubRedemp.Columns.Clear();

            SQLString = "SELECT * FROM NESTDB.dbo.FCN_Retur_SubsRedemp()";

            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtgSubRedemp.DataSource = tablep;
            
            tablep.Dispose();

            // Delete Button
            dgSubRedemp.Columns["Share Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgSubRedemp.Columns["Share Amount"].DisplayFormat.FormatString = "#,##0.0000;(#,##0.0000)";

            dgSubRedemp.Columns["Cash Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgSubRedemp.Columns["Cash Amount"].DisplayFormat.FormatString = "#,##0;(#,##0)";

            dgSubRedemp.Columns["Payment Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgSubRedemp.Columns["Payment Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgSubRedemp.Columns["Payment Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            dgSubRedemp.Columns["Conversion Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgSubRedemp.Columns["Conversion Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgSubRedemp.Columns["Conversion Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            dgSubRedemp.Columns["Trade Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            dgSubRedemp.Columns["Trade Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dgSubRedemp.Columns["Trade Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            Valida.SetColumnStyle(dgSubRedemp, 1);

        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            if (Valida.IsNumeric(this.txtCash.Text) )
            {
                decimal Cash = Convert.ToDecimal(txtCash.Text);
                this.txtCash.Text = Cash.ToString("##,##0");
            }

        }

        private void cmbTransType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate_Date();
        }

        void Validate_Date()
        {
 
            if ( cmbTransType.SelectedValue != null && cmbFund.SelectedValue != null)
            {
                if (Valida.IsNumeric(cmbTransType.SelectedValue.ToString()) && Valida.IsNumeric(cmbFund.SelectedValue.ToString()))
                {
                    grpDate.Enabled = true;
                    int Transaction_Type = Convert.ToInt32(cmbTransType.SelectedValue.ToString());

                  int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());
                  int Region;
                    if (Id_Fund == 4)
                    {
                        Region = 4;
                    }
                    else
                    {
                        Region = 1;
                    }

                    if (Transaction_Type == 30)
                    {
                        Transaction_Type_Message = "Subscription Inserted!";
                        //next day
                        dtpConversion.Value = dtpTradeDate.Value; //Convert.ToDateTime(CargaDados.DB.Execute_Query_String("Select NESTDB.dbo.FCN_NDATEADD('du',1,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "',2," + Region + ")"));
                        dtpPayment.Value = dtpTradeDate.Value;
                    }

                    if (Transaction_Type == 31 || Transaction_Type == 32)
                    {
                        Transaction_Type_Message = "Redemption Inserted!";
                        dtpConversion.Value = dtpTradeDate.Value;
                        dtpPayment.Value = Convert.ToDateTime(CargaDados.curConn.Execute_Query_String("Select NESTDB.dbo.FCN_NDATEADD('du',3,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "',2," + Region + ")"));
                        //3 days
                    }
                }
            }
        
        }

        private void cmbFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate_Date();
        }
     }
}