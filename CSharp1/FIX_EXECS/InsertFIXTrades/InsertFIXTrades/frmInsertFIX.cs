using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using NFIXConn;

namespace InsertFIXTrades
{
    public partial class frmInsertFIX : Form
    {
        public bool flagRunning = false;
        int Inserted = 0;
        int curUser = 77;

        //FIXConn_Drop curFixConn = new FIXConn_Drop(@"\\NESTSRV03\NESTSoft\FIX\Configs\DropCopy\DropCopy_Initiator.cfg", @"\\NESTSRV03\NESTSoft\FIX\Configs\DropCopy\DropCopy_Acceptor.cfg");
        FIXConn_Drop curFixConn = new FIXConn_Drop(@"T:\FIX\Configs\DropCopy\DropCopy_Initiator.cfg", @"T:\FIX\Configs\DropCopy\DropCopy_Acceptor.cfg");
        //FIXConn_Drop curFixConn = new FIXConn_Drop(@"\\NESTSRV03\NESTSoft\FIX\Configs\DropCopy\DropCopy_Initiator_CapitalMarkets.cfg", "");

        Dictionary<int, int> Accounts = new Dictionary<int, int>();
        Dictionary<int, int> Brokers = new Dictionary<int, int>();

        public frmInsertFIX()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAccounts();
            //curFixConn.UpdateOrderExecInsertedStatus();
            tmrRun.Start();
        }

        private void tmrRun_Tick(object sender, EventArgs e)
        {
            InsertTrades();
        }

        private void cmdRunNow_Click(object sender, EventArgs e)
        {
            //InsertTrades();
        }

        private void LoadAccounts()
        {
            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                Accounts.Clear();

                string SQLString = "SELECT Account_Number, Id_Account FROM dbo.Tb007_Accounts WHERE Account_Number IS NOT NULL";

                DataTable dt = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {
                    Accounts.Add((int)NestDLL.Utils.ParseToDouble(curRow[0]), (int)NestDLL.Utils.ParseToDouble(curRow[1]));
                }
            }

            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                Brokers.Clear();

                string SQLString = "SELECT Id_Account,Id_Broker FROM VW_PortAccounts WHERE Id_Port_Type=1";

                DataTable dt = curConn.Return_DataTable(SQLString);

