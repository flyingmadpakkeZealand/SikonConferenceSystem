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
using SikonConferenceSystem.ViewModel.Interfaces;

namespace SikonConferenceSystem.ViewModel
{
    public class SetupEventsPageVM : IFormattedEventViewModel
    {
        //Mockup values. You choose dates by going through a collection of valid dates based on the BaseDateTime and eventDuration.
        public static readonly DateTime BaseDateTime = new DateTime(2020,6,2);
        public static int EventDays = 3;
        //Cannot decide on the best practice for loading a pre-existing Event, therefore putting it into this static variable just before it is accessed is the simplest way.
        
        private Event _newEvent;
        public Event NewEvent
        {
            get { return _newEvent; }
        }

        public CatalogSingleton<Speaker> SpeakersCatalog { get; set; }

        private SetupEventsHandler _handler;
        public SetupEventsHandler Handler
        {
            get { return _handler; }
        }

        public bool DisableAdminControls { get; set; }

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

        public int SelectedTypeIndex { get; set; }

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


        public void LoadOrSetupEvent(Event eventToLoad)
        {
            if (eventToLoad != null)
            {
                _newEvent = new Event(eventToLoad);
                Handler.LoadEvent(_newEvent);
                //Remember to setup SelectedDay on load. And also duration hours and min.
                SetupSelectedDay();
                _eventDurationHours = NewEvent.Duration.Hours;
                _eventDurationMinutes = NewEvent.Duration.Minutes;
                SelectedTypeIndex = (int)Type;
            }
            else
            {
                _newEvent = new Event {EventID = -1};

                SpeakersInEvent = new ObservableCollection<Speaker>();
                SelectedTypeIndex = -1;

                ImagePath = string.Empty;
                Abstract = string.Empty;
                AbstractHeader = string.Empty;
                EventDuration = TimeSpan.Zero;
            }

            _allEventDays = GetDateStrings();
        }

        public SetupEventsPageVM()
        {
            SpeakersCatalog = CatalogSingleton<Speaker>.Instance;

            _handler = new SetupEventsHandler(this);

            

            _pressSpeakersInEventDeleteCommand = new RelayCommand(Handler.RemoveFromSpeakersView);
            _isSaving = false;
            _pressSaveCommand = new RelayCommand(Handler.SaveEvent, ()=>!IsSaving);
        }




        private void SetupSelectedDay()
        {
            int selectedDay = NewEvent.Date.Subtract(BaseDateTime).Days;
            if (selectedDay > 10)
            {
                throw new ArgumentException("Testing date mismatch, selectedDay: " + selectedDay); //TODO: make base date an admin setting.
            }
            else if (selectedDay > EventDays - 1)
            {
                EventDays = selectedDay + 1;
            }

            SelectedDay = selectedDay;
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

        private bool _isSaving;
        public bool IsSaving
        {
            get { return _isSaving;}
            set
            {
                _isSaving = value;
                _pressSaveCommand.RaiseCanExecuteChanged();
            }

        }

        private RelayCommand _pressSaveCommand;

        public ICommand PressSaveCommand
        {
            get { return _pressSaveCommand; }
        }

    }
}
