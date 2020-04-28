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
    public class BookingsController : ApiController
    {
        private ManageGenericWithLambda<Booking> bookingManager = DataBases.Access<Booking>(BaseNames.SikonDatabase, "Booking");

        private static Dictionary<string, object> PrimaryKeys(int bookingId)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("BookingId", bookingId);
            return lookupDictionary;
        }

        // GET: api/Bookings
        public IEnumerable<Booking> Get()
        {
            return bookingManager.Get(Fillables.FillBooking);
        }

        // GET: api/Bookings/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Bookings
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Bookings/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Bookings/5
        public void Delete(int id)
        {
        }
    }
}
