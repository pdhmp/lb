using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using SGN.Validacao;

namespace SGN
{
    public partial class frmStrategy : Form
    {
        CarregaDados CargaDados = new CarregaDados();
        Valida  Valida = new Valida();

        public frmStrategy()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmStrategy_Load(object sender, EventArgs e)
        {
            
            Carrega_Strategy();
            Carrega_Sub_Estrategy();
        }

        public void Carrega_Strategy()
        {
            CargaDados.carregaList(this.lstStrategy, "Select Id_Estrategia, Estrategia from Tb111_Estrategia", "Id_Estrategia", "Estrategia");
        }
        public void Carrega_Sub_Estrategy()
        {
            int valor;
            int indice;
            try
            {
                indice = Convert.ToInt32(lstStrategy.SelectedIndex.ToString());

                valor = Convert.ToInt32(lstStrategy.SelectedValue);
                //nome = lstStrategy.SelectedItem.ToString();
                //SubEstrategia
                CargaDados.carregaList(this.lstSubStrategy, "Select Id_Sub_Estrategia,Sub_Estrategia, Id_Estrategia from Tb112_Sub_Estrategia where Id_Estrategia =" + valor, "Id_Sub_Estrategia", "Sub_Estrategia");
            }
            catch(Exception e)
            {
                Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());
            }
        }

        private void lstStrategy_Click(object sender, EventArgs e)
        {
            Carrega_Sub_Estrategy();
        }

        private int Insert_Strategy()
        {
            String SQLString;
            int retorno;
            try
            {
                if (txtStrategy.Text != "")
                {
                    SQLString = "INSERT INTO Tb111_Estrategia([Estrategia])VALUES('" + txtStrategy.Text + "'); SELECT @@IDENTITY";
                    retorno = Convert.ToInt32(CargaDados.curConn.Execute_Query_String(SQLString));
                }
                else
                {
                    retorno = 0;
                }

            }
            catch(Exception e)
            {
                Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());

                retorno = 0;
            }
                if (retorno == 0)
                    MessageBox.Show("Error on insert. Verify Data!");
                else
                {
                    Carrega_Strategy();
                    lstStrategy.SelectedValue = retorno;
                    txtStrategy.Text = "";
                }
    
            return retorno;
        }
        private int Insert_SubStrategy(int Id_Strategy)
        {
            String SQLString;
            int retorno;
            try
            {
                if (txtSubStrategy.Text != "")
                {
                    SQLString = "INSERT INTO Tb112_Sub_Estrategia([Sub_Estrategia],[Id_Estrategia])" +
                                 " VALUES('" + txtSubStrategy.Text + "'," + Id_Strategy + "); SELECT @@IDENTITY ";
                    retorno = Convert.ToInt32(CargaDados.curConn.Execute_Query_String(SQLString));
                }
                else
                {
                    retorno= 0; 
                }
            }
            catch(Exception e)
            {
                Valida.Error_Dump_TXT(e.ToString(), this.Name.ToString());
                retorno= 0;
            }
            if (retorno == 0)
                MessageBox.Show("Error on insert. Verify Data!");
            else
            {
                Carrega_Sub_Estrategy();
                lstSubStrategy.SelectedValue = retorno;
                txtSubStrategy.Text = "";
            }
            return retorno;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Insert_Strategy();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Id_EStrategia;
            if (lstStrategy.SelectedValue.ToString() != null)
            {
                Id_EStrategia = Convert.ToInt32(lstStrategy.SelectedValue.ToString());
                Insert_SubStrategy(Id_EStrategia);
                Carrega_Sub_Estrategy();
            
            }
        }

        private void lstStrategy_Move(object sender, EventArgs e)
        {
            Carrega_Sub_Estrategy();
        }

        private void lstStrategy_SelectedValueChanged(object sender, EventArgs e)
        {
            Carrega_Sub_Estrategy();

        }

        private void frmStrategy_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
       
    }
}