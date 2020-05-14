using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace SikonConferenceSystem.ViewModel.Interfaces
{
    public interface IFormattedEventViewModel
    {
        string AbstractHeader { get; set; }
        string Abstract { get; set; }

        TimeSpan EventDuration { get; set; }

        string ImagePath { get; set; }

        ObservableCollection<Speaker> SpeakersInEvent { get; set; }

        Event.EventType Type { get; set; }

        DateTime EventDate { get; set; }
        TimeSpan EventDateHours { get; set; }
    }
}
