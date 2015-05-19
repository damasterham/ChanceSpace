using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
// Database namespaces
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;

using System.Diagnostics;
namespace ChanceSpace
{
   
    /// <summary>
    /// CRUD Version 1.1.0.3
    /// Crud is a sql database query handler meant for an easy process of running sql commands.
    /// When using Crud start by setting the DbConfig class's Connection string in the beginning of your page, as this is needed for the Crud class to function.
    /// </summary>
    public class Crud
    {
        #region Fields
        private SqlConnection _conn = new SqlConnection();
        private SqlCommand _cmd = new SqlCommand();
        private SqlDataReader _rdr;
        private DataSet _dataSet;
        private DataTable _dataTable;
        private Dictionary<string, string> _dic = new Dictionary<string,string>();
        private string _queryString;
        private string _storedProcedure;
        private bool _isStoredProcedure;
        private bool _error;
        private bool paraError = false;
        private bool _connOpen = false;
        private static string _connectionString = "DatabaseConnectionString";
        #endregion _Fields_

        #region Constructors
        /// <summary>
        /// Defualt Constructor. Uses the static ConnectionString.
        /// </summary>
        public Crud()
        {
            Debug.WriteLine("Default Constructor");
            SetConnString(_connectionString);
            //_rdr = _cmd.ExecuteReader();
        }

        /// <summary>
        /// ConnectionString Constructor. Add a custom connection string to use insted of the static ConnectionString.
        /// </summary>
        /// <param name="connectionString">The connection string to your database</param>
        public Crud(string connectionString)
        {
            Debug.WriteLine("ConnString Constructor");
            SetConnString(connectionString);
            //_rdr = _cmd.ExecuteReader();
        }
        /// <summary>
        /// Id constructor. Creates an instance of Crud with an id parameter. Used for specific sql commands.
        /// </summary>
        /// <param name="id">The specific id you want to use for sql commands</param>
        /// <param name="sqlParameter">The id's sql parameter, defaulted to "id"</param>
        public Crud(string id, string sqlParameter = "id") : this()
        {
            Debug.WriteLine("Id Constructor");
            AddParameter(id, sqlParameter, "int");
        }
        /// <summary>
        /// Id constructor with custom connection string. Creates an instance of Crud with an id paramater and a custom connection string. Used for specific sql commands.
        /// </summary>
        /// <param name="connectionString">The custom connection string</param>
        /// <param name="id">The specific id you want to use for sql commands</param>
        /// <param name="sqlParameter">The id's sql parameter, defaulted to "id"</param>
        public Crud(string connectionString, string id, string sqlParameter = "id") : this(connectionString)
        {
            Debug.WriteLine("Id Constructor");
            AddParameter(id, sqlParameter, "int");
        }

        //public Crud(string storedProcedure) { StoredProcedure = storedProcedure; }
       
        // Constructor Overload +1
        //public Crud(string mainId, string para) // Can take a string instead of an int
        //{
        //    // If id is supllied add it to the list of parameters
        //    if (mainId != "0")
        //    {
        //        AddParameter(mainId, para, "int");
        //    }
        //}

        // Default Contructor: 0 parameters'
        #endregion _Constructors_

        #region Properties
        

        /// <summary>
        /// The connection string to your database. The default value is "DatabaseConnectionString". Setting this sets all the connection strings for all Crud objects
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        /// <summary>
        /// Get Dataset. Used for databinding to data controls, such as gridviews.
        /// </summary>
        public DataSet DataSet { get { return this._dataSet; } }
        /// <summary>
        /// Get DataTable. Used for databinding to data controls, such as gridviews.
        /// </summary>
        public DataTable DataTable { get { return this._dataTable; } }
        /// <summary>
        /// Get the SqlDataReader to gain direct access to a dictionary of your sql request.
        /// </summary>
        public SqlDataReader Rdr { get { return this._rdr; } }
        public Dictionary<string, string> Row { get { return this._dic; } }
        public bool Error { get { return this._error; } }

        /// <summary>
        /// Sets the query string of the instance. Setting the QueryString Property will nullify the StoredProcedure Property and toggle what type of query will be used.
        /// </summary>
        public string QueryString
        {
            get { return this._queryString; }
            set { this._isStoredProcedure = false; if (this._storedProcedure != null)this._storedProcedure = null; this._queryString = value; }
        }
        /// <summary>
        /// Sets the stored procedure of the instance. Setting the StoedProcedure Property will nullify the QueryString Property and toggle what type of query will be used.
        /// </summary>
        public string StoredProcedure
        {
            get { return this._storedProcedure; }
            set { this._isStoredProcedure = true; if (this._queryString != null)this._queryString = null; this._storedProcedure = value; }
        }

        // Object used for ReadToObject()
        //Object _obj;
        //public Object Obj
        //{
        //    get { return this._obj; }
        //    set { this._obj = value; }
        //}

        #endregion

