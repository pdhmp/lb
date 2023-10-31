using System;
using System.Collections;
using System.Collections.Generic;
using DevExpress.Data;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;

namespace DevExpress.XtraGrid.Helpers {
	public class RefreshHelper {
		[Serializable]
		public struct RowInfo{
			public object Id;
			public int level;
		};

		private GridView view;
		private string keyFieldName;
		private ArrayList saveExpList;
		private ArrayList saveSelList;
		private ArrayList saveMasterRowsList;
		private int visibleRowIndex = -1;
        private List <GroupSummarySortInfo> GroupCollections = new List<GroupSummarySortInfo>();

        public GridSummaryItem _tempItem;
        public ColumnSortOrder _curOrder;
        public GridColumn _curGrouping;

		public RefreshHelper(GridView view, string keyFieldName) {
			this.view = view;
			this.keyFieldName = keyFieldName;
		}

		public ArrayList SaveExpList{
			get { 
				if(saveExpList == null)
					saveExpList = new ArrayList();
				return saveExpList;
			}
		}

		public ArrayList SaveSelList{
			get { 
				if(saveSelList == null)
					saveSelList = new ArrayList();
				return saveSelList;
			}
		}

		public ArrayList SaveMasterRowsList{
			get{
				if(saveMasterRowsList == null)
					saveMasterRowsList = new ArrayList();
				return saveMasterRowsList;
			}
		}

		protected int FindParentRowHandle(RowInfo rowInfo, int rowHandle) {
			int result = view.GetParentRowHandle(rowHandle);
            int counter = 0;
            while (view.GetRowLevel(result) != rowInfo.level && counter<200)
            {
                result = view.GetParentRowHandle(result);
                counter++;
                //Console.WriteLine(view.GetParentRowHandle(result));
            }
            
            return result;
		}

		protected void ExpandRowByRowInfo(RowInfo rowInfo){
			int dataRowHandle = view.LocateByValue(0, view.Columns[keyFieldName], rowInfo.Id);
			if(dataRowHandle != GridControl.InvalidRowHandle){
				int parentRowHandle = FindParentRowHandle(rowInfo, dataRowHandle);
				view.SetRowExpanded(parentRowHandle, true, false);
			}
		}

		protected int GetRowHandleToSelect(RowInfo rowInfo) {
			int dataRowHandle = view.LocateByValue(0, view.Columns[keyFieldName], rowInfo.Id);
			if(dataRowHandle != GridControl.InvalidRowHandle)
				if (view.GetRowLevel(dataRowHandle) != rowInfo.level)
					return  FindParentRowHandle(rowInfo, dataRowHandle);
			return dataRowHandle;
		}

		protected void SelectRowByRowInfo(RowInfo rowInfo, bool isFocused){
			if(isFocused)
				view.FocusedRowHandle = GetRowHandleToSelect(rowInfo);
			else 
				view.SelectRow(GetRowHandleToSelect(rowInfo));
		}

		public void SaveSelectionViewInfo(ArrayList list){
			list.Clear();
			GridColumn column = view.Columns[keyFieldName];
			RowInfo rowInfo;
			int[] selectionArray = view.GetSelectedRows();
			if (selectionArray != null)  // otherwise we have a single focused but not selected row
				for(int i = 0; i < selectionArray.Length; i++){
					int dataRowHandle = selectionArray[i];
					rowInfo.level = view.GetRowLevel(dataRowHandle);
					if(dataRowHandle < 0) // group row
						dataRowHandle = view.GetDataRowHandleByGroupRowHandle(dataRowHandle);
					rowInfo.Id = view.GetRowCellValue(dataRowHandle, column);            
					list.Add(rowInfo);
				}
			rowInfo.Id = view.GetRowCellValue(view.FocusedRowHandle, column);
			rowInfo.level = view.GetRowLevel(view.FocusedRowHandle);
			list.Add(rowInfo);
		}

