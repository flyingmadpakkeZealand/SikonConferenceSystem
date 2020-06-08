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
    public class BookingSettingsController : ApiController
    {
        private ManageGenericWithLambda<Booking> bookingSettingsManager = DataBases.Access<Booking>(BaseNames.SikonDatabase, "BookingSettings");

        // GET: api/BookingSettings
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/BookingSettings/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/BookingSettings
        public bool Post([FromBody]Booking booking)
        {
            return bookingSettingsManager.Post(Extractables.ExtractBookingSettings(booking));
        }

        // PUT: api/BookingSettings/5
        public bool Put(int id, [FromBody]Booking booking)
        {
            return bookingSettingsManager.Put(Extractables.ExtractBookingSettings(booking),
                BookingsController.PrimaryKeys(id));
        }

        // DELETE: api/BookingSettings/5
        public void Delete(int id)
        {
        }
    }
}
