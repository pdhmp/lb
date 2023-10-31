using System;
using System.Collections;
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

namespace SGN
{
    public partial class frmMenu : Form
    {
        public class myReverserClass : IComparer
        {
            // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
            int IComparer.Compare(Object x, Object y)
            {
                return ((new CaseInsensitiveComparer()).Compare(y, x));
            }

        }
        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        public int Id_usuario;
        
        //variaveis p/ calcular Parte do Patrimonio
        double somatoria_Pos;
        double somatoria_Pos_Delta;

        double somatoria_Neg;
        double somatoria_Neg_Delta;
        
        double PL;
        
        double exposicao;
        double exposicao_delta;

        double Net;
        double Net_Delta;
        
        double Percent_Soma_Pos;
        double Percent_Soma_Pos_Delta;  
        
        double Percent_Soma_Neg;
        double Percent_Soma_Neg_Delta;

        double Percent_exposicao;
        double Percent_exposicao_Delta;

        double Percent_Net;
        double Percent_Net_Delta;

        double Tot_MTM ;
        double Tot_MTM_PL;

        int Expandable;

        DataTable tablep = new DataTable();
        SqlDataAdapter dp = new SqlDataAdapter();
        public frmMenu()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
           // System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyNegativePattern;
           // InitializeComponent();
           // String StrinSQl = "SET LANGUAGE 'US_ENGLISH'";
          //  int lingua;
          //  lingua = Valida.DB.Execute_Insert_Delete_Update(StrinSQl);
        }
        public void frmMenu_Load(object sender, EventArgs e)
        {
            Carrega_form();
            cmbChoosePortfolio_SelectedValueChanged(sender, e);
        }
        public void Carrega_form()
        {
            Valida_Menu(Id_usuario);
            //CARTEIRAS
            cmbChoosePortfolio.SelectedValueChanged -= new System.EventHandler(this.cmbChoosePortfolio_SelectedValueChanged);
            if (Id_usuario == 1)
            {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  Tb002_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira", 4);
            }
            else
            {
                CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  VW_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira",99);
            }
            cmbChoosePortfolio.SelectedValueChanged += new System.EventHandler(this.cmbChoosePortfolio_SelectedValueChanged);
            Carrega_Insert_order();
            //Carrega_Grid();
            //ativar timer
            // tmrCarrega_Ordens.Start();
           // this.dgPositions = new Mobius.Utility.SortedDataGridView();


        }
       // private Mobius.Utility.SortedDataGridView dgPositions;
        public void Carrega_Insert_order()
        {
            Carrega_Portfolio();
            //Tipo de Mercado - Normal, after...
            CargaDados.carregacombo(this.cmbOrder_Type, "Select Id_Tipo_Mercado,Tipo_Mercado from  Tb114_Tipo_Mercados", "Id_Tipo_Mercado", "Tipo_Mercado", 99);

            //Ativos
            //CargaDados.carregacombo(this.cmbTicker, "Select Id_Ativo, Simbolo from VW001_Ativos order by Simbolo", "Id_Ativo", "Simbolo", 99);
            if (Id_usuario == 1 || Id_usuario == 9)
            {
                //Ativos
                CargaDados.carregacombo(this.cmbTicker, "Select Id_Ativo, Simbolo from Tb001_Ativos order by Simbolo", "Id_Ativo", "Simbolo", 99);
            }
            else
            {
                //Ativos
                CargaDados.carregacombo(this.cmbTicker, "Select Id_Ativo, Simbolo from VW001_Ativos order by Simbolo", "Id_Ativo", "Simbolo", 99);
            }
            //Estrategia
            CargaDados.carregacombo(this.cmbStrategy, "Select Id_Estrategia, Estrategia from Tb111_Estrategia", "Id_Estrategia", "Estrategia", 99);

            Carrega_Sub_Estrategy();
            //Brooker
            CargaDados.carregacombo(this.cmBrooker, "Select Id_Corretora,Nome from Tb011_Corretoras", "Id_Corretora", "Nome", 99);
    }
        public void Carrega_Sub_Estrategy()
        {
            int valor;
            try
            {
                valor = Convert.ToInt32(cmbStrategy.SelectedValue);
                //SubEstrategia
                CargaDados.carregacombo(this.cmbSub_Strategy, "Select Id_Sub_Estrategia,Sub_Estrategia, Id_Estrategia from Tb112_Sub_Estrategia where Id_Estrategia =" + this.cmbStrategy.SelectedValue, "Id_Sub_Estrategia", "Sub_Estrategia", 99);
            }
            catch
            {

            }
        }

        public void Carrega_Portfolio()
        {
            try
            {
                //SubEstrategia
                //CARTEIRAS
                if (Id_usuario == 1)
                {
                    CargaDados.carregacombo(this.cmbportfolio, "Select Id_Carteira,Carteira from  Tb002_Carteiras where insere_Ordem <> 0", "Id_Carteira", "Carteira", Convert.ToInt32(cmbChoosePortfolio.SelectedValue));
                }
                else
                {
                    CargaDados.carregacombo(this.cmbportfolio, "Select Id_Carteira,Carteira from  VW_Carteiras where insere_Ordem <> 0", "Id_Carteira", "Carteira", Convert.ToInt32(cmbChoosePortfolio.SelectedValue));
                }
                }
            catch
            {
                MessageBox.Show("Error when loading the Combo Portfólios ");
            }
        }