		public void SaveExpansionViewInfo(ArrayList list){
			if(view.GroupedColumns.Count == 0) return;
			list.Clear();
			GridColumn column = view.Columns[keyFieldName];
			for(int i = -1; i > int.MinValue; i--) {
				if(!view.IsValidRowHandle(i)) break;
				if(view.GetRowExpanded(i)){
					RowInfo rowInfo;
					int dataRowHandle = view.GetDataRowHandleByGroupRowHandle(i);
					rowInfo.Id = view.GetRowCellValue(dataRowHandle, column);
                    if (view.GetRowCellValue(dataRowHandle, column).ToString() == "0") 
                    {
                        rowInfo.Id = view.GetRowCellValue(view.GetNextVisibleRow(dataRowHandle), column);
                    }
					rowInfo.level = view.GetRowLevel(i);
					list.Add(rowInfo);
				}			  
			}			 
		}

		public void SaveExpandedMasterRows(ArrayList list){
			if(view.GridControl.Views.Count == 1) return;
			list.Clear();
			GridColumn column = view.Columns[keyFieldName];
			for(int i = 0; i < view.DataRowCount; i++)
				if(view.GetMasterRowExpanded(i))
					list.Add(view.GetRowCellValue(i, column));
		}

		public void SaveVisibleIndex(){
			visibleRowIndex = view.GetVisibleIndex(view.FocusedRowHandle) - view.TopRowIndex;
		}

		public void LoadVisibleIndex(){
			view.MakeRowVisible(view.FocusedRowHandle, true);
			view.TopRowIndex = view.GetVisibleIndex(view.FocusedRowHandle) - visibleRowIndex;
		}

		public void LoadSelectionViewInfo(ArrayList list) {
			view.BeginSelection();
			try {
				view.ClearSelection();
				for(int i = 0; i < list.Count; i++)
					SelectRowByRowInfo((RowInfo)list[i], i == list.Count - 1);
			}
			finally {
				view.EndSelection();
			}
		}

		public void LoadExpansionViewInfo(ArrayList list) {
			if(view.GroupedColumns.Count == 0) return;
			view.BeginUpdate();
			try {
				view.CollapseAllGroups();
				foreach(RowInfo info in list)
					ExpandRowByRowInfo(info);
			}
			finally {
				view.EndUpdate();
			}
		}

		public void LoadExpandedMasterRows(ArrayList list) {
			view.BeginUpdate();
			try {
				view.CollapseAllDetails();
				GridColumn column = view.Columns[keyFieldName];
				for(int i = 0; i < list.Count; i++){
					int rowHandle = view.LocateByValue(0, column, list[i]);
					view.SetMasterRowExpanded(rowHandle, true);
				}
			}
			finally {
				view.EndUpdate();
			}
		}

        private GridSummaryItem GetGroupByName(GridView view, string Groupname)
        {
            foreach (GridSummaryItem curItem in view.GroupSummary)
            {
                if (curItem.FieldName == Groupname)
                {
                    return curItem;
                }
            }
            return null;
        }

        public void SaveGroupSort()
        {
            GroupCollections.Clear();
            foreach (GroupSummarySortInfo curGrouping in view.GroupSummarySortInfo)
            {
                GroupCollections.Add(curGrouping); break;
            }

        }


		public void SaveViewInfo(){
			//SaveExpandedMasterRows(SaveMasterRowsList);
			SaveExpansionViewInfo(SaveExpList);
            //SaveGroupSort();

			//SaveSelectionViewInfo(SaveSelList);
			//SaveVisibleIndex();
		}

		public void LoadViewInfo(){
			//LoadExpandedMasterRows(SaveMasterRowsList);
			LoadExpansionViewInfo(SaveExpList);
			//LoadSelectionViewInfo(SaveSelList);
			//LoadVisibleIndex();
            foreach (GroupSummarySortInfo curGrouping in GroupCollections)
            {
                //ColumnSortOrder curOrder = curGrouping.SortOrder;
                //GridSummaryItem tempItem = GetGroupByName(view, curGrouping.SummaryItem.FieldName);
                //GridColumn curColumn = view.Columns[curGrouping.GroupColumn.Name.Replace("col", "")];
            }
		}
	}
}
