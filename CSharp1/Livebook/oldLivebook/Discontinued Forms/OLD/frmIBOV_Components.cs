using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using System.Data.SqlClient;
using NestDLL;

namespace SGN
{
    public partial class frmIBOV_Components : LBForm
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();

        public frmIBOV_Components()
        {
            InitializeComponent();
        }

        private void frmIBOV_Components_Load(object sender, EventArgs e)
        {
            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");

            SqlDataReader drRules;

            string SQLString = "exec sp_Get_Grupo_Nivel @Id_usuario= " + NestDLL.NUserControl.Instance.User_Id;
            drRules = CargaDados.curConn.Return_DataReader(SQLString);
            /*
            while (drRules.Read())
            {
                if ((int)drRules["Id_Grupo"] == 13)
                {
                    Flag_View = 1;
                }
            }
            */
            CargaDados.carregacombo(this.cmbView, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2  UNION ALL SELECT '-1', 'All Portfolios'", "Id_Portfolio", "Port_Name", 99);

            Carrega_Grid(Convert.ToInt32(cmbView.SelectedValue.ToString()));
            timer1.Start();
        }

        void Carrega_Grid(int Id_Portfolio)
        {
            string SQLString;
            
            DataTable tablep = new DataTable();

            SQLString = "SELECT A.Portfolio AS Portfolio,Strategy,SUM([Long Delta]) AS Long,SUM([Short Delta]) AS Short," +
                        " SUM([Gross Delta]) AS Gross,SUM([Delta Cash]/NAV) AS Net, SUM([Total P/L]/NAV) as Perf " +
                        " FROM NESTRT.dbo.FCN_Posicao_Atual() A Where A.[Id Portfolio] = 4  GROUP BY A.Portfolio,Strategy ";



            tablep = CargaDados.curConn.Return_DataTable(SQLString);

            dtg.DataSource = tablep;
            

            dgresume.Columns["Long"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgresume.Columns["Long"].DisplayFormat.FormatString = "P2";

            dgresume.Columns["Short"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgresume.Columns["Short"].DisplayFormat.FormatString = "P2";
            // gridView1.FormatConditions.Add(cn);
            dgresume.Columns["Gross"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgresume.Columns["Gross"].DisplayFormat.FormatString = "P2";
            // gridView1.FormatConditions.Add(cn);
            dgresume.Columns["Net"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgresume.Columns["Net"].DisplayFormat.FormatString = "P2";
            // gridView1.FormatConditions.Add(cn);
            dgresume.Columns["Perf"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgresume.Columns["Perf"].DisplayFormat.FormatString = "P2";
            // gridView1.FormatConditions.Add(cn);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Carrega_Grid(Convert.ToInt32(cmbView.SelectedValue.ToString()));

        }
    }
}