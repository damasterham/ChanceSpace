using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;

namespace ChanceSpace
{    
    /// <summary>
    /// SqlHandler Version 1.0.2.0
    /// SqlHandler is a sql database query handler meant for an easy process of running sql commands.
    /// </summary>
    public class SqlHandler
    {
        #region Fields
        private static string _globalConnectionString = "DatabaseConnectionString";
        private string _connectionString;
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;
        bool _open;
        #endregion _Fields_

        #region Constructors
        /// <summary>
        /// Sets the SqlHandler's ConnectionString with the value of GlobalConnectionString (Default value="DbConnectionString") 
        /// and initializes the Connenction and Command properties.
        /// </summary>
        public SqlHandler() : this(_globalConnectionString){ }
        /// <summary>
        /// Sets the SqlHandler's ConnectionString
        /// and initializes the Connenction and Command properties
        /// </summary>
        /// <param name="connectionString">The Connection String used to acces your Database. (Remmember to check your WebConfig)</param>
        public SqlHandler(string connectionString)
        {
            this._connectionString = connectionString;
            this._connection = new SqlConnection();
            this._command = new SqlCommand();
            this._connection.ConnectionString = ConfigurationManager.ConnectionStrings[connectionString].ConnectionString;
            this._command.Connection = _connection;
        }
        #endregion _Constructors_

        #region Properties
        /// <summary>
        /// Get or Set the GlobalConnectionString, a static ConnectionString that is Global for all SqlHandlers on the page.
        /// It is the default value of ConnectionString if other is not aplied via Constructor or ConnectionString Property.
        /// </summary>
        public static string GlobalConnectionString { get { return _globalConnectionString; } set { _globalConnectionString = value; } }
        /// <summary>
        /// Get or Set the ConnectionString that connects to the database. Must correspond a connectionstring in webconfig.
        /// </summary>
        public string ConncetionString { get { return this._connectionString; } set { this._connectionString = value; } }
        /// <summary>
        /// Get or Set the SqlConnection;
        /// </summary>
        public SqlConnection Connection { get { return this._connection; } set { this._connection = value; } }
        /// <summary>
        /// Get or Set the SqlCommand
        /// </summary>
        public SqlCommand Command { get { return this._command; } set { this._command = value; } }
        /// <summary>
        /// Get or Set the SqlDataReader
        /// </summary>
        public SqlDataReader Reader { get { return this._reader; } set { this._reader = value; } }
        /// <summary>
        /// Get the CommandText or Set the CommandText and CommandType to Text.
        /// </summary>
        public string QueryString
        {
            get { return _command.CommandText; }
            set { this._command.CommandType = CommandType.Text; this._command.CommandText = value; }
        }
        /// <summary>
        /// Get the CommandText or Set the CommandText and CommandType to StoredProcedure.
        /// </summary>
        public string StoredProcedure
        {
            get { return _command.CommandText; }
            set { this._command.CommandType = CommandType.StoredProcedure; this._command.CommandText = value; }
        }
        #endregion _Properties_

        #region Methods
        /// <summary>
        /// Opens the connection to your database.
        /// </summary>
        public void Open() { _connection.Open(); _open = true; }
        /// <summary>
        /// Closes the connection to your database.
        /// </summary>
        public void Close() { _connection.Close(); _open = false; }
        /// <summary>
        /// Executes A NonQuery, Create, Update, Delete.
        /// </summary>
        public void ExecuteNonQuery()
        {
            if (_open) _command.ExecuteNonQuery();
            else
            {
                _connection.Open();
                _command.ExecuteNonQuery();
                _connection.Close();
            }
        }
        /// <summary>
        /// Executes a reader Stored in the Reader Property. Needs an open connection to function. Use Open() to open the connection and Close() to close it when you are done using the Reader Property.
        /// </summary>
        public void ExecuteReader() { this._reader = _command.ExecuteReader(); }
        /// <summary>
        /// Returns a DataTable with selected data.
        /// </summary>
        /// <returns>Selected query data</returns>
        public DataTable DataTable()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adap = new SqlDataAdapter(_command);
            adap.Fill(dt);
            return dt;
        }
        //public DataTable DataTable(DataTable newDt)
        //{
        //    DataTable dt = newDt;
        //    SqlDataAdapter adap = new SqlDataAdapter(_command);
        //    adap.Fill(dt);
        //    return dt;
        //}        
        /// <summary>
        /// Binds selected data to a GridView
        /// </summary>
        /// <param name="repeater">The GridView you want to bind data to</param>
        public void BindTo(GridView gridView)
        {
            gridView.DataSource = DataTable();
            gridView.DataBind();
        }
        /// <summary>
        /// Binds selected data to a Repeater
        /// </summary>
        /// <param name="repeater">The Repeater you want to bind data to</param>
        public void BindTo(Repeater repeater)
        {
            repeater.DataSource = DataTable();
            repeater.DataBind();
        }
        /// <summary>
        /// Advances the SqlDataReader to the next record
        /// </summary>
        /// <returns>True if a row has been read</returns>
        public bool Read()
        {
            return this._reader.Read();
        }

