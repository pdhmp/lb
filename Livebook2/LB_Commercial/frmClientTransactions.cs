using System;
using System.Data;
using System.Windows.Forms;
using LiveBook.Business;
using LiveDLL;
using System.Collections;
using DevExpress.Data;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Drawing;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;

namespace LiveBook
{
    public partial class frmClientTransactions : LBForm
    {
        // Business_Class Negocios = new Business_Class();
        // LB_Utils curUtils = new LB_Utils();
        newNestConn curConn = new newNestConn();

        string Transaction_Type_Message = "";

        public frmClientTransactions()
        {
            InitializeComponent();
        }

        private void frmDividends_Load(object sender, EventArgs e)
        {
            chkFromDate.Checked = false;
            dtgClientTransac.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgClientTransac.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgClientTransac.LookAndFeel.SetSkinStyle("Blue");

            Load_Combos();

            Carrega_Grid();
        }

        void Load_Combos()
        {
            LiveDLL.FormUtils.LoadCombo(this.cmbFund, "Select Id_Portfolio,Port_Name from Tb002_Portfolios Where Id_Port_Type=3 and Discountinued=0 order by Port_Name", "Id_Portfolio", "Port_Name", 99);
            LiveDLL.FormUtils.LoadCombo(this.cmbTransType, "Select Id_Trans_Type, Description from dbo.Tb701_Transaction_Types WHERE Id_Trans_Type IN (30,31,32,30,34,35,36,37) order by Description", "Id_Trans_Type", "Description", 21);
            LiveDLL.FormUtils.LoadCombo(this.cmbContact, "SELECT * FROM dbo.Tb751_Contacts order by Contact_Name", "Id_Contact", "Contact_Name", 99);
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (txtShare.Text == "") { txtShare.Text = "0000"; };
            if (txtCash.Text == "") { txtCash.Text = "0"; };
            if (txtSharePrice.Text == "") { txtSharePrice.Text = "0000"; };
            if (txtIncomeTax.Text == "") { txtIncomeTax.Text = "0000"; };

            int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());
            int Id_Contact = Convert.ToInt32(cmbContact.SelectedValue.ToString());

            string AdminID = "";
            string AdminID_Sub = "";

            if (cmbTransType.SelectedValue.ToString() == "30") { AdminID_Sub = txtAdminID.Text; };
            if (cmbTransType.SelectedValue.ToString() != "30") { AdminID = txtAdminID.Text; };

