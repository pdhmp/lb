using System;
using System.Collections.Generic;
using System.Text;
using NestQuant.Common;
using System.Data;

namespace NestQuant.Strategies
{
    public class Rates_BZ : Strategy
    {
        public Price_Table curAdjustSett;
        public Price_Table curNextDaySett;
        public Price_Table curRate;
        public Price_Table curRateT1;
        public Price_Table curExpirations;

        public Signal_Table curSignals;
        public Weight_Table curWeights;
        public PercPositions_Table subWeights; 

        public override void RunStrategy() 
        {
            // Create Object

            Weighing = Utils.WeighingSchemes.Undefined;

            curAdjustSett = new Price_Table("curOpenPrices", Id_Ticker_Template, iniDate, endDate);
            curNextDaySett = new Price_Table("curClosePrices", Id_Ticker_Template, iniDate, endDate);
            curRate = new Price_Table("curRate", Id_Ticker_Template, iniDate, endDate);
            curRateT1 = new Price_Table("curRateT1", Id_Ticker_Template, iniDate, endDate);
            curExpirations = new Price_Table("curExpirations", Id_Ticker_Template, iniDate, endDate);

            curSignals = new Signal_Table("curSignals", Id_Ticker_Template, iniDate, endDate);
            curWeights = new Weight_Table("subWeights", Id_Ticker_Template, iniDate, endDate);

            /*
            curOpenPrices.Id_Ticker_Composite = Id_Ticker_Composite;
            curClosePrices.Id_Ticker_Composite = Id_Ticker_Composite;
            curRate.Id_Ticker_Composite = Id_Ticker_Composite;
            curRateT1.Id_Ticker_Composite = Id_Ticker_Composite;
            curExpirations.Id_Ticker_Composite = Id_Ticker_Composite;
            */

            curAdjustSett.ZeroFillFromComposite(Id_Ticker_Composite);
            curNextDaySett.ZeroFillFromComposite(Id_Ticker_Composite);
            curRate.ZeroFillFromComposite(Id_Ticker_Composite);
            curRateT1.ZeroFillFromComposite(Id_Ticker_Composite);
            curExpirations.ZeroFillFromComposite(Id_Ticker_Composite);
            
            curSignals.ZeroFillFromComposite(Id_Ticker_Composite);
            curWeights.ZeroFillFromComposite(Id_Ticker_Composite);

            PriceTables.Add(curAdjustSett);
            PriceTables.Add(curNextDaySett);
            PriceTables.Add(curRate);
            PriceTables.Add(curRateT1);
            PriceTables.Add(curExpirations);

            DataTable dt = new DataTable();

            tkPerformances = new Performances_Table("tkPerformances", curAdjustSett, true);
            stPerformances = new Performances_Table("stPerformances", curAdjustSett, true);

            using (NestConn curConn = new NestConn())
            {
                dt = curConn.ExecuteDataTable("SELECT * FROM NESTDB.dbo.FCN_SIM_F5('" + iniDate.Add(new TimeSpan(-1, 0, 0, 0)).ToString("yyyy-MM-dd") + "') WHERE Data_Hora_Reg<'2008-12-31' ORDER BY Data_Hora_Reg");

                int i = 0;
                int curTicker = 0;

                foreach (DataRow curRow in dt.Rows)
                {
                    if(Convert.ToDateTime(curRow["Data_Hora_Reg"]) == Convert.ToDateTime(curAdjustSett.DateRows[i]))
                    {
                        curTicker = Convert.ToInt32(curRow["Id_Ativo"]);
                        curAdjustSett.SetValue(i, curAdjustSett.GetValueColumnIndex(curTicker), DBToDouble(curRow["AdjustSett"]));
                        curNextDaySett.SetValue(i, curNextDaySett.GetValueColumnIndex(curTicker), DBToDouble(curRow["Ajuste"]));
                        curRate.SetValue(i, curRate.GetValueColumnIndex(curTicker), DBToDouble(curRow["Valor"]));
                        curRateT1.SetValue(i, curRateT1.GetValueColumnIndex(curTicker), DBToDouble(curRow["NextDayRate"]));
                        curExpirations.SetValue(i, curExpirations.GetValueColumnIndex(curTicker), DBToDouble(curRow["Vencimento"]));

                        curSignals.SetValue(i, curAdjustSett.GetValueColumnIndex(curTicker), 1);
                        curWeights.SetValue(i, curAdjustSett.GetValueColumnIndex(curTicker), 1);
                        tkPerformances.SetValue(i, curAdjustSett.GetValueColumnIndex(curTicker), DBToDouble(curRow["Ajuste"]) / DBToDouble(curRow["AdjustSett"]) - 1);
                        stPerformances.SetValue(i, curAdjustSett.GetValueColumnIndex(curTicker), DBToDouble(curRow["Ajuste"]) / DBToDouble(curRow["AdjustSett"]) - 1);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Dates not aligned!"); ;
                    }
                    i++;
                }
            }

            curRate.AddRowSum();
            curExpirations.AddRowSum();

            curRate.AddCustomColumn("CHANGE");
            int rateColumn = curRate.GetCustomColumnIndex("SUM");
            int chgColumn = curRate.GetCustomColumnIndex("CHANGE");

            double prevVal = 0;
            double curVal = 0;
            double curChange = 0;

            for (int i = 0; i < curRate.DateRowCount; i++)
            {
                curVal = curRate.GetCustomValue(i, rateColumn);
                if (prevVal != 0) curChange = curVal - prevVal;
                curRate.SetCustomValue(i, chgColumn, Math.Round(curChange*100, 4));
                prevVal = curVal;
            }

            curRate.AddRollingDev("RATEDEV", 1, chgColumn, 12, false);
            int facColumn = curRate.AddCustomColumn("MULTFACTOR");

            //int facColumn = curRate.GetCustomColumnIndex("MULTFACTOR");
            int devColumn = curRate.GetCustomColumnIndex("RATEDEV");
            
            int xpColumn = curExpirations.GetCustomColumnIndex("SUM");

            for (int i = 0; i < curRate.DateRowCount; i++)
            {
                double curMult = Math.Pow(curRate.GetCustomValue(i, xpColumn)*100, 2);
                if(curMult > 20) curMult=20;
                double curXp = curExpirations.GetCustomValue(i, xpColumn);
                double curDurationMult = 365/(curXp - curExpirations.DateRows[i].ToOADate());
                curRate.SetCustomValue(i, facColumn, curMult * curDurationMult);
            }
            
            tkPositions = new PercPositions_Table(curSignals, curWeights);
            tkContributions = new Contributions_Table("tkContributions", tkPositions, tkPerformances);

            stPositions = new PercPositions_Table(curSignals, curWeights);
            stContributions = new Contributions_Table("stContributions", stPositions, stPerformances);

            stPerfSummary = new perfSummary_Table("stratPerfSummary", stContributions, stContributions);

        }

        private double DBToDouble(object ValueToAdjust)
        {
            if (DBNull.Value.Equals(ValueToAdjust))
            {
                return double.NaN;
            }
            if(ValueToAdjust.GetType() == typeof(double)) return Convert.ToDouble(ValueToAdjust);
            if(ValueToAdjust.GetType() == typeof(DateTime)) return ((DateTime)ValueToAdjust).ToOADate();
            return double.NaN;
        }


        private void Array_CalcSignals(Price_Table perfTable, Price_Table PE_Table)
        {
            for (int i = 1; i < PE_Table.DateRowCount; i++)
            {
                for (int j = 0; j < PE_Table.ValueColumnCount; j++)
                {
                    int curSignal = 0;
                    if (PE_Table.GetValue(i, j) == 0)
                    {
                        curSignal = 0;
                    }
                    else
                    {
                        if (PE_Table.GetValue(i, j) > PE_Table.GetCustomValue(i, 0))
                        {
                            curSignal = -1;
                        }
                        if (PE_Table.GetValue(i, j) < PE_Table.GetCustomValue(i, 0))
                        {
                            curSignal = 1;
                        }
                    }
                    perfTable.SetValue(i, j, curSignal);
                }
            }
        }

    }
}
