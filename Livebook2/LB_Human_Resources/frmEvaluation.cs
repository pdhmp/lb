using System;
using System.Data;
using System.Text;
using System.Windows.Forms;
using LiveDLL;
using DevExpress.XtraEditors;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors.Repository;

namespace LiveBook
{
    public partial class frmEvaluation : LBForm
    {
        private int idUser { get { return GlobalVars.Instance.IdUser; } }
        private int idAtividade = 0;
        private DateTime SemestreAtual
        {
            get
            {
                if (cmbDate.Value.Month >= 1 && cmbDate.Value.Month < 6)
                    return new DateTime(cmbDate.Value.Year, 01, 01);
                else
                    return new DateTime(cmbDate.Value.Year, 06, 01);
            }
        }

        string StringSQL = "";
        public frmEvaluation()
        {
            InitializeComponent();

            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");

            dg.ColumnPanelRowHeight = 32;
            dg.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
        }

        private void frmEvaluation_Load(object sender, EventArgs e)
        {
            this.AtualizaCamposTela();
        }

        private void AtualizaCamposTela()
        {
            this.CarregaAvaliador();
            this.CarregaGrid();
            idAtividade = 0;
            lstAvaliador.SelectedIndex = -1;
            txtAtividade.Text = "";
            txtPeso.Text = "";
        }

