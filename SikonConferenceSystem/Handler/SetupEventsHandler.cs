using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Persistency;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class SetupEventsHandler
    {
        public Func<Task<bool>> TriggerOverrideDialogOnSaveEvent { get; set; }

        private SetupEventsPageVM _setupEventsPageVm;

        public SetupEventsHandler(SetupEventsPageVM setupEventsPageVM)
        {
            _setupEventsPageVm = setupEventsPageVM;
        }

        public async void SaveEvent()
        {
            SetupEventsPageVM Vm = _setupEventsPageVm;

            Vm.NewEvent.Duration = Vm.EventDuration;
            Vm.NewEvent.ImagePath = Vm.ImagePath;
            Vm.NewEvent.SpeakersInEvent = Vm.SpeakersInEvent.ToList();
            Vm.NewEvent.Type = Vm.Type;

            Vm.NewEvent.Date = Vm.EventDate;
            Vm.NewEvent.Date = Vm.NewEvent.Date.Add(Vm.EventDateHours);

            Vm.NewEvent.Abstract = UnifyAbstract();

            Consumer<Event> consumer = new Consumer<Event>(ConsumerCatalog.GetUrl<Event>());
            bool postOk = await consumer.PostAsync(Vm.NewEvent);

            if (!postOk && TriggerOverrideDialogOnSaveEvent != null)
            {
                bool confirmOverride = await TriggerOverrideDialogOnSaveEvent();
                if (confirmOverride)
                {
                    bool overrideOk = await consumer.PutAsync(Vm.NewEvent, new[] { Vm.NewEvent.EventID }); 
                }
            }
        }

        private string UnifyAbstract()
        {
            string header = _setupEventsPageVm.AbstractHeader;
            string content = _setupEventsPageVm.Abstract;
            int headerLength = header.Length;

            return $"{headerLength};{header}{content}";
        }

        public void LoadEvent(Event eventToLoad)
        {
            new LoadEventsHandler(_setupEventsPageVm).LoadEvent(eventToLoad);
            //SetupEventsPageVM Vm = _setupEventsPageVm;

            //Vm.EventDuration = eventToLoad.Duration;
            //Vm.ImagePath = eventToLoad.ImagePath;
            //Vm.SpeakersInEvent = new ObservableCollection<Speaker>(eventToLoad.SpeakersInEvent);
            //Vm.Type = eventToLoad.Type;

            //Vm.EventDate = eventToLoad.Date.Subtract(TimeSpan.FromHours(eventToLoad.Date.Hour));
            //Vm.EventDateHours = eventToLoad.Date.TimeOfDay;

            //FormatAbstract(eventToLoad.Abstract);
        }

        //private void FormatAbstract(string eventAbstract)
        //{
        //    string indexString = eventAbstract.Split(';', 2)[0];
        //    string pureAbstract = eventAbstract.Remove(0, indexString.Length + 1);
        //    int headerLength = Convert.ToInt32(indexString);

        //    _setupEventsPageVm.AbstractHeader = pureAbstract.Substring(0, headerLength);
        //    _setupEventsPageVm.Abstract = pureAbstract.Substring(headerLength);
        //}

        //Something more efficient? Async?
        public object SuggestBoxSpeakers(string text)
        {
            if (_setupEventsPageVm.SpeakersCatalog.IsLoading)
            {
                return new string[] {"Database is still loading..."};
            }

            ObservableCollection<Speaker> catalogSpeakers = _setupEventsPageVm.SpeakersCatalog.Catalog;
            ObservableCollection<Speaker> filteredSpeakers = null;
            if (!string.IsNullOrEmpty(text))
            {
                var quary = from speaker in catalogSpeakers
                    where speaker.Name.Contains(text, StringComparison.CurrentCultureIgnoreCase)
                    select speaker;

                filteredSpeakers = new ObservableCollection<Speaker>(quary);
            }
            else
            {
                filteredSpeakers = catalogSpeakers;
            }


            if (filteredSpeakers.Count == 0)
            {
                return new string[] {"No result where found... (Click to refresh NOT IMPLEMENTED YET)"};
            }
            return filteredSpeakers;
        }

        public bool AddUsingText(string text)
        {
            ObservableCollection<Speaker> catalogSpeakers = _setupEventsPageVm.SpeakersCatalog.Catalog;
            ObservableCollection<Speaker> filteredSpeakers = null;
            if (!string.IsNullOrEmpty(text))
            {
                var quary = from speaker in catalogSpeakers
                    where speaker.Name.Contains(text, StringComparison.CurrentCultureIgnoreCase)
                    select speaker;

                List<Speaker> speakers = quary.ToList();
                if (speakers.Count == 1)
                {
                    _setupEventsPageVm.SpeakersInEvent.Add(speakers[0]);
                    return true;
                }
            }

            return false;
        }

        public void RemoveFromSpeakersView(object parameter)
        {
            int id = (int) parameter;

            ObservableCollection<Speaker> speakers = _setupEventsPageVm.SpeakersInEvent;
            for (int i = 0; i < speakers.Count; i++)
            {
                if (speakers[i].Id == id)
                {
                    speakers.RemoveAt(i);
                    return;
                }
            }
        }
    }
}
