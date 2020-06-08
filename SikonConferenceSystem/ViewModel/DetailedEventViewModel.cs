using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Media.Core;
using Windows.Media.SpeechRecognition;
using ModelLibrary;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Converter;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.Model;
using SikonConferenceSystem.ViewModel.Interfaces;

namespace SikonConferenceSystem.ViewModel
{
    public class DetailedEventViewModel:IFormattedEventViewModel, INotifyPropertyChanged
    {
        private const string UserIsLoggedInText = "Are you sure you want to book this event?";
        private const string UserNotLoggedInText =
            "Please click the profile button in the top right corner to login or signup,\nbefore booking an event.";

        private DetailedEventHandler _handler;

        public DetailedEventHandler Handler
        {
            get { return _handler; }
        }


        public DetailedEventViewModel()
        {
            HelperText = AppData.LoadedUser != null ? UserIsLoggedInText : UserNotLoggedInText;

            SelectedTypeIndex = (int) Type;
            _handler= new DetailedEventHandler(this);
            
            _pressBookCommand = new RelayCommand(Handler.BookEvent, () => AppData.LoadedUser != null && !IsLoadingBooking);

            AppData.StashMethodForLogin("DEV", OnUserLoggedIn);
        }

        private void OnUserLoggedIn()
        {
            _pressBookCommand.RaiseCanExecuteChanged();
            Handler.UpdateEventIsBooked();
            HelperText = UserIsLoggedInText;
        }

        public string AbstractHeader { get; set; }
        public string Abstract { get; set; }
        public TimeSpan EventDuration { get; set; }
        public string ImagePath { get; set; }
        public ObservableCollection<Speaker> SpeakersInEvent { get; set; }
        public Event.EventType Type { get; set; }
        public DateTime EventDate { get; set; }
        public TimeSpan EventDateHours { get; set; }

        public int SelectedTypeIndex;
        //private List<string> _allEventDays;
        //public List<string> AllEventsDays
        //{
        //    get { return _allEventDays; }
        //}


        //private string _selectedDay;
        //public string SelectedDay
        //{
        //    get { return _selectedDay; }

        //}
        //private int _eventDurationHours;
        //public int EventDurationHours
        //{
        //    get { return _eventDurationHours; }
        //}

        //private int _eventDurationMinutes;
        //public int EventDurationMinutes
        //{
        //    get { return _eventDurationMinutes; }
        //}

        private bool _eventIsBooked;
        public bool EventIsBooked
        {
            get { return _eventIsBooked; }
            set
            {
                _eventIsBooked = value;
                OnPropertyChanged();
            }
        }

        private string _helperText;
        public string HelperText
        {
            get { return _helperText;}
            set
            {
                _helperText = value;
                OnPropertyChanged();
            }
        }

        private bool _isLoadingBooking;
        public bool IsLoadingBooking
        {
            get { return _isLoadingBooking;}
            set
            {
                _isLoadingBooking = value;
                OnPropertyChanged();
                _pressBookCommand.RaiseCanExecuteChanged();
            }
        }

        private RelayCommand _pressBookCommand;
        public ICommand PressBookCommand
        {
            get { return _pressBookCommand; }
        }


        //public static Event EventToLoad;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
