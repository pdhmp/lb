using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderSender
{
    public class Order
    {

        #region Attributes

        private int _IdOrder;
        private int _IdSecurity;
        private double _Price;
        private int _OrderQty;
        private int _UnordQty;
        private int _Leaves;
        private int _ExecQty;
        private double _ExecValue;
        private int _IdPortfolio;
        private int _IdBook;
        private int _IdSection;
        private DateTime _OrderDate;
        private string _ClOrdID;
        private string _ExecStrategy;        
        private string _Param1;
        private int _DontMatch;

        public string Param1
        {
            get { return _Param1; }
            set { _Param1 = value; }
        }
        private string _Param2;

        public string Param2
        {
            get { return _Param2; }
            set { _Param2 = value; }
        }

        private string _Param3;

        public string Param3
        {
            get { return _Param3; }
            set { _Param3 = value; }
        }

        private string _Param4;

        public string Param4
        {
            get { return _Param4; }
            set { _Param4 = value; }
        }



        #endregion

        #region Properties

        public int IdOrder
        {
            get { return _IdOrder; }
            set { _IdOrder = value; }
        }        
        public int IdSecurity
        {
            get { return _IdSecurity; }
            set { _IdSecurity = value; }
        }
        public double Price
        {
            get { return _Price; }
            set { _Price = value; }
        }
        public int OrderQty
        {
            get { return _OrderQty; }
            set { _OrderQty = value; }
        }
        public int UnordQty
        {
            get { return _UnordQty; }
            set { _UnordQty = value; }
        }
        public int Leaves
        {
            get { return _Leaves; }
            set { _Leaves = value; }
        }
        public int ExecQty
        {
            get { return _ExecQty; }
            set { _ExecQty = value; }
        }
        public double ExecValue
        {
            get { return _ExecValue; }
            set { _ExecValue = value; }
        }
        public int IdPortfolio
        {
            get { return _IdPortfolio; }
            set { _IdPortfolio = value; }
        }
        public int IdBook
        {
            get { return _IdBook; }
            set { _IdBook = value; }
        }
        public int IdSection
        {
            get { return _IdSection; }
            set { _IdSection = value; }
        }
        public DateTime OrderDate
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }
        public string ClOrdID
        {
            get { return _ClOrdID; }
            set { _ClOrdID = value; }
        }
        public string ExecStrategy
        {
            get { return _ExecStrategy; }
            set { _ExecStrategy = value; }
        }
        public int DontMatch
        {
            get { return _DontMatch; }
            set { _DontMatch = value; }
        }

        #endregion

    }
}
