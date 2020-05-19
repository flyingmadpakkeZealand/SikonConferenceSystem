using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary
{
    public class Event
    {
        public enum EventType
        {
            Workshop,
            MediumEvent,
            BigEvent
        }

        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public float Rating { get; set; }
        public string Abstract { get; set; }
        public int EventID { get; set; }
        public int RoomNr { get; set; }
        public string ImagePath { get; set; }
        public List<Speaker> SpeakersInEvent { get; set; }
        public EventType Type { get; set; }

        public Event(DateTime date, TimeSpan duration, float rating, string @abstract, int eventId, int roomNr,
            string imagePath, List<Speaker> speakersInEvent, EventType type)
        {
            Date = date;
            Duration = duration;
            Rating = rating;
            Abstract = @abstract;
            EventID = eventId;
            RoomNr = roomNr;
            ImagePath = imagePath;
            SpeakersInEvent = speakersInEvent;
            Type = type;
        }

        public Event()
        {
            
        }

        public Event(Event eventToCopy)
        {
            Date = eventToCopy.Date;
            Duration = eventToCopy.Duration;
            Rating = eventToCopy.Rating;
            Abstract = eventToCopy.Abstract;
            EventID = eventToCopy.EventID;
            RoomNr = eventToCopy.RoomNr;
            ImagePath = eventToCopy.ImagePath;
            SpeakersInEvent = new List<Speaker>(eventToCopy.SpeakersInEvent);
            Type = eventToCopy.Type;
        }

    }
}
