using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using NestSim.Common;

namespace NestSim
{
    public class Base_Table 
    {

        protected NestConn curConn = new NestConn();

        protected string _Name = "";
        protected int _Id_Ticker_Template = 0;
        protected int _Id_Ticker_Composite = 0;
        protected DateTime _iniDate;
        protected DateTime _endDate;

        protected Utils.TableTypes _ValueColumnType = Utils.TableTypes.Undefined;
        protected Utils.TableFillTypes _FillStyle = Utils.TableFillTypes.FillZero;
        protected Utils.TimeIntervals _DateRowInterval = Utils.TimeIntervals.IntervalDay;

        public string ValueFormat = "0.00; -0.00; -";

        public DateTime[] DateRows;
        protected SortedDictionary<DateTime, int> DatePos = new SortedDictionary<DateTime, int>();
        protected int _DateRowCount = 0;

        public int[] ValueColumns;
        protected List<float[]> Values = new List<float[]>();
        protected SortedDictionary<int, int> ValuePos = new SortedDictionary<int, int>();
        protected int _ValueColumnCount = 0;

        public string[] CustomColumns;
        protected List<float[]> CustomValues = new List<float[]>();
        protected SortedDictionary<string, int> CustomPos = new SortedDictionary<string, int>();
        protected int _CustomColumnCount = 0;

        #region Properties

        public string Name
        {
            get
            {
                return _Name;
            }
        }
        public int Id_Ticker_Template
        {
            get
            {
                return _Id_Ticker_Template;
            }
        }
        public int Id_Ticker_Composite
        {
            get
            {
                return _Id_Ticker_Composite;
            }
        }
        public DateTime iniDate
        {
            get
            {
                return _iniDate;
            }
            set
            {
                _iniDate = value;
            }
        }
        public DateTime endDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }
        public Utils.TableFillTypes FillStyle
        {
            get
            {
                return _FillStyle;
            }
            set
            {
                _FillStyle = value;
            }
        }
        public Utils.TableTypes ValueColumnType
        {
            get
            {
                return _ValueColumnType;
            }
            set
            {
                if (_ValueColumnType == value)
                    return;
                _ValueColumnType = value;
            }
        }
        public Utils.TimeIntervals DateRowInterval
        {
            get
            {
                return _DateRowInterval;
            }
            set
            {
                if (_DateRowInterval == value)
                    return;
                _DateRowInterval = value;
            }
        }

        public int DateRowCount
        {
            get
            {
                return _DateRowCount;
            }
            set
            {
                _DateRowCount = value;

                // resize all arrays to match new size
                DateRows = (DateTime[])Utils.ResizeArray(DateRows, _DateRowCount);
                for (int i = 0; i < Values.Count; i++) 
                {
                    Values[i] = (float[])Utils.ResizeArray(Values[i], _DateRowCount); 
                }

                for (int i = 0; i < CustomValues.Count; i++) 
                {
                    CustomValues[i] = (float[])Utils.ResizeArray(CustomValues[i], _DateRowCount); 
                }
            }
        }
        public int ValueColumnCount
        {
            get
            {
                return _ValueColumnCount;
            }
        }
        public int CustomColumnCount
        {
            get
            {
                return _CustomColumnCount;
            }
        }

#endregion

        public Base_Table(string Name, int Id_Ticker_Template, DateTime iniDate, DateTime endDate)
            : this(Name, Id_Ticker_Template)
        {
            _iniDate = iniDate;
            _endDate = endDate;
            FillRows();
        }

        public Base_Table(string Name, int Id_Ticker_Template)
        {
            _Name = Name;
            _Id_Ticker_Template = Id_Ticker_Template;
        }

