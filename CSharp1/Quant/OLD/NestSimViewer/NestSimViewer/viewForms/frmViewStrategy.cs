using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace NestQuant.Common
{
    public partial class frmViewStrategy : Form
    {
        public Strategy curStrategy;
        public int curY = 30;

        public frmViewStrategy()
        {
            InitializeComponent();
        }

        private void frmViewStrategy_Load(object sender, EventArgs e)
        {

        }

        public void AddButton(string ButtonName, string ButtonCaption, int curPosition, int curX)
        {
            System.Windows.Forms.Button newButton = new System.Windows.Forms.Button();
            
            newButton.Enabled = true;
            newButton.Location = new System.Drawing.Point(curX, curY);
            newButton.Name = ButtonName;
            newButton.Size = new System.Drawing.Size(89, 23);
            newButton.TabIndex = 3;
            newButton.Text = ButtonCaption;
            newButton.UseVisualStyleBackColor = true;
            newButton.Click += GenericHandler;
            newButton.Tag = curPosition;
            
            curY = curY + 30;

            Controls.Add(newButton);

        }

        private void GenericHandler(object sender, EventArgs e)
        {
            switch(((Button)sender).Left)
            {
                case 100:
                    if ((int)((Button)sender).Tag == 0) { FormUtils.ShowInForm(curStrategy.stratPerfSummary, "perfSummary", "0.00"); };
                    break;
                case 200:
                    FormUtils.ShowInForm(curStrategy.PriceTables[(int)((Button)sender).Tag], "Prices - " + curStrategy.PriceTables[(int)((Button)sender).Tag].Name, "0.00");
                    break;
                case 300:
                    FormUtils.ShowInForm(curStrategy.WeightTables[(int)((Button)sender).Tag], "Weights - " + curStrategy.WeightTables[(int)((Button)sender).Tag].Name, "0.00");
                    break;
                case 400:
                    FormUtils.ShowInForm(curStrategy.SignalTables[(int)((Button)sender).Tag], "Signals - " + curStrategy.SignalTables[(int)((Button)sender).Tag].Name, "0.00");
                    break;
            }
        }

    }
}