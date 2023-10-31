﻿// ------------------------------------------------------------------------------
//     This code was generated by 
//     9Rays.Net Decompiler.Net services Evaluation
//     ver.5.4.6.0
//     http://www.9rays.net
//     Copyright 2009 9Rays.Net All rights reserved.
//     Evaluation version: decompiles about 50% of methods, properties and events.
// ------------------------------------------------------------------------------

namespace SGN
{
  using System;
  using System.Collections;
  using System.ComponentModel;
  using System.Data;
  using System.Data.SqlClient;
  using System.Drawing;
  using System.Windows.Forms;
  using DevExpress.Data;
  using DevExpress.LookAndFeel;
  using DevExpress.Utils;
  using DevExpress.Utils.Drawing;
  using DevExpress.XtraExport;
  using DevExpress.XtraGrid;
  using DevExpress.XtraGrid.Columns;
  using DevExpress.XtraGrid.Drawing;
  using DevExpress.XtraGrid.Export;
  using DevExpress.XtraGrid.Helpers;
  using DevExpress.XtraGrid.Views.Base;
  using DevExpress.XtraGrid.Views.Base.ViewInfo;
  using DevExpress.XtraGrid.Views.Grid;
  using DevExpress.XtraGrid.Views.Grid.Drawing;
  using DevExpress.XtraGrid.Views.Grid.ViewInfo;
  using MyXtraGrid;
  using NestDLL;
  using SGN;
  using SGN.Business;
  using SGN.CargaDados;
  using SGN.Validacao;

