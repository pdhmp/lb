using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using NestDLL;
using System.Threading;

namespace FeedFutura
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        static int[] InsertedIds = new int[30000];
        static int InsertedIdsCounter;
        public newNestConn NestFcn = new newNestConn();

        static string Status="Waiting";
        static string StatusProgram = "Waiting";
        static DateTime LastTime;
        bool isRunning= false;

        private void button1_Click(object sender, EventArgs e)
        {
            Check_Insert();
        }
        
        public string GetOperacoes()
        {
            GetFuturaExecutions.WSIntegrador operacoes = new GetFuturaExecutions.WSIntegrador();
            string xml = "";

            try
            {
                xml = operacoes.QueryTradeStr();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Status = "Connection Error.";
                xml = "Erro" + e.ToString();
            }
            return xml;
        }
        
        void Insert_Data()
        {
            StatusProgram = "Running";
            Status = "Getting";

            string xmlOperacoes = GetOperacoes();
            Status = "Reading";

            if (xmlOperacoes != "")
            {
                xmlOperacoes = xmlOperacoes.Replace("\n", "");
                System.IO.StringReader xmlSr = new System.IO.StringReader(xmlOperacoes);

                DataSet ds = new DataSet();
                ds.ReadXml(xmlSr, XmlReadMode.Auto);
                DataTable tabela;

                if (ds.Tables.Count > 0)
                {
                    tabela = ds.Tables[0];

                    string Symbol;
                    DateTime DataPregao;
                    string hora;
                    string Qtd;
                    string Preco;
                    string CodCliente;
                    string Side;
                    string OrderID;

                    string StringSQL = "";
                    int Side_Number = 0;
                    try
                    {

                        foreach (DataRow row in tabela.Rows)
                        {
                            {
                                DataPregao = Convert.ToDateTime(row["dt-pregao"].ToString());
                                hora = row["hh-negocio"].ToString();
                                Symbol = row["cd-negocio"].ToString();
                                Qtd = row["qt-negocio"].ToString();
                                Preco = row["vl-negocio"].ToString();
                                CodCliente = row["cd-cliente"].ToString();
                                OrderID = row["nr-negocio"].ToString();
                                Side = row["tp-operacao"].ToString();
                                if (DataPregao.Day == DateTime.Now.Day)
                                {

                                    StringSQL = StringSQL + " EXEC [Insert_Orders_Generic] @Broker_Account = " + CodCliente +
                                            ", @Broker_Order_Id =" + Preco.ToString().Replace(".", "") + OrderID + DataPregao.ToString("yyyyMMdd").ToString() +
                                            ", @Time = '" + hora + "'" +
                                            ", @Ticker = '" + Symbol + "'" +
                                            ", @Qtd_Order = " + Qtd +
                                            ", @Qtd_Cancel = 0" +
                                            ", @Qtd_Exec = " + Qtd +
                                            ", @Price = " + Preco +
                                            ", @Type = 'TOTAL'" +
                                            ", @Status = 'EXECUTADA'" +
                                            ", @Broker_ID =106" +
                                            ", @Nat_Order = '" + Side + "'" +
                                            ", @Id_Login_Program = 33; ";

                                    if (Side == "C")
                                    {
                                        Side_Number = 1;
                                    }
                                    if (Side == "V")
                                    {
                                        Side_Number = 2;
                                    }

                                    StringSQL = StringSQL + " EXEC [proc_FIX_InsExecReport] " +
                                                "  @Broker_Id = 106" +
                                                ", @Account =" + CodCliente +
                                                ", @AvgPx = 0" +
                                                ", @ClOrdID=0" +
                                                ", @CumQty = 0" +
                                                ", @ExecID = " + Preco.ToString().Replace(".", "") + OrderID + DataPregao.ToString("yyyyMMdd").ToString() +
                                                ", @ExecTransType= 0" +
                                                ", @LastShares = " + Qtd +
                                                ", @LastPx =" + Preco +
                                                ", @OrderID = " + Preco.ToString().Replace(".", "") + OrderID + DataPregao.ToString("yyyyMMdd").ToString() +
                                                ", @OrderQty = 0" +
                                                ", @OrdStatus = 1" +
                                                ", @Side ='" + Side_Number.ToString() + "'" +
                                                ", @Symbol = '" + Symbol + "'" +
                                                ", @Texto = ' '" +
                                                ", @OrigClOrdID = " + CodCliente +
                                                ", @TransactTime = '" + DataPregao.ToString("yyyyMMdd HH:mm:ss") + "'" +
                                                ", @SendingTime = '" + DataPregao.ToString("yyyyMMdd HH:mm:ss") + "'" +
                                                ", @ExecType = 1" +
                                                ", @LeavesQty =0 " +
                                                ", @TargetSubID=0 " +
                                                ", @Id_Login_Program=33" +
                                                ", @Manual_Feed=1 ;";

                                    InsertedIds[InsertedIdsCounter] = Convert.ToInt32(OrderID);
                                    InsertedIdsCounter++;
                                }
                            }
                        }
                        int retorno;
                        Status = "Inserting";

                        if (StringSQL != "")
                        {
                            retorno = NestFcn.ExecuteNonQuery(StringSQL);
                        }
                        StringSQL = "";
                        Status = "Inserted";

                    }

                    catch (Exception err)
                    {
                        MessageBox.Show(err.ToString());
                        Console.WriteLine(err.ToString());
                        StringSQL = "";
                        Status = "Error";
                    }
                    InsertedIdsCounter = 0;
                }
                else
                {
                    Status = "Empty File";
                
                }
            }
            LastTime = DateTime.Now;
            isRunning = false;
   
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Start();
            Check_Insert();
            timer1.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            lblLastUpdate.Text = LastTime.ToString("HH:mm:ss.ms");
            lblInserted.Text = InsertedIdsCounter.ToString();
            lblStatus.Text = Status.ToString();
            lblStatusProgram.Text = StatusProgram.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Check_Insert();
        }

        void Check_Insert()
        {
            DateTime Datenow = DateTime.Now;
            DateTime Datediff = Convert.ToDateTime("20:00:00");

            if (Datenow < Datediff && isRunning==false)
            {
                isRunning = true;

                System.Threading.Thread t1;
                t1 = new System.Threading.Thread(new ThreadStart(Insert_Data));
                t1.Start();

                isRunning = false;

            }
       }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void cmdStop_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            StatusProgram = "Stopped";
        }
    }
}

