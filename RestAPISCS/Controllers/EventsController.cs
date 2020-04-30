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

        private static Dictionary<string, object> PrimaryKeys(int eventId)
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
        public Event Get(int eventId)
        {
            return eventManager.GetOne(Fillables.FillEvent, PrimaryKeys(eventId));
        }

        // POST: api/Events
        public bool Post([FromBody]Event sikonEvent)
        {
            return eventManager.Post(Extractables.ExtractEvent(sikonEvent));
            
        }

        // PUT: api/Events/5
        public bool Put(int eventId, [FromBody]Event sikonEvent)
        {
            return eventManager.Put(Extractables.ExtractEvent(sikonEvent), PrimaryKeys(eventId));
        }

        // DELETE: api/Events/5
        public bool Delete(int eventId)
        {
            return eventManager.Delete(PrimaryKeys(eventId));
        }

        //Der mangler CheckNoDuplicate og RetrieveId

    }
}
