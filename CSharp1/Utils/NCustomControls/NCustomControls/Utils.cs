using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Base.Handler;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;

namespace NCustomControls
{
    public static class Utils
    {
        public static void CreateButton(string ButtonCaption, NCustomControls.MyGridControl curGridControl)
        {
            CreateButton(ButtonCaption, curGridControl, (MyGridView)curGridControl.MainView);
        }

        public static void CreateButton(string ButtonCaption, NCustomControls.MyGridControl curGridControl, NCustomControls.MyGridView curGridView)
        {
            curGridView.Columns.AddField(ButtonCaption);
            curGridView.Columns[ButtonCaption].VisibleIndex = 0;
            curGridView.Columns[ButtonCaption].Width = 60;
            RepositoryItemButtonEdit curButtonItem = new RepositoryItemButtonEdit();
            curButtonItem.Buttons[0].Tag = 1;
            curButtonItem.Buttons[0].Kind = DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph;
            curButtonItem.Buttons[0].Caption = ButtonCaption;
            curGridControl.RepositoryItems.Add(curButtonItem);
            curGridView.Columns[ButtonCaption].ColumnEdit = curButtonItem;
            curGridView.Columns[ButtonCaption].ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            curGridView.OptionsBehavior.Editable = false;
            curGridView.Columns[ButtonCaption].Visible = true;
            curGridView.Columns[ButtonCaption].Caption = ButtonCaption;
        }
    }
}
