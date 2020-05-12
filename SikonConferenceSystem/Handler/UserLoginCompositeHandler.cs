using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Persistency;
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

        public async void Login()
        {
            //Consumer<Tuple<User, Speaker, Admin>> consumer = new Consumer<Tuple<User, Speaker, Admin>>(ConsumerCatalog.GetUrl<Tuple<User, Speaker, Admin>>()); 
            //Tuple<User, Speaker, Admin> loginDetails = await consumer.GetOneAsync()
            NavigateToProfileMenuOnLogin?.Invoke();
        }
    }
}
