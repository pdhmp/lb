using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using NestDLL;

namespace LB_Hist_Update
{

    public partial class frmUpdate : Form
    {
        bool CheckCloseUpdate = false;

        List<SQLExecutor> ExecutorGroup = new List<SQLExecutor>();
        SQLExecutor CALC_Recent_Price = new SQLExecutor("EXEC sp_Calc_LB2_Recent_Price", 500, "T:\\Log\\Update LiveBook\\CALC_Recent_Price.txt");
        SQLExecutor CALC_Recent_Price_Options = new SQLExecutor("EXEC sp_Calc_LB2_Recent_Price_Options; EXEC NESTDB.dbo.Proc_Atualiza_Dados_Fixos;", 1000, "T:\\Log\\Update LiveBook\\CALC_HIST_Calc_Fields.txt");
        SQLExecutor CALC_Recent_Price_Fowards = new SQLExecutor("EXEC sp_Calc_LB2_Recent_Price_FWD ", 1000000, "T:\\Log\\Update LiveBook\\CALC_Recent_Price_Fowards.txt");
        SQLExecutor CALC_Oldest_Updated = new SQLExecutor("EXEC sp_Calc_LB2_Oldest_Updated", 3500, "T:\\Log\\Update LiveBook\\CALC_HIST_Cost_Close.txt");
        SQLExecutor Cost_Close = new SQLExecutor("EXEC proc_Calc_LB2_Cost_Close", 2000, "T:\\Log\\Update LiveBook\\Cost_Close.txt");
        SQLExecutor Cost_Close_Oldest_Update = new SQLExecutor("EXEC proc_Calc_LB2_Cost_Close_Oldest_Update", 999999, "T:\\Log\\Update LiveBook\\Cost_Close_oldest_update.txt");

        SQLExecutor New_Positions_Fund = new SQLExecutor("EXEC NESTDB.dbo.Proc_Load_Portfolios; ", 2400, "T:\\Log\\Update LiveBook\\New_Positions.txt");

        SQLExecutor New_Quantities = new SQLExecutor("EXEC proc_Calc_LB2_New_Quantities", 600000, "T:\\Log\\Update LiveBook\\New_Quantities.txt");
        //SQLExecutor FIX_Insert_Trades = new SQLExecutor("EXEC Proc_FIX_Select_Drop_Copies", 1000, "T:\\Log\\Update LiveBook\\FIX_Insert_Trades.txt");
               

        //SQLExecutor New_Positions_Fund = new SQLExecutor("DECLARE @Date datetime ;Select  @Date = convert(varchar,getdate(),112) ;EXEC NESTDB.dbo.Proc_Load_Position 4,@Date,0; EXEC NESTDB.dbo.Proc_Atualiza_Dados_Fixos", 2400, "T:\\Log\\Update LiveBook\\New_Positions.txt");
        //SQLExecutor New_Positions_Mile = new SQLExecutor("DECLARE @Date datetime ;Select  @Date = convert(varchar,getdate(),112) ;EXEC NESTDB.dbo.Proc_Load_Position 43,@Date,0 ; EXEC NESTDB.dbo.Proc_Atualiza_Dados_Fixos", 2400, "T:\\Log\\Update LiveBook\\New_Positions.txt");
        //SQLExecutor New_Positions_Fia = new SQLExecutor("DECLARE @Date datetime ;Select  @Date = convert(varchar,getdate(),112) ;EXEC NESTDB.dbo.Proc_Load_Position 10,@Date,0 ; EXEC NESTDB.dbo.Proc_Atualiza_Dados_Fixos", 2400, "T:\\Log\\Update LiveBook\\New_Positions.txt");
        //SQLExecutor New_Positions_Arb = new SQLExecutor("DECLARE @Date datetime ;Select  @Date = convert(varchar,getdate(),112) ;EXEC NESTDB.dbo.Proc_Load_Position 38,@Date,0; EXEC NESTDB.dbo.Proc_Atualiza_Dados_Fixos", 2400, "T:\\Log\\Update LiveBook\\New_Positions.txt");
        //SQLExecutor New_Positions_Quant = new SQLExecutor("DECLARE @Date datetime ;Select  @Date = convert(varchar,getdate(),112) ;EXEC NESTDB.dbo.Proc_Load_Position 18,@Date,0; EXEC NESTDB.dbo.Proc_Atualiza_Dados_Fixos", 2400, "T:\\Log\\Update LiveBook\\New_Positions.txt");


     
        int GlobalThreadCounter = 0;

