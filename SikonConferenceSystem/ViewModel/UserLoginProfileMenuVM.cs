using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Annotations;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Handler;
using SikonConferenceSystem.ViewModel.Interfaces;

namespace SikonConferenceSystem.ViewModel
{
    public class UserLoginProfileMenuVM : INotifyPropertyChanged, IUserLoginMenu
    {
        private UserLoginCompositeHandler _handler;

        public UserLoginCompositeHandler Handler
        {
            get { return _handler; }
        }

        private User _loadedUser;

        public User LoadedUser
        {
            get { return _loadedUser; }
            set
            {
                _loadedUser = value;
                AppData.LoadedUser = value;
                if (value is Speaker speaker)
                {
                    SpecialUserInfo = "Speaker at Sikon";
                }
                else if (value is Admin admin)
                {
                    SpecialUserInfo = $"Admin at Sikon\nLevel: {admin.AccessLevel}";
                }
            }
        }

        public bool DisplaySpecialUserInfo { get; set; }
        private string _specialUserInfo;
        public string SpecialUserInfo
        {
            get { return _specialUserInfo; }
            set
            {
                _specialUserInfo = value;
                DisplaySpecialUserInfo = !string.IsNullOrEmpty(value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplaySpecialUserInfo));
            }
        }


        public UserLoginProfileMenuVM()
        {
            _handler = new UserLoginCompositeHandler(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
