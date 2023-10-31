namespace LiveBook
{
    partial class frmDataTable
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
            this.dgvDataGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvDataGrid
            // 
            this.dgvDataGrid.AllowUserToAddRows = false;
            this.dgvDataGrid.AllowUserToDeleteRows = false;
            this.dgvDataGrid.AllowUserToOrderColumns = true;
            this.dgvDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDataGrid.Location = new System.Drawing.Point(0, 0);
            this.dgvDataGrid.Name = "dgvDataGrid";
            this.dgvDataGrid.ReadOnly = true;
            this.dgvDataGrid.RowHeadersVisible = false;
            this.dgvDataGrid.Size = new System.Drawing.Size(949, 448);
            this.dgvDataGrid.TabIndex = 0;
            // 
            // frmDataTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 448);
            this.Controls.Add(this.dgvDataGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmDataTable";
            this.Text = "DataTable";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDataGrid;
    }
}