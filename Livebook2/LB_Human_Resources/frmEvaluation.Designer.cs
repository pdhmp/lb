namespace LiveBook
{
    partial class frmEvaluation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtAtividade = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPeso = new System.Windows.Forms.TextBox();
            this.cmdInserir = new System.Windows.Forms.Button();
            this.cmdDeletar = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dtg = new DevExpress.XtraGrid.GridControl();
            this.dg = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cmdAtualizar = new System.Windows.Forms.Button();
            this.lstAvaliador = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmdCancela = new System.Windows.Forms.Button();
            this.lblCopy = new System.Windows.Forms.Label();
            this.cmbDate = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Atividade";
            // 
            // txtAtividade
            // 
            this.txtAtividade.Location = new System.Drawing.Point(14, 55);
            this.txtAtividade.Multiline = true;
            this.txtAtividade.Name = "txtAtividade";
            this.txtAtividade.Size = new System.Drawing.Size(450, 77);
            this.txtAtividade.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 229);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Peso";
            // 
            // txtPeso
            // 
            this.txtPeso.Location = new System.Drawing.Point(47, 225);
            this.txtPeso.Name = "txtPeso";
            this.txtPeso.Size = new System.Drawing.Size(29, 20);
            this.txtPeso.TabIndex = 3;
            this.txtPeso.Text = "10";
            // 
            // cmdInserir
            // 
            this.cmdInserir.Location = new System.Drawing.Point(82, 223);
            this.cmdInserir.Name = "cmdInserir";
            this.cmdInserir.Size = new System.Drawing.Size(92, 23);
            this.cmdInserir.TabIndex = 5;
            this.cmdInserir.Text = "Inserir";
            this.cmdInserir.UseVisualStyleBackColor = true;
            this.cmdInserir.Click += new System.EventHandler(this.cmdInserir_Click);
            // 
            // cmdDeletar
            // 
            this.cmdDeletar.Location = new System.Drawing.Point(180, 223);
            this.cmdDeletar.Name = "cmdDeletar";
            this.cmdDeletar.Size = new System.Drawing.Size(92, 23);
            this.cmdDeletar.TabIndex = 5;
            this.cmdDeletar.Text = "Deletar";
            this.cmdDeletar.UseVisualStyleBackColor = true;
            this.cmdDeletar.Click += new System.EventHandler(this.cmdDeletar_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(12, 714);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(58, 13);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Total : 100";
            this.lblTotal.Visible = false;
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(14, 405);
            this.dtg.MainView = this.dg;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(450, 238);
            this.dtg.TabIndex = 34;
            this.dtg.TabStop = false;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dg});
            this.dtg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dtg_MouseClick);
            // 
            // dg
            // 
            this.dg.GridControl = this.dtg;
            this.dg.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dg.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dg.Name = "dg";
            this.dg.OptionsBehavior.Editable = false;
            this.dg.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dg.OptionsView.ColumnAutoWidth = false;
            this.dg.OptionsView.ShowFooter = true;
            this.dg.RowHeight = 15;
            this.dg.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dg_CustomDrawCell);
            this.dg.DoubleClick += new System.EventHandler(this.dg_DoubleClick);
            // 
            // cmdAtualizar
            // 
            this.cmdAtualizar.Location = new System.Drawing.Point(278, 223);
            this.cmdAtualizar.Name = "cmdAtualizar";
            this.cmdAtualizar.Size = new System.Drawing.Size(92, 23);
            this.cmdAtualizar.TabIndex = 5;
            this.cmdAtualizar.Text = "Atualizar";
            this.cmdAtualizar.UseVisualStyleBackColor = true;
            this.cmdAtualizar.Click += new System.EventHandler(this.cmdAtualizar_Click);
            // 
            // lstAvaliador
            // 
            this.lstAvaliador.FormattingEnabled = true;
            this.lstAvaliador.Location = new System.Drawing.Point(14, 151);
            this.lstAvaliador.Name = "lstAvaliador";
            this.lstAvaliador.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstAvaliador.Size = new System.Drawing.Size(450, 69);
            this.lstAvaliador.TabIndex = 35;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Avaliador";
            // 
            // cmdCancela
            // 
            this.cmdCancela.Location = new System.Drawing.Point(376, 223);
            this.cmdCancela.Name = "cmdCancela";
            this.cmdCancela.Size = new System.Drawing.Size(92, 23);
            this.cmdCancela.TabIndex = 5;
            this.cmdCancela.Text = "Cancelar";
            this.cmdCancela.UseVisualStyleBackColor = true;
            this.cmdCancela.Click += new System.EventHandler(this.cmdCancela_Click);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(433, 646);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 36;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.lblCopy_Click);
            // 
            // cmbDate
            // 
            this.cmbDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cmbDate.Location = new System.Drawing.Point(47, 9);
            this.cmbDate.Name = "cmbDate";
            this.cmbDate.Size = new System.Drawing.Size(82, 20);
            this.cmbDate.TabIndex = 37;
            this.cmbDate.Value = new System.DateTime(2013, 12, 2, 14, 11, 44, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Data";
            // 
            // frmEvaluation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 668);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbDate);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.lstAvaliador);
            this.Controls.Add(this.dtg);
            this.Controls.Add(this.cmdDeletar);
            this.Controls.Add(this.cmdCancela);
            this.Controls.Add(this.cmdAtualizar);
            this.Controls.Add(this.cmdInserir);
            this.Controls.Add(this.txtPeso);
            this.Controls.Add(this.txtAtividade);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "frmEvaluation";
            this.Text = "Avaliacao Semestral";
            this.Load += new System.EventHandler(this.frmEvaluation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAtividade;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPeso;
        private System.Windows.Forms.Button cmdInserir;
        private System.Windows.Forms.Button cmdDeletar;
        private System.Windows.Forms.Label lblTotal;
        private DevExpress.XtraGrid.GridControl dtg;
        private DevExpress.XtraGrid.Views.Grid.GridView dg;
        private System.Windows.Forms.Button cmdAtualizar;
        private System.Windows.Forms.ListBox lstAvaliador;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button cmdCancela;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.DateTimePicker cmbDate;
        private System.Windows.Forms.Label label2;
    }
}