            if (curUtils.IsNumeric(txtCash.Text) || curUtils.IsNumeric(txtShare))
            {
                string SQLString = "INSERT INTO [NESTDB].[dbo].[Tb760_Subscriptions_Mellon]([Id_Contact],[Request_Date],[Trade_Date],[Settlement_Date],[Transaction_Type],[Transaction_NAV],[Id_Portfolio],[AdminRef],[AdminRef_Sub],[Quantity],[Fin_Amount],[IncomeTax],[Create_Timestamp]) " +
                                    "VALUES ( " + Id_Contact + ",'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "','" + dtpConversion.Value.ToString("yyyyMMdd") + "','" + dtpPayment.Value.ToString("yyyyMMdd") + "'," + cmbTransType.SelectedValue.ToString() + ", " + txtSharePrice.Text.Replace(".", "").Replace(",", ".") + ", " + Id_Fund + ", '" + AdminID + "', '" + AdminID_Sub + "', " + txtShare.Text.Replace(",", ".") + ", " + txtCash.Text.Replace(".", "").Replace(",", ".") + ", " + txtIncomeTax.Text.Replace(".", "").Replace(",", ".") + ", GetDate())";

                int retorno = curConn.ExecuteNonQuery(SQLString, 1);

                if (retorno != 0 && retorno != 99)
                {
                    MessageBox.Show(Transaction_Type_Message);
                }
                else
                {
                    MessageBox.Show("There was an error. No data was inserted!", "Error on Insert!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Carrega_Grid();
                txtCash.Text = "";
                txtShare.Text = "";
            }
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Default_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {

            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(tempGrid);
        }

        private void Default_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView tempGrid = (DevExpress.XtraGrid.Views.Grid.GridView)sender;
            curUtils.Save_Columns(tempGrid);
        }

        private void Carrega_Grid()
        {
            string SQLString;

            System.Data.DataTable tablep = new System.Data.DataTable();

            dgClientTransac.Columns.Clear();

            SQLString =
                        " SELECT " +
                        "	A.[Transaction_Id], " +
                        "	A.[Id_Contact] , " +
                        "	A.[Request_Date], " +
                        "	A.[Trade_Date], " +
                        "	A.[Settlement_Date], " +
                        "	A.[Transaction_Type], " +
                        "	A.[Transaction_NAV], " +
                        "	A.[Id_Portfolio], " +
                        "	A.[AdminRef], " +
                        "	A.[AdminRef_Sub], " +
                        "   CASE WHEN A.[Transaction_Type] in (30,34,35,36,37) THEN 1 ELSE -1 END * A.[Quantity] AS Quantity, " +
                        "   CASE WHEN A.[Transaction_Type] in (30,34,35,36,37) THEN 1 ELSE -1 END * A.[Fin_Amount] AS [Fin_Amount], " +
                        "	A.[IncomeTax], " +
                        "	A.[Create_Timestamp], " +
                        "	A.[ClTransfer], " +
                        "	Cont.Contact_Name  Contact_Name , " +
                        "	C.Port_Name, " +
                        "	DATEADD(DD, 1 - DAY(Trade_Date), Trade_Date) AS RefMonth, " +
                        "	D.Description AS TransactionType " +
                        " FROM NESTDB.dbo.Tb760_Subscriptions_Mellon A " +
                        " INNER JOIN NESTDB.dbo.Tb751_Contacts Cont	ON A.Id_Contact = Cont.Id_Contact " +
                        " INNER JOIN NESTDB.dbo.Tb002_Portfolios C	ON A.Id_Portfolio = C.Id_Portfolio " +
                        " INNER JOIN NESTDB.dbo.Tb701_Transaction_Types D	ON A.Transaction_Type = D.Id_Trans_Type ";






            if (chkFromDate.Checked)
            {
                SQLString += " WHERE A.Trade_Date >= '" + cmbDateTo.Value.ToString("yyyy-MM-dd") + "' AND A.Trade_Date <= '" + cmbDateFrom.Value.ToString("yyyy-MM-dd") + "' ";
            }

            tablep = curConn.Return_DataTable(SQLString);

            dtgClientTransac.DataSource = tablep;

            tablep.Dispose();

            // Delete Button
            dgClientTransac.Columns.AddField("Delete");
            dgClientTransac.Columns["Delete"].VisibleIndex = 0;
            dgClientTransac.Columns["Delete"].Width = 60;
            RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
            item5.Buttons[0].Tag = 1;
            item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            item5.Buttons[0].Caption = "Delete";
            dtgClientTransac.RepositoryItems.Add(item5);
            dgClientTransac.Columns["Delete"].ColumnEdit = item5;
            dgClientTransac.Columns["Delete"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            dgClientTransac.OptionsBehavior.Editable = false;
            dgClientTransac.Columns["Delete"].Visible = true;

            dgClientTransac.Columns["Fin_Amount"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgClientTransac.Columns["Fin_Amount"].DisplayFormat.FormatString = "#,##0.00;(#,##0.00)";

            dgClientTransac.Columns["IncomeTax"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgClientTransac.Columns["IncomeTax"].DisplayFormat.FormatString = "n";

            dgClientTransac.Columns["Quantity"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            dgClientTransac.Columns["Quantity"].DisplayFormat.FormatString = "n";

            dgClientTransac.GroupSummary.Add(SummaryItemType.Sum, "Quantity", dgClientTransac.Columns["Quantity"], "{0:#,#0}");
            dgClientTransac.GroupSummary.Add(SummaryItemType.Sum, "Fin_Amount", dgClientTransac.Columns["Fin_Amount"], "{0:#,#0}");

            //dgSubRedemp.Columns["Payment Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //dgSubRedemp.Columns["Payment Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //dgSubRedemp.Columns["Payment Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            //dgSubRedemp.Columns["Conversion Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //dgSubRedemp.Columns["Conversion Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //dgSubRedemp.Columns["Conversion Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            //dgSubRedemp.Columns["Trade Date"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            //dgSubRedemp.Columns["Trade Date"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //dgSubRedemp.Columns["Trade Date"].DisplayFormat.FormatString = "dd/MMM/yy";

            curUtils.SetColumnStyle(dgClientTransac, 1);

        }

        private void txtCash_Leave(object sender, EventArgs e)
        {
            if (curUtils.IsNumeric(this.txtCash.Text))
            {
                decimal Cash = Convert.ToDecimal(txtCash.Text);
                this.txtCash.Text = Cash.ToString("##,##0");
            }

        }

        private void cmbTransType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate_Date();
        }

        void Validate_Date()
        {

            if (cmbTransType.SelectedValue != null && cmbFund.SelectedValue != null)
            {
                if (curUtils.IsNumeric(cmbTransType.SelectedValue.ToString()) && curUtils.IsNumeric(cmbFund.SelectedValue.ToString()))
                {
                    //grpDate.Enabled = true;
                    int Transaction_Type = Convert.ToInt32(cmbTransType.SelectedValue.ToString());

                    int Id_Fund = Convert.ToInt32(cmbFund.SelectedValue.ToString());
                    int Region;
                    if (Id_Fund == 4)
                    {
                        Region = 4;
                    }
                    else
                    {
                        Region = 1;
                    }

                    if (Transaction_Type == 30)
                    {
                        Transaction_Type_Message = "Subscription Inserted!";
                        //next day
                        dtpConversion.Value = dtpTradeDate.Value; //Convert.ToDateTime(CargaDados.DB.Execute_Query_String("Select NESTDB.dbo.FCN_NDATEADD('du',1,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "',2," + Region + ")"));
                        dtpPayment.Value = dtpTradeDate.Value;
                    }

                    if (Transaction_Type != 30)
                    {
                        Transaction_Type_Message = "Redemption Inserted!";
                        dtpConversion.Value = dtpTradeDate.Value;
                        dtpPayment.Value = Convert.ToDateTime(curConn.Execute_Query_String("Select NESTDB.dbo.FCN_NDATEADD('du',3,'" + dtpTradeDate.Value.ToString("yyyyMMdd") + "',2," + Region + ")"));
                        //3 days
                    }
                }
            }

        }

        private void cmbFund_SelectedIndexChanged(object sender, EventArgs e)
        {
            Validate_Date();
        }

        private void dgClientTransac_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Name;

            if (Column_Name == "colDelete" && (LiveDLL.NUserControl.Instance.User_Id == 53 || LiveDLL.NUserControl.Instance.User_Id == 17 || LiveDLL.NUserControl.Instance.User_Id == 49))
            {
                if (dgClientTransac.FocusedRowHandle != null)
                {
                    int curIndex = int.Parse(dgClientTransac.GetRowCellValue(dgClientTransac.FocusedRowHandle, "Transaction_Id").ToString());

                    DialogResult UserAnswer = MessageBox.Show("Do you really want to delete this entry?", "Delete Entry", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (UserAnswer == DialogResult.OK)
                    {
                        using (newNestConn curConn = new newNestConn())
                        {
                            curConn.ExecuteNonQuery("DELETE FROM NESTDB.dbo.Tb760_Subscriptions_Mellon WHERE Transaction_Id=" + curIndex.ToString() + ";");
                            Carrega_Grid();
                        }
                    }
                }
            }
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgClientTransac.SelectAll();
            dgClientTransac.CopyToClipboard();
            //  MessageBox.Show("Copied!");
        }

        private void dgClientTransac_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            GridView view = sender as GridView;

            // extract summary items
            ArrayList items = new ArrayList();
            foreach (GridSummaryItem si in view.GroupSummary)
                if (si is GridGroupSummaryItem && si.SummaryType != SummaryItemType.None)
                    items.Add(si);
            if (items.Count == 0) return;

            // draw group row without summary values
            DevExpress.XtraGrid.Drawing.GridGroupRowPainter painter;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo info;
            painter = e.Painter as DevExpress.XtraGrid.Drawing.GridGroupRowPainter;
            info = e.Info as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridGroupRowInfo;
            int level = view.GetRowLevel(e.RowHandle);
            int row = view.GetDataRowHandleByGroupRowHandle(e.RowHandle);
            info.GroupText = view.GetRowCellDisplayText(row, view.GroupedColumns[level]);
            e.Appearance.DrawBackground(e.Cache, info.Bounds);
            painter.ElementsPainter.GroupRow.DrawObject(info);

            // draw summary values aligned to columns
            Hashtable values = view.GetGroupSummaryValues(e.RowHandle);
            foreach (GridGroupSummaryItem item in items)
            {
                // obtain column rectangle
                GridColumn column = view.Columns[item.FieldName];
                System.Drawing.Rectangle rect = GetColumnBounds(column);
                if (rect.IsEmpty) continue;

                if ((rect.X + rect.Width / 2) > view.Columns[4].Width)
                {
                    // calculate summary text and boundaries
                    string text = item.GetDisplayText(values[item], false);
                    SizeF sz = e.Appearance.CalcTextSize(e.Cache, text, rect.Width);
                    int width = Convert.ToInt32(sz.Width) + 1;
                    rect.X += rect.Width - width - 2;
                    rect.Width = width;
                    rect.Y = e.Bounds.Y;
                    rect.Height = e.Bounds.Height - 2;

                    // draw a summary values
                    e.Appearance.DrawString(e.Cache, text, rect);
                }
            }

            // disable default painting of the group row
            e.Handled = true;
        }

        private System.Drawing.Rectangle GetColumnBounds(GridColumn column)
        {
            GridViewInfo gridInfo = column.View.GetViewInfo() as GridViewInfo;
            GridColumnInfoArgs colInfo = gridInfo.ColumnsInfo[column];
            if (colInfo != null)
                return colInfo.Bounds;
            else
                return System.Drawing.Rectangle.Empty;
        }

        private void chkFromDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFromDate.Checked)
            {
                cmbDateTo.Enabled = true; cmbDateTo.Visible = true;
                btnLoad.Enabled = true; btnLoad.Visible = true;
                label99.Enabled = true; label99.Visible = true;
                cmbDateFrom.Enabled = true; cmbDateFrom.Visible = true;
            }
            else
            {
                cmbDateTo.Enabled = false; cmbDateTo.Visible = false;
                btnLoad.Enabled = false; btnLoad.Visible = false;
                label99.Enabled = false; label99.Visible = false;
                cmbDateFrom.Enabled = false; cmbDateFrom.Visible = false;

                Carrega_Grid();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Load_Combos();

            Carrega_Grid();
        }

        private void cmbDateTo_ValueChanged(object sender, EventArgs e)
        {
            if (cmbDateTo.Value > cmbDateFrom.Value)
            {
                DateTime a = cmbDateTo.Value;
                cmbDateTo.Value = cmbDateFrom.Value;
                cmbDateFrom.Value = a;
            }
        }

        private void cmbDateFrom_ValueChanged(object sender, EventArgs e)
        {
            if (cmbDateTo.Value > cmbDateFrom.Value)
            {
                DateTime a = cmbDateTo.Value;
                cmbDateTo.Value = cmbDateFrom.Value;
                cmbDateFrom.Value = a;
            }
        }

        private void lblImportFile_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            e.Effect = DragDropEffects.None;

            foreach (string curFile in files)
            {
                if (curFile.Contains(".xls"))
                {
                    DialogResult UserAnswer = MessageBox.Show("Import file: " + curFile.Substring(curFile.LastIndexOf('\\') + 1) + "?", "Import NestPrev File", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (UserAnswer == DialogResult.OK)
                    {
                        NestPrevImporter curImporter = new NestPrevImporter(curFile);
                        curImporter.Import();
                        MessageBox.Show("Done", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Carrega_Grid();
                    }
                }
            }
        }

        private void lblImportFile_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;
        }

        private void lblImportFile_Click(object sender, System.EventArgs e)
        {

        }
    }

    public class NestPrevImporter : IDisposable
    {
        ~NestPrevImporter()
        {
            Dispose();
        }

        private string curFileName;
        private string curFilePathAndName;
        private OleDbConnection shtConn = new OleDbConnection();
        private newNestConn curNestConn = new newNestConn();
        private Microsoft.Office.Interop.Excel.Application ExcelObject;
        private Workbook NestPrevWorkbook;

        private Dictionary<string, Contact> ContactList;
        private List<Transaction> DuplicatedTransactionList;

        public NestPrevImporter(string curFilePathAndName)
        {
            ContactList = new Dictionary<string, Contact>();

            System.Data.DataTable curTable = curNestConn.Return_DataTable("SELECT * FROM NESTDB.dbo.Tb751_Contacts A INNER JOIN NESTDB.dbo.Tb751_Contacts_Proposta B ON A.Id_Contact = B.IdContact");

            List<string> Duplicados = new List<string>();

            if (curTable.Rows.Count > 0)
            {
                foreach (DataRow curRow in curTable.Rows)
                {
                    if (curRow["NumProposta"].ToString() != "")
                    {
                        Contact curContact = new Contact();
                        curContact.IdContact = (int)curRow["Id_Contact"];
                        curContact.ContactName = curRow["Contact_Name"].ToString();
                        curContact.NumProposta = curRow["NumProposta"].ToString();
                        curContact.CPF = curRow["CPF"].ToString();

                        if (!ContactList.ContainsKey(curContact.NumProposta)) { ContactList.Add(curContact.NumProposta, curContact); }
                        else { Duplicados.Add(curContact.NumProposta); }
                    }
                }
            }

            if (Duplicados.Count > 0)
            {
                string Message = "Duplicated Contacts - Contract Number: \r\n ";
                foreach (string dupli in Duplicados) { Message += dupli + " \r\n "; }
                MessageBox.Show(Message);
            }

            this.curFileName = curFilePathAndName.Substring(curFilePathAndName.LastIndexOf('\\') + 1);
            this.curFilePathAndName = curFilePathAndName;
        }

        public void Import()
        {
            try
            {
                shtConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + curFilePathAndName + "';Extended Properties= 'Excel 8.0;HDR=Yes;IMEX=1'");
                shtConn.Open();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("is already opened"))
                {
                    MessageBox.Show("Impossivel importar a planilha no modo Somente Leitura", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    MessageBox.Show("Impossivel importar a planilha \r\n " + e.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }
            ExcelObject = new Microsoft.Office.Interop.Excel.Application();
            NestPrevWorkbook = ExcelObject.Workbooks.Open(curFilePathAndName, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true);

            foreach (Worksheet curWorksheet in NestPrevWorkbook.Worksheets)
            {
                switch (curWorksheet.Name)
                {
                    case "Saldo por cliente":
                        ImportSaldo(curWorksheet);
                        break;
                    case "Contribuições Mensais":
                        ImportContriMensais(curWorksheet);
                        break;
                    case "Contribuições Esporádicas":
                        ImportContriEsporadicas(curWorksheet);
                        break;
                    case "Portabilidades":
                        ImportPortabilidades(curWorksheet);
                        break;
                    case "Resgates":
                        ImportResgates(curWorksheet);
                        break;
                    default:
                        break;
                }
            }

            shtConn = null;
            ExcelObject = null;
            NestPrevWorkbook = null;
        }

        public void ImportSaldo(Worksheet curWorksheet) //[Linha, Coluna]
        {
            System.Data.DataTable ReturnTable = Table(curWorksheet.Name);

            DateTime BalanceDate;

            if (!DateTime.TryParse(curWorksheet.Cells[2, 6].Value2.ToString().Replace("Data: ", ""), out BalanceDate))
            {
                MessageBox.Show("Erro ao importar saldo. Checar a data na aba Saldo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (BalanceDate.DayOfWeek == DayOfWeek.Sunday) BalanceDate = BalanceDate.AddDays(-2);
            if (BalanceDate.DayOfWeek == DayOfWeek.Saturday) BalanceDate = BalanceDate.AddDays(-1);

            int RowCount = int.MaxValue;

            for (int Row = 5; Row < RowCount; Row++)
            {
                DataRow curRow = ReturnTable.NewRow();

                for (int Column = 0; Column < 9; Column++)
                {
                    if (curWorksheet.Cells[Row, Column + 1].Value2 != null && curWorksheet.Cells[Row, Column + 1].Value2.ToString() != "")
                    {
                        curRow[Column] = curWorksheet.Cells[Row, Column + 1].Value2.ToString();
                    }
                    else
                    {
                        RowCount = Row;
                        curRow = null;
                        break;
                    }
                }
                if (curRow != null) { ReturnTable.Rows.Add(curRow); }
            }

            if (ReturnTable.Rows.Count > 0)
            {
                DuplicatedTransactionList = new List<Transaction>();
                ImportContacts(ReturnTable);

                foreach (DataRow curRow in ReturnTable.Rows)
                {
                    Transaction curTransaction = new Transaction("Saldo");

                    lock (ContactList)
                    {
                        if (!ContactList.TryGetValue(curRow["Num Proposta"].ToString(), out curTransaction.Contact))
                        {
                            MessageBox.Show("Cliente " + curRow["Nome Participante"].ToString() + " sem número de proposta cadastrado na base.");
                            continue;
                        }
                    }
                    curTransaction.TradeDate = BalanceDate;
                    curTransaction.Quantity = double.Parse(curRow["Qtd Cota"].ToString());
                    curTransaction.FinAmount = double.Parse(curRow["Val Saldo Reserva Reais"].ToString());
                    curTransaction.Description = curRow["Descrição Plano"].ToString();
                    curTransaction.Distributor = curRow["Nome do Corretor"].ToString();
                    curTransaction.PaymentType = curRow["Forma de Pagamento"].ToString();

                    switch (curTransaction.Distributor)
                    {
                        case "VALOR":
                        case "VALORES":
                            {
                                if (curNestConn.Return_Int("select count (*) from Tb752_DistContacts where Id_Contact = " + curTransaction.Contact.IdContact + " AND Id_Distributor <> 521") > 0)
                                {
                                    MessageBox.Show("Impossivel importar cliente " + curTransaction.Contact.ContactName + "\n\rCliente com distribuidor já cadastrado na base", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (curNestConn.Return_Int("select count (*) from Tb752_DistContacts where Id_Contact = " + curTransaction.Contact.IdContact + " AND Id_Distributor = 521") == 0)
                                {
                                    curNestConn.ExecuteNonQuery("INSERT Tb752_DistContacts (FromDate,Id_Distributor,Id_Contact) SELECT '1900-01-01',521, " + curTransaction.Contact.IdContact);
                                } //521 id_Distributor da Valor
                                break;
                            }
                        case "FUTURA INVEST":
                            {
                                if (curNestConn.Return_Int("select count (*) from Tb752_DistContacts where Id_Contact = " + curTransaction.Contact.IdContact + " AND Id_Distributor <> 504") > 0)
                                {
                                    MessageBox.Show("Impossivel importar cliente " + curTransaction.Contact.ContactName + "\n\rCliente com distribuidor já cadastrado na base", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else if (curNestConn.Return_Int("select count (*) from Tb752_DistContacts where Id_Contact = " + curTransaction.Contact.IdContact + " AND Id_Distributor = 504") == 0)
                                {
                                    curNestConn.ExecuteNonQuery("INSERT Tb752_DistContacts (FromDate,Id_Distributor,Id_Contact) SELECT '1900-01-01',504, " + curTransaction.Contact.IdContact);
                                } //504 id_Distributor da Futura                                
                                break;
                            }
                        default:
                            break;
                    }

                    if (!curTransaction.CheckExist()) { curTransaction.Insert(); }
                    else { DuplicatedTransactionList.Add(curTransaction); }
                }

                if (DuplicatedTransactionList.Count > 0)
                {
                    string str = "";

                    foreach (Transaction trans in DuplicatedTransactionList)
                    {
                        str += "\r\n" + trans.Contact.ContactName;
                    }

                    DialogResult UserAnswer = MessageBox.Show("Usuarios abaixo com saldo já existente na base, deseja atualiza-los?" + str, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (UserAnswer == DialogResult.OK)
                    {
                        foreach (Transaction curTransaction in DuplicatedTransactionList)
                        {
                            curTransaction.Delete();
                            curTransaction.Insert();
                        }
                    }
                }
                DuplicatedTransactionList.Clear();
            }

        }

        public void ImportContriMensais(Worksheet curWorksheet)
        {
            System.Data.DataTable ReturnTable = Table(curWorksheet.Name);

            int RowCount = int.MaxValue;

            for (int Row = 5; Row < RowCount; Row++)
            {
                DataRow curRow = ReturnTable.NewRow();

                for (int Column = 0; Column < 7; Column++)
                {
                    if (curWorksheet.Cells[Row, Column + 1].Value2 != null && curWorksheet.Cells[Row, Column + 1].Value2.ToString() != "")
                    {
                        if (Column == 5) { curRow[Column] = curWorksheet.Cells[Row, Column + 1].Value.ToString(); }
                        else { curRow[Column] = curWorksheet.Cells[Row, Column + 1].Value2.ToString(); }
                    }
                    else
                    {
                        RowCount = Row;
                        curRow = null;
                        break;
                    }
                }
                if (curRow != null) { ReturnTable.Rows.Add(curRow); }
            }

            if (ReturnTable.Rows.Count > 0)
            {
                DuplicatedTransactionList = new List<Transaction>();
                foreach (DataRow curRow in ReturnTable.Rows)
                {
                    Transaction curTransaction = new Transaction("ContriMensais");
                    lock (ContactList)
                    {
                        if (!ContactList.TryGetValue(curRow["Num Proposta"].ToString(), out curTransaction.Contact))
                        {
                            MessageBox.Show("Impossivel importar Contribuição Mensal do cliente " + curRow["Nome Participante"].ToString() + ". Sem número de proposta cadastrado na base.");
                            continue;
                        }
                    }

                    curTransaction.TradeDate = DateTime.Parse(curRow["Data Movimento"].ToString());

                    if (curTransaction.TransactionNAV == 0 || double.IsNaN(curTransaction.TransactionNAV))
                    {
                        MessageBox.Show("Impossivel importar Contribuição Mensal do cliente " + curRow["Nome Participante"].ToString() + ". Fundo com NAV = 0 no dia " + curTransaction.TradeDate.ToString("dd-MM-yyyy"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    curTransaction.PaymentType = curRow["Forma de Pagamento"].ToString();
                    curTransaction.FinAmount = double.Parse(curRow["Val Contribuição"].ToString());
                    curTransaction.Quantity = curTransaction.FinAmount / curTransaction.TransactionNAV;

                    if (!curTransaction.CheckExist()) { curTransaction.Insert(); }
                    else { DuplicatedTransactionList.Add(curTransaction); }
                }

                if (DuplicatedTransactionList.Count > 0)
                {
                    string str = "";

                    foreach (Transaction curTransaction in DuplicatedTransactionList)
                    {
                        str = "\r\n Nome: " + curTransaction.Contact.ContactName;
                        str += "\r\n Data: " + curTransaction.TradeDate.ToString("dd-MM-yyyy");
                        str += "\r\n Valor: " + curTransaction.FinAmount;

                        DialogResult UserAnswer = MessageBox.Show("Contribuição mensal abaixo já existente na base, deseja importa-la? \r\n" + str, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        if (UserAnswer == DialogResult.OK)
                        {
                            curTransaction.Insert();
                        }
                    }
                }

                DuplicatedTransactionList.Clear();
            }
        }
        public void ImportResgates(Worksheet curWorksheet)
        {
            System.Data.DataTable ReturnTable = Table(curWorksheet.Name);

            int RowCount = int.MaxValue;

            for (int Row = 5; Row < RowCount; Row++)
            {
                DataRow curRow = ReturnTable.NewRow();

                for (int Column = 0; Column < 7; Column++)
                {
                    if (curWorksheet.Cells[Row, Column + 1].Value2 != null && curWorksheet.Cells[Row, Column + 1].Value2.ToString() != "")
                    {
                        if (Column == 4) { curRow[Column] = curWorksheet.Cells[Row, Column + 1].Value.ToString(); }
                        else { curRow[Column] = curWorksheet.Cells[Row, Column + 1].Value2.ToString(); }
                    }
                    else
                    {
                        RowCount = Row;
                        curRow = null;
                        break;
                    }
                }
                if (curRow != null) { ReturnTable.Rows.Add(curRow); }
            }

            if (ReturnTable.Rows.Count > 0)
            {
                DuplicatedTransactionList = new List<Transaction>();
                foreach (DataRow curRow in ReturnTable.Rows)
                {
                    Transaction curTransaction = new Transaction("Resgate");
                    lock (ContactList)
                    {
                        if (!ContactList.TryGetValue(curRow["Num Proposta"].ToString(), out curTransaction.Contact))
                        {
                            MessageBox.Show("Impossivel importar Resgate do cliente " + curRow["Nome Participante"].ToString() + ". Sem número de proposta cadastrado na base.");
                            continue;
                        }
                    }

                    curTransaction.TradeDate = DateTime.Parse(curRow["Data Movimento"].ToString());

                    if (curTransaction.TransactionNAV == 0 || double.IsNaN(curTransaction.TransactionNAV))
                    {
                        MessageBox.Show("Impossivel importar Resgate do cliente " + curRow["Nome Participante"].ToString() + ". Fundo com NAV = 0 no dia " + curTransaction.TradeDate.ToString("dd-MM-yyyy"), "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        continue;
                    }
                    //curTransaction.PaymentType = curRow["Forma de Pagamento"].ToString();
                    curTransaction.FinAmount = double.Parse(curRow["Valor Resgate"].ToString());
                    curTransaction.Quantity = curTransaction.FinAmount / curTransaction.TransactionNAV;

                    if (!curTransaction.CheckExist()) { curTransaction.Insert(); }
                    else { DuplicatedTransactionList.Add(curTransaction); }
                }

                if (DuplicatedTransactionList.Count > 0)
                {
                    string str = "";

                    foreach (Transaction curTransaction in DuplicatedTransactionList)
                    {
                        str = "\r\n Nome: " + curTransaction.Contact.ContactName;
                        str += "\r\n Data: " + curTransaction.TradeDate.ToString("dd-MM-yyyy");
                        str += "\r\n Valor: " + curTransaction.FinAmount;

                        DialogResult UserAnswer = MessageBox.Show("Resgate abaixo já existente na base, deseja importa-la? \r\n" + str, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        if (UserAnswer == DialogResult.OK)
                        {
                            curTransaction.Insert();
                        }
                    }
                }

                DuplicatedTransactionList.Clear();
            }
        }
        public void ImportContriEsporadicas(Worksheet curWorksheet)
        {
            System.Data.DataTable ReturnTable = Table(curWorksheet.Name);

            int RowCount = int.MaxValue;

            for (int Row = 5; Row < RowCount; Row++)
            {
                DataRow curRow = ReturnTable.NewRow();

                for (int Column = 0; Column < 8; Column++)
                {
                    if (curWorksheet.Cells[Row, Column + 1].Value2 != null && curWorksheet.Cells[Row, Column + 1].Value2.ToString() != "")
                    {

                        if (Column == 6) { curRow[Column] = curWorksheet.Cells[Row, Column + 1].Value.ToString(); }
                        else { curRow[Column] = curWorksheet.Cells[Row, Column + 1].Value2.ToString(); }
                    }
                    else
                    {
                        RowCount = Row;
                        curRow = null;
                        break;
                    }
                }
                if (curRow != null) { ReturnTable.Rows.Add(curRow); }
            }

            if (ReturnTable.Rows.Count > 0)
            {
                DuplicatedTransactionList = new List<Transaction>();
                foreach (DataRow curRow in ReturnTable.Rows)
                {
                    Transaction curTransaction = new Transaction("ContriEsporadicas");
                    lock (ContactList)
                    {
                        if (!ContactList.TryGetValue(curRow["Num Proposta"].ToString(), out curTransaction.Contact))
                        {
                            MessageBox.Show("Impossivel importar Aplicação do cliente " + curRow["Nome Participante"].ToString() + ". Sem número de proposta cadastrado na base.");
                            continue;
                        }
                    }
                    curTransaction.PaymentType = curRow["Forma de Pagamento"].ToString();
                    curTransaction.TradeDate = DateTime.Parse(curRow["Data Movimento"].ToString());

                    curTransaction.FinAmount = double.Parse(curRow["Val Contribuição"].ToString());
                    curTransaction.Quantity = curTransaction.FinAmount / curTransaction.TransactionNAV;

                    if (!curTransaction.CheckExist()) { curTransaction.Insert(); }
                    else { DuplicatedTransactionList.Add(curTransaction); }
                }

                if (DuplicatedTransactionList.Count > 0)
                {
                    string str = "";

                    foreach (Transaction curTransaction in DuplicatedTransactionList)
                    {
                        str = "\r\n Nome: " + curTransaction.Contact.ContactName;
                        str += "\r\n Data: " + curTransaction.TradeDate.ToString("dd-MM-yyyy");
                        str += "\r\n Valor: " + curTransaction.FinAmount;

                        DialogResult UserAnswer = MessageBox.Show("Contribuição esporádica abaixo já existente na base, deseja importa-la? \r\n" + str, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        if (UserAnswer == DialogResult.OK)
                        {
                            curTransaction.Insert();
                        }
                    }
                }

                DuplicatedTransactionList.Clear();
            }


        }
        public void ImportPortabilidades(Worksheet curWorksheet)
        {
            System.Data.DataTable ReturnTable = Table(curWorksheet.Name);

            int RowCount = int.MaxValue;

            for (int Row = 5; Row < RowCount; Row++)
            {
                DataRow curRow = ReturnTable.NewRow();

                for (int Column = 0; Column < 7; Column++)
                {
                    if (curWorksheet.Cells[Row, Column + 1].Value2 != null && curWorksheet.Cells[Row, Column + 1].Value2.ToString() != "")
                    {
                        if (Column == 6) { curRow[Column] = curWorksheet.Cells[Row, Column + 1].Value.ToString(); }
                        else { curRow[Column] = curWorksheet.Cells[Row, Column + 1].Value2.ToString(); }
                    }
                    else
                    {
                        RowCount = Row;
                        curRow = null;
                        break;
                    }
                }
                if (curRow != null) { ReturnTable.Rows.Add(curRow); }
            }

            if (ReturnTable.Rows.Count > 0)
            {
                DuplicatedTransactionList = new List<Transaction>();
                foreach (DataRow curRow in ReturnTable.Rows)
                {
                    Transaction curTransaction = new Transaction("Portabilidade");
                    lock (ContactList)
                    {
                        if (!ContactList.TryGetValue(curRow["Num Proposta"].ToString(), out curTransaction.Contact))
                        {
                            MessageBox.Show("Impossivel importar portabilidade do cliente " + curRow["Nome Participante"].ToString() + ". Sem número de proposta cadastrado na base.");
                            continue;
                        }
                    }
                    curTransaction.TradeDate = DateTime.Parse(curRow["Data Movimento"].ToString());

                    curTransaction.FinAmount = double.Parse(curRow["Valor do Movimento"].ToString());
                    curTransaction.Quantity = curTransaction.FinAmount / curTransaction.TransactionNAV;

                    if (!curTransaction.CheckExist()) { curTransaction.Insert(); }
                    else { DuplicatedTransactionList.Add(curTransaction); }
                }

                if (DuplicatedTransactionList.Count > 0)
                {
                    string str = "";

                    foreach (Transaction curTransaction in DuplicatedTransactionList)
                    {
                        str = "\r\n Nome: " + curTransaction.Contact.ContactName;
                        str += "\r\n Data: " + curTransaction.TradeDate.ToString("dd-MM-yyyy");
                        str += "\r\n Valor: " + curTransaction.FinAmount;

                        DialogResult UserAnswer = MessageBox.Show("Portabilidade abaixo já existente na base, deseja importa-la? \r\n" + str, "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        if (UserAnswer == DialogResult.OK)
                        {
                            curTransaction.Insert();
                        }
                    }
                }

                DuplicatedTransactionList.Clear();
            }



        }
        public void ImportContacts(System.Data.DataTable curTable)
        {
            if (curTable.Rows.Count > 0)
            {
                List<string> Inserteds = new List<string>();
                foreach (DataRow curRow in curTable.Rows)
                {
                    Contact curContact = new Contact();
                    curContact.ContactName = curRow["Nome Participante"].ToString();
                    curContact.NumProposta = curRow["Num Proposta"].ToString();
                    curContact.CPF = curRow["CPF"].ToString();

                    if (!ContactList.ContainsKey(curContact.NumProposta))
                    {
                        if (curContact.Exist())
                        {
                            if (curContact.CheckLink())
                            {
                                ContactList.Add(curContact.NumProposta, curContact);
                            }
                            else
                            {
                                curContact.Link();
                                ContactList.Add(curContact.NumProposta, curContact);
                            }
                        }

                        else
                        {
                            curContact.Insert();
                            curContact.Link();
                            ContactList.Add(curContact.NumProposta, curContact);
                            Inserteds.Add(curContact.ContactName);
                        }
                    }
                }

                if (Inserteds.Count > 0)
                {
                    string Message = "Inserted Contacts: \r\n";

                    foreach (string ins in Inserteds)
                    {
                        Message += ins + " \n\r ";
                    }
                    MessageBox.Show(Message);
                }
            }
        }

        public void Dispose()
        {
            shtConn = null;
            curNestConn = null;
        }

        public System.Data.DataTable Table(string Name)
        {
            System.Data.DataTable curTable = new System.Data.DataTable();

            switch (Name)
            {
                case "Saldo por cliente":
                    curTable.Columns.Add("Nome Participante", typeof(string));
                    curTable.Columns.Add("CPF", typeof(string));
                    curTable.Columns.Add("Num Proposta", typeof(string));
                    curTable.Columns.Add("Valor Cota", typeof(string));
                    curTable.Columns.Add("Qtd Cota", typeof(string));
                    curTable.Columns.Add("Val Saldo Reserva Reais", typeof(string));
                    curTable.Columns.Add("Descrição Plano", typeof(string));
                    curTable.Columns.Add("Nome do Corretor", typeof(string));
                    curTable.Columns.Add("Forma de Pagamento", typeof(string));
                    break;
                case "Contribuições Mensais":
                    curTable.Columns.Add("Nome Participante", typeof(string));
                    curTable.Columns.Add("CPF", typeof(string));
                    curTable.Columns.Add("Num Proposta", typeof(string));
                    curTable.Columns.Add("Descrição Tipo Produto", typeof(string));
                    curTable.Columns.Add("Val Contribuição", typeof(string));
                    curTable.Columns.Add("Data Movimento", typeof(string));
                    curTable.Columns.Add("Forma de Pagamento", typeof(string));
                    break;
                case "Contribuições Esporádicas":
                    curTable.Columns.Add("Nome Participante", typeof(string));
                    curTable.Columns.Add("CPF", typeof(string));
                    curTable.Columns.Add("Num Proposta", typeof(string));
                    curTable.Columns.Add("Descrição Tipo Produto", typeof(string));
                    curTable.Columns.Add("Valor Cota", typeof(string));
                    curTable.Columns.Add("Val Contribuição", typeof(string));
                    curTable.Columns.Add("Data Movimento", typeof(string));
                    curTable.Columns.Add("Forma de Pagamento", typeof(string));
                    break;
                case "Portabilidades":
                    curTable.Columns.Add("Nome Participante", typeof(string));
                    curTable.Columns.Add("CPF", typeof(string));
                    curTable.Columns.Add("Num Proposta", typeof(string));
                    curTable.Columns.Add("Descrição Tipo Produto", typeof(string));
                    curTable.Columns.Add("Valor Cota", typeof(string));
                    curTable.Columns.Add("Valor do Movimento", typeof(string));
                    curTable.Columns.Add("Data Movimento", typeof(string));
                    break;
                case "Resgates":
                    curTable.Columns.Add("Nome Participante", typeof(string));
                    curTable.Columns.Add("CPF", typeof(string));
                    curTable.Columns.Add("Num Proposta", typeof(string));
                    curTable.Columns.Add("Descrição Plano", typeof(string));
                    curTable.Columns.Add("Data Movimento", typeof(string));
                    curTable.Columns.Add("Valor Resgate", typeof(string));
                    curTable.Columns.Add("Nome do Corretor", typeof(string));
                    break;
                default:
                    break;
            }
            return curTable;
        }
    }

    public class Contact
    {
        private newNestConn curNestConn = new newNestConn();

        public int IdContact = 0;
        public string ContactName = "";
        public string NumProposta = "";
        public string CPF = "";

        public bool Exist()
        {
            IdContact = curNestConn.Return_Int("SELECT Id_Contact from NESTDB.dbo.Tb751_Contacts WHERE Contact_Name = '" + this.ContactName + "'");
            if (IdContact != 0) { return true; }

            return false;
        }

        public bool CheckLink()
        {
            if (curNestConn.Return_Int("SELECT COUNT (*) FROM Tb751_Contacts_Proposta WHERE IdContact = " + this.IdContact + " AND NumProposta = '" + this.NumProposta + "'") >= 1)
            { return true; }

            return false;
        }

        public void Link()
        {
            curNestConn.ExecuteNonQuery("INSERT Tb751_Contacts_Proposta SELECT " + this.IdContact + ",'" + this.NumProposta + "','" + this.CPF + "'");
        }

        public void Update()
        {
            curNestConn.ExecuteNonQuery("UPDATE NESTDB.dbo.Tb751_Contacts SET NumProposta = '" + this.NumProposta + "', CPF = '" + this.CPF + "' WHERE Id_Contact = " + this.IdContact);
        }

        public void Insert()
        {
            this.IdContact = curNestConn.Return_Int("INSERT Tb751_Contacts SELECT '" + this.ContactName + "',NULL,NULL,0");
        }
    }

    public class Transaction
    {
        public Transaction(string TransactionType)
        {
            this.TransactionType = TransactionType;
        }

        private newNestConn curNestConn = new newNestConn();
        public Contact Contact = new Contact();
        public string TransactionType = "";
        public string PaymentType = "";
        private int IdTransactionType
        {
            get
            {
                // Retorna o Id da transação que está na NESTDB.dbo.Tb701_Transaction_Types
                switch (this.TransactionType)
                {
                    case "ContriMensais":
                        if (PaymentType == "DC") { return 34; } // Monthly Subscription
                        else { return 36; } // Monthly Subscription (Direct Debit)
                    case "ContriEsporadicas":
                        if (PaymentType == "DC") { return 30; } // Subscription
                        else { return 37; } //Subscription (Direct Debit)
                    case "Portabilidade":
                        return 35; // Portability
                    case "Resgate":
                        return 31; // Partial Redemption
                    default:
                        return 0;
                }
            }
        }
        public string Description = "";
        public string Distributor = "";
        public string Identifier = "";
        public DateTime TradeDate;
        private double _TransactionNAV = 0;

        public double TransactionNAV
        {
            get
            {
                if (_TransactionNAV > 0) { return _TransactionNAV; }
                else { _TransactionNAV = curNestConn.Return_Double("SELECT TOP 1 SrValue FROM NESTDB.dbo.Tb056_Precos_Fundos WHERE IdSecurity = 675151 AND SrDate < '" + this.TradeDate.ToString("yyyy-MM-dd") + "' AND SrType = 1 ORDER BY SrDate DESC, Source DESC"); return TransactionNAV; }
            }
            set
            {
                _TransactionNAV = value;
            }
        }
        public double Quantity = 0;
        public double FinAmount = 0;

        public bool CheckExist()
        {
            string SQLString = "";

            switch (TransactionType)
            {
                case "Saldo":
                    SQLString = "SELECT IdContactBalance FROM NESTDB.dbo.Tb757_Contacts_Balance_NestPrev WHERE NumProposta = '" + this.Contact.NumProposta + "' AND IdContact = " + this.Contact.IdContact + " AND Date = '" + TradeDate.ToString("yyyy-MM-dd") + "'";
                    break;
                case "ContriMensais":
                    SQLString =
                                "SELECT Transaction_Id " +
                                "FROM NESTDB.dbo.Tb760_Subscriptions_Mellon " +
                                "WHERE Id_Contact = " + this.Contact.IdContact + " AND " +
                                "	  Trade_Date = '" + this.TradeDate.ToString("yyyy-MM-dd") + "' AND  " +
                                "	  Transaction_Type = 34 AND " +
                                "	  Id_Portfolio = 50 AND " +
                                "	  AdminRef = " + this.Contact.NumProposta + " AND " +
                                "	  Fin_Amount = " + this.FinAmount.ToString().Replace(",", ".");
                    break;
                case "ContriEsporadicas":
                    SQLString =
                                "SELECT Transaction_Id " +
                                "FROM NESTDB.dbo.Tb760_Subscriptions_Mellon " +
                                "WHERE Id_Contact = " + this.Contact.IdContact + " AND " +
                                "	  Trade_Date = '" + this.TradeDate.ToString("yyyy-MM-dd") + "' AND  " +
                                "	  Transaction_Type = 30 AND " +
                                "	  Id_Portfolio = 50 AND " +
                                "	  AdminRef = " + this.Contact.NumProposta + " AND " +
                                "	  Fin_Amount = " + this.FinAmount.ToString().Replace(",", ".");
                    break;
                case "Portabilidade":
                    SQLString =
                               "SELECT Transaction_Id " +
                               "FROM NESTDB.dbo.Tb760_Subscriptions_Mellon " +
                               "WHERE Id_Contact = " + this.Contact.IdContact + " AND " +
                               "	  Trade_Date = '" + this.TradeDate.ToString("yyyy-MM-dd") + "' AND  " +
                               "	  Transaction_Type = 35 AND " +
                               "	  Id_Portfolio = 50 AND " +
                               "	  AdminRef = " + this.Contact.NumProposta + " AND " +
                               "	  Fin_Amount = " + this.FinAmount.ToString().Replace(",", ".");
                    break;
                case "Resgate":
                    SQLString = 
                               "SELECT Transaction_Id " +
                               "FROM NESTDB.dbo.Tb760_Subscriptions_Mellon " +
                               "WHERE Id_Contact = " + this.Contact.IdContact + " AND " +
                               "	  Trade_Date = '" + this.TradeDate.ToString("yyyy-MM-dd") + "' AND  " +
                               "	  Transaction_Type = 31 AND " +
                               "	  Id_Portfolio = 50 AND " +
                               "	  AdminRef = " + this.Contact.NumProposta + " AND " +
                               "	  Fin_Amount = " + this.FinAmount.ToString().Replace(",", ".");
                    break;
                default:
                    break;
            }

            string Id = curNestConn.Return_String(SQLString);

            if (Id != "")
            {
                Identifier = Id;
                return true;
            }
            else
                return false;
        }

        public void Delete()
        {
            string SQLString = "";

            switch (TransactionType)
            {
                case "Saldo":
                    SQLString = "DELETE FROM NESTDB.dbo.Tb757_Contacts_Balance_NestPrev where IdContact = " + this.Contact.IdContact + " and Date = '" + this.TradeDate.ToString("yyyy-MM-dd") + "' AND FinAmount = " + this.FinAmount.ToString().Replace(",", ".");
                    break;
                default:
                    break;
            }

            curNestConn.ExecuteNonQuery(SQLString);
        }

        public void Insert()
        {
            string SQLString = "";

            switch (TransactionType)
            {
                case "Saldo":
                    SQLString = " INSERT NESTDB.dbo.Tb757_Contacts_Balance_NestPrev " +
                                " (IdContact,NumProposta,CPF,TransactionNAV,Quantity,FinAmount,Description,Distributor,PaymentType,Date,InsertDate) " +
                                " SELECT  " + this.Contact.IdContact + " , " +
                                "        '" + this.Contact.NumProposta + "' ," +
                                "        '" + this.Contact.CPF + "' ," +
                                "        " + this.TransactionNAV.ToString().Replace(",", ".") + ", " +
                                "        " + this.Quantity.ToString().Replace(",", ".") + ", " +
                                "        " + this.FinAmount.ToString().Replace(",", ".") + ", " +
                                "        '" + this.Description + "' , " +
                                "        ' " + this.Distributor + "' , " +
                                "        '" + this.PaymentType + "' , " +
                                "        '" + this.TradeDate.ToString("yyyy-MM-dd") + "' , " +
                                "        GETDATE(); SELECT @@Identity ";
                    break;
                case "ContriMensais":
                    SQLString = " INSERT NESTDB.dbo.Tb760_Subscriptions_Mellon " +
                                " (Id_Contact,Request_Date,Trade_Date,Settlement_Date,Transaction_Type,Transaction_NAV,Id_Portfolio,AdminRef,AdminRef_Sub,Quantity,Fin_Amount,IncomeTax,Create_Timestamp,ClTransfer) " +
                                " SELECT " + this.Contact.IdContact + ", " +
                                "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                                "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                                "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                                "	    " + this.IdTransactionType + ", " +
                                " 	    " + this.TransactionNAV.ToString().Replace(",", ".") + ", " +
                                "	    50, " +
                                "	    " + this.Contact.NumProposta + ", " +
                                "	    NULL, " +
                                "	    " + this.Quantity.ToString().Replace(",", ".") + ", " +
                                "	    " + this.FinAmount.ToString().Replace(",", ".") + ", " +
                                "	    0, " +
                                "	    NULL, " +
                                "	    0; SELECT @@Identity";
                    break;
                case "ContriEsporadicas":
                    SQLString = " INSERT NESTDB.dbo.Tb760_Subscriptions_Mellon " +
                               " (Id_Contact,Request_Date,Trade_Date,Settlement_Date,Transaction_Type,Transaction_NAV,Id_Portfolio,AdminRef,AdminRef_Sub,Quantity,Fin_Amount,IncomeTax,Create_Timestamp,ClTransfer) " +
                               " SELECT " + this.Contact.IdContact + ", " +
                               "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                               "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                               "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                               "	    " + this.IdTransactionType + ", " +
                               " 	    " + this.TransactionNAV.ToString().Replace(",", ".") + ", " +
                               "	    50, " +
                               "	    " + this.Contact.NumProposta + ", " +
                               "	    NULL, " +
                               "	    " + this.Quantity.ToString().Replace(",", ".") + ", " +
                               "	    " + this.FinAmount.ToString().Replace(",", ".") + ", " +
                               "	    0, " +
                               "	    NULL, " +
                               "	    0; SELECT @@Identity";
                    break;
                case "Portabilidade":
                    SQLString = " INSERT NESTDB.dbo.Tb760_Subscriptions_Mellon " +
                                " (Id_Contact,Request_Date,Trade_Date,Settlement_Date,Transaction_Type,Transaction_NAV,Id_Portfolio,AdminRef,AdminRef_Sub,Quantity,Fin_Amount,IncomeTax,Create_Timestamp,ClTransfer) " +
                                " SELECT " + this.Contact.IdContact + ", " +
                                "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                                "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                                "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                                "	    " + this.IdTransactionType + ", " +
                                " 	    " + this.TransactionNAV.ToString().Replace(",", ".") + ", " +
                                "	    50, " +
                                "	    " + this.Contact.NumProposta + ", " +
                                "	    NULL, " +
                                "	    " + this.Quantity.ToString().Replace(",", ".") + ", " +
                                "	    " + this.FinAmount.ToString().Replace(",", ".") + ", " +
                                "	    0, " +
                                "	    NULL, " +
                                "	    0; SELECT @@Identity";
                    break;
                case "Resgate":
                  SQLString=  " INSERT NESTDB.dbo.Tb760_Subscriptions_Mellon " +
                                " (Id_Contact,Request_Date,Trade_Date,Settlement_Date,Transaction_Type,Transaction_NAV,Id_Portfolio,AdminRef,AdminRef_Sub,Quantity,Fin_Amount,IncomeTax,Create_Timestamp,ClTransfer) " +
                                " SELECT " + this.Contact.IdContact + ", " +
                                "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                                "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                                "	    '" + this.TradeDate.ToString("yyyy-MM-dd") + "', " +
                                "	    " + this.IdTransactionType + ", " +
                                " 	    " + this.TransactionNAV.ToString().Replace(",", ".") + ", " +
                                "	    50, " +
                                "	    " + this.Contact.NumProposta + ", " +
                                "	    NULL, " +
                                "	    " + this.Quantity.ToString().Replace(",", ".") + ", " +
                                "	    " + this.FinAmount.ToString().Replace(",", ".") + ", " +
                                "	    0, " +
                                "	    NULL, " +
                                "	    0; SELECT @@Identity";
                    break;
                default:
                    break;
            }
            Identifier = curNestConn.Return_String(SQLString);
        }
    }
}