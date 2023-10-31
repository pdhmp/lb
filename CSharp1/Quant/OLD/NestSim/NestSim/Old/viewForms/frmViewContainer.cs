using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using NestSimDLL;

namespace NestSim
{
    public partial class frmViewContainer : Form
    {
        public Strategy_Container curContainer;
        public int curY = 30;

        public frmViewContainer()
        {
            InitializeComponent();
        }

        private void frmViewContainer_Load(object sender, EventArgs e)
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
            switch (((Button)sender).Left)
            {
                case 100:
                    FormUtils.ShowInForm(curContainer.Strategies[(int)((Button)sender).Tag], "Strategy - " + curContainer.Strategies[(int)((Button)sender).Tag].Name);
                    break;
            }
        }

    }
}