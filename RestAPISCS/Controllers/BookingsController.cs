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
        public Booking Get(int bookingId)
        {
            return bookingManager.GetOne(Fillables.FillBooking, PrimaryKeys(bookingId));
        }

        // POST: api/Bookings
        public bool Post([FromBody]Booking booking)
        {
            return bookingManager.Post(Extractables.ExtractBooking(booking));
        }

        // PUT: api/Bookings/5
        public bool Put(int bookingId, [FromBody]Booking booking)
        {
            return bookingManager.Put(Extractables.ExtractBooking(booking), PrimaryKeys(bookingId));
        }

        // DELETE: api/Bookings/5
        public bool Delete(int bookingId)
        {
            return bookingManager.Delete(PrimaryKeys(bookingId));
        }

        //Der mangler CheckNoDuplicate og RetrieveId

    }
}
