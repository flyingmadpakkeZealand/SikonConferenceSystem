using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Event
    {
        public Room Room;

        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public float Rating { get; set; }
        public string Abstract { get; set; }
        public int EventID { get; set; }
        public int RoomNr { get; set; }

        public Event(DateTime date, TimeSpan duration, float rating, string @abstract, int eventId, int roomNr)
        {
            Date = date;
            Duration = duration;
            Rating = rating;
            Abstract = @abstract;
            EventID = eventId;
            RoomNr = roomNr;
        }

        public Event()
        {
            
        }
    }
}
