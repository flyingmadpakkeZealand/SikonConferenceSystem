using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModelLibrary;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.Model;

namespace SikonConferenceSystem.ViewModel
{
    public class SetupEventsPageVM
    {
        //Mockup values. You choose dates by going through a collection of valid dates based on the BaseDateTime and eventDuration.
        public static readonly DateTime BaseDateTime;
        public const int eventDuration = 2;
        //Cannot decide on the best practice for loading a pre-existing Event, therefore putting it into this static variable just before it is accessed is the simplest way.
        public static Event EventToLoad;

        private Event _newEvent;

        private SetupEventsHandler _handler;
        public SetupEventsHandler Handler
        {
            get { return _handler; }
        }

        public string Abstract { get; set; }
        public string AbstractHeader { get; set; }

        public TimeSpan EventDuration { get; set; }

        public string ImagePath { get; set; }

        public ObservableCollection<Speaker> SpeakersInEvent { get; set; }

        public CatalogSingleton<Speaker> SpeakersCatalog { get; set; }

        public SetupEventsPageVM()
        {
            _handler = new SetupEventsHandler(this);
            if (EventToLoad != null)
            {
                _newEvent = EventToLoad;
                EventToLoad = null;
            }
            else
            {
                _newEvent = new Event();
            }

            SpeakersCatalog = CatalogSingleton<Speaker>.Instance;

            SpeakersInEvent = new ObservableCollection<Speaker>();

            _pressSpeakersInEventDeleteCommand = new RelayCommand(Handler.RemoveFromSpeakersView);
        }

        private RelayCommand _pressSpeakersInEventDeleteCommand;

        public ICommand PressSpeakersInEventDeleteCommand
        {
            get { return _pressSpeakersInEventDeleteCommand; }
            
        }

    }
}
