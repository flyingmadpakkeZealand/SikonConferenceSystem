using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
