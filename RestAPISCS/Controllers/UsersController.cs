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
        public static Dictionary<string, object> PrimaryKeys(string phoneNumber)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("PhoneNumber", phoneNumber);
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
            string phoneNumber = Convert.ToString(id);
            return manager.GetOne(Fillables.FillUser, PrimaryKeys(phoneNumber));
        }

        // POST: api/Users
        public bool Post([FromBody]User user)
        {
            return manager.Post(Extractables.ExtractUser(user));
        }

        // PUT: api/Users/5
        public bool Put(int id, [FromBody]User user)
        {
            string phoneNumber = Convert.ToString(id);
            return manager.Put(Extractables.ExtractUser(user), PrimaryKeys(phoneNumber));
        }

        // DELETE: api/Users/5
        public bool Delete(int id)
        {
            string phoneNumber = Convert.ToString(id);
            return manager.Delete(PrimaryKeys(phoneNumber));
        }
    }
}
