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
using SikonConferenceSystem.ViewModel.Interfaces;

namespace SikonConferenceSystem.ViewModel
{
    public class UserLoginMenuVM : INotifyPropertyChanged, IUserLoginMenu
    {
        private UserLoginCompositeHandler _handler;

        public UserLoginCompositeHandler Handler
        {
            get { return _handler; }
        }

        public NavigationService NavigationService { get; set; }

        public User LoadedUser { get; set; }

        #region UserLoginMenuProps
        private string _loginId;
        public string LoginId
        {
            get { return _loginId; }
            set
            {
                _loginId = value;
                _pressLoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                _pressLoginCommand.RaiseCanExecuteChanged();
            }
        }

        public bool DisplayLoginIdErrorMessage { get; set; }
        private string _loginIdErrorMessage;
        public string LoginIdErrorMessage
        {
            get { return _loginIdErrorMessage; }
            set
            {
                _loginIdErrorMessage = value;
                DisplayLoginIdErrorMessage = value != "OK";
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayLoginIdErrorMessage));
            }
        }

        public bool DisplayPasswordErrorMessage { get; set; }
        private string _passwordErrorMessage;
        public string PasswordErrorMessage
        {
            get { return _passwordErrorMessage; }
            set
            {
                _passwordErrorMessage = value;
                DisplayPasswordErrorMessage = value != "OK";
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayPasswordErrorMessage));
            }
        }

        private bool _isLoadingUser;
        public bool IsLoadingUser
        {
            get { return _isLoadingUser; }
            set
            {
                _isLoadingUser = value;
                OnPropertyChanged();
                _pressLoginCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _wrongLogin;
        public bool WrongLogin
        {
            get { return _wrongLogin; }
            set
            {
                _wrongLogin = value;
                OnPropertyChanged();
            }
        } 
        #endregion

        public UserLoginMenuVM()
        {
            _loginId = string.Empty;
            _password = string.Empty;

            _handler = new UserLoginCompositeHandler(this);

            _pressLoginCommand = new RelayCommand(Handler.Login, ()=>ValidLogin() && !IsLoadingUser);

            LoginIdErrorMessage = "OK";
            PasswordErrorMessage = "OK";
        }

        private RelayCommand _pressLoginCommand;

        public ICommand PressLoginCommand
        {
            get { return _pressLoginCommand; }
        }


        private bool ValidLogin()
        {
            bool isEmail = LoginId.Contains('@');

            bool passwordValid = !string.IsNullOrEmpty(Password);
            PasswordErrorMessage = !passwordValid ? "Password cannot be empty" : "OK";

            if (!isEmail)
            {
                if (LoginId.Length == 0)
                {
                    LoginIdErrorMessage = "This field cannot be empty";
                    return false;
                }

                foreach (char character in LoginId)
                {
                    if (character<48 || character>57)
                    {
                        LoginIdErrorMessage = "A valid email must contain \"@\"";
                        return false;
                    }
                }
            }

            LoginIdErrorMessage = "OK";

            return passwordValid;
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
