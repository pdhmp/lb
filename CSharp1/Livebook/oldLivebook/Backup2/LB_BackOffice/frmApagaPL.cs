using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;

namespace SGN
{
    public partial class frmApagaPL : Form
    {

        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
                
        public frmApagaPL()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Data;
            int Portfolio;
            Data = dtpDate.Value.ToString("yyyyMMdd");
            Portfolio = Convert.ToInt32(cmbView.SelectedValue.ToString());

            int resposta;
            string SQLString;
            resposta = Convert.ToInt32(MessageBox.Show("Tem certeza que deseja apagar PL e a Posição Do Dia " + Data + " até hoje?", "Position", MessageBoxButtons.YesNo, MessageBoxIcon.Question));

            if (resposta == 6)
            {
                SQLString = " DELETE FROM Tb025_Valor_PL WHERE Id_Portfolio = " + Portfolio + " AND Data_PL >= '" + Data + "';";

              int retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,1);

              if (retorno != 0)
              {
                  SQLString = " INSERT INTO NESTLOG.dbo.Tb211_Log_PL(Id_Usuer,Id_Portfolio,Date_Selected,Host,Type_PL)VALUES(" + NestDLL.NUserControl.Instance.User_Id + "," + Portfolio + ",'" + Data + "',host_name(),'DEL',0)";
                  
                  retorno = CargaDados.curConn.ExecuteNonQuery(SQLString,1);

                  if(retorno ==0 )
                  {
                      MessageBox.Show("Error!");
                  }

                  SQLString = " DELETE FROM Tb000_Historical_Positions WHERE [Id Portfolio]= " + Portfolio + " AND [Date Now] >= '" + Data + "'";

                  int retorno2 = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);

                  if (retorno2 != 0)
                  {
                      MessageBox.Show("Deleted!");
                  }
              }
              else
              {
                  MessageBox.Show("ERROR!");
              }
            }
         }

        void carrega_Combo()
        {
            CargaDados.carregacombo(this.cmbView, "Select Id_Portfolio,Port_Name from  VW_Portfolios where Id_Port_Type=2 UNION ALL SELECT '-1', 'All Portfolios'", "Id_Portfolio", "Port_Name", 99);
       }

        private void frmApagaPL_Load(object sender, EventArgs e)
        {
            
            carrega_Combo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}