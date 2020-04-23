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

        public static Dictionary<string, object> PrimaryKeys(string PhoneNumber)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("PhoneNumber", PhoneNumber);
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
            string phoneNumber = Convert.ToString(id);
            return speakerManager.GetOne(Fillables.FillSpeaker, PrimaryKeys(phoneNumber));
        }

        // POST: api/Speakers
        public bool Post([FromBody]Speaker speaker)
        {
            bool userOk = userManager.Post(Extractables.ExtractUser(new User(speaker.Name, speaker.PhoneNumber,
                speaker.Email, speaker.Password)));

            bool speakerOk = false;
            if (userOk)
            {
                speakerOk = speakerManager.Post(Extractables.ExtractSpeaker(speaker));
            }

            return userOk && speakerOk;
        }

        // PUT: api/Speakers/5
        public bool Put(int id, [FromBody]Speaker speaker)
        {
            string phoneNumber = Convert.ToString(id);

            bool speakerOk = speakerManager.Put(Extractables.ExtractSpeaker(speaker), PrimaryKeys(phoneNumber));

            bool userOk = false;
            if (speakerOk)
            {
                userOk = userManager.Put(
                    Extractables.ExtractUser(new User(speaker.Name, speaker.PhoneNumber, speaker.Email,
                        speaker.Password)), PrimaryKeys(phoneNumber));
            }

            return speakerOk && userOk;
        }

        // DELETE: api/Speakers/5
        public bool Delete(int id)
        {
            string phoneNumber = Convert.ToString(id);

            bool speakerOk = speakerManager.Delete(PrimaryKeys(phoneNumber));

            bool userOk = false;
            if (speakerOk)
            {
                userOk = userManager.Delete(PrimaryKeys(phoneNumber));
            }

            return speakerOk && userOk;
        }
    }
}
