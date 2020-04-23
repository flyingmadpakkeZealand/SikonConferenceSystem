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

        public static void FillSpeaker(Speaker speaker, SqlDataReader reader)
        {
            string phoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));

            speaker.PhoneNumber = phoneNumber;
            speaker.Bio = reader.GetString(reader.GetOrdinal("Bio"));
            User user = DataBases.Access<User>(BaseNames.SikonDatabase, "UserSikon")
                .GetOne(FillUser, UsersController.PrimaryKeys(phoneNumber));
            speaker.Name = user.Name;
            speaker.Email = user.Email;
            speaker.Password = user.Password;
        }
    }
}