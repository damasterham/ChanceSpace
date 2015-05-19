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
}
