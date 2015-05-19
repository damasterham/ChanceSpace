using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceSpace
{/*
    public class QueryConstructor
    {
        #region Constructors
        public QueryConstructor(string queryType) 
        {
            this.QueryType = queryType;
            this._selectAllColumns = true;
        }
        public QueryConstructor() { }
        #endregion

        #region Property
        private string _queryString;
        public string QueryString
        {
            get { return this._queryString; }
            set { this._queryString = value; }
        }

        #region SELECT structure

        public string SelectQry { get; set; }
        private List<string> _innerJoins;
        public List<string> InnerJoins { get; set; }



        #endregion


        #region Query Variables
        public string QueryType { get; set; } // "SELECT" var(list) "FROM"
        
        private List<string> _selectColumns;
        public string SelectColumns // SELECT "a1, a2, a3" FROM
        {
            get
            {
                string columns ="";
                foreach(string col in _selectColumns)
                {
                    //if (_selectColumns.Count.Last() = _selectColumns[_selectColumns.Count - 1])
                    //columns += col;
                }
            }
            //set { this._selectColumns = value; this._selectAllColumns = false; }
        }

        private string _selectAllColumnsStr = "*";
        private bool _selectAllColumns; // SELECT "*" FROM  
        public string MainTable { get; set; } // FROM "table"


        #endregion

        #endregion

        public class Select
        {

        }


        #region Methods
        public void AddSelectedColumn(string columnName)
        {
            _selectColumns.Add(columnName);
        }
        #endregion



    }*/
}
