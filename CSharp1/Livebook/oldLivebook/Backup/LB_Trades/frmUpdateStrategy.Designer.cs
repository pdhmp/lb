namespace SGN
{
    partial class frmUpdateStrategy
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
            this.lblQuantity = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTicker = new System.Windows.Forms.Label();
            this.label03 = new System.Windows.Forms.Label();
            this.lblIdOrder = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbSection = new System.Windows.Forms.ComboBox();
            this.cmbBook = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.cmdClose = new System.Windows.Forms.Button();
            this.cmdInsertAloc = new System.Windows.Forms.Button();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblQuantity
            // 
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Location = new System.Drawing.Point(91, 41);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(0, 13);
            this.lblQuantity.TabIndex = 37;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 36;
            this.label6.Text = "Order Quantity:";
            // 
            // lblTicker
            // 
            this.lblTicker.AutoSize = true;
            this.lblTicker.Location = new System.Drawing.Point(168, 16);
            this.lblTicker.Name = "lblTicker";
            this.lblTicker.Size = new System.Drawing.Size(0, 13);
            this.lblTicker.TabIndex = 35;
            // 
            // label03
            // 
            this.label03.AutoSize = true;
            this.label03.Location = new System.Drawing.Point(122, 16);
            this.label03.Name = "label03";
            this.label03.Size = new System.Drawing.Size(40, 13);
            this.label03.TabIndex = 34;
            this.label03.Text = "Ticker:";
            // 
            // lblIdOrder
            // 
            this.lblIdOrder.AutoSize = true;
            this.lblIdOrder.Location = new System.Drawing.Point(71, 16);
            this.lblIdOrder.Name = "lblIdOrder";
            this.lblIdOrder.Size = new System.Drawing.Size(0, 13);
            this.lblIdOrder.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 32;
            this.label4.Text = "ID Order:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(246, 41);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(0, 13);
            this.lblPrice.TabIndex = 39;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(182, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 38;
            this.label2.Text = "Order Price:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cmbSection);
            this.groupBox6.Controls.Add(this.cmbBook);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Location = new System.Drawing.Point(7, 57);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(464, 53);
            this.groupBox6.TabIndex = 40;
            this.groupBox6.TabStop = false;
            // 
            // cmbSection
            // 
            this.cmbSection.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbSection.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbSection.FormattingEnabled = true;
            this.cmbSection.Location = new System.Drawing.Point(299, 23);
            this.cmbSection.Name = "cmbSection";
            this.cmbSection.Size = new System.Drawing.Size(141, 21);
            this.cmbSection.TabIndex = 13;
            // 
            // cmbBook
            // 
            this.cmbBook.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbBook.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbBook.FormattingEnabled = true;
            this.cmbBook.Location = new System.Drawing.Point(60, 23);
            this.cmbBook.Name = "cmbBook";
            this.cmbBook.Size = new System.Drawing.Size(141, 21);
            this.cmbBook.TabIndex = 12;
            this.cmbBook.SelectedValueChanged += new System.EventHandler(this.cmbStrategy_SelectedValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(250, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(43, 13);
            this.label10.TabIndex = 11;
            this.label10.Text = "Section";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 13);
            this.label13.TabIndex = 1;
            this.label13.Text = "Book";
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(249, 126);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(58, 23);
            this.cmdClose.TabIndex = 42;
            this.cmdClose.Text = "Close";
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // cmdInsertAloc
            // 
            this.cmdInsertAloc.Location = new System.Drawing.Point(165, 125);
            this.cmdInsertAloc.Name = "cmdInsertAloc";
            this.cmdInsertAloc.Size = new System.Drawing.Size(58, 24);
            this.cmdInsertAloc.TabIndex = 41;
            this.cmdInsertAloc.Text = "OK";
            this.cmdInsertAloc.UseVisualStyleBackColor = true;
            this.cmdInsertAloc.Click += new System.EventHandler(this.cmdInsertAloc_Click);
            // 
            // frmUpdateStrategy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(478, 163);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdInsertAloc);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblQuantity);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblTicker);
            this.Controls.Add(this.label03);
            this.Controls.Add(this.lblIdOrder);
            this.Controls.Add(this.label4);
            this.Name = "frmUpdateStrategy";
            this.Text = "Update Book & Section";
            this.Load += new System.EventHandler(this.frmUpdateStrategy_Load);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Label lblTicker;
        private System.Windows.Forms.Label label03;
        public System.Windows.Forms.Label lblIdOrder;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox cmbSection;
        private System.Windows.Forms.ComboBox cmbBook;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.Button cmdInsertAloc;
    }
}