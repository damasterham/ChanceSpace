using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace ChanceSpace
{
    /// <summary>
    /// A static Utility Class for various web utility and shortcuts
    /// </summary>
    public static class Utility
    {
        /// <summary>
        ///Reloads the page. Redirects you to the RawUrl of the current page.
        /// </summary>
        /// <param name="page">The aspx page you want to reload/redirect to</param>
        public static void ReloadPage(Page page)
        {
            page.Response.Redirect(page.Request.RawUrl);
        }
    }
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

}

