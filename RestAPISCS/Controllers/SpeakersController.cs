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
    public class SpeakersController : ApiController
    {
        private ManageGenericWithLambda<Speaker> speakerManager =
            DataBases.Access<Speaker>(BaseNames.SikonDatabase, "Speaker");

        private ManageGenericWithLambda<User>
            userManager = DataBases.Access<User>(BaseNames.SikonDatabase, "UserSikon");

        public static Dictionary<string, object> PrimaryKeys(int id)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Id", id);
            return lookupDictionary;
        }

        // GET: api/Speakers
        public IEnumerable<Speaker> Get()
        {
            return speakerManager.Get(Fillables.FillSpeaker);
        }

        // GET: api/Speakers/5
        public Speaker Get(int id)
        {
            return speakerManager.GetOne(Fillables.FillSpeaker, PrimaryKeys(id));
        }

        // POST: api/Speakers
        public bool Post([FromBody]Speaker speaker)
        {
            speaker.Id = -1;
            bool userOk = false;

            if (CheckNoDuplicate(speaker))
            {
                userOk = userManager.Post(Extractables.ExtractUser(speaker)); 
            }

            bool speakerOk = false;
            if (userOk)
            {
                speaker.Id = RetrieveId(speaker);
                speakerOk = speakerManager.Post(Extractables.ExtractSpeaker(speaker));
            }

            return userOk && speakerOk;
        }

        // PUT: api/Speakers/5
        public bool Put(int id, [FromBody]Speaker speaker)
        {
            speaker.Id = id;
            if (!CheckNoDuplicate(speaker))
            {
                return false;
            }

            bool speakerOk = speakerManager.Put(Extractables.ExtractSpeaker(speaker), PrimaryKeys(id));

            bool userOk = false;
            if (speakerOk)
            {
                userOk = userManager.Put(
                    Extractables.ExtractUser(speaker), PrimaryKeys(id));
            }

            return speakerOk && userOk;
        }

        // DELETE: api/Speakers/5
        public bool Delete(int id)
        {
            bool speakerOk = speakerManager.Delete(PrimaryKeys(id));

            bool userOk = false;
            if (speakerOk)
            {
                userOk = userManager.Delete(PrimaryKeys(id));
            }

            return speakerOk && userOk;
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
