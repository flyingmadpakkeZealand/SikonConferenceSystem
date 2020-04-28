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
        public static Dictionary<string, object> PrimaryKeys(int id)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Id", id);
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
            return adminManager.GetOne(Fillables.FillAdmin, PrimaryKeys(id));
        }

        // POST: api/Admins
        public bool Post([FromBody]Admin admin)
        {
            admin.Id = -1;
            bool userOk = false;

            if (CheckNoDuplicate(admin))
            {
                userOk = userManager.Post(Extractables.ExtractUser(admin)); 
            }

            bool adminOk = false;
            if (userOk)
            {
                admin.Id = RetrieveId(admin);
                adminOk = adminManager.Post(Extractables.ExtractAdmin(admin)); 
            }

            return userOk && adminOk;
        }

        // PUT: api/Admins/5
        public bool Put(int id, [FromBody]Admin admin)
        {
            admin.Id = id;
            if (!CheckNoDuplicate(admin))
            {
                return false;
            }

            bool adminOk = adminManager.Put(Extractables.ExtractAdmin(admin), PrimaryKeys(id));

            bool userOk = false;
            if (adminOk)
            {
                //UsersController.CleanUserStrings(user);
                userOk = userManager.Put(Extractables.ExtractUser(admin), PrimaryKeys(id)); 
            }

            return adminOk && userOk;
        }

        // DELETE: api/Admins/5
        public bool Delete(int id)
        {
            bool adminDelete = adminManager.Delete(PrimaryKeys(id));

            bool userDelete = false;
            if (adminDelete)
            {
                userDelete = userManager.Delete(PrimaryKeys(id)); 
            }
            return adminDelete && userDelete;
        }

        private bool CheckNoDuplicate(User user) //This should probably be replaced by some other code in the UWP app part.
        {
            User userEmailTemp = userManager.GetOne(Fillables.FillUser, UsersController.EmailKey(user.Email));
            User userPhoneNumberTemp = userManager.GetOne(Fillables.FillUser, UsersController.PhoneNumberKey(user.PhoneNumber));

            string email = userEmailTemp != null ? userEmailTemp.Email : string.Empty;
            string phoneNumber = userPhoneNumberTemp != null ? userPhoneNumberTemp.PhoneNumber : string.Empty;

            bool emailOk = email == string.Empty || userEmailTemp.Id == user.Id;
            bool phoneNumberOk = phoneNumber == string.Empty || userPhoneNumberTemp.Id == user.Id;

            return emailOk && phoneNumberOk;
        }

        private int RetrieveId(User user)
        {
            if (user.Email != string.Empty)
            {
                return userManager.GetOne(Fillables.FillUser, UsersController.EmailKey(user.Email)).Id;
            }

            return userManager.GetOne(Fillables.FillUser, UsersController.PhoneNumberKey(user.PhoneNumber)).Id;
        }
    }
}