        #region Parameters
        #region Generic Add Methods
        /// <summary>
        /// Adds a Parameter to Commands SqlParameterCollection
        /// </summary>
        /// <param name="value">The value of the parameter</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="sqlDbType">The SqlDbType of the parameter</param>
        public void AddParameter(object value, string parameterName, SqlDbType sqlDbType)
        {
            SqlParameter para = new SqlParameter();
            para.Value = value;
            if (parameterName.Substring(0, 1) == "@") para.ParameterName = parameterName;
            else para.ParameterName = "@" + parameterName;
            para.SqlDbType = sqlDbType;
            _command.Parameters.Add(para);
        }
        /// <summary>
        /// Adds a Parameter to Commands SqlParameterCollection
        /// </summary>
        /// <param name="value">The value of the parameter</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="sqlDbType">The string name of a SqlDbType of the parameter</param>
        public void AddParameter(object value, string parameterName, string sqlDbType)
        {
                SqlDbType dataType = SqlDbType.Int;
                switch (sqlDbType)
                {
                    case "int":
                        dataType = SqlDbType.Int;
                        break;
                    case "nvarchar":
                        dataType = SqlDbType.NVarChar;
                        break;
                    case "ntext":
                        dataType = SqlDbType.NText;
                        break;
                    case "datetime":
                        dataType = SqlDbType.DateTime;
                        break;
                    case "bit":
                        dataType = SqlDbType.Bit;
                        break;
                }
                AddParameter(value, parameterName, dataType);
        }
        /// <summary>
        /// Adds a Parameter to Commands SqlParameterCollection
        /// </summary>
        /// <param name="value">The value of the parameter</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="sqlDbType">The SqlDbType of the parameter</param>
        /// <param name="length">The length of the parameter</param>
        public void AddParameter(object value, string parameterName, SqlDbType sqlDbType, int length)
        {
            SqlParameter para = new SqlParameter();
            para.Value = value;
            if (parameterName.Substring(0, 1) == "@") para.ParameterName = parameterName;
            else para.ParameterName = "@" + parameterName;
            para.SqlDbType = sqlDbType;
            para.Size = length;
            _command.Parameters.Add(para);
        }
        /// <summary>
        /// Adds a Parameter to Commands SqlParameterCollection
        /// </summary>
        /// <param name="value">The value of the parameter</param>
        /// <param name="parameterName">The name of the parameter</param>
        /// <param name="sqlDbType">The string name of a SqlDbType of the parameter</param>
        /// <param name="length">The length of the parameter</param>

        public void AddParameter(object value, string parameterName, string sqlDbType, int length)
        {
                SqlDbType dataType = SqlDbType.Int;
                switch (sqlDbType)
                {
                    case "int":
                        dataType = SqlDbType.Int;
                        break;
                    case "nvarchar":
                        dataType = SqlDbType.NVarChar;
                        break;
                    case "ntext":
                        dataType = SqlDbType.NText;
                        break;
                    case "datetime":
                        dataType = SqlDbType.DateTime;
                        break;
                    case "bit":
                        dataType = SqlDbType.Bit;
                        break;
                }
                AddParameter(value, parameterName, dataType, length);
        }
        #endregion _Generic Add Methods_
        #region Custom Add Methods
        public void AddId(string value)
        {
            AddParameter(value, "@id", SqlDbType.Int);
        }
        //public void AddNVarChar50(string value, string parameterName)
        //{
        //    AddParameter(value, "@id", SqlDbType.Int);
        //} 
        #endregion _Custom Add Methods_
        #endregion _Parameters_

        #endregion _Methods_


        //public SpecificParameter AddSpecificParameter { get; set; }
        //public class SpecificParameter : SqlHandler
        //{
        //    public void NVarChar50(string value, string parameterName)
        //    {
        //        AddParameter(value, parameterName, SqlDbType.NVarChar, 50);
        //    }
        //}

    }
}
