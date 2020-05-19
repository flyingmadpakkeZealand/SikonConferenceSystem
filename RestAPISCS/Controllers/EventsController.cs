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

        private List<SimpleType<int>> _initialIds;

        // POST: api/Events
        public bool Post([FromBody]Event sikonEvent)
        {
            _initialIds = GetAllIds();
            bool eventOk = eventManager.Post(Extractables.ExtractEvent(sikonEvent));

            bool speakersInEventOk = false;
            if (eventOk)
            {
                sikonEvent.EventID = RetrieveId();
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

        private int RetrieveId()
        {
            List<SimpleType<int>> allIds = GetAllIds();

            for (int i = allIds.Count-1; i > 0; i--)
            {
                if (allIds[i].Variable != _initialIds[i-1].Variable)
                {
                    return allIds[i].Variable;
                }
                
            }

            return allIds[0].Variable;
        }

        private List<SimpleType<int>> GetAllIds()
        {
            var fillIds = Fillables.CreateFillSimpleType<int>("EventId");

            IEnumerable<SimpleType<int>> allIds =
                DataBases.Access<SimpleType<int>>(BaseNames.SikonDatabase, "Event").Get(fillIds);

            return new List<SimpleType<int>>(allIds);
        }
    }
}