        #region Sql Parameters
        /// <summary>
        /// Adds a parameter to the list of sql parameters
        /// </summary>
        /// <param name="value">The value you want to store in a database</param>
        /// <param name="sqlpara">The parameter name used in your sql command. Example "@id". The @ is not needed</param>
        /// <param name="sqltype">The data type used in your sql command. Can be: Int, nvarchar, ntext, datetime and bit</param>
        /// <param name="length">The length of you value. Only needed if the datatypes requires you to define length</param>
        public void AddParameter(string value, string sqlpara, string sqltype, int length = 0)
        {
            if (value != null && value != "")
            {
                SqlDbType datatype = SqlDbType.Int;
                switch (sqltype)
                {
                    case "int":
                        datatype = SqlDbType.Int;
                        break;
                    case "nvarchar":
                        datatype = SqlDbType.NVarChar;
                        break;
                    case "ntext":
                        datatype = SqlDbType.NText;
                        break;
                    case "datetime":
                        datatype = SqlDbType.DateTime;
                        break;
                    case "bit":
                        datatype = SqlDbType.Bit;
                        break;
                }
                SqlParameter para = new SqlParameter();
                para.Value = value;
                if (sqlpara.Substring(0, 1) == "@") para.ParameterName = sqlpara; 
                else para.ParameterName = "@" + sqlpara;
                para.SqlDbType = datatype;
                if (length != 0) para.Size = length;
                Debug.WriteLine(para.Value.ToString() + para.ParameterName + para.SqlDbType.ToString() + para.Size.ToString());

                _cmd.Parameters.Add(para);
            }
            else
            {
                paraError = true;
            }

        }
        public void AddDate(DateTime date, string sqlpara) { _cmd.Parameters.Add(date.ToString(), SqlDbType.DateTime, 0, sqlpara); }
        #endregion _Sql Parameters_

        #region Crud

        #region Misc Methods
        /// <summary>
        /// Checks which type of sql command shall be done. 
        /// </summary>
        private void CmdType()
        {
            if (_isStoredProcedure)
            {
                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.CommandText = this._storedProcedure;
            }
            else if (!_isStoredProcedure) { _cmd.CommandText = this._queryString; }
        }
        private void ParameterCheck()
        {
            if (paraError)
            {
                Debug.WriteLine("There was an error creating the parameters of this crud instance");
                return;
            }
        }
        private void SetConnString(string connectionString)
        {
            bool connExists = false;
            if (connectionString != null && connectionString != "")
            {
                foreach (ConnectionStringSettings con in ConfigurationManager.ConnectionStrings)
                {
                    if (con.Name == connectionString) connExists = true;
                }
            }
            if (connExists)
            {
                _conn.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
                _cmd.Connection = _conn;
            }
            else Debug.WriteLine("Your connection string does not exist in the WebConfig");
        }


        /// <summary>
        /// Binds selected data to a gridview
        /// </summary>
        /// <param name="gridView">The gridview you want to bind data to</param>
        public void BindTo(GridView gridView)
        {
            gridView.DataSource = _dataSet;
            gridView.DataBind();
        }
        /// <summary>
        /// Binds selected data to a repeater
        /// </summary>
        /// <param name="repeater">The repeater you want to bind data to</param>
        public void BindTo(Repeater repeater)
        {
            repeater.DataSource = _dataSet;
            repeater.DataBind();
        }
        public void Open() { _conn.Open(); _connOpen = true; }
        public void Close() { _conn.Close(); _connOpen = false; }


        #endregion _Misc Methods_

        #region NonQuerys
        /// <summary>
        /// Execute a sql non-query. Used for Create-, Delete- and Update sql commands.
        /// </summary>
        public void Execute()
        {
            Debug.WriteLine("ExecuteQuery");
            CmdType();
            if (_connOpen) _cmd.ExecuteNonQuery();
            else
            {
                _conn.Open();
                _cmd.ExecuteNonQuery();
                _conn.Close();
            }
        }
        
        #endregion _NonQuerys_

        #region Readers
        /// <summary>
        /// Reads selected data to the Data property. Used for Select sql commands.
        /// </summary>
        public void ReadToDataSet()
        {
            Debug.WriteLine("Reader - ReadToDataSet");
            CmdType();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(_cmd);
            adap.Fill(ds);
            this._dataSet = ds;
        }
        public void ReadToDataTable()
        {
            Debug.WriteLine("Reader - ReadToDataSet");
            CmdType();
            DataTable dt = new DataTable();
            SqlDataAdapter adap = new SqlDataAdapter(_cmd);
            adap.Fill(dt);
            this._dataTable = dt;
        }
        public bool ReaderCheck()
        {
            Debug.WriteLine("Reader - ReaderCheck");
            CmdType();
            _conn.Open();
            _rdr = _cmd.ExecuteReader();
            if (_rdr.Read()) return true;
            _conn.Close();
            return false;
        }

