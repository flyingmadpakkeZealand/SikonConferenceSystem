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
    class UserLoginSignupMenuVM:IUserLoginMenu,INotifyPropertyChanged
    {
        public CatalogSingleton<User> SignupUserSingleton { get; set; }

        public UserLoginCompositeHandler UserLoginHandler { get; set; }
        public User _newUser { get; set; }
        public UserLoginSignupMenuVM()
        {
            SignupUserSingleton = CatalogSingleton<User>.Instance;
            _newUser = new User("", "", "", "");
            UserLoginHandler = new UserLoginCompositeHandler(this);
            //CreateUserCommand = new RelayCommand(UserLoginHandler.CreateUser, (() => CheckforBlank() && CheckData()));
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

        public User LoadedUser { get; set; }
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
