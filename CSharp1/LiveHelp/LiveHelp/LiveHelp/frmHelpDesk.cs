using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Helpers;
using DevExpress.XtraEditors.Repository;

using DevExpress.XtraGrid.Views.Grid;
using System.Runtime.InteropServices;

namespace NestDesk
{
    public partial class frmHelpDesk : Form
    {
        public newNestConn DB = new newNestConn();
        Utils curUtils = new Utils();

        SqlDataAdapter daREfresh;
        DataTable Tb;
        RefreshHelper hlprOrders;

        /*
         --Desabilitar o "X"
        const int MF_BYPOSITION = 0x400;
        [DllImport("User32")]

        private static extern int RemoveMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("User32")]

        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("User32")]

        private static extern int GetMenuItemCount(IntPtr hWnd);
        */

        public frmHelpDesk()
        {
            string DBUser;
            DBUser = DB.Execute_Query_String("SELECT Id_Pessoa FROM NESTDB.dbo.Tb014_Pessoas WHERE login='" + System.Environment.UserName + "'");
            NestDLL.NUserControl.Instance.User_Id = int.Parse(DBUser);
            InitializeComponent();

            dgTicket.LookAndFeel.UseDefaultLookAndFeel = false;
            dgTicket.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dgTicket.LookAndFeel.SetSkinStyle("Blue");
            //this.Visible = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
           /*
         --Desabilitar o "X"
            IntPtr hMenu = GetSystemMenu(this.Handle, false);

            int menuItemCount = GetMenuItemCount(hMenu);

            RemoveMenu(hMenu, menuItemCount - 1, MF_BYPOSITION);
            */

            if (curUtils.Verifica_Acesso(27))
            {
                cmbUser.Enabled = true;
                dtpDate.Enabled = true;
            }
            dtpDate.Value = DateTime.Now;

            Carrega_Grid();
            NestDLL.FormUtils.LoadCombo(cmbUser, "Select IdUser,Login FROM [FCN_CheckGroupAccess](25)", "IdUser", "Login", NestDLL.NUserControl.Instance.User_Id);
            NestDLL.FormUtils.LoadCombo(cmbType, "Select 0 as Id_Incident,'Select Item:' as Incident_Description union all Select Id_Incident,Incident_Description from NESTHDESK.dbo.Tb001_Incident order by Incident_Description", "Id_Incident", "Incident_Description");
            cmbType.SelectedValue = 0;
            this.Hide();
            chkOpen.Checked = true;

        }

        void Carrega_Grid()
        {
            string SqlString;
            string StringFilter = "";
            string StringOrder = "";

            if (!curUtils.Verifica_Acesso(27))
            {
                StringFilter = " Where IdUserIncident=" + NestDLL.NUserControl.Instance.User_Id;
            }
            StringOrder = " Order by IdStatus asc ,OpenDate desc ";

            SqlString = "SELECT * FROM [NESTHDESK].[dbo].FCN_AllCalls() " + StringFilter + StringOrder;
            daREfresh = new SqlDataAdapter();
            Tb = new DataTable();

            daREfresh = DB.Return_DataAdapter(SqlString);
            daREfresh.Fill(Tb);
            dgTicket.DataSource = Tb;

            curUtils.SetColumnStyle(dtgTicket,2);
        }

