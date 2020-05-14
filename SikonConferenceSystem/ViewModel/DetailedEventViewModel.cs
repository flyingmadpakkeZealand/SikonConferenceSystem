using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Media.Core;
using Windows.Media.SpeechRecognition;
using ModelLibrary;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.Model;
using SikonConferenceSystem.ViewModel.Interfaces;

namespace SikonConferenceSystem.ViewModel
{
    public class DetailedEventViewModel:IFormattedEventViewModel
    {
        private Event _newEvent;

        public Event NewEvent
        {
            get { return _newEvent; }
        }

        public DetailedEventHandler _handler { get; set; }

        public DetailedEventHandler Handler
        {
            get { return _handler; }
        }


        public CatalogSingleton<Speaker> SpeakerCatalog { get; set; }
        public CatalogSingleton<Event> EventCatalog { get; set; }

        public DetailedEventViewModel()
        {
            SpeakerCatalog=CatalogSingleton<Speaker>.Instance;

            _newEvent = new Event();

            _eventDurationHours = NewEvent.Duration.Hours;
            _eventDurationMinutes = NewEvent.Duration.Minutes;
            SelectedTypeIndex = (int) Type;

            
        }


        public string AbstractHeader { get; set; }
        public string Abstract { get; set; }
        public TimeSpan EventDuration { get; set; }
        public string ImagePath { get; set; }
        public ObservableCollection<Speaker> SpeakersInEvent { get; set; }
        public Event.EventType Type { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventDateHours { get; set; }
        public int SelectedTypeIndex;
        private List<string> _allEventDays;
        public List<string> AllEventsDays
        {
            get { return _allEventDays; }
        }

        private int _selectedDay;
        public int SelectedDay
        {
            get { return _selectedDay; }
        }
        private int _eventDurationHours;
        public int EventDurationHours
        {
            get { return _eventDurationHours; }
        }

        private int _eventDurationMinutes;
        public int EventDurationMinutes
        {
            get { return _eventDurationMinutes; }
        }


        public static Event EventToLoad;
    }
}
