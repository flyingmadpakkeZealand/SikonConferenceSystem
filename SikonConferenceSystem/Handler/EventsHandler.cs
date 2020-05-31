using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.View;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class EventsHandler
    {
        private EventsPageVM _eventsPageVm;

        private List<EventsInfo> _filteredEvents;
        private List<EventsInfo> _bufferFilteredEvents;

        private List<Func<Event, bool>> _filters;

        public EventsHandler(EventsPageVM eventsPageVm)
        {
            _eventsPageVm = eventsPageVm;
            _filteredEvents = new List<EventsInfo>();
            _bufferFilteredEvents = new List<EventsInfo>();
            _filters = new List<Func<Event, bool>>();
        }

        public void ApplySpeakerEditFilter()
        {
            EventsPageVM Vm = _eventsPageVm;
            Speaker loadedSpeaker = AppData.TryCastUser<Speaker>();

            if (loadedSpeaker != null)
            {
                if (Vm.EventSingleton.IsLoading)
                {
                    Vm.EventSingleton.Subscribe(()=>SpeakerEditFilter(loadedSpeaker));
                }
                else
                {
                    SpeakerEditFilter(loadedSpeaker);
                }
            }
        }

        public void SpeakerEditFilter(Speaker speaker)
        {
            EventsPageVM Vm = _eventsPageVm;

            foreach (ObservableCollection<HourGroup> hourGroups in Vm.HourGroupsByDate)
            {
                foreach (HourGroup hourGroup in hourGroups)
                {
                    ObservableCollection<EventAdapter> events = hourGroup.Events;

                    for (int i = 0; i < events.Count; i++)
                    {
                        List<Speaker> speakersInEvent = events[i].Event.SpeakersInEvent;

                        if (speakersInEvent.Find((speakerInEvent => speakerInEvent.Id == speaker.Id)) == null)
                        {
                            events.RemoveAt(i);
                            i += -1;
                        }
                    }
                }
            }
        }

        //public void EditEvent()
        //{
        //    /*MainPageViewModel.InstanceNav.Navigate((Type) MainPageViewModel.InstanceNav.SetupEventsPage)*/
        //    NavigationService navigationService = NavigationService.GetService(Contents.MainPageContent);
        //    navigationService.Navigate(navigationService.SetupEventsPage);
        //}

        public void ApplyFilter()
        {
            ConstructFilters();

            AllEventsWithFilter(RunAllConditionsAsAnd);

            _filters.Clear();
        }

        private void ConstructFilters()
        {
            EventsPageVM Vm = _eventsPageVm;

            foreach (FilterVM filterVm in Vm.FilterVms)
            {
                _filters.Add(filterVm.FilterBuilder.ConstructFilterFunc());
            }
        }

        private bool RunAllConditionsAsAnd(EventAdapter eventToFilter)
        {
            foreach (Func<Event, bool> filter in _filters)
            {
                if (!filter(eventToFilter.Event))
                {
                    return false;
                }
            }

            return true;
        }

        private void AllEventsWithFilter(Func<EventAdapter, bool> condition)
        {
            EventsPageVM Vm = _eventsPageVm;

            MoveFilteredEventsToBuffer(condition);

            int dayIndex = 0;
            foreach (ObservableCollection<HourGroup> hourGroups in Vm.HourGroupsByDate)
            {
                for (int hourIndex = 0; hourIndex < hourGroups.Count; hourIndex++)
                {
                    ObservableCollection<EventAdapter> events = hourGroups[hourIndex].Events;
                    for (int i = 0; i < events.Count; i++)
                    {
                        if (!condition(events[i]))
                        {
                            _filteredEvents.Add(new EventsInfo(dayIndex, hourIndex, events[i]));
                            events.RemoveAt(i);
                            i += -1;
                        }
                    }
                }

                dayIndex++;
            }

            MoveBufferEventsToAllEvents();
        }

        private void MoveFilteredEventsToBuffer(Func<EventAdapter, bool> condition)
        {
            for (int i = 0; i < _filteredEvents.Count; i++)
            {
                if (condition(_filteredEvents[i].EventAdapter))
                {
                    _bufferFilteredEvents.Add(new EventsInfo(_filteredEvents[i].DayIndex, _filteredEvents[i].HourIndex, _filteredEvents[i].EventAdapter));
                    _filteredEvents.RemoveAt(i);
                    i += -1;
                }
            }
        }

        private void MoveBufferEventsToAllEvents()
        {
            EventsPageVM Vm = _eventsPageVm;

            foreach (EventsInfo eventsInfo in _bufferFilteredEvents)
            {
                Vm.HourGroupsByDate[eventsInfo.DayIndex][eventsInfo.HourIndex].Events.Add(eventsInfo.EventAdapter);
            }

            _bufferFilteredEvents.Clear();
        }

        private class EventsInfo
        {
            public int DayIndex { get; set; }
            public int HourIndex { get; set; }

            public EventAdapter EventAdapter { get; set; }

            public EventsInfo(int dayIndex, int hourIndex, EventAdapter @event)
            {
                DayIndex = dayIndex;
                HourIndex = hourIndex;
                EventAdapter = @event;
            }
        }
    }
}
