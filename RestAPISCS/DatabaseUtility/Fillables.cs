using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using DBUtility;
using ModelLibrary;
using RestAPISCS.App_Start;
using RestAPISCS.Controllers;

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

        public static void FillAdmin(Admin admin, SqlDataReader reader)
        {
            string PhoneNumber =  reader.GetString(reader.GetOrdinal("PhoneNumber"));

            admin.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
            admin.AccessLevel = reader.GetInt32(reader.GetOrdinal("AccessLevel"));
            User placeholder = DataBases.Access<User>(BaseNames.SikonDatabase, "UserSikon")
                .GetOne(FillUser, UsersController.PrimaryKeys(PhoneNumber));
            admin.Name = placeholder.Name;
            admin.Email = placeholder.Email;
            admin.Password = placeholder.Password;
        }
    }
}