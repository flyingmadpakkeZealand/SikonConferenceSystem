using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ModelLibrary;

namespace RestAPISCS.DatabaseUtility
{
    public static class Fillables
    {
        public static void FillUser(User user, SqlDataReader reader)
        {
            user.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
            user.Name = reader.GetString(reader.GetOrdinal("Name"));
            user.Email = reader.GetString(reader.GetOrdinal("Email"));
            user.Password = reader.GetString(reader.GetOrdinal("Password"));
        }
    }
}