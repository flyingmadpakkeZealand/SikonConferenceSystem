using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SikonConferenceSystem.ViewModel;

namespace SikonConferenceSystem.Handler
{
    public class UserLoginCompositeHandler
    {
        private UserLoginMenusCompositeVM _userLoginMenusCompositeVm;

        //Use a navigation service instead?
        public Action NavigateToProfileMenuOnLogin { get; set; }

        public UserLoginCompositeHandler(UserLoginMenusCompositeVM userLoginMenusCompositeVm)
        {
            _userLoginMenusCompositeVm = userLoginMenusCompositeVm;
        }

        public void Login()
        {
            NavigateToProfileMenuOnLogin?.Invoke();
        }
    }
}
