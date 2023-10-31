using System;
using System.Collections.Generic;
using System.Text;

namespace NestQuant.Common
{
    public class Signal_Table : Price_Table
    {
        public Signal_Table(string _Name, int _Id_Ticker_Template, DateTime _iniDate, DateTime _endDate, bool isRealTime)
            : base(_Name, _Id_Ticker_Template, _iniDate, _endDate, isRealTime)
        {
        }

        public Signal_Table(string _Name, int _Id_Ticker_Template, DateTime _iniDate, DateTime _endDate)
            : base(_Name, _Id_Ticker_Template, _iniDate, _endDate)
        {
        }
        
        public Signal_Table(string _Name, int Id_Ticker_Template, bool isRealTime)
            : base(_Name, Id_Ticker_Template, isRealTime)
        {            
        }

        public Signal_Table(string _Name, int Id_Ticker_Template)
            : base(_Name, Id_Ticker_Template)
        {
        }

        /// <summary>
        /// Fills the Signal Table based on the selected scheme
        /// </summary>
        /// <param name="id_Ticker_Composite">Composite to create table</param>
        /// <param name="positionScheme">Filling scheme</param>
        public void FillPositionFromComposite(int id_Ticker_Composite, Utils.PositionSchemes positionScheme)
        {
            ZeroFillFromComposite(id_Ticker_Composite);

            double position;

            switch (positionScheme)
            {
                case Utils.PositionSchemes.Long:
                    position = 1F;
                    break;
                case Utils.PositionSchemes.Flat:
                    position = 0F;
                    break;
                case Utils.PositionSchemes.Short:
                    position = -1F;
                    break;
                default:
                    position = 0F;
                    break;
            }

            for (int i = 0; i < DateRowCount; i++)
            {
                for (int j = 0; j < ValueColumnCount; j++)
                {
                    SetValue(i, j, position);
                }
            }                        
        }
    }
}
