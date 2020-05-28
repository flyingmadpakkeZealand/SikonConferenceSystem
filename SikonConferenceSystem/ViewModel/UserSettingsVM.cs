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

namespace SikonConferenceSystem.ViewModel
{
    public class UserSettingsVM:INotifyPropertyChanged
    {
        private User _newUser;

        public UserSettingsHandler UserSettingsHandler { get; set; }
        public UserSettingsVM()
        {
            _newUser = new User("", "", "","");
            
            UpdateUserCommand = new RelayCommand(UserSettingsHandler.UpdateUser, (() => CheckforBlank() && CheckData()));
        }



        private bool CheckforBlank()
        {
            if (NewUser.Email != "" || NewUser.PhoneNumber != "")
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
            if ((NewUser.Password.Length > 7 && NewUser.PhoneNumber.Length == 8) || 
                (NewUser.Password.Length > 7 && NewUser.Email.Contains("@")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public User NewUser
        {
            get { return _newUser; }
            set
            {
                if (value != null)
                {
                    _newUser = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TransitionName));
                    OnPropertyChanged(nameof(TransitionEmail));
                    OnPropertyChanged(nameof(TransitionPhoneNumber));
                    OnPropertyChanged(nameof(TransitionPassword));
                    ((RelayCommand)UpdateUserCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string TransitionName
        {
            get { return NewUser.Name; }
            set
            {
                NewUser.Name = value;
                ((RelayCommand)UpdateUserCommand).RaiseCanExecuteChanged();
            }
        }

        public string TransitionPhoneNumber
        {
            get { return NewUser.PhoneNumber; }
            set
            {
                NewUser.PhoneNumber = value;
                ((RelayCommand)UpdateUserCommand).RaiseCanExecuteChanged();
            }
        }

        public string TransitionEmail
        {
            get { return NewUser.Email; }
            set
            {
                NewUser.Email = value;
                ((RelayCommand)UpdateUserCommand).RaiseCanExecuteChanged();
            }
        }

        public string TransitionPassword
        {
            get { return NewUser.Password; }
            set
            {
                NewUser.Password = value;
                ((RelayCommand)UpdateUserCommand).RaiseCanExecuteChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand UpdateUserCommand { get; set; }
    }
}
