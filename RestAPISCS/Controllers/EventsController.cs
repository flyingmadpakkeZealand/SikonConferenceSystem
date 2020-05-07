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
    public class EventsController : ApiController
    {
        private ManageGenericWithLambda<Event> eventManager = DataBases.Access<Event>(BaseNames.SikonDatabase, "Event");

        private ManageGenericWithLambda<SimpleType<int>> speakersInEventManager =
            DataBases.Access<SimpleType<int>>(BaseNames.SikonDatabase, "SpeakersInEvent");

        public static Dictionary<string, object> PrimaryKeys(int eventId)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("EventId", eventId);
            return lookupDictionary;
        }

        // GET: api/Events
        public IEnumerable<Event> Get()
        {
            return eventManager.Get(Fillables.FillEvent);
        }

        // GET: api/Events/5
        public Event Get(int id)
        {
            return eventManager.GetOne(Fillables.FillEvent, PrimaryKeys(id));
        }

        // POST: api/Events
        public bool Post([FromBody]Event sikonEvent)
        {
            bool eventOk = eventManager.Post(Extractables.ExtractEvent(sikonEvent));

            bool speakersInEventOk = false;
            if (eventOk)
            {
                speakersInEventOk = true;
                foreach (Speaker speaker in sikonEvent.SpeakersInEvent)
                {
                    speakersInEventOk = speakersInEventManager.Post(Extractables.ExtractSpeakersInEvent(sikonEvent.EventID, speaker.Id));
                }
            }

            return eventOk && speakersInEventOk;
        }

        // PUT: api/Events/5
        public bool Put(int id, [FromBody]Event sikonEvent)
        {
            bool eventOk = eventManager.Put(Extractables.ExtractEvent(sikonEvent), PrimaryKeys(id));

            bool speakersInEventOk = false;
            if (eventOk)
            {
                speakersInEventManager.Delete(PrimaryKeys(id));

                speakersInEventOk = true;
                foreach (Speaker speaker in sikonEvent.SpeakersInEvent)
                {
                    speakersInEventOk = speakersInEventManager.Post(Extractables.ExtractSpeakersInEvent(sikonEvent.EventID, speaker.Id));
                }
            }

            return eventOk && speakersInEventOk;
        }

        // DELETE: api/Events/5
        public bool Delete(int id)
        {
            bool eventOk = eventManager.Delete(PrimaryKeys(id));

            return eventOk;
        }

        //Der mangler CheckNoDuplicate og RetrieveId

    }
}
