using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using SGN.CargaDados;
using SGN.Business;
using SGN.Validacao;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

using System.Runtime.InteropServices;
using System.Threading;

namespace SGN
{
    public partial class frmTop10Long2 : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        public int Id_usuario;
        bool processing;
        bool ThreadRunning;
        DataTable tempTable = new DataTable();

        string tempStringSQL;
        private CheckBox checkBox1;
        string StringSQL;

        // WinApi Beep() is in kernel32.dll    
        [DllImport("kernel32")]
        private static extern int Beep(int dwFreq, int dwDuration);

        public frmTop10Long2()
        {
            InitializeComponent();
        }

        private void frmTop10_Load(object sender, EventArgs e)
        {
            CargaDados.DB.User_Id = Id_usuario;

            processing = false;

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
            SqlDataReader drRules;

            string StringSQl = "exec sp_Get_Grupo_Nivel @Id_usuario= " + Id_usuario;
            drRules = CargaDados.DB.Execute_Query_DataRead(StringSQl);

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
            int Id_Portfolio;
            string Item;

            Id_Portfolio = Convert.ToInt32(cmbView.SelectedValue.ToString());

            Item = cmbGroup.SelectedItem.ToString();

            if (Item == "Underlying")
            {
                tempStringSQL = "SELECT [Underlying] as Item, " +
                            "SUM([Contribution pC])[Contribution pC],  " +
                            "dbo.FCN_GETD_RT_Value_Only(MAX([Id Underlying]), 1, 0, 0)/dbo.FCN_GET_PRICE_Value_Only(MAX([Id Underlying]), getdate(),1,0,1,0,0)-1 [Change], " +
                            "SUM([Delta/NAV])[Size]   " +
                            "from NESTRT.dbo.FCN_Posicao_Atual() " +
                            "WHERE [Id Portfolio]= " + Id_Portfolio + " group by [Underlying] order by [Size] desc";
            }
            else
            {
                tempStringSQL = "Select C.Nome as Item ,sum([Contribution pC])[Contribution pC], 0 Change, " +
                            " sum([Delta/NAV])[Size] from NESTRT.dbo.FCN_Posicao_Atual() A  inner join Tb001_Ativos B on A.[Id Ticker] = B.Id_Ativo" +
                            " inner join Tb000_Instituicoes C on B.Id_Instituicao = C.Id_Instituicao" +
                            " WHERE [Id Portfolio]= " + Id_Portfolio + "  group by C.Nome order by [Size] desc";
            }

            if (chkEquityOnly.Checked || chkOther.Checked)
            {
                string filterSQL = "[Id Asset Class]=-1";
                if (chkEquityOnly.Checked) { filterSQL = filterSQL + "OR [Id Asset Class]=1"; }
                if (chkOther.Checked) { filterSQL = filterSQL + "OR [Id Asset Class]<>1"; }
                tempStringSQL = tempStringSQL.Replace("WHERE", "WHERE (" + filterSQL + ") AND ");
            }

            StringSQL = tempStringSQL;
            dtg.DataSource = tempTable;

            if (dgTop10Desc.Columns.Count > 0)
            {
                dgTop10Desc.Columns["Contribution pC"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgTop10Desc.Columns["Contribution pC"].DisplayFormat.FormatString = "P2";

                dgTop10Desc.Columns["Change"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgTop10Desc.Columns["Change"].DisplayFormat.FormatString = "P2";

                dgTop10Desc.Columns["Size"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                dgTop10Desc.Columns["Size"].DisplayFormat.FormatString = "P2";
            }
        }

        private void ExecSQL()
        {
            SqlDataAdapter dp = new SqlDataAdapter();
            DataTable ThreadTable = new DataTable();

            ThreadRunning = true;

            dp = CargaDados.DB.Return_DataAdapter(StringSQL);

            try
            {
                dp.Fill(ThreadTable);
                tempTable = ThreadTable;
            }
            catch { }
            dp.Dispose();

            ThreadRunning = false;

        }


        private void frmTop10_FormClosing(object sender, FormClosingEventArgs e)
        {
            Valida.Save_Properties_Form(this, Id_usuario, 0);

        }

        private void cmbView_SelectedValueChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!ThreadRunning)
            {
                System.Threading.Thread t1;
                t1 = new System.Threading.Thread(new ThreadStart(ExecSQL));
                t1.Start();
            }
            if (!processing)
            {
                Carrega_Grid();
            };
        }

        private void dtg_MouseUp(object sender, MouseEventArgs e)
        {
            processing = false;

        }

        private void dtg_MouseDown(object sender, MouseEventArgs e)
        {
            processing = true;
        }
        
    }
}