using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;
using SikonConferenceSystem.Annotations;

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

        private static Dictionary<string, Action> _stashedMethodsLogin = new Dictionary<string, Action>();

        private static Dictionary<string, Action> _stashedMethodsLogout = new Dictionary<string, Action>();

        private static User _loadedUser;

        public static User LoadedUser
        {
            get { return _loadedUser;}
            set
            {
                _loadedUser = value;
                InvokeUserLogin();
            }
        }

        private static void InvokeUserLogin()
        {
            _onUserLoggedIn?.Invoke();

            foreach (Action action in _stashedMethodsLogin.Values)
            {
                action?.Invoke();
            }
        }

        private static void InvokeUserLogout()
        {
            _onUserLoggedOut?.Invoke();

            foreach (Action action in _stashedMethodsLogout.Values)
            {
                action?.Invoke();
            }
        }

        public static T TryCastUser<T>() where T : User
        {
            return LoadedUser as T;
        }

        public static void StashMethodForLogin(string stashId, [NotNull] Action action)
        {
            if (_stashedMethodsLogin.ContainsKey(stashId))
            {
                _stashedMethodsLogin[stashId] = action;
            }
            else
            {
                _stashedMethodsLogin.Add(stashId, action);
            }
        }

        public static void OnUserLoggedIn(Action action)
        {
            _onUserLoggedIn += action;
        }

        public static void StashMethodForLogout(string stashId, [NotNull] Action action)
        {
            if (_stashedMethodsLogout.ContainsKey(stashId))
            {
                _stashedMethodsLogout[stashId] = action;
            }
            else
            {
                _stashedMethodsLogout.Add(stashId, action);
            }
        }

        public static void OnUserLoggedOut([NotNull] Action action)
        {
            _onUserLoggedOut += action;
        }

        public static bool TryLogOut()
        {
            if (LoadedUser != null)
            {
                _loadedUser = null;
                InvokeUserLogout();
                return true;
            }

            return false;
        }
    }
}
