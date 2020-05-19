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

        public EventsHandler(EventsPageVM eventsPageVm)
        {
            _eventsPageVm = eventsPageVm;
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
    }
}
