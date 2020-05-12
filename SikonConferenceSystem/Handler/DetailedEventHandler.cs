using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class DetailedEventHandler
    {
        private DetailedEventViewModel _detailedEventViewModel;

        public DetailedEventHandler(DetailedEventViewModel detailedEventViewModel)
        {
            _detailedEventViewModel = detailedEventViewModel;
        }
        public void LoadEvent(Event eventToLoad)
        {
            new LoadEventsHandler(_detailedEventViewModel).LoadEvent(eventToLoad);
        }

    }
}