        public frmUpdate()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Top = 80;
            this.Left = 300;

            ExecutorGroup.Add(CALC_Recent_Price);
            ExecutorGroup.Add(CALC_Recent_Price_Options);
            ExecutorGroup.Add(CALC_Recent_Price_Fowards);
            ExecutorGroup.Add(CALC_Oldest_Updated);
            ExecutorGroup.Add(Cost_Close);
            //ExecutorGroup.Add(Cost_Close_Oldest_Update);
            ExecutorGroup.Add(New_Positions_Fund);
            /*
            ExecutorGroup.Add(New_Positions_Mile);
            ExecutorGroup.Add(New_Positions_Fia);
            ExecutorGroup.Add(New_Positions_Arb);
            ExecutorGroup.Add(New_Positions_Quant);
            */
            ExecutorGroup.Add(New_Quantities);
            //ExecutorGroup.Add(FIX_Insert_Trades);
            
            cmdStart_Click(this, new EventArgs());
            tmrUpdateLabels.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopTimers();
            Application.ExitThread();
        }

        int CheckInSkip = 0;

        private void Log_Checkin()
        {
            CheckInSkip++;
            if (CheckInSkip == 10)
            {
                String StringSQL = "UPDATE [NESTLOG].dbo.Tb900_CheckIn_Log SET CheckInTime=getdate() WHERE Program_Id=7";
                using (NestDLL.old_Conn curConn = new NestDLL.old_Conn())
                {
                    int retorno = curConn.ExecuteNonQuery(StringSQL);
                }
                CheckInSkip = 0;
            }
        }

        private void StartTimers()
        {
            foreach (SQLExecutor curExecutor in ExecutorGroup)
            {
                curExecutor.StartLoop();
                curExecutor.RunNow();
            }
            lblStartStop1.Text = "started";
            lblStartStop2.Text = "started";
            lblStartStop8.Text = "started";
            lblStartStop3.Text = "started";
            lblStartStop4.Text = "started";
            lblStartStop9.Text = "started";
            lblStartStop5.Text = "started";
            lblStartStop6.Text = "started";
            lblStartStop7.Text = "started";

            lblStartStop51.Text = "started";
            lblStartStop52.Text = "started";
            lblStartStop53.Text = "started";
            lblStartStop54.Text = "started";

        }

        private void StopTimers()
        {
            foreach (SQLExecutor curExecutor in ExecutorGroup)
            {
                curExecutor.StopLoop();
            }
            lblStartStop1.Text = "stopped";
            lblStartStop2.Text = "stopped";
            lblStartStop8.Text = "stopped";
            lblStartStop3.Text = "stopped";
            lblStartStop4.Text = "stopped";
            lblStartStop9.Text = "stopped";
            lblStartStop5.Text = "stopped";
            lblStartStop6.Text = "stopped";
            lblStartStop7.Text = "stopped";

            lblStartStop51.Text = "stopped";
            lblStartStop52.Text = "stopped";
            lblStartStop53.Text = "stopped";
            lblStartStop54.Text = "stopped";

        }
        private void cmdStart_Click(object sender, EventArgs e)
        {
            StartTimers();
            cmdStart.Enabled = false;
            cmdStop.Enabled = true;
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            StopTimers();
            cmdStart.Enabled = true;
            cmdStop.Enabled = false;
        }