        public void ReadRow()
        {
            Debug.WriteLine("Reader - Rdr");
            CmdType();
            _rdr = _cmd.ExecuteReader();
            if (_rdr.Read())
            {
                Debug.WriteLine(_rdr.FieldCount.ToString());
                for (int i = 0; i < _rdr.FieldCount; i++)
                {
                    Debug.WriteLine(i.ToString());
                    Debug.WriteLine(_rdr.GetName(i) + _rdr[i].ToString());
                    _dic.Add(_rdr.GetName(i), _rdr[i].ToString());
                }

            }
            else _error = true;
        }
        //public void ReadMultiple()
        //{
        //    CmdType();
        //    _rdr = _cmd.ExecuteReader();
        //}

        //public void ReadToPropList(string qry)
        //{

        //}

        // Select from sql database and gets and sets the properties of a passed object

        //public void Read()
        //{
        //    Debug.WriteLine("Reader - Read to rdr");
        //    CmdType();
        //    _conn.Open();
        //}
        public void ReadToObject(Object obj)
        {
            Debug.WriteLine("Reader - ReadToObject");
            CmdType();
            _conn.Open();
            Debug.WriteLine("Conn open");
            // Creates a reader object that can execute reader
            SqlDataReader rdr = _cmd.ExecuteReader();
            Debug.WriteLine("Reader Instatiated");

            if (rdr.HasRows)
            {
                Debug.WriteLine("Reader has rows");

                //int i = 0;
                // While the reader reads
                while (rdr.Read())
                {

                    //Debug.WriteLine("Reading begins");

                    // Gets each properties of a given object
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        //i++;
                        Debug.WriteLine("Get Property value");
                        // Gets the value of the property wich will contain the table's cell name
                        string col = Convert.ToString(prop.GetValue(obj));
                        // Sets the value of the property to what has been read from the database
                        // if the property containded a collumn name
                        //if (rdr.GetName(i).Equals(col, StringComparison.InvariantCultureIgnoreCase))

                        Debug.WriteLine("Collumn = " + col + "!");
                        if (col != "")
                        {
                            if (rdr[col].ToString() != null)
                            {
                            prop.SetValue(obj, rdr[col].ToString());
                            Debug.WriteLine("Column name = " + col + " got value = " + rdr[col].ToString());
                            }
                            //else
                            //    Debug.WriteLine("Column was not present");
                        }
                        else
                            Debug.WriteLine("Column property text was not present");
                    }
                }
            }
            else
            {
                Debug.WriteLine("Some rows were non exsistant");
                this._error = true;
            }

            _conn.Close();
        }

        public void ReadToDictionary(Dictionary<string, string> array)
        {
            Debug.WriteLine("Reader - ReadToDictionary");
            CmdType();

            _conn.Open();
            Debug.WriteLine("Conn open");
            SqlDataReader rdr = _cmd.ExecuteReader();
            Debug.WriteLine("Reader Instatiated");

            if (rdr.HasRows)
            {
                Debug.WriteLine("Reader has rows");

                while (rdr.Read())
                {
                    List<string> columns = new List<string>();
                    List<string> keys = new List<string>();


                    foreach (KeyValuePair<string, string> entry in array)
                    {
                        keys.Add(entry.Key);
                        columns.Add(entry.Value);
                    }

                    using (var e1 = keys.GetEnumerator())
                    using (var e2 = columns.GetEnumerator())
                    {
                        Debug.WriteLine("Start looping through keys and collumns");
                        while (e1.MoveNext() && e2.MoveNext())
                        {
                            string col = e2.Current;
                            string key = e1.Current;

                            if (rdr[col].ToString() != null)
                            {
                                array[key] = rdr[col].ToString();
                                Debug.WriteLine("Column name " + col + " was written");
                                Debug.Write(" - and was added to dic with key " + key);

                            }
                            else
                            {
                                Debug.WriteLine("Column was not present");
                            }
                        }
                    }
                }
            }
            else
            {
                Debug.WriteLine("Some rows were non exsistant");
                this._error = true;
            }

            _conn.Close();
        }

        #endregion _Readers_

        #endregion _Crud_
    }
                  
        //SqlConnection conn;
        //SqlCommand cmd;
        //DbConn(out conn, out cmd);
                  
        //*/


        ///// <summary>
        ///// Handles the database connection and connectionstring
        ///// </summary>
        //public class DbConfig
        //{
        //    /// <summary>
        //    /// Set the connection string.
        //    /// </summary>
        //    public static string ConnectionString = "DatabaseConnectionString";

        //    // Connects to your database and gives you sqlcommands as cmd and sqlconnections as conn
        //    public static void DbConn(out SqlConnection conn, out SqlCommand cmd)
        //    {
        //        conn = new SqlConnection();
        //        _conn.ConnectionString = ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
        //        cmd = new SqlCommand();
        //        _cmd.Connection = conn;
        //    }
        //}
        //#endregion
    
    // Crud End
}