        private void frmMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            int resposta;
            resposta = Convert.ToInt32(MessageBox.Show("Do you really want to Logoff?", "Orders", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

            if (resposta == 6)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
            
        }

        private void Valida_Menu(int Id_usuario)
            {
                string StringSQl;
                SqlDataReader drRules;
                StringSQl = "SET DATEFORMAT DMY;exec sp_Get_Grupo_Nivel @Id_usuario= " + Id_usuario;

                drRules = CargaDados.DB.Execute_Query_DataRead(StringSQl);

                while (drRules.Read())
                {
                    switch (Convert.ToInt32(drRules["Id_Grupo"]))
                  {
                        case 1:
                            //1 Administrador
                            grpOrdem.Enabled = true;
                            //pLToolStripMenuItem.Enabled = true;
                            //mnuOrdens.Enabled = true;

                            // ALTERAR
                             //dgAbertas.Columns[0].Visible = true;
                            //dgAbertas.Columns[1].Visible = true;
                            
                            testeToolStripMenuItem.Visible = true;
                            break;
                        case 2:
                            //2 Trader
                        
                            // ALTERAR
                            //dgAbertas.Columns[0].Visible = true;
                            //dgAbertas.Columns[1].Visible = true;
                           
                        
                            //dgTrades.Columns[0].Visible = true;
                           // dgTrades.Columns[1].Visible = true;
                            //inserirOrdemToolStripMenuItem.Visible = true;
                            grpOrdem.Enabled = true;
                            //mnuOrdens.Enabled = true;
                            //pLToolStripMenuItem.Enabled = true;
                            //mnuOrdens.Enabled = true;
                            //inserirPLToolStripMenuItem.Enabled = true;
                            break;
                        case 3:
                            //3 Middle
                            //grpOrdem.Enabled = true;
                            //pLToolStripMenuItem.Enabled = true;

                            break;
                        case 4:
                            //4 Consulta
                            break;
                    }
                }
                drRules.Close();
            drRules.Dispose();
            }
        private void Valida_Colunas(int Id_usuario, DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {

            string StringSQl;
            SqlDataReader drRules;
            StringSQl = "SET DATEFORMAT DMY;exec sp_Get_Grupo_Nivel @Id_usuario= " + Id_usuario;

            drRules = CargaDados.DB.Execute_Query_DataRead(StringSQl);

            while (drRules.Read())
            { 
                if (Nome_Grid.Name == "dgAbertas")
                {
                    switch (Convert.ToInt32(drRules["Id_Grupo"]))
                    {
                        case 1:
                            //1 Administrador
                            
                            //dgAbertas.Columns["Edit"].Visible = true;
                            //dgAbertas.Columns["Edit"].VisibleIndex = 0;
                            dgAbertas.Columns["Cancel"].Visible = false;
                            dgAbertas.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 2:
                            //2 Trader
                            //dgAbertas.Columns["Edit"].Visible = true;
                            //dgAbertas.Columns["Edit"].VisibleIndex = 0;
                            dgAbertas.Columns["Cancel"].Visible = false;
                            dgAbertas.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 3:
                            break;
                        case 4:
                            //4 Consulta
                            break;
                    }
                }

                if (Nome_Grid.Name == "dgTrades")
                {
                    switch (Convert.ToInt32(drRules["Id_Grupo"]))
                    {
                        case 1:
                            //1 Administrador
                            dgTrades.Columns["Edit"].Visible = true;
                            dgTrades.Columns["Edit"].VisibleIndex = 0;
                            dgTrades.Columns["Cancel"].Visible = true;
                            dgTrades.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 2:
                            //2 Trader
                            dgTrades.Columns["Edit"].Visible = true;
                            dgTrades.Columns["Edit"].VisibleIndex = 0;
                            dgTrades.Columns["Cancel"].Visible = true;
                            dgTrades.Columns["Cancel"].VisibleIndex = 1;
                            break;
                        case 3:
                            //3 Middle
                            break;
                        case 4:
                            //4 Consulta
                            break;
                    }
                }

            }
            drRules.Close();
            drRules.Dispose();

        }


        public void Carrega_Grid()
            //função q pega o tab selecionado e carrega o grid dele
        {
            this.Cursor = Cursors.WaitCursor;

          //  DataGridTableStyle ts = new DataGridTableStyle();
            string data_now = DateTime.Now.ToString("yyyyMMdd");

            ////////////////////////
            try
            {

            //////////////////////// 
            ////////////////////////
            string StringSQl;
            string String_Campos = " * ";
            //string data_now = DateTime.Now.ToString("yyyyMMdd");

            somatoria_Pos = 0;
            somatoria_Pos_Delta = 0;

            somatoria_Neg = 0;
            somatoria_Neg_Delta = 0; 

            PL = 0;

            exposicao = 0;
            exposicao_delta = 0;

            Net = 0;
            Net_Delta = 0;

            Percent_Soma_Pos = 0;
            Percent_Soma_Pos_Delta = 0;

            Percent_Soma_Neg = 0;
            Percent_Soma_Neg_Delta = 0;

            Percent_exposicao = 0;
            Percent_exposicao_Delta = 0;

            Percent_Net = 0;
            Percent_Net_Delta = 0;

            Tot_MTM = 0;
            Tot_MTM_PL = 0;

                
                SqlDataReader REad_Pl;
                switch (tbMenu.SelectedTab.Text)
                {
                    case "Positions":

                        if (this.cmbChoosePortfolio.SelectedValue.ToString() != null)
                        {
                            //Sorti = tablep.DefaultView.Sort;
                            int pos = this.BindingContext[tablep].Position;

                            tablep.Clear();
                            tablep = new DataTable();

                            String_Campos = "select Z.Data_PL as Data_PL,Z.Valor_PL as Valor_PL From Tb025_Valor_PL Z" +
                             "  inner join ( select Id_Carteira, max(Data_PL) as Data_PL" +
                             " From Tb025_Valor_PL group by Id_Carteira) mxDrv " +
                             " on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Carteira = Z.Id_Carteira Where Z.Id_Carteira =" + cmbChoosePortfolio.SelectedValue;

                            REad_Pl = CargaDados.DB.Execute_Query_DataRead(String_Campos);
                            if (! REad_Pl.Read())
                            {
                                PL = 0;
                            }
                            else
                            {
                                string data_PL1 = REad_Pl["Data_PL"].ToString();
                                if (Convert.ToInt32(cmbChoosePortfolio.SelectedValue) == 5 || Convert.ToInt32(cmbChoosePortfolio.SelectedValue) == 6)
                                {
                                    if (Convert.ToInt32(cmbChoosePortfolio.SelectedValue) == 5)
                                    {
                                        String_Campos = "select Z.Valor_PL * dbo.FCN_Convert_Moedas(1042,900,Z.Data_PL) as Valor_PL" +
                                        " From Tb025_Valor_PL Z  inner join ( select Id_Carteira, max(Data_PL) as Data_PL From Tb025_Valor_PL " +
                                        "  group by Id_Carteira) mxDrv on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Carteira = Z.Id_Carteira Where Z.Id_Carteira = 4";
                                        PL = Convert.ToDouble(CargaDados.DB.Execute_Query_String(String_Campos));

                                    }
                                    else
                                    {
                                        String_Campos = "select Z.Valor_PL as Valor_PL" +
                                        " From Tb025_Valor_PL Z  inner join ( select Id_Carteira, max(Data_PL) as Data_PL From Tb025_Valor_PL " +
                                        "  group by Id_Carteira) mxDrv on Mxdrv.Data_PL = Z.Data_PL and mxDrv.Id_Carteira = Z.Id_Carteira Where Z.Id_Carteira = 4";
                                        PL = Convert.ToDouble(CargaDados.DB.Execute_Query_String(String_Campos));
                                    }
                                }
                                else
                                {
                                    PL = Convert.ToDouble(REad_Pl["Valor_PL"].ToString());
                                }
                                REad_Pl.Close();
                                REad_Pl.Dispose();

                                DateTime data_PL;
                                if (data_PL1 != "")
                                {
                                    data_PL = Convert.ToDateTime(data_PL1);
                                }
                                else
                                {
                                    data_PL = Convert.ToDateTime(DateTime.Now.ToString("yyyyMMdd"));
                                    return;
                                }
                                string Data_Mod1 = data_PL.ToString("yyyyMMdd");
                                string Data_Mod2 = data_now;
                                //Inicio Abertas - tbopen

                                String_Campos = "Select coalesce(Carteira_OBJeto,Id_Carteira) as Carteira_OBJeto from Tb002_Carteiras Where Id_Carteira =  " + this.cmbChoosePortfolio.SelectedValue.ToString();
                                string Id_Carteira = CargaDados.DB.Execute_Query_String(String_Campos);

                               // String_Campos = Valida.Retorna_ordem(Id_usuario, 4);

                                if (String_Campos != "")
                                {
                                    String_Campos = "Select Moeda from Tb002_Carteiras where Id_Carteira = " + this.cmbChoosePortfolio.SelectedValue.ToString();
                                   string Moeda = CargaDados.DB.Execute_Query_String(String_Campos);

                                   String_Campos = "SET LANGUAGE 'US_ENGLISH'; Select * from [Fcn_LB1_Calcula_Posicao](" + Id_Carteira + ",'" + Data_Mod1 + "','" + Data_Mod2 + "'," + this.cmbChoosePortfolio.SelectedValue.ToString() + "," + Moeda + ") Where [Id Portfolio] = " + this.cmbChoosePortfolio.SelectedValue.ToString() + " order by [Delta Cash] desc ,[Ticker] asc";

                                    dp = CargaDados.DB.Return_DataAdapter(String_Campos);
                                    dp.Fill(tablep);
                                    //tablep.DefaultView.Sort = sort;
                                   // this.BindingContext[tablep].Position = pos;
                                    dtg.DataSource = tablep;

                                    dp.Dispose();
                                    if (tablep.Rows.Count > 0 )
                                    {
                                        /* LONG */
                                        if (tablep.Compute("Sum(Cash)", "Cash > 0").ToString() != "")
                                        {
                                            somatoria_Pos = Convert.ToDouble(tablep.Compute("Sum(Cash)", "Cash > 0"));
                                            Percent_Soma_Pos = somatoria_Pos / PL;
                                        }
                                        else
                                        {
                                            somatoria_Pos = 0;
                                            Percent_Soma_Pos = 0;
                                        }
                                        /* LONG DELTA  */
                                        if (tablep.Compute("Sum([Delta Cash])", "[Delta Cash] > 0  and [Asset Class] = 'Equity'").ToString() != "")
                                        {
                                            somatoria_Pos_Delta = Convert.ToDouble(tablep.Compute("Sum([Delta Cash])", "[Delta Cash] > 0  and [Asset Class] = 'Equity'"));
                                            Percent_Soma_Pos_Delta = somatoria_Pos_Delta / PL;
                                        }
                                        else
                                        {
                                            somatoria_Pos_Delta = 0;
                                            Percent_Soma_Pos_Delta = 0;
                                        }

                                        /* SHORT  */
                                        if (tablep.Compute("Sum(Cash)", "Cash < 0").ToString() != "")
                                        {
                                            somatoria_Neg = Convert.ToDouble(tablep.Compute("Sum(Cash)", "Cash < 0"));
                                            Percent_Soma_Neg = somatoria_Neg / PL;

                                        }
                                        else
                                        {
                                            somatoria_Neg = 0;
                                            Percent_Soma_Neg = 0;
                                        }

                                        /* SHORT DELTA  */
                                        if (tablep.Compute("Sum([Delta Cash])", "[Delta Cash] < 0  and [Asset Class] = 'Equity'").ToString() != "")
                                        {
                                            somatoria_Neg_Delta = Convert.ToDouble(tablep.Compute("Sum([Delta Cash])", "[Delta Cash] < 0  and [Asset Class] = 'Equity'"));
                                            Percent_Soma_Neg_Delta = somatoria_Neg_Delta / PL;
                                        }
                                        else
                                        {
                                            somatoria_Neg_Delta = 0;
                                            Percent_Soma_Neg_Delta = 0;
                                        }

                                        /* EXPOSIÇÃO */
                                        if (somatoria_Neg != 0 && somatoria_Pos != 0)
                                        {
                                            exposicao = Math.Abs(somatoria_Pos) + Math.Abs(somatoria_Neg);
                                            Net = Math.Abs(somatoria_Pos) - Math.Abs(somatoria_Neg);
                                            Percent_exposicao = exposicao / PL;
                                            Percent_Net = Net / PL;
                                        }

                                        /* EXPOSIÇÃO DELTA */
                                        if (somatoria_Neg_Delta != 0 && somatoria_Pos_Delta != 0)
                                        {
                                            exposicao_delta = Math.Abs(somatoria_Pos_Delta) + Math.Abs(somatoria_Neg_Delta);
                                            Net_Delta = Math.Abs(somatoria_Pos_Delta) - Math.Abs(somatoria_Neg_Delta);

                                            Percent_exposicao_Delta = exposicao_delta / PL;
                                            Percent_Net_Delta = Net_Delta / PL;
                                        }
                                               if (tablep.Compute("Sum([Total P/L])", "[Total P/L]<>0").ToString() != "")
                                                {
                                                    Tot_MTM = Convert.ToDouble(tablep.Compute("Sum([Total P/L])", "[Total P/L]<>0"));
                                                    //Tot_MTM_PL = Tot_MTM;
                                                    //Tot_MTM = Tot_MTM / PL;

                                                    //Tot_MTM_PL = Convert.ToDouble(tablep.Compute("Sum([Total P/L])", "[Total P/L] <> 0 "));
                                                    txtmtm.Text = Tot_MTM.ToString("#,##0.00");

                                                    Tot_MTM_PL = Tot_MTM / PL;
                                                }
                                                else
                                                {
                                                    txtmtm.Text = "";
                                                    Tot_MTM = 0;
                                                    Tot_MTM_PL = 0;
                                                }
                                         //}
                                    }
                                    else
                                    {
                                    somatoria_Pos =0;
                                    somatoria_Pos_Delta = 0;

                                    somatoria_Neg = 0;
                                    somatoria_Neg_Delta = 0;                                    

                                    exposicao = 0;
                                    exposicao_delta = 0;                                

                                    Net = 0;
                                    Net_Delta = 0;

                                    Percent_Soma_Pos = 0;
                                    Percent_Soma_Pos_Delta = 0;

                                    Percent_Soma_Neg = 0;
                                    Percent_Soma_Neg_Delta = 0;

                                    Percent_exposicao = 0;
                                    Percent_exposicao_Delta = 0;

                                    Percent_Net = 0;
                                    Percent_Net_Delta = 0;

                                    Tot_MTM = 0;
                                    txtmtm.Text = "";

                                    Tot_MTM_PL = 0;

                                    }

                                  //  Valida.Formatar_Grid(dgPositions, "Delta Cash");
                                    tablep.Dispose();
                                    // this.dgPositions.ColumnWidthChanged -= new DataGridViewColumnEventHandler(this.dgPositions_ColumnWidthChanged);
                                    
                                    //retorno = Valida.Carregar_Estilo_Colunas(dgPositions, Id_usuario);

                                   // this.dgPositions.ColumnWidthChanged += new DataGridViewColumnEventHandler(this.dgPositions_ColumnWidthChanged);
                                    Valida.SetColumnStyle(dgPositions, Id_usuario, "Delta Cash");
                                    Valida_Colunas(Id_usuario, dgPositions);
                                    dgPositions.RowHeight = 15;
                                }
                                /*********************************************/
                            }
                            Calcula_Patrimonio();
                        }
                            break;
                        
                    case "Open":
                        dgAbertas.Columns.Clear();  
                    //  String_Campos = Valida.Retorna_ordem(Id_usuario, 1);
                     //Inicio Abertas - tbopen
                        SqlDataAdapter da = new SqlDataAdapter();
                        DataTable table = new DataTable();


                    //StringSQl = " Select * from VW_Ordens_abertas order by Ticker asc";
                        //StringSQl = " Select top 10 * from VW_Ordens_abertas_Teste order by Ticker asc";
                        StringSQl = " Select Done,Leaves,[Avg Price Trade],[Id Order],[Id Ticker], Ticker,Total," +
                                    " [Order Price],[Cash Order],[Ticker Currency],[Order Date],[Login],[Id Strategy],Strategy," +
                                    " [Id Sub Strategy],[Sub Strategy] [Order Status],[Id Portfolio],[Portfolio]," +
                                    " [Broker] from  dbo.VW_Group_Ordens_Trades_EN" +
                                    " WHERE [Order Date]='" + data_now + "' AND [Id Order Status] NOT IN (3, 4, 5)" +
                                    " order by [Id Ticker]";

                        da = CargaDados.DB.Return_DataAdapter(StringSQl);
                        da.Fill(table);
                        dtg2.DataSource = table;
                        da.Dispose();
                         table.Dispose();
                        //dgAbertas.Columns.AddField("Edit");
                        // dgAbertas.Columns["Edit"].VisibleIndex = 0;
                        // dgAbertas.Columns["Edit"].Width = 25;

                         dgAbertas.Columns.AddField("Cancel");
                         dgAbertas.Columns["Cancel"].VisibleIndex = 0;
                         dgAbertas.Columns["Cancel"].Width = 55;
                         RepositoryItemButtonEdit item = new RepositoryItemButtonEdit();
                         item.Buttons[0].Tag = 1;
                         item.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                         item.Buttons[0].Caption = "Cancel";
                         item.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(item_ButtonClick);
                         dtg2.RepositoryItems.Add(item);
                         dgAbertas.Columns["Cancel"].ColumnEdit = item;
                         dgAbertas.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                         dgAbertas.Columns["Cancel"].Visible = false;

                         //dgAbertas.Columns.AddField("Edit");
                         //dgAbertas.Columns["Edit"].VisibleIndex = 0;
                         //dgAbertas.Columns["Edit"].Width = 55;
                         //RepositoryItemButtonEdit item2 = new RepositoryItemButtonEdit();
                         //item2.Buttons[0].Tag = 1;
                         //item2.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                         //item2.Buttons[0].Caption = "Edit";
                        // item2.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(item2_ButtonClick);
                         //dtg2.RepositoryItems.Add(item2);
                         //dgAbertas.Columns["Edit"].ColumnEdit = item2;
                         //dgAbertas.Columns["Edit"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                         dgAbertas.OptionsBehavior.Editable = false;
                        // dgAbertas.Columns["Edit"].Visible = false;
                         Valida_Colunas(Id_usuario, dgAbertas);
                         Valida.SetColumnStyle(dgAbertas, Id_usuario, "Total");

                        break;
                    //Fim Abertas

                    case "Done":
                      //  String_Campos = Valida.Retorna_ordem(Id_usuario, 2);
                        SqlDataAdapter dc = new SqlDataAdapter();
                        DataTable tablec = new DataTable();

                        //StringSQl = " Select * from VW_Ordens_Fechadas where [TimeStampf]='" + data_now + "' order by Ticker asc";

                        StringSQl = "  Select Done,Leaves,[Avg Price Trade],[Id Order],[Id Ticker], Ticker,Total," +
                                    " [Order Price],[Cash Order],[Ticker Currency],[Order Date],[Login],[Id Strategy],Strategy,[Id Sub Strategy],[Sub Strategy]" +
                                    " [Order Status],[Id Portfolio],[Portfolio], [Broker]" +
                                    " from  dbo.VW_Group_Ordens_Trades_EN" +
                                    " WHERE [Order Date]='" + data_now + "' AND [Id Order Status] =3" +
                                    " order by [Id Ticker]";

                        dc = CargaDados.DB.Return_DataAdapter(StringSQl);
                        dc.Fill(tablec);
                        dtgFec.DataSource = tablec;
                            dc.Dispose();
                        tablec.Dispose();
                        //Valida.Formatar_Grid(dgFechadas, "Quantity");
                        Valida.SetColumnStyle(dgFechadas, Id_usuario, "Total");
                        Valida_Colunas(Id_usuario, dgFechadas);
                        break;
                                                                                     
                    case "Cancel":
                       // String_Campos = Valida.Retorna_ordem(Id_usuario, 3);
                        SqlDataAdapter dl = new SqlDataAdapter();
                        DataTable tablel = new DataTable();

                       // StringSQl = " Select * from VW_Ordens_Canceladas where [TimeStampf]='" + data_now + "' order by Ticker asc";
                        StringSQl = " select * from  dbo.VW_Group_Ordens_Trades_EN " +
                                     " WHERE [Order Date]='" + data_now + "' AND [Id Order Status] = 4" +
                                     " order by [Id Ticker]";

                        
                        dl = CargaDados.DB.Return_DataAdapter(StringSQl);
                        dl.Fill(tablel);
                        
                        dtgCancel.DataSource = tablel;
                        dl.Dispose();
                        tablel.Dispose();
                       // Valida.Formatar_Grid(dgCanceladas, "Quantity");
                        Valida.SetColumnStyle(dgCanceladas, Id_usuario, "Total");
                        Valida_Colunas(Id_usuario, dgCanceladas);
                        break;
                    case "Trades":
                        dgTrades.Columns.Clear(); 
                        //String_Campos = Valida.Retorna_ordem(Id_usuario, 5);
                        SqlDataAdapter dt = new SqlDataAdapter();
                        DataTable tablet = new DataTable();
                        //StringSQl = "Select * from VW_Trades_Day";
                        
                        StringSQl = "Select [Id Trade],[Id Order],Ticker,[Trade Quantity],[Trade Price],[Cash],Rebate,Login,Strategy," +
                            " [Sub Strategy],[Round Lot],Portfolio,[Ticker Currency], [Status Trade],[Trade Date]," +
                            " [Id Ticker] from dbo.Vw_Ordens_Trades_En" +
                            " Where [Trade Date]='" + data_now + "' and [Status Trade]<>4";

                        
                        
                        dt = CargaDados.DB.Return_DataAdapter(StringSQl);
                        dt.Fill(tablet);

                        dtgTrade.DataSource = tablet;
                        dt.Dispose();
                        tablet.Dispose();
                        Valida.SetColumnStyle(dgTrades, Id_usuario, "Trade Quantity");

                        dgTrades.Columns.AddField("Cancel");
                        dgTrades.Columns["Cancel"].VisibleIndex = 0;
                        dgTrades.Columns["Cancel"].Width = 55;
                        RepositoryItemButtonEdit item3 = new RepositoryItemButtonEdit();
                        item3.Buttons[0].Tag = 1;
                        item3.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                        item3.Buttons[0].Caption = "Cancel";
                        dtg2.RepositoryItems.Add(item3);
                        dgTrades.Columns["Cancel"].ColumnEdit = item3;
                        dgTrades.Columns["Cancel"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                        dgTrades.OptionsBehavior.Editable = false;
                        dgTrades.Columns["Cancel"].Visible = false;

                        dgTrades.Columns.AddField("Edit");
                        dgTrades.Columns["Edit"].VisibleIndex = 0;
                        dgTrades.Columns["Edit"].Width = 55;
                        RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
                        item4.Buttons[0].Tag = 1;
                        item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                        item4.Buttons[0].Caption = "Edit";
                        dtg2.RepositoryItems.Add(item4);
                        dgTrades.Columns["Edit"].ColumnEdit = item4;
                        dgTrades.Columns["Edit"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                        dgTrades.OptionsBehavior.Editable = false;
                        dgTrades.Columns["Edit"].Visible = false;
                        Valida_Colunas(Id_usuario, dgTrades);
                        Valida.SetColumnStyle(dgTrades, Id_usuario, "Quantity");

                        break;
                 }
}
            finally
            {
                this.Cursor = Cursors.Default;
                

            }

            }

        void Calcula_Patrimonio()
        {
            this.txtPL.Text = PL.ToString("#,##0.00");
            
            this.txtLong.Text = somatoria_Pos.ToString("#,##0.00");
            this.txtLongDelta.Text = somatoria_Pos_Delta.ToString("#,##0.00");

            this.txtShort.Text = somatoria_Neg.ToString("#,##0.00");
            this.txtShort_Delta.Text = somatoria_Neg_Delta.ToString("#,##0.00");

            this.txtEx.Text = exposicao.ToString("#,##0.00");
            this.txtEx_Delta.Text = exposicao_delta.ToString("#,##0.00");

            this.txtNet.Text = Net.ToString("#,##0.00");
            this.txtNet_Delta.Text = Net_Delta.ToString("#,##0.00");

            Percent_Soma_Neg = Math.Abs(Percent_Soma_Neg);
            Percent_Soma_Neg_Delta = Math.Abs(Percent_Soma_Neg_Delta);
 
            this.txt_p_Long.Text = Percent_Soma_Pos.ToString("p").Replace("%","");
            this.txt_p_Long_Delta.Text = Percent_Soma_Pos_Delta.ToString("p").Replace("%", "");            
            
            this.txt_p_Short.Text = Percent_Soma_Neg.ToString("p").Replace("%", "");
            this.txt_p_Short_Delta.Text = Percent_Soma_Neg_Delta.ToString("p").Replace("%", "");

            this.txt_p_Ex.Text = Percent_exposicao.ToString("p").Replace("%", "");
            this.txt_p_Ex_Delta.Text = Percent_exposicao_Delta.ToString("p").Replace("%", "");

            this.txt_p_Net.Text = Percent_Net.ToString("p").Replace("%", "");
            this.txt_p_Net_Delta.Text = Percent_Net_Delta.ToString("p").Replace("%", "");

            this.txtmtm_perc.Text = Tot_MTM_PL.ToString("p").Replace("%", "");
        
        }
        
        private void tbMenu_click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void dgAbertas_CellContentClick(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            //int Id_ordem;
           // if (e.RowIndex != -1)
            {
          //  Id_ordem = Convert.ToInt32(dgAbertas.Rows[e.RowIndex].Cells["Id Order"].Value.ToString());

           // Opcoes_Abertas( e.ColumnIndex,Id_ordem, Id_usuario);

            }
            //DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e



        }

  private void Opcoes_Abertas(int Indice, double Id_ordem, int Id_usuario)
{
    int resultado;
            switch (Indice)
            {
                case 0:

                    resultado = Negocios.Cancela_Ordem(Id_ordem);
                    if (resultado  != 0)
                    {
                        Carrega_Grid();
                    }
                      ;
                    break;
                case 1:
                   Negocios.Editar_Ordem(Id_ordem);
                        Carrega_Grid();
                    ;
                    break;
                default:

                    if (Negocios.Inserir_Trade(Id_ordem, Id_usuario) == 1)
                    {
                        Carrega_Grid();
                    }
                    ;
                    break;
            }
}

        private void tmrCarrega_Ordens_Tick(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

          private void tradesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCaixa CashReport = new frmCaixa();
            CashReport.Id_usuario = Id_usuario;
            CashReport.Show();
        }

          private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
              this.Close();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPass Pass = new frmPass();
            Pass.Id_Usuario = Id_usuario;
            Pass.ShowDialog();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            Insert_order();
        }

        private void Insert_order()
        {
            int Id_Ativo;
            //////////////
            if (this.txtPrice.Text == "")
            {
                txtPrice.Text = "0";
                this.txtCash.Text = "0";
            }
            cmbTicker.Focus();

            if (this.cmbTicker.SelectedValue == null)
            {

                MessageBox.Show("Ticker Not Registered. Wait the new version!");
                //função de cadastrar opção, futuro ou ativo
            }
            else
            {
                Id_Ativo = Convert.ToInt32(this.cmbTicker.SelectedValue.ToString());
            }

            if (this.txtQtd.Text != "")
            {
                String SqlString = "Select Lote_Negociacao from Tb001_Ativos Where Id_Ativo =" + cmbTicker.SelectedValue.ToString();
                
                Decimal Lote_Negociacao = Convert.ToDecimal(CargaDados.DB.Execute_Query_String(SqlString));

                string divisao = Convert.ToString(Convert.ToDouble(this.txtQtd.Text) / Convert.ToDouble(Lote_Negociacao));
                decimal Quantidade;


                Quantidade = Convert.ToDecimal(this.txtQtd.Text);
                
                //int teste;

                //if (int.TryParse(divisao, out teste))
                //{
                    switch (Negocios.Insert_Order(0, false, Convert.ToInt32(this.cmbportfolio.SelectedValue.ToString()), Convert.ToInt32(this.cmbTicker.SelectedValue.ToString()), Quantidade, Convert.ToDecimal(this.txtPrice.Text), this.dtpExpiration.Value.ToString("yyyy-MM-dd"), Convert.ToInt32(this.cmBrooker.SelectedValue.ToString()), Convert.ToInt32(this.cmbStrategy.SelectedValue.ToString()), Convert.ToInt32(this.cmbSub_Strategy.SelectedValue.ToString()), Convert.ToInt32(this.cmbOrder_Type.SelectedValue.ToString()), Id_usuario))
                    {
                        case 1:
                            if (tbMenu.SelectedTab.Text == "Open")
                            {
                                Carrega_Grid();
                            }
                            this.cmbOrder_Type.SelectedValue = 1;
                            this.dtpExpiration.Value = Convert.ToDateTime(DateTime.Now);
                            this.txtQtd.Text = "";
                            this.txtCash.Text = "";
                            break;
                        case 0:
                            MessageBox.Show("Verifies the data of Insertion!");
                            break;
                        default:
                            break;
                    }
                   // MessageBox.Show("Inserted order!");
                //}
                //else
               // {
                  //  MessageBox.Show("This amount is a Fractionary Lot, will not be possible to insert the Order!");
               // }
            }
            else
            {
                MessageBox.Show("Insert a Valid Amount!");
            }
        }

        private void chkGtc_CheckedChanged(object sender, EventArgs e)
        {

            dtpExpiration.Enabled = !dtpExpiration.Enabled;
            //if (this.chkGtc.Checked)
            //{
           //     this.dtpExpiration.Enabled = false;
           // }
           // else
           // {
          //      this.dtpExpiration.Enabled = true;
          //  }

        }

        private void txtPrice_TextChanged(object sender, EventArgs e)
        {
            try
            {
               // decimal texto = Convert.ToDecimal(txtQtd.Text);
                //txtQtd.Text = texto.ToString("##,###.00");
                calcula_VF("qtd");
            }
            catch
            {

            }
        }
        
         private void txtPrice_Leave(object sender, EventArgs e)
        {
            try
            {
                decimal testa_pos;
                testa_pos = Convert.ToDecimal(this.txtQtd.Text);
                if (testa_pos < 0)
                {
                    cmdInsert_Order_Neg.Enabled = true;
                    cmdInsert_Order.Enabled = false;
                }
                else
                {
                    cmdInsert_Order_Neg.Enabled = false;
                    cmdInsert_Order.Enabled = true;
                }

                if (this.txtPrice.Text != "")
                {
                    decimal Preco = Convert.ToDecimal(txtPrice.Text);
                    this.txtPrice.Text = Preco.ToString("##,###.00#######");
                }
                if (this.txtQtd.Text != "")
                {
                   decimal qtd = Convert.ToDecimal(this.txtQtd.Text);
                   this.txtQtd.Text = qtd.ToString("##,###.00#######");
                }

                if (this.txtCash.Text != "")
                {
                    decimal vfin = Convert.ToDecimal(this.txtCash.Text);
                    this.txtCash.Text = vfin.ToString("##,###.00#######");
                }
            }
            catch
            {
            
            }
        }
        private void calcula_VF(string sender)
        {
            String SqlString;
           decimal Lote_Padrao;
            if (sender.ToString() == "qtd")
            {
                decimal VF;
                decimal QTD;
                if (decimal.TryParse(txtQtd.Text, out QTD))
                {
                    SqlString = "Select Lote_Padrao from VW001_Ativos Where Id_Ativo =" + cmbTicker.SelectedValue.ToString();
                    Lote_Padrao = Convert.ToDecimal(CargaDados.DB.Execute_Query_String(SqlString));

                    VF = (Convert.ToDecimal(txtPrice.Text) * (QTD/ Lote_Padrao));
                    txtCash.Text = Convert.ToString(VF);
                }
                else
                {
                    txtCash.Text = "";
                }
            }
        }

        private void txtpress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ProcessTabKey(true);
        }

        private void inserirOrdemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Valida.Verifica_Acesso(Id_usuario, 2) == true)
            {
                frmInsertOrdem InsertOrdem = new frmInsertOrdem();
                InsertOrdem.Id_usuario = Id_usuario;
                InsertOrdem.ShowDialog();
                Carrega_Grid();
            }
            else
            {
                MessageBox.Show("You do not have authorization to insert an Order!");
            }

        }

