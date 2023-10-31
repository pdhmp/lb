namespace SGN
{
    partial class frmOrders
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
            this.tbMenu = new System.Windows.Forms.TabControl();
            this.tbopen = new System.Windows.Forms.TabPage();
            this.dtg2 = new DevExpress.XtraGrid.GridControl();
            this.dgAbertas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tbclose = new System.Windows.Forms.TabPage();
            this.dtgFec = new DevExpress.XtraGrid.GridControl();
            this.dgFechadas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tbcancel = new System.Windows.Forms.TabPage();
            this.dtgCancel = new DevExpress.XtraGrid.GridControl();
            this.dgCanceladas = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tbMenu.SuspendLayout();
            this.tbopen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAbertas)).BeginInit();
            this.tbclose.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgFec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFechadas)).BeginInit();
            this.tbcancel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCanceladas)).BeginInit();
            this.SuspendLayout();
            // 
            // tbMenu
            // 
            this.tbMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbMenu.Controls.Add(this.tbopen);
            this.tbMenu.Controls.Add(this.tbclose);
            this.tbMenu.Controls.Add(this.tbcancel);
            this.tbMenu.Location = new System.Drawing.Point(11, 13);
            this.tbMenu.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbMenu.Name = "tbMenu";
            this.tbMenu.SelectedIndex = 0;
            this.tbMenu.Size = new System.Drawing.Size(1220, 940);
            this.tbMenu.TabIndex = 16;
            // 
            // tbopen
            // 
            this.tbopen.Controls.Add(this.dtg2);
            this.tbopen.Location = new System.Drawing.Point(4, 22);
            this.tbopen.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbopen.Name = "tbopen";
            this.tbopen.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbopen.Size = new System.Drawing.Size(1212, 914);
            this.tbopen.TabIndex = 0;
            this.tbopen.Text = "Open";
            this.tbopen.UseVisualStyleBackColor = true;
            // 
            // dtg2
            // 
            this.dtg2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg2.EmbeddedNavigator.Name = "";
            this.dtg2.Location = new System.Drawing.Point(1, 3);
            this.dtg2.MainView = this.dgAbertas;
            this.dtg2.Name = "dtg2";
            this.dtg2.Size = new System.Drawing.Size(1206, 904);
            this.dtg2.TabIndex = 14;
            this.dtg2.TabStop = false;
            this.dtg2.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgAbertas});
            // 
            // dgAbertas
            // 
            this.dgAbertas.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgAbertas.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.dgAbertas.GridControl = this.dtg2;
            this.dgAbertas.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgAbertas.Name = "dgAbertas";
            this.dgAbertas.OptionsView.ColumnAutoWidth = false;
            this.dgAbertas.RowHeight = 15;
            // 
            // tbclose
            // 
            this.tbclose.Controls.Add(this.dtgFec);
            this.tbclose.Location = new System.Drawing.Point(4, 22);
            this.tbclose.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbclose.Name = "tbclose";
            this.tbclose.Padding = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbclose.Size = new System.Drawing.Size(1212, 914);
            this.tbclose.TabIndex = 1;
            this.tbclose.Text = "Done";
            this.tbclose.UseVisualStyleBackColor = true;
            // 
            // dtgFec
            // 
            this.dtgFec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgFec.EmbeddedNavigator.Name = "";
            this.dtgFec.Location = new System.Drawing.Point(2, 3);
            this.dtgFec.MainView = this.dgFechadas;
            this.dtgFec.Name = "dtgFec";
            this.dtgFec.Size = new System.Drawing.Size(1230, 788);
            this.dtgFec.TabIndex = 15;
            this.dtgFec.TabStop = false;
            this.dtgFec.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgFechadas});
            // 
            // dgFechadas
            // 
            this.dgFechadas.GridControl = this.dtgFec;
            this.dgFechadas.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgFechadas.Name = "dgFechadas";
            this.dgFechadas.OptionsBehavior.Editable = false;
            this.dgFechadas.OptionsView.ColumnAutoWidth = false;
            this.dgFechadas.RowHeight = 15;
            // 
            // tbcancel
            // 
            this.tbcancel.Controls.Add(this.dtgCancel);
            this.tbcancel.Location = new System.Drawing.Point(4, 22);
            this.tbcancel.Margin = new System.Windows.Forms.Padding(2, 4, 2, 4);
            this.tbcancel.Name = "tbcancel";
            this.tbcancel.Size = new System.Drawing.Size(1212, 914);
            this.tbcancel.TabIndex = 2;
            this.tbcancel.Text = "Cancel";
            this.tbcancel.UseVisualStyleBackColor = true;
            // 
            // dtgCancel
            // 
            this.dtgCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgCancel.EmbeddedNavigator.Name = "";
            this.dtgCancel.Location = new System.Drawing.Point(2, 3);
            this.dtgCancel.MainView = this.dgCanceladas;
            this.dtgCancel.Name = "dtgCancel";
            this.dtgCancel.Size = new System.Drawing.Size(1227, 785);
            this.dtgCancel.TabIndex = 15;
            this.dtgCancel.TabStop = false;
            this.dtgCancel.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgCanceladas});
            // 
            // dgCanceladas
            // 
            this.dgCanceladas.GridControl = this.dtgCancel;
            this.dgCanceladas.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgCanceladas.Name = "dgCanceladas";
            this.dgCanceladas.OptionsBehavior.Editable = false;
            this.dgCanceladas.OptionsView.ColumnAutoWidth = false;
            this.dgCanceladas.RowHeight = 15;
            this.dgCanceladas.ShowCustomizationForm += new System.EventHandler(this.dgCanceladas_ShowCustomizationForm);
            this.dgCanceladas.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgCanceladas_DragObjectDrop);
            this.dgCanceladas.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgCanceladas_ColumnWidthChanged);
            this.dgCanceladas.EndGrouping += new System.EventHandler(this.dgCanceladas_EndGrouping);
            this.dgCanceladas.HideCustomizationForm += new System.EventHandler(this.dgCanceladas_HideCustomizationForm);
            // 
            // frmOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 966);
            this.Controls.Add(this.tbMenu);
            this.Name = "frmOrders";
            this.Text = "Open Orders";
            this.Load += new System.EventHandler(this.frmOpen_Load);
            this.tbMenu.ResumeLayout(false);
            this.tbopen.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAbertas)).EndInit();
            this.tbclose.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgFec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFechadas)).EndInit();
            this.tbcancel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtgCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCanceladas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbMenu;
        private System.Windows.Forms.TabPage tbopen;
        private DevExpress.XtraGrid.GridControl dtg2;
        private DevExpress.XtraGrid.Views.Grid.GridView dgAbertas;
        private System.Windows.Forms.TabPage tbclose;
        private DevExpress.XtraGrid.GridControl dtgFec;
        private DevExpress.XtraGrid.Views.Grid.GridView dgFechadas;
        private System.Windows.Forms.TabPage tbcancel;
        private DevExpress.XtraGrid.GridControl dtgCancel;
        private DevExpress.XtraGrid.Views.Grid.GridView dgCanceladas;

    }
}