        protected void FillRows()
        {
            if (_iniDate != null && _endDate != null)
            {
                DataTable dt;
                using (NestConn conn = new NestConn())
                {
                    string SQLString = "SELECT Data_Hora_Reg FROM NESTDB.dbo.Tb053_Precos_Indices WHERE id_Ativo=" + Id_Ticker_Template + " AND Tipo_Preco=1 AND Source=1 AND Data_Hora_Reg>='" + _iniDate.ToString("yyyy-MM-dd") + "' AND Data_Hora_Reg<='" + _endDate.ToString("yyyy-MM-dd") + "' GROUP BY Data_Hora_Reg ORDER BY Data_Hora_Reg";
                    dt = conn.ExecuteDataTable(SQLString);
                }

                _DateRowCount = dt.Rows.Count;

                DateRows = new DateTime[_DateRowCount];

                int i = 0;

                foreach (DataRow curRow in dt.Rows)
                {
                    DateRows[i] = DateTime.Parse(curRow[0].ToString());
                    DatePos.Add(DateRows[i], i);
                    i++;
                }
            }
        }

        protected void FillZeros(int noColumns)
        {
            for (int i = 0; i < noColumns; i++)
            {
                AddValueColumn(i);
            }
        }

        public int GetDateIndex(DateTime DateToGet)
        {
            int curIndex = 0;
            if (DatePos.TryGetValue(DateToGet, out curIndex))
            {
                return curIndex;
            }
            else
            {
                return -1;
            }
        }

        public int AddValueColumn(int DataColumn_Id)
        {
            if (DateRows != null)
            {
                // Add Ticker to ticker list and Dictionary
                _ValueColumnCount++;
                if (ValueColumns == null) { ValueColumns = new int[1]; };
                ValueColumns = (int[])Utils.ResizeArray(ValueColumns, _ValueColumnCount);
                ValueColumns[_ValueColumnCount - 1] = DataColumn_Id;
                ValuePos.Add(DataColumn_Id, _ValueColumnCount - 1);

                // Create Ticker column
                Values.Add(new float[_DateRowCount]);

                return _ValueColumnCount - 1;
            }
            else
            {
                return -1;
            }
        }

        public int GetValueColumnIndex(int Id_Value)
        {
            int curIndex = 0;
            if(ValuePos.TryGetValue (Id_Value, out curIndex))
            {
                return curIndex;
            }
            else
            {
                return -1;
            }
        }

        public float GetValue(int row, int col)
        {
            return Values[col][row];
        }

        public void SetValue(int row, int col, float ValueToSet)
        {
            Values[col][row] = ValueToSet;
        }

        public int AddCustomColumn(string ControlName)
        {
            if (DateRows != null)
            {
                // Add CTColumn to CTColumn list and Dictionary
                _CustomColumnCount++;
                if (CustomColumns == null) { CustomColumns = new string[1]; };
                CustomColumns = (string[])Utils.ResizeArray(CustomColumns, _CustomColumnCount);
                CustomColumns[_CustomColumnCount - 1] = ControlName;
                CustomPos.Add(ControlName, _CustomColumnCount - 1);

                // Create CTColumn column
                CustomValues.Add(new float[_DateRowCount]);
                return _CustomColumnCount - 1;
            }
            else
            {
                return -1;
            }
        }

        public int GetCustomColumnIndex(string CustomColumnName)
        {
            int curIndex = 0;
            if (CustomPos.TryGetValue(CustomColumnName, out curIndex))
            {
                return curIndex;
            }
            else
            {
                return -1;
            }
        }
        
        public float GetCustomValue(int row, int col)
        {
            return CustomValues[col][row];
        }

        public void SetCustomValue(int row, int col, float ValueToSet)
        {
            CustomValues[col][row] = ValueToSet;
        }


