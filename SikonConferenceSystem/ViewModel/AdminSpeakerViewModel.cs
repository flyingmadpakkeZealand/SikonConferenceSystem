using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Media.Animation;
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
            _newSpeaker = new Speaker("","","","","","");
            AdminSpeakerHandler = new AdminSpeakerHandler(this);
            CreateSpeakerCommand = new RelayCommand(AdminSpeakerHandler.CreateSpeaker, (() => CheckforBlank() && CheckData()));
            DeleteSpeakerCommand = new RelayCommand(AdminSpeakerHandler.DeleteSpeaker, (() => CheckforBlank()));
            UpdateSpeakerCommand = new RelayCommand(AdminSpeakerHandler.UpdateSpeaker, (() => CheckforBlank() && CheckData()));
            ClearSpeakerCommand = new RelayCommand(AdminSpeakerHandler.ClearSpeaker);
        }

        private bool CheckforBlank()
        {
            if ( NewSpeaker.Email != "" || NewSpeaker.PhoneNumber != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckData()
        {
            if ((NewSpeaker.Password.Length > 7 && NewSpeaker.PhoneNumber.Length == 8) || (NewSpeaker.Password.Length > 7 && NewSpeaker.Email.Contains("@")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public Speaker NewSpeaker
        {
            get { return _newSpeaker; }
            set {
                if (value != null)
                {
                    _newSpeaker = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TransitionName));
                    OnPropertyChanged(nameof(TransitionEmail));
                    OnPropertyChanged(nameof(TransitionPhoneNumber));
                    OnPropertyChanged(nameof(TransitionPassword));
                    ((RelayCommand)DeleteSpeakerCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)CreateSpeakerCommand).RaiseCanExecuteChanged();
                    ((RelayCommand)UpdateSpeakerCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string TransitionName
        {
            get { return NewSpeaker.Name; }
            set
            {
                NewSpeaker.Name = value;
                ((RelayCommand)CreateSpeakerCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateSpeakerCommand).RaiseCanExecuteChanged();
            }
        }

        public string TransitionPhoneNumber
        {
            get { return NewSpeaker.PhoneNumber; }
            set
            {
                NewSpeaker.PhoneNumber = value;
                ((RelayCommand)CreateSpeakerCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateSpeakerCommand).RaiseCanExecuteChanged();
            }
        }

        public string TransitionEmail
        {
            get { return NewSpeaker.Email; }
            set
            {
                NewSpeaker.Email = value;
                ((RelayCommand)CreateSpeakerCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateSpeakerCommand).RaiseCanExecuteChanged();
            }
        }

        public string TransitionPassword
        {
            get { return NewSpeaker.Password; }
            set
            {
                NewSpeaker.Password = value;
                ((RelayCommand)CreateSpeakerCommand).RaiseCanExecuteChanged();
                ((RelayCommand)UpdateSpeakerCommand).RaiseCanExecuteChanged();
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
