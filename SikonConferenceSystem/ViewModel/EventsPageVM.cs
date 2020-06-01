using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using ModelLibrary;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.Model;
using SikonConferenceSystem.View;

namespace SikonConferenceSystem.ViewModel
{
    public class EventsPageVM : INotifyPropertyChanged
    {
        private CatalogSingleton<Event> _eventSingleton;

        public CatalogSingleton<Event> EventSingleton
        {
            get { return _eventSingleton; }
        }

        private EventsHandler _handler;

        public EventsHandler Handler
        {
            get { return _handler; }
        }

        private bool _listIsLoading;
        public bool ListIsLoading
        {
            get { return _listIsLoading; }
            set
            {
                _listIsLoading = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<HourGroup> _hourGroups;

        public ObservableCollection<HourGroup> HourGroups
        {
            get { return _hourGroups; }
            set
            {
                _hourGroups = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> EventDays { get; set; }

        private List<ObservableCollection<HourGroup>> _HourGroupsByDate;
        public List<ObservableCollection<HourGroup>> HourGroupsByDate
        {
            get { return _HourGroupsByDate; }
        }

        private int _selectedDayIndex;

        public int SelectedDayIndex
        {
            get { return _selectedDayIndex; }
            set
            {
                //foreach (HourGroup hourGroup in _HourGroupsByDate[value])
                //{
                //    foreach (EventAdapter @event in hourGroup.Events)
                //    {
                //        @event.InformDayChanged(); //Should probably be refactored ;P
                //    }
                //}
                HourGroups = _HourGroupsByDate[value];
                _selectedDayIndex = value;
            }
        }

        private ObservableCollection<FilterVM> _filterVms;
        public ObservableCollection<FilterVM> FilterVms
        {
            get { return _filterVms; }
        }

        private int _hourGroupIterations;
        public EventsPageVM()
        {
            _filterVms = new ObservableCollection<FilterVM>(){new FilterVM()};

            _hourGroupIterations = 24;
            #region Old Mockup Code
            //HourGroups = new ObservableCollection<HourGroup>();

            //List<EventAdapter> allEvents = new List<EventAdapter>();

            //allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(11), TimeSpan.FromHours(1), 5, "The first event", 0, 0, "", null, Event.EventType.Workshop),
            //    "#33DEF010"));
            //allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(11), TimeSpan.FromHours(2), 5, "The second event", 0, 0, "", null, Event.EventType.BigEvent),
            //    "#33EA1616"));
            //allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(12), TimeSpan.FromHours(1), 5, "The Third event", 0, 0, "", null, Event.EventType.Workshop),
            //    "#33DEF010"));
            //allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(12), TimeSpan.FromHours(1), 5, "The Fourth event", 0, 0, "", null, Event.EventType.Workshop),
            //    "#33DEF010"));
            //allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(13), TimeSpan.FromHours(2), 5, "The Fifth event", 0, 0, "", null, Event.EventType.BigEvent),
            //    "#33EA1616"));
            //allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(13), TimeSpan.FromHours(1), 5, "The Sixth event", 0, 0, "", null, Event.EventType.Workshop),
            //    "#33DEF010"));
            //allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(14), TimeSpan.FromHours(1), 5, "The Seventh event", 0, 0, "", null, Event.EventType.Workshop),
            //    "#33DEF010"));
            //allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(14), TimeSpan.FromHours(1), 5, "The Eight event", 0, 0, "", null, Event.EventType.Workshop),
            //    "#33DEF010"));
            //allEvents.Add(new EventAdapter(new Event(DateTime.Today.AddHours(13.5), TimeSpan.FromHours(1), 5, "The Test event", 0, 0, "", null, Event.EventType.Workshop),
            //    "#33DEF010")); 
            #endregion

            _HourGroupsByDate = new List<ObservableCollection<HourGroup>>();
            EventDays = new ObservableCollection<string>();

            _handler = new EventsHandler(this);

            _eventSingleton = CatalogSingleton<Event>.Instance;
            if (_eventSingleton.IsLoading)
            {
                ListIsLoading = true;
                _eventSingleton.Subscribe(SetupEventsList);
            }
            else
            {
                ListIsLoading = false;
                SetupEventsList();
            }

            
        }

        private RelayCommand _goToEventCommand;

        public ICommand GotoEventCommand
        {
            get { return _goToEventCommand; }
        }


        private void SetupEventsList()
        {
            var quary = from @event in EventSingleton.Catalog
                group @event by @event.Date.Subtract(@event.Date.TimeOfDay)/*@event.Date.ToString("dd/MM/yyyy")*/
                into eventByDate
                orderby eventByDate.Key
                select eventByDate;

            foreach (var eventGroup in quary)
            {
                Event eventSample = eventGroup.First();
                if (eventSample == null)
                {
                    throw new ArgumentException("Empty Groups");
                }

                ObservableCollection<HourGroup> hourGroupsThisDay = new ObservableCollection<HourGroup>();
                SetupHourGroups(hourGroupsThisDay);
                AddEvents(eventGroup, hourGroupsThisDay);

                _HourGroupsByDate.Add(hourGroupsThisDay);

                EventDays.Add(eventSample.Date.ToString("dd/MM dddd"));
            }

            SelectedDayIndex = 0;
            ListIsLoading = false;
        }

        private void SetupHourGroups(ObservableCollection<HourGroup> collection)
        {
            for (int i = 0; i < _hourGroupIterations; i++)
            {
                collection.Add(new HourGroup(TimeSpan.FromHours(i)));
            }
        }

        private void AddEvents(IEnumerable<Event> events, ObservableCollection<HourGroup> hourGroupsThisDay)
        {
            List<EventAdapter> adaptedEvents = AdaptEvents(events);

            foreach (HourGroup hourGroup in hourGroupsThisDay)
            {
                foreach (EventAdapter @event in adaptedEvents)
                {
                    if (AddEventCondition(@event, hourGroup))
                    {
                        hourGroup.Events.Add(@event); //Could possibly make new objects here, but memory would be increased unless you had an adapter for the adapter...
                    }
                }
            }
        }

        private List<EventAdapter> AdaptEvents(IEnumerable<Event> events)
        {
            List<EventAdapter> adaptedEvents = new List<EventAdapter>();

            foreach (Event @event in events)
            {
                adaptedEvents.Add(new EventAdapter(@event));
            }

            return adaptedEvents;
        }

        private bool AddEventCondition(EventAdapter @event, HourGroup hourGroup)
        {
            bool startingTime = @event.Event.Date.TimeOfDay >= hourGroup.TimeAsTimeSpan &&
                                @event.Event.Date.TimeOfDay < hourGroup.TimeAsTimeSpan.Add(TimeSpan.FromHours(1));

            bool overlapTime = @event.Event.Date.TimeOfDay.Add(@event.Event.Duration) > hourGroup.TimeAsTimeSpan &&
                               hourGroup.TimeAsTimeSpan > @event.Event.Date.TimeOfDay;

            return startingTime || overlapTime;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class HourGroup
    {
        public ObservableCollection<EventAdapter> Events { get; set; }

        public HourGroup(TimeSpan time)
        {
            Events = new ObservableCollection<EventAdapter>();
            TimeAsTimeSpan = time;
            Time = $"{time:hh\\:mm} : {time.Add(TimeSpan.FromHours(1)):hh\\:mm}";
        }

        public string Time { get; set; }

        public TimeSpan TimeAsTimeSpan { get; set; }
    }

    public class EventAdapter
    {
        private const string WorkshopColor = "#4CE8C20F";
        private const string MediumEventColor = "#4C434032";
        private const string BigEventColor = "#4C0043FF";

        public Event Event { get; set; }
        public string Color { get; set; }
        public string FormattedDescription { get; set; }

        public string FormattedDuration
        {
            get { return FormatDuration(); }
        }

        private bool _formatOccurred;
        private int _hours;
        private int _remainingHours;

        public EventAdapter(Event @event)
        {
            Event = @event;
            FormattedDescription = FormatAbstract(@event.Abstract);
            switch (@event.Type)
            {
                case Event.EventType.Workshop:
                {
                    Color = WorkshopColor;
                } break;
                case Event.EventType.MediumEvent:
                {
                    Color = MediumEventColor;
                } break;
                case Event.EventType.BigEvent:
                {
                    Color = BigEventColor;
                } break;
            }

            DateTime eventHours = @event.Date.Add(@event.Duration);

            _hours = eventHours.Hour - @event.Date.Hour + (eventHours.Minute != 0 ? 1 : 0);
            _remainingHours = _hours;
        }

        private string FormatAbstract(string eventAbstract)
        {
            string[] splitAbstract = eventAbstract.Split(';', 2);
            string pureAbstract = splitAbstract[1];
            int headerLength = Convert.ToInt32(splitAbstract[0]);

            return pureAbstract.Substring(0, headerLength);
        }

        private string FormatDuration()
        {
            string result;
            if (!_formatOccurred)
            {
                result = $"Starts this hour! At {Event.Date.TimeOfDay:hh\\:mm}\nDuration : {Event.Duration:hh\\:mm}";
            }
            else
            {
                result = $"This event started at {Event.Date.TimeOfDay:hh\\:mm}\nDuration : {Event.Duration:hh\\:mm}";
            }

            _remainingHours--;
            if (_remainingHours<=0)
            {
                _remainingHours = _hours;
                _formatOccurred = false;
            }
            else
            {
                _formatOccurred = true;
            }
            
            return result;
        }

        //public void InformDayChanged()
        //{
        //    //_formatOccurred = false;
        //}
    }
}