        public void Merge(Base_Table ArrB)
        {
            if (Utils.Tables.CompareLines(this, ArrB))
            {
                foreach (KeyValuePair<int, int> ArrBValueColumn in ArrB.ValuePos)
                {
                    if (this.GetValueColumnIndex(ArrBValueColumn.Key) == -1)
                    {
                        int newColumnIndex = this.AddValueColumn(ArrBValueColumn.Key);

                        for (int i = 0; i < this.DateRowCount; i++)
                        {
                            this.SetValue(i, newColumnIndex, ArrB.GetValue(i, ArrBValueColumn.Value));
                        }
                    }
                    else
                    {
                        throw new System.NotImplementedException();
                    }
                }
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }
        
        public DataTable ToDataTable()
        {

            DataTable dt = new DataTable();

            DataColumn DateColumn = new DataColumn();
            DateColumn.DataType = System.Type.GetType("System.DateTime");
            DateColumn.ColumnName = "Dates";
            dt.Columns.Add(DateColumn);

            for (int i = 0; i < ValueColumnCount; i++)
            {
                DataColumn idColumn = new DataColumn();
                idColumn.DataType = System.Type.GetType("System.Double");
                idColumn.ColumnName = ValueColumns[i].ToString();
                dt.Columns.Add(idColumn);
            }

            for (int i = 0; i < _CustomColumnCount; i++)
            {
                DataColumn idColumn = new DataColumn();
                idColumn.DataType = System.Type.GetType("System.Double");
                idColumn.ColumnName = CustomColumns[i];
                dt.Columns.Add(idColumn);
            }

            for (int i = 0; i < DateRowCount; i++)
            {
                DataRow row;
                row = dt.NewRow();
                row[0] = DateRows[i];
                for (int j = 0; j < ValueColumnCount; j++)
                {
                    try
                    {
                        row[j + 1] = Values[j][i];
                    }
                    catch
                    {
                        row[j + 1] = 0;
                    }
                }

                for (int j = 0; j < _CustomColumnCount; j++)
                {
                    try
                    {
                        row[j + 1 + ValueColumnCount] = CustomValues[j][i];
                    }
                    catch
                    {
                        row[j + 1 + ValueColumnCount] = 0;
                    }
                }

                dt.Rows.Add(row);
            }

            return dt;

        }


        #region CreateCustomColumns

        public void AddCustomMedian()
        {
            int curCol = AddCustomColumn("MEDIAN");

            for (int i = 0; i < DateRowCount; i++)
            {
                float[] curLine = new float[ValueColumnCount];

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    curLine[j] = Values[j][i];
                }

                Array.Sort(curLine);

                CustomValues[curCol][i] = Utils.calcMedian(curLine);
            }
        }

        public void AddCustomSum()
        {
            int curCol = AddCustomColumn("SUM");

            float tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    tempTotal = tempTotal + Values[j][i];
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddCountNonZero()
        {
            int curCol = AddCustomColumn("COUNTNZ");

            float tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] != 0) { tempTotal++; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddCountLong()
        {
            int curCol = AddCustomColumn("COUNTLONG");

            float tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] > 0) { tempTotal++; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddCountShort()
        {
            int curCol = AddCustomColumn("COUNTSHORT");

            float tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] < 0) { tempTotal++; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddGross()
        {
            int curCol = AddCustomColumn("GROSS");

            float tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] != 0) { tempTotal = tempTotal + Math.Abs(Values[j][i]); };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddLong()
        {
            int curCol = AddCustomColumn("LONG");

            float tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] > 0) { tempTotal = tempTotal + Values[j][i]; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddShort()
        {
            int curCol = AddCustomColumn("SHORT");

            float tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] < 0) { tempTotal = tempTotal + Values[j][i]; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddNet()
        {
            int curCol = AddCustomColumn("NET");

            float tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] != 0) { tempTotal = tempTotal + Values[j][i]; };
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void ZeroFillFromComposite(int Id_Ticker_Composite)
        {
            DataTable dt;

            _Id_Ticker_Composite = Id_Ticker_Composite;

            string SQLString = "SELECT Id_Ticker_Component FROM dbo.Tb023_Securities_Composition WHERE Id_Ticker_Composite=" + Id_Ticker_Composite + " GROUP BY Id_Ticker_Component ORDER BY Id_Ticker_Component";

            dt = curConn.ExecuteDataTable(SQLString);

            foreach (DataRow curRow in dt.Rows)
            {
                AddValueColumn(Convert.ToInt16(curRow[0].ToString()));
            }
        }


        #endregion


    }
}
