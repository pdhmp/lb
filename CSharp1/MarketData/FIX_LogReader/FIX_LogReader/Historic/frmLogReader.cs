using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NestDLL;
using System.Threading;
using DevExpress.XtraGrid;
using System.Diagnostics;
using System.Data.SqlClient;
using System.IO;
using System.Xml;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;

namespace FixLogViewer
{
    public partial class frmLogReader : Form
    {
        string LastFileOpened = "";
        bool UserCancel = false;
        DataTable LinesDataTable = new DataTable();

        public frmLogReader()
        {
            InitializeComponent();

            dtgMessages.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgMessages.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgMessages.LookAndFeel.SetSkinStyle("Blue");

            dgMessages.ColumnPanelRowHeight = 32;
            dgMessages.Appearance.HeaderPanel.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;

            dtgDetails.LookAndFeel.UseDefaultLookAndFeel = false;
            dtgDetails.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            dtgDetails.LookAndFeel.SetSkinStyle("Blue");
        }

        private void frmHistCalc_Load(object sender, EventArgs e)
        {
            LoadDictionary(@"\\Nestsrv03\NESTSoft\FIX\Dictionaries\FIX44_XP_0.9.xml");
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            
        }
        
        private void LoadGrid()
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn curColumn in dgMessages.Columns)
            {
                curColumn.Visible = false;
                curColumn.Width = 70;
            }

            int CurPosition = 1;
            string curColName = "";

            curColName = "IdPending"; dgMessages.Columns[curColName].Visible = true; dgMessages.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "IdPortfolio"; dgMessages.Columns[curColName].Visible = true; dgMessages.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "LinesToCalc"; dgMessages.Columns[curColName].Visible = true; dgMessages.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "PositionDate"; dgMessages.Columns[curColName].Visible = true; dgMessages.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "Status"; dgMessages.Columns[curColName].Visible = true; dgMessages.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "StartTime"; dgMessages.Columns[curColName].Visible = true; dgMessages.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "CalcLines"; dgMessages.Columns[curColName].Visible = true; dgMessages.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "CreateLines"; dgMessages.Columns[curColName].Visible = true; dgMessages.Columns[curColName].VisibleIndex = CurPosition++;
            curColName = "TotalTimeTaken"; dgMessages.Columns[curColName].Visible = true; dgMessages.Columns[curColName].VisibleIndex = CurPosition++;

            //dgLinesToCalc.BestFitColumns();

