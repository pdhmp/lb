namespace LiveBook
{
    partial class frmOrderReview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmOrderReview));
            this.myGridView2 = new MyXtraGrid.MyGridView();
            this.myGridView3 = new MyXtraGrid.MyGridView();
            this.cmdRefresh = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.radCancelled = new System.Windows.Forms.RadioButton();
            this.radDone = new System.Windows.Forms.RadioButton();
            this.radOpen = new System.Windows.Forms.RadioButton();
            this.radAll = new System.Windows.Forms.RadioButton();
            this.cmdCollapse = new System.Windows.Forms.Button();
            this.cmdExpand = new System.Windows.Forms.Button();
            this.dtgOrderReview = new MyXtraGrid.MyGridControl();
            this.dgOrderReview = new MyXtraGrid.MyGridView();
            this.myGridView4 = new MyXtraGrid.MyGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.lblAtualizar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrderReview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrderReview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView4)).BeginInit();
            this.SuspendLayout();
            // 
            // myGridView2
            // 
            this.myGridView2.Name = "myGridView2";
            // 
            // myGridView3
            // 
            this.myGridView3.Name = "myGridView3";
            // 
            // cmdRefresh
            // 
            this.cmdRefresh.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdRefresh.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdRefresh.Location = new System.Drawing.Point(671, 12);
            this.cmdRefresh.Name = "cmdRefresh";
            this.cmdRefresh.Size = new System.Drawing.Size(72, 22);
            this.cmdRefresh.TabIndex = 49;
            this.cmdRefresh.Text = "Refresh";
            this.cmdRefresh.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdRefresh.UseVisualStyleBackColor = false;
            this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 48;
            this.label1.Text = "Filter Status:";
            // 
            // radCancelled
            // 
            this.radCancelled.AutoSize = true;
            this.radCancelled.Location = new System.Drawing.Point(259, 13);
            this.radCancelled.Name = "radCancelled";
            this.radCancelled.Size = new System.Drawing.Size(72, 17);
            this.radCancelled.TabIndex = 47;
            this.radCancelled.TabStop = true;
            this.radCancelled.Text = "Cancelled";
            this.radCancelled.UseVisualStyleBackColor = true;
            this.radCancelled.Click += new System.EventHandler(this.radCancelled_CheckedChanged);
            // 
            // radDone
            // 
            this.radDone.AutoSize = true;
            this.radDone.Location = new System.Drawing.Point(202, 13);
            this.radDone.Name = "radDone";
            this.radDone.Size = new System.Drawing.Size(51, 17);
            this.radDone.TabIndex = 46;
            this.radDone.TabStop = true;
            this.radDone.Text = "Done";
            this.radDone.UseVisualStyleBackColor = true;
            this.radDone.Click += new System.EventHandler(this.radDone_CheckedChanged);
            // 
            // radOpen
            // 
            this.radOpen.AutoSize = true;
            this.radOpen.Location = new System.Drawing.Point(145, 13);
            this.radOpen.Name = "radOpen";
            this.radOpen.Size = new System.Drawing.Size(51, 17);
            this.radOpen.TabIndex = 45;
            this.radOpen.TabStop = true;
            this.radOpen.Text = "Open";
            this.radOpen.UseVisualStyleBackColor = true;
            this.radOpen.Click += new System.EventHandler(this.radOpen_CheckedChanged);
            // 
            // radAll
            // 
            this.radAll.AutoSize = true;
            this.radAll.Location = new System.Drawing.Point(103, 13);
            this.radAll.Name = "radAll";
            this.radAll.Size = new System.Drawing.Size(36, 17);
            this.radAll.TabIndex = 44;
            this.radAll.TabStop = true;
            this.radAll.Text = "All";
            this.radAll.UseVisualStyleBackColor = true;
            this.radAll.Click += new System.EventHandler(this.radAll_CheckedChanged);
            // 
            // cmdCollapse
            // 
            this.cmdCollapse.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdCollapse.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdCollapse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdCollapse.Location = new System.Drawing.Point(348, 12);
            this.cmdCollapse.Name = "cmdCollapse";
            this.cmdCollapse.Size = new System.Drawing.Size(72, 22);
            this.cmdCollapse.TabIndex = 43;
            this.cmdCollapse.Text = "Collapse";
            this.cmdCollapse.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdCollapse.UseVisualStyleBackColor = false;
            this.cmdCollapse.Click += new System.EventHandler(this.cmdCollapse_Click);
            // 
            // cmdExpand
            // 
            this.cmdExpand.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cmdExpand.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmdExpand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdExpand.Location = new System.Drawing.Point(426, 12);
            this.cmdExpand.Name = "cmdExpand";
            this.cmdExpand.Size = new System.Drawing.Size(72, 22);
            this.cmdExpand.TabIndex = 42;
            this.cmdExpand.Text = "Expand";
            this.cmdExpand.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.cmdExpand.UseVisualStyleBackColor = false;
            this.cmdExpand.Click += new System.EventHandler(this.cmdExpand_Click);
            // 
            // dtgOrderReview
            // 
            this.dtgOrderReview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgOrderReview.Location = new System.Drawing.Point(12, 40);
            this.dtgOrderReview.LookAndFeel.SkinName = "Blue";
            this.dtgOrderReview.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtgOrderReview.MainView = this.dgOrderReview;
            this.dtgOrderReview.Name = "dtgOrderReview";
            this.dtgOrderReview.Size = new System.Drawing.Size(896, 506);
            this.dtgOrderReview.TabIndex = 41;
            this.dtgOrderReview.TabStop = false;
            this.dtgOrderReview.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dgOrderReview,
            this.myGridView4});
            // 
            // dgOrderReview
            // 
            this.dgOrderReview.Appearance.ColumnFilterButton.Font = new System.Drawing.Font("Tahoma", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgOrderReview.Appearance.ColumnFilterButton.Options.UseFont = true;
            this.dgOrderReview.GridControl = this.dtgOrderReview;
            this.dgOrderReview.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dgOrderReview.HorzScrollVisibility = DevExpress.XtraGrid.Views.Base.ScrollVisibility.Always;
            this.dgOrderReview.Name = "dgOrderReview";
            this.dgOrderReview.OptionsBehavior.Editable = false;
            this.dgOrderReview.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.dgOrderReview.OptionsView.ColumnAutoWidth = false;
            this.dgOrderReview.RowHeight = 15;
            this.dgOrderReview.DragObjectDrop += new DevExpress.XtraGrid.Views.Base.DragObjectDropEventHandler(this.dgOrderReview_DragObjectDrop);
            this.dgOrderReview.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.dgOrderReview_CustomDrawCell);
            this.dgOrderReview.CustomDrawGroupRow += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.dgOrderReview_CustomDrawGroupRow);
            this.dgOrderReview.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.dgOrderReview_MasterRowExpanded);
            this.dgOrderReview.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(this.dgOrderReview_ColumnWidthChanged);
            this.dgOrderReview.DoubleClick += new System.EventHandler(this.dgOrderReview_DoubleClick);
            // 
            // myGridView4
            // 
            this.myGridView4.GridControl = this.dtgOrderReview;
            this.myGridView4.Name = "myGridView4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(516, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "Lines to update:";
            // 
            // lblAtualizar
            // 
            this.lblAtualizar.AutoSize = true;
            this.lblAtualizar.Location = new System.Drawing.Point(605, 17);
            this.lblAtualizar.Name = "lblAtualizar";
            this.lblAtualizar.Size = new System.Drawing.Size(13, 13);
            this.lblAtualizar.TabIndex = 51;
            this.lblAtualizar.Text = "0";
            // 
            // frmOrderReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(920, 558);
            this.Controls.Add(this.lblAtualizar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmdRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radCancelled);
            this.Controls.Add(this.radDone);
            this.Controls.Add(this.radOpen);
            this.Controls.Add(this.radAll);
            this.Controls.Add(this.cmdCollapse);
            this.Controls.Add(this.cmdExpand);
            this.Controls.Add(this.dtgOrderReview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmOrderReview";
            this.Text = "Order Review";
            this.Load += new System.EventHandler(this.frmOpen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.myGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtgOrderReview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrderReview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.myGridView4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyXtraGrid.MyGridView myGridView2;
        private MyXtraGrid.MyGridView myGridView3;
        private System.Windows.Forms.Button cmdRefresh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radCancelled;
        private System.Windows.Forms.RadioButton radDone;
        private System.Windows.Forms.RadioButton radOpen;
        private System.Windows.Forms.RadioButton radAll;
        private System.Windows.Forms.Button cmdCollapse;
        private System.Windows.Forms.Button cmdExpand;
        private MyXtraGrid.MyGridControl dtgOrderReview;
        private MyXtraGrid.MyGridView dgOrderReview;
        private MyXtraGrid.MyGridView myGridView4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAtualizar;


    }
}