        private void UpdateLabels()
        {
            try
            {
                lblTimerLoop1.Text = CALC_Recent_Price.LoopInterval.ToString();
                lblMessage1.Text = CALC_Recent_Price.LastRunResult;
                lblupdate1.Text = CALC_Recent_Price.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken1.Text = CALC_Recent_Price.AvgRunTimeTaken.ToString();
                if (!CALC_Recent_Price.IsRunning && !CALC_Recent_Price.IsTimerOn) lblMessage1.Text = "Stopped";

                lblTimerLoop2.Text = CALC_Recent_Price_Options.LoopInterval.ToString();
                lblMessage2.Text = CALC_Recent_Price_Options.LastRunResult;
                lblupdate2.Text = CALC_Recent_Price_Options.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken2.Text = CALC_Recent_Price_Options.AvgRunTimeTaken.ToString();
                if (!CALC_Recent_Price_Options.IsRunning && !CALC_Recent_Price_Options.IsTimerOn) lblMessage2.Text = "Stopped";

                lblTimerLoop8.Text = CALC_Recent_Price_Fowards.LoopInterval.ToString();
                lblMessage8.Text = CALC_Recent_Price_Fowards.LastRunResult;
                lblupdate8.Text = CALC_Recent_Price_Fowards.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken8.Text = CALC_Recent_Price_Fowards.AvgRunTimeTaken.ToString();
                if (!CALC_Recent_Price_Fowards.IsRunning && !CALC_Recent_Price_Fowards.IsTimerOn) lblMessage8.Text = "Stopped";

                lblTimerLoop3.Text = CALC_Oldest_Updated.LoopInterval.ToString();
                lblMessage3.Text = CALC_Oldest_Updated.LastRunResult;
                lblupdate3.Text = CALC_Oldest_Updated.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken3.Text = CALC_Oldest_Updated.AvgRunTimeTaken.ToString();
                if (!CALC_Oldest_Updated.IsRunning && !CALC_Oldest_Updated.IsTimerOn) lblMessage3.Text = "Stopped";

                lblTimerLoop4.Text = Cost_Close.LoopInterval.ToString();
                lblMessage4.Text = Cost_Close.LastRunResult;
                lblupdate4.Text = Cost_Close.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken4.Text = Cost_Close.AvgRunTimeTaken.ToString();
                if (!Cost_Close.IsRunning && !Cost_Close.IsTimerOn) lblMessage4.Text = "Stopped";

                lblTimerLoop9.Text = Cost_Close_Oldest_Update.LoopInterval.ToString();
                lblMessage9.Text = Cost_Close_Oldest_Update.LastRunResult;
                lblupdate9.Text = Cost_Close_Oldest_Update.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken9.Text = Cost_Close_Oldest_Update.AvgRunTimeTaken.ToString();
                if (!Cost_Close_Oldest_Update.IsRunning && !Cost_Close_Oldest_Update.IsTimerOn) lblMessage9.Text = "Stopped";

                lblTimerLoop5.Text = New_Positions_Fund.LoopInterval.ToString();
                lblMessage5.Text = New_Positions_Fund.LastRunResult;
                lblupdate5.Text = New_Positions_Fund.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken5.Text = New_Positions_Fund.AvgRunTimeTaken.ToString();
                if (!New_Positions_Fund.IsRunning && !New_Positions_Fund.IsTimerOn) lblMessage5.Text = "Stopped";
                
                
                lblTimerLoop6.Text = New_Quantities.LoopInterval.ToString();
                lblMessage6.Text = New_Quantities.LastRunResult;
                lblupdate6.Text = New_Quantities.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken6.Text = New_Quantities.AvgRunTimeTaken.ToString();
                if (!New_Quantities.IsRunning && !New_Quantities.IsTimerOn) lblMessage6.Text = "Stopped";

                //lblTimerLoop7.Text = FIX_Insert_Trades.LoopInterval.ToString();
                //lblMessage7.Text = FIX_Insert_Trades.LastRunResult;
                //lblupdate7.Text = FIX_Insert_Trades.LastRunDateTime.ToString("HH:mm:ss");
                //lblTimeTaken7.Text = FIX_Insert_Trades.AvgRunTimeTaken.ToString();
                //if (!FIX_Insert_Trades.IsRunning && !FIX_Insert_Trades.IsTimerOn) lblMessage7.Text = "Stopped";

               
                /*

                lblTimerLoop51.Text = New_Positions_Mile.LoopInterval.ToString();
                lblMessage51.Text = New_Positions_Mile.LastRunResult;
                lblupdate51.Text = New_Positions_Mile.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken51.Text = New_Positions_Mile.AvgRunTimeTaken.ToString();
                if (!New_Positions_Mile.IsRunning && !New_Positions_Mile.IsTimerOn) lblMessage51.Text = "Stopped";

                lblTimerLoop52.Text = New_Positions_Fia.LoopInterval.ToString();
                lblMessage52.Text = New_Positions_Fia.LastRunResult;
                lblupdate52.Text = New_Positions_Fia.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken52.Text = New_Positions_Fia.AvgRunTimeTaken.ToString();
                if (!New_Positions_Fia.IsRunning && !New_Positions_Fia.IsTimerOn) lblMessage52.Text = "Stopped";

                lblTimerLoop53.Text = New_Positions_Arb.LoopInterval.ToString();
                lblMessage53.Text = New_Positions_Arb.LastRunResult;
                lblupdate53.Text = New_Positions_Arb.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken53.Text = New_Positions_Arb.AvgRunTimeTaken.ToString();
                if (!New_Positions_Arb.IsRunning && !New_Positions_Arb.IsTimerOn) lblMessage53.Text = "Stopped";

                lblTimerLoop54.Text = New_Positions_Quant.LoopInterval.ToString();
                lblMessage54.Text = New_Positions_Quant.LastRunResult;
                lblupdate54.Text = New_Positions_Quant.LastRunDateTime.ToString("HH:mm:ss");
                lblTimeTaken54.Text = New_Positions_Quant.AvgRunTimeTaken.ToString();
                if (!New_Positions_Quant.IsRunning && !New_Positions_Quant.IsTimerOn) lblMessage54.Text = "Stopped";
                */

                int ThreadCounter = 0;

                foreach (SQLExecutor curExecutor in ExecutorGroup)
                {
                    ThreadCounter = ThreadCounter + curExecutor.ThreadCounter;
                }

                txtThreadCounter.Text = ThreadCounter.ToString();


                Log_Checkin();
                Application.DoEvents();

                if (!CheckCloseUpdate && DateTime.Now < Convert.ToDateTime("16:46:00"))
                {
                    //Update_TRIndex_RT.RunNow();
                    CheckCloseUpdate = true;
                }
            }
            catch (Exception Excep)
            {
                NestDLL.Log.AddLogEntry(Excep.ToString(), "T:\\Log\\Update LiveBook\\HIST_UpdateLabels.txt");
                Application.Exit();
            }
        }

