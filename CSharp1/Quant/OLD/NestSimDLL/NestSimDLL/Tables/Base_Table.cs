using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Threading;

namespace NestQuant.Common
{
    public class Base_Table 
    {

        protected NestConn curConn = new NestConn();

        protected string _Name = "";
        protected int _Id_Ticker_Template = 0;
        protected int _Id_Ticker_Composite = 0;
        protected DateTime _iniDate;
        protected DateTime _endDate;

        protected bool _IsRealTime;
        protected int _RTPriceSubscriptionID = int.MinValue;
        protected bool hasSubscribed = false;
        protected DateTime LastUpdate = DateTime.MinValue;
        protected Mutex TableMutex = new Mutex();


        protected Utils.TableTypes _ValueColumnType = Utils.TableTypes.Undefined;
        protected Utils.TableFillTypes _FillStyle = Utils.TableFillTypes.FillZero;
        protected Utils.TimeIntervals _DateRowInterval = Utils.TimeIntervals.IntervalDay;

        public string ValueFormat = "0.00; -0.00; -";

        public DateTime[] DateRows;
        protected SortedDictionary<DateTime, int> DatePos = new SortedDictionary<DateTime, int>();
        protected int _DateRowCount = 0;

        public int[] ValueColumns;
        protected List<double[]> Values = new List<double[]>();
        protected SortedDictionary<int, int> ValuePos = new SortedDictionary<int, int>();
        protected int _ValueColumnCount = 0;

        public string[] CustomColumns;
        protected List<double[]> CustomValues = new List<double[]>();
        protected SortedDictionary<string, int> CustomPos = new SortedDictionary<string, int>();
        protected int _CustomColumnCount = 0;
        
        #region Properties

        public int RTPricesSubscriptionID
        {
            get { return _RTPriceSubscriptionID; }
            set
            {
                if (!hasSubscribed)
                {
                    hasSubscribed = true;
                    _RTPriceSubscriptionID = RTPricesSubscriptionID;
                }
            }
        }

        public bool IsRealTime
        {
            get { return _IsRealTime; }
        }

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
                    Values[i] = (double[])Utils.ResizeArray(Values[i], _DateRowCount); 
                }

