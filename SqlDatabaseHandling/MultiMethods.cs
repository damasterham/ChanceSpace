using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChanceSpace
{
    public class MultiMethods
    {

        public static void PrintText(string text)
        {
            //HttpContext.Current.Response.Write(text);
        }

        public static void PrintText(bool isCapital, string text)
        {
            string res;
            if (isCapital)
                res = text.ToUpper();

            else res = text.ToLower();

            //HttpContext.Current.Response.Write(res);
        }

    }
}