  public class partial frmPositions2 : System.Windows.Forms.Form
  {
    private SGN.CargaDados.CarregaDados CargaDados;
    private SGN.Business.Business_Class Negocios;
    private SGN.Validacao.Valida Valida;
    private int Expandable;
    public int Id_usuario;
    private bool GroupRunning;
    private int GroupCalcDelay;
    private RecordCollection srcList;
    private DevExpress.XtraGrid.Helpers.RefreshHelper helper;
    private System.Random rnd;
    private bool show;
    private bool processing;
    private System.Windows.Forms.ComboBox cmbChoosePortfolio;
    private System.Windows.Forms.GroupBox groupBox10;
    private System.Windows.Forms.Button cmdTxt;
    private System.Windows.Forms.Button cmdExcel;
    private System.Windows.Forms.Timer timer1;
    private MyXtraGrid.MyGridControl dtg;
    private MyXtraGrid.MyGridView dgPositions;
    private System.Windows.Forms.Label lblId;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.DateTimePicker dtpHistDate;
    private System.Windows.Forms.RadioButton radHistoric;
    private System.Windows.Forms.RadioButton radRealTime;
    private System.Windows.Forms.ComboBox cmbCustomView;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.CheckBox chkLink;
    private System.Windows.Forms.Button cmdExpand;
    private System.Windows.Forms.Button cmdCoplase;
    private System.Windows.Forms.Label label2;
    public frmPositions2()
    {
      // Evaluation version.
}
    private void frmPositions_Load(object sender, EventArgs e)
    {
      string str;
      DataTable dataTable;
      SqlDataAdapter sqlDataAdapter;
      int i;
      DataColumn dataColumn;
      bool bl;
      IEnumerator iEnumerator;
      IDisposable iDisposable;
      this.dtg.LookAndFeel.UseDefaultLookAndFeel = false;
      this.dtg.LookAndFeel.Style = LookAndFeelStyle.Skin;
      this.dtg.LookAndFeel.SetSkinStyle("Blue");
      this.cmbChoosePortfolio.SelectedValueChanged -= new EventHandler(this.cmbChoosePortfolio_SelectedValueChanged);
      this.radRealTime.Checked = true;
      this.cmbCustomView.Items.Add("Custom");
      this.cmbCustomView.Items.Add("Options");
      this.cmbCustomView.Items.Add("Standard");
      this.cmbCustomView.ValueMember = "Custom";
      bl = !(this.Id_usuario == 1);
      if (bl)
      {
        goto ILO_00eb;
      }
      this.CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  Tb002_Carteiras where Id_Carteira<> 11", "Id_Carteira", "Carteira", 4);
      goto ILO_0110;
    ILO_00eb:
      this.CargaDados.carregacombo(this.cmbChoosePortfolio, "Select Id_Carteira,Carteira from  VW_Carteiras where Id_Carteira<> 11 UNION ALL S" + 
"ELECT \'-1\', \'All Portfolios\'", "Id_Carteira", "Carteira", 99);
    ILO_0110:
      this.cmbChoosePortfolio.SelectedValueChanged += new EventHandler(this.cmbChoosePortfolio_SelectedValueChanged);
      Carrega_Grid();
      FormatAllColumns();
      dataTable = new DataTable();
      sqlDataAdapter = new SqlDataAdapter();
      str = "SET LANGUAGE \'US_ENGLISH\'; Select TOP 1 * from dbo.Tb000_Posicao_Atual";
      sqlDataAdapter = this.CargaDados.DB.Return_DataAdapter(str);
      sqlDataAdapter.Fill(dataTable);
      i = 0;
      iEnumerator = dataTable.Columns.GetEnumerator();
      try
      {
        goto ILO_01a6;
      ILO_0174:
        dataColumn = iEnumerator.Current as DataColumn;
        this.dgPositions.Columns[i].Caption = dataColumn.Caption;
        i++;
      ILO_01a6:
        bl = iEnumerator.MoveNext();
        if (bl)
        {
          goto ILO_0174;
        }
        goto ILO_01d2;
      }
      finally
      {
        iDisposable = iEnumerator as IDisposable;
        bl = iDisposable == null;
        if (bl)
        {
          goto ILO_01d1;
        }
        iDisposable.Dispose();
      ILO_01d1:;
      }
    ILO_01d2:
      Add_Totals();
      this.timer1.Start();
      sqlDataAdapter.Dispose();
      dataTable.Dispose();
      return;
    }
    private void timer1_Tick(object sender, EventArgs e)
    {
      // Evaluation version.
}
    private void AtualGridTotals()
    {
      this.GroupRunning = true;
      Remove_Totals();
      Add_Totals();
      this.GroupRunning = false;
      return;
    }
    public void Carrega_Grid()
    {
      // Evaluation version.
}
    public void CreateNewLine(object data, int idPosition)
    {
      RecordCollection recordCollection;
      Record record;
      recordCollection = data as RecordCollection;
      record = new Record();
      record.Column0 = (double)Convert.ToInt32(idPosition);
      recordCollection.Add(record);
      data = recordCollection;
      return;
    }
    private void SetValue(object data, int row, int column, object val)
    {
      // Evaluation version.
}
    private void UpdateGrid()
    {
      string str;
      DataTable dataTable;
      SqlDataAdapter sqlDataAdapter;
      string str1;
      Exception exception;
      int i;
      int i1;
      int i2;
      int i3;
      DataRow dataRow;
      int i4;
      Record record;
      int i5;
      string str2;
      int i6;
      DataColumn dataColumn;
      int i7;
      string str3;
      int i8;
      bool bl;
      IEnumerator iEnumerator;
      IEnumerator iEnumerator1;
      IDisposable iDisposable;
      int i9;
      Exception exception1;
      Exception exception2;
      this.dgPositions.BeginUpdate();
      dataTable = new DataTable();
      sqlDataAdapter = new SqlDataAdapter();
      str1 = this.lblId.Text.ToString();
      bl = !(str1 == null);
      if (bl)
      {
        goto ILO_003f;
      }
      str1 = "0";
    ILO_003f:
      bl = !(str1 == "System.Data.DataRowView");
      if (bl)
      {
        goto ILO_005b;
      }
      str1 = "0";
    ILO_005b:
      bl = !(str1 == "-1");
      if (bl)
      {
        goto ILO_0079;
      }
      str = "SELECT * FROM dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio] <> 4 order by " + 
"[Delta Cash] desc ,[Ticker] asc";
      goto ILO_008c;
    ILO_0079:
      str = string.Concat("SELECT * FROM dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Portfolio] =", str1, " order by [Delta Cash] desc ,[Ticker] asc");
      try
      {
      ILO_008c:
        sqlDataAdapter = this.CargaDados.DB.Return_DataAdapter(str);
        goto ILO_00c2;
      }
      catch (Exception exception1)
      {
        exception = exception1;
        bl = exception.ToString().Contains("NOLOCK");
        if (bl)
        {
          goto ILO_00bf;
        }
        throw exception;
      ILO_00bf:
        goto ILO_00c2;
      }
    ILO_00c2:
      sqlDataAdapter.Fill(dataTable);
      i = dataTable.Rows.Count;
      i1 = dataTable.Columns.Count;
      i2 = 1;
      i3 = 0;
      iEnumerator = dataTable.Rows.GetEnumerator();
      try
      {
        goto ILO_02ac;
      ILO_00fe:
        dataRow = iEnumerator.Current as DataRow;
        i4 = 0;
        bl = this.srcList == null;
        if (bl)
        {
          goto ILO_013b;
        }
        i4 = this.srcList.GetIndex(Convert.ToString(dataRow[0]));
      ILO_013b:
        bl = i4 != 99999 ? true : dataRow[3] == null;
        if (bl)
        {
          goto ILO_01cf;
        }
        record = new Record();
        record.Column0 = (double)Convert.ToInt32(dataRow[0]);
        this.srcList.Add(record);
        i5 = this.srcList.Count;
        str2 = Convert.ToString(dataRow[0]);
        i4 = this.srcList.GetIndex(Convert.ToString(dataRow[0]));
        i3 = 1;
        this.dgPositions.RefreshRow(i4);
        i3 = 1;
      ILO_01cf:
        i6 = 0;
        iEnumerator1 = dataTable.Columns.GetEnumerator();
        try
        {
          goto ILO_0275;
        ILO_01e5:
          dataColumn = iEnumerator1.Current as DataColumn;
          bl = !(this.srcList[i4].GetValue(i6) != dataRow[dataColumn].ToString());
          if (bl)
          {
            goto ILO_0244;
          }
          this.srcList[i4].SetValue(i6, dataRow[dataColumn]);
        ILO_0244:
          this.dgPositions.Columns[i6].AppearanceCell.TextOptions.HAlignment = getAlign(i6);
          i6++;
        ILO_0275:
          bl = iEnumerator1.MoveNext();
          if (bl)
          {
            goto ILO_01e5;
          }
          goto ILO_02a4;
        }
        finally
        {
          iDisposable = iEnumerator1 as IDisposable;
          bl = iDisposable == null;
          if (bl)
          {
            goto ILO_02a3;
          }
          iDisposable.Dispose();
        ILO_02a3:;
        }
      ILO_02a4:
        i2++;
      ILO_02ac:
        bl = iEnumerator.MoveNext();
        if (bl)
        {
          goto ILO_00fe;
        }
        goto ILO_02db;
      }
      finally
      {
        iDisposable = iEnumerator as IDisposable;
        bl = iDisposable == null;
        if (bl)
        {
          goto ILO_02da;
        }
        iDisposable.Dispose();
      ILO_02da:;
      }
    ILO_02db:
      i9 = Convert.ToInt32(this.label1.Text) + 1;
      this.label1.Text = i9.ToString();
      sqlDataAdapter.Dispose();
      dataTable.Dispose();
      i7 = this.srcList.Count - 1;
      goto ILO_041e;
    ILO_0325:
      str3 = string.Concat("SELECT count(*) FROM dbo.Tb000_Posicao_Atual(nolock) WHERE [Id Position] =", this.srcList[i7].Column0);
      try
      {
        i8 = Convert.ToInt32(this.Valida.DB.Execute_Query_String(str3));
        goto ILO_0389;
      }
      catch (Exception exception2)
      {
        exception = exception2;
        bl = exception.ToString().Contains("NOLOCK");
        if (bl)
        {
          goto ILO_0383;
        }
        throw exception;
      ILO_0383:
        i8 = 0;
        goto ILO_0389;
      }
    ILO_0389:
      bl = !(i8 == 0);
      if (bl)
      {
        goto ILO_0417;
      }
      i6 = 0;
      iEnumerator = dataTable.Columns.GetEnumerator();
      try
      {
        goto ILO_03e9;
      ILO_03ac:
        dataColumn = iEnumerator.Current as DataColumn;
        bl = i6 == 0;
        if (bl)
        {
          goto ILO_03e2;
        }
        this.srcList[i7].SetValue(i6, "0");
      ILO_03e2:
        i6++;
      ILO_03e9:
        bl = iEnumerator.MoveNext();
        if (bl)
        {
          goto ILO_03ac;
        }
        goto ILO_0415;
      }
      finally
      {
        iDisposable = iEnumerator as IDisposable;
        bl = iDisposable == null;
        if (bl)
        {
          goto ILO_0414;
        }
        iDisposable.Dispose();
      ILO_0414:;
      }
    ILO_0415:
    ILO_0417:
      i7--;
    ILO_041e:
      bl = !(i7 < 0);
      if (bl)
      {
        goto ILO_0325;
      }
      bl = !(i3 == 1);
      if (bl)
      {
        goto ILO_0463;
      }
      this.helper.SaveViewInfo();
      this.dgPositions.RefreshData();
      this.helper.LoadViewInfo();
    ILO_0463:
      i3 = 0;
      this.dgPositions.EndUpdate();
      return;
    }
    private HorzAlignment getAlign(int colNumber)
    {
      // Evaluation version.
}
    private HorzAlignment setColor(int rowNumber)
    {
      HorzAlignment horzAlignment;
      horzAlignment = HorzAlignment.Far;
      return horzAlignment;
    }
    private void FormatAllColumns()
    {
      // Evaluation version.
}
    public IList CreateData()
    {
      RecordCollection recordCollection;
      Record record;
      IList iList;
      recordCollection = new RecordCollection();
      record = new Record();
      recordCollection.Add(record);
      iList = recordCollection;
      return iList;
    }
    private void dgPositions_CalcRowHeight(object sender, RowHeightEventArgs e)
    {
      // Evaluation version.
}
    private void dgPositions_Click(object sender, EventArgs e)
    {
      bool bl;
      bl = this.dgPositions.GetFocusedRowCellValue("Column0") == null;
      if (bl)
      {
        goto ILO_0042;
      }
      Carrega_Dados(Convert.ToInt32(this.dgPositions.GetFocusedRowCellValue("Column0")));
      base.Focus();
    ILO_0042:
      return;
    }
    private void dgPositions_ColumnWidthChanged(object sender, ColumnEventArgs e)
    {
      // Evaluation version.
}
    private void dgPositions_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
    {
      GridView gridView;
      ArrayList arrayList;
      GridSummaryItem gridSummaryItem;
      GridGroupRowPainter gridGroupRowPainter;
      GridGroupRowInfo gridGroupRowInfo;
      int i;
      int i1;
      Hashtable hashtable;
      GridGroupSummaryItem gridGroupSummaryItem;
      GridColumn gridColumn;
      Rectangle rectangle;
      string str;
      SizeF sizeF;
      int i2;
      IEnumerator iEnumerator;
      bool bl;
      IDisposable iDisposable;
      Rectangle rectangle1;
      gridView = sender as GridView;
      arrayList = new ArrayList();
      iEnumerator = gridView.GroupSummary.GetEnumerator();
      try
      {
        goto ILO_004d;
      ILO_001e:
        gridSummaryItem = iEnumerator.Current as GridSummaryItem;
        bl = !(gridSummaryItem is GridGroupSummaryItem) ? true : gridSummaryItem.SummaryType == SummaryItemType.None;
        if (bl)
        {
          goto ILO_004d;
        }
        arrayList.Add(gridSummaryItem);
      ILO_004d:
        bl = iEnumerator.MoveNext();
        if (bl)
        {
          goto ILO_001e;
        }
        goto ILO_0079;
      }
      finally
      {
        iDisposable = iEnumerator as IDisposable;
        bl = iDisposable == null;
        if (bl)
        {
          goto ILO_0078;
        }
        iDisposable.Dispose();
      ILO_0078:;
      }
    ILO_0079:
      bl = !(arrayList.Count == 0);
      if (bl)
      {
        goto ILO_0091;
      }
      goto ILO_026c;
    ILO_0091:
      gridGroupRowPainter = e.Painter as GridGroupRowPainter;
      gridGroupRowInfo = e.Info as GridGroupRowInfo;
      i = gridView.GetRowLevel(e.RowHandle);
      i1 = gridView.GetDataRowHandleByGroupRowHandle(e.RowHandle);
      gridGroupRowInfo.GroupText = string.Concat(gridView.GroupedColumns[i].Caption, ": ", gridView.GetRowCellDisplayText(i1, gridView.GroupedColumns[i]));
      e.Appearance.DrawBackground(e.Cache, gridGroupRowInfo.Bounds);
      gridGroupRowPainter.ElementsPainter.GroupRow.DrawObject(gridGroupRowInfo);
      hashtable = gridView.GetGroupSummaryValues(e.RowHandle);
      iEnumerator = arrayList.GetEnumerator();
      try
      {
        goto ILO_0234;
      ILO_0147:
        gridGroupSummaryItem = iEnumerator.Current as GridGroupSummaryItem;
        gridColumn = gridView.Columns[gridGroupSummaryItem.FieldName];
        rectangle = GetColumnBounds(gridColumn);
        bl = !rectangle.IsEmpty;
        if (bl)
        {
          goto ILO_0189;
        }
        goto ILO_0234;
      ILO_0189:
        str = gridGroupSummaryItem.GetDisplayText(hashtable[gridGroupSummaryItem], false);
        sizeF = e.Appearance.CalcTextSize(e.Cache, str, rectangle.Width);
        i2 = Convert.ToInt32(sizeF.Width) + 1;
        rectangle.X += (rectangle.Width - i2) - 2;
        rectangle.Width = i2;
        rectangle1 = e.Bounds;
        rectangle.Y = rectangle1.Y;
        rectangle1 = e.Bounds;
        rectangle.Height = rectangle1.Height - 2;
        e.Appearance.DrawString(e.Cache, str, rectangle);
      ILO_0234:
        bl = iEnumerator.MoveNext();
        if (bl)
        {
          goto ILO_0147;
        }
        goto ILO_0263;
      }
      finally
      {
        iDisposable = iEnumerator as IDisposable;
        bl = iDisposable == null;
        if (bl)
        {
          goto ILO_0262;
        }
        iDisposable.Dispose();
      ILO_0262:;
      }
    ILO_0263:
      e.Handled = true;
    ILO_026c:
      return;
    }
    private void dgPositions_DragObjectDrop(object sender, DragObjectDropEventArgs e)
    {
      // Evaluation version.
}
    private void dgPositions_HideCustomizationForm(object sender, EventArgs e)
    {
      this.show = false;
      ShowColumnSelector(false, this.dgPositions);
      return;
    }
    private void dgPositions_ShowCustomizationForm(object sender, EventArgs e)
    {
      // Evaluation version.
}
    private Rectangle GetColumnBounds(GridColumn column)
    {
      GridViewInfo gridViewInfo;
      GridColumnInfoArgs gridColumnInfoArgs;
      Rectangle rectangle;
      bool bl;
      gridViewInfo = column.View.GetViewInfo() as GridViewInfo;
      gridColumnInfoArgs = gridViewInfo.ColumnsInfo[column];
      bl = gridColumnInfoArgs == null;
      if (bl)
      {
        goto ILO_0030;
      }
      rectangle = gridColumnInfoArgs.Bounds;
      goto ILO_0038;
    ILO_0030:
      rectangle = Rectangle.Empty;
    ILO_0038:
      return rectangle;
    }
    private void ShowColumnSelector()
    {
      // Evaluation version.
}
    private void ShowColumnSelector(bool showForm, GridView Nome_Grid)
    {
      bool bl;
      bl = !this.show;
      if (bl)
      {
        goto ILO_0021;
      }
      bl = !showForm;
      if (bl)
      {
        goto ILO_001e;
      }
      Nome_Grid.ColumnsCustomization();
    ILO_001e:
      goto ILO_0032;
    ILO_0021:
      bl = !showForm;
      if (bl)
      {
        goto ILO_0031;
      }
      Nome_Grid.DestroyCustomization();
    ILO_0031:
    ILO_0032:
      return;
    }
    private void cmdExpand_Click(object sender, EventArgs e)
    {
      // Evaluation version.
}
    private void cmdCollapse_Click(object sender, EventArgs e)
    {
      this.dgPositions.CollapseAllGroups();
      this.Expandable = 1;
      return;
    }
    private void cmdExcel_Click(object sender, EventArgs e)
    {
      // Evaluation version.
}
    private void cmdTxt_Click(object sender, EventArgs e)
    {
      string str;
      bool bl;
      str = ShowSaveFileDialog("Text Document", "Text Files|*.txt");
      bl = !(str != "");
      if (bl)
      {
        goto ILO_003b;
      }
      ExportTo(new ExportTxtProvider(str));
      OpenFile(str);
    ILO_003b:
      return;
    }
    private string ShowSaveFileDialog(string title, string filter)
    {
      // Evaluation version.
}
    private void ExportTo(IExportProvider provider)
    {
      Cursor cursor;
      BaseExportLink baseExportLink;
      cursor = Cursor.Current;
      Cursor.Current = Cursors.WaitCursor;
      base.FindForm().Refresh();
      baseExportLink = this.dgPositions.CreateExportLink(provider);
      (baseExportLink as GridViewExportLink).ExpandAll = false;
      baseExportLink.ExportTo(true);
      provider.Dispose();
      Cursor.Current = cursor;
      return;
    }
    private void OpenFile(string fileName)
    {
      // Evaluation version.
}
    private void frmPositions_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.Valida.Save_Properties_Form(this, this.Id_usuario, 0);
      return;
    }
    private void myGridView1_ColumnWidthChanged(object sender, ColumnEventArgs e)
    {
      // Evaluation version.
}
    private void myGridView1_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
    {
      GridView gridView;
      ArrayList arrayList;
      GridSummaryItem gridSummaryItem;
      GridGroupRowPainter gridGroupRowPainter;
      GridGroupRowInfo gridGroupRowInfo;
      int i;
      int i1;
      Hashtable hashtable;
      GridGroupSummaryItem gridGroupSummaryItem;
      GridColumn gridColumn;
      Rectangle rectangle;
      string str;
      SizeF sizeF;
      int i2;
      IEnumerator iEnumerator;
      bool bl;
      IDisposable iDisposable;
      Rectangle rectangle1;
      gridView = sender as GridView;
      arrayList = new ArrayList();
      iEnumerator = gridView.GroupSummary.GetEnumerator();
      try
      {
        goto ILO_004d;
      ILO_001e:
        gridSummaryItem = iEnumerator.Current as GridSummaryItem;
        bl = !(gridSummaryItem is GridGroupSummaryItem) ? true : gridSummaryItem.SummaryType == SummaryItemType.None;
        if (bl)
        {
          goto ILO_004d;
        }
        arrayList.Add(gridSummaryItem);
      ILO_004d:
        bl = iEnumerator.MoveNext();
        if (bl)
        {
          goto ILO_001e;
        }
        goto ILO_0079;
      }
      finally
      {
        iDisposable = iEnumerator as IDisposable;
        bl = iDisposable == null;
        if (bl)
        {
          goto ILO_0078;
        }
        iDisposable.Dispose();
      ILO_0078:;
      }
    ILO_0079:
      bl = !(arrayList.Count == 0);
      if (bl)
      {
        goto ILO_0091;
      }
      goto ILO_026c;
    ILO_0091:
      gridGroupRowPainter = e.Painter as GridGroupRowPainter;
      gridGroupRowInfo = e.Info as GridGroupRowInfo;
      i = gridView.GetRowLevel(e.RowHandle);
      i1 = gridView.GetDataRowHandleByGroupRowHandle(e.RowHandle);
      gridGroupRowInfo.GroupText = string.Concat(gridView.GroupedColumns[i].Caption, ": ", gridView.GetRowCellDisplayText(i1, gridView.GroupedColumns[i]));
      e.Appearance.DrawBackground(e.Cache, gridGroupRowInfo.Bounds);
      gridGroupRowPainter.ElementsPainter.GroupRow.DrawObject(gridGroupRowInfo);
      hashtable = gridView.GetGroupSummaryValues(e.RowHandle);
      iEnumerator = arrayList.GetEnumerator();
      try
      {
        goto ILO_0234;
      ILO_0147:
        gridGroupSummaryItem = iEnumerator.Current as GridGroupSummaryItem;
        gridColumn = gridView.Columns[gridGroupSummaryItem.FieldName];
        rectangle = GetColumnBounds(gridColumn);
        bl = !rectangle.IsEmpty;
        if (bl)
        {
          goto ILO_0189;
        }
        goto ILO_0234;
      ILO_0189:
        str = gridGroupSummaryItem.GetDisplayText(hashtable[gridGroupSummaryItem], false);
        sizeF = e.Appearance.CalcTextSize(e.Cache, str, rectangle.Width);
        i2 = Convert.ToInt32(sizeF.Width) + 1;
        rectangle.X += (rectangle.Width - i2) - 2;
        rectangle.Width = i2;
        rectangle1 = e.Bounds;
        rectangle.Y = rectangle1.Y;
        rectangle1 = e.Bounds;
        rectangle.Height = rectangle1.Height - 2;
        e.Appearance.DrawString(e.Cache, str, rectangle);
      ILO_0234:
        bl = iEnumerator.MoveNext();
        if (bl)
        {
          goto ILO_0147;
        }
        goto ILO_0263;
      }
      finally
      {
        iDisposable = iEnumerator as IDisposable;
        bl = iDisposable == null;
        if (bl)
        {
          goto ILO_0262;
        }
        iDisposable.Dispose();
      ILO_0262:;
      }
    ILO_0263:
      e.Handled = true;
    ILO_026c:
      return;
    }
    private void myGridView1_DragObjectDrop(object sender, DragObjectDropEventArgs e)
    {
      // Evaluation version.
}
    private void dgPositions_EndGrouping(object sender, EventArgs e)
    {
      bool bl;
      bl = !(this.Expandable == 0);
      if (bl)
      {
        goto ILO_0021;
      }
      this.dgPositions.ExpandAllGroups();
      goto ILO_002f;
    ILO_0021:
      this.dgPositions.CollapseAllGroups();
    ILO_002f:
      return;
    }
    private void myGridView1_HideCustomizationForm(object sender, EventArgs e)
    {
      // Evaluation version.
}
    private void myGridView1_ShowCustomizationForm(object sender, EventArgs e)
    {
      this.show = true;
      ShowColumnSelector(false, this.dgPositions);
      return;
    }
    private void cmbChoosePortfolio_SelectedValueChanged(object sender, EventArgs e)
    {
      // Evaluation version.
}
    private void radRealTime_CheckedChanged(object sender, EventArgs e)
    {
      this.dtpHistDate.Enabled = false;
      return;
    }
    private void radHistoric_CheckedChanged(object sender, EventArgs e)
    {
      // Evaluation version.
}
    private void dgPositions_RowStyle(object sender, RowStyleEventArgs e)
    {
      bool bl;
      bl = !(Convert.ToDouble(this.dgPositions.GetRowCellValue(e.RowHandle, "Column28")) > 0);
      if (bl)
      {
        goto ILO_0050;
      }
      e.Appearance.BackColor = Color.FromArgb(222, 254, 235);
    ILO_0050:
      bl = !(Convert.ToDouble(this.dgPositions.GetRowCellValue(e.RowHandle, "Column28")) < 0);
      if (bl)
      {
        goto ILO_009f;
      }
      e.Appearance.BackColor = Color.FromArgb(250, 220, 216);
    ILO_009f:
      bl = !(Convert.ToDouble(this.dgPositions.GetRowCellValue(e.RowHandle, "Column28")) == 0);
      if (bl)
      {
        goto ILO_00df;
      }
      e.Appearance.BackColor = Color.White;
    ILO_00df:
      return;
    }
    private void cmbCustomView_SelectedValueChanged(object sender, EventArgs e)
    {
      // Evaluation version.
}
    private void dgPositions_CustomDrawColumnHeader(object sender, ColumnHeaderCustomDrawEventArgs e)
    {
      return;
    }
    private void dgPositions_MouseDown(object sender, MouseEventArgs e)
    {
      // Evaluation version.
}
    private void dgPositions_MouseUp(object sender, MouseEventArgs e)
    {
      this.processing = false;
      return;
    }
    private void dtg_MouseUp(object sender, MouseEventArgs e)
    {
      // Evaluation version.
}
    private void dgPositions_EndSorting(object sender, EventArgs e)
    {
      this.helper.LoadViewInfo();
      return;
    }
    private void button1_Click(object sender, EventArgs e)
    {
      // Evaluation version.
}
    private void Add_Totals()
    {
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column25", this.dgPositions.Columns["Column25"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:#,#0.00}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column26", this.dgPositions.Columns["Column26"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column28", this.dgPositions.Columns["Column28"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:#,#0.00}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column29", this.dgPositions.Columns["Column29"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column44", this.dgPositions.Columns["Column44"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:#,#0.00}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column47", this.dgPositions.Columns["Column47"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:#,#0.00}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column46", this.dgPositions.Columns["Column46"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:#,#0.00}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column52", this.dgPositions.Columns["Column52"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column53", this.dgPositions.Columns["Column53"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column54", this.dgPositions.Columns["Column54"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column55", this.dgPositions.Columns["Column55"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column66", this.dgPositions.Columns["Column66"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:#,#0.00}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column67", this.dgPositions.Columns["Column67"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column68", this.dgPositions.Columns["Column68"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column69", this.dgPositions.Columns["Column69"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column70", this.dgPositions.Columns["Column70"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      this.dgPositions.GroupSummary.Add(SummaryItemType.Sum, "Column73", this.dgPositions.Columns["Column73"]);
      this.dgPositions.GroupSummary[this.dgPositions.GroupSummary.Count - 1].DisplayFormat = "{0:p2}";
      return;
    }
    private void Remove_Totals()
    {
      // Evaluation version.
}
    public event frmPositions.ChangeData Carrega_Dados;
    public event frmPositions.ChangePortfolio Carrega_Portfolio;
  }
}