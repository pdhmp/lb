using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NCommonTypes;
using NestDLL;

namespace NewValueBZ
{
    public partial class frmMain : Form
    {
        ValueBZ_Runner thisRunner;
        ValueBZ_Sim thisSim;

        public frmMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Initialize Data Objects
            FastDataObject.Instance.IniDatePrices = new DateTime(1999, 01, 01);
            FastDataObject.Instance.IniDateShareData = new DateTime(1999, 01, 01);
            FastDataObject.Instance.InitializeObjects();

            double[] IbovTicker = new double[1];
            IbovTicker[0] = 1073;
            FastDataObject.Instance.curPrices.LoadTickers(IbovTicker);


            /* Verify last sim date and update history */
            using (newNestConn curConn = new newNestConn())
            {
                DateTime MaxDate = curConn.Return_DateTime("SELECT MAX(SrDate) FROM NESTDB.dbo.Tb050_Preco_Acoes_Onshore WHERE IdSecurity=64158");

                DateTime MaxDateIbov = curConn.Return_DateTime("SELECT MAX(SrDate) FROM NESTDB.dbo.Tb053_Precos_Indices WHERE IdSecurity=1073");

                if (MaxDate < MaxDateIbov)
                {
                    updateHistory(DateTime.Now.Subtract(new TimeSpan(60, 0, 0, 0)));
                }
            }

            
            /* Run Strategy */ 
            thisRunner = new ValueBZ_Runner();

            
            /* Run sim for debugging */
            //DateTime TimeCounter = DateTime.Now;          
            //thisSim = new ValueBZ_Sim(new DateTime(2000, 06, 30));

            //TimeSpan TimeTaken = DateTime.Now.Subtract(TimeCounter);
            //Console.WriteLine("Time Taken: " + TimeTaken.ToString());

            //SendPerfToClipBoard();
            //PrintStratItems();
        }

        private void updateHistory(DateTime StartDate)
        {
            ValueBZ_Sim histSim;            

            histSim = new ValueBZ_Sim(StartDate);

            using (newNestConn curConn = new newNestConn())
            {
                foreach (KeyValuePair<DateTime, ValueBZ_Sim.StatItem> curKvp in histSim.SimData)
                {
                    string SQLString = "EXEC [NESTDB].[dbo].[Proc_Insert_Price]" +
                                                "@IdSecurity= 64158" +
                                                ", @SrValue= " + curKvp.Value.Performance.ToString().Replace(',', '.') +
                                                ", @Data='" + curKvp.Key.ToString("yyyy-MM-dd") + "'" +
                                                ", @SrType=100" +
                                                ", @Source=7" +
                                                ", @Automated=1";


                    curConn.ExecuteNonQuery(SQLString);

                    curConn.ExecuteNonQuery("EXEC NESTDB.dbo.Proc_Update_TRIndex 64158");
                }
            }
        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //thisRunner.Stop();
            NestSymConn.SymConn.Instance.Dispose();
        }

        private void SendPerfToClipBoard()
        {
            string tempData = "";

            foreach (KeyValuePair<DateTime, ValueBZ_Sim.StatItem> curKvp in thisSim.SimData)
            {
                tempData = tempData + curKvp.Key.ToString("yyyy-MM-dd") + "\t" + curKvp.Value.Performance + "\r\n";
            }

            Clipboard.SetText(tempData);

        }

        private void PrintStratItems()
        {
            string tempData = "";

            foreach (KeyValuePair<DateTime, ValueBZ_Sim.StatItem> curKvp in thisSim.SimData)
            {
                foreach(TickerPE curValueItem in curKvp.Value.StratPositions)
                {
                    tempData = tempData + curKvp.Key.ToString("yyyy-MM-dd");
                    tempData = tempData + "\t" + curKvp.Value.Performance.ToString("0.00%");
                    tempData = tempData + "\t" + curValueItem.IdTickerComposite;
                    tempData = tempData + "\t" + curValueItem.IdTicker;
                    tempData = tempData + "\t" + curValueItem.closeSignal;
                    tempData = tempData + "\t" + curValueItem.closeEYield.ToString("0.00%");
                    tempData = tempData + "\t" + curValueItem.DayTR.ToString("0.00%");
                    tempData = tempData + "\t" + curValueItem.Weight.ToString("0.00%");
                    tempData = tempData + "\t" + curValueItem.StratContrib.ToString("0.0000%");
                    tempData = tempData + "\r\n";
                }
            }

            Clipboard.SetText(tempData);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            foreach (ValueCalc curValueCalc in thisRunner.SectorCalcs.Values)
            {
                curValueCalc.StratRecalc();

                Console.WriteLine("\r\n\r\n");
                Console.WriteLine(curValueCalc.IdTickerComposite + "\t-\t" + curValueCalc.curMedianPE);
                Console.WriteLine("\r\n");
                foreach (KeyValuePair<int, TickerPE> curValueItem in curValueCalc.PositionPEs)
                {
                    Console.WriteLine(curValueItem.Key + "\t" + curValueItem.Value.EPSDate + "\t" + curValueItem.Value.EPSKnownDate + "\t" + curValueItem.Value.adjEPS + "\t" + curValueItem.Value.curPE + "\t" + curValueItem.Value.curSignal);
                }
            }
            */
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}