using System;
using System.IO;
using System.Windows.Forms;
using QuickFix;

namespace FIX_BMF
{
    public partial class frmControl : Form
    {
        SessionSettings settings;
        Application application;
        SocketInitiator initiator;
        FileStoreFactory storeFactory;
        FileLogFactory logFactory;
        MessageFactory messageFactory;

        int StatusCounter = 0;
        int CheckConnInterval = 30;
        
        public frmControl()
        {
            InitializeComponent();
        }

        private void tmrUpdateScreen_Tick(object sender, EventArgs e)
        {
            if (chkAutoUpdate.Checked)
            {
                txtMessages.Text = GlobalVars.Instance.strMessages;

                StatusCounter++;

                if (StatusCounter > (CheckConnInterval * 1000) / tmrUpdateScreen.Interval)
                {
                    if (application != null)
                    {
                        if (application.secaoId != null)
                        {
                            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - Sessão Ativa (BMF FIX)");
                        }
                        else
                        {
                            GlobalVars.Instance.AddMessage(DateTime.Now.ToString() + " - Sessão Inativa (BMF FIX)");
                        }
                    }
                    StatusCounter = 0;
                }
            }
        }

        private void frmControl_Load(object sender, EventArgs e)
        {
            tmrUpdateScreen.Start();
            tmrAutoStart.Start();
        }

        private void frmControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            //MarketDataAnalyzer.Instance.Stop();
            GlobalVars.Instance.isClosing = true;
            application = null;
            DataRecorder.Instance.StopProcessing = true;
        }

        private void chkSaveLog_CheckedChanged(object sender, EventArgs e)
        {
            if (txtRecIdTicker.Text != "")
            {
                if (chkSaveLog.Checked == true)
                {
                    GlobalVars.Instance.SaveLogTicker = txtRecIdTicker.Text;
                    try
                    {
                        File.Delete("T:\\Log\\not_implemented.txt");
                        File.Delete("T:\\Log\\depthlog_BID.txt");
                        File.Delete("T:\\Log\\depthlog_ASK.txt");
                    }
                    catch
                    { }
                    GlobalVars.Instance.SaveLog = true;
                    txtRecIdTicker.Enabled = false;
                }
                else
                {
                    GlobalVars.Instance.SaveLog = false;
                    txtRecIdTicker.Enabled = true;
                }
            }
            else
            {
                MessageBox.Show("Invalid Id Ticker!");
            }

        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void cmdReadLog_Click(object sender, EventArgs e)
        {
            frmReadLog thisLog = new frmReadLog();
            thisLog.Show();
        }

        private void chkAutoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            StatusCounter = (CheckConnInterval * 1000) / tmrUpdateScreen.Interval;
        }

        private bool Started = false;

        private void tmrAutoStart_Tick(object sender, EventArgs e)
        {
            tmrAutoStart.Stop();
            if (!Started)
            {                
                Start();
            }
        }

        private void Start()
        {
            Started = true;
            cmdStart.Enabled = false;

            string strAppPath = @"N:\TI\Projects\CSharp\Feeds\FIX_BMF\FIX_BMF\";
            clsArquivo.pprPathArquivoLog = @"T:\Log\FIX\FIX_BMF\";
            GlobalVars.Instance.AddMessage("Iniciando...");

            

            try
            {
                string logPath = @"C:\BMFData\";

                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }

                //DataRecorder.Instance.StartRecorder(logPath + DateTime.Today.ToString("yyyyMMdd") + "_FixBMF.txt");

                settings = new SessionSettings(@"T:\Resources\FixConfigs\BMF_BELL.cfg");
                application = new Application();
                storeFactory = new FileStoreFactory(settings);
                //logFactory = new FileLogFactory(logPath);
                logFactory = new FileLogFactory(settings);
                messageFactory = new DefaultMessageFactory();

                initiator = new SocketInitiator(application, storeFactory, settings, logFactory, messageFactory);
                //initiator = new SocketInitiator(application, storeFactory, settings, messageFactory);
                initiator.start();

                GlobalVars.Instance.AddMessage("Conexao: " + initiator.ToString());
            }
            catch (Exception curError)
            {
                GlobalVars.Instance.AddMessage(curError.Message.ToString());
                GlobalVars.Instance.AddMessage(curError.TargetSite.ToString());
            }
        }

        private void cmdDump_Click(object sender, EventArgs e)
        {
            string fileName = "T:\\Log\\FIX_BMF_L2_DUMP.txt";
            if (txtRecIdTicker.Text != "")
            {
                try
                {
                    File.Delete(fileName);
                }
                catch
                { }

                int curPos = -1;

                string curTicker = txtRecIdTicker.Text;

                if (application.SubListIndex.TryGetValue(curTicker, out curPos))
                {
                    MarketDepthItem curItem = application.SubscribedDepth[curPos];
                    FileStream fs = new FileStream(fileName, FileMode.Append);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.WriteLine(curItem.Ticker + "\r\n");
                    sw.Write(curItem.BidSize1.ToString("0.00").Replace(",", ".") + '\t' + curItem.Bid1.ToString("0.00").Replace(",", ".") + '\t');
                    sw.Write(curItem.Ask1.ToString("0.00").Replace(",", ".") + '\t' + curItem.AskSize1.ToString("0.00").Replace(",", ".") + "\r\n");
                    sw.Write(curItem.BidSize2.ToString("0.00").Replace(",", ".") + '\t' + curItem.Bid2.ToString("0.00").Replace(",", ".") + '\t');
                    sw.Write(curItem.Ask2.ToString("0.00").Replace(",", ".") + '\t' + curItem.AskSize2.ToString("0.00").Replace(",", ".") + "\r\n");
                    sw.Write(curItem.BidSize3.ToString("0.00").Replace(",", ".") + '\t' + curItem.Bid3.ToString("0.00").Replace(",", ".") + '\t');
                    sw.Write(curItem.Ask3.ToString("0.00").Replace(",", ".") + '\t' + curItem.AskSize3.ToString("0.00").Replace(",", ".") + "\r\n");
                    sw.Write(curItem.BidSize4.ToString("0.00").Replace(",", ".") + '\t' + curItem.Bid4.ToString("0.00").Replace(",", ".") + '\t');
                    sw.Write(curItem.Ask4.ToString("0.00").Replace(",", ".") + '\t' + curItem.AskSize4.ToString("0.00").Replace(",", ".") + "\r\n");
                    sw.Write(curItem.BidSize5.ToString("0.00").Replace(",", ".") + '\t' + curItem.Bid5.ToString("0.00").Replace(",", ".") + '\t');
                    sw.Write(curItem.Ask5.ToString("0.00").Replace(",", ".") + '\t' + curItem.AskSize5.ToString("0.00").Replace(",", ".") + "\r\n");
                    sw.Close();
                    fs.Close();
                    OpenFile(fileName);
                }
            }
            else
            {
                MessageBox.Show("Invalid Id Ticker!");
            }
        }

        private void OpenFile(string fileName)
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = fileName;
                process.StartInfo.Verb = "Open";
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                process.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show("Cannot find an application on your system suitable for openning the file with exported data.");
            }
        }
        
    }
}