using System;
using System.Windows.Forms;
using System.Reflection;

namespace NestQuant.Common
{
    public static class FormUtils
    {
        #region FormRegion

        public static void ShowInForm(Base_Table curTable)
        {
            ShowInForm(curTable, "Viewer", "0.00");
        }

        public static void ShowInForm(Base_Table curTable, string FormCaption, string defFormat)
        {
            frmViewTable viewThisTable = new frmViewTable();

            viewThisTable.dtgViewer.DataSource = curTable.ToDataTable();
            viewThisTable.Text = FormCaption;
            viewThisTable.dtgViewer.Columns[0].DefaultCellStyle.Format = "dd-MMM-yy";
            viewThisTable.dtgViewer.DefaultCellStyle.Format = defFormat;

            for (int i = 0; i < viewThisTable.dtgViewer.Columns.Count; i++)
            {
                viewThisTable.dtgViewer.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }

            viewThisTable.Show();
        }

        //public static void ShowInForm(Strategy curStrategy, string FormCaption)
        //{
        //    frmViewStrategy StratForm = new frmViewStrategy();
        //    StratForm.curStrategy = (SectorValueBZ)curStrategy;
        //    StratForm.Text = FormCaption;

        //    StratForm.AddButton("cmd_perfSummary", "perfSummary", 0, 100);

        //    int i = 0;
        //    foreach (Price_Table curTable in curStrategy.PriceTables)
        //    {
        //        string curName = curTable.Name;
        //        StratForm.AddButton("cmd_" + curName, curName, i, 200);
        //        i++;
        //    }

        //    i = 0;
        //    foreach (Weight_Table curTable in curStrategy.WeightTables)
        //    {
        //        string curName = curTable.Name;
        //        StratForm.AddButton("cmd_" + curName, curName, i, 300);
        //        i++;
        //    }

        //    i = 0;
        //    foreach (Price_Table curTable in curStrategy.SignalTables)
        //    {
        //        string curName = curTable.Name;
        //        StratForm.AddButton("cmd_" + curName, curName, i, 400);
        //        i++;
        //    }

        //    StratForm.Show();
        //}

        //public static void ShowInForm(Strategy_Container curContainer, string FormCaption)
        //{
        //    frmViewContainer ContainerForm = new frmViewContainer();
        //    ContainerForm.curContainer = curContainer;
        //    ContainerForm.Text = FormCaption;

        //    int i = 0;
        //    foreach (Strategy curStrategy in curContainer.Strategies)
        //    {
        //        string curName = curStrategy.Name;
        //        ContainerForm.AddButton("cmd_" + curName, curName, i, 100);
        //        i++;
        //    }

        //    ContainerForm.Show();
        //}

        #endregion
    }
}
