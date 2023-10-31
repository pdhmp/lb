namespace Dashboards
{
    partial class OrderReview
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
            this.OrdemConsolidado = new System.Windows.Forms.DataGridView();
            this.showAll = new System.Windows.Forms.RadioButton();
            this.showStrategy = new System.Windows.Forms.RadioButton();
            this.strategies = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.OrdemConsolidado)).BeginInit();
            this.SuspendLayout();
            // 
            // OrdemConsolidado
            // 
            this.OrdemConsolidado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrdemConsolidado.Location = new System.Drawing.Point(12, 85);
            this.OrdemConsolidado.Name = "OrdemConsolidado";
            this.OrdemConsolidado.Size = new System.Drawing.Size(570, 439);
            this.OrdemConsolidado.TabIndex = 0;
            // 
            // showAll
            // 
            this.showAll.AutoSize = true;
            this.showAll.Location = new System.Drawing.Point(518, 12);
            this.showAll.Name = "showAll";
            this.showAll.Size = new System.Drawing.Size(36, 17);
            this.showAll.TabIndex = 1;
            this.showAll.TabStop = true;
            this.showAll.Text = "All";
            this.showAll.UseVisualStyleBackColor = true;
            this.showAll.CheckedChanged += new System.EventHandler(this.showAll_CheckedChanged);
            this.showAll.Checked = true;
            // 
            // showStrategy
            // 
            this.showStrategy.AutoSize = true;
            this.showStrategy.Location = new System.Drawing.Point(518, 35);
            this.showStrategy.Name = "showStrategy";
            this.showStrategy.Size = new System.Drawing.Size(64, 17);
            this.showStrategy.TabIndex = 4;
            this.showStrategy.TabStop = true;
            this.showStrategy.Text = "Strategy";
            this.showStrategy.UseVisualStyleBackColor = true;
            this.showStrategy.CheckedChanged += new System.EventHandler(this.showStrategy_CheckedChanged);
            // 
            // strategies
            // 
            this.strategies.FormattingEnabled = true;
            this.strategies.Location = new System.Drawing.Point(461, 58);
            this.strategies.Name = "strategies";
            this.strategies.Size = new System.Drawing.Size(121, 21);
            this.strategies.TabIndex = 5;
            this.strategies.SelectedIndexChanged += new System.EventHandler(this.strategies_SelectedIndexChanged);
            // 
            // OrderReview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 556);
            this.Controls.Add(this.strategies);
            this.Controls.Add(this.showStrategy);
            this.Controls.Add(this.showAll);
            this.Controls.Add(this.OrdemConsolidado);
            this.Name = "OrderReview";
            this.Text = "OrderReview";
            ((System.ComponentModel.ISupportInitialize)(this.OrdemConsolidado)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView OrdemConsolidado;
        private System.Windows.Forms.RadioButton showAll;
        private System.Windows.Forms.RadioButton showStrategy;
        private System.Windows.Forms.ComboBox strategies;
    }
}