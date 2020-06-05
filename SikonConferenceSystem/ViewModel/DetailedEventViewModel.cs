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
    public class DetailedEventViewModel:IFormattedEventViewModel
    {
        private Event _newEvent;

        public Event NewEvent
        {
            get { return _newEvent; }
        }

        private DetailedEventHandler _handler;

        public DetailedEventHandler Handler
        {
            get { return _handler; }
        }


        public DetailedEventViewModel()
        {


            _newEvent = new Event();

            
            SelectedTypeIndex = (int) Type;
            _handler= new DetailedEventHandler(this);
            
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


        public RelayCommand Test { get { return new RelayCommand(()=>{});} }

        //public static Event EventToLoad;
    }
}
