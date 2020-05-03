using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class SetupEventsHandler
    {
        private SetupEventsPageVM _setupEventsPageVm;

        public SetupEventsHandler(SetupEventsPageVM setupEventsPageVM)
        {
            _setupEventsPageVm = setupEventsPageVM;
        }

        private void SaveEvent()
        {

        }

        private void LoadEvent()
        {

        }

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
