﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;
using LiveDLL;

namespace LiveBook
{
    public partial class frmPrepareXML : Form
    {
        

        public frmPrepareXML()
        {
            InitializeComponent();
        }

        private void cmdProcess_Click(object sender, EventArgs e)
        {
            ProccessFile();
        }

        private void ProccessFile()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1;

            string curFileName;
            
            int HideCounter = 0;

            openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = @"C:\Temp2\";
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "XML Files|*.xml";
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                curFileName = openFileDialog1.FileName;
                txtOrigPath.Text = curFileName;

                string newFileName = curFileName.Replace(".xml", "_SEND_VERSION.xml");

                StreamReader sr = new StreamReader(curFileName);
                StreamWriter sw = new StreamWriter(newFileName);

                SortedList<string, HideProperties> curList = new SortedList<string, HideProperties>();

                string allFile = "";

                allFile = sr.ReadToEnd();

                Int32 DatePos = allFile.IndexOf("<dtposicao>", 0);

                string dateString = GetNextBetweenTagValue(allFile, DatePos);

                DateTime curDate = DateTime.Parse(dateString.Substring(0, 4) + "-" + dateString.Substring(4, 2) + "-" + dateString.Substring(6, 2));

                using (newNestConn curConn = new newNestConn())
                {
                    /*
                    DataTable curTable = curConn.Return_DataTable(" SELECT ExchangeTicker, SUM(Position)  " +
                                             " FROM NESTDB.dbo.Tb000_Historical_Positions A " +
                                             " INNER JOIN NESTDB.dbo.Tb001_Securities B " +
                                             " ON A.[Id Ticker]=B.IdSecurity " +
                                             " WHERE [Id Portfolio]=38 AND [Date Now]='" + curDate.ToString("yyyy-MM-dd") + "' AND [Id Book] in (6,10)  " +
                                             " GROUP BY ExchangeTicker  " +
                                             " ORDER BY AVG([Total P/L]/CASE WHEN Cash<>0 THEN Cash ELSE 1 END)");
                    */

                    DataTable curTable = curConn.Return_DataTable(" SELECT ExchangeTicker, SUM(Position)  " +
                                             " FROM NESTDB.dbo.Tb000_Historical_Positions A " +
                                             " INNER JOIN NESTDB.dbo.Tb001_Securities B " +
                                             " ON A.[Id Ticker]=B.IdSecurity " +
                                             " WHERE [Id Portfolio]=38 AND [Date Now]='" + curDate.ToString("yyyy-MM-dd") + "' " + // AND [Id Book] in (6,10)  " +
                                             "    and [Id Ticker] in " +
                                             "   ( " +
                                             "   SELECT [Id Ticker] FROM NESTDB.dbo.Tb000_Historical_Positions A " +
                                             "   WHERE [Id Portfolio]=38 AND [Date Now]='" + curDate.ToString("yyyy-MM-dd") + "' AND [Id Book] in (6,10)  " +
                                             "   )" +
                                             " GROUP BY ExchangeTicker  " +
                                             " ORDER BY AVG([Total P/L]/CASE WHEN Cash<>0 THEN Cash ELSE 1 END)");




                    foreach (DataRow curRow in curTable.Rows)
                    {
                        HideProperties curHideProperties = new HideProperties();
                        curHideProperties.NestTicker = curRow[0].ToString();
                        curHideProperties.HideName = "NEST" + HideCounter++.ToString();
                        curHideProperties.Quantity = (int)LiveDLL.Utils.ParseToDouble(curRow[1]);
                        curList.Add(curHideProperties.NestTicker, curHideProperties);
                    }
                }



                Int32 curPos = 0;

