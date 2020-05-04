using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModelLibrary;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.Model;

namespace SikonConferenceSystem.ViewModel
{
    public class SetupEventsPageVM
    {
        //Mockup values. You choose dates by going through a collection of valid dates based on the BaseDateTime and eventDuration.
        public static readonly DateTime BaseDateTime = DateTime.Today;
        public const int EventDays = 2;
        //Cannot decide on the best practice for loading a pre-existing Event, therefore putting it into this static variable just before it is accessed is the simplest way.
        public static Event EventToLoad;

        private Event _newEvent;

        public CatalogSingleton<Speaker> SpeakersCatalog { get; set; }

        private SetupEventsHandler _handler;
        public SetupEventsHandler Handler
        {
            get { return _handler; }
        }

        public string Abstract { get; set; }
        public string AbstractHeader { get; set; }

        public TimeSpan EventDuration { get; set; }

        public string ImagePath { get; set; }

        public TimeSpan EventDateHours { get; set; }
        public DateTime EventDate { get; set; }

        public ObservableCollection<Speaker> SpeakersInEvent { get; set; }

        public Event.EventType Type { get; set; }

        public List<Event.EventType> AllEventTypes
        {
            get { return Enum.GetValues(typeof(Event.EventType)).Cast<Event.EventType>().ToList(); }
        }

        private List<string> _allEventDays;
        public List<string> AllEventDays
        {
            get { return _allEventDays; }
        }

        private int _selectedDay;
        public int SelectedDay
        {
            get { return _selectedDay;}
            set
            {
                EventDate = BaseDateTime.AddDays(value);
                _selectedDay = value;
            }
        }

        private int _eventDurationHours;
        public int EventDurationHours
        {
            get { return _eventDurationHours; }
            set
            {
                EventDuration = EventDuration.Subtract(TimeSpan.FromHours(EventDuration.Hours));
                EventDuration = EventDuration.Add(TimeSpan.FromHours(value));
                _eventDurationHours = value;
            }
        }

        private int _eventDurationMinutes;
        public int EventDurationMinutes
        {
            get { return _eventDurationMinutes; }
            set
            {
                EventDuration = EventDuration.Subtract(TimeSpan.FromMinutes(EventDuration.Minutes));
                EventDuration = EventDuration.Add(TimeSpan.FromMinutes(value));
                _eventDurationMinutes = value;
            }
        }


        public SetupEventsPageVM()
        {
            _handler = new SetupEventsHandler(this);
            if (EventToLoad != null)
            {
                _newEvent = EventToLoad;
                EventToLoad = null;
            }
            else
            {
                _newEvent = new Event();
            }

            SpeakersCatalog = CatalogSingleton<Speaker>.Instance;

            SpeakersInEvent = new ObservableCollection<Speaker>();
            _allEventDays = GetDateStrings();

            _pressSpeakersInEventDeleteCommand = new RelayCommand(Handler.RemoveFromSpeakersView);
        }

        private List<string> GetDateStrings()
        {
            List<string> dateStrings = new List<string>();
            for (int i = 0; i < EventDays; i++)
            {
                dateStrings.Add(BaseDateTime.AddDays(i).ToString("dd/MM dddd"));
            }

            return dateStrings;
        }

        private RelayCommand _pressSpeakersInEventDeleteCommand;

        public ICommand PressSpeakersInEventDeleteCommand
        {
            get { return _pressSpeakersInEventDeleteCommand; }
            
        }

    }
}
