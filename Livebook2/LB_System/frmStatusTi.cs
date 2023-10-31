using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmStatusTi : LBForm
    {
        public frmStatusTi()
        {
            InitializeComponent();
        }
        object padLock = new object();

        private void frmStatusTi_Load(object sender, EventArgs e)
        {
            timer1.Start();
            LoadPriceRT();
        }

        void LoadHistorial()
        {

            string StringSQL;

            StringSQL = " Select coalesce(E.Preco,F.Preco),coalesce(C.Descricao,D.Descricao),Yesterday,Prev,Yesterday-Prev as Diff from " +
                        " ( Select SrType,Source as Source1,count(*)Yesterday from  " +
                        " dbo.VW_Precos " +
                        " where SrDate=@Yesterday " +
                        " group by SrType,Source " +
                        " )A full outer JOIN ( " +
                        " Select SrType,Source as Source2,count(*)Prev from " +
                        " dbo.VW_Precos where SrDate=@Prev " +
                        " group by SrType,Source )B " +
                        " ON A.SrType = B.SrType AND A.Source1 = B.Source2" +
                        " INNER JOIN dbo.Tb102_Sistemas_Informacoes C" +
                        " ON A.Source1 = C.Id_Sistemas_Informacoes" +
                        " INNER JOIN dbo.Tb102_Sistemas_Informacoes D" +
                        " ON B.Source2 = D.Id_Sistemas_Informacoes" +
                        " INNER JOIN dbo.Tb116_Tipo_Preco E " +
                        " ON A.SrType = E.Id_Tipo_Preco" +
                        " INNER JOIN dbo.Tb116_Tipo_Preco F" + 
                        " ON B.SrType = F.Id_Tipo_Preco" +
                        " order by coalesce(A.SrType,B.SrType)" ;


        }        

        private int GetDelay(string ProgramName)
        {
            try
            {

                DateTime tempDate = Convert.ToDateTime("01/01/1900");
                DateTime tempNow = Convert.ToDateTime("01/01/1900");
                string SQLString1 = "SELECT CheckInTime " +
                                    " FROM NESTLOG.dbo.Tb900_CheckIn_Log A (nolock) " +
                                    " INNER JOIN NESTDB.dbo.Tb902_Programs B (nolock) " +
                                    " ON A.Program_Id=B.Program_Id" +
                                    " WHERE Program_Name='" + ProgramName + "'";

                using (newNestConn curConn = new newNestConn())
                {
                    string tempTime = curConn.Execute_Query_String(SQLString1);

                    if (tempTime != "" && tempTime != null)
                    {
                        tempDate = Convert.ToDateTime(tempTime);
                    }


                    string tempDBTime = curConn.Execute_Query_String("SELECT GetDate() ");
                    if (tempDBTime != "" && tempDBTime != null)
                    {
                        tempNow = Convert.ToDateTime(tempDBTime);
                    }
                }
                if (tempNow != null && tempDate != null)
                {
                    TimeSpan TotalDelay = tempNow - tempDate;
                    return TotalDelay.Seconds + TotalDelay.Minutes * 60 + TotalDelay.Hours * 3600;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception excep)
            {
                curUtils.Log_Error_Dump_TXT(excep.ToString(), this.Name.ToString());
                return 0;
            }

        }

        void LoadData()
        {
            lock (padLock)
            {

                foreach (Control curControl in this.Controls)
                {
                    if (curControl.Name.Contains("xlbl") && !curControl.Name.Contains("Flag"))
                    {
                        int tempDelay = GetDelay(curControl.Name.Replace("xlbl", ""));
                        if (tempDelay > 120)
                        {
                            ((Label)this.Controls[curControl.Name + "_Flag"]).Text = "OFF";
                            ((Label)this.Controls[curControl.Name + "_Flag"]).ForeColor = Color.Red;
                        }
                        else if (tempDelay > 8)
                        {
                            ((Label)this.Controls[curControl.Name + "_Flag"]).Text = "NOT RESP";
                            ((Label)this.Controls[curControl.Name + "_Flag"]).ForeColor = Color.Orange;
                        }
                        else
                        {
                            ((Label)this.Controls[curControl.Name + "_Flag"]).Text = "OK";
                            ((Label)this.Controls[curControl.Name + "_Flag"]).ForeColor = Color.Lime;
                        }
                    }
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadData();
        }

        void LoadPriceRT()
        {
            string StringSQL;

            StringSQL = "Select Preco,count(*)  as counter,convert(varchar,FeederTimeStamp,112) from nestrt.dbo.Tb065_Ultimo_Preco A " +
                        " INNER JOIN dbo.Tb116_Tipo_Preco B  " +
                        " ON A.SrType = B.Id_Tipo_Preco " +
                        " where SrType in (19,20,91) " +
                        " group by Preco,convert(varchar,FeederTimeStamp,112)" ;

            using (newNestConn curConn = new newNestConn())
            {
                DataTable tablep = curConn.Return_DataTable(StringSQL);

                foreach (DataRow curRow in tablep.Rows)
                {
                    if (curRow["Preco"].ToString() == "AV_VOLUME_6M")
                    {
                        lblVol6M.Text = curRow["counter"].ToString();
                    }

                    if (curRow["Preco"].ToString() == "DURATION")
                    {
                        lblDuration.Text = curRow["counter"].ToString();
                    }

                    if (curRow["Preco"].ToString() == "MKT_CAP")
                    {
                        lblMktCap.Text = curRow["counter"].ToString();
                    }
                }
            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadPriceHist();
        }

        void LoadPriceHist()
        {
            DataTable tableOrdens;
            string StringSQl = "Select * from FCN_CheckList_HistPrice('" + dtpPrev.Value.ToString("yyyy-MM-dd") + "','" + dtpYesterday.Value.ToString("yyyy-MM-dd") + "')";

            dgViewHistPrice.Columns.Clear();
            using (newNestConn curConn = new newNestConn())
            {
                tableOrdens = curConn.Return_DataTable(StringSQl);
            }
            dtgViewHistPrice.DataSource = tableOrdens;

            bool TR_Day, TR_Index, Last;
            Last = TR_Day = TR_Index = false;

            double TR_Day_Quant, TR_Index_Quant, Last_Quant;
            TR_Day_Quant = TR_Index_Quant = Last_Quant = 0;

            foreach (DataRow curRow in tableOrdens.Rows)
            {
                if (curRow.ItemArray[0].ToString() == "TR_Index" && curRow.ItemArray[1].ToString() == "BLOOMBERG")
                {
                    TR_Index = true;
                    TR_Index_Quant = double.Parse(curRow.ItemArray[2].ToString());
                }
                if (curRow.ItemArray[0].ToString() == "TR_Day" && curRow.ItemArray[1].ToString() == "BLOOMBERG")
                {
                    TR_Day = true;
                    TR_Day_Quant = double.Parse(curRow.ItemArray[2].ToString());
                }
                if (curRow.ItemArray[0].ToString() == "LAST" && curRow.ItemArray[1].ToString() == "BLOOMBERG")
                {
                    Last = true;
                    Last_Quant = double.Parse(curRow.ItemArray[2].ToString());
                }
            }

            if (TR_Index)
            {
                txtTR_Index.Text = TR_Index_Quant.ToString();
            }

            if (TR_Day)
            {
                txtTR_Day.Text = TR_Day_Quant.ToString();
            }

            if (Last)
            {
                txt_Last.Text = Last_Quant.ToString();
            }
        
        }
    }
}