        private void chkMarket_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPrice.Enabled = !txtPrice.Enabled;
        }

        private void dgTrades_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show("Function not qualified!");
        }

        private void cmbChoosePortfolio_SelectedValueChanged(object sender, EventArgs e)
        {
            Carrega_Grid();
            this.cmbportfolio.SelectedValue = this.cmbChoosePortfolio.SelectedValue;
            /*
            dgPositions.BeginSort();
            dgPositions.Columns["Asset Class"].GroupIndex = 0;
            dgPositions.EndSort();
            dgPositions.Focus();
        
             */
        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            //dgPositions.Refresh();
            Carrega_Grid();
        }

        private void inserirPLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmpl PL = new frmpl();
            PL.Id_usuario = Id_usuario;
            PL.ShowDialog();
        }

        private void frmMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

         private void configTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmcoluns Colunas = new frmcoluns();
            Colunas.Id_usuario = Id_usuario;
            Colunas.ShowDialog();
            Carrega_Grid();
        }

        private void Header_Click(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Valida.Formatar_Grid(dtg, "Delta Cash");
            /*
            Valida.Formatar_Grid(dgPositions, "Position");
            if (dgPositions.SortOrder.ToString() == "Ascending")
            {
                sort = "ASC";
            }
            else
            {
                sort = "DESC";
            }

            
            if (Sorti[0] != null)
            {
                Sorti[3] = Sorti[2];
                Sorti[2] = Sorti[1];
                Sorti[1] = Sorti[0];
               // Sorti[0] = "[" + dgPositions.Columns[e.ColumnIndex].Name.ToString() + "] " + sort;
                Sorti[0] =  tablep.DefaultView.Sort + " " + sort;
            }
            else
            {
                Sorti[0] = tablep.DefaultView.Sort + " " + sort;
            }

            sort = Sorti[0];
            if (Sorti[1] != null)
                sort = sort + "," + Sorti[1];
            if (Sorti[2] != null)
                sort = sort + "," + Sorti[2];
            if (Sorti[3] != null)
                sort = sort + "," + Sorti[3];
            //lstSort.Text = sort;
           // MessageBox.Show(sort);
           // Carrega_Grid();

          //  tablep.DefaultView.Sort = sort;
            //dgPositions.Columns.Clear();
           // dgPositions.DataSource = tablep;
            // Sorti[0] + "," + Sorti[1] + "," + Sorti[2] + "," + Sorti[3]

             //IComparer myComparer = new myReverserClass();

           // dgPositions.Sort(myComparer);
            */
        }

        private void cmdInsert_Order_Neg_Click(object sender, EventArgs e)
        {
            Insert_order();
        }

        private void insertVolatilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVol Fmrvol = new frmVol();
            Fmrvol.Id_Portfolio = Convert.ToInt32(cmbChoosePortfolio.SelectedValue.ToString());
            Fmrvol.ShowDialog();
        }

        private void aDRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdr Adr = new frmAdr();
            Adr.ShowDialog();
            Carrega_Insert_order();
        }

        private void onshoreStocksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAcoes Onshore = new frmAcoes();
            Onshore.ShowDialog();
            Carrega_Insert_order();

        }

        private void commoditiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCommodities Commodities = new frmCommodities();
            Commodities.ShowDialog();
            Carrega_Insert_order();

        }

        private void currencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCurrency Currency = new frmCurrency();
            Currency.ShowDialog();
            Carrega_Insert_order();

        }

        private void futuresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFuturos Futuro = new frmFuturos();
            Futuro.ShowDialog();
            Carrega_Insert_order();

        }

        private void indexToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmIndices Indice = new frmIndices();
            Indice.ShowDialog();
            Carrega_Insert_order();

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOpcoes Opcoes = new frmOpcoes();
            Opcoes.ShowDialog();
            Carrega_Insert_order();

        }

        private void sWAPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEdit Editar = new frmEdit();
            Editar.ShowDialog();
            
            //frmSwap Swap = new frmSwap();
            //Swap.ShowDialog();
            //Carrega_Insert_order();

        }

        private void issuerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssuer Issuer = new frmIssuer();
            Issuer.ShowDialog();
            Carrega_Insert_order();

        }

        private void testeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEdit_Trades TESTE = new frmEdit_Trades();
            //TESTE.MdiParent = this;
            //TESTE.sh
            TESTE.Id_usuario = Id_usuario;
            TESTE.Show();
        }

        private void positionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPosicoes Posicao = new frmPosicoes();
            Posicao.Id_usuario = Id_usuario;
            Posicao.ShowDialog();

        }

        private void tradesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTrades Trades = new frmTrades();
            Trades.Id_usuario = Id_usuario;
            Trades.ShowDialog();
        }

        private void checkConsistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsistency Consistencia = new frmConsistency();
            Consistencia.Id_usuario = Id_usuario;
            Consistencia.ShowDialog();
        }

        private void editDatasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEdit Editar = new frmEdit();
            Editar.ShowDialog();
        }

        private void dgPositions_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgPositions);

        }

        private void dgPositions_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false,dgPositions);

        }

        private void dgPositions_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Coluns(dgPositions, Id_usuario);
        }

        private void dgPositions_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Coluns(dgPositions,Id_usuario);
        }

        private void ShowColumnSelector() { ShowColumnSelector(true, dgPositions); }
        bool show = false;
        private void ShowColumnSelector(bool showForm,DevExpress.XtraGrid.Views.Grid.GridView Nome_Grid)
        {
            if (show)
            {
                button1.Text = "Hide Columns &Selector";
                if (showForm) Nome_Grid.ColumnsCustomization();
            }
            else
            {
                button1.Text = "Show Columns &Selector";
                if (showForm) Nome_Grid.DestroyCustomization();
            }
        }

        private void dgAbertas_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgAbertas);

        }

        private void dgAbertas_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgAbertas);
        }

        private void dgAbertas_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Coluns(dgAbertas, Id_usuario);
        }

        private void dgAbertas_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Coluns(dgAbertas, Id_usuario);
        }

        private void dgFechadas_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Coluns(dgFechadas, Id_usuario);
        }

        private void dgFechadas_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Coluns(dgFechadas, Id_usuario);
        }

        private void dgFechadas_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgFechadas);
        }

        private void dgFechadas_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgFechadas);
        }

        private void dgCanceladas_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Coluns(dgCanceladas, Id_usuario);
        }

        private void dgCanceladas_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Coluns(dgCanceladas, Id_usuario);
        }

        private void dgCanceladas_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgCanceladas);
        }

        private void dgCanceladas_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgCanceladas);
        }



        private void dgAbertas_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //int Id_ordem = Convert.ToInt32(dgAbertas.Columns["Id Order"].SortIndex.ToString() );

            //  Opcoes_Abertas( Convert.ToInt32(e.),Id_ordem, Id_usuario);

              // Id_ordem = Convert.ToInt32(dgAbertas.Rows[e.RowIndex].Cells["Id Order"].Value.ToString());

              //  Opcoes_Abertas( e.ColumnIndex,Id_ordem, Id_usuario);
            //MessageBox.Show(e.Row.ToString() + " " + sender.ToString());


        }

        private void dgAbertas_DoubleClick(object sender, EventArgs e)
        {
            int resultado;
            int Id_order = Retorna_Id(dgAbertas.GetDataRow(dgAbertas.FocusedRowHandle), "dgAbertas");

            GridView zz = sender  as GridView;

            if (Id_order != 0)
            {
                  string Column_Name = zz.FocusedColumn.Caption.ToString();
                    
                    switch (Column_Name)
                    {
                        case "Edit":

                            Negocios.Editar_Ordem(Id_order);
                            resultado = 1;
                            break;

                        case "Cancel":
                            resultado = Negocios.Cancela_Ordem(Id_order);

                            break;
                    
                        default :

                            resultado = Negocios.Inserir_Trade(Id_order, Id_usuario);
                            break;
                
                    }
                    if (Id_usuario != 9)
                    {
                        if (resultado != 0)
                        {
                            Carrega_Grid();
                        }
                    }
                    // ButtonEdit edit = sender as ButtonEdit;
                    // GridView view = (edit.Parent as GridControl).FocusedView as GridView;
                    // string name = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["Id Portfolio"]).ToString();
                    // MessageBox.Show(name);
            }    
        }

        private void dgAbertas_Click(object sender, EventArgs e)
        {
            //  if (e.RowIndex != -1)
            // {
            // Id_ordem = Convert.ToInt32(dgAbertas.Rows[e.RowIndex].Cells["Id Order"].Value.ToString());

            //  Opcoes_Abertas( e.ColumnIndex,Id_ordem, Id_usuario);
            // }
        }
      /*  void item_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //TESTE
            ButtonEdit edit = sender as ButtonEdit;
            GridView view = (edit.Parent as GridControl).FocusedView as GridView;
            string name = view.GetRowCellValue(view.FocusedRowHandle, view.Columns["CategoryName"]).ToString();
            MessageBox.Show(name);
        }
        */
        void item_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            //FUNCIONANDO
           // ButtonBase edit = sender as ButtonBase;
            //ButtonEdit edit = sender as ButtonEdit;

           // int numero = ((Convert.ToInt32(edit.Bounds.Location.Y.ToString()) - 59) / 21) + 1;
            /*
            int Id_order = Retorna_Id(dgAbertas.GetDataRow(dgAbertas.FocusedRowHandle));

            int resultado = Negocios.Cancela_Ordem(Id_order);
            if (resultado != 0)
            {
                Carrega_Grid();
            }
             */
        }

        private int Retorna_Id(DataRow dr, string Nome_Grid)
        {
            int Id_order;
            try
            {
                ;
                if (dr != null)
                {

                    object[] items = dr.ItemArray;
                    switch (Nome_Grid)
                    {
                        case "dgAbertas":
                            Id_order = Convert.ToInt32(items[3].ToString());
                            break;
                        case "dgTrades":
                            Id_order = Convert.ToInt32(items[0].ToString());
                            break;

                        case "dgTrades2":
                            Id_order = Convert.ToInt32(items[1].ToString());
                            break;

                        case "dgPositions":
                            Id_order = Convert.ToInt32(items[2].ToString());
                            break;
                        default:
                            Id_order = 0;
                            break;
                    }
                }
                else
                {
                    Id_order = 0;
                }
                return Id_order;
            }
            catch
            {
                return 0;
            }
        }

        private void dgPositions_EndGrouping(object sender, EventArgs e)
        {

            if (Expandable == 0)
            {
                dgPositions.ExpandAllGroups();
            }
            else
            {
                dgPositions.CollapseAllGroups();
            }

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Cash", dgPositions.Columns["Cash"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "BRL", dgPositions.Columns["BRL"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "BRL/NAV", dgPositions.Columns["BRL/NAV"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Brokerage", dgPositions.Columns["Brokerage"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Total P/L", dgPositions.Columns["Total P/L"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Asset P/L pC", dgPositions.Columns["Asset P/L pC"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Currency P/L", dgPositions.Columns["Currency P/L"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Delta Cash", dgPositions.Columns["Delta Cash"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Cash/NAV", dgPositions.Columns["Cash/NAV"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Delta/NAV", dgPositions.Columns["Delta/NAV"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Contribution pC", dgPositions.Columns["Contribution pC"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Long", dgPositions.Columns["Long"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Short", dgPositions.Columns["Short"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Gross", dgPositions.Columns["Gross"]);
            ((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";

            //dgPositions.GroupSummary.Add(SummaryItemType.Sum, "P/L %", dgPositions.Columns["P/L %"]);
            //((GridSummaryItem)dgPositions.GroupSummary[dgPositions.GroupSummary.Count - 1]).DisplayFormat = "{0:p2}";
        }

        private void dgTrades_EndGrouping(object sender, EventArgs e)
        {
            dgTrades.ExpandAllGroups();

            dgTrades.GroupSummary.Add(SummaryItemType.Sum, "Trade Quantity", dgTrades.Columns["Trade Quantity"]);
            ((GridSummaryItem)dgTrades.GroupSummary[dgTrades.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";
            dgTrades.GroupSummary.Add(SummaryItemType.Sum, "Cash", dgTrades.Columns["Cash"]);
            ((GridSummaryItem)dgTrades.GroupSummary[dgTrades.GroupSummary.Count - 1]).DisplayFormat = "{0:#,#0.00}";

        }


        private void dgAbertas_EndGrouping(object sender, EventArgs e)
        {
            dgAbertas.ExpandAllGroups();

            dgAbertas.GroupSummary.Add(SummaryItemType.Sum, "Total", dgAbertas.Columns["Total"]);
            dgAbertas.GroupSummary.Add(SummaryItemType.Sum, "Cash Order", dgAbertas.Columns["Cash Order"]);
        }

        private void dgFechadas_EndGrouping(object sender, EventArgs e)
        {
            dgFechadas.ExpandAllGroups();

            dgFechadas.GroupSummary.Add(SummaryItemType.Sum, "Total", dgFechadas.Columns["Total"]);
            dgFechadas.GroupSummary.Add(SummaryItemType.Sum, "Cash Order", dgFechadas.Columns["Cash Order"]);
        }

        private void dgCanceladas_EndGrouping(object sender, EventArgs e)
        {
            dgCanceladas.ExpandAllGroups();

            dgCanceladas.GroupSummary.Add(SummaryItemType.Sum, "Total", dgCanceladas.Columns["Total"]);
            dgCanceladas.GroupSummary.Add(SummaryItemType.Sum, "Cash Trade", dgCanceladas.Columns["Cash Trade"]);
        }

        private void dgPositions_CalcRowHeight(object sender, DevExpress.XtraGrid.Views.Grid.RowHeightEventArgs e)
        {
            //GridView View = sender as GridView;
            //if (!view.IsGroupRow(e.RowHandle))
             //   if (view.GetVisibleIndex(e.RowHandle) % 2 == 0)
             //       e.RowHeight = 5;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                ExportTo(new ExportXlsProvider(fileName));
                OpenFile(fileName);
            }
        }

        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Title = "Export To " + title;
            dlg.FileName = "Position";
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }

        private void ExportTo(IExportProvider provider)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            this.FindForm().Refresh();
            BaseExportLink link = dgPositions.CreateExportLink(provider);
            (link as GridViewExportLink).ExpandAll = false;
            //link.Progress += new DevExpress.XtraGrid.Export.ProgressEventHandler(Export_Progress);
            link.ExportTo(true);
            provider.Dispose();
           // link.Progress -= new DevExpress.XtraGrid.Export.ProgressEventHandler(Export_Progress);

            Cursor.Current = currentCursor;
        }

        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = fileName;
                    process.StartInfo.Verb = "Open";
                    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                    process.Start();
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //progressBarControl1.Position = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Text Document", "Text Files|*.txt");
            if (fileName != "")
            {
                ExportTo(new ExportTxtProvider(fileName));
                OpenFile(fileName);
            }
        }

        private void priceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInsertPrice Preco = new frmInsertPrice();
            Preco.Id_usuario = Id_usuario;
            Preco.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                ExportTo(new ExportXlsProvider(fileName));
                OpenFile(fileName);
            }

        }

        private void dgTrades_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Coluns(dgTrades, Id_usuario);

        }

        private void dgTrades_DoubleClick(object sender, EventArgs e)
        {
            int resultado;
            string SQLString;
            int Id_Trade = Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "dgTrades");
            int Id_Order = Retorna_Id(dgTrades.GetDataRow(dgTrades.FocusedRowHandle), "dgTrades2");

            GridView zz = sender as GridView;
            string Column_Name = zz.FocusedColumn.Caption.ToString();

            if (Column_Name == "Cancel")
            {
                SQLString = "Select Status_Ordem from Tb012_Ordens Where Id_Ordem=" + Id_Order;
                int StatusOrder = Convert.ToInt32(CargaDados.DB.Execute_Query_String(SQLString));

                int resposta;
                switch (StatusOrder)
                {
                    case 1:
                        resposta = Convert.ToInt32(MessageBox.Show("Do you really want cancel to Trade?", "Trades", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                        break;
                    case 3:
                        resposta = Convert.ToInt32(MessageBox.Show("This order is already complete. You Want really cancel the entirely order and all trades this order?", "Trades", MessageBoxButtons.YesNo, MessageBoxIcon.Question));
                        break;
                    default:
                        resposta = 0;
                        break;
                }
                if (resposta == 6)
                {
                    SQLString = "EXEC Proc_Cancela_Trade_Ordem @Id_Trade = " + Id_Trade;
                    resultado = CargaDados.DB.Execute_Insert_Delete_Update(SQLString);

                    if (resultado != 0)
                    {
                        Carrega_Grid();
                    }
                    else
                    {
                        MessageBox.Show("Error on Cancel!");
                    }
                }
            }
            if (Column_Name == "Edit")
            {

                frmEditPriceTrade Edita_Trade = new frmEditPriceTrade();
                Edita_Trade.Id_Trade = Id_Trade;
                Edita_Trade.ShowDialog();
                Carrega_Grid();

            }

        }

        private void dgTrades_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            Valida.Save_Coluns(dgTrades, Id_usuario);

        }


        private void dgTrades_HideCustomizationForm(object sender, EventArgs e)
        {
            show = false;
            ShowColumnSelector(false, dgTrades);

        }

        private void dgTrades_ShowCustomizationForm(object sender, EventArgs e)
        {
            show = true;
            ShowColumnSelector(false, dgTrades);

        }

        private void cmbStrategy_SelectedValueChanged(object sender, EventArgs e)
        {
            Carrega_Sub_Estrategy();
        }

        private void Preenche_Dados_Posicao(DataRow dr)
        {
            int Id_Ticker;
            int Id_Strategy;
            int Id_Sub_Strategy;
            int Id_Portfolio;
            int Id_Broker;
            int Id_Ticker_Type;
            double Posicao;
            double Ultimo;
            double Moeda;
           
            
            try
            {
            
                if (dr != null)
                {
                    object[] items = dr.ItemArray;

                    Id_Portfolio = Convert.ToInt32(items[0].ToString());
                    Id_Ticker = Convert.ToInt32(items[2].ToString());
                    Id_Strategy = Convert.ToInt32(items[7].ToString());
                    Id_Sub_Strategy = Convert.ToInt32(items[9].ToString());
                    Posicao = Convert.ToInt32(items[13]);
                    Id_Ticker_Type =  Convert.ToInt32(items[5]);
                    if (Id_Ticker_Type ==5) 
                    {
                    Id_Broker = 15;
                    }
                        else                   
                    {
                    Id_Broker = 1;
                    }

                    if (items[17].ToString() != "")
                        Ultimo = Convert.ToInt32(items[17]);
                    else
                        Ultimo = 0;
                    Moeda = Convert.ToInt32(items[27]);


                    if (Id_Portfolio != 0)
                    {
                        if (Id_Portfolio == 4)
                        {
                            if (Moeda == 900)
                            {
                                cmbportfolio.SelectedValue = 5;
                            }
                            else
                            {
                                cmbportfolio.SelectedValue = 6;
                            }
                        }
                        else
                        {
                            cmbportfolio.SelectedValue = Id_Portfolio;
                        }
                    }
                    cmBrooker.SelectedValue = Id_Broker;


                    if (Id_Ticker != 0)
                        cmbTicker.SelectedValue = Id_Ticker;

                    if (Id_Strategy != 0)
                        cmbStrategy.SelectedValue = Id_Strategy;

                    if (Id_Sub_Strategy != 0)
                        cmbSub_Strategy.SelectedValue = Id_Sub_Strategy;
                    //MessageBox.Show("PASSOU");
                    if (Posicao != 0)
                        txtQtd.Text = Convert.ToDouble(Posicao * -1).ToString("##,###.00#######");

                  //  if (Ultimo != 0)
                        txtPrice.Text = Convert.ToDouble(Ultimo).ToString("##,###.00#######");
                        if (this.txtCash.Text != "")
                        {
                            decimal vfin = Convert.ToDecimal(this.txtCash.Text);
                            this.txtCash.Text = vfin.ToString("##,###.00#######");
                        }
                        if (Convert.ToDouble(Posicao * -1) < 0)
                        {
                            cmdInsert_Order_Neg.Enabled = true;
                            cmdInsert_Order.Enabled = false;
                        }
                        else
                        {
                            cmdInsert_Order_Neg.Enabled = false;
                            cmdInsert_Order.Enabled = true;
                        }

                    
                }
            }
           
            catch
            {
                MessageBox.Show("ERRO");
            }
            
        }

        private void dgPositions_Click(object sender, EventArgs e)
        {
            Preenche_Dados_Posicao(dgPositions.GetDataRow(dgPositions.FocusedRowHandle));

        }

        private Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return Rectangle.Empty;
        }

        private void dgPositions_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            foreach (GridSummaryItem si in view.GroupSummary)
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                    items.Add(si);
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            info.GroupText = view.GroupedColumns[level].Caption + ": " + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);
            foreach (GridGroupSummaryItem item in items)
            {
                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                // calculate summary text and boundaries
                string text = item.GetDisplayText(values[item], false);
                SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                int width = Convert.ToInt32(sz.Width) + 1;
                rect.X += rect.Width - width - 2;
                rect.Width = width;
                rect.Y = e.Bounds.Y;
                rect.Height = e.Bounds.Height - 2;

                // draw a summary values
                e.Appearance.DrawString(e.Cache, text, rect);
            }

            // disable default painting of the group row
            e.Handled = true;
            //dgPositions.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
        
        }

        private void dgAbertas_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            foreach (GridSummaryItem si in view.GroupSummary)
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                    items.Add(si);
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            info.GroupText = view.GroupedColumns[level].Caption + ": " + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);
            foreach (GridGroupSummaryItem item in items)
            {
                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                // calculate summary text and boundaries
                string text = item.GetDisplayText(values[item], false);
                SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                int width = Convert.ToInt32(sz.Width) + 1;
                rect.X += rect.Width - width - 2;
                rect.Width = width;
                rect.Y = e.Bounds.Y;
                rect.Height = e.Bounds.Height - 2;

                // draw a summary values
                e.Appearance.DrawString(e.Cache, text, rect);
            }

            // disable default painting of the group row
            e.Handled = true;

        }

        private void dgFechadas_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            foreach (GridSummaryItem si in view.GroupSummary)
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                    items.Add(si);
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            info.GroupText = view.GroupedColumns[level].Caption + ": " + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);
            foreach (GridGroupSummaryItem item in items)
            {
                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                // calculate summary text and boundaries
                string text = item.GetDisplayText(values[item], false);
                SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                int width = Convert.ToInt32(sz.Width) + 1;
                rect.X += rect.Width - width - 2;
                rect.Width = width;
                rect.Y = e.Bounds.Y;
                rect.Height = e.Bounds.Height - 2;

                // draw a summary values
                e.Appearance.DrawString(e.Cache, text, rect);
            }

            // disable default painting of the group row
            e.Handled = true;

        }

        private void dgTrades_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            foreach (GridSummaryItem si in view.GroupSummary)
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                    items.Add(si);
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            info.GroupText = view.GroupedColumns[level].Caption + ": " + view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);
            foreach (GridGroupSummaryItem item in items)
            {
                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                // calculate summary text and boundaries
                string text = item.GetDisplayText(values[item], false);
                SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                int width = Convert.ToInt32(sz.Width) + 1;
                rect.X += rect.Width - width - 2;
                rect.Width = width;
                rect.Y = e.Bounds.Y;
                rect.Height = e.Bounds.Height - 2;

                // draw a summary values
                e.Appearance.DrawString(e.Cache, text, rect);
            }

            // disable default painting of the group row
            e.Handled = true;

        }

        private void strategySubStrategyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStrategy strategy = new frmStrategy();
            strategy.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            dgPositions.ExpandAllGroups();
            Expandable = 0;
        }

        private void txtCoplase_Click(object sender, EventArgs e)
        {
            dgPositions.CollapseAllGroups();
            Expandable = 1;
        }

        private void positionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmPositions Tela_Pos = new frmPositions();
            Tela_Pos.MdiParent = this;
            Tela_Pos.Show();
        }

        private void openOrdesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrders Tela_Ordens = new frmOrders();
            Tela_Ordens.MdiParent = this;
            Tela_Ordens.Show();
        }

        private void calculateDeltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCalculateDelta Delta = new frmCalculateDelta();
            Delta.ShowDialog();
            Delta.Close();
            Delta.Dispose();
        }



    }
}