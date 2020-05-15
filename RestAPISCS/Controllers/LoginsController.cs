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
    public class LoginsController : ApiController
    {
        private ManageGenericWithLambda<User>
            userManager = DataBases.Access<User>(BaseNames.SikonDatabase, "UserSikon");

        private ManageGenericWithLambda<Speaker> speakerManager =
            DataBases.Access<Speaker>(BaseNames.SikonDatabase, "Speaker");

        private ManageGenericWithLambda<Admin>
            adminManager = DataBases.Access<Admin>(BaseNames.SikonDatabase, "Admins");
        // GET: api/Logins
        public IEnumerable<string> Get()
        {
            return null;
        }

        // GET: api/Logins/5
        [Route ("api/Logins/{loginId}")]
        public Tuple<User, Speaker, Admin> Get(string loginId)
        {
            if (loginId.Contains('@'))
            {
                return ManageLogin(UsersController.EmailKey(loginId), true);
            }

            return ManageLogin(UsersController.PhoneNumberKey(loginId), false);
        }

        private Tuple<User, Speaker, Admin> ManageLogin(Dictionary<string, object> lookupDictionary, bool isEmailKey)
        {
            User user = userManager.GetOne(Fillables.FillUser, lookupDictionary);

            if (user != null)
            {

                Speaker speaker = speakerManager.GetOne(Fillables.FillSpeaker, SpeakersController.PrimaryKeys(user.Id));

                if (speaker != null)
                {
                    return new Tuple<User, Speaker, Admin>(null, speaker, null);
                }

                Admin admin = adminManager.GetOne(Fillables.FillAdmin, AdminsController.PrimaryKeys(user.Id));

                if (admin != null)
                {
                    return new Tuple<User, Speaker, Admin>(null, null, admin);
                }

                return new Tuple<User, Speaker, Admin>(user, null, null);
            }
            
            return null;
            
        }

        // POST: api/Logins
        public bool Post([FromBody]string value)
        {
            return false;
        }

        // PUT: api/Logins/5
        public bool Put(int id, [FromBody]string value)
        {
            return false;
        }

        // DELETE: api/Logins/5
        public bool Delete(int id)
        {
            return false;
        }
    }
}
