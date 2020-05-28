using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace SikonConferenceSystem.Common
{
    public enum SpecialCase
    {
        None,
        OnSpeakerEdit,
        OnAdminEdit
    }

    public static class AppData
    {
        private static event Action _onUserLoggedOut;

        private static event Action _onUserLoggedIn;

        private static User _loadedUser;

        public static User LoadedUser
        {
            get { return _loadedUser;}
            set
            {
                _loadedUser = value;
                _onUserLoggedIn?.Invoke();
            }
        }

        public static T TryCastUser<T>() where T : User
        {
            return LoadedUser as T;
        }

        public static void OnUserLoggedIn(Action action)
        {
            _onUserLoggedIn += action;
        }

        public static void OnUserLoggedOut(Action action)
        {
            _onUserLoggedOut += action;
        }

        public static bool TryLogOut()
        {
            if (LoadedUser != null)
            {
                _loadedUser = null;
                _onUserLoggedOut?.Invoke();
                return true;
            }

            return false;
        }
    }
}
