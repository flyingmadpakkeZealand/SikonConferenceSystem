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
using SikonConferenceSystem.ViewModel.Interfaces;

namespace SikonConferenceSystem.ViewModel
{
    public class UserLoginSignupMenuVM:IUserLoginMenu,INotifyPropertyChanged
    {

        public UserLoginCompositeHandler UserLoginHandler { get; set; }
        private User _newUser;
        public UserLoginSignupMenuVM()
        {
            _loadedUser = new User();
            DisplayPhoneNumberError = false;
            DisplayMailError = false;
            _newUser = new User("","","","");
            UserLoginHandler = new UserLoginCompositeHandler(this);
            CreateUserCommand = new RelayCommand(() => UserLoginHandler.SignUp(NewUser, CheckNoErrors), (() => CheckforBlank() && CheckData()));
            //ClearUserCommand = new RelayCommand(UserLoginHandler.ClearUser);
        }

        public User  NewUser
        {
            get { return _newUser; }
            set
            {
                if (value != null)
                {
                    _newUser = value;
                    OnPropertyChanged();
                    ((RelayCommand)CreateUserCommand).RaiseCanExecuteChanged();
                }
            }
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
            if ((NewUser.Password.Length > 7 && NewUser.PhoneNumber.Length == 8) || (NewUser.Password.Length > 7 && NewUser.Email.Contains("@")))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string TransitionName
        {
            get { return NewUser.Name; }
            set
            {
                NewUser.Name = value;
                ((RelayCommand)CreateUserCommand).RaiseCanExecuteChanged();
            }
        }

        public string TransitionPhoneNumber
        {
            get { return NewUser.PhoneNumber; }
            set
            {
                NewUser.PhoneNumber = value;
                ((RelayCommand)CreateUserCommand).RaiseCanExecuteChanged();
            }
        }

        public string TransitionEmail
        {
            get { return NewUser.Email; }
            set
            {
                NewUser.Email = value;
                ((RelayCommand)CreateUserCommand).RaiseCanExecuteChanged();
            }
        }

        public string TransitionPassword
        {
            get { return NewUser.Password; }
            set
            {
                NewUser.Password = value;
                ((RelayCommand)CreateUserCommand).RaiseCanExecuteChanged();
            }
        }


        public bool DisplayMailError { get; set; } 
        public bool DisplayPhoneNumberError { get; set; }

        private void CheckNoErrors()
        {
            bool emailTaken = !string.IsNullOrEmpty(LoadedUser.Email);
            bool phoneNumberTaken = !string.IsNullOrEmpty(LoadedUser.PhoneNumber);

            DisplayMailError = emailTaken;
            DisplayPhoneNumberError = phoneNumberTaken;

            OnPropertyChanged(nameof(DisplayMailError));
            OnPropertyChanged(nameof(DisplayPhoneNumberError));
        }

        private User _loadedUser;

        public User LoadedUser
        {
            get { return _loadedUser;}
            set { _loadedUser = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand CreateUserCommand { get; set; }

        public ICommand ClearUserCommand { get; set; }
    }
}