        private void tmrUpdateLabels_Tick(object sender, EventArgs e)
        {
            UpdateLabels();
        }

        // =====================   TOGGLE PROCESSES ON AND OFF   =======================================

        private void ToggleProcess(SQLExecutor curExecutor, Label curLabel)
        { 
            if (curExecutor.IsRunning)
            {
                
                DialogResult curResult = MessageBox.Show("Do you want to stop this process?", "Stop Process", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (curResult == DialogResult.Yes) curExecutor.StopLoop(); curLabel.Text = "stopped";
            }
            else
            {
                curLabel.Text = "started";
                curExecutor.StartLoop();
            }
        }

        private void lblStartStop1_Click(object sender, EventArgs e) { ToggleProcess(CALC_Recent_Price, lblStartStop1); }
        private void lblStartStop2_Click(object sender, EventArgs e) { ToggleProcess(CALC_Recent_Price_Options, lblStartStop2); }
        private void lblStartStop8_Click(object sender, EventArgs e) { ToggleProcess(CALC_Recent_Price_Fowards, lblStartStop8); }

        private void lblStartStop3_Click(object sender, EventArgs e) { ToggleProcess(CALC_Oldest_Updated, lblStartStop3); }
        private void lblStartStop4_Click(object sender, EventArgs e) { ToggleProcess(Cost_Close, lblStartStop4); }
        private void lblStartStop9_Click(object sender, EventArgs e) { ToggleProcess(Cost_Close_Oldest_Update, lblStartStop9); }
        private void lblStartStop5_Click(object sender, EventArgs e) { ToggleProcess(New_Positions_Fund, lblStartStop5); }
        private void lblStartStop6_Click(object sender, EventArgs e) { ToggleProcess(New_Quantities, lblStartStop6); }
        //private void lblStartStop7_Click(object sender, EventArgs e) { ToggleProcess(FIX_Insert_Trades, lblStartStop7); }

        /*
        private void lblStartStop51_Click(object sender, EventArgs e) { ToggleProcess(New_Positions_Mile, lblStartStop51); }
        private void lblStartStop52_Click(object sender, EventArgs e) { ToggleProcess(New_Positions_Fia, lblStartStop52); }
        private void lblStartStop53_Click(object sender, EventArgs e) { ToggleProcess(New_Positions_Arb, lblStartStop53); }
        private void lblStartStop54_Click(object sender, EventArgs e) { ToggleProcess(New_Positions_Quant, lblStartStop54); }
        */

        // =====================   CHANGE PROCESSES TIMER LOOP   =======================================

        private void ChangeLoop(SQLExecutor curExecutor)
        {
            string newValue = curExecutor.LoopInterval.ToString();
            DialogResult curResult = InputBox("Please enter new value for Timer?", "Stop Process", ref newValue);
            if (curResult == DialogResult.OK && newValue != "") curExecutor.ChangeInterval(int.Parse(newValue));
        }

        private void lblTimerLoop1_Click(object sender, EventArgs e) { ChangeLoop(CALC_Recent_Price); }
        private void lblTimerLoop2_Click(object sender, EventArgs e) { ChangeLoop(CALC_Recent_Price_Options); }
        private void lblTimerLoop8_Click(object sender, EventArgs e) { ChangeLoop(CALC_Recent_Price_Fowards); }
        private void lblTimerLoop3_Click(object sender, EventArgs e) { ChangeLoop(CALC_Oldest_Updated); }
        private void lblTimerLoop4_Click(object sender, EventArgs e) { ChangeLoop(Cost_Close); }
        private void lblTimerLoop9_Click(object sender, EventArgs e) { ChangeLoop(Cost_Close_Oldest_Update); }
        private void lblTimerLoop5_Click(object sender, EventArgs e) { ChangeLoop(New_Positions_Fund); }
        private void lblTimerLoop6_Click(object sender, EventArgs e) { ChangeLoop(New_Quantities); }
        //private void lblTimerLoop7_Click(object sender, EventArgs e) { ChangeLoop(FIX_Insert_Trades); }

        /*
        private void lblTimerLoop51_Click(object sender, EventArgs e) { ChangeLoop(New_Positions_Mile); }
        private void lblTimerLoop52_Click(object sender, EventArgs e) { ChangeLoop(New_Positions_Fia); }
        private void lblTimerLoop53_Click(object sender, EventArgs e) { ChangeLoop(New_Positions_Arb); }
        private void lblTimerLoop54_Click(object sender, EventArgs e) { ChangeLoop(New_Positions_Quant); }
        */

        public static DialogResult InputBox(string promptText, string title, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 20, 372, 13);
            textBox.SetBounds(12, 36, 372, 20);
            buttonOk.SetBounds(228, 72, 75, 23);
            buttonCancel.SetBounds(309, 72, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 107);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }
    }
}