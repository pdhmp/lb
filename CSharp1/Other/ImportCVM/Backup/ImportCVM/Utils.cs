using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NestDLL;

namespace ImportCVM
{
    public class Utils
    {
        public class FileColumns
        {
            public List<string[]> colList = new List<string[]>();

            public FileColumns(string colFileLocation)
            {
                using (StreamReader thisReader = File.OpenText(colFileLocation))
                {
                    string curLine = "";
                    while ((curLine = thisReader.ReadLine()) != null)
                    {
                        colList.Add(curLine.Split('\t'));
                    }
                }
            }
        }


        public static List<string[]> ConvertLine(string curLine, FileColumns curColumns, bool resizeCols)
        {
            List<string[]> outLine = new List<string[]>();
            int prevMark = 0;
            int i = 0;

            // Check if file length matches definition
            int expLength = 0;

            for (i = 0; i < curColumns.colList.Count; i++)
            {
                expLength = expLength + int.Parse(curColumns.colList[i][1]);
            }

            if (curLine.Length < expLength || curLine.Length == 1661)
            {
                int iniPos = 0;
                for (i = 0; i < curColumns.colList.Count; i++)
                {
                    if (curColumns.colList[i][0] == "ATIVPRIN")
                    {
                        break;
                    }
                    iniPos = iniPos + int.Parse(curColumns.colList[i][1]);
                }

                int endPos = curLine.IndexOf("  ", iniPos);

                if (endPos - iniPos < 100)
                {
                    curColumns.colList[i][1] = "55";
                }

                //int a = curLine.Length - expLength;
            }

            if (curLine.Contains("Ci@mastra.com.br"))
            {
                //curColumns.colList[89][1] = "16";
            }

            try
            {
                for (i = 0; i < curColumns.colList.Count; i++)
                {
                    string[] curItem = new string[2];
                    curItem[0] = curColumns.colList[i][0];
                    curItem[1] = curLine.Substring(prevMark, int.Parse(curColumns.colList[i][1]));
                    outLine.Add(curItem);
                    prevMark = prevMark + int.Parse(curColumns.colList[i][1]);
                }
            }
            catch(Exception e)
            {
                int a = 0;
            }

            return outLine;

        }


    }
}
