using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using System.Timers;
using System.Threading;
using System.IO;

namespace GetXPExecs
{
    public partial class Form1 : Form
    {    

        string[] vBrokers;
        string[] vAccounts;

        public Form1()
        {
            InitializeComponent();
        }

        ctXPI.ctServicoExterno curXP = new ctXPI.ctServicoExterno();

        

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAccounts();
            InsertTimer.Enabled = true;            
            InsertTimer.Start();
            InsertTimer_Tick(this, new EventArgs());
        }

        private DataTable GetXPTrades(string Broker, string Account, string Date)
        {
            ctXPI.XPSyncBrokerSOAPHeader vToken = curXP.AutenticarBroker("NEST", "622651");

            curXP.XPSyncBrokerSOAPHeaderValue = vToken;

            string[] vAvailableBrokers = vToken.avaliableBrokers;
            string[] vAvailableClients = vToken.avaliableClients;

            try
            {
                DataTable vResultado = curXP.XPSyncNegociosConsultar("ANALITICO", Date, Date, Broker, Account, "");

                return vResultado;
            }
            catch (Exception err)
            {
                return null;
            }
        }

        private void LoadAccounts()
        {
            ctXPI.XPSyncBrokerSOAPHeader vToken = curXP.AutenticarBroker("NEST", "622651");

            curXP.XPSyncBrokerSOAPHeaderValue = vToken;

            vBrokers = vToken.avaliableBrokers;
            vAccounts = vToken.avaliableClients;
        }

        private void printTrades()
        {
            DataTable curDataTable = GetXPTrades(vBrokers[0], vAccounts[7], DateTime.Today.ToString("dd/MM/yyyy"));

            foreach (DataRow curRow in curDataTable.Rows)
            {
                string tempVal = curRow[0].ToString();
                for (int i = 0; i < curRow.ItemArray.Length; i++)
                {                    
                    Console.Write(curRow[i] + ";");
                }
                Console.Write("\r\n");
            }
        }

        private void InsertTrades(string Account)
        {
            SortedList<int, SortedList<int, int>> InsertedOrders = CheckInsertedOrders(Account);

            DataTable XPAccountOrders = GetXPTrades(vBrokers[0], Account, DateTime.Today.ToString("dd/MM/yyyy"));

            if (!(XPAccountOrders == null))
            {
                foreach (DataRow curRow in XPAccountOrders.Rows)
                {
                    string TradeAccount = curRow[3].ToString();
                    if (TradeAccount == Account)
                    {
                        bool Insert = false;

                        int OrderId = int.Parse(curRow[16].ToString());
                        int ExecId = int.Parse(curRow[15].ToString());

                        if (InsertedOrders.ContainsKey(OrderId))
                        {
                            if (!InsertedOrders[OrderId].ContainsKey(ExecId))
                            {
                                Insert = true;
                            }
                        }
                        else
                        {
                            Insert = false;
                        }

                        if (!Insert)
                        {
                            if (!InsertedOrders.ContainsKey(OrderId))
                            {
                                InsertedOrders.Add(OrderId, new SortedList<int, int>());
                            }
                            InsertedOrders[OrderId].Add(ExecId, ExecId);

                            string Ticker = curRow[8].ToString();
                            string BloombergTicker = curRow[17].ToString();
                            string Qty = curRow[10].ToString();
                            string Price = curRow[11].ToString().Replace(",", ".");
                            string TradeDate = new DateTime(int.Parse(curRow[13].ToString().Substring(6, 4)), int.Parse(curRow[13].ToString().Substring(3, 2)), int.Parse(curRow[13].ToString().Substring(0, 2))).ToString("yyyy-MM-dd");
                            string Side = curRow[7].ToString() == "C" ? "1" : "2";

                            if (Side == "2")
                            { }

                            InsertTrade(Account, OrderId.ToString(), ExecId.ToString(), Ticker, BloombergTicker, Qty, Price, TradeDate, Side, "LB", "1");

                            for (int g = 0; g < curRow.ItemArray.Length; g++)
                            {
                                StreamWriter srWriter = new StreamWriter(@"C:\Temp\XP_EXEC_20120301.csv", true);
                                srWriter.Write(curRow[g] + ";");
                                srWriter.Close();
                            }
                            StreamWriter srWriterOut = new StreamWriter(@"C:\Temp\XP_EXEC_20120301.csv", true);
                            srWriterOut.Write("\r\n");
                            srWriterOut.Close();
                        }
                    }
                }
            }
        }