        private void cmdrefresh_Click(object sender, EventArgs e)
        {
            Carrega_Grid();
        }

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cmbType.SelectedValue.ToString()) !=0)
            {

                int retorno = Insert_Call();

                if (retorno == 99)
                {
                    MessageBox.Show("Error on insert!");
                }
                else
                {
                    //MessageBox.Show("Inserted!");
                    txtObs.Clear();
                    Carrega_Grid();
                }
            }
            else
            {
                MessageBox.Show("Select one Item!");
            }
        }

        int Insert_Call()
        {
            try
            {

                string StringSQL;
                StringSQL = " EXEC NESTHDESK.dbo.Proc_InsertCall " + cmbType.SelectedValue.ToString() + ",'" +
                    txtObs.Text + "'," +
                    cmbUser.SelectedValue.ToString() + ",1,'" +
                     dtpDate.Value.ToString("yyyyMMdd") + "','19000101'";

                return DB.ExecuteNonQuery(StringSQL, 1);
            }
            catch (Exception e)
            {
                return 99;
            }
        }

        private void frmHelpDesk_FormClosing(object sender, FormClosingEventArgs e)
        {
            curUtils.Save_Columns(dtgTicket);
            this.WindowState = FormWindowState.Minimized;
            /*
            if(!System.Environment.HasShutdownStarted)
                e.Cancel = true;
             * */
        }

        private void dtgTicket_DoubleClick(object sender, EventArgs e)
        {
            GridView Get_Column = sender as GridView;
            string Column_Name = Get_Column.FocusedColumn.Name.ToString();
            string TextValue = Convert.ToString(dtgTicket.GetRowCellValue(dtgTicket.FocusedRowHandle, "Action"));
            string IdTicket = Convert.ToString(dtgTicket.GetRowCellValue(dtgTicket.FocusedRowHandle, "IdTicket"));

            string SQLString;
            string StringStatus = "";

            if (Column_Name == "colAction")
            {
                if (TextValue == "Close")
                {
                    string curMessage = "Are you sure want to close the Incident?";

                    DialogResult UserAnswer = MessageBox.Show(curMessage, "Close Incident", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    if (UserAnswer == DialogResult.Yes)
                    {
                        StringStatus = "2";
                    }

                }
                if (TextValue == "ReOpen")
                {
                    string curMessage = "Are you sure want to ReOpen the Incident?";

                    DialogResult UserAnswer = MessageBox.Show(curMessage, "ReOpen Incident", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

                    if (UserAnswer == DialogResult.Yes)
                    {
                        StringStatus = "1";
                    }
                }
                if (StringStatus != "")
                {
                    SQLString = "EXEC NESTHDESK.dbo.proc_UpdateCall " + IdTicket + "," + StringStatus + "," + NestDLL.NUserControl.Instance.User_Id;
                    int retorno = DB.ExecuteNonQuery(SQLString);
                    if (retorno != 99)
                    {
                        Carrega_Grid();
                    }
                }
               
            }
        }

        private void RestaurarApp()
        {
            if (!this.Visible)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void frmHelpDesk_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }

        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            RestaurarApp();

        }

        private void mniRestaurar_Click(object sender, EventArgs e)
        {
            RestaurarApp();
        }

        //    notifyIcon1.ShowBalloonTip(10000, "Mostrando Balão", "Você pediu para o balão ser mostrado", ToolTipIcon.Info);

        private void dtgTicket_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "colCloseDate")
            {
                if (e.CellValue.ToString() == "01/01/1900 00:00:00")
                {
                    e.Appearance.ForeColor = Color.White;
                    e.DisplayText = "";
                }
            }

            if (e.Column.Name == "colAction")
            {
                if (e.CellValue.ToString() == "Close" || e.CellValue.ToString() == "ReOpen")
                {
                    e.Appearance.ForeColor = Color.Blue;
                    e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Underline);
                }
            }
        }

        
        void ApplyFilter()
        {
            if (chkOpen.Checked)
            {
                dtgTicket.Columns[8].FilterInfo = new DevExpress.XtraGrid.Columns.ColumnFilterInfo(DevExpress.XtraGrid.Columns.ColumnFilterType.Value, null, "[IdStatus] = 1b");
                dtgTicket.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Default;
            }
            else 
            {
                dtgTicket.Columns[8].ClearFilter();
            }

        }

        private void chkOpen_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();

        }

        private void dtgTicket_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            curUtils.Save_Columns(dtgTicket);
        }

        private void dtgTicket_DragObjectDrop(object sender, DevExpress.XtraGrid.Views.Base.DragObjectDropEventArgs e)
        {
            curUtils.Save_Columns(dtgTicket);
        }
    }
}