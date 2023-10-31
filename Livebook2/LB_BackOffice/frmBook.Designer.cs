namespace LiveBook
{
    partial class frmBook
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
            if(disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBook));
            this.ListBook = new System.Windows.Forms.ListBox();
            this.btnInsertBook = new System.Windows.Forms.Button();
            this.txtNewBook = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbValidSubStrategy = new System.Windows.Forms.ComboBox();
            this.btnInsertValidBook = new System.Windows.Forms.Button();
            this.ListValidBooks = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ListBook
            // 
            this.ListBook.FormattingEnabled = true;
            this.ListBook.HorizontalScrollbar = true;
            this.ListBook.Location = new System.Drawing.Point(17, 19);
            this.ListBook.Name = "ListBook";
            this.ListBook.ScrollAlwaysVisible = true;
            this.ListBook.Size = new System.Drawing.Size(148, 225);
            this.ListBook.Sorted = true;
            this.ListBook.TabIndex = 67;
            this.ListBook.SelectedIndexChanged += new System.EventHandler(this.ListBook_SelectedIndexChanged);
            // 
            // btnInsertBook
            // 
            this.btnInsertBook.Location = new System.Drawing.Point(43, 289);
            this.btnInsertBook.Name = "btnInsertBook";
            this.btnInsertBook.Size = new System.Drawing.Size(96, 23);
            this.btnInsertBook.TabIndex = 73;
            this.btnInsertBook.Text = "Insert New Book";
            this.btnInsertBook.UseVisualStyleBackColor = true;
            this.btnInsertBook.Click += new System.EventHandler(this.btnInsertBook_Click);
            // 
            // txtNewBook
            // 
            this.txtNewBook.Location = new System.Drawing.Point(17, 263);
            this.txtNewBook.Name = "txtNewBook";
            this.txtNewBook.Size = new System.Drawing.Size(148, 20);
            this.txtNewBook.TabIndex = 72;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(63, 247);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 71;
            this.label3.Text = "New Book";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnInsertBook);
            this.groupBox2.Controls.Add(this.ListBook);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtNewBook);
            this.groupBox2.Location = new System.Drawing.Point(12, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 329);
            this.groupBox2.TabIndex = 77;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Book";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbValidSubStrategy);
            this.groupBox1.Controls.Add(this.btnInsertValidBook);
            this.groupBox1.Controls.Add(this.ListValidBooks);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(206, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(182, 329);
            this.groupBox1.TabIndex = 78;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Valid Books";
            // 
            // cmbValidSubStrategy
            // 
            this.cmbValidSubStrategy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbValidSubStrategy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbValidSubStrategy.FormattingEnabled = true;
            this.cmbValidSubStrategy.Location = new System.Drawing.Point(17, 263);
            this.cmbValidSubStrategy.Name = "cmbValidSubStrategy";
            this.cmbValidSubStrategy.Size = new System.Drawing.Size(148, 21);
            this.cmbValidSubStrategy.TabIndex = 74;
            this.cmbValidSubStrategy.Text = "cmbValidSubStrategy";
            // 
            // btnInsertValidBook
            // 
            this.btnInsertValidBook.Location = new System.Drawing.Point(42, 290);
            this.btnInsertValidBook.Name = "btnInsertValidBook";
            this.btnInsertValidBook.Size = new System.Drawing.Size(98, 23);
            this.btnInsertValidBook.TabIndex = 73;
            this.btnInsertValidBook.Text = "Insert Valid Book";
            this.btnInsertValidBook.UseVisualStyleBackColor = true;
            this.btnInsertValidBook.Click += new System.EventHandler(this.btnInsertValidBook_Click);
            // 
            // ListValidBooks
            // 
            this.ListValidBooks.FormattingEnabled = true;
            this.ListValidBooks.HorizontalScrollbar = true;
            this.ListValidBooks.Location = new System.Drawing.Point(17, 19);
            this.ListValidBooks.Name = "ListValidBooks";
            this.ListValidBooks.ScrollAlwaysVisible = true;
            this.ListValidBooks.Size = new System.Drawing.Size(148, 225);
            this.ListValidBooks.Sorted = true;
            this.ListValidBooks.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 247);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 71;
            this.label2.Text = "Add to Valid SubStrategies";
            // 
            // frmBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(400, 352);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(416, 417);
            this.Name = "frmBook";
            this.Text = "Edit Book";
            this.Load += new System.EventHandler(this.frmBook_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox ListBook;
        private System.Windows.Forms.Button btnInsertBook;
        private System.Windows.Forms.TextBox txtNewBook;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnInsertValidBook;
        private System.Windows.Forms.ListBox ListValidBooks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbValidSubStrategy;

    }
}