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
            sikonEvent.EventID = -1;
            
        }

        // PUT: api/Events/5
        public void Put(int eventId, [FromBody]string value)
        {
        }

        // DELETE: api/Events/5
        public void Delete(int eventId)
        {
        }
    }
}
