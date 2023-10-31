using System;
using System.Collections.Generic;
using System.Text;


namespace Dashboards
{
    class QSEGS
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double ValueDm1 { get; set; }
        public double Var { get; set; }

        public QSEGS(string sName, double sValue, double sValueDm1)
        {
            this.Name = sName;
            this.Value = sValue;
            this.ValueDm1 = sValueDm1;
            if (sValueDm1 != 0)
            {
                this.Var = sValue / sValueDm1 - 1;
            }
        }
        //setters
        public void setName(string sName)
        {
            this.Name = sName;
        }
        public void setValue(double dValue)
        {
            this.Value = dValue;
            if (this.ValueDm1 != 0)
            {
                this.Var = Math.Round((this.Value / this.ValueDm1 - 1)*100,2);
            }
        }
        public void setValueDm1(double dValueDm1)
        {
            this.ValueDm1 = dValueDm1;
            if (this.ValueDm1 != 0)
            {
                this.Var = Math.Round((this.Value / this.ValueDm1 - 1)*100,2);
            }
        }
        public string getName()
        {
            return this.Name;
        }
    }
}
