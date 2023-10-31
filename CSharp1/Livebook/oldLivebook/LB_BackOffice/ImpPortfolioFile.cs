using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
using NestDLL;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;

namespace LiveBook
{
    #region Mellon
    public class Mellon
    {
        public void ImportFileMellonPort(string curFilePathAndName)
        {
            DateTime RefDate = new DateTime(1900, 01, 01);
            int IdPortfolio = 0;
            string curLine = "";
            string curFileName = curFilePathAndName.Substring(curFilePathAndName.LastIndexOf('\\') + 1);
            Encoding currentEncoding = Encoding.GetEncoding(1252);

            // Try to determine encoding

            StreamReader sr = new StreamReader(curFilePathAndName, currentEncoding);
            curLine = sr.ReadLine();
            if (curLine.Contains('\0')) { currentEncoding = System.Text.Encoding.Unicode; }

            sr = new StreamReader(curFilePathAndName, currentEncoding);

            // Try to Identify file

            curLine = sr.ReadToEnd();
            sr.Close();


            if (curLine.IndexOf("Benchmark	Limite Min.(%)	Limite Max.(%)") != -1)
            {
                ReadMellonAnaliseAnalitica(IdPortfolio, curFilePathAndName, curFileName, currentEncoding);
            }
            else
            {
                if (curLine.IndexOf("Cliente	Data") != -1)
                {
                    ReadMellonPort(IdPortfolio, curFilePathAndName, curFileName, currentEncoding);
                }
                if (curLine.IndexOf("Descrição	Fechamento D-1") != -1)
                {
                    RealMellonAnalise(IdPortfolio, curFilePathAndName, curFileName, currentEncoding);
                }
            }
        }

