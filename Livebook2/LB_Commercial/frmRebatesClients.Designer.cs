namespace LiveBook
{
    partial class frmRebateClients
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRebateClients));
            this.dtgRebates = new MyXtraGrid.MyGridControl();
            this.dgRebates = new MyXtraGrid.MyGridView();
            this.myGridView2 = new MyXtraGrid.MyGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblImportFile = new System.Windows.Forms.Label();
            this.cmdTxMellon = new System.Windows.Forms.Button();
            this.cmdExcel = new System.Windows.Forms.Button();
            this.lblCopy = new System.Windows.Forms.Label();
            this.CmbDate = new System.Windows.Forms.ComboBox();
            this.CmdLoad = new System.Windows.Forms.Button();
            this.cmdDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtgRebates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRebates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dtgRebates
            // 
            this.dtgRebates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgRebates.Location = new System.Drawing.Point(12, 79);
            this.dtgRebates.LookAndFeel.SkinName = "Blue";
            this.dtgRebates.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Style3D;
            this.dtgRebates.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgRebates.MainView = this.dgRebates;
            this.dtgRebates.Name = "dtgRebates";
            this.dtgRebates.Size = new System.Drawing.Size(1013, 490);
            this.dtgRebates.TabIndex = 25;
            this.dtgRebates.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgRebates,
            this.myGridView2});
            // 
            // dgRebates
            // 
            this.dgRebates.GridControl = this.dtgRebates;
            this.dgRebates.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgRebates.Name = "dgRebates";
            this.dgRebates.OptionsBehavior.Editable = false;
            this.dgRebates.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgRebates.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.dgRebates.OptionsSelection.EnableAppearanceHideSelection = false;
            this.dgRebates.OptionsSelection.InvertSelection = true;
            this.dgRebates.OptionsSelection.MultiSelect = true;
            this.dgRebates.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dgRebates.OptionsSelection.UseIndicatorForSelection = false;
            this.dgRebates.OptionsView.ColumnAutoWidth = false;
            this.dgRebates.OptionsView.ShowIndicator = false;
            this.dgRebates.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgRebates_DragObjectDrop);
            this.dgRebates.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgAttribution_CustomDrawGroupRow);
            this.dgRebates.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgRebates_ColumnWidthChanged);
            // 
            // myGridView2
            // 
            this.myGridView2.GridControl = this.dtgRebates;
            this.myGridView2.Name = "myGridView2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(77)))), ((int)(((byte)(117)))), ((int)(((byte)(170)))));
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "Initial Date:";
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Location = new System.Drawing.Point(373, 40);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(59, 22);
            this.cmdCollapse.TabIndex = 31;
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCollapse.UseVisualStyleBackColor = false;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            this.cmdCollapse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdCollapse_MouseDown);
            // 
            // cmdExpand
            // 
            this.cmdExpand.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExpand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand.Location = new System.Drawing.Point(373, 12);
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(59, 22);
            this.cmdExpand.TabIndex = 30;
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExpand.UseVisualStyleBackColor = false;
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            this.cmdExpand.MouseDown += new System.Windows.Forms.MouseEventHandler(this.cmdExpand_MouseDown);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblImportFile);
            this.groupBox3.Location = new System.Drawing.Point(800, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(227, 63);
            this.groupBox3.TabIndex = 85;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import File";
            // 
            // lblImportFile
            // 
            this.lblImportFile.AllowDrop = true;
            this.lblImportFile.Location = new System.Drawing.Point(6, 19);
            this.lblImportFile.Name = "lblImportFile";
            this.lblImportFile.Size = new System.Drawing.Size(215, 33);
            this.lblImportFile.TabIndex = 29;
            this.lblImportFile.Text = "Drag file here to import it into the system";
            this.lblImportFile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblImportFile.Click += new System.EventHandler(this.lblImportFile_Click);
            this.lblImportFile.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblImportFile_DragDrop);
            this.lblImportFile.DragOver += new System.Windows.Forms.DragEventHandler(this.lblImportFile_DragOver);
            // 
            // cmdTxMellon
            // 
            this.cmdTxMellon.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdTxMellon.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdTxMellon.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdTxMellon.Location = new System.Drawing.Point(691, 35);
            this.cmdTxMellon.Name = "cmdTxMellon";
            this.cmdTxMellon.Size = new System.Drawing.Size(94, 38);
            this.cmdTxMellon.TabIndex = 86;
            this.cmdTxMellon.Text = "Insert Percent Tx Mellon";
            this.cmdTxMellon.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdTxMellon.UseVisualStyleBackColor = false;
            this.cmdTxMellon.Click += new System.EventHandler(this.cmdTxMellon_Click);
            // 
            // cmdExcel
            // 
            this.cmdExcel.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExcel.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.cmdExcel.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.cmdExcel.FlatAppearance.BorderSize = 0;
            this.cmdExcel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExcel.Location = new System.Drawing.Point(469, 12);
            this.cmdExcel.Name = "cmdExcel";
            this.cmdExcel.Size = new System.Drawing.Size(59, 22);
            this.cmdExcel.TabIndex = 87;
            this.cmdExcel.Text = "Excel";
            this.cmdExcel.UseVisualStyleBackColor = false;
            this.cmdExcel.Click += new System.EventHandler(this.cmdExcel_Click);
            // 
            // lblCopy
            // 
            this.lblCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCopy.AutoSize = true;
            this.lblCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.Color.Blue;
            this.lblCopy.Location = new System.Drawing.Point(987, 576);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(31, 13);
            this.lblCopy.TabIndex = 88;
            this.lblCopy.Text = "Copy";
            this.lblCopy.Click += new System.EventHandler(this.Copy_Click);
            // 
            // CmbDate
            // 
            this.CmbDate.FormattingEnabled = true;
            this.CmbDate.Location = new System.Drawing.Point(80, 9);
            this.CmbDate.Name = "CmbDate";
            this.CmbDate.Size = new System.Drawing.Size(121, 21);
            this.CmbDate.TabIndex = 89;
            // 
            // CmdLoad
            // 
            this.CmdLoad.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CmdLoad.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.CmdLoad.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.CmdLoad.FlatAppearance.BorderSize = 0;
            this.CmdLoad.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CmdLoad.Location = new System.Drawing.Point(207, 8);
            this.CmdLoad.Name = "CmdLoad";
            this.CmdLoad.Size = new System.Drawing.Size(59, 22);
            this.CmdLoad.TabIndex = 90;
            this.CmdLoad.Text = "Load";
            this.CmdLoad.UseVisualStyleBackColor = false;
            this.CmdLoad.Click += new System.EventHandler(this.CmdLoad_Click);
            // 
            // cmdDelete
            // 
            this.cmdDelete.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.Location = new System.Drawing.Point(570, 35);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(94, 38);
            this.cmdDelete.TabIndex = 91;
            this.cmdDelete.Text = "Delete imported files";
            this.cmdDelete.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdDelete.UseVisualStyleBackColor = false;
            this.cmdDelete.Click += new System.EventHandler(this.cmdDelete_Click);
            // 
            // frmRebateClients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1039, 595);
            this.Controls.Add(this.cmdDelete);
            this.Controls.Add(this.CmdLoad);
            this.Controls.Add(this.CmbDate);
            this.Controls.Add(this.lblCopy);
            this.Controls.Add(this.cmdExcel);
            this.Controls.Add(this.cmdTxMellon);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dtgRebates);
            this.Controls.Add(this.cmdExpand);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmdCollapse);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRebateClients";
            this.Text = "Client Rebates";
            this.Load += new System.EventHandler(this.frmRebateClients_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgRebates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRebates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridControl dtgRebates;
        private MyXtraGrid.MyGridView dgRebates;
        private MyXtraGrid.MyGridView myGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.Button cmdExpand;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblImportFile;
        private System.Windows.Forms.Button cmdTxMellon;
        private System.Windows.Forms.Button cmdExcel;
        private System.Windows.Forms.Label lblCopy;
        private System.Windows.Forms.ComboBox CmbDate;
        private System.Windows.Forms.Button CmdLoad;
        private System.Windows.Forms.Button cmdDelete;


    }
}