namespace LiveBook
{
    partial class frmSubscription
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
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtTicker = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblVlrEmissao = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTransType = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTicker = new System.Windows.Forms.Label();
            this.cmdNewSecurity = new System.Windows.Forms.Button();
            this.dtg = new MyXtraGrid.MyGridControl();
            this.dg = new MyXtraGrid.MyGridView();
            this.cmdCreateTrades = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(204, 33);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(83, 20);
            this.txtPrice.TabIndex = 8;
            this.txtPrice.Text = "0,000000001";
            // 
            // txtTicker
            // 
            this.txtTicker.Location = new System.Drawing.Point(57, 33);
            this.txtTicker.Name = "txtTicker";
            this.txtTicker.Size = new System.Drawing.Size(63, 20);
            this.txtTicker.TabIndex = 8;
            this.txtTicker.Text = "ITSA2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(447, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Strike :";
            // 
            // lblVlrEmissao
            // 
            this.lblVlrEmissao.AutoSize = true;
            this.lblVlrEmissao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVlrEmissao.Location = new System.Drawing.Point(490, 9);
            this.lblVlrEmissao.Name = "lblVlrEmissao";
            this.lblVlrEmissao.Size = new System.Drawing.Size(32, 13);
            this.lblVlrEmissao.TabIndex = 6;
            this.lblVlrEmissao.Text = "0,00";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(325, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Percent :";
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercent.Location = new System.Drawing.Point(377, 9);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(16, 13);
            this.lblPercent.TabIndex = 4;
            this.lblPercent.Text = "%";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(136, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Price :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "TransType :";
            // 
            // lblTransType
            // 
            this.lblTransType.AutoSize = true;
            this.lblTransType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTransType.Location = new System.Drawing.Point(201, 9);
            this.lblTransType.Name = "lblTransType";
            this.lblTransType.Size = new System.Drawing.Size(86, 13);
            this.lblTransType.TabIndex = 2;
            this.lblTransType.Text = "SUBSCRICAO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "Ticker : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ticker : ";
            // 
            // lblTicker
            // 
            this.lblTicker.AutoSize = true;
            this.lblTicker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTicker.Location = new System.Drawing.Point(57, 9);
            this.lblTicker.Name = "lblTicker";
            this.lblTicker.Size = new System.Drawing.Size(42, 13);
            this.lblTicker.TabIndex = 0;
            this.lblTicker.Text = "ITSA4";
            // 
            // cmdNewSecurity
            // 
            this.cmdNewSecurity.Location = new System.Drawing.Point(328, 31);
            this.cmdNewSecurity.Name = "cmdNewSecurity";
            this.cmdNewSecurity.Size = new System.Drawing.Size(118, 23);
            this.cmdNewSecurity.TabIndex = 9;
            this.cmdNewSecurity.Text = "Create New Security";
            this.cmdNewSecurity.UseVisualStyleBackColor = true;
            this.cmdNewSecurity.Click += new System.EventHandler(this.cmdNewSecurity_Click);
            // 
            // dtg
            // 
            this.dtg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtg.Location = new System.Drawing.Point(15, 60);
            this.dtg.LookAndFeel.SkinName = "Blue";
            this.dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Flat;
            this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            this.dtg.MainView = this.dg;
            this.dtg.Name = "dtg";
            this.dtg.Size = new System.Drawing.Size(554, 311);
            this.dtg.TabIndex = 31;
            this.dtg.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.dg});
            // 
            // dg
            // 
            this.dg.GridControl = this.dtg;
            this.dg.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.Hidden;
            this.dg.Name = "dg";
            this.dg.OptionsBehavior.Editable = false;
            this.dg.OptionsSelection.MultiSelect = true;
            this.dg.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.dg.OptionsView.ShowGroupPanel = false;
            // 
            // cmdCreateTrades
            // 
            this.cmdCreateTrades.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCreateTrades.Location = new System.Drawing.Point(451, 379);
            this.cmdCreateTrades.Name = "cmdCreateTrades";
            this.cmdCreateTrades.Size = new System.Drawing.Size(118, 23);
            this.cmdCreateTrades.TabIndex = 9;
            this.cmdCreateTrades.Text = "Create Trades";
            this.cmdCreateTrades.UseVisualStyleBackColor = true;
            this.cmdCreateTrades.Click += new System.EventHandler(this.cmdCreateTrades_Click);
            // 
            // frmSubscription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 412);
            this.Controls.Add(this.dtg);
            this.Controls.Add(this.cmdCreateTrades);
            this.Controls.Add(this.cmdNewSecurity);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtTicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblVlrEmissao);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTransType);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTicker);
            this.Name = "frmSubscription";
            this.Text = "Subscription";
            this.Load += new System.EventHandler(this.frmSubscription_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblTransType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblVlrEmissao;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPercent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtTicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Button cmdNewSecurity;
        private MyXtraGrid.MyGridControl dtg;
        private MyXtraGrid.MyGridView dg;
        private System.Windows.Forms.Button cmdCreateTrades;
    }
}