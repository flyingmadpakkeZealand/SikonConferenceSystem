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


            sikonEvent.EventID = reader.GetInt32(reader.GetOrdinal("EventId"));
            sikonEvent.Date = reader.GetDateTime(reader.GetOrdinal("Date"));
            sikonEvent.Duration = reader.GetTimeSpan(reader.GetOrdinal("Time"));
            sikonEvent.Rating = reader.GetFloat(reader.GetOrdinal("Rating"));
            sikonEvent.Abstract = reader.GetString(reader.GetOrdinal("Abstact"));
            sikonEvent.RoomNr = roomNr;
        }


        public static void FillBooking(Booking booking, SqlDataReader reader)
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));

            booking.Id = id;
            booking.BookingID = reader.GetInt32(reader.GetOrdinal("BookingId"));
            booking.BookingDate = reader.GetDateTime(reader.GetOrdinal("BookingDate"));
        }
    }
}