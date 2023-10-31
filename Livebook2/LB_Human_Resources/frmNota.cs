using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LiveBook
{
    public partial class frmNota : LBForm
    {
        private int IdAtividade;
        private int IdUser;

        public frmNota(int IdAtividade, int IdUser)
        {
            this.IdAtividade = IdAtividade;
            this.IdUser = IdUser;
            InitializeComponent();
        }

        public EventHandler OnFinish;

        private void btnInserir_Click(object sender, EventArgs e)
        {
            int Nota;

            if (!int.TryParse(txtNota.Text, out Nota))
            {
                MessageBox.Show("A nota deve ser um numero", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNota.Text = "";
                return;
            }

            if (Nota < 1 || Nota > 5)
            {
                txtNota.Text = "";
                MessageBox.Show("Insira uma nota de 1 a 5", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (DevExpress.XtraEditors.XtraMessageBox.Show("Deseja concluir esta atividade?", "Concluir Atividades", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string SQLString = "UPDATE [NESTAVALIACAO].[dbo].[Tb_Atividade] SET IdStatus = 2, AutoAvaliacao = " + txtNota.Text + " WHERE IdAtividade = " + IdAtividade + " AND IdUsuario = " + IdUser + " ;";

            LiveDLL.newNestConn curConn = new LiveDLL.newNestConn();

            curConn.ExecuteNonQuery(SQLString);

            if (OnFinish != null)
            {
                OnFinish(this, new EventArgs());
            }

            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
