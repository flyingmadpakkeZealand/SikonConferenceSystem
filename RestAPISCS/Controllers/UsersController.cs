using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DBUtility;
using ModelLibrary;
using RestAPISCS.App_Start;
using RestAPISCS.DatabaseUtility;

namespace RestAPISCS.Controllers
{
    public class UsersController : ApiController
    {
        private ManageGenericWithLambda<User> manager = DataBases.Access<User>(BaseNames.SikonDatabase, "UserSikon");

        //The string inside is the name of the primary key for UserSikon table. It is used to associate an actual phone number value you give it ex. "12345678" with the that primary key via its column name, such that it can find a single entry with that phone number.
        //If you had more than one primary key, you add all of them one by one to the dictionary in the method.
        public static Dictionary<string, object> PrimaryKeys(int id)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Id", id);
            return lookupDictionary;
        }

        public static Dictionary<string, object> PhoneNumberKey(string phoneNumber)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("PhoneNumber", phoneNumber);
            return lookupDictionary;
        }

        public static Dictionary<string, object> EmailKey(string email)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Email", email);
            return lookupDictionary;
        }

        // GET: api/Users
        public IEnumerable<User> Get()
        {
            return manager.Get(Fillables.FillUser);
            
        }

        // GET: api/Users/5
        public User Get(int id)
        {
            return manager.GetOne(Fillables.FillUser, PrimaryKeys(id));
        }

        // POST: api/Users
        public bool Post([FromBody]User user)
        {
            user.Id = -1;
            if (CheckNoDuplicate(user))
            {
                //CleanUserStrings(user);
                return manager.Post(Extractables.ExtractUser(user));
            }

            return false;
        }

        // PUT: api/Users/5
        public bool Put(int id, [FromBody]User user)
        {
            user.Id = id;
            if (!CheckNoDuplicate(user))
            {
                return false;
            }
            //CleanUserStrings(user);
            return manager.Put(Extractables.ExtractUser(user), PrimaryKeys(id));
        }

        // DELETE: api/Users/5
        public bool Delete(int id)
        {
            return manager.Delete(PrimaryKeys(id));
        }

        private bool CheckNoDuplicate(User user) //This should probably be replaced by some other code in the UWP app part.
        {
            User userEmailTemp = manager.GetOne(Fillables.FillUser, EmailKey(user.Email));
            User userPhoneNumberTemp = manager.GetOne(Fillables.FillUser, PhoneNumberKey(user.PhoneNumber));

            string email = userEmailTemp != null ? userEmailTemp.Email : string.Empty;
            string phoneNumber = userPhoneNumberTemp != null ? userPhoneNumberTemp.PhoneNumber : string.Empty;

            bool emailOk = email == string.Empty || userEmailTemp.Id == user.Id;
            bool phoneNumberOk = phoneNumber == string.Empty || userPhoneNumberTemp.Id == user.Id;

            return emailOk && phoneNumberOk;
        }

        public static void CleanUserStrings(User user)
        {
            user.Email = user.Email == string.Empty ? null : user.Email;
            user.PhoneNumber = user.PhoneNumber == string.Empty ? null : user.PhoneNumber;
        }
    }
}
