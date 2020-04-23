using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ModelLibrary;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Model;

namespace SikonConferenceSystem.ViewModel
{
    public class AdminSpeakerPage:INotifyPropertyChanged
    {
        private Speaker _newSpeaker;

        public CatalogSingleton<Speaker> AdminSpeakerSingleton { get; set; }

        public AdminSpeakerPage()
        {
            AdminSpeakerSingleton = CatalogSingleton<Speaker>.Instance;
            _newSpeaker = new Speaker();
        }

        public Speaker NewSpeaker
        {
            get { return _newSpeaker; }
            set { _newSpeaker = value; OnPropertyChanged(); }
        }

        public ICommand CreateSpeakerCommand { get; set; }
        public ICommand DeleteSpeakerCommand { get; set; }
        public ICommand UpdateSpeakerCommand { get; set; }
        public ICommand ClearSpeakerCommand { get; set; }
















        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
