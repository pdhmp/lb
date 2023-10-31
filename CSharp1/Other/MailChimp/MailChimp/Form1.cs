using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using NestDLL;
using System.Text;
using System.Windows.Forms;
using PerceptiveMCAPI;
using PerceptiveMCAPI.Types;
using PerceptiveMCAPI.Methods;


namespace MailChimp
{

    public partial class MainForm : Form
    {
        public string GetKey
        {
            //  Set this to your Mailchimp API key for testing
            //  See http://kb.mailchimp.com/article/where-can-i-find-my-api-key
            //  for help finding your API key
            get { return "e61743af86fd1b04ea855e29e5c778d7-us3"; }
        }

        private string Daily = "Daily";
        private string Monthly = "Monthly";

        public Dictionary<string, Request> AllContactList = new Dictionary<string, Request>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //Dictionary<string, string> curList = MCAPISettings.ListAPISettings();

            // input parameters, using default apikey 
            listsInput input = new listsInput(GetKey);
            // execution 
            lists cmd = new lists(input);
            listsOutput output = cmd.Execute();
            // format output (Assuming a User control named show_lists) 
        }

        public string CreateCampaign(int IdPortfolio, string Type)
        {
            // compaign Create Options
            campaignCreateOptions campaignCreateOpt = new campaignCreateOptions();
            campaignCreateOpt.list_id = "472dcf7c64aa";
            campaignCreateOpt.subject = "subject";
            campaignCreateOpt.from_email = "relatorios@nestinvestimentos.com.br";
            campaignCreateOpt.from_name = "Nest Investimentos";

            // Content

            Dictionary<string, string> content = new Dictionary<string, string>();
            content.Add("html_ArticleTitle1", "ArticleTitle1");
            content.Add("html_ArticleTitle2", "ArticleTitle2");
            content.Add("html_ArticleTitle3", "ArticleTitle3");
            content.Add("html_Article1", "Article1");
            content.Add("html_Article2", "Article2");

            // Conditions
            List<campaignSegmentCondition> csCondition = new List<campaignSegmentCondition>();
            campaignSegmentCondition csC = new campaignSegmentCondition();

            csC.field = "interests-" + 123; // where 123 is the Grouping Id from listInterestGroupings()
            csC.op = "all";
            csC.value = "";
            csCondition.Add(csC);

            // Options
            campaignSegmentOptions csOptions = new campaignSegmentOptions();
            csOptions.match = "all";



            // Type Options
            Dictionary<string, string> typeOptions = new Dictionary<string, string>();
            typeOptions.Add("offset-units", "days");
            typeOptions.Add("offset-time", "0");
            typeOptions.Add("offset-dir", "after");

            // Create Campaigns

            campaignCreate campaignCreate = new campaignCreate(new campaignCreateInput(GetKey, EnumValues.campaign_type.plaintext, campaignCreateOpt, content, csOptions, typeOptions));
            campaignCreateOutput ccOutput = campaignCreate.Execute();

            List<Api_Error> error = ccOutput.api_ErrorMessages;  // Catching API Errors


            if (error.Count > 0)
            {
                foreach (Api_Error ae in error)
                {
                    Console.WriteLine("\n ERROR Creating Campaign : ERRORCODE\t:" + ae.code + "\t ERROR\t:" + ae.error);
                }
            }

            return ccOutput.result;
        }

