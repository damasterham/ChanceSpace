using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Validation Version 1.01
/// </summary>



public class Vali
{
    // Dictionary / Associative array of Error Messages
    public static readonly Dictionary<string, string> ErrorMsg = new Dictionary<string, string>
    {
        {"US_LoginFail", "Your username and/or password are incorrect"},
        {"DK_LoginFail", "Dit brugernavn og/eller koderord er forkert"}

    };
    public static readonly Dictionary<string, string> SuccesMsg = new Dictionary<string, string>
    {
        {"DK_LoginSuccess", "Du er blevet logget ind"},
        {"DK_ProfileUpdate", "Din profil er blevet opdateret"}
    };




    //public Vali()
    //{
    //    //
    //    // TODO: Add constructor logic here
    //    //
    //}


    public static bool IsNotEmpty(string text)
    {
        if (text == null || text == "") return false;
        else return true;
    }
    //public static string Needed(string objname = "this textbox", string language = "en")
    //{
    //    switch (language)
    //    {
    //        case "en":
    //            return "You need to fill out " + objname;
    //            break;

    //    }
    //}
}