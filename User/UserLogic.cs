//using System.Web;
//using System.Collections.Generic;
//using System.Web.SessionState;

//namespace ChanceSpace.WebUser
//{
//    /// <summary>
//    /// LoginLogic Version 1.02
//    /// </summary
//    public class WebUser
//    {
//        #region Fields
//        private SqlHandler _handler;
//        private string _loginId;
//        private string _pass;

//        private string _name;
//        private string _fname;
//        private string _lname;
//        private string _id;
//        private string _role;

//        private Dictionary<string, string> _columnName = new Dictionary<string, string> 
//        {
//            {"loginId", "name"},
//            {"pass", "pass" },
//            {"role", "fkRole"},
//            {"firstName", "firstName"},
//            {"lastName", "lastName"},
//            {"email", "email"},
//            {"address", "address"},
//            {"date", "date"},
//            {"image", "fkImage"}
//        };
        


//        private HttpContext _context = HttpContext.Current;
//        private HttpSessionState _sesh;
//        private string _seshName;
//        #endregion _Fields_

//        #region Constructors
//        //public SimpleUser() : this("DbConnectionString", "Login") { }
//        //public SimpleUser(string connectionString, string storedProcedure, string session = "user")
//        //{
//        //    Crud.ConnectionString = connectionString;
//        //    login = new Crud();
//        //    login.StoredProcedure = storedProcedure;
//        //}
//        #endregion _Constructors_

//        #region Properties
//        public string FirstName { get { return this._fname; } set { this._fname = value; } }
//        public string LastName { get { return this._lname; } set { this._lname = value; } }
//        public string FullName { get { return this._fname + " " + this._lname; } }
//        public string UserName { get { return this._user; } set { this._user = value; } }
//        public string Id { get { return this._id; } set { this._id = value; } }
//        public string Role { get { return this._role; } set { this._role = value; } }
//        #endregion _Properties_

//        #region Methods
//        public void Login(string user, string pass)
//        {
//            login.AddParameter(user, "name", "nvarchar", 50);
//            login.AddParameter(pass, "pass", "nvarchar", 50);
//        }
//        //public void Login(string user, string pass, string userPara, string passPara)
//        //{
//        //    login.AddParameter(user, userPara, "nvarchar", 50);
//        //    login.AddParameter(pass, passPara, "nvarchar", 50);
//        //}
//        //public void Login(string user, string pass, string userPara, string passPara, string sqlType)
//        //{
//        //    login.AddParameter(user, userPara, sqlType, 50);
//        //    login.AddParameter(pass, passPara, sqlType, 50);
//        //}
//        //public void Login(string user, string pass, string userPara, string passPara, int length)
//        //{
//        //    login.AddParameter(user, userPara, "nvarchar", 50);
//        //    login.AddParameter(pass, passPara, "nvarchar", 50);
//        //}
//        //public void Login(string user, string pass, string userPara, string passPara, string userSqlType, string passSqlType, int userLength = 50, int passLength = 50)
//        //{
//        //    login.AddParameter(user, userPara, userSqlType, userLength);
//        //    login.AddParameter(pass, passPara, passSqlType, passLength);
//        //}
//        //private void Start()
//        //{
//        //    Debug.WriteLine("Login");
//        //}
//        private void Read()
//        {
//            login.Open();
//            login.ReadRow();
//            login.Close();

//            if (login.Row["id"] != null) this.Id = login.Row["id"];
//            if (login.Row["name"] != null) this.UserName = login.Row["name"];
//            if (login.Row["firstName"] != null) this.FirstName = login.Row["firstName"];
//            if (login.Row["lastName"] != null) this.LastName = login.Row["lastName"];



//        }
//        #endregion _Methods_
//    }
//}


//        //public static class LoginStuff
//        //{
//        //    public static void Yay()
//        //    {
//        //        Debug.WriteLine("Yay");
//        //    }
//        //}

//        //public static class LoginUser
//        //{

//        //}

//        //public class SiteUser
//        //{
//        //     // Constructor
//        //    public SiteUser(string name = "name", string id = "id", string role = null)
//        //    {
//        //        this.Name = name;
//        //        this.Id = id;
//        //        this.Role = role;
//        //    }

//        //    private string _name;
//        //    private string _id;
//        //    private string _role;

//        //    // Properties
//        //    public string Name { get { return this._name; } set { this._name = value; } }
//        //    public string Id { get { return this._id; } set { this._id = value; } }
//        //    public string Role { get { return this._role; } set { this._role= value; } }
//        //    }

//        /// <summary>
//        /// UserObj Version 1.06
//        /// </summary>

   




//        #region OLD USER OBJECT

//        //public class TheUser
//        //{
//        //    #region Constructor
//        //     // Constructor

//        //    // string name, string pass, string colName = "name", string colId = "id", string sqlParaName = "name", string sqlParapass = "pass")

//        //    public TheUser(string name, string pass)
//        //    {
//        //        // Adds Users name as parameter with standard parameter settings
//        //        this.LoginQry.AddParameter(name, "name", "nvarchar", 50);
//        //        // Adds Users password as parameter with standard parameter setting
//        //        this.LoginQry.AddParameter(pass, "pass", "nvarchar", 50);

//        //        this.Name = "name";
//        //        this.Id = "id";
//        //        this.Session = "User";
//        //        this.LoginQry.StoredProcedure = "Login";
//        //    }

//        //    // Default Constructor
//        //    public TheUser() { this.Name = "name"; this.Id = "id"; this.Session = "User"; this._propsToSet = new List<string>(); _propsToSet.Add(Name); _propsToSet.Add(Id); } // Will have to manually add Name and Pass parameters
//        //    #endregion


//        //    #region Properties
//        //    // Properties
//        //    public Crud LoginQry = new Crud();
//        //    public Object ErrorMsg; 
//        //    public string Id { get; set; }
//        //    public string Name { get; set; }

//        //    private List<string> _propsToSet;

//        //    public string Session { get; set; }

//        //   // private string _pass;

//        //   //public string Pass {get; set; }

//        //    #endregion

//        //    #region Methods

//        //    // Perhaps read in properties instead of this class
//        //    public void Login()
//        //    {
//        //        bool error = false;

//        //        //this.LoginQry.ReadToObject(this, error);
//        //        HttpContext.Current.Session[this.Session] = this;

//        //        if(error)
//        //        {
//        //            HttpContext.Current.Response.Write(Vali.ErrorMsg["US_LoginFail"]);
//        //        }
//        //    }


//        //    public void TheProps()
//        //    {
//        //        foreach (var prop in this.GetType().GetProperties())
//        //        {

//        //            HttpContext.Current.Response.Write(prop.Name + " " + prop.GetValue(this) + "<br/>");
//        //        }
//        //    }

//        //    #endregion

//        //}

//        //public static void Login(TheUser userObj, string qry = "Login", bool isStoredProcedure = true)
//        //{
//        //     "Potentially" Reads to this User class, so a instatiated object of User 
//        //    login.ReadToObject(qry, userObj);
//        //    HttpContext.Current.Session["User"] = ;

//        //     Removes password from object
//        //    this.Pass = null;
//        //}

//        //private void GetRights(string qry = "GetRights")
//        //{
//        //    Crud rights = new Crud(this.Id);
//        //    rights.ReadToObject(qry, rights);
//        //}
//        #endregion


       
