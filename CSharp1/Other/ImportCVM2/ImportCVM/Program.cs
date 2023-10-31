using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Data;
using NestDLL;
using System.IO;

namespace ImportCVM
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            DateTime curDate = new DateTime(2010, 01, 21);
            ImportXML("T:\\Import\\CVMData\\CVM_" + curDate.ToString("yyyy-MM-dd") + ".xml");
            */

            string curPath = "T:\\Import\\CVMData\\";

            DirectoryInfo di = new DirectoryInfo(curPath);
            foreach (FileInfo fi in di.GetFiles())
            {
                ImportXML(curPath + fi.Name);
            }

        }

        static void ImportXML(string fileName)
        {
            try
            {

                DataSet dsXMLData = new DataSet();

                System.Xml.XmlDataDocument xml = new System.Xml.XmlDataDocument();
                dsXMLData.ReadXml(fileName);
                /*
                frmViewXML Viewer = new frmViewXML();

                Viewer.dgXML.DataSource = dsXMLData.Tables[1];
                Viewer.ShowDialog();
                */
                DB conn = new DB();
                string SQLExpression = "";
                string SQLInsert = "";
                string SQLValues = "";
                //   INSERT INTO NESTOTHER.dbo.Tb020_CVM_Files (
                if (dsXMLData.Tables.Count > 1)
                {
                    for (int i = 0; i < dsXMLData.Tables[1].Columns.Count - 1; i++)
                    {
                        SQLInsert = SQLInsert + ", " + dsXMLData.Tables[1].Columns[i].Caption;
                    }

                    SQLInsert = SQLInsert.Substring(1);

                    foreach (DataRow curRow in dsXMLData.Tables[1].Rows)
                    {
                        SQLValues = "";
                        for (int i = 0; i < dsXMLData.Tables[1].Columns.Count - 1; i++ )
                        {
                            if (dsXMLData.Tables[1].Columns[i].Caption != "DataRef")
                            {
                                SQLValues = SQLValues + ", '" + curRow.ItemArray[i].ToString() + "'";
                            }
                            else
                            {
                                SQLValues = SQLValues + ", '" + Convert.ToDateTime(curRow.ItemArray[i]).ToString("yyyy-MM-dd") + "'";
                            }
                        }
                        SQLValues = SQLValues.Substring(1);
                        SQLValues = " UNION SELECT " + SQLValues;
                        SQLExpression = SQLExpression + SQLValues;
                    }
                    
                    /*
                    {
                        
                        SQLExpression = SQLExpression + " UNION SELECT '" + curRow.ItemArray[0].ToString() + "', '" + curRow.ItemArray[1].ToString() + "', '" + curRow.ItemArray[2].ToString() + "', '" + Convert.ToDateTime(curRow.ItemArray[3]).ToString("yyyy-MM-dd") + "', '" + curRow.ItemArray[4].ToString() + "', '" + curRow.ItemArray[5].ToString() + "', '" + curRow.ItemArray[6].ToString() + "', '" + curRow.ItemArray[7].ToString() + "', '" + curRow.ItemArray[8].ToString() + "' ";
                    }*/

                    SQLExpression = SQLExpression.Substring(6);

                        conn.Execute_Insert_Delete_Update("INSERT INTO NESTOTHER.dbo.Tb020_CVM_Files (" + SQLInsert + ")  " + SQLExpression);
                }
                File.Move(fileName, fileName.Replace("\\CVMData", "\\CVMData\\Imported"));
                Console.WriteLine("File imported: " + fileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR on File: " + fileName + "\r\n" + e.ToString());
            }
        }

    }
}
