using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using DBUtility;
using ModelLibrary;
using RestAPISCS.App_Start;
using RestAPISCS.Controllers;
using Message = ModelLibrary.Message;

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
            //lookupDictionary.Add("EventId", sikonEvent.EventID); - Auto generated.
            lookupDictionary.Add("RoomNr", sikonEvent.RoomNr);
            lookupDictionary.Add("ImagePath", sikonEvent.ImagePath);
            lookupDictionary.Add("Type", sikonEvent.Type.ToString());

            return lookupDictionary;
        }

        public static Dictionary<string, object> ExtractSpeakersInEvent(int eventId, int speakerId)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();

            lookupDictionary.Add("EventId", eventId);
            lookupDictionary.Add("SpeakerId", speakerId);

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

        public static Dictionary<string, object> ExtractBookingSettings(Booking booking)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("UserId", booking.UserId);
            lookupDictionary.Add("ReceiveMessages", booking.ReceiveMessages);

            return lookupDictionary;
        }

        public static Dictionary<string, object> ExtractBookedEvents(int userId, int eventId)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();

            lookupDictionary.Add("UserId", userId);
            lookupDictionary.Add("EventId", eventId);

            return lookupDictionary;
        }


        public static Dictionary<string, object> ExtractMessage(Message message)
        {
            Dictionary<string, object> lookupDictionary = new Dictionary<string, object>();
            lookupDictionary.Add("Id", message.Id);
            lookupDictionary.Add("Message", message.textMessage);

            return lookupDictionary;
        }
    }
}