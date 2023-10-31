using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.CargaDados;
using SGN.Validacao;
using SGN.Business;

namespace SGN
{
    public partial class frmInsertOrdem : Form
    {
        CarregaDados Carga = new CarregaDados();
        Valida Valida = new Valida();
        Business_Class Negocios = new Business_Class();
        
        public double Id_Ordem;
        public int Id_usuario;
        public frmInsertOrdem()
        {
            InitializeComponent();
        }

        private void frmInsertOrdem_Load(object sender, EventArgs e)
        {
            //CARTEIRAS
            Carga.carregacombo(this.cmbCarteiras, "Select Id_Carteira,Carteira from  Tb002_Carteiras Where Insere_Ordem <>0", "Id_Carteira", "Carteira", 3);
            //Tipo de Mercado - Normal, after...
            Carga.carregacombo(this.cmbMercado, "Select Id_Tipo_Mercado,Tipo_Mercado from  Tb114_Tipo_Mercados", "Id_Tipo_Mercado", "Tipo_Mercado",99);

            //Ativos
            if (Id_usuario == 1)
            {
                Carga.carregacombo(this.cmbAtivo, "Select Id_Ativo, Simbolo from Tb001_Ativos", "Id_Ativo", "Simbolo", 99);
            }
            else
            {
                Carga.carregacombo(this.cmbAtivo, "Select Id_Ativo, Simbolo from VW001_Ativos", "Id_Ativo", "Simbolo", 99);
            }


            //Estrategia
            Carga.carregacombo(this.cmbEstrategia, "Select Id_Estrategia, Estrategia from Tb111_Estrategia", "Id_Estrategia", "Estrategia", 99);

            //Tipo Alocação - APAGAR
           // Carga.carregacombo(this.cmbTipoAloc, "Select Id_Tipo_Alocacao, Tipo_Alocacao from Tb110_Tipo_Alocacao", "Id_Tipo_Alocacao", "Tipo_Alocacao", 99);
            
            //Brooker
            Carga.carregacombo(this.cmbBrooker, "Select Id_Corretora,Nome from Tb011_Corretoras", "Id_Corretora", "Nome", 99);
            
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
                Carga.carregacombo(this.cmbSubEstrategia, "Select Id_Sub_Estrategia,Sub_Estrategia, Id_Estrategia from Tb112_Sub_Estrategia where Id_Estrategia =" + this.cmbEstrategia.SelectedValue, "Id_Sub_Estrategia", "Sub_Estrategia", 99);
            }
            catch
            {
         
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
            catch
            { 

            }
        }
        private void txtPreco_Leave(object sender, EventArgs e)
        {
            try
            {
                if (this.txtPreco.Text != "")
                {
                    decimal Preco = Convert.ToDecimal(txtPreco.Text);
                    this.txtPreco.Text = Preco.ToString("##,###.00");
                }
                if (this.txtQtd.Text != "")
                {
                    decimal qtd = Convert.ToDecimal(this.txtQtd.Text);
                    this.txtQtd.Text = qtd.ToString("##,###.00");
                }

                if (this.txtValorFinanc.Text != "")
                {
                    decimal vfin = Convert.ToDecimal(this.txtValorFinanc.Text);
                    this.txtValorFinanc.Text = vfin.ToString("##,###.00");
                }
            }
            catch(System.Exception z)
            {
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
            SqlDataReader dtOrdem = Carga.DB.Execute_Query_DataRead("Select * from Tb012_Ordens where Id_Ordem=" + Id_Ordem);

            this.cmdInsert.Text = "Editar";

            while (dtOrdem.Read())
            {
                this.cmbCarteiras.SelectedValue = dtOrdem["Id_Carteira"];
                this.cmbCarteiras.Enabled = false;
                this.cmbMercado.SelectedValue = dtOrdem["Tipo_Mercado"];
                this.cmbAtivo.SelectedValue = dtOrdem["Id_Ativo"];
                this.cmbAtivo.Enabled = false;
                this.cmbEstrategia.SelectedValue = dtOrdem["Estrategia"];
                this.cmbSubEstrategia.SelectedValue = dtOrdem["Sub_Estrategia"];
                this.cmbBrooker.SelectedValue = dtOrdem["Id_Corretora"];
                
                if (dtOrdem["Data_Valid_Ordem"].ToString() == null)
                {
                    this.dtpValOrdem.Enabled = false;
                    this.chkGtc.Checked = true;
                }
                else
                {
                    this.dtpValOrdem.Text = dtOrdem["Data_Valid_Ordem"].ToString();
                }
                decimal preco = Convert.ToDecimal(dtOrdem["Preco"].ToString());
                this.txtPreco.Text = preco.ToString("#,##0.00");

                decimal Qtd = Convert.ToDecimal(dtOrdem["Quantidade"].ToString());
                this.txtQtd.Text = Qtd.ToString("#,##0");

                decimal VF = Convert.ToDecimal(dtOrdem["Valor_Financeiro"].ToString());
                this.txtValorFinanc.Text = VF.ToString("#,##0.00");
                
                ;
            }
            dtOrdem.Close();
            dtOrdem.Dispose();

        }
        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (Id_Ordem != 0.0)
            {
                Negocios.Insert_Order(Id_Ordem, true, Convert.ToInt32(this.cmbCarteiras.SelectedValue.ToString()), Convert.ToInt32(this.cmbAtivo.SelectedValue.ToString()), Convert.ToDecimal(this.txtQtd.Text), Convert.ToDecimal(this.txtPreco.Text), this.dtpValOrdem.Value.ToString(), Convert.ToInt32(cmbBrooker.SelectedValue.ToString()), Convert.ToInt32(this.cmbEstrategia.SelectedValue.ToString()), Convert.ToInt32(this.cmbSubEstrategia.SelectedValue.ToString()), Convert.ToInt32(this.cmbMercado.SelectedValue.ToString()), Id_usuario);
            }
        else
            {
             Negocios.Insert_Order(0, false, Convert.ToInt32(this.cmbCarteiras.SelectedValue.ToString()), Convert.ToInt32(this.cmbAtivo.SelectedValue.ToString()), Convert.ToDecimal(this.txtQtd.Text), Convert.ToDecimal(this.txtPreco.Text), this.dtpValOrdem.Value.ToString(), Convert.ToInt32(cmbBrooker.SelectedValue.ToString()), Convert.ToInt32(this.cmbEstrategia.SelectedValue.ToString()), Convert.ToInt32(this.cmbSubEstrategia.SelectedValue.ToString()), Convert.ToInt32(this.cmbMercado.SelectedValue.ToString()), Id_usuario);
            }
            this.Close();
        }
     }
}