using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RestAPISCS.Controllers
{
    public class ShowMessagesController : ApiController
    {
        // GET: api/ShowMessages
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ShowMessages/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ShowMessages
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ShowMessages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ShowMessages/5
        public void Delete(int id)
        {
        }
    }
}
