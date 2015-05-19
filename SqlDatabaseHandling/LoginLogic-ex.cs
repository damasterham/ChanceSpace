using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/*
namespace ChanceSpace
{
        /// <summary>
        /// UserObj Version 1.06 ex
        /// </summary>

    private class UserObj
    {

        // Constructor
        public UserObj(string name = "name", string id = "id")
        {
            this.Name = name;
            this.Id = id;
        }

        private string _name;
        private string _id;

        // Properties
        public string Name { get { return this._name; } set { this._name = value; } }
        public string Id { get { return this._id; } set { this._id = value; } }
    }




        #region OLD USER OBJECT

        public class UserHandler
        {
            #region Constructor
             // Constructor
            public UserHandler(string name, string pass, string colName = "name", string colId = "id")
            {
                // Adds Users name as parameter with standard parameter settings
                this.LoginQry.AddPara(name, "name", "nvarchar", 50);
                // Adds Users password as parameter with standard parameter setting
                this.LoginQry.AddPara(pass, "pass", "nvarchar", 50);

                //this.Name = "name";
                //this.Id = "id";
                this.Session = "User";
                this.LoginQry.StoredProcedure = "Login";

                TheUser = new UserObj(colName, colId);
            }

            // Default Constructor
            public UserLogin() { this.Name = "name"; this.Id = "id"; this.Session = "User"; this._propsToSet = new List<string>(); _propsToSet.Add(Name); _propsToSet.Add(Id); } // Will have to manually add Name and Pass parameters
            #endregion


            #region Properties
            // Properties
            public Crud LoginQry = new Crud();
            public UserObj TheUser;
            public string Id { get; set; }
            public string Name { get; set; }

            private List<string> _propsToSet;

            public string Session { get; set; }

           // private string _pass;

           //public string Pass {get; set; }

            #endregion

            #region Methods

            // Perhaps read in properties instead of this class
            public void Login()
            {
                this.LoginQry.ReadToObject(this.TheUser);
                

                HttpContext.Current.Session[this.Session] = this;
            }


            public void TheProps()
            {
                foreach (var prop in this.GetType().GetProperties())
                {

                    HttpContext.Current.Response.Write(prop.Name + " " + prop.GetValue(this) + "<br/>");
                }
            }

            #endregion

        }

        //public static void Login(TheUser userObj, string qry = "Login", bool isStoredProcedure = true)
        //{
        //     "Potentially" Reads to this User class, so a instatiated object of User 
        //    login.ReadToObject(qry, userObj);
        //    HttpContext.Current.Session["User"] = ;

        //     Removes password from object
        //    this.Pass = null;
        //}

        //private void GetRights(string qry = "GetRights")
        //{
        //    Crud rights = new Crud(this.Id);
        //    rights.ReadToObject(qry, rights);
        //}
        #endregion


}
       
*/