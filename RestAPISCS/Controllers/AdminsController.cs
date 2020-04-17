using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DBUtility;
using ModelLibrary;
using RestAPISCS.App_Start;
using RestAPISCS.DatabaseUtility;

namespace RestAPISCS.Controllers
{
    public class AdminsController : ApiController
    {
        private ManageGenericWithLambda<Admin> adminManager = DataBases.Access<Admin>(BaseNames.SikonDatabase, "Admins");
        private ManageGenericWithLambda<User> userManager = DataBases.Access<User>(BaseNames.SikonDatabase, "UserSikon");

        //The string inside is the name of the primary key for UserSikon table. It is used to associate an actual phone number value you give it ex. "12345678" with the that primary key via its column name, such that it can find a single entry with that phone number.
        //If you had more than one primary key, you add all of them one by one to the dictionary in the method.
        public static Dictionary<string, object> PrimaryKeys(string phoneNumber)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("PhoneNumber", phoneNumber);
            return lookupDictionary;
        }

        // GET: api/Admins
        public IEnumerable<Admin> Get()
        {
            return adminManager.Get(Fillables.FillAdmin);

        }

        // GET: api/Admins/5
        public Admin Get(int id)
        {
            string phoneNumber = Convert.ToString(id);
            return adminManager.GetOne(Fillables.FillAdmin, PrimaryKeys(phoneNumber));
        }

        // POST: api/Admins
        public bool Post([FromBody]Admin admin)
        {
            bool userOk =
                userManager.Post(Extractables.ExtractUser(new User(admin.Name, admin.PhoneNumber, admin.Email,
                    admin.Password)));

            bool adminOk = false;
            if (userOk)
            {
                adminOk = adminManager.Post(Extractables.ExtractAdmin(admin)); 
            }

            
            return userOk && adminOk;
        }

        // PUT: api/Admins/5

        public bool Put(int id, [FromBody]Admin admin)

        {
            string phoneNumber = Convert.ToString(id);
            bool adminOk = adminManager.Put(Extractables.ExtractAdmin(admin), PrimaryKeys(phoneNumber));

            bool userOk =
                userManager.Put(Extractables.ExtractUser(new User(admin.Name, admin.PhoneNumber, admin.Email,
                    admin.Password)), PrimaryKeys(phoneNumber));
            return userOk && adminOk;
        }

        // DELETE: api/Admins/5
        public bool Delete(int id)
        {
            string phoneNumber = Convert.ToString(id);
            bool adminDelete = adminManager.Delete(PrimaryKeys(phoneNumber));
            bool userDelete = userManager.Delete(PrimaryKeys(phoneNumber));
            return adminDelete && userDelete;
        }
    }
}
