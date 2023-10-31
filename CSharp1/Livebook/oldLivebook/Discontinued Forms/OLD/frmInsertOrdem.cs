using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using SGN.Validacao;
using SGN.Business;

namespace SGN
{
    public partial class frmInsertOrdem : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Valida Valida = new Valida();
        Business_Class Negocios = new Business_Class();
        
        public double Id_Ordem;
        public int Id_User;
        public frmInsertOrdem()
        {
            InitializeComponent();
        }

        private void frmInsertOrdem_Load(object sender, EventArgs e)
        {
            //Port_NameS
            CargaDados.carregacombo(this.cmbPort_Names, "Select Id_Portfolio,Port_Name from  dbo.Tb002_Portfolios Where Insere_Ordem <>0", "Id_Portfolio", "Port_Name", 3);
            //Tipo de Mercado - Normal, after...
            CargaDados.carregacombo(this.cmbMercado, "Select Id_Tipo_Mercado,Tipo_Mercado from  Tb114_Tipo_Mercados", "Id_Tipo_Mercado", "Tipo_Mercado", 99);

            //Ativos

            CargaDados.carregacombo(this.cmbAtivo, "Select IdSecurity, Ticker from Tb001_Securities", "IdSecurity", "Ticker", 99);


            //Estrategia
            CargaDados.carregacombo(this.cmbEstrategia, "Select Id_Estrategia, Estrategia from Tb111_Estrategia", "Id_Estrategia", "Estrategia", 99);

            //Tipo Alocação - APAGAR
           // Carga.carregacombo(this.cmbTipoAloc, "Select Id_Tipo_Alocacao, Tipo_Alocacao from Tb110_Tipo_Alocacao", "Id_Tipo_Alocacao", "Tipo_Alocacao", 99);
            
            //Brooker
            CargaDados.carregacombo(this.cmbBrooker, "Select Id_Corretora,Nome from Tb011_Corretoras order by Show_Prefered desc,Nome asc ", "Id_Corretora", "Nome", 99);
            
            Carrega_Sub_Estrategia();
        
        if (Id_Ordem != 0)
        {
            Carrega_Edicao(Id_Ordem);
        }
        
        }

