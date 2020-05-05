using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.WiFiDirect;
using SikonConferenceSystem.Model;
using ModelLibrary;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.Model;

namespace SikonConferenceSystem.ViewModel
{
    public class BookingEventViewModel:INotifyPropertyChanged
    {
        private Booking _newBooking;

        public ObservableCollection<Event> BookedEvents;
        public Event _newEvent;

        public Booking NewBooking
        {
            get { return _newBooking;}
            set { _newBooking = value; }
        }

        public Event NewEvent
        {
            get { return _newEvent; }
            set { _newEvent = value; }
        }
        public CatalogSingleton<Booking> BookingEventSingleton { get; set; }

        public BookingEventViewModel()
        {
            BookingEventSingleton=CatalogSingleton<Booking>.Instance;
            _newBooking=new Booking();
        }





        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