                foreach (DataRow curRow in dt.Rows)
                {
                    Brokers.Add((int)NestDLL.Utils.ParseToDouble(curRow[0]), (int)NestDLL.Utils.ParseToDouble(curRow[1]));
                }
            }
        }

        private void SetBookSection(NFIXConn.DropExec curOrder)
        {
            using (NestDLL.newNestConn curConn = new NestDLL.newNestConn())
            {
                if (curOrder.IdSecurity != 0)
                {
                    DataTable curDataTable = curConn.Return_DataTable("SELECT IdAssetClass, IdInstrument FROM nestdb.dbo.Tb001_Securities WHERE IdSecurity=" + curOrder.IdSecurity);

                    int IdAssetClass = (int)NestDLL.Utils.ParseToDouble(curDataTable.Rows[0][0]);
                    int IdInstrument = (int)NestDLL.Utils.ParseToDouble(curDataTable.Rows[0][1]);

                    switch (IdAssetClass)
                    {
                        case 1: curOrder.IdBook = 2; curOrder.IdSection = 2; break;
                        case 2: curOrder.IdBook = 2; curOrder.IdSection = 4; break;
                        case 3: curOrder.IdBook = 5; curOrder.IdSection = 1; break;
                        case 4: curOrder.IdBook = 2; curOrder.IdSection = 6; break;
                        default: break;
                    }

                    if (IdInstrument == 4 || IdInstrument == 16)
                    {
                        curOrder.TipoMercado = 1;
                    }
                    else
                    {
                        curOrder.TipoMercado = 2;
                    }

                    if (curOrder.TraderID == "LB" || curOrder.TraderID == "LDB" || curOrder.TraderID == "PP")
                    {
                        curOrder.IdBook = 4;
                        if (IdAssetClass != 3) curOrder.IdSection = 2;
                    }

                    if (curOrder.IdSecurity == 108330 || curOrder.IdSecurity == 222165)
                    {
                        curOrder.IdBook = 2; curOrder.IdSection = 5;
                    }
                }
            }
        }

        private void InsertTrades()
        {
            //foreach (NFIXConn.DropExec curOrder in curFixConn.ExecList)
            //{
            //    if(curOrder.Symbol.getValue() == "FESA4")
            //    {
            //        Console.WriteLine(curOrder.OrderID + "\t" + curOrder.Account + "\t" + curOrder.ExecID + "\t" + curOrder.NetLastShares);
            //    }
            //}

            if (!flagRunning)
            {
                int notInserted = 0;

                flagRunning = true;
                if (curFixConn != null)
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        lock (curFixConn.ExecList)
                        {
                            foreach (NFIXConn.DropExec curOrder in curFixConn.ExecList)
                            {
                                if (!curOrder.Inserted)
                                {
                                    if (curOrder.IdBook == 0)
                                    {
                                        SetBookSection(curOrder);
                                    }

                                    int tempSide = 1;
                                    if (curOrder.NetDone < 0) tempSide = 2;

                                    int curAccount = 0;

                                    if (curOrder.Account.getValue().StartsWith("-"))
                                    {
                                        curAccount = int.Parse(curOrder.Account.getValue().Substring(1, curOrder.Account.getValue().Length - 1));
                                    }
                                    else
                                    {
                                        if (Accounts.ContainsKey(int.Parse(curOrder.Account.getValue())))
                                        {
                                            curAccount = Accounts[int.Parse(curOrder.Account.getValue())];
                                        }
                                        else
                                        {
                                            int tempAccount = int.Parse(curOrder.Account.getValue().Substring(0, curOrder.Account.getValue().Length - 1));
                                            if (Accounts.ContainsKey(tempAccount))
                                            {
                                                curAccount = Accounts[tempAccount];
                                            }
                                            else
                                            {
                                                //if (tempAccount == 99914) curAccount = 1204;
                                                if (tempAccount == 99914) curAccount = 1008;
                                                if (tempAccount == 964) curAccount = 1439;
                                                if (tempAccount == 705588) curAccount = 1233; // Capital Markets
                                            }
                                        }
                                    }

                                    if (curAccount != 0 && curOrder.IdBook != 0)
                                    {
                                        string tempIdOrder = "";

                                        int AdjFactor = 1;
                                        if (tempSide == 2) AdjFactor = -1;

                                        DataTable curDataTable = curConn.Return_DataTable("SELECT Id_Ordem, Quantidade, Tipo_Mercado FROM [NESTDB].[dbo].[Tb012_Ordens] WHERE [Id_Order_Broker]='" + curOrder.OrderID.getValue() + "' AND Data_Abert_Ordem='" + curOrder.TransactTime.ToString("yyyy-MM-dd") + "'");

                                        if (curDataTable.Rows.Count == 0)
                                        {
                                            double iQty = Convert.ToInt32(curOrder.OrderQty.ToString()) * AdjFactor;
                                            string SQLString = " EXEC proc_insert_Tb012_Ordens " +
                                                curOrder.IdSecurity + ", " +
                                                iQty + ", " +
                                                curOrder.LastPrice.ToString().Replace(",", ".") + ", " +
                                                (iQty * curOrder.LastPrice).ToString().Replace(",", ".") + ", " +
                                                curOrder.IdBook + ", " +
                                                curOrder.IdSection + ", " +
                                                curOrder.TipoMercado + ", " +
                                                curUser + ", '" +
                                                curOrder.TransactTime.ToString("yyyy-MM-dd") + "', 1, " +
                                                Brokers[curAccount] + ", '" +
                                                curOrder.TransactTime.ToString("yyyy-MM-dd") + "'," +
                                                curAccount + ", '" +
                                                curOrder.OrderID + "',0," +
                                                tempSide;

                                            string tempVal = curConn.Execute_Query_String(SQLString);
                                            tempIdOrder = curConn.Execute_Query_String("SELECT @@IDENTITY");
                                            //Console.WriteLine("Order - Query " + tempVal + ":: Ident " + tempIdOrder);
                                        }
                                        else
                                        {
                                            tempIdOrder = curDataTable.Rows[0][0].ToString();
                                        }

                                        double TradeAmount = curOrder.NetLastShares * curOrder.LastPrice * AdjFactor;
                                        string SQLStringTrade = " EXEC sp_insert_Tb013_Trades_FIX " +
                                            tempIdOrder + "," +
                                            curOrder.NetLastShares + "," +
                                            curOrder.LastPrice.ToString().Replace(",", ".") + "," +
                                            (curOrder.NetLastShares * curOrder.LastPrice / curOrder.RoundLot).ToString().Replace(",", ".") + "," +
                                            Brokers[curAccount] + " ," +
                                            curUser + ",'" +
                                            curOrder.TransactTime.ToString("yyyy-MM-dd") + "',1,'" +
                                            curOrder.ExecID + "'";

                                        string ExecResult = curConn.Execute_Query_String(SQLStringTrade);
                                        string tempIdExec = curConn.Execute_Query_String("SELECT @@IDENTITY");
                                        //Console.WriteLine("Trade - Query " + ExecResult + ":: Ident " + tempIdOrder);

                                        if (tempIdExec != "")
                                            curFixConn.UpdateExecDatabaseID(curOrder.ExecID, int.Parse(tempIdExec));

                                        Inserted++;
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {
                                    notInserted++;
                                }
                            }
                        }
                    }
                }
                flagRunning = false;
                lblLastRunTime.Text = DateTime.Now.ToString();
                lblNotInserted.Text = notInserted.ToString();
            }
            lblInserted.Text = Inserted.ToString();
        }

        private void InsertTrades2()
        {
            //    if (!flagRunning)
            //    {
            //        int notInserted = 0;
            //        flagRunning = true;
            //        cmdRunNow.Enabled = false;
            //        lblLastRunTime.Text = "Running";
            //        using (newNestConn curConn = new newNestConn())
            //        {
            //            string StringSQL = "SELECT IdTbTradeIntermediaria " +
            //                            " FROM dbo.Tb800_FIX_Drop_Copies(nolock) " +
            //                            " WHERE TransactTime >= convert(varchar,getdate(),112) + ' 00:00:00' AND  TransactTime <= convert(varchar,getdate(),112) + ' 23:59:00' " +
            //                            " and Order_Copy = 0" +
            //                            " ORDER BY TransactTime DESC";


            //            DataTable curTable = curConn.Return_DataTable(StringSQL);

            //            foreach (DataRow curRow in curTable.Rows)
            //            {
            //                string tempResult = curConn.Execute_Query_String("EXEC [PROC_FIX_INSERT_Drop_Copies2] @Id_Table_Fix=" + curRow[0]);
            //                if (tempResult.Length != 0)
            //                {
            //                    string curOrder = tempResult.Split('-')[1];
            //                    curConn.ExecuteNonQuery("UPDATE dbo.Tb800_FIX_Drop_Copies SET Id_Ordem=" + curOrder + ",Order_Copy=1 WHERE IdTbTradeIntermediaria = " + curRow[0]);
            //                    Inserted++;
            //                }
            //                else
            //                {
            //                    notInserted++;
            //                }
            //                lblNotInserted.Text = notInserted.ToString();
            //                lblInserted.Text = Inserted.ToString();
            //                Application.DoEvents();
            //            }
            //            cmdRunNow.Enabled = true;
            //        }
            //        flagRunning = false;
            //        lblLastRunTime.Text = DateTime.Now.ToString();
            //        lblNotInserted.Text = notInserted.ToString();
            //        lblInserted.Text = Inserted.ToString();
            //    }
        }
    }
}