            dgMessages.Columns["PositionDate"].Width = 70;
            dgMessages.Columns["Status"].Width = 140;
        }

        private void cmdOpenFile_Click(object sender, EventArgs e)
        {
            openFD.InitialDirectory = @"\\NESTSRV05\c$\FIXLOG";

            if (openFD.ShowDialog() != DialogResult.Cancel)
            {
                string curFileName = openFD.FileName;

                LastFileOpened = curFileName;

                OpenFile(curFileName);
            }
        }

        private void cmdReload_Click(object sender, EventArgs e)
        {
            OpenFile(LastFileOpened);
        }

        private void OpenFile(string openFileName)
        {
            bool ProcessLine = true;
            int PrevPercent = 0;
            int LoadedLines = 0;

            pgrOpenProgress.Visible = true;
            cmdOpenFile.Enabled = false;
            cmdReload.Enabled = false;
            labPercent.Visible = true;

            dtgMessages.DataSource = null;
            dgMessages.Columns.Clear();

            LinesDataTable = new DataTable();

            DataColumn InitialColumn = new DataColumn();
            InitialColumn.DataType = System.Type.GetType("System.String");
            InitialColumn.ColumnName = "0";
            LinesDataTable.Columns.Add(InitialColumn);

            DataColumn orderColumn = new DataColumn();
            orderColumn.DataType = System.Type.GetType("System.Int32");
            orderColumn.ColumnName = "-1";
            LinesDataTable.Columns.Add(orderColumn);


            FileInfo f = new FileInfo(openFileName);
            long TotalCounter = f.Length;

            StreamReader sr = new StreamReader(openFileName);

            string curLine = "";

            while ((curLine = sr.ReadLine()) != null)
            {
                if (UserCancel)
                {
                    return;
                }

                pgrOpenProgress.Value = (int)(sr.BaseStream.Position / (float)TotalCounter * 100);
                labPercent.Text = pgrOpenProgress.Value + " %";

                if (pgrOpenProgress.Value != PrevPercent)
                {
                    pgrOpenProgress.Refresh();
                    labPercent.Refresh();
                    labLinesLoaded.Refresh();
                }

                PrevPercent = pgrOpenProgress.Value;

                if (chkExcludeAdmin.Checked && (curLine.Contains("35=0") || curLine.Contains("35=2") || curLine.Contains("35=4") || curLine.Contains("35=5") || curLine.Contains("35=17") || curLine.Contains("35=A")))
                {
                    ProcessLine = false;
                }
                else if(radLast1.Checked && pgrOpenProgress.Value < 99)
                {
                    ProcessLine = false;
                }
                else if (radLast10.Checked && pgrOpenProgress.Value < 90)
                {
                    ProcessLine = false;
                }
                else if (radContains.Checked && !curLine.Contains(txtFilter.Text))
                {
                    ProcessLine = false;
                }
                else
                {
                    ProcessLine = true;
                }

                if (ProcessLine)
                {
                    LoadedLines++;

                    List<DataRow> DataRows = new List<DataRow>();
                    DataRow FirstRow = LinesDataTable.NewRow();
                    DataRows.Add(FirstRow);

                    FirstRow["0"] = curLine;
                    FirstRow["-1"] = 0;
                    LinesDataTable.Rows.Add(FirstRow);

                    string[] LineItems = curLine.Split((char)1);

                    for (int i = 0; i < LineItems.Length; i++)
                    {
                        string[] tempValues = LineItems[i].Split('=');
                        string curField = tempValues[0];

                        if (tempValues.Length > 1)
                        {
                            string curValue = tempValues[1];

                            if (!LinesDataTable.Columns.Contains(curField))
                            {
                                DataColumn newColumn = new DataColumn();
                                newColumn.DataType = System.Type.GetType("System.String");
                                newColumn.ColumnName = curField;
                                LinesDataTable.Columns.Add(newColumn);
                            }

                            bool ValueSet = false;

                            foreach (DataRow curRow in DataRows)
                            {
                                if (curRow[curField] == DBNull.Value)
                                {
                                    curRow[curField] = curValue;
                                    ValueSet = true;
                                }
                            }

                            if (!ValueSet)
                            {
                                DataRow newRow = LinesDataTable.NewRow();
                                DataRows.Add(newRow);
                                if (LinesDataTable.Columns.Contains("0")) newRow["0"] = curLine;
                                if (LinesDataTable.Columns.Contains("34")) newRow["34"] = FirstRow["34"];
                                if (LinesDataTable.Columns.Contains("35")) newRow["35"] = FirstRow["35"];
                                if (LinesDataTable.Columns.Contains("49")) newRow["49"] = FirstRow["49"];
                                if (LinesDataTable.Columns.Contains("55")) newRow["55"] = FirstRow["55"];
                                if (LinesDataTable.Columns.Contains("-1")) newRow["-1"] = DataRows.Count - 1;
                                newRow[curField] = curValue;
                                LinesDataTable.Rows.Add(newRow);
                            }

                            //FirstRow[curField] = curValue;
                        }
                    }

                    labLinesLoaded.Text = LoadedLines + " lines";
                }
            }

            sr.Close();

            dtgMessages.DataSource = LinesDataTable;

            List<int> ColNames = new List<int>();

            foreach (DevExpress.XtraGrid.Columns.GridColumn curColumn in dgMessages.Columns)
            {
                ColNames.Add(int.Parse(curColumn.Name.Replace("col", "")));
            }

            ColNames.Sort();

            int curCounter = 0;

            foreach (int curName in ColNames)
            {
                dgMessages.Columns[curName.ToString()].VisibleIndex = curCounter++;
            }

            if (dgMessages.Columns.Count > 2)
            {
                if (LinesDataTable.Columns.Contains("52")) { dgMessages.Columns["52"].Fixed = FixedStyle.Left; dgMessages.Columns["52"].BestFit(); }
                if (LinesDataTable.Columns.Contains("-1")) { dgMessages.Columns["-1"].Fixed = FixedStyle.Left; dgMessages.Columns["-1"].BestFit(); }
                if (LinesDataTable.Columns.Contains("35")) { dgMessages.Columns["35"].Fixed = FixedStyle.Left; dgMessages.Columns["35"].BestFit(); }
                if (LinesDataTable.Columns.Contains("55")) { dgMessages.Columns["55"].Fixed = FixedStyle.Left; dgMessages.Columns["55"].BestFit(); }
            }

            cmdOpenFile.Enabled = true;
            pgrOpenProgress.Visible = false;
            cmdReload.Enabled = true;
            labPercent.Visible = false;
        }

        private void dtgMessages_Click(object sender, EventArgs e)
        {
            if (dgMessages.FocusedRowHandle < -10000)
                return;

            bool RepeatingFound = false;
            List<string> RepeatingStarts = new List<string>();

            dgDetails.FormatConditions.Clear();

            dtgDetails.DataSource = null;

            string MessageText = (string)dgMessages.GetRowCellValue(dgMessages.FocusedRowHandle, "0").ToString();

            string[] MessageArray = MessageText.Split((char)1);

            List<DetailsLine> GridSource = new List<DetailsLine>();

            for (int i = 0; i < MessageArray.Length; i++)
            {
                string[] LineArray = MessageArray[i].Split('=');
                if (LineArray.Length > 1)
                {
                    DetailsLine curDetailsLine = new DetailsLine();
                    curDetailsLine.Field = LineArray[0];
                    if (RepeatingFound)
                    {
                        if (!RepeatingStarts.Contains(curDetailsLine.Field))
                        {
                            RepeatingStarts.Add(curDetailsLine.Field);
                        }
                        RepeatingFound = false;
                    }
                    curDetailsLine.Value = LineArray[1];
                    if (FieldDescriptions.ContainsKey(int.Parse(LineArray[0])))
                    {
                        curDetailsLine.FieldDescription = FieldDescriptions[int.Parse(LineArray[0])].Value;

                        if (curDetailsLine.FieldDescription.Substring(0, 2) == "No")
                        {
                            if(char.IsUpper(curDetailsLine.FieldDescription[2]))
                            {
                                RepeatingFound = true;
                            }
                        }

                        if (FieldDescriptions[int.Parse(LineArray[0])].Description.ContainsKey(LineArray[1]))
                        {
                            curDetailsLine.ValueDescription = FieldDescriptions[int.Parse(LineArray[0])].Description[LineArray[1]];
                        }
                    }
                    GridSource.Add(curDetailsLine);
                }
            }
            
            int repCounter = 0;

            Color[] curColors = new Color[6];

            curColors[0] = Color.LightGray;
            curColors[1] = Color.LightBlue;
            curColors[2] = Color.LightGreen;
            curColors[3] = Color.LightCoral;
            curColors[4] = Color.LightCyan;
            curColors[5] = Color.LightGoldenrodYellow;

            foreach (string FieldName in RepeatingStarts)
            {
                repCounter++;
                StyleFormatCondition condition1 = new StyleFormatCondition(FormatConditionEnum.Equal, dgDetails.Columns["Field"], null, FieldName);
                condition1.Appearance.BackColor = curColors[repCounter];
                condition1.ApplyToRow = true;
                condition1.Appearance.Options.UseBackColor = true;
                dgDetails.FormatConditions.Add(condition1);
            }

            //foreach (string FieldName in RepeatingStarts)
            //{
            //    repCounter++;
            //    string prefix = "";
            //    for (int i = 0; i < repCounter; i++)
            //    {
            //        prefix = prefix + "  ";
            //    }

            //    foreach (DetailsLine curDetailsLine in GridSource)
            //    {
            //        if (curDetailsLine.Field == FieldName)
            //        {
            //            curDetailsLine.Field = prefix + curDetailsLine.Field;
            //        }
            //    }
            //}

            dtgDetails.DataSource = GridSource;
            dgDetails.BestFitColumns();
        }

        public class DetailsLine
        {
            private string _Field = ""; public string Field { get { return _Field; } set { _Field = value; } }
            private string _FieldDescription = ""; public string FieldDescription { get { return _FieldDescription; } set { _FieldDescription = value; } }
            private string _Value = ""; public string Value { get { return _Value; } set { _Value = value; } }
            private string _ValueDescription = ""; public string ValueDescription { get { return _ValueDescription; } set { _ValueDescription = value; } }
        }

        public Dictionary<int, DescriptionItem> FieldDescriptions = new Dictionary<int, DescriptionItem>();

        private void LoadDictionary(string DictionaryFile)
        {
            FieldDescriptions.Clear();

            int tempNumber = 0;
            int curNumber = 0;
            string tempItem = "";

            XmlTextReader reader = new XmlTextReader(DictionaryFile);

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: 
                        //Console.Write("<" + reader.Name);

                        while (reader.MoveToNextAttribute()) // Read the attributes.
                        {
                            //Console.Write(" " + reader.Name + "='" + reader.Value + "'");
                            if (reader.Name == "number")
                            {
                                tempNumber = int.Parse(reader.Value);
                                tempItem = "";
                            }
                            if (reader.Name == "name" && tempNumber!=0)
                            {
                                DescriptionItem curDescriptionItem = new DescriptionItem ();
                                curDescriptionItem.Value = reader.Value;
                                FieldDescriptions.Add(tempNumber, curDescriptionItem);
                                curNumber = tempNumber;
                                tempNumber = 0;
                            }

                            if (reader.Name == "enum")
                            {
                                tempItem = reader.Value;

                            }

                            if (reader.Name == "description" && tempItem != "")
                            {
                                FieldDescriptions[curNumber].Description.Add(tempItem, reader.Value);
                                //tempItem = reader.Value;

                            }
                        }
                        //Console.WriteLine(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        //Console.WriteLine(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        //Console.Write("</" + reader.Name);
                        //Console.WriteLine(">");
                        break;
                }

            }
            dtgMessages_Click(this, new EventArgs());
        }

        public class DescriptionItem
        {
            public string Value = "";
            public Dictionary<string, string> Description = new Dictionary<string, string>();
        }

        private void cmdChangeDictionary_Click(object sender, EventArgs e)
        {
            openFD.InitialDirectory = @"\\Nestsrv03\NESTSoft\FIX\Dictionaries";
            
            if (openFD.ShowDialog() != DialogResult.Cancel)
            {
                string curFileName = openFD.FileName;
                LoadDictionary(openFD.FileName);
            }
        }

        private void frmLogReader_FormClosing(object sender, FormClosingEventArgs e)
        {
            UserCancel = true;
        }

        private void radContains_CheckedChanged(object sender, EventArgs e)
        {
            if (radContains.Checked)
            {
                txtFilter.Enabled = true;
            }
            else
            {
                txtFilter.Enabled = false;
            }
        }
    }
}
