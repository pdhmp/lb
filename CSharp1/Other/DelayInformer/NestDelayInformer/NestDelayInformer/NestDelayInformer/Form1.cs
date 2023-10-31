using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace NestDelayInformer
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.NotifyIcon notifyIcon1;          

        Informer Info;
        TimerCallback tmrCB;
        System.Threading.Timer tmrStart;

        public Form1()
        {
            this.components = new System.ComponentModel.Container();            
            

            // Create the NotifyIcon.
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);

            // The Icon property sets the icon that will appear
            // in the systray for this application.
            notifyIcon1.Icon = new Icon(@"N:\TI\Projects\CSharp\Utils\DelayInformer\NestDelayInformer\NestDelayInformer\Nestico.ico");
            

            // The Text property sets the text that will be displayed,
            // in a tooltip, when the mouse hovers over the systray icon.
            notifyIcon1.Text = "NestDelayInformer";
            notifyIcon1.Visible = false;

            // Handle the DoubleClick event to activate the form.
            notifyIcon1.DoubleClick += new System.EventHandler(Form1_DoubleClick);


            InitializeComponent();
            Info = new Informer();
            Info.OnDelayed += new EventHandler(Info_OnDelayed);
            tmrCB = new TimerCallback(Info.SubscribeTickers);
            tmrStart = new System.Threading.Timer(tmrCB, null, 1000, Timeout.Infinite);
            timer1.Start();
            timer2.Start();
        }       

        void Info_OnDelayed(object sender, EventArgs e)
        {
            Thread threadInformDelay = new Thread(new ThreadStart(InformDelay));
            threadInformDelay.Start();
        }

        void InformDelay()
        {
            MessageBox.Show("Nest Signal is Delayed");
        }

        void InformOut()
        {
            MessageBox.Show("Nest Signal is Out");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            txtHora.Text = Info.Hora.ToString();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string[] sep = { ":" };
            string[] values = txtHora.Text.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if ((new TimeSpan(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2])) -
                new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)) > new TimeSpan(0, 1, 0))
            {
                Thread threadInformOut = new Thread(new ThreadStart(InformOut));
                threadInformOut.Start();
            }
        }

        private void RestaurarApp()
        {            
            this.Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
            
        }        

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized) 
            { 
                ShowInTaskbar = false;
                notifyIcon1.Visible = true;                
            }             
            else 
            { 
                ShowInTaskbar = true;
                notifyIcon1.Visible = false;
            }
        }

        private void Form1_DoubleClick(object sender, EventArgs e)
        {
            RestaurarApp();
        }        

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Info.Close();
            Application.ExitThread();
        }    
    }
}
