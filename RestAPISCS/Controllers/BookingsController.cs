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
        private ManageGenericWithLambda<Booking> bookingSettingsManager = DataBases.Access<Booking>(BaseNames.SikonDatabase, "BookingSettings");

        private ManageGenericWithLambda<SimpleType<int>> bookedEventsManager =
            DataBases.Access<SimpleType<int>>(BaseNames.SikonDatabase, "BookedEvents");


        public static Dictionary<string, object> PrimaryKeys(int userId)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("UserId", userId);
            return lookupDictionary;
        }

        // GET: api/Bookings
        public IEnumerable<Booking> Get()
        {
            return bookingSettingsManager.Get(Fillables.FillBooking); //Fills a booking object by itself.
        }

        // GET: api/Bookings/5
        public Booking Get(int id)
        {
            return bookingSettingsManager.GetOne(Fillables.FillBooking, PrimaryKeys(id));
        }

        // POST: api/Bookings
        public bool Post([FromBody]Booking booking)
        {
            bool settingsOk = bookingSettingsManager.Post(Extractables.ExtractBookingSettings(booking));

            bool bookedOk = false;
            if (settingsOk)
            {
                bookedOk = true;
                foreach (int eventId in booking.BookedEventsId)
                {
                    bookedOk = bookedEventsManager.Post(Extractables.ExtractBookedEvents(booking.UserId, eventId));
                }
            }

            return settingsOk && bookedOk;
        }

        // PUT: api/Bookings/5
        public bool Put(int id, [FromBody]Booking booking)
        {
            bool settingsOk = bookingSettingsManager.Put(Extractables.ExtractBookingSettings(booking), PrimaryKeys(id));

            bool bookedOk = false;
            if (settingsOk)
            {
                bookedEventsManager.Delete(PrimaryKeys(id));

                bookedOk = true;
                foreach (int eventId in booking.BookedEventsId)
                {
                    bookedOk = bookedEventsManager.Post(Extractables.ExtractBookedEvents(booking.UserId, eventId));
                }
            }

            return settingsOk && bookedOk;
        }

        // DELETE: api/Bookings/5
        public bool Delete(int id)
        {
            bool settingsOk = bookingSettingsManager.Delete(PrimaryKeys(id));

            bool bookedOk = false;
            if (settingsOk)
            {
                bookedOk = bookedEventsManager.Delete(PrimaryKeys(id));
            }

            return settingsOk && bookedOk;
        }

    }
}
