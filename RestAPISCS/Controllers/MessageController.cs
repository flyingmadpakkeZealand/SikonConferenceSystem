using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using DBUtility;
using ModelLibrary;
using RestAPISCS.App_Start;

namespace RestAPISCS.Controllers
{
    public class MessageController : ApiController
    {
        private ManageGenericWithLambda<Booking> MessageManager =
            DataBases.Access<Booking>(BaseNames.SikonDatabase, "Booking");
        public void MessageEventCanceled(int EventID)
        {
            
        }

        // GET: api/Message
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Message/5
        public bool Get(int id)
        {
            return false;
        }

        // POST: api/Message
        public bool Post([FromBody]string value)
        {
            return false;
        }

        // PUT: api/Message/5
        public bool Put(int id, [FromBody]string value)
        {
            return false;
        }

        // DELETE: api/Message/5
        public bool Delete(int id)
        {
            return false;
        }
    }
}
