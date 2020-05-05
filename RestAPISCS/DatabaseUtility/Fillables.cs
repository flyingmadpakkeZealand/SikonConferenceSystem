using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using DBUtility;
using ModelLibrary;
using RestAPISCS.App_Start;
using RestAPISCS.Controllers;

namespace RestAPISCS.DatabaseUtility
{
    public static class Fillables
    {
        public static void FillUser(User user, SqlDataReader reader)
        {
            user.Id = reader.GetInt32(reader.GetOrdinal("Id"));
            user.PhoneNumber = reader.GetString(reader.GetOrdinal("PhoneNumber"));
            user.Name = reader.GetString(reader.GetOrdinal("Name"));
            user.Email = reader.GetString(reader.GetOrdinal("Email"));
            user.Password = reader.GetString(reader.GetOrdinal("Password"));
        }

        public static void FillAdmin(Admin admin, SqlDataReader reader)
        {
            int id =  reader.GetInt32(reader.GetOrdinal("Id"));

            admin.Id = id;
            admin.AccessLevel = reader.GetInt32(reader.GetOrdinal("AccessLevel"));
            User user = DataBases.Access<User>(BaseNames.SikonDatabase, "UserSikon")
                .GetOne(FillUser, UsersController.PrimaryKeys(id));
            admin.PhoneNumber = user.PhoneNumber;
            admin.Name = user.Name;
            admin.Email = user.Email;
            admin.Password = user.Password;
        }

        public static void FillSpeaker(Speaker speaker, SqlDataReader reader)
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));

            speaker.Id = id;
            speaker.Bio = reader.GetString(reader.GetOrdinal("Bio"));
            User user = DataBases.Access<User>(BaseNames.SikonDatabase, "UserSikon")
                .GetOne(FillUser, UsersController.PrimaryKeys(id));
            speaker.PhoneNumber = user.PhoneNumber;
            speaker.Name = user.Name;
            speaker.Email = user.Email;
            speaker.Password = user.Password;
        }
        public static void FillRoom(Room room, SqlDataReader reader)
        {
            room.RoomNr = reader.GetInt32(reader.GetOrdinal("RoomNr"));
            room.RoomMaxPersons = reader.GetInt32(reader.GetOrdinal("RoomMaxPersons"));
            room.AutistSeats = reader.GetInt32(reader.GetOrdinal("AutistSeats"));
        }
        public static void FillEvent(Event sikonEvent, SqlDataReader reader)
        {
            int roomNr = reader.GetInt32(reader.GetOrdinal("RoomNr"));
            int eventId = reader.GetInt32(reader.GetOrdinal("EventId"));

            sikonEvent.EventID = eventId;
            sikonEvent.Date = reader.GetDateTime(reader.GetOrdinal("Date"));
            sikonEvent.Duration = reader.GetTimeSpan(reader.GetOrdinal("Duration"));
            sikonEvent.Rating = reader.GetFloat(reader.GetOrdinal("Rating"));
            sikonEvent.Abstract = reader.GetString(reader.GetOrdinal("Abstract"));
            sikonEvent.RoomNr = roomNr;
            sikonEvent.ImagePath = reader.GetString(reader.GetOrdinal("ImagePath"));

            string typeString = reader.GetString(reader.GetOrdinal("Type"));
            sikonEvent.Type = (Event.EventType) Enum.Parse(typeof(Event.EventType), typeString);

            var fillInt = CreateFillSimpleType<int>("SpeakerId");
            IEnumerable<SimpleType<int>> speakerIds = DataBases.Access<SimpleType<int>>(BaseNames.SikonDatabase, "SpeakersInEvent")
                .GetSelection(fillInt, EventsController.PrimaryKeys(eventId));

            List<Speaker> speakersInEvent = new List<Speaker>();

            //Works but idk how efficient it is to get certain speakers, one at a time...
            foreach (SimpleType<int> simpleType in speakerIds)
            {
                Speaker speaker = DataBases.Access<Speaker>(BaseNames.SikonDatabase, "Speaker")
                    .GetOne(FillSpeaker, SpeakersController.PrimaryKeys(simpleType.Variable));
                speakersInEvent.Add(speaker);
            }

            sikonEvent.SpeakersInEvent = speakersInEvent;
        }


        public static void FillBooking(Booking booking, SqlDataReader reader)
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));

            booking.Id = id;
            booking.BookingID = reader.GetInt32(reader.GetOrdinal("BookingId"));
            booking.BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate"));
        }

        public static Action<SimpleType<T>, SqlDataReader> CreateFillSimpleType<T>(string columnName)
        {
            return (simpleType, reader) =>
            {
                simpleType.Variable = reader.GetFieldValue<T>(reader.GetOrdinal(columnName));
            };
        }
    }

    public class SimpleType<T>
    {
        public T Variable { get; set; }
    }
}