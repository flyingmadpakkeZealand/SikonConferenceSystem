using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace SikonConferenceSystem.View
{
    public class GroupingEventVM
    {
        public ObservableCollection<HourGroup> HourGroups { get; set; }

        private int _hourGroupIterations;
        public GroupingEventVM()
        {
            _hourGroupIterations = 4;
            HourGroups = new ObservableCollection<HourGroup>();

            List<EventAdapter> allEvents = new List<EventAdapter>();
            
            allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(11), TimeSpan.FromHours(1), 5, "The first event", 0, 0),
                "#33DEF010", ""));
            allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(11), TimeSpan.FromHours(2), 5, "The second event", 0, 0),
                "#33EA1616", ""));
            allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(12), TimeSpan.FromHours(1), 5, "The Third event", 0, 0),
                "#33DEF010", ""));
            allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(12), TimeSpan.FromHours(1), 5, "The Fourth event", 0, 0),
                "#33DEF010", ""));
            allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(13), TimeSpan.FromHours(2), 5, "The Fifth event", 0, 0),
                "#33EA1616", ""));
            allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(13), TimeSpan.FromHours(1), 5, "The Sixth event", 0, 0),
                "#33DEF010", ""));
            allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(14), TimeSpan.FromHours(1), 5, "The Seventh event", 0, 0),
                "#33DEF010", ""));
            allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(14), TimeSpan.FromHours(1), 5, "The Eight event", 0, 0),
                "#33DEF010", ""));
            allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(13.5), TimeSpan.FromHours(1), 5, "The Test event", 0, 0),
                "#33DEF010", ""));

            SetupHourGroups();

            AddEvents(allEvents);
        }

        private void SetupHourGroups()
        {
            for (int i = 0; i < _hourGroupIterations; i++)
            {
                HourGroups.Add(new HourGroup(DateTime.Today.AddHours(11+i)));
            }
        }

        private void AddEvents(List<EventAdapter> allEvents)
        {
            foreach (HourGroup hourGroup in HourGroups)
            {
                foreach (EventAdapter @event in allEvents)
                {
                    if (AddCondition(@event, hourGroup))
                    {
                        hourGroup.EventGroup.Add(@event);
                    }
                }
            }
        }

        private bool AddCondition(EventAdapter @event, HourGroup hourGroup)
        {
            bool startingTime = @event.Event.Date >= hourGroup.TimeAsDateTime &&
                                @event.Event.Date < hourGroup.TimeAsDateTime.AddHours(1);
            bool overlapTime = @event.Event.Date.Add(@event.Event.Duration) > hourGroup.TimeAsDateTime && @event.Event.Date < hourGroup.TimeAsDateTime;

            return startingTime || overlapTime;
        }
    }

    public class HourGroup : ObservableCollection<EventAdapter>
    {
        public ObservableCollection<EventAdapter> EventGroup { get; set; }

        public HourGroup(DateTime time)
        {
            EventGroup = new ObservableCollection<EventAdapter>();
            TimeAsDateTime = time;
            Time = $"{time.ToShortTimeString()} : {time.AddHours(1).ToShortTimeString()}";
        }

        public string Time { get; set; }

        public DateTime TimeAsDateTime { get; set; }
    }

    public class EventAdapter
    {
        public Event Event { get; set; }
        public string Color { get; set; }
        public string ImagePath { get; set; }

        public EventAdapter(Event @event, string color, string imagePath)
        {
            Event = @event;
            Color = color;
            ImagePath = imagePath;
        }
    }
}
