using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SGN.Validacao;
using NestDLL;
using System.Data.SqlClient;
using DevExpress.XtraEditors.Repository;
using System.Threading;


namespace SGN
{
    public partial class frmTradeSplit : LBForm
    {
        Valida Valida = new Valida();
        CarregaDados CargaDados = new CarregaDados();
        bool HoldUpdates;
        bool lockRefresh;
        bool lockSplit;
        string SplitDate;

        DataTable tablep = new DataTable();
        DataTable newTable = new DataTable();
        
        string SQLString = "SELECT * FROM [FCN_GET_Trade_Split_Status]()";
        
        DataTable tempTable = new DataTable();
        BindingSource bndGridSource = new BindingSource();
        static readonly object padlock = new object();

        bool buttonsAdded = false;

        public frmTradeSplit()
        {
            InitializeComponent();
        }

        public void SetUpdateFreq(int UpdTime)
        {
            //timer1.Interval = UpdTime;
        }


        private void frmTradeSplit_Load(object sender, EventArgs e)
        {
            

            dtg.LookAndFeel.UseDefaultLookAndFeel = false;
            dtg.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtg.LookAndFeel.SetSkinStyle("Blue");

            tempTable = CargaDados.curConn.Return_DataTable(SQLString);

            DataColumn[] KeyColumns = new DataColumn[4];
            KeyColumns[0] = tempTable.Columns["Id Book"];
            KeyColumns[1] = tempTable.Columns["Id Section"];
            KeyColumns[2] = tempTable.Columns["Id Broker"];
            KeyColumns[3] = tempTable.Columns["Id Ticker"];

            newTable = tempTable.Copy();

            DataColumn[] newKeyColumns = new DataColumn[4];
            newKeyColumns[0] = newTable.Columns["Id Book"];
            newKeyColumns[1] = newTable.Columns["Id Section"];
            newKeyColumns[2] = newTable.Columns["Id Broker"];
            newKeyColumns[3] = newTable.Columns["Id Ticker"];

            tempTable.PrimaryKey = KeyColumns;
            newTable.PrimaryKey = newKeyColumns;

            bndGridSource.DataSource = tempTable;
            dtg.DataSource = bndGridSource;

            Valida.SetColumnStyle(dgTradeSplit, 1);
            
            Carrega_Check();

            tmrUpdate.Start();
            dgTradeSplit.ExpandAllGroups();

        }

        void Carrega_Grid()
        {
            lock (padlock)
            {
                newTable.Clear();
                newTable = CargaDados.curConn.Return_DataTable(SQLString);
            }

            lockRefresh = false;

        }
       
        private void dgTradeSplit_DoubleClick(object sender, EventArgs e)
        {
            int Id_Broker;
            int Id_Ticker;
            int Id_Book;
            int Id_Section;
            string Ticker_Name;
            string Broker_Name;

            string Column_Name = dgTradeSplit.FocusedColumn.ToString();

            if (Column_Name == "Edit")
            {
                Id_Ticker = Convert.ToInt32(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "Id Ticker"));
                Id_Broker = Convert.ToInt32(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "Id Broker"));