        public void Carrega_Sub_Estrategia()
        {
            int valor;
            try
            {
                valor = Convert.ToInt32(cmbEstrategia.SelectedValue);
                //SubEstrategia
                CargaDados.carregacombo(this.cmbSubEstrategia, "Select Id_Sub_Estrategia,Sub_Estrategia, Id_Estrategia from Tb112_Sub_Estrategia where Id_Estrategia =" + this.cmbEstrategia.SelectedValue, "Id_Sub_Estrategia", "Sub_Estrategia", 99);
            }
            catch(Exception e)
            {
                Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());

            }
         }
        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtQtd_TextChanged(object sender, EventArgs e)
        {
            calcula_VF("qtd");
        }

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            try
            {
                    calcula_VF("qtd");
                }
            catch(Exception z)
            {
                Valida.Error_Dump_TXT(z.ToString(), this.Name.ToString());
            }
        }
        private void txtPreco_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.txtPreco.Text != "")
                {
                    decimal Preco = Convert.ToDecimal(txtPreco.Text);
                    this.txtPreco.Text = Preco.ToString("##,##0.00");
                }
                if (this.txtQtd.Text != "")
                {
                    decimal qtd = Convert.ToDecimal(this.txtQtd.Text);
                    this.txtQtd.Text = qtd.ToString("##,##0.00");
                }

                if (this.txtValorFinanc.Text != "")
                {
                    decimal vfin = Convert.ToDecimal(this.txtValorFinanc.Text);
                    this.txtValorFinanc.Text = vfin.ToString("##,##0.00");
                }
            }
            catch(System.Exception z)
            {
                Valida.Error_Dump_TXT(z.ToString(), this.Name.ToString());

                if (z.TargetSite.Name.ToString() == "StringToNumber")
                {
                    MessageBox.Show("You this trying to insert letters in numerical fields!");
                }
                else
                {
                    MessageBox.Show(z.ToString());
                }
            }
        }

            private void calcula_VF(string sender)
            {
                    if (sender.ToString() == "qtd")
                    {
                        decimal VF;

                        if (this.txtQtd.Text != "" && this.txtPreco.Text != "" && this.txtQtd.Text != "-")
                        {
                            VF = (Convert.ToDecimal(txtQtd.Text) * Convert.ToDecimal(txtPreco.Text));
                            txtValorFinanc.Text = Convert.ToString(VF);
                        }
                        else
                        {
                            txtValorFinanc.Text = "";
                        }
                    }
            }

        private void cmbEstrategia_SelectedIndexChanged(object sender, EventArgs e)
        {
            Carrega_Sub_Estrategia();
        }

        private void chkGtc_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkGtc.Checked)
            {
                this.dtpValOrdem.Enabled = false;
            }
            else
            {
                this.dtpValOrdem.Enabled = true;
            }
        }

        private void txtValorFinanc_TextChanged(object sender, EventArgs e)
        {
        }

        private void Carrega_Edicao(double Id_Ordem)
        {
            DataTable dtOrdem = CargaDados.curConn.Return_DataTable("Select * from Tb012_Ordens where Id_Ordem=" + Id_Ordem);

            this.cmdInsert.Text = "Editar";

            foreach (DataRow curRow in curTable.Rows)
            {
                this.cmbPort_Names.SelectedValue = curRow["Id_Portfolio"];
                this.cmbPort_Names.Enabled = false;
                this.cmbMercado.SelectedValue = curRow["Tipo_Mercado"];
                this.cmbAtivo.SelectedValue = curRow["Id_Ativo"];
                this.cmbAtivo.Enabled = false;
                this.cmbEstrategia.SelectedValue = curRow["Estrategia"];
                this.cmbSubEstrategia.SelectedValue = curRow["Sub_Estrategia"];
                this.cmbBrooker.SelectedValue = curRow["Id_Corretora"];

                if (curRow["Data_Valid_Ordem"].ToString() == null)
                {
                    this.dtpValOrdem.Enabled = false;
                    this.chkGtc.Checked = true;
                }
                else
                {
                    this.dtpValOrdem.Text = curRow["Data_Valid_Ordem"].ToString();
                }
                decimal preco = Convert.ToDecimal(curRow["Preco"].ToString());
                this.txtPreco.Text = preco.ToString("#,##0.00");

                decimal Qtd = Convert.ToDecimal(curRow["Quantidade"].ToString());
                this.txtQtd.Text = Qtd.ToString("#,##0");

                decimal VF = Convert.ToDecimal(curRow["Valor_Financeiro"].ToString());
                this.txtValorFinanc.Text = VF.ToString("#,##0.00");
                
                ;
            }

        }
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (Id_Ordem != 0.0)
            {
                //Negocios.Insert_Order(Id_Ordem, ,true, Convert.ToInt32(this.cmbPort_Names.SelectedValue.ToString()), Convert.ToInt32(this.cmbAtivo.SelectedValue.ToString()), Convert.ToDecimal(this.txtQtd.Text), Convert.ToDecimal(this.txtPreco.Text), this.dtpValOrdem.Value.ToString(), Convert.ToInt32(cmbBrooker.SelectedValue.ToString()), Convert.ToInt32(this.cmbEstrategia.SelectedValue.ToString()), Convert.ToInt32(this.cmbSubEstrategia.SelectedValue.ToString()), Convert.ToInt32(this.cmbMercado.SelectedValue.ToString()), Id_User);
            }
        else
            {
             //Negocios.Insert_Order(0, false, Convert.ToInt32(this.cmbPort_Names.SelectedValue.ToString()), Convert.ToInt32(this.cmbAtivo.SelectedValue.ToString()), Convert.ToDecimal(this.txtQtd.Text), Convert.ToDecimal(this.txtPreco.Text), this.dtpValOrdem.Value.ToString(), Convert.ToInt32(cmbBrooker.SelectedValue.ToString()), Convert.ToInt32(this.cmbEstrategia.SelectedValue.ToString()), Convert.ToInt32(this.cmbSubEstrategia.SelectedValue.ToString()), Convert.ToInt32(this.cmbMercado.SelectedValue.ToString()), Id_User);
            }
            this.Close();
        }

        private void frmInsertOrdem_Load_1(object sender, EventArgs e)
        {

        }

        private void frmInsertOrdem_Load_2(object sender, EventArgs e)
        {

        }
     }
}