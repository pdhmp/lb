using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using NestDLL;
using LiveBook.Business;

using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
namespace LiveBook
{
    public partial class frmQuote : LBForm
    {
        newNestConn curConn = new newNestConn();

        public int Id_Ticker;

        public frmQuote()
        {
            InitializeComponent();
        }

        private void frmTop10_Load(object sender, EventArgs e)
        {
            
            txtId_Ticker.Text = "1";
            timer1.Start();
        }

        public void LoadQuotes()
        {
            string SQLString;
            string SQLString1;

            if (Id_Ticker != 0)
            {
                SQLString = " SELECT NestTicker " +
                            "FROM dbo.Tb001_Securities  " +
                            "WHERE IdSecurity=" + Id_Ticker;

                string curTicker = curConn.Execute_Query_String(SQLString);

                txtTicker.Text = curTicker;
                this.Text = "Nest Quote - " + curTicker;


                SQLString1 = " SELECT IdSecurity, " +
                            "MAX(CASE WHEN SrType=1 THEN SrValue ELSE null END) AS Last, " +
                            "MAX(CASE WHEN SrType=9 THEN SrValue ELSE null END) AS Bid, " +
                            "MAX(CASE WHEN SrType=10 THEN SrValue ELSE null END) AS Ask, " +
                            "MAX(CASE WHEN SrType=22 THEN SrValue ELSE null END) AS BidQuantity, " +
                            "MAX(CASE WHEN SrType=23 THEN SrValue ELSE null END) AS AskQuantity, " +
                            "MAX(CASE WHEN SrType=6 THEN SrValue ELSE null END) AS AcVolume " +
                            "FROM [NESTRT].dbo.Tb065_Ultimo_Preco  " +
                            "WHERE IdSecurity=" + Id_Ticker + " " +
                            "GROUP BY IdSecurity ";

                DataTable curTable = curConn.Return_DataTable(SQLString1);
                foreach (DataRow curRow in curTable.Rows)
                {
                    if (curRow[1].ToString() != "") { txtLast.Text = Convert.ToSingle(curRow[1]).ToString("#,##0.00"); } else { txtLast.Text = ""; };
                    if (curRow[2].ToString() != "") { txtBid.Text = Convert.ToSingle(curRow[2]).ToString("#,##0.00"); } else { txtBid.Text = ""; };
                    if (curRow[3].ToString() != "") { txtAsk.Text = Convert.ToSingle(curRow[3]).ToString("#,##0.00"); } else { txtAsk.Text = ""; };
                    if (curRow[4].ToString() != "") { txtBidQuantity.Text = Convert.ToSingle(curRow[4]).ToString("#,##0."); } else { txtBidQuantity.Text = ""; };
                    if (curRow[5].ToString() != "") { txtAskQuantity.Text = Convert.ToSingle(curRow[5]).ToString("#,##0."); } else { txtAskQuantity.Text = ""; };
                    if (curRow[6].ToString() != "") { txtAcVolume.Text = Convert.ToSingle(curRow[6]).ToString("#,##0."); } else { txtAcVolume.Text = ""; };
                } 
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadQuotes();
            txtId_Ticker.Text = Id_Ticker.ToString();
        }

    }
}