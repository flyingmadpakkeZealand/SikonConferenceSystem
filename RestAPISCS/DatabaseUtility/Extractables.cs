using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBUtility;
using ModelLibrary;
using RestAPISCS.App_Start;
using RestAPISCS.Controllers;

namespace RestAPISCS.DatabaseUtility
{
    public static class Extractables
    {
        public static Dictionary<string, object> ExtractUser(User user)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("PhoneNumber", user.PhoneNumber);
            lookupDictionary.Add("Name", user.Name);
            lookupDictionary.Add("Email", user.Email);
            lookupDictionary.Add("Password", user.Password);
            return lookupDictionary;
        }

        public static Dictionary<string, object> ExtractAdmin(Admin admin)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Id", admin.Id);
            lookupDictionary.Add("AccessLevel", admin.AccessLevel);
            
            return lookupDictionary;
        }

        public static Dictionary<string, object> ExtractSpeaker(Speaker speaker)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Id", speaker.Id);
            lookupDictionary.Add("Bio", speaker.Bio);

            return lookupDictionary;
        }
        public static Dictionary<string, object> ExtractEvent(Event sikonEvent)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            
            lookupDictionary.Add("Date", sikonEvent.Date);
            lookupDictionary.Add("Duration", sikonEvent.Duration);
            lookupDictionary.Add("Rating", sikonEvent.Rating);
            lookupDictionary.Add("Abstract", sikonEvent.Abstract);
            lookupDictionary.Add("EventID", sikonEvent.EventID);
            lookupDictionary.Add("RoomNr", sikonEvent.RoomNr);

            return lookupDictionary;
        }

        public static Dictionary<string, object> ExtractRoom(Room room)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("RoomNr", room.RoomNr);
            lookupDictionary.Add("RoomMaxPersons", room.RoomMaxPersons);
            lookupDictionary.Add("AutistSeats", room.AutistSeats);

            return lookupDictionary;
        }

        public static Dictionary<string, object> ExtractBooking(Booking booking)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("BookingId", booking.BookingID);
            lookupDictionary.Add("BookingDate", booking.BookingDate);
            lookupDictionary.Add("Id", booking.Id);

            return lookupDictionary;
        }
    }
}