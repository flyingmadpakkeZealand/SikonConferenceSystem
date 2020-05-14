using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Handler.Interfaces;
using SikonConferenceSystem.ViewModel.Interfaces;

namespace SikonConferenceSystem.Handler
{
    public class LoadEventsHandler : ILoadEvent
    {
        private IFormattedEventViewModel Vm;
        public LoadEventsHandler(IFormattedEventViewModel Vm)
        {
            this.Vm = Vm;
        }

        public void LoadEvent(Event eventToLoad)
        {
            Vm.EventDuration = eventToLoad.Duration;
            Vm.ImagePath = eventToLoad.ImagePath;
            Vm.SpeakersInEvent = new ObservableCollection<Speaker>(eventToLoad.SpeakersInEvent);
            Vm.Type = eventToLoad.Type;

            Vm.EventDate = eventToLoad.Date.Subtract(TimeSpan.FromHours(eventToLoad.Date.Hour));
            Vm.EventDateHours = eventToLoad.Date.TimeOfDay;

            FormatAbstract(eventToLoad.Abstract);
        }

        private void FormatAbstract(string eventAbstract)
        {
            string indexString = eventAbstract.Split(';', 2)[0];
            string pureAbstract = eventAbstract.Remove(0, indexString.Length + 1);
            int headerLength = Convert.ToInt32(indexString);

            Vm.AbstractHeader = pureAbstract.Substring(0, headerLength);
            Vm.Abstract = pureAbstract.Substring(headerLength);
        }
    }
}