        public List<Contact> LoadContacts_DB(int IdPortfolio, string Type)
        {
            List<Contact> curContactList = new List<Contact>();

            using (newNestConn curConn = new newNestConn())
            {
                string Where = GetWhereString(IdPortfolio, Type);

                string SQLString = "SELECT [Contact_Name],[Contact_Mail] FROM [NESTDB].[dbo].[Tb000_Contacts] WHERE " + Where + " ;";

                DataTable ReturnTable = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in ReturnTable.Rows)
                {
                    Contact curContact = new Contact();
                    curContact.IdPortfolio = IdPortfolio;
                    curContact.Type = Type;
                    curContact.Name = curRow["Contact_Name"].ToString();
                    curContact.Email = curRow["Contact_Mail"].ToString();

                    if (!curContactList.Contains(curContact)) { curContactList.Add(curContact); }
                }
            }
            return curContactList;
        }

        public void LoadContacts()
        {
            Request request = null;
            List<Request> RequestList = new List<Request>();

            if (chkDArb.Checked)
            {
                request = new Request(38, Daily); if (!RequestList.Contains(request)) { RequestList.Add(request); }
            }

            if (chkMArb.Checked)
            {
                request = new Request(38, Monthly); if (!RequestList.Contains(request)) { RequestList.Add(request); }
            }

            foreach (Request curRequest in RequestList)
            {
                curRequest.ContactList = LoadContacts_DB(curRequest.IdPortfolio, curRequest.Type);

                if (!AllContactList.ContainsKey(curRequest.IdPortfolio + "_" + curRequest.Type))
                {
                    AllContactList.Add(curRequest.IdPortfolio + "_" + curRequest.Type, curRequest);
                }
            }

            // MessageBox.Show("Contacts Loaded", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnContacts_Click(object sender, EventArgs e)
        {
            LoadContacts();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SynchronizeContacts();

            AllContactList.Clear();
        }


        public void SynchronizeContacts()
        {
            if (AllContactList.Count < 1) return;

            foreach (Request curRequest in AllContactList.Values)
            {
                listBatchSubscribeInput curSubscriber = new listBatchSubscribeInput();

                // any directive overrides 
                curSubscriber.api_Validate = true;
                curSubscriber.api_AccessType = EnumValues.AccessType.Serial;
                curSubscriber.api_OutputType = EnumValues.OutputType.JSON;
                // method parameters 
                curSubscriber.parms.apikey = GetKey;
                curSubscriber.parms.id = curRequest.GetListID();
                curSubscriber.parms.double_optin = false;
                curSubscriber.parms.replace_interests = true;
                curSubscriber.parms.update_existing = true;

                List<Dictionary<string, object>> batch = new List<Dictionary<string, object>>();


                foreach (Contact curContact in curRequest.ContactList)
                {
                    Dictionary<string, object> entry = new Dictionary<string, object>();
                    entry.Add("EMAIL", curContact.Email);
                    //entry.Add("EMAIL_TYPE", curContact.email_type);
                    entry.Add("FNAME", curContact.Name);
                    //entry.Add("LNAME", curContact.last_name);
                    batch.Add(entry);
                }
                curSubscriber.parms.batch = batch;

                listBatchSubscribe cmd = new listBatchSubscribe(curSubscriber);
                listBatchSubscribeOutput output = cmd.Execute();

                if (output.api_ErrorMessages.Count > 0)
                {
                    //showResults(output.api_Request, output.api_Response,  // raw data  
                    //output.api_ErrorMessages, output.api_ValidatorMessages); // & errors 
                }
                else
                {
                    //show_listBatch1.Display(output);
                }
            }

        }


        public string GetWhereString(int IdPortfolio, string Type)
        {
            switch (IdPortfolio)
            {
                case 4:
                    if (Type == Daily) return ""; else return "[MReport_NestFund]=1";
                case 10:
                    if (Type == Daily) return "[DNAV_Bravo]=1"; else return "[MReport_Bravo]=1";
                case 38:
                    if (Type == Daily) return "[DNAV_ARB]=1"; else return "[MReport_ARB]=1";
                case 43:
                    if (Type == Daily) return "[DNAV_MH]=1"; else return "[MReport_MH]=1";
                case 50:
                    if (Type == Daily) return "[DNAV_PREV]=1"; else return "[MReport_PREV]=1";
                case 60:
                    if (Type == Daily) return "[DNAV_MultiEstrategia]=1"; else return "[MReport_MultiEstrategia]=1";
                default:
                    return "";
            }
        }

    }

    public class Contact
    {
        public int IdPortfolio;
        public string Name;
        public string Email;
        public string Type;
    }

    public class Request
    {
        public Request()
        {

        }
        public Request(int IdPortfolio, string Type)
        {
            this.IdPortfolio = IdPortfolio;
            this.Type = Type;
        }

        public int IdPortfolio;
        public string Type;

        public List<Contact> ContactList;

        public string GetListID()
        {
            if (Type == "Daily")
            {
                switch (IdPortfolio)
                {
                    case 4: return "";
                    case 10: return "";
                    case 38: return "5a94729b7c";
                    case 43: return "";
                    case 50: return "";
                    case 60: return "";
                }
            }

            if (Type == "Monthly")
            {
                switch (IdPortfolio)
                {
                    case 4: return "";
                    case 10: return "";
                    case 38: return "60e9bb6f6b";
                    case 43: return "";
                    case 50: return "";
                    case 60: return "";
                }
            }

            return "";
        }
    }
}
