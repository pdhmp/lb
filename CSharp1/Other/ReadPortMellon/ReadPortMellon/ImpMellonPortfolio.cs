using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NestDLL;

namespace ReadPortMellon
{
    class ImpMellonPortfolio
    {
        public void ImportFile(string curFilePathAndName)
        {
            StreamReader sr = new StreamReader(curFilePathAndName, Encoding.GetEncoding(1252));

            DateTime RefDate = new DateTime(1900, 01, 01);
            int IdPortfolio = 0;
            string curLine = "";
            string InsertMode = "NOINSERT";
            string InsertString = "";
            string curFileName = "";

            curFileName = curFilePathAndName.Substring(curFilePathAndName.LastIndexOf('\\') + 1);

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
                        if (refLine.Contains("NEST QUANT MASTER FIM")) IdPortfolio = 18;
                        if (refLine.Contains("NEST MULTIESTRATEGIA FIC DE FI")) IdPortfolio = 20;

                        if (IdPortfolio == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("Portfolio not found! File Not imported.");
                            return;
                        }
                    }
                    else if (curLine == "Ação") { InsertMode = "ACAO"; sr.ReadLine(); }
                    else if (curLine == "Fundo") { InsertMode = "FUNDO"; sr.ReadLine(); }
                    else if (curLine == "Opção") { InsertMode = "OPCAO"; sr.ReadLine(); }
                    else if (curLine == "Renda Fixa") { InsertMode = "RFIXA"; sr.ReadLine(); }
                    else if (curLine == "Empréstimo de Ações") { InsertMode = "EMPREST"; sr.ReadLine(); }
                    else if (curLine == "Contas a Pagar/Receber") { InsertMode = "CONTASPAG"; sr.ReadLine(); }
                    else if (curLine == "Tesouraria") { InsertMode = "TESOUR"; sr.ReadLine(); }
                    else
                    {
                        if (InsertMode == "ACAO") InsertString += GetAcaoSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "EMPREST") InsertString += GetEmprestSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "OPCAO") InsertString += GetOpcaoSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "FUNDO") InsertString += GetFundoSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "RFIXA") InsertString += GetRendaFixaSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "CONTASPAG") InsertString += GetContasPagSQL(curFileName, RefDate, IdPortfolio, curLine);
                        if (InsertMode == "TESOUR") InsertString += GetTesourariaSQL(curFileName, RefDate, IdPortfolio, curLine);
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
        }


        private string GetAcaoSQL(string curFileName, DateTime refDate, int IdPortfolio, string curLine)
        {
            string[] InsArray = curLine.Replace(",", "").Replace("(", "-").Replace(")", "").Split('\t');
            string SQLInsert = "";

            if (InsArray[0] != "Total")
            {
                SQLInsert = "'" + InsArray[0] + "', " +
                             "'" + InsArray[1] + "', " +
                            InsArray[2] + ", " +
                            InsArray[3] + ", " +
                            InsArray[4] + ", " +
                            InsArray[5] + ", " +
                            InsArray[6] + ", " +
                            InsArray[7] + ", " +
                            InsArray[8] + ", " +
                            InsArray[9] + ", " +
                            InsArray[10] + ", " +
                            InsArray[11] + ", " +
                            InsArray[12].Replace("%", "") + ", " +
                            InsArray[13].Replace("%", "") + ";";

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
                            InsArray[1] + ", " +
                            "'" + InsArray[2].Split('/')[2] + "-" + InsArray[2].Split('/')[1] + "-" + InsArray[2].Split('/')[0] + "', " +
                            "'" + InsArray[3].Split('/')[2] + "-" + InsArray[3].Split('/')[1] + "-" + InsArray[3].Split('/')[0] + "', " +
                            "'" + InsArray[4] + "', " +
                            "'" + InsArray[5] + "', " +
                            InsArray[6] + ", " +
                            InsArray[7] + ", " +
                            InsArray[8] + ", " +
                            InsArray[9].Replace("%", "") + ", " +
                            InsArray[10].Replace("%", "") + ", " +
                            InsArray[11] + ", " +
                            InsArray[12] + ", " +
                            InsArray[13] + ", " +
                            InsArray[14].Replace("%", "") + ", " +
                            InsArray[15].Replace("%", "") + ";";

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
                            InsArray[5] + ", " +
                            "'" + InsArray[6].Split('/')[2] + "-" + InsArray[6].Split('/')[1] + "-" + InsArray[6].Split('/')[0] + "', " +
                            InsArray[7] + ", " +
                            InsArray[8] + ", " +
                            InsArray[9] + ", " +
                            InsArray[10] + ", " +
                            InsArray[11] + ", " +
                            InsArray[12] + ", " +
                            InsArray[13] + ", " +
                            InsArray[14] + ", " +
                            InsArray[15].Replace("%", "") + ", " +
                            InsArray[16].Replace("%", "");

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Opcoes SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
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
                            InsArray[3] + ", " +
                            InsArray[4] + ", " +
                            InsArray[5] + ", " +
                            InsArray[6] + ", " +
                            InsArray[7] + ", " +
                            InsArray[8] + ", " +
                            InsArray[9] + ", " +
                            InsArray[10].Replace("%", "") + ", " +
                            InsArray[11].Replace("%", "");

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
                            InsArray[1] + ", " +
                            "'" + InsArray[2].Split('/')[2] + "-" + InsArray[2].Split('/')[1] + "-" + InsArray[2].Split('/')[0] + "', " +
                            "'" + InsArray[3] + "', " +
                            InsArray[4] + ", " +
                            InsArray[5].Replace("%", "") + ", " +
                            InsArray[6].Replace("%", "") + ", " +
                            InsArray[7].Replace("%", "") + ", " +
                            "'" + InsArray[8] + "', " +
                            "'" + InsArray[9].Split('/')[2] + "-" + InsArray[9].Split('/')[1] + "-" + InsArray[9].Split('/')[0] + "', " +
                            "'" + InsArray[10].Split('/')[2] + "-" + InsArray[10].Split('/')[1] + "-" + InsArray[10].Split('/')[0] + "', " +
                            InsArray[11] + ", " +
                            InsArray[12] + ", " +
                            InsArray[13] + ", " +
                            "0" + InsArray[14] + ", " +
                            InsArray[15] + ", " +
                            InsArray[16] + ", " +
                            InsArray[17] + ", " +
                            InsArray[18].Replace("%", "") + ", " +
                            InsArray[19].Replace("%", "");

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
                            InsArray[2] + ", " +
                            InsArray[3].Replace("%", "") + ", " +
                            InsArray[4].Replace("%", "");

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
                            InsArray[1] + ", " +
                            InsArray[2].Replace("%", "") + ", " +
                            InsArray[3].Replace("%", "");

                return "INSERT INTO NESTIMPORT.dbo.Tb_Mellon_Tesouraria SELECT '" + curFileName + "', '" + refDate.ToString("yyyy-MM-dd") + "', " + IdPortfolio + ", " + SQLInsert;
            }
            else
                return "";
        }

    }
}
