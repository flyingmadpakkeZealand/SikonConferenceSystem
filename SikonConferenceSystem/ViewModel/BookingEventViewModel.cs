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
        public static readonly DateTime BaseDateTime = DateTime.Today;
        private Booking _newBooking;
        private BookingEventHandler _handler;

        public BookingEventHandler Handler
        {
            get { return _handler; }
        }
        public BookingEventViewModel()
        {
            BookingEventSingleton = CatalogSingleton<Booking>.Instance;

            _handler=new BookingEventHandler(this);

            
            _bookUserCommand = new RelayCommand(Handler.CreateBooking);
        }

        
        public DateTime BookingDate { get; set; }

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
                    OnPropertyChanged(nameof(BookingDay));
                    ((RelayCommand)BookUserCommand).RaiseCanExecuteChanged();
                }
            }
        }

        
        public CatalogSingleton<Booking> BookingEventSingleton { get; set; }

        
        private User _loadedUser;

        public User LoadedUser
        {
            get { return _loadedUser; }
            set
            {
                _loadedUser=value;
                ((RelayCommand)BookUserCommand).RaiseCanExecuteChanged();
            }
        }


        public int TranstionBookToEvent
        {
            get { return NewBooking.UserId; }
            set
            {
                NewBooking.UserId = value;
                ((RelayCommand)BookUserCommand).RaiseCanExecuteChanged();
            }
        }

        private int _bookingDay;

        public int BookingDay
        {
            get { return _bookingDay; }
            set
            {
                BookingDate = BaseDateTime.Date;
                _bookingDay = value;
                ((RelayCommand)BookUserCommand).RaiseCanExecuteChanged();
            }
        }

        private RelayCommand _bookUserCommand;

        public ICommand BookUserCommand
        {
            get { return _bookUserCommand; }
        }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
