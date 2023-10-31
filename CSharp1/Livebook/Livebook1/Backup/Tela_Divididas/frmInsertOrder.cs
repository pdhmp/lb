using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.CargaDados;
using SGN.Business;
using SGN.Validacao;

namespace SGN.Tela_Divididas
{
    public partial class frmInsertOrder : Form
    {

        CarregaDados CargaDados = new CarregaDados();
        Business_Class Negocios = new Business_Class();
        Valida Valida = new Valida();
        public int Id_usuario;

        public frmInsertOrder()
        {
            InitializeComponent();
        }

        private void txtpress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.ProcessTabKey(true);

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

        private void cmdInsert_Order_Click(object sender, EventArgs e)
        {
            Insert_order();
        }

        private void cmdInsert_Order_Neg_Click(object sender, EventArgs e)
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


    }
}