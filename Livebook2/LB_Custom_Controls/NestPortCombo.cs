using System;
//using LiveDLL;

namespace LiveBook
{
    public class NestPortCombo : System.Windows.Forms.ComboBox
    {

        private bool _includeAllPortsOption = false;

        public bool includeAllPortsOption
        {
            get { return _includeAllPortsOption; }
            set { _includeAllPortsOption = value; ChangePortfolio(); }
        }

        private bool _includeMHConsolOption = false;

        public bool includeMHConsolOption
        {
            get { return _includeMHConsolOption; }
            set { _includeMHConsolOption = value; ChangePortfolio(); }
        }

        private int _IdPortType = 2;

        public int IdPortType
        {
            get { return _IdPortType; }
            set { _IdPortType = value; ChangePortfolio(); }
        }

        

        public NestPortCombo()
        {
            ChangePortfolio();
        }

        public void ChangePortfolio()
        {
            string tempUnions = "";
            if (_includeAllPortsOption) { tempUnions = tempUnions + " UNION SELECT -1, 'All Portfolios' "; }
            if (_includeAllPortsOption) { tempUnions = tempUnions + " UNION SELECT -2, 'MH Consolidated' "; }

            LiveDLL.FormUtils.LoadCombo(this, "SELECT Id_Portfolio,Port_Name FROM VW_Portfolios WHERE Id_Port_Type=" + _IdPortType + tempUnions, "Id_Portfolio", "Port_Name", 99);
        }


    }
}