                Id_Book = Convert.ToInt32(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "Id Book"));
                Id_Section = Convert.ToInt32(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "Id Section"));

                Ticker_Name = Convert.ToString(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "NestTicker"));
                Broker_Name = Convert.ToString(dgTradeSplit.GetRowCellValue(dgTradeSplit.FocusedRowHandle, "Broker"));

                frmTrade_Aloc_Override Aloca = new frmTrade_Aloc_Override();

                Aloca.Top = this.Top + 100;
                Aloca.Left = this.Left + 100;

                Aloca.Id_Ticker = Id_Ticker;
                Aloca.Id_Broker = Id_Broker;
                Aloca.Id_Book = Id_Book;
                Aloca.Id_Section = Id_Section;
                Aloca.lblTicker.Text = Ticker_Name;
                Aloca.lblBroker.Text = Broker_Name;
                Aloca.ShowDialog();
                Carrega_Grid();
            }
        }

        private void dgTradeSplit_MouseDown(object sender, MouseEventArgs e)
        {
            HoldUpdates = true;
        }

        private void dgTradeSplit_MouseUp(object sender, MouseEventArgs e)
        {
            HoldUpdates = false;
        }

        private void dgTradeSplit_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (Convert.ToDouble(dgTradeSplit.GetRowCellValue(e.RowHandle, "Override_Type")) > 0)
            {
                e.Appearance.BackColor = Color.LightBlue; ;
            }
        }

        private void dgTradeSplit_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            Valida.Save_Columns(dgTradeSplit);
        }

        private void dgTradeSplit_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {                                                                              
            Valida.Save_Columns(dgTradeSplit);
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            if (!lockRefresh)
            {
                lockRefresh = true;
                cmdRefresh.Enabled = false;
                System.Threading.Thread t1 = new System.Threading.Thread(new ThreadStart(Carrega_Grid));
                t1.Start();
            }
        }

        private void cmdSplitAll_Click(object sender, EventArgs e)
        {
            cmdSplitAll.Enabled = false;

            if (!lockSplit)
            {
                lockSplit = true;
                cmdSplitAll.Enabled = false;
                System.Threading.Thread t2 = new System.Threading.Thread(new ThreadStart(SplitAllTrades));
                t2.Start();
            }
        }

        private void SplitAllTrades()
        {
            int retorno = 0;

            SplitDate = DateTime.Now.ToString("yyyyMMdd");

            string SQLString = "DECLARE @Retorno int " +
                               " EXEC proc_Update_All_Trade_Splits '" + SplitDate + "',@Retorno output " +
                               " Select @Retorno ";

            retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);
            if (retorno == 0)
            {
                MessageBox.Show("Error on Split!");

            }
            SQLString = "UPDATE NESTLOG.dbo.Tb900_CheckIn_Log SET CheckInTime=getdate() Where Program_Id=70";
            retorno = CargaDados.curConn.ExecuteNonQuery(SQLString, 1);

            lockSplit = false;

        }

        private void tmrUpdate_Tick(object sender, EventArgs e)
        {
            Carrega_Check();
            if (!lockRefresh) 
            {
                if (cmdRefresh.Enabled == false)
                {
                    cmdRefresh.Enabled = true;

                    lock (padlock)
                    {
                        dgTradeSplit.BeginDataUpdate();
                        tempTable = newTable.Copy();
                        dtg.DataSource = tempTable;
                        dgTradeSplit.EndDataUpdate();
                        dgTradeSplit.ExpandAllGroups();
                        //bndGridSource.inva
                    }
                }
            }

            if (!lockSplit)
            {
                if (cmdSplitAll.Enabled == false)
                {
                    cmdSplitAll.Enabled = true;
                    lblLastUpdate.Text = SplitDate;
                    EventArgs tempE = new EventArgs();
                    cmdRefresh_Click(this, tempE);
                }
            }

            if (!buttonsAdded)
            {
                // Add Edit Button
                dgTradeSplit.Columns.AddField("Edit");
                dgTradeSplit.Columns["Edit"].VisibleIndex = 0;
                dgTradeSplit.Columns["Edit"].Width = 55;
                RepositoryItemButtonEdit item4 = new RepositoryItemButtonEdit();
                item4.Buttons[0].Tag = 1;
                item4.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item4.Buttons[0].Caption = "Edit";
                dtg.RepositoryItems.Add(item4);
                dgTradeSplit.Columns["Edit"].ColumnEdit = item4;
                dgTradeSplit.Columns["Edit"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;

                // Add Update
                dgTradeSplit.Columns.AddField("Update");
                dgTradeSplit.Columns["Update"].VisibleIndex = 0;
                dgTradeSplit.Columns["Update"].Width = 55;
                RepositoryItemButtonEdit item5 = new RepositoryItemButtonEdit();
                item5.Buttons[0].Tag = 2;
                item5.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
                item5.Buttons[0].Caption = "Update";
                dtg.RepositoryItems.Add(item5);
                dgTradeSplit.Columns["Update"].ColumnEdit = item5;
                dgTradeSplit.Columns["Update"].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;

                for (int c = 0; c < dgTradeSplit.Columns.Count; c++)
                {
                    dgTradeSplit.Columns[c].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                    dgTradeSplit.Columns[c].DisplayFormat.FormatString = "#,##0;(#,##0);-"; ;
                }
                
                buttonsAdded = true;
                tmrUpdate.Interval = 5000;
            }
        }

        void Carrega_Check()
        {
            string SQLString = "Select top 1 convert(varchar,CheckInTime,8) from NESTLOG.dbo.Tb900_CheckIn_Log Where Program_Id = 70";
            lblLastUpdate.Text = CargaDados.curConn.Execute_Query_String(SQLString);
        }

        private void lblCopy_Click(object sender, EventArgs e)
        {
            dgTradeSplit.SelectAll();
            dgTradeSplit.CopyToClipboard();
        }

    }
}