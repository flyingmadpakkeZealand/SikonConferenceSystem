using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.WiFiDirect;
using SikonConferenceSystem.Model;
using ModelLibrary;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.Model;
using SikonConferenceSystem.ViewModel.Interfaces;

namespace SikonConferenceSystem.ViewModel
{
    public class BookingEventViewModel:INotifyPropertyChanged
    {
        private Booking _newBooking;
        public BookingEventHandler BookingEventHandler { get; set; }
        public BookingEventViewModel()
        {
            BookingEventSingleton = CatalogSingleton<Booking>.Instance;
            _newBooking = new Booking();
            BookingEventHandler = new BookingEventHandler();
            BookUserCommand = new RelayCommand(BookingEventHandler.CreateBooking);
            /*_bookUserCommand = new RelayCommand(BookingEventHandler.CreateBooking)*/
            ;

        }

        public ObservableCollection<Event> BookedEvents;

        public Booking NewBooking
        {
            get { return _newBooking;}
            set
            {
                if (value != null)
                {
                    _newBooking = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TranstionBookToEvent));
                    ((RelayCommand)BookUserCommand).RaiseCanExecuteChanged();
                }
            }
        }

        
        public CatalogSingleton<Booking> BookingEventSingleton { get; set; }


        public int TranstionBookToEvent
        {
            get { return NewBooking.BookingID; }
            set
            {
                NewBooking.BookingID = value;
                ((RelayCommand)BookUserCommand).RaiseCanExecuteChanged();
            }
        }


        public ICommand BookUserCommand { get; set; }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