        private void InsertTrade(string Account, string IdOrder, string IdExec, string Ticker, string BloombergTicker, string Qty, string Price, string TradeDate, string Side, string User, string ManualFeed)       
        {
            /*
            string InsertHeadSQL =  "INSERT INTO NESTDB.dbo.Tb800_FIX_Drop_Copies " +
                                    //"INSERT INTO dbo.Tb800_FIX_Drop_Copies_TESTE " +
                                    "(Broker_Id,Account,OrderID,ExecID,Symbol,ClOrdID,TransactTime,LastShares, " +
                                    "OrderQty,LastPx,Side,TargetSubID,Manual_Feed,IdSecurity,ExecType) VALUES ";
            */

            string CumQtySQL =  "SELECT SUM(LastShares) " +
                                "FROM NESTDB.DBO.Tb800_FIX_Drop_Copies "+
                                "WHERE OrderID = '" + IdOrder + "'";
            /*
            string IdSecuritySQL =  "SELECT IdSecurity FROM NESTDB.DBO.Tb001_Securities " +
                                    "WHERE SecName = '" + Ticker + "' OR BloombergTicker = '" + BloombergTicker + "' OR ExchangeTicker = '" + Ticker + "'";
            */

            string CumQty = "";
            string IdSecurity = "";

            using (newNestConn conn = new newNestConn())
            {
                CumQty = conn.Return_Double(CumQtySQL).ToString().Replace(",", ".");
                CumQty = double.IsNaN(double.Parse(CumQty)) ? Qty : (int.Parse(CumQty) + int.Parse(Qty)).ToString();
                //IdSecurity = conn.Return_Int(IdSecuritySQL).ToString();
            }

            /*
            InsertHeadSQL +=    "(22," + Account + "," + IdOrder + "," + IdExec + ",'" + Ticker + "',0,'" + TradeDate + "'," + 
                                Qty + "," + CumQty + "," + Price + "," + Side + ",'" + User + "'," + ManualFeed + "," + IdSecurity + ",1)";
            */

            string Proc_Insert_FIX_Trade = "EXEC [proc_FIX_InsExecReport] " +
                                            "  @Broker_Id = 22" +
                                            ", @Account =" + Account +
                                            ", @AvgPx = 0" +
                                            ", @ClOrdID=0" +
                                            ", @CumQty = "+ CumQty +
                                            ", @ExecID = " + IdExec +
                                            ", @ExecTransType= 0" +
                                            ", @LastShares = " + Qty +
                                            ", @LastPx =" + Price +
                                            ", @OrderID = " + IdOrder +
                                            ", @OrderQty = " + CumQty +
                                            ", @OrdStatus = 1" +
                                            ", @Side ='" + Side + "'" +
                                            ", @Symbol = '" + Ticker + "'" +
                                            ", @Texto = ''" +
                                            ", @OrigClOrdID = " + Account +
                                            ", @TransactTime = '" + TradeDate + "'" +
                                            ", @SendingTime = '" + TradeDate + "'" +
                                            ", @ExecType = 1" +
                                            ", @LeavesQty =0 " +
                                            ", @TargetSubID='LB' " +
                                            ", @Id_Login_Program=43" +
                                            ", @Manual_Feed=1 ";

            string SQLUpdateOrderQty =  "UPDATE NESTDB.dbo.Tb800_FIX_Drop_Copies " +
                                        "SET OrderQty = ( " +
                                                            "SELECT MAX(OrderQty) " +
                                                            "FROM NESTDB.dbo.Tb800_FIX_Drop_Copies " +
                                                            "WHERE Account = '" + Account + "' AND OrderID = '" + IdOrder + "'" +
                                        "               )" +
                                        "WHERE Account = '" + Account + "' AND OrderID = '" + IdOrder + "'";

            using (newNestConn conn = new newNestConn())
            {
                //conn.ExecuteNonQuery(InsertHeadSQL);
                conn.ExecuteNonQuery(Proc_Insert_FIX_Trade);

                if (int.Parse(CumQty) > int.Parse(Qty))
                {
                    conn.ExecuteNonQuery(SQLUpdateOrderQty);
                }
            }


        }
        
        private SortedList<int, SortedList<int, int>> CheckInsertedOrders(string Account)
        {
            DataTable InsertedOrdersTable = new DataTable();
            SortedList<int, SortedList<int, int>> InsertedOrders = new SortedList<int, SortedList<int, int>>();

            using (newNestConn conn = new newNestConn())
            {
                string SQL =    "SELECT OrderID,ExecID FROM NESTDB.dbo.Tb800_FIX_Drop_Copies " +
                                "WHERE	Account in ('" + Account + "')";

                InsertedOrdersTable = conn.Return_DataTable(SQL);
            }

            foreach(DataRow curRow in InsertedOrdersTable.Rows)
            {

                if (!InsertedOrders.ContainsKey(int.Parse(curRow[0].ToString())))
                {
                    InsertedOrders.Add(int.Parse(curRow[0].ToString()), new SortedList<int, int>());
                    InsertedOrders[int.Parse(curRow[0].ToString())].Add(int.Parse(curRow[1].ToString()), int.Parse(curRow[1].ToString()));
                }
                else if (!InsertedOrders[int.Parse(curRow[0].ToString())].ContainsKey(int.Parse(curRow[1].ToString())))
                {
                    InsertedOrders[int.Parse(curRow[0].ToString())].Add(int.Parse(curRow[1].ToString()), int.Parse(curRow[1].ToString()));
                }                
            }

            return InsertedOrders;
        
        }
        
        private void printAll()
        {
            button2.Enabled = false;
            bool alreadyprinted = false;

            for (int i = 0; i < vAccounts.Length; i++)
            {
                
                DataTable curDataTable = GetXPTrades(vBrokers[0], vAccounts[i], DateTime.Today.ToString("dd/MM/yyyy"));

                if (!alreadyprinted)
                {
                    foreach (DataColumn curColumn in curDataTable.Columns)
                    {
                        Console.Write(curColumn.ColumnName + ";");
                    }
                    Console.Write("\r\n");
                    alreadyprinted = true;
                }

                foreach (DataRow curRow in curDataTable.Rows)
                {
                    string tempVal = curRow[0].ToString();
                    for (int g = 0; g < curRow.ItemArray.Length; g++)
                    {
                        Console.Write(curRow[g] + ";");
                    }
                    Console.Write("\r\n");
                }
            }
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            printTrades();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            printAll();
        }

        private void InsertTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                InsertTrades("801005");//Arb
                InsertTrades("801002");//Fia
                InsertTrades("900510");//Broker

                //InsertTrades("8477");//Fia
                //InsertTrades("900006");//Broker Bovespa

            }
            catch { }
        }

    }
}
