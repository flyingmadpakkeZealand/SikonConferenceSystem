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

        private static Dictionary<int, Event> _loadedEventsById;
        public static Dictionary<int, Event> LoadedEventsById
        {
            get { return _loadedEventsById; }
        }

        public static void SetLoadedEventsById(IEnumerable<Event> events)
        {
            if (events != null)
            {
                _loadedEventsById = events.ToDictionary((@event => @event.EventID));
            }
            else
            {
                _loadedEventsById = new Dictionary<int, Event>();
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
                _loadedEventsById = null;
                _onUserLoggedOut?.Invoke();
                return true;
            }

            return false;
        }
    }
}
