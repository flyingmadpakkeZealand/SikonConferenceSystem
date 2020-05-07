using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.Model;

namespace SikonConferenceSystem.ViewModel
{
    public class DetailedEventViewModel:INotifyPropertyChanged
    {
        
        public SetupEventsHandler _handler { get; }

        public SetupEventsHandler Handler
        {
            get { return _handler; }
        }
        public CatalogSingleton<Speaker> SpeakerCatalog { get; set; }
        public CatalogSingleton<Event> EventCatalog { get; set; }

        public DetailedEventViewModel()
        {
            SpeakerCatalog=CatalogSingleton<Speaker>.Instance;
            EventCatalog=CatalogSingleton<Event>.Instance;
        }
        


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
