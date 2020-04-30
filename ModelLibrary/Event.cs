using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Event
    {
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public float Rating { get; set; }
        public string Abstract { get; set; }
        public int EventID { get; set; }
        public int RoomNr { get; set; }
        public string ImagePath { get; set; }

        public Event(DateTime date, TimeSpan duration, float rating, string @abstract, int eventId, int roomNr, string imagePath)
        {
            Date = date;
            Duration = duration;
            Rating = rating;
            Abstract = @abstract;
            EventID = eventId;
            RoomNr = roomNr;
            ImagePath = imagePath;
        }

        public Event()
        {
            
        }

        public override string ToString()
        {
            return $"EventId: {EventID}";
        }
    }
}
