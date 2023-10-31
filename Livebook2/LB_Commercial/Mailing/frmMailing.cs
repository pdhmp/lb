using System;
using System.Collections.Generic;
using System.Data;
using LiveDLL;
using PerceptiveMCAPI;
using PerceptiveMCAPI.Methods;
using PerceptiveMCAPI.Types;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace LiveBook
{
    public partial class frmMailing : LBForm
    {

        public newNestConn curConn = new newNestConn();
        public Dictionary<string, Request> AllRequests = new Dictionary<string, Request>();
        public string CustomHTML;

        #region Main
        public frmMailing()
        {
            InitializeComponent();
        }

        private void frmMailing_Load(object sender, EventArgs e) { UpdateCounters(); }

        private void UpdateCounters()
        {
            using (newNestConn curConn = new newNestConn())
            {
                labDNAV_FIACount.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(10, RepType.Daily) + "=1").ToString();
                labDNAV_LOCount.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(80, RepType.Daily) + "=1").ToString();
                labDNAV_ARB30Count.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(62, RepType.Daily) + "=1").ToString();
                labDNAV_ICATUCount.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(55, RepType.Daily) + "=1").ToString();
                labDNAV_SULACount.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(50, RepType.Daily) + "=1").ToString();
                labDNAV_HEDGECount.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(60, RepType.Daily) + "=1").ToString();

                labMREP_FIACount.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(10, RepType.Monthly) + "=1").ToString();
                labMREP_LOCount.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(80, RepType.Monthly) + "=1").ToString();
                labMREP_ARB30Count.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(62, RepType.Monthly) + "=1").ToString();
                labMREP_ICATUCount.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(55, RepType.Monthly) + "=1").ToString();
                labMREP_SULACount.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(50, RepType.Monthly) + "=1").ToString();
                labMREP_HEDGECount.Text = curConn.Return_Int("SELECT COUNT(*) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(60, RepType.Monthly) + "=1").ToString();

                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_DNAV_Bravo) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(10, RepType.Daily) + "=1") < DateTime.Now.Date) FormatLabel(labDNAV_FIA, "Preview"); else FormatLabel(labDNAV_FIA, "OK");
                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_DNAV_LO) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(80, RepType.Daily) + "=1") < DateTime.Now.Date) FormatLabel(labDNAV_LO, "Preview"); else FormatLabel(labDNAV_LO, "OK");
                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_DNAV_ARB30) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(62, RepType.Daily) + "=1") < DateTime.Now.Date) FormatLabel(labDNAV_ARB30, "Preview"); else FormatLabel(labDNAV_ARB30, "OK");
                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_DNAV_Icatu) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(55, RepType.Daily) + "=1") < DateTime.Now.Date) FormatLabel(labDNAV_ICATU, "Preview"); else FormatLabel(labDNAV_ICATU, "OK");
                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_DNAV_Prev) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(50, RepType.Daily) + "=1") < DateTime.Now.Date) FormatLabel(labDNAV_SULA, "Preview"); else FormatLabel(labDNAV_SULA, "OK");
                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_DNAV_MultiEstrategia) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(60, RepType.Daily) + "=1") < DateTime.Now.Date) FormatLabel(labDNAV_HEDGE, "Preview"); else FormatLabel(labDNAV_HEDGE, "OK");

                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_MReport_Bravo) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(10, RepType.Monthly) + "=1") < DateTime.Now.Date) FormatLabel(labMREP_FIA, "Preview"); else FormatLabel(labMREP_FIA, "OK");
                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_MReport_LO) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(80, RepType.Monthly) + "=1") < DateTime.Now.Date) FormatLabel(labMREP_LO, "Preview"); else FormatLabel(labMREP_LO, "OK");
                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_MReport_ARB30) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(62, RepType.Monthly) + "=1") < DateTime.Now.Date) FormatLabel(labMREP_ARB30, "Preview"); else FormatLabel(labMREP_ARB30, "OK");
                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_MReport_Bravo) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(55, RepType.Monthly)) < DateTime.Now.Date) FormatLabel(labMREP_ICATU, "Preview"); else FormatLabel(labMREP_ICATU, "OK");
                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_MReport_Prev) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(50, RepType.Monthly) + "=1") < DateTime.Now.Date) FormatLabel(labMREP_SULA, "Preview"); else FormatLabel(labMREP_SULA, "OK");
                if (curConn.Return_DateTime("SELECT MAX(Last_Date_Send_MReport_MultiEstrategia) FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(60, RepType.Monthly) + "=1") < DateTime.Now.Date) FormatLabel(labMREP_HEDGE, "Preview"); else FormatLabel(labMREP_HEDGE, "OK");
            }
        }

        private void FormatLabel(Label curLabel, string curText)
        {
            if (curText == "Preview")
            {
                curLabel.Text = "Preview";
                curLabel.Font = new System.Drawing.Font(curLabel.Font, System.Drawing.FontStyle.Underline);
                curLabel.ForeColor = System.Drawing.Color.Blue;
            }

            if (curText == "OK")
            {
                curLabel.Text = "OK";
                curLabel.Font = new System.Drawing.Font(curLabel.Font, System.Drawing.FontStyle.Regular);
                curLabel.ForeColor = System.Drawing.Color.Black;
            }
        }
        #endregion

        #region DailyLabels

        private void labDNAV_FIA_Click(object sender, EventArgs e) { if (labDNAV_FIA.Text != "OK") { SendDaily(10, false); } }

        private void labDNAV_LO_Click(object sender, EventArgs e) { if (labDNAV_ARB30.Text != "OK") { SendDaily(80, false); } }

        private void labDNAV_SULA_Click(object sender, EventArgs e) { if (labDNAV_SULA.Text != "OK") { SendDaily(50, false); } }

        private void labDNAV_ICATU_Click(object sender, EventArgs e) { if (labDNAV_ICATU.Text != "OK") { SendDaily(55, false); } }

        private void labDNAV_HEDGE_Click(object sender, EventArgs e) { if (labDNAV_HEDGE.Text != "OK") { SendDaily(60, false); } }

        private void SendDaily(int IdPortfolio, bool Correction)
        {
            RepType curRepType = RepType.Daily;

            if (Correction) curRepType = RepType.Correction;

            Request curRequest = new Request(IdPortfolio, curRepType);

            frmMailingHTMLViewer curViewer = new frmMailingHTMLViewer(curRepType, IdPortfolio);

            try
            {
                curViewer.webOutput.Navigate("about:blank");

                curRequest.GenerateDailyHTML();

                string tempHTML = curRequest.MailHTML;

                curViewer.webOutput.Document.Write(tempHTML);
                curViewer.webOutput.Refresh();
                curViewer.Text = curRequest.GetSubject;

                using (newNestConn curConn = new newNestConn())
                {
                    String SQLExpression = "SELECT Contact_Mail FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(IdPortfolio, RepType.Daily) + "=1";
                    DataTable ReturnTable = curConn.Return_DataTable(SQLExpression);

                    foreach (DataRow curRow in ReturnTable.Rows)
                    {
                        curViewer.lstSubscribers.Items.Add(curRow["Contact_Mail"].ToString());
                    }
                }


            curViewer.ShowDialog();

            if (curViewer.SendMail)
            {
                curRequest.UnSubscribeAll();
                curRequest.Subscribe();
                curRequest.Send();
                System.Threading.Thread.Sleep(20000);
                curRequest.UnSubscribeAll();
                if(!Correction) curRequest.UpdateDBTimeStamps();
                UpdateCounters();
            }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }

        #endregion

        #region MonthlyLabels

        private void labMREP_FIA_Click(object sender, EventArgs e) { if (labMREP_FIA.Text != "OK") { SendMonthly(10); } }

        private void labMREP_LO_Click(object sender, EventArgs e) { if (labMREP_LO.Text != "OK") { SendMonthly(80); } }

        private void labMREP_ARB30_Click(object sender, EventArgs e) { if (labMREP_ARB30.Text != "OK") { SendMonthly(62); } }

        private void labMREP_SULA_Click(object sender, EventArgs e) { if (labMREP_SULA.Text != "OK") { SendMonthly(50); } }

        private void labMREP_ICATU_Click(object sender, EventArgs e) { if (labMREP_ICATU.Text != "OK") { SendMonthly(55); } }

        private void SendMonthly(int IdPortfolio)
        {
            Request curRequest = new Request(IdPortfolio, RepType.Monthly);

            frmMailingHTMLViewer curViewer = new frmMailingHTMLViewer(RepType.Monthly, IdPortfolio);

            try
            {
                curViewer.webOutput.Navigate("about:blank");

                curRequest.GenerateMonthlyHTML();

                string tempHTML = curRequest.MailHTML;

                curViewer.webOutput.Document.Write(tempHTML);
                curViewer.webOutput.Refresh();
                curViewer.Text = curRequest.GetSubject;

                using (newNestConn curConn = new newNestConn())
                {
                    String SQLExpression = "SELECT Contact_Mail FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(IdPortfolio, RepType.Monthly) + "=1";
                    DataTable ReturnTable = curConn.Return_DataTable(SQLExpression);

                    foreach (DataRow curRow in ReturnTable.Rows)
                    {
                        curViewer.lstSubscribers.Items.Add(curRow["Contact_Mail"].ToString());
                    }
                }
            }
            catch { }

            curViewer.ShowDialog();

            if (curViewer.SendMail)
            {
                curRequest.UnSubscribeAll();
                curRequest.Subscribe();
                curRequest.Send();
                System.Threading.Thread.Sleep(20000);
                curRequest.UnSubscribeAll();
                curRequest.UpdateDBTimeStamps();
                UpdateCounters();
            }
        }

        #endregion

        #region CorrectionLabels

        private void labDNAV_FIACorrection_Click(object sender, EventArgs e) { SendDaily(10, true); }

        private void labDNAV_LOCorrection_Click(object sender, EventArgs e) { SendDaily(80, true); }

        private void labDNAV_ARB30Correction_Click(object sender, EventArgs e) { SendDaily(62, true); }

        private void labDNAV_SULACorrection_Click(object sender, EventArgs e) { SendDaily(50, true); }

        private void labDNAV_ICATUCorrection_Click(object sender, EventArgs e) { SendDaily(55, true); }

        private void labDNAV_HEDGECorrection_Click(object sender, EventArgs e) { SendDaily(60, true); }

        #endregion

        #region DelayLabels

        private void labDNAV_FIADelay_Click(object sender, EventArgs e) { SendDelay(10); }




        private void labDNAV_LODelay_Click(object sender, EventArgs e) { SendDelay(80); }





        private void labDNAV_ARB30Delay_Click(object sender, EventArgs e) { SendDelay(62); }

        private void labDNAV_HEDGEDelay_Click(object sender, EventArgs e) { SendDelay(60); }

        private void labDNAV_SULADelay_Click(object sender, EventArgs e) { SendDelay(50); }

        private void labDNAV_ICATUDelay_Click(object sender, EventArgs e) { SendDelay(55); }

        private void SendDelay(int IdPortfolio)
        {
            RepType curRepType = RepType.Delay;

            Request curRequest = new Request(IdPortfolio, curRepType);

            frmMailingHTMLViewer curViewer = new frmMailingHTMLViewer(RepType.Daily, IdPortfolio);

            try
            {
                curViewer.webOutput.Navigate("about:blank");

                curRequest.GenerateDelayHTML();

                string tempHTML = curRequest.MailHTML;

                curViewer.webOutput.Document.Write(tempHTML);
                curViewer.webOutput.Refresh();
                curViewer.Text = curRequest.GetSubject;

                using (newNestConn curConn = new newNestConn())
                {
                    String SQLExpression = "SELECT Contact_Mail FROM dbo.Tb000_Contacts WHERE " + Request.GetFieldName(IdPortfolio, RepType.Daily) + "=1";
                    DataTable ReturnTable = curConn.Return_DataTable(SQLExpression);

                    foreach (DataRow curRow in ReturnTable.Rows)
                    {
                        curViewer.lstSubscribers.Items.Add(curRow["Contact_Mail"].ToString());
                    }
                }
            }
            catch { }

            curViewer.ShowDialog();

            if (curViewer.SendMail)
            {
                curRequest.UnSubscribeAll();
                curRequest.Subscribe();
                curRequest.Send();
                System.Threading.Thread.Sleep(20000);
                curRequest.UnSubscribeAll();
                curRequest.UpdateDBTimeStamps();
                UpdateCounters();
            }
        }
        #endregion

        #region CustomHTML

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                DialogResult userConfirmation = MessageBox.Show("Deseja selecionar todos os contatos cadastrados na base?", "", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

                if (userConfirmation != System.Windows.Forms.DialogResult.OK)
                {
                    chkAll.Checked = false;
                }
            }
        }

        private void lblImportHTML_DragDrop(object sender, DragEventArgs e)
        {
            CustomHTML = string.Empty;
            //    bool flagImport = false;
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            e.Effect = DragDropEffects.None;

            foreach (string curFile in files)
            {
                if (curFile.Contains(".htm"))
                {
                    FileStream fs = new FileStream(curFile, FileMode.Open, FileAccess.Read);
                    StreamReader sr = new StreamReader(fs);

                    string tempLine = "";
                    while ((tempLine = sr.ReadLine()) != null)
                    {
                        CustomHTML += tempLine;
                    }
                    fs.Close();
                    sr.Close();

                    panel9.Visible = true;
                    panel8.Visible = true;
                }
                else
                {
                    MessageBox.Show("Somente arquivos HTML podem ser enviados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lblImportHTML_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void btnSend_Click(object sender, EventArgs e) { SendCustom(); }

        Request curRequest = null;

        private void SendCustom()
        {
            RepType curRepType = RepType.Custom;

            curRequest = new Request(-99, curRepType);

            frmMailingHTMLViewer curViewer = new frmMailingHTMLViewer();

            string SQLExpression = "SELECT [Contact_Name],[Contact_Mail] FROM dbo.Tb000_Contacts " + ReturnWhere();

            try
            {
                curViewer.webOutput.Navigate("about:blank");

                curRequest.MailHTML = CustomHTML;

                curViewer.webOutput.Document.Write(curRequest.MailHTML);
                curViewer.webOutput.Refresh();
                curViewer.Text = curRequest.GetSubject;

                using (newNestConn curConn = new newNestConn())
                {
                    DataTable ReturnTable = curConn.Return_DataTable(SQLExpression);

                    if (ReturnTable != null && ReturnTable.Rows.Count > 0)
                    {
                        foreach (DataRow curRow in ReturnTable.Rows)
                        {
                            curViewer.lstSubscribers.Items.Add(curRow["Contact_Mail"].ToString());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Selecione a lista de contatos que deseja enviar."); return;
                    }
                }
            }
            catch { }

            curViewer.ShowDialog();

            if (curViewer.SendMail)
            {
                curRequest.UnSubscribeAll();
                curRequest.Subscribe(SQLExpression);
                curRequest.Send();
                System.Threading.Thread.Sleep(20000);
                curRequest.UnSubscribeAll();
                curRequest.UpdateDBTimeStamps();
                UpdateCounters();
            }
        }

        public string ReturnWhere()
        {
            if (chkAll.Checked) { return ""; }

            string ReturnWhere = "WHERE 0=1";
            if (chkDaily.Checked)
            {
                if (chkAcoes.Checked) { ReturnWhere += " OR DNAV_Bravo=1 "; }
                if (chkLO.Checked) { ReturnWhere += " OR DNAV_LO=1 "; }
                if (chkArb30.Checked) { ReturnWhere += " OR DNAV_ARB30=1 "; }
                if (chkHedge.Checked) { ReturnWhere += " OR DNAV_MultiEstrategia=1 "; }
                if (chkIcatu.Checked) { ReturnWhere += " OR DNAV_ICATU=1 "; }
                if (chkPrev.Checked) { ReturnWhere += " OR DNAV_PREV=1 "; }
            }

            if (chkMonthly.Checked)
            {
                if (chkAcoes.Checked) { ReturnWhere += " OR MReport_Bravo=1 "; }
                if (chkLO.Checked) { ReturnWhere += " OR MReport_LO=1 "; }
                if (chkArb30.Checked) { ReturnWhere += " OR MReport_ARB30=1 "; }
                if (chkHedge.Checked) { ReturnWhere += " OR MReport_MultiEstrategia=1 "; }
                if (chkIcatu.Checked) { ReturnWhere += " OR MReport_ICATU=1 "; }
                if (chkPrev.Checked) { ReturnWhere += " OR MReport_PREV=1 "; }
            }

            return ReturnWhere + ";";
        }

        #endregion

        private void frmMailing_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (curRequest != null)
            { while (!curRequest.Done && curRequest != null) { Thread.Sleep(1000); } }
        }

        private void btnManageContacts_Click(object sender, EventArgs e)
        {

        }

        private void btnEditContacts_Click(object sender, EventArgs e)
        {
            frmEditMailingContacts curEditor = new frmEditMailingContacts();
            curEditor.ShowDialog();
        }
  }

    public class ChimpContact
    {
        public int IdPortfolio;
        public string Name;
        public string Email;
        public RepType Type;
    }

    public class Request
    {
        public string curStatus = "";
        public bool Done = false;

        public string GetKey
        {
            //  Set this to your Mailchimp API key for testing
            //  See http://kb.mailchimp.com/article/where-can-i-find-my-api-key
            //  for help finding your API key
            get { return "e61743af86fd1b04ea855e29e5c778d7-us3"; }
        }

        public Request(int _IdPortfolio, RepType Type)
        {
            this.IdPortfolio = _IdPortfolio;
            this.Type = Type;
            this.RequestKey = IdPortfolio + "_" + Type;
            ContactList = new List<ChimpContact>();
        }
        private newNestConn curConn = new newNestConn();
        LB_HTML HTMLEngine = new LB_HTML();
        public int IdPortfolio;
        public RepType Type;
        public string RequestKey;
        public List<ChimpContact> ContactList;

        public string GetListID
        {
            get
            {
                // return "d322db3fc7"; //lista teste
                if (Type == RepType.Custom)
                {
                    return "486a443ddd"; // Lista para todos os emails personalizados.
                }

                else if (Type == RepType.Daily || Type == RepType.Delay || Type == RepType.Correction)
                {
                    switch (IdPortfolio)
                    {
                        case 4: return "e80a9f76ad";
                        case 10: return "a2655edf4c";
                        case 38: return "5a94729b7c";
                        case 50: return "8ee675fa30";
                        case 55: return "0623fca646";
                        case 60: return "35cae17498";
                        case 62: return "e0d4b1381b";
                        case 80: return "2def9ce245";
                    }
                }

                else if (Type == RepType.Monthly)
                {
                    switch (IdPortfolio)
                    {
                        case 4: return "75ff78f55f";
                        case 10: return "c37144b287";
                        case 38: return "60e9bb6f6b";
                        case 50: return "ebb2ec9bc4";
                        case 55: return "c22c85c2a2";
                        case 60: return "2f5e3e6b0f";
                        case 62: return "cab90a39d3";
                        case 80: return "3f55f0a20b";
                    }
                }
                return "";
            }
        }
        public string GetSubject
        {
            get
            {
                string ReturnString = "";
                if (Type == RepType.Daily) ReturnString = ReturnString + "Cota Diária - " + GetFundName(IdPortfolio);
                else if (Type == RepType.Monthly) ReturnString = ReturnString + "Relatório Mensal - " + GetFundName(IdPortfolio);
                else if (Type == RepType.Delay) ReturnString = ReturnString + "Atraso de Cotas - " + GetFundName(IdPortfolio);
                else if (Type == RepType.Correction) ReturnString = ReturnString + "Cota Diária - " + GetFundName(IdPortfolio) + " - ### CORREÇÃO ###";
                else if (Type == RepType.Custom) ReturnString = "Informativo - Nest Investimentos";
                //else if (Type == RepType.Custom) ReturnString = "Feliz Natal e Próspero Ano Novo";

                return ReturnString;
            }
        }
        private string GetFundName(int IdPortfolio)
        {
            switch (IdPortfolio)
            {
                case 4: return "Nest Equity Hedge";
                case 10: return "Nest Ações FIC FIA";
                case 38: return "Nest Arb FIC FIM";
                case 62: return "Nest Arb 30 FIC FIM";
                case 50: return "SulAmérica Nest Prev FIM";
                case 55: return "Icatu Nest Previdenciario FIM";
                case 60: return "Nest Hedge FIC FIM";
                case 80: return "Nest Long Only FIA";
                default: return "";
            }
        }

        public string MailHTML = "";

        public void GenerateDailyHTML()
        {
            string tempFile, TableHeaderString, ClientTableString;

            String TableFund = "";
            string ClientMessageString = "";

            if (Type == RepType.Daily || Type == RepType.Correction)
            {
                tempFile = "boletim_diario.html";
                //tempFile = "Atraso de Cotas.html"
                //tempFile = "Comunicado.html"
                string Path = "N:\\Sales\\Website\\Mail Templates\\" + tempFile;

                FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                string tempLine = "";
                while ((tempLine = sr.ReadLine()) != null)
                {
                    ClientMessageString += tempLine;
                }
                fs.Close();
                sr.Close();

                if (Type == RepType.Daily)
                    ClientMessageString = ClientMessageString.Replace("<!-- INTRODUCTION MESSAGE -->", "Segue abaixo o informativo diário de cotas:");
                else
                    ClientMessageString = ClientMessageString.Replace("<!-- INTRODUCTION MESSAGE -->", "Por favor, desconsiderar a cota do fundo " + GetFundName(IdPortfolio) + " enviada anteriormente, a cota correta encontra-se abaixo e foi atualizada corretamente em nosso site.");



                TableHeaderString = "<table align=\"center\" cellpadding=\"0\" cellspacing=\"0\" width=\"642\"><tbody>" +
                                    "<tr align=\"middle\" bgcolor=\"#666666\"> " +
                                        "<td height=\"20\" width=\"160\"><div align=\"center\"><font color=\"#ffffff\" face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">FUNDO</font></div></td> " +
                                        "<td height=\"20\" width=\"77\"><div align=\"center\"><font color=\"#ffffff\" face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">DATA</font></div></td> " +
                                        "<td height=\"20\" width=\"87\"><font color=\"#ffffff\" face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">COTA</font></td> " +
                                        "<td height=\"20\" width=\"56\"><div align=\"center\"><font color=\"#ffffff\" face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">DIA</font></div></td> " +
                                        "<td height=\"20\" width=\"60\"><div align=\"center\"><font color=\"#ffffff\" face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">MÊS</font></div></td> " +
                                        "<td height=\"20\" width=\"57\"><div align=\"center\"><font color=\"#ffffff\" face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">ANO</font></div></td> " +
                                        "<td height=\"20\" width=\"69\"><div align=\"center\"><font color=\"#ffffff\" face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">12 M</font></div></td> " +
                                        "<td height=\"20\" width=\"76\"><div align=\"center\"><font color=\"#ffffff\" face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">24 M</font></div></td>" +
                                    "</tr>";

                if (IdPortfolio == 60) IdPortfolio = 20;
                String SQLExpressionFund = "SELECT IdPortfolio,PortName, CONVERT(datetime, LastDate) AS LastDate,LastNAV,Today, MTD, YTD, [12m], [24m]  FROM NESTDB.[dbo].[FCN_Daily_Email]() WHERE IdPortfolio=" + IdPortfolio + " OR IdPortfolio=0 ORDER BY ShowOrder DESC";
                DataTable ReturnTableFund = curConn.Return_DataTable(SQLExpressionFund);
                if (IdPortfolio == 20) IdPortfolio = 60;
                foreach (DataRow curRow in ReturnTableFund.Rows)
                {
                    string bgcolor = "#cccccc";
                    string boldin = "<b>";
                    string boldout = "</b>";

                    string NumberFormat = "#,##0.00000000";
                    if (curRow["PortName"].ToString().Contains("CDI")) NumberFormat = "#,##0.00";
                    if (curRow["PortName"].ToString().Contains("ovespa")) NumberFormat = "#,##0";
                    if (curRow["PortName"].ToString().Contains("PTAX")) NumberFormat = "#,##0.0000";


                    if (LiveDLL.Utils.ParseToDouble(curRow["IdPortfolio"]) == 0)
                    {
                        bgcolor = "white";
                        boldin = "";
                        boldout = "";
                    }
                    TableFund = TableFund + "<tr align=\"left\" bgcolor=\"#999999\" valign=\"bottom\"><td bgcolor=\"" + bgcolor + "\" height=\"18\" width=\"160\"><font face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">" + boldin + "<font color=\"#000000\"><font color=\"" + bgcolor + "\">-</font></font>" + curRow["PortName"] + "" + boldout + "</font></td> " +
                        "<td bgcolor=\"" + bgcolor + "\" height=\"18\" width=\"77\"><div align=\"center\">" + boldin + "<font face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">" + LiveDLL.Utils.ParseToDateTime(curRow["LastDate"]).ToString("dd-MMM") + "</font>" + boldout + "</div></td> " +
                        "<td bgcolor=\"" + bgcolor + "\" height=\"18\" width=\"87\"><div align=\"center\">" + boldin + "<font face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">" + TextFormat(curRow["LastNAV"], NumberFormat) + "</font>" + boldout + "</div></td> " +
                        "<td bgcolor=\"" + bgcolor + "\" height=\"18\" width=\"56\"><div align=\"center\">" + boldin + "<font face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">" + TextFormat(curRow["Today"], "0.00%") + "</font>" + boldout + "</div></td> " +
                        "<td bgcolor=\"" + bgcolor + "\" height=\"18\" width=\"60\"><div align=\"center\">" + boldin + "<font face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">" + TextFormat(curRow["MTD"], "0.00%") + "</font>" + boldout + "</div></td> " +
                        "<td bgcolor=\"" + bgcolor + "\" height=\"18\" width=\"57\"><div align=\"center\">" + boldin + "<font face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">" + TextFormat(curRow["YTD"], "0.00%") + "</font>" + boldout + "</div></td> " +
                        "<td bgcolor=\"" + bgcolor + "\" height=\"18\" width=\"69\"><div align=\"center\">" + boldin + "<font face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">" + TextFormat(curRow["12m"], "0.00%") + "</font>" + boldout + "</div></td> " +
                        "<td bgcolor=\"" + bgcolor + "\" height=\"18\" width=\"76\"><div align=\"center\"><font face=\"Verdana, Arial, Helvetica, sans-serif\" size=\"1\">" + boldin + "" + TextFormat(curRow["24m"], "0.00%") + "" + boldout + "&nbsp;</font></div></td> " +
                        "</tr>";
                }


                ClientTableString = TableHeaderString + TableFund;

                ClientTableString = ClientTableString + "<tr><td bgcolor=white height=\"3\" colspan=8></td></tr></tbody></table>";

                ClientMessageString = ClientMessageString.Replace("<!-- PRICES_TABLE -->", ClientTableString);

            }

            MailHTML = ClientMessageString;
        }

        public void GenerateDelayHTML()
        {
            string tempFile;

            string ClientMessageString = "";

            if (Type == RepType.Delay)
            {
                // tempFile = "boletim_diario.html";
                tempFile = "Atraso de Cotas.html";
                //tempFile = "Comunicado.html"
                string Path = "N:\\Sales\\Website\\Mail Templates\\" + tempFile;

                FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                string tempLine = "";
                while ((tempLine = sr.ReadLine()) != null)
                {
                    ClientMessageString += tempLine;
                }
                fs.Close();
                sr.Close();

                string ClientFundsString = " A cota do fundo " + GetFundName(IdPortfolio) + " será enviada";

                ClientMessageString = ClientMessageString.Replace("<!--FUNDOS-->", ClientFundsString);

            }

            MailHTML = ClientMessageString;
        }

        private string TextFormat(object ValueToFormat, string StringFormat)
        {
            if (DBNull.Value.Equals(ValueToFormat))
            {
                return "NA";
            }
            else
            {
                return LiveDLL.Utils.ParseToDouble(ValueToFormat).ToString(StringFormat);
            }
        }

        public void GenerateMonthlyHTML()
        {
            string tempFile;
            string ClientMessageString = "";

            if (Type == RepType.Monthly)
            {
                //tempFile = "relatorio_mensal.html";
                tempFile = "relatorio_mensal - Copy.html";


                string Path = "N:\\Sales\\Website\\Mail Templates\\" + tempFile;

                FileStream fs = new FileStream(Path, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);

                string tempLine = "";
                while ((tempLine = sr.ReadLine()) != null)
                {
                    ClientMessageString += tempLine;
                }
                fs.Close();
                sr.Close();

                string FundName = "";
                string RepURL = "";
                DateTime RepDate = (DateTime.Now.AddDays(-DateTime.Now.Day));

                switch (IdPortfolio)
                {
                    case 10: FundName = "Nest Ações FIC FIA";
                        RepURL = "http://www.nestinvestimentos.com.br/mellon/files/relatorios/" + RepDate.ToString("yyyy_MM") + "_-_Relatorio_Mensal_FIA.pdf";
                        break;
                    case 38: FundName = "Nest Arb FIC FIM";
                        RepURL = "http://www.nestinvestimentos.com.br/mellon/files/relatorios/" + RepDate.ToString("yyyy_MM") + "_-_Relatorio_Mensal_Arb.pdf";
                        break;
                    case 62: FundName = "Nest Arb 30 FIC FIM";
                        RepURL = "http://www.nestinvestimentos.com.br/mellon/files/relatorios/" + RepDate.ToString("yyyy_MM") + "_-_Relatorio_Mensal_ARB30.pdf";
                        break;
                    case 50: FundName = "SulAmerica Nest Prev FIM";
                        RepURL = "http://www.nestinvestimentos.com.br/mellon/files/relatorios/" + RepDate.ToString("yyyy_MM") + "_-_Relatorio_Mensal_Nest_Prev.pdf";
                        break;
                    case 55: FundName = "Icatu Nest Previdenciario FIM";
                        RepURL = "http://www.nestinvestimentos.com.br/mellon/files/relatorios/" + RepDate.ToString("yyyy_MM") + "_-_Relatorio_Mensal_Icatu.pdf";
                        break;
                    case 80: FundName = "Nest Long Only FIA";
                        RepURL = "http://www.nestinvestimentos.com.br/mellon/files/relatorios/" + RepDate.ToString("yyyy_MM") + "_-_Relatorio_Mensal_LO.pdf";
                        break;
                    default: break;
                }

                ClientMessageString = ClientMessageString.Replace("<!-- REPDATE -->", RepDate.ToString("MMM-yy"));
                ClientMessageString = ClientMessageString.Replace("<!-- REPFUND -->", FundName);
                ClientMessageString = ClientMessageString.Replace("<!-- REPURL -->", RepURL);
                //ClientMessageString = ClientMessageString.Replace("<!-- CUSTOM MESSAGE -->", ClientCustomMessage + "<BR>");


            }

            MailHTML = ClientMessageString;

        }

        private void LoadContacts(string SQLString)
        {
            DataTable ReturnTable = curConn.Return_DataTable(SQLString);

            foreach (DataRow curRow in ReturnTable.Rows)
            {
                ChimpContact curContact = new ChimpContact();
                curContact.IdPortfolio = IdPortfolio;
                curContact.Type = Type;
                curContact.Name = curRow["Contact_Name"].ToString();
                curContact.Email = curRow["Contact_Mail"].ToString();

                if (!ContactList.Contains(curContact)) { ContactList.Add(curContact); }
            }
        }

        private void LoadContacts()
        {
            string SQLString = "SELECT [Contact_Name],[Contact_Mail] FROM [NESTDB].[dbo].[Tb000_Contacts] WHERE " + GetFieldName(IdPortfolio, Type) + "=1 ;";

            DataTable ReturnTable = curConn.Return_DataTable(SQLString);

            foreach (DataRow curRow in ReturnTable.Rows)
            {
                ChimpContact curContact = new ChimpContact();
                curContact.IdPortfolio = IdPortfolio;
                curContact.Type = Type;
                curContact.Name = curRow["Contact_Name"].ToString();
                curContact.Email = curRow["Contact_Mail"].ToString();

                if (!ContactList.Contains(curContact)) { ContactList.Add(curContact); }
            }
        }

        public void UpdateDBTimeStamps()
        {
            string SQLString = "UPDATE [NESTDB].[dbo].[Tb000_Contacts] SET Last_Date_Send_" + GetFieldName(IdPortfolio, Type) + "=getdate() WHERE " + GetFieldName(IdPortfolio, Type) + "=1 ;";

            curConn.ExecuteNonQuery(SQLString);
        }

        public void UnSubscribeAll()
        {
            string curListID = GetListID;
            string curKey = GetKey;


            listMembersInput listIn = new listMembersInput(curKey, curListID, EnumValues.listMembers_status.subscribed, 0, 10000);
            listMembers listMem = new listMembers(listIn);
            listMembersOutput listOut = listMem.Execute();

            foreach (listMembersResults.data_item rs in listOut.result.data)
            {
                listUnsubscribeInput curUnsubscribe = new listUnsubscribeInput(curKey, curListID, true, false, false);
                curUnsubscribe.parms.apikey = curKey;
                curUnsubscribe.parms.id = curListID;
                curUnsubscribe.parms.email_address = rs.email;
                listUnsubscribe curunsub = new listUnsubscribe(curUnsubscribe);
                curunsub.Execute();
                listUnsubscribeOutput output = new listUnsubscribeOutput(curUnsubscribe);
            }
        }

        public void Subscribe()
        {
            LoadContacts();

            List<Dictionary<string, object>> batch = new List<Dictionary<string, object>>();

            listBatchSubscribeInput curSubscriber = new listBatchSubscribeInput()
            {
                // any directive overrides 
                api_Validate = true,
                api_AccessType = EnumValues.AccessType.Serial,
                api_OutputType = EnumValues.OutputType.JSON,
            };
            // method parameters 

            curSubscriber.parms.apikey = GetKey;
            curSubscriber.parms.id = GetListID;
            curSubscriber.parms.double_optin = false;
            curSubscriber.parms.replace_interests = true;
            curSubscriber.parms.update_existing = true;

            foreach (ChimpContact curContact in ContactList)
            {
                Dictionary<string, object> curBatchInformation = new Dictionary<string, object>();
                curBatchInformation.Add("EMAIL", curContact.Email);
                //entry.Add("EMAIL_TYPE", curContact.email_type);
                curBatchInformation.Add("FNAME", curContact.Name);
                //entry.Add("LNAME", curContact.last_name);
                batch.Add(curBatchInformation);
            }
            curSubscriber.parms.batch = batch;

            listBatchSubscribeOutput output = new listBatchSubscribe(curSubscriber).Execute();
        }

        public void Subscribe(string SQLString)
        {
            LoadContacts(SQLString);

            List<Dictionary<string, object>> batch = new List<Dictionary<string, object>>();

            listBatchSubscribeInput curSubscriber = new listBatchSubscribeInput()
            {
                // any directive overrides 
                api_Validate = true,
                api_AccessType = EnumValues.AccessType.Serial,
                api_OutputType = EnumValues.OutputType.JSON,
            };
            // method parameters 

            curSubscriber.parms.apikey = GetKey;
            curSubscriber.parms.id = GetListID;
            curSubscriber.parms.double_optin = false;
            curSubscriber.parms.replace_interests = true;
            curSubscriber.parms.update_existing = true;

            foreach (ChimpContact curContact in ContactList)
            {
                Dictionary<string, object> curBatchInformation = new Dictionary<string, object>();
                curBatchInformation.Add("EMAIL", curContact.Email);
                //entry.Add("EMAIL_TYPE", curContact.email_type);
                curBatchInformation.Add("FNAME", curContact.Name);
                //entry.Add("LNAME", curContact.last_name);
                batch.Add(curBatchInformation);
            }
            curSubscriber.parms.batch = batch;

            listBatchSubscribeOutput output = new listBatchSubscribe(curSubscriber).Execute();
        }

        public void Send()
        {
            campaignCreateOptions campaignCreateOpt = new campaignCreateOptions();
            campaignCreateOpt.list_id = GetListID;
            campaignCreateOpt.subject = GetSubject;
            campaignCreateOpt.from_email = "relatorios@nestinvestimentos.com.br";
            campaignCreateOpt.from_name = "Nest Investimentos";

            Dictionary<string, string> content = new Dictionary<string, string>();

            content.Add("html", MailHTML);

            campaignSegmentOptions csOptions = new campaignSegmentOptions();
            csOptions.match = "all";

            // Need to set a Dictionary typeOptions because null is not supported
            Dictionary<string, string> typeOptions = new Dictionary<string, string>();

            campaignCreateParms campaignCreateParms = new campaignCreateParms(GetKey, EnumValues.campaign_type.regular, campaignCreateOpt, content, csOptions, typeOptions);
            campaignCreateInput campaignCreateInput = new campaignCreateInput(campaignCreateParms);
            campaignCreate campaignCreate = new campaignCreate(campaignCreateInput);
            campaignCreateOutput ccOutput = campaignCreate.Execute(campaignCreateInput);

            campaignSendNowInput campaignSendNowInput = new campaignSendNowInput(GetKey, ccOutput.result);
            campaignSendNow campaignSendNow = new campaignSendNow(campaignSendNowInput);
            campaignSendNowOutput campaignSendNowOutput = campaignSendNow.Execute();
        }

        public static string GetFieldName(int IdPortfolio, RepType Type)
        {
            string FieldName = "";

            if (Type == RepType.Daily || Type == RepType.Correction || Type == RepType.Delay) FieldName += "DNAV_";
            if (Type == RepType.Monthly) FieldName += "MReport_";

            switch (IdPortfolio)
            {
                case 4: return "";
                case 10: return FieldName + "Bravo";
                case 38: return FieldName + "ARB";
                case 62: return FieldName + "ARB30";
                case 50: return FieldName + "PREV";
                case 55: return FieldName + "ICATU";
                case 60: return FieldName + "MultiEstrategia";
                case 80: return FieldName + "LO";
                default: return "";
            }
        }
    }

    public enum RepType
    {
        Daily,
        Monthly,
        Correction,
        Delay,
        Custom
    }

}
