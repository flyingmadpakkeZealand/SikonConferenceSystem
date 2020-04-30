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
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.Model;

namespace SikonConferenceSystem.ViewModel
{
    public class AdminSpeakerViewModel:INotifyPropertyChanged
    {
        private Speaker _newSpeaker;
        public AdminSpeakerHandler AdminSpeakerHandler { get; set; }

        public int SelectedSpeakerIndex { get; set; }

        public CatalogSingleton<Speaker> AdminSpeakerSingleton { get; set; }

        public AdminSpeakerViewModel()
        {
            AdminSpeakerSingleton = CatalogSingleton<Speaker>.Instance;
            _newSpeaker = new Speaker();
            AdminSpeakerHandler = new AdminSpeakerHandler(this);
            CreateSpeakerCommand = new RelayCommand(AdminSpeakerHandler.CreateSpeaker, (() => NewSpeaker.PhoneNumber!="" && NewSpeaker.Email!=""));
            DeleteSpeakerCommand = new RelayCommand(AdminSpeakerHandler.DeleteSpeaker, (() => NewSpeaker.PhoneNumber != "" && NewSpeaker.Email != ""));
            UpdateSpeakerCommand = new RelayCommand(AdminSpeakerHandler.UpdateSpeaker, (() => NewSpeaker.PhoneNumber != "" && NewSpeaker.Email != ""));
            ClearSpeakerCommand = new RelayCommand(AdminSpeakerHandler.ClearSpeaker, (() => NewSpeaker.PhoneNumber != "" && NewSpeaker.Email != ""));
        }

        public Speaker NewSpeaker
        {
            get { return _newSpeaker; }
            set {
                if (value != null)
                {
                    _newSpeaker = value;
                    OnPropertyChanged();
                    ((RelayCommand)DeleteSpeakerCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)CreateSpeakerCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)UpdateSpeakerCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)ClearSpeakerCommand).RaiseCanExecuteChanged();
                }
            }
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
