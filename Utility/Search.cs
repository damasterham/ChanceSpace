using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChanceSpace.Utility
{
    /// <summary>
    /// Sql Search Class
    /// </summary>
    class Search
    {
        #region Fields
        private SqlHandler _handler;
        private string _text;
        
        //private Delegate _searched:
        #region _Fields_

        #region Constructors
        public Search()
        {
            _handler = new SqlHandler();
        }
        public Search(string connectionString)
        {
            _handler = new SqlHandler(connectionString);
        }
        #endregion _Constructors_

        #region Properties
        public string Text { get { return _text; } set { _text = value; } }
        public string SearchQuery
        {
            get { return _handler.QueryString;  }
            set { _handler.QueryString = value; }
        }
        public string SearchProcedure
        {
            get { return _handler.StoredProcedure; }
            set { _handler.StoredProcedure = value; }
        }
        #endregion _Properties_

        #region Methods
        //public void SearchQuery(string query)
        //{
        //    _handler.QueryString = query;
            
        //}
        #endregion _Methods_





    }
}
