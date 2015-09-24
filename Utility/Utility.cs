using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Security.Cryptography;
using System.Web.Security;
using System.Collections;
using System.Data.SqlClient;
using System.Web.UI.WebControls;



namespace ChanceSpace
{
    /// <summary>
    /// A static Utility Class for various web utility and shortcuts
    /// </summary>
    public static class Utility
    {
        #region PageHandling
        /// <summary>
        ///Reloads the page. Redirects you to the RawUrl of the current page.
        /// </summary>
        /// <param name="page">The aspx page you want to reload/redirect to</param>
        public static void ReloadPage(Page page)
        {
            page.Response.Redirect(page.Request.RawUrl);
        }
        public static void ReloadPage()
        {
            HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);
        }
        public static string GetPageName()
        {
            return HttpContext.Current.Request.Url.ToString().Split('/').Last();
        }

        public void CurrentPageCss(List<HyperLink> hyperLinks, string currentPageString, string currentClass = "current")
        {
            foreach (HyperLink link in hyperLinks)
            {
                if (link.NavigateUrl == currentPageString) link.CssClass = currentClass;
            }
        }

        #endregion _PageHandling_

        #region TextHandling
        public static string TextEndRemoval(string text, int stepsBackInText, int deleteCount)
        {
            return text.Remove(text.Length - stepsBackInText, deleteCount);
        }
        public static string NumerateString(string text)
        {
            string str = "";
            int i = 0;
            foreach (char ch in text)
            {
                i++;
                str += i + ": " + ch + "<br/>";
            }
            return str;
        }
        #endregion _TextHandling_ 


        #region SecurityHandling
        public static string NewSimpleSalt()
        {
            return Guid.NewGuid().ToString();
        }
        public static string Hash(string password, string salt)

        {
            string str = "";
            string str2 = "";

            int i = 0;
            using(SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(password + salt));

                foreach (byte b in result)
                {
                    //str += i + ": " + b.ToString() + "<br/>";

                    str += b.ToString();
                }
                str += " " + hash.HashSize.ToString();
            }
            
            return str2 ;
        }
        public static string SimpleHash(string password, string salt)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(password + salt, "SHA1");
        }
    }
    #endregion _SecurityHandling_

    /// <summary>
    /// A static Class used to check database readers to see if they contain Null and return a apropriate defualt value to a given type
    /// </summary>
    public static class NotNull
    {
        /// <summary>
        /// Returns the string of a reader[index] if it is not null, else returns an empty string
        /// </summary>
        /// <param name="reader">The selected index of a Reader Array (Reader[index])</param>
        /// <returns></returns>
        public static string String(object reader)
        {
            if (reader != null) return reader.ToString();
            else return "";
        }
        /// <summary>
        /// Returns the int of a reader[index] if it is not null, else returns 0
        /// </summary>
        /// <param name="reader">The selected index of a Reader Array (Reader[index])</param>
        /// <returns></returns>
        public static int Int(object reader)
        {
            if (reader != null) return (int)reader;
            else return 0;
        }
        /// <summary>
        /// Returns the DateTime of a reader[index] if it is not null, else returns a new DateTime - 01-01-0001 00:00:00
        /// </summary>
        /// <param name="reader">The selected index of a Reader Array (Reader[index])</param>
        /// <returns></returns>
        public static DateTime Date(object reader)
        {
            if (reader != null) return (DateTime)reader;
            else return new DateTime();
        }
        /// <summary>
        /// Returns the Guid of a reader[index] if it is not null, else returns a new Guid - 0000-0000..
        /// </summary>
        /// <param name="reader">The selected index of a Reader Array (Reader[index])</param>
        /// <returns></returns>
        public static Guid Guid(object reader)
        {
            if (reader != null) return (Guid)reader;
            else return new Guid();
        }
    }

    public static class ParameterCol
    {
        public static List<SqlParameter> parameters = new List<SqlParameter>();
        //public static void Add();
    }
    

}

