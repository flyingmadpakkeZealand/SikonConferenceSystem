using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace RestAPISCS.DatabaseUtility
{
    public static class Extractables
    {
        public static Dictionary<string, object> ExtractUser(User user)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("PhoneNumber", user.PhoneNumber);
            lookupDictionary.Add("Name", user.Name);
            lookupDictionary.Add("Email", user.Email);
            lookupDictionary.Add("Password", user.Password);
            return lookupDictionary;
        }
    }
}