        private void CarregaGrid()
        {
            try
            {
                using (newNestConn curConn = new newNestConn())
                {
                    dg.Columns.Clear();
                    dtg.Refresh();

                    StringSQL =
                                " SELECT " +
                                " 	A.IdAtividade, " +
                                " 	B.Nome, " +
                                " 	d.Nome AS Avaliador, " +
                                " 	A.DescAtividade AS Descricao, " +
                                " 	CASE " +
                                " 		WHEN MONTH(SemestreRef) = 6 THEN '02-' + CONVERT(varchar, YEAR(SemestreRef)) ELSE '01-' + CONVERT(varchar, YEAR(SemestreRef)) " +
                                " 	END AS Semestre, " +
                                " 	A.Peso, " +
                                " 	E.Status	 " +
                                " FROM  " +
                                " 	[NESTAVALIACAO].[dbo].[Tb_Atividade] A " +
                                " INNER JOIN  " +
                                " 	NESTDB.dbo.Tb014_Pessoas B  " +
                                " 	ON A.IdUsuario = B.Id_Pessoa " +
                                " INNER JOIN  " +
                                " 	NESTAVALIACAO.dbo.Tb_Avaliado_Avaliador C " +
                                " 	ON A.IdAtividade = c.idAtividade " +
                                " INNER JOIN  " +
                                " 	NESTDB.dbo.Tb014_Pessoas D  " +
                                " 	ON d.Id_Pessoa = c.idAvaliador " +
                                " INNER JOIN  " +
                                " 	NESTAVALIACAO.dbo.Tb_Status_Atividade E " +
                                " 	ON A.IdStatus = E.IdStatus " +
                                " WHERE A.IdUsuario = " + idUser +
                                " ORDER BY A.IdStatus , A.Peso DESC";

                    dtg.DataSource = curConn.Return_DataTable(StringSQL);
                }

                dg.Columns.AddField("Concluir");
                dg.Columns["Concluir"].VisibleIndex = 0;
                dg.Columns["Concluir"].Width = 55;
                RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
                item5.Buttons[0].Tag = 1;
                item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item5.Buttons[0].Caption = "Concluir";
                dtg.RepositoryItems.Add(item5);
                dg.Columns["Concluir"].ColumnEdit = item5;
                dg.Columns["Concluir"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
                dg.OptionsBehavior.Editable = false;
                dg.Columns["Concluir"].Visible = true;

                dg.OptionsView.ShowFooter = true;
                dg.Columns[6].SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
                dg.Columns[6].SummaryItem.FieldName = "Peso";
                dg.Columns[6].SummaryItem.DisplayFormat = "Total {0}";


                dg.BestFitColumns();
            }
            catch { }
        }

        private void CarregaAvaliador()
        {
            string SQL = "SELECT Id_Pessoa, Nome FROM Tb014_Pessoas WHERE Active = 1 AND Id_Pessoa NOT IN (20, 79," + idUser + ") ORDER BY Nome";
            LiveDLL.FormUtils.LoadList(this.lstAvaliador, SQL, "Id_Pessoa", "Nome");
        }

        private bool ValidaCamposTela()
        {
            int a;

            txtAtividade.Text = txtAtividade.Text.Replace("'", "");

            if (txtPeso.Text.Trim().Length < 1)
            {
                MessageBox.Show("Insira um peso para a atividade.");
                txtPeso.Select();
                return false;
            }
            else if (!int.TryParse(txtPeso.Text, out a))
            {
                MessageBox.Show("O peso da atividade deve ser um número.");
                txtPeso.Select();
                return false;
            }

            if (txtAtividade.Text.Trim().Length < 7)
            {
                MessageBox.Show("Insira uma atividade.");
                txtAtividade.Select();
                return false;
            }

            if (lstAvaliador.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um Avaliador.");
                return false;
            }

            return true;
        }

        private void InserirAtividade()
        {
            if (!this.ValidaCamposTela())
                return;
            StringSQL = " INSERT INTO [NESTAVALIACAO].[dbo].[TB_Atividade] (  idUsuario, DescAtividade, SemestreRef, Peso , IdStatus ) " +
                         " VALUES (" + idUser + ", '" + txtAtividade.Text + "', '" + SemestreAtual.ToString("yyyy-MM-dd") + "', " + txtPeso.Text + " , 1 ) ";

            using (LiveDLL.newNestConn curConn = new LiveDLL.newNestConn())
            {
                curConn.ExecuteNonQuery(StringSQL.ToString());
                string sTempID = curConn.Execute_Query_String("SELECT @@IDENTITY");

                foreach (DataRowView objDataRowView in lstAvaliador.SelectedItems)
                {
                    StringSQL = "";
                    StringSQL = " INSERT INTO [NESTAVALIACAO].[dbo].[TB_Avaliado_Avaliador] ( idAvaliado, idAvaliador, idAtividade, Nota ) " +
                                " VALUES (" + idUser + ", " + objDataRowView["Id_Pessoa"].ToString() + ", " + sTempID + ", NULL )";

                    curConn.ExecuteNonQuery(StringSQL);
                }
            }

            this.AtualizaCamposTela();
        }

        private void AtualizarAtividade()
        {
            if (!this.ValidaCamposTela())
                return;

            StringSQL = " UPDATE [NESTAVALIACAO].[dbo].[tb_Atividade] " +
                        " SET DescAtividade='" + txtAtividade.Text + "', Peso=" + txtPeso.Text +
                        " WHERE idUsuario =" + idUser + " AND idAtividade=" + idAtividade + " ; ";

            StringSQL += "DELETE [NESTAVALIACAO].[dbo].[tb_Avaliado_Avaliador] WHERE idAvaliado =" + idUser + " AND idAtividade=" + idAtividade + " ; ";

            foreach (DataRowView objDataRowView in lstAvaliador.SelectedItems)
            {
                StringSQL += " INSERT INTO [NESTAVALIACAO].[dbo].[tb_Avaliado_Avaliador] ( idAvaliado, idAvaliador, idAtividade, Nota ) " +
                            " VALUES (" + idUser + ", " + objDataRowView["Id_Pessoa"].ToString() + ", " + idAtividade + ", NULL ) ; ";
            }

            using (LiveDLL.newNestConn curConn = new LiveDLL.newNestConn())
            {
                curConn.ExecuteNonQuery(StringSQL);
            }
            this.AtualizaCamposTela();
        }

        private void DeletarAtividade()
        {
            if (DevExpress.XtraEditors.XtraMessageBox.Show("Deseja cancelar esta atividade?", "Exclusão de Atividades", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
            if (idAtividade == 0)
            {
                XtraMessageBox.Show("Selecione uma Atividade para Cancelar");
                return;
            }

            StringSQL = " UPDATE [NESTAVALIACAO].[dbo].[tb_Atividade]   SET IdStatus = 3       WHERE idUsuario =" + idUser + " AND idAtividade=" + idAtividade + " ; ";

            using (LiveDLL.newNestConn curConn = new LiveDLL.newNestConn()) { curConn.ExecuteNonQuery(StringSQL); }

            this.AtualizaCamposTela();
        }

        private void SelecionaAvaliador()
        {
            using (LiveDLL.newNestConn curConn = new LiveDLL.newNestConn())
            {
                int Index = -1;
                StringSQL = " SELECT a.idAvaliador, p.Nome " +
                            " FROM [NESTAVALIACAO].[dbo].[tb_Avaliado_Avaliador] a, [NESTDB].[dbo].[Tb014_Pessoas] p " +
                            " WHERE a.idAtividade = " + idAtividade + " AND a.idAvaliador = p.Id_Pessoa ; ";

                System.Data.SqlClient.SqlDataReader dr = curConn.Return_DataReader(StringSQL);

                while (dr.Read())
                {
                    Index = lstAvaliador.FindString(dr["Nome"].ToString());
                    if (Index != -1)
                    {
                        lstAvaliador.SetSelected(Index, true);
                    }
                }
            }
        }

        private void dtg_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (dg.DataRowCount > 0)
                {
                    txtAtividade.Text = dg.GetFocusedRowCellValue("Descricao").ToString();
                    txtPeso.Text = dg.GetFocusedRowCellValue("Peso").ToString();
                    idAtividade = Convert.ToInt32(dg.GetFocusedRowCellValue("IdAtividade"));

                    lstAvaliador.SelectedIndex = -1;
                    this.SelecionaAvaliador();
                }
            }
            catch { }
        }

        private void cmdInserir_Click(object sender, EventArgs e)
        {
            this.InserirAtividade();
        }

        private void cmdAtualizar_Click(object sender, EventArgs e)
        {
            if (idAtividade == 0)
            {
                MessageBox.Show("Selecione uma Atividade para Atualizar");
                return;
            }

            this.AtualizarAtividade();
        }

        private void cmdDeletar_Click(object sender, EventArgs e)
        {

            this.DeletarAtividade();

        }

        private void cmdCancela_Click(object sender, EventArgs e)
        {
            this.AtualizaCamposTela();
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dg.SelectAll();
            dg.CopyToClipboard();

            MessageBox.Show("Registros copiados");
        }

        private void dg_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            int TempVal = e.RowHandle;
            GridView curView = (GridView)sender;

            e.Appearance.ForeColor = Color.Black;

            if (curView.GetRowCellValue(TempVal, "Status").ToString() == "Completa") { e.Appearance.BackColor = Color.LightBlue; }
            if (curView.GetRowCellValue(TempVal, "Status").ToString() == "Pendente") { e.Appearance.BackColor = Color.WhiteSmoke; }
            if (curView.GetRowCellValue(TempVal, "Status").ToString() == "Cancelada") { e.Appearance.BackColor = Color.Red; }
        }

        private void dg_DoubleClick(object sender, EventArgs e)
        {
            string Column_Name = dg.FocusedColumn.ToString();
            GridView curView = (GridView)sender;

            if (dg.GetFocusedRowCellValue("Status").ToString() != "Pendente") return;

            if (Column_Name == "Concluir")
            {
                frmNota nota = new frmNota(idAtividade, idUser);
                nota.OnFinish += new EventHandler(OnFinish);
                nota.Show();

            }
        }

        private void OnFinish(object sender, EventArgs e)
        {
            AtualizaCamposTela();
        }
    }
}