        void ReadMellonPort(int IdPortfolio, string curFilePathAndName, string curFileName, Encoding currentEncoding)
        {
            StreamReader sr = new StreamReader(curFilePathAndName, currentEncoding);
            string curLine = "";
            string InsertMode = "";
            string InsertString = "";
            DateTime RefDate = DateTime.Parse("1900-01-01");

            while ((curLine = sr.ReadLine()) != null)
            {
                if (curLine == "")
                {
                    InsertMode = "NOINSERT";
                }
                else
                {
                    if (curLine.Contains("Cliente	Data"))
                    {
                        string refLine = sr.ReadLine();
                        string[] refData = refLine.Split('\t');
                        string[] Date = refData[1].Split('/');
                        RefDate = new DateTime(int.Parse(Date[2].ToString()), int.Parse(Date[1].ToString()), int.Parse(Date[0].ToString()));

                        if (refLine.Contains("NEST ACOES MASTER")) IdPortfolio = 10;
                        if (refLine.Contains("NEST ARB MASTER")) IdPortfolio = 38;
                        if (refLine.Contains("NEST MILE HIGH MASTER")) IdPortfolio = 43;
                        if (refLine.Contains("NEST MH OVERSEAS FUND")) IdPortfolio = 42;
                        if (refLine.Contains("NEST HEDGE MASTER FIM")) IdPortfolio = 60;
                        if (refLine.Contains("NEST QUANT MAST")) IdPortfolio = 18;


                        if (IdPortfolio == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("Portfolio not found! File Not imported.", "Import failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return;
                        }

                        string PortName = "";

                        using (newNestConn curConn = new newNestConn())
                        {
                            PortName = curConn.Execute_Query_String("SELECT Port_Name FROM dbo.Tb002_Portfolios WHERE Id_Portfolio=" + IdPortfolio);


                            System.Windows.Forms.DialogResult userConfirmation = System.Windows.Forms.MessageBox.Show("Importing the following file: \r\n\r\nPortfolio:\t" + PortName + "\r\nDate:\t " + RefDate.ToString("dd-MMM-yy") + "\r\n\r\nDo you comfirm?", "Mellon File Import", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

                            if (userConfirmation != System.Windows.Forms.DialogResult.OK)
                                return;

                            curConn.ExecuteNonQuery("EXEC NESTIMPORT.dbo.[Proc_DeleteMellonFileData] '" + RefDate.ToString("yyyy-MM-dd") + "'," + IdPortfolio + ",'" + curFileName + "'");
                        }
                    }

                    else if (curLine == "Futuro") { InsertMode = "Futuro"; sr.ReadLine(); }
                    else if (curLine == "Ação") { InsertMode = "ACAO"; sr.ReadLine(); }
                    else if (curLine == "BDRs") { InsertMode = "ACAO"; sr.ReadLine(); }
                    else if (curLine == "ADRs") { InsertMode = "ACAO"; sr.ReadLine(); }
                    else if (curLine == "Fundo") { InsertMode = "FUNDO"; sr.ReadLine(); }
                    else if (curLine == "Opção") { InsertMode = "OPCAO"; sr.ReadLine(); }
                    else if (curLine == "Opção de Futuro") { InsertMode = "OPCAOFUTURO"; sr.ReadLine(); }
                    else if (curLine == "Renda Fixa") { InsertMode = "RFIXA"; sr.ReadLine(); }
                    else if (curLine == "Empréstimo de Ações") { InsertMode = "EMPREST"; sr.ReadLine(); }
                    else if (curLine == "Contas a Pagar/Receber") { InsertMode = "CONTASPAG"; sr.ReadLine(); }
                    else if (curLine == "Tesouraria") { InsertMode = "TESOUR"; sr.ReadLine(); }
                    else if (curLine == "Patrimônio") { InsertMode = "NAV"; sr.ReadLine(); }
                    else if (curLine == "Rentabilidade") { InsertMode = "RENT"; sr.ReadLine(); }
                    else
                    {
                        if (InsertMode == "ACAO")
                            InsertString += GetAcaoSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "EMPREST")
                            InsertString += GetEmprestSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "OPCAO")
                            InsertString += GetOpcaoSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "FUNDO")
                            InsertString += GetFundoSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "RFIXA")
                            InsertString += GetRendaFixaSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "CONTASPAG")
                            InsertString += GetContasPagSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "TESOUR")
                            InsertString += GetTesourariaSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "NAV")
                            InsertString += GetNAVSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "OPCAOFUTURO")
                            InsertString += GetOpcaoFuturoSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "Futuro")
                            InsertString += GetFuturoSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "RENT")
                            InsertString += GetRentabilidadeSQL(curFileName, RefDate, IdPortfolio, curLine);

                    }

                    if (InsertString.Length > 10)
                    {
                        using (newNestConn curConn = new newNestConn())
                        {
                            curConn.ExecuteNonQuery(InsertString);
                            InsertString = "";
                        }
                    }
                }
            }
            sr.Close();

            if (IdPortfolio == 0)
            {
                System.Windows.Forms.MessageBox.Show("There was an error reading the file. File Not imported.", "Import failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("File:\r\n\r\n" + curFileName + "\r\n\r\nimported successfully.", "File Imported", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        void RealMellonAnalise(int IdPortfolio, string curFilePathAndName, string curFileName, Encoding currentEncoding)
        {
            StreamReader sr = new StreamReader(curFilePathAndName, currentEncoding);
            string curLine = "";
            DateTime RefDate;

            string refData = curFileName.Substring(curFileName.LastIndexOf('_') - 7, 10);
            string[] Date = refData.Split('_');
            string PortName = "";
            RefDate = new DateTime(int.Parse(Date[0].ToString()), int.Parse(Date[1].ToString()), int.Parse(Date[2].ToString()));

            if (curFileName.Contains("BRAVO"))
            {
                IdPortfolio = 10;
                PortName = "Nest Ações";
            }
            if (curFileName.Contains("ARB"))
            {
                IdPortfolio = 38;
                PortName = "Nest Arb";
            }
            if (curFileName.Contains("MILE"))
            {
                IdPortfolio = 43;
                PortName = "Nest MIle";
            }
            if (curFileName.Contains("QUANT"))
            {
                IdPortfolio = 18;
                PortName = "Nest Quant";
            }

            if (curFileName.Contains("HEDMASTER"))
            {
                IdPortfolio = 60;
                PortName = "Nest Hedge Master";
            }




            using (newNestConn curConn = new newNestConn())
            {
                System.Windows.Forms.DialogResult userConfirmation = System.Windows.Forms.MessageBox.Show("Importing the following file: \r\n\r\nPortfolio:\t" + PortName + "\r\nDate:\t " + RefDate.ToString("dd-MMM-yy") + "\r\n\r\nDo you comfirm?", "Mellon File Import", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

                if (userConfirmation != System.Windows.Forms.DialogResult.OK)
                    return;

                curConn.ExecuteNonQuery("DELETE FROM NESTIMPORT.dbo.Tb_Mellon_Analise whERE IdPortfolio = " + IdPortfolio + " AND RefDate='" + RefDate.ToString("yyyy-MM-dd") + "'");


                while ((curLine = sr.ReadLine()) != null)
                {
                    string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
                    string SQLInsert = "";

                    if (InsArray[0] != "Descrição")
                    {
                        if (IdPortfolio == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("There was an error reading the file. File Not imported.", "Import failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return;
                        }
                        else
                        {
                            if (InsArray[0] == "")
                            {
                                SQLInsert = "'" + InsArray[5] + "', " +
                                             "'" + 0 + "', " +
                                            0 + ", " +
                                            0 + ", " +
                                            0 + ", " +
                                            InsArray[6].Replace("%", "") + ", " +
                                            0 + ";";
                                curConn.ExecuteNonQuery("INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Analise SELECT '" + curFileName + "', '" + RefDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert);
                            }
                            else
                            {
                                SQLInsert = "'" + InsArray[0] + "', " +
                                            ifnull(InsArray[1], 0) + ", " +
                                            ifnull(InsArray[2], 0) + ", " +
                                            ifnull(InsArray[3], 0) + ", " +
                                            ifnull(InsArray[4], 0) + ", " +
                                            ifnull(InsArray[5], 0) + ", " +
                                            ifnull(InsArray[6], 0) + ";";

                                curConn.ExecuteNonQuery("INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Analise SELECT '" + curFileName + "', '" + RefDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert);
                            }
                        }
                    }
                }
                System.Windows.Forms.MessageBox.Show("File:\r\n\r\n" + curFileName + "\r\n\r\nimported successfully.", "File Imported", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);

            }
        }

        object ifnull(object checkval, object retval)
        {
            if (checkval != null && checkval != "")
            {
                return checkval;
            }
            else
            {
                return retval;
            }
        }

        private string GetNAVSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray.Length > 4)
            {
                SQLInsert = "" +
                    ifnull(InsArray[0], 0) + "," +
                    ifnull(InsArray[1], 0) + "," +
                    ifnull(InsArray[2], 0) + "," +
                    ifnull(InsArray[3], 0) +
                    ",'" + InsArray[4] + "'";

                return "INSERT INTO [NESTIMPORT].[dbo].[Tb_Mellon_Patrimonio] SELECT  '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;

            }

            return "0";
        }

        private string GetFuturoSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            "'" + InsArray[1] + "', " +
                            "'" + InsArray[2] + "', " +
                            ifnull(InsArray[3], 0) + ", " +
                            ifnull(InsArray[4], 0) + ", " +
                            ifnull(InsArray[5], 0) + ", " +
                            ifnull(InsArray[6], 0) + ", " +
                            ifnull(InsArray[7], 0) + ", " +
                            ifnull(InsArray[8].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[9].Replace("%", ""), 0) + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Futuros SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetAcaoSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                             "'" + InsArray[1] + "', " +
                            ifnull(InsArray[2], 0) + ", " +
                            ifnull(InsArray[3], 0) + ", " +
                            ifnull(InsArray[4], 0) + ", " +
                            ifnull(InsArray[5], 0) + ", " +
                            ifnull(InsArray[6], 0) + ", " +
                            ifnull(InsArray[7], 0) + ", " +
                            ifnull(InsArray[8], 0) + ", " +
                            ifnull(InsArray[9], 0) + ", " +
                            ifnull(InsArray[10], 0) + ", " +
                            ifnull(InsArray[11], 0) + ", " +
                            ifnull(InsArray[12].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[13].Replace("%", ""), 0) + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Acoes SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetEmprestSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            ifnull(InsArray[1], 0) + ", " +
                            "'" + InsArray[2].Split('/')[2] + "-" + InsArray[2].Split('/')[1] + "-" + InsArray[2].Split('/')[0] + "', " +
                            "'" + InsArray[3].Split('/')[2] + "-" + InsArray[3].Split('/')[1] + "-" + InsArray[3].Split('/')[0] + "', " +
                            "'" + InsArray[4] + "', " +
                            "'" + InsArray[5] + "', " +
                            ifnull(InsArray[6], 0) + ", " +
                            ifnull(InsArray[7], 0) + ", " +
                            ifnull(InsArray[8], 0) + ", " +
                            ifnull(InsArray[9].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[10].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[11], 0) + ", " +
                            ifnull(InsArray[12], 0) + ", " +
                            ifnull(InsArray[13], 0) + ", " +
                            ifnull(InsArray[14].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[15].Replace("%", ""), 0) + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_EmprestimoAcoes SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetOpcaoSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {



                SQLInsert = "'" + InsArray[0] + "', " +
                            "'" + InsArray[1] + "', " +
                            "'" + InsArray[2] + "', " +
                            "'" + InsArray[3] + "', " +
                            "'" + InsArray[4] + "', " +
                            ifnull(InsArray[5], 0) + ", " +
                            "'" + InsArray[6].Split('/')[2] + "-" + InsArray[6].Split('/')[1] + "-" + InsArray[6].Split('/')[0] + "', " +
                            ifnull(InsArray[7], 0) + ", " +
                            ifnull(InsArray[8], 0) + ", " +
                            ifnull(InsArray[9], 0) + ", " +
                            ifnull(InsArray[10], 0) + ", " +
                            ifnull(InsArray[11], 0) + ", " +
                            ifnull(InsArray[12], 0) + ", " +
                            ifnull(InsArray[13], 0) + ", " +
                            ifnull(InsArray[14], 0) + ", " +
                            ifnull(InsArray[15].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[16].Replace("%", ""), 0);

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Opcoes SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetOpcaoFuturoSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            "'" + InsArray[1] + "', " +
                            "'" + InsArray[2] + "', " +
                            "'" + InsArray[3] + "', " +
                            "'" + InsArray[4] + "', " +
                            ifnull(InsArray[5], 0) + ", " +
                            "'" + InsArray[6].Split('/')[2] + "-" + InsArray[6].Split('/')[1] + "-" + InsArray[6].Split('/')[0] + "', " +
                            ifnull(InsArray[7], 0) + ", " +
                            ifnull(InsArray[8], 0) + ", " +
                            ifnull(InsArray[9], 0) + ", " +
                            ifnull(InsArray[10], 0) + ", " +
                            ifnull(InsArray[11], 0) + ", " +
                            ifnull(InsArray[12], 0) + ", " +
                            ifnull(InsArray[13].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[14].Replace("%", ""), 0);

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_OpcoesFuturos SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetFundoSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            "'" + InsArray[1] + "', " +
                            "'" + InsArray[2] + "', " +
                            ifnull(InsArray[3], 0) + ", " +
                            ifnull(InsArray[4], 0) + ", " +
                            ifnull(InsArray[5], 0) + ", " +
                            ifnull(InsArray[6], 0) + ", " +
                            ifnull(InsArray[7], 0) + ", " +
                            ifnull(InsArray[8], 0) + ", " +
                            ifnull(InsArray[9], 0) + ", " +
                            ifnull(InsArray[10].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[11].Replace("%", ""), 0);

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Fundos SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetRendaFixaSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            ifnull(InsArray[1], 0) + ", " +
                            "'" + InsArray[2].Split('/')[2] + "-" + InsArray[2].Split('/')[1] + "-" + InsArray[2].Split('/')[0] + "', " +
                            "'" + InsArray[3] + "', " +
                            "'" + InsArray[4] + "', " +
                            ifnull(InsArray[5].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[6].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[7].Replace("%", ""), 0) + ", " +
                            "'" + InsArray[8] + "', " +
                            "'" + InsArray[9].Split('/')[2] + "-" + InsArray[9].Split('/')[1] + "-" + InsArray[9].Split('/')[0] + "', " +
                            "'" + InsArray[10].Split('/')[2] + "-" + InsArray[10].Split('/')[1] + "-" + InsArray[10].Split('/')[0] + "', " +
                            ifnull(InsArray[11], 0) + ", " +
                            ifnull(InsArray[12], 0) + ", " +
                            ifnull(InsArray[13], 0) + ", " +
                            "0" +
                            ifnull(InsArray[14], 0) + ", " +
                            ifnull(InsArray[15], 0) + ", " +
                            ifnull(InsArray[16], 0) + ", " +
                            ifnull(InsArray[17], 0) + ", " +
                            ifnull(InsArray[18].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[19].Replace("%", ""), 0);

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_RendaFixa SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetContasPagSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            "'" + InsArray[1] + "', " +
                            ifnull(InsArray[2], 0) + ", " +
                            ifnull(InsArray[3].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[4].Replace("%", ""), 0);

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_ContasPagar SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetTesourariaSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            ifnull(InsArray[1], 0) + ", " +
                            ifnull(InsArray[2].Replace("%", ""), 0) + ", " +
                            ifnull(InsArray[3].Replace("%", ""), 0);

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Tesouraria SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetRentabilidadeSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            ifnull(InsArray[1], 0).ToString().Replace("%", "") + ", " +
                            ifnull(InsArray[2], 0).ToString().Replace("%", "") + ", " +
                            ifnull(InsArray[3], 0).ToString().Replace("%", "") + ", " +
                            ifnull(InsArray[4], 0).ToString().Replace("%", "") + ", " +
                            ifnull(InsArray[5], 0).ToString().Replace("%", "") + ", " +
                            ifnull(InsArray[6], 0).ToString().Replace("%", "") + ", " +
                            ifnull(InsArray[7], 0).ToString().Replace("%", "") + ", " +
                            ifnull(InsArray[8], 0).ToString().Replace("%", "") + ", " +
                            ifnull(InsArray[9], 0).ToString().Replace("%", "");

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Rentabilidade SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        double CheckFileDatePosition(DateTime FileDate, int IdPortfolio)
        {
            string StringSQL;
            string retorno;

            StringSQL = "Select count(IdPortfolio) FROM " +
                        " NESTIMPORT.dbo.Tb_Mellon_Acoes " +
                        " WHere RefDate = '" + FileDate.ToString("yyyyMMdd") + "' AND IdPortfolio =" + IdPortfolio.ToString();
            using (old_Conn curConn = new old_Conn())
            {
                retorno = curConn.Execute_Query_String(StringSQL);
            }

            if (retorno != "" && retorno != "0")
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        double CheckFileNamePosition(DateTime FileDate, string refFileName)
        {
            string StringSQL;
            string retorno;

            StringSQL = "Select count(IdPortfolio) FROM " +
                        " NESTIMPORT.dbo.Tb_Mellon_Acoes " +
                        " WHere RefDate = '" + FileDate.ToString("yyyyMMdd") + "' AND refFileName ='" + refFileName + "'";
            using (old_Conn curConn = new old_Conn())
            {
                retorno = curConn.Execute_Query_String(StringSQL);
            }

            if (retorno != "" && retorno != "0")
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        public void ImportFileMellonPerfAdm(string curFilePathAndName)
        {
            DateTime RefDate = new DateTime(1900, 01, 01);
            int IdPortfolio = 0;
            string curLine = "";
            string curFileName = curFilePathAndName.Substring(curFilePathAndName.LastIndexOf('\\') + 1);
            Encoding currentEncoding = Encoding.GetEncoding(1252);

            // Try to determine encoding

            StreamReader sr = new StreamReader(curFilePathAndName, currentEncoding);
            curLine = sr.ReadLine();
            if (curLine.Contains('\0')) { currentEncoding = System.Text.Encoding.Unicode; }

            sr = new StreamReader(curFilePathAndName, currentEncoding);

            // Try to Identify file

            string curFileType = "";

            while ((curLine = sr.ReadLine()) != null)
            {
                if (curLine.Contains("DISTRIBUIDOR	OPERADOR"))
                {
                    ReadMellonPerfAdm(curFilePathAndName, curFileName, currentEncoding);
                    break;
                }
            }

            sr.Close();

        }

        void ReadMellonPerfAdm(string curFilePathAndName, string curFileName, Encoding currentEncoding)
        {
            StreamReader sr = new StreamReader(curFilePathAndName, currentEncoding);
            string curLine = "";
            string InsertMode = "";
            string InsertString = "";
            DateTime RefDate = DateTime.Parse("1900-01-01");
            int IdPortfolio = 0;

            while ((curLine = sr.ReadLine()) != null)
            {
                if (curLine == "")
                {
                    InsertMode = "NOINSERT";
                }
                else
                {
                    RefDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));

                    InsertString += GetLineSQL(curFileName, RefDate, curLine);

                    if (InsertString.Length > 10)
                    {
                        using (newNestConn curConn = new newNestConn())
                        {
                            curConn.ExecuteNonQuery(InsertString);
                            InsertString = "";
                        }
                    }
                }
            }
            using (newNestConn curConn = new newNestConn())
            {
                curConn.ExecuteNonQuery(" EXEC NESTIMPORT.dbo.PROC_Update_ShareHolderMellon");
            }
            sr.Close();
        }

        string GetLineSQL(string curFileName, DateTime RefDate, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";
            int IdPortfolio = 0;

            IdPortfolio = ReturnIdPortfolio(InsArray[3]);
            if (IdPortfolio != 0)
            {
                if (InsArray[0] != "Total")
                {
                    SQLInsert = "'" + InsArray[0] + "', " +
                                 "'" + InsArray[1] + "', " +
                                InsArray[2] + ", " +
                                "'" + InsArray[3] + "', " +
                                InsArray[4] + ", " +
                                "'" + InsArray[5] + "', " +
                                InsArray[6] + ", " +
                                InsArray[7] + ", " +
                                InsArray[8] + "";

                    return "INSERT INTO NESTIMPORT.dbo.[Tb_Mellon_Investors_Perf] SELECT '" + curFileName + "', '" + RefDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio.ToString() + ", " + SQLInsert;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }

        }

        int ReturnIdPortfolio(string PortName)
        {
            int IdPortfolio = 0;
            if (PortName.Contains("NEST MILE HIGH FIC DE FIM")) IdPortfolio = 2;
            if (PortName.Contains("NEST MILE HIGH 30 FIC DE FIM")) IdPortfolio = 3;
            if (PortName.Contains("NEST ACOES FIC DE FIA")) IdPortfolio = 12;
            if (PortName.Contains("NEST MH1 FIC DE FIM")) IdPortfolio = 15;
            if (PortName.Contains("NEST ARB FIC FIM")) IdPortfolio = 40;
            if (PortName.Contains("NEST NA1 FIC FIA")) IdPortfolio = 13;
            if (PortName.Contains("NEST MILE HIGH MASTER FIM")) IdPortfolio = 43;
            if (PortName.Contains("NEST ACOES MASTER FIA")) IdPortfolio = 10;
            if (PortName.Contains("NEST ARB MASTER FIM")) IdPortfolio = 38;
            if (PortName.Contains("NEST QUANT FIC DE FIM")) IdPortfolio = 19;
            if (PortName.Contains("NEST HEDGE MASTER FIM")) IdPortfolio = 60;



            return IdPortfolio;
        }

        int CheckFileNamePerf(DateTime RefDate, string curFileName)
        {

            return 1;
        }

        void ReadMellonAnaliseAnalitica(int IdPortfolio, string curFilePathAndName, string curFileName, Encoding currentEncoding)
        {
            StreamReader sr = new StreamReader(curFilePathAndName, currentEncoding);
            string curLine = "";
            string InsertMode = "";
            string InsertString = "";
            DateTime RefDate = DateTime.Parse("1900-01-01");

            while ((curLine = sr.ReadLine()) != null)
            {
                if (curLine == "")
                {
                    InsertMode = "NOINSERT";
                }
                else
                {
                    if (curLine.Contains("Cliente	Data"))
                    {
                        string refLine = sr.ReadLine();
                        string[] refData = refLine.Split('\t');
                        string[] Date = refData[1].Split('/');
                        RefDate = new DateTime(int.Parse(Date[2].ToString()), int.Parse(Date[1].ToString()), int.Parse(Date[0].ToString()));

                        if (refLine.Contains("NEST ACOES MASTER")) IdPortfolio = 10;
                        if (refLine.Contains("NEST ARB MASTER")) IdPortfolio = 38;
                        if (refLine.Contains("NEST MILE HIGH MASTER")) IdPortfolio = 43;
                        if (refLine.Contains("NEST MH OVERSEAS FUND")) IdPortfolio = 43;
                        if (refLine.Contains("NEST HEDGE MASTER FIM")) IdPortfolio = 60;


                        if (IdPortfolio == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("Portfolio not found! File Not imported.", "Import failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                            return;
                        }

                        string PortName = "";

                        using (newNestConn curConn = new newNestConn())
                        {
                            PortName = curConn.Execute_Query_String("SELECT Port_Name FROM dbo.Tb002_Portfolios WHERE Id_Portfolio=" + IdPortfolio);


                            System.Windows.Forms.DialogResult userConfirmation = System.Windows.Forms.MessageBox.Show("Importing the following file: \r\n\r\nPortfolio:\t" + PortName + "\r\nDate:\t " + RefDate.ToString("dd-MMM-yy") + "\r\n\r\nDo you comfirm?", "Mellon File Import", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

                            if (userConfirmation != System.Windows.Forms.DialogResult.OK)
                                return;

                            curConn.ExecuteNonQuery("EXEC NESTIMPORT.dbo.[Proc_DeleteMellonAnaliseFileData]'" + RefDate.ToString("yyyy-MM-dd") + "'," + IdPortfolio);
                        }
                    }
                    else if (curLine == "Patrimônio") { InsertMode = "NAV"; sr.ReadLine(); }
                    else if (curLine == "Benchmark") { InsertMode = "BENCH"; sr.ReadLine(); }
                    else if (curLine == "Ação") { InsertMode = "ACAO"; sr.ReadLine(); }
                    else if (curLine == "Opção") { InsertMode = "OPCAO"; sr.ReadLine(); }
                    else if (curLine == "Fundo") { InsertMode = "FUND"; sr.ReadLine(); }
                    else if (curLine == "Renda Fixa") { InsertMode = "RFIXA"; sr.ReadLine(); }
                    else if (curLine == "Caixa") { InsertMode = "CAIXA"; sr.ReadLine(); }
                    else if (curLine == "Rentabilidade") { InsertMode = "RENT"; sr.ReadLine(); }
                    else if (curLine == "BDRs") { InsertMode = "BDR"; sr.ReadLine(); }
                    else if (curLine == "Futuro") { InsertMode = "FUT"; sr.ReadLine(); }

                    else
                    {
                        if (InsertMode == "NAV")
                            InsertString = GetAnaliticNAVSQL(curFileName, RefDate, IdPortfolio, curLine);

                        //if (InsertMode == "BENCH")
                        //    InsertString = GetAnaliticBenchSQL(curFileName, RefDate, IdPortfolio, curLine);

                        //if (InsertMode == "ACAO")
                        //    InsertString = GetAnaliticAcaoSQL(curFileName, RefDate, IdPortfolio, curLine);

                        //if (InsertMode == "OPCAO")
                        //    InsertString = GetAnaliticOpcaoSQL(curFileName, RefDate, IdPortfolio, curLine);

                        //if (InsertMode == "FUND")
                        //    InsertString = GetAnaliticFundSQL(curFileName, RefDate, IdPortfolio, curLine);

                        //if (InsertMode == "FUT")
                        //    InsertString = GetAnaliticFutureSQL(curFileName, RefDate, IdPortfolio, curLine);

                        //if (InsertMode == "RFIXA")
                        //    InsertString = GetAnaliticRFSQL(curFileName, RefDate, IdPortfolio, curLine);

                        //if (InsertMode == "CAIXA")
                        //    InsertString = GetAnaliticCaixaSQL(curFileName, RefDate, IdPortfolio, curLine);

                        //if (InsertMode == "RENT")
                        //    InsertString = GetAnaliticRentSQL(curFileName, RefDate, IdPortfolio, curLine);

                        //if (InsertMode == "BDR")
                        //    InsertString = GetAnaliticBDRSQL(curFileName, RefDate, IdPortfolio, curLine);


                    }

                    if (InsertString.Length > 10)
                    {
                        using (newNestConn curConn = new newNestConn())
                        {
                            curConn.ExecuteNonQuery(InsertString);
                            InsertString = "";
                        }
                    }
                }
            }
            sr.Close();

            if (IdPortfolio == 0)
            {
                System.Windows.Forms.MessageBox.Show("There was an error reading the file. File Not imported.", "Import failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("File:\r\n\r\n" + curFileName + "\r\n\r\nimported successfully.", "File Imported", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private string GetAnaliticNAVSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray.Length > 4)
            {
                SQLInsert = "'" + Convert.ToDateTime(InsArray[0]).ToString("yyyy-MM-dd") + "'," + ifnull(InsArray[1], 0) + "," + ifnull(InsArray[2], 0) + "," + ifnull(InsArray[3], 0) + "," + ifnull(InsArray[4], 0) + "";

                return "INSERT INTO [NESTIMPORT].[dbo].[Tb_Mellon_Analise_Patrimonio] SELECT  '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;

            }


            return "0";
        }

        private string GetAnaliticBenchSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray.Length > 3)
            {
                SQLInsert = ifnull(InsArray[0], 0) + "," + ifnull(InsArray[4], 0) + "," + ifnull(InsArray[5], 0) + "," + ifnull(InsArray[5], 0) + "," + ifnull(InsArray[5], 0) + "";

                return "INSERT INTO [NESTIMPORT].[dbo].[Tb_Mellon_Analise_Benchmark] SELECT  '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;

            }


            return "0";
        }

        private string GetAnaliticAcaoSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                           ifnull(InsArray[1], 0) + ", " +
                           ifnull(InsArray[2], 0) + ", " +
                           ifnull(InsArray[3], 0) + ", " +
                           ifnull(InsArray[4], 0) + ", " +
                           ifnull(InsArray[5], 0) + ", " +
                           InsArray[6].Replace("%", "") + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Analise_Acao SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetAnaliticOpcaoSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                           ifnull(InsArray[1], 0) + ", " +
                            ifnull(InsArray[2], 0) + ", " +
                            ifnull(InsArray[3], 0) + ", " +
                            ifnull(InsArray[4], 0) + ", " +
                            ifnull(InsArray[5], 0) + ", " +
                            InsArray[6].Replace("%", "") + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Analise_Opcao SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetAnaliticFutureSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                             "'" + Convert.ToDateTime(InsArray[1]).ToString("yyyy-MM-dd") + "'," +
                           "'" + InsArray[2] + "', " +
                           ifnull(InsArray[3], 0) + ", " +
                           ifnull(InsArray[4], 0) + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Analise_Futuro SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetAnaliticFundSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                             ifnull(InsArray[1], 0) + ", " +
                             ifnull(InsArray[2], 0) + ", " +
                             ifnull(InsArray[3], 0) + ", " +
                            ifnull(InsArray[4], 0) + ", " +
                            InsArray[5].Replace("%", "") + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Analise_Fundos SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetAnaliticRFSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            "'" + InsArray[1].ToString().Trim() + "', " +
                            "'" + Convert.ToDateTime(InsArray[2]).ToString("yyyy-MM-dd") + "'," +
                            "'" + InsArray[3] + "', " +
                            "'" + InsArray[4] + "', " +
                            "'" + InsArray[5] + "', " +
                             "'" + Convert.ToDateTime(InsArray[6]).ToString("yyyy-MM-dd") + "'," +
                             "'" + Convert.ToDateTime(InsArray[7]).ToString("yyyy-MM-dd") + "'," +
                            ifnull(InsArray[8], 0) + "," +
                            ifnull(InsArray[9], 0) + "," +
                            ifnull(InsArray[10], 0) + "," +
                            ifnull(InsArray[11], 0) + "," +
                            InsArray[12].Replace("%", "") + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Analise_RendaFixa SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetAnaliticCaixaSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            ifnull(InsArray[1], 0) + "," +
                            ifnull(InsArray[2], 0) + "," +
                            ifnull(InsArray[3], 0) + "," +
                            ifnull(InsArray[4], 0) + "," +
                            InsArray[5].Replace("%", "") + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Analise_Caixa SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetAnaliticRentSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            "'" + InsArray[1] + "', " +
                            ifnull(InsArray[2], 0).ToString().Replace("%", "") + "," +
                            ifnull(InsArray[3], 0).ToString().Replace("%", "") + "," +
                            ifnull(InsArray[4], 0).ToString().Replace("%", "") + "," +
                            ifnull(InsArray[5], 0).ToString().Replace("%", "") + "," +
                            ifnull(InsArray[6], 0).ToString().Replace("%", "") + "," +
                            ifnull(InsArray[7], 0).ToString().Replace("%", "") + "," +
                            ifnull(InsArray[8], 0).ToString().Replace("%", "") + "," +
                            ifnull(InsArray[9], 0).ToString().Replace("%", "") + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Analise_Rentabilidade SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

        private string GetAnaliticBDRSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                            ifnull(InsArray[1], 0) + "," +
                            ifnull(InsArray[2], 0) + "," +
                            ifnull(InsArray[3], 0) + "," +
                            ifnull(InsArray[4], 0) + "," +
                            ifnull(InsArray[5], 0) + "," +
                            ifnull(InsArray[6], 0).ToString().Replace("%", "") + ";";

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Analise_BDRs SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

    }
    #endregion

    #region BTG
    public class BTG
    {
        object[,] BtgArray;
        old_Conn curConn = new old_Conn();

        public void ImportFileBtg(string curFilePathAndName)
        {
            string curFileName = curFilePathAndName.Substring(curFilePathAndName.LastIndexOf('\\') + 1);
            Encoding currentEncoding = Encoding.GetEncoding(1252);

            Microsoft.Office.Interop.Excel.Application ExcelObject = new Microsoft.Office.Interop.Excel.Application();

            Workbook BtgWorkbook = ExcelObject.Workbooks.Open(curFilePathAndName, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true);

            Console.WriteLine(BtgWorkbook.Name.ToString());

            Sheets Btgsheets = BtgWorkbook.Worksheets;

            int CountBtgSheets = BtgWorkbook.Sheets.Count;

            for (int sheetNum = 1; sheetNum < CountBtgSheets + 1; sheetNum++)
            {
                Worksheet Btgsheet = (Worksheet)BtgWorkbook.Sheets[sheetNum];

                Range BtgRange = Btgsheet.UsedRange;
                BtgArray = (object[,])BtgRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

                ReadBtgArray(BtgWorkbook.Name);
            }
            BtgWorkbook.Close();
            ExcelObject.Quit();
        }

        void ReadBtgArray(string curFileName)
        {
            string InsertMode = "";
            string tempString = "";
            string InsertString = "";

            DateTime RefDate = Convert.ToDateTime(BtgArray[6, 2].ToString());
            int IdPortfolio = 4;

            string stringCheck;

            stringCheck = "SELECT COUNT(*) FROM NESTIMPORT.dbo.Tb_BTG_Portfolio WHERE RefDate='" + RefDate.ToString("yyyy-MM-dd") + "' and IdPortfolio=4";
            int Counter = 0;

            using (newNestConn curConn = new newNestConn())
            {
                Counter = curConn.Return_Int(stringCheck);
            }
            if (Counter != 0)
            {
                System.Windows.Forms.DialogResult userConfirmation = System.Windows.Forms.MessageBox.Show("Importing the following file: \r\n\r\nPortfolio:\t Nest Fund \r\nDate:\t " + RefDate.ToString("dd-MMM-yy") + "\r\n\r\nDo you comfirm?", "Mellon File Import", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

                if (userConfirmation != System.Windows.Forms.DialogResult.OK)
                    return;

                curConn.ExecuteNonQuery("EXEC NESTIMPORT.dbo.[Proc_DeleteBTGFileData] '" + curFileName + "'");
            }

            for (int Count = 1; Count < BtgArray.GetLength(0) + 1; Count++)
            {
                if (BtgArray[Count, 1] != null)
                {
                    if (BtgArray[Count, 1].ToString() == "Portfolio") //
                    { InsertMode = "Portfolio"; }
                    if (BtgArray[Count, 1].ToString() == "NAV & Shares")//
                    { InsertMode = "NavShare"; }
                    if (BtgArray[Count, 1].ToString() == "Performance")//
                    { InsertMode = "Performance"; }
                    if (BtgArray[Count, 1].ToString() == "NEST FUND LTD")//
                    { InsertMode = "NestFund"; }
                    if (BtgArray[Count, 1].ToString() == "Brazilian Account")//
                    { InsertMode = "BRAccount"; }
                    if (BtgArray[Count, 1].ToString() == "Past Due")//
                    { InsertMode = "PastDue"; }
                    if (BtgArray[Count, 1].ToString() == "Payable / Receivable Brazil")//
                    { InsertMode = "PayRecBR"; }
                    if (BtgArray[Count, 1].ToString() == "Payable / Receivable Offshore")//
                    { InsertMode = "PayRecUSD"; }
                    if (BtgArray[Count, 1].ToString() == "Stock Borrowing & Lending")//
                    { InsertMode = "StockBorrLend"; }
                    if (BtgArray[Count, 1].ToString() == "Expenses")//
                    { InsertMode = "Expenses"; }
                    if (BtgArray[Count, 1].ToString() == "Money Markets")//
                    { InsertMode = "MoneyMkt"; }
                    if (BtgArray[Count, 1].ToString() == "Brazilian Stocks")//
                    { InsertMode = "BRStocks"; }
                    if (BtgArray[Count, 1].ToString() == "Brazilian Stock Options")//
                    { InsertMode = "BRStockOps"; }
                    if (BtgArray[Count, 1].ToString() == "Stocks & ADRs")//
                    { InsertMode = "StocksADR"; }
                    if (BtgArray[Count, 1].ToString() == "Stock & ADR Options")//
                    { InsertMode = "StocksADROps"; }
                    if (BtgArray[Count, 1].ToString() == "Brazilian Dividends")//
                    { InsertMode = "BRDivid"; }
                    if (BtgArray[Count, 1].ToString() == "Brazilian Bonds")//
                    { InsertMode = "BRBonds"; }
                    if (BtgArray[Count, 1].ToString() == "Bonds")//
                    { InsertMode = "Bonds"; }
                    if (BtgArray[Count, 1].ToString() == "Brazilian Futures")//
                    { InsertMode = "BRFuts"; }
                    if (BtgArray[Count, 1].ToString() == "Futures")//
                    { InsertMode = "Futs"; }
                    if (BtgArray[Count, 1].ToString() == "Future Options")//
                    { InsertMode = "FutOps"; }
                    if (BtgArray[Count, 1].ToString() == "Funds")//
                    { InsertMode = "Funds"; }
                    if (BtgArray[Count, 1].ToString() == "General Information")//
                    { InsertMode = "GenOps"; }
                }

                if (InsertMode != "")
                {

                    if (InsertMode == "Portfolio")
                    {
                        tempString = GetPortfolioSQL(curFileName, RefDate, IdPortfolio, Count);
                    }
                    if (InsertMode == "BRStocks")
                    {
                        tempString = GetBRStocksSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "BRStockOps")
                    {
                        tempString = GetBRStockOpsSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "StocksADR")
                    {
                        tempString = GetStocksADRSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "StocksADROps")
                    {
                        tempString = GetStocksADROpsSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "NavShare")
                    {
                        tempString = GetNavShareSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "Performance")
                    {
                        tempString = GetPerformanceSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "NestFund")
                    {
                        tempString = GetNestFundSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "BRAccount")
                    {
                        tempString = GetBRAccountQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "PastDue")
                    {
                        tempString = GetPastDueQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "PayRecBR")
                    {
                        tempString = GetPayRecBRQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "PayRecUSD")
                    {
                        tempString = GetPayRecUSDSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "StockBorrLend")
                    {
                        tempString = GetStockBorrLendSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "Expenses")
                    {
                        tempString = GetExpensesSQL(curFileName, RefDate, IdPortfolio, Count);
                    }
                    if (InsertMode == "MoneyMkt")
                    {
                        tempString = GetMoneyMktSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "BRDivid")
                    {
                        tempString = GetBRDividSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "BRBonds")
                    {
                        tempString = GetBRBondsSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "Bonds")
                    {
                        tempString = GetBondsSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "BRFuts")
                    {
                        tempString = GetBRFutsSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "Futs")
                    {
                        tempString = GetFutsSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "FutOps")
                    {
                        tempString = GetFutOpsSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "Funds")
                    {
                        tempString = GetFundsSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    if (InsertMode == "GenOps")
                    {
                        tempString = GetGenOpsSQL(curFileName, RefDate, IdPortfolio, Count);
                    }

                    //Console.WriteLine(tempString);
                    InsertString += tempString;
                }
            }
            int result = 0;
            try
            {
                if (InsertString.Length > 10)
                {
                    using (newNestConn curConn = new newNestConn())
                    {
                        result = curConn.ExecuteNonQuery(InsertString);
                        tempString = "";
                    }
                }
                if (result == 0 || result == -1)
                {
                    System.Windows.Forms.MessageBox.Show("There was an error reading the file. File Not imported.", "Import failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("File:\r\n\r\n" + curFileName + "\r\n\r\nimported successfully.", "File Imported", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                }

            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("There was an error reading the file. File Not imported.", "Import failed", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }



        }

        object ifnull(object checkval, object retval)
        {
            if (checkval != null)
            {
                return checkval;
            }
            else
            {
                return retval;
            }
        }

        string GetPortfolioSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 2].GetType() == typeof(double) && BtgArray[indexLine, 1] != null)
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    "," + BtgArray[indexLine, 2].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_Portfolio] " +
                    " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetBRStocksSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";
            string PriceBRL = "";
            string AmountBRL = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 5].GetType() == typeof(double))
                {
                    if (BtgArray[indexLine, 9] != null) { PriceBRL = BtgArray[indexLine, 9].ToString().Replace(",", "."); } else { PriceBRL = "0"; }
                    if (BtgArray[indexLine, 10] != null) { AmountBRL = BtgArray[indexLine, 10].ToString().Replace(",", "."); } else { AmountBRL = "0"; }

                    StringFields = "'" + BtgArray[indexLine, 1] +
                    "','" + BtgArray[indexLine, 2].ToString() +
                    "','" + BtgArray[indexLine, 3].ToString() +
                    "','" + BtgArray[indexLine, 4].ToString() +
                    "'," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8].ToString().Replace(",", ".") +
                    "," + ifnull(BtgArray[indexLine, 9], 0).ToString().Replace(",", ".") +
                    "," + ifnull(BtgArray[indexLine, 10], 0).ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 11].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 12].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 13].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 14].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_BrazilianStocks] " +
                    " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetBRStockOpsSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 3] != null)
            {
                if (BtgArray[indexLine, 5].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] +
                    "','" + ifnull(BtgArray[indexLine, 2], "").ToString() +
                    "','" + BtgArray[indexLine, 3].ToString() +
                    "','" + BtgArray[indexLine, 4].ToString() +
                    "'," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + ifnull(BtgArray[indexLine, 6], 0).ToString().Replace(",", ".") +
                    "," + ifnull(BtgArray[indexLine, 7], 0).ToString().Replace(",", ".") +
                    "," + ifnull(BtgArray[indexLine, 8], 0).ToString().Replace(",", ".") +
                    "," + ifnull(BtgArray[indexLine, 9], 0).ToString().Replace(",", ".") +
                    "," + ifnull(BtgArray[indexLine, 10], 0).ToString().Replace(",", ".") +
                    "," + ifnull(BtgArray[indexLine, 11], 0).ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_BrazilianStockOptions] " +
                    " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetStocksADRSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 3] != null)
            {
                if (BtgArray[indexLine, 6].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] +
                        "','" + BtgArray[indexLine, 2].ToString() +

                        "','" + BtgArray[indexLine, 3].ToString() +
                    "','" + ifnull(BtgArray[indexLine, 4], "").ToString() +
                    "','" + BtgArray[indexLine, 5].ToString() +
                    "'," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 9].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 10].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 11].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 12].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 13].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 14].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_StocksADRs] " +
                    " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetStocksADROpsSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";
            string[] SplitTicker;
            string LBTicker = "";
            DateTime tempDate;
            string tempType = "";
            if (BtgArray[indexLine, 4] != null)
            {
                if (BtgArray[indexLine, 6].GetType() == typeof(double))
                {
                    SplitTicker = BtgArray[indexLine, 4].ToString().Split(' ');

                    tempDate = DateTime.Parse(SplitTicker[SplitTicker.Length - 4] + " " + SplitTicker[SplitTicker.Length - 3] + " " + SplitTicker[SplitTicker.Length - 2]);

                    if (SplitTicker[SplitTicker.Length - 6].Trim() == "PUT") { tempType = "P"; }
                    if (SplitTicker[SplitTicker.Length - 6].Trim() == "CALL") { tempType = "C"; }

                    LBTicker = SplitTicker[0].Trim() + tempDate.ToString("MM") + tempDate.ToString("yy") + tempType + (double.Parse(SplitTicker[SplitTicker.Length - 5].Replace(".", ","))).ToString("#.##,##").Replace(",", ".");

                    StringFields = "'" + BtgArray[indexLine, 1] +
                    "','" + ifnull(BtgArray[indexLine, 2], "").ToString() +
                    "','" + ifnull(BtgArray[indexLine, 3], "").ToString() +
                    "','" + BtgArray[indexLine, 4].ToString() +
                    "','" + BtgArray[indexLine, 5].ToString() +
                    "'," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 9].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 10].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 11].ToString().Replace(",", ".") +
                    ",'" + LBTicker + "'";

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_StocksADROptions] " +
                    " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetNavShareSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 1] != null)
            {
                if (BtgArray[indexLine, 1].GetType() == typeof(double))
                {
                    StringFields = BtgArray[indexLine, 1].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 2].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_NAVShares] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetPerformanceSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 2].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    "," + BtgArray[indexLine, 2].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8].ToString().Replace(",", ".");


                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_Performance] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetNestFundSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 2].GetType() == typeof(double))
                {
                    StringFields = BtgArray[indexLine, 2].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_NestFundLTD] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetBRAccountQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 2].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    "," + BtgArray[indexLine, 2].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_BrazilianAccount] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetPastDueQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 3].GetType() == typeof(double))
                {
                    //  Console.WriteLine(DateTime.Parse(BtgArray[indexLine, 2].ToString()).ToString("yyyy-MM-dd"));

                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + DateTime.Parse(BtgArray[indexLine, 2].ToString()).ToString("yyyy-MM-dd") +
                    "'," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_PastDue] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetPayRecBRQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 2].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    "," + BtgArray[indexLine, 2].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_PayableReceivableBrazil] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetPayRecUSDSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 3].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + BtgArray[indexLine, 2].ToString() +
                    "'," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_PayableReceivableOffshore] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetStockBorrLendSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 2].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    "," + BtgArray[indexLine, 2].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_StockBorrowingLending] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetExpensesSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 4] != null)
            {
                if (BtgArray[indexLine, 4].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + DateTime.Parse(ifnull(BtgArray[indexLine, 2], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "','" + DateTime.Parse(ifnull(BtgArray[indexLine, 3], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "'," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 6].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_Expenses] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetMoneyMktSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 3].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + BtgArray[indexLine, 2].ToString() +
                    "'," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 6].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_MoneyMarkets] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetBRDividSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 4].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + BtgArray[indexLine, 2].ToString() +
                    "','" + DateTime.Parse(ifnull(BtgArray[indexLine, 3], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "'," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_BrazilianDividends] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetBRBondsSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 5].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + BtgArray[indexLine, 2].ToString() +
                    "','" + DateTime.Parse(ifnull(BtgArray[indexLine, 3], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "','" + DateTime.Parse(ifnull(BtgArray[indexLine, 4], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "'," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 9].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 10].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_BrazilianBonds] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetBondsSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 3] != null)
            {
                if (BtgArray[indexLine, 6].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + BtgArray[indexLine, 2].ToString() +
                    "','" + BtgArray[indexLine, 3].ToString() +
                    "','" + DateTime.Parse(ifnull(BtgArray[indexLine, 4], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "','" + DateTime.Parse(ifnull(BtgArray[indexLine, 5], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "'," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 9].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 10].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_Bonds] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetBRFutsSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 4].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + BtgArray[indexLine, 2].ToString() +
                    "'," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 9].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 10].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_BrazilianFutures] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetFutsSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            //Alterado devido modificação no layout do arquivo de importação 
            //(foi adicionado 1 em cada uma das posições do vetor à partir do campo index 3)
            //18-04-2012

            if (BtgArray[indexLine, 3 + 1] != null)
            {
                if (BtgArray[indexLine, 5 + 1].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + ifnull(BtgArray[indexLine, 2], "").ToString() +
                    "','" + DateTime.Parse(ifnull(BtgArray[indexLine, 3 + 1], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "','" + DateTime.Parse(ifnull(BtgArray[indexLine, 4 + 1], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "'," + BtgArray[indexLine, 5 + 1].ToString().Replace(",", ".") +
                    "," + ifnull(BtgArray[indexLine, 6 + 1], 0).ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7 + 1].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8 + 1].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 9 + 1].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_Futures] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }


        string GetFutOpsSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";
            string[] SplitTicker;
            //DateTime tempDate;
            string LBTicker = "";
            string tempType = "";
            string tempTicker = "";

            if (BtgArray[indexLine, 5] != null)
            {
                if (BtgArray[indexLine, 5].GetType() == typeof(double))
                {
                    SplitTicker = BtgArray[indexLine, 1].ToString().Split(' ');

                    //tempDate = DateTime.Parse(SplitTicker[SplitTicker.Length - 4] + " " + SplitTicker[SplitTicker.Length - 3] + " " + SplitTicker[SplitTicker.Length - 2]);

                    if (BtgArray[indexLine, 1].ToString().IndexOf("PUT") != -1) { tempType = "P"; }
                    if (BtgArray[indexLine, 1].ToString().IndexOf("CALL") != -1) { tempType = "C"; }

                    if (SplitTicker[0].Trim().ToString() == "USM1") { tempTicker = "USL"; }
                    if (SplitTicker[0].Trim().ToString() == "FVM1") { tempTicker = "TY"; }

                    LBTicker = "OPF " + tempTicker + " M1" + tempType + double.Parse(SplitTicker[SplitTicker.Length - 5].Replace(".", ",").ToString()).ToString("####");


                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + ifnull(BtgArray[indexLine, 2], "").ToString() +
                    "','" + DateTime.Parse(ifnull(BtgArray[indexLine, 3], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "','" + DateTime.Parse(ifnull(BtgArray[indexLine, 4], "1900-01-01").ToString()).ToString("yyyy-MM-dd") +
                    "'," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 9].ToString().Replace(",", ".") +
                    ",'" + LBTicker.ToString() + "'";

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_FuturesOption] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetFundsSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 3].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    ",'" + ifnull(BtgArray[indexLine, 2], "").ToString() +
                    "'," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 5].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 6].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 7].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 8].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 9].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_Funds] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        string GetGenOpsSQL(string curFileName, DateTime RefDate, int IdPortfolio, int indexLine)
        {
            string StringSQL = "";
            string StringFields = "";

            if (BtgArray[indexLine, 2] != null)
            {
                if (BtgArray[indexLine, 3].GetType() == typeof(double))
                {
                    StringFields = "'" + BtgArray[indexLine, 1] + "'" +
                    "," + BtgArray[indexLine, 2].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 3].ToString().Replace(",", ".") +
                    "," + BtgArray[indexLine, 4].ToString().Replace(",", ".");

                    StringSQL = " INSERT INTO [NESTIMPORT].[dbo].[Tb_BTG_GeneralInformation] " +
                     " SELECT '" + curFileName.ToString() + "'" + ",'" + RefDate.ToString("yyyy-MM-dd") + "'" + "," + IdPortfolio + "," + StringFields;

                    return StringSQL;
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
    }
    #endregion

    #region Itau
    public class Itau : IDisposable
    {
        ~Itau()
        {
            this.Dispose();
            curConn.Dispose();
        }

        public Itau(string curFilePathAndName)
        {
            this.curFileName = curFilePathAndName.Substring(curFilePathAndName.LastIndexOf('\\') + 1);
            workBookConnect(curFilePathAndName);
            ItauImport(curFilePathAndName);
            curConn.Dispose();
        }

        private string curFileName;
        private OleDbConnection shtConn = new OleDbConnection();
        private newNestConn curConn = new newNestConn();
        private Microsoft.Office.Interop.Excel.Application ExcelObject;
        private DateTime refDate;
        private Workbook ItauWorkbook;

        public void ItauImport(string curFilePathAndName)
        {
            ExcelObject = new Microsoft.Office.Interop.Excel.Application();
            refDate = new DateTime(1900, 01, 01);
            ItauWorkbook = ExcelObject.Workbooks.Open(curFilePathAndName, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true);
            Sheets ItauWorksheets = ItauWorkbook.Worksheets;

            if ((ItauWorksheets["Patrimonio_Cotas"].Cells[1, 5].Value2.ToString() != "") && (ItauWorksheets["Patrimonio_Cotas"].Cells[1, 5].Value2.ToString() == "Data Posição"))
            {
                if (DateTime.TryParse(ItauWorksheets["Patrimonio_Cotas"].Cells[2, 5].Value2, out refDate))
                {
                    bool checkExistRefDate = validaRefDateExist(refDate, curFileName);
                    if (checkExistRefDate)
                    {
                        System.Windows.Forms.DialogResult userConfirmation = System.Windows.Forms.MessageBox.Show("Importing the following file: \r\n\r\nPortfolio:\t Nest Prev \r\nDate:\t " + refDate.ToString("dd-MMM-yy") + "\r\n\r\nDo you confirm?", "Itau File Import", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question);

                        if (userConfirmation != System.Windows.Forms.DialogResult.OK)
                        {
                            MessageBox.Show("File not imported"); return;
                        }
                        else
                        {
                            curConn.ExecuteNonQuery("EXEC NESTIMPORT.dbo.[Proc_DeleteItauFileData] '" + curFileName + "'");
                            ImportFile(curFilePathAndName, ItauWorksheets);
                            MessageBox.Show("File Imported");
                        }
                    }
                    else
                    {
                        ImportFile(curFilePathAndName, ItauWorksheets);

                        MessageBox.Show("File Imported");
                    }
                }
                else { MessageBox.Show("Data de referência inválida"); }
            }
            else { MessageBox.Show("Arquivo sem data de referência"); }

        }

        public void ImportFile(string curFilePathAndName, Sheets ItauWorksheets)
        {
            try
            {


                for (int i = 1; i < (ItauWorkbook.Worksheets.Count) + 1; i++)
                {
                    if (ItauWorksheets[i].Name() == "Acoes") { insAcoes(ItauWorksheets, ItauWorkbook.Name.ToString(), refDate, i); }
                    else if (ItauWorksheets[i].Name() == "Emprestimo_De_Acoes") { insEmprestAcoes(ItauWorksheets, ItauWorkbook.Name.ToString(), refDate, i); }
                    else if (ItauWorksheets[i].Name() == "Opcoes") { insOpcoes(ItauWorksheets, ItauWorkbook.Name.ToString(), refDate, i); }
                    else if (ItauWorksheets[i].Name() == "Futuros") { insFuturos(ItauWorksheets, ItauWorkbook.Name.ToString(), refDate, i); }
                    else if (ItauWorksheets[i].Name() == "Renda_Fixa") { insRendaFixa(ItauWorksheets, ItauWorkbook.Name.ToString(), refDate, i); }
                    else if (ItauWorksheets[i].Name() == "Conta_Corrente") { insContaCorrente(ItauWorksheets, ItauWorkbook.Name.ToString(), refDate, i); }
                    else if (ItauWorksheets[i].Name() == "Contas_Pagar_Receber") { insContasPagarReceber(ItauWorksheets, ItauWorkbook.Name.ToString(), refDate, i); }
                    else if (ItauWorksheets[i].Name() == "Tesouraria") { insTesouraria(ItauWorksheets, ItauWorkbook.Name.ToString(), refDate, i); }
                    else if (ItauWorksheets[i].Name() == "Patrimonio_Cotas") { insPatrimonioCotas(ItauWorksheets, ItauWorkbook.Name.ToString(), refDate, i); }

                }



            }

            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            ItauWorkbook.Close();
            ExcelObject.Quit();
            Dispose();
        }

        public void insAcoes(Sheets _ItauWorksheets, string _ItauWorkbook, DateTime _refDate, int _i)
        {
            System.Text.StringBuilder strBuilder;
            System.Data.DataTable dtSheet = ExecutarConsulta(_ItauWorksheets[_i].Name.ToString());


            if (dtSheet.Rows.Count > 0)
            {
                for (int i = 0; i <= dtSheet.Rows.Count - 1; i++)
                {
                    if (dtSheet.Rows[i] != null && dtSheet.Rows[i]["Carteira/Fundo"].ToString() != "")
                    {
                        strBuilder = new StringBuilder();

                        strBuilder.AppendFormat("INSERT [NESTIMPORT].[dbo].[Tb_Itau_Acoes] SELECT ISNULL('{0}',NULL),ISNULL('{1}',NULL),50,ISNULL('{2}',NULL),ISNULL('{3}',NULL),ISNULL('{4}',NULL),ISNULL({5},NULL),ISNULL({6},NULL),ISNULL({7},NULL),ISNULL({8},NULL),ISNULL({9},NULL),ISNULL({10},NULL),ISNULL({11},NULL),ISNULL({12},NULL),isnull({13},NULL),ISNULL('{14}',NULL),ISNULL({15},NULL),ISNULL({16},NULL),ISNULL({17},NULL),ISNULL({18},NULL),ISNULL({19},NULL)",
                                                                                                _ItauWorkbook.ToString(),
                                                                                                _refDate.ToString("yyyy-MM-dd"),
                                                                                                dtSheet.Rows[i]["Carteira/Fundo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Código"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Papel"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Qtde Disponível"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Qtde Bloqueada"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Qtde Total"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Custo Médio Corretagem"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Cotação"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Cotação/Custo Médio"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Custo Total"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Resultado"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor Mercado"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Primeiro Venc# Doador"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Qtd# Tomada"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Qtd# Doada"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Qtd# Própria"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/R#V#"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/Total"].ToString().Replace(",", "."));

                        curConn.ExecuteNonQuery(strBuilder.ToString());
                    }
                    strBuilder = null;
                }
            }
        }

        public void insEmprestAcoes(Sheets _ItauWorksheets, string _ItauWorkbook, DateTime _refDate, int _i)
        {
            System.Text.StringBuilder strBuilder;
            System.Data.DataTable dtSheet = ExecutarConsulta(_ItauWorksheets[_i].Name.ToString());

            if (dtSheet.Rows.Count > 0)
            {
                for (int i = 0; i <= dtSheet.Rows.Count - 1; i++)
                {
                    if (dtSheet.Rows[i] != null && dtSheet.Rows[i]["Carteira/Fundo"].ToString() != "")
                    {
                        strBuilder = new StringBuilder();

                        strBuilder.AppendFormat("INSERT [NESTIMPORT].[dbo].[Tb_Itau_Emprestimo_De_Acoes] SELECT ISNULL('{0}',NULL),ISNULL('{1}',NULL),50,ISNULL('{2}',NULL),ISNULL('{3}',NULL),ISNULL('{4}',NULL),ISNULL('{5}',NULL),ISNULL('{6}',NULL),ISNULL('{7}',NULL),ISNULL('{8}',NULL),ISNULL('{9}',NULL),ISNULL('{10}',NULL),ISNULL('{11}',NULL),ISNULL('{12}',NULL),ISNULL('{13}',NULL),ISNULL('{14}',NULL),ISNULL('{15}',NULL),ISNULL('{16}',NULL),ISNULL({17},NULL),ISNULL({18},NULL),ISNULL({19},NULL),ISNULL({20},NULL),ISNULL({21},NULL),ISNULL({22},NULL),ISNULL({23},NULL),ISNULL({24},NULL)",
                                                                                                _ItauWorkbook.ToString(),
                                                                                                _refDate.ToString("yyyy-MM-dd"),

                                                                                                dtSheet.Rows[i]["Carteira/Fundo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Cliente"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Tipo Operação"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Codigo Tipo Operação"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Periodicidade"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Liquidação Antecipada Doador"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Quantidade Dias Liquidação"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Operação SAC"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Contrato BTC"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Data Operação"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Data Vencimento"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Data Final Carência"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Corretora"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Papel"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Quantidade"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Preço Unitário"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor Financeiro"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% Rem"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor Mercado"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Remuneração"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Apropriação Remuneração"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Apropriação Taxas"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Apropriação Total"].ToString().Replace(",", "."));

                        curConn.ExecuteNonQuery(strBuilder.ToString());
                    }
                    strBuilder = null;
                }
            }
        }

        public void insOpcoes(Sheets _ItauWorksheets, string _ItauWorkbook, DateTime _refDate, int _i)
        {
            System.Text.StringBuilder strBuilder;
            System.Data.DataTable dtSheet = ExecutarConsulta(_ItauWorksheets[_i].Name.ToString());

            if (dtSheet.Rows.Count > 0)
            {
                for (int i = 0; i <= dtSheet.Rows.Count - 1; i++)
                {
                    if (dtSheet.Rows[i] != null && dtSheet.Rows[i]["Carteira/Fundo"].ToString() != "")
                    {
                        strBuilder = new StringBuilder();

                        strBuilder.AppendFormat("INSERT [NESTIMPORT].[dbo].[Tb_Itau_Opcoes] SELECT ISNULL('{0}',NULL),ISNULL('{1}',NULL),50,ISNULL('{2}',NULL),ISNULL('{3}',NULL),ISNULL('{4}',NULL),ISNULL('{5}',NULL),ISNULL('{6}',NULL),ISNULL('{7}',NULL),ISNULL('{8}',NULL),ISNULL({9},NULL),ISNULL('{10}',NULL),ISNULL({11},NULL),ISNULL({12},NULL),ISNULL({13},NULL),ISNULL({14},NULL),ISNULL({15},NULL),ISNULL({16},NULL),ISNULL({17},NULL),ISNULL({18},NULL)",
                                                                                                _ItauWorkbook.ToString(),
                                                                                                _refDate.ToString("yyyy-MM-dd"),

                                                                                                dtSheet.Rows[i]["Carteira/Fundo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Tipo estoque"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Código"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Papel"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Tipo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Corretora"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Praça "].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Exercicío"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Data Vencimento"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Quantidade Total"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Custo Médio Corretagem"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Cotação"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Custo Total"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Resultado"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor Mercado"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/R#V#"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/Total"].ToString().Replace(",", "."));

                        curConn.ExecuteNonQuery(strBuilder.ToString());
                    }
                    strBuilder = null;
                }
            }
        }

        public void insFuturos(Sheets _ItauWorksheets, string _ItauWorkbook, DateTime _refDate, int _i)
        {
            System.Text.StringBuilder strBuilder;
            System.Data.DataTable dtSheet = ExecutarConsulta(_ItauWorksheets[_i].Name.ToString());

            if (dtSheet.Rows.Count > 0)
            {
                for (int i = 0; i <= dtSheet.Rows.Count - 1; i++)
                {
                    if (dtSheet.Rows[i] != null && dtSheet.Rows[i]["Carteira/Fundo"].ToString() != "")
                    {
                        strBuilder = new StringBuilder();

                        strBuilder.AppendFormat("INSERT [NESTIMPORT].[dbo].[Tb_Itau_Futuros] SELECT ISNULL('{0}',NULL),ISNULL('{1}',NULL),50,ISNULL('{2}',NULL),ISNULL('{3}',NULL),ISNULL('{4}',NULL),ISNULL('{5}',NULL),ISNULL('{6}',NULL),ISNULL({7},NULL),ISNULL({8},NULL),ISNULL({9},NULL),ISNULL({10},NULL),ISNULL({11},NULL),ISNULL({12},NULL),ISNULL({13},NULL)",
                                                                                                _ItauWorkbook.ToString(),
                                                                                                _refDate.ToString("yyyy-MM-dd"),

                                                                                                dtSheet.Rows[i]["Carteira/Fundo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Tipo estoque"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Ativo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Vencimento"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Corretora"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Quantidade"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Ajuste equalização"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Ajuste valorização"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Preço do mercado"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor de Mercado"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/FU"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/Total"].ToString().Replace(",", "."));

                        curConn.ExecuteNonQuery(strBuilder.ToString());
                    }
                    strBuilder = null;
                }
            }
        }

        public void insRendaFixa(Sheets _ItauWorksheets, string _ItauWorkbook, DateTime _refDate, int _i)
        {
            System.Text.StringBuilder strBuilder;
            System.Data.DataTable dtSheet = ExecutarConsulta(_ItauWorksheets[_i].Name.ToString());

            if (dtSheet.Rows.Count > 0)
            {
                for (int i = 0; i <= dtSheet.Rows.Count - 1; i++)
                {
                    if (dtSheet.Rows[i] != null && dtSheet.Rows[i]["Carteira/Fundo"].ToString() != "")
                    {
                        strBuilder = new StringBuilder();

                        strBuilder.AppendFormat("INSERT [NESTIMPORT].[dbo].[Tb_Itau_Renda_Fixa] SELECT ISNULL('{0}',NULL),ISNULL('{1}',null),50, ISNULL('{2}',NULL),ISNULL('{3}',NULL),ISNULL('{4}',NULL),ISNULL('{5}',NULL),ISNULL('{6}',NULL),ISNULL('{7}',NULL),ISNULL('{8}',NULL),ISNULL({9},NULL),ISNULL({10},NULL),ISNULL('{11}',NULL),ISNULL('{12}',NULL),ISNULL({13},NULL),ISNULL({14},NULL),ISNULL({15},NULL),ISNULL({16},NULL),ISNULL({17},NULL),ISNULL({18},NULL),ISNULL({19},NULL),ISNULL({20},NULL),ISNULL('{21}',NULL)",
                                                                                                _ItauWorkbook.ToString(),
                                                                                                _refDate.ToString("yyyy-MM-dd"),

                                                                                                dtSheet.Rows[i]["Carteira/Fundo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Tipo estoque"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Código"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Nome"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Aplicação"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Emitente/Descrição"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Papel"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["TX# MTM % AA"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Taxa % AA"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Index"].ToString().Replace(",", "."),
                                                                                                Convert.ToDateTime(dtSheet.Rows[i]["Vencimento"]).ToString("yyyy-MM-dd"),
                                                                                                dtSheet.Rows[i]["Quantidade "].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["PU de Mercado"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor Aplicação"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor Bruto"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Impostos"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor Líquido"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/R#F#"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/Total"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Tipo Operação"].ToString().Replace(",", "."));

                        curConn.ExecuteNonQuery(strBuilder.ToString());
                    }
                    strBuilder = null;
                }
            }
        }

        public void insContaCorrente(Sheets _ItauWorksheets, string _ItauWorkbook, DateTime _refDate, int _i)
        {
            System.Text.StringBuilder strBuilder;
            System.Data.DataTable dtSheet = ExecutarConsulta(_ItauWorksheets[_i].Name.ToString());

            if (dtSheet.Rows.Count > 0)
            {
                for (int i = 0; i <= dtSheet.Rows.Count - 1; i++)
                {
                    if (dtSheet.Rows[i] != null && dtSheet.Rows[i]["Carteira/Fundo"].ToString() != "")
                    {
                        strBuilder = new StringBuilder();

                        strBuilder.AppendFormat("INSERT [NESTIMPORT].[dbo].[Tb_Itau_Conta_Corrente] SELECT ISNULL('{0}',NULL),ISNULL('{1}',null),50,ISNULL('{2}',NULL),ISNULL('{3}',NULL),ISNULL('{4}',NULL),ISNULL('{5}',NULL),ISNULL('{6}',NULL),ISNULL({7},NULL),ISNULL({8},NULL),ISNULL({9},NULL)",
                                                                                                _ItauWorkbook.ToString(),
                                                                                                _refDate.ToString("yyyy-MM-dd"),

                                                                                                dtSheet.Rows[i]["Carteira/Fundo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Tipo estoque"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Código"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["NM"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Instituição"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/CPR"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/Total"].ToString().Replace(",", "."));

                        curConn.ExecuteNonQuery(strBuilder.ToString());
                    }
                    strBuilder = null;
                }
            }
        }

        public void insContasPagarReceber(Sheets _ItauWorksheets, string _ItauWorkbook, DateTime _refDate, int _i)
        {
            System.Text.StringBuilder strBuilder;
            System.Data.DataTable dtSheet = ExecutarConsulta(_ItauWorksheets[_i].Name.ToString());

            if (dtSheet.Rows.Count > 0)
            {
                for (int i = 0; i <= dtSheet.Rows.Count - 1; i++)
                {
                    if (dtSheet.Rows[i] != null && dtSheet.Rows[i]["Carteira/Fundo"].ToString() != "")
                    {
                        strBuilder = new StringBuilder();

                        strBuilder.AppendFormat("INSERT [NESTIMPORT].[dbo].[Tb_Itau_Contas_Pagar_Receber] SELECT ISNULL('{0}',NULL),ISNULL('{1}',null),50,ISNULL('{2}',NULL),ISNULL('{3}',NULL),ISNULL('{4}',NULL),ISNULL({5},NULL),ISNULL({6},NULL),ISNULL({7},NULL)",
                                                                                                _ItauWorkbook.ToString(),
                                                                                                _refDate.ToString("yyyy-MM-dd"),

                                                                                                dtSheet.Rows[i]["Carteira/Fundo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Tipo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Descrição"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/CPR"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/Total"].ToString().Replace(",", "."));

                        curConn.ExecuteNonQuery(strBuilder.ToString());
                    }
                    strBuilder = null;
                }
            }
        }

        public void insTesouraria(Sheets _ItauWorksheets, string _ItauWorkbook, DateTime _refDate, int _i)
        {
            System.Text.StringBuilder strBuilder;
            System.Data.DataTable dtSheet = ExecutarConsulta(_ItauWorksheets[_i].Name.ToString());

            if (dtSheet.Rows.Count > 0)
            {
                for (int i = 0; i <= dtSheet.Rows.Count - 1; i++)
                {
                    if (dtSheet.Rows[i] != null && dtSheet.Rows[i]["Carteira/Fundo"].ToString() != "")
                    {
                        strBuilder = new StringBuilder();

                        strBuilder.AppendFormat("INSERT [NESTIMPORT].[dbo].[Tb_Itau_Tesouraria] SELECT ISNULL('{0}',NULL),ISNULL('{1}',null),50,ISNULL('{2}',NULL),ISNULL('{3}',NULL),ISNULL({4},NULL),ISNULL({5},NULL),ISNULL({6},NULL)",
                                                                                                _ItauWorkbook.ToString(),
                                                                                                _refDate.ToString("yyyy-MM-dd"),

                                                                                                dtSheet.Rows[i]["Carteira/Fundo"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Descrição"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["Valor"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/TES"].ToString().Replace(",", "."),
                                                                                                dtSheet.Rows[i]["% S/Total"].ToString().Replace(",", "."));

                        curConn.ExecuteNonQuery(strBuilder.ToString());
                    }
                    strBuilder = null;
                }
            }
        }

        public void insPatrimonioCotas(Sheets _ItauWorksheets, string _ItauWorkbook, DateTime _refDate, int _i)
        {
            string sqlIns = "";

            System.Data.DataTable dtSheet = ExecutarConsulta(_ItauWorksheets[_i].Name.ToString());

            if (dtSheet.Rows.Count > 0)
            {
                for (int i = 0; i <= dtSheet.Rows.Count - 1; i++)
                {
                    if (dtSheet.Rows[i] != null && dtSheet.Rows[i]["Carteira/Fundo"].ToString() != "")
                    {

                        sqlIns = "INSERT [NESTIMPORT].[dbo].[Tb_Itau_Patrimonio_Cotas] SELECT '" + _ItauWorkbook.ToString() + "','" + _refDate.ToString("yyyy-MM-dd") + "',50,'" + dtSheet.Rows[i]["Código da Carteira"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Carteira/Fundo"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Data Atual"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Atualização Carteira"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Data Posição"].ToString().Replace(",", ".") + "'," + dtSheet.Rows[i]["Patrimônio"].ToString().Replace(",", ".") + "," + dtSheet.Rows[i]["Valor da cota bruta de performace"].ToString().Replace(",", ".") + "," + dtSheet.Rows[i]["Quantidade de cotas (Bruta)"].ToString().Replace(",", ".") + "," + dtSheet.Rows[i]["Quantidade de cotas (Liq#)"].ToString().Replace(",", ".") + "," + dtSheet.Rows[i]["Vl# Cota Unitária (Bruta)"].ToString().Replace(",", ".") + "," + dtSheet.Rows[i]["Vl# Cota Unitária (Liq#)"].ToString().Replace(",", ".") + "," + dtSheet.Rows[i]["Quantidade de cotas"].ToString().Replace(",", ".") + "," + dtSheet.Rows[i]["Valor de cotas unitária"].ToString().Replace(",", ".") + ",'" + dtSheet.Rows[i]["Acoes"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Emprestimo_De_Acoes"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Opcoes"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Opcoes_Flexiveis"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Futuros"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Opcoes_Futuro"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Termo"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Renda_Fixa_CPR"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Renda_Fixa"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Outros_Ativos"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Outros_Fundos_De_Investimento"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["SWAP"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Conta_Corrente"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Operacoes_cambio"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Contas_Pagar_Receber"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Tesouraria"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Rentabilidade"].ToString().Replace(",", ".") + "','" + dtSheet.Rows[i]["Disclaimer"].ToString().Replace(",", ".") + "'";

                        curConn.ExecuteNonQuery(sqlIns.ToString());
                    }
                    sqlIns = "";
                }
            }
        }

        public System.Data.DataTable ExecutarConsulta(string _ItauWorkbook)
        {
            System.Data.DataTable dtExcel = new System.Data.DataTable();
            dtExcel.TableName = _ItauWorkbook;
            string query = "Select * from [" + _ItauWorkbook + "$]";
            OleDbDataAdapter data = new OleDbDataAdapter(query, shtConn);
            data.Fill(dtExcel);
            return dtExcel;
        }

        public bool validaRefDateExist(DateTime _refDate, string fileName)
        {
            string query = "Select count (*) from [NESTIMPORT].[DBO].[TB_ITAU_acoes] WHERE IdPortfolio = 50 and RefDate = '" + _refDate.ToString("yyyy-MM-dd") + "' and refFileName = '" + fileName.ToString() + "'";

            int conta = curConn.Return_Int(query);

            if (conta > 0)
            {
                return true;
            }
            else
                return false;
        }

        public void workBookConnect(string _curFilePathAndName)
        {
            shtConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + _curFilePathAndName + "';Extended Properties= 'Excel 8.0;HDR=Yes;IMEX=1'");
            shtConn.Open();
        }

        public void Dispose()
        {
            if (shtConn != null && shtConn.State != System.Data.ConnectionState.Closed)
            {
                shtConn.Close();
            }

            curConn.Dispose();

        }

    }
    #endregion

}


/*
Range excelRange;
                       
foreach (Worksheet curSheet in theWorkbook.Sheets)
{
if (curSheet.Name == "Carteira")
{
excelRange = curSheet.UsedRange;

foreach (Range curCell in excelRange.Cells)
{
string curValue = curCell.FormulaR1C1.ToString();
string test = curCell.Address;
bool isbold = (bool)curCell.Font.Bold;
string curColor = ((double)curCell.Interior.Color).ToString();
//if (curValue != "")
{
   Console.Write(test + "  - ");
   Console.WriteLine(curValue);
}
}
}
}
*/