using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Handler;

namespace SikonConferenceSystem.ViewModel
{
    public class UserLoginMenusCompositeVM
    {
        private UserLoginCompositeHandler _handler;

        public UserLoginCompositeHandler Handler
        {
            get { return _handler; }
        }

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

        public UserLoginMenusCompositeVM()
        {
            _loginId = string.Empty;
            _password = string.Empty;

            _handler = new UserLoginCompositeHandler(this);

            _pressLoginCommand = new RelayCommand(Handler.Login, ValidLogin);
        }

        private RelayCommand _pressLoginCommand;

        public ICommand PressLoginCommand
        {
            get { return _pressLoginCommand; }
        }


        private bool ValidLogin()
        {
            bool isEmail = LoginId.Contains('@');

            
            if (!isEmail)
            {
                if (LoginId.Length == 0)
                {
                    return false;
                }

                foreach (char character in LoginId)
                {
                    if (character<48 || character>57)
                    {
                        return false;
                    }
                }
            }

            return !string.IsNullOrEmpty(Password);
        }
    }
}
