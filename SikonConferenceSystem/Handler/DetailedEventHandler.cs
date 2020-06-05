using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class DetailedEventHandler
    {
        private int _loadedEventId;
        public Action<bool> OnClickBook { get; set; }

        private DetailedEventViewModel _detailedEventViewModel;

        public DetailedEventHandler(DetailedEventViewModel detailedEventViewModel)
        {
            _detailedEventViewModel = detailedEventViewModel;
        }
        public void LoadEvent(Event eventToLoad)
        {
            new LoadEventsHandler(_detailedEventViewModel).LoadEvent(eventToLoad);
            _loadedEventId = eventToLoad.EventID;
            UpdateEventIsBooked();
        }

        public void UpdateEventIsBooked()
        {
            if (AppData.LoadedUser != null)
            {
                _detailedEventViewModel.EventIsBooked =
                    AppData.LoadedUser.Booking.BookedEventsId.Contains(_loadedEventId);
            }
            else
            {
                _detailedEventViewModel.EventIsBooked = false;
            }
        }

        public void BookEvent()
        {
            OnClickBook?.Invoke(true); //setting IsChecked to true is part of the OnClickBook method, it doesn't work with OnPropertyChanged(), presumably because of focus loss due to the context flyout.
        }
    }
}