                for (int i = 0; i < CustomValues.Count; i++) 
                {
                    CustomValues[i] = (double[])Utils.ResizeArray(CustomValues[i], _DateRowCount); 
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

        #region Constructors

        public Base_Table(string Name, int Id_Ticker_Template, DateTime iniDate, DateTime endDate, bool isRealTime)
            : this(Name, Id_Ticker_Template, isRealTime)
        {
            _iniDate = iniDate;
            _endDate = endDate;
            FillRows();
        }


        public Base_Table(string Name, int Id_Ticker_Template, DateTime iniDate, DateTime endDate)
            :this(Name,Id_Ticker_Template,iniDate,endDate,false)
        {
        }
        
        public Base_Table(string Name, int Id_Ticker_Template, bool isRealTime)
        {
            _Name = Name;
            _Id_Ticker_Template = Id_Ticker_Template;
            _IsRealTime = isRealTime;
            
        }

        public Base_Table(string Name, int Id_Ticker_Template)
            : this(Name, Id_Ticker_Template, false)
        {
        }

        #endregion

        #region Methods

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

                if (IsRealTime)
                    _DateRowCount = _DateRowCount + 1;


                DateRows = new DateTime[_DateRowCount];

                int i = 0;

                foreach (DataRow curRow in dt.Rows)
                {
                    DateRows[i] = DateTime.Parse(curRow[0].ToString());
                    DatePos.Add(DateRows[i], i);
                    i++;
                }

                if (IsRealTime)
                    DateRows[_DateRowCount - 1] = DateTime.Today;

            }
        }

        protected void FillZeros(int noColumns)
        {
            for (int i = 0; i < noColumns; i++)
            {
                AddValueColumn(i);
            }
        }

        protected void FillZeros(Base_Table templateTable)
        {
            _Id_Ticker_Composite = templateTable.Id_Ticker_Composite;
            _ValueColumnType = templateTable.ValueColumnType;

            for (int i = 0; i < templateTable.ValueColumnCount; i++)
            {
                AddValueColumn(templateTable.ValueColumns[i]);
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
                Values.Add(new double[_DateRowCount]);

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

        /// <summary>
        /// Returns the ID of a given value column index
        /// </summary>
        /// <param name="Idx_Value">Value column index to seek ID</param>
        /// <returns>ID of the column with the given index. -1 if not found.</returns>
        public int GetValueColumnID(int Idx_Value)
        {
            int id_Value = -1;
            foreach (KeyValuePair<int, int> curCol in ValuePos)
            {
                if (curCol.Value == Idx_Value)
                {
                    id_Value = curCol.Key;
                }
            }
            return id_Value;
        }
                
        public double GetValue(int row, int col)
        {
            return Values[col][row];
        }

        public void SetValue(int row, int col, double ValueToSet)
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
                CustomValues.Add(new double[_DateRowCount]);
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
        
        public double GetCustomValue(int row, int col)
        {
            return CustomValues[col][row];
        }

        public void SetCustomValue(int row, int col, double ValueToSet)
        {
            CustomValues[col][row] = ValueToSet;
        }
        
        public void Merge(Base_Table ArrB)
        {
            if (Utils.Tables.CompareLines(this, ArrB))
            {
                for (int j = 0; j < ArrB.ValueColumnCount; j++)
                {
                    int id_value = ArrB.GetValueColumnID(j);
                    if (this.GetValueColumnIndex(id_value) == -1)
                    {
                        int newColumnIndex = this.AddValueColumn(id_value);

                        for (int i = 0; i < this.DateRowCount; i++)
                        {
                            this.SetValue(i, newColumnIndex, ArrB.GetValue(i, j));
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
            TableMutex.WaitOne();

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

            TableMutex.ReleaseMutex();

            return dt;

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

        #region CreateCustomColumns

        public void AddRowMedian()
        {
            int curCol = AddCustomColumn("MEDIAN");

            for (int i = 0; i < DateRowCount; i++)
            {
                double[] curLine = new double[ValueColumnCount];

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    curLine[j] = Values[j][i];
                }

                CustomValues[curCol][i] = Utils.calcMedian(curLine);
            }
        }

        public void AddRowSum()
        {
            AddRowSum(true);
        }
        
        public void AddRowSum(bool IgnoreNulls)
        {
            int curCol = AddCustomColumn("SUM");

            double tempTotal = 0;

            for (int i = 0; i < DateRowCount; i++)
            {
                tempTotal = 0;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (!double.IsNaN(Values[j][i]) || IgnoreNulls)
                        tempTotal = tempTotal + Values[j][i];
                }

                CustomValues[curCol][i] = tempTotal;
            }
        }

        public void AddRowMax()
        {
            int curCol = AddCustomColumn("MAX");

            double curMax = double.NaN;

            for (int i = 0; i < DateRowCount; i++)
            {
                curMax = double.NaN;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] > curMax) { curMax = Values[j][i]; };
                }

                CustomValues[curCol][i] = curMax;
            }
        }

        public void AddRowMin()
        {
            int curCol = AddCustomColumn("MAX");

            double curMin = double.PositiveInfinity;

            for (int i = 0; i < DateRowCount; i++)
            {
                curMin = double.PositiveInfinity;

                for (int j = 0; j < ValueColumnCount; j++)
                {
                    if (Values[j][i] < curMin) { curMin = Values[j][i]; };
                }

                CustomValues[curCol][i] = curMin;
            }
        }

        public void AddRowCountNonZero()
        {
            int curCol = AddCustomColumn("COUNTNZ");

            double tempTotal = 0;

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

        public void AddRollingMax(string Name, int ColumnType, int RefColumnIndex, int WindowSize)
        {
            int curCol = AddCustomColumn(Name);
            double[] curValues;
            if (ColumnType == 0)
            {
                curValues = Values[RefColumnIndex];
            }
            else
            {
                curValues = CustomValues[RefColumnIndex];
            }

            for (int i = WindowSize; i < DateRowCount; i++)
            {
                CustomValues[curCol][i] = Utils.calcMax(ref curValues, i - WindowSize, i);
            }
        }

        public int AddRollingDev(string Name, int ColumnType, int RefColumnIndex, int WindowSize, bool Annual)
        {
            int curCol = AddCustomColumn(Name);
            double aFactor = 1;
            if (Annual) { aFactor = Math.Sqrt(252); };
            double[] curValues;
            if (ColumnType == 0)
            {
                curValues = Values[RefColumnIndex];
            }
            else
            {
                curValues = CustomValues[RefColumnIndex];
            }

            for (int i = WindowSize - 1; i < DateRowCount; i++)
            {
                CustomValues[curCol][i] = Utils.calcStdev(ref curValues, i - WindowSize + 1, i) * aFactor;
            }

            return curCol;
        }

        public int AddRollingPerf(string Name, int ColumnType, int RefColumnIndex, int WindowSize, bool Annual)
        {
            int curCol = AddCustomColumn(Name);
            double aFactor = 1;
            if (Annual) { aFactor = 252 / WindowSize; };

            double[] curValues;
            if (ColumnType == 0)
            {
                curValues = Values[RefColumnIndex];
            }
            else
            {
                curValues = CustomValues[RefColumnIndex];
            }

            for (int i = WindowSize - 1; i < DateRowCount; i++)
            {
                CustomValues[curCol][i] = Math.Pow((1 + Utils.calcCumPerf(ref curValues, i - WindowSize + 1, i)), (aFactor)) - 1;
            }

            return curCol;
        }

        public int AddRollingSharpe(string Name, int RefPerfColumnIndex, int RefStDevColumnIndex, int WindowSize)
        {
            int curCol = AddCustomColumn(Name);
            double[] curPerf = CustomValues[RefPerfColumnIndex];
            double[] curStDev = CustomValues[RefStDevColumnIndex];

            for (int i = WindowSize - 1; i < DateRowCount; i++)
            {
                CustomValues[curCol][i] = curPerf[i] / curStDev[i];
            }

            return curCol;
        }


        #region RefreshCustomColumns

        public void RefreshRowSum()
        {
            RefreshRowSum(true);
        }

        public void RefreshRowSum(bool IgnoreNulls)
        {
            int curCol = GetCustomColumnIndex("SUM");

            double tempTotal = 0;

            int curDate = DateRowCount -1;
            
            for (int j = 0; j < ValueColumnCount; j++)
            {
                if (!double.IsNaN(Values[j][curDate]) || IgnoreNulls)
                    tempTotal = tempTotal + Values[j][curDate];
            }

            CustomValues[curCol][curDate] = tempTotal;            
        }

        #endregion

        #endregion

        #region Real Time

        public event EventHandler TableChanged;

        private void Changed()
        {
            if (IsRealTime)
            {
                if (TableChanged != null)
                {
                    TableChanged(this, EventArgs.Empty);
                }
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }        

        public void Refresh(int[] Tickers, double[] RTValues, DateTime _LastUpdate)
        {
            if (IsRealTime)
            {
                TableMutex.WaitOne();

                if (LastUpdate < _LastUpdate)
                {
                    LastUpdate = _LastUpdate;
                    UpdateTable(Tickers, RTValues);
                }

                TableMutex.ReleaseMutex();

                Changed();
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        public void Refresh()
        {
            if (IsRealTime)
            {
                TableMutex.WaitOne();

                UpdateTable();

                TableMutex.ReleaseMutex();

                Changed();
                
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        protected virtual void UpdateTable(int[] Tickers, double[] RTValues)
        {
        }

        protected virtual void UpdateTable()
        {
        }

        #endregion

    }
}