                while (curPos <= allFile.Length && curPos != -1)
                {
                    curPos = allFile.IndexOf("<codativo>", curPos + 1);
                    if (curPos > 0)
                    {
                        Int32 endPos = allFile.IndexOf("</codativo>", curPos);
                        string curAsset = allFile.Substring(curPos + 10, endPos - curPos - 10);

                        Console.WriteLine(curAsset);

                        Int32 ISINPos = allFile.IndexOf("<isin>", curPos - 100);
                        Int32 ISINPosEnd = allFile.IndexOf("</isin>", ISINPos);
                        string curISIN = allFile.Substring(ISINPos + 6, ISINPosEnd - ISINPos - 6);

                        Int32 QtdPos = allFile.IndexOf("<qtdisponivel>", endPos + 1);
                        Int32 QtdPosEnd = allFile.IndexOf("</qtdisponivel>", QtdPos);
                        string curQuantity = allFile.Substring(QtdPos + 14, QtdPosEnd - QtdPos - 14);

                        Int32 FinPos = allFile.IndexOf("<valorfindisp", QtdPosEnd + 1);
                        if (FinPos > 0)
                        {
                            Int32 FinPosEnd = allFile.IndexOf("</valorfindisp", FinPos);
                            string curFinAmount = allFile.Substring(FinPos + 14, FinPosEnd - FinPos - 14);

                            Int32 PUPos = allFile.IndexOf("<puposicao>", FinPosEnd + 1);
                            Int32 PUPosEnd = allFile.IndexOf("</puposicao>", PUPos);
                            string curPU = allFile.Substring(PUPos + 11, PUPosEnd - PUPos - 11);

                            Int32 ClassePos = allFile.IndexOf("<classeoperacao>", PUPosEnd + 1);
                            Int32 ClassePosEnd = allFile.IndexOf("</classeoperacao>", ClassePos);
                            string curClasse = allFile.Substring(ClassePos + 16, ClassePosEnd - ClassePos - 16);

                            Int32 AlugPos = allFile.IndexOf("<txalug>", ClassePosEnd + 1);
                            Int32 AlugPosEnd = allFile.IndexOf("</txalug>", AlugPos);
                            string curAlug = allFile.Substring(AlugPos + 8, AlugPosEnd - AlugPos - 8);

                            int curIndex = 0;

                            if (curAsset == "TAMM4")
                            {
                                MessageBox.Show("d");
                            }

                            if ((curIndex = curList.IndexOfKey(curAsset)) >= 0)
                            {
                                Console.WriteLine(curAsset + "  " + curClasse + "  " + Convert.ToInt32(curQuantity) + "  " + curList[curAsset].Quantity);

                                int offset = 0;

                                string curHideName = curList[curAsset].HideName;

                                allFile = allFile.Substring(0, ISINPos + 6) + "XXXXXXXXXXXX" + allFile.Substring(ISINPosEnd);
                                offset = offset + "XXXXXXXXXXXX".Length - (ISINPosEnd - (ISINPos + 6));

                                allFile = allFile.Substring(0, curPos + 10) + curHideName + allFile.Substring(endPos);
                                offset = offset + curHideName.Length - (endPos - (curPos + 10));
                                allFile = allFile.Substring(0, QtdPos + 14 + offset) + curFinAmount + allFile.Substring(QtdPosEnd + offset);
                                offset = offset + curFinAmount.Length - (QtdPosEnd - (QtdPos + 14));
                                allFile = allFile.Substring(0, PUPos + 11 + offset) + "1" + allFile.Substring(PUPosEnd + offset);
                                offset = offset + "1".Length - (PUPosEnd - (PUPos + 11));
                                allFile = allFile.Substring(0, AlugPos + 8 + offset) + "0" + allFile.Substring(AlugPosEnd + offset);
                                offset = offset + "0".Length - (AlugPosEnd - (AlugPos + 8));

                                curPos = curPos + offset - 50;
                            }
                        }
                    }
                }

                sw.Write(allFile);

                sr.Close();
                sw.Close();

                string CheckSQL = " SELECT B.* " +
                  " FROM  " +
                  " ( " +
                  " 	SELECT Ticker, SUM(Position) Position, Book " +
                  " 	FROM NESTDB.dbo.Tb000_Historical_Positions " +
                  " 	WHERE [Id Portfolio]=38 AND [Date Now]='2011-08-05' AND [Id Book] in(6,10) " +
                  " 	GROUP BY Ticker, Book " +
                  " ) A " +
                  " INNER JOIN  " +
                  " ( " +
                  " 	SELECT Ticker, SUM(Position) Position, Book " +
                  " 	FROM NESTDB.dbo.Tb000_Historical_Positions " +
                  " 	WHERE [Id Portfolio]=38 AND [Date Now]='2011-08-05' AND [Id Book]<>6 AND [Id Book]<>10 AND [Id Book]<>7" +
                  " 	GROUP BY Ticker, Book " +
                  " ) B " +
                  " ON A.Ticker=B.Ticker ";


                using (newNestConn curConn = new newNestConn())
                {
                    DataTable CheckTable = curConn.Return_DataTable(CheckSQL);

                    if (CheckTable.Rows.Count > 0)
                    {
                        string MessageString = "The data was exported and the file generated. The following positions were in other books but had the same ticker as positions in the arb book. \r\n\r\nYOU MUST EDIT THE EXPORTED FILE!!! \r\n\r\n";

                        foreach (DataRow curRow in CheckTable.Rows)
                        {
                            MessageString += curRow[0].ToString() + "  " + curRow[1].ToString() + "  " + curRow[2].ToString() + "\r\n";
                        }

                        MessageBox.Show(MessageString, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string MessageString = "The file was successfully generated.";
                        MessageBox.Show(MessageString, "File Generated", MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                }
            }
        }

        private class HideProperties
        { 
            public string NestTicker;
            public int Quantity;
            public string HideName;
        }

        private void frmPrepareXML_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetNextBetweenTagValue(string HTMLString, int curPos)
        {
            int curPos2 = 0;

            if (curPos != -1)
            {
                curPos = HTMLString.IndexOf(">", curPos);
                curPos2 = HTMLString.IndexOf("<", curPos);
                return HTMLString.Substring(curPos + 1, curPos2 - curPos - 1);
            }
            else
            {
                return "";
            }
        }
    }
}
