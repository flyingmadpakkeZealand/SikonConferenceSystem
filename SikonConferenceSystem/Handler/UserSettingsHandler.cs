using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Common;
using SikonConferenceSystem.Persistency;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class UserSettingsHandler
    {
        private UserSettingsVM _userSettingsVM;

        public UserSettingsHandler(UserSettingsVM userSettingsVm)
        {
            _userSettingsVM = userSettingsVm;
        }

        public async void UpdateUser()
        {
            string speakerName = _userSettingsVM.NewUser.Name;
            string speakerEmail = _userSettingsVM.NewUser.Email;
            string speakerPhoneNumber = _userSettingsVM.NewUser.PhoneNumber;
            string speakerPassword = _userSettingsVM.NewUser.Password;

            User aUser = new User(speakerName, speakerPhoneNumber, speakerEmail, speakerPassword);
            Consumer<User> UserSettingsFacade = new Consumer<User>(ConsumerCatalog.GetUrl<User>());
            bool ok = await UserSettingsFacade.PutAsync(aUser, new[] { _userSettingsVM.NewUser.Id });


        }
    }
}
